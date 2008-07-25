namespace TvdbTester
{
  partial class PosterControl
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PosterControl));
      this.panelBackground = new System.Windows.Forms.Panel();
      this.panelImage = new System.Windows.Forms.Panel();
      this.panelForeground = new System.Windows.Forms.Panel();
      this.pbLoadingScreen = new System.Windows.Forms.PictureBox();
      this.panelLeft = new System.Windows.Forms.Panel();
      this.panelRight = new System.Windows.Forms.Panel();
      this.panelBackground.SuspendLayout();
      this.panelImage.SuspendLayout();
      this.panelForeground.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbLoadingScreen)).BeginInit();
      this.SuspendLayout();
      // 
      // panelBackground
      // 
      this.panelBackground.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.panelBackground.BackColor = System.Drawing.Color.Transparent;
      this.panelBackground.BackgroundImage = global::TvdbTester.Properties.Resources.dvdbox_back;
      this.panelBackground.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.panelBackground.Controls.Add(this.panelImage);
      this.panelBackground.Location = new System.Drawing.Point(0, 0);
      this.panelBackground.Name = "panelBackground";
      this.panelBackground.Size = new System.Drawing.Size(300, 400);
      this.panelBackground.TabIndex = 2;
      // 
      // panelImage
      // 
      this.panelImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.panelImage.BackColor = System.Drawing.Color.Transparent;
      this.panelImage.Controls.Add(this.panelForeground);
      this.panelImage.Location = new System.Drawing.Point(0, 0);
      this.panelImage.Name = "panelImage";
      this.panelImage.Size = new System.Drawing.Size(300, 400);
      this.panelImage.TabIndex = 2;
      // 
      // panelForeground
      // 
      this.panelForeground.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.panelForeground.BackColor = System.Drawing.Color.Transparent;
      this.panelForeground.BackgroundImage = global::TvdbTester.Properties.Resources.dvdbox_front1;
      this.panelForeground.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
      this.panelForeground.Controls.Add(this.pbLoadingScreen);
      this.panelForeground.Controls.Add(this.panelLeft);
      this.panelForeground.Controls.Add(this.panelRight);
      this.panelForeground.Location = new System.Drawing.Point(0, 0);
      this.panelForeground.Name = "panelForeground";
      this.panelForeground.Size = new System.Drawing.Size(300, 400);
      this.panelForeground.TabIndex = 0;
      // 
      // pbLoadingScreen
      // 
      this.pbLoadingScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.pbLoadingScreen.Image = ((System.Drawing.Image)(resources.GetObject("pbLoadingScreen.Image")));
      this.pbLoadingScreen.Location = new System.Drawing.Point(90, 119);
      this.pbLoadingScreen.Name = "pbLoadingScreen";
      this.pbLoadingScreen.Size = new System.Drawing.Size(154, 149);
      this.pbLoadingScreen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pbLoadingScreen.TabIndex = 2;
      this.pbLoadingScreen.TabStop = false;
      this.pbLoadingScreen.Visible = false;
      // 
      // panelLeft
      // 
      this.panelLeft.BackColor = System.Drawing.Color.Transparent;
      this.panelLeft.BackgroundImage = global::TvdbTester.Properties.Resources.play_back;
      this.panelLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.panelLeft.Location = new System.Drawing.Point(14, 0);
      this.panelLeft.Name = "panelLeft";
      this.panelLeft.Size = new System.Drawing.Size(43, 41);
      this.panelLeft.TabIndex = 0;
      this.panelLeft.Visible = false;
      this.panelLeft.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelLeft_MouseClick);
      // 
      // panelRight
      // 
      this.panelRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.panelRight.BackColor = System.Drawing.Color.Transparent;
      this.panelRight.BackgroundImage = global::TvdbTester.Properties.Resources.play;
      this.panelRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.panelRight.Location = new System.Drawing.Point(254, 1);
      this.panelRight.Name = "panelRight";
      this.panelRight.Size = new System.Drawing.Size(44, 40);
      this.panelRight.TabIndex = 1;
      this.panelRight.Visible = false;
      this.panelRight.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelRight_MouseClick);
      // 
      // PosterControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.panelBackground);
      this.Name = "PosterControl";
      this.Size = new System.Drawing.Size(300, 400);
      this.panelBackground.ResumeLayout(false);
      this.panelImage.ResumeLayout(false);
      this.panelForeground.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pbLoadingScreen)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panelBackground;
    private System.Windows.Forms.Panel panelForeground;
    private System.Windows.Forms.Panel panelLeft;
    private System.Windows.Forms.Panel panelRight;
    private System.Windows.Forms.Panel panelImage;
    private System.Windows.Forms.PictureBox pbLoadingScreen;
  }
}
