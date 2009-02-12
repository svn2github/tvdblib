namespace WikiCodeExamples
{
  partial class MemoryTest
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
      this.lvFavorites = new System.Windows.Forms.ListView();
      this.cmSeriesListView = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.unloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.loadFanart0ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.cmdInit = new System.Windows.Forms.Button();
      this.cmdClose = new System.Windows.Forms.Button();
      this.txtUserId = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.unloadFanart0ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.loadPoster0ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.unloadPoster0ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.loadSeries0ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.unloadSeries0ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.cmSeriesListView.SuspendLayout();
      this.SuspendLayout();
      // 
      // lvFavorites
      // 
      this.lvFavorites.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.lvFavorites.ContextMenuStrip = this.cmSeriesListView;
      this.lvFavorites.Location = new System.Drawing.Point(12, 38);
      this.lvFavorites.Name = "lvFavorites";
      this.lvFavorites.Size = new System.Drawing.Size(603, 306);
      this.lvFavorites.TabIndex = 0;
      this.lvFavorites.UseCompatibleStateImageBehavior = false;
      this.lvFavorites.View = System.Windows.Forms.View.List;
      // 
      // cmSeriesListView
      // 
      this.cmSeriesListView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.unloadToolStripMenuItem,
            this.loadFanart0ToolStripMenuItem,
            this.unloadFanart0ToolStripMenuItem,
            this.loadPoster0ToolStripMenuItem,
            this.unloadPoster0ToolStripMenuItem,
            this.loadSeries0ToolStripMenuItem,
            this.unloadSeries0ToolStripMenuItem});
      this.cmSeriesListView.Name = "cmSeriesListView";
      this.cmSeriesListView.Size = new System.Drawing.Size(171, 202);
      this.cmSeriesListView.Opening += new System.ComponentModel.CancelEventHandler(this.cmSeriesListView_Opening);
      // 
      // loadToolStripMenuItem
      // 
      this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
      this.loadToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
      this.loadToolStripMenuItem.Text = "Load";
      this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
      // 
      // unloadToolStripMenuItem
      // 
      this.unloadToolStripMenuItem.Name = "unloadToolStripMenuItem";
      this.unloadToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
      this.unloadToolStripMenuItem.Text = "Unload";
      this.unloadToolStripMenuItem.Click += new System.EventHandler(this.unloadToolStripMenuItem_Click);
      // 
      // loadFanart0ToolStripMenuItem
      // 
      this.loadFanart0ToolStripMenuItem.Name = "loadFanart0ToolStripMenuItem";
      this.loadFanart0ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
      this.loadFanart0ToolStripMenuItem.Text = "Load Fanart (0)";
      this.loadFanart0ToolStripMenuItem.Click += new System.EventHandler(this.loadFanart0ToolStripMenuItem_Click);
      // 
      // cmdInit
      // 
      this.cmdInit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdInit.Location = new System.Drawing.Point(12, 350);
      this.cmdInit.Name = "cmdInit";
      this.cmdInit.Size = new System.Drawing.Size(429, 23);
      this.cmdInit.TabIndex = 1;
      this.cmdInit.Text = "Init";
      this.cmdInit.UseVisualStyleBackColor = true;
      this.cmdInit.Click += new System.EventHandler(this.cmdInit_Click);
      // 
      // cmdClose
      // 
      this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdClose.Location = new System.Drawing.Point(12, 403);
      this.cmdClose.Name = "cmdClose";
      this.cmdClose.Size = new System.Drawing.Size(603, 23);
      this.cmdClose.TabIndex = 1;
      this.cmdClose.Text = "Close";
      this.cmdClose.UseVisualStyleBackColor = true;
      // 
      // txtUserId
      // 
      this.txtUserId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.txtUserId.Location = new System.Drawing.Point(447, 352);
      this.txtUserId.Name = "txtUserId";
      this.txtUserId.Size = new System.Drawing.Size(168, 20);
      this.txtUserId.TabIndex = 2;
      this.txtUserId.Text = "2EF20123489774E9";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(228, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(150, 24);
      this.label1.TabIndex = 4;
      this.label1.Text = "Memory Tester";
      // 
      // unloadFanart0ToolStripMenuItem
      // 
      this.unloadFanart0ToolStripMenuItem.Name = "unloadFanart0ToolStripMenuItem";
      this.unloadFanart0ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
      this.unloadFanart0ToolStripMenuItem.Text = "Unload Fanart (0)";
      this.unloadFanart0ToolStripMenuItem.Click += new System.EventHandler(this.unloadFanart0ToolStripMenuItem_Click);
      // 
      // loadPoster0ToolStripMenuItem
      // 
      this.loadPoster0ToolStripMenuItem.Enabled = false;
      this.loadPoster0ToolStripMenuItem.Name = "loadPoster0ToolStripMenuItem";
      this.loadPoster0ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
      this.loadPoster0ToolStripMenuItem.Text = "Load Poster (0)";
      // 
      // unloadPoster0ToolStripMenuItem
      // 
      this.unloadPoster0ToolStripMenuItem.Enabled = false;
      this.unloadPoster0ToolStripMenuItem.Name = "unloadPoster0ToolStripMenuItem";
      this.unloadPoster0ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
      this.unloadPoster0ToolStripMenuItem.Text = "Unload Poster (0)";
      // 
      // loadSeries0ToolStripMenuItem
      // 
      this.loadSeries0ToolStripMenuItem.Enabled = false;
      this.loadSeries0ToolStripMenuItem.Name = "loadSeries0ToolStripMenuItem";
      this.loadSeries0ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
      this.loadSeries0ToolStripMenuItem.Text = "Load Series (0)";
      // 
      // unloadSeries0ToolStripMenuItem
      // 
      this.unloadSeries0ToolStripMenuItem.Enabled = false;
      this.unloadSeries0ToolStripMenuItem.Name = "unloadSeries0ToolStripMenuItem";
      this.unloadSeries0ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
      this.unloadSeries0ToolStripMenuItem.Text = "Unload Series (0)";
      // 
      // MemoryTest
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(627, 438);
      this.Controls.Add(this.txtUserId);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.cmdClose);
      this.Controls.Add(this.cmdInit);
      this.Controls.Add(this.lvFavorites);
      this.Name = "MemoryTest";
      this.Text = "MemoryTest";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MemoryTest_FormClosed);
      this.cmSeriesListView.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ListView lvFavorites;
    private System.Windows.Forms.Button cmdInit;
    private System.Windows.Forms.Button cmdClose;
    private System.Windows.Forms.TextBox txtUserId;
    private System.Windows.Forms.ContextMenuStrip cmSeriesListView;
    private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem unloadToolStripMenuItem;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ToolStripMenuItem loadFanart0ToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem unloadFanart0ToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem loadPoster0ToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem unloadPoster0ToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem loadSeries0ToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem unloadSeries0ToolStripMenuItem;
  }
}