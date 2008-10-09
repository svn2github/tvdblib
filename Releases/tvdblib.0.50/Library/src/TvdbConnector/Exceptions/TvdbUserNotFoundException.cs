using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TvdbConnector.Exceptions
{
  /// <summary>
  /// Exception thrown when no user has been found
  /// </summary>
  public class TvdbUserNotFoundException : TvdbException
  {
    /// <summary>
    /// TvdbUserNotFoundException constructor
    /// </summary>
    /// <param name="_text"></param>
    public TvdbUserNotFoundException(String _text): base(_text)
    {
    }
  }
}
