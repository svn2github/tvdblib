namespace TvdbTester
{
  partial class FullscreenForm
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
      this.bcFullScreen = new TvdbTester.BannerControl();
      this.SuspendLayout();
      // 
      // bcFullScreen
      // 
      this.bcFullScreen.BannerImage = null;
      this.bcFullScreen.BannerImages = null;
      this.bcFullScreen.DefaultImage = null;
      this.bcFullScreen.Dock = System.Windows.Forms.DockStyle.Fill;
      this.bcFullScreen.ImageSizeMode = System.Windows.Forms.ImageLayout.Zoom;
      this.bcFullScreen.Index = 0;
      this.bcFullScreen.LoadingBackgroundColor = System.Drawing.Color.Transparent;
      this.bcFullScreen.LoadingImage = null;
      this.bcFullScreen.Location = new System.Drawing.Point(0, 0);
      this.bcFullScreen.Name = "bcFullScreen";
      this.bcFullScreen.Size = new System.Drawing.Size(644, 581);
      this.bcFullScreen.TabIndex = 0;
      this.bcFullScreen.UnavailableImage = null;
      this.bcFullScreen.UseThumb = false;
      // 
      // FullscreenForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(644, 581);
      this.Controls.Add(this.bcFullScreen);
      this.Name = "FullscreenForm";
      this.Text = "FullscreenForm";
      this.ResumeLayout(false);

    }

    #endregion

    private BannerControl bcFullScreen;
  }
}