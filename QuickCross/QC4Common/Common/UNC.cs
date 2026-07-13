using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace QC4Common.Common
{
    public class UNC
    {
        public static bool IsNetworkPath(string path)
        {
            try
            {
                if (!path.StartsWith(@"/") && !path.StartsWith(@"\"))
                {
                    string rootPath = System.IO.Path.GetPathRoot(path); // get drive's letter
                    DriveInfo driveInfo = new DriveInfo(rootPath); // get info about the drive
                    return driveInfo.DriveType == DriveType.Network; // return true if a network drive
                }

                return true; // is a UNC path
            }
            catch (Exception ex)
            {
               // string errorMessage = $"{ErrorMessagesJP.ExitCode99} {ex.Message}";
                //MessageDialog.Error(errorMessage);
                return false;
            }
        }
    }
}
