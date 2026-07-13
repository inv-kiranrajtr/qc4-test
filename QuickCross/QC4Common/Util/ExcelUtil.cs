
using QC4Common.Common;
using QC4Common.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using VB = Microsoft.VisualBasic;

namespace QC4Common.Util
{
    public class ExcelUtil
    {
        public Excel.Range GetLastCell(Excel.Range range)
        {
            return range.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
        }

        public static Excel.Range EndxlUp(Excel.Range targetCell)
        {
            //targetCell.Worksheet.Rows.Hidden = false; //TO handle last hidden row
            Excel.Range targetColumn = targetCell.Cells[1].EntireColumn;
            return targetColumn.Cells[targetColumn.Cells.Count].End(Excel.XlDirection.xlUp);
        }

        public static bool AddValidation(Excel.Range excelRange, Excel.XlDVType validationType, object AlertStyle, object Operator, object Formula1, object Formula2, object message = null)
        {
            excelRange.Validation.Delete();
            try
            {
                excelRange.Validation.Add(validationType, AlertStyle, Operator, Formula1, Formula2);
                excelRange.Validation.ShowError = false;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return true;
        }

        public static int? EndColumn(Excel.Worksheet sheet)
        {
            if (sheet == null)
            {
                return null;
            }
            return sheet.Columns.Count;
        }

        public static Excel.Range EndxlRight(Excel.Range targetCell)
        {
            Excel.Range targetColumn = targetCell.Cells[1].EntireRow;
            return targetColumn.Cells[targetColumn.Cells.Count].End(Excel.XlDirection.xlToLeft);
        }

        public static Excel.Worksheet GetWorkSheetByCodeName(Excel.Workbook book, string codeName)
        {
            foreach (Excel.Worksheet sheet in book.Worksheets)
            {
                if (codeName == sheet.CodeName)
                {
                    return sheet;
                }
            }
            return null;
        }

        public static Excel.Worksheet GetWorkSheetBySheetName(Excel.Workbook book, string sheetName)
        {
            foreach (Excel.Worksheet sheet in book.Worksheets)
            {
                if (sheetName == sheet.Name)
                {
                    return sheet;
                }
            }
            return null;
        }


        public static Excel.Workbook AddWorkbook(string selectedFile, Excel.Application excelApp = null)
        {
            Excel.Workbook excelWorkbook = null;
            try
            {
                if (excelApp == null)
                {
                    excelApp = new Excel.Application();
                }

                excelApp.DisplayAlerts = false;
                excelWorkbook = excelApp.Workbooks.Add(selectedFile);
                excelWorkbook.Unprotect(Constants.Password);
                excelWorkbook.Saved = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return excelWorkbook;
        }

        public static Excel.Workbook OpenWorkbok(string selectedFile, Excel.Application excelApp = null)
        {
            Excel.Workbook excelWorkbook = null;
            try
            {
                if (excelApp == null)
                {
                    excelApp = new Excel.Application();
                }
                try
                {
                    excelApp.DisplayAlerts = false;
                }
                catch { }
                excelWorkbook = excelApp.Workbooks.Open(selectedFile, 0, false, 5, Constants.Password, "", true,
                   Excel.XlPlatform.xlWindows, "\t", false, false, 0, false, 1, 0);
                excelWorkbook.Unprotect(Constants.Password);
                excelWorkbook.Saved = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return excelWorkbook;
        }

        internal static string GetTemplatePath(string templateName)
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\QC4\\Templates\\" + templateName;
        }

        internal static Excel.Range GetNamedRange(string CodeName, string RangeName, Excel.Workbook workbook)
        {
            try
            {
                Excel.Range ReturnRange = GetWorkSheetByCodeName(workbook, CodeName).get_Range(RangeName);
                return ReturnRange;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static bool GetCheckboxValue(Excel.Worksheet worksheet, string checkBoxName)
        {

            var response = false;
            // A checkbox is considered a shape by Excel and accessed using that object model
            Excel.Shapes shapes = worksheet.Shapes;
            foreach (Excel.Shape shape in shapes)
                if (shape.Name == checkBoxName) // Only give me the value for the checkbox I’m looking for
                    response = DetermineIfCheckboxIsChecked(shape.OLEFormat.Object.Value); // Value returned is a double (not a bool)
            return response;

        }

        private static bool DetermineIfCheckboxIsChecked(double value)
        {
            var response = false;
            if (value > 0) // Checkbox is checked == 1.0
                response = true;
            else // Checkbox is not checked == -4713.0
                response = false;
            return response;
        }

        public static void SetCellInteriorColor(Excel.Range ExcelCell, dynamic Color)
        {
            ExcelCell.Interior.Color = Color;
        }

        public static List<Excel.Worksheet> GetDataSheets(Excel.Workbook workbook)
        {
            return GetSheetByRegex(@"Data[0-9]+$", workbook);
        }

        public static List<Excel.Worksheet> GetDataAfterProcessSheets(Excel.Workbook workbook)
        {
            return GetSheetByRegex(@"Data[0-9]+\(.*\)$", workbook);
        }

        public static List<Excel.Worksheet> GetMultivariateSheet(Excel.Workbook workbook)
        {
            return GetSheetByRegex(@"(多変量|Multivariate)[0-9]+$", workbook);
        }

        public static List<Excel.Worksheet> GetSheetByRegex(string regex, Excel.Workbook workbook)
        {
            List<Excel.Worksheet> worksheets = new List<Excel.Worksheet>();
            foreach (Excel.Worksheet sheet in workbook.Worksheets)
            {
                string name = sheet.Name;
                try
                {
                    name = VB.Strings.StrConv(name, VB.VbStrConv.Narrow);
                }
                catch { }
                if (Regex.IsMatch(name, regex))
                {
                    worksheets.Add(sheet);
                }
            }
            return worksheets.OrderBy(w => w.Name).ToList();
        }

        public static Excel.Range FindLastCell(Excel.Worksheet worksheet)
        {
            Excel.Range lRow = null;
            try
            {
                lRow = worksheet.Cells.Find(What: "*",
                                After: worksheet.Range["A1"],
                                LookAt: Excel.XlLookAt.xlPart,
                                LookIn: Excel.XlFindLookIn.xlFormulas,
                                SearchOrder: Excel.XlSearchOrder.xlByRows,
                                SearchDirection: Excel.XlSearchDirection.xlPrevious,
                                MatchCase: false);
            }
            catch { }
            return lRow;
        }

        public static int FindLastUsedRow(Excel.Worksheet sheet)
        {
            Excel.Range r = FindLastCell(sheet);
            return null == r ? 0 : r.Row;
        }

        public static bool IsEditing(Excel.Application excelApp)
        {
            Microsoft.Office.Core.CommandBars cBars = excelApp.CommandBars;
            bool res = !cBars.GetEnabledMso("FileNewDefault");
            Marshal.ReleaseComObject(cBars);
            return res;
        }

        public static string GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return " " + columnName;
        }

        public static void ClearContents(Excel.Range range)
        {
            range.ClearContents();
        }
        public static void GenerateNewDataSheet(Excel.Workbook workbook, List<QC4Common.Model.QuestionSettings> questions, string tableName = "answers")
        {
            workbook.Application.EnableEvents = false;
            DataTable dt;
            Excel.Worksheet Sheet;
            Excel.Worksheet worksheet;
            int Max_RowCount = Constants.ExcelRowColumnMax.ExcelMaxRow;
            int Max_ColumnCount = Constants.ExcelRowColumnMax.ExcelMaxCol;
            string connectionString = QC4Common.DB.DBHelper.GetConnectionString(workbook);
            questions = questions.OrderBy(a => a.RowNumber).ToList();
            DataTable dtCount = QC4Common.DB.DBHelper.GetDataTable("SELECT COUNT(*) FROM " + tableName, connectionString);
            int Current_Row_count = Convert.ToInt32(dtCount.Rows[0][0].ToString());
            int Current_Col_Count = questions.Count();
            var ary = questions.Where(q => q.Id != 0).Select(q => "q_" + q.Id).ToArray();
            string sql = "";
            int No_New_Datasheet_Rowwise = 0;
            int No_New_Datasheet_Colwise = 0;
            if (Current_Row_count > Max_RowCount)
            {
                No_New_Datasheet_Rowwise = Current_Row_count / Max_RowCount;
            }
            if (Current_Col_Count > Max_ColumnCount)
            {
                No_New_Datasheet_Colwise = 1;
            }
            if (tableName == "answers")
            {
                if (No_New_Datasheet_Rowwise > 0 || No_New_Datasheet_Colwise > 0)
                {
                    workbook.Unprotect(Constants.Password);
                    Sheet = QC4Common.Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Common.Constants.SheetCodeName.Data01);
                    int SheetPosi = Sheet.Index;
                    DataTable dtatadt = GetDataSheetNamesAndPosition(workbook);
                    if (!(dtatadt.Rows.Count > 1))
                    {
                        for (int i = 0; i < No_New_Datasheet_Colwise; i++)
                        {
                            if (QC4Common.Util.ExcelUtil.GetWorkSheetBySheetName(workbook, "Data02") == null)
                            {
                                Sheet = QC4Common.Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Common.Constants.SheetCodeName.Data01);
                                Sheet.Copy(workbook.Sheets[1], Type.Missing);
                                workbook.Sheets[1].Name = "Data02";
                                workbook.Sheets[1].Move(Type.Missing, Sheet);
                                Sheet = QC4Common.Util.ExcelUtil.GetWorkSheetBySheetName(workbook, "Data02");
                                Sheet.Cells.ClearContents();
                                Sheet.Cells[3, 2].Select();
                                Sheet.Cells[3, 1].Select();
                            }

                        }
                        SheetPosi = Sheet.Index;
                        for (int i = 0; i <= No_New_Datasheet_Colwise; i++)
                        {
                            for (int j = 0; j < No_New_Datasheet_Rowwise; j++)
                            {
                                Sheet.Copy(workbook.Sheets[1], Type.Missing);
                                string newname = string.Empty;
                                SheetPosi++;
                                if (i == 0)
                                {
                                    workbook.Sheets[1].Name = newname = "Data01(" + (j + 1) + ")";
                                    workbook.Sheets[1].Move(Type.Missing, Sheet);
                                }
                                else
                                {
                                    workbook.Sheets[1].Name = newname = "Data02(" + (j + 1) + ")";
                                    workbook.Sheets[1].Move(Type.Missing, Sheet);
                                }
                                Sheet = QC4Common.Util.ExcelUtil.GetWorkSheetBySheetName(workbook, newname);
                                Sheet.Cells.ClearContents();
                                Sheet.Cells[3, 2].Select();
                                Sheet.Cells[3, 1].Select();

                            }
                        }
                    }
                    workbook.Protect(Constants.Password);
                }
                else
                {

                }

            }
            else if (tableName == "multivariate")
            {
                //workbook.Unprotect(Constants.Password);
                //Sheet = GetWorkSheetBySheetName(workbook, "Data01");
                //Sheet.Copy(workbook.Sheets[1], Type.Missing);
                //worksheet = GetWorkSheetBySheetName(workbook, "Data01 (2)");
                //worksheet.Name = "Multivariate01";
                //QC4Common.Util.ExcelUtil.ClearContents(worksheet.Cells);
                //worksheet.Move(Type.Missing, Sheet);
                //workbook.Protect(Constants.Password);
                //  Excel.Worksheet checksheet;
                if (No_New_Datasheet_Rowwise > 0 || No_New_Datasheet_Colwise > 0)
                {
                    workbook.Unprotect(Constants.Password);
                    dt = GetDataSheetNamesAndPosition(workbook);
                    if (dt.Rows.Count > 0)
                    {
                        string Lastname = dt.Rows[dt.Rows.Count - 1][1].ToString();
                        Sheet = GetWorkSheetBySheetName(workbook, Lastname);
                    }
                    else
                    {
                        Sheet = GetWorkSheetBySheetName(workbook, "Data01");
                    }

                    int SheetPosi = Sheet.Index;

                    //TODO multi  

                    if (GetWorkSheetBySheetName(workbook, "Multivariate") == null)
                    {
                        Sheet.Copy(workbook.Sheets[1], Type.Missing);
                        workbook.Sheets[1].Name = "Multivariate";
                        if (DBHelper.checkAfterProcess(workbook))
                        {
                            Sheet = QC4Common.Util.ExcelUtil.GetWorkSheetBySheetName(workbook, "Data02(Processed)");
                            if (Sheet == null)
                            {
                                Sheet = GetWorkSheetBySheetName(workbook, "Data01(Processed)");
                            }

                        }
                        workbook.Sheets[1].Move(Type.Missing, Sheet);
                        SheetPosi++;
                        Sheet = QC4Common.Util.ExcelUtil.GetWorkSheetBySheetName(workbook, "Multivariate");
                    }

                    QC4Common.Util.ExcelUtil.ClearContents(Sheet.Cells);
                    for (int i = 0; i < No_New_Datasheet_Colwise; i++)
                    {
                        if (GetWorkSheetBySheetName(workbook, "Multivariate02") == null)
                        {
                            Sheet.Copy(workbook.Sheets[1], Type.Missing);
                            workbook.Sheets[1].Name = "Multivariate02";
                           
                            workbook.Sheets[1].Move(Type.Missing, Sheet);
                            Sheet = QC4Common.Util.ExcelUtil.GetWorkSheetBySheetName(workbook, "Multivariate02");
                            Sheet.Cells.ClearContents();
                            Sheet.Cells[3, 2].Select();
                            Sheet.Cells[3, 1].Select();
                        }

                    }
                    SheetPosi = Sheet.Index;
                    for (int i = 0; i <= No_New_Datasheet_Colwise; i++)
                    {
                        for (int j = 0; j < No_New_Datasheet_Rowwise; j++)
                        {
                            Sheet.Copy(workbook.Sheets[1], Type.Missing);
                            String sheetname = string.Empty;
                            if (i == 0)
                            {
                                workbook.Sheets[1].Name = sheetname = "Multivariate01(" + (j + 1) + ")";
                                workbook.Sheets[1].Move(Type.Missing, Sheet);


                            }
                            else
                            {
                                workbook.Sheets[1].Name = sheetname = "Multivariate02(" + (j + 1) + ")";
                                workbook.Sheets[1].Move(Type.Missing, Sheet);

                            }
                            Sheet = QC4Common.Util.ExcelUtil.GetWorkSheetBySheetName(workbook, sheetname);
                            Sheet.Cells.ClearContents();
                            Sheet.Cells[3, 2].Select();
                            Sheet.Cells[3, 1].Select();

                        }
                    }
                    workbook.Protect(Constants.Password);
                }
                else
                {
                    workbook.Unprotect(Constants.Password);
                    Sheet = GetWorkSheetBySheetName(workbook, "Data01");
                    Sheet.Copy(workbook.Sheets[1], Type.Missing);
                    worksheet = GetWorkSheetBySheetName(workbook, "Data01 (2)");
                    worksheet.Name = "Multivariate";
                    QC4Common.Util.ExcelUtil.ClearContents(worksheet.Cells);

                    if (DBHelper.checkAfterProcess(workbook))
                    {
                        Sheet = GetWorkSheetBySheetName(workbook, "Data01(Processed)");

                        /* QC4Common.Util.ExcelUtil.DeleteDataAfterSheets(workbook);
                         workbook.Unprotect(Constants.Password);
                         Sheet = GetWorkSheetBySheetName(workbook, "Data01");
                         Sheet.Copy(workbook.Sheets[1], Type.Missing);
                         worksheet = GetWorkSheetBySheetName(workbook, "Data01 (2)");
                         worksheet.Name = "Data01(Processed)";
                         QC4Common.Util.ExcelUtil.ClearContents(worksheet.Cells);
                         worksheet.Move(Type.Missing, Sheet);
                         Common.CommonFlag.SetIsDataAfterUpdated(workbook, false);*/
                    }
                    worksheet.Move(Type.Missing, Sheet);
                    workbook.Protect(Constants.Password);
                }
            }
            else
            {
                Excel.Worksheet checksheet;
                if (No_New_Datasheet_Rowwise > 0 || No_New_Datasheet_Colwise > 0)
                {
                    workbook.Unprotect(Constants.Password);
                    dt = GetDataSheetNamesAndPosition(workbook);
                    if (dt.Rows.Count > 0)
                    {
                        string Lastname = dt.Rows[dt.Rows.Count - 1][1].ToString();
                        Sheet = GetWorkSheetBySheetName(workbook, Lastname);
                    }
                    else
                    {
                        Sheet = GetWorkSheetBySheetName(workbook, "Data01");
                    }

                    int SheetPosi = Sheet.Index;
                    if (GetWorkSheetBySheetName(workbook, "Data01(Processed)") == null)
                    {
                        Sheet.Copy(workbook.Sheets[1], Type.Missing);
                        workbook.Sheets[1].Name = "Data01(Processed)";
                        workbook.Sheets[1].Move(Type.Missing, Sheet);
                        SheetPosi++;
                        Sheet = QC4Common.Util.ExcelUtil.GetWorkSheetBySheetName(workbook, "Data01(Processed)");
                    }

                    QC4Common.Util.ExcelUtil.ClearContents(Sheet.Cells);
                    for (int i = 0; i < No_New_Datasheet_Colwise; i++)
                    {
                        if (GetWorkSheetBySheetName(workbook, "Data02(Processed)") == null)
                        {
                            Sheet.Copy(workbook.Sheets[1], Type.Missing);
                            workbook.Sheets[1].Name = "Data02(Processed)";
                            workbook.Sheets[1].Move(Type.Missing, Sheet);
                            Sheet = QC4Common.Util.ExcelUtil.GetWorkSheetBySheetName(workbook, "Data02(Processed)");
                            Sheet.Cells.ClearContents();
                            Sheet.Cells[3, 2].Select();
                            Sheet.Cells[3, 1].Select();
                        }

                    }
                    SheetPosi = Sheet.Index;

                    for (int i = 0; i <= No_New_Datasheet_Colwise; i++)
                    {
                        for (int j = 0; j < No_New_Datasheet_Rowwise; j++)
                        {
                            Sheet.Copy(workbook.Sheets[1], Type.Missing);
                            String sheetname = string.Empty;
                            if (i == 0)
                            {
                                workbook.Sheets[1].Name = sheetname = "Data01(" + (j + 1) + ")" + "(Processed)";
                                workbook.Sheets[1].Move(Type.Missing, Sheet);


                            }
                            else
                            {
                                workbook.Sheets[1].Name = sheetname = "Data02(" + (j + 1) + ")" + "(Processed)";
                                workbook.Sheets[1].Move(Type.Missing, Sheet);

                            }
                            Sheet = QC4Common.Util.ExcelUtil.GetWorkSheetBySheetName(workbook, sheetname);
                            Sheet.Cells.ClearContents();
                            Sheet.Cells[3, 2].Select();
                            Sheet.Cells[3, 1].Select();

                        }
                    }
                    workbook.Protect(Constants.Password);
                }
                else
                {
                    workbook.Unprotect(Constants.Password);
                    Sheet = GetWorkSheetBySheetName(workbook, "Data01");
                    Sheet.Copy(workbook.Sheets[1], Type.Missing);
                    worksheet = GetWorkSheetBySheetName(workbook, "Data01 (2)");
                    worksheet.Name = "Data01(Processed)";
                    QC4Common.Util.ExcelUtil.ClearContents(worksheet.Cells);
                    worksheet.Move(Type.Missing, Sheet);
                    workbook.Protect(Constants.Password);
                }
            }
            workbook.Application.EnableEvents = true;


        }
        public static void GererateData02ForQC3(Excel.Workbook workbook)
        {

            workbook.Unprotect(Constants.Password);

            Excel.Worksheet Sheet = ExcelUtil.GetWorkSheetByCodeName(workbook, Constants.SheetCodeName.Data01);
            Sheet.Copy(workbook.Sheets[1], Type.Missing);
            Excel.Worksheet Data02Sheet = workbook.Sheets[1];
            Data02Sheet.Name = "Data02";
            Data02Sheet.Move(Type.Missing, Sheet);
            Data02Sheet.Cells.ClearContents();
            Excel.Range cell = Data02Sheet.Cells[3, 2];
            cell.Select();
            cell = Data02Sheet.Cells[3, 1];
            cell.Select();
            workbook.Protect(Constants.Password);
        }
        public static DataTable GetDataSheetNamesAndPosition(Excel.Workbook workbook)
        {

            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("Sheetposi");
            dt.Columns.Add("Name");
            foreach (Excel.Worksheet sheet in workbook.Worksheets)
            {
                if (sheet.Name.StartsWith("Data") && sheet.Visible == Excel.XlSheetVisibility.xlSheetVisible)
                {
                    if (!sheet.Name.Contains("Process"))
                    {
                        dr = dt.NewRow();
                        dr["Sheetposi"] = sheet.Index;
                        dr["Name"] = sheet.Name;
                        dt.Rows.Add(dr);
                    }
                }
            }

            return dt;
        }
        public static DataTable GetDataAfterSheetNamesAndPosition(Excel.Workbook workbook)
        {

            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("Sheetposi");
            dt.Columns.Add("Name");
            foreach (Excel.Worksheet sheet in workbook.Worksheets)
            {
                if (sheet.Name.StartsWith("Data") && sheet.Visible == Excel.XlSheetVisibility.xlSheetVisible)
                {
                    if (sheet.Name.Contains("Processed"))
                    {
                        dr = dt.NewRow();
                        dr["Sheetposi"] = sheet.Index;
                        dr["Name"] = sheet.Name;
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }
        public static void DeleteDataAfterSheets(Excel.Workbook workbook)
        {
            workbook.Unprotect(Constants.Password);
            DataTable dt = GetDataAfterSheetNamesAndPosition(workbook);
            Excel.Worksheet Sheet;
            if (dt.Rows.Count > 0)
            {
                workbook.Application.DisplayAlerts = false;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Sheet = ExcelUtil.GetWorkSheetBySheetName(workbook, dt.Rows[i][1].ToString());
                    Sheet.Delete();
                }
                workbook.Application.DisplayAlerts = true;
            }
            workbook.Protect(Constants.Password);
        }
        public static void ClearDatasheetContent(Excel.Workbook workbook)
        {
            DataTable dt = GetDataSheetNamesAndPosition(workbook);
            Excel.Worksheet Sheet;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Sheet = Sheet = ExcelUtil.GetWorkSheetBySheetName(workbook, dt.Rows[i][1].ToString());
                    ClearContents(Sheet.Cells);
                }

            }
        }
        public static void ClearDataafterSheetContent(Excel.Workbook workbook)
        {
            DataTable dt = GetDataAfterSheetNamesAndPosition(workbook);
            Excel.Worksheet Sheet;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Sheet = Sheet = ExcelUtil.GetWorkSheetBySheetName(workbook, dt.Rows[i][1].ToString());
                    ClearContents(Sheet.Cells);
                }

            }
        }
        public static void SetChoices(Excel.Workbook workbook)
        {
            try
            {
                int i;

                Excel.Worksheet sheet = GetWorkSheetByCodeName(workbook, Constants.SheetCodeName.Setting);
                Excel.Range start = sheet.Cells[1, 1];
                Excel.Range end = sheet.Cells[1002, 1];
                Excel.Range r = sheet.Range[start, end];
                var values = r.Value;
                for (i = 1; i <= 1000; i++)
                {
                    values[i, 1] = i;
                }
                values[Constants.PRO_DP.DP_DK_ROW, 1] = Constants.PRO_DP.DP_DK;
                values[Constants.PRO_DP.DP_ASTERISK_ROW, 1] = Constants.PRO_DP.DP_ASTERISK;
                r.Value = values;
            }
            catch { }
        }
        public static void Data02validation(Excel.Workbook workbook, List<QC4Common.Model.QuestionSettings> questions)
        {
           
            Excel.Worksheet Data02sheet = GetWorkSheetBySheetName(workbook, "Data02");
            if (Data02sheet != null)
            {
                if (questions.Count <= QC4Common.Common.Constants.ExcelRowColumnMax.ExcelMaxCol)
                {
                    workbook.Unprotect(Constants.Password);//#207864	
                    Data02sheet.Application.DisplayAlerts = false;
                    Data02sheet.Delete();
                    Data02sheet.Application.DisplayAlerts = true;
                    workbook.Protect(Constants.Password);//#207864	
                }
            }
            

        }
        public static void SetCellColor(Microsoft.Office.Interop.Excel.Worksheet sheet, string answertype, int RowNo)
        {
            if (answertype == Constants.AnswerType.MA)
            {
                Microsoft.Office.Interop.Excel.Range r1 = sheet.Cells[RowNo, Constants.QS.QsColCount];
                Microsoft.Office.Interop.Excel.Range r2 = sheet.Cells[RowNo, Constants.QS.QsColNumberSubTotal];
                QC4Common.Util.ExcelUtil.SetCellInteriorColor(sheet.get_Range(r1, r2), Constants.Color.LightGrey);
            }
            if (answertype == Constants.AnswerType.SA)
            {
                Microsoft.Office.Interop.Excel.Range r1 = sheet.Cells[RowNo, Constants.QS.QsColAddSunTotal];
                Microsoft.Office.Interop.Excel.Range r2 = sheet.Cells[RowNo, Constants.QS.QsColNumberSubTotal];
                QC4Common.Util.ExcelUtil.SetCellInteriorColor(sheet.get_Range(r1, r2), Constants.Color.LightGrey);
            }


        }
        public static void SetChoiceCellsColor(Microsoft.Office.Interop.Excel.Worksheet sheet, int choiceCount, int RowNo)
        {
            Microsoft.Office.Interop.Excel.Range r1 = sheet.Cells[RowNo, Constants.QS.QsColChoiceBegin];
            Microsoft.Office.Interop.Excel.Range r2 = sheet.Cells[RowNo, Constants.QS.QsColChoiceBegin + choiceCount - 1];
            QC4Common.Util.ExcelUtil.SetCellInteriorColor(sheet.get_Range(r1, r2), Constants.Color.LightGrey);
        }
        public static void ResetChoiceCellsColor(Microsoft.Office.Interop.Excel.Worksheet sheet, int choiceCount, int RowNo)
        {
            if (choiceCount != 1000)
            {
                Microsoft.Office.Interop.Excel.Range r1 = sheet.Cells[RowNo, Constants.QS.QsColChoiceBegin + choiceCount];
                Microsoft.Office.Interop.Excel.Range r2 = sheet.Cells[RowNo, Constants.QS.QsColChoiceEnd];
                QC4Common.Util.ExcelUtil.SetCellInteriorColor(sheet.get_Range(r1, r2), Constants.Color.White);
            }
        }
        public static void DeleteWeightBack(Excel.Workbook workbook, string variable)
        {
            Excel.Worksheet s = Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Constants.SheetCodeName.Setting);
            Excel.Range Variable_cell = s.Cells[2, 10];
            Excel.Range r = s.Range[Variable_cell, Variable_cell];
            Excel.Range result = r.Find(What: variable, LookAt: Excel.XlLookAt.xlWhole);
            while (result != null)
            {
                Excel.Range start = s.Cells[2, 10];
                Excel.Range end = s.Cells[2, 12];
                Excel.Range area = s.Range[start, end];
                area.ClearContents();
                start = s.Cells[3, 9];
                end = s.Cells[1048576, 12];
                area = s.Range[start, end];
                area.ClearContents();
                result = null;
            }

        }
        public static void CheckWeightBackValidity(Excel.Workbook workbook, List<QC4Common.Model.QuestionSettings> questions)
        {

            Excel.Worksheet s = Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Constants.SheetCodeName.Setting);
            string val = s.Cells[2, 10].Value;
            if (val != null)
            {
                int i;
                for (i = 0; i < questions.Count; i++)
                {
                    if (val == questions[i].Variable)
                    {
                        break;
                    }
                }
                if (i >= questions.Count)
                {
                    DeleteWeightBack(workbook, val);
                }
                else
                {
                    if (questions[i].Variable == val && questions[i].AnswerType != QC4Common.Common.Constants.AnswerType.SA)
                    {
                        DeleteWeightBack(workbook, val);
                    }
                }
            }
        }
        public static void SetReadonlyMode(Excel.Workbook workbook, bool Read_Only)
        {
            try
            {
                Excel.Worksheet setting = Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Constants.SheetCodeName.Setting);
                Excel.Range ReadMode = setting.Cells[QC4Common.Common.Constants.FileOpendMode.File_Read_Only_Row, QC4Common.Common.Constants.FileOpendMode.File_Read_Only_Column];

                string data = string.Empty;
                if (Read_Only)
                {
                    ReadMode.Value = "1";
                }
                else
                {
                    ReadMode.Value = "0";
                }
            }
            catch { }
        }

        public static string GetReadonlyMode(Excel.Workbook workbook)
        {
            try
            {
                Excel.Worksheet setting = Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Constants.SheetCodeName.Setting);
                Excel.Range ReadMode = setting.Cells[QC4Common.Common.Constants.FileOpendMode.File_Read_Only_Row, QC4Common.Common.Constants.FileOpendMode.File_Read_Only_Column];
                string val = Convert.ToString(ReadMode.Value);
                return val;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public static DataTable GetDataMultivariateNamesAndPosition(Excel.Workbook workbook)
        {

            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("Sheetposi");
            dt.Columns.Add("Name");
            foreach (Excel.Worksheet sheet in workbook.Worksheets)
            {
                if (sheet.Name.StartsWith("Multivariate") && sheet.Visible == Excel.XlSheetVisibility.xlSheetVisible)
                {
                    if (!sheet.Name.Contains("Process"))
                    {
                        dr = dt.NewRow();
                        dr["Sheetposi"] = sheet.Index;
                        dr["Name"] = sheet.Name;
                        dt.Rows.Add(dr);
                    }
                }
            }

            return dt;
        }
        public static void DeleteMultivariateSheets(Excel.Workbook workbook)
        {
            workbook.Unprotect(Constants.Password);
            DataTable dt = GetDataMultivariateNamesAndPosition(workbook);
            Excel.Worksheet Sheet;
            if (dt.Rows.Count > 0)
            {
                workbook.Application.DisplayAlerts = false;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Sheet = ExcelUtil.GetWorkSheetBySheetName(workbook, dt.Rows[i][1].ToString());
                    Sheet.Delete();
                }
                workbook.Application.DisplayAlerts = true;
            }
            workbook.Protect(Constants.Password);
        }

        public static void DeleteFromMultiVariateSheet(Excel.Workbook workbook, string variable)
        {
            Excel.Worksheet s = QC4Common.Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Constants.SheetCodeName.MultiVariateSheet);
            Excel.Range Variable_cell = s.Cells[5, 137];
            Excel.Range Variable_cell2 = s.Cells[1048573, 237];
            Excel.Range r = s.Range[Variable_cell, Variable_cell2];
            Excel.Range reslt = r.Find(What: variable, LookAt: Excel.XlLookAt.xlWhole);
            while (reslt != null)
            {
                reslt.Cells.Replace(variable, "");
                reslt = null;
            }
        }
    }
}
