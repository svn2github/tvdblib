using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TvdbConnector.Data
{
  /// <summary>
  /// Represents a rating entry from thetvdb
  /// </summary>
  public class TvdbRating
  {
    #region private properties
    private int m_userRating;
    private double m_communityRating;
    private ItemType m_ratingItemType;
    #endregion

    /// <summary>
    /// Enum with all items on thetvdb that can be rated
    /// </summary>
    public enum ItemType { Series, Episode }

    /// <summary>
    /// Which item type is this rating for
    /// </summary>
    public ItemType RatingItemType
    {
      get { return m_ratingItemType; }
      set { m_ratingItemType = value; }
    }

    /// <summary>
    /// Community Rating is a double value from 0 to 10 and is the mean value of all user ratings for this item
    /// </summary>
    public double CommunityRating
    {
      get { return m_communityRating; }
      set { m_communityRating = value; }
    }

    /// <summary>
    /// The rating from this user
    /// </summary>
    public int UserRating
    {
      get { return m_userRating; }
      set { m_userRating = value; }
    }
  }
}
