using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TvdbConnector.Data;

namespace TvdbConnector.Cache
{
  public static class TvdbCache
  {
    private static List<TvdbLanguage> m_languageList = new List<TvdbLanguage>();
    public static List<TvdbLanguage> LanguageList
    {
      get { return m_languageList; }
      set { m_languageList = value; }
    }

    private static List<TvdbSeries> m_seriesList = new List<TvdbSeries>();
    public static List<TvdbSeries> SeriesList
    {
      get { return TvdbCache.m_seriesList; }
      set { TvdbCache.m_seriesList = value; }
    }
  }
}
