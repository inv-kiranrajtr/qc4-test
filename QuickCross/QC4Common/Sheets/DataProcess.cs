using QC4Common.Common;
using System;
using Excel = Microsoft.Office.Interop.Excel;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Vbe.Interop.Forms;
using Microsoft.VisualBasic.CompilerServices;
using Macromill.QCWeb.DataProcess;
using System.Data.OleDb;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SQLite;
using QC4Common.DB;
using Macromill.QCWeb.Common;
using Constants = QC4Common.Common.Constants;//
using System.IO;
using QC4Common.Model;
using QC4Common.Logic;//
using ProgressBar = QC4Common.Forms.ProgressBar;//
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using Qc4CommonConstants = QC4Common.Common.Constants;
using log4net;
using System.Reflection;
using QC4Common.Util;//
using Macromill.QCWeb.Question;
using static Macromill.QCWeb.Question.Questions;
using System.Windows;
using System.Text.RegularExpressions;
using Macromill.QCWeb.COMOperate;
using System.ComponentModel;
using System.Threading.Tasks;

namespace QC4Common.Sheets
{
    public class DataProcess//191  added public
    {
        public static Excel.Workbook currworkbook;
        public static Excel.Worksheet Sheet;
        private static CommandButton ExecuteButton;
        //private static CommandButton DeleteButton;
        private List<string> optionList = new List<string>();
        private bool FORValidationSuccess = false;
        private static int FORendrow;
        

        List<string> listCallFullKeys = new List<string>();
        List<string> listCallFullValues = new List<string>();
        public static bool sylk = false;
        string CurrentAlphabet = string.Empty;
        public Dictionary<int, string> operatorsDict = new Dictionary<int, string>();
        public Dictionary<int, Crit_Inst_Operator> crit_inst_operator_Dict = new Dictionary<int, Crit_Inst_Operator>();
        public Dictionary<int, Crit_Inst_Operator> ListUp_Dict = new Dictionary<int, Crit_Inst_Operator>();
        public static Dictionary<string, DPCallMethod> decst_ProgramList = new Dictionary<string, DPCallMethod>();
        QC4Common.Util.FormUtil frmutil = new QC4Common.Util.FormUtil();
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public class Crit_Inst_Operator
        {
            public string criteriavariable;
            public string instruction;
            public string substituteoperator;
            public string ListUpOpr;//191  added for listup operator
        }
        // List<string> DECSTVariableList = new List<string>();//IL_JP_MAM_007:4295046210
        public static bool isexecute = false; //Redmine id: 175141
        public bool ProInitialized = false;
        public DataProcess(Excel.Worksheet worksheet)
        {
            currworkbook = worksheet.Application.ActiveWorkbook;
            Sheet = worksheet;
            SetDecstProgramList(true);
            if (decst_ProgramList.Count > 0)
            {
                SetDECSTMethodList();
                UpdateDECENDRow(false);
            }
            ProInitialized = false;


            // BindDPButtons();

        }
        public void SetDECSTMethodList()
        {
            Excel.Worksheet listSheet = ExcelUtilForAddIn.GetWorksheetByName(currworkbook,"List");
            foreach (Excel.Name namedrange in listSheet.Names)
            {
                if (namedrange.NameLocal == Constants.DP.ListMethod)
                {
                    namedrange.Delete();
                }
            }
            listSheet.Cells[1, 23].Value = "DECST Methods";
            ExcelUtilForAddIn.SetCellInteriorColor(listSheet.Cells[1, 23], Constants.Color.Black);
            ExcelUtilForAddIn.SetFontColor(listSheet.Cells[1, 23], Constants.Color.White);
            List<string> methodList = decst_ProgramList.Keys.ToList<string>();
            Excel.Range startcell = listSheet.Cells[2, 23];
            Excel.Range endcell = listSheet.Cells[1 + methodList.Count, 23];
            Excel.Range methodRange = listSheet.Range[startcell, endcell];
            methodRange.NumberFormat = "@";//#212077
            for (int i = 0; i < methodList.Count; i++)
            {
                listSheet.Cells[2 + i, 23].Value = methodList[i];
            }

            listSheet.Application.ActiveWorkbook.Names.Add(Constants.DP.ListMethod, methodRange);


        }
        private void DataProcessDelete(Excel.Worksheet worksheet)
        {
            //need to get  currwnt cell celection,check the current cell is  inside range -where we enter instructions.themn clear
            //    Excel.Range selrange = (Excel.Range)worksheet.Application.Selection;
            //    int row= selrange.Row;
            //    if (row >=Constants.DP.ProUIstartRow)//only delete if row inside header;from column C to last column;can use switch and use other options also insert paste copy and make common funct
            //    {
            //        string ragecell = Constants.DP.DPColBegin + row.ToString();
            //string endcell = Constants.DP.DPColBegin + Constants.DP.MAX_DP_COLUMN.ToString();
            //        Excel.Range deleterange = worksheet.get_Range(ragecell, endcell);
            //        deleterange.Delete();
            //    }           
        }
        public void StopProcess()
        {
            if (ProgressBar.isStop)
            {
                AutoReset.WaitOne();
            }
            else
            {
                AutoReset.Set();
            }
        }
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        AutoResetEvent AutoReset;
        public void DataProcessExecute(Excel.Worksheet worksheet, ref Excel.Application xlApp,  object worker = null, DoWorkEventArgs bgWorkerArg = null, AutoResetEvent autoReset = null, ProgressBar progress = null, bool frmStd = false)//191  changed from private to  public for Ribbon Call

        {
            BackgroundWorker bgWorker = worker as BackgroundWorker;
            Logic.DataProcessExecute dpExecObject = new Logic.DataProcessExecute(this, worksheet, frmStd);
            //get answertable data
            if (autoReset != null)
            {
                AutoReset = autoReset;
            }
            DataTable dt = new DataTable();
            try
            {
                using (SQLiteConnection dbSource = DBHelper.GetConnection(DBHelper.GetConnectionString(DataProcess.Sheet.Application.ActiveWorkbook)))
                {
                    dbSource.Open();
                    DBHelper.CreateDataprocessTable(dbSource);
                    dbSource.Close();
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

            dpExecObject.Execute(worksheet,ref xlApp, progress, bgWorker, bgWorkerArg, autoReset);

            if (QC4Common.Global.Global.CheckOperationFlag == 0)
            {
                //MessageDialog.Info(CommonResource.DATAPROCESS_COMPLETED);

                try
                {
                    if (progress != null)
                    {
                        IntPtr hForeGroundWnd = QC4Common.Common.Util.GetIntPtrWIndowHandleFromSettings(currworkbook);// GetForegroundWindow();
                        if (frmStd)
                        {
                            StopProcess();
                            if (bgWorker.CancellationPending)
                            {

                                return;
                            }


                            //progress.Dispatcher.Invoke(() => progress.Topmost = false);
                            //progress.Dispatcher.Invoke(() =>
                            //QC4Common.Common.MessageDialog.ShowMessageOnParent(CommonResource.DATAPROCESS_COMPLETED, Qc4CommonConstants.MessageType.Info, hForeGroundWnd));

                            // progress.ShowErrorMessage(CommonResource.DATAPROCESS_COMPLETED);
                        }
                        else
                        {
                            StopProcess();
                            if (bgWorker.CancellationPending)
                            {

                                return;
                            }
                            //progress.Dispatcher.Invoke(() => QC4Common.Common.MessageDialog.ShowMessageOnWorkBook(CommonResource.DATAPROCESS_COMPLETED, Qc4CommonConstants.MessageType.Info, DataProcess.currworkbook /*Sheet.Application.ActiveWorkbook*/));//Sheet.Application.ActiveWorkbook

                        }
                    }

                    //     MessageBox.Show(progress, CommonResource.DATAPROCESS_COMPLETED,QC4Common.Common.Constants.MessageBoxTitle,MessageBoxButton.OK,MessageBoxImage.Information);

                    //  QC4Common.Common.MessageDialog.Error()
                }
                catch (Exception ex)
                {
                    MessageDialog.Info(CommonResource.DATAPROCESS_COMPLETED);
                    System.Diagnostics.Debug.WriteLine("StackTrace:{0}", ex.StackTrace);
                }
                if (progress != null)
                {
                    StopProcess();
                    if (bgWorker.CancellationPending)
                    {
                        return;
                    }
                    else
                    {
                        progress.OnWorkerMethodComplete(100, CommonResource.DP_PROGRESS_MSG_95, retainThread: true);
                    }

                }
                worksheet.Application.Interactive = true;
            }
            else
            {
                if (progress != null)
                {
                    StopProcess();
                    if (bgWorker.CancellationPending)
                    {

                        return;
                    }
                    else
                    {
                        progress.OnWorkerMethodComplete(100, CommonResource.DP_PROGRESS_MSG_95, retainThread: true);
                    }

                }
                // Thread.Sleep(2000);
            }




            // SetForegroundWindow((IntPtr)worksheet.Application.Hwnd);
            try
            {
                //Microsoft.Office.Interop.Excel.Application app =(Excel.Application)  System.Runtime.InteropServices.Marshal.GetActiveObject("Excel.Application");


                //foreach (Excel.Workbook wb in app.Workbooks)
                //{
                //    if(wb.Name.Equals("Comparison GT.xlsx"))
                //    {

                //        wb.Activate();
                //    }
                //}

                //Excel.Application exApp;
                //exApp = (Excel.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Excel.Application");
                if (bgWorker.CancellationPending)
                {

                    return;
                }
                else
                {
                    progress.OnWorkerMethodComplete(100, CommonResource.DP_PROGRESS_MSG_95, retainThread: true);
                }
                try
                {
                    if (Logic.DataProcessExecute.islistup == true)
                    {
                        //if (frmStd)
                        if (!bgWorker.CancellationPending)
                        {
                            try
                            {
                                xlApp.ErrorCheckingOptions.NumberAsText = false;
                            }
                            catch { }
                            xlApp.Worksheets["LIST"].name = CommonResource.LISTUP_LIST;
                            // Logic.DataProcessExecute.lworkbook.Worksheets["No. of cases"].name = CommonResource.LISTUP_NO_OF_CASES;
                            //
                            //  Logic.DataProcessExecute.lworkbook.Application.Visible = false;
                            //  Logic.DataProcessExecute.lworkbook.Activate();
                            //  Logic.DataProcessExecute.lworkbook.Application.WindowState = Excel.XlWindowState.xlMinimized;
                            try
                            {
                                DataProcess.Sheet.Application.ScreenUpdating = true;
                                var SettingSheet = ExcelUtilForAddIn.GetWorkSheetByCodeName(DataProcess.Sheet.Application.ActiveWorkbook, Constants.SheetCodeName.DataProcess);
                                Excel.Range rar = SettingSheet.get_Range("B5", "Z100");
                                //rar.Columns.AutoFit();
                                rar.ShrinkToFit = false;
                            }
                            catch (Exception e) { }
                        }

                    }
                    // bool wbOpened = ((Excel.Application)Marshal.GetActiveObject("Excel.Application")).Workbooks.Cast<Excel.Workbook>().FirstOrDefault(x => x.Name == "Comparison GT.xlsx") != null;
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                }

            }
            catch
            {
                // Excel is not running.
            }
            // SetForegroundWindow((IntPtr)worksheet.Application.Hwnd);
        }
        private bool UpdateDECENDRow(bool displayErrmsg = true)
        {
            List<int> DecEndRowList = GetInstructionRowList(Constants.DP.InstructionDECEND, false);
            if (DecEndRowList.Count > 0)
            {
                int iterationcount = decst_ProgramList.Count < DecEndRowList.Count ? decst_ProgramList.Count : DecEndRowList.Count;
                int decstcount = decst_ProgramList.Count;
                int decendcount = DecEndRowList.Count;
                int i = 0, j = 0;

                if (decst_ProgramList.Count <= 0)
                {
                    if (displayErrmsg)
                    {

                        ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, DecEndRowList[0].ToString()) + " ", CommonResource.ERR_INVALID_DECEND, string.Empty, DecEndRowList[0], Constants.DP.InstructionColumn);

                    }
                    return false;
                }

                for (i = 1, j = 1; i < decstcount && j <= decendcount; i++, j++)
                {
                    if ((decst_ProgramList.ElementAt(i - 1).Value.Rowstart < DecEndRowList[j - 1]) &&
                        (DecEndRowList[j - 1] < decst_ProgramList.ElementAt(i).Value.Rowstart))
                    {
                        //ShowAlert(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW + " " + decst_ProgramList.ElementAt(0).Value.Rowstart.ToString(), CommonResource.ERR_INVALID_DECST, string.Empty);
                        // return false;
                        decst_ProgramList.ElementAt(i - 1).Value.RowEnd = DecEndRowList[j - 1];
                    }
                    else
                    {
                        int rownumber = decst_ProgramList.ElementAt(i - 1).Value.Rowstart < DecEndRowList[j - 1] ? decst_ProgramList.ElementAt(i - 1).Value.Rowstart : DecEndRowList[j - 1];
                        string formattederr = string.Format(CommonResource.ERR_UNMATCHING_DECST, Sheet.Cells[rownumber, Constants.DP.InstructionColumn].Text);
                        if (displayErrmsg)
                        {

                            ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, rownumber) + " ", formattederr, string.Empty, rownumber, Constants.DP.InstructionColumn);

                        }
                        return false;

                    }
                }
                if (i == decstcount && j == decendcount && i == j)
                {
                    if (decst_ProgramList.ElementAt(i - 1).Value.Rowstart < DecEndRowList[j - 1])
                    {
                        decst_ProgramList.ElementAt(i - 1).Value.RowEnd = DecEndRowList[j - 1];
                    }
                    else
                    {
                        int rownumber = decst_ProgramList.ElementAt(i - 1).Value.Rowstart < DecEndRowList[j - 1] ? decst_ProgramList.ElementAt(i - 1).Value.Rowstart : DecEndRowList[j - 1];
                        string formattederr = string.Format(CommonResource.ERR_UNMATCHING_DECST, Sheet.Cells[rownumber, Constants.DP.InstructionColumn].Text);
                        if (displayErrmsg)
                        {

                            ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, rownumber) + " ", formattederr, string.Empty, rownumber, Constants.DP.InstructionColumn);

                        }
                        return false;

                    }
                }
                else
                {
                    string formattederr = string.Format(CommonResource.ERR_UNMATCHING_DECST, "DECST~DECEND");
                    if (displayErrmsg)
                    {

                        ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, decst_ProgramList.ElementAt(decst_ProgramList.Count - 1).Value.Rowstart.ToString()) + " ", formattederr, string.Empty, decst_ProgramList.ElementAt(decst_ProgramList.Count - 1).Value.Rowstart, Constants.DP.InstructionColumn);

                    }
                    return false;
                }
            }
            else
            {
                if (decst_ProgramList.Count > 0)
                {
                    if (displayErrmsg)
                    {


                        ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, decst_ProgramList.ElementAt(0).Value.Rowstart.ToString()) + " ", CommonResource.ERR_INVALID_DECST, string.Empty, decst_ProgramList.ElementAt(0).Value.Rowstart, Constants.DP.InstructionColumn);

                    }
                    return false;

                }
            }
            return true;
        }
        public bool DPCheck()
        {
            bool retval = true;
            Logic.DataProcessExecute dpExecObject = new Logic.DataProcessExecute(this);
            Excel.Range dpstart = DataProcess.Sheet.Cells[Constants.DP.ProUIstartRow, Constants.DP.OnOffColumn];
            Excel.Range lastcell = ExcelUtilForAddIn.EndxlUp(DataProcess.Sheet.Cells[Constants.DP.ProUIstartRow, Constants.DP.OnOffColumn]);
            SetDecstProgramList();
            Logic.DataProcessExecute.islistup = false;//for listup
            if (!UpdateDECENDRow()) return false;

            this.crit_inst_operator_Dict = dpExecObject.GetinstructionsByRow(dpstart, lastcell);
            if (ValidateCellValuesBforBtnClick(this.crit_inst_operator_Dict) == false) retval = false;

            return retval;
        }
        private List<int> GetInstructionRowList(string instructiontoFind, bool IgnoreONOFF = false)
        {
            List<int> Instructionrows = new List<int>();
            Excel.Range firstFind = null;
            Excel.Range startcell = DataProcess.Sheet.Cells[Constants.DP.ProUIstartRow - 1, Constants.DP.InstructionColumn];
            Excel.Range endcell = ExcelUtilForAddIn.EndxlUp(DataProcess.Sheet.Cells[Constants.DP.ProUIstartRow, Constants.DP.InstructionColumn]);
            Excel.Range InstructionColumn = DataProcess.Sheet.Range[startcell, endcell];
            Excel.Range instructionCell = InstructionColumn.Find(instructiontoFind, Missing.Value, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlWhole, Excel.XlSearchOrder.xlByColumns, Excel.XlSearchDirection.xlNext,
                true, false, Missing.Value);

            while (instructionCell != null)
            {
                if (firstFind == null)
                {
                    firstFind = instructionCell;
                }
                else if (firstFind.get_Address(Excel.XlReferenceStyle.xlA1) == instructionCell.get_Address(Excel.XlReferenceStyle.xlA1))
                {
                    break;
                }
                if (IgnoreONOFF || string.Equals(DataProcess.Sheet.Cells[instructionCell.Row, Constants.DP.OnOffColumn].Text, CommonResource.CELL_ON))
                {
                    Instructionrows.Add(instructionCell.Row);
                }
                instructionCell = InstructionColumn.FindNext(instructionCell);


            }


            return Instructionrows;

        }
        private int FindInstructionRow(int StartRow, string instructiontoFind, bool IgnoreONOFF = false)
        {
            Excel.Range startcell = DataProcess.Sheet.Cells[StartRow, Constants.DP.InstructionColumn];
            Excel.Range endcell = ExcelUtilForAddIn.EndxlUp(DataProcess.Sheet.Cells[Constants.DP.ProUIstartRow, Constants.DP.InstructionColumn]);
            Excel.Range InstructionColumn = DataProcess.Sheet.Range[startcell, endcell];
            Excel.Range instructionCell = InstructionColumn.Find(instructiontoFind, Missing.Value, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlWhole, Excel.XlSearchOrder.xlByColumns, Excel.XlSearchDirection.xlNext,
                true, false, Missing.Value);

            if (instructionCell != null)
            {
                if (IgnoreONOFF || string.Equals(DataProcess.Sheet.Cells[instructionCell.Row, Constants.DP.OnOffColumn].Text, CommonResource.CELL_ON))
                {
                    // Instructionrows.Add(instructionCell.Row);
                    return instructionCell.Row;
                }



            }
            return -1;

        }

        
        /// <summary>
        /// Insert Data(VariableNames) in DocPane
        /// </summary>
        public void FillDataInDocpane(CustomTaskPaneControl CustomControlPane)
        {
            Excel.Range Range = null;
            Excel.Range xlRange = null;
            bool enableEvet = Sheet.Application.EnableEvents;
            bool scrUpdate = Sheet.Application.ScreenUpdating;
            Excel.XlCalculation xlCalculation = Sheet.Application.Calculation;

            Sheet.Application.EnableEvents = false;
            Sheet.Application.ScreenUpdating = false;
            Sheet.Application.Calculation = Excel.XlCalculation.xlCalculationManual;

            CustomControlPane.ClearList();
            CustomControlPane.SetSheetObject(Sheet);
            Range = ExcelUtilForAddIn.GetNamedRange(currworkbook,"List", "List_Item_ALL");

            if (Range == null) return;
            xlRange = Sheet.Range["B5", "B" + (Range.Count + 4)];

            Excel.Range last = Sheet.Range["B5"].SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
            Sheet.Range["B5", "B" + last.Row].ClearContents();

            if (Range.Count > 1)
            {
                var arr = xlRange.Value;
                for (int i = 1; i <= Range.Count; i++)
                {
                    arr[i, 1] = Range.Cells[i, 1].Value;
                    CustomControlPane.AddListBoxItem(Range.Cells[i, 1].Value);
                }
                xlRange.Value = arr;
            }
            else
            {
                xlRange.Value = Range.Cells[1, 1].Text;
                CustomControlPane.AddListBoxItem(Range.Cells[1, 1].Text);
            }

            Sheet.Application.EnableEvents = enableEvet;
            Sheet.Application.ScreenUpdating = scrUpdate;
            Sheet.Application.Calculation = xlCalculation;

            //foreach (Excel.Range xlCell in Range.Cells.Cells)
            //{
            //    CustomControlPane.AddListBoxItem(xlCell[1, 1].Text);
            //}
        }
        /// <summary>
        /// Method to set properties of RECODE
        /// </summary>
        /// <param name="CurrentCell">Selected cell</param>
        private void SubstituteOperatorRECODESelected(Excel.Range CurrentCell)
        {
            Excel.Range Param1 = Sheet.Cells[CurrentCell.Row, Constants.DP.SubstituteParam1Column];
            if (Definitions.VariableDictionary.ContainsKey(Sheet.Cells[CurrentCell.Row, Constants.DP.SubstituteVariableColumn].Text) || InDECSTRange(CurrentCell.Row))
            {
                int paramcount = -1;
                QuestionSettings SubstituteQuestion = new QuestionSettings();
                if (Definitions.VariableDictionary.ContainsKey(Sheet.Cells[CurrentCell.Row, Constants.DP.SubstituteVariableColumn].Text))
                {
                    SubstituteQuestion = Definitions.VariableDictionary[Sheet.Cells[CurrentCell.Row, Constants.DP.SubstituteVariableColumn].Text];
                    paramcount = SubstituteQuestion.CategoryCount;
                }
                else
                {
                    paramcount = Constants.DP.MAX_DP_COLUMN - Constants.DP.SubstituteParam1Column;
                }

                string Formula = Constants.EqEqual + Constants.VariableList.ListItemSA;
                if (SubstituteQuestion.AnswerType == Constants.AnswerType.MA)
                {
                    Formula = Constants.EqEqual + Constants.VariableList.ListItemSAMA;
                }
                ExcelUtilForAddIn.AddValidation(Param1, Excel.XlDVType.xlValidateList, Type.Missing, Type.Missing, Formula, Type.Missing);
                if (!InDECSTRange(CurrentCell.Row))
                {
                    Param1.Validation.ErrorMessage = CommonResource.RECODE_PM1_ERRORMESSAGE;
                }
                else
                {
                    Param1.Validation.ShowError = false;
                }

                ExcelUtilForAddIn.SetCellInteriorColor(Param1, Constants.Color.LightBlue);
                SetToolTipforselectedcells(Param1, CommonResource.RECODE_PARAM1_TOOLTIPHEADER, CommonResource.RECODE_PARAM1_TOOLTIPDESC);

                for (int i = 0; i < paramcount; i++)
                {
                    ExcelUtilForAddIn.SetCellInteriorColor(Sheet.Cells[CurrentCell.Row, Constants.DP.SubstituteParam2Column + i], Constants.Color.Purple);
                    ExcelUtilForAddIn.AddValidation(Sheet.Cells[CurrentCell.Row, Constants.DP.SubstituteParam2Column + i], Excel.XlDVType.xlValidateTextLength, Excel.XlDVAlertStyle.xlValidAlertInformation, Excel.XlFormatConditionOperator.xlGreaterEqual, 0, Type.Missing);
                    if (SubstituteQuestion.Choices.Count > i)
                    {

                        string tooltipmessage = QC4Common.Common.CommonFunctions.FormatMsg(CommonResource.RECODE_PARM_N_TOOLTIPDESC, SubstituteQuestion.Choices[i]);
                        SetToolTipforselectedcells(Sheet.Cells[CurrentCell.Row, Constants.DP.SubstituteParam2Column + i], CommonResource.RECODE_PARM_N_TOOLTIPHEADER, tooltipmessage);
                    }

                }
            }
        }
        // If the user select DECSTDECEND PROGRAM  
        private void SubstituteInstructionDECENDSelected(Excel.Range CurrentCell)
        {
            try
            {
                bool DisplayMessage = true;
                bool bfound = false;
                for (int i = CurrentCell.Row - 1; i > 0; i--)
                {
                    string instrction = Sheet.Cells[i, CurrentCell.Column].Text;
                    switch (instrction)
                    {
                        case Constants.DP.InstructionDECEND:
                            DisplayMessage = true;
                            bfound = true;
                            break;
                        case Constants.DP.InstructionDECST:
                            if (decst_ProgramList.ContainsKey(Sheet.Cells[i, Constants.DP.SubstituteParam1Column].Text))
                            {
                                DisplayMessage = false;
                                decst_ProgramList[Sheet.Cells[i, Constants.DP.SubstituteParam1Column].Text].RowEnd = CurrentCell.Row;
                            }
                            bfound = true;
                            break;
                    }
                    if (bfound)
                        break;
                }
                if (DisplayMessage == true)
                {
                    MessageDialog.Error(CommonResource.DECST_NOTFOUND);

                }
                if (CurrentCell.Text == Constants.DP.InstructionDECEND)
                {
                    //PARAMETER DECLARATION FOR 11'TH AND 12'TH CELLS
                    Excel.Range param1 = Sheet.Cells[CurrentCell.Row + 1, Constants.DP.CriteriaVariableColumn];
                    param1.Select();
                    // if the user is going to select DECST operator FROM 8th column BUSINESS LOGIC will comes here
                    {
                        ExcelUtilForAddIn.AddValidation(param1, Excel.XlDVType.xlValidateList, Excel.XlDVAlertStyle.xlValidAlertStop, Excel.XlFormatConditionOperator.xlGreaterEqual, "=List_Item_ALL", Type.Missing);
                        ExcelUtilForAddIn.SetCellInteriorColor(param1, Constants.Color.Cream);

                        Excel.Range criteriavarcolumn = Sheet.Cells[CurrentCell.Row, Constants.DP.CriteriaVariableColumn];
                        criteriavarcolumn.Value = "";
                        ExcelUtilForAddIn.SetCellInteriorColor(criteriavarcolumn, Constants.Color.White);
                    }
                }
            }
            catch (ArgumentNullException arg)
            {
                _log.Error(arg.InnerException);
                // Console.WriteLine(arg.InnerException);
            }
        }
        // If the user select DECST Option from column 8'th AND create PROGRAM 
        private void SubstituteInstructionDECSTSelected(Excel.Range CurrentCell)
        {
            try
            {
                //PARAMETER DECLARATION FOR 11'TH AND 12'TH CELLS
                Excel.Range param1 = Sheet.Cells[CurrentCell.Row, Constants.DP.SubstituteParam1Column];
                Excel.Range param2 = Sheet.Cells[CurrentCell.Row, Constants.DP.SubstituteParam2Column];
                Excel.Range criteriavarcolumn = Sheet.Cells[CurrentCell.Row, Constants.DP.CriteriaVariableColumn];
                if (CurrentCell.Text == Constants.DP.InstructionDECST)
                {
                    // if the user is going to select DECST operator FROM 8th column BUSINESS LOGIC will comes here
                    {
                        criteriavarcolumn.Value = "";
                        ExcelUtilForAddIn.SetCellInteriorColor(criteriavarcolumn, Constants.Color.White);

                        ExcelUtilForAddIn.AddValidation(param1, Excel.XlDVType.xlValidateTextLength, Excel.XlDVAlertStyle.xlValidAlertStop, Excel.XlFormatConditionOperator.xlGreaterEqual, 0, Type.Missing);
                        SetToolTipforselectedcells(param1, CommonResource.DECST_PARAM1_TOOLTIPHEADER, CommonResource.DECST_PARAM1_TOOLTIPDESC);
                        ExcelUtilForAddIn.SetCellInteriorColor(param1, Constants.Color.LightBlue);

                        ExcelUtilForAddIn.AddValidation(param2, Excel.XlDVType.xlValidateList, Excel.XlDVAlertStyle.xlValidAlertStop, Excel.XlFormatConditionOperator.xlGreaterEqual, Constants.DP.DecscriptionList, Type.Missing);
                        param2.Validation.ErrorMessage = string.Format(CommonResource.DECST_PM2_ERRORMESSAGE, 1, 26);
                        SetToolTipforselectedcells(param2, CommonResource.DECST_PARAM2_TOOLTIPHEADER, CommonResource.DECST_PARAM2_TOOLTIPDESC);
                        ExcelUtilForAddIn.SetCellInteriorColor(param2, Constants.Color.Purple);
                    }

                    ExcelUtilForAddIn.SetSelectedCell(param1);
                }
            }
            catch (ArgumentNullException arg)
            {
                _log.Error(arg.InnerException);
                //Console.WriteLine(arg.InnerException);
            }
        }

        // If the user select CALL Option from column 8'th then Corresponding program names(created during decst) must
        //be binded into column 11
        private void SubstituteInstructionCALLSelected(Excel.Range CurrentCell)
        {
            try
            {
                Excel.Range Param1 = Sheet.Cells[CurrentCell.Row, Constants.DP.SubstituteParam1Column];
                Excel.Range InstCol = Sheet.Cells[CurrentCell.Row, Constants.DP.InstructionColumn];
                //  int decstPgmListCount = decst_ProgramList.Count;
                Excel.Range criteriavarcolumn = Sheet.Cells[CurrentCell.Row, Constants.DP.CriteriaVariableColumn];
                criteriavarcolumn.Value = "";
                ExcelUtilForAddIn.SetCellInteriorColor(criteriavarcolumn, Constants.Color.White);
                //List<int> DecstRowList = GetInstructionRowList(Constants.DP.InstructionDECST, true);
                SetDecstProgramList(true);


                if (decst_ProgramList.Keys.Count > 0)
                {
                    SetDECSTMethodList();
                    UpdateDECENDRow(false);
                    string Formula = Constants.EqEqual + Constants.DP.ListMethod;
                    ExcelUtilForAddIn.AddValidation(Param1, Excel.XlDVType.xlValidateList, Type.Missing, Type.Missing, Formula, Type.Missing);
                    // Param1.Validation.ErrorMessage = CommonResource.VALIDATION_MESSAGE_CALL;
                    ExcelUtilForAddIn.SetCellInteriorColor(Param1, Constants.Color.LightBlue);
                    ExcelUtilForAddIn.SetSelectedCell(Param1);
                    SetToolTipforselectedcells(Param1, CommonResource.CALL_PARAM1_TOOLTIP_HEADER, CommonResource.CALL_PARAM1_TOOLTIP_DESC);
                }
            }

            catch (ArgumentNullException arg)
            {
                _log.Error(arg.InnerException);
                // Console.WriteLine(arg.InnerException);
            }
        }


        private void SubstituteInstructionLDELSelected(Excel.Range CurrentCell)
        {

            //PARAMETER DECLARATION FOR 11'TH AND 12'TH CELLS
            Excel.Range param1 = Sheet.Cells[CurrentCell.Row, Constants.DP.SubstituteParam1Column];
            Excel.Range param2 = Sheet.Cells[CurrentCell.Row, Constants.DP.SubstituteParam2Column];

            // if the user is going to select LDEL operator FROM 8th column BUSINESS LOGIC will comes here
            try
            {
                if (CurrentCell.Text == Constants.DP.InstructionLDEL)
                {
                    Excel.Worksheet SheetData = ExcelUtilForAddIn.GetWorkSheetByCodeName(currworkbook,Constants.SheetCodeName.LDEL);// ExcelUtilForAddIn.GetWorksheetByName("LDEL");//ExcelUtilForAddIn.GetWorkSheetByCodeName(Constants.SheetCodeName.QuestionSetting)
                    Excel.Range start = SheetData.Cells[2, 2];
                    Excel.Range end = ExcelUtilForAddIn.EndxlRight(start);

                    ExcelUtilForAddIn.AddValidation(param1, Excel.XlDVType.xlValidateTextLength, Excel.XlDVAlertStyle.xlValidAlertStop, Excel.XlFormatConditionOperator.xlGreaterEqual, 0, Type.Missing);
                    SetToolTipforselectedcells(param1, CommonResource.LDEL_PARAM1_TOOLTIPHEADER, CommonResource.LDEL_PARAM1_TOOLTIPDESC);
                    ExcelUtilForAddIn.SetCellInteriorColor(param1, Constants.Color.Cream);
                    if (end.Column < 2) return;
                    Excel.Range range = SheetData.get_Range(start, end);
                    Definitions.optionList.Clear();

                    foreach (Excel.Range r in range)
                    {
                        Definitions.optionList.Add(r.Text);
                    }

                }
            }
            catch (ArgumentNullException arg)
            {
                _log.Error(arg.InnerException);
                //MessageBox.Show("NOT A CORRECT OPTION PLS SELECT A CORRECT OPTION", arg.InnerException.ToString());
            }
        }
        /// <summary>
        /// Set the validation, tooltip and interior color to the cells against the OMIT Row
        /// </summary>
        /// <param name="CurrentCell">Selected cell</param>
        private void SubstituteInstructionOMITSelected(Excel.Range CurrentCell)
        {
            bool inDecst = false;
            if (InDECSTRange(CurrentCell.Row))
            {
                inDecst = true;

            }
            try
            {
                Excel.Range Param1 = Sheet.Cells[CurrentCell.Row, Constants.DP.SubstituteParam1Column];
                string Formula = Constants.EqEqual + Constants.VariableList.ListItemALL; 
                Excel.Range startCell = Sheet.Cells[CurrentCell.Row, Constants.DP.SubstituteParam1Column];
                Excel.Range endCell = Sheet.Cells[CurrentCell.Row, Constants.DP.MAX_DP_COLUMN];
                Excel.Range paramRange = Sheet.Range[startCell, endCell];
                ExcelUtilForAddIn.AddValidation(paramRange, Excel.XlDVType.xlValidateList, Type.Missing, Type.Missing, Formula, Type.Missing);
                SetToolTipforselectedcells(paramRange, CommonResource.OMIT_PARAM1_TOOLTIPHEADER, CommonResource.OMIT_PARAM1_TOOLTIPDESC);
                ExcelUtilForAddIn.SetCellInteriorColor(paramRange, Constants.Color.LightGrey);
                if (inDecst)
                {
                    paramRange.Validation.ShowError = false;
                }

                ExcelUtilForAddIn.SetSelectedCell(Param1);
            }

            catch (ArgumentNullException arg)
            {
                _log.Error(arg.InnerException);// Console.WriteLine(arg.InnerException);
            }
        }
        private void SubstituteInstructionOMIT2Selected(Excel.Range CurrentCell)
        {
            bool inDecst = false;
            if (InDECSTRange(CurrentCell.Row))
            {
                inDecst = true;

            }
            try
            {
                Excel.Range Param1 = Sheet.Cells[CurrentCell.Row, Constants.DP.SubstituteParam1Column];
                Excel.Range Param2 = Sheet.Cells[CurrentCell.Row, Constants.DP.SubstituteParam2Column];
                ResetParamCells(CurrentCell.Row, Constants.DP.SubstituteParam1Column);
                string Formula = Constants.EqEqual + Constants.VariableList.ListItemALL;
                ExcelUtilForAddIn.AddValidation(Param1, Excel.XlDVType.xlValidateList, Type.Missing, Type.Missing, Formula, Type.Missing);
                SetToolTipforselectedcells(Param1, CommonResource.OMIT2_PARAM1_TOOLTIPHEADER, CommonResource.OMIT2_PARAM1_TOOLTIPDESC);
                ExcelUtilForAddIn.SetCellInteriorColor(Param1, Constants.Color.LightBlue);

                ExcelUtilForAddIn.AddValidation(Param2, Excel.XlDVType.xlValidateList, Type.Missing, Type.Missing, Formula, Type.Missing);
                SetToolTipforselectedcells(Param2, CommonResource.OMIT2_PARAM2_TOOLTIPHEADER1, CommonResource.OMIT2_PARAM2_TOOLTIPDESC1);
                ExcelUtilForAddIn.SetCellInteriorColor(Param2, Constants.Color.LightBlue);
                if (inDecst)
                {
                    Param1.Validation.ShowError = false;
                    Param2.Validation.ShowError = false;

                }
                ExcelUtilForAddIn.SetSelectedCell(Param1);

            }
            catch (ArgumentNullException arg)
            {
                _log.Error(arg.InnerException);// Console.WriteLine(arg.InnerException);
            }
        }


        /// <summary>
        /// Method to set properties of COUNT
        /// </summary>
        /// <param name="CurrentCell">Selected cell</param>
        private void SubstituteOperatorCOUNTSelected(Excel.Range CurrentCell)
        {
            Excel.Range param1 = Sheet.Cells[CurrentCell.Row, Constants.DP.SubstituteParam1Column];
            Excel.Range param2 = Sheet.Cells[CurrentCell.Row, Constants.DP.SubstituteParam2Column];
            if (Definitions.VariableDictionary.ContainsKey(Sheet.Cells[CurrentCell.Row, Constants.DP.SubstituteVariableColumn].Text) || InDECSTRange(CurrentCell.Row))
            {
                QuestionSettings SubstituteQuestion = new QuestionSettings();
                if (Definitions.VariableDictionary.ContainsKey(Sheet.Cells[CurrentCell.Row, Constants.DP.SubstituteVariableColumn].Text))
                {
                    SubstituteQuestion = Definitions.VariableDictionary[Sheet.Cells[CurrentCell.Row, Constants.DP.SubstituteVariableColumn].Text];
                }
                string Formula = Constants.EqEqual + Constants.VariableList.ListItemMA;
                switch (SubstituteQuestion.AnswerType)
                {
                    case Constants.AnswerType.N:
                    case "":

                        ExcelUtilForAddIn.AddValidation(param1, Excel.XlDVType.xlValidateList, Type.Missing, Type.Missing, Formula, Type.Missing);
                        param1.Validation.ErrorMessage = CommonResource.COUNT_PARAM_1_ERRORMESSAGE;
                        if (InDECSTRange(CurrentCell.Row))
                            param1.Validation.ShowError = false;


                        ExcelUtilForAddIn.SetCellInteriorColor(param1, Constants.Color.LightBlue);

                        ExcelUtilForAddIn.SetCellInteriorColor(param2, Constants.Color.Purple);
                        break;

                    case Constants.AnswerType.SA:
                        ExcelUtilForAddIn.AddValidation(param1, Excel.XlDVType.xlValidateList, Type.Missing, Type.Missing, Formula, Type.Missing);
                        param1.Validation.ErrorMessage = CommonResource.COUNT_PARAM_1_ERRORMESSAGE;
                        if (InDECSTRange(CurrentCell.Row))
                            param1.Validation.ShowError = false;

                        ExcelUtilForAddIn.SetCellInteriorColor(param1, Constants.Color.LightBlue);

                        ExcelUtilForAddIn.SetCellInteriorColor(param2, Constants.Color.LightGreen);

                        for (int i = 1; i <= SubstituteQuestion.CategoryCount; i++)
                        {
                            ExcelUtilForAddIn.SetCellInteriorColor(Sheet.Cells[CurrentCell.Row, Constants.DP.SubstituteParam2Column + i], Constants.Color.Purple);
                            ExcelUtilForAddIn.AddValidation(Sheet.Cells[CurrentCell.Row, Constants.DP.SubstituteParam2Column + i], Excel.XlDVType.xlValidateTextLength, Excel.XlDVAlertStyle.xlValidAlertInformation, Excel.XlFormatConditionOperator.xlGreaterEqual, 0, Type.Missing);
                            if (i <= SubstituteQuestion.Choices.Count)
                            {
                                string tooltipmessage = QC4Common.Common.CommonFunctions.FormatMsg(CommonResource.COUNT_PARAM_N_TOOLTIPDESC, SubstituteQuestion.Choices[i - 1]);
                                SetToolTipforselectedcells(Sheet.Cells[CurrentCell.Row, Constants.DP.SubstituteParam2Column + i], CommonResource.COUNT_PARAM_N_TOOLTIPHEADER, tooltipmessage);
                            }
                        }
                        break;
                }
                SetToolTipforselectedcells(param1, CommonResource.COUNT_PARAM1_TOOLTIPHEADER, CommonResource.COUNT_PARAM1_TOOLTIPDESC);

                ExcelUtilForAddIn.AddValidation(param2, Excel.XlDVType.xlValidateTextLength, Excel.XlDVAlertStyle.xlValidAlertInformation, Excel.XlFormatConditionOperator.xlGreaterEqual, 0, Type.Missing);
                SetToolTipforselectedcells(param2, CommonResource.COUNT_PARAM2_TOOLTIPHEADER, CommonResource.COUNT_PARAM2_TOOLTIPDESC);

            }
        }
        /// <summary>
        /// Method to set properties of CLASSS
        /// </summary>
        /// <param name="CurrentCell">Selected cell</param>
        private void SubstituteOperatorCLASSSelected(Excel.Range Currentcell)
        {
            Excel.Range param1 = Sheet.Cells[Currentcell.Row, Constants.DP.SubstituteParam1Column];
            Excel.Range param2 = Sheet.Cells[Currentcell.Row, Constants.DP.SubstituteParam2Column];
            Excel.Range param3 = Sheet.Cells[Currentcell.Row, Constants.DP.SubstituteParam3Column];
            if (Definitions.VariableDictionary.ContainsKey(Sheet.Cells[Currentcell.Row, Constants.DP.SubstituteVariableColumn].Text) || InDECSTRange(Currentcell.Row))
            {
                int paramcount = -1;
                QuestionSettings SubstituteQuestion = new QuestionSettings();
                if (Definitions.VariableDictionary.ContainsKey(Sheet.Cells[Currentcell.Row, Constants.DP.SubstituteVariableColumn].Text))
                {
                    SubstituteQuestion = Definitions.VariableDictionary[Sheet.Cells[Currentcell.Row, Constants.DP.SubstituteVariableColumn].Text];
                    paramcount = SubstituteQuestion.CategoryCount;
                }
                else
                {
                    paramcount = Constants.DP.MAX_DP_COLUMN - Constants.DP.SubstituteParam3Column;
                }


                string Formula = Constants.EqEqual + Constants.VariableList.ListItemN;
                ExcelUtilForAddIn.AddValidation(param1, Excel.XlDVType.xlValidateList, Type.Missing, Type.Missing, Formula, Type.Missing);

                ExcelUtilForAddIn.SetCellInteriorColor(param1, Constants.Color.LightBlue);
                SetToolTipforselectedcells(param1, CommonResource.CLASS_PARAM1_TOOLTIPHEADER, CommonResource.CLASS_PARAM1_TOOLTIPDESC);

                ExcelUtilForAddIn.AddValidation(param2, Excel.XlDVType.xlValidateList, Type.Missing, Type.Missing, Constants.DP.CLASSParam2List, Type.Missing);
                ExcelUtilForAddIn.SetCellInteriorColor(param2, Constants.Color.LightGreen);
                SetToolTipforselectedcells(param2, CommonResource.CLASS_PARAM2_TOOLTIPHEADER, CommonResource.CLASS_PARAM2_TOOLTIPDESC);

                ExcelUtilForAddIn.AddValidation(param3, Excel.XlDVType.xlValidateList, Type.Missing, Type.Missing, Constants.DP.CLASSParam3List, Type.Missing);
                ExcelUtilForAddIn.SetCellInteriorColor(param3, Constants.Color.LightGreen);
                SetToolTipforselectedcells(param3, CommonResource.CLASS_PARAM3_TOOLTIPHEADER, CommonResource.CLASS_PARAM3_TOOLTIPDESC);

                // 

                for (int i = 1; i <= paramcount; i++)
                {

                    ExcelUtilForAddIn.SetCellInteriorColor(Sheet.Cells[Currentcell.Row, Constants.DP.SubstituteParam3Column + i], Constants.Color.Purple);
                    ExcelUtilForAddIn.AddValidation(Sheet.Cells[Currentcell.Row, Constants.DP.SubstituteParam3Column + i], Excel.XlDVType.xlValidateTextLength, Excel.XlDVAlertStyle.xlValidAlertInformation, Excel.XlFormatConditionOperator.xlGreaterEqual, 0, Type.Missing);
                    if (SubstituteQuestion.Choices.Count >= i)
                    {

                        string value = QC4Common.Common.CommonFunctions.FormatMsg(CommonResource.CLASS_PARAM_N_TOOLTIPDESC, SubstituteQuestion.Choices[i - 1]);
                        SetToolTipforselectedcells(Sheet.Cells[Currentcell.Row, Constants.DP.SubstituteParam3Column + i], CommonResource.CLASS_PARAM_N_TOOLTIPHEADER, value);
                    }
                }
            }
        }
        private void SubstituteOperatorMCONVERTSelected(Excel.Range currentcell)
        {
            Excel.Range param1 = Sheet.Cells[currentcell.Row, Constants.DP.SubstituteParam1Column];
            Excel.Range param2 = Sheet.Cells[currentcell.Row, Constants.DP.SubstituteParam2Column];

            ExcelUtilForAddIn.AddValidation(param1, Excel.XlDVType.xlValidateList, Type.Missing, Type.Missing, Constants.DP.ParamList01, Type.Missing);
            ExcelUtilForAddIn.SetCellInteriorColor(param1, Constants.Color.LightGreen);
            SetToolTipforselectedcells(param1, CommonResource.MCONVERT_PARAM1_TOOLTIPHEADER, CommonResource.MCONVERT_PARAM1_TOOLTIPDESC);

            ExcelUtilForAddIn.AddValidation(param2, Excel.XlDVType.xlValidateTextLength, Excel.XlDVAlertStyle.xlValidAlertInformation, Excel.XlFormatConditionOperator.xlGreaterEqual, 0, Type.Missing);
            ExcelUtilForAddIn.SetCellInteriorColor(param2, Constants.Color.Purple);
            SetToolTipforselectedcells(param2, CommonResource.MCONVERT_PARAM2_TOOLTIPHEADER, CommonResource.MCONVERT_PARAM2_TOOLTIPDESC);
        }
        private bool InDECSTRange(int rownumber)
        {
            UpdateDECENDRow(false);
            foreach (KeyValuePair<string, DPCallMethod> kvp in decst_ProgramList)
            {
                if (kvp.Value.Rowstart < rownumber && (kvp.Value.RowEnd == 0 || kvp.Value.RowEnd > rownumber) && Sheet.Cells[kvp.Value.Rowstart, Constants.DP.OnOffColumn].Text == CommonResource.CELL_ON)
                {
                    //if (kvp.Value.RowEnd > rownumber && Sheet.Cells[kvp.Value.RowEnd, Constants.DP.OnOffColumn].Text == CommonResource.CELL_ON)
                    //{
                    return true;
                    //}
                }
            }


            Excel.Range startcell = Sheet.Cells[Constants.DP.ProUIstartRow, Constants.DP.InstructionColumn];
            Excel.Range endcell = Sheet.Cells[rownumber, Constants.DP.InstructionColumn];
            Excel.Range fornextRange = Sheet.Range[startcell, endcell];
            int forcounter = 0;
            if (fornextRange.Cells.Count == 1)
            {
                // If it's a single cell, the rest of the process is meaningless. 
                return true;
            }
            object[,] array = fornextRange.Value as object[,];
            int rowCount = array.GetLength(0);
            Parallel.For(1, rowCount + 1, i =>
            {
                var cellValue = array[i, 1];
                switch (cellValue)
                {
                    case Constants.DP.InstructionFOR:
                        Interlocked.Increment(ref forcounter);
                        break;
                    case Constants.DP.InstructionNEXT:
                        Interlocked.Decrement(ref forcounter);
                        break;
                }
            }
            );
            if (forcounter > 1)
            {
                MessageDialog.Warning(CommonResource.ERR_MSG_INVALID_FORNEXT);//"Invalid FOR NEXT"
                return true;
            }
            else if (forcounter == 1)
            {
                return true;
            }



            return false;
        }
        private void SubstituteOperatorAGGREGATESelected(Excel.Range Target)
        {
            string toolTipHeader = string.Format("[" + Target.Text + "]");// string toolTipHeader = string.Format("(" + Target.Text + ")");
            string formula = Constants.EqEqual + Constants.VariableList.ListItemSAN;
            Excel.Range startCell = Sheet.Cells[Target.Row, Constants.DP.SubstituteParam1Column];
            Excel.Range endCell = Sheet.Cells[Target.Row, Constants.DP.MAX_DP_COLUMN];
            Excel.Range paramRange = Sheet.Range[startCell, endCell];

            //for (int i = Constants.DP.SubstituteParam1Column; i <= Constants.DP.MAX_DP_COLUMN; i++)
            {
                //  Excel.Range paramcolumn = Sheet.Cells[Target.Row, i];
                ExcelUtilForAddIn.AddValidation(paramRange, Excel.XlDVType.xlValidateList, Type.Missing, Type.Missing, formula, Type.Missing);
                ExcelUtilForAddIn.SetCellInteriorColor(paramRange, Constants.Color.LightGrey);
                SetToolTipforselectedcells(paramRange, toolTipHeader, CommonResource.AGGREGATE_PARAMS_TOOTTIPDESC_);//SetToolTipforselectedcells(paramRange, toolTipHeader, CommonResource.AGGREGATE_PARAMS_TOOTTIPDESC);

                switch (Target.Text)
                {
                    case Constants.DP.SubstituteOperatorMIN:
                        paramRange.Validation.ErrorMessage = CommonResource.MIN_VALIDATION_ERRORMESSAGE;
                        break;

                    case Constants.DP.SubstituteOperatorMAX:


                        paramRange.Validation.ErrorMessage = CommonResource.MAX_VALIDATION_ERRORMESSAGE;
                        break; ;

                    case Constants.DP.SubstituteOperatorAVG:

                        paramRange.Validation.ErrorMessage = CommonResource.AVG_VALIDATION_ERRORMESSAGE;
                        break;


                }
                if (InDECSTRange(Target.Row)) paramRange.Validation.ShowError = false;

            }
        }
        private void SubstituteOperatorMTOSSelected(Excel.Range Target)
        {
            //naresh
            try
            {
                Excel.Range param1 = Sheet.Cells[Target.Row, Constants.DP.SubstituteParam1Column];
                Excel.Range param2 = Sheet.Cells[Target.Row, Constants.DP.SubstituteParam2Column];
                string formula = Constants.EqEqual + Constants.VariableList.ListItemMA;

                ExcelUtilForAddIn.AddValidation(param1, Excel.XlDVType.xlValidateList, Type.Missing, Type.Missing, formula, Type.Missing);
                ExcelUtilForAddIn.SetCellInteriorColor(param1, Constants.Color.LightBlue);
                SetToolTipforselectedcells(param1, CommonResource.MTOS_PARAM1_TOOTIPHEADER, CommonResource.MTOS_PARAM1_TOOTIPDESC);
                if (InDECSTRange(Target.Row)) param1.Validation.ShowError = false;


                ExcelUtilForAddIn.AddValidation(param2, Excel.XlDVType.xlValidateList, Type.Missing, Type.Missing, Constants.DP.MTOSParam2List, Type.Missing);
                param2.Validation.ShowError = false;
                ExcelUtilForAddIn.SetCellInteriorColor(param2, Constants.Color.LightGreen);
                SetToolTipforselectedcells(param2, CommonResource.MTOS_PARAM2_TOOTIPHEADER, CommonResource.MTOS_PARAM2_TOOTIPDESC);



            }
            catch (ArgumentNullException arg)
            {
                _log.Error(arg.InnerException);// Console.WriteLine(arg.InnerException);
            }
        }
        private void SubstituteOperatorADD1MINUS1Selected(Excel.Range Target)
        {

            Excel.Range param1 = Sheet.Cells[Target.Row, Constants.DP.SubstituteParam1Column];
            Excel.Range param2 = Sheet.Cells[Target.Row, Constants.DP.SubstituteParam2Column];

            string tooltipheader = string.Equals(Target.Text, Constants.DP.SubstituteOperatorADD1) ? CommonResource.ADD1_PARAM1_TOOLTIPHEADER : CommonResource.MINUS1_PARAM1_TOOLTIPHEADER;
            string tooltipdesc = string.Equals(Target.Text, Constants.DP.SubstituteOperatorADD1) ? CommonResource.ADD1_PARAM1_TOOLTIPDESC : CommonResource.MINUS1_PARAM1_TOOLTIPDESC;
            string formula = Constants.EqEqual + Constants.VariableList.ListItemSAMA;

            ExcelUtilForAddIn.AddValidation(param1, Excel.XlDVType.xlValidateList, Type.Missing, Type.Missing, formula, Type.Missing);
            SetToolTipforselectedcells(param1, tooltipheader, tooltipdesc);
            ExcelUtilForAddIn.SetCellInteriorColor(param1, Constants.Color.LightBlue);
            if (InDECSTRange(Target.Row)) param1.Validation.ShowError = false;

            ExcelUtilForAddIn.AddValidation(param2, Excel.XlDVType.xlValidateTextLength, Excel.XlDVAlertStyle.xlValidAlertStop, Excel.XlFormatConditionOperator.xlGreaterEqual, 0, Type.Missing);
            SetToolTipforselectedcells(param2, CommonResource.ADD1MINUS1_PARAM2_TOOLTIPHEADER, CommonResource.ADD1MINUS1_PARAM2_TOOLTIPDESC);
            ExcelUtilForAddIn.SetCellInteriorColor(param2, Constants.Color.Purple);

        }
        private void SubstituteOperatorADD2MINUS2Selected(Excel.Range Target)
        {
            Excel.Range param1 = Sheet.Cells[Target.Row, Constants.DP.SubstituteParam1Column];

            string tooltipheader = CommonResource.MINUS2_PARAM1_TOOLTIPHEADER;
            string tooltipdesc = CommonResource.MINUS2_PARAM1_TOOLTIPDESC;
            if (string.Equals(Target.Text, Constants.DP.SubstituteOperatorADD2))
            {
                tooltipheader = CommonResource.ADD2_PARAM1_TOOLTIPHEADER;
                tooltipdesc = CommonResource.ADD2_PARAM1_TOOLTIPDESC;
            }

            ExcelUtilForAddIn.AddValidation(param1, Excel.XlDVType.xlValidateTextLength, Excel.XlDVAlertStyle.xlValidAlertStop, Excel.XlFormatConditionOperator.xlGreaterEqual, 0, Type.Missing);
            ExcelUtilForAddIn.SetCellInteriorColor(param1, Constants.Color.Purple);
            SetToolTipforselectedcells(param1, tooltipheader, tooltipdesc);


        }
        private void SubstituteOperatorADD3Selected(Excel.Range currentcell)
        {
            Excel.Range param1 = Sheet.Cells[currentcell.Row, Constants.DP.SubstituteParam1Column];

            string tooltipheader = CommonResource.ADD3_PARAM1_TOOLTIPHEADER;
            string tooltipdesc = CommonResource.ADD3_PARAM1_TOOLTIPDESC;


            ExcelUtilForAddIn.AddValidation(param1, Excel.XlDVType.xlValidateList, Type.Missing, Type.Missing, Constants.DP.ParamList01, Type.Missing);
            ExcelUtilForAddIn.SetCellInteriorColor(param1, Constants.Color.LightGreen);
            SetToolTipforselectedcells(param1, tooltipheader, tooltipdesc);

            Excel.Range startCell = Sheet.Cells[currentcell.Row, Constants.DP.SubstituteParam2Column];
            Excel.Range endCell = Sheet.Cells[currentcell.Row, Constants.DP.MAX_DP_COLUMN];
            Excel.Range paramRange = Sheet.Range[startCell, endCell];

            string formula = Constants.EqEqual + Constants.VariableList.ListItemSAMA;
            // for (int i = Constants.DP.SubstituteParam2Column; i <= Constants.DP.MAX_DP_COLUMN; i++)
            {
                //  Excel.Range paramcolumn = Sheet.Cells[currentcell.Row, i];
                ExcelUtilForAddIn.AddValidation(paramRange, Excel.XlDVType.xlValidateList, Type.Missing, Type.Missing, formula, Type.Missing);
                ExcelUtilForAddIn.SetCellInteriorColor(paramRange, Constants.Color.LightGrey);
                SetToolTipforselectedcells(paramRange, CommonResource.ADD3_PARAM_N_TOOLTIPHEADER, CommonResource.AGGREGATE_PARAMS_TOOTTIPDESC_ADD3);//Redmine id - 174076  //  SetToolTipforselectedcells(paramRange, CommonResource.ADD3_PARAM_N_TOOLTIPHEADER, CommonResource.AGGREGATE_PARAMS_TOOTTIPDESC);
                if (InDECSTRange(currentcell.Row)) paramRange.Validation.ShowError = false;
            }
            // throw new NotImplementedException();
        }
        private void SubstituteOperatorINTEGRATESelected(Excel.Range currentcell)
        {
            Excel.Range param1 = Sheet.Cells[currentcell.Row, Constants.DP.SubstituteParam1Column];
            Excel.Range param2 = Sheet.Cells[currentcell.Row, Constants.DP.SubstituteParam2Column];

            ExcelUtilForAddIn.AddValidation(param1, Excel.XlDVType.xlValidateList, Type.Missing, Type.Missing, Constants.DP.ParamList01, Type.Missing);
            ExcelUtilForAddIn.SetCellInteriorColor(param1, Constants.Color.LightGreen);
            SetToolTipforselectedcells(param1, CommonResource.INTEGRATE_PARAM1_TOOLTIPHEADER, CommonResource.INTEGRATE_PARAM1_TOOLTIPDESC);
            string integrateparam2list = string.Empty;
            for (int i = 1; i <= Constants.DP.INTEGRATEParam2MAXValue; i++)
            {
                if (i > 1)
                    integrateparam2list += ",";

                integrateparam2list += i.ToString();
            }

            ExcelUtilForAddIn.AddValidation(param2, Excel.XlDVType.xlValidateList, Type.Missing, Type.Missing, integrateparam2list, Type.Missing);
            ExcelUtilForAddIn.SetCellInteriorColor(param2, Constants.Color.LightGreen);
            SetToolTipforselectedcells(param2, CommonResource.INTEGRATE_PARAM2_TOOLTIPHEADER, CommonResource.INTEGRATE_PARAM2_TOOLTIPDESC);

        }
        private void SubstituteOperatorJOINTSelected(Excel.Range currentcell)
        {
            //Sheet.Application.EnableEvents = false;
            string toolTipHeader = string.Format("(" + currentcell.Text + ")");
            string formula = Constants.EqEqual + Constants.VariableList.ListItemSAMA;
            Excel.Range variableRange = Sheet.Cells[currentcell.Row, Constants.DP.SubstituteParam1Column];
            Excel.Range ValueRange = Sheet.Cells[currentcell.Row, Constants.DP.SubstituteParam2Column];
            for (int i = Constants.DP.SubstituteParam1Column; i <= Constants.DP.MAX_DP_COLUMN; i += 2)
            {

                Excel.Range paramcolumn = Sheet.Cells[currentcell.Row, i];
                variableRange = variableRange.Application.Union(variableRange, paramcolumn);

                Excel.Range valuecell = Sheet.Cells[currentcell.Row, i + 1];
                ValueRange = ValueRange.Application.Union(ValueRange, valuecell);

                //  _log.Info("----------------------------------------------------------"+ paramcolumn.AddressLocal[Excel.XlReferenceStyle.xlA1]);

            }
            ExcelUtilForAddIn.AddValidation(variableRange, Excel.XlDVType.xlValidateList, Type.Missing, Type.Missing, formula, Type.Missing);
            ExcelUtilForAddIn.SetCellInteriorColor(variableRange, Constants.Color.LightGrey);
            SetToolTipforselectedcells(variableRange, CommonResource.JOINT_PARAM1_TOOLTIPHEADER, CommonResource.JOINT_PARAM1_TOOLTIPDESC);
            if (InDECSTRange(currentcell.Row)) variableRange.Validation.ShowError = false;

            ExcelUtilForAddIn.AddValidation(ValueRange, Excel.XlDVType.xlValidateTextLength, Excel.XlDVAlertStyle.xlValidAlertInformation, Excel.XlFormatConditionOperator.xlGreaterEqual, 0, Type.Missing);
            ExcelUtilForAddIn.SetCellInteriorColor(ValueRange, Constants.Color.LightBlue);
            SetToolTipforselectedcells(ValueRange, CommonResource.JOINT_PARAM2_TOOLTIPHEADER, CommonResource.JOINT_PARAM2_TOOLTIPDESC);
            //Sheet.Application.EnableEvents = true;
        }
        private void SubstituteOperatorEQUALSelected(Excel.Range currentcell)
        {
            Excel.Range param1 = Sheet.Cells[currentcell.Row, Constants.DP.SubstituteParam1Column];
            Excel.Range variablecell = Sheet.Cells[currentcell.Row, Constants.DP.SubstituteVariableColumn];
            if (Definitions.VariableDictionary.ContainsKey(variablecell.Text) || InDECSTRange(currentcell.Row))
            {
                QuestionSettings questionsetting = new QuestionSettings();
                string paramlist = Constants.EqEqual;
                string tooltipheader = string.Empty;
                if (Definitions.VariableDictionary.ContainsKey(variablecell.Text))
                {
                    questionsetting = Definitions.VariableDictionary[variablecell.Text];


                    switch (questionsetting.AnswerType)
                    {
                        case Constants.AnswerType.N:
                            paramlist += Constants.VariableList.ListItemSAN;
                            tooltipheader = "SAN";
                            break;
                        case Constants.AnswerType.SA:
                            paramlist += Constants.VariableList.ListItemSA;
                            tooltipheader = "SA";
                            break;
                        case Constants.AnswerType.MA:
                            paramlist += Constants.VariableList.ListItemSAMA;
                            tooltipheader = "SAMA";
                            break;
                        case Constants.AnswerType.FA:
                            //paramlist += Constants.VariableList.ListItemFA;
                            paramlist += string.Empty;
                            tooltipheader = "FA";
                            break;
                    }
                }
                else
                {
                    paramlist += Constants.VariableList.ListItemALL;
                    tooltipheader = CommonResource.INFO_TOOLTIP_TARGET_VARIABLE;// "Target Variable";
                }
                if (paramlist == "=")
                    ExcelUtilForAddIn.AddValidation(param1, Excel.XlDVType.xlValidateTextLength, Type.Missing, Excel.XlFormatConditionOperator.xlGreaterEqual, 0, Type.Missing);
                else
                    ExcelUtilForAddIn.AddValidation(param1, Excel.XlDVType.xlValidateList, Type.Missing, Type.Missing, paramlist, Type.Missing);
                param1.Validation.ShowError = false;
                SetToolTipforselectedcells(param1, string.Format(CommonResource.EQUAL_PARAM1_TOOLTIPHEADER, tooltipheader), CommonResource.EQUAL_PARAM1_TOOLTIPDESC);
                ExcelUtilForAddIn.SetCellInteriorColor(param1, Constants.Color.LightGrey);


            }

            //throw new NotImplementedException();
        }
        private string SetToolTipforselectedcells(Excel.Range range, string InputTitle, string InputMessage)
        {
            string StrTitle = InputTitle;
            string StrMessage = InputMessage;
            bool result1 = String.IsNullOrEmpty(StrTitle);
            bool result2 = String.IsNullOrEmpty(StrMessage);
            if (result1 == false && result2 == false)
            {
                range.Validation.InputTitle = InputTitle;
                range.Validation.InputMessage = InputMessage;
            }

            return string.Empty;
        }

        private void ResetParamCells(int rowNumber, int startcolumn, bool resetcolor = true, bool resetvalidation = true)
        {
            if (startcolumn > Constants.DP.MAX_DP_COLUMN) return;




            //need to get range nd reset
            Excel.Range start = DataProcess.Sheet.Cells[rowNumber, startcolumn];
            Excel.Range end = DataProcess.Sheet.Cells[rowNumber, Constants.DP.MAX_DP_COLUMN];
            Excel.Range range = DataProcess.Sheet.get_Range(start, end);

            ExcelUtilForAddIn.ResetCell(range, resetcolor, resetvalidation);

            ////old code  //191  commente to speedup
            //for (int i = startcolumn; i <= Constants.DP.MAX_DP_COLUMN; i++)
            //{
            //    ExcelUtilForAddIn.ResetCell(Sheet.Cells[rowNumber, i], resetcolor, resetvalidation);
            //}

        }
        private void ResetSubstituteOperatorCell(int rowNumber)
        {
            ExcelUtilForAddIn.ResetCell(Sheet.Cells[rowNumber, Constants.DP.SubstituteOperatorColumn]);
            ResetParamCells(rowNumber, Constants.DP.SubstituteParam1Column);
        }


        private void ResetpARAM12toNCell(int rowNumber)
        {

            ExcelUtilForAddIn.ResetCell(Sheet.Cells[rowNumber, Constants.DP.SubstituteVariableColumn]);
            ExcelUtilForAddIn.ResetCell(Sheet.Cells[rowNumber, Constants.DP.SubstituteOperatorColumn]);
            ResetParamCells(rowNumber, Constants.DP.SubstituteParam2Column);
        }

        private void ResetVariableAndOperatorCell(int rowNumber)
        {
            ExcelUtilForAddIn.ResetCell(Sheet.Cells[rowNumber, Constants.DP.SubstituteVariableColumn]);
            ExcelUtilForAddIn.ResetCell(Sheet.Cells[rowNumber, Constants.DP.SubstituteOperatorColumn]);
            ResetParamCells(rowNumber, Constants.DP.SubstituteParam1Column);
        }

        private void SubstituteOperatorChanged(Excel.Range Currentcell)
        {
            ResetParamCells(Currentcell.Row, Constants.DP.SubstituteParam1Column);
            bool setcrosscell = false;
            switch (Currentcell.Text)
            {
                case Constants.DP.SubstituteOperatorRECODE:
                    setcrosscell = true;
                    SubstituteOperatorRECODESelected(Currentcell);
                    break;

                case Constants.DP.SubstituteOperatorMIN:
                case Constants.DP.SubstituteOperatorMAX:
                case Constants.DP.SubstituteOperatorAVG:
                case Constants.DP.SubstituteOperatorSUM:
                    SubstituteOperatorAGGREGATESelected(Currentcell);
                    break;

                case Constants.DP.SubstituteOperatorMTOS:
                    setcrosscell = true;
                    SubstituteOperatorMTOSSelected(Currentcell);
                    break;

                case Constants.DP.SubstituteOperatorADD1:
                case Constants.DP.SubstituteOperatorMINUS1:
                    SubstituteOperatorADD1MINUS1Selected(Currentcell);
                    break;

                case Constants.DP.SubstituteOperatorADD2:
                case Constants.DP.SubstituteOperatorMINUS2:
                    SubstituteOperatorADD2MINUS2Selected(Currentcell);
                    break;

                case Constants.DP.SubstituteOperatorADD3:
                    setcrosscell = true;
                    SubstituteOperatorADD3Selected(Currentcell);
                    break;
                case Constants.DP.SubstituteOperatorEQUAL:
                    setcrosscell = true;
                    SubstituteOperatorEQUALSelected(Currentcell);
                    break;

                case Constants.DP.SubstituteOperatorCOUNT:
                    setcrosscell = true;
                    SubstituteOperatorCOUNTSelected(Currentcell);
                    break;

                case Constants.DP.SubstituteOperatorCLASS:
                    setcrosscell = true;
                    SubstituteOperatorCLASSSelected(Currentcell);
                    break;

                case Constants.DP.SubstituteOperatorMCONVERT:
                    setcrosscell = true;
                    SubstituteOperatorMCONVERTSelected(Currentcell);
                    break;

                case Constants.DP.SubstituteOperatorINTEGRATE:
                    setcrosscell = true;
                    SubstituteOperatorINTEGRATESelected(Currentcell);
                    break;
                case Constants.DP.SubstituteOperatorJOINT:
                    SubstituteOperatorJOINTSelected(Currentcell);
                    break;
                case Constants.DP.SubstituteOperatorCOMPUTE:
                    SubstituteOperatorCOMPUTESelected(Currentcell);
                    break;

            }
            SetCrossCellValue(Currentcell, setcrosscell);
            ExcelUtilForAddIn.SetSelectedCell(Sheet.Cells[Currentcell.Row, Constants.DP.SubstituteParam1Column]);

        }

        private void SubstituteOperatorCOMPUTESelected(Excel.Range currentcell)
        {
            Excel.Range param1 = Sheet.Cells[currentcell.Row, Constants.DP.SubstituteParam1Column];

            param1.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            param1.IndentLevel = 0;// 2-1-2020

            ExcelUtilForAddIn.SetCellInteriorColor(param1, Constants.Color.LightGrey);
            ExcelUtilForAddIn.AddValidation(param1, Excel.XlDVType.xlValidateTextLength, Excel.XlDVAlertStyle.xlValidAlertInformation, Excel.XlFormatConditionOperator.xlGreaterEqual, 0, Type.Missing);
            param1.Validation.ShowError = false;
            SetToolTipforselectedcells(param1, CommonResource.COMPUTE_TOOLTIP_HEADER, CommonResource.COMPUTE_TOOLTIP_DESC_);//SetToolTipforselectedcells(param1, CommonResource.COMPUTE_TOOLTIP_HEADER, CommonResource.COMPUTE_TOOLTIP_DESC);
        }

        private void SubstituteParam1Changed(Excel.Range ChangedCell)
        {

            Excel.Range InstructionCell = Sheet.Cells[ChangedCell.Row, Constants.DP.InstructionColumn];
            ExcelUtilForAddIn.SetFontColor(Sheet.Cells[ChangedCell.Row, ChangedCell.Column], Constants.Color.Black);
            Excel.Range OperatorCell = Sheet.Cells[ChangedCell.Row, Constants.DP.SubstituteOperatorColumn];


            string Contents = ChangedCell.Text;
            if (string.IsNullOrEmpty(Contents)) return;

            switch (InstructionCell.Text)
            {
                case Constants.DP.InstructionLDEL:
                    LDELValidation(ChangedCell);
                    break;

                case Constants.DP.InstructionCALL:
                    CALLParam1Changed(ChangedCell);
                    break;
                case Constants.DP.InstructionDECST:
                    //  SubstituteParam2ColumnForDECST(ChangedCell, flagpm1);
                    DECSTParam1Changed(ChangedCell);
                    break;

                case Constants.DP.InstructionOMIT:
                case Constants.DP.InstructionLISTUP:
                    ExcludeListParamChanged(ChangedCell);
                    break;

                case Constants.DP.InstructionOMIT2:
                    //   OMIT2ParamChanged(ChangedCell);
                    break;


                case Constants.DP.InstructionTHEN:
                    switch (OperatorCell.Text)
                    {
                        case Constants.DP.SubstituteOperatorADD2:
                        case Constants.DP.SubstituteOperatorMINUS2:
                            Excel.Range VariableCell = Sheet.Cells[ChangedCell.Row, Constants.DP.SubstituteVariableColumn];
                            if (Definitions.VariableDictionary.ContainsKey(VariableCell.Text) && !InDECSTRange(ChangedCell.Row))
                            {
                                ValidateNumericCell(ChangedCell, 1, Definitions.VariableDictionary[VariableCell.Text].CategoryCount, true, Constants.DP.SubstituteVariableColumn);
                            }
                            break;

                        case Constants.DP.SubstituteOperatorCOUNT:
                            ResetParamCells(ChangedCell.Row, Constants.DP.SubstituteParam2Column, false, false);
                            break;
                        //case Constants.DP.SubstituteOperatorRECODE:
                        //    ResetParamCells(ChangedCell.Row, Constants.DP.SubstituteParam2Column, false, false);
                        //    break;

                        case Constants.DP.SubstituteOperatorCOMPUTE:
                            ValidateCOMPUTEParam(ChangedCell);
                            break;

                        case Constants.DP.SubstituteOperatorEQUAL:
                            VariableCell = Sheet.Cells[ChangedCell.Row, Constants.DP.SubstituteVariableColumn];
                            ValidateCorrectData(ChangedCell, VariableCell.Text);
                            break;

                            //case Constants.DP.SubstituteOperatorMTOS:
                            //    if (!ValidateMTOSPARAM(ChangedCell))
                            //    {
                            //        ShowAlert(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW + " " + ChangedCell.Row.ToString(), string.Format(CommonResource.ERR_MSG_VALIDATION_CORRECTDATA, "!"), "");
                            //        ExcelUtilForAddIn.SetFontColor(ChangedCell, Constants.Color.Red);
                            //    }
                            //    break;
                    }
                    // SetNextCellSelection(ChangedCell);

                    break;

            }


        }

        private void SetNextCellSelection(Excel.Range changedCell)
        {
            Excel.Range nextcell = changedCell;
            Excel.Range endcell = ExcelUtilForAddIn.EndxlRight(nextcell);
            if (string.IsNullOrEmpty(changedCell.Text)) return;
            if (changedCell.Font.ColorIndex == 1)
            {
                nextcell = Sheet.Cells[changedCell.Row, changedCell.Column + 1];

                //<<<<<<< Updated upstream
                //if (nextcell.Validation.Value == false)
                //{
                //    nextcell = Sheet.Cells[changedCell.Row, Constants.DP.CriteriaVariableColumn];
                //}
                //=======
                if (nextcell.Validation.Value == false)
                {
                    nextcell = Sheet.Cells[changedCell.Row, Constants.DP.CriteriaVariableColumn];
                }
                //>>>>>>> Stashed changes
            }
            nextcell.Select();
        }

        private void ValidateCOMPUTEParam(Excel.Range changedCell)
        {
            string targetvariable = string.Empty;
            string formatedcontents = "";
            string cellcontent = changedCell.Text;
            string Computestring = changedCell.Text;
            string replacestring = GetReplacecharacter(cellcontent);
            cellcontent = cellcontent.Replace('[', '@');
            cellcontent = cellcontent.Replace(']', '@');
            string[] splitcontent = cellcontent.Split('@');
            foreach (string item in splitcontent)
            {
                if (Definitions.VariableDictionary.ContainsKey(item))
                {
                    Excel.Range subvariablerange = Sheet.Cells[changedCell.Row, Constants.DP.SubstituteVariableColumn];
                    if (
                string.IsNullOrEmpty(replacestring) &&
                (string.IsNullOrEmpty(GetReplacecharacter(targetvariable)) ||
                 (string.IsNullOrEmpty(GetReplacecharacter(targetvariable)) && Definitions.VariableDictionary.ContainsKey(targetvariable)))//#211586
               )
                    {
                        if (string.IsNullOrEmpty(subvariablerange.Text) || !Definitions.VariableDictionary.ContainsKey(subvariablerange.Text))
                        {
                            // ExcelUtilForAddIn.SetFontColor(changedCell, Constants.Color.Red);
                            return;
                        }
                        QuestionSettings qssubvariable = Definitions.VariableDictionary[subvariablerange.Text];
                        QuestionSettings qsObject = Definitions.VariableDictionary[item];
                        if ((qssubvariable.AnswerType.Equals(Constants.AnswerType.N) && ( qsObject.AnswerType.Equals(Constants.AnswerType.SA) || qsObject.AnswerType.Equals(Constants.AnswerType.N)))
                            ||
                            (qssubvariable.AnswerType.Equals(Constants.AnswerType.FA) && (qsObject.AnswerType.Equals(Constants.AnswerType.FA) || qsObject.AnswerType.Equals(Constants.AnswerType.SA) || qsObject.AnswerType.Equals(Constants.AnswerType.N) || qsObject.AnswerType.Equals(Constants.AnswerType.MA)))
                            )
                        {
                            formatedcontents += "1";
                        }
                        else
                        {
                            MessageDialog.ErrorOk(CommonResource.ERR_MSG_COMPUTE_MA_FA_NOT_ALLOWED);//ERR_MSG_COMPUTE_MA_NOT_ALLOWED  MessageDialog.ErrorOk("Questions MA not allowed");
                            ExcelUtilForAddIn.SetFontColor(changedCell, Constants.Color.Red);
                            return;
                        }
                    }
                }
                else
                {
                    formatedcontents += item;
                }
            }
            if (!formatedcontents.StartsWith(Constants.EqEqual))//#211586//(!formatedcontents[0].Equals(Constants.EqEqual))
            {
                formatedcontents = "=" + formatedcontents;
            }
            Excel.Range targetvariablerange = Sheet.Cells[changedCell.Row, Constants.DP.SubstituteVariableColumn];
            targetvariable = targetvariablerange.Text;
            if (//#211586
                !string.IsNullOrEmpty(replacestring) &&
                (!string.IsNullOrEmpty(GetReplacecharacter(targetvariable)) ||
                 (string.IsNullOrEmpty(GetReplacecharacter(targetvariable)) && Definitions.VariableDictionary.ContainsKey(targetvariable)))
               )
            {
                formatedcontents = Computestring.Replace(replacestring, string.Empty);
                Regex itemNameRegex = new Regex(Constants.ITEMNAME_PATTERN);
                List<string> tmpNamesList = new List<string>();
                foreach (Match match in itemNameRegex.Matches(formatedcontents))
                {
                    string tmpName = match.Groups[1].Value;
                    if (tmpNamesList.Contains(tmpName)) continue;
                    tmpNamesList.Add(tmpName);
                }
                foreach (string name in tmpNamesList)
                {
                    formatedcontents = formatedcontents.Replace(name, "1");
                }
                formatedcontents = formatedcontents.Replace("[", string.Empty);
                formatedcontents = formatedcontents.Replace("]", string.Empty);
            }
            var evalres = Sheet.Application.Evaluate(formatedcontents);
            switch (evalres)
            {
                case Constants.DP.ErrDiv0:
                case Constants.DP.ErrGettingData:
                case Constants.DP.ErrName:
                case Constants.DP.ErrNA:
                case Constants.DP.ErrNull:
                case Constants.DP.ErrNum:
                case Constants.DP.ErrRef:
                case Constants.DP.ErrValue:
                    MessageDialog.ErrorOk(CommonResource.ERR_MSG_CONTENT_INVALID);//"The content is invalid"
                    ExcelUtilForAddIn.SetFontColor(changedCell, Constants.Color.Red);
                    break;
            }
        }

        private void ValidateCorrectData(Excel.Range changedCell, string targetVariable)
        {
            if (InDECSTRange(changedCell.Row))
            {
                Match match = Regex.Match(changedCell.Text, @"[\[A-Za-z\]]");
                if (match.Success)
                {
                    return;
                }
            }
            QuestionSettings qstnDet = Definitions.VariableDictionary[targetVariable];
            if (qstnDet.AnswerType == Constants.AnswerType.MA)
            {
                string param1Str = changedCell.Text;
                param1Str = param1Str.StartsWith("!") ? param1Str.TrimStart('!') : param1Str;
                if (!Definitions.VariableDictionary.ContainsKey(param1Str))
                {
                    string[] arrData = param1Str.Split(',', '/', '-');
                    if (arrData.Length > 0)
                    {
                        if (changedCell.Text != "*" && changedCell.Text != "DK")
                        {
                            foreach (string arrVal in arrData)
                            {
                                if (arrVal.Trim().Length > 0)
                                {
                                    int number;//
                                    string paramvalue = arrVal;
                                    //if (!string.IsNullOrEmpty(paramvalue) && (paramvalue).StartsWith("!"))
                                    //{
                                    //    paramvalue = paramvalue.Replace("!", string.Empty);
                                    //}
                                    if (paramvalue.Contains("!"))
                                    {

                                        // ERR_MSG_VALIDATION_CORRECTDATA
                                        ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, changedCell.Row.ToString()) + " ", string.Format(CommonResource.ERR_MSG_VALIDATION_CORRECTDATA, "!"), "");
                                        ExcelUtilForAddIn.SetFontColor(changedCell, Constants.Color.Red);
                                        return;
                                    }
                                    if (!int.TryParse(paramvalue, out number))
                                    {
                                        MessageDialog.ErrorOk(CommonResource.ERR_MSG_SET_NUMERIC_VALUE);//"Set a numeric value."
                                        ExcelUtilForAddIn.SetFontColor(changedCell, Constants.Color.Red);
                                        return;
                                    }
                                    else if (number <= 0)
                                    {
                                        MessageDialog.ErrorOk(CommonResource.ERR_MSG_SET_AN_INTEGER_IN_THE_RANGE_OF + " [ 1 - " + qstnDet.CategoryCount + " ].");//ERR_MSG_SET_AN_INTEGER_IN_THE_RANGE_OF //Set an integer in the range of
                                        ExcelUtilForAddIn.SetFontColor(changedCell, Constants.Color.Red);
                                        return;
                                    }
                                    else if (number > qstnDet.CategoryCount)
                                    {
                                        MessageDialog.ErrorOk(CommonResource.ERR_MSG_SET_AN_INTEGER_IN_THE_RANGE_OF + " [ 1 - " + qstnDet.CategoryCount + " ].");//Set an integer in the range of
                                        ExcelUtilForAddIn.SetFontColor(changedCell, Constants.Color.Red);
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageDialog.ErrorOk(CommonResource.ERR_MSG_NO_PARAMETER_TO_PROCESS);//"No parameter to process"
                        ExcelUtilForAddIn.SetFontColor(changedCell, Constants.Color.Red);
                        return;
                    }
                }
            }
            else if (qstnDet.AnswerType == Constants.AnswerType.SA)
            {
                int number; string paramvalue = changedCell.Text;
                if (!Definitions.VariableDictionary.ContainsKey(changedCell.Text))
                {
                    if (!string.IsNullOrEmpty(changedCell.Text) && (changedCell.Text).StartsWith("!"))
                    {
                        paramvalue = paramvalue.Replace("!", string.Empty);
                    }
                    // Variable selected
                    if (changedCell.Text != "*" && changedCell.Text != "DK")
                    {
                        if (!int.TryParse(paramvalue, out number))

                        {
                            MessageDialog.ErrorOk(CommonResource.ERR_MSG_RANGE_CANNOT_BE_DESIGNATED);//"Range cannot be designated."
                            ExcelUtilForAddIn.SetFontColor(changedCell, Constants.Color.Red);
                            return;
                        }

                        else if (number <= 0)
                        {
                            MessageDialog.ErrorOk(CommonResource.ERR_MSG_SET_AN_INTEGER_IN_THE_RANGE_OF + " [ 1 - " + qstnDet.CategoryCount + " ].");//Set an integer in the range of
                            ExcelUtilForAddIn.SetFontColor(changedCell, Constants.Color.Red);
                            return;
                        }

                        else if (number > qstnDet.CategoryCount)
                        {

                            MessageDialog.ErrorOk(CommonResource.ERR_MSG_SET_AN_INTEGER_IN_THE_RANGE_OF + " [ 1 - " + qstnDet.CategoryCount + " ].");//Set an integer in the range of
                            ExcelUtilForAddIn.SetFontColor(changedCell, Constants.Color.Red);
                            return;
                        }
                    }
                }

            }
            else if (qstnDet.AnswerType == Constants.AnswerType.N)
            {
                Regex regex = new Regex(@"(^-?\d+\.\d+$)|(^\d+\.\d+$)|(^\d+$)|(^-?\d+$)");//"^-?\\d*(\\.\\d+)?$"               
                if (!Definitions.VariableDictionary.ContainsKey(changedCell.Text))
                {
                    if (changedCell.Text != "*" && changedCell.Text != "DK")
                    {
                        if (!regex.Match(changedCell.Text).Success)//.Match(str).Success)   // !regex.IsMatch(changedCell.Text)
                        {
                            MessageDialog.ErrorOk(CommonResource.ERR_MSG_INPUT_NUMERIC_OR_SELECT_VALUE_FROM_LIST);//"Input numeric value or select a value from the list"// ShowAlert(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW + " " + kvp.Key.ToString(), string.Format(CommonResource.LDEL_VALIDATION_ERROR1, Constants.DP.SubstituteOperatorEQUAL), string.Empty);
                            ExcelUtilForAddIn.SetFontColor(changedCell, Constants.Color.Red);
                            return;
                        }
                    }

                }
                //int number;
                //if (!Definitions.VariableDictionary.ContainsKey(changedCell.Text))
                //{
                //    if (changedCell.Text != "*" && changedCell.Text != "DK")
                //    {
                //        if (!int.TryParse(changedCell.Text, out number))
                //        {
                //            MessageDialog.ErrorOk("Input numeric value or select a value from the list");
                //            ExcelUtilForAddIn.SetFontColor(changedCell, Constants.Color.Red);
                //            return;
                //        }
                //    }
                //}
            }
        }


        private void SetDecstProgramList(bool ignoreONOFF = false)
        {
            try
            {
                List<int> DecstRowList = GetInstructionRowList(Constants.DP.InstructionDECST, ignoreONOFF);

                decst_ProgramList.Clear();
                foreach (int row in DecstRowList)
                {
                    Excel.Range decstparam = Sheet.Cells[row, Constants.DP.SubstituteParam1Column];
                    Excel.Range decstparam2 = Sheet.Cells[row, Constants.DP.SubstituteParam2Column];
                    if (!string.IsNullOrEmpty(decstparam.Text) && !string.IsNullOrEmpty(decstparam2.Text))
                    {
                        DPCallMethod DECSTMethod = new DPCallMethod();
                        DECSTMethod.MethodName = decstparam.Text;
                        DECSTMethod.Rowstart = decstparam.Row;
                        DECSTMethod.paramcount = Convert.ToInt32(decstparam2.Text);
                        decst_ProgramList.Add(decstparam.Text, DECSTMethod);

                    }
                }

            }
            catch (Exception ex)
            {
                _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }
        private void DECSTParam1Changed(Excel.Range changedCell)
        {
            //   Excel.Range Param1Column = Sheet.Cells[changedCell.Row, Constants.DP.SubstituteParam1Column];
            if (decst_ProgramList.ContainsKey(changedCell.Text))
            {
                MessageDialog.Warning(CommonResource.DECST_ALREADY_EXISTS);
                return;
            }
            ResetParamCells(changedCell.Row, Constants.DP.SubstituteParam2Column, false, false);
            //SetDecstProgramList(true);
            //if (decst_ProgramList.Count > 0)
            //{
            //    SetDECSTMethodList();
            //}
            if (changedCell.Text == "")
            {
                return;
            }
            //if (!decst_ProgramList.ContainsKey(changedCell.Text))
            //{
            //    //decst_ProgramList.Remove(SubParam1ColumnText);


            //}
            //else
            //{

            //}


        }

        //private void SubstituteParam2ColumnForDECST(Excel.Range ChangedCell, int flagpm1)
        //{
        //    Excel.Range Param1Column = Sheet.Cells[ChangedCell.Row, Constants.DP.SubstituteParam1Column];

        //    if (ChangedCell.Text == "")
        //    {
        //        return;
        //    }

        //    if (flagpm1 == 1 && !string.IsNullOrEmpty(Param1Column.Text))
        //    {
        //        Excel.Range param1 = Sheet.Cells[ChangedCell.Row, Constants.DP.SubstituteParam1Column];

        //        int flag = 0;

        //        DECSTParam2Changed(ChangedCell, flag);
        //    }

        //    else if (flagpm1 == 0 && !string.IsNullOrEmpty(Param1Column.Text))
        //    {
        //        Excel.Range param1 = Sheet.Cells[ChangedCell.Row, Constants.DP.SubstituteParam1Column];

        //        int flag = 1;

        //        DECSTParam2Changed(ChangedCell, flag);
        //    }
        //}
        /// <summary>
        /// Method to set properties of SubstituteParam2
        /// </summary>
        /// <param name="CurrentCell">Changed cell</param>
        private void SubstituteParam2Changed(Excel.Range ChangedCell)
        {
            Excel.Range operatorCell = Sheet.Cells[ChangedCell.Row, Constants.DP.SubstituteOperatorColumn];

            Excel.Range InstructionCell = Sheet.Cells[ChangedCell.Row, Constants.DP.InstructionColumn];


            ExcelUtilForAddIn.SetFontColor(Sheet.Cells[ChangedCell.Row, ChangedCell.Column], Constants.Color.Black);
            string Contents = ChangedCell.Text;

            if (string.IsNullOrEmpty(Contents)) return;

            if (InstructionCell.Text == Constants.DP.InstructionDECST)
            {

                //SubstituteParam2ColumnForDECST(ChangedCell, flagpm1);
                DECSTParam2Changed(ChangedCell);
                return;
            }

            switch (operatorCell.Text)
            {

                case Constants.DP.SubstituteOperatorADD1:
                case Constants.DP.SubstituteOperatorMINUS1:
                    Excel.Range VariableCell = Sheet.Cells[ChangedCell.Row, Constants.DP.SubstituteVariableColumn];
                    if (Definitions.VariableDictionary.ContainsKey(VariableCell.Text))
                    {
                        ValidateNumericCell(ChangedCell, 1, Definitions.VariableDictionary[VariableCell.Text].CategoryCount, true, Constants.DP.SubstituteVariableColumn);
                    }
                    break;

                case Constants.DP.SubstituteOperatorCOUNT:
                    COUNTParam2toNChanged(ChangedCell);
                    break;

                case Constants.DP.SubstituteOperatorMCONVERT:
                    ChangedCell.Font.Color = System.Drawing.Color.Black;
                    string VariableName = DataProcess.Sheet.Cells[ChangedCell.Row, Constants.DP.SubstituteVariableColumn].Text;
                    if (Definitions.VariableDictionary.ContainsKey(VariableName))
                    {
                        ValidateNumericCell(ChangedCell, 1, Int32.MaxValue, true, Constants.DP.SubstituteVariableColumn);



                        QuestionSettings MconvertQst = Definitions.VariableDictionary[VariableName];
                        string formula = Constants.EqEqual + Constants.VariableList.ListItemSAMA;
                        for (int i = 0; i < MconvertQst.CategoryCount; i++)
                        {
                            Excel.Range MconvertRow = DataProcess.Sheet.Cells[ChangedCell.Row, Constants.DP.SubstituteParam3Column + i];
                            ExcelUtilForAddIn.AddValidation(MconvertRow, Excel.XlDVType.xlValidateList, Type.Missing, Type.Missing, formula, Type.Missing);
                            ExcelUtilForAddIn.SetCellInteriorColor(MconvertRow, Constants.Color.LightBlue);
                            string tooltipHeader = CommonResource.MCONVERT_ARG_TOOLTIP_HEADER;
                            string tooltipDesc = QC4Common.Common.CommonFunctions.FormatMsg(CommonResource.MCONVERT_ARG_TOOLTIP_DESC, Definitions.VariableDictionary[VariableName].Choices[i]);
                            SetToolTipforselectedcells(MconvertRow, tooltipHeader, tooltipDesc);
                            if (InDECSTRange(MconvertRow.Row)) MconvertRow.Validation.ShowError = false;
                        }

                    }
                    break;

                case Constants.DP.SubstituteOperatorRECODE:

                    RECODEParam2toNChanged(ChangedCell);
                    break;

                case Constants.DP.SubstituteOperatorINTEGRATE:
                    INTEGRATEParam2Changed(ChangedCell);
                    break;

                case Constants.DP.SubstituteOperatorJOINT:
                    JOINTParam2toNChanged(ChangedCell);
                    break;
                case Constants.DP.SubstituteOperatorMTOS:
                    MTOSParam2Changed(ChangedCell);
                    break;

            }
            //   SetNextCellSelection(ChangedCell);
        }

        private void MTOSParam2Changed(Excel.Range changedCell)
        {
            if (InDECSTRange(changedCell.Row)) return;

            if (string.IsNullOrEmpty(Convert.ToString(changedCell.Text)) || (!string.Equals(Convert.ToString(changedCell.Text), "1") && !string.Equals(Convert.ToString(changedCell.Text), "2")))
            {
                //MTOS_PM2_ERRORMESSAGE
                changedCell.Font.Color = Constants.Color.Red;
                changedCell.Select();
                MessageDialog.ErrorOk(CommonResource.MTOS_PM2_ERRORMESSAGE);



            }

        }

        private void SubstituteParamNChanged(Excel.Range ChangedCell)
        {
            Excel.Range OperatorCell = Sheet.Cells[ChangedCell.Row, Constants.DP.SubstituteOperatorColumn];
            Excel.Range param1 = Sheet.Cells[ChangedCell.Row, Constants.DP.SubstituteParam1Column];
            Excel.Range InstructionCell = Sheet.Cells[ChangedCell.Row, Constants.DP.InstructionColumn];
            string Contents = ChangedCell.Text;
            if (string.IsNullOrEmpty(Contents))
            {
                //----added to display decst variables properly------//
                if (InstructionCell.Text == Constants.DP.InstructionDECST)
                {
                    MessageDialog.ErrorOk(CommonResource.DECST_PARAM_ENTRY_RESTRICT);
                    int x = ChangedCell.Column - Constants.DP.SubstituteParam2Column;
                    string currentAlphabet = "[" + (char)(64 + x) + "]";
                    //Sheet.Application.EnableEvents = false;
                    ChangedCell.Value = currentAlphabet;
                    // Sheet.Application.EnableEvents = true;
                }
                //-----------------------//
                return;
            }
            switch (OperatorCell.Text)
            {
                case Constants.DP.SubstituteOperatorRECODE:
                    RECODEParam2toNChanged(ChangedCell);
                    break;

                case Constants.DP.SubstituteOperatorCOUNT:
                    COUNTParam2toNChanged(ChangedCell);
                    break;
                case Constants.DP.SubstituteOperatorCLASS:
                    if (ChangedCell.Column > Constants.DP.SubstituteParam3Column)
                    {
                        CLASSParam4toNChanged(ChangedCell);
                    }
                    break;

                case Constants.DP.SubstituteOperatorMCONVERT:
                    Excel.Range param2 = Sheet.Cells[ChangedCell.Row, Constants.DP.SubstituteParam2Column];
                    ChangedCell.Font.Color = Constants.Color.Black;
                    try
                    {
                        ValidateNumericCell(param2, 0, Int32.MaxValue, true, Constants.DP.SubstituteVariableColumn);

                        int IntegerContent = FindLargestofMconvertParam2(ChangedCell.Row);//Convert.ToInt32(!string.IsNullOrEmpty(param2check) ? param2check : param2Text);
                        if (Definitions.VariableDictionary[ChangedCell.Text].CategoryCount < IntegerContent)
                        {
                            MessageDialog.ErrorOk(CommonResource.MCONVERT_PARAMRANGE_VALIDATION);
                            ChangedCell.Font.Color = Constants.Color.Red;
                            break;
                        }
                        if (ChangedCell.Column > Constants.DP.SubstituteParam3Column)
                        {
                            Excel.Range param3 = Sheet.Cells[ChangedCell.Row, Constants.DP.SubstituteParam3Column];
                            if (Definitions.VariableDictionary.ContainsKey(ChangedCell.Text) && Definitions.VariableDictionary.ContainsKey(param3.Text))
                            {
                                if (Definitions.VariableDictionary[ChangedCell.Text].CategoryCount != Definitions.VariableDictionary[param3.Text].CategoryCount)
                                {
                                    //Select a variable with the number of choices equal to that of variable as an input bias[11]
                                    MessageDialog.ErrorOk(string.Format(CommonResource.MCONVERT_PARAMMISMATCH_VALIDATION, Definitions.VariableDictionary[param3.Text].CategoryCount));
                                    ChangedCell.Font.Color = Constants.Color.Red;
                                }
                            }


                        }
                    }
                    catch (Exception ex)
                    {
                        _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                    }
                    break;

                case Constants.DP.SubstituteOperatorINTEGRATE:
                    INTEGRATEParamNChanged(ChangedCell);
                    break;
                case Constants.DP.SubstituteOperatorJOINT:
                    JOINTParam2toNChanged(ChangedCell);
                    break;

            }
            if (InstructionCell.Text == Constants.DP.InstructionDECST)
            {
                Excel.Range param2 = Sheet.Cells[ChangedCell.Row, Constants.DP.SubstituteParam2Column];
                if (!string.IsNullOrEmpty(param2.Text))
                {
                    if (ChangedCell.Column >= Constants.DP.SubstituteParam3Column)
                    {
                        int value = 0;
                        if (int.TryParse(param2.Text, out value))
                        {
                            MessageDialog.ErrorOk(CommonResource.DECST_PARAM_ENTRY_RESTRICT);

                            if (ChangedCell.Column >= Constants.DP.SubstituteParam3Column + value)
                            {
                                //Sheet.Application.EnableEvents = false;
                                ChangedCell.Value = "";
                                //Sheet.Application.EnableEvents = true;
                            }
                            else
                            {
                                int x = ChangedCell.Column - Constants.DP.SubstituteParam2Column;
                                string currentAlphabet = "[" + (char)(64 + x) + "]";
                                //Sheet.Application.EnableEvents = false;
                                ChangedCell.Value = currentAlphabet;
                                //Sheet.Application.EnableEvents = true;
                            }
                        }
                    }
                }
                else
                {
                    MessageDialog.Error(CommonResource.DECST_PARAM_ENTRY_RESTRICT);
                    //Sheet.Application.EnableEvents = false;
                    ChangedCell.Value = "";
                    //Sheet.Application.EnableEvents = true;
                }
                //SubstituteParam2ColumnForDECST(ChangedCell, flagpm1);

                return;
            }
            //SetNextCellSelection(ChangedCell);
        }

        private void COUNTParam2toNChanged(Excel.Range changedCell)
        {
            Excel.Range param1 = Sheet.Cells[changedCell.Row, Constants.DP.SubstituteParam1Column];
            Excel.Range variablecell = Sheet.Cells[changedCell.Row, Constants.DP.SubstituteVariableColumn];
            if (!string.IsNullOrEmpty(param1.Text) && Definitions.VariableDictionary.ContainsKey(variablecell.Text))
            {
                if (changedCell.Column <= Definitions.VariableDictionary[variablecell.Text].CategoryCount + Constants.DP.SubstituteParam2Column)
                {
                    if (Definitions.VariableDictionary.ContainsKey(param1.Text))
                    {
                        ValidateNumericCell(changedCell, 0, Definitions.VariableDictionary[param1.Text].CategoryCount);
                    }
                }
            }

            //throw new NotImplementedException();
        }
        private void CLASSParam4toNChanged(Excel.Range changedCell)
        {
            if (!(changedCell.Text.Contains("/") || changedCell.Text.Contains(",")))
            {
                // ValidateNumericCell(changedCell, 0, Int32.MaxValue);
                string message = ValidateNumericCellConent(changedCell.Text, 0, double.MaxValue);
                if (!string.IsNullOrEmpty(message))
                {
                    if (message != "")
                    {
                        message = ValidateParenthesisNumericValues(changedCell.Text);
                    }
                }
            }
            else
            {
                MessageDialog.ErrorOk(CommonResource.VALIDATION_CLASS_MSG);
                changedCell.Font.Color = Constants.Color.Red;


            }
        }

        private void JOINTParam2toNChanged(Excel.Range changedCell)
        {
            if (changedCell.Column % 2 != 0) return;
            string ErrorMsg = "";
            bool showError = false;
            Excel.Range variablecell = Sheet.Cells[changedCell.Row, changedCell.Column - 1];
            if (string.IsNullOrEmpty(variablecell.Text))
            {
                MessageDialog.ErrorOk(CommonResource.ERR_MSG_FAILED_TO_GET_DATA_);//"Failed to get data"
                changedCell.Font.Color = Constants.Color.Red;
                return;
            }
            if (Definitions.VariableDictionary.ContainsKey(variablecell.Text))
            {
                showError = ValidateNumericCell(changedCell, 1, Definitions.VariableDictionary[variablecell.Text].CategoryCount);
                if (!showError) return;
            }

            ErrorMsg = JointParamCheck(changedCell.Row, out showError);
            if (showError && !InDECSTRange(changedCell.Row))
            {
                MessageDialog.ErrorOk(ErrorMsg);
                changedCell.Font.Color = Constants.Color.Red;
                return;
            }
        }
        private void INTEGRATEParamNChanged(Excel.Range changedCell)
        {
            Excel.Range param2 = Sheet.Cells[changedCell.Row, Constants.DP.SubstituteParam2Column];
            int param2value = Convert.ToInt32(param2.Text);
            int totalvariableparams = Constants.DP.SubstituteParam3Column + (param2value * 2) - 1;
            if (changedCell.Column < totalvariableparams) return;

            changedCell.Font.Color = Constants.Color.Black;


            string content = changedCell.Text;

            int count = content.Count(f => f == ';');
            if (count != (param2value - 1))
            {
                MessageDialog.ErrorOk(CommonResource.ERR_MSG_INTEGRATE_PARAM_VALUE_COMBINATIONS);

                changedCell.Font.Color = Constants.Color.Red;
                return;
            }
            string[] values = content.Split(';');
            for (int i = 0; i < values.Length; i++)
            {
                Excel.Range variableparam = Sheet.Cells[changedCell.Row, Constants.DP.SubstituteParam3Column + i * 2];
                if (string.IsNullOrEmpty(variableparam.Text))
                {
                    MessageDialog.ErrorOk(CommonResource.ERR_MSG_FAILED_TO_GET_DATA_);//"Failed to get data"
                    changedCell.Font.Color = Constants.Color.Red;
                    break;
                }

                if (!string.IsNullOrEmpty(values[i]))
                {
                    if (Definitions.VariableDictionary.ContainsKey(variableparam.Text))
                    {
                        //values[i]
                        if (values[i].StartsWith("<>") || values[i].StartsWith("!"))
                        {
                            values[i] = values[i].Replace("<>", "");
                            values[i] = values[i].Replace("!", "");
                        }
                        string message = ValidateNumericCellConent(values[i], Definitions.VariableDictionary[variableparam.Text].AnswerType == "N" ? 0 : 1, Definitions.VariableDictionary[variableparam.Text].AnswerType == "N" ? Int32.MaxValue : Definitions.VariableDictionary[variableparam.Text].CategoryCount);
                        //<<<<<<< Updated upstream

                        if (message != "" && Definitions.VariableDictionary[variableparam.Text].AnswerType == "N")
                        {
                            message = ValidateParenthesisNumericValues(values[i]);
                        }
                        //=======
                        //>>>>>>> Stashed changes
                        if (!string.IsNullOrEmpty(message))
                        {
                            int paramnumber = i + 1;
                            MessageDialog.ErrorOk(string.Format(CommonResource.ERR_MSG_THE_COMBINING_CRITERIA, paramnumber.ToString()) + message);//"The combining criteria [" + paramnumber.ToString() + "] is "
                            changedCell.Font.Color = Constants.Color.Red;
                            break;
                        }
                    }

                    //
                }
            }
        }

        private void RECODEParam2toNChanged(Excel.Range changedCell)
        {
            Excel.Range param1 = Sheet.Cells[changedCell.Row, Constants.DP.SubstituteParam1Column];
            string VariableName = param1.Text;
            if (string.IsNullOrEmpty(VariableName))
            {
                MessageDialog.ErrorOk(CommonResource.ERR_MSG_FAILED_TO_GET_DATA_);//"Failed to get data"
                changedCell.Font.Color = Constants.Color.Red;

            }
            else
            {
                if (!InDECSTRange(changedCell.Row))
                {
                    if (Definitions.VariableDictionary.ContainsKey(param1.Text))
                    {
                        ValidateNumericCell(changedCell, 1, Definitions.VariableDictionary[param1.Text].CategoryCount, true, Constants.DP.SubstituteParam1Column, true);
                    }
                    string paramvaluecontent = changedCell.Text;
                    int val = 0;
                    if (paramvaluecontent.Length == 1 && !paramvaluecontent.IsIntegerExpression(out val))//https://app.gluemodel.com/#/project/task/4295061520
                    {
                        MessageDialog.ErrorOk(CommonResource.ERR_MSG_SET_NUMERIC_VALUE);
                        changedCell.Font.Color = System.Drawing.Color.Red;
                    }

                    //Commenting for Redmine id : 191425  https://app.gluemodel.com/#/project/task/4295061708
                    //if (frmutil.CheckMultiplkeLimit_SAMA(paramvaluecontent))//https://app.gluemodel.com/#/project/task/4295061556
                    //{
                    //    MessageDialog.ErrorOk(CommonResource.CANNOT_SPECIFY_MULTIPLE_RANGE);
                    //    changedCell.Font.Color = System.Drawing.Color.Red;
                    //}


                }

            }
        }
        private void DECSTParam2Changed(Excel.Range changedCell)
        {
            ResetParamCells(changedCell.Row, Constants.DP.SubstituteParam3Column);
            int totalVariableParams = 0;
            string currentAlphabet = string.Empty;
            Excel.Range SubParam1Col = Sheet.Cells[changedCell.Row, Constants.DP.SubstituteParam1Column];
            int j = 0, param2Value = 0;
            SetDecstProgramList(true);
            if (decst_ProgramList.Count > 0)
            {
                SetDECSTMethodList();
                UpdateDECENDRow(false);
            }
            //if (flag == 1)
            //{
            //    if (!string.IsNullOrEmpty(changedCell.Text))
            //    {
            //        int newValue = changedCell.Row;
            //        string changedCellRow = newValue.ToString() + ",";
            //        changedCellRow = changedCellRow + j;
            //        string SubParam1ColumnText = newValue + "," + SubParam1Col.Text;
            //        string value = "0";
            //        if (decst_ProgramList.ContainsKey(SubParam1ColumnText))
            //        {
            //            decst_ProgramList.Remove(SubParam1ColumnText);
            //            decst_ProgramList.Add(SubParam1ColumnText, value);
            //        }
            //        else if (decst_ProgramList.ContainsKey(SubParam1ColumnText) == false)
            //        {
            //            decst_ProgramList.Add(SubParam1ColumnText, value);
            //        }
            //    }
            //}

            //else
            //{
            param2Value = Convert.ToInt32(changedCell.Text);
            totalVariableParams = (param2Value + Constants.DP.SubstituteParam3Column);
            try
            {
                //Sheet.Application.EnableEvents = false;
                if (!string.IsNullOrEmpty(changedCell.Text))
                {
                    for (int i = 1; i <= param2Value; i++)
                    {
                        Excel.Range variableparamcell = Sheet.Cells[changedCell.Row, Constants.DP.SubstituteParam2Column + i];
                        ExcelUtilForAddIn.AddValidation(variableparamcell, Excel.XlDVType.xlValidateTextLength, Excel.XlDVAlertStyle.xlValidAlertInformation, Excel.XlFormatConditionOperator.xlGreaterEqual, 0, Type.Missing);
                        ExcelUtilForAddIn.SetCellInteriorColor(variableparamcell, Constants.Color.LightGreen);
                        SetToolTipforselectedcells(variableparamcell, string.Format(CommonResource.DECST_PARAMN_TOOLTIPHEADER, i), string.Format(CommonResource.DECST_PARAMN_TOOLTIPDESC, i));
                        currentAlphabet = "[" + (char)(64 + i) + "]";
                        variableparamcell.Value += currentAlphabet;
                        //variableparamcell.Locked = true;
                        // Sheet.Protect(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                        j++;
                        // DECSTVariableList.Add(currentAlphabet);//IL_JP_MAM_007:4295046210
                    }
                    //int newValue = changedCell.Row;
                    //string changedCellRow = newValue.ToString() + ",";
                    //changedCellRow = changedCellRow + j;
                    //string SubParam1ColumnText = newValue + "," + SubParam1Col.Text;
                    if (decst_ProgramList.ContainsKey(SubParam1Col.Text))
                    {
                        // decst_ProgramList.Remove(SubParam1Col.Text);
                        DPCallMethod method = decst_ProgramList[SubParam1Col.Text];
                        method.paramcount = param2Value;
                        if (method.ParamList.Count > 0)
                        {
                            method.ParamList.Clear();
                        }
                    }
                    //else if (decst_ProgramList.ContainsKey(SubParam1ColumnText) == false)
                    //{
                    //    decst_ProgramList.Add(SubParam1ColumnText, changedCell.Text);
                    //}
                }
            }
            catch (ArgumentNullException arg)
            {
                _log.Error(arg.InnerException);// Console.WriteLine(arg.InnerException);
            }
            finally
            {
                //Sheet.Application.EnableEvents = true;
            }
            //}
        }
        /// <summary>
        /// Method to set properties of INTEGRATEParam2
        /// </summary>
        /// <param name="CurrentCell">Changed cell</param>
        private void INTEGRATEParam2Changed(Excel.Range changedCell)
        {
            ResetParamCells(changedCell.Row, Constants.DP.SubstituteParam3Column);
            int totalvariableparams = 0;
            string formula = Constants.EqEqual + Constants.VariableList.ListItemSAMAN;
            Excel.Range variablecell = Sheet.Cells[changedCell.Row, Constants.DP.SubstituteVariableColumn];
            try
            {
                //Sheet.Application.EnableEvents = false;
                int param2value = 0;
                if (!string.IsNullOrEmpty(changedCell.Text))
                {
                    param2value = Convert.ToInt32(changedCell.Text);
                    totalvariableparams = Constants.DP.SubstituteParam3Column + (param2value * 2) - 1;

                }
                for (int i = Constants.DP.SubstituteParam3Column; i <= totalvariableparams; i += 2)
                {
                    Excel.Range variableparamcell = Sheet.Cells[changedCell.Row, i];
                    Excel.Range criteriaparamcell = Sheet.Cells[changedCell.Row, i + 1];
                    ExcelUtilForAddIn.AddValidation(variableparamcell, Excel.XlDVType.xlValidateList, Type.Missing, Type.Missing, formula, Type.Missing);
                    ExcelUtilForAddIn.SetCellInteriorColor(variableparamcell, Constants.Color.LightBlue);
                    if (InDECSTRange(variableparamcell.Row)) variableparamcell.Validation.ShowError = false;
                    SetToolTipforselectedcells(variableparamcell, CommonResource.INTEGRATE_SRC_PARAMN_TOOLTIPHEADER, CommonResource.INTEGRATE_SRC_PARAMN_TOOLTIPDESC);

                    if (i + 1 < totalvariableparams)
                    {
                        ExcelUtilForAddIn.AddValidation(criteriaparamcell, Excel.XlDVType.xlValidateList, Type.Missing, Type.Missing, Constants.DP.ANDORList, Type.Missing);
                        ExcelUtilForAddIn.SetCellInteriorColor(criteriaparamcell, Constants.Color.LightGreen);
                        SetToolTipforselectedcells(criteriaparamcell, CommonResource.INTEGRATE_CRITERIA_PARAMN_TOOLTIPHEADER, CommonResource.INTEGRATE_CRITERIA_PARAMN_TOOLTIPDESC);
                    }

                }
                int integratecategorycount = 0;
                if (Definitions.VariableDictionary.ContainsKey(variablecell.Text))
                {
                    integratecategorycount = Definitions.VariableDictionary[variablecell.Text].CategoryCount;
                    integratecategorycount = integratecategorycount - 1;
                }
                else
                {
                    integratecategorycount = Constants.DP.MAX_DP_COLUMN - totalvariableparams;
                }

                string integrateparamcell = string.Empty;
                for (int j = 1; j < param2value; j++)
                {
                    integrateparamcell += ";";
                }
                int start = totalvariableparams;
                int stop = start + integratecategorycount;

                Excel.Range colstart = DataProcess.Sheet.Cells[changedCell.Row, start];
                Excel.Range colend = DataProcess.Sheet.Cells[changedCell.Row, stop];
                Excel.Range row = DataProcess.Sheet.Range[colstart, colend];
                row.Value = integrateparamcell;
                ExcelUtilForAddIn.SetCellInteriorColor(row, Constants.Color.Purple);
                ExcelUtilForAddIn.AddValidation(row, Excel.XlDVType.xlValidateTextLength, Excel.XlDVAlertStyle.xlValidAlertInformation, Excel.XlFormatConditionOperator.xlGreaterEqual, 0, Type.Missing);
                int paramindex = 0;
                foreach (Excel.Range cell in row)
                {
                    string tooltip = QC4Common.Common.CommonFunctions.FormatMsg(CommonResource.INTEGRATE_PARAMN_TOOLTIPDESC, Definitions.VariableDictionary[variablecell.Text].Choices[paramindex]);
                    SetToolTipforselectedcells(cell, CommonResource.INTEGRATE_PARAMN_TOOLTIPHEADER, tooltip);
                    paramindex++;
                }
                //for (int i = totalvariableparams; i < totalvariableparams + integratecategorycount; i++)
                //{
                //    Excel.Range integrateparamcell = Sheet.Cells[changedCell.Row, i];
                //    ExcelUtilForAddIn.SetCellInteriorColor(integrateparamcell, Constants.Color.Purple);
                //    for (int j = 1; j < param2value; j++)
                //    {
                //        integrateparamcell.Value += ";";
                //    }
                //    ExcelUtilForAddIn.AddValidation(integrateparamcell, Excel.XlDVType.xlValidateTextLength, Excel.XlDVAlertStyle.xlValidAlertInformation, Excel.XlFormatConditionOperator.xlGreaterEqual, 0, Type.Missing);
                //    SetToolTipforselectedcells(integrateparamcell, CommonResource.INTEGRATE_PARAMN_TOOLTIPHEADER, CommonResource.INTEGRATE_PARAMN_TOOLTIPDESC);
                //}
            }
            catch (Exception ex) { _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException); }
            finally
            {
                //Sheet.Application.EnableEvents = true;
            }

        }

        private void PopulateCriteriaOperators(Excel.Range Target)
        {
            try
            {
                if (string.IsNullOrEmpty(Target.Text))
                {
                    try
                    {
                        //Sheet.Application.EnableEvents = false;
                        foreach (Excel.Range target in Target)
                        {
                            // if (!Definitions.VariableDictionary.ContainsKey(Target.Text) && !Array.Exists(DECSTVariableList.ToArray(), element => element == Target.Text))////IL_JP_MAM_007:4295046210
                            ExcelUtilForAddIn.ResetCell(Sheet.Cells[target.Row, Constants.DP.CriteriaOperatorColumn]);
                        }
                        return;
                        //Sheet.Application.EnableEvents = true;
                    }
                    catch (Exception ex)
                    {
                        _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                        //Sheet.Application.EnableEvents = true;
                        return;
                    }


                }
                string sep = Target.Application.International[Excel.XlApplicationInternational.xlListSeparator];
                string addCellContent = " =" + sep + "<>";
                Excel.Range operatorCell = Sheet.Cells[Target.Row, Constants.DP.CriteriaOperatorColumn];
                operatorCell.Value = string.Empty;
                Common.ExcelUtilForAddIn.SetCellInteriorColor(operatorCell, Target.Interior.Color);

                if (QC4Common.Global.Global.SANvariables.Contains(Target.Text) || InDECSTRange(Target.Row))
                {
                   addCellContent = " =" + sep + "<>" + sep + "<" + sep + ">" + sep + "<=" + sep + ">=";

                }
                ExcelUtilForAddIn.AddValidation(operatorCell, Excel.XlDVType.xlValidateList, Type.Missing, Type.Missing, addCellContent, Type.Missing);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                //Sheet.Application.EnableEvents = true;
                return;
            }

        }
        private void PopulateValueCell(Excel.Range Target)
        {
            if (string.IsNullOrEmpty(Target.Text))
            {
                try
                {
                    //Sheet.Application.EnableEvents = false;
                    foreach (Excel.Range target in Target)
                    {
                        //  if (!Definitions.VariableDictionary.ContainsKey(Target.Text) && !Array.Exists(DECSTVariableList.ToArray(), element => element == Target.Text))////IL_JP_MAM_007:4295046210

                        ExcelUtilForAddIn.ResetCell(Sheet.Cells[target.Row, Constants.DP.CriteriavalueColumn]);
                    }
                    // Sheet.Application.EnableEvents = true;
                    return;
                }
                catch (Exception ex)
                {
                    _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                    // Sheet.Application.EnableEvents = true;
                    return;
                }
            }
            Excel.Range valueCell = Sheet.Cells[Target.Row, Constants.DP.CriteriavalueColumn];
            valueCell.Value = string.Empty;
            string strValueCombo = " ";
            int nComboValueCount = 0;
            string sep = Target.Application.International[Excel.XlApplicationInternational.xlListSeparator];
            if (Definitions.VariableDictionary.ContainsKey(Target.Text) || InDECSTRange(Target.Row))
            {
                if (Definitions.VariableDictionary.ContainsKey(Target.Text))
                {
                    nComboValueCount = Definitions.VariableDictionary[Target.Text].CategoryCount;


                    for (int nVal = 1; nVal <= nComboValueCount; nVal++)
                    {
                        strValueCombo += nVal.ToString() + sep;
                    }
                }
                if (Definitions.VariableDictionary.ContainsKey(Target.Text) && Definitions.VariableDictionary[Target.Text].AnswerType == Common.Constants.AnswerType.FA)//if FA no need *  191  added code if condition nly else was der
                { strValueCombo += "DK"; }
                else
                {
                    strValueCombo += "DK"+sep+"*";
                }
                if (strValueCombo.Length > 255)//[Redmine id: 189027]
                {
                    //=INDIRECT("Setting!$A$1:$A$500")  =設定!A1:A16
                    Excel.Worksheet settingssheet = ExcelUtilForAddIn.GetWorkSheetByCodeName(valueCell.Application.ActiveWorkbook, Constants.SheetCodeName.Setting);
                    Excel.Range settingsrange = settingssheet.Range[settingssheet.Cells[1, 1], settingssheet.Cells[nComboValueCount, 1]];
                    Excel.Range settingsDKRange = settingssheet.Range[settingssheet.Cells[1, 1], settingssheet.Cells[nComboValueCount, 1]];
                    strValueCombo = "= INDIRECT(" + "\"" + settingssheet.Name + "!$A$1:$A$" + nComboValueCount + "\")";//"=INDIRECT(\"設定!$A$5:$A$" + nComboValueCount + "\")";
                    // strValueCombo = "= INDIRECT(" + "\"" + settingssheet.Name + "!$A$1:$A$" + nComboValueCount + "\")";//"=INDIRECT(\"設定!$A$5:$A$" + nComboValueCount + "\")";
                }
                ExcelUtilForAddIn.AddValidation(valueCell, Excel.XlDVType.xlValidateList, Type.Missing, Excel.XlFormatConditionOperator.xlBetween, strValueCombo, Type.Missing);
                valueCell.Validation.ShowError = false;
                valueCell.Interior.Color = Target.Interior.Color;
                //valueCell.NumberFormat = "General";


            }

        }

        private void PopulateSubstituteOperator(Excel.Range Currentcell, Excel.Range TargetCell)
        {
            int FoundRow = Common.Util.IsVariableFoundInQSSheet(currworkbook,Currentcell.Text);

            if (Definitions.VariableDictionary.ContainsKey(Currentcell.Text))
            {
                string AnswerType = Definitions.VariableDictionary[Currentcell.Text].AnswerType;

                ExcelUtilForAddIn.AddValidation(Sheet.Cells[Currentcell.Row, Constants.DP.SubstituteOperatorColumn], Excel.XlDVType.xlValidateList, Type.Missing, Type.Missing, "=List_Dp_" + AnswerType, Type.Missing);
                ExcelUtilForAddIn.SetCellInteriorColor(Sheet.Cells[Currentcell.Row, Constants.DP.SubstituteOperatorColumn], Currentcell.Interior.Color);
            }
            else if (InDECSTRange(Currentcell.Row))
            {
                ExcelUtilForAddIn.AddValidation(Sheet.Cells[Currentcell.Row, Constants.DP.SubstituteOperatorColumn], Excel.XlDVType.xlValidateList, Type.Missing, Type.Missing, "=List_Dp_DEC", Type.Missing);
                ExcelUtilForAddIn.SetCellInteriorColor(Sheet.Cells[Currentcell.Row, Constants.DP.SubstituteOperatorColumn], Currentcell.Interior.Color);
            }


        }

       
        /// <summary>
        /// Sets value of the on/off cell
        /// </summary>
        private void SetOnOffvalueCell(Excel.Range ChangedCell, bool enable = true)
        {
            Excel.Range OnOffCell = Sheet.Cells[ChangedCell.Row, Constants.DP.OnOffColumn];
            OnOffCell.Value = enable == true ? CommonResource.CELL_ON : string.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ChangedCell"></param>
        private void SetCrossCellValue(Excel.Range ChangedCell, bool enable = true)
        {
            Excel.Range OnOffCell = Sheet.Cells[ChangedCell.Row, Constants.DP.CheckCrossColumn];

            if (enable)
            {
                if (string.IsNullOrEmpty(OnOffCell.Text))
                {
                    OnOffCell.Value = CommonResource.CELL_ON;
                }
            }
            else
            {
                OnOffCell.Value = string.Empty;
            }
        }

        /// <summary>
        /// Clear the List of Variables in the first column
        /// </summary>
        public void ClearVariableListColumn()
        {
            // Excel.Range column = Sheet.Columns[2];
            for (int i = 0; i < Definitions.VariableDictionary.Count; i++)
            {

                Sheet.Cells[i + Constants.DP.ProUIstartRow, 2].ClearContents();

            }
        }

        /// <summary>
        /// Set the validation and populate contents to the 'Variable' field of Dataprocess criteria
        /// </summary>
        public void SetCriteriaVariableColumn()
        {
            Excel.Range column = Sheet.Columns[5];
            column.Validation.Delete();

            ExcelUtilForAddIn.AddValidation(column, Excel.XlDVType.xlValidateList, Type.Missing, Type.Missing, "=List_Item_ALL", Type.Missing);
            Excel.Range ExcludeColumn = Sheet.Range["E1", "E4"];
            column.Validation.ShowError = false;
            ExcludeColumn.Validation.Delete();

        }
        private bool ValidateNtypeCriteria(Excel.Range ChangedCell, bool displayMessage = true, int choiceVariableRefColumn = 0, bool ignoreNotEqual = false)
        {
            //ValidateNtypeCondition
            string[] SplitContent;
            string Contents = ChangedCell.Text;
            double val = 0;
            if (Contents.Length == 1 && !Contents.IsDoubleExpression(out val))
            {
                if (displayMessage)
                {
                    MessageDialog.ErrorOk(CommonResource.ERR_MSG_SET_NUMERIC_VALUE);

                }

                return false;
            }

            if (Contents.Length > 1 && (Contents.StartsWith("-") || Contents.EndsWith("-")) && choiceVariableRefColumn > 0)//191 added for criteria start with or end with "-"
                Contents = MinMaxAppendWithMinus(Contents, ChangedCell.Row, choiceVariableRefColumn);
            ChangedCell.Font.Color = System.Drawing.Color.Black;
            if (ignoreNotEqual && (Contents.StartsWith("<>") || Contents.StartsWith("!")))
            {
                Contents = Contents.Replace("<>", "");
                Contents = Contents.Replace("!", "");
            }
            //IL_JP_MAM_007:4295056901                     
            //if (Contents.Contains("(") || Contents.Contains(")"))
            //{
            //    //^([(]{1}[-]{1}\d+\.?\d+[)]{1}$)
            //    Regex numeric = new Regex(@"^([(]{1}[-]{1}\d*\.?\d+[)]{1}$)");
            //    //Regex numeric = new Regex(@"^([!]?[(]?[-]?\d*\.?\d+[)]?[-]{1}[(]?[-]?\d*\.?\d+[)]?$)");//^([(]{1}[-]{1}\d*\.?\d+[)]{1}$)|([!]?[(]{1}[-]{1}\d*\.?\d+[)]{1}[-]{1}[(]{1}[-]{1}\d*\.?\d+[)]{1}$)       //^[(]{1}[-]{1}[0-9]*\.?[0-9]+[)]{1}$
            //    if (!numeric.Match(Contents).Success)
            //    {
            //        if (displayMessage)
            //        {
            //            MessageDialog.ErrorOk(CommonResource.SET_NUMERIC_VALUE);
            //            ChangedCell.Font.Color = System.Drawing.Color.Red;
            //            ChangedCell.Select();
            //        }
            //        return false;
            //    }
            //}

            if (Contents.Contains("/") || Contents.Contains(","))
            {
                char[] splitchar = { '/', ',' };
                SplitContent = Contents.Split(splitchar);
            }
            else
            {
                SplitContent = new string[] { Contents };
            }
            foreach (string item in SplitContent)
            {
                string value = item;
                string Div_Char = "@";
                string minval = "minVal";

                value = value.Replace("(-", Div_Char);
                // value = value.Replace(int.MinValue.ToString(), minval);
                value = value.Replace("(", "");
                value = value.Replace(")", "");
                value = value.Replace("-", ",");
                value = value.Replace(Div_Char, "-");
                //  value = value.Replace(minval, int.MinValue.ToString());
                string[] split2 = value.Split(',');
                foreach (string s2 in split2)
                {
                    if (string.IsNullOrEmpty(s2))
                        continue;
                    double output = 0;
                    bool err = false;
                    double parse = 0;
                    if (!double.TryParse(s2, out parse))
                    {
                        err = true;
                    }
                    //if (!s2.IsDoubleExpression(out output, false, true, true, false))
                    //{
                    //    err = true;
                    //}
                    else if (output > double.MaxValue || output < double.MinValue)
                    {
                        err = true;
                    }
                    if (err)
                    {
                        if (displayMessage)
                        {
                            MessageDialog.ErrorOk(CommonResource.ERR_MSG_SET_NUMERIC_VALUE);

                        }

                        return false;
                    }
                }

            }/*Contents = Contents.Replace(Contents, Div_Char, "-");
            ReplacedContent = Split(Contents, ",")
            If UBound(Data_Array2) > 1 Then*/
            return true;

        }
        private bool ValidateNumericCell(Excel.Range ChangedCell, int minvalue, int maxvalue, bool displayMessage = true, int choiceVariableRefColumn = 0, bool ignoreNotEqual = false)
        {
            string Contents = ChangedCell.Text;
            if (Contents.Length > 1 && (Contents.StartsWith("-") || Contents.EndsWith("-")) && choiceVariableRefColumn > 0)//191 added for criteria start with or end with "-"
                Contents = MinMaxAppendWithMinus(Contents, ChangedCell.Row, choiceVariableRefColumn);
            ChangedCell.Font.Color = System.Drawing.Color.Black;
            if (ignoreNotEqual && (Contents.StartsWith("<>") || Contents.StartsWith("!")))
            {
                Contents = Contents.Replace("<>", "");
                Contents = Contents.Replace("!", "");
            }
            string message = ValidateNumericCellConent(Contents, minvalue, maxvalue);
            if (!string.IsNullOrEmpty(message))
            {
                if (displayMessage)
                {
                    MessageDialog.ErrorOk(message);
                }
                ChangedCell.Font.Color = System.Drawing.Color.Red;
                return false;
            }
            return true;
        }

        private string ValidateNumericCellConent(string cellcontents, double minvalue, double maxvalue)
        {
            string[] SplitContent;
            if (cellcontents.Contains("/") || cellcontents.Contains(","))
            {
                char[] splitchar = { '/', ',' };
                SplitContent = cellcontents.Split(splitchar);
            }
            else
            {
                SplitContent = new string[] { cellcontents };
            }

            foreach (string item in SplitContent)
            {
                try
                {
                    string message = ValidateRange(item, minvalue, maxvalue);
                    if (!string.IsNullOrEmpty(message))
                        return message;

                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("StackTrace:{0}", ex.StackTrace);
                    return CommonResource.ERR_MSG_SET_NUMERIC_VALUE;// "Set a numeric value";
                }
            }
            return string.Empty;

        }
        private string ValidateRange(string Contents, double minvalue, double maxvalue)
        {
            string[] SplitContent;
            if (Contents.StartsWith("-"))
            {
                Contents = minvalue.ToString("r") + Contents;
            }
            if (Contents.EndsWith("-"))
            {
                Contents = Contents + maxvalue.ToString("r");
            }
            SplitContent = Contents.Split('-');
            string[] splitExclusion = SplitContent[0].Split('!');
            try
            {
                if (splitExclusion[1] != "")
                {
                    SplitContent[0] = splitExclusion[1];
                }
            }
            catch { }

            foreach (string item in SplitContent)
            {
                try
                {


                    double value = 0;
                    if (double.TryParse(item, out value))
                    {
                        if (value < minvalue || value > maxvalue)
                        {
                            // return CommonResource.ERR_MSG_SET_AN_INTEGER_IN_THE_RANGE_OF + " [ " + minvalue.ToString() + "-" + maxvalue.ToString() + " ] ";//Set an integer in the range of
                            return string.Format(CommonResource.ERR_MSG_SET_AN_INTEGER_IN_THE_RANGE, minvalue.ToString() + "-" + maxvalue.ToString());
                        }
                    }
                    else
                    {
                        return CommonResource.ERR_MSG_SET_NUMERIC_VALUE;
                    }

                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("StackTrace:{0}", ex.StackTrace);
                    return CommonResource.ERR_MSG_SET_NUMERIC_VALUE;// "Set a numeric value";
                }
            }
            return string.Empty;
        }
        private void ValidateMaxValue(Excel.Range contents, int max)
        {
            try
            {

                int value = Convert.ToInt32(contents.Text);
                if (value > max)
                {
                    System.Windows.Forms.MessageBox.Show(CommonResource.ERR_MSG_SET_AN_INTEGER_IN_THE_RANGE_OF + " [1 - " + max.ToString() + "]", "QuickCross");//Set an integer in the range of
                    contents.Font.Color = System.Drawing.Color.Red;
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("StackTrace:{0}", ex.StackTrace);
                MessageDialog.ErrorOk(CommonResource.ERR_MSG_SET_NUMERIC_VALUE);//"Set a numeric value"
                contents.Font.Color = System.Drawing.Color.Red;

            }
        }

        private String ValidateParenthesisNumericValues(String Contents)
        {
            if (!string.IsNullOrEmpty(Contents))
            {
                Contents = frmutil.TrimStartEqualNotequal(Contents);
            }
            if (Contents.Contains("("))
            {

                Regex paranthesisRegEx = new Regex(@"(\(-\d+\.\d+\)-\d+\.\d+)|(\(-\d+\)-\(-\d+\.\d+\))|(\(-\d+\.\d+\)-\(-\d+\))|(\(-\d+\)-\d+\.\d+)|(\(-\d+\.\d+\)-\d+)|(\(-\d+\)-\d+)|(\(-\d+\.\d+\))|(\(-\d+\))");
                if (Contents.Contains("/") || Contents.Contains(","))
                {
                    char[] splitchar = { '/', ',' };
                    string[] splitcontents = Contents.Split(splitchar);
                    foreach (string str in splitcontents)
                    {
                        if (!paranthesisRegEx.Match(str).Success)
                        {
                            return CommonResource.ERR_MSG_SET_NUMERIC_VALUE;// "Set a numeric value";
                            break;
                        }
                    }

                }
                else
                {
                    if (!paranthesisRegEx.Match(Contents).Success)
                    {
                        Console.WriteLine(CommonResource.ERR_MSG_SET_NUMERIC_VALUE);//"Set a numeric value"

                    }
                }

                //string[] splitParenthesis = Contents.Split('-');
                //if (splitParenthesis[0] == "(" && splitParenthesis.Length < 4)
                //{
                //    string[] splitParenthesisValue = splitParenthesis[1].Split(')');
                //    try
                //    {
                //        int value = Convert.ToInt32(splitParenthesisValue[0]);
                //    }
                //    catch (Exception ex)
                //    {
                //        return "Set a numeric value";
                //    }
                //    return String.Empty;
                //}
                //return "Set a numeric value";
            }
            else
            {
                return ValidateNumericCellConent(Contents, 0, double.MaxValue);
            }
            return String.Empty;

        }
        private void ExcludeListParamChanged(Excel.Range ChangedCell)
        {

        }

        /*
         */
        // Method to list out program names corresponding characters 
        private void CALLParam1Changed(Excel.Range ChangedCell)
        {
            if (!string.IsNullOrEmpty(Sheet.Cells[ChangedCell.Row, Constants.DP.SubstituteParam2Column].Text))
            {
                Excel.Range range = ExcelUtilForAddIn.EndxlRight(Sheet.Cells[ChangedCell.Row, Constants.DP.SubstituteParam2Column]);
                ExcelUtilForAddIn.ResetCell(range);
            }

            if (!string.IsNullOrEmpty(ChangedCell.Text))
            {
                if (decst_ProgramList.ContainsKey(ChangedCell.Text))
                {
                    DPCallMethod decstmethod = decst_ProgramList[ChangedCell.Text];
                    if (decstmethod.paramcount > 0)
                    {
                        for (int i = 0; i < decstmethod.paramcount; i++)
                        {
                            Excel.Range paramcell = Sheet.Cells[ChangedCell.Row, Constants.DP.SubstituteParam2Column + i];
                            paramcell.Value = "[" + (char)(65 + i) + "]";
                            ExcelUtilForAddIn.AddValidation(paramcell, Excel.XlDVType.xlValidateTextLength, Excel.XlDVAlertStyle.xlValidAlertInformation, Excel.XlFormatConditionOperator.xlGreaterEqual, 0, Type.Missing);
                            ExcelUtilForAddIn.SetCellInteriorColor(paramcell, Constants.Color.LightGreen);
                            SetToolTipforselectedcells(paramcell, string.Format(CommonResource.CALL_PARAMN_TOOLTIP_HEADER, i + 1), string.Format(CommonResource.CALL_PARAMN_TOOLTIP_DESC, i + 1));
                        }
                    }

                }

            }

        }
        // IF THE USER SELECTS LDEL IN COLUMN 8 AND TYPE A DIGIT IN COLUMN 12 THEN BELOW FUNCTION VALIDATE THE TYPED VALUE
        private void LDELValidation(Excel.Range ChangedCell)
        {

            try
            {

                LDel ldel = new LDel(ExcelUtilForAddIn.GetWorkSheetByCodeName(currworkbook,Constants.SheetCodeName.LDEL));//
                ldel.OnLDELCellChanged(null);

                if (Definitions.optionList.Contains(ChangedCell.Text) || InDECSTRange(ChangedCell.Row))
                {
                    ExcelUtilForAddIn.SetFontColor(ChangedCell, Constants.Color.Black);
                    ExcelUtilForAddIn.SetSelectedCell(Sheet.Cells[ChangedCell.Row + 1, Constants.DP.CriteriavalueColumn]);
                }
                else
                {
                    MessageDialog.ErrorOk(string.Format(CommonResource.LDEL_VALIDATION_ERROR1, ChangedCell.Text));
                    ExcelUtilForAddIn.SetFontColor(ChangedCell, Constants.Color.Red);
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("StackTrace:{0}", ex.StackTrace);
                MessageDialog.ErrorOk(CommonResource.LDEL_VALIDATION_ERROR2);
                ChangedCell.Font.Color = System.Drawing.Color.Red;

            }
        }

        private void SubstituteInstructionDELETESelected(Excel.Range CurrentCell)
        {
            try
            {
                Excel.Range operatorcell = Sheet.Cells[CurrentCell.Row, Constants.DP.CriteriaOperatorColumn];
                Excel.Range valuecell = Sheet.Cells[CurrentCell.Row, Constants.DP.CriteriavalueColumn];
                ResetParamCells(CurrentCell.Row, Constants.DP.SubstituteParam1Column);
                // if the user is going to select DELETE operator FROM 8th column BUSINESS LOGIC will comes here
                {
                    ExcelUtilForAddIn.SetCellInteriorColor(operatorcell, Constants.Color.Cream);                                          //param2.Select();
                    ExcelUtilForAddIn.SetCellInteriorColor(valuecell, Constants.Color.Cream);
                }
            }
            catch (ArgumentNullException arg)
            {
                _log.Error(arg.InnerException);
                Console.WriteLine(arg.InnerException);
            }
        }
        private void HandleInstructionColumnChange(Excel.Range changedCell)
        {
            SetOnOffvalueCell(changedCell);
            ExcelUtilForAddIn.ResetCell(Sheet.Cells[changedCell.Row, Constants.DP.SubstituteVariableColumn]);
            if (string.IsNullOrEmpty(changedCell.Text))
            {
                ExcelUtilForAddIn.ResetCell(Sheet.Cells[changedCell.Row, Constants.DP.OnOffColumn]);
                ExcelUtilForAddIn.ResetCell(Sheet.Cells[changedCell.Row, Constants.DP.CheckCrossColumn]);
                //Sheet.Application.EnableEvents = false;
                Sheet.Cells[changedCell.Row, Constants.DP.CriteriaVariableColumn].ClearContents();
                //Sheet.Application.EnableEvents = true;
                ExcelUtilForAddIn.ResetCell(Sheet.Cells[changedCell.Row, Constants.DP.CriteriaOperatorColumn]);
                ExcelUtilForAddIn.ResetCell(Sheet.Cells[changedCell.Row, Constants.DP.CriteriavalueColumn]);


            }

            ResetSubstituteOperatorCell(changedCell.Row);
            SetCrossCellValue(changedCell, false);

            Excel.Range criteriaVariableColumn = Sheet.Cells[changedCell.Row, Constants.DP.CriteriaVariableColumn];
            ExcelUtilForAddIn.SetCellInteriorColor(criteriaVariableColumn, changedCell.Interior.Color);
            switch (changedCell.Text)
            {
                case Constants.DP.InstructionAND:
                case Constants.DP.InstructionOR:
                    SubstituteInstructionANDORSelected(changedCell);
                    break;


                case Constants.DP.InstructionTHEN:
                    SubstituteInstructionTHENSelected(changedCell);

                    break;

                // If the user selects instruction OMIT from instruction column- 7'th column 
                case Constants.DP.InstructionOMIT:
                    SubstituteInstructionOMITSelected(changedCell);
                    break;
                // If the user selects instruction OMIT2 from instruction column- 7'th column 
                case Constants.DP.InstructionOMIT2:
                    SubstituteInstructionOMIT2Selected(changedCell);
                    break;

                // If the user selects instruction LDEL from instruction column- 7'th column 
                case Constants.DP.InstructionLDEL:
                    SubstituteInstructionLDELSelected(changedCell);
                    break;

                // If the user selects instruction DELETE from instruction column- 7'th column 
                case Constants.DP.InstructionDELETE:
                    SubstituteInstructionDELETESelected(changedCell);
                    break;

                // If the user selects instruction DECST from instruction column- 7'th column 
                case Constants.DP.InstructionDECST:
                    SubstituteInstructionDECSTSelected(changedCell);
                    break;

                // If the user selects instruction DECEND from instruction column- 7'th column 
                case Constants.DP.InstructionDECEND:
                    SubstituteInstructionDECENDSelected(changedCell);
                    break;

                // If the user selects instruction CALL from instruction column- 7'th column 
                case Constants.DP.InstructionCALL:
                    SubstituteInstructionCALLSelected(changedCell);
                    break;

                // If the user selects instruction FOR from instruction column- 8'th column 
                case Constants.DP.InstructionFOR:
                    InstructionFORSelected(changedCell);
                    break;

                // If the user selects instruction FOR from instruction column- 8'th column 
                case Constants.DP.InstructionNEXT:
                    InstructionNEXTSelected(changedCell);
                    break;

                case Constants.DP.InstructionLISTUP:

                    SubstituteInstructionLISTUPSelected(changedCell);
                    break;

            }

        }
        private void SubstituteInstructionTHENSelected(Excel.Range changedCell)
        {
            // ResetSubstituteOperatorCell(changedCell.Row);//191  commntd for speedup
            Excel.Range VariableColumn = Sheet.Cells[changedCell.Row, Constants.DP.SubstituteVariableColumn];
            string formula = Constants.EqEqual + Constants.VariableList.ListItemALL;
            ExcelUtilForAddIn.AddValidation(VariableColumn, Excel.XlDVType.xlValidateList, Type.Missing, Type.Missing, formula, Type.Missing);
            ExcelUtilForAddIn.SetCellInteriorColor(VariableColumn, changedCell.Interior.Color);

            ExcelUtilForAddIn.SetSelectedCell(VariableColumn);
            VariableColumn.Validation.ShowError = false;
        }
        private void SubstituteInstructionANDORSelected(Excel.Range changedCell)
        {
            Excel.Range criteriaoperator = Sheet.Cells[changedCell.Row, Constants.DP.CriteriaOperatorColumn];
            Excel.Range criteriavalue = Sheet.Cells[changedCell.Row, Constants.DP.CriteriavalueColumn];
            Excel.Range criteriaoperatorNextrow = Sheet.Cells[changedCell.Row + 1, Constants.DP.CriteriaOperatorColumn];
            Excel.Range criteriavalueNextRow = Sheet.Cells[changedCell.Row + 1, Constants.DP.CriteriavalueColumn];
            Excel.Range criteriavariableNextrow = Sheet.Cells[changedCell.Row + 1, Constants.DP.CriteriaVariableColumn];
            ExcelUtilForAddIn.SetCellInteriorColor(criteriaoperator, Constants.Color.Cream);
            ExcelUtilForAddIn.SetCellInteriorColor(criteriavalue, Constants.Color.Cream);
            ExcelUtilForAddIn.SetCellInteriorColor(criteriaoperatorNextrow, Constants.Color.Cream);
            ExcelUtilForAddIn.SetCellInteriorColor(criteriavalueNextRow, Constants.Color.Cream);
            ExcelUtilForAddIn.SetSelectedCell(criteriavariableNextrow);
            //Excel.Range paramrange = Sheet.Range[param1column, paramncolumn];
        }
        private void SubstituteInstructionLISTUPSelected(Excel.Range changedCell)
        {
            Excel.Range param1column = Sheet.Cells[changedCell.Row, Constants.DP.SubstituteParam1Column];
            Excel.Range paramncolumn = Sheet.Cells[changedCell.Row, 11 + 80];
            Excel.Range paramrange = Sheet.Range[param1column, paramncolumn];
            Excel.Range CriteriaVariableCell = Sheet.Cells[changedCell.Row, Constants.DP.CriteriaVariableColumn];
            ExcelUtilForAddIn.SetCellInteriorColor(paramrange, Constants.Color.LightGrey);
            //CriteriaVariableCell.Clear();
            //CriteriaVariableCell.Select();           
            string formula = Constants.EqEqual + Constants.VariableList.ListItemALLD;

            ExcelUtilForAddIn.AddValidation(paramrange, Excel.XlDVType.xlValidateList, Excel.XlDVAlertStyle.xlValidAlertStop, Type.Missing, formula, Type.Missing, Type.Missing);
            SetToolTipforselectedcells(paramrange, CommonResource.LISTUP_TOOLTIP_HEADER, CommonResource.LISTUP_TOOLTIP_DESC);
            if (InDECSTRange(changedCell.Row))
            {
                paramrange.Validation.ShowError = false;
            }
            ExcelUtilForAddIn.SetSelectedCell(param1column);

            //throw new NotImplementedException();
        }
        private void InstructionFORSelected(Excel.Range currentcell)
        {
            Excel.Range param1column = Sheet.Cells[currentcell.Row, Constants.DP.SubstituteParam1Column];
            Excel.Range CriteriaVariableCell = Sheet.Cells[currentcell.Row, Constants.DP.CriteriaVariableColumn];
            ExcelUtilForAddIn.SetCellInteriorColor(CriteriaVariableCell, Excel.XlColorIndex.xlColorIndexNone);
            //CriteriaVariableCell.Clear();
            //CriteriaVariableCell.Select();           
            ExcelUtilForAddIn.AddValidation(param1column, Excel.XlDVType.xlValidateTextLength, Excel.XlDVAlertStyle.xlValidAlertStop, Excel.XlFormatConditionOperator.xlGreaterEqual, 0, Type.Missing);
            SetToolTipforselectedcells(param1column, CommonResource.VAL_PM1_ERROR_TITLE, CommonResource.VAL_PM1_ERROR_MESSAGE);
            ExcelUtilForAddIn.SetCellInteriorColor(param1column, Constants.Color.LightBlue);

            Excel.Range param2column = Sheet.Cells[currentcell.Row, Constants.DP.SubstituteParam2Column];
            ExcelUtilForAddIn.AddValidation(param2column, Excel.XlDVType.xlValidateTextLength, Excel.XlDVAlertStyle.xlValidAlertStop, Excel.XlFormatConditionOperator.xlGreaterEqual, 0, Type.Missing);
            SetToolTipforselectedcells(param2column, CommonResource.VAL_PM2_ERROR_TITLE, CommonResource.VAL_PM2_ERROR_MESSAGE);
            ExcelUtilForAddIn.SetCellInteriorColor(param2column, Constants.Color.LightBlue);

            Excel.Range param3column = Sheet.Cells[currentcell.Row, Constants.DP.SubstituteParam3Column];
            ExcelUtilForAddIn.AddValidation(param3column, Excel.XlDVType.xlValidateTextLength, Excel.XlDVAlertStyle.xlValidAlertStop, Excel.XlFormatConditionOperator.xlGreaterEqual, 0, Type.Missing);
            SetToolTipforselectedcells(param3column, CommonResource.VAL_PM3_ERROR_TITLE, CommonResource.VAL_PM3_ERROR_MESSAGE);
            ExcelUtilForAddIn.SetCellInteriorColor(param3column, Constants.Color.LightBlue);
            param1column.Select();

        }
        private void InstructionNEXTSelected(Excel.Range currentcell)
        {
            Excel.Range Nextcolumn = Sheet.Cells[currentcell.Row + 1, Constants.DP.CriteriaVariableColumn];
            Excel.Range CriteriaVariableCell = Sheet.Cells[currentcell.Row, Constants.DP.CriteriaVariableColumn];

            ExcelUtilForAddIn.SetCellInteriorColor(CriteriaVariableCell, Excel.XlColorIndex.xlColorIndexNone);
            // CriteriaVariableCell.Clear();
            Nextcolumn.Select();
        }
        private void FORNEXTSelected(Excel.Range currentcell)
        {
            Excel.Range Nextcolumn = Sheet.Cells[currentcell.Row + 1, Constants.DP.InstructionColumn];
            Nextcolumn.Select();
        }
        public void CriteriaVariableColumnChanged(Excel.Range ChangedCell)
        {
            bool inDECSTRange = false;
            ChangedCell.Font.Color = Constants.Color.Black;
            if (InDECSTRange(ChangedCell.Row))
            {
                ChangedCell.Validation.ShowError = false;//IL_JP_MAM_007:4295046210
                inDECSTRange = true;
            }


            PopulateCriteriaOperators(ChangedCell);
            PopulateValueCell(ChangedCell);
            Excel.Range instructuioncell = Sheet.Cells[ChangedCell.Row, Constants.DP.InstructionColumn];
            bool EnableOnOffcontent = true;
            if (string.IsNullOrEmpty(ChangedCell.Text))
            {
                if (string.IsNullOrEmpty(instructuioncell.Text))
                {
                    EnableOnOffcontent = false;
                }
            }
            else
            {
                if (inDECSTRange)
                {
                    Match match = Regex.Match(ChangedCell.Text, @"[\[A-Za-z\]]");
                    if (!match.Success)
                    {
                        // return;
                        MessageDialog.ErrorOk(CommonResource.ERR_MSG_CONTENT_INVALID);
                        ChangedCell.Font.Color = Constants.Color.Red;
                        ExcelUtilForAddIn.SetSelectedCell(ChangedCell);
                    }
                }
                else if (!Definitions.VariableDictionary.ContainsKey(ChangedCell.Text))
                {
                    MessageDialog.ErrorOk(CommonResource.ERR_MSG_CONTENT_INVALID);
                    ChangedCell.Font.Color = Constants.Color.Red;
                    ExcelUtilForAddIn.SetSelectedCell(ChangedCell);


                }
                else
                {
                    ExcelUtilForAddIn.SetSelectedCell(Sheet.Cells[ChangedCell.Row, Constants.DP.CriteriaOperatorColumn]);
                }
            }
            SetOnOffvalueCell(ChangedCell, EnableOnOffcontent);

        }
        public static string MinMaxAppendWithMinus(string value, int row, int column, Excel.Worksheet ProcessSheet = null)//191 added for criteria start with or end with "-"
        {
            //Excel.Range crange = Sheet.Cells[row, column];
            Excel.Range crange;
            if (ProcessSheet != null)
            {
                crange = ProcessSheet.Cells[row, column];
            }
            else
            {
                crange = Sheet.Cells[row, column];
            }
            if (Definitions.VariableDictionary.ContainsKey(crange.Text))
            {
                bool isnot = false;
                if (value.StartsWith("!"))//170984 fix for class
                {
                    isnot = true;
                    value = value.TrimStart('!');
                }
                string maxDoubleValue = "179769313486231570000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
                string doubleMinValue = "-179769313486231570000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
                double catcount = Definitions.VariableDictionary[crange.Text].CategoryCount == 0 ? double.MaxValue : Definitions.VariableDictionary[crange.Text].CategoryCount;//170984 fix for class // int catcount = Definitions.VariableDictionary[crange.Text].CategoryCount;
                if (value.StartsWith("-"))
                { value = (Definitions.VariableDictionary[crange.Text].CategoryCount == 0 ? doubleMinValue : "1") + value; }//Redmine id:177538 //Redmine id: 176455
                if (value.EndsWith("-"))
                { value = value + (Definitions.VariableDictionary[crange.Text].CategoryCount == 0 ? maxDoubleValue : Definitions.VariableDictionary[crange.Text].CategoryCount); }//Redmine id : 197237//(maxDoubleValue)
                if (isnot) value = "!" + value;
            }
            return value;
        }
        private bool ValidateCriteriaValueAsVariable(QuestionSettings qsValue, QuestionSettings qsVariable)
        {
            if (((qsValue.AnswerType == Constants.AnswerType.SA || qsValue.AnswerType == Constants.AnswerType.N || qsValue.AnswerType == Constants.AnswerType.FA) &&
                        (qsVariable.AnswerType == Constants.AnswerType.SA || qsVariable.AnswerType == Constants.AnswerType.N || qsVariable.AnswerType == Constants.AnswerType.FA)) ||
                        (qsValue.AnswerType == Constants.AnswerType.MA) && (qsVariable.AnswerType == Constants.AnswerType.MA))
            {
                return true;
            }
            return false;
        }
        private void CriteriaValuecellchanged(Excel.Range changedCell)
        {

            if (!string.IsNullOrEmpty(changedCell.Text))
            {
                //string content = ;


                Excel.Range criteriaVariable = Sheet.Cells[changedCell.Row, Constants.DP.CriteriaVariableColumn];
                changedCell.Font.Color = System.Drawing.Color.Black;
                if (InDECSTRange(changedCell.Row))
                {
                    Match match = Regex.Match(changedCell.Text, @"[\[A-Za-z\]]");
                    if (match.Success)
                    {
                        return;
                    }
                }

                if (Definitions.VariableDictionary.ContainsKey(changedCell.Text) && (Definitions.VariableDictionary.ContainsKey(criteriaVariable.Text)))
                {
                    QuestionSettings qsValue = Definitions.VariableDictionary[changedCell.Text];
                    QuestionSettings qsVariable = Definitions.VariableDictionary[criteriaVariable.Text];

                    if (ValidateCriteriaValueAsVariable(qsValue, qsVariable))
                    {
                        return;
                    }


                }

                ExcelUtilForAddIn.SetSelectedCell(Sheet.Cells[changedCell.Row, Constants.DP.InstructionColumn]);
                Excel.Range crange = Sheet.Cells[changedCell.Row, Constants.DP.CriteriaVariableColumn];
                Excel.Range operatorCell = Sheet.Cells[changedCell.Row, Constants.DP.CriteriaOperatorColumn];
                string operatorCellContent = operatorCell.Value;
                if (operatorCellContent != "=" && operatorCellContent != "<>")
                {
                    int cellvalue = 0;
                    //if (!int.TryParse(Convert.ToString(changedCell.Value), out cellvalue))//IL_JP_MAM_007:4295055994  //--if (!int.TryParse( changedCell.Value, out cellvalue))
                    string regex = @"(^\d+\.\d+$)|(^\d+$)|(^[(]-{1}\d+[)]$)|(^[(]-{1}\d+\.\d+[)]$)";
                    Regex numeric = new Regex(regex);
                    if (!numeric.Match(changedCell.Value.ToString()).Success)
                    {
                        MessageDialog.ErrorOk(CommonResource.SET_NUMERIC_VALUE);
                        changedCell.Font.Color = System.Drawing.Color.Red;
                        changedCell.Select();
                        return;
                    }
                }

                string questype = Definitions.VariableDictionary[crange.Text].AnswerType;//
                if ((changedCell.Text != "DK" && changedCell.Text != "*" && questype != Constants.AnswerType.FA))
                {


                    Excel.Range criterialvariablecell = Sheet.Cells[changedCell.Row, Constants.DP.CriteriaVariableColumn];
                    if (Definitions.VariableDictionary.ContainsKey(criterialvariablecell.Text) && !InDECSTRange(changedCell.Row))
                    {
                        int min = 1;
                        int max = Definitions.VariableDictionary[criterialvariablecell.Text].CategoryCount;
                        if (questype == Constants.AnswerType.N)
                        {

                            min = int.MinValue;
                            max = int.MaxValue;
                            ValidateNtypeCriteria(changedCell, true, Constants.DP.CriteriaVariableColumn, true);
                            // string regex = @"(^\d +\.\d +$)| (^\d +$)| (^[(] -{ 1}\d +[)]$)| (^[(] -{ 1}\d +\.\d +[)]$)";
                        }
                        else
                        {
                            ValidateNumericCell(changedCell, min, max, true, Constants.DP.CriteriaVariableColumn, true);//changed by 191  for Validation error message is displayed while entering a value for the 'N' type for critieria
                        }
                    }

                }
            }

        }
        public void SubstituteVariableColumnChanged(Excel.Range ChangedCell)
        {
            Excel.Range operatorCol = Sheet.Cells[ChangedCell.Row, Constants.DP.SubstituteOperatorColumn];
            if (!string.IsNullOrEmpty(operatorCol.Text))
            {
                ResetSubstituteOperatorCell(ChangedCell.Row);

            }
            if (!string.IsNullOrEmpty(ChangedCell.Text))
            {
                ExcelUtilForAddIn.SetSelectedCell(operatorCol);
            }
            PopulateSubstituteOperator(ChangedCell, operatorCol);
            ExcelUtilForAddIn.SetCellInteriorColor(operatorCol, ChangedCell.Interior.Color);
        }

        public void DataProcess_Cell_Change_Listener(Excel.Range ChangedRange)
        {

            try
            {
                if (ChangedRange.Cells.Count > 0)
                {
                    bool eventstate = ChangedRange.Application.EnableEvents;
                    bool screenUpdateState = ChangedRange.Application.ScreenUpdating;
                    ChangedRange.Application.EnableEvents = false;
                    ChangedRange.Application.ScreenUpdating = false;
                    //  int[][] fieldInfoArray = { new int[] { 1, 2 }, new int[] { 1, 2 } };
                    Array fieldInfoArray = new int[] { 1, 2 };

                    try
                    {
                        int c = 0;
                        foreach (Excel.Range changedCell in ChangedRange.Cells)
                        {
                            c++;
                            if (c > 1050)//228048-Limit cell validation
                                break;
                            if (!string.IsNullOrEmpty(changedCell.Text))//code block RedmineIssue :178232 Fixed -on 30 January 2020 
                            {
                                changedCell.TextToColumns(FieldInfo: (object)fieldInfoArray, TextQualifier: Excel.XlTextQualifier.xlTextQualifierNone, TrailingMinusNumbers: true);//changed for https://app.gluemodel.com/#/project/task/4295060422
                            }
                           
                        }

                    }
                    catch (Exception ex)
                    { }
                    ChangedRange.Application.ScreenUpdating = screenUpdateState;
                    ChangedRange.Application.EnableEvents = eventstate;

                }
                if (ChangedRange.EntireRow.Address == ChangedRange.Address)
                {
                    return;
                }
                if (ChangedRange.Columns.Count > 1)
                {
                    return; //pasting operation. No validation required.
                }

                ChangedRange.Application.EnableEvents = false;
                int d = 0;
                foreach (Excel.Range ChangedCell in ChangedRange)
                {
                    d++;
                    if (d > 1050)//228048-Limit cell validation
                        break;
                    if (ChangedCell.Row < 5 || ChangedCell.Row > Qc4CommonConstants.ExcelRowColumnMax.ExcelCommonMaxRowLimit)
                    {
                        return;
                    }
                    //if (string.IsNullOrEmpty(ChangedCell.Text))//191  added
                    //{
                    //    return;
                    //}
                    //if (ChangedCell.Text.Length > Constants.DP.SHRINKABLE_LENGTH)
                    //{
                    //    ChangedCell.ShrinkToFit = true;
                    //}
                    //Sheet.Application.EnableEvents = false;
                    ChangedCell.Font.Color = System.Drawing.Color.Black;
                    //Sheet.Application.EnableEvents = true;
                    if (InDECSTRange(ChangedCell.Row))
                    {
                        ChangedCell.Validation.ShowError = false;
                    }
                    switch (ChangedCell.Column)
                    {
                        case Constants.DP.CriteriaVariableColumn:
                            CriteriaVariableColumnChanged(ChangedCell);
                            break;
                        case Constants.DP.CriteriaOperatorColumn:
                            if (!string.IsNullOrEmpty(ChangedCell.Text))
                            {
                                ExcelUtilForAddIn.SetSelectedCell(Sheet.Cells[ChangedCell.Row, Constants.DP.CriteriavalueColumn]);
                            }
                            break;
                        case Constants.DP.CriteriavalueColumn:
                            CriteriaValuecellchanged(ChangedCell);
                            break;

                        case Constants.DP.InstructionColumn:
                            HandleInstructionColumnChange(ChangedCell);
                            break;

                        case Constants.DP.SubstituteVariableColumn:
                            //191 code added for resetting param columns 
                            Excel.Range instrccell = DataProcess.Sheet.Cells[ChangedCell.Row, Constants.DP.InstructionColumn];
                            if (!string.IsNullOrEmpty(instrccell.Text) && !string.Equals(instrccell.Text, Constants.DP.InstructionTHEN))
                            {
                                Excel.Range SubstituteVariableColumn = DataProcess.Sheet.Cells[ChangedCell.Row, Constants.DP.SubstituteVariableColumn];
                                //SubstituteVariableColumn.Clear();                        
                                if (!string.IsNullOrEmpty(SubstituteVariableColumn.Text))
                                {
                                    ShowAlert(string.Format(CommonResource.ERR_MSG_DP_INVALID_ENTRY, ChangedCell.Row), "", "");
                                    ExcelUtilForAddIn.SetFontColor(ChangedCell, Constants.Color.Red);

                                }
                                if (!Definitions.VariableDictionary.ContainsKey(SubstituteVariableColumn.Text))
                                { ShowAlert(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW + " " + ChangedCell.Row.ToString(), CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_VARIABLE, ""); }
                                return;
                            }
                            else if (!Definitions.VariableDictionary.ContainsKey(DataProcess.Sheet.Cells[ChangedCell.Row, Constants.DP.SubstituteVariableColumn].Text) && !InDECSTRange(ChangedCell.Row) && !string.IsNullOrEmpty(ChangedCell.Text))
                            { ShowAlert(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW + " " + ChangedCell.Row.ToString(), CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_VARIABLE, ""); }
                            SubstituteVariableColumnChanged(ChangedCell);
                            break;

                        case Constants.DP.SubstituteOperatorColumn:
                            //191 code added for resetting param columns 
                            Excel.Range instrccelll = DataProcess.Sheet.Cells[ChangedCell.Row, Constants.DP.InstructionColumn];
                            if (!string.IsNullOrEmpty(instrccelll.Text) && !string.Equals(instrccelll.Text, Constants.DP.InstructionTHEN))
                            {
                                Excel.Range SubstituteOperatorColumn = DataProcess.Sheet.Cells[ChangedCell.Row, Constants.DP.SubstituteOperatorColumn];
                                //SubstituteOperatorColumn.Clear();
                                if (!string.IsNullOrEmpty(SubstituteOperatorColumn.Text))
                                {
                                    ShowAlert(string.Format(CommonResource.ERR_MSG_DP_INVALID_ENTRY, ChangedCell.Row), "", "");
                                    ExcelUtilForAddIn.SetFontColor(ChangedCell, Constants.Color.Red);
                                }
                                return;
                            }
                            SubstituteOperatorChanged(ChangedCell);
                            break;

                        case Constants.DP.SubstituteParam1Column:
                            SubstituteParam1Changed(ChangedCell);
                            ParameterNavigationCheck(ChangedCell);
                            break;

                        case Constants.DP.SubstituteParam2Column:
                            SubstituteParam2Changed(ChangedCell);
                            ParameterNavigationCheck(ChangedCell);
                            break;

                        //case Constants.DP.SubstituteParam3Column:
                        //    FORNEXTSelected(ChangedCell);
                        //    break;

                        default:
                            if (ChangedCell.Column > Constants.DP.SubstituteParam2Column)
                            {
                                SubstituteParamNChanged(ChangedCell);
                                ParameterNavigationCheck(ChangedCell);
                            }
                            break;
                    }
                }
            }
            finally
            {
                try
                {
                    ChangedRange.Application.EnableEvents = true;
                    sylk = false;
                    // Clipboard.SetDataObject(dataObj);
                }
                catch { }
            }

        }
        private void ParameterNavigationCheck(Excel.Range ChangedCell)
        {
            Excel.Range nextRow = Sheet.Cells[ChangedCell.Row, ChangedCell.Column + 1];
            var nextRowColor = nextRow.Interior.ColorIndex;
            if (nextRowColor == -4142)
            {
                Excel.Range criteriavariableNextrow = Sheet.Cells[ChangedCell.Row + 1, Constants.DP.CriteriaVariableColumn];
                ExcelUtilForAddIn.SetSelectedCell(criteriavariableNextrow);
            }
            else
            {
                SetNextCellSelection(ChangedCell);
            }
        }
        private bool ValidateCellValuesBforBtnClick(Dictionary<int, Crit_Inst_Operator> operatorsDict)//Dictionary<int, string> operatorsDict
        {
            bool allow = true;
            if (operatorsDict.Count == 0)
            {
                ShowAlert(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_EMPTY, "", "");
                return false;
            }
            Excel.Range rangecriteriavariable;
            Excel.Range rangecriteriaoperator;
            Excel.Range rangecriteriavalue;
            Excel.Range rangeinstr;

            foreach (KeyValuePair<int, Crit_Inst_Operator> kvp in operatorsDict)
            {

                //if criteria is der ,den chek that then instruction
                try
                {
                    if (string.IsNullOrEmpty(kvp.Value.instruction ))//check instruction empty ,then criteria filled or not
                    {
                        if (CheckCriteriaByRow(kvp) == false) return false;
                    }

                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("StackTrace:{0}", ex.StackTrace);

                    ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_DIRECTION, "*", kvp.Key, Constants.DP.CriteriaVariableColumn);

                    return false;
                }
                //191 code added for resetting param columns 
                Excel.Range instrccelll = DataProcess.Sheet.Cells[kvp.Key, Constants.DP.InstructionColumn];
                Excel.Range SubstituteVariableColumn = DataProcess.Sheet.Cells[kvp.Key, Constants.DP.SubstituteVariableColumn];
                Excel.Range SubstituteOperatorColumn = DataProcess.Sheet.Cells[kvp.Key, Constants.DP.SubstituteOperatorColumn];
                if (!string.IsNullOrEmpty(instrccelll.Text) && !string.Equals(instrccelll.Text, Constants.DP.InstructionTHEN) && (!string.IsNullOrEmpty(SubstituteVariableColumn.Text) || !string.IsNullOrEmpty(SubstituteOperatorColumn.Text)))
                {

                    //SubstituteVariableColumn.Clear();
                    //SubstituteOperatorColumn.Clear();
                    ShowAlert(string.Format(CommonResource.ERR_MSG_DP_INVALID_ENTRY, kvp.Key), "", "");

                    return false;
                }
                switch (kvp.Value.instruction)//Constants.DP.InstructionTHEN checking only InstructionTHEN
                {
                    case Constants.DP.InstructionAND:
                    case Constants.DP.InstructionOR:
                        if (CheckCriteriaByRow(kvp) == false) return false;
                        int nextinstructionposition = Array.IndexOf(operatorsDict.Keys.ToArray(), kvp.Key);

                        if ((nextinstructionposition == operatorsDict.Count() - 1) || (nextinstructionposition + 1 <= operatorsDict.Count() && string.IsNullOrEmpty(operatorsDict.Values.ElementAt(nextinstructionposition + 1).criteriavariable)))
                        {
                            int rownumber = (nextinstructionposition == operatorsDict.Count() - 1) ? kvp.Key : operatorsDict.Keys.ElementAt(nextinstructionposition + 1);

                            ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, rownumber.ToString()) + " ", CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_CRITERIA_VARIABLE_NOT_SET, "", kvp.Key, Constants.DP.CriteriaVariableColumn);

                            return false;
                        }
                        //need to check next active row is filled with criteria or not else show error message//191 
                        break;

                    case Constants.DP.InstructionTHEN:
                        rangeinstr = Sheet.Cells[kvp.Key, Constants.DP.InstructionColumn] == null ? "" : Sheet.Cells[kvp.Key, Constants.DP.InstructionColumn];
                        rangecriteriavariable = Sheet.Cells[kvp.Key, Constants.DP.CriteriaVariableColumn] == null ? "" : Sheet.Cells[kvp.Key, Constants.DP.CriteriaVariableColumn];
                        rangecriteriaoperator = Sheet.Cells[kvp.Key, Constants.DP.CriteriaOperatorColumn] == null ? "" : Sheet.Cells[kvp.Key, Constants.DP.CriteriaOperatorColumn];
                        rangecriteriavalue = Sheet.Cells[kvp.Key, Constants.DP.CriteriavalueColumn] == null ? "" : Sheet.Cells[kvp.Key, Constants.DP.CriteriavalueColumn];
                        // rangeinstr = Sheet.Cells[kvp.Key, Constants.DP.InstructionColumn] == null ? "" : Sheet.Cells[kvp.Key, Constants.DP.InstructionColumn];
                        if (////# 197600
                            (string.IsNullOrEmpty(rangecriteriavariable.Text) && string.IsNullOrEmpty(rangecriteriaoperator.Text) && string.IsNullOrEmpty(rangecriteriavalue.Text))
                           ||
                           !string.IsNullOrEmpty(rangecriteriavariable.Text) && !string.IsNullOrEmpty(rangecriteriaoperator.Text ) && !string.IsNullOrEmpty(rangecriteriavalue.Text )
                            )
                        {
                            if (!string.IsNullOrEmpty(rangecriteriavariable.Text ) && !string.IsNullOrEmpty(rangecriteriaoperator.Text ) && !string.IsNullOrEmpty(rangecriteriavalue.Text))
                            {
                                // //trial criteria value as variable [Redmine id : 178707] -
                                if (CheckCriteriaByRow(kvp) == false) return false;
                            }
                            if (!Definitions.VariableDictionary.ContainsKey(rangecriteriavalue.Text) && !string.IsNullOrEmpty(rangecriteriavariable.Text) && CheckCriteriaNSAValues(kvp, rangecriteriavariable, rangecriteriaoperator, rangecriteriavalue, rangeinstr) == false) return false;
                            if (CheckInstructionByRow(kvp) == false) return false;// //trial criteria value as variable [Redmine id : 178707] - return false;
                        }
                        else
                        {
                            if (CheckCriteriaByRow(kvp) == false) return false;
                        }
                        break;

                    // If the user selects instruction OMIT from instruction column- 7'th column 
                    case Constants.DP.InstructionOMIT:

                        rangecriteriavariable = Sheet.Cells[kvp.Key, Constants.DP.CriteriaVariableColumn] == null ? "" : Sheet.Cells[kvp.Key, Constants.DP.CriteriaVariableColumn];
                        rangecriteriaoperator = Sheet.Cells[kvp.Key, Constants.DP.CriteriaOperatorColumn] == null ? "" : Sheet.Cells[kvp.Key, Constants.DP.CriteriaOperatorColumn];
                        rangecriteriavalue = Sheet.Cells[kvp.Key, Constants.DP.CriteriavalueColumn] == null ? "" : Sheet.Cells[kvp.Key, Constants.DP.CriteriavalueColumn];
                        rangeinstr = Sheet.Cells[kvp.Key, Constants.DP.InstructionColumn] == null ? "" : Sheet.Cells[kvp.Key, Constants.DP.InstructionColumn];
                        if (////# 197600
                            (string.IsNullOrEmpty(rangecriteriavariable.Text) && string.IsNullOrEmpty(rangecriteriaoperator.Text ) && string.IsNullOrEmpty(rangecriteriavalue.Text ))
                           ||
                           !string.IsNullOrEmpty(rangecriteriavariable.Text ) && !string.IsNullOrEmpty(rangecriteriaoperator.Text) && !string.IsNullOrEmpty(rangecriteriavalue.Text )
                            )
                        {
                            //4295056909
                            if (!string.IsNullOrEmpty(rangecriteriavariable.Text ) && !string.IsNullOrEmpty(rangecriteriaoperator.Text) && !string.IsNullOrEmpty(rangecriteriavalue.Text))
                            {
                                if (CheckCriteriaByRow(kvp) == false) return false;
                            }
                            //check  variables selected or not
                            if (!Definitions.VariableDictionary.ContainsKey(rangecriteriavalue.Text) && !string.IsNullOrEmpty(rangecriteriavariable.Text) && CheckCriteriaNSAValues(kvp, rangecriteriavariable, rangecriteriaoperator, rangecriteriavalue, rangeinstr) == false) return false;
                            if (string.IsNullOrEmpty(Sheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column].Text))
                            {
                                ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", string.Format(CommonResource.LDEL_VALIDATION_ERROR1, Sheet.Cells[kvp.Key, Constants.DP.InstructionColumn].Text), "", kvp.Key, Constants.DP.InstructionColumn);
                                // ShowAlert(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW + " " + kvp.Key.ToString(), CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_OMIT_PARAM1, "");// string.Format(CommonResource.LDEL_VALIDATION_ERROR1, Sheet.Cells[kvp.Key, Constants.DP.InstructionColumn].Text)
                                return false;
                            }
                        }
                        else
                        {
                            if (CheckCriteriaByRow(kvp) == false) return false;
                        }
                        if (CheckInstructionByRow(kvp) == false) return false;
                        break;
                    // If the user selects instruction OMIT2 from instruction column- 7'th column 
                    case Constants.DP.InstructionOMIT2:

                        rangecriteriavariable = Sheet.Cells[kvp.Key, Constants.DP.CriteriaVariableColumn] == null ? "" : Sheet.Cells[kvp.Key, Constants.DP.CriteriaVariableColumn];
                        rangecriteriaoperator = Sheet.Cells[kvp.Key, Constants.DP.CriteriaOperatorColumn] == null ? "" : Sheet.Cells[kvp.Key, Constants.DP.CriteriaOperatorColumn];
                        rangecriteriavalue = Sheet.Cells[kvp.Key, Constants.DP.CriteriavalueColumn] == null ? "" : Sheet.Cells[kvp.Key, Constants.DP.CriteriavalueColumn];
                        rangeinstr = Sheet.Cells[kvp.Key, Constants.DP.InstructionColumn] == null ? "" : Sheet.Cells[kvp.Key, Constants.DP.InstructionColumn];
                        if (
                            (string.IsNullOrEmpty(rangecriteriavariable.Text ) && string.IsNullOrEmpty(rangecriteriaoperator.Text ) && string.IsNullOrEmpty(rangecriteriavalue.Text ))
                           ||
                           !string.IsNullOrEmpty(rangecriteriavariable.Text ) && !string.IsNullOrEmpty(rangecriteriaoperator.Text ) && !string.IsNullOrEmpty(rangecriteriavalue.Text )
                            )
                        {
                            //4295056909
                            if (!string.IsNullOrEmpty(rangecriteriavariable.Text ) &&!string.IsNullOrEmpty(rangecriteriaoperator.Text ) && !string.IsNullOrEmpty(rangecriteriavalue.Text ))
                            {
                                if (CheckCriteriaByRow(kvp) == false) return false;
                            }
                            if (!Definitions.VariableDictionary.ContainsKey(rangecriteriavalue.Text) && !string.IsNullOrEmpty(rangecriteriavariable.Text) && CheckCriteriaNSAValues(kvp, rangecriteriavariable, rangecriteriaoperator, rangecriteriavalue, rangeinstr) == false) return false;
                            //check first and end variables selected or not
                            if ((string.IsNullOrEmpty(Sheet.Cells[kvp.Key, Constants.DP.SubstituteParam2Column].Text)) &&
                                (string.IsNullOrEmpty(Sheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column].Text))
                                )
                            {
                                ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", string.Format(CommonResource.LDEL_VALIDATION_ERROR1, Sheet.Cells[kvp.Key, Constants.DP.InstructionColumn].Text), "", kvp.Key, Constants.DP.InstructionColumn);
                                return false;
                            }
                            if (string.IsNullOrEmpty(Sheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column].Text) )
                            {
                                ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_OMIT_PARAM1, "", kvp.Key, Constants.DP.SubstituteParam1Column);
                                return false;
                            }
                            if (string.IsNullOrEmpty(Sheet.Cells[kvp.Key, Constants.DP.SubstituteParam2Column].Text) )
                            {
                                ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", string.Format(CommonResource.LDEL_VALIDATION_ERROR1, Sheet.Cells[kvp.Key, Constants.DP.InstructionColumn].Text), "", kvp.Key, Constants.DP.SubstituteParam2Column);
                                return false;
                            }
                        }
                        else
                        {
                            if (CheckCriteriaByRow(kvp) == false) return false;
                        }
                        if (CheckInstructionByRow(kvp) == false) return false;
                        break;

                    // If the user selects instruction LDEL from instruction column- 7'th column 
                    case Constants.DP.InstructionLDEL:
                        try
                        {
                            LDel ldel = new LDel(ExcelUtilForAddIn.GetWorkSheetByCodeName(currworkbook,Constants.SheetCodeName.LDEL));
                            ldel.OnLDELCellChanged(null);
                        }
                        catch { }
                        rangecriteriavariable = Sheet.Cells[kvp.Key, Constants.DP.CriteriaVariableColumn];
                        rangecriteriaoperator = Sheet.Cells[kvp.Key, Constants.DP.CriteriaOperatorColumn];
                        rangecriteriavalue = Sheet.Cells[kvp.Key, Constants.DP.CriteriavalueColumn];
                        rangeinstr = Sheet.Cells[kvp.Key, Constants.DP.InstructionColumn];
                        if (
                             (string.IsNullOrEmpty(rangecriteriavariable.Text) && string.IsNullOrEmpty(rangecriteriaoperator.Text) && string.IsNullOrEmpty(rangecriteriavalue.Text)) ||

                            (!string.IsNullOrEmpty(rangecriteriavariable.Text) && !string.IsNullOrEmpty(rangecriteriaoperator.Text) && !string.IsNullOrEmpty(rangecriteriavalue.Text)))
                        {
                            if (!Definitions.VariableDictionary.ContainsKey(rangecriteriavalue.Text) && !string.IsNullOrEmpty(rangecriteriavariable.Text) && CheckCriteriaNSAValues(kvp, rangecriteriavariable, rangecriteriaoperator, rangecriteriavalue, rangeinstr) == false) return false;
                            int count = Definitions.optionList.Where(x => x.Equals(Sheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column].Text)).Count(); //Definitions.optionList.GroupBy(n => n!=null && n!="").Any(c => c.Count()>1);
                            if (((!Definitions.optionList.Contains(Sheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column].Text)) || count > 1) && !string.IsNullOrEmpty(Sheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column].Text) && Definitions.optionList.Count > 0)
                            {
                                ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", string.Format(CommonResource.LDEL_VALIDATION_ERROR1, Sheet.Cells[kvp.Key, Constants.DP.InstructionColumn].Text), "", kvp.Key, Constants.DP.SubstituteParam1Column);//SubstituteParam1Column
                                return false;
                            }

                        }
                        else
                        {
                            if (CheckCriteriaByRow(kvp) == false) return false;
                        }
                        if (CheckInstructionByRow(kvp) == false) return false;
                        break;

                    // If the user selects instruction DELETE from instruction column- 7'th column 
                    case Constants.DP.InstructionDELETE:

                        if (CheckCriteriaByRow(kvp) == false) return false;

                        //new inplementation needed for checking db bcm empty or not

                        //commnted bcs of https://app.gluemodel.com/#/project/task/4295056920
                        //rangecriteriavariable = Sheet.Cells[kvp.Key, Constants.DP.CriteriaVariableColumn] == null ? "" : Sheet.Cells[kvp.Key, Constants.DP.CriteriaVariableColumn];
                        //rangecriteriavalue = Sheet.Cells[kvp.Key, Constants.DP.CriteriavalueColumn] == null ? "" : Sheet.Cells[kvp.Key, Constants.DP.CriteriavalueColumn];
                        //rangeinstr = Sheet.Cells[kvp.Key, Constants.DP.InstructionColumn] == null ? "" : Sheet.Cells[kvp.Key, Constants.DP.InstructionColumn];

                        //if (! string.IsNullOrEmpty())//check if entire variable value range in the criteria section selected
                        /* {
                             string value = rangecriteriavalue.Text;
                             if (value.Contains('-'))
                             {
                                 int start = 0;
                                 int limit = 0;
                                 string[] criteriavalues = value.Split('-');
                                 if (criteriavalues.Length == 1)
                                 {
                                     try
                                     {
                                         start = Convert.ToInt32(criteriavalues[0]);
                                     }
                                     catch (Exception e) { start = 1; System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace); }
                                     limit = start;
                                 }
                                 else
                                 {
                                     try
                                     {
                                         start = Convert.ToInt32(criteriavalues[0]);
                                     }
                                     catch (Exception e) { start = 1; System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace); }//actually get min value of answer
                                     try
                                     {
                                         limit = Convert.ToInt32(criteriavalues[1]);
                                     }
                                     catch (Exception e)
                                     {//actually get max value of answer;need to get max of choice no from item id and set limit
                                         limit = Convert.ToInt32(Definitions.VariableDictionary[rangecriteriavariable.Text].CategoryCount);
                                         //Convert.ToInt32(Definitions.VariableDictionary[rangecrivariable.Text].CategoryCount)
                                         System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                                     }
                                 }
                                 if (start == 1 && limit == Convert.ToInt32(Definitions.VariableDictionary[rangecriteriavariable.Text].CategoryCount))
                                 {
                                     //ERR_MSG_CANNOT_EXECUTE_BECAUSE_NO_DATA
                                     ShowAlert(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW + " " + kvp.Key.ToString(), CommonResource.ERR_MSG_CANNOT_EXECUTE_BECAUSE_NO_DATA, "");

                                     return false;
                                 }
                             }
                         }*/


                        break;

                    // If the user selects instruction DECST from instruction column- 7'th column 
                    case Constants.DP.InstructionDECST:
                        if (CheckInstructionByRow(kvp) == false) return false;

                        break;

                    // If the user selects instruction DECEND from instruction column- 7'th column 
                    case Constants.DP.InstructionDECEND:

                        break;

                    // If the user selects instruction CALL from instruction column- 7'th column 
                    case Constants.DP.InstructionCALL:
                        if (CheckInstructionByRow(kvp) == false) return false;
                        bool screenUpdatingStatus = DataProcess.Sheet.Application.ScreenUpdating;
                        try
                        {

                            DataProcess.Sheet.Application.ScreenUpdating = false;
                            string methodname = DataProcess.Sheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column].Text;
                            DataProcessExecute dpExecObject = new Logic.DataProcessExecute(this);
                            if (DataProcess.decst_ProgramList.ContainsKey(methodname))
                            {
                                DPCallMethod method = DataProcess.decst_ProgramList[methodname];
                                int startrow = method.Rowstart;
                                int endrow = method.RowEnd;
                                Excel.Range callmethodstart = DataProcess.Sheet.Cells[startrow + 1, Constants.DP.OnOffColumn];
                                Excel.Range callmethodend = DataProcess.Sheet.Cells[endrow, Constants.DP.OnOffColumn];
                                Dictionary<int, DataProcess.Crit_Inst_Operator> CALL_criteria_inst_operator_Dict = dpExecObject.GetinstructionsByRow(callmethodstart, callmethodend, "", false);
                                foreach (KeyValuePair<int, DataProcess.Crit_Inst_Operator> keyval in CALL_criteria_inst_operator_Dict/*dpsheet.crit_inst_operator_Dict*/)//(KeyValuePair<int, string> kvp in dpsheet.operatorsDict)
                                {
                                    Excel.Range rowstart = DataProcess.Sheet.Cells[keyval.Key, 1];
                                    Excel.Range rowend = ExcelUtilForAddIn.EndxlRight(DataProcess.Sheet.Cells[keyval.Key, 1]);
                                    Excel.Range row = DataProcess.Sheet.Range[rowstart, rowend];

                                    object[,] initvalue = row.Value;
                                    for (int ii = 0; ii < method.paramcount; ii++)
                                    {
                                        string paramchar = "[" + (char)(65 + ii) + "]";
                                        foreach (Excel.Range cell in row)
                                        {
                                            //string cellText = cell.Text.ToLower();
                                            string paramCharLower = paramchar.ToLower();
                                            if (cell.Text.Contains(paramCharLower) || cell.Text.Contains(paramchar))
                                            {

                                                DataProcess.Sheet.Application.EnableEvents = false;
                                                cell.Value = cell.Text.Replace(paramCharLower, DataProcess.Sheet.Cells[kvp.Key, Constants.DP.SubstituteParam2Column + ii].Text);//method.ParamList[ii];
                                                cell.Value = cell.Text.Replace(paramchar, DataProcess.Sheet.Cells[kvp.Key, Constants.DP.SubstituteParam2Column + ii].Text);
                                                if (cell.Column == Constants.DP.CriteriaVariableColumn)
                                                {
                                                    keyval.Value.criteriavariable = cell.Value.ToString();
                                                }
                                                else if (cell.Column == Constants.DP.SubstituteOperatorColumn)
                                                {
                                                    keyval.Value.substituteoperator = cell.Value.ToString();
                                                }
                                                DataProcess.Sheet.Application.EnableEvents = true;

                                            }
                                        }
                                    }
                                    try
                                    {
                                        if (!string.IsNullOrEmpty(DataProcess.Sheet.Cells[keyval.Key, Constants.DP.CriteriaVariableColumn].Text) || !string.IsNullOrEmpty(DataProcess.Sheet.Cells[keyval.Key, Constants.DP.CriteriaOperatorColumn].Text) || !string.IsNullOrEmpty(DataProcess.Sheet.Cells[keyval.Key, Constants.DP.CriteriavalueColumn].Text))
                                        {
                                            if (CheckCriteriaByRow(keyval) == false)
                                            {
                                                return false;
                                            }
                                        }

                                        if (CheckInstructionByRow(keyval) == false)
                                        {
                                            return false;
                                        }
                                    }
                                    finally
                                    {
                                        row.Value = initvalue;
                                    }

                                }
                            }
                        }
                        finally
                        {
                            DataProcess.Sheet.Application.ScreenUpdating = screenUpdatingStatus;
                        }


                        break;

                    // If the user selects instruction FOR from instruction column- 8'th column 
                    case Constants.DP.InstructionFOR:
                        if (CheckInstructionByRow(kvp) == false) return false;
                        string ErrMsg = ValidateFORStatement(kvp.Key);
                        if (!string.IsNullOrEmpty(ErrMsg))
                        {
                            ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", ErrMsg, "", kvp.Key, Constants.DP.InstructionColumn);
                            return false;
                        }
                        FORValidationSuccess = true;
                        //------------validate rows inside for-next-----------//
                        try
                        {
                            DataProcess.Sheet.Application.EnableEvents = false;
                            Logic.DataProcessExecute dpExecObject = new Logic.DataProcessExecute(this);
                            FORendrow = dpExecObject.FindFORNEXTEndRow(kvp.Key);
                            if (FORendrow > kvp.Key + 1)
                            {
                                DataProcess.Sheet.Application.ScreenUpdating = false;
                                Excel.Range forinstStart = DataProcess.Sheet.Cells[kvp.Key + 1, Constants.DP.OnOffColumn];
                                Excel.Range forinstEnd = DataProcess.Sheet.Cells[FORendrow, Constants.DP.OnOffColumn];
                                Dictionary<int, DataProcess.Crit_Inst_Operator> FOR_criteria_inst_operator_Dict = dpExecObject.GetinstructionsByRow(forinstStart, forinstEnd, "", false);
                                Excel.Range forparam1cell = DataProcess.Sheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column];
                                Excel.Range forparam2cell = DataProcess.Sheet.Cells[kvp.Key, Constants.DP.SubstituteParam2Column];
                                Excel.Range forparam3cell = DataProcess.Sheet.Cells[kvp.Key, Constants.DP.SubstituteParam3Column];
                                int startvalue = -1;
                                int endvalue = 0;
                                int counter = 1;
                                int.TryParse(forparam1cell.Text, out startvalue);
                                int.TryParse(forparam2cell.Text, out endvalue);
                                int.TryParse(forparam3cell.Text, out counter);
                                string paramchar = @"[￥]";
                                string paramchar1byte = @"[¥]";
                                string paramchareng = @"[\]";

                                for (int x = startvalue; startvalue > endvalue ? x >= endvalue : x <= endvalue; x += counter)// for (int x = startvalue; x <= endvalue; x += counter)
                                {
                                    foreach (KeyValuePair<int, DataProcess.Crit_Inst_Operator> keyval in FOR_criteria_inst_operator_Dict/*dpsheet.crit_inst_operator_Dict*/)//(KeyValuePair<int, string> kvp in dpsheet.operatorsDict)
                                    {
                                        Excel.Range rowstart = DataProcess.Sheet.Cells[keyval.Key, 1];
                                        Excel.Range rowend = ExcelUtilForAddIn.EndxlRight(DataProcess.Sheet.Cells[keyval.Key, 1]);
                                        Excel.Range row = DataProcess.Sheet.Range[rowstart, rowend];

                                        object[,] initvalue = row.Value;

                                        string value = string.Empty;
                                        foreach (Excel.Range cell in row)
                                        {
                                            string replacablechar = string.Empty;
                                            if (cell.Text.Contains(paramchar))
                                            {
                                                replacablechar = paramchar;
                                            }
                                            else if (cell.Text.Contains(paramchar1byte))
                                            {
                                                replacablechar = paramchar1byte;
                                            }
                                            else if (cell.Text.Contains(paramchareng))
                                            {
                                                replacablechar = paramchareng;
                                            }

                                            if (!string.IsNullOrEmpty(replacablechar))
                                            {
                                                value = cell.Value;
                                                value = value.Replace(replacablechar, x.ToString());
                                                cell.Application.EnableEvents = false;
                                                cell.Value = value;
                                                cell.Application.EnableEvents = true;
                                            }
                                        }

                                        if (!string.IsNullOrEmpty(DataProcess.Sheet.Cells[keyval.Key, Constants.DP.CriteriaVariableColumn].Text) || !string.IsNullOrEmpty(DataProcess.Sheet.Cells[keyval.Key, Constants.DP.CriteriaOperatorColumn].Text) || !string.IsNullOrEmpty(DataProcess.Sheet.Cells[keyval.Key, Constants.DP.CriteriavalueColumn].Text))
                                        {
                                            if (CheckCriteriaByRow(keyval) == false)
                                            {
                                                row.Value = initvalue;
                                                return false;
                                            }
                                        }
                                        if (CheckInstructionByRow(keyval) == false)
                                        {
                                            row.Value = initvalue;
                                            return false;
                                        }
                                        row.Value = initvalue;

                                        // Fix for 271305 Listup operation when encountered with the CALL within a FOR statement was not detected and 'islistup' was not set to true.
                                        //This portion of code checks for the list of DECST instructions first to check whether a LISTUP operation exist and changes 'islistup=true'
                                        try
                                        {
                                            if (Logic.DataProcessExecute.islistup == false)
                                            {
                                                if (keyval.Value.instruction == Constants.DP.InstructionCALL)
                                                {
                                                    string tempmethodname = DataProcess.Sheet.Cells[keyval.Key, Constants.DP.SubstituteParam1Column].Text;
                                                    DataProcessExecute tempdpExecObject = new Logic.DataProcessExecute(this);
                                                    DPCallMethod tempmethod = DataProcess.decst_ProgramList[tempmethodname];
                                                    int tempstartrow = tempmethod.Rowstart;
                                                    int tempendrow = tempmethod.RowEnd;
                                                    Excel.Range tempcallmethodstart = DataProcess.Sheet.Cells[tempstartrow + 1, Constants.DP.OnOffColumn];
                                                    Excel.Range tempcallmethodend = DataProcess.Sheet.Cells[tempendrow, Constants.DP.OnOffColumn];
                                                    Dictionary<int, DataProcess.Crit_Inst_Operator> tempCALL_criteria_inst_operator_Dict = tempdpExecObject.GetinstructionsByRow(tempcallmethodstart, tempcallmethodend, "", false);
                                                    bool islistuppresent = false;
                                                    foreach (var instruction in tempCALL_criteria_inst_operator_Dict)
                                                    {
                                                        if (instruction.Value.instruction == Constants.DP.InstructionLISTUP)
                                                        {
                                                            islistuppresent = true;
                                                            break;
                                                        }
                                                    }
                                                    if (islistuppresent == true)
                                                        Logic.DataProcessExecute.islistup = true;
                                                }
                                            }

                                        }
                                        catch (Exception ex) { }

                                    }
                                }
                            }
                        }
                        finally
                        {
                            if (!DataProcess.Sheet.Application.ScreenUpdating)
                            {
                                DataProcess.Sheet.Application.ScreenUpdating = true;
                                DataProcess.Sheet.Application.EnableEvents = true;
                            }

                        }
                        //--------------------------//
                        break;

                    // If the user selects instruction FOR from instruction column- 8'th column 
                    case Constants.DP.InstructionNEXT:
                        if (!FORValidationSuccess)
                        {
                            ErrMsg = CommonResource.ERR_INVALID_NEXT;
                            ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", ErrMsg, "", kvp.Key, Constants.DP.InstructionColumn);
                            return false;
                        }
                        else
                        {
                            FORValidationSuccess = false;
                        }
                        break;

                    case Constants.DP.InstructionLISTUP:
                        rangeinstr = Sheet.Cells[kvp.Key, Constants.DP.InstructionColumn] == null ? "" : Sheet.Cells[kvp.Key, Constants.DP.InstructionColumn];
                        rangecriteriavariable = Sheet.Cells[kvp.Key, Constants.DP.CriteriaVariableColumn] == null ? "" : Sheet.Cells[kvp.Key, Constants.DP.CriteriaVariableColumn];
                        rangecriteriaoperator = Sheet.Cells[kvp.Key, Constants.DP.CriteriaOperatorColumn] == null ? "" : Sheet.Cells[kvp.Key, Constants.DP.CriteriaOperatorColumn];
                        rangecriteriavalue = Sheet.Cells[kvp.Key, Constants.DP.CriteriavalueColumn] == null ? "" : Sheet.Cells[kvp.Key, Constants.DP.CriteriavalueColumn];

                        if (
                            (string.IsNullOrEmpty(rangecriteriavariable.Text ) && string.IsNullOrEmpty(rangecriteriaoperator.Text ) && string.IsNullOrEmpty(rangecriteriavalue.Text ))
                           ||
                           !string.IsNullOrEmpty(rangecriteriavariable.Text ) && !string.IsNullOrEmpty(rangecriteriaoperator.Text ) && !string.IsNullOrEmpty(rangecriteriavalue.Text )
                            )
                        {
                            if (!string.IsNullOrEmpty(rangecriteriavariable.Text) && !string.IsNullOrEmpty(rangecriteriaoperator.Text) && !string.IsNullOrEmpty(rangecriteriavalue.Text ))
                            {

                                if (CheckCriteriaByRow(kvp) == false) return false;
                            }
                            if (!Definitions.VariableDictionary.ContainsKey(rangecriteriavalue.Text) && !string.IsNullOrEmpty(rangecriteriavariable.Text) && CheckCriteriaNSAValues(kvp, rangecriteriavariable, rangecriteriaoperator, rangecriteriavalue, rangeinstr) == false) return false;
                            if (CheckInstructionByRow(kvp) == false) return false;// //trial criteria value as variable [Redmine id : 178707] - return false;
                        }
                        else
                        {
                            if (CheckCriteriaByRow(kvp) == false) return false;
                        }
                        Logic.DataProcessExecute.islistup = true;//for listup
                                                                 // if (CheckInstructionByRow(kvp) == false) return false;

                        break;

                }
            }
            return allow;
        }
        private string ValidateFORStatement(int rowFOR)
        {
            int NEXTRow = FindInstructionRow(rowFOR, Constants.DP.InstructionNEXT);
            string ErrMsg = string.Empty;
            if (NEXTRow > 0)
            {
                int decstrow = FindInstructionRow(rowFOR, Constants.DP.InstructionDECST);
                int decend = FindInstructionRow(rowFOR, Constants.DP.InstructionDECEND);
                int FOR = FindInstructionRow(rowFOR, Constants.DP.InstructionFOR);

                if (NEXTRow < rowFOR)
                {
                    ErrMsg = ErrMsg = CommonResource.ERR_NEXT_NOTFOUND;
                }
                //check any FOR or DECST VALUES are nested
                else if ((decstrow < NEXTRow && decstrow > rowFOR) || (decend < NEXTRow && decend > rowFOR))
                {
                    //DECST NESTING ERROR
                    ErrMsg = CommonResource.ERR_DECST_NESTED;
                }

                //else if 
                //{
                //    //DECEND NESTING ERROR
                //    ErrMsg = string.Empty;
                //}


                else if (FOR < NEXTRow && FOR > rowFOR)
                {
                    //FOR NESTING ERROR
                    ErrMsg = CommonResource.ERR_FOR_NESTED;
                }
            }
            else
            {
                ErrMsg = CommonResource.ERR_NEXT_NOTFOUND;
            }

            return ErrMsg;

        }
        private bool CheckCriteriaByRow(KeyValuePair<int, Crit_Inst_Operator> kvp)
        {
            bool retval = true;
            bool showError = false;


            Excel.Range rangecrivariable = Sheet.Cells[kvp.Key, Constants.DP.CriteriaVariableColumn] == null ? "" : Sheet.Cells[kvp.Key, Constants.DP.CriteriaVariableColumn];
            Excel.Range rangecrioperator = Sheet.Cells[kvp.Key, Constants.DP.CriteriaOperatorColumn] == null ? "" : Sheet.Cells[kvp.Key, Constants.DP.CriteriaOperatorColumn];
            Excel.Range rangecrivalue = Sheet.Cells[kvp.Key, Constants.DP.CriteriavalueColumn] == null ? "" : Sheet.Cells[kvp.Key, Constants.DP.CriteriavalueColumn];
            Excel.Range rangeinstr = Sheet.Cells[kvp.Key, Constants.DP.InstructionColumn] == null ? "" : Sheet.Cells[kvp.Key, Constants.DP.InstructionColumn];

            if (!string.IsNullOrEmpty(rangecrivariable.Text) && !Definitions.VariableDictionary.ContainsKey(rangecrivariable.Text))//variable in question settings or not?
            {
                ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", string.Format(CommonResource.ERR_MSG_VARIABLE_NOT_SET_IN_QUESTION_SETTING, rangecrivariable.Text), "", kvp.Key, Constants.DP.CriteriaVariableColumn);
                return false;
            }
            if (string.IsNullOrEmpty(rangecrivariable.Value ))
            {
                ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_CRITERIA_VARIABLE_NOT_SET, "", kvp.Key, Constants.DP.CriteriaVariableColumn);
                return false;
            }
            else if (string.IsNullOrEmpty(rangecrioperator.Text))
            {
                ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_CRITERIA_VARIABLE_PARAM_MISSING, "", kvp.Key, Constants.DP.CriteriaOperatorColumn);
                return false;
            }
            else if (string.IsNullOrEmpty(rangecrivalue.Text ))
            {
                ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_CRITERIA_VARIABLE_PARAM_MISSING, "", kvp.Key, Constants.DP.CriteriavalueColumn);
                return false;
            }
            else if (string.IsNullOrEmpty(rangeinstr.Text))
            {
                ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_DIRECTION, "-", kvp.Key, Constants.DP.InstructionColumn);
                return false;
            }
            if (!Definitions.VariableDictionary.ContainsKey(rangecrivariable.Text))
            {
                //ERR_MSG_DATAPROCESS_VALIDATION_CRITERIA_VARIABLE_PARAM_MISSING
                ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_CRITERIA_VARIABLE_PARAM_MISSING, "", kvp.Key, Constants.DP.CriteriaVariableColumn);
                return false;
            }
            string operatorvalue = rangecrioperator.Value;
            //Redmine id: 176449
            if ((Definitions.VariableDictionary[rangecrivariable.Text].AnswerType == Constants.AnswerType.MA || Definitions.VariableDictionary[rangecrivariable.Text].AnswerType == Constants.AnswerType.FA) && (operatorvalue != "=" && operatorvalue != "<>"))
            {
                //ERR_MSG_INEQUALITY_SIGN_CANNOT_BE_USED_MA_FA
                ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", string.Format(CommonResource.ERR_MSG_INEQUALITY_SIGN_CANNOT_BE_USED_MA_FA, Definitions.VariableDictionary[rangecrivariable.Text].AnswerType == Constants.AnswerType.MA ? Constants.AnswerType.MA : Constants.AnswerType.FA), "", kvp.Key, Constants.DP.CriteriaOperatorColumn);
                return false;
            }
            //IL_JP_MAM_007:4295056908
            if (Definitions.VariableDictionary[rangecrivariable.Text].AnswerType != Constants.AnswerType.FA && rangecrivalue.Text.Contains("<>"))
            {
                ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", CommonResource.SET_NUMERIC_VALUE, "", kvp.Key, Constants.DP.CriteriavalueColumn);
                rangecrivalue.Font.Color = System.Drawing.Color.Red;
                rangecrivalue.Select();
                return false;
            }


            if (operatorvalue != "=" && operatorvalue != "<>")
            {
                int cellvalue = 0;//(^\d+\.\d+$)|(^\d+$)|(^[(]-?\d+[)]$)
                if (Definitions.VariableDictionary.ContainsKey(rangecrivalue.Text) && (Definitions.VariableDictionary.ContainsKey(rangecrivariable.Text)))
                {
                    QuestionSettings qsValue = Definitions.VariableDictionary[rangecrivalue.Text];
                    QuestionSettings qsVariable = Definitions.VariableDictionary[rangecrivariable.Text];
                    if (!ValidateCriteriaValueAsVariable(qsValue, qsVariable) || qsValue.AnswerType == Constants.AnswerType.FA)
                    {

                        ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", CommonResource.SET_NUMERIC_VALUE, "");
                        rangecrivalue.Font.Color = System.Drawing.Color.Red;
                        rangecrivalue.Select();
                        return false;

                    }


                }

                // [Redmine id : 177637] similar issue as range for  other than equal and not eaqual
                //Regex regex = new Regex(@"(^\d +\.\d +$)| (^\d +$)| (^[(] -{ 1}\d +[)]$)| (^[(] -{ 1}\d +\.\d +[)]$)");
                else if (Definitions.VariableDictionary[rangecrivariable.Text].AnswerType == Constants.AnswerType.SA)
                {
                    Regex regex = new Regex(@"(^\d*$)");
                    if (!regex.Match(rangecrivalue.Value.ToString()).Success)
                    {
                        ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", CommonResource.SET_NUMERIC_VALUE, "");
                        rangecrivalue.Font.Color = System.Drawing.Color.Red;
                        rangecrivalue.Select();
                        return false;
                    }
                }
                else if (Definitions.VariableDictionary[rangecrivariable.Text].AnswerType == Constants.AnswerType.N)
                {
                    // Regex regex = new Regex(@"(^\d*$)");
                    Regex regex2 = new Regex(@"(^([(][-])\d*\.?\d*[)]$)|(^\d*\.?\d*$)");
                    if (!regex2.Match(rangecrivalue.Value.ToString()).Success)
                    {
                        ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", CommonResource.SET_NUMERIC_VALUE, "");
                        rangecrivalue.Font.Color = System.Drawing.Color.Red;
                        rangecrivalue.Select();
                        return false;
                    }

                }

            }
            else if (Definitions.VariableDictionary.ContainsKey(rangecrivalue.Text) && (Definitions.VariableDictionary.ContainsKey(rangecrivariable.Text)))
            {
                QuestionSettings qsValue = Definitions.VariableDictionary[rangecrivalue.Text];
                QuestionSettings qsVariable = Definitions.VariableDictionary[rangecrivariable.Text];

                if (!ValidateCriteriaValueAsVariable(qsValue, qsVariable))
                {

                    ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", CommonResource.SET_NUMERIC_VALUE, "");
                    rangecrivalue.Font.Color = System.Drawing.Color.Red;
                    rangecrivalue.Select();
                    return false;

                }


            }
            else if (Definitions.VariableDictionary[rangecrivariable.Text].AnswerType == Constants.AnswerType.N && !(string.Equals(rangecrivalue.Text, "*") || string.Equals(rangecrivalue.Text, "DK")))// if (Definitions.VariableDictionary[rangecrivariable.Text].AnswerType == Constants.AnswerType.N)
            {

                if (!ValidateNtypeCriteria(rangecrivalue, false, rangecrivariable.Column, true))//QC4Common.Validation.NumberCheck.Check(rangecrivalue.Text, 0))//0 from Ntype
                {
                    //SET_NUMERIC_VALUE
                    ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", CommonResource.SET_NUMERIC_VALUE, "");
                    rangecrivalue.Font.Color = System.Drawing.Color.Red;
                    rangecrivalue.Select();
                    return false;
                }

            }
            else if ((Definitions.VariableDictionary[rangecrivariable.Text].AnswerType == Constants.AnswerType.SA || Definitions.VariableDictionary[rangecrivariable.Text].AnswerType == Constants.AnswerType.MA) && !(string.Equals(rangecrivalue.Text, "*") || string.Equals(rangecrivalue.Text, "DK")))//IL_JP_MAM_007:4295056905 //IL_JP_MAM_007:4295056898//(Definitions.VariableDictionary[rangecrivariable.Text].AnswerType == Constants.AnswerType.SA)
            {
                if (!QC4Common.Validation.NumberCheck.Check(rangecrivalue.Text, 0))//0 from Ntype
                {
                    //SET_NUMERIC_VALUE
                    ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", CommonResource.SET_NUMERIC_VALUE, "");
                    rangecrivalue.Font.Color = System.Drawing.Color.Red;
                    rangecrivalue.Select();
                    return false;
                }

                showError = !ValidateNumericCell(rangecrivalue, 0, Definitions.VariableDictionary[rangecrivariable.Text].CategoryCount, false, Constants.DP.CriteriaVariableColumn, true);
                //if (Definitions.VariableDictionary[rangecrivariable.Text].CategoryCount <Convert.ToInt32(rangecrivalue.Text))
                //{
                //    //greater than cat count
                //}
            }
            // retval = CheckCriteriaNSAValues(kvp, rangecrivariable, rangecrioperator, rangecrivalue, rangeinstr);

            //Redmine id : 177646
            if (!Definitions.VariableDictionary.ContainsKey(rangecrivalue.Text) && !showError && (Definitions.VariableDictionary[rangecrivariable.Text].AnswerType != Constants.AnswerType.FA && operatorvalue == "<>" && (((rangecrivalue.Text).StartsWith("!")) || ((rangecrivalue.Text).StartsWith("<>")))))
            {
                ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_CRITERIA_VARIABLE_NOT_SET, string.Empty);
                rangecrivalue.Font.Color = System.Drawing.Color.Red;
                rangecrivalue.Select();
                return false;
            }

            //IL_JP_MAM_007:4295056907  4295056907
            if (!Definitions.VariableDictionary.ContainsKey(rangecrivalue.Text) && !showError && (Definitions.VariableDictionary[rangecrivariable.Text].AnswerType != Constants.AnswerType.FA))
            {
                Regex regexnegativevalues = new Regex(@"^([!]?[(]{1}[-]{1}\d*\.?\d+[)]{1}[-]{1}[(]{1}[-]{1}\d*\.?\d+[)]{1}$)");
                //^(\d+*\.?\d+[-]{1}\d+*\.?\d+$)
                Regex regex = new Regex(@"^([!]?[(]?[-]?\d*\.?\d+[)]?[-]{1}[(]?[-]?\d*\.?\d+[)]?$)");//^([!]?\d*\.?\d+[-]{1}\d*\.?\d+$)
                if (regex.Match(rangecrivalue.Value.ToString()).Success)//if success range then check reverse order
                {
                    string value = rangecrivalue.Value.ToString();
                    value = value.Replace("!", string.Empty);

                    string Div_Char = "@";
                    value = value.Replace("(-", Div_Char);
                    value = value.Replace("-", ",");

                    string[] tempval = value.Split(',');
                    foreach (string str in tempval)
                    {
                        Regex tempregex = new Regex(@"^[(]\d*.?\d+[)]$");//^([!]?\d*\.?\d+[-]{1}\d*\.?\d+$)
                        if (tempregex.Match(str).Success)//if success range then check reverse order
                        {
                            ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_CRITERIA_VARIABLE_NOT_SET, "");
                            rangecrivalue.Font.Color = System.Drawing.Color.Red;
                            rangecrivalue.Select();
                            return false;
                        }
                    }

                    value = value.Replace("(", "");
                    value = value.Replace(")", "");

                    value = value.Replace(Div_Char, "-");
                    string[] splitval = value.Split(',');
                    bool falsevalue = false;

                    float value1 = 0, value2 = 0;
                    if (!float.TryParse(splitval[0], out value1))
                    {
                        falsevalue = true;
                    }
                    if (!float.TryParse(splitval[1], out value2))
                    {
                        falsevalue = true;
                    }

                    if (falsevalue)
                    {
                        ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", CommonResource.SET_NUMERIC_VALUE, "");
                        rangecrivalue.Font.Color = System.Drawing.Color.Red;
                        rangecrivalue.Select();
                        return false;
                    }
                    if (value1 > value2)
                    {
                        ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_CRITERIA_VARIABLE_NOT_SET, "");
                        rangecrivalue.Font.Color = System.Drawing.Color.Red;
                        rangecrivalue.Select();
                        return false;
                    }

                }
                else if (!showError && regexnegativevalues.Match(rangecrivalue.Value.ToString()).Success)
                {
                    string value = rangecrivalue.Value.ToString();
                    string Div_Char = "@";
                    value = value.Replace("(-", Div_Char);
                    value = value.Replace("(", "");
                    value = value.Replace(")", "");
                    value = value.Replace("-", ",");
                    value = value.Replace(Div_Char, "-");
                    string[] splitval = value.Split(',');
                    float value1 = 0, value2 = 0;
                    bool falsevalue = false;
                    if (!float.TryParse(splitval[0], out value1))
                    {
                        falsevalue = true;
                    }
                    if (!float.TryParse(splitval[1], out value2))
                    {
                        falsevalue = true;
                    }
                    if (falsevalue)
                    {
                        ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", CommonResource.SET_NUMERIC_VALUE, "");
                        rangecrivalue.Font.Color = System.Drawing.Color.Red;
                        rangecrivalue.Select();
                        return false;
                    }
                    if (value1 > value2)
                    {
                        ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_CRITERIA_VARIABLE_NOT_SET, "");
                        rangecrivalue.Font.Color = System.Drawing.Color.Red;
                        rangecrivalue.Select();
                        return false;
                    }

                }
            }

            if (showError)
            {
                ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_CRITERIA_VARIABLE_NOT_SET, string.Empty, kvp.Key, Constants.DP.CriteriaVariableColumn);
                return false;
            }
            return retval;
        }

        private bool CheckCriteriaNSAValues(KeyValuePair<int, Crit_Inst_Operator> kvp, Excel.Range rangecrivariable, Excel.Range rangecrioperator, Excel.Range rangecrivalue, Excel.Range rangeinstr)
        {
            bool showError = false;
            if (Definitions.VariableDictionary.ContainsKey(rangecrivariable.Text))
            {
                string rangevalue = rangecrivalue.Text;
                rangevalue = MinMaxAppendWithMinus(rangevalue, rangecrivalue.Row, rangecrivariable.Column);
                if (Definitions.VariableDictionary[rangecrivariable.Text].AnswerType == Constants.AnswerType.N && !(string.Equals(rangecrivalue.Text, "*") || string.Equals(rangecrivalue.Text, "DK")))
                {

                    if (!ValidateNtypeCriteria(rangecrivalue, false, rangecrivariable.Column, true))// if (!QC4Common.Validation.NumberCheck.Check(rangevalue, 0))//0 from Ntype
                    {
                        //SET_NUMERIC_VALUE
                        ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", CommonResource.SET_NUMERIC_VALUE, "");
                        rangecrivalue.Select();
                        return false;
                    }

                }
                else if (Definitions.VariableDictionary[rangecrivariable.Text].AnswerType == Constants.AnswerType.SA && !(string.Equals(rangecrivalue.Text, "*") || string.Equals(rangecrivalue.Text, "DK")))
                {
                    if (!QC4Common.Validation.NumberCheck.Check(rangevalue, Definitions.VariableDictionary[rangecrivariable.Text].CategoryCount))
                    {
                        //SET_NUMERIC_VALUE
                        ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", CommonResource.SET_NUMERIC_VALUE, "");
                        rangecrivalue.Select();
                        return false;
                    }

                    showError = !ValidateNumericCell(rangecrivalue, 0, Definitions.VariableDictionary[rangecrivariable.Text].CategoryCount, false, Constants.DP.CriteriaVariableColumn, true);
                    //if (Definitions.VariableDictionary[rangecrivariable.Text].CategoryCount <Convert.ToInt32(rangecrivalue.Text))
                    //{
                    //    //greater than cat count
                    //}
                }
            }
            else
            {
                showError = true;
            }
            if (showError)
            {
                ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_CRITERIA_VARIABLE_NOT_SET, string.Empty, kvp.Key, Constants.DP.CriteriaVariableColumn);
                return false;
            }
            return true;
        }
        private int FindLargestofMconvertParam2(int row)
        {
            Excel.Range param2 = Sheet.Cells[row, Constants.DP.SubstituteParam2Column];

            string param2Text = param2.Text;
            string[] SplitContent;
            string param2check = string.Empty;
            string splitstring = string.Empty;
            string[] hypensplit = null;

            int largest = 0;
            int convertedvalue = 0;
            if (param2.Text.Contains("/") || param2.Text.Contains(",") || param2.Text.Contains("-"))
            {
                char[] splitchar = { '/', ',' };
                SplitContent = param2.Text.Split(splitchar);
                foreach (string stringparam in SplitContent)
                {
                    if (!stringparam.Contains("-"))
                    {
                        if (int.TryParse(stringparam, out convertedvalue))
                        {
                            if (convertedvalue > largest)
                            {
                                largest = convertedvalue;
                            }
                        }
                        continue;
                    }
                    hypensplit = stringparam.Split('-');
                    if (string.IsNullOrEmpty(hypensplit[1]))
                    {
                        hypensplit[1] = MinMaxAppendWithMinus(hypensplit[0], row, Constants.DP.SubstituteVariableColumn);
                    }
                    else if (string.IsNullOrEmpty(hypensplit[0]))
                    {
                        hypensplit[0] = "1";
                    }
                    if (int.TryParse(hypensplit[1], out convertedvalue))
                    {
                        if (convertedvalue > largest)
                        {
                            largest = convertedvalue;
                        }
                    }


                }

            }

            else
            {
                //hypensplit = param2.Text.Split('-');
                splitstring = param2.Text;
                int.TryParse(splitstring, out largest);
            }
            return largest;
        }
        //private bool ValidateNumericColumns(int startColumn, int count, )
        /// <summary>
        /// Method to check each data process instructions by row
        /// </summary>
        /// <param name="kvp">Data process instructions</param>
        /// <returns>return bool value if checking is successfull</returns>
        private bool CheckInstructionByRow(KeyValuePair<int, Crit_Inst_Operator> kvp)//checking each row
        {
            bool retval = true;
            try
            {

                if (string.IsNullOrEmpty(kvp.Value.substituteoperator) && string.Equals(kvp.Value.instruction, Constants.DP.InstructionTHEN))
                {
                    Excel.Range rangeparamvariable = Sheet.Cells[kvp.Key, Constants.DP.SubstituteVariableColumn] == null ? "" : Sheet.Cells[kvp.Key, Constants.DP.SubstituteVariableColumn];
                    Excel.Range rangeparamoperator = Sheet.Cells[kvp.Key, Constants.DP.SubstituteOperatorColumn] == null ? "" : Sheet.Cells[kvp.Key, Constants.DP.SubstituteOperatorColumn];
                    if (string.IsNullOrEmpty(rangeparamvariable.Text ))//need to chek variable in dictionary
                    {
                        ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_VARIABLE, "", kvp.Key, Constants.DP.SubstituteVariableColumn);
                    }
                    else if (string.IsNullOrEmpty(rangeparamoperator.Text ))
                    {
                        ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_OPERATOR, "", kvp.Key, Constants.DP.SubstituteOperatorColumn);
                    }
                    else if (!Definitions.VariableDictionary.ContainsKey(rangeparamvariable.Text))


                    { ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_VARIABLE, "", kvp.Key, Constants.DP.SubstituteVariableColumn); }
                    else
                        ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_VARIABLE, "-", kvp.Key, Constants.DP.SubstituteVariableColumn);
                    return false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("StackTrace:{0}", ex.StackTrace);
                ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_VARIABLE, "*", kvp.Key, Constants.DP.SubstituteVariableColumn);
                return false;
            }
            try
            {
                Excel.Range param1 = Sheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column];
                Excel.Range param2 = Sheet.Cells[kvp.Key, Constants.DP.SubstituteParam2Column];
                Excel.Range param3 = Sheet.Cells[kvp.Key, Constants.DP.SubstituteParam3Column];
                Excel.Range param4 = Sheet.Cells[kvp.Key, Constants.DP.SubstituteParam3Column + 1];
                Excel.Range targetvariable = Sheet.Cells[kvp.Key, Constants.DP.SubstituteVariableColumn];
                if (!string.IsNullOrEmpty(targetvariable.Text) && !Definitions.VariableDictionary.ContainsKey(targetvariable.Text))//variable in question settings or not?
                {
                    ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", string.Format(CommonResource.ERR_MSG_VARIABLE_NOT_SET_IN_QUESTION_SETTING, targetvariable.Text), "", kvp.Key, Constants.DP.SubstituteVariableColumn);
                    return false;
                }
                bool showError = false;
                string ErrorMsg = string.Format(CommonResource.ERR_MSG_COUNT_PARAM_OVERLAP, kvp.Value.substituteoperator);
                int columNumber = 0;
                switch (kvp.Value.instruction)
                {
                    case Constants.DP.InstructionTHEN:
                        switch (kvp.Value.substituteoperator)
                        {
                            case Constants.DP.SubstituteOperatorRECODE:
                                if (Definitions.VariableDictionary.ContainsKey(targetvariable.Value))
                                {
                                    QuestionSettings qs = Definitions.VariableDictionary[targetvariable.Value];


                                    if (string.IsNullOrEmpty(param1.Text) || string.IsNullOrEmpty(param2.Text))// == null || param1.Value == "")
                                    {
                                        showError = true;
                                        break;
                                    }

                                    if (qs.AnswerType == Constants.AnswerType.SA && (Definitions.VariableDictionary.ContainsKey(param1.Text)))
                                    {
                                        if (qs.AnswerType != Definitions.VariableDictionary[param1.Text].AnswerType)
                                        {
                                            showError = true;
                                            break;
                                        }

                                    }

                                    //------------to find any missing params in b/w-----------//
                                    Excel.Range endParam = ExcelUtilForAddIn.EndxlRight(param2);

                                    if (endParam.Column >= qs.CategoryCount + Constants.DP.SubstituteParam2Column)
                                    {
                                        //RedmineFix : 174046 , Todo: set err msg 
                                        //ErrorMsg = CommonResource.
                                        showError = true;
                                        break;
                                    }
                                    Excel.Range paramRange = DataProcess.Sheet.Range[param2, endParam];
                                    foreach (Excel.Range param in paramRange.Cells)
                                    {
                                        if (string.IsNullOrEmpty(param.Text))
                                        {
                                            ErrorMsg = CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_JOINT_VALUE_MISSING;
                                            showError = true;
                                            break;
                                        }
                                    }
                                    if (showError) break;
                                    //-----------------------------------//
                                    int val = 0;
                                    for (int i = 0; i < qs.CategoryCount; i++)
                                    {
                                        Excel.Range paramN = Sheet.Cells[kvp.Key, Constants.DP.SubstituteParam2Column + i];
                                        if (!string.IsNullOrEmpty(paramN.Text))
                                        {
                                            if (Definitions.VariableDictionary.ContainsKey(param1.Text))
                                            {
                                                showError = !ValidateNumericCell(paramN, 0, Definitions.VariableDictionary[param1.Text].CategoryCount, false, Constants.DP.SubstituteParam1Column, true);
                                            }
                                            else
                                            {
                                                showError = true;

                                            }
                                            if (!showError)
                                            {
                                                string paramvaluecontent = paramN.Text;
                                                if (paramvaluecontent.Length == 1 && !paramvaluecontent.IsIntegerExpression(out val))
                                                {
                                                    ErrorMsg = CommonResource.ERR_MSG_SET_NUMERIC_VALUE;
                                                    showError = true;
                                                    columNumber = paramN.Column;
                                                    paramN.Font.Color = Constants.Color.Red;
                                                    break;
                                                }

                                                //Commenting for Redmine id : 191425  https://app.gluemodel.com/#/project/task/4295061708
                                                //int countofseperator = paramvaluecontent.Count(f => (f == '-'));//https://app.gluemodel.com/#/project/task/4295061556
                                                //if (countofseperator > 1)
                                                //{
                                                //    ErrorMsg = CommonResource.CANNOT_SPECIFY_MULTIPLE_RANGE;
                                                //    showError = true;
                                                //    columNumber = paramN.Column;
                                                //    paramN.Font.Color = Constants.Color.Red;
                                                //    break;
                                                //}
                                            }
                                        }
                                        if (showError) break;
                                    }


                                }
                                else
                                {
                                    ErrorMsg = CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_INVALID_VARIABLE;
                                    showError = true;

                                }
                                break;

                            case Constants.DP.SubstituteOperatorMIN:
                            case Constants.DP.SubstituteOperatorMAX:
                            case Constants.DP.SubstituteOperatorAVG:
                            case Constants.DP.SubstituteOperatorSUM:
                                if (string.IsNullOrEmpty(param1.Text))
                                {
                                    showError = true;
                                }

                                Excel.Range paramfirst = DataProcess.Sheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column];
                                Excel.Range endcell = ExcelUtilForAddIn.EndxlRight(paramfirst);
                                Excel.Range paramrange = DataProcess.Sheet.Range[paramfirst, endcell];
                                if (paramrange.Columns.Count > 1)
                                {
                                    object[,] paramsarray = paramrange.Value;
                                    for (int j = 1; j <= paramsarray.GetLength(1); j++)
                                    {
                                        if (string.IsNullOrEmpty(Convert.ToString(paramsarray[1, j])))//# 197600//  if (paramsarray[1, j] == null)
                                        {
                                            showError = true;
                                            columNumber = paramfirst.Column + j-1;
                                            break;
                                        }
                                        else if (Definitions.VariableDictionary.ContainsKey(paramsarray[1, j].ToString()))
                                        {
                                            if (Definitions.VariableDictionary[paramsarray[1, j].ToString()].AnswerType != Constants.AnswerType.SA &&
                                                Definitions.VariableDictionary[paramsarray[1, j].ToString()].AnswerType != Constants.AnswerType.N)
                                            {
                                                showError = true;
                                                ErrorMsg = CommonResource.UNSUPPORTED_VARIABLETYPE;
                                                break;
                                            }
                                        }
                                        else if (!string.IsNullOrEmpty(paramsarray[1, j].ToString()) && !Definitions.VariableDictionary.ContainsKey(paramsarray[1, j].ToString()))//variable in question settings or not?
                                        {
                                            ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", string.Format(CommonResource.ERR_MSG_VARIABLE_NOT_SET_IN_QUESTION_SETTING, paramsarray[1, j].ToString()), "", kvp.Key, Constants.DP.SubstituteParam1Column + j - 1);
                                            return false;
                                        }

                                    }
                                }
                                else if (!string.IsNullOrEmpty(paramrange.Text) && !Definitions.VariableDictionary.ContainsKey(paramrange.Text))//variable in question settings or not?
                                {
                                    ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", string.Format(CommonResource.ERR_MSG_VARIABLE_NOT_SET_IN_QUESTION_SETTING, paramrange.Text), "", kvp.Key, Constants.DP.SubstituteParam1Column);
                                    return false;
                                }
                                break;
                            case Constants.DP.SubstituteOperatorMCONVERT:
                                if (string.IsNullOrEmpty(param1.Text) || string.IsNullOrEmpty(param2.Text))
                                {
                                    showError = true;
                                    break;
                                }


                                if (Definitions.VariableDictionary.ContainsKey(targetvariable.Value))
                                {
                                    QuestionSettings qs = Definitions.VariableDictionary[targetvariable.Value];
                                    if (!ValidateNumericCell(param2, 0, Int32.MaxValue, false, Constants.DP.SubstituteVariableColumn))
                                    {
                                        showError = true;
                                        break;
                                    }

                                    int largest = FindLargestofMconvertParam2(kvp.Key);

                                    for (int i = 0; i < qs.CategoryCount; i++)
                                    {
                                        Excel.Range paramN = Sheet.Cells[kvp.Key, Constants.DP.SubstituteParam3Column + i];
                                        if (Definitions.VariableDictionary.ContainsKey(paramN.Text))
                                        {


                                            //int intvalue = Convert.ToInt32(!string.IsNullOrEmpty(param2check) ? param2check : param2Text);
                                            //// int intvalue = Convert.ToInt32(param2.Text);
                                            if ((Definitions.VariableDictionary[paramN.Text].CategoryCount < largest) ||
                                                (i > 0 && Definitions.VariableDictionary[paramN.Text].CategoryCount != Definitions.VariableDictionary[param3.Text].CategoryCount))
                                            {
                                                showError = true;
                                                break;
                                            }

                                        }
                                        else
                                        {
                                            showError = true;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    showError = true;

                                }
                                if (showError) ErrorMsg = string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_NOT_SET_MCONVERT, kvp.Value.substituteoperator);
                                break;
                            case Constants.DP.SubstituteOperatorADD1:
                            case Constants.DP.SubstituteOperatorMINUS1:
                            case Constants.DP.SubstituteOperatorADD2:
                            case Constants.DP.SubstituteOperatorMINUS2:
                            case Constants.DP.SubstituteOperatorEQUAL:
                            case Constants.DP.SubstituteOperatorCOMPUTE:

                                if ((kvp.Value.substituteoperator == Constants.DP.SubstituteOperatorADD1 || kvp.Value.substituteoperator == Constants.DP.SubstituteOperatorMINUS1) && !string.IsNullOrEmpty(param1.Text) && !Definitions.VariableDictionary.ContainsKey(param1.Text))//variable in question settings or not?
                                {
                                    ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", string.Format(CommonResource.ERR_MSG_VARIABLE_NOT_SET_IN_QUESTION_SETTING, param1.Text), "", kvp.Key, Constants.DP.SubstituteParam1Column);
                                    return false;
                                }


                                if (string.IsNullOrEmpty(param1.Text))
                                {
                                    showError = true;
                                    break;
                                }
                                if (kvp.Value.substituteoperator == Constants.DP.SubstituteOperatorADD1 || kvp.Value.substituteoperator == Constants.DP.SubstituteOperatorMINUS1)
                                {
                                    if (!string.IsNullOrEmpty(param2.Text))
                                    {
                                        int choicecount = Definitions.VariableDictionary[targetvariable.Text].CategoryCount;//int choicecount = Definitions.VariableDictionary[param1.Text].CategoryCount;
                                        if (!ValidateNumericCell(param2, 1, choicecount, false, Constants.DP.SubstituteVariableColumn))
                                        {
                                            param2.Font.Color = Constants.Color.Red;
                                            showError = true;
                                        }
                                        break;
                                    }

                                }
                                else if (kvp.Value.substituteoperator == Constants.DP.SubstituteOperatorCOMPUTE)
                                {
                                    bool isnotnumeric = false;
                                    //ValidateCOMPUTEParam(param1);
                                    string[] paramnames = param1.Text.Split('[', ']');
                                    for (int pi = 1; pi < paramnames.Length; pi += 2)
                                    {
                                        if (!Definitions.VariableDictionary.ContainsKey(paramnames[pi]))
                                        {
                                            ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", string.Format(CommonResource.LDEL_VALIDATION_ERROR1, Constants.DP.SubstituteOperatorCOMPUTE), string.Empty, kvp.Key, Constants.DP.SubstituteParam1Column);// MessageDialog.ErrorOk(CommonResource.ERR_MSG_COMPUTE_CONSTRUCTION_SENTENCE_INCORRECT);//CommonResource.ERR_MSG_COMPUTE_CONSTRUCTION_SENTENCE_INCORRECT
                                            return false;// showError = true;                                          
                                        }
                                    }
                                    if (Definitions.VariableDictionary[targetvariable.Text].AnswerType.Equals(Constants.AnswerType.N))//chek if non integers are there 
                                    {
                                        string val = string.Empty;
                                        for (int pi = 0; pi < paramnames.Length; pi++)
                                        {
                                            if (Definitions.VariableDictionary.ContainsKey(paramnames[pi]))
                                            {
                                                Excel.Range subvariablerange = Sheet.Cells[kvp.Key, Constants.DP.SubstituteVariableColumn];
                                                if (string.IsNullOrEmpty(subvariablerange.Text) || !Definitions.VariableDictionary.ContainsKey(subvariablerange.Text))
                                                {
                                                    // ExcelUtilForAddIn.SetFontColor(changedCell, Constants.Color.Red);
                                                    return false;
                                                }
                                                QuestionSettings qssubvariable = Definitions.VariableDictionary[subvariablerange.Text];
                                                QuestionSettings qsObject = Definitions.VariableDictionary[paramnames[pi]];
                                                if ((qssubvariable.AnswerType.Equals(Constants.AnswerType.N) && (qsObject.AnswerType.Equals(Constants.AnswerType.FA) || qsObject.AnswerType.Equals(Constants.AnswerType.SA) || qsObject.AnswerType.Equals(Constants.AnswerType.N)))
                                                    ||
                                                    (qssubvariable.AnswerType.Equals(Constants.AnswerType.FA) && (qsObject.AnswerType.Equals(Constants.AnswerType.FA) || qsObject.AnswerType.Equals(Constants.AnswerType.SA) || qsObject.AnswerType.Equals(Constants.AnswerType.N) || qsObject.AnswerType.Equals(Constants.AnswerType.MA)))
                                                    )
                                                {
                                                    val += "1";
                                                }
                                                else
                                                {
                                                    //Redmine id: 175141
                                                    isnotnumeric = true;
                                                    //ERR_MSG_COMPUTE_NOT_NUMERIC // nees  ok cancel button
                                                    // System.Windows.Forms.DialogResult res = MessageDialog.InfoYesNo(string.Format(CommonResource.ERR_MSG_COMPUTE_NOT_NUMERIC, Constants.DP.SubstituteOperatorCOMPUTE, subvariablerange.Text)); //MessageDialog.ErrorOk(string.Format(CommonResource.ERR_MSG_COMPUTE_MA_NOT_ALLOWED, "N"));//ERR_MSG_COMPUTE_MA_NOT_ALLOWED  MessageDialog.ErrorOk("Questions MA not allowed");
                                                    //if (res == System.Windows.Forms.DialogResult.No)
                                                    // {
                                                    //     return false;
                                                    // }
                                                }
                                            }
                                            else
                                            {
                                                val += paramnames[pi];
                                            }
                                            ////if (!Definitions.VariableDictionary.ContainsKey(paramnames[pi]))//check if not in dict then those are non integers
                                            ////{
                                            ////    val += paramnames[pi];
                                            ////    //string val = paramnames[pi] + "1";
                                            ////    //var evalres = Sheet.Application.Evaluate(val);
                                            ////    //switch (evalres)
                                            ////    //{
                                            ////    //    case Constants.DP.ErrDiv0:
                                            ////    //    case Constants.DP.ErrGettingData:
                                            ////    //    case Constants.DP.ErrName:
                                            ////    //    case Constants.DP.ErrNA:
                                            ////    //    case Constants.DP.ErrNull:
                                            ////    //    case Constants.DP.ErrNum:
                                            ////    //    case Constants.DP.ErrRef:
                                            ////    //    case Constants.DP.ErrValue:
                                            ////    //        ShowAlert(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW + " " + kvp.Key.ToString(), string.Format(CommonResource.LDEL_VALIDATION_ERROR1, Constants.DP.SubstituteOperatorCOMPUTE), string.Empty);
                                            ////    //        return false;

                                            ////    //}
                                            ////    // if (!IsWholeDigits(paramnames[pi])) ///^-?\d+\.?\d*$/
                                            ////    //ShowAlert(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW + " " + kvp.Key.ToString(), string.Format(CommonResource.LDEL_VALIDATION_ERROR1, Constants.DP.SubstituteOperatorCOMPUTE), string.Empty);
                                            ////    //return false;
                                            ////}
                                            ////else
                                            ////{
                                            ////    val += paramnames[pi];
                                            ////}
                                        }

                                        var evalres = Sheet.Application.Evaluate(val);
                                        switch (evalres)
                                        {
                                            case Constants.DP.ErrDiv0:
                                            case Constants.DP.ErrGettingData:
                                            case Constants.DP.ErrName:
                                            case Constants.DP.ErrNA:
                                            case Constants.DP.ErrNull:
                                            case Constants.DP.ErrNum:
                                            case Constants.DP.ErrRef:
                                            case Constants.DP.ErrValue:
                                                //Redmine id: 175141
                                                isnotnumeric = true;
                                                //System.Windows.Forms.DialogResult res = MessageDialog.InfoYesNo(string.Format(CommonResource.ERR_MSG_COMPUTE_NOT_NUMERIC, Constants.DP.SubstituteOperatorCOMPUTE, Sheet.Cells[kvp.Key, Constants.DP.SubstituteVariableColumn])); //MessageDialog.ErrorOk(string.Format(CommonResource.ERR_MSG_COMPUTE_MA_NOT_ALLOWED, "N"));//ERR_MSG_COMPUTE_MA_NOT_ALLOWED  MessageDialog.ErrorOk("Questions MA not allowed");
                                                //if (res == System.Windows.Forms.DialogResult.No)
                                                //{
                                                //    return false;
                                                //}
                                                // ShowAlert(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW + " " + kvp.Key.ToString(), string.Format(CommonResource.LDEL_VALIDATION_ERROR1, Constants.DP.SubstituteOperatorCOMPUTE), string.Empty);
                                                //  return false;
                                                break;
                                        }
                                    }
                                    //Redmine id: 175141
                                    if (isnotnumeric && isexecute)
                                    {
                                        System.Windows.Forms.DialogResult res = MessageDialog.InfoYesNo(string.Format(CommonResource.ERR_MSG_COMPUTE_NOT_NUMERIC, Constants.DP.SubstituteOperatorCOMPUTE, targetvariable.Text)); //MessageDialog.ErrorOk(string.Format(CommonResource.ERR_MSG_COMPUTE_MA_NOT_ALLOWED, "N"));//ERR_MSG_COMPUTE_MA_NOT_ALLOWED  MessageDialog.ErrorOk("Questions MA not allowed");
                                        if (res == System.Windows.Forms.DialogResult.No)
                                        {
                                            return false;
                                        }
                                        // isexecute = false;
                                    }
                                    //if( showError == true)
                                    //break;
                                    if (Definitions.VariableDictionary.ContainsKey(param1.Value) && !Definitions.VariableDictionary[param1.Value].AnswerType.Equals(Constants.AnswerType.FA) && !Definitions.VariableDictionary[param1.Value].AnswerType.Equals(Constants.AnswerType.SA) && !Definitions.VariableDictionary[param1.Value].AnswerType.Equals(Constants.AnswerType.N))
                                    {

                                        MessageDialog.ErrorOk(string.Format(CommonResource.ERR_MSG_COMPUTE_MA_NOT_ALLOWED, "N"));//ERR_MSG_COMPUTE_MA_NOT_ALLOWED  MessageDialog.ErrorOk("Questions MA not allowed");
                                        return false;          // showError = true;
                                    }

                                }
                                else if (kvp.Value.substituteoperator == Constants.DP.SubstituteOperatorEQUAL)
                                {

                                    if (Definitions.VariableDictionary.ContainsKey(targetvariable.Value))
                                    {
                                        //if (!string.IsNullOrEmpty(param1.Text) && !Definitions.VariableDictionary.ContainsKey(param1.Text) &&
                                        //    Definitions.VariableDictionary[targetvariable.Text].AnswerType != Constants.AnswerType.FA && !int.TryParse(param1.Text, out int returnnumval))//variable in question settings or not?
                                        //{
                                        //    ShowAlert(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW + " " + kvp.Key.ToString(), string.Format(CommonResource.ERR_MSG_VARIABLE_NOT_SET_IN_QUESTION_SETTING, param1.Text), "");
                                        //    return false;
                                        //}
                                        QuestionSettings qstnDet = Definitions.VariableDictionary[targetvariable.Value];
                                        if (qstnDet.AnswerType == Constants.AnswerType.MA)
                                        {

                                            /* For MA only SA/MA type variable can set as source variable*/
                                            if (Definitions.VariableDictionary.ContainsKey(param1.Text) && Definitions.VariableDictionary[param1.Text].AnswerType != Constants.AnswerType.SA && Definitions.VariableDictionary[param1.Text].AnswerType != Constants.AnswerType.MA)
                                            {
                                                ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", string.Format(CommonResource.LDEL_VALIDATION_ERROR1, Constants.DP.SubstituteOperatorEQUAL), string.Empty, kvp.Key, Constants.DP.SubstituteParam1Column);
                                                return false;
                                            }

                                            string param1Str = param1.Text;//  if (str.StartsWith("!")) notvalue = str.TrimStart('!');
                                            param1Str = param1Str.StartsWith("!") ? param1Str.TrimStart('!') : param1Str;
                                            if (!Definitions.VariableDictionary.ContainsKey(param1Str) && !param1Str.Equals("*") && !param1Str.Equals("DK"))
                                            {
                                                string[] arrData = param1Str.Split(',', '/', '-');
                                                if (arrData.Length > 0)
                                                {
                                                    foreach (string arrVal in arrData)
                                                    {
                                                        if (arrVal.Trim().Length > 0)
                                                        {
                                                            string paramvalue = arrVal;
                                                            //if (!string.IsNullOrEmpty(paramvalue) && (paramvalue).StartsWith("!"))//commented for misplace !
                                                            //{
                                                            //    paramvalue = paramvalue.Replace("!", string.Empty);
                                                            //}
                                                            if (paramvalue.Contains("!"))
                                                            {
                                                                // ERR_MSG_VALIDATION_CORRECTDATA
                                                                ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", string.Format(CommonResource.ERR_MSG_VALIDATION_CORRECTDATA, "!"), "", kvp.Key, Constants.DP.SubstituteParam1Column);
                                                                return false;
                                                            }
                                                            int number;
                                                            if (!int.TryParse(paramvalue, out number))
                                                            {
                                                                //// ERR_MSG_VALIDATION_CORRECTDATA
                                                                //ShowAlert(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW + " " + kvp.Key.ToString(), string.Format(CommonResource.ERR_MSG_VALIDATION_CORRECTDATA, "!"), "");
                                                                //return false;
                                                                showError = true;
                                                                columNumber = param1.Column;
                                                                break;
                                                            }
                                                            if (number <= 0)
                                                            {
                                                                showError = true;
                                                                columNumber = param1.Column;
                                                                break;
                                                            }
                                                            else if (number > qstnDet.CategoryCount)
                                                            {
                                                                showError = true;
                                                                columNumber = param1.Column;
                                                                break;
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    showError = true;
                                                    columNumber = param1.Column;
                                                    break;
                                                }
                                            }
                                        }
                                        else if (qstnDet.AnswerType == Constants.AnswerType.SA)
                                        {
                                            /*For SA only SA type variables can set as source variable
*/
                                            if (Definitions.VariableDictionary.ContainsKey(param1.Text) && Definitions.VariableDictionary[param1.Text].AnswerType != Constants.AnswerType.SA && Definitions.VariableDictionary[param1.Text].AnswerType != Constants.AnswerType.N)
                                            {
                                                ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", string.Format(CommonResource.LDEL_VALIDATION_ERROR1, Constants.DP.SubstituteOperatorEQUAL), string.Empty, kvp.Key, Constants.DP.SubstituteParam1Column);

                                                return false;
                                            }
                                            int number;
                                            string paramvalue = param1.Text;
                                            if (!string.IsNullOrEmpty(paramvalue) && (paramvalue).StartsWith("!"))
                                            {
                                                paramvalue = paramvalue.Replace("!", string.Empty);
                                            }
                                            if (!Definitions.VariableDictionary.ContainsKey(param1.Text) && !param1.Text.Equals("*") && !param1.Text.Equals("DK"))
                                            {
                                                // Variable selected

                                                if (!int.TryParse(paramvalue, out number))
                                                {
                                                    showError = true;
                                                    columNumber = param1.Column;
                                                    break;
                                                }
                                                if (number <= 0)
                                                {
                                                    showError = true;
                                                    columNumber = param1.Column;
                                                    break;
                                                }
                                                else if (number > qstnDet.CategoryCount)
                                                {
                                                    showError = true;
                                                    columNumber = param1.Column;
                                                    break;
                                                }
                                            }
                                            else if (Definitions.VariableDictionary.ContainsKey(param1.Text))
                                            {
                                                try
                                                {
                                                    if (Definitions.VariableDictionary.ContainsKey(param1.Text) && Definitions.VariableDictionary[param1.Text].AnswerType != Constants.AnswerType.SA)
                                                    {
                                                        ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", string.Format(CommonResource.LDEL_VALIDATION_ERROR1, Constants.DP.SubstituteOperatorEQUAL), string.Empty, kvp.Key, Constants.DP.SubstituteParam1Column);
                                                        return false;
                                                        // showError = true;
                                                        // break;
                                                    }
                                                }
                                                catch { }
                                            }
                                        }
                                        else if (qstnDet.AnswerType == Constants.AnswerType.FA)
                                        { /*
                                         For FA only FA type variable can set as source variable*/
                                            //if (Definitions.VariableDictionary.ContainsKey(param1.Text) && Definitions.VariableDictionary[param1.Text].AnswerType != Constants.AnswerType.FA)
                                            //{
                                            //    ShowAlert(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW + " " + kvp.Key.ToString(), string.Format(CommonResource.LDEL_VALIDATION_ERROR1, Constants.DP.SubstituteOperatorEQUAL), string.Empty);

                                            //    return false;
                                            //}
                                        }
                                        else if (qstnDet.AnswerType == Constants.AnswerType.N)
                                        { /*For N only SA/N type variable can set as source variable*/
                                            if (Definitions.VariableDictionary.ContainsKey(param1.Text) && Definitions.VariableDictionary[param1.Text].AnswerType != Constants.AnswerType.SA && Definitions.VariableDictionary[param1.Text].AnswerType != Constants.AnswerType.N)
                                            {
                                                ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", string.Format(CommonResource.LDEL_VALIDATION_ERROR1, Constants.DP.SubstituteOperatorEQUAL), string.Empty, kvp.Key, Constants.DP.SubstituteParam1Column);

                                                return false;
                                            }
                                            var regex = new Regex(@"(^-?\d+\.\d+$)|(^\d+\.\d+$)|(^\d+$)|(^-?\d+$)");//^-?[0-9][0-9,\.]+$    //"^-?\\d*(\\.\\d+)?$"
                                            //int number;
                                            if (!Definitions.VariableDictionary.ContainsKey(param1.Text))
                                            {
                                                if (param1.Text != "*" && param1.Text != "DK")
                                                {
                                                    if (!regex.Match(param1.Text).Success)
                                                    {
                                                        ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", string.Format(CommonResource.LDEL_VALIDATION_ERROR1, Constants.DP.SubstituteOperatorEQUAL), string.Empty, kvp.Key, Constants.DP.SubstituteParam1Column);
                                                        return false;
                                                    }
                                                }
                                                //if (param1.Text != "*" && param1.Text != "DK")
                                                //{
                                                //    if (!int.TryParse(param1.Text, out number))
                                                //    {
                                                //        ShowAlert(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW + " " + kvp.Key.ToString(), string.Format(CommonResource.LDEL_VALIDATION_ERROR1, Constants.DP.SubstituteOperatorEQUAL), string.Empty);
                                                //        // MessageDialog.ErrorOk("Input numeric value or select a value from the list");                                                      
                                                //        return false;
                                                //    }
                                                //}
                                            }
                                        }



                                    }
                                    else
                                    {
                                        showError = true;
                                        columNumber = Constants.DP.SubstituteVariableColumn;
                                        break;
                                    }
                                }
                                break;
                            case Constants.DP.SubstituteOperatorCLASS:
                                if (string.IsNullOrEmpty(param1.Text) || string.IsNullOrEmpty(param2.Text) || string.IsNullOrEmpty(param3.Text) || string.IsNullOrEmpty(param4.Text))// == null || param1.Value == "")
                                {
                                    showError = true;
                                    //kvp.Key, Constants.DP.SubstituteParam1Column
                                    columNumber = param1.Column;
                                    break;

                                }
                                if (!string.IsNullOrEmpty(param1.Text) && !Definitions.VariableDictionary.ContainsKey(param1.Text) && Definitions.VariableDictionary[targetvariable.Text].AnswerType != Constants.AnswerType.FA)//variable in question settings or not?
                                {
                                    ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", string.Format(CommonResource.ERR_MSG_VARIABLE_NOT_SET_IN_QUESTION_SETTING, param1.Text), "", kvp.Key, Constants.DP.SubstituteParam1Column);
                                    return false;
                                }


                                if (Definitions.VariableDictionary.ContainsKey(targetvariable.Value))
                                {
                                    Excel.Range endParam = ExcelUtilForAddIn.EndxlRight(param2);
                                    if (endParam.Column > Definitions.VariableDictionary[targetvariable.Text].CategoryCount + Constants.DP.SubstituteParam3Column)
                                    {
                                        showError = true;
                                        break;

                                    }
                                    for (int i = 1; i <= Definitions.VariableDictionary[targetvariable.Text].CategoryCount; i++)
                                    {
                                        Excel.Range paramNcell = Sheet.Cells[kvp.Key, Constants.DP.SubstituteParam3Column + i];
                                        if (!string.IsNullOrEmpty(paramNcell.Text))
                                        {
                                            string message = string.Empty;
                                            if (!(paramNcell.Text.Contains("/") || paramNcell.Text.Contains(",")))
                                            {
                                                // ValidateNumericCell(changedCell, 0, Int32.MaxValue);
                                                message = ValidateNumericCellConent(paramNcell.Text, 0, double.MaxValue);
                                                if (!string.IsNullOrEmpty(message))
                                                {
                                                    if (message != "")
                                                    {
                                                        message = ValidateParenthesisNumericValues(paramNcell.Text);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                message = CommonResource.VALIDATION_CLASS_MSG;
                                                ErrorMsg = message;
                                                paramNcell.Font.Color = Constants.Color.Red;

                                            }



                                            if (!string.IsNullOrEmpty(message))
                                            {
                                                showError = true;
                                                columNumber = paramNcell.Column;
                                                break;
                                            }

                                            ///https://app.gluemodel.com/#/project/task/4295061478
                                            if (!ValidateCLASSNtypeCriteria(paramNcell, false, paramNcell.Column, true))
                                            {
                                                showError = true;
                                                columNumber = paramNcell.Column;
                                                break;
                                            }
                                            ///

                                            //if (!ValidateNumericCell(paramNcell, 0, Int32.MaxValue, false))
                                            //{
                                            //    // param2.Font.Color = Constants.Color.Red;
                                            //    showError = true;
                                            //    break;
                                            //}
                                        }
                                    }
                                }
                                break;

                            case Constants.DP.SubstituteOperatorCOUNT:
                                List<string> countparamsValues = new List<string>();
                                if (!string.IsNullOrEmpty(param1.Text) && !Definitions.VariableDictionary.ContainsKey(param1.Text))//variable in question settings or not?
                                {
                                    ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", string.Format(CommonResource.ERR_MSG_VARIABLE_NOT_SET_IN_QUESTION_SETTING, param1.Text), "", kvp.Key, Constants.DP.SubstituteParam1Column);
                                    return false;
                                }
                                if (Definitions.VariableDictionary.ContainsKey(targetvariable.Value))
                                {
                                    QuestionSettings qs = Definitions.VariableDictionary[targetvariable.Value];

                                    if (string.IsNullOrEmpty(param1.Text) || string.IsNullOrEmpty(param2.Text))// == null || param1.Value == "")
                                    {
                                        showError = true;
                                        columNumber = string.IsNullOrEmpty(param1.Text) ? param1.Column : param2.Column;
                                        break;
                                    }

                                    if (Definitions.VariableDictionary[targetvariable.Value].AnswerType == Constants.AnswerType.SA)
                                    {
                                        List<string> paramValues = new List<string>();
                                        for (int i = 0; i < qs.CategoryCount; i++)
                                        {
                                            Excel.Range paramN = Sheet.Cells[kvp.Key, Constants.DP.SubstituteParam3Column + i];
                                            if (!string.IsNullOrEmpty(paramN.Text))
                                            {
                                                if (Definitions.VariableDictionary.ContainsKey(param1.Text))
                                                {
                                                    showError = !ValidateNumericCell(paramN, 0, Definitions.VariableDictionary[param1.Text].CategoryCount, false);
                                                }
                                                else
                                                {
                                                    showError = true;
                                                }
                                                paramValues.Add(paramN.Text);
                                            }
                                            //---------------------//
                                            else
                                            {
                                                if (i == 0) showError = true;
                                            }
                                            //----------------------//
                                            if (showError)
                                            {
                                                columNumber = paramN.Column;
                                                break;
                                            }
                                        }
                                        countparamsValues = paramValues;
                                        bool anyDuplicate = paramValues.GroupBy(x => x).Any(g => g.Count() > 1);
                                        if (anyDuplicate)
                                        {
                                            showError = true;
                                        }

                                    }
                                }
                                else
                                {
                                    ErrorMsg = CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_INVALID_VARIABLE;
                                    showError = true;
                                    columNumber = targetvariable.Column;

                                }
                                if (IsOverlap(countparamsValues, Definitions.VariableDictionary[param1.Text].CategoryCount))//Redmine id -171477
                                {
                                    ErrorMsg = string.Format(CommonResource.ERR_MSG_COUNT_PARAM_OVERLAP, Constants.DP.SubstituteOperatorCOUNT);
                                    showError = true;
                                    columNumber = param1.Column;
                                }
                                break;
                            case Constants.DP.SubstituteOperatorMTOS:
                                if (string.IsNullOrEmpty(param1.Text) || string.IsNullOrEmpty(param2.Text))
                                {
                                    showError = true;
                                    columNumber = param1.Column;
                                }
                                if (!string.IsNullOrEmpty(param1.Text) && !Definitions.VariableDictionary.ContainsKey(param1.Text))//variable in question settings or not?
                                {
                                    ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", string.Format(CommonResource.ERR_MSG_VARIABLE_NOT_SET_IN_QUESTION_SETTING, param1.Text), "", kvp.Key, param1.Column);
                                    return false;
                                }
                                if (!string.IsNullOrEmpty(param1.Text) && Definitions.VariableDictionary.ContainsKey(param1.Text) && Definitions.VariableDictionary[param1.Text].AnswerType != Constants.AnswerType.MA)//variable in question settings or not?
                                {
                                    ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", string.Format(CommonResource.LDEL_VALIDATION_ERROR1, Constants.DP.SubstituteOperatorMTOS), string.Empty, kvp.Key, param1.Column);
                                    return false;
                                }
                                //Redmine id: 174564
                                if (string.IsNullOrEmpty(Convert.ToString(param2.Text)) || (!string.Equals(Convert.ToString(param2.Text), "1") && !string.Equals(Convert.ToString(param2.Text), "2")))
                                {
                                    //MTOS_PM2_ERRORMESSAGE
                                    ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", string.Format(CommonResource.LDEL_VALIDATION_ERROR1, Constants.DP.SubstituteOperatorMTOS), CommonResource.MTOS_PM2_ERRORMESSAGE, kvp.Key, param2.Column);
                                    return false;
                                }
                                break;
                            case Constants.DP.SubstituteOperatorADD3:
                                if (string.IsNullOrEmpty(param1.Text) || string.IsNullOrEmpty(param2.Text))// == null || param1.Value == "")
                                {
                                    showError = true;
                                    columNumber = param2.Column;
                                }
                                Excel.Range add3param1 = DataProcess.Sheet.Cells[kvp.Key, Constants.DP.SubstituteParam2Column];
                                Excel.Range endcelladd3 = ExcelUtilForAddIn.EndxlRight(add3param1);
                                Excel.Range add3paramrange = DataProcess.Sheet.Range[add3param1, endcelladd3];
                                if (add3paramrange.Columns.Count > 1)
                                {
                                    object[,] add3paramsarray = add3paramrange.Value;
                                    for (int j = 2; j <= add3paramsarray.GetLength(1); j++)
                                    {

                                        if (!string.IsNullOrEmpty(Convert.ToString(add3paramsarray[1, j])) && !Definitions.VariableDictionary.ContainsKey(Convert.ToString(add3paramsarray[1, j])))//variable in question settings or not?
                                        {
                                            ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", string.Format(CommonResource.ERR_MSG_VARIABLE_NOT_SET_IN_QUESTION_SETTING, Convert.ToString(add3paramsarray[1, j])), "", kvp.Key, j + param2.Column - 1);
                                            return false;
                                        }
                                        if (!string.IsNullOrEmpty(Convert.ToString(add3paramsarray[1, j])) && Definitions.VariableDictionary.ContainsKey(Convert.ToString(add3paramsarray[1, j]))
                                           && (Definitions.VariableDictionary[Convert.ToString(add3paramsarray[1, j])].AnswerType == Constants.AnswerType.N ||
                                           Definitions.VariableDictionary[Convert.ToString(add3paramsarray[1, j])].AnswerType == Constants.AnswerType.FA))
                                        {
                                            ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", string.Format(CommonResource.LDEL_VALIDATION_ERROR1, Constants.DP.SubstituteOperatorADD3), string.Empty, kvp.Key, j + param2.Column - 1);

                                            return false;
                                        }
                                        if (string.IsNullOrEmpty(Convert.ToString(add3paramsarray[1, j])))
                                        {
                                            showError = true;
                                            columNumber = param2.Column+j-1;
                                        }
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(add3paramrange.Text) && !Definitions.VariableDictionary.ContainsKey(add3paramrange.Text))//variable in question settings or not?
                                    {
                                        ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", string.Format(CommonResource.ERR_MSG_VARIABLE_NOT_SET_IN_QUESTION_SETTING, add3paramrange.Text), "", kvp.Key, Constants.DP.SubstituteVariableColumn);
                                        return false;
                                    }
                                    if (!string.IsNullOrEmpty(Convert.ToString(add3paramrange.Text)) && Definitions.VariableDictionary.ContainsKey(Convert.ToString(add3paramrange.Text))
                                           && (Definitions.VariableDictionary[Convert.ToString(add3paramrange.Text)].AnswerType == Constants.AnswerType.N ||
                                           Definitions.VariableDictionary[Convert.ToString(add3paramrange.Text)].AnswerType == Constants.AnswerType.FA))
                                    {
                                        ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", string.Format(CommonResource.LDEL_VALIDATION_ERROR1, Constants.DP.SubstituteOperatorADD3), string.Empty, kvp.Key, Constants.DP.SubstituteVariableColumn);

                                        return false;
                                    }
                                }
                                /* if (!string.IsNullOrEmpty(param1.Text) && !Definitions.VariableDictionary.ContainsKey(param1.Text) && Definitions.VariableDictionary[targetvariable.Text].AnswerType != Constants.AnswerType.FA)//variable in question settings or not?
                                {
                                    ShowAlert(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW + " " + kvp.Key.ToString(), string.Format(CommonResource.ERR_MSG_VARIABLE_NOT_SET_IN_QUESTION_SETTING, param1.Text), "");
                                    return false;
                                }*/
                                break;
                            case Constants.DP.SubstituteOperatorINTEGRATE:
                                if (string.IsNullOrEmpty(param1.Text) || string.IsNullOrEmpty(param2.Text))// == null || param1.Value == "")
                                {
                                    showError = true;
                                    columNumber = string.IsNullOrEmpty(param1.Text) ? param1.Column : param2.Column;
                                    break;
                                }
                                int param2integer = 0;
                                if (int.TryParse(param2.Text, out param2integer))
                                {
                                    //if(param2integer)
                                    //for(int i=0;i<param2integer;i++)
                                    //{
                                    //    Excel.Range variableparam = Sheet.Cells[kvp.Key, Constants.DP.SubstituteParam3Column + (i * 2)];
                                    //    if(!Definitions.VariableDictionary.ContainsKey(variableparam.Text))
                                    //    {
                                    //        showError = true;
                                    //        break;
                                    //    }

                                    //}
                                    Excel.Range endParam = ExcelUtilForAddIn.EndxlRight(param2);
                                    int totalvariableparams = Constants.DP.SubstituteParam3Column + (param2integer * 2) - 1;
                                    Excel.Range variablecell = Sheet.Cells[kvp.Key, Constants.DP.SubstituteVariableColumn]; //added by anu
                                    for (int i = totalvariableparams; i < totalvariableparams + Definitions.VariableDictionary[variablecell.Text].CategoryCount; i++) // modified by anu
                                    {
                                        string cellval = Sheet.Cells[kvp.Key, i].Text;
                                        int count = cellval.Count(f => f == ';');
                                        if (count != (param2integer - 1))
                                        {
                                            ErrorMsg = CommonResource.ERR_MSG_INTEGRATE_PARAM_VALUE_COMBINATIONS;
                                            showError = true;
                                            columNumber = i;
                                            break; ;
                                        }
                                        //--------validation for 1st param value(combining criteria)-------//
                                        //int valCount = cellval.Count(v => v != ';');
                                        //if (i == totalvariableparams && (valCount == 0 || string.IsNullOrEmpty(cellval)))
                                        //{
                                        //    showError = true;
                                        //    columNumber = i;
                                        //    break;
                                        //}
                                        //----------------------------------//
                                    }

                                    for (int i = Constants.DP.SubstituteParam3Column; i < totalvariableparams; i++)
                                    {
                                        if (string.IsNullOrEmpty(Sheet.Cells[kvp.Key, i].Text))
                                        {
                                            showError = true;
                                            columNumber = i;// Constants.DP.SubstituteParam3Column;
                                            break;
                                        }
                                    }

                                    if (!showError)
                                    {
                                        // Excel.Range variablecell = Sheet.Cells[kvp.Key, Constants.DP.SubstituteVariableColumn];
                                        //if(endParam.Column >= totalvariableparams + Definitions.VariableDictionary[variablecell.Text].CategoryCount)
                                        //{
                                        //    showError = true;
                                        //    break;
                                        //}
                                        for (int i = totalvariableparams; i < totalvariableparams + Definitions.VariableDictionary[variablecell.Text].CategoryCount; i++)
                                        {
                                            Excel.Range paramcell = Sheet.Cells[kvp.Key, i];
                                            string content = paramcell.Text;
                                            string[] values = content.Split(';');
                                            for (int ii = 0; ii < values.Length; ii++)
                                            {
                                                Excel.Range variableparam = Sheet.Cells[kvp.Key, Constants.DP.SubstituteParam3Column + ii * 2];
                                                if (string.IsNullOrEmpty(variableparam.Text))
                                                {
                                                    showError = true;
                                                    columNumber = variableparam.Column;
                                                    break;
                                                }

                                                if (!string.IsNullOrEmpty(values[ii]))
                                                {
                                                    if (!Definitions.VariableDictionary.ContainsKey(variableparam.Text))
                                                    {
                                                        showError = true;
                                                        columNumber = variableparam.Column;
                                                        break;

                                                    }
                                                    if (values[ii].StartsWith("<>") || values[ii].StartsWith("!"))
                                                    {
                                                        values[ii] = values[ii].Replace("<>", "");
                                                        values[ii] = values[ii].Replace("!", "");
                                                    }
                                                    string message = ValidateNumericCellConent(values[ii], Definitions.VariableDictionary[variableparam.Text].AnswerType == "N" ? 0 : 1, Definitions.VariableDictionary[variableparam.Text].AnswerType == "N" ? Int32.MaxValue : Definitions.VariableDictionary[variableparam.Text].CategoryCount);
                                                    if (!string.IsNullOrEmpty(message) && Definitions.VariableDictionary[variableparam.Text].AnswerType == "N")
                                                    {
                                                        message = ValidateParenthesisNumericValues(values[ii]);
                                                    }

                                                    if (!string.IsNullOrEmpty(message))
                                                    {
                                                        showError = true;
                                                        columNumber = variableparam.Column;
                                                        break;
                                                    }
                                                }
                                            }
                                            if (showError) break;
                                        }
                                        for (int i = totalvariableparams + Definitions.VariableDictionary[variablecell.Text].CategoryCount; i < endParam.Column; i++)
                                        {
                                            Excel.Range paramcell = Sheet.Cells[kvp.Key, i];
                                            string contents = paramcell.Text;
                                            contents = contents.Replace(";", "");
                                            if (contents.Length > 0)
                                            {
                                                showError = true;
                                                columNumber = paramcell.Column;
                                                break;

                                            }
                                            if (showError) break;
                                        }
                                    }
                                }
                                break;

                            case Constants.DP.SubstituteOperatorJOINT:
                                if (string.IsNullOrEmpty(param1.Text) || string.IsNullOrEmpty(param2.Text))// == null || param1.Value == "")
                                {
                                    showError = true;
                                    ErrorMsg = CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_JOINT_VALUE_MISSING;
                                    columNumber = string.IsNullOrEmpty(param1.Text) ? param1.Column : param2.Column;
                                    break;
                                }

                                //joint parameter cheking here
                                ErrorMsg = JointParamCheck(kvp.Key, out showError);

                                break;
                                //case Constants.DP.SubstituteOperatoro:
                                //    break;
                        }
                        break;
                    case Constants.DP.InstructionDECST:
                        if (string.IsNullOrEmpty(param1.Text) || string.IsNullOrEmpty(param2.Text))
                        {
                            showError = true;
                            columNumber = string.IsNullOrEmpty(param1.Text) ? param1.Column : param2.Column;
                            break;
                        }
                        break;
                    case Constants.DP.InstructionCALL:
                        if (string.IsNullOrEmpty(param1.Text) || string.IsNullOrEmpty(param2.Text))
                        {
                            showError = true;
                            columNumber = string.IsNullOrEmpty(param1.Text) ? param1.Column : param2.Column;
                            break;
                        }
                        if (!DataProcess.decst_ProgramList.ContainsKey(param1.Text))
                        {
                            ErrorMsg = CommonResource.ERR_CALL_METHODNOTFOUND;
                            columNumber = param1.Column;
                            showError = true;

                        }
                        break;
                    case Constants.DP.InstructionFOR:

                        int param1value = 0, param2value = 0, param3value = 0;
                        if (string.IsNullOrEmpty(param1.Text) || string.IsNullOrEmpty(param2.Text) || string.IsNullOrEmpty(param3.Text))
                        {
                            showError = true;
                            columNumber = string.IsNullOrEmpty(param1.Text) ? param1.Column : string.IsNullOrEmpty(param1.Text) ? param2.Column : param3.Column;
                            break;
                        }
                        else if (!int.TryParse(param1.Text, out param1value) || !int.TryParse(param2.Text, out param2value) || !int.TryParse(param3.Text, out param3value))
                        {
                            showError = true;
                            columNumber = Constants.DP.SubstituteVariableColumn;
                            break;
                        }


                        //param1value = int.Parse(param1.Text);
                        //param2value = int.Parse(param2.Text);
                        //param3value = int.Parse(param3.Text);
                        if (param3value <= 0 && param1value < param2value)
                        {
                            ErrorMsg = CommonResource.FOR_NEXT_PARAM3_INVALID;// "Increment / Decrement param not proper";
                            columNumber = Constants.DP.SubstituteVariableColumn;
                            showError = true;
                            break;
                        }
                        else if (param3value >= 0 && param1value > param2value)
                        {
                            ErrorMsg = CommonResource.FOR_NEXT_PARAM3_INVALID;// "Increment / Decrement param not proper";
                            showError = true;
                            columNumber = Constants.DP.SubstituteVariableColumn;
                            break;
                        }
                        break;
                    case Constants.DP.InstructionOMIT:
                    case Constants.DP.InstructionOMIT2:
                    case Constants.DP.InstructionLISTUP:
                        if (kvp.Value.instruction == Constants.DP.InstructionOMIT2)
                        {
                            Excel.Range startomit2 = DataProcess.Sheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column];
                            Excel.Range endomit2 = DataProcess.Sheet.Cells[kvp.Key, Constants.DP.SubstituteParam2Column];
                            if (string.IsNullOrEmpty(startomit2.Text) || string.IsNullOrEmpty(endomit2.Text))
                            {
                                ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", string.Format(CommonResource.LDEL_VALIDATION_ERROR1, Sheet.Cells[kvp.Key, Constants.DP.InstructionColumn].Text), "", kvp.Key, Constants.DP.SubstituteVariableColumn);//SubstituteParam1Column
                                return false;
                            }
                            if (!Definitions.VariableDictionary.ContainsKey(startomit2.Text))
                            {
                                ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", string.Format(CommonResource.ERR_MSG_VARIABLE_NOT_SET_IN_QUESTION_SETTING, startomit2.Text), string.Empty, kvp.Key, Constants.DP.SubstituteParam1Column);
                                return false;
                            }
                            if (!Definitions.VariableDictionary.ContainsKey(endomit2.Text))
                            {
                                ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", string.Format(CommonResource.ERR_MSG_VARIABLE_NOT_SET_IN_QUESTION_SETTING, endomit2.Text), string.Empty, kvp.Key, Constants.DP.SubstituteParam2Column);
                                return false;
                            }
                        }
                        else
                        {
                            Excel.Range listupparamcell = DataProcess.Sheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column];
                            Excel.Range listupendcell = ExcelUtilForAddIn.EndxlRight(listupparamcell);
                            Excel.Range variablerange = DataProcess.Sheet.Range[listupparamcell, listupendcell];

                            foreach (Excel.Range cell in variablerange.Cells)
                            {
                                try
                                {
                                    if (string.IsNullOrEmpty(cell.Text) || !Definitions.VariableDictionary.ContainsKey(cell.Text))
                                    {
                                        ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", string.Format(CommonResource.ERR_MSG_VARIABLE_NOT_SET_IN_QUESTION_SETTING, cell.Text), string.Empty, kvp.Key, cell.Column);
                                        return false;
                                    }
                                }
                                catch { }
                            }
                        }

                        if (kvp.Value.instruction == Constants.DP.InstructionLISTUP)
                        {
                            Logic.DataProcessExecute.islistup = true;//for listup
                        }

                        break;
                    case Constants.DP.InstructionLDEL:
                        try
                        {
                            LDel ldel = new LDel(ExcelUtilForAddIn.GetWorkSheetByCodeName(currworkbook,Constants.SheetCodeName.LDEL));
                            ldel.OnLDELCellChanged(null);
                        }
                        catch { }
                        if (Definitions.optionList == null)
                        {
                            ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", string.Format(CommonResource.LDEL_VALIDATION_ERROR1, Sheet.Cells[kvp.Key, Constants.DP.InstructionColumn].Text), "", kvp.Key, Constants.DP.SubstituteVariableColumn);//SubstituteParam1Column
                            return false;
                        }

                        Excel.Range ldelparamcell = DataProcess.Sheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column];
                        if (string.IsNullOrEmpty(ldelparamcell.Text) || !Definitions.optionList.Contains(ldelparamcell.Text))
                        {
                            ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", string.Format(CommonResource.LDEL_VALIDATION_ERROR1, Sheet.Cells[kvp.Key, Constants.DP.InstructionColumn].Text), "", kvp.Key, Constants.DP.SubstituteParam1Column);//SubstituteParam1Column
                            return false;
                        }
                        break;
                }
                if (showError)
                {
                    ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", ErrorMsg, string.Empty, kvp.Key, columNumber > 0 ? columNumber : Constants.DP.SubstituteVariableColumn);//ShowAlert(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW + " " + kvp.Key.ToString(), ErrorMsg, string.Empty);//ShowAlert(string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, kvp.Key.ToString()) + " ", ErrorMsg, string.Empty);
                    return false;
                }
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine("StackTrace:{0}", ex.StackTrace); return false; }
            return retval;
        }

        public static bool IsContainDigits(string str)
        {
            return !string.IsNullOrEmpty(str) && str.Any(char.IsDigit);
        }

        public static bool IsWholeDigits(string str)
        {
            return !string.IsNullOrEmpty(str) && str.All(char.IsDigit);
        }

        private void ShowAlert(string row, string variable, string pOperator, int rowNumber = 0, int columnNumber = 0)
        {
            string message = row + "\n" + variable + pOperator;
            MessageDialog.ErrorOk(message);
            if (rowNumber > 1 && columnNumber > 1)
            {
                Sheet.Application.ScreenUpdating = true;
                Sheet.Cells[rowNumber, columnNumber].Select();
            }
        }

        private bool CheckLimitExceed(int noofchoices, List<string> paramvalues)
        {

            List<string> commasep;
            List<string> barsep;
            List<string> minsep = new List<string>();
            foreach (string paramstr in paramvalues)
            {
                commasep = new List<string>();
                barsep = new List<string>();

                //split with ','
                string[] criteriacommavalues = paramstr.Split(',');
                foreach (string str in criteriacommavalues)
                {
                    commasep.Add(str);//add whole to  list
                }
                // for each nd split with '/'
                foreach (string str in commasep)
                {
                    if (str.Contains('/'))
                    {
                        string[] criteriabarvalues = str.Split('/');
                        foreach (string s in criteriabarvalues)
                        {
                            barsep.Add(s);//add whole to list
                        }
                    }
                    else
                        barsep.Add(str);
                }

                //chek for '-'
                foreach (string str in barsep)
                {
                    if (str.Contains('-'))
                    {
                        int start = 0, limit = 0;
                        string[] criteriaminvalues = str.Split('-');
                        {

                            try
                            {

                                if (criteriaminvalues.Length == 1)
                                {
                                    try
                                    {
                                        start = Convert.ToInt32(criteriaminvalues[0]);
                                    }
                                    catch (Exception e) { start = 1; System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace); }
                                    limit = start;
                                }
                                else
                                {
                                    try
                                    {
                                        start = Convert.ToInt32(criteriaminvalues[0]);
                                    }
                                    catch (Exception e) { start = 1; System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace); }//actually get min value of answer
                                    try
                                    {
                                        limit = Convert.ToInt32(criteriaminvalues[1]);
                                    }
                                    catch (Exception e) { System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace); }//actually get max value of answer;need to get max of choice no from item id and set limit
                                }
                                if (limit < start)//need to reverse if 9-7 comes
                                {
                                    int temp = limit;
                                    limit = start;
                                    start = temp;
                                }
                            }
                            catch { }

                            for (int ci = start; ci <= limit; ci++)
                            {
                                minsep.Add(ci.ToString());//add whole to list
                            }
                        }
                    }
                    else
                        minsep.Add(str);
                }
            }

            if (minsep.Count > noofchoices)
                return false;
            else return true;
        }
        private string JointParamCheck(int row, out bool showError)//KeyValuePair<int, Crit_Inst_Operator> kvp
        {
            string ErrorMsg = "";
            showError = false;
            Excel.Range jointparam1 = DataProcess.Sheet.Cells[row, Constants.DP.SubstituteParam1Column];//kvp.Key
            Excel.Range endcelljoint = ExcelUtilForAddIn.EndxlRight(jointparam1);
            Excel.Range jointparamrange = DataProcess.Sheet.Range[jointparam1, endcelljoint];

            object[,] jointparamsarray = jointparamrange.Value;
            List<string> paramvalues = new List<string>();
            int noofchoices = 0;

            //jointparamsarray.GetLength(1)
            if (jointparamsarray.GetLength(1) % 2 != 0)
            {
                showError = true;
                return ErrorMsg = CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_JOINT_VALUE_MISSING;// // break;
            }
            noofchoices = (Definitions.VariableDictionary[Sheet.Cells[row, Constants.DP.SubstituteVariableColumn].Text]).CategoryCount;
            for (int j = 1; j <= jointparamsarray.GetLength(1); j += 2)
            {
                string variable = jointparamsarray[1, j] == null ? string.Empty : Convert.ToString(jointparamsarray[1, j]);
                if (!string.IsNullOrEmpty(variable))
                {
                    if (jointparamsarray[1, j + 1] == null)
                    {
                        ErrorMsg = CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_JOINT_VALUE_MISSING;
                        showError = true;
                        return ErrorMsg;// break;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Sheet.Cells[row, Constants.DP.SubstituteParam1Column + j - 1].Text) && !Definitions.VariableDictionary.ContainsKey(Sheet.Cells[row, Constants.DP.SubstituteParam1Column + j - 1].Text))//variable in question settings or not?
                        {


                            ErrorMsg = string.Format(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_ROW_LINE_NUMBER, row.ToString()) + " " + string.Format(CommonResource.ERR_MSG_VARIABLE_NOT_SET_IN_QUESTION_SETTING, Sheet.Cells[row, Constants.DP.SubstituteParam1Column + j - 1].Text);
                            showError = true;

                        }

                        if (Definitions.VariableDictionary.ContainsKey(Sheet.Cells[row, Constants.DP.SubstituteParam1Column + j - 1].Text))
                        {
                            int paramchoicecount = Definitions.VariableDictionary[Sheet.Cells[row, Constants.DP.SubstituteParam1Column + j - 1].Text].CategoryCount;
                            if (!ValidateNumericCell(DataProcess.Sheet.Cells[row, Constants.DP.SubstituteParam1Column + j], 1, paramchoicecount, false))
                            {
                                showError = true;
                                return ErrorMsg;// break;
                            }
                        }
                    }
                    paramvalues.Add(Convert.ToString(jointparamsarray[1, j + 1]));
                }
            }
            if (!CheckLimitExceed(noofchoices, paramvalues) && !showError)
            {
                //ShowAlert(CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_JOINT,"", "");
                ErrorMsg = CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_JOINT;// CommonResource.ERR_MSG_DATAPROCESS_VALIDATION_ERROR_JOINT_VALUE_MISSING;
                showError = true;

            }
            return ErrorMsg;
        }

        public static void DpInsertDel(Excel.Worksheet sheet, bool deleteMode = false)
        {
            Excel.Application application = sheet.Application;

            if (!(application.Selection is Excel.Range))
            {
                return;
            }

            Excel.Range range = application.Selection;
            Excel.Worksheet targetSheet = range.Worksheet;
            if (range.Areas.Count > 1)
            {
                MessageDialog.ErrorOk(CommonResource.CANNOT_SPECIFY_MULTIPLE_RANGE);
                return;
            }

            if (range.Rows.Count == ExcelUtilForAddIn.EndRow(targetSheet) && range.Columns.Count == ExcelUtilForAddIn.EndColumn(targetSheet))
            {
                return;
            }

            AppHelper appHelper = new AppHelper();
            appHelper.ExcelSet(currworkbook);

            try
            {
                string targetAddress = "";
                if (deleteMode == false)
                {
                    targetAddress = range.Address;
                }


                if (range.Rows.Count == ExcelUtilForAddIn.EndRow(targetSheet))
                {

                    if (deleteMode == false)
                    {
                        int offsetCount = range.Columns.Count;
                        if (CmRowColInsert(targetSheet, Excel.XlRowCol.xlColumns, range.Column, range.Columns.Count, Qc4CommonConstants.DPDeleteInsertMinRow) == true)
                        {
                            targetSheet.Range[targetAddress].Offset[0, offsetCount].Select();
                        }
                    }
                    else
                    {
                        CmRowColDelete(targetSheet, Excel.XlRowCol.xlColumns, range.Column, range.Columns.Count, Qc4CommonConstants.DPDeleteInsertMinRow);
                    }

                }
                else
                {
                    if (deleteMode == false)
                    {
                        int offsetCount = range.Rows.Count;
                        if (CmRowColInsert(targetSheet, Excel.XlRowCol.xlRows, range.Row, range.Rows.Count, Qc4CommonConstants.DPDeleteInsertMinRow) == true)
                        {
                            CrColumnAssign(targetSheet);
                            targetSheet.Range[targetAddress].Offset[offsetCount, 0].Select();
                        }
                    }
                    else
                    {
                        if (CmRowColDelete(targetSheet, Excel.XlRowCol.xlRows, range.Row, range.Rows.Count, Qc4CommonConstants.DPDeleteInsertMinRow) == true)
                        {
                            CrColumnAssign(targetSheet, true);
                        }
                    }
                }
            }
            finally
            {
                appHelper.ExcelReset(currworkbook);
            }
        }

        private static void CrColumnAssign(Excel.Worksheet targetSheet, bool emptyPermit = false)
        {

            int endRow = ExcelUtilForAddIn.EndxlUp(targetSheet.Cells[1, 2]).Row;
            if (emptyPermit == false)
            {
                endRow = Math.Max(endRow, ExcelUtilForAddIn.EndxlUp(targetSheet.Cells[1, 3]).Row);
            }
            if (endRow < Constants.CR.CRRowInputStart + 1) return;
            dynamic colArray = targetSheet.Range[targetSheet.Cells[Constants.CR.CRRowInputStart + 1, 2], targetSheet.Cells[endRow, 2]].Value2;
            if (colArray.GetType().IsArray)
            {
                for (int i = 1; i <= colArray.GetLength(0); i++)
                {
                    if (!(emptyPermit == true && (null == colArray[i, 1] || string.IsNullOrEmpty(colArray[i, 1].ToString()))))
                    {
                        colArray[i, 1] = i;
                    }
                }
            }
            targetSheet.Range[targetSheet.Cells[Constants.CR.CRRowInputStart + 1, 2], targetSheet.Cells[endRow, 2]].Value2 = colArray;
        }
        private static bool CmRowColDelete(Excel.Worksheet targetSheet, Excel.XlRowCol rowCol, int targetPlace, int deleteNum, int minCount = 0)
        {

            if (targetPlace < minCount && minCount != 0)
            {
                return false;
            }
            if (rowCol == Excel.XlRowCol.xlRows)
            {
                targetSheet.Range[targetSheet.Rows[targetPlace], targetSheet.Rows[targetPlace + deleteNum - 1]].Delete(Shift: Excel.XlDirection.xlUp);
            }
            else
            {
                targetSheet.Range[targetSheet.Columns[targetPlace], targetSheet.Columns[targetPlace + deleteNum - 1]].Delete(Shift: Excel.XlDirection.xlToLeft);

            }
            return true;
        }

        private static bool CmRowColInsert(Excel.Worksheet targetSheet, Excel.XlRowCol rowCol, int targetPlace, int insertNum, int minCount = 0)
        {
            if (targetPlace < minCount && minCount != 0)
            {
                return false;
            }

            if (rowCol == Excel.XlRowCol.xlRows)
            {
                targetSheet.Range[targetSheet.Rows[targetPlace], targetSheet.Rows[targetPlace + insertNum - 1]].Insert(Shift: Excel.XlDirection.xlDown);
                int copyRowCol = Math.Max(targetSheet.Range["A1"].SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Row + 1, minCount);
                targetSheet.Rows[copyRowCol].Copy();
                targetSheet.Range[targetSheet.Rows[targetPlace], targetSheet.Rows[targetPlace + insertNum - 1]].PasteSpecial(Excel.XlPasteType.xlPasteAll);

            }
            else
            {
                targetSheet.Range[targetSheet.Columns[targetPlace], targetSheet.Columns[targetPlace + insertNum - 1]].Insert(Shift: Excel.XlDirection.xlToRight);
                int copyRowCol = Math.Max(targetSheet.Range["A1"].SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Column + 1, minCount);
                targetSheet.Columns[copyRowCol].Copy();
                targetSheet.Range[targetSheet.Columns[targetPlace], targetSheet.Columns[targetPlace + insertNum - 1]].PasteSpecial(Excel.XlPasteType.xlPasteAll);

            }
            targetSheet.Application.CutCopyMode = (Excel.XlCutCopyMode)0;
            return true;
        }

        public static void DP_Up_Down_Row(Excel.Worksheet targetSheet, bool Down_Mode = false)
        {
            Excel.Application application = targetSheet.Application;

            if (!(application.Selection is Excel.Range))
            {
                return;
            }

            Excel.Range selectedcell = application.Selection;
            Excel.Range range = application.Selection.EntireRow;
            // Excel.Worksheet targetSheet = range.Worksheet;
            if (range.Areas.Count > 1)
            {
                MessageDialog.ErrorOk(CommonResource.CANNOT_SPECIFY_MULTIPLE_RANGE);
                return;
            }

            if (range.Rows.Count == ExcelUtilForAddIn.EndRow(targetSheet) && range.Columns.Count == ExcelUtilForAddIn.EndColumn(targetSheet))
            {
                return;
            }

            long Ret_Row = 0;

            if (Down_Mode == true)
            {

                if (range.Row >= Qc4CommonConstants.DPDownMinRow)
                    Ret_Row = FNC_Dp_UpDown_Exec(targetSheet, range, Down_Mode);
            }
            else
            {
                if (range.Row >= Qc4CommonConstants.DPUpMinRow)
                    Ret_Row = FNC_Dp_UpDown_Exec(targetSheet, range, Down_Mode);
            }
            //selectedcell Excel.Range targetrange = Sheet.Cells[selectedcell.Row + Ret_Row, selectedcell.Column];
            selectedcell.Select();

        }


        private static long FNC_Dp_UpDown_Exec(Excel.Worksheet targetSheet, Excel.Range Exec_Range, bool Down_Mode = false)
        {
            long Ret_Row = 0;



            if (Down_Mode == false)
            {
                if (Exec_Range.Row != Qc4CommonConstants.DPDeleteInsertMinRow)
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


        public static void Dp_Copy(Excel.Worksheet targetSheet)
        {
            Excel.Application application = targetSheet.Application;

            if (!(application.Selection is Excel.Range))
            {
                return;
            }

            Excel.Range range = application.Selection.EntireRow;
            // Excel.Worksheet targetSheet = range.Worksheet;
            if (range.Areas.Count > 1)
            {
                MessageDialog.ErrorOk(CommonResource.CANNOT_SPECIFY_MULTIPLE_RANGE);
                return;
            }

            var withBlock = targetSheet;


            range = FNC_Dp_Range_Abstract(targetSheet, range, Qc4CommonConstants.DPDeleteInsertMinRow);


            // if (!string.IsNullOrEmpty(range.Text))
            {
                {
                    Excel.Worksheet withBlock1 = ExcelUtilForAddIn.GetWorksheetByName(currworkbook,"sheet_wk"); // Sheet_Wk ; sheet_wk   ; Work     //GetWorkSheetByCodeName();//
                    //ExcelUtilForAddIn.GetWorkSheetByCodeName(Constants.SheetCodeName.LDEL)

                    withBlock1.Cells.Clear();
                    range.Copy();


                    targetSheet.Application.DisplayAlerts = false;
                    withBlock1.Range["A1"].Value = System.Convert.ToInt64(range.Cells.Count / (double)range.Worksheet.Columns.Count);
                    withBlock1.Range["A2"].PasteSpecial(Excel.XlPasteType.xlPasteAll);
                    targetSheet.Application.DisplayAlerts = true;

                    //  Sheet_Menu.Activate();
                }
            }

        }

        public static void Dp_Paste(Excel.Worksheet targetSheet)
        {
            Excel.Application application = targetSheet.Application;

            if (!(application.Selection is Excel.Range))
            {
                return;
            }

            Excel.Range range = application.Selection.EntireRow;
            // Excel.Worksheet targetSheet = range.Worksheet;
            if (range.Areas.Count > 1)
            {
                MessageDialog.ErrorOk(CommonResource.CANNOT_SPECIFY_MULTIPLE_RANGE);
                return;
            }

            range = FNC_Dp_Range_Abstract(targetSheet, range, Qc4CommonConstants.DPDeleteInsertMinRow);

            var withBlock = targetSheet;
            // if (!string.IsNullOrEmpty(range.Text))
            {

                Excel.Worksheet withBlock1 = ExcelUtilForAddIn.GetWorksheetByName(currworkbook,"Sheet_Wk"); // Sheet_Wk;
                if (!string.IsNullOrEmpty(withBlock1.Range["A1"].Text))
                {
                    targetSheet.Application.DisplayAlerts = false;
                    int rowcount = System.Convert.ToInt32(withBlock1.Range["A1"].Value) + 1;
                    withBlock1.Range["2:" + rowcount.ToString()].EntireRow.Copy();
                    range.PasteSpecial(Excel.XlPasteType.xlPasteAll);
                    targetSheet.Application.DisplayAlerts = true;


                    //  targetSheet.Application.CutCopyMode = false;
                    withBlock1.Cells.Clear();

                    //  Sheet_Menu.Activate();
                }

            }

        }
        private static Excel.Range FNC_Dp_Range_Abstract(Excel.Worksheet Target_Sheet, Excel.Range Target_Range, long First_Row)//Excel.Range Target_Range
        {
            //Excel.Worksheet Target_Sheet;

            //Target_Sheet = Target_Range.Worksheet;

            return Target_Sheet.Application.Intersect(Target_Range, Target_Sheet.Range[First_Row + ":" + ExcelUtilForAddIn.EndRow(Target_Sheet)]);//First_Row, ExcelUtilForAddIn.EndRow(Target_Sheet)// Excel.End_Row(Target_Sheet)


            // Target_Sheet = null;
            // return Target_Sheet;

        }
        private bool ValidateMTOSPARAM(Excel.Range changedCell)
        {
            Excel.Range subvariablerange = Sheet.Cells[changedCell.Row, Constants.DP.SubstituteVariableColumn];
            if (string.IsNullOrEmpty(subvariablerange.Text) || !Definitions.VariableDictionary.ContainsKey(subvariablerange.Text))
            {
                // ExcelUtilForAddIn.SetFontColor(changedCell, Constants.Color.Red);
                return false;
            }
            QuestionSettings qssubvariable = Definitions.VariableDictionary[subvariablerange.Text];
            QuestionSettings qsmavariable = Definitions.VariableDictionary[changedCell.Text];
            if (qssubvariable.CategoryCount < qsmavariable.CategoryCount)
            {
                return false;
            }
            return true;
        }
        private bool IsOverlap(List<string> countparamsValues, int catcount)
        {
            bool isoverlap = false;
            //if    countparamsValues is null then return true

            if (countparamsValues.Count > 1)
            {
                //if "!"  contains return true
                for (int i = 0; i < countparamsValues.Count; i++)
                {
                    string[] val1 = GetCommaSeperated(countparamsValues[i], catcount).Split(',');
                    for (int j = i + 1; j < countparamsValues.Count; j++)
                    {
                        string[] val2 = GetCommaSeperated(countparamsValues[j], catcount).Split(',');
                        foreach (string s in val1)
                        {
                            if (Array.Exists(val2, element => element == s))//(val2.Contains(s))// Array.Exists(array, element => element == "perls")
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            //else if "!"  contains return true

            return isoverlap;

        }
        //    int FindOverlapping(int aStart, int aEnd, int bStart, int bEnd)
        //{
        //    int overlap = System.Linq.Enumerable.Range(aStart, aEnd - aStart).Intersect(System.Linq.Enumerable.Range(bStart, bEnd - bStart)).Count();

        //    return overlap;
        //}
        private String GetCommaSeperated(string value, int catcount)
        {
            string commaseperatedvalues = string.Empty;
            bool isnot = false;
            if (value.StartsWith("!") || value.StartsWith("<>"))
            {
                isnot = true;
            }
            if (value.StartsWith("!")) value = value.TrimStart('!');
            //else if (value.StartsWith("<>")) value = value.Replace("<>", "");//currently <>  omitted
            List<string> commasep = new List<string>();
            List<string> barsep = new List<string>();
            List<string> minsep = new List<string>();
            List<int> exclidelist = new List<int>();
            //split with ','
            string[] criteriacommavalues = value.Split(',');
            foreach (string str in criteriacommavalues)
            {
                commasep.Add(str);//add whole to  list
            }
            // for each nd split with '/'
            foreach (string str in commasep)
            {
                if (str.Contains('/'))
                {
                    string[] criteriabarvalues = str.Split('/');
                    foreach (string s in criteriabarvalues)
                    {
                        barsep.Add(s);//add whole to list
                    }
                }
                else
                    barsep.Add(str);
            }

            foreach (string str in barsep)
            {
                if (isnot)//str.StartsWith("!") || str.StartsWith("<>")
                {
                    string notvalue = str;
                    //need to remove the items from list and add other category numbers
                    // criteriaValueDescription = criteriaValueDescription.TrimStart('!');
                    if (str.StartsWith("!")) notvalue = str.TrimStart('!');
                    else if (str.StartsWith("<>")) notvalue = str.Replace("<>", "");
                    //criteriaValueDescription = criteriaValueDescription.Replace("<>", "");//TrimStart('<>');

                    int criteriaend = catcount;// Definitions.VariableDictionary[quesvar].CategoryCount;
                    if (str.Contains('-'))
                    {
                        int strt = 0, end = 0;
                        string[] criterisplitvals = notvalue.Split('-');

                        if (criterisplitvals.Length == 1)
                        {
                            try
                            {
                                strt = Convert.ToInt32(criterisplitvals[0]);
                            }
                            catch (Exception e) { strt = 1; System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace); }
                            end = strt;

                        }
                        else
                        {
                            try
                            {
                                strt = Convert.ToInt32(criterisplitvals[0]);
                            }
                            catch (Exception e) { strt = 1; System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace); }
                            try
                            {
                                end = Convert.ToInt32(criterisplitvals[1]);
                            }
                            catch (Exception e)
                            {
                                end = catcount;// Definitions.VariableDictionary[quesvar].CategoryCount;
                                System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                            }
                        }

                        for (int ci = strt; ci <= end; ci++)
                        {
                            exclidelist.Add(ci);
                        }
                        //for (int ci = 1; ci <= Definitions.VariableDictionary[quesvar].CategoryCount; ci++)
                        //{
                        //    if (!exclidelist.Contains(ci))
                        //        minsep.Add(ci.ToString());
                        //}
                    }
                    else
                    {
                        try
                        {
                            exclidelist.Add(Convert.ToInt32(str));
                        }
                        catch { }
                    }


                }
                else
                {
                    //else
                    if (str.Contains('-'))
                    {

                        int start = 0, limit = 0;
                        string[] criteriaminvalues = str.Split('-');
                        // foreach (string s in criteriaminvalues)
                        {

                            try
                            {

                                if (criteriaminvalues.Length == 1)
                                {
                                    try
                                    {
                                        start = Convert.ToInt32(criteriaminvalues[0]);
                                    }
                                    catch (Exception e) { start = 1; System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace); }
                                    limit = start;
                                }
                                else
                                {
                                    try
                                    {
                                        start = Convert.ToInt32(criteriaminvalues[0]);
                                    }
                                    catch (Exception e) { start = 1; System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace); }//actually get min value of answer
                                    try
                                    {
                                        limit = Convert.ToInt32(criteriaminvalues[1]);
                                        if (limit == 0) start = 0;
                                    }
                                    catch (Exception e)
                                    {//actually get max value of answer;need to get max of choice no from item id and set limit
                                        limit = catcount;// Definitions.VariableDictionary[quesvar].CategoryCount;
                                        System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                                    }
                                }
                                if (limit < start)//need to reverse if 9-7 comes
                                {
                                    int temp = limit;
                                    limit = start;
                                    start = temp;
                                }
                            }
                            catch { }

                            for (int ci = start; ci <= limit; ci++)
                            {
                                minsep.Add(ci.ToString());//add whole to list
                            }
                        }
                    }
                    else
                        minsep.Add(str);
                }
            }
            if (isnot)
            {
                for (int ci = 1; ci <= catcount; ci++)
                {
                    if (!exclidelist.Contains(ci))
                    {
                        if (!string.IsNullOrEmpty(commaseperatedvalues))
                        {
                            commaseperatedvalues += ",";
                        }

                        commaseperatedvalues += ci;
                    }
                }
            }
            else
            {
                foreach (string str in minsep)
                {
                    if (!string.IsNullOrEmpty(commaseperatedvalues))
                    {
                        commaseperatedvalues += ",";
                    }

                    commaseperatedvalues += str;
                }
            }
            return commaseperatedvalues;
        }
        private bool ValidateCLASSNtypeCriteria(Excel.Range ChangedCell, bool displayMessage = true, int choiceVariableRefColumn = 0, bool ignoreNotEqual = false)
        {
            //ValidateNtypeCondition
            string[] SplitContent;
            string Contents = ChangedCell.Text;
            int val = 0;
            if (Contents.Length == 1 && !Contents.IsIntegerExpression(out val))
            {
                if (displayMessage)
                {
                    MessageDialog.ErrorOk(CommonResource.ERR_MSG_SET_NUMERIC_VALUE);

                }

                return false;
            }


            if (ignoreNotEqual && (Contents.StartsWith("<>") || Contents.StartsWith("!")))
            {
                Contents = Contents.Replace("<>", "");
                Contents = Contents.Replace("!", "");
            }
            if (Contents.Length > 1 && (Contents.StartsWith("-")))//191 added for criteria start with or end with "-"
            {
                Contents = double.MinValue.ToString("r") + Contents;
            }
            if (Contents.Length > 1 && Contents.EndsWith("-"))
            {
                Contents = Contents + double.MaxValue.ToString("r");
            }
            ChangedCell.Font.Color = System.Drawing.Color.Black;

            if (Contents.Contains("/") || Contents.Contains(","))
            {
                char[] splitchar = { '/', ',' };
                SplitContent = Contents.Split(splitchar);
            }
            else
            {
                SplitContent = new string[] { Contents };
            }
            foreach (string item in SplitContent)
            {
                string value = item;
                string Div_Char = "@";
                string minval = "minVal";

                value = value.Replace("(-", Div_Char);
                value = value.Replace(double.MinValue.ToString("r"), minval);
                value = value.Replace("(", "");
                value = value.Replace(")", "");

                int countofseperator = value.Count(f => (f == '-'));
                if (countofseperator > 1)
                {
                    return false;
                }

                value = value.Replace("-", ",");
                value = value.Replace(Div_Char, "-");
                value = value.Replace(minval, double.MinValue.ToString("r"));
                string[] split2 = value.Split(',');
                double[] values = new double[2];
                int i = 0;
                foreach (string s2 in split2)
                {
                    if (string.IsNullOrEmpty(s2))
                        continue;
                    double output = 0;
                    bool err = false;
                    //if (!s2.IsDoubleExpression(out output, false, true, true, false))
                    //{
                    //    err = true;
                    //}
                    //else 
                    if (output > double.MaxValue || output < double.MinValue)
                    {
                        err = true;
                    }
                    if (err)
                    {
                        if (displayMessage)
                        {
                            MessageDialog.ErrorOk(CommonResource.ERR_MSG_SET_NUMERIC_VALUE);

                        }

                        return false;
                    }
                    values[i] = output;
                    i++;
                }
                if (values[0] > values[1] && i == 2)
                {
                    return false;
                }
            }/*Contents = Contents.Replace(Contents, Div_Char, "-");
            ReplacedContent = Split(Contents, ",")
            If UBound(Data_Array2) > 1 Then*/
            return true;

        }
        public static void FillDataProcessColumnB(Excel.Worksheet Sheet)
        {
            Excel.Range Range = null;
            Excel.Range xlRange = null;
            bool enableEvet = Sheet.Application.EnableEvents;
            bool scrUpdate = Sheet.Application.ScreenUpdating;
            Excel.XlCalculation xlCalculation = Sheet.Application.Calculation;

            Sheet.Application.EnableEvents = false;
            Sheet.Application.ScreenUpdating = false;
            Sheet.Application.Calculation = Excel.XlCalculation.xlCalculationManual;

            Range = ExcelUtilForAddIn.GetNamedRange(currworkbook,"List", "List_Item_ALL");

            if (Range == null) return;
            xlRange = Sheet.Range["B5", "B" + (Range.Count + 4)];

            Excel.Range last = Sheet.Range["B5"].SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
            Sheet.Range["B5", "B" + last.Row].ClearContents();

            if (Range.Count > 1)
            {
                var arr = xlRange.Value;
                for (int i = 1; i <= Range.Count; i++)
                {
                    arr[i, 1] = Range.Cells[i, 1].Value;
                }
                xlRange.Value = arr;
            }
            else
            {
                xlRange.Value = Range.Cells[1, 1].Text;
            }

            Sheet.Application.EnableEvents = enableEvet;
            Sheet.Application.ScreenUpdating = scrUpdate;
            Sheet.Application.Calculation = xlCalculation;
        }
        public string GetReplacecharacter(string Variablename)////redmine #211586
        {
            string paramchar = @"[￥]";
            string paramchar1byte = @"[¥]";
            string paramchareng = @"[\]";
            string replacablechar = string.Empty;

            if (Variablename.Contains(paramchar))
            {
                replacablechar = paramchar;
            }
            else if (Variablename.Contains(paramchar1byte))
            {
                replacablechar = paramchar1byte;
            }
            else if (Variablename.Contains(paramchareng))
            {
                replacablechar = paramchareng;
            }
            return replacablechar;
        }
    }

}
