using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TvdbConnector.Data;
using TvdbConnector.Xml;
using TvdbConnector.Data.Banner;
using System.IO;
using System.Drawing;
using System.Text.RegularExpressions;

namespace TvdbConnector.Cache
{
  public class XmlCacheProvider : ICacheProvider
  {
    TvdbXmlWriter m_xmlWriter;
    TvdbXmlReader m_xmlReader;
    String m_rootFolder;
    public XmlCacheProvider(String _rootFolder)
    {
      m_xmlWriter = new TvdbXmlWriter();
      m_xmlReader = new TvdbXmlReader();
      m_rootFolder = _rootFolder;
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
        m_xmlWriter.WriteMirrorFile(_mirrorInfo, m_rootFolder + "\\mirrors.xml");
      }
    }


    /// <summary>
    /// Saves the series to cache
    /// </summary>
    /// <param name="_series"></param>
    public void SaveToCache(TvdbSeries _series)
    {
      String root = m_rootFolder + "\\" + _series.Id;
      m_xmlWriter.WriteSeriesContent(_series, root + "\\all.xml");
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
        if (b.GetType() == typeof(TvdbFanartBanner))
        {//banner is fanart -> has vignette and thumb
          file = new FileInfo(root + "\\bannerthumb_" + b.Id + ".jpg");
          if (((TvdbFanartBanner)b).IsThumbLoaded && !file.Exists)
          {
            if (!file.Directory.Exists) file.Directory.Create();
            ((TvdbFanartBanner)b).BannerThumb.Save(file.FullName);
          }
          //todo: vignette!!!
        }
      }

      if (_series.EpisodesLoaded)
      {
        foreach (TvdbEpisode e in _series.Episodes)
        {
          FileInfo file = new FileInfo(root + "\\EpisodeImages\\S" + e.SeasonNumber + "E" + e.EpisodeNumber + ".jpg");
          if (e.Banner.IsLoaded && !file.Exists)
          {
            if (!file.Directory.Exists) file.Directory.Create();
            e.Banner.Banner.Save(file.FullName);
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
    /// <param name="_seriesId"></param>
    /// <returns></returns>
    public TvdbSeries LoadSeriesFromCache(int _seriesId)
    {
      String seriesRoot = m_rootFolder + "\\" + _seriesId;
      String xmlFile = seriesRoot + "\\all.xml";
      if (!File.Exists(xmlFile)) return null;
      String content = File.ReadAllText(xmlFile);
      List<TvdbSeries> seriesList = m_xmlReader.ExtractSeries(content);
      if (seriesList != null && seriesList.Count == 1)
      {
        TvdbSeries series = seriesList[0];
        List<TvdbEpisode> epList = m_xmlReader.ExtractEpisodes(content);
        series.Episodes = epList;
        String bannerFile = seriesRoot + "\\banners.xml";
        String actorFile = seriesRoot + "\\actors.xml";
        Regex rex = null;
        try
        {
          rex = new Regex("S(\\d+)E(\\d+)");
        }
        catch (Exception ex)
        {

        }

        if (Directory.Exists(seriesRoot + "\\EpisodeImages"))
        {
          String[] episodeFiles = Directory.GetFiles(seriesRoot + "\\EpisodeImages", "*.jpg");
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
                  e.Banner.Banner = Image.FromFile(epImageFile);
                  e.Banner.IsLoaded = true;
                  break;
                }
              }
              //int season = Int32.Parse(epImageFile.Remove(1).Remove(epImageFile.IndexOf('E')));
              /*int bannerId = Int32.Parse(b.Remove(b.IndexOf(".")).Remove(0, b.LastIndexOf("_") + 1));
              foreach (TvdbBanner banner in bannerList)
              {
                if (banner.Id == bannerId)
                {
                  banner.Banner = Image.FromFile(b);
                  banner.IsLoaded = true;
                }
              }*/

            }
            catch (Exception ex)
            {
              Log.Warn("Couldn't load episode image file " + epImageFile);
            }
          }
        }
        //load cached banners
        if (File.Exists(bannerFile))
        {//banners have been already loaded
          List<TvdbBanner> bannerList = m_xmlReader.ExtractBanners(File.ReadAllText(bannerFile));

          String[] banners = Directory.GetFiles(seriesRoot, "banner_*.jpg");
          foreach (String b in banners)
          {
            try
            {
              int bannerId = Int32.Parse(b.Remove(b.IndexOf(".")).Remove(0, b.LastIndexOf("_") + 1));
              foreach (TvdbBanner banner in bannerList)
              {
                if (banner.Id == bannerId)
                {
                  banner.Banner = Image.FromFile(b);
                  banner.IsLoaded = true;
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

        //load actor info
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
                  actor.ActorImage.Banner = Image.FromFile(b);
                  actor.ActorImage.IsLoaded = true;
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

        return series;
      }
      else
      {
        return null;
      }
    }
    #endregion
  }
}
