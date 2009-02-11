namespace TvdbTester
{
  partial class TestForm
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
      this.cmdTest1 = new System.Windows.Forms.Button();
      this.cmdTestZip = new System.Windows.Forms.Button();
      this.cmdInit = new System.Windows.Forms.Button();
      this.cmdEnd = new System.Windows.Forms.Button();
      this.lvSeries = new System.Windows.Forms.ListView();
      this.chProperty = new System.Windows.Forms.ColumnHeader();
      this.chValue = new System.Windows.Forms.ColumnHeader();
      this.txtSeriesId = new System.Windows.Forms.TextBox();
      this.txtSeriesId2 = new System.Windows.Forms.TextBox();
      this.txtSeason = new System.Windows.Forms.TextBox();
      this.txtEpisode = new System.Windows.Forms.TextBox();
      this.Episodes = new System.Windows.Forms.GroupBox();
      this.cbOrdering = new System.Windows.Forms.ComboBox();
      this.cmdGetEpisodes = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.cmdGetAllSeriesRatings = new System.Windows.Forms.Button();
      this.cmdSetUser = new System.Windows.Forms.Button();
      this.txtUserId = new System.Windows.Forms.TextBox();
      this.cmdGetRatingsForSeries = new System.Windows.Forms.Button();
      this.txtSeriesRatingsId = new System.Windows.Forms.TextBox();
      this.dateTimePickerEpAired = new System.Windows.Forms.DateTimePicker();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.cmdGetEpisodeAired = new System.Windows.Forms.Button();
      this.label4 = new System.Windows.Forms.Label();
      this.txtSeriesEpisodeAiredId = new System.Windows.Forms.TextBox();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.cmdBannerTestingPrev = new System.Windows.Forms.Button();
      this.cmdBannerTestingNext = new System.Windows.Forms.Button();
      this.cmdLoadBannerTest = new System.Windows.Forms.Button();
      this.rbLoadEpisodeBannerThumb = new System.Windows.Forms.RadioButton();
      this.rbLoadPosterThumb = new System.Windows.Forms.RadioButton();
      this.rbLoadEpisodeBanner = new System.Windows.Forms.RadioButton();
      this.rbLoadPoster = new System.Windows.Forms.RadioButton();
      this.rbLoadSeasonThumb = new System.Windows.Forms.RadioButton();
      this.rbLoadFanartThumb = new System.Windows.Forms.RadioButton();
      this.rbLoadSeason = new System.Windows.Forms.RadioButton();
      this.rbLoadFanart = new System.Windows.Forms.RadioButton();
      this.rbLoadSeriesBannerThumb = new System.Windows.Forms.RadioButton();
      this.rbLoadSeriesBanner = new System.Windows.Forms.RadioButton();
      this.txtSeriesIdForBanners = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.pbBannerTesting = new System.Windows.Forms.PictureBox();
      this.bcSeriesBanner = new TvdbTester.BannerControl();
      this.bcActors = new TvdbTester.BannerControl();
      this.rbFanartVignette = new System.Windows.Forms.RadioButton();
      this.Episodes.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbBannerTesting)).BeginInit();
      this.SuspendLayout();
      // 
      // cmdTest1
      // 
      this.cmdTest1.Enabled = false;
      this.cmdTest1.Location = new System.Drawing.Point(22, 67);
      this.cmdTest1.Name = "cmdTest1";
      this.cmdTest1.Size = new System.Drawing.Size(184, 23);
      this.cmdTest1.TabIndex = 0;
      this.cmdTest1.Text = "Test Actors";
      this.cmdTest1.UseVisualStyleBackColor = true;
      this.cmdTest1.Click += new System.EventHandler(this.cmdTest1_Click);
      // 
      // cmdTestZip
      // 
      this.cmdTestZip.Enabled = false;
      this.cmdTestZip.Location = new System.Drawing.Point(124, 96);
      this.cmdTestZip.Name = "cmdTestZip";
      this.cmdTestZip.Size = new System.Drawing.Size(82, 23);
      this.cmdTestZip.TabIndex = 2;
      this.cmdTestZip.Text = "Test Zip";
      this.cmdTestZip.UseVisualStyleBackColor = true;
      this.cmdTestZip.Click += new System.EventHandler(this.cmdTestZip_Click);
      // 
      // cmdInit
      // 
      this.cmdInit.Location = new System.Drawing.Point(22, 9);
      this.cmdInit.Name = "cmdInit";
      this.cmdInit.Size = new System.Drawing.Size(184, 23);
      this.cmdInit.TabIndex = 3;
      this.cmdInit.Text = "Initialise";
      this.cmdInit.UseVisualStyleBackColor = true;
      this.cmdInit.Click += new System.EventHandler(this.cmdInit_Click);
      // 
      // cmdEnd
      // 
      this.cmdEnd.Enabled = false;
      this.cmdEnd.Location = new System.Drawing.Point(6, 747);
      this.cmdEnd.Name = "cmdEnd";
      this.cmdEnd.Size = new System.Drawing.Size(200, 23);
      this.cmdEnd.TabIndex = 4;
      this.cmdEnd.Text = "End";
      this.cmdEnd.UseVisualStyleBackColor = true;
      this.cmdEnd.Click += new System.EventHandler(this.cmdEnd_Click);
      // 
      // lvSeries
      // 
      this.lvSeries.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chProperty,
            this.chValue});
      this.lvSeries.FullRowSelect = true;
      this.lvSeries.Location = new System.Drawing.Point(302, 96);
      this.lvSeries.Name = "lvSeries";
      this.lvSeries.Size = new System.Drawing.Size(348, 560);
      this.lvSeries.TabIndex = 5;
      this.lvSeries.UseCompatibleStateImageBehavior = false;
      this.lvSeries.View = System.Windows.Forms.View.Details;
      // 
      // chProperty
      // 
      this.chProperty.Text = "Property";
      this.chProperty.Width = 139;
      // 
      // chValue
      // 
      this.chValue.Text = "Value";
      this.chValue.Width = 200;
      // 
      // txtSeriesId
      // 
      this.txtSeriesId.Location = new System.Drawing.Point(22, 98);
      this.txtSeriesId.Name = "txtSeriesId";
      this.txtSeriesId.Size = new System.Drawing.Size(96, 20);
      this.txtSeriesId.TabIndex = 7;
      this.txtSeriesId.Text = "73255";
      // 
      // txtSeriesId2
      // 
      this.txtSeriesId2.Location = new System.Drawing.Point(10, 37);
      this.txtSeriesId2.Name = "txtSeriesId2";
      this.txtSeriesId2.Size = new System.Drawing.Size(46, 20);
      this.txtSeriesId2.TabIndex = 8;
      // 
      // txtSeason
      // 
      this.txtSeason.Location = new System.Drawing.Point(62, 37);
      this.txtSeason.Name = "txtSeason";
      this.txtSeason.Size = new System.Drawing.Size(41, 20);
      this.txtSeason.TabIndex = 8;
      // 
      // txtEpisode
      // 
      this.txtEpisode.Location = new System.Drawing.Point(109, 37);
      this.txtEpisode.Name = "txtEpisode";
      this.txtEpisode.Size = new System.Drawing.Size(49, 20);
      this.txtEpisode.TabIndex = 8;
      // 
      // Episodes
      // 
      this.Episodes.Controls.Add(this.cbOrdering);
      this.Episodes.Controls.Add(this.cmdGetEpisodes);
      this.Episodes.Controls.Add(this.label3);
      this.Episodes.Controls.Add(this.label2);
      this.Episodes.Controls.Add(this.label1);
      this.Episodes.Controls.Add(this.txtSeriesId2);
      this.Episodes.Controls.Add(this.txtEpisode);
      this.Episodes.Controls.Add(this.txtSeason);
      this.Episodes.Location = new System.Drawing.Point(6, 125);
      this.Episodes.Name = "Episodes";
      this.Episodes.Size = new System.Drawing.Size(200, 100);
      this.Episodes.TabIndex = 9;
      this.Episodes.TabStop = false;
      this.Episodes.Text = "Episodes";
      // 
      // cbOrdering
      // 
      this.cbOrdering.FormattingEnabled = true;
      this.cbOrdering.Location = new System.Drawing.Point(10, 63);
      this.cbOrdering.Name = "cbOrdering";
      this.cbOrdering.Size = new System.Drawing.Size(64, 21);
      this.cbOrdering.TabIndex = 11;
      // 
      // cmdGetEpisodes
      // 
      this.cmdGetEpisodes.Enabled = false;
      this.cmdGetEpisodes.Location = new System.Drawing.Point(80, 63);
      this.cmdGetEpisodes.Name = "cmdGetEpisodes";
      this.cmdGetEpisodes.Size = new System.Drawing.Size(78, 23);
      this.cmdGetEpisodes.TabIndex = 10;
      this.cmdGetEpisodes.Text = "Get Episode";
      this.cmdGetEpisodes.UseVisualStyleBackColor = true;
      this.cmdGetEpisodes.Click += new System.EventHandler(this.cmdGetEpisodes_Click);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(106, 21);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(45, 13);
      this.label3.TabIndex = 9;
      this.label3.Text = "Episode";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(59, 21);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(43, 13);
      this.label2.TabIndex = 9;
      this.label2.Text = "Season";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(8, 21);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(36, 13);
      this.label1.TabIndex = 9;
      this.label1.Text = "Series";
      // 
      // cmdGetAllSeriesRatings
      // 
      this.cmdGetAllSeriesRatings.Enabled = false;
      this.cmdGetAllSeriesRatings.Location = new System.Drawing.Point(6, 231);
      this.cmdGetAllSeriesRatings.Name = "cmdGetAllSeriesRatings";
      this.cmdGetAllSeriesRatings.Size = new System.Drawing.Size(200, 24);
      this.cmdGetAllSeriesRatings.TabIndex = 10;
      this.cmdGetAllSeriesRatings.Text = "Get all rated series";
      this.cmdGetAllSeriesRatings.UseVisualStyleBackColor = true;
      this.cmdGetAllSeriesRatings.Click += new System.EventHandler(this.cmdGetAllSeriesRatings_Click);
      // 
      // cmdSetUser
      // 
      this.cmdSetUser.Enabled = false;
      this.cmdSetUser.Location = new System.Drawing.Point(147, 38);
      this.cmdSetUser.Name = "cmdSetUser";
      this.cmdSetUser.Size = new System.Drawing.Size(59, 23);
      this.cmdSetUser.TabIndex = 2;
      this.cmdSetUser.Text = "Set User";
      this.cmdSetUser.UseVisualStyleBackColor = true;
      this.cmdSetUser.Click += new System.EventHandler(this.cmdSetUser_Click);
      // 
      // txtUserId
      // 
      this.txtUserId.Location = new System.Drawing.Point(22, 40);
      this.txtUserId.Name = "txtUserId";
      this.txtUserId.Size = new System.Drawing.Size(119, 20);
      this.txtUserId.TabIndex = 7;
      // 
      // cmdGetRatingsForSeries
      // 
      this.cmdGetRatingsForSeries.Enabled = false;
      this.cmdGetRatingsForSeries.Location = new System.Drawing.Point(109, 260);
      this.cmdGetRatingsForSeries.Name = "cmdGetRatingsForSeries";
      this.cmdGetRatingsForSeries.Size = new System.Drawing.Size(97, 23);
      this.cmdGetRatingsForSeries.TabIndex = 11;
      this.cmdGetRatingsForSeries.Text = "Get series ratings";
      this.cmdGetRatingsForSeries.UseVisualStyleBackColor = true;
      this.cmdGetRatingsForSeries.Click += new System.EventHandler(this.button1_Click);
      // 
      // txtSeriesRatingsId
      // 
      this.txtSeriesRatingsId.Location = new System.Drawing.Point(6, 263);
      this.txtSeriesRatingsId.Name = "txtSeriesRatingsId";
      this.txtSeriesRatingsId.Size = new System.Drawing.Size(102, 20);
      this.txtSeriesRatingsId.TabIndex = 8;
      this.txtSeriesRatingsId.Text = "73255";
      // 
      // dateTimePickerEpAired
      // 
      this.dateTimePickerEpAired.Location = new System.Drawing.Point(5, 45);
      this.dateTimePickerEpAired.Name = "dateTimePickerEpAired";
      this.dateTimePickerEpAired.Size = new System.Drawing.Size(189, 20);
      this.dateTimePickerEpAired.TabIndex = 12;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.cmdGetEpisodeAired);
      this.groupBox1.Controls.Add(this.label4);
      this.groupBox1.Controls.Add(this.dateTimePickerEpAired);
      this.groupBox1.Controls.Add(this.txtSeriesEpisodeAiredId);
      this.groupBox1.Location = new System.Drawing.Point(6, 290);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(200, 100);
      this.groupBox1.TabIndex = 13;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Episode Aired";
      // 
      // cmdGetEpisodeAired
      // 
      this.cmdGetEpisodeAired.Enabled = false;
      this.cmdGetEpisodeAired.Location = new System.Drawing.Point(5, 71);
      this.cmdGetEpisodeAired.Name = "cmdGetEpisodeAired";
      this.cmdGetEpisodeAired.Size = new System.Drawing.Size(189, 23);
      this.cmdGetEpisodeAired.TabIndex = 14;
      this.cmdGetEpisodeAired.Text = "Get Episode";
      this.cmdGetEpisodeAired.UseVisualStyleBackColor = true;
      this.cmdGetEpisodeAired.Click += new System.EventHandler(this.cmdGetEpisodeAired_Click);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(8, 22);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(48, 13);
      this.label4.TabIndex = 13;
      this.label4.Text = "Series Id";
      // 
      // txtSeriesEpisodeAiredId
      // 
      this.txtSeriesEpisodeAiredId.Location = new System.Drawing.Point(125, 19);
      this.txtSeriesEpisodeAiredId.Name = "txtSeriesEpisodeAiredId";
      this.txtSeriesEpisodeAiredId.Size = new System.Drawing.Size(69, 20);
      this.txtSeriesEpisodeAiredId.TabIndex = 7;
      this.txtSeriesEpisodeAiredId.Text = "73255";
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.rbFanartVignette);
      this.groupBox2.Controls.Add(this.cmdBannerTestingPrev);
      this.groupBox2.Controls.Add(this.cmdBannerTestingNext);
      this.groupBox2.Controls.Add(this.cmdLoadBannerTest);
      this.groupBox2.Controls.Add(this.rbLoadEpisodeBannerThumb);
      this.groupBox2.Controls.Add(this.rbLoadPosterThumb);
      this.groupBox2.Controls.Add(this.rbLoadEpisodeBanner);
      this.groupBox2.Controls.Add(this.rbLoadPoster);
      this.groupBox2.Controls.Add(this.rbLoadSeasonThumb);
      this.groupBox2.Controls.Add(this.rbLoadFanartThumb);
      this.groupBox2.Controls.Add(this.rbLoadSeason);
      this.groupBox2.Controls.Add(this.rbLoadFanart);
      this.groupBox2.Controls.Add(this.rbLoadSeriesBannerThumb);
      this.groupBox2.Controls.Add(this.rbLoadSeriesBanner);
      this.groupBox2.Controls.Add(this.txtSeriesIdForBanners);
      this.groupBox2.Controls.Add(this.label5);
      this.groupBox2.Location = new System.Drawing.Point(6, 396);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(200, 345);
      this.groupBox2.TabIndex = 14;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Banners testing";
      // 
      // cmdBannerTestingPrev
      // 
      this.cmdBannerTestingPrev.Enabled = false;
      this.cmdBannerTestingPrev.Location = new System.Drawing.Point(10, 316);
      this.cmdBannerTestingPrev.Name = "cmdBannerTestingPrev";
      this.cmdBannerTestingPrev.Size = new System.Drawing.Size(29, 23);
      this.cmdBannerTestingPrev.TabIndex = 18;
      this.cmdBannerTestingPrev.Text = "<<";
      this.cmdBannerTestingPrev.UseVisualStyleBackColor = true;
      this.cmdBannerTestingPrev.Click += new System.EventHandler(this.cmdBannerTestingPrev_Click);
      // 
      // cmdBannerTestingNext
      // 
      this.cmdBannerTestingNext.Enabled = false;
      this.cmdBannerTestingNext.Location = new System.Drawing.Point(164, 316);
      this.cmdBannerTestingNext.Name = "cmdBannerTestingNext";
      this.cmdBannerTestingNext.Size = new System.Drawing.Size(29, 23);
      this.cmdBannerTestingNext.TabIndex = 18;
      this.cmdBannerTestingNext.Text = ">>";
      this.cmdBannerTestingNext.UseVisualStyleBackColor = true;
      this.cmdBannerTestingNext.Click += new System.EventHandler(this.cmdBannerTestingNext_Click);
      // 
      // cmdLoadBannerTest
      // 
      this.cmdLoadBannerTest.Enabled = false;
      this.cmdLoadBannerTest.Location = new System.Drawing.Point(43, 316);
      this.cmdLoadBannerTest.Name = "cmdLoadBannerTest";
      this.cmdLoadBannerTest.Size = new System.Drawing.Size(115, 23);
      this.cmdLoadBannerTest.TabIndex = 17;
      this.cmdLoadBannerTest.Text = "Load";
      this.cmdLoadBannerTest.UseVisualStyleBackColor = true;
      this.cmdLoadBannerTest.Click += new System.EventHandler(this.cmdLoadBannerTest_Click);
      // 
      // rbLoadEpisodeBannerThumb
      // 
      this.rbLoadEpisodeBannerThumb.AutoSize = true;
      this.rbLoadEpisodeBannerThumb.Location = new System.Drawing.Point(50, 274);
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
      this.rbLoadPosterThumb.Location = new System.Drawing.Point(50, 182);
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
      this.rbLoadEpisodeBanner.Location = new System.Drawing.Point(50, 251);
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
      this.rbLoadPoster.Location = new System.Drawing.Point(50, 159);
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
      this.rbLoadSeasonThumb.Location = new System.Drawing.Point(50, 228);
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
      this.rbLoadFanartThumb.Location = new System.Drawing.Point(50, 115);
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
      this.rbLoadSeason.Location = new System.Drawing.Point(50, 205);
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
      this.rbLoadFanart.Location = new System.Drawing.Point(50, 92);
      this.rbLoadFanart.Name = "rbLoadFanart";
      this.rbLoadFanart.Size = new System.Drawing.Size(55, 17);
      this.rbLoadFanart.TabIndex = 16;
      this.rbLoadFanart.TabStop = true;
      this.rbLoadFanart.Text = "Fanart";
      this.rbLoadFanart.UseVisualStyleBackColor = true;
      // 
      // rbLoadSeriesBannerThumb
      // 
      this.rbLoadSeriesBannerThumb.AutoSize = true;
      this.rbLoadSeriesBannerThumb.Location = new System.Drawing.Point(50, 69);
      this.rbLoadSeriesBannerThumb.Name = "rbLoadSeriesBannerThumb";
      this.rbLoadSeriesBannerThumb.Size = new System.Drawing.Size(127, 17);
      this.rbLoadSeriesBannerThumb.TabIndex = 16;
      this.rbLoadSeriesBannerThumb.TabStop = true;
      this.rbLoadSeriesBannerThumb.Text = "Series Banner Thumb";
      this.rbLoadSeriesBannerThumb.UseVisualStyleBackColor = true;
      // 
      // rbLoadSeriesBanner
      // 
      this.rbLoadSeriesBanner.AutoSize = true;
      this.rbLoadSeriesBanner.Location = new System.Drawing.Point(50, 46);
      this.rbLoadSeriesBanner.Name = "rbLoadSeriesBanner";
      this.rbLoadSeriesBanner.Size = new System.Drawing.Size(91, 17);
      this.rbLoadSeriesBanner.TabIndex = 16;
      this.rbLoadSeriesBanner.TabStop = true;
      this.rbLoadSeriesBanner.Text = "Series Banner";
      this.rbLoadSeriesBanner.UseVisualStyleBackColor = true;
      // 
      // txtSeriesIdForBanners
      // 
      this.txtSeriesIdForBanners.Location = new System.Drawing.Point(94, 19);
      this.txtSeriesIdForBanners.Name = "txtSeriesIdForBanners";
      this.txtSeriesIdForBanners.Size = new System.Drawing.Size(100, 20);
      this.txtSeriesIdForBanners.TabIndex = 15;
      this.txtSeriesIdForBanners.Text = "73255";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(13, 22);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(48, 13);
      this.label5.TabIndex = 13;
      this.label5.Text = "Series Id";
      // 
      // pbBannerTesting
      // 
      this.pbBannerTesting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.pbBannerTesting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
      this.pbBannerTesting.Location = new System.Drawing.Point(226, 9);
      this.pbBannerTesting.Name = "pbBannerTesting";
      this.pbBannerTesting.Size = new System.Drawing.Size(733, 770);
      this.pbBannerTesting.TabIndex = 15;
      this.pbBannerTesting.TabStop = false;
      this.pbBannerTesting.Visible = false;
      // 
      // bcSeriesBanner
      // 
      this.bcSeriesBanner.BackColor = System.Drawing.SystemColors.ControlDark;
      this.bcSeriesBanner.BannerImage = null;
      this.bcSeriesBanner.BannerImages = null;
      this.bcSeriesBanner.DefaultImage = null;
      this.bcSeriesBanner.ImageSizeMode = System.Windows.Forms.ImageLayout.Zoom;
      this.bcSeriesBanner.Index = 0;
      this.bcSeriesBanner.LoadingBackgroundColor = System.Drawing.Color.Transparent;
      this.bcSeriesBanner.LoadingImage = null;
      this.bcSeriesBanner.Location = new System.Drawing.Point(302, 9);
      this.bcSeriesBanner.Name = "bcSeriesBanner";
      this.bcSeriesBanner.Size = new System.Drawing.Size(348, 81);
      this.bcSeriesBanner.TabIndex = 6;
      this.bcSeriesBanner.UnavailableImage = null;
      this.bcSeriesBanner.UseThumb = false;
      // 
      // bcActors
      // 
      this.bcActors.BackColor = System.Drawing.SystemColors.AppWorkspace;
      this.bcActors.BannerImage = null;
      this.bcActors.BannerImages = null;
      this.bcActors.DefaultImage = null;
      this.bcActors.ImageSizeMode = System.Windows.Forms.ImageLayout.Zoom;
      this.bcActors.Index = 0;
      this.bcActors.LoadingBackgroundColor = System.Drawing.Color.Black;
      this.bcActors.LoadingImage = global::TvdbTester.Properties.Resources.loader4;
      this.bcActors.Location = new System.Drawing.Point(665, 9);
      this.bcActors.Name = "bcActors";
      this.bcActors.Size = new System.Drawing.Size(300, 450);
      this.bcActors.TabIndex = 1;
      this.bcActors.UnavailableImage = null;
      this.bcActors.UseThumb = false;
      // 
      // rbFanartVignette
      // 
      this.rbFanartVignette.AutoSize = true;
      this.rbFanartVignette.Location = new System.Drawing.Point(50, 136);
      this.rbFanartVignette.Name = "rbFanartVignette";
      this.rbFanartVignette.Size = new System.Drawing.Size(97, 17);
      this.rbFanartVignette.TabIndex = 19;
      this.rbFanartVignette.TabStop = true;
      this.rbFanartVignette.Text = "Fanart Vignette";
      this.rbFanartVignette.UseVisualStyleBackColor = true;
      // 
      // TestForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(960, 782);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.cmdGetRatingsForSeries);
      this.Controls.Add(this.cmdGetAllSeriesRatings);
      this.Controls.Add(this.Episodes);
      this.Controls.Add(this.txtUserId);
      this.Controls.Add(this.txtSeriesId);
      this.Controls.Add(this.txtSeriesRatingsId);
      this.Controls.Add(this.bcSeriesBanner);
      this.Controls.Add(this.lvSeries);
      this.Controls.Add(this.cmdEnd);
      this.Controls.Add(this.cmdInit);
      this.Controls.Add(this.cmdSetUser);
      this.Controls.Add(this.cmdTestZip);
      this.Controls.Add(this.bcActors);
      this.Controls.Add(this.cmdTest1);
      this.Controls.Add(this.pbBannerTesting);
      this.Name = "TestForm";
      this.Text = "TestForm";
      this.Episodes.ResumeLayout(false);
      this.Episodes.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbBannerTesting)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button cmdTest1;
    private BannerControl bcActors;
    private System.Windows.Forms.Button cmdTestZip;
    private System.Windows.Forms.Button cmdInit;
    private System.Windows.Forms.Button cmdEnd;
    private System.Windows.Forms.ListView lvSeries;
    private System.Windows.Forms.ColumnHeader chProperty;
    private System.Windows.Forms.ColumnHeader chValue;
    private BannerControl bcSeriesBanner;
    private System.Windows.Forms.TextBox txtSeriesId;
    private System.Windows.Forms.TextBox txtSeriesId2;
    private System.Windows.Forms.TextBox txtSeason;
    private System.Windows.Forms.TextBox txtEpisode;
    private System.Windows.Forms.GroupBox Episodes;
    private System.Windows.Forms.ComboBox cbOrdering;
    private System.Windows.Forms.Button cmdGetEpisodes;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button cmdGetAllSeriesRatings;
    private System.Windows.Forms.Button cmdSetUser;
    private System.Windows.Forms.TextBox txtUserId;
    private System.Windows.Forms.Button cmdGetRatingsForSeries;
    private System.Windows.Forms.TextBox txtSeriesRatingsId;
    private System.Windows.Forms.DateTimePicker dateTimePickerEpAired;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Button cmdGetEpisodeAired;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox txtSeriesEpisodeAiredId;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Button cmdLoadBannerTest;
    private System.Windows.Forms.RadioButton rbLoadPosterThumb;
    private System.Windows.Forms.RadioButton rbLoadPoster;
    private System.Windows.Forms.RadioButton rbLoadFanartThumb;
    private System.Windows.Forms.RadioButton rbLoadFanart;
    private System.Windows.Forms.RadioButton rbLoadSeriesBannerThumb;
    private System.Windows.Forms.RadioButton rbLoadSeriesBanner;
    private System.Windows.Forms.TextBox txtSeriesIdForBanners;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Button cmdBannerTestingPrev;
    private System.Windows.Forms.Button cmdBannerTestingNext;
    private System.Windows.Forms.PictureBox pbBannerTesting;
    private System.Windows.Forms.RadioButton rbLoadEpisodeBannerThumb;
    private System.Windows.Forms.RadioButton rbLoadEpisodeBanner;
    private System.Windows.Forms.RadioButton rbLoadSeasonThumb;
    private System.Windows.Forms.RadioButton rbLoadSeason;
    private System.Windows.Forms.RadioButton rbFanartVignette;
  }
}