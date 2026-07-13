using log4net.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace Qc4Launcher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {   
            base.OnStartup(e);
			if (null != e.Args && e.Args.Count() > 0)
			{
                Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                Util.Definiotion.commandLineArg = e.Args[0];
            }
            string mode = "";
            string configPath = Util.CommonFunction.GetTemporaryDirectory() + @"language.config";
            if (File.Exists(configPath))
            {
                mode = File.ReadAllText(configPath);
            }

            QC4Common.Common.Constants.GlobalMode = "ja-JP," + Util.Constants.QCFont.MS_Gothic;
            if (string.IsNullOrEmpty(mode))
            {
                string cult = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
                if (cult != null && cult.Length > 2 && cult.Substring(0, 3) != "ja-")
                {
                    QC4Common.Common.Constants.GlobalMode = "en-US," + Util.Constants.QCFont.Segoe_UI;
                }
            }
            else
            {
                QC4Common.Common.Constants.GlobalMode = mode;
            }
            LocalResource.Culture = new CultureInfo(QC4Common.Common.Constants.GlobalMode.Split(',')[0]);/*System.Threading.Thread.CurrentThread.CurrentCulture;*/
            ExcelAddIn.AddinResource.Culture = new System.Globalization.CultureInfo(QC4Common.Common.Constants.GlobalMode.Split(',')[0]);
            QC4Common.CommonResource.Culture = new System.Globalization.CultureInfo(QC4Common.Common.Constants.GlobalMode.Split(',')[0]);
            FilterSettingsView.LocalResource.Culture = new System.Globalization.CultureInfo(QC4Common.Common.Constants.GlobalMode.Split(',')[0]);

            try
			{
				XmlConfigurator.Configure();
				var date = DateTime.Now.AddDays(-10);
				var task = new QC4Common.Util.LogFileCleanupTask();
				task.CleanUp(date);
			}
			catch { }
            
            CleanOldLogs();
		}
        /// <summary>
        /// Function to delete log files older than 11 days
        /// </summary>
        private void CleanOldLogs()
        {
            try
            {
                string tempDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Macromill\QuickCross\");
                string[] files = Directory.GetFiles(tempDirectory + "QC4/Logs/Error");
                foreach (string file in files)
                {
                    FileInfo fi = new FileInfo(file);
                    if (fi.LastWriteTime < DateTime.Now.AddDays(-10))
                        fi.Delete();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot delete :" + e.Message);
            }
        }
    }
}
