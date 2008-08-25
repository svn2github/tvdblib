using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TvdbConnector.Data;
using System.Net;
using TvdbConnector.Cache;
using TvdbConnector.Data.Banner;
using TvdbConnector.Xml;
using System.Diagnostics;

namespace TvdbConnector
{
  public class Tvdb
  {
    private List<TvdbMirror> m_mirrorInfo;
    private ICacheProvider m_cacheProvider;
    private String m_apiKey;
    private TvdbUser m_userInfo;
    private TvdbDownloader m_downloader;
    private TvdbData m_loadedData;

    public TvdbUser UserInfo
    {
      get { return m_userInfo; }
      set
      {
        m_userInfo = value;
        if (m_cacheProvider != null)
        {
          //try to load the userinfo from cache
          TvdbUser user = m_cacheProvider.LoadUserInfoToCache(value.UserIdentifier);
          if (user != null)
          {
            m_userInfo.UserFavorites = user.UserFavorites;
            m_userInfo.UserPreferredLanguage = user.UserPreferredLanguage;
            m_userInfo.UserName = user.UserName;
          }
        }
      }
    }

    public String ApiKey
    {
      get { return m_apiKey; }
    }

    public Tvdb(ICacheProvider _cacheProvider, String _apiKey)
    {
      m_apiKey = _apiKey; //store api key
      m_cacheProvider = _cacheProvider; //store given cache provider
      m_loadedData = new TvdbData();
      m_loadedData.Mirrors = new List<TvdbMirror>();
      m_loadedData.SeriesList = new List<TvdbSeries>();
      m_loadedData.LanguageList = new List<TvdbLanguage>();
      TvdbLinks.ActiveMirror = new TvdbMirror(0, new Uri(TvdbLinks.BASE_SERVER), 7);
      m_downloader = new TvdbDownloader(m_apiKey);
    }

    /// <summary>
    /// Load previously stored information on (except series information) from cache
    /// </summary>
    /// <returns>true if cache could be loaded successfully, false otherwise</returns>
    public bool InitCache()
    {
      if (m_cacheProvider != null)
      {
        //TODO: merge existing settings with loaded settings
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
    /// <param name="_seriesId">id of series</param>
    /// <param name="_language">language that should be retrieved</param>
    /// <param name="_full">if true, the full series record will be loaded (series + all episodes), otherwise only the base record will be loaded which contains only series information</param>
    /// <param name="_loadBanners">if true also loads the paths to the banners</param>
    /// <returns>Instance of TvdbSeries containing all gained information</returns>
    public TvdbSeries GetSeries(int _seriesId, TvdbLanguage _language, bool _loadEpisodes,
                                bool _loadActors, bool _loadBanners)
    {
      Stopwatch watch = new Stopwatch();
      watch.Start();

      TvdbSeries series = GetSeriesFromCache(_seriesId, _language);

      if (series == null)
      {
        series = m_downloader.DownloadSeries(_seriesId, _language, _loadEpisodes, _loadActors, _loadBanners);
        if (series == null)
        {
          return null;
        }
        watch.Stop();
        Log.Debug("Loaded series in " + watch.ElapsedMilliseconds + " milliseconds");
        series.IsFavorite = m_userInfo == null ? false : CheckIfSeriesFavorite(_seriesId, m_userInfo.UserFavorites);
        AddSeriesToCache(series);
        return series;
      }

      if (_loadActors && !series.TvdbActorsLoaded)
      {//user wants actors loaded
        series.TvdbActors = m_downloader.DownloadActors(_seriesId);
      }

      if (_loadEpisodes && !series.EpisodesLoaded)
      {//user wants the full version but only the basic has been loaded (without episodes
        series.Episodes = m_downloader.DownloadEpisodes(_seriesId, _language);
      }

      if (_loadBanners && !series.BannersLoaded)
      {//user wants banners loaded but current series hasn't -> Do it baby
        series.Banners = m_downloader.DownloadBanners(_seriesId);
      }

      watch.Stop();
      Log.Debug("Loaded series in " + watch.ElapsedMilliseconds + " milliseconds");

      return series;
    }


    /// <summary>
    /// Gets the full series (including episode information and actors) with the given id either from cache 
    /// (if it has already been loaded) or from the selected tvdb mirror.
    /// 
    /// To check if this series has already been cached, pleas use the Method IsCached(TvdbSeries _series)
    /// </summary>
    /// <param name="_seriesId">id of series</param>
    /// <param name="_language">language that should be retrieved</param>
    /// <param name="_full">if true, the full series record will be loaded (series + all episodes), otherwise only the base record will be loaded which contains only series information</param>
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
    /// <param name="_seriesId">id of series</param>
    /// <param name="_language">language that should be retrieved</param>
    /// <param name="_full">if true, the full series record will be loaded (series + all episodes), otherwise only the base record will be loaded which contains only series information</param>
    /// <param name="_loadBanners">if true also loads the paths to the banners</param>
    /// <returns>Instance of TvdbSeries containing all gained information</returns>
    public TvdbSeries GetBasicSeries(int _seriesId, TvdbLanguage _language, bool _loadBanners)
    {
      return GetSeries(_seriesId, _language, false, false, _loadBanners);
    }

    /// <summary>
    /// Returns if the series is locally cached
    /// </summary>
    /// <param name="_seriesId"></param>
    /// <param name="_language"></param>
    /// <param name="_full"></param>
    /// <returns></returns>
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
      return false;
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="_episodeId"></param>
    /// <param name="_language"></param>
    /// <returns></returns>
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

          _series.UpdateSeriesInfo(m_loadedData.SeriesList[i]); //so we're not losing banners, etc.
          m_loadedData.SeriesList[i] = _series;
        }
      }
      if (!found)
      {
        m_loadedData.SeriesList.Add(_series);
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_epList"></param>
    private void AddEpisodeToCache(TvdbEpisode e)
    {
      bool seriesFound = false;
      foreach (TvdbSeries s in m_loadedData.SeriesList)
      {
        if (s.Id == e.SeriesId)
        {//series for ep found
          seriesFound = true;
          Util.AddEpisodeToSeries(e, s);
          break;
        }

      }
      if (!seriesFound)
      {//the series doesn't exist yet -> add episode to dummy series
        TvdbSeries newSeries = new TvdbSeries();
        newSeries.LastUpdated = new DateTime(1, 1, 1);
        newSeries.Episodes.Add(e);
        m_loadedData.SeriesList.Add(newSeries);
      }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="_seriesId"></param>
    /// <param name="_language"></param>
    /// <returns></returns>
    private TvdbSeries GetSeriesFromCache(int _seriesId, TvdbLanguage _language)
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
        series.IsFavorite = CheckIfSeriesFavorite(series.Id, m_userInfo.UserFavorites);
        AddSeriesToCache(series);
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
      WebClient client = new WebClient();
      String xml = client.DownloadString(TvdbLinks.BASE_SERVER + "api/" +
                                         "E8D8A47528D5B5AD" +
                                         TvdbLinks.MIRROR_PATH);
      TvdbXmlReader hand = new TvdbXmlReader();
      List<TvdbMirror> list = hand.ExtractMirrors(xml);
      if (list != null && list.Count > 0)
      {
        m_mirrorInfo = list;
        return true;
      }
      else
      {
        return false;
      }
    }

    /// <summary>
    /// Is the information on available tvdb mirrors already cached
    /// </summary>
    public bool IsMirrorInformationCached
    {
      get
      {
        throw new NotImplementedException("todo");
        return true;
      }
    }

    public bool IsUpdateAvailable
    {
      get
      {
        return true;
      }
    }

    public bool UpdateAllSeries()
    {
      TimeSpan timespanLastUpdate = (DateTime.Now - m_loadedData.LastUpdated);
      //MakeUpdate(TvdbLinks.CreateUpdateLink(m_apiKey, TvdbLinks.UpdateInterval.day));
      if (timespanLastUpdate < new TimeSpan(1, 0, 0, 0))
      {//last update is less than a day ago -> make a daily update
        //MakeUpdate(TvdbLinks.CreateUpdateLink(m_apiKey, Util.UpdateInterval.day));
        MakeUpdate(Util.UpdateInterval.day);
      }
      else if (timespanLastUpdate < new TimeSpan(7, 0, 0, 0))
      {//last update is less than a week ago -> make a weekly update
        //MakeUpdate(TvdbLinks.CreateUpdateLink(m_apiKey, Util.UpdateInterval.week));
        MakeUpdate(Util.UpdateInterval.week);
      }
      else if (timespanLastUpdate < new TimeSpan(31, 0, 0, 0))
      {//last update is less than a month ago -> make a monthly update
        //MakeUpdate(TvdbLinks.CreateUpdateLink(m_apiKey, Util.UpdateInterval.month));
        MakeUpdate(Util.UpdateInterval.month);

      }
      else
      {//TODO: Make a full update -> full update deosn't make sense... (do a complete re-scan?)
        //MakeUpdate(TvdbLinks.CreateUpdateLink(m_apiKey, TvdbLinks.UpdateInterval.day));
      }


      return true;
    }

    private bool MakeUpdate(Util.UpdateInterval _interval)
    {
      //update all flagged series
      List<TvdbSeries> updateSeries;
      List<TvdbEpisode> updateEpisodes;
      DateTime updateTime = m_downloader.DownloadUpdate(out updateSeries, out updateEpisodes, _interval);
      foreach (TvdbSeries us in updateSeries)
      {
        foreach (TvdbSeries s in m_loadedData.SeriesList)
        {
          if (us.Id == s.Id && s.LastUpdated < us.LastUpdated)
          {//changes occured in series
            //get series info
            TvdbSeries newSeries = GetSeries(s.Id, s.Language, false, false, false);
            newSeries.LastUpdated = us.LastUpdated;
            if (newSeries != null)
            {
              s.UpdateSeriesInfo(newSeries);
              Log.Info("Updated Series " + s.Id);
            }
          }
        }
      }

      //update all flagged episodes
      foreach (TvdbEpisode ue in updateEpisodes)
      {
        foreach (TvdbSeries s in m_loadedData.SeriesList)
        {
          if (ue.SeriesId == s.Id)
          {
            foreach (TvdbEpisode e in s.Episodes)
            {
              if (e.Id == ue.Id && e.LastUpdated < ue.LastUpdated)
              {
                //download episode which has been updated
                TvdbEpisode newEpisode = m_downloader.DownloadEpisode(e.Id, e.Language);

                //update information of episode with new episodes informations
                if (newEpisode != null)
                {
                  newEpisode.LastUpdated = ue.LastUpdated;

                  e.UpdateEpisodeInfo(newEpisode);
                  Log.Info("Updated Episode " + e.Id);
                }
              }
            }
            break;
          }
        }
      }

      //todo: update banner information here -> ask in forum if fanart doesn't contain all fields on purpose...

      //set the last updated time to time of this update
      m_loadedData.LastUpdated = updateTime;

      return true;

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
      m_cacheProvider.SaveAllToCache(new TvdbData(m_loadedData.SeriesList, m_loadedData.LanguageList, m_mirrorInfo));
      if (m_userInfo != null) m_cacheProvider.SaveToCache(m_userInfo);
    }


    /// <summary>
    /// Returns all series that are already cached in memory -> won't return information that has been cached locally but not yet loaded into memory
    /// </summary>
    /// <returns>List of loaded series</returns>
    public List<TvdbSeries> GetCachedSeries()
    {
      //TODO: should be removed at some point since we're not holding all cached information in memory
      return m_loadedData.SeriesList;
    }


    /// <summary>
    /// Forces a complete update of the series. All information that has already been loaded (including loaded images!) will be deleted and reloaded from tvdb -> if you only want to update the series, use the "MakeUpdate" method
    /// </summary>
    /// <param name="_seriesId"></param>
    public TvdbSeries ForceUpdate(TvdbSeries _series)
    {
      return ForceUpdate(_series, _series.EpisodesLoaded, _series.TvdbActorsLoaded, _series.BannersLoaded);


    }
    /// <summary>
    /// Forces a complete update of the series. All information that has already been loaded (including loaded images!) will be deleted and reloaded from tvdb -> if you only want to update the series, use the "MakeUpdate" method
    /// </summary>
    /// <param name="m_currentSeries"></param>
    /// <param name="p"></param>
    /// <param name="p_3"></param>
    /// <param name="p_4"></param>
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
              retList.Add(GetSeriesFromCache(sId, _lang));
            }
            else
            {
              TvdbSeries series = m_downloader.DownloadSeries(sId, _lang, false, false, false);
              if (series != null)
              {
                retList.Add(series);
              }
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

    #endregion


  }
}
