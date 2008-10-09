using System;

namespace TvdbConnector.Exceptions
{
  /// <summary>
  /// Exception that is thrown when a nonexistent content is requested
  /// </summary>
  public class TvdbContentNotFoundException: TvdbException
  {
        /// <summary>
    /// TvdbInvalidAPIKeyException constructor
    /// </summary>
    /// <param name="_text"></param>
    public TvdbContentNotFoundException(String _text)
      : base(_text)
    {
    }

    /// <summary>
    /// TvdbInvalidAPIKeyException constructor
    /// </summary>
    public TvdbContentNotFoundException()
      : base()
    {

    }
  }
}
