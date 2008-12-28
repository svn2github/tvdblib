using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TvdbConnector.Data.Banner
{
  /// <summary>
  /// Represents the episode banner, which is currently only one image
  /// per episode (no language differentiation either) limited to a maximum 
  /// size of 400 x 300 
  /// 
  /// further information on http://thetvdb.com/wiki/index.php/Episode_Images
  /// </summary>
  [Serializable]
  public class TvdbEpisodeBanner: TvdbBannerWithThumb
  {
    /// <summary>
    /// TvdbEpisodeBanner constructor
    /// </summary>
    public TvdbEpisodeBanner()
      : base()
    {
      this.Language = new TvdbLanguage(-99, "Universal, valid for all languages", "all");
    }

    /// <summary>
    /// TvdbEpisodeBanner constructor
    /// </summary>
    public TvdbEpisodeBanner(int _id, String _bannerPath):this()
    {
      Id = _id;
      BannerPath = _bannerPath;
    }
  }
}
