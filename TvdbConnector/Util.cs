using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TvdbConnector.Data;
using TvdbConnector.Cache;
using System.Drawing;

namespace TvdbConnector
{
  internal class Util
  {
    internal enum UpdateInterval { day = 0, week = 1, month = 1 };

    internal static int Int32Parse(String _number)
    {
      try
      {
        return Int32.Parse(_number);
      }
      catch (FormatException)
      {
        return -99;
      }
    }

    internal static double DoubleParse(string _number)
    {
      try
      {
        return Double.Parse(_number);
      }
      catch (FormatException)
      {
        return -99;
      }
    }

    internal static List<String> SplitTvdbString(String _text)
    {
      List<String> list = new List<string>();
      String[] values = _text.Split('|');
      foreach (String v in values)
      {
        if(!v.Equals(""))list.Add(v);
      }

      return list;
    }

    internal static TvdbLanguage ParseLanguage(String _shortLanguageDesc)
    {
      if (TvdbCache.LanguageList == null) return null;
      foreach (TvdbLanguage l in TvdbCache.LanguageList)
      {
        if (l.Abbriviation == _shortLanguageDesc)
        {
          return l;
        }
      }
      return null;
    }

    internal static DateTime UnixToDotNet(String _unixTimestamp)
    {
      System.DateTime date = System.DateTime.Parse("1/1/1970");
      return date.AddSeconds(Int32.Parse(_unixTimestamp));
    }

    internal static DayOfWeek? GetDayOfWeek(string _dayOfWeek)
    {
      switch (_dayOfWeek.ToLower())
      {
        case "monday":
        case "montag":
        case "mo":
          return DayOfWeek.Monday;
        case "tuesday":
        case "dienstag":
        case "di":
          return DayOfWeek.Tuesday;
        case "wednesday":
        case "mittwoch":
        case "mi":
          return DayOfWeek.Wednesday;
        case "thursday":
        case "donnerstag":
        case "do":
          return DayOfWeek.Thursday;
        case "friday":
        case "freitag":
        case "fr":
          return DayOfWeek.Friday;
        case "saturday":
        case "samstag":
        case "sa":
          return DayOfWeek.Saturday;
        case "sunday":
        case "sonntag":
        case "so":
          return DayOfWeek.Sunday;
        default:
          return null;
      }
    }



    internal static List<Color> ParseColors(String _text)
    {
      List<Color> retList = new List<Color>();
      List<String> colorList = SplitTvdbString(_text);
      for (int i = 0; i < colorList.Count; i++)
      {
        String[] color = colorList[i].Split(',');
        retList.Add(Color.FromArgb(Int32.Parse(color[0]), Int32.Parse(color[1]), Int32.Parse(color[2])));
      }
      return null;
      //throw new NotImplementedException();
    }

    internal static System.Drawing.Point ParseResolution(String _text)
    {
      String[] res = _text.Split('x');
      return new Point(Int32.Parse(res[0]), Int32.Parse(res[0]));
      //throw new NotImplementedException();
    }

    internal static TvdbConnector.Data.Banner.TvdbSeasonBanner.Type ParseSeasonBannerType(String _type)
    {
      if (_type.Equals("season")) return TvdbConnector.Data.Banner.TvdbSeasonBanner.Type.season;
      else if (_type.Equals("seasonwide")) return TvdbConnector.Data.Banner.TvdbSeasonBanner.Type.seasonwide;
      else return TvdbConnector.Data.Banner.TvdbSeasonBanner.Type.none;
    }

    internal static TvdbConnector.Data.Banner.TvdbSeriesBanner.Type ParseSeriesBannerType(String _type)
    {
      if (_type.Equals("season")) return TvdbConnector.Data.Banner.TvdbSeriesBanner.Type.blank;
      else if (_type.Equals("graphical")) return TvdbConnector.Data.Banner.TvdbSeriesBanner.Type.graphical;
      else if (_type.Equals("text")) return TvdbConnector.Data.Banner.TvdbSeriesBanner.Type.text;
      else return TvdbConnector.Data.Banner.TvdbSeriesBanner.Type.none;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="_episode"></param>
    /// <param name="_series"></param>
    internal static void AddEpisodeToSeries(TvdbEpisode _episode, TvdbSeries _series)
    {
      bool episodeFound = false; ;
      for (int i = 0; i < _series.Episodes.Count; i++)
      {
        if (_series.Episodes[i].Id == _episode.Id)
        {//we have already stored this episode -> overwrite it
          _series.Episodes[i].UpdateEpisodeInfo(_episode);
          episodeFound = true;
          break;
        }
      }
      if (!episodeFound)
      {//the episode doesn't exist yet
        _series.Episodes.Add(_episode);
        if (!_series.EpisodesLoaded) _series.EpisodesLoaded = true;
      }
    }
  }
}
