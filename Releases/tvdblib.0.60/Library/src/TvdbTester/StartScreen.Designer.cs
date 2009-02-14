namespace TvdbTester
{
  partial class StartScreen
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartScreen));
      this.cmdStart = new System.Windows.Forms.Button();
      this.txtUserIdentifier = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.richTextBox1 = new System.Windows.Forms.RichTextBox();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.tsStatus = new System.Windows.Forms.ToolStripStatusLabel();
      this.cmdUseWithoutId = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      this.richTextBox2 = new System.Windows.Forms.RichTextBox();
      this.cbCacheProvider = new System.Windows.Forms.ComboBox();
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.folderBrowseCache = new System.Windows.Forms.FolderBrowserDialog();
      this.txtRootFolder = new System.Windows.Forms.TextBox();
      this.cmdBrowseCacheFolder = new System.Windows.Forms.Button();
      this.cbSaveLogin = new System.Windows.Forms.CheckBox();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.statusStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // cmdStart
      // 
      this.cmdStart.Location = new System.Drawing.Point(9, 509);
      this.cmdStart.Name = "cmdStart";
      this.cmdStart.Size = new System.Drawing.Size(187, 23);
      this.cmdStart.TabIndex = 0;
      this.cmdStart.Text = "Start";
      this.cmdStart.UseVisualStyleBackColor = true;
      this.cmdStart.Click += new System.EventHandler(this.cmdStart_Click);
      // 
      // txtUserIdentifier
      // 
      this.txtUserIdentifier.Location = new System.Drawing.Point(94, 483);
      this.txtUserIdentifier.Name = "txtUserIdentifier";
      this.txtUserIdentifier.Size = new System.Drawing.Size(187, 20);
      this.txtUserIdentifier.TabIndex = 1;
      this.txtUserIdentifier.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(13, 486);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(75, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "User Identifier:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(9, 342);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(116, 17);
      this.label2.TabIndex = 3;
      this.label2.Text = "User Identifier:";
      // 
      // pictureBox1
      // 
      this.pictureBox1.Image = global::TvdbTester.Properties.Resources.tvdb_logo;
      this.pictureBox1.Location = new System.Drawing.Point(0, 0);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(385, 121);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pictureBox1.TabIndex = 4;
      this.pictureBox1.TabStop = false;
      // 
      // richTextBox1
      // 
      this.richTextBox1.Location = new System.Drawing.Point(12, 371);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.ReadOnly = true;
      this.richTextBox1.Size = new System.Drawing.Size(356, 94);
      this.richTextBox1.TabIndex = 5;
      this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
      this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
      // 
      // statusStrip1
      // 
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsStatus});
      this.statusStrip1.Location = new System.Drawing.Point(0, 542);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(379, 22);
      this.statusStrip1.TabIndex = 6;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // tsStatus
      // 
      this.tsStatus.Name = "tsStatus";
      this.tsStatus.Size = new System.Drawing.Size(0, 17);
      // 
      // cmdUseWithoutId
      // 
      this.cmdUseWithoutId.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cmdUseWithoutId.Location = new System.Drawing.Point(202, 509);
      this.cmdUseWithoutId.Name = "cmdUseWithoutId";
      this.cmdUseWithoutId.Size = new System.Drawing.Size(166, 23);
      this.cmdUseWithoutId.TabIndex = 7;
      this.cmdUseWithoutId.Text = "Use Without Id";
      this.cmdUseWithoutId.UseVisualStyleBackColor = true;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(8, 124);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(71, 17);
      this.label3.TabIndex = 3;
      this.label3.Text = "Caching:";
      // 
      // richTextBox2
      // 
      this.richTextBox2.Location = new System.Drawing.Point(12, 147);
      this.richTextBox2.Name = "richTextBox2";
      this.richTextBox2.ReadOnly = true;
      this.richTextBox2.Size = new System.Drawing.Size(356, 103);
      this.richTextBox2.TabIndex = 5;
      this.richTextBox2.Text = resources.GetString("richTextBox2.Text");
      this.richTextBox2.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
      // 
      // cbCacheProvider
      // 
      this.cbCacheProvider.FormattingEnabled = true;
      this.cbCacheProvider.Location = new System.Drawing.Point(153, 265);
      this.cbCacheProvider.Name = "cbCacheProvider";
      this.cbCacheProvider.Size = new System.Drawing.Size(214, 21);
      this.cbCacheProvider.TabIndex = 8;
      this.cbCacheProvider.Text = " Save to XML";
      this.cbCacheProvider.SelectedIndexChanged += new System.EventHandler(this.cbCacheProvider_SelectedIndexChanged);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(12, 268);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(88, 13);
      this.label4.TabIndex = 9;
      this.label4.Text = "Caching Method:";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(12, 300);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(51, 13);
      this.label5.TabIndex = 9;
      this.label5.Text = "Location:";
      // 
      // folderBrowseCache
      // 
      this.folderBrowseCache.Description = "Browse for the root location of the cached files";
      this.folderBrowseCache.RootFolder = System.Environment.SpecialFolder.MyComputer;
      // 
      // txtRootFolder
      // 
      this.txtRootFolder.Location = new System.Drawing.Point(153, 300);
      this.txtRootFolder.Name = "txtRootFolder";
      this.txtRootFolder.Size = new System.Drawing.Size(144, 20);
      this.txtRootFolder.TabIndex = 10;
      // 
      // cmdBrowseCacheFolder
      // 
      this.cmdBrowseCacheFolder.Location = new System.Drawing.Point(303, 298);
      this.cmdBrowseCacheFolder.Name = "cmdBrowseCacheFolder";
      this.cmdBrowseCacheFolder.Size = new System.Drawing.Size(64, 23);
      this.cmdBrowseCacheFolder.TabIndex = 11;
      this.cmdBrowseCacheFolder.Text = "Browse";
      this.cmdBrowseCacheFolder.UseVisualStyleBackColor = true;
      this.cmdBrowseCacheFolder.Click += new System.EventHandler(this.cmdBrowseCacheFolder_Click);
      // 
      // cbSaveLogin
      // 
      this.cbSaveLogin.AutoSize = true;
      this.cbSaveLogin.Checked = true;
      this.cbSaveLogin.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbSaveLogin.Location = new System.Drawing.Point(288, 485);
      this.cbSaveLogin.Name = "cbSaveLogin";
      this.cbSaveLogin.Size = new System.Drawing.Size(80, 17);
      this.cbSaveLogin.TabIndex = 12;
      this.cbSaveLogin.Text = "Save Login";
      this.cbSaveLogin.UseVisualStyleBackColor = true;
      // 
      // StartScreen
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cmdUseWithoutId;
      this.ClientSize = new System.Drawing.Size(379, 564);
      this.Controls.Add(this.cbSaveLogin);
      this.Controls.Add(this.cmdBrowseCacheFolder);
      this.Controls.Add(this.txtRootFolder);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.cbCacheProvider);
      this.Controls.Add(this.cmdUseWithoutId);
      this.Controls.Add(this.statusStrip1);
      this.Controls.Add(this.richTextBox2);
      this.Controls.Add(this.richTextBox1);
      this.Controls.Add(this.pictureBox1);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txtUserIdentifier);
      this.Controls.Add(this.cmdStart);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "StartScreen";
      this.ShowInTaskbar = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.Text = "StartScreen";
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button cmdStart;
    private System.Windows.Forms.TextBox txtUserIdentifier;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.RichTextBox richTextBox1;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripStatusLabel tsStatus;
    private System.Windows.Forms.Button cmdUseWithoutId;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.RichTextBox richTextBox2;
    private System.Windows.Forms.ComboBox cbCacheProvider;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.FolderBrowserDialog folderBrowseCache;
    private System.Windows.Forms.TextBox txtRootFolder;
    private System.Windows.Forms.Button cmdBrowseCacheFolder;
    private System.Windows.Forms.CheckBox cbSaveLogin;
  }
}