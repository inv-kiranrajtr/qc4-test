using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgressBarForm = Qc4Launcher.Forms.ProgressBar;

namespace Qc4Launcher.Forms.QCM
{
    class ProgressUpdate
    {
        public  ProgressBarForm progress = null;
        public delegate void OnWorkerMethodCompleteDelegate(double message, string status);
        private event OnWorkerMethodCompleteDelegate OnWorkerComplete;
        public double prec;

        public ProgressUpdate(System.Windows.Window parentWindow)
        {
            progress = new ProgressBarForm();
            progress.Owner = parentWindow;
            OnWorkerComplete += new ProgressUpdate.OnWorkerMethodCompleteDelegate(OnWorkerMethodComplete);
            prec = 0;
        }

        public void OnWorkerMethodComplete(double value, string status)
        {
            progress.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
            new Action(
            delegate ()
            {
                progress.UpdateProgressBar(value, status);
            }
            ));
        }

        public void UpdateProgress(double value, string message = "")
        {
            OnWorkerComplete(value, message);
        }
    }
}
