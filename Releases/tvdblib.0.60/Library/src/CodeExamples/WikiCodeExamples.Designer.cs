namespace WikiCodeExamples
{
  partial class WikiCodeExamples
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
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.lbLog = new System.Windows.Forms.ListBox();
      this.cmdExample1 = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.cbUseCaching = new System.Windows.Forms.CheckBox();
      this.cmdBrowseCacheDirectory = new System.Windows.Forms.Button();
      this.txtDirectory = new System.Windows.Forms.TextBox();
      this.txtSeriesId = new System.Windows.Forms.TextBox();
      this.lblSeriesId = new System.Windows.Forms.Label();
      this.tabPage3 = new System.Windows.Forms.TabPage();
      this.tabPage4 = new System.Windows.Forms.TabPage();
      this.cmdLoadCache = new System.Windows.Forms.Button();
      this.cmdSaveCache = new System.Windows.Forms.Button();
      this.cmdInit = new System.Windows.Forms.Button();
      this.cmdShowCachedSeries = new System.Windows.Forms.Button();
      this.lvCachedSeries = new System.Windows.Forms.ListView();
      this.tabPage5 = new System.Windows.Forms.TabPage();
      this.txtSearchText = new System.Windows.Forms.TextBox();
      this.cmdSearchSeries = new System.Windows.Forms.Button();
      this.lbSearchResult = new System.Windows.Forms.ListBox();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.tabPage5.SuspendLayout();
      this.SuspendLayout();
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage5);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Controls.Add(this.tabPage3);
      this.tabControl1.Controls.Add(this.tabPage4);
      this.tabControl1.Location = new System.Drawing.Point(1, 72);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(495, 305);
      this.tabControl1.TabIndex = 0;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.lblSeriesId);
      this.tabPage1.Controls.Add(this.txtSeriesId);
      this.tabPage1.Controls.Add(this.lbLog);
      this.tabPage1.Controls.Add(this.cmdExample1);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(487, 279);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Basics";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.lvCachedSeries);
      this.tabPage2.Controls.Add(this.cmdShowCachedSeries);
      this.tabPage2.Controls.Add(this.cmdSaveCache);
      this.tabPage2.Controls.Add(this.cmdLoadCache);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(487, 279);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Cache";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // lbLog
      // 
      this.lbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.lbLog.FormattingEnabled = true;
      this.lbLog.Location = new System.Drawing.Point(6, 32);
      this.lbLog.Name = "lbLog";
      this.lbLog.Size = new System.Drawing.Size(474, 238);
      this.lbLog.TabIndex = 3;
      // 
      // cmdExample1
      // 
      this.cmdExample1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdExample1.Location = new System.Drawing.Point(165, 7);
      this.cmdExample1.Name = "cmdExample1";
      this.cmdExample1.Size = new System.Drawing.Size(315, 23);
      this.cmdExample1.TabIndex = 2;
      this.cmdExample1.Text = "Code example 1: Retrieve and show series details";
      this.cmdExample1.UseVisualStyleBackColor = true;
      this.cmdExample1.Click += new System.EventHandler(this.cmdExample1_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(127, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(200, 20);
      this.label1.TabIndex = 1;
      this.label1.Text = "TvdbLib Code Examples";
      // 
      // cbUseCaching
      // 
      this.cbUseCaching.AutoSize = true;
      this.cbUseCaching.Location = new System.Drawing.Point(311, 49);
      this.cbUseCaching.Name = "cbUseCaching";
      this.cbUseCaching.Size = new System.Drawing.Size(118, 17);
      this.cbUseCaching.TabIndex = 2;
      this.cbUseCaching.Text = "Use Caching (XML)";
      this.cbUseCaching.UseVisualStyleBackColor = true;
      this.cbUseCaching.CheckedChanged += new System.EventHandler(this.cbUseCaching_CheckedChanged);
      // 
      // cmdBrowseCacheDirectory
      // 
      this.cmdBrowseCacheDirectory.Enabled = false;
      this.cmdBrowseCacheDirectory.Location = new System.Drawing.Point(229, 45);
      this.cmdBrowseCacheDirectory.Name = "cmdBrowseCacheDirectory";
      this.cmdBrowseCacheDirectory.Size = new System.Drawing.Size(75, 23);
      this.cmdBrowseCacheDirectory.TabIndex = 3;
      this.cmdBrowseCacheDirectory.Text = "Browse";
      this.cmdBrowseCacheDirectory.UseVisualStyleBackColor = true;
      // 
      // txtDirectory
      // 
      this.txtDirectory.Enabled = false;
      this.txtDirectory.Location = new System.Drawing.Point(5, 46);
      this.txtDirectory.Name = "txtDirectory";
      this.txtDirectory.Size = new System.Drawing.Size(218, 20);
      this.txtDirectory.TabIndex = 4;
      // 
      // txtSeriesId
      // 
      this.txtSeriesId.Location = new System.Drawing.Point(60, 9);
      this.txtSeriesId.Name = "txtSeriesId";
      this.txtSeriesId.Size = new System.Drawing.Size(100, 20);
      this.txtSeriesId.TabIndex = 4;
      this.txtSeriesId.Text = "73255";
      // 
      // lblSeriesId
      // 
      this.lblSeriesId.AutoSize = true;
      this.lblSeriesId.Location = new System.Drawing.Point(7, 13);
      this.lblSeriesId.Name = "lblSeriesId";
      this.lblSeriesId.Size = new System.Drawing.Size(51, 13);
      this.lblSeriesId.TabIndex = 5;
      this.lblSeriesId.Text = "Series Id:";
      // 
      // tabPage3
      // 
      this.tabPage3.Location = new System.Drawing.Point(4, 22);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage3.Size = new System.Drawing.Size(487, 279);
      this.tabPage3.TabIndex = 2;
      this.tabPage3.Text = "Languages";
      this.tabPage3.UseVisualStyleBackColor = true;
      // 
      // tabPage4
      // 
      this.tabPage4.Location = new System.Drawing.Point(4, 22);
      this.tabPage4.Name = "tabPage4";
      this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage4.Size = new System.Drawing.Size(487, 279);
      this.tabPage4.TabIndex = 3;
      this.tabPage4.Text = "Updating";
      this.tabPage4.UseVisualStyleBackColor = true;
      // 
      // cmdLoadCache
      // 
      this.cmdLoadCache.Location = new System.Drawing.Point(7, 19);
      this.cmdLoadCache.Name = "cmdLoadCache";
      this.cmdLoadCache.Size = new System.Drawing.Size(211, 23);
      this.cmdLoadCache.TabIndex = 0;
      this.cmdLoadCache.Text = "Load Cache";
      this.cmdLoadCache.UseVisualStyleBackColor = true;
      this.cmdLoadCache.Click += new System.EventHandler(this.cmdLoadCache_Click);
      // 
      // cmdSaveCache
      // 
      this.cmdSaveCache.Location = new System.Drawing.Point(265, 19);
      this.cmdSaveCache.Name = "cmdSaveCache";
      this.cmdSaveCache.Size = new System.Drawing.Size(211, 23);
      this.cmdSaveCache.TabIndex = 0;
      this.cmdSaveCache.Text = "Save Cache";
      this.cmdSaveCache.UseVisualStyleBackColor = true;
      this.cmdSaveCache.Click += new System.EventHandler(this.cmdSaveCache_Click);
      // 
      // cmdInit
      // 
      this.cmdInit.Location = new System.Drawing.Point(434, 45);
      this.cmdInit.Name = "cmdInit";
      this.cmdInit.Size = new System.Drawing.Size(60, 23);
      this.cmdInit.TabIndex = 5;
      this.cmdInit.Text = "Init";
      this.cmdInit.UseVisualStyleBackColor = true;
      this.cmdInit.Click += new System.EventHandler(this.cmdInit_Click);
      // 
      // cmdShowCachedSeries
      // 
      this.cmdShowCachedSeries.Location = new System.Drawing.Point(7, 55);
      this.cmdShowCachedSeries.Name = "cmdShowCachedSeries";
      this.cmdShowCachedSeries.Size = new System.Drawing.Size(469, 23);
      this.cmdShowCachedSeries.TabIndex = 2;
      this.cmdShowCachedSeries.Text = "Show cached Series";
      this.cmdShowCachedSeries.UseVisualStyleBackColor = true;
      this.cmdShowCachedSeries.Click += new System.EventHandler(this.cmdShowCachedSeries_Click);
      // 
      // lvCachedSeries
      // 
      this.lvCachedSeries.Location = new System.Drawing.Point(7, 84);
      this.lvCachedSeries.Name = "lvCachedSeries";
      this.lvCachedSeries.Size = new System.Drawing.Size(469, 189);
      this.lvCachedSeries.TabIndex = 3;
      this.lvCachedSeries.UseCompatibleStateImageBehavior = false;
      // 
      // tabPage5
      // 
      this.tabPage5.Controls.Add(this.lbSearchResult);
      this.tabPage5.Controls.Add(this.cmdSearchSeries);
      this.tabPage5.Controls.Add(this.txtSearchText);
      this.tabPage5.Location = new System.Drawing.Point(4, 22);
      this.tabPage5.Name = "tabPage5";
      this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage5.Size = new System.Drawing.Size(487, 279);
      this.tabPage5.TabIndex = 4;
      this.tabPage5.Text = "Searching";
      this.tabPage5.UseVisualStyleBackColor = true;
      // 
      // txtSearchText
      // 
      this.txtSearchText.Location = new System.Drawing.Point(19, 15);
      this.txtSearchText.Name = "txtSearchText";
      this.txtSearchText.Size = new System.Drawing.Size(364, 20);
      this.txtSearchText.TabIndex = 0;
      // 
      // cmdSearchSeries
      // 
      this.cmdSearchSeries.Location = new System.Drawing.Point(389, 11);
      this.cmdSearchSeries.Name = "cmdSearchSeries";
      this.cmdSearchSeries.Size = new System.Drawing.Size(75, 23);
      this.cmdSearchSeries.TabIndex = 1;
      this.cmdSearchSeries.Text = "Search";
      this.cmdSearchSeries.UseVisualStyleBackColor = true;
      this.cmdSearchSeries.Click += new System.EventHandler(this.cmdSearchSeries_Click);
      // 
      // lbSearchResult
      // 
      this.lbSearchResult.FormattingEnabled = true;
      this.lbSearchResult.Location = new System.Drawing.Point(19, 41);
      this.lbSearchResult.Name = "lbSearchResult";
      this.lbSearchResult.Size = new System.Drawing.Size(445, 225);
      this.lbSearchResult.TabIndex = 2;
      // 
      // WikiCodeExamples
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(497, 379);
      this.Controls.Add(this.cmdInit);
      this.Controls.Add(this.txtDirectory);
      this.Controls.Add(this.cmdBrowseCacheDirectory);
      this.Controls.Add(this.cbUseCaching);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.tabControl1);
      this.Name = "WikiCodeExamples";
      this.Text = "WikiCodeExamples";
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage1.PerformLayout();
      this.tabPage2.ResumeLayout(false);
      this.tabPage5.ResumeLayout(false);
      this.tabPage5.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.ListBox lbLog;
    private System.Windows.Forms.Button cmdExample1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.CheckBox cbUseCaching;
    private System.Windows.Forms.Button cmdBrowseCacheDirectory;
    private System.Windows.Forms.TextBox txtDirectory;
    private System.Windows.Forms.Label lblSeriesId;
    private System.Windows.Forms.TextBox txtSeriesId;
    private System.Windows.Forms.TabPage tabPage3;
    private System.Windows.Forms.TabPage tabPage4;
    private System.Windows.Forms.Button cmdSaveCache;
    private System.Windows.Forms.Button cmdLoadCache;
    private System.Windows.Forms.Button cmdInit;
    private System.Windows.Forms.Button cmdShowCachedSeries;
    private System.Windows.Forms.ListView lvCachedSeries;
    private System.Windows.Forms.TabPage tabPage5;
    private System.Windows.Forms.Button cmdSearchSeries;
    private System.Windows.Forms.TextBox txtSearchText;
    private System.Windows.Forms.ListBox lbSearchResult;

  }
}