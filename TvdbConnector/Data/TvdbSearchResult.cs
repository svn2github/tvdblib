using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TvdbConnector.Data.Banner;

namespace TvdbConnector.Data
{
  /// <summary>
  /// Class representing the result of a tvdb name query -> for further information
  /// visit http://thetvdb.com/wiki/index.php/API:GetSeries
  /// </summary>
  public class TvdbSearchResult
  {
    public TvdbSearchResult()
    {

    }

    public TvdbSearchResult(int _id)
    {
      m_id = _id;
    }

    #region tvdb properties
    private int m_id;
    public int Id
    {
      get { return m_id; }
      set { m_id = value; }
    }

    private String m_seriesName;
    public String SeriesName
    {
      get { return m_seriesName; }
      set { m_seriesName = value; }
    }

    private DateTime m_firstAired;
    public DateTime FirstAired
    {
      get { return m_firstAired; }
      set { m_firstAired = value; }
    }

    private TvdbLanguage m_language;
    public TvdbLanguage Language
    {
      get { return m_language; }
      set { m_language = value; }
    }

    private String m_overview;
    public String Overview
    {
      get { return m_overview; }
      set { m_overview = value; }
    }

    private TvdbSeriesBanner m_banner;
    public TvdbSeriesBanner Banner
    {
      get { return m_banner; }
      set { m_banner = value; }
    }

    private String m_imdbId;
    public String ImdbId
    {
      get { return m_imdbId;}
      set { m_imdbId = value; }
    }

    #endregion
  }
}
