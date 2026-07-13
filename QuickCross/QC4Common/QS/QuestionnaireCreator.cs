using QC4Common.Common;
using log4net;
using QC4Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using ProgressBar = QC4Common.Forms.ProgressBar;
using System.ComponentModel;
using System.Windows.Interop;
using Macromill.QCWeb.COMOperate;

namespace QC4Common.QS
{
    public class QuestionnaireCreator
    {
        private const String TEMPLATE_NAME = "Questionnaire_Std.xlsx";
        ExcelOperate excelOperate = null;
        Excel.Workbook targetBook = null;
        Excel.Application xlApp = null;
        Excel.Workbooks targetBooks = null;
        private Excel.Worksheet TargetSheet;
        private int WriteRow = 3;

        private const String ButtonOption = "○";
        private const String ButtonCheckBox = "□";
        private const String ButtonDropDown = "▽";
        private const String strLeftSB = "【";
        private const String strRightSB = "】";

        private const Double HeightRow = 14.25;
        private const Double LenofLine = 31;
        private const Double ToMergeCell = 22;
        Excel.Workbook wb = null;
        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string GetTemplatePath(string templateName)
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\QC4\\Templates\\" + templateName;
        }

        public void CreateQuesionnaire(Excel.Workbook workbook = null, int row = 0, int col = 0)
        {
            wb = workbook ?? workbook;
            try { wb.Application.Cursor = Excel.XlMousePointer.xlWait; } catch (Exception e) { }
            ReturnClass rc = new QSValidate(wb.Application.ActiveWorkbook).QuestionConfigurationCheck();
            wb.Application.Cursor = Excel.XlMousePointer.xlDefault;
            if (row != 0 && col != 0)
            {
                if (!rc.Result)
                {
                    wb.Application.EnableEvents = false;
                    Excel.Worksheet s = ExcelUtilForAddIn.GetWorkSheetByCodeName(wb,Constants.SheetCodeName.Setting);
                    Excel.Range r = s.Cells[row, col];
                    s.Application.EnableEvents = false;
                    var a = new IntPtr(Convert.ToInt32(Convert.ToString(r.Value)));
                    r.Value = null;
                    r = s.Cells[row - 1, col];
                    r.Value = null;
                    r = s.Cells[row + 1, col];
                    r.Value = rc.Msg;
                    //QC4Common.Common.MessageDialog.ShowMessageOnParent(rc.Msg, QC4Common.Common.Constants.MessageType.ErrorOk, a);
                    s.Application.EnableEvents = true;
                    return;
                }
            }
            else
            {
                if (!rc.Result)
                {
                    Excel.Range value = (Excel.Range)rc.Value;
                    value.Select();
                    MessageDialog.ErrorOk(rc.Msg);
                    return;
                }
            }
            ProgressBar progress;
            Excel.Worksheet sourceSheet = ExcelUtilForAddIn.GetWorkSheetByCodeName(wb,Constants.SheetCodeName.QuestionSetting);
            if (row != 0 && col != 0)
            {
                Excel.Worksheet s = ExcelUtilForAddIn.GetWorkSheetByCodeName(wb,Constants.SheetCodeName.Setting);
                Excel.Range r = s.Cells[row, col];
                progress = new ProgressBar(Convert.ToString(r.Value), sourceSheet);
                s.Application.EnableEvents = false;
                r.Value = null;
                r = s.Cells[row - 1, col];
                r.Value = null;
                s.Application.EnableEvents = true;
            }
            else
            {

                progress = new ProgressBar(sourceSheet);
            }
            new Thread(() => CreateQuesionnaire(progress, sourceSheet)).Start();
            progress.ShowDialog();
            try
            {
                SetForegroundWindow((IntPtr)xlApp.Hwnd);
                xlApp.WindowState = Excel.XlWindowState.xlMaximized;
            }
            catch { }
            finally
            {
                COMWholeOperate.releaseComObject(ref excelOperate);
                COMWholeOperate.releaseComObject(ref sourceSheet);
                COMWholeOperate.releaseComObject(ref TargetSheet);
                COMWholeOperate.releaseComObject(ref targetBook);
                COMWholeOperate.releaseComObject(ref targetBooks);
                COMWholeOperate.releaseComObject(ref xlApp);
                GC.Collect();
            }
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        internal void CreateQuesionnaire(ProgressBar progress, Excel.Worksheet sourceSheet)
        {
            try
            {
                double percentage = 0;
                excelOperate = new ExcelOperate();
                xlApp = excelOperate.Excel;
                targetBooks = xlApp.Workbooks;
                progress.OnWorkerMethodComplete(percentage += 5, CommonResource.EXPORT_QUESTIONNAIRE);
                Definitions.VariableDictionary = QC4Common.Util.DictionaryUtil.PopulateQSDictionary(wb.Application.ActiveWorkbook);
                progress.OnWorkerMethodComplete(percentage += 2, CommonResource.EXPORT_QUESTIONNAIRE);
                sourceSheet.Application.EnableEvents = false;
                sourceSheet.Application.ScreenUpdating = false;
                sourceSheet.Application.DisplayAlerts = false;
                sourceSheet.Application.Calculation = Excel.XlCalculation.xlCalculationManual;

                string tempPath = GetTemplatePath(TEMPLATE_NAME);
                targetBook = targetBooks.Add(tempPath);
                if (targetBook == null) return;
                progress.OnWorkerMethodComplete(percentage += 1, CommonResource.EXPORT_QUESTIONNAIRE);
                xlApp.WindowState = Excel.XlWindowState.xlMinimized;
                TargetSheet = targetBook.Sheets["調査票"];
                TargetSheet.Name = CommonResource.QS_QUESTIONIRE_SHEET_NAME;
                TargetSheet.Range["K13"].Value = CommonResource.QS_QUESTIONIRE_TITLE1;
                TargetSheet.Range["F5"].Value = CommonResource.QS_QUESTIONIRE_TITLE2;
                TargetSheet.Range["H6"].Value = CommonResource.QS_QUESTIONIRE_TITLE3;
                TargetSheet.Range["H7"].Value = CommonResource.QS_QUESTIONIRE_TITLE4;
                TargetSheet.Range["H8"].Value = CommonResource.QS_QUESTIONIRE_TITLE5;
                string str = (sourceSheet.Cells[2, 12] as Excel.Range).Text;
                TargetSheet.Rectangles("Invest_Title").Text = str;
                WriteMain(progress, ref percentage);
                progress.OnWorkerMethodComplete(percentage += 1, CommonResource.EXPORT_QUESTIONNAIRE);
                TargetSheet.get_Range("A11", "A20").EntireRow.Delete();
                if (QC4Common.Common.Constants.GlobalMode.Split(',')[0] == "en-US")
                {
                    TargetSheet.Rows.Font.Name = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
                    TargetSheet.Rectangles("Invest_Title").Text = "";

                    foreach (Microsoft.Office.Interop.Excel.Shape shp in TargetSheet.Shapes)
                    {
                        try
                        {
                            shp.TextFrame2.TextRange.Font.NameFarEast = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
                            shp.TextFrame2.TextRange.Characters.Font.Name = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
                            TargetSheet.Rectangles("Invest_Title").Font.Size = 10;
                            shp.TextFrame2.TextRange.Text = str;
                        }
                        catch (Exception ex)
                        { }
                    }
                }
                xlApp.Visible = true;
                TargetSheet.Visible = Excel.XlSheetVisibility.xlSheetVisible;
                progress.OnWorkerMethodComplete(100, CommonResource.EXPORT_QUESTIONNAIRE);
                sourceSheet.Application.EnableEvents = true;
                sourceSheet.Application.ScreenUpdating = true;
                sourceSheet.Application.DisplayAlerts = true;
                sourceSheet.Application.Calculation = Excel.XlCalculation.xlCalculationAutomatic;
            }
            catch (Exception ex)
            {
                _log.LogErrorForExcel(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void WriteMain(ProgressBar progress, ref double percentage)
        {
            try
            {
                List<QuestionSettings> qArray = new List<QuestionSettings>();
                WriteRow += 10;
                bool printerExists = CommonCheck.PrinterCheck();
                List<QuestionSettings> list = Definitions.VariableDictionary
                    .Select(v => v.Value).OrderBy(q => q.RowNumber).ToList();
                double pbVal = (double)90 / list.Count();
                foreach (QuestionSettings qs in list)
                {
                    if (qs.QuestionNumber != "")
                    {
                        if (!JudgeSameQuestion(qs, qArray))
                        {
                            if (printerExists)
                            {
                                ItemArrayWithPageBreak(qArray);
                            }
                            else
                            {
                                ItemArray(qArray);
                            }
                            qArray.Clear();
                            qArray.Add(qs);
                        }
                        else
                        {
                            qArray.Add(qs);
                        }
                    }
                    else
                    {
                        qArray.Add(qs);
                    }
                    progress.OnWorkerMethodComplete(percentage += pbVal, "");
                }
                if (printerExists)
                {
                    ItemArrayWithPageBreak(qArray, true);
                }
                else
                {
                    ItemArray(qArray, true);
                }
            }
            catch (Exception ex)
            {
                _log.LogErrorForExcel(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void ItemArray(List<QuestionSettings> qArray, bool isLast = false)
        {
            //'Mod.2017/6/18 新デザイン調査票対応
            QuestionSettings qs = qArray[0];
            if (!isLast)
            {
                WriteQuestion(qs);
            }
            else if (qs.QuestionType != "")
            {
                WriteQuestion(qs);
            }
            switch (qs.QuestionType)
            {
                case Constants.QuestionType.SAR:
                case Constants.QuestionType.MAC:
                case Constants.QuestionType.SAS:
                case Constants.QuestionType.SAP:
                    WriteSAMA(qs);
                    WriteSAMAFAorN(qArray);
                    break;
                case Constants.QuestionType.MTS:
                case Constants.QuestionType.MTT:
                case Constants.QuestionType.MTM:
                    WriteMT(qArray);
                    WriteSAMAFAorN(qArray);
                    break;
                case Constants.QuestionType.RNK:
                    Excel.Range tempCell = TargetSheet.Cells.Item[WriteRow, 8];
                    tempCell.NumberFormat = "@";
                    string rnkStr = CommonResource.QS_QUESTIONIRE_TITLE8 + "（";
                    rnkStr += string.Format(CommonResource.QS_QUESTIONIRE_TITLE9 + "）", qs.QuestionCount == null ? 1 : qs.QuestionCount);
                    tempCell.Value = rnkStr;
                    WriteRow += 2;
                    WriteSAMA(qs);
                    break;
                case Constants.QuestionType.FAS:
                case Constants.QuestionType.RAT:
                    WriteRATFAS(qArray);
                    WriteSAMAFAorN(qArray);
                    break;
                case Constants.QuestionType.FAL:
                    WriteFAs(qs);
                    break;
            }

        }

        private void ItemArrayWithPageBreak(List<QuestionSettings> qArray, bool isLast = false)
        {
            long pageCount;
            long pageRow;
            long afterPageCount;

            pageCount = TargetSheet.HPageBreaks.Count + 1;
            pageRow = WriteRow;
            ItemArray(qArray, isLast);
            //doEvents
            afterPageCount = TargetSheet.HPageBreaks.Count + 1;
            if (afterPageCount > pageCount)
            {
                TargetSheet.HPageBreaks.Add(TargetSheet.Cells[pageRow - 10, 2]);
            }
        }

        private void WriteQuestion(QuestionSettings qs)
        {
            //表題の取得
            string writeString = qs.TableHeading;
            // '表題が空文字の場合、質問文を取得
            if (string.IsNullOrEmpty(writeString))
            {
                writeString = qs.Question;
            }
            else
            {
                //Add.2017/04　#74285　「質問文A 【質問文B】」
                if ((qs.QuestionType == Constants.QuestionType.SAR || qs.QuestionType == Constants.QuestionType.MAC ||
                    qs.QuestionType == Constants.QuestionType.SAS || qs.QuestionType == Constants.QuestionType.SAP) && "" != qs.Question)
                {
                    writeString = writeString + strLeftSB + qs.Question + strRightSB;
                }
            }
            WriteRow += 2;
            TargetSheet.get_Range("D11", "K13").EntireRow.Copy(TargetSheet.Cells[WriteRow, 1]);// removed insert to qc3 
            TargetSheet.Cells[WriteRow, 2].Value = qs.QuestionType;
            TargetSheet.Cells[WriteRow, 4].Value = qs.QuestionNumber;
            TargetSheet.Cells[WriteRow /*+ 1*/, 8].Value = writeString;
            WriteRow += 4;
        }

        private void WriteSAMA(QuestionSettings qs)
        {
            string markBuf = ButtonCheckBox;
            if (Constants.AnswerType.SA == qs.AnswerType)
            {
                if (Constants.QuestionType.SAP == qs.QuestionType)
                {
                    markBuf = ButtonDropDown;
                }
                else
                {
                    markBuf = ButtonOption;
                }
            }

            string[,] pasteArray = new string[qs.CategoryCount, 3];
            Excel.Range outPutStartCell = TargetSheet.Cells[WriteRow, 6];
            for (int i = 0; i < qs.CategoryCount; i++)
            {
                pasteArray[i, 0] = markBuf;
                pasteArray[i, 1] = i + 1 + ".";
                pasteArray[i, 2] = qs.Choices[i];
            }
            outPutStartCell = outPutStartCell.Resize[qs.CategoryCount, 3];
            outPutStartCell.NumberFormat = "@";
            Excel.Range lineRng = outPutStartCell.Rows.Item[0].Resize(outPutStartCell.Rows.Count + 2, 5);
            lineRng.Borders.Item[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
            lineRng.Borders.Item[Excel.XlBordersIndex.xlInsideHorizontal].Weight = Excel.XlBorderWeight.xlThin;
            lineRng.Borders.Item[Excel.XlBordersIndex.xlInsideHorizontal].Color = Constants.Color.GreyBorder;

            outPutStartCell.Value = pasteArray;
            WriteRow += qs.CategoryCount;
        }
        /// <summary>
        /// Mathod to wriite SA, MA, FA or N question data to the output sheet
        /// </summary>
        /// <param name="qArray">List of Question Settings</param>
        private void WriteSAMAFAorN(List<QuestionSettings> qArray)
        {
            Dictionary<string, Dictionary<string, string>> dicAddFa;
            Dictionary<string, string> dicAddFaSub;
            dicAddFa = new Dictionary<string, Dictionary<string, string>>();
            QuestionSettings farstSetting = qArray[0];
            QuestionSettings qs;
            List<string> rowss = new List<string>();

            List<QuestionSettings> validQuestions = GetAllValidQuestions(qArray);
            bool isAllItemsValid = AllItemNumsAreValid(validQuestions);

            for (int i = 1; i < validQuestions.Count; i++)
            {
                qs = validQuestions[i];
                if (Constants.AnswerType.FA == qs.AnswerType || Constants.AnswerType.N == qs.AnswerType)
                {
                    if (isAllItemsValid)
                    {
                        string itemNum = qs.Variable.Substring(qs.Variable.LastIndexOf("_", qs.Variable.Length - 2) + 1);
                        itemNum = itemNum.Replace(Constants.AnswerType.FA, "");
                        itemNum = itemNum.Replace(Constants.AnswerType.N, "");

                        if (!Int32.TryParse(itemNum, out int index))
                        {
                            return;
                        }
                        if (!dicAddFa.ContainsKey(itemNum))
                        {
                            dicAddFaSub = new Dictionary<string, string>();
                            dicAddFa.Add(itemNum, dicAddFaSub);
                        }
                        else
                        {
                            dicAddFaSub = dicAddFa[itemNum];
                        }

                        dicAddFaSub.Add(qs.Variable, qs.AnswerType);
                        dicAddFa[itemNum] = dicAddFaSub;
                        dicAddFaSub = null;

                        Excel.Range r1 = TargetSheet.Cells[WriteRow, 4].End(Excel.XlDirection.xlUp).Offset(1, 3);
                        Excel.Range r2 = TargetSheet.Cells[WriteRow, 7];
                        Excel.Range r = TargetSheet.get_Range(r1, r2);
                        var rs = r.Value;
                        Excel.Range findRange = SCExcelClass.FIndCell(r, itemNum + ".");
                        rowss.Add(itemNum + ".");
                        var rslt = findRange.Value;
                        if (null == findRange)
                        {
                            return;
                        }
                        findRange.EntireRow.Cells[1, 9].Value = qs.Variable;
                        ChangeQFAorN(findRange, qs.AnswerType);
                    }
                    else
                    {
                        Regex regex1 = new Regex("^" + farstSetting.QuestionNumber + @"S\d+(FA|N)$");
                        if (regex1.IsMatch(qs.Variable))
                        {
                            string itemNum = qs.Variable.Trim();
                            itemNum = itemNum.Replace(Constants.AnswerType.FA, "");
                            itemNum = itemNum.Replace(Constants.AnswerType.N, "");

                            Excel.Range r1 = TargetSheet.Cells[WriteRow, 2].End(Excel.XlDirection.xlUp).Offset(1, 2);
                            Excel.Range r2 = TargetSheet.Cells[WriteRow, 4];
                            Excel.Range r = TargetSheet.get_Range(r1, r2);

                            Excel.Range findRange = SCExcelClass.FIndCell(r, itemNum);

                            if (null == findRange)
                            {
                                return;
                            }
                            findRange.EntireRow.Cells[1, 9].Value = qs.Variable;
                            ChangeQFAorN(findRange, qs.AnswerType);
                        }
                    }
                }
            }
            string wkStr;
            for (int i = 1; i < dicAddFa.Count; i++)
            {

                string itemNum = dicAddFa.ElementAt(i - 1).Key;
                dicAddFaSub = dicAddFa[itemNum];
                if (dicAddFaSub.Count > 1)
                {
                    wkStr = "";
                    for (int j = 1; j <= dicAddFaSub.Count; j++)
                    {
                        if (wkStr == "")
                        {
                            wkStr = dicAddFaSub.ElementAt(j - 1).Value;
                        }
                        else if (wkStr != dicAddFaSub.ElementAt(j - 1).Value)
                        {
                            wkStr = "FA/N";
                            break; //exit for
                        }
                    }
                }
                else
                {
                    wkStr = dicAddFaSub.ElementAt(0).Key;
                }
                Excel.Range r1 = TargetSheet.Cells[WriteRow, 4].End(Excel.XlDirection.xlUp).Offset(1, 3);
                Excel.Range r2 = TargetSheet.Cells[WriteRow, 7];
                Excel.Range r = TargetSheet.get_Range(r1, r2);
                Excel.Range findRange = SCExcelClass.FIndCell(r, itemNum + ".");

                if (null == findRange)
                {
                    return;
                }
                findRange.EntireRow.Cells[1, 9].Value = wkStr;
            }
        }

        /// <summary>
        /// Validate the Questions by checking the variable name
        /// </summary>
        /// <param name="qArray">List of Question Settings</param>
        /// <returns>return a new list containing only the valid <see cref="QuestionSettings"/> items.</returns>
        private List<QuestionSettings> GetAllValidQuestions(List<QuestionSettings> qArray)
        {
            List<QuestionSettings> result = new List<QuestionSettings>()
            {
                 qArray[0]
            };

            QuestionSettings qs = qArray[0];
            string regex = "^" + qs.QuestionNumber + @"(S\d+)?_\d+(FA|N)$";
            for (int i = 1; i < qArray.Count; i++)
            {
                qs = qArray[i];
                if (Regex.IsMatch(qs.Variable, regex, RegexOptions.IgnoreCase))
                {
                    result.Add(qs);
                }
            }
            return result;
        }

        /// <summary>
        /// Validate the Questions by checking the variable name
        /// </summary>
        /// <param name="qArray">List of Question Settings</param>
        /// <returns>return a bool value based on choices Valid or not</returns>
        private bool AllItemNumsAreValid(List<QuestionSettings> qArray)
        {
            bool isValid = true;
            QuestionSettings qs = qArray[0];
            int count = 0;
            if (qs.AnswerType == Constants.AnswerType.SA || qs.AnswerType == Constants.AnswerType.MA)
                count = qs.Choices.Count;
            else
                count = qArray.Count;
            for (int i = 1; i < qArray.Count; i++)
            {
                qs = qArray[i];
                string itemNum = qs.Variable.Substring(qs.Variable.LastIndexOf("_", qs.Variable.Length - 2) + 1);
                itemNum = itemNum.Replace(Constants.AnswerType.FA, "");
                itemNum = itemNum.Replace(Constants.AnswerType.N, "");
                int index = 0;
                if (!Int32.TryParse(itemNum, out index) || index > count)
                {
                    isValid = false;
                    break;
                }
            }
            return isValid;
        }

        /// <summary>
        /// Change Question using regular expression for FA or N type variable
        /// </summary>
        /// <param name="findRange">Range of Question to be update</param>
        /// <param name="ansType">Answer Type</param>
        private void ChangeQFAorN(Excel.Range findRange, string ansType)
        {
            Regex regex = new Regex("【( |　)*】");
            if (findRange.EntireRow.Cells[1, 8].Value != null)//Fix as per the Redmine Id:220732
                findRange.EntireRow.Cells[1, 8].Value = regex.Replace(findRange.EntireRow.Cells[1, 8].Value, "【" + ansType + "】");
        }

        private void WriteMT(List<QuestionSettings> qNoArray)
        {
            string[] wkArray;
            Dictionary<string, Dictionary<string, string>> dicAddFa;
            Dictionary<string, string> dicAddFaSub;
            string itemNum;
            Excel.Range findRange;
            string wkStr;
            QuestionSettings qs;
            QuestionSettings farstSetting;
            int qsCount = qNoArray[0].QuestionCount == null ? 1 : Convert.ToInt32(qNoArray[0].QuestionCount) == 0 ? 1 : Convert.ToInt32(qNoArray[0].QuestionCount);

            dicAddFa = new Dictionary<string, Dictionary<string, string>>();

            Excel.Range tmpRange = TargetSheet.Cells.Item[WriteRow, 6];
            tmpRange.NumberFormat = "@";
            tmpRange.Value = CommonResource.QS_QUESTIONIRE_TITLE6;

            WriteRow += 1;

            farstSetting = qNoArray[0];

            for (int i = 0; i < qsCount; i++)
            {
                qs = qNoArray[i];
                switch (qs.AnswerType)
                {
                    case Constants.AnswerType.SA:
                    case Constants.AnswerType.MA:
                        if (farstSetting.QuestionType != Constants.QuestionType.MTT)
                        {
                            TargetSheet.Rows[WriteRow].Insert();
                            tmpRange = TargetSheet.Cells.Item[WriteRow, 4];
                            tmpRange.NumberFormat = "@";
                            tmpRange.Value = qs.Variable;
                            tmpRange = TargetSheet.Cells.Item[WriteRow, 6].Resize[1, 5];//[,5]
                            tmpRange.NumberFormat = "@";
                            TargetSheet.Cells[WriteRow, 7].Value = i + 1 + ".";
                            TargetSheet.Cells[WriteRow, 8].Value = qs.Question; //CHANGE
                            tmpRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin);///Change may
							tmpRange.Borders.Item[Excel.XlBordersIndex.xlEdgeTop].Color = Constants.Color.GreyBorder;
                            tmpRange.Borders.Item[Excel.XlBordersIndex.xlEdgeRight].Color = Constants.Color.GreyBorder;
                            tmpRange.Borders.Item[Excel.XlBordersIndex.xlEdgeLeft].Color = Constants.Color.GreyBorder;
                            tmpRange.Borders.Item[Excel.XlBordersIndex.xlEdgeBottom].Color = Constants.Color.GreyBorder;
                            tmpRange.Interior.Color = Constants.Color.InteriorColor;//&HF9EFD7
                            WriteRow += 1;
                        }
                        else
                        {
                            wkArray = qs.Question.Split('\n');
                            TargetSheet.Rows[WriteRow].Resize(wkArray.GetUpperBound(0) + 1).EntireRow().Insert();
                            tmpRange = TargetSheet.Cells.Item[WriteRow, 4];
                            tmpRange.NumberFormat = "@";
                            tmpRange.Value = qs.Variable;
                            tmpRange = TargetSheet.Cells.Item[WriteRow, 6].Resize[wkArray.Count(), 5];
                            tmpRange.NumberFormat = "@";
                            TargetSheet.Cells[WriteRow, 7].Value = i + 1 + ".";
                            //TargetSheet.Cells[WriteRow, 8].Value = qs.Question.Trim();
                            for (int j = 0; j < wkArray.Count(); j++)
                            {
                                TargetSheet.Cells[WriteRow + j, 8].Value = wkArray[j].Trim();
                            }
                            Excel.Range tmp = TargetSheet.Cells[WriteRow, 8].Resize[wkArray.Count() + 1, 3];
                            tmp.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
                            tmp.Borders[Excel.XlBordersIndex.xlInsideHorizontal].Weight = Excel.XlBorderWeight.xlHairline;
                            tmp.Borders[Excel.XlBordersIndex.xlInsideHorizontal].Color = Constants.Color.GreyBorder;
                            tmpRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin);
                            //change
                            tmpRange.Borders.Item[Excel.XlBordersIndex.xlEdgeTop].Color = Constants.Color.GreyBorder;
                            tmpRange.Borders.Item[Excel.XlBordersIndex.xlEdgeRight].Color = Constants.Color.GreyBorder;
                            tmpRange.Borders.Item[Excel.XlBordersIndex.xlEdgeLeft].Color = Constants.Color.GreyBorder;
                            tmpRange.Borders.Item[Excel.XlBordersIndex.xlEdgeBottom].Color = Constants.Color.GreyBorder;
                            //
                            tmpRange.Interior.Color = Constants.Color.InteriorColor;//&HF9EFD7
                            WriteRow += wkArray.Count();
                        }
                        if (i == qsCount - 1)
                        {
                            tmpRange = TargetSheet.Cells[WriteRow + 1, 6];
                            tmpRange.NumberFormat = "@";
                            tmpRange.Value = CommonResource.QS_QUESTIONIRE_TITLE7;
                            WriteRow += 2;
                            WriteSAMA(qs);
                        }
                        break;
                    default:
                        Regex regex = new Regex("^" + farstSetting.QuestionNumber + @"S\d+(FA|N)$");
                        if (regex.IsMatch(qs.Variable))
                        {
                            itemNum = qs.Variable.Replace(Constants.AnswerType.FA, "");
                            itemNum = qs.Variable.Replace(Constants.AnswerType.N, "");

                            Excel.Range r1 = TargetSheet.Cells[WriteRow, 2].End(Excel.XlDirection.xlUp).Offset(1, 2);
                            Excel.Range r2 = TargetSheet.Cells[WriteRow, 4];
                            Excel.Range r = TargetSheet.get_Range(r1, r2);
                            findRange = SCExcelClass.FIndCell(r, itemNum);

                            if (null == findRange)
                            {
                                return;
                            }
                            findRange.EntireRow.Cells[1, 9].Value = qs.Variable;
                            ChangeQFAorN(findRange, qs.AnswerType);
                        }
                        else
                        {
                            regex = new Regex("^" + farstSetting.QuestionNumber + @"(S\d+)?_\d+(FA|N)$", RegexOptions.IgnoreCase);
                            if (regex.IsMatch(qs.Variable))
                            {
                                itemNum = qs.Variable.Substring(qs.Variable.LastIndexOf("_", qs.Variable.Length - 2) + 1);
                                itemNum = itemNum.Replace(Constants.AnswerType.FA, "");
                                itemNum = itemNum.Replace(Constants.AnswerType.N, "");

                                if (!Int32.TryParse(itemNum, out int index))
                                {
                                    return;
                                }

                                if (!dicAddFa.ContainsKey(itemNum))
                                {
                                    dicAddFaSub = new Dictionary<string, string>();
                                    dicAddFa.Add(itemNum, dicAddFaSub);
                                }
                                else
                                {
                                    dicAddFaSub = dicAddFa[itemNum];
                                }

                                dicAddFaSub.Add(qs.Variable, qs.AnswerType);
                                dicAddFa[itemNum] = dicAddFaSub;
                                dicAddFaSub = null;

                                if (Regex.IsMatch(qs.Variable, "^" + farstSetting.QuestionNumber + @"_\d+(FA|N)$"))
                                {
                                    Excel.Range r1 = TargetSheet.Cells[WriteRow + farstSetting.CategoryCount + 1, 7];
                                    Excel.Range r2 = TargetSheet.Cells[WriteRow, 7];
                                    Excel.Range r = TargetSheet.get_Range(r1, r2);
                                    findRange = SCExcelClass.FIndCell(r, itemNum + ".");

                                    if (null == findRange)
                                    {
                                        return;
                                    }
                                    ChangeQFAorN(findRange, qs.AnswerType);
                                }
                            }
                        }
                        break;
                }
            }
            //WriteRow += farstSetting.CategoryCount + 2;
            //選択肢付属FA、セル付属FA
            for (int i = 1; i < dicAddFa.Count; i++)
            {
                itemNum = dicAddFa.ElementAt(i - 1).Key;
                dicAddFaSub = dicAddFa[itemNum];
                if (dicAddFaSub.Count > 1)
                {
                    wkStr = "";
                    for (int j = 1; j < dicAddFaSub.Count; j++)
                    {
                        if (wkStr == "")
                        {
                            wkStr = dicAddFaSub.ElementAt(j - 1).Value;
                        }
                        else if (wkStr != dicAddFaSub.ElementAt(j - 1).Value)
                        {
                            wkStr = "FA/N";
                            break; //exit for
                        }
                    }
                }
                else
                {
                    wkStr = dicAddFaSub.ElementAt(0).Key;
                }
                Excel.Range r1 = TargetSheet.Cells[WriteRow, 4].End(Excel.XlDirection.xlUp).Offset(1, 3);
                Excel.Range r2 = TargetSheet.Cells[WriteRow, 7];
                Excel.Range r = TargetSheet.get_Range(r1, r2);
                findRange = SCExcelClass.FIndCell(r, itemNum + ".");

                if (null == findRange)
                {
                    return;
                }
                findRange.EntireRow.Cells[1, 9].Value = wkStr;
            }
        }

        private void WriteRATFAS(List<QuestionSettings> qNoArray)
        {
            string[,] pasteArray = new string[qNoArray.Count, 4];
            int qCount = qNoArray[0].QuestionCount == null ? 1 : Convert.ToInt32(qNoArray[0].QuestionCount) == 0 ? 1 : Convert.ToInt32(qNoArray[0].QuestionCount);
            int trimPos;
            if (qCount == 0)
            {
                qCount = 1;
            }
            for (int i = 0; i < qCount; i++)
            {
                pasteArray[i, 0] = i + 1 + ".";
                pasteArray[i, 1] = qNoArray[i].Question;
                pasteArray[i, 2] = qNoArray[i].Variable;
                if (qNoArray[0].QuestionType == Constants.QuestionType.RAT)
                {
                    trimPos = pasteArray[i, 1].LastIndexOf("】", pasteArray[i, 1].Length - 2) + 1;
                    if (trimPos > 0)
                    {
                        pasteArray[i, 3] = pasteArray[i, 1].Substring(trimPos + 1);
                    }

                    Regex regex = new Regex("【( |　)*】" + pasteArray[i, 2] + "$");//3 --- 2
                    pasteArray[i, 1] = regex.Replace(pasteArray[i, 1], "");
                }
                else
                {
                    Regex regex = new Regex("【( |　)*】");
                    if (pasteArray[i, 1] != null)//Fix as per the Redmine Id:220732
                        pasteArray[i, 1] = regex.Replace(pasteArray[i, 1], "【" + qNoArray[i].AnswerType + "】");
                }
            }
            Excel.Range tempCell = TargetSheet.Cells[WriteRow, 6];
            Excel.Range lineRng;
            tempCell = tempCell.Resize[qCount, 5];
            tempCell.NumberFormat = "@";
            lineRng = tempCell.Rows.Item[0].Resize[tempCell.Rows.Count + 2];
            Excel.Range outPutStartCell = lineRng.Offset[1, 1].Resize[qCount, 3]; //tempCell.get_Range("B1");
            lineRng.Borders.Item[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
            lineRng.Borders.Item[Excel.XlBordersIndex.xlInsideHorizontal].Weight = Excel.XlBorderWeight.xlThin;
            lineRng.Borders.Item[Excel.XlBordersIndex.xlInsideHorizontal].Color = Constants.Color.GreyBorder;
            outPutStartCell.Value = pasteArray;
            WriteRow += qCount;
        }

        private void WriteFAs(QuestionSettings qs)
        {
            TargetSheet.Cells[WriteRow, 9].Value = qs.Variable;
            TargetSheet.Cells[WriteRow, 8].Resize[3, 2].BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin);
            WriteRow += 3;
        }

        private bool JudgeSameQuestion(QuestionSettings qs, List<QuestionSettings> qArray)
        {
            Regex rgx;
            QuestionSettings previousQuestion = qArray[0];
            if (qs.QuestionNumber == previousQuestion.QuestionNumber + "FA" && qs.QuestionType == "FAS")
            {
                string regex = "^" + previousQuestion.QuestionNumber + @"S\d+_\d+(FA|N)$";
                if (Regex.IsMatch(qs.Variable, regex, RegexOptions.IgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
