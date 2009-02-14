namespace TvdbTester
{
  partial class LoaderForm
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
      this.lblLoadingText = new System.Windows.Forms.Label();
      this.iBar1 = new MG.Controls.BarLib.iBar();
      this.SuspendLayout();
      // 
      // lblLoadingText
      // 
      this.lblLoadingText.AutoSize = true;
      this.lblLoadingText.Location = new System.Drawing.Point(12, 9);
      this.lblLoadingText.Name = "lblLoadingText";
      this.lblLoadingText.Size = new System.Drawing.Size(45, 13);
      this.lblLoadingText.TabIndex = 1;
      this.lblLoadingText.Text = "Loading";
      // 
      // iBar1
      // 
      this.iBar1.BarBackgroundDark = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(200)))), ((int)(((byte)(201)))));
      this.iBar1.BarBackgroundLight = System.Drawing.Color.WhiteSmoke;
      this.iBar1.BarBorderColor = System.Drawing.Color.DarkGray;
      this.iBar1.BarBorderWidth = 1.25F;
      this.iBar1.BarDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(68)))), ((int)(((byte)(202)))));
      this.iBar1.BarLight = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(144)))), ((int)(((byte)(252)))));
      this.iBar1.BarMirrorOpacity = 40F;
      this.iBar1.BarType = MG.Controls.BarLib.BarType.Static;
      this.iBar1.Location = new System.Drawing.Point(12, 42);
      this.iBar1.Name = "iBar1";
      this.iBar1.Size = new System.Drawing.Size(112, 24);
      this.iBar1.StepInterval = 100;
      this.iBar1.StepSize = 0F;
      this.iBar1.TabIndex = 0;
      // 
      // LoaderForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(131, 68);
      this.Controls.Add(this.lblLoadingText);
      this.Controls.Add(this.iBar1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "LoaderForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "LoaderForm";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private MG.Controls.BarLib.iBar iBar1;
    private System.Windows.Forms.Label lblLoadingText;
  }
}