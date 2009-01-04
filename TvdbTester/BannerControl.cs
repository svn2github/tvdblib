using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TvdbLib.Data;
using System.Threading;
using TvdbTester.Properties;
using TvdbLib.Data.Banner;

namespace TvdbTester
{
  public partial class BannerControl : UserControl
  {
    #region private fields
    private List<TvdbBanner> m_imageList;
    private int m_index = 0;
    private Image m_defaultImage;
    private Size m_buttonSize;
    private Thread m_latestLoadingThread; //thread that loads the LATEST image (which should be shown after loading)
    private Color m_loadingBackgroundColor = Color.Transparent;
    private Image m_unavailableImage;
    #endregion

    public delegate void IndexChangedHandler(EventArgs _event);
    public event IndexChangedHandler IndexChanged;


    public BannerControl()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Background color when an image is loading
    /// </summary>
    [Description("Background color when loading an image")]
    public Color LoadingBackgroundColor
    {
      get { return m_loadingBackgroundColor; }
      set
      {
        m_loadingBackgroundColor = value;
      }
    }

    /// <summary>
    /// Loading image that is shown when the image is currently loading
    /// </summary>
    public Image LoadingImage
    {
      get { return pbLoading.Image; }
      set { pbLoading.Image = value; }
    }

    /// <summary>
    /// Image that is shown when the banner has no image available
    /// </summary>
    [Description("Image that is shown when the banner has no image available")]
    public Image UnavailableImage
    {
      get { return m_unavailableImage; }
      set { m_unavailableImage = value; }
    }

    /// <summary>
    /// List of images for this control
    /// </summary>
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
            SetBannerImage(value[0]);
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

    /// <summary>
    /// Set the banner for the control
    /// </summary>
    public TvdbBanner BannerImage
    {
      set
      {
        if (value != null)
        {
          //if (value == null) value = new TvdbBanner();
          List<TvdbBanner> list = new List<TvdbBanner>();
          list.Add(value);
          BannerImages = list;
        }
      }
      get { return (m_imageList != null && m_imageList.Count > 0) ? m_imageList[0] : null; }
    }

    /// <summary>
    /// Currently active index
    /// </summary>
    public int Index
    {
      get { return m_index; }
      set
      {
        if (m_imageList != null && m_index >= 0 && m_index < m_imageList.Count)
        {
          m_index = value;
          SetBannerImage(m_imageList[value]);
          if (IndexChanged != null) IndexChanged(new EventArgs());

          if (m_index >= m_imageList.Count - 1)
          {
            cmdRight.Visible = false;
          }
          else
          {
            cmdRight.Visible = true;
          }

          if (m_index <= 0)
          {
            cmdLeft.Visible = false;
          }
          else
          {
            cmdLeft.Visible = true;
          }
        }
      }
    }



    /// <summary>
    /// The default image which is shown if no banners are set
    /// </summary>
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
        if (m_imageList == null || m_imageList.Count == 0)
        {
          return null;
        }
        else
        {
          if (m_useThumbIfPossible && m_imageList[m_index].GetType().BaseType == typeof(TvdbBannerWithThumb))
          {
            if (((TvdbBannerWithThumb)m_imageList[m_index]).IsThumbLoaded) return ((TvdbBannerWithThumb)m_imageList[m_index]).BannerThumb;
          }
          else
          {
            if (m_imageList[m_index].IsLoaded) return m_imageList[m_index].Banner;
          }

          return null;
        }
        
      }
    }

    #region threadsafe operations
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
    #endregion

    /// <summary>
    /// Clears the banner images
    /// </summary>
    public void ClearControl()
    {
      m_imageList = null;
      m_index = 0;
      panelImage.BackgroundImage = m_defaultImage;
      cmdLeft.Visible = false;
      cmdRight.Visible = false;
      pbLoading.Visible = false;
    }

    private bool m_useThumbIfPossible;

    /// <summary>
    /// Will use the thumbnail of the image
    /// </summary>
    [Description("Will use the thumbnail of the image if a thumbnail is available")]
    public bool UseThumb
    {
      get { return m_useThumbIfPossible; }
      set { m_useThumbIfPossible = value; }
    }

    /// <summary>
    /// Do a banner load within it's own thread
    /// </summary>
    /// <param name="_param"></param>
    private void DoBannerLoad(object _param)
    {
      TvdbBanner banner = (TvdbBanner)_param;
      try
      {
        if (banner.BannerPath != null && !banner.BannerPath.Equals(""))
        {
          int index = m_index;
          //the basetype of the banner is TvdbBannerWithThumb, not TvdbBanner (only poster atm)
          bool hasThumb = banner.GetType().BaseType == typeof(TvdbBannerWithThumb);
          if (m_useThumbIfPossible && hasThumb)
          {
            if (!((TvdbBannerWithThumb)banner).IsThumbLoaded)
            {
              SetImageThreadSafe(null);
              SetLoadingVisibleThreadSafe(true);
              ((TvdbBannerWithThumb)banner).LoadThumb();
            }
          }
          else
          {
            if (!banner.IsLoaded)
            {
              SetImageThreadSafe(null);
              SetLoadingVisibleThreadSafe(true);
              banner.LoadBanner();
            }
          }

          lock (m_latestLoadingThread)
          {
            if (Thread.CurrentThread == m_latestLoadingThread)
            {
              //Console.WriteLine("Loading finished of " + banner.Id);
              if (m_useThumbIfPossible && hasThumb && ((TvdbBannerWithThumb)banner).IsThumbLoaded)
              {
                SetLoadingVisibleThreadSafe(false);
                SetImageThreadSafe(((TvdbBannerWithThumb)banner).BannerThumb);
              }
              else if (banner.IsLoaded)
              {//banner was successfully loaded
                SetLoadingVisibleThreadSafe(false);
                SetImageThreadSafe(banner.Banner);
              }
              else
              {//couldn't load the banner
                SetLoadingVisibleThreadSafe(false);
              }
            }
            else
            {
              //Console.WriteLine("Didn't load " + banner.Id + " because it's not the latest image");
            }
          }
        }
        else
        {//no banner information available -> use default image if there is one
          SetLoadingVisibleThreadSafe(false);
          SetImageThreadSafe(m_unavailableImage);
        }
      }
      catch (ThreadAbortException)
      {
        //Console.WriteLine("Bannercontrol aborted loading");
      }
    }

    /// <summary>
    /// Set the new banner image
    /// </summary>
    /// <param name="_value"></param>
    private void SetBannerImage(TvdbBanner _value)
    {
      Thread imageLoader = new Thread(new ParameterizedThreadStart(DoBannerLoad));
      m_latestLoadingThread = imageLoader;
      imageLoader.Priority = ThreadPriority.Lowest;
      imageLoader.Name = "Imageloader_" + _value.BannerPath;
      imageLoader.Start(_value);

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
        SetBannerImage(m_imageList[m_index]);
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
        SetBannerImage(m_imageList[m_index]);
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

    /// <summary>
    /// panel image size changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void panelImage_SizeChanged(object sender, EventArgs e)
    {
      m_buttonSize = cmdRight.Size;
    }

    /// <summary>
    /// The right button was pressed down
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void cmdRight_MouseDown(object sender, MouseEventArgs e)
    {
      cmdRight.Size = new Size(m_buttonSize.Width - 1, m_buttonSize.Height - 1);
    }

    /// <summary>
    /// The right button was released
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void cmdRight_MouseUp(object sender, MouseEventArgs e)
    {
      cmdRight.Size = m_buttonSize;
    }

    /// <summary>
    /// The left button was pressed down
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void cmdLeft_MouseDown(object sender, MouseEventArgs e)
    {
      cmdLeft.Size = new Size(m_buttonSize.Width - 1, m_buttonSize.Height - 1);
    }

    /// <summary>
    /// The left button was released
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void cmdLeft_MouseUp(object sender, MouseEventArgs e)
    {
      cmdLeft.Size = m_buttonSize;
    }
  }
}
