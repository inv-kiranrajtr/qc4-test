using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using ExcelCommon = QC4Common.Util.ExcelUtil;

namespace QC4Common.Common
{
	class QSValidate
	{
		const string CountBaseValue = "Lower";
		int i;
		int rowNum;
		Object[,] objAry;
		ReturnClass rc;
		Worksheet questionSettingSheet;

		internal QSValidate(Workbook workbook)
		{
			//questionSettingSheet = ExcelUtil.GetWorksheetByIndex(1);
			questionSettingSheet = ExcelUtilForAddIn.GetWorkSheetByCodeName(workbook, Constants.SheetCodeName.QuestionSetting);

			rowNum = 1;
		}

		public ReturnClass QuestionConfigurationCheck()
		{
			if (null == questionSettingSheet)
			{
				return null;
			}

			if (string.IsNullOrEmpty(questionSettingSheet.Range["L2"].Text))
			{
				return new ReturnClass(false, GetRange(2, 12), CommonResource.QS_ALERT_QTITLE_NOT_EMPTY);
			}

			Range qsRange = questionSettingSheet.Range[Constants.QS.QsStartAddress];
			qsRange = ExcelUtilForAddIn.EndxlUp(qsRange, true);
			int lastRow = 4;

			try
			{
				Range lRow = questionSettingSheet.Cells.Find(What: "*",
								After: questionSettingSheet.Range["A1"],
								LookAt: XlLookAt.xlPart,
								LookIn: XlFindLookIn.xlFormulas,
								SearchOrder: XlSearchOrder.xlByRows,
								SearchDirection: XlSearchDirection.xlPrevious,
								MatchCase: false);
				lastRow = lRow.Row;
			}
			catch { }

			qsRange = questionSettingSheet.get_Range(questionSettingSheet.Range[Constants.QS.QsFirstCell], GetRange(lastRow, Constants.QS.QsColcriteria10));
			objAry = qsRange.Value2;
			int maxRow = objAry.GetLength(0);

			List<string> varList = new List<string>();
			rowNum = Constants.QS.QsRowDataStart;

			for (i = 1; i <= maxRow; i++)
			{
				rc = ValidateQuestionNumber(objAry[i, Constants.QS.QsColQuestionNumber], ref varList);
				if (!rc.Result)
				{
					return rc;
				}

				rc = ValidateVariable(objAry[i, Constants.QS.QsColItem], out string variable, ref varList);
				if (!rc.Result)
				{
					return rc;
				}

				rc = ValidateQuestionType(objAry[i, Constants.QS.QsColQuestiontype], out string questionType);
				if (!rc.Result)
				{
					return rc;
				}

				rc = ValidateAnswerType(objAry[i, Constants.QS.QsColAnswerType], out string answertType, questionType, variable, rowNum);
				if (!rc.Result)
				{
					return rc;
				}

				rc = ValidateCategoryCount(objAry[i, Constants.QS.QsColCategories], out int catergoryCount, answertType, variable);
				if (!rc.Result)
				{
					return rc;
				}

				rc = ValidateCount(objAry[i, Constants.QS.QsColCount], catergoryCount, answertType, Constants.QS.QsColCount, out string countValue);
				if (!rc.Result)
				{
					return rc;
				}

				rc = ValidateCountBase(objAry[i, Constants.QS.QsColCountBase], answertType, Constants.QS.QsColCountBase, countValue);
				if (!rc.Result)
				{
					return rc;
				}

				rc = ValidateSubTotal(objAry[i, Constants.QS.QsColAddSunTotal], out int addSubTotal, answertType, 1, Constants.QS.QsColAddSunTotal, 0);
				if (!rc.Result)
				{
					return rc;
				}

				rc = ValidateSubTotal(objAry[i, Constants.QS.QsColNumberSubTotal], out int subTotal, answertType, 10, Constants.QS.QsColNumberSubTotal, addSubTotal);
				if (!rc.Result)
				{
					return rc;
				}

				rc = ValidateSubtotalAndCondition(subTotal, catergoryCount);
				if (!rc.Result)
				{
					return rc;
				}

				rc = ValidateQuestionCount(objAry[i, Constants.QS.QsColNumberOfQuestion], out int questionCont, questionType, catergoryCount, variable, subTotal, answertType);
				if (!rc.Result)
				{
					return rc;
				}

				rc = ValidateWT(objAry[i, Constants.QS.QsColWT], catergoryCount, variable); if (!rc.Result)
				{
					return rc;
				}

				rc = ValidateSortTab(objAry[i, Constants.QS.QsColSortDisplay], catergoryCount);
				if (!rc.Result)
				{
					return rc;
				}
				rowNum++;
			}
			return new ReturnClass(true);
		}
		private ReturnClass ValidateQuestionNumber(object obj, ref List<string> varList)
		{
			string val = String.Empty;
			if (null != obj)
			{
				val = obj.ToString();
			}
			if (val.Length >= 1)
			{
				if (!QC4Common.Util.QSUtil.ValidatedQuestionNumber(val, out string msg))
				{
					return new ReturnClass(false, GetRange(rowNum, Constants.QS.QsColQuestionNumber), msg, new string[] { rowNum.ToString() });
				}
			}
			return new ReturnClass(true);
		}

		private ReturnClass ValidateVariable(object obj, out string variable, ref List<string> varList)
		{
			if (IsNullOrEmpty(obj, out variable))
			{
				return new ReturnClass(false, GetRange(rowNum, Constants.QS.QsColItem), CommonResource.QS_VARIABLE_NULL_NOT_ALLOWED, new string[] { rowNum.ToString() });
			}

			string item = variable;
			if (varList.Any(s => s.Equals(item, StringComparison.OrdinalIgnoreCase))) // TODO case check
			{
				return new ReturnClass(false, GetRange(rowNum, Constants.QS.QsColItem), CommonResource.QS_VARIABLE_DUPLICATED, new string[] { rowNum.ToString() });
			}
			string val = String.Empty;
			if (null != obj)
			{
				val = obj.ToString();
			}

			if (!QC4Common.Util.QSUtil.ValidateVariable(val, out string msg))
			{
				return new ReturnClass(false, GetRange(rowNum, Constants.QS.QsColItem), msg, new string[] { rowNum.ToString() });
			}


			varList.Add(item);
			return new ReturnClass(true);
		}

		private ReturnClass ValidateQuestionNumber(Range targetCell)
		{
			return new ReturnClass(true);
		}

		private ReturnClass ValidateQuestionType(Object obj, out string qType)
		{
			if (IsNullOrEmpty(obj, out qType))
			{
				return new ReturnClass(true);
			}

			if (!(Constants.QuestionType.FAL.Equals(qType)
				|| Constants.QuestionType.FAS.Equals(qType)
				|| Constants.QuestionType.MAC.Equals(qType)
				|| Constants.QuestionType.MTM.Equals(qType)
				|| Constants.QuestionType.MTS.Equals(qType)
				|| Constants.QuestionType.MTT.Equals(qType)
				|| Constants.QuestionType.RAT.Equals(qType)
				|| Constants.QuestionType.RNK.Equals(qType)
				|| Constants.QuestionType.SAP.Equals(qType)
				|| Constants.QuestionType.SAR.Equals(qType)
				|| Constants.QuestionType.SAS.Equals(qType)))
			{
				return new ReturnClass(false, GetRange(rowNum, Constants.QS.QsColQuestiontype), CommonResource.QS_ALERT_INVALID_QUESTION_TYPE, new string[] { rowNum.ToString() });
			}
			return new ReturnClass(true);
		}

		//validating questioncount and matixtype validation
		private ReturnClass ValidateQuestionCount(object obj, out int questionCount, string questionType, int categoryCount, string variable, int noOfSubTotal, string answerType)
		{
			questionCount = 0;
			if (IsNullOrEmpty(obj, out string qNumber))
			{
				return new ReturnClass(true);
			}

			if (!Int32.TryParse(qNumber, out int count) || count > 200 || count < 1)
			{
				return new ReturnClass(false, GetRange(rowNum, Constants.QS.QsColNumberOfQuestion), String.Format(CommonResource.SET_INETGER_BETWEEN, 1, 200), new string[] { rowNum.ToString(), questionType });
			}

			if (Constants.QuestionType.SAP.Equals(questionType)
				|| Constants.QuestionType.SAS.Equals(questionType)
				|| Constants.QuestionType.SAR.Equals(questionType)
				|| Constants.QuestionType.MAC.Equals(questionType))
			{
				return count == 1 ? new ReturnClass(true) : new ReturnClass(false, GetRange(rowNum, Constants.QS.QsColNumberOfQuestion), CommonResource.QS_QUESTION_COUNT_SHOULD_EMPTY, new string[] { rowNum.ToString(), questionType });
			}
			if (!Int32.TryParse(qNumber, out questionCount))
			{
				return new ReturnClass(false, GetRange(rowNum, Constants.QS.QsColNumberOfQuestion), CommonResource.QS_QUESTION_COUNT_INTEGER, new String[] { rowNum.ToString() });
			}
			if (questionCount < 0)
			{
				return new ReturnClass(false, GetRange(rowNum, Constants.QS.QsColNumberOfQuestion), CommonResource.QS_QUESTION_COUNT_NEGATIVE, new String[] { rowNum.ToString() });
			}

			if (Constants.QuestionType.MTS.Equals(questionType)
				|| Constants.QuestionType.MTT.Equals(questionType)
				|| Constants.QuestionType.RNK.Equals(questionType)
				|| Constants.QuestionType.MTM.Equals(questionType))
			{
				if (questionCount > 200)
				{
					return new ReturnClass(false, GetRange(rowNum, Constants.QS.QsColNumberOfQuestion), CommonResource.QS_QUESTION_COUNT_MORETHAN_201, new string[] { rowNum.ToString(), questionType });
				}
			}

			noOfSubTotal *= 2;
			int startCol = Constants.QS.QsColCount;
			if (answerType == Constants.AnswerType.SA)
			{
				noOfSubTotal += 2;
				startCol = Constants.QS.QsColAddSunTotal;
			}
			else if (answerType == Constants.AnswerType.MA)
			{
				noOfSubTotal += 4;
			}

			for (int j = 1; j < questionCount; j++)
			{
				try
				{
					Object categoryObj = objAry[i + j, Constants.QS.QsColCategories];
					int c = 0;
					if (!IsNullOrEmpty(categoryObj, out string str) && !Int32.TryParse(str, out c))
					{
						return new ReturnClass(false, GetRange(rowNum + j, Constants.QS.QsColCategories), CommonResource.QS_SET_INTEGER, new string[] { "1", "1000", (rowNum + j).ToString(), (Constants.QS.QsColCategories).ToString() });
					}

					if (!IsNullOrEmpty(objAry[i + j, Constants.QS.QsColQuestionNumber], out string s)
						|| !IsNullOrEmpty(objAry[i + j, Constants.QS.QsColQuestiontype], out s)
						|| !IsNullOrEmpty(objAry[i + j, Constants.QS.QsColNumberOfQuestion], out s))
					{
						return new ReturnClass(false, GetRange(rowNum + j, Constants.QS.QsColQuestionNumber), CommonResource.QS_MATRIX_QUESTION_LESS, new string[] { variable, (rowNum + j).ToString() });
					}

					if (questionType.Equals(Constants.QuestionType.FAS))
					{
						rc = ValidateAnswerType(objAry[i + j, Constants.QS.QsColAnswerType], out string at, questionType, variable, rowNum + j);
						if (!rc.Result)
						{
							return rc;
						}
					}
					else if (!answerType.Equals(Convert.ToString(objAry[i + j, Constants.QS.QsColAnswerType])))//String.IsNullOrEmpty(Convert.ToString(objAry[i + j, Constants.QS.QsColAnswerType])) || answerType != Convert.To)
					{
						return new ReturnClass(false, GetRange(rowNum + j, Constants.QS.QsColAnswerType), CommonResource.QS_INVALID_ANSWER_TYPE, new string[] { (rowNum + j).ToString(), variable });
					}

					if (c != categoryCount)
					{
						return new ReturnClass(false, GetRange(rowNum + j, Constants.QS.QsColCategories), CommonResource.QS_MATRIX_CHOICE_COUNT_NOT_EQUAL, new string[] { (rowNum + j).ToString() });
					}

					string str1;
					string str2;
					if (Convert.ToString(objAry[i + j, Constants.QS.QsColWT]) != Convert.ToString(objAry[i + j - 1, Constants.QS.QsColWT]))
					{
						return new ReturnClass(false, GetRange(rowNum + j, Constants.QS.QsColWT), CommonResource.QS_MATRIX_WT_NOT_EQUAL, new string[] { (rowNum + j).ToString(), (Constants.QS.QsColWT).ToString() });
					}

					if (Convert.ToString(objAry[i + j, Constants.QS.QsColSortDisplay]) != Convert.ToString(objAry[i + j - 1, Constants.QS.QsColSortDisplay]))
					{
						return new ReturnClass(false, GetRange(rowNum + j, Constants.QS.QsColSortDisplay), CommonResource.QS_MATRIX_SORT_NOT_EQUAL, new string[] { (rowNum + j).ToString(), (Constants.QS.QsColSortDisplay).ToString() });
					}

					for (int k = 0; k < noOfSubTotal; k++)
					{
						IsNullOrEmpty(objAry[i + j, startCol + k], out str1);
						IsNullOrEmpty(objAry[i + j - 1, startCol + k], out str2);

						if (!str1.Equals(str2))
						{
							string message = "";
							if (startCol + k == Constants.QS.QsColCount)
							{
								message = CommonResource.QS_ERROR_IN_MATRIX_SUBTOTAL_COUNT;
							}
							else if (startCol + k == Constants.QS.QsColCountBase)
							{
								message = CommonResource.QS_ERROR_IN_MATRIX_SUBTOTAL_COUNTBASE;
							}
							else if (startCol + k == Constants.QS.QsColAddSunTotal)
							{
								message = CommonResource.QS_ERROR_IN_MATRIX_SUBTOTAL_ADDSUB;
							}
							else if (startCol + k == Constants.QS.QsColNumberSubTotal)
							{
								message = CommonResource.QS_ERROR_IN_MATRIX_SUBTOTAL_ADDSUBCOUNT;
							}
							else
							{
								message = CommonResource.QS_ERROR_IN_MATRIX_SUBTOTAL;
							}
							return new ReturnClass(false, GetRange(rowNum + j, startCol + k), message, new string[] { (rowNum + j).ToString(), (startCol + k).ToString() });
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					return new ReturnClass(false, GetRange(rowNum, Constants.QS.QsColNumberOfQuestion), CommonResource.QS_ERROR_IN_MATRIX_QUESTION, new string[] { rowNum.ToString(), Constants.QS.QsColNumberOfQuestion.ToString() });
				}
			}
			return new ReturnClass(true);
		}

		private ReturnClass ValidateAnswerType(Object obj, out string answerType, string questionType, string variable, int row)
		{

			if (IsNullOrEmpty(obj, out answerType))
			{
				return new ReturnClass(false, GetRange(rowNum, Constants.QS.QsColAnswerType), CommonResource.QS_NULL_ANSWER_TYPE, new string[] { rowNum.ToString(), variable });
			}

			switch (answerType)
			{
				case Constants.AnswerType.SA:
					if (Constants.QuestionType.SAR.Equals(questionType)
						|| Constants.QuestionType.SAP.Equals(questionType)
						|| Constants.QuestionType.MTS.Equals(questionType)
						|| Constants.QuestionType.MTT.Equals(questionType)
						|| Constants.QuestionType.RNK.Equals(questionType)
						|| Constants.QuestionType.MTM.Equals(questionType)
						|| Constants.QuestionType.SAS.Equals(questionType))
					{
						return new ReturnClass(true);
					}
					break;
				case Constants.AnswerType.MA:
					if (Constants.QuestionType.MAC.Equals(questionType)
						|| Constants.QuestionType.MTM.Equals(questionType))
					{
						return new ReturnClass(true);
					}
					break;
				case Constants.AnswerType.FA:
					if (Constants.QuestionType.FAS.Equals(questionType)
						|| Constants.QuestionType.FAL.Equals(questionType))
					{
						return new ReturnClass(true);
					}
					break;
				case Constants.AnswerType.N:
					if (Constants.QuestionType.FAS.Equals(questionType)
						|| Constants.QuestionType.RAT.Equals(questionType))
					{
						return new ReturnClass(true);
					}
					break;
				case Constants.AnswerType.D:
					if (String.Empty.Equals(questionType))
					{
						return new ReturnClass(true);
					}
					break;
				default:
					if (answerType != Constants.AnswerType.D || answerType != Constants.AnswerType.N || answerType != Constants.AnswerType.FA || answerType != Constants.AnswerType.MA || answerType != Constants.AnswerType.SA)
					{
						return new ReturnClass(false, GetRange(row, Constants.QS.QsColAnswerType), CommonResource.QS_INVALID_ANSWER_TYPE, new string[] { rowNum.ToString(), variable });
					}
					else
					{
						return new ReturnClass(false, GetRange(row, Constants.QS.QsColAnswerType), CommonResource.QS_RAWDATA_ANSWER_TYPE_NOT_MATCH, new string[] { rowNum.ToString(), variable, answerType, Definitions.VariableDictionary[variable].AnswerTypeBefore });
					}
					break;
			}

			if (String.IsNullOrEmpty(questionType))
			{
				return new ReturnClass(true);
			}
            try
            {
				return new ReturnClass(false, GetRange(row, Constants.QS.QsColAnswerType), CommonResource.QS_RAWDATA_ANSWER_TYPE_NOT_MATCH, new string[] { rowNum.ToString(), variable, answerType, Definitions.VariableDictionary[variable].AnswerTypeBefore });
			}
            catch
            {
				return new ReturnClass(false, GetRange(row, Constants.QS.QsColAnswerType), CommonResource.QS_INVALID_ANSWER_TYPE, new string[] { rowNum.ToString(), variable });
			}
			
			
		}

		private ReturnClass ValidateCategoryCount(Object obj, out int categoryCount, string answerType, string variable)
		{
			categoryCount = 0;
			string count;
			if (IsNullOrEmpty(obj, out count))
			{
				if (Constants.AnswerType.FA.Equals(answerType)
					|| Constants.AnswerType.N.Equals(answerType)
					|| Constants.AnswerType.D.Equals(answerType))
				{
					return CheckUnwantedChoice(categoryCount);
				}
			}
			else
			{
				if (Constants.AnswerType.SA.Equals(answerType)
					|| Constants.AnswerType.MA.Equals(answerType)
					//|| Constants.AnswerType.D.Equals(answerType)
					//|| Constants.AnswerType.FA.Equals(answerType)
					)
				{
					if (!Int32.TryParse(count, out categoryCount) || (categoryCount < 1 || categoryCount > 1000))
					{
						return new ReturnClass(false, GetRange(rowNum, Constants.QS.QsColCategories), CommonResource.QS_SET_INTEGER, new string[] { "1", "1000", rowNum.ToString(), Constants.QS.QsColCategories.ToString() });
					}
					return CheckUnwantedChoice(categoryCount);
				}
				else
				{
					return new ReturnClass(false, GetRange(rowNum, Constants.QS.QsColCategories), CommonResource.QS_NUMBER_OF_CHOICE_SET, new string[] { rowNum.ToString(), answerType, variable });
				}
			}
			return new ReturnClass(false, GetRange(rowNum, Constants.QS.QsColCategories), CommonResource.QS_NUMBER_OF_CHOICE_NOT_SET, new string[] { rowNum.ToString(), variable });
		}

		private ReturnClass CheckUnwantedChoice(int categoryCount)
		{
			Range choiceRange = questionSettingSheet.get_Range(Constants.QS.QsColCategoriesBegin + rowNum, Constants.QS.BufferColumn + rowNum);
			//to check more number of choices than no.of categories are there or not
			Range left = choiceRange.Cells[choiceRange.Cells.Count].End(XlDirection.xlToLeft);
			if (left.Column - 12 > categoryCount)
			{
				return new ReturnClass(false, left, CommonResource.QS_CHOICE_VALUE_MORETHAN_COUNT, new string[] { left.Row.ToString(), left.Column.ToString() });
			}
			return new ReturnClass(true);
		}

		private ReturnClass ValidateWT(Object obj, int categoryCount, string variable)
		{
			if (IsNullOrEmpty(obj, out string str))
			{
				return new ReturnClass(true);
			}
			string[] s = str.Split(',');
			if (s.Count() != categoryCount)
			{
				return new ReturnClass(false, GetRange(rowNum, Constants.QS.QsColWT), CommonResource.QS_ALERT_WT, new string[] { rowNum.ToString(), variable });
			}

			for (int i = 0; i < categoryCount; i++)
			{
				if (!string.IsNullOrEmpty(s[i]))
				{
					if (!Double.TryParse(s[i], out double n))
					{
						return new ReturnClass(false, GetRange(rowNum, Constants.QS.QsColCategories), CommonResource.QS_NUMBER_OF_CHOICE_NOT_SET, new string[] { rowNum.ToString(), variable });
					}
				}
			}
			return new ReturnClass(true);
		}

		private ReturnClass ValidateSortTab(Object obj, int categoryCount)
		{
			int sortVal = 0;

			if (IsNullOrEmpty(obj, out string str))
			{
				return new ReturnClass(true);
			}

			if (categoryCount == 0)
			{
				return new ReturnClass(false, GetRange(rowNum, Constants.QS.QsColSortDisplay), CommonResource.QS_ALERT_INVALID_VALUE, new string[] { rowNum.ToString(), Constants.QS.QsColSortDisplay.ToString() });
			}

			if (!Int32.TryParse(str, out sortVal))
			{
				return new ReturnClass(false, GetRange(rowNum, Constants.QS.QsColSortDisplay), CommonResource.SET_NUMERIC_VALUE_RANGE, new string[] { rowNum.ToString(), Constants.QS.QsColSortDisplay.ToString() });
			}
			if (sortVal != 0 && sortVal <= categoryCount)
			{
				return new ReturnClass(true);
			}
			return new ReturnClass(false, GetRange(rowNum, Constants.QS.QsColSortDisplay), CommonResource.QS_ALERT_INVALID_VALUE_NOT_BETWEEN, new string[] { categoryCount.ToString() });
		}

		private ReturnClass ValidateCount(Object obj, int catCount, string answerType, int column, out String count)
		{
			if (IsNullOrEmpty(obj, out count))
			{
				return new ReturnClass(true);
			}

			if (!Constants.AnswerType.MA.Equals(answerType))
			{
				return new ReturnClass(false, GetRange(rowNum, column), CommonResource.QS_NO_VALUE_REQUIRED, new string[] { rowNum.ToString(), column.ToString() });
			}

			if (!QC4Common.Validation.NumberCheck.NUmberCheckSubtotal(count, catCount))
			{
				rc.Result = false;
				rc.Msg = String.Format(CommonResource.SET_INETGER_BETWEEN, 1, catCount);
				rc.Value = GetRange(rowNum, column);
			}
			return rc;
		}

		private ReturnClass ValidateCountBase(Object obj, string answertype, int column, string countValue)
		{
			if (IsNullOrEmpty(obj, out string str))
			{
				return new ReturnClass(true);
			}

			if (!Constants.AnswerType.MA.Equals(answertype))
			{
				return new ReturnClass(false, GetRange(rowNum, column), CommonResource.QS_NO_VALUE_REQUIRED, new string[] { rowNum.ToString(), column.ToString() });
			}

			if (!CountBaseValue.Equals(str))
			{
				return new ReturnClass(false, GetRange(rowNum, column), CommonResource.QS_ALERT_INVALID_COUNTBASE_FLAG, new string[] { rowNum.ToString() });
			}
			return new ReturnClass(true);
		}

		private ReturnClass ValidateSubTotal(Object obj, out int subTotal, string answerType, int maxValue, int column, int addSubTotal)
		{
			subTotal = 0;

			if (IsNullOrEmpty(obj, out string str) && addSubTotal != 0)
			{
				return new ReturnClass(false, GetRange(rowNum, column), CommonResource.QS_ALERT_ADD_SUBTOTAL_COUNT_NOT_SET, new string[] { rowNum.ToString(), column.ToString() });
			}

			if (addSubTotal == 0 && String.IsNullOrEmpty(str))
			{
				return new ReturnClass(true);
			}

			if (!Constants.AnswerType.MA.Equals(answerType) && !Constants.AnswerType.SA.Equals(answerType))
			{
				return new ReturnClass(false, GetRange(rowNum, column), CommonResource.QS_NO_VALUE_REQUIRED, new string[] { rowNum.ToString(), column.ToString() });
			}

			if (Int32.TryParse(str, out subTotal))
			{
				if (maxValue >= subTotal && subTotal >= 1)
				{
					return new ReturnClass(true);
				}
			}
			return new ReturnClass(false, GetRange(rowNum, column), CommonResource.QS_ALERT_INVALID_VALUE, new string[] { rowNum.ToString(), column.ToString() });
		}

		private ReturnClass ValidateSubtotalAndCondition(int subTotalCount, int catCount)
		{
			Range range = GetRange(rowNum, Constants.QS.QsColSubtotal1).Resize[1, 20];
			range = range.Cells[range.Cells.Count].End(XlDirection.xlToLeft);
			if ((range.Column > Constants.QS.QsColNumberSubTotal + 2 * subTotalCount))
			{
				return new ReturnClass(false, range, CommonResource.QS_NO_VALUE_REQUIRED, new string[] { range.Row.ToString(), range.Column.ToString() });
			}

			for (int j = 0; j < subTotalCount; j++)
			{
				if (IsNullOrEmpty(objAry[i, Constants.QS.QsColSubtotal1 + j * 2], out string str))
				{
					return new ReturnClass(false, GetRange(rowNum, Constants.QS.QsColSubtotal1 + j * 2), CommonResource.QS_ALERT_ADD_SUBTOTAL_NAME_NOT_SET, new string[] { rowNum.ToString(), (Constants.QS.QsColSubtotal1 + j * 2).ToString() });
				}

				if (IsNullOrEmpty(objAry[i, Constants.QS.QsColcriteria1 + j * 2], out str))
				{
					return new ReturnClass(false, GetRange(rowNum, Constants.QS.QsColcriteria1 + j * 2), CommonResource.QS_ALERT_ADD_SUBTOTAL_CRITERIA_NOT_SET, new string[] { rowNum.ToString(), (Constants.QS.QsColSubtotal1 + j * 2).ToString() });
				}

				Object obj = objAry[i, Constants.QS.QsColcriteria1 + j * 2];
				if (!QC4Common.Validation.NumberCheck.NUmberCheckSubtotal(str, catCount))
				{
					rc.Result = false;
					rc.Msg = String.Format(CommonResource.SET_INETGER_BETWEEN, 1, catCount);
					rc.Value = GetRange(rowNum, Constants.QS.QsColcriteria1 + j * 2);
					return rc;
				}
			}
			return new ReturnClass(true);
		}

		private bool IsNullOrEmpty(Object obj, out string str)
		{
			str = String.Empty;
			if (null != obj)
			{
				str = obj.ToString().Trim();
				return string.IsNullOrEmpty(str);

			}
			return true;
		}

		private Range GetRange(int row, int col)
		{
			return questionSettingSheet.Cells[row, col];
		}
	}
}