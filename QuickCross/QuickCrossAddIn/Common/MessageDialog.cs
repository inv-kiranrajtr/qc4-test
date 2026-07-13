using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExcelAddIn.Common
{
	public class MessageDialog
    {
        public static void Warning(string message)
        {
            MessageBox.Show(message, "QuickCross", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

		public static void WarningOkCancel(string message)
		{
			MessageBox.Show(message, "QuickCross", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
		}

		public static void Error(string message)
        {
            MessageBox.Show(message, "QuickCross", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
        }

        public static void ErrorOk(string message)
        {
            MessageBox.Show(message, "QuickCross", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void ErrorOk(string message, IntPtr active)
        {
            MessageBox.Show(new DiskCatalog.Classes.Win32Window(active),message, "QuickCross", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void Info(string message)
        {
            MessageBox.Show(message, "QuickCross", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void Info(string message, IntPtr active)
        {
            MessageBox.Show(new DiskCatalog.Classes.Win32Window(active), message, "QuickCross", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static DialogResult ErrorOkCancel(string message)
        {
            DialogResult result = MessageBox.Show(message, "QuickCross", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            return result;
        }

        public static DialogResult InfoResult(string message)
        {
            return MessageBox.Show(message, "QuickCross", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        public static DialogResult InfoYesNo(string message)
        {
            return MessageBox.Show(message, "QuickCross", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        }
        public static DialogResult InfoYesNo(string message, IntPtr active)
        {
            return MessageBox.Show(new DiskCatalog.Classes.Win32Window(active), message, "QuickCross", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        }
        public static DialogResult InfoYesNo(string message, MessageBoxIcon icon)
        {
            DialogResult result = MessageBox.Show(message, "QuickCross", MessageBoxButtons.YesNo, icon);
            return result;
        }
    }
}
