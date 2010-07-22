namespace TvdbTester
{
  partial class FanartControl
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.SuspendLayout();
      // 
      // FanartControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.DoubleBuffered = true;
      this.Name = "FanartControl";
      this.Size = new System.Drawing.Size(527, 471);
      this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FanartControl_MouseMove);
      this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FanartControl_KeyUp);
      this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FanartControl_MouseDown);
      this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FanartControl_MouseUp);
      this.MouseEnter += new System.EventHandler(this.FanartControl_MouseEnter);
      this.ResumeLayout(false);

    }

    #endregion
  }
}
