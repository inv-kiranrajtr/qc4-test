using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class Log
    {
        private string message;
        private string event_occured;
        private static System.Threading.Mutex _mutex_LogApp = new System.Threading.Mutex();
        private static String path = Path.Combine(System.IO.Path.GetTempPath(), "QuickCross\\Logs\\AdminTool");

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
                //if ((level == Level.Info) && !Settings.Default.LogApp_Info) return;
                //if ((level == Level.Debug) && !Settings.Default.LogApp_Debug) return;
                //if ((level == Level.Warning) && !Settings.Default.LogApp_Warning) return;
                try
                {
                    _mutex_LogApp.WaitOne();
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string LogPath = path + "\\AdminToolLog_" + DateTime.Today.ToString("yyyyMMdd") + ".log";
                    string Log_msg = DateTime.Now.ToString("HH:mm:ss") + ",[" + level.ToString() + "],[" + EventOccured + "],[" + Message + "]\r\n";
                    File.AppendAllText(LogPath, Log_msg, Encoding.GetEncoding("Shift_JIS"));
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

