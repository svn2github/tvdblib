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
    public static TvdbLanguage DefaultLanguage = new TvdbLanguage(7, "English", "en");

    private String m_name;
    private String m_abbriviation;
    private int m_id;

    public TvdbLanguage():this(-99, "", "")
    {

    }

    public TvdbLanguage(int _id, String _name, String _abbr)
    {
      m_id = _id;
      m_name = _name;
      m_abbriviation = _abbr;
    }

    public int Id
    {
      get { return m_id; }
      set { m_id = value; }
    }

    public String Abbriviation
    {
      get { return m_abbriviation; }
      set { m_abbriviation = value; }
    }

    public String Name
    {
      get { return m_name; }
      set { m_name = value; }
    }

    public override string ToString()
    {
      return m_name + "(" + m_abbriviation + ")";
      //return base.ToString();
    }
  }
}
