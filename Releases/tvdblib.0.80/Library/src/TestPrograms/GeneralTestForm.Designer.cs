using TestPrograms.Properties;
namespace TvdbTester
{
  partial class GeneralTestForm
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
      this.cmdShowEpisodesAbsoluteOrdering = new System.Windows.Forms.Button();
      this.cmdShowEpisodesInGivenOrder = new System.Windows.Forms.Button();
      this.label6 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.txtSeriesIdEpisodeOrdering = new System.Windows.Forms.TextBox();
      this.rbDvd = new System.Windows.Forms.RadioButton();
      this.rbAired = new System.Windows.Forms.RadioButton();
      this.bcSeriesBanner = new TvdbTester.BannerControl();
      this.bcActors = new TvdbTester.BannerControl();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.comboBox1 = new System.Windows.Forms.ComboBox();
      this.txtExternalId = new System.Windows.Forms.TextBox();
      this.cmdTestGetSeriesByExternalId = new System.Windows.Forms.Button();
      this.Episodes.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox3.SuspendLayout();
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
      this.Episodes.Location = new System.Drawing.Point(6, 391);
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
      this.cmdGetAllSeriesRatings.Location = new System.Drawing.Point(6, 497);
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
      this.cmdGetRatingsForSeries.Location = new System.Drawing.Point(109, 526);
      this.cmdGetRatingsForSeries.Name = "cmdGetRatingsForSeries";
      this.cmdGetRatingsForSeries.Size = new System.Drawing.Size(97, 23);
      this.cmdGetRatingsForSeries.TabIndex = 11;
      this.cmdGetRatingsForSeries.Text = "Get series ratings";
      this.cmdGetRatingsForSeries.UseVisualStyleBackColor = true;
      this.cmdGetRatingsForSeries.Click += new System.EventHandler(this.button1_Click);
      // 
      // txtSeriesRatingsId
      // 
      this.txtSeriesRatingsId.Location = new System.Drawing.Point(6, 529);
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
      this.groupBox1.Location = new System.Drawing.Point(6, 556);
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
      this.groupBox2.Controls.Add(this.cmdShowEpisodesAbsoluteOrdering);
      this.groupBox2.Controls.Add(this.cmdShowEpisodesInGivenOrder);
      this.groupBox2.Controls.Add(this.label6);
      this.groupBox2.Controls.Add(this.label5);
      this.groupBox2.Controls.Add(this.textBox1);
      this.groupBox2.Controls.Add(this.txtSeriesIdEpisodeOrdering);
      this.groupBox2.Controls.Add(this.rbDvd);
      this.groupBox2.Controls.Add(this.rbAired);
      this.groupBox2.Location = new System.Drawing.Point(6, 125);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(247, 122);
      this.groupBox2.TabIndex = 15;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Episode Ordering";
      // 
      // cmdShowEpisodesAbsoluteOrdering
      // 
      this.cmdShowEpisodesAbsoluteOrdering.Location = new System.Drawing.Point(6, 93);
      this.cmdShowEpisodesAbsoluteOrdering.Name = "cmdShowEpisodesAbsoluteOrdering";
      this.cmdShowEpisodesAbsoluteOrdering.Size = new System.Drawing.Size(231, 23);
      this.cmdShowEpisodesAbsoluteOrdering.TabIndex = 18;
      this.cmdShowEpisodesAbsoluteOrdering.Text = "Show Absolute Episodes";
      this.cmdShowEpisodesAbsoluteOrdering.UseVisualStyleBackColor = true;
      this.cmdShowEpisodesAbsoluteOrdering.Click += new System.EventHandler(this.cmdShowEpisodesAbsoluteOrdering_Click);
      // 
      // cmdShowEpisodesInGivenOrder
      // 
      this.cmdShowEpisodesInGivenOrder.Location = new System.Drawing.Point(6, 67);
      this.cmdShowEpisodesInGivenOrder.Name = "cmdShowEpisodesInGivenOrder";
      this.cmdShowEpisodesInGivenOrder.Size = new System.Drawing.Size(231, 23);
      this.cmdShowEpisodesInGivenOrder.TabIndex = 17;
      this.cmdShowEpisodesInGivenOrder.Text = "Show Episodes";
      this.cmdShowEpisodesInGivenOrder.UseVisualStyleBackColor = true;
      this.cmdShowEpisodesInGivenOrder.Click += new System.EventHandler(this.cmdShowEpisodesInGivenOrder_Click);
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(9, 21);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(48, 13);
      this.label6.TabIndex = 13;
      this.label6.Text = "Series Id";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(139, 46);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(46, 13);
      this.label5.TabIndex = 16;
      this.label5.Text = "Season:";
      // 
      // textBox1
      // 
      this.textBox1.Location = new System.Drawing.Point(186, 43);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(51, 20);
      this.textBox1.TabIndex = 15;
      // 
      // txtSeriesIdEpisodeOrdering
      // 
      this.txtSeriesIdEpisodeOrdering.Location = new System.Drawing.Point(126, 18);
      this.txtSeriesIdEpisodeOrdering.Name = "txtSeriesIdEpisodeOrdering";
      this.txtSeriesIdEpisodeOrdering.Size = new System.Drawing.Size(111, 20);
      this.txtSeriesIdEpisodeOrdering.TabIndex = 7;
      this.txtSeriesIdEpisodeOrdering.Text = "78874";
      // 
      // rbDvd
      // 
      this.rbDvd.AutoSize = true;
      this.rbDvd.Location = new System.Drawing.Point(81, 44);
      this.rbDvd.Name = "rbDvd";
      this.rbDvd.Size = new System.Drawing.Size(45, 17);
      this.rbDvd.TabIndex = 14;
      this.rbDvd.Text = "Dvd";
      this.rbDvd.UseVisualStyleBackColor = true;
      // 
      // rbAired
      // 
      this.rbAired.AutoSize = true;
      this.rbAired.Checked = true;
      this.rbAired.Location = new System.Drawing.Point(11, 44);
      this.rbAired.Name = "rbAired";
      this.rbAired.Size = new System.Drawing.Size(49, 17);
      this.rbAired.TabIndex = 14;
      this.rbAired.TabStop = true;
      this.rbAired.Text = "Aired";
      this.rbAired.UseVisualStyleBackColor = true;
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
      this.bcActors.LoadingImage = global::TestPrograms.Properties.Resources.loader4;
      this.bcActors.Location = new System.Drawing.Point(665, 9);
      this.bcActors.Name = "bcActors";
      this.bcActors.Size = new System.Drawing.Size(300, 450);
      this.bcActors.TabIndex = 1;
      this.bcActors.UnavailableImage = null;
      this.bcActors.UseThumb = false;
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.cmdTestGetSeriesByExternalId);
      this.groupBox3.Controls.Add(this.txtExternalId);
      this.groupBox3.Controls.Add(this.comboBox1);
      this.groupBox3.Location = new System.Drawing.Point(6, 276);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(247, 109);
      this.groupBox3.TabIndex = 16;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "External IDs";
      // 
      // comboBox1
      // 
      this.comboBox1.FormattingEnabled = true;
      this.comboBox1.Items.AddRange(new object[] {
            "ImdbId"});
      this.comboBox1.Location = new System.Drawing.Point(5, 19);
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.Size = new System.Drawing.Size(231, 21);
      this.comboBox1.TabIndex = 0;
      // 
      // txtExternalId
      // 
      this.txtExternalId.Location = new System.Drawing.Point(6, 47);
      this.txtExternalId.Name = "txtExternalId";
      this.txtExternalId.Size = new System.Drawing.Size(230, 20);
      this.txtExternalId.TabIndex = 1;
      this.txtExternalId.Text = "tt0411008";
      // 
      // cmdTestGetSeriesByExternalId
      // 
      this.cmdTestGetSeriesByExternalId.Location = new System.Drawing.Point(6, 73);
      this.cmdTestGetSeriesByExternalId.Name = "cmdTestGetSeriesByExternalId";
      this.cmdTestGetSeriesByExternalId.Size = new System.Drawing.Size(230, 23);
      this.cmdTestGetSeriesByExternalId.TabIndex = 2;
      this.cmdTestGetSeriesByExternalId.Text = "Test External Id";
      this.cmdTestGetSeriesByExternalId.UseVisualStyleBackColor = true;
      this.cmdTestGetSeriesByExternalId.Click += new System.EventHandler(this.cmdTestGetSeriesByExternalId_Click);
      // 
      // TestForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(960, 782);
      this.Controls.Add(this.groupBox3);
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
      this.Name = "TestForm";
      this.Text = "TestForm";
      this.Episodes.ResumeLayout(false);
      this.Episodes.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
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
    private System.Windows.Forms.Button cmdShowEpisodesInGivenOrder;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.RadioButton rbDvd;
    private System.Windows.Forms.RadioButton rbAired;
    private System.Windows.Forms.Button cmdShowEpisodesAbsoluteOrdering;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox txtSeriesIdEpisodeOrdering;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.TextBox txtExternalId;
    private System.Windows.Forms.ComboBox comboBox1;
    private System.Windows.Forms.Button cmdTestGetSeriesByExternalId;
  }
}