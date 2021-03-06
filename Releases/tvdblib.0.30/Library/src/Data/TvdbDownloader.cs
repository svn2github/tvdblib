﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using TvdbConnector.Xml;

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
    internal DateTime DownloadUpdate(out List<TvdbSeries> updateSeries, out List<TvdbEpisode> updateEpisodes, Util.UpdateInterval _interval)
    {
      String xml = m_webClient.DownloadString(TvdbLinks.CreateUpdateLink(m_apiKey, Util.UpdateInterval.day));

      updateEpisodes = m_xmlHandler.ExtractEpisodeUpdates(xml);
      updateSeries = m_xmlHandler.ExtractSeriesUpdates(xml);

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
  }
}
