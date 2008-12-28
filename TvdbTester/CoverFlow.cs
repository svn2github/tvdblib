using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TvdbLib.Data.Banner;
using TvdbTester.Properties;
using System.Threading;

namespace TvdbTester
{
  public partial class CoverFlow : UserControl
  {
    private int m_currentIndex;
    private List<PictureBox> pbList;
    private List<TvdbFanartBanner> m_items;

    /// <summary>
    /// Banners to be shown at the control
    /// </summary>
    public List<TvdbFanartBanner> Items
    {
      get { return m_items; }
      set
      {
        m_items = value;
        if (m_items == null) return;
        m_currentIndex = -3;

        for (int i = 3; i < 3 + m_items.Count; i++)
        {
          if (i < pbList.Count)
          {
            pbList[i].Image = Resources.loading;
            pbList[i].SizeMode = PictureBoxSizeMode.Zoom;
          }
        }

        new Thread(new ThreadStart(DoLoading)).Start();
      }
    }

    /// <summary>
    /// The currently show fullscreen image
    /// </summary>
    public Image ActiveImage
    {
      get
      {
        if (m_items == null || m_items.Count == 0)
        {
          return null;
        }
        return pbFull.Image;
      }
    }

    /// <summary>
    /// Clear all items
    /// </summary>
    public void Clear()
    {
      for (int i = 0; i < pbList.Count; i++)
      {
        pbList[i].Image = null;
      }
      if (m_items != null) m_items.Clear();
      pbFull.Image = null;
      this.Tag = null;
    }


    /// <summary>
    /// Thread for Loading the images
    /// </summary>
    private void DoLoading()
    {
      for (int i = 0; i < m_items.Count; i++)
      {
        if (m_items[i].IsThumbLoaded || m_items[i].LoadThumb())
        {
          if (3 + i + m_currentIndex >= 0 && i + m_currentIndex <= 0 && i < pbList.Count)
          {//current index is visible
            SetImageThreadSafe(3 + i, m_items[3 + i + m_currentIndex].BannerThumb);
          }
        }
      }
    }

    /// <summary>
    /// Set the Image on the panel 
    /// </summary>
    /// <param name="_index"></param>
    /// <param name="_img"></param>
    delegate void SetImageThreadSafeDelegate(int _index, Image _img);
    void SetImageThreadSafe(int _index, Image _img)
    {
      if (!InvokeRequired)
      {
        try
        {
          if (_index > 0 && _index <= 6)
          {
            pbList[_index].Image = _img;
          }
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.ToString());
        }
      }
      else
        Invoke(new SetImageThreadSafeDelegate(SetImageThreadSafe), new object[] { _index, _img });
    }

    public int CurrentIndex
    {
      get { return m_currentIndex; }
      set
      {
        m_currentIndex = value;
      }
    }

    public CoverFlow()
    {
      InitializeComponent();
      pbList = new List<PictureBox>();
      pbList.Add(pb0);
      pbList.Add(pb1);
      pbList.Add(pb2);
      pbList.Add(pb3);
      pbList.Add(pb4);
      pbList.Add(pb5);
      pbList.Add(pb6);

      pnlThumbOverview.MouseWheel += new MouseEventHandler(pnlThumbOverview_MouseWheel);
      this.MouseWheel += new MouseEventHandler(pnlThumbOverview_MouseWheel);
    }

    public void SetNext()
    {
      if (m_items == null) return;
      if (m_currentIndex < m_items.Count - 4)
      {
        m_currentIndex += 1;
        ReloadBitmaps();
      }
    }

    public void SetPrevious()
    {
      if (m_items == null) return;
      if (m_currentIndex >= -2)
      {
        m_currentIndex--;
        ReloadBitmaps();
      }
    }

    void pnlThumbOverview_MouseWheel(object sender, MouseEventArgs e)
    {
      Console.WriteLine(e.Delta);
      //throw new NotImplementedException();
    }

    private void  ReloadBitmaps()
    {
      if (m_items == null) return;
      for (int i = 0; i < pbList.Count; i++)
      {
        if (i + m_currentIndex >= 0 && i + m_currentIndex < m_items.Count)
        {//panel has a image on it
          int imageIndex = m_currentIndex + i;
          if (i == 3)
          {//the middle image -> set rating here -> not supported by tvdb :(
            //int rating = m_items[imageIndex].rat
          }
          if (m_items[imageIndex].IsThumbLoaded)
          {
            pbList[i].SizeMode = PictureBoxSizeMode.Zoom;
            pbList[i].Image = m_items[imageIndex].BannerThumb;
          }
          else if (pbList[i].Image == null)
          {
            pbList[i].SizeMode = PictureBoxSizeMode.Zoom;
            pbList[i].Image = Resources.loading;
          }
        }
        else
        {
          pbList[i].Image = null;
        }
      }

    }

    private void pb0_Click(object sender, EventArgs e)
    {
      if (m_items == null) return;
      if (m_currentIndex >= 0)
      {
        m_currentIndex -= 3;
        ReloadBitmaps();
      }
    }

    private void pb1_Click(object sender, EventArgs e)
    {
      if (m_items == null) return;
      if (m_currentIndex >= -1)
      {
        m_currentIndex -= 2;
        ReloadBitmaps();
      }
    }
    private void pb2_Click(object sender, EventArgs e)
    {
      if (m_items == null) return;
      if (m_currentIndex >= -2)
      {
        m_currentIndex -= 1;
        ReloadBitmaps();
      }
    }

    private void pb4_Click(object sender, EventArgs e)
    {
      if (m_items == null) return;
      if (m_currentIndex < m_items.Count - 4)
      {
        m_currentIndex += 1;
        ReloadBitmaps();
      }
    }

    private void pb5_Click(object sender, EventArgs e)
    {
      if (m_items == null) return;
      if (m_currentIndex < m_items.Count -5)
      {
        m_currentIndex += 2;
        ReloadBitmaps();
      }
    }

    private void pb6_Click(object sender, EventArgs e)
    {
      if (m_items == null) return;
      if (m_currentIndex < m_items.Count - 6)
      {
        m_currentIndex += 3;
        ReloadBitmaps();
      }
    }

    private void pb3_Click(object sender, EventArgs e)
    {
      if (m_items == null) return;
      TvdbFanartBanner banner = m_items[m_currentIndex + 3];
      if (banner.IsLoaded || banner.LoadBanner())
      {
        pbFull.Image = banner.Banner;
      }
      else
      {
        pbFull.Image = pbFull.ErrorImage;
      }
    }

    private void pnlThumbOverview_MouseClick(object sender, MouseEventArgs e)
    {

    }

    private void CoverFlow_SizeChanged(object sender, EventArgs e)
    {
      Console.Write("x");
      int oldPos = 20;
      int widthFactor = (this.Width-30) / 16;

      for (int i = 0; i <= (pbList.Count-1)/2; i++)
      {//loop until the middle panel
        pbList[i].Left = oldPos;
        pbList[i].Width = widthFactor * (i + 1);
        oldPos = oldPos + pbList[i].Width;
      }

      for (int i = (pbList.Count - 1) / 2 + 1; i < pbList.Count; i++)
      {//loop from the end until after the middle panel
        pbList[i].Left = oldPos;
        pbList[i].Width = pbList[pbList.Count - i -1].Width ;
        oldPos = oldPos + pbList[i].Width;
      }
    }




  }
}
