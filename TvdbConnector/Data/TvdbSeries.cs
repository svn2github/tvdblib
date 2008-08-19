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

    //Episode information
    private List<TvdbEpisode> m_episodes;
    private bool m_episodesLoaded;

    //all banners
    private List<TvdbBanner> m_banners;
    private bool m_bannersLoaded;

    public TvdbSeries()
    {
      m_episodes = new List<TvdbEpisode>();
      m_episodesLoaded = false;

      m_banners = new List<TvdbBanner>();
      m_bannersLoaded = false;
    }

    #region user properties
    private bool m_isFavorite;

    public bool IsFavorite
    {
      get { return m_isFavorite; }
      set { m_isFavorite = value; }
    }

    #endregion

    #region basic properties
    public int Id
    {
      get { return m_id; }
      set { m_id = value; }
    }

    public String SeriesName
    {
      get { return m_seriesName; }
      set { m_seriesName = value; }
    }

    public String Network
    {
      get { return m_network; }
      set { m_network = value; }
    }

    public TvdbLanguage Language
    {
      get { return m_language; }
      set { m_language = value; }
    }
    public string ContentRating
    {
      get { return m_contentRating; }
      set { m_contentRating = value; }
    }

    public String Zap2itId
    {
      get { return m_zap2itId; }
      set { m_zap2itId = value; }
    }

    public DateTime LastUpdated
    {
      get { return m_lastUpdated; }
      set { m_lastUpdated = value; }
    }

    public String FanartPath
    {
      get { return m_fanartPath; }
      set { m_fanartPath = value; }
    }

    public String BannerPath
    {
      get { return m_bannerPath; }
      set { m_bannerPath = value; }
    }

    public String Status
    {
      get { return m_status; }
      set { m_status = value; }
    }

    public int TvDotComId
    {
      get { return m_tvDotComId; }
      set { m_tvDotComId = value; }
    }

    public double Runtime
    {
      get { return m_runtime; }
      set { m_runtime = value; }
    }

    public double Rating
    {
      get { return m_rating; }
      set { m_rating = value; }
    }

    public String Overview
    {
      get { return m_overview; }
      set { m_overview = value; }
    }

    public String ImdbId
    {
      get { return m_imdbId; }
      set { m_imdbId = value; }
    }

    public List<String> Genre
    {
      get { return m_genre; }
      set { m_genre = value; }
    }

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

    public DateTime FirstAired
    {
      get { return m_firstAired; }
      set { m_firstAired = value; }
    }

    public DateTime AirsTime
    {
      get { return m_airsTime; }
      set { m_airsTime = value; }
    }

    public DayOfWeek? AirsDayOfWeek
    {
      get { return m_airsDayOfWeek; }
      set { m_airsDayOfWeek = value; }
    }

    public List<String> Actors
    {
      get { return m_actors; }
      set { m_actors = value; }
    }
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
    public bool EpisodesLoaded
    {
      get { return m_episodesLoaded; }
      set { m_episodesLoaded = value; }
    }

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

    public override string ToString()
    {
      return SeriesName + "(" + Id + ")";
    }

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
