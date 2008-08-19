using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace TvdbConnector.Cache
{
  public class BinaryCacheProvider: ICacheProvider
  {
    private BinaryFormatter m_formatter;//Formatter to serialize/deserialize messages
    private String m_filename;
    public BinaryCacheProvider(String _filename)
    {
      m_formatter = new BinaryFormatter(); // the formatter that will serialize my object on my stream
      m_filename = _filename;
    }

    #region ICacheProvider Members

    /// <summary>
    /// Load the cached data
    /// </summary>
    /// <returns></returns>
    public TvdbData LoadUserDataFromCache()
    {
      if (File.Exists(m_filename))
      {
        try
        {
          FileStream fs = new FileStream(m_filename, FileMode.Open);
          TvdbData retValue = (TvdbData)m_formatter.Deserialize(fs);
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
    /// Save the cache to filesystem
    /// </summary>
    /// <param name="_content"></param>
    public void SaveAllToCache(TvdbData _content)
    {
      FileStream fs = new FileStream(m_filename, FileMode.Create);
      m_formatter.Serialize(fs, _content);
      fs.Close();
    }

    public TvdbData LoadFromCache()
    {
      throw new NotImplementedException();
    }

    public void SaveAllToCache()
    {
      throw new NotImplementedException();
    }

    public void SaveToCache(List<TvdbConnector.Data.TvdbLanguage> _languageList)
    {
      throw new NotImplementedException();
    }

    public void SaveToCache(List<TvdbConnector.Data.TvdbMirror> _mirrorInfo)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region ICacheProvider Members


    public List<TvdbConnector.Data.TvdbLanguage> LoadLanguageListFromCache()
    {
      throw new NotImplementedException();
    }

    public List<TvdbConnector.Data.TvdbMirror> LoadMirrorListFromCache()
    {
      throw new NotImplementedException();
    }

    public TvdbConnector.Data.TvdbSeries LoadSeriesFromCache(int _seriesId)
    {
      throw new NotImplementedException();
    }

    public void SaveToCache(TvdbConnector.Data.TvdbSeries _series)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region ICacheProvider Members


    public List<TvdbConnector.Data.TvdbSeries> LoadAllSeriesFromCache()
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}
