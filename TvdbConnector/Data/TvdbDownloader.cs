using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace TvdbConnector.Data
{
  internal class TvdbDownloader
  {
    private String m_apiKey;
    private WebClient m_webClient;
    XmlHandler m_xmlHandler;

    internal TvdbDownloader(String _apiKey)
    {
      m_apiKey = _apiKey;
      m_webClient = new WebClient();//initialise webclient for downloading xml files
      m_xmlHandler = new XmlHandler();//xml handler (extract xml information into objects)
    }

    internal List<TvdbEpisode> DownloadEpisodes(int _seriesId, TvdbLanguage _language)
    {
      String xml = m_webClient.DownloadString(TvdbLinks.CreateSeriesEpisodesLink(m_apiKey, _seriesId, _language));
      List<TvdbEpisode> epList = m_xmlHandler.ExtractEpisodes(xml);

      return epList;
    }

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
    internal TvdbSeries DownloadSeries(int _seriesId, TvdbLanguage _language, bool _full, bool _loadBanners)
    {
      //download the xml data from this request
      String data;
      try
      {
        data = m_webClient.DownloadString(TvdbLinks.CreateSeriesLink(m_apiKey, _seriesId, _language, _full, false));
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
        if (_full)
        {
          //add episode info to series
          List<TvdbEpisode> epList = m_xmlHandler.ExtractEpisodes(data);
          foreach (TvdbEpisode e in epList)
          {
            Util.AddEpisodeToSeries(e, series);
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

    internal List<int> DownloadUserFavouriteList(String _userId)
    {
      String xml = m_webClient.DownloadString(TvdbLinks.CreateUserFavouriteLink(_userId));
      List<int> userLang = m_xmlHandler.ExtractSeriesFavourites(xml);
      return userLang;
    }

    internal DateTime DownloadUpdate(out List<TvdbSeries> updateSeries, out List<TvdbEpisode> updateEpisodes, Util.UpdateInterval _interval)
    {
      String xml = m_webClient.DownloadString(TvdbLinks.CreateUpdateLink(m_apiKey, Util.UpdateInterval.day));

      updateEpisodes = m_xmlHandler.ExtractEpisodeUpdates(xml);
      updateSeries = m_xmlHandler.ExtractSeriesUpdates(xml);

      return m_xmlHandler.ExtractUpdateTime(xml);
    }

    internal List<TvdbLanguage> DownloadLanguages()
    {
      XmlHandler hand = new XmlHandler();
      String xml = m_webClient.DownloadString(TvdbLinks.CreateLanguageLink(m_apiKey));
      return hand.ExtractLanguages(xml);
    }

    internal List<TvdbSearchResult> DownloadSearchResults(String _name)
    {
      String xml = m_webClient.DownloadString(TvdbLinks.CreateSearchLink(_name));
      return m_xmlHandler.ExtractSeriesSearchResults(xml);
    }
  }
}
