using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TvdbConnector.Data;

namespace TvdbConnector.Cache
{
  public interface ICacheProvider
  {
    /// <summary>
    /// Loads all cached series from cache -> can take a while
    /// </summary>
    /// <returns></returns>
    TvdbData LoadUserDataFromCache();

    /// <summary>
    /// Loads the available languages from cache
    /// </summary>
    /// <returns></returns>
    List<TvdbLanguage> LoadLanguageListFromCache();

    /// <summary>
    /// Load the available mirrors from cache
    /// </summary>
    /// <returns></returns>
    List<TvdbMirror> LoadMirrorListFromCache();

    /// <summary>
    /// Loads all series from cache
    /// </summary>
    /// <returns></returns>
    List<TvdbSeries> LoadAllSeriesFromCache();

    /// <summary>
    /// Load the give series from cache
    /// </summary>
    /// <param name="_seriesId"></param>
    /// <returns></returns>
    TvdbSeries LoadSeriesFromCache(int _seriesId);

    /// <summary>
    /// Load user info from cache
    /// </summary>
    /// <param name="_userId"></param>
    /// <returns></returns>
    TvdbUser LoadUserInfoToCache(String _userId);

    /// <summary>
    /// Saves all available data to cache
    /// </summary>
    /// <param name="_content"></param>
    void SaveAllToCache(TvdbData _content);

    /// <summary>
    /// Save the language to cache
    /// </summary>
    /// <param name="_languageList"></param>
    void SaveToCache(List<TvdbLanguage> _languageList);

    /// <summary>
    /// Save the mirror info to cache
    /// </summary>
    /// <param name="_mirrorInfo"></param>
    void SaveToCache(List<TvdbMirror> _mirrorInfo);

    /// <summary>
    /// Saves the series to cache
    /// </summary>
    /// <param name="_series"></param>
    void SaveToCache(TvdbSeries _series);

    /// <summary>
    /// Saves the user data to cache
    /// </summary>
    /// <param name="_user"></param>
    void SaveToCache(TvdbUser _user);
  }
}
