using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TvdbConnector.Data.Banner
{
  /// <summary>
  /// Season bannners for each season of a series come in poster format (400 x 578) and wide format(758 x 140)
  /// - Wide format: http://thetvdb.com/wiki/index.php/Wide_Season_Banners
  /// - Poster format: http://thetvdb.com/wiki/index.php/Season_Banners
  /// </summary>
  [Serializable]
  public class TvdbSeasonBanner: TvdbBanner
  {
    public enum Type  { season = 0, seasonwide = 1 , none = 2};

    private Type m_bannerType;
    private int m_season;

    public int Season
    {
      get { return m_season; }
      set { m_season = value; }
    }

    public Type BannerType
    {
      get { return m_bannerType; }
      set { m_bannerType = value; }
    }
  }
}
