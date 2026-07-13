using Qc4Launcher.Model;
using Qc4Launcher.DB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using ProgressBarForm = Qc4Launcher.Forms.ProgressBar;
using ExcelAddIn.Sheets;
using QC4Common.Model;
using System.IO;
using log4net;
using System.Reflection;
using Macromill.QCWeb.COMOperate;
using System.Diagnostics;

namespace Qc4Launcher.Util
{
    internal class Qc3Parse
    {
		//Logger.Log log;
		ExcelOperate excelOperate;

		private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public delegate void OnWorkerMethodCompleteDelegate(double message, string status);
        public event OnWorkerMethodCompleteDelegate OnWorkerComplete;
        private ProgressBarForm progress = null;
        System.Windows.Window ParentWindow;

        private Excel.Workbook SourceWorkBook;
        private Excel.Workbook TargetWorkBook;

		public string TargetPath;
		private string SourcePath;
		public string TempPath;

		int maxRowCount = 10000;

        private Dictionary<string, QuestionSettings> dictQS;
        internal double prec = 0;
        bool isCancelled = false;
        private Excel.Worksheet questionSettingSheet;

        #region ListSheetUpdateVariable
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
        #endregion

        #region constuctor
        internal Qc3Parse(System.Windows.Window parent, string filePath, string tempPath)
        {
            SourcePath = filePath;
            ParentWindow = parent;
            TempPath = tempPath;
            dictQS = new Dictionary<string, QuestionSettings>();
        }

		internal Qc3Parse(string tempPath, Excel.Workbook workbook)
		{
			TempPath = tempPath;
			SourceWorkBook = workbook;
			questionSettingSheet = ExcelUtil.GetWorkSheetByCodeName(SourceWorkBook, Constants.SheetCodeName.QuestionSettingB);
		}

		internal Qc3Parse(Excel.Workbook sourceWorkBook, System.Windows.Window parent, string sourcePath, string tempPath)
        {
            SourceWorkBook = sourceWorkBook;

            dictQS = new Dictionary<string, QuestionSettings>();
            ParentWindow = parent;
            SourcePath = sourcePath;
            TempPath = tempPath;
            //log = new Logger.Log();
            questionSettingSheet = SourceWorkBook.Worksheets[1];
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns>null - if parsing failed</returns>
        #region publicFunction
        internal Excel.Workbook StartParsing(ref ExcelOperate eOperate, string tempFolder = Constants.PathName.FileOpenTemp, bool ShowAlert = true)
        {
            progress = new ProgressBarForm();
            progress.Owner = ParentWindow;
            this.OnWorkerComplete += new Qc3Parse.OnWorkerMethodCompleteDelegate(OnWorkerMethodComplete);
            new Thread(() => ParseQc3ToQc4(tempFolder)).Start();
            progress.ShowDialog();
            if (null == TargetWorkBook && !isCancelled && ShowAlert)
            {
                MessageDialog.ErrorOk(LocalResource.QC3PARSE_ALERT_UNEXPECTED);
            }
			eOperate = this.excelOperate;
			return TargetWorkBook;
        }

		internal Excel.Workbook ParseQc3ToQc4(string tempFolder = Constants.PathName.FileOpenTemp)
        {
            try
            {
                OnWorkerComplete(prec += 1, LocalResource.QC3PARSE_PB_LOADING_QC3FILE);
                OpenQc3File();

                questionSettingSheet = SourceWorkBook.Worksheets[1];
                List<Excel.Worksheet> multivariateSheets = QCWorkbookHelper.GetMultivariateSheet(SourceWorkBook);
                if (multivariateSheets.Count > 0)
                {
                    isCancelled = true;
                    Excel.Application app = SourceWorkBook.Application;
                    SourceWorkBook.Close();
                    app.Quit();
					excelOperate.Dispose();
                    OnWorkerComplete(prec += 100, LocalResource.QC3PARSE_PB_FAILED);
                    MessageDialog.ErrorOk(LocalResource.QC3PARSE_ALERT_CONTAIN_MULTI_SHEET);
                    return null;
                }

                List<QDataDetail> qData = GetQDataDetail(out List<Excel.Worksheet> dataSheets, out List<int> dataSheetMaxCol);
                if (qData == null)
                {
                    isCancelled = true;
                    Excel.Application app = SourceWorkBook.Application;
                    SourceWorkBook.Close();
                    app.Quit();
					excelOperate.Dispose();
					OnWorkerComplete(100, LocalResource.QC3PARSE_PB_FAILED);
                    MessageDialog.ErrorOk(LocalResource.QC3PARSE_ALERT_CONTAIN_INVALID_VARIABLE1);
					return null;
                }

                if(qData.Count>0&& qData[0].variableName!= "SAMPLEID")
                {
                    isCancelled = true;
                    Excel.Application app = SourceWorkBook.Application;
                    SourceWorkBook.Close();
                    app.Quit();
                    excelOperate.Dispose();
                    OnWorkerComplete(100, LocalResource.QC3PARSE_PB_FAILED);
                    MessageDialog.ErrorOk(LocalResource.QC3PARSE_ALERT_CONTAIN_INVALID_VARIABLE);
                    return null;
                }

                if(!ISValidQSN())
                {
                    isCancelled = true;
                    Excel.Application app = SourceWorkBook.Application;
                    SourceWorkBook.Close();
                    app.Quit();
                    excelOperate.Dispose();
                    OnWorkerComplete(100, LocalResource.QC3PARSE_PB_FAILED);
                    MessageDialog.ErrorOk(LocalResource.QCM_INVALID_QUESTION_NUMBER);
                    return null;
                }

                SourceWorkBook.Application.DisplayAlerts = false;

                OnWorkerComplete(prec += 5, LocalResource.QC3PARSE_PB_LOADING_TEMPLATE);
                TargetWorkBook = OpenTemplate();
                TargetWorkBook.Application.EnableEvents = false;
                TargetWorkBook.Application.DisplayAlerts = false;
                TempPath = QcFileHelper.CreateTempFile(TempPath, tempFolder);
                OnWorkerComplete(prec += 1, LocalResource.QC3PARSE_PB_LOADING_QS_DATASHEET);
                string[] varAry = qData.Where(q => !q.isFound).Select(q => q.variableName).ToArray();
                OnWorkerComplete(prec += 6, LocalResource.QC3PARSE_PB_PROCESS_QS_UPDATION);
				maxRowCount = (int)(10000 * ((float)300 / qData.Count()));
				UpdateQS(qData);
                OnWorkerComplete(prec += 1, LocalResource.QC3PARSE_PB_PROCESS_DATA_UPDATION);
                DataUpdation(dataSheets, qData, dataSheetMaxCol);
                Process currentProcess = Process.GetCurrentProcess();
                QC4Common.DB.DBHelper.SetConnectionString(TargetWorkBook, TempPath, Path.GetFileNameWithoutExtension(SourcePath) + Constants.Qc4Extension, TargetPath, currentProcess.Id.ToString());
                OnWorkerComplete(prec += 5, LocalResource.QC3PARSE_PB_INIT_SETTING);
                UpdateSettings(qData);
                OnWorkerComplete(prec += 5, LocalResource.QC3PARSE_PB_INIT_LIST);
                UpdateListSheet(qData);
                OnWorkerComplete(prec += 4, LocalResource.QC3PARSE_PB_INIT_GT);
				UpdateGTSheet();
                OnWorkerComplete(prec += 1, LocalResource.QC3PARSE_PB_INIT_CR);
                UpdateSheetCS(qData);
                DBHelper.ExecuteQuery("CREATE TABLE IF NOT EXISTS `weight_back` (id INTEGER PRIMARY KEY AUTOINCREMENT,name VARCHAR(255) ,weight_back_table VARCHAR(255))", DBHelper.GetConnectionString(TargetWorkBook));
                OnWorkerComplete(90, "");
				TargetWorkBook.Password = Constants.Password;
				TargetWorkBook.Protect(Constants.Password, true);
				Excel.Worksheet ldelSheet = ExcelUtil.GetWorkSheetByCodeName(TargetWorkBook, Constants.SheetCodeName.LDEL);
				ldelSheet.Protect(Constants.Password);
				TargetWorkBook.SaveAs(TempPath + "\\" + Constants.TemplateFile.QC4_Template);
				TargetPath = Path.GetDirectoryName(SourcePath) + "\\" + Path.GetFileNameWithoutExtension(SourcePath) + ".qc4";
                QC4Common.Util.FileHelper fileHelper = new QC4Common.Util.FileHelper();
				fileHelper.SaveFile(ref TargetPath,ref TempPath, TargetWorkBook, TargetWorkBook.Application, true, false);

                try
                {
                    if (!Qc4Launcher.MainWindow.File_read_only)
                    {
                        Qc4Launcher.MainWindow.fileloack = new FileStream(TargetPath, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
                    }
                }
                catch { }
                //QcFileHelper.SaveFile(ref TargetPath, TempPath, false);
                OnWorkerComplete(95, LocalResource.QC3PARSE_PB_UPDATE_SETTNIG);
                Definiotion.VariableDictionary = QC4Common.Util.DictionaryUtil.PopulateQSDictionary(TargetWorkBook);
                var array = Util.Definiotion.VariableDictionary.Where(a => (a.Value.QuestionFlag == QC4Common.Common.Constants.QuestionFlag.Org) || (a.Value.QuestionFlag == QC4Common.Common.Constants.QuestionFlag.Imp)).Select(q => q.Value).ToList();
                QC4Common.Util.ExcelUtil.GenerateNewDataSheet(TargetWorkBook, array);
                OnWorkerComplete(96, LocalResource.QC3PARSE_PB_ALMOST_FINISH);
                TargetWorkBook.Activate();
                TargetWorkBook.Application.EnableEvents = true;
                TargetWorkBook.Application.DisplayAlerts = true;
                OnWorkerComplete(97, LocalResource.QC3PARSE_PB_CLOSE_SOURCE_FILE);
                SourceWorkBook.Close(false);
                OnWorkerComplete(98, LocalResource.PB_SWITCH_LANGUAGE);
                if (ParentWindow is MainWindow)
                {
                    MainWindow main = (MainWindow)ParentWindow;
                    main.IsUpdateSheetLanguage = true;
                    main.ChangeWorkbookLanguage(TargetWorkBook);
                    main.SaveGlobalSetting(TargetWorkBook);
                }
                OnWorkerComplete(99, LocalResource.QC3PARSE_PB_FINISH);
               if (varAry.Count() > 0)
                {
                         ParentWindow.Dispatcher.Invoke(new Action(
            delegate ()
            {
                MessageDialog.Warning(string.Format(LocalResource.QC3PARSE_PB_ALERT_NOT_CONVERTED_ITEM, string.Join(", ", varAry)));
            }
            ));

                }
                OnWorkerComplete(100, LocalResource.QC3PARSE_PB_FINISH);
            }
            catch (Exception ex)
            {
                TargetWorkBook = null;
                _log.Error("QC3 to QC4 migration" + ex.Message + "\n" + ex.StackTrace);
                OnWorkerComplete(100, LocalResource.QC3PARSE_PB_FAILED);
            }

            return TargetWorkBook;
        }

        private bool ISValidQSN()
        {
            Excel.Range startRange = questionSettingSheet.Range["C4"];
            Excel.Range endRange = ExcelUtil.EndxlUp(startRange);
            Excel.Range tRange = questionSettingSheet.get_Range(startRange, endRange);
            if (tRange.Value2 is Array)
            {
                Object[,] qsVarAry = tRange.Value2;
                for (int i = 1; i <= qsVarAry.GetLength(0); i++)
                {
                    if (qsVarAry[i,1] != null && qsVarAry[i,1].ToString() != "" && (qsVarAry[i,1].ToString().Contains("\n") || qsVarAry[i,1].ToString().Contains("\t")))
                        return false;
                }
            }
            else
            {
                if (tRange.Value2 != null && tRange.Value2.ToString() != "" && (tRange.Value2.ToString().Contains("\n") || tRange.Value2.ToString().Contains("\t")))
                    return false;
            }
            return true;
        }

        #endregion

        private void OpenQc3File()
        {
            try
            {
				excelOperate = new ExcelOperate();

				Excel.Application application = excelOperate.Excel;
				application.DisplayAlerts = false;
                SourceWorkBook = application.Workbooks.Open(SourcePath, 0, true, 5, Constants.Password, "", true,
                       Excel.XlPlatform.xlWindows, "\t", false, false, 0, false, 1, 0);
                OnWorkerComplete(prec += 10, LocalResource.QC3PARSE_PB_QC3_UNPROTECT);
                SourceWorkBook.Unprotect(Constants.Password);
            }
            catch (Exception ex)
            {
                OnWorkerComplete(100, LocalResource.QC3PARSE_PB_QC3OPEN_FAILED);
				_log.Error("QC3 to QC4 migration" + ex.Message + "\n" + ex.StackTrace);
				MessageDialog.ErrorOk(LocalResource.QC3PARSE_PB_QC3OPEN_FAILED);
            }
        }

        private Excel.Workbook OpenTemplate() // need to change util class
        {
            return SourceWorkBook.Application.Workbooks.Open(System.AppContext.BaseDirectory + "Templates\\" + Constants.TemplateFile.QC4_Template);
        }

        private long CopyValues(Excel.Worksheet source, Excel.Worksheet target, long sourceRowStart, long sourceColStart, long targetRowStart, long targetColStart, long lastRow = -1, long lastCol = -1)
        {
            source.Unprotect(Constants.Password);
            Excel.Range start = source.Cells[sourceRowStart, sourceColStart];
            if (lastRow == -1) lastRow = start.EntireRow.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing).Row;
            if (lastCol == -1) lastCol = start.EntireColumn.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing).Column;
            Excel.Range end = source.Cells[lastRow, lastCol];
            Excel.Range r = source.get_Range(start, end);
            Excel.Range targetRange = target.Cells[targetRowStart, targetColStart].Resize[r.Rows.Count, r.Columns.Count];
            targetRange.Value = r.Value;
            return lastCol;
        }

        #region ListSheetUpdateFunctions
        private void UpdateListSheet(List<QDataDetail> qData)
        {
            Excel.Worksheet list = ExcelUtil.GetWorkSheetByCodeName(TargetWorkBook, Constants.SheetCodeName.List);
            qData.RemoveAll(q => !q.isFound);

            int max = qData.Count();
            for (int i = 0; i < max; i++)
            {
                UpdateListVariable(qData[i].variableName, qData[i].answerType);
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
            Excel.Worksheet listSheet = TargetWorkBook.Worksheets[Constants.SheetType.sh_ListView];
            //listSheet.Unprotect(Password);
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

            listSheet.get_Range("A2", "J" + (allDArray.Count + 1)).Value = outPutArray;
        }

        private static void ListOutPutAdd(String[,] outPutArray, ArrayList list, int n)
        {
            for (int i = 0; i < list.Count; i++)
            {
                outPutArray[i, n] = list[i].ToString();
            }
        }
        #endregion

        private void OnWorkerMethodComplete(double value, string status)
        {
            progress.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
            new Action(
            delegate ()
            {
                progress.UpdateProgressBar(value, status);
            }
            ));
        }

        public void UpdateProgressBar(double value, string status)
        {
            OnWorkerMethodComplete(value, status);
        }

        internal class QDataDetail
        {
            internal string variableName;
            internal int sheetIndex;
            internal int columnIndex;
            internal bool isFound;
            internal int questionOrder;
            internal int categoryCount;
            internal string answerType;


            internal QDataDetail(string variableName)
            {
                this.variableName = variableName;
                this.questionOrder = -1;
                this.sheetIndex = -1;
                this.columnIndex = -1;
            }

			internal QDataDetail(string variableName, string answerType, int categoryCount)
			{
				this.variableName = variableName;
				this.answerType = answerType;
				this.categoryCount = categoryCount;
				this.questionOrder = -1;
				this.sheetIndex = -1;
				this.columnIndex = -1;
			}

			internal QDataDetail(string variableName, int sheetIndex, int columnIndex, bool isFound)
            {
                this.variableName = variableName;
                this.sheetIndex = sheetIndex;
                this.columnIndex = columnIndex;
                this.isFound = isFound;
            }
        }

        public List<QDataDetail> GetQDataDetail(out List<Excel.Worksheet> dataSheets, out List<int> dataSheetMaxCol, bool validate = true)
        {
            Excel.Range startRange = questionSettingSheet.Range[Constants.QS.StartVariableCell];
            Excel.Range endRange = ExcelUtil.EndxlUp(startRange);
			endRange = endRange.Offset[0, 2];
            Excel.Range tRange = questionSettingSheet.get_Range(startRange, endRange);
            Object[,] qsVarAry = tRange.Value2;
            dataSheets = null;
            dataSheetMaxCol = null;
            List<QDataDetail> qsDetail = new List<QDataDetail>();
			if (validate)
			{
				for (int i = 1; i <= qsVarAry.GetLength(0); i++)
				{
					if (null == qsVarAry[i, 1])
					{
						continue;
					}
					if (!QC4Common.Util.QSUtil.ValidateVariable(qsVarAry[i, 1].ToString(), out string variable))
					{
						return null;
					}
					qsDetail.Add(new QDataDetail(qsVarAry[i, 1].ToString()));
				}
			}
			else
			{
				for (int i = 1; i <= qsVarAry.GetLength(0); i++)
				{
					qsDetail.Add(new QDataDetail(qsVarAry[i, 1].ToString(), qsVarAry[i, 2].ToString(), qsVarAry[i, 3] == null ? 0 : (String.IsNullOrEmpty(qsVarAry[i, 3].ToString()) ? 0 : Convert.ToInt32(qsVarAry[i, 3]))));
				}
			}

			//check data afterprocess sheet found or not
			if (validate)
			{
				dataSheets = QCWorkbookHelper.GetDataAfterProcessSheets(SourceWorkBook);
			}

            if (null == dataSheets || dataSheets.Count == 0)
            {
                dataSheets = QCWorkbookHelper.GetDataSheets(SourceWorkBook);
            }

            //List<Excel.Worksheet> multivariateSheets = QCWorkbookHelper.GetMultivariateSheet(SourceWorkBook);
            //if (multivariateSheets.Count > 0)
            //{
            //	dataSheets.AddRange(multivariateSheets);
            //}

            List<List<String>> dsVarName = new List<List<String>>();
            dataSheetMaxCol = new List<int>();
            foreach (Excel.Worksheet worksheet in dataSheets)
            {
                Excel.Range dStart = worksheet.Range[Constants.DataSheet.HeaderStartCell];
                Excel.Range dEnd = dStart.End[Excel.XlDirection.xlToRight];
                Excel.Range dTotal = worksheet.get_Range(dStart, dEnd);
                Object[,] objAry = dTotal.Value2;
                List<String> nameAry = new List<string>();

                for (int i = 1; i <= objAry.GetLength(1); i++)
                {
                    nameAry.Add(objAry[1, i] == null ? "" : objAry[1, i].ToString());
                }
                dataSheetMaxCol.Add(dEnd.Column);
                dsVarName.Add(nameAry);
            }

            int order = 0;
            for (int i = 0; i < qsDetail.Count(); i++)
            {
                for (int j = 0; j < dsVarName.Count(); j++)
                {
                    int index = dsVarName[j].IndexOf(qsDetail[i].variableName);
                    if (index != -1)
                    {
                        qsDetail[i].isFound = true;
                        qsDetail[i].sheetIndex = j;
                        qsDetail[i].columnIndex = index;
                        qsDetail[i].questionOrder = order++;
                        break;
                    }
                }
            }
            return qsDetail;
        }

        private void UpdateQS(List<QDataDetail> qData)
        {
            Excel.Worksheet sourceSettingSheet = ExcelUtil.GetWorkSheetByCodeName(SourceWorkBook, Constants.SheetCodeName.Setting);
            Excel.Worksheet targetSettingSheet = ExcelUtil.GetWorkSheetByCodeName(TargetWorkBook, Constants.SheetCodeName.Setting);
            Excel.Range titleRangeAfter = sourceSettingSheet.Range["D2"];
            Excel.Range titleRangeBefore = sourceSettingSheet.Range["E2"];

			Excel.Worksheet tQs = ExcelUtil.GetWorkSheetByCodeName(TargetWorkBook, Constants.SheetCodeName.QuestionSetting); //TargetWorkBook.Worksheets[1];
            questionSettingSheet.Unprotect(Constants.Password);
            tQs.Cells[2, 12].Value = titleRangeAfter.Value;//questionSettingSheet.Rectangles("Text_Title").Text;
            targetSettingSheet.Range["F2"].Value = titleRangeAfter.Value;
            targetSettingSheet.Range["E2"].Value = titleRangeBefore.Value;


            //isNewFile for check count and count base,... columns are there or not is qc3 file
            bool isNewFile = true;
            int maxCol = 1038;
            int sourceMaxCol = 238;
            Excel.Range qsHeadCount = questionSettingSheet.Range["HG3"];

            if (String.IsNullOrEmpty(qsHeadCount.Text))
            {
                isNewFile = false;
                maxCol -= 24;
                sourceMaxCol -= 24;
            }

            Excel.Range startRange = questionSettingSheet.Range[Constants.QS.StartVariableCell];
            Excel.Range endRange = ExcelUtil.EndxlUp(startRange);
            Excel.Range lastCcount = endRange.Offset[0, 2];
            int extraRow = 0;
            if (Int32.TryParse(lastCcount.Text, out int c))
            {
                extraRow = c > 400 ? 2 : c > 200 ? 1 : 0;
            }

            startRange = questionSettingSheet.Range["A4"];
            endRange = questionSettingSheet.Cells[endRange.Row + extraRow, sourceMaxCol];
            Excel.Range tRange = questionSettingSheet.get_Range(startRange, endRange);

            int maxRow = qData.Count();// 0;
            maxRow = qData.Where(q => q.isFound).Count();

            int[] categoryCounts = new int[maxRow];
            int[] counts = new int[maxRow];

            Object[,] objAry = tRange.Value;
            Object[,] qsAry = new Object[maxRow, maxCol];

            int index = 0;
            int qDataIndex = -1;
            int aryLength = objAry.GetLength(0);
            for (int i = 1; i <= aryLength; i++)
            {
                if (!qData[++qDataIndex].isFound
                    || null == objAry[i, 6]
                    || string.IsNullOrEmpty(objAry[i, 6].ToString()))
                {
                    continue;
                }

                qsAry[index, Constants.QS.QsColNew - 1] = "Org";
                qsAry[index, Constants.QS.QsColQuestionNumber - 1] = String.IsNullOrEmpty(Convert.ToString(objAry[i, 3])) ?null : SetSpace(objAry[i, 3]);
                qsAry[index, Constants.QS.QsColQuestionType - 1] =String .IsNullOrEmpty(Convert.ToString(objAry[i, 4]))?null:objAry[i,4];
                qsAry[index, Constants.QS.QsColNumberOfQuestion - 1] = objAry[i, 5];
                qsAry[index, Constants.QS.QsColItem - 1] = ConvertDoubleQuote(objAry[i, 6]);
                qsAry[index, Constants.QS.QsColAnswerType - 1] = objAry[i, 7];
                qsAry[index, Constants.QS.QsColCategories - 1] = objAry[i, 8];
                qsAry[index, Constants.QS.QsColWT - 1] = String.IsNullOrEmpty(Convert.ToString(objAry[i, 9])) ? null : objAry[i, 9];
                qsAry[index, Constants.QS.QsColSortDisplay - 1] = objAry[i, 10];
                qsAry[index, Constants.QS.QsColTableHeading - 1] = String.IsNullOrEmpty(Convert.ToString(ConvertDoubleQuote(objAry[i, 12]))) ? null : SetSpace(ConvertDoubleQuote(objAry[i, 12]));
                qsAry[index, Constants.QS.QsColQuestion - 1] = SetSpace(ConvertDoubleQuote(objAry[i, 13]));

                int categoryCount = qsAry[index, Constants.QS.QsColCategories - 1] == null ? 0 : String.IsNullOrEmpty(qsAry[index, Constants.QS.QsColCategories - 1].ToString()) ? 0 : Convert.ToInt32(qsAry[index, Constants.QS.QsColCategories - 1]);
                qData[qDataIndex].categoryCount = categoryCount;
                qData[qDataIndex].answerType = objAry[i, 7].ToString();
                categoryCounts[index] = categoryCount;

                int maxCount = categoryCount / 200;
                if (categoryCount % 200 != 0)
                {
                    maxCount++;
                }

                int colOffset = Constants.QS.QsColChoiceBegin - 1;
                for (int j = 0; j < maxCount; j++)
                {
                    int max = (200 * (1 + j));
                    if (max > categoryCount)
                    {
                        max = categoryCount;
                    }
                    max = max - (200 * j);
                    for (int k = 0; k < max; k++)
                    {
                        qsAry[index, colOffset++] = SetSpace(ConvertDoubleQuote(objAry[i + j, 14 + k]));
                    }
                }


                if (isNewFile)
                {
                    int qc3ColCount = 215;
                    int qc3ColNumber = 218;
                    colOffset = Constants.QS.QsColCount - 1;

                    int count = objAry[i, qc3ColNumber] == null ? 0 : string.IsNullOrEmpty(objAry[i, qc3ColNumber].ToString()) ? 0 : Convert.ToInt32(objAry[i, qc3ColNumber].ToString());
                    counts[index] = count;

                    count *= 2;// for criteria 1 - 10 and ...
                    count += 4;//for count, countbase,...

                    for (int j = 0; j < count; j++)
                    {
                        qsAry[index, colOffset++] = String.IsNullOrEmpty(Convert.ToString(SetSpace(ConvertDoubleQuote(objAry[i, qc3ColCount + j])))) ? null : SetSpace(ConvertDoubleQuote(objAry[i, qc3ColCount + j]));
                    }
                }

                //to add extra row if more then 200 chocies
                if (maxCount > 1) i += (maxCount - 1);
                index++;
            }

			Excel.XlCalculation xlCalculation = TargetWorkBook.Application.Calculation;
			TargetWorkBook.Application.Calculation = Excel.XlCalculation.xlCalculationManual;
			Excel.Range range = tQs.Range["A4"];
            range = range.Resize[qsAry.GetLength(0), qsAry.GetLength(1)];
            range.Value2 = qsAry;

            Excel.Worksheet qsB = ExcelUtil.GetWorkSheetByCodeName(TargetWorkBook, Constants.SheetCodeName.QuestionSettingB);
            Excel.Range qsBRange = qsB.Range["A4"];
            qsBRange = qsBRange.Resize[qsAry.GetLength(0), qsAry.GetLength(1)];
            qsBRange.Value = qsAry;

            //setting background color and row name
            startRange = tQs.Cells[Constants.QS.StartRow, Constants.QS.ColChoiceBegin];

            index = 0;
            qData.RemoveAll(q => !q.isFound);
            tRange = startRange.Resize[qData.Count(), 1];
            foreach (Excel.Range r in tRange)
            {
                QC4Common.Util.QSUtil.SetRowName(tQs, r.EntireRow, qData[index].questionOrder);
                if (categoryCounts[index] > 0)
                {
                    r.Resize[1, categoryCounts[index]].Interior.Color = Constants.Color.LightGrey;
                }

                if (counts[index] > 0)
                {
                    range = tQs.Cells[r.Row, Constants.QS.QsColSubtotal1];
                    range.Resize[1, counts[index] * 2].Interior.Color = Constants.Color.LightGrey;
                }
                index++;
            }
			TargetWorkBook.Application.Calculation = xlCalculation;
			QuestionSettingDao qDao = new QuestionSettingDao(DBHelper.GetConnectionString(TempPath + "\\" + Constants.TemplateFile.DB_FIlE));
            qDao.CreateQuestion();
            int maxQ = qData.Count();
            for(int i =0; i < maxQ; i++)
            {
                qData[i].variableName = qData[i].variableName;//.Replace("\"\"", "\"");
            }
            qDao.InsertQuestions(qData);
        }

        private object SetSpace(object v)
        {
            if (v != null && v.ToString() != "" &&(v.ToString()[0]=='\''|| v.ToString()[0] == '’'))
                return " "+v.ToString();
            return v;
        }

        private string ConvertDoubleQuoteFAData(Object obj)
        {
            if (obj == null)
            {
                return "";
            }
            string data = obj.ToString();//.Replace("\"\"", "\"");
            if (data.Length > 0 && (data[0] == '\''|| data[0] == '’'))
                data = " " + data;
            return data;
        }

        private string ConvertDoubleQuote(Object obj)
		{
			if (obj == null)
			{
				return "";
			}
            return obj.ToString();//.Replace("\"\"", "\"");
        }

		public void DataUpdation(List<Excel.Worksheet> dataSheets, List<QDataDetail> qData, List<int> dataSheetMaxCol, bool pbUpdate = true)
        {
            Excel.Range dStart = dataSheets[0].Range["A4"];
            Excel.Range dEnd = ExcelUtil.EndxlUp(dStart);
            qData.RemoveAll(q => !q.isFound);
            int maxRow = dEnd.Row;
            int maxCol = qData.Count();

            double pbVal = (double)45 / maxRow;
            int max = (maxRow-4) / maxRowCount;
            if (maxRow % maxRowCount != 0) max++;

            int rowStart = 4;
            int rowEnd = 4;
			
            DataSheetDao dataDao = new DataSheetDao(DBHelper.GetConnectionString(TempPath + "\\" + Constants.TemplateFile.DB_FIlE));
            dataDao.CreateAnswer(qData);
			if (maxRow < rowStart)
			{
				return;
			}

			for (int i = 0; i < max; i++)
            {
                rowEnd += maxRowCount;
                if (rowEnd > maxRow)
                {
                    rowEnd = maxRow;
                }

                List<Object[,]> dataObj = new List<Object[,]>();
                for (int j = 0; j < dataSheets.Count; j++)
                {
                    Excel.Range s = dataSheets[j].Cells[rowStart, 1];
                    Excel.Range e = dataSheets[j].Cells[rowEnd, dataSheetMaxCol[j]];
                    Excel.Range t = dataSheets[j].get_Range(s, e);
                    dataObj.Add(t.Value);
                }

                Object[,] dataAry = new Object[dataObj[0].GetLength(0), maxCol];
                int index = 0;
                foreach (QDataDetail data in qData)
                {
                    Object[,] ary = dataObj[data.sheetIndex];
                    int sourceCol = data.columnIndex;
                    int destCol = data.questionOrder;

                    switch (data.answerType)
                    {
                        case Constants.AnswerType.MA:
                            for (int j = 1; j <= ary.GetLength(0); j++)
                            {
                                dataAry[j - 1, destCol] = null == ary[j, sourceCol + 1] ? "" : GenerateMaAnswer(ary[j, sourceCol + 1].ToString(), data.categoryCount);
                            }
                            break;
						case Constants.AnswerType.FA:
							for (int j = 1; j <= ary.GetLength(0); j++)
							{
								dataAry[j - 1, destCol] = null == ary[j, sourceCol + 1] ? "" : ConvertDoubleQuoteFAData(ary[j, sourceCol + 1]);
							}
							break;
						case Constants.AnswerType.D:
                            for (int j = 1; j <= ary.GetLength(0); j++)
                            {
                                try
                                {
                                    if (ary[j, sourceCol + 1] is DateTime)
                                        dataAry[j - 1, destCol] = null == ary[j, sourceCol + 1] ? "" : ((DateTime)ary[j, sourceCol + 1]).ToString("yyyy/MM/dd HH:mm:ss");
                                    else
                                        dataAry[j - 1, destCol] = null == ary[j, sourceCol + 1] ? "" : DateTime.FromOADate(Convert.ToDouble(ary[j, sourceCol + 1])).ToString("yyyy/MM/dd HH:mm:ss");
                                }
                                catch
                                {
                                    dataAry[j - 1, destCol] = ary[j, sourceCol + 1];
                                }
                            }
                            break;
                        default:
                            for (int j = 1; j <= ary.GetLength(0); j++)
                            {
                                dataAry[j - 1, destCol] = ary[j, sourceCol + 1];
                            }
                            break;
                    }
                    index++;
                }
                dataDao.InsertData(dataAry, qData, this, pbVal, pbUpdate);
                rowStart = rowEnd + 1;
            }
        }

        private void UpdateSettings(List<QDataDetail> qData)
        {
            Excel.Worksheet sourceAdvSetting = ExcelUtil.GetWorkSheetByCodeName(SourceWorkBook, Constants.SheetCodeName.DetailsSetting);
            Excel.Range start = sourceAdvSetting.Range[Constants.AdvancedSetting.AdvSettingStartCell];
            Excel.Range end = ExcelUtil.EndxlUp(start);
            if (start.Row > end.Row)
            {
                return;
            }

            Excel.Range total = sourceAdvSetting.get_Range(start, end);

            Excel.Range wbCrCheck = total.Find("F_Cr_Cross_AddUp_Check_Summary_WeightBack_S", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
            Excel.Range wbCrCombo = total.Find("F_Cr_Cross_AddUp_Combo_Summary_WeightBack_S", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);

            bool isFound = false;
            string[,] valAry = new string[4, 2];
            valAry[0, 0] = "F_Cr_Cross_AddUp_Check_Summary_WeightBack_S";
            valAry[1, 0] = "F_Cr_Cross_AddUp_Combo_Summary_WeightBack_S";
            valAry[2, 0] = "F_Gt_GT_AddUp_Check_Summary_WeightBack_S";
            valAry[3, 0] = "F_Gt_GT_AddUp_Combo_Summary_WeightBack_S";

            if (wbCrCheck != null && null != wbCrCombo)
            {
                valAry[0, 1] = wbCrCheck.Offset[0, 1].Text;
                if (valAry[0, 1] != "FALSE")
                {
                    valAry[1, 1] = wbCrCombo.Offset[0, 1].Text;
                    if (valAry[1, 1] == "WeightBack")
                    {
                        valAry[1, 1] = "";
                        valAry[0, 1] = "FALSE";
                        isFound = true;
                    }
                    else if (!qData.Any(x => x.variableName == wbCrCombo.Offset[0, 1].Text))
                    {
                        valAry[1, 1] = "";
                    }
                    else
                        isFound = true;

                    valAry[2, 1] = valAry[0, 1];
                    valAry[3, 1] = valAry[1, 1];
                }
            }

            if (!isFound)
            {
                Excel.Range wbGtCheck = total.Find("F_Gt_GT_AddUp_Check_Summary_WeightBack_S", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                        Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
                Excel.Range wbGtCombo = total.Find("F_Gt_GT_AddUp_Combo_Summary_WeightBack_S", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                        Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);

                valAry[0, 0] = "F_Gt_GT_AddUp_Check_Summary_WeightBack_S";
                valAry[1, 0] = "F_Gt_GT_AddUp_Combo_Summary_WeightBack_S";
                valAry[2, 0] = "F_Cr_Cross_AddUp_Check_Summary_WeightBack_S";
                valAry[3, 0] = "F_Cr_Cross_AddUp_Combo_Summary_WeightBack_S";
                if (wbGtCheck != null && null != wbGtCombo)
                {
                    valAry[0, 1] = wbGtCheck.Offset[0, 1].Text;
                    if (valAry[0, 1] != "FALSE")
                    {
                        valAry[1, 1] = wbGtCombo.Offset[0, 1].Text;

                        if (valAry[1, 1] == "WeightBack")
                        {
                            valAry[1, 1] = "";
                            valAry[0, 1] = "FALSE";
                        }
                        else if (!qData.Any(x => x.variableName == wbGtCombo.Offset[0, 1].Text))
                        {
                            valAry[1, 1] = "";
                        }

                        valAry[2, 1] = valAry[0, 1];
                        valAry[3, 1] = valAry[1, 1];
                    }
                }
            }

            Excel.Worksheet targetAdvSetting = ExcelUtil.GetWorkSheetByCodeName(TargetWorkBook, Constants.SheetCodeName.DetailsSetting);
            start = targetAdvSetting.Range[Constants.AdvancedSetting.AdvSettingStartCell];
            start.Resize[4, 2].Value2 = valAry;

            #region update headings
            Excel.Range setting = targetAdvSetting.Range["A1"].EntireColumn.Find("F_Cr_Cross_AddUp_Text_Summary_Change_Hyosoku_P", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
            if (setting != null)
                setting = targetAdvSetting.Range["A1"].EntireColumn.Find("F_Cr_Cross_AddUp_Text_Summary_Change_Hyosoku_P", setting, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
        Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
            if (setting != null)
                setting.Offset[0, 1].Value = QC4Common.Common.Constants.CRLFchar;

            setting = targetAdvSetting.Range["A1"].EntireColumn.Find("F_Cr_Cross_AddUp_Text_Summary_Change_Non_P", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
            if (setting != null)
                setting = targetAdvSetting.Range["A1"].EntireColumn.Find("F_Cr_Cross_AddUp_Text_Summary_Change_Non_P", setting, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
            if (setting != null)
                setting.Offset[0, 1].Value = QC4Common.Common.Constants.CRLFchar;

            setting = targetAdvSetting.Range["A1"].EntireColumn.Find("F_Cr_Cross_AddUp_Text_Summary_Change_Hyoutou_P", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
            if (setting != null)
                setting = targetAdvSetting.Range["A1"].EntireColumn.Find("F_Cr_Cross_AddUp_Text_Summary_Change_Hyoutou_P", setting, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
            if (setting != null)
                setting.Offset[0, 1].Value = QC4Common.Common.Constants.CRLFchar;

            setting = targetAdvSetting.Range["A1"].EntireColumn.Find("F_Gt_GT_AddUp_Text_Summary_Change_Hyoutou_P", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
            if (setting != null)
                setting.Offset[0, 1].Value = QC4Common.Common.Constants.CRLFchar;

            setting = targetAdvSetting.Range["A1"].EntireColumn.Find("F_Gt_GT_AddUp_Text_Summary_Change_Non_P", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
            if (setting != null)
                setting.Offset[0, 1].Value = QC4Common.Common.Constants.CRLFchar;

            #endregion
        }

        public string GenerateMaAnswer(string ans, int count)
        {
            if ("*".Equals(ans) || string.Empty.Equals(ans))
            {
                return ans;
            }
            var str = ans.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            char[] cArray = Enumerable.Repeat('0', count).ToArray();
            foreach (string s in str)
            {
                try
                {
                    cArray[count - Convert.ToInt32(s)] = '1';
                }
                catch { }
            }
            return new string(cArray);
        }

        private void UpdateGTSheet()
        {
            try
            {
                Excel.Worksheet gtSheet = ExcelUtil.GetWorkSheetByCodeName(TargetWorkBook, Constants.SheetCodeName.GTTabulation);
                //QC4Common.Common.GTAutoSetting.ExcelSet(gtSheet.Application);
                QC4Common.Common.GTAutoSetting.FNCGTAutoSettingMainIni(gtSheet);
                QC4Common.Common.GTAutoSetting.FNCGetQuesData(gtSheet, null, ExcelUtil.GetWorkSheetByCodeName(TargetWorkBook, Constants.SheetCodeName.QuestionSetting));
                QC4Common.Common.GTAutoSetting.LoadDefaultDataToGTHiddenSheet(gtSheet);
                //QC4Common.Common.GTAutoSetting.ExcelReset(gtSheet.Application);
            }
            catch (Exception ex)
            {
				Console.WriteLine(ex.Message);
			}
        }

        /// <summary>
        /// Variable listing in cross and summary sheets
        /// </summary>
        private void UpdateSheetCS(List<QDataDetail> qData)
        {
            const string crossStartCel = "B14";

            qData.RemoveAll(q => !q.isFound);
            int max = qData.Count();
            Object[,] variableAry = new Object[max - 1 , 4];
            int index = 0;
            for (int i = 1; i < max; i++)
            {
                if (qData[i].answerType == Constants.AnswerType.D || qData[i].answerType == Constants.AnswerType.FA)
                {
                    continue;
                }
                variableAry[index, 1] = qData[i].variableName;
                variableAry[index, 2] = qData[i].answerType;
				if (qData[i].categoryCount != 0)
				{
					variableAry[index, 3] = qData[i].categoryCount;
				}
				//variableAry[index, 3] = qData[i].categoryCount == 0 ? null : qData[i].categoryCount;
                variableAry[index, 0] = ++index;
            }
            VariableUpdation(Constants.SheetCodeName.CrossTabulation, crossStartCel, variableAry);
        }

        private void VariableUpdation(string codeName, string startCell, Object[,] varAry)
        {
            Excel.Worksheet sheet = ExcelUtil.GetWorkSheetByCodeName(TargetWorkBook, codeName);
            Excel.Range range = sheet.Range[startCell].Resize[varAry.GetLength(0), varAry.GetLength(1)];
            range.Value = varAry;
        }

        public void ProgressUpdate(double value, string message = "")
        {
            OnWorkerComplete(value, message);
        }
    }
}
