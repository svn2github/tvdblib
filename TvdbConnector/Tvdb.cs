using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TvdbConnector.Data;
using System.Net;
using TvdbConnector.Cache;
using TvdbConnector.Data.Banner;

namespace TvdbConnector
{
  public class Tvdb
  {
    private List<TvdbMirror> m_mirrorInfo;
    private TvdbSettings m_settings;
    private ICacheProvider m_cacheProvider;
    private String m_apiKey;
    private TvdbUser m_userInfo;
    private TvdbDownloader m_downloader;



    public TvdbUser UserInfo
    {
      get { return m_userInfo; }
      set { m_userInfo = value; }
    }

    public String ApiKey
    {
      get { return m_apiKey; }
    }

    public Tvdb(ICacheProvider _cacheProvider, String _apiKey)
    {
      m_apiKey = _apiKey; //store api key
      m_cacheProvider = _cacheProvider; //store given cache provider


      TvdbCache.SeriesList = new List<TvdbSeries>();
      TvdbCache.LanguageList = new List<TvdbLanguage>();
      m_mirrorInfo = new List<TvdbMirror>();
      TvdbLinks.ActiveMirror = new TvdbMirror(0, new Uri(TvdbLinks.BASE_SERVER), 7);
      m_settings = new TvdbSettings(new DateTime(1, 1, 1));

      m_downloader = new TvdbDownloader(m_apiKey);
    }

    /// <summary>
    /// Load previously stored information on series, episodes,... from cache
    /// </summary>
    /// <returns>true if cache could be loaded successfully, false otherwise</returns>
    public bool LoadCache()
    {
      if (m_cacheProvider != null)
      {
        //TODO: merge existing settings with loaded settings
        CachableContent cache = m_cacheProvider.LoadFromCache(); //try to load cache
        if (cache != null)
        {
          TvdbCache.SeriesList = cache.SeriesInfo != null ? cache.SeriesInfo : new List<TvdbSeries>();
          TvdbCache.LanguageList = cache.Language != null ? cache.Language : new List<TvdbLanguage>();
          if (cache.Mirrors != null && cache.Mirrors.Count > 0)
          {
            m_mirrorInfo = cache.Mirrors;
          }
          else
          {
            m_mirrorInfo = new List<TvdbMirror>();
            TvdbLinks.ActiveMirror = new TvdbMirror(0, new Uri(TvdbLinks.BASE_SERVER), 7);
          }
          m_settings = cache.Settings != null ? cache.Settings : new TvdbSettings(new DateTime(1, 1, 1));
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
    /// To check if this series has already been cached, pleas use the Method IsCached(TvdbSeries _series)
    /// </summary>
    /// <param name="_seriesId">id of series</param>
    /// <param name="_language">language that should be retrieved</param>
    /// <param name="_full">if true, the full series record will be loaded (series + all episodes), otherwise only the base record will be loaded which contains only series information</param>
    /// <param name="_loadBanners">if true also loads the paths to the banners</param>
    /// <returns>Instance of TvdbSeries containing all gained information</returns>
    public TvdbSeries GetSeries(int _seriesId, TvdbLanguage _language, bool _full, bool _loadBanners)
    {
      TvdbSeries series = GetSeriesFromCache(_seriesId, _language);

      if (series == null)
      {
        series = m_downloader.DownloadSeries(_seriesId, _language, _full, _loadBanners);
        if (series == null)
        {
          return null;
        }
        AddSeriesToCache(series);
        return series;
      }
      
      if (_full && !series.EpisodesLoaded)
      {//user wants the full version but only the basic has been loaded (without episodes
        series.Episodes = m_downloader.DownloadEpisodes(_seriesId, _language);
      }

      if (_loadBanners && !series.BannersLoaded)
      {//user wants banners loaded but current series hasn't -> Do it baby
        series.Banners = m_downloader.DownloadBanners(_seriesId);
      }

      return series;
      
    }


    /// <summary>
    /// Gets the full series (including episode information) with the given id either from cache 
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
      return GetSeries(_seriesId, _language, false, _loadBanners);
    }

    /// <summary>
    /// Gets the basic series (without episode information) with the given id either from cache 
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
      return GetSeries(_seriesId, _language, false, _loadBanners);
    }

    /// <summary>
    /// Returns if the series is locally cached
    /// </summary>
    /// <param name="_seriesId"></param>
    /// <param name="_language"></param>
    /// <param name="_full"></param>
    /// <returns></returns>
    public bool IsCached(int _seriesId, TvdbLanguage _language, bool _full)
    {
      foreach (TvdbSeries s in TvdbCache.SeriesList)
      {
        if (s.Id == _seriesId
            && s.Language.Id == _language.Id
            && ((s.GetType() == typeof(TvdbSeries) && _full) || !_full))
        {
          return true;
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
        episode =  m_downloader.DownloadEpisode(_episodeId, _language);
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
      for (int i = 0; i < TvdbCache.SeriesList.Count; i++)
      {
        if (((TvdbSeries)TvdbCache.SeriesList[i]).Id == _series.Id)
        {
          found = true;

          _series.UpdateSeriesInfo(TvdbCache.SeriesList[i]); //so we're not losing banners, etc.
          TvdbCache.SeriesList[i] = _series;
        }
      }
      if (!found)
      {
        TvdbCache.SeriesList.Add(_series);
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_epList"></param>
    private void AddEpisodeToCache(TvdbEpisode e)
    {
        bool seriesFound = false;
        foreach (TvdbSeries s in TvdbCache.SeriesList)
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
          TvdbCache.SeriesList.Add(newSeries);
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
      foreach (TvdbSeries s in TvdbCache.SeriesList)
      {
        if (s.Id == _seriesId)
        {
          return s;
        }
      }
      return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_episodeId"></param>
    /// <param name="_language"></param>
    /// <returns></returns>
    private TvdbEpisode GetEpisodeFromCache(int _episodeId, TvdbLanguage _language)
    {
      foreach (TvdbSeries s in TvdbCache.SeriesList)
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
      XmlHandler hand = new XmlHandler();
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
      TimeSpan timespanLastUpdate = (DateTime.Now - m_settings.LastUpdated);
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
      {//TODO: Make a full update -> full update deosn't make sen
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
        foreach (TvdbSeries s in TvdbCache.SeriesList)
        {
          if (us.Id == s.Id && s.LastUpdated < us.LastUpdated)
          {//changes occured in series
            //get series info
            TvdbSeries newSeries = GetSeries(s.Id, s.Language, false, false);
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
        foreach (TvdbSeries s in TvdbCache.SeriesList)
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

      //set the last updated time to time of this update
      m_settings.LastUpdated = updateTime;

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
          return TvdbCache.LanguageList;
        }
        else
        {
          List<TvdbLanguage> list = m_downloader.DownloadLanguages();
          if (list != null && list.Count > 0)
          {
            TvdbCache.LanguageList = list;
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
        TvdbCache.LanguageList = list;
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
        return (TvdbCache.LanguageList != null && TvdbCache.LanguageList.Count > 0);
      }
    }

    /// <summary>
    /// Saves all retrieved data into persistant storage cache (depending on cache provider)
    /// </summary>
    public void SaveCache()
    {
      m_cacheProvider.SaveToCache(new CachableContent(TvdbCache.SeriesList, TvdbCache.LanguageList, m_mirrorInfo, m_settings));
    }


    /// <summary>
    /// Returns all series that are already cached locally
    /// </summary>
    /// <returns>List of loaded series</returns>
    public List<TvdbSeries> GetCachedSeries()
    {
      return TvdbCache.SeriesList;
    }


    /// <summary>
    /// Forces a complete update of the series
    /// </summary>
    /// <param name="_seriesId"></param>
    public TvdbSeries ForceUpdate(TvdbSeries _series)
    {

      _series = m_downloader.DownloadSeries(_series.Id, _series.Language, true, true);
      return _series;

    }

    public TvdbLanguage GetPreferredLanguage()
    {
      if (m_userInfo != null)
      {
        TvdbLanguage userLang = m_downloader.DownloadUserPreferredLanguage(m_userInfo.UserIdentifier);

        if (userLang != null)
        {
          //only one language is contained in the userlang file
          foreach (TvdbLanguage l in TvdbCache.LanguageList)
          {
            if (l.Id == userLang.Id) return l;
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
        throw new Exception("You can't get the preferred language when no user is specified");
      }
    }

    public List<int> GetUserFavouritesList(TvdbLanguage _lang)
    {
      if (m_userInfo != null)
      {
        List<int> userLang = m_downloader.DownloadUserFavouriteList(m_userInfo.UserIdentifier);
        return userLang;
      }
      else
      {
        throw new Exception("You can't get the preferred language when no user is specified");
      }
    }


    public List<TvdbSeries> GetUserFavourites(TvdbLanguage _lang)
    {
      if (m_userInfo != null)
      {
        if (_lang != null)
        {
          List<int> idList = GetUserFavouritesList(_lang);
          List<TvdbSeries> retList = new List<TvdbSeries>();

          foreach (int sId in idList)
          {
            if (IsCached(sId, _lang, false))
            {
              retList.Add(GetSeriesFromCache(sId, _lang));
            }
            else
            {
              TvdbSeries series = m_downloader.DownloadSeries(sId, _lang, false, false);

              AddSeriesToCache(series);

              if (series != null)
              {
                retList.Add(series);
              }
            }
          }
          return retList;
        }
        else
        {
          throw new Exception("you have to define a language");
        }
      }
      else
      {
        throw new Exception("You can't get the favourites when no user is defined");
      }
    }
  }
}
