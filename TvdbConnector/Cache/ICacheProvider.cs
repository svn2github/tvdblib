using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TvdbConnector.Data;

namespace TvdbConnector.Cache
{
  /// <summary>
  /// A cache provider stores and loads the data that has been previously retrieved from http://thetvdb.com.
  /// </summary>
  public interface ICacheProvider
  {
    /// <summary>
    /// Loads all cached series from cache -> can take a while
    /// </summary>
    /// <returns>The loaded TvdbData object</returns>
    TvdbData LoadUserDataFromCache();

    /// <summary>
    /// Loads the available languages from cache
    /// </summary>
    /// <returns>A list of TvdbLanguage objects from cache or null</returns>
    List<TvdbLanguage> LoadLanguageListFromCache();

    /// <summary>
    /// Load the available mirrors from cache
    /// </summary>
    /// <returns>A list of TvdbMirror objects from cache or null</returns>
    List<TvdbMirror> LoadMirrorListFromCache();

    /// <summary>
    /// Loads all series from cache
    /// </summary>
    /// <returns>A list of TvdbSeries objects from cache or null</returns>
    List<TvdbSeries> LoadAllSeriesFromCache();

    /// <summary>
    /// Load the give series from cache
    /// </summary>
    /// <param name="_seriesId">Id of the series to load</param>
    /// <returns>The TvdbSeries object from cache or null</returns>
    TvdbSeries LoadSeriesFromCache(int _seriesId);

    /// <summary>
    /// Load user info from cache
    /// </summary>
    /// <param name="_userId">Id of the user</param>
    /// <returns>TvdbUser object or null if the user couldn't be loaded</returns>
    TvdbUser LoadUserInfoFromCache(String _userId);

    /// <summary>
    /// Saves all available data to cache
    /// </summary>
    /// <param name="_content">TvdbData object</param>
    void SaveAllToCache(TvdbData _content);

    /// <summary>
    /// Save the language to cache
    /// </summary>
    /// <param name="_languageList">List of languages that are available on http://thetvdb.com</param>
    void SaveToCache(List<TvdbLanguage> _languageList);

    /// <summary>
    /// Save the mirror info to cache
    /// </summary>
    /// <param name="_mirrorInfo">List of mirrors of http://thetvdb.com</param>
    void SaveToCache(List<TvdbMirror> _mirrorInfo);

    /// <summary>
    /// Saves the series to cache
    /// </summary>
    /// <param name="_series">TvdbSeries object</param>
    void SaveToCache(TvdbSeries _series);

    /// <summary>
    /// Saves the user data to cache
    /// </summary>
    /// <param name="_user">TvdbUser object</param>
    void SaveToCache(TvdbUser _user);

    /// <summary>
    /// Receives a list of all series that have been cached
    /// </summary>
    /// <returns>A list of series that have been already stored with this cache provider</returns>
    List<int> GetCachedSeries();

    /// <summary>
    /// Check if the series is cached in the given configuration
    /// </summary>
    /// <param name="_seriesId">Id of the series</param>
    /// <param name="_episodesLoaded">are episodes loaded</param>
    /// <param name="_bannersLoaded">are banners loaded</param>
    /// <param name="_actorsLoaded">are actors loaded</param>
    /// <param name="_language">language of the series</param>
    /// <returns>true if the series is cached, false otherwise</returns>
    bool IsCached(int _seriesId, TvdbLanguage _language, bool _episodesLoaded, bool _bannersLoaded, bool _actorsLoaded);

  }
}
