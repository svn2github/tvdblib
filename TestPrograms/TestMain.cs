using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestPrograms
{
  public partial class TestMain : Form
  {
    public TestMain()
    {
      InitializeComponent();
    }

    private void cmdShowUpdateTestForm_Click(object sender, EventArgs e)
    {
      UpdatingTestForm updateForm = new UpdatingTestForm();
      updateForm.Show();
    }

    private void cmdShowMemoryTestForm_Click(object sender, EventArgs e)
    {
      MemoryTest memoryForm = new MemoryTest();
      memoryForm.Show();
    }

    private void cmdShowMainTest_Click(object sender, EventArgs e)
    {
      TestMain mainForm = new TestMain();
      mainForm.Show();
    }

    private void cmdShowImageTestForm_Click(object sender, EventArgs e)
    {
      ImageTestForm imageForm = new ImageTestForm();
      imageForm.Show();
    }
  }
}
