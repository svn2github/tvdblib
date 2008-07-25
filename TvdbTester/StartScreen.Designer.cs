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
      this.cmdStart = new System.Windows.Forms.Button();
      this.txtUserIdentifier = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.richTextBox1 = new System.Windows.Forms.RichTextBox();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // cmdStart
      // 
      this.cmdStart.Location = new System.Drawing.Point(48, 295);
      this.cmdStart.Name = "cmdStart";
      this.cmdStart.Size = new System.Drawing.Size(268, 23);
      this.cmdStart.TabIndex = 0;
      this.cmdStart.Text = "Start";
      this.cmdStart.UseVisualStyleBackColor = true;
      this.cmdStart.Click += new System.EventHandler(this.cmdStart_Click);
      // 
      // txtUserIdentifier
      // 
      this.txtUserIdentifier.Location = new System.Drawing.Point(129, 269);
      this.txtUserIdentifier.Name = "txtUserIdentifier";
      this.txtUserIdentifier.Size = new System.Drawing.Size(187, 20);
      this.txtUserIdentifier.TabIndex = 1;
      this.txtUserIdentifier.Text = "2EF20123489774E9";
      this.txtUserIdentifier.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(48, 272);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(75, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "User Identifier:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(12, 124);
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
      this.richTextBox1.Location = new System.Drawing.Point(15, 157);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.ReadOnly = true;
      this.richTextBox1.Size = new System.Drawing.Size(356, 94);
      this.richTextBox1.TabIndex = 5;
      this.richTextBox1.Text = "To get your personal user id, go to http://thetvdb.com and register an account. A" +
          "fter logging in at the page you can access your \"Account Identifier\"  on the \"Ac" +
          "count\" page. ";
      this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
      // 
      // StartScreen
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(383, 330);
      this.Controls.Add(this.richTextBox1);
      this.Controls.Add(this.pictureBox1);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txtUserIdentifier);
      this.Controls.Add(this.cmdStart);
      this.Name = "StartScreen";
      this.Text = "StartScreen";
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
  }
}