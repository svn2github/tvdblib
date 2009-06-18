namespace WikiCodeExamples
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
      this.lvSeriesDetails = new System.Windows.Forms.ListView();
      this.chPropertyName = new System.Windows.Forms.ColumnHeader();
      this.chBeforeUpdate = new System.Windows.Forms.ColumnHeader();
      this.chAfterUpdate = new System.Windows.Forms.ColumnHeader();
      this.chCurrentValue = new System.Windows.Forms.ColumnHeader();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.cmdInitTvdbHandler = new System.Windows.Forms.Button();
      this.txtApiKey = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.txtUserId = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
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
      this.cmdShowUpdateLog = new System.Windows.Forms.Button();
      this.cmdComareValues = new System.Windows.Forms.Button();
      this.cmdCompareValuesDetailed = new System.Windows.Forms.Button();
      this.checkBox1 = new System.Windows.Forms.CheckBox();
      this.rbUpdateMonth = new System.Windows.Forms.RadioButton();
      this.rbUpdateWeek = new System.Windows.Forms.RadioButton();
      this.rbUpdateDay = new System.Windows.Forms.RadioButton();
      this.rbUpdateAutomatic = new System.Windows.Forms.RadioButton();
      this.cmdBack = new System.Windows.Forms.Button();
      this.lvCachedSeries = new System.Windows.Forms.ListView();
      this.txtCurrent = new System.Windows.Forms.RichTextBox();
      this.txtAfter = new System.Windows.Forms.RichTextBox();
      this.txtBefore = new System.Windows.Forms.RichTextBox();
      this.Before = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.groupBox1.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.SuspendLayout();
      // 
      // lvSeriesDetails
      // 
      this.lvSeriesDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chPropertyName,
            this.chBeforeUpdate,
            this.chAfterUpdate,
            this.chCurrentValue});
      this.lvSeriesDetails.FullRowSelect = true;
      this.lvSeriesDetails.Location = new System.Drawing.Point(187, 117);
      this.lvSeriesDetails.MultiSelect = false;
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
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.cmdCompareValuesDetailed);
      this.groupBox1.Controls.Add(this.cmdComareValues);
      this.groupBox1.Controls.Add(this.cmdShowUpdateLog);
      this.groupBox1.Location = new System.Drawing.Point(589, 685);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(379, 180);
      this.groupBox1.TabIndex = 2;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Analyze Results";
      // 
      // cmdInitTvdbHandler
      // 
      this.cmdInitTvdbHandler.Location = new System.Drawing.Point(12, 48);
      this.cmdInitTvdbHandler.Name = "cmdInitTvdbHandler";
      this.cmdInitTvdbHandler.Size = new System.Drawing.Size(883, 23);
      this.cmdInitTvdbHandler.TabIndex = 3;
      this.cmdInitTvdbHandler.Text = "Initialise";
      this.cmdInitTvdbHandler.UseVisualStyleBackColor = true;
      this.cmdInitTvdbHandler.Click += new System.EventHandler(this.cmdInitTvdbHandler_Click);
      // 
      // txtApiKey
      // 
      this.txtApiKey.Location = new System.Drawing.Point(83, 22);
      this.txtApiKey.Name = "txtApiKey";
      this.txtApiKey.Size = new System.Drawing.Size(100, 20);
      this.txtApiKey.TabIndex = 4;
      this.txtApiKey.Text = "49E28C3EB13EB1CF";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(22, 25);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(45, 13);
      this.label1.TabIndex = 5;
      this.label1.Text = "API Key";
      // 
      // txtUserId
      // 
      this.txtUserId.Location = new System.Drawing.Point(318, 22);
      this.txtUserId.Name = "txtUserId";
      this.txtUserId.Size = new System.Drawing.Size(100, 20);
      this.txtUserId.TabIndex = 6;
      this.txtUserId.Text = "2EF20123489774E9";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(213, 25);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(41, 13);
      this.label2.TabIndex = 7;
      this.label2.Text = "User Id";
      // 
      // txtCacheLocation
      // 
      this.txtCacheLocation.Location = new System.Drawing.Point(555, 22);
      this.txtCacheLocation.Name = "txtCacheLocation";
      this.txtCacheLocation.Size = new System.Drawing.Size(100, 20);
      this.txtCacheLocation.TabIndex = 8;
      this.txtCacheLocation.Text = "Cache";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(437, 25);
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
      this.cbCacheType.Location = new System.Drawing.Point(774, 21);
      this.cbCacheType.Name = "cbCacheType";
      this.cbCacheType.Size = new System.Drawing.Size(121, 21);
      this.cbCacheType.TabIndex = 9;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(686, 25);
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
      this.groupBox3.Location = new System.Drawing.Point(15, 685);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(349, 180);
      this.groupBox3.TabIndex = 2;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Manage Cached Series";
      // 
      // cmdAddAllFavorites
      // 
      this.cmdAddAllFavorites.Location = new System.Drawing.Point(13, 47);
      this.cmdAddAllFavorites.Name = "cmdAddAllFavorites";
      this.cmdAddAllFavorites.Size = new System.Drawing.Size(276, 23);
      this.cmdAddAllFavorites.TabIndex = 2;
      this.cmdAddAllFavorites.Text = "Use Favorites";
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
      this.label5.Location = new System.Drawing.Point(12, 84);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(74, 13);
      this.label5.TabIndex = 10;
      this.label5.Text = "Last Updated:";
      // 
      // txtLastUpdated
      // 
      this.txtLastUpdated.Location = new System.Drawing.Point(92, 81);
      this.txtLastUpdated.Name = "txtLastUpdated";
      this.txtLastUpdated.ReadOnly = true;
      this.txtLastUpdated.Size = new System.Drawing.Size(224, 20);
      this.txtLastUpdated.TabIndex = 11;
      // 
      // cmdUpdate
      // 
      this.cmdUpdate.Location = new System.Drawing.Point(397, 739);
      this.cmdUpdate.Name = "cmdUpdate";
      this.cmdUpdate.Size = new System.Drawing.Size(158, 126);
      this.cmdUpdate.TabIndex = 12;
      this.cmdUpdate.Text = "Update";
      this.cmdUpdate.UseVisualStyleBackColor = true;
      this.cmdUpdate.Click += new System.EventHandler(this.cmdUpdate_Click);
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
      // checkBox1
      // 
      this.checkBox1.AutoSize = true;
      this.checkBox1.Location = new System.Drawing.Point(12, 120);
      this.checkBox1.Name = "checkBox1";
      this.checkBox1.Size = new System.Drawing.Size(169, 17);
      this.checkBox1.TabIndex = 13;
      this.checkBox1.Text = "Download current version also";
      this.checkBox1.UseVisualStyleBackColor = true;
      // 
      // rbUpdateMonth
      // 
      this.rbUpdateMonth.AutoSize = true;
      this.rbUpdateMonth.Location = new System.Drawing.Point(492, 716);
      this.rbUpdateMonth.Name = "rbUpdateMonth";
      this.rbUpdateMonth.Size = new System.Drawing.Size(55, 17);
      this.rbUpdateMonth.TabIndex = 57;
      this.rbUpdateMonth.Text = "Month";
      this.rbUpdateMonth.UseVisualStyleBackColor = true;
      // 
      // rbUpdateWeek
      // 
      this.rbUpdateWeek.AutoSize = true;
      this.rbUpdateWeek.Location = new System.Drawing.Point(492, 693);
      this.rbUpdateWeek.Name = "rbUpdateWeek";
      this.rbUpdateWeek.Size = new System.Drawing.Size(54, 17);
      this.rbUpdateWeek.TabIndex = 58;
      this.rbUpdateWeek.Text = "Week";
      this.rbUpdateWeek.UseVisualStyleBackColor = true;
      // 
      // rbUpdateDay
      // 
      this.rbUpdateDay.AutoSize = true;
      this.rbUpdateDay.Location = new System.Drawing.Point(408, 716);
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
      this.rbUpdateAutomatic.Location = new System.Drawing.Point(408, 694);
      this.rbUpdateAutomatic.Name = "rbUpdateAutomatic";
      this.rbUpdateAutomatic.Size = new System.Drawing.Size(72, 17);
      this.rbUpdateAutomatic.TabIndex = 56;
      this.rbUpdateAutomatic.TabStop = true;
      this.rbUpdateAutomatic.Text = "Automatic";
      this.rbUpdateAutomatic.UseVisualStyleBackColor = true;
      // 
      // cmdBack
      // 
      this.cmdBack.Location = new System.Drawing.Point(187, 117);
      this.cmdBack.Name = "cmdBack";
      this.cmdBack.Size = new System.Drawing.Size(114, 23);
      this.cmdBack.TabIndex = 59;
      this.cmdBack.Text = "Back";
      this.cmdBack.UseVisualStyleBackColor = true;
      this.cmdBack.Click += new System.EventHandler(this.cmdBack_Click);
      // 
      // lvCachedSeries
      // 
      this.lvCachedSeries.Location = new System.Drawing.Point(12, 143);
      this.lvCachedSeries.Name = "lvCachedSeries";
      this.lvCachedSeries.Size = new System.Drawing.Size(169, 329);
      this.lvCachedSeries.TabIndex = 60;
      this.lvCachedSeries.UseCompatibleStateImageBehavior = false;
      this.lvCachedSeries.View = System.Windows.Forms.View.List;
      this.lvCachedSeries.SelectedIndexChanged += new System.EventHandler(this.lvCachedSeries_SelectedIndexChanged);
      // 
      // txtCurrent
      // 
      this.txtCurrent.Location = new System.Drawing.Point(723, 520);
      this.txtCurrent.Name = "txtCurrent";
      this.txtCurrent.Size = new System.Drawing.Size(242, 159);
      this.txtCurrent.TabIndex = 61;
      this.txtCurrent.Text = "";
      // 
      // txtAfter
      // 
      this.txtAfter.Location = new System.Drawing.Point(459, 520);
      this.txtAfter.Name = "txtAfter";
      this.txtAfter.Size = new System.Drawing.Size(242, 159);
      this.txtAfter.TabIndex = 61;
      this.txtAfter.Text = "";
      // 
      // txtBefore
      // 
      this.txtBefore.Location = new System.Drawing.Point(187, 520);
      this.txtBefore.Name = "txtBefore";
      this.txtBefore.Size = new System.Drawing.Size(242, 159);
      this.txtBefore.TabIndex = 61;
      this.txtBefore.Text = "";
      // 
      // Before
      // 
      this.Before.AutoSize = true;
      this.Before.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Before.Location = new System.Drawing.Point(273, 491);
      this.Before.Name = "Before";
      this.Before.Size = new System.Drawing.Size(56, 17);
      this.Before.TabIndex = 62;
      this.Before.Text = "Before";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label6.Location = new System.Drawing.Point(552, 491);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(43, 17);
      this.label6.TabIndex = 62;
      this.label6.Text = "After";
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label7.Location = new System.Drawing.Point(829, 491);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(62, 17);
      this.label7.TabIndex = 62;
      this.label7.Text = "Current";
      // 
      // UpdatingTestForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(977, 877);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.Before);
      this.Controls.Add(this.txtBefore);
      this.Controls.Add(this.txtAfter);
      this.Controls.Add(this.txtCurrent);
      this.Controls.Add(this.lvCachedSeries);
      this.Controls.Add(this.cmdBack);
      this.Controls.Add(this.rbUpdateMonth);
      this.Controls.Add(this.rbUpdateWeek);
      this.Controls.Add(this.rbUpdateDay);
      this.Controls.Add(this.rbUpdateAutomatic);
      this.Controls.Add(this.checkBox1);
      this.Controls.Add(this.cmdUpdate);
      this.Controls.Add(this.txtLastUpdated);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.cbCacheType);
      this.Controls.Add(this.txtCacheLocation);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.txtUserId);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txtApiKey);
      this.Controls.Add(this.cmdInitTvdbHandler);
      this.Controls.Add(this.groupBox3);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.lvSeriesDetails);
      this.Name = "UpdatingTestForm";
      this.Text = "UpdatingTestForm";
      this.groupBox1.ResumeLayout(false);
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
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
    private System.Windows.Forms.TextBox txtApiKey;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtUserId;
    private System.Windows.Forms.Label label2;
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
    private System.Windows.Forms.CheckBox checkBox1;
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
  }
}