/*
 *   TvdbLib: A library to retrieve information and media from http://thetvdb.com
 * 
 *   Copyright (C) 2008  Benjamin Gmeiner
 * 
 *   This program is free software: you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation, either version 3 of the License, or
 *   (at your option) any later version.
 *
 *   This program is distributed in the hope that it will be useful,
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *   GNU General Public License for more details.
 *
 *   You should have received a copy of the GNU General Public License
 *   along with this program.  If not, see <http://www.gnu.org/licenses/>.
 * 
 */

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
using TvdbLib;
using TvdbLib.Cache;
using TvdbLib.Data;
using TvdbLib.Data.Banner;
using TvdbTester.Properties;
using TvdbLib.Exceptions;
using System.Diagnostics;

namespace TvdbTester
{
  public partial class SeriesBrowser : Form
  {
    #region private fields
    private TvdbHandler m_tvdbHandler;
    private TvdbLanguage m_currentLanguage;//the currently selected language
    private TvdbSeries m_currentSeries;//the currently loaded series
    #endregion

    public SeriesBrowser()
    {
      String[] dirs = Directory.GetDirectories(@"C:\Windows\Microsoft.NET\Framework");
      bool is35installed = false;//is the .net 3.5 framework installed
      foreach (String d in dirs)
      {
        if (d.Contains("v3.5"))
        {
          is35installed = true;
          break;
        }
      }
      if (!is35installed)
      {
        MessageBox.Show("To use this program, you have to install the .net 3.5 runtime");
        Application.Exit();
        //this.Close();
      }
      else
      {
        InitializeComponent();
        this.MouseWheel += new MouseEventHandler(Tester_MouseWheel);
      }

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
        try
        {
          StartScreen screen = new StartScreen();
          screen.StartPosition = FormStartPosition.Manual;
          screen.Left = (this.Left) + (this.Width / 2) - (screen.Width / 2);
          screen.Top = (this.Top) + (this.Height / 2) - (screen.Height / 2);
          DialogResult res = screen.ShowDialog();

          if (res == DialogResult.OK)
          {
            ICacheProvider p = null;
            if (screen.CacheProvider == typeof(XmlCacheProvider))
            {
              p = new XmlCacheProvider(screen.RootFolder);
            }
            else if (screen.CacheProvider == typeof(BinaryCacheProvider))
            {
              p = new BinaryCacheProvider(screen.RootFolder);
            }

            if (screen.UseUserIdentifier)
            {
              InitialiseForm(screen.UserIdentifier, p);
            }
            else
            {
              InitialiseForm(null, p);
            }
          }
          else
          {//user clicked on exit -> use no userid and no caching
            InitialiseForm(null, null);
          }
        }
        catch (TvdbNotAvailableException)
        {
          MessageBox.Show("Tvdb not available, try again later");
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
    public void InitialiseForm(String _userId, ICacheProvider _provider)
    {
      //m_tvdbHandler = new Tvdb(new BinaryCacheProvider(@"cachefile.bin"), Resources.API_KEY);new XmlCacheProvider("XmlCache")

      m_tvdbHandler = new TvdbHandler(_provider, Resources.API_KEY);
      m_tvdbHandler.InitCache();

      List<TvdbLanguage> m_languages = m_tvdbHandler.Languages;

      foreach (TvdbLanguage l in m_languages)
      {
        if (l.Abbriviation.Equals("en")) m_currentLanguage = l;
        cbLanguage.Items.Add(l);
      }
      lblCurrentLanguage.Text = "[" + m_currentLanguage.ToString() + "]";

      //enable/disable community features
      if (_userId != null)
      {
        TvdbUser user = new TvdbUser("user", _userId);
        m_tvdbHandler.UserInfo = user;
        List<TvdbSeries> favList = null;
        try
        {
          user.UserPreferredLanguage = m_tvdbHandler.GetPreferredLanguage();
          favList = m_tvdbHandler.GetUserFavorites(user.UserPreferredLanguage);
        }
        catch (TvdbInvalidApiKeyException ex)
        {
          MessageBox.Show(ex.Message);
        }
        catch (TvdbNotAvailableException ex)
        {
          MessageBox.Show(ex.Message);
        }

        if (favList != null)
        {
          foreach (TvdbSeries s in favList)
          {
            if (s != null)
            {
              cbUserFavourites.Items.Add(s);
            }
          }
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
      if (m_tvdbHandler != null) m_tvdbHandler.CloseCache();
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
      TvdbSeries series = null;

      try
      {
        series = m_tvdbHandler.GetSeries(_seriesId, m_currentLanguage, cbLoadFull.Checked,
                                                    cbLoadActors.Checked, cbLoadBanner.Checked, cbUseZipped.Checked);
      }
      catch (TvdbInvalidApiKeyException ex)
      {
        MessageBox.Show(ex.Message);
      }
      catch (TvdbNotAvailableException ex)
      {
        MessageBox.Show(ex.Message);
      }

      if (series != null)
      {
        CleanUpForm();
        tabControlTvdb.SelectedTab = tabSeries;
        UpdateSeries(series);
      }
    }

    private void UpdateSeries(TvdbSeries _series)
    {
      m_currentSeries = _series;
      FillSeriesDetails(_series);

      cmdForceUpdate.Enabled = true;
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

      if (_series.TvdbActorsLoaded && _series.Actors.Count > 0)
      {
        cmdLoadActorInfo.Enabled = false;
        pnlActorsEnabled.Visible = false;
        lbAllActors.Items.Clear();
        bcActors.ClearControl();
        if (_series.TvdbActors.Count > 0)
        {
          List<TvdbBanner> bannerList = new List<TvdbBanner>();
          foreach (TvdbActor a in _series.TvdbActors)
          {
            lbAllActors.Items.Add(a.Name);
            bannerList.Add(a.ActorImage);
          }

          bcActors.BannerImages = bannerList;
          SetActorInfo(_series.TvdbActors[0]);
        }
      }
      else
      {
        bcActors.ClearControl();

        cmdLoadActorInfo.Enabled = true;
        pnlActorsEnabled.Visible = true;
      }

    }



    private void CleanUpForm()
    {
      cmdForceUpdate.Enabled = false;
      cmdLoadActorInfo.Enabled = false;
      cmdLoadFullSeriesInfo.Enabled = false;
      cmdLoadBanners.Enabled = false;
      posterControlSeries.ClearPoster();
      coverFlowFanart.Clear();
      bcSeriesBanner.ClearControl();
      bcSeasonBanner.ClearControl();
      bcSeasonBannerWide.ClearControl();
      bcEpisodeBanner.ClearControl();
      bcActors.ClearControl();
      lbAllActors.Items.Clear();
      ClearSeriesDetails();

      ClearEpisodeDetail();
      tvEpisodes.Nodes.Clear();
    }

    private void ClearSeriesDetails()
    {
      txtSeriesId.Text = "";
      txtSeriesName.Text = "";
      txtStatus.Text = "";
      txtGenre.Text = "";
      txtFirstAired.Text = "";
      txtAirsWeekday.Text = "";
      txtAirstime.Text = "";
      txtNetwork.Text = "";
      txtRuntime.Text = "";
      txtRating.Text = "";
      txtActors.Text = "";
      txtOverview.Text = "";
      txtTvComId.Text = ""; //series.
      llblTvComId.Text = "";
      llblTvComId.Links.Clear();
      txtImdbId.Text = "";
      llblImdb.Text = "";
      llblImdb.Links.Clear();
      txtZap2itId.Text = "";
      raterSeriesSiteRating.CurrentRating = 0;

      bcSeriesBanner.ClearControl();
    }

    private void ClearEpisodeDetail()
    {
      txtEpisodeAbsoluteNumber.Text = "";
      txtEpisodeDVDChapter.Text = "";
      txtEpisodeDVDId.Text = "";
      txtEpisodeDVDNumber.Text = "";
      txtEpisodeDVDSeason.Text = "";
      txtEpisodeFirstAired.Text = "";
      lbGuestStars.Items.Clear();
      lbWriters.Items.Clear();
      lbDirectors.Items.Clear();
      txtEpisodeImdbID.Text = "";
      txtEpisodeLanguage.Text = "";
      lbEpisodeInformation.Text = "Episode Info";
      txtEpisodeOverview.Text = "";
      txtEpisodeProductionCode.Text = "";
      bcEpisodeBanner.ClearControl();
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
        bcSeriesBanner.ClearControl();
      }

      txtSeriesId.Text = series.Id.ToString();
      txtSeriesName.Text = series.SeriesName;
      txtStatus.Text = series.Status;
      txtGenre.Text = series.GenreString;
      txtFirstAired.Text = series.FirstAired != null ? series.FirstAired.ToShortDateString() : "";
      txtAirsWeekday.Text = series.AirsDayOfWeek != null ? series.AirsDayOfWeek.ToString() : "";
      txtAirstime.Text = series.AirsTime.ToShortTimeString();
      txtNetwork.Text = series.Network;
      txtRuntime.Text = series.Runtime != -99 ? series.Runtime.ToString() : "";
      txtRating.Text = series.Rating != -99 ? series.Rating.ToString() : "";
      txtActors.Text = series.ActorsString;
      txtOverview.Text = series.Overview;
      txtTvComId.Text = series.TvDotComId != -99 ? series.TvDotComId.ToString() : ""; //series.
      if (series.TvDotComId != -99)
      {
        String link = "http://www.tv.com/show/" + series.TvDotComId + "/summary.html";
        llblTvComId.Text = "Open";
        llblTvComId.Links.Clear();
        llblTvComId.Links.Add(0, link.Length, link);
      }
      else
      {
        llblTvComId.Links.Clear();
        llblTvComId.Text = "";
      }
      txtImdbId.Text = series.ImdbId;
      if (series.ImdbId != null && !series.ImdbId.Equals(""))
      {
        String link = "http://www.imdb.com/title/" + series.ImdbId;
        llblImdb.Text = "Open";
        llblImdb.Links.Clear();
        llblImdb.Links.Add(0, link.Length, link);
      }
      else
      {
        llblImdb.Links.Clear();
        llblImdb.Text = "";
      }

      txtZap2itId.Text = series.Zap2itId;
      raterSeriesSiteRating.CurrentRating = series.Rating != -99 ? (int)(series.Rating / 10) : 0;
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

      lbGuestStars.Items.Clear();
      foreach (String s in _episode.GuestStars)
      {
        lbGuestStars.Items.Add(s.Trim());
      }

      lbDirectors.Items.Clear();
      foreach (String s in _episode.Directors)
      {
        lbDirectors.Items.Add(s.Trim());
      }

      lbWriters.Items.Clear();
      foreach (String s in _episode.Writer)
      {
        lbWriters.Items.Add(s.Trim());
      }

      txtEpisodeProductionCode.Text = _episode.ProductionCode;
      txtEpisodeOverview.Text = _episode.Overview;
      txtEpisodeDVDId.Text = _episode.DvdDiscId != -99 ? _episode.DvdDiscId.ToString() : "";
      txtEpisodeDVDSeason.Text = _episode.DvdSeason != -99 ? _episode.DvdSeason.ToString() : "";
      txtEpisodeDVDNumber.Text = _episode.DvdEpisodeNumber != -99 ? _episode.DvdEpisodeNumber.ToString() : "";
      txtEpisodeDVDChapter.Text = _episode.DvdChapter != -99 ? _episode.DvdChapter.ToString() : "";
      txtEpisodeAbsoluteNumber.Text = _episode.AbsoluteNumber != -99 ? _episode.AbsoluteNumber.ToString() : "";
      txtEpisodeImdbID.Text = _episode.ImdbId;

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
            bcEpisodeBanner.ClearControl();
          }
          selectedSeason = ep.SeasonNumber;
        }
        else if (tvEpisodes.SelectedNode.Tag.GetType() == typeof(SeasonTag))
        {//season node
          SeasonTag tag = (SeasonTag)tvEpisodes.SelectedNode.Tag;
          selectedSeason = tag.SeasonNumber;
          ClearEpisodeDetail();
          lbEpisodeInformation.Text = "Season " + tag.SeasonNumber;
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
                  if (b.Language == null || b.Language.Abbriviation.Equals(m_currentLanguage.Abbriviation))
                  {
                    seasonList.Add(b);
                  }
                }

                if (b.BannerType == TvdbSeasonBanner.Type.seasonwide)
                {
                  if (b.Language == null || b.Language.Abbriviation.Equals(m_currentLanguage.Abbriviation))
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
            bcSeasonBanner.ClearControl();
          }
          bcSeasonBanner.Tag = selectedSeason;

          if (seasonWideList.Count > 0)
          {
            bcSeasonBannerWide.BannerImages = seasonWideList;
          }
          else
          {
            bcSeasonBannerWide.ClearControl();
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
      if (!txtSeriesToFind.Text.Equals(""))
      {
        List<TvdbSearchResult> list = m_tvdbHandler.SearchSeries(txtSeriesToFind.Text, m_currentLanguage);
        if (list != null && list.Count > 0)
        {
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
        else
        {
          MessageBox.Show("No results for this series");
        }
      }
    }

    private void cbLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (cbLanguage.SelectedItem != null && cbLanguage.SelectedItem.GetType() == typeof(TvdbLanguage))
      {
        lblCurrentLanguage.Text = "[" + ((TvdbLanguage)cbLanguage.SelectedItem).ToString() + "]";
        m_currentLanguage = (TvdbLanguage)cbLanguage.SelectedItem;
        if (m_currentSeries != null &&
            MessageBox.Show("Change language of current series \"" + m_currentSeries.SeriesName + "\" to " +
                            m_currentLanguage.Name + "?", "Change language", MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
          LoadSeries(m_currentSeries.Id);//reload series with new language
        }
      }
    }

    private void updateToolStripMenuItem_Click(object sender, EventArgs e)
    {
      m_tvdbHandler.UpdateAllSeries();
    }

    private void cmdForceUpdate_Click(object sender, EventArgs e)
    {
      TvdbSeries series = null;
      try
      {
        series = m_tvdbHandler.ForceReload(m_currentSeries, cbLoadFull.Checked,
                                                    cbLoadActors.Checked, cbLoadBanner.Checked);
      }
      catch (TvdbInvalidApiKeyException ex)
      {
        MessageBox.Show(ex.Message);
      }
      catch (TvdbNotAvailableException ex)
      {
        MessageBox.Show(ex.Message);
      }
      if (series != null)
      {
        m_currentSeries = series;
        UpdateSeries(m_currentSeries);
      }
    }

    private void cmdLoadFullSeriesInfo_Click(object sender, EventArgs e)
    {
      TvdbSeries series = null;
      try
      {
        series = m_tvdbHandler.GetSeries(m_currentSeries.Id, m_currentSeries.Language, true,
                                                    m_currentSeries.TvdbActorsLoaded, m_currentSeries.BannersLoaded);
      }
      catch (TvdbInvalidApiKeyException ex)
      {
        MessageBox.Show(ex.Message);
      }
      catch (TvdbNotAvailableException ex)
      {
        MessageBox.Show(ex.Message);
      }

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
      TvdbSeries series = null;
      try
      {
        series = m_tvdbHandler.GetSeries(m_currentSeries.Id, m_currentSeries.Language, m_currentSeries.EpisodesLoaded,
                                                  true, m_currentSeries.BannersLoaded);
      }
      catch (TvdbInvalidApiKeyException ex)
      {
        MessageBox.Show(ex.Message);
      }
      catch (TvdbNotAvailableException ex)
      {
        MessageBox.Show(ex.Message);
      }

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
      TvdbSeries series = null;
      try
      {
        series = m_tvdbHandler.GetSeries(m_currentSeries.Id, m_currentSeries.Language,
                                                  m_currentSeries.EpisodesLoaded, m_currentSeries.TvdbActorsLoaded,
                                                  true);
      }
      catch (TvdbInvalidApiKeyException ex)
      {
        MessageBox.Show(ex.Message);
      }
      catch (TvdbNotAvailableException ex)
      {
        MessageBox.Show(ex.Message);
      }

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
      TvdbSeries series = null;
      try
      {
        series = m_tvdbHandler.GetSeries(((TvdbSeries)cbUserFavourites.SelectedItem).Id,
                                                      m_currentLanguage, cbLoadFull.Checked, cbLoadActors.Checked,
                                                      cbLoadBanner.Checked, cbUseZipped.Checked);
      }
      catch (TvdbInvalidApiKeyException ex)
      {
        MessageBox.Show(ex.Message);
      }
      catch (TvdbNotAvailableException ex)
      {
        MessageBox.Show(ex.Message);
      }

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
      if (tabControlTvdb.SelectedTab == tabFanart &&
          m_currentSeries != null &&
          m_currentSeries.FanartBanners != null &&
          coverFlowFanart.Tag != m_currentSeries)
      {
        List<TvdbFanartBanner> fanartList = m_currentSeries.FanartBanners;
        coverFlowFanart.Tag = m_currentSeries;
        if (fanartList != null && fanartList.Count > 0)
        {
          coverFlowFanart.Items = fanartList;
        }
      }
    }

    private void saveImageContext_Opening(object sender, CancelEventArgs e)
    {
      showImageToolStripMenuItem.Enabled = true;
    }

    private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Image img = GetSelectedImage();
      if (img != null)
      {
        Clipboard.SetImage(img);
      }
    }

    /// <summary>
    /// Context to save a loaded image
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void saveImageToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      Image saveImage = GetSelectedImage();

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

    private Image GetSelectedImage()
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
      return saveImage;
    }

    private void showImageToolStripMenuItem_Click(object sender, EventArgs e)
    {
      FullscreenForm fsf = new FullscreenForm();
      fsf.Banner = GetSelectedImage();
      fsf.Show();
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
      if (lbAllActors.SelectedIndex != bcActors.Index)
      {
        lbAllActors.SelectedIndex = bcActors.Index;
      }
      else
      {
        SetActorInfo(m_currentSeries.TvdbActors[bcActors.Index]);
      }
    }
    private void SetActorInfo(TvdbActor _actor)
    {
      txtActorId.Text = _actor.Id.ToString();
      txtActorName.Text = _actor.Name.ToString();
      txtActorRole.Text = _actor.Role.ToString();
      txtActorSortOrder.Text = _actor.SortOrder.ToString();
    }

    private void cmdRefreshSeries_Click(object sender, EventArgs e)
    {
      TvdbLib.TvdbHandler.Interval updateInterval = TvdbHandler.Interval.automatic;
      if (rbUpdateAutomatic.Checked)
      {
        updateInterval = TvdbHandler.Interval.automatic;
      }
      else if (rbUpdateDay.Checked)
      {
        updateInterval = TvdbHandler.Interval.day;
      }
      else if (rbUpdateWeek.Checked)
      {
        updateInterval = TvdbHandler.Interval.week;
      }
      else if (rbUpdateMonth.Checked)
      {
        updateInterval = TvdbHandler.Interval.month;
      }
      UpdateForm frm = new UpdateForm(m_tvdbHandler, updateInterval, cbUseZipped.Checked);
      frm.ShowDialog();
      
    }

    private void txtSeriesToFind_TextChanged(object sender, EventArgs e)
    {
    }

    private void txtSeriesToFind_KeyPress(object sender, KeyPressEventArgs e)
    {

    }

    private void txtSeriesToFind_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        cmdFindSeries_Click(this, new EventArgs());
      }
    }

    private void cbUseZipped_CheckedChanged(object sender, EventArgs e)
    {
      if (cbUseZipped.Checked)
      {
        cbLoadFull.Checked = true;
        cbLoadFull.Enabled = false;
        cbLoadBanner.Checked = true;
        cbLoadBanner.Enabled = false;
        cbLoadActors.Checked = true;
        cbLoadActors.Enabled = false;
      }
      else
      {
        cbLoadFull.Enabled = true;
        cbLoadBanner.Enabled = true;
        cbLoadActors.Enabled = true;
      }
    }

    private void lbAllActors_SelectedIndexChanged(object sender, EventArgs e)
    {
      bcActors.Index = lbAllActors.SelectedIndex;
      SetActorInfo(m_currentSeries.TvdbActors[lbAllActors.SelectedIndex]);
    }

    private void cmdBack_Click(object sender, EventArgs e)
    {
      //pnlFullscreen.Visible = false;
    }

    private void LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      if (e.Link.LinkData != null)
      {
        Process.Start((String)e.Link.LinkData);
      }
    }


  }
}
