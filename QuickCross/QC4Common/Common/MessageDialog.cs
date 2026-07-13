using DiskCatalog.Classes;
using Microsoft.Office.Interop.Excel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Interop;

using Action = System.Action;
using IWin32Window = System.Windows.Forms.IWin32Window;

namespace QC4Common.Common
{
    public class MessageDialog
    {
        public static void Warning(string message)
        {
            MessageBox.Show(message, Constants.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void Error(string message)
        {
            MessageBox.Show(message, Constants.MessageBoxTitle, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
        }

        public static void ErrorOk(string message)
        {
            MessageBox.Show(message, Constants.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void Info(string message)
        {
            MessageBox.Show(message, Constants.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void Info(string message, IntPtr active)
        {
            MessageBox.Show(new DiskCatalog.Classes.Win32Window(active), message, "QuickCross", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static DialogResult ErrorOkCancel(string message)
        {
            DialogResult result = MessageBox.Show(message, Constants.MessageBoxTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            return result;
        }

        public static DialogResult WarningYesNoCancel(string message)
        {
            DialogResult result = MessageBox.Show(message, Constants.MessageBoxTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            return result;
        }

		public static DialogResult WarningOkCancel(string message)
		{
			DialogResult result = MessageBox.Show(message, Constants.MessageBoxTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
			return result;
		}

		public static DialogResult WarningYesNo(string message)
        {
            DialogResult result = MessageBox.Show(message, Constants.MessageBoxTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            return result;
        }

        public static DialogResult InfoYesNo(string message)
        {
            DialogResult result = MessageBox.Show(message, Constants.MessageBoxTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            return result;
        }
        public static DialogResult InfoYesNo(string message, IntPtr active)
        {
            return MessageBox.Show(new DiskCatalog.Classes.Win32Window(active), message, "QuickCross", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        }

        public static DialogResult ShowMessageOnWorkBook(string message, Constants.MessageType messageType, Workbook workbook)
        {
            Win32Window win32Window = new Win32Window(workbook.Application.Hwnd);
            return LoadMessage(message, win32Window, messageType);
        }
        public static DialogResult ShowMessageOnParent(string message, Constants.MessageType messageType, IntPtr parent)
        {
           // int intValue = Marshal.ReadInt32(parent);
            Win32Window win32Window = new Win32Window(parent);
            
            return LoadMessage(message, win32Window, messageType);
        }

        private static DialogResult LoadMessage(string message, IWin32Window owner, Constants.MessageType messageType)
        {
            DialogResult result;
            switch (messageType)
            {
                case Constants.MessageType.Warning:
                    result = MessageBoxEx.Warning(owner, message);
                    break;
                case Constants.MessageType.Error:
                    result = MessageBoxEx.Error(owner, message);
                    break;
                case Constants.MessageType.ErrorOk:
                    result = MessageBoxEx.ErrorOk(owner, message);
                    break;
                case Constants.MessageType.ErrorOkCancel:
                    result = MessageBoxEx.ErrorOkCancel(owner, message);
                    break;
                case Constants.MessageType.Info:
                default:
                    result = MessageBoxEx.Info(owner, message);
                    break;
            }
            return result;
        }


    }
}
