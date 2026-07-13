using QC4Common.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Drawing;
using Excel = Microsoft.Office.Interop.Excel;
using ExcelUtil = QC4Common.Util.ExcelUtil;
using System.Data;
using QC4Common.Common;

using System.Threading;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using ExcelAddIn.Common;
using log4net;
using System.Reflection;
using NPOI.SS.Formula.Functions;
using System.Drawing.Imaging;
using Macromill.QCWeb.COMOperate;
using Constants = QC4Common.Common.Constants;

namespace Qc4Launcher.Forms.QuestionSetting
{
    /// <summary>
    /// Interaction logic for QuestionSettingMainWindow.xaml
    /// </summary>
    public partial class QuestionSettingMainWindow : Window
    {
        private static MainWindow mAinWindow = null;
        private static Excel.Workbook excelWorkBook = null;
        private string title = string.Empty;
        private string tempPath = string.Empty;
        private bool deleteprocess = false;
        public List<int> KeyCodes = new List<int>() { 38,40};
        bool addqus = false;
        public  double large = 0;
        int totalquestion = 0;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public int prevoiusselecteditem=-1;
        public bool isPro = false;

        public QuestionSettingMainWindow(MainWindow mainWindow, Excel.Workbook workBook, string title,string tmppath)
        {
            mAinWindow = mainWindow;
            this.title = title;
            this.tempPath = tmppath;
            excelWorkBook = workBook;
            InitializeComponent();
            DisableButtons();
            Disablekey();
            GetTitle();
            DisplayDetails();
            isPro = Qc4Launcher.Util.CommonFunction.ActivationKeyChecking();

        }

        private void AddQuestion_btn_Click(object sender, RoutedEventArgs e)
        {
           
           
            totalquestion = Util.Definiotion.VariableDictionary.Count;
            addqus = true;
            this.Hide();
            AddQuestion addQuestion = new AddQuestion(this, excelWorkBook, tempPath);
            addQuestion.ShowDialog();
            addQuestion.Owner = this;

        }
        private void Headingchange(object sender, RoutedEventArgs e)
        {
            try
            {
                if (qstitle.Text == "")
                {
                    Excel.Worksheet s = Util.ExcelUtil.GetWorkSheetByCodeName(excelWorkBook, Util.Constants.SheetCodeName.QuestionSetting);
                    Excel.Range r = s.Cells[2, 12];
                    r.Value = title;
                }
                else
                {
                    Excel.Worksheet s = Util.ExcelUtil.GetWorkSheetByCodeName(excelWorkBook, Util.Constants.SheetCodeName.QuestionSetting);
                    Excel.Range r = s.Cells[2, 12];
                    r.Value = qstitle.Text;
                }
            }
            catch { }

        }
        private void ResetTitle(object sender, RoutedEventArgs e)
        {
            try
            {
                Excel.Worksheet setting = Util.ExcelUtil.GetWorkSheetByCodeName(excelWorkBook, Util.Constants.SheetCodeName.Setting);
                Excel.Range servay = setting.Cells[QC4Common.Common.Constants.SourceSarvaytitile.servay_Row, QC4Common.Common.Constants.SourceSarvaytitile.servay_col];
                qstitle.Text = servay.Value;
            }
            catch { }

        }
      
        private void Edit_btn_Click(object sender, RoutedEventArgs e)
        {
            int index = prevoiusselecteditem= MyListView.SelectedIndex;
            addqus = true;
            this.Hide();
            Qc4Launcher.Forms.QuestionSetting.QuestionSettingEdit edit = new QuestionSettingEdit(index, excelWorkBook, this);
            edit.ShowDialog();
            edit.Owner = this;
        }
        private void Close_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
        [DllImport("user32.dll")]
        static extern IntPtr GetActiveWindow();
        private void Export_questionnair(object sender, RoutedEventArgs e)
        {
            IntPtr active = GetActiveWindow();
            Excel.Worksheet s = Util.ExcelUtil.GetWorkSheetByCodeName(excelWorkBook, Util.Constants.SheetCodeName.Setting);
            Excel.Range errorflag = s.Cells[QC4Common.Common.Constants.Exportqustionform.row + 2, QC4Common.Common.Constants.Exportqustionform.colunm];
            errorflag.Value = null;
            Excel.Range r = s.Cells[QC4Common.Common.Constants.Exportqustionform.row+1, QC4Common.Common.Constants.Exportqustionform.colunm];
            r.Value =active;
            /*Excel.Range rr = s.Cells[QC4Common.Common.Constants.Exportqustionform.row, QC4Common.Common.Constants.Exportqustionform.colunm];
             rr.Value = true;*/
            QC4Common.QS.QuestionnaireCreator qs = new QC4Common.QS.QuestionnaireCreator();
            qs.CreateQuesionnaire(excelWorkBook, QC4Common.Common.Constants.Exportqustionform.row + 1, QC4Common.Common.Constants.Exportqustionform.colunm);
            if (errorflag.Value!=null)
            {
                ExcelAddIn.Common.MessageDialog.ErrorOk(errorflag.Value);
            }
            COMWholeOperate.releaseComObject(ref r);
            //COMWholeOperate.releaseComObject(ref rr);
            COMWholeOperate.releaseComObject(ref s);
            GC.Collect();
        }


        private void Disablekey()
        {

        }
        private void GetTitle()
        {
            Excel.Worksheet s = Util.ExcelUtil.GetWorkSheetByCodeName(excelWorkBook, Util.Constants.SheetCodeName.QuestionSetting);
            qstitle.Text = s.Cells[2, 12].Value;
  
        }

        private void DisplayDetails()
        {
            DataTable table = new DataTable();
            table.Columns.Add();
            table.Columns.Add();
            table.Columns.Add();
            table.Columns.Add();
            table.Columns.Add();
            table.Columns.Add();
            DataRow dr;
            QC4Common.Util.FormUtil formUtil = new QC4Common.Util.FormUtil();
            large = 0;
            for (int i = 0; i < Util.Definiotion.VariableDictionary.Count; i++)
            {

                dr = table.NewRow();
                dr[0] = (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(i).QuestionFlag.ToString();
                if ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(i).Score == "" && (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(i).Choices.Count == 0)
                {
                    dr[1] = "";
                }
                else if ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(i).Score != "" && (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(i).Count == "")
                {
                    dr[1] = "W";
                }
                else if ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(i).AnswerType == Constants.AnswerType.MA && (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(i).Count != "")
                {
                    dr[1] = "C";
                }
                if ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(i).AddSubTotal != "")
                {
                    if ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(i).SubTotalCount > 0)
                    {
                        dr[2] = "S/" + (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(i).SubTotalCount;
                    }
                    else
                    {
                        dr[2] = "";
                    }
                }
                else
                {
                    dr[2] = "";
                }
                dr[3] = (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(i).Variable.ToString();
                if ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(i).CategoryCount > 0)
                {
                    dr[4] = (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(i).AnswerType.ToString() + "/" + (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(i).CategoryCount;
                }
                else
                {
                    dr[4] = (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(i).AnswerType.ToString();
                }

                dr[5] = formUtil.EscapeCRLF((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(i).Question.ToString());
                table.Rows.Add(dr);
                using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(new Bitmap(1,1)))
                {
                    SizeF size = graphics.MeasureString(Convert.ToString(dr[5]), new Font("Arial", 12, GraphicsUnit.Pixel));
                    if (large < size.Width)
                    {
                        large = size.Width;
                    }
                }
            }
            int a = table.Rows.Count;
            MyListView.ItemsSource = table.AsDataView();
        }
        private void DisableButtons()
        {
            editButton.IsEnabled = false;
            copyButton.IsEnabled = false;
            delButton.IsEnabled = false;
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            mAinWindow.Show();
        }

        private void DeleteQuestion(object sender, RoutedEventArgs e)
        {
            Util.QS.QuestionDelete questionDelete = new Util.QS.QuestionDelete();
            if (MyListView.SelectedItem != null)
            {
        
                if (MessageBoxResult.Yes == System.Windows.MessageBox.Show(LocalResource.QS_DELETE_WARNING, LocalResource.TITLE_QC, MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                    questionDelete.StartQuestionDelete(excelWorkBook, ((System.Data.DataRowView)MyListView.SelectedItem).Row.ItemArray[3].ToString(),true);
                    string qtype = Util.Definiotion.VariableDictionary[((System.Data.DataRowView)MyListView.SelectedItem).Row.ItemArray[3].ToString()].QuestionFlag;
                    string errormsg = string.Empty;
                    if (!questionDelete.Firstcheck)
                    {

                        if (!Util.QS.CheckVariableExistInSheets.DataProcess_sheet && !Util.QS.CheckVariableExistInSheets.Gt_sheet && !Util.QS.CheckVariableExistInSheets.Cross_sheet && !Util.QS.CheckVariableExistInSheets.Summery_T_sheet && !Util.QS.CheckVariableExistInSheets.FA_sheet && qtype != Constants.QuestionFlag.An)
                        {
                            errormsg = null;
                        }
                        else
                        {
                            errormsg = LocalResource.QS_DELETE_WARNING1;

                            if (Util.QS.CheckVariableExistInSheets.DataProcess_sheet)
                            {
                                errormsg += "\n" + LocalResource.QS_DELETE_WARNING2;
                            }
                            if (Util.QS.CheckVariableExistInSheets.Gt_sheet)
                            {
                                errormsg += "\n" + LocalResource.QS_DELETE_WARNING3;
                            }
                            if (Util.QS.CheckVariableExistInSheets.Cross_sheet)
                            {
                                errormsg += "\n" + LocalResource.QS_DELETE_WARNING4;
                            }
                            if (Util.QS.CheckVariableExistInSheets.Summery_T_sheet)
                            {
                                errormsg += "\n" + LocalResource.QS_DELETE_WARNING5;
                            }
                            if (Util.QS.CheckVariableExistInSheets.FA_sheet)
                            {
                                errormsg += "\n" + LocalResource.QS_DELETE_WARNING6;
                            }
                            if (Util.QS.CheckVariableExistInSheets.Data_After_process)
                            {
                                errormsg += "\n"+LocalResource.QS_DELETE_WARNING7;
                            }
                            if (qtype == Constants.QuestionFlag.An)
                            {
                                errormsg += "\n" + LocalResource.QS_DELETE_WARNING_MULTIVARIATE;
                            }
                            errormsg +="\n"+ LocalResource.QS_DELETE_WARNING8;
                        }
                    }
                    Util.QS.CheckVariableExistInSheets check = new Util.QS.CheckVariableExistInSheets();
                    check.Resetsheetstatus();

                    if (errormsg == null)
                    {
                        questionDelete.StartQuestionDelete(excelWorkBook, ((System.Data.DataRowView)MyListView.SelectedItem).Row.ItemArray[3].ToString());
                     
                        DisplayDetails();
                    }
                 
                    else
                    {
                        if (MessageBoxResult.Yes == System.Windows.MessageBox.Show(String.Format(errormsg), LocalResource.TITLE_QC, MessageBoxButton.YesNo, MessageBoxImage.Question))
                        {

                            questionDelete.StartQuestionDelete(excelWorkBook, ((System.Data.DataRowView)MyListView.SelectedItem).Row.ItemArray[3].ToString());
                             DisplayDetails();
                        }
                    }
                }
            }
            Util.QS.CheckVariableExistInSheets.DataProcess_sheet = Util.QS.CheckVariableExistInSheets.Cross_sheet = Util.QS.CheckVariableExistInSheets.Gt_sheet = Util.QS.CheckVariableExistInSheets.Summery_T_sheet = Util.QS.CheckVariableExistInSheets.FA_sheet = Util.QS.CheckVariableExistInSheets.Data_After_process = false;

        }
        private void Listview_selected(object sender, SelectionChangedEventArgs e)
        {
            editButton.IsEnabled = true;
            copyButton.IsEnabled = true;


            try
            {
                if (MyListView.SelectedItem != null)
                {
                    if (((System.Data.DataRowView)MyListView.SelectedItem).Row != null)
                    {
                        if (((System.Data.DataRowView)MyListView.SelectedItem).Row.ItemArray[0].ToString() != Constants.QuestionFlag.Org)
                        {
                            if (((System.Data.DataRowView)MyListView.SelectedItem).Row.ItemArray[0].ToString() == Constants.QuestionFlag.An && !isPro)
                            {
                                delButton.IsEnabled = false;
                            }
                            else
                            {
                                delButton.IsEnabled = true;
                            }
                        }
                        else
                        {
                            delButton.IsEnabled = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                _log.LogErrorForExcel(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

        }

       
        private void HandleCtrldownup(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
             
                if (e.Key == Key.Down)

                {
                    var selectedindex = MyListView.SelectedIndex;
                    if(selectedindex==MyListView.Items.Count-1)
                    {
                        MyListView.SelectedItem = MyListView.Items[selectedindex];
                    }
                    else
                    {
                        MyListView.SelectedItem = MyListView.Items[++selectedindex];
                    }
                }
                if(e.Key==Key.Up)
                {
                    var selectedindex = MyListView.SelectedIndex;
                    if (selectedindex ==0)
                    {
                        MyListView.SelectedItem = MyListView.Items[selectedindex];
                    }
                    else
                    {
                        MyListView.SelectedItem = MyListView.Items[--selectedindex];
                    }
                }
            }
        
        }

        private void Copy_btn_Click(object sender, RoutedEventArgs e)
        {
            addqus = true;
            prevoiusselecteditem = MyListView.SelectedIndex;
            totalquestion = Util.Definiotion.VariableDictionary.Count;
            this.Hide();
            Qc4Launcher.Forms.QuestionSetting.QuestionSettingCopy copy = new QuestionSettingCopy(this, excelWorkBook, tempPath,MyListView.SelectedIndex);
            copy.ShowDialog();
            copy.Owner = this;
        }

        private void window_Open(object sender, EventArgs e)
        {
           
            if (addqus)
            {
                DisplayDetails();
                editButton.IsEnabled = false;
                copyButton.IsEnabled = false;
                delButton.IsEnabled = false;
                addqus = false;
            }
            try
            {

              MyListViewGridview.Columns[5].Width =Convert.ToDouble (new DataGridLength(1, DataGridLengthUnitType.SizeToCells)); 

            }
            catch { }
            try
            {
                if (prevoiusselecteditem != -1)
                {
                    MyListView.SelectedIndex = prevoiusselecteditem;
                    prevoiusselecteditem = -1;
                    MyListView.ScrollIntoView(MyListView.SelectedItem);

                }
                if (totalquestion != 0)
                {
                    if (totalquestion < Util.Definiotion.VariableDictionary.Count)
                    {
                        MyListView.SelectedIndex = Util.Definiotion.VariableDictionary.Count - 1;
                        totalquestion = Util.Definiotion.VariableDictionary.Count;
                        MyListView.ScrollIntoView(MyListView.SelectedItem);

                    }
                }
            }
            catch(Exception ex)
            {
                string message = ex.Message;
                 _log.LogErrorForExcel(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void Move_edit_form(object sender, MouseButtonEventArgs e)
        {

            var originalSource = (DependencyObject)e.OriginalSource;
            while ((originalSource != null) && !(originalSource is System.Windows.Controls.ListViewItem)) originalSource = VisualTreeHelper.GetParent(originalSource);
            if (originalSource == null) return;
            int index = prevoiusselecteditem=MyListView.SelectedIndex;
            addqus = true;
            this.Hide();
            Qc4Launcher.Forms.QuestionSetting.QuestionSettingEdit edit = new QuestionSettingEdit(index, excelWorkBook, this);
            edit.ShowDialog();
            edit.Owner = this;
        }

        private void DisableRightMenu(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
       

      
    }
}



  

