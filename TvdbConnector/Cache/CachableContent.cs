using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TvdbConnector.Data;

namespace TvdbConnector.Cache
{
  [Serializable]
  public class CachableContent
  {
    private List<TvdbSeries> m_seriesInfo;
    private List<TvdbLanguage> m_langInfo;
    private List<TvdbMirror> m_mirrorInfo;
    private TvdbSettings m_settings;

    public TvdbSettings Settings
    {
      get { return m_settings; }
      set { m_settings = value; }
    }

    public List<TvdbSeries> SeriesInfo
    {
      get { return m_seriesInfo;}
    }

    public List<TvdbLanguage> Language
    {
      get { return m_langInfo; }
    }

    public List<TvdbMirror> Mirrors
    {
      get { return m_mirrorInfo; }
    }

    public CachableContent(List<TvdbSeries> _seriesInfo, List<TvdbLanguage> _language, List<TvdbMirror> _mirrors, TvdbSettings _settings)
    {
      m_seriesInfo = _seriesInfo;
      m_langInfo = _language;
      m_mirrorInfo = _mirrors;
      m_settings = _settings;
    }
  }
}
