using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TvdbConnector.Exceptions
{
  /// <summary>
  /// Exception thrown when a request is made which requires a valid
  /// api key but none is set
  /// </summary>
  public class TvdbInvalidApiKeyException : Exception
  {
    /// <summary>
    /// TvdbInvalidAPIKeyException constructor
    /// </summary>
    /// <param name="_text"></param>
    public TvdbInvalidApiKeyException(String _text)
      : base(_text)
    {
    }

    /// <summary>
    /// TvdbInvalidAPIKeyException constructor
    /// </summary>
    public TvdbInvalidApiKeyException()
      : base()
    {

    }
  }
}
