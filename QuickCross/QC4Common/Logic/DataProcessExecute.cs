using QC4Common.Common;
using QC4Common.DB;
using QC4Common.Sheets;
using Macromill.QCWeb.Common;
using Macromill.QCWeb.DataProcess;
using Macromill.QCWeb.Tabulation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using Constants = QC4Common.Common.Constants;
using Excel = Microsoft.Office.Interop.Excel;
using ProgressBar = QC4Common.Forms.ProgressBar;
using System.Runtime.InteropServices;
using log4net;
using System.Reflection;
using System.Windows;
using QC4Common.Model;
using Macromill.QCWeb.COMOperate;
using System.Windows.Interop;
using System.ComponentModel;
using System.Threading;

namespace QC4Common.Logic
{
    public class DataProcessExecute
    {
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int RegisterWindowMessage(string lpString);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool SendNotifyMessage(int hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        private static Regex itemNameRegex = new Regex(Constants.ITEMNAME_PATTERN);
        private List<string> dpvariables = new List<string>();
        Sheets.DataProcess dpsheet = null;
        long samplescount = 0;
        string loadeddtquery = string.Empty;
        int listuprangestartindex = 3;
        int lastrow = 3;
        string CriteriaQuerystring = string.Empty;// "(";//string.Empty;
        string lastoperator = string.Empty;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static bool islistup;
        public static Int32 pogressbarcheckvalue = 0;//IL_JP_MAM_007:4295055420
        private bool RowCountChanged = false;
        List<string> updatedColumns = new List<string>();
        Dictionary<string, List<string>> updatedFAColumnsAndValues = new Dictionary<string, List<string>>();
        private int deletedrowcount = 0;
        private int filteringflaglocation = 0;
        private int totaldeletedrowcount = 0;
        private bool firstlistitration = true;
        private static List<string> SampleidforDELETE = new List<string>();
        private static List<string> DeletedSortIdsToFilterData = new List<string>();
        private static bool isDeleteforDECST = false;
        public class Datatabledataposi
        {
            public int Start_posi { get; set; }
            public int End_posi { get; set; }
            public int Dtdata_StartPosi { get; set; }
            public int Dtdata_EndPosi { get; set; }
            public int datacount { get; set; }
        }
        List<Datatabledataposi> Positionlist = new List<Datatabledataposi>();
        public class ListupDetails
        {
            public int rowno { get; set; }
            public int totatlrowcount { get; set; }

        }
        List<ListupDetails> listupcount = new List<ListupDetails>();
        private int listnumber = 0;
        int endposition = 0;
        private Excel.Worksheet DPSheet;
        private bool isFrmStd = false;
        private Macromill.QCWeb.Question.Questions DPQuestions = null;
        QC4Common.Util.FormUtil frmutil = new QC4Common.Util.FormUtil();
        public DataProcessExecute(Sheets.DataProcess initobject, Excel.Worksheet ProcessSheet = null, bool frmStd = false)
        {
            dpsheet = initobject;
            if (ProcessSheet == null)
            {
                ProcessSheet = DataProcess.Sheet;
            }
            DPSheet = ProcessSheet;
            DPQuestions = QC4Common.Util.DictionaryUtil.GetQuestions(DataProcess.currworkbook);
            isFrmStd = frmStd;
        }
        string ParseMconvertdata(char[] line)
        {
            string mconvert = "*";
            int j = 0;
            int i = line.Length;
            for (i = line.Length, j = 1; i >= 1; i--, j++)
            {
                if (line[i - 1] == '1')
                {
                    if (mconvert == "*")
                    {
                        mconvert = ",";
                    }
                    mconvert += j;
                    mconvert += ",";
                }
            }
            return mconvert;
        }
        void ReadDataFromOutputFile(Excel.Worksheet DataAfterProcess, string filepath, int itemid, System.Data.DataTable dt,/*object[,] testarray,*/ int row = 0, bool isMconvert = false, string criteria = null, bool checkExclude = false, bool isFAquestion = false)
        {
            //string grpfileoutput = dirPath + "\\" + newGroupQuestion.ItemId + ".dp";

            System.IO.StreamReader srgrp = new System.IO.StreamReader(filepath);
            string opline = string.Empty;
            List<string> opcontents = new List<string>();

            string line = string.Empty;
            int grpcolindex = itemid == 0 ? Definitions.VariableDictionary.Count : itemid + 1;
            // object[,] value_array = new object[dt.Rows.Count, 2];

            // int row = 0;

            int colindex = itemid == 0 ? Definitions.VariableDictionary.Count : itemid;//int colindex = itemid == 0 ? Definitions.VariableDictionary.Count : itemid + 1;
                                                                                       // _log.Info("STARTED Reading Data from outputfile");
            string colname = "q_" + colindex;
            if (itemid == 0) colname = "sample_id";
            List<string> updatedFaColumnList = new List<string>();
            while ((line = srgrp.ReadLine()) != null)
            {
                
                if (line.Equals("*") && checkExclude == true)
                {
                    line = string.Empty;
                }
                if (isFAquestion)
                {

                    try
                    {
                        updatedFaColumnList.Add(frmutil.UnEscapeCRLF(System.Text.RegularExpressions.Regex.Unescape(line)));//Redmine id: 210184//[Redmine id : 174859] -
                    }
                    catch
                    {
                        updatedFaColumnList.Add(frmutil.UnEscapeCRLF(line));
                    }
                }
                else
                {
                    if (!updatedColumns.Contains(colname))
                    {
                        updatedColumns.Add(colname);
                    }
                }

                try
                {
                    dt.Rows[row][colname] = frmutil.UnEscapeCRLF(System.Text.RegularExpressions.Regex.Unescape(line));//Redmine id: 210184 ; //  dt.Rows[row][colname] = line; // dt.Rows[row][colindex] = line; //191 changed code  System.Text.RegularExpressions.Regex.Unescape(
                }
                catch
                {
                    dt.Rows[row][colname] = frmutil.UnEscapeCRLF(line);
                }//Redmine id: 210184 ; //  dt.Rows[row][colname] = line; // dt.Rows[row][colindex] = line; //191 changed code  System.Text.RegularExpressions.Regex.Unescape(
                //value_array[row, 0] = dt.Rows[row]["sort_no"];
                //value_array[row, 1] = line;

                if (isMconvert)
                    line = ParseMconvertdata(line.ToCharArray());

                //testarray[row, colindex - 1] = line; //COMMENTED JIJ
                row++;
                //DataAfterProcess.Cells[i++, grpcolindex].Value ="*";// line;// contents.Add(line);                
            }
            if (isFAquestion && updatedFaColumnList.Count > 0)
            {
                if (updatedFAColumnsAndValues.ContainsKey(colname))
                {
                    updatedFAColumnsAndValues.Remove(colname);
                }
                updatedFAColumnsAndValues.Add(colname, updatedFaColumnList);
            }
            srgrp.Close();
            srgrp.Dispose();


        }
        void ReadDataFromOutputFileForClass(Excel.Worksheet DataAfterProcess, string filepath, int itemid, DataTable dt,/*object[,] testarray,*/
            bool iscriteriaclassflg, bool[] filterringclassFlg,
            int row = 0, bool isMconvert = false, string criteria = null, bool checkExclude = false, bool isFAquestion = false)
        {
            row = 0; //[Redmine id : 176491] -
            //string grpfileoutput = dirPath + "\\" + newGroupQuestion.ItemId + ".dp";

            System.IO.StreamReader srgrp = new System.IO.StreamReader(filepath);
            string opline = string.Empty;
            List<string> opcontents = new List<string>();

            string line = string.Empty;
            int grpcolindex = itemid == 0 ? Definitions.VariableDictionary.Count : itemid + 1;
            // object[,] value_array = new object[dt.Rows.Count, 2];

            // int row = 0;

            int colindex = itemid == 0 ? Definitions.VariableDictionary.Count : itemid;//int colindex = itemid == 0 ? Definitions.VariableDictionary.Count : itemid + 1;
                                                                                       // _log.Info("STARTED Reading Data from outputfile");
            string colname = "q_" + colindex;
            if (itemid == 0) colname = "sample_id";
            List<string> updatedFaColumnList = new List<string>();


            while ((line = srgrp.ReadLine()) != null)
            {

                int dtsortno = Convert.ToInt32(dt.Rows[row]["sort_no"].ToString());
                if (isFAquestion)
                {
                    updatedFaColumnList.Add(System.Text.RegularExpressions.Regex.Unescape(line));//[Redmine id : 174859] -
                }
                else
                {
                    if (!updatedColumns.Contains(colname))
                    {
                        updatedColumns.Add(colname);
                    }
                }
                //[Redmine id : 176491] -
                if (iscriteriaclassflg && !filterringclassFlg[row])// if (iscriteriaclassflg && !filterringclassFlg[dtsortno - 1])//j + filteringflaglocation

                {
                    dt.Rows[row][colname] = string.Empty;
                }
                else
                {
                    dt.Rows[row][colname] = System.Text.RegularExpressions.Regex.Unescape(line); //  dt.Rows[row][colname] = line; // dt.Rows[row][colindex] = line; //191 changed code  System.Text.RegularExpressions.Regex.Unescape(
                }                                                                      //value_array[row, 0] = dt.Rows[row]["sort_no"];
                                                                                       //value_array[row, 1] = line;

                if (isMconvert)
                    line = ParseMconvertdata(line.ToCharArray());

                //testarray[row, colindex - 1] = line; //COMMENTED JIJ
                row++;
                //DataAfterProcess.Cells[i++, grpcolindex].Value ="*";// line;// contents.Add(line);                
            }
            if (isFAquestion && updatedFaColumnList.Count > 0)
            {
                if (updatedFAColumnsAndValues.ContainsKey(colname))
                {
                    updatedFAColumnsAndValues.Remove(colname);
                }
                updatedFAColumnsAndValues.Add(colname, updatedFaColumnList);
            }
            srgrp.Close();
            srgrp.Dispose();




        }
        public void SaveDTtoTable(DataTable dt)
        {
            //fix for #266922
            object[,] value_array = new object[dt.Rows.Count, dt.Columns.Count + updatedFAColumnsAndValues.Count + 1];
            List<string> Columns = new List<string>();
            List<string> faColumns = updatedFAColumnsAndValues.Keys.ToList();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int ii = 0; ii < updatedColumns.Count; ii++)
                {
                    //if(j == 0)
                    //{
                    //    Columns.Add(dt.Columns[ii].ColumnName);

                    //}
                    value_array[j, ii + 1] = dt.Rows[j][updatedColumns[ii]];

                }

                for (int kk = updatedColumns.Count, keyIndex = 0; kk < updatedColumns.Count + updatedFAColumnsAndValues.Count && keyIndex < faColumns.Count; kk++, keyIndex++)
                {
                    value_array[j, kk + 1] = dt.Rows[j][faColumns[keyIndex]];
                }
                value_array[j, 0] = dt.Rows[j][1];
            }
            if (updatedColumns.Count > 0 || updatedFAColumnsAndValues.Count > 0)
            {
                using (SQLiteConnection dbcon = DBHelper.GetConnection(DBHelper.GetConnectionString(DataProcess.currworkbook)))//DPSheet.Application.ActiveWorkbook
                {
                    dbcon.Open();
                    DBHelper.SaveDataTable(dbcon, dt, value_array, updatedColumns, updatedFAColumnsAndValues);
                    dbcon.Close();
                }
            }
            //  return value_array;

        }

        
        public void GetFromDBAndDelete(Excel.Worksheet DataAfterProcess, string filepath, DataTable dt, int sort_number = 0, bool trigger = false)
        {
            // DataTable dt = LoadDataFromDB();//reading from DB
            System.IO.StreamReader srgrp = new System.IO.StreamReader(filepath);//Reading from file
            string line = string.Empty;
            if(isDeleteforDECST)
                dt = LoadDataFromDB(sort_number, DPSheet.Application.ActiveWorkbook, totaldeletedrowcount);
            if (dt.Rows.Count > 0)
            {
                // dt.AcceptChanges();
                foreach (DataRow row in dt.Rows)
                {
                    if ((line = srgrp.ReadLine()) != null)
                    {
                        if (line == "1")
                        {
                            SampleidforDELETE.Add(row["sort_no"].ToString());// Sampleid.Add(row["sample_id"].ToString());
                            //267601-Creating a list of sort ids for deleted rows
                            if (isDeleteforDECST)
                                DeletedSortIdsToFilterData.Add(row["sample_id"].ToString());
                            // row.Delete();
                        }
                    }
                }
                dt.AcceptChanges();

                srgrp.Close();
                srgrp.Dispose();
                try
                {
                    if (System.IO.File.Exists(filepath))
                    {
                        System.IO.File.Delete(filepath);
                    }
                }
                catch (Exception e)
                { _log.Error(e.Message + "\n" + e.StackTrace + "\n---------------\n" + e.InnerException); }
                //foreach (DataRow row in dt.Rows)
                //{
                //    row.AcceptChanges();
                //}
                //  string sql = "delete * from data_after_process where ";
                
                // dt.AcceptChanges();
            }
            if (isDeleteforDECST)// This condition is added as an alternative fix for #259498, Delete operation is only performed once at the end incase of a DECST operation.
            {
                if (trigger)
                {
                    ExecuteDelete();
                    isDeleteforDECST = false;
                }
            }
            else
            {
                ExecuteDelete();
            }
        }

        public void ExecuteDelete()
        {
            string args = "";
            if (SampleidforDELETE.Count >= 1)
                args = SampleidforDELETE[0].ToString();
            for (int i = 1; i < SampleidforDELETE.Count; i++)
            {
                args += " , " + SampleidforDELETE[i].ToString();
            }
            /*String args = TextUtils.join(", ", ids);
db.execSQL(String.format("DELETE FROM rows WHERE ids IN (%s);", args));*/
            deletedrowcount = SampleidforDELETE.Count;
            if (SampleidforDELETE.Count >= 1)
            {
                using (SQLiteConnection dbcon = DBHelper.GetConnection(DBHelper.GetConnectionString(DataProcess.currworkbook)))//DPSheet.Application.ActiveWorkbook
                {
                    dbcon.Open();
                    DBHelper.ExecuteQuery(String.Format("delete  from data_after_process where sort_no IN ({0})", args), dbcon);// DBHelper.ExecuteQuery(String.Format("delete  from data_after_process where sample_id IN ({0})", args), dbcon);
                                                                                                                                //DBHelper.SaveDataTable(dbcon, dt, loadeddtquery);
                    dbcon.Close();
                }
            }
            SampleidforDELETE.Clear();
        }

        public object[,] Make2DArray(DataTable dt)
        {
            object[,] dataObject = new object[dt.Rows.Count, dt.Columns.Count];
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int ii = 0; ii < dt.Columns.Count; ii++)
                {
                    if (ii == 0)
                    {
                        dataObject[j, ii] = dt.Rows[j][ii];
                    }
                    if (ii > 1)
                    {
                        dataObject[j, ii - 1] = dt.Rows[j][ii];
                    }
                }
            }
            return dataObject;
        }
        public DataTable UpdateAndGetFromDB(Excel.Worksheet DataAfterProcess, List<int> itemidlist, DataTable dt, /*object[,] content,*/ string option, bool iscriteriaomitflg, bool[] filterringomitFlg, int sortno)
        {
            string colname = "";
            object[,] value_array = new object[dt.Rows.Count, itemidlist.Count + 1];
            List<string> colList = new List<string>();
            List<int> clearCacheList = new List<int>();
            foreach (int item in itemidlist)
            {
                if (item == 0)
                { colname = "sample_id"; }
                else { colname = "q_" + item; }
                colList.Add(colname);
            }
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                value_array[j, 0] = dt.Rows[j]["sort_no"];

                int dtsortno = Convert.ToInt32(dt.Rows[j]["sort_no"].ToString());
                int count = 1;
                foreach (int i in itemidlist)
                {

                    if (i == 0)
                    { colname = "sample_id"; }
                    else { colname = "q_" + i; }

                    if (!updatedColumns.Contains(colname))
                    {
                        updatedColumns.Add(colname);
                    }
                    //[Redmine id : 176491] -
                    if (iscriteriaomitflg && !filterringomitFlg[j])// if (iscriteriaomitflg && !filterringomitFlg[dtsortno - 1])//j + filteringflaglocation

                    {
                        value_array[j, count] = dt.Rows[j][colname];

                    }
                    else
                    {
                        value_array[j, count] = "*";
                        dt.Rows[j][colname] = "*";
                        if (!clearCacheList.Contains(i))
                        {
                            clearCacheList.Add(i);
                        }


                    }
                    count++;
                }
            }
            foreach (int id in clearCacheList)
            {
                try
                {
                    string cacheFilePath = Path.Combine(GetProcessIdPath()+ "\\", id.ToString() + ".dp");
                    string cacheFileTxtPath = Path.Combine(GetProcessIdPath()+ "\\", id.ToString() + ".txt");

                    if (File.Exists(cacheFilePath))
                    {
                        File.Delete(cacheFilePath);
                    }

                    if (File.Exists(cacheFileTxtPath))
                    {
                        File.Delete(cacheFileTxtPath);
                    }
                }
                catch (Exception ex)
                {

                }

            }
            // return dt;
            using (SQLiteConnection dbcon = DBHelper.GetConnection(DBHelper.GetConnectionString(DataProcess.currworkbook)))//DPSheet.Application.ActiveWorkbook
            {
                dt.TableName = "data_after_process"; //191 added this bcs of SQL Logic error
                dbcon.Open();
                DBHelper.SaveDataTable(dbcon, dt, value_array, colList);
                dbcon.Close();
            }
            //return dt;
            return dt;
        }



        public Dictionary<int, DataProcess.Crit_Inst_Operator> GetinstructionsByRow(Excel.Range dpstart, Excel.Range lastcell, string callmethodname = "", bool updateDict = true)//made "public"   by 191  for validation in execute button click
        {
            Dictionary<int, DataProcess.Crit_Inst_Operator> criteria_inst_operator_Dict = new Dictionary<int, DataProcess.Crit_Inst_Operator>();
            try
            {
                bool startskip = false;

                Excel.Range operatorrange = DPSheet.Range[dpstart, lastcell];
                // dpsheet.operatorsDict.Clear();
                if (updateDict)
                {
                    dpsheet.crit_inst_operator_Dict.Clear();
                }
                DataProcess.Crit_Inst_Operator objdpoperators;


                foreach (Excel.Range cell in operatorrange.Cells)
                {

                    int currentrow = cell.Row;
                    //check for descst and decend

                    Excel.Range substituteoperator = DPSheet.Cells[currentrow, Constants.DP.SubstituteOperatorColumn];
                    //  dpsheet.operatorsDict.Add(cell.Row, substituteoperator.Text);
                    Excel.Range criteriavariable = DPSheet.Cells[currentrow, Constants.DP.CriteriaVariableColumn];
                    Excel.Range instruction = DPSheet.Cells[currentrow, Constants.DP.InstructionColumn];

                    if (instruction.Text == Constants.DP.InstructionDECST)
                    {
                        if (string.Equals(cell.Text, CommonResource.CELL_ON))
                        {
                            startskip = true;
                        }
                        continue;
                    }
                    else if (instruction.Text == Constants.DP.InstructionDECEND && string.Equals(cell.Text, CommonResource.CELL_ON))
                    {
                        if (string.Equals(cell.Text, CommonResource.CELL_ON))
                        {
                            startskip = false;
                        }

                        continue;
                    }

                    else if (instruction.Text == Constants.DP.InstructionFOR)
                    {

                        if (string.Equals(cell.Text, CommonResource.CELL_ON))
                        {
                            startskip = true;
                            objdpoperators = new DataProcess.Crit_Inst_Operator();
                            objdpoperators.criteriavariable = criteriavariable.Text;
                            objdpoperators.instruction = instruction.Text;
                            objdpoperators.substituteoperator = substituteoperator.Text;
                            if (updateDict)
                            { dpsheet.crit_inst_operator_Dict.Add(cell.Row, objdpoperators); }

                            criteria_inst_operator_Dict.Add(cell.Row, objdpoperators);
                        }
                        continue;
                    }
                    else if (instruction.Text == Constants.DP.InstructionNEXT)
                    {
                        if (string.Equals(cell.Text, CommonResource.CELL_ON))
                        {
                            startskip = false;

                            objdpoperators = new DataProcess.Crit_Inst_Operator();
                            objdpoperators.criteriavariable = criteriavariable.Text;
                            objdpoperators.instruction = instruction.Text;
                            objdpoperators.substituteoperator = substituteoperator.Text;
                            if (updateDict)
                            {
                                dpsheet.crit_inst_operator_Dict.Add(cell.Row, objdpoperators);
                            }
                            criteria_inst_operator_Dict.Add(cell.Row, objdpoperators);
                        }
                        continue;
                    }

                    if (string.Equals(cell.Text, CommonResource.CELL_ON))
                    {

                        if (!startskip /*&& string.IsNullOrEmpty(callmethodname)*/)
                        {
                            objdpoperators = new DataProcess.Crit_Inst_Operator();
                            objdpoperators.criteriavariable = criteriavariable.Text;
                            objdpoperators.instruction = instruction.Text;
                            objdpoperators.substituteoperator = substituteoperator.Text;
                            if (updateDict)
                            {
                                dpsheet.crit_inst_operator_Dict.Add(cell.Row, objdpoperators);
                            }
                            criteria_inst_operator_Dict.Add(cell.Row, objdpoperators);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
            return criteria_inst_operator_Dict;
        }
        private DataTable GetDataByColumn(string column, string sortno, string connectionstring = "")
        {
            DataTable dt = new DataTable();
            try
            {

                using (SQLiteConnection dbSource = DBHelper.GetConnection(!string.IsNullOrEmpty(connectionstring) ? connectionstring : DBHelper.GetConnectionString(DataProcess.currworkbook)))//DPSheet.Application.ActiveWorkbook
                {
                    dbSource.Open();
                    string sql = "SELECT " + column + " FROM data_after_process where sort_no > " + sortno + " order by sort_no limit " + Constants.MAX_ROW_COUNT.ToString();
                    dt = DB.DBHelper.GetDataTable(sql, dbSource);
                    if (dt.Rows.Count == 0)
                    {
                        return null;
                    }
                    dbSource.Close();
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                MessageDialog.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                return null;
            }
            return dt;
        }
        private int GetQuestionId(string variableName, string connectionstring = "")
        {
            int id = 0;
            try
            {

                using (SQLiteConnection dbSource = DBHelper.GetConnection(!string.IsNullOrEmpty(connectionstring) ? connectionstring : DBHelper.GetConnectionString(DataProcess.currworkbook)))//DPSheet.Application.ActiveWorkbook
                {
                    dbSource.Open();
                    string sql = "SELECT id FROM question where variable='" + variableName + "'";
                    using (SQLiteCommand mycommand = new SQLiteCommand(dbSource))
                    {
                        mycommand.CommandText = sql;
                        SQLiteDataReader reader = mycommand.ExecuteReader();
                        if (reader.Read())
                        {
                            id = reader.GetInt32(0);
                        }
                    }

                    dbSource.Close();
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                MessageDialog.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                return 0;
            }
            return id;
        }
        private DataTable LoadDataFromDB(int sortnumber = 0, Excel.Workbook activeworkbook = null, int deletedrows = 0)//, Excel.Workbook activeworkbook= null
        {
            string sql = "";
            if (Definitions.VariableDictionary.Any(x => x.Value.QuestionFlag == "An"))
                sql = "SELECT * FROM data_after_process a join multivariate_temp m on a.sort_no = m.sort_no where a.sort_no > " + sortnumber.ToString() + " order by a.sort_no limit " + (Constants.MAX_ROW_COUNT - deletedrows).ToString();
            else
                sql = "SELECT * FROM data_after_process where sort_no > " + sortnumber.ToString() + " order by sort_no limit " + (Constants.MAX_ROW_COUNT - deletedrows).ToString();
            loadeddtquery = sql;


            return Common.Util.LoadDataFromDB(activeworkbook, sql);
        }
        private DataTable LoadCancelDB(int sortnumber = 0, Excel.Workbook activeworkbook = null, int deletedrows = 0)//, Excel.Workbook activeworkbook= null
        {
            string sql = "";
            if (Definitions.VariableDictionary.Any(x => x.Value.QuestionFlag == "An"))
                sql = "SELECT * FROM answers a join multivariate_temp m on a.sort_no = m.sort_no where a.sort_no > " + sortnumber.ToString() + " order by a.sort_no limit " + (Constants.MAX_ROW_COUNT - deletedrows).ToString();
            else
                sql = "SELECT * FROM answers where sort_no > " + sortnumber.ToString() + " order by sort_no limit " + (Constants.MAX_ROW_COUNT - deletedrows).ToString();
            loadeddtquery = sql;


            return Common.Util.LoadDataFromDB(activeworkbook, sql);
        }


        private int UpdateColumnsForOmit(string column, string connectionstring = "")
        {
            int rowseffected = 0;
            try
            {

                using (SQLiteConnection dbSource = DBHelper.GetConnection(!string.IsNullOrEmpty(connectionstring) ? connectionstring : DBHelper.GetConnectionString(DataProcess.currworkbook)))//DPSheet.Application.ActiveWorkbook
                {
                    dbSource.Open();
                    string sql = "UPDATE data_after_process SET " + column;//where condition may com later for criteria part
                    DB.DBHelper.ExecuteQuery(sql, dbSource);//need to check whether updated success or not

                    dbSource.Close();
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                MessageDialog.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                return 0;
            }
            return rowseffected;
        }
        private bool WriteFileFromQuestionID(int qId, string filepath, DataTable dt)
        {
            string readFilePath = GetProcessIdPath();// Path.Combine(Path.GetTempPath(), "QC4\\" + qId.ToString() + ".dp");

            readFilePath = readFilePath + ("\\" + qId.ToString() + ".dp");


            if (File.Exists(readFilePath))
            {
                string inputtext = File.ReadAllText(readFilePath);
                //if (File.Exists(filepath))
                //{
                File.AppendAllText(filepath, inputtext);
                //}
            }
            else
            {
                StreamWriter crossData = new StreamWriter(filepath, true);
                foreach (DataRow dr in dt.Rows)
                {
                    // dt.Columns["q_"+qId.ToString()].
                    crossData.WriteLine(dr["q_" + qId].ToString());
                    //File.AppendAllText(filepath,);
                }
                crossData.Close();
            }
            return true;
        }
        private string GetCrossdataFilePath(int rowNumber, int qstnId, bool isParam = false, int subRow = 0)
        {
            string template = string.Empty;
            string name = string.Empty;
            if (subRow > 0)
            {
                template = "{0}_{1}_{2}";
                name = string.Format(template, rowNumber, subRow, qstnId);

            }
            else
            {
                template = "{0}_{1}";
                name = string.Format(template, rowNumber, qstnId);
            }

            name += isParam ? "_param_Data.cross" : "_Data.cross";
            return Path.Combine(Path.GetTempPath(), "QC4\\" + name);
        }
        private int GetIDfromQuestionName(string qName)
        {
            if (Definitions.VariableDictionary.ContainsKey(qName))
            {
                return Definitions.VariableDictionary[qName].ItemId;
            }
            return -1;
        }
        private string WriteCheckCrossVariableFile(int rowNumber, string newqstn, List<string> paramVariables, DataTable dt, int SubRow = 0)
        {
            int qstnId = GetIDfromQuestionName(newqstn);

            string inputfilepath = GetCrossdataFilePath(rowNumber, qstnId, subRow: SubRow);

            WriteFileFromQuestionID(qstnId, inputfilepath, dt);

            List<string> paramname = new List<string>();

            foreach (string param in paramVariables)
            {
                if (!paramname.Contains(param))
                {
                    paramname.Add(param);
                    int paramId = Definitions.VariableDictionary[param].ItemId;
                    inputfilepath = GetCrossdataFilePath(rowNumber, paramId, true, SubRow);
                    WriteFileFromQuestionID(paramId, inputfilepath, dt);
                }
            }

            return inputfilepath;
        }
        
        private void HandleCALLStatement(KeyValuePair<int, DataProcess.Crit_Inst_Operator> kvp, Excel.Worksheet sheet, DataTable dt, Excel.Worksheet DataAfterProcess_sheet, int sort_number, int samplecount)
        {
            Excel.Range row = null;
            object[,] initvalue = null;
            try
            {
                DPSheet.Application.ScreenUpdating = false;
                string methodname = DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column].Text;
                if (DataProcess.decst_ProgramList.ContainsKey(methodname))
                {
                    DPCallMethod method = DataProcess.decst_ProgramList[methodname];
                    int startrow = method.Rowstart;
                    int endrow = method.RowEnd;
                    Excel.Range callmethodstart = DPSheet.Cells[startrow + 1, Constants.DP.OnOffColumn];
                    Excel.Range callmethodend = DPSheet.Cells[endrow, Constants.DP.OnOffColumn];
                    Dictionary<int, DataProcess.Crit_Inst_Operator> CALL_criteria_inst_operator_Dict = GetinstructionsByRow(callmethodstart, callmethodend);
                    int instructionIndex = 0;
                    int indexfordecstcheck = 0;//267601 creating a list of index for decst deletes
                    List<int> instructionList = new List<int>();
                    foreach (KeyValuePair<int, DataProcess.Crit_Inst_Operator> keypair in CALL_criteria_inst_operator_Dict)
                    { if (keypair.Value.instruction == Constants.DP.InstructionDELETE || keypair.Value.instruction == Constants.DP.InstructionLDEL)
                            instructionList.Add(indexfordecstcheck);
                        indexfordecstcheck++;
                    }
                    foreach (KeyValuePair<int, DataProcess.Crit_Inst_Operator> keyval in CALL_criteria_inst_operator_Dict/*dpsheet.crit_inst_operator_Dict*/)//(KeyValuePair<int, string> kvp in dpsheet.operatorsDict)
                    {
                        for (int i = 0; i < DeletedSortIdsToFilterData.Count; i++)
                        {
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                if (dt.Rows[j]["sample_id"].ToString() == DeletedSortIdsToFilterData[i])
                                {
                                    dt.Rows.RemoveAt(j);
                                    break;
                                }
                            }
                        }
                        DeletedSortIdsToFilterData.Clear();//267601-Clearing the data created for deleted rows.
                        instructionIndex++;
                        Excel.Range rowstart = DPSheet.Cells[keyval.Key, 1];
                        Excel.Range rowend = ExcelUtilForAddIn.EndxlRight(DPSheet.Cells[keyval.Key, 1]);
                        row = DPSheet.Range[rowstart, rowend];

                        initvalue = row.Value;
                        for (int ii = 0; ii < method.paramcount; ii++)
                        {
                            string paramchar = "[" + (char)(65 + ii) + "]";
                            foreach (Excel.Range cell in row)
                            {
                                //string cellText = cell.Text.ToLower();
                                string paramCharLower = paramchar.ToLower();
                                if (cell.Text.Contains(paramCharLower) || cell.Text.Contains(paramchar))
                                {
                                    if (backgroundWorker.CancellationPending)
                                    {
                                        return;
                                    }
                                    DPSheet.Application.EnableEvents = false;
                                    cell.Value = cell.Text.Replace(paramCharLower, DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam2Column + ii].Text);//method.ParamList[ii];
                                    cell.Value = cell.Text.Replace(paramchar, DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam2Column + ii].Text);
                                    if (cell.Column == Constants.DP.CriteriaVariableColumn)
                                    {
                                        keyval.Value.criteriavariable = cell.Value.ToString();
                                    }
                                    else if (cell.Column == Constants.DP.SubstituteOperatorColumn)
                                    {
                                        keyval.Value.substituteoperator = cell.Value.ToString();
                                    }
                                    DPSheet.Application.EnableEvents = true;

                                }
                            }
                        }

                        //foreach(var param in method.ParamList)
                        //{
                        //    if()
                        //}
                        try
                        {
                            bool triggerDelete = false;
                            if (keyval.Value.instruction == Constants.DP.InstructionDELETE || keyval.Value.instruction == Constants.DP.InstructionLDEL)
                            {
                                isDeleteforDECST = true;
                                //267601 Fix for Trigger Delete not working when DELETE & LISTUP coexist in DECST.
                                if (instructionList.Last() + 1 == instructionIndex)
                                    triggerDelete = true;
                            }
                            ExecuteInternal(keyval, sheet, dt/*, tablearray*/, sort_number, DataAfterProcess_sheet, samplecount, CALLstatementRow: kvp.Key, triggerDelete: triggerDelete);
                        }
                        finally
                        {
                            row.Value = initvalue;
                        }
                    }

                    DeletedSortIdsToFilterData.Clear();//267601-Clearing the data created for deleted rows.
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("StackTrace:{0}", ex.StackTrace);
            }
            finally
            {
                if (row != null && backgroundWorker.CancellationPending)
                {
                    row.Value = initvalue;//Setting to initial value when canceling the process
                }
                if (!DPSheet.Application.ScreenUpdating && QC4Common.Global.Global.CheckOperationFlag == 0)
                {
                    DPSheet.Application.ScreenUpdating = true;
                }
            }
        }
        private void AddDPVariableInfo(int rowNumber, string Operator, string newqstn, List<string> paramVariables, string inputfilepath, int subrowNum)
        {
            string line = string.Empty;
            int qstnId = GetIDfromQuestionName(newqstn);
            line = string.Format("{0}\t{1},", Operator, qstnId);
            string filePath = GetCrossdataFilePath(rowNumber, qstnId, false, subrowNum);
            if (File.Exists(filePath))
            {
                line += filePath;
            }
            line += "\t";
            foreach (string param in paramVariables)
            {
                if (line.Substring(line.Length - 1) != "\t")
                {
                    line += ",";
                }
                qstnId = GetIDfromQuestionName(param);
                line += qstnId + ",";
                filePath = GetCrossdataFilePath(rowNumber, qstnId, true, subrowNum);
                if (File.Exists(filePath))
                {
                    line += filePath;
                }
            }
            //  dpvariables.Add(string.Format("{0}\t{1}\t{2}", Operator, newqstn, string.Join(",", paramVariables)));
            dpvariables.Add(line);
        }

        public int FindFORNEXTEndRow(int RowNumbr)
        {
            int endrow = RowNumbr;
            Excel.Range lastcell = ExcelUtilForAddIn.EndxlUp(DPSheet.Cells[RowNumbr, Constants.DP.OnOffColumn]);
            int rowcount = lastcell.Row;
            int totalRow = rowcount - RowNumbr;
            for (int i = RowNumbr; i <= lastcell.Row; i++)
            {
                Excel.Range operatorcell = DPSheet.Cells[i, Constants.DP.InstructionColumn];
                if (operatorcell.Text == Constants.DP.InstructionNEXT)//GetQuestions(DPSheet.Application.ActiveWorkbook);
                {
                    return i;
                }
            }
            return endrow;
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
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();


        Excel.Worksheet templateList;//for listup
        Excel.Workbook targetBook = null;
        Excel.Workbooks targetBooks = null;
        Excel.Worksheet activesheet = null;
        AutoResetEvent AutoReset;
        BackgroundWorker backgroundWorker = new BackgroundWorker();
        int progressvalue = 0;
        DataTable dataCancelTable = new DataTable();
        public void Execute(Excel.Worksheet sheet, ref Excel.Application xlApp, ProgressBar progress, object worker = null, DoWorkEventArgs bgWorkerArg = null, AutoResetEvent autoReset = null)//added progress for progress bar
        {
            //ExcelOperate excelOperate = null;
            //Excel.Application xlApp = null;
            BackgroundWorker bgWorker = worker as BackgroundWorker;
            backgroundWorker = bgWorker;
            Excel.Workbook curwrkbuk = DPSheet.Application.ActiveWorkbook;
            if (autoReset != null)
            {
                this.AutoReset = autoReset;
            }
            //   try { DPSheet.Application.ScreenUpdating = false;  catch { }
            try
            {
                {
                    //create dir
                    string outputfilepath = System.IO.Path.Combine(GetProcessIdPath(), "\\");//+ Cluster_Input_File_Name
                  /*  ApplicationConfig appConfig = new ApplicationConfig();
                    outputfilepath =
            System.IO.Path.Combine(
                appConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_COMMON_ACCUMULATE_PATH_AP));
                    GlobalMethodClass.GuaranteeDirectoryExist(outputfilepath);*/
                }
            }
            catch { }
            try
            {

                // try { DPSheet.Application.ScreenUpdating = false; } catch { }
                pogressbarcheckvalue = 0;//IL_JP_MAM_007:4295055420
                if (progress != null)
                {
                    StopProcess();
                    if (bgWorker.CancellationPending)
                    {
                        return;
                    }
                    else
                    {
                        progress.OnWorkerMethodComplete(pogressbarcheckvaluefuntion(progressvalue), CommonResource.GT_PROGRESS_MSG1);//IL_JP_MAM_007:4295055420
                    }

                    progress.Dispatcher.Invoke(() =>
                    { //IntPtr hForeGroundWindw = GetForegroundWindow();
                        if (progress.changeWIndowPos)
                        {
                            IntPtr hForeGroundWindw = isFrmStd ? QC4Common.Common.Util.GetIntPtrWIndowHandleFromSettings(curwrkbuk) : GetForegroundWindow();


                            var top = progress.Top;
                            var left = progress.Left;
                        }

                    });

                }

                Excel.Range dpstart = DPSheet.Cells[Constants.DP.ProUIstartRow, Constants.DP.OnOffColumn];
                Excel.Range lastcell = ExcelUtilForAddIn.EndxlUp(DPSheet.Cells[Constants.DP.ProUIstartRow, Constants.DP.OnOffColumn]);
                Dictionary<int, DataProcess.Crit_Inst_Operator> criteria_inst_operator_Dict = GetinstructionsByRow(dpstart, lastcell);

                dpsheet.ListUp_Dict.Clear();//for getting listup criteria list
                Excel.Worksheet ws = ExcelUtilForAddIn.GetWorksheetByName(DPSheet.Application.ActiveWorkbook,Constants.SheetType.sh_Data01 + "(Processed)");
                Excel.Worksheet DataSheet = ExcelUtilForAddIn.GetWorksheetByName(DPSheet.Application.ActiveWorkbook,Constants.SheetType.sh_Data01);
                DataProcess.currworkbook = DPSheet.Application.ActiveWorkbook;
                DataProcess.currworkbook.Unprotect(Constants.Password);
                DPSheet.Application.EnableEvents = false;
                if (ws != null)
                {
                    QC4Common.Util.ExcelUtil.DeleteDataAfterSheets(DataProcess.currworkbook);
                    //  ws.Application.DisplayAlerts = false;
                    //ws.Delete();
                    //if(DataSheet != null)
                    //    DataSheet.Visible = Excel.XlSheetVisibility.xlSheetVisible; //COmmenting based on customer feedback to keep data and data processed sheet
                }
                if (!DPSheet.Application.DisplayAlerts)
                {
                    DPSheet.Application.DisplayAlerts = true;
                }


                if (progress != null)
                {
                    StopProcess();
                    if (bgWorker.CancellationPending)
                    {
                        return;
                    }

                    //IntPtr hForeGroundWindw = isFrmStd ? ExcelAddIn.Common.Util.GetIntPtrWIndowHandleFromSettings() : GetForegroundWindow();
                    //SetParent(progress.hWnd, hForeGroundWindw);
                    else
                    {
                        progress.OnWorkerMethodComplete(pogressbarcheckvaluefuntion(5), CommonResource.GT_PROGRESS_MSG1);//IL_JP_MAM_007:4295055420
                    }
                }
                /* DataAfterProcess_sheet.Name = Constants.SheetType.sh_Data01 + "(Processed)"; //sh_Data01//"Data After Process";
                 DataAfterProcess_sheet.Visible = Excel.XlSheetVisibility.xlSheetVisible;
                 DataProcess.currworkbook.Protect(Constants.Password, true);*/
                var array = Definitions.VariableDictionary.Select(q => q.Value).ToList();
                QC4Common.Util.ExcelUtil.GenerateNewDataSheet(DataProcess.currworkbook, array, "data_after_process");
                Excel.Worksheet DataAfterProcess_sheet = ExcelUtilForAddIn.GetWorksheetByName(DataProcess.currworkbook,"Data01(Processed)");
                // DataAfterProcess_sheet.Name = Constants.SheetType.sh_Data01 + "(Processed)"; 
                //DataAfterProcess_sheet.Visible = Excel.XlSheetVisibility.xlSheetVisible;
                DPSheet.Application.EnableEvents = true;
                DPSheet.Activate();
                // Excel.Range header = DataAfterProcess_sheet.Rows[3];
                List<string> QuestionList = Definitions.VariableDictionary.Keys.ToList<string>();

                //datarange.Value = tablearray;
                int sort_number = 0;
                int pre_sort_number = 0;
                int samplescount = Common.Util.GetTotalRowcount();
                listuprowcountlist = new List<int>();
                int nPageCount = samplescount % Constants.MAX_ROW_COUNT > 0 ? (samplescount / Constants.MAX_ROW_COUNT) + 1 : (samplescount / Constants.MAX_ROW_COUNT);

                dpvariables.Clear();

                if (progress != null)
                {
                    StopProcess();
                    if (bgWorker.CancellationPending)
                    {
                        return;
                    }
                    else
                    {
                        progressvalue = 10;
                        progress.OnWorkerMethodComplete(pogressbarcheckvaluefuntion(10), CommonResource.DP_PROGRESS_MSG_5);//progress % with message  //IL_JP_MAM_007:4295055420
                    }
                }

                if (islistup == true)
                {
                    string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\QC4\\Templates\\" + "Comparison GT.xlsx"; //GetTemplatePath(TEMPLATE_NAME);  
                                                                                                                                                          // string targetPath = QC4Common.Util.QCUtil.GetTargetPath(DataProcess.currworkbook);
                                                                                                                                                          //  targetPath = Path.GetDirectoryName(targetPath) + "\\";
                                                                                                                                                          //  targetPath.Replace("\\\\", "\\");

                    string fileopenPath = QC4Common.Util.QCUtil.GetTempPath(DataProcess.currworkbook);
                    fileopenPath = Path.GetDirectoryName(fileopenPath) + "\\";
                    fileopenPath.Replace("\\\\", "\\");

                    //  string saveAsPath = targetPath + CommonResource.COMPARISON_FILE_NAME + ".xlsx";
                    string savefileopenPath = fileopenPath + CommonResource.COMPARISON_FILE_NAME + ".xlsx";

                    int count = 1;
                    do
                    {
                        try
                        {
                            //if (File.Exists(saveAsPath))
                            //{
                            //    File.Delete(saveAsPath);
                            //}
                            if (File.Exists(savefileopenPath))
                            {
                                File.Delete(savefileopenPath);
                            }
                            break;
                        }
                        catch
                        {
                            //  saveAsPath = targetPath + CommonResource.COMPARISON_FILE_NAME + (count) + ".xlsx";
                            savefileopenPath = fileopenPath + CommonResource.COMPARISON_FILE_NAME + (count++) + ".xlsx";
                        }
                    }
                    while (true);
                    File.Copy(tempPath, savefileopenPath);
                    tempPath = savefileopenPath;

                    QC4Common.Global.Global.islistup = true;
                    Excel.Worksheet templateSheetGT;
                    Excel.Worksheet templateSheetCross;

                    // DataProcess.currworkbook.Application.DefaultFilePath = saveAsPath;
                    //excelOperate = new ExcelOperate();
                    //xlApp = excelOperate.Excel;
                    xlApp.Visible = false;
                    var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    // DataProcess.currworkbook.Application.DefaultFilePath = null;
                    targetBook = xlApp.Workbooks.Add(tempPath);

                    QC4Common.Global.Global.islistup = false;
                    templateSheetGT = ExcelUtilForAddIn.GetWorksheetByName(DPSheet.Application.ActiveWorkbook,"Comparison GT", xlApp);
                    if (templateSheetGT != null)
                    {
                        templateSheetGT.Application.DisplayAlerts = false;
                        templateSheetGT.Delete();

                    }
                    templateSheetCross = ExcelUtilForAddIn.GetWorksheetByName(DPSheet.Application.ActiveWorkbook,"Check Cross", xlApp);
                    if (templateSheetCross != null)
                    {
                        templateSheetCross.Application.DisplayAlerts = false;
                        templateSheetCross.Delete();

                    }
                    templateList = ExcelUtilForAddIn.GetWorksheetByName(DPSheet.Application.ActiveWorkbook, "LIST");


                    try
                    {

                        //   lworkbook = targetBook;
                        foreach (Excel.Worksheet sh in targetBook.Worksheets)
                            sh.Rows.Font.Name = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
                        //targetBook.Application.WindowState = Excel.XlWindowState.xlMinimized;
                    }
                    catch { }


                    QC4Common.Global.Global.islistup = false;

                    ///////////https://app.gluemodel.com/#/project/task/4295061492
                    bool enbleevents = DPSheet.Application.EnableEvents;
                    DPSheet.Application.EnableEvents = false;
                    {
                        templateList = targetBook.Sheets[1];
                    }


                    templateList.Cells[2, 2].Value = CommonResource.LISTUP_COMMAND_LINE_NUMBER;
                    templateList.Cells[2, 3].Value = CommonResource.LISTUP_SAMPLE_ID;
                    targetBook.Sheets[2].Cells[2, 2].Value = CommonResource.LISTUP_COMMAND_LINE_NUMBER;
                    targetBook.Sheets[2].Cells[2, 2].ColumnWidth = 13;
                    targetBook.Sheets[2].Cells[2, 3].Value = CommonResource.LISTUP_VARIABLE;
                    targetBook.Sheets[2].Cells[2, 3].ColumnWidth = 13;
                    targetBook.Sheets[2].Cells[2, 4].Value = CommonResource.LISTUP_COMPARISON_OPERATOR;
                    targetBook.Sheets[2].Cells[2, 4].ColumnWidth = 13;
                    targetBook.Sheets[2].Cells[2, 5].Value = CommonResource.LISTUP_CRITERIA_VALUE;
                    targetBook.Sheets[2].Cells[2, 5].ColumnWidth = 13;
                    targetBook.Sheets[2].Cells[2, 6].Value = CommonResource.LISTUP__INSTRUCTION;
                    targetBook.Sheets[2].Cells[2, 6].ColumnWidth = 13;
                    targetBook.Sheets[2].Cells[2, 7].Value = CommonResource.LISTUP_NO_OF_CASES;
                    targetBook.Sheets[2].Cells[2, 7].ColumnWidth = 13;
                    targetBook.Sheets[2].name = CommonResource.LISTUP_NO_OF_CASES;//4295061541


                    DPSheet.Application.EnableEvents = enbleevents;
                    ////////////////https://app.gluemodel.com/#/project/task/4295061492


                    try
                    {

                        if (!bgWorker.CancellationPending)
                        {
                            // lworkbook = targetBook;
                            foreach (Excel.Worksheet sh in targetBook.Worksheets)
                                sh.Rows.Font.Name = QC4Common.Common.Constants.GlobalMode.Split(',')[1];

                            targetBook.Application.WindowState = Excel.XlWindowState.xlMinimized;
                            targetBook.Application.DisplayScrollBars = true;
                            // targetBook.Application.Columns.AutoFit();
                        }
                        //DPSheet.Columns.AutoFit();
                    }
                    catch (Exception e) { System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace); }//for listup minimizing and openenin later aftr ok -191 -23-11-19
                    finally
                    {
                        if (bgWorker.CancellationPending)
                        {
                            if (targetBook != null)
                            {
                                try
                                {
                                    //if (File.Exists(saveAsPath))
                                    //{
                                    //    File.Delete(saveAsPath);
                                    //}
                                    if (File.Exists(savefileopenPath))
                                    {
                                        File.Delete(savefileopenPath);
                                    }

                                }
                                catch
                                { }
                            }
                        }
                    }
                }

                List<int> forKeyList = criteria_inst_operator_Dict.Keys.Where(a => criteria_inst_operator_Dict[a].instruction == Constants.DP.InstructionFOR).ToList();
                int totaliteration = 0;
                if (forKeyList != null && forKeyList.Count > 0)
                {

                    foreach (int keyRow in forKeyList)
                    {
                        int executionstatements = 0;
                        int FORendrow = FindFORNEXTEndRow(keyRow);
                        if (FORendrow > keyRow + 1)
                        {
                            DPSheet.Application.ScreenUpdating = false;
                            Excel.Range forinstStart = DPSheet.Cells[keyRow + 1, Constants.DP.OnOffColumn];
                            Excel.Range forinstEnd = DPSheet.Cells[FORendrow, Constants.DP.OnOffColumn];
                            Dictionary<int, DataProcess.Crit_Inst_Operator> FOR_criteria_inst_operator_Dict = GetinstructionsByRow(forinstStart, forinstEnd);
                            Excel.Range paramstart = DPSheet.Cells[keyRow, Constants.DP.SubstituteParam1Column];
                            Excel.Range paramend = DPSheet.Cells[keyRow, Constants.DP.SubstituteParam2Column];
                            Excel.Range paramcounter = DPSheet.Cells[keyRow, Constants.DP.SubstituteParam3Column];

                            int startvalue = -1;
                            int endvalue = 0;
                            int counter = 1;
                            int.TryParse(paramstart.Text, out startvalue);
                            int.TryParse(paramend.Text, out endvalue);
                            int.TryParse(paramcounter.Text, out counter);

                            if (endvalue > startvalue)
                            {
                                totaliteration = ((endvalue - startvalue) / counter) + 1;
                                executionstatements = FOR_criteria_inst_operator_Dict.Count;
                            }


                        }
                        totaliteration += executionstatements * totaliteration;
                    }
                }
                int totalOperation = (totaliteration + criteria_inst_operator_Dict.Count) * nPageCount;
                //////////////////////////////////////////////////
                /////////////////////////////////////////////
                int processingrowcount = 0;
                //int totalValue = samplescount *
                int processedStatement = 0;
                string tempCrosspath = Path.Combine(Path.GetTempPath(), "QC4");
                var crossdir = new DirectoryInfo(tempCrosspath);

                foreach (var file in crossdir.EnumerateFiles("*.cross"))
                {
                    file.Delete();
                }

                for (int i = 0; i < nPageCount; i++)
                {
                    try
                    {
                        string directoryPath = GetProcessIdPath();// Path.Combine(Path.GetTempPath(), "QC4");
                        var dir = new DirectoryInfo(directoryPath);

                        foreach (var file in dir.EnumerateFiles("*.dp"))
                        {
                            file.Delete();
                        }
                        foreach (var file in dir.EnumerateFiles("*.txt"))
                        {
                            file.Delete();
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.Error(ex.Message);
                        continue;
                    }
                    listuploopnum = 0;
                    dpvariables.Clear();
                    if (i != 0) listuploopfirsttime = false;
                    DataTable dt = LoadDataFromDB(sort_number, curwrkbuk);


                    processingrowcount += dt.Rows.Count;
                    int progcounter = 5 + (((i + criteria_inst_operator_Dict.Count)) * 100) / (nPageCount + criteria_inst_operator_Dict.Count);
                    if (progcounter >= 100 || progcounter >= 95) progcounter = 96;

                    pre_sort_number = Common.Util.GetLastprocessedsortnumber(sort_number);
                    filteringflaglocation = sort_number == 0 ? sort_number : sort_number - totaldeletedrowcount;
                    listnumber = 0;
                    foreach (KeyValuePair<int, DataProcess.Crit_Inst_Operator> kvp in criteria_inst_operator_Dict/*dpsheet.crit_inst_operator_Dict*/)//(KeyValuePair<int, string> kvp in dpsheet.operatorsDict)
                    {
                        //  _log.Info("Starting instruction :" + kvp.Value.instruction + "Operator : " + kvp.Value.substituteoperator + "at ROW :" + kvp.Key.ToString());
                        if (RowCountChanged)
                        {
                            SaveDTtoTable(dt);
                            dt = LoadDataFromDB(sort_number, curwrkbuk, totaldeletedrowcount);// deletedrowcount
                            RowCountChanged = false;
                            pre_sort_number = Common.Util.GetLastprocessedsortnumber(sort_number, totaldeletedrowcount);//191 for delete issue
                            string directoryPath = GetProcessIdPath();// Path.Combine(Path.GetTempPath(), "QC4");
                            var dir = new DirectoryInfo(directoryPath);

                            foreach (var file in dir.EnumerateFiles("*.dp"))
                            {
                                file.Delete();
                            }
                            foreach (var file in dir.EnumerateFiles("*.txt"))
                            {
                                file.Delete();
                            }
                            //totaldeletedrowcount += deletedrowcount;
                            deletedrowcount = 0;
                            //  filteringflaglocation = sort_number == 0 ? sort_number : sort_number - totaldeletedrowcount;
                        }
                        else
                        {
                            //  filteringflaglocation = sort_number == 0 ? sort_number : sort_number - totaldeletedrowcount;
                            // if (sort_number != 0) filteringflaglocation += (sort_number - totaldeletedrowcount);
                        }
                        if (bgWorker.CancellationPending)
                        {
                            return;
                        }

                        switch (kvp.Value.instruction)
                        {
                            case Constants.DP.InstructionCALL:
                                //GetinstructionsByRow(true);
                                Excel.Range rowstartcs = DPSheet.Cells[kvp.Key, 1];
                                Excel.Range rowendcs = ExcelUtilForAddIn.EndxlRight(DPSheet.Cells[kvp.Key, 1]);
                                Excel.Range rowcs = DPSheet.Range[rowstartcs, rowendcs];
                                object[,] initvalueCS = rowcs.Value;
                                try
                                {
                                    HandleCALLStatement(kvp, sheet, dt, DataAfterProcess_sheet, sort_number, samplescount);
                                }
                                finally
                                {
                                    rowcs.Value = initvalueCS;
                                }

                                break;
                            case Constants.DP.InstructionFOR:
                                try
                                {
                                    int FORendrow = FindFORNEXTEndRow(kvp.Key);
                                    if (FORendrow > kvp.Key + 1)
                                    {
                                        DPSheet.Application.ScreenUpdating = false;
                                        Excel.Range forinstStart = DPSheet.Cells[kvp.Key + 1, Constants.DP.OnOffColumn];
                                        Excel.Range forinstEnd = DPSheet.Cells[FORendrow, Constants.DP.OnOffColumn];
                                        Dictionary<int, DataProcess.Crit_Inst_Operator> FOR_criteria_inst_operator_Dict = GetinstructionsByRow(forinstStart, forinstEnd);
                                        Excel.Range forparam1cell = DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column];
                                        Excel.Range forparam2cell = DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam2Column];
                                        Excel.Range forparam3cell = DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam3Column];
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
                                                if (RowCountChanged)
                                                {
                                                    SaveDTtoTable(dt);
                                                    dt = LoadDataFromDB(sort_number, curwrkbuk, deletedrowcount);
                                                    RowCountChanged = false;
                                                    pre_sort_number = Common.Util.GetLastprocessedsortnumber(sort_number, deletedrowcount);//191 for delete issue
                                                  //  string directoryPath = Path.Combine(Path.GetTempPath(), "QC4");
                                                    var dir = new DirectoryInfo(GetProcessIdPath());

                                                    foreach (var file in dir.EnumerateFiles("*.dp"))
                                                    {
                                                        file.Delete();
                                                    }
                                                    foreach (var file in dir.EnumerateFiles("*.txt"))
                                                    {
                                                        file.Delete();
                                                    }
                                                    //totaldeletedrowcount += deletedrowcount;
                                                    deletedrowcount = 0;
                                                    //  filteringflaglocation = sort_number == 0 ? sort_number : sort_number - totaldeletedrowcount;
                                                }
                                                Excel.Range rowstart = DPSheet.Cells[keyval.Key, 1];
                                                Excel.Range rowend = ExcelUtilForAddIn.EndxlRight(DPSheet.Cells[keyval.Key, 1]);
                                                Excel.Range row = DPSheet.Range[rowstart, rowend];

                                                object[,] initvalue = row.Value;

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
                                                        string value = cell.Value;
                                                        value = value.Replace(replacablechar, x.ToString());
                                                        cell.Application.EnableEvents = false;
                                                        cell.Value = value;
                                                        cell.Application.EnableEvents = true;
                                                    }

                                                }
                                                if (keyval.Value.instruction == "CALL")
                                                {
                                                    Excel.Range rowstartcsi = DPSheet.Cells[kvp.Key, 1];
                                                    Excel.Range rowendcsi = ExcelUtilForAddIn.EndxlRight(DPSheet.Cells[kvp.Key, 1]);
                                                    Excel.Range rowcsi = DPSheet.Range[rowstartcsi, rowendcsi];
                                                    object[,] initvalueCSi = rowcsi.Value;
                                                    try
                                                    {
                                                        HandleCALLStatement(keyval, sheet, dt, DataAfterProcess_sheet, sort_number, samplescount);
                                                    }
                                                    finally
                                                    {
                                                        rowcsi.Value = initvalueCSi;
                                                    }
                                                }
                                                else
                                                {
                                                    try { 
                                                    ExecuteInternal(keyval, sheet, dt/*, tablearray*/, sort_number, DataAfterProcess_sheet, samplescount, x);
                                                    }
                                                    finally
                                                    {
                                                        row.Value = initvalue;
                                                    }
                                                    processedStatement++;
                                                    int prgvalue = (processedStatement * 90) / totalOperation;
                                                    if (prgvalue >= 90)
                                                    {
                                                        prgvalue = 87;
                                                    }
                                                    if (progress != null)
                                                    {
                                                        StopProcess();
                                                        if (bgWorker.CancellationPending)
                                                        {
                                                            return;
                                                        }
                                                        //progress.OnWorkerMethodComplete(pogressbarcheckvaluefuntion((prgvalue + 30)), CommonResource.DP_PROGRESS_MSG_5);//IL_JP_MAM_007:4295055420

                                                    }
                                                }

                                                row.Value = initvalue;
                                            }

                                        }
                                    }
                                }
                                finally
                                {
                                    if (!DPSheet.Application.ScreenUpdating)
                                    {
                                        DPSheet.Application.ScreenUpdating = true;

                                    }

                                }
                                break;
                            default:
                                Excel.Range rowstartd = DPSheet.Cells[kvp.Key, 1];
                                Excel.Range rowendd = ExcelUtilForAddIn.EndxlRight(DPSheet.Cells[kvp.Key, 1]);
                                Excel.Range rowd = DPSheet.Range[rowstartd, rowendd];

                                object[,] initvalued = rowd.Value;
                                // progress.OnWorkerMethodComplete(50, CommonResource.DP_PROGRESS_MSG_10);//progress % with message
                                try
                                {
                                    if (bgWorker.CancellationPending)
                                    {
                                        return;
                                    }

                                    ExecuteInternal(kvp, sheet, dt/*, tablearray*/, sort_number, DataAfterProcess_sheet, samplescount);
                                }
                                catch (Exception ex)
                                {
                                    CriteriaQuerystring = string.Empty;
                                }
                                finally
                                {
                                    rowd.Value = initvalued;
                                }
                                //    progress.OnWorkerMethodComplete(5 + ((i * 100) / nPageCount), CommonResource.DP_PROGRESS_MSG_10);//progress % with message

                                break;
                        }
                        // progress.OnWorkerMethodComplete(50, CommonResource.DP_PROGRESS_MSG_10);//progress % with message
                        //curwrkbuk.Activate();
                        //_log.Info("PROCESSED INstruction :" + kvp.Value.instruction + "Operator : " + kvp.Value.substituteoperator + "at ROW :" + kvp.Key.ToString());
                        processedStatement++;
                        int progressValue = progressvalue + (processedStatement * 60) / totalOperation;
                        if (progressValue >= 90)
                        {
                            progressValue = 87;
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
                                progress.OnWorkerMethodComplete(pogressbarcheckvaluefuntion(progressValue), CommonResource.DP_PROGRESS_MSG_95);//IL_JP_MAM_007:4295055420
                            }
                        }
                    }
                    RowCountChanged = false;//191 16-12-19
                    //object[,] value_array = Make2DArray(dt);

                    //datarange = DataAfterProcess.Range[DataAfterProcess.Cells[4, 1], DataAfterProcess.Cells[3 + dt.Rows.Count + sort_number, dt.Columns.Count - 1]];
                    //datarange.Value = value_array;
                    sort_number = pre_sort_number;//   sort_number = Common.Util.GetLastprocessedsortnumber(sort_number); //191 commented for delete issue                   
                    SaveDTtoTable(dt);
                    deletedrowcount = 0;
                    firstlistitration = false;
                    totaldeletedrowcount = 0;
                    //progress.OnWorkerMethodComplete(95, CommonResource.DP_PROGRESS_MSG_95);//progress % with message
                }

                if (bgWorker.CancellationPending)
                {
                    return;
                }
                if (islistup == true)
                {
                    // switch (kvp.Value.instruction)// this is using  for criteria only-- DELETE AND OR -- like instruction  which may or may not in the row
                    foreach (ListupDetails listupdet in listupcount)
                    {
                        Excel.Worksheet worksheet = targetBook.Sheets[2];


                        Excel.Range categorycount = worksheet.Cells[listupdet.rowno, 7];//get each row 
                        categorycount.Value = listupdet.totatlrowcount;//set total row count


                    }
                    //  targetBook.Application.ScreenUpdating = true;
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
                        progress.OnWorkerMethodComplete(pogressbarcheckvaluefuntion(97), CommonResource.DP_PROGRESS_MSG_95);//progress % with message //IL_JP_MAM_007:4295055420
                    }
                }
                WriteCheckCrossEntries();

                if (progress != null)
                {
                    StopProcess();
                    if (bgWorker.CancellationPending)
                    {
                        return;
                    }
                    else
                    {
                        progress.OnWorkerMethodComplete(pogressbarcheckvaluefuntion(100), CommonResource.DP_PROGRESS_MSG_95, retainThread: true);//progress % with message
                    }                                                                                                             //DataProcess.currworkbook = DPSheet.Application.ActiveWorkbook; //IL_JP_MAM_007:4295055420
                }

                if (QC4Common.Global.Global.CheckOperationFlag != 0 && !isFrmStd)
                {
                    if (bgWorker.CancellationPending)
                    {
                        return;
                    }
                    sheet.Application.Interactive = false;

                }


                //

                //Excel.Range datarange = DataAfterProcess.Range[DataAfterProcess.Cells[4, 1], DataAfterProcess.Cells[3 + dt.Rows.Count, dt.Columns.Count - 1]];

                //datarange.Value = tablearray;
            }
            catch (Exception ex)
            {
                if (progress != null)
                {

                    progress.OnWorkerMethodComplete(pogressbarcheckvaluefuntion(100), CommonResource.DP_PROGRESS_MSG_95, retainThread: true); //IL_JP_MAM_007:4295055420

                }
                _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
            finally
            {
                if (progress != null)
                {
                    if (!bgWorker.CancellationPending)
                    {
                        progress.OnWorkerMethodComplete(pogressbarcheckvaluefuntion(100), CommonResource.DP_PROGRESS_MSG_95, retainThread: true); //IL_JP_MAM_007:4295055420
                    }
                }
                QC4Common.Common.CommonFlag.SetIsDataAfterUpdated(curwrkbuk, false);//DPSheet.Application.ActiveWorkbook
                try
                {

                    //StopProcess();
                    if (bgWorker.CancellationPending)
                    {

                    }
                    if (targetBook != null)
                        targetBook.Application.DisplayAlerts = true;//191 added for confirmation in listup while closing
                                                                //  targetBook.Activate();
                }
                catch { }


                try
                {
                    // COMWholeOperate.releaseComObject(ref excelOperate);
                    if (templateList != null)
                    {
                        try
                        {
                            COMWholeOperate.releaseComObject(ref templateList);
                        }
                        catch { }
                    }
                    if (targetBook != null)
                    {
                        try
                        {
                            COMWholeOperate.releaseComObject(ref targetBook);
                        }
                        catch { }
                    }
                    if (targetBooks != null)
                    {
                        try
                        {
                            COMWholeOperate.releaseComObject(ref targetBooks);
                        }
                        catch { }
                    }
                    // COMWholeOperate.releaseComObject(ref xlApp);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    if (!DPSheet.Application.ScreenUpdating)
                    {
                        DPSheet.Application.ScreenUpdating = true;
                    }
                }
                catch { }
            }
        }
        public int progressData(int progress)
        {
            if (progress > 80)
            {
                progress = 80;
            }
            else
            {

            }
            return progress;
        }
        private void WriteCheckCrossEntries()
        {
            string inputfilepath = Path.Combine(Path.GetTempPath(), "QC4\\" + "CheckCross.cross");// Path.Combine(GetProcessIdPath()+ "\\",  "CheckCross.cross");
            if (File.Exists(inputfilepath))
            {
                File.Delete(inputfilepath);
            }
            StreamWriter dpcheckcross = new StreamWriter(inputfilepath);
            foreach (string line in dpvariables)
            {
                dpcheckcross.WriteLine(line);
            }

            dpcheckcross.Close();
        }
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        bool listuploopfirsttime = true;
        int listuploopnum = 0;
        List<int> listuprowcountlist;
        private void ExecuteInternal(KeyValuePair<int, DataProcess.Crit_Inst_Operator> kvp, Excel.Worksheet sheet, DataTable dt/*, object[,] tablearray*/, int sort_number, Excel.Worksheet DataAfterProcess, int samplescount, int forNextIterator = 0, int CALLstatementRow = 0, bool triggerDelete = false)
        {
            samplescount = Common.Util.GetTotalRowcount();
            string cellitemid = "";
            string cellopertor = "";
            string cellvalue = "";
            bool isivcriteria = false; //Redmine id: 170984
            bool isnacriteria = false;//Redmine id: 170984
            bool isnotcriteriavalue = false;//Redmine id: 170984
            DataProcess.Crit_Inst_Operator objlistupcriteria;
            if (backgroundWorker.CancellationPending) { return; }
            if (DPQuestions == null)
            {
                DPQuestions = QC4Common.Util.DictionaryUtil.GetQuestions(DataProcess.currworkbook);
            }
            DataProcesses dp = new DataProcesses();
            dp.Questions = DPQuestions;
            if (dt != null)
            {
                using (dp)
                {
                    var dirPath = GetProcessIdPath(); //Path.Combine(Path.GetTempPath(), "QC4");  //@"c:\qcweb\txt\7";
                    dp.DataDirectoryPath = dirPath;
                    dp.SamplesCount = dt.Rows.Count;
                    dp.SortNumber = sort_number;
                    dp.ConnectionString = DBHelper.GetConnectionString(DataProcess.currworkbook);//DPSheet.Application.ActiveWorkbook
                    string NewQstnVariable = DPSheet.Cells[kvp.Key, Constants.DP.SubstituteVariableColumn].Text;//CriteriaVariableColumn
                    string CriteriaVariableText = DPSheet.Cells[kvp.Key, Constants.DP.CriteriaVariableColumn].Text;
                    string criteriaOperatorText = DPSheet.Cells[kvp.Key, Constants.DP.CriteriaOperatorColumn].Text;
                    string criteriaValueText = DPSheet.Cells[kvp.Key, Constants.DP.CriteriavalueColumn].Text;
                    try { cellopertor = criteriaOperatorText; } catch { }//for listup
                                                                         //trial criteria value as variable [Redmine id : 178707] -
                    string tempcriteriaValueText = criteriaValueText;
                    if (Definitions.VariableDictionary.ContainsKey(criteriaValueText))//criteriaValueText.StartsWith("[") && criteriaValueText.EndsWith("]")
                    {
                        string criteriatext = criteriaValueText;
                        //criteriatext= criteriatext.Remove(0, 1);
                        //criteriatext= criteriatext.Remove(criteriatext.Length - 1, 1);
                        // if (Definitions.VariableDictionary.ContainsKey(criteriatext))
                        {
                            criteriatext = "[" + criteriatext + "@" + Definitions.VariableDictionary[criteriatext].ItemId.ToString() + "@" + Definitions.VariableDictionary[criteriaValueText].AnswerType + "]";
                            criteriaValueText = criteriatext;
                        }

                    }
                    if (backgroundWorker.CancellationPending) { return; }
                    //end trial
                    if (Definitions.VariableDictionary.ContainsKey(CriteriaVariableText) && Definitions.VariableDictionary[CriteriaVariableText].AnswerType == Constants.AnswerType.FA && !Definitions.VariableDictionary.ContainsKey(tempcriteriaValueText))//[Redmine id : 175610]//https://app.gluemodel.com/#/project/task/4295056940
                    {
                        Excel.Range criValue = DPSheet.Cells[kvp.Key, Constants.DP.CriteriavalueColumn];
                        Excel.Range criVariable = DPSheet.Cells[kvp.Key, Constants.DP.CriteriaVariableColumn];

                        criteriaValueText = criValue.PrefixCharacter + criValue.Text;//  criteriaValueText = criValue.PrefixCharacter + criValue.Text;

                    }
                    if (backgroundWorker.CancellationPending) { return; }
                    //[,] criteriaarray = criteriarange.Value2;
                    IDataProcess dpOperator;
                    _INewQuestion newQuestion;
                    INewQuestionSectors sectors;
                    string delim = string.Empty;//for adding ( 
                    int opencount = 0;
                    int closecount = 0;
                    int diff = 0;
                    dp.DeletedRowCount = deletedrowcount;//for getting specific rows from limit if som deleted
                    switch (kvp.Value.instruction)// this is using  for criteria only-- DELETE AND OR -- like instruction  which may or may not in the row
                    {
                        case Constants.DP.InstructionAND:

                            if (!string.IsNullOrEmpty(CriteriaVariableText))
                            {
                                FetchCriteria(CriteriaVariableText, criteriaOperatorText, criteriaValueText, kvp, null, isnotcriteriavalue, isivcriteria, isnacriteria, false);
                            }

                            if (backgroundWorker.CancellationPending) { return; }

                            objlistupcriteria = new DataProcess.Crit_Inst_Operator();
                            objlistupcriteria.criteriavariable = CriteriaVariableText;// DPSheet.Cells[kvp.Key, Constants.DP.CriteriaVariableColumn].Text;
                            objlistupcriteria.instruction = cellopertor;
                            objlistupcriteria.substituteoperator = criteriaValueText;//DPSheet.Cells[kvp.Key, Constants.DP.CriteriavalueColumn].Text;;
                            objlistupcriteria.ListUpOpr = kvp.Value.instruction;
                            dpsheet.ListUp_Dict.Add(kvp.Key, objlistupcriteria);
                            break;
                        case Constants.DP.InstructionOR:
                            if (!string.IsNullOrEmpty(CriteriaVariableText))
                            {
                                FetchCriteria(CriteriaVariableText, criteriaOperatorText, criteriaValueText, kvp, null, isnotcriteriavalue, isivcriteria, isnacriteria, false);
                            }

                            if (backgroundWorker.CancellationPending) { return; }
                            objlistupcriteria = new DataProcess.Crit_Inst_Operator();
                            objlistupcriteria.criteriavariable = CriteriaVariableText;// DPSheet.Cells[kvp.Key, Constants.DP.CriteriaVariableColumn].Text;
                            objlistupcriteria.instruction = cellopertor;
                            objlistupcriteria.substituteoperator = criteriaValueText;//DPSheet.Cells[kvp.Key, Constants.DP.CriteriavalueColumn].Text;;
                            objlistupcriteria.ListUpOpr = kvp.Value.instruction;
                            dpsheet.ListUp_Dict.Add(kvp.Key, objlistupcriteria);

                            break;
                        case Constants.DP.InstructionDELETE:
                        case Constants.DP.InstructionLDEL:
                            //adding criteria left to delete instruction 
                            dpsheet.ListUp_Dict.Clear();//clear if not listup
                            IDataProcess dpDeleteEntity = dp.Add(DataProcessCode.DeleteData);
                            dpDeleteEntity.RunFlag = true;
                            _INewQuestion newdeleteQuestion = dpDeleteEntity.Questions.Add();
                            dp.SamplesCount = Common.Util.GetTotalRowCountForDeleteLDEL(sort_number);// Common.Util.GetTotalRowCountForDeleteLDEL(sort_number)< dt.Rows.Count? Common.Util.GetTotalRowCountForDeleteLDEL(sort_number): dt.Rows.Count;// dt.Rows.Count;// samplescount;// 191 changed   samplescount to  dt.Rows.Count
                            int sourceItemId = 0;
                            string ldelQuerystring = string.Empty;
                            if (backgroundWorker.CancellationPending) { return; }
                            if (kvp.Value.instruction == Constants.DP.InstructionDELETE)
                            {
                                sourceItemId = Definitions.VariableDictionary[CriteriaVariableText].ItemId;
                                NewQstnVariable = CriteriaVariableText;
                                newdeleteQuestion.ItemId = Definitions.VariableDictionary[NewQstnVariable].ItemId.ToString();
                                newdeleteQuestion.Name = NewQstnVariable;

                                newdeleteQuestion.QuestionType = QuestionType.SA;// Definitions.VariableDictionary[NewQstnVariable].AnswerType == Constants.AnswerType.MA ? QuestionType.MA : (Definitions.VariableDictionary[NewQstnVariable].AnswerType == Constants.AnswerType.SA ? QuestionType.SA : Definitions.VariableDictionary[NewQstnVariable].AnswerType == Constants.AnswerType.N ? QuestionType.N : QuestionType.FA);// Macromill.QCWeb.Tabulation.QuestionType.SA;
                                ((INewQuestion)newdeleteQuestion).ChangeExtension = GlobalsCommonConstant.fileExtension.dp;
                                newdeleteQuestion.SourceItemId = newdeleteQuestion.ItemId;

                                if (!string.IsNullOrEmpty(CriteriaVariableText))
                                {
                                    FetchCriteria(CriteriaVariableText, criteriaOperatorText, criteriaValueText, kvp, newdeleteQuestion, isnotcriteriavalue, isivcriteria, isnacriteria);
                                }


                            }
                            //if sampleid

                            if (kvp.Value.instruction == Constants.DP.InstructionLDEL)
                            {
                                string sampleid = "SAMPLEID";
                                NewQstnVariable = sampleid;
                                newdeleteQuestion.ItemId = Definitions.VariableDictionary["SAMPLEID"].ItemId.ToString();
                                newdeleteQuestion.Name = NewQstnVariable;// "SAMPLEID";
                                                                         //if(newdeleteQuestion.ItemId =="0") newdeleteQuestion.ItemId = "111111111";//trial codes
                                if (backgroundWorker.CancellationPending) { return; }                                   //setting SA Bcs Sectors will b null for FA
                                newdeleteQuestion.QuestionType = QuestionType.SA;// Definitions.VariableDictionary[NewQstnVariable].AnswerType == Constants.AnswerType.MA ? QuestionType.MA : (Definitions.VariableDictionary[NewQstnVariable].AnswerType == Constants.AnswerType.SA ? QuestionType.SA : Definitions.VariableDictionary[NewQstnVariable].AnswerType == Constants.AnswerType.N ? QuestionType.N : QuestionType.FA);// Macromill.QCWeb.Tabulation.QuestionType.SA;

                                //LDEL
                                Excel.Worksheet SheetData = ExcelUtilForAddIn.GetWorkSheetByCodeName(DataProcess.currworkbook, Constants.SheetCodeName.LDEL);// ExcelUtil.GetWorksheetByName("LDEL");
                                Excel.Range start = SheetData.Cells[2, 2];
                                Excel.Range end = ExcelUtilForAddIn.EndxlRight(start);
                                Excel.Range range = SheetData.get_Range(start, end);
                                StringBuilder ldelQuerystringbuilder = new StringBuilder();
                                if (string.IsNullOrEmpty(DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column].Text))
                                {
                                    foreach (Excel.Range r in range)
                                    {
                                        if (!string.IsNullOrEmpty(r.Text))
                                        {
                                            if (backgroundWorker.CancellationPending) { return; }
                                            start = SheetData.Cells[3, r.Column];
                                            end = ExcelUtilForAddIn.EndxlUp(start);
                                            Excel.Range rng = SheetData.get_Range(start, end);
                                            foreach (Excel.Range rr in rng)
                                            {
                                                if (!string.IsNullOrEmpty(rr.Text) && r.Column >= 2)
                                                {
                                                    ldelQuerystringbuilder.Append(newdeleteQuestion.ItemId + "=" + rr.Text + " | "); // ldelQuerystring += newdeleteQuestion.ItemId + "=" + rr.Text + " | ";
                                                }
                                            }

                                        }

                                    }
                                }
                                else
                                {
                                    int columnnum = 2;
                                    foreach (Excel.Range r in range)
                                    {
                                        if (r.Text == DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column].Text)
                                            break;
                                        columnnum++;
                                    }
                                    start = SheetData.Cells[3, columnnum];
                                    end = ExcelUtilForAddIn.EndxlUp(start);
                                    range = SheetData.get_Range(start, end);
                                    if (backgroundWorker.CancellationPending) { return; }
                                    if (range != null && range.Cells.Count > 1)
                                    {
                                        var objrabge = range.Value;
                                        foreach (object rangevalue in objrabge)//(Excel.Range r in range)
                                        {
                                            if (backgroundWorker.CancellationPending) { return; }
                                            string sampid = Convert.ToString(rangevalue);
                                            if (!string.IsNullOrEmpty(sampid))//(r.Text != null && r.Text != "")
                                            {
                                                ldelQuerystringbuilder.Append(newdeleteQuestion.ItemId + "=" + sampid + " | ");   //ldelQuerystring += newdeleteQuestion.ItemId + "=" + r.Text + " | "; //delQuerystring += newdeleteQuestion.ItemId + "=" + r.Text + " | ";  //newdeleteQuestion.ItemId                                    
                                            }
                                        }
                                    }
                                    else
                                    {
                                        string rangesampleid = Convert.ToString(range.Value);
                                        if (!string.IsNullOrEmpty(rangesampleid))//(r.Text != null && r.Text != "")
                                        {
                                            ldelQuerystringbuilder.Append(newdeleteQuestion.ItemId + "=" + rangesampleid + " | ");   //ldelQuerystring += newdeleteQuestion.ItemId + "=" + r.Text + " | "; //delQuerystring += newdeleteQuestion.ItemId + "=" + r.Text + " | ";  //newdeleteQuestion.ItemId                                    
                                        }
                                    }
                                }
                                //SCExcelClass.FIndCell();
                                ldelQuerystring = Convert.ToString(ldelQuerystringbuilder);
                                if (!string.IsNullOrEmpty(ldelQuerystring))
                                    ldelQuerystring = ldelQuerystring.Remove(ldelQuerystring.Length - 3); // delQuerystring = delQuerystring.Remove(delQuerystring.Length - 3);

                                if (backgroundWorker.CancellationPending) { return; }
                                delim = string.Empty;//for adding ( 
                                opencount = CriteriaQuerystring.Count(f => f == '(');
                                closecount = CriteriaQuerystring.Count(f => f == ')');
                                diff = opencount - closecount;
                                if (diff != 0) delim = ")"; // if (diff != 1) { delim = "))"; } else { delim = ")"; }
                                CriteriaQuerystring = (CriteriaQuerystring + delim);

                                ((INewQuestion)newdeleteQuestion).ChangeExtension = GlobalsCommonConstant.fileExtension.dp;
                                newdeleteQuestion.SourceItemId = newdeleteQuestion.ItemId;
                                INewQuestionSectors deletesectors = newdeleteQuestion.Sectors;
                                sectors = newdeleteQuestion.Sectors;
                                var virtualSector = sectors.Add(ldelQuerystring, true) as INewVirtualQuestionSector;
                                virtualSector.Alias = "1";

                                if (!string.IsNullOrEmpty(CriteriaVariableText))
                                {
                                    FetchCriteria(CriteriaVariableText, criteriaOperatorText, criteriaValueText, kvp, newdeleteQuestion, isnotcriteriavalue, isivcriteria, isnacriteria);
                                }


                            }




                            if (backgroundWorker.CancellationPending) { return; }
                            dp.Execute();
                            //omit dt = LoadDataFromDB(sort_number);
                            string filename = DataIoConstant.DELETE_FLAG_TEXT_FILE_NAME;// newdeleteQuestion.ItemId;
                            string deletefileoutput = dirPath + "\\" + filename;// + ".dp";
                            if (kvp.Value.instruction == Constants.DP.InstructionDELETE)
                            {
                                int itemidval = Definitions.VariableDictionary[CriteriaVariableText].ItemId;
                            }
                            //  ReadDataFromOutputFile(DataAfterProcess, deletefileoutput, Definitions.VariableDictionary[NewQstnVariable].ItemId);
                            GetFromDBAndDelete(DataAfterProcess, deletefileoutput, dt, sort_number, trigger: triggerDelete);
                            if (backgroundWorker.CancellationPending) { return; }
                            if (deletedrowcount > 0)
                            {
                                RowCountChanged = true;
                            }
                            //   datarange.ClearContents(); Commented by jijesh 09-09-2019
                            CriteriaQuerystring = string.Empty;
                            totaldeletedrowcount += deletedrowcount;
                            //deletedrowcount = 0;
                            break;

                        case Constants.DP.InstructionLISTUP:
                            DPSheet.Application.EnableEvents = false;
                            // try { DPSheet.Application.ScreenUpdating = true; } catch { }
                            string connectionstring = DBHelper.GetConnectionString(DataProcess.currworkbook);
                            // if(isFrmStd)
                            //{
                            //    templateList = targetBook.Sheets[1];
                            //}


                            //templateList.Cells[2, 2].Value = CommonResource.LISTUP_COMMAND_LINE_NUMBER;
                            //templateList.Cells[2, 3].Value = CommonResource.LISTUP_SAMPLE_ID;
                            //targetBook.Sheets[2].Cells[2, 2].Value = CommonResource.LISTUP_COMMAND_LINE_NUMBER;
                            //targetBook.Sheets[2].Cells[2, 3].Value = CommonResource.LISTUP_VARIABLE;
                            //targetBook.Sheets[2].Cells[2, 4].Value = CommonResource.LISTUP_COMPARISON_OPERATOR;
                            //targetBook.Sheets[2].Cells[2, 5].Value = CommonResource.LISTUP_CRITERIA_VALUE;
                            //targetBook.Sheets[2].Cells[2, 6].Value = CommonResource.LISTUP__INSTRUCTION;
                            //targetBook.Sheets[2].Cells[2, 7].Value = CommonResource.LISTUP_NO_OF_CASES;

                            //
                            if (backgroundWorker.CancellationPending) { return; }

                            // 191  adddition criteria
                            bool iscriterialistupflg = false;

                            bool[] filterringlistupFlg = new bool[samplescount];//samplescount //// int samplescount = Common.Util.GetTotalRowcount();
                                                                                // dp.Questions = Util.DictUpdate.GetQuestions(DataProcess.currworkbook);//DPSheet.Application.ActiveWorkbook
                                                                                //Perormance//dp.Questions = QC4Common.Util.DictionaryUtil.GetQuestions(DataProcess.currworkbook);

                            int rowcount = samplescount;// dp.SamplesCount;
                                                        //for (int fi=0;fi< dp.SamplesCount;fi++)
                                                        //{
                                                        //    filterringFlag[fi]=false;
                                                        //}
                            if (!string.IsNullOrEmpty(CriteriaVariableText))
                            {
                                FetchCriteria(CriteriaVariableText, criteriaOperatorText, criteriaValueText, kvp, null, isnotcriteriavalue, isivcriteria, isnacriteria, false);
                                iscriterialistupflg = true;

                                objlistupcriteria = new DataProcess.Crit_Inst_Operator();
                                objlistupcriteria.criteriavariable = CriteriaVariableText;
                                objlistupcriteria.instruction = cellopertor;
                                objlistupcriteria.substituteoperator = criteriaValueText;//DPSheet.Cells[kvp.Key, Constants.DP.CriteriavalueColumn].Text;;
                                dpsheet.ListUp_Dict.Add(kvp.Key, objlistupcriteria);
                            }


                            if (iscriterialistupflg)//if criteria true getting true fals 
                            {
                                //DataProcess.currworkbook)))
                                filterringlistupFlg = new Criteria(CriteriaQuerystring, "", dp.Questions).Filtering(DBHelper.GetConnectionString(DataProcess.currworkbook), dt: dt);//DPSheet.Application.ActiveWorkbook));
                                rowcount = filterringlistupFlg.Where(c => c).Count();
                            }
                            if (backgroundWorker.CancellationPending) { return; }
                            Excel.Range paramcell = DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column];
                            bool IsEmptyParams = false;
                            if (string.IsNullOrEmpty(paramcell.Text))
                            {
                                IsEmptyParams = true;
                            }
                            Excel.Range endcell = ExcelUtilForAddIn.EndxlRight(paramcell);
                            Excel.Range variablerange = DPSheet.Range[paramcell, endcell];

                            int outputcolumncount = IsEmptyParams ? 1 : variablerange.Count * 2;// Discarding Edit column
                            Excel.Range tempstart = templateList.Cells[2, 4];
                            Excel.Range tempend = templateList.Cells[2, 3 + outputcolumncount];
                            Excel.Range outputListHeader = templateList.Range[tempstart, tempend];
                            int x = 1;


                            List<int> VariablecolumnList = new List<int>();
                            List<int> variableIdlist = new List<int>();
                            if (backgroundWorker.CancellationPending) { return; }
                            ///////////////mixing issue  for bigger files  
                            if (listuploopfirsttime)//setting row count of all listup   
                            {
                                if (listuprowcountlist.Count == 0)
                                {
                                    listuprowcountlist.Add(listuprangestartindex);
                                }
                                else
                                {
                                    listuprowcountlist.Add(listuprowcountlist[listuploopnum] + rowcount);//need to add prev row count
                                }
                                listuprowcountlist.Add(listuprowcountlist[listuploopnum] + rowcount);//adding next writing  row no. for future listup usage
                            }
                            listuprangestartindex = listuprowcountlist[listuploopnum];

                            foreach (Excel.Range cell in outputListHeader)
                            {
                                //int xvalue = (x / 3) + 1;
                                int xvalue = (x / 2) + 1;
                                cell.ColumnWidth = 15;
                                //switch (x % 3)
                                switch (x % 2)
                                {
                                    case 1:
                                        cell.Value = CommonResource.LISTUP_VARIABLE + xvalue.ToString();//"Variable" + xvalue.ToString();
                                        VariablecolumnList.Add(x);
                                        break;
                                    case 0:
                                        xvalue--;
                                        cell.Value = CommonResource.LISTUP_VALUE + xvalue.ToString();//"Value" + xvalue.ToString();
                                        break;
                                }
                                x++;
                            }
                            if (backgroundWorker.CancellationPending) { return; }
                            int lastlistuprange = 0;
                            if (dpsheet.ListUp_Dict.Count > 0)//191  added if else ; else part was only der by jijesh
                            {

                                DataTable dtColumn = dt.DefaultView.ToTable(false, "sample_id"); //GetDataByColumn("sample_id", sort_number.ToString(), connectionstring);
                                DataTable newsampledt = new DataTable();
                                newsampledt.Columns.Add("sample_id");
                                for (int li = 0; li < dtColumn.Rows.Count; li++)//for sampleid
                                {
                                    //column setting logic here
                                    DataRow dr;
                                    int dtsortno = Convert.ToInt32(dt.Rows[li]["sort_no"].ToString());
                                    //[Redmine id : 176491] -
                                    if (iscriterialistupflg && !filterringlistupFlg[li])// if (iscriterialistupflg && !filterringlistupFlg[dtsortno - 1])//li + dp.SortNumber//if criteria true and success
                                    {
                                    }
                                    else
                                    {
                                        dr = newsampledt.NewRow();
                                        dr["sample_id"] = dtColumn.Rows[li][0];
                                        newsampledt.Rows.Add(dr);
                                    }
                                }
                                if (backgroundWorker.CancellationPending) { return; }
                                var loc = listupupdation(newsampledt);
                                if (newsampledt.Rows.Count > 0)
                                {

                                    Excel.Range commandcolstart = templateList.Cells[loc.Dtdata_StartPosi/*listuprangestartindex*/, 2];//  templateList.Cells[3, 2];
                                    Excel.Range commandcolstartend = templateList.Cells[loc.Dtdata_EndPosi/*listuprangestartindex + newsampledt.Rows.Count - 1*/, 2];// templateList.Cells[rowcount + 2, 2];  //templateList.Cells[dp.SamplesCount + 2, 2]
                                    Excel.Range commandcol = templateList.Range[commandcolstart, commandcolstartend];
                                    commandcol.Value = kvp.Key;

                                    object[,] sampleIdcolval = Make2DArray(newsampledt);//need to change range after each page
                                    Excel.Range sampleIdcol = templateList.Range[templateList.Cells[loc.Dtdata_StartPosi/*listuprangestartindex*/, 3], templateList.Cells[loc.Dtdata_EndPosi/*listuprangestartindex + newsampledt.Rows.Count - 1*/, 3]];// templateList.Range[templateList.Cells[3, 3], templateList.Cells[listuprangestartindex, 3]];//templateList.Range[templateList.Cells[3, 3], templateList.Cells[rowcount + 2, 3]];// templateList.Cells[dtColumn.Rows.Count + 2, 3]
                                    sampleIdcol.Value = sampleIdcolval;


                                    for (int xx = 0; xx < VariablecolumnList.Count; xx++)//setting variablename in full colum with value
                                    {
                                        if (backgroundWorker.CancellationPending) { return; }
                                        int lastroww = 1;
                                        string variableval = DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column + xx].Text;
                                        int variableId = 0;
                                        if (QC4Common.Common.Definitions.VariableDictionary.ContainsKey(variableval))
                                            variableId = QC4Common.Common.Definitions.VariableDictionary[variableval].ItemId;

                                        DataTable valuetable = null;//= GetDataByColumn("q_" + variableId.ToString(), sort_number.ToString(), connectionstring);
                                        if (variableId != 0)
                                        {
                                            // valuetable = GetDataByColumn("q_" + variableId.ToString(), sort_number.ToString(), connectionstring);
                                            string colName = "q_" + variableId.ToString();
                                            valuetable = dt.DefaultView.ToTable(false, colName);
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(variableval))
                                            {
                                                //   valuetable = GetDataByColumn("sample_id", sort_number.ToString(), connectionstring); //Select sample_id from data_after_process
                                                valuetable = dt.DefaultView.ToTable(false, "sample_id");
                                            }
                                        }
                                        DataTable newcolumndt = new DataTable();
                                        newcolumndt.Columns.Add("id");
                                        if (!string.IsNullOrEmpty(variableval))
                                        {
                                            for (int li = 0; li < valuetable.Rows.Count; li++)
                                            {
                                                if (backgroundWorker.CancellationPending) { return; }
                                                //column setting logic here
                                                DataRow dr;
                                                int dtsortno = Convert.ToInt32(dt.Rows[li]["sort_no"].ToString());
                                                //[Redmine id : 176491] -
                                                if (iscriterialistupflg && !filterringlistupFlg[li])//if (iscriterialistupflg && !filterringlistupFlg[dtsortno - 1])//li + dp.SortNumber//if criteria true and success
                                                {

                                                }
                                                else
                                                {
                                                    dr = newcolumndt.NewRow();
                                                    dr["id"] = valuetable.Rows[li][0];
                                                    newcolumndt.Rows.Add(dr);
                                                }
                                            }
                                            if (QC4Common.Common.Definitions.VariableDictionary.ContainsKey(variableval) && QC4Common.Common.Definitions.VariableDictionary[variableval].AnswerType == Constants.AnswerType.MA)
                                            {
                                                for (int i = 0; i < newcolumndt.Rows.Count; i++)
                                                {
                                                    string s = Convert.ToString(newcolumndt.Rows[i][0]);
                                                    if (!string.IsNullOrEmpty(s))
                                                        s = ParseMconvertdata(s.ToCharArray());
                                                    newcolumndt.Rows[i][0] = s;
                                                }
                                            }
                                            object[,] valuearray = Make2DArray(newcolumndt);

                                            lastroww = valuearray.GetLength(0);


                                            if (backgroundWorker.CancellationPending) { return; }

                                            Excel.Range variablecolstrt = templateList.Cells[loc.Dtdata_StartPosi/*listuprangestartindex*/, VariablecolumnList[xx] + 3];
                                            Excel.Range variablecolend = templateList.Cells[loc.Dtdata_EndPosi/*listuprangestartindex + newsampledt.Rows.Count - 1*/, VariablecolumnList[xx] + 3];//dp.SamplesCount
                                            Excel.Range variablecol = templateList.Range[variablecolstrt, variablecolend];


                                            variablecol.Value = variableval;

                                            Excel.Range valuecolstart = templateList.Cells[loc.Dtdata_StartPosi/*listuprangestartindex*/, VariablecolumnList[xx] + 4];



                                            Excel.Range valuecolend = templateList.Cells[loc.Dtdata_EndPosi/*listuprangestartindex + newcolumndt.Rows.Count - 1*/, VariablecolumnList[xx] + 4];//lastroww + 2
                                            Excel.Range valuecol = templateList.Range[valuecolstart, valuecolend];//need to change range after each page
                                            valuecol.Value = valuearray;
                                            lastlistuprange = valuearray.GetLength(0);
                                        }

                                    }



                                }
                                else { lastlistuprange = 0; }

                                if (listuploopfirsttime)
                                {
                                    Excel.Worksheet worksheet = targetBook.Sheets[2];
                                    foreach (KeyValuePair<int, DataProcess.Crit_Inst_Operator> item in dpsheet.ListUp_Dict)
                                    {
                                        worksheet.Cells[lastrow, 2].Value = item.Key;
                                        worksheet.Cells[lastrow, 3].Value = item.Value.criteriavariable;
                                        worksheet.Cells[lastrow, 4].Value = item.Value.instruction;
                                        worksheet.Cells[lastrow, 5].Value = item.Value.substituteoperator;
                                        worksheet.Cells[lastrow, 6].Value = item.Value.ListUpOpr;
                                        lastrow++;
                                    }

                                    Excel.Range categorycount = worksheet.Cells[lastrow - 1, 7];
                                    categorycount.Value = rowcount;

                                    worksheet.Cells[lastrow - 1, 2].Value = kvp.Key;
                                    worksheet.Cells[lastrow - 1, 6].Value = "LISTUP";

                                    ListupDetails ld = new ListupDetails();
                                    ld.rowno = lastrow - 1;
                                    ld.totatlrowcount = loc.datacount;
                                    listupcount.Add(ld);
                                }
                                else
                                {
                                    listupcount[listuploopnum].totatlrowcount = loc.datacount;
                                }

                            }
                            else
                            {
                                var loc = listupupdation(dt);
                                for (int xx = 0; xx < VariablecolumnList.Count; xx++)
                                {
                                    if (backgroundWorker.CancellationPending) { return; }
                                    Excel.Range variablecolstrt = templateList.Cells[loc.Dtdata_StartPosi/*listuprangestartindex*/, VariablecolumnList[xx] + 3];
                                    Excel.Range variablecolend = templateList.Cells[loc.Dtdata_EndPosi/*listuprangestartindex + dp.SamplesCount - 1*/, VariablecolumnList[xx] + 3];
                                    Excel.Range variablecol = templateList.Range[variablecolstrt, variablecolend];

                                    string variableval = DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column + xx].Text;
                                    variablecol.Value = variableval;

                                    Excel.Range valuecolstart = templateList.Cells[loc.Dtdata_StartPosi/*listuprangestartindex*/, VariablecolumnList[xx] + 4];
                                    Excel.Range valuecolend = templateList.Cells[loc.Dtdata_EndPosi/*listuprangestartindex + dp.SamplesCount - 1*/, VariablecolumnList[xx] + 4];
                                    Excel.Range valuecol = templateList.Range[valuecolstart, valuecolend];
                                    int variableId = 0;
                                    if (QC4Common.Common.Definitions.VariableDictionary.ContainsKey(variableval))
                                        variableId = QC4Common.Common.Definitions.VariableDictionary[variableval].ItemId;

                                    DataTable valuetable = new DataTable();//= GetDataByColumn("q_" + variableId.ToString(), sort_number.ToString(), connectionstring);
                                    if (variableId != 0)
                                    {
                                        // valuetable = GetDataByColumn("q_" + variableId.ToString(), sort_number.ToString(), connectionstring);
                                        string colName = "q_" + variableId.ToString();
                                        valuetable = dt.DefaultView.ToTable(false, colName);
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(variableval))
                                        {
                                            // valuetable = GetDataByColumn("sample_id", sort_number.ToString(), connectionstring);
                                            valuetable = dt.DefaultView.ToTable(false, "sample_id");
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(variableval))
                                    {
                                        if (QC4Common.Common.Definitions.VariableDictionary.ContainsKey(variableval) && QC4Common.Common.Definitions.VariableDictionary[variableval].AnswerType == Constants.AnswerType.MA)
                                        {
                                            for (int i = 0; i < valuetable.Rows.Count; i++)
                                            {
                                                string s = Convert.ToString(valuetable.Rows[i][0]);
                                                if (!string.IsNullOrEmpty(s))
                                                    s = ParseMconvertdata(s.ToCharArray());
                                                valuetable.Rows[i][0] = s;
                                            }
                                        }
                                        object[,] valuearray = Make2DArray(valuetable);
                                        valuecol.Value = valuearray;

                                        lastlistuprange = valuearray.GetLength(0);
                                        samplescount = valuetable.Rows.Count;
                                    }
                                }


                                if (backgroundWorker.CancellationPending) { return; }

                                DataTable dtColumn = GetDataByColumn("sample_id", sort_number.ToString(), connectionstring);


                                Excel.Range commandcolstart = templateList.Cells[loc.Dtdata_StartPosi/*listuprangestartindex*/, 2];//  templateList.Cells[3, 2];
                                Excel.Range commandcolstartend = templateList.Cells[loc.Dtdata_EndPosi/*listuprangestartindex + dtColumn.Rows.Count - 1*/, 2];// templateList.Cells[rowcount + 2, 2];  //templateList.Cells[dp.SamplesCount + 2, 2]
                                Excel.Range commandcol = templateList.Range[commandcolstart, commandcolstartend];
                                commandcol.Value = kvp.Key;

                                object[,] sampleIdcolval = Make2DArray(dtColumn);
                                Excel.Range sampleIdcol = templateList.Range[templateList.Cells[loc.Dtdata_StartPosi/*listuprangestartindex*/, 3], templateList.Cells[loc.Dtdata_EndPosi/*listuprangestartindex + dtColumn.Rows.Count - 1*/, 3]];//listuprangestartindex// templateList.Cells[dtColumn.Rows.Count + 2, 3]
                                                                                                                                                                                                                                                 // templateList.Range[templateList.Cells[3, 3], templateList.Cells[dtColumn.Rows.Count + 2, 3]];
                                sampleIdcol.Value = sampleIdcolval;
                                if (listuploopfirsttime)
                                {
                                    Excel.Worksheet worksheet = targetBook.Sheets[2];
                                    Excel.Range categorycount = worksheet.Cells[lastrow, 7];

                                    categorycount.Value = Common.Util.GetTotalRowcount();// samplescount;// dp.SamplesCount;

                                    worksheet.Cells[lastrow, 2].Value = kvp.Key;
                                    worksheet.Cells[lastrow, 6].Value = "LISTUP";

                                    ListupDetails ld = new ListupDetails();
                                    ld.rowno = lastrow;
                                    ld.totatlrowcount = loc.datacount;
                                    listupcount.Add(ld);
                                }
                                else
                                {
                                    listupcount[listuploopnum].totatlrowcount = loc.datacount;
                                }
                                lastrow++;
                            }
                            listuprangestartindex += lastlistuprange;//for getting last inserted column if more than limit
                            listuprowcountlist[listuploopnum] = listuprowcountlist[listuploopnum] + lastlistuprange;
                            listuploopnum++;
                            CriteriaQuerystring = string.Empty;
                            dpsheet.ListUp_Dict.Clear();//clear  listup criterias
                            if (backgroundWorker.CancellationPending) { return; }                           //  targetBook.Save();
                            DPSheet.Application.EnableEvents = true;
                            listnumber++;
                            //    targetBook.Application.ScreenUpdating = true;//4295061541
                            //try { DPSheet.Application.ScreenUpdating = false; } catch { }
                            break;

                        case Constants.DP.InstructionOMIT:
                        case Constants.DP.InstructionOMIT2:
                            dpsheet.ListUp_Dict.Clear();//clear if not listup
                            List<int> paramidlist = new List<int>();
                            List<DataTable> paramcolumnlist = new List<DataTable>();
                            if (kvp.Value.instruction == Constants.DP.InstructionOMIT)//Taking sepereate variable from each param cell
                            {
                                if (backgroundWorker.CancellationPending) { return; }
                                Excel.Range paramomit = DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column];
                                Excel.Range endcellomit = ExcelUtilForAddIn.EndxlRight(paramomit);
                                Excel.Range paramrangeomit = DPSheet.Range[paramomit, endcellomit];
                                int paramid = 0;
                                if (paramrangeomit.Columns.Count > 1)
                                {
                                    object[,] paramsarray = paramrangeomit.Value;//if single cell means error will through

                                    for (int j = 1; j <= paramsarray.GetLength(1); j++)
                                    {
                                        if (!string.IsNullOrEmpty(Convert.ToString(paramsarray[1, j])))
                                        {
                                            paramid = Definitions.VariableDictionary[paramsarray[1, j].ToString()].ItemId;
                                            paramidlist.Add(paramid);
                                        }
                                    }
                                }
                                else
                                {
                                    paramid = Definitions.VariableDictionary[paramrangeomit.Value].ItemId;
                                    paramidlist.Add(paramid);
                                }
                            }
                            else if (kvp.Value.instruction == Constants.DP.InstructionOMIT2)
                            {
                                int startitemid = Definitions.VariableDictionary[DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column].Text].ItemId;
                                int enditemid = Definitions.VariableDictionary[DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam2Column].Text].ItemId;

                                int startrow = Definitions.VariableDictionary[DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column].Text].RowNumber;
                                int endrow = Definitions.VariableDictionary[DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam2Column].Text].RowNumber;
                                List<QuestionSettings> omitList = Definitions.VariableDictionary.Values.Where(qstns => qstns.RowNumber >= startrow && qstns.RowNumber <= endrow).ToList();
                                //if (startitemid > enditemid)
                                //{
                                //    int temp = startitemid;
                                //    startitemid = enditemid;
                                //    enditemid = temp;
                                //}
                                //for (int j = startitemid; j <= enditemid; j++)
                                //{
                                //    paramidlist.Add(j);
                                //}
                                foreach (var item in omitList)
                                {
                                    //if(item.RowNumber )
                                    paramidlist.Add(item.ItemId);
                                }
                            }
                            if (backgroundWorker.CancellationPending) { return; }
                            // 191  adddition criteria
                            bool iscriteriaomitflg = false;
                            bool[] filterringomitFlg = new bool[samplescount];
                            //Perormance//dp.Questions = QC4Common.Util.DictionaryUtil.GetQuestions(DataProcess.currworkbook);// dp.Questions = QC4Common.Util.DictionaryUtil.GetQuestions(DPSheet.Application.ActiveWorkbook);

                            //for (int fi=0;fi< dp.SamplesCount;fi++)
                            //{
                            //    filterringFlag[fi]=false;
                            //}
                            if (!string.IsNullOrEmpty(CriteriaVariableText))
                            {
                                FetchCriteria(CriteriaVariableText, criteriaOperatorText, criteriaValueText, kvp, null, isnotcriteriavalue, isivcriteria, isnacriteria, false);
                                iscriteriaomitflg = true;
                            }

                            if (iscriteriaomitflg)//if criteria true getting true fals 
                            {
                                //string tablename = Definitions.VariableDictionary[CriteriaVariableText].QuestionFlag.Equals("New") ? "data_after_process" : "answers";

                                filterringomitFlg = new Criteria(CriteriaQuerystring, "", dp.Questions).Filtering(DBHelper.GetConnectionString(DataProcess.currworkbook), dt: dt);//, "data_after_process"//DPSheet.Application.ActiveWorkbook

                            }
                            if (backgroundWorker.CancellationPending) { return; }
                            dt = UpdateAndGetFromDB(DataAfterProcess, paramidlist, dt/*, tablearray*/, kvp.Value.instruction, iscriteriaomitflg, filterringomitFlg, sort_number);//for omit,omit2
                            CriteriaQuerystring = string.Empty;
                            //    dt = LoadDataFromDB();
                            break;

                    }


                    List<string> dpvariableparams = new List<string>();

                    switch (kvp.Value.substituteoperator)//kvp.Value
                    {

                        case Constants.DP.SubstituteOperatorRECODE:
                            dpsheet.ListUp_Dict.Clear();//clear if not listup
                            int sourceItemId = Definitions.VariableDictionary[DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column].Text].ItemId;
                            if (backgroundWorker.CancellationPending) { return; }
                            dpOperator = dp.Add(DataProcessCode.Recode);
                            dpOperator.RunFlag = true;
                            newQuestion = dpOperator.Questions.Add();
                            newQuestion.ItemId = Definitions.VariableDictionary[NewQstnVariable].ItemId.ToString();//"1463";
                            newQuestion.Name = NewQstnVariable;// Sheet.Cells[kvp.Key, Constants.DP.SubstituteVariableColumn].Text;//"NGID";

                            newQuestion.QuestionType = Definitions.VariableDictionary[NewQstnVariable].AnswerType == Constants.AnswerType.SA ? QuestionType.SA : QuestionType.MA;

                            ((INewQuestion)newQuestion).ChangeExtension = GlobalsCommonConstant.fileExtension.dp;
                            newQuestion.SourceItemId = sourceItemId.ToString();
                            newQuestion.SourceQuestionType = Definitions.VariableDictionary[DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column].Text].AnswerType == Constants.AnswerType.SA ? QuestionType.SA : QuestionType.MA;
                            dpvariableparams.Add(DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column].Text);
                            // dpvariableinfo = string.Format("{0}:{1}", NewQstnVariable, DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column].Text);
                            sectors = newQuestion.Sectors;
                            sectors.ParentQuestion.CategoryCount = (Definitions.VariableDictionary[NewQstnVariable]).CategoryCount;
                            if (backgroundWorker.CancellationPending) { return; }
                            for (int paramcount = 0; paramcount < Definitions.VariableDictionary[NewQstnVariable].CategoryCount; paramcount++)
                            {
                                if (backgroundWorker.CancellationPending) { return; }
                                if (!string.IsNullOrEmpty(DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam2Column + paramcount].Text))
                                {
                                    string celldata = DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam2Column + paramcount].Text;
                                    celldata = DataProcess.MinMaxAppendWithMinus(celldata, kvp.Key, Constants.DP.SubstituteParam1Column, DPSheet);
                                    string eqopr = "=";
                                    if (celldata.StartsWith("!") || celldata.StartsWith("<>"))
                                    {
                                        eqopr = "!=";
                                        celldata = celldata.Replace("!", "");
                                        celldata = celldata.Replace("<>", "");
                                    }

                                    sectors.Add(newQuestion.SourceItemId + eqopr + celldata);
                                    sectors.ParentQuestion.CategoryCount = (Definitions.VariableDictionary[NewQstnVariable]).CategoryCount;

                                }
                            }
                            //sectors.Add(newQuestion.SourceItemId + "=" + "DK");

                            // 191  adddition criteria
                            if (!string.IsNullOrEmpty(CriteriaVariableText))
                            {
                                FetchCriteria(CriteriaVariableText, criteriaOperatorText, criteriaValueText, kvp, newQuestion, isnotcriteriavalue, isivcriteria, isnacriteria);
                            }
                            if (backgroundWorker.CancellationPending) { return; }

                            //Perormance//dp.Questions = QC4Common.Util.DictionaryUtil.GetQuestions(DataProcess.currworkbook);//191 for Ma Recode issue
                            if (backgroundWorker.CancellationPending) { return; }
                            dp.Execute();
                            if (backgroundWorker.CancellationPending) { return; }
                            string processedfile = dirPath + "\\" + newQuestion.ItemId + ".dp";
                            int itemid = Convert.ToInt32(newQuestion.ItemId);
                            ReadDataFromOutputFile(DataAfterProcess, processedfile, itemid, dt/*, tablearray*/);
                            CriteriaQuerystring = string.Empty;

                            break;

                        case Constants.DP.SubstituteOperatorMIN:
                        case Constants.DP.SubstituteOperatorMAX:
                        case Constants.DP.SubstituteOperatorAVG:
                        case Constants.DP.SubstituteOperatorSUM:
                            dpsheet.ListUp_Dict.Clear();//clear if not listup                           
                            dpOperator = dp.Add(DataProcessCode.Group);
                            dpOperator.RunFlag = true;
                            newQuestion = dpOperator.Questions.Add();
                            newQuestion.ItemId = Definitions.VariableDictionary[NewQstnVariable].ItemId.ToString();//"1463";
                            newQuestion.Name = NewQstnVariable;// Sheet.Cells[kvp.Key, Constants.DP.SubstituteVariableColumn].Text;//"NGID";
                            if (backgroundWorker.CancellationPending) { return; }
                            newQuestion.QuestionType = Macromill.QCWeb.Tabulation.QuestionType.N;
                            ((INewQuestion)newQuestion).ChangeExtension = GlobalsCommonConstant.fileExtension.dp;
                            newQuestion.SourceItemId = newQuestion.ItemId;
                            string formulastring = string.Empty;
                            string funtionname = string.Empty;
                            string inputfilepath = Path.Combine(GetProcessIdPath()+ "\\", newQuestion.ItemId + ".dp.tmp");

                            switch (kvp.Value.substituteoperator)
                            {
                                case Constants.DP.SubstituteOperatorMIN:
                                    funtionname = "min";
                                    break;
                                case Constants.DP.SubstituteOperatorMAX:
                                    funtionname = "max";
                                    break;
                                case Constants.DP.SubstituteOperatorAVG:
                                    funtionname = "average";
                                    break;
                                case Constants.DP.SubstituteOperatorSUM:
                                    funtionname = "sum";
                                    break;
                            }
                            if (backgroundWorker.CancellationPending) { return; }
                            string dpPath = Path.Combine(GetProcessIdPath()+ "\\", newQuestion.ItemId + ".dp");
                            //byte[][] backup = new byte[backuppath.Length][];
                            //for (int ind = 0; ind < backuppath.Length; ++ind)
                            //{
                            //    backup[ind] = System.IO.File.ReadAllBytes(backuppath[ind]);
                            //    try
                            //    {
                            //        System.IO.File.Delete(backuppath[ind]);
                            //    }
                            //    catch (Exception ex)
                            //    { System.Diagnostics.Debug.WriteLine("StackTrace:{0}", ex.StackTrace); }
                            //}

                            if (File.Exists(dpPath))//[Redmine id : 178621] 
                            {
                                try
                                {
                                    System.IO.File.Delete(dpPath);
                                }
                                catch (Exception ex)
                                { System.Diagnostics.Debug.WriteLine("StackTrace:{0}", ex.StackTrace); }
                            }
                            if (backgroundWorker.CancellationPending) { return; }

                            Excel.Range param1 = DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column];
                            Excel.Range endcell = ExcelUtilForAddIn.EndxlRight(param1);
                            Excel.Range paramrange = DPSheet.Range[param1, endcell];


                            List<int> paramidlist = new List<int>();//191 edited for single  variable selected  then exception will come for object array conversion
                            if (paramrange.Columns.Count > 1)
                            {
                                object[,] paramsarray = paramrange.Value;

                                List<DataTable> paramcolumnlist = new List<DataTable>();
                                for (int j = 1; j <= paramsarray.GetLength(1); j++)
                                {
                                    int paramid = Definitions.VariableDictionary[paramsarray[1, j].ToString()].ItemId;

                                    paramidlist.Add(paramid);

                                }
                            }
                            else
                            {
                                int paramid = Definitions.VariableDictionary[paramrange.Value].ItemId;
                                paramidlist.Add(paramid);
                            }
                            if (backgroundWorker.CancellationPending) { return; }
                            // 191  adddition criteria
                            bool iscriteriaflg = false;
                            bool[] filterringFlg = new bool[samplescount];
                            //Perormance//dp.Questions = QC4Common.Util.DictionaryUtil.GetQuestions(DataProcess.currworkbook);// dp.Questions = QC4Common.Util.DictionaryUtil.GetQuestions(DPSheet.Application.ActiveWorkbook); ;// Questions questions = DictUpdate.GetQuestions(workBook);

                            //for (int fi=0;fi< dp.SamplesCount;fi++)
                            //{
                            //    filterringFlag[fi]=false;
                            //}
                            if (!string.IsNullOrEmpty(CriteriaVariableText))
                            {
                                FetchCriteria(CriteriaVariableText, criteriaOperatorText, criteriaValueText, kvp, newQuestion, isnotcriteriavalue, isivcriteria, isnacriteria);
                                iscriteriaflg = true;
                            }
                            if (backgroundWorker.CancellationPending) { return; }
                            if (iscriteriaflg)//if criteria true getting true fals 
                            {

                                filterringFlg = new Criteria(CriteriaQuerystring, "", dp.Questions).Filtering(DBHelper.GetConnectionString(DataProcess.currworkbook), dt: dt);//DPSheet.Application.ActiveWorkbook

                            }
                            int oitemid = Convert.ToInt32(newQuestion.ItemId);
                            StreamWriter groupwriter = new StreamWriter(inputfilepath);

                            for (int k = 0; k < dp.SamplesCount; k++)
                            {
                                int dtsortno = Convert.ToInt32(dt.Rows[k]["sort_no"].ToString());
                                //[Redmine id : 176491] -
                                if (iscriteriaflg && !filterringFlg[k])// if (iscriteriaflg && !filterringFlg[dtsortno - 1])//k + dp.SortNumber//if criteria true and success
                                {
                                    //Redmine id:175387; 175466
                                    groupwriter.WriteLine();// groupwriter.WriteLine(dt.Rows[k][oitemid + 1]);// groupwriter.WriteLine();//groupwriter.WriteLine(dt.Rows[k][oitemid + 1]);
                                }
                                else
                                {

                                    string args = string.Empty;
                                    foreach (int paramid in paramidlist)
                                    {
                                        //paramid == 0 ? paramid : paramid + 1
                                        string celldata = dt.Rows[k][paramid == 0 ? QC4Common.Common.Constants.VariableSampleId : QC4Common.Common.Constants.VariableSuffix + paramid].ToString(); //DataAfterProcess.Cells[4 + k, paramid + 1].Text;//paramid + 1
                                        if (!string.IsNullOrEmpty(celldata))
                                        {
                                            if (!string.Equals(celldata, "*") && !string.Equals(celldata, "**"))//https://app.gluemodel.com/#/project/task/4295064959
                                            {
                                                if (!string.IsNullOrEmpty(args))
                                                {
                                                    args += ",";
                                                }
                                                args += "(" + celldata + ")";
                                            }
                                        }
                                        if (backgroundWorker.CancellationPending) { return; }
                                    }
                                    string expression = string.IsNullOrEmpty(args) ? args : string.Format("{0}({1})", funtionname, args);

                                    //expression = expression.Replace("(*)", "");

                                    groupwriter.WriteLine(expression);
                                }
                            }
                            if (backgroundWorker.CancellationPending) { return; }
                            groupwriter.Close();
                            dp.Execute();
                            if (backgroundWorker.CancellationPending) { return; }

                            string grpfileoutput = dirPath + "\\" + newQuestion.ItemId + ".dp";
                            int grpitemid = Convert.ToInt32(newQuestion.ItemId);

                            ReadDataFromOutputFile(DataAfterProcess, grpfileoutput, grpitemid, dt/*, tablearray*/);
                            CriteriaQuerystring = string.Empty;
                            break;

                        case Constants.DP.SubstituteOperatorMCONVERT:
                            // _log.Info("MCONVERT Starting , Setting DP object for QC4");
                            {
                                if (backgroundWorker.CancellationPending) { return; }
                                dpsheet.ListUp_Dict.Clear();//clear if not listup
                                dpOperator = dp.Add(DataProcessCode.MConvert);
                                dpOperator.RunFlag = true;
                                newQuestion = dpOperator.Questions.Add();
                                newQuestion.ItemId = Definitions.VariableDictionary[NewQstnVariable].ItemId.ToString();//"1463";
                                newQuestion.Name = NewQstnVariable;// Sheet.Cells[kvp.Key, Constants.DP.SubstituteVariableColumn].Text;//"NGID";
                                _INewQuestionSector sectorUnmatch;

                                newQuestion.QuestionType = Macromill.QCWeb.Tabulation.QuestionType.MA;
                                ((INewQuestion)newQuestion).ChangeExtension = GlobalsCommonConstant.fileExtension.dp;
                                try
                                {
                                    newQuestion.CategoryCount = (Definitions.VariableDictionary[NewQstnVariable]).CategoryCount;
                                }
                                catch { }
                                sectors = newQuestion.Sectors;
                                List<string> srcParamIDList = new List<string>();
                                string celldata = DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam2Column].Text;
                                if (backgroundWorker.CancellationPending) { return; }
                                string eqopr = "=";
                                if (celldata.StartsWith("!"))
                                {
                                    eqopr = "!=";
                                    celldata = celldata.Replace("!", "");
                                }
                                if (celldata.EndsWith("-"))
                                {
                                    celldata += "1000";
                                }
                                if (celldata.StartsWith("-"))
                                {
                                    celldata = "1" + celldata;

                                }
                                if (backgroundWorker.CancellationPending) { return; }
                                //  _log.Info("MCONVERT Looping through the params");
                                for (int paramcount = 0; paramcount < Definitions.VariableDictionary[NewQstnVariable].CategoryCount; paramcount++)
                                {
                                    if (backgroundWorker.CancellationPending) { return; }
                                    string celltext = DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam3Column + paramcount].Text;
                                    if (!string.IsNullOrEmpty(celltext))
                                    {
                                        newQuestion.SourceItemId = Definitions.VariableDictionary[celltext].ItemId.ToString();
                                        srcParamIDList.Add(newQuestion.SourceItemId);
                                        dpvariableparams.Add(celltext);

                                        sectors.Add(newQuestion.SourceItemId + eqopr + celldata);
                                    }

                                }
                                if (backgroundWorker.CancellationPending) { return; }
                                //  _log.Info("MCONVERT Looping through the params FINISHED");
                                param1 = DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column];
                                if (param1.Text == "1")//somtimes this has to add after criteria
                                {
                                    (newQuestion as NewQuestions.NewQuestion).UnfitFlag = true;
                                    StringBuilder criteriaString = new StringBuilder();
                                    for (int j = 0; j < Definitions.VariableDictionary[NewQstnVariable].CategoryCount; ++j)
                                    {
                                        if (backgroundWorker.CancellationPending) { return; }
                                        //if (string.IsNullOrEmpty(bean.SrcItemId)) continue;
                                        if (criteriaString.Length > 0)
                                        {
                                            criteriaString.Append(" & ");
                                        }
                                        criteriaString.Append(srcParamIDList[j]);
                                        criteriaString.Append("=");
                                        criteriaString.Append("*");
                                    }
                                    sectorUnmatch = sectors.Add(criteriaString.ToString());
                                    sectorUnmatch.DataType = DataType.IVData;
                                }
                                if (backgroundWorker.CancellationPending) { return; }
                                // 191  adddition criteria
                                //  _log.Info("MCONVERT Adding filter Criteria");
                                if (!string.IsNullOrEmpty(CriteriaVariableText))
                                {
                                    FetchCriteria(CriteriaVariableText, criteriaOperatorText, criteriaValueText, kvp, newQuestion, isnotcriteriavalue, isivcriteria, isnacriteria);
                                }

                                //Perormance//try { dp.Questions = QC4Common.Util.DictionaryUtil.GetQuestions(DataProcess.currworkbook); } catch { }
                                //  _log.Info("MCONVERT Calling QCWEB Execute");
                                if (backgroundWorker.CancellationPending) { return; }
                                dp.Execute();
                                if (backgroundWorker.CancellationPending) { return; }
                                //  _log.Info("MCONVERT Calling QCWEB Execute COMPLETED");
                                string mconvertdata = dirPath + "\\" + newQuestion.ItemId + ".dp";
                                int item_id = Convert.ToInt32(newQuestion.ItemId);
                                // _log.Info("MCONVERT Calling DB update");
                                ReadDataFromOutputFile(DataAfterProcess, mconvertdata, item_id, dt, /*tablearray,*/ 0, true);
                                // _log.Info("MCONVERT Calling DB Update Completed");
                                CriteriaQuerystring = string.Empty;
                                break;
                            }
                        case Constants.DP.SubstituteOperatorADD1:
                        case Constants.DP.SubstituteOperatorMINUS1:
                        case Constants.DP.SubstituteOperatorADD2:
                        case Constants.DP.SubstituteOperatorMINUS2:
                        case Constants.DP.SubstituteOperatorEQUAL:
                        case Constants.DP.SubstituteOperatorADD3://191 
                            bool excludeflag = false;//Redmine id:178805
                            dpsheet.ListUp_Dict.Clear();//clear if not listup
                            dpOperator = dp.Add(DataProcessCode.ModifyData);
                            Excel.Range paramvariable = DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column];
                            dpOperator.RunFlag = true;
                            newQuestion = dpOperator.Questions.Add();
                            newQuestion.ItemId = Definitions.VariableDictionary[NewQstnVariable].ItemId.ToString();//"1463";
                            newQuestion.Name = NewQstnVariable;// Sheet.Cells[kvp.Key, Constants.DP.SubstituteVariableColumn].Text;//"NGID";
                            if (backgroundWorker.CancellationPending) { return; }
                            newQuestion.QuestionType = Definitions.VariableDictionary[NewQstnVariable].AnswerType == Constants.AnswerType.MA ? QuestionType.MA : QuestionType.SA;
                            ((INewQuestion)newQuestion).ChangeExtension = GlobalsCommonConstant.fileExtension.dp;

                            sectors = newQuestion.Sectors;
                            var virtualsectors = sectors.Add("", true) as INewVirtualQuestionSector;
                            switch (kvp.Value.substituteoperator)
                            {
                                case Constants.DP.SubstituteOperatorADD1://no need to add all values of variable ,only specified value in cell -need logic
                                    virtualsectors.EditMethod = EditMethod.APPEND;
                                    virtualsectors.ModifyDataEdit = ModifyDataEdit.ITEM;
                                    virtualsectors.Alias = Definitions.VariableDictionary[paramvariable.Text].ItemId.ToString();
                                    string paramval = DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam2Column].Text;
                                    if (string.IsNullOrEmpty(paramval))//if blank is specified adding range
                                    {
                                        paramval = "1-" + (Definitions.VariableDictionary[paramvariable.Text]).CategoryCount;
                                    }
                                    else
                                    {
                                        paramval = DataProcess.MinMaxAppendWithMinus(paramval, kvp.Key, Constants.DP.SubstituteVariableColumn, DPSheet);
                                    }
                                    virtualsectors.Add1paramvalue = GetCommaSeperated(paramval, NewQstnVariable);// paramval;// DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam2Column].Text;
                                    string add1query = Definitions.VariableDictionary[paramvariable.Text].ItemId.ToString() + "=" + paramval;//DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam2Column].Text
                                    virtualsectors.jointCategoryCount = (Definitions.VariableDictionary[paramvariable.Text]).CategoryCount;
                                    if (backgroundWorker.CancellationPending) { return; }
                                    //INewQuestionSectors Criteriasectors = newQuestion.Sectors;
                                    //var virtualcriteriaSector = Criteriasectors.Add(add1query, true) as INewVirtualQuestionSector;
                                    //virtualcriteriaSector.Alias = "1";
                                    break;
                                case Constants.DP.SubstituteOperatorMINUS1:
                                    virtualsectors.EditMethod = EditMethod.REMOVE;
                                    virtualsectors.ModifyDataEdit = ModifyDataEdit.ITEM;
                                    virtualsectors.Alias = Definitions.VariableDictionary[paramvariable.Text].ItemId.ToString();
                                    if (backgroundWorker.CancellationPending) { return; }
                                    paramval = DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam2Column].Text;
                                    if (string.IsNullOrEmpty(paramval))//if blank is specified adding range
                                    {
                                        paramval = "1-" + (Definitions.VariableDictionary[paramvariable.Text]).CategoryCount;
                                    }
                                    else
                                    {
                                        paramval = DataProcess.MinMaxAppendWithMinus(paramval, kvp.Key, Constants.DP.SubstituteVariableColumn, DPSheet);
                                    }
                                    if (backgroundWorker.CancellationPending) { return; }
                                    virtualsectors.Add1paramvalue = GetCommaSeperated(paramval, NewQstnVariable);// DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam2Column].Text;
                                    virtualsectors.Alias = Definitions.VariableDictionary[paramvariable.Text].ItemId.ToString();
                                    virtualsectors.jointCategoryCount = (Definitions.VariableDictionary[paramvariable.Text]).CategoryCount;

                                    break;
                                case Constants.DP.SubstituteOperatorADD2:
                                case Constants.DP.SubstituteOperatorMINUS2:
                                    if (backgroundWorker.CancellationPending) { return; }
                                    string[] paramlist = paramvariable.Text.Split(',', '/');
                                    string joinparams = string.Empty;
                                    joinparams = GetCommaSeperated(paramvariable.Text, NewQstnVariable);

                                    if (kvp.Value.substituteoperator == Constants.DP.SubstituteOperatorADD2)
                                    {
                                        virtualsectors.EditMethod = EditMethod.APPEND;
                                    }
                                    else
                                    {
                                        virtualsectors.EditMethod = EditMethod.REMOVE;
                                    }
                                    if (backgroundWorker.CancellationPending) { return; }
                                    virtualsectors.ModifyDataEdit = ModifyDataEdit.CATEGORY;
                                    virtualsectors.Alias = joinparams;
                                    //virtualsectors.Criteria.
                                    break;

                                case Constants.DP.SubstituteOperatorEQUAL:
                                    //    _log.Info("Starting correct data operation");
                                    virtualsectors.EditMethod = EditMethod.SUBSTITUTION; //191 
                                    virtualsectors.ModifyDataEdit = ModifyDataEdit.CATEGORY;// Definitions.VariableDictionary.ContainsKey(paramvariable.Text) ? ModifyDataEdit.ITEM : (Definitions.VariableDictionary.ContainsKey(paramvariable.Text) ? ModifyDataEdit.CATEGORY : ModifyDataEdit.FREE);//Definitions.VariableDictionary.ContainsKey(paramvariable.Text) ? ModifyDataEdit.ITEM : ModifyDataEdit.CATEGORY
                                    if (Definitions.VariableDictionary.ContainsKey(paramvariable.Text) && Definitions.VariableDictionary[NewQstnVariable].AnswerType != Constants.AnswerType.FA)
                                    {
                                        dpvariableparams.Add(paramvariable.Text);
                                        virtualsectors.ModifyDataEdit = ModifyDataEdit.ITEM;
                                    }

                                    if (backgroundWorker.CancellationPending) { return; }
                                    virtualsectors.Alias = virtualsectors.ModifyDataEdit == ModifyDataEdit.ITEM ? Definitions.VariableDictionary[paramvariable.Text].ItemId.ToString() :
                                        Definitions.VariableDictionary[NewQstnVariable].AnswerType == Constants.AnswerType.MA ? GetCommaSeperated(paramvariable.Text, NewQstnVariable) :
                                        Definitions.VariableDictionary[NewQstnVariable].AnswerType == Constants.AnswerType.SA ? GetNextValueForSA(paramvariable.Text, Definitions.VariableDictionary[NewQstnVariable].CategoryCount) : ((paramvariable.Text).Replace("＊", "*"));//[Redmine id : 174859] -


                                    if (string.Equals(virtualsectors.Alias, "DK"))
                                    {
                                        virtualsectors.Alias = string.Empty;
                                        virtualsectors.DataType = DataType.NAData;
                                    }
                                    else if (string.Equals(virtualsectors.Alias, "*"))
                                    {
                                        virtualsectors.DataType = DataType.IVData;
                                    }
                                    if (virtualsectors.ModifyDataEdit != ModifyDataEdit.ITEM)//191  edited for corrrect data issue
                                    {//Definitions.VariableDictionary[NewQstnVariable].AnswerType == Constants.AnswerType.MA ? QuestionType.MA : QuestionType.SA;
                                        if (Definitions.VariableDictionary[NewQstnVariable].AnswerType == Constants.AnswerType.FA || Definitions.VariableDictionary[NewQstnVariable].AnswerType == Constants.AnswerType.N)//(newQuestion.QuestionType == QuestionType.N || newQuestion.QuestionType == QuestionType.FA)
                                            virtualsectors.ModifyDataEdit = ModifyDataEdit.FREE;
                                    }
                                    if (backgroundWorker.CancellationPending) { return; }
                                    //  _log.Info("ADDED Sectors");
                                    break;
                                case Constants.DP.SubstituteOperatorADD3://191 
                                    int add3categorycount = 0;
                                    dpsheet.ListUp_Dict.Clear();//clear if not listup
                                                                //virtualsectors.EditMethod = EditMethod.APPEND;
                                                                //virtualsectors.ModifyDataEdit = ModifyDataEdit.ITEM;
                                                                //virtualsectors.Alias = Definitions.VariableDictionary[paramvariable.Text].ItemId.ToString();
                                    string exclude = string.IsNullOrEmpty(DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column].Text) ? "-1" : DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column].Text;
                                    if (string.IsNullOrEmpty(exclude) || exclude.Equals("-1"))
                                        return;
                                    int excludevalue = Convert.ToInt32(exclude);//need chek for null
                                    Excel.Range add3param1 = DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam2Column];
                                    Excel.Range endcelladd3 = ExcelUtilForAddIn.EndxlRight(add3param1);
                                    Excel.Range add3paramrange = DPSheet.Range[add3param1, endcelladd3];
                                    if (backgroundWorker.CancellationPending) { return; }
                                    if (add3paramrange.Columns.Count > 1)
                                    {
                                        object[,] add3paramsarray = add3paramrange.Value;

                                        List<int> add3paramidlist = new List<int>();

                                        virtualsectors.EditMethod = EditMethod.APPEND;
                                        virtualsectors.ModifyDataEdit = ModifyDataEdit.ITEM;
                                        virtualsectors.Alias = Definitions.VariableDictionary[add3paramsarray[1, 1].ToString()].ItemId.ToString();
                                        dpvariableparams.Add(add3paramsarray[1, 1].ToString());
                                        virtualsectors.jointCategoryCount = -1;
                                        virtualsectors.Add3Exludesettings = excludevalue;
                                        add3categorycount += (Definitions.VariableDictionary[add3paramsarray[1, 1].ToString()]).CategoryCount;
                                        for (int j = 2; j <= add3paramsarray.GetLength(1); j++)
                                        {
                                            if (backgroundWorker.CancellationPending) { return; }
                                            if (!string.IsNullOrEmpty(Convert.ToString(add3paramsarray[1, j])))
                                            {
                                                virtualsectors = sectors.Add("", true) as INewVirtualQuestionSector;

                                                //paramid = Definitions.VariableDictionary[add3paramsarray[1, j].ToString()].ItemId;
                                                //add3paramidlist.Add(paramid);
                                                virtualsectors.EditMethod = EditMethod.APPEND;
                                                virtualsectors.ModifyDataEdit = ModifyDataEdit.ITEM;
                                                virtualsectors.Alias = Definitions.VariableDictionary[add3paramsarray[1, j].ToString()].ItemId.ToString();
                                                dpvariableparams.Add(add3paramsarray[1, j].ToString());
                                                virtualsectors.jointCategoryCount = -1;
                                                virtualsectors.Add3Exludesettings = excludevalue;
                                                //virtualsectors = sectors.Add("", true) as INewVirtualQuestionSector;
                                                add3categorycount += (Definitions.VariableDictionary[add3paramsarray[1, j].ToString()]).CategoryCount;

                                            }
                                        }
                                    }

                                    else
                                    {
                                        virtualsectors.EditMethod = EditMethod.APPEND;
                                        virtualsectors.ModifyDataEdit = ModifyDataEdit.ITEM;
                                        virtualsectors.Alias = Definitions.VariableDictionary[add3paramrange.Text].ItemId.ToString();
                                        virtualsectors.jointCategoryCount = -1;
                                        virtualsectors.Add3Exludesettings = excludevalue;
                                        add3categorycount += (Definitions.VariableDictionary[add3paramrange.Text]).CategoryCount;
                                        dpvariableparams.Add(add3paramrange.Text);
                                    }
                                    if (backgroundWorker.CancellationPending) { return; }
                                    sectors.ParentQuestion.CategoryCount = add3categorycount;
                                    param1 = DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column];//Redmine id:178805
                                    excludeflag = param1.Text == "0" ? true : false;//Redmine id:178805
                                    break;
                            }
                            //_log.Info("Updating Questions");
                            //Perormance//dp.Questions = QC4Common.Util.DictionaryUtil.GetQuestions(DataProcess.currworkbook);// dp.Questions = QC4Common.Util.DictionaryUtil.GetQuestions(DPSheet.Application.ActiveWorkbook);



                            // 191  adddition criteria
                            //  _log.Info("Updating Criteria");
                            if (!string.IsNullOrEmpty(CriteriaVariableText))
                            {
                                FetchCriteria(CriteriaVariableText, criteriaOperatorText, criteriaValueText, kvp, newQuestion, isnotcriteriavalue, isivcriteria, isnacriteria);
                            }
                            if (backgroundWorker.CancellationPending) { return; }
                            dp.Execute();
                            if (backgroundWorker.CancellationPending) { return; }
                            //_log.Info("DP->EXECUTE--- COMPLETED----");
                            string add1data = dirPath + "\\" + newQuestion.ItemId + ".dp";
                            //  string txtPath = Path.Combine(Path.GetTempPath(), @"QC4\" + newQuestion.ItemId + ".txt");
                            int itemId = Convert.ToInt32(newQuestion.ItemId);

                            //if (!File.Exists(add1data) && File.Exists(txtPath))
                            //{
                            //    add1data = txtPath;
                            //}
                            if (backgroundWorker.CancellationPending) { return; }
                            if (File.Exists(add1data))
                            {//Redmine id:178805
                                ReadDataFromOutputFile(DataAfterProcess, add1data, itemId, dt, /*tablearray,*/ 0, newQuestion.QuestionType == QuestionType.MA, null, excludeflag, Definitions.VariableDictionary[NewQstnVariable].AnswerType == Constants.AnswerType.FA);

                                // ReadDataFromOutputFile(DataAfterProcess, add1data, itemId, dt, /*tablearray,*/ 0, newQuestion.QuestionType == QuestionType.MA, null, param1.Text == "0" ? true : false);

                            }
                            if (backgroundWorker.CancellationPending) { return; }
                            CriteriaQuerystring = string.Empty;
                            break;

                        case Constants.DP.SubstituteOperatorCLASS:
                            dpsheet.ListUp_Dict.Clear();//clear if not listup
                            dpOperator = dp.Add(DataProcessCode.Class);
                            dpOperator.RunFlag = true;
                            newQuestion = dpOperator.Questions.Add();
                            newQuestion.ItemId = Definitions.VariableDictionary[NewQstnVariable].ItemId.ToString();
                            newQuestion.Name = NewQstnVariable;
                            newQuestion.QuestionType = Macromill.QCWeb.Tabulation.QuestionType.SA;
                            int sourceQstnId = Definitions.VariableDictionary[DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column].Text].ItemId;
                            newQuestion.SourceItemId = sourceQstnId.ToString();
                            dpvariableparams.Add(DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column].Text);
                            ((INewQuestion)newQuestion).ChangeExtension = GlobalsCommonConstant.fileExtension.dp;
                            sectors = newQuestion.Sectors;
                            if (backgroundWorker.CancellationPending) { return; }
                            //ewPageBean.ConditionItemInfoPageBean = FindItemInfo(decimal.Parse(newPageBean.dataProcessNewItemBean.SrcItemId), itemList);
                            //queries = classLogic.GetClassQueryList(newPageBean);
                            List<string> queries = new List<string>();
                            string param2 = DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam2Column].Text;
                            string param3 = DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam3Column].Text;
                            int param2Val = string.IsNullOrEmpty(param2) ? 0 : Convert.ToInt32(param2);
                            string query = "{0}{1}{2}&{3}{4}{5}";
                            string upperlimit = string.Empty;
                            string lowerlimit = string.Empty;
                            string operatorlower = string.Empty;
                            string operatorupper = string.Empty;
                            switch (param2Val)
                            {
                                case 1:
                                    operatorlower = ">=";
                                    operatorupper = "<=";
                                    break;
                                case 2:
                                    operatorlower = ">=";
                                    operatorupper = "<";
                                    break;
                                case 3:
                                    operatorlower = ">";
                                    operatorupper = "<=";
                                    break;
                                case 4:
                                    operatorlower = ">";
                                    operatorupper = "<";
                                    break;

                            }
                            if (backgroundWorker.CancellationPending) { return; }
                            if (param3.Equals("2"))
                            {
                                dpOperator.IsTreatasZero = true;
                            }
                            for (int paramcount = 1; paramcount <= Definitions.VariableDictionary[NewQstnVariable].CategoryCount; paramcount++)
                            {
                                if (backgroundWorker.CancellationPending) { return; }
                                string paramNvalue = DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam3Column + paramcount].Text;
                                if (paramNvalue.StartsWith("!"))
                                {
                                    dpOperator.ReverseIsTrue = true;
                                    paramNvalue = paramNvalue.Replace("!", "");
                                }
                                if (paramNvalue.Contains("(-"))
                                {
                                    paramNvalue = paramNvalue.Replace("(-", "@");
                                }
                                if (paramNvalue.Contains(")"))
                                {
                                    paramNvalue = paramNvalue.Replace(")", "");
                                }
                                if (paramNvalue.Contains("("))//191 added for trimming '('
                                {
                                    paramNvalue = paramNvalue.Replace("(", "");
                                }
                                if (!string.IsNullOrEmpty(paramNvalue))
                                {
                                    string[] splitrange = paramNvalue.Split('-');
                                    if (splitrange.Length == 1)
                                    {
                                        lowerlimit = splitrange[0];
                                        upperlimit = splitrange[0];

                                    }
                                    if (splitrange.Length == 2)
                                    {
                                        lowerlimit = !string.IsNullOrEmpty(splitrange[0]) ? splitrange[0] : double.MinValue.ToString("r"); ;
                                        upperlimit = !string.IsNullOrEmpty(splitrange[1]) ? splitrange[1] : double.MaxValue.ToString("r");
                                        // query = string.Format("{0}{1}{2}&{3}{4}{5}", newQuestion.SourceItemId, operatorlower, lowerlimit, newQuestion.SourceItemId, operatorupper, upperlimit);
                                    }
                                    lowerlimit = lowerlimit.Replace("@", "-");
                                    upperlimit = upperlimit.Replace("@", "-");
                                    query = string.Format("{0}{1}{2}&{3}{4}{5}", newQuestion.SourceItemId, operatorlower, lowerlimit, newQuestion.SourceItemId, operatorupper, upperlimit);

                                    sectors.Add(query);

                                }
                            }
                            if (backgroundWorker.CancellationPending) { return; }
                            // 191  adddition criteria
                            bool iscriteriaclass = false;
                            bool[] filterringFlagClass = new bool[samplescount];
                            if (!string.IsNullOrEmpty(CriteriaVariableText))
                            {
                                FetchCriteria(CriteriaVariableText, criteriaOperatorText, criteriaValueText, kvp, newQuestion, isnotcriteriavalue, isivcriteria, isnacriteria);
                                iscriteriaclass = true;
                            }
                            if (backgroundWorker.CancellationPending) { return; }
                            //Perormance//try { dp.Questions = QC4Common.Util.DictionaryUtil.GetQuestions(DataProcess.currworkbook); } catch { }
                            if (backgroundWorker.CancellationPending) { return; }
                            dp.Execute();
                            if (backgroundWorker.CancellationPending) { return; }
                            string classoutput = dirPath + "\\" + newQuestion.ItemId + ".dp";
                            int classsrcId = Convert.ToInt32(newQuestion.ItemId);
                            if (iscriteriaclass)//if criteria true getting true fals 
                            {
                                filterringFlagClass = new Criteria(CriteriaQuerystring, "", dp.Questions).Filtering(DBHelper.GetConnectionString(DataProcess.currworkbook), dt: dt);//(DPSheet.Application.ActiveWorkbook

                            }
                            if (backgroundWorker.CancellationPending) { return; }
                            ReadDataFromOutputFileForClass(DataAfterProcess, classoutput, classsrcId, dt, iscriteriaclass, filterringFlagClass);

                            CriteriaQuerystring = string.Empty;
                            break;
                        case Constants.DP.SubstituteOperatorCOUNT:
                            dpsheet.ListUp_Dict.Clear();//clear if not listup
                            dpOperator = Definitions.VariableDictionary[NewQstnVariable].AnswerType == Constants.AnswerType.N ? dp.Add(DataProcessCode.ResponseCount) : dp.Add(DataProcessCode.CategorizeResponseCount);
                            dpOperator.RunFlag = true;
                            newQuestion = dpOperator.Questions.Add();
                            newQuestion.ItemId = Definitions.VariableDictionary[NewQstnVariable].ItemId.ToString();
                            newQuestion.Name = NewQstnVariable;
                            newQuestion.QuestionType = Definitions.VariableDictionary[NewQstnVariable].AnswerType == Constants.AnswerType.N ? Macromill.QCWeb.Tabulation.QuestionType.N : Macromill.QCWeb.Tabulation.QuestionType.SA;
                            int paramQstnId = Definitions.VariableDictionary[DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column].Text].ItemId;
                            dpvariableparams.Add(DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column].Text);
                            newQuestion.SourceItemId = paramQstnId.ToString();
                            newQuestion.CategoryCount = (Definitions.VariableDictionary[DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column].Text]).CategoryCount;//191 added for ! issue --cat
                            string param2value = DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam2Column].Text;
                            param2value = DataProcess.MinMaxAppendWithMinus(param2value, kvp.Key, Constants.DP.SubstituteParam1Column, DPSheet);
                            ((INewQuestion)newQuestion).ChangeExtension = GlobalsCommonConstant.fileExtension.dp;
                            sectors = newQuestion.Sectors;
                            if (backgroundWorker.CancellationPending) { return; }
                            sectors.Add(newQuestion.SourceItemId + "=" + param2value);
                            List<List<NData.ValueRange>> cateRangeList = new List<List<NData.ValueRange>>();
                            for (int paramcount = 0; paramcount < Definitions.VariableDictionary[NewQstnVariable].CategoryCount; paramcount++)
                            {
                                if (backgroundWorker.CancellationPending) { return; }
                                List<NData.ValueRange> categryParamList = new List<NData.ValueRange>();
                                string paramNvalue = DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam3Column + paramcount].Text;
                                if (!string.IsNullOrEmpty(paramNvalue))
                                {
                                    paramNvalue = DataProcess.MinMaxAppendWithMinus(paramNvalue, kvp.Key, Constants.DP.SubstituteParam1Column);
                                    string[] splitcommaslash = GetCommaSeperated(paramNvalue, DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column].Text).Split(',', '/'); //GetCommaSeperated(paramvariable.Text, NewQstnVariable);//  string[] splitcommaslash = paramNvalue.Split(',', '/');
                                    foreach (string criteria in splitcommaslash)
                                    {

                                        string[] splitrange = criteria.Split('-');
                                        lowerlimit = splitrange[0];
                                        upperlimit = splitrange[0];

                                        if (splitrange.Length == 2)
                                        {
                                            upperlimit = splitrange[1];

                                        }
                                        int minval = Convert.ToInt32(lowerlimit);
                                        int maxval = Convert.ToInt32(upperlimit);
                                        NData.ValueRange catrange = new NData.ValueRange(minval, maxval);
                                        categryParamList.Add(catrange);

                                    }
                                    cateRangeList.Add(categryParamList);



                                }
                            }
                            if (backgroundWorker.CancellationPending) { return; }
                            ((INewQuestion)newQuestion).CountSectorRange = cateRangeList;

                            // 191  adddition criteria
                            if (!string.IsNullOrEmpty(CriteriaVariableText))
                            {
                                FetchCriteria(CriteriaVariableText, criteriaOperatorText, criteriaValueText, kvp, newQuestion, isnotcriteriavalue, isivcriteria, isnacriteria);
                            }

                            //Perormance//try { dp.Questions = QC4Common.Util.DictionaryUtil.GetQuestions(DataProcess.currworkbook); } catch { }
                            if (backgroundWorker.CancellationPending) { return; }
                            dp.Execute();
                            if (backgroundWorker.CancellationPending) { return; }
                            string countNoutput = dirPath + "\\" + newQuestion.ItemId + ".dp";

                            ReadDataFromOutputFile(DataAfterProcess, countNoutput, Definitions.VariableDictionary[NewQstnVariable].ItemId, dt/*, tablearray*/);
                            CriteriaQuerystring = string.Empty;
                            break;
                        case Constants.DP.SubstituteOperatorMTOS:                 //191    
                            dpsheet.ListUp_Dict.Clear();//clear if not listup
                            dp.QcWebId = 7;

                            if (backgroundWorker.CancellationPending) { return; }
                            IDataProcess dpMtoSEntity = dp.Add(DataProcessCode.MtoS);
                            dpMtoSEntity.RunFlag = true;
                            _INewQuestion newMtoSQuestion = dpMtoSEntity.Questions.Add();
                            newMtoSQuestion.ItemId = Definitions.VariableDictionary[NewQstnVariable].ItemId.ToString();//"1463";
                            newMtoSQuestion.Name = NewQstnVariable;// Sheet.Cells[kvp.Key, Constants.DP.SubstituteVariableColumn].Text;//"NGID";
                            newMtoSQuestion.CategoryCount = (Definitions.VariableDictionary[NewQstnVariable]).CategoryCount;
                            newMtoSQuestion.QuestionType = Macromill.QCWeb.Tabulation.QuestionType.SA;
                            ((INewQuestion)newMtoSQuestion).ChangeExtension = GlobalsCommonConstant.fileExtension.dp;
                            newMtoSQuestion.SourceItemId = newMtoSQuestion.ItemId;


                            dpvariableparams.Add(DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column].Text);

                            INewQuestionSectors mtossectors = newMtoSQuestion.Sectors;
                            //need to recheck 

                            mtossectors.Add(string.Format("{0}=1-{1}", Definitions.VariableDictionary[DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column].Text].ItemId, Definitions.VariableDictionary[NewQstnVariable].CategoryCount));

                            string index = string.IsNullOrEmpty(DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam2Column].Text) ? "0" : DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam2Column].Text;
                            if (string.IsNullOrEmpty(index) || index == "0")
                                return;
                            int SelectMethod = Convert.ToInt32(index);//need chek for null

                            switch (SelectMethod)
                            {
                                case (int)GlobalsCommonConstant.MtoS_SelectMethod.BEFORE:
                                    ((INewQuestion)newMtoSQuestion).SelectedMethod = GlobalsCommonConstant.MtoS_SelectMethod.BEFORE;
                                    break;
                                case (int)GlobalsCommonConstant.MtoS_SelectMethod.AFTER:
                                    ((INewQuestion)newMtoSQuestion).SelectedMethod = GlobalsCommonConstant.MtoS_SelectMethod.AFTER;
                                    break;
                                case (int)GlobalsCommonConstant.MtoS_SelectMethod.RANDOM:
                                    ((INewQuestion)newMtoSQuestion).SelectedMethod = GlobalsCommonConstant.MtoS_SelectMethod.RANDOM;
                                    break;
                            }
                            // 191  adddition criteria
                            if (!string.IsNullOrEmpty(CriteriaVariableText))
                            {
                                FetchCriteria(CriteriaVariableText, criteriaOperatorText, criteriaValueText, kvp, newMtoSQuestion, isnotcriteriavalue, isivcriteria, isnacriteria);
                            }

                            //Perormance//try { dp.Questions = QC4Common.Util.DictionaryUtil.GetQuestions(DataProcess.currworkbook); } catch { }
                            if (backgroundWorker.CancellationPending) { return; }
                            dp.Execute();
                            if (backgroundWorker.CancellationPending) { return; }
                            string filename = newMtoSQuestion.ItemId;// (Definitions.VariableDictionary[DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column].Text].ItemId).ToString();
                            string mtosfileoutput = dirPath + "\\" + filename + ".dp";
                            int itemidval = Definitions.VariableDictionary[DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column].Text].ItemId;// Convert.ToInt32(newMtoSQuestion.ItemId);
                            ReadDataFromOutputFile(DataAfterProcess, mtosfileoutput, Definitions.VariableDictionary[NewQstnVariable].ItemId, dt/*, tablearray*/, 0, false);//,0,false, delQuerystring added by 191
                            CriteriaQuerystring = string.Empty;
                            break;
                        case Constants.DP.SubstituteOperatorINTEGRATE:

                            string maxDoubleValue = "179769313486231570000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
                            string minDoubleValue = "-179769313486231570000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";

                            dpsheet.ListUp_Dict.Clear();//clear if not listup
                            dpOperator = dp.Add(DataProcessCode.Integrate);
                            dpOperator.RunFlag = true;
                            newQuestion = dpOperator.Questions.Add();
                            newQuestion.ItemId = Definitions.VariableDictionary[NewQstnVariable].ItemId.ToString();//"1463";
                            newQuestion.SourceItemId = Convert.ToString(Definitions.VariableDictionary[DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam3Column].Text].ItemId);// newQuestion.ItemId;//191code 27-12-19
                            newQuestion.Name = NewQstnVariable;
                            newQuestion.QuestionType = Definitions.VariableDictionary[NewQstnVariable].AnswerType == Constants.AnswerType.SA ? QuestionType.SA : QuestionType.MA;// QuestionType.SA;
                            ((INewQuestion)newQuestion).ChangeExtension = GlobalsCommonConstant.fileExtension.dp;
                            sectors = newQuestion.Sectors;
                            //  virtualsectors.jointCategoryCount = (Definitions.VariableDictionary[paramvariable.Text]).CategoryCount;
                            newQuestion.CategoryCount = (Definitions.VariableDictionary[NewQstnVariable]).CategoryCount;
                            Excel.Range param1cell = DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column];

                            if (backgroundWorker.CancellationPending) { return; }
                            _INewQuestionSector sectorUnmatched;
                            List<string> querystring = new List<string>();
                            List<string> variableList = new List<string>();
                            List<string> variableIdList = new List<string>();
                            List<string> operatorList = new List<string>();
                            Excel.Range param2cell = DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam2Column];
                            string template = string.Empty;
                            int variablecount = Convert.ToInt32(param2cell.Text);
                            int indexpos = -1;
                            bool isand = false;
                            string lastVar = "";
                            for (int x = 0; x < variablecount; x++)
                            {
                                Excel.Range variablecell = DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam3Column + (x * 2)];
                                if (x < variablecount - 1)
                                {
                                    Excel.Range operatorcell = DPSheet.Cells[kvp.Key, (Constants.DP.SubstituteParam3Column + (x * 2) + 1)];
                                    operatorList.Add(operatorcell.Text == "AND" ? " & " : " | ");//191 AND OR result  Not equal -171801   //operatorList.Add(operatorcell.Text == "AND" ? "&" : "|");

                                }
                                //int itemId = Definitions.VariableDictionary[variablecell.Text].ItemId;
                                variableIdList.Add(Definitions.VariableDictionary[variablecell.Text].ItemId.ToString());
                                variableList.Add(variablecell.Text);
                                dpvariableparams.Add(variablecell.Text);
                                lastVar = variablecell.Text;
                                //template += "{" + x.ToString() + "}";
                            }
                            int categoryCOunt = Definitions.VariableDictionary[NewQstnVariable].CategoryCount;
                            List<string> qstringList = new List<string>();
                            for (int y = 0; y < categoryCOunt; y++)
                            {
                                int paramNcolumn = Constants.DP.SubstituteParam3Column + ((variablecount * 2) - 1) + y;
                                Excel.Range paramcell = DPSheet.Cells[kvp.Key, paramNcolumn];
                                string paramtext = paramcell.Text;
                                if (!string.IsNullOrEmpty(paramtext))
                                {
                                    paramtext = paramtext.Replace(" ", string.Empty);
                                }
                                string[] values = paramtext.Split(';');// paramcell.Text.Split(';');
                                string qstring = string.Empty;

                                if (values.Any(s => !string.IsNullOrEmpty(s)))
                                    for (int x = 0; x < variableIdList.Count; x++)
                                    {
                                        if (string.IsNullOrEmpty(values[x]))
                                            values[x] = "¶";

                                        if (!string.IsNullOrEmpty(qstring))
                                        {
                                            if (operatorList[x - 1] == " | ")
                                            {
                                                if (isand)
                                                {
                                                    qstring += ")" + operatorList[x - 1] + "(";
                                                }
                                                else
                                                {
                                                    qstring = qstring.Remove(indexpos, 1);
                                                    qstring += operatorList[x - 1] + "(";
                                                }
                                                isand = false;
                                            }
                                            else
                                            {
                                                qstring += operatorList[x - 1];
                                                isand = true;
                                            }
                                            indexpos = qstring.Length - 1;

                                        }
                                        if (string.IsNullOrEmpty(qstring))
                                        {
                                            qstring = "(";
                                            indexpos = 0;
                                        }
                                        qstring += variableIdList[x];
                                        if (values[x].StartsWith("!") || values[x].StartsWith("<>"))
                                        {
                                            qstring += "!";
                                            values[x] = values[x].Replace("!", "");
                                            values[x] = values[x].Replace("<>", "");
                                        }
                                        if (values[x].StartsWith("-") && values[x].Length > 1)
                                        {
                                            if (Definitions.VariableDictionary[variableList[x]].AnswerType == Constants.AnswerType.N)
                                            {
                                                values[x] = "(" + minDoubleValue + ")" + values[x];// double.MinValue.ToString("r")
                                            }
                                            else
                                            {
                                                values[x] = "1" + values[x];
                                            }

                                        }
                                        else if (values[x].EndsWith("-") && values[x].Length > 1)
                                        {
                                            if (Definitions.VariableDictionary[variableList[x]].AnswerType == Constants.AnswerType.N)
                                            {
                                                values[x] = values[x] + maxDoubleValue;// double.MaxValue;
                                            }
                                            else
                                            {
                                                values[x] = values[x] + Definitions.VariableDictionary[variableList[x]].CategoryCount;
                                            }

                                        }
                                        qstring += "=";

                                        values[x] = values[x].Replace("(", "");
                                        values[x] = values[x].Replace(")", "");

                                        qstring += values[x];

                                    }

                                if (!string.IsNullOrEmpty(qstring))
                                {
                                    // qstring = qstring.Replace("(", "");
                                    // qstring = qstring.Replace(")", "");

                                    //int startpara = qstring.Count(f => f == '(');
                                    //int endpara = qstring.Count(f => f == ')');
                                    //int bal = startpara - endpara;
                                    //if (bal != 0) qstring += ")";

                                    if (isand)
                                    {
                                        qstring += ")";
                                    }
                                    else
                                    {
                                        qstring = qstring.Remove(indexpos, 1);
                                    }
                                    
                                    qstring = RemoveSpecialCharAndOrderQueryString(qstring);
                                    qstringList.Add(qstring);
                                    isand = false;
                                }
                                //Unmatched conditions for null conditions. This fix as per Redmine Id:238543
                                else
                                {
                                    QuestionSettings qst = Definitions.VariableDictionary[lastVar];
                                    if (qst.AnswerType == Constants.AnswerType.SA || qst.AnswerType == Constants.AnswerType.MA)
                                    {
                                        qstring = qst.ItemId + "!= & " + qst.ItemId + "!=* & " + qst.ItemId + "!=" + 1 + "-" + qst.CategoryCount;
                                    }
                                    else
                                    {
                                        qstring = qst.ItemId + "!= & " + qst.ItemId + "!=* & " + qst.ItemId + "!=" + Int64.MinValue + "-" + Int64.MaxValue;
                                    }
                                    qstringList.Add(qstring);
                                }
                                qstring = string.Empty;

                            }
                            foreach (string item in qstringList)
                            {
                                sectorUnmatched = sectors.Add(item);

                            }
                            if (string.Equals(param1cell.Text, "1"))//somtimes this has to add after criteria
                            {
                                (newQuestion as NewQuestions.NewQuestion).UnfitFlag = true;
                                string qstr = string.Empty;
                                for (int x = 0; x < variableIdList.Count; x++)
                                {
                                    if (x > 0)
                                    {
                                        qstr += "&";

                                    }
                                    qstr += variableIdList[x];
                                    qstr += "=";
                                    qstr += "*";

                                }
                                sectorUnmatched = sectors.Add(qstr);
                                sectorUnmatched.DataType = DataType.IVData;
                            }

                            if (backgroundWorker.CancellationPending) { return; }
                            // 191  adddition criteria
                            if (!string.IsNullOrEmpty(CriteriaVariableText))
                            {
                                FetchCriteria(CriteriaVariableText, criteriaOperatorText, criteriaValueText, kvp, newQuestion, isnotcriteriavalue, isivcriteria, isnacriteria);
                            }

                            if (backgroundWorker.CancellationPending) { return; }
                            //Perormance//dp.Questions = QC4Common.Util.DictionaryUtil.GetQuestions(DataProcess.currworkbook);
                            dp.Execute();
                            if (backgroundWorker.CancellationPending) { return; }
                            string intgrateid = newQuestion.ItemId;// (Definitions.VariableDictionary[DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column].Text].ItemId).ToString();
                            string integrate = dirPath + "\\" + intgrateid + ".dp";
                            ReadDataFromOutputFile(DataAfterProcess, integrate, Definitions.VariableDictionary[NewQstnVariable].ItemId, dt/*, tablearray*/);
                            //querystring = 
                            // newQuestion.SourceItemId = newQuestion.ItemId;
                            CriteriaQuerystring = string.Empty;
                            break;
                        case Constants.DP.SubstituteOperatorCOMPUTE:
                            try
                            {
                                //_log.Warn("Start COMPUTE OPERATION");
                                dpsheet.ListUp_Dict.Clear();//clear if not listup
                                int computeItemId = Definitions.VariableDictionary[DPSheet.Cells[kvp.Key, Constants.DP.SubstituteVariableColumn].Text].ItemId;
                                bool isMA = false;
                                dpOperator = dp.Add(DataProcessCode.Compute);
                                dpOperator.RunFlag = true;
                                newQuestion = dpOperator.Questions.Add();
                                newQuestion.ItemId = Definitions.VariableDictionary[NewQstnVariable].ItemId.ToString();//"1463";
                                newQuestion.Name = NewQstnVariable;// Sheet.Cells[kvp.Key, Constants.DP.SubstituteVariableColumn].Text;//"NGID";

                                newQuestion.QuestionType = Macromill.QCWeb.Tabulation.QuestionType.SA; // Definitions.VariableDictionary[NewQstnVariable].AnswerType == Constants.AnswerType.FA ? Macromill.QCWeb.Tabulation.QuestionType.FA : Macromill.QCWeb.Tabulation.QuestionType.SA;// Macromill.QCWeb.Tabulation.QuestionType.SA;

                                ((INewQuestion)newQuestion).ChangeExtension = GlobalsCommonConstant.fileExtension.dp;
                                newQuestion.SourceItemId = computeItemId.ToString();
                                string computeinputfilepath = Path.Combine(GetProcessIdPath()+ "\\", newQuestion.ItemId + ".dp");//".dp.tmp"
                                                                                                                                     //if (Definitions.VariableDictionary[NewQstnVariable].AnswerType == Constants.AnswerType.FA)
                                string rawformula = string.Empty;                                                                                           //_log.Warn("COMPUTE OPERATION-----Setting DP Params"); //    computeinputfilepath = Path.Combine(Path.GetTempPath(), "QC4\\" + newQuestion.ItemId + ".dp.tmp");
                                List<string> itemNamesList = new List<string>();
                                List<string> tmpNamesList = new List<string>();
                                Dictionary<string, decimal> itemNameIdList = new Dictionary<string, decimal>();
                                Dictionary<string, string> itemValuNameList = new Dictionary<string, string>();//Dictionary<string, double> itemValuNameList = new Dictionary<string, double>();
                                Dictionary<string, string> stringitemValuNameList = new Dictionary<string, string>();
                                string formula = "";
                                string formula_ = "";
                                string formula__ = "";
                                //Regex regex = new Regex(@"\[([^:\[\]@\|='&\\\!\?<>\*/\r\n]+)\]");
                                var regex = new Regex(@"(^-?\d+\.\d+$)|(^\d+\.\d+$)|(^\d+$)|(^-?\d+$)");
                                //Perormance//dp.Questions = QC4Common.Util.DictionaryUtil.GetQuestions(DataProcess.currworkbook);// dp.Questions = QC4Common.Util.DictionaryUtil.GetQuestions(DPSheet.Application.ActiveWorkbook);
                                //_log.Warn("COMPUTE OPERATION-----Setting ItemList From Formula");
                                ((INewQuestion)newQuestion).FormulaString = Definitions.VariableDictionary.ContainsKey(DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column].Text) ?
                                                                            string.Empty : DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column].Text;
                                foreach (Match match in itemNameRegex.Matches(((INewQuestion)newQuestion).FormulaString))
                                {
                                    string tmpName = match.Groups[1].Value;
                                    if (tmpNamesList.Contains(tmpName)) continue;
                                    tmpNamesList.Add(tmpName);
                                    if (itemNamesList != null) itemNamesList.Add(tmpName);
                                }
                                for (int j = 0; j < itemNamesList.Count; j++)
                                {
                                    if (!itemNameIdList.ContainsKey(itemNamesList[j]))
                                    {
                                        int paramid = Definitions.VariableDictionary[itemNamesList[j]].ItemId;
                                        itemNameIdList.Add(itemNamesList[j], paramid);
                                    }

                                }

                                // 191  adddition criteria
                                bool iscriteria = false;
                                bool[] filterringFlag = new bool[samplescount];
                                //for (int fi=0;fi< dp.SamplesCount;fi++)
                                //{
                                //    filterringFlag[fi]=false;
                                //}
                                //_log.Warn("COMPUTE OPERATION-----Start Fetching Criteria");
                                if (!string.IsNullOrEmpty(CriteriaVariableText))
                                {
                                    FetchCriteria(CriteriaVariableText, criteriaOperatorText, criteriaValueText, kvp, null, isnotcriteriavalue, isivcriteria, isnacriteria, false);
                                    iscriteria = true;
                                }

                                if (iscriteria)//if criteria true getting true fals 
                                {

                                    filterringFlag = new Criteria(CriteriaQuerystring, "", dp.Questions).Filtering(DBHelper.GetConnectionString(DataProcess.currworkbook), dt: dt);//(DPSheet.Application.ActiveWorkbook

                                }
                                bool isnaiv = false;
                                StreamWriter computwriter = new StreamWriter(computeinputfilepath);
                                //_log.Warn("COMPUTE OPERATION-----Iterating Samples");
                                for (int k = 0; k < dp.SamplesCount; k++)
                                {
                                    //get from variableparam as FormulaString;  split according to operator,get variable then
                                    //get variable id || Constant Value
                                    //Get operator
                                    //loop it
                                    //save to file
                                    isnaiv = false;

                                    if (Definitions.VariableDictionary[NewQstnVariable].AnswerType == Constants.AnswerType.N)
                                    {
                                        for (int j = 0; j < itemNamesList.Count; ++j)
                                        {
                                            int id = Convert.ToInt32(itemNameIdList[itemNamesList[j]]);
                                            string variableid = id == 0 ? QC4Common.Common.Constants.VariableSampleId : QC4Common.Common.Constants.VariableSuffix + id;

                                            if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[k][variableid])) && !dt.Rows[k][variableid].Equals("*") && !string.IsNullOrEmpty(Convert.ToString(dt.Rows[k][variableid])))
                                            {

                                                if (Definitions.VariableDictionary[NewQstnVariable].AnswerType.Equals(Constants.AnswerType.N) && ((Definitions.VariableDictionary[itemNamesList[j]].AnswerType.Equals(Constants.AnswerType.FA) || Definitions.VariableDictionary[itemNamesList[j]].AnswerType.Equals(Constants.AnswerType.SA) || Definitions.VariableDictionary[itemNamesList[j]].AnswerType.Equals(Constants.AnswerType.N)))
                                                    ||
                                                    (Definitions.VariableDictionary[NewQstnVariable].AnswerType.Equals(Constants.AnswerType.FA) && (Definitions.VariableDictionary[itemNamesList[j]].AnswerType.Equals(Constants.AnswerType.FA) || Definitions.VariableDictionary[itemNamesList[j]].AnswerType.Equals(Constants.AnswerType.SA) || Definitions.VariableDictionary[itemNamesList[j]].AnswerType.Equals(Constants.AnswerType.N) || Definitions.VariableDictionary[itemNamesList[j]].AnswerType.Equals(Constants.AnswerType.MA)))
                                                    )
                                                {

                                                    double val = string.IsNullOrEmpty(dt.Rows[k][variableid].ToString()) ? 0 : double.Parse(dt.Rows[k][variableid].ToString());//
                                                    itemValuNameList.Add(itemNamesList[j], (id == 0 ? Convert.ToString(dt.Rows[k]["sample_id"]) : Convert.ToString(dt.Rows[k][variableid])));//[Phase 2] [ Redmine id : 180517]       //itemValuNameList.Add(itemNamesList[j], val.ToString());          //itemValuNameList.Add(itemNamesList[j], val); //DataAfterProcess.Cells[4 + k, paramid + 1].Text;
                                                }
                                                else
                                                {
                                                    formula = string.Empty;
                                                }
                                            }
                                            else
                                            {
                                                isnaiv = true;
                                                itemValuNameList.Add(itemNamesList[j], null);
                                            }
                                        }
                                        if (isnaiv == true)
                                        {
                                             formula = string.Empty;                                          
                                        }
                                        else
                                        {
                                            formula = GlobalMethodClass.BuildExpressionFA(((INewQuestion)newQuestion).FormulaString, itemNamesList, itemValuNameList);
                                            // formula_ = GlobalMethodClass.BuildExpressionFA_(((INewQuestion)newQuestion).FormulaString, itemNamesList, itemValuNameList);
                                            // formula = GlobalMethodClass.BuildExpression(((INewQuestion)newQuestion).FormulaString, itemNamesList, itemValuNameList);
                                        }

                                        //formula = GlobalMethodClass.BuildExpression(((INewQuestion)newQuestion).FormulaString, itemNamesList, itemValuNameList);

                                    }
                                    else
                                    {

                                        for (int j = 0; j < itemNamesList.Count; ++j)
                                        {
                                            int id = Convert.ToInt32(itemNameIdList[itemNamesList[j]]);
                                            string variableid = id == 0 ? QC4Common.Common.Constants.VariableSampleId : QC4Common.Common.Constants.VariableSuffix + id;
                                            if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[k][variableid])) && !dt.Rows[k][variableid].Equals("*") && !string.IsNullOrEmpty(Convert.ToString(dt.Rows[k][variableid])))// dt.Rows[k][id + 1] != ""
                                            {
                                                isMA = Definitions.VariableDictionary[itemNamesList[j]].AnswerType == Constants.AnswerType.MA ? true : false;
                                                // string val = Definitions.VariableDictionary[itemNamesList[j]].AnswerType == Constants.AnswerType.MA ? "#QC#INV#"+ParseMconvertdata(Convert.ToString(dt.Rows[k][id + 1]).ToCharArray())+"#QC#INV#": Convert.ToString(dt.Rows[k][id + 1]);//check Ma or not
                                                // string val = Definitions.VariableDictionary[itemNamesList[j]].AnswerType == Constants.AnswerType.MA ? '"' + ParseMconvertdata(Convert.ToString(dt.Rows[k][id + 1]).ToCharArray()) + '"' : Convert.ToString(dt.Rows[k][id + 1]);//check Ma or not
                                                // string val = Definitions.VariableDictionary[itemNamesList[j]].AnswerType == Constants.AnswerType.MA ? '"' + ParseMconvertdata(Convert.ToString(dt.Rows[k][id + 1]).ToCharArray()) + '"' : Convert.ToString(dt.Rows[k][id + 1]);//check Ma or not //.Replace(",", "\\")
                                                string val = Definitions.VariableDictionary[itemNamesList[j]].AnswerType == Constants.AnswerType.MA ? ParseMconvertdata(Convert.ToString(dt.Rows[k][variableid]).ToCharArray()) : (id == 0 ? Convert.ToString(dt.Rows[k]["sample_id"]) : Convert.ToString(dt.Rows[k][variableid]));//[Phase 2] [ Redmine id : 180517] //check Ma or not //.Replace(",", "\\")
                                                                                                                                                                                                                                                                                                                                   // string val = Definitions.VariableDictionary[itemNamesList[j]].AnswerType == Constants.AnswerType.MA ? ParseMconvertdata(Convert.ToString(dt.Rows[k][id + 1]).ToCharArray()) : Convert.ToString(dt.Rows[k][id + 1]);//check Ma or not //.Replace(",", "\\")

                                                stringitemValuNameList.Add(itemNamesList[j], System.Text.RegularExpressions.Regex.Escape(val)); //DataAfterProcess.Cells[4 + k, paramid + 1].Text;

                                            }
                                            else
                                            {
                                                isnaiv = true;                                               
                                                itemValuNameList.Add(itemNamesList[j], null);//itemValuNameList.Add(itemNamesList[j], 0);
                                            }
                                        }
                                        if (isnaiv == true)
                                        {
                                             formula = string.Empty;                                           
                                        }
                                        else
                                        {
                                            // formula = GlobalMethodClass.BuildExpressionFA(((INewQuestion)newQuestion).FormulaString, itemNamesList, stringitemValuNameList);
                                            // _log.Warn("COMPUTE OPERATION-----BuildExpressionFA__");
                                            //  rawformula = ((INewQuestion)newQuestion).FormulaString;
                                            formula = GlobalMethodClass.BuildExpressionFA__(((INewQuestion)newQuestion).FormulaString, itemNamesList, stringitemValuNameList);
                                            // _log.Warn("COMPUTE OPERATION-----BuildExpressionFA__ END");
                                        }
                                    }
                                    /////////////////
                                    int dtsortno = Convert.ToInt32(dt.Rows[k]["sort_no"].ToString());
                                    if (backgroundWorker.CancellationPending) { return; }
                                    // (iscriteria && Questions[i].Sectors.Count > 1 && Questions[i].Sectors[Sectorscount].Criteria != null && !(Questions[i].Sectors[Sectorscount].Criteria.IsTrue(r)))//codeby 191  this if not in original 
                                    //[Redmine id : 176491] -
                                    if (iscriteria && !filterringFlag[k])// if (iscriteria && !filterringFlag[dtsortno - 1])//k + dp.SortNumber//if criteria true and success
                                    {
                                        //write unsatified value
                                        if (computeItemId == 0)//sampleid or not
                                            formula = Convert.ToString(dt.Rows[k]["sample_id"]);
                                        else
                                            formula = Convert.ToString(dt.Rows[k][QC4Common.Common.Constants.VariableSuffix + computeItemId]);// evalres = Convert.ToDouble(dt.Rows[k][computeItemId + 1]);
                                    }
                                    else
                                    {
                                        if (Definitions.VariableDictionary[NewQstnVariable].AnswerType == Constants.AnswerType.N)
                                        {
                                            if (isnaiv == true)
                                            {
                                                formula = string.Empty;
                                            }
                                            else
                                            {                                                
                                                //Redmine id : 175135
                                                // var evalres = double.Parse(0.ToString());
                                                try
                                                {
                                                    var evalres = DPSheet.Application.Evaluate(formula);
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
                                                            ////MessageDialog.ErrorOk("The content is invalid"); 
                                                            formula = string.Empty;//"0"
                                                            break;
                                                        default:
                                                            //Redmine id : 195209
                                                            double formula_value = 0;
                                                            if (double.TryParse(evalres.ToString(), out formula_value))
                                                            {
                                                                formula = evalres.ToString();
                                                            }
                                                            else
                                                            {
                                                                formula = string.Empty;
                                                            }
                                                            //Redmine id : 175135
                                                            //if (regex.Match(Convert.ToString(evalres)).Success)
                                                            //{
                                                            //    formula = evalres.ToString();
                                                            //}
                                                            //else { formula = string.Empty; }
                                                            break;
                                                    }
                                                }
                                                catch (Exception ex) { formula = string.Empty; }
                                            }
                                        }
                                        else
                                        {
                                            //_log.Warn("COMPUTE OPERATION-----Application.Evaluate  Start");
                                            string s = string.Empty;
                                            if (!isnaiv)
                                            {
                                                //Redmine id: 175313
                                                try
                                                {
                                                    if (formula.Length <= QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit)
                                                    {
                                                        var evalres = DPSheet.Application.Evaluate(System.Text.RegularExpressions.Regex.Unescape(formula));//[Redmine id : 175592] -
                                                        switch (evalres)
                                                        {
                                                            case Constants.DP.ErrDiv0:
                                                            case Constants.DP.ErrGettingData:
                                                            case Constants.DP.ErrName:
                                                            case Constants.DP.ErrNA:
                                                            case Constants.DP.ErrNull:
                                                            case Constants.DP.ErrNum:
                                                            case Constants.DP.ErrRef:
                                                                s = string.Empty;
                                                                break;
                                                            case Constants.DP.ErrValue:
                                                                //length greater than 255
                                                                s = EVAL_FA_Value(((INewQuestion)newQuestion).FormulaString, itemNamesList, stringitemValuNameList);
                                                                break;
                                                            default:
                                                                //s = Convert.ToString(DPSheet.Application.Evaluate(System.Text.RegularExpressions.Regex.Unescape(formula)));
                                                                s = evalres.ToString();
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        s = EVAL_FA_Value(((INewQuestion)newQuestion).FormulaString, itemNamesList, stringitemValuNameList);
                                                    }
                                                }
                                                catch (Exception ex) { s = formula_; }
                                            }
                                            formula = s;
                                            //_log.Warn("COMPUTE OPERATION-----Application.Evaluate  END");
                                        }
                                    }
                                    /////////////////
                                    ///

                                    if (backgroundWorker.CancellationPending) { return; }
                                    if (Definitions.VariableDictionary[NewQstnVariable].AnswerType == Constants.AnswerType.FA)//Redmine id: 210184
                                    {

                                        formula = frmutil.EscapeCRLF(formula);
                                    }

                                    computwriter.WriteLine(formula); itemValuNameList.Clear(); stringitemValuNameList.Clear();
                                }
                                computwriter.Close();
                                //if (Definitions.VariableDictionary[NewQstnVariable].AnswerType == Constants.AnswerType.FA)
                                //    dp.Execute();
                                //_log.Warn("ENded COMPUTE OPERATION Process");
                                string processedcomputefile = dirPath + "\\" + newQuestion.ItemId + ".dp";
                                int computeitemid = Convert.ToInt32(newQuestion.ItemId);
                                try
                                {
                                    // _log.Warn("Start Update DT COMPUTE OPERATION Process");
                                    ReadDataFromOutputFile(DataAfterProcess, processedcomputefile, computeitemid, dt/*, tablearray*/, 0, false, null, false, Definitions.VariableDictionary[NewQstnVariable].AnswerType == Constants.AnswerType.FA);
                                    // _log.Warn("END Update DT COMPUTE OPERATION Process");
                                }
                                catch (Exception ex) { }
                                CriteriaQuerystring = string.Empty;
                            }
                            catch (Exception ex) { int i = 0; }
                            break;
                        case Constants.DP.SubstituteOperatorJOINT:
                            dpsheet.ListUp_Dict.Clear();//clear if not listup 

                            dpOperator = dp.Add(DataProcessCode.ModifyData);//need to check and somtimes make new inherited method  
                            dpOperator.RunFlag = true;//need to che neede or not for modify
                            newQuestion = dpOperator.Questions.Add();
                            newQuestion.ItemId = Definitions.VariableDictionary[NewQstnVariable].ItemId.ToString();
                            newQuestion.Name = NewQstnVariable;
                            newQuestion.QuestionType = Macromill.QCWeb.Tabulation.QuestionType.MA;

                            ((INewQuestion)newQuestion).ChangeExtension = GlobalsCommonConstant.fileExtension.dp;

                            Excel.Range jointparam1 = DPSheet.Cells[kvp.Key, Constants.DP.SubstituteParam1Column];
                            Excel.Range endcelljoint = ExcelUtilForAddIn.EndxlRight(jointparam1);
                            Excel.Range jointparamrange = DPSheet.Range[jointparam1, endcelljoint];

                            object[,] jointparamsarray = jointparamrange.Value;
                            sectors = newQuestion.Sectors;
                            for (int j = 1; j <= jointparamsarray.GetLength(1); j += 2)
                            {
                                string variable = string.IsNullOrEmpty(Convert.ToString(jointparamsarray[1, j])) ? string.Empty : Convert.ToString(jointparamsarray[1, j]);
                                if (!string.IsNullOrEmpty(variable))
                                {
                                    INewQuestionSectors jointsectors = newQuestion.Sectors;

                                    newQuestion.SourceItemId = Definitions.VariableDictionary[variable].ItemId.ToString();
                                    var virtualjoinSector = jointsectors.Add(newQuestion.SourceItemId + "=" + GetCommaSeperated(Convert.ToString(jointparamsarray[1, j + 1]), variable), true) as INewVirtualQuestionSector;//jointsectors.Add(newQuestion.SourceItemId + "=" + jointparamsarray[1, j + 1], true) as INewVirtualQuestionSector;
                                    virtualjoinSector.EditMethod = EditMethod.APPEND;// EditMethod.JOIN;// EditMethod.APPEND;
                                    virtualjoinSector.ModifyDataEdit = ModifyDataEdit.JOIN;// ModifyDataEdit.ITEM;
                                    virtualjoinSector.Alias = newQuestion.SourceItemId;
                                    virtualjoinSector.jointCategoryCount = (Definitions.VariableDictionary[variable]).CategoryCount;
                                    //newQuestion.SourceItemId = Definitions.VariableDictionary[variable].ItemId.ToString();
                                    //sectors.Add(newQuestion.SourceItemId + "=" + jointparamsarray[ 1, j + 1]);
                                }
                            }
                            //Perormance//dp.Questions = QC4Common.Util.DictionaryUtil.GetQuestions(DataProcess.currworkbook);// dp.Questions = QC4Common.Util.DictionaryUtil.GetQuestions(DPSheet.Application.ActiveWorkbook);
                            // 191  adddition criteria
                            if (!string.IsNullOrEmpty(CriteriaVariableText))
                            {
                                FetchCriteria(CriteriaVariableText, criteriaOperatorText, criteriaValueText, kvp, newQuestion, isnotcriteriavalue, isivcriteria, isnacriteria);
                            }
                            if (backgroundWorker.CancellationPending) { return; }

                            dp.Execute();
                            if (backgroundWorker.CancellationPending) { return; }
                            string joinid = newQuestion.ItemId;
                            string join = dirPath + "\\" + joinid + ".dp";
                            //string txtJoinPath = Path.Combine(Path.GetTempPath(), @"QC4\" + newQuestion.ItemId + ".txt");


                            //if (!File.Exists(join) && File.Exists(txtJoinPath))
                            //{
                            //    join = txtJoinPath;
                            //}
                            if (File.Exists(join))
                            {
                                ReadDataFromOutputFile(DataAfterProcess, join, Definitions.VariableDictionary[NewQstnVariable].ItemId, dt/*, tablearray*/);
                            }
                            CriteriaQuerystring = string.Empty;
                            break;
                    }
                    if (DPSheet.Cells[kvp.Key, Constants.DP.CheckCrossColumn].Text == CommonResource.CELL_ON)
                    {
                        if (dpvariableparams.Count > 0)
                        {
                            int subRow = forNextIterator > CALLstatementRow ? forNextIterator : CALLstatementRow;
                            string filePath = WriteCheckCrossVariableFile(kvp.Key, NewQstnVariable, dpvariableparams, dt, subRow);
                            AddDPVariableInfo(kvp.Key, kvp.Value.substituteoperator, NewQstnVariable, dpvariableparams, filePath, subRow);
                        }
                    }
                    // delQuerystring = string.Empty;
                }
            }
        }
        /// <summary>
        /// Create a query string by removing special character
        /// </summary>
        /// <param name="qstring">Query string</param>
        /// <returns>Formatted query string</returns>
        private string RemoveSpecialCharAndOrderQueryString(string qstring)
        {
            string inp = qstring;
            string oup;

            oup = Regex.Replace(inp, @"[0-9]+=\¶\s&\s", "");
            oup = Regex.Replace(oup, @"[0-9]+=\¶\s\|\s", "");
            oup = Regex.Replace(oup, @"\s&\s[0-9]+=\¶", "");
            oup = Regex.Replace(oup, @"\s\|\s[0-9]+=\¶", "");
            oup = Regex.Replace(oup, @"[0-9]+=\¶", "");

            oup = oup.Replace("¶", "");
            oup = oup.Replace("() | ", "");
            oup = oup.Replace("() & ", "");
            oup = oup.Replace("() |", "");
            oup = oup.Replace("() &", "");
            oup = oup.Replace(" | ()", "");
            oup = oup.Replace(" & ()", "");
            oup = oup.Replace("| ()", "");
            oup = oup.Replace("& ()", "");
            oup = oup.Replace("()", "");

            string[] test = oup.Split('(', ')');
            foreach (string t in test)
            {
                if (!(t.Contains("*") || t.Contains("/") || t.Contains("-") || t.Contains("+") || t.Contains("&") || t.Contains("|")))
                {
                    oup = oup.Replace("(" + t + ")", t);
                }
            }

            if (oup.Contains("("))
            {
                string[] splt = oup.Split('|');
                oup = string.Empty;
                for (int i=0;i<splt.Length;i++)
                {
                    if (splt[i].Contains('(') && !splt[i].Contains('&'))
                        oup += splt[i].Replace("(", "").Replace(")", "") + "|";
                    else
                        oup += splt[i] + "|";
                }
                oup = oup.Remove(oup.Length - 1, 1);
            }

            return oup;
        }

        private String GetMAValueCommaSeperatedOfFA(string value, string quesvar)
        {
            string[] paramlist = value.Split(',', '/');
            string joinparams = string.Empty;
            foreach (string str in paramlist)
            {

                if (str.Contains('-'))
                {
                    int minimum = 0;
                    int maximum = 0;
                    string[] range = str.Split('-');
                    if (range.Length > 1)
                    {
                        minimum = 0;
                        maximum = Definitions.VariableDictionary[quesvar].CategoryCount;
                        if (!string.IsNullOrEmpty(range[0]))
                        {
                            int.TryParse(range[0], out minimum);
                        }
                        if (!string.IsNullOrEmpty(range[1]))
                        {
                            int.TryParse(range[1], out maximum);
                        }
                    }
                    for (int i = minimum; i <= maximum; i++)
                    {
                        if (!string.IsNullOrEmpty(joinparams))
                        {
                            joinparams += ",";
                        }
                        joinparams += i.ToString();

                    }

                }
                if (!str.Contains('-'))
                {
                    if (!string.IsNullOrEmpty(joinparams))
                    {
                        joinparams += ",";
                    }

                    joinparams += str;
                }
            }
            return joinparams;
        }
        private String GetCommaSeperated(string value, string quesvar)
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
            List<double> exclidelist = new List<double>();
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
                var variabledictkeys = Definitions.VariableDictionary.Keys.ToArray();//266932-Fix for incorrect processing result when hyphen exist in variable
                var criteriaval = str.Split('@')[0].Replace("[", "");
                bool isValueInArray = Array.Exists(variabledictkeys, element => element.Contains(criteriaval));
                if (isnot)//str.StartsWith("!") || str.StartsWith("<>")
                {
                    string notvalue = str;
                    //need to remove the items from list and add other category numbers
                    // criteriaValueDescription = criteriaValueDescription.TrimStart('!');
                    if (str.StartsWith("!")) notvalue = str.TrimStart('!');
                    else if (str.StartsWith("<>")) notvalue = str.Replace("<>", "");
                    //criteriaValueDescription = criteriaValueDescription.Replace("<>", "");//TrimStart('<>');

                    int criteriaend = Definitions.VariableDictionary[quesvar].CategoryCount;
                    if (str.Contains('-') && !isValueInArray) //Modified as part of 266932
                    {
                        double strt = 0, end = 0;
                        string[] criterisplitvals = notvalue.Split('-');

                        if (criterisplitvals.Length == 1)
                        {
                            try
                            {
                                strt = Convert.ToDouble(criterisplitvals[0]);
                            }
                            catch (Exception e) { strt = 1; System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace); }
                            end = strt;

                        }
                        else
                        {
                            try
                            {
                                strt = Convert.ToDouble(criterisplitvals[0]);
                            }
                            catch (Exception e) { strt = 1; System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace); }
                            try
                            {
                                end = Convert.ToDouble(criterisplitvals[1]);
                            }
                            catch (Exception e)
                            {
                                end = Definitions.VariableDictionary[quesvar].CategoryCount;
                                System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                            }
                        }

                        for (double ci = strt; ci <= end; ci++)
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
                            exclidelist.Add(Convert.ToDouble(str));
                        }
                        catch { }
                    }


                }
                else
                {
                    //else
                    if (str.Contains('-') && !isValueInArray)//Modified as part of 266932
                    {

                        double start = 0, limit = 0;
                        string[] criteriaminvalues = str.Split('-');
                        // foreach (string s in criteriaminvalues)
                        {

                            try
                            {

                                if (criteriaminvalues.Length == 1)
                                {
                                    try
                                    {
                                        start = Convert.ToDouble(criteriaminvalues[0]);
                                    }
                                    catch (Exception e) { start = 1; System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace); }
                                    limit = start;
                                }
                                else
                                {
                                    try
                                    {
                                        start = Convert.ToDouble(criteriaminvalues[0]);
                                    }
                                    catch (Exception e) { start = 1; System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace); }//actually get min value of answer
                                    try
                                    {
                                        limit = Convert.ToDouble(criteriaminvalues[1]);
                                    }
                                    catch (Exception e)
                                    {//actually get max value of answer;need to get max of choice no from item id and set limit
                                        limit = Definitions.VariableDictionary[quesvar].CategoryCount;
                                        System.Diagnostics.Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                                    }
                                }
                                if (limit < start)//need to reverse if 9-7 comes
                                {
                                    double temp = limit;
                                    limit = start;
                                    start = temp;
                                }
                            }
                            catch { }

                            for (double ci = start; ci <= limit; ci++)
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
                for (int ci = 1; ci <= Definitions.VariableDictionary[quesvar].CategoryCount; ci++)
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
        private string GetNextValueForSA(string value, int catcount)
        {
            string savalue = value;
            if (savalue.StartsWith("!"))
            {
                savalue = savalue.Replace("!", string.Empty);
                int paramnum = 0;
                if (!int.TryParse(savalue, out paramnum))
                {
                    savalue = string.Empty;
                }
                if (paramnum == 1)
                {
                    savalue = paramnum <= catcount ? (paramnum + 1).ToString() : string.Empty;
                }
                else savalue = 1.ToString();
            }

            return savalue;
        }
        private Datatabledataposi listupupdation(DataTable dtColumn)//siv
        {
            if (firstlistitration)
            {
                if (Positionlist.Count == 0)
                {
                    var liststructobj = new Datatabledataposi();
                    liststructobj.Start_posi = 3;
                    liststructobj.End_posi = (liststructobj.Start_posi + dtColumn.Rows.Count) - 1;
                    liststructobj.Dtdata_StartPosi = liststructobj.Start_posi;
                    liststructobj.Dtdata_EndPosi = liststructobj.End_posi;
                    liststructobj.datacount = dtColumn.Rows.Count;
                    endposition = liststructobj.End_posi;
                    Positionlist.Add(liststructobj);
                }
                else
                {
                    try
                    {
                        var liststructobj = new Datatabledataposi();
                        //var tempposi = Positionlist.ElementAt(listnumber - 1);
                        liststructobj.Start_posi = Positionlist.ElementAt((listnumber - 1)).End_posi + 1;// tempposi.End_posi + 1;
                        liststructobj.End_posi = (liststructobj.Start_posi + dtColumn.Rows.Count) - 1;
                        liststructobj.Dtdata_StartPosi = liststructobj.Start_posi;
                        liststructobj.Dtdata_EndPosi = liststructobj.End_posi;
                        liststructobj.datacount = dtColumn.Rows.Count;
                        endposition = liststructobj.End_posi;
                        Positionlist.Add(liststructobj);
                    }
                    catch { }

                }
            }
            else
            {
                var tempposi = Positionlist.ElementAt(listnumber);

                if (listnumber == 0)
                {

                    //tempposi.Start_posi = tempposi.End_posi + 1;
                    tempposi.Dtdata_StartPosi = tempposi.End_posi + 1;
                    tempposi.Dtdata_EndPosi = (tempposi.Dtdata_StartPosi + dtColumn.Rows.Count) - 1;
                    tempposi.End_posi = tempposi.Dtdata_EndPosi;
                    tempposi.datacount = tempposi.datacount + dtColumn.Rows.Count;
                }
                else
                {
                    tempposi.Start_posi = Positionlist.ElementAt(listnumber - 1).End_posi + 1;
                    tempposi.Dtdata_StartPosi = tempposi.End_posi + 1;
                    tempposi.Dtdata_EndPosi = (tempposi.Dtdata_StartPosi + dtColumn.Rows.Count) - 1;
                    tempposi.End_posi = tempposi.Dtdata_EndPosi;
                    tempposi.datacount = tempposi.datacount + dtColumn.Rows.Count;


                }

                if ((tempposi.Dtdata_StartPosi - 1) != endposition)
                {
                    try
                    {
                        Excel.Range temprange = templateList.Rows.Item[tempposi.Dtdata_StartPosi].Resize(dtColumn.Rows.Count);
                        temprange.Insert(Excel.XlInsertShiftDirection.xlShiftDown);
                    }
                    catch (Exception ex)
                    {

                    }
                }
                endposition = tempposi.End_posi;
                for (int i = listnumber; i < (Positionlist.Count) - 1; i++)
                {
                    var updateposi = Positionlist.ElementAt(i + 1);
                    updateposi.Start_posi = Positionlist.ElementAt(i).End_posi + 1;//(updateposi.Start_posi + dt.Rows.Count);
                    /*var updateposi = Positionlist.ElementAt(listnumber + 1);
                    updateposi.Start_posi = Positionlist.ElementAt(listnumber).End_posi + 1;//(updateposi.Start_posi + dt.Rows.Count);*/
                    updateposi.End_posi = (updateposi.Start_posi + updateposi.datacount) - 1;
                    endposition = updateposi.End_posi;
                }
            }

            return Positionlist.ElementAt(listnumber);
        }
        private Int32 pogressbarcheckvaluefuntion(Int32 value)//IL_JP_MAM_007:4295055420
        {
            if (pogressbarcheckvalue > value)
            {
                return pogressbarcheckvalue;
            }
            else
            {
                pogressbarcheckvalue = value;
                return value;
            }

        }
        // criteria fetching common Code But logic for AND ,OR is little diffrnt not included here;hence any changes for criteria Fetching  needed to do in AND ,OR also
        private void FetchCriteria(string CriteriaVariableText, string criteriaOperatorText, string criteriaValueText,
            KeyValuePair<int, DataProcess.Crit_Inst_Operator> kvp, _INewQuestion newQuestion, bool isnotcriteriavalue, bool isivcriteria, bool isnacriteria, bool IsQCweb = true)
        {
            string delim = string.Empty;
            int opencount = 0;
            int closecount = 0;
            int diff = 0;

            string cellitemid = (Definitions.VariableDictionary[CriteriaVariableText].ItemId).ToString();// DPSheet.Cells[kvp.Key, Constants.DP.CriteriaVariableColumn].Text;
            string cellopertor = criteriaOperatorText;//DPSheet.Cells[kvp.Key, Constants.DP.CriteriaOperatorColumn].Text;
            string cellvalue = criteriaValueText;//DPSheet.Cells[kvp.Key, Constants.DP.CriteriavalueColumn].Text;;
                                                 // cellvalue = DataProcess.MinMaxAppendWithMinus(cellvalue, kvp.Key, Constants.DP.CriteriaVariableColumn);
                                                 //Redmine id: 175537
            if (Definitions.VariableDictionary[CriteriaVariableText].AnswerType != Constants.AnswerType.FA)
            {
                cellvalue = DataProcess.MinMaxAppendWithMinus(cellvalue, kvp.Key, Constants.DP.CriteriaVariableColumn, DPSheet);
            }
            else if (Definitions.VariableDictionary[CriteriaVariableText].AnswerType == Constants.AnswerType.FA)//https://app.gluemodel.com/#/project/task/4295063196
            {
                cellvalue = Regex.Escape(cellvalue);
            }
            
            if (Definitions.VariableDictionary[CriteriaVariableText].AnswerType != Constants.AnswerType.FA)//Redmine:210212
            {
                //Redmine id: 176455
                try
                {
                    var criteriaval = cellvalue.Split('@')[0].Replace("[", "");
                    if (!Array.Exists(Definitions.VariableDictionary.Keys.ToArray(), element => element.Contains(criteriaval)))
                    {

                        cellvalue = cellvalue.Replace("(", "");
                        cellvalue = cellvalue.Replace(")", "");
                    }
                }
                catch { }
            }
            // GetCommaSeperated(cellvalue, CriteriaVariableText)//Redmine id: 170984
            if (cellvalue.StartsWith("!") || cellvalue.StartsWith("<>"))//Redmine id: 170984//if not check * Dk is there if not ,add to cell value
            {
                isnotcriteriavalue = true;
                if (!cellvalue.Contains("*"))
                {
                    isivcriteria = true;
                }
                if (!cellvalue.Contains("DK"))
                {
                    isnacriteria = true;
                }
            }
            //(Definitions.VariableDictionary[CriteriaVariableText].AnswerType == Constants.AnswerType.N|| Definitions.VariableDictionary[CriteriaVariableText].AnswerType == Constants.AnswerType.MA) //[Redmine id :177838] solution for upcommng error
            // if (Definitions.VariableDictionary[CriteriaVariableText].AnswerType == Constants.AnswerType.N && (cellvalue.StartsWith("!") || cellvalue.StartsWith("<>")))//Redmine id: 170984//if not check * Dk is there if not ,add to cell value
            if (Definitions.VariableDictionary[CriteriaVariableText].AnswerType != Constants.AnswerType.FA && (cellvalue.StartsWith("!") || cellvalue.StartsWith("<>")))//Redmine id: 170984//if not check * Dk is there if not ,add to cell value
            {
                cellopertor = "<>";
                cellvalue = cellvalue.TrimStart('!');
            }
            if (Definitions.VariableDictionary[CriteriaVariableText].AnswerType != Constants.AnswerType.N && Definitions.VariableDictionary[CriteriaVariableText].AnswerType != Constants.AnswerType.FA)//IL_JP_MAM_007:4295056906
            {
                cellvalue = GetCommaSeperated(cellvalue, CriteriaVariableText);//Redmine id: 170984
            }
            //cellvalue = GetCommaSeperated(cellvalue, CriteriaVariableText);//Redmine id: 170984
            if (isnotcriteriavalue)//Redmine id: 170984//add *,Dk to value 
            {
                if (isnacriteria)
                {
                    // cellvalue += ",DK";
                    isnacriteria = false;
                }
                if (isivcriteria)
                {
                    //cellvalue += ",*";
                    isivcriteria = false;
                }
            }
            if (kvp.Value.instruction == Constants.DP.InstructionAND)
            {
                delim = string.Empty;//for adding ( 
                opencount = CriteriaQuerystring.Count(f => f == '(');
                closecount = CriteriaQuerystring.Count(f => f == ')');
                diff = opencount - closecount;
                if (diff == 0) delim = "(";
                CriteriaQuerystring += (delim + cellitemid + cellopertor + cellvalue + " & ");
            }
            else if (kvp.Value.instruction == Constants.DP.InstructionOR)
            {
                delim = string.Empty;//for adding ( 
                opencount = CriteriaQuerystring.Count(f => f == '(');
                closecount = CriteriaQuerystring.Count(f => f == ')');
                diff = opencount - closecount;
                if (diff > 0) delim = ")";
                CriteriaQuerystring += (cellitemid + cellopertor + cellvalue + delim + " | ");
            }
            else
            {
                delim = string.Empty;//for adding ( 
                opencount = CriteriaQuerystring.Count(f => f == '(');
                closecount = CriteriaQuerystring.Count(f => f == ')');
                diff = opencount - closecount;
                if (diff > 0) delim = ")"; // if (diff != 1) { delim = "))"; } else { delim = ")"; }
                CriteriaQuerystring += (cellitemid + cellopertor + cellvalue + delim);
            }

            // delQuerystring = (delQuerystring + delim);
            if (IsQCweb)
            {
                try
                {
                    INewQuestionSectors Criteriasectors = newQuestion.Sectors;
                    var virtualcriteriaSector = Criteriasectors.Add(CriteriaQuerystring, true) as INewVirtualQuestionSector;
                    virtualcriteriaSector.Alias = "1";
                }
                catch (Exception ex) { }

            }
        }
        private string EVAL_FA_Value(string rawformula, List<string> variablenames, Dictionary<string, string> variablevalues)
        {
            Dictionary<string, string> temp = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> key in variablevalues)
            {
                temp.Add(key.Key, key.Value);
            }
            for (int i = 0; i < temp.Count; i++)
            {
                temp[temp.Keys.ElementAt(i)] = "[" + variablenames[i] + "]";
            }
            string returnstring = string.Empty;
            string temprawformula = GlobalMethodClass.BuildExpressionFA_Temp(rawformula, variablenames, temp);
            var evalres = DPSheet.Application.Evaluate(temprawformula);
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
                    returnstring = string.Empty;
                    break;
                default:

                    returnstring = evalres.ToString();
                    foreach (string variablename in variablenames)
                    {
                        if (!returnstring.Contains(variablename))
                        {
                            returnstring = string.Empty;
                            break;
                        }
                    }
                    if (returnstring.Contains("&"))
                    {
                        returnstring.Replace("&", string.Empty);
                    }
                    returnstring = GlobalMethodClass.BuildExpressionFA_(returnstring, variablenames, variablevalues);
                    break;
            }
            if (returnstring.Length > QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit)
            {
                returnstring = returnstring.PadRight(QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit).Substring(0, QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit);
            }
            return returnstring;
        }
        public string GetProcessIdPath()//#212092 
        {
            string outputfilepath = System.IO.Path.Combine(Path.GetTempPath(), "QC4\\");
            ApplicationConfig appConfig = new ApplicationConfig();
            outputfilepath =
    System.IO.Path.Combine(
        appConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_COMMON_ACCUMULATE_PATH_AP));
            GlobalMethodClass.GuaranteeDirectoryExist(outputfilepath);

            return outputfilepath;
        }
    }
}