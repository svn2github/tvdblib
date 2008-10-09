using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TvdbConnector.Exceptions
{
  /// <summary>
  /// Exception that is thrown if http://thetvdb.com seems to be unavailable
  /// </summary>
  public class TvdbNotAvailableException : TvdbException
  {
    /// <summary>
    /// TvdbNotAvailableException constructor
    /// </summary>
    /// <param name="_text"></param>
    public TvdbNotAvailableException(String _text)
      : base(_text)
    {
    }

    /// <summary>
    /// TvdbNotAvailableException constructor
    /// </summary>
    public TvdbNotAvailableException()
      : base()
    {
    }
  }
}
