using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TvdbLib.Data.Banner;
using TvdbLib.Data;

namespace TvdbTester
{
  public partial class FullscreenForm : Form
  {
    public FullscreenForm()
    {
      InitializeComponent();
    }
    public Image Banner
    {
      set
      {
        pbImage.Image = value;
        this.Size = new Size(value.Width, value.Height + 24);
      }
      get
      {
        return pbImage.Image;
      }
    }
  }
}
