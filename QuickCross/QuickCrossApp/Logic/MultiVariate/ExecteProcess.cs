using log4net;
using Qc4Launcher.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Qc4Launcher.Logic.MultiVariate
{
    class ExecteProcess
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static int Execute(string rscriptCmd, out string errmsg)
        {

            try
            {
                Process p = new Process();
                p.StartInfo.FileName = System.Environment.GetEnvironmentVariable("ComSpec"); // CMDを使用
                                                                                             //Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"QC4\Templates\ComputeExpression\ComputeExpression.exe");
                p.StartInfo.Arguments = rscriptCmd; // Rスクリプト実行コマンド本体								

                // 出力を読み取れるようにする								
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardInput = false;
                p.StartInfo.RedirectStandardError = true;
                //ウィンドウを表示しないようにする								
                p.StartInfo.CreateNoWindow = true;
                // プロセス実行		
                // p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                p.Start();
                // 出力を読み取る								
                var results = p.StandardOutput.ReadToEnd(); // 通常出力								
                var err = p.StandardError.ReadToEnd();      // エラー出力								
                p.WaitForExit();
                // 終了コードを取得								
                var rltcode = p.ExitCode;
                // プロセスクローズ								
                p.Close();

                if (rltcode != 0)
                {
                    errmsg = err;
                }
                else
                {
                    errmsg = string.Empty;
                }

                return rltcode;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                _log.Error(ex.StackTrace);
                _log.LogError(ex.Message);
                errmsg = ex.Message;
                return -1;
            }
        }

    }
}
