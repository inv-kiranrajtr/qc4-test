using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PB = Qc4Launcher.Forms.ProgressBar;

namespace Qc4Launcher.Util
{
	internal class ProgressBar
	{
		public delegate void OnWorkerMethodCompleteDelegate(double message, string status);
		public PB progress = null;
		public System.Windows.Window ParentWindow;
		public decimal percentage;
        
		protected void InitProgressBar(bool IsIndeterminate, System.Windows.Window parent=null)
		{
			progress = new PB(IsIndeterminate);
            progress.Owner = ParentWindow == null ? parent : ParentWindow;
			percentage = 0;
		}

        //-----added for Phase3-------
        protected void InitProgressBar(bool IsIndeterminate, BackgroundWorker worker, System.Windows.Window parent = null)
        {
            progress = new PB(IsIndeterminate);
            progress.Owner = ParentWindow == null ? parent : ParentWindow;
            percentage = 0;
            progress.thrd = worker;
            progress.Command_Cancel.Visibility = System.Windows.Visibility.Visible;
        }
        //------------------------------

        protected void OnWorkerMethodComplete(double value, string status)
		{
			progress.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
			new Action(
			delegate ()
			{
				progress.UpdateProgressBar(value, status);
			}
			));
		}

		protected void OnWorkerMethodComplete(double value, string status, bool isHide)
		{
			progress.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
			new Action(
			delegate ()
			{
				progress.UpdateProgressBar(value, status, isHide);
			}
			));
		}

		internal void UpdateProgressBar(decimal value, string status = "")
		{
			OnWorkerMethodComplete((double)value, status);
		}

		internal void UpdateProgressBar(decimal value, string status, bool isHide = false)
		{
			OnWorkerMethodComplete((double)value, status, isHide);
		}
	}
}
