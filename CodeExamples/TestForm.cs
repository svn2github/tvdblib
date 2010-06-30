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
using TvdbLib.Data.Banner;
using TvdbLib.SharpZipLib.Zip;
using System.IO;
using TvdbLib.Exceptions;

namespace TvdbTester
{
  public partial class TestForm : Form
  {
    private TvdbSeries m_selectedSeriesForBannerTesting = null;
    private int m_bannerTestingIndex = 0;
    private TvdbHandler m_tvdbHandler;

    public TestForm()
    {
      InitializeComponent();
      TvdbEpisode.EpisodeOrdering[] names = (TvdbEpisode.EpisodeOrdering[])Enum.GetValues(typeof(TvdbEpisode.EpisodeOrdering));
      foreach (TvdbEpisode.EpisodeOrdering o in names)
      {
        cbOrdering.Items.Add(o);
      }
    }


    private void cmdInit_Click(object sender, EventArgs e)
    {
      m_tvdbHandler = new TvdbHandler(new XmlCacheProvider("C:\\test"), File.ReadAllText("api_key.txt"));
      m_tvdbHandler.InitCache();
      //m_tvdbHandler.ClearCache();
      //m_tvdbHandler.GetBasicSeries(73545, TvdbLanguage.DefaultLanguage, false);
      //TvdbSeries s = m_tvdbHandler.GetSeries(73545, TvdbLanguage.DefaultLanguage, true, true, true);

      List<TvdbLanguage> lang = m_tvdbHandler.Languages;
      cmdTest1.Enabled = true;
      cmdTestZip.Enabled = true;
      cmdGetEpisodes.Enabled = true;
      cmdEnd.Enabled = true;
      cmdSetUser.Enabled = true;
      cmdGetEpisodeAired.Enabled = true;
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
      m_tvdbHandler.CloseCache();
    }

    private void cmdGetEpisodes_Click(object sender, EventArgs e)
    {
      int sId = Int32.Parse(txtSeriesId2.Text);
      int season = Int32.Parse(txtSeason.Text);
      int episode = Int32.Parse(txtEpisode.Text);
      lvSeries.Items.Clear();
      TvdbEpisode ep;
      try
      {
        ep = m_tvdbHandler.GetEpisode(sId, season, episode,
                                                 (TvdbEpisode.EpisodeOrdering)cbOrdering.SelectedItem,
                                                 TvdbLanguage.DefaultLanguage);
        lvSeries.Items.Add(CreateItem("Series Id", ep.SeriesId.ToString()));
        lvSeries.Items.Add(CreateItem("Episode Id", ep.Id.ToString()));
        lvSeries.Items.Add(CreateItem("Name", ep.EpisodeName));
        lvSeries.Items.Add(CreateItem("Gueststars", ep.GuestStarsString));
        lvSeries.Items.Add(CreateItem("Directors", ep.DirectorsString));
        lvSeries.Items.Add(CreateItem("Writer", ep.WriterString));
        lvSeries.Items.Add(CreateItem("Overview", ep.Overview));
        lvSeries.Items.Add(CreateItem("Imdb Id", ep.ImdbId));

      }
      catch (TvdbContentNotFoundException ex)
      {
        MessageBox.Show("Couldn't find the specified episode");
      }

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

    private void cmdShowEpisodesInGivenOrder_Click(object sender, EventArgs e)
    {
      TvdbSeries s = m_tvdbHandler.GetSeries(Int32.Parse(txtSeriesIdEpisodeOrdering.Text), TvdbLanguage.DefaultLanguage, true, true, true, true);

      if (rbAired.Checked)
      {
        List<TvdbEpisode> eps = s.GetEpisodes(1, TvdbEpisode.EpisodeOrdering.DefaultOrder);
        lvSeries.Items.Clear();
        eps.ForEach(delegate(TvdbEpisode ep) { lvSeries.Items.Add(CreateItem(ep.EpisodeNumber.ToString() + "(" + ep.DvdEpisodeNumber + ")", ep.EpisodeName)); });
      }
      else if (rbDvd.Checked)
      {
        List<TvdbEpisode> eps = s.GetEpisodes(1, TvdbEpisode.EpisodeOrdering.DvdOrder);
        lvSeries.Items.Clear();
        eps.ForEach(delegate(TvdbEpisode ep) { lvSeries.Items.Add(CreateItem(ep.DvdEpisodeNumber.ToString() + "(" + ep.EpisodeNumber + ")", ep.EpisodeName)); });
      }

    }

    private void cmdShowEpisodesAbsoluteOrdering_Click(object sender, EventArgs e)
    {
      TvdbSeries s = m_tvdbHandler.GetSeries(Int32.Parse(txtSeriesIdEpisodeOrdering.Text), TvdbLanguage.DefaultLanguage, true, true, true, true);
      List<TvdbEpisode> eps = s.GetEpisodesAbsoluteOrder();
      lvSeries.Items.Clear();
      eps.ForEach(delegate(TvdbEpisode ep) { lvSeries.Items.Add(CreateItem(ep.AbsoluteNumber + "(S" + ep.SeasonNumber + "E" + ep.EpisodeNumber + ")", ep.EpisodeName)); });

    }

    private void cmdTestGetSeriesByExternalId_Click(object sender, EventArgs e)
    {
      TvdbDownloader downloader = new TvdbDownloader(File.ReadAllText("api_key.txt"));
      TvdbSearchResult s = downloader.DownloadSerieByExternalId(TvdbLibInfo.ExternalId.ImdbId, txtExternalId.Text);
    }
  }
}
