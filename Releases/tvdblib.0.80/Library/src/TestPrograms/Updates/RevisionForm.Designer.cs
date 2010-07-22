namespace TestPrograms.Updates
{
  partial class RevisionForm
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
      this.cmdOk = new System.Windows.Forms.Button();
      this.cmdCancel = new System.Windows.Forms.Button();
      this.txtInputName = new System.Windows.Forms.TextBox();
      this.lblDescription = new System.Windows.Forms.Label();
      this.txtInputDescription = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // cmdOk
      // 
      this.cmdOk.Location = new System.Drawing.Point(70, 175);
      this.cmdOk.Name = "cmdOk";
      this.cmdOk.Size = new System.Drawing.Size(137, 23);
      this.cmdOk.TabIndex = 0;
      this.cmdOk.Text = "Ok";
      this.cmdOk.UseVisualStyleBackColor = true;
      this.cmdOk.Click += new System.EventHandler(this.button1_Click);
      // 
      // cmdCancel
      // 
      this.cmdCancel.Location = new System.Drawing.Point(259, 175);
      this.cmdCancel.Name = "cmdCancel";
      this.cmdCancel.Size = new System.Drawing.Size(141, 23);
      this.cmdCancel.TabIndex = 1;
      this.cmdCancel.Text = "Cancel";
      this.cmdCancel.UseVisualStyleBackColor = true;
      this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
      // 
      // txtInputName
      // 
      this.txtInputName.Location = new System.Drawing.Point(115, 12);
      this.txtInputName.Name = "txtInputName";
      this.txtInputName.Size = new System.Drawing.Size(336, 20);
      this.txtInputName.TabIndex = 2;
      // 
      // lblDescription
      // 
      this.lblDescription.AutoSize = true;
      this.lblDescription.Location = new System.Drawing.Point(23, 15);
      this.lblDescription.Name = "lblDescription";
      this.lblDescription.Size = new System.Drawing.Size(35, 13);
      this.lblDescription.TabIndex = 3;
      this.lblDescription.Text = "Name";
      // 
      // txtInputDescription
      // 
      this.txtInputDescription.Location = new System.Drawing.Point(115, 38);
      this.txtInputDescription.Multiline = true;
      this.txtInputDescription.Name = "txtInputDescription";
      this.txtInputDescription.Size = new System.Drawing.Size(336, 131);
      this.txtInputDescription.TabIndex = 2;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(23, 41);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(60, 13);
      this.label1.TabIndex = 3;
      this.label1.Text = "Description";
      // 
      // InputForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(463, 210);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.lblDescription);
      this.Controls.Add(this.txtInputDescription);
      this.Controls.Add(this.txtInputName);
      this.Controls.Add(this.cmdCancel);
      this.Controls.Add(this.cmdOk);
      this.Name = "InputForm";
      this.Text = "InputForm";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button cmdOk;
    private System.Windows.Forms.Button cmdCancel;
    private System.Windows.Forms.TextBox txtInputName;
    private System.Windows.Forms.Label lblDescription;
    private System.Windows.Forms.TextBox txtInputDescription;
    private System.Windows.Forms.Label label1;
  }
}