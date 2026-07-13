using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QC4Common.Model;
using Constant = QC4Common.Common.Constants;
using ExcelAddIn.Common;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.SQLite;
using System.Data;
using QC4Common.DB;
using System.Windows.Forms;

namespace ExcelAddIn.QS
{
    public class IntegrityCheck
    {
        private Excel.Workbook workbook;
        private Excel.Worksheet worksheet;
        Dictionary<string, QuestionSettings> dict;
        string connectionString;

        public IntegrityCheck(Excel.Workbook workbook)
        {
            this.workbook = workbook;
            dict = QC4Common.Util.DictionaryUtil.PopulateQSDictionary(workbook, true, false);
            worksheet = ExcelUtil.GetWorkSheetByCodeName(workbook, Constant.SheetCodeName.QuestionSetting);
            connectionString = QC4Common.DB.DBHelper.GetConnectionString(workbook);
        }
        /// <summary>
        /// Integerity checking for Question settings
        /// </summary>
        /// <param name="variableChanges">Updated variable list</param>
        /// <param name="answerChanges">Out updated Answer table column list</param>
        /// <param name="countChanges">out updated variable count</param>
        /// <param name="deleteList">Out deleted variable list</param>
        /// <param name="updateList">Out deleted list</param>
        /// <param name="alert">bool value that represents whether show the alert or not</param>
        /// <returns>Result of the integrity check</returns>
        public ReturnClass Check(out List<QuestionSettings> variableChanges, out List<QuestionSettings> answerChanges
            , out List<QuestionSettings> countChanges, out List<QuestionSettings> deleteList, out List<QuestionSettings> updateList, bool alert = true)
        {
            answerChanges = null;
            countChanges = null;
            deleteList = dict.Where(q => !q.Value.IsFound).Select(val => val.Value).ToList();
            var ids = dict.Where(q => !q.Value.IsFound).Select(val => val.Key).ToList();
            try
            {
                for (int i = 0; i < dict.Count; i++)
                {
                    QuestionSettings qset = dict.Values.ToList().ElementAt(i);
                    if (qset.Variable.Length < 1 && qset.VariableBefore.Length > 0)
                    {
                        DB.QuestionSettingDao questionSetting = new DB.QuestionSettingDao(QC4Common.DB.DBHelper.GetConnectionString(workbook));
                        if (qset.QuestionFlag == Constants.QuestionFlag.Imp)
                        {
                            questionSetting.DeleteAnswer(qset.VariableBefore);
                            Excel.Worksheet s = QC4Common.Util.ExcelUtil.GetWorkSheetBySheetName(workbook, Constants.SheetType.sh_Data01 + "(Processed)");
                            if (s != null)
                            {
                                questionSetting.DeleteData_After(qset.Id);
                            }
                        }
                        else if (qset.QuestionFlag == Constants.QuestionFlag.An)
                        {
                            questionSetting.DeleteFromMultiVariateTable(qset.VariableBefore);
                            QC4Common.Util.ExcelUtil.DeleteFromMultiVariateSheet(workbook, qset.VariableBefore);
                        }
                    }
                }
            }
            catch { }

            foreach (string id in ids)
            {
                dict.Remove(id);
            }
            if (deleteList.Count > 0)
            {
                foreach (QuestionSettings qs in deleteList)
                {
                    if (qs.QuestionFlag != Constant.QuestionFlag.Org)
                    {
                        if (qs.Variable != String.Empty)
                            QC4Common.Util.ExcelUtil.DeleteWeightBack(workbook, qs.Variable);
                        else
                            QC4Common.Util.ExcelUtil.DeleteWeightBack(workbook, qs.VariableBefore);
                    }
                }
            }

            DBHelper.CheckIfColumnExists(workbook, dict.Values.ToList(), out List<string> varList, out List<string> colList, out List<decimal> idList);

            ReturnClass result = ValidateVarable(out variableChanges, out updateList);
            if (!result.Result)
            {
                return result;
            }

            List<QuestionSettings> newQuestions = new List<QuestionSettings>();
            var newQList = dict.Select(d => d.Value).Where(q => q.IsNew);
            if (newQList != null && newQList.Count() > 0)
            {
                newQuestions = dict.Select(d => d.Value).Where(q => q.IsNew).ToList();
            }

            if (deleteList.Count() > 0 && newQuestions.Count() > 0)
            {
                foreach (QuestionSettings newQ in newQuestions)
                {
                    QuestionSettings qs = deleteList.Where(q => q.VariableBefore == newQ.Variable).FirstOrDefault();
                    if (qs != null)
                    {
                        try
                        {
                            QC4Common.Util.QSUtil.SetRowName(worksheet, worksheet.Rows[newQ.RowNumber], (int)qs.Id);
                            dict[newQ.Variable].IsNew = false;
                            dict[newQ.Variable].VariableBefore = newQ.Variable;
                            dict[newQ.Variable].Id = qs.Id;
                            dict[newQ.Variable].ItemId = (Int32)qs.Id;
                            dict[newQ.Variable].QuestionFlag = qs.QuestionFlag;
                            dict[newQ.Variable].AnswerTypeBefore = qs.AnswerTypeBefore;
                            dict[newQ.Variable].CategoryCountBefore = qs.CategoryCountBefore;
                        }
                        catch { }
                        deleteList.Remove(qs);
                    }
                }
            }

            result = ValidateAnswer(out answerChanges, varList);
            if (!result.Result)
            {
                return result;
            }

            result = ValidateCount(out countChanges, varList);
            if (!result.Result)
            {
                return result;
            }



            if (deleteList.Count() > 0)
            {
                bool alertFlag = false;
                string message = "";
                DBHelper.GetAnswerTableColumns(workbook, out List<string> columns);
                foreach (QuestionSettings qs in deleteList)
                {
                    if (qs.QuestionFlag == Constant.QuestionFlag.Org)
                    {
                        return new ReturnClass(false, null, String.Format(AddinResource.QS_ALERT_DELETE_ORG_QUESTION, qs.Variable));
                    }
                    else if (qs.QuestionFlag == Constant.QuestionFlag.An
                        || qs.QuestionFlag == Constant.QuestionFlag.Imp
                        || qs.QuestionFlag == Constant.QuestionFlag.New)
                    {
                        if (columns.IndexOf("q_" + qs.Id) != -1)
                        {
                            alertFlag = true;
                            message += qs.VariableBefore + " - " + qs.AnswerTypeBefore + ", ";
                        }
                    }
                }

                if (ThisAddIn.DataProcessSheet != null && deleteList.Count > 0)
                {
                    if (ThisAddIn.DataProcessSheet.ProInitialized)
                        ThisAddIn.TriggerUpdateTaskPane();
                }

                if (alertFlag && alert)
                {
                    Definitions.isQsChanged = true;
                    message = message.Substring(0, message.Length - 1);
                    DialogResult dialog = MessageBox.Show(String.Format(AddinResource.QS_CONFM_ALERT_IMPAN_DELETE, message), "QuickCross", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dialog == DialogResult.Cancel)
                    {
                        return new ReturnClass(false, null, AddinResource.QS_FIX_DELETED_QUESTIONS);
                    }
                    //TODO code to remove the column in a answer table if Ok
                    else
                    {
                        QC4Common.Common.CommonFlag.SetIsDataAfterUpdated(Globals.ThisAddIn.Application.ActiveWorkbook, false);
                    }
                }
            }

            Definitions.VariableDictionary = dict;
            return new ReturnClass(true);
        }

        public ReturnClass ValidateVarable(out List<QuestionSettings> variableChange, out List<QuestionSettings> updateList)
        {
            updateList = null;
            variableChange = dict
                  .Where(pair => pair.Value.VariableBefore != pair.Key && !pair.Value.IsNew)
                  .Select(val => val.Value).ToList();

            if (variableChange.Count() == 0)
            {
                return new ReturnClass(true);
            }

            DBHelper.CheckIfColumnExists(workbook, variableChange, out List<string> variables, out List<string> columns, out List<decimal> ids);

            if (ids.Count() != 0)
            {
                updateList = new List<QuestionSettings>();
                for (int i = variableChange.Count() - 1; i >= 0; i--)
                {
                    var qs = variableChange[i];
                    if (ids.Contains(qs.Id))
                    {
                        updateList.Add(qs);
                        variableChange.Remove(qs);
                    }
                }
            }

            if (updateList != null && updateList.Count() > 0)
            {
                var list = updateList.Select(q => q).Where(q => q.QuestionFlag != Constants.QuestionFlag.New);
                if (list.Count() > 0)
                {
                    return new ReturnClass(false, GetRange(list.First().RowNumber, Constant.QS.QsColItem), AddinResource.QS_INTEGIRITY_VARIABLE_CHANGED, new string[] { list.First().RowNumber.ToString(), list.First().Variable, list.First().VariableBefore });
                }
            }

            if (variableChange.Count() == 0)
            {
                return new ReturnClass(true);
            }
            return new ReturnClass(false, GetRange(variableChange.First().RowNumber, Constant.QS.QsColItem), AddinResource.QS_INTEGIRITY_VARIABLE_CHANGED, new string[] { variableChange.First().RowNumber.ToString(), variableChange.First().Variable, variableChange.First().VariableBefore });
        }

        //to validate answer type change rule return true if any error & return false no error
        public ReturnClass ValidateAnswer(out List<QuestionSettings> answerChanges, List<String> variableList)
        {
            answerChanges = dict.Where(pair => pair.Value.AnswerType != pair.Value.AnswerTypeBefore && !pair.Value.IsNew)
                 .Select(val => val.Value).ToList();

            List<QuestionSettings> checkList = new List<QuestionSettings>();
            for (int i = answerChanges.Count() - 1; i >= 0; i--)
            {
                if (!variableList.Contains(answerChanges[i].Variable))
                {
                    checkList.Add(answerChanges[i]);
                }
            }

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                foreach (QuestionSettings qs in checkList)
                {
                    switch (qs.AnswerTypeBefore)
                    {
                        case Constant.AnswerType.D:
                        case Constant.AnswerType.MA:
                            return new ReturnClass(false, GetRange(qs.RowNumber, Constant.QS.QsColAnswerType), AddinResource.QS_ANSWER_TYPE_RULE, new string[] { qs.RowNumber.ToString(), qs.Variable, qs.AnswerType, qs.AnswerTypeBefore });
                        case Constant.AnswerType.SA:
                            if (Constant.AnswerType.N != qs.AnswerType)
                            {
                                return new ReturnClass(false, GetRange(qs.RowNumber, Constant.QS.QsColAnswerType), AddinResource.QS_ANSWER_TYPE_RULE, new string[] { qs.RowNumber.ToString(), qs.Variable, qs.AnswerType, qs.AnswerTypeBefore });
                            }
                            break;
                        case Constant.AnswerType.N:
                            if (Constant.AnswerType.SA != qs.AnswerType)
                            {
                                return new ReturnClass(false, GetRange(qs.RowNumber, Constant.QS.QsColAnswerType), AddinResource.QS_ANSWER_TYPE_RULE, new string[] { qs.RowNumber.ToString(), qs.Variable, qs.AnswerType, qs.AnswerTypeBefore });
                            }

                            ReturnClass rc = ValidateSACount(connection, qs);
                            if (!rc.Result)
                            {
                                return rc;
                            }
                            break;
                        case Constant.AnswerType.FA:
                            if (Constant.AnswerType.N != qs.AnswerType && Constant.AnswerType.SA != qs.AnswerType)
                            {
                                return new ReturnClass(false, GetRange(qs.RowNumber, Constant.QS.QsColAnswerType), AddinResource.QS_ANSWER_TYPE_RULE, new string[] { qs.RowNumber.ToString(), qs.Variable, qs.AnswerType, qs.AnswerTypeBefore });
                            }

                            switch (qs.AnswerType)
                            {
                                case Constant.AnswerType.SA:
                                    rc = ValidateSACount(connection, qs);
                                    if (!rc.Result)
                                    {
                                        return rc;
                                    }
                                    break;
                                case Constant.AnswerType.N:
                                    rc = ValidateNCount(connection, qs);
                                    if (!rc.Result)
                                    {
                                        return rc;
                                    }
                                    break;
                                default:
                                    return new ReturnClass(false, GetRange(qs.RowNumber, Constant.QS.QsColAnswerType), AddinResource.QS_ANSWER_TYPE_RULE, new string[] { qs.RowNumber.ToString(), qs.Variable, qs.AnswerType, qs.AnswerTypeBefore });
                            }
                            break;
                    }
                }
            }
            return new ReturnClass(true);
        }

        private ReturnClass ValidateCount(out List<QuestionSettings> countChanges, List<String> variableList)
        {
            countChanges = dict.Where(pair => pair.Value.CategoryCount != pair.Value.CategoryCountBefore && !pair.Value.IsNew)
                 .Select(val => val.Value).ToList();

            List<QuestionSettings> checkList = new List<QuestionSettings>();
            for (int i = countChanges.Count() - 1; i >= 0; i--)
            {
                if (!variableList.Contains(countChanges[i].Variable))
                {
                    checkList.Add(countChanges[i]);
                }
            }

            List<QuestionSettings> cat = checkList.Where(q => q.CategoryCount < q.CategoryCountBefore && (q.AnswerTypeBefore != Constants.AnswerType.SA || (q.AnswerTypeBefore == Constants.AnswerType.SA && q.AnswerType != Constants.AnswerType.N))).ToList();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                foreach (QuestionSettings qs in cat)
                {
                    switch (qs.AnswerType)
                    {
                        case Constant.AnswerType.SA:
                            ReturnClass rc = ValidateSACount(connection, qs);
                            if (!rc.Result)
                            {
                                return rc;
                            }
                            break;
                        case Constant.AnswerType.MA:
                            rc = ValidateMACount(connection, qs);
                            if (!rc.Result)
                            {
                                return rc;
                            }
                            break;
                    }
                }
            }
            return new ReturnClass(true);
        }

        ReturnClass ValidateNCount(SQLiteConnection connection, QuestionSettings qs)
        {
            long count = 0;
            string columnName = qs.Id == 0 ? "sample_id" : "q_" + qs.Id;

            switch (qs.AnswerTypeBefore)
            {
                case Constant.AnswerType.FA:
                    DataTable dt = null;
                    string sql;
                    if (qs.QuestionFlag == Constant.QuestionFlag.New)
                    {
                        sql = "select count(" + columnName + ") from data_after_process where " + columnName + " != 0 and " + columnName + " != '*' and " + columnName + " is not null and " + columnName + " != ''";
                        dt = DBHelper.GetDataTable(sql, connection);
                        count = Convert.ToInt32(dt.Rows[0][0]);
                        sql = "select " + columnName + " from data_after_process where abs(" + columnName + ") > 0 and " + columnName + " != 0 and " + columnName + " != '*' and " + columnName + " is not null and " + columnName + " != ''";
                        dt = QC4Common.DB.DBHelper.GetDataTable(sql, connection);
                    }
                    else if (qs.QuestionFlag == Constant.QuestionFlag.An)
                    {
                        sql = "select count(" + columnName + ") from multivariate where " + columnName + " != 0 and " + columnName + " != '*' and " + columnName + " is not null and " + columnName + " != ''";
                        dt = DBHelper.GetDataTable(sql, connection);
                        count = Convert.ToInt32(dt.Rows[0][0]);
                        sql = "select " + columnName + " from multivariate where abs(" + columnName + ") > 0 and " + columnName + " != 0 and " + columnName + " != '*' and " + columnName + " is not null and " + columnName + " != ''";
                        dt = QC4Common.DB.DBHelper.GetDataTable(sql, connection);
                    }
                    else
                    {
                        sql = "select count(" + columnName + ") from answers where " + columnName + " != 0 and " + columnName + " != '*' and " + columnName + " is not null and " + columnName + " != ''";
                        dt = DBHelper.GetDataTable(sql, connection);
                        count = Convert.ToInt32(dt.Rows[0][0]);
                        sql = "select " + columnName + " from answers where abs(" + columnName + ") > 0 and " + columnName + " != 0 and " + columnName + " != '*' and " + columnName + " is not null and " + columnName + " != ''";
                        dt = QC4Common.DB.DBHelper.GetDataTable(sql, connection);
                    }
                    if ((count != 0 && dt == null) || dt.Rows.Count != count)
                    {
                        return new ReturnClass(false, GetRange(qs.RowNumber, Constant.QS.QsColAnswerType), AddinResource.QS_RAWDATA_ANSWER_TYPE_NOT_MATCH1, new string[] { qs.Variable, qs.AnswerType, qs.AnswerTypeBefore });
                        // return new ReturnClass(false, GetRange(qs.RowNumber, Constant.QS.QsColAnswerType), AddinResource.QS_RAWDATA_ANSWER_TYPE_NOT_MATCH, new string[] { qs.RowNumber.ToString(), qs.Variable, qs.AnswerType, qs.AnswerTypeBefore });
                    }

                    int rows = dt.Rows.Count;
                    string Answervalue;
                    for (int i = 0; i < rows; i++)
                    {
                        Answervalue = dt.Rows[i][0].ToString();
                        if (!Double.TryParse(Answervalue, out double val) || Answervalue.Contains(",")||Answervalue.StartsWith("-"))
                        {
                            return new ReturnClass(false, GetRange(qs.RowNumber, Constant.QS.QsColAnswerType), AddinResource.QS_RAWDATA_ANSWER_TYPE_NOT_MATCH1, new string[] { qs.Variable, qs.AnswerType, qs.AnswerTypeBefore });
                            //return new ReturnClass(false, GetRange(qs.RowNumber, Constant.QS.QsColAnswerType), AddinResource.QS_RAWDATA_ANSWER_TYPE_NOT_MATCH, new string[] { qs.RowNumber.ToString(), qs.Variable, qs.AnswerType, qs.AnswerTypeBefore });
                        }
                    }
                    break;
                case Constant.AnswerType.SA:
                    if (qs.QuestionFlag == Constant.QuestionFlag.New)
                    {
                        sql = "select count(" + columnName + ") from data_after_proces where (" + columnName + " glob '*[^0-9]*' and " + columnName + " != '*' and " + columnName + " != '' and " + columnName + " is not null)";
                        dt = QC4Common.DB.DBHelper.GetDataTable(sql, connection);
                        count = Convert.ToInt32(dt.Rows[0][0]);
                    }
                    else if (qs.QuestionFlag == Constant.QuestionFlag.An)
                    {
                        sql = "select count(" + columnName + ") from multivariate where (" + columnName + " glob '*[^0-9]*' and " + columnName + " != '*' and " + columnName + " != '' and " + columnName + " is not null)";
                        dt = QC4Common.DB.DBHelper.GetDataTable(sql, connection);
                        count = Convert.ToInt32(dt.Rows[0][0]);
                    }
                    else
                    {
                        sql = "select count(" + columnName + ") from answers where (" + columnName + " glob '*[^0-9]*' and " + columnName + " != '*' and " + columnName + " != '' and " + columnName + " is not null)";
                        dt = QC4Common.DB.DBHelper.GetDataTable(sql, connection);
                    }
                    count = Convert.ToInt32(dt.Rows[0][0]);
                    if (count > 0)
                    {
                        return new ReturnClass(false, GetRange(qs.RowNumber, Constant.QS.QsColAnswerType), AddinResource.QS_RAWDATA_ANSWER_TYPE_NOT_MATCH, new string[] { qs.RowNumber.ToString(), qs.Variable, qs.AnswerType, qs.AnswerTypeBefore });
                    }
                    break;
            }
            //if (count > 0)
            //{
            //	return new ReturnClass(false, GetRange(qs.RowNumber, Constant.QS.QsColAnswerType), AddinResource.QS_RAWDATA_ANSWER_TYPE_NOT_MATCH, new string[] { qs.RowNumber.ToString(), qs.Variable, qs.AnswerType, qs.AnswerTypeBefore });
            //}
            return new ReturnClass(true);
        }

        private ReturnClass ValidateSACount(SQLiteConnection connection, QuestionSettings qs)
        {
            long count = 0;
            string columnName = qs.Id == 0 ? "sample_id" : "q_" + qs.Id;
            string sql;
            DataTable dt = null;
            if (qs.QuestionFlag == Constant.QuestionFlag.New)
            {
                sql = "select count(" + columnName + ") from data_after_process where (" + columnName + " glob '*[^0-9]*' and " + columnName + " != '*' and " + columnName + " != '' and " + columnName + " is not null) or " + columnName + " = 0";
                dt = QC4Common.DB.DBHelper.GetDataTable(sql, connection);
                count = Convert.ToInt32(dt.Rows[0][0]);
            }
            else if (qs.QuestionFlag == Constants.QuestionFlag.An)
            {
                sql = "select count(" + columnName + ") from multivariate where (" + columnName + " glob '*[^0-9]*' and " + columnName + " != '*' and " + columnName + " != '' and " + columnName + " is not null) or " + columnName + " = 0";
                dt = QC4Common.DB.DBHelper.GetDataTable(sql, connection);
                count = Convert.ToInt32(dt.Rows[0][0]);
            }
            else
            {
                sql = "select count(" + columnName + ") from answers where (" + columnName + " glob '*[^0-9]*' and " + columnName + " != '*' and " + columnName + " != '' and " + columnName + " is not null) or " + columnName + " = 0";
                dt = QC4Common.DB.DBHelper.GetDataTable(sql, connection);
                count = Convert.ToInt32(dt.Rows[0][0]);
            }
            if (count > 0)
            {
                return new ReturnClass(false, GetRange(qs.RowNumber, Constant.QS.QsColAnswerType), AddinResource.QS_RAWDATA_ANSWER_TYPE_NOT_MATCH1, new string[] { qs.Variable, qs.AnswerType, qs.AnswerTypeBefore });
            }
            if (qs.QuestionFlag == Constant.QuestionFlag.New)
            {
                sql = "select count(value) from (select CAST(" + columnName + " as integer) as value from data_after_process where " + columnName + " glob '[0-9]' or " + columnName + " glob '[0-9][0-9]' or " + columnName + " glob '[0-9][0-9][0-9]') where value < 1 or value > " + qs.CategoryCount + "";
                dt = QC4Common.DB.DBHelper.GetDataTable(sql, connection);
                count = Convert.ToInt32(dt.Rows[0][0]);
            }
            else if (qs.QuestionFlag == Constants.QuestionFlag.An)
            {
                sql = "select count(value) from (select CAST(" + columnName + " as integer) as value from multivariate where " + columnName + " glob '[0-9]' or " + columnName + " glob '[0-9][0-9]' or " + columnName + " glob '[0-9][0-9][0-9]') where value < 1 or value > " + qs.CategoryCount + "";
                dt = QC4Common.DB.DBHelper.GetDataTable(sql, connection);
                count = Convert.ToInt32(dt.Rows[0][0]);
            }
            else
            {
                sql = "select count(value) from (select CAST(" + columnName + " as integer) as value from answers where " + columnName + " glob '[0-9]' or " + columnName + " glob '[0-9][0-9]' or " + columnName + " glob '[0-9][0-9][0-9]') where value < 1 or value > " + qs.CategoryCount + "";
                dt = QC4Common.DB.DBHelper.GetDataTable(sql, connection);
                count = Convert.ToInt32(dt.Rows[0][0]);
            }
            if (count > 0)
            {
                return new ReturnClass(false, GetRange(qs.RowNumber, Constant.QS.QsColCategories), AddinResource.QS_RAWDATA_CATEGORY_NOT_MATCH, new string[] { qs.RowNumber.ToString(), qs.Variable, qs.AnswerType, qs.AnswerTypeBefore });
            }
            return new ReturnClass(true);
        }

        private ReturnClass ValidateMACount(SQLiteConnection connection, QuestionSettings qs)
        {
            if (qs.CategoryCount >= qs.CategoryCountBefore)
            {
                return new ReturnClass(true);
            }
            string sql = string.Empty;
           string columnName = qs.Id == 0 ? "sample_id" : "q_" + qs.Id;
            if (qs.QuestionFlag == Constant.QuestionFlag.New)//Redmine id: 190379
            {
                 sql = "select count(" + columnName + ") from data_after_process where " + columnName + " != '*' and " + columnName + " != '' and " + columnName + " is not null and " + columnName + " glob '*1*" + String.Concat(Enumerable.Repeat("?", qs.CategoryCount)) + "'";
            }
            else if (qs.QuestionFlag == Constant.QuestionFlag.An)
            {
                sql = "select count(" + columnName + ") from multivariate where " + columnName + " != '*' and " + columnName + " != '' and " + columnName + " is not null and " + columnName + " glob '*1*" + String.Concat(Enumerable.Repeat("?", qs.CategoryCount)) + "'";
            }
            else
            {
                 sql = "select count(" + columnName + ") from answers where " + columnName + " != '*' and " + columnName + " != '' and " + columnName + " is not null and " + columnName + " glob '*1*" + String.Concat(Enumerable.Repeat("?", qs.CategoryCount)) + "'";
            }
            DataTable dt = DBHelper.GetDataTable(sql, connection);
            if (Convert.ToInt32(dt.Rows[0][0]) > 0)
            {
                return new ReturnClass(false, GetRange(qs.RowNumber, Constant.QS.QsColCategories), AddinResource.QS_RAWDATA_CATEGORY_NOT_MATCH, new string[] { qs.RowNumber.ToString(), qs.Variable, qs.AnswerType, qs.AnswerTypeBefore });
            }
            return new ReturnClass(true);
        }

        private Excel.Range GetRange(int row, int col)
        {
            return worksheet.Cells[row, col];
        }
    }
}
