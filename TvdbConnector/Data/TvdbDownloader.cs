using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using TvdbConnector.Xml;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;

namespace TvdbConnector.Data
{
  internal class TvdbDownloader
  {
    #region private properties
    private String m_apiKey;
    private WebClient m_webClient;
    private TvdbXmlReader m_xmlHandler;
    #endregion

    /// <summary>
    /// TvdbDownloader constructor
    /// </summary>
    /// <param name="_apiKey"></param>
    internal TvdbDownloader(String _apiKey)
    {
      m_apiKey = _apiKey;
      m_webClient = new WebClient();//initialise webclient for downloading xml files
      m_webClient.Encoding = Encoding.UTF8;
      m_xmlHandler = new TvdbXmlReader();//xml handler (extract xml information into objects)
    }

    /// <summary>
    /// Download the episodes
    /// </summary>
    /// <param name="_seriesId"></param>
    /// <param name="_language"></param>
    /// <returns></returns>
    internal List<TvdbEpisode> DownloadEpisodes(int _seriesId, TvdbLanguage _language)
    {
      String xml = m_webClient.DownloadString(TvdbLinks.CreateSeriesEpisodesLink(m_apiKey, _seriesId, _language));
      List<TvdbEpisode> epList = m_xmlHandler.ExtractEpisodes(xml);

      return epList;
    }

    /// <summary>
    /// Download the banners
    /// </summary>
    /// <param name="_seriesId"></param>
    /// <returns></returns>
    internal List<TvdbBanner> DownloadBanners(int _seriesId)
    {
      String xml = m_webClient.DownloadString(TvdbLinks.CreateSeriesBannersLink(m_apiKey, _seriesId));
      List<TvdbBanner> banners = m_xmlHandler.ExtractBanners(xml);
      return banners;
    }

    /// <summary>
    /// Download series from tvdb
    /// </summary>
    /// <param name="series"></param>
    /// <returns></returns>
    internal TvdbSeries DownloadSeries(int _seriesId, TvdbLanguage _language, bool _loadEpisodes, bool _loadActors, bool _loadBanners)
    {
      //download the xml data from this request
      String data;
      try
      {
        data = m_webClient.DownloadString(TvdbLinks.CreateSeriesLink(m_apiKey, _seriesId, _language, _loadEpisodes, false));
      }
      catch (Exception ex)
      {
        Log.Warn("Request not successfull", ex);
        return null;
      }
      //extract all series the xml file contains
      List<TvdbSeries> seriesList = m_xmlHandler.ExtractSeries(data);

      //if a request is made on a series id, one and only one result 
      //should be returned, otherwise there obviously was an error
      if (seriesList != null && seriesList.Count == 1)
      {
        TvdbSeries series = seriesList[0];
        if (_loadEpisodes)
        {
          //add episode info to series
          List<TvdbEpisode> epList = m_xmlHandler.ExtractEpisodes(data);
          foreach (TvdbEpisode e in epList)
          {
            Util.AddEpisodeToSeries(e, series);
          }
        }

        //also load actors
        if (_loadActors)
        {
          List<TvdbActor> list = DownloadActors(_seriesId);
          if (list != null)
          {
            series.TvdbActors = list;
          }
        }

        //also load banner paths
        if (_loadBanners)
        {
          List<TvdbBanner> banners = DownloadBanners(_seriesId);
          if (banners != null)
          {
            series.Banners = banners;
          }
        }
        return series;
      }
      else
      {
        Log.Warn("More than one series returned when trying to retrieve series " + _seriesId);
        return null;
      }
    }

    internal TvdbSeries DownloadSeriesZipped(int _seriesId, TvdbLanguage _language)
    {
      //download the xml data from this request
      byte[] data;
      try
      {
        data = m_webClient.DownloadData(TvdbLinks.CreateSeriesLinkZipped(m_apiKey, _seriesId, _language));
      }
      catch (Exception ex)
      {
        Log.Warn("Request not successfull", ex);
        return null;
      }

      ZipInputStream zip = new ZipInputStream(new MemoryStream(data));

      ZipEntry entry = zip.GetNextEntry();
      String seriesString = null;
      String actorsString = null;
      String bannersString = null;
      while (entry != null)
      {
        Log.Debug("Extracting " + entry.Name);
        byte[] buffer = new byte[zip.Length];
        int count = zip.Read(buffer, 0, (int)zip.Length);
        if (entry.Name.Equals(_language.Abbriviation + ".xml"))
        {
          seriesString = Encoding.UTF8.GetString(buffer);
        }
        else if (entry.Name.Equals("banners.xml"))
        {
          bannersString = Encoding.UTF8.GetString(buffer);
        }
        else if (entry.Name.Equals("actors.xml"))
        {
          actorsString = Encoding.UTF8.GetString(buffer);
        }
        entry = zip.GetNextEntry();
      }
      zip.Close();

      //extract all series the xml file contains
      List<TvdbSeries> seriesList = m_xmlHandler.ExtractSeries(seriesString);

      //if a request is made on a series id, one and only one result 
      //should be returned, otherwise there obviously was an error
      if (seriesList != null && seriesList.Count == 1)
      {
        TvdbSeries series = seriesList[0];

        //add episode info to series
        List<TvdbEpisode> epList = m_xmlHandler.ExtractEpisodes(seriesString);
        foreach (TvdbEpisode e in epList)
        {
          Util.AddEpisodeToSeries(e, series);
        }

        //also load actors
        List<TvdbActor> actors = m_xmlHandler.ExtractActors(actorsString);
        if (actors != null)
        {
          series.TvdbActors = actors;
        }

        //also load banner paths
        List<TvdbBanner> banners = m_xmlHandler.ExtractBanners(bannersString);
        if (banners != null)
        {
          series.Banners = banners;
        }

        return series;
      }
      else
      {
        Log.Warn("More than one series returned when trying to retrieve series " + _seriesId);
        return null;
      }
    }

    internal TvdbSeriesFields DownloadSeriesFields(int _seriesId, TvdbLanguage _language)
    {
      String data;
      try
      {
        data = m_webClient.DownloadString(TvdbLinks.CreateSeriesLink(m_apiKey, _seriesId, _language, false, false));
      }
      catch (Exception ex)
      {
        Log.Warn("Request not successfull", ex);
        return null;
      }
      //extract all series the xml file contains
      List<TvdbSeriesFields> seriesList = m_xmlHandler.ExtractSeriesFields(data);

      if (seriesList != null && seriesList.Count == 1)
      {
        return seriesList[0];
      }
      else
      {
        return null;
      }
    }

    /// <summary>
    /// Download the given episode from tvdb
    /// </summary>
    /// <param name="_episodeId"></param>
    /// <param name="_language"></param>
    /// <returns></returns>
    internal TvdbEpisode DownloadEpisode(int _episodeId, TvdbLanguage _language)
    {
      String data;
      try
      {
        data = m_webClient.DownloadString(TvdbLinks.CreateEpisodeLink(m_apiKey, _episodeId, _language, false));
      }
      catch (Exception ex)
      {
        Log.Warn("Request not successfull", ex);
        return null;
      }
      List<TvdbEpisode> epList = m_xmlHandler.ExtractEpisodes(data);

      if (epList.Count == 1)
      {
        return epList[0];
      }
      else
      {
        return null;
      }
    }

    /// <summary>
    /// Download the preferred language of the user
    /// </summary>
    /// <param name="_userId"></param>
    /// <returns></returns>
    internal TvdbLanguage DownloadUserPreferredLanguage(String _userId)
    {
      String xml = m_webClient.DownloadString(TvdbLinks.CreateUserLanguageLink(_userId));
      List<TvdbLanguage> langList = m_xmlHandler.ExtractLanguages(xml);
      if (langList != null && langList.Count == 1)
      {
        return langList[0];
      }
      return null;
    }

    /// <summary>
    /// Download the user favorite list
    /// </summary>
    /// <param name="_userId"></param>
    /// <returns></returns>
    internal List<int> DownloadUserFavoriteList(String _userId)
    {
      return DownloadUserFavoriteList(_userId, Util.UserFavouriteAction.none, 0);
    }

    /// <summary>
    /// Download the user favorite list
    /// </summary>
    /// <param name="_userId"></param>
    /// <param name="_type"></param>
    /// <param name="_seriesId"></param>
    /// <returns></returns>
    internal List<int> DownloadUserFavoriteList(String _userId, Util.UserFavouriteAction _type, int _seriesId)
    {
      String xml = m_webClient.DownloadString(TvdbLinks.CreateUserFavouriteLink(_userId, _type, _seriesId));
      List<int> favList = m_xmlHandler.ExtractSeriesFavorites(xml);
      return favList;
    }

    /// <summary>
    /// Download an Update
    /// </summary>
    /// <param name="updateSeries"></param>
    /// <param name="updateEpisodes"></param>
    /// <param name="_interval"></param>
    /// <returns></returns>
    internal DateTime DownloadUpdate(out List<TvdbSeries> _updateSeries, out List<TvdbEpisode> _updateEpisodes,
                                     out List<TvdbBanner> _updateBanners, Util.UpdateInterval _interval, bool _zipped)
    {

      String xml = null;
      if (_zipped)
      {
        byte[] data = m_webClient.DownloadData(TvdbLinks.CreateUpdateLink(m_apiKey, _interval, _zipped));
        ZipInputStream zip = new ZipInputStream(new MemoryStream(data));
        zip.GetNextEntry();
        byte[] buffer = new byte[zip.Length];
        int count = zip.Read(buffer, 0, (int)zip.Length);
        xml = Encoding.UTF8.GetString(buffer);
      }
      else
      {
        xml = m_webClient.DownloadString(TvdbLinks.CreateUpdateLink(m_apiKey, _interval, _zipped));
      }

      _updateEpisodes = m_xmlHandler.ExtractEpisodeUpdates(xml);
      _updateSeries = m_xmlHandler.ExtractSeriesUpdates(xml);
      _updateBanners = m_xmlHandler.ExtractBannerUpdates(xml);

      return m_xmlHandler.ExtractUpdateTime(xml);
    }

    /// <summary>
    /// Download available languages
    /// </summary>
    /// <returns></returns>
    internal List<TvdbLanguage> DownloadLanguages()
    {
      TvdbXmlReader hand = new TvdbXmlReader();
      String xml = m_webClient.DownloadString(TvdbLinks.CreateLanguageLink(m_apiKey));
      return hand.ExtractLanguages(xml);
    }

    /// <summary>
    /// Download search results
    /// </summary>
    /// <param name="_name"></param>
    /// <returns></returns>
    internal List<TvdbSearchResult> DownloadSearchResults(String _name)
    {
      String xml = m_webClient.DownloadString(TvdbLinks.CreateSearchLink(_name));
      return m_xmlHandler.ExtractSeriesSearchResults(xml);
    }

    /// <summary>
    /// Make the request for rating a series
    /// </summary>
    /// <param name="_userId"></param>
    /// <param name="_seriesId"></param>
    /// <param name="_rating"></param>
    /// <returns></returns>
    internal double RateSeries(String _userId, int _seriesId, int _rating)
    {
      String xml = m_webClient.DownloadString(TvdbLinks.CreateUserSeriesRating(_userId, _seriesId, _rating));
      return m_xmlHandler.ExtractRating(xml);
    }

    /// <summary>
    /// Make the request for rating an episode
    /// </summary>
    /// <param name="_userId"></param>
    /// <param name="_episodeId"></param>
    /// <param name="_rating"></param>
    /// <returns></returns>
    internal double RateEpisode(String _userId, int _episodeId, int _rating)
    {
      String xml = m_webClient.DownloadString(TvdbLinks.CreateUserEpisodeRating(_userId, _episodeId, _rating));
      return m_xmlHandler.ExtractRating(xml);
    }

    /// <summary>
    /// Download the series rating without doing a rating
    /// </summary>
    /// <param name="_userId"></param>
    /// <param name="_seriesId"></param>
    /// <returns></returns>
    internal double DownloadSeriesRating(String _userId, int _seriesId)
    {
      String xml = m_webClient.DownloadString(TvdbLinks.CreateUserSeriesRating(_userId, _seriesId));
      return m_xmlHandler.ExtractRating(xml);
    }

    /// <summary>
    /// Download the episode rating without rating 
    /// </summary>
    /// <param name="_userId"></param>
    /// <param name="_seriesId"></param>
    /// <returns></returns>
    internal double DownloadEpisodeRating(String _userId, int _seriesId)
    {
      String xml = m_webClient.DownloadString(TvdbLinks.CreateUserEpisodeRating(_userId, _seriesId));
      return m_xmlHandler.ExtractRating(xml);
    }

    /// <summary>
    /// Download the list of actors
    /// </summary>
    /// <param name="_seriesId"></param>
    /// <returns></returns>
    internal List<TvdbActor> DownloadActors(int _seriesId)
    {
      String xml = m_webClient.DownloadString(TvdbLinks.CreateActorLink(_seriesId, m_apiKey));
      return m_xmlHandler.ExtractActors(xml);
    }

    internal List<TvdbMirror> DownloadMirrorList()
    {
      String xml = m_webClient.DownloadString(TvdbLinks.CreateMirrorsLink(m_apiKey));
      List<TvdbMirror> list = m_xmlHandler.ExtractMirrors(xml);
      return list;
    }
  }
}
