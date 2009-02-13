using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TvdbLib;
using TvdbLib.Data;
using System.IO;
using TvdbLib.Cache;

namespace WikiCodeExamples
{
  public partial class WikiCodeExamples : Form
  {
    String m_rootFolder;
    ICacheProvider m_cacheProvider = null;
    TvdbHandler m_tvdbHandler = null;

    public WikiCodeExamples()
    {
      InitializeComponent();
      m_rootFolder = Application.StartupPath;
      txtDirectory.Text = m_rootFolder;
    }


    #region init and setup tvdblib handler

    private void cmdInit_Click(object sender, EventArgs e)
    {
      m_cacheProvider = null;
      if (cbUseCaching.Checked)
      {
        if (!Directory.Exists(m_rootFolder)) Directory.CreateDirectory(m_rootFolder);
        m_cacheProvider = new XmlCacheProvider(m_rootFolder);
      }

      //no caching -> null or "new Tvdb(insert_my_apikey);"
      m_tvdbHandler = new TvdbHandler(m_cacheProvider, File.ReadAllText("api_key.txt"));
    }

    #endregion

    #region Code Example 1 -> retrieving series

    private void cmdExample1_Click(object sender, EventArgs args)
    {
      int seriesId = Int32.Parse(txtSeriesId.Text);
      
      //retrieve series "e.g. 73255 (House M.D.)" with all available information (without using zipped downloading)
      TvdbSeries s = m_tvdbHandler.GetSeries(seriesId, TvdbLanguage.DefaultLanguage, true, true, true, false);
      if (s != null)
      {
        WriteToConsole("Series Description:");
        WriteToConsole("================");
        WriteToConsole("Series id: " + s.Id);
        WriteToConsole("Name: " + s.SeriesName);
        WriteToConsole("Overview: " + s.Overview);
        WriteToConsole("Day of Week: " + s.AirsDayOfWeek);
        WriteToConsole("Airs Time: " + s.AirsTime.ToShortTimeString());
        WriteToConsole("Banner Path: " + s.BannerPath);
        WriteToConsole("Content Rating: " + s.ContentRating);
        WriteToConsole("Fanart Path: " + s.FanartPath);
        WriteToConsole("First Aired: " + s.FirstAired.ToShortDateString());
        WriteToConsole("Genres: " + s.GenreString);
        WriteToConsole("Imdb: " + s.ImdbId);
        WriteToConsole("Rating: " + s.Rating);
        WriteToConsole("Runtime: " + s.Runtime);
        WriteToConsole("Status: " + s.Status);
        WriteToConsole("tv.com id: " + s.TvDotComId);
        WriteToConsole("Zap2itId id: " + s.Zap2itId);
        WriteToConsole("");

        WriteToConsole("Episodes for " + s.SeriesName + ":");
        WriteToConsole("=================");
        foreach (TvdbEpisode e in s.Episodes)
        {
          WriteToConsole("Season " + e.SeasonNumber + " Episode " + e.EpisodeNumber + ": " + e.EpisodeName);
        }
        WriteToConsole("");

        WriteToConsole("Actors:");
        WriteToConsole("======");
        foreach (TvdbActor a in s.TvdbActors)
        {
          WriteToConsole(a.Name + " as " + a.Role + " (" + a.ActorImage.BannerPath + ")");
        }

        WriteToConsole("Banner:");
        WriteToConsole("======");
        foreach (TvdbBanner b in s.Banners)
        {
          WriteToConsole(b.Id + ": " + b.BannerPath);
        }

      }
      else
      {
        WriteToConsole("Couldn't find series");
      }
    }

    private void WriteToConsole(String _msg)
    {
      Console.WriteLine(_msg);
      lbLog.Items.Add(_msg);
    }

    private void ClearConsole()
    {
      lbLog.Items.Clear();
    }
    #endregion

    #region Code Example 2 -> caching

    private void cmdLoadCache_Click(object sender, EventArgs e)
    {
      m_tvdbHandler.InitCache();
    }

    private void cmdSaveCache_Click(object sender, EventArgs e)
    {
      m_tvdbHandler.SaveCache();
    }

    #endregion

    private void cbUseCaching_CheckedChanged(object sender, EventArgs e)
    {
      txtDirectory.Enabled = cbUseCaching.Checked;
      cmdBrowseCacheDirectory.Enabled = cbUseCaching.Checked;
    }

    private void cmdShowCachedSeries_Click(object sender, EventArgs e)
    {
      List<int> cachedSeries = m_tvdbHandler.GetCachedSeries();

      lvCachedSeries.Items.Clear();
      foreach (int s in cachedSeries)
      {
        lvCachedSeries.Items.Add(s.ToString());
      }
    }

    #region searching

    private void cmdSearchSeries_Click(object sender, EventArgs e)
    {
      List<TvdbSearchResult> res = m_tvdbHandler.SearchSeries(txtSearchText.Text);
      lbSearchResult.Items.Clear();
      foreach (TvdbSearchResult s in res)
      {
        AddSearchLogEntry(s.SeriesName + "(" + s.Id + "):");
        AddSearchLogEntry("=========================");
        AddSearchLogEntry(s.Overview);
        AddSearchLogEntry("Imdb: " + s.ImdbId);
        AddSearchLogEntry("Banner: " + s.Banner.BannerPath);
        AddSearchLogEntry("First Aired: " + s.FirstAired.ToShortDateString());
        AddSearchLogEntry("");
      }
    }
    
    private void AddSearchLogEntry(String _text)
    {
      Console.WriteLine(_text);
      lbSearchResult.Items.Add(_text);
      //txtSearchText.Text.Appe
    }

    #endregion




  }
}
