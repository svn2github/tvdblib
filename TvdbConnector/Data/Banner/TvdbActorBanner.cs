using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TvdbConnector.Data.Banner
{
  /// <summary>
  /// An actor poster
  ///     *  Actor images must be 300px x 450px and must fill the entire image. Do not add black bars to the sides to get it to that size.
  ///         * Actor images must be smaller than 100kb
  ///         * Low quality images should not be scaled up to fit the resolution. Use only high quality art.
  ///         * Actor images should show the actor in that particular role, wearing the clothes/makeup they'd wear on the series. Unless it's a cartoon, in which case just a normal picture of the voice actor will do.
  ///         * Try to shy away from full body shots. Ideally include some upper body but don't go to far past the waist.
  ///         * No nudity, even if the actor is playing the role of a striper who is almost always nude, the images must be family safe. 
  /// </summary>
  [Serializable]
  public class TvdbActorBanner: TvdbBanner
  {

  }
}
