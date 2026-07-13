using Qc4Launcher.Forms.QCM;
using Qc4Launcher.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Excel = Microsoft.Office.Interop.Excel;
namespace Qc4Launcher.Forms
{
    /// <summary>
    /// Interaction logic for DataMerge.xaml
    /// </summary>
    /// 
    public partial class DataMerge : Window
    {

        private static string newSelectedFile = null;
        private static Excel.Workbook excelWorkBook = null;
        private static string tempFolder = null;
        private static MainWindow mAinWindow = null;


        public DataMerge(MainWindow mainWindow, string selectedFile, Excel.Workbook workBook, string file1TempFolder)
        {
            mAinWindow = mainWindow;
            newSelectedFile = selectedFile;
            excelWorkBook = workBook;
            tempFolder = file1TempFolder;

            InitializeComponent();
            if (QC4Common.Common.Constants.GlobalMode.Split(',')[0] != "ja-JP")
                Button_Help.Visibility = Visibility.Hidden;
            else
                Button_Help.Visibility = Visibility.Visible;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (CommonFunction.ActivationKeyChecking())
            {
                //btn_ExternalData.IsEnabled = true;
                btn_CreateNewQCFile.IsEnabled = true;
                btn_CreateNewQCFile.Visibility = Visibility.Visible;
                Qc4Text.Visibility = Visibility.Visible;
            }
            else
            {
                //btn_ExternalData.IsEnabled = false;
                btn_CreateNewQCFile.IsEnabled = false;
                btn_CreateNewQCFile.Visibility = Visibility.Hidden;
                Qc4Text.Visibility = Visibility.Hidden;

            }
        }

        private void DragableGridMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        { 
            this.Close();
            //mAinWindow.Show();
        }

        private void btn_ExternalData_Click(object sender, RoutedEventArgs e)
        {
            ////if (CommonFunction.ActivationKeyChecking())
            ////{
            //DataImport dataImport = new DataImport(newSelectedFile);
            //new Thread(() => CloseMe()).Start();
            //dataImport.ShowDialog();
            ////}
            ////else
            ////    MessageDialog.Warning("Application is not licensed");
            ///
            mAinWindow.Hide();
            DataImport dataImport = new DataImport(mAinWindow, this, newSelectedFile, excelWorkBook, tempFolder);
            dataImport.Owner = this;
            dataImport.ShowDialog();
        }


        //private void CloseMe()
        //{
        //    Dispatcher.Invoke(() =>
        //    {
        //        this.Close();
        //    });
        //}

        private void btn_CombineSideWays_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(LocalResource.DATAMERGE_MESSAGE1, "QuickCross");
        }

        private void btn_CombineVertically_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(LocalResource.DATAMERGE_MESSAGE2, "QuickCross");
        }

        private void btn_ScreeningMatch_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(LocalResource.DATAMERGE_MESSAGE3, "QuickCross");
        }

        private void btn_CreateNewQCFile_Click(object sender, RoutedEventArgs e)
        {
         
            if (MainWindow.qc4FileActivestate)
            {
                MessageDialog.ErrorOk(LocalResource.QCM_ALERT_FILE_OPEN);
               
            }
            else
            {
                this.Hide();
                QcmMainScreen dataImport = new QcmMainScreen(this, excelWorkBook, tempFolder);
                dataImport.Owner = this;
                dataImport.ShowDialog();
            }
        }

		private void Window_Closed(object sender, EventArgs e)
		{
			mAinWindow.Show();
		}

        private void help_Btn_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(QC4Common.Classes.Help.GetHelpLink(QC4Common.Common.Constants.HelpButtonType.DATAMERGE));
        }
    }
}
