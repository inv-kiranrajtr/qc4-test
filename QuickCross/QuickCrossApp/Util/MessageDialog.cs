using DiskCatalog.Classes;
using Microsoft.Office.Interop.Excel;
using Qc4Launcher.Forms;
using Qc4Launcher.Logic.Gross_Tabulation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Interop;
using static Qc4Launcher.Util.Enums;
using Action = System.Action;
using IWin32Window = System.Windows.Forms.IWin32Window;

namespace Qc4Launcher.Util
{
    class MessageDialog
    {
        public static void Warning(string message)
        {
            MessageBox.Show(message, Constants.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static void Warning(string message, System.Windows.Window owner)
        {
            owner.Dispatcher.BeginInvoke(new Action(delegate
            {
                IntPtr inPtr = new WindowInteropHelper(owner).Handle;
                MessageBox.Show(new Win32Window(inPtr), message, Constants.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }));
        }

        public static DialogResult WarningOkCancel(string message)
        {
            DialogResult result = MessageBox.Show(message, Constants.MessageBoxTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            return result;
        }

        public static void Error(string message)
        {
            MessageBox.Show(message, Constants.MessageBoxTitle, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
        }


        public static void ErrorOk(string message)
        {
            MessageBox.Show(message, Constants.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void ErrorOk(string message , System.Windows.Window owner)
        {
            owner.Dispatcher.BeginInvoke(new Action(delegate
            {
                IntPtr inPtr = new WindowInteropHelper(owner).Handle;
                MessageBox.Show(new Win32Window(inPtr),message, Constants.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }));
        }
        public static void ErrorOkFromCross(string message, CrossTabulation ct)
        {
            System.Windows.MessageBox.Show(ct, message, Constants.MessageBoxTitle, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
        }

        public static void Info(string message)
        {
            MessageBox.Show(message, Constants.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Info(string message, System.Windows.Window owner)
        {
            owner.Dispatcher.BeginInvoke(new Action(delegate
            {
                IntPtr inPtr = new WindowInteropHelper(owner).Handle;
                MessageBox.Show(new Win32Window(inPtr), message, Constants.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }));
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

        public static DialogResult InfoYesNo(string message, MessageBoxIcon icon)
        {
            DialogResult result = MessageBox.Show(message, Constants.MessageBoxTitle, MessageBoxButtons.YesNo, icon);
            return result;
        }

        public static DialogResult ShowMessageOnWorkBook(string message, MessageType messageType, Workbook workbook,MessageBoxIcon icon = MessageBoxIcon.Information)
        {
            Win32Window win32Window = new Win32Window(workbook.Application.Hwnd);
            return LoadMessage(message, win32Window, messageType,icon);
        }
        public static DialogResult ShowMessageOnForm(string message, MessageType messageType, IntPtr inPtr, MessageBoxIcon icon = MessageBoxIcon.Information)
        {
            Win32Window win32Window = new Win32Window(inPtr);
            return LoadMessage(message, win32Window, messageType, icon);
        }
        public static void ShowMessageOnForm(string message, System.Windows.Window window = null)
        {
            if (window != null)
            {
                IntPtr inPtr = new IntPtr();
                window.Dispatcher.BeginInvoke(new Action(delegate
                {
                    inPtr = new WindowInteropHelper(window).Handle;
                    LoadMessage(message, new Win32Window(inPtr), MessageType.ErrorOk);
                }));
            }
        }
        public static DialogResult ShowMessageOnWorkBookPR(string message, MessageType messageType, Workbook workbook, MessageBoxIcon icon = MessageBoxIcon.Information, System.Windows.Window window = null)
        {
            DialogResult res = new DialogResult();
            if (window != null)
            {
                IntPtr inPtr=new IntPtr();
                window.Dispatcher.BeginInvoke(new Action(delegate
                {
                    inPtr = new WindowInteropHelper(window).Handle;
                    LoadMessage(message, new Win32Window(inPtr), messageType);
                })); 
                return res;
            }
            Win32Window win32Window = new Win32Window(workbook.Application.Hwnd);
            return LoadMessage(message, win32Window, messageType, icon);
        }

        public static void ShowMessageOnWindow(string message, MessageType messageType, IntPtr inPtr)
        {
            LoadMessageWindow(message, new Win32Window(inPtr), messageType);
        }

        public static void ShowMessageOnWorkBook(string message, MessageType messageType, System.Windows.Window window)
        {
            IntPtr inPtr;
            window.Dispatcher.BeginInvoke(new Action(delegate
            {
                inPtr = new WindowInteropHelper(window).Handle;
                LoadMessage(message, new Win32Window(inPtr), messageType);
            }));
        }

        public static void ShowMessageOnWorkBook(string message, MessageType messageType, System.Windows.Window window, GTOptions gtOption)
        {
            IntPtr inPtr;
            window.Dispatcher.BeginInvoke(new Action(delegate
            {
                inPtr = new WindowInteropHelper(window).Handle;
                LoadMessage(message, new Win32Window(inPtr), messageType);
                if(null != gtOption) Process.Start(gtOption.GroupFolderPath);
            }));
        }

        public static void ShowMessageOnWorkBook(string message, MessageType messageType, Workbook workbook, IntPtr inPtr)
        {
            if (inPtr != default(IntPtr))
            {
                ShowMessageOnWindow(message, messageType, inPtr);
            }
            else
            {
                ShowMessageOnWorkBook(message, messageType, workbook);
            }
        }
        private static DialogResult LoadMessage(string message, IWin32Window owner, MessageType messageType,MessageBoxIcon icon = MessageBoxIcon.Information)
        {
            switch (messageType)
            {
                case MessageType.Warning:
                    return MessageBoxEx.Warning(owner, message);
                case MessageType.Error:
                    return MessageBoxEx.Error(owner, message);
                case MessageType.ErrorOk:
                    return MessageBoxEx.ErrorOk(owner, message);
                case MessageType.ErrorOkCancel:
                    return MessageBoxEx.ErrorOkCancel(owner, message);
                case MessageType.InfoYesNo:
                    return MessageBoxEx.InfoYesNo(owner, message,icon);
                case MessageType.Info:
                default:
                    return MessageBoxEx.Info(owner, message, icon);
            }
        }
        private static DialogResult LoadMessageWindow(string message, IWin32Window owner, MessageType messageType)
        {
            switch (messageType)
            {
                case MessageType.Warning:
                    return MessageBox.Show(owner, message, Constants.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                case MessageType.Error:
                    return MessageBox.Show(owner, message, Constants.MessageBoxTitle, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                case MessageType.ErrorOk:
                    return MessageBox.Show(owner, message, Constants.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                case MessageType.ErrorOkCancel:
                    return MessageBox.Show(owner, message, Constants.MessageBoxTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                case MessageType.InfoYesNo:
                    return MessageBox.Show(owner, message, Constants.MessageBoxTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                case MessageType.Info:
                default:
                    return MessageBox.Show(owner, message, Constants.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


    }
}
