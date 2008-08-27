using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TvdbConnector;
using TvdbConnector.Cache;
using TvdbTester.Properties;
using TvdbConnector.Data;
using TvdbConnector.Data.Banner;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;

namespace TvdbTester
{
  public partial class TestForm : Form
  {
    public TestForm()
    {
      InitializeComponent();
    }

    private Tvdb m_tvdbHandler;

    private void cmdInit_Click(object sender, EventArgs e)
    {
      m_tvdbHandler = new Tvdb(new XmlCacheProvider("C:\\test"), Resources.API_KEY);
      List<TvdbLanguage> lang = m_tvdbHandler.Languages;
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
      Tvdb tvdb = new Tvdb(new BinaryCacheProvider("C:\\test"), Resources.API_KEY);
      List<TvdbLanguage> lang = tvdb.Languages;
      TvdbSeries s = tvdb.GetSeries(Int32.Parse(txtSeriesId.Text), TvdbLanguage.DefaultLanguage, true, true, true, true);

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


  }
}
