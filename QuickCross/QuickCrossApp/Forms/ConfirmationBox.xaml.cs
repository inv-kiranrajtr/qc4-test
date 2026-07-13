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

namespace Qc4Launcher.Forms
{
    /// <summary>
    /// Interaction logic for ConfirmationBox.xaml
    /// </summary>
    public partial class ConfirmationBox : Window
    {
        public ConfirmationBox()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void BtnSaveAs_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = null;
            txtDialogStatus.Text = MessageBoxResult.Cancel.ToString();
            this.Close();
        }

    }
}
