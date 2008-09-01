using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Net;
using System.IO;
using System.Threading;

namespace TvdbConnector.Data
{
  /// <summary>
  /// Tvdb Banners are the graphical element of tvdb. There are different types of banners which are
  /// representet by sub-classes in this library. These subclasses are:
  /// - TvdbEpisodeBanner: Each episode may contain a small image that should be an non-spoiler action 
  ///                      shot from the episode (http://thetvdb.com/wiki/index.php/Episode_Images)
  ///                      
  /// - TvdbFanartBanner: Fan Art is high quality artwork that is displayed in the background of HTPC menus 
  ///                     (http://thetvdb.com/wiki/index.php/Fan_Art)
  ///                     
  /// - TvdbSeasonBanner: Banner for each season of a series, dvd-style (400 x 578) or banner style (758 x 140)
  ///                     (http://thetvdb.com/wiki/index.php/Wide_Season_Banners)
  ///                     
  /// - TvdbSeriesBanner: Wide banner for each series (758 x 140), comes in graphical, text or blank style. For
  ///                     further information see http://thetvdb.com/wiki/index.php/Series_Banners
  ///                     
  /// - TvdbPosterBanner: Newest addition to the tvdb graphical section (680px x 1000px) and not smaller than 500k
  ///                     (http://thetvdb.com/wiki/index.php/Posters)
  /// </summary>
  [Serializable]
  public class TvdbBanner
  {
    #region private fields
    private String m_bannerPath;
    private Image m_banner;
    private bool m_isLoaded;
    private int m_id;
    private TvdbLanguage m_language;
    private bool m_bannerLoading = false;
    private System.Object m_bannerLoadingLock = new System.Object();
    private DateTime m_lastUpdated;
    private int m_seriesId;
    #endregion

    /// <summary>
    /// Language of the banner
    /// </summary>
    public TvdbLanguage Language
    {
      get { return m_language; }
      set { m_language = value; }
    }

    /// <summary>
    /// Id of the banner
    /// </summary>
    public int Id
    {
      get { return m_id; }
      set { m_id = value; }
    }

    /// <summary>
    /// Image data of the banner
    /// </summary>
    public Image Banner
    {
      get { return m_banner; }
      set { m_banner = value; }
    }

    /// <summary>
    /// True if the image data has been already loaded, false otherwise
    /// </summary>
    public bool IsLoaded
    {
      get { return m_isLoaded; }
    }

    /// <summary>
    /// Path to the location on the tvdb server where the image is located
    /// </summary>
    public String BannerPath
    {
      get { return m_bannerPath; }
      set { m_bannerPath = value; }
    }

    /// <summary>
    /// When was the banner updated the last time
    /// </summary>
    public DateTime LastUpdated
    {
      get { return m_lastUpdated; }
      set { m_lastUpdated = value; }
    }

    /// <summary>
    /// Id of the series this banner belongs to
    /// </summary>
    public int SeriesId
    {
      get { return m_seriesId; }
      set { m_seriesId = value; }
    }

    /// <summary>
    /// Loads the actual image data of the banner
    /// </summary>
    /// <returns>true if the banner could be loaded successfully, false otherwise</returns>
    public bool LoadBanner()
    {
      return LoadBanner(false);
    }

    /// <summary>
    /// Loads the actual image data of the banner
    /// </summary>
    /// <param name="_replaceOld">If true will replace an old image (if one exists already)</param>
    /// <returns>true if the banner could be loaded successfully, false otherwise</returns>
    public bool LoadBanner(bool _replaceOld)
    {
      bool wasLoaded = m_isLoaded;//is the banner already loaded at this point
      lock (m_bannerLoadingLock)
      {//if another thread is already loading THIS banner, the lock will block this thread until the other thread
        //has finished loading
        if (!wasLoaded && !_replaceOld && m_isLoaded)
        {////the banner has already been loaded from a different thread and we don't want to replace it
          return false;
        }

        m_bannerLoading = true;
        if (m_bannerPath.Equals("")) return false;
        try
        {
          Image img = LoadImage(TvdbLinks.CreateBannerLink(m_bannerPath));
          Thread.Sleep(2000);
          if (img != null)
          {
            m_banner = img;
            m_isLoaded = true;
            m_bannerLoading = false;
            return true;
          }
        }
        catch (WebException ex)
        {
          Log.Error("Couldn't load banner " + m_bannerPath, ex);
        }
        m_isLoaded = false;
        m_bannerLoading = false;
        return false;
      }
    }



    /// <summary>
    /// Loads the banner with the given image
    /// </summary>
    /// <param name="_img"></param>
    /// <returns></returns>
    public bool LoadBanner(Image _img)
    {
      if (_img != null)
      {
        m_banner = _img;
        m_isLoaded = true;
        return true;
      }
      else
      {
        m_isLoaded = false;
        return false;
      }
    }

    /// <summary>
    /// Loads the image from the given path
    /// </summary>
    /// <param name="_path"></param>
    /// <returns></returns>
    protected Image LoadImage(String _path)
    {
      try
      {
        WebClient client = new WebClient();
        byte[] imgData = client.DownloadData(_path);
        MemoryStream ms = new MemoryStream(imgData);
        Image img = Image.FromStream(ms, true, true);

        return img;
      }
      catch (Exception ex)
      {
        Log.Error("Error while loading image ", ex);
        return null;
      }
    }
  }
}
