using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TvdbConnector.Data;

namespace TvdbConnector
{

  /// <summary>
  /// Information on server structure and mirrors of tvdb
  /// 
  /// 
  /// <mirrorpath>/api/<apikey>/
  /// |---- mirrors.xml
  /// |---- languages.xml
  /// |
  /// |---- series/
  /// |     |---- <seriesid>/
  /// |           |---- <language>.xml  (Base Series Record)
  /// |           |---- banners.xml  (All banners related to this series)
  /// |           |
  /// |           |---- all/
  /// |           |     |---- <language>.xml  (Full Series Record)
  /// |           |     |---- <language>.zip  (Zipped version of Full Series Record and banners.xml)
  /// |           |
  /// |           |---- default/  (sorts using the default ordering method)
  /// |           |     |---- <season#>/<episode#>/
  /// |           |           |---- <language>.xml  (Base Episode Record)
  /// |           |
  /// |           |---- dvd/  (sorts using the dvd ordering method)
  /// |           |     |---- <season#>/<episode#>/
  /// |           |           |---- <language>.xml  (Base Episode Record)
  /// |           |
  /// |           |---- absolute/  (sorts using the absolute ordering method)
  /// |                 |---- <absolute#>/
  /// |                   |---- <language>.xml  (Base Episode Record)
  /// |
  /// |---- episodes
  /// |     |---- <episodeid>/  (will return en.xml by default)
  /// |           |---- <language>.xml  (Base Episode Record)
  /// |
  /// |---- (updates)
  ///       |---- s<timeframe>.xml
  ///       |---- updates_<timeframe>.zip
  /// 
  /// </summary>
  internal class TvdbLinks
  {
    /// <summary>
    /// Base server where all operations start
    /// </summary>
    internal const String BASE_SERVER = "http://thetvdb.com/";

    /// <summary>
    /// Path of file where we get the available mirrors
    /// </summary>
    internal const String MIRROR_PATH = "/mirrors.xml";


    /// <summary>
    /// Path of file where we get the available languages
    /// </summary>
    internal const String LANG_PATH = "/languages.xml";

    /// <summary>
    /// Currently active mirror
    /// 
    /// TODO: should be choosen randomly, I don't care atm since
    /// there are no mirrors ;)
    /// </summary>
    internal static TvdbMirror ActiveMirror = null;

    

    internal static String CreateSeriesLink(String _apiKey, int _seriesId, TvdbLanguage _lang, bool _full, bool _zipped)
    {
      return TvdbLinks.ActiveMirror.MirrorPath + "/api/" + _apiKey + "/series/" + _seriesId + 
             (_full ? "/all/" : "/") + (_lang != null ? _lang.Abbriviation : "en") + ".xml";
    }

    internal static String CreateSeriesLinkZipped(String _apiKey, int _seriesId, TvdbLanguage _lang)
    {
      return TvdbLinks.ActiveMirror.MirrorPath + "/api/" + _apiKey + "/series/" + _seriesId +
                                    "/all/" + (_lang != null ? _lang.Abbriviation : "en") + ".zip";
    }

    internal static String CreateSeriesBannersLink(String _apiKey, int _seriesId)
    {
      return TvdbLinks.ActiveMirror.MirrorPath + "/api/" + _apiKey + "/series/" + _seriesId + "/banners.xml";
    }

    internal static String CreateSeriesEpisodesLink(String _apiKey, int _seriesId, TvdbLanguage _lang)
    {
      //this in fact returns the "full series page (http://thetvdb.com/wiki/index.php/API:Full_Series_Record)
      //which sucks because to retrieve all episodes I have to also download the series information (which isn't)
      //all that big on the other hand
      return TvdbLinks.ActiveMirror.MirrorPath + "/api/" + _apiKey + "/series/" + _seriesId + "/all/" + 
            (_lang != null ? _lang.Abbriviation : "en") + ".xml";
    }

    internal static String CreateEpisodeLink(string _apiKey, int _episodeId, TvdbLanguage _lang, bool p)
    {
      return TvdbLinks.ActiveMirror.MirrorPath + "/api/" + _apiKey + "/episodes/"
        + _episodeId + "/" + (_lang != null ? _lang.Abbriviation: "en") + ".xml";
    }

    internal static String CreateUpdateLink(string _apiKey, TvdbConnector.Util.UpdateInterval _interval, bool _zipped)
    {
      return TvdbLinks.ActiveMirror.MirrorPath + "/api/" + _apiKey + "/updates/updates_"
             + _interval.ToString() + (_zipped ? ".zip" : ".xml" );
    }

    internal static String CreateSearchLink(String _searchString)
    {
      return TvdbLinks.BASE_SERVER + "/api/GetSeries.php?seriesname=" + _searchString;
    }


    internal static string CreateBannerLink(string _bannerPath)
    {
      return TvdbLinks.BASE_SERVER + "/banners/" + _bannerPath;
    }

    internal static string CreateLanguageLink(string _apiKey)
    {
      return TvdbLinks.ActiveMirror.MirrorPath + "/api/" + _apiKey + "/languages.xml";
    }

    internal static String CreateUserLanguageLink(String _identifier)
    {
      return TvdbLinks.BASE_SERVER + "/api/User_PreferredLanguage.php?accountid=" + _identifier;
    }

    /// <summary>
    /// Creates link which (depending on params) gets user favorites, adds a series to user
    /// favorites or removes a series from the favorite lis
    /// </summary>
    /// <param name="_identifier"></param>
    /// <param name="_type"></param>
    /// <param name="_seriesId"></param>
    /// <returns></returns>
    internal static String CreateUserFavouriteLink(String _identifier, Util.UserFavouriteAction _type, int _seriesId)
    {
      return TvdbLinks.BASE_SERVER + "/api/User_Favorites.php?accountid=" + _identifier
        + ((_type == Util.UserFavouriteAction.none) ? "" : ("&type=" + _type.ToString() + "&seriesid=" + _seriesId));
    }
    /// <summary>
    /// Creates link which only retrieves the user favourites
    /// </summary>
    /// <param name="_identifier"></param>
    /// <returns></returns>
    internal static String CreateUserFavouriteLink(String _identifier)
    {
      return CreateUserFavouriteLink(_identifier, Util.UserFavouriteAction.none, 0);
    }

    #region Rating

    private static String CreateBasicRating(String _identifier)
    {
      return TvdbLinks.BASE_SERVER + "/api/User_Rating.php?accountid=" + _identifier;
    }

    internal static String CreateUserSeriesRating(String _identifier, int _seriesId)
    {
      return CreateBasicRating(_identifier) + "&itemtype=series&itemid=" + _seriesId;
    }

    internal static String CreateUserSeriesRating(String _identifier, int _seriesId, int _rating)
    {
      return CreateUserSeriesRating(_identifier, _seriesId) + "&rating=" + _rating;
    }

    internal static String CreateUserEpisodeRating(String _identifier, int _episodeId)
    {
      return CreateBasicRating(_identifier) + "&itemtype=episode&itemid=" + _episodeId;
    }

    internal static String CreateUserEpisodeRating(String _identifier, int _episodeId, int _rating)
    {
      return CreateUserEpisodeRating(_identifier, _episodeId) + "&rating=" + _rating;
    }

    #endregion

    /// <summary>
    /// Create link to get actor info
    /// </summary>
    /// <param name="_seriesId"></param>
    /// <param name="_apiKey"></param>
    /// <returns></returns>
    internal static String CreateActorLink(int _seriesId, String _apiKey)
    {
      return TvdbLinks.ActiveMirror.MirrorPath + "/api/" + _apiKey + "/series/" + _seriesId + "/actors.xml";
    }


  }
}
