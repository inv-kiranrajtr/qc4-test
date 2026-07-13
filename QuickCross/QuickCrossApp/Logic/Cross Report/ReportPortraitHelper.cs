using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qc4Launcher.Logic.Cross_Report
{
    static class ReportPortraitHelper
    {
        public static class N_CellStyle
        {
            public static uint[] Header_TopRowStyleIndex = new uint[] { 168, 168, 21 };
            public static uint[] Header_SecondRowStyleIndex = new uint[] { 167, 168, 22 };
            public static uint[] Header_TripleStyleIndex = new uint[] { 167, 168, 119 };
            public static uint[] Header_ShowPrewbTotalStyleIndex = new uint[] { 115, 117, 27 };
            public static uint[] Header_TotalStyleIndex = new uint[] { 115, 117, 27 };
            public static uint[] Header_PopulationStyleIndex = new uint[] { 97, 155, 111 };
            public static uint[] Header_SummaryStyleIndex = new uint[] { 59, 154, 135 };
            public static uint[] Header_AverageStyleIndex = new uint[] { 59, 154, 112 };
            public static uint[] Header_DeviationStyleIndex = new uint[] { 59, 154, 112 };
            public static uint[] Header_MaximumStyleIndex = new uint[] { 59, 154, 112 };
            public static uint[] Header_MinimumStyleIndex = new uint[] { 59, 154, 112 };
            public static uint[] Header_MedianStyleIndex = new uint[] { 59, 154, 112 };
            public static uint[] Header_NoAnswerStyleIndex = new uint[] { 84, 156, 113 };
            public static uint[] Header_InvalidStyleIndex = new uint[] { 123, 157, 130 };

            public static uint[] Double_TopRowStyleIndex = new uint[] { 21, 172, 173, 174 };
            public static uint[] Double_SecondRowStyleIndex = new uint[] { 22, 180, 182, 184 };
            public static uint[] Double_TripleStyleIndex = new uint[] { 119, 186, 187, 188 };
            public static uint[] Double_TotalStyleIndex = new uint[] { 27, 76, 41, 30 };
            public static uint[] Double_ShowPrewbTotalStyleIndex = new uint[] { 27, 76, 41, 30 };
            public static uint[] Double_PopulationStyleIndex = new uint[] { 111, 101, 98, 99 };
            public static uint[] Double_SummaryStyleIndex = new uint[] { 135, 139, 137, 138 };
            public static uint[] Double_AverageStyleIndex = new uint[] { 112, 105, 102, 103 };
            public static uint[] Double_DeviationStyleIndex = new uint[] { 112, 105, 102, 103 };
            public static uint[] Double_MaximumStyleIndex = new uint[] { 112, 105, 102, 103 };
            public static uint[] Double_MinimumStyleIndex = new uint[] { 112, 105, 102, 103 };
            public static uint[] Double_MedianStyleIndex = new uint[] { 112, 105, 102, 103 };
            public static uint[] Double_NoAnswerStyleIndex = new uint[] { 113, 109, 106, 107 };
            public static uint[] Double_InvalidStyleIndex = new uint[] { 130, 134, 132, 133 };
        }

        public static class NSig_CellStyle
        {
            public static uint[] Header_TopRowStyleIndex = new uint[] { 146, 146, 53 };
            public static uint[] Header_SecondRowStyleIndex = new uint[] { 146, 146, 54 };
            public static uint[] Header_TripleStyleIndex = new uint[] { 146, 146, 54 };
            public static uint[] Header_SigStyleIndex = new uint[] { 146, 146, 54 };
            public static uint[] Header_ShowPrewbTotalStyleIndex = new uint[] { 72, 88, 77 };
            public static uint[] Header_TotalStyleIndex = new uint[] { 72, 88, 77 };
            public static uint[] Header_PopulationStyleIndex = new uint[] { 52,95,40};
            public static uint[] Header_SummaryStyleIndex = new uint[] { 33,93,46 };
            public static uint[] Header_AverageStyleIndex = new uint[] { 33,93,41};
            public static uint[] Header_DeviationStyleIndex = new uint[] { 33, 93, 41 };
            public static uint[] Header_MaximumStyleIndex = new uint[] { 33, 93, 41 };
            public static uint[] Header_MinimumStyleIndex = new uint[] { 33, 93, 41 };
            public static uint[] Header_MedianStyleIndex = new uint[] { 33, 93, 41 };
            public static uint[] Header_NoAnswerStyleIndex = new uint[] { 48,96,42};
            public static uint[] Header_SigTest = new uint[] { 113,94,68 };

            public static uint[] Double_TopRowStyleIndex = new uint[] { 53, 136, 137, 138 };
            public static uint[] Double_SecondRowStyleIndex = new uint[] { 54, 142, 143, 144 };
            public static uint[] Double_TripleStyleIndex = new uint[] { 54, 147, 148, 149 };
            public static uint[] Double_SigStyleIndex = new uint[] { 54, 60, 55, 56 };
            public static uint[] Double_TotalStyleIndex = new uint[] { 77, 76, 74, 75 };
            public static uint[] Double_ShowPrewbTotalStyleIndex = new uint[] { 77, 76, 74, 75 };
            public static uint[] Double_PopulationStyleIndex = new uint[] { 40, 19, 20, 21 };
            public static uint[] Double_SummaryStyleIndex = new uint[] { 46, 47, 44, 45 };
            public static uint[] Double_AverageStyleIndex = new uint[] { 41, 34, 35, 36 };
            public static uint[] Double_DeviationStyleIndex = new uint[] { 41, 34, 35, 36 };
            public static uint[] Double_MaximumStyleIndex = new uint[] { 41, 34, 35, 36 };
            public static uint[] Double_MinimumStyleIndex = new uint[] { 41, 34, 35, 36 };
            public static uint[] Double_MedianStyleIndex = new uint[] { 41, 34, 35, 36 };
            public static uint[] Double_NoAnswerStyleIndex = new uint[] { 42, 37, 38, 39 };
            public static uint[] Double_SigTest = new uint[] { 68, 64, 62, 63 };
        }

        public static class SAMA_WT_CellStyle
        {
            public static uint[] Header_TopRow = new uint[] { 168, 168, 21 };
            public static uint[] Header_SecondRow = new uint[] { 168, 168, 22 };
            public static uint[] Header_Triple = new uint[] { 168, 168, 22 };
            public static uint[] Header_ShowPrewbTotal = new uint[] { 115, 117, 27 };
            public static uint[] Header_Total = new uint[] { 115, 117, 27 };
            public static uint[] Header_SectorRow1 = new uint[] { 97, 155, 141 };
            public static uint[] Header_SectorRow2 = new uint[] { 59, 154, 60 };
            public static uint[] Header_NoAnswer = new uint[] { 84, 156, 86 };
            public static uint[] Header_Invalid = new uint[] { 123, 157, 32 };
            public static uint[] Header_Population = new uint[] { 97, 145, 146 };
            public static uint[] Header_Average = new uint[] { 84, 85, 150 };

            public static uint[] Double_TopRow = new uint[] { 21, 172, 173, 174 };
            public static uint[] Double_SecondRow = new uint[] { 22, 180, 182, 184 };
            public static uint[] Double_Triple = new uint[] { 22, 186, 187, 188 };
            public static uint[] Double_ShowPrewbTotal = new uint[] { 27, 76, 41, 30 };
            public static uint[] Double_Total = new uint[] { 27, 76, 41, 30 };
            public static uint[] Double_SectorRow1 = new uint[] { 141, 142, 143, 144 };
            public static uint[] Double_SectorRow2 = new uint[] { 60, 65, 62, 63 };
            public static uint[] Double_NoAnswer = new uint[] { 86, 90, 88, 89 };
            public static uint[] Double_Invalid = new uint[] { 32, 38, 44, 45 };
            public static uint[] Double_Population = new uint[] { 146, 147, 148, 149 };
            public static uint[] Double_Average = new uint[] { 150, 151, 152, 153 };
        }

        public static class SAMA_WT_Sig_CellStyle
        {
            public static uint[] Header_TopRow = new uint[] { 146, 146, 53 };
            public static uint[] Header_SecondRow = new uint[] { 146, 146, 54 };
            public static uint[] Header_Triple = new uint[] { 146, 146, 54 };
            public static uint[] Header_SigStyleIndex = new uint[] { 146, 146, 54 };
            public static uint[] Header_ShowPrewbTotal = new uint[] { 72, 88, 77 };
            public static uint[] Header_Total = new uint[] { 72, 88, 77 };
            public static uint[] Header_SectorRow1_1 = new uint[] { 139, 89, 78 };
            public static uint[] Header_SectorRow1_2 = new uint[] { 150, 91, 82 };
            public static uint[] Header_SectorRow2_1 = new uint[] { 145, 90, 69 };
            public static uint[] Header_SectorRow2_2 = new uint[] { 150, 91, 82 };
            public static uint[] Header_NoAnswer1_1 = new uint[] { 145, 90, 69 };
            public static uint[] Header_NoAnswer1_2 = new uint[] { 150, 91, 82 };
            public static uint[] Header_Population1_1 = new uint[] { 139, 89, 71 };
            public static uint[] Header_Population1_2 = new uint[] { 150, 91, 57 };
            public static uint[] Header_Average1_1 = new uint[] { 145, 90, 108 };
            public static uint[] Header_Average1_2 = new uint[] { 151, 92, 102 };

            public static uint[] Double_TopRow = new uint[] { 53, 136, 137, 138 };
            public static uint[] Double_SecondRow = new uint[] { 54, 142, 143, 144 };
            public static uint[] Double_Triple = new uint[] { 54, 147, 148, 149 };
            public static uint[] Double_SigTest = new uint[] { 54, 60, 55, 56 };
            public static uint[] Double_ShowPrewbTotal = new uint[] { 77, 76, 74, 75 };
            public static uint[] Double_Total = new uint[] { 77, 76, 74, 75 };
            public static uint[] Double_SectorRow1_1 = new uint[] { 78, 79, 50, 51 };
            public static uint[] Double_SectorRow1_2 = new uint[] { 82, 112, 110, 111 };
            public static uint[] Double_SectorRow2_1 = new uint[] { 69, 70, 66, 67 };
            public static uint[] Double_SectorRow2_2 = new uint[] { 82, 112, 110, 111 };
            public static uint[] Double_NoAnswer1_1 = new uint[] { 69, 70, 66, 67 };
            public static uint[] Double_NoAnswer1_2 = new uint[] { 81, 114, 115, 116 };
            public static uint[] Double_Population1_1 = new uint[] { 71, 26, 27, 28 };
            public static uint[] Double_Population1_2 = new uint[] { 57, 61, 58, 59 };
            public static uint[] Double_Average1_1 = new uint[] { 108, 109, 106, 107 };
            public static uint[] Double_Average1_2 = new uint[] { 102, 105, 103, 104 };
        }

        public static class SAMA_CellStyle
        {
            public static uint[] Header_TopRow = new uint[] { 168, 168, 21, };
            public static uint[] Header_SecondRow = new uint[] { 167, 168, 22 };
            public static uint[] Header_Triple = new uint[] { 167, 168, 22 };
            public static uint[] Header_ShowPrewbTotal = new uint[] { 115, 117, 27 };
            public static uint[] Header_Total = new uint[] { 115, 117, 27 };
            public static uint[] Header_SectorRow1 = new uint[] { 97, 155, 141 };
            public static uint[] Header_SectorRow2 = new uint[] { 59, 154, 60 };
            public static uint[] Header_NoAnswer = new uint[] { 84, 156, 86 };
            public static uint[] Header_Invalid = new uint[] { 123, 157, 32 };

            public static uint[] Double_TopRow = new uint[] { 21, 172, 173, 174 };
            public static uint[] Double_SecondRow = new uint[] { 22, 180, 182, 184 };
            public static uint[] Double_Triple = new uint[] { 22, 181, 183, 185 };
            public static uint[] Double_ShowPrewbTotal = new uint[] { 27, 76, 41, 30 };
            public static uint[] Double_Total = new uint[] { 27, 76, 41, 30 };
            public static uint[] Double_SectorRow1 = new uint[] { 141, 142, 143, 144 };
            public static uint[] Double_SectorRow2 = new uint[] { 60, 65, 62, 63 };
            public static uint[] Double_NoAnswer = new uint[] { 86, 90, 88, 89 };
            public static uint[] Double_Invalid = new uint[] { 32, 38, 44, 45 };
        }

        public static class SAMA_Sig_CellStyle
        {
            public static uint[] Header_TopRow = new uint[] { 146, 146, 53 };
            public static uint[] Header_SecondRow = new uint[] { 146, 146, 54 };
            public static uint[] Header_Triple = new uint[] { 146, 146, 54 };
            public static uint[] Header_SigStyleIndex = new uint[] { 146, 146, 54 };
            public static uint[] Header_ShowPrewbTotal = new uint[] { 72, 88, 77 };
            public static uint[] Header_Total = new uint[] { 72, 88, 77 };
            public static uint[] Header_SectorRow1_1 = new uint[] { 139, 89, 78 };
            public static uint[] Header_SectorRow1_2 = new uint[] { 150, 91, 82 };
            public static uint[] Header_SectorRow2_1 = new uint[] { 145, 90, 69 };
            public static uint[] Header_SectorRow2_2 = new uint[] { 150, 91, 82 };
            public static uint[] Header_NoAnswer1_1 = new uint[] { 145, 90, 69 };
            public static uint[] Header_NoAnswer1_2 = new uint[] { 150, 91, 82 };

            public static uint[] Double_TopRow = new uint[] { 53, 136, 137, 138 };
            public static uint[] Double_SecondRow = new uint[] { 54, 142, 143, 144 };
            public static uint[] Double_Triple = new uint[] { 54, 147, 148, 149 };
            public static uint[] Double_SigTest = new uint[] { 54, 60, 55, 56 };
            public static uint[] Double_ShowPrewbTotal = new uint[] { 77, 76, 74, 75 };
            public static uint[] Double_Total = new uint[] { 77, 76, 74, 75 };
            public static uint[] Double_SectorRow1_1 = new uint[] { 78, 79, 50, 51 };
            public static uint[] Double_SectorRow1_2 = new uint[] { 82, 112, 110, 111 };
            public static uint[] Double_SectorRow2_1 = new uint[] { 69, 70, 66, 67 };
            public static uint[] Double_SectorRow2_2 = new uint[] { 82, 112, 110, 111 };
            public static uint[] Double_NoAnswer1_1 = new uint[] { 69, 70, 66, 67 };
            public static uint[] Double_NoAnswer1_2 = new uint[] { 81, 114, 115, 116 };
        }

        public static class BarClusterGraph
        {
            public static uint[] Bar_ShowPrewbTotal = new uint[] { 115, 116, 116, 116, 116, 116, 116, 117 };
            public static uint[] Bar_Total = new uint[] { 115, 116, 116, 116, 116, 116, 116, 117 };
            public static uint[] Bar_Graph_FirstRow = new uint[] { 197, 198, 198, 198, 198, 198, 198, 199 };
            public static uint[] Bar_Graph_MiddleRow = new uint[] { 200, 2, 2, 2, 2, 2, 2, 201 };
            public static uint[] Bar_Graph_LastRow = new uint[] { 194, 195, 195, 195, 195, 195, 195, 196 };

            public static double Bar_Cluster_Column_Width = 5.50;
            public static int Bar_Cluster_Start_Row { get; set; }
            public static int Bar_Cluster_End_Row { get; set; }
        }

        public static class GraphHelper
        {
            public static int graphStartRow { get; set; }
            public static int graphEndRow { get; set; }
            public static int graphFirstColumn { get; set; }
            public static int graphLastColumn { get; set; }
        }
    }
}
