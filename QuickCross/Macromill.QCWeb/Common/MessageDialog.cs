using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExcelAddIn.Common
{
    class MessageDialog
    {
        public static void Warning(string message)
        {
            MessageBox.Show(message, "QuickCross", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void Error(string message)
        {
            MessageBox.Show(message, "QuickCross", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
        }

        public static void ErrorOk(string message)
        {
            MessageBox.Show(message, "QuickCross", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void Info(string message)
        {
            MessageBox.Show(message, "QuickCross", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static DialogResult ErrorOkCancel(string message)
        {
            DialogResult result = MessageBox.Show(message, "QuickCross", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            return result;
        }
    }
}
