using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TvdbConnector.Data
{
  /// <summary>
  /// This class represents all fields that are available on http://thetvdb.com and
  /// a list of episodefields. This is used for localised series information
  /// 
  /// These are like the following:
  ///       <id>73739</id>
  ///       <Actors>|Malcolm David Kelley|Jorge Garcia|Maggie Grace|...|</Actors>
  ///       <Airs_DayOfWeek>Thursday</Airs_DayOfWeek>
  ///       <Airs_Time>9:00 PM</Airs_Time>
  ///       <ContentRating>TV-14</ContentRating>
  ///       <FirstAired>2004-09-22</FirstAired>
  ///       <Genre>|Action and Adventure|Drama|Science-Fiction|</Genre>
  ///       <IMDB_ID>tt0411008</IMDB_ID>
  ///       <Language>en</Language>
  ///       <Network>ABC</Network>
  ///       <Overview>After Oceanic Air flight 815...</Overview>
  ///       <Rating>8.9</Rating>
  ///       <Runtime>60</Runtime>
  ///       <SeriesID>24313</SeriesID>
  ///       <SeriesName>Lost</SeriesName>
  ///       <Status>Continuing</Status>
  ///       <banner>graphical/24313-g2.jpg</banner>
  ///       <fanart>fanart/original/73739-1.jpg</fanart>
  ///       <lastupdated>1205694666</lastupdated>
  ///       <zap2it_id>SH672362</zap2it_id>
  /// </summary>
  [Serializable]
  public class TvdbSeriesFields
  {
    #region private fields
    private int m_id;
    private String m_seriesName;
    private List<String> m_actors;
    private DayOfWeek? m_airsDayOfWeek;
    private DateTime m_airsTime;
    private String m_contentRating;
    private DateTime m_firstAired;
    private List<String> m_genre;
    private String m_imdbId;
    private TvdbLanguage m_language;
    private String m_network;
    private String m_overview;
    private double m_rating;
    private double m_runtime;
    private int m_tvDotComId;
    private String m_status;
    private String m_bannerPath;
    private String m_fanartPath;
    private DateTime m_lastUpdated;
    private String m_zap2itId;

    private List<TvdbEpisode> m_episodes = null;
    #endregion

    /// <summary>
    /// List of episodes for this translation
    /// </summary>
    public List<TvdbEpisode> Episodes
    {
      get { return m_episodes; }
      set { m_episodes = value; }
    }

    /// <summary>
    /// Series Id
    /// </summary>
    public int Id
    {
      get { return m_id; }
      set { m_id = value; }
    }

    /// <summary>
    /// Series Name
    /// </summary>
    public String SeriesName
    {
      get { return m_seriesName; }
      set { m_seriesName = value; }
    }

    /// <summary>
    /// Series network
    /// </summary>
    public String Network
    {
      get { return m_network; }
      set { m_network = value; }
    }

    /// <summary>
    /// The language of the series
    /// </summary>
    public TvdbLanguage Language
    {
      get { return m_language; }
      set { m_language = value; }
    }

    /// <summary>
    /// Content-Rating of the series
    /// </summary>
    public string ContentRating
    {
      get { return m_contentRating; }
      set { m_contentRating = value; }
    }

    /// <summary>
    /// Zap2it Id of the series
    /// </summary>
    public String Zap2itId
    {
      get { return m_zap2itId; }
      set { m_zap2itId = value; }
    }

    /// <summary>
    /// When was the series updated the last time
    /// </summary>
    public DateTime LastUpdated
    {
      get { return m_lastUpdated; }
      set { m_lastUpdated = value; }
    }

    /// <summary>
    /// Path to the primary fanart banner
    /// </summary>
    public String FanartPath
    {
      get { return m_fanartPath; }
      set { m_fanartPath = value; }
    }

    /// <summary>
    /// Path to primary banner
    /// </summary>
    public String BannerPath
    {
      get { return m_bannerPath; }
      set { m_bannerPath = value; }
    }

    /// <summary>
    /// Status of the show
    /// </summary>
    public String Status
    {
      get { return m_status; }
      set { m_status = value; }
    }

    /// <summary>
    /// Tv.com id of the series
    /// </summary>
    public int TvDotComId
    {
      get { return m_tvDotComId; }
      set { m_tvDotComId = value; }
    }

    /// <summary>
    /// Runtime of the show
    /// </summary>
    public double Runtime
    {
      get { return m_runtime; }
      set { m_runtime = value; }
    }

    /// <summary>
    /// Rating of the series
    /// </summary>
    public double Rating
    {
      get { return m_rating; }
      set { m_rating = value; }
    }

    /// <summary>
    /// Overview of the series
    /// </summary>
    public String Overview
    {
      get { return m_overview; }
      set { m_overview = value; }
    }

    /// <summary>
    /// Imdb Id of the series
    /// </summary>
    public String ImdbId
    {
      get { return m_imdbId; }
      set { m_imdbId = value; }
    }

    /// <summary>
    /// List of the series' genres
    /// </summary>
    public List<String> Genre
    {
      get { return m_genre; }
      set { m_genre = value; }
    }

    /// <summary>
    /// The Date the series was first aired
    /// </summary>
    public DateTime FirstAired
    {
      get { return m_firstAired; }
      set { m_firstAired = value; }
    }

    /// <summary>
    /// At which time does the series air
    /// </summary>
    public DateTime AirsTime
    {
      get { return m_airsTime; }
      set { m_airsTime = value; }
    }

    /// <summary>
    /// At which day of the week does the series air
    /// </summary>
    public DayOfWeek? AirsDayOfWeek
    {
      get { return m_airsDayOfWeek; }
      set { m_airsDayOfWeek = value; }
    }

    /// <summary>
    /// List of actors that appear in this series
    /// </summary>
    public List<String> Actors
    {
      get { return m_actors; }
      set { m_actors = value; }
    }
  }
}
