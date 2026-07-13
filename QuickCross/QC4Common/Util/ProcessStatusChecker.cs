using log4net;
using Microsoft.Office.Interop.Excel;
using QC4Common.Common;
using System;
using System.IO;
using System.Timers;

namespace QC4Common.Logic
{

    public class ProcessStatusChecker:IDisposable
    {
        public static string STATUS_FILE_PATH = "process_status.txt";
        public static int INTERVAL = 1000; //1s
        public static Timer aTimer; //1s
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //public static bool check(Workbook workBook)
        //{
        //    string chkCrsfile = Path.Combine(Path.GetTempPath(), "QC4", STATUS_FILE_PATH);
        //    DateTime time = File.GetLastWriteTimeUtc(chkCrsfile);
        //    long fileUnixTime = ((DateTimeOffset)time).ToUnixTimeMilliseconds();
        //    long currentUnixTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        //    _log.Debug("In check : " + fileUnixTime + ":" + currentUnixTime + ":" + (currentUnixTime - fileUnixTime) + ":" + 1.5 * INTERVAL);
        //    if (currentUnixTime - fileUnixTime < 1.5 * INTERVAL)
        //    {
        //        MessageDialog.ShowMessageOnWorkBook("Some process is running in other instances.Please wait the process to be completed", QC4Common.Common.Constants.MessageType.Info, workBook);
        //        return false;
        //    }
        //    using (StreamWriter w = File.AppendText(chkCrsfile)) { }
        //    _log.Debug("File created" + chkCrsfile);
        //    aTimer = new System.Timers.Timer();
        //    aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
        //    aTimer.Interval = INTERVAL;
        //    aTimer.Enabled = true;
        //    return true;
        //}

        public static void stop()
        {
            if (aTimer != null)
            {
                aTimer.Stop();
                aTimer.Close();
                aTimer = null;
                GC.Collect();
            }
        }

        private static void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            string chkCrsfile = Path.Combine(Path.GetTempPath(), "QC4", STATUS_FILE_PATH);
            File.SetLastWriteTimeUtc(chkCrsfile, DateTime.UtcNow);
        }

        public void Dispose()
        {
            stop();
        }
    }
}