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
    public CachableContent LoadFromCache()
    {
      if (File.Exists(m_filename))
      {
        try
        {
          FileStream fs = new FileStream(m_filename, FileMode.Open);
          CachableContent retValue = (CachableContent)m_formatter.Deserialize(fs);
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
    public void SaveToCache(CachableContent _content)
    {
      FileStream fs = new FileStream(m_filename, FileMode.Create);
      m_formatter.Serialize(fs, _content);
      fs.Close();
    }

    #endregion
  }
}
