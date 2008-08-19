namespace TvdbTester
{
  partial class BannerControl
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
      this.cmdLeft = new System.Windows.Forms.Panel();
      this.cmdRight = new System.Windows.Forms.Panel();
      this.panelImage = new System.Windows.Forms.Panel();
      this.pbLoading = new System.Windows.Forms.PictureBox();
      this.panelImage.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbLoading)).BeginInit();
      this.SuspendLayout();
      // 
      // cmdLeft
      // 
      this.cmdLeft.BackColor = System.Drawing.Color.Transparent;
      this.cmdLeft.BackgroundImage = global::TvdbTester.Properties.Resources.first;
      this.cmdLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.cmdLeft.Location = new System.Drawing.Point(0, 0);
      this.cmdLeft.Name = "cmdLeft";
      this.cmdLeft.Size = new System.Drawing.Size(22, 21);
      this.cmdLeft.TabIndex = 0;
      this.cmdLeft.Visible = false;
      this.cmdLeft.Click += new System.EventHandler(this.cmdLeft_Click);
      // 
      // cmdRight
      // 
      this.cmdRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdRight.BackColor = System.Drawing.Color.Transparent;
      this.cmdRight.BackgroundImage = global::TvdbTester.Properties.Resources.last;
      this.cmdRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.cmdRight.Location = new System.Drawing.Point(692, -1);
      this.cmdRight.Name = "cmdRight";
      this.cmdRight.Size = new System.Drawing.Size(23, 21);
      this.cmdRight.TabIndex = 0;
      this.cmdRight.Visible = false;
      this.cmdRight.Click += new System.EventHandler(this.cmdRight_Click);
      // 
      // panelImage
      // 
      this.panelImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.panelImage.Controls.Add(this.cmdLeft);
      this.panelImage.Controls.Add(this.cmdRight);
      this.panelImage.Controls.Add(this.pbLoading);
      this.panelImage.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelImage.Location = new System.Drawing.Point(0, 0);
      this.panelImage.Name = "panelImage";
      this.panelImage.Size = new System.Drawing.Size(715, 145);
      this.panelImage.TabIndex = 1;
      // 
      // pbLoading
      // 
      this.pbLoading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.pbLoading.BackColor = System.Drawing.Color.Transparent;
      this.pbLoading.Location = new System.Drawing.Point(247, 26);
      this.pbLoading.Name = "pbLoading";
      this.pbLoading.Size = new System.Drawing.Size(231, 89);
      this.pbLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.pbLoading.TabIndex = 0;
      this.pbLoading.TabStop = false;
      this.pbLoading.Visible = false;
      // 
      // BannerControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.panelImage);
      this.Name = "BannerControl";
      this.Size = new System.Drawing.Size(715, 145);
      this.SizeChanged += new System.EventHandler(this.BannerControl_SizeChanged);
      this.panelImage.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pbLoading)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel cmdLeft;
    private System.Windows.Forms.Panel cmdRight;
    private System.Windows.Forms.Panel panelImage;
    private System.Windows.Forms.PictureBox pbLoading;

  }
}
