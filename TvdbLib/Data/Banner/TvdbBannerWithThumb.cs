/*
 *   TvdbLib: A library to retrieve information and media from http://thetvdb.com
 * 
 *   Copyright (C) 2008  Benjamin Gmeiner
 * 
 *   This program is free software: you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation, either version 3 of the License, or
 *   (at your option) any later version.
 *
 *   This program is distributed in the hope that it will be useful,
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *   GNU General Public License for more details.
 *
 *   You should have received a copy of the GNU General Public License
 *   along with this program.  If not, see <http://www.gnu.org/licenses/>.
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Net;

namespace TvdbLib.Data.Banner
{
  /// <summary>
  /// This class extends the regular banner class with the ability to retrieve thumbnails of the actual images.
  /// 
  /// These thumbnails are at the moment availabe for all banner types except actors
  /// </summary>
  public class TvdbBannerWithThumb: TvdbBanner
  {
    #region private fields
    private String m_thumbPath;
    private Image m_bannerThumb;
    private bool m_thumbLoaded;
    private bool m_thumbLoading;
    private System.Object m_thumbLoadingLock = new System.Object();
    #endregion


    /// <summary>
    /// Is the thumbnail currently beeing loaded
    /// </summary>
    public bool ThumbLoading
    {
      get { return m_thumbLoading; }
      set { m_thumbLoading = value; }
    }

    /// <summary>
    /// Path to the fanart thumbnail
    /// </summary>
    public String ThumbPath
    {
      get { return m_thumbPath; }
      set { m_thumbPath = value; }
    }

    /// <summary>
    /// Image of the thumbnail
    /// </summary>
    public Image BannerThumb
    {
      get { return m_bannerThumb; }
      set { m_bannerThumb = value; }
    }


    /// <summary>
    /// Load the thumb from tvdb, if there isn't already a thumb loaded,
    /// (an existing one will NOT be replaced)
    /// </summary>
    /// <see cref="LoadThumb(bool _replaceOld)"/>
    /// <returns>true if the loading completed sccessfully, false otherwise</returns>
    public bool LoadThumb()
    {
      return LoadThumb(false);
    }

    /// <summary>
    /// Load the thumb from tvdb
    /// </summary>
    /// <param name="_replaceOld">if true, an existing banner will be replaced, 
    /// if false the banner will only be loaded if there is no existing banner</param>
    /// <returns>true if the loading completed sccessfully, false otherwise</returns>
    public bool LoadThumb(bool _replaceOld)
    {
      bool wasLoaded = m_thumbLoaded;//is the banner already loaded at this point
      lock (m_thumbLoadingLock)
      {//if another thread is already loading THIS banner, the lock will block this thread until the other thread
        //has finished loading
        if (!wasLoaded && !_replaceOld && m_thumbLoaded)
        {////the banner has already been loaded from a different thread and we don't want to replace it
          return false;
        }
        m_thumbLoading = true;

        /*
         * every banner (except actors) has a cached thumbnail on tvdb... The path to the thumbnail
         * is only given for fanart banners via the api, however every thumbnail path is "_cache/" + image_path
         * so if no path for the thumbnail is given, it is assumed that there is a thumbnail at that location
         * 
         * TODO: wait for forum response regarding this matter
         */
        if (m_thumbPath == null && (BannerPath != null || BannerPath.Equals("")))
        {
          m_thumbPath = String.Concat("_cache/", BannerPath);
        }

        if (m_thumbPath != null)
        {
          try
          {
            Image img = LoadImage(TvdbLinks.CreateBannerLink(m_thumbPath));

            if (img != null)
            {
              m_bannerThumb = img;
              m_thumbLoaded = true;
              m_thumbLoading = false;
              return true;
            }
          }
          catch (WebException ex)
          {
            Log.Error("Couldn't load banner thumb" + m_thumbPath, ex);
          }
        }
        m_thumbLoaded = false;
        m_thumbLoading = false;
        return false;
      }
    }

    /// <summary>
    /// Load thumbnail with given image
    /// </summary>
    /// <param name="_img">the image to be used forthe banner</param>
    /// <returns>true if the loading completed sccessfully, false otherwise</returns>
    public bool LoadThumb(Image _img)
    {
      if (_img != null)
      {
        m_bannerThumb = _img;
        m_thumbLoaded = true;
        return true;
      }
      else
      {
        m_thumbLoaded = false;
        return false;
      }
    }

    /// <summary>
    /// Is the Image of the thumb already loaded
    /// </summary>
    public bool IsThumbLoaded
    {
      get { return m_thumbLoaded; }
    }
  }
}
