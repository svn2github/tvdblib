using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Net;
using TvdbConnector;
using TvdbConnector.Cache;
using TvdbConnector.Data;
using TvdbConnector.Data.Banner;
using TvdbTester.Properties;

namespace TvdbTester
{
  public partial class SeriesBrowser : Form
  {
    Tvdb m_tvdbHandler;
    TvdbLanguage m_defaultLang;
    TvdbSeries m_currentSeries;
    public SeriesBrowser()
    {
      InitializeComponent();
      this.MouseWheel += new MouseEventHandler(Tester_MouseWheel);

    }

    #region Form events
    private void Tester_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Form is shown (after it's loaded)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Tester_Shown(object sender, EventArgs e)
    {
      if (Resources.API_KEY != null && !Resources.API_KEY.Equals(""))
      {
        StartScreen screen = new StartScreen();
        screen.StartPosition = FormStartPosition.Manual;
        screen.Left = (this.Left) + (this.Width / 2) - (screen.Width / 2);
        screen.Top = (this.Top) + (this.Height / 2) - (screen.Height / 2);
        DialogResult res = screen.ShowDialog();
        if (res == DialogResult.Cancel)
        {
          InitialiseForm(null);
        }
        else
        {
          InitialiseForm(screen.UserIdentifier);
        }
      }
      else
      {
        MessageBox.Show("Please insert your api key into the project's Resources");
        panelSeriesOverview.Enabled = false;
        tabControlTvdb.Enabled = false;
      }
    }

    /// <summary>
    /// Initialise the form
    /// </summary>
    /// <param name="_userId"></param>
    public void InitialiseForm(String _userId)
    {
      //m_tvdbHandler = new Tvdb(new BinaryCacheProvider(@"cachefile.bin"), Resources.API_KEY);
      m_tvdbHandler = new Tvdb(new XmlCacheProvider("Cache"), Resources.API_KEY);
      m_tvdbHandler.InitCache();


      List<TvdbLanguage> m_languages = m_tvdbHandler.Languages;

      foreach (TvdbLanguage l in m_languages)
      {
        if (l.Abbriviation.Equals("en")) m_defaultLang = l;
        cbLanguage.Items.Add(l);
      }
      lblCurrentLanguage.Text = "[" + m_defaultLang.ToString() + "]";

      //enable/disable community features
      if (_userId != null)
      {
        TvdbUser user = new TvdbUser("user", _userId);
        m_tvdbHandler.UserInfo = user;
        user.UserPreferredLanguage = m_tvdbHandler.GetPreferredLanguage();
        List<TvdbSeries> favList = m_tvdbHandler.GetUserFavorites(user.UserPreferredLanguage);
        foreach (TvdbSeries s in favList)
        {
          cbUserFavourites.Items.Add(s);
        }
      }
      else
      {
        cbUserFavourites.Text = "No userinfo set";
        cbUserFavourites.Enabled = false;
      }
      cmdAddRemoveFavorites.Enabled = false;
      cmdSendSeriesRating.Enabled = false;
      raterSeriesYourRating.Enabled = false;
    }

    private void Tester_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (m_tvdbHandler != null) m_tvdbHandler.SaveCache();
    }

    void Tester_MouseWheel(object sender, MouseEventArgs e)
    {

      if (coverFlowFanart.Visible)
      {
        Console.WriteLine("[" + e.Delta + "]");
        if (e.Delta > 0)
        {
          coverFlowFanart.SetNext();
        }
        else
        {
          coverFlowFanart.SetPrevious();
        }
      }
    }
    #endregion

    private void cmdGetSeries_Click(object sender, EventArgs args)
    {
      try
      {
        LoadSeries(Int32.Parse(txtGetSeries.Text));
      }
      catch (FormatException)
      {
        MessageBox.Show("Please enter a valid series id");
      }
    }

    private void LoadSeries(int _seriesId)
    {
      CleanUpForm();

      TvdbSeries series = m_tvdbHandler.GetSeries(_seriesId, m_defaultLang, cbLoadFull.Checked, 
                                                  cbLoadActors.Checked, cbLoadBanner.Checked);

      if (series != null)
      {
        UpdateSeries(series);
      }
    }

    private void UpdateSeries(TvdbSeries _series)
    {
      m_currentSeries = _series;
      FillSeriesDetails(_series);

      if (_series.BannersLoaded)
      {
        cmdLoadBanners.Enabled = false;
        pnlFanartEnabled.Visible = false;

        if (_series.PosterBanners.Count > 0)
        {
          posterControlSeries.PosterImages = _series.PosterBanners;
        }
        else
        {
          posterControlSeries.ClearPoster();
        }
      }
      else
      {
        cmdLoadBanners.Enabled = true;
        pnlFanartEnabled.Visible = true;
      }

      if (_series.EpisodesLoaded)
      {
        cmdLoadFullSeriesInfo.Enabled = false;
        pnlEpisodeEnabled.Visible = false;
        FillFullSeriesDetails(_series);
      }
      else
      {
        cmdLoadFullSeriesInfo.Enabled = true;
        pnlEpisodeEnabled.Visible = true;
      }

      if (_series.TvdbActorsLoaded)
      {
        cmdLoadActorInfo.Enabled = false;
        pnlActorsEnabled.Visible = false;
        if (_series.TvdbActors.Count > 0)
        {
          List<TvdbBanner> bannerList = new List<TvdbBanner>();
          foreach (TvdbActor a in _series.TvdbActors)
          {
            bannerList.Add(a.ActorImage);
          }

          bcActors.BannerImages = bannerList;
          SetActorInfo(_series.TvdbActors[0]);
        }
      }
      else
      {
        cmdLoadActorInfo.Enabled = true;
        pnlActorsEnabled.Visible = true;
      }

    }



    private void CleanUpForm()
    {
      posterControlSeries.ClearPoster();
      coverFlowFanart.Clear();
      bcSeriesBanner.ClearBanner();
      bcSeasonBanner.ClearBanner();
      bcSeasonBannerWide.ClearBanner();
      bcEpisodeBanner.ClearBanner();

      txtEpisodeAbsoluteNumber.Text = "";
      txtEpisodeDirector.Text = "";
      txtEpisodeDVDChapter.Text = "";
      txtEpisodeDVDId.Text = "";
      txtEpisodeDVDNumber.Text = "";
      txtEpisodeDVDSeason.Text = "";
      txtEpisodeFirstAired.Text = "";
      lbGuestStars.Items.Clear();
      txtEpisodeImdbID.Text = "";
      txtEpisodeLanguage.Text = "";
      lbEpisodeInformation.Text = "Episode Info";
      txtEpisodeOverview.Text = "";
      txtEpisodeProductionCode.Text = "";
      txtEpisodeWriter.Text = "";
      tvEpisodes.Nodes.Clear();
    }

    private void FillSeriesDetails(TvdbSeries series)
    {
      if (m_tvdbHandler.UserInfo != null)
      {
        cmdAddRemoveFavorites.Enabled = true;
        cmdAddRemoveFavorites.Text = series.IsFavorite ? "Remove from favorites" : "Add to favorites";
        cmdSendSeriesRating.Enabled = true;
        raterSeriesYourRating.Enabled = true;
      }
      else
      {
        cmdAddRemoveFavorites.Enabled = false;
        cmdSendSeriesRating.Enabled = false;
        raterSeriesYourRating.Enabled = false;
      }

      List<TvdbBanner> bannerlist = new List<TvdbBanner>();
      foreach (TvdbBanner b in series.Banners)
      {
        if (b.GetType() == typeof(TvdbSeriesBanner))
        {
          //if (b.Language.Id == m_defaultLang.Id)
          {
            bannerlist.Add(b);
          }
        }
      }
      if (bannerlist.Count > 0)
      {
        bcSeriesBanner.BannerImages = bannerlist;
      }
      else
      {
        bcSeriesBanner.ClearBanner();
      }

      txtSeriesId.Text = series.Id.ToString();
      txtSeriesName.Text = series.SeriesName;
      txtStatus.Text = series.Status;
      txtGenre.Text = series.GenreString;
      txtFirstAired.Text = series.FirstAired.ToShortDateString();
      txtAirsWeekday.Text = series.AirsDayOfWeek.ToString();
      txtAirstime.Text = series.AirsTime.ToShortTimeString();
      txtNetwork.Text = series.Network;
      txtRuntime.Text = series.Runtime.ToString();
      txtRating.Text = series.Rating.ToString();
      txtActors.Text = series.ActorsString;
      txtOverview.Text = series.Overview;
      txtTvComId.Text = series.TvDotComId.ToString(); //series.
      txtImdbId.Text = series.ImdbId;
      txtZap2itId.Text = series.Zap2itId;
      raterSeriesSiteRating.CurrentRating = (int)(series.Rating / 10);
    }

    private void FillFullSeriesDetails(TvdbSeries _series)
    {
      List<TvdbSeasonBanner> seasonBannerList = _series.SeasonBanners;
      tvEpisodes.Nodes.Clear();
      foreach (TvdbEpisode e in _series.Episodes)
      {
        bool found = false;
        foreach (TreeNode seasonNode in tvEpisodes.Nodes)
        {
          if (((SeasonTag)seasonNode.Tag).SeasonId == e.SeasonId)
          {
            TreeNode node = new TreeNode(e.EpisodeNumber + " - " + e.EpisodeName);
            node.Tag = e;
            seasonNode.Nodes.Add(node);
            found = true;
            break;
          }
        }
        if (!found)
        {
          TreeNode node = new TreeNode("Season " + e.SeasonNumber);

          List<TvdbSeasonBanner> tagList = new List<TvdbSeasonBanner>();
          foreach (TvdbSeasonBanner b in seasonBannerList)
          {
            if (b.Season == e.SeasonNumber) tagList.Add(b);
          }
          SeasonTag tag = new SeasonTag(e.SeasonNumber, e.SeasonId, tagList);
          node.Tag = tag;
          tvEpisodes.Nodes.Add(node);

          TreeNode epNode = new TreeNode(e.EpisodeNumber + " - " + e.EpisodeName);
          epNode.Tag = e;
          node.Nodes.Add(epNode);
        }
      }
    }

    private void FillEpisodeDetail(TvdbEpisode _episode)
    {
      lbEpisodeInformation.Text = _episode.EpisodeName + "(" + _episode.EpisodeNumber + ")";
      txtEpisodeLanguage.Text = _episode.Language != null ? _episode.Language.ToString() : "";
      txtEpisodeFirstAired.Text = _episode.FirstAired.ToShortDateString();
      foreach (String s in _episode.GuestStars)
      {
        lbGuestStars.Items.Add(s);
      }
      txtEpisodeDirector.Text = _episode.DirectorsString;
      txtEpisodeWriter.Text = _episode.WriterString;
      txtEpisodeProductionCode.Text = _episode.ProductionCode;
      txtEpisodeOverview.Text = _episode.Overview;
      txtEpisodeDVDId.Text = _episode.DvdDiscId.ToString();
      txtEpisodeDVDSeason.Text = _episode.DvdSeason.ToString();
      txtEpisodeDVDNumber.Text = _episode.DvdEpisodeNumber.ToString();
      txtEpisodeDVDChapter.Text = _episode.DvdChapter.ToString();
      txtEpisodeAbsoluteNumber.Text = _episode.AbsoluteNumber.ToString();
      txtImdbId.Text = _episode.ImdbId;

    }

    private void tvEpisodes_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
    {

    }

    private void tvEpisodes_AfterSelect(object sender, TreeViewEventArgs e)
    {
      if (tvEpisodes.SelectedNode != null && tvEpisodes.SelectedNode.Tag != null)
      {
        int selectedSeason = 0;
        if (tvEpisodes.SelectedNode.Tag.GetType() == typeof(TvdbEpisode))
        {//episode node
          TvdbEpisode ep = (TvdbEpisode)tvEpisodes.SelectedNode.Tag;
          FillEpisodeDetail(ep);

          //load episode image
          if (ep.Banner != null)
          {
            bcEpisodeBanner.BannerImage = ep.Banner;
          }
          else
          {
            bcEpisodeBanner.ClearBanner();
          }
          selectedSeason = ep.SeasonNumber;
        }
        else if (tvEpisodes.SelectedNode.Tag.GetType() == typeof(SeasonTag))
        {//season node
          SeasonTag tag = (SeasonTag)tvEpisodes.SelectedNode.Tag;
          selectedSeason = tag.SeasonNumber;
        }
        else
        {//shouldn't happen at all
          return;
        }

        if (bcSeasonBanner.Tag == null || selectedSeason != (int)bcSeasonBanner.Tag)
        {
          List<TvdbBanner> seasonList = new List<TvdbBanner>();
          List<TvdbBanner> seasonWideList = new List<TvdbBanner>();

          if (m_currentSeries.SeasonBanners != null && m_currentSeries.SeasonBanners.Count > 0)
          {
            foreach (TvdbSeasonBanner b in m_currentSeries.SeasonBanners)
            {
              if (b.Season == selectedSeason)
              {
                if (b.BannerType == TvdbSeasonBanner.Type.season)
                {
                  if (b.Language == null || b.Language.Abbriviation.Equals(m_defaultLang.Abbriviation))
                  {
                    seasonList.Add(b);
                  }
                }

                if (b.BannerType == TvdbSeasonBanner.Type.seasonwide)
                {
                  if (b.Language == null || b.Language.Abbriviation.Equals(m_defaultLang.Abbriviation))
                  {
                    seasonWideList.Add(b);
                  }
                }
              }
            }
          }
          if (seasonList.Count > 0)
          {
            bcSeasonBanner.BannerImages = seasonList;
          }
          else
          {
            bcSeasonBanner.ClearBanner();
          }
          bcSeasonBanner.Tag = selectedSeason;

          if (seasonWideList.Count > 0)
          {
            bcSeasonBannerWide.BannerImages = seasonWideList;
          }
          else
          {
            bcSeasonBannerWide.ClearBanner();
          }

          bcSeasonBannerWide.Tag = selectedSeason;
        }
      }
    }

    private void txtEpisodeNumber_TextChanged(object sender, EventArgs e)
    {

    }



    private void cmdFindSeries_Click(object sender, EventArgs e)
    {
      List<TvdbSearchResult> list = m_tvdbHandler.SearchSeries(txtSeriesToFind.Text);
      SearchResultForm form = new SearchResultForm(list);
      form.StartPosition = FormStartPosition.Manual;
      form.Left = this.Left + this.Width / 2 - form.Width / 2;
      form.Top = this.Top + this.Height / 2 - form.Height / 2;
      DialogResult res = form.ShowDialog();
      if (res == DialogResult.OK)
      {
        LoadSeries(form.Selection.Id);
      }
    }

    private void cbLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (cbLanguage.SelectedItem != null && cbLanguage.SelectedItem.GetType() == typeof(TvdbLanguage))
      {
        lblCurrentLanguage.Text = "[" + ((TvdbLanguage)cbLanguage.SelectedItem).ToString() + "]";
        m_defaultLang = (TvdbLanguage)cbLanguage.SelectedItem;
      }
    }

    private void updateToolStripMenuItem_Click(object sender, EventArgs e)
    {
      m_tvdbHandler.UpdateAllSeries();
    }

    private void cmdForceUpdate_Click(object sender, EventArgs e)
    {
      m_tvdbHandler.ForceUpdate(m_currentSeries);
      UpdateSeries(m_currentSeries);
    }

    private void cmdLoadFullSeriesInfo_Click(object sender, EventArgs e)
    {
      TvdbSeries series = m_tvdbHandler.GetSeries(m_currentSeries.Id, m_currentSeries.Language, true, 
                                                  m_currentSeries.TvdbActorsLoaded, m_currentSeries.BannersLoaded);
      if (series != null && series.Episodes != null && series.Episodes.Count != 0)
      {
        UpdateSeries(series);
      }
      else
      {
        MessageBox.Show("Couldn't load Series details");
      }
    }

    private void cmdLoadActorInfo_Click(object sender, EventArgs e)
    {
      TvdbSeries series = m_tvdbHandler.GetSeries(m_currentSeries.Id, m_currentSeries.Language, m_currentSeries.EpisodesLoaded,
                                                  true, m_currentSeries.BannersLoaded);
      
      if (series != null && series.TvdbActorsLoaded)
      {
        UpdateSeries(series);
      }
      else
      {
        MessageBox.Show("Couldn't load extended actor info");
      }
    }

    private void cmdLoadBanners_Click(object sender, EventArgs e)
    {
      TvdbSeries series = m_tvdbHandler.GetSeries(m_currentSeries.Id, m_currentSeries.Language, 
                                                  m_currentSeries.EpisodesLoaded, m_currentSeries.TvdbActorsLoaded, 
                                                  true);
      
      if (series != null && series.BannersLoaded)
      {
        UpdateSeries(series);
      }
      else
      {
        MessageBox.Show("Couldn't load Banners");
      }
    }

    private void cbUserFavourites_SelectedIndexChanged(object sender, EventArgs e)
    {
      TvdbSeries series = m_tvdbHandler.GetSeries(((TvdbSeries)cbUserFavourites.SelectedItem).Id,
                                                      m_defaultLang, cbLoadFull.Checked, cbLoadActors.Checked,
                                                      cbLoadBanner.Checked);

      if (series != null)
      {
        CleanUpForm();
        tabControlTvdb.SelectedTab = tabSeries;
        pnlEpisodeEnabled.Visible = series.EpisodesLoaded;
        pnlFanartEnabled.Visible = series.BannersLoaded;
        UpdateSeries(series);
      }
      else
      {
        MessageBox.Show("Couldn't retrieve series");
      }
    }

    private void tabControlTvdb_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (tabControlTvdb.SelectedTab == tabFanart && m_currentSeries != null && m_currentSeries.FanartBanners != null)
      {
        List<TvdbFanartBanner> fanartList = m_currentSeries.FanartBanners;

        if (fanartList != null && fanartList.Count > 0)
        {
          coverFlowFanart.Items = fanartList;
        }
      }
    }


    /// <summary>
    /// Context to save a loaded image
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void saveImageToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      Image saveImage = null;
      if (saveImageContext.SourceControl.GetType() == typeof(PictureBox))
      {
        saveImage = ((PictureBox)saveImageContext.SourceControl).Image;
      }
      else if (saveImageContext.SourceControl.GetType() == typeof(PosterControl))
      {
        saveImage = ((PosterControl)saveImageContext.SourceControl).ActiveImage;
      }
      else if (saveImageContext.SourceControl.GetType() == typeof(CoverFlow))
      {
        saveImage = ((CoverFlow)saveImageContext.SourceControl).ActiveImage;
      }
      else if (saveImageContext.SourceControl.GetType() == typeof(BannerControl))
      {
        saveImage = ((BannerControl)saveImageContext.SourceControl).ActiveImage;
      }

      if (saveImage != null)
      {
        DialogResult res = saveImageDialog.ShowDialog();
        if (res == DialogResult.OK)
        {
          String fileName = saveImageDialog.FileName;
          saveImage.Save(fileName);
        }
      }
    }

    private void cmdSendSeriesRating_Click(object sender, EventArgs e)
    {
      raterSeriesSiteRating.CurrentRating = (int)m_tvdbHandler.RateSeries(m_currentSeries.Id,
                                                 raterSeriesYourRating.CurrentRating);
    }

    private void cmdAddRemoveFavorites_Click(object sender, EventArgs e)
    {
      if (m_currentSeries != null && !m_currentSeries.IsFavorite)
      {
        m_tvdbHandler.AddSeriesToFavorites(m_currentSeries.Id);
      }
      else
      {
        m_tvdbHandler.RemoveSeriesFromFavorites(m_currentSeries.Id);
      }
      cmdAddRemoveFavorites.Text = m_currentSeries.IsFavorite ? "Remove from favorites" : "Add to favorites";
    }

    private void bcActors_IndexChanged(EventArgs _event)
    {
      SetActorInfo(m_currentSeries.TvdbActors[bcActors.Index]);
    }
    private void SetActorInfo(TvdbActor _actor)
    {
      txtActorId.Text = _actor.Id.ToString();
      txtActorName.Text = _actor.Name.ToString();
      txtActorRole.Text = _actor.Role.ToString();
      txtActorSortOrder.Text = _actor.SortOrder.ToString();
    }
  }
}
