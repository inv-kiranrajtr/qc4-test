using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using QSUtil = QC4Common.Util.QSUtil;
using MessageDialog = QC4Common.Common.MessageDialog;
using Constant = QC4Common.Common.Constants;
using QC4Common;
using Office = Microsoft.Office.Core;
using ExcelAddIn.Common;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace ExcelAddIn.QS
{
    class KeyMock
    {

        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static bool slyk;
        public static int QsKeyMock(bool KeyWasAlreadyPressed, IntPtr _hookID, int nCode, IntPtr wParam, IntPtr lParam)
        {
            try
            {
               
                bool cancel = false;
                Excel.Application app = Globals.ThisAddIn.Application;
                Excel.Range target = (Excel.Range)app.Selection;
                if (Functions.IsKeyDown(Keys.Apps))
                {
                    if (Definitions.VariableEditMode)
                    {
                        cancel = true;
                    }
                    ResetCellMenu();  // reset the cell context menu back to the default
                    try { ExcelAddIn.Common.CommandBar.AddMenuItem(null, target, cancel); } catch (Exception e) { }
                }
            }
            catch (Exception e) { }
            if (Functions.IsKeyDown(Keys.Delete))
            {
                return DeleteKeyMock(_hookID, nCode, wParam, lParam);
            }

            if (Functions.IsKeyDown(Keys.ControlKey) && (Functions.IsKeyDown(Keys.Subtract) || Functions.IsKeyDown(Keys.OemMinus)) && KeyWasAlreadyPressed == false)
            {
                return CtrlMinus(_hookID, nCode, wParam, lParam);
            }

            if (Functions.IsKeyDown(Keys.ControlKey) && Functions.IsKeyDown(Keys.F11) && KeyWasAlreadyPressed == false)
            {
                return 1;
            }

            if (Functions.IsKeyDown(Keys.ControlKey) && Functions.IsKeyDown(Keys.V) && KeyWasAlreadyPressed == false)
            {
                return CtrlV(_hookID, nCode, wParam, lParam);
            }

            if (Common.Definitions.VariableEditMode)
            {
                if (Functions.IsKeyDown(Keys.ControlKey) && Functions.IsKeyDown(Keys.S) && KeyWasAlreadyPressed == false)
                {
                    return 1;
                }

                if (Functions.IsKeyDown(Keys.Menu) && Functions.IsKeyDown(Keys.F2) && KeyWasAlreadyPressed == false)
                {
                    return 1;
                }

                if (Functions.IsKeyDown(Keys.F12))
                {
                    return 1;
                }
            }
            return (int)CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);


        #region keymock functions

        private static int DeleteKeyMock(IntPtr _hookID, int nCode, IntPtr wParam, IntPtr lParam)
        {
            Excel.Application app = Globals.ThisAddIn.Application;
            if (QC4Common.Util.ExcelUtil.IsEditing(app))
            {
                return (int)CallNextHookEx(_hookID, nCode, wParam, lParam);
            }
            Excel.Range range = (Excel.Range)app.Selection;
            if (range.Row <= 3)
            {
                return (int)CallNextHookEx(_hookID, nCode, wParam, lParam);
            }

            Excel.Range rangeRow = range.EntireRow;
            bool isOrg = false;
            bool isAnImp = false;
            bool isRow = false;
            bool isVarRow = false;

            if (range.Address == rangeRow.Address)
            {
                isRow = true;
            }

            if (!isRow)
            {
                isVarRow = QSUtil.IsVariableColumnFound(range);
            }

            GetQuestionFlag(rangeRow, out isOrg, out isAnImp);

            if (isOrg)
            {
                if (isRow || isVarRow)
                {
                    MessageDialog.ErrorOk(AddinResource.QS_ALERT_ORG_DELETE);
                }
                else
                {
                    //ClearContents(range);
                    return (int)CallNextHookEx(_hookID, nCode, wParam, lParam);
                }
            }
            else if (isAnImp)
            {
                Definitions.isQsUpdated = true;
                if (isRow)
                {
                    QSUtil.AnImpRowDelete(range);
                }
                else if (isVarRow)
                {
                    ClearContents(range);
                }
                else
                {
                    return (int)CallNextHookEx(_hookID, nCode, wParam, lParam);
                }
            }
            else
            {
                Definitions.isQsUpdated = true;
                if (isRow)
                {
                    DeleteRow(range);
                }
                else if (isVarRow)
                {
                    ClearContents(range);
                }
                else
                {
                    return (int)CallNextHookEx(_hookID, nCode, wParam, lParam);
                }
            }
            return 1;
        }

        private static int CtrlMinus(IntPtr _hookID, int nCode, IntPtr wParam, IntPtr lParam)
        {
            Excel.Range range = (Excel.Range)Globals.ThisAddIn.Application.Selection;
            Excel.Range rangeRow = range.EntireRow;
            if (range.Row <= 3)
            {
                return (int)CallNextHookEx(_hookID, nCode, wParam, lParam);
            }

            if (range.Address != rangeRow.Address)
            {
                return 1;
            }

            GetQuestionFlag(range, out bool isOrg, out bool isAnImp);
            if (isOrg)
            {
                MessageDialog.ErrorOk(AddinResource.QS_ALERT_DELETE_ORG_QUESTION);
            }
            else if (isAnImp)
            {
                QSUtil.AnImpRowDelete(range);
                Definitions.isQsUpdated = true;
            }
            else
            {
                DeleteRow(range);
                Definitions.isQsUpdated = true;
            }
            return 1;
        }

        //private static int CtrlHAndF(IntPtr _hookID, int nCode, IntPtr wParam, IntPtr lParam)
        //{
        //	if (Definitions.VariableEditMode)
        //	{
        //		return 1;
        //	}
        //	return (int)CallNextHookEx(_hookID, nCode, wParam, lParam);
        //}
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        private static string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }

        public static int CtrlV(IntPtr _hookID, int nCode, IntPtr wParam, IntPtr lParam)
        {
            string title = GetActiveWindowTitle();
            if (null == title || title.Equals(String.Empty) || !title.EndsWith("Quick-CROSS"))
            {
                return (int)CallNextHookEx(_hookID, nCode, wParam, lParam);
            }

            _log.Info(title);
            Excel.Range range = Globals.ThisAddIn.Application.ActiveCell;
            if (range.Address == range.EntireRow.Address)
            {
                return (int)CallNextHookEx(_hookID, nCode, wParam, lParam);
            }
            IDataObject obj = null;
            bool isSylk = false;
            bool isbiff = false;
            Excel.Worksheet s = null;
            try
            {
                obj = Clipboard.GetDataObject();
                isSylk = obj.GetDataPresent(DataFormats.SymbolicLink);
                
                isbiff = obj.GetDataPresent("Biff5");
               s = (Excel.Worksheet)Globals.ThisAddIn.Application.ActiveSheet;
               
                   }
            catch { }
            if (isSylk)
            {
                try
                {
                  ExcelAddIn.Common.ExcelUtil.sylk = isSylk;
                    Globals.ThisAddIn.Application.Selection.Select();

                    Globals.ThisAddIn.Application.ActiveSheet.PasteSpecial(Format: "SYLK", Link: false, DisplayAsIcon: false);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    try
                    {
                        Excel.Range r = (Excel.Range)Globals.ThisAddIn.Application.Selection;
                        r.Select();
                        r.PasteSpecial(Excel.XlPasteType.xlPasteValues);
                        //Globals.ThisAddIn.Application.ActiveSheet.PasteSpecial(Excel.XlPasteType.xlPasteValues);
                    }
                    catch (Exception w)
                    {
                        return (int)CallNextHookEx(_hookID, nCode, wParam, lParam);
                    }
                }
            }
            //else if(isbiff && s.CodeName == Constants.SheetCodeName.DataProcess)
            //{
            //    try
            //    {
            //        Globals.ThisAddIn.Application.Selection.Select();

            //        Globals.ThisAddIn.Application.ActiveSheet.PasteSpecial(Format: "Biff5", Link: false, DisplayAsIcon: false);

            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //        try
            //        {
            //            Excel.Range r = (Excel.Range)Globals.ThisAddIn.Application.Selection;
            //            r.Select();
            //            r.PasteSpecial(Excel.XlPasteType.xlPasteValues);
            //            //Globals.ThisAddIn.Application.ActiveSheet.PasteSpecial(Excel.XlPasteType.xlPasteValues);
            //        }
            //        catch (Exception w)
            //        {
            //            return (int)CallNextHookEx(_hookID, nCode, wParam, lParam);
            //        }
            //    }
            //}

            else
			{
                try
                {
                  ExcelUtil.sylk = true;
                    var copiedText = (string)obj.GetData(DataFormats.UnicodeText, true);
                 
                    if (copiedText.EndsWith("\r\n"))
                        copiedText = copiedText.Remove(copiedText.Length - 2);
                    //string regexReplacedStr = Regex.Replace(copiedText, "(?!(([^\"]*\"){2})*[^\"]*$)(\\*)", string.Empty);//to identify rows
                    var dataToPaste = copiedText.Split(new[] { "\r\n" }, StringSplitOptions.None).Select(i => Regex.Split(i, "\t")).ToArray();

                    Object[,] copyArray = new Object[dataToPaste.Length, dataToPaste[0].Length];
                    for (int i = 0; i != dataToPaste.Length; i++)
                        for (int j = 0; j != dataToPaste[0].Length; j++)
                        {
                            copyArray[i, j] = dataToPaste[i][j];
                            string str = Convert.ToString(copyArray[i, j]);
                            if (str.Contains("\n"))
                            {
                                copyArray[i,j] = str.Remove(str.Length - 1, 1).Remove(0, 1);
                               // copyArray[i, j] = str.Replace("(\"\"){2}", string.Empty);
                            }
                            else if(string.IsNullOrEmpty(str))
                            {
                                // String.IsNullOrEmpty(Convert.ToString(objAry[i, 3])) ? null : objAry[i, 3];
                                copyArray[i, j] = null;
                            }

                        }
                           
                    Excel.Range r = (Excel.Range)Globals.ThisAddIn.Application.Selection;
                    r = r.Resize[copyArray.GetLength(0), copyArray.GetLength(1)];
                    r.Value = copyArray;
                }
                catch
                {
                    return (int)CallNextHookEx(_hookID, nCode, wParam, lParam);
                }
                
            }
            return 1;
        }

        private static void ClearContents(Excel.Range range)
        {
            Globals.ThisAddIn.Application.EnableEvents = false;
            //range.ClearContents();
            QC4Common.Util.ExcelUtil.ClearContents(range);
            Globals.ThisAddIn.Application.EnableEvents = true;
        }

        private static void DeleteRow(Excel.Range row)
        {
            Globals.ThisAddIn.Application.EnableEvents = false;
            row.Delete();
            Globals.ThisAddIn.Application.EnableEvents = true;
        }

        private static void GetQuestionFlag(Excel.Range rangeRow, out bool isOrg, out bool isAnImp)
        {
            isOrg = false;
            isAnImp = false;

            foreach (Excel.Range r in rangeRow.Rows)
            {
                string flag = "New";
                if (Common.Definitions.IdFlagDictionary.ContainsKey(QSUtil.GetQSRowId(r)))
                {
                    flag = Common.Definitions.IdFlagDictionary[QSUtil.GetQSRowId(r)];
                }

                if (flag == Constant.QuestionFlag.Org)
                {
                    isOrg = true;
                    return;
                }
                else if (flag == Constant.QuestionFlag.An || flag == Constant.QuestionFlag.Imp)
                {
                    isAnImp = true;
                }
            }
        }

        //private static void AnImpRowDelete(Excel.Range range)
        //{
        //	DialogResult result = MessageBox.Show(AddinResource.QS_ALERT_AN_IMP_ROW_DELETE, "QuickCross", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        //	if (result == DialogResult.OK)
        //	{
        //		DeleteRow(range);
        //	}
        //}
        #endregion


        private static void ResetCellMenu()
        {
            Globals.ThisAddIn.Application.CommandBars["Cell"].Reset();

        }

    }
}
