using QC4Common.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static Qc4Launcher.Util.Enums;

namespace Qc4Launcher.Forms
{
    /// <summary>
    /// Interaction logic for LauncherDialog.xaml
    /// </summary>
    public partial class LauncherDialog : Window
    {
        public LauncherDialog()
        {
            InitializeComponent();
            //LoadMessage(message, messageType);
            //this.Close();
        }

        internal void LoadMessage(string message, MessageType messageType)
        {
            switch (messageType)
            {
                case MessageType.Warning:
                    MessageDialog.Warning(message);
                    break;
                case MessageType.Error:
                    MessageDialog.Error(message);
                    break;
                case MessageType.ErrorOk:
                    MessageDialog.ErrorOk(message);
                    break;
                case MessageType.ErrorOkCancel:
                    MessageDialog.ErrorOkCancel(message);
                    break;
                //case MessageType.WarningYesNoCancel:
                //    MessageDialog.WarningYesNoCancel(message);
                //    break;
                case MessageType.Info:
                default:
                    MessageDialog.Info(message);
                    break;
            }
            Application.Current.Dispatcher.Invoke(delegate
            {
                this.Close();
            });
        }

    }
}
