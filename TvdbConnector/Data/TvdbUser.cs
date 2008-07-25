using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TvdbConnector.Data
{
  public class TvdbUser
  {
    private String m_userName;
    private String m_userIdentifier;
    private TvdbLanguage m_userPreferredLanguage;


    public TvdbUser(String _username, String _userIdentifier)
    {
      m_userName = _username;
      m_userIdentifier = _userIdentifier;
    }

    public TvdbLanguage UserPreferredLanguage
    {
      get { return m_userPreferredLanguage; }
      set { m_userPreferredLanguage = value; }
    }

    public String UserIdentifier
    {
      get { return m_userIdentifier; }
      set { m_userIdentifier = value; }
    }

    public String UserName
    {
      get { return m_userName; }
      set { m_userName = value; }
    }
  }
}
