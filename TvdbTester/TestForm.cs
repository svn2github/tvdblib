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

namespace TvdbTester
{
  public partial class TestForm : Form
  {
    public TestForm()
    {
      InitializeComponent();
    }

    private void cmdTest1_Click(object sender, EventArgs e)
    {
      Tvdb tvdb = new Tvdb(new XmlCacheProvider("C:\\test"), Resources.API_KEY);
      List<TvdbLanguage> lang = tvdb.Languages;
      TvdbSeries s = tvdb.GetSeries(73255, TvdbLanguage.DefaultLanguage, true, true);
      bannerControl1.BannerImages = s.Banners;
      tvdb.SaveCache();
    }
  }
}
