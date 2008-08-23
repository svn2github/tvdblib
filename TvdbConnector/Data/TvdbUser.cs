using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TvdbConnector.Data
{

  [Serializable]
  public class TvdbUser
  {
    #region private properties
    private String m_userName;
    private String m_userIdentifier;
    private TvdbLanguage m_userPreferredLanguage;
    private List<int> m_userFavorites;
    #endregion

    /// <summary>
    /// TvdbUser constructor
    /// </summary>
    /// <param name="_username"></param>
    /// <param name="_userIdentifier"></param>
    public TvdbUser(String _username, String _userIdentifier)
    {
      m_userName = _username;
      m_userIdentifier = _userIdentifier;
    }

    /// <summary>
    /// Preferred language of the user
    /// </summary>
    public TvdbLanguage UserPreferredLanguage
    {
      get { return m_userPreferredLanguage; }
      set { m_userPreferredLanguage = value; }
    }

    /// <summary>
    /// This is the unique identifier assigned to every user. They can access this value by visiting the account settings page on the site. This is a 16 character alphanumeric string, but you should program your applications to handle id strings up to 32 characters in length. 
    /// </summary>
    public String UserIdentifier
    {
      get { return m_userIdentifier; }
      set { m_userIdentifier = value; }
    }

    /// <summary>
    /// Username
    /// </summary>
    public String UserName
    {
      get { return m_userName; }
      set { m_userName = value; }
    }

    /// <summary>
    /// List of user favorites
    /// </summary>
    public List<int> UserFavorites
    {
      get { return m_userFavorites; }
      set { m_userFavorites = value; }
    }
  }
}
