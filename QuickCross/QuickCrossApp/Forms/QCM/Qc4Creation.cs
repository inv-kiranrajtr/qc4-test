using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using Macromill.QCWeb.COMOperate;
using Qc4Launcher.Util;
using System.IO;
using static Qc4Launcher.Util.Qc3Parse;
using System.Collections;
using System.Diagnostics;

namespace Qc4Launcher.Forms.QCM
{
    class Qc4Creation
    {
        private ExcelOperate excelOperate;
        private Excel.Workbook TargetWorkBook;

        #region ListSheetUpdateVariable
        private ArrayList saArray = new ArrayList();
        private ArrayList maArray = new ArrayList();
        private ArrayList nArray = new ArrayList();
        private ArrayList samaArray = new ArrayList();
        private ArrayList sanArray = new ArrayList();
        private ArrayList manArray = new ArrayList();
        private ArrayList samanArray = new ArrayList();
        private ArrayList faArray = new ArrayList();
        private ArrayList allArray = new ArrayList();
        private ArrayList allDArray = new ArrayList();
        #endregion


        public Qc4Creation(ExcelOperate excelOperate)
        {
            this.excelOperate = excelOperate;
            TargetWorkBook = OpenQc4_Template(excelOperate);
        }

        public void Save_Qc4File(string TempPath, string SourcePath)
        {
            TargetWorkBook.Password = Constants.Password;
            TargetWorkBook.Protect(Constants.Password, true);
            Excel.Worksheet ldelSheet = ExcelUtil.GetWorkSheetByCodeName(TargetWorkBook, Constants.SheetCodeName.LDEL);
            ldelSheet.Protect(Constants.Password);
            TargetWorkBook.SaveAs(TempPath + "\\" + Constants.TemplateFile.QC4_Template);
            string TargetPath = Path.GetDirectoryName(SourcePath) + "\\" + Path.GetFileNameWithoutExtension(SourcePath) + ".qc4";
            QC4Common.Util.FileHelper fileHelper = new QC4Common.Util.FileHelper();
            fileHelper.SaveFile(ref TargetPath, ref TempPath, TargetWorkBook, TargetWorkBook.Application, true, false,QC4Common.Common.Constants.FileProperties.QCM);
            //TargetWorkBook.Activate();
            //TargetWorkBook.Application.EnableEvents = true;
            //TargetWorkBook.Application.DisplayAlerts = true;
            //TargetWorkBook.Close();
            Excel.Application app = TargetWorkBook.Application;
            app.Quit();
            excelOperate.Dispose();
            try
            {
                Directory.Delete(TempPath, true);
            }
            catch { }
        }

        private Excel.Workbook OpenQc4_Template(ExcelOperate excelOperate)
        {
            Excel.Application application = excelOperate.Excel;
            TargetWorkBook = application.Workbooks.Open(System.AppContext.BaseDirectory + "Templates\\" + Constants.TemplateFile.QC4_Template);
            TargetWorkBook.Application.EnableEvents = false;
            TargetWorkBook.Application.DisplayAlerts = false;
            return TargetWorkBook;
        }

        public bool Update_QuestionSettingSheet(Object[,] qsAry, List<Qc3Parse.QDataDetail> qData,String title)
        {
            Excel.Worksheet tQs = ExcelUtil.GetWorkSheetByCodeName(TargetWorkBook, Constants.SheetCodeName.QuestionSetting);
            tQs.Cells[2, 12].Value = title;

            Excel.XlCalculation xlCalculation = TargetWorkBook.Application.Calculation;
            TargetWorkBook.Application.Calculation = Excel.XlCalculation.xlCalculationManual;
            Excel.Range range = tQs.Range["A4"];
            range = range.Resize[qsAry.GetLength(0), qsAry.GetLength(1)];
            range.Value2 = qsAry;

            Excel.Worksheet qsB = ExcelUtil.GetWorkSheetByCodeName(TargetWorkBook, Constants.SheetCodeName.QuestionSettingB);
            Excel.Range qsBRange = qsB.Range["A4"];
            qsBRange = qsBRange.Resize[qsAry.GetLength(0), qsAry.GetLength(1)];
            qsBRange.Value = qsAry;

            Excel.Range startRange = tQs.Cells[Constants.QS.StartRow, Constants.QS.ColChoiceBegin];

            int index = 0;
            qData.RemoveAll(q => !q.isFound);
            Excel.Range tRange = startRange.Resize[qData.Count(), 1];
            foreach (Excel.Range r in tRange)
            {
                QC4Common.Util.QSUtil.SetRowName(tQs, r.EntireRow, qData[index].questionOrder);
                if (qData[index].categoryCount > 0)
                {
                    r.Resize[1, qData[index].categoryCount].Interior.Color = Constants.Color.LightGrey;
                }

                //if (counts[index] > 0)
                //{
                //    range = tQs.Cells[r.Row, Constants.QS.QsColSubtotal1];
                //    range.Resize[1, counts[index] * 2].Interior.Color = Constants.Color.LightGrey;
                //}
                index++;
            }
            TargetWorkBook.Application.Calculation = xlCalculation;

            return true;
        }

        public bool Update_DetailSettingSheet()
        {
            return true;
        }

        public bool Update_ListSheet(List<QDataDetail> qData)
        {
            Excel.Worksheet list = ExcelUtil.GetWorkSheetByCodeName(TargetWorkBook, Constants.SheetCodeName.List);
            qData.RemoveAll(q => !q.isFound);

            int max = qData.Count();
            for (int i = 0; i < max; i++)
            {
                UpdateListVariable(qData[i].variableName, qData[i].answerType);
            }
            UpdateListSheet();

            return true;
        }

        private void UpdateListVariable(string variable, string ansType)
        {
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

        private void UpdateListSheet()
        {
            Excel.Worksheet listSheet = TargetWorkBook.Worksheets[Constants.SheetType.sh_ListView];
            //listSheet.Unprotect(Password);
            String[,] outPutArray = new string[allDArray.Count, 10];

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

            listSheet.get_Range("A2", "J" + (allDArray.Count + 1)).Value = outPutArray;
        }

        private static void ListOutPutAdd(String[,] outPutArray, ArrayList list, int n)
        {
            for (int i = 0; i < list.Count; i++)
            {
                outPutArray[i, n] = list[i].ToString();
            }
        }

        public void Update_GTSheet()
        {
            try
            {
                Excel.Worksheet gtSheet = ExcelUtil.GetWorkSheetByCodeName(TargetWorkBook, Constants.SheetCodeName.GTTabulation);
                //QC4Common.Common.GTAutoSetting.ExcelSet(gtSheet.Application);
                QC4Common.Common.GTAutoSetting.FNCGTAutoSettingMainIni(gtSheet);
                QC4Common.Common.GTAutoSetting.FNCGetQuesData(gtSheet, null, ExcelUtil.GetWorkSheetByCodeName(TargetWorkBook, Constants.SheetCodeName.QuestionSetting));
                QC4Common.Common.GTAutoSetting.LoadDefaultDataToGTHiddenSheet(gtSheet);
                //QC4Common.Common.GTAutoSetting.ExcelReset(gtSheet.Application);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Update_CrossSheet(List<QDataDetail> qData)
        {
            const string crossStartCel = "B14";

            qData.RemoveAll(q => !q.isFound);
            int max = qData.Count();
            Object[,] variableAry = new Object[max - 1, 4];
            int index = 0;
            for (int i = 1; i < max; i++)
            {
                if (qData[i].answerType == Constants.AnswerType.D || qData[i].answerType == Constants.AnswerType.FA)
                {
                    continue;
                }
                variableAry[index, 1] = qData[i].variableName;
                variableAry[index, 2] = qData[i].answerType;
                if (qData[i].categoryCount != 0)
                {
                    variableAry[index, 3] = qData[i].categoryCount;
                }
                //variableAry[index, 3] = qData[i].categoryCount == 0 ? null : qData[i].categoryCount;
                variableAry[index, 0] = ++index;
            }
            VariableUpdation(Constants.SheetCodeName.CrossTabulation, crossStartCel, variableAry);
        }

        private void VariableUpdation(string codeName, string startCell, Object[,] varAry)
        {
            try
            {
                Excel.Worksheet sheet = ExcelUtil.GetWorkSheetByCodeName(TargetWorkBook, codeName);
                Excel.Range range = sheet.Range[startCell].Resize[varAry.GetLength(0), varAry.GetLength(1)];
                range.Value = varAry;
            }
            catch { }
        }

        public bool Update_SettingSheet(String TempPath,String SourcePath,String TargetPath)
        {
            Process currentProcess = Process.GetCurrentProcess();
            QC4Common.DB.DBHelper.SetConnectionString(TargetWorkBook, TempPath, Path.GetFileNameWithoutExtension(SourcePath) + Constants.Qc4Extension, TargetPath, currentProcess.Id.ToString());
            return true;
        }

        public Excel.Workbook GetTargetWorkbook()
        {
            return TargetWorkBook;
        }
    }
}

