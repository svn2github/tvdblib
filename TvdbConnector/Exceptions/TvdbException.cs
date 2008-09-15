using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TvdbConnector.Exceptions
{
  /// <summary>
  /// Base excpetion for tvdblib 
  /// </summary>
  public class TvdbException: Exception
  {
        /// <summary>
    /// TvdbException constructor
    /// </summary>
    /// <param name="_text"></param>
    public TvdbException(String _text)
      : base(_text)
    {
    }

    /// <summary>
    /// TvdbException constructor
    /// </summary>
    public TvdbException()
      : base()
    {
    }
  }
}
