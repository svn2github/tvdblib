using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TvdbConnector.Data;
using System.IO;
using System.Xml.Linq;
using TvdbConnector.Data.Banner;
using System.Drawing;

namespace TvdbConnector.Xml
{
  /// <summary>
  /// Writes tvdb data to xml files
  /// </summary>
  internal class TvdbXmlWriter
  {
    /// <summary>
    /// TvdbXmlWriter constructor
    /// </summary>
    internal TvdbXmlWriter()
    {

    }

    /// <summary>
    /// Create the file contents
    /// </summary>
    /// <param name="_languages"></param>
    /// <returns></returns>
    internal String CreateLanguageFile(List<TvdbLanguage> _languages)
    {
      XElement xml = new XElement("Languages");
      foreach (TvdbLanguage l in _languages)
      {
        xml.Add(new XElement("Language",
                   new XElement("name", l.Name),
                   new XElement("abbreviation", l.Abbriviation),
                   new XElement("id", l.Id))
               );
      }
      return xml.ToString();
    }

    /// <summary>
    /// Write the list of languages to file
    /// </summary>
    /// <param name="_languages"></param>
    /// <param name="_path"></param>
    /// <returns></returns>
    internal bool WriteLanguageFile(List<TvdbLanguage> _languages, String _path)
    {
      String fileContent = CreateLanguageFile(_languages);
      try
      {
        FileInfo info = new FileInfo(_path);
        if (!info.Directory.Exists) info.Directory.Create();
        File.WriteAllText(info.FullName, fileContent);
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }

    /// <summary>
    /// Create the file content for a list of mirrors
    /// </summary>
    /// <param name="_mirrors"></param>
    /// <returns></returns>
    internal String CreateMirrorList(List<TvdbMirror> _mirrors)
    {
      XElement xml = new XElement("Mirrors");
      foreach (TvdbMirror m in _mirrors)
      {
        xml.Add(new XElement("Mirror",
                   new XElement("id", m.Id),
                   new XElement("mirrorpath", m.MirrorPath),
                   new XElement("typemask", m.TypeMask))
               );
      }
      return xml.ToString();
    }

    /// <summary>
    /// Write the xml file for the mirrors to file
    /// </summary>
    /// <param name="_mirrors"></param>
    /// <param name="_path"></param>
    /// <returns></returns>
    internal bool WriteMirrorFile(List<TvdbMirror> _mirrors, String _path)
    {
      String fileContent = CreateMirrorList(_mirrors);
      try
      {
        FileInfo info = new FileInfo(_path);
        if (!info.Directory.Exists) info.Directory.Create();
        File.WriteAllText(info.FullName, fileContent);
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }

    /// <summary>
    /// Create the file content for a list of actors
    /// </summary>
    /// <param name="_mirrors"></param>
    /// <returns></returns>
    internal String CreateActorList(List<TvdbActor> _actors)
    {
      XElement xml = new XElement("Actors");
      foreach (TvdbActor m in _actors)
      {
        xml.Add(new XElement("Actor",
                   new XElement("id", m.Id),
                   new XElement("Image", m.ActorImage.BannerPath),
                   new XElement("Role", m.Role),
                   new XElement("SortOrder", m.SortOrder),
                   new XElement("Name", m.Name))
               );
      }
      return xml.ToString();
    }

    /// <summary>
    /// Write the xml file for the actors to file
    /// </summary>
    /// <param name="_mirrors"></param>
    /// <param name="_path"></param>
    /// <returns></returns>
    internal bool WriteActorFile(List<TvdbActor> _actors, String _path)
    {
      String fileContent = CreateActorList(_actors);
      try
      {
        FileInfo info = new FileInfo(_path);
        if (!info.Directory.Exists) info.Directory.Create();
        File.WriteAllText(info.FullName, fileContent);
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }

    /// <summary>
    /// Create the series content
    /// </summary>
    /// <param name="m"></param>
    /// <returns></returns>
    internal String CreateSeriesContent(TvdbSeries m)
    {
      XElement xml = new XElement("Data");

      xml.Add(new XElement("Series",
                  new XElement("id", m.Id),
                  new XElement("Actors", m.ActorsString),
                  new XElement("Airs_DayOfWeek", m.AirsDayOfWeek),
                  new XElement("Airs_Time", m.AirsTime),
                  new XElement("ContentRating", m.ContentRating),
                  new XElement("FirstAired", m.FirstAired),
                  new XElement("Genre", m.GenreString),
                  new XElement("IMDB_ID", m.ImdbId),
                  new XElement("Language", m.Language),
                  new XElement("Network", m.Network),
                  new XElement("Overview", m.Overview),
                  new XElement("Rating", m.Rating),
                  new XElement("Runtime", m.Runtime),
                  new XElement("SeriesID", m.TvDotComId),
                  new XElement("SeriesName", m.SeriesName),
                  new XElement("Status", m.Status),
                  new XElement("banner", m.BannerPath != null ? m.BannerPath : ""),
                  new XElement("fanart", m.FanartPath != null ? m.FanartPath : ""),
                  new XElement("lastupdated", Util.DotNetToUnix(m.LastUpdated)),
                  new XElement("zap2it_id", m.Zap2itId))
             );


      if (m.Episodes != null && m.EpisodesLoaded)
      {
        foreach (TvdbEpisode e in m.Episodes)
        {
          xml.Add(new XElement("Episode",
                  new XElement("id", e.Id),
                  new XElement("Combined_episodenumber", e.CombinedEpisodeNumber),
                  new XElement("Combined_season", e.CombinedSeason),
                  new XElement("DVD_chapter", e.DvdChapter != -99 ? e.DvdChapter.ToString() : ""),
                  new XElement("DVD_discid", e.DvdDiscId != -99 ? e.DvdDiscId.ToString() : ""),
                  new XElement("DVD_episodenumber", e.DvdEpisodeNumber != -99 ? e.DvdEpisodeNumber.ToString() : ""),
                  new XElement("DVD_season", e.DvdSeason != -99 ? e.DvdSeason.ToString() : ""),
                  new XElement("Director", e.DirectorsString),
                  new XElement("EpisodeName", e.EpisodeName),
                  new XElement("EpisodeNumber", e.EpisodeNumber),
                  new XElement("FirstAired", e.FirstAired),
                  new XElement("GuestStars", e.GuestStarsString),
                  new XElement("IMDB_ID", e.ImdbId),
                  new XElement("Language", e.Language.Name),
                  new XElement("Overview", e.Overview),
                  new XElement("ProductionCode", e.ProductionCode),
                  new XElement("Rating", e.Rating.ToString()),
                  new XElement("SeasonNumber", e.SeasonNumber),
                  new XElement("Writer", e.WriterString),
                  new XElement("absolute_number", e.AbsoluteNumber),
                  new XElement("airsafter_season", e.AirsAfterSeason != -99 ? e.AirsAfterSeason.ToString() : ""),
                  new XElement("airsbefore_episode", e.AirsBeforeEpisode != -99 ? e.AirsBeforeEpisode.ToString() : ""),
                  new XElement("airsbefore_season", e.AirsBeforeSeason != -99 ? e.AirsBeforeSeason.ToString() : ""),
                  new XElement("filename", e.Banner.BannerPath),
                  new XElement("lastupdated", Util.DotNetToUnix(e.LastUpdated)),
                  new XElement("seasonid", e.SeasonId),
                  new XElement("seriesid", e.SeriesId))
                 );

        }
      }
      return xml.ToString();
    }

    /// <summary>
    /// Write the series content to file
    /// </summary>
    /// <param name="_series"></param>
    /// <param name="_path"></param>
    /// <returns></returns>
    internal bool WriteSeriesContent(TvdbSeries _series, String _path)
    {
      String fileContent = CreateSeriesContent(_series);
      try
      {
        FileInfo info = new FileInfo(_path);
        if (!info.Directory.Exists) info.Directory.Create();
        File.WriteAllText(info.FullName , fileContent);
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }

    /// <summary>
    /// Create the series banner content
    /// </summary>
    /// <param name="_bannerList"></param>
    /// <returns></returns>
    internal String CreateSeriesBannerContent(List<TvdbBanner> _bannerList)
    {
      XElement xml = new XElement("Banners");
      
      foreach (TvdbBanner b in _bannerList)
      {
        XElement banner = new XElement("Banner");
        if (b.GetType() == typeof(TvdbSeriesBanner))
        {
          TvdbSeriesBanner sb = (TvdbSeriesBanner)b;
          banner.Add(new XElement("id", sb.Id));
          banner.Add(new XElement("BannerPath", sb.BannerPath));
          banner.Add(new XElement("BannerType", "series"));
          banner.Add(new XElement("BannerType2", sb.BannerType));
          banner.Add(new XElement("Language", sb.Language.Abbriviation));
        }
        else if (b.GetType() == typeof(TvdbFanartBanner))
        {
          TvdbFanartBanner fb = (TvdbFanartBanner)b;
          banner.Add(new XElement("id", fb.Id));
          banner.Add(new XElement("BannerPath", fb.BannerPath));
          banner.Add(new XElement("BannerType", "fanart"));
          banner.Add(new XElement("BannerType2", fb.Resolution.X + "x" + fb.Resolution.Y));
          if (fb.Colors != null && fb.Colors.Count == 0)
          {
            StringBuilder colorString = new StringBuilder();
            colorString.Append("|");
            foreach (Color c in fb.Colors)
            {
              colorString.Append(c.R);
              colorString.Append(",");
              colorString.Append(c.G);
              colorString.Append(",");
              colorString.Append(c.B);
              colorString.Append("|");
            }
            banner.Add(new XElement("Colors", colorString.ToString()));
          }
          else
          {
            banner.Add(new XElement("Colors", ""));
          }
          banner.Add(new XElement("VignettePath", fb.VignettePath));
          banner.Add(new XElement("ThumbnailPath", fb.ThumbPath));
          banner.Add(new XElement("Language", fb.Language.Abbriviation));
        }
        else if (b.GetType() == typeof(TvdbSeasonBanner))
        {
          TvdbSeasonBanner sb = (TvdbSeasonBanner)b;
          banner.Add(new XElement("id", sb.Id));
          banner.Add(new XElement("BannerPath", sb.BannerPath));
          banner.Add(new XElement("BannerType", "season"));
          banner.Add(new XElement("BannerType2", sb.BannerType));
          banner.Add(new XElement("Language", sb.Language.Abbriviation));
          banner.Add(new XElement("Season", sb.Season));
        }
        else if (b.GetType() == typeof(TvdbPosterBanner))
        {
          TvdbPosterBanner pb = (TvdbPosterBanner)b;
          banner.Add(new XElement("id", pb.Id));
          banner.Add(new XElement("BannerPath", pb.BannerPath));
          banner.Add(new XElement("BannerType", "poster"));
          banner.Add(new XElement("BannerType2", pb.Resolution.X + "x" + pb.Resolution.Y));
          banner.Add(new XElement("Language", pb.Language.Abbriviation));
        }
        else
        {
          //this shouldn't happen, it's an invalid banner type (maybe new?) -> don't store it
          continue;
        }
        xml.Add(banner);
      }

      return xml.ToString();
    }

    /// <summary>
    /// Write the series banner contents to xml file
    /// </summary>
    /// <param name="_bannerList"></param>
    /// <param name="_path"></param>
    /// <returns></returns>
    internal bool WriteSeriesBannerContent(List<TvdbBanner> _bannerList, String _path)
    {
      String fileContent = CreateSeriesBannerContent(_bannerList);
      try
      {
        FileInfo info = new FileInfo(_path);
        if (!info.Directory.Exists) info.Directory.Create();
        File.WriteAllText(info.FullName, fileContent);
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }
  }
}
