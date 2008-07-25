using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TvdbConnector.Data;

namespace TvdbConnector.Cache
{
  public interface ICacheProvider
  {
    CachableContent LoadFromCache();
    void SaveToCache(CachableContent _content);

  }
}
