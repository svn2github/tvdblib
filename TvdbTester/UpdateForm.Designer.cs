namespace TvdbTester
{
  partial class UpdateForm
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
      this.lblUpdateStage = new System.Windows.Forms.Label();
      this.txtUpdateProgress = new System.Windows.Forms.TextBox();
      this.cmdApply = new System.Windows.Forms.Button();
      this.cmdAbortUpdate = new System.Windows.Forms.Button();
      this.ibUpdateProgress = new MG.Controls.BarLib.iBar();
      this.SuspendLayout();
      // 
      // lblUpdateStage
      // 
      this.lblUpdateStage.AutoSize = true;
      this.lblUpdateStage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblUpdateStage.Location = new System.Drawing.Point(25, 9);
      this.lblUpdateStage.Name = "lblUpdateStage";
      this.lblUpdateStage.Size = new System.Drawing.Size(82, 20);
      this.lblUpdateStage.TabIndex = 0;
      this.lblUpdateStage.Text = "Updating";
      // 
      // txtUpdateProgress
      // 
      this.txtUpdateProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtUpdateProgress.Location = new System.Drawing.Point(11, 74);
      this.txtUpdateProgress.Multiline = true;
      this.txtUpdateProgress.Name = "txtUpdateProgress";
      this.txtUpdateProgress.ReadOnly = true;
      this.txtUpdateProgress.Size = new System.Drawing.Size(269, 117);
      this.txtUpdateProgress.TabIndex = 1;
      // 
      // cmdApply
      // 
      this.cmdApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.cmdApply.Location = new System.Drawing.Point(11, 211);
      this.cmdApply.Name = "cmdApply";
      this.cmdApply.Size = new System.Drawing.Size(121, 23);
      this.cmdApply.TabIndex = 2;
      this.cmdApply.Text = "Ok";
      this.cmdApply.UseVisualStyleBackColor = true;
      this.cmdApply.Click += new System.EventHandler(this.cmdApply_Click);
      // 
      // cmdAbortUpdate
      // 
      this.cmdAbortUpdate.Enabled = false;
      this.cmdAbortUpdate.Location = new System.Drawing.Point(138, 211);
      this.cmdAbortUpdate.Name = "cmdAbortUpdate";
      this.cmdAbortUpdate.Size = new System.Drawing.Size(142, 23);
      this.cmdAbortUpdate.TabIndex = 4;
      this.cmdAbortUpdate.Text = "Abort Update";
      this.cmdAbortUpdate.UseVisualStyleBackColor = true;
      this.cmdAbortUpdate.Click += new System.EventHandler(this.cmdAbortUpdate_Click);
      // 
      // ibUpdateProgress
      // 
      this.ibUpdateProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.ibUpdateProgress.BarBackgroundDark = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(200)))), ((int)(((byte)(201)))));
      this.ibUpdateProgress.BarBackgroundLight = System.Drawing.Color.WhiteSmoke;
      this.ibUpdateProgress.BarBorderColor = System.Drawing.Color.DarkGray;
      this.ibUpdateProgress.BarBorderWidth = 1.25F;
      this.ibUpdateProgress.BarDark = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(68)))), ((int)(((byte)(202)))));
      this.ibUpdateProgress.BarFillProcent = 0F;
      this.ibUpdateProgress.BarLight = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(144)))), ((int)(((byte)(252)))));
      this.ibUpdateProgress.BarMirrorOpacity = 40F;
      this.ibUpdateProgress.BarType = MG.Controls.BarLib.BarType.Static;
      this.ibUpdateProgress.Location = new System.Drawing.Point(11, 36);
      this.ibUpdateProgress.Name = "ibUpdateProgress";
      this.ibUpdateProgress.Size = new System.Drawing.Size(269, 36);
      this.ibUpdateProgress.StepInterval = 100;
      this.ibUpdateProgress.StepSize = 0F;
      this.ibUpdateProgress.TabIndex = 3;
      // 
      // UpdateForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(294, 246);
      this.ControlBox = false;
      this.Controls.Add(this.cmdAbortUpdate);
      this.Controls.Add(this.ibUpdateProgress);
      this.Controls.Add(this.cmdApply);
      this.Controls.Add(this.txtUpdateProgress);
      this.Controls.Add(this.lblUpdateStage);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "UpdateForm";
      this.Text = "UpdateForm";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblUpdateStage;
    private System.Windows.Forms.TextBox txtUpdateProgress;
    private System.Windows.Forms.Button cmdApply;
    private MG.Controls.BarLib.iBar ibUpdateProgress;
    private System.Windows.Forms.Button cmdAbortUpdate;
  }
}