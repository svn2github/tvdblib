namespace TestPrograms
{
  partial class TestMain
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
      this.cmdShowUpdateTestForm = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.cmdShowMemoryTestForm = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.cmdShowImageTestForm = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      this.cmdShowMainTest = new System.Windows.Forms.Button();
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // cmdShowUpdateTestForm
      // 
      this.cmdShowUpdateTestForm.Location = new System.Drawing.Point(143, 156);
      this.cmdShowUpdateTestForm.Name = "cmdShowUpdateTestForm";
      this.cmdShowUpdateTestForm.Size = new System.Drawing.Size(129, 23);
      this.cmdShowUpdateTestForm.TabIndex = 0;
      this.cmdShowUpdateTestForm.Text = "Show";
      this.cmdShowUpdateTestForm.UseVisualStyleBackColor = true;
      this.cmdShowUpdateTestForm.Click += new System.EventHandler(this.cmdShowUpdateTestForm_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(12, 161);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(69, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Updatetest";
      // 
      // cmdShowMemoryTestForm
      // 
      this.cmdShowMemoryTestForm.Location = new System.Drawing.Point(143, 230);
      this.cmdShowMemoryTestForm.Name = "cmdShowMemoryTestForm";
      this.cmdShowMemoryTestForm.Size = new System.Drawing.Size(129, 23);
      this.cmdShowMemoryTestForm.TabIndex = 0;
      this.cmdShowMemoryTestForm.Text = "Show";
      this.cmdShowMemoryTestForm.UseVisualStyleBackColor = true;
      this.cmdShowMemoryTestForm.Click += new System.EventHandler(this.cmdShowMemoryTestForm_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(12, 235);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(71, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "Memorytest";
      // 
      // cmdShowImageTestForm
      // 
      this.cmdShowImageTestForm.Location = new System.Drawing.Point(143, 303);
      this.cmdShowImageTestForm.Name = "cmdShowImageTestForm";
      this.cmdShowImageTestForm.Size = new System.Drawing.Size(129, 23);
      this.cmdShowImageTestForm.TabIndex = 0;
      this.cmdShowImageTestForm.Text = "Show";
      this.cmdShowImageTestForm.UseVisualStyleBackColor = true;
      this.cmdShowImageTestForm.Click += new System.EventHandler(this.cmdShowImageTestForm_Click);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(12, 308);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(62, 13);
      this.label3.TabIndex = 1;
      this.label3.Text = "Imagetest";
      // 
      // cmdShowMainTest
      // 
      this.cmdShowMainTest.Location = new System.Drawing.Point(143, 80);
      this.cmdShowMainTest.Name = "cmdShowMainTest";
      this.cmdShowMainTest.Size = new System.Drawing.Size(129, 23);
      this.cmdShowMainTest.TabIndex = 0;
      this.cmdShowMainTest.Text = "Show";
      this.cmdShowMainTest.UseVisualStyleBackColor = true;
      this.cmdShowMainTest.Click += new System.EventHandler(this.cmdShowMainTest_Click);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label4.Location = new System.Drawing.Point(12, 85);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(55, 13);
      this.label4.TabIndex = 1;
      this.label4.Text = "Maintest";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic)
                      | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label5.Location = new System.Drawing.Point(83, 22);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(119, 17);
      this.label5.TabIndex = 1;
      this.label5.Text = "Available Tests";
      // 
      // TestMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(284, 406);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.cmdShowMainTest);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.cmdShowImageTestForm);
      this.Controls.Add(this.cmdShowMemoryTestForm);
      this.Controls.Add(this.cmdShowUpdateTestForm);
      this.Name = "TestMain";
      this.Text = "Test Programs";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button cmdShowUpdateTestForm;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button cmdShowMemoryTestForm;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button cmdShowImageTestForm;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button cmdShowMainTest;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
  }
}

