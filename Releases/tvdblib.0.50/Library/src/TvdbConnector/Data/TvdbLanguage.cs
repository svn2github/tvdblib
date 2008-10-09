using System;
using System.Collections.Generic;
using System.Text;

namespace TvdbConnector.Data
{
  /// <summary>
  /// Baseclass for a tvdb language
  /// </summary>
  [Serializable]
  public class TvdbLanguage
  {
    /// <summary>
    /// The default language (which is English)
    /// Id:           7
    /// Abbriviation: en
    /// Name:         English
    /// 
    /// </summary>
    public static TvdbLanguage DefaultLanguage = new TvdbLanguage(7, "English", "en");

    /// <summary>
    /// language valid for all available languages
    /// Id:           7
    /// Abbriviation: en
    /// Name:         English
    /// 
    /// </summary>
    public static TvdbLanguage UniversalLanguage = new TvdbLanguage(99, "Universal", "all");

    #region private properties
    private String m_name;
    private String m_abbriviation;
    private int m_id;
    #endregion

    /// <summary>
    /// TvdbLanguage constructor
    /// </summary>
    public TvdbLanguage():this(-99, "", "")
    {

    }

    /// <summary>
    /// TvdbLanguage constructor
    /// </summary>
    /// <param name="_id"></param>
    /// <param name="_name"></param>
    /// <param name="_abbr"></param>
    public TvdbLanguage(int _id, String _name, String _abbr)
    {
      m_id = _id;
      m_name = _name;
      m_abbriviation = _abbr;
    }

    /// <summary>
    /// Id of the language
    /// </summary>
    public int Id
    {
      get { return m_id; }
      set { m_id = value; }
    }

    /// <summary>
    /// Abbriviation of the series
    /// </summary>
    public String Abbriviation
    {
      get { return m_abbriviation; }
      set { m_abbriviation = value; }
    }

    /// <summary>
    /// Name of the series
    /// </summary>
    public String Name
    {
      get { return m_name; }
      set { m_name = value; }
    }

    /// <summary>
    /// Returns String that describes the language in the format "Name (Abbriviation)"
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      return m_name + "(" + m_abbriviation + ")";
      //return base.ToString();
    }
  }
}
