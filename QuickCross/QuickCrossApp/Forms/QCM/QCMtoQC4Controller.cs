using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qc4Launcher.Util;
using System.Threading;
using Macromill.QCWeb.COMOperate;
using System.IO;
using Constant = QC4Common.Common.Constants;
using log4net;

namespace Qc4Launcher.Forms.QCM
{
    class QCMtoQC4Controller
    {
        private string qlayoutFilePath;
        private string qrawdataFilePath;
        private string qc4FilePath;
        private string outputFilePath;
        private Encoding encode;
        private System.Windows.Window parentWindow;
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private ProgressUpdate progressUpdate;

        public QCMtoQC4Controller(String qlayoutFilePath, String qrawdataFilePath, String qc4FilePath, String outputPath, Encoding encode, System.Windows.Window parentWindow)
        {
            this.qlayoutFilePath = qlayoutFilePath;
            this.qrawdataFilePath = qrawdataFilePath;
            this.qc4FilePath = qc4FilePath;
            this.outputFilePath = outputPath;
            this.encode = encode;
            this.parentWindow = parentWindow;
        }
        public bool QcmToQc4_StartProcess()
        {
            String msg = "";
            bool validate = true;
            progressUpdate = new ProgressUpdate(parentWindow);
            new Thread(() => QcmToQc4_Process(out msg, out validate)).Start();
            progressUpdate.progress.ShowDialog();

            if (!validate)
            {
                if (msg.Contains(QCMHelper.ReturnErrorLabel()))
                {
                    _log.LogError(msg);
                    MessageDialog.ErrorOk(LocalResource.EX_EXPORT_FAILED);
                }
                else if (!msg.Equals(""))
                    MessageDialog.ErrorOk(msg);

                return false;
            }
            return true;
        }

        public bool QcmToQc4_Process(out string message, out bool validate)
        {
            validate = false;
            message = "";

            if (!QCMValidation.ValidateSelectedFiles(qlayoutFilePath, qrawdataFilePath, outputFilePath, out message))
            {
                progressUpdate.UpdateProgress(100, LocalResource.QC3PARSE_PB_FAILED);
                return false;
            }

            progressUpdate.UpdateProgress(progressUpdate.prec++, LocalResource.QCM_PB_INPUTFILE_VALIDATE);
            if (!QCMValidation.ValidateEncoding(qlayoutFilePath, encode, out message))
            {
                progressUpdate.UpdateProgress(100, LocalResource.QC3PARSE_PB_FAILED);
                return false;
            }

            progressUpdate.UpdateProgress(progressUpdate.prec += 3, LocalResource.QCM_PB_INPUTFILE_VALIDATE);
            string TempPath = CreateTempFile(outputFilePath);
            if (TempPath == "")
            {
                progressUpdate.UpdateProgress(100, LocalResource.QC3PARSE_PB_FAILED);
                return false;
            }
            TempPath = QcFileHelper.CreateTempFile(TempPath, Constants.PathName.FileOpenTemp);

            progressUpdate.UpdateProgress(progressUpdate.prec += 0, LocalResource.QCM_PB_INPUTFILE_VALIDATE);
            if (!QCMValidation.ValidateFileName(qlayoutFilePath, qrawdataFilePath, out message))
            {
                progressUpdate.UpdateProgress(100, LocalResource.QC3PARSE_PB_FAILED);
                return false;
            }

            progressUpdate.UpdateProgress(progressUpdate.prec++, LocalResource.QCM_PB_PARSE_QLAYOUT);
            if (!QCMInputFileProcessor.QlayoutParser(qlayoutFilePath, encode, ",", out List<string[]> qlayoutData, out message))
            {
                progressUpdate.UpdateProgress(100, LocalResource.QC3PARSE_PB_FAILED);
                return false;
            }

            progressUpdate.UpdateProgress(progressUpdate.prec += 4, LocalResource.QCM_PB_PARSE_QRAWDATA);
            if (!QCMInputFileProcessor.QrawdataParser(qrawdataFilePath, encode, "\t", out List<string[]> qrawData, out message))
            {
                progressUpdate.UpdateProgress(100, LocalResource.QC3PARSE_PB_FAILED);
                return false;
            }

            progressUpdate.UpdateProgress(progressUpdate.prec += 7, LocalResource.QCM_PB_PROCESS_QLAYOUT);
            if (!QCMInputFileProcessor.QlayoutDataProcessor(qlayoutData, out List<Qc3Parse.QDataDetail> qDataDetails, out Object[,] qsAry, out message))
            {
                progressUpdate.UpdateProgress(100, LocalResource.QC3PARSE_PB_FAILED);
                return false;
            }

            progressUpdate.UpdateProgress(progressUpdate.prec += 3, LocalResource.QCM_PB_INPUTFILE_VALIDATE);
            if (!QCMValidation.ValidateQlayoutdataAndQrawdata(qDataDetails, qrawData[0], out message))
            {
                progressUpdate.UpdateProgress(100, LocalResource.QC3PARSE_PB_FAILED);
                return false;
            }

            progressUpdate.UpdateProgress(progressUpdate.prec += 2, LocalResource.QCM_PB_PROCESS_QRAWDATA);
            if (!QCMInputFileProcessor.QrawdataProcessor(qrawData, qDataDetails,TempPath,progressUpdate, (double)53 / qrawData.Count(),out message))
            {
                progressUpdate.UpdateProgress(100, LocalResource.QC3PARSE_PB_FAILED);
                return false;
            }

            progressUpdate.UpdateProgress(progressUpdate.prec += 0, LocalResource.QC3PARSE_PB_LOADING_TEMPLATE);
            ExcelOperate excelOperate = new ExcelOperate();
            Qc4Creation qc4Create = new Qc4Creation(excelOperate);

            progressUpdate.UpdateProgress(progressUpdate.prec += 2, LocalResource.QC3PARSE_PB_PROCESS_QS_UPDATION);
            QCMDatabaseOperations.Insert_QuestionToDb(qDataDetails, TempPath);
            if (!qc4Create.Update_QuestionSettingSheet(qsAry, qDataDetails, QCMHelper.QlayoutVariables.surveyName))
            {
                progressUpdate.UpdateProgress(100, LocalResource.QC3PARSE_PB_FAILED);
                return false;
            }

            progressUpdate.UpdateProgress(progressUpdate.prec += 6, LocalResource.QC3PARSE_PB_INIT_SETTING);
            if (!qc4Create.Update_SettingSheet(TempPath, outputFilePath, outputFilePath))
            {
                progressUpdate.UpdateProgress(100, LocalResource.QC3PARSE_PB_FAILED);
                return false;
            }

            //OnWorkerComplete(percentageCompletion += 2, "Detail Setting sheet completed");
            //qc4Create.Update_DetailSettingSheet(); 

            progressUpdate.UpdateProgress(progressUpdate.prec += 4, LocalResource.QC3PARSE_PB_INIT_SETTING);
            if (!qc4Create.Update_ListSheet(qDataDetails))
            {
                progressUpdate.UpdateProgress(100, LocalResource.QC3PARSE_PB_FAILED);
                return false;
            }

            progressUpdate.UpdateProgress(progressUpdate.prec += 4, LocalResource.QC3PARSE_PB_INIT_GT);
            qc4Create.Update_GTSheet();

            progressUpdate.UpdateProgress(progressUpdate.prec += 4, LocalResource.QC3PARSE_PB_INIT_CR);
            qc4Create.Update_CrossSheet(qDataDetails);

            progressUpdate.UpdateProgress(progressUpdate.prec += 2, LocalResource.QC3PARSE_PB_ALMOST_FINISH);
            QCMDatabaseOperations.Insert_WeightBack(qc4Create.GetTargetWorkbook());
            qc4Create.Save_Qc4File(TempPath, outputFilePath);
            //try
            //{
            //    Directory.Delete(TempPath, true);
            //}
            //catch { }
            progressUpdate.UpdateProgress(100, LocalResource.QC3PARSE_PB_FINISH);

            validate = true;
            return true;
        }

        public string CreateTempFile(string SelectedFile)
        {
            string targetFile = QcFileHelper.GetTempPath() + Path.GetFileNameWithoutExtension(SelectedFile) + ".qc4";
            if (targetFile.Length > 260)
            {
                MessageDialog.Warning(LocalResource.MSG_PATH_TOO_LONG);
                return "";
            }
            if (Directory.Exists(targetFile))
            {
                try
                {
                    string excelFile = targetFile + "\\" + Constant.TemplateFile.QC4_Template;
                    excelFile = excelFile.Replace("\\\\", "\\");
                    File.Delete(excelFile);
                    Directory.Delete(targetFile, true);

                }
                catch (Exception ex)
                {
                    if (Path.GetExtension(SelectedFile).ToLower() == Constant.Qc4Extension)
                    {
                        MessageDialog.ErrorOk(LocalResource.MW_ALERT_ALREADY_OPENED);
                        return "";
                    }
                    else
                    {
                        targetFile = QcFileHelper.GenerateFileName(1, ".qc4", Path.GetFileNameWithoutExtension(SelectedFile), Path.GetDirectoryName(QcFileHelper.GetTempPath()));
                    }
                }
            }
            return targetFile;
        }
    }
}

