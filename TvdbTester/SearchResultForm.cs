using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TvdbConnector.Data;

namespace TvdbTester
{
  public partial class SearchResultForm : Form
  {
    List<TvdbSearchResult> m_results;
    private DialogResult m_result = DialogResult.Cancel;
    private TvdbSearchResult m_selection = null;

    public SearchResultForm(List<TvdbSearchResult> _searchResults)
    {
      InitializeComponent();

      m_results = _searchResults;
      foreach (TvdbSearchResult r in _searchResults)
      {
        ListViewItem item = new ListViewItem(r.Id.ToString());
        item.SubItems.Add(r.SeriesName);
        item.SubItems.Add(r.Language.Name);
        item.Tag = r;
        lvSearchResult.Items.Add(item);
      }
    }

    private void cmdChoose_Click(object sender, EventArgs e)
    {
      if (m_selection == null)
      {
        lblStatus.Text = "Please select a series first";
      }
      else
      {
        this.DialogResult = DialogResult.OK;
        this.Close();
      }
    }

    private void cmdCancel_Click(object sender, EventArgs e)
    {
      m_result = DialogResult.Cancel;
      this.Close();
    }


    public TvdbSearchResult Selection
    {
      get { return m_selection; }
      set { m_selection = value; }
    }

    private void lvSearchResult_AfterLabelEdit(object sender, LabelEditEventArgs e)
    {

    }

    private void lvSearchResult_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (lvSearchResult.SelectedItems.Count == 1)
      {
        m_selection = (TvdbSearchResult)lvSearchResult.SelectedItems[0].Tag;
        bcSeriesBanner.ClearControl();
        if (m_selection != null)
        {
          bcSeriesBanner.BannerImage = m_selection.Banner;
        }

        txtOverview.Text = m_selection.Overview;
        linkImdb.Text = m_selection.ImdbId.Equals("")? "": "http://www.imdb.com/title/" + m_selection.ImdbId;
        txtFirstAired.Text = m_selection.FirstAired.ToShortDateString();
      }
    }

  }
}
