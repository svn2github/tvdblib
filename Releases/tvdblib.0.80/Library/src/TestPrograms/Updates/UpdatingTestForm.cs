using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TvdbLib;
using TvdbLib.Cache;
using TvdbLib.Data;
using TvdbTester;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using TestPrograms.Updates;
using TvdbLib.Data.Banner;

namespace TestPrograms
{
  public enum ItemState
  {
    Unchanged = 0,
    Updated = 1,
    UnchangedAndInconsistent = 2,
    UpdatedAndInconsistent = 3,
    AvailableButNotDownloaded = 4,
    Added = 5,
    AddedAndInconsistent = 6,
    UpdatedAndValueChanged = 7,
    None = 8
  }

  public enum ItemType
  {
    SeriesAttribute = 0,
    Episode = 1,
    EpisodeAttribute = 2,
    Series = 3,
    Heading = 4,
    Banner = 5,
    Actor = 6
  }


  public partial class UpdatingTestForm : Form
  {
    TvdbHandler m_tvdbHandler;
    TvdbDownloader m_tvdbDownloader;
    List<TvdbSeries> m_beforeUpdateList = new List<TvdbSeries>();
    List<TvdbSeries> m_afterUpdateList = new List<TvdbSeries>();
    List<TvdbSeries> m_currentVersionList = new List<TvdbSeries>();

    List<int> m_updatedSeries = new List<int>();
    List<int> m_updatedEpisodes = new List<int>();

    public UpdatingTestForm()
    {
      InitializeComponent();

    }

    private void cmdInitTvdbHandler_Click(object sender, EventArgs e)
    {
      InitTvdblibHandler();
    }

    private void InitTvdblibHandler()
    {
      ICacheProvider provider;
      if (cbCacheType.SelectedIndex == 0 || cbCacheType.SelectedIndex == -1)
      {
        provider = new XmlCacheProvider(txtCacheLocation.Text);
      }
      else
      {
        provider = new BinaryCacheProvider(txtCacheLocation.Text);
      }

      lbCacheSnapshots.Items.Clear();
      foreach (String f in Directory.GetFiles(Directory.GetCurrentDirectory(), "Rev_*.zip"))
      {
        FileInfo file = new FileInfo(f);
        CacheRevision rev = CacheRevision.CreateFromFile(file);

        if (rev != null)
        {
          lbCacheSnapshots.Items.Add(rev);
        }
      }

      m_tvdbHandler = new TvdbHandler(provider, File.ReadAllText("api_key.txt"));
      // m_tvdbHandler.UserInfo = new TvdbLib.Data.TvdbUser("DieBagger", txtUserId.Text);
      m_tvdbHandler.InitCache();
      txtLastUpdated.Text = m_tvdbHandler.GetLastUpdate().ToString();

      List<int> cached = m_tvdbHandler.GetCachedSeries();
      lvCachedSeries.Items.Clear();
      cached.ForEach(delegate(int s)
      {
        ListViewItem item = new ListViewItem(s.ToString());
        item.SubItems.Add("");
        item.Tag = s;
        lvCachedSeries.Items.Add(item);
      });
      m_tvdbDownloader = new TvdbDownloader(File.ReadAllText("api_key.txt"));
      m_tvdbHandler.UpdateFinished += new TvdbHandler.UpdateFinishedDelegate(m_tvdbHandler_UpdateFinished);
    }

    private void lvCachedSeries_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void lvCachedSeries_DoubleClick(object sender, EventArgs e)
    {
      if (lvCachedSeries.SelectedItems.Count == 1)
      {
        CheckSeries(lvCachedSeries.SelectedItems[0]);
      }
    }

    private void checkToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (lvCachedSeries.SelectedItems.Count == 1)
      {
        CheckSeries(lvCachedSeries.SelectedItems[0]);
      }
    }

    private void checkSelectedToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (lvCachedSeries.SelectedItems.Count >= 1)
      {
        foreach (ListViewItem i in lvCachedSeries.SelectedItems)
        {
          CheckSeries(i);
        }
      }
    }

    private void CheckSeries(ListViewItem _itemToCheck)
    {
      int seriesId = (int)_itemToCheck.Tag;

      TvdbSeries before = GetBefore(seriesId);
      TvdbSeries after = GetAfter(seriesId);

      if (before == null)
      {
        before = m_tvdbHandler.GetSeries(seriesId, TvdbLanguage.DefaultLanguage, true, true, true);
        if (!m_beforeUpdateList.Contains(before)) m_beforeUpdateList.Add(before);
      }

      _itemToCheck.SubItems[1].Text = before.SeriesName;

      TvdbSeries current = null;
      if (cbDownloadCurrentVersion.Checked)
      {
        current = GetCurrent(before.Id);

        if (current == null)
        {
          try
          {
            current = m_tvdbDownloader.DownloadSeries(before.Id, before.Language, true, true, true);
            m_currentVersionList.Add(current);
          }
          catch (Exception ex)
          {
            MessageBox.Show(ex.ToString());
          }
        }
      }

      ItemState status = FillSeriesDetails(before, after, current);
      SetColorForListViewItem(_itemToCheck, status, ItemType.Series);
    }

    private ItemState FillSeriesDetails(TvdbSeries _before, TvdbSeries _after, TvdbSeries _current)
    {
      lvSeriesDetails.Items.Clear();
      lvSeriesDetails.Tag = _before.Id;

      //id
      lvSeriesDetails.Items.Add(CreateItem("Id", _before != null ? _before.Id.ToString() : "",
                                                 _after != null ? _after.Id.ToString() : "",
                                                 _current != null ? _current.Id.ToString() : ""));

      //name
      lvSeriesDetails.Items.Add(CreateItem("name", _before != null ? _before.SeriesName.ToString() : "",
                                           _after != null ? _after.SeriesName.ToString() : "",
                                           _current != null ? _current.SeriesName.ToString() : ""));


      //actors
      lvSeriesDetails.Items.Add(CreateItem("actors", _before != null ? _before.ActorsString.ToString() : "",
                                           _after != null ? _after.ActorsString.ToString() : "",
                                           _current != null ? _current.ActorsString.ToString() : ""));

      //airs day
      lvSeriesDetails.Items.Add(CreateItem("airs day", _before != null ? _before.AirsDayOfWeek.ToString() : "",
                                           _after != null ? _after.AirsDayOfWeek.ToString() : "",
                                           _current != null ? _current.AirsDayOfWeek.ToString() : ""));

      //airs time
      lvSeriesDetails.Items.Add(CreateItem("airs time", _before != null ? _before.AirsTime.ToString() : "",
                                           _after != null ? _after.AirsTime.ToString() : "",
                                           _current != null ? _current.AirsTime.ToString() : ""));

      //content rating
      lvSeriesDetails.Items.Add(CreateItem("content rating", _before != null ? _before.ContentRating.ToString() : "",
                                           _after != null ? _after.ContentRating.ToString() : "",
                                           _current != null ? _current.ContentRating.ToString() : ""));

      //first aired
      lvSeriesDetails.Items.Add(CreateItem("first aired", _before != null ? _before.FirstAired.ToString() : "",
                                           _after != null ? _after.FirstAired.ToString() : "",
                                           _current != null ? _current.FirstAired.ToString() : ""));

      //genre
      lvSeriesDetails.Items.Add(CreateItem("genre", _before != null ? _before.GenreString.ToString() : "",
                                           _after != null ? _after.GenreString.ToString() : "",
                                           _current != null ? _current.GenreString.ToString() : ""));

      //imdb id
      lvSeriesDetails.Items.Add(CreateItem("imdb id", _before != null ? _before.ImdbId.ToString() : "",
                                           _after != null ? _after.ImdbId.ToString() : "",
                                           _current != null ? _current.ImdbId.ToString() : ""));

      //language
      lvSeriesDetails.Items.Add(CreateItem("language", _before != null ? _before.Language.ToString() : "",
                                           _after != null ? _after.Language.ToString() : "",
                                           _current != null ? _current.Language.ToString() : ""));

      //network
      lvSeriesDetails.Items.Add(CreateItem("network", _before != null ? _before.Network.ToString() : "",
                                           _after != null ? _after.Network.ToString() : "",
                                           _current != null ? _current.Network.ToString() : ""));

      //overview
      lvSeriesDetails.Items.Add(CreateItem("overview", _before != null ? _before.Overview.ToString() : "",
                                           _after != null ? _after.Overview.ToString() : "",
                                           _current != null ? _current.Overview.ToString() : ""));

      //rating
      lvSeriesDetails.Items.Add(CreateItem("rating", _before != null ? _before.Rating.ToString() : "",
                                           _after != null ? _after.Rating.ToString() : "",
                                           _current != null ? _current.Rating.ToString() : ""));

      //runtime
      lvSeriesDetails.Items.Add(CreateItem("runtime", _before != null ? _before.Runtime.ToString() : "",
                                           _after != null ? _after.Runtime.ToString() : "",
                                           _current != null ? _current.Runtime.ToString() : ""));

      //series id (tv.com id)
      lvSeriesDetails.Items.Add(CreateItem("tv.com id", _before != null ? _before.TvDotComId.ToString() : "",
                                           _after != null ? _after.TvDotComId.ToString() : "",
                                           _current != null ? _current.TvDotComId.ToString() : ""));

      //status
      lvSeriesDetails.Items.Add(CreateItem("status", _before != null ? _before.Status.ToString() : "",
                                           _after != null ? _after.Status.ToString() : "",
                                           _current != null ? _current.Status.ToString() : ""));

      //banner
      lvSeriesDetails.Items.Add(CreateItem("banner", _before != null ? _before.BannerPath.ToString() : "",
                                           _after != null ? _after.BannerPath.ToString() : "",
                                           _current != null ? _current.BannerPath.ToString() : ""));

      //fanart
      lvSeriesDetails.Items.Add(CreateItem("fanart", _before != null ? _before.FanartPath.ToString() : "",
                                           _after != null ? _after.FanartPath.ToString() : "",
                                           _current != null ? _current.FanartPath.ToString() : ""));

      //lastupdated
      lvSeriesDetails.Items.Add(CreateItem("lastupdated", _before != null ? _before.LastUpdated.ToString() : "",
                                           _after != null ? _after.LastUpdated.ToString() : "",
                                           _current != null ? _current.LastUpdated.ToString() : ""));

      //poster
      lvSeriesDetails.Items.Add(CreateItem("poster", _before != null ? _before.PosterPath.ToString() : "",
                                           _after != null ? _after.PosterPath.ToString() : "",
                                           _current != null ? _current.PosterPath.ToString() : ""));

      //zap2it
      lvSeriesDetails.Items.Add(CreateItem("zap2it", _before != null ? _before.Zap2itId.ToString() : "",
                                           _after != null ? _after.Zap2itId.ToString() : "",
                                           _current != null ? _current.Zap2itId.ToString() : ""));

      lvSeriesDetails.Items.Add(CreateHeadingItem("", "", "", ""));
      lvSeriesDetails.Items.Add(CreateHeadingItem("---------------------", "Episodes:", "-----------------", "-----------------"));

      List<int> handledEpisodes = new List<int>();
      if (_before != null)
      {
        foreach (TvdbEpisode e in _before.Episodes)
        {
          if (!handledEpisodes.Contains(e.Id))
          {
            lvSeriesDetails.Items.Add(CreateEpisodeItem(e.Id, _before, _after, _current));
            handledEpisodes.Add(e.Id);
          }
        }
      }
      if (_after != null)
      {
        foreach (TvdbEpisode e in _after.Episodes)
        {
          if (!handledEpisodes.Contains(e.Id))
          {
            lvSeriesDetails.Items.Add(CreateEpisodeItem(e.Id, _before, _after, _current));
            handledEpisodes.Add(e.Id);
          }
        }
      }

      if (_current != null)
      {
        foreach (TvdbEpisode e in _current.Episodes)
        {
          if (!handledEpisodes.Contains(e.Id))
          {
            lvSeriesDetails.Items.Add(CreateEpisodeItem(e.Id, _before, _after, _current));
            handledEpisodes.Add(e.Id);
          }
        }
      }

      lvSeriesDetails.Items.Add(CreateHeadingItem("", "", "", ""));
      lvSeriesDetails.Items.Add(CreateHeadingItem("---------------------", "Banners:", "-----------------", "-----------------"));

      List<String> handledBanners = new List<String>();
      if (_before != null)
      {
        foreach (TvdbBanner b in _before.Banners)
        {
          if (!handledBanners.Contains(b.BannerPath))
          {
            lvSeriesDetails.Items.Add(CreateBannerItem(b.BannerPath, _before, _after, _current));
            handledBanners.Add(b.BannerPath);
          }
        }
      }

      if (_after != null)
      {
        foreach (TvdbBanner b in _after.Banners)
        {
          if (!handledBanners.Contains(b.BannerPath))
          {
            lvSeriesDetails.Items.Add(CreateBannerItem(b.BannerPath, _before, _after, _current));
            handledBanners.Add(b.BannerPath);
          }
        }
      }

      if (_current != null)
      {
        foreach (TvdbBanner b in _current.Banners)
        {
          if (!handledBanners.Contains(b.BannerPath))
          {
            lvSeriesDetails.Items.Add(CreateBannerItem(b.BannerPath, _before, _after, _current));
            handledBanners.Add(b.BannerPath);
          }
        }
      }

      lvSeriesDetails.Items.Add(CreateHeadingItem("", "", "", ""));
      lvSeriesDetails.Items.Add(CreateHeadingItem("---------------------", "Actors:", "-----------------", "-----------------"));

      List<int> handledActors = new List<int>();
      if (_before != null)
      {
        foreach (TvdbActor a in _before.TvdbActors)
        {
          if (!handledActors.Contains(a.Id))
          {
            lvSeriesDetails.Items.Add(CreateActorItem(a.Id, _before, _after, _current));
            handledActors.Add(a.Id);
          }
        }
      }


      //check tag of all items to get overall state of series
      //

      ItemState overallState = ItemState.Unchanged;
      foreach (ListViewItem i in lvSeriesDetails.Items)
      {
        ListViewTag tag = i.Tag as ListViewTag;

        if (overallState == ItemState.Unchanged &&
           tag.State == ItemState.Updated)
        {//updated but no value changed
          overallState = ItemState.Updated;
        }

        if (tag.State == ItemState.UpdatedAndValueChanged || tag.State == ItemState.Added)
        {//updated and everything went fine
          overallState = ItemState.UpdatedAndValueChanged;
        }
        if (tag.State == ItemState.AddedAndInconsistent ||
          tag.State == ItemState.UpdatedAndInconsistent ||
          tag.State == ItemState.UnchangedAndInconsistent ||
          tag.State == ItemState.AvailableButNotDownloaded)
        {//something doesn't add up
          overallState = ItemState.UpdatedAndInconsistent;
          break;//worst case, so break
        }
      }

      return overallState;
    }

    private ListViewItem CreateActorItem(int _id, TvdbSeries _before, TvdbSeries _after, TvdbSeries _current)
    {
      ListViewItem item = new ListViewItem(_id.ToString());

      TvdbActor before = GetActor(_before, _id);
      item.SubItems.Add(before != null ? before.Name + ": " + before.Role : "");

      TvdbActor after = GetActor(_after, _id);
      item.SubItems.Add(after != null ? after.Name + ": " + after.Role : "");

      TvdbActor current = GetActor(_current, _id);
      item.SubItems.Add(current != null ? current.Name + ": " + current.Role : "");


      ListViewTag tag = new ListViewTag();
      tag.Type = ItemType.Actor;//episode
      ItemState epChanged = CheckActorChanged(before, after, current);
      tag.State = epChanged;
      item.Tag = tag;
      SetColorForListViewItem(item, epChanged, ItemType.Actor);

      return item;
    }

    private ItemState CheckActorChanged(TvdbActor _before, TvdbActor _after, TvdbActor _current)
    {
      if (_current != null)
      {
        if (_after != null)
        {//episode has been updated (_before != null) or added (_before == null)
          if (ActorChanged(_after, _current))
          {
            if (_before == null)
            {
              return ItemState.AddedAndInconsistent;
            }
            else
            {
              return ItemState.UpdatedAndInconsistent;
            }
          }
          else
          {
            if (_before == null)
            {//the item has been added correctly
              return ItemState.Added;
            }
            else
            {//the item has been updated correctly (after == current)
              if (ActorChanged(_before, _after))
              {
                return ItemState.UpdatedAndValueChanged;
              }
              else
              {
                return ItemState.Updated;
              }
            }
          }
        }

        //episode is available but hasn't been added to our cache yet
        if (_before == null && _after == null) return ItemState.AvailableButNotDownloaded;

        //we have no update for this item
        if (_after == null)
        {
          if (ActorChanged(_before, _current))
          {
            return ItemState.UnchangedAndInconsistent;
          }
          else
          {
            return ItemState.Unchanged;
          }
        }
      }

      if (_current == null)
      {//current version hasn't been loaded, we can't compare if the update was done correctly
        if (_before != null && _after != null)
        {//episode has been updated
          return ItemState.Updated;
        }
        else if (_before == null && _after != null)
        {//episode has been added
          return ItemState.Added;
        }
        else
        {
          return ItemState.Unchanged;
        }
      }
      return ItemState.Unchanged;
    }

    private bool ActorChanged(TvdbActor _actor1, TvdbActor _actor2)
    {
      if (_actor1 == null && _actor2 == null) return false;
      if (_actor1 == null || _actor2 == null) return true;

      bool actorChanged = false;
      //if (!_actor1.ActorImage.BannerPath.Equals(_actor2.BannerPath)) actorChanged = true;//shouldn't happen
      //if (_banner1.LastUpdated != _banner2.LastUpdated) bannerChanged = true;
      if (_actor1.Id != _actor2.Id) actorChanged = true;
      if (_actor1.Name != _actor2.Name) actorChanged = true;
      if (_actor1.Role != _actor2.Role) actorChanged = true;
      if (_actor1.SortOrder != _actor2.SortOrder) actorChanged = true;


      return actorChanged;
    }

    private TvdbActor GetActor(TvdbSeries _series, int _id)
    {
      if (_series == null || _series.TvdbActors == null) return null;
      foreach (TvdbActor a in _series.TvdbActors)
      {
        if (a.Id == _id) return a;
      }
      return null;
    }

    private ListViewItem CreateBannerItem(String _bannerPath, TvdbSeries _before, TvdbSeries _after, TvdbSeries _current)
    {
      ListViewItem item = new ListViewItem(_bannerPath);

      TvdbBanner before = GetBanner(_before, _bannerPath);
      item.SubItems.Add(before != null ? before.Id + ": " + before.BannerPath : "");

      TvdbBanner after = GetBanner(_after, _bannerPath);
      item.SubItems.Add(after != null ? after.Id + ": " + after.BannerPath : "");

      TvdbBanner current = GetBanner(_current, _bannerPath);
      item.SubItems.Add(current != null ? current.Id + ": " + current.BannerPath : "");


      ListViewTag tag = new ListViewTag();
      tag.Type = ItemType.Banner;//episode
      ItemState epChanged = CheckBannerChanged(before, after, current);
      tag.State = epChanged;
      item.Tag = tag;
      SetColorForListViewItem(item, epChanged, ItemType.Banner);

      return item;
    }

    private ItemState CheckBannerChanged(TvdbBanner _before, TvdbBanner _after, TvdbBanner _current)
    {
      if (_current != null)
      {
        if (_after != null)
        {//episode has been updated (_before != null) or added (_before == null)
          if (BannerChanged(_after, _current))
          {
            if (_before == null)
            {
              return ItemState.AddedAndInconsistent;
            }
            else
            {
              return ItemState.UpdatedAndInconsistent;
            }
          }
          else
          {
            if (_before == null)
            {//the item has been added correctly
              return ItemState.Added;
            }
            else
            {//the item has been updated correctly (after == current)
              if (BannerChanged(_before, _after))
              {
                return ItemState.UpdatedAndValueChanged;
              }
              else
              {
                return ItemState.Updated;
              }
            }
          }
        }

        //episode is available but hasn't been added to our cache yet
        if (_before == null && _after == null) return ItemState.AvailableButNotDownloaded;

        //we have no update for this item
        if (_after == null)
        {
          if (BannerChanged(_before, _current))
          {
            return ItemState.UnchangedAndInconsistent;
          }
          else
          {
            return ItemState.Unchanged;
          }
        }
      }

      if (_current == null)
      {//current version hasn't been loaded, we can't compare if the update was done correctly
        if (_before != null && _after != null)
        {//episode has been updated
          return ItemState.Updated;
        }
        else if (_before == null && _after != null)
        {//episode has been added
          return ItemState.Added;
        }
        else
        {
          return ItemState.Unchanged;
        }
      }
      return ItemState.Unchanged;
    }

    private bool BannerChanged(TvdbBanner _banner1, TvdbBanner _banner2)
    {
      if (_banner1 == null && _banner2 == null) return false;
      if (_banner1 == null || _banner2 == null) return true;
      if (_banner1.GetType() != _banner2.GetType()) return true;

      bool bannerChanged = false;
      if (!_banner1.BannerPath.Equals(_banner2.BannerPath)) bannerChanged = true;//shouldn't happen
      //if (_banner1.LastUpdated != _banner2.LastUpdated) bannerChanged = true;
      if (_banner1.Id != _banner2.Id) bannerChanged = true;

      if (_banner1.GetType() == typeof(TvdbSeriesBanner))
      {
        TvdbSeriesBanner banner1 = _banner1 as TvdbSeriesBanner;
        TvdbSeriesBanner banner2 = _banner2 as TvdbSeriesBanner;

      }

      if (_banner1.GetType() == typeof(TvdbSeasonBanner))
      {
        TvdbSeasonBanner banner1 = _banner1 as TvdbSeasonBanner;
        TvdbSeasonBanner banner2 = _banner2 as TvdbSeasonBanner;
        if (banner1.Season != banner2.Season) bannerChanged = true;
      }

      if (_banner1.GetType() == typeof(TvdbFanartBanner))
      {
        TvdbFanartBanner banner1 = _banner1 as TvdbFanartBanner;
        TvdbFanartBanner banner2 = _banner2 as TvdbFanartBanner;
        if (banner1.Color1 != banner2.Color1) bannerChanged = true;
        if (banner1.Color2 != banner2.Color2) bannerChanged = true;
        if (banner1.Color3 != banner2.Color3) bannerChanged = true;
        if (banner1.ContainsSeriesName != banner2.ContainsSeriesName) bannerChanged = true;
        if (banner1.Resolution != banner2.Resolution) bannerChanged = true;
        if (banner1.VignettePath != banner2.VignettePath) bannerChanged = true;
      }

      if (_banner1.GetType() == typeof(TvdbPosterBanner))
      {
        TvdbPosterBanner banner1 = _banner1 as TvdbPosterBanner;
        TvdbPosterBanner banner2 = _banner2 as TvdbPosterBanner;
        if (banner1.Resolution != banner2.Resolution) bannerChanged = true;
      }
      return bannerChanged;
    }

    private TvdbBanner GetBanner(TvdbSeries _series, string _bannerPath)
    {
      if (_series == null || _series.Banners == null) return null;
      foreach (TvdbBanner b in _series.Banners)
      {
        if (b.BannerPath.Equals(_bannerPath)) return b;
      }
      return null;
    }

    private ListViewItem CreateHeadingItem(String _heading1, String _heading2, String _heading3, String _heading4)
    {
      ListViewItem item = new ListViewItem();
      ListViewTag headingTag = new ListViewTag();
      headingTag.Type = ItemType.Heading;
      item.Tag = headingTag;
      item.Text = _heading1;
      item.SubItems.Add(_heading2);
      item.SubItems.Add(_heading3);
      item.SubItems.Add(_heading4);

      return item;
    }

    private ListViewItem CreateEpisodeItem(int _id, TvdbSeries _before, TvdbSeries _after, TvdbSeries _current)
    {
      ListViewItem item = new ListViewItem(_id.ToString());

      TvdbEpisode before = GetEpisode(_before, _id);
      item.SubItems.Add(before != null ? before.SeasonNumber + "x" + before.EpisodeNumber + " " + before.EpisodeName : "");
      TvdbEpisode after = GetEpisode(_after, _id);
      item.SubItems.Add(after != null ? after.SeasonNumber + "x" + after.EpisodeNumber + " " + after.EpisodeName : "");
      TvdbEpisode current = GetEpisode(_current, _id);
      item.SubItems.Add(current != null ? current.SeasonNumber + "x" + current.EpisodeNumber + " " + current.EpisodeName : "");

      ListViewTag tag = new ListViewTag();
      tag.Type = ItemType.Episode;//episode
      ItemState epChanged = CheckEpisodeChanged(before, after, current);
      tag.State = epChanged;
      item.Tag = tag;
      SetColorForListViewItem(item, epChanged, ItemType.Episode);

      return item;
    }

    private void SetColorForListViewItem(ListViewItem _item, ItemState _state, ItemType _type)
    {

      switch (_state)
      {
        case ItemState.Added:
          _item.BackColor = Color.DarkGreen;
          break;
        case ItemState.UpdatedAndValueChanged:
          _item.BackColor = Color.Green;
          break;
        case ItemState.Updated:
          _item.BackColor = Color.LightGray;
          break;
        case ItemState.Unchanged:
          _item.BackColor = Color.Transparent;
          break;
        case ItemState.AddedAndInconsistent:
          _item.BackColor = Color.DarkRed;
          break;
        case ItemState.UpdatedAndInconsistent:
          _item.BackColor = Color.Red;
          break;
        case ItemState.AvailableButNotDownloaded:
          _item.BackColor = Color.Purple;
          break;
        case ItemState.UnchangedAndInconsistent:
          _item.BackColor = Color.Gray;
          break;
      }
    }

    private ItemState CheckEpisodeChanged(TvdbEpisode _before, TvdbEpisode _after, TvdbEpisode _current)
    {
      if (_current != null)
      {
        if (_after != null)
        {//episode has been updated (_before != null) or added (_before == null)
          if (EpsiodeChanged(_after, _current))
          {
            if (_before == null)
            {
              return ItemState.AddedAndInconsistent;
            }
            else
            {
              return ItemState.UpdatedAndInconsistent;
            }
          }
          else
          {
            if (_before == null)
            {//the item has been added correctly
              return ItemState.Added;
            }
            else
            {//the item has been updated correctly (after == current)
              if (EpsiodeChanged(_before, _after))
              {
                return ItemState.UpdatedAndValueChanged;
              }
              else
              {
                return ItemState.Updated;
              }
            }
          }
        }

        //episode is available but hasn't been added to our cache yet
        if (_before == null && _after == null) return ItemState.AvailableButNotDownloaded;


        //we have no update for this item
        if (_after == null)
        {
          if (EpsiodeChanged(_before, _current))
          {
            return ItemState.UnchangedAndInconsistent;
          }
          else
          {
            return ItemState.Unchanged;
          }
        }
      }


      if (_current == null)
      {//current version hasn't been loaded, we can't compare if the update was done correctly
        if (_before != null && _after != null)
        {//episode has been updated
          return ItemState.Updated;
        }
        else if (_before == null && _after != null)
        {//episode has been added
          return ItemState.Added;
        }
        else
        {
          return ItemState.Unchanged;
        }
      }



      return ItemState.Unchanged;
    }

    /// <summary>
    /// Returns true if episodes are different
    /// </summary>
    /// <param name="_ep1"></param>
    /// <param name="_ep2"></param>
    /// <returns></returns>
    private bool EpsiodeChanged(TvdbEpisode _ep1, TvdbEpisode _ep2)
    {
      bool episodeChanged = false;
      if (!_ep1.AbsoluteNumber.Equals(_ep2.AbsoluteNumber)) episodeChanged = true;
      if (!_ep1.AirsAfterSeason.Equals(_ep2.AirsAfterSeason)) episodeChanged = true;
      if (!_ep1.AirsBeforeEpisode.Equals(_ep2.AirsBeforeEpisode)) episodeChanged = true;
      if (!_ep1.AirsBeforeSeason.Equals(_ep2.AirsBeforeSeason)) episodeChanged = true;
      if (!_ep1.BannerPath.Equals(_ep2.BannerPath)) episodeChanged = true;
      //those two will always be wrong because they're not included in the tvdb xml files of single episodes
      //-> see: http://forums.thetvdb.com/viewtopic.php?f=8&t=3993
      //if (!_ep1.CombinedEpisodeNumber.Equals(_ep2.CombinedEpisodeNumber)) episodeChanged = true; 
      //if (!_ep1.CombinedSeason.Equals(_ep2.CombinedSeason)) episodeChanged = true;
      if (!_ep1.DirectorsString.Equals(_ep2.DirectorsString)) episodeChanged = true;
      if (!_ep1.DvdChapter.Equals(_ep2.DvdChapter)) episodeChanged = true;
      if (!_ep1.DvdDiscId.Equals(_ep2.DvdDiscId)) episodeChanged = true;
      if (!_ep1.DvdEpisodeNumber.Equals(_ep2.DvdEpisodeNumber)) episodeChanged = true;
      if (!_ep1.DvdSeason.Equals(_ep2.DvdSeason)) episodeChanged = true;
      if (!_ep1.EpisodeName.Equals(_ep2.EpisodeName)) episodeChanged = true;
      if (!_ep1.EpisodeNumber.Equals(_ep2.EpisodeNumber)) episodeChanged = true;
      if (!_ep1.FirstAired.Equals(_ep2.FirstAired)) episodeChanged = true;
      if (!_ep1.GuestStarsString.Equals(_ep2.GuestStarsString)) episodeChanged = true;
      if (!_ep1.Id.Equals(_ep2.Id)) episodeChanged = true;
      if (!_ep1.ImdbId.Equals(_ep2.ImdbId)) episodeChanged = true;
      if (!_ep1.IsSpecial.Equals(_ep2.IsSpecial)) episodeChanged = true;
      //if (!_ep1.LastUpdated.Equals(_ep2.LastUpdated)) episodeChanged = true;
      if (!_ep1.Overview.Equals(_ep2.Overview)) episodeChanged = true;
      if (!_ep1.ProductionCode.Equals(_ep2.ProductionCode)) episodeChanged = true;
      //if (!_ep1.Rating.Equals(_ep2.Rating)) episodeChanged = true;
      if (!_ep1.SeasonId.Equals(_ep2.SeasonId)) episodeChanged = true;
      if (!_ep1.SeasonNumber.Equals(_ep2.SeasonNumber)) episodeChanged = true;
      if (!_ep1.SeriesId.Equals(_ep2.SeriesId)) episodeChanged = true;
      if (!_ep1.WriterString.Equals(_ep2.WriterString)) episodeChanged = true;

      return episodeChanged;
    }


    private TvdbEpisode GetEpisode(TvdbSeries _series, int _id)
    {
      if (_series == null || _series.Episodes == null) return null;
      foreach (TvdbEpisode e in _series.Episodes)
      {
        if (e.Id == _id) return e;
      }
      return null;
    }

    private ListViewItem CreateItem(String _object, String _before, String _after, String _current)
    {
      ListViewItem item = new ListViewItem(_object);
      item.SubItems.Add(_before);
      item.SubItems.Add(_after);
      item.SubItems.Add(_current);

      ItemState state = CheckStringChanged(_before, _after, _current);

      ListViewTag tag = new ListViewTag();
      tag.State = state;
      tag.Type = ItemType.SeriesAttribute;
      item.Tag = tag;

      SetColorForListViewItem(item, state, ItemType.SeriesAttribute);

      return item;
    }

    private ItemState CheckStringChanged(String _before, String _after, String _current)
    {
      if (_current != "")
      {
        if (_after != "")
        {//episode has been updated (_before != null) or added (_before == null)
          if (_after != _current)
          {
            if (_before == "")
            {
              return ItemState.AddedAndInconsistent;
            }
            else
            {
              return ItemState.UpdatedAndInconsistent;
            }
          }
          else
          {
            if (_before == "")
            {//the item has been added correctly
              return ItemState.Added;
            }
            else
            {//the item has been updated correctly (after == current)
              if (_before != _after)
              {
                return ItemState.UpdatedAndValueChanged;
              }
              else
              {
                return ItemState.Updated;
              }
            }
          }
        }

        //episode is available but hasn't been added to our cache yet
        if (_before == "" && _after == "") return ItemState.AvailableButNotDownloaded;

        //we have no update for this item
        if (_after == "")
        {
          if (_before != _current)
          {
            return ItemState.UnchangedAndInconsistent;
          }
          else
          {
            return ItemState.Unchanged;
          }
        }
      }

      if (_current == "")
      {//current version hasn't been loaded, we can't compare if the update was done correctly
        if (_before != "" && _after != "")
        {//episode has been updated
          return ItemState.Updated;
        }
        else if (_before == "" && _after != "")
        {//episode has been added
          return ItemState.Added;
        }
        else
        {
          return ItemState.Unchanged;
        }
      }
      return ItemState.Unchanged;
    }

    private void cmdAddAllFavorites_Click(object sender, EventArgs e)
    {
      List<int> favs = m_tvdbHandler.GetUserFavouritesList();
      foreach (int f in favs)
      {
        TvdbSeries s = m_tvdbHandler.GetSeries(f, TvdbLanguage.DefaultLanguage, true, true, true, true);
        if (!ListViewContainsSeries(s.Id))
        {
          ListViewItem item = new ListViewItem(s.ToString());
          item.Tag = s.Id;
          lvCachedSeries.Items.Add(item);
        }
      }
    }

    private bool ListViewContainsSeries(int _id)
    {
      foreach (ListViewItem i in lvCachedSeries.Items)
      {
        if (((int)i.Tag) == _id) return true;
      }
      return false;
    }

    private void cmdUpdate_Click(object sender, EventArgs e)
    {
      Interval updateInterval = Interval.automatic;
      if (rbUpdateAutomatic.Checked)
      {
        updateInterval = Interval.automatic;
      }
      else if (rbUpdateDay.Checked)
      {
        updateInterval = Interval.day;
      }
      else if (rbUpdateWeek.Checked)
      {
        updateInterval = Interval.week;
      }
      else if (rbUpdateMonth.Checked)
      {
        updateInterval = Interval.month;
      }

      //store all series in a list so we won't lose the reference
      foreach (int s in m_tvdbHandler.GetCachedSeries())
      {
        if (GetBefore(s) == null)
        {
          m_beforeUpdateList.Add(m_tvdbHandler.GetSeries(s, TvdbLanguage.DefaultLanguage, true, true, true, true));
        }
      }

      UpdateForm frm = new UpdateForm(m_tvdbHandler, updateInterval, true);
      frm.ShowDialog();


    }

    void m_tvdbHandler_UpdateFinished(TvdbHandler.UpdateFinishedEventArgs _event)
    {
      UpdateFormFinishedThreadSafe(_event);
    }

    delegate void UpdateFormFinishedThreadSafeDelegate(TvdbHandler.UpdateFinishedEventArgs _event);
    void UpdateFormFinishedThreadSafe(TvdbHandler.UpdateFinishedEventArgs _event)
    {
      if (!InvokeRequired)
      {
        m_updatedEpisodes = _event.UpdatedEpisodes;
        m_updatedSeries = _event.UpdatedSeries;

        //store all updated series in a list
        foreach (int s in _event.UpdatedSeries)
        {
          m_afterUpdateList.Add(m_tvdbHandler.GetSeries(s, TvdbLanguage.DefaultLanguage, true, true, true));
        }

        for (int i = 0; i < lvCachedSeries.Items.Count; i++)
        {
          if (_event.UpdatedSeries.Contains((int)lvCachedSeries.Items[i].Tag))
          {
            lvCachedSeries.Items[i].BackColor = Color.Orange;
          }
        }
      }
      else
        Invoke(new UpdateFormFinishedThreadSafeDelegate(UpdateFormFinishedThreadSafe), new object[] { _event });
    }

    private void lvSeriesDetails_DoubleClick(object sender, EventArgs e)
    {
      if (lvSeriesDetails.SelectedItems.Count == 1)
      {//episode selected
        ListViewTag tag = (ListViewTag)lvSeriesDetails.SelectedItems[0].Tag;
        TvdbSeries before = GetBefore((int)lvSeriesDetails.Tag);
        TvdbSeries after = GetAfter((int)lvSeriesDetails.Tag);
        TvdbSeries current = GetCurrent((int)lvSeriesDetails.Tag);

        if (tag.Type == ItemType.Episode)
        {
          int episodeId = Int32.Parse(lvSeriesDetails.SelectedItems[0].Text);
          FillEpisodeDetails(GetEpisode(before, episodeId), GetEpisode(after, episodeId), GetEpisode(current, episodeId));
        }
        else if (tag.Type == ItemType.Banner)
        {
          String bannerPath = lvSeriesDetails.SelectedItems[0].Text;
          FillBannerDetails(GetBanner(before, bannerPath), GetBanner(after, bannerPath), GetBanner(current, bannerPath));
        }
        else if (tag.Type == ItemType.SeriesAttribute || tag.Type == ItemType.EpisodeAttribute)
        {
          txtBefore.Text = lvSeriesDetails.SelectedItems[0].SubItems[1].Text;
          txtAfter.Text = lvSeriesDetails.SelectedItems[0].SubItems[2].Text;
          txtCurrent.Text = lvSeriesDetails.SelectedItems[0].SubItems[3].Text;
        }
        else if (tag.Type == ItemType.Actor)
        {
          int actorId = Int32.Parse(lvSeriesDetails.SelectedItems[0].Text);
          FillActorDetails(GetActor(before, actorId), GetActor(after, actorId), GetActor(current, actorId));
        }

      }
    }



    private void FillBannerDetails(TvdbBanner _before, TvdbBanner _after, TvdbBanner _current)
    {
      lvSeriesDetails.Items.Clear();

      //id
      lvSeriesDetails.Items.Add(CreateItem("Id", _before != null ? _before.Id.ToString() : "",
                                                 _after != null ? _after.Id.ToString() : "",
                                                 _current != null ? _current.Id.ToString() : ""));

      //BannerPath
      lvSeriesDetails.Items.Add(CreateItem("BannerPath", _before != null ? _before.BannerPath.ToString() : "",
                                           _after != null ? _after.BannerPath.ToString() : "",
                                           _current != null ? _current.BannerPath.ToString() : ""));

      //Type
      lvSeriesDetails.Items.Add(CreateItem("Type", _before != null ? _before.GetType().ToString() : "",
                                           _after != null ? _after.GetType().ToString() : "",
                                           _current != null ? _current.GetType().ToString() : ""));

      if ((_before != null && _after != null && _before.GetType() == _after.GetType()) ||
         (_before != null && _current != null && _before.GetType() == _current.GetType()) ||
         (_after != null && _current != null && _after.GetType() == _current.GetType()) ||
         (_before != null && _current != null && _before.GetType() == _current.GetType()) ||
         (_before != null && _after != null && _current != null && _before.GetType() == _current.GetType() && _before.GetType() == _after.GetType()))
      {//all banners have the same type
        if ((_before != null && _before.GetType() == typeof(TvdbSeriesBanner))
            || (_after != null && _after.GetType() == typeof(TvdbSeriesBanner))
            || (_current != null && _current.GetType() == typeof(TvdbSeriesBanner)))
        {//TvdbSeriesBanner

        }

        if ((_before != null && _before.GetType() == typeof(TvdbSeasonBanner))
            || (_after != null && _after.GetType() == typeof(TvdbSeasonBanner))
            || (_current != null && _current.GetType() == typeof(TvdbSeasonBanner)))
        {//TvdbSeasonBanner
          //Season
          lvSeriesDetails.Items.Add(CreateItem("Season", _before != null ? ((TvdbSeasonBanner)_before).Season.ToString() : "",
                                               _after != null ? ((TvdbSeasonBanner)_after).Season.ToString() : "",
                                               _current != null ? ((TvdbSeasonBanner)_current).Season.ToString() : ""));
        }

        if ((_before != null && _before.GetType() == typeof(TvdbFanartBanner))
            || (_after != null && _after.GetType() == typeof(TvdbFanartBanner))
            || (_current != null && _current.GetType() == typeof(TvdbFanartBanner)))
        {//TvdbFanartBanner
          //Color1
          lvSeriesDetails.Items.Add(CreateItem("Color1", _before != null ? ((TvdbFanartBanner)_before).Color1.ToString() : "",
                                               _after != null ? ((TvdbFanartBanner)_after).Color1.ToString() : "",
                                               _current != null ? ((TvdbFanartBanner)_current).Color1.ToString() : ""));
          //Color2
          lvSeriesDetails.Items.Add(CreateItem("Color2", _before != null ? ((TvdbFanartBanner)_before).Color2.ToString() : "",
                                               _after != null ? ((TvdbFanartBanner)_after).Color2.ToString() : "",
                                               _current != null ? ((TvdbFanartBanner)_current).Color2.ToString() : ""));

          //Color3
          lvSeriesDetails.Items.Add(CreateItem("Color3", _before != null ? ((TvdbFanartBanner)_before).Color3.ToString() : "",
                                               _after != null ? ((TvdbFanartBanner)_after).Color3.ToString() : "",
                                               _current != null ? ((TvdbFanartBanner)_current).Color3.ToString() : ""));

          //Resolution
          lvSeriesDetails.Items.Add(CreateItem("Resolution", _before != null ? ((TvdbFanartBanner)_before).Resolution.ToString() : "",
                                               _after != null ? ((TvdbFanartBanner)_after).Resolution.ToString() : "",
                                               _current != null ? ((TvdbFanartBanner)_current).Resolution.ToString() : ""));

          //ContainsSeriesName
          lvSeriesDetails.Items.Add(CreateItem("Resolution", _before != null ? ((TvdbFanartBanner)_before).ContainsSeriesName.ToString() : "",
                                               _after != null ? ((TvdbFanartBanner)_after).ContainsSeriesName.ToString() : "",
                                               _current != null ? ((TvdbFanartBanner)_current).ContainsSeriesName.ToString() : ""));

          //VignettePath
          lvSeriesDetails.Items.Add(CreateItem("VignettePath", _before != null ? ((TvdbFanartBanner)_before).VignettePath.ToString() : "",
                                               _after != null ? ((TvdbFanartBanner)_after).VignettePath.ToString() : "",
                                               _current != null ? ((TvdbFanartBanner)_current).VignettePath.ToString() : ""));



        }

        if ((_before != null && _before.GetType() == typeof(TvdbPosterBanner))
            || (_after != null && _after.GetType() == typeof(TvdbPosterBanner))
            || (_current != null && _current.GetType() == typeof(TvdbPosterBanner)))
        {//TvdbPosterBanner
          //Resolution
          lvSeriesDetails.Items.Add(CreateItem("Resolution", _before != null ? ((TvdbPosterBanner)_before).Resolution.ToString() : "",
                                               _after != null ? ((TvdbPosterBanner)_after).Resolution.ToString() : "",
                                               _current != null ? ((TvdbPosterBanner)_current).Resolution.ToString() : ""));
        }
      }
    }

    private void FillActorDetails(TvdbActor _before, TvdbActor _after, TvdbActor _current)
    {
      lvSeriesDetails.Items.Clear();

      //id
      lvSeriesDetails.Items.Add(CreateItem("Id", _before != null ? _before.Id.ToString() : "",
                                                 _after != null ? _after.Id.ToString() : "",
                                                 _current != null ? _current.Id.ToString() : ""));

      //Name
      lvSeriesDetails.Items.Add(CreateItem("Name", _before != null ? _before.Name.ToString() : "",
                                                 _after != null ? _after.Name.ToString() : "",
                                                 _current != null ? _current.Name.ToString() : ""));

      //Role
      lvSeriesDetails.Items.Add(CreateItem("Role", _before != null ? _before.Role.ToString() : "",
                                                 _after != null ? _after.Role.ToString() : "",
                                                 _current != null ? _current.Role.ToString() : ""));

      //SortOrder
      lvSeriesDetails.Items.Add(CreateItem("SortOrder", _before != null ? _before.SortOrder.ToString() : "",
                                                 _after != null ? _after.SortOrder.ToString() : "",
                                                 _current != null ? _current.SortOrder.ToString() : ""));

      //SortOrder
      lvSeriesDetails.Items.Add(CreateItem("Image", (_before != null && _before.ActorImage != null) ? _before.ActorImage.BannerPath.ToString() : "",
                                                 (_after != null && _after.ActorImage != null) ? _after.ActorImage.BannerPath.ToString() : "",
                                                 (_current != null && _current.ActorImage != null) ? _current.ActorImage.BannerPath.ToString() : ""));
    }

    private void FillEpisodeDetails(TvdbEpisode _before, TvdbEpisode _after, TvdbEpisode _current)
    {
      lvSeriesDetails.Items.Clear();
      //id
      lvSeriesDetails.Items.Add(CreateItem("Id", _before != null ? _before.Id.ToString() : "",
                                                 _after != null ? _after.Id.ToString() : "",
                                                 _current != null ? _current.Id.ToString() : ""));

      //EpisodeNumber
      lvSeriesDetails.Items.Add(CreateItem("EpisodeNumber", _before != null ? _before.EpisodeNumber.ToString() : "",
                                           _after != null ? _after.EpisodeNumber.ToString() : "",
                                           _current != null ? _current.EpisodeNumber.ToString() : ""));


      //EpisodeName
      lvSeriesDetails.Items.Add(CreateItem("EpisodeName", _before != null ? _before.EpisodeName.ToString() : "",
                                           _after != null ? _after.EpisodeName.ToString() : "",
                                           _current != null ? _current.EpisodeName.ToString() : ""));

      //FirstAired
      lvSeriesDetails.Items.Add(CreateItem("FirstAired", _before != null ? _before.FirstAired.ToString() : "",
                                           _after != null ? _after.FirstAired.ToString() : "",
                                           _current != null ? _current.FirstAired.ToString() : ""));

      //GuestStarsString
      lvSeriesDetails.Items.Add(CreateItem("GuestStarsString", _before != null ? _before.GuestStarsString.ToString() : "",
                                           _after != null ? _after.GuestStarsString.ToString() : "",
                                           _current != null ? _current.GuestStarsString.ToString() : ""));

      //DirectorsString
      lvSeriesDetails.Items.Add(CreateItem("DirectorsString", _before != null ? _before.DirectorsString.ToString() : "",
                                           _after != null ? _after.DirectorsString.ToString() : "",
                                           _current != null ? _current.DirectorsString.ToString() : ""));

      //WriterString
      lvSeriesDetails.Items.Add(CreateItem("WriterString", _before != null ? _before.WriterString.ToString() : "",
                                           _after != null ? _after.WriterString.ToString() : "",
                                           _current != null ? _current.WriterString.ToString() : ""));

      //Overview
      lvSeriesDetails.Items.Add(CreateItem("Overview", _before != null ? _before.Overview.ToString() : "",
                                           _after != null ? _after.Overview.ToString() : "",
                                           _current != null ? _current.Overview.ToString() : ""));

      //ProductionCode
      lvSeriesDetails.Items.Add(CreateItem("ProductionCode", _before != null ? _before.ProductionCode.ToString() : "",
                                           _after != null ? _after.ProductionCode.ToString() : "",
                                           _current != null ? _current.ProductionCode.ToString() : ""));

      //language
      lvSeriesDetails.Items.Add(CreateItem("language", _before != null ? _before.Language.ToString() : "",
                                           _after != null ? _after.Language.ToString() : "",
                                           _current != null ? _current.Language.ToString() : ""));

      //rating
      lvSeriesDetails.Items.Add(CreateItem("rating", _before != null ? _before.Rating.ToString() : "",
                                           _after != null ? _after.Rating.ToString() : "",
                                           _current != null ? _current.Rating.ToString() : ""));

      //banner
      lvSeriesDetails.Items.Add(CreateItem("banner", _before != null ? _before.BannerPath.ToString() : "",
                                           _after != null ? _after.BannerPath.ToString() : "",
                                           _current != null ? _current.BannerPath.ToString() : ""));

      //lastupdated
      lvSeriesDetails.Items.Add(CreateItem("lastupdated", _before != null ? _before.LastUpdated.ToString() : "",
                                           _after != null ? _after.LastUpdated.ToString() : "",
                                           _current != null ? _current.LastUpdated.ToString() : ""));

      //////////////////////////////////

      //DvdDiscId
      lvSeriesDetails.Items.Add(CreateItem("DvdDiscId", _before != null ? _before.DvdDiscId.ToString() : "",
                                           _after != null ? _after.DvdDiscId.ToString() : "",
                                           _current != null ? _current.DvdDiscId.ToString() : ""));

      //DvdSeason
      lvSeriesDetails.Items.Add(CreateItem("DvdSeason", _before != null ? _before.DvdSeason.ToString() : "",
                                           _after != null ? _after.DvdSeason.ToString() : "",
                                           _current != null ? _current.DvdSeason.ToString() : ""));

      //DvdEpisodeNumber
      lvSeriesDetails.Items.Add(CreateItem("DvdEpisodeNumber", _before != null ? _before.DvdEpisodeNumber.ToString() : "",
                                           _after != null ? _after.DvdEpisodeNumber.ToString() : "",
                                           _current != null ? _current.DvdEpisodeNumber.ToString() : ""));

      //DvdChapter
      lvSeriesDetails.Items.Add(CreateItem("DvdChapter", _before != null ? _before.DvdChapter.ToString() : "",
                                           _after != null ? _after.DvdChapter.ToString() : "",
                                           _current != null ? _current.DvdChapter.ToString() : ""));

      //AbsoluteNumber
      lvSeriesDetails.Items.Add(CreateItem("AbsoluteNumber", _before != null ? _before.AbsoluteNumber.ToString() : "",
                                           _after != null ? _after.AbsoluteNumber.ToString() : "",
                                           _current != null ? _current.AbsoluteNumber.ToString() : ""));

      //CombinedEpisodeNumber
      lvSeriesDetails.Items.Add(CreateItem("CombinedEpisodeNumber", _before != null ? _before.CombinedEpisodeNumber.ToString() : "",
                                           _after != null ? _after.CombinedEpisodeNumber.ToString() : "",
                                           _current != null ? _current.CombinedEpisodeNumber.ToString() : ""));

      //CombinedSeason
      lvSeriesDetails.Items.Add(CreateItem("CombinedSeason", _before != null ? _before.CombinedSeason.ToString() : "",
                                           _after != null ? _after.CombinedSeason.ToString() : "",
                                           _current != null ? _current.CombinedSeason.ToString() : ""));

      //AbsoluteNumber
      lvSeriesDetails.Items.Add(CreateItem("AbsoluteNumber", _before != null ? _before.AbsoluteNumber.ToString() : "",
                                           _after != null ? _after.AbsoluteNumber.ToString() : "",
                                           _current != null ? _current.AbsoluteNumber.ToString() : ""));

      //SeriesId
      lvSeriesDetails.Items.Add(CreateItem("SeriesId", _before != null ? _before.SeriesId.ToString() : "",
                                           _after != null ? _after.SeriesId.ToString() : "",
                                           _current != null ? _current.SeriesId.ToString() : ""));

      //ImdbId
      lvSeriesDetails.Items.Add(CreateItem("ImdbId", _before != null ? _before.ImdbId.ToString() : "",
                                           _after != null ? _after.ImdbId.ToString() : "",
                                           _current != null ? _current.ImdbId.ToString() : ""));

      //SeasonNumber
      lvSeriesDetails.Items.Add(CreateItem("SeasonNumber", _before != null ? _before.SeasonNumber.ToString() : "",
                                           _after != null ? _after.SeasonNumber.ToString() : "",
                                           _current != null ? _current.SeasonNumber.ToString() : ""));


      //AirsAfterSeason
      lvSeriesDetails.Items.Add(CreateItem("AirsAfterSeason", _before != null ? _before.AirsAfterSeason.ToString() : "",
                                           _after != null ? _after.AirsAfterSeason.ToString() : "",
                                           _current != null ? _current.AirsAfterSeason.ToString() : ""));

      //AirsBeforeSeason
      lvSeriesDetails.Items.Add(CreateItem("AirsBeforeSeason", _before != null ? _before.AirsBeforeSeason.ToString() : "",
                                           _after != null ? _after.AirsBeforeSeason.ToString() : "",
                                           _current != null ? _current.AirsBeforeSeason.ToString() : ""));

      //AirsBeforeEpisode
      lvSeriesDetails.Items.Add(CreateItem("AirsBeforeEpisode", _before != null ? _before.AirsBeforeEpisode.ToString() : "",
                                           _after != null ? _after.AirsBeforeEpisode.ToString() : "",
                                           _current != null ? _current.AirsBeforeEpisode.ToString() : ""));
    }

    private TvdbSeries GetBefore(int _seriesId)
    {
      if (m_beforeUpdateList == null) return null;
      foreach (TvdbSeries s in m_beforeUpdateList)
      {
        if (s.Id == _seriesId) return s;
      }
      return null;
    }

    private TvdbSeries GetAfter(int _seriesId)
    {
      if (m_afterUpdateList == null) return null;
      foreach (TvdbSeries s in m_afterUpdateList)
      {
        if (s.Id == _seriesId) return s;
      }
      return null;
    }

    private TvdbSeries GetCurrent(int _seriesId)
    {
      if (m_currentVersionList == null) return null;
      foreach (TvdbSeries s in m_currentVersionList)
      {
        if (s.Id == _seriesId) return s;
      }
      return null;
    }

    private void cmdBack_Click(object sender, EventArgs e)
    {
      int seriesId = (int)lvSeriesDetails.Tag;
      FillSeriesDetails(GetBefore(seriesId), GetAfter(seriesId), GetCurrent(seriesId));
    }

    private void cmdComareValues_Click(object sender, EventArgs e)
    {

    }

    private void cmdCompareValuesDetailed_Click(object sender, EventArgs e)
    {

    }

    private void cmdMakeSnapshotOfCache_Click(object sender, EventArgs e)
    {
      RevisionForm input = new RevisionForm();
      input.ReadOnly = false;
      if (input.ShowDialog() == DialogResult.OK)
      {
        CacheRevision rev = new CacheRevision();
        rev.Date = DateTime.Now;
        rev.Name = input.InputName;
        rev.Description = input.InputDescription;

        FastZip zip = new FastZip();
        zip.CreateZip(rev.CreateFileName(), txtCacheLocation.Text, true, "");
        rev.Data = zip;

        File.WriteAllText(rev.CreateFileName() + ".desc.txt", rev.Description);

        lbCacheSnapshots.Items.Add(rev);

      }
    }

    private void lbCacheSnapshots_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      CacheRevision rev = (CacheRevision)lbCacheSnapshots.SelectedItem;
      RevisionForm form = new RevisionForm();
      form.ReadOnly = true;
      form.InputName = rev.Name;
      form.InputDescription = rev.Description;
      form.Show();
    }

    private void deleteRevisionToolStripMenuItem_Click(object sender, EventArgs e)
    {
      CacheRevision rev = (CacheRevision)lbCacheSnapshots.SelectedItem;

      if (MessageBox.Show("Really delete Revision " + rev.Name + " from " + rev.Date.ToShortDateString() + "?",
                         "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
      {
        File.Delete(rev.CreateFileName());
        File.Delete(rev.CreateFileName() + ".desc.txt");
        lbCacheSnapshots.Items.Remove(rev);
      }

    }

    private void revertToThisRevisionToolStripMenuItem_Click(object sender, EventArgs e)
    {
      CacheRevision rev = (CacheRevision)lbCacheSnapshots.SelectedItem;

      if (MessageBox.Show("Really revert to Revision " + rev.Name + " from " + rev.Date.ToShortDateString() + "?",
                         "Revert", MessageBoxButtons.YesNo) == DialogResult.Yes)
      {
        m_tvdbHandler.CloseCache();
        m_tvdbHandler = null;
        m_beforeUpdateList.Clear();

        if (Directory.Exists(txtCacheLocation.Text))
        {
          Directory.Delete(txtCacheLocation.Text, true);
        }

        FastZip zip = new FastZip();
        zip.ExtractZip(rev.CreateFileName(), txtCacheLocation.Text, "");

        InitTvdblibHandler();
      }
    }

    private void cmCacheRevisions_Opening(object sender, CancelEventArgs e)
    {
      if (lbCacheSnapshots.SelectedItems != null && lbCacheSnapshots.SelectedItems.Count == 2)
      {
        compareBetweenTheseVersionsToolStripMenuItem.Enabled = true;
      }
      else
      {
        compareBetweenTheseVersionsToolStripMenuItem.Enabled = false;
      }
    }


    #region Find Updates
    private void updatesmonthxmlToolStripMenuItem_Click(object sender, EventArgs args)
    {
      if (lvSeriesDetails.SelectedItems != null && lvSeriesDetails.SelectedItems.Count == 1)
      {
        ListViewTag tag = lvSeriesDetails.SelectedItems[0].Tag as ListViewTag;

        if (tag.Type == ItemType.Episode)
        {
          int episodeId = Int32.Parse(lvSeriesDetails.SelectedItems[0].Text);
          //FillEpisodeDetails(GetEpisode(before, episodeId), GetEpisode(after, episodeId), GetEpisode(current, episodeId));


          FindEpisodeInUpdates(episodeId, Interval.month);
        }
      }
    }

    private void updatesweekxmlToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (lvSeriesDetails.SelectedItems != null && lvSeriesDetails.SelectedItems.Count == 1)
      {
        ListViewTag tag = lvSeriesDetails.SelectedItems[0].Tag as ListViewTag;

        if (tag.Type == ItemType.Episode)
        {
          int episodeId = Int32.Parse(lvSeriesDetails.SelectedItems[0].Text);
          //FillEpisodeDetails(GetEpisode(before, episodeId), GetEpisode(after, episodeId), GetEpisode(current, episodeId));


          FindEpisodeInUpdates(episodeId, Interval.week);
        }
      }
    }

    private void updatesdayxmlToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (lvSeriesDetails.SelectedItems != null && lvSeriesDetails.SelectedItems.Count == 1)
      {
        ListViewTag tag = lvSeriesDetails.SelectedItems[0].Tag as ListViewTag;

        if (tag.Type == ItemType.Episode)
        {
          int episodeId = Int32.Parse(lvSeriesDetails.SelectedItems[0].Text);
          //FillEpisodeDetails(GetEpisode(before, episodeId), GetEpisode(after, episodeId), GetEpisode(current, episodeId));


          FindEpisodeInUpdates(episodeId, Interval.day);
        }
      }
    }

    private void FindEpisodeInUpdates(int _episodeId, Interval _interval)
    {


      bool episodeFound = false;
      TvdbEpisode episode = null;
      List<TvdbEpisode> episodes = null;
      switch (_interval)
      {
        case Interval.month:
          episodes = m_updateEpisodesMonth;
          break;
        case Interval.week:
          episodes = m_updateEpisodesWeek;
          break;
        case Interval.day:
          episodes = m_updateEpisodesDay;
          break;
      }

      foreach (TvdbEpisode e in episodes)
      {
        if (_episodeId == e.Id)
        {
          episodeFound = true;
          episode = e;
        }
      }

      if (episodeFound)
      {
        MessageBox.Show("Found episode " + _episodeId + " in updates (Series=" + episode.SeriesId + ", ID=" + episode.Id + ", LastUpdated=" + episode.LastUpdated.ToShortDateString());
      }
      else
      {
        MessageBox.Show("updates.xml doesn't contain episode id " + _episodeId);

      }
    }
    #endregion

    private void saveToFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (lvSeriesDetails.SelectedItems != null && lvSeriesDetails.SelectedItems.Count >= 1)
      {


        int seriesId = (int)lvCachedSeries.SelectedItems[0].Tag;
        TvdbSeries series = GetCurrent(seriesId);

        foreach (ListViewItem i in lvSeriesDetails.SelectedItems)
        {
          ListViewTag tag = i.Tag as ListViewTag;
          if (tag.Type == ItemType.Episode)
          {
            int episodeId = Int32.Parse(i.Text);
            SaveMissingEpToFile(series, episodeId);
          }
        }
      }
    }

    private void SaveMissingEpToFile(TvdbSeries series, int episodeId)
    {
      //FillEpisodeDetails(GetEpisode(before, episodeId), GetEpisode(after, episodeId), GetEpisode(current, episodeId));
      TvdbEpisode episode = GetEpisode(series, episodeId);
      TvdbEpisode currentEp = m_tvdbDownloader.DownloadEpisode(episodeId, episode.Language);
      File.AppendAllText("missing_eps.txt", "Episode " + episodeId + ": " + series.SeriesName + " (" + series.Id + ") " + episode.SeasonNumber +
                         "x" + episode.EpisodeNumber + " LastUpdated: " + currentEp.LastUpdated + "\r\n");
    }

    private void saveListOfSeriesToFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (File.Exists("series_list.txt")) File.Delete("series_list.txt");
      foreach (ListViewItem i in lvCachedSeries.Items)
      {
        int seriesId = (int)i.Tag;
        String seriesName = LookupName(i);
        File.AppendAllText("series_list.txt" ,seriesName + " (" + seriesId + ")\r\n");
      }
    }

    private String LookupName(ListViewItem _item)
    {
      int seriesId = (int)_item.Tag;
      if (_item.SubItems[1].Text.Equals(""))
      {
        TvdbSeries series = m_tvdbHandler.GetSeries(seriesId, TvdbLanguage.DefaultLanguage, false, false, false);
        _item.SubItems[1].Text = series.SeriesName;
      }
      return _item.SubItems[1].Text;
    }

    private void lookUpNamesToolStripMenuItem_Click(object sender, EventArgs e)
    {
      foreach (ListViewItem i in lvCachedSeries.Items)
      {
        LookupName(i);
      }
    }

    //update all flagged series
    List<TvdbSeries> m_updateSeriesMonth;
    List<TvdbEpisode> m_updateEpisodesMonth;
    List<TvdbBanner> m_updateBannersMonth;

    List<TvdbSeries> m_updateSeriesWeek;
    List<TvdbEpisode> m_updateEpisodesWeek;
    List<TvdbBanner> m_updateBannersWeek;

    List<TvdbSeries> m_updateSeriesDay;
    List<TvdbEpisode> m_updateEpisodesDay;
    List<TvdbBanner> m_updateBannersDay;

    private void monthToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        DateTime updateTime = m_tvdbDownloader.DownloadUpdate(out m_updateSeriesMonth, out m_updateEpisodesMonth, 
                                                              out m_updateBannersMonth, Interval.month, true);
        updatesmonthxmlToolStripMenuItem.Enabled = true;
      }
      catch (Exception ex)
      {
        MessageBox.Show("Couldn't download updates");
      }

    }

    private void weekToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        DateTime updateTime = m_tvdbDownloader.DownloadUpdate(out m_updateSeriesWeek, out m_updateEpisodesWeek,
                                                              out m_updateBannersWeek, Interval.week, true);
        updatesweekxmlToolStripMenuItem.Enabled = true;
      }
      catch (Exception ex)
      {
        MessageBox.Show("Couldn't download updates");
      }
    }

    private void dayToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        DateTime updateTime = m_tvdbDownloader.DownloadUpdate(out m_updateSeriesDay, out m_updateEpisodesDay,
                                                              out m_updateBannersDay, Interval.day, true);
        updatesdayxmlToolStripMenuItem.Enabled = true;
      }
      catch (Exception ex)
      {
        MessageBox.Show("Couldn't download updates");
      }
    }

  }
}
