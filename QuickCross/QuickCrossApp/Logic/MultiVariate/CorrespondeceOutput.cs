using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ExcelAddIn.Common;
using log4net;
using Macromill.QCWeb.COMOperate;
using Macromill.QCWeb.Question;
using Microsoft.Office.Interop.Excel;
using Microsoft.VisualBasic.FileIO;
using QC4Common.Model;
using Constants = Qc4Launcher.Util.Constants;

namespace Qc4Launcher.Logic.MultiVariate
{
    class CorrespondeceOutput
    {

        string Str_Siborikomi = LocalResource.PSM_FILTER_CRITREA;
        Questions questions;
        public static string strItemPrefix = "[";
        string strItemSuffix = "]";
        string strEdit01 = LocalResource.PSM_FILTER_VAL;// "...value of...";
        string strEdit_LG = LocalResource.PSM_FILTER_LG;//"  excluding";
        string strEdit_E = "";//"";
        string strEdit_LE = LocalResource.PSM_FILTER_LE;//" or under";
        string strEdit_GE = LocalResource.PSM_FILTER_GE;//" or over";
        string strEdit_L = LocalResource.PSM_FILTER_L;//" under";
        string strEdit_G = LocalResource.PSM_FILTER_G;//" over";
        string strEdit_And = LocalResource.PSM_FILTER_AND;//"  AND";
        string strEdit_Or = LocalResource.PSM_FILTER_OR;//"  OR";
        string strNoAnswer = LocalResource.PSM_FILTER_NO_ANSWER;//"'No Answer'";
        string strNotApply = LocalResource.PSM_FILTER_NOT_APPLY;//"'Excluded'";

        public static string u_DK = "DK";
        public static string u_Asterisk = "*";


        public static string Sign_LG = "<>";
        public static string Sign_E = "=";
        public static string Sign_LE = "<=";
        public static string Sign_GE = ">=";
        public static string Sign_G = ">";
        public static string Sign_L = "<";
        public static string Sign_EX = "!";

        public static string D_AND = CrossSettingsReader.AND;
        public static string D_OR = CrossSettingsReader.OR;


        QC4Common.Util.FormUtil frmutil = new QC4Common.Util.FormUtil();
        public static string TEMPLATE_NAME = "CorrespondenceTemplate.xlsx";
        public static string TEMPLATE_NAME_JP = "CorrespondenceTemplate_JP.xlsx";
        public Application xlApp;
        public Workbooks wbs;
        Workbook workBook = null;
        Workbook baseBook = null;
        Worksheet dataSheet = null;
        Worksheet resultsheet = null;
        Worksheet Crossheet = null;
        Worksheet corressheet = null;
        Worksheet gtsheet = null;
        public string FileEncoding = "UTF-8";

        public string TemplateDirectoryPath;
        public string correspondence = "Sheet1";
        public string ResultSheet = "Sheet2";
        public string Data = "Sheet5";
        public string Cross = "Sheet4";
        public string GT = "Sheet3";

        string result_eig_File_Name = "result_eig.tsv";
        string result_col_coord_File_Name = "result_col_coord.tsv";
        string result_row_coord_File_Name = "result_row_coord.tsv";
        string input = "1.tsv";
        string filename;
        CorrespondenceSettings caSettings1;
        Dictionary<string, QuestionSettings> dic = new Dictionary<string, QuestionSettings>();
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        List<Questions> qs = new List<Questions>();
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        bool hascount;
        string[] colChoices = null;
        string[] rowChoices = null;
        public string EscapeCRLF(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {

                text = text.Replace("\t", QC4Common.Common.Constants.CRLFchar1);

            }
            return text;
        }
        public string UnEscapeCRLF(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                text = text.Replace(QC4Common.Common.Constants.CRLFchar1, "\t");
            }
            return text;
        }
        public void GenerateOutput(Workbook workbook, Application xlApp, bool hascount, Questions questions, string outPaths, string inputPath, string ipPath, CorrespondenceCalc calc, CorrespondenceSettings caSettings, string[] colChoices, string[] rowChoices)
        {
            Workbook baseBook = null;
            try
            {
                this.colChoices = colChoices;
                this.rowChoices = rowChoices;
                this.questions = questions;
                this.hascount = hascount;
                dic = Util.Definiotion.VariableDictionary;
                string outputPathFile = string.Empty;
                string inputPathFile = string.Empty;
                TemplateDirectoryPath = AppContext.BaseDirectory;
                caSettings1 = caSettings;
                // TemplateDirectoryPath = Path.Combine(TemplateDirectoryPath, "Templates");
                this.xlApp = xlApp;
                wbs = xlApp.Workbooks;
                //var res = Util.ExcelUtil.GetWorkSheetByCodeName(workBook, "");
                if (QC4Common.Common.Constants.GlobalMode.Split(',')[0] == "ja-JP")//Todo
                {
                    //language ="ja"
                    baseBook = wbs.Add(OutputUtil.getTemplatePath(TemplateDirectoryPath, TEMPLATE_NAME_JP, xlApp.PathSeparator));
                }
                else
                {
                    //language ="en"
                    baseBook = wbs.Add(OutputUtil.getTemplatePath(TemplateDirectoryPath, TEMPLATE_NAME, xlApp.PathSeparator));
                }
                ExcelUtil.GetWorkSheetByCodeName(baseBook, "Sheet3").Delete();
                ExcelUtil.GetWorkSheetByCodeName(baseBook, "Sheet4").Delete();

                corressheet = ExcelUtil.GetWorkSheetByCodeName(baseBook, correspondence);
                if (caSettings.tabulationType == 1)
                {
                    Crossheet = ExcelUtil.GetWorkSheetByCodeName(baseBook, "Sheet2");
                    ExcelUtil.GetWorkSheetByCodeName(baseBook, "Sheet5").Delete(); ;
                }
                else
                {
                    Crossheet = ExcelUtil.GetWorkSheetByCodeName(baseBook, "Sheet5");
                    ExcelUtil.GetWorkSheetByCodeName(baseBook, "Sheet2").Delete();
                }
                calc.updateProgress(70, LocalResource.PSM_PB_RESLT_OUT);

                Range clustervaluerar = corressheet.get_Range("A56:N56", "A74:N74");
                clustervaluerar.Delete();
                ChartObject chartObject = (ChartObject)corressheet.ChartObjects(2);
                chartObject.Delete();
                if (caSettings.tabulationType == 1)
                {
                    ReadCrossDataFile(inputPath, outPaths, caSettings);
                }
                else
                {
                    ReadGtFile(inputPath, outPaths, caSettings);
                }
                Encoding enc = Encoding.GetEncoding(FileEncoding);
                ReadInputValueTable(ipPath, caSettings, enc, "\t");
                corressheet.Name = LocalResource.MULTI_CORRESPONDANCE;
                calc.updateProgress(85, LocalResource.PSM_PB_RESLT_OUT);
                ChartUpdate();
            }
            catch(Exception ex)
            {
                _log.Error(ex.Message);
                _log.Error(ex.StackTrace);
                _log.Error(ex.Source);
                if (!ex.Message.Contains("OutOfMemoryException"))
                {
                    _log.LogError(ex.Message);
                }
               
            }
            finally
            {

                if (Crossheet != null)
                {
                    COMWholeOperate.releaseComObject(ref Crossheet);
                }
                if (corressheet != null)
                {
                    COMWholeOperate.releaseComObject(ref corressheet);
                }
                if (wbs != null)
                {
                    COMWholeOperate.releaseComObject(ref wbs);
                }
                if (baseBook != null)
                {
                    COMWholeOperate.releaseComObject(ref baseBook);
                }
            }
        }
        public void ReadGtFile(string Inputfile, string OutputFile, CorrespondenceSettings caSettings)
        {
            string resultFile = Path.Combine(OutputFile, result_eig_File_Name);
            string result_Col_file = Path.Combine(OutputFile, result_col_coord_File_Name);
            string result_row_file = Path.Combine(OutputFile, result_row_coord_File_Name);
            Encoding enc = Encoding.GetEncoding(FileEncoding);
            string[,] InputArray = ReadGTinputFile(Inputfile, enc, "\t", caSettings);
            List<string> RowChoices = new List<string>();
            List<string> ColChoices = new List<string>();
            List<string> RowSum = new List<string>();
            List<string> ColSum = new List<string>();
            string[,] ArrayData = new string[InputArray.GetLength(0) + 1, InputArray.GetLength(1) + 1];
            if (InputArray.GetLength(0) > 0 && InputArray.GetLength(1) > 0)
            {
                double sum = 0;
                for (int i = 0; i < InputArray.GetLength(1); i++)
                {
                    sum = 0;
                    double[] ar = new double[InputArray.GetLength(1)];
                    for (int j = 0; j < InputArray.GetLength(0); j++)
                    {
                        sum = sum + Convert.ToDouble(InputArray[j, i]);//column array
                    }
                    if (sum != 0)
                    {
                        ColChoices.Add(colChoices[i]);
                    }
                    ColSum.Add(sum.ToString());
                }


                for (int i = 0; i < InputArray.GetLength(0); i++)
                {
                    sum = 0;
                    double[] ar = new double[InputArray.GetLength(0)];
                    for (int j = 0; j < InputArray.GetLength(1); j++)
                    {
                        sum = sum + Convert.ToDouble(InputArray[i, j]);//row array

                    }
                    if (sum != 0)
                    {
                        RowChoices.Add(rowChoices[i]);
                    }
                    RowSum.Add(sum.ToString());
                }
               
            }
            string[,] RowDataArray = ReadCorresFile(result_row_file, enc, "\t", RowChoices, caSettings);
            string[,] ColDataArray = ReadCorresFile(result_Col_file, enc, "\t", ColChoices, caSettings);
            string[,] ResultArray = ReadResult(resultFile, enc, "\t");
           
            CrossSheetGeneration(RowDataArray, ColDataArray, ResultArray);
        }
        public string[,] ReadInputValueTable(String InputFile, CorrespondenceSettings caSettings, Encoding encode, string deLimiter)
        {
            List<List<string>> MultiList = new List<List<string>>();

            string[] lines = File.ReadAllLines(InputFile, encode);
            char del = Convert.ToChar(deLimiter);
            int lineCount = lines.Count();
            if (caSettings.tabulationType == 1)
            {
                lineCount = (lineCount - 1);
            }
            for (int i = 0; i < lineCount; i++)
            {
                List<string> RowList = new List<string>();


                string[] data = lines[i].Split(del);
                int dataColcount = data.Count();

                for (int j = 0; j < dataColcount; j++)
                {
                    if (j == (data.Count() - 3))
                    {
                        continue;
                    }
                    RowList.Add(data[j]);
                }

                if (caSettings.tabulationType == 1)
                {
                    int linecounted = lines.Count() - 2;
                    if (i == linecounted && data[3] == "0")
                    { continue; }

                }
                MultiList.Add(RowList);
            }

            List<double> noanserwerList = new List<double>();
            List<double> ValidCases = new List<double>();
            List<double> MeanValue = new List<double>();
            for (int i = 0; i < MultiList.Count; i++)
            {
                for (int j = 0; j < MultiList[i].Count; j++)
                {
                    if (i >= 3 && j == MultiList[i].Count() - 1)
                    {
                        double validcase = 0;
                        if (double.TryParse(MultiList[i][j], out validcase))
                        {
                            ValidCases.Add(validcase);
                        }
                        else
                        {

                        }
                    }
                    if (i >= 3 && j == MultiList[i].Count() - 2)
                    {
                        double meanvalue = 0;
                        if (double.TryParse(MultiList[i][j], out meanvalue))
                        {
                            MeanValue.Add(meanvalue);
                        }
                        else
                        {
                        }
                    }
                    if (i >= 3 && j == MultiList[i].Count() - 3)
                    {
                        double nonanswer = 0;
                        if (double.TryParse(MultiList[i][j], out nonanswer))
                        {
                            noanserwerList.Add(nonanswer);
                        }
                    }
                }
            }

            if (noanserwerList.Sum() == 0)
            {
                foreach (var item in MultiList)
                {
                    item.RemoveAt(item.Count - 3);
                }
            }
            //checking value has count
            if (!hascount)
            {
                if (MeanValue.Sum() == 0)
                {
                    foreach (var item in MultiList)
                    {
                        item.RemoveAt(item.Count - 2);
                    }
                }
                if (ValidCases.Sum() == 0)
                {
                    foreach (var item in MultiList)
                    {
                        item.RemoveAt(item.Count - 1);
                    }
                }
            }
            else
            {
                if (MeanValue.Sum() == 0)
                {
                    foreach (var item in MultiList)
                    {
                        item[item.Count - 2] = string.Empty;
                    }
                }

            }
            string[,] FullDataObject = new string[MultiList.Count(), MultiList[0].Count()];
            int li = 0;
            foreach (var item in MultiList)
            {
                int lj = 0;
                foreach (var lt in item)
                {
                    FullDataObject[li, lj] = lt;
                    lj++;
                }
                li++;
            }

            int countRows = FullDataObject.GetLength(0);
            int countColumns = FullDataObject.GetLength(1);
            Range c1 = Crossheet.Cells[3, 2];
            Range c2 = Crossheet.Cells[FullDataObject.GetLength(0) + 3, FullDataObject.GetLength(1) + 1];
            Range InputRange = Crossheet.get_Range(c1, c2);
            object[,] InputObject = InputRange.Value;
            if (countColumns > 0 && countRows > 0)
            {
                for (int i = 0; i < FullDataObject.GetLength(0); i++)
                {
                    for (int j = 0; j < FullDataObject.GetLength(1); j++)
                    {
                        if (i == 0 && j == 2)
                        {
                        }
                        else
                        {
                            InputObject[i + 2, j + 1] = frmutil.UnEscapeCRLF(UnEscapeCRLF(FullDataObject[i, j]));
                        }

                    }
                }
            }

            Crossheet.Cells[4, 1].EntireRow.NumberFormat = "@";
            Crossheet.Cells[5, 1].EntireRow.NumberFormat = "@";
            Crossheet.Cells[3, 1].EntireRow.NumberFormat = "@";
            Crossheet.Cells[3, 3].EntireColumn.NumberFormat = "@";
            Crossheet.Cells[3, 2].EntireColumn.NumberFormat = "@";

            //Data Value Range as numeric
            if (caSettings.tabulationType == 1)
            {
                Range cds1 = Crossheet.Cells[7, 6];
                Range cds2 = Crossheet.Cells[FullDataObject.GetLength(0) + 3, FullDataObject.GetLength(1) + 1];
                if (caSettings.calcType == 2)
                {
                    Crossheet.Range[cds1, cds2].NumberFormat = "0_ ";
                }
                else
                {
                    Crossheet.Range[cds1, cds2].NumberFormat = "0.0";
                }
              
                Range tow1 = Crossheet.Cells[5, 5];
                Range tow2 = Crossheet.Cells[FullDataObject.GetLength(0) + 3, 4];
                Crossheet.Range[tow1, tow2].NumberFormat = "0_ ";
            }
            else
            {
                Range cds1 = Crossheet.Cells[6, 7];
                Range cds2 = Crossheet.Cells[FullDataObject.GetLength(0) + 3, FullDataObject.GetLength(1) + 1];
                if (caSettings.calcType == 2)
                {
                    Crossheet.Range[cds1, cds2].NumberFormat = "0_ ";
                }
                else
                {
                    Crossheet.Range[cds1, cds2].NumberFormat = "0.0";
                }
                Range tow1 = Crossheet.Cells[6, 6];
                Range tow2 = Crossheet.Cells[FullDataObject.GetLength(0) + 3, 5];
                Crossheet.Range[tow1, tow2].NumberFormat = "0_ ";

            }

            
            if (caSettings1.tabulationType == 1)
            {
                //Changing number format for Weight value
                Range weightRStart = Crossheet.Cells[5, 6];
                Range weightREnd = Crossheet.Cells[5, FullDataObject.GetLength(1) + 1];
                if (!hascount)
                {
                    Crossheet.Range[weightRStart, weightREnd].NumberFormat = "[>=0](+##0);[<0](-##0)";
                    Crossheet.Range[weightRStart, weightREnd].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                }
                else
                {
                    Crossheet.Range[weightRStart, weightREnd].NumberFormat = "@";
                    Crossheet.Range[weightRStart, weightREnd].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                }
                //Changing number format for Weight value
            }



            InputRange.Value = InputObject;
            if (ValidCases.Sum() != 0)
            {
                Range weightRStart = Crossheet.Cells[6, FullDataObject.GetLength(1) + 1];
                Range weightREnd = Crossheet.Cells[FullDataObject.GetLength(0) + 3, FullDataObject.GetLength(1) + 1];
                Crossheet.Range[weightRStart, weightREnd].NumberFormat = "0.0";

            }
            if (MeanValue.Sum() != 0)
            {
                Range weightRStart = Crossheet.Cells[4, FullDataObject.GetLength(1)];
                Range weightREnd = Crossheet.Cells[FullDataObject.GetLength(0) + 3, FullDataObject.GetLength(1)];
                Crossheet.Range[weightRStart, weightREnd].NumberFormat = "0";

            }

            if (caSettings.tabulationType == 1)
            {

                Range c3 = Crossheet.Cells[6, 2];
                Range c4 = Crossheet.Cells[6, FullDataObject.GetLength(1) + 1];
                Range deleteRange = Crossheet.get_Range(c3, c4);
                deleteRange.Delete(XlDeleteShiftDirection.xlShiftUp);


                //Remove d3 row Values
                string rowid = "D" + (FullDataObject.GetLength(0) + 2);
                Range d3Firstcol = Crossheet.get_Range("D3:" + rowid);
                d3Firstcol.Value = null;
                //Row Header choice
                Range rowHeader = Crossheet.Cells[4, 6];
                Range rowHeaderE = Crossheet.Cells[5, FullDataObject.GetLength(1) + 1];
                Crossheet.Range[rowHeader, rowHeaderE].Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
                Crossheet.Range[rowHeader, rowHeaderE].Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
                Crossheet.Range[rowHeader, rowHeaderE].Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
                Crossheet.Range[rowHeader, rowHeaderE].Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlHairline;
                Crossheet.Range[rowHeader, rowHeaderE].Borders[XlBordersIndex.xlInsideVertical].Color = 10921638;
                Crossheet.Range[rowHeader, rowHeaderE].Borders[XlBordersIndex.xlEdgeLeft].Color = 10921638;
                Crossheet.Range[rowHeader, rowHeaderE].Borders[XlBordersIndex.xlEdgeTop].Color = 10921638;
                Crossheet.Range[rowHeader, rowHeaderE].Borders[XlBordersIndex.xlEdgeBottom].Color = 10921638;
                Crossheet.Range[rowHeader, rowHeaderE].WrapText = true;
                //Total text and Grand total column
                Range gndtxt = Crossheet.Cells[4, 5];
                Range gndnum = Crossheet.Cells[5, 5];
                Crossheet.Range[gndtxt, gndnum].Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
                Crossheet.Range[gndtxt, gndnum].Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
                Crossheet.Range[gndtxt, gndnum].Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
                Crossheet.Range[gndtxt, gndnum].Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
                Crossheet.Range[gndtxt, gndnum].Borders[XlBordersIndex.xlEdgeBottom].Color = 10921638;
                Crossheet.Range[gndtxt, gndnum].Borders[XlBordersIndex.xlEdgeTop].Color = 10921638;
                Crossheet.Range[gndtxt, gndnum].Borders[XlBordersIndex.xlEdgeLeft].Color = 10921638;
                Crossheet.Range[gndtxt, gndnum].Borders[XlBordersIndex.xlEdgeRight].Color = 10921638;
                Crossheet.Range[gndtxt, gndnum].WrapText = true;
                //Row  Total
                Range rowTot = Crossheet.Cells[6, 2];
                Range rowTot1 = Crossheet.Cells[6, 4];
                Crossheet.Range[rowTot, rowTot1].Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
                Crossheet.Range[rowTot, rowTot1].Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
                Crossheet.Range[rowTot, rowTot1].Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
                Crossheet.Range[rowTot, rowTot1].Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
                Crossheet.Range[rowTot, rowTot1].Borders[XlBordersIndex.xlEdgeLeft].Color = 10921638;
                Crossheet.Range[rowTot, rowTot1].Borders[XlBordersIndex.xlEdgeRight].Color = 10921638;
                Crossheet.Range[rowTot, rowTot1].Borders[XlBordersIndex.xlEdgeTop].Color = 10921638;
                Crossheet.Range[rowTot, rowTot1].Borders[XlBordersIndex.xlEdgeBottom].Color = 10921638;
                Crossheet.Range[rowTot, rowTot1].Merge();
                Crossheet.Range[rowTot, rowTot1].WrapText = true;
                Crossheet.Range[rowTot, rowTot1].Rows.AutoFit();
                Crossheet.Range[rowTot, rowTot1].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                //Column total
                string colTotstr = "E" + (FullDataObject.GetLength(0) + 2);
                Range colTotF = Crossheet.get_Range("E7:" + colTotstr);
                colTotF.Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
                colTotF.Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
                colTotF.Borders[XlBordersIndex.xlInsideHorizontal].Weight = XlBorderWeight.xlHairline;
                colTotF.Borders[XlBordersIndex.xlInsideHorizontal].Color = 10921638;
                colTotF.Borders[XlBordersIndex.xlEdgeTop].Color = 10921638;
                colTotF.Borders[XlBordersIndex.xlEdgeLeft].Color = 10921638;
                //Row Choices
                Range rowChoices = Crossheet.Cells[7, 3];
                Range rowChoiceslast = Crossheet.Cells[FullDataObject.GetLength(0) + 2, 4];
                Crossheet.Range[rowChoices, rowChoiceslast].Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
                Crossheet.Range[rowChoices, rowChoiceslast].Borders[XlBordersIndex.xlInsideHorizontal].Weight = XlBorderWeight.xlHairline;
                Crossheet.Range[rowChoices, rowChoiceslast].Borders[XlBordersIndex.xlInsideHorizontal].Color = 10921638;
                Crossheet.Range[rowChoices, rowChoiceslast].Borders[XlBordersIndex.xlEdgeLeft].Color = 10921638;
                Crossheet.Range[rowChoices, rowChoiceslast].WrapText = true;
                ///Row Question Header
                Range RowQs = Crossheet.Cells[7, 2];
                Range RowQsl = Crossheet.Cells[FullDataObject.GetLength(0) + 2, 2];
                Crossheet.Range[RowQs, RowQsl].Merge();
                Crossheet.Range[RowQs, RowQsl].WrapText = true;
                Crossheet.Range[RowQs, RowQsl].Rows.AutoFit();
                Crossheet.Range[RowQs, RowQsl].VerticalAlignment = XlVAlign.xlVAlignTop;
                //Column Header
                Range Colh = Crossheet.Cells[3, 5];
                Range Colhl = Crossheet.Cells[3, FullDataObject.GetLength(1) + 1];
                QuestionSettings qstnDet = Qc4Launcher.Util.Definiotion.VariableDictionary[caSettings.crColVar];
                Questions.Question itemInfo = (Questions.Question)questions[qstnDet.Id];
                //Crossheet.Cells[4, 2] = itemInfo.Number;
                Crossheet.Cells[6, 2] = FullDataObject[0, 3];

                Crossheet.Range[Colh, Colhl].Merge();
                Crossheet.Range[Colh, Colhl].WrapText = true;
                Crossheet.Range[Colh, Colhl].Rows.AutoFit();
                Crossheet.Range[Colh, Colhl].HorizontalAlignment = XlHAlign.xlHAlignLeft;
                float height = 0;
                string tbandqs = string.Format(qstnDet.TableHeading);
                if (tbandqs != string.Empty || !string.IsNullOrWhiteSpace(tbandqs))
                {
                    tbandqs += Environment.NewLine + qstnDet.Question;
                    if (!string.IsNullOrEmpty(itemInfo.Number2))
                    { tbandqs = itemInfo.Number + Environment.NewLine + tbandqs; }
                    height = MeasureString(tbandqs, 9, "Segoe UI");
                }
                else
                {
                    tbandqs = qstnDet.Question;
                    if (!string.IsNullOrEmpty(itemInfo.Number2))
                    { tbandqs = itemInfo.Number + Environment.NewLine + tbandqs; }
                    height = MeasureString(tbandqs, 9, "Segoe UI");
                }
                Crossheet.Cells[3, 5] = tbandqs;
                if (height > 11.25)
                {
                    Crossheet.Range[Colh, Colhl].Rows.RowHeight = height;
                }




                Range FROw = Crossheet.Cells[3, 2];
                Range FROwEnd = Crossheet.Cells[5, 4];
                Crossheet.Range[FROw, FROwEnd].Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
                Crossheet.Range[FROw, FROwEnd].Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
                Crossheet.Range[FROw, FROwEnd].Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
                Crossheet.Range[FROw, FROwEnd].Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
                Crossheet.Range[FROw, FROwEnd].Borders[XlBordersIndex.xlEdgeBottom].Color = 10921638;
                Crossheet.Range[FROw, FROwEnd].Borders[XlBordersIndex.xlEdgeTop].Color = 10921638;
                Crossheet.Range[FROw, FROwEnd].Borders[XlBordersIndex.xlEdgeLeft].Color = 10921638;
                Crossheet.Range[FROw, FROwEnd].Borders[XlBordersIndex.xlEdgeRight].Color = 10921638;
                //ROw TOtal
                Range rowtot = Crossheet.Cells[6, 6];
                Range rowtotE = Crossheet.Cells[6, FullDataObject.GetLength(1) + 1];
                Crossheet.Range[rowtot, rowtotE].Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlHairline;
                Crossheet.Range[rowtot, rowtotE].Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
                Crossheet.Range[rowtot, rowtotE].Borders[XlBordersIndex.xlEdgeLeft].Color = 10921638;
                Crossheet.Range[rowtot, rowtotE].Borders[XlBordersIndex.xlInsideVertical].Color = 10921638;

                //Data Value range
                Range valueF = Crossheet.Cells[7, 6];
                Range valueE = Crossheet.Cells[FullDataObject.GetLength(0) + 2, FullDataObject.GetLength(1) + 1];
                Crossheet.Range[valueF, valueE].Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
                Crossheet.Range[valueF, valueE].Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
                Crossheet.Range[valueF, valueE].Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlHairline;
                Crossheet.Range[valueF, valueE].Borders[XlBordersIndex.xlInsideHorizontal].Weight = XlBorderWeight.xlHairline;
                Crossheet.Range[valueF, valueE].Borders[XlBordersIndex.xlEdgeLeft].Color = 10921638;
                Crossheet.Range[valueF, valueE].Borders[XlBordersIndex.xlEdgeTop].Color = 10921638;
                Crossheet.Range[valueF, valueE].Borders[XlBordersIndex.xlInsideHorizontal].Color = 10921638;
                Crossheet.Range[valueF, valueE].Borders[XlBordersIndex.xlInsideVertical].Color = 10921638;
                if (!String.IsNullOrEmpty(qstnDet.Score))
                {
                    Range countCol = Crossheet.Cells[3, FullDataObject.GetLength(1) - 1];
                    Range countRow = Crossheet.Cells[FullDataObject.GetLength(0) + 2, FullDataObject.GetLength(1) + 1];
                    Crossheet.Range[countCol, countRow].Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlThin;
                    Crossheet.Range[countCol, countRow].Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlThin;
                    Crossheet.Range[countCol, countRow].Borders[XlBordersIndex.xlEdgeLeft].Weight = XlBorderWeight.xlHairline;
                    Crossheet.Range[countCol, countRow].Borders[XlBordersIndex.xlEdgeLeft].Color = 10921638;
                    Crossheet.Range[countCol, countRow].Borders[XlBordersIndex.xlInsideVertical].Color = 10921638;

                }
                //main Boarder
                InputRange.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
                InputRange.Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
                InputRange.Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
                InputRange.Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
                InputRange.Borders[XlBordersIndex.xlEdgeLeft].Color = 10921638;
                InputRange.Borders[XlBordersIndex.xlEdgeBottom].Color = 10921638;
                InputRange.Borders[XlBordersIndex.xlEdgeTop].Color = 10921638;
                InputRange.Borders[XlBordersIndex.xlEdgeRight].Color = 10921638;
                //Color Region
                Range colorColumnH = Crossheet.Cells[3, 5];
                Range colorColumnHl = Crossheet.Cells[5, FullDataObject.GetLength(1) + 1];
                Crossheet.Range[colorColumnH, colorColumnHl].Interior.Color = 15921906;


                Range colorRowH = Crossheet.Cells[6, 2];
                Range colorRowHl = Crossheet.Cells[FullDataObject.GetLength(0) + 2, 4];
                Crossheet.Range[colorRowH, colorRowHl].Interior.Color = 15853276;


                Range colorToT = Crossheet.Cells[6, 5];
                Range colorToTl = Crossheet.Cells[FullDataObject.GetLength(0) + 2, 5];
                Crossheet.Range[colorToT, colorToTl].Interior.Color = 14277081;



            }
            else if (caSettings.tabulationType == 2)
            {
                Range c3 = Crossheet.Cells[3, 2];
                Range c4 = Crossheet.Cells[3, FullDataObject.GetLength(1) + 1];
                Range deleteRange = Crossheet.get_Range(c3, c4);
                deleteRange.Delete(XlDeleteShiftDirection.xlShiftUp);

                Range c5 = Crossheet.Cells[3, 5];
                Range c6 = Crossheet.Cells[FullDataObject.GetLength(0) + 2, 5];
                Range deleteRange1 = Crossheet.get_Range(c5, c6);
                deleteRange1.Delete(XlDeleteShiftDirection.xlShiftToLeft);

                // Crossheet.Cells[4, 2] = questions.Values[name;
                QuestionSettings qstnDet = Qc4Launcher.Util.Definiotion.VariableDictionary[caSettings.gtVars[0]];
                Questions.Question itemInfo = (Questions.Question)questions[qstnDet.Id];
                Crossheet.Cells[4, 2] = itemInfo.Number;
                Crossheet.Cells[4, 2].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                Crossheet.get_Range("B4", "B4").Cells.Font.Bold = true;
                //
                Crossheet.Cells[4, 3] = dic[caSettings.gtVars[0]].TableHeading;
                Crossheet.Cells[4, 3].VerticalAlignment = XlVAlign.xlVAlignBottom;
                Crossheet.Cells[4, 3].WrapText = true;
                Crossheet.Cells[4, 3].Font.Bold = true;


                //Answer Type
                Crossheet.Cells[5, 3] = FullDataObject[0, 1];
                Crossheet.get_Range("C5", "C5").Cells.Font.Size = 8;
                Crossheet.get_Range("C5", "C5").Cells.Font.Bold = false;



                Range c15 = Crossheet.Cells[3, 4];
                Range c16 = Crossheet.Cells[4, FullDataObject.GetLength(1) + 1];
                Crossheet.Range[c15, c16].Style.WrapText = true;
                c15.Style.WrapText = true;
                //Crossheet.Range[c15, c16].Rows.AutoFit();

                Range color1 = Crossheet.Cells[6, 5];
                Range color2 = Crossheet.Cells[FullDataObject.GetLength(0) + 2, 5];
                Crossheet.Range[color1, color2].Interior.Color = 14277081;
                //Row Header color
                Range color3 = Crossheet.Cells[6, 2];
                Range color4 = Crossheet.Cells[FullDataObject.GetLength(0) + 2, 4];
                Crossheet.Range[color3, color4].Interior.Color = 15853276;


                //First Column

                Range XlFirstcol = Crossheet.get_Range("B3:B5");
                XlFirstcol.Borders[XlBordersIndex.xlEdgeRight].Weight = XlBorderWeight.xlHairline;
                XlFirstcol.Borders[XlBordersIndex.xlEdgeRight].Color = 10921638;

                Range dec = Crossheet.Cells[3, 5];
                Range e3end = Crossheet.Cells[5, FullDataObject.GetLength(1)];
                Crossheet.Range[dec, e3end].Interior.Color = 15921906;
                Crossheet.Range[dec, e3end].WrapText = true;
                //Column Header Color
                //Range numeric = corressheet.Cells[3, 6];



                Range cl1 = Crossheet.Cells[3, 6];
                Range cl2 = Crossheet.Cells[5, FullDataObject.GetLength(1)];
                Crossheet.Range[cl1, cl2].Interior.Color = 15921906;
                Crossheet.Range[cl1, cl2].Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
                Crossheet.Range[cl1, cl2].Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
                Crossheet.Range[cl1, cl2].Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
                Crossheet.Range[cl1, cl2].Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlHairline;
                Crossheet.Range[cl1, cl2].Borders[XlBordersIndex.xlInsideVertical].Color = 10921638;
                Crossheet.Range[cl1, cl2].Borders[XlBordersIndex.xlEdgeRight].Color = 10921638;
                Crossheet.Range[cl1, cl2].Borders[XlBordersIndex.xlEdgeLeft].Color = 10921638;
                Crossheet.Range[cl1, cl2].Borders[XlBordersIndex.xlEdgeBottom].Color = 10921638;
                Crossheet.Range[cl1, cl2].WrapText = true;
                Crossheet.Range[cl1, cl2].Rows.AutoFit();


                Range cl3 = Crossheet.Cells[4, 6];
                Range cl4 = Crossheet.Cells[5, FullDataObject.GetLength(1)];
                Crossheet.Range[cl3, cl4].Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlDot;
                Crossheet.Range[cl3, cl4].Borders[XlBordersIndex.xlEdgeTop].Weight = XlBorderWeight.xlHairline;
                Crossheet.Range[cl3, cl4].Borders[XlBordersIndex.xlEdgeTop].Color = 10921638;
                Crossheet.Range[cl3, cl4].WrapText = true;
                Crossheet.Range[cl3, cl4].Rows.AutoFit();
                //Row num and Row Value

                Range rownum = Crossheet.Cells[6, 2];
                Range rownumend = Crossheet.Cells[FullDataObject.GetLength(0) + 2, 4];
                Range RowNameFileld = Crossheet.get_Range(rownum, rownumend);
                RowNameFileld.Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
                RowNameFileld.Borders[XlBordersIndex.xlInsideHorizontal].Weight = XlBorderWeight.xlHairline;
                RowNameFileld.Borders[XlBordersIndex.xlInsideHorizontal].Color = 10921638;
                RowNameFileld.Borders[XlBordersIndex.xlEdgeTop].Color = 10921638;
                RowNameFileld.WrapText = true;

                string b6Row = "B" + (FullDataObject.GetLongLength(0) + 2);
                Range B6Po = Crossheet.get_Range("B6:" + b6Row);
                B6Po.Borders[XlBordersIndex.xlEdgeRight].Weight = XlBorderWeight.xlHairline;
                B6Po.Borders[XlBordersIndex.xlEdgeRight].Color = 10921638;
                B6Po.HorizontalAlignment = XlHAlign.xlHAlignRight;

                //Total Heading
                Range TotalHeading = Crossheet.get_Range("E3:E5");
                TotalHeading.Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
                TotalHeading.Borders[XlBordersIndex.xlEdgeLeft].Color = 10921638;
                //TotalRow
                string dddd = "E" + (FullDataObject.GetLength(0) + 2);
                Range TotalH = Crossheet.get_Range("E6:" + dddd);
                TotalH.Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
                TotalH.Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
                TotalH.Borders[XlBordersIndex.xlInsideHorizontal].Weight = XlBorderWeight.xlHairline;
                TotalH.Borders[XlBordersIndex.xlInsideHorizontal].Color = 10921638;
                TotalH.Borders[XlBordersIndex.xlEdgeTop].Color = 10921638;
                TotalH.Borders[XlBordersIndex.xlEdgeLeft].Color = 10921638;



                //  //value field update
                Range ValueF = Crossheet.Cells[6, 6];
                Range ValueH = Crossheet.Cells[FullDataObject.GetLength(0) + 2, FullDataObject.GetLength(1)];
                Crossheet.Range[ValueF, ValueH].Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
                Crossheet.Range[ValueF, ValueH].Borders[XlBordersIndex.xlInsideHorizontal].Weight = XlBorderWeight.xlHairline;
                Crossheet.Range[ValueF, ValueH].Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlHairline;
                Crossheet.Range[ValueF, ValueH].Borders[XlBordersIndex.xlEdgeLeft].Color = 10921638;
                Crossheet.Range[ValueF, ValueH].Borders[XlBordersIndex.xlInsideVertical].Color = 10921638;
                Crossheet.Range[ValueF, ValueH].Borders[XlBordersIndex.xlInsideHorizontal].Color = 10921638;


                InputRange.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
                InputRange.Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
                InputRange.Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
                InputRange.Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
                InputRange.Borders[XlBordersIndex.xlEdgeLeft].Color = 10921638;
                InputRange.Borders[XlBordersIndex.xlEdgeBottom].Color = 10921638;
                InputRange.Borders[XlBordersIndex.xlEdgeTop].Color = 10921638;
                InputRange.Borders[XlBordersIndex.xlEdgeRight].Color = 10921638;
                Crossheet.Cells[3, 3] = null;
                Questions.Question itemInfo1 = (Questions.Question)questions[qstnDet.ItemId];
                if (!String.IsNullOrEmpty(qstnDet.Score))
                {
                    Range countCol = Crossheet.Cells[3, FullDataObject.GetLength(1) - 2];
                    Range countRow = Crossheet.Cells[FullDataObject.GetLength(0) + 2, FullDataObject.GetLength(1)];
                    Crossheet.Range[countCol, countRow].Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlThin;
                    Crossheet.Range[countCol, countRow].Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlThin;
                    Crossheet.Range[countCol, countRow].Borders[XlBordersIndex.xlEdgeLeft].Color = 10921638;

                }

            }

            if (caSettings.calcType == 2)
            {
                Crossheet.Name = LocalResource.LABEL_N_;
            }
            else
            {

                Crossheet.Name = LocalResource.REPORT_CROSS_P_SHEET_NAME;
            }
            if (caSettings1.HasFilter)
            {
                string cd = "B" + (FullDataObject.GetLength(0) + 7);
                Range TargetRange = Crossheet.Range[cd];
                FNC_An_ConditionalOutput(TargetRange, 3);
            }
            if (caSettings1.tabulationType == 2)
            {
                //Changing number format for Weight value
                Range weightRStart = Crossheet.Cells[5, 6];
                Range weightREnd = Crossheet.Cells[5, FullDataObject.GetLength(1) + 1];
                if (!hascount)
                {
                    Crossheet.Range[weightRStart, weightREnd].NumberFormat = "[>=0](+##0);[<0](-##0)";
                    Crossheet.Range[weightRStart, weightREnd].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                }
                else
                {
                    Crossheet.Range[weightRStart, weightREnd].NumberFormat = "@";
                    if (caSettings1.calcType == 2)
                    {
                        Crossheet.Range[weightRStart, weightREnd].HorizontalAlignment = XlHAlign.xlHAlignRight;
                    }
                    else
                    {
                        Crossheet.Range[weightRStart, weightREnd].HorizontalAlignment = XlHAlign.xlHAlignLeft;
                    }
                }
                //Changing number format for Weight value




            }

            return FullDataObject;
        }
        public float MeasureString(string text, double fontSize, string typeFace)
        {
            System.Windows.Media.FormattedText ft = new System.Windows.Media.FormattedText
            (
               text,
               CultureInfo.CurrentCulture,
               System.Windows.FlowDirection.LeftToRight,
               new System.Windows.Media.Typeface(typeFace),
               fontSize,
               System.Windows.Media.Brushes.Black
            );
            return (float)ft.Height;
        }
        public void ReadCrossDataFile(string Inputfile, string OutputFile, CorrespondenceSettings caSettings)
        {
            string resultFile = Path.Combine(OutputFile, result_eig_File_Name);
            string result_Col_file = Path.Combine(OutputFile, result_col_coord_File_Name);
            string result_row_file = Path.Combine(OutputFile, result_row_coord_File_Name);

            Encoding enc = Encoding.GetEncoding(FileEncoding);
            string[,] InputArray = ReadCorresFileResult(Inputfile, enc, "\t", caSettings);
            List<string> RowChoices = new List<string>();
            List<string> ColChoices = new List<string>();
            List<string> RowSum = new List<string>();
            List<string> ColSum = new List<string>();
            string[,] ArrayData = new string[InputArray.GetLength(0) + 1, InputArray.GetLength(1) + 1];
            if (InputArray.GetLength(0) > 0 && InputArray.GetLength(1) > 0)
            {
                double sum = 0;
                for (int i = 0; i < InputArray.GetLength(1); i++)
                {
                    sum = 0;
                    double[] ar = new double[InputArray.GetLength(1)];
                    for (int j = 0; j < InputArray.GetLength(0); j++)
                    {
                        sum = sum + Convert.ToDouble(InputArray[j, i]);//column array

                    }
                    if (sum != 0)
                    {
                        // ColChoices.Add(InputArray[0, i]);
                        ColChoices.Add(colChoices[i]);
                    }
                    ColSum.Add(sum.ToString());
                }


                for (int i = 0; i < InputArray.GetLength(0); i++)
                {
                    sum = 0;
                    double[] ar = new double[InputArray.GetLength(0)];
                    for (int j = 0; j < InputArray.GetLength(1); j++)
                    {
                        sum = sum + Convert.ToDouble(InputArray[i, j]);//row array

                    }
                    if (sum != 0)
                    {
                        RowChoices.Add(rowChoices[i]);
                    }
                    RowSum.Add(sum.ToString());
                }



            }


            string[,] RowDataArray = ReadCorresFile(result_row_file, enc, "\t", RowChoices, caSettings);
            string[,] ColDataArray = ReadCorresFile(result_Col_file, enc, "\t", ColChoices, caSettings);
            string[,] ResultArray = ReadResult(resultFile, enc, "\t");
            CrossSheetGeneration(RowDataArray, ColDataArray, ResultArray);
        }
        public void inputArrayCOnversion(string[,] InputArray, List<string> RowSum, List<string> ColumnSum, CorrespondenceSettings caSettings)
        {
            int countRows = InputArray.GetLength(0);
            int countColumns = InputArray.GetLength(1);
            Range c1 = Crossheet.Cells[3, 2];
            Range c2 = Crossheet.Cells[InputArray.GetLength(0) + 4, InputArray.GetLength(1) + 3];
            Range InputRange = Crossheet.get_Range(c1, c2);
            object[,] ObjectInput = InputRange.Value;
            System.Data.DataTable dt = new System.Data.DataTable();

            if (RowSum.Count() > 0 && ColumnSum.Count > 0)
            {
                for (int i = 0; i < countRows; i++)
                {
                    for (int j = 0; j < countColumns; j++)
                    {
                        if (i > 0 && j > 0)
                        {
                            ObjectInput[i + 3, j + 3] = InputArray[i, j];
                        }
                    }
                }
                for (int i = 0; i < RowSum.Count; i++)
                {
                    ObjectInput[i + 4, 3] = RowSum[i];
                    ObjectInput[i + 4, 2] = InputArray[i + 1, 0];
                    if (caSettings.tabulationType == 2)
                    {
                        ObjectInput[i + 4, 1] = i + 1;
                    }
                }
                double sum = 0;
                for (int i = 0; i < ColumnSum.Count; i++)
                {
                    if (caSettings.tabulationType == 1)
                    {
                        sum = Convert.ToDouble(ColumnSum[i]) + sum;
                        ObjectInput[3, i + 4] = ColumnSum[i];
                    }
                    ObjectInput[2, i + 4] = InputArray[0, i + 1];

                }
                if (caSettings.tabulationType == 1)
                {
                    ObjectInput[3, 3] = sum;
                }
            }
            InputRange.Value = ObjectInput;
            if (caSettings.tabulationType == 1)
            {


            }

            Crossheet.Name = LocalResource.LABEL_N_;

        }
        public void CrossSheetGeneration(string[,] RowDataArray, string[,] ColDataArray, string[,] ResultArray)
        {
            string lCol = string.Empty;
            string RCol = string.Empty;
            string[] Variables;

            lCol = "B" + (51 + ColDataArray.GetLength(0));
            RCol = "D" + (51 + ColDataArray.GetLength(1));
            String RangeCol = lCol + ":" + RCol;


            Range ColDataRange = corressheet.get_Range("B52:D52", RangeCol);
            object[,] data = ColDataRange.Value;
            for (int i = 0; i < ColDataArray.GetLength(0); i++)
            {
                data[i + 1, 1] = frmutil.UnEscapeCRLF(UnEscapeCRLF(ColDataArray[i, 0]));
                data[i + 1, 2] = ColDataArray[i, 1];
                data[i + 1, 3] = ColDataArray[i, 2];
            }
            Range ChangeChoice = corressheet.get_Range("B52:B" + (51 + ColDataArray.GetLength(0)));
            ChangeChoice.NumberFormat = "@";
            ColDataRange.Value = data;
            corressheet.Columns["H:H"].ColumnWidth = 13.5;
            corressheet.Columns["G:G"].ColumnWidth = 13.5;

            ColDataRange.Cells.Style.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            ChangeChoice.Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
            ChangeChoice.Borders[XlBordersIndex.xlEdgeLeft].Color = 10921638;
            ChangeChoice.Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
            ChangeChoice.Borders[XlBordersIndex.xlEdgeRight].Color = 10921638;
            ChangeChoice.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
            ChangeChoice.Borders[XlBordersIndex.xlEdgeBottom].Color = 10921638;

            ChangeChoice.Borders[XlBordersIndex.xlInsideHorizontal].Color = 10921638;
            ChangeChoice.Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlHairline;
            ChangeChoice.Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;

            Range C52toEnd = corressheet.get_Range("C52:D" + (51 + ColDataArray.GetLength(0)));
            C52toEnd.Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
            C52toEnd.Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
            C52toEnd.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
            C52toEnd.Borders[XlBordersIndex.xlEdgeRight].Color = 10921638;
            C52toEnd.Borders[XlBordersIndex.xlEdgeTop].Color = 10921638;
            C52toEnd.Borders[XlBordersIndex.xlEdgeBottom].Color = 10921638;
            C52toEnd.Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlHairline;
            C52toEnd.Borders[XlBordersIndex.xlInsideVertical].Color = 10921638;
            C52toEnd.Borders[XlBordersIndex.xlInsideHorizontal].Weight = XlBorderWeight.xlHairline;
            C52toEnd.Borders[XlBordersIndex.xlInsideHorizontal].Color = 10921638;

            for (int i = 0; i < ColDataArray.GetLength(0); i++)
            {

                ColDataRange.Cells[i + 1, 1].Interior.Color = 15921906;
                ColDataRange.Cells[i + 1, 2].HorizontalAlignment = XlHAlign.xlHAlignRight;
                ColDataRange.Cells[i + 1, 3].HorizontalAlignment = XlHAlign.xlHAlignRight;

            }



            lCol = "F" + (51 + RowDataArray.GetLength(0));
            RCol = "H" + (51 + RowDataArray.GetLength(1));
            RangeCol = lCol + ":" + RCol;
            Range RowDataRange = corressheet.get_Range("F52:H52", RangeCol);
            object[,] RangeRow = RowDataRange.Value;
            for (int i = 0; i < RowDataArray.GetLength(0); i++)
            {
                RangeRow[i + 1, 1] = frmutil.UnEscapeCRLF(UnEscapeCRLF(RowDataArray[i, 0]));
                RangeRow[i + 1, 2] = RowDataArray[i, 1];
                RangeRow[i + 1, 3] = RowDataArray[i, 2];
            }
            Range ChangeChoice1 = corressheet.get_Range("F52:F" + (51 + RowDataArray.GetLength(0)));
            ChangeChoice1.NumberFormat = "@";
            RowDataRange.Value = RangeRow;
            ChangeChoice1.Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
            ChangeChoice1.Borders[XlBordersIndex.xlEdgeLeft].Color = 10921638;
            ChangeChoice1.Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
            ChangeChoice1.Borders[XlBordersIndex.xlEdgeRight].Color = 10921638;
            ChangeChoice1.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
            ChangeChoice1.Borders[XlBordersIndex.xlEdgeBottom].Color = 10921638;

            ChangeChoice1.Borders[XlBordersIndex.xlInsideHorizontal].Color = 10921638;
            ChangeChoice1.Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlHairline;
            ChangeChoice1.Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;

            Range F52toEnd = corressheet.get_Range("G52:H" + (51 + RowDataArray.GetLength(0)));
            F52toEnd.Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
            F52toEnd.Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
            F52toEnd.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
            F52toEnd.Borders[XlBordersIndex.xlEdgeRight].Color = 10921638;
            F52toEnd.Borders[XlBordersIndex.xlEdgeTop].Color = 10921638;
            F52toEnd.Borders[XlBordersIndex.xlEdgeBottom].Color = 10921638;
            F52toEnd.Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlHairline;
            F52toEnd.Borders[XlBordersIndex.xlInsideVertical].Color = 10921638;
            F52toEnd.Borders[XlBordersIndex.xlInsideHorizontal].Weight = XlBorderWeight.xlHairline;
            F52toEnd.Borders[XlBordersIndex.xlInsideHorizontal].Color = 10921638;

            for (int i = 0; i < RowDataArray.GetLength(0); i++)
            {

                RowDataRange.Cells[i + 1, 1].Interior.Color = 15853276;
                RowDataRange.Cells[i + 1, 2].HorizontalAlignment = XlHAlign.xlHAlignRight;
                RowDataRange.Cells[i + 1, 3].HorizontalAlignment = XlHAlign.xlHAlignRight;

            }

            Range DataResultRange = corressheet.get_Range("D47:E47", "D48:E48");
            object[,] ResultRange = DataResultRange.Value;
            for (int i = 0; i < ResultArray.GetLength(0); i++)
            {
                for (int j = 0; j < ResultArray.GetLength(1); j++)
                {
                    if (j == 0)
                    {
                        ResultRange[i + 1, j + 1] = ResultArray[i, j];
                    }
                    else
                    {
                        double percenTage = 0;
                        if (double.TryParse((ResultArray[i, j]), out percenTage))
                        {
                            if (percenTage > 0)
                            {
                                ResultRange[i + 1, j + 1] = percenTage / 100;
                            }
                            else
                            {
                                ResultRange[i + 1, j + 1] = percenTage;
                            }
                        }
                        else
                        {
                            ResultRange[i + 1, j + 1] = ((ResultArray[i, j]));
                        }
                    }

                }
            }
            DataResultRange.Value = ResultRange;



        }
        public void ChartUpdate()
        {
            ChartObject chartObject = (ChartObject)corressheet.ChartObjects(1);
            chartObject.Activate();
            chartObject.Chart.ChartStyle = 4;

            Range dpstart = corressheet.Cells[52, 2];
            Range lastcell = ExcelUtil.EndxlUp(corressheet.Cells[52, 4]);
            Range rar = corressheet.Range[dpstart, lastcell];
            object[,] obj = rar.Value;
            Output_C3_Graph(rar, chartObject, obj);


            Range gstart = corressheet.Cells[52, 6];
            Range gend = ExcelUtil.EndxlUp(corressheet.Cells[52, 8]);
            Range rar1 = corressheet.Range[gstart, gend];
            object[,] obj1 = rar1.Value;
            Output_C3_Graph1(rar1, chartObject, obj1);

            chartObject.Chart.PlotArea.Interior.Color = 16777215;
            string Shapes1 = "各軸の説明度";
            Shape s;
            if (QC4Common.Common.Constants.GlobalMode.Split(',')[0] == "ja-JP")//Todo
            {
                s = corressheet.Shapes.Item("各軸の説明度");
            }
            else
            {
                s = corressheet.Shapes.Item("% of variance by each factor");
            }


            Axis Xaxis = chartObject.Chart.Axes(XlAxisType.xlCategory, XlAxisGroup.xlPrimary) as Axis;

            Axis Y2axis = chartObject.Chart.Axes(XlAxisType.xlValue) as Axis;

            Xaxis.AxisTitle.Text = LocalResource.MULTI_DIMENSION_1 + " " + caSettings1.horizontalNo;
            corressheet.Cells[48, 3] = LocalResource.MULTI_DIMENSION_1 + " " + caSettings1.verticalNo;
            corressheet.Cells[51, 4] = LocalResource.MULTI_DIMENSION_1 + " " + caSettings1.verticalNo;
            corressheet.Cells[51, 8] = LocalResource.MULTI_DIMENSION_1 + " " + caSettings1.verticalNo;

            Y2axis.AxisTitle.Text = LocalResource.MULTI_DIMENSION_1 + " " + caSettings1.verticalNo;
            corressheet.Cells[47, 3] = LocalResource.MULTI_DIMENSION_1 + " " + caSettings1.horizontalNo;
            corressheet.Cells[51, 3] = LocalResource.MULTI_DIMENSION_1 + " " + caSettings1.horizontalNo;
            corressheet.Cells[51, 7] = LocalResource.MULTI_DIMENSION_1 + " " + caSettings1.horizontalNo;


            Y2axis.AxisTitle.Format.TextFrame2.TextRange.Font.Fill.ForeColor.RGB = 16711680;
            Xaxis.AxisTitle.Format.TextFrame2.TextRange.Font.Fill.ForeColor.RGB = 16711680;



            Range perStart = corressheet.Cells[47, 3];
            Range perEnd = corressheet.Cells[48, 5];
            Range PerRange = corressheet.Range[perStart, perEnd];
            object[,] perValue = PerRange.Value;
            double perSum = (Convert.ToDouble(perValue[1, 3]) * 100) + (Convert.ToDouble(perValue[2, 3]) * 100);
            string colText = (string)perValue[1, 1];
            string colText2 = (string)perValue[2, 1];
            double per1 = (Convert.ToDouble(perValue[1, 3])) * 100;
            double per2 = (Convert.ToDouble(perValue[2, 3])) * 100;
            per1 = Math.Round(per1, 1);
            per2 = Math.Round(per2, 1);
            perSum = Math.Round(perSum, 1);
            s.TextFrame.Characters(Type.Missing, Type.Missing).Text = LocalResource.CORRES_PERCENTAGE_OF_VARIANCE +
                System.Environment.NewLine + colText + " : " + per1 + " %" +
                System.Environment.NewLine + colText2 + " : " + per2 + " %" +
                System.Environment.NewLine + LocalResource.LABEL_OPTION_TOTAL + " : " + perSum + " %";
            s.TextFrame.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            if (QC4Common.Common.Constants.GlobalMode.Split(',')[0] == "ja-JP")//Todo
            {
               
            }

           


        }
        public void Output_C3_Graph(Range range, ChartObject chartObject, object[,] val)
        {
            if (val != null)
            {
                chartObject.Activate();
                Series series;
                for (int i = 1; i <= val.GetLength(0) + 1; i++)
                {
                    if (i != 1)
                    {
                        series = chartObject.Chart.SeriesCollection().NewSeries;
                        series.Name = "='" + corressheet.Name + "'!" + "B" + (51 + (i - 1));
                        series.ApplyDataLabels(AutoText: true, LegendKey: false, ShowSeriesName: true, ShowCategoryName: false, ShowValue: false, ShowPercentage: false, ShowBubbleSize: false);
                        series.XValues = corressheet.Cells[51 + (i - 1), 3];
                        series.Values = corressheet.Cells[51 + (i - 1), 4];
                        DataLabel dataLabel = series.DataLabels(0);
                        series.MarkerStyle = XlMarkerStyle.xlMarkerStyleCircle;
                        series.MarkerBackgroundColor = 16777215;
                        series.MarkerForegroundColor = 0;

                        series.MarkerSize = 5;
                    }
                    else
                    {
                      
                    }
                    
                }
                chartObject.Chart.AutoScaling = true;
            }
        }
        public void Output_C3_Graph1(Range range, ChartObject chartObject, object[,] val)
        {
            if (val != null)
            {
                Series series;
                for (int i = 1; i <= val.GetLength(0) + 1; i++)
                {
                    if (i != 1)
                    {
                        series = chartObject.Chart.SeriesCollection().NewSeries;
                        series.Name = "='" + corressheet.Name + "'!" + "F" + (51 + (i - 1));
                        series.ApplyDataLabels(AutoText: true, LegendKey: false, ShowSeriesName: true, ShowCategoryName: false, ShowValue: false, ShowPercentage: false, ShowBubbleSize: false);
                        DataLabel dataLabel = series.DataLabels(0);
                        dataLabel.Format.TextFrame2.TextRange.Font.Fill.ForeColor.RGB = 12611584;
                        series.XValues = corressheet.Cells[51 + (i - 1), 7];
                        series.Values = corressheet.Cells[51 + (i - 1), 8];
                        series.MarkerBackgroundColor = 12611584;
                        series.MarkerForegroundColor = 12611584;
                        series.MarkerStyle = XlMarkerStyle.xlMarkerStyleDiamond;
                        series.MarkerSize = 5;
                    }
                    else
                    {
                        chartObject.Chart.SeriesCollection(1).Delete();
                    }
                }
                chartObject.Chart.AutoScaling = true;
            }
        }
        public void set_Gt_Excel_sheet(CorrespondenceSettings caSettings, string outPaths)
        {
            int rowcount = 0;
            if (caSettings.crRowChoiceCnt >= caSettings.crColChoiceCnt)
            {
                rowcount = caSettings.crRowChoiceCnt;
            }
            else
            {
                rowcount = caSettings.crColChoiceCnt;
            }
            string lCol = string.Empty;
            string RCol = string.Empty;
            string[] Variables;
            if (caSettings.tabulationType == 1)
            {
                lCol = "B" + (51 + rowcount);
                RCol = "D" + (51 + rowcount);


            }

            String RangeCol = lCol + ":" + RCol;
            filename = Path.Combine(outPaths, result_col_coord_File_Name);
            string[,] result_col_value = new string[rowcount, 3];
            Encoding enc = Encoding.GetEncoding(FileEncoding);
            Range crossRange = corressheet.get_Range("B52:D52", RangeCol);
            object[,] data = crossRange.Value;
            for (int i = 0; i < result_col_value.GetLength(0); i++)
            {
                if (result_col_value[i, 0] != null)
                {
                    data[i + 1, 2] = result_col_value[i, 0];
                    data[i + 1, 3] = result_col_value[i, 1];
                    data[i + 1, 1] = result_col_value[i, 2];
                }

            }

            crossRange.Value = data;
            crossRange.Cells.Style.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            crossRange.Borders[XlBordersIndex.xlInsideHorizontal].Color = 10921638;
            crossRange.Borders[XlBordersIndex.xlInsideHorizontal].Weight = XlBorderWeight.xlThin;
            crossRange.Borders[XlBordersIndex.xlInsideVertical].Color = 10921638;
            crossRange.Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlThin;
            crossRange.Borders.Color = 10921638;
            for (int i = 0; i < result_col_value.GetLength(0); i++)
            {
                if (result_col_value[i, 0] != null)
                {
                    crossRange.Cells[i + 1, 1].Interior.Color = 15921906;
                    crossRange.Cells[i + 1, 2].HorizontalAlignment = XlHAlign.xlHAlignRight;
                    crossRange.Cells[i + 1, 3].HorizontalAlignment = XlHAlign.xlHAlignRight;
                }
            }

            crossRange.Borders.Weight = XlBorderWeight.xlThin;
            //================================================================================Col writing===========================///

            RangeCol = string.Empty;
            filename = string.Empty;
            filename = Path.Combine(outPaths, result_row_coord_File_Name);
            if (caSettings.tabulationType == 1)
            {
                lCol = "F" + (51 + rowcount);
                RCol = "H" + (51 + rowcount);


            }
            RangeCol = lCol + ":" + RCol;
            Range crossRowRange = corressheet.get_Range("F52:H52", RangeCol);
            string[,] result_Row_value = new string[rowcount, 3];
            // ReadCorresFile(filename, enc, "\t", Definitions.VariableDictionary[caSettings.crRowVar], ref result_Row_value);
            object[,] crossColVal = crossRowRange.Value;
            for (int i = 0; i < result_Row_value.GetLength(0); i++)
            {
                if (result_Row_value[i, 0] != null)
                {
                    crossColVal[i + 1, 2] = result_Row_value[i, 0];
                    crossColVal[i + 1, 3] = result_Row_value[i, 1];
                    crossColVal[i + 1, 1] = result_Row_value[i, 2];
                }

            }
            crossRowRange.Value = crossColVal;
            crossRowRange.Borders[XlBordersIndex.xlInsideHorizontal].Color = 10921638;
            crossRowRange.Borders[XlBordersIndex.xlInsideHorizontal].Weight = XlBorderWeight.xlThin;
            crossRowRange.Borders[XlBordersIndex.xlInsideVertical].Color = 10921638;
            crossRowRange.Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlThin;
            crossRowRange.Borders.Color = 10921638;
            crossRowRange.Borders.Weight = XlBorderWeight.xlThin;
            for (int i = 0; i < result_Row_value.GetLength(0); i++)
            {
                if (result_Row_value[i, 0] != null)
                {
                    crossRowRange.Cells[i + 1, 1].Interior.Color = 15853276;
                    crossRowRange.Cells[i + 1, 2].HorizontalAlignment = XlHAlign.xlHAlignRight;
                    crossRowRange.Cells[i + 1, 3].HorizontalAlignment = XlHAlign.xlHAlignRight;
                }
            }

            //   crossRowRange.HorizontalAlignment = XlVAlign.xlVAlignCenter;
            //=======================================End of ROw Value writing=================================================//

            filename = string.Empty;
            filename = Path.Combine(outPaths, result_eig_File_Name);
            Range GtResult = corressheet.get_Range("D47:F47", "D48:F48");
            string[,] GtResult_array = new string[2, 2];
            //  ReadCorresFile(filename, enc, "\t", Definitions.VariableDictionary[caSettings.crRowVar], ref GtResult_array);
            object[,] GtResultObject = GtResult.Value;
            for (int i = 0; i < GtResult_array.GetLength(0); i++)
            {
                if (GtResult_array[i, 1] != null)
                {
                    GtResultObject[i + 1, 1] = GtResult_array[i, 0];
                    // GtResultObject[i + 1, 3] = GtResult_array[i, 2];
                    GtResultObject[i + 1, 3] = GtResult_array[i, 1];
                }

            }
            GtResult.Value = GtResultObject;
            GtResult.Borders[XlBordersIndex.xlInsideHorizontal].Color = 10921638;
            GtResult.Borders[XlBordersIndex.xlInsideHorizontal].Weight = XlBorderWeight.xlThin;
            GtResult.Borders[XlBordersIndex.xlInsideVertical].Color = 10921638;
            GtResult.Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlThin;
            GtResult.Borders.Color = 10921638;
            GtResult.Borders.Weight = XlBorderWeight.xlThin;

        }
        public string[,] ReadCorresFile(String filePath, Encoding encode, String deLimiter, List<string> choices, CorrespondenceSettings caSettings)
        {
            string[,] DataArray = new string[choices.Count(), caSettings.noOfDimension + 1];
            try
            {
                string[] lines = File.ReadAllLines(filePath, encode);
                char del = Convert.ToChar(deLimiter);
                for (int i = 0; i < lines.Count(); i++)
                {
                    string[] data = lines[i].Split(del);
                    DataArray[i, 0] = choices[i];

                    if (caSettings.horizontalRevData && !caSettings.verticalRevData)
                    {
                        DataArray[i, 1] = (Convert.ToDouble(data[caSettings.horizontalNo - 1]) * (-1)).ToString();
                        DataArray[i, 2] = (Convert.ToDouble(data[caSettings.verticalNo - 1])).ToString();
                    }
                    else if (!caSettings.horizontalRevData && caSettings.verticalRevData)
                    {
                        DataArray[i, 1] = (Convert.ToDouble(data[caSettings.horizontalNo - 1])).ToString();
                        DataArray[i, 2] = (Convert.ToDouble(data[caSettings.verticalNo - 1]) * (-1)).ToString();
                    }
                    else if (caSettings.horizontalRevData && caSettings.verticalRevData)
                    {
                        DataArray[i, 1] = (Convert.ToDouble(data[caSettings.horizontalNo - 1]) * (-1)).ToString();
                        DataArray[i, 2] = (Convert.ToDouble(data[caSettings.verticalNo - 1]) * (-1)).ToString();
                    }
                    else
                    {
                        DataArray[i, 2] = (Convert.ToDouble(data[caSettings.verticalNo - 1])).ToString();
                        DataArray[i, 1] = (Convert.ToDouble(data[caSettings.horizontalNo - 1])).ToString();
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return DataArray;
        }
        public string[,] ReadCorresFileResult(String filePath, Encoding encode, String deLimiter, CorrespondenceSettings corrData)
        {
            string[,] valuearray = new string[corrData.crRowChoiceCnt + 1, corrData.crColChoiceCnt + 1];
            try
            {
                string[] lines = File.ReadAllLines(filePath, encode);
                char del = Convert.ToChar(deLimiter);


                for (int i = 0; i < lines.Count(); i++)
                {
                    string[] data = lines[i].Split(del);
                    for (int j = 0; j < data.Count(); j++)
                    {
                        valuearray[i, j] = data[j];
                    }

                }

            }
            catch (Exception ex)
            {

            }
            return valuearray;
        }
        public string[,] ReadResult(String filePath, Encoding encode, String deLimiter)
        {
            string[,] DataArray = new string[2, 2];
            try
            {
                string[] lines = File.ReadAllLines(filePath, encode);
                char del = Convert.ToChar(deLimiter);


                for (int i = 0; i < lines.Count(); i++)
                {
                    if ((caSettings1.horizontalNo - 1) == i)
                    {
                        string[] data = lines[i].Split(del);
                        DataArray[0, 0] = data[0];
                        DataArray[0, 1] = data[1];
                    }
                    else if ((caSettings1.verticalNo - 1) == i)
                    {
                        string[] data = lines[i].Split(del);
                        DataArray[1, 0] = data[0];
                        DataArray[1, 1] = data[1];
                    }

                }

            }
            catch (Exception ex)
            {

            }
            return DataArray;
        }
        public string[,] ReadGTinputFile(String filePath, Encoding encode, String deLimiter, CorrespondenceSettings corrData)
        {
            string[,] valuearray = new string[corrData.gtVars.Count + 1, corrData.gtChoiceCnt + 1];
            try
            {
                string[] lines = File.ReadAllLines(filePath, encode);
                char del = Convert.ToChar(deLimiter);


                for (int i = 0; i < lines.Count(); i++)
                {
                    string[] data = lines[i].Split(del);
                    for (int j = 0; j < data.Count(); j++)
                    {
                        valuearray[i, j] = data[j];
                    }

                }

            }
            catch (Exception ex)
            {

            }
            return valuearray;
        }
        private void PRV_MergeConditionalRange(Range TargetRange)
        {
            if (TargetRange.Cells.Count > 1)
            {
                TargetRange.Merge();
            }
        }
        private void FNC_An_ConditionalOutput(Range StartCell, long ColumnCount, long HeaderInteriorColor = 12611584, long HeaderFontColor = 16777215)
        {
            long pConditionalCount = 5;
            //string ConditionalWord = Edit_SiborikomiAll(caSettings1.Filters);
            string ConditionalWord = Macromill.QCWeb.Logic.TabulationEx.Criteria.CriteriaDescProvider.Edit_SiborikomiAll_General(caSettings1.Filters, questions);
            if (ConditionalWord != "")
            {
                Worksheet TargetSheet = StartCell.Worksheet;
                //	With TargetSheet
                Range HeaderRange = TargetSheet.Range[StartCell, StartCell.Offset[0, ColumnCount - 1]];
                Range ValueRange = TargetSheet.Range[StartCell.Offset[1, 0], StartCell.Offset[pConditionalCount, ColumnCount - 1]];
                PRV_MergeConditionalRange(HeaderRange);
                PRV_MergeConditionalRange(ValueRange);
                HeaderRange.BorderAround(XlLineStyle.xlContinuous, Weight: XlBorderWeight.xlThin, Color: ColorPallet.colorIndex[16]);
                ValueRange.BorderAround(XlLineStyle.xlContinuous, Weight: XlBorderWeight.xlThin, Color: ColorPallet.colorIndex[16]);
                HeaderRange.Cells[1, 1].Value = Str_Siborikomi;
                ValueRange.Cells[1, 1].Value = ConditionalWord;
                HeaderRange.Interior.Color = HeaderInteriorColor;
                // ValueRange.Interior.Color = 5296274;
                HeaderRange.Font.Color = HeaderFontColor;
                HeaderRange.Font.Bold = true;
                ValueRange.Font.ColorIndex = XlColorIndex.xlColorIndexAutomatic;
                ValueRange.Font.Bold = false;
                ValueRange.WrapText = true;
                HeaderRange.Borders.Color = 10921638;
                ValueRange.Borders.Color = 10921638;
            }
        }
        public string Edit_SiborikomiAll(System.Collections.Generic.List<FilterSettingsCr> filters)
        {
            string EditString = "";
            string space = " ";
            if (QC4Common.Common.Constants.GlobalMode.Split(',')[0] == "ja-JP")
            {
                space = "";
            }
            if (filters == null || filters.Count == 0)
            {
                return EditString;
            }

            foreach (FilterSettingsCr filter in filters)
            {
                if (filter.variable == null || filter.variable.Length == 0)
                {
                    continue;
                }
                QuestionSettings qstnDet = Qc4Launcher.Util.Definiotion.VariableDictionary[filter.variable];
                Questions.Question itemInfo = (Questions.Question)questions[qstnDet.Id];

                if (filter.conditionType == D_AND)
                {
                    EditString += strEdit_And;
                }
                else if (filter.conditionType == D_OR)
                {
                    EditString += strEdit_Or;
                }
                if (EditString.Length > 0)
                {
                    EditString += "\n";
                }
                EditString += itemInfo.Name2 + space;

                if (filter.values == u_DK)
                {
                    EditString += strEdit01 + strNoAnswer;
                }
                else if (filter.values == u_Asterisk)
                {
                    EditString += strEdit01 + strNotApply;
                }
                else
                {
                    EditString += strEdit01 + filter.values + space;
                }


                if (filter.operatorType == Sign_LG)
                {
                    EditString += strEdit_LG;
                }
                else if (filter.operatorType == Sign_E)
                {
                    EditString += strEdit_E;
                }
                else if (filter.operatorType == Sign_LE)
                {
                    EditString += strEdit_LE;
                }
                else if (filter.operatorType == Sign_GE)
                {
                    EditString += strEdit_GE;
                }
                else if (filter.operatorType == Sign_L)
                {
                    EditString += strEdit_L;
                }
                else if (filter.operatorType == Sign_G)
                {
                    EditString += strEdit_G;
                }
            }
            return EditString;
        }


    }
}
