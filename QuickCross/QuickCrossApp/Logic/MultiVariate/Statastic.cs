using System;
using System.Linq;
using Microsoft.Office.Interop.Excel;

namespace Qc4Launcher.Logic.MultiVariate
{
    internal class Statastic
    {
        internal static double Average(double[] dataArr)
        {
            return dataArr.Average();
        }

        internal static double Min(double[] dataArr)
        {
            return dataArr.Min();
        }

        internal static double Max(double[] dataArr)
        {
            return dataArr.Max();
        }

        internal static double Range(double[] dataArr)
        {
            return dataArr.Max() - dataArr.Min();
        }

        internal static double Sum(double[] dataArr)
        {
            return dataArr.Sum();
        }

        internal static double Count(double[] dataArr)
        {
            return dataArr.Count();
        }

        internal static void Medain(double[] dataArr, Application xlApp, ref double median, ref double mode)
        {
            median = 0.0;

            if (dataArr.Count() < 65000)
            {
                WorksheetFunction wf = xlApp.WorksheetFunction;
                try
                {
                    mode = wf.Mode_Sngl(dataArr);
                }
                catch (Exception)
                {
                    mode = double.NaN;
                }
            }

            double[] array = dataArr.OrderByDescending(c => c).ToArray();
            int arrLngth = array.Length;
            if (arrLngth % 2 == 0)
            {
                median = (array[arrLngth / 2] + array[arrLngth / 2 - 1]) / 2;
            }
            else
            {
                median = array[(arrLngth - 1) / 2];
            }
            if (mode == 0)
            {
                int NCounter = 1;
                int MaxCnt = 1;
                mode = array[0];
                bool invalid = true;

                for (int i = 1; i < arrLngth; i++)
                {
                    if (array[i] == array[i - 1])
                    {
                        NCounter += 1;
                        invalid = false;
                    }
                    else
                    {
                        if (NCounter > MaxCnt)
                        {
                            MaxCnt = NCounter;
                            mode = array[i - 1];
                        }
                        NCounter = 1;
                    }
                }
                if (NCounter > MaxCnt)
                {
                    mode = array[arrLngth - 1];
                }

                if (invalid)
                    mode = double.NaN;
            }

        }

        internal static double? StdDev(double[] dataArr, Application xlApp)
        {
            double? StDevPVal = null;
            if (dataArr.Count() < 65000)
            {
                WorksheetFunction wf = xlApp.WorksheetFunction;
                try
                {
                    StDevPVal =Convert.ToDouble( wf.StDev(dataArr));
                }
                catch (Exception)
                {
                    StDevPVal = double.NaN;
                }
            }
            else
            {
                int NumCnt = dataArr.Length;
               
                if (NumCnt > 0)
                {
                    double sum = dataArr.Sum();
                    double sumSq = 0.0;
                    for (int i = 0; i < NumCnt; i++)
                    {
                        sumSq = sumSq + dataArr[i] * dataArr[i];

                    }
                    StDevPVal = NumCnt * sumSq - sum * sum;
                    StDevPVal = Math.Sqrt(Convert.ToDouble(StDevPVal));
                    StDevPVal = StDevPVal / NumCnt;
                    StDevPVal = StDevPVal * Math.Sqrt(NumCnt / Convert.ToDouble(NumCnt - 1));
                }
            }
            return StDevPVal;
        }

        internal static double Kurtosis(double[] highDataArr, double NumCnt, double avg, double stdDevC)
        {
            double wkVal_A = NumCnt / (double)(NumCnt - 1);
            wkVal_A = wkVal_A * (NumCnt + 1) / (NumCnt - 2);
            wkVal_A = wkVal_A / (NumCnt - 3);
            double wkVal_B = 3 * (NumCnt - 1) / (double)(NumCnt - 2);
            wkVal_B = wkVal_B * (NumCnt - 1) / (NumCnt - 3);
            double SumVal = 0.0;
            double wkVal_C;
            for (int i = 0; i < NumCnt; i++)
            {
                wkVal_C = Standardize(highDataArr[i], avg, stdDevC);
                SumVal = SumVal + Math.Pow(wkVal_C, 4);
            }

            double KurtVal = wkVal_A * SumVal - wkVal_B;
            return KurtVal;
        }

        private static double Standardize(double v, double avg, double stdDevC)
        {
            return (v - avg) / stdDevC;
        }

        internal static double SkewValue(double[] highDataArr, double NumCnt, double avg, double stdDevC)
        {
            double wkVal_A = NumCnt / (double)(NumCnt - 1);
            wkVal_A = wkVal_A / (NumCnt - 2);
            double SumVal = 0.0;
            double wkVal_B;
            for (int i = 0; i < NumCnt; i++)
            {
                wkVal_B = Standardize(highDataArr[i], avg, stdDevC);
                SumVal = SumVal + Math.Pow(wkVal_B, 3);

            }

            double SkewVal = wkVal_A * SumVal;
            return SkewVal;
        }
    }
}