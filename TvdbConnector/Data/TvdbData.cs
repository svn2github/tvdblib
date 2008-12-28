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
