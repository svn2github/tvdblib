using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using TvdbConnector.Data;

namespace TvdbConnector.Cache
{
  /// <summary>
  /// Binary cache provider saves all the cached info into 
  /// </summary>
  public class BinaryCacheProvider : ICacheProvider
  {
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
      TvdbData data = new TvdbData(new List<TvdbSeries>(), languages, mirrors);
      return data;
    }

    /// <summary>
    /// Saves all available data to cache
    /// </summary>
    /// <param name="_content"></param>
    public void SaveAllToCache(TvdbData _content)
    {
      SaveToCache(_content.LanguageList);
      SaveToCache(_content.Mirrors);
      foreach (TvdbSeries s in _content.SeriesList)
      {
        SaveToCache(s);
      }
    }


    /// <summary>
    /// Save the language to cache
    /// </summary>
    /// <param name="_languageList"></param>
    public void SaveToCache(List<TvdbLanguage> _languageList)
    {
      if (_languageList != null)
      {
        m_filestream = new FileStream(m_rootFolder + "\\languageInfo.ser", FileMode.Create);
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
        m_filestream = new FileStream(m_rootFolder + "\\mirrorInfo.ser", FileMode.Create);
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
      if (File.Exists(m_rootFolder + "\\languageInfo.ser"))
      {
        try
        {
          FileStream fs = new FileStream(m_rootFolder + "\\languageInfo.ser", FileMode.Open);
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
      if (File.Exists(m_rootFolder + "\\mirrorInfo.ser"))
      {
        try
        {
          FileStream fs = new FileStream(m_rootFolder + "\\mirrorInfo.ser", FileMode.Open);
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
      if (File.Exists(m_rootFolder + "\\series_" + _seriesId + ".ser"))
      {
        try
        {
          FileStream fs = new FileStream(m_rootFolder + "\\series_" + _seriesId + ".ser", FileMode.Open);
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
        m_filestream = new FileStream(m_rootFolder + "\\series_" + _series.Id + ".ser", FileMode.Create);
        m_formatter.Serialize(m_filestream, _series);
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
        m_filestream = new FileStream(m_rootFolder + "\\user_" + _user.UserIdentifier + ".ser", FileMode.Create);
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
        String[] files = Directory.GetFiles(m_rootFolder, "\\series*.ser");
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
    public TvdbUser LoadUserInfoToCache(String _userId)
    {
      if (File.Exists(m_rootFolder + "\\user_" + _userId + ".ser"))
      {
        try
        {
          FileStream fs = new FileStream(m_rootFolder + "\\user_" + _userId + ".ser", FileMode.Open);
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
    #endregion
  }
}
