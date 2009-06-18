using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TvdbLib;
using System.Threading;

namespace TvdbTester
{
  public partial class UpdateForm : Form
  {
    TvdbHandler m_tvdbHandler;
    TvdbHandler.Interval m_interval;
    bool m_useZip;
    Thread m_updateThread;

    public UpdateForm(TvdbHandler _handler, TvdbHandler.Interval _interval, bool _useZip)
    {
      InitializeComponent();
      m_tvdbHandler = _handler;
      m_interval = _interval;
      m_useZip = _useZip;
      cmdApply.Enabled = false;
      m_tvdbHandler.UpdateProgressed += new TvdbHandler.UpdateProgressDelegate(m_tvdbHandler_UpdateProgressed);
      m_tvdbHandler.UpdateFinished += new TvdbHandler.UpdateFinishedDelegate(m_tvdbHandler_UpdateFinished);
      m_updateThread = new Thread(new ThreadStart(DoUpdating));
      m_updateThread.Priority = ThreadPriority.BelowNormal;
      m_updateThread.Name = "Updating Thread";
      m_updateThread.Start();
    }

    private void DoUpdating()
    {
      m_tvdbHandler.UpdateAllSeries(m_interval, m_useZip);
    }

    void m_tvdbHandler_UpdateFinished(TvdbHandler.UpdateFinishedEventArgs _event)
    {

      UpdateFormFinishedThreadSafe(_event);
      //throw new NotImplementedException();
    }

    delegate void UpdateFormFinishedThreadSafeDelegate(TvdbHandler.UpdateFinishedEventArgs _event);
    void UpdateFormFinishedThreadSafe(TvdbHandler.UpdateFinishedEventArgs _event)
    {
      if (!InvokeRequired)
      {
        try
        {
          StringBuilder b = new StringBuilder();
          b.AppendLine("Update finished in " + (_event.UpdateFinished - _event.UpdateStarted).TotalSeconds + " seconds");
          b.AppendLine("Updated the following (" + _event.UpdatedSeries.Count + ") series");
          b.AppendLine("=========================================================");
          foreach (int s in _event.UpdatedSeries)
          {
            b.AppendLine(s.ToString());
          }
          b.AppendLine("");
          b.AppendLine("Updated the following (" + _event.UpdatedEpisodes.Count + ") episodes");
          b.AppendLine("=========================================================");
          foreach (int e in _event.UpdatedEpisodes)
          {
            b.AppendLine(e.ToString());
          }

          txtUpdateProgress.Text = b.ToString();
          cmdApply.Enabled = true;
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.ToString());
        }
      }
      else
        Invoke(new UpdateFormFinishedThreadSafeDelegate(UpdateFormFinishedThreadSafe), new object[] { _event });
    }

    delegate void UpdateFormThreadSafeDelegate(TvdbHandler.UpdateProgressEventArgs _event);
    void UpdateFormThreadSafe(TvdbHandler.UpdateProgressEventArgs _event)
    {
      try
      {
        if (!InvokeRequired)
        {

          switch (_event.CurrentUpdateStage)
          {
            case TvdbHandler.UpdateProgressEventArgs.UpdateStage.downloading:
              lblUpdateStage.Text = "Downloading updates";
              break;
            case TvdbHandler.UpdateProgressEventArgs.UpdateStage.seriesupdate:
              lblUpdateStage.Text = "Updating series";
              break;
            case TvdbHandler.UpdateProgressEventArgs.UpdateStage.episodesupdate:
              lblUpdateStage.Text = "Updating episode";
              break;
            case TvdbHandler.UpdateProgressEventArgs.UpdateStage.bannerupdate:
              lblUpdateStage.Text = "Updating banners";
              break;
            case TvdbHandler.UpdateProgressEventArgs.UpdateStage.finishupdate:
              lblUpdateStage.Text = "Updating Finished";
              break;
          }

          ibUpdateProgress.BarFillProcent = _event.CurrentStageProgress;
          txtUpdateProgress.Text = _event.CurrentUpdateDescription;

        }
        else
          Invoke(new UpdateFormThreadSafeDelegate(UpdateFormThreadSafe), new object[] { _event });
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
    }

    void m_tvdbHandler_UpdateProgressed(TvdbHandler.UpdateProgressEventArgs _event)
    {
      UpdateFormThreadSafe(_event);
    }

    private void cmdApply_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
    }

    private void cmdCancel_Click(object sender, EventArgs e)
    {
      m_tvdbHandler.AbortUpdate(true);
    }
  }
}
