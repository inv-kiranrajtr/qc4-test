using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelAddIn.Common
{
	class CellValidation
	{
		//eg :- !1-2,4,2-7
		internal static ReturnClass ValidateNumeric(string Contents, int minvalue, int maxvalue, int row, int col)
		{
			//	if (range.Text == "")
			//{
			//	return new ReturnClass(true);
			//}

			string[] SplitContent;
			//string Contents = range.Text;
			if (Contents.Contains("/") || Contents.Contains(","))
			{
				char[] splitchar = { '/', ',' };
				SplitContent = Contents.Split(splitchar);
				foreach (string item in SplitContent)
				{
					try
					{
						if (item.Contains("-"))
						{
							ReturnClass rc = ValidateRange(item, minvalue, maxvalue, row, col);//, range);
							if (rc.Result == false)
							{
								return rc;
							}
						}
						else
						{
							int value = Convert.ToInt32(item);
							if (value < minvalue || value > maxvalue)
							{
								return new ReturnClass(false, null, string.Format(AddinResource.SET_INETGER_BETWEEN_RANGE, minvalue.ToString(), maxvalue.ToString(), row.ToString(), col.ToString()));
							}
						}

					}
					catch (Exception ex)
					{
						return new ReturnClass(false, null, string.Format(AddinResource.SET_NUMERIC_VALUE_RANGE, QC4Common.Util.ExcelUtil.GetExcelColumnName(col) + row));
					}
				}
			}
			else
			{
				return ValidateRange(Contents, minvalue, maxvalue, row, col);//, range);
			}
			return new ReturnClass(true);
		}

		private static ReturnClass ValidateRange(string Contents, int minvalue, int maxvalue, int row, int col)//, Excel.Range range)
		{
			string[] SplitContent;
			SplitContent = Contents.Split('-');
			foreach (string item in SplitContent)
			{
				try
				{

					int value = Convert.ToInt32(item);
					if (value < minvalue || value > maxvalue)
					{
						return new ReturnClass(false, null, string.Format(AddinResource.SET_INETGER_BETWEEN_RANGE, minvalue.ToString(), maxvalue.ToString(), row.ToString(), col.ToString()));
					}

				}
				catch (Exception ex)
				{
					return new ReturnClass(false, null, string.Format(AddinResource.SET_NUMERIC_VALUE_RANGE, QC4Common.Util.ExcelUtil.GetExcelColumnName(col) + row));
				}
			}
			return new ReturnClass(true);
		}
	}
}
