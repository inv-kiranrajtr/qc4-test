using log4net;
using Microsoft.Office.Interop.Excel;
using QC4Common.Model;
using QC4Common.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Constant = QC4Common.Common.Constants;
using ExcelUt = QC4Common.Util.ExcelUtil;
using MsgDialog = QC4Common.Common.MessageDialog;
using ExcelAddIn;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelAddIn.Common
{
    internal class QSSheetChange
    {
        static AppHelper appHelper = new AppHelper();
        //used to update varible name in variable edit mdoe
        static private Dictionary<int, string> updateList = new Dictionary<int, string>();
        static private List<Range> deletedList = new List<Range>();
        static private bool isVarEdited = false;
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public static Dictionary<int, string> GetUpdateList(out bool isEdited)
        {
            isEdited = isVarEdited;
            return updateList;
        }


        public static void ClearUpdateList()
        {
            isVarEdited = false;
            updateList.Clear();
        }

        internal static void QSValueChange(Worksheet sheet, Range target)
        {
            Definitions.isQsUpdated = true;
            if (target.EntireRow.Address == target.Address)
            {
                long tRows = sheet.UsedRange.Rows.Count;
                if (tRows > Definitions.QsRowCount)
                {
                    Definitions.QsRowCount = tRows;
                    return;
                }
                else if (tRows < Definitions.QsRowCount)
                {
                    string rangeAddress = target.Address;
                    // Temporarily disable Excel events to prevent undesired side effects during undo operation
                    sheet.Application.EnableEvents = false;
                    // Undo the last action in Excel
                    sheet.Application.Undo();
                    // Re-enable Excel events
                    sheet.Application.EnableEvents = true;

                    // Retrieve the range using the stored address
                    Excel.Range range = sheet.Range[rangeAddress];
                    QS.QSHelper.GetQuestionFlag(range, out bool isOrg, out bool isAnImp);
                    if (isOrg)
                    {
                        //sheet.Application.EnableEvents = false;
                        //sheet.Application.Undo();
                        //sheet.Application.EnableEvents = true;
                        MessageDialog.ErrorOk(AddinResource.QS_ALERT_ORG_DELETE);
                    }
                    else if (isAnImp)
                    {
                        DialogResult result = MessageBox.Show(AddinResource.QS_ALERT_AN_IMP_ROW_DELETE, "QuickCross", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        if (result == DialogResult.OK)
                        {
                            sheet.Application.EnableEvents = false;
                            sheet.Application.Repeat();
                            sheet.Application.EnableEvents = true;
                            Definitions.QsRowCount = tRows;
                        }

                    }
                    else
                    {
                        sheet.Application.EnableEvents = false;
                        sheet.Application.Repeat();
                        sheet.Application.EnableEvents = true;
                        Definitions.QsRowCount = tRows;
                    }
                    sheet.Application.EnableEvents = true;
                }
                return;

            }


            if (Definitions.VariableEditMode)
            {
                ItemChangeVariableMode(sheet, target);
                return;
            }

            bool inputSupport = true;
            int columnCount = target.Columns.Count;
            if (columnCount > 1)
            {
                return;
                //foreach (Range c in target.Columns)
                //{
                //	if (c.Column == Constant.QS.QsColItem)
                //	{
                //		inputSupport = false;
                //		break;
                //	}
                //}
            }

            AppHelper appHelper = new AppHelper();
            appHelper.ExcelSet(calculationMode: false, targetApp: sheet.Application);

            try
            {
                foreach (Range targetCell in target)
                {
                    if (targetCell.Row < 4)
                    {
                        continue;
                    }

                    switch (targetCell.Column)
                    {
                        case Constant.QS.QsColNew:
                        case Constant.QS.QsColQuestionNumber:
                            QSChangeQuestionNumber(sheet, targetCell);
                            break;
                        case Constant.QS.QsColQuestiontype:
                            QSChangeQuestionType(sheet, targetCell);
                            break;
                        case Constant.QS.QsColNumberOfQuestion:
                            QSChangeNumberOfQuestion(sheet, targetCell);
                            break;
                        case Constant.QS.QsColItem:
                            QSChangeVariable(sheet, targetCell, inputSupport);
                            break;
                        case Constant.QS.QsColAnswerType:
                            QSChangeAnswerType(sheet, targetCell);
                            break;
                        case Constant.QS.QsColCategories:
                            QSChangeCategories(sheet, targetCell);
                            break;
                        case Constant.QS.QsColWT:
                            QSChangeWT(sheet, targetCell);
                            break;
                        case Constant.QS.QsColSortDisplay:
                            QSChangeSort(sheet, targetCell);
                            break;
                        case Constant.QS.QsColTableHeading:
                            QSChangeTableHeading(sheet, targetCell);
                            break;
                        case Constant.QS.QsColQuestion:
                            QSChangeQuestion(sheet, targetCell);
                            break;
                        case Constant.QS.QsColCountBase:
                            QSChangeCountBase(sheet, targetCell);
                            break;
                        case Constant.QS.QsColAddSunTotal:
                            QSChangeAddSubTotal(sheet, targetCell);
                            break;
                        case Constant.QS.QsColNumberSubTotal:
                            QSChangeNumberSubTotal(sheet, targetCell);
                            break;
                        case Constant.QS.QsColCount:
                        case Constant.QS.QsColcriteria1:
                        case Constant.QS.QsColcriteria2:
                        case Constant.QS.QsColcriteria3:
                        case Constant.QS.QsColcriteria4:
                        case Constant.QS.QsColcriteria5:
                        case Constant.QS.QsColcriteria6:
                        case Constant.QS.QsColcriteria7:
                        case Constant.QS.QsColcriteria8:
                        case Constant.QS.QsColcriteria9:
                        case Constant.QS.QsColcriteria10:
                            QSChangeCount(sheet, targetCell);
                            break;
                        case Constant.QS.QsColSubtotal1:
                        case Constant.QS.QsColSubtotal2:
                        case Constant.QS.QsColSubtotal3:
                        case Constant.QS.QsColSubtotal4:
                        case Constant.QS.QsColSubtotal5:
                        case Constant.QS.QsColSubtotal6:
                        case Constant.QS.QsColSubtotal7:
                        case Constant.QS.QsColSubtotal8:
                        case Constant.QS.QsColSubtotal9:
                        case Constant.QS.QsColSubtotal10:
                            targetCell.Next.Select();
                            break;
                        default:
                            Range range = GetCategoriesCell(targetCell);
                            int catCount = range.Text == "" ? 0 : Convert.ToInt32(range.Text);
                            if (targetCell.Column >= Constant.QS.QsColChoiceBegin && targetCell.Column < Constant.QS.QsColChoiceBegin - 1 + catCount)
                            {
                                targetCell.Next.Select();
                            }
                            else if (targetCell.Column == Constant.QS.QsColChoiceBegin - 1 + catCount)
                            {
                                GetVariableCell(targetCell).Offset[1, 0].Select();
                            }
                            break;
                    }
                }
                QSDeleteRows();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
            finally
            {
                appHelper.ExcelReset(true, targetApp: sheet.Application);
            }
        }


        public static void DeleteQSRow(Worksheet sheet, Range range, out bool isDeleted)
        {
            Excel.Range rangeRow = range.EntireRow;
            bool isRow = false;
            bool isVarRow = false;
            isDeleted = false;

            if (range.Address == rangeRow.Address)
            {
                isRow = true;
            }
            if (range.Row <= 3)
            {
                MessageDialog.ErrorOk(AddinResource.ALERT_HEADER_DELETE);
            }

            if (!isRow)
            {
                isVarRow = QSUtil.IsVariableColumnFound(range);
            }

            QS.CommandBar.GetQuestionFlag(rangeRow, out bool isOrg, out bool isAnImp);

            if (isOrg)
            {
                if (isRow || isVarRow)
                    QSUtil.OrgDelete();

            }
            else if (isAnImp)
            {
                if (isRow)
                {
                    QSUtil.AnImpRowDelete(range);
                    isDeleted = true;
                }
                else
                {
                    sheet.Application.EnableEvents = false;
                    sheet.Application.Repeat();
                    sheet.Application.EnableEvents = true;
                }
                Definitions.isQsUpdated = true;
            }
            else
            {
                if (isRow)
                {
                    QS.CommandBar.DeleteRow(range);
                    isDeleted = true;
                }
                else
                {
                    sheet.Application.EnableEvents = false;
                    sheet.Application.Repeat();
                    sheet.Application.EnableEvents = true;
                }
                Definitions.isQsUpdated = true;
            }
        }
        private static void ItemChangeVariableMode(Worksheet sheet, Range targetCells)
        {
            foreach (Range targetCell in targetCells)
            {
                QuestionSettings qs = Definitions.VariableDictionary.Select(v => v.Value).Where(q => q.RowNumber == targetCell.Row).First();
                if (qs == null)
                {
                    return;
                }

                if (string.IsNullOrEmpty(targetCell.Value))
                {
                    MessageDialog.ErrorOk(string.Format(AddinResource.QS_VARIABLE_NULL_NOT_ALLOWED, targetCell.Row.ToString()));
                    targetCell.Value = qs.Variable;
                    return;
                }
                try
                {
                    isVarEdited = true;
                    Name name = targetCell.EntireRow.Name;
                    string str = name.Name;
                    if (QSUtil.IsRowName(str))
                    {
                        int x = Convert.ToInt32(str.Substring(str.Length - 5));
                        if (updateList.ContainsKey(x))
                        {
                            updateList[x] = targetCell.Text;
                        }
                        else
                        {
                            updateList.Add(x, targetCell.Text);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                }
            }
        }

        private static void QSChangeQuestionType(Worksheet sheet, Range targetCell)
        {
            targetCell.Next.Select();
        }

        private static void QSChangeQuestionNumber(Worksheet sheet, Range targetCell)
        {
            Range selectRange=null;
            string text = targetCell.Text;
            if (!QSUtil.ValidatedQuestionNumber(text, out string message))
            {
                MsgDialog.ErrorOk(message);
                selectRange = targetCell;
                selectRange.Select();
            }
            else
            {
                targetCell.Next.Select();
            }


        }

        private static void QSChangeNumberOfQuestion(Worksheet sheet, Range targetCell)
        {
            if (!String.IsNullOrEmpty(targetCell.Text))
            {
                if (!Int32.TryParse(targetCell.Text, out int count) || count > 200 || count < 1)
                {
                    MessageDialog.ErrorOk(String.Format(AddinResource.SET_INETGER_BETWEEN, 1, 200));
                    targetCell.Select();
                    return;
                }
            }
            targetCell.Next.Select();
        }

        private static void QSChangeVariable(Worksheet sheet, Range targetCell, bool inputSupport = false)
        {
            if (!inputSupport)
            {
                return;
            }

            Object obj = targetCell.Value2;
            int id = QSUtil.GetQSRowId(targetCell.EntireRow);
            if (obj != null && String.IsNullOrEmpty(obj.ToString()))
            {
                MsgDialog.ErrorOk(AddinResource.QS_VARIABLE_SPECIAL_CHARACTER);
                Range cell = targetCell.Offset[0, -4];
                if (id == -1 && String.IsNullOrEmpty(cell.Text))
                {
                    cell.Value = Constant.QuestionFlag.New;
                }
                return;
            }
            string text = targetCell.Text;
            if (text == "WeightBack")
            {
                MessageDialog.ErrorOk(AddinResource.QS_ALERT_WEIGHTBACK);
                //targetCell.ClearContents();
                QC4Common.Util.ExcelUtil.ClearContents(targetCell);
            }
            Range selectRange = targetCell.Offset[0, 1];
            string questionFlag = "New";
            if (Definitions.IdFlagDictionary.ContainsKey(id))
            {
                questionFlag = Definitions.IdFlagDictionary[id];
            }

            if (String.IsNullOrEmpty(text))
            {
                switch (questionFlag)
                {
                    case Constant.QuestionFlag.An:
                    case Constant.QuestionFlag.Imp:
                        QSUtil.AnImpRowDelete(targetCell);
                        break;
                    case Constants.QuestionFlag.Org:
                        MsgDialog.ErrorOk(AddinResource.QS_ALERT_ORG_DELETE);
                        break;
                    case Constant.QuestionFlag.New:
                        targetCell.EntireRow.Delete();
                        break;
                }
                return;
            }

            Range start = sheet.Cells[Constant.QS.QsRowDataStart,Constant.QS.QsColItem];
            Range end = sheet.Cells[sheet.Cells.SpecialCells(XlCellType.xlCellTypeLastCell, Type.Missing).Row, Constant.QS.QsColItem];
            Range total = sheet.Range[start, end];
            Range findCell = total.Find(text, Type.Missing, XlFindLookIn.xlValues, XlLookAt.xlWhole,
                        XlSearchOrder.xlByRows, XlSearchDirection.xlNext, false, Type.Missing,false);

            if (findCell!=null)
            {
                string val = findCell.Text;
                int row = findCell.Row;
                if (val.ToLower()!=text.ToLower())
                {
                    int rowstart = Constant.QS.QsRowDataStart;
                    int rowend = end.Row;
                    for(int inc=rowstart;inc<=rowend;inc++)
                    {
                        findCell = sheet.Cells[inc, Constant.QS.QsColItem];
                        val = findCell.Text;
                        if (val.ToLower() == text.ToLower())
                            break;
                    }
                }
            }
            if (findCell != null && targetCell.Address == findCell.Address)
            {
                findCell = total.FindNext(findCell);
                if (targetCell.Address == findCell.Address)
                {
                    findCell = null;
                }
            }

            Range flagCell = targetCell.Offset[0, -4];
            bool evnt = targetCell.Application.EnableEvents;
            if (targetCell.Application.EnableEvents == true)
            {
                targetCell.Application.EnableEvents = false;
            }
            if (id == -1 && String.IsNullOrEmpty(flagCell.Text))
            {
                flagCell.Value = Constant.QuestionFlag.New;
            }

            if (null != findCell)
            {

                flagCell.Value = Constant.QuestionFlag.New;
                targetCell.Offset[0, -3].Resize[1, Constant.QS.QsColcriteria9].Value = findCell.Offset[0, -3].Resize[1, Constant.QS.QsColcriteria9].Value;
                Regex regex = new Regex(@"^N(\d+)(.*)");
                Match match = regex.Match(text);
                if (match.Success)
                {
                    targetCell.Value = GenerateVariableName(match.Groups[2].Value, targetCell, total, Convert.ToInt32(match.Groups[1].Value) + 1);
                }
                else
                {
                    regex = new Regex(@"^N(.*)");
                    match = regex.Match(text);
                    if (match.Success)
                    {
                        targetCell.Value = GenerateVariableName(match.Groups[1].Value, targetCell, total, 1);
                    }
                    else
                    {
                        targetCell.Value = GenerateVariableName(text, targetCell, total);
                    }
                }
                targetCell.Offset[0, -1].Value = String.Empty;
                if (Int32.TryParse(targetCell.Offset[0, 2].Text, out int count))
                {
                    ExcelUt.SetCellInteriorColor(GetChoiceStartCells(targetCell).Resize[1, count], Constant.Color.LightGrey);
                }
                targetCell.Application.EnableEvents = evnt;
            }

            text = targetCell.Text;
            if (!QSUtil.ValidateVariable(text, out string message))
            {
                MsgDialog.ErrorOk(message);
                selectRange = targetCell;
            }
            selectRange.Select();
        }
        /// <summary>
        /// Clear the QS row based on variable Answer type
        /// </summary>
        /// <param name="sheet">QS sheet</param>
        /// <param name="targetCell">Current selected cellx</param>
        private static void QSChangeAnswerType(Worksheet sheet, Range targetCell)
        {
            Range selectRange = targetCell.Next;
            string str = targetCell.Text;
            if ((Constant.AnswerType.N == str || Constant.AnswerType.FA == str || Constant.AnswerType.D == str))
            {
                Range categoriesCell = targetCell.Next;
                //clear the cell color and all even if the category cell is empty
                {
                    QC4Common.Util.ExcelUtil.ClearContents(categoriesCell);
                    QSChangeCategories(sheet, categoriesCell);
                }
                CommonFunctions.CellFormatInitialize(sheet.get_Range(GetCountCell(targetCell), GetLastQsCol(targetCell)));
                selectRange = targetCell.Offset[0, 4];
            }
            selectRange.Select();
        }

        private static void QSChangeCategories(Worksheet sheet, Range targetCell)
        {
            Range r1;
            Range r2;
            Range selectRange = targetCell;
            int colBegin = Constant.QS.QsColChoiceBegin;
            int colEnd = Constant.QS.QsColChoiceEnd;
            if (Int32.TryParse(targetCell.Text, out int count) && count > 0)
            {
                if (count > 1000)
                {
                    count = 1000;
                }
                r1 = sheet.Cells[targetCell.Row, Constant.QS.QsColChoiceBegin];
                r2 = sheet.Cells[targetCell.Row, Constant.QS.QsColChoiceBegin + count - 1];
                ExcelUt.SetCellInteriorColor(sheet.get_Range(r1, r2), Constant.Color.LightGrey);
                colBegin += count;
                selectRange = selectRange.Next;
            }

            if (colBegin < colEnd)
            {
                r1 = sheet.Cells[targetCell.Row, colBegin];
                r2 = sheet.Cells[targetCell.Row, colEnd];
                CommonFunctions.CellFormatInitializeForQSCatChange(sheet.get_Range(r1, r2));
            }
            QSChangeWT(sheet, GetWTCell(targetCell));
            selectRange.Select();
        }

        private static void QSDeleteRows()
        {
            deletedList = deletedList.OrderByDescending(l => l.Row).ToList();
            foreach (Range range in deletedList)
            {
                range.EntireRow.Delete();
            }
        }

        //To generate new variable name like N1 , N2 , ,,,, N100
        private static string GenerateVariableName(string name, Range targetCell, Range totalRange, int times = 0)
        {
            string str = "N" + (times == 0 ? "" : times.ToString()) + name;
            Range findCell = totalRange.Find(str, Type.Missing, XlFindLookIn.xlValues, XlLookAt.xlWhole,
                        XlSearchOrder.xlByRows, XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);

            if (findCell != null && targetCell.Address == findCell.Address)
            {
                findCell = findCell.FindNext(findCell);
                if (targetCell.Address == findCell.Address)
                {
                    findCell = null;
                }
            }

            if (findCell != null)
            {
                str = GenerateVariableName(name, targetCell, totalRange, times + 1);
            }
            return str;
        }

        private static void QSChangeWT(Worksheet sheet, Range targetCell)
        {
            Range selectRange = targetCell.Next;
            string score = targetCell.Text;
            if (String.IsNullOrEmpty(score))
            {
                return;
            }

            Range catCell = GetCategoriesCell(targetCell);
            if (!Int32.TryParse(catCell.Text,out int count))
            {
                count = 0;
            }

            if (count == 0 || score == "")
            {
                //targetCell.ClearContents();
                QC4Common.Util.ExcelUtil.ClearContents(targetCell);
                return;
            }

            string[] s = score.Split(',');
            score = "";
            bool alert = false;
            for (int i = 0; i < count; i++)
            {
                if (s.Length > i && !string.IsNullOrEmpty(s[i]))
                {
                    if (Double.TryParse(s[i], out double n))
                    {
                        score += s[i];
                    }
                    else
                    {
                        score += "0";
                        alert = true;
                    }
                }
                score += ",";
            }
            targetCell.Value = score.Substring(0, score.Length - 1);

            if (alert)
            {
                MessageDialog.ErrorOk(string.Format(AddinResource.QS_SCORE_INVALID_VALUE));
                selectRange = targetCell;
            }
            selectRange.Select();
        }

        private static void QSChangeSort(Worksheet sheet, Range targetCell)
        {
            try
            {
                targetCell.Next.Select();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        private static void QSChangeTableHeading(Worksheet sheet, Range targetCell)
        {
            targetCell.Next.Select();
        }

        private static void QSChangeQuestion(Worksheet sheet, Range targetCell)
        {
            Range range = GetAnswerCell(targetCell);
            Range selectRange = targetCell.Next;
            if (Constant.AnswerType.N == range.Text || Constant.AnswerType.FA == range.Text)
            {
                selectRange = GetVariableCell(targetCell).Offset[1, 0];
            }

            selectRange.Select();
        }

        private static void QSChangeCount(Worksheet sheet, Range targetCell)
        {
            targetCell.Font.Color = Constant.Color.Black;
            Range CategoriesCell = GetCategoriesCell(targetCell);
            Int32.TryParse(CategoriesCell.Text, out int count);
            if (!QC4Common.Validation.NumberCheck.NUmberCheckSubtotal(targetCell.Text, count))
            {
                MessageDialog.ErrorOk(string.Format(AddinResource.SET_INETGER_BETWEEN, 1, count));
                targetCell.Font.Color = Constant.Color.Red;
            }
            targetCell.Next.Select();
        }

        private static void QSChangeCountBase(Worksheet sheet, Range targetCell)
        {
            if (!String.IsNullOrEmpty(targetCell.Text))
            {
                if (targetCell.Text != "Lower")
                {
                    MessageDialog.ErrorOk(AddinResource.QS_ALERT_INVALID_COUNTBASE);
                }
            }
            targetCell.Next.Select();
        }

        private static void QSChangeAddSubTotal(Worksheet sheet, Range targetCell)
        {
            if (!String.IsNullOrEmpty(targetCell.Text))
            {
                if (targetCell.Text != "1")
                {
                    MessageDialog.ErrorOk(AddinResource.QS_ALERT_INVALID_ADDSUBTOTAL);
                }
            }
            IgnoreError(targetCell);
            targetCell.Next.Select();
        }


        private static void QSChangeNumberSubTotal(Worksheet sheet, Range targetCell)
        {
            Range r1;
            Range r2;
            int colBegin = Constant.QS.QsColSubtotal1;
            int colEnd = Constant.QS.QsColcriteria10;

            int initalVal = 0;
            if (Int32.TryParse(targetCell.Text, out int count))
            {
                initalVal = count;
                if (count < 1)
                {
                    count = 0;
                }

                if (count > 10)
                {
                    count = 10;
                }
                r1 = sheet.Cells[targetCell.Row, Constant.QS.QsColSubtotal1];
                r2 = sheet.Cells[targetCell.Row, Constant.QS.QsColSubtotal1 + (count * 2) - 1];
                ExcelUt.SetCellInteriorColor(sheet.get_Range(r1, r2), Constant.Color.LightGrey);
                colBegin += (count * 2);
            }

            if (colBegin < colEnd)
            {
                r1 = sheet.Cells[targetCell.Row, colBegin];
                r2 = sheet.Cells[targetCell.Row, colEnd];
                CommonFunctions.CellFormatInitialize(sheet.get_Range(r1, r2));
            }
            IgnoreError(targetCell);
            if (String.IsNullOrEmpty(targetCell.Offset[0, -1].Text) && String.IsNullOrEmpty(targetCell.Text))
            {
                return;
            }

            if (initalVal < 1 || initalVal > 10)
            {
                MessageDialog.ErrorOk(String.Format(AddinResource.SET_INETGER_BETWEEN, 1, 10));
            }
        }

        private static void IgnoreError(Range range)
        {
            if (range.Errors[XlErrorChecks.xlNumberAsText].Value)
            {
                try
                {
                    range.Errors.Item[XlErrorChecks.xlNumberAsText].Ignore = true;
                }
                catch { }
            }
        }

        private static Range GetVariableCell(Range targetCell) => targetCell.Worksheet.Cells[targetCell.Row, Constant.QS.QsColItem];
        private static Range GetAnswerCell(Range targetCell) => targetCell.Worksheet.Cells[targetCell.Row, Constant.QS.QsColAnswerType];
        private static Range GetNewCell(Range targetCell) => targetCell.Worksheet.Cells[targetCell.Row, Constant.QS.QsColNew];
        private static Range GetCategoriesCell(Range targetCell) => targetCell.Worksheet.Cells[targetCell.Row, Constant.QS.QsColCategories];
        private static Range GetWTCell(Range targetCell) => targetCell.Worksheet.Cells[targetCell.Row, Constant.QS.QsColWT];
        private static Range GetQuestionTypeCell(Range targetCell) => targetCell.Worksheet.Cells[targetCell.Row, Constant.QS.QsColQuestiontype];
        private static Range GetAnswerCells(Range targetCell) => targetCell.Worksheet.get_Range(Constant.QS.QsColCategoriesBegin + targetCell.Row, Constant.QS.QsColCategoriesBegin + targetCell.Row);
        private static Range GetChoiceStartCells(Range targetCell) => targetCell.Worksheet.Cells[targetCell.Row, Constant.QS.QsColChoiceBegin];
        private static Range GetSubTotalStartCells(Range targetCell) => targetCell.Worksheet.Cells[targetCell.Row, Constant.QS.QsColSubtotal1];
        private static Range GetSubTotalCount(Range targetCell) => targetCell.Worksheet.Cells[targetCell.Row, Constant.QS.QsColNumberSubTotal];
        private static Range GetCountCell(Range targetCell) => targetCell.Worksheet.Cells[targetCell.Row, Constant.QS.QsColCount];
        private static Range GetLastQsCol(Range targetCell) => targetCell.Worksheet.Cells[targetCell.Row, Constant.QS.QsColcriteria10];
    }
}
