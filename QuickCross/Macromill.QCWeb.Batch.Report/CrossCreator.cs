using Macromill.QCWeb.Batch.Report;
using Macromill.QCWeb.Common;
using Macromill.QCWeb.COMOperate;
using Macromill.QCWeb.ReportRequest;
using Macromill.QCWeb.Tabulation;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Macromill.QCWeb.Batch.Report.Outputs;
using static Macromill.QCWeb.Batch.Report.Reportsets;
using static Macromill.QCWeb.Batch.Report.Tables;
using static Macromill.QCWeb.Common.Constants;
using XlFileFormat = Microsoft.Office.Interop.Excel.XlFileFormat;
using XlPageOrientation = Macromill.QCWeb.Common.XlPageOrientation;
using XlPaperSize = Macromill.QCWeb.Common.XlPaperSize;

namespace Qc4Launcher.Logic
{
    public class CrossCreator
    {
        private static OutputCross CurrentOutput = null;
        private static Worksheet WorkingSheet;
        private static Workbook WorkingBook;
        public static bool ResumeError;
        public static string ThisLocationCode;
        public static ExecuteStaticMethod ExecuteStaticMethod = new ExecuteStaticMethod();
        public static MacroExecuter macroExecuter = null;

        public static string TEMPLATE_NAME = "Cross.xlt";
        public static string TRANSPOSE_TEMPLATE_NAME = "CrossPortrait.xlt";
        public static string INDIVIDUAL_TEMPLATE_NAME = "CrossNP.xlt";
        public static string TRANSPOSE_INDIVIDUAL_TEMPLATE_NAME = "CrossNPPortrait.xlt";
        public static string REPORT_TEMPLATE_NAME = "Report.xlt";
        public static string TRANSPOSE_REPORT_TEMPLATE_NAME = "ReportPortrait.xlt";

        public static string FORMAT_TEMPLATE_NAME = "CrossFormat.xlt";
        public static string TRANSPOSE_FORMAT_TEMPLATE_NAME = "CrossPortraitFormat.xlt";
        public static string INDIVIDUAL_FORMAT_TEMPLATE_NAME = "CrossNPFormat.xlt";
        public static string TRANSPOSE_INDIVIDUAL_FORMAT_TEMPLATE_NAME = "CrossNPPortraitFormat.xlt";
        public static string REPORT_FORMAT_TEMPLATE_NAME = "ReportFormat.xlt";
        public static string TRANSPOSE_REPORT_FORMAT_TEMPLATE_NAME = "ReportPortraitFormat.xlt";


        public static string GetReportKeyword(ReportMessageIndex Index, params string[] replaceWords)
        {
            String joinedReplaceWords;
            if (replaceWords.Length > 0)
            {
                joinedReplaceWords = string.Join("\v", replaceWords);
                return ExecuteStaticMethod.GetReportKeyword(Index, ThisLocationCode, joinedReplaceWords);
            }
            else
            {
                return ExecuteStaticMethod.GetReportKeyword(Index, ThisLocationCode);
            }
        }

        public static string GetUnescapedReportKeyword(ReportMessageIndex Index, params string[] replaceWords)
        {
            String joinedReplaceWords;
            if (replaceWords.Length > 0)
            {
                joinedReplaceWords = string.Join("\v", replaceWords);
                return ExecuteStaticMethod.GetReportKeyword(Index, ThisLocationCode, joinedReplaceWords, true);
            }
            else
            {
                return ExecuteStaticMethod.GetReportKeyword(Index, ThisLocationCode, null, true);
            }
        }

        public static void CreateCross(Output Output, MacroExecuter MacroExecuter)
        {

            //Dim FormatBook Workbook
            XlFileFormat xlFmt = XlFileFormat.xlOpenXMLWorkbook;
            //Dim tmpTable QCWebReportBatch.CrossTable
            //Dim KeyItemInfo KeyItemInformation
            //Dim n;

            string tmp;
            //Dim i;
            //Dim res MethodResult
            //Dim Errors() ErrorStruct, ErrorsCount;
            string OrgProcName;
            //OrgProcName = RunningProcName
            //RunningProcName = "CrossCreator.CreateCross"
            //On Error GoTo ErrHdl
            // CurrentOutput = Output
            macroExecuter = MacroExecuter;
            Reportset reportset = (Reportset)Output.ParentReportset;
            CurrentOutput = (OutputCross)Output;
            WorkingBook = macroExecuter.MacroBook;
            WorkingSheet = WorkingBook.Application.Workbooks.Add(OutputUtil.BaseTemplatePath(WorkingBook.Path, WorkingBook.Application.PathSeparator)).Worksheets.Item[1];
            string TempPath = TemplatePath(ref xlFmt, CurrentOutput.Orientation);
            string FormatPath = FormatTemplatePath(CurrentOutput.Orientation);


            //    if( Len(Dir(TempPath)) = 0 & ){
            //        ' SFTP‚ÅGET
            //    }
            //    if( Len(Dir(FormatPath)) = 0 & ){
            //        ' SFTP‚ÅGET
            //    }
            //    On Error Resume Next
            //    Dir
            //    On Error GoTo ErrHdl
            CrossTable tmpTable = (CrossTable)Output.Tables[0];
            Macromill.QCWeb.ReportRequest.KeyItemInformation KeyItemInfo = tmpTable.KeyItem;
            string KeyItemName = string.Empty;
            string filenameSuffix = null;
            if (KeyItemInfo != null)
                KeyItemName = KeyItemInfo.Name;
            if (KeyItemName.Length > 0)
            {
                //        ' filenameSuffix = "_" & KeyItemName & "_" & CStr(KeyItemInfo.SectorNumber)
                int n = reportset.KeyItemMaxSectorNumber(KeyItemName);
                if (n > 0) n = (int)(Math.Log(n) / Math.Log(10)) + 1;
                string fmt = new string('0', n);
                filenameSuffix = "_" + KeyItemName + "_" + KeyItemInfo.SectorNumber.ToString(fmt);
            }
            //    'Me.Application.ScreenUpdating = false
            //    Me.Application.Visible = true
            //    Me.Application.PrintCommunication = false
            Workbook FormatBook = WorkingBook.Application.Workbooks.Add(FormatPath);
            FormatBook.Unprotect(macroExecuter.Templatebookpassword);
            //    if( Output.TablesOnOneSheet = TablesOnOneSheet_Multi ){
            //        res = CreateStandardCross(TempPath, FormatBook, xlFmt, filenameSuffix)
            //    }else{
            //        res = CreateIndividualCross(TempPath, FormatBook, xlFmt, filenameSuffix)
            //    }

            CreateStandardCross(TempPath, FormatBook, (XlFileFormat)CurrentOutput.ParentRequest.ExcelFileFormat, filenameSuffix);


            //ExitProc:
            //            On Error Resume Next
            //    if( Not FormatBook == null ){ FormatBook.Close false
            //    if( Not WorkingSheet == null ){
            //        WorkingSheet.Parent.Close false
            //         WorkingSheet = null
            //    }
            //    CreateCross = res
            //    if( res = RaisedError ){ PutErrorsInformation Errors
            //    RunningProcName = OrgProcName
            //    Exit Function
            //ErrHdl:
            //            if( ResumeError ){
            //                res = RaisedError
            //        PushError Err(), Errors, ErrorsCount
            //        Resume Next
            //    }else{
            //        res = Uncontinuable
            //        ThisWorkbook.AsynchronousForm.SetError Err()
            //        Resume ExitProc
            //    }
        }

        private static void CreateStandardCross(string TempPath, Workbook FormatBook, XlFileFormat FileFormat, string Suffix)
        {

            CrossTable tmpTable;
            CrossTable tmpNextTable;
            bool HasWeightColumn = false;
            int MaxAxesCount = 0;
            bool isN = false;
            bool HasWeight;
            string FormatRangeNamePrefix = null;
            Hashtable CutRowsCol = null;
            Hashtable CutColumnsCol = null;

            string ReportTitle;
            string header;

            int n;
            string fmt;
            string strIdx;

            bool PageSetupOn;
            Array ContentsValue = null;
            Array HyperlinkTargetCells = null; //Range
            Array PageSetupContentsValue = null; //string
            Array PageSetupHyperlinkTargetCells = null; //range

            string PageSetupSheetBaseName = null;
            Worksheet ContentsSheet;
            Worksheet TemplateSheet = null;
            Worksheet SigTestTemplateSheet = null;
            Worksheet FormatSheet = null;
            Worksheet SigTestFormatSheet = null;
            Worksheet PageSetupTemplateSheet = null;
            Worksheet PageSetupSigTestTemplateSheet = null;
            Worksheet NPerSheet = null;
            Worksheet NSheet = null;
            Worksheet PerSheet = null;
            Worksheet SigTestSheet = null;
            Worksheet PageSetupContentsSheet;
            Worksheet PageSetupNPerSheet = null;
            Worksheet PageSetupNSheet = null;
            Worksheet PageSetupPerSheet = null;
            Worksheet PageSetupSigTestSheet = null;
            Range NPerStartCell = null;
            Range NStartCell = null;
            Range PerStartCell = null;
            Range SigTestStartCell = null;
            Range PageSetupNPerStartCell = null;
            Range PageSetupNStartCell = null;
            Range PageSetupPerStartCell = null;
            Range PageSetupSigTestStartCell = null;
            Worksheet tmpSheet = null;
            Worksheet tmpPageSetupSheet = null;
            string tmp = null;
            int i;
            int j;
            double[] h;
            double maxH;
            int PageSetupColumnsCountPerPage = 0;
            int MaxRowsCountPerPage = 0;
            int NPerRemainedRowsCount = 0;
            int NRemainedRowsCount = 0;
            int PerRemainedRowsCount = 0;
            int SigTestRemainedRowsCount = 0;
            double DefLineHeight = 0;
            bool SigTestOn = false;
            int SigTestPageSetupColumnsCountPerPage = 0;
            Hashtable WholeRowCol = null;

            bool HasShowPreWBTotal = false;
            TableOrientation tmpOrientation;
            int tmpTablesCount;

            //Dim res MethodResult
            // ErrorStruct[] Errors;
            int ErrorsCount;
            //On Error GoTo ErrHdl
            Application Application = WorkingBook.Application;
            Workbook NewBook = Application.Workbooks.Add(TempPath);
            NewBook.Unprotect(macroExecuter.Templatebookpassword);


            Application.ScreenUpdating = true;
            Application.Visible = true;

            PageSetupOn = CurrentOutput.PageSetup & CurrentOutput.Orientation == TableOrientation.Landscape;

            if (PageSetupOn)
            {
                switch (CurrentOutput.PaperSize)
                {
                    case XlPaperSize.xlPaperA3:
                        PageSetupSheetBaseName = "A3";
                        break;
                    case XlPaperSize.xlPaperB4:
                        PageSetupSheetBaseName = "B4";
                        break;
                    default:
                        PageSetupSheetBaseName = "A4";
                        break;
                }
                if (CurrentOutput.PaperOrientation == XlPageOrientation.xlPortrait)
                {
                    PageSetupSheetBaseName = PageSetupSheetBaseName + "Portrait";
                }
                else
                {
                    PageSetupSheetBaseName = PageSetupSheetBaseName + "Landscape";
                }
            }
            if ((CurrentOutput.SignificanceTestOne || CurrentOutput.SignificanceTestFive || CurrentOutput.SignificanceTestTen)
                && (!CurrentOutput.SignificanceTestOne || !CurrentOutput.SignificanceTestFive || !CurrentOutput.SignificanceTestTen))
            {
                SigTestOn = true;
            }

            Hashtable tmpCol = new Hashtable();
            int[] MaxAxesCountArray = new int[CurrentOutput.Tables.Count];
            for (i = 0; i < CurrentOutput.Tables.Count; i++)
            {
                tmpTable = (CrossTable)CurrentOutput.Tables[i];
                MaxAxesCountArray[i] = GetMaxAxesCount(tmpTable);
                if (MaxAxesCount < 2) MaxAxesCount = MaxAxesCountArray[i];
                if (!HasWeightColumn) HasWeightColumn = GetHasWeight(tmpTable);
            }



            //With NewBook.Worksheets
            Sheets newBookWShhets = NewBook.Worksheets;
            ContentsSheet = NewBook.Worksheets.Item["INDEX"];
            ContentsSheet.Name = GetReportKeyword(ReportMessageIndex.ReportCrossContentsSheetNameIndex);
            tmpCol.Add(ContentsSheet.Name, string.Empty);
            AdjustContentsSheet(null, ContentsSheet, ref ContentsValue, ref HyperlinkTargetCells, (TableType)(Convert.ToInt16(TableType.SignificanceTest) & ToInt(SigTestOn)));
            if (CurrentOutput.Orientation == TableOrientation.Landscape)
            {
                tmp = MaxAxesCount == 2 ? "Triple" : "double";
                if (CurrentOutput.OutputNPerTable || CurrentOutput.OutputNTable || CurrentOutput.OutputPerTable)
                {
                    TemplateSheet = NewBook.Worksheets.Item[tmp + "Standard"];
                    tmpCol.Add(TemplateSheet.Name, string.Empty);
                    if (PageSetupOn)
                    {
                        PageSetupTemplateSheet = NewBook.Worksheets.Item[tmp + PageSetupSheetBaseName];
                        DefLineHeight = PageSetupTemplateSheet.Rows.Item[1].Height;
                        MaxRowsCountPerPage = PageSetupTemplateSheet.Range["A1"].Value;
                        PageSetupColumnsCountPerPage = PageSetupTemplateSheet.Range["B1"].Value;
                        tmpCol.Add(PageSetupTemplateSheet.Name, string.Empty);
                    }
                }
                if (SigTestOn)
                {
                    SigTestTemplateSheet = newBookWShhets.Item[tmp + "SigTest"];
                    tmpCol.Add(SigTestTemplateSheet.Name, string.Empty);
                    if (CurrentOutput.PageSetupSignificanceTestTable)
                    {
                        PageSetupSigTestTemplateSheet = newBookWShhets.Item[tmp + "SigTest" + PageSetupSheetBaseName];
                        if (DefLineHeight == 0) { DefLineHeight = PageSetupSigTestTemplateSheet.Rows.Item[1].Height; }
                        if (MaxRowsCountPerPage == 0) { MaxRowsCountPerPage = PageSetupSigTestTemplateSheet.Range["A1"].Value; }
                        SigTestPageSetupColumnsCountPerPage = PageSetupSigTestTemplateSheet.Range["B1"].Value;
                        tmpCol.Add(PageSetupSigTestTemplateSheet.Name, string.Empty);
                    }
                }
            }
            else
            {
                TemplateSheet = newBookWShhets.Item["Template"];
                if (PageSetupOn)
                {
                    PageSetupTemplateSheet = newBookWShhets.Item[PageSetupSheetBaseName];
                }
                if (SigTestOn)
                {
                    TemplateSheet.Copy(Type.Missing, newBookWShhets.Item[newBookWShhets.Count]);
                    SigTestTemplateSheet = newBookWShhets.Item[newBookWShhets.Count];
                    tmpCol.Add(SigTestTemplateSheet.Name, string.Empty);
                    if (CurrentOutput.PageSetupSignificanceTestTable)
                    {
                        PageSetupTemplateSheet.Copy(Type.Missing, SigTestTemplateSheet);
                        PageSetupSigTestTemplateSheet = SigTestTemplateSheet.Next;
                        tmpCol.Add(PageSetupSigTestTemplateSheet.Name, string.Empty);
                    }
                }
                if (CurrentOutput.OutputNPerTable || CurrentOutput.OutputNTable || CurrentOutput.OutputPerTable)
                {
                    tmpCol.Add(TemplateSheet.Name, string.Empty);
                    if (PageSetupTemplateSheet != null)
                    {
                        tmpCol.Add(PageSetupTemplateSheet.Name, string.Empty);
                    }
                }
                else
                {
                    TemplateSheet = null;
                    PageSetupTemplateSheet = null;
                }
            }
            Application.DisplayAlerts = false;
            foreach (Worksheet sht in NewBook.Worksheets)
            {
                if (!tmpCol.ContainsKey(sht.Name))
                {
                    sht.Delete();
                }
            }
            //    On Error GoTo ErrHdl
            ReportTitle = CurrentOutput.ParentRequest.Title;
            header = OutputUtil.GetAdjustedHeader(ReportTitle);
            if (TemplateSheet != null)
            {
                TemplateSheet.Unprotect(macroExecuter.Templatesheetpassword);
                TemplateSheet.PageSetup.CenterHeader = header;
            }
            if (PageSetupTemplateSheet != null)
            {
                PageSetupTemplateSheet.Unprotect(macroExecuter.Templatesheetpassword);
                PageSetupTemplateSheet.PageSetup.CenterHeader = header;
            }
            if (SigTestTemplateSheet != null)
            {
                SigTestTemplateSheet.Unprotect(macroExecuter.Templatesheetpassword);
                SigTestTemplateSheet.PageSetup.CenterHeader = header;
            }
            if (PageSetupOn)
            {
                ContentsSheet.Copy(Type.Missing, ContentsSheet);
                PageSetupContentsSheet = ContentsSheet.Next;
                tmp = GetReportKeyword(ReportMessageIndex.ReportCrossPageSetupSheetSuffixIndex);
                PageSetupContentsSheet.Name = GetReportKeyword(ReportMessageIndex.ReportCrossContentsSheetNameIndex) + tmp;
                PageSetupContentsValue = Array.CreateInstance(typeof(string),
                    new int[] { ContentsValue.GetUpperBound(0), ContentsValue.GetUpperBound(1) },
                    new int[] { 1, 1 });

                PageSetupContentsValue = Array.CreateInstance(typeof(string),
                    new int[] { HyperlinkTargetCells.GetUpperBound(0), HyperlinkTargetCells.GetUpperBound(1) },
                    new int[] { 1, HyperlinkTargetCells.GetLowerBound(1) });

            }
            if (CurrentOutput.OutputNPerTable)
            {
                NPerSheet = TemplateSheet;
                NPerSheet.Name = GetReportKeyword(ReportMessageIndex.ReportCrossNPSheetNameIndex);
                NPerStartCell = NPerSheet.Range["A1"];
                tmpSheet = NPerSheet;
                if (PageSetupOn)
                {
                    if (CurrentOutput.PageSetupNPerTable)
                    {
                        NPerRemainedRowsCount = MaxRowsCountPerPage;
                        PageSetupNPerSheet = PageSetupTemplateSheet;
                        PageSetupNPerSheet.Name = NPerSheet.Name + tmp;
                        PageSetupNPerStartCell = PageSetupNPerSheet.Range["A1"];
                        tmpPageSetupSheet = PageSetupNPerSheet;
                    }
                }
            }
            if (CurrentOutput.OutputNTable)
            {
                if (tmpSheet == null)
                {
                    NSheet = TemplateSheet;
                }
                else
                {
                    TemplateSheet.Copy(Type.Missing, tmpSheet);
                    NSheet = tmpSheet.Next;
                }
                NSheet.Name = GetReportKeyword(ReportMessageIndex.ReportCrossNSheetNameIndex);
                NStartCell = NSheet.Range["A1"];
                tmpSheet = NSheet;
                if (PageSetupOn)
                {
                    if (CurrentOutput.PageSetupNTable)
                    {
                        NRemainedRowsCount = MaxRowsCountPerPage;
                        if (tmpPageSetupSheet == null)
                        {
                            PageSetupNSheet = PageSetupTemplateSheet;
                        }
                        else
                        {
                            PageSetupTemplateSheet.Copy(Type.Missing, tmpPageSetupSheet);
                            PageSetupNSheet = tmpPageSetupSheet.Next;
                        }
                        PageSetupNSheet.Name = NSheet.Name + tmp;
                        PageSetupNStartCell = PageSetupNSheet.Range["A1"];
                        tmpPageSetupSheet = PageSetupNSheet;
                    }
                }
            }
            if (CurrentOutput.OutputPerTable)
            {
                if (tmpSheet == null)
                {
                    PerSheet = TemplateSheet;
                }
                else
                {
                    TemplateSheet.Copy(Type.Missing, tmpSheet);
                    PerSheet = tmpSheet.Next;
                }
                PerSheet.Name = GetReportKeyword(ReportMessageIndex.ReportCrossPSheetNameIndex);
                PerStartCell = PerSheet.Range["A1"];
                tmpSheet = PerSheet;
                if (PageSetupOn)
                {
                    if (CurrentOutput.PageSetupPerTable)
                    {
                        PerRemainedRowsCount = MaxRowsCountPerPage;
                        if (tmpPageSetupSheet == null)
                        {
                            PageSetupPerSheet = PageSetupTemplateSheet;
                        }
                        else
                        {
                            PageSetupTemplateSheet.Copy(Type.Missing, tmpPageSetupSheet);
                            PageSetupPerSheet = tmpPageSetupSheet.Next;
                        }
                        PageSetupPerSheet.Name = PerSheet.Name + tmp;
                        PageSetupPerStartCell = PageSetupPerSheet.Range["A1"];
                    }
                }
            }
            if (SigTestOn)
            {
                SigTestSheet = SigTestTemplateSheet;
                SigTestSheet.Name = GetReportKeyword(ReportMessageIndex.ReportCrossSignificanceTestSheetNameIndex);
                SigTestStartCell = SigTestSheet.Range["A1"];
                if (CurrentOutput.PageSetupSignificanceTestTable)
                {
                    SigTestRemainedRowsCount = MaxRowsCountPerPage;
                    PageSetupSigTestSheet = PageSetupSigTestTemplateSheet;
                    PageSetupSigTestSheet.Name = SigTestSheet.Name + tmp;
                    PageSetupSigTestStartCell = PageSetupSigTestSheet.Range["A1"];
                }
            }
            if (CurrentOutput.OutputNPerTable || CurrentOutput.OutputNTable || CurrentOutput.OutputPerTable)
            {
                FormatSheet = FormatBook.Worksheets.Item["Standard"];
            }
            if (SigTestOn)
            {
                SigTestFormatSheet = FormatBook.Worksheets.Item["SignificanceTest"];
            }
            AdjustFormat(FormatSheet, SigTestFormatSheet, MaxAxesCount, HasWeightColumn);



            HasShowPreWBTotal = CurrentOutput.ShowPreWBTotal;
            tmpOrientation = CurrentOutput.Orientation;
            tmpTablesCount = CurrentOutput.Tables.Count;

            n = (int)(Math.Log(CurrentOutput.Tables.Count) / Math.Log(10)) + 1;

            if (n < 3)
            {
                n = 3;
            }
            fmt = new string('0', n);
            for (i = 0; i < CurrentOutput.Tables.Count; i++)
            {
                strIdx = (i + 1).ToString(fmt);// string.Format(fmt, i + 1);// to do
                tmpTable = (CrossTable)CurrentOutput.Tables[i];
                if (i < CurrentOutput.Tables.Count - 1)
                {
                    tmpNextTable = (CrossTable)CurrentOutput.Tables[i + 1];
                }
                else
                {
                    tmpNextTable = null;
                }
                ContentsValue.SetValue(tmpTable.Question.Name, i + 1, 1);
                ContentsValue.SetValue(tmpTable.Question.Description, i + 1, 2);
                if (PageSetupOn)
                {
                    PageSetupContentsValue.SetValue(ContentsValue.GetValue(i + 1, 1), i + 1, 1);
                    PageSetupContentsValue.SetValue(ContentsValue.GetValue(i + 1, 2), i + 1, 2);
                }
                switch (tmpTable.Question.QuestionType & (QuestionType.SA | QuestionType.MA | QuestionType.N))
                {
                    case QuestionType.SA:
                    case QuestionType.MA:
                        FormatRangeNamePrefix = "SA_MA";
                        isN = false;
                        break;
                    case QuestionType.N:
                        FormatRangeNamePrefix = "N";
                        isN = true;
                        break;
                    default:
                        //Err().Raise vbObjectError + 100 &, RunningProcName, ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportUnjustQuestionTypeMessageIndex)
                        break;
                }
                HasWeight = GetHasWeight(tmpTable);
                if (SigTestOn) WholeRowCol = new Hashtable();
                GetCutRowsAndColumns(tmpTable, HasShowPreWBTotal, HasWeight, MaxAxesCount, ref CutRowsCol, ref CutColumnsCol, -1, false, false, WholeRowCol);

                OutputTable(tmpTable, ref NPerStartCell, TableType.NPer, isN, FormatRangeNamePrefix, HasWeight, tmpOrientation
                              , PageSetupOn, CutRowsCol, CutColumnsCol, MaxAxesCountArray[i], MaxAxesCount, ref PageSetupNPerStartCell
                              , i == 0, i + 1 == tmpTablesCount - 1, tmpNextTable, FormatSheet, ref NPerSheet, ref PageSetupNPerSheet
                                , ref ContentsValue, ref HyperlinkTargetCells, ref PageSetupContentsValue, ref PageSetupHyperlinkTargetCells
                              , strIdx, TemplateSheet, ContentsSheet, HasWeightColumn, PageSetupColumnsCountPerPage, MaxRowsCountPerPage, ref NPerRemainedRowsCount
                              , DefLineHeight
                              //,Errors, ErrorsCount, res
                              );
                //        DoEvents
                //        ' N•\
                OutputTable(tmpTable, ref NStartCell, TableType.N, isN, FormatRangeNamePrefix, HasWeight, tmpOrientation
                                  , PageSetupOn, CutRowsCol, CutColumnsCol, MaxAxesCountArray[i], MaxAxesCount, ref PageSetupNStartCell
                                  , i == 0, i + 1 == tmpTablesCount - 1, tmpNextTable, FormatSheet, ref NSheet, ref PageSetupNSheet
                                    , ref ContentsValue, ref HyperlinkTargetCells, ref PageSetupContentsValue, ref PageSetupHyperlinkTargetCells
                                  , strIdx, TemplateSheet, ContentsSheet, HasWeightColumn, PageSetupColumnsCountPerPage, MaxRowsCountPerPage, ref NRemainedRowsCount
                                  , DefLineHeight
                                  //, Errors, ErrorsCount, res
                                  );
                //        DoEvents
                //        ' “•\
                OutputTable(tmpTable, ref PerStartCell, TableType.Per, isN, FormatRangeNamePrefix, HasWeight, tmpOrientation
                          , PageSetupOn, CutRowsCol, CutColumnsCol, MaxAxesCountArray[i], MaxAxesCount, ref PageSetupPerStartCell
                          , i == 0, i + 1 == tmpTablesCount - 1, tmpNextTable, FormatSheet, ref PerSheet, ref PageSetupPerSheet
                          , ref ContentsValue, ref HyperlinkTargetCells, ref PageSetupContentsValue, ref PageSetupHyperlinkTargetCells
                          , strIdx, TemplateSheet, ContentsSheet, HasWeightColumn, PageSetupColumnsCountPerPage, MaxRowsCountPerPage, ref PerRemainedRowsCount
                          , DefLineHeight
                                // , Errors, ErrorsCount, res
                                );
                //        DoEvents
                //        ' ŒŸ’è•\
                OutputTable(tmpTable, ref SigTestStartCell, TableType.SignificanceTest, isN, FormatRangeNamePrefix, HasWeight, tmpOrientation
                                  , PageSetupOn, CutRowsCol, CutColumnsCol, MaxAxesCountArray[i], MaxAxesCount, ref PageSetupSigTestStartCell
                                  , i == 0, i + 1 == tmpTablesCount - 1, tmpNextTable, SigTestFormatSheet, ref SigTestSheet, ref PageSetupSigTestSheet
                                    , ref ContentsValue, ref HyperlinkTargetCells, ref PageSetupContentsValue, ref PageSetupHyperlinkTargetCells
                                  , strIdx, SigTestTemplateSheet, ContentsSheet, HasWeightColumn, SigTestPageSetupColumnsCountPerPage, MaxRowsCountPerPage, ref SigTestRemainedRowsCount
                                  , DefLineHeight
                                  //, Errors, ErrorsCount, res
                                  , WholeRowCol
                                  );
                //        DoEvents
            }
            PutContents(ContentsSheet, ref ContentsValue, ref HyperlinkTargetCells, Application);
            //    if( Not PageSetupContentsSheet == null ){
            //        PutContents PageSetupContentsSheet, PageSetupContentsValue, PageSetupHyperlinkTargetCells
            //    }
            SaveBook(NewBook, CurrentOutput.ExcelBookNamePrefix, Application, Suffix, FileFormat);
            //    if( res<> RaisedError ){ res = Successful
            //    CreateStandardCross = res
            //    if( res = RaisedError ){ PutErrorsInformation Errors
            //    RunningProcName = OrgProcName
            //    Exit Function
            //ErrHdl:
            //if( IsDebug ){
            //    Debug.Print RunningProcName
            //    Debug.Print Err().Number, Err().Description
            //    Stop
            //    Resume
            //}
            //    if( ResumeError ){
            //        res = RaisedError
            //        PushError Err(), Errors, ErrorsCount
            //        Resume Next
            //    }else{
            //        With Err()
            //            .Raise.Number, .Source, .Description, .HelpFile, .HelpContext
            //        End With
            //    }
            //End Function
        }

        private static void OutputTable(CrossTable Table, ref Range StartCell
              , TableType TableType, bool isN, string FormatRangeNamePrefix
              , bool HasWeight, TableOrientation Orientation, bool PageSetupOn
              , Hashtable CutRowsCol, Hashtable CutColumnsCol, int AxesCount, int MaxAxesCount
              , ref Range PageSetupStartCell, bool IsFirstTable, bool NextIsLast
            , CrossTable NextTable, Worksheet FormatSheet
            , ref Worksheet Sheet, ref Worksheet PageSetupSheet, ref Array ContentsValue, ref Array HyperlinkTargetCells
            , ref Array PageSetupContentsValue, ref Array PageSetupHyperlinkTargetCells, string TableIndex
            , Worksheet TemplateSheet, Worksheet ContentsSheet, bool HasWeightColumn
            , int PageSetupColumnsCountPerPage, int MaxRowsCountPerPage, ref int RemainedRowsCount, double DefLineHeight
            //, ByRef Errors() As ErrorStruct, ByRef ErrorsCount As Long, ByRef res As MethodResult _
            , Hashtable WholeRowCol = null)
        {
            string tmp = null;
            Array TableValue = null; // objec
            string[,] TableStringValue = null;// string
            object[] DataValue;// object
            Array Ranking = null; // int
            Array HatchingColorIndex = null; //XlColorIndex
            Array ArrowEnd = null;// object
            Array SigTestMarking = null; // string
            int PagesCount = 0;
            int PageRowsCount = 0;
            Array h;
            double maxH;
            int idx = 0;
            bool f;
            int i; ;
            bool CheckOverRow;
            bool CheckOverColumn = false;
            if (StartCell == null) return;

            f = true;
            if (Orientation == TableOrientation.Landscape)
            {
                switch (TableType)
                {
                    case TableType.NPer:
                        if (CurrentOutput.OutputNTable || CurrentOutput.OutputPerTable || CurrentOutput.SignificanceTest)
                        {
                            f = false;
                        }
                        break;
                    case TableType.SignificanceTest:
                        if (CurrentOutput.OutputNTable || CurrentOutput.OutputPerTable)
                        {
                            f = false;
                        }
                        break;
                }
            }
            else
            {
                switch (TableType)
                {
                    case TableType.SignificanceTest:
                        if (CurrentOutput.OutputNPerTable || CurrentOutput.OutputNTable || CurrentOutput.OutputPerTable)
                            f = false;
                        break;
                    case TableType.NPer:
                        if (CurrentOutput.OutputNTable || CurrentOutput.OutputPerTable)
                            f = false;
                        break;
                }
            }
            if (!isN)
            {
                switch (TableType)
                {
                    case TableType.NPer:
                        tmp = "_NP";
                        break;
                    case TableType.N:
                        tmp = "_N";
                        break;
                    case TableType.Per:
                    case TableType.SignificanceTest:
                        tmp = "_P";
                        break;
                }
                FormatRangeNamePrefix = FormatRangeNamePrefix + tmp;
                if (HasWeight)
                {
                    FormatRangeNamePrefix = FormatRangeNamePrefix + "_WT";
                }
            }
            switch (TableType)
            {
                case TableType.NPer:
                    tmp = "";
                    idx = 4; break;
                case TableType.N:
                    tmp = "N";
                    idx = 5; break;
                case TableType.Per:
                    tmp = "P";
                    idx = 6; break;
                case TableType.SignificanceTest:
                    tmp = "T";
                    idx = 0; break;
            }
            TableIndex = tmp + "Table" + TableIndex;
            if (Orientation == TableOrientation.Landscape)
            {

                CreateTurnedLandscapeCrossArray(Table, CutRowsCol, CutColumnsCol, ref TableValue, ref Ranking, ref HatchingColorIndex,
                    ref ArrowEnd, ref SigTestMarking
                                                  , 1 + (ToInt(HasWeight) & 1), 1  +AxesCount, HasWeight, isN
                                                   , TableType, StartCell.Worksheet.Rows.Count - StartCell.Row - 1
                                                  , StartCell.Worksheet.Columns.Count - 2, MaxAxesCount - AxesCount
                                                  , ref PagesCount, ref PageRowsCount, WholeRowCol);

                CheckOverRow = TableValue.Length == 0;
            }
            else
            {
                CheckOverRow = true;
                CheckOverColumn = true;
                //CreatePortraitCrossArray(Table, CutRowsCol, CutColumnsCol, TableStringValue, DataValue, Ranking, HatchingColorIndex, ArrowEnd, SigTestMarking 
                //                       , 1  +(ToInt(HasWeight) & 1 ), 1 & +AxesCount, HasWeightColumn, HasWeight, isN 
                //                , TableType, StartCell.Worksheet.Rows.Count - StartCell.Row - 1  
                //                       , StartCell.Worksheet.Columns.Count - 1 , CheckOverRow, CheckOverColumn);
            }
            if (CheckOverRow || CheckOverColumn)
            {
                StartCell = null;
                PageSetupStartCell = null;
                if (IsFirstTable)
                {
                    //Me.Application.DisplayAlerts = false;
                    Sheet.Delete();
                    Sheet = null;
                    if (PageSetupSheet != null)
                    {
                        PageSetupSheet.Delete();
                        PageSetupSheet = null;
                    }
                    if (f)
                    {
                        if (CheckOverRow)
                        {
                            //Err().Raise vbObjectError + 100 &, RunningProcName _
                            //           , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportRowsCountOverAtOneTableMessageIndex)
                        }
                        else
                        {
                            //Err().Raise vbObjectError + 101 &, RunningProcName _
                            //           , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportColumnsCountOverAtOneTableFailedMessageIndex)
                        }
                    }
                }
                ResumeError = true;
                switch (TableType)
                {
                    case TableType.NPer:
                        if (NextTable == null)
                        {
                            if (CheckOverRow)
                            {
                                //    Err().Raise vbObjectError + 150 &, RunningProcName _
                                //               , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportRowsCountOverDetailNPMessageIndex, Table.Question.Name)
                            }
                            else
                            {
                                //   Err().Raise vbObjectError + 151 &, RunningProcName _
                                //             , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportColumnsCountOverDetailNPWarningMessageIndex, Table.Question.Name)
                            }
                        }
                        else
                        {
                            if (CheckOverRow)
                            {
                                //   Err().Raise vbObjectError + 150 &, RunningProcName _
                                //              , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportRowsCountOverDetailNPOnAfterMessageIndex, Table.Question.Name)
                            }
                            else
                            {
                                //   Err().Raise vbObjectError + 151 &, RunningProcName _
                                //              , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportColumnsCountOverDetailNPOnAfterWarningMessageIndex, Table.Question.Name)
                            }
                        }
                        break;
                    case TableType.N:
                        if (NextTable == null)
                        {
                            if (CheckOverRow)
                            {
                                //  Err().Raise vbObjectError + 160 &, RunningProcName _
                                //             , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportRowsCountOverDetailNMessageIndex, Table.Question.Name)
                            }
                            else
                            {
                                //  Err().Raise vbObjectError + 161 &, RunningProcName _
                                //             , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportColumnsCountOverDetailNWarningMessageIndex, Table.Question.Name)
                            }
                        }
                        else
                        {
                            if (CheckOverRow)
                            {
                                //   Err().Raise vbObjectError + 160 &, RunningProcName _
                                //              , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportRowsCountOverDetailNOnAfterMessageIndex, Table.Question.Name)
                            }
                            else
                            {
                                //   Err().Raise vbObjectError + 161 &, RunningProcName _
                                //              , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportColumnsCountOverDetailNOnAfterWarningMessageIndex, Table.Question.Name)
                            }
                        }
                        break;
                    case TableType.Per:
                        if (NextTable == null)
                        {
                            if (CheckOverRow)
                            {
                                // Err().Raise vbObjectError + 170 &, RunningProcName _
                                //             , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportRowsCountOverDetailPMessageIndex, Table.Question.Name)
                            }
                            else
                            {
                                // Err().Raise vbObjectError + 171 &, RunningProcName _
                                //            , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportColumnsCountOverDetailPWarningMessageIndex, Table.Question.Name)
                            }
                        }
                        else
                        {
                            if (CheckOverRow)
                            {
                                //  Err().Raise vbObjectError + 170 &, RunningProcName _
                                //            , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportRowsCountOverDetailPOnAfterMessageIndex, Table.Question.Name)
                            }
                            else
                            {
                                //  Err().Raise vbObjectError + 171 &, RunningProcName _
                                //             , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportColumnsCountOverDetailPOnAfterWarningMessageIndex, Table.Question.Name)
                            }
                        }
                        break;
                    case TableType.SignificanceTest:
                        if (NextTable == null)
                        {
                            if (CheckOverRow)
                            {
                                //   Err().Raise vbObjectError + 180 &, RunningProcName _
                                //              , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportRowsCountOverDetailSignificanceTestMessageIndex, Table.Question.Name)
                            }
                            else
                            {
                                //   Err().Raise vbObjectError + 181 &, RunningProcName _
                                //             , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportColumnsCountOverDetailSignificanceTestWarningMessageIndex, Table.Question.Name)
                            }
                        }
                        else
                        {
                            if (CheckOverRow)
                            {
                                //  Err().Raise vbObjectError + 180 &, RunningProcName _
                                //           , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportRowsCountOverDetailSignificanceTestOnAfterMessageIndex, Table.Question.Name)
                            }
                            else
                            {
                                //   Err().Raise vbObjectError + 181 &, RunningProcName _
                                //              , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportColumnsCountOverDetailSignificanceTestOnAfterWarningMessageIndex, Table.Question.Name)
                            }
                        }
                        break;
                }
                if (f)
                {
                    ContentsValue.SetValue(String.Empty, Table.Index + 1, 1);
                    ContentsValue.SetValue(String.Empty, Table.Index + 1, 2);
                    if (PageSetupOn)
                    {
                        PageSetupContentsValue.SetValue(String.Empty, Table.Index + 1, 1);
                        PageSetupContentsValue.SetValue(String.Empty, Table.Index + 1, 2);
                    }
                }
            }

            else
            {    //' 出せた場合
                if (Orientation == TableOrientation.Landscape)
                {
                    FormatTurnedLandscapeTable(Table, CutRowsCol, CutColumnsCol, FormatSheet, FormatRangeNamePrefix
                                                        , TableType, HasWeight, StartCell.Worksheet.Columns.Count - 2
                                                         , PagesCount, StartCell, isN, AxesCount, WholeRowCol);
                    OutputUtil.PutValue(StartCell.Range["B2"], ref TableIndex);
                    Range resizRnge = StartCell.Range["B3"].Resize[TableValue.GetUpperBound(0), TableValue.GetUpperBound(1)];


                    resizRnge.Value = TableValue;
                    if (!isN)
                    {
                        if (CurrentOutput.MarkingRanking)
                        {
                            RankMarking(resizRnge.Cells, ref Ranking);
                        }
                        if (CurrentOutput.MarkingColoring)
                        {
                            Hatching(resizRnge.Cells, ref HatchingColorIndex);
                        }
                        if (CurrentOutput.MarkingAscending)
                        {
                            AscendingMarking(resizRnge.Cells, ref ArrowEnd);
                        }
                    }
                    if (CurrentOutput.MarkingSignificance)
                    {
                        SignificanceTestMarking(resizRnge.Cells, ref SigTestMarking);
                    }
                    //       ' オートフィット
                    //ReDim h(1 To PagesCount)
                    h = Array.CreateInstance(typeof(double), new int[] { PagesCount }, new int[] { 1 });// new double[PagesCount];
                    maxH = 0;
                    for (i = 1; i <= PagesCount; i++)
                    {
                        Range itemWith = resizRnge.Rows.Item[2 + (i - 1) * (PageRowsCount + 2) + 1];
                        OutputUtil.AutoFitEx(itemWith.Rows);
                        h.SetValue(itemWith.RowHeight, i);
                        if ((double)h.GetValue(i) > maxH)
                        {
                            maxH = (double)h.GetValue(i);
                        }
                    }
                    for (i = 1; i <= PagesCount; i++)
                    {
                        if ((double)h.GetValue(i) < maxH)
                        {
                            resizRnge.Rows.Item[2 + (i - 1) * (PageRowsCount + 2) + 1].RowHeight = maxH;
                        }
                    }
                    if (idx > 0)
                    {
                        ContentsValue.SetValue(TableIndex, Table.Index + 1, idx);
                        HyperlinkTargetCells.SetValue(StartCell, Table.Index + 1, idx);
                    }
                    if (resizRnge.Row + resizRnge.Rows.Count < resizRnge.Worksheet.Rows.Count)
                    {
                        StartCell = resizRnge.Item[resizRnge.Rows.Count + 1, 1].EntireRow.Range["A1"];
                    }
                    else
                    {
                        StartCell = null;
                        if (NextTable != null)
                        {
                            ResumeError = true;
                            switch (TableType)
                            {
                                case TableType.NPer:
                                    if (NextIsLast)
                                    {
                                        //     Err().Raise vbObjectError + 150 &, RunningProcName _
                                        //               , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportRowsCountOverDetailNPMessageIndex, NextTable.Question.Name)
                                    }
                                    else
                                    {
                                        //      Err().Raise vbObjectError + 150 &, RunningProcName _
                                        //                , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportRowsCountOverDetailNPOnAfterMessageIndex, NextTable.Question.Name)
                                    }
                                    break;
                                case TableType.N:
                                    if (NextIsLast)
                                    {
                                        //     Err().Raise vbObjectError + 160 &, RunningProcName _
                                        //                , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportRowsCountOverDetailNMessageIndex, NextTable.Question.Name)
                                    }
                                    else
                                    {
                                        //     Err().Raise vbObjectError + 160 &, RunningProcName _
                                        //                 , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportRowsCountOverDetailNOnAfterMessageIndex, NextTable.Question.Name)
                                    }
                                    break;
                                case TableType.Per:
                                    if (NextIsLast)
                                    {
                                        //   Err().Raise vbObjectError + 170 &, RunningProcName _
                                        //                , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportRowsCountOverDetailPMessageIndex, NextTable.Question.Name)
                                    }
                                    else
                                    {
                                        //    Err().Raise vbObjectError + 170 &, RunningProcName _
                                        //                , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportRowsCountOverDetailPOnAfterMessageIndex, NextTable.Question.Name)
                                    }
                                    break;
                                case TableType.SignificanceTest:
                                    if (NextIsLast)
                                    {
                                        //  Err().Raise vbObjectError + 180 &, RunningProcName _
                                        //                , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportRowsCountOverDetailSignificanceTestMessageIndex, NextTable.Question.Name)
                                    }
                                    else
                                    {
                                        //     Err().Raise vbObjectError + 180 &, RunningProcName _
                                        //                , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportRowsCountOverDetailSignificanceTestOnAfterMessageIndex, NextTable.Question.Name)
                                    }
                                    break;
                            }
                        }
                    }
                    if (PageSetupStartCell != null)
                    {
                        PageSetupLandscapeTable(Table, CutRowsCol, CutColumnsCol, FormatSheet, FormatRangeNamePrefix, TableType, HasWeight
                                              , PageSetupColumnsCountPerPage - 1, ref PageSetupStartCell, ref PageSetupSheet, isN
                                               , AxesCount, MaxAxesCount, ref RemainedRowsCount
                                              , MaxRowsCountPerPage, DefLineHeight, maxH, ref PageSetupContentsValue, ref PageSetupHyperlinkTargetCells
                                              , IsFirstTable, NextTable, TableIndex,
                                              //Errors, ErrorsCount, res, 
                                              WholeRowCol);
                    }
                }
                else
                {
                    // FormatPortraitTable Table, TemplateSheet, CutRowsCol, CutColumnsCol, FormatSheet, FormatRangeNamePrefix _
                    //                   , TableType, HasWeight, StartCell, isN, ContentsSheet, MaxAxesCount - AxesCount
                    // PutValue StartCell.Range("B2"), TableIndex
                    Range resizRnge = StartCell.Range["B3"].Resize[TableStringValue.GetUpperBound(0), TableStringValue.GetUpperBound(1)];
                    // PutValue.Cells, TableStringValue
                    // PutValue.Worksheet.Range(.Item(LBound(DataValue, 1), LBound(DataValue, 2)), .Item(.Rows.Count, .Columns.Count)), DataValue
                    if (HasWeight)
                    {

                        //Range itemWith = resizRnge.Item[DataValue.GetLowerBound(0) + (CurrentOutput.ShowPreWBTotal & 1) + 1, DataValue.GetLowerBound(1) - 1 &].Resize[Table.SectorsCount * (1 + (!isN && 1))];
                        //                   itemWith.Value = itemWith.Value;
                    }
                    if (!isN)
                    {
                        if (CurrentOutput.MarkingRanking)
                        {
                            //RankMarking resizRnge .Cells, Ranking
                        }
                        if (CurrentOutput.MarkingColoring)
                        {
                            // Hatching .Cells, HatchingColorIndex
                        }
                        if (CurrentOutput.MarkingAscending)
                        {
                            //AscendingMarking .Cells, ArrowEnd
                        }
                        //  ' if (CurrentOutput.MarkingSignificance { SignificanceTestMarking .Cells, SigTestMarking
                    }
                    if (CurrentOutput.MarkingSignificance)
                    {
                        //  SignificanceTestMarking .Cells, SigTestMarking
                    }
                    //   ' AutoFitEx .Rows.Item(3&).Resize(1& + AxesCount)
                    if (AxesCount == 1)
                    {
                        // AutoFitEx .Rows.Item[4];
                    }

                    else
                    {
                        resizRnge.Rows.Item[4].RowHeight = resizRnge.Rows.Item[4].RowHeight * 2;
                        // AutoFitEx.Rows.Item[5]
                    }
                    if (idx > 0)
                    {
                        ContentsValue.SetValue(TableIndex, Table.Index + 1, idx);
                        HyperlinkTargetCells.SetValue(StartCell, Table.Index + 1, idx);
                    }
                    if (resizRnge.Row + resizRnge.Rows.Count < resizRnge.Worksheet.Rows.Count)
                    {
                        StartCell = resizRnge.Item[resizRnge.Rows.Count + 1, 1].EntireRow.Range("A1");
                    }
                    else
                    {
                        StartCell = null;
                        if (NextTable != null)
                        {
                            ResumeError = true;
                            switch (TableType)
                            {
                                case TableType.NPer:
                                    if (NextIsLast)
                                    {
                                        //   Err().Raise vbObjectError + 150 &, RunningProcName _
                                        //               , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportRowsCountOverDetailNPMessageIndex, NextTable.Question.Name)
                                    }
                                    else
                                    {
                                        //      Err().Raise vbObjectError + 150 &, RunningProcName _
                                        //                  , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportRowsCountOverDetailNPOnAfterMessageIndex, NextTable.Question.Name)
                                    }
                                    break;
                                case TableType.N:
                                    if (NextIsLast)
                                    {
                                        //    Err().Raise vbObjectError + 160 &, RunningProcName _
                                        //               , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportRowsCountOverDetailNMessageIndex, NextTable.Question.Name)
                                    }
                                    else
                                    {
                                        //      Err().Raise vbObjectError + 160 &, RunningProcName _
                                        //                 , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportRowsCountOverDetailNOnAfterMessageIndex, NextTable.Question.Name)
                                    }
                                    break;
                                case TableType.Per:
                                    if (NextIsLast)
                                    {
                                        //      Err().Raise vbObjectError + 170 &, RunningProcName _
                                        //                 , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportRowsCountOverDetailPMessageIndex, NextTable.Question.Name)
                                    }
                                    else
                                    {
                                        //    Err().Raise vbObjectError + 170 &, RunningProcName _
                                        //                  , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportRowsCountOverDetailPOnAfterMessageIndex, NextTable.Question.Name)
                                    }
                                    break;
                                case TableType.SignificanceTest:
                                    if (NextIsLast)
                                    {
                                        //      Err().Raise vbObjectError + 180 &, RunningProcName _
                                        //                 , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportRowsCountOverDetailSignificanceTestMessageIndex, NextTable.Question.Name)
                                    }
                                    else
                                    {
                                        //     Err().Raise vbObjectError + 180 &, RunningProcName _
                                        //                 , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportRowsCountOverDetailSignificanceTestOnAfterMessageIndex, NextTable.Question.Name)
                                    }
                                    break;
                            }
                        }
                    }

                }
            }
            //ExitProc:
            //    RunningProcName = OrgProcName
            //    Exit Sub
            //ErrHdl:
            //if (IsDebug {
            //    Debug.Print RunningProcName
            //    Debug.Print Err().Number, Err().Description
            //    Stop
            //    Resume
            //}
            //    if (ResumeError {
            //        res = RaisedError
            //        PushError Err(), Errors, ErrorsCount
            //        Resume }
            //    }else{
            //        With Err()
            //            .Raise.Number, .Source, .Description, .HelpFile, .HelpContext
            //       End With
            //    }
        }


        private static void CreateTurnedLandscapeCrossArray(CrossTable Table
            , Hashtable CutRowsCol, Hashtable CutColumnsCol
              , ref Array TableValue
              , ref Array Ranking, ref Array HatchingColorIndex, ref Array ArrowEnd, ref Array SigTestMarking
              , int DataOffsetRow, int DataOffsetColumn
              , bool HasWeight, bool isN
              , TableType TableType, int MaxRowsCount, int ColumnsCountPerPage
              , int AddColumnsCount, ref int PagesCount, ref int PageRowsCount
              , Hashtable WholeRowCol = null)
        {
            int CaptionRowsCount = 0;
            int SectorsCountPerPage;
            int RowsCount = 0;
            int ColumnsCount = 0;
            int TotalRowsCount;
            int d = 0, d2 = 0;
            int PreWBColumnIndex;
            int x, y;
            int r, c;
            int i;
            bool isNew;
            Array tmp;
            string tmpBuf;
            object buf;
            XlColorIndex clr;
            bool f;
            bool IsSigTest;
            int lc = 0;
            TableType tType;

            int u;
            int[] tmpArrowEnd = new int[2];
            DataMarking reverseSide;

            int tmpY = 0;
            int tmpR = 0;
            bool IsShowPreWBTotal;
            bool IsMarkingSignificance;
            bool IsMarkingRanking;
            bool IsMarkingColoring;
            bool IsMarkingcending;
            bool IsMarkingColoringLevel2High;
            bool IsMarkingColoringLevel1High;
            bool IsMarkingColoringLevel2Low;
            bool IsMarkingColoringLevel1Low;
            int tmpLevel2HighColorIndex;
            int tmpLevel1HighColorIndex;
            int tmpLevel2LowColorIndex;
            int tmpLevel1LowColorIndex;
            int tmpRowIndexFrom;
            int tmpRowIndexTo;
            int tmpColumnIndexFrom;
            int tmpColumnIndexTo;
            object[,] tmpTableValue;
            double[,] tmpPercentValue;
            object[,] tmpSignificanceTestCharacters;
            object[,] tmpSignificanceMark;
            int[,] tmpRank;
            bool[,] tmpColoringLevel2High;
            bool[,] tmpColoringLevel1High;
            bool[,] tmpColoringLevel2Low;
            bool[,] tmpColoringLevel1Low;

            IsShowPreWBTotal = CurrentOutput.ShowPreWBTotal;
            IsMarkingSignificance = CurrentOutput.MarkingSignificance;
            IsMarkingRanking = CurrentOutput.MarkingRanking;
            IsMarkingColoring = CurrentOutput.MarkingColoring;
            IsMarkingcending = CurrentOutput.MarkingAscending;
            IsMarkingColoringLevel2High = CurrentOutput.MarkingColoringLevel2High;
            IsMarkingColoringLevel1High = CurrentOutput.MarkingColoringLevel1High;
            IsMarkingColoringLevel2Low = CurrentOutput.MarkingColoringLevel2Low;
            IsMarkingColoringLevel1Low = CurrentOutput.MarkingColoringLevel1Low;
            tmpLevel2HighColorIndex = CurrentOutput.Level2HighColorIndex;
            tmpLevel1HighColorIndex = CurrentOutput.Level1HighColorIndex;
            tmpLevel2LowColorIndex = CurrentOutput.Level2LowColorIndex;
            tmpLevel1LowColorIndex = CurrentOutput.Level1LowColorIndex;

            IsSigTest = (TableType & TableType.SignificanceTest) == TableType.SignificanceTest;
            //tType = TableType And Not TableType.SignificanceTest
            tType = TableType & ~TableType.SignificanceTest;
            if (IsSigTest)
            {
                tType = TableType.Per;
            }
            GetRequiredRowsColsCountLandscape(Table, CutRowsCol, CutColumnsCol, DataOffsetRow, AddColumnsCount, TableType
                                             , isN, ref d, ref CaptionRowsCount, ref RowsCount, ref ColumnsCount, ref d2, WholeRowCol);
            SectorsCountPerPage = ColumnsCountPerPage - DataOffsetColumn - AddColumnsCount - (ToInt(IsSigTest) & 1);

            PagesCount = (ColumnsCount - DataOffsetColumn - AddColumnsCount - (ToInt(IsSigTest) & 1) - 1 - (ToInt(IsShowPreWBTotal) & 1) - 1) / (SectorsCountPerPage - 1 - (ToInt(IsShowPreWBTotal) & 1)) + 1;
            PageRowsCount = RowsCount - CaptionRowsCount;
            TotalRowsCount = RowsCount + (PagesCount - 1) * (PageRowsCount + 2);
            if (TotalRowsCount > MaxRowsCount)
            {
                TableValue = new object[0, 0];
                return;
            }
            if (IsSigTest)
            {
                if (isN)
                {
                    if (PagesCount > 1)
                    {
                        lc = (ColumnsCount - 1) % ColumnsCountPerPage + 1;
                    }
                    else
                    {
                        lc = ColumnsCount;
                    }
                }
            }
            if (PagesCount > 1) 
            {
                ColumnsCount = ColumnsCountPerPage;
            }
            TableValue = Array.CreateInstance(typeof(object), new int[] { TotalRowsCount, ColumnsCount }, new int[] { 1, 1 });

            // ReDim Ranking(CaptionRowsCount +DataOffsetRow + 1 & To TotalRowsCount, 
            //DataOffsetColumn + AddColumnsCount + (IsSigTest And 1 &) +1 & To ColumnsCount)
            Ranking = Array.CreateInstance(typeof(int),
                new int[] { TotalRowsCount - (CaptionRowsCount + DataOffsetRow + 1) + 1, ColumnsCount - (DataOffsetColumn + AddColumnsCount + (ToInt(IsSigTest) & 1) + 1) + 1 },
                new int[] { CaptionRowsCount + DataOffsetRow + 1, DataOffsetColumn + AddColumnsCount + (ToInt(IsSigTest) & 1) + 1 });

            // HatchingColorIndex(LBound(Ranking, 1) To TotalRowsCount, LBound(Ranking, 2) To ColumnsCount)
            HatchingColorIndex = Array.CreateInstance(typeof(XlColorIndex),
                new int[] { (int)TotalRowsCount - Ranking.GetLowerBound(0) + 1, (int)ColumnsCount - Ranking.GetLowerBound(1) + 1 },
                new int[] { Ranking.GetLowerBound(0), Ranking.GetLowerBound(1) });

            //ReDim ArrowEnd(LBound(Ranking, 1) To TotalRowsCount, LBound(Ranking, 2) To ColumnsCount)
            ArrowEnd = Array.CreateInstance(typeof(object),
                new int[] { (int)TotalRowsCount - Ranking.GetLowerBound(0) + 1, (int)ColumnsCount - Ranking.GetLowerBound(1) + 1 },
                new int[] { Ranking.GetLowerBound(0), Ranking.GetLowerBound(1) });

            //ReDim SigTestMarking(LBound(Ranking, 1) To TotalRowsCount, LBound(Ranking, 2) To ColumnsCount)
            SigTestMarking = Array.CreateInstance(typeof(string),
                new int[] { (int)TotalRowsCount - Ranking.GetLowerBound(0) + 1, (int)ColumnsCount - Ranking.GetLowerBound(1) + 1 },
                new int[] { Ranking.GetLowerBound(0), Ranking.GetLowerBound(1) });

            tmpBuf = Table.Question.Name + ' ' + Table.Question.Description;
            OutputUtil.AddPrefix(ref tmpBuf, true);
            TableValue.SetValue(tmpBuf, 1, 1);
            PreWBColumnIndex = Table.GetTableValueColumnIndexMinimum + DataOffsetColumn;

            // ReDim tmp(Table.GetTableValueColumnIndexMinimum To PreWBColumnIndex -1 &)
            tmp = Array.CreateInstance(typeof(string),
                new int[] { (int)(PreWBColumnIndex - 1) - Table.GetTableValueColumnIndexMinimum + 1 },
                new int[] { Table.GetTableValueColumnIndexMinimum });

            r = CaptionRowsCount + DataOffsetRow;
            for (y = Table.GetTableValueRowIndexMinimum + DataOffsetRow; y <= Table.GetTableValueRowIndexMaximum; y++)
            {
                if (CutRowsCol.ContainsKey(y))
                {
                    if (tmp.GetValue(tmp.GetLowerBound(0)) == null)
                    {
                        for (x = tmp.GetLowerBound(0); x <= tmp.GetUpperBound(0); x++)
                        {
                            tmp.SetValue((Table.TableValue(y, x, true)), x);
                            string tmpArrStr = (string)tmp.GetValue(x);
                            OutputUtil.AddPrefix(ref tmpArrStr, true);
                            tmp.SetValue(tmpArrStr, x);
                        }
                    }
                }
                else
                {
                    r = r + 1;
                    c = 0;
                    if (tmp.GetValue(tmp.GetLowerBound(0)) == null)
                    {
                        for (x = tmp.GetLowerBound(0); x <= tmp.GetUpperBound(0); x++)

                        {
                            c = c + 1;
                            tmpBuf = Table.TableValue(y, x, true);
                            OutputUtil.AddPrefix(ref tmpBuf, true);
                            TableValue.SetValue(tmpBuf, r, c);
                        }
                    }
                    else
                    {
                        for (x = tmp.GetLowerBound(0); x <= tmp.GetUpperBound(0); x++)
                        {
                            c = c + 1;
                            tmpBuf = Table.TableValue(y, x, true);
                            OutputUtil.AddPrefix(ref tmpBuf, true);
                            if (null != tmpBuf && tmpBuf.Length > 0)
                            {
                                tmp.SetValue(tmpBuf, x);
                            }
                            TableValue.SetValue(tmp.GetValue(x), r, c);
                        }
                    }
                    if (IsSigTest)
                    {
                        x = tmp.GetUpperBound(0);
                        tmpBuf = Table.SignificanceTestCharacters(y, x);
                        OutputUtil.AddPrefix(ref tmpBuf, true);
                        if (null != tmpBuf && tmpBuf.Length > 0)
                        {
                            c = c + AddColumnsCount + 1;
                            TableValue.SetValue(tmpBuf, r, c);
                        }
                    }
                    for (i = 2; i <= PagesCount; i++)
                    {
                        c = 0;
                        for (x = tmp.GetLowerBound(0); x <= tmp.GetUpperBound(0); x++)
                        {
                            c = c + 1;
                            TableValue.SetValue(TableValue.GetValue(r, c), r + (PageRowsCount + 2) * (i - 1), c);
                        }
                    }
                    tmp = Array.CreateInstance(typeof(string),
                        new int[] { tmp.GetUpperBound(0) - tmp.GetLowerBound(0) + 1 },
                        new int[] { tmp.GetLowerBound(0) });
                    if (IsSigTest)
                    {
                        r = r + (WholeRowCol.ContainsKey(y) ? d : d2) - 1;
                    }
                    else
                    {
                        r = r + d - 1;
                    }
                }
            }
            if (IsSigTest)
            {
                if (isN)
                {
                    r = CaptionRowsCount + (PagesCount - 1) * (PageRowsCount + 2) + 1;
                    TableValue.SetValue(GetReportKeyword(ReportMessageIndex.ReportSignificanceTestRowColumnCaptionIndex), r, TableValue.GetUpperBound(1));
                }
            }
            i = 0;
            c = DataOffsetColumn + AddColumnsCount + (ToInt(IsSigTest) & 1);
            u = Table.GetTableValueRowIndexMinimum + DataOffsetRow - 1;
            for (x = PreWBColumnIndex; x <= Table.GetTableValueColumnIndexMaximum; x++)
            {
                if (!CutColumnsCol.ContainsKey(x))
                {
                    c = c + 1;
                    isNew = c - DataOffsetColumn - AddColumnsCount - (ToInt(IsSigTest) & 1) > SectorsCountPerPage;
                    if (isNew)
                    {
                        c = DataOffsetColumn + AddColumnsCount + (ToInt(IsSigTest) & 1) + 1;
                        i = i + 1;
                    }
                    r = CaptionRowsCount + i * (PageRowsCount + 2);
                    if (isNew)
                    {
                        TableValue.SetValue(GetReportKeyword(ReportMessageIndex.ReportToAfterTableMarkAtTurnKeywordIndex), r - 1, ColumnsCountPerPage);
                        TableValue.SetValue(GetReportKeyword(ReportMessageIndex.ReportFromBeforeTableMarkAtTurnKeywordIndex), r, c);
                        if (IsShowPreWBTotal)
                        {
                            TableValue.SetValue(TableValue.GetValue(CaptionRowsCount + 1, c), r + 1, c);
                            c = c + 1;
                        }
                        TableValue.SetValue(TableValue.GetValue(CaptionRowsCount + 1, c), r + 1, c);
                        c = c + 1;
                    }
                    for (y = Table.GetTableValueRowIndexMinimum; y <= u; y++)
                    {
                        tmpBuf = Table.TableValue(y, x, true);
                        r = r + 1;
                        if (!HasWeight || y != u || OutputUtil.IsNumeric(tmpBuf))
                        {
                            OutputUtil.AddPrefix(ref tmpBuf, true);
                            TableValue.SetValue(tmpBuf, r, c);
                        }
                        else
                        {
                            TableValue.SetValue(Convert.ToDouble(tmpBuf), r, c);
                        }
                    }
                }
            }
            // ' 性能対策 start
            tmpRowIndexFrom = Table.GetTableValueRowIndexMinimum + DataOffsetRow;
            tmpRowIndexTo = Table.GetTableValueRowIndexMaximum;
            tmpColumnIndexFrom = PreWBColumnIndex;
            tmpColumnIndexTo = Table.GetTableValueColumnIndexMaximum;
            tmpTableValue = Table.TableValueByMatrix(tmpRowIndexFrom, tmpRowIndexTo, tmpColumnIndexFrom, tmpColumnIndexTo);
            tmpPercentValue = Table.PercentValueByMatrix(tmpRowIndexFrom, tmpRowIndexTo, tmpColumnIndexFrom, tmpColumnIndexTo);
            tmpSignificanceTestCharacters =
                Table.SignificanceTestCharactersByMatrix(tmpRowIndexFrom, tmpRowIndexTo, tmpColumnIndexFrom, tmpColumnIndexTo);
            tmpSignificanceMark = Table.SignificanceMarkByMatrix(tmpRowIndexFrom, tmpRowIndexTo, tmpColumnIndexFrom, tmpColumnIndexTo);
            tmpRank = Table.RankByMatrix(tmpRowIndexFrom, tmpRowIndexTo, tmpColumnIndexFrom, tmpColumnIndexTo);
            tmpColoringLevel2High = Table.ColoringLevel2HighByMatrix(tmpRowIndexFrom, tmpRowIndexTo, tmpColumnIndexFrom, tmpColumnIndexTo);
            tmpColoringLevel1High = Table.ColoringLevel1HighByMatrix(tmpRowIndexFrom, tmpRowIndexTo, tmpColumnIndexFrom, tmpColumnIndexTo);
            tmpColoringLevel2Low = Table.ColoringLevel2LowByMatrix(tmpRowIndexFrom, tmpRowIndexTo, tmpColumnIndexFrom, tmpColumnIndexTo);
            tmpColoringLevel1Low = Table.ColoringLevel1LowByMatrix(tmpRowIndexFrom, tmpRowIndexTo, tmpColumnIndexFrom, tmpColumnIndexTo);
            //    ' 性能対策 end
            //    ' データ
            i = 0;
            c = DataOffsetColumn + AddColumnsCount + (ToInt(IsSigTest) & 1);
            for (x = PreWBColumnIndex; x <= Table.GetTableValueColumnIndexMaximum; x++)
            {
                if (!(CutColumnsCol.ContainsKey(x)))
                {
                    c = c + 1;
                    isNew = c - DataOffsetColumn - AddColumnsCount - (ToInt(IsSigTest) & 1) > SectorsCountPerPage;
                    if (isNew)
                    {
                        c = DataOffsetColumn + AddColumnsCount + (ToInt(IsSigTest) & 1) + (ToInt(IsShowPreWBTotal) & 1) + 1 + 1;
                        i = i + 1;
                    }
                    r = CaptionRowsCount + DataOffsetRow + i * (PageRowsCount + 2);
                    for (y = Table.GetTableValueRowIndexMinimum + DataOffsetRow;
                         y <= Table.GetTableValueRowIndexMaximum; y++)
                    {
                        if (!(CutRowsCol.ContainsKey(y)))
                        {
                            r = r + 1;
                            f = false;
                            if (HasWeight)
                            {
                                switch (Table.GetTableValueColumnIndexMaximum - x)
                                {
                                    case 0: //' 加重平均
                                        buf = tmpTableValue[y, x];
                                        if (OutputUtil.IsNumeric(buf)) { buf = Convert.ToDouble(buf); }
                                        TableValue.SetValue(buf, r + d - 1, c);
                                        if (IsSigTest)
                                        {
                                            if (!(WholeRowCol.ContainsKey(y)))
                                            {
                                                buf = tmpSignificanceTestCharacters[y, x];
                                                if (null != buf && ((string)buf).Length > 0)
                                                {
                                                    TableValue.SetValue(buf, r + d2 - 1, c);
                                                }
                                            }
                                        }
                                        else if (IsMarkingSignificance)
                                        {
                                            SigTestMarking.SetValue(tmpSignificanceMark[y, x], r + d - 1, c);
                                        }
                                        f = true; break;
                                    case 1:// & ' 加重平均母数
                                        buf = tmpTableValue[y, x];
                                        if (OutputUtil.IsNumeric(buf)) { buf = Convert.ToDouble(buf); }
                                        TableValue.SetValue(buf, r, c);
                                        f = true; break;
                                }
                            }
                            if (!f)
                            {
                                if (isN || x - PreWBColumnIndex <= (ToInt(IsShowPreWBTotal) & 1))
                                {  // ' WB前全体/全体
                                    buf = tmpTableValue[y, x];
                                    if (OutputUtil.IsNumeric(buf)) { buf = Convert.ToDouble(buf); }
                                    TableValue.SetValue(buf, r, c);
                                    if (isN)
                                    {
                                        if (IsSigTest)
                                        {
                                            if (!(WholeRowCol.ContainsKey(y)))
                                            {
                                                buf = tmpSignificanceTestCharacters[y, x];
                                                if (null != buf && ((string)buf).Length > 0)
                                                {
                                                    TableValue.SetValue(buf, r + (PagesCount - i - 1) * (PageRowsCount + 2), lc);
                                                }
                                            }
                                        }
                                        else if (IsMarkingSignificance)
                                        {
                                            SigTestMarking.SetValue(tmpSignificanceMark[y, x], r, c);
                                        }
                                    }
                                }
                                else
                                {
                                    if (tType == TableType.Per)
                                    {
                                        TableValue.SetValue(tmpPercentValue[y, x], r, c);
                                    }
                                    else
                                    {
                                        buf = tmpTableValue[y, x];
                                        if (OutputUtil.IsNumeric(buf)) { buf = Convert.ToDouble(buf); }
                                        TableValue.SetValue(buf, r, c);
                                        if (tType == TableType.NPer)
                                        {
                                            TableValue.SetValue(tmpPercentValue[y, x], r + 1, c);
                                        }
                                    }
                                    if (IsSigTest)
                                    {
                                        if (!(WholeRowCol.ContainsKey(y)))
                                        {
                                            buf = tmpSignificanceTestCharacters[y, x];
                                            if (null != buf && ((string)buf).Length > 0) { TableValue.SetValue(buf, r + d2 - 1, c); }
                                        }
                                    }
                                    //                                ' ランキング
                                    if (IsMarkingRanking) { Ranking.SetValue(tmpRank[y, x], r, c); }
                                    //                                ' ハッチング
                                    if (IsMarkingColoring)
                                    {
                                        clr = (XlColorIndex)(-1);

                                        if (IsMarkingColoringLevel2High)
                                        {
                                            if ((bool)tmpColoringLevel2High[y, x]) { clr = (XlColorIndex)tmpLevel2HighColorIndex; }
                                        }
                                        if (clr == (XlColorIndex)(-1))
                                        {
                                            if (IsMarkingColoringLevel1High)
                                            {
                                                if ((bool)tmpColoringLevel1High[y, x]) { clr = (XlColorIndex)tmpLevel1HighColorIndex; }
                                            }
                                        }
                                        if (clr == (XlColorIndex)(-1))
                                        {
                                            if (IsMarkingColoringLevel2Low)
                                            {
                                                if ((bool)tmpColoringLevel2Low[y, x]) { clr = (XlColorIndex)tmpLevel2LowColorIndex; }
                                            }
                                        }
                                        if (clr == (XlColorIndex)(-1))
                                        {
                                            if (IsMarkingColoringLevel1Low)
                                            {
                                                if ((bool)tmpColoringLevel1Low[y, x]) { clr = (XlColorIndex)tmpLevel1LowColorIndex; }
                                            }
                                        }
                                        if (clr < (XlColorIndex)(1) || clr > (XlColorIndex)(56))
                                        {
                                            clr = (XlColorIndex)2;
                                        }

                                        HatchingColorIndex.SetValue(clr, r, c);
                                        if (tType == TableType.NPer) { HatchingColorIndex.SetValue(clr, r + 1, c); }
                                        if (IsSigTest)
                                        {
                                            if (!WholeRowCol.ContainsKey(y))
                                            {
                                                HatchingColorIndex.SetValue(clr, r + d2 - 1, c);
                                            }
                                        }
                                    }
                                    if (IsMarkingcending)
                                    {
                                        if (!(ArrowEnd.GetValue(r, c).GetType().IsArray))
                                        {
                                            if (Table.IsArrowEnd(y, x, out reverseSide))
                                            {
                                                if (IsSigTest)
                                                {
                                                    tmpR = r + (WholeRowCol.ContainsKey(y) ? d : d2) - 1;//' 全体行のことはないけど、形をそろえる
                                                }
                                                else
                                                {
                                                    tmpR = r + d - 1;
                                                }
                                                for (tmpY = y + 1; y <= Table.GetTableValueRowIndexMaximum; y++)
                                                {
                                                    if (!(CutRowsCol.ContainsKey(tmpY)))
                                                    {
                                                        if (IsSigTest)
                                                        {
                                                            tmpR = tmpR + (WholeRowCol.ContainsKey(tmpY) ? d : d2);
                                                        }
                                                        else
                                                        {
                                                            tmpR = tmpR + d;
                                                        }
                                                        if (!Table.IsArrowShaft(tmpY, x))
                                                        {
                                                            // 最小ベースで出さないものは、ここではじく
                                                            if (Table.AscendingMarking(tmpY, x) == reverseSide)
                                                            {
                                                                tmpArrowEnd[1] = c;
                                                                if (reverseSide == DataMarking.AscendingStart)
                                                                {
                                                                    tmpArrowEnd[0] = r;
                                                                    ArrowEnd.SetValue(tmpArrowEnd, tmpR, c);
                                                                }
                                                                else if (reverseSide == DataMarking.AscendingEnd)
                                                                {
                                                                    tmpArrowEnd[0] = tmpR;
                                                                    ArrowEnd.SetValue(tmpArrowEnd, r, c);
                                                                }
                                                            }
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    //                                ' 全体との差の検定
                                    if (IsMarkingSignificance) { SigTestMarking.SetValue(tmpSignificanceMark[y, x], r + (ToInt(tType == TableType.NPer) & 1), c); }
                                }
                            }
                            if (IsSigTest)
                            {
                                r = r + (WholeRowCol.ContainsKey(y) ? d : d2) - 1;
                            }
                            else
                            {
                                r = r + d - 1;
                            }
                        }
                    }
                }
            }
            //    ' WB前全体と全体のコピー
            for (i = 2; i <= PagesCount; i++)
            {
                r = CaptionRowsCount + DataOffsetRow;
                for (y = Table.GetTableValueRowIndexMinimum + DataOffsetRow;
                     y <= Table.GetTableValueRowIndexMaximum; y++)
                {
                    if (!CutRowsCol.ContainsKey(y))
                    {
                        r = r + 1;
                        c = DataOffsetColumn + AddColumnsCount + (ToInt(IsSigTest) & 1);
                        for (x = Table.GetTableValueColumnIndexMinimum + DataOffsetColumn;
                             x <= Table.GetTableValueColumnIndexMinimum + DataOffsetColumn + (ToInt(IsShowPreWBTotal) & 1); x++)
                        {
                            c = c + 1;
                            TableValue.SetValue(TableValue.GetValue(r, c), r + (i - 1) * (PageRowsCount + 2), c);
                        }
                        if (IsSigTest)
                        {
                            r = r + (WholeRowCol.ContainsKey(y) ? d : d2) - 1;
                        }
                        else
                        {
                            r = r + d - 1;
                        }
                    }
                }
            }
        }

        private static int ToInt(bool test)
        {
            return test ? -1 : 0;
        }

        private static void AdjustContentsSheet(Hashtable Books, Worksheet ContentsSheet
      , ref Array ContentsValue, ref Array HyperlinkTargetCells
      , TableType TableType
      , int MinIndex = 0, int MaxIndex = 0)
        {
            CrossTable tmpTable;
            string buf;
            GroupObject g;
            TextBox b;
            double r;
            double d;
            Shape tmpS, tmpS2;
            Shape[] s = new Shape[1];
            ; int cnt = 0;
            int i;
            double t1, t2;
            int c;
            int n;
            Array v;
            XlColorIndex clrIdx;
            double h;
            double delH = 0;
            int u;
            bool IsSigTest;
            IsSigTest = (TableType & TableType.SignificanceTest) == TableType.SignificanceTest;
            ContentsSheet.Unprotect(macroExecuter.Templatesheetpassword);
            ContentsSheet.Rectangles("TitleBox").Text = CurrentOutput.ParentRequest.Title;
            tmpTable = (CrossTable)CurrentOutput.Tables[0];
            if (tmpTable.KeyItem == null)
            {
                Name WithContentsSheetItem = ContentsSheet.Names.Item("KeyItemInformation");
                Range WithWithContentsSheetItemRef = WithContentsSheetItem.RefersToRange.EntireRow;
                delH = WithWithContentsSheetItemRef.Height;
                WithWithContentsSheetItemRef.Delete(XlDeleteShiftDirection.xlShiftUp);
                WithContentsSheetItem.Delete();
            }
            else
            {
                v = new string[2, 2];
                v.SetValue(GetReportKeyword(ReportMessageIndex.ReportClassificationItemKeywordIndex), 0, 0);
                v.SetValue(GetReportKeyword(ReportMessageIndex.ReportSectorKeywordIndex), 1, 0);
                KeyItemInformation WithtmpTable = tmpTable.KeyItem;
                v.SetValue(WithtmpTable.Name + ":" + WithtmpTable.Description, 0, 1);
                v.SetValue(WithtmpTable.SectorNumber + ":" + WithtmpTable.SectorDescription, 1, 1);

                Range WithContentsSheetRng = ContentsSheet.Range["KeyItemInformation"].Range["B1:C2"];
                OutputUtil.PutValue(WithContentsSheetRng.Cells, ref v);
                OutputUtil.AutoFitEx(WithContentsSheetRng.Rows);
            }
            buf = CurrentOutput.LocalizedFilteringExpression;
            if (null == buf || buf.Length == 0)
            {
                Name WithContentsSheetNams = ContentsSheet.Names.Item("Criteria");
                WithContentsSheetNams.RefersToRange.Clear();
                WithContentsSheetNams.Delete();
            }
            else
            {
                v = new string[2];
                v.SetValue(GetReportKeyword(ReportMessageIndex.ReportFilterCriterionKeywordIndex), 0);
                v.SetValue(buf, 1);
                Range WithContentsSheetRng = ContentsSheet.Range["Criteria"];
                OutputUtil.PutValue(WithContentsSheetRng.Cells, ref v);
                Range WithContentsSheetRngER = WithContentsSheetRng.EntireRow;
                h = WithContentsSheetRngER.RowHeight;
                WithContentsSheetRngER.AutoFit();
                if (WithContentsSheetRngER.RowHeight < h) { WithContentsSheetRngER.RowHeight = h; }
            }
            if (CurrentOutput.WBOn)
            {
                string msg = GetReportKeyword(ReportMessageIndex.ReportMarkingLegendWeightbackOnPromptIndex);
                OutputUtil.PutValue(ContentsSheet.Range["WBPrompt"], ref msg);
            }
            if (CurrentOutput.MarkingRanking || CurrentOutput.MarkingColoring || CurrentOutput.MarkingSignificance ||
                     CurrentOutput.MarkingAscending || IsSigTest)
            {
                if (CurrentOutput.MinSamplesCountOnMarking > 0)
                {
                    string msg = GetReportKeyword(ReportMessageIndex.ReportMarkingLegendMinBasePromptIndex,
                        CurrentOutput.MinSamplesCountOnMarking.ToString());
                    OutputUtil.PutValue(ContentsSheet.Range["MinBase"], ref msg);
                }
            }
            g = ContentsSheet.GroupObjects("RateDifferenceLegend");
            b = ContentsSheet.TextBoxes("SignificanceTestLegend");
            r = g.Left + g.Width;
            d = b.Left - r;
            r = b.Left + b.Width;
            Shapes WithShapes = ContentsSheet.Shapes;
            tmpS = WithShapes.Item("SignificanceTestLegend");
            if (IsSigTest || CurrentOutput.MarkingSignificance)
            {
                u = -1;
                v = "".Split();
                if (IsSigTest)
                {
                    if (CurrentOutput.SignificanceTestOne)
                    {
                        if (u < 1)
                        {
                            u = u + 1;
                            ArrayPreserve(ref v, typeof(string), u);
                            v.SetValue(GetReportKeyword(ReportMessageIndex.ReportMarkingLegendSignificanceTestAt1CaptionIndex), u);
                        }
                    }
                    if (CurrentOutput.SignificanceTestFive)
                    {
                        if (u < 1)
                        {
                            u = u + 1;
                            ArrayPreserve(ref v, typeof(string), u);
                            string msg = u == 1 ? GetReportKeyword(ReportMessageIndex.ReportMarkingLegendSignificanceTestAt5CaptionIndex).ToLower() : GetReportKeyword(ReportMessageIndex.ReportMarkingLegendSignificanceTestAt5CaptionIndex);
                            v.SetValue(msg, u);
                        }
                    }
                    if (CurrentOutput.SignificanceTestTen)
                    {
                        if (u < 1)
                        {
                            u = u + 1;
                            ArrayPreserve(ref v, typeof(string), u);
                            string msg = u == 1 ? GetReportKeyword(ReportMessageIndex.ReportMarkingLegendSignificanceTestAt10CaptionIndex).ToLower() : GetReportKeyword(ReportMessageIndex.ReportMarkingLegendSignificanceTestAt10CaptionIndex);
                            v.SetValue(msg, u);
                        }
                    }
                    tmpS.DrawingObject.Text = GetUnescapedReportKeyword(ReportMessageIndex.ReportMarkingLegendSignificanceTestCaptionIndex) + "\n" + String.Join("\n", (string[])v);
                }
                else
                {
                    if (CurrentOutput.MarkingSignificanceOne)
                    {
                        u = u + 1;
                        ArrayPreserve(ref v, typeof(string), u);
                        v.SetValue(GetReportKeyword(ReportMessageIndex.ReportMarkingLegendSignificanceTestToWholeAt1CaptionIndex), u);
                    }
                    if (CurrentOutput.MarkingSignificanceFive)
                    {
                        u = u + 1;
                        ArrayPreserve(ref v, typeof(string), u);
                        v.SetValue(GetReportKeyword(ReportMessageIndex.ReportMarkingLegendSignificanceTestToWholeAt5CaptionIndex), u);
                    }
                    if (CurrentOutput.MarkingSignificanceTen)
                    {
                        u = u + 1;
                        ArrayPreserve(ref v, typeof(string), u);
                        v.SetValue(GetReportKeyword(ReportMessageIndex.ReportMarkingLegendSignificanceTestToWholeAt10CaptionIndex), u);
                    }
                    tmpS.DrawingObject.Text = GetUnescapedReportKeyword(ReportMessageIndex.ReportMarkingLegendSignificanceTestToWholeCaptionIndex) + "\n" + String.Join("\n", (string[])v);
                }
                Array.Resize(ref s, cnt+1);
                s[cnt] = tmpS;
                cnt = cnt + 1;
            }
            else
            {
                tmpS.Delete();
            }
            tmpS = WithShapes.Item("RateDifferenceLegend");
            if (CurrentOutput.MarkingColoring)
            {
                tmpS.TextEffect.Text = GetReportKeyword(ReportMessageIndex.ReportMarkingLegendRateDifferenceCaptionIndex);
                Array.Resize(ref s, cnt + 1);
                s[cnt] = tmpS;
                cnt = cnt + 1;
            }
            else
            {
                tmpS.Delete();
            }
            tmpS = WithShapes.Item("RankingMarkingLegend");
            if (CurrentOutput.MarkingRanking)
            {
                tmpS.TextEffect.Text = GetReportKeyword(ReportMessageIndex.ReportMarkingLegendRankingCaptionIndex);
                WithShapes.Item("Rank1Label").TextEffect.Text = GetReportKeyword(ReportMessageIndex.ReportMarkingLegendRanking1stCaptionIndex);
                WithShapes.Item("Rank2Label").TextEffect.Text = GetReportKeyword(ReportMessageIndex.ReportMarkingLegendRanking2ndCaptionIndex);
                WithShapes.Item("Rank3Label").TextEffect.Text = GetReportKeyword(ReportMessageIndex.ReportMarkingLegendRanking3rdCaptionIndex);
                Array.Resize(ref s, cnt + 1);
                s[cnt] = tmpS;
                cnt = cnt + 1;
            }
            else
            {
                tmpS.Delete();
            }
            tmpS = null;
            for (i = 0; i <= cnt - 1; i++)
            {
                s[i].Left = (float)r - s[i].Width;
                s[i].Top = s[i].Top - (float)delH;
                r = s[i].Left - (float)d;
            }
            if (CurrentOutput.MarkingColoring)
            {
                t1 = WithShapes.Item("Level2HighLabel").Top;
                t2 = WithShapes.Item("Level2HighPalette").Top;
                d = WithShapes.Item("Level1HighLabel").Top - t1;
                tmpS = WithShapes.Item("Level2HighLabel");
                tmpS2 = WithShapes.Item("Level2HighPalette");
                if (CurrentOutput.MarkingColoringLevel2High)
                {
                    tmpS.TextEffect.Text = GetReportKeyword(
                          ReportMessageIndex.ReportMarkingLegendRateDifferenceHighCaptionIndex
                       , (' ' + CurrentOutput.Level2Percent).ToString().Substring(CurrentOutput.Level2Percent.ToString().Length - 1));
                    clrIdx = (XlColorIndex)CurrentOutput.Level2HighColorIndex;
                    if (clrIdx < (XlColorIndex)1 || clrIdx > (XlColorIndex)56)
                    {
                        clrIdx = (XlColorIndex)2;// ' 白
                    }

                    tmpS2.Fill.ForeColor.RGB = Convert.ToInt32(WorkingBook.Colors[clrIdx]);
                    tmpS.Top = (float)t1;
                    tmpS2.Top = (float)t2;
                    t1 = t1 + d;
                    t2 = t2 + d;
                }
                else
                {
                    tmpS.Delete();
                    tmpS2.Delete();
                }
                tmpS = WithShapes.Item("Level1HighLabel");
                tmpS2 = WithShapes.Item("Level1HighPalette");
                if (CurrentOutput.MarkingColoringLevel1High)
                {
                    tmpS.TextEffect.Text = GetReportKeyword(
                         ReportMessageIndex.ReportMarkingLegendRateDifferenceHighCaptionIndex
                         , (' ' + CurrentOutput.Level1Percent).ToString().Substring(CurrentOutput.Level1Percent.ToString().Length - 1));
                    clrIdx = (XlColorIndex)CurrentOutput.Level1HighColorIndex;
                    if (clrIdx < (XlColorIndex)1 || clrIdx > (XlColorIndex)56)
                    {
                        clrIdx = (XlColorIndex)2;// ' 白
                    }
                    tmpS2.Fill.ForeColor.RGB = Convert.ToInt32(WorkingBook.Colors[clrIdx]);
                    tmpS.Top = (float)t1;
                    tmpS2.Top = (float)t2;
                    t1 = t1 + d;
                    t2 = t2 + d;
                }
                else
                {
                    tmpS.Delete();
                    tmpS2.Delete();
                }
                tmpS = WithShapes.Item("Level1LowLabel");
                tmpS2 = WithShapes.Item("Level1LowPalette");
                if (CurrentOutput.MarkingColoringLevel1Low)
                {
                    tmpS.TextEffect.Text = GetReportKeyword(
                          ReportMessageIndex.ReportMarkingLegendRateDifferenceLowCaptionIndex
                           , (' ' + CurrentOutput.Level1Percent).ToString().Substring(CurrentOutput.Level1Percent.ToString().Length - 1));
                    clrIdx = (XlColorIndex)CurrentOutput.Level1LowColorIndex;
                    if (clrIdx < (XlColorIndex)1 || clrIdx > (XlColorIndex)56)
                    {
                        clrIdx = (XlColorIndex)2;// ' 白
                    }
                    tmpS2.Fill.ForeColor.RGB = Convert.ToInt32(WorkingBook.Colors[clrIdx]);
                    tmpS.Top = (float)t1;
                    tmpS2.Top = (float)t2;
                    t1 = t1 + d;
                    t2 = t2 + d;
                }
                else
                {
                    tmpS.Delete();
                    tmpS2.Delete();
                }
                tmpS = WithShapes.Item("Level2LowLabel");
                tmpS2 = WithShapes.Item("Level2LowPalette");
                if (CurrentOutput.MarkingColoringLevel2Low)
                {
                    tmpS.TextEffect.Text = GetReportKeyword(
                           ReportMessageIndex.ReportMarkingLegendRateDifferenceLowCaptionIndex
                           , (' ' + CurrentOutput.Level2Percent).ToString().Substring(CurrentOutput.Level2Percent.ToString().Length - 1));
                    clrIdx = (XlColorIndex)CurrentOutput.Level2LowColorIndex;
                    if (clrIdx < (XlColorIndex)1 || clrIdx > (XlColorIndex)56)
                    {
                        clrIdx = (XlColorIndex)2;// ' 白
                    }
                    tmpS2.Fill.ForeColor.RGB = Convert.ToInt32(WorkingBook.Colors[clrIdx]);
                    tmpS.Top = (float)t1;
                    tmpS2.Top = (float)t2;
                    t1 = t1 + d;
                    t2 = t2 + d;
                }
                else
                {
                    tmpS.Delete();
                    tmpS2.Delete();
                }
            }

            c = CurrentOutput.Tables.Count;
            if (Books == null)
            {    //' 1シート複数クロス
                ContentsValue = Array.CreateInstance(typeof(string),
                    new int[] { c, 6 },
                    new int[] { 1, 1 });
                n = c;
            }
            else
            {   // ' 1シート1クロス
                ContentsValue = Array.CreateInstance(typeof(string),
                    new int[] { MaxIndex - MinIndex + 1, 4 },
                    new int[] { MinIndex, 1 });
                n = MaxIndex - MinIndex + 1;
            }
            HyperlinkTargetCells = Array.CreateInstance(typeof(Range),
                new int[] { ContentsValue.GetUpperBound(0) - ContentsValue.GetLowerBound(0) + 1, ContentsValue.GetUpperBound(1) - 4 + 1 },
                new int[] { ContentsValue.GetLowerBound(0), 4 });

            Range rowWith = ContentsSheet.Range["Contents"].EntireRow.Item[2];
            if (n > 3)
            {
                rowWith.Copy();
                rowWith.Resize[n - 3].Insert(XlInsertShiftDirection.xlShiftDown);
                rowWith.Application.CutCopyMode = (XlCutCopyMode)1;
            }
            else if (n < 3)
            {
                rowWith.Resize[3 - n].Delete(XlDeleteShiftDirection.xlShiftUp);
                if (n < 2)
                {
                    Border borderWith = ContentsSheet.Range["Contents"].Borders.Item[XlBordersIndex.xlEdgeBottom];
                    borderWith.LineStyle = XlLineStyle.xlContinuous;
                    borderWith.Weight = XlBorderWeight.xlThin;
                }
            }
            v = Array.CreateInstance(typeof(string), new int[] { ContentsValue.GetUpperBound(1) }, new int[] { 1 });
            v.SetValue(GetReportKeyword(ReportMessageIndex.ReportLayoutQuestionNumberColumnCaptionIndex), 1);
            v.SetValue(GetReportKeyword(ReportMessageIndex.ReportLayoutQC3Description2ColumnCaptionIndex), 2);
            if ((CurrentOutput.ParentReportset.FileType & FileType.Report) == FileType.Report)
            {
                v.SetValue(GetReportKeyword(ReportMessageIndex.ReportPageKeywordIndex), 4);
            }
            else if (CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Single)
            {
                switch (TableType)
                {
                    case TableType.NPer:
                        v.SetValue(GetReportKeyword(ReportMessageIndex.ReportCrossNPSheetNameIndex), 4);
                        break;
                    case TableType.N:
                        v.SetValue(GetReportKeyword(ReportMessageIndex.ReportCrossNSheetNameIndex), 4);
                        break;
                    case TableType.Per:
                        v.SetValue(GetReportKeyword(ReportMessageIndex.ReportCrossPSheetNameIndex), 4);
                        break;
                    case TableType.SignificanceTest:
                        v.SetValue(GetReportKeyword(ReportMessageIndex.ReportCrossSignificanceTestSheetNameIndex), 4);
                        break;
                }
            }
            else
            {
                v.SetValue(GetReportKeyword(ReportMessageIndex.ReportCrossNPSheetNameIndex), 4);
                v.SetValue(GetReportKeyword(ReportMessageIndex.ReportCrossNSheetNameIndex), 5);
                v.SetValue(GetReportKeyword(ReportMessageIndex.ReportCrossPSheetNameIndex), 6);
            }
            OutputUtil.PutValue(ContentsSheet.Range["ContentsCaption"], ref v);
        }

        private static void ArrayPreserve(ref Array v, Type type, int u)
        {
            Array t = (Array)v.Clone();
            v = Array.CreateInstance(type, u+1);
            t.CopyTo(v, 0);
        }

        private static void GetRequiredRowsColsCountLandscape(CrossTable Table
      , Hashtable CutRowsCol, Hashtable CutColumnsCol
      , int DataOffsetRow, int AddColumnsCount
      , TableType TableType, bool isN
      , ref int d, ref int CaptionRowsCount
      , ref int RequiredRowsCount, ref int RequiredColumnsCount
      , ref int d2, Hashtable WholeRowCol = null)
        {
            int RowsCount;
            int ColumnsCount;
            int DataRowsCount;
            int r;
            bool IsSigTest;
            int i;
            IsSigTest = (TableType & TableType.SignificanceTest) == TableType.SignificanceTest;

            //d = 1 & +(Not isN And(TableType And TableType_NPer) = TableType_NPer And 1 &)
            //d2 = d + (Not isN And IsSigTest And 1 &)

            d = 1 + (ToInt(!isN & ((TableType & TableType.NPer) == TableType.NPer)) & 1);
            d2 = d + (ToInt(!isN & IsSigTest) & 1);

            RowsCount = Table.GetTableValueRowIndexMaximum - Table.GetTableValueRowIndexMinimum + 1 - CutRowsCol.Count;
            ColumnsCount = Table.GetTableValueColumnIndexMaximum - Table.GetTableValueColumnIndexMinimum + 1 - CutColumnsCol.Count + AddColumnsCount;
            if (IsSigTest)
            {
                r = Table.GetTableValueRowIndexMinimum + DataOffsetRow; //' GTs
                DataRowsCount = d;
                for (i = r + 1; i <= Table.GetTableValueRowIndexMaximum; i++)
                {
                    if (!(CutRowsCol.ContainsKey(i)))
                    {
                        DataRowsCount = DataRowsCount + (WholeRowCol.ContainsKey(i) ? d : d2);
                    }
                }
            }
            else
            {
                DataRowsCount = (RowsCount - DataOffsetRow) * d;
            }
            if ((Table.ParentReportset.FileType & FileType.Report) == 0)
            {
                CaptionRowsCount = 2 + (ToInt(CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Single) & 1);
                RequiredRowsCount = CaptionRowsCount + DataOffsetRow + DataRowsCount;
            }
            else
            {
                CaptionRowsCount = 0;
                RequiredRowsCount = DataOffsetRow + DataRowsCount;
            }
            RequiredColumnsCount = ColumnsCount + (ToInt(IsSigTest) & (1 + (ToInt(isN) & 1)));
        }

        public static int GetMaxAxesCount(CrossTable Table)
        {
            int res;
            int i;
            res = 1;
            AxesGroupInformation WithTableAxesGroups = Table.AxesGroups;
            for (i = 1; i <= WithTableAxesGroups.Count; i++)
            {
                if (WithTableAxesGroups[i - 1].Count == 2)
                {
                    res = 2;
                    break;
                }
            }
            return res;
        }

        public static bool GetHasWeight(CrossTable Table)
        {
            if ((Table.Question.QuestionType & (QuestionType.SA | QuestionType.MA)) == 0) { return false; }
            string val = Table.TableValue(Table.GetTableValueRowIndexMinimum + 1, Table.GetTableValueColumnIndexMinimum);
            return null == val || val.Length == 0;
        }

        public static void AdjustFormat(
        Worksheet FormatSheet, Worksheet SigTestFormatSheet
              , int MaxAxesCount, bool HasWeightColumn, bool OnlyCutTriple = false, bool ExtendRowHeight = false)
        {
            string[] tmpNamesArray;
            string tmpName;
            int tmp;
            string Suffix;
            string tmpSuffix;
            string fmt;
            bool IsPortrait;
            XlDeleteShiftDirection tmpDelShift;
            int i;
            Range tmpRange;
            bool RedrawBorder;
            IsPortrait = CurrentOutput.Orientation == TableOrientation.Portrait;
            if (!OnlyCutTriple)
            {
                if (FormatSheet != null)
                {
                    FormatSheet.Unprotect(macroExecuter.Templatesheetpassword);
                }
                if (SigTestFormatSheet != null)
                {
                    SigTestFormatSheet.Unprotect(macroExecuter.Templatesheetpassword);
                }
                if (!CurrentOutput.ParentRequest.MergeAxis)
                {
                    if (FormatSheet != null)
                    {
                        Range WithFormatSheetRange = FormatSheet.Range["MergedCells"];
                        WithFormatSheetRange.UnMerge();
                        WithFormatSheetRange.WrapText = false;
                    }
                    if (CurrentOutput.SignificanceTest)
                    {
                        if (SigTestFormatSheet != null)
                        {
                            Range WithSigTestFormatSheetRange = SigTestFormatSheet.Range["MergedCells"];
                            WithSigTestFormatSheetRange.UnMerge();
                            WithSigTestFormatSheetRange.WrapText = false;

                        }
                    }
                }
            }
            if (MaxAxesCount == 1)
            {
                if (IsPortrait)
                {
                    DeleteName(FormatSheet, SigTestFormatSheet, "TripleRows", XlDeleteShiftDirection.xlShiftUp);
                    if (ExtendRowHeight)
                    {
                        Range WithFormatSheetRange = FormatSheet.Range["DoubleRows"];
                        WithFormatSheetRange.RowHeight = WithFormatSheetRange.RowHeight * 2;
                    }
                }
                else
                {
                    DeleteName(FormatSheet, SigTestFormatSheet, "TripleColumn", XlDeleteShiftDirection.xlShiftToLeft);
                }
            }
            if (OnlyCutTriple) { return; }
            //' フォーマットシートのWB前全体列またはWB前全体行の削除
            if (!CurrentOutput.ShowPreWBTotal)
            {
                if (IsPortrait)
                {
                    DeleteName(FormatSheet, SigTestFormatSheet, "PreWBRows", XlDeleteShiftDirection.xlShiftUp);
                }
                else
                {
                    DeleteName(FormatSheet, SigTestFormatSheet, "PreWBColumn", XlDeleteShiftDirection.xlShiftToLeft);
                }
            }
            // ' 不要な無回答/非該当行列の削除
            if (CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Multi)
            {
                tmpNamesArray = "SA_MA_NP SA_MA_N SA_MA_P SA_MA_NP_WT SA_MA_N_WT SA_MA_P_WT N".Split();
            }
            else
            {
                tmpNamesArray = "SA_MA SA_MA_WT N".Split();
            }
            RedrawBorder = !CurrentOutput.ShowIVAtItem;
            if (IsPortrait)
            {
                if (!CurrentOutput.ShowIVAtItem)
                {
                    tmpSuffix = "_InvalidRow";
                    for (i = 0; i <= tmpNamesArray.GetUpperBound(0); i++)
                    {
                        DeleteName(FormatSheet, SigTestFormatSheet, tmpNamesArray[i] + tmpSuffix, XlDeleteShiftDirection.xlShiftUp
                              , RedrawBorder & !tmpNamesArray[i].EndsWith("_WT"));
                    }
                }
                if (!CurrentOutput.ShowNAAtItem)
                {
                    tmpSuffix = "_NoAnswerRow";
                    for (i = 0; i <= tmpNamesArray.GetUpperBound(0); i++)
                    {
                        DeleteName(FormatSheet, SigTestFormatSheet, tmpNamesArray[i] + tmpSuffix, XlDeleteShiftDirection.xlShiftUp
                             , RedrawBorder & !tmpNamesArray[i].EndsWith("_WT") && !tmpNamesArray[i].StartsWith("SA_"));
                    }
                }
                else
                {
                    RedrawBorder = false;
                }
            }
            else
            {
                if (!CurrentOutput.ShowIVAtItem)
                {
                    tmpSuffix = "_InvalidColumn";
                    for (i = 0; i <= tmpNamesArray.GetUpperBound(0); i++)
                    {
                        DeleteName(FormatSheet, SigTestFormatSheet, tmpNamesArray[i] + tmpSuffix, XlDeleteShiftDirection.xlShiftToLeft
                            , RedrawBorder & !tmpNamesArray[i].EndsWith("_WT"));
                    }
                }
                if (!CurrentOutput.ShowNAAtItem)
                {
                    tmpSuffix = "_NoAnswerColumn";
                    for (i = 0; i <= tmpNamesArray.GetUpperBound(0); i++)
                    {
                        DeleteName(FormatSheet, SigTestFormatSheet, tmpNamesArray[i] + tmpSuffix, XlDeleteShiftDirection.xlShiftToLeft
                               , RedrawBorder & !tmpNamesArray[i].EndsWith("_WT") && !tmpNamesArray[i].StartsWith("SA_"));
                    }
                }
                else
                {
                    RedrawBorder = false;
                }
            }
            //' ウエイト値、加重平均のセルの書式設定
            if (HasWeightColumn)
            {
                tmp = CurrentOutput.ParentRequest.WeightNumDigitsAfterDecimal;
                if (CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Single)
                {
                    tmpNamesArray = "SA_MA".Split();
                }
                else
                {
                    tmpNamesArray = "SA_MA_NP SA_MA_N SA_MA_P".Split();
                }
                Suffix = "_WT_Weight" + (IsPortrait ? "Column" : "Row");
                NumberFormat(FormatSheet, SigTestFormatSheet, tmpNamesArray, tmp, Suffix, true);
                tmp = CurrentOutput.ParentRequest.WeightAverageNumDigitsAfterDecimal;
                Suffix = "_WT_WeightAverage";
                NumberFormat(FormatSheet, SigTestFormatSheet, tmpNamesArray, tmp, Suffix);
            }
            else if ((CurrentOutput.ParentReportset.FileType & FileType.Report) == 0)
            {
                //' 縦％表の場合はウエイト列削除
                if (IsPortrait)
                {
                    DeleteName(FormatSheet, SigTestFormatSheet, "WeightColumn", XlDeleteShiftDirection.xlShiftToLeft);
                }
            }
            // ' フォーマットシートの数値回答集計表フォーマットの調整
            tmpSuffix = IsPortrait ? "Row" : "Column";
            tmpDelShift = IsPortrait ? XlDeleteShiftDirection.xlShiftUp : XlDeleteShiftDirection.xlShiftToLeft;
            tmpNamesArray = "N".Split();
            if (CurrentOutput.ParentRequest.ShowMedian)
            {
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Median);
                Suffix = "_Median";
                NumberFormat(FormatSheet, SigTestFormatSheet, tmpNamesArray, tmp, Suffix);
                RedrawBorder = false;
            }
            else
            {
                tmpName = "N_Median" + tmpSuffix;
                DeleteName(FormatSheet, SigTestFormatSheet, tmpName, tmpDelShift, RedrawBorder);
            }
            if (CurrentOutput.ParentRequest.ShowMaximum)
            {
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Maximum);
                Suffix = "_Maximum";
                NumberFormat(FormatSheet, SigTestFormatSheet, tmpNamesArray, tmp, Suffix);
                RedrawBorder = false;
            }
            else
            {
                tmpName = "N_Maximum" + tmpSuffix;
                DeleteName(FormatSheet, SigTestFormatSheet, tmpName, tmpDelShift, RedrawBorder);
            }
            if (CurrentOutput.ParentRequest.ShowMinimum)
            {
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Minimum);
                Suffix = "_Minimum";
                NumberFormat(FormatSheet, SigTestFormatSheet, tmpNamesArray, tmp, Suffix);
                RedrawBorder = false;
            }
            else
            {
                tmpName = "N_Minimum" + tmpSuffix;
                DeleteName(FormatSheet, SigTestFormatSheet, tmpName, tmpDelShift, RedrawBorder);
            }
            if (CurrentOutput.ParentRequest.ShowStdev)
            {
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Stdev);
                Suffix = "_Deviation";
                NumberFormat(FormatSheet, SigTestFormatSheet, tmpNamesArray, tmp, Suffix);
                RedrawBorder = false;
            }
            else
            {
                tmpName = "N_Deviation" + tmpSuffix;
                DeleteName(FormatSheet, SigTestFormatSheet, tmpName, tmpDelShift, RedrawBorder);
            }
            if (CurrentOutput.ParentRequest.ShowAverage)
            {
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Average);
                Suffix = "_Average";
                NumberFormat(FormatSheet, SigTestFormatSheet, tmpNamesArray, tmp, Suffix);
                RedrawBorder = false;
            }
            else
            {
                tmpName = "N_Average" + tmpSuffix;
                DeleteName(FormatSheet, SigTestFormatSheet, tmpName, tmpDelShift, RedrawBorder);
            }
            if (CurrentOutput.ParentRequest.ShowSummary)
            {
                tmp = CurrentOutput.ParentRequest.NumDigitsAfterDecimal(NumericContentsCode.Summary);
                Suffix = "_Summary";
                NumberFormat(FormatSheet, SigTestFormatSheet, tmpNamesArray, tmp, Suffix);
                RedrawBorder = false;
            }
            else
            {
                tmpName = "N_Summary" + tmpSuffix;
                DeleteName(FormatSheet, SigTestFormatSheet, tmpName, tmpDelShift, RedrawBorder);
            }
            if (!CurrentOutput.ParentRequest.ShowParameter)
            {
                tmpName = "N_Population" + tmpSuffix;
                DeleteName(FormatSheet, SigTestFormatSheet, tmpName, tmpDelShift, RedrawBorder);
            }
        }

        private static void DeleteName(
                Worksheet FormatSheet, Worksheet SigTestFormatSheet
              , string DeleteName, XlDeleteShiftDirection DelShift
              , bool RedrawBorder = false)
        {
            Range tmpRange = null;
            if (FormatSheet != null)
            {
                Name WithFormatSheetName = FormatSheet.Names.Item(DeleteName);
                if (RedrawBorder)
                {
                    Range WithWithFormatSheetNameRefersToRange = WithFormatSheetName.RefersToRange;
                    if (DelShift == XlDeleteShiftDirection.xlShiftUp)
                    {
                        tmpRange = WithWithFormatSheetNameRefersToRange.Rows.Item[0];
                    }
                    else
                    {
                        tmpRange = WithWithFormatSheetNameRefersToRange.Columns.Item[0];
                    }
                }
                if (DelShift == XlDeleteShiftDirection.xlShiftUp)
                {
                    WithFormatSheetName.RefersToRange.EntireRow.Delete(DelShift);
                }
                else
                {
                    WithFormatSheetName.RefersToRange.Delete(DelShift);
                }
                WithFormatSheetName.Delete();
                if (RedrawBorder)
                {
                    Border WithtmpRangeBorder = tmpRange.Borders.Item[DelShift == XlDeleteShiftDirection.xlShiftUp ? XlBordersIndex.xlEdgeBottom : XlBordersIndex.xlEdgeRight];
                    WithtmpRangeBorder.LineStyle = XlLineStyle.xlContinuous;
                    WithtmpRangeBorder.Weight = XlBorderWeight.xlThin;
                }
            }
            if (CurrentOutput.SignificanceTest)
            {
                if (SigTestFormatSheet != null)
                {
                    Name WithSigTestFormatSheetName = SigTestFormatSheet.Names.Item(DeleteName);
                    if (RedrawBorder)
                    {
                        Range WithWithSigTestFormatSheetName = WithSigTestFormatSheetName.RefersToRange;
                        if (DelShift == XlDeleteShiftDirection.xlShiftUp)
                        {
                            tmpRange = WithWithSigTestFormatSheetName.Rows.Item[0];
                        }
                        else
                        {
                            tmpRange = WithWithSigTestFormatSheetName.Columns.Item[0];
                        }
                    }
                    if (DelShift == XlDeleteShiftDirection.xlShiftUp)
                    {
                        WithSigTestFormatSheetName.RefersToRange.EntireRow.Delete(DelShift);
                    }
                    else
                    {
                        WithSigTestFormatSheetName.RefersToRange.Delete(DelShift);
                    }
                    WithSigTestFormatSheetName.Delete();
                    if (RedrawBorder)
                    {
                        Border WithtmpRangeBorder = tmpRange.Borders.Item[DelShift == XlDeleteShiftDirection.xlShiftUp ? XlBordersIndex.xlEdgeBottom : XlBordersIndex.xlEdgeRight];

                        WithtmpRangeBorder.LineStyle = XlLineStyle.xlContinuous;
                        WithtmpRangeBorder.Weight = XlBorderWeight.xlThin;
                    }
                }
            }
        }

        private static void NumberFormat(
                Worksheet FormatSheet, Worksheet SigTestFormatSheet
              , string[] NamesArray, int NumDigitsAfterDecimal
              , string Suffix = null, bool IsWeight = false)
        {
            string fmt;
            string n;
            if (NumDigitsAfterDecimal == 0)
            {
                fmt = "0";
            }
            else if (NumDigitsAfterDecimal >= 1 || NumDigitsAfterDecimal <= 5)
            {
                fmt = "0." + new String('0', NumDigitsAfterDecimal);
            }
            else
            {
                fmt = "0.0";
            }
            if (IsWeight)
            {
                if (FormatSheet != null)
                {
                    fmt = FormatSheet.Range[NamesArray[0] + Suffix].Cells.Item[1].NumberFormat.Replace("0.0", fmt);
                }
                else if (SigTestFormatSheet != null)
                {
                    fmt = SigTestFormatSheet.Range[NamesArray[0] + Suffix].Cells.Item[1].NumberFormat.Replace("0.0", fmt);
                }
            }
            foreach (string tmpName in NamesArray)
            {
                n = tmpName + Suffix;
                if (FormatSheet != null)
                {
                    FormatSheet.Range[n].NumberFormat = fmt;
                }
                if (CurrentOutput.SignificanceTest)
                {
                    if (SigTestFormatSheet != null)
                    {
                        SigTestFormatSheet.Range[n].NumberFormat = fmt;
                    }
                }
            }
        }

        public static void GetCutRowsAndColumns(CrossTable Table
              , bool HasWeightBack, bool HasWeight, int MaxAxesCount
              , ref Hashtable CutRowsCol, ref Hashtable CutColumnsCol, int MedIdx = -1 // ref int MedIdx   = -1
              , bool IsReport = false
              , bool CutMedian = false
              , Hashtable WholeRowCol = null)
        {
            bool DefHasNAAtItem;
            bool DefHasIVAtItem;
            bool DefHasNAAtAxis;
            bool DefHasIVAtAxis;
            int tmpIdx;
            int PreSummaryRowIndex;
            int i, j;
            int x;
            int r;
            int PreWBColumnIndex;
            int LastColumnIndex;
            bool CheckZero;
            bool CutNA;
            bool CutIV;

            CutRowsCol = new Hashtable();
            CutColumnsCol = new Hashtable();
            DefHasNAAtItem = CurrentOutput.ShowNAAtItem;
            DefHasIVAtItem = CurrentOutput.ShowIVAtItem;
            DefHasNAAtAxis = CurrentOutput.ShowNAAtAxis;
            DefHasIVAtAxis = CurrentOutput.ShowIVAtAxis;

            PreWBColumnIndex = Table.GetTableValueColumnIndexMinimum + MaxAxesCount + 1;
            LastColumnIndex = Table.GetTableValueColumnIndexMaximum;
            CheckZero = !Table.ParentRequest.ShowZeroNAIV;
            if (DefHasNAAtItem)
            {
                if (CheckZero)
                {
                    tmpIdx = LastColumnIndex - (ToInt(HasWeight) & 2) - (ToInt(DefHasIVAtItem) & 1);
                    CutNA = Convert.ToDouble(Table.TableValue(1 + (ToInt(HasWeight) & 1), tmpIdx)) == 0;
                    if (CutNA) { CutColumnsCol.Add(tmpIdx, tmpIdx); }
                }
            }
            if (DefHasIVAtItem)
            {
                if (CheckZero)
                {
                    tmpIdx = LastColumnIndex - (ToInt(HasWeight) & 2);
                    CutIV = Convert.ToDouble(Table.TableValue(1 + (ToInt(HasWeight) & 1), tmpIdx)) == 0;
                    if (CutIV) { CutColumnsCol.Add(tmpIdx, tmpIdx); }
                }
            }
            if (IsReport)
            {
                MedIdx = Table.GetTableValueColumnIndexMinimum - 1;
                if ((Table.Question.QuestionType & QuestionType.N) == QuestionType.N)
                {
                    if (CutMedian)
                    {
                        MedIdx = LastColumnIndex - (ToInt(DefHasIVAtItem) & 1) - (ToInt(DefHasNAAtItem) & 1);
                        CutColumnsCol.Add(MedIdx, MedIdx);
                    }
                }
            }

            r = (ToInt(HasWeight) & 1) + 1;
            if (WholeRowCol != null) { WholeRowCol.Add(r, r); }
            PreSummaryRowIndex = r;
            for (i = 1; i <= Table.AxesGroups.Count; i++)
            {
                AxesInformation WithTableAxesGroup = Table.AxesGroups[i - 1];
                if (WithTableAxesGroup.Count == 1)
                { //' 二重クロス
                    r = r + 1;  //' 小計行
                    if (WholeRowCol != null) { WholeRowCol.Add(r, r); }
                    for (j = PreWBColumnIndex; j <= LastColumnIndex; j++)
                    {
                        if (Table.TableValue(r, j) != Table.TableValue(PreSummaryRowIndex, j))
                        {
                            PreSummaryRowIndex = r;
                            break;
                        }
                    }
                    if (j > LastColumnIndex) { CutRowsCol.Add(r, r); }
                    r = r + WithTableAxesGroup[0].SectorsCount;
                    if (DefHasNAAtAxis)
                    {
                        r = r + 1;
                        if (CheckZero)
                        {
                            CutNA = Convert.ToDouble(Table.TableValue(r, PreWBColumnIndex)) == 0;
                            if (CutNA) { CutRowsCol.Add(r, r); }
                        }
                    }
                    if (DefHasIVAtAxis)
                    {
                        r = r + 1;
                        if (CheckZero)
                        {
                            CutIV = Convert.ToDouble(Table.TableValue(r, PreWBColumnIndex)) == 0;
                            if (CutIV) { CutRowsCol.Add(r, r); }
                        }
                    }
                }
                else
                {    //' 三重クロス
                    for (x = 1; x <= WithTableAxesGroup[0].SectorsCount; x++)
                    {
                        r = r + 1;  //' 二重クロスの小計行
                        if (WholeRowCol != null) { WholeRowCol.Add(r, r); }
                        r = r + WithTableAxesGroup[1].SectorsCount;
                        if (DefHasNAAtAxis)
                        {
                            r = r + 1;
                            if (CheckZero)
                            {
                                CutNA = Convert.ToDouble(Table.TableValue(r, PreWBColumnIndex)) == 0;
                                if (CutNA) { CutRowsCol.Add(r, r); }
                            }
                        }
                        if (DefHasIVAtAxis)
                        {
                            r = r + 1;
                            if (CheckZero)
                            {
                                CutIV = Convert.ToDouble(Table.TableValue(r, PreWBColumnIndex)) == 0;
                                if (CutIV) { CutRowsCol.Add(r, r); }
                            }
                        }
                    }
                    if (DefHasNAAtAxis)
                    {
                        r = r + 1;
                        if (WholeRowCol != null) { WholeRowCol.Add(r, r); }
                        if (CheckZero)
                        {
                            CutNA = Convert.ToDouble(Table.TableValue(r, PreWBColumnIndex)) == 0;
                            if (CutNA)
                            {
                                CutRowsCol.Add(r, r);
                                for (x = 1; x <= WithTableAxesGroup[1].SectorsCount; x++)
                                {
                                    r = r + 1;
                                    CutRowsCol.Add(r, r);
                                }
                                r = r + 1;
                                CutRowsCol.Add(r, r);
                                if (DefHasIVAtAxis)
                                {
                                    r = r + 1;
                                    CutRowsCol.Add(r, r);
                                }
                            }
                            else
                            {
                                r = r + WithTableAxesGroup[1].SectorsCount + 1;
                                CutNA = Convert.ToDouble(Table.TableValue(r, PreWBColumnIndex)) == 0;
                                if (CutNA) { CutRowsCol.Add(r, r); }
                                if (DefHasIVAtAxis)
                                {
                                    r = r + 1;
                                    CutIV = Convert.ToDouble(Table.TableValue(r, PreWBColumnIndex)) == 0;
                                    if (CutIV) { CutRowsCol.Add(r, r); }
                                }
                            }
                        }
                        else
                        {
                            r = r + WithTableAxesGroup[1].SectorsCount + 1 & +(ToInt(DefHasIVAtAxis) & 1);
                        }
                    }
                    if (DefHasIVAtAxis)
                    {
                        r = r + 1;
                        if (WholeRowCol != null) { WholeRowCol.Add(r, r); }
                        if (CheckZero)
                        {
                            CutIV = Convert.ToDouble(Table.TableValue(r, PreWBColumnIndex)) == 0;
                            if (CutIV)
                            {
                                CutRowsCol.Add(r, r);
                                for (x = 1; x <= WithTableAxesGroup[1].SectorsCount; x++)
                                {
                                    r = r + 1;
                                    CutRowsCol.Add(r, r);
                                }
                                if (DefHasIVAtAxis)
                                {
                                    r = r + 1;
                                    CutRowsCol.Add(r, r);
                                }
                                r = r + 1;
                                CutRowsCol.Add(r, r);
                            }
                            else
                            {
                                r = r + WithTableAxesGroup[1].SectorsCount;
                                if (DefHasNAAtAxis)
                                {
                                    r = r + 1;
                                    CutIV = Convert.ToDouble(Table.TableValue(r, PreWBColumnIndex)) == 0;
                                    if (CutIV) { CutRowsCol.Add(r, r); }
                                }
                                r = r + 1;
                                CutIV = Convert.ToDouble(Table.TableValue(r, PreWBColumnIndex)) == 0;
                                if (CutIV) { CutRowsCol.Add(r, r); }
                            }
                        }
                        else
                        {
                            r = r + WithTableAxesGroup[1].SectorsCount + (ToInt(DefHasNAAtAxis) & 1) + 1;
                        }
                    }
                }
            }
        }


        private static void FormatTurnedLandscapeTable(CrossTable Table
              , Hashtable CutRowsCol, Hashtable CutColumnsCol
              , Worksheet FormatSheet, string FormatRangeNamePrefix
              , TableType TableType, bool HasWeight
              , int ColumnsCountPerPage, int PagesCount
              , Range StartCell, bool isN, int AxesCount
              , Hashtable WholeRowCol = null)
        {
            List<Range> tmpCol = null;
            Range FormattedRange;


            FormatTurnedLandscapeTableSub(Table, CutRowsCol, CutColumnsCol, FormatSheet, FormatRangeNamePrefix, TableType, HasWeight, isN
                                        , AxesCount, ColumnsCountPerPage, PagesCount, ref tmpCol, 0, WholeRowCol);
            FormattedRange = WorkingSheet.Range[tmpCol[0], tmpCol[tmpCol.Count - 1]];
            OutputUtil.CopyRow(FormattedRange, StartCell);
        }


        private static void FormatTurnedLandscapeTableSub(CrossTable Table
              , Hashtable CutRowsCol, Hashtable CutColumnsCol
              , Worksheet FormatSheet, string FormatRangeNamePrefix
              , TableType TableType, bool HasWeight
              , bool isN, int AxesCount
              , int ColumnsCountPerPage, int PagesCount
              , ref List<Range> PartsFormatRangeCol
              , double HeaderRowHeight = 0
              , Hashtable WholeRowCol = null)
        {
            Worksheet SubWorkingSheet;
            bool RedrawBorder;
            bool HasNAColumn;
            bool HasIVColumn;
            bool HasNARow;
            bool HasIVRow;
            int d, d2;
            bool CutNAColumn = false;
            bool CutIVColumn = false;
            int ItemSectorsCount = 0;
            int[] SectorsCount = new int[2];
            int rs, cs;
            int rc, cc;
            int SectorsCountPerPage;
            Range tmpRange;
            Range TableHeaderRange;
            Range TableRange;
            Range BodyRange; ; ;
            Range PagingRange;
            Range SectorColumns = null;
            Range NAColumn = null;
            Range IVColumn = null;
            Range WTColumns = null;
            bool CutNA = false;
            bool CutIV = false;
            int r, c;
            int y, x;
            int n;
            int i;
            int tmp;
            int tmpR;
            int RemainedSectorsCount;
            bool RemainedNA;
            bool RemainedIV;
            bool RemainedWTPopulation;
            bool RemainedWTAverage;
            int tmpCount;
            bool IsSigTest;
            TableType tType;

            IsSigTest = (TableType & TableType.SignificanceTest) == TableType.SignificanceTest;
            tType = TableType & ~TableType.SignificanceTest;
            if (IsSigTest) { tType = TableType.Per; }
            SubWorkingSheet = WorkingSheet.Parent.Worksheets.Item(2);
            PartsFormatRangeCol = new List<Range>();
            WorkingSheet.UsedRange.Clear();
            WorkingSheet.DrawingObjects().Delete();
            SubWorkingSheet.UsedRange.Clear();
            HasNAColumn = CurrentOutput.ShowNAAtItem;
            HasIVColumn = CurrentOutput.ShowIVAtItem;
            HasNARow = CurrentOutput.ShowNAAtAxis;
            HasIVRow = CurrentOutput.ShowIVAtAxis;
            d = 1 + (ToInt(!isN & tType == TableType.NPer) & 1);
            d2 = d + (ToInt(!isN & IsSigTest) & 1);

            if (isN)
            {
                if (HasNAColumn)
                {
                    CutNAColumn = CutColumnsCol.ContainsKey(Table.GetTableValueColumnIndexMaximum - (ToInt(HasIVColumn) & 1));
                }
                if (HasIVColumn)
                {
                    CutIVColumn = CutColumnsCol.ContainsKey(Table.GetTableValueColumnIndexMaximum);
                }
            }
            else
            {
                ItemSectorsCount = Table.SectorsCount;
                if (HasNAColumn)
                {
                    CutNAColumn = CutColumnsCol.ContainsKey(Table.GetTableValueColumnIndexMaximum - (ToInt(HasWeight) & 2) - (ToInt(HasIVColumn) & 1));
                }
                if (HasIVColumn)
                {
                    CutIVColumn = CutColumnsCol.ContainsKey(Table.GetTableValueColumnIndexMaximum - (ToInt(HasWeight) & 2));
                }
                if (!HasWeight)
                {
                    if (!HasIVColumn || CutIVColumn)
                    {
                        if (!HasNAColumn || CutNAColumn)
                        {
                            RedrawBorder = true;
                        }
                    }
                }
            }
            tmpRange = FormatSheet.Range[FormatRangeNamePrefix + "_Header"];
            OutputUtil.CopyRow(tmpRange, SubWorkingSheet.Range["A1"]);
            TableHeaderRange = FormatSheet.Application.Intersect(tmpRange, FormatSheet.Range[FormatRangeNamePrefix + "_Table"]);
            rs = TableHeaderRange.Row - tmpRange.Row + 1;
            cs = TableHeaderRange.Column;
            rc = TableHeaderRange.Rows.Count;
            cc = TableHeaderRange.Columns.Count;
            TableHeaderRange = SubWorkingSheet.Cells.Item[rs, cs].Resize(rc, cc);
            if (HeaderRowHeight > 0)
            {
                TableHeaderRange.EntireRow.Item[1].RowHeight = HeaderRowHeight;
            }
            PagingRange = FormatSheet.Application.Intersect(tmpRange, FormatSheet.Range[FormatRangeNamePrefix + "_PagingArea"]);
            cs = PagingRange.Column;
            cc = PagingRange.Columns.Count;
            PagingRange = SubWorkingSheet.Cells.Item[rs, cs].Resize(rc, cc);
            if (isN)
            {
                if (CutIVColumn)
                {
                    IVColumn = FormatSheet.Application.Intersect(tmpRange, FormatSheet.Range[FormatRangeNamePrefix + "_InvalidColumn"]);
                    IVColumn = SubWorkingSheet.Cells.Item[rs, IVColumn.Column].Resize(rc);
                    IVColumn.Delete(XlDeleteShiftDirection.xlShiftToLeft);
                    IVColumn = null;
                }
                if (CutNAColumn)
                {
                    NAColumn = FormatSheet.Application.Intersect(tmpRange, FormatSheet.Range[FormatRangeNamePrefix + "_NoAnswerColumn"]);
                    NAColumn = SubWorkingSheet.Cells.Item[rs, NAColumn.Column].Resize(rc);
                    NAColumn.Delete(XlDeleteShiftDirection.xlShiftToLeft);
                    NAColumn = null;
                }
            }
            else
            {
                SectorColumns = FormatSheet.Application.Intersect(tmpRange, FormatSheet.Range[FormatRangeNamePrefix + "_SectorColumns"]);
                cs = SectorColumns.Column;
                cc = SectorColumns.Columns.Count;
                SectorColumns = SubWorkingSheet.Cells.Item[rs, cs].Resize(rc, cc);
            }
            if (!HasNAColumn || CutNAColumn)
            {
            }
            else
            {
                NAColumn = FormatSheet.Application.Intersect(tmpRange, FormatSheet.Range[FormatRangeNamePrefix + "_NoAnswerColumn"]);
                NAColumn = SubWorkingSheet.Cells.Item[rs, NAColumn.Column].Resize(rc);
            }
            if (!HasIVColumn || CutIVColumn)
            {
            }
            else
            {
                IVColumn = FormatSheet.Application.Intersect(tmpRange, FormatSheet.Range[FormatRangeNamePrefix + "_InvalidColumn"]);
                IVColumn = SubWorkingSheet.Cells.Item[rs, IVColumn.Column].Resize(rc);
            }
            if (HasWeight)
            {
                WTColumns = FormatSheet.Range[FormatSheet.Range[FormatRangeNamePrefix + "_PopulationColumn"],
                    FormatSheet.Range[FormatRangeNamePrefix + "_WeightAverageColumn"]];
                WTColumns = FormatSheet.Application.Intersect(tmpRange, WTColumns);
                cs = WTColumns.Column;
                cc = WTColumns.Columns.Count;
                WTColumns = SubWorkingSheet.Cells.Item[rs, cs].Resize(rc, cc);
            }
            Range WithFormatSheetRange = FormatSheet.Range[FormatRangeNamePrefix + (AxesCount == 1 ? "_Double" : "_Triple")];
            OutputUtil.CopyRow(WithFormatSheetRange.Rows, TableHeaderRange.Item[TableHeaderRange.Rows.Count + 1, 1]);
            rc = WithFormatSheetRange.Rows.Count;
            if (isN)
            {
                if (CutIVColumn)
                {
                    IVColumn = WithFormatSheetRange.Application.Intersect(tmpRange, FormatSheet.Range[FormatRangeNamePrefix + "_InvalidColumn"]);
                    IVColumn = TableHeaderRange.EntireRow.Cells.Item[TableHeaderRange.Rows.Count + 1, IVColumn.Column].Resize(rc);
                    IVColumn.Delete(XlDeleteShiftDirection.xlShiftToLeft);
                    IVColumn = null;
                }
                if (CutNAColumn)
                {
                    NAColumn = WithFormatSheetRange.Application.Intersect(tmpRange, FormatSheet.Range[FormatRangeNamePrefix + "_NoAnswerColumn"]);
                    NAColumn = TableHeaderRange.EntireRow.Cells.Item[TableHeaderRange.Rows.Count + 1, NAColumn.Column].Resize(rc);
                    NAColumn.Delete(XlDeleteShiftDirection.xlShiftToLeft);
                    NAColumn = null;
                }
            }
            TableRange = TableHeaderRange.Resize[TableHeaderRange.Rows.Count + rc];
            BodyRange = TableHeaderRange.Rows.Item[TableHeaderRange.Rows.Count + 1].Resize(rc).Cells;
            PagingRange = PagingRange.Resize[PagingRange.Rows.Count + rc];
            if (isN)
            {
                if (!IsSigTest)
                {
                    if (CutIVColumn || !(HasIVColumn & CutNAColumn))
                    {
                        PagingRange.Borders.Item[XlBordersIndex.xlEdgeRight].Weight = XlBorderWeight.xlThin;
                    }
                }
            }
            if (SectorColumns != null)
            {
                SectorColumns = SectorColumns.Resize[SectorColumns.Rows.Count + rc];
            }
            if (NAColumn != null)
            {
                NAColumn = NAColumn.Resize[NAColumn.Rows.Count + rc];
            }
            if (IVColumn != null)
            {
                IVColumn = IVColumn.Resize[IVColumn.Rows.Count + rc];
            }
            if (WTColumns != null)
            {
                WTColumns = WTColumns.Resize[WTColumns.Rows.Count + rc];
            }
            y = 1 + (ToInt(HasWeight) & 1); // ' ヘッダの下端インデックス
            if (AxesCount == 1)
            {  //' 二重クロス
                y = y + 1;  //' 小計行
                r = 1;
                if (CutRowsCol.ContainsKey(y))
                {
                    BodyRange.Rows.Item[r].Resize(d).EntireRow.Delete(XlDeleteShiftDirection.xlShiftUp);
                }
                else
                {
                    r = r + d;
                }
                r = r + d2;//  ' 2つ目の選択肢行
                AxesInformation tmpAxGrp = Table.AxesGroups[0];
                SectorsCount[0] = tmpAxGrp[0].SectorsCount;
                y = y + SectorsCount[0];
                if (HasNARow)
                {
                    y = y + 1;
                    if (!(CutRowsCol.ContainsKey(y)))
                    {  // ' 無回答出力
                        SectorsCount[0] = SectorsCount[0] + 1;
                    }
                }
                if (HasIVRow)
                {
                    y = y + 1;
                    if (!(CutRowsCol.ContainsKey(y)))
                    {   //' 非該当出力
                        SectorsCount[0] = SectorsCount[0] + 1;
                    }
                }
                if (SectorsCount[0] > 3)
                {
                    Range WithBodyRangeRows = BodyRange.Rows.Item[r].Resize(d2).EntireRow();
                    WithBodyRangeRows.Copy();
                    WithBodyRangeRows.Resize[(SectorsCount[0] - 3) * d2].Insert(XlInsertShiftDirection.xlShiftDown);
                    WithBodyRangeRows.Application.CutCopyMode = (XlCutCopyMode)1;
                }
                else if (SectorsCount[0] < 3)
                {
                    BodyRange.Rows.Item[r].Resize[(3 - SectorsCount[0]) * d2].EntireRow.Delete(XlDeleteShiftDirection.xlShiftUp);
                    if (SectorsCount[0] < 2)
                    {
                        Border WithBodyRange = BodyRange.Borders.Item[XlBordersIndex.xlEdgeBottom];
                        WithBodyRange.LineStyle = XlLineStyle.xlContinuous;
                        WithBodyRange.Weight = XlBorderWeight.xlThin;
                    }
                }
            }
            else
            {   // ' 三重クロス
                tmpRange = BodyRange.Rows.Item[d + 3 * d2 + 1].Resize(d + 3 * d2);
                AxesInformation tmpAxGrp1 = Table.AxesGroups[0];
                SectorsCount[0] = tmpAxGrp1[0].SectorsCount;
                SectorsCount[1] = tmpAxGrp1[1].SectorsCount;
                n = SectorsCount[1] + (ToInt(HasNARow) & 1) + (ToInt(HasIVRow) & 1) + 1;
                y = y + SectorsCount[0] * n;
                if (HasNARow)
                {
                    if (!(CutRowsCol.ContainsKey(y + 1)))
                    { //' 無回答出力
                        SectorsCount[0] = SectorsCount[0] + 1;
                    }
                    else
                    {
                        CutNA = true;
                    }
                    y = y + n;
                }
                if (HasIVRow)
                {
                    if (!(CutRowsCol.ContainsKey(y + 1)))
                    {//  ' 非該当出力
                        SectorsCount[0] = SectorsCount[0] + 1;
                    }
                    else
                    {
                        CutIV = true;
                    }
                    y = y + n;
                }
                if (SectorsCount[0] < 3)
                {
                    tmpRange.Resize[tmpRange.Rows.Count * (3  -SectorsCount[0])].EntireRow.Delete(XlDeleteShiftDirection.xlShiftUp);
                }
                SectorsCount[1] = n - 1;
                if (SectorsCount[1] > 3)
                {
                    i = 3;
                    for (r = 3 * d + 7 * d2 + 1; r <= d + d2 + 1; r = r + -(d + 3 * d2))
                    {
                        if (i <= SectorsCount[0])
                        {
                            Range WithBodyRange = BodyRange.Rows.Item[r].Resize(d2).EntireRow;
                            WithBodyRange.Copy();
                            WithBodyRange.Resize[(SectorsCount[1] - 3) * d2].EntireRow.Insert(XlInsertShiftDirection.xlShiftDown);
                            WithBodyRange.Application.CutCopyMode = (XlCutCopyMode)1;
                        }
                        i = i - 1;
                    }
                }
                else if (SectorsCount[1] < 3)
                {
                    i = 3;
                    for (r = 3 * d + 7 * d2 + 1; r <= d + d2 + 1; r = r + -(d + 3 * d2))
                    {
                        if (i <= SectorsCount[0])
                        {
                            BodyRange.Rows.Item[r].Resize((3 & -SectorsCount[1]) * d2).EntireRow.Delete(XlDeleteShiftDirection.xlShiftUp);
                        }
                        i = i - 1;
                    }
                }
                if (SectorsCount[0] > 3)
                {
                    Range WithtmpRange = tmpRange.EntireRow;
                    WithtmpRange.Copy();
                    WithtmpRange.Resize[WithtmpRange.Rows.Count * (SectorsCount[0] - 3)].Insert(XlInsertShiftDirection.xlShiftDown);
                    WithtmpRange.Application.CutCopyMode = (XlCutCopyMode)1;
                }
                r = SectorsCount[0] * (d + SectorsCount[1] * d2) + 1;
                for (i = y - (ToInt(CutIV) & n) - (ToInt(CutNA) & n); i <= 1 + (ToInt(HasWeight) & 1) + 1; i--)
                {
                    tmp = i;
                    if (CutNA & !CutIV)
                    {
                        if (i > y - n) { tmp = (i + n); }
                    }
                    if (IsSigTest)
                    {
                        r = r - (WholeRowCol.ContainsKey(tmp) ? d : d2);
                    }
                    else
                    {
                        r = r - d;
                    }
                    if (CutRowsCol.ContainsKey(tmp))
                    {
                        if (IsSigTest)
                        {
                            BodyRange.Rows.Item[r].Resize((WholeRowCol.ContainsKey(tmp) ? d : d2)).EntireRow.Delete(XlDeleteShiftDirection.xlShiftUp);
                        }
                        else
                        {
                            BodyRange.Rows.Item[r].Resize(d).EntireRow.Delete(XlDeleteShiftDirection.xlShiftUp);
                        }
                    }
                }
                Border WithBodyRangeBorders = BodyRange.Borders.Item[XlBordersIndex.xlEdgeBottom];
                WithBodyRangeBorders.LineStyle = XlLineStyle.xlContinuous;
                WithBodyRangeBorders.Weight = XlBorderWeight.xlThin;
            }
            SectorsCountPerPage = ColumnsCountPerPage - PagingRange.Column + 2;
            Range WithSubWorkingSheetRange = SubWorkingSheet.Range[SubWorkingSheet.Range["A1"], TableRange].EntireRow;
            WithSubWorkingSheetRange.Copy(WorkingSheet.Range["A1"]);
            rc = WithSubWorkingSheetRange.Rows.Count;
            cs = PagingRange.Column;
            cc = PagingRange.Columns.Count;
            WorkingSheet.Cells.Item[PagingRange.Row, cs].Resize(PagingRange.Rows.Count).Columns.Resize(Type.Missing, cc).Delete(XlDeleteShiftDirection.xlShiftToLeft);
            rs = TableRange.Row;
            rc = TableRange.Rows.Count;
            if (PagesCount > 1)
            {
                WorkingSheet.Rows.Item[rs + rc].Resize(2).EntireRow.RowHeight = 11.25; //' 15px
                WorkingSheet.Cells.Item[rs + rc, ColumnsCountPerPage].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlRight;
                WorkingSheet.Rows.Item[rs].Resize(rc).Copy(WorkingSheet.Rows.Item[rs + rc + 2]);
                if (PagesCount > 2)
                {
                    WorkingSheet.Rows.Item[rs + rc].Resize(2 + rc).Copy(WorkingSheet.Rows.Item[rs + rc + 2 + rc].Resize((PagesCount - 2) * (2 + rc)));
                }
            }
            tmpR = 1;
            if (isN)
            {
                for (i = 1; i <= PagesCount; i++)
                {
                    cs = (i - 1) * SectorsCountPerPage + 1;
                    if (i < PagesCount)
                    {
                        cc = SectorsCountPerPage;
                    }
                    else
                    {
                        cc = (PagingRange.Columns.Count - 1) % SectorsCountPerPage + 1;
                    }
                    tmpRange = WorkingSheet.Cells.Item[rs, PagingRange.Column].Resize(rc, cc);
                    PagingRange.Columns.Item[cs].Resize(Type.Missing, cc).Copy(tmpRange);
                    if (i < PagesCount)
                    {
                        Border WithtmpRangeBorders = tmpRange.Borders.Item[XlBordersIndex.xlEdgeRight];
                        WithtmpRangeBorders.LineStyle = XlLineStyle.xlContinuous;
                        WithtmpRangeBorders.Weight = XlBorderWeight.xlThin;
                    }

                    tmpRange = WorkingSheet.Range[WorkingSheet.Cells.Item[tmpR, 1], tmpRange];
                    if (i < PagesCount)
                    {
                        tmpRange = tmpRange.Resize[tmpRange.Rows.Count + 1];
                    }
                    tmpR = tmpR + tmpRange.Rows.Count;
                    PartsFormatRangeCol.Add(tmpRange);
                    rs = rs + rc + 2;
                }
            }
            else
            {
                RemainedSectorsCount = ItemSectorsCount;
                RemainedNA = NAColumn != null;
                RemainedIV = IVColumn != null;
                RemainedWTPopulation = HasWeight;
                RemainedWTAverage = HasWeight;
                for (i = 1; i <= PagesCount; i++)
                {
                    cs = PagingRange.Column;
                    tmpCount = SectorsCountPerPage;
                    cc = 0;
                    tmpRange = null;
                    if (RemainedSectorsCount > 0)
                    {
                        tmpRange = WorkingSheet.Cells.Item[rs, cs];
                        RemainedSectorsCount = RemainedSectorsCount - tmpCount;
                        if (RemainedSectorsCount >= 0)
                        {
                            cc = tmpCount;
                        }
                        else
                        {
                            cc = RemainedSectorsCount + tmpCount;
                            RemainedSectorsCount = 0;
                        }
                        tmpCount = tmpCount - cc;
                        tmpRange = tmpRange.Resize[rc, cc];
                        SectorColumns.Columns.Item[2].Copy(tmpRange);
                        cs = cs + cc;
                        if (RemainedSectorsCount > 0 || tmpCount <= 0 || !RemainedNA)
                        {
                            Border WithtmpRangeBorders = tmpRange.Borders.Item[XlBordersIndex.xlEdgeRight];
                            WithtmpRangeBorders.LineStyle = XlLineStyle.xlContinuous;
                            WithtmpRangeBorders.Weight = XlBorderWeight.xlThin;
                        }
                    }
                    if (tmpCount > 0)
                    {
                        if (RemainedNA)
                        {
                            tmpRange = WorkingSheet.Cells.Item[rs, cs];
                            cc = 1;
                            tmpCount = tmpCount - cc;
                            RemainedNA = false;
                            tmpRange = tmpRange.Resize[rc, cc];
                            cs = cs + cc;
                            NAColumn.Copy(tmpRange);
                        }
                    }
                    if (tmpCount > 0)
                    {
                        if (RemainedIV)
                        {
                            tmpRange = WorkingSheet.Cells.Item[rs, cs];
                            cc = 1;
                            tmpCount = tmpCount - cc;
                            RemainedIV = false;
                            tmpRange = tmpRange.Resize[rc, cc];
                            cs = cs + cc;
                            IVColumn.Copy(tmpRange);
                        }
                    }
                    if (tmpCount > 0)
                    {
                        if (RemainedWTPopulation)
                        {
                            tmpRange = WorkingSheet.Cells.Item[rs, cs];
                            if (tmpCount >= 2)
                            {
                                cc = 2;
                                RemainedWTAverage = false;
                            }
                            else
                            {
                                cc = 1;
                            }
                            tmpCount = tmpCount - cc;
                            RemainedWTPopulation = false;
                            tmpRange = tmpRange.Resize[rc, cc];
                            WTColumns.Resize[Type.Missing, cc].Copy(tmpRange);
                            if (cc == 1)
                            {
                                Border WithtmpRangeBorders = tmpRange.Borders.Item[XlBordersIndex.xlEdgeRight];
                                WithtmpRangeBorders.LineStyle = XlLineStyle.xlContinuous;
                                WithtmpRangeBorders.Weight = XlBorderWeight.xlThin;
                            }
                        }
                        else if (RemainedWTAverage)
                        {
                            tmpRange = WorkingSheet.Cells.Item[rs, cs];
                            cc = 1;
                            RemainedWTAverage = false;
                            tmpRange = tmpRange.Resize[rc, cc];
                            WTColumns.Columns.Item[2].Copy(tmpRange);
                        }
                    }
                    tmpRange = WorkingSheet.Range[WorkingSheet.Cells.Item[tmpR, 1], tmpRange];
                    if (i < PagesCount)
                    {
                        tmpRange = tmpRange.Resize[tmpRange.Rows.Count + 1];
                    }
                    tmpR = tmpR + tmpRange.Rows.Count;
                    PartsFormatRangeCol.Add(tmpRange);
                    rs = rs + rc + 2;
                }
            }
        }


        public static void RankMarking(Range rng, ref Array Ranking)
        {
            int r, c;
            int clr;
            Shape o;
            for (r = Ranking.GetLowerBound(0); r <= Ranking.GetUpperBound(0); r++)
            {
                for (c = Ranking.GetLowerBound(1); c <= Ranking.GetUpperBound(1); c++)
                {
                    switch (Ranking.GetValue(r, c))
                    {
                        case 1:
                            clr = 0XFF;//red
                            break;
                        case 2:
                            clr = 0XFF0000;//blue
                            break;
                        case 3:
                            clr = 0X339966;
                            break;
                        default:
                            continue;
                    }
                    o = rng.Worksheet.Shapes.AddShape(
                        Microsoft.Office.Core.MsoAutoShapeType.msoShapeOval, rng.Item[r, c].Left + 2.25, rng.Item[r, c].Top + 2.25, 6, 6
                        );
                    o.Fill.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
                    o.Fill.ForeColor.RGB = clr;
                    o.Line.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
                    o.Line.Weight = 1;
                    o.Line.ForeColor.RGB = 0XFFFFFF; //white
                }
            }
        }

        public static void Hatching(Range rng, ref Array HatchingColorIndex)
        {
            int r, c;
            for (r = HatchingColorIndex.GetLowerBound(0); r <= HatchingColorIndex.GetUpperBound(0); r++)
            {
                for (c = HatchingColorIndex.GetLowerBound(1); c <= HatchingColorIndex.GetUpperBound(1); c++)
                {
                    switch (HatchingColorIndex.GetValue(r, c))
                    {
                        case (XlColorIndex)0:
                        case (XlColorIndex)2:
                        case XlColorIndex.xlColorIndexNone:
                            break;
                        default:
                            rng.Item[r, c].Interior.ColorIndex = HatchingColorIndex.GetValue(r, c);
                            break;
                    }
                }
            }
        }

        public static void AscendingMarking(Range rng, ref Array ArrowEnd)
        {
            int r, c;
            int targetR, targetC;
            double startX = 0, startY = 0;
            double endX = 0, endY = 0;
            int[] tmpArrowEnd;
            Range sCell = null;
            Range eCell;
            Line arrow;
            for (r = ArrowEnd.GetLowerBound(0); r <= ArrowEnd.GetUpperBound(0); r++)
            {
                for (c = ArrowEnd.GetLowerBound(1); c <= ArrowEnd.GetUpperBound(1); c++)
                {
                    if ((ArrowEnd.GetValue(r, c).GetType().IsArray))
                    {
                        tmpArrowEnd = (int[])ArrowEnd.GetValue(r, c);
                        targetR = tmpArrowEnd[0];
                        targetC = tmpArrowEnd[1];
                        if (targetC == c)
                        { //' 横％表
                            if (targetR != r)
                            {
                                sCell = rng.Item[r, c];
                                eCell = rng.Item[targetR, c];
                                startX = sCell.Left + sCell.Width / 4;
                                endX = startX;
                                if (targetR > r)
                                {
                                    startY = sCell.Top;
                                    endY = eCell.Top + eCell.Height;
                                }
                                else if (targetR < r)
                                {
                                    startY = eCell.Top + eCell.Height;
                                    endY = sCell.Top;
                                }
                            }
                        }
                        else if (targetR == r)
                        {    //' 縦％表
                            if (targetC != c)
                            {
                                sCell = rng.Item[r, c];
                                eCell = rng.Item[targetR, c];
                                startY = sCell.Top + sCell.Height * 3 / 4 + 1.5;
                                endY = startY;
                                if (targetC > c)
                                {
                                    startX = sCell.Left;
                                    endX = eCell.Left + eCell.Width;
                                }
                                else if (targetR < c)
                                {
                                    startX = eCell.Left + eCell.Width;
                                    endX = sCell.Left;
                                }
                            }
                        }
                        if (sCell != null)
                        {
                            arrow = (Line)rng.Worksheet.Shapes.AddShape(Microsoft.Office.Core.MsoAutoShapeType.msoShapeLineCallout1, (float)startX, (float)startY, (float)endX, (float)endY);
                            //arrow = rng.Worksheet.Lines. Add(startX, startY, endX, endY);
                            arrow.ShapeRange.Line.EndArrowheadStyle = Microsoft.Office.Core.MsoArrowheadStyle.msoArrowheadTriangle;
                            arrow.ShapeRange.Line.Weight = targetC == c ? 3 : 2;
                            arrow.ShapeRange.Line.ForeColor.RGB = 0XFF9900;
                            arrow.ShapeRange.Line.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
                            sCell = null;
                            eCell = null;
                        }
                    }
                }
            }
        }


        private static void PageSetupLandscapeTable(CrossTable Table
              , Hashtable CutRowsCol, Hashtable CutColumnsCol
              , Worksheet FormatSheet, string FormatRangeNamePrefix
              , TableType TableType, bool HasWeight
              , int ColumnsCountPerPage
              , ref Range StartCell, ref Worksheet Sheet, bool isN
              , int AxesCount, int MaxAxesCount
              , ref int RemainedRowsCount, int MaxRowsCountPerPage, double DefLineHeight, double HeaderRowHeight
              , ref Array ContentsValue, ref Array HyperlinkTargetCells
              , bool IsFirstTable, CrossTable NextTable, string TableIndex
              // , ref Errors()  ErrorStruct, ref ErrorsCount  int, ref res  MethodResult 
              , Hashtable WholeRowCol = null)
        {
            List<Range> tmpCol = null;
            Array TableValue = null;
            Array Ranking = null;
            Array HatchingColorIndex = null;
            Array ArrowEnd = null;
            Array SigTestMarking = null;
            int PagesCount = 0;
            int PageRowsCount = 0;
            Range FormattedRange;
            Range PageTableRange;
            int i;
            int sr;
            int r;
            bool f;
            int idx = 0;
            int num;
            double RemainedHeight;
            double RangeHeight;
            int RangeRowsCount;
            int tmpCount;
            int n;

            // On Error GoTo ErrHdl
            f = true;
            switch (TableType)
            {
                case TableType.NPer:
                    if (CurrentOutput.OutputNTable && CurrentOutput.PageSetupNTable
                            || CurrentOutput.OutputPerTable && CurrentOutput.PageSetupPerTable
                            || CurrentOutput.SignificanceTest && CurrentOutput.PageSetupSignificanceTestTable)
                    {
                        f = false;

                    }
                    break;
                case TableType.SignificanceTest:
                    if (CurrentOutput.OutputNTable && CurrentOutput.PageSetupNTable
                   || CurrentOutput.OutputPerTable && CurrentOutput.PageSetupPerTable)
                    {
                        f = false;
                    }
                    break;
            }
            switch (TableType)
            {
                case TableType.NPer:
                    idx = 4;
                    num = 50;
                    break;
                case TableType.N:
                    idx = 5;
                    num = 60; break;
                case TableType.Per:
                    idx = 6;
                    num = 70; break;
                case TableType.SignificanceTest:
                    idx = 0;
                    num = 80; break;
            }
            CreateTurnedLandscapeCrossArray(Table, CutRowsCol, CutColumnsCol, ref TableValue, ref Ranking, ref HatchingColorIndex,
                ref ArrowEnd, ref SigTestMarking
                                          , 1 + (ToInt(HasWeight) & 1), 1 + AxesCount, HasWeight, isN
                                  , TableType, StartCell.Worksheet.Rows.Count - StartCell.Row - 1
                                  , ColumnsCountPerPage, MaxAxesCount - AxesCount
                                  , ref PagesCount, ref PageRowsCount, WholeRowCol);
            if (TableValue.GetUpperBound(0) == -1)
            { // to do
                StartCell = null;
                if (IsFirstTable)
                {
                    //Me.Application.DisplayAlerts = false;
                    Sheet.Delete();
                    Sheet = null;
                    if (f)
                    {
                        // ResumeError = true;
                        //     Err().Raise vbObjectError + 200&, RunningProcName _
                        //            , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportRowsCountOverAtOneTableOnPageSetupMessageIndex)
                    }
                }
                else
                {
                    // ResumeError = true
                    //  Err().Raise vbObjectError + 200& + num, RunningProcName _
                    //        , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportRowsCountOverOnPageSetupMessageIndex)
                }
                if (f)
                {
                    ContentsValue.SetValue(null, Table.Index + 1, 1);
                    ContentsValue.SetValue(null, Table.Index + 1, 2);
                }
                return;
            }

            FormatTurnedLandscapeTableSub(Table, CutRowsCol, CutColumnsCol, FormatSheet, FormatRangeNamePrefix, TableType, HasWeight, isN
                                            , AxesCount, ColumnsCountPerPage, PagesCount, ref tmpCol, HeaderRowHeight, WholeRowCol);
            FormattedRange = WorkingSheet.Range[tmpCol[0], tmpCol[tmpCol.Count - 1]];
            sr = StartCell.Row;
            r = sr;

            if (RemainedRowsCount < MaxRowsCountPerPage)
            {
                RangeHeight = FormattedRange.Height;
                tmpCount = (int)Math.Ceiling(RangeHeight / DefLineHeight);
                if (tmpCount > RemainedRowsCount)
                {
                    Sheet.Rows.Item[r].Resize[RemainedRowsCount].Delete(XlDeleteShiftDirection.xlShiftUp);
                    RemainedRowsCount = MaxRowsCountPerPage;
                }
            }
            for (i = 0; i < tmpCol.Count(); i++)
            {
                PageTableRange = tmpCol[i];
                RemainedHeight = DefLineHeight * RemainedRowsCount;
                RangeHeight = PageTableRange.Height;
                RangeRowsCount = PageTableRange.Rows.Count;
                tmpCount = (int)Math.Ceiling(RangeHeight / DefLineHeight);
                if (tmpCount > RemainedRowsCount)
                {
                    if (RemainedRowsCount < MaxRowsCountPerPage)
                    {
                        Sheet.Rows.Item[r].Resize[RemainedRowsCount].Delete(XlDeleteShiftDirection.xlShiftUp);
                        RemainedRowsCount = MaxRowsCountPerPage;
                    }
                    n = (int)Math.Ceiling(RangeHeight / (DefLineHeight * MaxRowsCountPerPage));
                    if (n > 1)
                    {
                        Sheet.Rows.Item[Sheet.Rows.Count - (n - 1) * MaxRowsCountPerPage + 1].Resize((n - 1) * MaxRowsCountPerPage).Delete(XlDeleteShiftDirection.xlShiftUp);
                        Sheet.Rows.Item[r + (ToInt(RemainedRowsCount % MaxRowsCountPerPage == 0) & 1)].Resize[(n - 1) * MaxRowsCountPerPage].Insert(XlInsertShiftDirection.xlShiftDown);
                    }
                    RemainedRowsCount = MaxRowsCountPerPage * n;
                }
                if (tmpCount > RangeRowsCount)
                {
                    Sheet.Rows.Item[r + (ToInt(RemainedRowsCount % MaxRowsCountPerPage == 0) & 1)].Resize(tmpCount - RangeRowsCount).Delete(XlDeleteShiftDirection.xlShiftUp);
                }
                else if (tmpCount < RangeRowsCount)
                {
                    Sheet.Rows.Item[Sheet.Rows.Count - (RangeRowsCount - tmpCount) + 1].Resize[RangeRowsCount - tmpCount].Delete(XlDeleteShiftDirection.xlShiftUp);
                    Sheet.Rows.Item[r + (ToInt(RemainedRowsCount % MaxRowsCountPerPage == 0) & 1)].Resize[RangeRowsCount - tmpCount].Insert(XlInsertShiftDirection.xlShiftDown);
                }
                r = r + RangeRowsCount;
                RemainedRowsCount = RemainedRowsCount - tmpCount;
                if (RemainedRowsCount == 0) { RemainedRowsCount = MaxRowsCountPerPage; }
            }
            StartCell = Sheet.Cells.Item[sr, 1];
            OutputUtil.CopyRow(FormattedRange, StartCell);
            OutputUtil.PutValue(StartCell.Range["B2"], ref TableIndex);
            Range WithStartCellRange = StartCell.Range["B3"].Resize[TableValue.GetUpperBound(0), TableValue.GetUpperBound(1)];
            WithStartCellRange.Value = TableValue;
            if (!isN)
            {
                if (CurrentOutput.MarkingRanking) { RankMarking(WithStartCellRange.Cells, ref Ranking); }
                if (CurrentOutput.MarkingColoring) { Hatching(WithStartCellRange.Cells, ref HatchingColorIndex); }
                if (CurrentOutput.MarkingAscending) { AscendingMarking(WithStartCellRange.Cells, ref ArrowEnd); }
            }
            if (CurrentOutput.MarkingSignificance) { SignificanceTestMarking(WithStartCellRange.Cells, ref SigTestMarking); }
            if (idx > 0)
            {
                ContentsValue.SetValue(TableIndex, Table.Index + 1, idx);
                HyperlinkTargetCells.SetValue(StartCell, Table.Index + 1, idx);
            }
            if (WithStartCellRange.Row + WithStartCellRange.Rows.Count < WithStartCellRange.Worksheet.Rows.Count)
            {
                StartCell = WithStartCellRange.Item[WithStartCellRange.Rows.Count + 1, 1].EntireRow.Range("A1");
            }
            else
            {
                StartCell = null;
                if (NextTable != null)
                {
                    ResumeError = true;
                    // Err().Raise vbObjectError + 200& + num, RunningProcName _
                    //   , ThisWorkbook.GetReportKeyword(ReportMessageIndex_ReportRowsCountOverOnPageSetupMessageIndex)
                }
            }
            // End With
            //ExitProc:
            //    RunningProcName = OrgProcName
            //    Exit Sub
            //ErrHdl:
            //if( IsDebug ){
            //    Debug.Print RunningProcName
            //    Debug.Print Err().Number, Err().Description
            //    Stop
            //    Resume
            //}
            //    if( ResumeError ){
            //        res = RaisedError
            //        PushError Err(), Errors, ErrorsCount
            //        Resume }
            //    }else{
            //        With Err()
            //            .Raise .Number, .Source, .Description, .HelpFile, .HelpContext
            //        End With
            //    }
        }


        private static void SignificanceTestMarking(Range rng, ref Array SigTestMarking)
        {
            int y, x;
            Range c;
            string buf;
            string fmt;
            for (y = SigTestMarking.GetLowerBound(0); y <= SigTestMarking.GetUpperBound(0); y++)
            {
                for (x = SigTestMarking.GetLowerBound(1); x <= SigTestMarking.GetUpperBound(1); x++)
                {
                    buf = (string)SigTestMarking.GetValue(y, x);
                    if (null != buf && buf.Length > 0)
                    {
                        c = rng.Item[y, x];
                        fmt = @"""" + buf + @"""" + c.NumberFormat;
                        c.NumberFormat = fmt;
                    }
                }
            }
        }

        public static string TemplatePath(

        ref XlFileFormat FileFormat,
                 TableOrientation Orientation = TableOrientation.Landscape
      )
        {
            if (FileFormat == null) { FileFormat = XlFileFormat.xlOpenXMLWorkbook; }
            string d;
            string n;

            if (Orientation != TableOrientation.Portrait) { Orientation = TableOrientation.Landscape; }
            FileFormat = (XlFileFormat)CurrentOutput.ParentRequest.ExcelFileFormat;
            if (FileFormat != XlFileFormat.xlExcel8) { FileFormat = XlFileFormat.xlOpenXMLWorkbook; }
            if ((CurrentOutput.ParentReportset.FileType & FileType.Report) == 0)
            {
                if (CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Multi)
                {
                    if (Orientation == TableOrientation.Landscape)
                    {
                        n = TEMPLATE_NAME;
                    }
                    else
                    {
                        n = TRANSPOSE_TEMPLATE_NAME;
                    }
                }
                else
                {
                    if (Orientation == TableOrientation.Landscape)
                    {
                        n = INDIVIDUAL_TEMPLATE_NAME;
                    }
                    else
                    {
                        n = TRANSPOSE_INDIVIDUAL_TEMPLATE_NAME;
                    }
                }
            }
            else
            {
                if (Orientation == TableOrientation.Landscape)
                {
                    n = REPORT_TEMPLATE_NAME;
                }
                else
                {
                    n = TRANSPOSE_REPORT_TEMPLATE_NAME;
                }
            }
            if (FileFormat == XlFileFormat.xlOpenXMLWorkbook) { n = n + "x"; }
            d = OutputUtil.GetTemplateDirectoryPath(WorkingBook.Path, WorkingBook.Application.PathSeparator);
            return OutputUtil.BuildPath(d, n, WorkingBook.Application.PathSeparator);
        }

        public static string FormatTemplatePath(
                 TableOrientation Orientation = TableOrientation.Landscape
              )
        {
            string d;
            string n;
            if (Orientation != TableOrientation.Portrait) { Orientation = TableOrientation.Landscape; }
            if ((CurrentOutput.ParentReportset.FileType & FileType.Report) == 0)
            {
                if (CurrentOutput.TablesOnOnesheet == TablesOnOneSheet.Multi)
                {
                    if (Orientation == TableOrientation.Landscape)
                    {
                        n = FORMAT_TEMPLATE_NAME;
                    }
                    else
                    {
                        n = TRANSPOSE_FORMAT_TEMPLATE_NAME;
                    }
                }
                else
                {
                    if (Orientation == TableOrientation.Landscape)
                    {
                        n = INDIVIDUAL_FORMAT_TEMPLATE_NAME;
                    }
                    else
                    {
                        n = TRANSPOSE_INDIVIDUAL_FORMAT_TEMPLATE_NAME;
                    }
                }
            }
            else
            {
                if (Orientation == TableOrientation.Landscape)
                {
                    n = REPORT_FORMAT_TEMPLATE_NAME;
                }
                else
                {
                    n = TRANSPOSE_REPORT_FORMAT_TEMPLATE_NAME;
                }
            }
            d = OutputUtil.GetTemplateDirectoryPath(WorkingBook.Path, WorkingBook.Application.PathSeparator);
            return OutputUtil.BuildPath(d, n, WorkingBook.Application.PathSeparator);
        }



        private static void PutContents(
                Worksheet ContentsSheet, ref Array ContentsValue
              , ref Array HyperlinkTargetCells //Excel.Range 
              , Application Application
              , Hashtable OrgSheets = null)
        {
            string OrgProcName;
            int i, j;
            int r = 0;
            // Worksheet sht  ;
            Sheets shts;
            //  Object sh  ;
            string n;
            Object tmpSht;
            string tmp;
            Range WithContentsSheet = ContentsSheet.Range["Contents"];
            OutputUtil.PutValue(WithContentsSheet.Cells, ref ContentsValue);
            for (i = HyperlinkTargetCells.GetLowerBound(0); i <= HyperlinkTargetCells.GetUpperBound(0); i++)
            {
                r = r + 1;
                for (j = HyperlinkTargetCells.GetLowerBound(1); j <= HyperlinkTargetCells.GetUpperBound(1); j++)
                {
                    if (HyperlinkTargetCells.GetValue(i, j) != null)
                    {
                        Range tmpRng = (Range)HyperlinkTargetCells.GetValue(i, j);
                        tmp = "'" + tmpRng.Worksheet.Name + "'!" + tmpRng.Address;
                        WithContentsSheet.Item[r, j].Hyperlinks.Add(WithContentsSheet.Item[r, j], "", tmp);
                    }
                }
            }
            //if( OrgSheets != null ){  //  ' 1シート1クロス
            //            Application.DisplayAlerts = false;
            //    foreach(Worksheet sht in OrgSheets){
            //        if( sht.Name != ContentsSheet.Name ){ sht.Delete(); }
            //    }
            //             shts = ContentsSheet.Parent.Sheets;
            //    foreach( object sh in shts){
            //                n = ()sh.Name + "~2";
            //        On Error Resume }
            //        Set tmpSht = shts.Item(n)
            //        if( Not tmpSht == null ){
            //            sh.Name = sh.Name & "~1"
            //        }
            //        On Error GoTo 0
            //        Set tmpSht = null
            //    }
            //}
        }

        private static void SaveBook(Workbook Book
              , string Prefix,
            Application Application,
            string Suffix = ""
              , XlFileFormat FileFormat = XlFileFormat.xlOpenXMLWorkbook)
        {
            string ext;
            string n;
            string p;
            Worksheet sh;
            int i;
            Sheets WithBookWorksheets = Book.Worksheets;
            if (WithBookWorksheets.Count == 1)
            {
                Book.Close(false);
                return;
            }
            for (i = WithBookWorksheets.Count; i >= 1; i--)
            {
                sh = WithBookWorksheets.Item[i];
                if (sh.Visible == XlSheetVisibility.xlSheetVisible)
                {
                    //    WithBookWorksheets.Application.GoTo(sh.Range["A1"]);
                }
            }
            ext = FileFormat == XlFileFormat.xlExcel8 ? "xls" : "xlsx";
            do
            {
                i = i + 1;
                n = Prefix + i + Suffix + "." + ext;
                p = OutputUtil.BuildPath(macroExecuter.OutputDirectoryPath, n, Application.PathSeparator);
            } while (File.Exists(p));

            Book.CheckCompatibility = false;
            Application.DisplayAlerts = false;
            Application.PrintCommunication = true;
            Book.SaveAs(p, FileFormat);
            Book.Close(false);
            // OutputBooksPathCol.Add p, n
        }
    }
}
