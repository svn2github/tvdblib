using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using TvdbLib.Data;
using TvdbTester.Properties;
using TvdbLib.Data.Banner;

namespace TvdbTester
{
  public partial class FanartControl : UserControl
  {
    /// <summary>
    /// Control for showing images in a carousel
    /// </summary>
    private class FanartControlImage : IComparable
    {
      #region private properties
      private int m_index;
      private Rectangle m_rectangle;//position of image;
      private TvdbBanner m_image;//image to draw
      private TvdbBanner m_framedImage;
      private bool m_isLoading = false;

      private int m_degree;
      private Point m_position;
      #endregion

      internal FanartControlImage()
      {

      }

      /// <summary>
      /// Gets or sets a value indicating whether this instance is loading.
      /// </summary>
      /// <value>
      /// 	<c>true</c> if this instance is loading; otherwise, <c>false</c>.
      /// </value>
      public bool IsLoading
      {
        get { return m_isLoading; }
        set { m_isLoading = value; }
      }

      internal FanartControlImage(int _index)
        : this()
      {
        m_index = _index;
      }

      /// <summary>
      /// Gets or sets the index.
      /// </summary>
      /// <value>The index.</value>
      public int Index
      {
        get { return m_index; }
        set { m_index = value; }
      }

      /// <summary>
      /// Image to draw
      /// </summary>
      public TvdbBanner Image
      {
        get { return m_image; }
        set { m_image = value; }
      }

      private int m_loadingIndex;

      /// <summary>
      /// Gets or sets the index of the loading.
      /// </summary>
      /// <value>The index of the loading.</value>
      public int LoadingIndex
      {
        get { return m_loadingIndex; }
        set { m_loadingIndex = value; }
      }


      /// <summary>
      /// Rectangle describing the position of the image
      /// </summary>
      public Rectangle Rectangle
      {
        get { return m_rectangle; }
        set { m_rectangle = value; }
      }

      public Image CreateFramedImage()
      {
        if (Image.GetType() == typeof(TvdbFanartBanner))
        {
          if (((TvdbFanartBanner)Image).IsThumbLoaded)
          {
            return ((TvdbFanartBanner)Image).BannerThumb;
          }
        }
        else if (Image.IsLoaded)
        {
          return m_image.Banner;
        }
        return Resources.loader4;//todo: dummy image
      }

      public Point Position
      {
        get { return m_position; }
        set { m_position = value; }
      }

      public int Degree
      {
        get { return m_degree; }
        set { m_degree = value; }
      }

      #region IComparable Members

      public int CompareTo(object obj)
      {
        FanartControlImage image = (FanartControlImage)obj;
        return this.Position.Y.CompareTo(image.Position.Y);
      }

      #endregion
    }

    private class GifImage
    {
      private Image m_gifImage;
      private FrameDimension dimension;
      private int frameCount;
      private int currentFrame;
      private bool reverse;
      private int step = 1;

      internal GifImage(string path)
        : this(Image.FromFile(path))
      {
      }

      internal GifImage(Image _img)
      {
        m_gifImage = _img;
        dimension = new FrameDimension(m_gifImage.FrameDimensionsList[0]); //gets the GUID
        frameCount = m_gifImage.GetFrameCount(dimension); //total frames in the animation
      }

      public bool ReverseAtEnd //whether the gif should play backwards when it reaches the end
      {
        get { return reverse; }
        set { reverse = value; }
      }

      public Image GetNextFrame()
      {

        currentFrame += step;

        //if the animation reaches a boundary...
        if (currentFrame >= frameCount || currentFrame < 1)
        {
          if (reverse)
          {
            step *= -1; //...reverse the count
            currentFrame += step; //apply it
          }
          else
            currentFrame = 0; //...or start over
        }
        return GetFrame(currentFrame);
      }

      public Image GetFrame(int index)
      {
        m_gifImage.SelectActiveFrame(dimension, index); //find the frame
        return (Image)m_gifImage.Clone(); //return a copy of it
      }
    }

    private const double GROWING_MULTIPLIER = 0.5;

    public delegate void ImageClickedEventHandler(EventArgs _event);
    public event ImageClickedEventHandler ImageClicked;
    private int m_selectedIndex;

    private Dictionary<int, PointF> m_elipse;
    private Point m_thumbSize = new Point(80, 60);
    private int m_numberOfImages = 6;
    private int m_position = 0; //0 to 360
    private List<FanartControlImage> m_imagePositions = new List<FanartControlImage>();
    private Color m_frameColor = Color.Gray;
    public Color FrameColor
    {
      get { return m_frameColor; }
      set { m_frameColor = value; }
    }
    public int SelectedIndex
    {
      get { return m_selectedIndex; }
      set { m_selectedIndex = value; }
    }

    public int Position
    {
      get { return m_position; }
      set
      {
        UpdateRectanglePositions(value);
        //UpdateElipseRect();
        Invalidate();
      }
    }

    public int NumberOfImages
    {
      get { return m_numberOfImages; }
      set
      {
        m_numberOfImages = value;
        UpdateRectanglePositions(0);
      }
    }

    public void SetImage(TvdbBanner _banner, int _position)
    {
      if (_position < m_imagePositions.Count)
      {
        m_imagePositions[_position].Image = _banner;
      }
    }

    private Rectangle m_elipseRect;

    public Point ThumbSize
    {
      get { return m_thumbSize; }
      set
      {
        m_thumbSize = value;
        UpdateElipseRect();
        UpdateRectanglePositions(0);
      }
    }

    public FanartControl()
    {
      InitializeComponent();


      UpdateElipseRect();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      Draw(ClientRectangle, e.Graphics);
    }

    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
      UpdateElipseRect();
      UpdateRectanglePositions(0);
      Invalidate();
    }

    private void UpdatePosition()
    {
      if (m_mouseDownClosestDegree >= 0 && m_mouseDownClosestDegree < 360)
      {
        FanartControlImage img = m_imagePositions[0];
        img.Degree = m_mouseDownClosestDegree;
        float size = CalcSize(m_mouseDownClosestDegree);//how 
        PointF p = m_elipse[m_mouseDownClosestDegree];
        m_imagePositions[0].Rectangle = new Rectangle((int)(p.X - 100 * size),
                                                      (int)(p.Y - 50 * size),
                                                      (int)(200.0 * size),
                                                      (int)(100.0 * size));

        //m_imagePositions[0].Position.X = 10;// = (int)m_elipse[m_mouseDownClosestDegree].X;
        //m_imagePositions[0].Position.Y = 
      }
    }


    /// <summary>
    /// returns the factor on how large the object should be -> 1 = full size, 0 = hidden
    /// </summary>
    /// <param name="_degr"></param>
    /// <returns></returns>
    private float CalcSize(int _degr)
    {
      if (_degr < 0 || _degr >= 360) return 0;
      if (_degr >= 90)
      {
        _degr = 360 - _degr - 90;
      }
      else
      {
        _degr = (_degr + 90) % 180;
      }
      return ((float)_degr) / 180;
    }

    private void UpdateRectanglePositions(int _position)
    {
      if (_position < 0)
      {
        _position += 360;
      }
      if (_position >= 0 && _position < 360)
      {
        m_position = _position;
        //Console.WriteLine("Change pos to : " + _position);
        //PointF p;
        //int degr = 360 / m_numberOfImages;
        double degr = 0.0;
        int index = 1;

        //first image
        if (m_imagePositions.Count == 0) m_imagePositions.Add(new FanartControlImage(index));
        m_imagePositions[0].Degree = 0;
        Point p = new Point((int)m_elipse[0].X, (int)m_elipse[0].Y);
        m_imagePositions[0].Position = p;
        m_imagePositions[0].Rectangle = new Rectangle((p.X - m_thumbSize.X / 2),
                                            (p.Y - m_thumbSize.Y / 2),
                                            m_thumbSize.X, m_thumbSize.Y);



        for (int i = 1; i < 360; i++)
        {
          int growSize = 0;
          double growMultiplier = 1;
          if (m_imagePositions.Count <= index)
          {
            m_imagePositions.Add(new FanartControlImage(index));
          }
          FanartControlImage img = m_imagePositions[index];
          img.Degree = i;
          if (img.Degree > 0 && img.Degree <= 90)
          {//right-down portion of elipse
            //size grows
            growSize = (int)((double)img.Degree * GROWING_MULTIPLIER);
            growMultiplier = img.Degree * GROWING_MULTIPLIER / 100;
            //Console.WriteLine(i + ": " + img.Degree + "°, Growsize: " + growSize);
          }
          else if (img.Degree > 90 && img.Degree <= 180)
          {
            growSize = (int)(((double)(90 - (img.Degree - 90))) * GROWING_MULTIPLIER);
            //Console.WriteLine(i + ": " + img.Degree + "°, Growsize: " + growSize);
          }
          else if (img.Degree > 180 && img.Degree <= 270)
          {
            growSize = (int)((img.Degree - 180) * ((-1) * GROWING_MULTIPLIER));
          }
          else if (img.Degree > 270 && img.Degree <= 360)
          {
            growSize = (int)((90 - (img.Degree - 270)) * ((-1) * GROWING_MULTIPLIER));
          }
          PointF pos = m_elipse[i];
          int width = (int)((m_thumbSize.Y + m_distance) * growMultiplier);
          int height = (int)((m_thumbSize.X) * growMultiplier);

          if(!m_imagePositions[index-1].Rectangle.IntersectsWith(new Rectangle((int)(pos.X - (width/2.0)), (int)(pos.Y - (height/2.0)), width, height)))
          {//the two images don't intersect -> use this degree
            ChangeRectangleSize(growSize, img);

            m_imagePositions[index].Degree = (int)(m_position + degr);
            m_imagePositions[index].Position = new Point((int)pos.X, (int)pos.Y);
            m_imagePositions[index].Rectangle = new Rectangle((int)(pos.X - m_thumbSize.X / 2.0),
                                               (int)(pos.Y - m_thumbSize.Y / 2.0),
                                                m_thumbSize.X, m_thumbSize.Y);
          }
        }
      }
    }

    private void UpdateElipseRect()
    {
      m_elipseRect = this.ClientRectangle;
      m_elipseRect.X += m_thumbSize.X / 2 + 20;
      m_elipseRect.Width -= (m_thumbSize.X + 40);
      m_elipseRect.Height -= (m_thumbSize.Y + 40);
      m_elipseRect.Y += m_thumbSize.Y / 2 + 20;

      //Save all points on the elipse, so we don't have to compute it everytime
      m_elipse = new Dictionary<int, PointF>();
      for (int i = 0; i < 360; i++)
      {
        m_elipse.Add(i, PointFromEllipse(m_elipseRect, i));
      }
    }

    private void RectCenter(Rectangle r, out int x, out int y)
    {
      x = (r.Left + r.Right) / 2;
      y = (r.Top + r.Bottom) / 2;
    }

    private PointF PointFromEllipse(Rectangle bounds, float degrees)
    {
      float a = bounds.Width / 2.0f;
      float b = bounds.Height / 2.0f;
      float rad = ((float)Math.PI / 180.0f) * degrees;

      int xCenter, yCenter;
      RectCenter(bounds, out xCenter, out yCenter);

      float x = xCenter + (a * (float)Math.Cos(rad));
      float y = yCenter + (b * (float)Math.Sin(rad));

      return new PointF(x, y);
    }

    private void Draw(Rectangle rect, Graphics g)
    {
      //draw debug output
      if (m_imagePositions.Count > m_selectedIndex && m_selectedIndex > 0)
      {
        g.DrawString("Current: " + m_imagePositions[m_selectedIndex].Degree + ", Nearest: " + m_mouseDownClosestDegree + ", Position: " + m_position, SystemFonts.DefaultFont, SystemBrushes.HotTrack, 10, 10);
      }

      //draw the ellipse
      PointF prev = PointFromEllipse(m_elipseRect, 0);
      for (int i = 1; i < 360; i++)
      {
        PointF next = PointFromEllipse(m_elipseRect, i);
        g.DrawLine(new Pen(new SolidBrush(Color.Black), 5), prev, next);
        prev = next;
      }

      if(m_imagePositions.Count > 0)
      {
        FanartControlImage fci = m_imagePositions[0];
        if (fci.Image != null)
        {
          g.DrawImage(fci.CreateFramedImage(), fci.Rectangle);

        }
        else
        {
          g.FillRectangle(new SolidBrush(FrameColor), fci.Rectangle);
          g.DrawRectangle(new Pen(Color.Black), fci.Rectangle);
        }
      }

      //Draw a red dot representing the closest match
      if (m_mouseDownClosestDegree != -99)
      {
        g.DrawEllipse(new Pen(new SolidBrush(Color.Red)),
                      m_elipse[m_mouseDownClosestDegree].X - 5,
                      m_elipse[m_mouseDownClosestDegree].Y - 5, 10, 10);
      }

      return;
      rect.Inflate(-5, -5);
      prev = PointFromEllipse(m_elipseRect, 0);
      for (int i = 1; i < 360; i++)
      {
        PointF next = PointFromEllipse(m_elipseRect, i);
        g.DrawLine(new Pen(new SolidBrush(Color.Black), 5), prev, next);
        prev = next;
      }
      for (int i = 0; i < m_numberOfImages; i++)
      {
        //Draw the border
        if (m_imagePositions.Count > i)
        {


          //g.DrawRectangle(new Pen(new SolidBrush(Color.Black)), img.Rectangle);
        }
      }
      m_imagePositions.Sort(delegate(FanartControlImage i1, FanartControlImage i2) { return i1.Position.Y.CompareTo(i2.Position.Y); });
      foreach (FanartControlImage fci in m_imagePositions)
      {
        if (fci.Image != null)
        {
          g.DrawImage(fci.CreateFramedImage(), fci.Rectangle);

        }
        else
        {
          g.FillRectangle(new SolidBrush(FrameColor), fci.Rectangle);
          g.DrawRectangle(new Pen(Color.Black), fci.Rectangle);
        }
      }

      m_imagePositions.Sort(delegate(FanartControlImage i1, FanartControlImage i2) { return i1.Index.CompareTo(i2.Index); });

      if (m_mouseDownClosestDegree != -99)
      {
        g.DrawEllipse(new Pen(new SolidBrush(Color.Red)),
                      m_elipse[m_mouseDownClosestDegree].X - 5,
                      m_elipse[m_mouseDownClosestDegree].Y - 5, 10, 10);
      }


    }

    private void ChangeRectangleSize(int growSize, FanartControlImage img)
    {
      Rectangle pos = new Rectangle();
      pos.X = img.Position.X - (m_thumbSize.X / 2) - (growSize / 2);
      pos.Y = img.Position.Y - (m_thumbSize.Y / 2) - (growSize / 2);
      pos.Width = m_thumbSize.X + growSize;
      pos.Height = m_thumbSize.Y + growSize;
      img.Rectangle = pos;
    }



    private void FanartControl_KeyUp(object sender, KeyEventArgs e)
    {

    }

    private bool m_mouseDown;
    private Point m_mouseDownCoordinates;
    private int m_mouseDownClosestDegree = -99;
    private bool m_mouseClick;//mouseclick occured (no mousemove between mouse-down and mouse-up)
    private int m_distance;

    private void FanartControl_MouseUp(object sender, MouseEventArgs e)
    {
      m_mouseDown = false;
      if (m_mouseClick)
      {
        m_selectedIndex = GetSelectedIndex(e.Location);
        if (ImageClicked != null)
        {
          ImageClicked(new EventArgs());
        }
      }
    }

    private int GetSelectedIndex(Point _pos)
    {
      for (int i = 0; i < m_imagePositions.Count; i++)
      {
        if (m_imagePositions[i].Rectangle.Contains(_pos))
        {
          return i;
        }
      }
      return -99;
    }

    private void FanartControl_MouseEnter(object sender, EventArgs e)
    {

    }

    private void FanartControl_MouseDown(object sender, MouseEventArgs e)
    {
      if (!m_mouseDown)
      {
        m_mouseClick = true;
        m_mouseDown = true;
        m_mouseDownCoordinates = e.Location;
        m_selectedIndex = GetSelectedIndex(e.Location);
      }
    }

    private void FanartControl_MouseMove(object sender, MouseEventArgs e)
    {
      if (m_mouseDown)
      {
        if (m_mouseClick &&
           (Math.Abs(m_mouseDownCoordinates.X - e.X) > 5 ||
           Math.Abs(m_mouseDownCoordinates.Y - e.Y) > 5))
        {
          m_mouseClick = false;
        }
        m_mouseDownClosestDegree = GetNearestDegree(e.Location);
        if (m_selectedIndex != -99)
        {
          //UpdateRectanglePositions((m_position + m_mouseDownClosestDegree - m_imagePositions[m_selectedIndex].Degree) % 360);
          UpdatePosition();
        }
        Invalidate();
      }
    }



    private int GetNearestDegree(Point _point)
    {
      double minDist = -99;
      int minDegr = -99;
      for (int i = 0; i < 180; i++)
      {
        double dist = Math.Sqrt(Math.Abs(m_elipse[i].X - _point.X) + Math.Abs(m_elipse[i].Y - _point.Y));
        if (minDist == -99 || dist < minDist)
        {
          minDist = dist;
          minDegr = i;
        }
      }
      return minDegr;
    }
  }
}
