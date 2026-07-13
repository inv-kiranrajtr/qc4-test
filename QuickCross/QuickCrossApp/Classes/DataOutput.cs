using ExcelAddIn.Sheets;
using log4net;
using Macromill.QCWeb.Exceptions;
using Macromill.QCWeb.Logic.TabulationEx.Criteria;
using Macromill.QCWeb.Question;
using Macromill.QCWeb.Tabulation;
using QC4Common.Model;
using Qc4Launcher.DB;
using Qc4Launcher.Model;
using Qc4Launcher.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using ProgressBar = Qc4Launcher.Util.ProgressBar;
using Vb = Microsoft.VisualBasic;

namespace Qc4Launcher.Classes
{
	class DataOutput : ProgressBar
	{
		private const string SheetVerticalLayout = "Layout_Tate";
		private const string SheetHorizontalLayout = "Layout_Yoko";
		private const string SheetName = "Layout";
		private const string SpssSyntaxHeader = "****************.\r\n* [TO IMPORT QuickCross DATA TO SPSS] .\r\n* - Open this syntax file on SPSS .\r\n* - From the syntax window menu, select [Run]-[All] .\r\n\r\n* [BEFORE YOU EXECUTE THIS SYNTAX FILE] .\r\n* - Close all files other than this syntax .\r\n* - From the SPSS menu, select [Edit]-[Options]-[Language] .\r\n* - Asure that [Character Encoding for Data and Syntax] is set to %%CharacterEncoding%% .\r\n* - IF NOT, change the setting manualy or execute the SET command below .\r\n\r\nSET Unicode=%%UnicodeMode%%.\r\nSHOW LOCALE UNICODE.\r\n";
		private const string SpssSyntaxRule = "****************.\r\n* The following contents will be changed in output files, conforming the SPSS rules .\r\n%%Rules%%\r\n\r\n";
		private const string SpssSyntaxVarLabel = ".\r\n\r\n****************.\r\n* Set Variable Labels .\r\nVAR LABELS ";
		private const string SpssSyntaxValLabel = "\r\n.\r\n\r\n****************.\r\n* Set Value Labels .\r\nVALUE LABELS ";
		private const string SpssSyntaxVariables = "****************.\r\n* Import rawdata to SPSS .\r\n\r\nGET DATA \r\n/ TYPE = TXT\r\n/ FILE = !FILENAME + '.tsv'\r\n/ ARRANGEMENT = DELIMITED\r\n/ FIRSTCASE = 3\r\n/ DELCASE = LINE\r\n/ DELIMITERS = '\\t'\r\n/ QUALIFIER = \"'\"\r\n/ VARIABLES =%%Variables%%\r\n.\r\nDATASET NAME imported WINDOW = FRONT.\r\nALTER TYPE ALL (A=AMIN).\r\nVARIABLE LEVEL\r\n	ALL	(NOMINAL)";
		private const string SpssSyntaxFileName = "****************.\r\n* Set file-name .\r\n\r\nDEFINE !FILENAME()\r\n	'%%FileName%%'\r\n!ENDDEFINE.\r\n\r\nCD	'%%FilePath%%' .\r\n";
		private const string SpssSyntaxFooter = "\r\n****************.\r\n* Save files .\r\nDISPLAY DICTIONARY.\r\nSAVE OUTFILE = !FILENAME + '_Imported.sav' /COMPRESSED.\r\nOUTPUT SAVE OUTFILE =  !FILENAME + '_Log.spv' .";
		private const string SpssFileNameRawData = "_rawdataSpss.tsv";
		private const string SpssFileNameSyntax = "_rawdataSpssSyntax.sps";
		private const string QLayoutFileName = "_Qlayout.csv";
		private const string QRawDataFileName = "_Qrawdata.tsv";

		private Excel.Workbook Workbook { get; set; }
		private Excel.Workbook SourceWorkbook { get; set; }
		private Excel.Worksheet SettingSheet { get; set; }
		private string TargetPath { get; set; }
		private List<QuestionSettings> UsedQuestion { get; set; }
		private QuestionSettings DivisionQuestion { get; set; }
		private Object[,] SettingList { get; set; }
		private DB.DataOutput OutputDao { get; set; }
		private AdvanceSettingHelper AsHelper { get; set; }
		private Constants.DataOutput.FileType FileType { get; set; }
		private bool VerticalFlag { get; set; }
		private String C1 { get; set; }
		private String C2 { get; set; }
		private Macromill.QCWeb.ReportRequest.OutputDataType OutputDataType { get; set; }
		private Logger.Log log;
		private String Extension { get; set; }
		private bool Unicode { get; set; }
		private string SpssMALabel { get; set; }
		private ProgressBar Progress { get; set; }
		private List<FilterSettingsCr> FilterSettings { get; set; }
		private string TableName { get; set; }
		internal static bool isSuccess = true;
		private MainWindow mainWindow;
		DataExportQcFile dataExportQcFile = null;

		System.Windows.Window window;
		private readonly ILog _log;

		internal DataOutput(Excel.Workbook book, string targetPath, String fileType, bool verticalFlag, String outputFormat
			, String c1, String c2, string maLabel, bool spssUniode, string divVariable = "", string[] variables = null
			, List<FilterSettingsCr> filterSettingsCrs = null)
		{
            string extension;
			FileType = GetFileType(fileType, out extension);
			Extension = extension;
			SourceWorkbook = book;
			SettingSheet = ExcelUtil.GetWorkSheetByCodeName(book, Constants.SheetCodeName.Setting);
			OutputDao = new DB.DataOutput(SourceWorkbook);
			AsHelper = new AdvanceSettingHelper(SourceWorkbook);
			
			UsedQuestion = AsHelper.GetSelectedVariables(variables,FileType);
            try
            {
                Constants.MaxRowCount = (int)(10000 * ((float)300 / UsedQuestion.Count()));
            }
            catch { }
            DivisionQuestion = Definiotion.VariableDictionary.ContainsKey(divVariable) ? Definiotion.VariableDictionary[divVariable] : null;
			SettingList = AsHelper.GetSettingList();
			TargetPath = targetPath;
			SpssMALabel = maLabel;
			log = new Logger.Log();
			VerticalFlag = verticalFlag;
			Unicode = spssUniode;
			C1 = LocalResource.LABEL_BLANK == c1 ? "" : c1;
			C2 = LocalResource.LABEL_BLANK == c2 ? "" : c2;
			FilterSettings = filterSettingsCrs;
			TableName = "answers";
			if (DBHelper.checkAfterProcess(book))
			{
				TableName = "data_after_process";
			}
			OutputDataType = GetOutputType(outputFormat);
			isSuccess = true;
			_log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		}
		string Message = "";
		internal void OutputMainStart(MainWindow tab,Forms.TabExportFilterSetting form)
        {
            window = form;
            mainWindow = tab;
            InitProgressBar(false, form);
			new Thread(() => OutMainRes(null)).Start();
			progress.ShowDialog();
			if (isSuccess)
			{
				form.Close();
			}
			else
				form.Activate();
			if (Message != "")
				MessageDialog.ErrorOk(Message);
			else if (dataExportQcFile != null && dataExportQcFile.WMessage != "")
				MessageDialog.Warning(dataExportQcFile.WMessage, mainWindow);
		}

		internal void OutMainRes(ProgressBar pb)
		{
			Progress = this;
			if (!OutputMain())
			{
				Progress.UpdateProgressBar(100, LocalResource.EX_PROGRESSBAR_EXPORT_FAILED);
				MessageDialog.ErrorOk(LocalResource.EX_EXPORT_FAILED);
			}
			Constants.MaxRowCount = Constants.MaxRowLimit;
		}

		internal bool OutputMain()
		{
			try
			{
				UpdateProgressBar(++Progress.percentage, LocalResource.EX_PROGRESSBAR_INITIALIZE);
				if (OutputDataType == Macromill.QCWeb.ReportRequest.OutputDataType.QC3)
				{
					return false;
				}
				if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(TargetPath + ".txt")))
				{
					Message = LocalResource.EX_PATH_NOT_FOUND;
					Progress.UpdateProgressBar(100, LocalResource.EX_PROGRESSBAR_EXPORT_FAILED);
					return true;
				}

				string TemplatePath = ExcelUtil.GetTemplatePath(Constants.TemplateFile.DATA_OUTPUT);
				if (UsedQuestion.Count() < 1)
				{
					return false;
				}
				if (!UsedQuestion[0].Variable.Equals("SAMPLEID", StringComparison.CurrentCultureIgnoreCase))
				{
					return false;
				}
				decimal[] ids = UsedQuestion.Select(a => a.Id).ToArray();
				if (ids.Count() == 0)
				{
					return false;
				}

				QC4Common.DB.DBHelper.CheckIfColumnExists(SourceWorkbook, UsedQuestion, out List<string> variables, out List<string> columns, out List<decimal> idss);
				if (variables.Count > 0)
				{
					Progress.UpdateProgressBar(100, LocalResource.EX_PROGRESSBAR_EXPORT_FAILED);
					MessageDialog.ErrorOk(String.Format(LocalResource.DO_ALERT_NO_DATA_FOUND, string.Join(",", variables)), mainWindow);
					return true;
				}

				Progress.UpdateProgressBar(Progress.percentage += 1, LocalResource.EX_PROGRESSBAR_LOADING);
				Questions questions = DictUpdate.GetQuestions(SourceWorkbook);
				string[,] layoutArray;
				Progress.UpdateProgressBar(Progress.percentage += 1, LocalResource.EX_PROGRESSBAR_PROCESSING);
				dataExportQcFile = new DataExportQcFile(this, FilterSettings, TableName, window, mainWindow);
				string[] layoutFile = null;
				string[] dataFile = null;
				bool fileCheck = false;
				SeparatedValuesBase sv = new SeparatedValuesBase();

				switch (FileType)
				{
					case Constants.DataOutput.FileType.CSV:
					case Constants.DataOutput.FileType.TAB:
						GetFileName(ref layoutFile, ref dataFile, "Layout", "001", Extension, Extension);
						layoutArray = VerticalFlag ? VerticalLayout(UsedQuestion, false, true, rawDataFileName: dataFile[0]) : HorizontalLayout(UsedQuestion, FileType, false, true, rawDataFileName: dataFile[0]);
                        string ext = FileType == Constants.DataOutput.FileType.CSV ? Constants.CsvExtension : Constants.TabExtension;
						string[] totalFiles = layoutFile.Union(dataFile).ToArray();
						fileCheck = FileCheckAndAlert(totalFiles);
						if (!fileCheck)
						{
							return true;
						}

						string sep = FileType == Constants.DataOutput.FileType.CSV ? "," : "\t";

						string[] layoutBuffer = sv.GetRawDataBuffer(sep, layoutArray, c1: C1, c2: C2);
						sv.Export(layoutFile[0], layoutBuffer[0], encoding: Constants.DataOutput.defaultEncoding, typ: FileType);

						if (DivisionQuestion != null)
						{
							for (int i = 1; i < layoutFile.Count(); i++)
							{
								System.IO.File.Copy(layoutFile[0], layoutFile[i]);
							}
						}
						dataExportQcFile.CsvTabMain(UsedQuestion, questions, TargetPath, this, FileType, layoutFile
							, dataFile, SourceWorkbook, C1, C2, OutputDataType, DivisionQuestion, null, isRawdata: true);
						break;
					case Constants.DataOutput.FileType.R2D3:
						GetFileName(ref layoutFile, ref dataFile, "R2D3Layout", "001", Extension, Extension);
						UsedQuestion = UsedQuestion.Where(d => d.Variable == "SAMPLEID" ||
											(d.AnswerType != Constants.AnswerType.D &&
											 d.AnswerType != Constants.AnswerType.N &&
											 d.AnswerType != Constants.AnswerType.FA))
							   .ToList();
						layoutArray = VerticalLayout(UsedQuestion, false, true, rawDataFileName: dataFile[0]);
						totalFiles = layoutFile.ToArray();
						fileCheck = FileCheckAndAlert(totalFiles);
						if (!fileCheck)
						{
							return true;
						}

						sep = ",";
						
						//logic for filling values in first 2 columns of csv + vertical layout output
						for (int i = 0; i < layoutArray.GetUpperBound(0); i++)
						{
							if (string.IsNullOrEmpty(layoutArray[i, 0]) && string.IsNullOrEmpty(layoutArray[i, 1]))
							{
								layoutArray[i, 0] = layoutArray[i, 2];
								switch (layoutArray[i, 4])
								{
									case Constants.AnswerType.SA:
										layoutArray[i, 1] = Constants.QuestionType.SAR;
										break;
									case Constants.AnswerType.MA:
										layoutArray[i, 1] = Constants.QuestionType.MAC;
										break;
									case Constants.AnswerType.FA:
									case Constants.AnswerType.N:
									case Constants.AnswerType.D:
										layoutArray[i, 1] = Constants.QuestionType.FAS;
										break;
								}

							}
						}
						layoutBuffer = sv.GetRawDataBuffer(sep, layoutArray, c1: C1, c2: C2);
						sv.Export(layoutFile[0], layoutBuffer[0], encoding: Constants.DataOutput.defaultEncoding, typ: FileType);

						if (DivisionQuestion != null)
						{
							for (int i = 1; i < layoutFile.Count(); i++)
							{
								System.IO.File.Copy(layoutFile[0], layoutFile[i]);
							}
						}
                        dataExportQcFile.CsvTabMain(UsedQuestion, questions, TargetPath, this, FileType, layoutFile
                            , dataFile, SourceWorkbook, C1, C2, OutputDataType, DivisionQuestion, null, isRawdata: true);
                        break;
					case Constants.DataOutput.FileType.Excel2007:
						dataFile = new string[DivisionQuestion == null ? 1 : DivisionQuestion.CategoryCount];
						if (DivisionQuestion == null)
						{
							dataFile[0] = TargetPath + Extension;
						}
						else
						{
							string dataPath = TargetPath + "_" + DivisionQuestion.Variable + "_";

							for (int i = 1; i <= DivisionQuestion.CategoryCount; i++)
							{
								dataFile[i - 1] = dataPath + i.ToString("0000") + Extension;
							}
						}
						if (!ValidatePath(dataFile))
						{
							Progress.UpdateProgressBar(100, LocalResource.EX_PROGRESSBAR_COMPLETED);
							Message = LocalResource.WRONG_FILEPATH;

							return true;
						}
						fileCheck = FileCheckAndAlert(dataFile);
						if (!fileCheck)
						{
							return true;
						}
						SourceWorkbook.Application.EnableEvents = false;
						SourceWorkbook.Application.DisplayAlerts = false;
						layoutArray = VerticalFlag ? VerticalLayout(UsedQuestion, fileType: Constants.DataOutput.FileType.Excel2007, headerFlag: true) : HorizontalLayout(UsedQuestion, FileType);
						string layoutSheet;
						string delLayoutSheet;

						if (VerticalFlag)
						{
							layoutSheet = SheetVerticalLayout;
							delLayoutSheet = SheetHorizontalLayout;
						}
						else
						{
							layoutSheet = SheetHorizontalLayout;
							delLayoutSheet = SheetVerticalLayout;
						}

						Workbook = ExcelUtil.OpenWorkbok(TemplatePath, SourceWorkbook.Application);
						Workbook.Application.EnableEvents = false;
						Excel.Worksheet sheet = Workbook.Worksheets[layoutSheet];
						Excel.Range rng = sheet.Range["A1", "ALU1"];
						rng.ColumnWidth = 9;
						Excel.Range start = sheet.Cells[1, 1];
						start = start.Resize[layoutArray.GetLength(0), layoutArray.GetLength(1)];
						start.Value = layoutArray;
						sheet.Name = SheetName;
						Excel.Worksheet delSheet = Workbook.Worksheets[delLayoutSheet];
						delSheet.Delete();
						Excel.Worksheet sheetData = null;
						int mode = (UsedQuestion.Count % 16384) > 0 ? 1 : 0;
						int sheetCount = (UsedQuestion.Count / 16384) + mode;

						int maxCol = 0;
						string[,] header;
						List<string[,]> headerAry = new List<string[,]>();
						if (OutputDataType != Macromill.QCWeb.ReportRequest.OutputDataType.Code)
						{
							for (int i = 0; i < UsedQuestion.Count; i++)
							{
								var x = UsedQuestion[i];
								if (x.AnswerType == Constants.AnswerType.MA)
								{
									maxCol += x.CategoryCount;
								}
								else
								{
									maxCol++;
								}
								if (maxCol > 16384)
								{
									maxCol = maxCol - 16384;
									header = new string[1, 16384];
									headerAry.Add(header);
								}
							}
							if (maxCol > 0)
							{
								header = new string[1, maxCol];
								headerAry.Add(header);
							}
						}
						else
						{
							maxCol = UsedQuestion.Count;
							for (int s = 0; s < sheetCount; s++)
							{
								if (maxCol > 16384)
								{
									header = new string[1, 16384];
									headerAry.Add(header);
									maxCol = maxCol - 16384;
								}
								else
								{
									header = new string[1, maxCol];
									headerAry.Add(header);
								}
							}
						}
						sheetCount = headerAry.Count;
						for (int i = 1; i <= sheetCount; i++)
						{
							sheetData = (Excel.Worksheet)Workbook.Worksheets.Add(After: Workbook.Worksheets[i]);
							sheetData.Name = "00" + i.ToString();
						}

						int index = 0;
						int headerSheet = 0;
						if (OutputDataType != Macromill.QCWeb.ReportRequest.OutputDataType.Code)
						{
							for (int i = 0; i < UsedQuestion.Count; i++)
							{
								var x = UsedQuestion[i];
								if (x.AnswerType == Constants.AnswerType.MA)
								{
									for (int j = 0; j < x.CategoryCount; j++)
									{
										if (index > 16383)
										{
											index = 0;
											headerSheet++;
										}
										headerAry[headerSheet][0, index++] = x.Variable + "_" + (j + 1);
									}
								}
								else
								{
									if (index > 16383)
									{
										index = 0;
										headerSheet++;
									}
									headerAry[headerSheet][0, index++] = x.Variable;
								}
							}
						}
						else
						{
							for (int i = 0; i < UsedQuestion.Count; i++)
							{
								if (index > 16383)
								{
									index = 0;
									headerSheet++;
								}
								headerAry[headerSheet][0, index++] = UsedQuestion[i].Variable;
							}
						}
						int sh = 0;
						foreach (Excel.Worksheet sht in Workbook.Worksheets)
						{
							if (sh > 0)
								sht.Cells[1, 1].Resize[1, headerAry[sh - 1].GetLength(1)].Value = headerAry[sh - 1];
							sh++;
						}
						foreach (Excel.Worksheet sht in Workbook.Worksheets)
							sht.Rows.Font.Name = QC4Common.Common.Constants.GlobalMode.Split(',')[1];
						Workbook.SaveAs(dataFile[0]);
						Workbook.Close();
						if (DivisionQuestion != null)
						{
							for (int i = 1; i < dataFile.Count(); i++)
							{
								System.IO.File.Copy(dataFile[0], dataFile[i]);
							}
						}
						Workbook = null;
						Message = dataExportQcFile.ExcelMain(UsedQuestion, questions, TargetPath, this, FileType
							, dataFile, SourceWorkbook, C1, C2, OutputDataType, DivisionQuestion);
						SourceWorkbook.Application.EnableEvents = true;
						SourceWorkbook.Application.DisplayAlerts = true;
						break;
					case Constants.DataOutput.FileType.SPSS:
						GetFileName(ref layoutFile, ref dataFile, "SpssSyntax", "Spss", ".sps", Constants.TabExtension);
						fileCheck = FileCheckAndAlertForSPSS(layoutFile.Union(dataFile).ToArray());
						if (!fileCheck)
							return true;
						bool isExport = SpssMain(UsedQuestion, SpssMALabel, questions, ids, layoutFile, dataFile, fileCheck, Unicode);
						if (isExport)
						{
							dataExportQcFile.GetRawDataArraySpss(questions, ids, Macromill.QCWeb.ReportRequest.OutputDataType.Flag
								, "", C1, "", null, null, null, SourceWorkbook, dataFile.ToList(), "", DivisionQuestion, UsedQuestion
								, false, TargetPath, Constants.DataOutput.FileType.SPSS, layoutFile);
						}
						else
						{
							return true;
						}

						break;
					case Constants.DataOutput.FileType.QLayout:
						string fPath = TargetPath;
						string fName = System.IO.Path.GetFileNameWithoutExtension(fPath);
						fName = fName.Replace("_rawdata", "");
						fName = fName.Replace("rawdata", "");
						fPath = System.IO.Path.GetDirectoryName(fPath) + "\\" + fName;
						var isPath = IsNetworkPath(fPath);
						if (isPath == false)
						{
							fPath = fPath.Replace("\\\\", "\\");
						}
						GetFileName(ref layoutFile, ref dataFile, "_Qlayout", "_Qrawdata", Constants.CsvExtension, Constants.TabExtension, fPath);
						fileCheck = FileCheckAndAlert(layoutFile.Union(dataFile).ToArray());
						if (!fileCheck)
						{
							return true;
						}

						QLayoutMain(UsedQuestion, questions.SurveyTitle, layoutFile, dataFile, out string msg);


						dataExportQcFile.GetRawDataArray(questions, ids, Macromill.QCWeb.ReportRequest.OutputDataType.Code
							, "", "", "*", null, null, null, SourceWorkbook, dataFile.ToList(), "", DivisionQuestion, UsedQuestion
							, false, TargetPath, Constants.DataOutput.FileType.QLayout, layoutFile, isRawdata: true);

						if (msg != "")
						{
							MessageDialog.Warning(string.Format(LocalResource.DO_ALERT_CHOICE_MORETHAN_600, msg));
						}
						break;
					case Constants.DataOutput.FileType.QC3:
					case Constants.DataOutput.FileType.QC4:
						string[] qcFiles = new string[DivisionQuestion == null ? 1 : DivisionQuestion.CategoryCount];
						ext = FileType == Constants.DataOutput.FileType.QC3 ? Constants.Qc3Extension : Constants.Qc4Extension;
						if (DivisionQuestion == null)
						{
							qcFiles[0] = TargetPath + ext;
						}
						else
						{
							string path = TargetPath + "_" + DivisionQuestion.Variable + "_";

							for (int i = 1; i <= DivisionQuestion.CategoryCount; i++)
							{
								qcFiles[i - 1] = path + i.ToString("0000") + ext;
							}
						}
						if (!ValidatePath(qcFiles))
						{
							Progress.UpdateProgressBar(100, LocalResource.EX_PROGRESSBAR_COMPLETED);
							Message = LocalResource.WRONG_FILEPATH;

							return true;
						}
						fileCheck = FileCheckAndAlert(qcFiles);
						if (!fileCheck)
						{
							return true;
						}
						string msgs = "";
						if (Constants.DataOutput.FileType.QC3 == FileType)
						{
							UsedQuestion = TrimQuestionDetail(UsedQuestion, out msgs);
						}

						dataExportQcFile.QcMain(UsedQuestion, questions, SourceWorkbook, TargetPath, this,
							Constants.DataOutput.FileType.QC4 == FileType ? true : false, DivisionQuestion);

						if (!String.IsNullOrEmpty(msgs))
						{
							MessageDialog.Warning(string.Format(LocalResource.DO_ALERT_CHOICE_MORETHAN_600, msgs));
						}
						break;
				}
				if (isSuccess && fileCheck && FilterSettings != null && FilterSettings.Count > 0)
				{
					Macromill.QCWeb.ReportRequest.Request req = new Macromill.QCWeb.ReportRequest.Request();
					string filterExp = CriteriaDescProvider.CreateCriteriaDescriptionsForLocalExp(FilterSettings, questions);
					string localizedFilteringExpression1 = CriteriaDescProvider.LocalizeFilteringExpression(filterExp, req, questions);

					string filename = TargetPath.Remove(TargetPath.LastIndexOf('\\')) + "\\Filter.txt";
					
					using (StreamWriter sw = new StreamWriter(filename))
					{
						sw.Write(localizedFilteringExpression1);
						sw.Close();
					}
				}
			}
			catch (Exception ex)
			{
				try
				{
					string message = ex.Message; 
					Message = LocalResource.EX_PROGRESSBAR_EXPORT_FAILED;

					if (ex.Message ==
						"The file could not be accessed. Try one of the following:\n\n• Make sure the specified folder exists. \n• Make sure the folder that contains the file is not read-only.\n• Make sure the filename and folder path do not contain any of the following characters:  <  >  ?  [  ]  :  | or  *\n• Make sure the filename and folder path do not contain more than 218 characters.")
					{
						Message = message = LocalResource.WRONG_FILEPATH;
					}
					else if(ex.GetType().IsAssignableFrom(typeof(System.OutOfMemoryException)))
					{
						Message = message = LocalResource.OUTOFMEMORY_EXCEPTION_MESSAGE;
					}

					_log.LogError(message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);

					SourceWorkbook.Application.EnableEvents = true;
					SourceWorkbook.Application.DisplayAlerts = true;
					if (null != Workbook)
						Workbook.Close();
				}
				catch { }

				Progress.UpdateProgressBar(100, LocalResource.EX_PROGRESSBAR_COMPLETED);
			}

			return true;
		}

        private bool ValidatePath(string[] qcFiles)
        {
            if (qcFiles != null)
                for (int i = 0; i < qcFiles.Length; i++)
                {
                    if (qcFiles[i] != null && qcFiles[i].IndexOfAny(new char[] { '<', '>', '[', ']', '*', '|', '?' }) != -1)
                        return false;
                }
            return true;
        }

        private bool IsNetworkPath(string path)
        {
            if (!path.StartsWith(@"/") && !path.StartsWith(@"\"))
            {
                string rootPath = System.IO.Path.GetPathRoot(path); // get drive's letter
                System.IO.DriveInfo driveInfo = new System.IO.DriveInfo(rootPath); // get info about the drive
                return driveInfo.DriveType == DriveType.Network; // return true if a network drive
            }

            return true; // is a UNC path
        }

        private string[,] VerticalLayout(List<QuestionSettings> S_Data, bool convertFlag = false, bool headerFlag = false,string rawDataFileName="", Constants.DataOutput.FileType fileType=Constants.DataOutput.FileType.CSV)
		{
			long j;
			string[,] dataArray;
			long columnCount = 0;
			long maxRowCount = 1;
			long index;

			for (int i = 0; i < S_Data.Count; i++)
			{
				QuestionSettings qs = S_Data[i];
				if (qs.QuestionCount != null && qs.QuestionCount > 0)//check top flag check
				{
					maxRowCount++;
				}

				if (qs.CategoryCount > 0)
				{
					maxRowCount += qs.CategoryCount;
				}

				maxRowCount++;
			}

			dataArray = new string[maxRowCount, 9];
			index = 0;
			if (headerFlag)
			{
				dataArray[0, 0] = LocalResource.LBL_QUESTION_NUMBER;
				dataArray[0, 1] = LocalResource.LBL_QUESTION_TYPE;
				dataArray[0, 2] = LocalResource.LBL_VARIABLE;
				dataArray[0, 3] = LocalResource.LBL_LABEL;
				dataArray[0, 4] = LocalResource.LBL_ANSWER_TYPE;
				dataArray[0, 5] = LocalResource.LBL_CATEGORY_COUNT;
				dataArray[0, 6] = LocalResource.LBL_COLUMN;
				dataArray[0, 7] = LocalResource.LBL_CHOICE_NUMBER;
				dataArray[0, 8] = LocalResource.LBL_QUESTION_CHOICE;
				index++;
			}

			for (int i=0;i< S_Data.Count;i++)
			{
				QuestionSettings qs = S_Data[i];
				bool topFlag = false;
				if (qs.QuestionNumber != "")
				{
					topFlag = true;
					dataArray[index, 0] = qs.QuestionNumber;
					dataArray[index, 1] = qs.QuestionType;
				}
                else
                {
                    dataArray[index, 0] = qs.QuestionNumber;
                    dataArray[index, 1] = qs.QuestionType;
                }
                string variable = qs.Variable;
                if (fileType != Constants.DataOutput.FileType.Excel2007)
                    variable = variable.Replace(",", "，");
                else
                    variable = SetSpace(variable);
                if (qs.QuestionCount != 0 && topFlag)
                {
                    if (fileType != Constants.DataOutput.FileType.Excel2007)
                        dataArray[index, 8] = ConvertComma(qs.TableHeading.Replace(",", "，"), convertFlag);
                    else
                        dataArray[index, 8] = SetSpace(ConvertComma(qs.TableHeading, convertFlag));
                    dataArray[index, 2] = "";
                    dataArray[index, 3] = "";
                    dataArray[index, 4] = "";
                    dataArray[index, 5] = "";
                    dataArray[index, 6] = "";
                    dataArray[index, 7] = "";
                    index++;
                    dataArray[index, 0] = "";
                    dataArray[index, 1] = "";
                }

                if (fileType != Constants.DataOutput.FileType.Excel2007)
                    dataArray[index, 8] = ConvertComma(qs.Question.Replace(",", "，"), convertFlag);
                else
                    dataArray[index, 8] = SetSpace(ConvertComma(qs.Question, convertFlag));
                if (fileType != Constants.DataOutput.FileType.Excel2007)
                    dataArray[index, 2] = ConvertComma(qs.Variable.Replace(",", "，"), convertFlag);
                else
                    dataArray[index, 2] = SetSpace(ConvertComma(qs.Variable, convertFlag));
                dataArray[index, 4] = qs.AnswerType;
				dataArray[index, 5] = qs.CategoryCount == 0 ? "" : qs.CategoryCount.ToString();

                if (qs.AnswerType == Constants.AnswerType.N || qs.AnswerType == Constants.AnswerType.FA || qs.AnswerType == Constants.AnswerType.D)
                {
                    dataArray[index, 7] = "";
                }

                if (qs.AnswerType == Constants.AnswerType.MA)
                {
                    dataArray[index, 3] = "";
                    dataArray[index, 6] = "";
                    for (j = 1; j <= qs.CategoryCount; j++)
                    {
                        dataArray[index + j, 0] = "";
                        dataArray[index + j, 1] = "";
                        dataArray[index + j, 2] = "";
                        dataArray[index + j, 4] = "";
                        dataArray[index + j, 5] = "";
                        dataArray[index + j, 3] = variable + "_" + j;
                        dataArray[index + j, 6] = (++columnCount).ToString();
                    }
                }
                else
                {
                    dataArray[index, 3] = dataArray[index, 2];
                    dataArray[index, 6] = (++columnCount).ToString();
                }

				if (qs.AnswerType == Constants.AnswerType.SA || qs.AnswerType == Constants.AnswerType.MA)
				{
					j = 1;
                    dataArray[index , 7] = "";
                    foreach (string choice in qs.Choices)
                    {
                        dataArray[index + j, 0] = "";
                        dataArray[index + j, 1] = "";
                        dataArray[index + j, 2] = "";
                        dataArray[index + j, 4] = "";
                        dataArray[index + j, 5] = "";
                        if (qs.AnswerType == Constants.AnswerType.SA)
                        {
                            dataArray[index + j, 6] = "";
                            dataArray[index + j, 3] = "";
                        }

                        dataArray[index + j, 7] = j.ToString();
                        if (fileType != Constants.DataOutput.FileType.Excel2007)
                            dataArray[index + j, 8] = ConvertComma(choice.Replace(",", "，"), convertFlag);
                        else
                            dataArray[index + j, 8] = SetSpace(ConvertComma(choice, convertFlag));
                        j++;
					}
				}
                if (qs.AnswerType == Constants.AnswerType.MA)
                    index += qs.CategoryCount + 1;
                else if (qs.AnswerType == Constants.AnswerType.SA || qs.AnswerType == Constants.AnswerType.MA)
                    index += qs.Choices.Count + 1;
                else
                    index++;
            }

			return dataArray;
		}
		/// <summary>
		/// Method to create Horizontal Question Settings Layout data
		/// </summary>
		/// <param name="S_Data">Question Settings list</param>
		/// <param name="fileType">File type such as CSV,EXcel..</param>
		/// <param name="convertFlag">bool value to represent whether convert the special characters or not</param>
		/// <param name="headerFlag">bool value reperesents whether write headers or not</param>
		/// <param name="rawDataFileName">output Data sheet file name</param>
		/// <returns></returns>
		private string[,] HorizontalLayout(List<QuestionSettings> S_Data, Constants.DataOutput.FileType fileType, bool convertFlag = false,bool headerFlag = false,string rawDataFileName="")
		{
			long j;
			string[,] dataArray;
			long index = 0;

			dataArray = new string[S_Data.Count() + 1, 1009];

			if(fileType== Constants.DataOutput.FileType.Excel2007 || headerFlag)
			{
				dataArray[index, 0] = LocalResource.LBL_SHEET_NUMBER;
				dataArray[index, 1] = LocalResource.LBL_COLUMN_NUMBER;
				dataArray[index, 2] = LocalResource.LBL_VARIABLE;
				dataArray[index, 3] = LocalResource.LBL_ANSWER_TYPE;
				dataArray[index, 4] = LocalResource.LBL_CATEGORY_COUNT;
				dataArray[index, 5] = LocalResource.LBL_SCORE;
				dataArray[index, 6] = LocalResource.LBL_SORT;
				dataArray[index, 7] = LocalResource.LBL_QUESTION_A;
				dataArray[index, 8] = LocalResource.LBL_QUESTION_B;

				for (int i = 1; i <= 1000; i++)
				{
					dataArray[index, 8 + i] = i.ToString();
				}
				index++;
			}

			string num = rawDataFileName.Substring(rawDataFileName.LastIndexOf("\\")+1,rawDataFileName.Length- (rawDataFileName.LastIndexOf("\\")+1));
			int columNumber = 1;
			int sheetIndex = 0;
			int clm = 0;
			for(int k=0;k< S_Data.Count;k++)
			{
				QuestionSettings qs = S_Data[k];
				if (qs.AnswerType == Constants.AnswerType.MA)
					clm += qs.CategoryCount;
				else
					clm++;
				dataArray[index, 0] = num;

                if (qs.AnswerType == Constants.AnswerType.MA && OutputDataType != Macromill.QCWeb.ReportRequest.OutputDataType.Code)
				{
                    dataArray[index, 1] = columNumber + "～" + (columNumber + qs.CategoryCount - 1);
                    columNumber += qs.CategoryCount;
                }
				else
				{
					dataArray[index, 1] = columNumber.ToString();
					columNumber++;
				}
				if (fileType == Constants.DataOutput.FileType.Excel2007)
				{
					if (index == 1 || (clm> 16384&& OutputDataType != Macromill.QCWeb.ReportRequest.OutputDataType.Code) )
					{
						if (index > 1)
							clm = 0;
						sheetIndex++;
						dataArray[index, 0] = "00" + sheetIndex.ToString();
					}
					else if(OutputDataType == Macromill.QCWeb.ReportRequest.OutputDataType.Code)
					{
						if (columNumber == 2&&index > 0)
						{
							sheetIndex++;
							dataArray[index, 0] = "00" + sheetIndex.ToString();
						}
					}
				}
				else
				{
					if (index == 0 )
					{
						sheetIndex++;
						dataArray[index, 0] = "00" + sheetIndex.ToString();
					}
				}
				if (fileType == Constants.DataOutput.FileType.Excel2007 && OutputDataType != Macromill.QCWeb.ReportRequest.OutputDataType.Code && clm == 16384)
					columNumber = 1;
				else if (fileType == Constants.DataOutput.FileType.Excel2007 && OutputDataType == Macromill.QCWeb.ReportRequest.OutputDataType.Code && columNumber == 16385)
					columNumber = 1;
				if (fileType != Constants.DataOutput.FileType.Excel2007)
                    dataArray[index, 2] = ConvertComma(qs.Variable.Replace(",", "，"), convertFlag);
                else
                    dataArray[index, 2] = SetSpace(ConvertComma(qs.Variable, convertFlag));
                dataArray[index, 3] = qs.AnswerType;
				dataArray[index, 4] = qs.CategoryCount == 0 ? "" : qs.CategoryCount.ToString();
				dataArray[index, 5] = "";
				dataArray[index, 6] = qs.Sort;
                if (fileType != Constants.DataOutput.FileType.Excel2007)
                    dataArray[index, 7] = ConvertComma(qs.TableHeading.Replace(",", "，"), convertFlag);
                else
                    dataArray[index, 7] = SetSpace(ConvertComma(qs.TableHeading, convertFlag));
                if (fileType != Constants.DataOutput.FileType.Excel2007)
                    dataArray[index, 8] = ConvertComma(qs.Question.Replace(",", "，"), convertFlag);
                else
                    dataArray[index, 8] = SetSpace(ConvertComma(qs.Question, convertFlag));

                j = 9;
				foreach (string str in qs.Choices)
                {
                    if (fileType != Constants.DataOutput.FileType.Excel2007)
                        dataArray[index, j++] = ConvertComma(str.Replace(",", "，"), convertFlag);
                    else
                        dataArray[index, j++] = SetSpace(ConvertComma(str, convertFlag));
                }
				index++;
                num = "";
            }
			return dataArray;
		}
        private string SetSpace(object v)
        {
            if (v != null && v.ToString() != "" && (v.ToString()[0] == '\'' || v.ToString()[0] == '’'))
                return " " + v.ToString();
            return v.ToString();
        }
        private string ConvertComma(string target, bool flagConvert)
		{
			if (flagConvert && (Microsoft.VisualBasic.Strings.InStrRev(target, ",") != 0))
			{
				char c = (char)34;
                return c + target + c;
			}
            return target;
		}

		private void LoadData()
		{
			List<Condition> list = new List<Condition>();
			List<Question> questions = OutputDao.GetVariableMappingList(UsedQuestion);
			list.Add(new Condition(questions[1].Id, questions[1].Variable, "=", new List<int>() { 1 }, ""));
			list.Add(new Condition(questions[2].Id, questions[1].Variable, "=", new List<int>() { 1, 2, 3, 4 }, "OR"));
		}

		private Constants.DataOutput.FileType GetFileType(string str, out string extension)
		{
			extension = null;
			if (str == LocalResource.DO_OUTPUT_FORMAT_EXCEL)
			{
				extension = ".xlsx";
				return Constants.DataOutput.FileType.Excel2007;
			}
			else if (str == LocalResource.DO_OUTPUT_FORMAT_CSV)
			{
				extension = ".csv";
				return Constants.DataOutput.FileType.CSV;
			}
			else if (str == LocalResource.DO_OUTPUT_FORMAT_TAB)
			{
				extension = ".txt";
				return Constants.DataOutput.FileType.TAB;
			}
			else if (str == LocalResource.DO_OUTPUT_FORMAT_SPSS)
			{
				return Constants.DataOutput.FileType.SPSS;
			}
			else if (str == LocalResource.DO_OUTPUT_FORMAT_QC3)
			{
				return Constants.DataOutput.FileType.QC3;
			}
			else if (str == LocalResource.DO_OUTPUT_FORMAT_QC4)
			{
				return Constants.DataOutput.FileType.QC4;
			}
			else if (str == LocalResource.DO_OUTPUT_FORMAT_QLAYOUT)
			{
				return Constants.DataOutput.FileType.QLayout;
			}
			else if (str == LocalResource.DO_OUTPUT_FORMAT_R2D3)
			{
				extension = ".csv";
				return Constants.DataOutput.FileType.R2D3;
			}
			return Constants.DataOutput.FileType.NONE;
		}

		private Macromill.QCWeb.ReportRequest.OutputDataType GetOutputType(string str)
		{
			if (LocalResource.CB_ITEM_01_MA01 == str)
			{
				return Macromill.QCWeb.ReportRequest.OutputDataType.Flag;
			}
			else if (LocalResource.CB_ITEM_02_MA_COMMA_SEPERATED == str)
			{
				return Macromill.QCWeb.ReportRequest.OutputDataType.Code;
			}
			else if (LocalResource.LABEL_MULTIDIGIT_NUMBERS == str)
			{
				return Macromill.QCWeb.ReportRequest.OutputDataType.Decode;
			}
			else
			{
				return Macromill.QCWeb.ReportRequest.OutputDataType.QC3;
			}
		}

		private bool FileCheck(string fileName)
		{

			if (System.IO.File.Exists(fileName))
			{
				var x = MessageBox.Show(String.Format(LocalResource.EX_FILE_ALREADY_EXIST,  System.IO.Path.GetFileName(fileName)), "QuickCross", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
				if (DialogResult.No == x)
				{
					return false;
				}
				try
				{
					System.IO.File.Delete(fileName);
				}
				catch (Exception ex)
				{
                    _log.LogError(ex.Message);
					MessageDialog.ErrorOk(LocalResource.EX_FILE_OPEN_FAILED);
					return false;
				}
			}

			return true;

		}

		private bool FileFoundCheck(string fullPath)
		{
			if (System.IO.File.Exists(fullPath))
			{
				return true;
			}
			return false;
		}

		private string GetFileName(string ext, string fileName = "")
		{
			string filePath = System.IO.Path.GetDirectoryName(TargetPath);
			if (fileName.Equals(""))
			{
				fileName = System.IO.Path.GetFileNameWithoutExtension(TargetPath) + "_rawdata";
			}
			string fullName = filePath + @"\" + fileName + ext;
			return fullName;
		}

		private bool ReplaceAlert(string message)
		{
			if (message.Equals(""))
			{
				return true;
			}
			var x = MessageBox.Show(String.Format(LocalResource.EX_FILE_ALREADY_EXIST,  message ), "QuickCross", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
			if (DialogResult.OK == x)
			{
				return true;
			}
			return false;
		}

		private bool DeleteFile(string fullPath)
		{
			try
			{
				System.IO.File.Delete(fullPath);
				return true;
			}
			catch (Exception ex)
			{
				_log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
			}
			return false;
		}

		private bool DeleteFiles(string[] files)
		{
			foreach (string file in files)
			{
				try
				{
					System.IO.File.Delete(file);

				}
				catch (Exception ex)
				{
					Progress.UpdateProgressBar(100, LocalResource.EX_PROGRESSBAR_EXPORT_FAILED);
					MessageDialog.Warning(LocalResource.EX_FILE_USED_OTHER);
					_log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
					return false;
				}
			}
			return true;
		}

        private bool SpssMain(List<QuestionSettings> S_Data, string spssMaLabel, Questions questions, decimal[] questionIds, string[] layoutFile, string[] dataFile, bool fileCheck,bool unicode)
        {
            string[] changeVarName;
            Dictionary<string, string> dicItemName = SpssChangeItemName(S_Data, out changeVarName);

            Dictionary<string, string> dicVarName = SpssMakeVariantName(S_Data, dicItemName);

            string[] changeVarLabel;
            Dictionary<string, string> dicVarLabel = SpssMakeVariantLabel(S_Data, dicItemName, spssMaLabel, out changeVarLabel);

            string[] changeValLabel;
            Dictionary<string, string[]> dicValLabel = SpssMakeValueLabel(S_Data, dicItemName, spssMaLabel, out changeValLabel, dicVarName);

            string ruleMsg = SpssRuleMsg(changeVarName, changeVarLabel, changeValLabel, false);
            if (!string.IsNullOrEmpty(ruleMsg))
            {
                DialogResult result = MessageDialog.WarningOkCancel(ruleMsg);
                if (result == DialogResult.Cancel)
                {
                    UpdateProgressBar(100, LocalResource.EX_PROGRESSBAR_CANCELED);
                    return false;
                }

                ruleMsg = SpssRuleMsg(changeVarName, changeVarLabel, changeValLabel, true);
            }
            if (!DeleteFileIfExist(layoutFile.Union(dataFile).ToArray()))
            {
                UpdateProgressBar(100, LocalResource.EX_PROGRESSBAR_CANCELED);
                return false;
            }

            Dictionary<string, string> spssAnsType = new Dictionary<string, string>();

			string[] itemNameAry;
			Dictionary<string, string> spssAnstype = new Dictionary<string, string>();
			
			var fileStream = System.IO.File.Open(dataFile[0], System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite);
			fileStream.Dispose();
			using (System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(dataFile[0], false, new UTF8Encoding(true)))
			{
				streamWriter.WriteLine(String.Join("\t", dicVarName.Keys.ToArray()));
				itemNameAry = dicVarName.Values.ToArray();
				for (int i = 0; i < itemNameAry.Count(); i++)
				{
					itemNameAry[i] = itemNameAry[i].Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Replace("\t", "");
				}
				streamWriter.Write(String.Join("\t", itemNameAry));
			}

			for (int i = 1; i < dataFile.GetLength(0); i++)
			{
				System.IO.File.Copy(dataFile[0], dataFile[i]);
			}
			GetSpssAnswerType(ref spssAnsType, questions, questionIds, dicVarName, unicode);
			SpssOutputSyntax(ruleMsg, dicVarLabel, dicValLabel, spssAnsType, "",layoutFile[0]);
            return true;
		}

        private bool DeleteFileIfExist(string[] files)
        {
            if (FileCheck(files) != "")
            {
                foreach (string file in files)
                {
                    try
                    {
                        System.IO.File.Delete(file);

                    }
                    catch (Exception ex)
                    {
                        Progress.UpdateProgressBar(100, LocalResource.EX_PROGRESSBAR_EXPORT_FAILED);
                        MessageDialog.Warning(LocalResource.EX_FILE_USED_OTHER);
                        _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                        return false;
                    }
                }
            }
            return true;
        }

        private Dictionary<string, string> SpssChangeItemName(List<QuestionSettings> S_Data, out string[] changeVarName)
		{
			Dictionary<string, string> itemNameDic = new Dictionary<string, string>();
			Dictionary<string, string> labelDic = new Dictionary<string, string>();
			string itemName;
			string wkStr;
			int listIdx;
			int i;
			int j;

			String[] rvAry = new string[] { "ALL", "AND", "BY", "EQ", "GE", "GT", "LE", "LT", "NE", "NOT", "OR", "TO", "WITH" };
			String[] okAry = new string[] { ".", "_", "$", "#", "@" , "＃","．","＿", "＠","＄" };

            String[] ngAry = new string[] { " ", "!", @"""", "%", "&", "'", "(", ")", "*", "+", ",", "-", "/", ":", ";", "<", "=", ">", "?", "[", @"\", "]", "^", "`", "{", "|", "}", "~", "￥",
                "　", "！", "”", "％", "＆", "’", "（", "）", "＊", "＋", "，", "－", "／", "：", "；", "＜", "＝", "＞", "？", "［", "］", "＾", "‘", "｛", "｜", "｝", "～"
            };

			foreach (QuestionSettings qs in S_Data)
			{
				itemName = qs.Variable;
				foreach (string s in ngAry)
				{
					itemName = itemName.Replace(s, "_");
				}

				//'ASCIIコード0～31の削除（追加仕様）
				//ItemName = Application.WorksheetFunction.Clean(ItemName)

				//末尾の記号削除
				for (j = itemName.Length; j > 0; j--)
				{
					foreach (string s in okAry)
					{
						if (Vb.Strings.InStr(1, Vb.Strings.Right(itemName, 1), s, Vb.CompareMethod.Text) > 0)
						{
							itemName = Vb.Strings.Left(itemName, j - 1);
							break;
						}
					}
					if (j == Vb.Strings.Len(itemName)) break;
				}

				if (itemName == "") itemName = "var";

				//'先頭が記号の場合"v"を付加
				for (i = 0; i < okAry.Length; i++)
				{
					if (Vb.Strings.InStr(1, Vb.Strings.Left(itemName, 1), okAry[i], Vb.CompareMethod.Text) > 0)
					{
						itemName = "v" + itemName;
					}
				}

				//'先頭文字が0～9の場合"v"を付加
				if (Vb.Information.IsNumeric(Vb.Strings.Left(itemName, 1)))
				{
					itemName = "v" + itemName;
				}

				//'予約語の場合"v"を付加
				for (i = 0; i < rvAry.Length; i++)
				{
					if (Vb.Strings.StrComp(itemName, rvAry[i], Vb.CompareMethod.Text) == 0)
					{
						itemName = "v" + itemName;
						break;
					}
				}

				//'変数名の長さ
				if (qs.AnswerType.Equals(Constants.AnswerType.MA))
				{
					wkStr = itemName + "_" + qs.CategoryCount;
				}
				else
				{
					wkStr = itemName;
				}

				if (StrLen(wkStr, Unicode) > 64)
				{
					itemName = "var";
				}

				//'既出チェック
				if (!itemName.Equals("var"))
				{
					if (qs.AnswerType.Equals(Constants.AnswerType.MA))
					{
						for (j = 1; j <= qs.CategoryCount; j++)
						{
							if (labelDic.ContainsKey(Vb.Strings.LCase(itemName + "_" + j)))
							{
								itemName = "var";
								break;
							}
						}
					}
					else
					{
						if (labelDic.ContainsKey(Vb.Strings.LCase(itemName)))
						{
							itemName = "var";
						}
					}
				}

				//既出チェックOK　->　次のチェック用にディクショナリ登録
				if (!itemName.Equals("var"))
				{
					if (qs.AnswerType.Equals(Constants.AnswerType.MA))
					{
						for (j = 1; j <= qs.CategoryCount; j++)
						{
							labelDic.Add(Vb.Strings.LCase(itemName + "_" + j), "");
						}
					}
					else
					{
						labelDic.Add(itemName, "");
					}
				}
				itemNameDic.Add(qs.Variable, itemName);
			}

			//アイテム名がvarXだった場合のインデックス付与（既存変数名と同じ場合は次の番号へ）
			i = 0;
			listIdx = 0;
			String[] changeAry = new string[] { };
            for(int k=0;k<itemNameDic.Count;k++)
			{
                var item = itemNameDic.ElementAt(k);
                string str = item.Key;
                itemName = itemNameDic[str];
				var qs = S_Data[listIdx];
				if (itemName.Equals("var"))
				{
					do
					{
						i = i + 1;
						if (Constants.AnswerType.MA.Equals(S_Data[listIdx].AnswerType))
						{
							if (!labelDic.ContainsKey("var" + i))
							{
								for (j = 1; j <= qs.CategoryCount; j++)
								{
									if (labelDic.ContainsKey("var" + i + "_" + j))
									{
										break;
									}
									if (j == qs.CategoryCount)
									{
										itemName = "var" + i;
										goto ExitDo;
									}
									//break;
								}
							}
						}
						else
						{
							if (!labelDic.ContainsKey("var" + i))
							{
								itemName = "var" + i;
								goto ExitDo;
							}
						}
					}
					while (i == 65536);
					ExitDo:
					if (qs.AnswerType.Equals(Constants.AnswerType.MA))
					{
						labelDic.Add(Vb.Strings.LCase(itemName), "");
						for (j = 1; j <= qs.CategoryCount; j++)
						{
							labelDic.Add(Vb.Strings.LCase(itemName + "_" + j), "");
						}
					}
					else
					{
						labelDic.Add(Vb.Strings.LCase(itemName), "");
					}

					itemNameDic[str] = itemName;
				}

				if (!str.Equals(itemNameDic[str]))
				{
					Array.Resize(ref changeAry, changeAry.Count() + 1);
					changeAry[changeAry.Count() - 1] = str;
				}
				listIdx++;
			}
			changeVarName = changeAry;
			return itemNameDic;
		}


		private Dictionary<string, string> SpssMakeVariantName(List<QuestionSettings> S_Data, Dictionary<string, string> itemNameDic)
		{
			Dictionary<string, string> retVariantName = new Dictionary<string, string>();
			string itemName;
			int i;
			foreach (QuestionSettings qs in S_Data)
			{
				itemName = qs.Variable;
				if (Constants.AnswerType.MA.Equals(qs.AnswerType))
				{
					for (i = 1; i <= qs.CategoryCount; i++)
					{
						retVariantName.Add(itemNameDic[itemName] + "_" + i, itemName + "_" + i);
					}
				}
				else
				{
					retVariantName.Add(itemNameDic[itemName], itemName);
				}
			}
			return retVariantName;
		}

		private Dictionary<string, string> SpssMakeVariantLabel(List<QuestionSettings> S_Data, Dictionary<string, string> itemNameDic, string SpssMaLabel, out string[] changeAry)
		{
			string itemName;
			string strA;
			string strB;
			string strC;
			Dictionary<string, string> dictStrA = new Dictionary<string, string>();
			Dictionary<string, string> dictStrB = new Dictionary<string, string>();
			Dictionary<string, string> dictStrC = new Dictionary<string, string>();
			string checkStr;
			string wkStr;
			int i;

			foreach (QuestionSettings qs in S_Data)
			{
				itemName = qs.Variable;
				strA = qs.TableHeading; //title
				if (dictStrA.ContainsKey(strA))
				{
					strA = dictStrA[strA];
				}

				strB = qs.Question;
				if (dictStrB.ContainsKey(strB))
				{
					strB = dictStrB[strB];
				}

				//'MA選択肢　変数ラベル
				if (Constants.AnswerType.MA.Equals(qs.AnswerType) && LocalResource.CB_MA_SPSS_01.Equals(SpssMaLabel))
				{

					for (i = 0; i < qs.Choices.Count(); i++)
					{
						strC = qs.Choices[i];
						if (dictStrC.ContainsKey(strC))
						{
							strC = dictStrC[strC];
						}

						checkStr = itemName + "_" + (i + 1) + " " + (strA == "" ? "" : strA + " : ") + strB + " _ " + strC;
						if (StrLen(checkStr, Unicode) > 190)
						{
							// '表題
							if (!dictStrA.ContainsKey(qs.TableHeading))
							{
								wkStr = StrCut(strA, 80, Unicode);//#255661-Bytecut Length changed to 80
								if (!strA.Equals(wkStr))
								{
									dictStrA.Add(strA, wkStr);
									strA = wkStr;
								}
							}

							checkStr = itemName + "_" + (i + 1) + " " + (strA == "" ? "" : strA + " : ") + strB + " - " + strC; ;
							if (StrLen(checkStr, Unicode) > 190)
							{
								//質問文
								if (!dictStrB.ContainsKey(qs.Question))
								{
									wkStr = StrCut(strB, 60, Unicode);//#255661-Bytecut Length changed to 60
									if (!wkStr.Equals(strB))
									{
										dictStrB.Add(strB, wkStr);
										strB = wkStr;
									}
								}

								checkStr = itemName + "_" + (i + 1) + " " + ("".Equals(strA) ? "" : strA + " : ") + strB + " - " + strC;
								if (StrLen(checkStr, Unicode) > 254)
								{
									//選択肢
									if (!dictStrC.ContainsKey(qs.Choices[i]))
									{
										wkStr = StrCut(strC, 251 - StrLen(itemName + "_" + (i + 1) + " " + ("".Equals(strA) ? "" : strA + " : ") + strB + " - ", Unicode), Unicode);
										if (!wkStr.Equals(strC))
										{
											dictStrC.Add(strC, wkStr);
											strC = wkStr;
										}
									}
									else
									{
										wkStr = StrCut(Convert.ToString(qs.Choices[i]), 251 - StrLen(itemName + "_" + (i + 1) + " " + (strA == "" ? "" : strA + " : ") + strB + " - ", Unicode), Unicode);
										if (!wkStr.Equals(qs.Choices[i]))
										{
											dictStrC[qs.Choices[i]] = wkStr;
											strC = wkStr;
										}
									}
								}
							}
						}
					}
				}
				else
				{
					if (Constants.AnswerType.MA.Equals(qs.AnswerType))
					{
						itemName = itemName + "_" + qs.CategoryCount;
					}

					checkStr = itemName + " " + ("".Equals(strA) ? "" : strA + " : ") + strB;
					if (StrLen(checkStr, Unicode) > 254)
					{
						// '表題
						if (!dictStrA.ContainsKey(qs.TableHeading))
						{
							wkStr = StrCut(strA, 80, Unicode); //#255661-Bytecut Length changed to 80
							if (!strA.Equals(wkStr))
							{
								dictStrA.Add(strA, wkStr);
								strA = wkStr;
							}

							checkStr = itemName + " " + ("".Equals(strA) ? "" : strA + " : ") + strB;
							if (StrLen(checkStr, Unicode) > 254)
							{
								//'質問文
								if (!dictStrB.ContainsKey(qs.Question))
								{
									wkStr = StrCut(strB, 251 - StrLen(itemName + " " + ("".Equals(strA) ? "" : strA + " : "), Unicode), Unicode);
									if (!strB.Equals(wkStr))
									{
										dictStrB.Add(strB, wkStr);
										strB = wkStr;
									}
								}
								else
								{
									wkStr = StrCut(Convert.ToString(qs.Question), 251 - StrLen(itemName + " " + ("".Equals(strA) ? "" : strA + " : "), Unicode), Unicode);
									if (!wkStr.Equals(qs.Question))
									{
										dictStrB[qs.Question] = wkStr;
										strB = wkStr;
									}
								}
							}
						}
					}
				}
			}

			// '戻り値作成
			Dictionary<string, string> retLabels = new Dictionary<string, string>();
			bool changeFlag;
			changeAry = new string[] { };
			foreach (QuestionSettings qs in S_Data)
			{
				changeFlag = false;
				itemName = qs.Variable;

				strA = qs.TableHeading;
				if (dictStrA.ContainsKey(strA))
				{
					strA = dictStrA[strA];
					changeFlag = true;
				}

				strB = qs.Question;
				if (dictStrB.ContainsKey(strB))
				{
					strB = dictStrB[strB];
					changeFlag = true;
				}

				// 'MA選択肢　変数ラベル
				if (Constants.AnswerType.MA.Equals(qs.AnswerType) && LocalResource.CB_MA_SPSS_01.Equals(SpssMaLabel))
				{
					for (i = 0; i < qs.Choices.Count(); i++)
					{
						strC = qs.Choices[i];//'選択肢
						if (dictStrC.ContainsKey(strC))
						{
							strC = dictStrC[strC];
							changeFlag = true;
						}
						retLabels.Add(itemNameDic[itemName] + "_" + (i + 1), itemName + "_" + (i + 1) + " " + ("".Equals(strA) ? "" : strA + " : ") + strB + " - " + strC);
					}
				}
				else if (Constants.AnswerType.MA.Equals(qs.AnswerType))
				{
					for (i = 0; i < qs.Choices.Count(); i++)
					{
						retLabels.Add(itemNameDic[itemName] + "_" + (i + 1), itemName + "_" + (i + 1) + " " + ("".Equals(strA) ? "" : strA + " : ") + strB);
					}
				}
				//'MA以外
				else
				{
					retLabels.Add(itemNameDic[itemName], itemName + " " + ("".Equals(strA) ? "" : strA + " : ") + strB);
				}

				if (changeFlag)
				{
					Array.Resize(ref changeAry, changeAry.Count() + 1);
					changeAry[changeAry.Count() - 1] = itemName;
				}
			}
			return retLabels;
		}

		private Dictionary<string, string[]> SpssMakeValueLabel(List<QuestionSettings> S_Data, Dictionary<string, string> itemNameDic, string maLabel, out string[] changeAry, Dictionary<string, string> dicVarName)
		{
			Dictionary<string, string[]> retLabels = new Dictionary<string, string[]>();
			changeAry = new string[] { };
			string[] choicesAry = new string[] { };
			string itemName;
			bool changeFlag;
			int i;

			foreach (QuestionSettings qs in S_Data)
			{
				changeFlag = false;
				itemName = qs.Variable;
                string valBykey = dicVarName.FirstOrDefault(x => x.Value == itemName).Key;

                // 'SA、MA（値ラベル時）
                if ((qs.AnswerType.Equals(Constants.AnswerType.MA) && LocalResource.CB_MA_SPSS_02.Equals(maLabel)) || qs.AnswerType.Equals(Constants.AnswerType.SA))
				{
					choicesAry = new string[qs.CategoryCount];
					for (i = 0; i < qs.CategoryCount; i++)
					{
						choicesAry[i] = qs.Choices[i];
						if (StrLen(Convert.ToString(choicesAry[i]), Unicode) > 120)
						{
							choicesAry[i] = StrCut(Convert.ToString(choicesAry[i]), 117, Unicode);
							changeFlag = true;
						}
					}


					if (Constants.AnswerType.SA.Equals(qs.AnswerType))
					{
						retLabels.Add(valBykey, choicesAry);
					}
					else
					{
						for (i = 0; i < qs.CategoryCount; i++)
						{
                            valBykey = dicVarName.FirstOrDefault(x => x.Value == itemName + "_" + (i + 1)).Key;
                            retLabels.Add(valBykey, new string[] { choicesAry[i] });
						}
					}
					if (changeFlag)
					{
						Array.Resize(ref changeAry, changeAry.Count() + 1);
						changeAry[changeAry.Count() - 1] = qs.Variable;
					}
				}
				//'MA（変数ラベル）
				else if (Constants.AnswerType.MA.Equals(qs.AnswerType))
				{
					for (i = 0; i < qs.CategoryCount; i++)
					{
                        valBykey = dicVarName.FirstOrDefault(x => x.Value == itemName + "_" + (i + 1)).Key;
                        retLabels.Add(valBykey , new string[] { "ON" });
					}
				}
			}

			return retLabels;
		}

		private string SpssRuleMsg(string[] changeVarName, string[] changeVarLabel, string[] changeValLabel, bool syntaxMode)
		{
			string ruleMsg = "";
			if (!syntaxMode)
			{
				if ((changeVarName != null && changeVarName.Count() > 0) || (changeVarLabel != null && changeVarLabel.Count() > 0) || (changeValLabel != null && changeValLabel.Count() > 0))
					ruleMsg = LocalResource.SPSS_RULE_MESSAGE;
                if (changeVarName != null && changeVarName.Count() > 0)
				{
                    ruleMsg += Environment.NewLine + "  [VARIABLE NAME] " + Vb.Strings.Join(changeVarName, ", ");
                }
				if (changeVarLabel != null && changeVarLabel.Count() > 0)
				{
                    if (!"".Equals(ruleMsg)) ruleMsg += "\r\n";
                    ruleMsg += "  [VARIABLE LABELS] " + Vb.Strings.Join(changeVarLabel, ", ");
                }
				if (changeValLabel != null && changeValLabel.Count() > 0)
				{
					if (!"".Equals(ruleMsg)) ruleMsg += "\r\n";
					ruleMsg +=  "  [VALUE LABELS] " + Vb.Strings.Join(changeValLabel, ", ");
				}
			}
			else
			{
                if (changeVarName != null && changeVarName.Count() > 0)
				{
                    ruleMsg += "*   [VARIABLE NAME]\t" + Vb.Strings.Join(changeVarName, "\t") + "\t.";
				}
				if (changeVarLabel != null && changeVarLabel.Length>0)
				{
                    if (!"".Equals(ruleMsg)) ruleMsg += "\r\n";
					ruleMsg += "*   [VARIABLE LABELS]\t" + Vb.Strings.Join(changeVarLabel, "\t") + "\t.";
				}
				if (changeValLabel != null && changeValLabel.Length>0)
				{
                    if (!"".Equals(ruleMsg)) ruleMsg += "\r\n";
					ruleMsg += "*   [VALUE LABELS]\t" + Vb.Strings.Join(changeValLabel, "\t") + "\t.";
				}
			}
			return ruleMsg;
		}

		private int StrLen(string targetStr, bool uniCodeMode)
		{   //Old Implementation
            /*int codepage = LocalResource.Culture.TextInfo.ANSICodePage;
            byte[] convertedBytes = Encoding.GetEncoding(codepage).GetBytes(targetStr);
            string convertedAsciiString = System.Text.Encoding.ASCII.GetString(convertedBytes);
            int wkLen = convertedAsciiString.Length;
            if (uniCodeMode)
            {
                wkLen = (wkLen - Vb.Strings.Len(targetStr)) * 3 + Vb.Strings.Len(targetStr) * 2 - wkLen;
            }
            return wkLen;*/
            //New Implementation
            Encoding shiftjisEnc = Encoding.GetEncoding("Shift_JIS");
            int chrByteNum = shiftjisEnc.GetByteCount(targetStr);
            if (uniCodeMode)
            {
                int numFullWidth = chrByteNum - targetStr.Length;
                int numHalfWidth = (targetStr.Length * 2) - chrByteNum;
                int chrUnicodeByteNum = (numFullWidth * 3) + numHalfWidth;
                return chrUnicodeByteNum;
            }
            else
            {
                return chrByteNum;
            }

        }

		private string StrCut(string targetStr, int limitLen, bool unicode)
		{
			int i;
			string wkStr = "";
			string retVal;

			if (limitLen <= 0)
			{
				return String.Empty;
			}
			bool withinlimit = true;//Variable to identify whether the given limit is within the approved limit or not
			retVal = targetStr;
			//New Implementation
			string appendnotation = "";
			if (StrLen(retVal, unicode) > limitLen)
				withinlimit = false;
			while (StrLen(retVal, unicode) > limitLen)
			{
				retVal = retVal.Remove(retVal.Length - 1);
				appendnotation = "...";
			}
			if (!withinlimit)
			{
				while (StrLen(retVal, unicode) > limitLen - 3) // Remove 3 bytes of data from the end to append the '...' which takes extra 3 bytes.
					retVal = retVal.Remove(retVal.Length - 1);
			}
			return retVal + appendnotation;

			//Old Implementation
			/* if (StrLen(retVal, unicode) > limitLen)
             {
                 if (unicode)
                 {
                     for (i = limitLen / 3; i <= Vb.Strings.Len(retVal); i++)
                     {
                         wkStr = Vb.Strings.Left(retVal, i);
                         if (StrLen(wkStr, unicode) > limitLen)
                         {
                             retVal = Vb.Strings.Left(wkStr, Vb.Strings.Len(wkStr) - 1) + "...";
                             break;
                         }
                     }
                 }*/

		}

		private void SpssOutputSyntax(string ruleMsg, Dictionary<string, string> dicVarLabel, Dictionary<string, string[]> dicValLabel, Dictionary<string, string> spssAnsType, string bunruiPath, string path)
		{
			var fileStream = System.IO.File.Open(path, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite);
			fileStream.Dispose();
			using (System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(path, false, new UTF8Encoding(true)))
			{
				string tempLines = SpssSyntaxHeader;
				//'ヘッダー　UTF8記載
				if (Unicode)
				{
					tempLines = tempLines.Replace("%%CharacterEncoding%%", "[Unicode (universal character set)]");
					tempLines = tempLines.Replace("%%UnicodeMode%%", "ON");
				}
				else
				{
					tempLines = tempLines.Replace("%%CharacterEncoding%%", "[Locale's writing system]");
					tempLines = tempLines.Replace("%%UnicodeMode%%", "OFF");
				}
				streamWriter.WriteLine(tempLines);

                //ルール
                if (!"".Equals(ruleMsg))
                {
                    tempLines = SpssSyntaxRule.Replace("%%Rules%%", ruleMsg);
                    streamWriter.Write(tempLines);
                }

                //'ファイル名
                tempLines = SpssSyntaxFileName.Replace("%%FileName%%", System.IO.Path.GetFileName(TargetPath) + "Spss" + bunruiPath);
				tempLines = tempLines.Replace("%%FilePath%%", System.IO.Path.GetDirectoryName(TargetPath));
				streamWriter.WriteLine(tempLines);

				//'Variables
				string wkStr = "";
				var keys = dicVarLabel.Keys.ToArray();
				foreach (string key in spssAnsType.Keys)
				{
					wkStr += "\r\n\t" + key + "\t" + spssAnsType[key];
				}
				tempLines = SpssSyntaxVariables.Replace("%%Variables%%", wkStr);
				streamWriter.WriteLine(tempLines);

				wkStr = "";
				foreach (string key in spssAnsType.Keys)
				{
					if (Vb.Strings.InStr(1, spssAnsType[key], ".") > 0 && Vb.Strings.StrComp(key, "SAMPLEID", Vb.CompareMethod.Text) != 0)
					{
						wkStr += "\t" + key;
					}
				}

				if (!wkStr.Equals(""))
				{
					streamWriter.WriteLine("/" + wkStr + "\t(SCALE)", 1);
				}

				// 'Var Labels
				tempLines = SpssSyntaxVarLabel;
				streamWriter.WriteLine(tempLines, 1);

				tempLines = "";
                bool isDicVar = false;
                for (int i=0;i< dicVarLabel.Count;i++)
				{
                    string key = dicVarLabel.ElementAt(i).Key;
                    string itm = key != null && key != "" && (key[0] == '_' || key[0] == '＿') ? "v" + key : key;
                    tempLines += string.Format("\t" + itm + "\t'" + dicVarLabel[key].Replace("'", "''").Replace("{","{{").Replace("}","}}").Replace("{}", "{0}") + "'", "{{}}");
					streamWriter.Write(tempLines.Replace("{", "{{").Replace("}", "}}"), 1);
					tempLines = "\r\n/";
                    isDicVar = true;
                }

                // 'Val Labels
                tempLines = "\t";
				if (dicValLabel.Count > 0)
				{
					streamWriter.WriteLine(SpssSyntaxValLabel.Replace("{}", "{{}}"), 1);
                    for (int i = 0; i < dicValLabel.Count; i++)
                    {
                        string key = dicValLabel.ElementAt(i).Key;
                        streamWriter.WriteLine(tempLines + key.Replace("{", "{{").Replace("}", "}}"), 1);
						for (int j = 0; j < dicValLabel[key].Count(); j++)
						{
							streamWriter.WriteLine("\t\t" + (j + 1) + "\t'" + dicValLabel[key][j].Replace("'", "''").Replace("{", "{{").Replace("}", "}}") + "'", 1);
						}
						tempLines = "/\t";
                    }
                }
                if (isDicVar)
                {
                    streamWriter.WriteLine(".");
                    streamWriter.WriteLine(SpssSyntaxFooter, 1);
                }
                else
                {
                    streamWriter.WriteLine(".\n\r" + SpssSyntaxFooter, 1);
                }
			}
		}

		private void GetSpssAnswerType(ref Dictionary<string, string> spssAnsType, Questions questions, decimal[] questionIds,Dictionary<string, string> dicVarName,bool unicode)
        {
			string table = "answers";
			if (DBHelper.checkAfterProcess(SourceWorkbook))
				table = "data_after_process";

			bool[] filteringFlag = null;
            if (FilterSettings != null && FilterSettings.Count > 0)
            {
                string connectionString = DB.DBHelper.GetConnectionString(SourceWorkbook);
                string filterExp = Macromill.QCWeb.Logic.TabulationEx.Criteria.CriteriaDescProvider.CreateCriteriaDescriptions(FilterSettings, questions);
                filteringFlag = new Criteria(filterExp, "", questions).Filtering(connectionString, TableName);
            }

            string[] maxVal = DB.DataOutput.SpssNCount(questionIds, questions, SourceWorkbook, QCAnswerType.N, unicode, filteringFlag, table);
            string[] maxFAVal = DB.DataOutput.SpssNCount(questionIds, questions, SourceWorkbook, QCAnswerType.FA, unicode, filteringFlag, table);
            for (int listIdx = 0; listIdx < questionIds.Count(); listIdx++)
			{
				Questions.Question question = questions[questionIds[listIdx]] as Questions.Question;
                string qName = dicVarName.FirstOrDefault(x => x.Value == question.Name).Key;
                qName = qName != null && qName != "" && (qName[0] == '_' || qName[0] == '＿') ? "v" + qName : qName;
                switch (question.QCAnswerType)
				{
					case QCAnswerType.SA:
						if (!spssAnsType.ContainsKey(question.Name))
							spssAnsType.Add(qName, "F3");
						break;
					case QCAnswerType.MA:
						for (int i = 1; i <= question.Sectors.Count; i++)
                        {
                            qName = dicVarName.FirstOrDefault(x => x.Value == question.Name + "_" + (i)).Key;
                            if (!spssAnsType.ContainsKey(question.Name))
								spssAnsType.Add(qName , "F1");
						}
						break;
					case QCAnswerType.FA:
                        if (!spssAnsType.ContainsKey(question.Name))
                        {
                            spssAnsType.Add(qName, maxFAVal[listIdx]);
                        }
						break;
					case QCAnswerType.N:
						if (!spssAnsType.ContainsKey(question.Name))
                            spssAnsType.Add(qName, maxVal[listIdx]);
                        break;
					case QCAnswerType.D:
						if (!spssAnsType.ContainsKey(question.Name))
							spssAnsType.Add(qName, "DATETIME20");
						break;
				}
			}
		}

		private string FileCheck(String[] fileNames)
		{
			string message = "";
			foreach (string str in fileNames)
			{
				if (FileFoundCheck(str))
				{
					message += "\n" + System.IO.Path.GetFileName(str);
				}
			}
            if(fileNames!=null&& fileNames.Length>0)
            {
                string filterPath = fileNames[0].Substring(0, fileNames[0].LastIndexOf('\\'))+"\\Filter.txt";
                if (FilterSettings!=null && FileFoundCheck(filterPath))
                {
                    message += "\n" + System.IO.Path.GetFileName(filterPath);
                }
            }
			return message;
		}

		private bool FileCheckAndAlert(string[] files)
		{
			string message = FileCheck(files);

			if (!ReplaceAlert(message))
			{
				UpdateProgressBar(100, LocalResource.EX_PROGRESSBAR_CANCELED);
				return false;
			}

			if (!DeleteFiles(files))
			{
				return false;
			}

			return true;
		}

        private bool FileCheckAndAlertForSPSS(string[] files)
        {
            string message = FileCheck(files);

            if (!ReplaceAlert(message))
            {
                UpdateProgressBar(100, LocalResource.EX_PROGRESSBAR_CANCELED);
                return false;
            }
            
            return true;
        }

        private List<QuestionSettings> TrimQuestionDetail(List<QuestionSettings> questions, out string message)
		{
			message = "";
			string prefix = "";
			foreach (QuestionSettings qs in questions)
			{
				if (qs.CategoryCount > 600)
				{
					if (qs.QuestionType == Constants.QuestionType.SAR || qs.QuestionType == Constants.QuestionType.SAP || qs.QuestionType == Constants.QuestionType.SAS || qs.QuestionType == Constants.QuestionType.MAC || qs.QuestionType == Constants.QuestionType.MTS || qs.QuestionType == Constants.QuestionType.MTM || qs.QuestionType == Constants.QuestionType.MTT || qs.QuestionType == Constants.QuestionType.RNK)
					{
						qs.QuestionType = Constants.QuestionType.FAS;
					}

					if (qs.AnswerType == Constants.AnswerType.MA)
					{
						qs.AnswerType = Constants.AnswerType.FA;
					}
					else if (qs.AnswerType == Constants.AnswerType.SA)
					{
						qs.AnswerType = Constants.AnswerType.N;
					}

					qs.CategoryCount = 0;
					qs.Choices = null;

					message += prefix + qs.Variable;
					prefix = ",";
				}
			}
			return questions;
		}

		private bool QLayoutMain(List<QuestionSettings> questions, string title, string[] layoutPath,string[] outputFile, out string message)
		{
			questions = TrimQuestionDetail(questions, out message);
			string[,] layoutAry = QLayoutHorizontal(UsedQuestion, title);
			SeparatedValuesBase sb = new SeparatedValuesBase();
			string[] layout = sb.GetRawDataBuffer(",", layoutAry);
			layout[0] += "FILEEND\r\n";
			sb.Export(layoutPath[0], layout[0],typ: Constants.DataOutput.FileType.QLayout,encoding: Constants.DataOutput.defaultEncoding);
            string header = String.Join("\t", UsedQuestion.Select(v => v.Variable).ToArray()).Replace(",", "，");
            sb.Export(outputFile[0], header + "\r\n", typ: Constants.DataOutput.FileType.QLayout, encoding: Constants.DataOutput.defaultEncoding);
			for (int i = 1; i < outputFile.GetLength(0); i++)
			{
				System.IO.File.Copy(outputFile[0],outputFile[i]);
			}
			
			return true;
		}

		private string[,] QLayoutHorizontal(List<QuestionSettings> questions, string title)
		{
			int maxRow = 2;
			foreach (QuestionSettings qs in questions)
			{
				maxRow++;
				if (qs.CategoryCount > 200)
				{
					int outer = qs.CategoryCount / 200;
					if (qs.CategoryCount % 200 != 0) outer++;
					maxRow += outer - 1;
				}
			}

			string[,] dataArray = new string[maxRow, 212];
			long index = 1;
			int column = 1;
			dataArray[0, 0] = 99999.ToString();
			dataArray[0, 1] = title;
			dataArray[1, 0] = LocalResource.LBL_QUESTION_NUMBER;
			dataArray[1, 1] = LocalResource.LBL_QUESTION_TYPE;
			dataArray[1, 2] = LocalResource.LBL_QUESTION_COUNT;
			dataArray[1, 3] = LocalResource.LBL_VARIABLE;
			dataArray[1, 4] = LocalResource.LBL_ANSWER_TYPE;
			dataArray[1, 5] = LocalResource.LBL_CATEGORY_COUNT;
			dataArray[1, 6] = LocalResource.LBL_SCORE;
			dataArray[1, 7] = LocalResource.LBL_SORTING;
			dataArray[1, 8] = LocalResource.LBL_COLUMN;
            dataArray[1, 9] = LocalResource.LBL_QUESTION_A;
			dataArray[1, 10] = LocalResource.LBL_QUESTION_B;

			foreach (QuestionSettings qs in questions)
			{
				index++;
				dataArray[index, 0] = ConvertProcess(qs.QuestionNumber);
				dataArray[index, 1] = qs.QuestionType;
				dataArray[index, 2] = qs.QuestionCount == null || qs.QuestionCount == 0 ? "" : qs.QuestionCount.ToString();
				dataArray[index, 3] = ConvertProcess(qs.Variable);
				dataArray[index, 4] = qs.AnswerType;
				dataArray[index, 5] = qs.CategoryCount == 0 ? "" : qs.CategoryCount.ToString();
                dataArray[index, 6] = String.Empty;
                dataArray[index, 7] = String.Empty;
				dataArray[index, 8] = (column++).ToString();
                dataArray[index, 9] = ConvertProcess(qs.TableHeading);
                dataArray[index, 10] = ConvertProcess(qs.Question);

                if (qs.CategoryCount <= 0)
				{
					continue;
				}

				int outer = qs.CategoryCount / 200;
				if (qs.CategoryCount % 200 != 0) outer++;
				int choiceIndex = 0;
				index--;
				for (int i = 1; i <= outer; i++)
				{
					index++;
					int inner = 200;
					if (200 * i > qs.CategoryCount) inner = qs.CategoryCount - 200 * (i - 1);
                    if(i>1)
                    {
                        for (int c = 0; c < 11; c++)
                            dataArray[index, c] = "";
                    }
					for (int j = 0; j < inner; j++)
					{
                        if (qs.Choices[choiceIndex].Contains("\n"))
                            dataArray[index, j + 12] = "\"" + ConvertProcess(qs.Choices[choiceIndex]) + "\"";
                        else
                            dataArray[index, j + 12] = ConvertProcess(qs.Choices[choiceIndex]);
                        choiceIndex++;
                    }
				}
			}

			return dataArray;
		}

        private string ConvertProcess(string str)
		{
            str = str.Replace(",", "，")
                .Replace("\r", "")
                .Replace("\n", "<LF>")
                .Replace("\t", "<TAB>");
			return str;
		}

		private void GetHeaderAry(ref string[,] header)
		{
			int maxCol = 0;
			if (OutputDataType != Macromill.QCWeb.ReportRequest.OutputDataType.Code)
			{
				for (int i = 0; i < UsedQuestion.Count; i++)
				{
					var x = UsedQuestion[i];
					if (x.AnswerType == Constants.AnswerType.MA)
					{
						maxCol += x.CategoryCount;
					}
					else
					{
						maxCol++;
					}
				}
			}
			else
			{
				maxCol = UsedQuestion.Count;
			}

			header = new string[1, maxCol];
			int index = 0;

			if (OutputDataType != Macromill.QCWeb.ReportRequest.OutputDataType.Code)
			{
				for (int i = 0; i < UsedQuestion.Count; i++)
				{
					var x = UsedQuestion[i];
					if (x.AnswerType == Constants.AnswerType.MA)
					{
						for (int j = 0; j < x.CategoryCount; j++)
						{
							header[0, index++] = x.Variable + "_" + (j + 1);
						}
					}
					else
					{
						header[0, index++] = x.Variable;
					}
				}
			}
			else
			{
				for (int i = 0; i < UsedQuestion.Count; i++)
				{
					header[0, index++] = UsedQuestion[i].Variable;
				}
			}
		}

		private void GetFileName(ref string[] layoutFile, ref string[] dataFile,string layoutName, string dataName, string layountExt, string dataExt, string basePath = null)
		{
			int maxFileCount = DivisionQuestion == null ? 1 : DivisionQuestion.CategoryCount;
			layoutFile = new string[maxFileCount];
			dataFile = new string[maxFileCount];
			if (basePath == null)
			{
				basePath = TargetPath;
			}

			string ext = FileType == Constants.DataOutput.FileType.CSV ? Constants.CsvExtension : Constants.TabExtension;

			if (DivisionQuestion == null)
			{
				layoutFile[0] = basePath + layoutName + layountExt;
				dataFile[0] = basePath + dataName + dataExt;
			}
			else
			{
				string layoutPath = basePath + layoutName + "_" + DivisionQuestion.Variable + "_";
				string dataPath = basePath + dataName + "_" + DivisionQuestion.Variable + "_";

				for (int i = 1; i <= DivisionQuestion.CategoryCount; i++)
				{
					layoutFile[i - 1] = layoutPath + i.ToString("0000") + layountExt;
					dataFile[i - 1] = dataPath + i.ToString("0000") + dataExt;
				}
			}
		}
	}
}