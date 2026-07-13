using Microsoft.VisualBasic;
using Qc4Launcher.Classes;
using Qc4Launcher.DB;
using Qc4Launcher.Forms.QCM;
using Qc4Launcher.Util;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using ProgressBar = Qc4Launcher.Forms.ProgressBar;

namespace Qc4Launcher.Logic.DataImport
{
    class BatchFileController
    {
        private string fileName;
        private Encoding encode;
        Model.ColumnImportSettings importSettings;
        private ProgressBar progress;
        private string WarngingMessage = LocalResource.IM_QLAYOUT_READ_WARNING_MESSAGE1;
        private bool IsWarningMessage = false;
        double prec = 0;
        string DBPath;
        Qc4Launcher.DataImport DataImport;

        public BatchFileController(string fileName, Encoding encode, ref Model.ColumnImportSettings importSettings, string dBPath, Qc4Launcher.DataImport dataImport)
        {
            this.fileName = fileName;
            this.encode = encode;
            this.importSettings = importSettings;
            this.DBPath = dBPath;
            this.DataImport = dataImport;
        }

        public bool ProcessBatchFile()
        {
            String msg = "";
            progress = new ProgressBar(LocalResource.LBL_DATA_IMPORT_TITILE);
            progress.Owner = DataImport;
            new Thread(() => StartBatchFile_Process(out msg)).Start();
            progress.ShowDialog();

            if (msg != "")
            {
                progress.Close();
                MessageDialog.ErrorOk(msg);
                return false;
            }
            else if (IsWarningMessage)
            {
                MessageDialog.Warning(WarngingMessage);
            }

            return true;
        }

        private void StartBatchFile_Process(out string message)
        {
            message = "";

            OnWorkerMethodComplete(prec++, LocalResource.QCM_PB_INPUTFILE_VALIDATE);
            if (!QCMValidation.ValidateEncoding(fileName, encode, out message, true))
            {
                OnWorkerMethodComplete(100, LocalResource.DI_MSG_001);
                return;
            }

            prec++;
            OnWorkerMethodComplete(prec, LocalResource.QCM_PB_PARSE_QLAYOUT);
            List<string[]> qlayoutData = TextParser.ReadFile(fileName, encode, ",");

            prec += 18;
            OnWorkerMethodComplete(prec, LocalResource.QCM_PB_PROCESS_QLAYOUT);
            if (!QlayoutDataProcessor(qlayoutData, out message))
            {
                OnWorkerMethodComplete(100, LocalResource.DI_MSG_001);
                return;
            }

            OnWorkerMethodComplete(prec, LocalResource.ST_VALIDATION_CHECKS);
            ValidateInformation();

            OnWorkerMethodComplete(100, LocalResource.STD_DB_CREATE_DATASHEET_FINISH);
        }

        private void OnWorkerMethodComplete(double value, string status)
        {
            progress.Dispatcher.Invoke(DispatcherPriority.Normal,
            new Action(
            delegate ()
            {
                progress.UpdateProgressBar(value, status);
            }
            ));
        }

        #region parsing
        private bool QlayoutDataProcessor(List<string[]> parsedCsv, out string message)
        {
            message = "";
            try
            {
                if (parsedCsv.Count < 3 || (parsedCsv.Count > 2 && parsedCsv[2][0] == "FILEEND"))
                {
                    message = LocalResource.QCM_ZERO_QLAYOUT_DATA;
                    return false;
                }

                bool isMAFlagFormat = importSettings.MAformat == Enums.MAFormat.FlagFormat;
                int infoCount = importSettings.ImportInformations.Count;
                int dataCount = parsedCsv.Count;
                int wMsgCount = 0;
                int informationIndex = 0;
                double percInc = 30.0 / (dataCount - 2);
                bool isChoice = false;
                for (int i = 2; i < dataCount && informationIndex < infoCount; i++, informationIndex++)
                {
                    string[] information = parsedCsv[i];
                    if (information[0] == "FILEEND")
                        break;

                    string[] invalidItems = new string[] { LocalResource.IM_QLAYOUT_READ_WARNING_VARIABLE, LocalResource.IM_QLAYOUT_READ_WARNING_ANS_TYPE, LocalResource.IM_QLAYOUT_READ_WARNING_NO_CHOICES };

                    for (int j = 0; j < information.Length; j++)
                    {
                        switch (j)
                        {
                            case 3:
                                string variableName = information[3].Trim();
                                invalidItems[0] = "";
                                if (variableName.Length > 24)
                                {
                                    variableName = variableName.Substring(0, 25);
                                    invalidItems[0] = LocalResource.IM_QLAYOUT_READ_WARNING_VARIABLE;
                                }
                                importSettings.ImportInformations[informationIndex].VariableName = variableName;
                                break;
                            case 4:
                                string ansType = information[4].Trim().ToUpper();
                                invalidItems[1] = "";
                                if (ansType != "SA" && ansType != "MA" && ansType != "FA" && ansType != "N")
                                {
                                    invalidItems[1] = LocalResource.IM_QLAYOUT_READ_WARNING_ANS_TYPE;
                                    ansType = "SA";
                                }
                                importSettings.ImportInformations[informationIndex].AnswerType = ansType;
                                break;
                            case 5:
                                invalidItems[2] = "";
                                if (importSettings.ImportInformations[informationIndex].AnswerType == "SA" || importSettings.ImportInformations[informationIndex].AnswerType == "MA")
                                {
                                    int categoryCount = 0;
                                    bool isInvalid = false;
                                    int.TryParse(information[5], out categoryCount);
                                    if (information[5].Trim() == "0" || categoryCount < 0)
                                    {
                                        categoryCount = 1;
                                        isInvalid = true;
                                    }
                                    else if (categoryCount == 0 || categoryCount > 1000)
                                    {
                                        categoryCount = 1000;
                                        isInvalid = true;
                                    }
                                    if (isMAFlagFormat && importSettings.ImportInformations[informationIndex].AnswerType == "MA" && importSettings.ImportInformations.Count < (informationIndex + categoryCount))
                                    {
                                        categoryCount = 0;
                                        isInvalid = true;
                                    }
                                    if (isInvalid)
                                    {
                                        invalidItems[2] = LocalResource.IM_QLAYOUT_READ_WARNING_NO_CHOICES;
                                    }
                                    importSettings.ImportInformations[informationIndex].NoOfChoices = categoryCount;
                                }
                                break;
                            case 9:
                                importSettings.ImportInformations[informationIndex].QuestionTitle = ConvertTitle(information[9]);
                                break;
                            case 10:
                                importSettings.ImportInformations[informationIndex].QuestionSentence = ConvertQSText(information[10]);
                                break;
                            case 11:
                                if (importSettings.ImportInformations[informationIndex].AnswerType == "SA" || importSettings.ImportInformations[informationIndex].AnswerType == "MA")
                                {
                                    isChoice = true;
                                    List<Model.ChoiceWording> choices = new List<Model.ChoiceWording>();
                                    int slNo = 0;
                                    int catCount = importSettings.ImportInformations[informationIndex].NoOfChoices;
                                    for (int k = j; slNo < catCount; k++)
                                    {
                                        string wText = k < information.Length ? ConvertChoiceWording(information[k]) : "";
                                        choices.Add(new Model.ChoiceWording { SlNo = slNo+1, WordingText = wText });
                                        slNo++;
                                    }
                                    importSettings.ImportInformations[informationIndex].ChoiceWordings = choices;
                                }
                                break;
                        }
                    }
                    if(!isChoice && (importSettings.ImportInformations[informationIndex].AnswerType == "SA" || importSettings.ImportInformations[informationIndex].AnswerType == "MA"))
                    {
                        int catCount = importSettings.ImportInformations[informationIndex].NoOfChoices;
                        List<Model.ChoiceWording> choices = new List<Model.ChoiceWording>();
                        for (int k = 0; k < catCount; k++)
                        {
                            choices.Add(new Model.ChoiceWording { SlNo = k+1, WordingText = "" });
                        }
                        importSettings.ImportInformations[informationIndex].ChoiceWordings = choices;
                    }
                    if (importSettings.ImportInformations[informationIndex].AnswerType == "MA" && isMAFlagFormat && importSettings.ImportInformations[informationIndex].NoOfChoices != 0)
                    {
                        informationIndex += importSettings.ImportInformations[informationIndex].NoOfChoices - 1;
                    }
                    if (wMsgCount < 6 && invalidItems.Any(x => x != ""))
                    {
                        wMsgCount++;
                        string wMsg = "";
                        IsWarningMessage = true;
                        for (int v = 0; v < 3 && wMsgCount < 6; v++)
                        {
                            if (invalidItems[v] != "")
                            {
                                if (wMsg == "")
                                    wMsg += string.Format(LocalResource.IM_QLAYOUT_READ_WARNING_MESSAGE2, i+1, invalidItems[v]);
                                else
                                    wMsg += ", " + invalidItems[v];
                            }
                        }
                        if (wMsgCount > 5)
                            WarngingMessage += "\n...";
                        else
                            WarngingMessage += "\n" + wMsg;
                    }

                    prec += percInc;
                    OnWorkerMethodComplete(prec, LocalResource.QCM_PB_PROCESS_QLAYOUT);
                }

                prec = 50;
                OnWorkerMethodComplete(prec, LocalResource.QCM_PB_PROCESS_QLAYOUT);
                return true;
            }catch
            {
                return false;
            }
        }
        #endregion

        #region Convert data
        private string ConvertTitle(string title)
        {
            string convertedItem = "";
            convertedItem = title.Replace("<COMMA>", ",").Replace("<TAB>"," ");
            return convertedItem;
        }
        private string ConvertQSText(string qsText)
        {
            string convertedItem = "";
            convertedItem = qsText.Replace("<COMMA>", ",").Replace("<TAB>", " ").Replace("<LF>", "\n").Replace("←→", "\n");
            return convertedItem;
        }
        private string ConvertChoiceWording(string choiceWording)
        {
            string convertedItem = "";
            convertedItem = choiceWording.Replace("<COMMA>", ",").Replace("<TAB>", "\t").Replace("<LF>", "\n").Replace("←→", "\n");
            return convertedItem;
        }
        #endregion

        #region Validation
        private void ValidateInformation()
        {
            SQLiteConnection con = DBHelper.GetConnection(DBHelper.GetConnectionString(DBPath));
            con.Open();
            double precInc = 40.0 / importSettings.ImportInformations.Count;
            DataImport.IsNotValidateFromThread = false;
            for (int i = 0; i < importSettings.ImportInformations.Count; i++)
            {
                OnWorkerMethodComplete(prec, LocalResource.ST_VALIDATION_CHECKS);
                prec += precInc;

                importSettings.SelectedIndex = i;
                importSettings.ImportInformations[i].IsDataValidated = DataImport.IsValidInformation(importSettings, con, true);
                importSettings.SelectedIndex = 0;
                if (importSettings.ImportInformations[i].AnswerType == "MA" && importSettings.MAformat == Enums.MAFormat.FlagFormat && importSettings.ImportInformations[i].NoOfChoices != 0)
                {
                    int c = 1;
                    for (int j = i + 1; j < importSettings.ImportInformations.Count && c < importSettings.ImportInformations[i].NoOfChoices; j++, c++)
                    {
                        OnWorkerMethodComplete(prec, LocalResource.ST_VALIDATION_CHECKS);
                        prec += precInc;

                        importSettings.ImportInformations[j].IsColumnSetForFlagFormat = true;
                        importSettings.ImportInformations[j].IsDataValidated = true;
                        importSettings.SelectedIndex = 0;
                    }
                    i += importSettings.ImportInformations[i].NoOfChoices - 1;
                }
            }
            con.Close(); con.Dispose();

            prec = 90;
            OnWorkerMethodComplete(prec, LocalResource.ST_VALIDATION_CHECKS);
            DataImport.IsNotValidateFromThread = true;
        }
        #endregion
    }
}
