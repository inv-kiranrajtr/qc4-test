using Ionic.Zip;
using QC4Common.Util;
using Qc4Launcher.DB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using ProgressBarForm = Qc4Launcher.Forms.ProgressBar;

namespace Qc4Launcher.Util
{
	class QcFileHelper
	{
		//private const string TemplateName = "Qc4_Template.xlsx";
		private const string TemplateName = "Macromill.Quick-CROSS";
		private const string DbName = "qc4.db";
		private string BasePath = System.AppContext.BaseDirectory;
		private static Logger.Log LogObj = new Logger.Log();

        internal static string CreateTempFile(string path, string tempFolder = Constants.PathName.FileOpenTemp)
        {
            string fileName = Path.GetFileNameWithoutExtension(path);
            //string tempPath = Path.GetTempPath() + tempFolder + fileName + ".qc4";
            string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + tempFolder + fileName + ".qc4";
            if (Directory.Exists(tempPath))
            {
                Directory.Delete(tempPath, true);
            }
            Directory.CreateDirectory(tempPath);
            string templatePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\QC4\\Templates\\";
            //File.Copy(templatePath + TemplateName, tempPath+"\\" + TemplateName);
            DBHelper.CreateDatabase(tempPath + "\\" + DbName);
            return tempPath;
        }

        internal static string GetTempPath()
        {
            //return Path.GetTempPath() + Constants.PathName.FileOpenTemp;
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + Constants.PathName.FileOpenTemp;
        }

        internal static string GetTempPathDImport()
        {
            //return Path.GetTempPath() + Constants.PathName.TempDataImportPath;
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + Constants.PathName.TempDataImportPath;
        }


        internal static bool ArchiveFiles(string sourcePath, string destPath)
        {
            try
            {
                using (ZipFile zip = new ZipFile())
                {
                    zip.Password = Constants.Password;
                    zip.AddDirectory(sourcePath);
					zip.ParallelDeflateThreshold = -1;
					zip.Save(destPath);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        internal static string EmbedTempFiles(string tempPath)
        {
            string filePath = tempPath + ".qc4";
            try
            {
                using (ZipFile zip = new ZipFile())
                {
                    zip.Password = Constants.Password;
                    zip.AddDirectory(tempPath);
					zip.ParallelDeflateThreshold = -1;
					zip.Save(filePath);
                }
            }
            catch (Exception ex)
            {
                File.Delete(filePath);
                FileUtil.DirectoryCopy(tempPath, tempPath + "\\zip", true);

                using (ZipFile zip = new ZipFile())
                {
                    zip.Password = Constants.Password;
                    zip.AddDirectory(tempPath + "\\zip");
					zip.ParallelDeflateThreshold = -1;
					zip.Save(filePath);
                }
                Directory.Delete(tempPath + "\\zip", true);
            }
            return filePath;
        }

        internal static string ExtractFile(string filePath, string temp = Constants.PathName.FileOpenTemp)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            //string tempPath = Path.GetTempPath() + temp + fileName + ".qc4";
            string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + temp + fileName + ".qc4";

            try
            {
                using (ZipFile archive = new ZipFile(filePath))
                {
                    archive.Password = Constants.Password;
                    archive.Encryption = EncryptionAlgorithm.PkzipWeak; // the default: you might need to select the proper value here
                    archive.StatusMessageTextWriter = Console.Out;
                    archive.ExtractAll(tempPath, ExtractExistingFileAction.OverwriteSilently);
                }
                //Qc4Launcher.MainWindow.fileloack = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None);
                try
                {
                    if (!Qc4Launcher.MainWindow.File_read_only)
                    {
                        Qc4Launcher.MainWindow.fileloack = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
                    }
                }
                catch { }

                if (!File.Exists(tempPath + @"\" + Constants.TemplateFile.QC4_Template))
                {
                    return null;
                }
                if (!File.Exists(tempPath + @"\" + Constants.TemplateFile.DB_FIlE))
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                return null;
            }

            return tempPath;
        }

        internal static void CloseQCFile(string tempPath, bool alert = true)
        {
            try
            {
                Directory.Delete(tempPath, true);
                if (Qc4Launcher.MainWindow.fileloack != null)
                {
                    Qc4Launcher.MainWindow.fileloack.Dispose();
                    Qc4Launcher.MainWindow.fileloack.Close();
                    Qc4Launcher.MainWindow.fileloack = null;
                }
            }
            catch (Exception ex)
            {
                if (alert)
                {
                    MessageDialog.ErrorOk(LocalResource.FILE_USED_ANOTHER_PROCESS);
                }
                LogObj.WriteLog("Close file", "Deletion failed " + ex.Message);
            }
        }

        internal static void SaveFileWithPB(MainWindow ParentWindow, ref string targetPath, ref string sourcePath, Excel.Workbook workbook, bool reWrite = true, bool saveAs = false)
        {
            string loackedFile = String.Empty;
            if (Qc4Launcher.MainWindow.fileloack != null)
            {
                loackedFile = Qc4Launcher.MainWindow.fileloack.Name;
                Qc4Launcher.MainWindow.fileloack.Dispose();
                Qc4Launcher.MainWindow.fileloack.Close();
                Qc4Launcher.MainWindow.fileloack = null;
            }
            string tPath = targetPath;
			string sPath = sourcePath;
			ProgressBarForm progress = new ProgressBarForm(true, LocalResource.STATUS_SAVING);
            progress.Owner = ParentWindow;
			new System.Threading.Thread(() => Save(ref tPath, ref sPath, workbook, workbook.Application, reWrite, saveAs, progress)).Start();
			progress.ShowDialog();
            if (Definiotion.SaveAs)
            {
                targetPath = tPath;
                sourcePath = sPath;
                if (Qc4Launcher.MainWindow.fileloack == null)
                {
                    Qc4Launcher.MainWindow.fileloack = new FileStream(targetPath, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
                    //Qc4Launcher.MainWindow.fileloack = new FileStream(targetPath, FileMode.Open, FileAccess.Read, FileShare.None);

                }
            }
            else
            {
                targetPath = loackedFile;
                sourcePath = sPath;
                if (Qc4Launcher.MainWindow.fileloack == null)
                {
                    Qc4Launcher.MainWindow.fileloack = new FileStream(targetPath, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
                    //Qc4Launcher.MainWindow.fileloack = new FileStream(loackedFile, FileMode.Open, FileAccess.Read, FileShare.None);

                }
            }
        }

		private static void Save(ref string destPath, ref string TempPath, Excel.Workbook excelWorkbook, Excel.Application excelApp, bool reWrite, bool saveAs, ProgressBarForm pb )
		{
			FileHelper fileHelper = new FileHelper();

			Definiotion.SaveAs = fileHelper.SaveFile(ref destPath, ref TempPath, excelWorkbook, excelApp, saveAs, reWrite);
			if (pb != null)
			{
				pb.Dispatcher.Invoke(() =>
				{
					pb.Close();
				});
			}
		}

        internal static void SaveFile(string targetPath, string sourcePath, bool reWrite = true, ProgressBarForm pb = null, Excel.Workbook workbook = null, bool alert = false)
        {
            bool success = false;
            try
            {
                if (null != workbook)
                {
                    workbook.Application.EnableEvents = false;
                    workbook.Save();
                    workbook.Application.EnableEvents = true;
                }
                if (!reWrite)
                {
                    if (File.Exists(targetPath))
                    {
                        targetPath = GenerateFileName(1, ".qc4", Path.GetFileNameWithoutExtension(targetPath), Path.GetDirectoryName(targetPath));
                    }
                }

                string zip = EmbedTempFiles(sourcePath);
                File.Copy(zip, targetPath, true);
                File.Delete(zip);
                success = true;
            }
            catch (Exception ex)
            {
                if (alert) MessageDialog.Info("File not saved.\n File is used by some other process.");
                LogObj.WriteLog("SaveFile", "Failed : " + ex.Message);
            }
            if (pb != null)
            {
                pb.Dispatcher.Invoke(() =>
                {
                    pb.Close();
                });
            }
            if (alert && success) MessageDialog.Info("File saved");
        }


        public static string GenerateFileName(int count, string ext, string fileName, string filePath)
        {
            string fullPath = filePath + "\\" + fileName + "(" + (count++) + ")" + ext;
            if (File.Exists(fullPath)||Directory.Exists(fullPath))
            {
                fullPath = GenerateFileName(count, ext, fileName, filePath);
            }
            return fullPath;
        }

    }
}
