using System;
using System.Collections.Generic;
using System.Text;

namespace TvdbConnector.Data
{
  /// <summary>
  /// Baseclass for a tvdb mirror. A mirror is defined in the tvdb xml by:
  /// <![CDATA[
  /// <?xml version="1.0" encoding="UTF-8" ?>
  /// <Mirrors>
  ///  <Mirror>
  ///    <id>1</id>
  ///    <mirrorpath>http://thetvdb.com</mirrorpath>
  ///    <typemask>7</typemask>
  ///  </Mirror>
  /// </Mirrors>
  /// ]]>
  /// </summary>
  [Serializable]
  public class TvdbMirror
  {
    #region private properties
    private int m_id;
    private Uri m_mirrorPath;
    private int m_typeMask;
    #endregion

    /// <summary>
    /// TvdbMirror constructor
    /// </summary>
    public TvdbMirror():this(-99,null, -99)
    {
    }

    /// <summary>
    /// TvdbMirror constructor
    /// </summary>
    /// <param name="_id">Id of the mirror</param>
    /// <param name="_mirror">Url to the mirror</param>
    /// <param name="_typeMask">Typemask of the mirror, see property "TypeMask"</param>
    public TvdbMirror(int _id, Uri _mirror, int _typeMask)
    {
      m_id = _id;
      m_mirrorPath = _mirror;
      m_typeMask = _typeMask;
    }

    /// <summary>
    /// Id of the mirror
    /// </summary>
    public int Id
    {
      get { return m_id; }
      set { m_id = value; }
    }

    /// <summary>
    /// The value of typemask is the sum of whichever file types that mirror holds:
    /// 1 xml files
    /// 2 banner files
    /// 4 zip files
    /// So, a mirror that has a typemask of 5 would hold XML and ZIP files, but no banner files. 
    /// </summary>
    public int TypeMask
    {
      get { return m_typeMask; }
      set { m_typeMask = value; }
    }

    ///<summary>
    /// Returns true if the mirror offers images for downloading, false otherwise
    ///</summary>
    public bool OffersImages
    {
      get
      {
        //todo: proper way to calculate capabilities of a mirror
        return (TypeMask == 2 || TypeMask == 6 || TypeMask == 5 || TypeMask == 7);
      }
    }

    ///<summary>
    /// Returns true if the mirror offers xml files for downloading, false otherwise
    ///</summary>
    public bool OffersXml
    {      
      get
      {
        //all uneven TypeMasks indicate the mirror offers xml files
        return ((TypeMask%2) == 1);
      }
    }

    ///<summary>
    /// Returns true if the mirror offers zipped downloads, false otherwise
    ///</summary>
    public bool OffersZip
    {
     get
     {
       //if typemask is 4 or higher, it has to contain zipped files (1+2=3 ;))
       return (TypeMask >= 4);
     }

    }

    /// <summary>
    /// Path to the mirror
    /// </summary>
    public Uri MirrorPath
    {
      get { return m_mirrorPath; }
      set { m_mirrorPath = value; }
    }
  }
}
