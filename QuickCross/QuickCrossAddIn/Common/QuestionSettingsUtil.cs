using ExcelAddIn.DB;
using ExcelAddIn.Util;
using Microsoft.Office.Interop.Excel;
using QC4Common.Model;
using Constant = QC4Common.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using QC4Common.Sheets;

namespace ExcelAddIn.Common
{
	public class QuestionSettingsUtil
	{
		private static List<QuestionSettings> categoryCountChange = new List<QuestionSettings>();
		private static List<QuestionSettings> answerTypeChange = new List<QuestionSettings>();

		public static void DeleteQuestionSetting(int row)
		{
			row -= 4;
			if (Definitions.RowVariableList.Count() <= row )
			{
				return;
			}
			if ("" == Definitions.RowVariableList[row])
			{
				Definitions.RowVariableList.RemoveAt(row);
				return;
			}
			Definitions.VariableDictionary.Remove(Definitions.RowVariableList[row]);
			Definitions.RowVariableList.RemoveAt(row);
		}

		public static void InsertQuestionSetting(int row,string variable,QuestionSettings qs = null)
		{
			if (qs == null)
			{
				qs = new QuestionSettings(variable);
			}
			row -= 4;
			if (row >= Definitions.RowVariableList.Count)
			{
				for (int i = Definitions.RowVariableList.Count; i < row; i++)
				{
					Definitions.RowVariableList.Insert(i, "");
				}
				Definitions.RowVariableList.Insert(row, variable);
			}
			else
			{
				Definitions.RowVariableList[row] = variable;
			}
			
            Definitions.VariableDictionary.Add(variable, qs);
			Worksheet sheet = ExcelUtil.GetWorksheetByName(Constants.SheetType.sh_ListView);
			Range x = ExcelUtil.EndxlUp(sheet.Columns.Cells[Constants.ListSheet.StartRow, Constants.ListSheet.ALLD]);
			x.Value = qs.Variable;
		}

		public static void UpdateQuestionSettingVariable(int row,string variable,QuestionSettings qs = null)
		{
			row -= 4;
			if (qs == null)
			{
				qs = Definitions.VariableDictionary[Definitions.RowVariableList[row]];
			}
			qs.Variable = variable;
            Definitions.VariableDictionary.Remove(Definitions.RowVariableList[row]);
            Definitions.VariableDictionary.Add(qs.Variable, qs);
			Definitions.RowVariableList[row] = qs.Variable;
		}

		public static int IsVariableFound(string variable)
		{
			int row = -1;
			
			row = Definitions.RowVariableList.FindIndex(x => x.Equals(variable, StringComparison.OrdinalIgnoreCase));
			if (row != -1)
			{
				row += 4;
			}
			return row;
		}

		private static ReturnClass VariableColumCheck(Worksheet worksheet)
		{
			Range range = worksheet.Cells[Constant.QS.QsRowDataStart, Constant.QS.QsColItem];
			Range end = ExcelUtil.EndxlUp(range);

			int maxRow = Definitions.VariableDictionary.Count();
			List<string> varList = new List<string>();
			range = worksheet.get_Range(range, end);
			Object[,] objAry = range.Value2;
			for (int i = 1; i <= maxRow; i++)
			{

				if (objAry[i, 1] == null)
				{
					return new ReturnClass(false, worksheet.Cells[Constant.QS.QsRowHeader + i, Constant.QS.QsColItem], string.Format(AddinResource.QS_VARIABLE_NULL_NOT_ALLOWED, (Constant.QS.QsRowHeader + i).ToString()));
				}

				string variable = objAry[i, 1].ToString();
				if (!QC4Common.Util.QSUtil.ValidateVariable(variable, out string msg))
				{
					return new ReturnClass(false, worksheet.Cells[(i + 3), Constants.QS.QsColItem], msg, new string[] { (i + 3).ToString() });
				}

				//if (varList.IndexOf(objAry[i, 1].ToString()) != -1)
				if (varList.Contains(objAry[i, 1].ToString(), StringComparer.OrdinalIgnoreCase))
				{
					return new ReturnClass(false, worksheet.Cells[Constant.QS.QsRowHeader + i, Constant.QS.QsColItem], string.Format(AddinResource.QS_VARIABLE_DUPLICATED, (Constant.QS.QsRowHeader + i).ToString()));
				}
				varList.Add(objAry[i, 1].ToString());
			}
			return new ReturnClass(true);
		}


		public static ReturnClass ValidateVarable()
		{
			var array = Definitions.VariableDictionary
				  .Where(pair =>  pair.Value.VariableBefore.Trim() != pair.Key && !pair.Value.IsNew)
				  .ToArray();
			if (array.Count() == 0)
			{
				return new ReturnClass(true);
			}
			int count = Definitions.RowVariableList.IndexOf(array.First().Value.Variable);
			
			return new ReturnClass(false,null ,AddinResource.QS_INTEGIRITY_VARIABLE_CHANGED, new string[] { count + 4 + "", array.First().Value.Variable, array.First().Value.VariableBefore }); //array.First().Value.VariableBefore + " is changed to " + array.First().Value.Variable); 
		}

		public static bool VariableEditModeChange(Workbook workbook)
		{
			ReturnClass rc;
			Worksheet sheet = ExcelUtil.GetWorkSheetByCodeName(workbook, Constant.SheetType.sh_QuesSetting);
			if (Definitions.VariableEditMode)
			{
				var dict = QSSheetChange.GetUpdateList(out bool edited);
				if(edited)
				{
					rc = VariableColumCheck(sheet);
					if (!rc.Result)
					{
						MessageDialog.ErrorOk(rc.Msg);
						Range range = (Range)rc.Value;
						range.Select();
						return false;
					}
				}
				
				sheet.Unprotect(Constants.variableModePassword);
				sheet.Application.CellDragAndDrop = true;

				if (dict != null && dict.Count() > 0)
				{
					Definitions.isQsChanged = true;
				}

				QC4Common.Common.CommonFlag.SetIsDataUpdated(Globals.ThisAddIn.Application.ActiveWorkbook, false);
				QC4Common.Common.CommonFlag.SetIsDataAfterUpdated(Globals.ThisAddIn.Application.ActiveWorkbook, false);
                QC4Common.Common.CommonFlag.SetIsMultivariateUpdated(Globals.ThisAddIn.Application.ActiveWorkbook, false);
                QuestionSettingDao dao = new QuestionSettingDao(QC4Common.DB.DBHelper.GetConnectionString(workbook));
				dao.UpdateVariable(dict);
			}
			else
			{
				 rc = new QSValidate(Globals.ThisAddIn.Application.ActiveWorkbook).QuestionConfigurationCheck();
				if (!rc.Result)
				{
					Range r = (Range)rc.Value;
					r.Select();
					MessageDialog.ErrorOk(rc.Msg);
					return false;
				}

				Definitions.VariableDictionary = QC4Common.Util.DictionaryUtil.PopulateQSDictionary(workbook);
				rc = new QS.IntegrityCheck(workbook).Check(out List<QuestionSettings> l1, out List<QuestionSettings> l2, 
					out List<QuestionSettings> l3, out List<QuestionSettings> l4, out List<QuestionSettings> l5, false);
				if (!rc.Result)
				{
					if (rc.Value != null)
					{
						Range r = (Range)rc.Value;
						r.Select();
					}
					MessageDialog.ErrorOk(rc.Msg);
					return false;
				}
				QSSheetChange.ClearUpdateList();

				sheet.Application.CellDragAndDrop = false;
				sheet.Cells.Locked = true;
				Range gtSettingRange = ExcelUtil.EndxlUp(sheet.Range[Constants.QS.QsStartAddress]);
				gtSettingRange = sheet.Range[sheet.Range[Constants.QS.QsStartAddress], gtSettingRange];
				gtSettingRange.Locked = false;
				
			}

			Definitions.VariableEditMode = !Definitions.VariableEditMode;
			foreach (Shape shp in sheet.Shapes)
			{
				if (Constants.QS.QsTopBoxName == shp.Name)
				{
					var dyShapes = shp.GroupItems;
					foreach (dynamic item in dyShapes)
					{
						if (Constants.QS.QsTopShapeName == item.Name)
						{
							item.TextFrame2.TextRange.Text = Definitions.VariableEditMode ? AddinResource.QS_VARIABLE_EDIT_MODE : AddinResource.QS_TOP_BOX_TEXT;
						}
					}
					shp.Fill.ForeColor.RGB = Definitions.VariableEditMode ? Constants.Color.YellowGreen.ToArgb() : Constants.Color.Grey.ToArgb(); ;
					break;
				}
			}
			if(Definitions.VariableEditMode) sheet.Protect(Constants.variableModePassword);
			return true;
		}

		public static bool IsNew(int row)
		{
			bool flag = true;
			row -= 4;
			if (Definitions.RowVariableList.Count >= row)
			{
				if (!"".Equals(Definitions.RowVariableList[row]))
				{
					flag = false;
				}
			}
			return flag;
		}


		public static ReturnClass QsSheetChangeValidate(Workbook workbook)
		{
			
			if (Definitions.VariableEditMode)
			{
				
				return new ReturnClass(false,null,AddinResource.QS_VARIABLE_MODE_SHEET_CHANGE);
				

			}

			ReturnClass result = new QSValidate(workbook).QuestionConfigurationCheck();
			if (!result.Result)
			{
				return result;
			}
			return new ReturnClass(true);
		}

		public static bool QsValidateSheet()
		{
			ReturnClass result = new QSValidate(Globals.ThisAddIn.Application.ActiveWorkbook).QuestionConfigurationCheck();
			if (!result.Result)
			{
				Range value = (Range)result.Value;
				value.Select();
				MessageDialog.ErrorOk(result.Msg);
			}
			return result.Result;
		}

		public static void ChangeFromQs(Worksheet questionSheet, Workbook workbook
			, List<QuestionSettings> variableChanges, List<QuestionSettings> answerChanges
			, List<QuestionSettings> countChanges, List<QuestionSettings> deleteList, List<QuestionSettings> updateList)
		{
			QuestionSettingDao qDao = new QuestionSettingDao(QC4Common.DB.DBHelper.GetConnectionString(workbook));
			if (deleteList.Where(q => q.QuestionFlag == Constant.QuestionFlag.Imp).Count() > 0)
			{
				QC4Common.Common.CommonFlag.SetIsDataUpdated(Globals.ThisAddIn.Application.ActiveWorkbook, false);
                QC4Common.Common.CommonFlag.SetIsDataAfterUpdated(Globals.ThisAddIn.Application.ActiveWorkbook, false);
                QC4Common.Common.CommonFlag.SetIsMultivariateUpdated(Globals.ThisAddIn.Application.ActiveWorkbook, false);
            }

			qDao.DeleteQuestions(deleteList);
			qDao.UpdateQuestionSetting(updateList, answerChanges, countChanges);
			List<QuestionSettings> questionsList = Definitions.VariableDictionary.Select(d => d.Value).Where(q => q.IsNew).ToList();
			qDao.InsertQuestionSetting(questionsList, questionSheet);
			UpdateQuestionFlag(questionSheet);
			new ListUpdate(workbook).UpdateListSheet(Definitions.VariableDictionary.Select(q => q.Value).ToList());
			//Worksheet sheet = ExcelUtil.GetWorksheetByIndex(2);
			Worksheet sheet = ExcelUtil.GetWorkSheetByCodeName(workbook,Constant.SheetCodeName.DataProcess);
			Range start = sheet.Cells[3,Constants.DP.InstructionColumn];

			Range end = ExcelUtil.EndxlUp(start);
			if (Definitions.isQsChanged)
			{
				//QC4Common.Common.CommonFlag.SetQsUpdated(workbook, true);
				if (start.Row != end.Row )
				{
					MessageDialog.Warning(AddinResource.QS_SHEET_CHANGE_ALERT);
				}
			}
			string[] str = Definitions.VariableDictionary.Select(q => q.Value).Where(q => q.QuestionFlag != q.QuestionFlagUpdated).Select(q => q.Variable).ToArray();
			if (str.Count() > 0)
			{
				MessageDialog.Warning(AddinResource.QS_ALERT_FLAG_UPDATE + "\n" + String.Join(",", str)); 
			}
		}


		public static void UpdateQuestionFlag(Worksheet worksheet)
		{
			bool eEvent = worksheet.Application.EnableEvents;
			worksheet.Application.EnableEvents = false;
			List<QuestionSettings> list = Definitions.VariableDictionary.Select(a => a.Value).OrderBy(a => a.RowNumber).ToList();
			int max = list.Count();
			string[,] array = new string[max, 1];
			for (int i = 0; i < max; i++)
			{
				array[i, 0] = list[i].QuestionFlag;
			}
			Range range = worksheet.Cells[Constant.QS.QsRowDataStart, Constant.QS.QsColNew];
			range.Resize[max, 1].Value2 = array;
			worksheet.Application.EnableEvents = eEvent;
		}

		public static void UpdateIdFlag()
		{
			Definitions.IdFlagDictionary = new Dictionary<int, string>();
			Definitions.IdFlagDictionary.Add(-1, "New");
			foreach (QuestionSettings qs in Definitions.VariableDictionary.Values)
			{
				int id = Convert.ToInt32(qs.Id);
				if (!Definitions.IdFlagDictionary.ContainsKey(id))
				{
					Definitions.IdFlagDictionary.Add(id, qs.QuestionFlag);
				}
			}
		}

		public static ReturnClass MoveFromQs(Workbook workBook, Worksheet qsSheet, bool isSave = false, bool alert = true)
		{
			if (!Definitions.isQsUpdated)
			{
				return new ReturnClass(true);
			}

			ReturnClass result = QuestionSettingsUtil.QsSheetChangeValidate(workBook);
			if (!result.Result)
			{
				return result;
			}

			result = new QS.IntegrityCheck(workBook).Check(out List<QuestionSettings> variableChanges, out List<QuestionSettings> answerChanges,
				out List<QuestionSettings> countChanges, out List<QuestionSettings> deleteList, out List<QuestionSettings> updateList, alert);
			if (!result.Result)
			{
				return result;
			}

			if (!isSave)
			{
				QuestionSettingsUtil.ChangeFromQs(qsSheet, workBook, variableChanges, answerChanges, countChanges, deleteList, updateList);
			}
			return new ReturnClass(true);
		}

	}
}
