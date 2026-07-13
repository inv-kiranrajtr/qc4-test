using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using QC4Common.Model;
using QC4Common.Common;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.SQLite;
using DB = QC4Common.DB;
using Macromill.QCWeb.Question;
using Constant = QC4Common.Common.Constants;
using QC4Common.DB;
using QC4Common.Model.Model;
using log4net;
using System.Reflection;

namespace QC4Common.Util
{
   
    public class DictionaryUtil
	{
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
       
        public static Dictionary<string, QuestionSettings> PopulateQSDictionary(Excel.Workbook workbook, bool dbCheck = true, bool sheetOnly = true)
        {
            if (null == workbook)
            {
                return null;
            }
            FormUtil frmutil = new FormUtil();
            Excel.Worksheet worksheet = ExcelUtil.GetWorkSheetByCodeName(workbook, Constants.SheetCodeName.QuestionSetting);
            if (worksheet == null)
            {
                return null;
            }
            
            Dictionary<int, QuestionSettings> questions = new Dictionary<int, QuestionSettings>();
            Dictionary<int, int> rowIdDict = new Dictionary<int, int>();

			Excel.Range start = worksheet.Cells[Constants.QS.QsRowDataStart, Constants.QS.QsColItem];
            Excel.Range end = ExcelUtil.EndxlUp(start);
            Excel.Range total;

            if (dbCheck)
            {
                total = worksheet.get_Range(start, end);
				total = total.Rows;
				Excel.XlCalculation xlCalculation = workbook.Application.Calculation;
				workbook.Application.Calculation = Excel.XlCalculation.xlCalculationManual;
				Regex regex = new Regex("(.*)\\$(.*)\\$(.*)");
				foreach (Excel.Name name in workbook.Names)
				{
					string str = name.Name;
					if (QSUtil.IsRowName(str))
					{
                        try
                        {
							Match match = regex.Match(name.RefersTo);
							if (match.Success)
							{
								rowIdDict.Add(Convert.ToInt32(match.Groups[3].Value) - 3, Convert.ToInt32(str.Substring(str.Length - 5)));
							}
                        }
                        catch (Exception ex)
                        {
							Console.Write(ex.Message);
                            try
                            {
                                name.Delete();
                            }
                            catch
                            { }
                        }
                    }
                }
               
                workbook.Application.Calculation = xlCalculation;
             
				questions = GetQSFromDb(workbook, rowIdDict.Values.ToArray());
            
            }

            start = worksheet.Range[Constants.QS.QsFirstCell];
            end = worksheet.Range[Constants.QS.QsLastColumn + end.Row];
            total = worksheet.get_Range(start, end);
            Object[,] objAry = total.Value2;

            Dictionary<string, QuestionSettings> dict = new Dictionary<string, QuestionSettings>();
            
            int max = objAry.GetLength(0);
            for (int i = 1; i <= max; i++)
            {
                QuestionSettings qs;
                if (rowIdDict.ContainsKey(i) && questions.ContainsKey(rowIdDict[i]))
                {
                    qs = questions[rowIdDict[i]];
                    questions.Remove(rowIdDict[i]);
                    qs.IsNew = false;
                }
                else
                {
                    qs = new QuestionSettings();
                    qs.IsNew = true;
                    qs.QuestionFlag = "New";
                }

				qs.RowNumber = i + 3;
				qs.ItemId = Convert.ToInt32(qs.Id);
				qs.IsFound = true;
				qs.QuestionFlagUpdated = objAry[i, Constants.QS.QsColNew] == null ? "" : objAry[i, Constants.QS.QsColNew].ToString();
				qs.QuestionNumber = objAry[i, Constants.QS.QsColQuestionNumber] == null ? "" : frmutil.Addsinglequete(objAry[i, Constants.QS.QsColQuestionNumber].ToString());
				qs.QuestionType = objAry[i, Constants.QS.QsColQuestiontype] == null ? "" : objAry[i, Constants.QS.QsColQuestiontype].ToString();
				qs.QuestionCount = objAry[i, Constants.QS.QsColNumberOfQuestion] == null ? 0 : string.IsNullOrEmpty(objAry[i, Constants.QS.QsColNumberOfQuestion].ToString()) ? 0 : Convert.ToInt32(objAry[i, Constants.QS.QsColNumberOfQuestion]);
				qs.Question = objAry[i, Constants.QS.QsColQuestion] == null ? "" : frmutil.Addsinglequete( objAry[i, Constants.QS.QsColQuestion].ToString());
				qs.TableHeading = objAry[i, Constants.QS.QsColTableHeading] == null ? "" : frmutil.Addsinglequete(objAry[i, Constants.QS.QsColTableHeading].ToString());
				qs.Variable = objAry[i, Constants.QS.QsColItem].ToString();
				qs.AnswerType = objAry[i, Constants.QS.QsColAnswerType].ToString();
				qs.CategoryCount = objAry[i, Constants.QS.QsColCategories] == null ? 0 : String.Empty.Equals(objAry[i, Constants.QS.QsColCategories]) ? 0 : Convert.ToInt32(objAry[i, Constants.QS.QsColCategories]);
				qs.Score = objAry[i, Constants.QS.QsColWT] == null ? "" : objAry[i, Constants.QS.QsColWT].ToString();
				qs.Sort = objAry[i, Constants.QS.QsColSortDisplay] == null ? "" : objAry[i, Constants.QS.QsColSortDisplay].ToString();

                qs.Choices = new List<string>();
                for (int j = 0; j < qs.CategoryCount; j++)
                {
                    qs.Choices.Add(objAry[i, Constants.QS.QsColChoiceBegin + j] == null ? "" : frmutil.Addsinglequete(objAry[i, Constants.QS.QsColChoiceBegin + j].ToString()));
                }

                qs.Count = null == objAry[i, Constants.QS.QsColCount] ? "" : objAry[i, Constants.QS.QsColCount].ToString();
                qs.CountBase = null == objAry[i, Constants.QS.QsColCountBase] ? "" : objAry[i, Constants.QS.QsColCountBase].ToString();
                qs.AddSubTotal = null == objAry[i, Constants.QS.QsColAddSunTotal] ? "" : objAry[i, Constants.QS.QsColAddSunTotal].ToString();
                qs.SubTotalCount = null == objAry[i, Constants.QS.QsColNumberSubTotal] ? 0 : String.Empty.Equals(objAry[i, Constants.QS.QsColNumberSubTotal].ToString()) ? 0 : Convert.ToInt32(objAry[i, Constants.QS.QsColNumberSubTotal]);

                qs.SubTotals = new List<QuestionSettings.SubTotal>();
                for (int j = 0, k = Constants.QS.QsColNumberSubTotal + 1; j < qs.SubTotalCount; j++, k++)
                {
                    string subTotal = null == objAry[i, k] ? "" : objAry[i, k].ToString();
                    k++;
                    string criterion = null == objAry[i, k] ? "" : objAry[i, k].ToString();
                    qs.SubTotals.Add(new QuestionSettings.SubTotal(subTotal, criterion));
                }

                dict.Add(qs.Variable, qs);
            }
            
            if (!sheetOnly)
            {
                int key = 1;
                foreach (QuestionSettings qs in questions.Values)
                {
                    dict.Add("@#DEL" + key.ToString("0000"), qs);
                    key++;
                }
            }
            return dict;
        }

        private static Dictionary<int, QuestionSettings> GetQSFromDb(Excel.Workbook workbook, int[] ids)
        {
            if (null == ids && ids.GetLength(0) <= 0)
            {
                return null;
            }
            Dictionary<int, QuestionSettings> dict = new Dictionary<int, QuestionSettings>();
            string connectionString = DB.DBHelper.GetConnectionString(workbook);
         

            DataTable dt = new DataTable();
            using (SQLiteConnection connection = DBHelper.GetConnection(connectionString))
            {
                connection.Open();
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT id, variable, answer_type, category_count, question_flag FROM question ");

                int max = ids.GetLength(0);
          

                using (SQLiteCommand command = connection.CreateCommand())
                {
                    command.CommandText = sql.ToString();
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dict.Add(Convert.ToInt32(dt.Rows[i][0])
                        , new QuestionSettings(Convert.ToInt32(dt.Rows[i][0]), dt.Rows[i][1].ToString()
                        , dt.Rows[i][2].ToString(), Convert.ToInt32(dt.Rows[i][3]), dt.Rows[i][4].ToString()));
                }
            }
         
            return dict;
        }


        public static Questions GetQuestions(Excel.Workbook workbook)
        {
      
            try
            {
                Questions questions = new Questions();

                DB.DataOutput doDao = new DB.DataOutput(workbook);
                var mapDict = doDao.GetVariableMappingDictionary();

                Questions.Question prevParent = new Questions.Question();
                Excel.Worksheet QuestionSettingSheet = ExcelUtil.GetWorkSheetByCodeName(workbook, Constants.SheetCodeName.QuestionSetting);
                Excel.Range vStart = QuestionSettingSheet.Cells[Constant.QS.QsRowDataStart, Constant.QS.QsColItem];
                Excel.Range vEnd = ExcelUtil.EndxlUp(vStart);
                questions.SurveyTitle = QuestionSettingSheet.Cells[2, 12].Text;
                Excel.Range tRng = QuestionSettingSheet.Cells[Constant.QS.QsRowDataStart, 1];
                tRng = tRng.Resize[vEnd.Row - Constant.QS.QsRowDataStart + 1, Constant.QS.QsColcriteria10];
                Object[,] obj = tRng.Value2;

                bool faParent = false;
                string faText = "";

                int order = 1;
                int max = obj.GetLength(0);
                for (int j = 1; j <= max; j++)
                {
                    SetQuestion(obj, mapDict, ref j, ref questions, ref order, ref faParent, ref faText);
                }
              
                return questions;
               
            }
            catch (Exception ex)
            {
				Console.WriteLine(ex.Message);
                return null;
            }
            
        }
        public static void SetQuestion(Object[,] obj, Dictionary<string, Question> mapDict, ref int index, ref Questions questions, ref int questionIndex, ref bool faParent, ref string faText, bool matrixChildFlag = false)
        {
           
            Questions.Question question = new Questions.Question();
            question.Name = obj[index, Constant.QS.QsColItem] == null ? "" : obj[index, Constant.QS.QsColItem].ToString();

            if (mapDict.ContainsKey(question.Name))
            {
                if (mapDict[question.Name].Id == 0)
                {
                    question.Index = 0;
                    question.ID = 0;
                    question.TopTableName = "answers";
                    question.ColumnName = "sample_id";
                }
                else
                {
                    question.Index = questionIndex;
                    question.ID = mapDict[question.Name].Id;
                    question.TopTableName = "answers";
                    question.ColumnName = "q_" + question.ID;
                }
            }
            else
            {
                return;
            }

            switch (obj[index, Constant.QS.QsColAnswerType].ToString())
            {
                case Constant.AnswerType.D:
                    question.QuestionType = Macromill.QCWeb.Tabulation.QuestionType.FA;
                    question.QCAnswerType = QCAnswerType.D;
                    break;
                case Constant.AnswerType.MA:
                    question.QuestionType = Macromill.QCWeb.Tabulation.QuestionType.MA;
                    question.QCAnswerType = QCAnswerType.MA;
                    break;
                case Constant.AnswerType.SA:
                    question.QuestionType = Macromill.QCWeb.Tabulation.QuestionType.SA;
                    question.QCAnswerType = QCAnswerType.SA;
                    break;
                case Constant.AnswerType.N:
                    question.QuestionType = Macromill.QCWeb.Tabulation.QuestionType.N;
                    question.QCAnswerType = QCAnswerType.N;
                    break;
                default:
                    question.QuestionType = Macromill.QCWeb.Tabulation.QuestionType.FA;
                    question.QCAnswerType = QCAnswerType.FA;
                    break;
            }
            if (matrixChildFlag)
            {
                question.QuestionType |= Macromill.QCWeb.Tabulation.QuestionType.MatrixChild;
            }

            switch (null == obj[index, Constant.QS.QsColQuestiontype] ? "" : obj[index, Constant.QS.QsColQuestiontype].ToString())
            {
                case Constant.QuestionType.FAL:
                    question.QCQuestionType = QCQuestionType.FAL;
                    break;
                case Constant.QuestionType.FAS:
                    question.QCQuestionType = QCQuestionType.FAS;
                    break;
                case Constant.QuestionType.MAC:
                    question.QCQuestionType = QCQuestionType.MAC;
                    break;
                case Constant.QuestionType.MTM:
                    question.QCQuestionType = QCQuestionType.MTM;
                    break;
                case Constant.QuestionType.MTS:
                    question.QCQuestionType = QCQuestionType.MTS;
                    break;
                case Constant.QuestionType.MTT:
                    question.QCQuestionType = QCQuestionType.MTT;
                    break;
                case Constant.QuestionType.RAT:
                    question.QCQuestionType = QCQuestionType.RAT;
                    break;
                case Constant.QuestionType.RNK:
                    question.QCQuestionType = QCQuestionType.RNK;
                    break;
                case Constant.QuestionType.SAP:
                    question.QCQuestionType = QCQuestionType.SAP;
                    break;
                case Constant.QuestionType.SAR:
                    question.QCQuestionType = QCQuestionType.SAR;
                    break;
                case Constant.QuestionType.SAS:
                    question.QCQuestionType = QCQuestionType.SAS;
                    break;
                default:
                    question.QCQuestionType = QCQuestionType.None;
                    break;
            }

            int cCount = obj[index, Constant.QS.QsColCategories] == null || String.IsNullOrEmpty(obj[index, Constant.QS.QsColCategories].ToString()) ? 0 : Convert.ToInt32(obj[index, 8]);
            Sectors sec = new Sectors(question);
            for (int k = 0; k < cCount; k++)
            {
                string choice = null == obj[index, Constant.QS.QsColChoiceBegin + k] ? "" : obj[index, Constant.QS.QsColChoiceBegin + k].ToString();
                sec.Add(k + 1, choice);
            }
            question.Sectors = sec;

            question.Number = null == obj[index, Constant.QS.QsColQuestionNumber] ? "" : obj[index, Constant.QS.QsColQuestionNumber].ToString();

            if (null != question.Number && question.QCQuestionType != QCQuestionType.None && !matrixChildFlag)
            {
                if (obj[index, Constant.QS.QsColQuestiontype].ToString() == Constant.QuestionType.FAS || obj[index, Constant.QS.QsColQuestiontype].ToString() == Constant.QuestionType.FAL)
                {
                    faParent = true;
                    faText = question.Name;
                }
                if (null != obj[index, Constant.QS.QsColNumberOfQuestion] && !string.IsNullOrEmpty(obj[index, Constant.QS.QsColNumberOfQuestion].ToString()))
                {
                    int matrixChildCount = Convert.ToInt32(obj[index, Constant.QS.QsColNumberOfQuestion]);
                    if (matrixChildCount > 0)
                    {
                        question.QuestionType = question.QuestionType | Macromill.QCWeb.Tabulation.QuestionType.MatrixParent;
                        question.childquestions = new Questions(question);
                        question.ID += 32000; //TODO
                        Questions.Question matParent = question;
                        int childCount = 1;
                        for (int i = 1; i <= matrixChildCount; i++)
                        {
                            SetQuestion(obj, mapDict, ref index, ref question.childquestions, ref childCount, ref faParent, ref faText, true);
                            index++;
                        }
                        index--;
                    }
                }
            }
            else if (faParent)
            {
                if (question.Name.StartsWith(faText))
                {
                    question.QuestionType = question.QuestionType | Macromill.QCWeb.Tabulation.QuestionType.FA_Sub;
                }
                else
                {
                    faParent = false;
                }
            }
            question.Description = MakeDescription(Convert.ToString(obj[index, Constant.QS.QsColTableHeading]), Convert.ToString(obj[index, Constant.QS.QsColQuestion]));
            question.ParentCollection = questions;
            questionIndex++;
            questions.Add(question.ID.ToString(), question);
     
        }
        private static string MakeDescription(string lv1title, string lv2title)
        {
      
            string lv1 = string.IsNullOrEmpty(lv1title) ? "" : lv1title;
            string lv2 = string.IsNullOrEmpty(lv2title) ? "" : lv2title;

            StringBuilder description = new StringBuilder(lv2);
            _log.Info("end MakeDescriptionfunction ---");
            return description.ToString();
        }

        public static string[,] Dictionary2Array(Dictionary<string, string> dic)
        {
           
            string[,] arr = new string[dic.Count, 2];
            int i = 0;

            foreach (KeyValuePair<string, string> item in dic)
            {
                arr[i, 0] = item.Key;
                arr[i, 1] = item.Value;
                i++;
            }

            return arr;
        }

    }
}
