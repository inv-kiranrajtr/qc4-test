using Qc4Launcher.Classes;
using Qc4Launcher.Util;
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
using Excel = Microsoft.Office.Interop.Excel;

namespace Qc4Launcher.Forms.QCM
{
    /// <summary>
    /// Interaction logic for QcmMainScreen.xaml
    /// </summary>
    public partial class QcmMainScreen : Window
    {
        private static Excel.Workbook excelWorkBook = null;
        private static string tempFolder = null;
        private static DataMerge mAinWindow = null;
        private static bool isOverWritePrompt = false;

        public QcmMainScreen(DataMerge mainWindow, Excel.Workbook workBook, string file1TempFolder)
        {
            excelWorkBook = workBook;
            tempFolder = file1TempFolder;
            mAinWindow = mainWindow;        
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            mAinWindow.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            radio_sjis.IsChecked = true;
        }

        private void DragableGridMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Qcm_cancel(object sender, RoutedEventArgs e)
        {
            mAinWindow.Show();
            this.Close();
        }

        private void Qcm_Convert(object sender, RoutedEventArgs e)
        {
            try
            {
                Encoding encode = Encoding.GetEncoding("shift_jis");
                if (radio_utf.IsChecked == true)
                {
                    encode = Encoding.GetEncoding("utf-8");
                }

                if (isOverWritePrompt)
                {
                    System.IO.File.Delete(txt_out.Text);
                    isOverWritePrompt = false;
                }
                else
                {
                    if (System.IO.File.Exists(txt_out.Text))
                    {
                        System.Windows.Forms.DialogResult result = MessageDialog.InfoYesNo(LocalResource.QCM_ALERT_FILEEXIST);
                        if (System.Windows.Forms.DialogResult.Yes == result)
                        {
                            System.IO.File.Delete(txt_out.Text);
                        }
                        else
                        { 
                            return;
                        }
                    }
                }

                QCMtoQC4Controller qcmController = new QCMtoQC4Controller(txt_qlayout.Text, txt_qrowdata.Text, System.IO.Path.GetDirectoryName(txt_out.Text), txt_out.Text, encode, this.Owner);
                if (qcmController.QcmToQc4_StartProcess())
                {
                    MessageDialog.Info(LocalResource.EX_EXPORT_SUCCESS);
                    this.Close();
                }
               
            }
            catch (Exception ex)
            {
                MessageDialog.Info(ex.Message);
            }
        }

        private void Open_qlayout_csvdata(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog1 = new Microsoft.Win32.OpenFileDialog();
            openFileDialog1.Multiselect = false;
            openFileDialog1.Filter = "Qlayoutファイル|*_Qlayout.csv";
            Nullable<bool> result = openFileDialog1.ShowDialog();
            if (result == true)
            {
                // Open document 
                txt_qlayout.Text = openFileDialog1.FileName;
            }
            EnableOrDisableButtons();
        }

        private void Open_qrewdata_tsvdata(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog1 = new Microsoft.Win32.OpenFileDialog();
            openFileDialog1.Multiselect = false;
            openFileDialog1.Filter = "Qrawdataファイル|*_Qrawdata.tsv";
            Nullable<bool> result = openFileDialog1.ShowDialog();
            if (result == true)
            {
                // Open document 
                txt_qrowdata.Text = openFileDialog1.FileName;
            }
            EnableOrDisableButtons();
        }

        private void Open_qc4_output(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            // Default file name
            dlg.DefaultExt = ".qc4"; // Default file extension
            dlg.Filter = "QC4 files |*.qc4"; // Filter files by extension

            // Show save file dialog box
            if (!String.IsNullOrEmpty(txt_out.Text))
                dlg.FileName = System.IO.Path.GetFileName(txt_out.Text);

            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                isOverWritePrompt = dlg.OverwritePrompt;
                // Save document
                txt_out.Text = dlg.FileName;
            }
            EnableOrDisableButtons();
        }

        private void EnableOrDisableButtons()
        {
            if (!(txt_qlayout.Text.Equals("")) && !(txt_qrowdata.Text.Equals("")))
            {
                btn_out.IsEnabled = true;
                if (!(txt_out.Text.Equals("")))
                {
                    StartConvert_btn.IsEnabled = true;
                }
                else
                    StartConvert_btn.IsEnabled = false;
            }
            else
            {
                btn_out.IsEnabled = false;
                StartConvert_btn.IsEnabled = false;
            }
        }
    }
}
