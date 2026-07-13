using ExcelAddIn.Sheets;
using QC4Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using Vb = Microsoft.VisualBasic;

namespace Qc4Launcher.Util
{
	class AdvanceSettingHelper
	{
		public const int CrCrossAddUpOptionSettingOutputLateralSIndex = 1;
		public const int CrCrossAddUpOptionSettingOutputVerticalSIndex = 2;
		public const int CrCrossAddUpOptionSettingSummaryDoubleSIndex = 3;
		public const int CrCrossAddUpOptionSettingSummarySingleSIndex = 4;
		public const int CrCrossAddUpOptionSettingSummaryTripleSIndex = 5;
		public const int CrCrossAddUpCheckSettingCrossGroupSIndex = 6;
		public const int CrCrossAddUpCheckSettingRunSIndex = 7;
		public const int CrCrossAddUpCheckSummaryNon1SIndex = 8;
		public const int CrCrossAddUpCheckSummaryNon2SIndex = 9;
		public const int CrCrossAddUpCheckSummaryWeightBackSIndex = 10;
		public const int CrCrossAddUpComboSummaryWeightBackSIndex = 11;
		public const int CrCrossAddUpCheckSummaryValueSumSIndex = 12;
		public const int CrCrossAddUpCheckSummaryValueDenominatorSIndex = 13;
		public const int CrCrossAddUpCheckSummaryValueDeviationSIndex = 14;
		public const int CrCrossAddUpCheckSummaryValueAverageSIndex = 15;
		public const int CrCrossAddUpCheckSummaryValueMaxSIndex = 16;
		public const int CrCrossAddUpCheckSummaryValueMinSIndex = 17;
		public const int CrCrossAddUpCheckSummaryValueMedianSIndex = 18;
		public const int CrCrossAddUpCheckAllBaseSIndex = 19;
		public const int CrCrossAddUpCheckOutputCrossNParSIndex = 20;
		public const int CrCrossAddUpCheckOutputCrossNSIndex = 21;
		public const int CrCrossAddUpCheckOutputCrossParSIndex = 22;
		public const int CrCrossAddUpCheckOutputPageNParSIndex = 23;
		public const int CrCrossAddUpCheckOutputPageParSIndex = 24;
		public const int CrCrossAddUpCheckOutputPageNSIndex = 25;
		public const int CrCrossAddUpComboOutputPageDirectionSIndex = 26;
		public const int CrCrossAddUpComboOutputPagePaperSIndex = 27;
		public const int CrCrossAddUpTextOutputPageZoomSIndex = 28;
		public const int CrCrossAddUpComboOutputPageOutputSIndex = 29;
		public const int CrCrossAddUpCheckOutputPageZoomSIndex = 30;
		public const int CrCrossAddUpCheckReportOptionShowCommentSIndex = 31;
		public const int CrCrossAddUpCheckReportOptionOutputPPTSIndex = 32;
		public const int CrCrossAddUpCheckReportOptionOutputSurveySlipSIndex = 33;
		public const int CrCrossAddUpComboReportOptionSettingTemplateSIndex = 34;
		public const int CrCrossAddUpTextSummaryValueSum1SIndex = 35;
		public const int CrCrossAddUpTextSummaryValueAverage1SIndex = 36;
		public const int CrCrossAddUpTextSummaryValueDeviation1SIndex = 37;
		public const int CrCrossAddUpTextSummaryValueMin1SIndex = 38;
		public const int CrCrossAddUpTextSummaryValueMax1SIndex = 39;
		public const int CrCrossAddUpTextSummaryValueMedian1SIndex = 40;
		public const int CrCrossAddUpCheckOutputPageSettingSIndex = 41;
		public const int CrCrossAddUpCheckSummaryMarkRankingSIndex = 42;
		public const int CrCrossAddUpCheckSummaryMarkRatio1SIndex = 43;
		public const int CrCrossAddUpComboSummaryRateDifference1SIndex = 44;
		public const int CrCrossAddUpComboSummaryRateDifference2SIndex = 45;
		public const int CrCrossAddUpCheckSummaryRateDifference1SIndex = 46;
		public const int CrCrossAddUpCheckSummaryRateDifference2SIndex = 47;
		public const int CrCrossAddUpTextSummaryRateDifference1SIndex = 48;
		public const int CrCrossAddUpTextSummaryRateDifference2SIndex = 49;
		public const int CrCrossAddUpCheckSummarySignificantDiffereceTestSIndex = 50;
		public const int CrCrossAddUpComboSignificantDifferenceTestSIndex = 51;
		public const int CrCrossAddUpCheckPar99SIndex = 52;
		public const int CrCrossAddUpCheckPar95SIndex = 53;
		public const int CrCrossAddUpCheckPar90SIndex = 54;
		public const int CrCrossAddUpTextSummaryMarkNEqualSIndex = 55;
		public const int CrCrossAddUpTextSummaryValueWeightedValue1SIndex = 56;
		public const int CrCrossAddUpTextSummaryValueWeightedAverage1SIndex = 57;
		public const int CrCrossAddUpComboClassifyItemSIndex = 58;
		public const int CrCrossAddUpComboClassifyFolderPathSIndex = 59;
		public const int CrCrossAddUpCheckRefineConditionSIndex = 60;
		public const int CrCrossAddUpComboConditionalItem1SIndex = 61;
		public const int CrCrossAddUpComboConditionalOperator1SIndex = 62;
		public const int CrCrossAddUpComboConditionalValue1SIndex = 63;
		public const int CrCrossAddUpOptionConditionalAnd1SIndex = 64;
		public const int CrCrossAddUpOptionConditionalOr1SIndex = 65;
		public const int CrCrossAddUpComboConditionalItem2SIndex = 66;
		public const int CrCrossAddUpComboConditionalOperator2SIndex = 67;
		public const int CrCrossAddUpComboConditionalValue2SIndex = 68;
		public const int CrCrossAddUpComboConditionalItem3SIndex = 69;
		public const int CrCrossAddUpComboConditionalOperator3SIndex = 70;
		public const int CrCrossAddUpComboConditionalValue3SIndex = 71;
		public const int CrCrossAddUpComboConditionalItem4SIndex = 72;
		public const int CrCrossAddUpComboConditionalOperator4SIndex = 73;
		public const int CrCrossAddUpComboConditionalValue4SIndex = 74;
		public const int CrCrossAddUpOptionConditionalAnd2SIndex = 75;
		public const int CrCrossAddUpOptionConditionalOr2SIndex = 76;
		public const int CrCrossAddUpOptionConditionalOr3SIndex = 77;
		public const int CrCrossAddUpOptionConditionalAnd3SIndex = 78;
		public const int CrCrossAddUpOptionConditionalAnd4SIndex = 79;
		public const int CrCrossAddUpComboConditionalItem5SIndex = 80;
		public const int CrCrossAddUpComboConditionalOperator5SIndex = 81;
		public const int CrCrossAddUpComboConditionalValue5SIndex = 82;
		public const int CrCrossAddUpOptionConditionalOr4SIndex = 83;
		public const int CrCrossAddUpCheckReportOptionSettingTemplateSIndex = 84;
		public const int CrCrossAddUpOptionOutputSheetTypeOneSIndex = 85;
		public const int CrCrossAddUpOptionOutputSheetTypePluralSIndex = 86;
		public const int CrCrossAddUpCheckOutputCrossNParOneSIndex = 87;
		public const int CrCrossAddUpCheckOutputCrossNOneSIndex = 88;
		public const int CrCrossAddUpCheckOutputCrossParOneSIndex = 89;
		public const int CrCrossAddUpCheckOutputPageNParOneSIndex = 90;
		public const int CrCrossAddUpCheckOutputPageParOneSIndex = 91;
		public const int CrCrossAddUpCheckOutputPageNOneSIndex = 92;
		public const int CrCrossAddUpComboOutputPageDirectionOneSIndex = 93;
		public const int CrCrossAddUpComboOutputPagePaperOneSIndex = 94;
		public const int CrCrossAddUpTextOutputPageZoomOneSIndex = 95;
		public const int CrCrossAddUpComboOutputPageOutputOneSIndex = 96;
		public const int CrCrossAddUpCheckOutputPageZoomOneSIndex = 97;
		public const int CrCrossAddUpCheckOutputPageSettingOneSIndex = 98;
		public const int CrCrossAddUpCheckSummaryFormatV1SIndex = 99;
		public const int CrCrossAddUpCheckSummaryFormatV2SIndex = 100;
		public const int CrCrossAddUpCheckSummaryFormatL1SIndex = 101;
		public const int CrCrossAddUpCheckSummaryFormatL2SIndex = 102;
		public const int CrCrossAddUpTextSummaryValueMACountAverage1SIndex = 103;
		public const int CrCrossAddUpOutputUnweightbackedTotalCheckSIndex = 104;
		public const int CrCrossAddUpUnweightbackedBaseCheckSIndex = 105;
		public const int CrCrossAddUpOutputUnweightbackedTotalSIndex = 106;
		public const int CrCrossAddUpUnweightbackedBaseSIndex = 107;
		public const int DpProcessCheckCheckListSIndex = 108;
		public const int DpProcessCheckEditSIndex = 109;
		public const int GtGTAddUpCheckRefineConditionSIndex = 110;
		public const int GtGTAddUpComboOutputPagePaperSIndex = 111;
		public const int GtGTAddUpCheckOutputPageSettingSIndex = 112;
		public const int GtGTAddUpTextOutputPageZoomSIndex = 113;
		public const int GtGTAddUpCheckOutputSortSIndex = 114;
		public const int GtGTAddUpComboOutputPageOutputSIndex = 115;
		public const int GtGTAddUpComboOutputPageDirectionSIndex = 116;
		public const int GtGTAddUpCheckOutputPageZoomSIndex = 117;
		public const int GtGTAddUpComboClassifyItemSIndex = 118;
		public const int GtGTAddUpCheckGraphOutputSIndex = 119;
		public const int GtGTAddUpCheckRateSIndex = 120;
		public const int GtGTAddUpCheckAdvantage90SIndex = 121;
		public const int GtGTAddUpCheckAdvantage95SIndex = 122;
		public const int GtGTAddUpCheckAdvantage99SIndex = 123;
		public const int GtGTAddUpCheckSummaryValueSumSIndex = 124;
		public const int GtGTAddUpCheckSummaryValueDenominatorSIndex = 125;
		public const int GtGTAddUpCheckSummaryValueDeviationSIndex = 126;
		public const int GtGTAddUpCheckSummaryValueAverageSIndex = 127;
		public const int GtGTAddUpCheckSummaryValueMaxSIndex = 128;
		public const int GtGTAddUpCheckSummaryValueMinSIndex = 129;
		public const int GtGTAddUpCheckSummaryValueMedianSIndex = 130;
		public const int GtGTAddUpTextSummaryValueSum1SIndex = 131;
		public const int GtGTAddUpTextSummaryValueAverage1SIndex = 132;
		public const int GtGTAddUpTextSummaryValueDeviation1SIndex = 133;
		public const int GtGTAddUpTextSummaryValueMin1SIndex = 134;
		public const int GtGTAddUpTextSummaryValueMax1SIndex = 135;
		public const int GtGTAddUpTextSummaryValueMedian1SIndex = 136;
		public const int GtGTAddUpComboConditionalItem1SIndex = 137;
		public const int GtGTAddUpComboConditionalOperator1SIndex = 138;
		public const int GtGTAddUpComboConditionalValue1SIndex = 139;
		public const int GtGTAddUpOptionConditionalAnd1SIndex = 140;
		public const int GtGTAddUpOptionConditionalOr1SIndex = 141;
		public const int GtGTAddUpComboConditionalItem2SIndex = 142;
		public const int GtGTAddUpComboConditionalOperator2SIndex = 143;
		public const int GtGTAddUpComboConditionalValue2SIndex = 144;
		public const int GtGTAddUpComboConditionalItem3SIndex = 145;
		public const int GtGTAddUpComboConditionalOperator3SIndex = 146;
		public const int GtGTAddUpComboConditionalValue3SIndex = 147;
		public const int GtGTAddUpComboConditionalItem4SIndex = 148;
		public const int GtGTAddUpComboConditionalOperator4SIndex = 149;
		public const int GtGTAddUpComboConditionalValue4SIndex = 150;
		public const int GtGTAddUpOptionConditionalAnd2SIndex = 151;
		public const int GtGTAddUpOptionConditionalOr2SIndex = 152;
		public const int GtGTAddUpOptionConditionalOr3SIndex = 153;
		public const int GtGTAddUpOptionConditionalAnd3SIndex = 154;
		public const int GtGTAddUpOptionConditionalAnd4SIndex = 155;
		public const int GtGTAddUpComboConditionalItem5SIndex = 156;
		public const int GtGTAddUpComboConditionalOperator5SIndex = 157;
		public const int GtGTAddUpComboConditionalValue5SIndex = 158;
		public const int GtGTAddUpOptionConditionalOr4SIndex = 159;
		public const int GtGTAddUpCheckSummaryWeightBackSIndex = 160;
		public const int GtGTAddUpComboSummaryWeightBackSIndex = 161;
		public const int GtGTAddUpCheckAllBaseSIndex = 162;
		public const int GtGTAddUpTextSummaryValueWeightedValue1SIndex = 163;
		public const int GtGTAddUpTextSummaryValueWeightedAverage1SIndex = 164;
		public const int GtGTAddUpTextInputRateSIndex = 165;
		public const int GtGTAddUpComboClassifyFolderPathSIndex = 166;
		public const int GtGTAddUpTextSummaryValueMACountAverage1SIndex = 167;
		public const int GtGTAddUpOutputUnweightbackedTotalCheckSIndex = 168;
		public const int GtGTAddUpOutputUnweightbackedTotalSIndex = 169;
		public const int DoOutputTextChangeFileIndex = 170;
		public const int DoOutputComboOutputFileTypeIndex = 171;
		public const int DoOutputComboNonAnserIndex = 172;
		public const int DoOutputComboNonApplyingIndex = 173;
		public const int DoOutputTextOutputPathIndex = 174;
		public const int DoOutputCheckVerticalIndex = 175;
		public const int DoOutputOptionDirectIndex = 176;
		public const int DoOutputOptionZeroIndex = 177;
		public const int DoOutputOptionDKIndex = 178;
		public const int DoOutputOptionOffZeroIndex = 179;
		public const int DoOutputOptionOffDKIndex = 180;
		public const int DoOutputCheckSplitIndex = 181;
		public const int DoOutputComboOutputTypeIndex = 182;
		public const int DoOutputCheckRefineConditionIndex = 183;
		public const int DoOutputComboConditionalItem1Index = 184;
		public const int DoOutputComboConditionalOperator1Index = 185;
		public const int DoOutputComboConditionalValue1Index = 186;
		public const int DoOutputOptionConditionalAnd1Index = 187;
		public const int DoOutputOptionConditionalOr1Index = 188;
		public const int DoOutputComboConditionalItem2Index = 189;
		public const int DoOutputComboConditionalOperator2Index = 190;
		public const int DoOutputComboConditionalValue2Index = 191;
		public const int DoOutputComboConditionalItem3Index = 192;
		public const int DoOutputComboConditionalOperator3Index = 193;
		public const int DoOutputComboConditionalValue3Index = 194;
		public const int DoOutputComboConditionalItem4Index = 195;
		public const int DoOutputComboConditionalOperator4Index = 196;
		public const int DoOutputComboConditionalValue4Index = 197;
		public const int DoOutputOptionConditionalAnd2Index = 198;
		public const int DoOutputOptionConditionalOr2Index = 199;
		public const int DoOutputOptionConditionalOr3Index = 200;
		public const int DoOutputOptionConditionalAnd3Index = 201;
		public const int DoOutputOptionConditionalAnd4Index = 202;
		public const int DoOutputComboConditionalItem5Index = 203;
		public const int DoOutputComboConditionalOperator5Index = 204;
		public const int DoOutputComboConditionalValue5Index = 205;
		public const int DoOutputOptionConditionalOr4Index = 206;
		public const int DoOutputComboClassifyItemIndex = 207;
		public const int DoOutputComboClassifyFolderPathIndex = 208;
		public const int DoOutputComboMALabelIndex = 209;
		public const int DoOutputCheckUnicodeIndex = 210;
		public const int CrCrossAddUpOptionSettingOutputLateralPIndex = 211;
		public const int CrCrossAddUpOptionSettingOutputVerticalPIndex = 212;
		public const int CrCrossAddUpOptionSettingSummaryDoublePIndex = 213;
		public const int CrCrossAddUpOptionSettingSummarySinglePIndex = 214;
		public const int CrCrossAddUpOptionSettingSummaryTriplePIndex = 215;
		public const int CrCrossAddUpCheckSettingCrossGroupPIndex = 216;
		public const int CrCrossAddUpCheckSettingRunPIndex = 217;
		public const int CrCrossAddUpCheckSummaryNon1PIndex = 218;
		public const int CrCrossAddUpCheckSummaryNon2PIndex = 219;
		public const int CrCrossAddUpCheckSummaryWeightBackPIndex = 220;
		public const int CrCrossAddUpComboSummaryWeightBackPIndex = 221;
		public const int CrCrossAddUpCheckSummaryValueSumPIndex = 222;
		public const int CrCrossAddUpCheckSummaryValueDenominatorPIndex = 223;
		public const int CrCrossAddUpCheckSummaryValueDeviationPIndex = 224;
		public const int CrCrossAddUpCheckSummaryValueAveragePIndex = 225;
		public const int CrCrossAddUpCheckSummaryValueMaxPIndex = 226;
		public const int CrCrossAddUpCheckSummaryValueMinPIndex = 227;
		public const int CrCrossAddUpCheckSummaryValueMedianPIndex = 228;
		public const int CrCrossAddUpCheckAllBasePIndex = 229;
		public const int CrCrossAddUpCheckOutputCrossNParPIndex = 230;
		public const int CrCrossAddUpCheckOutputCrossNPIndex = 231;
		public const int CrCrossAddUpCheckOutputCrossParPIndex = 232;
		public const int CrCrossAddUpCheckOutputPageNParPIndex = 233;
		public const int CrCrossAddUpCheckOutputPageParPIndex = 234;
		public const int CrCrossAddUpCheckOutputPageNPIndex = 235;
		public const int CrCrossAddUpComboOutputPageDirectionPIndex = 236;
		public const int CrCrossAddUpComboOutputPagePaperPIndex = 237;
		public const int CrCrossAddUpTextOutputPageZoomPIndex = 238;
		public const int CrCrossAddUpComboOutputPageOutputPIndex = 239;
		public const int CrCrossAddUpCheckOutputPageZoomPIndex = 240;
		public const int CrCrossAddUpCheckReportOptionShowCommentPIndex = 241;
		public const int CrCrossAddUpCheckReportOptionOutputPPTPIndex = 242;
		public const int CrCrossAddUpCheckReportOptionOutputSurveySlipPIndex = 243;
		public const int CrCrossAddUpComboReportOptionSettingTemplatePIndex = 244;
		public const int CrCrossAddUpTextSummaryValueSum1PIndex = 245;
		public const int CrCrossAddUpTextSummaryValueAverage1PIndex = 246;
		public const int CrCrossAddUpTextSummaryValueDeviation1PIndex = 247;
		public const int CrCrossAddUpTextSummaryValueMin1PIndex = 248;
		public const int CrCrossAddUpTextSummaryValueMax1PIndex = 249;
		public const int CrCrossAddUpTextSummaryValueMedian1PIndex = 250;
		public const int CrCrossAddUpCheckOutputPageSettingPIndex = 251;
		public const int CrCrossAddUpCheckSummaryMarkRankingPIndex = 252;
		public const int CrCrossAddUpCheckSummaryMarkRatio1PIndex = 253;
		public const int CrCrossAddUpComboSummaryRateDifference1PIndex = 254;
		public const int CrCrossAddUpComboSummaryRateDifference2PIndex = 255;
		public const int CrCrossAddUpCheckSummaryRateDifference1PIndex = 256;
		public const int CrCrossAddUpCheckSummaryRateDifference2PIndex = 257;
		public const int CrCrossAddUpTextSummaryRateDifference1PIndex = 258;
		public const int CrCrossAddUpTextSummaryRateDifference2PIndex = 259;
		public const int CrCrossAddUpCheckSummarySignificantDiffereceTestPIndex = 260;
		public const int CrCrossAddUpComboSignificantDifferenceTestPIndex = 261;
		public const int CrCrossAddUpCheckPar99PIndex = 262;
		public const int CrCrossAddUpCheckPar95PIndex = 263;
		public const int CrCrossAddUpCheckPar90PIndex = 264;
		public const int CrCrossAddUpTextSummaryMarkNEqualPIndex = 265;
		public const int CrCrossAddUpTextSummaryValueWeightedValue1PIndex = 266;
		public const int CrCrossAddUpTextSummaryValueWeightedAverage1PIndex = 267;
		public const int CrCrossAddUpComboClassifyItemPIndex = 268;
		public const int CrCrossAddUpComboClassifyFolderPathPIndex = 269;
		public const int CrCrossAddUpCheckRefineConditionPIndex = 270;
		public const int CrCrossAddUpComboConditionalItem1PIndex = 271;
		public const int CrCrossAddUpComboConditionalOperator1PIndex = 272;
		public const int CrCrossAddUpComboConditionalValue1PIndex = 273;
		public const int CrCrossAddUpOptionConditionalAnd1PIndex = 274;
		public const int CrCrossAddUpOptionConditionalOr1PIndex = 275;
		public const int CrCrossAddUpComboConditionalItem2PIndex = 276;
		public const int CrCrossAddUpComboConditionalOperator2PIndex = 277;
		public const int CrCrossAddUpComboConditionalValue2PIndex = 278;
		public const int CrCrossAddUpComboConditionalItem3PIndex = 279;
		public const int CrCrossAddUpComboConditionalOperator3PIndex = 280;
		public const int CrCrossAddUpComboConditionalValue3PIndex = 281;
		public const int CrCrossAddUpComboConditionalItem4PIndex = 282;
		public const int CrCrossAddUpComboConditionalOperator4PIndex = 283;
		public const int CrCrossAddUpComboConditionalValue4PIndex = 284;
		public const int CrCrossAddUpOptionConditionalAnd2PIndex = 285;
		public const int CrCrossAddUpOptionConditionalOr2PIndex = 286;
		public const int CrCrossAddUpOptionConditionalOr3PIndex = 287;
		public const int CrCrossAddUpOptionConditionalAnd3PIndex = 288;
		public const int CrCrossAddUpOptionConditionalAnd4PIndex = 289;
		public const int CrCrossAddUpComboConditionalItem5PIndex = 290;
		public const int CrCrossAddUpComboConditionalOperator5PIndex = 291;
		public const int CrCrossAddUpComboConditionalValue5PIndex = 292;
		public const int CrCrossAddUpOptionConditionalOr4PIndex = 293;
		public const int CrCrossAddUpCheckReportOptionSettingTemplatePIndex = 294;
		public const int CrCrossAddUpOptionOutputSheetTypeOnePIndex = 295;
		public const int CrCrossAddUpOptionOutputSheetTypePluralPIndex = 296;
		public const int CrCrossAddUpCheckOutputCrossNParOnePIndex = 297;
		public const int CrCrossAddUpCheckOutputCrossNOnePIndex = 298;
		public const int CrCrossAddUpCheckOutputCrossParOnePIndex = 299;
		public const int CrCrossAddUpCheckOutputPageNParOnePIndex = 300;
		public const int CrCrossAddUpCheckOutputPageParOnePIndex = 301;
		public const int CrCrossAddUpCheckOutputPageNOnePIndex = 302;
		public const int CrCrossAddUpComboOutputPageDirectionOnePIndex = 303;
		public const int CrCrossAddUpComboOutputPagePaperOnePIndex = 304;
		public const int CrCrossAddUpTextOutputPageZoomOnePIndex = 305;
		public const int CrCrossAddUpComboOutputPageOutputOnePIndex = 306;
		public const int CrCrossAddUpCheckOutputPageZoomOnePIndex = 307;
		public const int CrCrossAddUpCheckOutputPageSettingOnePIndex = 308;
		public const int CrCrossAddUpCheckSummaryFormatV1PIndex = 309;
		public const int CrCrossAddUpCheckSummaryFormatV2PIndex = 310;
		public const int CrCrossAddUpCheckSummaryFormatL1PIndex = 311;
		public const int CrCrossAddUpCheckSummaryFormatL2PIndex = 312;
		public const int CrCrossAddUpTextSummaryValueMACountAverage1PIndex = 313;
		public const int CrCrossAddUpOutputUnweightbackedTotalCheckPIndex = 314;
		public const int CrCrossAddUpUnweightbackedBaseCheckPIndex = 315;
		public const int CrCrossAddUpOutputUnweightbackedTotalPIndex = 316;
		public const int CrCrossAddUpUnweightbackedBasePIndex = 317;
		public const int FoFAListCheckOutputPageSettingPIndex = 318;
		public const int FoFAListCheckOptionSortPIndex = 319;
		public const int FoFAListComboOutputPagePaperPIndex = 320;
		public const int FoFAListTextOutputPageZoomPIndex = 321;
		public const int FoFAListComboOutputPageOutputPIndex = 322;
		public const int FoFAListComboOutputPageDirectionPIndex = 323;
		public const int FoFAListCheckOutputPageZoomPIndex = 324;
		public const int FoFAListCheckRankingListUpperRankPIndex = 325;
		public const int FoFAListComboRankingListUpperRankPIndex = 326;

		private const string DoSelectedVariableStartCell = "C2";
		private const string SettingStartCell = "A2";
		private const string SettingEndCell = "B327";
		private Excel.Worksheet SettingSheet;
		private static Object[,] SettingList { get; set; }

		internal AdvanceSettingHelper(Excel.Workbook workbook)
		{
			SettingSheet = ExcelUtil.GetWorkSheetByCodeName(workbook, Constants.SheetCodeName.DetailsSetting);
			SettingList = GetSettingList();
		}

		
		internal List<QuestionSettings> GetSelectedVariables(string[] variables, Constants.DataOutput.FileType fileType= Constants.DataOutput.FileType.NONE)
		{
			List<QuestionSettings> list = new List<QuestionSettings>();
            QuestionSettings item = new QuestionSettings();
            if (variables != null)
            {
                foreach (string variable in variables)
                {
                    if (Definiotion.VariableDictionary.ContainsKey(variable))
                    {
                        switch (fileType)
                        {
                            case Constants.DataOutput.FileType.Excel2007:
                            case Constants.DataOutput.FileType.QC3:
                            case Constants.DataOutput.FileType.QC4:
								item = CloneQuestion(Definiotion.VariableDictionary[variable].Clone());
								list.Add(item);
                                break;
                            case Constants.DataOutput.FileType.SPSS:
                                item = CloneQuestion(Definiotion.VariableDictionary[variable].Clone());
                                item.TableHeading = ReplaceCRLF(item.TableHeading, true);
                                item.Question = ReplaceCRLF(item.Question, true);
                                if (item.Choices.Count > 0)
                                {
                                    for (int j = 0; j < item.Choices.Count; j++)
                                    {
                                        item.Choices[j] = ReplaceCRLF(item.Choices[j], true);
                                    }
                                }
                                list.Add(item);
                                break;
                            case Constants.DataOutput.FileType.QLayout:
                            case Constants.DataOutput.FileType.TAB:
                            //case Constants.DataOutput.FileType.CSV:
                                item = CloneQuestion(Definiotion.VariableDictionary[variable].Clone());
                                item.TableHeading = item.TableHeading.Replace("\n", "<LF>");
                                item.TableHeading = item.TableHeading.Replace("\r", "");
                                item.Question = item.Question.Replace("\n", "<LF>");
                                item.Question = item.Question.Replace("\r", "");
                                if (item.Choices.Count > 0)
                                {
									for (int j = 0; j < item.Choices.Count; j++)
									{
										item.Choices[j] = item.Choices[j].Replace("\n", "<LF>");
										item.Choices[j] = item.Choices[j].Replace("\r", "");
									}
                                }
                                list.Add(item);
                                break;
                            default:
                                item = CloneQuestion(Definiotion.VariableDictionary[variable].Clone());
                                item.TableHeading = item.TableHeading.Replace("\n", " ");
                                item.TableHeading = item.TableHeading.Replace("\r", "");
                                item.Question = item.Question.Replace("\n", " ");
                                item.Question = item.Question.Replace("\r", "");
                                if (item.Choices.Count > 0)
                                {
                                    for (int j = 0; j < item.Choices.Count; j++)
                                    {
                                        item.Choices[j] = item.Choices[j].Replace("\n", " ");
                                        item.Choices[j] = item.Choices[j].Replace("\r", "");
                                    }
                                }
                                list.Add(item);
                                break;
                        }
                    }
                }
                return list;
            }

			//TO Delete
			Excel.Range start = SettingSheet.Range[DoSelectedVariableStartCell];
			Excel.Range end = ExcelUtil.EndxlUp(start);
			Excel.Range total = SettingSheet.get_Range(start, end);
			if (total.Rows.Count == 1)
			{
				if (Definiotion.VariableDictionary.ContainsKey("SAMPLEID"))
				{
					list.Add(Definiotion.VariableDictionary["SAMPLEID"].Clone());
					return list;
				}
			}
			foreach (Object obj in total.Value2)
			{
				if (null != obj)
				{
					string variable = obj.ToString();
					if (Definiotion.VariableDictionary.ContainsKey(variable))
					{
						list.Add(Definiotion.VariableDictionary[variable].Clone());
					}
				}
			}
			return list;
		}

		private QuestionSettings CloneQuestion(QuestionSettings questionSettings)
		{
			QuestionSettings qs = new QuestionSettings();
			qs.CategoryCount = questionSettings.CategoryCount;
			qs.AddSubTotal = questionSettings.AddSubTotal;
			qs.AnswerType = questionSettings.AnswerType;
			qs.AnswerTypeBefore = questionSettings.AnswerTypeBefore;
			qs.CategoryCountBefore = questionSettings.CategoryCountBefore;
			List<string> choce = new List<string>();
			for (int i = 0; i < questionSettings.Choices.Count; i++)
				choce.Add(RemoveFirstSingleQuote(questionSettings.Choices[i]));
			qs.Choices = choce;
			qs.Count = questionSettings.Count;
			qs.CountBase = questionSettings.CountBase;
			qs.Id = questionSettings.Id;
			qs.IsFound = questionSettings.IsFound;
			qs.IsNew = questionSettings.IsNew;
			qs.ItemId = questionSettings.ItemId;
			qs.Question = RemoveFirstSingleQuote(questionSettings.Question);
			qs.QuestionCount = questionSettings.QuestionCount;
			qs.QuestionFlag = questionSettings.QuestionFlag;
			qs.QuestionFlagUpdated = questionSettings.QuestionFlagUpdated;
			qs.QuestionNumber = RemoveFirstSingleQuote(questionSettings.QuestionNumber);
			qs.QuestionType = questionSettings.QuestionType;
			qs.RowName = questionSettings.RowName;
			qs.RowNumber = questionSettings.RowNumber;
			qs.Score = questionSettings.Score;
			qs.SeriallNumber = questionSettings.SeriallNumber;
			qs.Sort = questionSettings.Sort;
			qs.SubTotalCount = questionSettings.SubTotalCount;
			List<QuestionSettings.SubTotal> sub = new List<QuestionSettings.SubTotal>();
			if (questionSettings.SubTotals != null)
			{
				for (int i = 0; i < questionSettings.SubTotals.Count; i++)
				{
					QuestionSettings.SubTotal su = new QuestionSettings.SubTotal(questionSettings.SubTotals[i].Subtotal, questionSettings.SubTotals[i].Criteria);
					sub.Add(su);
				}
			}
			qs.SubTotals = sub;
			qs.TableHeading = RemoveFirstSingleQuote(questionSettings.TableHeading);
			qs.Variable = questionSettings.Variable;
			qs.VariableBefore = questionSettings.VariableBefore;
			return qs;
		}

		private string RemoveFirstSingleQuote(string itm)
		{
			if (itm.Length > 0 && itm[0] == '\'')
				itm = itm.Remove(0, 1);
			return itm;
		}

		private string ReplaceCRLF(string tableHeading, bool lastStrCheck)
        {
            String retVal = "";
            retVal = tableHeading;
            retVal = retVal.Replace('\t', ' ');
            retVal = retVal.Replace("\r", "");
            retVal = retVal.Replace('\n', ' ');
            retVal = retVal.Replace("\r\n", " ");
            retVal = retVal.Trim();
            retVal = retVal.Replace("【　　　】", "【 】");
            retVal = System.Text.RegularExpressions.Regex.Replace(retVal, @"\s+(?=[^[\]]*\])", " ");
            if (lastStrCheck == true)
            {
                if(retVal.Length>2 && (retVal.Substring(retVal.Length - 3, 3) == "【 】" || retVal.Substring(retVal.Length-3, 3) == "【　】" || retVal.Substring(retVal.Length - 3, 3) == "[ ]") )
                {
                    retVal = retVal.Substring(0, retVal.Length - 3);
                }
                //retVal = retVal.Replace("【　　　】", " ");
                //retVal = retVal.Replace("[ ]", " ");
                retVal = retVal.Trim();
            }
            return retVal;
        }

        internal Object[,] GetSettingList()
		{
			Excel.Range start = SettingSheet.Range[SettingStartCell];
			Excel.Range end = SettingSheet.Range[SettingEndCell];
			Excel.Range total = SettingSheet.get_Range(start, end.Next);
			
			return total.Value;
		}

		internal string GetSettingValue(int index)
		{
			Object obj = SettingList[index, 1];
			if (obj == null)
			{
				return "";
			}
			return obj.ToString();
		}
	}
}
