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
using TvdbTester.Properties;
using TvdbLib.Data;
using TvdbLib.Data.Banner;
using TvdbLib.SharpZipLib.Zip;
using System.IO;

namespace TvdbTester
{
  public partial class TestForm : Form
  {
    private TvdbSeries m_selectedSeriesForBannerTesting = null;
    private int m_bannerTestingIndex = 0;

    public TestForm()
    {
      InitializeComponent();
      TvdbEpisode.EpisodeOrdering[] names = (TvdbEpisode.EpisodeOrdering[])Enum.GetValues(typeof(TvdbEpisode.EpisodeOrdering));
      foreach (TvdbEpisode.EpisodeOrdering o in names)
      {
        cbOrdering.Items.Add(o);
      }
    }

    private Tvdb m_tvdbHandler;

    private void cmdInit_Click(object sender, EventArgs e)
    {
      m_tvdbHandler = new Tvdb(new XmlCacheProvider("C:\\test"), Resources.API_KEY);
      List<TvdbLanguage> lang = m_tvdbHandler.Languages;
      cmdTest1.Enabled = true;
      cmdTestZip.Enabled = true;
      cmdGetEpisodes.Enabled = true;
      cmdEnd.Enabled = true;
      cmdSetUser.Enabled = true;
      cmdGetEpisodeAired.Enabled = true;
      cmdBannerTestingNext.Enabled = true;
      cmdBannerTestingPrev.Enabled = true;
      cmdLoadBannerTest.Enabled = true;
    }

    private void cmdTest1_Click(object sender, EventArgs e)
    {

      TvdbSeries s = m_tvdbHandler.GetSeries(73255, TvdbLanguage.DefaultLanguage, true, true, true);

      if (s.TvdbActorsLoaded)
      {
        List<TvdbBanner> bannerList = new List<TvdbBanner>();
        foreach (TvdbActor a in s.TvdbActors)
        {
          bannerList.Add(a.ActorImage);
        }
        bcActors.BannerImages = bannerList;
      }
    }

    private void cmdTestZip_Click(object sender, EventArgs e)
    {
      List<TvdbLanguage> lang = m_tvdbHandler.Languages;
      TvdbSeries s = m_tvdbHandler.GetSeries(Int32.Parse(txtSeriesId.Text), TvdbLanguage.DefaultLanguage, true, true, true, true);

      lvSeries.Items.Add(CreateItem("Id", s.Id.ToString()));
      lvSeries.Items.Add(CreateItem("Name", s.SeriesName));
      lvSeries.Items.Add(CreateItem("Actors", s.ActorsString));
      lvSeries.Items.Add(CreateItem("Genre", s.GenreString));
      lvSeries.Items.Add(CreateItem("Overview", s.Overview));
      lvSeries.Items.Add(CreateItem("Tv.com Id", s.TvDotComId.ToString()));
      lvSeries.Items.Add(CreateItem("Imdb Id", s.ImdbId));

      if (s.TvdbActorsLoaded)
      {
        List<TvdbBanner> bannerList = new List<TvdbBanner>();
        foreach (TvdbActor a in s.TvdbActors)
        {
          bannerList.Add(a.ActorImage);
        }

        bcActors.BannerImages = bannerList;
      }

      if (s.SeriesBanners != null && s.SeriesBanners.Count > 0)
      {
        bcSeriesBanner.BannerImage = s.SeriesBanners[0];
      }
    }

    private ListViewItem CreateItem(String _par1, String _par2)
    {
      ListViewItem item = new ListViewItem(_par1);
      item.SubItems.Add(_par2);
      return item;
    }

    private void cmdEnd_Click(object sender, EventArgs e)
    {
      m_tvdbHandler.SaveCache();
    }

    private void cmdGetEpisodes_Click(object sender, EventArgs e)
    {
      int sId = Int32.Parse(txtSeriesId2.Text);
      int season = Int32.Parse(txtSeason.Text);
      int episode = Int32.Parse(txtEpisode.Text);
      TvdbEpisode ep = m_tvdbHandler.GetEpisode(sId, season, episode, 
                                               (TvdbEpisode.EpisodeOrdering)cbOrdering.SelectedItem, 
                                               TvdbLanguage.DefaultLanguage);

      lvSeries.Items.Clear();
      lvSeries.Items.Add(CreateItem("Series Id", ep.SeriesId.ToString()));
      lvSeries.Items.Add(CreateItem("Episode Id", ep.Id.ToString()));
      lvSeries.Items.Add(CreateItem("Name", ep.EpisodeName));
      lvSeries.Items.Add(CreateItem("Gueststars", ep.GuestStarsString));
      lvSeries.Items.Add(CreateItem("Directors", ep.DirectorsString));
      lvSeries.Items.Add(CreateItem("Writer", ep.WriterString));
      lvSeries.Items.Add(CreateItem("Overview", ep.Overview));
      lvSeries.Items.Add(CreateItem("Imdb Id", ep.ImdbId));
    }

    private void cmdGetAllSeriesRatings_Click(object sender, EventArgs e)
    {
      Dictionary<int, TvdbRating> ratingList = m_tvdbHandler.GetRatedSeries();
      if (ratingList != null)
      {
        lvSeries.Items.Clear();
        foreach (KeyValuePair<int, TvdbRating> r in ratingList)
        {
         
          lvSeries.Items.Add(CreateItem(r.Key.ToString(), "Community: " + r.Value.CommunityRating + ", User: " + r.Value.UserRating));
        }
      }
      else
      {
        MessageBox.Show("Nothing returned");
      }
    }

    private void cmdSetUser_Click(object sender, EventArgs e)
    {
      TvdbUser user = new TvdbUser();
      user.UserIdentifier = txtUserId.Text;
      user.UserName = "test";
      m_tvdbHandler.UserInfo = user;
      cmdGetAllSeriesRatings.Enabled = true;
      cmdGetRatingsForSeries.Enabled = true;

    }

    private void button1_Click(object sender, EventArgs e)
    {
      Dictionary<int, TvdbRating> ratingList = m_tvdbHandler.GetRatingsForSeries(Int32.Parse(txtSeriesRatingsId.Text));
      if (ratingList != null)
      {
        lvSeries.Items.Clear();
        foreach (KeyValuePair<int, TvdbRating> r in ratingList)
        {
          lvSeries.Items.Add(CreateItem(r.Value.RatingItemType.ToString() + ": " + r.Key.ToString(), "Community: " + r.Value.CommunityRating + ", User: " + r.Value.UserRating));
        }
      }
      else
      {
        MessageBox.Show("Nothing returned");
      }
    }

    private void cmdGetEpisodeAired_Click(object sender, EventArgs e)
    {
      TvdbEpisode ep = m_tvdbHandler.GetEpisode(Int32.Parse(txtSeriesEpisodeAiredId.Text), dateTimePickerEpAired.Value, TvdbLanguage.DefaultLanguage);
      if (ep != null)
      {
        lvSeries.Items.Clear();
        lvSeries.Items.Add(CreateItem("Series Id", ep.SeriesId.ToString()));
        lvSeries.Items.Add(CreateItem("Episode Id", ep.Id.ToString()));
        lvSeries.Items.Add(CreateItem("Name", ep.EpisodeName));
        lvSeries.Items.Add(CreateItem("Gueststars", ep.GuestStarsString));
        lvSeries.Items.Add(CreateItem("Directors", ep.DirectorsString));
        lvSeries.Items.Add(CreateItem("Writer", ep.WriterString));
        lvSeries.Items.Add(CreateItem("Overview", ep.Overview));
        lvSeries.Items.Add(CreateItem("Imdb Id", ep.ImdbId));
      }
      else
      {
        MessageBox.Show("Nothing found");
      }
    }

    private void cmdLoadBannerTest_Click(object sender, EventArgs e)
    {
      m_selectedSeriesForBannerTesting = m_tvdbHandler.GetSeries(Int32.Parse(txtSeriesIdForBanners.Text), TvdbLanguage.DefaultLanguage, true, true, true, true);
      if (m_selectedSeriesForBannerTesting != null)
      {
        m_bannerTestingIndex = 0;
        pbBannerTesting.Visible = true;
        pbBannerTesting.BringToFront();
        LoadBanner(m_bannerTestingIndex);
      }
      else
      {
        MessageBox.Show("Couldn't load series");
      }
    }

    private void LoadBanner(int _index)
    {
      if (rbLoadFanart.Checked)
      {
        if (m_selectedSeriesForBannerTesting.FanartBanners != null && m_selectedSeriesForBannerTesting.FanartBanners.Count > _index)
        {
          if (m_selectedSeriesForBannerTesting.FanartBanners[_index].IsLoaded || m_selectedSeriesForBannerTesting.FanartBanners[_index].LoadBanner())
          {
            pbBannerTesting.Image = m_selectedSeriesForBannerTesting.FanartBanners[_index].Banner;
          }
        }
        else
        {
          MessageBox.Show("No fanart for this series");
        }
      }
      else if (rbLoadFanartThumb.Checked)
      {
        if (m_selectedSeriesForBannerTesting.FanartBanners != null && m_selectedSeriesForBannerTesting.FanartBanners.Count > _index)
        {
          if (m_selectedSeriesForBannerTesting.FanartBanners[_index].IsThumbLoaded || m_selectedSeriesForBannerTesting.FanartBanners[_index].LoadThumb())
          {
            pbBannerTesting.Image = m_selectedSeriesForBannerTesting.FanartBanners[_index].BannerThumb;
          }
        }
        else
        {
          MessageBox.Show("No fanart for this series");
        }
      }
      else if (rbLoadSeriesBanner.Checked)
      {
        if (m_selectedSeriesForBannerTesting.SeriesBanners != null && m_selectedSeriesForBannerTesting.SeriesBanners.Count > _index)
        {
          if (m_selectedSeriesForBannerTesting.SeriesBanners[_index].IsLoaded || m_selectedSeriesForBannerTesting.SeriesBanners[_index].LoadBanner())
          {
            pbBannerTesting.Image = m_selectedSeriesForBannerTesting.SeriesBanners[_index].Banner;
          }
        }
        else
        {
          MessageBox.Show("No series banners  for this series");
        }
      }
      else if (rbLoadSeriesBannerThumb.Checked)
      {
        if (m_selectedSeriesForBannerTesting.SeriesBanners != null && m_selectedSeriesForBannerTesting.SeriesBanners.Count > _index)
        {
          if (m_selectedSeriesForBannerTesting.SeriesBanners[_index].IsThumbLoaded || m_selectedSeriesForBannerTesting.SeriesBanners[_index].LoadThumb())
          {
            pbBannerTesting.Image = m_selectedSeriesForBannerTesting.SeriesBanners[_index].BannerThumb;
          }
        }
        else
        {
          MessageBox.Show("No series banners for this series");
        }
      }
      else if (rbLoadPoster.Checked)
      {
        if (m_selectedSeriesForBannerTesting.PosterBanners != null && m_selectedSeriesForBannerTesting.PosterBanners.Count > _index)
        {
          if (m_selectedSeriesForBannerTesting.PosterBanners[_index].IsLoaded || m_selectedSeriesForBannerTesting.PosterBanners[_index].LoadBanner())
          {
            pbBannerTesting.Image = m_selectedSeriesForBannerTesting.PosterBanners[_index].Banner;
          }
        }
        else
        {
          MessageBox.Show("No Poster banners  for this Poster");
        }
      }
      else if (rbLoadPosterThumb.Checked)
      {
        if (m_selectedSeriesForBannerTesting.PosterBanners != null && m_selectedSeriesForBannerTesting.PosterBanners.Count > _index)
        {
          if (m_selectedSeriesForBannerTesting.PosterBanners[_index].IsThumbLoaded || m_selectedSeriesForBannerTesting.PosterBanners[_index].LoadThumb())
          {
            pbBannerTesting.Image = m_selectedSeriesForBannerTesting.PosterBanners[_index].BannerThumb;
          }
        }
        else
        {
          MessageBox.Show("No Poster banners for this Poster");
        }
      }
      else if (rbLoadSeason.Checked)
      {
        if (m_selectedSeriesForBannerTesting.SeasonBanners != null && m_selectedSeriesForBannerTesting.SeasonBanners.Count > _index)
        {
          if (m_selectedSeriesForBannerTesting.SeasonBanners[_index].IsLoaded || m_selectedSeriesForBannerTesting.SeasonBanners[_index].LoadBanner())
          {
            pbBannerTesting.Image = m_selectedSeriesForBannerTesting.SeasonBanners[_index].Banner;
          }
        }
        else
        {
          MessageBox.Show("No Poster banners  for this Poster");
        }
      }
      else if (rbLoadSeasonThumb.Checked)
      {
        if (m_selectedSeriesForBannerTesting.SeasonBanners != null && m_selectedSeriesForBannerTesting.SeasonBanners.Count > _index)
        {
          if (m_selectedSeriesForBannerTesting.SeasonBanners[_index].IsThumbLoaded || m_selectedSeriesForBannerTesting.SeasonBanners[_index].LoadThumb())
          {
            pbBannerTesting.Image = m_selectedSeriesForBannerTesting.SeasonBanners[_index].BannerThumb;
          }
        }
        else
        {
          MessageBox.Show("No Poster banners for this Poster");
        }
      }
      else if (rbLoadEpisodeBanner.Checked)
      {
        if (m_selectedSeriesForBannerTesting.EpisodesLoaded && m_selectedSeriesForBannerTesting.Episodes != null && m_selectedSeriesForBannerTesting.Episodes.Count > _index)
        {
          if (m_selectedSeriesForBannerTesting.Episodes[_index].Banner.IsLoaded || m_selectedSeriesForBannerTesting.Episodes[_index].Banner.LoadBanner())
          {
            pbBannerTesting.Image = m_selectedSeriesForBannerTesting.Episodes[_index].Banner.Banner;
          }
        }
        else
        {
          MessageBox.Show("No Poster banners  for this Poster");
        }
      }
      else if (rbLoadEpisodeBannerThumb.Checked)
      {
        if (m_selectedSeriesForBannerTesting.Episodes != null && m_selectedSeriesForBannerTesting.Episodes.Count > _index)
        {
          if (m_selectedSeriesForBannerTesting.Episodes[_index].Banner.IsThumbLoaded || m_selectedSeriesForBannerTesting.Episodes[_index].Banner.LoadThumb())
          {
            pbBannerTesting.Image = m_selectedSeriesForBannerTesting.Episodes[_index].Banner.BannerThumb;
          }
        }
        else
        {
          MessageBox.Show("No Poster banners for this Poster");
        }
      }

    }

    private void cmdBannerTestingNext_Click(object sender, EventArgs e)
    {
      m_bannerTestingIndex ++;
      LoadBanner(m_bannerTestingIndex);
    }

    private void cmdBannerTestingPrev_Click(object sender, EventArgs e)
    {
      m_bannerTestingIndex++;
      LoadBanner(m_bannerTestingIndex);
    }
  }
}
