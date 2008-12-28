/*
 *   TvdbLib: A library to retrieve information and media from http://thetvdb.com
 * 
 *   Copyright (C) 2008  Benjamin Gmeiner
 * 
 *   This program is free software: you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation, either version 3 of the License, or
 *   (at your option) any later version.
 *
 *   This program is distributed in the hope that it will be useful,
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *   GNU General Public License for more details.
 *
 *   You should have received a copy of the GNU General Public License
 *   along with this program.  If not, see <http://www.gnu.org/licenses/>.
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TvdbLib.Data;
using System.Net;
using TvdbLib.Cache;
using TvdbLib.Data.Banner;
using TvdbLib.Xml;
using System.Diagnostics;
using TvdbLib.Exceptions;

namespace TvdbLib
{
  /// <summary>
  /// Tvdb Handler for handling all features that are available on http://thetvdb.com/
  /// 
  /// http://thetvdb.com/ is an open database that can be modified by anybody. All content and images on the site have been contributed by our users. The database schema and website are open source under the GPL, and are available at Sourceforge.
  /// The site also has a full XML API that allows other software and websites to use this information. The API is currently being used by the myTV add-in for Windows Media Center, XBMC (formerly XBox Media Center); the meeTVshows and TVNight plugins for Meedio; the MP-TVSeries plugin for MediaPortal; Boxee; and many more.
  /// </summary>
  public class Tvdb
  {
    #region private fields
    private List<TvdbMirror> m_mirrorInfo;
    private ICacheProvider m_cacheProvider;
    private String m_apiKey;
    private TvdbUser m_userInfo;
    private TvdbDownloader m_downloader;
    private TvdbData m_loadedData;
    #endregion

    #region events

    public class UpdateProgressEventArgs : EventArgs
    {
      public UpdateProgressEventArgs(UpdateStage _currentUpdateStage, String _currentUpdateDescription,
                                     int _currentStageProgress, int _overallProgress)
      {
        CurrentUpdateStage = _currentUpdateStage;
        CurrentUpdateDescription = _currentUpdateDescription;
        CurrentStageProgress = _currentStageProgress;
        OverallProgress = _overallProgress;
      }

      public enum UpdateStage { downloading = 0, seriesupdate = 1, episodesupdate = 2, bannerupdate = 3};
      public UpdateStage CurrentUpdateStage { get; set; }
      public String CurrentUpdateDescription { get; set; }
      public int CurrentStageProgress { get; set; }
      public int OverallProgress { get; set; }
    }

    public class UpdateFinishedEventArgs : EventArgs
    {
      public UpdateFinishedEventArgs(DateTime _started, DateTime _ended, List<int> _updatedSeries,
                                     List<int> _updatedEpisodes, List<int> _updatedBanners)
      {
        UpdateStarted = _started;
        UpdateFinished = _ended;
        UpdatedSeries = _updatedSeries;
        UpdatedEpisodes = _updatedEpisodes;
        UpdatedBanners = _updatedBanners;
      }
      public DateTime UpdateStarted { get; set; }
      public DateTime UpdateFinished { get; set; }
      public List<int> UpdatedSeries { get; set; }
      public List<int> UpdatedEpisodes { get; set; }
      public List<int> UpdatedBanners { get; set; }
    }

    public delegate void UpdateProgressDelegate(UpdateProgressEventArgs _event);
    public event UpdateProgressDelegate UpdateProgressed;

    public delegate void UpdateFinishedDelegate(UpdateFinishedEventArgs _event);
    public event UpdateFinishedDelegate UpdateFinished;
    #endregion

    /// <summary>
    /// Update interval
    /// </summary>
    public enum Interval { day = 0, week = 1, month = 2, automatic = 3 };

    /// <summary>
    /// UserInfo for this class
    /// </summary>
    public TvdbUser UserInfo
    {
      get { return m_userInfo; }
      set
      {
        m_userInfo = value;
        if (m_cacheProvider != null)
        {
          //try to load the userinfo from cache
          TvdbUser user = m_cacheProvider.LoadUserInfoFromCache(value.UserIdentifier);
          if (user != null)
          {
            m_userInfo.UserFavorites = user.UserFavorites;
            m_userInfo.UserPreferredLanguage = user.UserPreferredLanguage;
            m_userInfo.UserName = user.UserName;
          }
        }
      }
    }

    /// <summary>
    /// Unique id for every project that is using thetvdb
    /// 
    /// More information on: http://thetvdb.com/wiki/index.php/Programmers_API
    /// </summary>
    public String ApiKey
    {
      get { return m_apiKey; }
    }

    public Tvdb(String _apiKey)
    {
      m_apiKey = _apiKey; //store api key
      m_loadedData = new TvdbData();
      m_loadedData.Mirrors = new List<TvdbMirror>();
      m_loadedData.SeriesList = new List<TvdbSeries>();
      m_loadedData.LanguageList = new List<TvdbLanguage>();
      TvdbLinks.ActiveMirror = new TvdbMirror(0, new Uri(TvdbLinks.BASE_SERVER), 7);
      m_downloader = new TvdbDownloader(m_apiKey);
      m_cacheProvider = null;
    }

    /// <summary>
    /// Constructor for a tvdb class
    /// </summary>
    /// <param name="_cacheProvider">The cache provider used to store the information</param>
    /// <param name="_apiKey">Api key to use for this project</param>
    public Tvdb(ICacheProvider _cacheProvider, String _apiKey)
      : this(_apiKey)
    {
      m_cacheProvider = _cacheProvider; //store given cache provider
    }

    /// <summary>
    /// Load previously stored information on (except series information) from cache
    /// </summary>
    /// <returns>true if cache could be loaded successfully, false otherwise</returns>
    public bool InitCache()
    {
      if (m_cacheProvider != null)
      {
        TvdbData cache = m_cacheProvider.LoadUserDataFromCache(); //try to load cache
        if (cache != null)
        {
          if (cache.LanguageList != null)
          {
            m_loadedData.LanguageList = cache.LanguageList;
          }
          else if (m_loadedData.LanguageList == null)
          {
            m_loadedData.LanguageList = new List<TvdbLanguage>();
          }

          if (cache.Mirrors != null && cache.Mirrors.Count > 0)
          {
            m_loadedData.Mirrors = cache.Mirrors;
          }
          else
          {
            m_loadedData.Mirrors = new List<TvdbMirror>();
            TvdbLinks.ActiveMirror = new TvdbMirror(0, new Uri(TvdbLinks.BASE_SERVER), 7);
          }
          m_loadedData.LastUpdated = cache.LastUpdated;
          return true;
        }
      }
      return false;
    }

    /// <summary>
    /// Search for a seris on tvdb using the name of the series
    /// </summary>
    /// <param name="_name">Name of series</param>
    /// <returns>List of possible hits (containing only very basic information (id, name,....) TODO: finish</returns>
    public List<TvdbSearchResult> SearchSeries(String _name)
    {
      List<TvdbSearchResult> retSeries = m_downloader.DownloadSearchResults(_name);

      return retSeries;
    }

    /// <summary>
    /// Gets the series with the given id either from cache (if it has already been loaded) or from 
    /// the selected tvdb mirror.
    /// 
    /// To check if this series has already been cached, use the Method IsCached(TvdbSeries _series)
    /// </summary>
    /// <exception cref="TvdbNotAvailableException">Tvdb is not available</exception>
    /// <exception cref="TvdbInvalidApiKeyException">The given api key is not valid</exception>
    /// <param name="_seriesId">id of series</param>
    /// <param name="_language">language that should be retrieved</param>
    /// <param name="_loadEpisodes">if true, the full series record will be loaded (series + all episodes), otherwise only the base record will be loaded which contains only series information</param>
    /// <param name="_loadActors">if true also loads the extended actor information</param>
    /// <param name="_loadBanners">if true also loads the paths to the banners</param>
    /// <returns>Instance of TvdbSeries containing all gained information</returns>
    public TvdbSeries GetSeries(int _seriesId, TvdbLanguage _language, bool _loadEpisodes,
                                bool _loadActors, bool _loadBanners)
    {
      return GetSeries(_seriesId, _language, _loadEpisodes, _loadActors, _loadBanners, false);
    }

    /// <summary>
    /// Gets the series with the given id either from cache (if it has already been loaded) or from 
    /// the selected tvdb mirror. If this series is not already cached and the series has to be 
    /// downloaded, the zipped version will be downloaded
    /// 
    /// To check if this series has already been cached, use the Method IsCached(TvdbSeries _series)
    /// </summary>
    /// <exception cref="TvdbNotAvailableException">Tvdb is not available</exception>
    /// <exception cref="TvdbInvalidApiKeyException">The given api key is not valid</exception>
    /// <param name="_seriesId">id of series</param>
    /// <param name="_language">language that should be retrieved</param>
    /// <returns>Instance of TvdbSeries containing all gained information</returns>
    internal TvdbSeries GetSeriesZipped(int _seriesId, TvdbLanguage _language)
    {
      return GetSeries(_seriesId, _language, true, true, true, true);
    }

    /// <summary>
    /// Gets the series with the given id either from cache (if it has already been loaded) or from 
    /// the selected tvdb mirror. If you use zip the request automatically downloads the episodes, the actors and the banners, so you should also select those features.
    /// 
    /// To check if this series has already been cached, use the Method IsCached(TvdbSeries _series)
    /// </summary>
    /// <exception cref="TvdbNotAvailableException">Tvdb is not available</exception>
    /// <exception cref="TvdbInvalidApiKeyException">The given api key is not valid</exception>
    /// <param name="_seriesId">id of series</param>
    /// <param name="_language">language that should be retrieved</param>
    /// <param name="_loadEpisodes">if true, the full series record will be loaded (series + all episodes), otherwise only the base record will be loaded which contains only series information</param>
    /// <param name="_loadBanners">if true also loads the paths to the banners</param>
    /// <param name="_loadActors">if true also loads the extended actor information</param>
    /// <param name="_useZip">If this series is not already cached and the series has to be downloaded, the zipped version will be downloaded</param>
    /// <returns>Instance of TvdbSeries containing all gained information</returns>
    public TvdbSeries GetSeries(int _seriesId, TvdbLanguage _language, bool _loadEpisodes,
                                bool _loadActors, bool _loadBanners, bool _useZip)
    {
      Stopwatch watch = new Stopwatch();
      watch.Start();
      TvdbSeries series = GetSeriesFromCache(_seriesId);

      if (series == null || //series not yet cached
          (_useZip && (!series.EpisodesLoaded && !series.TvdbActorsLoaded && !series.BannersLoaded)))//only the basic series info has been loaded -> zip is still faster than fetching the missing informations without using zip
      {//load complete series from tvdb
        if (_useZip)
        {
          series = m_downloader.DownloadSeriesZipped(_seriesId, _language);
        }
        else
        {
          series = m_downloader.DownloadSeries(_seriesId, _language, _loadEpisodes, _loadActors, _loadBanners);
        }

        if (series == null)
        {
          return null;
        }
        watch.Stop();
        Log.Info("Loaded series in " + watch.ElapsedMilliseconds + " milliseconds");
        series.IsFavorite = m_userInfo == null ? false : CheckIfSeriesFavorite(_seriesId, m_userInfo.UserFavorites);
        AddSeriesToCache(series);
        return series;
      }
      else
      {//some (if not all) information has already been loaded from tvdb at some point -> fill the missing details and return the series

        if (!_language.Abbriviation.Equals(series.Language.Abbriviation))
        {//user wants a different language than the one that has been loaded
          if (series.GetAvailableLanguages().Contains(_language))
          {
            series.SetLanguage(_language);
          }
          else
          {
            TvdbSeriesFields newFields = m_downloader.DownloadSeriesFields(_seriesId, _language);
            if (_loadEpisodes)
            {
              List<TvdbEpisode> epList = m_downloader.DownloadEpisodes(_seriesId, _language);
              if (epList != null)
              {
                newFields.Episodes = epList;
              }
            }
            if (newFields != null)
            {
              series.AddLanguage(newFields);
              series.SetLanguage(_language);
            }
            else
            {
              Log.Warn("Couldn't load new language " + _language.Abbriviation + " for series " + _seriesId);
              return null;
            }
          }
        }

        if (_loadActors && !series.TvdbActorsLoaded)
        {//user wants actors loaded
          List<TvdbActor> actorList = m_downloader.DownloadActors(_seriesId);
          if (actorList != null)
          {
            series.TvdbActorsLoaded = true;
            series.TvdbActors = actorList;
          }
        }

        if (_loadEpisodes && !series.EpisodesLoaded)
        {//user wants the full version but only the basic has been loaded (without episodes
          List<TvdbEpisode> epList = m_downloader.DownloadEpisodes(_seriesId, _language);
          if (epList != null)
          {
            series.EpisodesLoaded = true;
            series.Episodes = epList;
          }
        }

        if (_loadBanners && !series.BannersLoaded)
        {//user wants banners loaded but current series hasn't -> Do it baby
          List<TvdbBanner> bannerList = m_downloader.DownloadBanners(_seriesId);
          if (bannerList != null)
          {
            series.BannersLoaded = true;
            series.Banners = bannerList;
          }
        }

        watch.Stop();
        Log.Info("Loaded series in " + watch.ElapsedMilliseconds + " milliseconds");

        return series;
      }
    }


    /// <summary>
    /// Gets the full series (including episode information and actors) with the given id either from cache 
    /// (if it has already been loaded) or from the selected tvdb mirror.
    /// 
    /// To check if this series has already been cached, pleas use the Method IsCached(TvdbSeries _series)
    /// </summary>
    /// <exception cref="TvdbNotAvailableException">Tvdb is not available</exception>
    /// <exception cref="TvdbInvalidApiKeyException">The given api key is not valid</exception>
    /// <param name="_seriesId">id of series</param>
    /// <param name="_language">language that should be retrieved</param>
    /// <param name="_loadBanners">if true also loads the paths to the banners</param>
    /// <returns>Instance of TvdbSeries containing all gained information</returns>
    public TvdbSeries GetFullSeries(int _seriesId, TvdbLanguage _language, bool _loadBanners)
    {
      return GetSeries(_seriesId, _language, true, true, _loadBanners);
    }

    /// <summary>
    /// Gets the basic series (without episode information and actors) with the given id either from cache 
    /// (if it has already been loaded) or from the selected tvdb mirror.
    /// 
    /// To check if this series has already been cached, please use the Method IsCached(TvdbSeries _series)
    /// </summary>
    /// <exception cref="TvdbNotAvailableException">Tvdb is not available</exception>
    /// <exception cref="TvdbInvalidApiKeyException">The given api key is not valid</exception>
    /// <param name="_seriesId">id of series</param>
    /// <param name="_language">language that should be retrieved</param>
    /// <param name="_loadBanners">if true also loads the paths to the banners</param>
    /// <returns>Instance of TvdbSeries containing all gained information</returns>
    public TvdbSeries GetBasicSeries(int _seriesId, TvdbLanguage _language, bool _loadBanners)
    {
      return GetSeries(_seriesId, _language, false, false, _loadBanners);
    }



    /// <summary>
    /// Returns if the series is locally cached
    /// </summary>
    /// <param name="_seriesId">Id of the series</param>
    /// <param name="_language">Language</param>
    /// <param name="_loadEpisodes">Load Episodes</param>
    /// <param name="_loadActors">Load Actors</param>
    /// <param name="_loadBanners">Load Banners</param>
    /// <returns>True if the series is cached in the given configuration</returns>
    public bool IsCached(int _seriesId, TvdbLanguage _language, bool _loadEpisodes,
                         bool _loadActors, bool _loadBanners)
    {
      foreach (TvdbSeries s in m_loadedData.SeriesList)
      {
        if (s.Id == _seriesId && s.Language.Abbriviation.Equals(_language.Abbriviation))
        {
          if ((s.BannersLoaded || !_loadActors) &&
             (s.TvdbActorsLoaded || !_loadActors) &&
             (s.EpisodesLoaded || !_loadEpisodes))
          {
            return true;
          }
        }
      }

      if (m_cacheProvider != null)
      {
        return m_cacheProvider.IsCached(_seriesId, _loadEpisodes, _loadBanners, _loadActors);
      }
      return false;
    }



    /// <summary>
    /// Retrieve the episode with the given id in the given language
    /// </summary>
    /// <param name="_episodeId">id of the episode</param>
    /// <param name="_language">languageof the episode</param>
    /// <returns>The retrieved episode</returns>
    public TvdbEpisode GetEpisode(int _episodeId, TvdbLanguage _language)
    {
      TvdbEpisode episode = GetEpisodeFromCache(_episodeId, _language);

      if (episode != null)
      {
        return episode;
      }
      else
      {
        episode = m_downloader.DownloadEpisode(_episodeId, _language);
        AddEpisodeToCache(episode);
        return episode;
      }
    }

    /// <summary>
    /// Retrieve the episode with the given parameters
    /// </summary>
    /// <param name="_seriesId">id of the series</param>
    /// <param name="_seasonNr">season number of the episode</param>
    /// <param name="_episodeNr">number of the episode</param>
    /// <param name="_language">language of the episode</param>
    /// <param name="_order">The sorting order that should be user when downloading the episode</param>
    /// <returns>The retrieved episode</returns>
    public TvdbEpisode GetEpisode(int _seriesId, int _seasonNr, int _episodeNr,
                                  TvdbEpisode.EpisodeOrdering _order, TvdbLanguage _language)
    {
      String order = null;
      switch (_order)
      {
        case TvdbEpisode.EpisodeOrdering.AbsoluteOrder:
          order = "absolut";
          break;
        case TvdbEpisode.EpisodeOrdering.DefaultOrder:
          order = "default";
          break;
        case TvdbEpisode.EpisodeOrdering.DvdOrder:
          order = "dvd";
          break;
      }

      TvdbEpisode episode = m_downloader.DownloadEpisode(_seriesId, _seasonNr, _episodeNr, order, _language);
      return episode;
    }

    /// <summary>
    /// Retrieve the episode with the given parameters
    /// </summary>
    /// <param name="_seriesId">id of the series</param>
    /// <param name="_airDate">When did the episode air</param>
    /// <param name="_language">language of the episode</param>
    /// <exception cref="TvdbInvalidApiKeyException">The given api key is not valid</exception>
    /// <returns>The retrieved episode</returns>
    public TvdbEpisode GetEpisode(int _seriesId, DateTime _airDate, TvdbLanguage _language)
    {
      TvdbEpisode ep = m_downloader.DownloadEpisode(_seriesId, _airDate, _language);
      return ep;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_seriesList"></param>
    private void AddSeriesToCache(List<TvdbSeries> _seriesList)
    {
      //Add Series to Cache if not existant, overwrite existing entry otherwise
      foreach (TvdbSeries s in _seriesList)
      {
        AddSeriesToCache(s);

      }
    }

    private void AddSeriesToCache(TvdbSeries _series)
    {
      bool found = false;
      for (int i = 0; i < m_loadedData.SeriesList.Count; i++)
      {
        if (((TvdbSeries)m_loadedData.SeriesList[i]).Id == _series.Id)
        {
          found = true;
          m_loadedData.SeriesList[i].UpdateSeriesInfo(_series);//so we're not losing banners, etc.
          //_series.UpdateSeriesInfo(m_loadedData.SeriesList[i]); //so we're not losing banners, etc.
          //m_loadedData.SeriesList[i] = _series;
        }
      }
      if (!found)
      {
        m_loadedData.SeriesList.Add(_series);
      }
    }

    /// <summary>
    /// Add this episode to the proper cached series
    /// </summary>
    /// <param name="_episode">Episode to add to cache</param>
    private void AddEpisodeToCache(TvdbEpisode _episode)
    {
      bool seriesFound = false;
      foreach (TvdbSeries s in m_loadedData.SeriesList)
      {
        if (s.Id == _episode.SeriesId)
        {//series for ep found
          seriesFound = true;
          Util.AddEpisodeToSeries(_episode, s);
          break;
        }

      }

      //todo: maybe skip this since downloading an episode is no biggie
      if (!seriesFound)
      {//the series doesn't exist yet -> add episode to dummy series
        TvdbSeries newSeries = new TvdbSeries();
        newSeries.LastUpdated = new DateTime(1, 1, 1);
        newSeries.Episodes.Add(_episode);
        m_loadedData.SeriesList.Add(newSeries);
      }
    }


    /// <summary>
    /// Get the series from cache
    /// </summary>
    /// <param name="_seriesId">Id of series</param>
    /// <returns></returns>
    private TvdbSeries GetSeriesFromCache(int _seriesId)
    {
      foreach (TvdbSeries s in m_loadedData.SeriesList)
      {
        if (s.Id == _seriesId)
        {
          return s;
        }
      }

      //try to retrieve the series from the cache provider
      try
      {
        TvdbSeries series = m_cacheProvider.LoadSeriesFromCache(_seriesId);
        if (series != null)
        {
          series.IsFavorite = m_userInfo == null ? false : CheckIfSeriesFavorite(series.Id, m_userInfo.UserFavorites);
          AddSeriesToCache(series);
        }
        return series;
      }
      catch (Exception)
      {
        return null;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_episodeId"></param>
    /// <param name="_language"></param>
    /// <returns></returns>
    private TvdbEpisode GetEpisodeFromCache(int _episodeId, TvdbLanguage _language)
    {
      foreach (TvdbSeries s in m_loadedData.SeriesList)
      {
        foreach (TvdbEpisode e in s.Episodes)
        {
          if (e.Id == _episodeId)
          {
            return e;
          }
        }
      }
      return null;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool UpdateTvdbMirrors()
    {
      List<TvdbMirror> list = m_downloader.DownloadMirrorList();
      if (list != null && list.Count > 0)
      {
        m_mirrorInfo = list;
        TvdbLinks.m_mirrorList = list;
        return true;
      }
      else
      {
        return false;
      }
    }

    ///<summary>
    /// Returns a list of available mirrors
    ///</summary>
    public List<TvdbMirror> MirrorList
    {
      get
      {
        return m_mirrorInfo;
      }

    }

    /// <summary>
    /// Is the information on available tvdb mirrors already cached
    /// </summary>
    public bool IsMirrorInformationCached
    {
      get
      {
        if (m_loadedData.Mirrors != null && m_loadedData.Mirrors.Count > 0)
        {
          return true;
        }
        else
        {
          return false;
        }
      }
    }

    /// <summary>
    /// Update all the series (not using zip) with the updated information
    /// </summary>
    /// <returns>true if the update was successful, false otherwise</returns>
    public bool UpdateAllSeries()
    {
      return UpdateAllSeries(false);
    }

    /// <summary>
    /// Update all the series with the updated information
    /// </summary>
    /// <param name="_zipped">download zipped file?</param>
    /// <returns>true if the update was successful, false otherwise</returns>
    public bool UpdateAllSeries(bool _zipped)
    {
      //MakeUpdate(Util.UpdateInterval.month);
      //return true;
      TimeSpan timespanLastUpdate = (DateTime.Now - m_loadedData.LastUpdated);
      //MakeUpdate(TvdbLinks.CreateUpdateLink(m_apiKey, TvdbLinks.UpdateInterval.day));
      if (timespanLastUpdate < new TimeSpan(1, 0, 0, 0))
      {//last update is less than a day ago -> make a daily update
        //MakeUpdate(TvdbLinks.CreateUpdateLink(m_apiKey, Util.UpdateInterval.day));
        MakeUpdate(Util.UpdateInterval.day, _zipped);
      }
      else if (timespanLastUpdate < new TimeSpan(7, 0, 0, 0))
      {//last update is less than a week ago -> make a weekly update
        //MakeUpdate(TvdbLinks.CreateUpdateLink(m_apiKey, Util.UpdateInterval.week));
        MakeUpdate(Util.UpdateInterval.week, _zipped);
      }
      else if (timespanLastUpdate < new TimeSpan(31, 0, 0, 0) ||
                m_loadedData.LastUpdated == new DateTime())//lastUpdated not available -> make longest possible upgrade
      {//last update is less than a month ago -> make a monthly update
        //MakeUpdate(TvdbLinks.CreateUpdateLink(m_apiKey, Util.UpdateInterval.month));
        MakeUpdate(Util.UpdateInterval.month, _zipped);
      }
      else
      {//todo: Make a full update -> full update deosn't make sense... (do a complete re-scan?)
        //MakeUpdate(TvdbLinks.CreateUpdateLink(m_apiKey, TvdbLinks.UpdateInterval.day));
      }

      return true;
    }

    public bool UpdateAllSeries(Interval _interval, bool _zipped)
    {
      switch (_interval)
      {
        case Interval.day:
          return MakeUpdate(Util.UpdateInterval.day, _zipped);
        case Interval.week:
          return MakeUpdate(Util.UpdateInterval.week, _zipped);
        case Interval.month:
          return MakeUpdate(Util.UpdateInterval.month, _zipped);
        case Interval.automatic:
          return UpdateAllSeries(_zipped);
        default:
          return false;
      }
    }

    private bool MakeUpdate(Util.UpdateInterval _interval, bool _zipped)
    {
      Log.Info("Started update (" + _interval.ToString() + ")");
      Stopwatch watch = new Stopwatch();
      watch.Start();
      DateTime startUpdate = DateTime.Now;
      if (UpdateProgressed != null)
      {//update has started, we're downloading the updated content from tvdb
        UpdateProgressed(new UpdateProgressEventArgs(UpdateProgressEventArgs.UpdateStage.downloading,
                                                     "Downloading " + (_zipped ? " zipped " : " unzipped") + " updated content",
                                                     0, 0));
      }

      //update all flagged series
      List<TvdbSeries> updateSeries;
      List<TvdbEpisode> updateEpisodes;
      List<TvdbBanner> updateBanners;
      DateTime updateTime = m_downloader.DownloadUpdate(out updateSeries, out updateEpisodes, out updateBanners, _interval, _zipped);
      List<int> cachedSeries = m_cacheProvider.GetCachedSeries();

      List<TvdbSeries> seriesToSave = new List<TvdbSeries>();

      if (UpdateProgressed != null)
      {//update has started, we're downloading the updated content from tvdb
        UpdateProgressed(new UpdateProgressEventArgs(UpdateProgressEventArgs.UpdateStage.seriesupdate,
                                                     "Begin updating series",
                                                     0, 25));
      }

      int countUpdatedSeries = updateSeries.Count;
      int countSeriesDone = 0;
      List<int> updatedSeries = new List<int>();
      List<int> updatedEpisodes = new List<int>();
      List<int> updatedBanners = new List<int>();
      foreach (TvdbSeries us in updateSeries)
      {
        foreach (TvdbSeries s in m_loadedData.SeriesList)
        {
          if (us.Id == s.Id)
          {
            if (s.LastUpdated < us.LastUpdated)
            {//changes occured in series
              if (UpdateProgressed != null)
              {//update has started, we're downloading the updated content from tvdb
                int currProg = 100 / countUpdatedSeries * countSeriesDone;
                UpdateProgressed(new UpdateProgressEventArgs(UpdateProgressEventArgs.UpdateStage.downloading,
                                                             "Updating series " + us.SeriesName,
                                                             currProg, 25 + (int)(currProg / 4)));
              }
              UpdateSeries(s, us.LastUpdated);

            }
            break;
          }
        }

        //Update series that have been already cached but are not in memory
        foreach (int s in cachedSeries)
        {
          if (us.Id == s)
          {//changes occured in series
            TvdbSeries series = m_cacheProvider.LoadSeriesFromCache(us.Id);
            if (series.LastUpdated < us.LastUpdated)
            {
              if (UpdateProgressed != null)
              {//update has started, we're downloading the updated content from tvdb
                int currProg = (int)(100.0 / countUpdatedSeries * countSeriesDone);
                UpdateProgressed(new UpdateProgressEventArgs(UpdateProgressEventArgs.UpdateStage.downloading,
                                                             "Updating series " + us.SeriesName,
                                                             currProg, 25 + (int)(currProg / 4)));
              }
              UpdateSeries(series, us.LastUpdated);
              AddSeriesToCache(series);
              seriesToSave.Add(series);
            }
            break;
          }
        }
        countSeriesDone++;
      }

      int countEpisodeUpdates = updateEpisodes.Count; ;
      int countEpisodesDone = 0;
      //update all flagged episodes
      foreach (TvdbEpisode ue in updateEpisodes)
      {
        foreach (TvdbSeries s in m_loadedData.SeriesList)
        {
          if (ue.SeriesId == s.Id)
          {
            if (UpdateProgressed != null)
            {//update has started, we're downloading the updated content from tvdb
              int currProg = (int)(100.0 / countEpisodeUpdates * countEpisodesDone);
              UpdateProgressed(new UpdateProgressEventArgs(UpdateProgressEventArgs.UpdateStage.downloading,
                                                           "Updating episode " + ue.SeriesId + " " + ue.SeasonNumber +
                                                           "x" + ue.EpisodeNumber + "(id: " + ue.Id + ")",
                                                           currProg, 50 + (int)(currProg / 4)));
            }
            UpdateEpisode(s, ue);
            break;
          }
        }

        foreach (int s in cachedSeries)
        {
          if (ue.SeriesId == s)
          {//changes occured in series
            if (UpdateProgressed != null)
            {//update has started, we're downloading the updated content from tvdb
              int currProg = (int)(100.0 /  countEpisodeUpdates * countEpisodesDone);
              UpdateProgressed(new UpdateProgressEventArgs(UpdateProgressEventArgs.UpdateStage.downloading,
                                                           "Updating episode " + ue.SeriesId + " " + ue.SeasonNumber +
                                                           "x" + ue.EpisodeNumber + "(id: " + ue.Id + ")",
                                                           currProg, 50 + (int)(currProg / 4)));
            }
            TvdbSeries series = m_cacheProvider.LoadSeriesFromCache(ue.SeriesId);
            UpdateEpisode(series, ue);
            break;
          }
        }
        countEpisodesDone++;
      }

      int countUpdatedBanner = updateBanners.Count;
      int countBannerDone = 0;
      //todo: update banner information here -> wait for forum response regarding missing banner id within updates
      foreach (TvdbBanner b in updateBanners)
      {
        foreach (TvdbSeries s in m_loadedData.SeriesList)
        {
          if (s.Id == b.SeriesId)
          {
            if (UpdateProgressed != null)
            {//update has started, we're downloading the updated content from tvdb
              int currProg = (int)(100.0 /  countUpdatedBanner * countBannerDone);
              UpdateProgressed(new UpdateProgressEventArgs(UpdateProgressEventArgs.UpdateStage.bannerupdate,
                                                           "Updating banner " + b.BannerPath + "(id=" + b.Id + ")",
                                                           currProg, 75 + (int)(currProg / 4)));
            }
            UpdateBanner(s, b);
            break;
          }
        }

        foreach (int s in cachedSeries)
        {
          if (b.SeriesId == s)
          {//changes occured in series
            if (UpdateProgressed != null)
            {//update has started, we're downloading the updated content from tvdb
              int currProg = (int)(100.0 /  countUpdatedBanner * countBannerDone);
              UpdateProgressed(new UpdateProgressEventArgs(UpdateProgressEventArgs.UpdateStage.bannerupdate,
                                                           "Updating banner " + b.BannerPath + "(id=" + b.Id + ")",
                                                           currProg, 75 + (int)(currProg / 4)));
            }
            TvdbSeries series = m_cacheProvider.LoadSeriesFromCache(s);
            UpdateBanner(series, b);
            break;
          }
        }
        countBannerDone++;
      }
      //set the last updated time to time of this update
      m_loadedData.LastUpdated = updateTime;
      watch.Stop();
      Log.Info("Finished update (" + _interval.ToString() + ") in " + watch.ElapsedMilliseconds + " milliseconds");

      if (UpdateFinished != null)
      {
        UpdateFinished(new UpdateFinishedEventArgs(startUpdate, DateTime.Now, null, null, null));
      }
      return true;

    }

    /// <summary>
    /// Update the series with the banner
    /// </summary>
    /// <param name="_series"></param>
    /// <param name="_banner"></param>
    private void UpdateBanner(TvdbSeries _series, TvdbBanner _banner)
    {
      if (!_series.BannersLoaded)
      {//banners for this series havn't been loaded -> don't update banners
        return;
      }
      bool found = false;
      foreach (TvdbBanner b in _series.Banners)
      {
        if (_banner.GetType() == b.GetType() && _banner.BannerPath.Equals(b.BannerPath))
        {
          if (b.LastUpdated < _banner.LastUpdated)
          {
            b.LastUpdated = _banner.LastUpdated;
            if (_banner.GetType() == typeof(TvdbFanartBanner))
            {
              TvdbFanartBanner fanart = (TvdbFanartBanner)b;

              fanart.Resolution = ((TvdbFanartBanner)_banner).Resolution;
              if (fanart.IsThumbLoaded)
              {
                fanart.LoadThumb(null);
              }

              if (fanart.IsVignetteLoaded)
              {
                fanart.LoadVignette(null);
              }
            }
            if (b.IsLoaded)
            {
              b.LoadBanner(null);
            }

            Log.Info("Replacing banner " + _banner.Id);
          }
          else
          {
            Log.Debug("Not replacing banner " + _banner.Id + " because it's not newer than current image");
          }
          found = true;
        }
      }
      if (!found)
      {//banner not found -> add it to bannerlist
        Log.Info("Adding banner " + _banner.Id);
        _series.Banners.Add(_banner);
      }
    }

    /// <summary>
    /// Update the series with the episode (Add it to the series if it doesn't already exist or update the episode if the current episode is older than the updated one)
    /// </summary>
    /// <param name="s"></param>
    /// <param name="ue"></param>
    private void UpdateEpisode(TvdbSeries s, TvdbEpisode ue)
    {
      List<TvdbEpisode> allEpList = new List<TvdbEpisode>();
      allEpList.AddRange(s.Episodes);
      foreach (TvdbLanguage l in s.GetAvailableLanguages())
      {
        if (s.SeriesTranslations[l].Episodes != null && s.SeriesTranslations[l].Language != s.Language)
        {
          allEpList.AddRange(s.SeriesTranslations[l].Episodes);
        }
      }

      //check all episodes if the updated episode is in it
      foreach (TvdbEpisode e in allEpList)
      {
        if (e.Id == ue.Id)
        {
          if (e.LastUpdated < ue.LastUpdated)
          {
            //download episode which has been updated
            TvdbEpisode newEpisode = m_downloader.DownloadEpisode(e.Id, e.Language);

            //update information of episode with new episodes informations
            if (newEpisode != null)
            {
              newEpisode.LastUpdated = ue.LastUpdated;

              e.UpdateEpisodeInfo(newEpisode);
              Log.Info("Updated Episode " + e.Id + " for series " + e.SeriesId);
            }
          }
          return;
        }
      }

      //episode hasn't been found
      foreach (TvdbLanguage l in s.GetAvailableLanguages())
      {
        //hasn't been found -> add it
        TvdbEpisode ep = m_downloader.DownloadEpisode(ue.Id, l);
        AddEpisodeToCache(ep);
        Log.Info("Added Episode " + ep.Id + " for series " + ep.SeriesId);
      }
    }



    /// <summary>
    /// Download the new series and update the information
    /// </summary>
    /// <param name="_series">Series to update</param>
    /// <param name="_lastUpdated">When was the last update made</param>
    private void UpdateSeries(TvdbSeries _series, DateTime _lastUpdated)
    {
      //get series info
      TvdbSeries newSeries = GetSeries(_series.Id, _series.Language, false, false, false);
      newSeries.LastUpdated = _lastUpdated;
      if (newSeries != null)
      {
        _series.UpdateSeriesInfo(newSeries);
        Log.Info("Updated Series " + _series.Id);
      }
    }


    /// <summary>
    /// Returns list of all available Languages on tvdb
    /// </summary>
    /// <returns>list of available languages</returns>
    public List<TvdbLanguage> Languages
    {
      get
      {
        if (IsLanguagesCached)
        {
          return m_loadedData.LanguageList;
        }
        else
        {
          List<TvdbLanguage> list = m_downloader.DownloadLanguages();
          if (list != null && list.Count > 0)
          {
            m_loadedData.LanguageList = list;
            return list;
          }
          else
          {
            return null;
          }
        }
      }
    }

    /// <summary>
    /// Reloads all language definitions from tvdb
    /// </summary>
    public bool ReloadLanguages()
    {
      List<TvdbLanguage> list = m_downloader.DownloadLanguages();
      if (list != null && list.Count > 0)
      {
        m_loadedData.LanguageList = list;
        return true;
      }
      else
      {
        return false;
      }
    }

    /// <summary>
    /// Are the language definitions already cached
    /// </summary>
    public bool IsLanguagesCached
    {
      get
      {
        return (m_loadedData.LanguageList != null && m_loadedData.LanguageList.Count > 0);
      }
    }

    /// <summary>
    /// Saves all retrieved data into persistant storage cache (depending on cache provider)
    /// </summary>
    public void SaveCache()
    {
      if (m_cacheProvider != null)
      {
        m_cacheProvider.SaveAllToCache(m_loadedData);
        if (m_userInfo != null) m_cacheProvider.SaveToCache(m_userInfo);
      }
    }


    /// <summary>
    /// Returns all series id's that are already cached in memory or locally via the cacheprovider
    /// </summary>
    /// <returns>List of loaded series</returns>
    public List<int> GetCachedSeries()
    {
      List<int> retList = new List<int>();

      //add series that are stored with the cacheprovider
      if (m_cacheProvider != null)
      {
        retList.AddRange(m_cacheProvider.GetCachedSeries());
      }

      //add series that are in memory
      foreach (TvdbSeries s in m_loadedData.SeriesList)
      {
        if (!retList.Contains(s.Id))
        {
          retList.Add(s.Id);
        }
      }

      return retList;
    }


    /// <summary>
    /// Forces a complete update of the series. All information that has already been loaded (including loaded images!) will be deleted and reloaded from tvdb -> if you only want to update the series, use the "MakeUpdate" method
    /// </summary>
    /// <param name="_series">Series to reload</param>
    public TvdbSeries ForceUpdate(TvdbSeries _series)
    {
      return ForceUpdate(_series, _series.EpisodesLoaded, _series.TvdbActorsLoaded, _series.BannersLoaded);


    }
    /// <summary>
    /// Forces a complete update of the series. All information that has already been loaded (including loaded images!) will be deleted and reloaded from tvdb -> if you only want to update the series, use the "MakeUpdate" method
    /// </summary>
    /// <param name="_series">Series to update</param>
    /// <param name="_loadEpisodes">Should episodes be loaded as well</param>
    /// <param name="_loadActors">Should actors be loaded as well</param>
    /// <param name="_loadBanners">Should banners be loaded as well</param>
    /// <returns></returns>
    public TvdbSeries ForceUpdate(TvdbSeries _series, bool _loadEpisodes,
                                bool _loadActors, bool _loadBanners)
    {
      for (int i = 0; i < m_loadedData.SeriesList.Count; i++)
      {
        if (((TvdbSeries)m_loadedData.SeriesList[i]).Id == _series.Id)
        {
          m_loadedData.SeriesList.Remove((TvdbSeries)m_loadedData.SeriesList[i]);
        }
      }

      _series = m_downloader.DownloadSeries(_series.Id, _series.Language, _loadEpisodes,
                                      _loadActors, _loadBanners);

      m_loadedData.SeriesList.Add(_series);


      return _series;
    }

    /// <summary>
    /// Gets the preferred language of the user
    /// 
    /// user information has to be set, otherwise TvdbUserNotFoundException is thrown
    /// </summary>
    /// <returns>preferred language of user</returns>
    public TvdbLanguage GetPreferredLanguage()
    {
      if (m_userInfo != null)
      {
        TvdbLanguage userLang = m_downloader.DownloadUserPreferredLanguage(m_userInfo.UserIdentifier);

        if (userLang != null)
        {
          //only one language is contained in the userlang file
          foreach (TvdbLanguage l in m_loadedData.LanguageList)
          {
            if (l.Abbriviation.Equals(userLang.Abbriviation)) return l;
          }
          return userLang;//couldn't find language -> return new instance
        }
        else
        {
          return null; //problem with parsing xml file
        }
      }
      else
      {
        throw new TvdbUserNotFoundException("You can't get the preferred language when no user is specified");
      }
    }

    #region user favorites and rating

    /// <summary>
    /// If the list of user favorites is retrieved, go through all loaded series and look if the series is a favorite
    /// </summary>
    /// <param name="_favs"></param>
    private void HandleUserFavoriteRetrieved(List<int> _favs)
    {
      m_userInfo.UserFavorites = _favs;
      foreach (TvdbSeries s in m_loadedData.SeriesList)
      {
        s.IsFavorite = CheckIfSeriesFavorite(s.Id, _favs);
      }
    }

    /// <summary>
    /// Check if series is in the list of favorites
    /// </summary>
    /// <param name="_series"></param>
    /// <param name="_favs"></param>
    /// <returns></returns>
    private bool CheckIfSeriesFavorite(int _series, List<int> _favs)
    {
      if (_favs == null) return false;
      foreach (int f in _favs)
      {
        if (_series == f)
        {//series is a favorite
          return true;
        }
      }
      return false;
    }

    /// <summary>
    /// Gets a list of IDs of the favorite series of the user
    /// </summary>
    /// <returns>id list of favorite series</returns>
    public List<int> GetUserFavouritesList()
    {
      if (m_userInfo != null)
      {
        List<int> userFavs = m_downloader.DownloadUserFavoriteList(m_userInfo.UserIdentifier);
        HandleUserFavoriteRetrieved(userFavs);
        return userFavs;
      }
      else
      {
        throw new Exception("You can't get the list of user favorites when no user is specified");
      }
    }

    /// <summary>
    /// Get the favorite series of the user (only basic series information will be loaded)
    /// </summary>
    /// <param name="_lang">Which language should be used</param>
    /// <returns>List of favorite series</returns>
    public List<TvdbSeries> GetUserFavorites(TvdbLanguage _lang)
    {
      if (m_userInfo != null)
      {
        if (_lang != null)
        {
          List<int> idList = GetUserFavouritesList();
          List<TvdbSeries> retList = new List<TvdbSeries>();

          foreach (int sId in idList)
          {
            if (IsCached(sId, _lang, false, false, false))
            {
              retList.Add(GetSeriesFromCache(sId));
            }
            else
            {
              TvdbSeries series = m_downloader.DownloadSeries(sId, _lang, false, false, false);
              if (series != null)
              {
                retList.Add(series);
              }
              AddSeriesToCache(series);
            }
          }
          HandleUserFavoriteRetrieved(idList);
          return retList;
        }
        else
        {
          throw new Exception("you have to define a language");
        }
      }
      else
      {
        throw new TvdbUserNotFoundException("You can't get the favourites when no user is defined");
      }
    }

    /// <summary>
    /// Adds the series id to the users list of favorites and returns the new list of
    /// favorites
    /// </summary>
    /// <param name="_seriesId">series to add to the favorites</param>
    /// <returns>new list with all favorites</returns>
    public List<int> AddSeriesToFavorites(int _seriesId)
    {
      if (m_userInfo != null)
      {
        List<int> list = m_downloader.DownloadUserFavoriteList(m_userInfo.UserIdentifier,
                                                      Util.UserFavouriteAction.add,
                                                      _seriesId);

        HandleUserFavoriteRetrieved(list);
        return list;
      }
      else
      {
        throw new TvdbUserNotFoundException("You can only add favorites if a user is set");
      }
    }

    /// <summary>
    /// Adds the series to the users list of favorites and returns the new list of
    /// favorites
    /// </summary>
    /// <param name="_series">series to add to the favorites</param>
    /// <returns>new list with all favorites</returns>
    public List<int> AddSeriesToFavorites(TvdbSeries _series)
    {
      if (_series == null) return null;
      return AddSeriesToFavorites(_series.Id);
    }

    /// <summary>
    /// Removes the series id from the users list of favorites and returns the new list of
    /// favorites
    /// </summary>
    /// <param name="_seriesId">series to remove from the favorites</param>
    /// <returns>new list with all favorites</returns>
    public List<int> RemoveSeriesFromFavorites(int _seriesId)
    {
      if (m_userInfo != null)
      {

        List<int> list = m_downloader.DownloadUserFavoriteList(m_userInfo.UserIdentifier,
                                                      Util.UserFavouriteAction.remove,
                                                      _seriesId);
        HandleUserFavoriteRetrieved(list);
        return list;
      }
      else
      {
        throw new TvdbUserNotFoundException("You can only add favorites if a user is set");
      }
    }

    /// <summary>
    /// Removes the series from the users list of favorites and returns the new list of
    /// favorites
    /// </summary>
    /// <param name="_series">series to remove from the favorites</param>
    /// <returns>new list with all favorites</returns>
    public List<int> RemoveSeriesFromFavorites(TvdbSeries _series)
    {
      return RemoveSeriesFromFavorites(_series.Id);
    }


    /// <summary>
    /// Rate the given series
    /// </summary>
    /// <param name="_seriesId"></param>
    /// <param name="_rating"></param>
    /// <returns></returns>
    public double RateSeries(int _seriesId, int _rating)
    {
      if (m_userInfo != null)
      {
        if (_rating < 0 || _rating > 10)
        {
          throw new ArgumentOutOfRangeException("rating must be an integer between 0 and 10");
        }
        return m_downloader.RateSeries(m_userInfo.UserIdentifier, _seriesId, _rating);
      }
      else
      {
        throw new TvdbUserNotFoundException("You can only add favorites if a user is set");
      }
    }

    /// <summary>
    /// Rate the given episode
    /// </summary>
    /// <param name="_seriesId"></param>
    /// <param name="_rating"></param>
    /// <returns></returns>
    public double RateEpisode(int _seriesId, int _rating)
    {
      if (m_userInfo != null)
      {
        if (_rating < 0 || _rating > 10)
        {
          throw new ArgumentOutOfRangeException("rating must be an integer between 0 and 10");
        }
        return m_downloader.RateEpisode(m_userInfo.UserIdentifier, _seriesId, _rating);
      }
      else
      {
        throw new TvdbUserNotFoundException("You can only add favorites if a user is set");
      }
    }

    /// <summary>
    /// Gets all series this user has already ratet
    /// </summary>
    /// <exception cref="TvdbUserNotFoundException">Thrown when no user is set</exception>
    /// <returns>A list of all rated series</returns>
    public Dictionary<int, TvdbRating> GetRatedSeries()
    {
      if (m_userInfo != null)
      {
        return m_downloader.DownloadAllSeriesRatings(m_userInfo.UserIdentifier);
      }
      else
      {
        throw new TvdbUserNotFoundException("You can only add favorites if a user is set");
      }
    }

    /// <summary>
    /// Gets all series this user has already ratet
    /// </summary>
    /// <exception cref="TvdbUserNotFoundException">Thrown when no user is set</exception>
    /// <returns>A list of all ratings for the series</returns>
    public Dictionary<int, TvdbRating> GetRatingsForSeries(int _seriesId)
    {
      if (m_userInfo != null)
      {
        return m_downloader.DownloadRatingsForSeries(m_userInfo.UserIdentifier, _seriesId);
      }
      else
      {
        throw new TvdbUserNotFoundException("You can only add favorites if a user is set");
      }
    }

    #endregion


  }
}
