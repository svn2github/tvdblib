using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TvdbConnector.Data;
using TvdbConnector.Xml;
using TvdbConnector.Data.Banner;
using System.IO;
using System.Drawing;

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

    public void SaveAllToCache(TvdbData _content)
    {
      SaveToCache(_content.LanguageList);
      SaveToCache(_content.Mirrors);
      foreach (TvdbSeries s in _content.SeriesList)
      {
        SaveToCache(s);
      }
    }

    public void SaveToCache(List<TvdbLanguage> _languageList)
    {
      if (_languageList != null && _languageList.Count > 0)
      {
        m_xmlWriter.WriteLanguageFile(_languageList, m_rootFolder + "\\languages.xml");
      }
    }

    public void SaveToCache(List<TvdbMirror> _mirrorInfo)
    {
      if (_mirrorInfo != null && _mirrorInfo.Count > 0)
      {
        m_xmlWriter.WriteMirrorFile(_mirrorInfo, m_rootFolder + "\\mirrors.xml");
      }
    }


    public TvdbData LoadUserDataFromCache()
    {
      TvdbData data = new TvdbData();
      data.LanguageList = LoadLanguageListFromCache();
      data.Mirrors = LoadMirrorListFromCache();
      if (data.SeriesList == null) data.SeriesList = new List<TvdbSeries>();
      return data;
    }



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

        String[] episodeFiles = Directory.GetFiles(seriesRoot, "*.jpg");
        foreach (String epImageFile in episodeFiles)
        {
          try
          {
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

        //load cached banners
        if (File.Exists(bannerFile))
        {//banners have been already loaded
         
          List<TvdbBanner> bannerList = m_xmlReader.ExtractBanners(File.ReadAllText(bannerFile));

          String[] banners = Directory.GetFiles(seriesRoot, "*.jpg");
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
        return series;
      }
      else
      {
        return null;
      }
    }

    public void SaveToCache(TvdbSeries _series)
    {
      String root = m_rootFolder + "\\" + _series.Id;
      m_xmlWriter.WriteSeriesContent(_series, root + "\\all.xml");
      m_xmlWriter.WriteSeriesBannerContent(_series.Banners, root + "\\banners.xml");

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

    #endregion
  }
}
