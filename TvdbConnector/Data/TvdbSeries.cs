using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TvdbConnector.Data.Banner;

namespace TvdbConnector.Data
{
  [Serializable]
  public class TvdbSeries
  {
    #region tvdb properties
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
    #endregion

    public TvdbSeries()
    {
      m_episodes = new List<TvdbEpisode>();
      m_episodesLoaded = false;

      m_banners = new List<TvdbBanner>();
      m_bannersLoaded = false;
      m_tvdbActorsLoaded = false;
    }

    #region user properties
    private bool m_isFavorite;

    /// <summary>
    /// Is the series a favorite
    /// </summary>
    public bool IsFavorite
    {
      get { return m_isFavorite; }
      set { m_isFavorite = value; }
    }

    #endregion

    #region basic properties
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
    /// Returns the genre string in the format | genre1 | genre2 | genre3 |
    /// </summary>
    public String GenreString
    {
      get
      {
        if (Genre == null || Genre.Count == 0) return "";
        StringBuilder retString = new StringBuilder();
        retString.Append("|");
        foreach (String s in Genre)
        {
          retString.Append(s);
          retString.Append("|");
        }
        return retString.ToString();
      }
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

    /// <summary>
    /// Formatted String of actors that appear during this episode in the 
    /// format | actor1 | actor2 | actor3 |
    /// </summary>
    public String ActorsString
    {
      get
      {
        if (Actors == null || Actors.Count == 0) return "";
        StringBuilder retString = new StringBuilder();
        retString.Append("|");
        foreach (String s in Actors)
        {
          retString.Append(s);
          retString.Append("|");
        }
        return retString.ToString();
      }
    }

    #endregion

    #region banners

    //all banners
    private List<TvdbBanner> m_banners;
    private bool m_bannersLoaded;

    /// <summary>
    /// returns a list of all banners for this series
    /// </summary>
    public List<TvdbBanner> Banners
    {
      get { return m_banners; }
      set
      {
        m_banners = value;
        m_bannersLoaded = true;
      }
    }

    /// <summary>
    /// Is the banner info loaded
    /// </summary>
    public bool BannersLoaded
    {
      get { return m_bannersLoaded; }
      set { m_bannersLoaded = value; }
    }

    /// <summary>
    /// returns a list of all series banners for this series
    /// </summary>
    public List<TvdbSeriesBanner> SeriesBanners
    {
      get
      {
        List<TvdbSeriesBanner> retList = new List<TvdbSeriesBanner>();
        foreach (TvdbBanner b in Banners)
        {
          if (b.GetType() == typeof(TvdbSeriesBanner))
          {
            retList.Add((TvdbSeriesBanner)b);
          }
        }
        return retList;
      }
    }

    /// <summary>
    /// Returns a list of all season banners for this series
    /// </summary>
    public List<TvdbSeasonBanner> SeasonBanners
    {
      get
      {
        List<TvdbSeasonBanner> retList = new List<TvdbSeasonBanner>();
        foreach (TvdbBanner b in Banners)
        {
          if (b.GetType() == typeof(TvdbSeasonBanner))
          {
            retList.Add((TvdbSeasonBanner)b);
          }
        }
        return retList;
      }
    }

    /// <summary>
    /// Returns a list of all season banners for this series
    /// </summary>
    public List<TvdbPosterBanner> PosterBanners
    {
      get
      {
        List<TvdbPosterBanner> retList = new List<TvdbPosterBanner>();
        foreach (TvdbBanner b in Banners)
        {
          if (b.GetType() == typeof(TvdbPosterBanner))
          {
            retList.Add((TvdbPosterBanner)b);
          }
        }
        return retList;
      }
    }

    /// <summary>
    /// Returns a list of all fanart banners for this series
    /// </summary>
    public List<TvdbFanartBanner> FanartBanners
    {
      get
      {
        List<TvdbFanartBanner> retList = new List<TvdbFanartBanner>();
        foreach (TvdbBanner b in Banners)
        {
          if (b.GetType() == typeof(TvdbFanartBanner))
          {
            retList.Add((TvdbFanartBanner)b);
          }
        }
        return retList;
      }
    }

    #endregion

    #region episodes
    //Episode information
    private List<TvdbEpisode> m_episodes;
    private bool m_episodesLoaded;

    /// <summary>
    /// Is the episode info loaded
    /// </summary>
    public bool EpisodesLoaded
    {
      get { return m_episodesLoaded; }
      set { m_episodesLoaded = value; }
    }

    /// <summary>
    /// List of Loaded episodes
    /// </summary>
    public List<TvdbEpisode> Episodes
    {
      get { return m_episodes; }
      set
      {
        m_episodes = value;
        m_episodesLoaded = true;
      }
    }
    #endregion

    #region Actors
    //Actor Information
    private List<TvdbActor> m_tvdbActors;
    private bool m_tvdbActorsLoaded;

    /// <summary>
    /// List of loaded tvdb actors
    /// </summary>
    public List<TvdbActor> TvdbActors
    {
      get { return m_tvdbActors; }
      set {
        m_tvdbActorsLoaded = true;
        m_tvdbActors = value; 
      }
    }
    
    /// <summary>
    /// Is the actor info loaded
    /// </summary>
    public bool TvdbActorsLoaded
    {
      get { return m_tvdbActorsLoaded; }
      set { m_tvdbActorsLoaded = value; }
    }
    #endregion

    /// <summary>
    /// returns SeriesName (SeriesId)
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      return SeriesName + "(" + Id + ")";
    }


    /// <summary>
    /// Uptdate the info of the current series with the updated one
    /// </summary>
    /// <param name="_series"></param>
    public void UpdateSeriesInfo(TvdbSeries _series)
    {
      this.Actors = _series.Actors;
      this.AirsDayOfWeek = _series.AirsDayOfWeek;
      this.AirsTime = _series.AirsTime;
      this.BannerPath = _series.BannerPath;
      this.Banners = _series.Banners;
      this.ContentRating = _series.ContentRating;
      this.FanartPath = _series.FanartPath;
      this.FirstAired = _series.FirstAired;
      this.Genre = _series.Genre;
      this.Id = _series.Id;
      this.ImdbId = _series.ImdbId;
      this.Language = _series.Language;
      this.LastUpdated = _series.LastUpdated;
      this.Network = _series.Network;
      this.Overview = _series.Overview;
      this.Rating = _series.Rating;
      this.Runtime = _series.Runtime;
      this.SeriesName = _series.SeriesName;
      this.Status = _series.Status;
      this.TvDotComId = _series.TvDotComId;
      this.Zap2itId = _series.Zap2itId;

      if (_series.EpisodesLoaded)
      {
        this.Episodes = _series.Episodes;
      }
    }
  }
}
