using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Macromill.QCWeb.Question;
using ExcelAddIn.Sheets;
using Excel = Microsoft.Office.Interop.Excel;
using Qc4Launcher.Util;
using System.Collections;
using Macromill.QCWeb.Exceptions;
using Macromill.QCWeb.ReportRequest;
using Macromill.QCWeb.Tabulation;
using System.Data.SQLite;
using System.IO;
using QC4Common.Model;
using log4net;
using System.Reflection;
using System.Data;
using System.Globalization;
using Qc4Launcher.DB;

namespace Qc4Launcher.Classes
{
    class DataExportQcFile
    {
        private ProgressBar progressBar;
        List<FilterSettingsCr> filterSettings;
        static string tableName;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        System.Windows.Window window;
        MainWindow mainWindow;
        public string WMessage = "";
        string EXMessage = "";
        List<int> ExcelRowStart = null;
        int ExcelColumnStart = 0;
        public DataExportQcFile(ProgressBar pb, List<FilterSettingsCr> filterSettings, string tName, System.Windows.Window window, MainWindow mainWindow)
        {
            this.filterSettings = filterSettings;
            progressBar = pb;
            tableName = tName;
            this.window = window;
            this.mainWindow = mainWindow;
        }

        public bool QcMain(List<QuestionSettings> usedQuestions, Questions questions, Excel.Workbook sourceWorkbook
            , string targetPath, ProgressBar pb, bool isQc4 = false, QuestionSettings divisionQuestion = null)
        {
            string ext = ".qc4";
            progressBar = pb;
            int choiceColBegin = Constants.QS.ColChoiceBegin;
            string templateName = isQc4 ? Constants.TemplateFile.QC4_Template_Do : Constants.TemplateFile.QC3_Template;
            string TemplatePath = ExcelUtil.GetTemplatePath(templateName);
            Excel.Workbook targetWorkbook = sourceWorkbook.Application.Workbooks.Open(TemplatePath);
            targetWorkbook.Application.EnableEvents = false;
            Excel.XlCalculation xlCalculation = targetWorkbook.Application.Calculation;
            targetWorkbook.Application.Calculation = Excel.XlCalculation.xlCalculationManual;
            targetWorkbook.Application.ScreenUpdating = false;
            Excel.Worksheet qsSheet = ExcelUtil.GetWorkSheetByCodeName(targetWorkbook, Constants.SheetCodeName.QuestionSetting);
            pb.UpdateProgressBar(++pb.percentage, LocalResource.EX_PROGRESSBAR_UPDATING_QS);
            if (isQc4)
            {
                qsSheet.Cells[2, 12].Value = questions.SurveyTitle;
            }
            else
            {
                ext = ".qc3x";
                qsSheet.Rectangles("Text_Title").Text = questions.SurveyTitle;
                choiceColBegin++;
            }
            Object[,] qsLayout = GetQuestionLayout(usedQuestions, isQc4, questions);
            qsSheet.Application.EnableEvents = false;
            qsSheet.get_Range("A4").Resize[qsLayout.GetLength(0), qsLayout.GetLength(1)].Value = qsLayout;
            Excel.Worksheet qsBSheet = ExcelUtil.GetWorkSheetByCodeName(targetWorkbook, Constants.SheetCodeName.QuestionSettingB);
            qsBSheet.get_Range("A4").Resize[qsLayout.GetLength(0), qsLayout.GetLength(1)].Value = qsLayout;
            Excel.Range catRange = qsSheet.Cells[Constants.QS.StartRow, Constants.QS.ColCategoryCount].Resize[RowSize: usedQuestions.Count];
            Object[,] objAry = catRange.Value;
            if (objAry != null)
            {
                if (isQc4)
                {
                    for (int i = 1; i <= objAry.GetLength(0); i++)
                    {
                        Excel.Range range = qsSheet.Rows[i + Constants.QS.RowHeader];
                        QC4Common.Util.QSUtil.SetRowName(qsSheet, range, i - 1);
                        if (null == objAry[i, 1])
                        {
                            continue;
                        }
                        range = qsSheet.Cells[i + Constants.QS.RowHeader, choiceColBegin].Resize[ColumnSize: Convert.ToInt32(objAry[i, 1])];
                        range.Interior.Color = Constants.Color.LightGrey;
                        if (!String.IsNullOrEmpty(Convert.ToString(qsLayout[i-1, Constants.QS.QsColNumberSubTotal])))
                        {
                            range = qsSheet.Cells[i + Constants.QS.RowHeader, Constants.QS.QsColSubtotal1].Resize[ColumnSize: Convert.ToInt32(qsLayout[i-1, Constants.QS.QsColNumberSubTotal - 1]) * 2];
                            range.Interior.Color = Constants.Color.LightGrey;
                        }
                    }
                }
                else
                {
                    for (int i = 1; i <= objAry.GetLength(0); i++)
                    {
                        if (null == objAry[i, 1])
                        {
                            continue;
                        }

                        int count = Convert.ToInt32(objAry[i, 1]);
                        int rowResizeCount = count / 200;
                        if (count % 200 != 0) rowResizeCount++;
                        i--;
                        for (int j = 0; j < rowResizeCount; j++)
                        {
                            i++;
                            int c = 200;
                            if (c > count)
                            {
                                c = count;
                            }
                            count -= 200;
                            Excel.Range range = qsSheet.Cells[i + Constants.QS.RowHeader, choiceColBegin].Resize[ColumnSize: c];
                            range.Interior.Color = Constants.Color.LightGrey;
                        }
                    }
                }
            }
            pb.UpdateProgressBar(++pb.percentage, LocalResource.EX_PROGRESSBAR_UPDATING_QSB);
            string wbVariable = string.Empty;

            if (isQc4)
            {
                pb.UpdateProgressBar(++pb.percentage, LocalResource.EX_PROGRESSBAR_UPDATE_LIST);
                UpdateListView(ExcelUtil.GetWorkSheetByCodeName(targetWorkbook, Constants.SheetCodeName.List), usedQuestions);
                pb.UpdateProgressBar(++pb.percentage, LocalResource.EX_PROGRESSBAR_UPDATE_CROSS);
                UpdateCross(targetWorkbook, isQc4);
                pb.UpdateProgressBar(++pb.percentage, LocalResource.EX_PROGRESSBAR_UPDATE_GT);
                UpdateGt(targetWorkbook);
                pb.UpdateProgressBar(++pb.percentage, LocalResource.EX_PROGRESSBAR_UPDATE_DS);
                UpdateDetailSetting(sourceWorkbook, targetWorkbook, usedQuestions);
                pb.UpdateProgressBar(++pb.percentage, LocalResource.EX_PROGRESSBAR_UPDATE_SETTINGS);
                UpdateSetting(sourceWorkbook, targetWorkbook, usedQuestions, questions.SurveyTitle);
            }
            else
            {
                pb.UpdateProgressBar(++pb.percentage, LocalResource.EX_PROGRESSBAR_UPDATE_SETTINGS);
                UpdateSettingQC3(sourceWorkbook, targetWorkbook, usedQuestions, questions.SurveyTitle);
            }

            pb.UpdateProgressBar(++pb.percentage, LocalResource.EX_PROGRESSBAR_UPDATE_LIST);
            List<String> tempPathList = new List<string>();
            string fileName = divisionQuestion == null ? targetPath + ext : targetPath + "_" + divisionQuestion.Variable + "_0001" + ext;
            string tempFileName = isQc4 ? QcFileHelper.CreateTempFile(fileName, Constants.PathName.FileDataOutput) : fileName;
            targetWorkbook.Application.Calculation = xlCalculation;
            targetWorkbook.Application.ScreenUpdating = true;
            if (isQc4)
            {
                sourceWorkbook.Application.EnableEvents = false;
                targetWorkbook.Password = Constants.Password;
                targetWorkbook.Protect(Constants.Password, true); try
                {
                    targetWorkbook.SaveAs(tempFileName + "\\Temp.xlsx");
                }
                catch (Exception ex)
                {
                    targetWorkbook.Application.EnableEvents = true;
                    targetWorkbook.Application.Calculation = xlCalculation;
                    targetWorkbook.Application.ScreenUpdating = true;
                    if (targetWorkbook != null)
                        targetWorkbook.Close();
                    sourceWorkbook.Application.EnableEvents = true;
                    throw new Exception(ex.Message);
                }
                sourceWorkbook.Application.EnableEvents = true;
                targetWorkbook.Close();
                File.Move(tempFileName + "\\Temp.xlsx", tempFileName + "\\" + Constants.TemplateFile.QC4_Template);
                pb.UpdateProgressBar(++pb.percentage, LocalResource.EX_PROGRESSBAR_UPDATE_DB);
                UpdateQSDB(usedQuestions, tempFileName, wbVariable);
            }
            else
            {
                pb.UpdateProgressBar(++pb.percentage, LocalResource.EX_PROGRESSBAR_DS_CREATION);
                Excel.Worksheet dataSheet = ExcelUtil.GetWorkSheetByCodeName(targetWorkbook, Constants.SheetCodeName.Data);
                dataSheet.Copy(Type.Missing, targetWorkbook.Sheets[targetWorkbook.Sheets.Count]);
                dataSheet = targetWorkbook.Sheets[targetWorkbook.Sheets.Count];
                dataSheet.Name = "Data01";
                dataSheet._CodeName = Constants.SheetCodeName.Data01;
                dataSheet = ExcelUtil.GetWorkSheetByCodeName(targetWorkbook, Constants.SheetCodeName.Data01);
                if (usedQuestions.Count > QC4Common.Common.Constants.ExcelRowColumnMax.ExcelMaxCol)
                {
                    QC4Common.Util.ExcelUtil.GererateData02ForQC3(targetWorkbook);
                }
                if (usedQuestions.Count > QC4Common.Common.Constants.ExcelRowColumnMax.ExcelMaxCol)
                {
                    string[,] dataHeader;
                    for (int i = 0; i < 2; i++)
                    {
                        if (i == 0)
                        {
                            dataHeader = new string[1, QC4Common.Common.Constants.ExcelRowColumnMax.ExcelMaxCol];
                            for (int j = 0; j < QC4Common.Common.Constants.ExcelRowColumnMax.ExcelMaxCol; j++)
                            {
                                QuestionSettings qs = usedQuestions[j];
                                dataHeader[0, j] = qs.Variable;
                                switch (qs.AnswerType)
                                {
                                    case Constants.AnswerType.FA:
                                        Excel.Range c = dataSheet.Columns[j + 1];
                                        c.NumberFormat = "@";
                                        break;
                                    case Constants.AnswerType.D:
                                        c = dataSheet.Columns[j + 1];
                                        c.NumberFormat = "yyyy/mm/dd hh:mm:ss";
                                        break;
                                }
                            }
                            dataSheet.Cells[Constants.DataSheet.RowHeader, Constants.DataSheet.ColHeader].Resize[1, dataHeader.GetLength(1)].Value2 = dataHeader;
                        }
                        else
                        {
                            dataHeader = new string[1, (usedQuestions.Count - QC4Common.Common.Constants.ExcelRowColumnMax.ExcelMaxCol) + 1];
                            dataSheet = ExcelUtil.GetWorkSheetBySheetName(targetWorkbook, "Data02");
                            int k = QC4Common.Common.Constants.ExcelRowColumnMax.ExcelMaxCol;
                            for (int j = 0; j <= usedQuestions.Count - QC4Common.Common.Constants.ExcelRowColumnMax.ExcelMaxCol; j++)
                            {
                                if (j == 0)
                                {
                                    QuestionSettings qs = usedQuestions[j];
                                    dataHeader[0, j] = qs.Variable;
                                    switch (qs.AnswerType)
                                    {
                                        case Constants.AnswerType.FA:
                                            Excel.Range c = dataSheet.Columns[j + 1];
                                            c.NumberFormat = "@";
                                            break;
                                        case Constants.AnswerType.D:
                                            c = dataSheet.Columns[j + 1];
                                            c.NumberFormat = "yyyy/mm/dd hh:mm:ss";
                                            break;
                                    }
                                }
                                else
                                {
                                    QuestionSettings qs = usedQuestions[k];
                                    dataHeader[0, j] = qs.Variable;
                                    switch (qs.AnswerType)
                                    {
                                        case Constants.AnswerType.FA:
                                            Excel.Range c = dataSheet.Columns[j + 1];
                                            c.NumberFormat = "@";
                                            break;
                                        case Constants.AnswerType.D:
                                            c = dataSheet.Columns[j + 1];
                                            c.NumberFormat = "yyyy/mm/dd hh:mm:ss";
                                            break;
                                    }
                                    k++;
                                }
                            }
                            dataSheet.Cells[Constants.DataSheet.RowHeader, Constants.DataSheet.ColHeader].Resize[1, dataHeader.GetLength(1)].Value2 = dataHeader;
                        }
                    }
                }
                else
                {
                    string[,] dataHeader = new string[1, usedQuestions.Count];
                    for (int i = 0; i < usedQuestions.Count; i++)
                    {
                        QuestionSettings qs = usedQuestions[i];
                        dataHeader[0, i] = qs.Variable;
                        switch (qs.AnswerType)
                        {
                            case Constants.AnswerType.FA:
                                Excel.Range c = dataSheet.Columns[i + 1];
                                c.NumberFormat = "@";
                                break;
                            case Constants.AnswerType.D:
                                c = dataSheet.Columns[i + 1];
                                c.NumberFormat = "yyyy/mm/dd hh:mm:ss";
                                break;
                        }
                    }
                    dataSheet.Cells[Constants.DataSheet.RowHeader, Constants.DataSheet.ColHeader].Resize[1, dataHeader.GetLength(1)].Value2 = dataHeader;
                }
                try
                {
                    targetWorkbook.SaveAs(tempFileName);
                }
                catch(Exception ex)
                {
                    sourceWorkbook.Application.EnableEvents = false;
                    targetWorkbook.Application.EnableEvents = true;
                    targetWorkbook.Application.Calculation = xlCalculation;
                    targetWorkbook.Application.ScreenUpdating = true;
                    if (targetWorkbook != null)
                        targetWorkbook.Close();
                    sourceWorkbook.Application.EnableEvents = true;
                    throw new Exception(ex.Message);
                }

                sourceWorkbook.Application.EnableEvents = false;
                if (targetWorkbook != null)
                    targetWorkbook.Close();
                sourceWorkbook.Application.EnableEvents = true;
            }

            tempPathList.Add(tempFileName);

            DuplicateDivisionFile(isQc4, ref tempPathList, ref divisionQuestion);

            pb.UpdateProgressBar(++pb.percentage, LocalResource.EX_PROGRESSBAR_LOADING);
            GetRawDataArray(questions, usedQuestions.Select(q => q.Id).ToArray(), OutputDataType.QC3
                , "", "", "*", null, null, null, sourceWorkbook, tempPathList
                , String.Join("\t", usedQuestions.Select(V => V.Variable).ToArray())
                , divisionQuestion, usedQuestions, isQc4, targetPath, isQc4 ? Constants.DataOutput.FileType.QC4 : Constants.DataOutput.FileType.QC3);
            return true;
        }

        private void UpdateDetailSettingForQC3(Excel.Workbook sourceWorkbook, Excel.Workbook targetWorkbook, List<QuestionSettings> usedQuestions)
        {
            Excel.Worksheet sourceAdvSetting = ExcelUtil.GetWorkSheetByCodeName(sourceWorkbook, Constants.SheetCodeName.DetailsSetting);
            Excel.Range start = sourceAdvSetting.Range[Constants.AdvancedSetting.AdvSettingStartCell];
            Excel.Range end = ExcelUtil.EndxlUp(start);
            if (start.Row > end.Row)
            {
                return;
            }

            Excel.Range sourceTotal = sourceAdvSetting.get_Range(start, end);

            Excel.Worksheet targetAdvSetting = ExcelUtil.GetWorkSheetByCodeName(targetWorkbook, Constants.SheetCodeName.DetailsSetting);
            start = targetAdvSetting.Range[Constants.AdvancedSetting.AdvSettingStartCell];
            end = ExcelUtil.EndxlUp(start);
            Excel.Range targetTotal = targetAdvSetting.get_Range(start, end);
            if (end.Row < 2)
            {
                end = end.Worksheet.Cells[2, 1];
            }

            Excel.Range wbCrCheckS = sourceTotal.Find("F_Cr_Cross_AddUp_Combo_Summary_WeightBack_S", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
            if (null != wbCrCheckS && wbCrCheckS.Offset[0, 1].Value != null && wbCrCheckS.Offset[0, 1].Value.ToString() != ""&&
                usedQuestions.Where(q => q.Variable == wbCrCheckS.Offset[0, 1].Value).Count() > 0)
            {
                Excel.Range wbCrCheckT = targetTotal.Find("F_Cr_Cross_AddUp_Combo_Summary_WeightBack_S", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                        Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
                Excel.Range wbCrCheckTgt = targetTotal.Find("F_Gt_GT_AddUp_Combo_Summary_WeightBack_S", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                        Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
                if (wbCrCheckT == null)
                {
                    end = end.Offset[1, 0];
                    end.Value = "F_Cr_Cross_AddUp_Combo_Summary_WeightBack_S";
                    end.Next.Value = wbCrCheckS.Offset[0, 1].Value;
                }
                else
                {
                    wbCrCheckT.Offset[0, 1].Value = wbCrCheckS.Offset[0, 1].Value;
                }
                if (wbCrCheckTgt == null)
                {
                    end = end.Offset[1, 0];
                    end.Value = "F_Gt_GT_AddUp_Combo_Summary_WeightBack_S";
                    end.Next.Value = wbCrCheckS.Offset[0, 1].Value;
                }
                else
                {
                    wbCrCheckTgt.Offset[0, 1].Value = wbCrCheckS.Offset[0, 1].Value;
                }

                wbCrCheckT = targetTotal.Find("F_Cr_Cross_AddUp_Check_Summary_WeightBack_S", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                        Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
                wbCrCheckTgt = targetTotal.Find("F_Gt_GT_AddUp_Check_Summary_WeightBack_S", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                        Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
                if (wbCrCheckT == null)
                {
                    end = end.Offset[1, 0];
                    end.Value = "F_Cr_Cross_AddUp_Check_Summary_WeightBack_S";
                    end.Next.Value = "TRUE";
                }
                else
                {
                    wbCrCheckT.Offset[0, 1].Value = "TRUE";
                }
                if (wbCrCheckTgt == null)
                {
                    end = end.Offset[1, 0];
                    end.Value = "F_Gt_GT_AddUp_Check_Summary_WeightBack_S";
                    end.Next.Value = "TRUE";
                }
                else
                {
                    wbCrCheckTgt.Offset[0, 1].Value = "TRUE";
                }
            }
        }

        public void CsvTabMain(List<QuestionSettings> usedQuestions, Questions questions
            , string targetPath, ProgressBar pb, Constants.DataOutput.FileType outputFileType
            , string[] layoutPath, string[] dataPath, Excel.Workbook sourceWorkbook
            , string NALetter, string IVLetter, OutputDataType outputDataType
            , QuestionSettings divisionQuestion = null, bool[] filteringFlag = null, bool isRawdata = false)
        {

            progressBar = pb;
            string header = "";
            String ext = Constants.DataOutput.FileType.CSV == outputFileType ? "," : "\t";
            if (outputDataType != OutputDataType.Code)
            {
                StringBuilder str = new StringBuilder();
                for (int i = 0; i < usedQuestions.Count; i++)
                {
                    var x = usedQuestions[i];
                    if (x.AnswerType == Constants.AnswerType.MA)
                    {
                        for (int j = 0; j < x.CategoryCount; j++)
                        {
                            str.Append(x.Variable.Replace(",", "，") + "_" + (j + 1) + ext);
                        }
                    }
                    else
                    {
                        str.Append(x.Variable.Replace(",", "，") + ext);
                    }
                }
                str.Length--;
                header = str.ToString();
            }
            else
            {
                header = string.Join(ext, usedQuestions.Select(a => a.Variable.Replace(",", "，")).ToArray());
            }
            header += "\r\n";
            Encoding enc = Constants.DataOutput.defaultEncoding == "UTF-8" ? new UTF8Encoding(false) : Encoding.GetEncoding(Constants.DataOutput.defaultEncoding);
            for (int i = 0; i < dataPath.Count(); i++)
            {
                using (StreamWriter streamWriter = new StreamWriter(dataPath[i], true, enc))
                {
                    streamWriter.Write(header);
                }
            }

            GetRawDataArray(questions, usedQuestions.Select(q => q.Id).ToArray(), outputDataType
                , "", NALetter, IVLetter, null, null, filteringFlag, sourceWorkbook, dataPath.ToList()
                , header.ToString(), divisionQuestion, usedQuestions, false, targetPath, outputFileType, layoutPath, isRawdata);
        }

        public string ExcelMain(List<QuestionSettings> usedQuestions, Questions questions
            , string targetPath, ProgressBar pb, Constants.DataOutput.FileType outputFileType
            , string[] dataPath, Excel.Workbook sourceWorkbook, string NALetter, string IVLetter
            , OutputDataType outputDataType, QuestionSettings divisionQuestion = null)
        {
            progressBar = pb;
            GetRawDataArray(questions, usedQuestions.Select(q => q.Id).ToArray(), outputDataType
                , "", NALetter, IVLetter, null, null, null, sourceWorkbook, dataPath.ToList()
                , "", divisionQuestion, usedQuestions, false, targetPath, outputFileType);
            return EXMessage;
        }

        public static void GetDivisionFlag(ref bool[,] divisionFlag, ref string[,] divAry
            , QuestionSettings question, ref string[,] rawData, ref bool[] dataContainFlag)
        {
            if (null == question)
            {
                divisionFlag = new bool[rawData.GetLength(0), 1];
                for (int i = 0; i < rawData.GetLength(0); i++)
                {
                    divisionFlag[i, 0] = true;
                }
                if (null == dataContainFlag)
                {
                    dataContainFlag = new bool[] { false };
                }

                if (rawData != null && rawData.GetLength(0) > 0)
                {
                    dataContainFlag[0] = true;
                }
                return;
            }

            int maxRow = divAry.GetLength(0);
            int maxCol = question.CategoryCount;
            divisionFlag = new bool[maxRow, maxCol];
            if (null == dataContainFlag)
            {
                dataContainFlag = new bool[question.CategoryCount];
            }

            switch (question.AnswerType)
            {
                case Constants.AnswerType.SA:
                    for (int i = 0; i < divAry.GetLength(0); i++)
                    {
                        if (string.IsNullOrEmpty(divAry[i, 0]) || "*".Equals(divAry[i, 0]))
                        {
                            continue;
                        }
                        try
                        {
                            int index = Convert.ToInt32(divAry[i, 0]) - 1;
                            divisionFlag[i, index] = true;
                            dataContainFlag[index] = true;
                        }
                        catch { }
                    }
                    break;
                case Constants.AnswerType.MA:
                    for (int i = 0; i < divAry.GetLength(0); i++)
                    {
                        string str = divAry[i, 0];
                        if (string.IsNullOrEmpty(str) || "*".Equals(divAry[i, 0]))
                        {
                            continue;
                        }

                        char[] choices = str.ToCharArray();
                        for (int j = choices.Length - 1, k = 0; j >= 0; j--, k++)
                        {
                            if (choices[j] == '0')
                            {
                                continue;
                            }
                            divisionFlag[i, k] = true;
                            dataContainFlag[k] = true;
                        }
                    }
                    break;
            }
        }

        private void DuplicateDivisionFile(bool isQc4, ref List<string> tempPathList, ref QuestionSettings divQ)
        {
            if (null == divQ)
            {
                return;
            }

            if (tempPathList.Count == 0)
            {
                return;
            }



            if (isQc4)
            {
                string dir = tempPathList[0].Remove(tempPathList[0].Count() - 4, 4);
                for (int i = 2; i <= divQ.CategoryCount; i++)
                {
                    FileUtil.DirectoryCopy(tempPathList[0], dir + i.ToString("0000"), false);
                    tempPathList.Add(dir + i.ToString("0000"));
                }
            }
            else
            {
                string str = tempPathList[0];
                string dir = str.Remove(tempPathList[0].Count() - 9, 9);
                for (int i = 2; i <= divQ.CategoryCount; i++)
                {
                    File.Copy(str, dir + i.ToString("0000") + ".qc3x");
                    tempPathList.Add(dir + i.ToString("0000") + ".qc3x");
                }
            }
        }

        private void UpdateSettingSheet(Excel.Workbook sourceWorkbook, Excel.Workbook targetWorkbook, ref string wbVariable)
        {
            Excel.Worksheet sourceWorksheet = ExcelUtil.GetWorkSheetByCodeName(sourceWorkbook, Constants.SheetCodeName.Setting);
            Excel.Worksheet targetWorksheet = ExcelUtil.GetWorkSheetByCodeName(targetWorkbook, Constants.SheetCodeName.Setting);
            targetWorksheet.get_Range("C1", "F17").Value = sourceWorksheet.get_Range("C1", "F17").Value;
            Excel.Range s = sourceWorksheet.get_Range("I2");
            Excel.Range e = ExcelUtil.EndxlUp(s);
            Excel.Range r = sourceWorksheet.get_Range(s, e.Offset[1, 4]);
            Object[,] obj = r.Value2;
            targetWorksheet.get_Range("I2").Resize[obj.GetLength(0), obj.GetLength(1)].Value2 = obj;
            wbVariable = sourceWorksheet.Range["J2"].Text;
        }

        private void UpdateCross(Excel.Workbook workbook, bool isQc4)
        {
            Excel.Worksheet qs = ExcelUtil.GetWorkSheetByCodeName(workbook, Constants.SheetCodeName.QuestionSetting);
            Excel.Range start = qs.Cells[Constants.QS.StartRow, Constants.QS.ColVariable];
            Excel.Range end = ExcelUtil.EndxlUp(start);
            Object[,] obj = qs.get_Range(start, end.Offset[1, 3]).Value2;
            int rowCount = obj.GetLength(0);
            Object[,] objAry = new Object[rowCount, 4];

            int index = 0;
            for (int i = 2; i <= rowCount; i++)
            {
                if (obj[i, 2] == null || obj[i, 2].ToString() == Constants.AnswerType.D || (obj[i, 2].ToString() == Constants.AnswerType.FA))
                {
                    continue;
                }
                objAry[index, 1] = obj[i, 1];
                objAry[index, 2] = obj[i, 2];
                objAry[index, 3] = obj[i, 3];
                objAry[index, 0] = ++index;
            }

            Excel.Worksheet crossSheet = ExcelUtil.GetWorkSheetByCodeName(workbook, Constants.SheetCodeName.CrossTabulation);
            crossSheet.Range["B14"].Resize[rowCount, 4].Value2 = objAry;
        }

        private void UpdateGt(Excel.Workbook workbook)
        {
            try
            {
                Excel.Worksheet gtSheet = ExcelUtil.GetWorkSheetByCodeName(workbook, Constants.SheetCodeName.GTTabulation);
                QC4Common.Common.GTAutoSetting.FNCGTAutoSettingMainIni(gtSheet);
                QC4Common.Common.GTAutoSetting.FNCGetQuesData(gtSheet, null, ExcelUtil.GetWorkSheetByCodeName(workbook, Constants.SheetCodeName.QuestionSetting));
                QC4Common.Common.GTAutoSetting.LoadDefaultDataToGTHiddenSheet(gtSheet);
            }
            catch (Exception ex) { _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException); }
        }

        private void UpdateQSDB(List<QuestionSettings> questions, string tempPath, string wbVar)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("CREATE TABLE IF NOT EXISTS `answers` (sample_id VARCHAR(255) ,sort_no INTEGER PRIMARY KEY AUTOINCREMENT,");
            int max = questions.Count();
            for (int j = 1; j < max; j++)
            {
                QuestionSettings qs = questions[j];

                string dataType = "TEXT";
                switch (qs.AnswerType)
                {
                    case Constants.AnswerType.SA:
                        dataType = "TEXT";
                        break;
                    case Constants.AnswerType.D:
                        dataType = "TEXT";
                        break;
                    case Constants.AnswerType.N:
                        dataType = "TEXT";
                        break;
                }
                sql.Append("`q_" + j + "` " + dataType + " NULL ,");
            }
            sql.Remove(sql.Length - 1, 1);
            sql.Append(")");

            using (SQLiteConnection con = new SQLiteConnection(DB.DBHelper.GetConnectionString(tempPath + "\\" + Constants.TemplateFile.DB_FIlE)))
            {
                con.Open();
                using (SQLiteCommand command = con.CreateCommand())
                {
                    command.CommandText = "CREATE TABLE IF NOT EXISTS `question` (id INTEGER, variable TEXT ,answer_type TEXT,category_count int, question_flag VARCHAR(5))";
                    command.ExecuteNonQuery();
                    command.CommandText = sql.ToString();
                    command.ExecuteNonQuery();
                    command.CommandText = "CREATE TABLE IF NOT EXISTS `weight_back` (id INTEGER PRIMARY KEY AUTOINCREMENT,name VARCHAR(255) ,weight_back_table VARCHAR(255) );";
                    command.ExecuteNonQuery();
                }
                using (SQLiteTransaction tr = con.BeginTransaction())
                {
                    using (SQLiteCommand mapSql = con.CreateCommand())
                    {
                        mapSql.Transaction = tr;
                        for (int j = 0; j < max; j++)
                        {
                            QuestionSettings qs = questions[j];
                            mapSql.CommandText = "insert into question(id, variable, answer_type,category_count,question_flag) values(@id, @variable, @answer,@count,@flag)";
                            mapSql.Parameters.Add(new SQLiteParameter("@id", j));
                            mapSql.Parameters.Add(new SQLiteParameter("@variable", qs.Variable));
                            mapSql.Parameters.Add(new SQLiteParameter("@answer", qs.AnswerType));
                            mapSql.Parameters.Add(new SQLiteParameter("@count", qs.CategoryCount));
                            mapSql.Parameters.Add(new SQLiteParameter("@flag", "Org"));
                            mapSql.ExecuteNonQuery();
                        }
                        tr.Commit();
                    }
                }
            }
        }

        private bool UpdateSurveyTitle(string surveyTitle, bool isQc4 = false)
        {
            return true;
        }

        private Object[,] GetQuestionLayout(List<QuestionSettings> questions, bool isQc4 = false, Questions questionsMap = null)
        {
            int colFLag = 1;
            int colQuestionNumber = 2;
            int colQuestionType = 3;
            int colQuestionCount = 4;
            int colVariable = 5;
            int colAnswerType = 6;
            int colCategoryCount = 7;
            int colWT = 8;
            int colSort = 9;
            int colColumn = 10;
            int colTableHeading;
            int colQuestion;
            int colChoiceBegin;
            int colCount = 1013;
            int colCountBase = 1014;
            int colAddSubTotal = 1015;
            int colNumberOfSubTotal = 1016;
            int colSubTotal1 = 1017;
            int colcriteria1 = 1018;

            int maxRow = 0;
            int maxCol = 212;
            if (isQc4)
            {
                colTableHeading = 10;
                colQuestion = 11;
                colChoiceBegin = 12;
                maxRow = questions.Count + 5;
                maxCol = 1050;
                colCount = 1013;
                colCountBase = 1014;
                colAddSubTotal = 1015;
                colNumberOfSubTotal = 1016;
                colSubTotal1 = 1017;
                colcriteria1 = 1018;
            }
            else
            {
                colTableHeading = 11;
                colQuestion = 12;
                colChoiceBegin = 13;
                foreach (QuestionSettings qs in questions)
                {
                    if (null != qs.Choices && qs.CategoryCount > 200)
                    {
                        int cMaxRow = qs.CategoryCount / 200;
                        if (qs.CategoryCount % 200 != 0)
                        {
                            cMaxRow++;
                        }
                        maxRow += cMaxRow;
                    }
                    else
                        maxRow++;
                }
                maxCol = 251;
                colCount = 214;
                colCountBase = 215;
                colAddSubTotal = 216;
                colNumberOfSubTotal = 217;
                colSubTotal1 = 218;
                colcriteria1 = 219;
            }


            Object[,] qsArray = new Object[maxRow, maxCol];
            int rowIndex = 0;
            int column = 1;

            int prevMatrixRow = 0;
            int prevMatrixCount = 0;
            string prevMatrixQuestion = null;
            string prevMatrixQType = "";
            Dictionary<string, int> matrixGroupCount = new Dictionary<string, int>();

            QuestionSettings prevParentMatrixQuestion=null;

            foreach (QuestionSettings qs in questions)
            {
                Questions.Question question = questionsMap[qs.Id] as Questions.Question;
                if ((question.QuestionType & QuestionType.MatrixChild) == QuestionType.MatrixChild)
                {
                    QuestionSettings parent = Definiotion.VariableDictionary[question.ParentQuestion.Name];
                    if (null == prevMatrixQuestion)
                    {
                        prevMatrixRow = rowIndex;
                        prevMatrixCount = 1;
                        prevMatrixQuestion = parent.QuestionNumber;
                        prevMatrixQType = parent.QuestionType;
                        if (!matrixGroupCount.ContainsKey(prevMatrixQuestion))
                        {
                            matrixGroupCount.Add(prevMatrixQuestion, 1);
                        }
                        else
                        {
                            matrixGroupCount[prevMatrixQuestion]++;
                        }
                    }
                    else if (parent.QuestionNumber.Equals(prevMatrixQuestion) && parent.Id.Equals(prevParentMatrixQuestion.Id))
                    {
                        prevMatrixCount++;
                    }
                    else
                    {
                        qsArray[prevMatrixRow, colQuestionNumber] = matrixGroupCount[prevMatrixQuestion] == 1 ? SetSpace(prevMatrixQuestion) : SetSpace(prevMatrixQuestion + "_" + matrixGroupCount[prevMatrixQuestion]);
                        qsArray[prevMatrixRow, colQuestionCount] = prevMatrixCount == 0 ? "" : prevMatrixCount.ToString();
                        qsArray[prevMatrixRow, colQuestionType] = prevMatrixQType;
                        prevMatrixRow = rowIndex;
                        prevMatrixCount = 1;
                        prevMatrixQuestion = SetSpace(parent.QuestionNumber);
                        prevMatrixQType = parent.QuestionType;
                        if (!matrixGroupCount.ContainsKey(prevMatrixQuestion))
                        {
                            matrixGroupCount.Add(prevMatrixQuestion, 1);
                        }
                        
                    }
                    prevParentMatrixQuestion = parent;
                }
                else
                {
                    if (null != prevMatrixQuestion)
                    {
                        qsArray[prevMatrixRow, colQuestionNumber] = matrixGroupCount[prevMatrixQuestion] == 1 ? SetSpace(prevMatrixQuestion) : SetSpace(prevMatrixQuestion + "_" + matrixGroupCount[prevMatrixQuestion]);
                        qsArray[prevMatrixRow, colQuestionCount] = prevMatrixCount == 0 ? "" : prevMatrixCount.ToString();
                        qsArray[prevMatrixRow, colQuestionType] = prevMatrixQType;
                        prevMatrixRow = 0;
                        prevMatrixCount = 0;
                        prevMatrixQuestion =  null;
                        prevMatrixQType = "";
                    }
                    qsArray[rowIndex, colQuestionNumber] = SetSpace(qs.QuestionNumber);
                    qsArray[rowIndex, colQuestionType] = qs.QuestionType;
                    qsArray[rowIndex, colQuestionCount] = qs.QuestionCount == null || qs.QuestionCount == 0 ? "" : qs.QuestionCount.ToString();
                }


                qsArray[rowIndex, colVariable] = qs.Variable;
                qsArray[rowIndex, colAnswerType] = qs.AnswerType;
                qsArray[rowIndex, colCategoryCount] = qs.CategoryCount == 0 ? "" : qs.CategoryCount.ToString();
                qsArray[rowIndex, colWT] = qs.Score;
                qsArray[rowIndex, colSort] = qs.Sort;
                qsArray[rowIndex, colTableHeading] = SetSpace(qs.TableHeading);
                qsArray[rowIndex, colQuestion] = SetSpace(qs.Question);
                qsArray[rowIndex, colCount] = qs.Count;
                qsArray[rowIndex, colCountBase] = SetSpace(qs.CountBase);
                qsArray[rowIndex, colAddSubTotal] = SetSpace(qs.AddSubTotal);
                qsArray[rowIndex, colNumberOfSubTotal] = qs.SubTotalCount == 0 ? "" : qs.SubTotalCount.ToString();

                if (qs.SubTotalCount > 0 && null != qs.SubTotals)
                {
                    for (int i = 0; i < qs.SubTotals.Count; i++)
                    {
                        qsArray[rowIndex, colSubTotal1 + 2 * i] = SetSpace(qs.SubTotals[i].Subtotal);
                        qsArray[rowIndex, colcriteria1 + 2 * i] = SetSpace(qs.SubTotals[i].Criteria);
                    }
                }

                if (isQc4)
                {
                    qsArray[rowIndex, colFLag] = "Org";
                    for (int i = 0; i < qs.CategoryCount; i++)
                    {
                        qsArray[rowIndex, i + colChoiceBegin] = SetSpace(qs.Choices[i]);
                    }
                }
                else
                {
                    qsArray[rowIndex, colColumn] = (column++).ToString();

                    int cMaxRow = qs.CategoryCount / 200;
                    if (qs.CategoryCount % 200 != 0)
                    {
                        cMaxRow++;
                    }
                    int cIndex = 0;
                    bool change = false;
                    int count = qs.CategoryCount;
                    for (int i = 0; i < cMaxRow; i++)
                    {
                        int max = (i + 1) * 200 > qs.CategoryCount ? count : 200;
                        for (int j = 0; j < max; j++)
                        {
                            qsArray[rowIndex, colChoiceBegin + j] = SetSpace(qs.Choices[cIndex++]);
                        }
                        count -= max;
                        change = true;
                        colChoiceBegin = 13;
                        rowIndex++;
                    }
                    if (change)
                        rowIndex--;
                }
                rowIndex++;
            }

            if (null != prevMatrixQuestion)
            {
                qsArray[prevMatrixRow, colQuestionNumber] = matrixGroupCount[prevMatrixQuestion] == 1 ? SetSpace(prevMatrixQuestion) : SetSpace(prevMatrixQuestion + "_" + matrixGroupCount[prevMatrixQuestion]);
                qsArray[prevMatrixRow, colQuestionCount] = prevMatrixCount == 0 ? "" : prevMatrixCount.ToString();
                qsArray[prevMatrixRow, colQuestionType] = prevMatrixQType;
            }
            return qsArray;
        }

        public void UpdateListView(Excel.Worksheet sheet, List<QuestionSettings> questions)
        {
            sheet.Cells[1, 1].CurrentRegion.Offset[1].ClearContents();

            ArrayList saArray = new ArrayList();
            ArrayList maArray = new ArrayList();
            ArrayList nArray = new ArrayList();
            ArrayList samaArray = new ArrayList();
            ArrayList sanArray = new ArrayList();
            ArrayList manArray = new ArrayList();
            ArrayList samanArray = new ArrayList();
            ArrayList faArray = new ArrayList();
            ArrayList allArray = new ArrayList();
            ArrayList allDArray = new ArrayList();
            String[,] outPutArray = new string[questions.Count, 10];

            foreach (var qs in questions)
            {
                string variable = qs.Variable;
                string ansType = qs.AnswerType;

                switch (ansType)
                {
                    case Constants.AnswerType.SA:
                        saArray.Add(variable);
                        samaArray.Add(variable);
                        sanArray.Add(variable);
                        samanArray.Add(variable);
                        allArray.Add(variable);
                        allDArray.Add(variable);
                        break;

                    case Constants.AnswerType.MA:
                        maArray.Add(variable);
                        samaArray.Add(variable);
                        manArray.Add(variable);
                        samanArray.Add(variable);
                        allArray.Add(variable);
                        allDArray.Add(variable);
                        break;

                    case Constants.AnswerType.N:
                        nArray.Add(variable);
                        sanArray.Add(variable);
                        manArray.Add(variable);
                        samanArray.Add(variable);
                        allArray.Add(variable);
                        allDArray.Add(variable);
                        break;

                    case Constants.AnswerType.FA:
                        faArray.Add(variable);
                        allArray.Add(variable);
                        allDArray.Add(variable);
                        break;

                    case Constants.AnswerType.D:
                        allDArray.Add(variable);
                        break;
                }
            }
            ListOutPutAdd(outPutArray, saArray, 0);
            ListOutPutAdd(outPutArray, maArray, 1);
            ListOutPutAdd(outPutArray, nArray, 2);
            ListOutPutAdd(outPutArray, samaArray, 3);
            ListOutPutAdd(outPutArray, sanArray, 4);
            ListOutPutAdd(outPutArray, manArray, 5);
            ListOutPutAdd(outPutArray, samanArray, 6);
            ListOutPutAdd(outPutArray, faArray, 7);
            ListOutPutAdd(outPutArray, allArray, 8);
            ListOutPutAdd(outPutArray, allDArray, 9);

            sheet.get_Range("A2", "J" + (allDArray.Count + 1)).Value = outPutArray;
        }

        private void ListOutPutAdd(String[,] outPutArray, ArrayList list, int n)
        {
            for (int i = 0; i < list.Count; i++)
            {
                outPutArray[i, n] = list[i].ToString();
            }
        }

        private void UpdateSettingSheet()
        {

        }
        int PbVal = 0;
        int PbCount = 80;
        public QCWebException GetRawDataArray(Questions questions
            , decimal[] questionids, OutputDataType datatype, string dirpath, string NALetter, string IVLetter
            , bool[] divisiblePoint, QCAnswerType[] columnQCAnsType, bool[] filteringFlag = null, Excel.Workbook workbook = null
            , List<string> outPuthPath = null, string headerRow = "", QuestionSettings divisonVariable = null
            , List<QuestionSettings> usedQuestions = null, bool isQc4 = true, string targetPath = null
            , Constants.DataOutput.FileType outputType = Constants.DataOutput.FileType.QC3, string[] layoutOutPath = null,bool isRawdata=false)
        {
            string[,] resultArray = null;
            int columnsCount = 0;
            string toptablename = null;
            QCWebException exception = null;
            if (!checkGetRawDataArrayArguments(questions, questionids
                    , ref datatype, ref toptablename, ref columnsCount, out divisiblePoint, out columnQCAnsType, out exception, isQc4))
            {
                return exception;
            }
            columnQCAnsType = null;
            divisiblePoint = null;

            int clmIdx = -1;
            int dataCount = 0;
            int count = 0;
            int allDataCount = 0;

            string[,] divAry = null;


            string connectionString = DB.DBHelper.GetConnectionString(workbook);
            SQLiteConnection con = new SQLiteConnection(connectionString);

            filteringFlag = null;
            if (filterSettings != null && filterSettings.Count > 0)
            {
                string filterExp = Macromill.QCWeb.Logic.TabulationEx.Criteria.CriteriaDescProvider.CreateCriteriaDescriptions(filterSettings, questions);
                filteringFlag = new Criteria(filterExp, "", questions).Filtering(connectionString, tableName);
            }

            try
            {
                con.Open();
                var c = DB.DBHelper.GetDataTable("select count(*) from "+ tableName, con);
                count = Convert.ToInt32(c.Rows[0][0]);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

            bool[] dataFoundFlag = null;
            if (count == 0)
            {
                progressBar.UpdateProgressBar(++progressBar.percentage, LocalResource.EX_PROGRESSBAR_REMOVING_UNWANTED_FILES);
                dataFoundFlag =  divisonVariable == null ? new bool[] { false } : new bool[divisonVariable.CategoryCount];
                AlertAndRemoveFiles(dataFoundFlag, outPuthPath, divisonVariable, targetPath, outputType, layoutOutPath);
                return null;
            }

            int maxLimit = Constants.MaxRowCount;
            int max = count / maxLimit;
            int GetDataPB = 5;
            if (max > 0)
            {
                GetDataPB = max * 3;
                PbCount = 80 / max;
                GetDataPB = GetDataPB / max;
            }
            if (count % maxLimit != 0) max++;

            decimal pbVal = (decimal)80 / max / questionids.Length;
            decimal pbVal1 = (decimal)7 / max / 3;
            SeparatedValuesBase sv = new SeparatedValuesBase();
            long lastVal = 0;
            int filterIndex = 0;
            int filterIndexEnd = 0;

            for (int j = 0; j < max; j++)
            {
                bool[] filterFlag = null;

                if (filteringFlag != null && filteringFlag.Count() > 0)
                {
                    filterFlag = new bool[maxLimit];
                    filterIndexEnd = maxLimit;
                    if (filterIndexEnd + filterIndex > count)
                    {
                        filterIndexEnd = count - filterIndex;
                    }
                    for (int i = 0; i < filterIndexEnd; i++)
                    {
                        filterFlag[i] = filteringFlag[filterIndex++];
                    }
                }
                if (null != divisonVariable)
                {
                    int clmIdxDiv = -1;
                    int dataCountDiv = 0;
                    int allDataCountDiv = 0;
                    Questions.Question question = questions[divisonVariable.Id] as Questions.Question;
                    string questionFlag = Definiotion.VariableDictionary[question.Name].QuestionFlag;

                    if (!putData(question, OutputDataType.Code, 1, dirpath, "", "*"
                                        , ref dataCountDiv, ref allDataCountDiv, ref divAry, ref clmIdxDiv, out exception, ref filterFlag, ref lastVal, connectionString, maxLimit, questionFlag, con, true))
                    {
                        return exception;
                    }
                }

                if (isQc4)
                {
                    GetQcRawData(questions, questionids, connectionString, ref lastVal, ref resultArray, maxLimit, usedQuestions, filterFlag);
                    progressBar.percentage += GetDataPB;
                    progressBar.UpdateProgressBar(progressBar.percentage);
                }
                else
                {
                    for (var i = 0; i < questionids.Length; ++i)
                    {
                        Questions.Question question = questions[questionids[i]] as Questions.Question;
                        string questionFlag = usedQuestions.First(x => x.Id == question.ID).QuestionFlag;
                        if (null == question)
                        {
                            continue;
                        }
                        if (Constants.DataOutput.FileType.QLayout.Equals(outputType))
                        {

                            if (!putDataQrowdata(question, datatype, columnsCount, dirpath, NALetter, IVLetter
                                    , ref dataCount, ref allDataCount, ref resultArray, ref clmIdx, out exception
                                    , ref filterFlag, ref lastVal, connectionString, maxLimit, questionFlag, con, isQc4))
                            {
                                return exception;
                            }
                        }
                        else
                        {
                            if (!putData(question, datatype, columnsCount, dirpath, NALetter, IVLetter
                                    , ref dataCount, ref allDataCount, ref resultArray, ref clmIdx, out exception, ref filterFlag, ref lastVal, connectionString, maxLimit, questionFlag, con, isQc4, outputType))
                            {
                                return exception;
                            }
                        }
                        progressBar.percentage += pbVal;
                        progressBar.UpdateProgressBar(progressBar.percentage);
                    }
                }

                bool[,] divFlag = null;
                PbVal = (int)(pbVal1 * 3) + 4;
                progressBar.percentage += pbVal1;
                progressBar.UpdateProgressBar(progressBar.percentage, LocalResource.EX_PROGRESSBAR_LOADING_FLAG);
                GetDivisionFlag(ref divFlag, ref divAry, divisonVariable, ref resultArray, ref dataFoundFlag);
                progressBar.percentage += pbVal1;
                progressBar.UpdateProgressBar(progressBar.percentage, LocalResource.EX_PROGRESSBAR_WRITING);
                string expType = OutputDataType.Code == datatype ? "CODE" : OutputDataType.Decode == datatype ? "Decode" : "";
                WriteData(resultArray, outPuthPath, divFlag, usedQuestions, outputType, ref dataFoundFlag, targetPath, divisonVariable, workbook.Application, expType, isRawdata,isLast: j + 1 == max);
                progressBar.percentage += pbVal1;
                progressBar.UpdateProgressBar(progressBar.percentage, LocalResource.EX_PROGRESSBAR_LOADING);
                string lastValSql = "select sort_no from (select sort_no from " + tableName + " where sort_no > " + lastVal + " order by sort_no limit " + maxLimit + ") order by sort_no desc limit 1";
                var lastVa = DB.DBHelper.GetDataTable(lastValSql, con);
                lastVal = Convert.ToInt32(lastVa.Rows[0][0]);

                clmIdx = -1;
                dataCount = 0;
                allDataCount = 0;
                resultArray = null;
            }
            progressBar.UpdateProgressBar(++progressBar.percentage, LocalResource.EX_PROGRESSBAR_REMOVING_UNWANTED_FILES);
            AlertAndRemoveFiles(dataFoundFlag, outPuthPath, divisonVariable, targetPath, outputType, layoutOutPath);
            progressBar.UpdateProgressBar(progressBar.percentage, LocalResource.EX_PROGRESSBAR_FINISHING);
            con.Close();
            con.Dispose();
            return null;
        }

        internal bool putData(Questions.Question question, OutputDataType datatype
                          , int columnsCount, string dirpath, string NALetter, string IVLetter
                          , ref int dataCount, ref int allDataCount, ref string[,] puttoarray, ref int clmIdx
                          , out QCWebException exception, ref bool[] filteringFlag, ref long lastValue
                          , string conString, int maxLimit,string questionFlag , SQLiteConnection con = null, bool isQc4 = true, Constants.DataOutput.FileType fileType = Constants.DataOutput.FileType.NONE)
        {
            exception = null;
            if ((question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
            {
                foreach (DictionaryEntry de in question.ChildQuestions)
                {
                    Questions.Question childQ = de.Value as Questions.Question;
                    if (!putData(childQ, datatype, columnsCount, dirpath, NALetter, IVLetter
                            , ref dataCount, ref allDataCount, ref puttoarray, ref clmIdx, out exception, ref filteringFlag, ref lastValue, conString, maxLimit, questionFlag, con, isQc4))
                    {
                        return false;
                    }
                }
                return true;
            }
            QuestionType qType = question.QuestionType & (QuestionType.SA | QuestionType.MA | QuestionType.N | QuestionType.FA);
            if ((int)qType == 0)
            {
                return false;
            }
            List<Data> data = null;
            try
            {
                string sql = "";
                if (questionFlag == "An")
                {
                    sql = "SELECT m.`" + question.ColumnName + "` FROM multivariate m join " + tableName + " a on m.sort_no = a.sort_no  WHERE m.sort_no > " + lastValue + " ORDER BY m.sort_no LIMIT " + maxLimit;
                }
                else
                    sql = "SELECT " + question.ColumnName + " FROM " + tableName + " WHERE sort_no > " + lastValue + " ORDER BY sort_no LIMIT " + maxLimit;
                var dtReadTable1 = DB.DBHelper.GetDataTable(sql, con);
                data = ReadTextFile.ReadDataTable(dtReadTable1, qType, null, out exception);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

            if (data == null) return false;
            if (clmIdx == -1)
            {
                allDataCount = data.Count;
                int l = filteringFlag == null ? 0 : filteringFlag.Length;
                if (filteringFlag == null)
                {
                    filteringFlag = new bool[allDataCount];
                }
                if (l != allDataCount)
                {
                    Array.Resize<bool>(ref filteringFlag, allDataCount);
                    if (l < allDataCount) Macromill.QCWeb.Common.OperateArray.InitializeWith<bool>(ref filteringFlag, true, l, allDataCount - 1);
                }
                dataCount = 1;
                for (int i = 0; i < allDataCount; ++i)
                {
                    if (!data[i].IsDeleted && filteringFlag[i]) ++dataCount;
                }
                puttoarray = new string[--dataCount, columnsCount];
            }
            else if (data.Count != allDataCount)
            {
                return false;
            }
            string[] sectorDecArray = null;
            if ((qType == QuestionType.SA || qType == QuestionType.MA) && datatype == OutputDataType.Decode)
            {
                sectorDecArray = new string[question.Sectors.Count];
                for (int i = 0; i < question.Sectors.Count; ++i)
                {
                    sectorDecArray[i] = (i + 1).ToString("0000") + ":" + question.Sectors[i + 1].Description;
                }
            }
            if (qType == QuestionType.MA && datatype != OutputDataType.Code && datatype != OutputDataType.QC3)
            {
                int startClmIdx = clmIdx;
                int rowIdx = -1;
                for (int i = 0; i < data.Count; ++i)
                {
                    if (data[i].IsDeleted || !filteringFlag[i]) continue;
                    ++rowIdx;
                    clmIdx = startClmIdx;
                    switch (data[i].DataType)
                    {
                        case DataType.NormalData:
                        case DataType.NAData:
                            bool isNA = true;
                            if (data[i].DataType == DataType.NormalData)
                            {
                                for (int j = 0; j < question.Sectors.Count; ++j)
                                {
                                    int idx = j / GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                                    int e = j % GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                                    if (((data[i] as MAData).Value(idx) & (int)Math.Pow(2.0, (double)e)) != 0)
                                    {
                                        puttoarray[rowIdx, ++clmIdx] = datatype == OutputDataType.Flag ? "1" : sectorDecArray[j];
                                        isNA = false;
                                    }
                                    else
                                    {
                                        if (datatype == OutputDataType.Decode)
                                            puttoarray[rowIdx, ++clmIdx] = "";
                                        else
                                            puttoarray[rowIdx, ++clmIdx] = datatype == OutputDataType.Flag ? "0" : null;
                                    }
                                }
                            }
                            if (isNA)
                            {
                                for (int j = 0; j < question.Sectors.Count; ++j)
                                {
                                    puttoarray[rowIdx, ++clmIdx] = NALetter;
                                }
                            }
                            break;
                        case DataType.IVData:
                            for (int j = 0; j < question.Sectors.Count; ++j)
                            {
                                puttoarray[rowIdx, ++clmIdx] = IVLetter;
                            }
                            break;
                    }
                }
            }
            else
            {
                ++clmIdx;
                int rowIdx = -1;
                for (int i = 0; i < data.Count; ++i)
                {
                    if (data[i].IsDeleted || !filteringFlag[i]) continue;
                    ++rowIdx;
                    switch (data[i].DataType)
                    {
                        case DataType.NormalData:
                            switch (qType)
                            {
                                case QuestionType.SA:
                                    if (datatype == OutputDataType.Decode)
                                    {
                                        puttoarray[rowIdx, clmIdx] = sectorDecArray[(data[i] as SAData).Value - 1];
                                    }
                                    else
                                    {
                                        puttoarray[rowIdx, clmIdx] = (data[i] as SAData).Value.ToString();
                                    }
                                    break;
                                case QuestionType.MA:

                                    if (isQc4)
                                    {
                                        string d = "";
                                        for (int j = question.Sectors.Count - 1; j >= 0; --j)
                                        {
                                            int idx = j / GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                                            int e = j % GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                                            if (((data[i] as MAData).Value(idx) & (int)Math.Pow(2.0, (double)e)) != 0)
                                            {
                                                d += "1";
                                            }
                                            else
                                            {
                                                d += "0";
                                            }
                                        }
                                        puttoarray[rowIdx, clmIdx] = d;
                                    }
                                    else
                                    {
                                        if (datatype == OutputDataType.Code)
                                        {
                                            puttoarray[rowIdx, clmIdx] = (data[i] as MAData).CodeValue;
                                        }
                                        else
                                        {
                                            puttoarray[rowIdx, clmIdx] = (data[i] as MAData).CodeValue==""?"": "," + (data[i] as MAData).CodeValue + ",";
                                        }
                                    }
                                    break;
                                case QuestionType.FA:
                                    if (fileType == Constants.DataOutput.FileType.QC3 || fileType == Constants.DataOutput.FileType.QC4 || fileType == Constants.DataOutput.FileType.Excel2007)
                                    {
                                        if (fileType == Constants.DataOutput.FileType.QC4)
                                            puttoarray[rowIdx, clmIdx] = (data[i] as FAData).Value.StartsWith("=") ? SetSpace("'" + (data[i] as FAData).Value) : SetSpace((data[i] as FAData).Value);
                                        else
                                            puttoarray[rowIdx, clmIdx] = SetSpace((data[i] as FAData).Value);
                                    }
                                    else
                                        puttoarray[rowIdx, clmIdx] = (data[i] as FAData).Value.StartsWith("=") ? "'" + (data[i] as FAData).Value : (data[i] as FAData).Value;
                                    break;
                                case QuestionType.N:
                                    puttoarray[rowIdx, clmIdx] = getConveted_Exponent_Value((data[i] as NData).Value.ToString());
                                    break;
                            }
                            break;
                        case DataType.NAData:
                            puttoarray[rowIdx, clmIdx] = NALetter;
                            break;
                        case DataType.IVData:
                            puttoarray[rowIdx, clmIdx] = IVLetter;
                            break;
                    }
                }
            }
            return true;
        }

        private static string SetSpace(object v)
        {
            if (v != null && v.ToString() != "" && (v.ToString()[0] == '\'' || v.ToString()[0] == '’'))
                return " " + v.ToString();
            return v.ToString();
        }

        internal static bool checkGetRawDataArrayArguments(
                    Questions questions, decimal[] questionids
                  , ref OutputDataType datatype
                  , ref string toptablename, ref int columnsCount
                  , out bool[] divisiblePoint, out QCAnswerType[] columnQCAnsType
            , out QCWebException exception, bool isQc4)
        {
            exception = null;
            divisiblePoint = null;
            columnQCAnsType = null;
            OutputDataType dType = OutputDataType.Code;
            switch (datatype)
            {
                case OutputDataType.Flag:
                case OutputDataType.Decode:
                    dType = datatype;
                    break;
                case OutputDataType.QC3:
                    break;
                default:
                    datatype = OutputDataType.Code;
                    break;
            }
            columnsCount = 0;
            toptablename = null;
            for (var i = 0; i < questionids.Length; ++i)
            {
                Questions.Question question = questions[questionids[i]] as Questions.Question;
                if (question == null)
                {
                    divisiblePoint = null;
                    columnQCAnsType = null;

                    return false;
                }
                if (toptablename == null)
                {
                    toptablename = question.TopTableName;
                    if (string.IsNullOrWhiteSpace(toptablename))
                    {
                        return false;
                    }
                }
                int cnt = 1;
                if (dType != OutputDataType.Code && (question.QuestionType & QuestionType.MA) == QuestionType.MA && !isQc4)
                {
                    cnt = question.Sectors.Count;
                }
                if ((question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
                {
                    cnt *= question.ChildQuestions.Count;
                }
                Array.Resize<bool>(ref divisiblePoint, columnsCount += cnt);
                Array.Resize<QCAnswerType>(ref columnQCAnsType, columnsCount);
            }
            return true;
        }
        /// <summary>
        /// Method to write data to different type of files such as CSV, Excel..
        /// </summary>
        /// <param name="rawDataAry">Data Array</param>
        /// <param name="outputPath">Output path</param>
        /// <param name="divFlag">Divided data as bool flags</param>
        /// <param name="usedQuestion">Used Question details list</param>
        /// <param name="outputType">Output File type</param>
        /// <param name="dataContainFlag">reperesents bool flags where the position of the data of divided data</param>
        /// <param name="destPath"></param>
        /// <param name="divQuestion"></param>
        /// <param name="excelApp">QC4 Excel application object refference</param>
        /// <param name="exportType">Export type</param>
        /// <param name="isRawdata">bool value that represents whether this is Raw data</param>
        /// <param name="isLast">bool value represents the last stage of writing</param>
        public void WriteData(string[,] rawDataAry, List<string> outputPath, bool[,] divFlag
            , List<QuestionSettings> usedQuestion, Constants.DataOutput.FileType outputType
            , ref bool[] dataContainFlag, string destPath, QuestionSettings divQuestion, Excel.Application excelApp = null,string exportType="", bool isRawdata = false, bool isLast = false)
        {
            if (rawDataAry.GetLength(0) == 0)
            {
                return;
            }

            switch (outputType)
            {
                case Constants.DataOutput.FileType.CSV:
                case Constants.DataOutput.FileType.TAB:
                case Constants.DataOutput.FileType.QLayout:
                case Constants.DataOutput.FileType.SPSS:
                    string sep = Constants.DataOutput.FileType.CSV == outputType ? "," : "\t";
                    for (int i = 0; i < divFlag.GetLength(1); i++)
                    {
                        if (!dataContainFlag[i]) continue;
                        int maxRow = 0;
                        for (int j = 0; j < rawDataAry.GetLength(0); j++)
                        {
                            if (divFlag[j, i])
                            {
                                maxRow++;
                            }
                        }
                        string[,] result = new string[maxRow, rawDataAry.GetLength(1)];
                        for (int j = 0, r = 0; j < rawDataAry.GetLength(0); j++)
                        {
                            if (!divFlag[j, i]) continue;
                            for (int k = 0, c = 0; k < rawDataAry.GetLength(1); k++)
                            {
                                result[r, c++] = rawDataAry[j, k];
                            }
                            r++;
                        }
                        string[] QAnsTypes = new string[usedQuestion.Count];
                        string otType = Constants.DataOutput.FileType.CSV == outputType ? "CSV" : Constants.DataOutput.FileType.TAB == outputType ? "TAB" : Constants.DataOutput.FileType.QLayout == outputType ? "QLayout": "";
                        if(otType!="")
                        {
                            if (isRawdata && exportType!="CODE")
                            {
                                int rs = 0;
                                QAnsTypes = new string[result.GetLength(1)];
                                for (int u = 0; u < usedQuestion.Count; u++)
                                {
                                    if (usedQuestion[u].AnswerType=="MA" && usedQuestion[u].Choices.Count > 0)
                                    {
                                        for (int h = 0; h < usedQuestion[u].Choices.Count; h++)
                                        {
                                            QAnsTypes[rs] = usedQuestion[u].AnswerType;
                                            rs++;
                                        }
                                    }
                                    else
                                    {
                                        QAnsTypes[rs] = usedQuestion[u].AnswerType;
                                        rs++;
                                    }
                                }
                            }
                            else
                            {
                                for (int u = 0; u < usedQuestion.Count; u++)
                                {
                                    QAnsTypes[u] = usedQuestion[u].AnswerType;
                                }
                            }
                        }

                        SeparatedValuesBase sv = new SeparatedValuesBase();
                        string enc = outputType == Constants.DataOutput.FileType.QLayout || outputType == Constants.DataOutput.FileType.CSV || outputType == Constants.DataOutput.FileType.TAB ? Constants.DataOutput.defaultEncoding : "UTF-8";
                        string[] dataBuffer = sv.GetRawDataBuffer(sep, result,outputType: otType,itemTypeAry: QAnsTypes,exportType: exportType,isRawdata: isRawdata);
                        sv.Export(outputPath[i], dataBuffer[0], enc, outputType );
                    }
                    break;
                case Constants.DataOutput.FileType.QC4:
                    string sql = "INSERT OR IGNORE INTO answers ( sample_id,";
                    string values = "  values( @sample_id,";
                    for (int j = 1; j < usedQuestion.Count; j++)
                    {
                        sql += "q_" + j + ",";
                        values += "@q_" + j + ",";
                    }
                    sql = sql.Remove(sql.Length - 1);
                    values = values.Remove(values.Length - 1);
                    sql += ")";
                    values += ")";
                    for (int i = 0; i < divFlag.GetLength(1); i++)
                    {
                        if (!dataContainFlag[i]) continue;
                        InsertItems(usedQuestion, DB.DBHelper.GetConnectionString(outputPath[i] + "\\" + Constants.TemplateFile.DB_FIlE), divFlag, i, rawDataAry, sql + values);
                    }
                    break;
                case Constants.DataOutput.FileType.Excel2007:
                    bool isNewApp = false;
                    string sheetName = Constants.SheetCodeName.Data01;
                    int rowHeader = Constants.DataSheet.RowHeader;
                    int colStart = Constants.DataSheet.ColStart;
                    Excel.XlCalculation xlCalculation = Excel.XlCalculation.xlCalculationAutomatic;
                    Excel.Workbook workbook = null;
                    try
                    {
                        sheetName = "00";
                        rowHeader = 1;
                        if (ExcelRowStart == null)
                        {
                            ExcelRowStart = new List<int>();
                            for (int i = 0; i < divFlag.GetLength(1); i++)
                            {
                                ExcelRowStart.Add(0);
                            }
                        }

                        if (excelApp == null)
                        {
                            isNewApp = true;
                            excelApp = new Excel.Application();
                        }
                        for (int i = 0; i < divFlag.GetLength(1); i++)
                        {
                            if (!dataContainFlag[i]) continue;
                            excelApp.EnableEvents = false;

                            workbook = ExcelUtil.OpenWorkbok(outputPath[i], excelApp);
                            int sheetCnt = -1;
                            foreach (Excel.Worksheet sht in workbook.Worksheets)
                            {
                                sheetCnt++;
                            }


                            List<Object[,]> rsltAry = new List<object[,]>();
                            int leng = rawDataAry.GetLength(1);
                            int rowLen = 0;
                            for (int h = 0; h < rawDataAry.GetLength(0); h++)
                            {
                                if (!divFlag[h, i]) continue;
                                rowLen++;
                            }
                            for (int j = 0; j < sheetCnt; j++)
                            {
                                Object[,] result = null;
                                if (leng > 16384)
                                {
                                    leng = leng - 16384;
                                    result = new Object[rowLen, 16384];
                                }
                                else
                                    result = new Object[rowLen, leng];
                                rsltAry.Add(result);
                            }
                            int headerSheet = 0;
                            int strtCol = 0;
                            for (int j = 0, r = 0; j < rawDataAry.GetLength(0); j++)
                            {
                                if (!divFlag[j, i]) continue;
                                for (int k = 0, c = 0; k < rawDataAry.GetLength(1); k++)
                                {
                                    if (c > 16383)
                                    {
                                        c = 0;
                                        headerSheet++;
                                    }
                                    rsltAry[headerSheet][r, c++] = rawDataAry[j, k];
                                }
                                headerSheet = 0;
                                r++;
                            }



                            Excel.Worksheet worksheet = null;
                            xlCalculation = workbook.Application.Calculation;
                            int strtSubCol = 0;
                            if(ExcelRowStart[i]==0)
                            {
                                ExcelRowStart[i] = 1;
                            }
                            for (int s = 0; s < sheetCnt; s++)
                            {
                                object[,] result = rsltAry[s];
                                worksheet = ExcelUtil.GetWorkSheetBySheetName(workbook, sheetName + (s + 1).ToString());
                                Excel.Range rangeEnd = worksheet.Cells[ExcelRowStart[i], colStart];
                                workbook.Application.Calculation = Excel.XlCalculation.xlCalculationManual;
                                workbook.Application.ScreenUpdating = false;
                                int col = 1;
                                int maxCl = 0;
                                if (exportType == "Decode")
                                {
                                    for (int u = strtCol; u < usedQuestion.Count; u++)
                                    {
                                        if (col > 16383)
                                            break;
                                        strtCol++;
                                        switch (usedQuestion[u].AnswerType)
                                        {
                                            case Constants.AnswerType.FA:
                                                rangeEnd.Columns[col].EntireColumn.NumberFormat = "@";
                                                break;
                                            case Constants.AnswerType.D:
                                                rangeEnd.Columns[col].EntireColumn.NumberFormat = "yyyy/mm/dd hh:mm:ss";
                                                break;
                                            case Constants.AnswerType.SA:
                                                rangeEnd.Columns[col].EntireColumn.NumberFormat = "@";
                                                break;
                                            case Constants.AnswerType.MA:
                                                for (int j = strtSubCol; j < usedQuestion[u].CategoryCount; j++)
                                                {
                                                    if (col > 16383)
                                                        break;
                                                    rangeEnd.Columns[col].EntireColumn.NumberFormat = "@";
                                                    col++;
                                                    strtSubCol++;
                                                }
                                                col--;
                                                break;
                                        }
                                        col++;
                                    }
                                }
                                else if (exportType == "CODE")
                                {
                                    for (int u = strtCol; u < usedQuestion.Count; u++)
                                    {
                                        if (maxCl > 16383)
                                            break;
                                        strtCol++;
                                        maxCl++;
                                        switch (usedQuestion[u].AnswerType)
                                        {
                                            case Constants.AnswerType.FA:
                                                rangeEnd.Columns[col].EntireColumn.NumberFormat = "@";
                                                break;
                                            case Constants.AnswerType.D:
                                                rangeEnd.Columns[col].EntireColumn.NumberFormat = "yyyy/mm/dd hh:mm:ss";
                                                break;
                                            case Constants.AnswerType.MA:
                                                rangeEnd.Columns[col].EntireColumn.NumberFormat = "@";
                                                break;
                                        }
                                        col++;
                                    }
                                }
                                else
                                {
                                    for (int u = strtCol; u < usedQuestion.Count; u++)
                                    {
                                        if (col > 16383)
                                            break;
                                        strtCol++;
                                        maxCl++;
                                        switch (usedQuestion[u].AnswerType)
                                        {
                                            case Constants.AnswerType.FA:
                                                rangeEnd.Columns[col].EntireColumn.NumberFormat = "@";
                                                break;
                                            case Constants.AnswerType.D:
                                                rangeEnd.Columns[col].EntireColumn.NumberFormat = "yyyy/mm/dd hh:mm:ss";
                                                break;
                                            case Constants.AnswerType.MA:
                                                col += usedQuestion[u].CategoryCount - 1;
                                                break;
                                        }
                                        col++;
                                    }
                                }
                                if (result.GetLength(0) != 0 && result.GetLength(1) != 0)
                                {
                                    int totaldataCnt = result.GetUpperBound(0) * result.GetUpperBound(1);
                                    if (totaldataCnt >= 10000000)
                                    {
                                        Object[,] tmpArray = null;
                                        int lstCol = 0, lstColCnt = 0, flag = 0,
                                            colCount = 0, colOffset = 0,
                                            rowCount = 0, startCol = 0,
                                            colIdx = 0, maxCol = 0;

                                        maxCol = 10000000 / result.GetUpperBound(0);
                                        startCol = result.GetLowerBound(1);
                                        lstCol = result.GetUpperBound(1);
                                        lstColCnt = lstCol;

                                        while (lstColCnt > maxCol)
                                        {
                                            tmpArray = new Object[result.GetLength(0), maxCol];

                                            for (rowCount = result.GetLowerBound(0); rowCount <= result.GetUpperBound(0); rowCount++)
                                            {
                                                colIdx = 0;
                                                for (colCount = startCol; colCount < startCol + maxCol; colCount++, colIdx++)
                                                {
                                                    tmpArray.SetValue(result.GetValue(rowCount, colCount), rowCount, colIdx);
                                                }
                                            }
                                            rangeEnd.Offset[1, colOffset].Resize[result.GetLength(0), maxCol].Value2 = tmpArray;

                                            startCol = colOffset += maxCol;
                                            lstColCnt = lstColCnt - maxCol;
                                            flag++;
                                        }

                                        lstColCnt = lstCol - (flag * maxCol) + 1;
                                        tmpArray = new Object[result.GetLength(0), lstColCnt];
                                        for (rowCount = result.GetLowerBound(0); rowCount <= result.GetUpperBound(0); rowCount++)
                                        {
                                            colIdx = 0;
                                            for (colCount = startCol; colCount < startCol + lstColCnt; colCount++, colIdx++)
                                            {
                                                tmpArray.SetValue(result.GetValue(rowCount, colCount), rowCount, colIdx);
                                            }
                                        }
                                        rangeEnd.Offset[1, colOffset].Resize[result.GetLength(0), lstColCnt].Value2 = tmpArray;
                                    }
                                    else
                                    {
                                        rangeEnd.Offset[1, 0].Resize[result.GetLength(0), result.GetLength(1)].Value2 = result;
                                    }
                                }
                            }
                            ExcelRowStart[i] += rsltAry[0].GetLength(0);
                            workbook.Application.Calculation = xlCalculation;
                            workbook.Application.ScreenUpdating = true;
                            foreach (Excel.Worksheet shet in workbook.Worksheets)
                            {
                                shet.Select();
                                Excel.Range ExcelRange = shet.get_Range("A1");
                                ExcelRange.Select();
                            }
                            worksheet = ExcelUtil.GetWorkSheetBySheetName(workbook, "Layout");
                            worksheet.Activate();

                            workbook.Save();
                            workbook.Application.EnableEvents = true;
                            workbook.Close();
                        }
                    }
                    catch(Exception ex)
                    {
                        if (ex.GetType().IsAssignableFrom(typeof(System.OutOfMemoryException)))
                        {
                            EXMessage = LocalResource.OUTOFMEMORY_EXCEPTION_MESSAGE;
                            _log.LogError(LocalResource.OUTOFMEMORY_EXCEPTION_MESSAGE + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                        }
                        else
                            _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                        if (workbook != null)
                        {
                            workbook.Application.Calculation = xlCalculation;
                            workbook.Application.ScreenUpdating = true;
                            workbook.Application.EnableEvents = true;
                            workbook.Close();
                        }
                    }
                    finally
                    {
                        if (isNewApp)
                        {
                            excelApp.Quit();
                        }
                    }
                    break;
                case Constants.DataOutput.FileType.QC3:
                    sheetName = Constants.SheetCodeName.Data01;
                    rowHeader = Constants.DataSheet.RowHeader;
                    colStart = Constants.DataSheet.ColStart;
                    Excel.Worksheet Data01Sheet = null;
                    Excel.Workbook workbook1 = null;
                    Excel.Worksheet Data02Sheet = null;

                    isNewApp = false;
                    if (ExcelRowStart == null)
                    {
                        ExcelRowStart = new List<int>();
                        for (int i = 0; i < divFlag.GetLength(1); i++)
                        {
                            ExcelRowStart.Add(0);
                        }
                    }
                    if (excelApp == null)
                    {
                        isNewApp = true;
                        excelApp = new Excel.Application();
                    }
                    if (usedQuestion.Count > QC4Common.Common.Constants.ExcelRowColumnMax.ExcelMaxCol)
                    {
                        for (int i = 0; i < divFlag.GetLength(1); i++)
                        {
                            if (ExcelRowStart[i] == 0)
                                ExcelRowStart[i] = 4;
                            if (!dataContainFlag[i]) continue;
                            excelApp.EnableEvents = false;

                            workbook1 = ExcelUtil.OpenWorkbok(outputPath[i], excelApp);
                            workbook1.Unprotect(Constants.Password);
                            Data01Sheet = ExcelUtil.GetWorkSheetByCodeName(workbook1, sheetName);
                            Excel.Range range = Data01Sheet.Cells[rowHeader, colStart];
                            Excel.Range rangeEnd = ExcelUtil.EndxlUp(range);
                            if (rangeEnd.Row < Constants.DataSheet.RowStart)
                            {
                                rangeEnd = range;
                            }
                            int leng = rawDataAry.GetLength(1);
                            if (leng > 16382)
                                leng = 16383;
                            Object[,] result = new Object[rawDataAry.GetLength(0), leng];
                            for (int j = 0, r = 0; j < rawDataAry.GetLength(0); j++)
                            {
                                if (!divFlag[j, i]) continue;
                                for (int k = 0, c = 0; k < rawDataAry.GetLength(1); k++)
                                {
                                    if (c > 16382)
                                        continue;
                                    result[r, c++] = rawDataAry[j, k];
                                }
                                r++;
                            }
                            xlCalculation = workbook1.Application.Calculation;
                            workbook1.Application.Calculation = Excel.XlCalculation.xlCalculationManual;
                            workbook1.Application.ScreenUpdating = false;
                            int col = 1;
                            for (int u = 0; u < usedQuestion.Count; u++)
                            {
                                if (u > 16382)
                                    break;
                                switch (usedQuestion[u].AnswerType)
                                {
                                    case Constants.AnswerType.FA:
                                        rangeEnd.Columns[col].EntireColumn.NumberFormat = "@";
                                        break;
                                    case Constants.AnswerType.D:
                                        rangeEnd.Columns[col].EntireColumn.NumberFormat = "yyyy/mm/dd hh:mm:ss";
                                        break;
                                }
                                col++;
                            }
                            if (result.GetLength(0) != 0 && result.GetLength(1) != 0)
                            {
                                Data01Sheet.Cells[ExcelRowStart[i], Constants.DataSheet.ColHeader].Resize[result.GetLength(0), result.GetLength(1)].Value2 = result;
                            }


                            Data02Sheet = ExcelUtil.GetWorkSheetBySheetName(workbook1, "Data02");
                            range = Data01Sheet.Cells[rowHeader, colStart];
                            rangeEnd = ExcelUtil.EndxlUp(range);
                            if (rangeEnd.Row < Constants.DataSheet.RowStart)
                            {
                                rangeEnd = range;
                            }
                            int data02ColCount = (usedQuestion.Count - QC4Common.Common.Constants.ExcelRowColumnMax.ExcelMaxCol) + 1;
                            result = new Object[rawDataAry.GetLength(0), data02ColCount];
                            for (int j = 0; j < rawDataAry.GetLength(0); j++)
                            {
                                if (!divFlag[j, i]) continue;
                                for (int k = 0, c = QC4Common.Common.Constants.ExcelRowColumnMax.ExcelMaxCol; k <= usedQuestion.Count - QC4Common.Common.Constants.ExcelRowColumnMax.ExcelMaxCol; k++)
                                {
                                    if (k == 0)
                                    {
                                        result[j, k] = rawDataAry[j, k];
                                    }
                                    else
                                    {
                                        result[j, k] = rawDataAry[j, c];
                                        c++;
                                    }
                                }
                            }
                            col = 1;
                            for (int u = 0; u < data02ColCount; u++)
                            {
                                if (u > 16382)
                                    break;
                                switch (usedQuestion[u].AnswerType)
                                {
                                    case Constants.AnswerType.FA:
                                        rangeEnd.Columns[col].EntireColumn.NumberFormat = "@";
                                        break;
                                    case Constants.AnswerType.D:
                                        rangeEnd.Columns[col].EntireColumn.NumberFormat = "yyyy/mm/dd hh:mm:ss";
                                        break;
                                }
                                col++;
                            }
                            if (result.GetLength(0) != 0 && result.GetLength(1) != 0)
                            {
                                Data02Sheet.Cells[ExcelRowStart[i], Constants.DataSheet.ColHeader].Resize[result.GetLength(0), result.GetLength(1)].Value2 = result;
                                Excel.Range dhE = Data02Sheet.Range["A3"].Offset[0, result.GetLength(1)];
                                if (dhE.Column < 8)
                                {
                                    dhE = Data02Sheet.Range["I3"];
                                }
                                Excel.Range dhEndMost = dhE.End[Excel.XlDirection.xlToRight];
                                Data02Sheet.get_Range(dhE, dhEndMost).EntireColumn.Hidden = true;
                            }
                            ExcelRowStart[i] += rawDataAry.GetLength(0);
                            if (isLast)
                            {
                                Excel.Worksheet QsSSht = null;
                                foreach (Excel.Worksheet shtt in workbook1.Worksheets)
                                {
                                    if (shtt.CodeName != Constants.SheetCodeName.QuestionSetting)
                                    {
                                        shtt.Protect(Constants.Password);
                                        shtt.Visible = Excel.XlSheetVisibility.xlSheetVeryHidden;
                                    }
                                    else
                                    {
                                        QsSSht = shtt;
                                    }
                                }

                                QsSSht.Select();
                                QsSSht.Protect(Constants.Password);
                                workbook1.Password = Constants.Password;
                                workbook1.Protect(Constants.Password);
                                workbook1.Application.Calculation = Excel.XlCalculation.xlCalculationAutomatic;
                                workbook1.Application.ScreenUpdating = true;
                                workbook1.Application.EnableEvents = true;
                                workbook1.Save();
                                workbook1.Close();
                            }
                            else
                            {
                                workbook1.Password = Constants.Password;
                                workbook1.Protect(Constants.Password);
                                workbook1.Application.Calculation = Excel.XlCalculation.xlCalculationAutomatic;
                                workbook1.Application.ScreenUpdating = true;
                                workbook1.Application.EnableEvents = true;
                                workbook1.Save();
                                workbook1.Close();
                            }
                        }
                    }
                    else
                    {
                        Object[,] result = null;
                        for (int i = 0; i < divFlag.GetLength(1); i++)
                        {
                            if (!dataContainFlag[i]) continue;
                            excelApp.EnableEvents = false;

                            workbook1 = ExcelUtil.OpenWorkbok(outputPath[i], excelApp);
                            if (outputType == Constants.DataOutput.FileType.QC3)
                            {
                                workbook1.Unprotect(Constants.Password);
                            }

                            Excel.Worksheet worksheet = ExcelUtil.GetWorkSheetByCodeName(workbook1, sheetName);
                            Excel.Range range = worksheet.Cells[rowHeader, colStart];
                            Excel.Range rangeEnd = ExcelUtil.EndxlUp(range);
                            if (rangeEnd.Row < Constants.DataSheet.RowStart)
                            {
                                rangeEnd = range;
                            }
                            int leng = rawDataAry.GetLength(1);
                            if (leng > 16382)
                                leng = 16383;
                            result = new Object[rawDataAry.GetLength(0), leng];
                            for (int j = 0, r = 0; j < rawDataAry.GetLength(0); j++)
                            {
                                if (!divFlag[j, i]) continue;
                                for (int k = 0, c = 0; k < rawDataAry.GetLength(1); k++)
                                {
                                    if (c > 16382)
                                        continue;
                                    result[r, c++] = rawDataAry[j, k];
                                }
                                r++;
                            }

                            xlCalculation = workbook1.Application.Calculation;
                            workbook1.Application.Calculation = Excel.XlCalculation.xlCalculationManual;
                            workbook1.Application.ScreenUpdating = false;
                            int col = 1;
                            for (int u = 0; u < usedQuestion.Count; u++)
                            {
                                if (u > 16382)
                                    break;
                                switch (usedQuestion[u].AnswerType)
                                {
                                    case Constants.AnswerType.FA:
                                        rangeEnd.Columns[col].EntireColumn.NumberFormat = "@";
                                        break;
                                    case Constants.AnswerType.D:
                                        rangeEnd.Columns[col].EntireColumn.NumberFormat = "yyyy/mm/dd hh:mm:ss";
                                        break;
                                }
                                col++;
                            }
                            if (result.GetLength(0) != 0 && result.GetLength(1) != 0)
                            {
                                rangeEnd.Offset[1, 0].Resize[result.GetLength(0), result.GetLength(1)].Value2 = result;
                            }

                            if (isLast)
                            {
                                Excel.Worksheet QsSSht = null;
                                foreach (Excel.Worksheet shtt in workbook1.Worksheets)
                                {
                                    if (shtt.CodeName != Constants.SheetCodeName.QuestionSetting)
                                    {
                                        shtt.Protect(Constants.Password);
                                        shtt.Visible = Excel.XlSheetVisibility.xlSheetVeryHidden;
                                    }
                                    else
                                    {
                                        QsSSht = shtt;
                                    }
                                }

                                QsSSht.Select();
                                QsSSht.Protect(Constants.Password);
                                workbook1.Password = Constants.Password;
                                workbook1.Protect(Constants.Password);
                                workbook1.Application.Calculation = Excel.XlCalculation.xlCalculationAutomatic;
                                workbook1.Application.ScreenUpdating = true;
                                workbook1.Application.EnableEvents = true;
                                workbook1.Save();
                                workbook1.Close();
                            }
                            else
                            {
                                workbook1.Password = Constants.Password;
                                workbook1.Protect(Constants.Password);
                                workbook1.Application.Calculation = Excel.XlCalculation.xlCalculationAutomatic;
                                workbook1.Application.ScreenUpdating = true;
                                workbook1.Application.EnableEvents = true;
                                workbook1.Save();
                                workbook1.Close();
                            }
                        }
                    }

                    if (isNewApp)
                    {
                        excelApp.Quit();
                    }
                    break;
                case Constants.DataOutput.FileType.R2D3:
                    break;
                default: break;
            }
        }

        private void AlertAndRemoveFiles(bool[] dataContainFlag, List<string> outputPath, QuestionSettings divQuestion
            , string destPath, Constants.DataOutput.FileType fileType, string[] layoutOutputPath = null)
        {
            if (Constants.DataOutput.FileType.QC4 == fileType && divQuestion == null)
            {
                QcFileHelper.ArchiveFiles(outputPath[0], destPath + Constants.Qc4Extension);
                Directory.Delete(outputPath[0], true);
            }

            switch (fileType)
            {
                case Constants.DataOutput.FileType.QC4:
                    if (divQuestion == null)
                    {
                        break;
                    }
                    destPath = destPath + "_" + divQuestion.Variable + "_";
                    for (int i = 0; i < outputPath.Count; i++)
                    {
                        if (dataContainFlag[i])
                        {
                            QcFileHelper.ArchiveFiles(outputPath[i], destPath + (i + 1).ToString("0000") + Constants.Qc4Extension);
                        }
                        Directory.Delete(outputPath[i], true);
                    }
                    if (File.Exists(outputPath[0]))
                    {
                        File.Delete(outputPath[0]);
                    }
                    break;
                case Constants.DataOutput.FileType.QC3:
                    destPath = outputPath[0].Remove(outputPath[0].Count() - 9, 9);
                    for (int i = 0; i < outputPath.Count; i++)
                    {
                        if (!dataContainFlag[i])
                        {
                            File.Delete(destPath + (i + 1).ToString("0000") + ".qc3x");
                        }
                    }
                    break;
                case Constants.DataOutput.FileType.CSV:
                case Constants.DataOutput.FileType.TAB:
                    for (int i = 0; i < outputPath.Count; i++)
                    {
                        if (!dataContainFlag[i])
                        {
                            File.Delete(outputPath[i]);
                            File.Delete(layoutOutputPath[i]);
                        }
                    }
                    break;
                case Constants.DataOutput.FileType.R2D3:
                    for (int i = 0; i < outputPath.Count; i++)
                    {
                        if (!dataContainFlag[i])
                        {
                            File.Delete(layoutOutputPath[i]);
                        }
                        File.Delete(outputPath[i]);
                    }
                    break;
                case Constants.DataOutput.FileType.Excel2007:
                    for (int i = 0; i < outputPath.Count; i++)
                    {
                        if (!dataContainFlag[i])
                        {
                            File.Delete(outputPath[i]);
                        }
                    }
                    break;
                case Constants.DataOutput.FileType.SPSS:
                    for (int i = 1; i < layoutOutputPath.GetLength(0); i++)
                    {

                        if (dataContainFlag[i])
                        {
                            File.Copy(layoutOutputPath[0], layoutOutputPath[i]);
                        }
                        else
                        {
                            File.Delete(outputPath[i]);
                        }
                    }
                    if (!dataContainFlag[0])
                    {
                        File.Delete(layoutOutputPath[0]);
                        File.Delete(outputPath[0]);
                    }
                    break;
                case Constants.DataOutput.FileType.QLayout:
                    SeparatedValuesBase sb = new SeparatedValuesBase();
                    sb.Export(outputPath[0], "FILEEND\r\n", encoding: Constants.DataOutput.defaultEncoding, typ: Constants.DataOutput.FileType.QLayout);
                    for (int i = 1; i < layoutOutputPath.GetLength(0); i++)
                    {

                        if (dataContainFlag[i])
                        {
                            File.Copy(layoutOutputPath[0], layoutOutputPath[i]);
                            sb.Export(outputPath[i], "FILEEND\r\n", encoding: Constants.DataOutput.defaultEncoding, typ: Constants.DataOutput.FileType.QLayout);
                        }
                        else
                        {
                            File.Delete(outputPath[i]);
                        }
                    }
                    if (!dataContainFlag[0])
                    {
                        File.Delete(layoutOutputPath[0]);
                        File.Delete(outputPath[0]);
                    }
                    break;
            }

            string message = "";
            for (int i = 0; i < dataContainFlag.GetLength(0); i++)
            {
                if (!dataContainFlag[i])
                {
                    message += "," + (i + 1);
                }
            }
            progressBar.UpdateProgressBar(96, "");
            progressBar.UpdateProgressBar(97, "");
            progressBar.UpdateProgressBar(98, "");
            progressBar.UpdateProgressBar(99, "");


            if (!"".Equals(message))
            {
                if (divQuestion != null)
                {
                    progressBar.UpdateProgressBar(100, LocalResource.EX_PROGRESSBAR_SUCCESS);
                    WMessage = string.Format(LocalResource.DO_DIV_WARNING, divQuestion.Variable, message.Remove(0, 1));
                }
                else
                {
                    if (File.Exists(destPath + Constants.Qc4Extension))
                    {
                        File.Delete(destPath + Constants.Qc4Extension);
                    }

                    if (File.Exists(outputPath[0]))
                    {
                        File.Delete(outputPath[0]);
                    }
                    DataOutput.isSuccess = false;
                    progressBar.UpdateProgressBar(100, LocalResource.PB_SD_FAILED, true);
                    MessageDialog.ShowMessageOnWorkBook(LocalResource.DO_NO_DATA_ALERT, Enums.MessageType.ErrorOk, window);
                    progressBar.UpdateProgressBar(100, LocalResource.PB_SD_FAILED);
                }
            }
            else
            {
                progressBar.UpdateProgressBar(100, LocalResource.EX_PROGRESSBAR_SUCCESS);
                if (EXMessage == "")
                    MessageDialog.Info(LocalResource.EX_EXPORT_SUCCESS, mainWindow);
            }
        }

        internal bool putDataQrowdata(Questions.Question question, OutputDataType datatype
                          , int columnsCount, string dirpath, string NALetter, string IVLetter
                          , ref int dataCount, ref int allDataCount, ref string[,] puttoarray, ref int clmIdx
                          , out QCWebException exception, ref bool[] filteringFlag, ref long lastValue
                          , string conString, int maxLimit,string questionFlag, SQLiteConnection con = null, bool isQc4 = true)
        {
            exception = null;
            if ((question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
            {
                foreach (DictionaryEntry de in question.ChildQuestions)
                {
                    Questions.Question childQ = de.Value as Questions.Question;
                    if (!putDataQrowdata(childQ, datatype, columnsCount, dirpath, NALetter, IVLetter
                            , ref dataCount, ref allDataCount, ref puttoarray, ref clmIdx, out exception, ref filteringFlag, ref lastValue, conString, maxLimit, questionFlag, con, isQc4))
                    {
                        return false;
                    }
                }
                return true;
            }
            QuestionType qType = question.QuestionType & (QuestionType.SA | QuestionType.MA | QuestionType.N | QuestionType.FA);
            if ((int)qType == 0)
            {
                return false;
            }
            List<Data> data = null;
            try
            {
                string sql = "";
                if (questionFlag == "An")
                {
                    sql = "SELECT m.`" + question.ColumnName + "` FROM multivariate m join " + tableName + " a on m.sort_no = a.sort_no  WHERE m.sort_no > " + lastValue + " ORDER BY m.sort_no LIMIT " + maxLimit;
                }
                else
                    sql = "SELECT " + question.ColumnName + " FROM " + tableName + " WHERE sort_no > " + lastValue + " ORDER BY sort_no LIMIT " + maxLimit;
                var dtReadTable1 = DB.DBHelper.GetDataTable(sql, con);
                data = ReadTextFile.ReadDataTable(dtReadTable1, qType, null, out exception);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

            if (data == null) return false;
            if (clmIdx == -1)
            {
                allDataCount = data.Count;
                int l = filteringFlag == null ? 0 : filteringFlag.Length;
                if (filteringFlag == null)
                {
                    filteringFlag = new bool[allDataCount];
                }
                if (l != allDataCount)
                {
                    Array.Resize<bool>(ref filteringFlag, allDataCount);
                    if (l < allDataCount) Macromill.QCWeb.Common.OperateArray.InitializeWith<bool>(ref filteringFlag, true, l, allDataCount - 1);
                }
                dataCount = 1;
                for (int i = 0; i < allDataCount; ++i)
                {
                    if (!data[i].IsDeleted && filteringFlag[i]) ++dataCount;
                }
                puttoarray = new string[--dataCount, columnsCount];
            }
            else if (data.Count != allDataCount)
            {
                return false;
            }
            string[] sectorDecArray = null;
            if (qType == QuestionType.SA || qType == QuestionType.MA)
            {
                string fmt = new string('0', (int)Math.Floor(Math.Log10(question.Sectors.Count)) + 1);
                if (datatype == OutputDataType.Decode)
                {
                    sectorDecArray = new string[question.Sectors.Count];
                    for (int i = 0; i < question.Sectors.Count; ++i)
                    {
                        sectorDecArray[i] = (i + 1).ToString(fmt) + ":" + question.Sectors[i + 1].Description;
                    }
                }
            }
            if (qType == QuestionType.MA && datatype != OutputDataType.Code && datatype != OutputDataType.QC3)
            {
                int startClmIdx = clmIdx;
                int rowIdx = -1;
                for (int i = 0; i < data.Count; ++i)
                {
                    if (data[i].IsDeleted || !filteringFlag[i]) continue;
                    ++rowIdx;
                    clmIdx = startClmIdx;
                    switch (data[i].DataType)
                    {
                        case DataType.NormalData:
                        case DataType.NAData:
                            bool isNA = true;
                            if (data[i].DataType == DataType.NormalData)
                            {
                                for (int j = 0; j < question.Sectors.Count; ++j)
                                {
                                    int idx = j / GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                                    int e = j % GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                                    if (((data[i] as MAData).Value(idx) & (int)Math.Pow(2.0, (double)e)) != 0)
                                    {
                                        puttoarray[rowIdx, ++clmIdx] = datatype == OutputDataType.Flag ? "1" : sectorDecArray[j];
                                        isNA = false;
                                    }
                                    else
                                    {
                                        puttoarray[rowIdx, ++clmIdx] = datatype == OutputDataType.Flag ? "0" : null;
                                    }
                                }
                            }
                            if (isNA)
                            {
                                for (int j = 0; j < question.Sectors.Count; ++j)
                                {
                                    puttoarray[rowIdx, ++clmIdx] = NALetter;
                                }
                            }
                            break;
                        case DataType.IVData:
                            for (int j = 0; j < question.Sectors.Count; ++j)
                            {
                                puttoarray[rowIdx, ++clmIdx] = IVLetter;
                            }
                            break;
                    }
                }
            }
            else
            {
                ++clmIdx;
                int rowIdx = -1;
                for (int i = 0; i < data.Count; ++i)
                {
                    if (data[i].IsDeleted || !filteringFlag[i]) continue;
                    ++rowIdx;
                    switch (data[i].DataType)
                    {
                        case DataType.NormalData:
                            switch (qType)
                            {
                                case QuestionType.SA:
                                    if (datatype == OutputDataType.Decode)
                                    {
                                        puttoarray[rowIdx, clmIdx] = sectorDecArray[(data[i] as SAData).Value - 1];
                                    }
                                    else
                                    {
                                        puttoarray[rowIdx, clmIdx] = (data[i] as SAData).Value.ToString();
                                    }
                                    break;
                                case QuestionType.MA:

                                    if (isQc4)
                                    {
                                        string d = "";
                                        for (int j = question.Sectors.Count - 1; j >= 0; --j)
                                        {
                                            int idx = j / GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                                            int e = j % GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                                            if (((data[i] as MAData).Value(idx) & (int)Math.Pow(2.0, (double)e)) != 0)
                                            {
                                                d += "1";
                                            }
                                            else
                                            {
                                                d += "0";
                                            }
                                        }
                                        puttoarray[rowIdx, clmIdx] = d;
                                    }
                                    else
                                    {
                                        string maData = (data[i] as MAData).CodeValue;
                                        maData = maData.IndexOf(',') != 0 ? ',' + maData : maData;
                                        maData = maData.LastIndexOf(',') != maData.Length - 1 ? maData + ',' : maData;
                                        puttoarray[rowIdx, clmIdx] = maData;
                                    }
                                    break;
                                case QuestionType.FA:
                                    puttoarray[rowIdx, clmIdx] = (data[i] as FAData).Value.Replace("\t","<TAB>");
                                    break;
                                case QuestionType.N:
                                    puttoarray[rowIdx, clmIdx] = getConveted_Exponent_Value((data[i] as NData).Value.ToString());
                                    break;
                            }
                            break;
                        case DataType.NAData:
                            puttoarray[rowIdx, clmIdx] = NALetter;
                            break;
                        case DataType.IVData:
                            puttoarray[rowIdx, clmIdx] = IVLetter;
                            break;
                    }
                }
            }
            return true;
        }
        bool IsPro = false;
        private void UpdateDetailSetting(Excel.Workbook sourceWorkBook, Excel.Workbook targetWorkbook, List<QuestionSettings> usedQuestions)
        {
            Excel.Worksheet sourceAdvSetting = ExcelUtil.GetWorkSheetByCodeName(sourceWorkBook, Constants.SheetCodeName.DetailsSetting);
            Excel.Range start = sourceAdvSetting.Range[Constants.AdvancedSetting.AdvSettingStartCell];
            Excel.Range end = ExcelUtil.EndxlUp(start);
            if (start.Row > end.Row)
            {
                return;
            }

            Excel.Range sourceTotal = sourceAdvSetting.get_Range(start, end);

            Excel.Worksheet targetAdvSetting = ExcelUtil.GetWorkSheetByCodeName(targetWorkbook, Constants.SheetCodeName.DetailsSetting);
            start = targetAdvSetting.Range[Constants.AdvancedSetting.AdvSettingStartCell];
            end = ExcelUtil.EndxlUp(start);
            Excel.Range targetTotal = targetAdvSetting.get_Range(start, end);
            if (end.Row < 2)
            {
                end = end.Worksheet.Cells[2, 1];
            }

            IsPro = CheckIsPro(sourceTotal, usedQuestions);

            SetSettingValue(sourceTotal, targetTotal, ref end, "F_Cr_Cross_AddUp_Check_Summary_WeightBack_P", "F_Cr_Cross_AddUp_Check_Summary_WeightBack_S");


            SetWbComboVariable(sourceTotal, targetTotal, usedQuestions, ref end, "F_Cr_Cross_AddUp_Combo_Summary_WeightBack_P", "F_Cr_Cross_AddUp_Combo_Summary_WeightBack_S");
        }

        private bool CheckIsPro(Excel.Range sRange, List<QuestionSettings> usedQuestions)
        {
            Excel.Range wbCrCheckS = sRange.Find("F_Cr_Cross_AddUp_Check_Summary_WeightBack_P", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
            if (null != wbCrCheckS && wbCrCheckS.Offset[0, 1].Value != null && wbCrCheckS.Offset[0, 1].Value.ToString().ToLower() == "true")
            {
                return true;
            }
            wbCrCheckS = sRange.Find("F_Cr_Cross_AddUp_Combo_Summary_WeightBack_P", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                   Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
            string variable = "";
            if (wbCrCheckS != null && wbCrCheckS.Offset[0, 1].Value != null && (variable = wbCrCheckS.Offset[0, 1].Text) != "" &&
                (variable.Equals("WeightBack") || usedQuestions.Where(q => q.Variable == variable).Count() > 0))
            {
                return true;
            }
            wbCrCheckS = sRange.Find("F_Cr_Cross_AddUp_OutputUnweightbackedTotalCheck_P", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
            if (null != wbCrCheckS && wbCrCheckS.Offset[0, 1].Value !=null&& wbCrCheckS.Offset[0, 1].Value.ToString().ToLower() == "false")
            {
                return true;
            }
            wbCrCheckS = sRange.Find("F_Cr_Cross_AddUp_UnweightbackedBaseCheck_P", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
            if (null != wbCrCheckS && wbCrCheckS.Offset[0, 1].Value != null && wbCrCheckS.Offset[0, 1].Value.ToString().ToLower() == "false")
            {
                return true;
            }
            wbCrCheckS = sRange.Find("F_Gt_GT_AddUp_OutputUnweightbackedTotalCheck_P", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
            if (null != wbCrCheckS && wbCrCheckS.Offset[0, 1].Value != null && wbCrCheckS.Offset[0, 1].Value.ToString().ToLower() == "false")
            {
                return true;
            }
            return false;
        }

        private void SetSettingValue(Excel.Range sRange, Excel.Range tRange, ref Excel.Range tEnd, string key, string key2)
        {
            Excel.Range wbCrCheckT = tRange.Find(key, Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
            Excel.Range wbCrCheckT2 = tRange.Find(key2, Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
            if (IsPro)
            {
                Excel.Range wbCrCheckS = sRange.Find(key, Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                        Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
                if (wbCrCheckT == null)
                {
                    tEnd = tEnd.Offset[1, 0];
                    tEnd.Value = key;
                    tEnd.Next.Value = wbCrCheckS.Offset[0, 1].Value;
                }
                else
                {
                    wbCrCheckT.Offset[0, 1].Value = wbCrCheckS.Offset[0, 1].Value;
                }
                if (wbCrCheckT2 == null)
                {
                    tEnd = tEnd.Offset[1, 0];
                    tEnd.Value = key;
                    tEnd.Next.Value = wbCrCheckS.Offset[0, 1].Value;
                }
                else
                {
                    wbCrCheckT2.Offset[0, 1].Value = wbCrCheckS.Offset[0, 1].Value;
                }
            }
            else
            {
                Excel.Range wbCrCheckS2 = sRange.Find(key2, Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                        Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
                string flag = wbCrCheckS2 == null ? null : wbCrCheckS2.Offset[0, 1].Value;
                if (wbCrCheckT == null)
                {
                    tEnd = tEnd.Offset[1, 0];
                    tEnd.Value = key;
                    tEnd.Next.Value = flag;
                }
                else
                {
                    wbCrCheckT.Offset[0, 1].Value = flag;
                }
                if (wbCrCheckT2 == null)
                {
                    tEnd = tEnd.Offset[1, 0];
                    tEnd.Value = key;
                    tEnd.Next.Value = flag;
                }
                else
                {
                    wbCrCheckT2.Offset[0, 1].Value = flag;
                }
            }
        }

        private void SetWbComboVariable(Excel.Range sourceTotal, Excel.Range targetTotal, List<QuestionSettings> usedQuestions, ref Excel.Range end, string key, string key2)
        {
            string variable = "";
            Excel.Range wbCrComboT = targetTotal.Find(key, Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                            Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
            Excel.Range wbCrComboT2 = targetTotal.Find(key2, Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
            if (IsPro)
            {
                Excel.Range wbCrComboS = sourceTotal.Find(key, Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                        Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
                variable = wbCrComboS.Offset[0, 1].Text;
                if (wbCrComboT != null)
                {
                    wbCrComboT.Offset[0, 1].Value = variable;
                }
                else
                {
                    end = end.Offset[1, 0];
                    end.Value = key;
                    end.Next.Value = variable;
                }
                if (wbCrComboT2 != null)
                {
                    wbCrComboT2.Offset[0, 1].Value = variable;
                }
                else
                {
                    end = end.Offset[1, 0];
                    end.Value = key;
                    end.Next.Value = variable;
                }
                Excel.Range wbCrCheckS = sourceTotal.Find("F_Cr_Cross_AddUp_OutputUnweightbackedTotalCheck_P", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                            Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
                Excel.Range wbCrCheckT = targetTotal.Find("F_Cr_Cross_AddUp_OutputUnweightbackedTotalCheck_P", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
                Excel.Range wbCrCheckT2 = targetTotal.Find("F_Cr_Cross_AddUp_OutputUnweightbackedTotalCheck_S", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
                if (null != wbCrCheckS)
                {
                    if (wbCrCheckT == null)
                    {
                        end = end.Offset[1, 0];
                        end.Value = "F_Cr_Cross_AddUp_OutputUnweightbackedTotalCheck_P";
                        end.Next.Value = wbCrCheckS.Offset[0, 1].Value;
                    }
                    else
                    {
                        wbCrCheckT.Offset[0, 1].Value = wbCrCheckS.Offset[0, 1].Value;
                    }
                    if (wbCrCheckT2 == null)
                    {
                        end = end.Offset[1, 0];
                        end.Value = "F_Cr_Cross_AddUp_OutputUnweightbackedTotalCheck_S";
                        end.Next.Value = wbCrCheckS.Offset[0, 1].Value;
                    }
                    else
                    {
                        wbCrCheckT2.Offset[0, 1].Value = wbCrCheckS.Offset[0, 1].Value;
                    }
                }
                wbCrCheckS = sourceTotal.Find("F_Cr_Cross_AddUp_UnweightbackedBaseCheck_P", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                        Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
                wbCrCheckT = targetTotal.Find("F_Cr_Cross_AddUp_UnweightbackedBaseCheck_P", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
                wbCrCheckT2 = targetTotal.Find("F_Cr_Cross_AddUp_UnweightbackedBaseCheck_S", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
                if (null != wbCrCheckS)
                {
                    if (wbCrCheckT == null)
                    {
                        end = end.Offset[1, 0];
                        end.Value = "F_Cr_Cross_AddUp_UnweightbackedBaseCheck_P";
                        end.Next.Value = wbCrCheckS.Offset[0, 1].Value;
                    }
                    else
                    {
                        wbCrCheckT.Offset[0, 1].Value = wbCrCheckS.Offset[0, 1].Value;
                    }
                    if (wbCrCheckT2 == null)
                    {
                        end = end.Offset[1, 0];
                        end.Value = "F_Cr_Cross_AddUp_UnweightbackedBaseCheck_S";
                        end.Next.Value = wbCrCheckS.Offset[0, 1].Value;
                    }
                    else
                    {
                        wbCrCheckT2.Offset[0, 1].Value = wbCrCheckS.Offset[0, 1].Value;
                    }
                }
                wbCrCheckS = sourceTotal.Find("F_Gt_GT_AddUp_OutputUnweightbackedTotalCheck_P", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                        Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
                wbCrCheckT = targetTotal.Find("F_Gt_GT_AddUp_OutputUnweightbackedTotalCheck_P", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
                wbCrCheckT2 = targetTotal.Find("F_Gt_GT_AddUp_Check_Output_Sort_S", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
                if (null != wbCrCheckS)
                {
                    if (wbCrCheckT == null)
                    {
                        end = end.Offset[1, 0];
                        end.Value = "F_Gt_GT_AddUp_OutputUnweightbackedTotalCheck_P";
                        end.Next.Value = wbCrCheckS.Offset[0, 1].Value;
                    }
                    else
                    {
                        wbCrCheckT.Offset[0, 1].Value = wbCrCheckS.Offset[0, 1].Value;
                    }
                    if (wbCrCheckT2 == null)
                    {
                        end = end.Offset[1, 0];
                        end.Value = "F_Gt_GT_AddUp_Check_Output_Sort_S";
                        end.Next.Value = wbCrCheckS.Offset[0, 1].Value;
                    }
                    else
                    {
                        wbCrCheckT2.Offset[0, 1].Value = wbCrCheckS.Offset[0, 1].Value;
                    }
                }
            }
            else
            {
                Excel.Range wbCrComboS2 = sourceTotal.Find(key2, Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                        Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
                variable = wbCrComboS2 == null ? "" : wbCrComboS2.Offset[0, 1].Text;
                if (wbCrComboT != null)
                {
                    wbCrComboT.Offset[0, 1].Value = variable;
                }
                else
                {
                    end = end.Offset[1, 0];
                    end.Value = key;
                    end.Next.Value = variable;
                }
                if (wbCrComboT2 != null)
                {
                    wbCrComboT2.Offset[0, 1].Value = variable;
                }
                else
                {
                    end = end.Offset[1, 0];
                    end.Value = key;
                    end.Next.Value = variable;
                }

                Excel.Range wbCrCheckS = sourceTotal.Find("F_Cr_Cross_AddUp_OutputUnweightbackedTotalCheck_S", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                        Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
                Excel.Range wbCrCheckT = targetTotal.Find("F_Cr_Cross_AddUp_OutputUnweightbackedTotalCheck_P", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
                Excel.Range wbCrCheckT2 = targetTotal.Find("F_Cr_Cross_AddUp_OutputUnweightbackedTotalCheck_S", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
                if (null != wbCrCheckS)
                {
                    if (wbCrCheckT == null)
                    {
                        end = end.Offset[1, 0];
                        end.Value = "F_Cr_Cross_AddUp_OutputUnweightbackedTotalCheck_P";
                        end.Next.Value = wbCrCheckS.Offset[0, 1].Value;
                    }
                    else
                    {
                        wbCrCheckT.Offset[0, 1].Value = wbCrCheckS.Offset[0, 1].Value;
                    }
                    if (wbCrCheckT2 == null)
                    {
                        end = end.Offset[1, 0];
                        end.Value = "F_Cr_Cross_AddUp_OutputUnweightbackedTotalCheck_S";
                        end.Next.Value = wbCrCheckS.Offset[0, 1].Value;
                    }
                    else
                    {
                        wbCrCheckT2.Offset[0, 1].Value = wbCrCheckS.Offset[0, 1].Value;
                    }
                }
                wbCrCheckS = sourceTotal.Find("F_Cr_Cross_AddUp_UnweightbackedBaseCheck_S", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                        Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
                wbCrCheckT = targetTotal.Find("F_Cr_Cross_AddUp_UnweightbackedBaseCheck_P", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
                wbCrCheckT2 = targetTotal.Find("F_Cr_Cross_AddUp_UnweightbackedBaseCheck_S", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
                if (null != wbCrCheckS)
                {
                    if (wbCrCheckT == null)
                    {
                        end = end.Offset[1, 0];
                        end.Value = "F_Cr_Cross_AddUp_UnweightbackedBaseCheck_P";
                        end.Next.Value = wbCrCheckS.Offset[0, 1].Value;
                    }
                    else
                    {
                        wbCrCheckT.Offset[0, 1].Value = wbCrCheckS.Offset[0, 1].Value;
                    }
                    if (wbCrCheckT2 == null)
                    {
                        end = end.Offset[1, 0];
                        end.Value = "F_Cr_Cross_AddUp_UnweightbackedBaseCheck_S";
                        end.Next.Value = wbCrCheckS.Offset[0, 1].Value;
                    }
                    else
                    {
                        wbCrCheckT2.Offset[0, 1].Value = wbCrCheckS.Offset[0, 1].Value;
                    }
                }
                wbCrCheckS = sourceTotal.Find("F_Gt_GT_AddUp_Check_Output_Sort_S", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                        Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
                wbCrCheckT = targetTotal.Find("F_Gt_GT_AddUp_OutputUnweightbackedTotalCheck_P", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
                wbCrCheckT2 = targetTotal.Find("F_Gt_GT_AddUp_Check_Output_Sort_S", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
                if (null != wbCrCheckS)
                {
                    if (wbCrCheckT == null)
                    {
                        end = end.Offset[1, 0];
                        end.Value = "F_Gt_GT_AddUp_OutputUnweightbackedTotalCheck_P";
                        end.Next.Value = wbCrCheckS.Offset[0, 1].Value;
                    }
                    else
                    {
                        wbCrCheckT.Offset[0, 1].Value = wbCrCheckS.Offset[0, 1].Value;
                    }
                    if (wbCrCheckT2 == null)
                    {
                        end = end.Offset[1, 0];
                        end.Value = "F_Gt_GT_AddUp_Check_Output_Sort_S";
                        end.Next.Value = wbCrCheckS.Offset[0, 1].Value;
                    }
                    else
                    {
                        wbCrCheckT2.Offset[0, 1].Value = wbCrCheckS.Offset[0, 1].Value;
                    }
                }
            }
        }

        private void UpdateSettingQC3(Excel.Workbook sourceWorkBook, Excel.Workbook targetWorkbook, List<QuestionSettings> usedQuestions, string qTitle)
        {
            Excel.Worksheet targetAdvSetting = ExcelUtil.GetWorkSheetByCodeName(targetWorkbook, Constants.SheetCodeName.Setting);
            Excel.Worksheet sourceAdvSetting = ExcelUtil.GetWorkSheetByCodeName(sourceWorkBook, Constants.SheetCodeName.Setting);

            targetAdvSetting.Range["F2"].Value = qTitle;
            targetAdvSetting.Range["E2"].Value = sourceAdvSetting.Range["E2"].Value;

            Excel.Range start = sourceAdvSetting.Range[Constants.Setting.WbVariable];
            try
            {
                QC4Common.Util.AddFileProperties properties = new QC4Common.Util.AddFileProperties();
                properties.AddSource(targetWorkbook);
                properties.AddFileVersion(targetWorkbook);
                properties.AddUpdatedSource(targetWorkbook);
            }
            catch { }

        }
        private void UpdateSetting(Excel.Workbook sourceWorkBook, Excel.Workbook targetWorkbook, List<QuestionSettings> usedQuestions, string qTitle)
        {
            Excel.Worksheet targetAdvSetting = ExcelUtil.GetWorkSheetByCodeName(targetWorkbook, Constants.SheetCodeName.Setting);
            Excel.Worksheet sourceAdvSetting = ExcelUtil.GetWorkSheetByCodeName(sourceWorkBook, Constants.SheetCodeName.Setting);

            targetAdvSetting.Range["F2"].Value = qTitle;
            targetAdvSetting.Range["E2"].Value = sourceAdvSetting.Range["E2"].Value;

            Excel.Range start = sourceAdvSetting.Range[Constants.Setting.WbVariable];
            string variable = start.Text;
            try {
                QC4Common.Util.AddFileProperties properties = new QC4Common.Util.AddFileProperties();
                properties.AddSource(targetWorkbook);
                properties.AddFileVersion(targetWorkbook);
                properties.AddUpdatedSource(targetWorkbook);
            }
            catch { }
            if (string.IsNullOrEmpty(variable))
            {
                return;
            }

            if (usedQuestions.Where(q => q.Variable == variable).Count() == 0)
            {
                return;
            }

            Excel.Range targetWbVariable = targetAdvSetting.Range[Constants.Setting.WbVariable];
            targetWbVariable.Value = variable;
            targetAdvSetting.Range["K2"].Value = sourceAdvSetting.Range["K2"].Value;
            targetAdvSetting.Range["L2"].Value = sourceAdvSetting.Range["L2"].Value;

            start = sourceAdvSetting.Range[Constants.Setting.WbChoiceBegin];
            Excel.Range end = ExcelUtil.EndxlUp(start);
            if (start.Row > end.Row)
            {
                return;
            }

            end = sourceAdvSetting.get_Range(start, end);
            Object[,] objS = end.Value2;

            Excel.Range targetChoiceBegin = targetAdvSetting.Range[Constants.Setting.WbChoiceBegin];
            targetChoiceBegin.Resize[objS.GetLength(0), 1].Value2 = objS;

            objS = end.Offset[0, 3].Value2;
            targetChoiceBegin = targetChoiceBegin.Offset[0, 3];
            targetChoiceBegin.Resize[objS.GetLength(0), 1].NumberFormat = "@";
            targetChoiceBegin.Resize[objS.GetLength(0), 1].Value2 = objS;
            //targetChoiceBegin.NumberFormat = "@"; //fix for #268410

            objS = end.Offset[0, 2].Value2;
            targetChoiceBegin = targetChoiceBegin.Offset[0, -1];
            targetChoiceBegin.Resize[objS.GetLength(0), 1].NumberFormat = "@";
            targetChoiceBegin.Resize[objS.GetLength(0), 1].Value2 = objS;
            //targetChoiceBegin.NumberFormat = "@"; //fix for #268410

            objS = end.Offset[0, 1].Value2;
            targetChoiceBegin = targetChoiceBegin.Offset[0, -1];
            targetChoiceBegin.Resize[objS.GetLength(0), 1].NumberFormat = "@";
            targetChoiceBegin.Resize[objS.GetLength(0), 1].Value2 = objS;
            //targetChoiceBegin.NumberFormat = "@"; //fix for #268410

        }

        public QCWebException GetRawDataArraySpss(Questions questions
            , decimal[] questionids, OutputDataType datatype, string dirpath, string NALetter, string IVLetter
            , bool[] divisiblePoint, QCAnswerType[] columnQCAnsType, bool[] filteringFlag = null, Excel.Workbook workbook = null
            , List<string> outPuthPath = null, string headerRow = "", QuestionSettings divisonVariable = null
            , List<QuestionSettings> usedQuestions = null, bool isQc4 = true, string targetPath = null
            , Constants.DataOutput.FileType outputType = Constants.DataOutput.FileType.QC3, string[] layoutOutPath = null)
        {
            string[,] resultArray = null;
            int columnsCount = 0;
            string toptablename = null;
            QCWebException exception = null;
            if (!checkGetRawDataArrayArguments(questions, questionids
                    , ref datatype, ref toptablename, ref columnsCount, out divisiblePoint, out columnQCAnsType, out exception, isQc4))
            {
                return exception;
            }
            columnQCAnsType = null;
            divisiblePoint = null;

            int clmIdx = -1;
            int dataCount = 0;
            int count = 0;
            int allDataCount = 0;

            string[,] divAry = null;

            string connectionString = DB.DBHelper.GetConnectionString(workbook);
            SQLiteConnection con = new SQLiteConnection(connectionString);

            filteringFlag = null;
            if (filterSettings != null && filterSettings.Count > 0)
            {
                string filterExp = Macromill.QCWeb.Logic.TabulationEx.Criteria.CriteriaDescProvider.CreateCriteriaDescriptions(filterSettings, questions);
                filteringFlag = new Criteria(filterExp, "", questions).Filtering(connectionString, tableName);
            }

            try
            {
                con.Open();
                var c = DB.DBHelper.GetDataTable("select count(*) from " + tableName, con);
                count = Convert.ToInt32(c.Rows[0][0]);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

            bool[] dataFoundFlag = null;

            if (count == 0)
            {
                progressBar.UpdateProgressBar(++progressBar.percentage, LocalResource.EX_PROGRESSBAR_REMOVING_UNWANTED_FILES);
                dataFoundFlag = divisonVariable == null ? new bool[] { false } : new bool[divisonVariable.CategoryCount];
                AlertAndRemoveFiles(dataFoundFlag, outPuthPath, divisonVariable, targetPath, outputType, layoutOutPath);
                return null;
            }

            int maxLimit = Constants.MaxRowCount;
            int max = count / maxLimit;
            if (count % maxLimit != 0) max++;

            decimal pbVal = (decimal)85 / max / questionids.Length;
            decimal pbVal1 = (decimal)7 / max / 3;
            SeparatedValuesBase sv = new SeparatedValuesBase();
            long lastVal = 0;

            int filterIndex = 0;
            int filterIndexEnd = 0;

            for (int j = 0; j < max; j++)
            {
                bool[] filterFlag = null;

                if (filteringFlag != null && filteringFlag.Count() > 0)
                {
                    filterFlag = new bool[maxLimit];
                    filterIndexEnd = maxLimit;
                    if (filterIndexEnd + filterIndex > count)
                    {
                        filterIndexEnd = count - filterIndex;
                    }
                    for (int i = 0; i < filterIndexEnd; i++)
                    {
                        filterFlag[i] = filteringFlag[filterIndex++];
                    }
                }
                if (null != divisonVariable)
                {
                    int clmIdxDiv = -1;
                    int dataCountDiv = 0;
                    int allDataCountDiv = 0;
                    Questions.Question question = questions[divisonVariable.Id] as Questions.Question;
                    string questionFlag = usedQuestions.First(x => x.Id == question.ID).QuestionFlag;

                    if (!putData(question, OutputDataType.Code, 1, dirpath, "", "*"
                                    , ref dataCountDiv, ref allDataCountDiv, ref divAry, ref clmIdxDiv, out exception, ref filterFlag, ref lastVal, connectionString, maxLimit,questionFlag, con, true))
                    {
                        return exception;
                    }
                }

                for (var i = 0; i < questionids.Length; ++i)
                {
                    Questions.Question question = questions[questionids[i]] as Questions.Question;
                    string questionFlag = usedQuestions.First(x => x.Id == question.ID).QuestionFlag;
                    if (null == question)
                    {
                        continue;
                    }
                    if (Constants.DataOutput.FileType.QLayout.Equals(outputType))
                    {

                        if (!putDataQrowdata(question, datatype, columnsCount, dirpath, NALetter, IVLetter
                                , ref dataCount, ref allDataCount, ref resultArray, ref clmIdx, out exception
                                , ref filterFlag, ref lastVal, connectionString, maxLimit, questionFlag, con, isQc4))
                        {
                            return exception;
                        }
                    }
                    else
                    {
                        if (!putDataSpss(question, datatype, columnsCount, dirpath, NALetter, IVLetter
                                , ref dataCount, ref allDataCount, ref resultArray, ref clmIdx, out exception, ref filterFlag, ref lastVal, connectionString, maxLimit, questionFlag, con, isQc4))
                        {
                            return exception;
                        }
                    }
                    progressBar.percentage += pbVal;
                    progressBar.UpdateProgressBar(progressBar.percentage);
                }

                bool[,] divFlag = null;
                progressBar.percentage += pbVal1;
                progressBar.UpdateProgressBar(progressBar.percentage, LocalResource.EX_PROGRESSBAR_LOADING_FLAG);
                GetDivisionFlag(ref divFlag, ref divAry, divisonVariable, ref resultArray, ref dataFoundFlag);
                progressBar.percentage += pbVal1;
                progressBar.UpdateProgressBar(progressBar.percentage, LocalResource.EX_PROGRESSBAR_WRITING);
                WriteData(resultArray, outPuthPath, divFlag, usedQuestions, outputType, ref dataFoundFlag, targetPath, divisonVariable, workbook.Application);
                progressBar.percentage += pbVal1;
                progressBar.UpdateProgressBar(progressBar.percentage, LocalResource.EX_PROGRESSBAR_LOADING);
                string lastValSql = "select sort_no from (select sort_no from " + tableName + " where sort_no > " + lastVal + " order by sort_no limit " + maxLimit + ") order by sort_no desc limit 1";
                var lastVa = DB.DBHelper.GetDataTable(lastValSql, con);
                lastVal = Convert.ToInt32(lastVa.Rows[0][0]);

                clmIdx = -1;
                dataCount = 0;
                allDataCount = 0;
                resultArray = null;
            }
            progressBar.UpdateProgressBar(++progressBar.percentage, LocalResource.EX_PROGRESSBAR_REMOVING_UNWANTED_FILES);
            AlertAndRemoveFiles(dataFoundFlag, outPuthPath, divisonVariable, targetPath, outputType, layoutOutPath);
            progressBar.UpdateProgressBar(progressBar.percentage, LocalResource.EX_PROGRESSBAR_FINISHING);
            con.Close();
            con.Dispose();
            return null;
        }

        internal bool putDataSpss(Questions.Question question, OutputDataType datatype
                          , int columnsCount, string dirpath, string NALetter, string IVLetter
                          , ref int dataCount, ref int allDataCount, ref string[,] puttoarray, ref int clmIdx
                          , out QCWebException exception, ref bool[] filteringFlag, ref long lastValue
                          , string conString, int maxLimit,string questionFlag, SQLiteConnection con = null, bool isQc4 = true)
        {
            exception = null;
            if ((question.QuestionType & QuestionType.MatrixParent) == QuestionType.MatrixParent)
            {
                foreach (DictionaryEntry de in question.ChildQuestions)
                {
                    Questions.Question childQ = de.Value as Questions.Question;
                    if (!putDataSpss(childQ, datatype, columnsCount, dirpath, NALetter, IVLetter
                            , ref dataCount, ref allDataCount, ref puttoarray, ref clmIdx, out exception, ref filteringFlag, ref lastValue, conString, maxLimit, questionFlag, con, isQc4))
                    {
                        return false;
                    }
                }
                return true;
            }
            QuestionType qType = question.QuestionType & (QuestionType.SA | QuestionType.MA | QuestionType.N | QuestionType.FA);
            if ((int)qType == 0)
            {
                return false;
            }

            List<Data> data = null;
            try
            {
                string sql = "";
                if (questionFlag == "An")
                {
                    sql = "SELECT m.`" + question.ColumnName + "` FROM multivariate m join " + tableName + " a on m.sort_no = a.sort_no  WHERE m.sort_no > " + lastValue + " ORDER BY m.sort_no LIMIT " + maxLimit;
                }
                else
                    sql = "SELECT " + question.ColumnName + " FROM " + tableName + " WHERE sort_no > " + lastValue + " ORDER BY sort_no LIMIT " + maxLimit;
                var dtReadTable1 = DB.DBHelper.GetDataTable(sql, con);
                data = ReadTextFile.ReadDataTable(dtReadTable1, qType, null, out exception);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

            if (data == null) return false;
            if (clmIdx == -1)
            {
                allDataCount = data.Count;
                int l = filteringFlag == null ? 0 : filteringFlag.Length;
                if (filteringFlag == null)
                {
                    filteringFlag = new bool[allDataCount];
                }
                if (l != allDataCount)
                {
                    Array.Resize<bool>(ref filteringFlag, allDataCount);
                    if (l < allDataCount) Macromill.QCWeb.Common.OperateArray.InitializeWith<bool>(ref filteringFlag, true, l, allDataCount - 1);
                }
                dataCount = 1;
                for (int i = 0; i < allDataCount; ++i)
                {
                    if (!data[i].IsDeleted && filteringFlag[i]) ++dataCount;
                }
                puttoarray = new string[--dataCount, columnsCount];
            }
            else if (data.Count != allDataCount)
            {
                return false;
            }
            string[] sectorDecArray = null;
            if (qType == QuestionType.SA || qType == QuestionType.MA)
            {
                string fmt = new string('0', (int)Math.Floor(Math.Log10(question.Sectors.Count)) + 1);
                if (datatype == OutputDataType.Decode)
                {
                    sectorDecArray = new string[question.Sectors.Count];
                    for (int i = 0; i < question.Sectors.Count; ++i)
                    {
                        sectorDecArray[i] = (i + 1).ToString(fmt) + ":" + question.Sectors[i + 1].Description;
                    }
                }
            }
            if (qType == QuestionType.MA && datatype != OutputDataType.Code && datatype != OutputDataType.QC3)
            {
                int startClmIdx = clmIdx;
                int rowIdx = -1;
                for (int i = 0; i < data.Count; ++i)
                {
                    if (data[i].IsDeleted || !filteringFlag[i]) continue;
                    ++rowIdx;
                    clmIdx = startClmIdx;
                    switch (data[i].DataType)
                    {
                        case DataType.NormalData:
                        case DataType.NAData:
                            bool isNA = true;
                            if (data[i].DataType == DataType.NormalData)
                            {
                                for (int j = 0; j < question.Sectors.Count; ++j)
                                {
                                    int idx = j / GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                                    int e = j % GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                                    if (((data[i] as MAData).Value(idx) & (int)Math.Pow(2.0, (double)e)) != 0)
                                    {
                                        puttoarray[rowIdx, ++clmIdx] = datatype == OutputDataType.Flag ? "1" : sectorDecArray[j];
                                        isNA = false;
                                    }
                                    else
                                    {
                                        puttoarray[rowIdx, ++clmIdx] = datatype == OutputDataType.Flag ? "0" : null;
                                    }
                                }
                            }
                            if (isNA)
                            {
                                for (int j = 0; j < question.Sectors.Count; ++j)
                                {
                                    puttoarray[rowIdx, ++clmIdx] = NALetter;
                                }
                            }
                            break;
                        case DataType.IVData:
                            for (int j = 0; j < question.Sectors.Count; ++j)
                            {
                                puttoarray[rowIdx, ++clmIdx] = IVLetter;
                            }
                            break;
                    }
                }
            }
            else
            {
                ++clmIdx;
                int rowIdx = -1;
                for (int i = 0; i < data.Count; ++i)
                {
                    if (data[i].IsDeleted || !filteringFlag[i]) continue;
                    ++rowIdx;
                    switch (data[i].DataType)
                    {
                        case DataType.NormalData:
                            switch (qType)
                            {
                                case QuestionType.SA:
                                    if (datatype == OutputDataType.Decode)
                                    {
                                        puttoarray[rowIdx, clmIdx] = sectorDecArray[(data[i] as SAData).Value - 1];
                                    }
                                    else
                                    {
                                        puttoarray[rowIdx, clmIdx] = (data[i] as SAData).Value.ToString();
                                    }
                                    break;
                                case QuestionType.MA:

                                    if (isQc4)
                                    {
                                        string d = "";
                                        for (int j = question.Sectors.Count - 1; j >= 0; --j)
                                        {
                                            int idx = j / GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                                            int e = j % GlobalTabulation.SECTORS_COUNT_PER_4BITE;
                                            if (((data[i] as MAData).Value(idx) & (int)Math.Pow(2.0, (double)e)) != 0)
                                            {
                                                d += "1";
                                            }
                                            else
                                            {
                                                d += "0";
                                            }
                                        }
                                        puttoarray[rowIdx, clmIdx] = d;
                                    }
                                    else
                                    {
                                        if (datatype == OutputDataType.Code)
                                        {
                                            puttoarray[rowIdx, clmIdx] = (data[i] as MAData).CodeValue;
                                        }
                                        else
                                        {
                                            puttoarray[rowIdx, clmIdx] = "," + (data[i] as MAData).CodeValue + ",";
                                        }
                                    }
                                    break;
                                case QuestionType.FA:
                                    try
                                    {
                                        puttoarray[rowIdx, clmIdx] = question.QCAnswerType == QCAnswerType.D ? Convert.ToDateTime((data[i] as FAData).Value).ToString("dd-MMM-yyyy HH:mm:ss", new CultureInfo("en-US")) : (data[i] as FAData).Value;
                                    } catch
                                    {
                                        puttoarray[rowIdx, clmIdx] = (data[i] as FAData).Value;
                                    }
                                    if (question.QCAnswerType != QCAnswerType.D)
                                    {
                                        string faStr = puttoarray[rowIdx, clmIdx].Replace('\t', ' ').Trim();
                                        faStr = String.Join(" ", faStr.Split(new string[] { " ", "　" }, StringSplitOptions.RemoveEmptyEntries));
                                        if(faStr.Contains("'"))
                                        {
                                            faStr = "'" + faStr.Replace("'", "''") + "'";
                                        }
                                        puttoarray[rowIdx, clmIdx] = faStr;
                                    }
                                    break;
                                case QuestionType.N:
                                    puttoarray[rowIdx, clmIdx] = getConveted_Exponent_Value((data[i] as NData).Value.ToString());
                                    break;
                            }
                            break;
                        case DataType.NAData:
                            switch (qType)
                            {
                                case QuestionType.N:
                                case QuestionType.FA:
                                    puttoarray[rowIdx, clmIdx] = IVLetter;
                                    break;
                                default:
                                    puttoarray[rowIdx, clmIdx] = NALetter;
                                    break;
                            }
                            break;
                        case DataType.IVData:
                            puttoarray[rowIdx, clmIdx] = IVLetter;
                            break;
                    }
                }
            }
            return true;
        }

        private static void GetQcRawData(Questions questions, decimal[] questionids, string connectionString,
            ref long lastVal, ref string[,] dataAry, int maxLimit, List<QuestionSettings> usedQuestions, bool[] filteringFlag = null)
        {
            StringBuilder str = new StringBuilder();
            str.Append("SELECT ");
            bool isMultivariate = false;
            for (var i = 0; i < questionids.Length; ++i)
            {
                Questions.Question question = questions[questionids[i]] as Questions.Question;
                if (null == question)
                {
                    continue;
                }
                string questionFlag = usedQuestions.First(x => x.Id == question.ID).QuestionFlag;
                if (questionFlag == "An")
                {
                    isMultivariate = true;
                    str.Append("m.`" + question.ColumnName + "`,");
                }
                else
                {
                    str.Append("a.`" + question.ColumnName + "`,");
                }
            }
            str.Length--;
            if (isMultivariate)
            {
                str.Append(" FROM multivariate m join " + tableName + " a on a.sort_no = m.sort_no  WHERE a.sort_no > " + lastVal + " ORDER BY a.sort_no LIMIT " + maxLimit);
            }
            else
                str.Append(" FROM " + tableName + " a WHERE a.sort_no > " + lastVal + " ORDER BY a.sort_no LIMIT " + maxLimit);

            dataAry = GetDataAry(str.ToString(), connectionString, filteringFlag);
        }

        private static String[,] GetDataAry(string sql, string connectionString, bool[] filteringFlag)
        {
            DataTable dt;
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                con.Open();
                dt = DB.DBHelper.GetDataTable(sql, con);
            }

            int maxRow = dt.Rows.Count;
            int maxCol = dt.Columns.Count;
            string[,] dataAry;

            if (filteringFlag == null)
            {
                dataAry = new string[maxRow, maxCol];
                for (int i = 0; i < maxRow; i++)
                {
                    for (int j = 0; j < maxCol; j++)
                    {
                        dataAry[i, j] = SetSpace(Convert.ToString(dt.Rows[i][j]));
                    }
                }
            }
            else
            {
                int rows = 0;
                for (int i = 0; i < maxRow; i++)
                {
                    if (filteringFlag[i])
                    {
                        rows++;
                    }
                }

                dataAry = new string[rows, maxCol];
                for (int i = 0, k = 0; i < maxRow; i++)
                {
                    if (filteringFlag[i])
                    {
                        for (int j = 0; j < maxCol; j++)
                        {
                            dataAry[k, j] = SetSpace(Convert.ToString(dt.Rows[i][j]));
                        }
                        k++;
                    }
                }
            }

            return dataAry;
        }

        private void InsertItems(List<QuestionSettings> usedQuestions, string connectionString, bool[,] divFlag, int i, object[,] rawDataAry, string sql)
        {
            using(SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = connection.CreateCommand())
                {
                    using (SQLiteTransaction transaction = connection.BeginTransaction())
                    {
                        command.CommandText = sql;

                        command.Parameters.AddWithValue("@sample_id", "");
                        List<int> anIds = new List<int>() { 0 };
                        for (int j = 1; j < usedQuestions.Count; j++)
                        {
                            if (usedQuestions[j].QuestionFlag == "An")
                                anIds.Add(Convert.ToInt32(j));
                            else
                                anIds.Add(0);
                            command.Parameters.AddWithValue("@q_" + j, "");
                        }
                        int pbR = PbCount - PbVal;
                        decimal pbInc = (decimal)pbR / divFlag.GetLength(0);
                        for (int j = 0; j < divFlag.GetLength(0); j++)
                        {
                            progressBar.percentage += pbInc;
                            if (!divFlag[j, i]) continue;
                            command.Parameters["@sample_id"].Value = rawDataAry[j, 0];
                            for (int k = 1; k < rawDataAry.GetLength(1); k++)
                            {
                                if (k == anIds[k])
                                    command.Parameters["@q_" + k].Value = rawDataAry[j, k] != null && rawDataAry[j, k].ToString() == "**" ? "*" : rawDataAry[j, k];
                                else
                                    command.Parameters["@q_" + k].Value = rawDataAry[j, k];
                            }
                            command.ExecuteNonQuery();
                            progressBar.UpdateProgressBar(progressBar.percentage, LocalResource.EX_PROGRESSBAR_LOADING);
                        }
                        transaction.Commit();
                    }
                    command.Dispose();
                }
                connection.Dispose();
            }
        }

        public int InsertResultItem(string runTag, int topicId, string documentNumber, int rank, double score, SQLiteCommand command)
        {
            command.Parameters["@RunTag"].Value = runTag;
            command.Parameters["@TopicId"].Value = topicId;
            command.Parameters["@DocumentNumber"].Value = documentNumber;
            command.Parameters["@Rank"].Value = rank;
            command.Parameters["@Score"].Value = score;
            return command.ExecuteNonQuery();
        }
        private string getConveted_Exponent_Value(string val)
        {
            if (val.Contains("E"))
            {
                try
                {
                    string Evalue = Convert.ToString(Decimal.Parse(val, System.Globalization.NumberStyles.Float));
                    if (Evalue.StartsWith("-"))
                    {
                        if (Evalue.Length < 23)
                        {
                            return Evalue;
                        }
                        else
                        {
                            return val;
                        }
                    }
                    else
                    {
                        if (Evalue.Length < 22)
                        {
                            return Evalue;
                        }
                        else
                        {
                            return val;
                        }
                    }
                }
                catch (Exception ex) { return val; }
            }
            else
            {
                return val;
            }
        }

    }
}
