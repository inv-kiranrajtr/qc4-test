using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyGeneration
{
    class MessageDialog
    {
        public static void Warning(string message)
        {
            MessageBox.Show(message, "QuickCross", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void ErrorOk(string message)
        {
            MessageBox.Show(message, "QuickCross", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void Info(string message)
        {
            MessageBox.Show(message, "QuickCross", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
       
    }
}
