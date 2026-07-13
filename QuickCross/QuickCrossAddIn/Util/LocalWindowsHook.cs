using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Office.Core;
using Excel = Microsoft.Office.Interop.Excel;
using ExcelAddIn.Common;
using Constatnt = QC4Common.Common.Constants;
using ExcelAddIn.QS;
using System.Text;

namespace ExcelAddIn
{
    public class InterceptKeys
    {
        public delegate int LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;
        private static Microsoft.Office.Tools.CustomTaskPane ctpRef = null;

        //Declare the mouse hook constant.
        //For other hook types, you can obtain these values from Winuser.h in the Microsoft SDK.            

        private const int WH_KEYBOARD = 2;
        private const int HC_ACTION = 0;

        public static void SetHook()
        {
            _hookID = SetWindowsHookEx(WH_KEYBOARD, _proc, IntPtr.Zero, (uint)AppDomain.GetCurrentThreadId());
        }

        public static void ReleaseHook()
        {
            UnhookWindowsHookEx(_hookID);
        }

        public static bool IsPresent()
        {
            // Enumerate windows to find the message box
            EnumThreadWndProc callback = new EnumThreadWndProc(checkWindow);
            return false == EnumThreadWindows(GetCurrentThreadId(), callback, IntPtr.Zero);
        }
        private static bool checkWindow(IntPtr hWnd, IntPtr lp)
        {
            // Checks if <hWnd> is a dialog
            StringBuilder sb = new StringBuilder(260);
            GetClassName(hWnd, sb, sb.Capacity);
            if (sb.ToString() != "#32770") return true;
            // Got a dialog, check if the the STATIC control is present
            IntPtr hText = GetDlgItem(hWnd, 0xffff);
            return hText == IntPtr.Zero;
        }
        // P/Invoke declarations
        private delegate bool EnumThreadWndProc(IntPtr hWnd, IntPtr lp);
        [DllImport("user32.dll")]
        private static extern bool EnumThreadWindows(int tid, EnumThreadWndProc callback, IntPtr lp);
        [DllImport("kernel32.dll")]
        private static extern int GetCurrentThreadId();
        [DllImport("user32.dll")]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder buffer, int buflen);
        [DllImport("user32.dll")]
        private static extern IntPtr GetDlgItem(IntPtr hWnd, int item);

        private static int HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            int PreviousStateBit = 31;
            bool KeyWasAlreadyPressed = false;

            Int64 bitmask = (Int64)Math.Pow(2, (PreviousStateBit - 1));

            try
            {
                if (nCode < 0)
                {
                    return (int)CallNextHookEx(_hookID, nCode, wParam, lParam);
                }
                else
                {
                    if (nCode == HC_ACTION)
                    {
						//if (QC4Common.Util.ExcelUtil.IsEditing(Globals.ThisAddIn.Application))
						//{
							//Globals.Ribbons.qc4Ribbon.buttonMenu.Enabled = false;
						Util.RibbonUtil.EnableRibbon(!QC4Common.Util.ExcelUtil.IsEditing(Globals.ThisAddIn.Application));
						//}
						//else
						//{
						//	Globals.Ribbons.qc4Ribbon.buttonMenu.Enabled = true;
						//}

                        Keys keyData = (Keys)wParam;
                        KeyWasAlreadyPressed = ((Int64)lParam & bitmask) > 0;
                        if (Functions.IsKeyDown(Keys.ControlKey) && Functions.IsKeyDown(Keys.ShiftKey) && keyData == Keys.C && KeyWasAlreadyPressed == false)
                        {
                            ExcelUtil.CopySelected();
                            return 1;
                        }
                        if (Functions.IsKeyDown(Keys.ControlKey) && Functions.IsKeyDown(Keys.ShiftKey) && keyData == Keys.V && KeyWasAlreadyPressed == false)
                        {
                            ExcelUtil.PasteSelected();
                            return 1;
                        }
                        if(Functions.IsKeyDown(Keys.ControlKey) && keyData == Keys.V && KeyWasAlreadyPressed == false)
                        {
                            Constatnt.IsCtrlV = true;
                        }
                        Object obj = Globals.ThisAddIn.Application.ActiveSheet;
						if (obj != null && ((Excel.Worksheet)obj).CodeName == Constatnt.SheetType.sh_QuesSetting && !IsPresent())
						{
                            return KeyMock.QsKeyMock(KeyWasAlreadyPressed, _hookID, nCode, wParam, lParam);
						}
                        else if (Functions.IsKeyDown(Keys.ControlKey) && Functions.IsKeyDown(Keys.V) && KeyWasAlreadyPressed == false && obj != null && 
                                    (((Excel.Worksheet)obj).CodeName == Constatnt.SheetCodeName.DataProcess ||
                                    ((Excel.Worksheet)obj).CodeName == Constatnt.SheetCodeName.FACreation ||
                                    ((Excel.Worksheet)obj).CodeName == Constatnt.SheetCodeName.GTTabulation||
                                    ((Excel.Worksheet)obj).CodeName == Constatnt.SheetCodeName.SummaryT ||
                                    ((Excel.Worksheet)obj).CodeName == Constatnt.SheetCodeName.LDEL ||
                                    ((Excel.Worksheet)obj).CodeName == Constatnt.SheetCodeName.CrossTabulation) && !IsPresent())
                        {
                            return KeyMock.CtrlV(_hookID, nCode, wParam, lParam);
                        }

                    }
                    return (int)CallNextHookEx(_hookID, nCode, wParam, lParam);
                }
            }
            catch (Exception ex)
            {
				Console.WriteLine(ex.Message);
                return (int)CallNextHookEx(_hookID, nCode, wParam, lParam);
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);
    }

    public class Functions
    {
        public static bool IsKeyDown(Keys keys)
        {
            return (GetKeyState((int)keys) & 0x8000) == 0x8000;
        }

        [DllImport("user32.dll")]
        static extern short GetKeyState(int nVirtKey);

    }
}
