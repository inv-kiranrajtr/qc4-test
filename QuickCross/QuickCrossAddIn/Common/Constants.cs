using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelAddIn.Common
{
    public static class Constants
    {
        public const string EqEqual = "=";
        public const int MAX_ROW_COUNT = 100000;
        public const string variableModePassword = "P@ssW0rD";
        private static char a = (char)14;
        public static string Password = "MacroMill" + a + "!3";
        internal static class PathName
        {
            internal const string TempPath = "temp\\";
            internal const string FileOpenTemp = "QC4\\File\\";
            internal const string FileSwapData = "QC4\\Swap\\";
        }

        public static class SheetType
        {
            public static string EXT_QC4 = ".qc4";
            public const string sh_QuesSetting = "Sheet11_Qs";
            public const string sh_QuesSettingB = "Question Setting B";
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
            public const string sh_Data_AN = "Multivariate??";
            public const string sh_Data_AN2 = "Multivariate";
            public const string sh_Qinfo = "Qinfo";
            public const string sh_Data01 = "Data01";
            public const string Sh_LDEL = "Sheet18_LDEL";
            public const string SH_FAList = "Sheet15_Fo";
            public const string DataProcess = "Sheet12_Dp";
            public const string GTHidden = "Sheet23_GtS";

        }
        internal static class TemplateFile
        {
            internal const string DB_FIlE = "qc4.db";
        }
        internal static class SheetCodeName
        {
            public const string QuestionSetting = "Sheet11_Qs";
            public const string DataProcess = "Sheet12_Dp";
            public const string CrossTabulation = "Sheet14_Cr";
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
            public const string DataAfterProc = "Sheet03_DataAfterProcess";
            public const string Multivariate01 = "Sheet4";
        }

        public static class Mark
        {
            public const string MarkDiv = "div";
            public const string MarkSep = "sep";
        }

        public static class CR
        {
            public const int CRColVariable = 3;
            public const int CRColNo = 2;
            public const int CRColInputStart = 6;
            public const int CRRowInputStart = 13;
            public const int CRRowLineSettings = 11;
            public const int CRRow2CRVariable = 8;
            public const int CRRow3CRVariable = 5;
            public const int CRRowNarrowVariable = 3;
            public const int CRRowNarrowSettings = 4;
            public const int CRRowGT = 12;

            public const string CRBUTTONCHANGE = "Command_Cr_Change";
            public const string CRBUTTONINSERT = "Command_Cr_Insert";
            public const string CRBUTTONDELETE = "Command_Cr_Delete";
            public const string CRBUTTONCHECK = "Command_Rp_Chek";
            public const string CRBUTTONOPTION = "Command_Cr_Option";
            public const string CRBUTTONEXEC = "Command_Cr_Exec";
            public const string CRBUTTONRPEXEC = "Command_Rp_Exec";

            public const string CRVariableStartAddress = "B14";
            public const string CRNarrowStartAddress = "G3";
            public const string CR3WayCrossStartAddress = "G5";
            public const string CRAxesVariableStartAddress = "G8";
            public const string CRChartStartAddress = "G11";
            public const string CRGTStartAddress = "G12";
            public const string CRRowDivStartAddress = "G13";
        }


        public static class SL
        {
            public const int SLColVariable = 4;
            public const int SLColNo = 2;
            public const int SLColInputStart = 7;
            public const int SLRowInputStart = 7;
            public const int SLRow2CRVariable = 4;
            public const int SLRowOuputEnable = 3;

            public const string SLBUTTONINSERT = "Command_SL_Insert";
            public const string SLBUTTONDELETE = "Command_SL_Delete";
            public const string SLBUTTONCHECK = "Command_SL_Check";
            public const string SLBUTTONEXEC = "Command_SL_Exec";
            public const string SLBUTTONOPT = "Command_SL_Option";

            public const string SLVariableStartAddress = "D8";
            public const string SLOutputStartAddress = "H3";
            public const string SLAxesVariableStartAddress = "H4";
            public const string SLRowDivStartAddress = "H7";

            public const string SLNumericItemList = "MIN,MAX,MED,SUM,SD,AVG";
        }

        public static class AdvancedSettings
        {
            public const string StartCell = "A2";
            public const string F_Gt_GT_AddUp_Check_SigLevel_1 = "F_Gt_GT_AddUp_Check_SigLevel_1";
            public const string F_Gt_GT_AddUp_Check_SigLevel_5 = "F_Gt_GT_AddUp_Check_SigLevel_5";
            public const string F_Gt_GT_AddUp_Check_SigLevel_10 = "F_Gt_GT_AddUp_Check_SigLevel_10";
        }

        public static class GT
        {
            public const int GtColExec = 2;
            public const int GtColChartType = 3;
            public const int GtColGraphType = 5;
            public const int GtColItem = 7;
            public const int GtRowDataStart = 5;
            public const int GtMaxItemNo = 200;
            public const int GtColTest = 4;
            public const int GtColTableHeading = 6;


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
            public const string GTBUTTONEXEC = "Command_Gt_Exec";
            public const string GTBUTTONOPTION = "Command_Gt_Option";
            public const string GTBUTTONAUTO = "Command_Gt_Auto";

            public const string FormControlPrefixAddup = "F_Gt_GT_AddUp_";
            public const string FormControlPostFixPro = "_P";
            public const string FormControlPostFixStd = "_S";
            public const string ComboBoxTypeName = "Combo";

        }

        public static class FA
        {
            public const int FastartColumn = 4;
            public const int FastartRow = 5;


            public const int VarstartColumn = 34;
            public const int VarFastartRow = 5;
            public const int CrtstartColumn = 3;
            public const int OnoffstartColumn = 2;


            public const int FaVariableEnd = 33;
            public const int FaColItem = 4;
            public const int FaColGraphType = 5;
            public const int FaRowDataStart = 5;
            public const int FaColChartType = 3;
            public const string FANarrowStartAddress = "D11";
            public const int FAColInputStart = 3;
            public const int FARowDataStart = 4;
            //public const string FAStartAddress = "D8";
            public const int FaMaxItemNo = 33;
            public const string FAStartAddress = "B5";
            public const int FaAddColItem = 34;
            public const int FaAddMaxItemNo = 63;



            public const string APP_CONFIG_COMMON_RAWDATA_PATH_AP = "Common.Rawdata.Path.AP";
            public const string FAButtonMenu = "Command_Menu";
            public const string FAButtonExec = "Command_Fo_Exec";
            public const string FAButtonOption = "Command_Fo_Option";

        }

        public static class QS
        {
            public const int QsColNew = 2;
            public const int QsColQuestionNumber = 3;
            public const int QsColQuestiontype = 4;
            public const int QsColNumberOfQuestion = 5;
            public const int QsColItem = 6;
            public const int QsColAnswerType = 7;
            public const int QsColCategories = 8;
            public const int QsColWT = 9;
            public const int QsColSortDisplay = 10;
            //public const int QsColSerialNumber = 11;
            public const int QsColTableHeading = 11; //COLUMNCHANGE 12 to 11
            public const int QsColQuestion = 12; //COLUMNCHANGE 13 to 12
            public const int QsColChoiceBegin = 13; //COLUMNCHANGE 14 to 13
            public const int QsColChoiceEnd = 1012; //COLUMNCHANGE 14 to 13
            public const int QsColCount = 1014;// //COLUMNCHANGE 1015 to 1014
            public const int QsColCountBase = 1015;//COLUMNCHANGE 1016 to 1015
            public const int QsColAddSunTotal = 1016;//COLUMNCHANGE 1017 to 1016
            public const int QsColNumberSubTotal = 1017;//COLUMNCHANGE 1018 to 1017

            public const int QsColcriteria1 = 1019;//COLUMNCHANGE 1020 to 1019
            public const int QsColcriteria2 = 1021;//COLUMNCHANGE 1022 to 1021
            public const int QsColcriteria3 = 1023;//COLUMNCHANGE 1024 to 1023
            public const int QsColcriteria4 = 1025;//COLUMNCHANGE 1026 to 1025
            public const int QsColcriteria5 = 1027;//COLUMNCHANGE 1028 to 1027
            public const int QsColcriteria6 = 1029;//COLUMNCHANGE 1030 to 1029
            public const int QsColcriteria7 = 1031;//COLUMNCHANGE 1032 to 1031
            public const int QsColcriteria8 = 1033;//COLUMNCHANGE 1034 to 1033
            public const int QsColcriteria9 = 1035;//COLUMNCHANGE 1036 to 1035
            public const int QsColcriteria10 = 1037;//COLUMNCHANGE 1038 to 1037

            public const int QsColSubtotal1 = 1018;//COLUMNCHANGE 1020 to 1019
            public const int QsColSubtotal2 = 1020;//COLUMNCHANGE 1020 to 1019
            public const int QsColSubtotal3 = 1022;//COLUMNCHANGE 1020 to 1019
            public const int QsColSubtotal4 = 1024;//COLUMNCHANGE 1020 to 1019
            public const int QsColSubtotal5 = 1026;//COLUMNCHANGE 1020 to 1019
            public const int QsColSubtotal6 = 1028;//COLUMNCHANGE 1020 to 1019
            public const int QsColSubtotal7 = 1030;//COLUMNCHANGE 1020 to 1019
            public const int QsColSubtotal8 = 1032;//COLUMNCHANGE 1020 to 1019
            public const int QsColSubtotal9 = 1034;//COLUMNCHANGE 1020 to 1019
            public const int QsColSubtotal10 = 1036;//COLUMNCHANGE 1020 to 1019

            public const int QsRowDataStart = 4;
            public const int QsRowHeader = 3;
            public const int QsColStart = 6;

            public const string QsColCopyBegin = "C";
            public const string QsColCopyEnd = "AMW";//COLUMNCHANGE AMX to AMW

            public const string QsColCategoriesBegin = "M";//COLUMNCHANGE N to M
            public const string QsColCategoriesEnd = "ALX";//COLUMNCHANGE ALY to ALX

            public const string QsColAfterJumpStart = "ALY2"; //COLUMNCHANGE ALZ2 to ALY2
            public const string QsColAfterJumpEnd = "AMW2"; //COLUMNCHANGE AMX2 to AMW2

            public const string QsJumpCell = "J2";

            public const string BufferColumn = "ALY"; //COLUMNCHANGE AMA to ALZ
            public const string Countcolumn = "ALZ"; //COLUMNCHANGE AMA to ALZ
            public const string CountBaseColumn = "AMA";//COLUMNCHANGE AMB to AMA
            public const string AddSubTotalColumn = "AMB";//COLUMNCHANGE AMC to AMB
            public const string NumberOfSubtotalColumn = "AMC";//COLUMNCHANGE AMD to AMC

            public const string QsStartAddress = "F4";
            public const string QsFirstCell = "A4";

            public const string QsButtonMenu = "Command_Menu";
            public const string QSButtonCreate = "Command_Qs_Create";
            public const string QSButtonInsert = "Command_Qs_Insert";
            public const string QSButtonCheck = "CommandButton1";
            public const string QsJump = "\n\n\nJUMP";
            public const string QsContextMenuDBcheck = "DB Check";
            public const string QsContextMenuConfigCheck = "Question Configuration check";


            public const string QsTopBoxName = "グループ化 6";
            public const string QsTopShapeName = "正方形/長方形 4";



        }
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

        public static class AnswerType
        {
            public const string SA = "SA";
            public const string MA = "MA";
            public const string FA = "FA";
            public const string N = "N";
            public const string D = "D";
        }

        public static class QuestionType
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
            public const string EMPTY = "";
        }

        public enum QuestionTypeEnum
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

        public enum AnswerTypeEnum
        {
            SA = 1,
            MA = 2,
            FA = 3,
            N = 4,
            D = 5
        }

        public static class ListSheet
        {
            public static int StartRow = 2;
            public static int SA = 1;
            public static int MA = 2;
            public static int N = 3;
            public static int SAMA = 4;
            public static int SAN = 5;
            public static int MAN = 6;
            public static int SAMAN = 7;
            public static int FA = 8;
            public static int ALL = 9;
            public static int ALLD = 10;
        }

        internal class Setting
        {
            public const string MODE_CELL = "D20";
            public const string MODE_STD = "STD";
            public const string MODE_PRO = "PRO";
            public const string MODE_DATABROWSE = "DATABROWSE";
        }

        public const string ITEMNAME_PATTERN = @"\[([^:\[\]@\|='&\\\!\?<>\*/\r\n]+)\]";

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
            public const string msg_Menu_other = "GotoMenuUpdated";
            public const string msg_File_lockRelease = "ReleaseLock";
            public const string msg_File_lockCreate = "CreateLock";
        }

        internal class QuestionFlag
        {
            public const string New = "New";
            public const string Org = "Org";
            public const string An = "An";
            public const string Imp = "Imp";
        }
    }

}
