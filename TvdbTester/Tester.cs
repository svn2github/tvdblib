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
using ICSharpCode.SharpZipLib.Zip;
using System.Net;
using TvdbConnector;
using TvdbConnector.Cache;
using TvdbConnector.Data;
using TvdbConnector.Data.Banner;
using TvdbTester.Properties;

namespace TvdbTester
{
  public partial class Tester : Form
  {
    Tvdb m_tvdbHandler;
    TvdbLanguage m_defaultLang;
    TvdbSeries m_currentSeries;
    public Tester()
    {
      InitializeComponent();
      this.MouseWheel += new MouseEventHandler(Tester_MouseWheel);

    }

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

        InitialiseForm(screen.UserIdentifier);

        TvdbUser user = new TvdbUser("DieBagger", screen.UserIdentifier);
        m_tvdbHandler.UserInfo = user;
        user.UserPreferredLanguage = m_tvdbHandler.GetPreferredLanguage();
        List<TvdbSeries> favList = m_tvdbHandler.GetUserFavourites(user.UserPreferredLanguage);
        foreach (TvdbSeries s in favList)
        {
          cbUserFavourites.Items.Add(s);
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
      m_tvdbHandler = new Tvdb(new BinaryCacheProvider(@"cachefile.bin"), Resources.API_KEY);
      m_tvdbHandler.LoadCache();

      List<TvdbSeries> cachedSeries = m_tvdbHandler.GetCachedSeries();
      if (cachedSeries != null && cachedSeries.Count > 0)
      {
        foreach (TvdbSeries s in cachedSeries)
        {
          cbCachedSeries.Items.Add(s);
        }
      }

      List<TvdbLanguage> m_languages = m_tvdbHandler.Languages;

      foreach (TvdbLanguage l in m_languages)
      {
        if (l.Abbriviation.Equals("en")) m_defaultLang = l;
        cbLanguage.Items.Add(l);
      }
      lblCurrentLanguage.Text = "[" + m_defaultLang.ToString() + "]";

      TvdbUser user = new TvdbUser("DieBagger", _userId);
      m_tvdbHandler.UserInfo = user;
      user.UserPreferredLanguage = m_tvdbHandler.GetPreferredLanguage();
      List<TvdbSeries> favList = m_tvdbHandler.GetUserFavourites(user.UserPreferredLanguage);
      foreach (TvdbSeries s in favList)
      {
        cbUserFavourites.Items.Add(s);
      }
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
      //throw new NotImplementedException();
    }

    private void cmdTest_Click(object sender, EventArgs e)
    {

      //FastZip fz = new FastZip();
      //fz.ExtractZip(@"C:\en.zip", @"C:\test2","");


    }

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

      TvdbSeries series = m_tvdbHandler.GetSeries(_seriesId, m_defaultLang, cbLoadFull.Checked, cbLoadBanner.Checked);

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

        posterControlSeries.PosterImages = _series.PosterBanners;
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

    }



    private void CleanUpForm()
    {
      posterControlSeries.ClearPoster();
      coverFlowFanart.Clear();

      pbEpisodeBanner.Image = Resources.episode_notfound;
      pbEpisodeSeasonBannerWide.Image = Resources.tvdb_logo1;
      pbEpisodeSeasonImage.Image = Resources.season_notfound;
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
      foreach (TvdbBanner b in series.Banners)
      {
        if (b.GetType() == typeof(TvdbSeriesBanner))
        {
          //if (b.Language.Id == m_defaultLang.Id)
          {
            if (b.IsLoaded || b.LoadBanner())
            {
              pbSeries.Image = b.Banner;
            }
            break;
          }
        }
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
          if (ep.Banner.IsLoaded || ep.Banner.LoadBanner())
          {
            pbEpisodeBanner.Image = ep.Banner.Banner;
          }
          else
          {
            pbEpisodeBanner.Image = null;
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

        if (pbEpisodeSeasonImage.Tag == null || selectedSeason != ((SeasonImageList)pbEpisodeSeasonImage.Tag).Season)
        {
          List<TvdbSeasonBanner> seasonList = new List<TvdbSeasonBanner>();
          List<TvdbSeasonBanner> seasonWideList = new List<TvdbSeasonBanner>();

          if (m_currentSeries.SeasonBanners != null && m_currentSeries.SeasonBanners.Count > 0)
          {
            foreach (TvdbSeasonBanner b in m_currentSeries.SeasonBanners)
            {
              if (b.Season == selectedSeason)
              {
                if (b.BannerType == TvdbSeasonBanner.Type.season)
                {
                  if (b.Language == null || b.Language.Id == m_defaultLang.Id)
                  {
                    seasonList.Add(b);
                  }
                }

                if (b.BannerType == TvdbSeasonBanner.Type.seasonwide)
                {
                  if (b.Language == null || b.Language.Id == m_defaultLang.Id)
                  {
                    seasonWideList.Add(b);
                  }
                }
              }
            }
          }

          pbEpisodeSeasonImage.Tag = new SeasonImageList(seasonList, 0, selectedSeason);
          cmdPreviousSeasonBanner.Visible = false;
          if (seasonList.Count <= 1)
          {
            cmdNextSeasonBanner.Visible = false;
          }
          else
          {
            cmdNextSeasonBanner.Visible = true;
          }

          if (seasonList.Count > 0 && (seasonList[0].IsLoaded || seasonList[0].LoadBanner()))
          {
            pbEpisodeSeasonImage.Image = seasonList[0].Banner;
          }
          else
          {
            pbEpisodeSeasonImage.Image = Resources.episode_notfound;
          }

          pbEpisodeSeasonBannerWide.Tag = new SeasonImageList(seasonWideList, 0, selectedSeason);
          cmdPreviousWideSeasonBanner.Visible = false;
          if (seasonWideList.Count <= 1)
          {
            cmdNextWideSeasonBanner.Visible = false;
          }
          else
          {
            cmdNextWideSeasonBanner.Visible = true;
          }

          if (seasonWideList.Count > 0 && (seasonWideList[0].IsLoaded || seasonWideList[0].LoadBanner()))
          {
            pbEpisodeSeasonBannerWide.Image = seasonWideList[0].Banner;
          }
          else
          {
            pbEpisodeSeasonBannerWide.Image = Resources.tvdb_logo1;
          }


        }
      }
    }

    private void txtEpisodeNumber_TextChanged(object sender, EventArgs e)
    {

    }

    private void Tester_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (m_tvdbHandler != null) m_tvdbHandler.SaveCache();
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

    private void cbCachedSeries_SelectedIndexChanged(object sender, EventArgs e)
    {
      LoadSeries(((TvdbSeries)cbCachedSeries.SelectedItem).Id);
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
      TvdbSeries series = m_tvdbHandler.GetSeries(m_currentSeries.Id, m_currentSeries.Language, true, false);
      if (series != null && series.Episodes != null && series.Episodes.Count != 0)
      {
        UpdateSeries(series);
      }
      else
      {
        MessageBox.Show("Couldn't load Series details");
      }
    }

    private void cmdLoadBanners_Click(object sender, EventArgs e)
    {
      TvdbSeries series = m_tvdbHandler.GetSeries(m_currentSeries.Id, m_currentSeries.Language, false, true);
      if (series != null && m_currentSeries.BannersLoaded)
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
                                                      m_defaultLang, cbLoadFull.Checked, 
                                                      cbLoadBanner.Checked);

      tabControlTvdb.SelectedTab = tabSeries;
      pnlEpisodeEnabled.Visible = true;
      pnlFanartEnabled.Visible = true;
      UpdateSeries(series);
    }

    private void tabControlTvdb_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (tabControlTvdb.SelectedTab == tabFanart)
      {
        List<TvdbFanartBanner> fanartList = m_currentSeries.FanartBanners;

        if (fanartList != null && fanartList.Count > 0)
        {
          coverFlowFanart.Items = fanartList;
        }
      }
    }

    #region Banner and WideBanner Changing
    private void cmdPreviousWideSeasonBanner_Click(object sender, EventArgs e)
    {
      SeasonImageList list = ((SeasonImageList)(pbEpisodeSeasonBannerWide.Tag));
      if (list != null)
      {
        list.CurrentIndex--;
        ChangeSeasonWideBannerButtonEnabled(list);
      }
    }

    private void cmdNextWideSeasonBanner_Click(object sender, EventArgs e)
    {
      SeasonImageList list = ((SeasonImageList)(pbEpisodeSeasonBannerWide.Tag));
      if (list != null)
      {
        list.CurrentIndex++;
        ChangeSeasonWideBannerButtonEnabled(list);
      }
    }

    private void ChangeSeasonWideBannerButtonEnabled(SeasonImageList list)
    {
      if (list.Banners[list.CurrentIndex].IsLoaded || list.Banners[list.CurrentIndex].LoadBanner())
      {
        pbEpisodeSeasonBannerWide.Image = list.Banners[list.CurrentIndex].Banner;
      }

      if (list.CurrentIndex + 1 == list.Banners.Count)
      {
        cmdNextWideSeasonBanner.Visible = false;
      }
      else
      {
        cmdNextWideSeasonBanner.Visible = true;
      }

      if (list.CurrentIndex == 0)
      {
        cmdPreviousWideSeasonBanner.Visible = false;
      }
      else
      {
        cmdPreviousWideSeasonBanner.Visible = true;
      }
    }



    private void cmdPreviousSeasonBanner_Click(object sender, EventArgs e)
    {
      SeasonImageList list = ((SeasonImageList)(pbEpisodeSeasonImage.Tag));
      if (list != null)
      {
        list.CurrentIndex--;
        ChangeSeasonBannerButtonEnabled(list);
      }
    }

    private void cmdNextSeasonBanner_Click(object sender, EventArgs e)
    {
      SeasonImageList list = ((SeasonImageList)(pbEpisodeSeasonImage.Tag));
      if (list != null)
      {
        list.CurrentIndex++;
        ChangeSeasonBannerButtonEnabled(list);
      }
    }

    private void ChangeSeasonBannerButtonEnabled(SeasonImageList list)
    {
      if (list.Banners[list.CurrentIndex].IsLoaded || list.Banners[list.CurrentIndex].LoadBanner())
      {
        pbEpisodeSeasonImage.Image = list.Banners[list.CurrentIndex].Banner;
      }

      if (list.CurrentIndex + 1 == list.Banners.Count)
      {
        cmdNextSeasonBanner.Visible = false;
      }
      else
      {
        cmdNextSeasonBanner.Visible = true;
      }

      if (list.CurrentIndex == 0)
      {
        cmdPreviousSeasonBanner.Visible = false;
      }
      else
      {
        cmdPreviousSeasonBanner.Visible = true;
      }
    }

    #endregion

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

      if (saveImage != null)
      {
        DialogResult res = saveImageDialog.ShowDialog();
        if (res == DialogResult.OK)
        {
          String fileName = saveImageDialog.FileName;
          if(!File.Exists(fileName) || 
             MessageBox.Show("Overwrite File?", "File Exists", MessageBoxButtons.YesNo) == DialogResult.Yes)
          {
            saveImage.Save(fileName);
          }
        }
      }
    }

  }
}
