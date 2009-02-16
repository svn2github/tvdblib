namespace WikiCodeExamples
{
  partial class ImageTestForm
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
      this.rbFanartVignette = new System.Windows.Forms.RadioButton();
      this.rbLoadEpisodeBannerThumb = new System.Windows.Forms.RadioButton();
      this.rbLoadPosterThumb = new System.Windows.Forms.RadioButton();
      this.rbLoadEpisodeBanner = new System.Windows.Forms.RadioButton();
      this.rbLoadPoster = new System.Windows.Forms.RadioButton();
      this.rbLoadSeasonThumb = new System.Windows.Forms.RadioButton();
      this.rbLoadFanartThumb = new System.Windows.Forms.RadioButton();
      this.rbLoadSeason = new System.Windows.Forms.RadioButton();
      this.rbLoadFanart = new System.Windows.Forms.RadioButton();
      this.txtSeriesIdForBanners = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.cmdInit = new System.Windows.Forms.Button();
      this.cmdEnd = new System.Windows.Forms.Button();
      this.pbBannerTesting = new System.Windows.Forms.PictureBox();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.lvSeriesBanners = new System.Windows.Forms.ListView();
      this.cmdLoadSeriesBanner = new System.Windows.Forms.Button();
      this.cmdUnloadSeriesBanner = new System.Windows.Forms.Button();
      this.rbLoadSeriesBannerThumb = new System.Windows.Forms.RadioButton();
      this.rbLoadSeriesBanner = new System.Windows.Forms.RadioButton();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.tabPage3 = new System.Windows.Forms.TabPage();
      this.tabPage4 = new System.Windows.Forms.TabPage();
      this.tabPage5 = new System.Windows.Forms.TabPage();
      this.cmdLoadSeries = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.pbBannerTesting)).BeginInit();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.tabPage3.SuspendLayout();
      this.tabPage4.SuspendLayout();
      this.tabPage5.SuspendLayout();
      this.SuspendLayout();
      // 
      // rbFanartVignette
      // 
      this.rbFanartVignette.AutoSize = true;
      this.rbFanartVignette.Location = new System.Drawing.Point(174, 6);
      this.rbFanartVignette.Name = "rbFanartVignette";
      this.rbFanartVignette.Size = new System.Drawing.Size(97, 17);
      this.rbFanartVignette.TabIndex = 19;
      this.rbFanartVignette.TabStop = true;
      this.rbFanartVignette.Text = "Fanart Vignette";
      this.rbFanartVignette.UseVisualStyleBackColor = true;
      // 
      // rbLoadEpisodeBannerThumb
      // 
      this.rbLoadEpisodeBannerThumb.AutoSize = true;
      this.rbLoadEpisodeBannerThumb.Location = new System.Drawing.Point(142, 6);
      this.rbLoadEpisodeBannerThumb.Name = "rbLoadEpisodeBannerThumb";
      this.rbLoadEpisodeBannerThumb.Size = new System.Drawing.Size(99, 17);
      this.rbLoadEpisodeBannerThumb.TabIndex = 16;
      this.rbLoadEpisodeBannerThumb.TabStop = true;
      this.rbLoadEpisodeBannerThumb.Text = "Episode Thumb";
      this.rbLoadEpisodeBannerThumb.UseVisualStyleBackColor = true;
      // 
      // rbLoadPosterThumb
      // 
      this.rbLoadPosterThumb.AutoSize = true;
      this.rbLoadPosterThumb.Location = new System.Drawing.Point(131, 12);
      this.rbLoadPosterThumb.Name = "rbLoadPosterThumb";
      this.rbLoadPosterThumb.Size = new System.Drawing.Size(91, 17);
      this.rbLoadPosterThumb.TabIndex = 16;
      this.rbLoadPosterThumb.TabStop = true;
      this.rbLoadPosterThumb.Text = "Poster Thumb";
      this.rbLoadPosterThumb.UseVisualStyleBackColor = true;
      // 
      // rbLoadEpisodeBanner
      // 
      this.rbLoadEpisodeBanner.AutoSize = true;
      this.rbLoadEpisodeBanner.Location = new System.Drawing.Point(18, 6);
      this.rbLoadEpisodeBanner.Name = "rbLoadEpisodeBanner";
      this.rbLoadEpisodeBanner.Size = new System.Drawing.Size(100, 17);
      this.rbLoadEpisodeBanner.TabIndex = 16;
      this.rbLoadEpisodeBanner.TabStop = true;
      this.rbLoadEpisodeBanner.Text = "Episode Banner";
      this.rbLoadEpisodeBanner.UseVisualStyleBackColor = true;
      // 
      // rbLoadPoster
      // 
      this.rbLoadPoster.AutoSize = true;
      this.rbLoadPoster.Location = new System.Drawing.Point(11, 12);
      this.rbLoadPoster.Name = "rbLoadPoster";
      this.rbLoadPoster.Size = new System.Drawing.Size(55, 17);
      this.rbLoadPoster.TabIndex = 16;
      this.rbLoadPoster.TabStop = true;
      this.rbLoadPoster.Text = "Poster";
      this.rbLoadPoster.UseVisualStyleBackColor = true;
      // 
      // rbLoadSeasonThumb
      // 
      this.rbLoadSeasonThumb.AutoSize = true;
      this.rbLoadSeasonThumb.Location = new System.Drawing.Point(141, 6);
      this.rbLoadSeasonThumb.Name = "rbLoadSeasonThumb";
      this.rbLoadSeasonThumb.Size = new System.Drawing.Size(134, 17);
      this.rbLoadSeasonThumb.TabIndex = 16;
      this.rbLoadSeasonThumb.TabStop = true;
      this.rbLoadSeasonThumb.Text = "Season Banner Thumb";
      this.rbLoadSeasonThumb.UseVisualStyleBackColor = true;
      // 
      // rbLoadFanartThumb
      // 
      this.rbLoadFanartThumb.AutoSize = true;
      this.rbLoadFanartThumb.Location = new System.Drawing.Point(77, 6);
      this.rbLoadFanartThumb.Name = "rbLoadFanartThumb";
      this.rbLoadFanartThumb.Size = new System.Drawing.Size(91, 17);
      this.rbLoadFanartThumb.TabIndex = 16;
      this.rbLoadFanartThumb.TabStop = true;
      this.rbLoadFanartThumb.Text = "Fanart Thumb";
      this.rbLoadFanartThumb.UseVisualStyleBackColor = true;
      // 
      // rbLoadSeason
      // 
      this.rbLoadSeason.AutoSize = true;
      this.rbLoadSeason.Location = new System.Drawing.Point(18, 6);
      this.rbLoadSeason.Name = "rbLoadSeason";
      this.rbLoadSeason.Size = new System.Drawing.Size(98, 17);
      this.rbLoadSeason.TabIndex = 16;
      this.rbLoadSeason.TabStop = true;
      this.rbLoadSeason.Text = "Season Banner";
      this.rbLoadSeason.UseVisualStyleBackColor = true;
      // 
      // rbLoadFanart
      // 
      this.rbLoadFanart.AutoSize = true;
      this.rbLoadFanart.Location = new System.Drawing.Point(6, 6);
      this.rbLoadFanart.Name = "rbLoadFanart";
      this.rbLoadFanart.Size = new System.Drawing.Size(55, 17);
      this.rbLoadFanart.TabIndex = 16;
      this.rbLoadFanart.TabStop = true;
      this.rbLoadFanart.Text = "Fanart";
      this.rbLoadFanart.UseVisualStyleBackColor = true;
      // 
      // txtSeriesIdForBanners
      // 
      this.txtSeriesIdForBanners.Location = new System.Drawing.Point(82, 45);
      this.txtSeriesIdForBanners.Name = "txtSeriesIdForBanners";
      this.txtSeriesIdForBanners.Size = new System.Drawing.Size(100, 20);
      this.txtSeriesIdForBanners.TabIndex = 15;
      this.txtSeriesIdForBanners.Text = "73255";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(12, 48);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(48, 13);
      this.label5.TabIndex = 13;
      this.label5.Text = "Series Id";
      // 
      // cmdInit
      // 
      this.cmdInit.Location = new System.Drawing.Point(12, 12);
      this.cmdInit.Name = "cmdInit";
      this.cmdInit.Size = new System.Drawing.Size(315, 23);
      this.cmdInit.TabIndex = 19;
      this.cmdInit.Text = "Initialise";
      this.cmdInit.UseVisualStyleBackColor = true;
      this.cmdInit.Click += new System.EventHandler(this.cmdInit_Click);
      // 
      // cmdEnd
      // 
      this.cmdEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.cmdEnd.Enabled = false;
      this.cmdEnd.Location = new System.Drawing.Point(12, 386);
      this.cmdEnd.Name = "cmdEnd";
      this.cmdEnd.Size = new System.Drawing.Size(315, 23);
      this.cmdEnd.TabIndex = 21;
      this.cmdEnd.Text = "End";
      this.cmdEnd.UseVisualStyleBackColor = true;
      this.cmdEnd.Click += new System.EventHandler(this.cmdEnd_Click);
      // 
      // pbBannerTesting
      // 
      this.pbBannerTesting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.pbBannerTesting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
      this.pbBannerTesting.Location = new System.Drawing.Point(347, 12);
      this.pbBannerTesting.Name = "pbBannerTesting";
      this.pbBannerTesting.Size = new System.Drawing.Size(489, 397);
      this.pbBannerTesting.TabIndex = 17;
      this.pbBannerTesting.TabStop = false;
      this.pbBannerTesting.Visible = false;
      // 
      // tabControl1
      // 
      this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)));
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Controls.Add(this.tabPage3);
      this.tabControl1.Controls.Add(this.tabPage4);
      this.tabControl1.Controls.Add(this.tabPage5);
      this.tabControl1.Location = new System.Drawing.Point(12, 79);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(315, 301);
      this.tabControl1.TabIndex = 22;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.lvSeriesBanners);
      this.tabPage1.Controls.Add(this.cmdLoadSeriesBanner);
      this.tabPage1.Controls.Add(this.cmdUnloadSeriesBanner);
      this.tabPage1.Controls.Add(this.rbLoadSeriesBannerThumb);
      this.tabPage1.Controls.Add(this.rbLoadSeriesBanner);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(307, 275);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Series Banner";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // lvSeriesBanners
      // 
      this.lvSeriesBanners.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)));
      this.lvSeriesBanners.Location = new System.Drawing.Point(6, 29);
      this.lvSeriesBanners.Name = "lvSeriesBanners";
      this.lvSeriesBanners.Size = new System.Drawing.Size(295, 208);
      this.lvSeriesBanners.TabIndex = 24;
      this.lvSeriesBanners.UseCompatibleStateImageBehavior = false;
      this.lvSeriesBanners.View = System.Windows.Forms.View.List;
      // 
      // cmdLoadSeriesBanner
      // 
      this.cmdLoadSeriesBanner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.cmdLoadSeriesBanner.Location = new System.Drawing.Point(3, 243);
      this.cmdLoadSeriesBanner.Name = "cmdLoadSeriesBanner";
      this.cmdLoadSeriesBanner.Size = new System.Drawing.Size(147, 23);
      this.cmdLoadSeriesBanner.TabIndex = 23;
      this.cmdLoadSeriesBanner.Text = "Load";
      this.cmdLoadSeriesBanner.UseVisualStyleBackColor = true;
      this.cmdLoadSeriesBanner.Click += new System.EventHandler(this.cmdLoadSeriesBanner_Click);
      // 
      // cmdUnloadSeriesBanner
      // 
      this.cmdUnloadSeriesBanner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.cmdUnloadSeriesBanner.Location = new System.Drawing.Point(156, 243);
      this.cmdUnloadSeriesBanner.Name = "cmdUnloadSeriesBanner";
      this.cmdUnloadSeriesBanner.Size = new System.Drawing.Size(138, 23);
      this.cmdUnloadSeriesBanner.TabIndex = 22;
      this.cmdUnloadSeriesBanner.Text = "Unload";
      this.cmdUnloadSeriesBanner.UseVisualStyleBackColor = true;
      // 
      // rbLoadSeriesBannerThumb
      // 
      this.rbLoadSeriesBannerThumb.AutoSize = true;
      this.rbLoadSeriesBannerThumb.Location = new System.Drawing.Point(175, 6);
      this.rbLoadSeriesBannerThumb.Name = "rbLoadSeriesBannerThumb";
      this.rbLoadSeriesBannerThumb.Size = new System.Drawing.Size(127, 17);
      this.rbLoadSeriesBannerThumb.TabIndex = 20;
      this.rbLoadSeriesBannerThumb.TabStop = true;
      this.rbLoadSeriesBannerThumb.Text = "Series Banner Thumb";
      this.rbLoadSeriesBannerThumb.UseVisualStyleBackColor = true;
      // 
      // rbLoadSeriesBanner
      // 
      this.rbLoadSeriesBanner.AutoSize = true;
      this.rbLoadSeriesBanner.Location = new System.Drawing.Point(11, 6);
      this.rbLoadSeriesBanner.Name = "rbLoadSeriesBanner";
      this.rbLoadSeriesBanner.Size = new System.Drawing.Size(91, 17);
      this.rbLoadSeriesBanner.TabIndex = 21;
      this.rbLoadSeriesBanner.TabStop = true;
      this.rbLoadSeriesBanner.Text = "Series Banner";
      this.rbLoadSeriesBanner.UseVisualStyleBackColor = true;
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.rbLoadFanart);
      this.tabPage2.Controls.Add(this.rbLoadFanartThumb);
      this.tabPage2.Controls.Add(this.rbFanartVignette);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(307, 273);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Fanart";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // tabPage3
      // 
      this.tabPage3.Controls.Add(this.rbLoadPosterThumb);
      this.tabPage3.Controls.Add(this.rbLoadPoster);
      this.tabPage3.Location = new System.Drawing.Point(4, 22);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage3.Size = new System.Drawing.Size(307, 273);
      this.tabPage3.TabIndex = 2;
      this.tabPage3.Text = "Poster";
      this.tabPage3.UseVisualStyleBackColor = true;
      // 
      // tabPage4
      // 
      this.tabPage4.Controls.Add(this.rbLoadSeason);
      this.tabPage4.Controls.Add(this.rbLoadSeasonThumb);
      this.tabPage4.Location = new System.Drawing.Point(4, 22);
      this.tabPage4.Name = "tabPage4";
      this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage4.Size = new System.Drawing.Size(307, 273);
      this.tabPage4.TabIndex = 3;
      this.tabPage4.Text = "Season";
      this.tabPage4.UseVisualStyleBackColor = true;
      // 
      // tabPage5
      // 
      this.tabPage5.Controls.Add(this.rbLoadEpisodeBanner);
      this.tabPage5.Controls.Add(this.rbLoadEpisodeBannerThumb);
      this.tabPage5.Location = new System.Drawing.Point(4, 22);
      this.tabPage5.Name = "tabPage5";
      this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage5.Size = new System.Drawing.Size(307, 273);
      this.tabPage5.TabIndex = 4;
      this.tabPage5.Text = "Episode";
      this.tabPage5.UseVisualStyleBackColor = true;
      // 
      // cmdLoadSeries
      // 
      this.cmdLoadSeries.Location = new System.Drawing.Point(248, 43);
      this.cmdLoadSeries.Name = "cmdLoadSeries";
      this.cmdLoadSeries.Size = new System.Drawing.Size(75, 23);
      this.cmdLoadSeries.TabIndex = 23;
      this.cmdLoadSeries.Text = "Load";
      this.cmdLoadSeries.UseVisualStyleBackColor = true;
      this.cmdLoadSeries.Click += new System.EventHandler(this.cmdLoadSeries_Click);
      // 
      // ImageTestForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(848, 424);
      this.Controls.Add(this.cmdLoadSeries);
      this.Controls.Add(this.tabControl1);
      this.Controls.Add(this.cmdEnd);
      this.Controls.Add(this.cmdInit);
      this.Controls.Add(this.pbBannerTesting);
      this.Controls.Add(this.txtSeriesIdForBanners);
      this.Controls.Add(this.label5);
      this.Name = "ImageTestForm";
      this.Text = "ImageTestForm";
      ((System.ComponentModel.ISupportInitialize)(this.pbBannerTesting)).EndInit();
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage1.PerformLayout();
      this.tabPage2.ResumeLayout(false);
      this.tabPage2.PerformLayout();
      this.tabPage3.ResumeLayout(false);
      this.tabPage3.PerformLayout();
      this.tabPage4.ResumeLayout(false);
      this.tabPage4.PerformLayout();
      this.tabPage5.ResumeLayout(false);
      this.tabPage5.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.RadioButton rbFanartVignette;
    private System.Windows.Forms.RadioButton rbLoadEpisodeBannerThumb;
    private System.Windows.Forms.RadioButton rbLoadPosterThumb;
    private System.Windows.Forms.RadioButton rbLoadEpisodeBanner;
    private System.Windows.Forms.RadioButton rbLoadPoster;
    private System.Windows.Forms.RadioButton rbLoadSeasonThumb;
    private System.Windows.Forms.RadioButton rbLoadFanartThumb;
    private System.Windows.Forms.RadioButton rbLoadSeason;
    private System.Windows.Forms.RadioButton rbLoadFanart;
    private System.Windows.Forms.TextBox txtSeriesIdForBanners;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.PictureBox pbBannerTesting;
    private System.Windows.Forms.Button cmdInit;
    private System.Windows.Forms.Button cmdEnd;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.ListView lvSeriesBanners;
    private System.Windows.Forms.Button cmdLoadSeriesBanner;
    private System.Windows.Forms.Button cmdUnloadSeriesBanner;
    private System.Windows.Forms.RadioButton rbLoadSeriesBannerThumb;
    private System.Windows.Forms.RadioButton rbLoadSeriesBanner;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.TabPage tabPage3;
    private System.Windows.Forms.TabPage tabPage4;
    private System.Windows.Forms.TabPage tabPage5;
    private System.Windows.Forms.Button cmdLoadSeries;
  }
}