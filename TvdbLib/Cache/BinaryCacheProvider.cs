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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using TvdbLib.Data;

namespace TvdbLib.Cache
{
  /// <summary>
  /// Binary cache provider saves all the cached info into 
  /// </summary>
  public class BinaryCacheProvider : ICacheProvider
  {
    /// <summary>
    /// Class to store what
    /// </summary>
    [Serializable]
    internal class SeriesConfiguration
    {
      #region private fields
      private int m_seriesId;
      private bool m_episodesLoaded;
      private bool m_bannersLoaded;
      private bool m_actorsLoaded;
      #endregion

      /// <summary>
      /// Are actors loaded
      /// </summary>
      internal bool ActorsLoaded
      {
        get { return m_actorsLoaded; }
        set { m_actorsLoaded = value; }
      }

      /// <summary>
      /// Are banners loaded
      /// </summary>
      internal bool BannersLoaded
      {
        get { return m_bannersLoaded; }
        set { m_bannersLoaded = value; }
      }

      /// <summary>
      /// Are episodes loaded
      /// </summary>
      internal bool EpisodesLoaded
      {
        get { return m_episodesLoaded; }
        set { m_episodesLoaded = value; }
      }

      /// <summary>
      /// Id of series
      /// </summary>
      internal int SeriesId
      {
        get { return m_seriesId; }
        set { m_seriesId = value; }
      }

      /// <summary>
      /// constructor
      /// </summary>
      /// <param name="_seriesId">Id of series</param>
      /// <param name="_episodesLoaded">Are episodes loaded</param>
      /// <param name="_bannersLoaded">Are banners loaded</param>
      /// <param name="_actorsLoaded">Are actors loaded</param>
      internal SeriesConfiguration(int _seriesId, bool _episodesLoaded, bool _bannersLoaded, bool _actorsLoaded)
      {
        m_seriesId = _seriesId;
        m_episodesLoaded = _episodesLoaded;
        m_bannersLoaded = _bannersLoaded;
        m_actorsLoaded = _actorsLoaded;
      }
    }

    #region private fields
    private BinaryFormatter m_formatter;//Formatter to serialize/deserialize messages
    private String m_rootFolder;
    private FileStream m_filestream;
    #endregion

    /// <summary>
    /// BinaryCacheProvider constructor
    /// </summary>
    /// <param name="_root"></param>
    public BinaryCacheProvider(String _root)
    {
      m_formatter = new BinaryFormatter(); // the formatter that will serialize my object on my stream
      if (!Directory.Exists(_root)) Directory.CreateDirectory(_root);
      m_rootFolder = _root;
    }

    #region ICacheProvider Members

    /// <summary>
    /// Load the cached data
    /// </summary>
    /// <returns></returns>
    public TvdbData LoadUserDataFromCache()
    {
      List<TvdbMirror> mirrors = LoadMirrorListFromCache();
      List<TvdbLanguage> languages = LoadLanguageListFromCache();
      DateTime lastUpdated = LoadLastUpdatedFromCache();
      TvdbData data = new TvdbData(new List<TvdbSeries>(), languages, mirrors);
      data.LastUpdated = lastUpdated;
      return data;
    }

    private DateTime LoadLastUpdatedFromCache()
    {
      if (File.Exists(m_rootFolder + Path.DirectorySeparatorChar + "lastUpdated.ser"))
      {
        try
        {
          FileStream fs = new FileStream(m_rootFolder + Path.DirectorySeparatorChar + "lastUpdated.ser", FileMode.Open);
          DateTime retValue = (DateTime)m_formatter.Deserialize(fs);
          fs.Close();
          return retValue;
        }
        catch (SerializationException)
        {
          return DateTime.Now;

        }
      }
      else
      {
        return DateTime.Now;
      }
    }

    /// <summary>
    /// Saves all available data to cache
    /// </summary>
    /// <param name="_content"></param>
    public void SaveAllToCache(TvdbData _content)
    {
      SaveToCache(_content.LanguageList);
      SaveToCache(_content.Mirrors);
      SaveToCache(_content.LastUpdated);
      foreach (TvdbSeries s in _content.SeriesList)
      {
        SaveToCache(s);
      }
    }

    private void SaveToCache(DateTime _time)
    {
      if (!Directory.Exists(m_rootFolder)) Directory.CreateDirectory(m_rootFolder);
      m_filestream = new FileStream(m_rootFolder + Path.DirectorySeparatorChar + "lastUpdated.ser", FileMode.Create);
      m_formatter.Serialize(m_filestream, _time);
      m_filestream.Close();
    }


    /// <summary>
    /// Save the language to cache
    /// </summary>
    /// <param name="_languageList"></param>
    public void SaveToCache(List<TvdbLanguage> _languageList)
    {
      if (_languageList != null)
      {
        if (!Directory.Exists(m_rootFolder)) Directory.CreateDirectory(m_rootFolder);
        m_filestream = new FileStream(m_rootFolder + Path.DirectorySeparatorChar + "languageInfo.ser", FileMode.Create);
        m_formatter.Serialize(m_filestream, _languageList);
        m_filestream.Close();
      }
    }

    /// <summary>
    /// Save the mirror info to cache
    /// </summary>
    /// <param name="_mirrorInfo"></param>
    public void SaveToCache(List<TvdbMirror> _mirrorInfo)
    {
      if (_mirrorInfo != null)
      {
        if (!Directory.Exists(m_rootFolder)) Directory.CreateDirectory(m_rootFolder);
        m_filestream = new FileStream(m_rootFolder + Path.DirectorySeparatorChar + "mirrorInfo.ser", FileMode.Create);
        m_formatter.Serialize(m_filestream, _mirrorInfo);
        m_filestream.Close();
      }
    }

    /// <summary>
    /// Loads the available languages from cache
    /// </summary>
    /// <returns></returns>
    public List<TvdbLanguage> LoadLanguageListFromCache()
    {
      if (File.Exists(m_rootFolder + Path.DirectorySeparatorChar + "languageInfo.ser"))
      {
        try
        {
          FileStream fs = new FileStream(m_rootFolder + Path.DirectorySeparatorChar + "languageInfo.ser", FileMode.Open);
          List<TvdbLanguage> retValue = (List<TvdbLanguage>)m_formatter.Deserialize(fs);
          fs.Close();
          return retValue;
        }
        catch (SerializationException)
        {
          return null;

        }
      }
      else
      {
        return null;
      }
    }

    /// <summary>
    /// Load the available mirrors from cache
    /// </summary>
    /// <returns></returns>
    public List<TvdbMirror> LoadMirrorListFromCache()
    {
      if (File.Exists(m_rootFolder + Path.DirectorySeparatorChar + "mirrorInfo.ser"))
      {
        try
        {
          FileStream fs = new FileStream(m_rootFolder + Path.DirectorySeparatorChar + "mirrorInfo.ser", FileMode.Open);
          List<TvdbMirror> retValue = (List<TvdbMirror>)m_formatter.Deserialize(fs);
          fs.Close();
          return retValue;
        }
        catch (SerializationException)
        {
          return null;

        }
      }
      else
      {
        return null;
      }
    }

    /// <summary>
    /// Load the give series from cache
    /// </summary>
    /// <param name="_seriesId"></param>
    /// <returns></returns>
    public TvdbSeries LoadSeriesFromCache(int _seriesId)
    {
      if (File.Exists(m_rootFolder + Path.DirectorySeparatorChar + "series_" + _seriesId + ".ser"))
      {
        try
        {
          FileStream fs = new FileStream(m_rootFolder + Path.DirectorySeparatorChar + "series_" + _seriesId + ".ser", FileMode.Open);
          TvdbSeries retValue = (TvdbSeries)m_formatter.Deserialize(fs);
          fs.Close();
          return retValue;
        }
        catch (SerializationException)
        {
          return null;

        }
      }
      else
      {
        return null;
      }
    }

    /// <summary>
    /// Saves the series to cache
    /// </summary>
    /// <param name="_series"></param>
    public void SaveToCache(TvdbSeries _series)
    {
      if (_series != null)
      {
        if (!Directory.Exists(m_rootFolder)) Directory.CreateDirectory(m_rootFolder);
        m_filestream = new FileStream(m_rootFolder + Path.DirectorySeparatorChar + "series_" + _series.Id + ".ser", FileMode.Create);
        m_formatter.Serialize(m_filestream, _series);
        m_filestream.Close();

        SeriesConfiguration cfg = new SeriesConfiguration(_series.Id, _series.EpisodesLoaded,
                                                  _series.BannersLoaded, _series.TvdbActorsLoaded);
        m_filestream = new FileStream(m_rootFolder + Path.DirectorySeparatorChar + "series_" + _series.Id + ".cfg", FileMode.Create);
        m_formatter.Serialize(m_filestream, cfg);
        m_filestream.Close();
      }
    }

    /// <summary>
    /// Saves the user data to cache
    /// </summary>
    /// <param name="_user"></param>
    public void SaveToCache(TvdbUser _user)
    {
      if (_user != null)
      {
        if (!Directory.Exists(m_rootFolder)) Directory.CreateDirectory(m_rootFolder);
        m_filestream = new FileStream(m_rootFolder + Path.DirectorySeparatorChar + "user_" + _user.UserIdentifier + ".ser", FileMode.Create);
        m_formatter.Serialize(m_filestream, _user);
        m_filestream.Close();
      }
    }

    /// <summary>
    /// Loads all series from cache
    /// </summary>
    /// <returns></returns>
    public List<TvdbSeries> LoadAllSeriesFromCache()
    {
      if (Directory.Exists(m_rootFolder))
      {
        String[] files = Directory.GetFiles(m_rootFolder, Path.DirectorySeparatorChar + "series*.ser");
        List<TvdbSeries> retSeries = new List<TvdbSeries>();
        foreach (String f in files)
        {
          if (File.Exists(f))
          {
            try
            {
              FileStream fs = new FileStream(f, FileMode.Open);
              TvdbSeries retValue = (TvdbSeries)m_formatter.Deserialize(fs);
              fs.Close();
              retSeries.Add(retValue);
            }
            catch (SerializationException)
            {
            }
          }
        }
        return retSeries;
      }
      else
      {
        return null;
      }
    }
    /// <summary>
    /// Load the userinfo from the cache
    /// </summary>
    /// <param name="_userId"></param>
    /// <returns></returns>
    public TvdbUser LoadUserInfoFromCache(String _userId)
    {
      if (File.Exists(m_rootFolder + Path.DirectorySeparatorChar + "user_" + _userId + ".ser"))
      {
        try
        {
          FileStream fs = new FileStream(m_rootFolder + Path.DirectorySeparatorChar + "user_" + _userId + ".ser", FileMode.Open);
          TvdbUser retValue = (TvdbUser)m_formatter.Deserialize(fs);
          fs.Close();
          return retValue;
        }
        catch (SerializationException)
        {
          return null;

        }
      }
      else
      {
        return null;
      }
    }

    /// <summary>
    /// Receives a list of all series that have been cached
    /// </summary>
    /// <returns></returns>
    public List<int> GetCachedSeries()
    {
      if (Directory.Exists(m_rootFolder))
      {
        String[] files = Directory.GetFiles(m_rootFolder, "series*.ser");
        List<int> retSeries = new List<int>();
        foreach (String f in files)
        {
          String id = f.Substring(f.LastIndexOf("_") + 1, f.LastIndexOf(".") - f.LastIndexOf("_") - 1);
          try
          {
            int intId = Int32.Parse(id);
            retSeries.Add(intId);
          }
          catch (Exception)
          {

          }
        }
        return retSeries;
      }
      else
      {
        return null;
      }
    }

    /// <summary>
    /// Check if the series is cached in the given configuration
    /// </summary>
    /// <param name="_seriesId">Id of the series</param>
    /// <param name="_episodesLoaded">are episodes loaded</param>
    /// <param name="_bannersLoaded">are banners loaded</param>
    /// <param name="_actorsLoaded">are actors loaded</param>
    /// <returns>true if the series is cached, false otherwise</returns>
    public bool IsCached(int _seriesId, bool _episodesLoaded, 
                         bool _bannersLoaded, bool _actorsLoaded)
    {
      if (File.Exists(m_rootFolder + Path.DirectorySeparatorChar + "series_" + _seriesId + ".cfg"))
      {
        try
        {
          FileStream fs = new FileStream(m_rootFolder + Path.DirectorySeparatorChar + "series_" + _seriesId + ".cfg", FileMode.Open);
          SeriesConfiguration config = (SeriesConfiguration)m_formatter.Deserialize(fs);
          fs.Close();

          if (config.EpisodesLoaded || !_episodesLoaded &&
             config.BannersLoaded || !_bannersLoaded &&
             config.ActorsLoaded || !_actorsLoaded)
          {
            return true;
          }
          else
          {
            return false;
          }
        }
        catch (SerializationException)
        {
          Log.Warn("Cannot deserialize SeriesConfiguration object");
          return false;
        }
      }
      else
      {
        return false;
      }
    }

    #endregion
  }
}
