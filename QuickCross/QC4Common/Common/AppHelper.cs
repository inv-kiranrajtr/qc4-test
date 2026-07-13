using Microsoft.Office.Interop.Excel;
using System;
using Excel = Microsoft.Office.Interop.Excel;

namespace QC4Common.Common
{
    internal class AppHelper
    {
        private XlEnableCancelKey preCancelKey;
        private bool preScreenUpdate;
        private bool preEnableEvents;
        private XlCalculation preCalculation;

        internal void ExcelSet(Excel.Workbook book,bool CancelKeyMode = true, bool screenUpdateMode = true, bool enableEventsMode = true,
                bool calculationMode = true, Application targetApp = null)
        {
            if (null == targetApp)
            {
                targetApp = book.Application;
            }

            preCancelKey = targetApp.EnableCancelKey;
            preScreenUpdate = targetApp.ScreenUpdating;
            preEnableEvents = targetApp.EnableEvents;
            if (VisibleBookCount(targetApp) > 0)
            {
                preCalculation = targetApp.Calculation;
            }

            if (CancelKeyMode == true && targetApp.EnableCancelKey != XlEnableCancelKey.xlDisabled) {
                targetApp.EnableCancelKey = XlEnableCancelKey.xlDisabled;
            }
            if (screenUpdateMode == true && targetApp.ScreenUpdating != false)
            {
                targetApp.ScreenUpdating = false;

            }

            if (enableEventsMode == true && targetApp.EnableEvents != false)
            {
                targetApp.EnableEvents = false;

            }


            if (VisibleBookCount(targetApp) > 0)
            {
                if (calculationMode == true && targetApp.Calculation != XlCalculation.xlCalculationManual)
                {
                    targetApp.Calculation = XlCalculation.xlCalculationManual;

                }
            }
        }


        internal static int VisibleBookCount(Application targetApp)
        {
            int i = 0;
            foreach (Workbook workbook in targetApp.Workbooks)
            {
                foreach (Window window in workbook.Windows)
                {
                    if (window.Visible == true)
                    {
                        i++;
                    }
                }

            }
            return i;

        }

        internal void ExcelReset(Excel.Workbook book,bool preMode = false, Application targetApp = null)
        {
            if (null == targetApp)
            {
                targetApp = book.Application;
            }

            if (preMode)
            {
                if (VisibleBookCount(targetApp) > 0)
                {
                    if (targetApp.Calculation != preCalculation)
                    {
                        targetApp.Calculation = preCalculation;
                    }
                }
                if (targetApp.EnableEvents != preEnableEvents)
                {
                    targetApp.EnableEvents = preEnableEvents;
                }
                if (targetApp.ScreenUpdating != preScreenUpdate)
                {
                    targetApp.ScreenUpdating = preScreenUpdate;
                }
                if (targetApp.EnableCancelKey != preCancelKey)
                {
                    targetApp.EnableCancelKey = preCancelKey;
                }

            }
            else
            {
                if (VisibleBookCount(targetApp) > 0)
                {
                    if (targetApp.Calculation != XlCalculation.xlCalculationAutomatic)
                    {
                        targetApp.Calculation = XlCalculation.xlCalculationAutomatic;
                    }
                }
                if (targetApp.EnableEvents != true)
                {
                    targetApp.EnableEvents = true;
                }
                if (targetApp.ScreenUpdating != true)
                {
                    targetApp.ScreenUpdating = true;
                }
                if (targetApp.EnableCancelKey != XlEnableCancelKey.xlInterrupt)
                {
                    targetApp.EnableCancelKey = XlEnableCancelKey.xlInterrupt;
                }

            }

            if (targetApp.DisplayAlerts == false)
            {
                targetApp.DisplayAlerts = true;
            }
        }

    }
}

