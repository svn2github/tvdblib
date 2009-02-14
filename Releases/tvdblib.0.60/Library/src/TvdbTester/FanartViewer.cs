using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TvdbTester.Properties;
using TvdbLib;
using TvdbLib.Cache;
using TvdbLib.Data;
using TvdbLib.Data.Banner;

namespace TvdbTester
{
  public partial class FanartViewer : Form
  {
    private TvdbHandler m_tvdbHandler;
    TvdbSeries m_series;
    public FanartViewer()
    {
      InitializeComponent();
    }

    private void cmdInit_Click(object sender, EventArgs e)
    {
      m_tvdbHandler = new TvdbHandler(new XmlCacheProvider("C:\\test"), Resources.API_KEY);
      m_series = m_tvdbHandler.GetBasicSeries(72129, TvdbLanguage.DefaultLanguage, true);
      int count = 0;
      fanartControl1.NumberOfImages = m_series.FanartBanners.Count;
      foreach (TvdbFanartBanner fb in m_series.FanartBanners)
      {
        if(fb.IsThumbLoaded || fb.LoadThumb())
        {
          fanartControl1.SetImage(fb, count);
          count++;
        }
      }
    }

    private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
    {
      fanartControl1.Position = hScrollBar1.Value;
    }

    private void fanartControl1_ImageClicked(EventArgs _event)
    {
      if(m_series != null)
      {
        bannerControl1.BannerImage = m_series.FanartBanners[fanartControl1.SelectedIndex];
      }
    }
  }
}
