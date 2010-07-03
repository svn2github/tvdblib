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

namespace WikiCodeExamples
{
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
      ICacheProvider provider;
      if (cbCacheType.SelectedIndex == 0 || cbCacheType.SelectedIndex == -1)
      {
        provider = new XmlCacheProvider(txtCacheLocation.Text);
      }
      else
      {
        provider = new BinaryCacheProvider(txtCacheLocation.Text);
      }
      m_tvdbHandler = new TvdbHandler(provider,  File.ReadAllText("api_key.txt"));
      m_tvdbHandler.UserInfo = new TvdbLib.Data.TvdbUser("DieBagger", txtUserId.Text);
      m_tvdbHandler.InitCache();
      txtLastUpdated.Text = m_tvdbHandler.GetLastUpdate().ToString();

      List<int> cached = m_tvdbHandler.GetCachedSeries();
      lvCachedSeries.Items.Clear();
      cached.ForEach(delegate(int s)
      {
        ListViewItem item = new ListViewItem(s.ToString());
        item.Tag = s;
        lvCachedSeries.Items.Add(item);
      });
      m_tvdbDownloader = new TvdbDownloader(txtApiKey.Text);
      m_tvdbHandler.UpdateFinished += new TvdbHandler.UpdateFinishedDelegate(m_tvdbHandler_UpdateFinished);
    }

    private void lvCachedSeries_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (lvCachedSeries.SelectedItems.Count == 1)
      {
        TvdbSeries before = GetBefore((int)lvCachedSeries.SelectedItems[0].Tag);
        TvdbSeries after = GetAfter((int)lvCachedSeries.SelectedItems[0].Tag);

        if (before == null)
        {
          before = m_tvdbHandler.GetSeries((int)lvCachedSeries.SelectedItems[0].Tag, TvdbLanguage.DefaultLanguage, true, true, true);
          if (!m_beforeUpdateList.Contains(before)) m_beforeUpdateList.Add(before);
        }
        FillSeriesDetails(before, after, null);
      }
    }

    private void FillSeriesDetails(TvdbSeries _before, TvdbSeries _after, TvdbSeries _current)
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
          }
        }
      }
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
      item.Tag = 1;//episode
      if (CheckEpisodeChanged(before, after, current))
      {
        item.BackColor = Color.Orange;
      }
      return item;
    }

    private bool CheckEpisodeChanged(TvdbEpisode _before, TvdbEpisode _after, TvdbEpisode _current)
    {
      if (_before != null && _after != null)
      {
        if (m_updatedEpisodes.Contains(_before.Id))
        {
          return true;
        }
      }
      return false;
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
      if (!_before.Equals("") && !_after.Equals("") && !_before.Equals(_after))
      {
        item.BackColor = Color.Orange;
      }
      
      item.Tag = 0; //series
      return item;
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
          item.Tag = s;
          lvCachedSeries.Items.Add(item);
        }
      }
    }

    private bool ListViewContainsSeries(int _id)
    {
      foreach (ListViewItem i in lvCachedSeries.Items)
      {
        if ((int)i.Tag == _id) return true;
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
          m_beforeUpdateList.Add(m_tvdbHandler.GetSeries(s, TvdbLanguage.DefaultLanguage, true, true, true));
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
      if (lvSeriesDetails.SelectedItems.Count == 1 && (int)lvSeriesDetails.SelectedItems[0].Tag == 1)
      {//episode selected
        int episodeId = Int32.Parse(lvSeriesDetails.SelectedItems[0].Text);
        TvdbSeries before = GetBefore((int)lvSeriesDetails.Tag);
        TvdbSeries after = GetAfter((int)lvSeriesDetails.Tag);
        TvdbSeries current = GetAfter((int)lvSeriesDetails.Tag);

        FillEpisodeDetails(GetEpisode(before, episodeId), GetEpisode(after, episodeId), GetEpisode(current, episodeId));
      }
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


  }
}
