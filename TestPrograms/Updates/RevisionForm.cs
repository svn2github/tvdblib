using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestPrograms.Updates
{
  public partial class RevisionForm : Form
  {
    public RevisionForm()
    {
      InitializeComponent();
    }

    public String DialogDescription
    {
      get { return lblDescription.Text; }
      set { lblDescription.Text = value; }
    }

    public String InputName
    {
      get { return txtInputName.Text; }
      set { txtInputName.Text = value; }
    }

    public String InputDescription
    {
      get { return txtInputDescription.Text; }
      set { txtInputDescription.Text = value; }
    }

    public String Title
    {
      get { return this.Text; }
      set { this.Text = value; }
    }

    public bool ReadOnly
    {
      set
      {
        txtInputDescription.ReadOnly = value;
        txtInputName.ReadOnly = value;

      }
    }

    private void button1_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
    }

    private void cmdCancel_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
    }
  }
}
