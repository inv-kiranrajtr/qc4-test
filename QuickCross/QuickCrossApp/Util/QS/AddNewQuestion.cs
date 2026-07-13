using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Reflection;
using System.Windows.Forms.VisualStyles;

namespace Qc4Launcher.Util.QS
{
    class AddNewQuestion
    {
        string newvariable = string.Empty;
        Microsoft.Office.Interop.Excel.Workbook excelWorkBook = null;
        string temppath = string.Empty;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public AddNewQuestion(Microsoft.Office.Interop.Excel.Workbook workBook,string temppath)
        {
            this.excelWorkBook = workBook;
            this.temppath = temppath;
        }

        public bool SaveToSheet(string variable, string answertype, string tableHeading = null, string question = null, int choiceindex = 0, int no_choice_index = -1, DataTable data = null,bool macn=false,DataTable subtotalData=null, string addsubtotal = "")
        {
            bool isSaved = false;
            try
            {
                if (Util.Definiotion.VariableDictionary.Count < QC4Common.Common.Constants.ExcelRowColumnMax.ExcelQsMaxRowLimit)
                {
                    Microsoft.Office.Interop.Excel.Worksheet sheet = Util.ExcelUtil.GetWorkSheetByCodeName(excelWorkBook, Util.Constants.SheetCodeName.QuestionSetting);
                    if (answertype == Constants.AnswerType.FA || answertype == Constants.AnswerType.N || answertype == Constants.AnswerType.D)
                    {
                        isSaved = SaveFA_N_type(sheet, variable, answertype, tableHeading, question);
                    }
                    else if (answertype == Constants.AnswerType.SA)
                    {
                        isSaved = SaveSA_type(sheet, variable, answertype, tableHeading, question, choiceindex, no_choice_index, data);
                    }
                    else if (answertype == Constants.AnswerType.MA)
                    {
                        isSaved = SaveMA_type(sheet, variable, answertype, tableHeading, question, choiceindex, no_choice_index, data, macn);
                    }
                    if (isSaved)
                    {
                        QC4Common.Common.GTAutoSetting.LoadNewDataToGTHiddenSheet(excelWorkBook,
                             new List<QC4Common.Common.GTAutoSetting.VariableDT>() { new QC4Common.Common.GTAutoSetting.VariableDT { Variable = variable, Type = answertype } });
                    }
                    return isSaved;
                }
                else
                {
                    _log.LogError("QuestionSetting Error :Number of question exceed");
                    return false;

                }
            }
            catch(Exception ex)
            {
             
                string message = ex.Message;
                 _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            
            return false; }
        }
        public bool Savesubtotal(DataTable subdt = null)
        {
            try
            {
                if (subdt != null)
                {
                    Microsoft.Office.Interop.Excel.Worksheet sheet= Util.ExcelUtil.GetWorkSheetByCodeName(excelWorkBook, Util.Constants.SheetCodeName.QuestionSetting);
                    int qestionCount = Util.Definiotion.VariableDictionary.Count;
                    Microsoft.Office.Interop.Excel.Range r = sheet.Cells[qestionCount + 3, 1016];
                    r.Value = 1;
                    r= sheet.Cells[qestionCount + 3, 1017];
                    r.Value = subdt.Rows.Count;
                    sheet.Application.EnableEvents = false;
                    int choicebegin = 1018;
                    int valuebegin = 1019;
                    for(int i=0;i< subdt.Rows.Count;i++,choicebegin= choicebegin + 2,valuebegin= valuebegin +2)
                    {
                        r = sheet.Cells[qestionCount + 3, choicebegin];
                        r.Value = subdt.Rows[i][1];
                        r = sheet.Cells[qestionCount + 3, valuebegin];
                        r.Value = subdt.Rows[i][0];

                    }
                    Util.Definiotion.VariableDictionary = QC4Common.Util.DictionaryUtil.PopulateQSDictionary(excelWorkBook);
                    sheet.Application.EnableEvents = true;
                    return true;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                 _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            
            return false; };
        }
        private bool SaveFA_N_type(Microsoft.Office.Interop.Excel.Worksheet sheet, string variable, string answertype, string tableHeading = null, string question=null)
        {
            try
            {
                int qestionCount = Util.Definiotion.VariableDictionary.Count;
                QC4Common.Model.QuestionSettings questionSettings = new QC4Common.Model.QuestionSettings();
                sheet.Application.EnableEvents = false;
                Microsoft.Office.Interop.Excel.Range r;
                Microsoft.Office.Interop.Excel.Range start = sheet.Cells[qestionCount + 4, 1];
                Microsoft.Office.Interop.Excel.Range end = sheet.Cells[qestionCount + 4, 12];
                Microsoft.Office.Interop.Excel.Range Fa_n_range = sheet.Range[start, end];
                var values = Fa_n_range.Value;
                questionSettings.QuestionFlagUpdated = questionSettings.QuestionFlag=values[1, 2] = "New";
                questionSettings.Variable = questionSettings.VariableBefore = values[1, 6] = variable;
                questionSettings.AnswerTypeBefore = questionSettings.AnswerType = values[1, 7] = answertype;
                questionSettings.TableHeading =values[1, 11] = tableHeading;
                questionSettings.Question=values[1, 12] = question;
                Fa_n_range.Value = values;
                DB.QuestionSettingDao questionSetting = new DB.QuestionSettingDao(QC4Common.DB.DBHelper.GetConnectionString(excelWorkBook));
                questionSetting.insertQuestioninForm(variable, answertype, "New",sheet:sheet,Rownumber: qestionCount + 4);
                
                int id = questionSetting.GetvariableId(variable);
                start = sheet.Cells[qestionCount + 4, 1];
                end = sheet.Cells[qestionCount + 4, 16348];
                r = sheet.Range[start, end];
              
                questionSettings.Id = questionSettings.ItemId = id;
                questionSettings.RowNumber = qestionCount + 4;
                questionSettings.IsFound = true;
                questionSettings.IsNew = false;
                questionSettings.QuestionCount = 0;
                questionSettings.SeriallNumber = 0;
                questionSettings.AddSubTotal = "";
                 Util.Definiotion.VariableDictionary = QC4Common.Util.DictionaryUtil.PopulateQSDictionary(excelWorkBook);
                new QC4Common.Sheets.ListUpdate(excelWorkBook).UpdateListSheet(Util.Definiotion.VariableDictionary.Select(q => q.Value).ToList());
                sheet.Application.EnableEvents = true;
                return true;
            }
            catch (Exception ex) {
              string message = ex.Message;
                 _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            
            return false; }
           
        }
        private bool SaveSA_type(Microsoft.Office.Interop.Excel.Worksheet sheet, string variable, string answertype, string tableHeading = null, string question = null, int choiceindex = 0, int no_choice_index = -1, DataTable data = null,string addsubtotal="")
        {
          
            try
            {
                int qestionCount = Util.Definiotion.VariableDictionary.Count;
                QC4Common.Model.QuestionSettings questionSettings = new QC4Common.Model.QuestionSettings();
                sheet.Application.EnableEvents = false;
                Microsoft.Office.Interop.Excel.Range start = sheet.Cells[qestionCount + 4, 1];
                Microsoft.Office.Interop.Excel.Range end = sheet.Cells[qestionCount + 4, 16348];
                Microsoft.Office.Interop.Excel.Range r = sheet.Range[start, end];
                var values = r.Value;
                questionSettings.QuestionFlagUpdated = questionSettings.QuestionFlag = values[1, 2] = "New";
                questionSettings.Variable = questionSettings.VariableBefore = values[1, 6] = variable;
                questionSettings.AnswerTypeBefore = questionSettings.AnswerType = values[1, 7] = answertype;
                SetCellColor(sheet, answertype, qestionCount + 4);
                sheet.Cells[qestionCount + 4,8].Value= choiceindex.ToString();
                SetChoiceCellsColor(sheet, choiceindex, qestionCount + 4);
                values[1, 8] = choiceindex.ToString();
                questionSettings.CategoryCount = questionSettings.CategoryCountBefore = choiceindex;
                if (data.Columns.Count > 2)
                {
                    questionSettings.Score = values[1, 9] = getWTstring(data, choiceindex);
                }
                if (no_choice_index != -1)
                {
                    if (no_choice_index > choiceindex)
                    {
                        values[1, 10] = choiceindex;
                        questionSettings.Sort = choiceindex.ToString();
                    }
                    else
                    {
                        values[1, 10] = no_choice_index + 1;
                        questionSettings.Sort = (no_choice_index + 1).ToString();

                    }
                }
                if (addsubtotal == "")
                {
                    values[1, 1016] = string.Empty;
                }
                else
                {
                    values[1, 1016] = "1";
                }

                questionSettings.TableHeading = values[1, 11] = tableHeading;
                questionSettings.Question = values[1, 12] = question;
                for (int i = 0, colcount = 13; i < choiceindex; i++, colcount++)
                {
                    values[1, colcount] = UnEscapeCRLF(data.Rows[i][1].ToString());
                    questionSettings.Choices.Add(data.Rows[i][1].ToString());
                }
     
                r.Value = values;
           
                DB.QuestionSettingDao questionSetting = new DB.QuestionSettingDao(QC4Common.DB.DBHelper.GetConnectionString(excelWorkBook));
                questionSetting.insertQuestioninForm(variable, answertype, "New", choiceindex, sheet: sheet, Rownumber: qestionCount + 4);
                int id = questionSetting.GetvariableId(variable);
                questionSettings.Id = questionSettings.ItemId = id;
                questionSettings.RowNumber = qestionCount + 4;
                questionSettings.IsFound = true;
                questionSettings.IsNew = false;
                questionSettings.QuestionCount = 0;
                questionSettings.SeriallNumber = 0;
                questionSettings.AddSubTotal = "";
                Util.Definiotion.VariableDictionary.Add(variable, questionSettings);
                new QC4Common.Sheets.ListUpdate(excelWorkBook).UpdateListSheet(Util.Definiotion.VariableDictionary.Select(q => q.Value).ToList());
                sheet.Application.EnableEvents = true;
                return true;

            }
            catch(Exception ex) {

                string message = ex.Message;
                 _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            
            return false; }

        }

        private bool SaveMA_type(Microsoft.Office.Interop.Excel.Worksheet sheet, string variable, string answertype, string tableHeading = null, string question = null, int choiceindex = 0, int no_choice_index = -1, DataTable data = null,bool macn=false,string addsubtotal="")
        {
            try
            {
                int qestionCount = Util.Definiotion.VariableDictionary.Count;
                QC4Common.Model.QuestionSettings questionSettings = new QC4Common.Model.QuestionSettings();
                sheet.Application.EnableEvents = false;
                Microsoft.Office.Interop.Excel.Range start = sheet.Cells[qestionCount + 4, 1];
                Microsoft.Office.Interop.Excel.Range end = sheet.Cells[qestionCount + 4, 16348];
                Microsoft.Office.Interop.Excel.Range r = sheet.Range[start, end];
                var values = r.Value;
                questionSettings.QuestionFlag = questionSettings.QuestionFlagUpdated = values[1, 2] = "New";
                questionSettings.Variable = questionSettings.VariableBefore = values[1, 6] = variable;
                sheet.Cells[qestionCount + 4, 7].Value = answertype;
                SetCellColor(sheet, answertype, qestionCount + 4);
                questionSettings.AnswerType = questionSettings.AnswerTypeBefore = values[1, 7] = answertype;
                sheet.Cells[qestionCount + 4, 8].Value = choiceindex.ToString();
                SetChoiceCellsColor(sheet, choiceindex,qestionCount + 4);
                values[1, 8] = choiceindex.ToString();
                questionSettings.CategoryCount = choiceindex;
                if (data.Columns.Count > 2)
                {
                    questionSettings.Score = values[1, 9] = getWTstring(data, choiceindex);
                }
                if (no_choice_index != -1)
                {
                    if (no_choice_index > choiceindex)
                    {
                        values[1, 10] = choiceindex;
                        questionSettings.Sort = choiceindex.ToString();
                    }
                    else
                    {
                        values[1, 10] = no_choice_index + 1;
                        questionSettings.Sort = (no_choice_index + 1).ToString();
                    }
                }
                questionSettings.TableHeading = values[1, 11] = tableHeading;
                questionSettings.Question = values[1, 12] = question;
                for (int i = 0, colcount = 13; i < choiceindex; i++, colcount++)
                {
                    values[1, colcount] = UnEscapeCRLF(data.Rows[i][1].ToString());
                    questionSettings.Choices.Add(data.Rows[i][1].ToString());
                }
                if (data.Columns.Count > 3)
                {
                    questionSettings.Count = values[1, 1014] = getCountMean(data, choiceindex);
                }
                if (macn)
                {
                    questionSettings.CountBase = values[1, 1015] = "Lower";
                }
                if (addsubtotal == "")
                {
                    values[1, 1016] = string.Empty;
                }
                else
                {
                    values[1, 1016] = "1";
                }
                r.Value = values;
               
                DB.QuestionSettingDao questionSetting = new DB.QuestionSettingDao(QC4Common.DB.DBHelper.GetConnectionString(excelWorkBook));
                questionSetting.insertQuestioninForm(variable, answertype, "New", choiceindex, sheet: sheet, Rownumber: qestionCount + 4);
                int id = questionSetting.GetvariableId(variable);
                questionSettings.Id = questionSettings.ItemId = id;
                questionSettings.RowNumber = qestionCount + 4;
                questionSettings.IsFound = true;
                questionSettings.IsNew = false;
                questionSettings.QuestionCount = 0;
                questionSettings.SeriallNumber = 0;
                questionSettings.AddSubTotal = "";
                Definiotion.VariableDictionary.Add(variable, questionSettings);
                sheet.Application.EnableEvents = false;
                new QC4Common.Sheets.ListUpdate(excelWorkBook).UpdateListSheet(Util.Definiotion.VariableDictionary.Select(q => q.Value).ToList());
                sheet.Application.EnableEvents = true;
                return true;
            }
            catch(Exception ex) {
              
                string message = ex.Message;
                 _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            
            return false; }

        }

        private string getWTstring(DataTable dt,int choiceindx)
        {
            string value = string.Empty;
            int j = 0;
            for(int i=0;i< choiceindx; i++)
            {
                if (i == 0)
                {
                    if (dt.Rows[i][2].ToString() == "")
                    {
                        j++;
                    }
                    else
                    {
                        value = dt.Rows[i][2].ToString();
                    }
                }
                else
                {
                    if (dt.Rows[i][2].ToString() == "")
                    {
                        j++;
                        value += "," + dt.Rows[i][2].ToString();
                    }
                    else
                    {
                        value += ","+dt.Rows[i][2].ToString();
                    }
                }
            }
            if (value.Replace(",", string.Empty).Length < 1)
            {
                return string.Empty;
            }
            else
            {
                return value;
            }
            

        }
        private string getCountMean(DataTable dt, int choiceindex)
        {
            try
           {
              
                int[] a = new int[choiceindex+1];
                int j = 0;
                for (int i = 0; i < choiceindex; i++)
                {
                    if (dt.Rows[i][3].ToString() == "True")
                    {
                        a[j] = i + 1;
                        j++;
                    }
                }

                string startValue = a[0].ToString();
                string val1 = "";
                string val2 = startValue;
                for (int k = 0; k < j; k++)
                {
                  List<int> startVal = new List<int>();
                    int s = k;
                    for (int i = s; i < choiceindex && a[i] + 1 == a[k + 1]; i++, k++)
                    {
                        startVal.Add(a[k]);
                    }
                    int endVal = a[k];
                    if (startVal.Count > 0)
                    {
                        if (val1 == "")
                            val1 = startVal[0] + "-" + endVal;
                        else
                            val1 += "/" + startVal[0] + "-" + endVal;
                    }
                    else
                    {
                        if (val1 == "")
                            val1 += endVal;
                        else
                            val1 += "/" + endVal;
                    }

                  
                }
                return val1;
           }
            catch (Exception ex) {
             string message = ex.Message;
                 _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            
            return string.Empty; }
        }



        






        public bool EditToSheet(string variable,string answertype,string tableHeading = null, string question = null, int choiceindex = 0, int no_choice_index = -1, DataTable data = null, bool macn = false,int selectedIndex=0,bool matrixvariable_parant=false,int matrixvariable_parant_child_count=0, string addsubtotal = "")
        {
            bool isSaved = false;
            try
            {
                selectedIndex = Util.Definiotion.VariableDictionary[variable].RowNumber-4;
                Microsoft.Office.Interop.Excel.Worksheet sheet = Util.ExcelUtil.GetWorkSheetByCodeName(excelWorkBook, Util.Constants.SheetCodeName.QuestionSetting);
                if (answertype == Constants.AnswerType.FA || answertype == Constants.AnswerType.N || answertype == Constants.AnswerType.D)
                {
                    isSaved = SaveFA_N_type_edit(sheet,tableHeading, question,selectedIndex,matrixvariable_parant,matrixvariable_parant_child_count);
                }
                else if (answertype == Constants.AnswerType.SA)
                {
                    isSaved = SaveSA_type_edit(sheet,tableHeading,question,choiceindex,no_choice_index,data,selectedIndex,matrixvariable_parant,matrixvariable_parant_child_count,addsubtotal);
                }
                else if (answertype == Constants.AnswerType.MA)
                {
                    isSaved = SaveMA_type_edit(sheet,tableHeading,question,choiceindex,no_choice_index,data,macn,selectedIndex,matrixvariable_parant,matrixvariable_parant_child_count,addsubtotal);
                }
                return isSaved;
            }
            catch (Exception ex)
            { 
                string message = ex.Message;
                 _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            
            return false; }
        }
        private bool SaveFA_N_type_edit(Microsoft.Office.Interop.Excel.Worksheet sheet,string tableHeading = null, string question = null,int selectedIndex=0,bool matrixvariable_parant= false, int matrixvariable_parant_child_count = 0)
        {
            try
            {
                sheet.Application.EnableEvents = false;
                Microsoft.Office.Interop.Excel.Range r;
                if (matrixvariable_parant)
                {
                    for (int i = 0; i < matrixvariable_parant_child_count; i++)
                    {
                        r = sheet.Cells[(selectedIndex + 4) + i, 11];
                        r.Value = checksinglequete(tableHeading, selectedIndex);
                    }
                }
                else
                {
                    r = sheet.Cells[selectedIndex + 4, 11];
                    r.Value = checksinglequete(tableHeading, selectedIndex);
                }
                r = sheet.Cells[selectedIndex + 4, 12];
                r.Value = checksinglequete(question);
                sheet.Application.EnableEvents = true;
                Util.Definiotion.VariableDictionary = QC4Common.Util.DictionaryUtil.PopulateQSDictionary(excelWorkBook);
                new QC4Common.Sheets.ListUpdate(excelWorkBook).UpdateListSheet(Util.Definiotion.VariableDictionary.Select(q => q.Value).ToList());
                return true;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                 _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);

                return false;
            }
        }  
        private bool SaveSA_type_edit(Microsoft.Office.Interop.Excel.Worksheet sheet,string tableHeading,string question,int choiceindex,int no_choice_index,DataTable data,int selectedIndex, bool matrixvariable_parant= false, int matrixvariable_parant_child_count = 0,string addsubtotal="")
        {
            int row;

            Microsoft.Office.Interop.Excel.Range r;
            object[,] values = null;
            try
            {
                sheet.Application.EnableEvents = false;

                if (matrixvariable_parant)
                {
                    Microsoft.Office.Interop.Excel.Range start = sheet.Cells[selectedIndex + 4, 1];
                    Microsoft.Office.Interop.Excel.Range end = sheet.Cells[((selectedIndex + 4) + matrixvariable_parant_child_count) - 1, 1016];
                    r = sheet.Range[start, end];

                }
                else
                {
                    Microsoft.Office.Interop.Excel.Range start = sheet.Cells[selectedIndex + 4, 1];
                    Microsoft.Office.Interop.Excel.Range end = sheet.Cells[(selectedIndex + 4), 1016];
                    r = sheet.Range[start, end];

                }
                values = r.Value;
                if (matrixvariable_parant)
                {
                    for (int i = 0; i < matrixvariable_parant_child_count; i++)
                    {

                        values[(1 + i), 3] = Util.Definiotion.VariableDictionary.Values.ToList().ElementAt((selectedIndex + i)).QuestionNumber;
                    }
                }
                else
                {
                    values[1, 3] = Util.Definiotion.VariableDictionary.Values.ToList().ElementAt(selectedIndex).QuestionNumber;
                }
                if (matrixvariable_parant)
                {
                    for (int i = 0; i < matrixvariable_parant_child_count; i++)
                    {

                        values[(1 + i), 11] = checksinglequete(tableHeading);
                    }
                }
                else
                {

                    values[1, 11] = checksinglequete(tableHeading);
                }

                values[1, 12] = question;
                if (matrixvariable_parant)
                {
                    for (int i = 0; i < matrixvariable_parant_child_count; i++)
                    {
                        values[(1 + i), 8] = choiceindex.ToString();
                    }
                }
                else
                {

                    values[1, 8] = choiceindex.ToString();
                }


                if (matrixvariable_parant)
                {
                    for (int i = 0; i < matrixvariable_parant_child_count; i++)
                    {
                        if (no_choice_index != -1)
                        {
                            if (no_choice_index > choiceindex)
                            {
                                values[(1 + i), 10] = choiceindex;
                            }
                            else
                            {
                                values[(1 + i), 10] = no_choice_index + 1;
                            }
                        }
                        else
                        {
                            values[(1 + i), 10] = null;
                        }
                    }
                }
                else
                {


                    if (no_choice_index != -1)
                    {
                        if (no_choice_index > choiceindex)
                        {
                            values[1, 10] = choiceindex;
                        }
                        else
                        {
                            values[1, 10] = no_choice_index + 1;
                        }
                    }
                    else
                    {
                        values[1, 10] = null;
                    }
                }


                if (matrixvariable_parant)
                {
                    for (int i = 0; i < matrixvariable_parant_child_count; i++)
                    {

                        if (data.Columns.Count > 2)
                        {
                            values[(1 + i), 9] = getWTstring(data, choiceindex);
                        }
                    }
                }
                else
                {

                    if (data.Columns.Count > 2)
                    {
                        values[1, 9] = getWTstring(data, choiceindex);
                    }
                }


                if (matrixvariable_parant)
                {
                    for (int i = 0; i < matrixvariable_parant_child_count; i++)
                    {

                        for (int ii = 0, colcount = 13; ii < choiceindex; ii++, colcount++)
                        {
                            values[(1 + i), colcount] = UnEscapeCRLF(checksinglequete(data.Rows[ii][1].ToString()));
                        }
                        SetChoiceCellsColor(sheet, choiceindex, ((selectedIndex + 4) + i));
                        for (int ii = choiceindex, colcount = 13 + choiceindex; ii < (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedIndex).CategoryCount; ii++, colcount++)
                        {
                            values[(1 + i), colcount] = null;
                        }
                        ResetChoiceCellsColor(sheet, choiceindex, ((selectedIndex + 4) + i));
                    }

                }
                else
                {
                    for (int i = 0, colcount = 13; i < choiceindex; i++, colcount++)
                    {
                        values[1, colcount] = UnEscapeCRLF(checksinglequete(data.Rows[i][1].ToString()));
                    }
                    SetChoiceCellsColor(sheet, choiceindex, (selectedIndex + 4));
                    for (int i = choiceindex, colcount = 13 + choiceindex; i < (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedIndex).CategoryCount; i++, colcount++)
                    {
                        values[1, colcount] = null;
                    }
                    ResetChoiceCellsColor(sheet, choiceindex, (selectedIndex + 4));
                }
               
                if (matrixvariable_parant)
                {
                    for (int i = 0; i < matrixvariable_parant_child_count; i++)
                    {

                        if (addsubtotal == "")
                        {
                            values[(1 + i), 1016] = string.Empty;
                        }
                        else
                        {
                            values[(1 + i), 1016] = "1";
                        }
                    }
                }
                else
                {

                    if (addsubtotal == "")
                    {
                        values[1, 1016] = string.Empty;
                    }
                    else
                    {
                        values[1, 1016] = "1";
                    }
                }






               
                r.Value = values;
                sheet.Application.EnableEvents = true;
                DB.QuestionSettingDao questionSetting = new DB.QuestionSettingDao(QC4Common.DB.DBHelper.GetConnectionString(excelWorkBook));
                if (matrixvariable_parant)
                {
                    for (int i = 0; i < matrixvariable_parant_child_count; i++)
                    {
                        row = selectedIndex + i;
                        questionSetting.UpdateQuestioninForm((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(row).Variable, choiceindex);
                    }
                }
                else
                {
                    questionSetting.UpdateQuestioninForm((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedIndex).Variable, choiceindex);
                }
                Util.Definiotion.VariableDictionary = QC4Common.Util.DictionaryUtil.PopulateQSDictionary(excelWorkBook);
                new QC4Common.Sheets.ListUpdate(excelWorkBook).UpdateListSheet(Util.Definiotion.VariableDictionary.Select(q => q.Value).ToList());

                return true;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                 _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);

                return false;
            }
           
        }
        private bool SaveMA_type_edit(Microsoft.Office.Interop.Excel.Worksheet sheet,string tableHeading=null, string question = null, int choiceindex = 0, int no_choice_index = 0, DataTable data = null, bool macn = false, int selectedIndex = 0,bool matrixvariable_parant= false, int matrixvariable_parant_child_count = 0,string addsubtotal="")
        {
            int row;
          
            Microsoft.Office.Interop.Excel.Range r;
            object[,] values = null;
            try
            {
                sheet.Application.EnableEvents = false;
               
                if (matrixvariable_parant)
                {
                    Microsoft.Office.Interop.Excel.Range start = sheet.Cells[selectedIndex + 4, 1];
                    Microsoft.Office.Interop.Excel.Range end = sheet.Cells[((selectedIndex + 4)+ matrixvariable_parant_child_count)-1, 1016];
                    r = sheet.Range[start, end];
                   
                }
                else
                {
                    Microsoft.Office.Interop.Excel.Range start = sheet.Cells[selectedIndex + 4, 1];
                    Microsoft.Office.Interop.Excel.Range end = sheet.Cells[(selectedIndex + 4), 1016];
                    r = sheet.Range[start, end];
                    
                }
                values = r.Value;
                if (matrixvariable_parant)
                {
                    for (int i = 0; i < matrixvariable_parant_child_count; i++)
                    {

                        values[(1 + i), 3] = Util.Definiotion.VariableDictionary.Values.ToList().ElementAt((selectedIndex + i)).QuestionNumber;
                    }
                }
                else
                {
                    values[1, 3] = Util.Definiotion.VariableDictionary.Values.ToList().ElementAt(selectedIndex).QuestionNumber;
                }
                if (matrixvariable_parant)
                {
                    for (int i = 0; i < matrixvariable_parant_child_count; i++)
                    {
                     
                        values[(1+i),11]= checksinglequete(tableHeading);
                    }
                }
                else
                {
               
                    values[1, 11] = checksinglequete(tableHeading);
                }
           
                values[1, 12] = question;
                if (matrixvariable_parant)
                {
                    for (int i = 0; i < matrixvariable_parant_child_count; i++)
                    {
                        values[(1+i), 8] = choiceindex.ToString();
                    }
                }
                else
                {
                 
                    values[1, 8] = choiceindex.ToString();
                }


                if (matrixvariable_parant)
                {
                    for (int i = 0; i < matrixvariable_parant_child_count; i++)
                    {
                        if (no_choice_index != -1)
                        {
                            if (no_choice_index > choiceindex)
                            {
                                values[(1 + i), 10] = choiceindex;
                            }
                            else
                            {
                                values[(1 + i), 10] = no_choice_index + 1;
                            }
                        }
                        else
                        {
                            values[(1 + i), 10] = null;
                        }
                    }
                }
                else
                {


                    if (no_choice_index != -1)
                    {
                        if (no_choice_index > choiceindex)
                        {
                            values[1, 10] = choiceindex;
                        }
                        else
                        {
                            values[1, 10] = no_choice_index + 1;
                        }
                    }
                    else
                    {
                        values[1, 10] = null;
                    }
                }


                if (matrixvariable_parant)
                {
                    for (int i = 0; i < matrixvariable_parant_child_count; i++)
                    {
                    
                        if (data.Columns.Count > 2)
                        {
                            values[(1+i), 9] = getWTstring(data, choiceindex);
                        }
                    }
                }
                else
                {
                 
                    if (data.Columns.Count > 2)
                    {
                        values[1, 9] = getWTstring(data, choiceindex);
                    }
                }
              

                if (matrixvariable_parant)
                {
                    for (int i = 0; i < matrixvariable_parant_child_count; i++)
                    {

                        for (int ii = 0, colcount = 13; ii < choiceindex; ii++, colcount++)
                        {
                            values[(1 + i), colcount] = UnEscapeCRLF(checksinglequete(data.Rows[ii][1].ToString()));
                        }
                        SetChoiceCellsColor(sheet, choiceindex, ((selectedIndex + 4) + i));
                        for (int ii = choiceindex, colcount = 13 + choiceindex; ii < (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedIndex).CategoryCount; ii++, colcount++)
                        {
                            values[(1 + i), colcount] = null;
                        }
                         ResetChoiceCellsColor(sheet, choiceindex, ((selectedIndex + 4) + i));
                    }
                   
                }
                else
                {
                    for (int i = 0, colcount = 13; i < choiceindex; i++, colcount++)
                    {
                        values[1, colcount] = UnEscapeCRLF(checksinglequete(data.Rows[i][1].ToString()));
                    }
                    SetChoiceCellsColor(sheet, choiceindex,(selectedIndex + 4));
                    for (int i = choiceindex, colcount = 13 + choiceindex; i < (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedIndex).CategoryCount; i++, colcount++)
                    {
                        values[1, colcount] = null;   
                    }
                    ResetChoiceCellsColor(sheet, choiceindex, (selectedIndex + 4));
                }

                if (matrixvariable_parant)
                {
                    for (int i = 0; i < matrixvariable_parant_child_count; i++)
                    {
                      
                        if (data.Columns.Count > 3)
                        {
                            values[(1 + i), 1014] = getCountMean(data, choiceindex);
                        }
                    }
                }
                else
                {
                 
                    if (data.Columns.Count > 3)
                    {
                        values[1, 1014] = getCountMean(data, choiceindex);
                    }
                }
                if (matrixvariable_parant)
                {
                    for (int i = 0; i < matrixvariable_parant_child_count; i++)
                    {
                    
                        if (macn)
                        {
                          
                            values[(1 + i), 1015] = "Lower";
                        }
                        else
                        {
                            values[(1 + i), 1015] = null;
                        }
                    }
                }
                else
                {
                    if (macn)
                    {
                        values[1, 1015] = "Lower";
                    }
                    else
                    {
                        values[1, 1015] = null;
                    }
                }
                if (matrixvariable_parant)
                {
                    for (int i = 0; i < matrixvariable_parant_child_count; i++)
                    {
                     
                        if (addsubtotal == "")
                        {
                            values[(1 + i), 1016] = string.Empty;
                        }
                        else
                        {
                            values[(1 + i), 1016] = "1";
                        }
                    }
                }
                else
                {
                  
                    if (addsubtotal == "")
                    {
                        values[1, 1016] = string.Empty;
                    }
                    else
                    {
                        values[1, 1016] = "1";
                    }
                }






               
                r.Value = values;
                sheet.Application.EnableEvents = true;
                DB.QuestionSettingDao questionSetting = new DB.QuestionSettingDao(QC4Common.DB.DBHelper.GetConnectionString(excelWorkBook));
                if (matrixvariable_parant)
                {
                    for (int i = 0; i < matrixvariable_parant_child_count; i++)
                    {
                        row = selectedIndex + i;
                        questionSetting.UpdateQuestioninForm((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(row).Variable, choiceindex);
                    }
                }
                else
                {
                    questionSetting.UpdateQuestioninForm((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedIndex).Variable, choiceindex);
                }
                Util.Definiotion.VariableDictionary = QC4Common.Util.DictionaryUtil.PopulateQSDictionary(excelWorkBook);
                new QC4Common.Sheets.ListUpdate(excelWorkBook).UpdateListSheet(Util.Definiotion.VariableDictionary.Select(q => q.Value).ToList());
              
                return true;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                 _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);

                return false;
            }
        }
        public bool Savesubtotal_edit(int selectedIndex, DataTable subdt = null, int childcount = 0)
        {

            Microsoft.Office.Interop.Excel.Range r;
            object[,] values = null;
            int datacount = subdt.Rows.Count;
            try
            {
               
                Microsoft.Office.Interop.Excel.Worksheet sheet = Util.ExcelUtil.GetWorkSheetByCodeName(excelWorkBook, Util.Constants.SheetCodeName.QuestionSetting);
                excelWorkBook.Application.EnableEvents = false;
                if (subdt != null)
                {
                    if (childcount != 0 && childcount > 0)
                    {
                        Microsoft.Office.Interop.Excel.Range start = sheet.Cells[selectedIndex + 4, Constants.QS.QsColAddSunTotal];
                        Microsoft.Office.Interop.Excel.Range end = sheet.Cells[((selectedIndex + 4) + childcount) - 1, Constants.QS.ColEnd];
                        r = sheet.Range[start, end];
                    }
                    else
                    {
                        Microsoft.Office.Interop.Excel.Range start = sheet.Cells[selectedIndex + 4, Constants.QS.QsColAddSunTotal];
                        Microsoft.Office.Interop.Excel.Range end = sheet.Cells[(selectedIndex + 4), Constants.QS.ColEnd];
                        r = sheet.Range[start, end];
                    }
                    values = r.Value;
                    if (subdt.Rows.Count > 0)
                    {
                        if (childcount != 0)
                        {
                            for (int i = 0; i < childcount; i++)
                            {
                                values[(1 + i), 1] = 1;
                                values[(1 + i), 2] = subdt.Rows.Count;
                                SetSubtotalCellsColor(sheet, subdt.Rows.Count, ((selectedIndex+4) + i));
                                ResetSubtotalCellsColor(sheet, datacount, ((selectedIndex + 4) + i));
                            }
                        }
                        else
                        {
                            values[1, 1] = 1;
                            values[1, 2] = subdt.Rows.Count;
                            SetSubtotalCellsColor(sheet, datacount, (selectedIndex+4));
                            ResetSubtotalCellsColor(sheet, datacount, (selectedIndex+4));
                        }
                        if (childcount != 0)
                        {
                            for (int j = 0; j < childcount; j++)
                            {
                                int choicebegin = 3;
                                int valuebegin = 4;
                                for (int i = 0; i < subdt.Rows.Count; i++, choicebegin = choicebegin + 2, valuebegin = valuebegin + 2)
                                {
                                    values[(1 + j), choicebegin] = checksinglequete(subdt.Rows[i][1].ToString());
                                    values[(1 + j), valuebegin] = subdt.Rows[i][0];
                                }
                                for (int i = 0; i < 10 - subdt.Rows.Count; i++, choicebegin = choicebegin + 2, valuebegin = valuebegin + 2)
                                {
                                    values[(1 + j), choicebegin] = string.Empty;
                                    values[(1 + j), valuebegin] = string.Empty;
                                }

                            }
                        }
                        else
                        {
                            int choicebegin = 3;
                            int valuebegin = 4;
                            for (int i = 0; i < subdt.Rows.Count; i++, choicebegin = choicebegin + 2, valuebegin = valuebegin + 2)
                            {
                                values[1, choicebegin] = checksinglequete(subdt.Rows[i][1].ToString());
                                values[1, valuebegin] = subdt.Rows[i][0];
                            }
                            for (int i = 0; i < 10 - subdt.Rows.Count; i++, choicebegin = choicebegin + 2, valuebegin = valuebegin + 2)
                            {
                                values[1, choicebegin] = string.Empty;
                                values[1, valuebegin] = string.Empty;
                            }
                        }
                    }
                    else
                    {
                        if (childcount != 0)
                        {
                            for (int i = 0; i < childcount; i++)
                            {
                                values[(1 + i), 1] = string.Empty;
                                values[(1 + i), 2] = string.Empty;
                                ResetSubtotalCellsColor(sheet, 0,((selectedIndex + 4) + i));
                            }
                        }
                        else
                        {
                            values[1, 1] = string.Empty;
                            values[1, 2] = string.Empty;
                            ResetSubtotalCellsColor(sheet, 0, selectedIndex+4);
                        }
                        if (childcount != 0)
                        {
                            for (int j = 0; j < childcount; j++)
                            {
                                int choicebegin = 3;
                                int valuebegin = 4;
                                for (int i = 0; i < 10; i++, choicebegin = choicebegin + 2, valuebegin = valuebegin + 2)
                                {
                                    values[(1 + j), choicebegin] = string.Empty;
                                    values[(1 + j), valuebegin] = string.Empty;
                                }
                            }
                        }
                        else
                        {
                            int choicebegin = 3;
                            int valuebegin = 4;
                            for (int i = 0; i < 10; i++, choicebegin = choicebegin + 2, valuebegin = valuebegin + 2)
                            {
                                values[1, choicebegin] = string.Empty;
                                values[1, valuebegin] = string.Empty;
                            }
                        }
                    }

                     r.Value=values;
                    sheet.Application.EnableEvents = true;
                    Util.Definiotion.VariableDictionary = QC4Common.Util.DictionaryUtil.PopulateQSDictionary(excelWorkBook);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                string message = ex.Message;
                 _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);

                return false;
            }
            }

        public string checksinglequete(string value,int index=0)
        {
       
            return value;
        }
        public string UnEscapeCRLF(string text)
        {
            text = text.Replace(QC4Common.Common.Constants.CRLFchar, "\n");
            return text;
        }
      public void SetCellColor(Microsoft.Office.Interop.Excel.Worksheet sheet,string answertype,int RowNo)
        {
            if(answertype==Constants.AnswerType.MA)
            {
                Microsoft.Office.Interop.Excel.Range r1 = sheet.Cells[RowNo,Constants.QS.QsColCount];
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
        public void SetChoiceCellsColor(Microsoft.Office.Interop.Excel.Worksheet sheet, int choiceCount, int RowNo)
        {
            Microsoft.Office.Interop.Excel.Range r1 = sheet.Cells[RowNo, Constants.QS.QsColChoiceBegin];
            Microsoft.Office.Interop.Excel.Range r2 = sheet.Cells[RowNo, Constants.QS.QsColChoiceBegin + choiceCount - 1];
            QC4Common.Util.ExcelUtil.SetCellInteriorColor(sheet.get_Range(r1, r2), Constants.Color.LightGrey);
        }
        public void ResetChoiceCellsColor(Microsoft.Office.Interop.Excel.Worksheet sheet, int choiceCount, int RowNo)
        {
            if (choiceCount != 1000)
            {
                Microsoft.Office.Interop.Excel.Range r1 = sheet.Cells[RowNo, Constants.QS.QsColChoiceBegin + choiceCount];
                Microsoft.Office.Interop.Excel.Range r2 = sheet.Cells[RowNo, Constants.QS.QsColChoiceEnd];
                QC4Common.Util.ExcelUtil.SetCellInteriorColor(sheet.get_Range(r1, r2), Constants.Color.White);
            }
        }
        public void SetSubtotalCellsColor(Microsoft.Office.Interop.Excel.Worksheet sheet, int choiceCount, int RowNo)
        {
            choiceCount = +choiceCount * 2;
            Microsoft.Office.Interop.Excel.Range r1 = sheet.Cells[RowNo, Constants.QS.QsColSubtotal1];
            Microsoft.Office.Interop.Excel.Range r2 = sheet.Cells[RowNo, Constants.QS.QsColSubtotal1+choiceCount-1];
            QC4Common.Util.ExcelUtil.SetCellInteriorColor(sheet.get_Range(r1, r2), Constants.Color.LightGrey);
        }
        public void ResetSubtotalCellsColor(Microsoft.Office.Interop.Excel.Worksheet sheet, int choiceCount, int RowNo)
        {
           choiceCount = +choiceCount * 2;
            Microsoft.Office.Interop.Excel.Range r1 = sheet.Cells[RowNo, Constants.QS.QsColSubtotal1 + choiceCount];
            Microsoft.Office.Interop.Excel.Range r2 = sheet.Cells[RowNo, Constants.QS.ColEnd];
            QC4Common.Util.ExcelUtil.SetCellInteriorColor(sheet.get_Range(r1, r2), Constants.Color.White);
        }
        public bool Save_AN_SA_type(Microsoft.Office.Interop.Excel.Worksheet sheet, string variable, string answertype, string tableHeading = null, string question = null, int choiceindex = 0, int no_choice_index = -1, DataTable data = null, string addsubtotal = "")
        {

            try
            {
                int qestionCount = Util.Definiotion.VariableDictionary.Count;
                QC4Common.Model.QuestionSettings questionSettings = new QC4Common.Model.QuestionSettings();
                sheet.Application.EnableEvents = false;
                Microsoft.Office.Interop.Excel.Range start = sheet.Cells[qestionCount + 4, 1];
                Microsoft.Office.Interop.Excel.Range end = sheet.Cells[qestionCount + 4, 16348];
                Microsoft.Office.Interop.Excel.Range r = sheet.Range[start, end];
                var values = r.Value;
                questionSettings.QuestionFlagUpdated = questionSettings.QuestionFlag = values[1, 2] = Util.Constants.Variable_Type_An;
                questionSettings.Variable = questionSettings.VariableBefore = values[1, 6] = variable;
                questionSettings.AnswerTypeBefore = questionSettings.AnswerType = values[1, 7] = answertype;
                SetCellColor(sheet, answertype, qestionCount + 4);
                sheet.Cells[qestionCount + 4, 8].Value = choiceindex.ToString();
                SetChoiceCellsColor(sheet, choiceindex, qestionCount + 4);
                values[1, 8] = choiceindex.ToString();
                questionSettings.CategoryCount = questionSettings.CategoryCountBefore = choiceindex;
                if (data.Columns.Count > 2)
                {
                    questionSettings.Score = values[1, 9] = getWTstring(data, choiceindex);
                }
                if (no_choice_index != -1)
                {
                    if (no_choice_index > choiceindex)
                    {
                        values[1, 10] = choiceindex;
                        questionSettings.Sort = choiceindex.ToString();
                    }
                    else
                    {
                        values[1, 10] = no_choice_index + 1;
                        questionSettings.Sort = (no_choice_index + 1).ToString();

                    }
                }
                if (addsubtotal == "")
                {
                    values[1, 1016] = string.Empty;
                }
                else
                {
                    values[1, 1016] = "1";
                }

                questionSettings.TableHeading = values[1, 11] = tableHeading;
                questionSettings.Question = values[1, 12] = question;
                for (int i = 0, colcount = 13; i < choiceindex; i++, colcount++)
                {
                    values[1, colcount] = UnEscapeCRLF(data.Rows[i][1].ToString());
                    questionSettings.Choices.Add(data.Rows[i][1].ToString());
                }

                r.Value = values;

                DB.QuestionSettingDao questionSetting = new DB.QuestionSettingDao(QC4Common.DB.DBHelper.GetConnectionString(excelWorkBook));
                questionSetting.insertQuestioninForm(variable, answertype, Util.Constants.Variable_Type_An, choiceindex, sheet: sheet, Rownumber: qestionCount + 4);
                int id = questionSetting.GetvariableId(variable);
                questionSettings.Id = questionSettings.ItemId = id;
                questionSettings.RowNumber = qestionCount + 4;
                questionSettings.IsFound = true;
                questionSettings.IsNew = false;
                questionSettings.QuestionCount = 0;
                questionSettings.SeriallNumber = 0;
                questionSettings.AddSubTotal = "";
                Util.Definiotion.VariableDictionary.Add(variable, questionSettings);
                new QC4Common.Sheets.ListUpdate(excelWorkBook).UpdateListSheet(Util.Definiotion.VariableDictionary.Select(q => q.Value).ToList());
                sheet.Application.EnableEvents = true;

                QC4Common.Common.GTAutoSetting.LoadNewDataToGTHiddenSheet(excelWorkBook,
                     new List<QC4Common.Common.GTAutoSetting.VariableDT>() { new QC4Common.Common.GTAutoSetting.VariableDT { Variable = variable, Type = answertype } });
                return true;

            }
            catch (Exception ex)
            {

                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);

                return false;
            }

        }
        public bool Save_AN_FA_N_type(Microsoft.Office.Interop.Excel.Worksheet sheet, string variable, string answertype, string tableHeading = null, string question = null)
        {
            try
            {
                int qestionCount = Util.Definiotion.VariableDictionary.Count;
                QC4Common.Model.QuestionSettings questionSettings = new QC4Common.Model.QuestionSettings();
                sheet.Application.EnableEvents = false;
                Microsoft.Office.Interop.Excel.Range r;
                Microsoft.Office.Interop.Excel.Range start = sheet.Cells[qestionCount + 4, 1];
                Microsoft.Office.Interop.Excel.Range end = sheet.Cells[qestionCount + 4, 12];
                Microsoft.Office.Interop.Excel.Range Fa_n_range = sheet.Range[start, end];
                var values = Fa_n_range.Value;
                questionSettings.QuestionFlagUpdated = questionSettings.QuestionFlag = values[1, 2] = Util.Constants.Variable_Type_An;
                questionSettings.Variable = questionSettings.VariableBefore = values[1, 6] = variable;
                questionSettings.AnswerTypeBefore = questionSettings.AnswerType = values[1, 7] = answertype;
                questionSettings.TableHeading = values[1, 11] = tableHeading;
                questionSettings.Question = values[1, 12] = question;
                Fa_n_range.Value = values;
                DB.QuestionSettingDao questionSetting = new DB.QuestionSettingDao(QC4Common.DB.DBHelper.GetConnectionString(excelWorkBook));
                questionSetting.insertQuestioninForm(variable, answertype, Util.Constants.Variable_Type_An, sheet: sheet, Rownumber: qestionCount + 4);

                int id = questionSetting.GetvariableId(variable);
                start = sheet.Cells[qestionCount + 4, 1];
                end = sheet.Cells[qestionCount + 4, 16348];
                r = sheet.Range[start, end];

                questionSettings.Id = questionSettings.ItemId = id;
                questionSettings.RowNumber = qestionCount + 4;
                questionSettings.IsFound = true;
                questionSettings.IsNew = false;
                questionSettings.QuestionCount = 0;
                questionSettings.SeriallNumber = 0;
                questionSettings.AddSubTotal = "";
                Util.Definiotion.VariableDictionary = QC4Common.Util.DictionaryUtil.PopulateQSDictionary(excelWorkBook);
                new QC4Common.Sheets.ListUpdate(excelWorkBook).UpdateListSheet(Util.Definiotion.VariableDictionary.Select(q => q.Value).ToList());
                sheet.Application.EnableEvents = true;
                return true;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);

                return false;
            }

        }

    }
}
