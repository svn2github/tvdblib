using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TvdbLib.Cache;
using System.IO;

namespace TvdbTester
{
  public partial class StartScreen : Form
  {
    public StartScreen()
    {
      InitializeComponent();

      //fill CacheProvider combobox with possible choices
      cbCacheProvider.Text = "Save to XML";
      cbCacheProvider.Items.Add("Don't use caching");
      cbCacheProvider.Items.Add("Save to XML");
      cbCacheProvider.Items.Add("Save to Serialized");

      //default option
      m_cacheProvider = typeof(XmlCacheProvider);
      m_rootFolder = "Cache";
      txtRootFolder.Text = "Cache";

      if (File.Exists("tvdbUser.txt"))
      {
        cbSaveLogin.Checked = true;
        txtUserIdentifier.Text = File.ReadAllLines("tvdbUser.txt")[0];
      }
    }

    private String m_userIdentifier;
    public String UserIdentifier
    {
      get { return m_userIdentifier; }
      set { m_userIdentifier = value; }
    }

    private String m_rootFolder;
    public String RootFolder
    {
      get { return m_rootFolder; }
      set { m_rootFolder = value; }
    }

    private Type m_cacheProvider;
    public Type CacheProvider
    {
      get { return m_cacheProvider; }
      set { m_cacheProvider = value; }
    }


    private void cmdStart_Click(object sender, EventArgs e)
    {
      if(txtUserIdentifier.Text.Equals("") || txtUserIdentifier.Text.Length != 16)
      {
        tsStatus.Text = "Please specify a valid user id";
      }
      else
      {
        if (cbSaveLogin.Checked)
        {
          File.WriteAllText("tvdbUser.txt", txtUserIdentifier.Text);
        }
        this.DialogResult = DialogResult.OK;
        UserIdentifier = txtUserIdentifier.Text;
        this.Close();
      }
    }

    private void richTextBox1_TextChanged(object sender, EventArgs e)
    {

    }

    private void cmdBrowseCacheFolder_Click(object sender, EventArgs e)
    {
      DialogResult res = folderBrowseCache.ShowDialog();
      if (res == DialogResult.OK)
      {
        m_rootFolder = folderBrowseCache.SelectedPath;
        txtRootFolder.Text = folderBrowseCache.SelectedPath;
      }
    }

    private void cbCacheProvider_SelectedIndexChanged(object sender, EventArgs e)
    {
      switch (cbCacheProvider.SelectedIndex)
      {
        case 0:
          m_cacheProvider = null;
          break;
        case 1:
          m_cacheProvider = typeof(XmlCacheProvider);
          break;
        case 2:
          m_cacheProvider = typeof(BinaryCacheProvider);
          break;
      }
    }
  }
}
