using ExcelAddIn.Common;
using log4net;
using QC4Common.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
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
using static FilterSettingsView.FilterSettingsClass;
using Excel = Microsoft.Office.Interop.Excel;

namespace Qc4Launcher.Forms.FA
{
    /// <summary>
    /// Interaction logic for FA_List.xaml
    /// </summary>
    public partial class FA_List : Window
    {
        Excel.Workbook Workbook;
        MainWindow MainWnd = null;
        System.Windows.Controls.ComboBox combo = null;
        int LastSelected = 0;
        string LastSelectedText = "";
        bool FirstFocus = true;
        bool ButtonExecute = false;
        private ObservableCollection<DataExport> _qstnvariablDD1 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD2 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD3 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD4 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD5 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> RemovedItems = new ObservableCollection<DataExport>();
        List<DataExport> FAItems = new List<DataExport>();
        List<DataExport> AdditiuonalItems = new List<DataExport>();
        FilterSettingsView.FilterSettingsClass fs = new FilterSettingsView.FilterSettingsClass();
        QC4Common.Util.FormUtil formUtil = new QC4Common.Util.FormUtil();
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        System.Windows.Controls.DataGrid ExpGrid = null;
        public FA_List(Excel.Workbook _workbook, MainWindow _mainWnd)
        {
            MainWnd = _mainWnd;
            Workbook = _workbook;
            MainWnd.Hide();
            InitializeComponent();
        }

        private void Window_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadingData();
            LoadSavedFASetting();
            Dispatcher.BeginInvoke((Action)(() => Tab_Control.UpdateLayout()));
            Dispatcher.BeginInvoke((Action)(() => txt.Focus()));
        }

        private void LoadSavedFASetting()
        {
            try
            {
                Excel.Worksheet FAHiddenSheet = QC4Common.Util.ExcelUtil.GetWorkSheetByCodeName(Workbook, QC4Common.Common.Constants.SheetCodeName.FACreationS);
                FAHiddenSheet.Range["B5"].Value = LocalResource.CELL_ON;
                Excel.Range faStart = FAHiddenSheet.Cells[5, 2];
                Excel.Range faEnd = Util.ExcelUtil.EndxlUp(faStart);
                Excel.Range faRange = FAHiddenSheet.get_Range(faStart.Offset[0, 0], faEnd.Offset[0, 61]);
                object[,] faVal = faRange.Value2;
                if (faVal[1, 2] != null && faVal[1, 2].ToString() != "")
                {
                    DataExport item = _qstnvariablDD1.Where(x => x.QuestionVariable == faVal[1, 2].ToString()).FirstOrDefault();
                    if (item != null)
                        Combo_Classify_Item.SelectedItem = item;
                }
                for (int i = 1; i <= 30; i++)
                {
                    if (faVal[1, i + 2] == null || faVal[1, i + 2].ToString() == "")
                        break;
                    DataExport item = _qstnvariablDD2.Where(x => x.QuestionVariable == faVal[1, i + 2].ToString()).FirstOrDefault();
                    if (item != null)
                    {
                        _qstnvariablDD2.Remove(item);
                        _qstnvariablDD4.Add(item);
                    }
                }
                for (int i = 1; i <= 30; i++)
                {
                    if (faVal[1, i + 32] == null || faVal[1, i + 32].ToString() == "")
                        break;
                    DataExport item = _qstnvariablDD3.Where(x => x.QuestionVariable == faVal[1, i + 32].ToString()).FirstOrDefault();
                    if (item != null)
                    {
                        _qstnvariablDD3.Remove(item);
                        _qstnvariablDD5.Add(item);
                    }
                }

                LBVariablesFAGrid.ItemsSource = _qstnvariablDD2;
                RBVariablesFAGrid.ItemsSource = _qstnvariablDD4;
                LBVariablesGrid.ItemsSource = _qstnvariablDD3;
                RBVariablesGrid.ItemsSource = _qstnvariablDD5;

                Excel.Worksheet SettingSheet = Util.ExcelUtil.GetWorkSheetByCodeName(Workbook, QC4Common.Common.Constants.SheetCodeName.DetailsSetting);
                Excel.Range rarCom = SettingSheet.get_Range("A1", "C1000");
                var obj = rarCom.Value;
                int rowvalue = 1000;
                string name = "F_Fo_FAList_" + Check_Option_Sort.Name + "_S";
                for (int i = 2; i < rowvalue; i++)
                {
                    if (obj[i, 1] != null && obj[i, 1].ToString() == name && obj[i, 2] != null)
                    {
                        Check_Option_Sort.IsChecked = obj[i, 2].ToString() == "TRUE" ? true : false;
                    }
                    else if (obj[i, 1] == null)
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
            }
        }

        private void LoadingData()
        {
            _qstnvariablDD1.Add(new DataExport() { QuestionVariable = "", QuestionVariableType = "", Question = "" });
            for (int i = 0; i < Util.Definiotion.VariableDictionary.Count; i++)
            {
                QuestionSettings qs = Util.Definiotion.VariableDictionary.ElementAt(i).Value;
                DataExport itm = new DataExport()
                {
                    QuestionVariable = qs.Variable,
                    QuestionVariableType = qs.CategoryCount>0? qs.AnswerType +  "/" + qs.CategoryCount: qs.AnswerType,
                    Question = formUtil.EscapeCRLF(qs.Question),
                    QuestionIndex = i - 1,
                    QuestionChoiceNo = qs.QuestionNumber,
                    Choisces = qs.Choices
                };
                _qstnvariablDD3.Add(itm);
                AdditiuonalItems.Add(itm);
                if (qs.AnswerType == Util.Constants.AnswerType.SA || qs.AnswerType == Util.Constants.AnswerType.MA)
                {
                    _qstnvariablDD1.Add(itm);
                }
                else if (qs.AnswerType == Util.Constants.AnswerType.FA)
                {
                    FAItems.Add(itm);
                    _qstnvariablDD2.Add(itm);
                }
            }
            Combo_Classify_Item.DataContext = _qstnvariablDD1;
        }

        private void Combo_Classify_Item_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.ComboBox sen = ((System.Windows.Controls.ComboBox)sender);
            System.Windows.Controls.TextBox textBox = sen.Template.FindName("PART_EditableTextBox", sen) as System.Windows.Controls.TextBox;
            if (textBox != null)
            {
                textBox.AddHandler(System.Windows.Controls.TextBox.PreviewMouseDownEvent, new MouseButtonEventHandler(textBox_PreviewMouseDown), true);
            }
        }
        void textBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            combo.IsDropDownOpen = true;
        }

        private void Combo_Classify_Item_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (combo.SelectedIndex != 0)
                LastSelected = combo.SelectedIndex;
            else if (combo.SelectedIndex == 0)
            {
                LastSelectedText = combo.Text;
            }
            if (Key.Delete == e.Key && (this.Combo_Classify_Item.IsKeyboardFocusWithin || this.Combo_Classify_Item.IsDropDownOpen))
            {
                LastSelectedText = "";
                LastSelected = -1;
                e.Handled = false;
            }
            else if (Key.Delete == e.Key)
            {
                e.Handled = true;
            }
        }

        private void Combo_Classify_Item_GotFocus(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.ComboBox sen = ((System.Windows.Controls.ComboBox)sender);
            combo = sen;
        }

        private void Combo_Classify_Item_DropDownClosed(object sender, EventArgs e)
        {
            System.Windows.Controls.ComboBox sen = ((System.Windows.Controls.ComboBox)sender);
            if (sen.SelectedItem != null)
            {
                sen.Text = ((DataExport)(sen.SelectedItem)).QuestionVariable;
            }
            else
            {
                sen.Text = "";
            }
        }

        private void Combo_Classify_Item_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (!FirstFocus)
            {
                System.Windows.Controls.ComboBox sen = ((System.Windows.Controls.ComboBox)sender);
                sen.IsDropDownOpen = true;
            }
            else
                FirstFocus = false;
        }

        private void Combo_Classify_Item_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Combo_Classify_Item.IsEnabled = true;
                int selectedindex = Combo_Classify_Item.SelectedIndex;
                string NoOfChoice = string.Empty;
                if (selectedindex > 0)
                {

                    this.TBAnsType.Text = _qstnvariablDD1[selectedindex].QuestionVariableType.Split('/')[0];
                    try
                    {
                        _qstnvariablDD1[selectedindex].QuestionChoiceNo = _qstnvariablDD1[selectedindex].QuestionVariableType.Split('/')[1];
                    }
                    catch (Exception ex) { _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException); }
                    if (_qstnvariablDD1[selectedindex].QuestionChoiceNo != "")
                        this.TBNoOfChoice.Text = _qstnvariablDD1[selectedindex].QuestionChoiceNo.ToString();
                    else
                        this.TBNoOfChoice.Text = "";
                    this.TAQuestion.Text = formUtil.UnEscapeCRLF(_qstnvariablDD1[selectedindex].Question);
                }
                else
                {
                    this.TBAnsType.Text = "";
                    this.TBNoOfChoice.Text = "";
                    this.TAQuestion.Text = "";
                }
            }
            catch (Exception ex) { _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException); }
        }

        private void Combo_Classify_Item_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            System.Windows.Controls.ComboBox sen = ((System.Windows.Controls.ComboBox)sender);
            if ((Key.Delete == e.Key || ((sen.SelectedIndex == -1 && sen.Text == "") || (sen.SelectedIndex == -1 && sen.Text != ""))) && 
                (this.Combo_Classify_Item.IsKeyboardFocusWithin || this.Combo_Classify_Item.IsDropDownOpen))
            {
                LastSelected = 0;
                this.Combo_Classify_Item.SelectedIndex = 0;
            }
            else if (sen.SelectedItem == null && (sen.IsKeyboardFocusWithin || sen.IsDropDownOpen))
            {
                if (sen.Name != "Combo_Classify_Item")
                    sen.SelectedIndex = LastSelected;
            }
        }

        private void LBVariablesFAGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            var dg = (System.Windows.Controls.DataGrid)sender;
            ExpGrid = dg;
            dg.Focus();
        }

        private void LBVariablesFAGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            QC4Common.Util.GridCommonFunc.Arrow(sender, e);
        }

        private void b1SetColor(object sender, MouseButtonEventArgs e)
        {
            if (ExpGrid != null)
                ExpGrid.Focus();
        }

        private void ButtonSingleRightArrow_Click(object sender, RoutedEventArgs e)
        {
            if(LBVariablesFAGrid.SelectedItems.Count>0)
            {
                if((_qstnvariablDD4.Count+ LBVariablesFAGrid.SelectedItems.Count) > 30)
                {
                    QC4Common.Common.MessageDialog.ErrorOk(LocalResource.FA_MAX_LEFT_LIST);
                    return;
                }
                RemovedItems.Clear();
                for (int i = 0; i < LBVariablesFAGrid.SelectedItems.Count; i++)
                {
                    _qstnvariablDD4.Add(LBVariablesFAGrid.SelectedItems[i] as DataExport);
                }
                for (int i = 0; i < LBVariablesFAGrid.SelectedItems.Count; i++)
                {
                    RemovedItems.Add(LBVariablesFAGrid.SelectedItems[i] as DataExport);
                }
                for (int i = 0; i < RemovedItems.Count; i++)
                {
                    _qstnvariablDD2.Remove(RemovedItems[i]);
                }
                LBVariablesFAGrid.ItemsSource = _qstnvariablDD2.OrderBy(x=>x.QuestionIndex);
                RBVariablesFAGrid.ItemsSource = _qstnvariablDD4;
            }
        }

        private void ButtonSingleLefttArrow_Click(object sender, RoutedEventArgs e)
        {
            if (RBVariablesFAGrid.SelectedItems.Count > 0)
            {
                RemovedItems.Clear();
                for (int i = 0; i < RBVariablesFAGrid.SelectedItems.Count; i++)
                {
                    DataExport item = RBVariablesFAGrid.SelectedItems[i] as DataExport;
                    if (item.QuestionVariableType != null && item.QuestionVariableType != "")
                        _qstnvariablDD2.Add(item);
                }
                for (int i = 0; i < RBVariablesFAGrid.SelectedItems.Count; i++)
                {
                    RemovedItems.Add(RBVariablesFAGrid.SelectedItems[i] as DataExport);
                }
                for (int i = 0; i < RemovedItems.Count; i++)
                {
                    _qstnvariablDD4.Remove(RemovedItems[i]);
                }
                LBVariablesFAGrid.ItemsSource = _qstnvariablDD2.OrderBy(x => x.QuestionIndex);
                RBVariablesFAGrid.ItemsSource = _qstnvariablDD4;
            }
        }

        private void ButtonSingleRightArrow1_Click(object sender, RoutedEventArgs e)
        {
            if (LBVariablesGrid.SelectedItems.Count > 0)
            {
                if ((_qstnvariablDD5.Count + LBVariablesGrid.SelectedItems.Count) > 30)
                {
                    QC4Common.Common.MessageDialog.ErrorOk(LocalResource.FA_MAX_LEFT_LIST);
                    return;
                }
                RemovedItems.Clear();
                for (int i = 0; i < LBVariablesGrid.SelectedItems.Count; i++)
                {
                    _qstnvariablDD5.Add(LBVariablesGrid.SelectedItems[i] as DataExport);
                }

                for (int i = 0; i < LBVariablesGrid.SelectedItems.Count; i++)
                {
                    RemovedItems.Add(LBVariablesGrid.SelectedItems[i] as DataExport);
                }
                for (int i = 0; i < RemovedItems.Count; i++)
                {
                    _qstnvariablDD3.Remove(RemovedItems[i]);
                }
                LBVariablesGrid.ItemsSource = _qstnvariablDD3.OrderBy(x => x.QuestionIndex);
                RBVariablesGrid.ItemsSource = _qstnvariablDD5;
            }
        }

        private void ButtonSingleLefttArrow1_Click(object sender, RoutedEventArgs e)
        {
            if (RBVariablesGrid.SelectedItems.Count > 0)
            {
                RemovedItems.Clear();
                for (int i = 0; i < RBVariablesGrid.SelectedItems.Count; i++)
                {
                    DataExport item = RBVariablesGrid.SelectedItems[i] as DataExport;
                    if (item.QuestionVariableType != null && item.QuestionVariableType != "")
                        _qstnvariablDD3.Add(item);
                }
                for (int i = 0; i < RBVariablesGrid.SelectedItems.Count; i++)
                {
                    RemovedItems.Add(RBVariablesGrid.SelectedItems[i] as DataExport);
                }
                for (int i = 0; i < RemovedItems.Count; i++)
                {
                    _qstnvariablDD5.Remove(RemovedItems[i]);
                }
                LBVariablesGrid.ItemsSource = _qstnvariablDD3.OrderBy(x => x.QuestionIndex);
                RBVariablesGrid.ItemsSource = _qstnvariablDD5;
            }
        }

        private void Command_Execute_Click(object sender, RoutedEventArgs e)
        {
            if (CrossTabulation.checkUnprocessedNewQuestionDialog(Workbook, new System.Windows.Interop.WindowInteropHelper(this).Handle))
                return;

            if (RBVariablesFAGrid.Items.Count == 0)
            {
                MessageDialog.ErrorOk(LocalResource.FA_RLIST_EMPTY, new System.Windows.Interop.WindowInteropHelper(this).Handle);
                return;
            }
            string errorMessage = "";
            if (Combo_Classify_Item.Text != "" && !Util.Definiotion.VariableDictionary.ContainsKey(Combo_Classify_Item.Text))
            {
                errorMessage = LocalResource.ERR_MSG_INVALID_ITEM;
                MessageDialog.ErrorOk(errorMessage, new System.Windows.Interop.WindowInteropHelper(this).Handle);
                return;
            }
            for (int i = 0; i < _qstnvariablDD4.Count; i++)
            {
                if (!Util.Definiotion.VariableDictionary.ContainsKey(_qstnvariablDD4[i].QuestionVariable))
                {
                    errorMessage = (i+1)+LocalResource.ERR_MSG_INVALID_ITEM;
                    MessageDialog.ErrorOk(errorMessage, new System.Windows.Interop.WindowInteropHelper(this).Handle);
                    return;
                }
                else
                {
                    QuestionSettings variableDetails = Util.Definiotion.VariableDictionary[_qstnvariablDD4[i].QuestionVariable];
                    if (variableDetails.AnswerType != "FA")
                    {
                        errorMessage = (i + 1) + LocalResource.ERR_MSG_INVALID_FA_ITEM;
                        MessageDialog.ErrorOk(errorMessage, new System.Windows.Interop.WindowInteropHelper(this).Handle);
                        return;
                    }
                }

            }
            for (int i = 0; i < _qstnvariablDD5.Count; i++)
            {
                if (!Util.Definiotion.VariableDictionary.ContainsKey(_qstnvariablDD5[i].QuestionVariable))
                {
                    errorMessage = (i + 1) + LocalResource.ERR_MSG_INVALID_ITEM;
                    MessageDialog.ErrorOk(errorMessage, new System.Windows.Interop.WindowInteropHelper(this).Handle);
                    return;
                }
            }
            ButtonExecute = true;
            this.Close();
        }

        private void Command_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        [DllImport("user32.dll")]
        static extern IntPtr GetActiveWindow();
        private void Window_Closed(object sender, EventArgs e)
        {
            MainWnd.Show();
            MainWnd.Activate();
            if (ButtonExecute)
            {
                Excel.Worksheet worksheet = QC4Common.Util.ExcelUtil.GetWorkSheetByCodeName(Workbook, QC4Common.Common.Constants.SheetCodeName.FACreationS);
                IntPtr? active = GetActiveWindow();
                string filename = "";
                ExcelAddIn.Common.Definitions.VariableDictionary = Util.Definiotion.VariableDictionary;
                if (!Util.CommonFunction.ActivationKeyChecking())
                {
                    filename = Qc4Launcher.Util.Definiotion.SelectedFile;
                    filename = filename.Split('_')[0];
                }
                FAList.FAExecClick(worksheet, Workbook, Check_Option_Sort.IsChecked, active, filename);
                MainWnd.Activate();
            }
        }

        bool FocusedByTab = false;
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Tab)
            {
                if (Page_GT_Summary.IsFocused)
                {
                    FirstFocus = true;
                }
                else if(TAQuestion.IsFocused)
                {
                    e.Handled = true;
                    ButtonSingleRightArrow.Focus();
                }
                else if (Keyboard.FocusedElement is TextBox)
                {
                    FocusedByTab = true;
                }
                else if(ButtonSingleLefttArrow.IsFocused)
                {
                    Style style = Application.Current.FindResource("MyFocusVisual") as Style;
                    RBVariablesFAGrid.FocusVisualStyle = style;
                    RBVariablesFAGrid.Focus();
                    if (RBVariablesFAGrid.SelectedItem == null && RBVariablesFAGrid.Items.Count > 0)
                        RBVariablesFAGrid.SelectedIndex = 0;
                    e.Handled = true;
                }
                else if(RBVariablesFAGrid.IsFocused)
                {
                    Style style = Application.Current.FindResource("MyFocusVisual") as Style;
                    LBVariablesFAGrid.FocusVisualStyle = style;
                    LBVariablesFAGrid.Focus();
                    if (LBVariablesFAGrid.SelectedItem == null && LBVariablesFAGrid.Items.Count > 0)
                        LBVariablesFAGrid.SelectedIndex = 0;
                    e.Handled = true;
                }
                else if(LBVariablesFAGrid.IsFocused)
                {
                    ButtonSingleRightArrow1.Focus();
                    e.Handled = true;
                }
                else if (ButtonSingleLefttArrow1.IsFocused)
                {
                    Style style = Application.Current.FindResource("MyFocusVisual") as Style;
                    RBVariablesGrid.FocusVisualStyle = style;
                    RBVariablesGrid.Focus();
                    if (RBVariablesGrid.SelectedItem == null && RBVariablesGrid.Items.Count > 0)
                        RBVariablesGrid.SelectedIndex = 0;
                    e.Handled = true;
                }
                else if (RBVariablesGrid.IsFocused)
                {
                    Style style = Application.Current.FindResource("MyFocusVisual") as Style;
                    LBVariablesGrid.FocusVisualStyle = style;
                    LBVariablesGrid.Focus();
                    if (LBVariablesGrid.SelectedItem == null && LBVariablesGrid.Items.Count > 0)
                        LBVariablesGrid.SelectedIndex = 0;
                    e.Handled = true;
                }
                else if (LBVariablesGrid.IsFocused)
                {
                    Check_Option_Sort.Focus();
                    e.Handled = true;
                }
            }
        }

        private void TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (FocusedByTab)
            {
                TextBox tb = (TextBox)sender;
                tb.Dispatcher.BeginInvoke(new Action(() => tb.SelectAll()));
            }
            FocusedByTab = false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
                SaveFASettings();
        }

        private void SaveFASettings()
        {
            try
            {
                object[,] faData = new object[1, 62];

                faData[0, 0] = LocalResource.CELL_ON;
                faData[0, 1] = Combo_Classify_Item.SelectedItem == null ? "" : Combo_Classify_Item.Text;

                for (int i = 0; i < _qstnvariablDD4.Count; i++)
                {
                    faData[0, i + 2] = _qstnvariablDD4[i].QuestionVariable;
                }
                for (int i = 0; i < _qstnvariablDD5.Count; i++)
                {
                    faData[0, i + 32] = _qstnvariablDD5[i].QuestionVariable;
                }

                Excel.Worksheet FAHiddenSheet = QC4Common.Util.ExcelUtil.GetWorkSheetByCodeName(Workbook, QC4Common.Common.Constants.SheetCodeName.FACreationS);
                bool EEFlg = FAHiddenSheet.Application.EnableEvents;
                FAHiddenSheet.Application.EnableEvents = false;
                FAHiddenSheet.Cells[5, 2].Resize[1, 62].Value = faData;
                FAHiddenSheet.Application.EnableEvents = EEFlg;

                Excel.Worksheet SettingSheet = Util.ExcelUtil.GetWorkSheetByCodeName(Workbook, QC4Common.Common.Constants.SheetCodeName.DetailsSetting);
                Excel.Range rarCom = SettingSheet.get_Range("A1", "C1000");
                var obj = rarCom.Value;
                int rowvalue = 1000;
                string name = "F_Fo_FAList_" + Check_Option_Sort.Name + "_S";
                for (int i = 2; i < rowvalue; i++)
                {
                    if (obj[i, 1] != null && obj[i, 1].ToString() == name)
                    {
                        rarCom.Cells[i, 2] = Check_Option_Sort.IsChecked == true ? "TRUE" : "FALSE";
                    }
                    else if(obj[i, 1] == null)
                    {
                        rarCom.Cells[i, 1] = name;
                        rarCom.Cells[i, 2] = Check_Option_Sort.IsChecked == true ? "TRUE" : "FALSE";
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
            }
        }
    }
}
