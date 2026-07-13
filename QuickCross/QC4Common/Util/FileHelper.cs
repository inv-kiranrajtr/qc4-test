using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using Ionic.Zip;
using Macromill.QCWeb.COMOperate;
using log4net;
using System.Reflection;
using System.Threading;

namespace QC4Common.Util
{
    public class FileHelper
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static bool isFileSaveSuccess = true;

        public bool SaveFile(ref string path, ref string tempPath, Excel.Workbook workbook, Excel.Application excelApp, bool saveAs = false, bool overWrite = true, int source = 1)
        {
            bool enableEvents = excelApp.EnableEvents;
            bool displayAlerts = excelApp.DisplayAlerts;

            excelApp.EnableEvents = false;
            excelApp.DisplayAlerts = false;
            string tPath = tempPath;
            bool saved = true;
            try
            {
                Util.AddFileProperties properties = new AddFileProperties();
                if (!overWrite)
                {
                    properties.AddSource(workbook, source);
                }

                properties.AddFileVersion(workbook);
                properties.AddUpdatedSource(workbook);

            }
            catch (Exception ex) { }
            if (saveAs && overWrite)
            {
                if (File.Exists(path))
                {
                    //string tp = Path.GetTempPath() + "\\" + Common.Constants.PathName.FileOpenTemp + Path.GetFileNameWithoutExtension(path) + ".qc4";
                    string tp = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + Common.Constants.PathName.FileOpenTemp + Path.GetFileNameWithoutExtension(path) + ".qc4";
                    try
                    {
                        File.Delete(tp + Common.Constants.TemplateFile.QC4_Template);
                    }
                    catch
                    {
                        Common.MessageDialog.ErrorOk(CommonResource.FILE_ALERDY_OPENED);
                        excelApp.EnableEvents = enableEvents;
                        excelApp.DisplayAlerts = displayAlerts;
                        return false;
                    }

                    try
                    {
                        File.Delete(path);
                    }
                    catch
                    {
                        Common.MessageDialog.ErrorOk(CommonResource.FILE_USED_ALERT);
                        excelApp.EnableEvents = enableEvents;
                        excelApp.DisplayAlerts = displayAlerts;
                        return false;
                    }
                }
            }

            if (!overWrite)
            {
                if (!path.EndsWith(".qc4"))
                {
                    path = path + ".qc4";
                }

                if (File.Exists(path))
                {
                    path = GenerateFileName(1, ".qc4", Path.GetFileNameWithoutExtension(path), Path.GetDirectoryName(path));
                }
            }

            if (saveAs)
            {
                //tPath = Path.GetTempPath() + "\\" + Common.Constants.PathName.FileOpenTemp + Path.GetFileNameWithoutExtension(path) + ".qc4";
                tPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + Common.Constants.PathName.FileOpenTemp + Path.GetFileNameWithoutExtension(path) + ".qc4";
                int i = 1;
                while (Directory.Exists(tPath))
                {
                    //tPath = Path.GetTempPath() + "\\" + Common.Constants.PathName.FileOpenTemp + Path.GetFileNameWithoutExtension(path)+"("+i+")" + ".qc4";
                    tPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + Common.Constants.PathName.FileOpenTemp + Path.GetFileNameWithoutExtension(path) + "(" + i + ")" + ".qc4";
                    i++;
                }
                tPath = tPath.Replace("\\\\", "\\");
            }

            List<Excel.Worksheet> dataSheet = ExcelUtil.GetDataSheets(workbook);
            List<Excel.Worksheet> dataAfterSheet = ExcelUtil.GetDataAfterProcessSheets(workbook);
            if (dataSheet.Count() > 0)
            {
                foreach (Excel.Worksheet worksheet in dataSheet)
                {

                    ExcelUtil.ClearContents(worksheet.Cells);
                }
                QC4Common.Util.ExcelUtil.ClearDatasheetContent(workbook);
            }

            if (dataAfterSheet.Count() > 0)
            {
                foreach (Excel.Worksheet worksheet in dataAfterSheet)
                {

                    ExcelUtil.ClearContents(worksheet.Cells);
                }
                QC4Common.Util.ExcelUtil.ClearDataafterSheetContent(workbook);
            }


            if (!tPath.Equals(tempPath))
            {
                string fName = tPath + "\\" + Common.Constants.TemplateFile.QC4_Template;
                Directory.CreateDirectory(tPath);
                if (!RetryWorkbookSaveAs(workbook, fName))
                {
                    //Dont do any of the file operations since the saveAs operation has already failed.Return save state as false if saving have failed more than the maximum limit
                    isFileSaveSuccess = false;
                    return false;
                }
                else
                {
                    isFileSaveSuccess = true;
                    try
                    {
                        File.Move(tempPath + "\\" + Common.Constants.TemplateFile.DB_FIlE, tPath + "\\" + Common.Constants.TemplateFile.DB_FIlE);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        try
                        {
                            File.Delete(tPath + "\\" + Common.Constants.TemplateFile.DB_FIlE);
                            File.Move(tempPath + "\\" + Common.Constants.TemplateFile.DB_FIlE, tPath + "\\" + Common.Constants.TemplateFile.DB_FIlE);
                        }
                        catch
                        {
                            File.Copy(tempPath + "\\" + Common.Constants.TemplateFile.DB_FIlE, tPath + "\\" + Common.Constants.TemplateFile.DB_FIlE);
                        }
                        saved = false;
                    }
                    if (File.Exists(tempPath + "\\" + Common.Constants.TemplateFile.QC4_Template))
                    {
                        try
                        {
                            File.Delete(tempPath + "\\" + Common.Constants.TemplateFile.QC4_Template);
                        }
                        catch { }
                    }
                    try
                    {
                        Directory.Delete(tempPath, true);
                    }
                    catch { }
                    tempPath = tPath;
                }
            }
            else
            {
                if (!RetryWorkbookSave(workbook))
                {
                    //Return save state as false if saving have failed more than the maximum limit
                    isFileSaveSuccess = false;
                    return false;
                }
                else
                {
                    isFileSaveSuccess = true;
                }
            }

            EmbedTempFiles(tempPath, path);

            DB.DBHelper.SetConnectionString(workbook, tempPath, Path.GetFileNameWithoutExtension(path) + Common.Constants.Qc4Extension, path);
            Common.CommonFlag.SetIsDataUpdated(workbook, false);
            Common.CommonFlag.SetIsDataAfterUpdated(workbook, false);
            Common.CommonFlag.SetIsMultivariateUpdated(workbook, false);
            if (!RetryEnableEvents(excelApp, enableEvents))
            {
                //No need to stop the execution even if EnableEvents have failed
            }
            if (!RetryDisplayAlerts(excelApp, displayAlerts))
            {
                //No need to stop the execution even if DisplayAlerts have failed
            }
            return saved;
        }

        private bool RetryWorkbookSave(Excel.Workbook workbook)
        {
            const int maxRetryCount = 5;
            int retryCount = 0;
            while (true)
            {
                try
                {
                    workbook.Save();
                    return true; // Saveが成功したら関数を終了
                }
                catch (System.Runtime.InteropServices.COMException ex) when (ex.HResult == unchecked((int)0x800AC472))
                {
                    if (++retryCount > maxRetryCount)
                    {
                        Common.MessageDialog.ErrorOk(CommonResource.SAVE_FILE_RETRY_ALERT);
                        _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                        return false;
                    }
                    Thread.Sleep(1000); // 1秒待つ
                }
            }
        }
        private bool RetryWorkbookSaveAs(Excel.Workbook workbook, string fName)
        {
            const int maxRetryCount = 5;
            int retryCount = 0;
            while (true)
            {
                try
                {
                    workbook.SaveAs(fName);
                    return true; // Saveが成功したら関数を終了
                }
                catch (System.Runtime.InteropServices.COMException ex) when (ex.HResult == unchecked((int)0x800AC472))
                {
                    if (++retryCount > maxRetryCount)
                    {
                        Common.MessageDialog.ErrorOk(CommonResource.SAVE_FILE_RETRY_ALERT);
                        _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                        return false;
                    }
                    Thread.Sleep(1000); // 1秒待つ
                }
            }
        }
        private bool RetryEnableEvents(Excel.Application excelApp, bool enableEvents)
        {
            const int maxRetryCount = 5;
            int retryCount = 0;
            while (true)
            {
                try
                {
                    excelApp.EnableEvents = enableEvents;
                    return true; // Saveが成功したら関数を終了
                }
                catch (System.Runtime.InteropServices.COMException ex) when (ex.HResult == unchecked((int)0x800AC472))
                {
                    if (++retryCount > maxRetryCount)
                    {
                        Common.MessageDialog.ErrorOk(CommonResource.SAVE_FILE_RESTART_ALERT);
                        _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                        return false;
                    }
                    Thread.Sleep(1000); // 1秒待つ
                }
            }
        }
        private bool RetryDisplayAlerts(Excel.Application excelApp, bool displayAlerts)
        {
            const int maxRetryCount = 5;
            int retryCount = 0;
            while (true)
            {
                try
                {
                    excelApp.DisplayAlerts = displayAlerts;
                    return true; // Saveが成功したら関数を終了
                }
                catch (System.Runtime.InteropServices.COMException ex) when (ex.HResult == unchecked((int)0x800AC472))
                {
                    if (++retryCount > maxRetryCount)
                    {
                        Common.MessageDialog.ErrorOk(CommonResource.SAVE_FILE_RESTART_ALERT);
                        _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                        return false;
                    }
                    Thread.Sleep(1000); // 1秒待つ
                }
            }
        }

        private static string GenerateFileName(int count, string ext, string fileName, string filePath)
        {
            string fullPath = filePath + "\\" + fileName + "(" + (count++) + ")" + ext;
            if (File.Exists(fullPath))
            {
                fullPath = GenerateFileName(count, ext, fileName, filePath);
            }
            return fullPath;
        }

        internal static void EmbedTempFiles(string tempPath, string targetPath)
        {
            try
            {
                using (ZipFile zip = new ZipFile())
                {
                    zip.Password = Common.Constants.Password;
                    zip.AddDirectory(tempPath);
                    zip.ParallelDeflateThreshold = -1;
                    zip.Save(targetPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                File.Delete(targetPath);
                DirectoryCopy(tempPath, tempPath + "\\zip", true);

                using (ZipFile zip = new ZipFile())
                {
                    zip.Password = Common.Constants.Password;
                    zip.AddDirectory(tempPath + "\\zip");
                    zip.ParallelDeflateThreshold = -1;
                    zip.Save(targetPath);
                }
                Directory.Delete(tempPath + "\\zip", true);
            }
        }

        internal static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = System.IO.Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, true);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = System.IO.Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        public bool IsFileLocked(FileInfo file)
        {
            try
            {
                if (File.Exists(file.FullName))
                {
                    using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        stream.Close();
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (IOException e)
            {


                return true;
            }
            return false;
        }

        public static object getTemplatePath(string basePath, string tEMPLATE_NAME, string sep)
        {
            return BuildPath(GetTemplateDirectoryPath(basePath, sep), tEMPLATE_NAME, sep);
        }
        public static string BuildPath(string Path1, string Path2, string sep)
        {
            if (Path1.IndexOf(sep) == -1)
            {
                if (Path1.IndexOf("/") > 0) { sep = "/"; }
            }
            while (Path1.Substring(Path1.Length - sep.Length) == sep)
            {
                Path1 = Path1.Substring(0, Path1.Length - sep.Length);
            }

            while (Path2.Substring(0, sep.Length) == sep)
            {
                Path2 = Path2.Substring(sep.Length);
            }
            return Path1 + sep + Path2;
        }

        public static string GetTemplateDirectoryPath(string basePath, string sep)
        {
            string TEMPLATE_DIRECTORY_NAME = "Templates";
            return BuildPath(basePath, TEMPLATE_DIRECTORY_NAME, sep);
        }
        public static string BASE_TEMPLATE_NAME = "Base.xltx";
        public static string BaseTemplatePath(string basePath, string sep)
        {
            return BuildPath(GetTemplateDirectoryPath(basePath, sep), BASE_TEMPLATE_NAME, sep);
        }
    }
}
