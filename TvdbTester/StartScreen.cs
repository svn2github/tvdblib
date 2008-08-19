using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TvdbTester
{
  public partial class StartScreen : Form
  {
    public StartScreen()
    {
      InitializeComponent();
    }

    private String m_userIdentifier;
    public String UserIdentifier
    {
      get { return m_userIdentifier; }
      set { m_userIdentifier = value; }
    }

    private void cmdStart_Click(object sender, EventArgs e)
    {
      if(txtUserIdentifier.Text.Equals("") || txtUserIdentifier.Text.Length != 16)
      {
        tsStatus.Text = "Please specify a valid user id";
      }
      else
      {
        this.DialogResult = DialogResult.OK;
        UserIdentifier = txtUserIdentifier.Text;
        this.Close();
      }
    }

    private void richTextBox1_TextChanged(object sender, EventArgs e)
    {

    }
  }
}
