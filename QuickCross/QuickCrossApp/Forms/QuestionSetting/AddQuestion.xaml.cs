using Qc4Launcher.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using MessageBox = System.Windows.Forms.MessageBox;
using Vb = Microsoft.VisualBasic;
using log4net;
using System.Reflection;
using Macromill.QCWeb.Tabulation;

namespace Qc4Launcher.Forms.QuestionSetting
{
    /// <summary>
    /// Interaction logic for AddQuestion.xaml
    /// </summary>
    public partial class AddQuestion : Window
    {
        private string tempClipBoardData;
        string clipboardText = "";
        private bool Combo_boxflag = false;
        string previousvalue = string.Empty;
        DataTable dt;
        bool checksubsave = false;
        DataTable copydt = null;
        bool subtotalbtn = false;
        public bool Issave = false;
        int scoremeanpointcount = 0;
        DataTable subtotal_dt = null;
        private int prevoiuslimit = 1000;
        private bool gridloadfirst = true;
        private static Microsoft.Office.Interop.Excel.Workbook excelWorkBook = null;
        private static QuestionSettingMainWindow mAinWindow = null;
        string temppath = string.Empty;
        private int lastoccurance = 0;
        DataTable tempdt=null;
        private bool ischeckAllboxEnabled;
        System.Windows.Controls.CheckBox gridheadercheckboxall;
        System.Windows.Controls.Label labelcheckboxall;
        bool checkflag = true;
        bool issubtoalsave = false;
        bool iseditablestate = false;
        bool ischeckallmediumstate = true;
      bool  isimprossedkey=false;
        System.Windows.Controls.DataGridCell cell;
       bool EditSaveErrorflag=false;
        string previewsnumber = string.Empty;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        QC4Common.Util.QSUtil qstUil = new QC4Common.Util.QSUtil();
        QC4Common.Util.FormUtil frmutil = new QC4Common.Util.FormUtil();
        public AddQuestion(QuestionSettingMainWindow mainWindow, Microsoft.Office.Interop.Excel.Workbook workBook, string temppath)
        {
            mAinWindow = mainWindow;
            excelWorkBook = workBook;
            this.temppath = temppath;
            InitializeComponent();
            tempdt = FilltempDt();

        }
        private bool Check_any_changes()
        {
            bool value = false;
            if (removesingleForcheck(Text_QuestionA.Text.TrimStart().TrimEnd())!=(Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(Definiotion.VariableDictionary.Count - 1).TableHeading)
                return true;
            else if (removesingleForcheck(Text_QuestionB.Text.TrimStart().TrimEnd()) != (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(Definiotion.VariableDictionary.Count - 1).Question)
                return true;

            if ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(Definiotion.VariableDictionary.Count - 1).AnswerType == Constants.AnswerType.SA || (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(Definiotion.VariableDictionary.Count - 1).AnswerType == Constants.AnswerType.MA)
            {
                if (Combo_Category.Text != (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(Definiotion.VariableDictionary.Count - 1).Choices.Count.ToString())
                    return true;
                else if (Combo_Sort.Text != (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(Definiotion.VariableDictionary.Count - 1).Sort)
                    return true;
                else if (((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(Definiotion.VariableDictionary.Count - 1).AddSubTotal=="" && Check_SubTotal.IsChecked == true)&&(!checksubsave))
                    return true;
                else if (((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(Definiotion.VariableDictionary.Count - 1).AddSubTotal == "1" && Check_SubTotal.IsChecked == false) && (!checksubsave))
                    return true;
                if ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(Definiotion.VariableDictionary.Count - 1).AnswerType == Constants.AnswerType.MA)
                    {
                    if ((bool)CheckMACN.IsChecked && (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(Definiotion.VariableDictionary.Count - 1).CountBase.Length < 1)
                        return true;
                    }

                dt = dt.AsEnumerable().Zip<DataRow, DataRow, DataRow>(copydt.AsEnumerable(), (DataRow modif, DataRow orig) =>
                {
                    if (!orig.ItemArray.SequenceEqual<object>(modif.ItemArray))
                    {
                        value = true;
                    }
                    return modif;
                }).CopyToDataTable<DataRow>();

                if (value == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
        private void Subtotalsetting_btn_Click(object sender, RoutedEventArgs e)
        {
            Issave = false;
            EditSaveErrorflag = false;
          checksubsave = true;
            if (Text_Item_Name.Text.TrimStart() == (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(Definiotion.VariableDictionary.Count - 1).Variable && Text_Item_Name.IsEnabled == false)
            {
                if (Check_any_changes())
                {
                    checksubsave = false;
                    if (MessageBoxResult.Yes == System.Windows.MessageBox.Show(LocalResource.QS_SUBTOTAL_CONFIRMATION, LocalResource.TITLE_QC, MessageBoxButton.YesNo, MessageBoxImage.Question))
                    {
                        subtotalbtn = true;
                        EditQuestion();
                        if (!EditSaveErrorflag)
                        {
                            Set_Subtotal_values();
                            Qc4Launcher.Forms.QuestionSetting.SubtotalSetting sub = new SubtotalSetting(Util.Definiotion.VariableDictionary.Count - 1, subtotal_dt, excelWorkBook);
                           

                            sub.Closed += Subtotal_close;
                            this.ShowInTaskbar = false;
                            sub.ShowDialog();
                        }
                        else
                        {
                            EditSaveErrorflag = false;
                        }
                        
                    }
                }
                else
                {
                    subtotalbtn = true;
                     Set_Subtotal_values();
                        Qc4Launcher.Forms.QuestionSetting.SubtotalSetting sub = new SubtotalSetting(Util.Definiotion.VariableDictionary.Count - 1, subtotal_dt, excelWorkBook);
                        sub.Closed += Subtotal_close;
                    this.ShowInTaskbar = false;
                    sub.ShowDialog();
                    
                }

            }
            else
            {

                if (MessageBoxResult.Yes == System.Windows.MessageBox.Show(LocalResource.QS_SUBTOTAL_CONFIRMATION, LocalResource.TITLE_QC, MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                    subtotalbtn = true;
                    saveQuestion();
                    if (issubtoalsave)
                    {
                        if (!EditSaveErrorflag)
                        {
                            Set_Subtotal_values();
                            Qc4Launcher.Forms.QuestionSetting.SubtotalSetting sub = new SubtotalSetting(Util.Definiotion.VariableDictionary.Count - 1, subtotal_dt, excelWorkBook);
                           

                            sub.Closed += Subtotal_close;
                            this.ShowInTaskbar = false;
                            sub.ShowDialog();
                        }
                        else
                        {
                            EditSaveErrorflag = false;
                        }

                    }

                }
            }

            issubtoalsave = false;
        }
        private void Subtotal_close(object sender, EventArgs e)
        {
            this.ShowInTaskbar = true;
            var child = (sender as Qc4Launcher.Forms.QuestionSetting.SubtotalSetting);
            subtotal_dt = child.subdt;
            if (subtotal_dt.Rows.Count > 0)
            {
                Check_SubTotal.IsChecked = true;
                Check_SubTotal.Content = string.Format(LocalResource.ADDQS_SUBTOTAL_CHECK_LABEL, subtotal_dt.Rows.Count);
            }
            else
            {
                Check_SubTotal.IsChecked = true;
                Check_SubTotal.Content = string.Format(LocalResource.ADDQS_SUBTOTAL_CHECK_LABEL, string.Empty);
            }
            Text_Item_Name.IsEnabled = false;
            Combo_Answer_Type.IsEnabled = false;
            Combo_Category.SelectedIndex = (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(Util.Definiotion.VariableDictionary.Count - 1).Choices.Count + 1;
            Combo_Category.IsEnabled = false;
            try
            {
                Combo_Sort.SelectedIndex = Convert.ToInt32((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(Util.Definiotion.VariableDictionary.Count - 1).Sort) - 1;
            }
            catch(Exception ex)
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
            try
            {
                data_grid.Items.Refresh();
            }
            catch(Exception ex)
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
            subtotalbtn = false;
        }

        private void HideButtonsLabels()
        {
            Label_Category.Visibility = Visibility.Hidden;
            Combo_Category.Visibility = Visibility.Hidden;
            Check_Sort.Visibility = Visibility.Hidden;
            Label_Sort2.Visibility = Visibility.Hidden;
            Label_Sort3.Visibility = Visibility.Hidden;
            Combo_Sort.IsEnabled = false;
            Combo_Sort.Visibility = Visibility.Hidden;
            Check_SubTotal.Visibility = Visibility.Hidden;
            Command_Subtotal.IsEnabled = false;
            Command_Subtotal.Visibility = Visibility.Hidden;
            CheckMACN.IsEnabled = false;
            CheckMACN.Visibility = Visibility.Hidden;
            data_grid.Visibility = Visibility.Hidden;
          
            Label_MACN.Visibility = Visibility.Hidden;

        }
        private void LoadItemAnswertype()
        {

        }


        private void Check_SortStateChange(object sender, RoutedEventArgs e)
        {

            if ((bool)Check_Sort.IsChecked)
            {
                Combo_Sort.IsEnabled = true;
                changeCombo_sort();

            }
            else
            {
                Combo_Sort.Text = "";
                Combo_Sort.IsEnabled = false;
            }
        }
        private void AnswertypeChanged(object sender, SelectionChangedEventArgs e)
        {
           
            if (Text_Item_Name.Text.Length >= 1)
            {
                addqs_btn_save.IsEnabled = true;
            }

            if (((sender as System.Windows.Controls.ComboBox).SelectedItem as ComboBoxItem) != null)
            {
                if (((sender as System.Windows.Controls.ComboBox).SelectedItem as ComboBoxItem).Content.ToString() == "SA")
                {
                    EnablebuttonsLabelsforSA();
                }
                else if (((sender as System.Windows.Controls.ComboBox).SelectedItem as ComboBoxItem).Content.ToString() == "MA")
                {
                    EnablebuttonsLabelsforMA();
                }
                else if (((sender as System.Windows.Controls.ComboBox).SelectedItem as ComboBoxItem).Content.ToString() == "FA")
                {
                    HideButtonsLabels();
                }
                else if (((sender as System.Windows.Controls.ComboBox).SelectedItem as ComboBoxItem).Content.ToString() == "N")
                {
                    HideButtonsLabels();
                }
            }
            else
            {
                HideButtonsLabels();
               

            }
        }
        private void AddComboxitem()
        {
            Combo_Category.Items.Add(LocalResource.LBL_AUTO);
            for (int i = 0; i < 1000; i++)
            {
                Combo_Category.Items.Add(i + 1);
                Combo_Sort.Items.Add(i + 1);
            }
            Combo_Category.Text = Combo_Category.Items.GetItemAt(1).ToString();
            Combo_boxflag = true;
        }
        private void EnablebuttonsLabelsforSA()
        {
            Label_Category.Visibility = Visibility.Visible;
            Combo_Category.Visibility = Visibility.Visible;
            Check_Sort.Visibility = Visibility.Visible;
            Label_Sort2.Visibility = Visibility.Visible;
            Label_Sort3.Visibility = Visibility.Visible;
            Combo_Sort.Visibility = Visibility.Visible;
            Check_SubTotal.Visibility = Visibility.Visible;
            CheckMACN.Visibility = Visibility.Hidden;
            Label_MACN.Visibility = Visibility.Hidden;
            Command_Subtotal.Visibility = Visibility.Visible;

            data_grid.Visibility = Visibility.Visible;
            data_grid.Columns[2].IsReadOnly = false;
            data_grid.Columns[3].Visibility = Visibility.Collapsed;
           
            if (dt.Rows.Count < 1)
            {
                dt = FillDataGrid(1000);

                data_grid.DataContext = dt;
            }

           
       }
        private void EnablebuttonsLabelsforMA()
        {
            Label_Category.Visibility = Visibility.Visible;
            Combo_Category.Visibility = Visibility.Visible;
            Check_Sort.Visibility = Visibility.Visible;
            Label_Sort2.Visibility = Visibility.Visible;
            Label_Sort3.Visibility = Visibility.Visible;
            Combo_Sort.Visibility = Visibility.Visible;
            Check_SubTotal.Visibility = Visibility.Visible;
            Command_Subtotal.Visibility = Visibility.Visible;
            data_grid.Visibility = Visibility.Visible;
            CheckMACN.Visibility = Visibility.Visible;
            Label_MACN.Visibility = Visibility.Visible;
            data_grid.Columns[2].IsReadOnly = false;
           
            data_grid.Columns[3].Visibility = Visibility.Visible;
            if (dt.Rows.Count < 1)
            {
                dt = FillDataGrid(1000);
                data_grid.DataContext = dt;
            }
            
        }
        private void changeCombo_sort()
        {

            try
            {
                if ((bool)Check_Sort.IsChecked)
                {
                    int limit;
                    if (Combo_Category.SelectedIndex == -1 || Combo_Category.SelectedIndex == 1 || Combo_Category.SelectedIndex == 0)
                    {
                        limit = 1000;
                    }
                    else
                    {
                        limit = Combo_Category.SelectedIndex - 1;
                    }
                    Combo_Sort.Items.Clear();
                    for (int i = 1; i <= limit; i++)
                    {
                        Combo_Sort.Items.Add(i);
                    }
                    if (Combo_Category.SelectedIndex == 0)
                    {
                        Combo_Sort.Text = string.Empty;
                    }
                    else if (limit == 1000)
                    {
                        Combo_Sort.Text = "1";
                    }
                    else
                    {
                        Combo_Sort.Text = limit.ToString();
                    }
                }
            }
            catch(Exception ex)
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

        }


        private void Combo_Category_Validation(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                
                changeCombo_sort();
                if (Combo_Category.SelectedItem != null)
                {
                    if (Combo_Category.SelectedIndex > 1)
                    {
                        gridheadercheckboxall.IsEnabled = true;
                    }
                  
                    System.Windows.Controls.ComboBox combo = sender as System.Windows.Controls.ComboBox;
                    ObservableCollection<string> choicescount = new ObservableCollection<string>();
                    int limit = combo.SelectedIndex;
                    if (limit != 0)
                    {


                        if (limit == 1)
                        {
                            limit = 1000;
                        }
                        else
                        {
                            limit = limit - 1;
                        }

                        if (gridloadfirst)
                        {
                            if (combo != null)
                            {
                                dt = FillDataGrid(limit);
                                data_grid.DataContext = dt;
                                gridloadfirst = false;
                            }
                        }
                        else
                        {

                            if (prevoiuslimit < limit)
                            {
                                DataRow dr;

                                for (; prevoiuslimit < limit; prevoiuslimit++)
                                {
                                    dr = dt.NewRow();
                                    dr[0] = prevoiuslimit + 1;
                                    dr[1] = string.Empty;
                                    dr[2] = string.Empty;
                                    dr[3] = new System.Windows.Controls.CheckBox();
                                    dt.Rows.Add(dr);
                                }

                            }

                            else
                            {
                                int dtcount = dt.Rows.Count;
                                for (; limit < dtcount; dtcount--)
                                {
                                   dt.Rows.RemoveAt(dtcount - 1);
                               }

                            }

                        }

                        
                        prevoiuslimit = limit;
                    }

                }

               


                else
                {

                   

                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void Combo_sort_Validation(object sender, SelectionChangedEventArgs e)
        {

            if (Combo_Sort.SelectedItem == null)
            {

                Combo_Sort.Text = "";

            }
        }

        private void Check_SubTotalChange(object sender, RoutedEventArgs e)
        {
            if ((bool)Check_SubTotal.IsChecked)
            {
                Command_Subtotal.IsEnabled = true;
            }
            else
            {

                Command_Subtotal.IsEnabled = false;
            }
        }

        private void Close_btn_Click(object sender, RoutedEventArgs e)
        {
            EditSaveErrorflag = false;
            if (Text_Item_Name.Text.TrimStart() == (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(Definiotion.VariableDictionary.Count - 1).Variable && Text_Item_Name.IsEnabled == false)
            {
                if(Check_SubTotal.IsChecked==true && subtotal_dt.Rows.Count<1)
                {
                    if (MessageBoxResult.Yes == System.Windows.MessageBox.Show(LocalResource.QS_WARN_SAVE_CHANGE, LocalResource.TITLE_QC, MessageBoxButton.YesNo, MessageBoxImage.Question))
                    {
                        EditQuestion();

                        if (!EditSaveErrorflag)
                        {
                            copydt = null;
                            this.Close();
                        }
                    }
                    else
                    {
                        this.Close();
                    }
                }
              else if (Check_any_changes())
                {
                        
                    if (MessageBoxResult.Yes == System.Windows.MessageBox.Show(LocalResource.QS_WARN_SAVE_CHANGE, LocalResource.TITLE_QC, MessageBoxButton.YesNo, MessageBoxImage.Question))
                    {
                        EditQuestion();
                        
                        if (!EditSaveErrorflag)
                        {
                            copydt = null;
                            this.Close();
                        }
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }  
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            mAinWindow.Show();
        }
      
        private void Windowloaded(object sender, RoutedEventArgs e)
        {
            if (!subtotalbtn)
            {
                AddComboxitem();

                addqs_btn_save.IsEnabled = false;
                Check_SubTotal.Content = string.Format(LocalResource.ADDQS_SUBTOTAL_CHECK_LABEL, String.Empty);
                subtotal_dt = CreateTableSubtotal();
                HideButtonsLabels();
            }
            else
            {
                subtotalbtn = false;
                string value = string.Empty;
                if ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(Util.Definiotion.VariableDictionary.Count-1).SubTotalCount > 0)
                {
                    value = (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(Util.Definiotion.VariableDictionary.Count - 1).SubTotalCount.ToString();
                    Check_SubTotal.IsChecked = true;
                    Command_Subtotal.IsEnabled = true;

                }
                else
                {
                    Check_SubTotal.IsChecked = false;
                    Command_Subtotal.IsEnabled = false;
                }
               
                

            }
           
        }

        private void Add_New_question_Save_btn(object sender, RoutedEventArgs e)
        {
            Issave = true;
            if ((bool)Text_Item_Name.IsEnabled == false)
            {
                EditQuestion();
            }
            else
            {
                if (Util.Definiotion.VariableDictionary.Count < QC4Common.Common.Constants.ExcelRowColumnMax.ExcelQsMaxRowLimit)
                {
                    saveQuestion();
                }
                else
                {
                    MessageDialog.ErrorOk(LocalResource.QS_MAX_NO_EXCEED);
                }
               
            }

        }
        private void saveQuestion()
        {
            try
            {
                string addsubtotal = string.Empty;
                if (Text_Item_Name.Text.TrimStart().Length > 0)
                {
                   Util.QS.NewQuestionValidation validation = new Util.QS.NewQuestionValidation(Text_Item_Name.Text.TrimStart().TrimEnd());
                  
                    if (validation.Validation_Variable())
                    {
              
                        string qustion = Text_QuestionB.Text;
                        if (Text_QuestionB.Text.Replace(" ", string.Empty).Length != 0)
                        {
                         
                            Util.QS.AddNewQuestion addNew = new Util.QS.AddNewQuestion(excelWorkBook, temppath);
                            if (Combo_Answer_Type.Text == Constants.AnswerType.N || Combo_Answer_Type.Text == Constants.AnswerType.FA)
                            {
                                if (addNew.SaveToSheet(Text_Item_Name.Text.TrimStart().TrimEnd(), Combo_Answer_Type.Text, Text_QuestionA.Text.TrimStart().TrimStart().TrimEnd(), Text_QuestionB.Text.TrimStart().TrimStart().TrimEnd()))
                                {
                            
                                    
                                    if (!subtotalbtn)
                                    {
                                        MessageDialog.Info(LocalResource.ADDQS_INFO_MSG_SUCCESS);
                                        Resetall();
                                    }
                                    copydt = dt.Copy();

                                    issubtoalsave = true;
                                }
                                else
                                {
                                    issubtoalsave= false;
                                    EditSaveErrorflag = true;
                                }
                            }
                            else if (choiceCheck(dt))
                           
                            {
                             
                                if (Combo_Answer_Type.Text == Constants.AnswerType.SA)
                                {
                                    int index;
                                    if (Combo_Category.SelectedIndex == 1 || Combo_Category.SelectedIndex == 0)
                                    {
                                        index = lastoccurance + 1;
                                    }
                                    else
                                    {
                                        index = Combo_Category.SelectedIndex - 1;
                                    }
                                    if(Check_SubTotal.IsChecked==true)
                                    {
                                        addsubtotal = "1";
                                    }
                                    else
                                    {
                                        addsubtotal = string.Empty;
                                    }

                                    if (!((bool)Check_SubTotal.IsChecked == true && (!subtotalbtn) && (subtotal_dt.Rows.Count <= 0 || subtotal_dt == null)))
                                    {
                                       
                                        if (addNew.SaveToSheet(Text_Item_Name.Text.TrimStart().TrimEnd(), Combo_Answer_Type.Text, Text_QuestionA.Text.TrimStart().TrimEnd(), Text_QuestionB.Text.TrimStart().TrimEnd(), index,Combo_Sort.SelectedIndex, dt,addsubtotal:addsubtotal))
                                        {
                                            
                                            if (!subtotalbtn)
                                            {
                                                MessageDialog.Info(LocalResource.ADDQS_INFO_MSG_SUCCESS);
                                                Resetall();
                                            }
                                            copydt = dt.Copy();

                                            issubtoalsave = true;
                                        }
                                        else
                                        {
                                            issubtoalsave = false;
                                        }
                                    }
                                    else
                                    {
                                        MessageDialog.ErrorOk(LocalResource.QS_ERROR_CONFI_SUBTOTAL);
                                        issubtoalsave = false;
                                        EditSaveErrorflag = true;
                                    }
                                }
                                if (Combo_Answer_Type.Text == Constants.AnswerType.MA)
                                {
                                    int index;
                                    bool checkMacn = false;
                                    if ((bool)CheckMACN.IsChecked)
                                    {
                                        checkMacn = true;
                                    }
                                    if (Combo_Category.SelectedIndex == 1 || Combo_Category.SelectedIndex == 0)
                                    {
                                        index = lastoccurance + 1;
                                    }
                                    else
                                    {
                                        index = Combo_Category.SelectedIndex - 1;
                                    }
                                    if (Check_SubTotal.IsChecked == true)
                                    {
                                        addsubtotal = "1";
                                    }
                                    else
                                    {
                                        addsubtotal = string.Empty;
                                    }
                                    if (!((bool)Check_SubTotal.IsChecked == true && (!subtotalbtn) &&(subtotal_dt.Rows.Count <= 0||subtotal_dt==null)))
                                    {
                                        if (addNew.SaveToSheet(Text_Item_Name.Text.TrimStart().TrimEnd(), Combo_Answer_Type.Text, Text_QuestionA.Text.TrimStart().TrimEnd(), Text_QuestionB.Text.TrimStart().TrimEnd(), index, Combo_Sort.SelectedIndex, dt, checkMacn,addsubtotal:addsubtotal))
                                        {
                                           
                                            if (!subtotalbtn)
                                            {
                                                MessageDialog.Info(LocalResource.ADDQS_INFO_MSG_SUCCESS);
                                                Resetall();
                                            }
                                            copydt = dt.Copy();

                                            issubtoalsave = true;
                                        }
                                        else
                                        {
                                            issubtoalsave = false;
                                        }
                                    }
                                    else
                                    {
                                        MessageDialog.ErrorOk(LocalResource.QS_ERROR_CONFI_SUBTOTAL);
                                        issubtoalsave = false;
                                        EditSaveErrorflag = true;
                                    }
                                }
                            }
                            else
                            {
                                issubtoalsave = false;
                            }
                        }
                        else
                        {
                            MessageDialog.ErrorOk(LocalResource.ADDQS_ERROR_MSG_QUSTION_TEXT_MISS);
                            issubtoalsave=false;
                            EditSaveErrorflag = true;
                        }
                    }
                    else
                    {
                        if (Qc4Launcher.Util.QS.NewQuestionValidation.exist)
                        {
                            if (MessageBoxResult.Yes == System.Windows.MessageBox.Show(LocalResource.QS_VARIABLE_INCREMENT, LocalResource.TITLE_QC, MessageBoxButton.YesNo, MessageBoxImage.Question))
                            {
                                Text_Item_Name.Text = qstUil.GetVariableName(Text_Item_Name.Text, Util.Definiotion.VariableDictionary.Values.ToList());
                               saveQuestion();
                            }
                            else
                            {
                                issubtoalsave= false;
                                
                            }
                        }
                        else
                        {
                            issubtoalsave =true;
                            EditSaveErrorflag = true;
                        }
                    }
                   
                }
                else
                {
                    MessageDialog.ErrorOk(LocalResource.ADDQS_ERROR_MSG_EMPTY_VARIABLE);
                    issubtoalsave= false;
                    EditSaveErrorflag = true;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }
        private void EditQuestion()
        {
            string addsubtotal = string.Empty;
            string qustion = Text_QuestionB.Text;
            int selectedindex = Util.Definiotion.VariableDictionary.Count - 1;
            if (Text_QuestionB.Text.Replace(" ", string.Empty).Length != 0)
            {
                Util.QS.AddNewQuestion addNew = new Util.QS.AddNewQuestion(excelWorkBook, temppath);
                if (Combo_Answer_Type.Text == Constants.AnswerType.N || Combo_Answer_Type.Text == Constants.AnswerType.FA || Combo_Answer_Type.Text == "D")
                {
                    if (addNew.EditToSheet(Text_Item_Name.Text,Combo_Answer_Type.Text, Text_QuestionA.Text.TrimStart().TrimEnd(), Text_QuestionB.Text.TrimStart().TrimEnd(), 0, 0, null, false, selectedindex, false, 0))
                    {
                        if (!subtotalbtn)
                        {
                            MessageDialog.Info(LocalResource.ADDQS_INFO_MSG_SUCCESS);
                        }
                        copydt = dt.Copy();
                    }
                    else
                    {
                      EditSaveErrorflag = true;
                    }
                }
                else if (choiceCheck(dt))

                {
                    if (Combo_Answer_Type.Text == Constants.AnswerType.SA)
                    {
                        int index;
                        if (Combo_Category.SelectedIndex == 1 || Combo_Category.SelectedIndex == 0)
                        {
                            index = lastoccurance + 1;
                        }
                        else
                        {
                            index = Combo_Category.SelectedIndex - 1;
                        }
                        if (Check_SubTotal.IsChecked == true)
                        {
                            addsubtotal = "1";
                        }
                        else
                        {
                            addsubtotal = string.Empty;
                        }
                        if (!((bool)Check_SubTotal.IsChecked == true && subtotal_dt.Rows.Count <= 0) && (!subtotalbtn))
                        {
                            if (addNew.EditToSheet(Text_Item_Name.Text, Combo_Answer_Type.Text, Text_QuestionA.Text.TrimStart().TrimEnd(), Text_QuestionB.Text.TrimStart().TrimEnd(), index, Combo_Sort.SelectedIndex, dt, false, selectedindex, false, 0,addsubtotal:addsubtotal))

                            {
                                if (!subtotalbtn)
                                {
                                    MessageDialog.Info(LocalResource.ADDQS_INFO_MSG_SUCCESS);
                                }
                                copydt = dt.Copy();
                               
                            }
                            else
                            {
                                EditSaveErrorflag = true;
                            }
                        }
                        else
                        {
                            MessageDialog.ErrorOk(LocalResource.QS_ERROR_CONFI_SUBTOTAL);
                            EditSaveErrorflag = true;
                        }
                    }
                    if (Combo_Answer_Type.Text == Constants.AnswerType.MA)
                    {
                        int index;
                        bool checkMacn = false;
                        if ((bool)CheckMACN.IsChecked)
                        {
                            checkMacn = true;
                        }
                        if (Combo_Category.SelectedIndex == 1 || Combo_Category.SelectedIndex == 0)
                        {
                            index = lastoccurance + 1;
                        }
                        else
                        {
                            index = Combo_Category.SelectedIndex - 1;
                        }
                        if (Check_SubTotal.IsChecked == true)
                        {
                            addsubtotal = "1";
                        }
                        else
                        {
                            addsubtotal = string.Empty;
                        }
                        if (!((bool)Check_SubTotal.IsChecked == true && subtotal_dt.Rows.Count <= 0) && (!subtotalbtn))
                        {
                            if (addNew.EditToSheet(Text_Item_Name.Text, Combo_Answer_Type.Text, Text_QuestionA.Text.TrimStart().TrimEnd(), Text_QuestionB.Text.TrimStart().TrimEnd(), index, Combo_Sort.SelectedIndex, dt, checkMacn, selectedindex, false, 0,addsubtotal:addsubtotal))

                            {
                                if (!subtotalbtn)
                                {
                                    MessageDialog.Info(LocalResource.ADDQS_INFO_MSG_SUCCESS);
                                }
                                copydt = dt.Copy();
                              
                            }
                            else
                            {
                                EditSaveErrorflag = true;
                            }
                        }
                        else
                        {
                            MessageDialog.ErrorOk(LocalResource.QS_ERROR_CONFI_SUBTOTAL);
                            EditSaveErrorflag = true;
                        }
                    }
                }
                else
                {
                    EditSaveErrorflag = true;
                }
            }
            else
            {
                MessageDialog.ErrorOk(LocalResource.ADDQS_ERROR_MSG_QUSTION_TEXT_MISS);
                EditSaveErrorflag = true;
            }
        }
        private void Resetall()
        {
            try
            {
                Text_Item_Name.Text = string.Empty;
                Text_QuestionA.Text = string.Empty;
                Text_QuestionB.Text = string.Empty;
                Combo_Answer_Type.SelectedIndex = -1;
                Combo_Sort.SelectedIndex = -1;
                Check_Sort.IsChecked = false;
                Check_SubTotal.IsChecked = false;
                CheckMACN.IsChecked = false;
                dt.Rows.Clear();
                if (tempdt != null)
                {
                    tempdt.Rows.Clear();
                }
                gridloadfirst = true;
                Combo_Category.SelectedIndex = 1;
                addqs_btn_save.IsEnabled = false;
                if (gridheadercheckboxall.IsChecked == false)
                {
                    checkflag = false;
                }
                else
                {
                    checkflag = true;
                }
                gridheadercheckboxall.IsChecked = false;
                HideButtonsLabels();
            }
            catch(Exception ex) 
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }
        private bool choiceCheck(DataTable dt)
        {
          
            try
            {
                int i;

                var count = dt
              .AsEnumerable()

              .Where(p => p.Field<string>("Choice") == string.Empty)
              .Count();
                if (Combo_Category.SelectedIndex == 1)
                {
                  
                    if (count == 1000)
                    {
                        MessageDialog.ErrorOk(LocalResource.QS_CHOICE_NOT_SET);
                        return false;
                    }

                    for (i = 0; i < 1000; i++)
                    {
                        if (dt.Rows[i][1].ToString() != "")
                        {
                            lastoccurance = i;
                        }
                    }
                    for (i = 0; i < lastoccurance; i++)
                    {
                        if (dt.Rows[i][1].ToString() == "")
                        {
                            break;
                        }
                    }
                    if (i < lastoccurance)
                    {
                        MessageDialog.ErrorOk(string.Format(LocalResource.QS_CHOICE_LINE_ERROR, (i + 1).ToString())); 
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                   
                }
                else
                {
                    if (count == Combo_Category.SelectedIndex - 1)
                    {
                        MessageDialog.ErrorOk(LocalResource.QS_CHOICE_NOT_SET);
                        return false;
                    }
                    else
                    {
                        for (i = 0; i < Combo_Category.SelectedIndex - 1; i++)
                        {
                            if (dt.Rows[i][1].ToString() == "")
                            {
                                break;
                            }
                        }
                        if (i < Combo_Category.SelectedIndex - 1)
                        {
                            MessageDialog.ErrorOk(string.Format(LocalResource.QS_CHOICE_LINE_ERROR, (i + 1).ToString()));
                            return false;
                        }
                        else
                        {
                            return true;
                        }


                       
                    }
                }
              
            }
         
            catch (Exception ex)
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);   
                 return false; 
            }
            
        }



        private void Datagrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
           

        }


        private DataTable CreateTable()
        {
            DataTable griddata = new DataTable();
            griddata.Columns.Add("No");
            griddata.Columns.Add("Choice");
            griddata.Columns.Add("SMean");
            griddata.Columns.Add("cMean");
            return griddata;
        }
        private DataTable CreateTable1()
        {
            DataTable griddata = new DataTable();
            griddata.Columns.Add("Data");
            return griddata;
        }
        private DataTable FillDataGrid(int limit)
        {
            DataTable griddata = CreateTable();
            DataRow dr;
            for (int i = 1; i <= limit; i++)
            {
                dr = griddata.NewRow();
                dr["No"] = i;
                dr["Choice"] = String.Empty;
                dr["SMean"] = String.Empty;
                dr["cMean"] = String.Empty;
                griddata.Rows.Add(dr);
            }
            return griddata;
        }
        private DataTable FilltempDt()
        {

            DataTable griddata = CreateTable1();
            return griddata;
        }

        private void Listallchecked(object sender, RoutedEventArgs e)
        {
           
            try
            {
                if (data_grid.SelectedItems.Count > 0)
                {
                    data_grid.SelectedIndex = data_grid.SelectedIndex;
                    data_grid.CommitEdit();
                 
                }
                if (cell != null)
                {
                   
                    cell.Focus();
                }
                if (Combo_Category.SelectedIndex<2)
                {
                    if (tempdt.Rows.Count < 1 && tempdt != null)
                    {
                        if (gridheadercheckboxall.IsChecked == false)
                        {
                            checkflag = true;
                        }
                        else
                        {
                            checkflag = false;
                        }
                        gridheadercheckboxall.IsChecked = false;
                    }
                }
                if (Combo_Category.SelectedIndex == 0 || Combo_Category.SelectedIndex == 1)
                {
                    if (checkflag)
                    {
                        for (int i = 0; i < tempdt.Rows.Count; i++)
                        {
                            dt.Rows[i][3] = true;

                        }
                        gridheadercheckboxall.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                    }
                    checkflag = true;
                }
                else
                {
                    if (checkflag)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dt.Rows[i][3] = true;
                        }
                        gridheadercheckboxall.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                    }
                    checkflag = true;
                }
            }
            catch(Exception ex)
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void Listallunchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (iseditablestate)
                {
                    data_grid.SelectedIndex = data_grid.SelectedIndex + 1;
                }

                if (gridheadercheckboxall.Background.ToString() == "#FFE1E1E1")
                {
                    if (Combo_Category.SelectedIndex == 0 || Combo_Category.SelectedIndex == 1)
                    {

                        var falsecount = 0;
                        for (int i = 0; i < tempdt.Rows.Count; i++)
                        {
                            if ((string)dt.Rows[i][3] == "False")
                            {
                                falsecount++;
                            }
                        }
                        var emptycount = 0;
                        for (int i = 0; i < tempdt.Rows.Count; i++)
                        {
                            if ((string)dt.Rows[i][3] == string.Empty)
                            {
                                emptycount++;
                            }
                        }

                        if (tempdt.Rows.Count != (falsecount + emptycount))
                        {
                            for (int i = 0; i < tempdt.Rows.Count; i++)
                            {
                                dt.Rows[i][3] = true;
                                checkflag = true;

                            }
                            gridheadercheckboxall.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                            checkflag = true;
                        }
                    }
                    else
                    {
                        var falsecount = 0;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if ((string)dt.Rows[i][3] == "False")
                            {
                                falsecount++;
                            }
                        }
                        var emptycount = 0;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if ((string)dt.Rows[i][3] == string.Empty)
                            {
                                emptycount++;
                            }
                        }

                        if (dt.Rows.Count != (falsecount + emptycount))
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {

                                dt.Rows[i][3] = true;
                                checkflag = true;

                            }
                            gridheadercheckboxall.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                            checkflag = true;
                        }
                    }
                    
                }
                else
                {
                   
                    if (Combo_Category.SelectedIndex == 0 || Combo_Category.SelectedIndex == 1)
                    {
                        if (checkflag)
                        {
                            for (int i = 0; i < tempdt.Rows.Count; i++)
                            {
                                dt.Rows[i][3] = false;

                            }
                            gridheadercheckboxall.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                            checkflag = true;
                        }
                    }
                    else
                    {
                        if (checkflag)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                dt.Rows[i][3] = false;
                            }
                            gridheadercheckboxall.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                        }
                        checkflag = true;
                    }
                    var emtycount = dt
               .AsEnumerable()

               .Where(p => p.Field<string>("cMean") == string.Empty)
               .Count();
                    var failcount = dt
                .AsEnumerable()

                .Where(p => p.Field<string>("cMean") == "False")
                .Count();
                    if (dt.Rows.Count == emtycount + failcount)
                    {
                        if (gridheadercheckboxall.IsChecked == false)
                        {
                            checkflag = true;
                        }
                        else
                        {
                            checkflag = false;
                        }
                        gridheadercheckboxall.IsChecked = false;
                        CheckMACN.IsEnabled = false;
                        data_grid.Columns[2].IsReadOnly = false;
                    }
                }
                checkflag = true;
            }
            catch(Exception ex) 
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

        }

        private void cellend(object sender, DataGridCellEditEndingEventArgs e)

        {
            cell = sender as System.Windows.Controls.DataGridCell;
            previewsnumber = string.Empty;

              iseditablestate = true;
            try
            {
               
               
                System.Windows.Controls.TextBox a = e.EditingElement as System.Windows.Controls.TextBox;
                if (a != null)
                {
                    string aa = a.Text;
                    string sub = string.Empty;
                    string temp = string.Empty;
                    if (e.Column.DisplayIndex == 2)
                    {
                       
                            for (int i = 0; i < aa.Length; i++)
                            {
                                sub = sub + aa[i];
                                if (sub == ".")
                                    sub = "0.";
                                if (new Regex(@"^[-+]?\d+(\.\d+)?$").IsMatch(sub))
                                {
                                temp = sub;
                                }
                            }
                        aa = temp;
                        aa = aa.Normalize(NormalizationForm.FormKC);
                         aa= aa.TrimStart(new Char[] { '0' });
                        if (aa.Length == 0 &&sub.Length>=1)
                            aa = "0";
                    }

                    if (aa.Length >= 1)
                    {
                        if (e.Column.DisplayIndex == 2)
                        {
                            if(aa.StartsWith("."))
                            {
                                aa = "0" + aa;
                            }
                            if (!new Regex(@"^[-+]?\d+(\.\d+)?$").IsMatch(aa))
                            {
                                dt.Rows[e.Row.GetIndex()][e.Column.DisplayIndex] = "0";
                            }
                            else
                            {
                                if(aa.Length>30)
                                {
                                    aa = aa.Substring(0, 30);
                                }
                                dt.Rows[e.Row.GetIndex()][e.Column.DisplayIndex] = aa;
                            }

                        }

                        
                    }
                   
                    if (e.Column.DisplayIndex == 1 && (Combo_Category.SelectedIndex == 0 || Combo_Category.SelectedIndex == 1 || Combo_Category.SelectedIndex>2 ))
                    {
                        if (aa.Length >= 1)
                        {
                            if (tempdt.Rows.Count < 1)
                            {
                                gridheadercheckboxall.IsEnabled = true;
                                
                                for (int i = 0; i <=data_grid.SelectedIndex; i++)
                                {
                                    DataRow dr;
                                    dr = tempdt.NewRow();
                                    dr["Data"] = String.Empty;
                                    tempdt.Rows.Add(dr);


                                }
                            }
                            else
                            {
                                if (tempdt.Rows.Count < data_grid.SelectedIndex + 1)
                                {

                                    for (int i = tempdt.Rows.Count; i < data_grid.SelectedIndex + 1; i++)
                                    {

                                        DataRow dr;
                                        dr = tempdt.NewRow();
                                        dr["Data"] = String.Empty;
                                        tempdt.Rows.Add(dr);
                                    }
                                }
                            }
                        }
                        if (aa.Length < 1)
                        {
                            var emptycount = dt
             .AsEnumerable()

             .Where(p => p.Field<string>("Choice") == string.Empty)
             .Count();
                            if (emptycount == dt.Rows.Count)
                            {
                                tempdt.Rows.Clear();
                            }
                            if (data_grid.SelectedIndex == tempdt.Rows.Count - 1)
                            {
                                tempdt.Rows.RemoveAt(data_grid.SelectedIndex);
                            }
                            else
                            {
                                tempdt.Rows[data_grid.SelectedIndex][0] = string.Empty;
                            }
                        }
                        else
                        {
                            tempdt.Rows[data_grid.SelectedIndex][0] = aa.TrimStart().TrimEnd();
                            dt.Rows[data_grid.SelectedIndex][1] = aa.TrimStart().TrimEnd();
                        }
                        try
                        {
                            if (dt.Rows[data_grid.SelectedIndex][3].ToString() == "True")
                            {
                                CheckMACN.IsEnabled = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            string message = ex.Message;
                            _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                        }
                        var count = tempdt
              .AsEnumerable()

              .Where(p => p.Field<string>("Data") == string.Empty)
              .Count();
                        if(count==tempdt.Rows.Count)
                        {
                           
                        }
                    }

                }
                
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
            iseditablestate = false;
            scoremeanpointcount = 0;
        }

        private void getevnt(object sender, RoutedEventArgs e)
        {   
            conutbasecheck();
        }
        private void fail(object sender, RoutedEventArgs e)
        {
            failuncheck();
        }
        private void failuncheck()
        {
            try
            {
                int limit = 0;
                var count = dt
              .AsEnumerable()

              .Where(p => p.Field<string>("Choice") == string.Empty)
              .Count();
                if (Combo_Category.SelectedIndex == 1 || Combo_Category.SelectedIndex == 0)
                {
                    if (tempdt.Rows.Count >= 1)
                    {
                        var tempcount = tempdt
                  .AsEnumerable()

                  .Where(p => p.Field<string>("Data").Length >= 1)
                  .Count();
                        var falsecount = 0;
                        for (int i = 0; i < tempdt.Rows.Count; i++)
                        {
                            if ((string)dt.Rows[i][3] == "False")
                            {
                                falsecount++;
                            }
                        }
                        var emptycount = 0;
                        for (int i = 0; i < tempdt.Rows.Count; i++)
                        {
                            if ((string)dt.Rows[i][3] == string.Empty)
                            {
                                emptycount++;
                            }
                        }
                        var truecount = 0;
                        for (int i = 0; i < tempdt.Rows.Count; i++)
                        {
                            if ((string)dt.Rows[i][3] == "True")
                            {
                                truecount++;
                            }
                        }


                        if ((tempdt.Rows.Count == falsecount + emptycount || tempcount == falsecount) && truecount == 0)
                        {
                            CheckMACN.IsEnabled = false;
                            gridheadercheckboxall.IsEnabled = true;
                            if (gridheadercheckboxall.IsChecked == false)
                            {
                                checkflag = true;
                            }
                            else
                            {
                                checkflag = false;
                            }
                            gridheadercheckboxall.IsChecked = false;
                            data_grid.Columns[2].IsReadOnly = false;
                            gridheadercheckboxall.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                        }

                        else
                        {
                            for (int i = 0; i < tempdt.Rows.Count; i++)
                            {
                                if ((string)tempdt.Rows[i][0] != string.Empty && (string)dt.Rows[i][3] == "False")
                                {
                                    CheckMACN.IsEnabled = true;
                                    gridheadercheckboxall.IsEnabled = true;
                                    if (gridheadercheckboxall.IsChecked == true)
                                    {
                                        checkflag = true;
                                    }
                                    else
                                    {
                                        checkflag = false;
                                    }
                                    gridheadercheckboxall.IsChecked = true;
                                    data_grid.Columns[2].IsReadOnly = true;
                                    gridheadercheckboxall.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE1E1E1"));
                                }
                            }
                        }


                    }

                }
                else
                {
                    if (Combo_Category.SelectedIndex > 2 || Combo_Category.SelectedIndex < 1002)
                    {
                        var tempcount = dt
             .AsEnumerable()

             .Where(p => p.Field<string>("Choice").Length >= 1)
             .Count();
                        var falsecount = dt
             .AsEnumerable()

             .Where(p => p.Field<string>("cMean") == "False")
             .Count();
                        var emptycount = dt
            .AsEnumerable()

            .Where(p => p.Field<string>("cMean") == String.Empty)
            .Count();




                        if (dt.Rows.Count == falsecount + emptycount)
                        {
                            CheckMACN.IsEnabled = false;
                            gridheadercheckboxall.IsEnabled = true;
                            if (gridheadercheckboxall.IsChecked == false)
                            {
                                checkflag = true;
                            }
                            else
                            {
                                checkflag = true;
                            }
                            gridheadercheckboxall.IsChecked = false;
                            data_grid.Columns[2].IsReadOnly = false;
                            gridheadercheckboxall.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                        }
                        else
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if ((string)dt.Rows[i][3] == "False")
                                {
                                    CheckMACN.IsEnabled = true;
                                    gridheadercheckboxall.IsEnabled = true;
                                    if (gridheadercheckboxall.IsChecked == true)
                                    {
                                        checkflag = true;
                                    }
                                    else
                                    {
                                        checkflag = false;
                                    }

                                    gridheadercheckboxall.IsChecked = true;
                                    data_grid.Columns[2].IsReadOnly = true;
                                    gridheadercheckboxall.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE1E1E1"));
                                }
                            }
                        }

                    }
                }
            }
            catch(Exception ex) 
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }
        private void conutbasecheck()
        {
            try
            {
                
                int limit = 0;
                var count = dt
              .AsEnumerable()

              .Where(p => p.Field<string>("Choice") == string.Empty)
              .Count();
                if (Combo_Category.SelectedIndex == 1 || Combo_Category.SelectedIndex == 0)
                {
                    if (tempdt.Rows.Count >= 1)
                    {
                        var tempcount = tempdt
              .AsEnumerable()

              .Where(p => p.Field<string>("Data").Length >= 1)
              .Count();
                        var truecount = dt
             .AsEnumerable()

             .Where(p => p.Field<string>("cMean") == "True")
             .Count();
                        
                            int j = 0;
                            for(int i=0;i<tempdt.Rows.Count;i++)
                            {
                                if( (string)dt.Rows[i][3] =="True" )
                                {
                                    j++;
                                    CheckMACN.IsEnabled = true;
                                    gridheadercheckboxall.IsEnabled = true;
                                  if(gridheadercheckboxall.IsChecked == true)
                                    {
                                        checkflag = true;
                                    }
                                    else
                                    {
                                        checkflag = false;
                                    }
                                   
                                    gridheadercheckboxall.IsChecked = true;
                                    data_grid.Columns[2].IsReadOnly = true;
                                    gridheadercheckboxall.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE1E1E1"));
                                    gridheadercheckboxall.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE1E1E1"));
                                }
                            }
                            if (j >= tempdt.Rows.Count)
                            {
                                gridheadercheckboxall.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                            }
                        

                        
                    }
                }
                else
                {
                    if (Combo_Category.SelectedIndex > 2 || Combo_Category.SelectedIndex < 1002)
                    {
                        var tempcount = dt
             .AsEnumerable()

             .Where(p => p.Field<string>("Choice").Length >= 1)
             .Count();
                        var truecount = dt
             .AsEnumerable()

             .Where(p => p.Field<string>("cMean") == "True")
             .Count();



                        if (dt.Rows.Count == truecount)
                        {
                            CheckMACN.IsEnabled = true;
                            gridheadercheckboxall.IsEnabled = true;
                            if (gridheadercheckboxall.IsChecked == true)
                            {
                                checkflag = true;
                            }
                            else
                            {
                                checkflag =false;
                            }

                            gridheadercheckboxall.IsChecked = true;
                            data_grid.Columns[2].IsReadOnly = true;
                            data_grid.Columns[2].CellStyle = new Style(typeof(System.Windows.Controls.DataGridCell));
                            data_grid.Columns[2].CellStyle.Setters.Add(new Setter(System.Windows.Controls.DataGridCell.BackgroundProperty, new SolidColorBrush(Colors.LightBlue)));
                            
                            gridheadercheckboxall.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                        }
                        else
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if ((string)dt.Rows[i][3] == "True")
                                {
                                    CheckMACN.IsEnabled = true;
                                    gridheadercheckboxall.IsEnabled = true;
                                    if (gridheadercheckboxall.IsChecked == true)
                                    {
                                        checkflag = true;
                                    }
                                    else
                                    {
                                        checkflag = false;
                                    }
                                    gridheadercheckboxall.IsChecked = true;
                                    data_grid.Columns[2].IsReadOnly = true;
                                    gridheadercheckboxall.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE1E1E1"));
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

        }

        private void begincelledit(object sender, DataGridPreparingCellForEditEventArgs e)
        {
            
        }

        private void Combo_Category_PreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                System.Windows.Controls.ComboBox combo = (System.Windows.Controls.ComboBox)sender;
                string cmbotxt = combo.Text;
                cmbotxt.TrimStart(new Char[] { '0' });
                int result = 0;
                if (int.TryParse(cmbotxt, out result))
                {
                    if ((result > 1001 || result < 0))
                    {
                        MessageDialog.ErrorOk(LocalResource.ADDQS_ALERT_MSG_COMBO_CATEGORY);
                     
                        combo.SelectedIndex = 1001;
                    }
                    else
                    {
                        if (result == 0)
                        {
                            MessageDialog.ErrorOk(LocalResource.ADDQS_ALERT_MSG_COMBO_CATEGORY);
                            
                            combo.SelectedIndex = 2;
                        }
                        else
                        {
                            combo.SelectedIndex = result + 1;
                        }
                    }

                }
                else
                {
                    if (!(cmbotxt== LocalResource.LBL_AUTO))
                    {
                        MessageDialog.ErrorOk(LocalResource.ADDQS_ALERT_MSG_COMBO_CATEGORY);
                                                combo.SelectedIndex = 1001;
                    }
                 
                }
            }
            catch(Exception ex)
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void Combo_sort_PreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                System.Windows.Controls.ComboBox combo = (System.Windows.Controls.ComboBox)sender;

                int result = 0;
                if (int.TryParse(combo.Text, out result))
                {
                    int max;
                    if (Combo_Category.SelectedIndex == 0 || Combo_Category.SelectedIndex == 1)
                    {
                        max = 1000;
                    }
                    else
                    {
                        max = Combo_Category.SelectedIndex - 1;
                    }
                    if (!(result <= max && result >= 1))
                    {
                       
                        combo.SelectedIndex = 0;
                    }
                }
                else
                {

                   
                    combo.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

        }

    

        private void Handle_save_btn(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                System.Windows.Controls.TextBox txtbox = (System.Windows.Controls.TextBox)sender;
                if (txtbox.Text.Length >= 1)
                {
                    if (Combo_Answer_Type.SelectedIndex >= 0)
                    {
                        addqs_btn_save.IsEnabled = true;
                    }
                   
                }
                else
                {
                    addqs_btn_save.IsEnabled = false;
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }


        private void Checkbox_all_load(object sender, RoutedEventArgs e)
        {
            try
            {
                gridheadercheckboxall = (System.Windows.Controls.CheckBox)sender;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void label_countmean(object sender, RoutedEventArgs e)
        {
            try
            {
               labelcheckboxall = (System.Windows.Controls.Label)sender;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }
        private string GetDuplicateVariableName(string oldvariableName)
        {
            string newVariable = "N" + oldvariableName;
            int j = 1;
            for (int i = 0; i < Util.Definiotion.VariableDictionary.Count; i++)
            {
                if ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(i).Variable == newVariable)
                {
                    newVariable = "N" + j + oldvariableName;
                    i = 0;
                    j++;
                }
            }

            return newVariable;
        }
        private void Set_Subtotal_values()
        {
            try
            {
                if (subtotal_dt == null)
                {
                    subtotal_dt = FillSubTotalDatagrid((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(Util.Definiotion.VariableDictionary.Count-1).SubTotalCount);
                }
            }

            catch (Exception ex)
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }
        private DataTable CreateTableSubtotal()
        {
            DataTable griddata = new DataTable();
            griddata.Columns.Add("Criteria");
            griddata.Columns.Add("Subtotal");
            return griddata;
        }
        private DataTable FillSubTotalDatagrid(int limit)
        {
            DataTable griddata = CreateTableSubtotal();
            DataRow dr;
            for (int i = 1; i <= limit; i++)
            {
                dr = griddata.NewRow();
                dr["Criteria"] = (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(Util.Definiotion.VariableDictionary.Count - 1).SubTotals.ElementAt(i - 1).Criteria;
                dr["Subtotal"] = (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(Util.Definiotion.VariableDictionary.Count - 1).SubTotals.ElementAt(i - 1).Subtotal; ;
                griddata.Rows.Add(dr);
            }
            return griddata;
        }
        
        private void HandleCopyPaste(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                var uiElement = e.OriginalSource as UIElement;
                var ue = e.OriginalSource as FrameworkElement;
                int datagridColumn = data_grid.CurrentCell.Column.DisplayIndex;
                if (data_grid.SelectedIndex == -1)
                {
                    data_grid.SelectedIndex = 0;
                }
                System.Windows.Controls.DataGridCell cell = frmutil.GetCell(data_grid, data_grid.SelectedIndex, datagridColumn);
                if (data_grid.SelectedIndex >= 0)
                {
                    bool _altModifierPressed = (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl));
                    if (_altModifierPressed && Keyboard.IsKeyDown(Key.V))
                    {
                        try
                        {
                            QC4Common.Common.CommonFunctions comFunc = new QC4Common.Common.CommonFunctions();
                            tempClipBoardData = System.Windows.Clipboard.GetText();
                            System.Windows.Clipboard.SetText(comFunc.RemoveFullWidth(System.Windows.Clipboard.GetText(),datagridColumn));
                            Util.QS.GridCopyPaste copyPaste = new Util.QS.GridCopyPaste();
                            var data = copyPaste.PastetoDatagrid(sender);
                            int No_Row = copyPaste.No_Row;
                            int No_Column = copyPaste.No_Columns;
                            if (data == null) { e.Handled = true; }
                            if (!cell.IsEditing)
                            {

                                if (data != null)
                                {
                                    e.Handled = true;
                                    int datagridRow = data_grid.SelectedIndex;
                                    if (data_grid.CurrentCell.Column.DisplayIndex == 1)
                                    {

                                        if (No_Column > 2 || No_Row > (dt.Rows.Count - data_grid.SelectedIndex))
                                        {
                                            MessageDialog.ErrorOk(LocalResource.COPY_ERROR_MSG);
                                            e.Handled = true;
                                        }
                                        else
                                        {
                                            int RowIndex = data_grid.SelectedIndex;
                                            for (int i = 0; i < No_Row; i++, RowIndex++)
                                            {
                                                for (int j = 1; j <= No_Column; j++)
                                                {
                                                    DataRow dr;
                                                    if (j == 1)
                                                    {
                                                        if (tempdt.Rows.Count < 1)
                                                        {
                                                            for (int rowadditertion = 0; rowadditertion <= RowIndex; rowadditertion++)
                                                            {
                                                                dr = tempdt.NewRow();
                                                                dr["Data"] = String.Empty;
                                                                tempdt.Rows.Add(dr);
                                                            }
                                                        }
                                                        else if (tempdt.Rows.Count <= dt.Rows.Count)
                                                        {
                                                            if (tempdt.Rows.Count == RowIndex)
                                                            {
                                                                dr = tempdt.NewRow();
                                                                dr["Data"] = String.Empty;
                                                                tempdt.Rows.Add(dr);
                                                            }
                                                            else if (tempdt.Rows.Count < RowIndex)
                                                            {
                                                                for (int rowadditertion = tempdt.Rows.Count; rowadditertion <= RowIndex; rowadditertion++)
                                                                {
                                                                    dr = tempdt.NewRow();
                                                                    dr["Data"] = String.Empty;
                                                                    tempdt.Rows.Add(dr);
                                                                }
                                                            }
                                                        }
                                                        if (data[i, (j - 1)] == null)
                                                        {

                                                            dt.Rows[RowIndex][j] = tempdt.Rows[RowIndex][0] = String.Empty;
                                                        }
                                                        else
                                                        {
                                                            dt.Rows[RowIndex][j] = tempdt.Rows[RowIndex][0] = data[i, (j - 1)].ToString();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (j == 2 && data_grid.Columns[2].IsReadOnly == false)
                                                        {
                                                            if (data[i, (j - 1)] == null)
                                                            {
                                                                dt.Rows[RowIndex][j] = String.Empty;
                                                            }
                                                            else
                                                            {
                                                                dt.Rows[RowIndex][j] = comFunc.DataString(data[i, (j - 1)].ToString());
                                                            }
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else if (data_grid.CurrentCell.Column.DisplayIndex == 2)
                                    {

                                        if (No_Column > 1 || No_Row > (dt.Rows.Count - data_grid.SelectedIndex))
                                        {
                                            MessageDialog.ErrorOk(LocalResource.COPY_ERROR_MSG);
                                        }
                                        else
                                        {

                                            int RowIndex = data_grid.SelectedIndex;
                                            for (int i = 0; i < No_Row; i++, RowIndex++)
                                            {
                                                if (data_grid.Columns[2].IsReadOnly == false)
                                                {
                                                    if (data[i, 0] == null)
                                                    {
                                                        dt.Rows[RowIndex][2] = String.Empty;
                                                    }
                                                    else
                                                    {
                                                        dt.Rows[RowIndex][2] = comFunc.DataString(data[i, 0].ToString());
                                                    }

                                                }
                                            }
                                        }
                                    }
                                }

                            }
                            else
                            {
                                string regexReplacedStr = "";
                                if (System.Windows.Clipboard.ContainsText(System.Windows.TextDataFormat.UnicodeText))
                                {
                                    clipboardText = string.Empty;
                                    clipboardText = System.Windows.Clipboard.GetText(System.Windows.TextDataFormat.UnicodeText);
                                }
                                if (data != null && data[0, 0] != null)
                                    System.Windows.Clipboard.SetText(data[0, 0].ToString());
                            }
                            System.Windows.Clipboard.SetText(tempClipBoardData);
                        }
                        catch (Exception ex)
                        {
                            string message = ex.Message;
                            _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                        }
                        
                    }

                    try
                    {
                        if (e.Key == Key.Delete)
                        {
                            int RowIndex = 0;
                            if ((data_grid.SelectedItems != null) && (data_grid.SelectedItems.Count > 0))
                            {
                                for (int i = data_grid.SelectedItems.Count; i > 0; i--)
                                {
                                    var presentRow = (System.Data.DataRowView)data_grid.SelectedItems[i - 1];
                                    RowIndex = Convert.ToInt32(presentRow.Row.ItemArray[0].ToString());
                                    dt.Rows[RowIndex - 1][1] = String.Empty;
                                    dt.Rows[RowIndex - 1][2] = String.Empty;
                                    if (tempdt.Rows.Count > 0)
                                    {

                                        try
                                        {
                                            if ((RowIndex - 1) == tempdt.Rows.Count - 1)
                                            {
                                                tempdt.Rows.RemoveAt(RowIndex - 1);
                                            }
                                            else
                                            {
                                                tempdt.Rows[RowIndex - 1][0] = string.Empty;
                                            }
                                        }
                                        catch { }
                                    }
                                }
                                var emptycount = dt
                .AsEnumerable()

                .Where(p => p.Field<string>("Choice") == string.Empty)
                .Count();
                                if (emptycount == dt.Rows.Count)
                                {
                                    tempdt.Rows.Clear();
                                }
                                else
                                {
                                    if (Combo_Category.SelectedIndex == 0 || Combo_Category.SelectedIndex == 1)
                                    {
                                        tempdt.Rows.Clear();
                                        int last = 0;
                                        for (int j = 0; j < 1000; j++)
                                        {
                                            if (dt.Rows[j][1].ToString() != "")
                                            {
                                                last = j;
                                            }
                                        }
                                        for (int i = 0; i <= last; i++)
                                        {
                                            DataRow dr;
                                            dr = tempdt.NewRow();
                                            dr["Data"] = dt.Rows[i][1].ToString();
                                            tempdt.Rows.Add(dr);
                                        }
                                    }

                                }

                            }
                        }

                        int curentIndex = 0;
                        if (e.Key == Key.Enter)
                        {
                            curentIndex = data_grid.CurrentCell.Column.DisplayIndex;
                            e.Handled = true;
                            ue.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));

                        }

                        if (data_grid.Columns[3].Visibility == Visibility.Collapsed && data_grid.CurrentCell.Column.DisplayIndex == 1 && curentIndex == 2)
                        {
                            data_grid.CommitEdit();
                            data_grid.SelectedIndex = data_grid.SelectedIndex + 1;
                        }
                        if (data_grid.Columns[3].Visibility == Visibility.Visible&&data_grid.CurrentCell.Column.DisplayIndex == 1&& curentIndex ==3)
                        {
                            data_grid.CommitEdit();
                            data_grid.SelectedIndex = data_grid.SelectedIndex + 1;
                        }
                        if ((e.Key == Key.Tab) && data_grid.Columns[2].IsReadOnly == true)
                        {
                            if (data_grid.SelectedIndex >= 14 && data_grid.CurrentCell.Column.DisplayIndex == 3)
                            {
                                if (addqs_btn_save.IsEnabled == true)
                                {
                                    e.Handled = true;
                                    addqs_btn_save.Focus();
                                }
                                else
                                {
                                    e.Handled = true;
                                    Close_btn.Focus();
                                }
                            }
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        string message = ex.Message;
                        _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                    }
                }
            }
            catch { }


        }
        private void TextBox_scoreofmean_TextChanged(object sender, TextChangedEventArgs e)
        {
            System.Windows.Controls.TextBox txtgridtext = sender as System.Windows.Controls.TextBox;
            string input = txtgridtext.Text.Normalize(NormalizationForm.FormKC);
            if (!isimprossedkey)
            {
                e.Handled = new Regex(@"^[-]?([0-9]+(\.[0-9]*)?)?$").IsMatch(input);
                if ((e.Handled) && Vb.Information.IsNumeric(input))
                {
                    if (input.Length > 30)
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        previewsnumber = input;
                    }
                }
                else if (input.Length == 1 && !(e.Handled))
                {
                    input = previewsnumber = "0";
                    e.Handled = true;
                }
                else if (input.Length > 0 && !(e.Handled))
                    e.Handled = false;

                if (e.Handled == false && input.Length > 0) input = previewsnumber.Normalize(NormalizationForm.FormKC);

                try
                {
                    txtgridtext.Text = input.Normalize(NormalizationForm.FormKC);
                    txtgridtext.CaretIndex = input.Length;
                }
                catch { }
            }
           

        }
        private void CheckForRemovesubtotal()
        {

        }

        private string removesingleForcheck(string value)
        {
            if (value.StartsWith("'"))
            {
                value = value.Remove(0, 1);
            }
            return value;
        }

        private void DisableRightMenu(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
        private void textBox_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Copy)
            {
                e.Handled = true;
            }
        }

        private void data_grid_CopyingRowClipboardContent(object sender, DataGridRowClipboardEventArgs e)
        {
            try
            {
                e.ClipboardRowContent.RemoveAt(0);
                e.ClipboardRowContent.RemoveAt(2);
            }
            catch (Exception ex) { _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException); }
        }

        private void data_grid_PreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
         
                bool _altModifierPressed = (Keyboard.IsKeyUp(Key.LeftCtrl) || Keyboard.IsKeyUp(Key.RightCtrl));
                if (_altModifierPressed && e.Key == Key.C)
                {
                    if (System.Windows.Clipboard.ContainsText(System.Windows.TextDataFormat.UnicodeText))
                    {
                        clipboardText = string.Empty;
                        clipboardText = System.Windows.Clipboard.GetText(System.Windows.TextDataFormat.UnicodeText);
                    }
                }
                int datagridColumn = data_grid.CurrentCell.Column.DisplayIndex;
                System.Windows.Controls.DataGridCell cell = frmutil.GetCell(data_grid, data_grid.SelectedIndex, datagridColumn);
                if (cell.IsEditing)
                {
                    bool _altModifierPressed1 = (Keyboard.IsKeyUp(Key.LeftCtrl) || Keyboard.IsKeyUp(Key.RightCtrl));
                    if (_altModifierPressed1 && e.Key == Key.V)
                    {
                        if (!string.IsNullOrEmpty(clipboardText))
                        {
                            System.Windows.Clipboard.SetText(tempClipBoardData);
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void TextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            isimprossedkey = false;
            string a = e.ImeProcessedKey.ToString();
            if(a!="None")
            {
                isimprossedkey = true;
            }
        }
       
    }
   
}

