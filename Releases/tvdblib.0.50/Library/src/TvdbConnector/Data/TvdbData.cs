using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TvdbConnector.Data;

namespace TvdbConnector
{
  /// <summary>
  /// TvdbData contains a list of series, a list of languages and a list of mirror
  /// </summary>
  [Serializable]
  public class TvdbData
  {
    #region private properties
    private List<TvdbSeries> m_seriesInfo;
    private List<TvdbLanguage> m_langInfo;
    private List<TvdbMirror> m_mirrorInfo;
    private DateTime m_lastUpdated;
    #endregion

    /// <summary>
    /// TvdbData constructor
    /// </summary>
    public TvdbData()
    {
      m_lastUpdated = new DateTime(1, 1, 1);
    }

    /// <summary>
    /// TvdbData constructor
    /// </summary>
    public TvdbData(List<TvdbSeries> _seriesInfo, List<TvdbLanguage> _language, List<TvdbMirror> _mirrors)
      : this()
    {
      m_seriesInfo = _seriesInfo;
      m_langInfo = _language;
      m_mirrorInfo = _mirrors;
    }

    /// <summary>
    /// When was the last time thetvdb has been checked
    /// for updates
    /// </summary>
    public DateTime LastUpdated
    {
      get { return m_lastUpdated; }
      set { m_lastUpdated = value; }
    }

    /// <summary>
    /// List of all available Series
    /// </summary>
    public List<TvdbSeries> SeriesList
    {
      get { return m_seriesInfo; }
      set { m_seriesInfo = value; }
    }

    /// <summary>
    /// List of all available languages
    /// </summary>
    public List<TvdbLanguage> LanguageList
    {
      get { return m_langInfo; }
      set 
      { 
        m_langInfo = value;
        Util.LanguageList = value;
      }
    }

    /// <summary>
    /// List of all available mirrors
    /// </summary>
    public List<TvdbMirror> Mirrors
    {
      get { return m_mirrorInfo; }
      set { m_mirrorInfo = value; }
    }


  }
}
