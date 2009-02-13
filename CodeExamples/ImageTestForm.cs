using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TvdbLib;
using TvdbLib.Data;
using TvdbLib.Cache;

namespace WikiCodeExamples
{
  public partial class ImageTestForm : Form
  {
    private TvdbSeries m_selectedSeriesForBannerTesting = null;
    private int m_bannerTestingIndex = 0;
    private TvdbHandler m_tvdbHandler;

    public ImageTestForm()
    {
      InitializeComponent();
    }

    private void cmdInit_Click(object sender, EventArgs e)
    {
      m_tvdbHandler = new TvdbHandler(new XmlCacheProvider("C:\\test"), "");
      m_tvdbHandler.InitCache();
      cmdEnd.Enabled = true;
  }

    private void cmdEnd_Click(object sender, EventArgs e)
    {
      m_tvdbHandler.SaveCache();
    }

    private void LoadBanner(int _index)
    {
      if (rbLoadFanart.Checked)
      {
        if (m_selectedSeriesForBannerTesting.FanartBanners != null && m_selectedSeriesForBannerTesting.FanartBanners.Count > _index)
        {
          if (m_selectedSeriesForBannerTesting.FanartBanners[_index].IsLoaded || m_selectedSeriesForBannerTesting.FanartBanners[_index].LoadBanner())
          {
            pbBannerTesting.Image = m_selectedSeriesForBannerTesting.FanartBanners[_index].BannerImage;
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
            pbBannerTesting.Image = m_selectedSeriesForBannerTesting.FanartBanners[_index].ThumbImage;
          }
        }
        else
        {
          MessageBox.Show("No fanart for this series");
        }
      }
      else if (rbFanartVignette.Checked)
      {
        if (m_selectedSeriesForBannerTesting.FanartBanners != null && m_selectedSeriesForBannerTesting.FanartBanners.Count > _index)
        {
          if (m_selectedSeriesForBannerTesting.FanartBanners[_index].IsVignetteLoaded || m_selectedSeriesForBannerTesting.FanartBanners[_index].LoadVignette())
          {
            pbBannerTesting.Image = m_selectedSeriesForBannerTesting.FanartBanners[_index].VignetteImage;
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
            pbBannerTesting.Image = m_selectedSeriesForBannerTesting.SeriesBanners[_index].BannerImage;
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
            pbBannerTesting.Image = m_selectedSeriesForBannerTesting.SeriesBanners[_index].ThumbImage;
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
            pbBannerTesting.Image = m_selectedSeriesForBannerTesting.PosterBanners[_index].BannerImage;
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
            pbBannerTesting.Image = m_selectedSeriesForBannerTesting.PosterBanners[_index].ThumbImage;
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
            pbBannerTesting.Image = m_selectedSeriesForBannerTesting.SeasonBanners[_index].BannerImage;
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
            pbBannerTesting.Image = m_selectedSeriesForBannerTesting.SeasonBanners[_index].ThumbImage;
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
            pbBannerTesting.Image = m_selectedSeriesForBannerTesting.Episodes[_index].Banner.BannerImage;
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
            pbBannerTesting.Image = m_selectedSeriesForBannerTesting.Episodes[_index].Banner.ThumbImage;
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
      m_bannerTestingIndex++;
      LoadBanner(m_bannerTestingIndex);
    }

    private void cmdBannerTestingPrev_Click(object sender, EventArgs e)
    {
      m_bannerTestingIndex++;
      LoadBanner(m_bannerTestingIndex);
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
  }
}
