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
    private TvdbEpisodeBanner m_bannerPath;
    private DateTime m_lastUpdated;
    private int m_seasonId;
    private int m_seriesId;
    private bool m_isSpecial;
    private int m_airsAfterSeason;
    private int m_airsBeforeEpisode;
    private int m_airsBeforeSeason;

    public int AirsBeforeSeason
    {
      get { return m_airsBeforeSeason; }
      set { m_airsBeforeSeason = value; }
    }

    public int AirsBeforeEpisode
    {
      get { return m_airsBeforeEpisode; }
      set { m_airsBeforeEpisode = value; }
    }

    public int AirsAfterSeason
    {
      get { return m_airsAfterSeason; }
      set { m_airsAfterSeason = value; }
    }

    public bool IsSpecial
    {
      get { return m_isSpecial; }
      set { m_isSpecial = value; }
    }


    public int DvdSeason
    {
      get { return m_dvdSeason; }
      set { m_dvdSeason = value; }
    }

    public int SeriesId
    {
      get { return m_seriesId; }
      set { m_seriesId = value; }
    }

    public int SeasonId
    {
      get { return m_seasonId; }
      set { m_seasonId = value; }
    }

    public DateTime LastUpdated
    {
      get { return m_lastUpdated; }
      set { m_lastUpdated = value; }
    }

    public TvdbEpisodeBanner Banner
    {
      get { return m_bannerPath; }
      set { m_bannerPath = value; }
    }

    public int AbsoluteNumber
    {
      get { return m_absoluteNumber; }
      set { m_absoluteNumber = value; }
    }

    public List<String> Writer
    {
      get { return m_writer; }
      set { m_writer = value; }
    }

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

    public int SeasonNumber
    {
      get { return m_seasonNumber; }
      set { m_seasonNumber = value; }
    }

    public double Rating
    {
      get { return m_rating; }
      set { m_rating = value; }
    }

    public String ProductionCode
    {
      get { return m_productionCode; }
      set { m_productionCode = value; }
    }

    public String Overview
    {
      get { return m_overview; }
      set { m_overview = value; }
    }

    public TvdbLanguage Language
    {
      get { return m_language; }
      set { m_language = value; }
    }

    public String ImdbId
    {
      get { return m_imdbId; }
      set { m_imdbId = value; }
    }

    public List<String> GuestStars
    {
      get { return m_guestStars; }
      set { m_guestStars = value; }
    }

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

    public DateTime FirstAired
    {
      get { return m_firstAired; }
      set { m_firstAired = value; }
    }

    public int EpisodeNumber
    {
      get { return m_episodeNumber; }
      set { m_episodeNumber = value; }
    }

    public String EpisodeName
    {
      get { return m_episodeName; }
      set { m_episodeName = value; }
    }

    public List<String> Directors
    {
      get { return m_directors; }
      set { m_directors = value; }
    }

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

    public int DvdEpisodeNumber
    {
      get { return m_dvdEpisodeNumber; }
      set { m_dvdEpisodeNumber = value; }
    }

    public int DvdDiscId
    {
      get { return m_dvdDiscId; }
      set { m_dvdDiscId = value; }
    }

    public int DvdChapter
    {
      get { return m_dvdChapter; }
      set { m_dvdChapter = value; }
    }



    public int CombinedSeason
    {
      get { return m_combinedSeason; }
      set { m_combinedSeason = value; }
    }
    public int CombinedEpisodeNumber
    {
      get { return m_combinedEpisodeNumber; }
      set { m_combinedEpisodeNumber = value; }
    }

    public int Id
    {
      get { return m_id; }
      set { m_id = value; }
    }

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
