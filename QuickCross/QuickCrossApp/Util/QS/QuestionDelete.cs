using log4net;
using NPOI.SS.Formula.Functions;
using NPOI.XWPF.UserModel;
using QC4Common.DB;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace Qc4Launcher.Util.QS
{
   public class QuestionDelete
    {
        Excel.Workbook workbook;
        string variable = String.Empty;
        string temppath = String.Empty;
        int selectedItemInedex;
        String sourcevariabledigit = string.Empty;
        string qstype = string.Empty;
        public bool Firstcheck=false;
        int DpRecodeCount;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public void StartQuestionDelete(Excel.Workbook workbook,string variable,bool Firstcheck=false,int DpRecodeCount=0)
        {
            try
            {
                this.workbook = workbook;
                this.variable = variable;
                this.DpRecodeCount = DpRecodeCount;
                this.qstype = Util.Definiotion.VariableDictionary[variable].QuestionFlag;
                this.selectedItemInedex = Util.Definiotion.VariableDictionary[variable].RowNumber - 4;
                this.Firstcheck = Firstcheck;
                workbook.Application.EnableEvents = false;
                if (!Firstcheck)
                {
                    DeletefromQuestionSettingSheet();
                }
                DeleteFromCrossTabulationsheet_S();
                DeleteFromGTTabulation_S();
                DeleteFromFAList_S();
                DeleteFromDataProcesssheet_S();
                if (this.qstype != Qc4Launcher.Util.Constants.Variable_Type_An)
                {
                    DeleteFromDataAfterProcesssheet();
                }
                if (!Firstcheck)
                {
                    DeletefromDB();
                    if (DpRecodeCount != 0)
                    {
                        if (DpRecodeCount != 0)
                        {
                            int digit = GetDigitFromSourceVariable(variable);
                            sourcevariabledigit = digit.ToString();
                            string variable1 = ReplaceRepeatSourceVariable(variable, digit.ToString());
                          
                            for (int i = 0; i < DpRecodeCount; i++)
                            {
                                Util.Definiotion.VariableDictionary.Remove(variable1);
                                try
                                {//Redmine id : 201968-new commnt GT issue
                                    this.variable = variable1;
                                    DeleteFromCrossTabulationsheet_S();
                                    DeleteFromGTTabulation_S();
                                    DeleteFromFAList_S();
                                    DeleteFromDataProcesssheet_S();
                                    if (this.qstype != Qc4Launcher.Util.Constants.Variable_Type_An)
                                    {
                                        DeleteFromDataAfterProcesssheet();
                                    }
                                }
                                catch { }
                                digit++;
                                variable1 = ReplaceRepeatSourceVariable(variable, digit.ToString());
                            }
                        }
                    }
                    else
                    {
                        Util.Definiotion.VariableDictionary.Remove(variable);
                    }
                    DeletefromList();
                    DeleteWeightBack();
                }
                this.Firstcheck = false;
                workbook.Application.EnableEvents = true;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }
        private void DeletefromList()
        {
            new QC4Common.Sheets.ListUpdate(workbook).UpdateListSheet(Util.Definiotion.VariableDictionary.Select(q => q.Value).ToList());
        }
        private void DeletefromQuestionSettingSheet()
        {
            Excel.Worksheet s = Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Util.Constants.SheetCodeName.QuestionSetting);
            Excel.Range start = s.Cells[4, 6];
            Excel.Range end = s.Cells[1048576, 6];
            Excel.Range r = s.Range[start, end];
            if (DpRecodeCount != 0)
            {
                int digit = GetDigitFromSourceVariable(variable);
                sourcevariabledigit = digit.ToString();
                string variable1 = ReplaceRepeatSourceVariable(variable, digit.ToString());
                for (int i=0;i<DpRecodeCount;i++)
                {
                    Excel.Range result = r.Find(What: variable1, LookAt: Excel.XlLookAt.xlWhole);
                    while (result != null)
                    {
                        result.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                        result = r.Find(variable1);
                    }
                    digit++;
                    variable1= ReplaceRepeatSourceVariable(variable, digit.ToString());
                }
            }
            else
            {
                
                Excel.Range result = r.Find(What: variable, LookAt: Excel.XlLookAt.xlWhole);
                while (result != null)
                {
                    result.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                    result = r.Find(variable);
                }
            }

        }
        
        private void DeleteFromDataProcesssheet()
        {
            Excel.Worksheet s = Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Util.Constants.SheetCodeName.DataProcess);
            Excel.Range start = s.Cells[5, 3];
            Excel.Range End = s.Cells[1048576, 1042];
            Excel.Range r = s.Range[start, End];
            Excel.Range result = r.Find(What:variable,LookAt: Excel.XlLookAt.xlWhole);
             while( result!=null)
            {
                if (Firstcheck)
                {
                    CheckVariableExistInSheets check = new CheckVariableExistInSheets();
                    check.Changestate(Util.Constants.SheetCodeName.DataProcess, true);
                    break;
                }
             
            } 
        }
        private void DeleteFromDataAfterProcesssheet()
        {
            Excel.Worksheet s = Util.ExcelUtil.GetWorkSheetBySheetName(workbook, Constants.SheetType.sh_Data01 + "(Processed)");
            workbook.Unprotect(Constants.Password);
            if(s!=null)
            {
                if (Firstcheck)
                {
                    CheckVariableExistInSheets check = new CheckVariableExistInSheets();
                    check.Changestate(Util.Constants.SheetCodeName.Data01After, true);
                }
                else
                {                   
                    QC4Common.Util.ExcelUtil.DeleteDataAfterSheets(workbook);
                    using (SQLiteConnection dbConn = DBHelper.GetConnection(DBHelper.GetConnectionString(workbook)))
                    {
                        dbConn.Open();
                        string sql = "DROP TABLE IF EXISTS data_after_process;";
                        DBHelper.ExecuteQuery(sql, dbConn);
                    }
                }
            }

        }
        private void DeleteFromDataProcesssheet_S()
        {
            Excel.Worksheet s = Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Util.Constants.SheetCodeName.DataProcessS);
            Excel.Range start = s.Cells[5, 3];
            Excel.Range End = s.Cells[1048576, 1042];
            Excel.Range r = s.Range[start, End];
            DeleteprocessformDataprocss_sSheet(s, r);
        }
        private void DeleteFromCrossTabulationsheet()
        {
            Excel.Worksheet s = Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Util.Constants.SheetCodeName.CrossTabulation);
          
            Excel.Range start = s.Cells[14, 3];
            Excel.Range End = s.Cells[1048576,3];
            Excel.Range r = s.Range[start, End];
            Excel.Range result = r.Find(What: variable, LookAt: Excel.XlLookAt.xlWhole);
            while (result != null)
            {
                if (Firstcheck)
                {
                    CheckVariableExistInSheets check = new CheckVariableExistInSheets();
                    check.Changestate(Util.Constants.SheetCodeName.CrossTabulation, true);
                    break;
                }
           
            }
        
            start = s.Cells[3, 7];
            End = s.Cells[8, 16348];
            r = s.Range[start, End];
            while (result != null)
            {
                if (Firstcheck)
                {
                    CheckVariableExistInSheets check = new CheckVariableExistInSheets();
                    check.Changestate(Util.Constants.SheetCodeName.CrossTabulation, true);
                    break;
                }
                result.EntireColumn.Delete(Excel.XlDeleteShiftDirection.xlShiftToLeft);
                result = r.Find(What: variable, LookAt: Excel.XlLookAt.xlWhole);
            }

        }
        private void DeleteFromCrossTabulationsheet_S()
        {
            Excel.Worksheet s = Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Util.Constants.SheetCodeName.CrossTabulationS);
         
            Excel.Range start = s.Cells[15, 3];
            Excel.Range End = s.Cells[1048576, 3];
            Excel.Range r = s.Range[start, End];
            Excel.Range result = r.Find(What: variable, LookAt: Excel.XlLookAt.xlWhole);
            while (result != null)
            {
                if (Firstcheck)
                {
                    CheckVariableExistInSheets check = new CheckVariableExistInSheets();
                    check.Changestate(Util.Constants.SheetCodeName.CrossTabulation, true);
                    break;
                }
                result.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                result = r.Find(What: variable, LookAt: Excel.XlLookAt.xlWhole);
            }
            
            start = s.Cells[3, 7];
            End = s.Cells[8, 16348];
            r = s.Range[start, End];
            while (result != null)
            {
                if (Firstcheck)
                {
                    CheckVariableExistInSheets check = new CheckVariableExistInSheets();
                    check.Changestate(Util.Constants.SheetCodeName.CrossTabulation, true);
                    break;
                }
                result.EntireColumn.Delete(Excel.XlDeleteShiftDirection.xlShiftToLeft);
                result = r.Find(What: variable, LookAt: Excel.XlLookAt.xlWhole);
            }

        }
        private void DeleteFromGTTabulation()
        {
            Excel.Worksheet s = Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Util.Constants.SheetCodeName.GTTabulation);
       
            Excel.Range start = s.Cells[5, 2];
            Excel.Range End = s.Cells[1048576, 206];
            Excel.Range r = s.Range[start, End];
            Excel.Range result = r.Find(What: variable, LookAt: Excel.XlLookAt.xlWhole);
            while (result != null)
            {
                if (Firstcheck)
                {
                    CheckVariableExistInSheets check = new CheckVariableExistInSheets();
                    check.Changestate(Util.Constants.SheetCodeName.GTTabulation, true);
                    break;
                }
              
            }
        }
        private void DeleteFromGTTabulation_S()
        {
            Excel.Worksheet s = Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Util.Constants.SheetCodeName.GTTabulationS);

            Excel.Range start = s.Cells[5, 2];
            Excel.Range End = s.Cells[1048576, 206];
            Excel.Range r = s.Range[start, End];
            Excel.Range result = r.Find(What: variable, LookAt: Excel.XlLookAt.xlWhole);
            while (result != null)
            {
                if (Firstcheck)
                {
                    CheckVariableExistInSheets check = new CheckVariableExistInSheets();
                    check.Changestate(Util.Constants.SheetCodeName.GTTabulation, true);
                    break;
                }
                result.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                result = r.Find(What: variable, LookAt: Excel.XlLookAt.xlWhole);
            }
        }
        private void DeleteFromFAList()
            
        {
            Excel.Worksheet s = Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Util.Constants.SheetCodeName.FACreation);
            Excel.Range start = s.Cells[5, 2];
            Excel.Range End = s.Cells[1048576, 63];
            Excel.Range r = s.Range[start, End];
            Excel.Range result = r.Find(What: variable, LookAt: Excel.XlLookAt.xlWhole);
            while (result != null)
            {
                if (Firstcheck)
                {
                    CheckVariableExistInSheets check = new CheckVariableExistInSheets();
                    check.Changestate(Util.Constants.SheetCodeName.FACreation, true);
                    break;
                }
              
            }
        }
        private void DeleteFromFAList_S()
        {
            Excel.Worksheet s = Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Util.Constants.SheetCodeName.FACreationS);
            Excel.Range start = s.Cells[5, 2];
            Excel.Range End = s.Cells[1048576, 63];
            Excel.Range r = s.Range[start, End];
            Excel.Range result = r.Find(What: variable, LookAt: Excel.XlLookAt.xlWhole);
            while (result != null)
            {
                if (Firstcheck)
                {
                    CheckVariableExistInSheets check = new CheckVariableExistInSheets();
                    check.Changestate(Util.Constants.SheetCodeName.FACreation, true);
                    break;
                }
                result = null;
            }
            if(!Firstcheck)
            {
              
                bool IsRowdeleted = false;
                start = s.Cells[5, 3];
                if (start.Value==variable)
                {
                    start.Value = null;
                    IsRowdeleted = true;
                }
                if (!IsRowdeleted)
                {
                    start = s.Cells[5, 4];
                    End = s.Cells[5, 33];
                    r = s.Range[start, End];
                    result = r.Find(What: variable, LookAt: Excel.XlLookAt.xlWhole);
                    while (result != null)
                    {
                        result.Delete(Excel.XlDeleteShiftDirection.xlShiftToLeft);
                        Excel.Range InsertNew = s.Cells[5, 33];
                        InsertNew.Insert(Excel.XlInsertShiftDirection.xlShiftToRight);
                        result = r.Find(What: variable, LookAt: Excel.XlLookAt.xlWhole);

                    }
                }
                    start = s.Cells[5, 34];
                    End = s.Cells[5, 63];
                    r = s.Range[start, End];
                    result = r.Find(What: variable, LookAt: Excel.XlLookAt.xlWhole);
                    while (result != null)
                    {
                        result.Delete(Excel.XlDeleteShiftDirection.xlShiftToLeft);
                        result = r.Find(What: variable, LookAt: Excel.XlLookAt.xlWhole);
                    }
                
            }

        }
        private void DeleteFromSummeryTable()
        {
            Excel.Worksheet s = Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Util.Constants.SheetCodeName.SummaryTabulation);
          
            Excel.Range start = s.Cells[8,4];
            Excel.Range End = s.Cells[1048576, 4];
            Excel.Range r = s.Range[start, End];
            Excel.Range result = r.Find(What: variable, LookAt: Excel.XlLookAt.xlWhole);
            while (result != null)
            {
                if (Firstcheck)
                {
                    CheckVariableExistInSheets check = new CheckVariableExistInSheets();
                    check.Changestate(Util.Constants.SheetCodeName.SummaryTable, true);
                    break;
                }

                result.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                result = r.Find(What: variable, LookAt: Excel.XlLookAt.xlWhole);
            }
           
            start = s.Cells[4, 8];
            End = s.Cells[4, 16348];
            r = s.Range[start, End];
            while (result != null)
            {
                if (Firstcheck)
                {
                    CheckVariableExistInSheets check = new CheckVariableExistInSheets();
                    check.Changestate(Util.Constants.SheetCodeName.SummaryTable, true);
                    break;
                }
                result.EntireColumn.Delete(Excel.XlDeleteShiftDirection.xlShiftToLeft);
                result = r.Find(What: variable, LookAt: Excel.XlLookAt.xlWhole);
            }
        }
        private void DeletefromDB()
        {
            DB.QuestionSettingDao questionSetting = new DB.QuestionSettingDao(QC4Common.DB.DBHelper.GetConnectionString(workbook));
            try
            {
                if (qstype == "Imp")
                {
                    questionSetting.DeleteAnswer(variable);
                }
                if (qstype == "An")
                {
                    questionSetting.DeleteFromMultiVariateTable(variable);
                    QC4Common.Util.ExcelUtil.DeleteFromMultiVariateSheet(workbook, variable);
                }

                if (DpRecodeCount != 0)
                {
                    int digit = GetDigitFromSourceVariable(variable);
                    sourcevariabledigit = digit.ToString();
                    string variable1 = ReplaceRepeatSourceVariable(variable, digit.ToString());
                    for (int i = 0; i < DpRecodeCount; i++)
                    {
                        questionSetting.DeleteQuestions(variable1);
                        digit++;
                        variable1 = ReplaceRepeatSourceVariable(variable, digit.ToString());
                    }
                    if (qstype != "An")
                    {
                        questionSetting.DeleteDataAfterProcess();
                    }
                }
                else
                {
                    questionSetting.DeleteQuestions(variable);
                    if (qstype != "An")
                    {
                        questionSetting.DeleteDataAfterProcess();
                    }
                }
                    
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

        }
        private void DeleteWeightBack()
        {
            Excel.Worksheet s = Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Util.Constants.SheetCodeName.Setting);
            Excel.Range Variable_cell = s.Cells[2,10];
            Excel.Range r = s.Range[Variable_cell, Variable_cell];
            Excel.Range result = r.Find(What: variable, LookAt: Excel.XlLookAt.xlWhole);
            while (result != null)
            {
                Excel.Range start = s.Cells[2, 10];
                Excel.Range end = s.Cells[2,12];
                Excel.Range area= s.Range[start, end];
                area.ClearContents();
                start = s.Cells[3,9];
                end = s.Cells[1048576, 12];
                area = s.Range[start, end];
                area.ClearContents();
                result = null;
            }

        }
        private void  DeleteprocessformDataprocss_sSheet(Excel.Worksheet sheet, Excel.Range range)
        {
            try
            {
                Excel.Range result = range.Find(What: variable, LookAt: Excel.XlLookAt.xlWhole);
                Excel.Range start;
                Excel.Range End;
                Excel.Range cell;

                while (result != null)
                {
                    if (Firstcheck)
                    {
                        CheckVariableExistInSheets check = new CheckVariableExistInSheets();
                        check.Changestate(Util.Constants.SheetCodeName.DataProcess, true);
                        break;
                    }
                    Excel.Range subRange = result.EntireRow;
                    if (subRange.Find(What: Constants.ProcessingMethod.CLASS, LookAt: Excel.XlLookAt.xlWhole) != null)
                    {
                        result.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                    }
                    else if (subRange.Find(What: Constants.ProcessingMethod.MTOS, LookAt: Excel.XlLookAt.xlWhole) != null)
                    {
                        result.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                    }
                    else if (subRange.Find(What: Constants.ProcessingMethod.JOINT, LookAt: Excel.XlLookAt.xlWhole) != null)
                    {
                        result.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                    }
                    else if (subRange.Find(What: Constants.ProcessingMethod.COUNT, LookAt: Excel.XlLookAt.xlWhole) != null)
                    {
                        result.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                    }
                    else if (subRange.Find(What: Constants.ProcessingMethod.INTEGRATE, LookAt: Excel.XlLookAt.xlWhole) != null)
                    {
                        result.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                    }
                    else if (subRange.Find(What: Constants.ProcessingMethod.MCONVERT, LookAt: Excel.XlLookAt.xlWhole) != null)
                    {
                        result.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                    }
                    else if (subRange.Find(What: Constants.ProcessingMethod.COMPUTE, LookAt: Excel.XlLookAt.xlWhole) != null)
                    {
                        result.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                    }
                    else if (subRange.Find(What: Constants.ProcessingMethod.RECODE, LookAt: Excel.XlLookAt.xlWhole) != null)
                    {
                        result.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);

                    }
                    else if (subRange.Find(What: QC4Common.Common.Constants.DP.SubstituteOperatorMIN, LookAt: Excel.XlLookAt.xlWhole) != null)
                    {
                        result.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                    }
                    else if (subRange.Find(What: QC4Common.Common.Constants.DP.SubstituteOperatorMAX, LookAt: Excel.XlLookAt.xlWhole) != null)
                    {
                        result.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                    }
                    else if (subRange.Find(What: QC4Common.Common.Constants.DP.SubstituteOperatorSUM, LookAt: Excel.XlLookAt.xlWhole) != null)
                    {
                        result.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                    }
                    else if (subRange.Find(What: QC4Common.Common.Constants.DP.SubstituteOperatorADD3, LookAt: Excel.XlLookAt.xlWhole) != null)
                    {
                        result.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                    }
                    else if (subRange.Find(What: QC4Common.Common.Constants.DP.SubstituteOperatorEQUAL, LookAt: Excel.XlLookAt.xlWhole) != null)
                    {
                    
                        int removedRowno = subRange.Row;
                        int row = subRange.Row;
                        start = sheet.Cells[row - 1, 8];
                        End = sheet.Cells[row - 1, 8];
                        cell = sheet.Range[start, End];
                        int No_row = 0;
                        while (cell.Value == QC4Common.Common.Constants.DP.InstructionAND || cell.Value == QC4Common.Common.Constants.DP.InstructionOR)
                        {
                            No_row++;
                            row = cell.Row;
                            removedRowno = row;
                            start = sheet.Cells[row - 1, 8];
                            End = sheet.Cells[row - 1, 8];
                            cell = sheet.Range[start, End];
                        }
                        for (int i = 0; i < No_row + 1; i++)
                        {
                            start = sheet.Cells[removedRowno, 8];
                            End = sheet.Cells[removedRowno, 8];
                            cell = sheet.Range[start, End];
                            cell.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                        }

                    }
                    else if (subRange.Find(What: QC4Common.Common.Constants.DP.SubstituteOperatorADD1, LookAt: Excel.XlLookAt.xlWhole) != null)
                    {
                   
                        int removedRowno = subRange.Row;
                        int row = subRange.Row;
                        start = sheet.Cells[row - 1, 8];
                        End = sheet.Cells[row - 1, 8];
                        cell = sheet.Range[start, End];
                        int No_row = 0;
                        while (cell.Value == QC4Common.Common.Constants.DP.InstructionAND || cell.Value == QC4Common.Common.Constants.DP.InstructionOR)
                        {
                            No_row++;
                            row = cell.Row;
                            removedRowno = row;
                            start = sheet.Cells[row - 1, 8];
                            End = sheet.Cells[row - 1, 8];
                            cell = sheet.Range[start, End];
                        }
                        for (int i = 0; i < No_row + 1; i++)
                        {
                            start = sheet.Cells[removedRowno, 8];
                            End = sheet.Cells[removedRowno, 8];
                            cell = sheet.Range[start, End];
                            cell.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                        }

                    }
                    else if (subRange.Find(What: QC4Common.Common.Constants.DP.SubstituteOperatorADD2, LookAt: Excel.XlLookAt.xlWhole) != null)
                    {
                        
                        int removedRowno = subRange.Row;
                        int row = subRange.Row;
                        start = sheet.Cells[row - 1, 8];
                        End = sheet.Cells[row - 1, 8];
                        cell = sheet.Range[start, End];
                        int No_row = 0;
                        while (cell.Value == QC4Common.Common.Constants.DP.InstructionAND || cell.Value == QC4Common.Common.Constants.DP.InstructionOR)
                        {
                            No_row++;
                            row = cell.Row;
                            removedRowno = row;
                            start = sheet.Cells[row - 1, 8];
                            End = sheet.Cells[row - 1, 8];
                            cell = sheet.Range[start, End];
                        }
                        for (int i = 0; i < No_row + 1; i++)
                        {
                            start = sheet.Cells[removedRowno, 8];
                            End = sheet.Cells[removedRowno, 8];
                            cell = sheet.Range[start, End];
                            cell.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                        }

                    }
                    else if (subRange.Find(What: QC4Common.Common.Constants.DP.SubstituteOperatorMINUS1, LookAt: Excel.XlLookAt.xlWhole) != null)
                    {
                        
                        int removedRowno = subRange.Row;
                        int row = subRange.Row;
                        start = sheet.Cells[row - 1, 8];
                        End = sheet.Cells[row - 1, 8];
                        cell = sheet.Range[start, End];
                        int No_row = 0;
                        while (cell.Value == QC4Common.Common.Constants.DP.InstructionAND || cell.Value == QC4Common.Common.Constants.DP.InstructionOR)
                        {
                            No_row++;
                            row = cell.Row;
                            removedRowno = row;
                            start = sheet.Cells[row - 1, 8];
                            End = sheet.Cells[row - 1, 8];
                            cell = sheet.Range[start, End];
                        }
                        for (int i = 0; i < No_row + 1; i++)
                        {
                            start = sheet.Cells[removedRowno, 8];
                            End = sheet.Cells[removedRowno, 8];
                            cell = sheet.Range[start, End];
                            cell.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                        }

                    }
                    else if (subRange.Find(What: QC4Common.Common.Constants.DP.SubstituteOperatorMINUS2, LookAt: Excel.XlLookAt.xlWhole) != null)
                    {
                    
                        int removedRowno = subRange.Row;
                        int row = subRange.Row;
                        start = sheet.Cells[row - 1, 8];
                        End = sheet.Cells[row - 1, 8];
                        cell = sheet.Range[start, End];
                        int No_row = 0;
                        while (cell.Value == QC4Common.Common.Constants.DP.InstructionAND || cell.Value == QC4Common.Common.Constants.DP.InstructionOR)
                        {
                            No_row++;
                            row = cell.Row;
                            removedRowno = row;
                            start = sheet.Cells[row - 1, 8];
                            End = sheet.Cells[row - 1, 8];
                            cell = sheet.Range[start, End];
                        }
                        for (int i = 0; i < No_row + 1; i++)
                        {
                            start = sheet.Cells[removedRowno, 8];
                            End = sheet.Cells[removedRowno, 8];
                            cell = sheet.Range[start, End];
                            cell.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                        }

                    }
                    else if (subRange.Find(What: QC4Common.Common.Constants.DP.InstructionOMIT, LookAt: Excel.XlLookAt.xlWhole) != null)
                    {
                    
                        int removedRowno = subRange.Row;
                        int row = subRange.Row;
                        start = sheet.Cells[row - 1, 8];
                        End = sheet.Cells[row - 1, 8];
                        cell = sheet.Range[start, End];
                        int No_row = 0;
                        while (cell.Value == QC4Common.Common.Constants.DP.InstructionAND || cell.Value == QC4Common.Common.Constants.DP.InstructionOR)
                        {
                            No_row++;
                            row = cell.Row;
                            removedRowno = row;
                            start = sheet.Cells[row - 1, 8];
                            End = sheet.Cells[row - 1, 8];
                            cell = sheet.Range[start, End];
                        }
                        for (int i = 0; i < No_row + 1; i++)
                        {
                            start = sheet.Cells[removedRowno, 8];
                            End = sheet.Cells[removedRowno, 8];
                            cell = sheet.Range[start, End];
                            cell.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                        }

                    }
                    else if (subRange.Find(What: QC4Common.Common.Constants.DP.InstructionDELETE, LookAt: Excel.XlLookAt.xlWhole) != null)
                    {
                      
                        int removedRowno = subRange.Row;
                        int row = subRange.Row;
                        start = sheet.Cells[row - 1, 8];
                        End = sheet.Cells[row - 1, 8];
                        cell = sheet.Range[start, End];
                        int No_row = 0;
                        while (cell.Value == QC4Common.Common.Constants.DP.InstructionAND || cell.Value == QC4Common.Common.Constants.DP.InstructionOR)
                        {
                            No_row++;
                            row = cell.Row;
                            removedRowno = row;
                            start = sheet.Cells[row - 1, 8];
                            End = sheet.Cells[row - 1, 8];
                            cell = sheet.Range[start, End];
                        }
                        for (int i = 0; i < No_row + 1; i++)
                        {
                            start = sheet.Cells[removedRowno, 8];
                            End = sheet.Cells[removedRowno, 8];
                            cell = sheet.Range[start, End];
                            cell.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                        }

                    }
                    else if (subRange.Find(What: QC4Common.Common.Constants.DP.InstructionLISTUP, LookAt: Excel.XlLookAt.xlWhole) != null)
                    {
                        
                        int removedRowno = subRange.Row;
                        int row = subRange.Row;
                        start = sheet.Cells[row - 1, 8];
                        End = sheet.Cells[row - 1, 8];
                        cell = sheet.Range[start, End];
                        int No_row = 0;
                        while (cell.Value == QC4Common.Common.Constants.DP.InstructionAND || cell.Value == QC4Common.Common.Constants.DP.InstructionOR)
                        {
                            No_row++;
                            row = cell.Row;
                            removedRowno = row;
                            start = sheet.Cells[row - 1, 8];
                            End = sheet.Cells[row - 1, 8];
                            cell = sheet.Range[start, End];
                        }
                        for (int i = 0; i < No_row + 1; i++)
                        {
                            start = sheet.Cells[removedRowno, 8];
                            End = sheet.Cells[removedRowno, 8];
                            cell = sheet.Range[start, End];
                            cell.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                        }

                    }

                    else if (subRange.Find(What: QC4Common.Common.Constants.DP.InstructionAND, LookAt: Excel.XlLookAt.xlWhole) != null)
                    {
                       
                        int startRowno = subRange.Row;
                        int endRowno = startRowno + 1;
                        int No_row = 0;
                        int row = subRange.Row;
                        start = sheet.Cells[row + 1, 8];
                        End = sheet.Cells[row + 1, 8];
                        cell = sheet.Range[start, End];
                        while (cell.Value == QC4Common.Common.Constants.DP.InstructionAND || cell.Value == QC4Common.Common.Constants.DP.InstructionOR)
                        {
                            No_row++;
                            endRowno = cell.Row + 1;
                            row = cell.Row;
                            start = sheet.Cells[row + 1, 8];
                            End = sheet.Cells[row + 1, 8];
                            cell = sheet.Range[start, End];

                        }
                        row = subRange.Row;
                        start = sheet.Cells[row - 1, 8];
                        End = sheet.Cells[row - 1, 8];
                        cell = sheet.Range[start, End];
                        while (cell.Value == QC4Common.Common.Constants.DP.InstructionAND || cell.Value == QC4Common.Common.Constants.DP.InstructionOR)
                        {
                            No_row++;
                            startRowno = cell.Row;
                            row = cell.Row;
                            start = sheet.Cells[row - 1, 8];
                            End = sheet.Cells[row - 1, 8];
                            cell = sheet.Range[start, End];

                        }

                        for (int i = startRowno; i <= startRowno + (endRowno - startRowno); i++)
                        {
                            start = sheet.Cells[startRowno, 8];
                            End = sheet.Cells[startRowno, 8];
                            cell = sheet.Range[start, End];
                            cell.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                        }
                    }

                    else if (subRange.Find(What: QC4Common.Common.Constants.DP.InstructionOR, LookAt: Excel.XlLookAt.xlWhole) != null)
                    {
                        
                        int startRowno = subRange.Row;
                        int endRowno = startRowno + 1;
                        int No_row = 0;
                        int row = subRange.Row;
                        start = sheet.Cells[row + 1, 8];
                        End = sheet.Cells[row + 1, 8];
                        cell = sheet.Range[start, End];
                        while (cell.Value == QC4Common.Common.Constants.DP.InstructionAND || cell.Value == QC4Common.Common.Constants.DP.InstructionOR)
                        {
                            No_row++;
                            endRowno = cell.Row + 1;
                            row = cell.Row;
                            start = sheet.Cells[row + 1, 8];
                            End = sheet.Cells[row + 1, 8];
                            cell = sheet.Range[start, End];

                        }
                        row = subRange.Row;
                        start = sheet.Cells[row - 1, 8];
                        End = sheet.Cells[row - 1, 8];
                        cell = sheet.Range[start, End];
                        while (cell.Value == QC4Common.Common.Constants.DP.InstructionAND || cell.Value == QC4Common.Common.Constants.DP.InstructionOR)
                        {
                            No_row++;
                            startRowno = cell.Row;
                            row = cell.Row;
                            start = sheet.Cells[row - 1, 8];
                            End = sheet.Cells[row - 1, 8];
                            cell = sheet.Range[start, End];

                        }

                        for (int i = startRowno; i <= startRowno + (endRowno - startRowno); i++)
                        {
                            start = sheet.Cells[startRowno, 8];
                            End = sheet.Cells[startRowno, 8];
                            cell = sheet.Range[start, End];
                            cell.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                        }

                    }


                    result = range.Find(What: variable, LookAt: Excel.XlLookAt.xlWhole);
                }

                string variable1 = "[" + variable + "]";
                result = range.Find(What: variable1, LookAt: Excel.XlLookAt.xlPart);
                while (result != null)
                {
                    if (Firstcheck)
                    {
                        CheckVariableExistInSheets check = new CheckVariableExistInSheets();
                        check.Changestate(Util.Constants.SheetCodeName.DataProcess, true);
                        break;
                    }
                    result.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                    result = range.Find(What: variable1, LookAt: Excel.XlLookAt.xlWhole);
                }
                if (DpRecodeCount > 0)
                {
                    string variableRecode = Get_RepeateVariable(variable);
                    result = range.Find(What: variableRecode, LookAt: Excel.XlLookAt.xlPart);
                    while (result != null)
                    {
                        int row = result.Row;
                        row--;
                        
                        for (int i = 0; i < 3; i++)
                        {
                            start = sheet.Cells[row, 8];
                            End = sheet.Cells[row, 8];
                            cell = sheet.Range[start, End];
                            cell.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                        }
                        result = range.Find(What: variableRecode, LookAt: Excel.XlLookAt.xlWhole);
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

        }
        public int GetDigitFromSourceVariable(string sourceVariable)
        {
            
            string digit = string.Empty;
            int val;

            for (int i = 0; i < sourceVariable.Length; i++)
            {
                if (Char.IsDigit(sourceVariable[i]))
                    digit += sourceVariable[i];
                else
                    digit = string.Empty;
            }

            if (digit.Length > 0)
            {
                val = int.Parse(digit.Normalize(NormalizationForm.FormKC));
            }
            else
            {
                val = -1;
            }
            return val;
        }
        public string ReplaceRepeatSourceVariable(string actualSourceVariable, string digit)
        {
            try
            {
                actualSourceVariable = actualSourceVariable.Remove(actualSourceVariable.LastIndexOf(sourcevariabledigit)) + digit;
                return actualSourceVariable;
            }
            catch
            {
                return actualSourceVariable; 
            }
        }
        public string Get_RepeateVariable(string actualSourceVariable, string digit="1")
        {
            actualSourceVariable = actualSourceVariable.Remove(actualSourceVariable.LastIndexOf(digit)) + "[\\]";
            return actualSourceVariable;
        }


    }
}
