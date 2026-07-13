using ExcelAddIn;
using ExcelAddIn.Common;
using ExcelAddIn.Sheets;
using Macromill.QCWeb.Common;
using QC4Common.DB;
using QC4Common.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Qc4Launcher.Forms.DP_Main.RECODE;
using Excel = Microsoft.Office.Interop.Excel;

namespace Qc4Launcher.Logic
{
    public class DataProcessHelper
    {
        public bool WriteProcess(Excel.Workbook workbook,
                                 string processingType, string variable, string answertype, string question, int choiceindex, DataTable data,
                                 string command, string[,] instructionarray, bool isnewquestion, int stdwriterow, string stdprocessingoption, List<RepeatQuestionSetting> repeatQuestionSetting = null, bool isupdatequest = false)
        {
            bool isSaved = false;
            Util.QS.AddNewQuestion addNewRepeatQuestionSetting = new Util.QS.AddNewQuestion(workbook, DBHelper.GetConnectionString(workbook));
            int no_of_choice_index = -1;//as per  addquestion
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

            if (isupdatequest)
            {
                isnewquestion = false;
                if (repeatQuestionSetting != null)
                {
                    foreach (var item in repeatQuestionSetting)
                    {
                        List<QuestionSettings> questionSettings = new List<QuestionSettings>();
                        questionSettings = Util.Definiotion.VariableDictionary.Values.ToList();
                        if (questionSettings.Any(q => q.Variable.Equals(item.Variable, StringComparison.OrdinalIgnoreCase)))
                        {
                            if (addNewRepeatQuestionSetting.EditToSheet(item.Variable, item.AnswerType, item.TableHeading, item.Question, item.ChoiceIndex, no_of_choice_index, data))
                            {
                                isSaved = true;
                            }
                            else
                            {
                                isSaved = false;
                                return isSaved;
                            }
                        }
                        else
                        {
                            if (addNewRepeatQuestionSetting.SaveToSheet(item.Variable, item.AnswerType, item.TableHeading, item.Question, item.ChoiceIndex, no_of_choice_index, item.Choices, false))
                            {
                                isSaved = true;
                            }
                            else
                            {
                                isSaved = false;
                                return isSaved;
                            }
                        }
                    }

                }
                else
                {
                    Util.QS.AddNewQuestion addNew = new Util.QS.AddNewQuestion(workbook, DBHelper.GetConnectionString(workbook));
                    if (addNew.EditToSheet(variable, answertype, "", question, choiceindex, no_of_choice_index, data))
                    {
                        isSaved = true;
                    }
                    else
                    {
                        isSaved = false;
                    }
                }
            }

            if (isnewquestion)
            {
                if (repeatQuestionSetting != null)
                {
                    foreach (var item in repeatQuestionSetting)
                    {

                        if (addNewRepeatQuestionSetting.SaveToSheet(item.Variable, item.AnswerType, item.TableHeading, item.Question, item.ChoiceIndex, no_of_choice_index, item.Choices, false))
                        {
                            isSaved = true;
                        }
                        else
                        {
                            isSaved = false;
                            return isSaved;
                        }
                    }
                  Qc4Launcher.Util.Definiotion.VariableDictionary = QC4Common.Util.DictionaryUtil.PopulateQSDictionary(workbook);

                }
                else
                {
                    Util.QS.AddNewQuestion addNew = new Util.QS.AddNewQuestion(workbook, DBHelper.GetConnectionString(workbook));
                    if (addNew.SaveToSheet(variable, answertype, "", question, choiceindex, no_of_choice_index, data, false))
                    {
                        isSaved = true;
                    }
                    else
                    {
                        isSaved = false;
                    }
                }

            }
            if (isSaved || (!isnewquestion && !isSaved))
            {
                try
                {
                    int endwriterow = instructionarray.GetLength(0);
                    int writearraycount = endwriterow;
                    endwriterow = stdwriterow + endwriterow - 1;
                    Excel.Worksheet dataProcessSheet = Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Qc4Launcher.Util.Constants.SheetCodeName.DataProcessS);

                    if (stdprocessingoption == QC4Common.Common.Constants.STD_DP.Process_Edit)
                    {
                        switch (processingType)
                        {
                            case Util.Constants.ProcessingType.CreateNewVariable:
                                switch (command)
                                {
                                    case Util.Constants.ProcessingMethod.RECODE:
                                        int row = GetCurrentInstructionLastRow(stdwriterow, stdwriterow + 2, workbook);
                                        row += stdwriterow;
                                        if (stdwriterow != row)
                                        {
                                            row = row + 1;
                                        }
                                        int exsistingrowcount = row - stdwriterow+1;
                                        if (writearraycount == exsistingrowcount)
                                        {
                                            // Do nothing since same number of instructions are getting updated.
                                        }
                                        else if (stdwriterow < row)
                                        {
                                            //delete extra 2 rows  stdwriterow-row
                                            for (int i = 0; i < row - stdwriterow; i++)
                                            {
                                                Excel.Range start = dataProcessSheet.Cells[row - 1, QC4Common.Common.Constants.DP.OnOffColumn];
                                                Excel.Range End = dataProcessSheet.Cells[row - 1, QC4Common.Common.Constants.DP.MAX_DP_COLUMN];
                                                Excel.Range r = dataProcessSheet.Range[start, End];
                                                r.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                                            }

                                        }
                                        else if (endwriterow > row)
                                        {
                                            //insert 2 rows  instructionarray.GetLength(0)-1
                                            for (int i = 0; i < endwriterow - row; i++)
                                            {
                                                Excel.Range start = dataProcessSheet.Cells[row + 1, QC4Common.Common.Constants.DP.OnOffColumn];
                                                Excel.Range End = dataProcessSheet.Cells[row + 1, QC4Common.Common.Constants.DP.MAX_DP_COLUMN];
                                                Excel.Range r = dataProcessSheet.Range[start, End];
                                                r.EntireRow.Insert(Excel.XlInsertShiftDirection.xlShiftDown);
                                            }
                                        }
                                        break;
                                }
                                break;
                            case Util.Constants.ProcessingType.ReviseData:
                            case Util.Constants.ProcessingType.Exclude:
                            case Util.Constants.ProcessingType.DeleteCases:
                            case Util.Constants.ProcessingType.OutputList:
                                int roww = GetCurrentInstructionLastRow(stdwriterow, stdwriterow + 5, workbook);
                                roww += stdwriterow;
                                //if (stdwriterow != row)
                                //{
                                //    row = row + 1;
                                //}
                                //if (stdwriterow < roww)
                                if (endwriterow < roww)
                                  {
                                    //delete extra rows  stdwriterow-row
                                    for (int i = 0; i < roww - endwriterow; i++)
                                    {
                                        Excel.Range start = dataProcessSheet.Cells[roww - 1, QC4Common.Common.Constants.DP.OnOffColumn];
                                        Excel.Range End = dataProcessSheet.Cells[roww - 1, QC4Common.Common.Constants.DP.MAX_DP_COLUMN];
                                        Excel.Range r = dataProcessSheet.Range[start, End];
                                        r.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                                    }
                                }
                                else if (endwriterow > roww)
                                {
                                    //insert extra rows  instructionarray.GetLength(0)-1
                                    for (int i = 0; i < endwriterow - roww; i++)
                                    {
                                        Excel.Range start = dataProcessSheet.Cells[roww + 1, QC4Common.Common.Constants.DP.OnOffColumn];
                                        Excel.Range End = dataProcessSheet.Cells[roww + 1, QC4Common.Common.Constants.DP.MAX_DP_COLUMN];
                                        Excel.Range r = dataProcessSheet.Range[start, End];
                                        r.EntireRow.Insert(Excel.XlInsertShiftDirection.xlShiftDown);
                                    }
                                }
                                break;
                        }
                    }

                    Excel.Range rowstart = dataProcessSheet.Cells[stdwriterow, ExcelAddIn.Common.Constants.DP.OnOffColumn]; //ExcelAddIn.Common.Constants.DP.OnOffColumn;
                    Excel.Range rowend = dataProcessSheet.Cells[endwriterow, ExcelAddIn.Common.Constants.DP.MAX_DP_COLUMN];
                    Excel.Range rarDataProcess = dataProcessSheet.Range[rowstart, rowend];
                    var stdwritingarray = rarDataProcess.Value;
                    try
                    {
                        for (int i = 0; i < instructionarray.GetLength(0); i++)
                        {
                            for (int j = 0; j < instructionarray.GetLength(1); j++)
                            {
                                stdwritingarray[i + 1, j + 1] = instructionarray[i, j];
                            }
                        }

                    }
                    catch (Exception ex) { }
                    rarDataProcess.Value2 = stdwritingarray;
                    isSaved = true;
                }
                catch (Exception ex)
                {
                    isSaved = false;
                }

            }

            // Resetting mouse cursor to default
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default; 
            //  workbook = Addtosheet(workbook, processingType, variable, answertype, question, choiceindex, data, command, paramList);
            return isSaved;
        }

        /// <summary>Gets the row number.</summary>
        /// <returns></returns>
        public int GetRowNumber(Microsoft.Office.Interop.Excel.Workbook workbook, Microsoft.Office.Interop.Excel.Worksheet dataProcessSheet)
        {
            //Getting row number

            Excel.Range dpstart = dataProcessSheet.Cells[ExcelAddIn.Common.Constants.DP.ProUIstartRow, ExcelAddIn.Common.Constants.DP.OnOffColumn];
            Excel.Range lastcell = ExcelUtil.EndxlUp(dataProcessSheet.Cells[ExcelAddIn.Common.Constants.DP.ProUIstartRow, ExcelAddIn.Common.Constants.DP.OnOffColumn]);
            Excel.Range rar = dataProcessSheet.Range[dpstart, lastcell];
            int rowNo = ExcelAddIn.Common.Constants.DP.ProUIstartRow;

            if (rar.Cells.Count > 1)
            {
                var objAry = rar.Value;
                for (int i = 1; i <= objAry.GetLength(0); i++)
                {
                    if (objAry[i, 1] == LocalResource.CELL_ON || objAry[i, 1] == LocalResource.CELL_OFF)
                    {
                        rowNo = ExcelAddIn.Common.Constants.DP.ProUIstartRow + i;
                    }
                }
            }
            else
            {
                if (rar.Text == LocalResource.CELL_ON || rar.Text == LocalResource.CELL_OFF)
                {
                    rowNo += 1;
                }

            }
            return rowNo;
        }

        public Excel.Workbook Addtosheet(Microsoft.Office.Interop.Excel.Workbook workbook, string processingType, string variable, string answertype, string question, int choiceindex, DataTable data, string command, string[] paramList)
        {
            bool isSaved = false;

            //Add to Question setting
            if (processingType != ExcelAddIn.Common.Constants.DP.InstructionFOR && processingType != ExcelAddIn.Common.Constants.DP.InstructionNEXT)
            {
                Util.QS.AddNewQuestion addNew = new Util.QS.AddNewQuestion(workbook, DBHelper.GetConnectionString(workbook));

                if (addNew.SaveToSheet(variable, answertype, "", question, choiceindex, 0, data, false))
                {
                    MessageDialog.Info(LocalResource.ADDQS_INFO_MSG_SUCCESS);
                    isSaved = true;
                }
                else
                {
                    isSaved = false;
                }
            }


            Excel.Worksheet dataProcessSheet = Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Qc4Launcher.Util.Constants.SheetCodeName.DataProcessS);
            Excel.Range rowstart = dataProcessSheet.Cells[GetRowNumber(workbook, dataProcessSheet), 1];
            Excel.Range rowend = dataProcessSheet.Cells[GetRowNumber(workbook, dataProcessSheet), ExcelAddIn.Common.Constants.DP.MAX_DP_COLUMN];
            Excel.Range rarDataProcess = dataProcessSheet.Range[rowstart, rowend];
            if (rarDataProcess.Cells.Count > 1)
            {
                var obj = rarDataProcess.Value;
                obj[1, 3] = LocalResource.CELL_ON;
                switch (processingType)
                {
                    case Util.Constants.ProcessingType.CreateNewVariable:
                    case Util.Constants.ProcessingType.ReviseData:
                        obj[1, ExcelAddIn.Common.Constants.DP.InstructionColumn] = ExcelAddIn.Common.Constants.DP.InstructionTHEN;
                        break;
                    case "FOR":
                        obj[1, ExcelAddIn.Common.Constants.DP.InstructionColumn] = ExcelAddIn.Common.Constants.DP.InstructionFOR;
                        break;
                    case "NEXT":
                        obj[1, ExcelAddIn.Common.Constants.DP.InstructionColumn] = ExcelAddIn.Common.Constants.DP.InstructionNEXT;
                        break;
                    default:
                        break;
                }
                obj[1, ExcelAddIn.Common.Constants.DP.SubstituteVariableColumn] = variable;// New variable added
                obj[1, ExcelAddIn.Common.Constants.DP.SubstituteOperatorColumn] = command;//Command added
                if (paramList != null)
                {
                    for (int i = 0; i < paramList.Length; i++)
                    {
                        obj[1, ExcelAddIn.Common.Constants.DP.SubstituteParam1Column + i] = paramList[i];//adding all parameters
                    }
                }

                rarDataProcess.Value2 = obj;

            }
            else
            {
                var obj = rarDataProcess.Value;
            }


            return workbook;
        }

        public string[,] GetRangevalues(int startrow, int lastrow, Microsoft.Office.Interop.Excel.Workbook workbook)
        {
            List<string> rangevalues = new List<string>();
            if (startrow < ExcelAddIn.Common.Constants.DP.ProUIstartRow)
            {
                return null;
            }
            Excel.Worksheet stddataprocess = Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Qc4Launcher.Util.Constants.SheetCodeName.DataProcessS);
            Excel.Range strddpstart = stddataprocess.Cells[startrow, ExcelAddIn.Common.Constants.DP.OnOffColumn];
            Excel.Range stddplastcell = stddataprocess.Cells[lastrow, ExcelAddIn.Common.Constants.DP.MAX_DP_COLUMN];
            Excel.Range stddprowrange = stddataprocess.Range[strddpstart, stddplastcell];
            var objarray = stddprowrange.Value;
            string[,] rangearray = new string[objarray.GetLength(0), objarray.GetLength(1)];
            for (int j = 1; j <= objarray.GetLength(0); j++)
            {
                for (int i = 1; i <= objarray.GetLength(1); i++)
                {
                    rangearray[j - 1, i - 1] = Convert.ToString(objarray[j, i]);
                }
            }
            return rangearray;
        }
        public int GetCurrentInstructionLastRow(int currentrow, int lastrow, Microsoft.Office.Interop.Excel.Workbook workbook)
        {
            int returnrow = currentrow;
            var currentrange = GetRangevalues(currentrow, lastrow, workbook);
            int column = QC4Common.Common.Constants.DP.InstructionColumn - QC4Common.Common.Constants.DP.OnOffColumn;
            for (int i = 0; i < currentrange.GetLength(0); i++)
            {
                //for (int j = 1; j <= currentrange.GetLength(1); j++)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(currentrange[i, column])))
                    {
                        switch (Convert.ToString(currentrange[i, column]))
                        {
                            case QC4Common.Common.Constants.DP.InstructionTHEN:
                            case QC4Common.Common.Constants.DP.InstructionOMIT:
                            case QC4Common.Common.Constants.DP.InstructionLISTUP:
                            case QC4Common.Common.Constants.DP.InstructionDELETE:
                            case QC4Common.Common.Constants.DP.InstructionLDEL:
                                return i;
                                
                            default:
                                returnrow = i;
                                break;
                        }
                    }
                }
            }
            return returnrow;
        }

        public int GetCurrentProcessFirstRow(int currentrow, Microsoft.Office.Interop.Excel.Workbook workbook)
        {
            int returnrow = currentrow;
            var currentrange = GetRangevalues(currentrow, QC4Common.Common.Constants.STD_DP.Criteria_MaxRows, workbook);
            int column = QC4Common.Common.Constants.DP.InstructionColumn - QC4Common.Common.Constants.DP.OnOffColumn;
            for (int i = currentrange.GetLength(0) - 2; i >= 0; i--)
            {
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(currentrange[i, column])))
                    {
                        switch (Convert.ToString(currentrange[i, column]))
                        {
                            case QC4Common.Common.Constants.DP.InstructionTHEN:
                            case QC4Common.Common.Constants.DP.InstructionOMIT:
                            case QC4Common.Common.Constants.DP.InstructionDELETE:
                            case QC4Common.Common.Constants.DP.InstructionLISTUP:
                            case QC4Common.Common.Constants.DP.InstructionLDEL:
                            case QC4Common.Common.Constants.DP.InstructionNEXT:
                                return returnrow;
                            default:
                                returnrow--;
                                break;
                        }
                    }
                }
            }
            return returnrow;
        }

        public Excel.Range GetLastCellInRow(Excel.Range targetCell)
        {
            Excel.Range targetColumn = targetCell.Cells[1].EntireRow;
            return targetColumn.Cells[targetColumn.Cells.Count].End(Excel.XlDirection.xlToLeft);
        }
        int deletedItemsCount = 0;
        public void DeleteFromDataProcesssheet(int rowN0, Excel.Worksheet dataProcessSheet, int repeatCount, string processingMethodType)
        {
            int totalRowsToBeDeleted = 0;
            switch (processingMethodType)
            {
                case QC4Common.Common.Constants.DP.InstructionLISTUP:
                    rowN0 = rowN0 - deletedItemsCount;
                    totalRowsToBeDeleted = GetTotalRowsCount_MultipleCriteria(rowN0, dataProcessSheet);
                    for (int i = 1; i <= totalRowsToBeDeleted; i++)
                    {
                        Excel.Range start1 = dataProcessSheet.Cells[rowN0 - (totalRowsToBeDeleted- 1), QC4Common.Common.Constants.DP.OnOffColumn];
                        Excel.Range End1 = dataProcessSheet.Cells[rowN0 - (totalRowsToBeDeleted - 1), QC4Common.Common.Constants.DP.MAX_DP_COLUMN];
                        Excel.Range r1 = dataProcessSheet.Range[start1, End1];
                        r1.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                        deletedItemsCount++;
                    }
                    break;
                case QC4Common.Common.Constants.DP.InstructionOMIT:
                    rowN0 = rowN0 - deletedItemsCount;
                    totalRowsToBeDeleted = GetTotalRowsCount_MultipleCriteria(rowN0, dataProcessSheet);
                    for (int i = 1; i <= totalRowsToBeDeleted; i++)
                    {
                        Excel.Range start1 = dataProcessSheet.Cells[rowN0 - (totalRowsToBeDeleted - 1), QC4Common.Common.Constants.DP.OnOffColumn];
                        Excel.Range End1 = dataProcessSheet.Cells[rowN0 - (totalRowsToBeDeleted - 1), QC4Common.Common.Constants.DP.MAX_DP_COLUMN];
                        Excel.Range r1 = dataProcessSheet.Range[start1, End1];
                        r1.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                        deletedItemsCount++;
                    }
                    break;
                case QC4Common.Common.Constants.DP.SubstituteOperatorADD_MINUS:
                    rowN0 = rowN0 - deletedItemsCount;
                    totalRowsToBeDeleted = GetTotalRowsCount_MultipleCriteria(rowN0, dataProcessSheet);
                    for (int i = 1; i <= totalRowsToBeDeleted; i++)
                    {
                        Excel.Range start1 = dataProcessSheet.Cells[rowN0 - (totalRowsToBeDeleted - 1), QC4Common.Common.Constants.DP.OnOffColumn];
                        Excel.Range End1 = dataProcessSheet.Cells[rowN0 - (totalRowsToBeDeleted - 1), QC4Common.Common.Constants.DP.MAX_DP_COLUMN];
                        Excel.Range r1 = dataProcessSheet.Range[start1, End1];
                        r1.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                        deletedItemsCount++;
                    }
                    break;

                case QC4Common.Common.Constants.DP.SubstituteOperatorDELETE:
                    rowN0 = rowN0 - deletedItemsCount;
                    totalRowsToBeDeleted = GetTotalRowsCount_MultipleCriteria(rowN0, dataProcessSheet);
                    for (int i = 1; i <= totalRowsToBeDeleted; i++)
                    {
                        Excel.Range start1 = dataProcessSheet.Cells[rowN0 - (totalRowsToBeDeleted - 1), QC4Common.Common.Constants.DP.OnOffColumn];
                        Excel.Range End1 = dataProcessSheet.Cells[rowN0 - (totalRowsToBeDeleted - 1), QC4Common.Common.Constants.DP.MAX_DP_COLUMN];
                        Excel.Range r1 = dataProcessSheet.Range[start1, End1];
                        r1.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                        deletedItemsCount++;
                    }
                    break;
                case QC4Common.Common.Constants.DP.SubstituteOperatorRECODE:
                    if (repeatCount>0)
                    {
                        totalRowsToBeDeleted = 3;
                        rowN0 = rowN0 - deletedItemsCount;
                        for (int i = 1; i <= totalRowsToBeDeleted; i++)
                        {
                            Excel.Range start1 = dataProcessSheet.Cells[rowN0-1, QC4Common.Common.Constants.DP.OnOffColumn];
                            Excel.Range End1 = dataProcessSheet.Cells[rowN0-1, QC4Common.Common.Constants.DP.MAX_DP_COLUMN];
                            Excel.Range r1 = dataProcessSheet.Range[start1, End1];
                            r1.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                            deletedItemsCount++;
                        }
                    }
                    else
                    {
                        rowN0 = rowN0 - deletedItemsCount;
                        Excel.Range start2 = dataProcessSheet.Cells[rowN0, QC4Common.Common.Constants.DP.OnOffColumn];
                        Excel.Range End2 = dataProcessSheet.Cells[rowN0, QC4Common.Common.Constants.DP.MAX_DP_COLUMN];
                        Excel.Range r2 = dataProcessSheet.Range[start2, End2];
                        r2.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                        deletedItemsCount++;
                    }
                    break;
                default:
                        rowN0 = rowN0 - deletedItemsCount;
                        Excel.Range start = dataProcessSheet.Cells[rowN0, QC4Common.Common.Constants.DP.OnOffColumn];
                        Excel.Range End = dataProcessSheet.Cells[rowN0, QC4Common.Common.Constants.DP.MAX_DP_COLUMN];
                        Excel.Range r = dataProcessSheet.Range[start, End];
                        r.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                        deletedItemsCount++;
                        break;
            }
        }
        public void DeleteDataProcess(int rowN0, Excel.Worksheet dataProcessSheet, int rowsCount, string processingMethodType)
        {
            for (int i = 1; i <= rowsCount; i++)
            {
                Excel.Range start = dataProcessSheet.Cells[rowN0, 3];
                Excel.Range End = dataProcessSheet.Cells[rowN0, 1042];
                Excel.Range r = dataProcessSheet.Range[start, End];
                r.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
            }

        }

        public int GetTotalRowsCount_MultipleCriteria(int rowN0, Excel.Worksheet dataProcessSheet)
        {
            int rowsCount = 1;
            for (int i = rowN0; i > rowN0 - 4; i--)
            {
                Excel.Range r_LISTUP1 = dataProcessSheet.Cells[i - 1, 8];

                if (r_LISTUP1.Value == QC4Common.Common.Constants.DP.InstructionAND || r_LISTUP1.Value == QC4Common.Common.Constants.DP.InstructionOR)
                {
                    rowsCount++;
                }
                else
                {
                    break;
                }
            }
            return rowsCount;
        }
        public void ONorOFFDataProcess(int rowN0, int? repeats, Excel.Worksheet dataProcessSheet, string processingMethodType)
        {
            switch (processingMethodType)
            {
                case QC4Common.Common.Constants.DP.InstructionLISTUP:
                    OnOROFF_MultipleCriteria(rowN0, dataProcessSheet);
                    break;

                case QC4Common.Common.Constants.DP.InstructionOMIT:
                    OnOROFF_MultipleCriteria(rowN0, dataProcessSheet);
                    break;

                case QC4Common.Common.Constants.DP.SubstituteOperatorADD_MINUS:
                    OnOROFF_MultipleCriteria(rowN0, dataProcessSheet);
                    break;

                case QC4Common.Common.Constants.DP.SubstituteOperatorDELETE:
                    OnOROFF_MultipleCriteria(rowN0, dataProcessSheet);
                    break;
                case QC4Common.Common.Constants.DP.SubstituteOperatorRECODE:
                    if (repeats != null)
                    {
                        for (int i = rowN0; i < rowN0+3; i++)
                        {
                            Excel.Range repeatsRange = dataProcessSheet.Cells[i-1, 3];
                            if (repeatsRange.Value == LocalResource.CELL_ON)
                            {
                                repeatsRange.Value = LocalResource.CELL_OFF;
                            }
                            else
                            {
                                repeatsRange.Value = LocalResource.CELL_ON;
                            }
                        }

                    }
                    else
                    {
                        Excel.Range recodeRange = dataProcessSheet.Cells[rowN0, 3];
                        if (recodeRange.Value == LocalResource.CELL_ON)
                        {
                            recodeRange.Value = LocalResource.CELL_OFF;
                        }
                        else
                        {
                            recodeRange.Value = LocalResource.CELL_ON;
                        }
                    }
                    break;
                default:
                    Excel.Range r = dataProcessSheet.Cells[rowN0, 3];
                    if (r.Value == LocalResource.CELL_ON)
                    {
                        r.Value = LocalResource.CELL_OFF;
                    }
                    else
                    {
                        r.Value = LocalResource.CELL_ON;
                    }
                    break;
            }

        }

        // Handles On Or OFF function for data process having mutliple criterias(LISTUP, OMIT, DELETE, ADDMINUS)
        public void OnOROFF_MultipleCriteria(int rowN0, Excel.Worksheet dataProcessSheet)
        {
            Excel.Range r_LISTUP = dataProcessSheet.Cells[rowN0, 3];
            if (r_LISTUP.Value == LocalResource.CELL_ON)
            {
                r_LISTUP.Value = LocalResource.CELL_OFF;
                for (int i = rowN0; i > rowN0 - 4; i--)
                {
                    Excel.Range r_LISTUP1 = dataProcessSheet.Cells[i - 1, 8];

                    if (r_LISTUP1.Value == QC4Common.Common.Constants.DP.InstructionAND || r_LISTUP1.Value == QC4Common.Common.Constants.DP.InstructionOR)
                    {
                        Excel.Range r_LISTUP2 = dataProcessSheet.Cells[i - 1, 3];
                        r_LISTUP2.Value = LocalResource.CELL_OFF;
                    }
                    else
                    {
                        break;
                    }
                }

            }
            else
            {
                r_LISTUP.Value = LocalResource.CELL_ON;
                for (int i = rowN0; i > rowN0 - 4; i--)
                {
                    Excel.Range r_LISTUP1 = dataProcessSheet.Cells[i - 1, 8];

                    if (r_LISTUP1.Value == QC4Common.Common.Constants.DP.InstructionAND || r_LISTUP1.Value == QC4Common.Common.Constants.DP.InstructionOR)
                    {
                        Excel.Range r_LISTUP2 = dataProcessSheet.Cells[i - 1, 3];
                        r_LISTUP2.Value = LocalResource.CELL_ON;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        
        // Hanldes DP_Main up and down arrow button fuctionalities
        public void DP_Up_Down_Row(Excel.Worksheet targetSheet, int startRow, int endRow, bool Down_Mode = false)
        {

            Excel.Range start = targetSheet.Cells[startRow, ExcelAddIn.Common.Constants.DP.OnOffColumn];
            Excel.Range end = targetSheet.Cells[endRow, ExcelAddIn.Common.Constants.DP.MAX_DP_COLUMN];
            //Excel.Range end = ExcelUtil.EndxlRight(start);
            Excel.Range range = targetSheet.get_Range(start, end);
            var obj = range.Value;
            if (range.Areas.Count > 1)
            {
                MessageDialog.ErrorOk(AddinResource.CANNOT_SPECIFY_MULTIPLE_RANGE);
                return;
            }

            if (range.Rows.Count == ExcelUtil.EndRow(targetSheet) && range.Columns.Count == ExcelUtil.EndColumn(targetSheet))
            {
                return;
            }

            long Ret_Row = 0;

            if (Down_Mode == true)
            {

                if (range.Row >= QC4Common.Common.Constants.DPDownMinRow)
                    Ret_Row = FNC_Dp_UpDown_Exec(targetSheet, range, Down_Mode);
            }
            else
            {
                if (range.Row >= QC4Common.Common.Constants.DPUpMinRow)
                    Ret_Row = FNC_Dp_UpDown_Exec(targetSheet, range, Down_Mode);
            }

        }

        // Hanldes DP_Main up and down arrow button fuctionalities
        public long FNC_Dp_UpDown_Exec(Excel.Worksheet targetSheet, Excel.Range Exec_Range, bool Down_Mode = false)
        {
            long Ret_Row = 0;



            if (Down_Mode == false)
            {
                if (Exec_Range.Row != QC4Common.Common.Constants.DPDeleteInsertMinRow)
                {
                    Exec_Range.Cut();
                    Exec_Range.Offset[-1, 0].Insert(Excel.XlDirection.xlDown);
                    Ret_Row = -1;
                }
            }
            else if (Exec_Range.Row + (Exec_Range.Rows.Count - 1) != targetSheet.Rows.Count)
            {
                Exec_Range.Cut();
                Exec_Range.Offset[Exec_Range.Rows.Count + 1, 0].Insert(Excel.XlDirection.xlDown);
                Ret_Row = 1;
            }
            return Ret_Row;
        }
    }
}
