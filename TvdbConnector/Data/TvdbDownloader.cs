/*
 *   TvdbLib: A library to retrieve information and media from http://thetvdb.com
 * 
 *   Copyright (C) 2008  Benjamin Gmeiner
 * 
 *   This program is free software: you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation, either version 3 of the License, or
 *   (at your option) any later version.
 *
 *   This program is distributed in the hope that it will be useful,
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *   GNU General Public License for more details.
 *
 *   You should have received a copy of the GNU General Public License
 *   along with this program.  If not, see <http://www.gnu.org/licenses/>.
 * 
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;

using TvdbConnector.Exceptions;
using TvdbConnector.ICSharpCode.SharpZipLib.Zip;
using TvdbConnector.SharpZipLib.Zip;
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
      String xml = "";
      String link = "";
      try
      {
        link = TvdbLinks.CreateSeriesEpisodesLink(m_apiKey, _seriesId, _language);
        xml = m_webClient.DownloadString(link);
        List<TvdbEpisode> epList = m_xmlHandler.ExtractEpisodes(xml);
        return epList;
      }
      catch (XmlException ex)
      {
        Log.Error("Error parsing the xml file " + link + "\n\n" + xml, ex);
        throw new TvdbInvalidXmlException("Error parsing the xml file " + link + "\n\n" + xml);
      }
      catch (WebException ex)
      {
        Log.Warn("Request not successfull", ex);
        if (ex.Message.Equals("The remote server returned an error: (404) Not Found."))
        {
          throw new TvdbInvalidApiKeyException("Couldn't connect to Thetvdb.com to retrieve episodes fo " + _seriesId +
                                               ", you may use an invalid api key  or the series doesn't exists");
        }
        else
        {
          throw new TvdbNotAvailableException("Couldn't connect to Thetvdb.com to retrieve episodes for" + _seriesId +
                                              ", check your internet connection and the status of http://thetvdb.com");
        }
      }



    }

    /// <summary>
    /// Download the banners
    /// </summary>
    /// <param name="_seriesId"></param>
    /// <returns></returns>
    internal List<TvdbBanner> DownloadBanners(int _seriesId)
    {
      String xml = "";
      String link = "";
      try
      {
        link = TvdbLinks.CreateSeriesBannersLink(m_apiKey, _seriesId);
        xml = m_webClient.DownloadString(link);
        List<TvdbBanner> banners = m_xmlHandler.ExtractBanners(xml);
        return banners;
      }
      catch (XmlException ex)
      {
        Log.Error("Error parsing the xml file " + link + "\n\n" + xml, ex);
        throw new TvdbInvalidXmlException("Error parsing the xml file " + link + "\n\n" + xml);
      }
      catch (WebException ex)
      {
        Log.Warn("Request not successfull", ex);
        if (ex.Message.Equals("The remote server returned an error: (404) Not Found."))
        {
          throw new TvdbInvalidApiKeyException("Couldn't connect to Thetvdb.com to retrieve banners fo " + _seriesId +
                                               ", you may use an invalid api key  or the series doesn't exists");
        }
        else
        {
          throw new TvdbNotAvailableException("Couldn't connect to Thetvdb.com to retrieve banners for" + _seriesId +
                                              ", check your internet connection and the status of http://thetvdb.com");
        }
      }
    }

    /// <summary>
    /// Download series from tvdb
    /// </summary>
    /// <param name="_seriesId">id of series</param>
    /// <param name="_language">language of series</param>
    /// <param name="_loadEpisodes">load episodes</param>
    /// <param name="_loadActors">load actors</param>
    /// <param name="_loadBanners">load banners</param>
    /// <returns></returns>
    internal TvdbSeries DownloadSeries(int _seriesId, TvdbLanguage _language, bool _loadEpisodes, bool _loadActors, bool _loadBanners)
    {
      //download the xml data from this request
      String xml = "";
      String link = "";
      try
      {
        link = TvdbLinks.CreateSeriesLink(m_apiKey, _seriesId, _language, _loadEpisodes, false);
        xml = m_webClient.DownloadString(link);

        //extract all series the xml file contains
        List<TvdbSeries> seriesList = m_xmlHandler.ExtractSeries(xml);

        //if a request is made on a series id, one and only one result
        //should be returned, otherwise there obviously was an error
        if (seriesList != null && seriesList.Count == 1)
        {
          TvdbSeries series = seriesList[0];
          if (_loadEpisodes)
          {
            //add episode info to series
            List<TvdbEpisode> epList = m_xmlHandler.ExtractEpisodes(xml);
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
      catch (XmlException ex)
      {
        Log.Error("Error parsing the xml file " + link + "\n\n" + xml, ex);
        throw new TvdbInvalidXmlException("Error parsing the xml file " + link + "\n\n" + xml);
      }
      catch (WebException ex)
      {
        Log.Warn("Request not successfull", ex);
        if (ex.Message.Equals("The remote server returned an error: (404) Not Found."))
        {
          throw new TvdbInvalidApiKeyException("Couldn't connect to Thetvdb.com to retrieve " + _seriesId +
                                               ", you may use an invalid api key  or the series doesn't exists");
        }
        else
        {
          throw new TvdbNotAvailableException("Couldn't connect to Thetvdb.com to retrieve " + _seriesId +
                                              ", check your internet connection and the status of http://thetvdb.com");
        }
      }
    }

    internal TvdbSeries DownloadSeriesZipped(int _seriesId, TvdbLanguage _language)
    {
      //download the xml data from this request
      byte[] xml = null;
      String link = "";
      try
      {
        link = TvdbLinks.CreateSeriesLinkZipped(m_apiKey, _seriesId, _language);
        xml = m_webClient.DownloadData(link);

        ZipInputStream zip = new ZipInputStream(new MemoryStream(xml));

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
          series.EpisodesLoaded = true;
          series.Episodes = new List<TvdbEpisode>();
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
            series.TvdbActorsLoaded = true;
            series.TvdbActors = actors;
          }

          //also load banner paths
          List<TvdbBanner> banners = m_xmlHandler.ExtractBanners(bannersString);
          if (banners != null)
          {
            series.BannersLoaded = true;
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
      catch (XmlException ex)
      {
        Log.Error("Error parsing the xml file " + link + "\n\n" + Encoding.Unicode.GetString(xml), ex);
        throw new TvdbInvalidXmlException("Error parsing the xml file " + link + "\n\n" + xml);
      }
      catch (WebException ex)
      {
        Log.Warn("Request not successfull", ex);
        if (ex.Message.Equals("The remote server returned an error: (404) Not Found."))
        {
          throw new TvdbInvalidApiKeyException("Couldn't connect to Thetvdb.com to retrieve " + _seriesId +
                                               ", you may an invalid api key  or the series doesn't exists");
        }
        else
        {
          throw new TvdbNotAvailableException("Couldn't connect to Thetvdb.com to retrieve " + _seriesId +
                                              ", check your internet connection and the status of http://thetvdb.com");
        }
      }
    }

    internal TvdbSeriesFields DownloadSeriesFields(int _seriesId, TvdbLanguage _language)
    {
      String xml = "";
      String link = "";
      try
      {
        link = TvdbLinks.CreateSeriesLink(m_apiKey, _seriesId, _language, false, false);
        xml = m_webClient.DownloadString(link);

        //extract all series the xml file contains
        List<TvdbSeriesFields> seriesList = m_xmlHandler.ExtractSeriesFields(xml);

        if (seriesList != null && seriesList.Count == 1)
        {
          return seriesList[0];
        }
        else
        {
          return null;
        }
      }
      catch (XmlException ex)
      {
        Log.Error("Error parsing the xml file " + link + "\n\n" + xml, ex);
        throw new TvdbInvalidXmlException("Error parsing the xml file " + link + "\n\n" + xml);
      }
      catch (WebException ex)
      {
        Log.Warn("Request not successfull", ex);
        if (ex.Message.Equals("The remote server returned an error: (404) Not Found."))
        {
          throw new TvdbInvalidApiKeyException("Couldn't connect to Thetvdb.com to retrieve " + _seriesId +
                                               ", you may use an invalid api key or the series doesn't exists");
        }
        else
        {
          throw new TvdbNotAvailableException("Couldn't connect to Thetvdb.com to retrieve " + _seriesId +
                                              ", check your internet connection and the status of http://thetvdb.com");
        }
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
      String xml = "";
      String link = "";
      try
      {
        link = TvdbLinks.CreateEpisodeLink(m_apiKey, _episodeId, _language, false);
        xml = m_webClient.DownloadString(link);
        List<TvdbEpisode> epList = m_xmlHandler.ExtractEpisodes(xml);

        if (epList.Count == 1)
        {
          return epList[0];
        }
        else
        {
          return null;
        }
      }
      catch (XmlException ex)
      {
        Log.Error("Error parsing the xml file " + link + "\n\n" + xml, ex);
        throw new TvdbInvalidXmlException("Error parsing the xml file " + link + "\n\n" + xml);
      }
      catch (WebException ex)
      {
        Log.Warn("Request not successfull", ex);
        if (ex.Message.Equals("The remote server returned an error: (404) Not Found."))
        {
          throw new TvdbContentNotFoundException("Couldn't download episode " + _episodeId + "(" + _language.Abbriviation +
                                                 "), maybe the episode doesn't exist");
        }
        else
        {
          throw new TvdbNotAvailableException("Couldn't connect to Thetvdb.com to retrieve " + _episodeId +
                                              ", check your internet connection and the status of http://thetvdb.com");
        }
      }
    }

    /// <summary>
    /// Download the episode specified from http://thetvdb.com
    /// </summary>
    /// <param name="_seriesId">series id</param>
    /// <param name="_seasonNr">season nr</param>
    /// <param name="_episodeNr">episode nr</param>
    /// <param name="_language">language</param>
    /// <param name="_order">order</param>
    /// <returns></returns>
    internal TvdbEpisode DownloadEpisode(int _seriesId, int _seasonNr, int _episodeNr, String _order, TvdbLanguage _language)
    {
      String xml = "";
      String link = "";
      try
      {
        link = TvdbLinks.CreateEpisodeLink(m_apiKey, _seriesId, _seasonNr, _episodeNr, _order, _language);
        xml = m_webClient.DownloadString(link);
        List<TvdbEpisode> epList = m_xmlHandler.ExtractEpisodes(xml);
        if (epList.Count == 1)
        {
          return epList[0];
        }
        else
        {
          return null;
        }
      }
      catch (XmlException ex)
      {
        Log.Error("Error parsing the xml file " + link + "\n\n" + xml, ex);
        throw new TvdbInvalidXmlException("Error parsing the xml file " + link + "\n\n" + xml);
      }
      catch (WebException ex)
      {
        Log.Warn("Request not successfull", ex);
        if (ex.Message.Equals("The remote server returned an error: (404) Not Found."))
        {
          throw new TvdbContentNotFoundException("Couldn't download episode " + _seriesId + "/" +
                                                 _order + "/" + _seasonNr + "/" + _episodeNr + "/" + _language.Abbriviation +
                                                 ", maybe the episode doesn't exist");
        }
        else
        {
          throw new TvdbNotAvailableException("Couldn't connect to Thetvdb.com to retrieve " + _seriesId + "/" +
                                              _order + "/" + _seasonNr + "/" + _episodeNr + "/" + _language.Abbriviation +
                                              ", check your internet connection and the status of http://thetvdb.com");
        }
      }
    }


    /// <summary>
    /// Download the episode specified from http://thetvdb.com
    /// </summary>
    /// <param name="_seriesId">series id</param>
    /// <param name="_airDate">when did the episode air</param>
    /// <param name="_language">language</param>
    /// <returns>Episode</returns>
    internal TvdbEpisode DownloadEpisode(int _seriesId, DateTime _airDate, TvdbLanguage _language)
    {
      String xml = "";
      String link = "";
      try
      {
        link = TvdbLinks.CreateEpisodeLink(m_apiKey, _seriesId, _airDate, _language);
        xml = m_webClient.DownloadString(link);
        if (!xml.Contains("No Results from SP"))
        {
          List<TvdbEpisode> epList = m_xmlHandler.ExtractEpisodes(xml);
          if (epList.Count == 1)
          {
            return epList[0];
          }
          else
          {
            return null;
          }
        }
        else
        {
          return null;
        }
      }
      catch (XmlException ex)
      {
        Log.Error("Error parsing the xml file " + link + "\n\n" + xml, ex);
        throw new TvdbInvalidXmlException("Error parsing the xml file " + link + "\n\n" + xml);
      }
      catch (WebException ex)
      {
        Log.Warn("Request not successfull", ex);
        if (ex.Message.Equals("The remote server returned an error: (404) Not Found."))
        {
          throw new TvdbContentNotFoundException("Couldn't download episode  for series " + _seriesId + " from " +
                                                 _airDate.ToShortDateString() + "(" + _language.Abbriviation +
                                                 "), maybe the episode doesn't exist");
        }
        else
        {
          throw new TvdbNotAvailableException("Couldn't download episode  for series " + _seriesId + " from " +
                                                 _airDate.ToShortDateString() + "(" + _language.Abbriviation +
                                                 "), maybe the episode doesn't exist");
        }
      }
    }

    /// <summary>
    /// Download the preferred language of the user
    /// </summary>
    /// <param name="_userId"></param>
    /// <returns></returns>
    internal TvdbLanguage DownloadUserPreferredLanguage(String _userId)
    {
      String xml = "";
      String link = "";
      try
      {
        link = TvdbLinks.CreateUserLanguageLink(_userId);
        xml = m_webClient.DownloadString(link);
      }
      catch (XmlException ex)
      {
        Log.Error("Error parsing the xml file " + link + "\n\n" + xml, ex);
        throw new TvdbInvalidXmlException("Error parsing the xml file " + link + "\n\n" + xml);
      }
      catch (WebException ex)
      {
        Log.Warn("Request not successfull", ex);
        if (ex.Message.Equals("The remote server returned an error: (404) Not Found."))
        {
          throw new TvdbInvalidApiKeyException("Couldn't connect to Thetvdb.com to retrieve preferred languae for user " + _userId +
                                               ", you may use an invalid api key");
        }
        else
        {
          throw new TvdbNotAvailableException("Couldn't connect to Thetvdb.com to retrieve preferred languae for user " + _userId +
                                              ", check your internet connection and the status of http://thetvdb.com");
        }
      }
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
      String xml = "";
      String link = "";
      try
      {
        link = TvdbLinks.CreateUserFavouriteLink(_userId, _type, _seriesId);
        xml = m_webClient.DownloadString(link);
      }
      catch (XmlException ex)
      {
        Log.Error("Error parsing the xml file " + link + "\n\n" + xml, ex);
        throw new TvdbInvalidXmlException("Error parsing the xml file " + link + "\n\n" + xml);
      }
      catch (WebException ex)
      {
        Log.Warn("Request not successfull", ex);
        if (ex.Message.Equals("The remote server returned an error: (404) Not Found."))
        {
          throw new TvdbInvalidApiKeyException("Couldn't connect to Thetvdb.com to retrieve favorite list for user " + _userId +
                                               ", you may use an invalid api key");
        }
        else
        {
          throw new TvdbNotAvailableException("Couldn't connect to Thetvdb.com to retrieve favorite list for user " + _userId +
                                              ", check your internet connection and the status of http://thetvdb.com");
        }
      }
      List<int> favList = m_xmlHandler.ExtractSeriesFavorites(xml);
      return favList;
    }
    /// <summary>
    /// Download an Update
    /// </summary>
    /// <param name="_updateSeries">updated series to return</param>
    /// <param name="_updateEpisodes">updated episodes to return</param>
    /// <param name="_updateBanners">updated banners to return</param>
    /// <param name="_interval">interval to download</param>
    /// <param name="_zipped">use zip</param>
    /// <returns>Time of the update</returns>
    internal DateTime DownloadUpdate(out List<TvdbSeries> _updateSeries, out List<TvdbEpisode> _updateEpisodes,
                                     out List<TvdbBanner> _updateBanners, Util.UpdateInterval _interval, bool _zipped)
    {

      String xml = "";
      String link = "";
      try
      {
        link = TvdbLinks.CreateUpdateLink(m_apiKey, _interval, _zipped);
        if (_zipped)
        {
          byte[] data = m_webClient.DownloadData(link);
          ZipInputStream zip = new ZipInputStream(new MemoryStream(data));
          zip.GetNextEntry();
          byte[] buffer = new byte[zip.Length];
          int count = zip.Read(buffer, 0, (int)zip.Length);
          xml = Encoding.UTF8.GetString(buffer);
        }
        else
        {
          xml = m_webClient.DownloadString(link);
        }

        _updateEpisodes = m_xmlHandler.ExtractEpisodeUpdates(xml);
        _updateSeries = m_xmlHandler.ExtractSeriesUpdates(xml);
        _updateBanners = m_xmlHandler.ExtractBannerUpdates(xml);

        return m_xmlHandler.ExtractUpdateTime(xml);
      }
      catch (XmlException ex)
      {
        Log.Error("Error parsing the xml file " + link + "\n\n" + xml, ex);
        throw new TvdbInvalidXmlException("Error parsing the xml file " + link + "\n\n" + xml);
      }
      catch (ZipException ex)
      {
        Log.Error("Error unzipping the xml file " + link, ex);
        throw new TvdbInvalidXmlException("Error unzipping the xml file " + link);
      }
      catch (WebException ex)
      {
        Log.Warn("Request not successfull", ex);
        if (ex.Message.Equals("The remote server returned an error: (404) Not Found."))
        {
          throw new TvdbInvalidApiKeyException("Couldn't connect to Thetvdb.com to retrieve updates for " + _interval +
                                               ", you may use an invalid api key");
        }
        else
        {
          throw new TvdbNotAvailableException("Couldn't connect to Thetvdb.com to retrieve updates for " + _interval +
                                              ", check your internet connection and the status of http://thetvdb.com");
        }
      }
    }

    /// <summary>
    /// Download available languages
    /// </summary>
    /// <returns></returns>
    internal List<TvdbLanguage> DownloadLanguages()
    {
      String xml = "";
      String link = "";
      try
      {
        link = TvdbLinks.CreateLanguageLink(m_apiKey);
        xml = m_webClient.DownloadString(link);
        return m_xmlHandler.ExtractLanguages(xml);
      }
      catch (XmlException ex)
      {
        Log.Error("Error parsing the xml file " + link + "\n\n" + xml, ex);
        throw new TvdbInvalidXmlException("Error parsing the xml file " + link + "\n\n" + xml);
      }
      catch (WebException ex)
      {
        Log.Warn("Request not successfull", ex);
        if (ex.Message.Equals("The remote server returned an error: (404) Not Found."))
        {
          throw new TvdbInvalidApiKeyException("Couldn't connect to Thetvdb.com to retrieve the list of available languages" +
                                               ", you may use an invalid api key");
        }
        else
        {
          throw new TvdbNotAvailableException("Couldn't connect to Thetvdb.com to retrieve the list of available languages" +
                                              ", check your internet connection and the status of http://thetvdb.com");
        }
      }
    }

    /// <summary>
    /// Download search results
    /// </summary>
    /// <param name="_name"></param>
    /// <returns></returns>
    internal List<TvdbSearchResult> DownloadSearchResults(String _name)
    {
      String xml = "";
      String link = "";
      try
      {
        link = TvdbLinks.CreateSearchLink(_name);
        xml = m_webClient.DownloadString(link);
        return m_xmlHandler.ExtractSeriesSearchResults(xml);
      }
      catch (XmlException ex)
      {
        Log.Error("Error parsing the xml file " + link + "\n\n" + xml, ex);
        throw new TvdbInvalidXmlException("Error parsing the xml file " + link + "\n\n" + xml);
      }
      catch (WebException ex)
      {
        Log.Warn("Request not successfull", ex);
        if (ex.Message.Equals("The remote server returned an error: (404) Not Found."))
        {
          throw new TvdbInvalidApiKeyException("Couldn't connect to Thetvdb.com to retrieve search results for " + _name +
                                               ", you may use an invalid api key");
        }
        else
        {
          throw new TvdbNotAvailableException("Couldn't connect to Thetvdb.com to retrieve search results for " + _name +
                                              ", check your internet connection and the status of http://thetvdb.com");
        }
      }
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
      String xml = "";
      String link = "";
      try
      {
        link = TvdbLinks.CreateUserSeriesRating(_userId, _seriesId, _rating);
        xml = m_webClient.DownloadString(link);
      }
      catch (XmlException ex)
      {
        Log.Error("Error parsing the xml file " + link + "\n\n" + xml, ex);
        throw new TvdbInvalidXmlException("Error parsing the xml file " + link + "\n\n" + xml);
      }
      catch (WebException ex)
      {
        Log.Warn("Request not successfull", ex);
        if (ex.Message.Equals("The remote server returned an error: (404) Not Found."))
        {
          throw new TvdbInvalidApiKeyException("Couldn't connect to Thetvdb.com to rate series " + _seriesId +
                                               ", you may use an invalid api key");
        }
        else
        {
          throw new TvdbNotAvailableException("Couldn't connect to Thetvdb.com to rate series " + _seriesId +
                                              ", check your internet connection and the status of http://thetvdb.com");
        }
      }
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
      String xml = "";
      String link = "";
      try
      {
        link = TvdbLinks.CreateUserEpisodeRating(_userId, _episodeId, _rating);
        xml = m_webClient.DownloadString(link);
        return m_xmlHandler.ExtractRating(xml);
      }
      catch (XmlException ex)
      {
        Log.Error("Error parsing the xml file " + link + "\n\n" + xml, ex);
        throw new TvdbInvalidXmlException("Error parsing the xml file " + link + "\n\n" + xml);
      }
      catch (WebException ex)
      {
        Log.Warn("Request not successfull", ex);
        if (ex.Message.Equals("The remote server returned an error: (404) Not Found."))
        {
          throw new TvdbInvalidApiKeyException("Couldn't connect to Thetvdb.com to rate episode " + _episodeId +
                                               ", you may use an invalid api key  or the episode doesn't exists");
        }
        else
        {
          throw new TvdbNotAvailableException("Couldn't connect to Thetvdb.com to rate episode " + _episodeId +
                                              ", check your internet connection and the status of http://thetvdb.com");
        }
      }
    }

    /// <summary>
    /// Download the series rating without doing a rating
    /// </summary>
    /// <param name="_userId"></param>
    /// <param name="_seriesId"></param>
    /// <returns></returns>
    internal double DownloadSeriesRating(String _userId, int _seriesId)
    {
      String xml = "";
      String link = "";
      try
      {
        link = TvdbLinks.CreateUserSeriesRating(_userId, _seriesId);
        xml = m_webClient.DownloadString(link);
        return m_xmlHandler.ExtractRating(xml);
      }
      catch (XmlException ex)
      {
        Log.Error("Error parsing the xml file " + link + "\n\n" + xml, ex);
        throw new TvdbInvalidXmlException("Error parsing the xml file " + link + "\n\n" + xml);
      }
      catch (WebException ex)
      {
        Log.Warn("Request not successfull", ex);
        if (ex.Message.Equals("The remote server returned an error: (404) Not Found."))
        {
          throw new TvdbInvalidApiKeyException("Couldn't connect to Thetvdb.com to retrieve rating of series " + _seriesId +
                                               ", you may use an invalid api key or the series doesn't exists");
        }
        else
        {
          throw new TvdbNotAvailableException("Couldn't connect to Thetvdb.com to retrieve rating of series " + _seriesId +
                                              ", check your internet connection and the status of http://thetvdb.com");
        }
      }
    }

    /// <summary>
    /// Download the episode rating without rating
    /// </summary>
    /// <param name="_userId">id of the user</param>
    /// <param name="_episodeId">id of the episode</param>
    /// <returns></returns>
    internal double DownloadEpisodeRating(String _userId, int _episodeId)
    {
      String xml = "";
      String link = "";
      try
      {
        link = TvdbLinks.CreateUserEpisodeRating(_userId, _episodeId);
        xml = m_webClient.DownloadString(link);
        return m_xmlHandler.ExtractRating(xml);
      }
      catch (XmlException ex)
      {
        Log.Error("Error parsing the xml file " + link + "\n\n" + xml, ex);
        throw new TvdbInvalidXmlException("Error parsing the xml file " + link + "\n\n" + xml);
      }
      catch (WebException ex)
      {
        Log.Warn("Request not successfull", ex);
        if (ex.Message.Equals("The remote server returned an error: (404) Not Found."))
        {
          throw new TvdbInvalidApiKeyException("Couldn't connect to Thetvdb.com to retrieve rating of series " + _episodeId +
                                               ", you may use an invalid api key  or the episode doesn't exists");
        }
        else
        {
          throw new TvdbNotAvailableException("Couldn't connect to Thetvdb.com to retrieve rating of series " + _episodeId +
                                              ", check your internet connection and the status of http://thetvdb.com");
        }
      }
    }

    /// <summary>
    /// Download the list of actors
    /// </summary>
    /// <param name="_seriesId"></param>
    /// <returns></returns>
    internal List<TvdbActor> DownloadActors(int _seriesId)
    {
      String xml = "";
      String link = "";
      try
      {
        link = TvdbLinks.CreateActorLink(_seriesId, m_apiKey);
        xml = m_webClient.DownloadString(link);
        return m_xmlHandler.ExtractActors(xml);
      }
      catch (XmlException ex)
      {
        Log.Error("Error parsing the xml file " + link + "\n\n" + xml, ex);
        throw new TvdbInvalidXmlException("Error parsing the xml file " + link + "\n\n" + xml);
      }
      catch (WebException ex)
      {
        Log.Warn("Couldn't download actor info from thetvdb.com", ex);
        if (ex.Message.Equals("The remote server returned an error: (404) Not Found."))
        {
          throw new TvdbInvalidApiKeyException("Couldn't connect to Thetvdb.com to retrieve actor info of series " + _seriesId +
                                               ", you may use an invalid api key or the series doesn't exists");
        }
        else
        {
          throw new TvdbNotAvailableException("Couldn't connect to Thetvdb.com to retrieve actor info of series " + _seriesId +
                                              ", check your internet connection and the status of http://thetvdb.com");
        }
      }
    }

    internal List<TvdbMirror> DownloadMirrorList()
    {
      String xml = "";
      String link = "";
      try
      {
        link = TvdbLinks.CreateMirrorsLink(m_apiKey);
        xml = m_webClient.DownloadString(link);
        List<TvdbMirror> list = m_xmlHandler.ExtractMirrors(xml);
        return list;
      }
      catch (XmlException ex)
      {
        Log.Error("Error parsing the xml file " + link + "\n\n" + xml, ex);
        throw new TvdbInvalidXmlException("Error parsing the xml file " + link + "\n\n" + xml);
      }
      catch (WebException ex)
      {
        Log.Warn("Couldn't download mirror list from thetvdb.com", ex);
        if (ex.Message.Equals("The remote server returned an error: (404) Not Found."))
        {
          throw new TvdbInvalidApiKeyException("Couldn't connect to Thetvdb.com to retrieve mirror list" +
                                               ", you may use an invalid api key");
        }
        else
        {
          throw new TvdbNotAvailableException("Couldn't connect to Thetvdb.com to retrieve mirror list" +
                                              ", check your internet connection and the status of http://thetvdb.com");
        }
      }
    }



    /// <summary>
    /// Gets all series this user has already ratet
    /// </summary>
    /// <exception cref="TvdbUserNotFoundException">Thrown when no user is set</exception>
    /// <returns></returns>
    internal Dictionary<int, TvdbRating> DownloadAllSeriesRatings(String _userId)
    {
      String xml = "";
      String link = "";
      try
      {
        link = TvdbLinks.CreateAllSeriesRatingsLink(m_apiKey, _userId);
        xml = m_webClient.DownloadString(link);
        return m_xmlHandler.ExtractRatings(xml, TvdbRating.ItemType.Series);
      }
      catch (XmlException ex)
      {
        Log.Error("Error parsing the xml file " + link + "\n\n" + xml, ex);
        throw new TvdbInvalidXmlException("Error parsing the xml file " + link + "\n\n" + xml);
      }
      catch (WebException ex)
      {
        Log.Warn("Request not successfull", ex);
        if (ex.Message.Equals("The remote server returned an error: (404) Not Found."))
        {
          throw new TvdbInvalidApiKeyException("Couldn't connect to Thetvdb.com to retrieve rating of all series " +
                                               ", you may use an invalid api key");
        }
        else
        {
          throw new TvdbNotAvailableException("Couldn't connect to Thetvdb.com to retrieve rating of all series " +
                                              ", check your internet connection and the status of http://thetvdb.com");
        }
      }
    }

    internal Dictionary<int, TvdbRating> DownloadRatingsForSeries(string _userId, int _seriesId)
    {
      String xml = "";
      String link = "";
      try
      {
        link = TvdbLinks.CreateSeriesRatingsLink(m_apiKey, _userId, _seriesId);
        xml = m_webClient.DownloadString(link);
        Dictionary<int, TvdbRating> retList = m_xmlHandler.ExtractRatings(xml, TvdbRating.ItemType.Series);
        Dictionary<int, TvdbRating> episodeList = m_xmlHandler.ExtractRatings(xml, TvdbRating.ItemType.Episode);
        if (retList != null && episodeList != null && retList.Count > 0)
        {
          foreach (KeyValuePair<int, TvdbRating> r in episodeList)
          {
            if (!retList.ContainsKey(r.Key))
            {
              retList.Add(r.Key, r.Value);
            }
          }
          return retList;
        }
        else return null;
      }
      catch (XmlException ex)
      {
        Log.Error("Error parsing the xml file " + link + "\n\n" + xml, ex);
        throw new TvdbInvalidXmlException("Error parsing the xml file " + link + "\n\n" + xml);
      }
      catch (WebException ex)
      {
        Log.Warn("Request not successfull", ex);
        if (ex.Message.Equals("The remote server returned an error: (404) Not Found."))
        {
          throw new TvdbInvalidApiKeyException("Couldn't connect to Thetvdb.com to retrieve rating of all series " +
                                               ", you may use an invalid api key");
        }
        else
        {
          throw new TvdbNotAvailableException("Couldn't connect to Thetvdb.com to retrieve rating of all series " +
                                              ", check your internet connection and the status of http://thetvdb.com");
        }
      }
    }


  }
}
