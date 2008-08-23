using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TvdbConnector.Data;
using System.Threading;
using TvdbTester.Properties;

namespace TvdbTester
{
  public partial class BannerControl : UserControl
  {
    public BannerControl()
    {
      InitializeComponent();
    }
    private List<TvdbBanner> m_imageList;
    private int m_index = 0;
    private Image m_defaultImage;

    private Color m_loadingBackgroundColor = Color.Transparent;

    public delegate void IndexChangedHandler(EventArgs _event);
    public event IndexChangedHandler IndexChanged;

    [Description("Background color when loading an image")]
    public Color LoadingBackgroundColor
    {
      get { return m_loadingBackgroundColor; }
      set
      {
        m_loadingBackgroundColor = value;
      }
    }

    public Image LoadingImage
    {
      get { return pbLoading.Image; }
      set { pbLoading.Image = value; }
    }

    [Description("List of banner images")]
    public List<TvdbBanner> BannerImages
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
            cmdLeft.Visible = false;
            cmdRight.Visible = false;
          }
          else
          {
            cmdLeft.Visible = false;
            cmdRight.Visible = true;
          }
        }
        else
        {
          panelImage.BackgroundImage = null;
          cmdLeft.Visible = false;
          cmdRight.Visible = false;
        }
      }
      get { return m_imageList; }
    }

    public int Index
    {
      get { return m_index; }
      set
      {
        if (m_index > 0 && m_index < m_imageList.Count)
        {
          m_index = value;
          SetPosterImage(m_imageList[value]);
          if (IndexChanged != null) IndexChanged(new EventArgs());
        }
      }
    }

    public TvdbBanner BannerImage
    {
      set
      {
        if (value != null)
        {
          List<TvdbBanner> list = new List<TvdbBanner>();
          list.Add(value);
          BannerImages = list;
        }
        else
        {
          SetImageThreadSafe(null);
        }
      }
      get { return (m_imageList != null && m_imageList.Count > 0) ? m_imageList[0] : null; }
    }

    [Description("Default Image shown when no control has no banners")]
    public Image DefaultImage
    {
      get { return m_defaultImage; }
      set
      {
        panelImage.BackgroundImage = value;
        m_defaultImage = value;
      }
    }

    /// <summary>
    /// Layout of the shown image
    /// </summary>
    public ImageLayout ImageSizeMode
    {
      get { return panelImage.BackgroundImageLayout; }
      set { panelImage.BackgroundImageLayout = value; }
    }

    /// <summary>
    /// The currently show fullscreen image
    /// </summary>
    public Image ActiveImage
    {
      get
      {
        if (m_imageList == null || m_imageList.Count == 0 || !m_imageList[m_index].IsLoaded)
        {
          return null;
        }
        return panelImage.BackgroundImage;
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
          pbLoading.Visible = _visible;
          if (_visible)
          {
            this.BackColor = m_loadingBackgroundColor;
            panelImage.BackColor = m_loadingBackgroundColor;
          }
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.ToString());
        }
      }
      else
        Invoke(new SetLoadingVisibleThreadSafeDelegate(SetLoadingVisibleThreadSafe), new object[] { _visible });
    }

    /// <summary>
    /// Clears the banner images
    /// </summary>
    public void ClearBanner()
    {
      m_imageList = null;
      m_index = 0;
      panelImage.BackgroundImage = m_defaultImage;
      cmdLeft.Visible = false;
      cmdRight.Visible = false;
    }

    private void DoPosterLoad(object _param)
    {
      TvdbBanner banner = (TvdbBanner)_param;

      int index = m_index;
      if (!banner.IsLoaded)
      {
        SetImageThreadSafe(null);
        SetLoadingVisibleThreadSafe(true);
        banner.LoadBanner();
      }
      if (index == m_index)
      {//the current index is still (event after downloading the image) the images' index
        //todo: check if another image has been loaded while the image has been downloaded
        if (banner.IsLoaded)
        {//banner was successfully loaded
          SetLoadingVisibleThreadSafe(false);
          SetImageThreadSafe(banner.Banner);
        }
        else
        {//couldn't load the banner
          SetLoadingVisibleThreadSafe(false);
        }
      }
    }

    private void SetPosterImage(TvdbBanner _value)
    {
      new Thread(new ParameterizedThreadStart(DoPosterLoad)).Start(_value);
    }


    /// <summary>
    /// Select the previous image
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void cmdLeft_Click(object sender, EventArgs e)
    {
      if (m_imageList != null && m_imageList.Count != 0)
      {
        cmdRight.Visible = true;
        m_index--;
        if (IndexChanged != null) IndexChanged(new EventArgs());
        SetPosterImage(m_imageList[m_index]);
        if (m_index <= 0)
        {
          cmdLeft.Visible = false;
        }
      }
    }

    /// <summary>
    /// Select the next image
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void cmdRight_Click(object sender, EventArgs e)
    {
      if (m_imageList != null && m_imageList.Count != 0)
      {
        cmdLeft.Visible = true;
        m_index++;
        if (IndexChanged != null) IndexChanged(new EventArgs());
        SetPosterImage(m_imageList[m_index]);
        if (m_index >= m_imageList.Count - 1)
        {
          cmdRight.Visible = false;
        }
      }
    }

    /// <summary>
    /// Size changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BannerControl_SizeChanged(object sender, EventArgs e)
    {
      pbLoading.Left = (this.Width / 2) - (pbLoading.Width / 2);
      pbLoading.Top = (this.Height / 2) - (pbLoading.Height / 2);
      cmdLeft.Top = 0;
      cmdLeft.Left = 0;
      cmdRight.Top = 0;
      cmdRight.Left = this.Width - cmdRight.Width;
    }
  }
}
