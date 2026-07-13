using Excel = Microsoft.Office.Interop.Excel;
using QC4Common.Common;
using QC4Common.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QC4Common.Model;
using System.Windows.Forms;

namespace QC4Common.Sheets
{
	public class ListUpdate
	{
		private Excel.Workbook TargetWorkBook { get; set; }

		public ListUpdate(Excel.Workbook workbook)
		{
			TargetWorkBook = workbook;
		}

		private ArrayList saArray = new ArrayList();
		private ArrayList maArray = new ArrayList();
		private ArrayList nArray = new ArrayList();
		private ArrayList samaArray = new ArrayList();
		private ArrayList sanArray = new ArrayList();
		private ArrayList manArray = new ArrayList();
		private ArrayList samanArray = new ArrayList();
		private ArrayList faArray = new ArrayList();
		private ArrayList allArray = new ArrayList();
		private ArrayList allDArray = new ArrayList();

		public void UpdateListSheet(List<QuestionSettings> qData)
		{
			qData = qData.OrderBy(q => q.RowNumber).ToList();
			int max = qData.Count();
			for (int i = 0; i < max; i++)
			{
				UpdateListVariable(qData[i].Variable, qData[i].AnswerType);
			}
			UpdateListSheet();
		}

		private void UpdateListVariable(string variable, string ansType)
		{
			switch (ansType)
			{
				case Constants.AnswerType.SA:
					saArray.Add(variable);
					samaArray.Add(variable);
					sanArray.Add(variable);
					samanArray.Add(variable);
					allArray.Add(variable);
					allDArray.Add(variable);
					break;

				case Constants.AnswerType.MA:
					maArray.Add(variable);
					samaArray.Add(variable);
					manArray.Add(variable);
					samanArray.Add(variable);
					allArray.Add(variable);
					allDArray.Add(variable);
					break;

				case Constants.AnswerType.N:
					nArray.Add(variable);
					sanArray.Add(variable);
					manArray.Add(variable);
					samanArray.Add(variable);
					allArray.Add(variable);
					allDArray.Add(variable);
					break;

				case Constants.AnswerType.FA:
					faArray.Add(variable);
					allArray.Add(variable);
					allDArray.Add(variable);
					break;

				case Constants.AnswerType.D:
					allDArray.Add(variable);
					break;
			}
		}

		private void UpdateListSheet()
		{
			Excel.Worksheet list = ExcelUtil.GetWorkSheetByCodeName(TargetWorkBook, Constants.SheetCodeName.List);

			String[,] outPutArray = new string[allDArray.Count, 10];

			ListOutPutAdd(outPutArray, saArray, 0);
			ListOutPutAdd(outPutArray, maArray, 1);
			ListOutPutAdd(outPutArray, nArray, 2);
			ListOutPutAdd(outPutArray, samaArray, 3);
			ListOutPutAdd(outPutArray, sanArray, 4);
			ListOutPutAdd(outPutArray, manArray, 5);
			ListOutPutAdd(outPutArray, samanArray, 6);
			ListOutPutAdd(outPutArray, faArray, 7);
			ListOutPutAdd(outPutArray, allArray, 8);
			ListOutPutAdd(outPutArray, allDArray, 9);

			Excel.Range r1 = list.Range["J1"];
			r1 = ExcelUtil.EndxlUp(r1);
			ExcelUtil.ClearContents(list.get_Range("A2", "J" + (r1.Row)));
			//IDataObject dataObj = Clipboard.GetDataObject();
			//list.get_Range("A2", "J" + (r1.Row)).ClearContents();
			//Clipboard.SetDataObject(dataObj);
			list.get_Range("A2", "J" + (allDArray.Count + 1)).Value = outPutArray;
		}


		private static void ListOutPutAdd(String[,] outPutArray, ArrayList list, int n)
		{
			for (int i = 0; i < list.Count; i++)
			{
				outPutArray[i, n] = list[i].ToString();
			}
		}
	}
}
