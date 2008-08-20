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
      this.bcActors = new TvdbTester.BannerControl();
      this.SuspendLayout();
      // 
      // cmdTest1
      // 
      this.cmdTest1.Location = new System.Drawing.Point(22, 22);
      this.cmdTest1.Name = "cmdTest1";
      this.cmdTest1.Size = new System.Drawing.Size(137, 23);
      this.cmdTest1.TabIndex = 0;
      this.cmdTest1.Text = "Test 1";
      this.cmdTest1.UseVisualStyleBackColor = true;
      this.cmdTest1.Click += new System.EventHandler(this.cmdTest1_Click);
      // 
      // bcActors
      // 
      this.bcActors.BannerImage = null;
      this.bcActors.BannerImages = null;
      this.bcActors.DefaultImage = null;
      this.bcActors.ImageSizeMode = System.Windows.Forms.ImageLayout.Zoom;
      this.bcActors.LoadingBackgroundColor = System.Drawing.Color.Black;
      this.bcActors.LoadingImage = global::TvdbTester.Properties.Resources.loader4;
      this.bcActors.Location = new System.Drawing.Point(22, 67);
      this.bcActors.Name = "bcActors";
      this.bcActors.Size = new System.Drawing.Size(300, 450);
      this.bcActors.TabIndex = 1;
      // 
      // TestForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(352, 557);
      this.Controls.Add(this.bcActors);
      this.Controls.Add(this.cmdTest1);
      this.Name = "TestForm";
      this.Text = "TestForm";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button cmdTest1;
    private BannerControl bcActors;
  }
}