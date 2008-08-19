using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TvdbConnector
{
  public class TvdbUserNotFoundException: Exception
  {
    public TvdbUserNotFoundException(String _text): base(_text)
    {
    }
  }
}
