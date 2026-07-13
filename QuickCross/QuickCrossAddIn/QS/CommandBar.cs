using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using QSUtil = QC4Common.Util.QSUtil;
using MessageDialog = QC4Common.Common.MessageDialog;
using Constant = QC4Common.Common.Constants;
using ExcelAddIn.Common;

namespace ExcelAddIn.QS
{
	class CommandBar
	{
		public static int DeleteQSRow(Excel.Range range)
		{
			Excel.Range rangeRow = range.EntireRow;
			bool isOrg = false;
			bool isAnImp = false;
			bool isRow = false;
			bool isVarRow = false;

			if (range.Address == rangeRow.Address)
			{
				isRow = true;
			}
			if (range.Row <= 3)
			{
				MessageDialog.ErrorOk(AddinResource.ALERT_HEADER_DELETE);
				return 1;
			}

			if (!isRow)
			{
				isVarRow = QSUtil.IsVariableColumnFound(range);
			}

			GetQuestionFlag(rangeRow, out isOrg, out isAnImp);

			if (isOrg)
			{
				//if (isRow || isVarRow)
				//{
					QSUtil.OrgDelete();
				//}
				//else
				//{
				//	Definitions.isQsUpdated = true;
				//	ClearContents(range);
				//}
			}
			else if (isAnImp)
			{
				if (isRow)
				{
					QSUtil.AnImpRowDelete(range);
				}
				else
				{
					ClearContents(range);
				}
				Definitions.isQsUpdated = true;
			}
			else
			{
				if (isRow)
				{
					DeleteRow(range);
				}
				else
				{
					ClearContents(range);
				}
				Definitions.isQsUpdated = true;
			}
			return 1;
		}

		public static void GetQuestionFlag(Excel.Range rangeRow, out bool isOrg, out bool isAnImp)
		{
			isOrg = false;
			isAnImp = false;

			foreach (Excel.Range r in rangeRow.Rows)
			{
				string flag = "New";
				if (Common.Definitions.IdFlagDictionary.ContainsKey(QSUtil.GetQSRowId(r)))
				{
					flag = Common.Definitions.IdFlagDictionary[QSUtil.GetQSRowId(r)];
				}

				if (flag == Constant.QuestionFlag.Org)
				{
					isOrg = true;
					return;
				}
				else if (flag == Constant.QuestionFlag.An || flag == Constant.QuestionFlag.Imp)
				{
					isAnImp = true;
				}
			}
		}

		public static void ClearContents(Excel.Range range)
		{
			Globals.ThisAddIn.Application.EnableEvents = false;
			//range.ClearContents();
			QC4Common.Util.ExcelUtil.ClearContents(range);
			Globals.ThisAddIn.Application.EnableEvents = true;
		}

		public static void DeleteRow(Excel.Range row)
		{
			Globals.ThisAddIn.Application.EnableEvents = false;
			row.Delete();
			Globals.ThisAddIn.Application.EnableEvents = true;
		}
	}
}
