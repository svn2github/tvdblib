using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TvdbConnector.Data;
using TvdbConnector.Data.Banner;

namespace TvdbConnector.Xml
{
  internal class TvdbXmlReader
  {
    internal TvdbXmlReader()
    {

    }

    internal List<TvdbLanguage> ExtractLanguages(String _data)
    {
      XDocument xml = XDocument.Parse(_data);

      var allLanguages = from language in xml.Descendants("Language")
                         select new
                         {
                           name = language.Element("name").Value,
                           abbreviation = language.Element("abbreviation").Value,
                           id = language.Element("id").Value
                         };

      List<TvdbLanguage> retList = new List<TvdbLanguage>();
      foreach (var l in allLanguages)
      {
        TvdbLanguage lang = new TvdbLanguage();
        lang.Name = l.name;
        lang.Abbriviation = l.abbreviation;
        lang.Id = Util.Int32Parse(l.id);

        if (lang.Id != -99) retList.Add(lang);
      }
      return retList;
    }

    internal List<TvdbMirror> ExtractMirrors(String _data)
    {
      XDocument xml = XDocument.Parse(_data);

      var allLanguages = from language in xml.Descendants("Mirror")
                         select new
                         {
                           typemask = language.Element("typemask").Value,
                           mirrorpath = language.Element("mirrorpath").Value,
                           id = language.Element("id").Value
                         };

      List<TvdbMirror> retList = new List<TvdbMirror>();
      foreach (var l in allLanguages)
      {
        TvdbMirror lang = new TvdbMirror();
        lang.MirrorPath = new Uri(l.mirrorpath);
        lang.TypeMask = Util.Int32Parse(l.typemask);
        lang.Id = Util.Int32Parse(l.id);

        if (lang.Id != -99) retList.Add(lang);
      }
      return retList;
    }

    internal List<TvdbSeries> ExtractSeries(String _data)
    {
      XDocument xml = XDocument.Parse(_data);

      var allSeries = from series in xml.Descendants("Series")
                      select new
                      {
                        Id = series.Element("id").Value,
                        Actors = series.Element("Actors").Value,
                        Airs_DayOfWeek = series.Element("Airs_DayOfWeek").Value,
                        Airs_Time = series.Element("Airs_Time").Value,
                        ContentRating = series.Element("ContentRating").Value,
                        FirstAired = series.Element("FirstAired").Value,
                        Genre = series.Element("Genre").Value,
                        IMDB_ID = series.Element("IMDB_ID").Value,
                        Language = series.Element("Language").Value,
                        Network = series.Element("Network").Value,
                        Overview = series.Element("Overview").Value,
                        Rating = series.Element("Rating").Value,
                        Runtime = series.Element("Runtime").Value,
                        SeriesID = series.Element("SeriesID").Value,
                        SeriesName = series.Element("SeriesName").Value,
                        Status = series.Element("Status").Value,
                        banner = series.Element("banner").Value,
                        fanart = series.Element("fanart").Value,
                        lastupdated = series.Element("lastupdated").Value,
                        zap2it_id = series.Element("zap2it_id").Value
                      };

      List<TvdbSeries> retList = new List<TvdbSeries>();
      foreach (var s in allSeries)
      {
        TvdbSeries series = new TvdbSeries();
        series.Id = Util.Int32Parse(s.Id);
        series.Actors = Util.SplitTvdbString(s.Actors);
        series.AirsDayOfWeek = Util.GetDayOfWeek(s.Airs_DayOfWeek);
        series.AirsTime = s.Airs_Time.Equals("") ? new DateTime(1, 1, 1) : DateTime.Parse(s.Airs_Time.Replace(".", ":"));
        series.ContentRating = s.ContentRating;
        series.FirstAired = s.FirstAired.Equals("") ? new DateTime() : DateTime.Parse(s.FirstAired);
        series.Genre = Util.SplitTvdbString(s.Genre);
        series.ImdbId = s.IMDB_ID;
        series.Language = Util.ParseLanguage(s.Language);
        series.Network = s.Network;
        series.Overview = s.Overview;
        series.Rating = Util.DoubleParse(s.Rating);
        series.Runtime = Util.DoubleParse(s.Runtime);
        series.TvDotComId = Util.Int32Parse(s.SeriesID);
        series.SeriesName = s.SeriesName;
        series.Status = s.Status;
        if (!s.banner.Equals(""))
        {
          series.Banners.Add(new TvdbSeriesBanner(series.Id, s.banner, series.Language, TvdbSeriesBanner.Type.graphical));
        }

        if (!s.fanart.Equals(""))
        {
          series.Banners.Add(new TvdbFanartBanner(series.Id, s.fanart, series.Language));
        }

        series.LastUpdated = Util.UnixToDotNet(s.lastupdated);
        series.Zap2itId = s.zap2it_id;
        if (series.Id != -99) retList.Add(series);
      }
      return retList;
    }

    internal List<TvdbEpisode> ExtractEpisodes(String _data)
    {
      XDocument xml = XDocument.Parse(_data);
      var allEpisodes = from episode in xml.Descendants("Episode")
                        select new
                        {
                          Id = episode.Element("id").Value,
                          Combined_episodenumber = episode.Element("Combined_episodenumber").Value,
                          Combined_season = episode.Element("Combined_season").Value,
                          DVD_chapter = episode.Element("DVD_chapter").Value,
                          DVD_discid = episode.Element("DVD_discid").Value,
                          DVD_episodenumber = episode.Element("DVD_episodenumber").Value,
                          DVD_season = episode.Element("DVD_season").Value,
                          Director = episode.Element("Director").Value,
                          EpisodeName = episode.Element("EpisodeName").Value,
                          EpisodeNumber = episode.Element("EpisodeNumber").Value,
                          FirstAired = episode.Element("FirstAired").Value,
                          GuestStars = episode.Element("GuestStars").Value,
                          IMDB_ID = episode.Element("IMDB_ID").Value,
                          Language = episode.Element("Language").Value,
                          Overview = episode.Element("Overview").Value,
                          ProductionCode = episode.Element("ProductionCode").Value,
                          Rating = episode.Element("Rating").Value,
                          SeasonNumber = episode.Element("SeasonNumber").Value,
                          Writer = episode.Element("Writer").Value,
                          absolute_number = episode.Element("absolute_number").Value,
                          filename = episode.Element("filename").Value,
                          lastupdated = episode.Element("lastupdated").Value,
                          seasonid = episode.Element("seasonid").Value,
                          seriesid = episode.Element("seriesid").Value,
                          airsafter_season = episode.Elements("airsafter_season").Count() == 1 
                                           ? episode.Element("airsafter_season").Value : "-99",
                          airsbefore_episode = episode.Elements("airsbefore_episode").Count() == 1
                                             ? episode.Element("airsbefore_episode").Value : "-99",
                          airsbefore_season = episode.Elements("airsbefore_season").Count() == 1
                                            ? episode.Element("airsbefore_season").Value : "-99"
                        };

      List<TvdbEpisode> retList = new List<TvdbEpisode>();
      foreach (var e in allEpisodes)
      {
        TvdbEpisode ep = new TvdbEpisode();
        ep.Id = Util.Int32Parse(e.Id);
        ep.CombinedEpisodeNumber = Util.Int32Parse(e.Combined_episodenumber);
        ep.CombinedSeason = Util.Int32Parse(e.Combined_season);
        ep.DvdChapter = Util.Int32Parse(e.DVD_chapter);
        ep.DvdDiscId = Util.Int32Parse(e.DVD_discid);
        ep.DvdEpisodeNumber = Util.Int32Parse(e.DVD_episodenumber);
        ep.DvdSeason = Util.Int32Parse(e.DVD_season);
        ep.Directors = Util.SplitTvdbString(e.Director);
        ep.EpisodeName = e.EpisodeName;
        ep.EpisodeNumber = Util.Int32Parse(e.EpisodeNumber);
        ep.AirsAfterSeason = Util.Int32Parse(e.airsafter_season);
        ep.AirsBeforeEpisode = Util.Int32Parse(e.airsbefore_episode);
        ep.AirsBeforeSeason = Util.Int32Parse(e.airsbefore_season);
        try
        {
          ep.FirstAired = e.FirstAired.Equals("") ? new DateTime(1, 1, 1) : DateTime.Parse(e.FirstAired);
        }
        catch (Exception)
        {
          ep.FirstAired = new DateTime();
        }
        ep.GuestStars = Util.SplitTvdbString(e.GuestStars);
        ep.ImdbId = e.IMDB_ID;
        ep.Language = Util.ParseLanguage(e.Language);
        ep.Overview = e.Overview;
        ep.ProductionCode = e.ProductionCode;
        ep.Rating = Util.DoubleParse(e.Rating);
        ep.SeasonNumber = Util.Int32Parse(e.SeasonNumber);
        ep.Writer = Util.SplitTvdbString(e.Writer);
        ep.AbsoluteNumber = Util.Int32Parse(e.absolute_number);
        ep.Banner = new TvdbEpisodeBanner(Util.Int32Parse(e.Id), e.filename);
        ep.LastUpdated = Util.UnixToDotNet(e.lastupdated);
        ep.SeasonId = Util.Int32Parse(e.seasonid);
        ep.SeriesId = Util.Int32Parse(e.seriesid);

        if (ep.Id != -99) retList.Add(ep);
      }

      return retList;

    }


    internal List<TvdbSeries> ExtractSeriesUpdates(String _data)
    {

      XDocument xml = XDocument.Parse(_data);

      var allSeries = from series in xml.Descendants("Series")
                      where series.HasElements == true
                      select new TvdbSeries
                      {
                        Id = Util.Int32Parse(series.Element("id").Value),
                        LastUpdated = Util.UnixToDotNet(series.Element("time").Value)
                      };

      List<TvdbSeries> retList = new List<TvdbSeries>();
      foreach (TvdbSeries s in allSeries)
      {
        if (s != null && s.Id != -99) retList.Add(s);
      }

      return retList;
    }

    internal List<TvdbSearchResult> ExtractSeriesSearchResults(String _data)
    {

      XDocument xml = XDocument.Parse(_data);

      var allSeries = from series in xml.Descendants("Series")
                      where series.HasElements == true
                      select new
                      {
                        Id = Util.Int32Parse(series.Element("seriesid").Value),
                        FirstAired = series.Element("FirstAired") != null ? series.Element("FirstAired").Value : "",
                        Language = series.Element("language") != null ? series.Element("language").Value : "",
                        Overview = series.Element("Overview") != null ? series.Element("Overview").Value : "",
                        SeriesName = series.Element("SeriesName") != null ? series.Element("SeriesName").Value : "",
                        IMDB_ID = series.Element("IMDB_ID") != null ? series.Element("IMDB_ID").Value : "",
                        BannerPath = series.Element("banner") != null ? series.Element("banner").Value : ""
                      };

      List<TvdbSearchResult> retList = new List<TvdbSearchResult>();
      foreach (var s in allSeries)
      {
        TvdbSearchResult res = new TvdbSearchResult();
        res.Id = s.Id;
        res.ImdbId = s.IMDB_ID;
        if (!s.FirstAired.Equals("")) res.FirstAired = DateTime.Parse(s.FirstAired);
        if (!s.Language.Equals("")) res.Language = Util.ParseLanguage(s.Language);
        res.SeriesName = s.SeriesName;
        res.Overview = s.Overview;
        if (!s.BannerPath.Equals("")) res.Banner = new TvdbSeriesBanner(0, s.BannerPath, null, TvdbSeriesBanner.Type.none);
        retList.Add(res);
      }

      return retList;
    }

    internal List<int> ExtractSeriesFavorites(String _data)
    {

      XDocument xml = XDocument.Parse(_data);

      var allSeries = from series in xml.Descendants("Series")
                      select new
                      {
                        Id = Util.Int32Parse(series.Value),
                      };

      List<int> retList = new List<int>();
      foreach (var s in allSeries)
      {
        if (s.Id != -99) retList.Add(s.Id);
      }

      return retList;
    }

    internal double ExtractRating(String _data)
    {
      XDocument xml = XDocument.Parse(_data);

      var ratings = from series in xml.Descendants("Rating")
                      select new
                      {
                        rating = series.Value
                      };
      if (ratings.Count() == 1 && ratings.ElementAt(0).rating != null)
      {
        return Util.DoubleParse(ratings.ElementAt(0).rating);
      }
      else
      {
        return -99;
      }
    }

    internal List<TvdbEpisode> ExtractEpisodeUpdates(String _data)
    {
      XDocument xml = XDocument.Parse(_data);
      var allEpisodes = from episode in xml.Descendants("Episode")
                        select new TvdbEpisode
                        {
                          Id = Util.Int32Parse(episode.Element("id").Value),
                          LastUpdated = Util.UnixToDotNet(episode.Element("time").Value),
                          SeriesId = Util.Int32Parse(episode.Element("Series").Value)
                        };

      List<TvdbEpisode> retList = new List<TvdbEpisode>();
      foreach (TvdbEpisode e in allEpisodes)
      {
        if (e.Id != -99) retList.Add(e);
      }

      return retList;

    }


    internal DateTime ExtractUpdateTime(string _data)
    {
      XDocument xml = XDocument.Parse(_data);
      var updateTime = from episode in xml.Descendants("Data")
                       select new
                       {
                         time = episode.Attribute("time").Value
                       };
      foreach (var d in updateTime)
      {
        if (d.time != "")
        {
          return Util.UnixToDotNet(d.time);
        }
      }
      return new DateTime(1, 1, 1);
    }


    internal List<TvdbBanner> ExtractBanners(String _data)
    {
      XDocument xml = XDocument.Parse(_data);
      List<TvdbBanner> retList = new List<TvdbBanner>();
      var allEpisodes = from episode in xml.Descendants("Banner")
                        where episode.Element("BannerType").Value.Equals("fanart")
                        select new TvdbFanartBanner
                        {

                          Id = Util.Int32Parse(episode.Element("id").Value),
                          BannerPath = episode.Element("BannerPath").Value,
                          VignettePath = episode.Element("VignettePath").Value,
                          ThumbPath = episode.Element("ThumbnailPath").Value,
                          Resolution = Util.ParseResolution(episode.Element("BannerType2").Value),
                          Colors = Util.ParseColors(episode.Element("Colors").Value),
                          Language = Util.ParseLanguage(episode.Element("Language").Value)

                          //LastUpdated = Util.UnixToDotNet(episode.Element("time").Value),
                          // SeriesId = Util.Int32Parse(episode.Element("Series").Value)
                        };

      foreach (TvdbBanner e in allEpisodes)
      {
        if (e.Id != -99) retList.Add(e);
      }

      var allBanners = from banner in xml.Descendants("Banner")
                       where banner.Element("BannerType").Value.Equals("season")
                       select new TvdbSeasonBanner
                       {

                         Id = Util.Int32Parse(banner.Element("id").Value),
                         BannerPath = banner.Element("BannerPath").Value,
                         Season = Util.Int32Parse(banner.Element("Season").Value),
                         BannerType = Util.ParseSeasonBannerType(banner.Element("BannerType2").Value),
                         Language = Util.ParseLanguage(banner.Element("Language").Value)
                       };

      foreach (TvdbBanner e in allBanners)
      {
        if (e.Id != -99) retList.Add(e);
      }

      var allBanners2 = from banner in xml.Descendants("Banner")
                        where banner.Element("BannerType").Value.Equals("series")
                        select new TvdbSeriesBanner
                        {

                          Id = Util.Int32Parse(banner.Element("id").Value),
                          BannerPath = banner.Element("BannerPath").Value,
                          BannerType = Util.ParseSeriesBannerType(banner.Element("BannerType2").Value),
                          Language = Util.ParseLanguage(banner.Element("Language").Value)
                        };

      foreach (TvdbBanner e in allBanners2)
      {
        if (e.Id != -99) retList.Add(e);
      }

      var allPosters = from banner in xml.Descendants("Banner")
                       where banner.Element("BannerType").Value.Equals("poster")
                       select new TvdbPosterBanner
                       {

                         Id = Util.Int32Parse(banner.Element("id").Value),
                         BannerPath = banner.Element("BannerPath").Value,
                         Resolution = Util.ParseResolution(banner.Element("BannerType2").Value),
                         Language = Util.ParseLanguage(banner.Element("Language").Value)
                       };

      foreach (TvdbPosterBanner e in allPosters)
      {
        if (e.Id != -99) retList.Add(e);
      }

      return retList;

    }

    internal List<TvdbActor> ExtractActors(String _data)
    {
      XDocument xml = XDocument.Parse(_data);
      List<TvdbBanner> retList = new List<TvdbBanner>();
      var allActors = from episode in xml.Descendants("Actor")
                        select new
                        {

                          Id = episode.Element("id").Value,
                          Image = episode.Element("Image").Value,
                          Name = episode.Element("Name").Value,
                          Role = episode.Element("Role").Value,
                          SortOrder = episode.Element("SortOrder").Value
                        };
      List<TvdbActor> actorList = new List<TvdbActor>();
      foreach (var a in allActors)
      {
        TvdbActor actor = new TvdbActor();
        actor.Id = Util.Int32Parse(a.Id);
        actor.Name = a.Name;
        actor.Role = a.Role;
        actor.SortOrder = Util.Int32Parse(a.SortOrder);

        TvdbActorBanner banner = new TvdbActorBanner();
        banner.Id = actor.Id;
        banner.BannerPath = a.Image;
        actor.ActorImage = banner;
        if (actor.Id != -99)
        {
          actorList.Add(actor);
        }
      }
      return actorList;
    }
  }
}
