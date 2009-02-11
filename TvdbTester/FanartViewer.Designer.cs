namespace TvdbTester
{
  partial class FanartViewer
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.cmdInit = new System.Windows.Forms.Button();
      this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
      this.bannerControl1 = new TvdbTester.BannerControl();
      this.fanartControl1 = new TvdbTester.FanartControl();
      this.SuspendLayout();
      // 
      // cmdInit
      // 
      this.cmdInit.Location = new System.Drawing.Point(7, -1);
      this.cmdInit.Name = "cmdInit";
      this.cmdInit.Size = new System.Drawing.Size(534, 26);
      this.cmdInit.TabIndex = 0;
      this.cmdInit.Text = "Init";
      this.cmdInit.UseVisualStyleBackColor = true;
      this.cmdInit.Click += new System.EventHandler(this.cmdInit_Click);
      // 
      // hScrollBar1
      // 
      this.hScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.hScrollBar1.Location = new System.Drawing.Point(13, 247);
      this.hScrollBar1.Maximum = 360;
      this.hScrollBar1.Name = "hScrollBar1";
      this.hScrollBar1.Size = new System.Drawing.Size(508, 29);
      this.hScrollBar1.TabIndex = 2;
      this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
      // 
      // bannerControl1
      // 
      this.bannerControl1.BannerImage = null;
      this.bannerControl1.BannerImages = null;
      this.bannerControl1.DefaultImage = null;
      this.bannerControl1.ImageSizeMode = System.Windows.Forms.ImageLayout.Zoom;
      this.bannerControl1.Index = 0;
      this.bannerControl1.LoadingBackgroundColor = System.Drawing.Color.Transparent;
      this.bannerControl1.LoadingImage = global::TvdbTester.Properties.Resources.loading;
      this.bannerControl1.Location = new System.Drawing.Point(13, 283);
      this.bannerControl1.Name = "bannerControl1";
      this.bannerControl1.Size = new System.Drawing.Size(519, 337);
      this.bannerControl1.TabIndex = 3;
      this.bannerControl1.UnavailableImage = null;
      this.bannerControl1.UseThumb = false;
      // 
      // fanartControl1
      // 
      this.fanartControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.fanartControl1.FrameColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
      this.fanartControl1.Location = new System.Drawing.Point(7, 31);
      this.fanartControl1.Name = "fanartControl1";
      this.fanartControl1.NumberOfImages = 8;
      this.fanartControl1.Position = 0;
      this.fanartControl1.SelectedIndex = 0;
      this.fanartControl1.Size = new System.Drawing.Size(514, 213);
      this.fanartControl1.TabIndex = 1;
      this.fanartControl1.ThumbSize = new System.Drawing.Point(100, 75);
      this.fanartControl1.ImageClicked += new TvdbTester.FanartControl.ImageClickedEventHandler(this.fanartControl1_ImageClicked);
      // 
      // FanartViewer
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(524, 628);
      this.Controls.Add(this.bannerControl1);
      this.Controls.Add(this.hScrollBar1);
      this.Controls.Add(this.fanartControl1);
      this.Controls.Add(this.cmdInit);
      this.Name = "FanartViewer";
      this.Text = "FanartViewer";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button cmdInit;
    private FanartControl fanartControl1;
    private System.Windows.Forms.HScrollBar hScrollBar1;
    private BannerControl bannerControl1;
  }
}