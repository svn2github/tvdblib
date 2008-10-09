using System;

namespace TvdbConnector.Exceptions
{
	/// <summary>
	/// Description of TvdbInvalidXmlException.
	/// </summary>
	public class TvdbInvalidXmlException: TvdbException
	{
		/// <summary>
		/// TvdbInvalidXmlException constructor
		/// </summary>
		/// <param name="_text"></param>
		public TvdbInvalidXmlException(String _text): base(_text)
		{
			
		}
		
		/// <summary>
		/// TvdbInvalidXmlException constructor
		/// </summary>
		public TvdbInvalidXmlException(): base()
		{
			
		}
	}
}
