using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;


namespace Logger
{
    public class Log
    {
        private string message;
        private string event_occured;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        private static System.Threading.Mutex _mutex_LogApp = new System.Threading.Mutex();
        
        private static String path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Macromill", "QuickCross" , "QC4\\Logs");

        public enum Level
        {
            Info,
            Warning,
            Error,
            Debug,
            Critical
        };


        public void WriteLog(string EventOccured, string Message, Level level = Level.Info)
        {
            try
            {

               

                try
                {
                    _mutex_LogApp.WaitOne();
                    if(!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string LogPath = path + "\\ApplicationLog_" + DateTime.Today.ToString("yyyyMMdd") + ".log";
                    string Log_msg = DateTime.Now.ToString("HH:mm:ss") + ",[" + level.ToString() + "],[" + EventOccured + "],[" + Message + "]\r\n";
                    _log.Info(Log_msg);
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    _mutex_LogApp.ReleaseMutex();
                };
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

    }
}
