using QC4Common.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using Constant = QC4Common.Common.Constants;
using QSUtil = QC4Common.Util.QSUtil;



namespace ExcelAddIn.QS
{
	class QSHelper
	{
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
	}
}
