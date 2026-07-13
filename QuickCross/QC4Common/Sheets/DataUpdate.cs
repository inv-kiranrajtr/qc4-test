using QC4Common.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;

namespace QC4Common.Sheets
{
	public class DataUpdate
	{
		private Excel.Worksheet Sheet;
		private String connectionString;

		public DataUpdate(Excel.Workbook workbook, Excel.Worksheet sheet)
		{
			Sheet = sheet;
			connectionString = DB.DBHelper.GetConnectionString(workbook);
		}

		//public void LoadData(string tableName, List<Model.QuestionSettings> questions)
		//{
			//questions = questions.OrderBy(a => a.Id).ToList();
			//string[] variableName = questions.Select(a => a.Variable).ToArray();
			//DataTable dtCount = DB.DBHelper.GetDataTable("SELECT COUNT(*) FROM " + tableName, connectionString);
			//Sheet.Cells.ClearContents();
			////Excel.Range dummy = Sheet.Range["A1"];
			////double cW = dummy.ColumnWidth;
			////double rH = dummy.RowHeight;
			////dummy.ColumnWidth = 221;
			////dummy.RowHeight = 409.5;
			//var ary = questions.Where(q => q.Id != 0).Select(q => "q_" + q.Id).ToArray();
			//string sql = String.Join(",", ary);
			//sql = "select sample_id, " + sql + " from " + tableName;
			//int samplescount = 0;
			//if (dtCount != null)
			//{
			//	samplescount = Convert.ToInt32(dtCount.Rows[0][0]);
			//}
			//int max = variableName.Length;
			//string[,] varAry = new string[1, max];
			//for (int i = 0; i < max; i++)
			//{
			//	varAry[0, i] = variableName[i];
			//}

			//Excel.Range r = Sheet.Range["A3"].Resize[1, max];
   //         r.AutoFilter(1, System.Reflection.Missing.Value, Excel.XlAutoFilterOperator.xlOr, System.Reflection.Missing.Value, true);
   //         r.Value = varAry;
			//int sortNo = 0;
			//int rowNumber = 4;
			//int nPageCount = samplescount % Constants.MaxRowCount > 0 ? (samplescount / Constants.MaxRowCount) + 1 : (samplescount / Constants.MaxRowCount);
			//for (int i = 0; i < nPageCount; i++)
			//{
			//	DataTable dt = DB.DBHelper.GetDataTable(sql + "  where sort_no > " + sortNo + " and sort_no <= " + (sortNo + Constants.MaxRowCount), connectionString);
			//	object[,] tablearray = LoadDataToArray(dt, questions);
			//	Excel.Range startcell = Sheet.Cells[rowNumber, 1];
			//	Excel.Range datarange = startcell.Resize[tablearray.GetLength(0), tablearray.GetLength(1)];
			//	datarange.Value = tablearray;
			//	sortNo += Constants.MaxRowCount;
			//	rowNumber += tablearray.GetLength(0);
			//}
			//dummy.ColumnWidth = cW;
			//dummy.RowHeight = rH;
		//}

		public string[,] LoadDataToArray(DataTable dt, List<Model.QuestionSettings> questions)
		{
			int maxRow = dt.Rows.Count;
			int maxCol = questions.Count;
			string[,] array = new string[maxRow, maxCol];
			for (int c = 0; c < maxCol; c++)
			{
				switch (questions[c].AnswerType)
				{
					case Constants.AnswerType.MA:
						for (int r = 0; r < maxRow; r++)
						{
							array[r, c] = dt.Rows[r][c] == null ? "" : ConverToMaAnser(dt.Rows[r][c].ToString());
						}
						break;
					case Constants.AnswerType.FA:
						for (int r = 0; r < maxRow; r++)
						{
							array[r, c] = dt.Rows[r][c] == null ? "" : dt.Rows[r][c].ToString().StartsWith("=") ? "'" + dt.Rows[r][c].ToString() : dt.Rows[r][c].ToString();
						}
						break;
					default:
						for (int r = 0; r < maxRow; r++)
						{
							array[r, c] = dt.Rows[r][c] == null ? "" : dt.Rows[r][c].ToString();
						}
						break;
				}
			}
			return array;
		}

		private string ConverToMaAnser(string str)
		{
			string res = "";
			if (String.IsNullOrEmpty(str))
			{
				return res;
			}

			if ("*".Equals(str))
			{
				return str;
			}

			char[] array = str.ToCharArray();
			int len = array.Length;
			for (int i = len - 1; i >= 0; i--)
			{
				if (array[i] == '1')
				{
					res += "," + (len - i);
				}
			}

			if (!String.IsNullOrEmpty(res))
			{
				res += ",";
			}
			return res;
		}
	}
}
