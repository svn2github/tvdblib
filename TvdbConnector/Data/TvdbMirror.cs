using System;
using System.Collections.Generic;
using System.Text;

namespace TvdbConnector.Data
{
  /// <summary>
  /// Baseclass for a tvdb mirror
  /// </summary>
  [Serializable]
  public class TvdbMirror
  {
    private int m_id;
    private Uri m_mirrorPath;
    private int m_typeMask;

    public TvdbMirror():this(-99,null, -99)
    {
    }
    public TvdbMirror(int _id, Uri _mirror, int _typeMask)
    {
      m_id = _id;
      m_mirrorPath = _mirror;
      m_typeMask = _typeMask;
    }

    public int TypeMask
    {
      get { return m_typeMask; }
      set { m_typeMask = value; }
    }

    public Uri MirrorPath
    {
      get { return m_mirrorPath; }
      set { m_mirrorPath = value; }
    }

    public int Id
    {
      get { return m_id; }
      set { m_id = value; }
    }
  }
}
