using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TvdbConnector.Data.Banner;

namespace TvdbTester
{
  internal class SeasonImageList
  {
    internal SeasonImageList(List<TvdbSeasonBanner> _banners, int _currentIndex)
    {
      m_banners = _banners;
      m_currentIndex = _currentIndex;
    }

    internal SeasonImageList(List<TvdbSeasonBanner> _banners,  int _currentIndex, int _season)
    {
      m_banners = _banners;
      m_currentIndex = _currentIndex;
      m_season = _season;
    }

    private List<TvdbSeasonBanner> m_banners;
    private int m_currentIndex;
    private int m_season;

    public int Season
    {
      get { return m_season; }
      set { m_season = value; }
    }

    public int CurrentIndex
    {
      get { return m_currentIndex; }
      set { m_currentIndex = value; }
    }
    internal List<TvdbSeasonBanner> Banners
    {
      get { return m_banners; }
      set { m_banners = value; }
    }
  }
}
