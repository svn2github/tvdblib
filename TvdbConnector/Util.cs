using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TvdbConnector.Data;
using TvdbConnector.Cache;
using System.Drawing;
using System.Globalization;
using TvdbConnector.Data.Banner;

namespace TvdbConnector
{
  internal class Util
  {
    /// <summary>
    /// Update interval
    /// </summary>
    internal enum UpdateInterval { day = 0, week = 1, month = 1 };

    /// <summary>
    /// Type when handling user favorites
    /// </summary>
    internal enum UserFavouriteAction { none, add, remove }

    #region private fields
    private static List<TvdbLanguage> m_languageList;
    #endregion

    /// <summary>
    /// List of available languages -> needed for some methods
    /// </summary>
    public static List<TvdbLanguage> LanguageList
    {
      get { return m_languageList; }
      set { m_languageList = value; }
    }

    /// <summary>
    /// Parses an integer string and returns the number or -99 if the format
    /// is invalid
    /// </summary>
    /// <param name="_number"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Parses an double string and returns the number or -99 if the format
    /// is invalid
    /// </summary>
    /// <param name="_number"></param>
    /// <returns></returns>
    internal static double DoubleParse(string _number)
    {
      try
      {
        _number = _number.Replace(',', '.');
        NumberFormatInfo nfi = new NumberFormatInfo();
        nfi.NumberGroupSeparator = ".";
        return Double.Parse(_number, nfi);
      }
      catch (FormatException)
      {
        return -99;
      }
    }

    /// <summary>
    /// Splits a tvdb string (having the format | item1 | item2 | item3 |)
    /// </summary>
    /// <param name="_text"></param>
    /// <returns></returns>
    internal static List<String> SplitTvdbString(String _text)
    {
      List<String> list = new List<string>();
      String[] values = _text.Split('|');
      foreach (String v in values)
      {
        if (!v.Equals("")) list.Add(v);
      }

      return list;
    }

    /// <summary>
    /// Parse the short description of a tvdb language and returns the proper
    /// object. If no such language exists yet (maybe the list of available
    /// languages hasn't been downloaded yet), a placeholder is created
    /// </summary>
    /// <param name="_shortLanguageDesc"></param>
    /// <returns></returns>
    internal static TvdbLanguage ParseLanguage(String _shortLanguageDesc)
    {
      if (m_languageList != null)
      {
        foreach (TvdbLanguage l in m_languageList)
        {
          if (l.Abbriviation == _shortLanguageDesc)
          {
            return l;
          }
        }
      }
      else
      {
        m_languageList = new List<TvdbLanguage>();
      }

      //the language doesn't exist yet -> create placeholder
      TvdbLanguage lang = new TvdbLanguage(-99, "unknown", _shortLanguageDesc);
      m_languageList.Add(lang);
      return lang;
    }

    /// <summary>
    /// Converts a unix timestamp (used on tvdb) into a .net datetime object
    /// </summary>
    /// <param name="_unixTimestamp"></param>
    /// <returns></returns>
    internal static DateTime UnixToDotNet(String _unixTimestamp)
    {
      System.DateTime date = System.DateTime.Parse("1/1/1970");
      return date.AddSeconds(Int32.Parse(_unixTimestamp));
    }

    /// <summary>
    /// Converts a .net datetime object into a unix timestamp (used on tvdb)  
    /// </summary>
    /// <param name="_unixTimestamp"></param>
    /// <returns></returns>
    internal static String DotNetToUnix(DateTime _date)
    {
      TimeSpan span = (_date - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
      return span.TotalSeconds.ToString();
    }

    /// <summary>
    /// returns a day of the week object parsed from the string
    /// </summary>
    /// <param name="_dayOfWeek"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Returns a List of colors parsed from the _text
    /// </summary>
    /// <param name="_text"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Returns a point objects parsed from _text
    /// </summary>
    /// <param name="_text"></param>
    /// <returns></returns>
    internal static Point ParseResolution(String _text)
    {
      String[] res = _text.Split('x');
      return new Point(Int32.Parse(res[0]), Int32.Parse(res[1]));
      //throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_type"></param>
    /// <returns></returns>
    internal static TvdbSeasonBanner.Type ParseSeasonBannerType(String _type)
    {
      if (_type.Equals("season")) return TvdbConnector.Data.Banner.TvdbSeasonBanner.Type.season;
      else if (_type.Equals("seasonwide")) return TvdbConnector.Data.Banner.TvdbSeasonBanner.Type.seasonwide;
      else return TvdbConnector.Data.Banner.TvdbSeasonBanner.Type.none;
    }

    /// <summary>
    /// Returns the fitting SeriesBanner type from parameter
    /// </summary>
    /// <param name="_type"></param>
    /// <returns></returns>
    internal static TvdbSeriesBanner.Type ParseSeriesBannerType(String _type)
    {
      if (_type.Equals("season")) return TvdbConnector.Data.Banner.TvdbSeriesBanner.Type.blank;
      else if (_type.Equals("graphical")) return TvdbConnector.Data.Banner.TvdbSeriesBanner.Type.graphical;
      else if (_type.Equals("text")) return TvdbConnector.Data.Banner.TvdbSeriesBanner.Type.text;
      else return TvdbConnector.Data.Banner.TvdbSeriesBanner.Type.none;
    }


    /// <summary>
    /// Add the episode to the series
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
