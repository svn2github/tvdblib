using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TvdbConnector.Data.Banner;

namespace TvdbConnector.Data
{
  [Serializable]
  public class TvdbEpisode
  {

    #region private fields

    private TvdbEpisodeBanner m_banner;
    private int m_id;
    private int m_combinedEpisodeNumber;
    private int m_combinedSeason;
    private int m_dvdChapter;
    private int m_dvdDiscId;
    private int m_dvdEpisodeNumber;
    private int m_dvdSeason;
    private List<String> m_directors;
    private String m_episodeName;
    private int m_episodeNumber;
    private DateTime m_firstAired;
    private List<String> m_guestStars;
    private String m_imdbId;
    private TvdbLanguage m_language;
    private String m_overview;
    private String m_productionCode;
    private double m_rating;
    private int m_seasonNumber;
    private List<String> m_writer;
    private int m_absoluteNumber;
    private String m_bannerPath;
    private DateTime m_lastUpdated;
    private int m_seasonId;
    private int m_seriesId;
    private int m_airsAfterSeason;
    private int m_airsBeforeEpisode;
    private int m_airsBeforeSeason;
    #endregion


    public TvdbEpisode()
    {

    }

    #region specials

    /// <summary>
    /// if the episode is a special episode -> Before which season did
    /// it air
    /// </summary>
    public int AirsBeforeSeason
    {
      get { return m_airsBeforeSeason; }
      set { m_airsBeforeSeason = value; }
    }

    /// <summary>
    /// if the episode is a special episode -> Before which episode did
    /// it air
    /// </summary>
    public int AirsBeforeEpisode
    {
      get { return m_airsBeforeEpisode; }
      set { m_airsBeforeEpisode = value; }
    }

    /// <summary>
    /// if the episode is a special episode -> After which season did
    /// it air
    /// </summary>
    public int AirsAfterSeason
    {
      get { return m_airsAfterSeason; }
      set { m_airsAfterSeason = value; }
    }

    /// <summary>
    /// Is the episode a special episode
    /// 
    /// The fields airsafter_season, airsbefore_episode, and airsbefore_season will only be included when the episode is listed as a special. Specials are also listed as being in season 0, so they're easy to identify and sort.
    /// </summary>
    public bool IsSpecial
    {
      get
      {
        return (this.SeasonNumber == 0);
      }
    }

    #endregion

    #region DVD
    /// <summary>
    /// Which DVD season is this episode
    /// </summary>
    public int DvdSeason
    {
      get { return m_dvdSeason; }
      set { m_dvdSeason = value; }
    }

    /// <summary>
    /// The Dvd Episode Number
    /// </summary>
    public int DvdEpisodeNumber
    {
      get { return m_dvdEpisodeNumber; }
      set { m_dvdEpisodeNumber = value; }
    }

    /// <summary>
    /// The DVD Disc Id
    /// </summary>
    public int DvdDiscId
    {
      get { return m_dvdDiscId; }
      set { m_dvdDiscId = value; }
    }

    /// <summary>
    /// The chapter of this episode on the dvd
    /// </summary>
    public int DvdChapter
    {
      get { return m_dvdChapter; }
      set { m_dvdChapter = value; }
    }
    #endregion

    #region other tvdb information

    /// <summary>
    /// unique tvdb Id of this episode
    /// </summary>
    public int Id
    {
      get { return m_id; }
      set { m_id = value; }
    }

    /// <summary>
    /// Id of series this episode belongs to
    /// </summary>
    public int SeriesId
    {
      get { return m_seriesId; }
      set { m_seriesId = value; }
    }

    /// <summary>
    /// Id of season this episode belong to
    /// </summary>
    public int SeasonId
    {
      get { return m_seasonId; }
      set { m_seasonId = value; }
    }

    /// <summary>
    /// When was the episode last updated
    /// </summary>
    public DateTime LastUpdated
    {
      get { return m_lastUpdated; }
      set { m_lastUpdated = value; }
    }

    public String BannerPath
    {
      get { return m_bannerPath; }
      set { m_bannerPath = value; }
    }


    /// <summary>
    /// The absolute number of the episode
    /// </summary>
    public int AbsoluteNumber
    {
      get { return m_absoluteNumber; }
      set { m_absoluteNumber = value; }
    }

    /// <summary>
    /// List of writers for this episode
    /// </summary>
    public List<String> Writer
    {
      get { return m_writer; }
      set { m_writer = value; }
    }

    /// <summary>
    /// Season number of this episode
    /// </summary>
    public int SeasonNumber
    {
      get { return m_seasonNumber; }
      set { m_seasonNumber = value; }
    }

    /// <summary>
    /// Rating for this episode
    /// </summary>
    public double Rating
    {
      get { return m_rating; }
      set { m_rating = value; }
    }

    /// <summary>
    /// Production code for this episode
    /// </summary>
    public String ProductionCode
    {
      get { return m_productionCode; }
      set { m_productionCode = value; }
    }

    /// <summary>
    /// Overview of this episode
    /// </summary>
    public String Overview
    {
      get { return m_overview; }
      set { m_overview = value; }
    }

    /// <summary>
    /// Language of this episode
    /// </summary>
    public TvdbLanguage Language
    {
      get { return m_language; }
      set { m_language = value; }
    }

    /// <summary>
    /// Imdb number of this episode
    /// </summary>
    public String ImdbId
    {
      get { return m_imdbId; }
      set { m_imdbId = value; }
    }

    /// <summary>
    /// List of guest stars that appeared in this episode
    /// </summary>
    public List<String> GuestStars
    {
      get { return m_guestStars; }
      set { m_guestStars = value; }
    }

    /// <summary>
    /// When did the episode air first
    /// </summary>
    public DateTime FirstAired
    {
      get { return m_firstAired; }
      set { m_firstAired = value; }
    }

    /// <summary>
    /// Episode number
    /// </summary>
    public int EpisodeNumber
    {
      get { return m_episodeNumber; }
      set { m_episodeNumber = value; }
    }

    /// <summary>
    /// Name of the episode
    /// </summary>
    public String EpisodeName
    {
      get { return m_episodeName; }
      set { m_episodeName = value; }
    }

    /// <summary>
    /// List of directors for this episode
    /// </summary>
    public List<String> Directors
    {
      get { return m_directors; }
      set { m_directors = value; }
    }

    /// <summary>
    /// n/a
    /// </summary>
    public int CombinedSeason
    {
      get { return m_combinedSeason; }
      set { m_combinedSeason = value; }
    }
    /// <summary>
    /// n/a
    /// </summary>
    public int CombinedEpisodeNumber
    {
      get { return m_combinedEpisodeNumber; }
      set { m_combinedEpisodeNumber = value; }
    }
    #endregion
   

    /// <summary>
    /// Formatted String of writers for this episode in the 
    /// format | writer1 | writer2 | writer3 |
    /// </summary>
    public String WriterString
    {
      get
      {
        if (Writer == null || Writer.Count == 0) return "";
        StringBuilder retString = new StringBuilder();
        retString.Append("|");
        foreach (String s in Writer)
        {
          retString.Append(s);
          retString.Append("|");
        }
        return retString.ToString();
      }
    }

    /// <summary>
    /// Formatted String of guest stars that appeared during this episode in the 
    /// format | gueststar1 | gueststar2 | gueststar3 |
    /// </summary>
    public String GuestStarsString
    {
      get
      {
        if (GuestStars == null || GuestStars.Count == 0) return "";
        StringBuilder retString = new StringBuilder();
        retString.Append("|");
        foreach (String s in GuestStars)
        {
          retString.Append(s);
          retString.Append("|");
        }
        return retString.ToString();
      }
    }

    

    /// <summary>
    /// Formatted String of directors of this episode in the 
    /// format | director1 | director2 | director3 |
    /// </summary>
    public String DirectorsString
    {
      get
      {
        if (Directors == null || Directors.Count == 0) return "";
        StringBuilder retString = new StringBuilder();
        retString.Append("|");
        foreach (String s in Directors)
        {
          retString.Append(s);
          retString.Append("|");
        }
        return retString.ToString();
      }
    }

    /// <summary>
    /// The episode image banner
    /// </summary>
    public TvdbEpisodeBanner Banner
    {
      get { return m_banner; }
      set { m_banner = value; }
    }

    /// <summary>
    /// Updates all information of this episode from the given
    /// episode...
    /// </summary>
    /// <param name="_episode">new episode</param>
    internal void UpdateEpisodeInfo(TvdbEpisode _episode)
    {
      this.LastUpdated = _episode.LastUpdated;
      this.Banner = _episode.Banner;
      this.AbsoluteNumber = _episode.AbsoluteNumber;
      this.CombinedEpisodeNumber = _episode.CombinedEpisodeNumber;
      this.CombinedSeason = _episode.CombinedSeason;
      this.Directors = _episode.Directors;
      this.DvdChapter = _episode.DvdChapter;
      this.DvdDiscId = _episode.DvdDiscId;
      this.DvdEpisodeNumber = _episode.DvdEpisodeNumber;
      this.DvdSeason = _episode.DvdSeason;
      this.EpisodeName = _episode.EpisodeName;
      this.EpisodeNumber = _episode.EpisodeNumber;
      this.FirstAired = _episode.FirstAired;
      this.GuestStars = _episode.GuestStars;
      this.ImdbId = _episode.ImdbId;
      this.Language = _episode.Language;
      this.Overview = _episode.Overview;
      this.ProductionCode = _episode.ProductionCode;
      this.Rating = _episode.Rating;
      this.SeasonId = _episode.SeasonId;
      this.SeasonNumber = _episode.SeasonNumber;
      this.Writer = _episode.Writer;
    }
  }
}
