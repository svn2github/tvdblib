using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TvdbConnector.Data.Banner
{
  /// <summary>
  /// Newest addition to the graphical section. Like the name says it has poster
  /// format (680px x 1000px) and is not smaller than 500 kb
  /// 
  /// More information at http://thetvdb.com/wiki/index.php/Posters
  /// </summary>
  [Serializable]
  public class TvdbPosterBanner: TvdbBanner
  {
    private Point m_resolution;

    public Point Resolution
    {
      get { return m_resolution; }
      set { m_resolution = value; }
    }
  }
}
