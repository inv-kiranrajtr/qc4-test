using Macromill.QCWeb.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qc4Launcher.Logic.DPCheckCross
{
    public class CheckCrossHelper
    {
        public static void RemoveOutPutFiles()
        {
            string filePath = @"D:\checkcross\output";
            GlobalMethodClass.GuaranteeDirectoryExist(filePath);
            System.IO.DirectoryInfo di = new DirectoryInfo(filePath);

            if (di.GetFiles() != null)
            {
                foreach (FileInfo file in di.GetFiles())
                {
                    try
                    {
                        file.Delete();

                    }
                    catch (Exception ex)
                    {
                       // _log.Error(ex.Message);
                    }
                }
            }
        }
    }
}
