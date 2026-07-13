using ExcelAddIn.Sheets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using Constant = ExcelAddIn.Common.Constants;
using Macromill.QCWeb.Question;
using ExcelAddIn.Common;
using QC4Common.Model;

namespace ExcelAddIn.Util
{
	class DictUpdate
	{
		public static void SetQuestion(Object[,] obj, Dictionary<string, Model.Question> mapDict, ref int index, ref Questions questions, ref int questionIndex, ref bool faParent, ref string faText, bool matrixChildFlag = false)
		{
			Questions.Question question = new Questions.Question();
			question.Name = obj[index, Constant.QS.QsColItem] == null ? "" : obj[index, Constant.QS.QsColItem].ToString();
			if (mapDict.ContainsKey(question.Name))
			{
				question.Index = questionIndex;
				question.ID = mapDict[question.Name].Id;
				question.TopTableName = "answers";
				question.ColumnName = "q_" + question.ID;
			}
			else if (index == 1)
			{
				question.Index = 0;
				question.ID = 0;
				question.TopTableName = "answers";
				question.ColumnName = "sample_id";
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

			int cCount = null == obj[index, Constant.QS.QsColCategories] ? 0 : Convert.ToInt32(obj[index, 8]);
			Sectors sec = new Sectors(question);
			for (int k = 0; k < cCount; k++)
			{
				string choice = null == obj[index, Constant.QS.QsColChoiceBegin + k] ? "" : obj[index, Constant.QS.QsColChoiceBegin + k].ToString();
				sec.Add(k + 1, choice);
			}
			question.Sectors = sec;

			question.Number = obj[index, Constant.QS.QsColQuestionNumber]?.ToString();

			if (null != question.Number && question.QCQuestionType != QCQuestionType.None && !matrixChildFlag)
			{
				if (obj[index, Constant.QS.QsColQuestiontype].ToString() == Constant.QuestionType.FAS || obj[index, Constant.QS.QsColQuestiontype].ToString() == Constant.QuestionType.FAL)
				{
					faParent = true;
					faText = question.Name;
				}
				if (null != obj[index, Constant.QS.QsColNumberOfQuestion])
				{
					int matrixChildCount = Convert.ToInt32(obj[index, Constant.QS.QsColNumberOfQuestion]);
					if (matrixChildCount > 0)
					{
						question.QuestionType = question.QuestionType | Macromill.QCWeb.Tabulation.QuestionType.MatrixParent;
						question.childquestions = new Questions(question);
						question.ID += 1000; //TODO
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

		public static Questions GetQuestions(Excel.Workbook workbook)
		{
			try
			{
				Questions questions = new Questions();
				{
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
				}
				return questions;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}
		}

		private static string MakeDescription(string lv1title, string lv2title)
		{
			string lv1 = string.IsNullOrEmpty(lv1title) ? "" : lv1title;
			string lv2 = string.IsNullOrEmpty(lv2title) ? "" : lv2title;

			StringBuilder description = new StringBuilder(lv2);
			//for (int i = 0; i < 1000 - lv1.Length; ++i)
			//{
			//    description.Append((char)1);
			//}
			//description.Append(lv2);
			//for (int i = 0; i < 1000 - lv2.Length; ++i)
			//{
			//    description.Append((char)1);
			//}
			return description.ToString();
		}


		public static Dictionary<string, QuestionSettings> PopulateVariableDictionary(Excel.Workbook workbook)
		{
			if (null == workbook)
			{
				return null;
			}

			Dictionary<string, QuestionSettings> dict = new Dictionary<string, QuestionSettings>();

			DB.DataOutput doDao = new DB.DataOutput(workbook);
			var mapDict = doDao.GetVariableMappingDict();

			//Definiotion.VariableDictionary.Clear();
			Excel.Worksheet QuestionSettingSheet = ExcelUtil.GetWorkSheetByCodeName(workbook, Constants.SheetCodeName.QuestionSetting);
			Excel.Range vStart = QuestionSettingSheet.Cells[Constant.QS.QsRowDataStart, Constant.QS.QsColItem];
			Excel.Range vEnd = ExcelUtil.EndxlUp(vStart);

			Excel.Range tRng = QuestionSettingSheet.Cells[Constant.QS.QsRowDataStart, 1];
			tRng = tRng.Resize[vEnd.Row - Constant.QS.QsRowDataStart + 1, Constant.QS.QsColcriteria10];
			Object[,] obj = tRng.Value2;

			int max = obj.GetLength(0);
			for (int j = 1; j <= max; j++)
			{
				QuestionSettings qs = new QuestionSettings();
				qs.QuestionNumber = null == obj[j, Constant.QS.QsColQuestionNumber] ? "" : obj[j, Constant.QS.QsColQuestionNumber].ToString();
				qs.QuestionType = null == obj[j, Constant.QS.QsColQuestiontype] ? "" : obj[j, Constant.QS.QsColQuestiontype].ToString();
				if (obj[j, Constant.QS.QsColNumberOfQuestion] != null && !String.IsNullOrEmpty(obj[j, Constant.QS.QsColNumberOfQuestion].ToString()) && String.IsNullOrWhiteSpace(obj[j, Constant.QS.QsColNumberOfQuestion].ToString()))
				{
					qs.QuestionCount = Convert.ToInt32(obj[j, Constant.QS.QsColNumberOfQuestion]);
				}
				qs.Variable = null == obj[j, Constant.QS.QsColItem] ? "" : obj[j, Constant.QS.QsColItem].ToString();
				if (mapDict.ContainsKey(qs.Variable))
				{
					var q = mapDict[qs.Variable];
					qs.Id = q.Id;
					qs.VariableBefore = q.Variable;
				}

				qs.AnswerType = null == obj[j, Constant.QS.QsColAnswerType] ? "" : obj[j, Constant.QS.QsColAnswerType].ToString();

				if (obj[j, Constant.QS.QsColCategories] != null && !String.IsNullOrEmpty(obj[j, Constant.QS.QsColCategories].ToString()) && !String.IsNullOrWhiteSpace(obj[j, Constant.QS.QsColCategories].ToString()))
				{
					qs.CategoryCount = Convert.ToInt32(obj[j, Constant.QS.QsColCategories]);
				}
				//else
				//{
				//	qs.CategoryCount = 0;
				//}
				//qs.CategoryCount = null == obj[j, Constant.QS.QsColCategories] ? 0 : Convert.ToInt32(obj[j, 8]);

				qs.Score = null == obj[j, Constant.QS.QsColWT] ? "" : obj[j, Constant.QS.QsColWT].ToString();
				qs.TableHeading = null == obj[j, Constant.QS.QsColTableHeading] ? "" : obj[j, Constant.QS.QsColTableHeading].ToString();
				qs.Question = null == obj[j, Constant.QS.QsColQuestion] ? "" : obj[j, Constant.QS.QsColQuestion].ToString();
				for (int k = 0; k < qs.CategoryCount; k++)
				{
					string choice = null == obj[j, Constant.QS.QsColChoiceBegin + k] ? "" : obj[j, Constant.QS.QsColChoiceBegin + k].ToString();
					qs.Choices.Add(choice);
				}
				qs.Count = null == obj[j, Constant.QS.QsColCount] ? "" : obj[j, Constant.QS.QsColCount].ToString();
				qs.CountBase = null == obj[j, Constant.QS.QsColCountBase] ? "" : obj[j, Constant.QS.QsColCountBase].ToString();


				//qs.SubTotalCount = null == obj[j, Constant.QS.QsColNumberSubTotal] ? 0 : Convert.ToInt32(obj[j, Constant.QS.QsColNumberSubTotal]);
				if (obj[j, Constant.QS.QsColNumberSubTotal] != null && !String.IsNullOrEmpty(obj[j, Constant.QS.QsColNumberSubTotal].ToString()) && !String.IsNullOrWhiteSpace(obj[j, Constant.QS.QsColNumberSubTotal].ToString()))
				{
					qs.SubTotalCount = Convert.ToInt32(obj[j, Constant.QS.QsColNumberSubTotal]);
				}
				else
				{
					qs.SubTotalCount = 0;
				}
				qs.SubTotals = new List<QuestionSettings.SubTotal>();
				for (int i = 0, k = Constant.QS.QsColNumberSubTotal + 1; i < qs.SubTotalCount; i++, k++)
				{
					string subTotal = null == obj[i, k] ? "" : obj[i, k].ToString();
					string criterion = null == obj[i, ++k] ? "" : obj[i, ++k].ToString();
					qs.SubTotals.Add(new QuestionSettings.SubTotal(subTotal, criterion));
				}
				dict.Add(qs.Variable, qs);
			}

			Excel.Names xx = QuestionSettingSheet.Names;
			foreach (Excel.Name name in xx)
			{
				string x = name.Name;
				string var = name.RefersToRange.Cells[1, Constants.QS.QsColItem].Text;
				if (dict.ContainsKey(var))
				{
					dict[var].RowName = x;
				}
			}

			return dict;
		}
	}
}
