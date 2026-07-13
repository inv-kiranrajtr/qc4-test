using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelAddIn.Common
{
	class SCExcelClass
	{
		public static Excel.Range FIndCell(Excel.Range targetRange,string varFind, Excel.Range nextCell = null, bool bLUCase = false, bool autoDateMode = true)
		{
			//			'-----------------------------------------------------------------------
			//'<概要>     検索範囲から条件に一致するRangeオブジェクトを検索
			//'<引数>     Target_Range    ：検索範囲
			//'<引数>     var_Find        ：検索文字列
			//'<引数>     Next_Cell       ：指定されたセルの次から検索
			//'<引数>     b_LUCase        ：大文字小文字の区別　True:する　False:しない
			//'<引数>     Auto_Date_Mode  ：日付検索の場合、自動的にシリアル値に変換して検索
			//'<戻値>     Rangeオブジェクト（見つからなければNothing）
			//'<備考>     １列、または１行のみ対象
			//'-----------------------------------------------------------------------

			Excel.Range findRange;
			Excel.Range retCell;
			Excel.Range lastCell;
			double findNo;
			bool dateMode;

			retCell = null;
			findRange = null;
			findNo = 0;

			if (targetRange.Rows.Count == 1 || targetRange.Columns.Count == 1)
			{
				if (null == nextCell)
				{
					findRange = targetRange;
					var xx = findRange.Address;
				}
				else
				{
					lastCell = targetRange.Cells[targetRange.Cells.Count];
					if (null == lastCell.Application.Intersect(lastCell, nextCell))
					{
						findRange = targetRange.Worksheet.get_Range(nextCell.Offset[targetRange.Columns.Count == 1 ? 1 : 0, targetRange.Rows.Count == 1 ? 1 : 0], lastCell);
					}
				}

				dateMode = false;
				//	Date_Mode = False
				//   If (IsDate(var_Find) = True) And (Auto_Date_Mode = True) Then
				//       var_Find = CDbl(CDate(var_Find))
				//       Date_Mode = True
				//   End If

				try
				{
					findNo = targetRange.Application.WorksheetFunction.Match(varFind, findRange, 0);
				}
				catch { }

				if (findNo > 0)
				{
					retCell = findRange.Cells[findNo];

					if (bLUCase && !dateMode)
					{
						if (retCell.Value != varFind)
						{
							retCell = FIndCell(targetRange, varFind, retCell, true);
						}
					}
				}
			}
			return retCell;
		}
	}
}
