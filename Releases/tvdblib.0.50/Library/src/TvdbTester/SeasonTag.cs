using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TvdbConnector.Data.Banner;

namespace TvdbTester
{
  internal class SeasonTag
  {
    private List<TvdbSeasonBanner> m_bannerList;
    private int m_seasonNumber;
    private int m_seasonId;
    private TvdbSeasonBanner m_selectedBanner;
    private TvdbSeasonBanner m_selectedBannerWide;



    internal SeasonTag(int _season, int _seasonId, List<TvdbSeasonBanner> _bannerList)
    {
      m_bannerList = _bannerList;
      m_seasonNumber = _season;
      m_seasonId = _seasonId;
    }

    public TvdbSeasonBanner SelectedBannerWide
    {
      get { return m_selectedBannerWide; }
      set { m_selectedBannerWide = value; }
    }

    public TvdbSeasonBanner SelectedBanner
    {
      get { return m_selectedBanner; }
      set { m_selectedBanner = value; }
    }

    public int SeasonId
    {
      get { return m_seasonId; }
      set { m_seasonId = value; }
    }

    internal int SeasonNumber
    {
      get { return m_seasonNumber; }
      set { m_seasonNumber = value; }
    }

    internal List<TvdbSeasonBanner> BannerList
    {
      get { return m_bannerList; }
      set { m_bannerList = value; }
    }
  }
}
