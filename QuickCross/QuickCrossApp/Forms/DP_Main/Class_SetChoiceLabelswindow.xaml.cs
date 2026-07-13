using log4net;
using System;
using System.Reflection;
using System.Windows;

namespace Qc4Launcher.Forms.DP_Main
{
    /// <summary>
    /// Interaction logic for Class_SetChoiceLabelswindow.xaml
    /// </summary>
    public partial class Class_SetChoiceLabelswindow : Window
    {
        private string inputUnits;
        private bool isThousandSeparatorEnabled = false;
        private bool isExecuted = false;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region Properties
        public string InputUnits
        {
            get
            {
                return inputUnits;
            }
            set
            {
                inputUnits = value;
            }
        }
        public bool IsThousandSeparatorEnabled
        {
            get
            {
                return isThousandSeparatorEnabled;
            }
            set
            {
                isThousandSeparatorEnabled = value;
            }
        }
        public bool IsExecuted
        {
            get
            {
                return isExecuted;
            }
            set
            {
                isExecuted = value;
            }
        }
        #endregion
        public Class_SetChoiceLabelswindow(string input, bool isThousandSeparatorChecked)
        {
            InitializeComponent();
            txt_inputUnits.Text = input;
            chk_thousandSeparator.IsChecked = isThousandSeparatorChecked;
        }

        private void Btn_execute_Click(object sender, RoutedEventArgs e)
        {
            isThousandSeparatorEnabled = chk_thousandSeparator.IsChecked.Value;
            inputUnits = txt_inputUnits.Text;
            isExecuted = true;
            this.Close();
        }

        private void Btn_closeBtn_Click(object sender, RoutedEventArgs e)
        {
            isThousandSeparatorEnabled = chk_thousandSeparator.IsChecked.Value;
            inputUnits = txt_inputUnits.Text;
            isExecuted = false;
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }

        private void Chk_thousandSeparator_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Window_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }
}
