using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TvdbConnector.Data.Banner
{
  /// <summary>
  /// Fan Art is high quality artwork that is displayed in the background of 
  /// HTPC menus. Since fan art is displayed behind other content in most cases, 
  /// we place more restrictions on the formatting of the image. 
  /// 
  /// The resolution is either 1920x1080 or 1280x720...
  /// 
  /// More information: http://thetvdb.com/wiki/index.php/Fan_Art
  /// </summary>
  [Serializable]
  public class TvdbFanartBanner : TvdbBanner
  {
    private String m_thumbPath;
    private String m_vignettePath;

    private Image m_bannerThumb;
    private Image m_vignette;
    private Point m_resolution;
    private List<Color> m_colors;

    private bool m_thumbLoaded;

    public List<Color> Colors
    {
      get { return m_colors; }
      set { m_colors = value; }
    }
    private Color m_color1;
    private Color m_color2;
    private Color m_color3;

    public TvdbFanartBanner()
    {

    }
    public TvdbFanartBanner(int _id, String _path, TvdbLanguage _lang)
    {
      this.Id = _id;
      this.BannerPath = _path;
      this.Language = _lang;
    }
    
    public String VignettePath
    {
      get { return m_vignettePath; }
      set { m_vignettePath = value; }
    }

    public String ThumbPath
    {
      get { return m_thumbPath; }
      set { m_thumbPath = value; }
    }
    public Color Color3
    {
      get { return m_color3; }
      set { m_color3 = value; }
    }

    public Color Color2
    {
      get { return m_color2; }
      set { m_color2 = value; }
    }

    public Color Color1
    {
      get { return m_color1; }
      set { m_color1 = value; }
    }

    public Point Resolution
    {
      get { return m_resolution; }
      set { m_resolution = value; }
    }

    public Image BannerThumb
    {
      get { return m_bannerThumb; }
      set { m_bannerThumb = value; }
    }


    public bool LoadThumb()
    {
      try
      {
        Image img = LoadImage(TvdbLinks.CreateBannerLink(m_thumbPath));

        if (img != null)
        {
          m_bannerThumb = img;
          m_thumbLoaded = true;
          return true;
        }
        else
        {
          m_thumbLoaded = false;
          return false;
        }
      }
      catch (Exception ex)
      {
        Log.Error("Couldn't load banner thumb" + m_thumbPath, ex);
        m_thumbLoaded = false;
        return false;
      }
    }

    public bool IsThumbLoaded
    {
      get
      {
        return m_thumbLoaded;
      }
    }
  }
}
