using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TvdbConnector.Data
{
  [Serializable]
  public class TvdbSettings
  {

    private DateTime m_lastUpdated;

    public TvdbSettings(DateTime _lastUpdated)
    {
      m_lastUpdated = _lastUpdated;
    }

    public DateTime LastUpdated
    {
      get { return m_lastUpdated; }
      set { m_lastUpdated = value; }
    }
  }
}
