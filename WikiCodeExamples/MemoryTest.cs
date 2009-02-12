using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TvdbLib;
using TvdbLib.Cache;
using System.IO;
using TvdbLib.Data;
using TvdbLib.Data.Banner;

namespace WikiCodeExamples
{
  public partial class MemoryTest : Form
  {
    TvdbHandler m_tvdbHandler;
    String m_rootFolder;
    Dictionary<int, TvdbSeries> m_loadedSeries;

    public MemoryTest()
    {
      InitializeComponent();
      m_rootFolder = Application.StartupPath + "\\Cache";
      m_tvdbHandler = new TvdbHandler(new XmlCacheProvider(m_rootFolder), File.ReadAllText("api_key.txt"));
      m_loadedSeries = new Dictionary<int, TvdbSeries>();
    }

    private void cmdInit_Click(object sender, EventArgs e)
    {
      m_tvdbHandler.InitCache();
      m_tvdbHandler.UserInfo = new TvdbLib.Data.TvdbUser("DieBagger", txtUserId.Text);
      List<int> favorites = m_tvdbHandler.GetUserFavouritesList();
      favorites.ForEach(delegate(int f)
      {
        ListViewItem item = new ListViewItem(f.ToString());
        if (m_tvdbHandler.IsCached(f, TvdbLanguage.DefaultLanguage, true, true, true))
        {
          item.BackColor = Color.LightGreen;
        }
        else
        {
          item.BackColor = Color.White;
        }
        item.Tag = f;
        lvFavorites.Items.Add(item);
      });
    }

    private void loadToolStripMenuItem_Click(object sender, EventArgs e)
    {
      foreach (ListViewItem i in lvFavorites.SelectedItems)
      {
        int seriesId = (int)i.Tag;
        TvdbSeries series = m_tvdbHandler.GetSeries(seriesId, TvdbLanguage.DefaultLanguage, true, true, true);
        if (series != null)
        {
          m_loadedSeries.Add(seriesId, series);
          i.BackColor = Color.DarkSeaGreen;
        }
      }
    }

    private void unloadToolStripMenuItem_Click(object sender, EventArgs e)
    {
      foreach (ListViewItem i in lvFavorites.SelectedItems)
      {
        int seriesId = (int)i.Tag;
        if (m_loadedSeries.ContainsKey(seriesId))
        {
          m_loadedSeries.Remove(seriesId);
          i.BackColor = Color.LightGreen;
        }       
      }
    }

    private void MemoryTest_FormClosed(object sender, FormClosedEventArgs e)
    {
      m_tvdbHandler.SaveCache();
    }

    private void cmSeriesListView_Opening(object sender, CancelEventArgs e)
    {
      if (lvFavorites.SelectedItems.Count == 1)
      {
        if (m_loadedSeries.ContainsKey((int)lvFavorites.SelectedItems[0].Tag))
        {
          TvdbSeries s = m_loadedSeries[(int)lvFavorites.SelectedItems[0].Tag];
          int count = 0;
          s.FanartBanners.ForEach(delegate(TvdbFanartBanner b) { if (b.IsLoaded)count++; });
          loadFanart0ToolStripMenuItem.Text = "Load Fanart (" + count + " of " + s.FanartBanners.Count + ")";
          if (count == s.FanartBanners.Count)
          {
            loadFanart0ToolStripMenuItem.Enabled = false;
          }
          else
          {
            loadFanart0ToolStripMenuItem.Enabled = true;
          }
          unloadToolStripMenuItem.Enabled = true;
          loadToolStripMenuItem.Enabled = false;
        }
        else
        {
          loadFanart0ToolStripMenuItem.Enabled = false;
          unloadToolStripMenuItem.Enabled = false;
          loadToolStripMenuItem.Enabled = true;
        }
      }
    }

    private void loadFanart0ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (lvFavorites.SelectedItems.Count == 1 && m_loadedSeries.ContainsKey((int)lvFavorites.SelectedItems[0].Tag))
      {
        TvdbSeries s = m_loadedSeries[(int)lvFavorites.SelectedItems[0].Tag];
        foreach (TvdbBanner b in s.FanartBanners)
        {
          if (!b.IsLoaded)
          {
            b.LoadBanner();
            return;
          }
        }
      }
    }
  }
}
