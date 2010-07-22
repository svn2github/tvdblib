namespace TestPrograms
{
  partial class UpdatingTestForm
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
      this.components = new System.ComponentModel.Container();
      this.lvSeriesDetails = new System.Windows.Forms.ListView();
      this.chPropertyName = new System.Windows.Forms.ColumnHeader();
      this.chBeforeUpdate = new System.Windows.Forms.ColumnHeader();
      this.chAfterUpdate = new System.Windows.Forms.ColumnHeader();
      this.chCurrentValue = new System.Windows.Forms.ColumnHeader();
      this.cmDetailsView = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.findEpisodeIDInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.updatesmonthxmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.updatesweekxmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.updatesdayxmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.saveToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.cmdCompareValuesDetailed = new System.Windows.Forms.Button();
      this.cmdComareValues = new System.Windows.Forms.Button();
      this.cmdShowUpdateLog = new System.Windows.Forms.Button();
      this.cmdInitTvdbHandler = new System.Windows.Forms.Button();
      this.txtCacheLocation = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.cbCacheType = new System.Windows.Forms.ComboBox();
      this.label4 = new System.Windows.Forms.Label();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.cmdAddAllFavorites = new System.Windows.Forms.Button();
      this.textBox4 = new System.Windows.Forms.TextBox();
      this.cmdAddSeriesById = new System.Windows.Forms.Button();
      this.label5 = new System.Windows.Forms.Label();
      this.txtLastUpdated = new System.Windows.Forms.TextBox();
      this.cmdUpdate = new System.Windows.Forms.Button();
      this.cbDownloadCurrentVersion = new System.Windows.Forms.CheckBox();
      this.rbUpdateMonth = new System.Windows.Forms.RadioButton();
      this.rbUpdateWeek = new System.Windows.Forms.RadioButton();
      this.rbUpdateDay = new System.Windows.Forms.RadioButton();
      this.rbUpdateAutomatic = new System.Windows.Forms.RadioButton();
      this.cmdBack = new System.Windows.Forms.Button();
      this.lvCachedSeries = new System.Windows.Forms.ListView();
      this.chSeriesiD = new System.Windows.Forms.ColumnHeader();
      this.chSeriesName = new System.Windows.Forms.ColumnHeader();
      this.cmChachedSeries = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.checkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.checkSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.checkAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.saveListOfSeriesToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.lookUpNamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.txtCurrent = new System.Windows.Forms.RichTextBox();
      this.txtAfter = new System.Windows.Forms.RichTextBox();
      this.txtBefore = new System.Windows.Forms.RichTextBox();
      this.Before = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.label9 = new System.Windows.Forms.Label();
      this.cbReloadOld = new System.Windows.Forms.CheckBox();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.groupBox4 = new System.Windows.Forms.GroupBox();
      this.cmdMakeSnapshotOfCache = new System.Windows.Forms.Button();
      this.lbCacheSnapshots = new System.Windows.Forms.ListBox();
      this.cmCacheRevisions = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.deleteRevisionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.revertToThisRevisionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.compareBetweenTheseVersionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.downloadUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.monthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.weekToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.dayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.cmDetailsView.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.cmChachedSeries.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox4.SuspendLayout();
      this.cmCacheRevisions.SuspendLayout();
      this.SuspendLayout();
      // 
      // lvSeriesDetails
      // 
      this.lvSeriesDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chPropertyName,
            this.chBeforeUpdate,
            this.chAfterUpdate,
            this.chCurrentValue});
      this.lvSeriesDetails.ContextMenuStrip = this.cmDetailsView;
      this.lvSeriesDetails.FullRowSelect = true;
      this.lvSeriesDetails.Location = new System.Drawing.Point(190, 39);
      this.lvSeriesDetails.Name = "lvSeriesDetails";
      this.lvSeriesDetails.Size = new System.Drawing.Size(778, 355);
      this.lvSeriesDetails.TabIndex = 0;
      this.lvSeriesDetails.UseCompatibleStateImageBehavior = false;
      this.lvSeriesDetails.View = System.Windows.Forms.View.Details;
      this.lvSeriesDetails.DoubleClick += new System.EventHandler(this.lvSeriesDetails_DoubleClick);
      // 
      // chPropertyName
      // 
      this.chPropertyName.Text = "";
      this.chPropertyName.Width = 107;
      // 
      // chBeforeUpdate
      // 
      this.chBeforeUpdate.Text = "Value before Update";
      this.chBeforeUpdate.Width = 218;
      // 
      // chAfterUpdate
      // 
      this.chAfterUpdate.Text = "Value After Update";
      this.chAfterUpdate.Width = 215;
      // 
      // chCurrentValue
      // 
      this.chCurrentValue.Text = "Current value on thetvdb";
      this.chCurrentValue.Width = 239;
      // 
      // cmDetailsView
      // 
      this.cmDetailsView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findEpisodeIDInToolStripMenuItem,
            this.saveToFileToolStripMenuItem,
            this.downloadUpdatesToolStripMenuItem});
      this.cmDetailsView.Name = "cmDetailsView";
      this.cmDetailsView.Size = new System.Drawing.Size(197, 92);
      // 
      // findEpisodeIDInToolStripMenuItem
      // 
      this.findEpisodeIDInToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updatesmonthxmlToolStripMenuItem,
            this.updatesweekxmlToolStripMenuItem,
            this.updatesdayxmlToolStripMenuItem});
      this.findEpisodeIDInToolStripMenuItem.Name = "findEpisodeIDInToolStripMenuItem";
      this.findEpisodeIDInToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
      this.findEpisodeIDInToolStripMenuItem.Text = "Find Episode ID in ..";
      // 
      // updatesmonthxmlToolStripMenuItem
      // 
      this.updatesmonthxmlToolStripMenuItem.Enabled = false;
      this.updatesmonthxmlToolStripMenuItem.Name = "updatesmonthxmlToolStripMenuItem";
      this.updatesmonthxmlToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
      this.updatesmonthxmlToolStripMenuItem.Text = "updates_month.xml";
      this.updatesmonthxmlToolStripMenuItem.Click += new System.EventHandler(this.updatesmonthxmlToolStripMenuItem_Click);
      // 
      // updatesweekxmlToolStripMenuItem
      // 
      this.updatesweekxmlToolStripMenuItem.Enabled = false;
      this.updatesweekxmlToolStripMenuItem.Name = "updatesweekxmlToolStripMenuItem";
      this.updatesweekxmlToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
      this.updatesweekxmlToolStripMenuItem.Text = "updates_week.xml";
      this.updatesweekxmlToolStripMenuItem.Click += new System.EventHandler(this.updatesweekxmlToolStripMenuItem_Click);
      // 
      // updatesdayxmlToolStripMenuItem
      // 
      this.updatesdayxmlToolStripMenuItem.Enabled = false;
      this.updatesdayxmlToolStripMenuItem.Name = "updatesdayxmlToolStripMenuItem";
      this.updatesdayxmlToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
      this.updatesdayxmlToolStripMenuItem.Text = "updates_day.xml";
      this.updatesdayxmlToolStripMenuItem.Click += new System.EventHandler(this.updatesdayxmlToolStripMenuItem_Click);
      // 
      // saveToFileToolStripMenuItem
      // 
      this.saveToFileToolStripMenuItem.Name = "saveToFileToolStripMenuItem";
      this.saveToFileToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
      this.saveToFileToolStripMenuItem.Text = "Save to File";
      this.saveToFileToolStripMenuItem.Click += new System.EventHandler(this.saveToFileToolStripMenuItem_Click);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.cmdCompareValuesDetailed);
      this.groupBox1.Controls.Add(this.cmdComareValues);
      this.groupBox1.Controls.Add(this.cmdShowUpdateLog);
      this.groupBox1.Location = new System.Drawing.Point(774, 614);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(194, 180);
      this.groupBox1.TabIndex = 2;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Analyze Results";
      // 
      // cmdCompareValuesDetailed
      // 
      this.cmdCompareValuesDetailed.Location = new System.Drawing.Point(16, 79);
      this.cmdCompareValuesDetailed.Name = "cmdCompareValuesDetailed";
      this.cmdCompareValuesDetailed.Size = new System.Drawing.Size(160, 23);
      this.cmdCompareValuesDetailed.TabIndex = 0;
      this.cmdCompareValuesDetailed.Text = "Compare Series and Episodes";
      this.cmdCompareValuesDetailed.UseVisualStyleBackColor = true;
      this.cmdCompareValuesDetailed.Click += new System.EventHandler(this.cmdCompareValuesDetailed_Click);
      // 
      // cmdComareValues
      // 
      this.cmdComareValues.Location = new System.Drawing.Point(16, 50);
      this.cmdComareValues.Name = "cmdComareValues";
      this.cmdComareValues.Size = new System.Drawing.Size(160, 23);
      this.cmdComareValues.TabIndex = 0;
      this.cmdComareValues.Text = "Compare Series only";
      this.cmdComareValues.UseVisualStyleBackColor = true;
      this.cmdComareValues.Click += new System.EventHandler(this.cmdComareValues_Click);
      // 
      // cmdShowUpdateLog
      // 
      this.cmdShowUpdateLog.Location = new System.Drawing.Point(16, 21);
      this.cmdShowUpdateLog.Name = "cmdShowUpdateLog";
      this.cmdShowUpdateLog.Size = new System.Drawing.Size(160, 23);
      this.cmdShowUpdateLog.TabIndex = 0;
      this.cmdShowUpdateLog.Text = "Show Update Log";
      this.cmdShowUpdateLog.UseVisualStyleBackColor = true;
      // 
      // cmdInitTvdbHandler
      // 
      this.cmdInitTvdbHandler.Location = new System.Drawing.Point(16, 11);
      this.cmdInitTvdbHandler.Name = "cmdInitTvdbHandler";
      this.cmdInitTvdbHandler.Size = new System.Drawing.Size(151, 23);
      this.cmdInitTvdbHandler.TabIndex = 3;
      this.cmdInitTvdbHandler.Text = "Initialise";
      this.cmdInitTvdbHandler.UseVisualStyleBackColor = true;
      this.cmdInitTvdbHandler.Click += new System.EventHandler(this.cmdInitTvdbHandler_Click);
      // 
      // txtCacheLocation
      // 
      this.txtCacheLocation.Location = new System.Drawing.Point(628, 13);
      this.txtCacheLocation.Name = "txtCacheLocation";
      this.txtCacheLocation.Size = new System.Drawing.Size(100, 20);
      this.txtCacheLocation.TabIndex = 8;
      this.txtCacheLocation.Text = "Cache";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(510, 16);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(82, 13);
      this.label3.TabIndex = 7;
      this.label3.Text = "Cache Location";
      // 
      // cbCacheType
      // 
      this.cbCacheType.FormattingEnabled = true;
      this.cbCacheType.Items.AddRange(new object[] {
            "Xml Caching",
            "Serialized Caching"});
      this.cbCacheType.Location = new System.Drawing.Point(847, 12);
      this.cbCacheType.Name = "cbCacheType";
      this.cbCacheType.Size = new System.Drawing.Size(121, 21);
      this.cbCacheType.TabIndex = 9;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(759, 16);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(65, 13);
      this.label4.TabIndex = 7;
      this.label4.Text = "Cache Type";
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.cmdAddAllFavorites);
      this.groupBox3.Controls.Add(this.textBox4);
      this.groupBox3.Controls.Add(this.cmdAddSeriesById);
      this.groupBox3.Location = new System.Drawing.Point(216, 616);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(314, 180);
      this.groupBox3.TabIndex = 2;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Load content and add it to cache";
      // 
      // cmdAddAllFavorites
      // 
      this.cmdAddAllFavorites.Location = new System.Drawing.Point(13, 47);
      this.cmdAddAllFavorites.Name = "cmdAddAllFavorites";
      this.cmdAddAllFavorites.Size = new System.Drawing.Size(276, 23);
      this.cmdAddAllFavorites.TabIndex = 2;
      this.cmdAddAllFavorites.Text = "Add all favorites to cache";
      this.cmdAddAllFavorites.UseVisualStyleBackColor = true;
      this.cmdAddAllFavorites.Click += new System.EventHandler(this.cmdAddAllFavorites_Click);
      // 
      // textBox4
      // 
      this.textBox4.Location = new System.Drawing.Point(13, 21);
      this.textBox4.Name = "textBox4";
      this.textBox4.Size = new System.Drawing.Size(187, 20);
      this.textBox4.TabIndex = 1;
      // 
      // cmdAddSeriesById
      // 
      this.cmdAddSeriesById.Location = new System.Drawing.Point(214, 19);
      this.cmdAddSeriesById.Name = "cmdAddSeriesById";
      this.cmdAddSeriesById.Size = new System.Drawing.Size(75, 23);
      this.cmdAddSeriesById.TabIndex = 0;
      this.cmdAddSeriesById.Text = "Add";
      this.cmdAddSeriesById.UseVisualStyleBackColor = true;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(188, 16);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(74, 13);
      this.label5.TabIndex = 10;
      this.label5.Text = "Last Updated:";
      // 
      // txtLastUpdated
      // 
      this.txtLastUpdated.Location = new System.Drawing.Point(268, 13);
      this.txtLastUpdated.Name = "txtLastUpdated";
      this.txtLastUpdated.ReadOnly = true;
      this.txtLastUpdated.Size = new System.Drawing.Size(161, 20);
      this.txtLastUpdated.TabIndex = 11;
      // 
      // cmdUpdate
      // 
      this.cmdUpdate.Location = new System.Drawing.Point(25, 145);
      this.cmdUpdate.Name = "cmdUpdate";
      this.cmdUpdate.Size = new System.Drawing.Size(158, 27);
      this.cmdUpdate.TabIndex = 12;
      this.cmdUpdate.Text = "1";
      this.cmdUpdate.UseVisualStyleBackColor = true;
      this.cmdUpdate.Click += new System.EventHandler(this.cmdUpdate_Click);
      // 
      // cbDownloadCurrentVersion
      // 
      this.cbDownloadCurrentVersion.AutoSize = true;
      this.cbDownloadCurrentVersion.Checked = true;
      this.cbDownloadCurrentVersion.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbDownloadCurrentVersion.Location = new System.Drawing.Point(15, 42);
      this.cbDownloadCurrentVersion.Name = "cbDownloadCurrentVersion";
      this.cbDownloadCurrentVersion.Size = new System.Drawing.Size(169, 17);
      this.cbDownloadCurrentVersion.TabIndex = 13;
      this.cbDownloadCurrentVersion.Text = "Download current version also";
      this.cbDownloadCurrentVersion.UseVisualStyleBackColor = true;
      // 
      // rbUpdateMonth
      // 
      this.rbUpdateMonth.AutoSize = true;
      this.rbUpdateMonth.Location = new System.Drawing.Point(118, 119);
      this.rbUpdateMonth.Name = "rbUpdateMonth";
      this.rbUpdateMonth.Size = new System.Drawing.Size(55, 17);
      this.rbUpdateMonth.TabIndex = 57;
      this.rbUpdateMonth.Text = "Month";
      this.rbUpdateMonth.UseVisualStyleBackColor = true;
      // 
      // rbUpdateWeek
      // 
      this.rbUpdateWeek.AutoSize = true;
      this.rbUpdateWeek.Location = new System.Drawing.Point(118, 96);
      this.rbUpdateWeek.Name = "rbUpdateWeek";
      this.rbUpdateWeek.Size = new System.Drawing.Size(54, 17);
      this.rbUpdateWeek.TabIndex = 58;
      this.rbUpdateWeek.Text = "Week";
      this.rbUpdateWeek.UseVisualStyleBackColor = true;
      // 
      // rbUpdateDay
      // 
      this.rbUpdateDay.AutoSize = true;
      this.rbUpdateDay.Location = new System.Drawing.Point(34, 119);
      this.rbUpdateDay.Name = "rbUpdateDay";
      this.rbUpdateDay.Size = new System.Drawing.Size(44, 17);
      this.rbUpdateDay.TabIndex = 55;
      this.rbUpdateDay.Text = "Day";
      this.rbUpdateDay.UseVisualStyleBackColor = true;
      // 
      // rbUpdateAutomatic
      // 
      this.rbUpdateAutomatic.AutoSize = true;
      this.rbUpdateAutomatic.Checked = true;
      this.rbUpdateAutomatic.Location = new System.Drawing.Point(34, 97);
      this.rbUpdateAutomatic.Name = "rbUpdateAutomatic";
      this.rbUpdateAutomatic.Size = new System.Drawing.Size(72, 17);
      this.rbUpdateAutomatic.TabIndex = 56;
      this.rbUpdateAutomatic.TabStop = true;
      this.rbUpdateAutomatic.Text = "Automatic";
      this.rbUpdateAutomatic.UseVisualStyleBackColor = true;
      // 
      // cmdBack
      // 
      this.cmdBack.Location = new System.Drawing.Point(190, 39);
      this.cmdBack.Name = "cmdBack";
      this.cmdBack.Size = new System.Drawing.Size(114, 23);
      this.cmdBack.TabIndex = 59;
      this.cmdBack.Text = "Back";
      this.cmdBack.UseVisualStyleBackColor = true;
      this.cmdBack.Click += new System.EventHandler(this.cmdBack_Click);
      // 
      // lvCachedSeries
      // 
      this.lvCachedSeries.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chSeriesiD,
            this.chSeriesName});
      this.lvCachedSeries.ContextMenuStrip = this.cmChachedSeries;
      this.lvCachedSeries.FullRowSelect = true;
      this.lvCachedSeries.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
      this.lvCachedSeries.Location = new System.Drawing.Point(15, 65);
      this.lvCachedSeries.Name = "lvCachedSeries";
      this.lvCachedSeries.Size = new System.Drawing.Size(169, 543);
      this.lvCachedSeries.TabIndex = 60;
      this.lvCachedSeries.UseCompatibleStateImageBehavior = false;
      this.lvCachedSeries.View = System.Windows.Forms.View.Details;
      this.lvCachedSeries.SelectedIndexChanged += new System.EventHandler(this.lvCachedSeries_SelectedIndexChanged);
      this.lvCachedSeries.DoubleClick += new System.EventHandler(this.lvCachedSeries_DoubleClick);
      // 
      // chSeriesiD
      // 
      this.chSeriesiD.Text = "ID";
      this.chSeriesiD.Width = 49;
      // 
      // chSeriesName
      // 
      this.chSeriesName.Text = "Name";
      this.chSeriesName.Width = 116;
      // 
      // cmChachedSeries
      // 
      this.cmChachedSeries.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkToolStripMenuItem,
            this.checkSelectedToolStripMenuItem,
            this.checkAllToolStripMenuItem,
            this.saveListOfSeriesToFileToolStripMenuItem,
            this.lookUpNamesToolStripMenuItem});
      this.cmChachedSeries.Name = "cmChachedSeries";
      this.cmChachedSeries.Size = new System.Drawing.Size(196, 114);
      // 
      // checkToolStripMenuItem
      // 
      this.checkToolStripMenuItem.Name = "checkToolStripMenuItem";
      this.checkToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
      this.checkToolStripMenuItem.Text = "Check";
      this.checkToolStripMenuItem.Click += new System.EventHandler(this.checkToolStripMenuItem_Click);
      // 
      // checkSelectedToolStripMenuItem
      // 
      this.checkSelectedToolStripMenuItem.Name = "checkSelectedToolStripMenuItem";
      this.checkSelectedToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
      this.checkSelectedToolStripMenuItem.Text = "Check Selected";
      this.checkSelectedToolStripMenuItem.Click += new System.EventHandler(this.checkSelectedToolStripMenuItem_Click);
      // 
      // checkAllToolStripMenuItem
      // 
      this.checkAllToolStripMenuItem.Enabled = false;
      this.checkAllToolStripMenuItem.Name = "checkAllToolStripMenuItem";
      this.checkAllToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
      this.checkAllToolStripMenuItem.Text = "Check All";
      // 
      // saveListOfSeriesToFileToolStripMenuItem
      // 
      this.saveListOfSeriesToFileToolStripMenuItem.Name = "saveListOfSeriesToFileToolStripMenuItem";
      this.saveListOfSeriesToFileToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
      this.saveListOfSeriesToFileToolStripMenuItem.Text = "Save list of series to file";
      this.saveListOfSeriesToFileToolStripMenuItem.Click += new System.EventHandler(this.saveListOfSeriesToFileToolStripMenuItem_Click);
      // 
      // lookUpNamesToolStripMenuItem
      // 
      this.lookUpNamesToolStripMenuItem.Name = "lookUpNamesToolStripMenuItem";
      this.lookUpNamesToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
      this.lookUpNamesToolStripMenuItem.Text = "Look up names";
      this.lookUpNamesToolStripMenuItem.Click += new System.EventHandler(this.lookUpNamesToolStripMenuItem_Click);
      // 
      // txtCurrent
      // 
      this.txtCurrent.Location = new System.Drawing.Point(723, 449);
      this.txtCurrent.Name = "txtCurrent";
      this.txtCurrent.Size = new System.Drawing.Size(242, 159);
      this.txtCurrent.TabIndex = 61;
      this.txtCurrent.Text = "";
      // 
      // txtAfter
      // 
      this.txtAfter.Location = new System.Drawing.Point(459, 449);
      this.txtAfter.Name = "txtAfter";
      this.txtAfter.Size = new System.Drawing.Size(242, 159);
      this.txtAfter.TabIndex = 61;
      this.txtAfter.Text = "";
      // 
      // txtBefore
      // 
      this.txtBefore.Location = new System.Drawing.Point(191, 449);
      this.txtBefore.Name = "txtBefore";
      this.txtBefore.Size = new System.Drawing.Size(242, 159);
      this.txtBefore.TabIndex = 61;
      this.txtBefore.Text = "";
      // 
      // Before
      // 
      this.Before.AutoSize = true;
      this.Before.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Before.Location = new System.Drawing.Point(273, 420);
      this.Before.Name = "Before";
      this.Before.Size = new System.Drawing.Size(56, 17);
      this.Before.TabIndex = 62;
      this.Before.Text = "Before";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label6.Location = new System.Drawing.Point(552, 420);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(43, 17);
      this.label6.TabIndex = 62;
      this.label6.Text = "After";
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label7.Location = new System.Drawing.Point(829, 420);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(62, 17);
      this.label7.TabIndex = 62;
      this.label7.Text = "Current";
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(11, 70);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(53, 13);
      this.label8.TabIndex = 63;
      this.label8.Text = "Timespan";
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(11, 26);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(83, 13);
      this.label9.TabIndex = 63;
      this.label9.Text = "Update Settings";
      // 
      // cbReloadOld
      // 
      this.cbReloadOld.AutoSize = true;
      this.cbReloadOld.Checked = true;
      this.cbReloadOld.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbReloadOld.Location = new System.Drawing.Point(34, 47);
      this.cbReloadOld.Name = "cbReloadOld";
      this.cbReloadOld.Size = new System.Drawing.Size(186, 17);
      this.cbReloadOld.TabIndex = 64;
      this.cbReloadOld.Text = "Re-download old content (default)";
      this.cbReloadOld.UseVisualStyleBackColor = true;
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.cmdUpdate);
      this.groupBox2.Controls.Add(this.cbReloadOld);
      this.groupBox2.Controls.Add(this.rbUpdateAutomatic);
      this.groupBox2.Controls.Add(this.label9);
      this.groupBox2.Controls.Add(this.rbUpdateDay);
      this.groupBox2.Controls.Add(this.label8);
      this.groupBox2.Controls.Add(this.rbUpdateWeek);
      this.groupBox2.Controls.Add(this.rbUpdateMonth);
      this.groupBox2.Location = new System.Drawing.Point(543, 616);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(225, 178);
      this.groupBox2.TabIndex = 65;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Manage Update";
      // 
      // groupBox4
      // 
      this.groupBox4.Controls.Add(this.cmdMakeSnapshotOfCache);
      this.groupBox4.Controls.Add(this.lbCacheSnapshots);
      this.groupBox4.Location = new System.Drawing.Point(10, 616);
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.Size = new System.Drawing.Size(200, 180);
      this.groupBox4.TabIndex = 66;
      this.groupBox4.TabStop = false;
      this.groupBox4.Text = "Cache Revisions";
      // 
      // cmdMakeSnapshotOfCache
      // 
      this.cmdMakeSnapshotOfCache.Location = new System.Drawing.Point(6, 148);
      this.cmdMakeSnapshotOfCache.Name = "cmdMakeSnapshotOfCache";
      this.cmdMakeSnapshotOfCache.Size = new System.Drawing.Size(188, 23);
      this.cmdMakeSnapshotOfCache.TabIndex = 3;
      this.cmdMakeSnapshotOfCache.Text = "Make Snapshot of current cache";
      this.cmdMakeSnapshotOfCache.UseVisualStyleBackColor = true;
      this.cmdMakeSnapshotOfCache.Click += new System.EventHandler(this.cmdMakeSnapshotOfCache_Click);
      // 
      // lbCacheSnapshots
      // 
      this.lbCacheSnapshots.ContextMenuStrip = this.cmCacheRevisions;
      this.lbCacheSnapshots.FormattingEnabled = true;
      this.lbCacheSnapshots.Location = new System.Drawing.Point(6, 21);
      this.lbCacheSnapshots.Name = "lbCacheSnapshots";
      this.lbCacheSnapshots.Size = new System.Drawing.Size(188, 121);
      this.lbCacheSnapshots.TabIndex = 0;
      this.lbCacheSnapshots.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbCacheSnapshots_MouseDoubleClick);
      // 
      // cmCacheRevisions
      // 
      this.cmCacheRevisions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteRevisionToolStripMenuItem,
            this.revertToThisRevisionToolStripMenuItem,
            this.compareBetweenTheseVersionsToolStripMenuItem});
      this.cmCacheRevisions.Name = "cmCacheRevisions";
      this.cmCacheRevisions.Size = new System.Drawing.Size(201, 70);
      this.cmCacheRevisions.Opening += new System.ComponentModel.CancelEventHandler(this.cmCacheRevisions_Opening);
      // 
      // deleteRevisionToolStripMenuItem
      // 
      this.deleteRevisionToolStripMenuItem.Name = "deleteRevisionToolStripMenuItem";
      this.deleteRevisionToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
      this.deleteRevisionToolStripMenuItem.Text = "Delete Revision";
      this.deleteRevisionToolStripMenuItem.Click += new System.EventHandler(this.deleteRevisionToolStripMenuItem_Click);
      // 
      // revertToThisRevisionToolStripMenuItem
      // 
      this.revertToThisRevisionToolStripMenuItem.Name = "revertToThisRevisionToolStripMenuItem";
      this.revertToThisRevisionToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
      this.revertToThisRevisionToolStripMenuItem.Text = "Revert to this Revision";
      this.revertToThisRevisionToolStripMenuItem.Click += new System.EventHandler(this.revertToThisRevisionToolStripMenuItem_Click);
      // 
      // compareBetweenTheseVersionsToolStripMenuItem
      // 
      this.compareBetweenTheseVersionsToolStripMenuItem.Name = "compareBetweenTheseVersionsToolStripMenuItem";
      this.compareBetweenTheseVersionsToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
      this.compareBetweenTheseVersionsToolStripMenuItem.Text = "Compare these versions";
      // 
      // downloadUpdatesToolStripMenuItem
      // 
      this.downloadUpdatesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.monthToolStripMenuItem,
            this.weekToolStripMenuItem,
            this.dayToolStripMenuItem});
      this.downloadUpdatesToolStripMenuItem.Name = "downloadUpdatesToolStripMenuItem";
      this.downloadUpdatesToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
      this.downloadUpdatesToolStripMenuItem.Text = "Download Updates xml";
      // 
      // monthToolStripMenuItem
      // 
      this.monthToolStripMenuItem.Name = "monthToolStripMenuItem";
      this.monthToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
      this.monthToolStripMenuItem.Text = "Month";
      this.monthToolStripMenuItem.Click += new System.EventHandler(this.monthToolStripMenuItem_Click);
      // 
      // weekToolStripMenuItem
      // 
      this.weekToolStripMenuItem.Name = "weekToolStripMenuItem";
      this.weekToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
      this.weekToolStripMenuItem.Text = "Week";
      this.weekToolStripMenuItem.Click += new System.EventHandler(this.weekToolStripMenuItem_Click);
      // 
      // dayToolStripMenuItem
      // 
      this.dayToolStripMenuItem.Name = "dayToolStripMenuItem";
      this.dayToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
      this.dayToolStripMenuItem.Text = "Day";
      this.dayToolStripMenuItem.Click += new System.EventHandler(this.dayToolStripMenuItem_Click);
      // 
      // UpdatingTestForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(977, 817);
      this.Controls.Add(this.groupBox4);
      this.Controls.Add(this.txtBefore);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.Before);
      this.Controls.Add(this.cbCacheType);
      this.Controls.Add(this.txtAfter);
      this.Controls.Add(this.txtCurrent);
      this.Controls.Add(this.lvCachedSeries);
      this.Controls.Add(this.cmdBack);
      this.Controls.Add(this.cbDownloadCurrentVersion);
      this.Controls.Add(this.txtLastUpdated);
      this.Controls.Add(this.txtCacheLocation);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.cmdInitTvdbHandler);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.lvSeriesDetails);
      this.Controls.Add(this.groupBox3);
      this.Controls.Add(this.groupBox1);
      this.Name = "UpdatingTestForm";
      this.Text = "UpdatingTestForm";
      this.cmDetailsView.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      this.cmChachedSeries.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.groupBox4.ResumeLayout(false);
      this.cmCacheRevisions.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ListView lvSeriesDetails;
    private System.Windows.Forms.ColumnHeader chPropertyName;
    private System.Windows.Forms.ColumnHeader chBeforeUpdate;
    private System.Windows.Forms.ColumnHeader chAfterUpdate;
    private System.Windows.Forms.ColumnHeader chCurrentValue;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Button cmdInitTvdbHandler;
    private System.Windows.Forms.TextBox txtCacheLocation;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ComboBox cbCacheType;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.Button cmdAddAllFavorites;
    private System.Windows.Forms.TextBox textBox4;
    private System.Windows.Forms.Button cmdAddSeriesById;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox txtLastUpdated;
    private System.Windows.Forms.Button cmdUpdate;
    private System.Windows.Forms.Button cmdShowUpdateLog;
    private System.Windows.Forms.Button cmdCompareValuesDetailed;
    private System.Windows.Forms.Button cmdComareValues;
    private System.Windows.Forms.CheckBox cbDownloadCurrentVersion;
    private System.Windows.Forms.RadioButton rbUpdateMonth;
    private System.Windows.Forms.RadioButton rbUpdateWeek;
    private System.Windows.Forms.RadioButton rbUpdateDay;
    private System.Windows.Forms.RadioButton rbUpdateAutomatic;
    private System.Windows.Forms.Button cmdBack;
    private System.Windows.Forms.ListView lvCachedSeries;
    private System.Windows.Forms.RichTextBox txtCurrent;
    private System.Windows.Forms.RichTextBox txtAfter;
    private System.Windows.Forms.RichTextBox txtBefore;
    private System.Windows.Forms.Label Before;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.CheckBox cbReloadOld;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.GroupBox groupBox4;
    private System.Windows.Forms.Button cmdMakeSnapshotOfCache;
    private System.Windows.Forms.ListBox lbCacheSnapshots;
    private System.Windows.Forms.ContextMenuStrip cmCacheRevisions;
    private System.Windows.Forms.ToolStripMenuItem deleteRevisionToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem revertToThisRevisionToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem compareBetweenTheseVersionsToolStripMenuItem;
    private System.Windows.Forms.ColumnHeader chSeriesiD;
    private System.Windows.Forms.ColumnHeader chSeriesName;
    private System.Windows.Forms.ContextMenuStrip cmChachedSeries;
    private System.Windows.Forms.ToolStripMenuItem checkToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem checkSelectedToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem checkAllToolStripMenuItem;
    private System.Windows.Forms.ContextMenuStrip cmDetailsView;
    private System.Windows.Forms.ToolStripMenuItem findEpisodeIDInToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem updatesmonthxmlToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem updatesweekxmlToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem updatesdayxmlToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem saveToFileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem saveListOfSeriesToFileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem lookUpNamesToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem downloadUpdatesToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem monthToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem weekToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem dayToolStripMenuItem;
  }
}