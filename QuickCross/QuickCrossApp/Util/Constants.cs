using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qc4Launcher.Util
{
    internal class Constants
    {
        public const int MAX_ROW_COUNT = 100000;
        public static int MaxRowCount = 20000;
		public const int MaxRowLimit = 20000;
        public const string EqEqual = "=";
        private static char a = (char)14;
        public static string Password = "MacroMill" + a + "!3";
        public static string EXT_QC4 = ".qc4";
        public static string Qc4Extension = ".qc4";
        public static string Qc3Extension = ".qc3x";
        public static string CsvExtension = ".csv";
        public static string TabExtension = ".tsv";
        public static int MarkingValue = 30;
        public static int MarkingMaxValue = 3000;
        public static int MarkingMinValue = 0;
        public static int DifferenceSet1Value = 10;
        public static int DifferenceSet2Value = 5;
        public static int DifferenceSetMaxValue = 30;
        public static int DifferenceSetMinValue = 1;
        public static string WeightBack = "WeightBack";
        public static string ExportPathDefaultFileName = "_rawdata";
        public static string Qc4FileName = "";
        public static string Variable_Type_An= "An";
        public static string MulivariateTablename = "multivariate";
        //Pro Switch
        public const string QC4Key = "QC4STDPRO";
        public static bool IsPro = true;
        public const string EncryptDecryptPass = "QC1234";
        public const string RegistryPath = @"Software/QC4";
        public const string RegistrykeyName = "qc4Key";
        public const string HiddenAuthKey = "ACT";

        public const string MessageBoxTitle = "QuickCross";
        public const int ExcelMaxLength = 32767;
        public const double ExcelRowMaxHeight = 409.5;
        public const int MaxChoiceCount = 1000;
        public const int StartRow = 5;
        public const int EndRow = 1048575;
        public const int StartCol = 3;
        public const int EndCol = 1040;

        public static class DP
        {
            public const int CategoryCountColumn = 3;
            public const int QuestionVariable = 1;
            public const int QuestionVariableType = 2;
            public const int QuestionColumn = 8;
            //Column Indexes
            public const int OnOffColumn = 3;
            public const int CheckCrossColumn = 4;
            public const int CriteriaVariableColumn = 5;
            public const int CriteriaOperatorColumn = 6;
            public const int CriteriavalueColumn = 7;
            public const int InstructionColumn = 8;
            public const int SubstituteVariableColumn = 9;
            public const int SubstituteOperatorColumn = 10;
            public const int SubstituteParam1Column = 11;
            public const int SubstituteParam2Column = 12;
            public const int SubstituteParam3Column = 13;

            public const int MAX_DP_COLUMN = 1040;
            public const int SHRINKABLE_LENGTH = 5;

            public const int ErrDiv0 = -2146826281;
            public const int ErrGettingData = -2146826245;
            public const int ErrNA = -2146826246;
            public const int ErrName = -2146826259;
            public const int ErrNull = -2146826288;
            public const int ErrNum = -2146826252;
            public const int ErrRef = -2146826265;
            public const int ErrValue = -2146826273;

            public const string ListMethod = "List_Method";

            //row indexes
            public const int ProUIstartRow = 5;


            //InstructionList
            public const string InstructionAND = "AND";
            public const string InstructionOR = "OR";
            public const string InstructionTHEN = "THEN";
            public const string InstructionOMIT = "OMIT";
            public const string InstructionDELETE = "DELETE";
            public const string InstructionLDEL = "LDEL";
            public const string InstructionLISTUP = "LISTUP";
            public const string InstructionOMIT2 = "OMIT2";
            public const string InstructionDECST = "DECST";
            public const string InstructionDECEND = "DECEND";
            public const string InstructionCALL = "CALL";
            public const string InstructionFOR = "FOR";
            public const string InstructionNEXT = "NEXT";



            //Substitute operators

            public const string SubstituteOperatorADD1 = "ADD1";
            public const string SubstituteOperatorADD2 = "ADD2";
            public const string SubstituteOperatorADD3 = "ADD3";
            public const string SubstituteOperatorMINUS1 = "MINUS1";
            public const string SubstituteOperatorMINUS2 = "MINUS2";
            public const string SubstituteOperatorEQUAL = ":=";

            public const string SubstituteOperatorCLASS = "CLASS";
            public const string SubstituteOperatorADD = "ADD3";
            public const string SubstituteOperatorJOINT = "JOINT";
            public const string SubstituteOperatorRECODE = "RECODE";
            public const string SubstituteOperatorMTOS = "MTOS";
            public const string SubstituteOperatorCOUNT = "COUNT";
            public const string SubstituteOperatorINTEGRATE = "INTEGRATE";
            public const string SubstituteOperatorMCONVERT = "MCONVERT";
            public const string SubstituteOperatorCOMPUTE = "COMPUTE";
            public const string SubstituteOperatorMAX = "MAX";
            public const string SubstituteOperatorMIN = "MIN";
            public const string SubstituteOperatorAVG = "AVG";
            public const string SubstituteOperatorSUM = "SUM";
            public const string SubstituteOperatorADD_MINUS = "ADD_MINUS";
            public const string SubstituteOperatorDELETE = "DELETE";


            //param fixedlist
            public const string CLASSParam2List = "1,2,3,4";
            public const string CLASSParam3List = "1,2";
            public const string DecscriptionList = "1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26";

            public const string MTOSParam2List = "1,2";
            public const string ParamList01 = "0,1";
            public const string ANDORList = "AND,OR";



            public const string DPButtonMenu = "Command_Menu";
            public const string DPButtonExec = "Command_Dp_Exec";
            public const string DPButtonInsert = "Command_Dp_Insert";
            public const string DPButtonUp = "Command_Dp_Up";
            public const string DPButtonDown = "Command_Dp_Down";
            public const string DPButtonCopy = "Command_Dp_Copy";
            public const string DPButtonPaste = "Command_Dp_Paste";
            public const string DPButtonDelete = "Command_Dp_Delete";
            public const string DPButtonCheck = "Command_Dp_Check";

            public const string TempTableVarcharDataType = "VARCHAR(255)";
            public const string TEMP_DATA_AFTER_PROC = "temp_data_after_process";

            public const int INTEGRATEParam2MAXValue = 10;

            //column starting  numbers
            public const string DPColBegin = "C"; //for getting and clearing column
        }
        public static readonly int[] DefaultColorIndex = new int[] {
           0,0x926036,0xBFBFBF,0xD7B395,0xB4D5FC,0xA26480,0xBCE4D8,0xE4CCB8,0xE8DEB7,0xBD814F,0x4696F7,0x7A4960,0x59BB9B,0xD58D53,0xC6AC4B,0xA6A6A6,0x262626,0xFF6600,0x807CFF,0x50D092,0x00C0FF,
           0xCCCC33,0xFF6699,0xC07000,0xF2F2F2,0xFF0000,0x0000FF,0x50B000,0x0099FF,0xF0B000,0xA03070,0x602000,0xFFFFFF,0xDAC0CC,0xF3EEDA,0xD9E9FD,0xDEF1EB,0xECDFE4,0xF1E6DC,0xF1D9C5,0xDBDCF2,
           0xC7A0B1,0xDCCD92,0x9BD7C4,0xB7B8E6,0x9496DA,0x4D50C0,0x7D491F,0x595959,0x9B8631,0x8FBFFA,0x0A6BE2,0x3C9376,0x343696,0xE2B48D,0x5C3616,0x000000};

        internal static class PathName
        {
            internal const string TempPath = "temp\\";
            internal const string FileOpenTemp = "QC4\\File\\";
            internal const string FileSwapData = "QC4\\Swap\\";
            internal const string FileDataOutput = "QC4\\DO\\";

            internal const string TempDataImportPath = "QC4\\DataImport\\";
            internal const string TempDataImportSourcePath = "QC4\\DataImport\\Temp\\Source\\";
            internal const string TempDataImportDestPath = "QC4\\DataImport\\Temp\\Dest\\";

        }

        internal static class TemplateFile
        {
            //internal const string QC4_Template = "Qc4_Template.xlsx";
            internal const string QC4_Template = "Macromill.Quick-CROSS";
            internal const string QC4_Template_Do = "Qc4_Template_DO.xlsx";
            internal const string QC3_Template = "Qc3_Template.xlsm";
            internal const string DB_FIlE = "qc4.db";
            internal const string DATA_OUTPUT = "DataOutput.xlsx";
        }

        public static class GT
        {
            public const int GtColExec = 2;
            public const int GtColChartType = 3;
            public const int GtColTestCode = 4;
            public const int GtColGraphType = 5;
            public const int GtColTableHeading = 6;
            public const int GtColItem = 7;
            public const int GtRowDataStart = 5;
            public const int GtMaxItemNo = 200;
            public const int GtColumnLimit = 206;

            public const string GTCheckBoxSig1Per = "Gt_Check_99";
            public const string GTCheckBoxSig5Per = "Gt_Check_95";
            public const string GTCheckBoxSig10Per = "Gt_Check_90";

            public const string GtQCMainQuesType = "A2:A18";
            public const string GtQCMainChartType = "B23:B30";
            public const string GTStartAddress = "B5";

            public const string GTSA = "GT-SA";
            public const string GTMA = "GT-MA";
            public const string GTN = "GT-N";
            public const string GTRAT = "GT-RAT";
            public const string GTRNK = "GT-RNK";
            public const string GTMTS = "GT-MTS";
            public const string GTMTM = "GT-MTM";
            public const string GTMTN = "GT-MTN";

            public const string GTBUTTONINSERT = "Command_Gt_Insert";
            public const string GTBUTTONDELETE = "Command_Gt_Delete";
            public const string GTBUTTONCHECK = "CommandButton1";

            public static System.Drawing.Color GTGraphBorder = System.Drawing.Color.FromArgb(191, 191, 191);
            public static System.Drawing.Color GTColBorder = System.Drawing.Color.FromArgb(166, 166, 166);

            public static int DifferenceSetMaxValueGT = 50;
            public static int DifferenceSetMinValueGT = 0;
        }

        public static class GraphType
        {
            public const string GRAPH_TYPE_QCCIRCLE = "001";
            public const string GRAPH_TYPE_QCWIDTHBELT = "002";
            public const string GRAPH_TYPE_QCHEIGHTBELT = "003";
            public const string GRAPH_TYPE_QCWIDTHSTICK = "004";
            public const string GRAPH_TYPE_QCHEIGHTSTICK = "005";
            public const string GRAPH_TYPE_QCMWIDTHSTICK = "006";
            public const string GRAPH_TYPE_QCMHEIGHTSTICK = "007";
            public const string GRAPH_TYPE_QCMCIRCLE = "008";
            public const string GRAPH_TYPE_QCWIDTHSTICKRAT = "009";
            public const string GRAPH_TYPE_QCHEIGHTSTICKRAT = "010";
            public const string GRAPH_TYPE_QCMCIRCLERAT = "011";
            public const string GRAPH_TYPE_QCWIDTHONSTICK = "012";
            public const string GRAPH_TYPE_QCHEIGHTONSTICK = "013";
            public const string GRAPH_TYPE_QCLINE = "014";
            public const string GRAPH_TYPE_SA_MATRIX_QCM_WIDTH_BELT_CODE_VALUE = "015";
            public const string GRAPH_TYPE_SA_MATRIX_QCM_HEIGHT_BELT_CODE_VALUE = "016";
        }

        public static class GTGraphColorIndex
        {
            //public const string CIRCLE = "37,34,36,40,38,44,39,43,15,19,8,6,7,14,42,41,23,24,19,20";
            //public const string BELT = "3,45,6,43,50,42,41,11,24,38,3,45,6,43,50,42,41,11,24,38";
            //public const string LINE = "3,10,5,33,46,6,7,4,9,10,11,12,13,14,16,22,17,18,56,31";
            public const string CIRCLE = "3,45,43,50,42,41,13,7,44,6,4,8,33,54,38,40,36,35,34,37";
            public const string BELT = "3,45,43,50,42,41,13,7,44,6,4,8,33,54,38,40,36,35,34,37";
            public const string LINE = "3,45,43,50,42,41,13,7,44,6,4,8,33,54,38,40,36,35,34,37";

            //public const string WIDTH_STICK = "23";
            //public const string HEIGHT_STICK = "41";
            public const string WIDTH_STICK_M = "24";
            public const string HEIGHT_STICK_M = "24";

            public const string STICK = "3,45,43,50,42,41,13,7,44,6,4,8,33,54,38,40,36,35,34,37";
        }

        public static class GradationType
        {
            public const string GRADATION_TYPE_NONE = "001";
            public const string GRADATION_TYPE_MSO_GRADIENT_HORIZONTAL = "002";
            public const string GRADATION_TYPE_MSO_GRADIENT_VERTICAL = "003";
            public const string GRADATION_TYPE_MSO_GRADIENT_DIAGONALUP = "004";
            public const string GRADATION_TYPE_MSO_GRADIENT_DIAGONALDOWN = "005";
            public const string GRADATION_TYPE_MSO_GRADIENT_FROMCORNER = "006";
            public const string GRADATION_TYPE_MSO_GRADIENT_FROMCENTER = "007";
        }

        internal static class DataSheet
        {
            public const int ROW_START = 4;
            public const int ROW_HEADER = 3;
            public const int COL_START = 1;
            public const int COL_HEADER = 1;
            public const int RowStart = 4;
            public const int RowHeader = 3;
            public const int ColStart = 1;
            public const int ColHeader = 1;
            public const string HeaderStartCell = "A3";
        }

        internal static class SheetType
        {
            public const string sh_QuesSetting = "Sheet11_Qs";
            public const string sh_QuesSettingB = "Question Setting B";
            public const string sh_DataProcess = "Data Process";
            public const string sh_DataProcessS = "Data Process S";
            public const string GTCounting = "Sheet13_Gt";
            public const string sh_GTCountingS = "GT Tabulation S";
            public const string sh_SummaryList = "Sheet14_Cl";
            public const string sh_CrossCounting = "Sheet14_Cr";
            public const string sh_CrossCountingS = "Cross Tabulation S";
            public const string sh_FAMake = "FA Creation";
            public const string sh_FAMakeS = "FA Creation S";
            public const string sh_GridCounting = "Grid Tabulation";
            public const string sh_PackCounting = "Pack Tabulation";
            public const string sh_ListView = "List";
            public const string sh_Setting = "Setting";
            public const string sh_SetDetails = "Details Setting";
            public const string sh_Data = "Data";
            public const string sh_Data_After = "Data??(After Process)";
            public const string sh_DelList = "LDEL";
            public const string sh_ANSettingS = "Analysis Setting S";
            public const string sh_Data_AN = "Multivariate";
            public const string sh_Data_AN2 = "Multivariate";
            public const string sh_Qinfo = "Qinfo";
            public const string sh_Data01 = "Data01";
            public const string sh_DataAfterProcess = "Data After Process";
            public const string sh_Work = "Data After Process";
            public const string sh_Sheet1 = "分析設定S";
            public const string sh_Sheet2 = "MA S";
        }

        internal static class AnswerType
        {
            public const string SA = "SA";
            public const string MA = "MA";
            public const string FA = "FA";
            public const string N = "N";
            public const string D = "D";
        }

        internal static class QuestionType
        {
            public const string SAS = "SAS";
            public const string SAR = "SAR";
            public const string SAP = "SAP";
            public const string MAC = "MAC";
            public const string FAS = "FAS";
            public const string RAT = "RAT";
            public const string FAL = "FAL";
            public const string MTS = "MTS";
            public const string MTT = "MTT";
            public const string RNK = "RNK";
            public const string MTM = "MTM";
            public const string MTN = "MTN";
            public const string EMPTY = "";
        }

        internal enum QuestionTypeEnum
        {
            SAS = 1,
            SAR = 2,
            SAP = 3,
            MAC = 4,
            FAS = 5,
            RAT = 6,
            FAL = 7,
            MTS = 8,
            MTT = 9,
            RNK = 10,
            MTM = 11
        }

        internal enum AnswerTypeEnum
        {
            SA = 1,
            MA = 2,
            FA = 3,
            N = 4,
            D = 5
        }

        internal static class QS
        {
            //Need to delete
            public const int START_ROW = 4;
            public const int ROW_HEADER = 3;
            public const int START_COL = 2;
            public const int COL_NEW = 2;
            public const int COL_VARIABLE = 6;
            public const int COL_ANSWER_TYPE = 7;
            public const int COL_CAT_COUNT = 8;
            public const int COL_COUNT = 215;

            public const int StartRow = 4;
            public const int RowHeader = 3;
            public const int ColStart = 2;
            public const int ColEnd = 1037;
            public const int ColNew = 2;
            public const int ColVariable = 6;
            public const int ColAnswerType = 7;
            public const int ColCategoryCount = 8;
            public const int ColChoiceBegin = 13;
            public const string StartVariableCell = "F4";
            public const string HeaderStartCell = "B3";

            public const int QsColNew = 2;
            public const int QsColQuestionNumber = 3;
            public const int QsColQuestionType = 4;
            public const int QsColNumberOfQuestion = 5;
            public const int QsColItem = 6;
            public const int QsColAnswerType = 7;
            public const int QsColCategories = 8;
            public const int QsColWT = 9;
            public const int QsColSortDisplay = 10;
            public const int QsColTableHeading = 11;
            public const int QsColQuestion = 12;
            public const int QsColChoiceBegin = 13;
            public const int QsColChoiceEnd = 1012;
            public const int QsColCount = 1014;
            public const int QsColCountBase = 1015;
            public const int QsColAddSunTotal = 1016;
            public const int QsColNumberSubTotal = 1017;
            public const int QsColSubtotal1 = 1018;
            
            //public const int ColCount = 215;
        }

        public static class Color
        {
            public const int yellow = 19;
            public static dynamic LightBlue = System.Drawing.Color.FromArgb(204, 236, 255);
            public static dynamic LightGreen = System.Drawing.Color.FromArgb(204, 255, 204);
            public static dynamic Purple = System.Drawing.Color.FromArgb(204, 204, 255);
            public static dynamic Blue = System.Drawing.Color.FromArgb(0, 0, 255);
            public static dynamic LightGrey = System.Drawing.Color.FromArgb(234, 234, 234);
            public static dynamic Red = System.Drawing.Color.FromArgb(255, 0, 0);
            public static dynamic Black = System.Drawing.Color.FromArgb(0, 0, 0);
            public static dynamic White = System.Drawing.Color.FromArgb(255, 255, 255);
            public static dynamic Cream = System.Drawing.Color.FromArgb(255, 255, 204);
            public static dynamic YellowGreen = System.Drawing.Color.FromArgb(80, 208, 146);
            public static dynamic Grey = System.Drawing.Color.FromArgb(127, 127, 127);
            public static dynamic GreyBorder = System.Drawing.Color.FromArgb(186, 186, 186);
            public static dynamic GreyFont = System.Drawing.Color.FromArgb(128, 128, 128);
            public static dynamic DarkGrey = System.Drawing.Color.FromArgb(192, 192, 192);
            public static dynamic InteriorColor = System.Drawing.Color.FromArgb(215, 239, 249);//change
        }

        public static class VariableList
        {
            public const string ListItemSA = "List_Item_SA";
            public const string ListItemMA = "List_Item_MA";
            public const string ListItemN = "List_Item_N";
            public const string ListItemFA = "List_Item_FA";
            public const string ListItemD = "List_Item_D";
            public const string ListItemSAN = "List_Item_SAN";
            public const string ListItemMAN = "List_Item_MAN";
            public const string ListItemSAMA = "List_Item_SAMA";
            public const string ListItemSAMAN = "List_Item_SAMAN";
            public const string ListItemALL = "List_Item_ALL";
            public const string ListItemALLD = "List_Item_ALLD";
            public const string ListAnswer = "List_AnsType";
        }

        internal static class SheetCodeName
        {
            public const string QuestionSetting = "Sheet11_Qs";
            public const string DataProcess = "Sheet12_Dp";
            public const string CrossTabulation = "Sheet14_Cr";
            public const string SummaryTable = "Sheet14_Cl";
            public const string GTTabulation = "Sheet13_Gt";
            public const string FACreation = "Sheet15_Fo";
            public const string GridTabulation = "Sheet16_Gr";
            public const string PackTabulation = "Sheet17_Pk";
            public const string LDEL = "Sheet18_LDEL";
            public const string Data = "Sheet00_Data";
            public const string QuestionSettingB = "Sheet21_QsB";
            public const string DataProcessS = "Sheet22_DpS";
            public const string GTTabulationS = "Sheet23_GtS";
            public const string CrossTabulationS = "Sheet24_CrS";
            public const string FACreationS = "Sheet25_FoS";
            public const string AnalysisSettingS = "Sheet1";
            public const string List = "Sheet91";
            public const string Setting = "Sheet92_Cm";
            public const string DetailsSetting = "Sheet93_Cm";
            public const string Data01 = "Sheet00_Data1";
            public const string Data02 = "Sheet00_Data2";
            public const string Qinfo = "Qinfo";
            public const string Data01After = "Sheet00_Data3";
            public const string Data02After = "Sheet00_Data4";
            public const string DataAn = "Sheet00_Data5";
            public const string SummaryTabulation = "Sheet14_Cl";
            public const string Work = "Sheet3";
            public const string MultiVariate = "Sheet2";//"Sheet_MA_S"; //Redmine ID 206014]
            public const string MultiVariateSheet = "Sheet1";
        }

        public enum DataType
        {
            INTEGER = 1,
            STRING = 2,
            DATETIME = 3,
            LONG = 4
        }

        internal class Setting
        {
            public const string MODE_CELL = "D20";
            public const string MODE_STD = "STD";
            public const string MODE_PRO = "PRO";
            public const string WbChoiceBegin = "I3";
            public const string WbVariable = "J2";

        }

        internal class DataOutput
        {
            internal enum FileType
            {
                Excel2007,
                CSV,
                TAB,
                SPSS,
                QLayout,
                QC3,
                QC4,
                R2D3,
                NONE
            }

            public static string defaultEncoding = "Shift-JIS";
        }

        internal class AdvancedSetting
        {

            public const string DoSelectedVariableStartCell = "C2";
            public const string AdvSettingStartCell = "A2";

            //public const string DoSettingStartCell = "B169";
            // public const string DoSettingEndCell = "B209";

            //public const int DoTextChangeFileIndex = 1;
            //public const int DoComboOutputFileTypeIndex = 2;
            //public const int DoComboNonAnserIndex = 1;
            //public const int DoComboNonApplyingIndex = 2;
            //public const int DoTextOutputPathIndex = 1;
            //public const int DoCheckVerticalIndex = 2;
            //public const int DoOptionDirectIndex = 3;
            //public const int DoOptionZeroIndex = 4;
            //public const int DoOptionDKIndex = 5;
            //public const int DoOptionOffZeroIndex = 6;
            //public const int DoOptionOffDKIndex = 7;
            //public const int DoCheckSplitIndex = 8;
            //public const int DoComboOutputTypeIndex = 9;
            //public const int DoCheckRefineConditionIndex = 10;
            //public const int DoComboConditionalItem1Index = 11;
            //public const int DoComboConditionalOperator1Index = 12;
            //public const int DoComboConditionalValue1Index = 13;
            //public const int DoOptionConditionalAnd1Index = 14;
            //public const int DoOptionConditionalOr1Index = 15;
            //public const int DoComboConditionalItem2Index = 16;
            //public const int DoComboConditionalOperator2Index = 17;
            //public const int DoComboConditionalValue2Index = 18;
            //public const int DoComboConditionalItem3Index = 19;
            //public const int DoComboConditionalOperator3Index = 20;
            //public const int DoComboConditionalValue3Index = 21;
            //public const int DoComboConditionalItem4Index = 22;
            //public const int DoComboConditionalOperator4Index = 23;
            //public const int DoComboConditionalValue4Index = 24;
            //public const int DoOptionConditionalAnd2Index = 25;
            //public const int DoOptionConditionalOr2Index = 26;
            //public const int DoOptionConditionalOr3Index = 27;
            //public const int DoOptionConditionalAnd3Index = 28;
            //public const int DoOptionConditionalAnd4Index = 29;
            //public const int DoComboConditionalItem5Index = 30;
            //public const int DoComboConditionalOperator5Index = 31;
            //public const int DoComboConditionalValue5Index = 32;
            //public const int DoOptionConditionalOr4Index = 33;
            //public const int DoComboClassifyItemIndex = 34;
            //public const int DoComboClassifyFolderPathIndex = 35;
            //public const int DoComboMALabelIndex = 36;
            //public const int DoCheckUnicodeInde2 = 37;
        }

        public class DatatableSettings
        {
            public const string ColumnNameReplacer1 = "%£!*&^$/#><--ReplaceKey1--<>#/$^&*!£%";
            public const string ColumnNameReplacer2 = "%£!*&^$/#><--ReplaceKey2--<>#/$^&*!£%";
            public const string ColumnNameReplacer3 = "%£!*&^$/#><--ReplaceKey3--<>#/$^&*!£%";
            public const string ColumnNameReplacer4 = "%£!*&^$/#><--ReplaceKey4--<>#/$^&*!£%";
            public const string RowIdColumnNameReplacer = "%£!*&^$/#><--ReplaceKeyRowId--<>#/$^&*!£%";
            public const string DummyColumnName = "Untitled Column"; //eg: Column-1
            public const string DummyColumnSpecifier = ";',./`*/-+@()-_={}[]:%£!*&^$/#><--ColumnSpecifier--<>;',./`*/-+@()-_={}[]:%£!*&^$/;',./`*/-+@()-_={}[]:%£!*&^$/#";
            public const string DummyStringQuotes = ";',./`*/-+@()-_={}[]:%£!*&^$/#><--ColumnSpecifier--<>;',./`*/-+@()-_={}[]:%£!*&^$/;',./`*/-+@()-_={}[]:%£!*&^$/#";
        }

        public class ComboBoxSettings
        {
            public const string NoneText = "--None--";
        }

        public class DBSettings
        {
            public const string ColumnNamePreText = "q_";// eg: q_1, q_2
            public const string SampleIdColumnName = "sample_id";
            public const string NotApplicableCharacter = "*";
            public const string WeightBackTablePrefix = "weight_back_"; // eg : weight_back_q_3 
            public const int BulkDataInsertMaxRecordPerTrans = 20000;
            public const int MaxNoOfColumnInsertInBulk = 300;
        }

        public class QuestionVariableValue
        {
            public static String QuestionVariableItem = "SAMPLEID";
        }

        public class FileSettings
        {
            public const string MainWindowFileFilter = "All (*.qcg;*.qc3x;*.qcgx;*.qc3;*.qc4)|*.qcg;*.qc3x;*.qcgx;*.qc3;*.qc4|QC3 files (*.qcg;*.qc3x;*.qcgx;*.qc3)|*.qcg;*.qc3x;*.qcgx;*.qc3|QC4 files(*.QC4)|*.qc4"; //QC3 files (*.qcg;*.qc3x;*.qcgx;)|*.qcg;*.qc3x;*.qcgx|
            public const string DataImportSourceFileFilter = "Excel/Text files (*.xls, *.xlsx, *.xlsm, *.csv, *.tsv, *.txt)|*.xls;*.xlsx;*.xlsm;*.csv;*.tsv;*.txt";
        }

        public class DataImportSettings
        {
            public const string DataImportSourceTempTableRemoveComma = "temp_data_import_remove_comma";
            public const string DataImportSourceTempTable = "temp_data_import";
            public const string TempTableVarcharDataType = "TEXT";
            public const string DataImportSourceTempTableMA = "temp_table_ma";
            public const string DataImportTempAnswers = "temp_answers";
            public const string DataImportTempDataAfterProcess = "temp__data_after_process";
            public const string DataImportTempDataAfterProcess1 = "temp__data_after_process1";
            //public const string DoubleQuote = @"""";
            //public const string DoubleQuoteReplacer = @"""""";
        }

        public static class RibbonMessage
        {
            public const string msg_Menu = "GOTOMENU";
            public const string msg_FAExecute = "FAExecuteClick";
            public const string msg_CrossOptions = "CrossOptionClick";
            public const string msg_CrossExecute = "CrossExecutionClick";
            public const string msg_CrossChart = "CrossCreateChartClick";
            public const string msg_GTExecute = "GTExecuteClick";
            public const string msg_GTOptions = "GTOptionsClick";
            public const string msg_SummaryExecute = "SummaryExecuteClick";
            public const string msg_SummaryOptions = "SummaryOptionsClick";
            public const string msg_Save = "SaveFile";
            public const string msg_Close = "CloseFile";
            public const string msg_SaveAs = "SaveAs";
            public const string msg_cancelled = "Cancelled";
            public const string msg_Menu_other = "GotoMenuUpdated"; 
            public const string msg_File_lockRelease = "ReleaseLock";
            public const string msg_File_lockCreate = "CreateLock";

        }


        public static class QCFont
        {
            public const string MS_Gothic = "ＭＳ ゴシック";
            public const string MS_P_Gothic = "ＭＳ Ｐゴシック";
            public const string Segoe_UI = "Segoe UI";
        }

        public static class CheckList
        {
            public const int CheckListEnabled = 1;
        }

        public static class ProcessingType
        {
            public const string CreateNewVariable = "Create New Variable";
            public const string ReviseData = "Revise Data";
            public const string Exclude = "Exclude";
            public const string DeleteCases = "Delete Cases";
            public const string OutputList = "Output List";
        }
        public static class ProcessingMethodDescription
        {
            public const string RECODE = "Reassigns the choices for SA / MA answers.";
            public const string INTEGRATE = "Create a new variable combining multiple source variables.";
            public const string CLASS = "Categorize numeric answers.";
            public const string MCONVERT = "Convert the same choices of multiple variables into one MA variable.";
            public const string COUNT = "Count the number of choices selected in an MA variable.";
            public const string ADD = "Add up multiple SA/MA variables with the same choices into one MA variable.";
            public const string JOINT = "Bind multiple variables into one MA variable.";
            public const string MTOS = "Convert an MA variable into an SA variable.";
            public const string GROUP = "Calculate the maximum, minimum, average and sum value of a numeric variable.";
            public const string COMPUTE = "Perform four arithmetic operations.";
        }

        public static class ProcessingMethod
        {
            public const string RECODE = "RECODE";
            public const string INTEGRATE = "INTEGRATE";
            public const string CLASS = "CLASS";
            public const string MCONVERT = "MCONVERT";
            public const string COUNT = "COUNT";
            public const string ADD = "ADD";
            public const string JOINT = "JOINT";
            public const string MTOS = "MTOS";
            public const string GROUP = "GROUP";
            public const string COMPUTE = "COMPUTE";
        }

        public class RevisionMethod
        {
            public string EQUAL_Desc = LocalResource.REVISION_METHOD_EQUAL_DESC;
            public string ADD_Desc = LocalResource.REVISION_METHOD_ADD_DESC;
            public string MINUS_Desc = LocalResource.REVISION_METHOD_MINUS_DESC;
            public const string SubstituteOperatorADD1 = "ADD1";
            public const string SubstituteOperatorADD2 = "ADD2";
            public const string SubstituteOperatorMINUS1 = "MINUS1";
            public const string SubstituteOperatorMINUS2 = "MINUS2";
            public const string SubstituteOperatorEQUAL = ":=";
        }
        public class Status
        {
            public const string Enable = "Enable";
            public const string Disable = "Disable";
        }
    }

    public class Enums
    {
        public enum DataImportScreenMode
        {
            FileSelection = 0,
            DelimitterSelection = 1,
            FilePreview = 2,
            VennDiagram = 3,
            ColumnSelection = 4,
            MergingSettings = 5
        }

        public enum JoinType
        {
            /// <summary>
            /// Same as regular join. Inner join produces only the set of records that match in both Table A and Table B.
            /// </summary>
            Inner = 0,
            /// <summary>
            /// Same as Left Outer join. Left outer join produces a complete set of records from Table A, with the matching records (where available) in Table B. If there is no match, the right side will contain null.
            /// </summary>
            Left = 1
        }

        public enum ColumnView // View columns from left/right/both tables
        {
            Left = 0,
            Right = 1,
            Both = 2
        }

        public enum MAFormat
        {
            FlagFormat = 0,
            InCellCommaSeperated = 1
        }
        public enum SA_N_Operators
        {
            [Description("=")]
            Equal,
            [Description("<>")]
            NotEqual,
            [Description("<")]
            LessThan,
            [Description(">")]
            GreaterThan,
            [Description("<=")]
            LessThanEqual,
            [Description(">=")]
            GreaterThanEqual

        }
        public enum FA_MA_Operators
        {
            [Description("=")]
            Equal,
            [Description("<>")]
            NotEqual
        }

        public enum MessageType
        {
            Info = 1,
            Warning = 2,
            Error = 3,
            ErrorOk = 4,
            ErrorOkCancel = 5,
            WarningYesNoCancel = 6,
            InfoYesNo = 7
        }
        

    }
   
}
