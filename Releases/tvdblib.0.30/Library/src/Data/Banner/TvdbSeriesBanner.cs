using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TvdbConnector.Data.Banner
{
  /// <summary>
  /// Graphical representation of a series, tpyes are text, graphical or blank
  /// - Graphical Banners are defined as having a graphical/logo version of the series name 
  /// - Text Banners generally use Arial Bold font, 27pt as the text 
  /// - The main requirement for blank banners is they should be blank on the left side of the banner as 
  ///   that is where the auto-generated text will be placed
  ///   
  /// More information on http://thetvdb.com/wiki/index.php/Series_Banners
  /// </summary>
  [Serializable]
  public class TvdbSeriesBanner: TvdbBanner
  {
    #region private fields
    private Type m_bannerType;
    #endregion

    /// <summary>
    /// Type of the series banner
    /// </summary>
    public enum Type { text, graphical, blank, none };

    /// <summary>
    /// TvdbSeriesBanner constructor
    /// </summary>
    public TvdbSeriesBanner()
    {

    }

    /// <summary>
    /// TvdbSeriesBanner constructor
    /// </summary>
    /// <param name="_id"></param>
    /// <param name="_path"></param>
    /// <param name="_lang"></param>
    /// <param name="_type"></param>
    public TvdbSeriesBanner(int _id, String _path, TvdbLanguage _lang, Type _type)
    {
      this.BannerPath = _path;
      this.Language = _lang;
      this.Id = _id;
      this.BannerType = _type;
    }

    /// <summary>
    /// Banner type of the series banner
    /// </summary>
    public Type BannerType
    {
      get { return m_bannerType; }
      set { m_bannerType = value; }
    }
  }
}
