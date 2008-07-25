using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TvdbConnector.Data.Banner;
using TvdbTester.Properties;
using System.Threading;

namespace TvdbTester
{
  public partial class PosterControl : UserControl
  {
    private List<TvdbPosterBanner> m_imageList;
    int m_index = 0;


    public PosterControl()
    {
      InitializeComponent();
    }

    [Description("My Description")]
    public List<TvdbPosterBanner> PosterImages
    {
      set
      {
        m_imageList = value;
        if (m_imageList != null)
        {
          if (m_imageList.Count > 0)
          {
            m_index = 0;
            SetPosterImage(value[0]);
          }
          if (m_imageList.Count <= 1)
          {
            panelLeft.Visible = false;
            panelRight.Visible = false;
          }
          else
          {
            panelLeft.Visible = true;
            panelRight.Visible = true;
          }
        }
        else
        {
          panelImage.BackgroundImage = null;
          panelLeft.Visible = false;
          panelRight.Visible = false;
        }

      }
    }

    delegate void SetImageThreadSafeDelegate(Image _image);
    void SetImageThreadSafe(Image _image)
    {
      if (!InvokeRequired)
      {
        try
        {
          panelImage.BackgroundImage = _image;
          //ReloadBitmaps();
          //pbList[_index].Image = _img;
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.ToString());
        }
      }
      else
        Invoke(new SetImageThreadSafeDelegate(SetImageThreadSafe), new object[] { _image });
    }

    delegate void SetLoadingVisibleThreadSafeDelegate(bool _visible);
    void SetLoadingVisibleThreadSafe(bool _visible)
    {
      if (!InvokeRequired)
      {
        try
        {
          pbLoadingScreen.Visible = _visible;
          //ReloadBitmaps();
          //pbList[_index].Image = _img;
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.ToString());
        }
      }
      else
        Invoke(new SetLoadingVisibleThreadSafeDelegate(SetLoadingVisibleThreadSafe), new object[] { _visible });
    }

    public void ClearPoster()
    {
      panelImage.BackgroundImage = null;
    }

    private void DoPosterLoad(object _param)
    {
      TvdbPosterBanner banner = (TvdbPosterBanner)_param;

      int index = m_index;
      if (!banner.IsLoaded)
      {
        SetImageThreadSafe(null);
        SetLoadingVisibleThreadSafe(true);
        banner.LoadBanner();
      }

      if (banner.IsLoaded && index == m_index)
      {//the current index is still (event after downloading the image) the images' index
        SetLoadingVisibleThreadSafe(false);
        SetImageThreadSafe(CreatePosterBitmap(banner.Banner));
      }

    }

    private void SetPosterImage(TvdbPosterBanner _value)
    {
      new Thread(new ParameterizedThreadStart(DoPosterLoad)).Start(_value);

    }

    private Bitmap CreatePosterBitmap(Image _image)
    {
      Bitmap bm = new Bitmap(this.Width, this.Height);
      Graphics g = Graphics.FromImage(bm);
      g.Clear(Color.Transparent);
      //g.DrawImage(value.Banner, new RectangleF(
      g.DrawImage(_image, new RectangleF((float)(bm.Width * 0.15), (float)(bm.Height * 0.07),
                                               (float)(bm.Width * 0.8), (float)(bm.Height * 0.85)));
      return bm;
    }

    private void panelRight_MouseClick(object sender, MouseEventArgs e)
    {
      if (m_imageList != null && m_imageList.Count != 0)
      {
        if (m_index < m_imageList.Count - 1)
        {
          m_index++;
          SetPosterImage(m_imageList[m_index]);
        }
        else
        {
          m_index = 0;
          SetPosterImage(m_imageList[m_index]);
        }
      }
    }

    private void panelLeft_MouseClick(object sender, MouseEventArgs e)
    {
      if (m_imageList != null && m_imageList.Count != 0)
      {
        if (m_index > 0)
        {
          m_index--;
          SetPosterImage(m_imageList[m_index]);
        }
        else
        {
          m_index = m_imageList.Count - 1;
          SetPosterImage(m_imageList[m_index]);
        }
      }
    }
  }
}
