using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TvdbConnector;
using TvdbConnector.Data;
using System.IO;

namespace WikiCodeExamples
{
  public partial class WikiCodeExamples : Form
  {
    public WikiCodeExamples()
    {
      InitializeComponent();
    }

    private void cmdExample1_Click(object sender, EventArgs args)
    {
      Tvdb tvdbHandler = new Tvdb(null, File.ReadAllText("api_key.txt"));//no caching -> same as "new Tvdb(insert_my_apikey);"
      //retrieve series "House" with all available information (without using zipped downloading)
      TvdbSeries s = tvdbHandler.GetSeries(73255, TvdbLanguage.DefaultLanguage, true, true, true, false);
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
  }
}
