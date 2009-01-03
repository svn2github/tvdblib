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
using System.Linq;
using System.Text;
using TvdbLib.Data;
using TvdbLib.Xml;
using TvdbLib.Data.Banner;
using System.IO;
using System.Drawing;
using System.Text.RegularExpressions;

namespace TvdbLib.Cache
{
  /// <summary>
  /// XmlCacheProvider stores all the information that have been retrieved from http://thetvdb.com as human-readable xml files on the hard disk
  /// </summary>
  public class XmlCacheProvider : ICacheProvider
  {
    #region private fields
    TvdbXmlWriter m_xmlWriter;
    TvdbXmlReader m_xmlReader;
    String m_rootFolder;
    #endregion

    /// <summary>
    /// Constructor for XmlCacheProvider
    /// </summary>
    /// <param name="_rootFolder">This is the folder on the disk where all the information are stored</param>
    public XmlCacheProvider(String _rootFolder)
    {
      m_xmlWriter = new TvdbXmlWriter();
      m_xmlReader = new TvdbXmlReader();
      m_rootFolder = _rootFolder;
      if (!Directory.Exists(_rootFolder))
      {
        Directory.CreateDirectory(_rootFolder);
      }
    }

    #region ICacheProvider Members

    /// <summary>
    /// Saves all available data to cache
    /// </summary>
    /// <param name="_content"></param>
    public void SaveAllToCache(TvdbData _content)
    {
      SaveToCache(_content.LanguageList);
      SaveToCache(_content.Mirrors);
      foreach (TvdbSeries s in _content.SeriesList)
      {
        SaveToCache(s);
      }
    }

    /// <summary>
    /// Save the language to cache
    /// </summary>
    /// <param name="_languageList"></param>
    public void SaveToCache(List<TvdbLanguage> _languageList)
    {
      if (_languageList != null && _languageList.Count > 0)
      {
        if (!Directory.Exists(m_rootFolder)) Directory.CreateDirectory(m_rootFolder);
        m_xmlWriter.WriteLanguageFile(_languageList, m_rootFolder + "\\languages.xml");
      }
    }

    /// <summary>
    /// Save the mirror info to cache
    /// </summary>
    /// <param name="_mirrorInfo"></param>
    public void SaveToCache(List<TvdbMirror> _mirrorInfo)
    {
      if (_mirrorInfo != null && _mirrorInfo.Count > 0)
      {
        if (!Directory.Exists(m_rootFolder)) Directory.CreateDirectory(m_rootFolder);
        m_xmlWriter.WriteMirrorFile(_mirrorInfo, m_rootFolder + "\\mirrors.xml");
      }
    }


    /// <summary>
    /// Saves the series to cache
    /// </summary>
    /// <param name="_series">The series to save</param>
    public void SaveToCache(TvdbSeries _series)
    {
      if (!Directory.Exists(m_rootFolder)) Directory.CreateDirectory(m_rootFolder);
      String root = m_rootFolder + "\\" + _series.Id;

      m_xmlWriter.WriteSeriesContent(_series, root + "\\all.xml");
      TvdbLanguage defaultLang = _series.Language;

      foreach (TvdbLanguage l in _series.GetAvailableLanguages())
      {
        if (l != defaultLang)
        {
          _series.SetLanguage(l);
          m_xmlWriter.WriteSeriesContent(_series, root + "\\" + l.Abbriviation + ".xml");
        }
      }


      if (_series.BannersLoaded)
      {
        m_xmlWriter.WriteSeriesBannerContent(_series.Banners, root + "\\banners.xml");
      }

      if (_series.TvdbActorsLoaded)
      {
        m_xmlWriter.WriteActorFile(_series.TvdbActors, root + "\\actors.xml");
      }

      //Save all loaded banners to file
      foreach (TvdbBanner b in _series.Banners)
      {
        FileInfo file = new FileInfo(root + "\\banner_" + b.Id + ".jpg");
        if (b.IsLoaded && !file.Exists)
        {//banner is cached
          if (!file.Directory.Exists) file.Directory.Create();
          b.Banner.Save(file.FullName);
        }

        if (b.GetType().BaseType == typeof(TvdbBannerWithThumb))
        {//banner has thumb -> store thumb as well
          file = new FileInfo(root + "\\bannerthumb_" + b.Id + ".jpg");
          if (((TvdbBannerWithThumb)b).IsThumbLoaded && !file.Exists)
          {
            if (!file.Directory.Exists) file.Directory.Create();
            ((TvdbBannerWithThumb)b).BannerThumb.Save(file.FullName);
          }
        }

        if (b.GetType() == typeof(TvdbFanartBanner))
        {//banner is fanart -> has vignette and thumb
          file = new FileInfo(root + "\\bannervignette_" + b.Id + ".jpg");
          if (((TvdbFanartBanner)b).IsVignetteLoaded && !file.Exists)
          {
            if (!file.Directory.Exists) file.Directory.Create();
            ((TvdbFanartBanner)b).VignetteImage.Save(file.FullName);
          }

        }
      }

      if (_series.EpisodesLoaded)
      {
        foreach (TvdbEpisode e in _series.Episodes)
        {
          if (e.Banner != null)
          {
            FileInfo file = new FileInfo(root + "\\EpisodeImages\\ep_S" + e.SeasonNumber + "E" + e.EpisodeNumber + ".jpg");
            if (e.Banner.IsLoaded && !file.Exists)
            {
              if (!file.Directory.Exists) file.Directory.Create();
              e.Banner.Banner.Save(file.FullName);
            }
            file = new FileInfo(root + "\\EpisodeImages\\ep_S" + e.SeasonNumber + "E" + e.EpisodeNumber + "_thumb.jpg");
            if (e.Banner.BannerThumb != null && e.Banner.IsThumbLoaded && !file.Exists)
            {
              if (!file.Directory.Exists) file.Directory.Create();
              e.Banner.BannerThumb.Save(file.FullName);
            }
          }
        }
      }

      if (_series.TvdbActorsLoaded)
      {
        foreach (TvdbActor a in _series.TvdbActors)
        {
          FileInfo file = new FileInfo(root + "\\actor_" + a.ActorImage.Id + ".jpg");
          if (a.ActorImage.IsLoaded && !file.Exists)
          {
            if (!file.Directory.Exists) file.Directory.Create();
            a.ActorImage.Banner.Save(file.FullName);
          }
        }
      }

    }

    /// <summary>
    /// Loads all cached series from cache -> can take a while
    /// </summary>
    /// <returns></returns>
    public TvdbData LoadUserDataFromCache()
    {
      TvdbData data = new TvdbData();
      data.LanguageList = LoadLanguageListFromCache();
      data.Mirrors = LoadMirrorListFromCache();
      if (data.SeriesList == null) data.SeriesList = new List<TvdbSeries>();
      return data;
    }

    /// <summary>
    /// Loads the available languages from cache
    /// </summary>
    /// <returns></returns>
    public List<TvdbLanguage> LoadLanguageListFromCache()
    {

      String file = m_rootFolder + "\\languages.xml";
      if (File.Exists(file))
      {
        return m_xmlReader.ExtractLanguages(File.ReadAllText(file));
      }
      else
      {
        return null;
      }
    }

    /// <summary>
    /// Load the available mirrors from cache
    /// </summary>
    /// <returns></returns>
    public List<TvdbMirror> LoadMirrorListFromCache()
    {
      String file = m_rootFolder + "\\mirrors.xml";
      if (File.Exists(file))
      {
        return m_xmlReader.ExtractMirrors(File.ReadAllText(file));
      }
      else
      {
        return null;
      }
    }

    /// <summary>
    /// Loads all series from cache
    /// </summary>
    /// <returns></returns>
    public List<TvdbSeries> LoadAllSeriesFromCache()
    {
      List<TvdbSeries> retList = new List<TvdbSeries>();
      string[] dirs = Directory.GetDirectories(m_rootFolder);
      foreach (String d in dirs)
      {
        TvdbSeries series = LoadSeriesFromCache(Int32.Parse(d.Remove(0, d.LastIndexOf("\\") + 1)));
        if (series != null) retList.Add(series);
      }
      return retList;
    }



    /// <summary>
    /// Load the give series from cache
    /// </summary>
    /// <param name="_seriesId">Id of the series to load</param>
    /// <returns>Series that has been loaded</returns>
    public TvdbSeries LoadSeriesFromCache(int _seriesId)
    {
      String seriesRoot = m_rootFolder + "\\" + _seriesId;
      //todo: handle languages
      TvdbSeries series = new TvdbSeries();

      #region load series in all available languages
      String[] seriesLanguages = Directory.GetFiles(seriesRoot, "*.xml");
      TvdbLanguage defaultLanguage = null; ;

      foreach (String l in seriesLanguages)
      {
        if (!l.EndsWith("actors.xml") && !l.EndsWith("banners.xml"))
        {
          String content = File.ReadAllText(l);
          List<TvdbSeriesFields> seriesList = m_xmlReader.ExtractSeriesFields(content);
          if (seriesList != null && seriesList.Count == 1)
          {
            TvdbSeriesFields s = seriesList[0];
            if (l.EndsWith("all.xml")) defaultLanguage = s.Language;

            //Load episodes
            List<TvdbEpisode> epList = m_xmlReader.ExtractEpisodes(content);
            if (epList != null && epList.Count > 0)
            {
              s.EpisodesLoaded = true;
              s.Episodes = epList;
            }
            series.AddLanguage(s);
          }
        }
      }

      if (defaultLanguage != null)
      {//change language of the series to the default language
        series.SetLanguage(defaultLanguage);
      }
      else
      {//no series info could be loaded
        return null;
      }

      Regex rex = new Regex("S(\\d+)E(\\d+)");
      if (Directory.Exists(seriesRoot + "\\EpisodeImages"))
      {
        String[] episodeFiles = Directory.GetFiles(seriesRoot + "\\EpisodeImages", "ep_*.jpg");
        foreach (String epImageFile in episodeFiles)
        {
          try
          {
            Match match = rex.Match(epImageFile);
            int season = Int32.Parse(match.Groups[1].Value);
            int episode = Int32.Parse(match.Groups[2].Value);
            foreach (TvdbEpisode e in series.Episodes)
            {
              if (e.SeasonNumber == season && e.EpisodeNumber == episode)
              {
                if (epImageFile.Contains("thumb"))
                {
                  e.Banner.LoadThumb(Image.FromFile(epImageFile));
                }
                else
                {
                  e.Banner.LoadBanner(Image.FromFile(epImageFile));
                }
                break;
              }
            }
          }
          catch (Exception)
          {
            Log.Warn("Couldn't load episode image file " + epImageFile);
          }
        }
      }

      #endregion

      #region Banner loading
      String bannerFile = seriesRoot + "\\banners.xml";
      //load cached banners
      if (File.Exists(bannerFile))
      {//banners have been already loaded
        List<TvdbBanner> bannerList = m_xmlReader.ExtractBanners(File.ReadAllText(bannerFile));

        String[] banners = Directory.GetFiles(seriesRoot, "banner*.jpg");
        foreach (String b in banners)
        {
          try
          {
            int bannerId = Int32.Parse(b.Remove(b.IndexOf(".")).Remove(0, b.LastIndexOf("_") + 1));
            foreach (TvdbBanner banner in bannerList)
            {
              if (banner.Id == bannerId)
              {
                if (b.Contains("thumb") && banner.GetType().BaseType == typeof(TvdbBannerWithThumb))
                {
                  ((TvdbBannerWithThumb)banner).LoadThumb(Image.FromFile(b));
                }
                else if (b.Contains("vignette") && banner.GetType() == typeof(TvdbFanartBanner))
                {
                  ((TvdbFanartBanner)banner).LoadVignette(Image.FromFile(b));
                }
                else
                {
                  banner.LoadBanner(Image.FromFile(b));
                }
              }
            }

          }
          catch (Exception)
          {
            Log.Warn("Couldn't load image file " + b);
          }
        }
        series.Banners = bannerList;
      }
      #endregion

      #region actor loading
      //load actor info
      String actorFile = seriesRoot + "\\actors.xml";
      if (File.Exists(actorFile))
      {
        List<TvdbActor> actorList = m_xmlReader.ExtractActors(File.ReadAllText(actorFile));

        String[] banners = Directory.GetFiles(seriesRoot, "actor_*.jpg");
        foreach (String b in banners)
        {
          try
          {
            int actorId = Int32.Parse(b.Remove(b.IndexOf(".")).Remove(0, b.LastIndexOf("_") + 1));
            foreach (TvdbActor actor in actorList)
            {
              if (actor.Id == actorId)
              {
                actor.ActorImage.LoadBanner(Image.FromFile(b));
              }
            }

          }
          catch (Exception)
          {
            Log.Warn("Couldn't load image file " + b);
          }
        }
        series.TvdbActors = actorList;
      }
      #endregion

      return series;

    }

    /// <summary>
    /// Load user info from cache
    /// </summary>
    /// <param name="_userId">Id of the user</param>
    /// <returns>TvdbUser object or null if the user couldn't be loaded</returns>
    public TvdbUser LoadUserInfoFromCache(string _userId)
    {
      String seriesRoot = m_rootFolder;
      String xmlFile = seriesRoot + "\\user_" + _userId + ".xml";
      if (!File.Exists(xmlFile)) return null;
      String content = File.ReadAllText(xmlFile);
      List<TvdbUser> userList = m_xmlReader.ExtractUser(content);
      if (userList != null && userList.Count == 1)
      {
        return userList[0];
      }
      else
      {
        return null;
      }
    }

    /// <summary>
    /// Saves the user data to cache
    /// </summary>
    /// <param name="_user">TvdbUser object</param>
    public void SaveToCache(TvdbUser _user)
    {
      if (_user != null)
      {
        if (!Directory.Exists(m_rootFolder)) Directory.CreateDirectory(m_rootFolder);
        m_xmlWriter.WriteUserData(_user, m_rootFolder + "\\user_" + _user.UserIdentifier + ".xml");
      }
    }


    /// <summary>
    /// Receives a list of all series that have been cached
    /// </summary>
    /// <returns>A list of series that have been already stored with this cache provider</returns>
    public List<int> GetCachedSeries()
    {
      List<int> retList = new List<int>();
      if (Directory.Exists(m_rootFolder))
      {
        string[] dirs = Directory.GetDirectories(m_rootFolder);
        foreach (String d in dirs)
        {
          try
          {
            int series = Int32.Parse(d.Remove(0, d.LastIndexOf("\\") + 1));
            retList.Add(series);
          }
          catch (FormatException)
          {
            Log.Error("Couldn't parse " + d + " when loading list of cached series");
          }
        }
      }
      return retList;
    }

    /// <summary>
    /// Check if the series is cached in the given configuration
    /// </summary>
    /// <param name="_seriesId">Id of the series</param>
    /// <param name="_episodesLoaded">are episodes loaded</param>
    /// <param name="_bannersLoaded">are banners loaded</param>
    /// <param name="_actorsLoaded">are actors loaded</param>
    /// <returns>true if the series is cached, false otherwise</returns>
    public bool IsCached(int _seriesId, bool _episodesLoaded,
                         bool _bannersLoaded, bool _actorsLoaded)
    {
      bool actorsLoaded = false;
      bool episodesLoaded = false;
      bool bannersLoaded = false;

      String seriesRoot = m_rootFolder + "\\" + _seriesId;
      String xmlFile = seriesRoot + "\\all.xml";
      if (!File.Exists(xmlFile)) return false;
      String content = File.ReadAllText(xmlFile);

      List<TvdbEpisode> epList = m_xmlReader.ExtractEpisodes(content);
      if (epList != null && epList.Count > 0)
      {//episodes are not loaded
        episodesLoaded = true;
      }
      String bannerFile = seriesRoot + "\\banners.xml";
      String actorFile = seriesRoot + "\\actors.xml";

      //load cached banners
      if (File.Exists(bannerFile))
      {//banners have been already loaded
        bannersLoaded = true;
      }

      //load actor info
      if (File.Exists(actorFile))
      {
        actorsLoaded = true;
      }

      if (episodesLoaded || !_episodesLoaded &&
          bannersLoaded || !_bannersLoaded &&
          actorsLoaded || !_actorsLoaded)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    #endregion
  }
}
