using ExcelAddIn;
using FilterSettingsView;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using static FilterSettingsView.FilterSettingsClass;
using excel = Microsoft.Office.Interop.Excel;

namespace Qc4Launcher.Forms.GrossTabulationSetting.Common
{
    public class CommonFunc
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static ObservableCollection<string> GetChartByAnsType(string type)
        {
            string[] types = null;
            if (LocalResource.FOR_SA == type)
            {
                types = LocalResource.GTSAGraph.Split(',');
                return GetGraph(types);
            }
            else if (LocalResource.FOR_MA == type)
            {
                types = LocalResource.GTMAGraph.Split(',');
                return GetGraph(types);
            }
            else if (LocalResource.FOR_MTM == type)
            {
                types = LocalResource.GTMTMGraph.Split(',');
                return GetGraph(types);
            }
            else if (LocalResource.FOR_MTS == type)
            {
                types = LocalResource.GTMTSGraph.Split(',');
                return GetGraph(types);
            }
            else if (LocalResource.FOR_RAT == type)
            {
                types = LocalResource.GTRATGraph.Split(',');
                return GetGraph(types);
            }
            else if (LocalResource.FOR_RNK == type)
            {
                types = LocalResource.GTRNKGraph.Split(',');
                return GetGraph(types);
            }
            return null;
        }

        private static ObservableCollection<string> GetGraph(string[] types)
        {
            ObservableCollection<string> grpah = new ObservableCollection<string>();
            for (int i = 0; i < types.Length; i++)
                grpah.Add(types[i]);
            return grpah;
        }

        private static Dictionary<string, string> GetTableType()
        {
            Dictionary<string, string> tableType = new Dictionary<string, string>()
            {
                {"SA","GT-SA"},
                {"MA","GT-MA"},
                {"N","GT-N"},
                {"FA",""},
                {"SAR","GT-SA"},
                {"SAS","GT-SA"},
                {"SAP","GT-SA"},
                {"MAC","GT-MA"},
                {"MTS","GT-MTS"},
                {"MTM","GT-MTM"},
                {"MTT","GT-MTS"},
                {"RAT","GT-RAT"},
                {"RNK","GT-RNK"},
                {"FAS",""},
                {"FASN","GT-MTN"},
                {"NUM","GT-N"},
                {"FAL",""}
            };
            return tableType;
        }
        internal static ObservableCollection<DataGT> LoadQSItemsByType(string[] type, excel.Workbook Workbook)
        {
            QC4Common.Util.FormUtil formUtil = new QC4Common.Util.FormUtil();
            ObservableCollection<DataGT> qsItems = new ObservableCollection<DataGT>();
            try
            {
                excel.Worksheet qsSheet = Util.ExcelUtil.GetWorkSheetByCodeName(Workbook, Util.Constants.SheetCodeName.QuestionSetting);
                excel.Range qsStart = qsSheet.Cells[ExcelAddIn.Common.Constants.QS.QsRowDataStart, ExcelAddIn.Common.Constants.QS.QsColStart];
                excel.Range qsEnd = Util.ExcelUtil.EndxlUp(qsStart);
                excel.Range qsTotal = qsSheet.get_Range(qsStart.Offset[0, -2], qsEnd.Offset[0, 7 + 1000]);
                Object[,] objAry = qsTotal.Value2;
                int qIndex = 0;
                int qsNumIndx = 0;
                int qsNum = 0;
                string graphType = "";
                Dictionary<string, string> tableType = GetTableType();
                for (int i = 1; i <= objAry.GetLength(0); i++)
                {
                    int catCount = objAry[i, 5] == null || objAry[i, 5].ToString() == "" ? 0 : Convert.ToInt32(objAry[i, 5].ToString());
                    bool isSetQSNum = false;
                    if (objAry[i, 2] != null && objAry[i, 2].ToString() != "")
                    {
                        qsNum = Convert.ToInt32(objAry[i, 2].ToString());
                        isSetQSNum = true;
                    }
                    for (int ty = 0; ty < type.Length; ty++)
                    {
                        if (type[ty] == objAry[i, 4].ToString())
                        {
                            string QsTypePlusCatCount = objAry[i, 4].ToString();
                            QsTypePlusCatCount += catCount > 0 ? "/" + catCount.ToString() : "";

                            string tbType;
                            if (objAry[i, 1] == null || objAry[i, 1].ToString() == "")
                            {
                                tableType.TryGetValue(objAry[i, 4].ToString(), out tbType);
                            }
                            else
                            {
                                tableType.TryGetValue(objAry[i, 1].ToString(), out tbType);
                                if (objAry[i, 1].ToString() == "FAS" && objAry[i, 4].ToString() == "N")
                                {
                                    tbType = isSetQSNum ? "GT-MTN" : "GT-N";
                                }
                            }
                            graphType = GetGraphList(tbType);

                            qsItems.Add(new DataGT
                            {
                                QuestionVariable = objAry[i, 3].ToString(),
                                QSType = isSetQSNum ? tbType : qsNum == 0 ? tbType : "",
                                QSHeading = formUtil.EscapeCRLF(objAry[i, 8] == null ? "" : objAry[i, 8].ToString()),
                                Question = objAry[i, 9] == null ? "" : formUtil.EscapeCRLF(objAry[i, 9].ToString()),
                                QuestionIndex = qIndex,
                                CategoryCount = catCount,
                                QsTypePlusCatCount = QsTypePlusCatCount,
                                Variable = objAry[i, 3].ToString(),
                                QSTypeShort = objAry[i, 4].ToString(),
                                QsNumberIndex = qsNumIndx,
                                QuestionNumber = isSetQSNum ? qsNum : 0,
                                Graph = graphType == null ? "" : graphType.Length > 0 ? graphType.Split(',')[0] : ""
                            });
                            qIndex++;
                            if (qsNum != 0)
                            {
                                qsNum--;
                            }
                            if (qsNum == 0)
                                qsNumIndx++;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
            return qsItems;
        }

        public static ObservableCollection<DataGT> LoadQSItemsBySameTypeAndChoice(DataGT item, excel.Workbook Workbook)
        {
            QC4Common.Util.FormUtil formUtil = new QC4Common.Util.FormUtil();
            ObservableCollection<DataGT> qsItems = new ObservableCollection<DataGT>();
            try
            {
                excel.Worksheet qsSheet = Util.ExcelUtil.GetWorkSheetByCodeName(Workbook, Util.Constants.SheetCodeName.QuestionSetting);
                excel.Range qsStart = qsSheet.Cells[ExcelAddIn.Common.Constants.QS.QsRowDataStart, ExcelAddIn.Common.Constants.QS.QsColStart];
                excel.Range qsEnd = Util.ExcelUtil.EndxlUp(qsStart);
                excel.Range qsTotal = qsSheet.get_Range(qsStart.Offset[0, -2], qsEnd.Offset[0, 7 + 1000]);
                Object[,] objAry = qsTotal.Value2;
                int qIndex = 0;
                int qsNumIndx = 0;
                int qsNum = 0;
                string graphType = "";
                Dictionary<string, string> tableType = GetTableType();
                bool isSetQSNum = false;
                for (int i = 1; i <= objAry.GetLength(0); i++)
                {
                    int catCount = objAry[i, 5] == null || objAry[i, 5].ToString() == "" ? 0 : Convert.ToInt32(objAry[i, 5].ToString());
                    if (objAry[i, 2] != null && objAry[i, 2].ToString() != "")
                    {
                        qsNum = Convert.ToInt32(objAry[i, 2].ToString());
                        isSetQSNum = true;
                    }
                    if (item.QSTypeShort == objAry[i, 4].ToString() && item.CategoryCount == catCount && IsMatchChoice(objAry, i, item.Variable))
                    {
                        string QsTypePlusCatCount = objAry[i, 4].ToString();
                        QsTypePlusCatCount += catCount > 0 ? "/" + catCount.ToString() : "";

                        string tbType;
                        if (objAry[i, 1] == null || objAry[i, 1].ToString() == "")
                        {
                            tableType.TryGetValue(objAry[i, 4].ToString(), out tbType);
                        }
                        else
                        {
                            tableType.TryGetValue(objAry[i, 1].ToString(), out tbType);
                            if (objAry[i, 1].ToString() == "FAS" && objAry[i, 4].ToString() == "N")
                            {
                                tbType = isSetQSNum ? "GT-MTN" : "GT-N";
                            }
                        }
                        graphType = GetGraphList(tbType);

                        qsItems.Add(new DataGT
                        {
                            QuestionVariable = objAry[i, 3].ToString(),
                            QSType = isSetQSNum ? tbType : qsNum == 0 ? tbType : "",
                            QSHeading = formUtil.EscapeCRLF(objAry[i, 8] == null ? "" : formUtil.EscapeCRLF(objAry[i, 8].ToString())),
                            Question = objAry[i, 9] == null ? "" : formUtil.EscapeCRLF(objAry[i, 9].ToString()),
                            QuestionIndex = qIndex,
                            CategoryCount = catCount,
                            QsTypePlusCatCount = QsTypePlusCatCount,
                            Variable = objAry[i, 3].ToString(),
                            QSTypeShort = objAry[i, 4].ToString(),
                            QsNumberIndex = qsNumIndx,
                            QuestionNumber = isSetQSNum ? qsNum : 0,
                            Graph = graphType == null ? "" : graphType.Length > 0 ? graphType.Split(',')[0] : ""
                        });
                        qIndex++;
                        if (qsNum != 0)
                        {
                            qsNum--;
                        }
                        if (qsNum > 0)
                            qsNumIndx++;
                        else
                        {
                            qsNumIndx = 0;
                            qsNum = 0;
                        }
                        isSetQSNum = false;
                    }
                    if (qsNum > 0 && isSetQSNum)
                    {
                        isSetQSNum = false;
                        qsNum = 0;
                    }
                }

            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
            return qsItems;
        }

        public static string GetFullTypeNameByAnsType(string type)
        {
            switch (type)
            {
                case Util.Constants.GT.GTSA:
                    return LocalResource.FOR_SA;
                case Util.Constants.GT.GTN:
                    return LocalResource.FOR_N;
                case Util.Constants.GT.GTMTS:
                    return LocalResource.FOR_MTS;
                case Util.Constants.GT.GTMTN:
                    return LocalResource.FOR_MTN;
                case Util.Constants.GT.GTMA:
                    return LocalResource.FOR_MA;
                case Util.Constants.GT.GTMTM:
                    return LocalResource.FOR_MTM;
                case Util.Constants.GT.GTRAT:
                    return LocalResource.FOR_RAT;
                case Util.Constants.GT.GTRNK:
                    return LocalResource.FOR_RNK;

            }
            return null;
        }


        private static string GetGraphList(string chartType)
        {
            string graphList = null;
            switch (chartType)
            {
                case Util.Constants.GT.GTMTS:
                    return LocalResource.GTMTSGraph;
                case Util.Constants.GT.GTMTM:
                    return LocalResource.GTMTMGraph;
                case Util.Constants.GT.GTRAT:
                    return LocalResource.GTRATGraph;
                case Util.Constants.GT.GTRNK:
                    return LocalResource.GTRNKGraph;
                case Util.Constants.GT.GTSA:
                    return LocalResource.GTSAGraph;
                case Util.Constants.GT.GTMA:
                    return LocalResource.GTMAGraph;
                default:
                    return graphList;
            }
        }

        private static bool IsMatchChoice(object[,] objAry, int indx, string questionVariable)
        {
            int currentChoice = 0;
            for (int i = 1; i <= objAry.GetLength(0); i++)
            {
                if (objAry[i, 3].ToString() == questionVariable)
                {
                    currentChoice = i;
                    break;
                }
            }
            for (int i = 10; i <= objAry.GetLength(1); i++)
            {
                string fIt = objAry[indx, i] == null ? "" : objAry[indx, i].ToString();
                string sIt = objAry[currentChoice, i] == null ? "" : objAry[currentChoice, i].ToString();
                if (fIt != sIt)
                    return false;
            }
            return true;
        }

        public static string[] GetTypesByStr(string str)
        {
            if (LocalResource.FOR_SA == str)
                return new string[] { Util.Constants.AnswerType.SA };
            else if (LocalResource.FOR_MA == str)
                return new string[] { Util.Constants.AnswerType.MA };
            else if (LocalResource.FOR_N == str)
                return new string[] { Util.Constants.AnswerType.N };
            else if (LocalResource.FOR_MTM == str)
                return new string[] { Util.Constants.AnswerType.SA, Util.Constants.AnswerType.MA };
            else if (LocalResource.FOR_MTS == str)
                return new string[] { Util.Constants.AnswerType.SA };
            else if (LocalResource.FOR_MTN == str)
                return new string[] { Util.Constants.AnswerType.N };
            else if (LocalResource.FOR_RAT == str)
                return new string[] { Util.Constants.AnswerType.N };

            return new string[] { Util.Constants.AnswerType.SA };
        }

        public static string GetGTTypeShortByGTTypeFull(string type)
        {
            if (LocalResource.FOR_SA == type)
            {
                return Util.Constants.AnswerType.SA;
            }
            else if (LocalResource.FOR_MA == type)
            {
                return Util.Constants.AnswerType.MA;
            }
            else if (LocalResource.FOR_N == type)
            {
                return Util.Constants.AnswerType.N;
            }
            else if (LocalResource.FOR_MTM == type)
            {
                return Util.Constants.QuestionType.MTM;
            }
            else if (LocalResource.FOR_MTS == type)
            {
                return Util.Constants.QuestionType.MTS;
            }
            else if (LocalResource.FOR_MTN == type)
            {
                return Util.Constants.QuestionType.MTN;
            }
            else if (LocalResource.FOR_RAT == type)
            {
                return Util.Constants.QuestionType.RAT;
            }
            else if (LocalResource.FOR_RNK == type)
            {
                return Util.Constants.QuestionType.RNK;
            }
            return "";
        }
        public static string GetGraphByLanguage(string graph)
        {
            if (graph == QC4Common.Common.Constants.GTGraphQC100StackedBarChartJP || graph == QC4Common.Common.Constants.GTGraphQC100StackedBarChart)
            {
                return AddinResource.GTGraphQC100StackedBarChart;
            }
            else if (graph == QC4Common.Common.Constants.GTGraphQC100StackedColumnChartJP || graph == QC4Common.Common.Constants.GTGraphQC100StackedColumnChart)
            {
                return AddinResource.GTGraphQC100StackedColumnChart;
            }
            else if (graph == QC4Common.Common.Constants.GTGraphQCBarChartJP || graph == QC4Common.Common.Constants.GTGraphQCBarChart)
            {
                return AddinResource.GTGraphQCBarChart;
            }
            else if (graph == QC4Common.Common.Constants.GTGraphQCColumnChartJP || graph == QC4Common.Common.Constants.GTGraphQCColumnChart)
            {
                return AddinResource.GTGraphQCColumnChart;
            }
            else if (graph == QC4Common.Common.Constants.GTGraphQCMBarChartJP || graph == QC4Common.Common.Constants.GTGraphQCMBarChart)
            {
                return AddinResource.GTGraphQCMBarChart;
            }
            else if (graph == QC4Common.Common.Constants.GTGraphQCMColumnChartJP || graph == QC4Common.Common.Constants.GTGraphQCMColumnChart)
            {
                return AddinResource.GTGraphQCMColumnChart;
            }
            else if (graph == QC4Common.Common.Constants.GTGraphQCPieRATChartJP || graph == QC4Common.Common.Constants.GTGraphQCPieRATChart)
            {
                return AddinResource.GTGraphQCPieRATChart;
            }
            else if (graph == QC4Common.Common.Constants.GTGraphQCRATBarChartJP || graph == QC4Common.Common.Constants.GTGraphQCRATBarChart)
            {
                return AddinResource.GTGraphQCRATBarChart;
            }
            else if (graph == QC4Common.Common.Constants.GTGraphQCRATColumnChartJP || graph == QC4Common.Common.Constants.GTGraphQCRATColumnChart)
            {
                return AddinResource.GTGraphQCRATColumnChart;
            }
            else if (graph == QC4Common.Common.Constants.GTGraphQCStackedBarChartJP || graph == QC4Common.Common.Constants.GTGraphQCStackedBarChart)
            {
                return AddinResource.GTGraphQCStackedBarChart;
            }
            else if (graph == QC4Common.Common.Constants.GTGraphQCStackedColumnChartJP || graph == QC4Common.Common.Constants.GTGraphQCStackedColumnChart)
            {
                return AddinResource.GTGraphQCStackedColumnChart;
            }
            else if (graph == QC4Common.Common.Constants.GTGraphQCPieChartJP || graph == QC4Common.Common.Constants.GTGraphQCPieChart)
            {
                return AddinResource.GTGraphQCPieChart;
            }
            else if (graph == QC4Common.Common.Constants.GTGraphQCMPieChartJP || graph == QC4Common.Common.Constants.GTGraphQCMPieChart)
            {
                return AddinResource.GTGraphQCMPieChart;
            }
            return "";
        }
    }
}
