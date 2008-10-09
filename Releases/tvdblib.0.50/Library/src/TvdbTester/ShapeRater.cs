using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing;
using System;
using System.Windows.Forms;

[DefaultProperty("CurrentRating"), DefaultEvent("CurrentRatingChanged"), DefaultBindingProperty("CurrentRating")]
public class Rater : UserControl, ISupportInitialize
{

  private const int DEFAULTVALUE = 0;

  private bool m_Initializing = false;
  private int m_CurrentRating = DEFAULTVALUE;
  private int PaintRating;
  private bool IsPainting = false;
  private Color PaintColor;
  private Label RateLabel;
  private Color PaintBorderColor;

  public Rater()
    : base()
  {

    //This call is required by the Windows Form Designer.
   
    InitializeComponent();
    LabelText = "RateLabel";
    //Add any initialization after the InitializeComponent() call
    SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);

  }



  #region "Properties"

  public enum eShape
  {
    Star,
    Circle,
    Square,
    Triangle,
    Diamond,
    Heart
  }

  public enum eLabelType
  {
    Text,
    TextItems,
    FormatString
  }

  public enum eShapeNumberShow
  {
    None,
    All,
    RateOnly
  }

  #region "AppearanceShaperRater"

  #region "Rating"

  public event EventHandler CurrentRatingChanged;

  [Category("AppearanceShaperRater"), Description("Get or Set the current Ratings value"), Bindable(true)]
  public int CurrentRating
  {
    get { return this.m_CurrentRating; }
    set
    {
      if ((value != this.m_CurrentRating) && !(this.m_Initializing))
      {
        this.m_CurrentRating = value;
        if (CurrentRatingChanged != null)
        {
          CurrentRatingChanged(this, EventArgs.Empty);
        }
        PaintRating = value;
        this.Invalidate();
        //RateIt(value)
      }
    }
  }

  private bool ShouldSerializeCurrentRating()
  {
    return !(bool)(this.m_CurrentRating != DEFAULTVALUE);
  }

  public override string ToString()
  {
    return this.CurrentRating.ToString();
  }

  private int _MaxRating = 5;
  [Category("AppearanceShaperRater"), Description("Get or Set number of shapes"), RefreshProperties(RefreshProperties.Repaint), DefaultValue(5)]
  public int MaxRating
  {
    get { return _MaxRating; }
    set
    {
      _MaxRating = value;
      this.Invalidate();
    }
  }
  #endregion

  #region "Label"

  private bool _LabelShow = true;
  [Category("AppearanceShaperRater"), Description("Get or Set if the Label is Visible"), RefreshProperties(RefreshProperties.Repaint), DefaultValue(true)]
  public bool LabelShow
  {
    get { return _LabelShow; }
    set
    {
      _LabelShow = value;
      RateLabel.Visible = value;
      this.Invalidate();
    }
  }

  private string _LabelText = "";
  [Category("AppearanceShaperRater"), Description("Get or Set the base Label text with rating of 0"), RefreshProperties(RefreshProperties.Repaint), DefaultValue("")]
  public string LabelText
  {
    get { return _LabelText; }
    set
    {
      _LabelText = value;
      RateLabel.Text = value;

      this.Invalidate();
    }
  }

  private ContentAlignment _LabelAlignment = ContentAlignment.MiddleCenter;
  [Category("AppearanceShaperRater"), Description("Get or Set Alignment for the Label"), RefreshProperties(RefreshProperties.Repaint), DefaultValue(StringAlignment.Near)]
  public ContentAlignment LabelAlignment
  {
    get { return _LabelAlignment; }
    set
    {
      _LabelAlignment = value;
      RateLabel.TextAlign = value;

      this.Invalidate();
    }
  }

  private int _LabelIndent = 10;
  [Category("AppearanceShaperRater"), Description("Get or Set the Gap between Rating and the Label"), RefreshProperties(RefreshProperties.Repaint), DefaultValue(10)]
  public int LabelIndent
  {
    get { return _LabelIndent; }
    set
    {
      _LabelIndent = value;
      this.Invalidate();
    }
  }

  private string[] _LabelTextItems = new string[] { "Poor", "Fair", "Good", "Better", "Best" };
  [Category("AppearanceShaperRater"), Description("Get or Set the text array of Label Items"), DefaultValue(new string[] { "Poor", "Fair", "Good", "Better", "Best" })]
  public string[] LabelTextItems
  {
    get { return _LabelTextItems; }
    set
    {
      _LabelTextItems = value;
      this.Invalidate();
    }
  }

  private string _LabelFormatString = "{0} out of {1}";
  [Category("AppearanceShaperRater"), Description("Get or Set the a String.Format where {0} = Rate being hovered over and {1} = the MaxRating"), RefreshProperties(RefreshProperties.Repaint), DefaultValue("{0} out of {1}")]
  public string LabelFormatString
  {
    get { return _LabelFormatString; }
    set
    {
      _LabelFormatString = value;
      this.Invalidate();
    }
  }

  private eLabelType _LabelTypeText = eLabelType.Text;
  [Category("AppearanceShaperRater"), Description("Get or Set the what text to show in the Label after selection"), RefreshProperties(RefreshProperties.Repaint), DefaultValue(eLabelType.Text)]
  public eLabelType LabelTypeText
  {
    get { return _LabelTypeText; }
    set
    {
      _LabelTypeText = value;
      this.Invalidate();
    }
  }

  private eLabelType _LabelTypeHover = eLabelType.TextItems;
  [Category("AppearanceShaperRater"), Description("Get or Set the what text to show in the Label while hovering"), RefreshProperties(RefreshProperties.Repaint), DefaultValue(eLabelType.TextItems)]
  public eLabelType LabelTypeHover
  {
    get { return _LabelTypeHover; }
    set
    {
      _LabelTypeHover = value;
      this.Invalidate();
    }
  }
  #endregion

  #region "Shape"

  private float _RadiusOuter = 10;
  [Category("AppearanceShaperRater"), Description("Get or Set Radius of the shape"), RefreshProperties(RefreshProperties.Repaint), DefaultValue(10)]
  public float RadiusOuter
  {
    get { return _RadiusOuter; }
    set
    {
      _RadiusOuter = value;
      this.Invalidate();
    }
  }

  private float _RadiusInner = 0;
  [Category("AppearanceShaperRater"), Description("Get or Set inner Radius of the Star shape"), RefreshProperties(RefreshProperties.Repaint), DefaultValue(0)]
  public float RadiusInner
  {
    get { return _RadiusInner; }
    set
    {
      _RadiusInner = value;
      this.Invalidate();
    }
  }

  private int _ShapeGap = 4;
  [Category("AppearanceShaperRater"), Description("Get or Set the distance between the shapes"), RefreshProperties(RefreshProperties.Repaint), DefaultValue(4)]
  public int ShapeGap
  {
    get { return _ShapeGap; }
    set
    {
      _ShapeGap = value;
      this.Invalidate();
    }
  }

  private eShape _Shape = eShape.Star;
  [Category("AppearanceShaperRater"), Description("Get or Set what shape to draw"), RefreshProperties(RefreshProperties.Repaint), DefaultValue(eShape.Star)]
  public eShape Shape
  {
    get { return _Shape; }
    set
    {
      _Shape = value;
      this.Invalidate();
    }
  }

  private Color _ShapeColorEmpty = Color.Transparent;
  [Category("AppearanceShaperRater"), Description("Get or Set the Color to fill the empty Shape"), DefaultValue(typeof(Color), "Transparent")]
  public Color ShapeColorEmpty
  {
    get { return _ShapeColorEmpty; }
    set
    {
      _ShapeColorEmpty = value;
      this.Invalidate();
    }
  }

  private Color _ShapeColorHover = Color.LightCoral;
  [Category("AppearanceShaperRater"), Description("Get or Set the Color to fill the Shapes being hovered over"), DefaultValue(typeof(Color), "LightCoral")]
  public Color ShapeColorHover
  {
    get { return _ShapeColorHover; }
    set
    {
      _ShapeColorHover = value;
      this.Invalidate();
    }
  }

  private Color _ShapeColorFill = Color.Yellow;
  [Category("AppearanceShaperRater"), Description("Get or Set the Color to fill the rated Shape"), DefaultValue(typeof(Color), "Yellow")]
  public Color ShapeColorFill
  {
    get { return _ShapeColorFill; }
    set
    {
      _ShapeColorFill = value;
      this.Invalidate();
    }
  }

  private bool _HighlightRateHover = false;
  [Category("AppearanceShaperRater"), Description("Get or Set the fill color of just the shape under the Mouse or all selected shapes while hovering"), DefaultValue(false)]
  public bool HighlightRateHover
  {
    get { return _HighlightRateHover; }
    set { _HighlightRateHover = value; }
  }

  private bool _HighlightRateFill = false;
  [Category("AppearanceShaperRater"), Description("Get or Set the fill color of just the rated shape or all selected shapes up to the rated shape"), DefaultValue(false)]
  public bool HighlightRateFill
  {
    get { return _HighlightRateFill; }
    set { _HighlightRateFill = value; }
  }

  private Color _ShapeBorderFilledColor = Color.Blue;
  [Category("AppearanceShaperRater"), Description("Get or Set the Color to border the rated Shapes"), DefaultValue(typeof(Color), "Blue")]
  public Color ShapeBorderFilledColor
  {
    get { return _ShapeBorderFilledColor; }
    set
    {
      _ShapeBorderFilledColor = value;
      this.Invalidate();
    }
  }

  private Color _ShapeBorderEmptyColor = Color.LightBlue;
  [Category("AppearanceShaperRater"), Description("Get or Set the Color to border the empty Shape"), DefaultValue(typeof(Color), "LightBlue")]
  public Color ShapeBorderEmptyColor
  {
    get { return _ShapeBorderEmptyColor; }
    set
    {
      _ShapeBorderEmptyColor = value;
      this.Invalidate();
    }
  }

  private Color _ShapeBorderHoverColor = Color.Blue;
  [Category("AppearanceShaperRater"), Description("Get or Set the Color to border the Shape when hovering"), DefaultValue(typeof(Color), "Blue")]
  public Color ShapeBorderHoverColor
  {
    get { return _ShapeBorderHoverColor; }
    set
    {
      _ShapeBorderHoverColor = value;
      this.Invalidate();
    }
  }

  private int _ShapeBorderWidth = 1;
  [Category("AppearanceShaperRater"), Description("Get or Set the Width of the Shapes border"), DefaultValue(1)]
  public int ShapeBorderWidth
  {
    get { return _ShapeBorderWidth; }
    set
    {
      _ShapeBorderWidth = value;
      this.Invalidate();
    }
  }
  #endregion

  #region "ShapeNumber"

  private eShapeNumberShow _ShapeNumberShow = eShapeNumberShow.None;
  [Category("AppearanceShaperRater"), Description("Get or Set if the number is shown inside the Shape"), DefaultValue(eShapeNumberShow.None)]
  public eShapeNumberShow ShapeNumberShow
  {
    get { return _ShapeNumberShow; }
    set
    {
      _ShapeNumberShow = value;
      this.Invalidate();
    }
  }

  private Color _ShapeNumberColor = Color.Black;
  [Category("AppearanceShaperRater"), Description("Get or Set the Color for the Number inside the Shape"), DefaultValue(typeof(Color), "Black")]
  public Color ShapeNumberColor
  {
    get { return _ShapeNumberColor; }
    set
    {
      _ShapeNumberColor = value;
      this.Invalidate();
    }
  }

  private Font _ShapeNumberFont = new Font("Arial", 8);
  [Category("AppearanceShaperRater"), Description("Get or Set the Font for the Number inside the Shape"), DefaultValue(typeof(Font), "Arial, 8pt")]
  public Font ShapeNumberFont
  {
    get { return _ShapeNumberFont; }
    set
    {
      _ShapeNumberFont = value;
      this.Invalidate();
    }
  }

  private Point _ShapeNumberIndent = new Point(0, 0);
  [Category("AppearanceShaperRater"), Description("Get or Set the offset for the Number inside the Shape"), DefaultValue(typeof(Point), "0, 0")]
  public Point ShapeNumberIndent
  {
    get { return _ShapeNumberIndent; }
    set
    {
      _ShapeNumberIndent = value;
      this.Invalidate();
    }
  }

  #endregion

  #endregion

  #endregion

  protected override void OnMouseMove(MouseEventArgs e)
  {
    base.OnMouseMove(e);
    if (!this.m_Initializing)
    {

      if (e.Y > (this.Height - (RadiusOuter * 2)) / 2 & e.Y < ((this.Height - (RadiusOuter * 2)) / 2) + (RadiusOuter * 2) - 1 & e.X > Padding.Left & e.X < (RadiusOuter * 2 * MaxRating) + (ShapeGap * MaxRating) + Padding.Left)
      {
        this.PaintRating = GetRate(e.X);
        IsPainting = true;
        PaintColor = ShapeColorHover;
        PaintBorderColor = ShapeBorderHoverColor;
      }
      this.Refresh();
    }
  }

  protected override void OnMouseDown(MouseEventArgs e)
  {
    base.OnMouseDown(e);
    if (!this.m_Initializing)
    {
      if (e.Button == System.Windows.Forms.MouseButtons.Right)
      {
        this.CurrentRating = 0;
      }
      else
      {
        if (e.Y > (this.Height - (RadiusOuter * 2)) / 2 & e.Y < ((this.Height - (RadiusOuter * 2)) / 2) + (RadiusOuter * 2) - 1 & e.X > Padding.Left & e.X < (RadiusOuter * 2 * MaxRating) + (ShapeGap * MaxRating) + Padding.Left)
        {
          this.CurrentRating = GetRate(e.X);
        }
      }
    }
  }

  protected override void OnMouseLeave(EventArgs e)
  {
    base.OnMouseLeave(e);
    if (!this.m_Initializing)
    {
      PaintRating = CurrentRating;
      PaintColor = ShapeColorFill;
      PaintBorderColor = ShapeBorderFilledColor;
      IsPainting = false;
    }
    this.Refresh();
  }

  public string GetLabelText(eLabelType LabelType)
  {
    switch (LabelType)
    {
      case eLabelType.Text:
        return LabelText;
      case eLabelType.TextItems:
        switch (this.PaintRating)
        {
          case 0:
            return LabelText;
          //case  // ERROR: Case labels with binary operators are unsupported : GreaterThan
//LabelTextItems.GetUpperBound(0) + 1:
            //return "";
          default:
            return (LabelTextItems.Length > this.PaintRating && this.PaintRating > 0) ? LabelTextItems[this.PaintRating - 1] : "n/a";
        }
      case eLabelType.FormatString:
        return string.Format(LabelFormatString, this.PaintRating, MaxRating);
      default:
        return "";
    }

  }

  public int GetRate(int eX)
  {
    int mRate = 0;

    while (!(eX >= (ShapeGap * mRate) + ((RadiusOuter * 2) * mRate) + Padding.Left & eX <= (ShapeGap * (mRate + 1)) + ((RadiusOuter * 2) * (mRate + 1)) + Padding.Left))
    {
      mRate += 1;
    }
    return mRate + 1;
  }

  
  protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
  {
    int intRate = 0;
    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
    Color UseFillColor = new Color();
    Color UseBorderColor = new Color();
    GraphicsPath ShapePath = new GraphicsPath();
    float ptx;
    float pty;
    StringFormat sf = new StringFormat();
    sf.Alignment = StringAlignment.Center;
    sf.LineAlignment = StringAlignment.Center;

    pty = (this.Height - (RadiusOuter * 2)) / 2;
    intRate = 0;
    while (!(intRate == MaxRating))
    {
      ptx = intRate * (RadiusOuter * 2 + ShapeGap) + Padding.Left + (ShapeGap / 2);
      if (PaintRating > intRate)
      {
        if (!IsPainting & HighlightRateFill & (PaintRating != intRate + 1))
        {
          UseFillColor = ShapeColorHover;
          UseBorderColor = ShapeBorderHoverColor;
        }
        else if (IsPainting & HighlightRateHover & (PaintRating == intRate + 1))
        {
          UseFillColor = ShapeColorFill;
          UseBorderColor = ShapeBorderFilledColor;
        }
        else
        {
          UseFillColor = PaintColor;
          UseBorderColor = PaintBorderColor;
        }
      }
      else
      {
        UseFillColor = ShapeColorEmpty;
        UseBorderColor = ShapeBorderEmptyColor;
      }

      ShapePath.Reset();
      Point[] pts;
      switch (Shape)
      {
        case eShape.Star:
          ShapePath = DrawStar(ptx, pty);
          break;
        case eShape.Heart:
          ShapePath = DrawHeart(ptx, pty);
          break;
        case eShape.Square:
          ShapePath.AddRectangle(new Rectangle((int)ptx, (int)pty, (int)(RadiusOuter * 2), (int)(RadiusOuter * 2)));
          break;
        case eShape.Circle:
          ShapePath.AddEllipse(ptx, pty, RadiusOuter * 2, RadiusOuter * 2);
          break;
        case eShape.Diamond:
          pts = new Point[] { new Point((int)(ptx + RadiusOuter), (int)pty), new Point((int)(ptx + RadiusOuter * 2), (int)(pty + RadiusOuter)), new Point((int)(ptx + RadiusOuter), (int)(pty + RadiusOuter * 2)), new Point((int)ptx, (int)(pty + RadiusOuter)) };
          ShapePath.AddPolygon(pts);
          break;
        case eShape.Triangle:
          pts = new Point[] { new Point((int)(ptx + RadiusOuter), (int)pty), new Point((int)(ptx + RadiusOuter * 2), (int)(pty + RadiusOuter * 2)), new Point((int)ptx, (int)(pty + RadiusOuter * 2)) };
          ShapePath.AddPolygon(pts);
          break;

      }

      e.Graphics.FillPath(new SolidBrush(UseFillColor), ShapePath);
      e.Graphics.DrawPath(new Pen(UseBorderColor, ShapeBorderWidth), ShapePath);

      if (ShapeNumberShow != eShapeNumberShow.None)
      {
        if (ShapeNumberShow == eShapeNumberShow.All | (ShapeNumberShow == eShapeNumberShow.RateOnly & PaintRating == intRate + 1))
        {
          e.Graphics.DrawString((intRate + 1).ToString(), ShapeNumberFont, new SolidBrush(ShapeNumberColor), new RectangleF(ShapeNumberIndent.X + ptx, ShapeNumberIndent.Y + pty, RadiusOuter * 2, RadiusOuter * 2), sf);
        }
      }

      intRate += 1;
    }

    if (LabelShow)
    {
      int R_x = (int)(((RadiusOuter * 2) * (MaxRating)) + LabelIndent + ((ShapeGap) * MaxRating) + Padding.Left);
      if (IsPainting)
      {
        RateLabel.Text = GetLabelText(LabelTypeHover);
      }
      else
      {
        RateLabel.Text = GetLabelText(LabelTypeText);
      }
      RateLabel.Width = (this.Width - R_x);
      RateLabel.Height = (this.Height);
      RateLabel.Location = new Point(R_x, 0);
    }
  }

  public GraphicsPath DrawStar(float Xc, float Yc)
  {
    GraphicsPath gp = new GraphicsPath();
    Xc += RadiusOuter;
    Yc += RadiusOuter;
    // RadiusInner and InnerRadius: determines how far from the center the inner vertices of the star are.
    // RadiusOuter: determines the size of the star.
    // xc, yc: determine the location of the star.
    float sin36 = (float)Math.Sin(36.0 * Math.PI / 180.0);
    float sin72 = (float)Math.Sin(72.0 * Math.PI / 180.0);
    float cos36 = (float)Math.Cos(36.0 * Math.PI / 180.0);
    float cos72 = (float)Math.Cos(72.0 * Math.PI / 180.0);
    float InnerRadius = (RadiusOuter * cos72 / cos36) + RadiusInner;

    PointF[] pts = new PointF[10];
    pts[0] = new PointF(Xc, Yc - RadiusOuter);
    pts[1] = new PointF(Xc + InnerRadius * sin36, Yc - InnerRadius * cos36);
    pts[2] = new PointF(Xc + RadiusOuter * sin72, Yc - RadiusOuter * cos72);
    pts[3] = new PointF(Xc + InnerRadius * sin72, Yc + InnerRadius * cos72);
    pts[4] = new PointF(Xc + RadiusOuter * sin36, Yc + RadiusOuter * cos36);
    pts[5] = new PointF(Xc, Yc + InnerRadius);
    pts[6] = new PointF(Xc - RadiusOuter * sin36, Yc + RadiusOuter * cos36);
    pts[7] = new PointF(Xc - InnerRadius * sin72, Yc + InnerRadius * cos72);
    pts[8] = new PointF(Xc - RadiusOuter * sin72, Yc - RadiusOuter * cos72);
    pts[9] = new PointF(Xc - InnerRadius * sin36, Yc - InnerRadius * cos36);

    gp.AddPolygon(pts);

    return gp;

  }

  public GraphicsPath DrawHeart(float Xc, float Yc)
  {
    GraphicsPath gp = new GraphicsPath();

    Rectangle HeartTopLeftSquare = new Rectangle((int)Xc, (int)Yc, (int)RadiusOuter, (int)RadiusOuter);
    Rectangle HeartTopRightSquare = new Rectangle((int)Xc + (int)RadiusOuter, (int)Yc, (int)RadiusOuter, (int)RadiusOuter);

    gp.AddArc(HeartTopLeftSquare, 135f, 225f);
    gp.AddArc(HeartTopRightSquare, 180f, 225f);
    gp.AddLine((int)(Xc + (RadiusOuter * 2) - (1 - Math.Sin(45 / 180 * Math.PI)) * RadiusOuter / 2), (int)(Yc + RadiusOuter / 2 + Math.Sin(45 / 180 * Math.PI) * RadiusOuter / 2), (int)(Xc + RadiusOuter), (int)(Yc + (RadiusOuter * 2)));
    gp.AddLine((int)(Xc + RadiusOuter / 2 - Math.Sin(45 / 180 * Math.PI) * RadiusOuter / 2), (int)(Yc + RadiusOuter / 2 + Math.Sin(45 / 180 * Math.PI) * RadiusOuter / 2), (int)(Xc + RadiusOuter), (int)(Yc + (RadiusOuter * 2)));
    return gp;
  }

  #region ISupportInitialize Members

  public void BeginInit()
  {
    this.m_Initializing = true;
  }

  public void EndInit()
  {
    this.m_Initializing = false;
    PaintColor = ShapeColorFill;
    PaintBorderColor = ShapeBorderFilledColor;
    PaintRating = CurrentRating;
    this.CurrentRating = this.m_CurrentRating;
  }

  #endregion

  private void InitializeComponent()
  {
    this.RateLabel = new System.Windows.Forms.Label();
    this.SuspendLayout();
    // 
    // RateLabel
    // 
    this.RateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)));
    this.RateLabel.AutoSize = true;
    this.RateLabel.Location = new System.Drawing.Point(4, 8);
    this.RateLabel.Name = "RateLabel";
    this.RateLabel.Size = new System.Drawing.Size(35, 13);
    this.RateLabel.TabIndex = 0;
    this.RateLabel.Text = "label1";
    // 
    // Rater
    // 
    this.Controls.Add(this.RateLabel);
    this.Name = "Rater";
    this.Size = new System.Drawing.Size(78, 30);
    this.ResumeLayout(false);
    this.PerformLayout();

  }
}
