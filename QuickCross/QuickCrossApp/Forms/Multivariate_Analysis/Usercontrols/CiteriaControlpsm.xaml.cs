using FilterSettingsView;
using Macromill.QCWeb.Common;
using QC4Common.Model;
using Qc4Launcher.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using log4net;
using System.Reflection;
using QC4Common.Validation;
using static FilterSettingsView.FilterSettingsClass;
using Excel = Microsoft.Office.Interop.Excel;
using VB = Microsoft.VisualBasic;
using ExcelAddIn.Sheets;

namespace Qc4Launcher.Forms.Multivariate_Analysis.Usercontrols
{
    /// <summary>
    /// Interaction logic for CiteriaControlpsm.xaml
    /// </summary>
    public partial class CiteriaControlpsm : UserControl
    {
        public static Excel.Workbook _workbook;
        private Dictionary<String, QuestionSettings> PopulatedDictionary = new Dictionary<string, QuestionSettings>();
        private List<DataExport> dataFromSheet = new List<DataExport>();
        private bool IsInitialLoad = false;
        private ObservableCollection<DataExport> _qstnvariablDD1 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD2 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD3 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD4 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD5 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD6 = new ObservableCollection<DataExport>();
        private string Combo_Conditional_Item_1selectedQuestionVariable = string.Empty;
        private string Combo_Conditional_Item_2selectedQuestionVariable = string.Empty;
        private string Combo_Conditional_Item_3selectedQuestionVariable = string.Empty;
        private string Combo_Conditional_Item_4selectedQuestionVariable = string.Empty;
        private string Combo_Conditional_Item_5selectedQuestionVariable = string.Empty;
        private string Combo_Conditional_Item_selectedQuestionVariableType = string.Empty;
        private string Combo_Conditional_Item_1selectedQuestionVariableType = string.Empty;
        private string Combo_Conditional_Item_2selectedQuestionVariableType = string.Empty;
        private string Combo_Conditional_Item_3selectedQuestionVariableType = string.Empty;
        private string Combo_Conditional_Item_4selectedQuestionVariableType = string.Empty;
        private string Combo_Conditional_Item_5selectedQuestionVariableType = string.Empty;
        private List<String> Combo_Conditional_Item_1Choices = new List<string>();
        private List<String> Combo_Conditional_Item_2Choices = new List<string>();
        private List<String> Combo_Conditional_Item_3Choices = new List<string>();
        private List<String> Combo_Conditional_Item_4Choices = new List<string>();
        private List<String> Combo_Conditional_Item_5Choices = new List<string>();
        private QC4Common.Util.FormUtil formUtil = new QC4Common.Util.FormUtil();
        int CmbItm1 = 0;
        int CmbItm2 = 0;
        int CmbItm3 = 0;
        int CmbItm4 = 0;
        int CmbItm5 = 0;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public CiteriaControlpsm()
        {
            InitializeComponent();
            IsInitialLoad = true;
        }
        public void CriteriaControl_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        public void ReviseData_Checkbox_OnClick(bool isChecked)
        {
            try
            {
                if (isChecked == true)
                {
                    EnableCriteriaControl();
                }
                else
                {
                    DisableCriteriaControl();
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        #region Loading data to Comboboxes
        //read all variables from the VariableDictionary and set the lists to the CriteriaVariable Comboboxes
        public void LoadingData(Excel.Workbook workbook)
        {
            try
            {
                _workbook = workbook;
                PopulatedDictionary = Definiotion.VariableDictionary;
                String[] dictKeys = PopulatedDictionary.Keys.ToArray<String>();
                for (int i = 0; i < dictKeys.Count<String>(); i++)
                {
                    QuestionSettings qs = PopulatedDictionary[dictKeys[i]];
                    if (qs.AnswerType == QC4Common.Common.Constants.AnswerType.MA || qs.AnswerType == QC4Common.Common.Constants.AnswerType.SA)
                    {
                        dataFromSheet.Add(new DataExport() { QuestionVariable = qs.Variable, QuestionVariableType = qs.AnswerType + '/' + qs.CategoryCount, Question = formUtil.EscapeCRLF(qs.Question), QuestionIndex = i - 1, QuestionChoiceNo = qs.QuestionNumber, Choisces = qs.Choices });
                    }
                    else if (qs.AnswerType == QC4Common.Common.Constants.AnswerType.FA || qs.AnswerType == QC4Common.Common.Constants.AnswerType.N)
                    {
                        dataFromSheet.Add(new DataExport() { QuestionVariable = qs.Variable, QuestionVariableType = qs.AnswerType, Question = formUtil.EscapeCRLF(qs.Question), QuestionIndex = i - 1, QuestionChoiceNo = qs.QuestionNumber, Choisces = qs.Choices });
                    }
                    else
                        continue;
                }
                _qstnvariablDD1.Add(new DataExport() { QuestionVariable = "", QuestionVariableType = "", Question = "" });
                _qstnvariablDD2.Add(new DataExport() { QuestionVariable = "", QuestionVariableType = "", Question = "" });
                _qstnvariablDD3.Add(new DataExport() { QuestionVariable = "", QuestionVariableType = "", Question = "" });
                _qstnvariablDD4.Add(new DataExport() { QuestionVariable = "", QuestionVariableType = "", Question = "" });
                _qstnvariablDD5.Add(new DataExport() { QuestionVariable = "", QuestionVariableType = "", Question = "" });
                foreach (DataExport item in dataFromSheet)
                {
                    _qstnvariablDD1.Add(item);
                    _qstnvariablDD2.Add(item);
                    _qstnvariablDD3.Add(item);
                    _qstnvariablDD4.Add(item);
                    _qstnvariablDD5.Add(item);

                }
                Combo_Conditional_Item_1.DataContext = _qstnvariablDD1;
                Combo_Conditional_Item_2.DataContext = _qstnvariablDD2;
                Combo_Conditional_Item_3.DataContext = _qstnvariablDD3;
                Combo_Conditional_Item_4.DataContext = _qstnvariablDD4;
                Combo_Conditional_Item_5.DataContext = _qstnvariablDD5;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        // load Operator Comboboxes with values accordingly corresponding to the selected CriteriaVariable 
        private void LoadingOperatorsOnComboBox(String QuestionVariableType, System.Windows.Controls.ComboBox CBOperator, System.Windows.Controls.ComboBox Conditional)
        {
            try
            {
                string[] CBcvselectedQuestionVariableType = QuestionVariableType.Split(new Char[] { '/' });
                Combo_Conditional_Item_selectedQuestionVariableType = CBcvselectedQuestionVariableType[0].ToString();
                if (CBcvselectedQuestionVariableType[0] == Qc4Launcher.Util.Constants.AnswerType.SA || CBcvselectedQuestionVariableType[0] == Qc4Launcher.Util.Constants.AnswerType.N)
                {
                    CBOperator.Items.Clear();

                    CBOperator.Items.Add("=");
                    CBOperator.Items.Add("<>");
                    CBOperator.Items.Add("<");
                    CBOperator.Items.Add(">");
                    CBOperator.Items.Add("<=");
                    CBOperator.Items.Add(">=");
                }
                else if (CBcvselectedQuestionVariableType[0] == Qc4Launcher.Util.Constants.AnswerType.FA || CBcvselectedQuestionVariableType[0] == Qc4Launcher.Util.Constants.AnswerType.MA)
                {
                    CBOperator.Items.Clear();
                    CBOperator.Items.Add(LocalResource.GTT_FS_CB_ITEM_VALUE_EQUALS);
                    CBOperator.Items.Add(LocalResource.GTT_FS_CB_ITEM_VALUE_LESS_OR_GREATER);
                }
                CBOperator.IsEnabled = Conditional.IsEnabled;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }
        #endregion

        #region CriteriaControl enabling and disabling logic

        public void EnableCriteriaControl()
        {
            //----------------enabling column heading------------------
            lblCriteriaVariable.IsEnabled = true;
            lblOperator.IsEnabled = true;
            lblValue.IsEnabled = true;
            lblCriteriaVariable.Foreground = new SolidColorBrush(Colors.Black);
            lblOperator.Foreground = new SolidColorBrush(Colors.Black);
            lblValue.Foreground = new SolidColorBrush(Colors.Black);

            //-----------------enabling controls------------------------
            Combo_Conditional_Item_1.IsEnabled = true;
            if (Combo_Conditional_Item_1.Text != "")
                Combo_Conditional_Operator_1.IsEnabled = true;
            if (Combo_Conditional_Item_2.Text != "")
            {
                Combo_Conditional_Item_2.IsEnabled = true;
                Combo_Conditional_Operator_2.IsEnabled = true;
            }
            if (Combo_Conditional_Item_3.Text != "")
            {
                Combo_Conditional_Item_3.IsEnabled = true;
                Combo_Conditional_Operator_3.IsEnabled = true;
            }
            if (Combo_Conditional_Item_4.Text != "")
            {
                Combo_Conditional_Item_4.IsEnabled = true;
                Combo_Conditional_Operator_4.IsEnabled = true;
            }
            if (Combo_Conditional_Item_5.Text != "")
            {
                Combo_Conditional_Item_5.IsEnabled = true;
                Combo_Conditional_Operator_5.IsEnabled = true;
            }
            if (Combo_Conditional_Operator_1.SelectedIndex >= 0)
            {
                Combo_Conditional_Value_1.IsEnabled = true;
                BTnFilter1.IsEnabled = true;
                BTnFilter1.Foreground = new SolidColorBrush(Colors.Black);
            }
            if (Combo_Conditional_Operator_2.SelectedIndex >= 0)
            {
                Combo_Conditional_Value_2.IsEnabled = true;
                BTnFilter2.IsEnabled = true;
                BTnFilter2.Foreground = new SolidColorBrush(Colors.Black);
            }
            if (Combo_Conditional_Operator_3.SelectedIndex >= 0)
            {
                Combo_Conditional_Value_3.IsEnabled = true;
                BTnFilter3.IsEnabled = true;
                BTnFilter3.Foreground = new SolidColorBrush(Colors.Black);
            }
            if (Combo_Conditional_Operator_4.SelectedIndex >= 0)
            {
                Combo_Conditional_Value_4.IsEnabled = true;
                BTnFilter4.IsEnabled = true;
                BTnFilter4.Foreground = new SolidColorBrush(Colors.Black);
            }
            if (Combo_Conditional_Operator_5.SelectedIndex >= 0)
            {
                Combo_Conditional_Value_5.IsEnabled = true;
                BTnFilter5.IsEnabled = true;
                BTnFilter5.Foreground = new SolidColorBrush(Colors.Black);
            }

            EnableRadioButtons();

            //--------------------controls text colour change-----------------
            Combo_Conditional_Item_1.Foreground = new SolidColorBrush(Colors.Black);
            Combo_Conditional_Operator_1.Foreground = new SolidColorBrush(Colors.Black);
            Combo_Conditional_Value_1.Foreground = new SolidColorBrush(Colors.Black);

            Combo_Conditional_Item_2.Foreground = new SolidColorBrush(Colors.Black);
            Combo_Conditional_Operator_2.Foreground = new SolidColorBrush(Colors.Black);
            Combo_Conditional_Value_2.Foreground = new SolidColorBrush(Colors.Black);

            Combo_Conditional_Item_3.Foreground = new SolidColorBrush(Colors.Black);
            Combo_Conditional_Operator_3.Foreground = new SolidColorBrush(Colors.Black);
            Combo_Conditional_Value_3.Foreground = new SolidColorBrush(Colors.Black);

            Combo_Conditional_Item_4.Foreground = new SolidColorBrush(Colors.Black);
            Combo_Conditional_Operator_4.Foreground = new SolidColorBrush(Colors.Black);
            Combo_Conditional_Value_4.Foreground = new SolidColorBrush(Colors.Black);

            Combo_Conditional_Item_5.Foreground = new SolidColorBrush(Colors.Black);
            Combo_Conditional_Operator_5.Foreground = new SolidColorBrush(Colors.Black);
            Combo_Conditional_Value_5.Foreground = new SolidColorBrush(Colors.Black);
        }

        public void DisableCriteriaControl()
        {
            //----------Column Heading disabled ---------------------

            lblCriteriaVariable.IsEnabled = false;
            lblOperator.IsEnabled = false;
            lblValue.IsEnabled = false;
            lblCriteriaVariable.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
            lblOperator.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
            lblValue.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));

            //-----------------------Row wise Controls disabled--------------------------------
            Combo_Conditional_Item_1.IsEnabled = false;
            Combo_Conditional_Operator_1.IsEnabled = false;
            Combo_Conditional_Value_1.IsEnabled = false;
            BTnFilter1.IsEnabled = false;
            Option_Conditional_And_1.IsEnabled = false;
            Option_Conditional_Or_1.IsEnabled = false;
            Combo_Conditional_Item_1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
            Combo_Conditional_Operator_1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
            Combo_Conditional_Value_1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
            BTnFilter1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));

            Combo_Conditional_Item_2.IsEnabled = false;
            Combo_Conditional_Operator_2.IsEnabled = false;
            Combo_Conditional_Value_2.IsEnabled = false;
            BTnFilter2.IsEnabled = false;
            Option_Conditional_And_2.IsEnabled = false;
            Option_Conditional_Or_2.IsEnabled = false;
            Combo_Conditional_Item_2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
            Combo_Conditional_Operator_2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
            Combo_Conditional_Value_2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
            BTnFilter2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));

            Combo_Conditional_Item_3.IsEnabled = false;
            Combo_Conditional_Operator_3.IsEnabled = false;
            Combo_Conditional_Value_3.IsEnabled = false;
            BTnFilter3.IsEnabled = false;
            Option_Conditional_And_3.IsEnabled = false;
            Option_Conditional_Or_3.IsEnabled = false;
            Combo_Conditional_Item_3.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
            Combo_Conditional_Operator_3.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
            Combo_Conditional_Value_3.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
            BTnFilter3.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));

            Combo_Conditional_Item_4.IsEnabled = false;
            Combo_Conditional_Operator_4.IsEnabled = false;
            Combo_Conditional_Value_4.IsEnabled = false;
            BTnFilter4.IsEnabled = false;
            Option_Conditional_And_4.IsEnabled = false;
            Option_Conditional_Or_4.IsEnabled = false;
            Combo_Conditional_Item_4.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
            Combo_Conditional_Operator_4.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
            Combo_Conditional_Value_4.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
            BTnFilter4.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));


            Combo_Conditional_Item_5.IsEnabled = false;
            Combo_Conditional_Operator_5.IsEnabled = false;
            Combo_Conditional_Value_5.IsEnabled = false;
            BTnFilter5.IsEnabled = false;
            Combo_Conditional_Item_5.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
            Combo_Conditional_Operator_5.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
            Combo_Conditional_Value_5.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
            BTnFilter5.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));

            //------------------- radio text colour change----------------------------
            Option_Conditional_And_1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
            Option_Conditional_And_2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
            Option_Conditional_And_3.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
            Option_Conditional_And_4.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));

            Option_Conditional_Or_1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
            Option_Conditional_Or_2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
            Option_Conditional_Or_3.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
            Option_Conditional_Or_4.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
        }

        private void EnableRadioButtons()
        {
            //-------------------enable radio buttons, text colour change--------------------
            if (Combo_Conditional_Value_1.Text != "" && Combo_Conditional_Value_1.Text != null)
            {
                Option_Conditional_And_1.IsEnabled = true;
                Option_Conditional_Or_1.IsEnabled = true;
                Option_Conditional_And_1.Foreground = new SolidColorBrush(Colors.Black);
                Option_Conditional_Or_1.Foreground = new SolidColorBrush(Colors.Black);
            }
            if (Combo_Conditional_Value_2.Text != "" && Combo_Conditional_Value_2.Text != null)
            {
                Option_Conditional_And_2.IsEnabled = true;
                Option_Conditional_Or_2.IsEnabled = true;
                Option_Conditional_And_2.Foreground = new SolidColorBrush(Colors.Black);
                Option_Conditional_Or_2.Foreground = new SolidColorBrush(Colors.Black);
            }
            if (Combo_Conditional_Value_3.Text != "" && Combo_Conditional_Value_3.Text != null)
            {
                Option_Conditional_And_3.IsEnabled = true;
                Option_Conditional_Or_3.IsEnabled = true;
                Option_Conditional_And_3.Foreground = new SolidColorBrush(Colors.Black);
                Option_Conditional_Or_3.Foreground = new SolidColorBrush(Colors.Black);
            }
            if (Combo_Conditional_Value_4.Text != "" && Combo_Conditional_Value_4.Text != null)
            {
                Option_Conditional_And_4.IsEnabled = true;
                Option_Conditional_Or_4.IsEnabled = true;
                Option_Conditional_And_4.Foreground = new SolidColorBrush(Colors.Black);
                Option_Conditional_Or_4.Foreground = new SolidColorBrush(Colors.Black);
            }

        }
        #endregion

        #region CriteriaVariable comboboxes Event handlers
        int LastSelected = 0;

        private void KeyPress(object sender, KeyEventArgs e)
        {
            try
            {
                System.Windows.Controls.ComboBox sen = ((System.Windows.Controls.ComboBox)sender);
                if ((Key.Delete == e.Key || ((sen.SelectedIndex == -1 && sen.Text == "") || (sen.SelectedIndex == -1 && sen.Text != ""))) && (this.Combo_Conditional_Item_1.IsKeyboardFocusWithin || this.Combo_Conditional_Item_1.IsDropDownOpen) &&
                    (Option_Conditional_And_1.IsChecked == false && Option_Conditional_Or_1.IsChecked == false))
                {
                    LastSelected = 0;
                    this.Combo_Conditional_Item_1.SelectedIndex = 0;
                }
                else if ((Key.Delete == e.Key || ((sen.SelectedIndex == -1 && sen.Text == "") || (sen.SelectedIndex == -1 && sen.Text != ""))) && (this.Combo_Conditional_Item_2.IsKeyboardFocusWithin || this.Combo_Conditional_Item_2.IsDropDownOpen) &&
                    (Option_Conditional_And_2.IsChecked == false && Option_Conditional_Or_2.IsChecked == false))
                {
                    LastSelected = 0;
                    this.Combo_Conditional_Item_2.SelectedIndex = 0;
                }
                else if ((Key.Delete == e.Key || ((sen.SelectedIndex == -1 && sen.Text == "") || (sen.SelectedIndex == -1 && sen.Text != ""))) && (this.Combo_Conditional_Item_3.IsKeyboardFocusWithin || this.Combo_Conditional_Item_3.IsDropDownOpen) &&
                    (Option_Conditional_And_3.IsChecked == false && Option_Conditional_Or_3.IsChecked == false))
                {
                    LastSelected = 0;
                    this.Combo_Conditional_Item_3.SelectedIndex = 0;
                }
                else if ((Key.Delete == e.Key || ((sen.SelectedIndex == -1 && sen.Text == "") || (sen.SelectedIndex == -1 && sen.Text != ""))) && (this.Combo_Conditional_Item_4.IsKeyboardFocusWithin || this.Combo_Conditional_Item_4.IsDropDownOpen) &&
                    (Option_Conditional_And_4.IsChecked == false && Option_Conditional_Or_4.IsChecked == false))
                {
                    LastSelected = 0;
                    this.Combo_Conditional_Item_4.SelectedIndex = 0;
                }
                else if ((Key.Delete == e.Key || ((sen.SelectedIndex == -1 && sen.Text == "") || (sen.SelectedIndex == -1 && sen.Text != ""))) && (this.Combo_Conditional_Item_5.IsKeyboardFocusWithin || this.Combo_Conditional_Item_5.IsDropDownOpen))
                {
                    LastSelected = 0;
                    this.Combo_Conditional_Item_5.SelectedIndex = 0;
                }
                



                //System.Windows.Controls.ComboBox sen = ((System.Windows.Controls.ComboBox)sender);
                //if ((Key.Delete == e.Key || Key.Back == e.Key || (sen.SelectedIndex == -1 && sen.Text == "") || (sen.SelectedIndex == -1 && sen.Text != "")) && (this.Combo_Conditional_Item_1.IsKeyboardFocusWithin || this.Combo_Conditional_Item_1.IsDropDownOpen) &&
                //    (Option_Conditional_And_1.IsChecked == false && Option_Conditional_Or_1.IsChecked == false))
                //{
                //    LastSelected = 0;
                //    this.Combo_Conditional_Item_1.SelectedIndex = 0;
                //}
                //else if ((Key.Delete == e.Key || Key.Back == e.Key || (sen.SelectedIndex == -1 && sen.Text == "") || (sen.SelectedIndex == -1 && sen.Text != "")) && (this.Combo_Conditional_Item_2.IsKeyboardFocusWithin || this.Combo_Conditional_Item_2.IsDropDownOpen) &&
                //    (Option_Conditional_And_2.IsChecked == false && Option_Conditional_Or_2.IsChecked == false))
                //{
                //    LastSelected = 0;
                //    this.Combo_Conditional_Item_2.SelectedIndex = 0;
                //}
                //else if ((Key.Delete == e.Key || Key.Back == e.Key || (sen.SelectedIndex == -1 && sen.Text == "") || (sen.SelectedIndex == -1 && sen.Text != "")) && (this.Combo_Conditional_Item_3.IsKeyboardFocusWithin || this.Combo_Conditional_Item_3.IsDropDownOpen) &&
                //    (Option_Conditional_And_3.IsChecked == false && Option_Conditional_Or_3.IsChecked == false))
                //{
                //    LastSelected = 0;
                //    this.Combo_Conditional_Item_3.SelectedIndex = 0;
                //}
                //else if ((Key.Delete == e.Key || Key.Back == e.Key || (sen.SelectedIndex == -1 && sen.Text == "") || (sen.SelectedIndex == -1 && sen.Text != "")) && (this.Combo_Conditional_Item_4.IsKeyboardFocusWithin || this.Combo_Conditional_Item_4.IsDropDownOpen) &&
                //    (Option_Conditional_And_4.IsChecked == false && Option_Conditional_Or_4.IsChecked == false))
                //{
                //    LastSelected = 0;
                //    this.Combo_Conditional_Item_4.SelectedIndex = 0;
                //}
                //else if ((Key.Delete == e.Key || Key.Back == e.Key || (sen.SelectedIndex == -1 && sen.Text == "") || (sen.SelectedIndex == -1 && sen.Text != "")) && (this.Combo_Conditional_Item_5.IsKeyboardFocusWithin || this.Combo_Conditional_Item_5.IsDropDownOpen))
                //{
                //    LastSelected = 0;
                //    this.Combo_Conditional_Item_5.SelectedIndex = 0;
                //}
                //else if (Key.Delete == e.Key || Key.Back == e.Key || (sen.SelectedIndex == -1 && sen.Text == "") || (sen.SelectedIndex == -1 && sen.Text != ""))
                //{
                //    LastSelected = 0;
                //}
                //else if (sen.SelectedItem == null && (sen.IsKeyboardFocusWithin || sen.IsDropDownOpen))
                //{
                //    //if (sen.Name != "Combo_Classify_Item")
                //    sen.SelectedIndex = LastSelected;
                //}
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        System.Windows.Controls.ComboBox combo = null;
        string LastSelectedText = "";
        private void Combo_CriteriaVariable_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
               // combo = (ComboBox)sender;
                TextBox txt = null;
                if (e.OriginalSource is TextBox)
                    txt = (TextBox)e.OriginalSource;

                if (combo.SelectedIndex != 0)
                    LastSelected = combo.SelectedIndex;
                else if (combo.SelectedIndex == 0)
                {
                    LastSelectedText = combo.Text;
                }
                if (combo.SelectedIndex != 0)
                    LastSelected = combo.SelectedIndex;
                if (Key.Delete == e.Key && (this.Combo_Conditional_Item_1.IsKeyboardFocusWithin || this.Combo_Conditional_Item_1.IsDropDownOpen) &&
                    (Option_Conditional_And_1.IsChecked == false && Option_Conditional_Or_1.IsChecked == false))
                {
                    LastSelectedText = "";
                    LastSelected = -1;
                    e.Handled = false;
                }
                else if (Key.Delete == e.Key && (this.Combo_Conditional_Item_2.IsKeyboardFocusWithin || this.Combo_Conditional_Item_2.IsDropDownOpen) &&
                    (Option_Conditional_And_2.IsChecked == false && Option_Conditional_Or_2.IsChecked == false))
                {
                    LastSelectedText = "";
                    LastSelected = -1;
                    e.Handled = false;
                }
                else if (Key.Delete == e.Key && (this.Combo_Conditional_Item_3.IsKeyboardFocusWithin || this.Combo_Conditional_Item_3.IsDropDownOpen) &&
                    (Option_Conditional_And_3.IsChecked == false && Option_Conditional_Or_3.IsChecked == false))
                {
                    LastSelectedText = "";
                    LastSelected = -1;
                    e.Handled = false;
                }
                else if (Key.Delete == e.Key && (this.Combo_Conditional_Item_4.IsKeyboardFocusWithin || this.Combo_Conditional_Item_4.IsDropDownOpen) &&
                    (Option_Conditional_And_4.IsChecked == false && Option_Conditional_Or_4.IsChecked == false))
                {
                    LastSelectedText = "";
                    LastSelected = -1;
                    e.Handled = false;
                }
                else if (Key.Delete == e.Key && (this.Combo_Conditional_Item_5.IsKeyboardFocusWithin || this.Combo_Conditional_Item_5.IsDropDownOpen))
                {
                    LastSelectedText = "";
                    LastSelected = -1;
                    e.Handled = false;
                }
               
                else if (Key.Delete == e.Key)
                {
                    e.Handled = true;
                }
                else if ((txt != null && txt.SelectedText.Length == txt.Text.Length && Key.Back == e.Key) && (this.Combo_Conditional_Item_1.IsKeyboardFocusWithin || this.Combo_Conditional_Item_1.IsDropDownOpen) &&
                    (Option_Conditional_And_1.IsChecked == true || Option_Conditional_Or_1.IsChecked == true))
                {
                    e.Handled = true;
                }
                else if ((txt != null && txt.SelectedText.Length == txt.Text.Length && Key.Back == e.Key) && (this.Combo_Conditional_Item_2.IsKeyboardFocusWithin || this.Combo_Conditional_Item_2.IsDropDownOpen) &&
                    (Option_Conditional_And_2.IsChecked == true || Option_Conditional_Or_2.IsChecked == true))
                {
                    e.Handled = true;
                }
                else if ((txt != null && txt.SelectedText.Length == txt.Text.Length && Key.Back == e.Key) && (this.Combo_Conditional_Item_3.IsKeyboardFocusWithin || this.Combo_Conditional_Item_3.IsDropDownOpen) &&
                    (Option_Conditional_And_3.IsChecked == true || Option_Conditional_Or_3.IsChecked == true))
                {
                    e.Handled = true;
                }
                else if ((txt != null && txt.SelectedText.Length == txt.Text.Length && Key.Back == e.Key) && (this.Combo_Conditional_Item_4.IsKeyboardFocusWithin || this.Combo_Conditional_Item_4.IsDropDownOpen) &&
                    (Option_Conditional_And_4.IsChecked == true || Option_Conditional_Or_4.IsChecked == true))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }


        private void Combo_CriteriaVariable_GotFocus(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.ComboBox sen = ((System.Windows.Controls.ComboBox)sender);
            combo = sen;
        }

        private void Combo_CriteriaVariable_DropDownClosed(object sender, EventArgs e)
        {
            System.Windows.Controls.ComboBox sen = ((System.Windows.Controls.ComboBox)sender);
            if (sen.SelectedItem != null)
            {
                sen.Text = ((FilterSettingsClass.DataExport)(sen.SelectedItem)).QuestionVariable;
                //TextBox text = sen.Template.FindName("PART_EditableTextBox", sen) as TextBox;
                //if (null != text)
                //{
                //    text.SelectionStart = 0;
                //    text.SelectionLength = 0;
                //}
            }
            else
            {
                sen.Text = "";
            }
        }

        private void Combo_CriteriaVariable_Loaded(object sender, RoutedEventArgs e)
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
            //var textBox = (TextBox)sender;
            //if (null != textBox)
            //{
            //    textBox.SelectionStart = 0;
            //    textBox.SelectionLength = textBox.Text.Length;
            //}
        }

        bool FirstFocus = true;
        private void Combo_CriteriaVariable_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (!FirstFocus)
            {
                System.Windows.Controls.ComboBox sen = ((System.Windows.Controls.ComboBox)sender);
                sen.IsDropDownOpen = true;
            }
            else
                FirstFocus = false;
        }
        //ComboBox Custom Event Handler
        public class MyComboBoxSelectionChangedEventArgs : EventArgs
        {
            public object MyComboBoxItem { get; set; }
            public string sendr { get; set; }
            public int LastSelected { get; set; }
            public int SelectedIndex { get; set; }
            public string LastSelectedText { get; set; }
        }
        public delegate void MyComboBoxSelectionChangedEventHandler(object sender, MyComboBoxSelectionChangedEventArgs e);
        public event MyComboBoxSelectionChangedEventHandler MyComboBoxSelectionChanged;
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (e.AddedItems.Count > 0)
            {
                MyComboBoxSelectionChanged?.Invoke(this,
                    new MyComboBoxSelectionChangedEventArgs()
                    {
                        sendr = ((ComboBox)sender).Name,
                        MyComboBoxItem = e.AddedItems[0],
                        LastSelected = this.LastSelected,
                        SelectedIndex = ((ComboBox)sender).SelectedIndex,
                        LastSelectedText = this.LastSelectedText
                    });
            }
        }
        #endregion

        #region 'SelectionChanged' events of Criteria Variable Comboboxes

        private void Combo_Conditional_Item_1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int selectedindex = Combo_Conditional_Item_1.SelectedIndex;
                if ((selectedindex > 0 && selectedindex != LastSelected) || (selectedindex == 0 && _qstnvariablDD1[selectedindex].QuestionVariable != "" && LastSelectedText == ""))
                {
                    Combo_Conditional_Item_1selectedQuestionVariableType = _qstnvariablDD1[selectedindex].QuestionVariableType;
                    Combo_Conditional_Item_1Choices = _qstnvariablDD1[selectedindex].Choisces;
                    if (Combo_Conditional_Item_1selectedQuestionVariableType != null)
                    {
                        LoadingOperatorsOnComboBox(Combo_Conditional_Item_1selectedQuestionVariableType, Combo_Conditional_Operator_1, Combo_Conditional_Item_1);
                    }
                    if (!IsInitialLoad)
                    {
                        Combo_Conditional_Operator_1.IsEnabled = true;
                        Combo_Conditional_Operator_1.SelectedIndex = -1;
                        Combo_Conditional_Value_1.Text = "";
                        Combo_Conditional_Value_1.IsEnabled = false;
                        BTnFilter1.IsEnabled = false;

                        Option_Conditional_And_1.IsEnabled = false;
                        Option_Conditional_Or_1.IsEnabled = false;
                    }

                }
                else
                {
                    if (LastSelected == 0 && LastSelectedText == "")
                    {
                        Combo_Conditional_Operator_1.IsEnabled = false;
                        Combo_Conditional_Operator_1.SelectedIndex = -1;
                        Combo_Conditional_Value_1.Text = "";
                        Combo_Conditional_Value_1.IsEnabled = false;
                        BTnFilter1.IsEnabled = false;
                        Option_Conditional_And_1.IsChecked = false;
                        Option_Conditional_Or_1.IsChecked = false;
                        Option_Conditional_And_1.IsEnabled = false;
                        Option_Conditional_Or_1.IsEnabled = false;
                        Option_Conditional_And_1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                        Option_Conditional_Or_1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                    }
                }
                // if (IsInitialLoad && CmbItm1 > 0)
                //    _qstnvariablDD1.RemoveAt(0);
                Combo_Conditional_Item_1.Foreground = new SolidColorBrush(Colors.Black);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void Combo_Conditional_Item_2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int selectedindex = Combo_Conditional_Item_2.SelectedIndex;
                if (selectedindex > 0 && selectedindex != LastSelected || (selectedindex == 0 && !string.IsNullOrWhiteSpace(_qstnvariablDD2[selectedindex].QuestionVariableType)&& LastSelectedText == ""))
                {
                    Combo_Conditional_Item_2selectedQuestionVariableType = _qstnvariablDD2[selectedindex].QuestionVariableType;
                    Combo_Conditional_Item_2Choices = _qstnvariablDD2[selectedindex].Choisces;
                    if (Combo_Conditional_Item_2selectedQuestionVariableType != null)
                    {
                        LoadingOperatorsOnComboBox(Combo_Conditional_Item_2selectedQuestionVariableType, Combo_Conditional_Operator_2, Combo_Conditional_Item_2);
                    }

                    if (!IsInitialLoad)
                    {
                        Combo_Conditional_Operator_2.IsEnabled = true;
                        Combo_Conditional_Operator_2.SelectedIndex = -1;
                        Combo_Conditional_Value_2.Text = "";
                        Combo_Conditional_Value_2.IsEnabled = false;
                        BTnFilter2.IsEnabled = false;
                        Option_Conditional_And_2.IsEnabled = false;
                        Option_Conditional_Or_2.IsEnabled = false;
                    }
                }
                else
                {
                    if (LastSelected == 0 && LastSelectedText == "")
                    {
                        Option_Conditional_And_1.IsChecked = false;
                        Option_Conditional_Or_1.IsChecked = false;
                        Combo_Conditional_Item_2.IsEnabled = false;
                        Combo_Conditional_Operator_2.IsEnabled = false;
                        Combo_Conditional_Operator_2.SelectedIndex = -1;
                        Combo_Conditional_Value_2.Text = "";
                        Combo_Conditional_Value_2.IsEnabled = false;
                        BTnFilter2.IsEnabled = false;
                        Option_Conditional_And_2.IsEnabled = false;
                        Option_Conditional_Or_2.IsEnabled = false;
                        Option_Conditional_And_2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                        Option_Conditional_Or_2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                        if (!IsInitialLoad)
                            _qstnvariablDD1.Insert(0, DataExportObjectCreator());
                    }
                }
                if (IsInitialLoad && CmbItm2 > 0)
                    _qstnvariablDD1.RemoveAt(0);
                Combo_Conditional_Item_2.Foreground = new SolidColorBrush(Colors.Black);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void Combo_Conditional_Item_3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int selectedindex = Combo_Conditional_Item_3.SelectedIndex;
                if (selectedindex > 0 && selectedindex != LastSelected || (selectedindex == 0 && _qstnvariablDD3[selectedindex].QuestionVariableType != "" && LastSelectedText == ""))
                {
                    Combo_Conditional_Item_3selectedQuestionVariableType = _qstnvariablDD3[selectedindex].QuestionVariableType;
                    Combo_Conditional_Item_3Choices = _qstnvariablDD3[selectedindex].Choisces;
                    if (Combo_Conditional_Item_3selectedQuestionVariableType != null)
                    {
                        LoadingOperatorsOnComboBox(Combo_Conditional_Item_3selectedQuestionVariableType, Combo_Conditional_Operator_3, Combo_Conditional_Item_3);
                    }

                    if (!IsInitialLoad)
                    {
                        Combo_Conditional_Operator_3.IsEnabled = true;
                        Combo_Conditional_Operator_3.SelectedIndex = -1;
                        Combo_Conditional_Value_3.Text = "";
                        Combo_Conditional_Value_3.IsEnabled = false;
                        BTnFilter3.IsEnabled = false;
                        Option_Conditional_And_3.IsEnabled = false;
                        Option_Conditional_Or_3.IsEnabled = false;
                    }
                }
                else
                {
                    if (LastSelected == 0 && LastSelectedText == "")
                    {
                        Option_Conditional_And_2.IsChecked = false;
                        Option_Conditional_Or_2.IsChecked = false;
                        Combo_Conditional_Item_3.IsEnabled = false;
                        Combo_Conditional_Operator_3.IsEnabled = false;
                        Combo_Conditional_Operator_3.SelectedIndex = -1;
                        Combo_Conditional_Value_3.Text = "";
                        Combo_Conditional_Value_3.IsEnabled = false;
                        BTnFilter3.IsEnabled = false;
                        Option_Conditional_And_3.IsEnabled = false;
                        Option_Conditional_Or_3.IsEnabled = false;
                        Option_Conditional_And_3.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                        Option_Conditional_Or_3.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                        if (!IsInitialLoad)
                            _qstnvariablDD2.Insert(0, DataExportObjectCreator());
                    }
                }
                if (IsInitialLoad && CmbItm3 > 0)
                    _qstnvariablDD2.RemoveAt(0);
                Combo_Conditional_Item_3.Foreground = new SolidColorBrush(Colors.Black);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void Combo_Conditional_Item_4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int selectedindex = Combo_Conditional_Item_4.SelectedIndex;
                if (selectedindex > 0 && selectedindex != LastSelected || (selectedindex == 0 && _qstnvariablDD4[selectedindex].QuestionVariableType != "" && LastSelectedText == ""))
                {
                    Combo_Conditional_Item_4selectedQuestionVariableType = _qstnvariablDD4[selectedindex].QuestionVariableType;
                    Combo_Conditional_Item_4Choices = _qstnvariablDD4[selectedindex].Choisces;
                    if (Combo_Conditional_Item_4selectedQuestionVariableType != null)
                    {
                        LoadingOperatorsOnComboBox(Combo_Conditional_Item_4selectedQuestionVariableType, Combo_Conditional_Operator_4, Combo_Conditional_Item_4);
                    }

                    if (!IsInitialLoad)
                    {
                        Combo_Conditional_Operator_4.IsEnabled = true;
                        Combo_Conditional_Operator_4.SelectedIndex = -1;
                        Combo_Conditional_Value_4.Text = "";
                        Combo_Conditional_Value_4.IsEnabled = false;
                        BTnFilter4.IsEnabled = false;
                        Option_Conditional_And_4.IsEnabled = false;
                        Option_Conditional_Or_4.IsEnabled = false;
                    }
                }
                else
                {
                    if (LastSelected == 0 && LastSelectedText == "")
                    {
                        Option_Conditional_And_3.IsChecked = false;
                        Option_Conditional_Or_3.IsChecked = false;
                        Combo_Conditional_Item_4.IsEnabled = false;
                        Combo_Conditional_Operator_4.IsEnabled = false;
                        Combo_Conditional_Operator_4.SelectedIndex = -1;
                        Combo_Conditional_Value_4.Text = "";
                        Combo_Conditional_Value_4.IsEnabled = false;
                        BTnFilter4.IsEnabled = false;
                        Option_Conditional_And_4.IsEnabled = false;
                        Option_Conditional_Or_4.IsEnabled = false;
                        Option_Conditional_And_4.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                        Option_Conditional_Or_4.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                        if (!IsInitialLoad)
                            _qstnvariablDD3.Insert(0, DataExportObjectCreator());
                    }
                }
                if (IsInitialLoad && CmbItm4 > 0)
                    _qstnvariablDD3.RemoveAt(0);
                Combo_Conditional_Item_4.Foreground = new SolidColorBrush(Colors.Black);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void Combo_Conditional_Item_5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int selectedindex = Combo_Conditional_Item_5.SelectedIndex;
                if (selectedindex > 0 && selectedindex != LastSelected)
                {
                    Combo_Conditional_Item_5selectedQuestionVariableType = _qstnvariablDD5[selectedindex].QuestionVariableType;
                    Combo_Conditional_Item_5Choices = _qstnvariablDD5[selectedindex].Choisces;
                    if (Combo_Conditional_Item_5selectedQuestionVariableType != null)
                    {
                        LoadingOperatorsOnComboBox(Combo_Conditional_Item_5selectedQuestionVariableType, Combo_Conditional_Operator_5, Combo_Conditional_Item_5);
                    }

                    if (!IsInitialLoad)
                    {
                        Combo_Conditional_Operator_5.IsEnabled = true;
                        Combo_Conditional_Operator_5.SelectedIndex = -1;
                        Combo_Conditional_Value_5.Text = "";
                        Combo_Conditional_Value_5.IsEnabled = false;
                        BTnFilter5.IsEnabled = false;
                    }
                }
                else
                {
                    if (LastSelected == 0)
                    {
                        Option_Conditional_And_4.IsChecked = false;
                        Option_Conditional_Or_4.IsChecked = false;
                        Combo_Conditional_Item_5.IsEnabled = false;
                        Combo_Conditional_Operator_5.IsEnabled = false;
                        Combo_Conditional_Operator_5.SelectedIndex = -1;
                        Combo_Conditional_Value_5.Text = "";
                        Combo_Conditional_Value_5.IsEnabled = false;
                        BTnFilter5.IsEnabled = false;
                        if (!IsInitialLoad)
                            _qstnvariablDD4.Insert(0, DataExportObjectCreator());
                    }
                }
                if (IsInitialLoad)
                {
                    if (CmbItm5 > 0)
                        _qstnvariablDD4.RemoveAt(0);
                    IsInitialLoad = false;
                }

                Combo_Conditional_Item_5.Foreground = new SolidColorBrush(Colors.Black);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private DataExport DataExportObjectCreator()
        {
            DataExport dataExport_Obj = new DataExport();
            dataExport_Obj.Question = "";
            dataExport_Obj.QuestionChoiceNo = "";
            dataExport_Obj.QuestionIndex = 0;
            dataExport_Obj.QuestionVariable = "";
            dataExport_Obj.QuestionVariableType = "";
            return dataExport_Obj;
        }
        #endregion

        #region 'SelectionChanged' events of Operator Comboboxes
        private void Combo_Conditional_Operator_1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Combo_Conditional_Operator_1.SelectedIndex >= 0)
            {
                Combo_Conditional_Value_1.IsEnabled = true;
                BTnFilter1.IsEnabled = true;
            }
        }

        private void Combo_Conditional_Operator_2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Combo_Conditional_Operator_2.SelectedIndex >= 0)
            {
                Combo_Conditional_Value_2.IsEnabled = true;
                BTnFilter2.IsEnabled = true;
            }
        }

        private void Combo_Conditional_Operator_3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Combo_Conditional_Operator_3.SelectedIndex >= 0)
            {
                Combo_Conditional_Value_3.IsEnabled = true;
                BTnFilter3.IsEnabled = true;
            }
        }

        private void Combo_Conditional_Operator_4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Combo_Conditional_Operator_4.SelectedIndex >= 0)
            {
                Combo_Conditional_Value_4.IsEnabled = true;
                BTnFilter4.IsEnabled = true;
            }
        }

        private void Combo_Conditional_Operator_5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Combo_Conditional_Operator_5.SelectedIndex >= 0)
            {
                Combo_Conditional_Value_5.IsEnabled = true;
                BTnFilter5.IsEnabled = true;
            }
        }

        #endregion



        private static readonly string[] SuggestionValues = {
            "DK"
        };

        #region 'TextChanged' event handler for Value TextBox
        private void OnTextBoxChange(object sender, TextChangedEventArgs e)
        {

            string _currentInput = "";
            string _currentSuggestion = "";
            string _currentText = "";

            int _selectionStart;
            int _selectionLength;
            try
            {
                if (Keyboard.IsKeyUp(Key.Back))
                {
                    var sndr = (System.Windows.Controls.TextBox)sender;
                    var input = sndr.Text;
                    if (input.Length > _currentInput.Length && input != _currentSuggestion)
                    {
                        _currentSuggestion = SuggestionValues.FirstOrDefault(x => x.StartsWith(input, StringComparison.OrdinalIgnoreCase) != false);
                        if (_currentSuggestion != null)
                        {
                            _currentText = _currentSuggestion;
                            _selectionStart = input.Length;
                            _selectionLength = _currentSuggestion.Length - input.Length;

                            sndr.Text = _currentText;
                            sndr.Select(_selectionStart, _selectionLength);
                        }
                    }
                    _currentInput = input;
                }

                bool enable = false;
                if (((string)((System.Windows.Controls.TextBox)sender).Text).Length > 0)
                {
                    enable = true;
                }
                if (((System.Windows.Controls.TextBox)sender) == Combo_Conditional_Value_1)
                {
                    Option_Conditional_And_1.IsEnabled = enable;
                    Option_Conditional_Or_1.IsEnabled = enable;
                    Option_Conditional_And_1.Foreground = new SolidColorBrush(Colors.Black);
                    Option_Conditional_Or_1.Foreground = new SolidColorBrush(Colors.Black);
                }
                else if (((System.Windows.Controls.TextBox)sender) == Combo_Conditional_Value_2)
                {
                    Option_Conditional_And_2.IsEnabled = enable;
                    Option_Conditional_Or_2.IsEnabled = enable;
                    Option_Conditional_And_2.Foreground = new SolidColorBrush(Colors.Black);
                    Option_Conditional_Or_2.Foreground = new SolidColorBrush(Colors.Black);
                }
                else if (((System.Windows.Controls.TextBox)sender) == Combo_Conditional_Value_3)
                {
                    Option_Conditional_And_3.IsEnabled = enable;
                    Option_Conditional_Or_3.IsEnabled = enable;
                    Option_Conditional_And_3.Foreground = new SolidColorBrush(Colors.Black);
                    Option_Conditional_Or_3.Foreground = new SolidColorBrush(Colors.Black);
                }
                else if (((System.Windows.Controls.TextBox)sender) == Combo_Conditional_Value_4)
                {
                    Option_Conditional_And_4.IsEnabled = enable;
                    Option_Conditional_Or_4.IsEnabled = enable;
                    Option_Conditional_And_4.Foreground = new SolidColorBrush(Colors.Black);
                    Option_Conditional_Or_4.Foreground = new SolidColorBrush(Colors.Black);
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }
        #endregion

        #region ButtonClick event handlers for Value filter buttons
        private void OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                string buttonName = ((Button)sender).Name;
                if (buttonName == "BTnFilter1")
                {
                    string value = ChoiceSelection(Combo_Conditional_Item_1Choices);
                    if (value != null && value != "")
                    {
                        Combo_Conditional_Value_1.Text = value;
                        Option_Conditional_And_1.IsEnabled = true;
                        Option_Conditional_Or_1.IsEnabled = true;
                    }
                }
                else if (buttonName == "BTnFilter2")
                {
                    string value = ChoiceSelection(Combo_Conditional_Item_2Choices);
                    if (value != null && value != "")
                    {
                        Combo_Conditional_Value_2.Text = value;
                        Option_Conditional_And_2.IsEnabled = true;
                        Option_Conditional_Or_2.IsEnabled = true;
                    }
                }
                else if (buttonName == "BTnFilter3")
                {
                    string value = ChoiceSelection(Combo_Conditional_Item_3Choices);
                    if (value != null && value != "")
                    {
                        Combo_Conditional_Value_3.Text = value;
                        Option_Conditional_And_3.IsEnabled = true;
                        Option_Conditional_Or_3.IsEnabled = true;
                    }
                }
                else if (buttonName == "BTnFilter4")
                {
                    string value = ChoiceSelection(Combo_Conditional_Item_4Choices);
                    if (value != null && value != "")
                    {
                        Combo_Conditional_Value_4.Text = value;
                        Option_Conditional_And_4.IsEnabled = true;
                        Option_Conditional_Or_4.IsEnabled = true;
                    }
                }
                else if (buttonName == "BTnFilter5")
                {
                    string value = ChoiceSelection(Combo_Conditional_Item_5Choices);
                    if (value != null && value != "")
                        Combo_Conditional_Value_5.Text = value;
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private string ChoiceSelection(List<String> CBcvChoices)
        {
            try
            {
                String selectedChoice = "";
                if (Combo_Conditional_Item_1selectedQuestionVariableType != Qc4Launcher.Util.Constants.AnswerType.FA)
                {
                    FilterSettingValue popUp = new FilterSettingValue(CBcvChoices, LocalResource.TEXT_FILTER_VALUE_DK_NO_ANSWER, LocalResource.TEXT_FILTER_VALUE_STAR_EXCLUDED);
                    popUp.Owner = Window.GetWindow(this);
                    popUp.ShowDialog();
                    if (string.IsNullOrEmpty(popUp.CurrentValue) == false)
                    {
                        if (popUp.CurrentValue != LocalResource.TEXT_FILTER_VALUE_DK_NO_ANSWER || popUp.CurrentValue != LocalResource.TEXT_FILTER_VALUE_STAR_EXCLUDED)
                            selectedChoice = popUp.CurrentValue;
                        else
                            selectedChoice = popUp.CurrentValue;
                    }
                }
                else
                {
                    FilterSettingValue popUp = new FilterSettingValue(CBcvChoices, LocalResource.TEXT_FILTER_VALUE_DK_NO_ANSWER, "");
                    popUp.Owner = Window.GetWindow(this);
                    popUp.ShowDialog();
                    if (string.IsNullOrEmpty(popUp.CurrentValue) == false)
                    {
                        if (popUp.CurrentValue != LocalResource.TEXT_FILTER_VALUE_DK_NO_ANSWER)
                            selectedChoice = popUp.CurrentValue.Split(':')[0];
                        else
                            selectedChoice = popUp.CurrentValue;
                    }
                }
                return selectedChoice;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                return String.Empty;
            }
        }
        #endregion

        #region AND_OR RadioButton Checked event handlers
        private void OnRadioClick(object sender, RoutedEventArgs e)
        {
            try
            {
                string buttonName = ((RadioButton)sender).Name;
                var button = (RadioButton)sender;
                if ((buttonName == "Option_Conditional_And_1" || buttonName == "Option_Conditional_Or_1") && button.IsChecked == true)
                {
                    Combo_Conditional_Item_2.IsEnabled = true;
                     Combo_Conditional_Operator_2.IsEnabled = true;
                    if (Combo_Conditional_Item_2.SelectedIndex < 1)
                    { 
                        if (!IsInitialLoad)
                        {
                            if (string.IsNullOrWhiteSpace(_qstnvariablDD2[0].QuestionVariable))
                                Combo_Conditional_Item_2.SelectedIndex = 1;
                            else
                                Combo_Conditional_Item_2.SelectedIndex = 0;
                        }
                    }
                    if (!IsInitialLoad)
                    {
                        if (_qstnvariablDD1[0].QuestionVariable == "" || _qstnvariablDD1[0].QuestionVariable == null)
                            _qstnvariablDD1.RemoveAt(0);
                    }
                }
                else if ((buttonName == "Option_Conditional_And_2" || buttonName == "Option_Conditional_Or_2") && button.IsChecked == true)
                {
                    Combo_Conditional_Item_3.IsEnabled = true;
                    Combo_Conditional_Operator_3.IsEnabled = true;
                    if (Combo_Conditional_Item_3.SelectedIndex < 1)
                    {
                        if (!IsInitialLoad)
                        {
                            if (_qstnvariablDD3[0].QuestionVariable == "" || _qstnvariablDD3[0].QuestionVariable == null)
                                Combo_Conditional_Item_3.SelectedIndex = 1;
                            else
                                Combo_Conditional_Item_3.SelectedIndex = 0;
                        }
                    }
                    if (!IsInitialLoad)
                    {
                        if (_qstnvariablDD2[0].QuestionVariable == "" || _qstnvariablDD2[0].QuestionVariable == null)
                            _qstnvariablDD2.RemoveAt(0);
                    }
                }
                else if ((buttonName == "Option_Conditional_And_3" || buttonName == "Option_Conditional_Or_3") && button.IsChecked == true)
                {
                    Combo_Conditional_Item_4.IsEnabled = true;
                    Combo_Conditional_Operator_4.IsEnabled = true;
                    if (Combo_Conditional_Item_4.SelectedIndex < 1)
                    {
                        if (!IsInitialLoad)
                        {
                            if (_qstnvariablDD4[0].QuestionVariable == "" || _qstnvariablDD4[0].QuestionVariable == null)
                                Combo_Conditional_Item_4.SelectedIndex = 1;
                            else
                                Combo_Conditional_Item_4.SelectedIndex = 0;
                        }
                    }
                    if (!IsInitialLoad)
                    {
                        if (_qstnvariablDD3[0].QuestionVariable == "" || _qstnvariablDD3[0].QuestionVariable == null)
                            _qstnvariablDD3.RemoveAt(0);
                    }
                }
                else if ((buttonName == "Option_Conditional_And_4" || buttonName == "Option_Conditional_Or_4") && button.IsChecked == true)
                {
                    Combo_Conditional_Item_5.IsEnabled = true;
                    Combo_Conditional_Operator_5.IsEnabled = true;
                    if (Combo_Conditional_Item_5.SelectedIndex < 1)
                    {
                        if (!IsInitialLoad)
                        {
                            Combo_Conditional_Item_5.SelectedIndex = 1;
                        }
                    }
                    if (!IsInitialLoad)
                    {
                        if (_qstnvariablDD4[0].QuestionVariable == "" || _qstnvariablDD4[0].QuestionVariable == null)
                            _qstnvariablDD4.RemoveAt(0);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        #endregion

        #region Validation for Criteria section
        bool valueIsVariable = false;
        public bool ValidateCriteriaControls()
        {
            bool isValid = false;
            object selectedVar = null;
            string variable = string.Empty;
            string operatorr = string.Empty;
            string value = string.Empty;
            string variabletype = string.Empty;
            string wMsg = string.Empty;
            try
            {
               
                if (string.IsNullOrEmpty(Combo_Conditional_Item_1.Text))
                    return true;
                else if (!string.IsNullOrEmpty(Combo_Conditional_Item_1.Text))
                {
                    if (string.IsNullOrEmpty(Combo_Conditional_Operator_1.Text) || string.IsNullOrEmpty((Combo_Conditional_Value_1.Text)))
                    {
                        MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_FILTER_CRITERIA_VARIABLE_MISSING, 1));
                        isValid = false;
                    }
                    else
                    {
                        
                        selectedVar = Combo_Conditional_Item_1.SelectedItem;
                        variable = selectedVar == null || ((DataExport)selectedVar).QuestionVariable == "" ? "" : ((DataExport)selectedVar).QuestionVariable;
                        operatorr = Combo_Conditional_Operator_1.Text;
                        value = Combo_Conditional_Value_1.Text;
                        variabletype = ((DataExport)selectedVar).QuestionVariableType == null ? "" : ((DataExport)selectedVar).QuestionVariableType.Split('/')[0];
                         
                        isValid = true;
                        wMsg = GetWMsg(NumberCheck.CheckNotOperator(value, operatorr, variabletype));
                        if (wMsg != "")
                        {
                            MessageDialog.ErrorOk(string.Format(wMsg, 1));
                            return false;
                        }
                        wMsg = GetWMsg(NumberCheck.CheckValueAgainstOP(value, operatorr, variabletype));
                        if (wMsg != "")
                        {
                            MessageDialog.ErrorOk(string.Format(wMsg, 1));
                            return false;
                        }
                        isValid = CheckCriteriaControlValues1(selectedVar, variable, operatorr, value, variabletype, 1);
                    }
                    if (!isValid)
                        return isValid;
                }

                if (string.IsNullOrEmpty(Combo_Conditional_Item_2.Text))
                    isValid = true;
                else if (!string.IsNullOrEmpty(Combo_Conditional_Item_2.Text))
                {
                    if (string.IsNullOrEmpty(Combo_Conditional_Operator_2.Text) || string.IsNullOrEmpty(Combo_Conditional_Value_2.Text))
                    {
                        MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_FILTER_CRITERIA_VARIABLE_MISSING, 2));
                        isValid = false;
                    }
                    else
                    {
                       
                        selectedVar = Combo_Conditional_Item_2.SelectedItem;
                        variable = selectedVar == null || ((DataExport)selectedVar).QuestionVariable == "" ? "" : ((DataExport)selectedVar).QuestionVariable;
                        operatorr = Combo_Conditional_Operator_2.Text;
                        value = Combo_Conditional_Value_2.Text;
                        variabletype = ((DataExport)selectedVar).QuestionVariableType == null ? "" : ((DataExport)selectedVar).QuestionVariableType.Split('/')[0];
                        //  
                        isValid = true;
                        wMsg = GetWMsg(NumberCheck.CheckNotOperator(value, operatorr, variabletype));
                        if (wMsg != "")
                        {
                            MessageDialog.ErrorOk(string.Format(wMsg, 2));
                            return false;
                        }
                        wMsg = GetWMsg(NumberCheck.CheckValueAgainstOP(value, operatorr, variabletype));
                        if (wMsg != "")
                        {
                            MessageDialog.ErrorOk(string.Format(wMsg, 2));
                            return false;
                        }
                        isValid = CheckCriteriaControlValues1(selectedVar, variable, operatorr, value, variabletype, 2);
                    }

                    if (!isValid)
                        return isValid;
                }

                if (string.IsNullOrEmpty(Combo_Conditional_Item_3.Text))
                    isValid = true;
                else if (!string.IsNullOrEmpty(Combo_Conditional_Item_3.Text))
                {
                    if (string.IsNullOrEmpty(Combo_Conditional_Operator_3.Text) || string.IsNullOrEmpty(Combo_Conditional_Value_3.Text))
                    {
                        MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_FILTER_CRITERIA_VARIABLE_MISSING, 3));
                        isValid = false;
                    }
                    else
                    {
                        
                        selectedVar = Combo_Conditional_Item_3.SelectedItem;
                        variable = selectedVar == null || ((DataExport)selectedVar).QuestionVariable == "" ? "" : ((DataExport)selectedVar).QuestionVariable;
                        operatorr = Combo_Conditional_Operator_3.Text;
                        value = Combo_Conditional_Value_3.Text;
                        variabletype = ((DataExport)selectedVar).QuestionVariableType == null ? "" : ((DataExport)selectedVar).QuestionVariableType.Split('/')[0];
                        //   
                        isValid = true;
                        wMsg = GetWMsg(NumberCheck.CheckNotOperator(value, operatorr, variabletype));
                        if (wMsg != "")
                        {
                            MessageDialog.ErrorOk(string.Format(wMsg, 3));
                            return false;
                        }
                        wMsg = GetWMsg(NumberCheck.CheckValueAgainstOP(value, operatorr, variabletype));
                        if (wMsg != "")
                        {
                            MessageDialog.ErrorOk(string.Format(wMsg, 3));
                            return false;
                        }
                        isValid = CheckCriteriaControlValues1(selectedVar, variable, operatorr, value, variabletype, 3);
                    }

                    if (!isValid)
                        return isValid;
                }

                if (string.IsNullOrEmpty(Combo_Conditional_Item_4.Text))
                    isValid = true;

                else if (!string.IsNullOrEmpty(Combo_Conditional_Item_4.Text))
                {
                    if (string.IsNullOrEmpty(Combo_Conditional_Operator_4.Text) || string.IsNullOrEmpty(Combo_Conditional_Value_4.Text))
                    {
                        MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_FILTER_CRITERIA_VARIABLE_MISSING, 4));
                        isValid = false;
                    }
                    else
                    {
                       
                        selectedVar = Combo_Conditional_Item_4.SelectedItem;
                        variable = selectedVar == null || ((DataExport)selectedVar).QuestionVariable == "" ? "" : ((DataExport)selectedVar).QuestionVariable;
                        operatorr = Combo_Conditional_Operator_4.Text;
                        value = Combo_Conditional_Value_4.Text;
                        variabletype = ((DataExport)selectedVar).QuestionVariableType == null ? "" : ((DataExport)selectedVar).QuestionVariableType.Split('/')[0];
                        //  
                        isValid = true;
                        wMsg = GetWMsg(NumberCheck.CheckNotOperator(value, operatorr, variabletype));
                        if (wMsg != "")
                        {
                            MessageDialog.ErrorOk(string.Format(wMsg, 4));
                            return false;
                        }
                        wMsg = GetWMsg(NumberCheck.CheckValueAgainstOP(value, operatorr, variabletype));
                        if (wMsg != "")
                        {
                            MessageDialog.ErrorOk(string.Format(wMsg, 4));
                            return false;
                        }
                        isValid = CheckCriteriaControlValues1(selectedVar, variable, operatorr, value, variabletype, 4);
                    }

                    if (!isValid)
                        return isValid;
                }

                if (string.IsNullOrEmpty(Combo_Conditional_Item_5.Text))
                    isValid = true;

                else if (!string.IsNullOrEmpty(Combo_Conditional_Item_5.Text))
                {
                    if (string.IsNullOrEmpty(Combo_Conditional_Operator_5.Text) || string.IsNullOrEmpty(Combo_Conditional_Value_5.Text))
                    {
                        MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_FILTER_CRITERIA_VARIABLE_MISSING, 5));
                        isValid = false;
                    }
                    else
                    {
                      
                        selectedVar = Combo_Conditional_Item_5.SelectedItem;
                        variable = selectedVar == null || ((DataExport)selectedVar).QuestionVariable == "" ? "" : ((DataExport)selectedVar).QuestionVariable;
                        operatorr = Combo_Conditional_Operator_5.Text;
                        value = Combo_Conditional_Value_5.Text;
                        variabletype = ((DataExport)selectedVar).QuestionVariableType == null ? "" : ((DataExport)selectedVar).QuestionVariableType.Split('/')[0];
                        //    
                        isValid = true;
                        wMsg = GetWMsg(NumberCheck.CheckNotOperator(value, operatorr, variabletype));
                        if (wMsg != "")
                        {
                            MessageDialog.ErrorOk(string.Format(wMsg, 5));
                            return false;
                        }
                        wMsg = GetWMsg(NumberCheck.CheckValueAgainstOP(value, operatorr, variabletype));
                        if (wMsg != "")
                        {
                            MessageDialog.ErrorOk(string.Format(wMsg, 5));
                            return false;
                        }
                        isValid = CheckCriteriaControlValues1(selectedVar, variable, operatorr, value, variabletype, 5);
                    }

                    if (!isValid)
                        return isValid;
                }
                else
                    isValid = true;

                return isValid;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
            return isValid;
        }


        public bool CheckCriteriaControlValues(object selectedVar, string variable, string operatorr, string value, string variabletype, int row)
        {
            int catcount = 0;
            string wMsg = "";
            bool valid = true;
            try
            {
                if (!(value.Equals("DK") || value.Equals("*")))
                {
                    value = value.TrimStart().TrimEnd();
                    if (string.IsNullOrEmpty(value))
                    {
                        MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_DATAEXPORT_SET_NUMERIC, row));
                        return false;
                    }
                    else
                    {
                        wMsg = CheckIfVariable(value, variabletype, operatorr);
                        if (wMsg != "")
                        {
                            MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_FILTER_CRITERIA_INVALID_VALUE, row) + "\n" + wMsg);
                            valid = false;
                        }
                        else
                            valid = true;
                    }

                    if (variable != "" && variable != null)
                    {
                        catcount = ((DataExport)selectedVar).Choisces.Count == 0 ? 0 : Convert.ToInt32(((DataExport)selectedVar).Choisces.Count);
                        if (!CheckRange(value, catcount, variabletype, operatorr) && !(value.Equals("DK") || value.Equals("*")) && !(variabletype == QC4Common.Common.Constants.AnswerType.FA))
                        {
                            if (NumberCheck.Error_Mesage != "")
                            {
                                if (NumberCheck.Error_Mesage == "ERR_MSG_DATAEXPORT_SET_NUMERIC")
                                    MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_DATAEXPORT_SET_NUMERIC, 1));
                            }
                            else
                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_FILTER_CRITERIA_NOT_OP, 1));
                            return false;
                        }
                    }
                    //if (variable != "" && variable != null && !valueIsVariable)
                    //{
                    //    catcount = ((DataExport)selectedVar).Choisces.Count == 0 ? 0 : Convert.ToInt32(((DataExport)selectedVar).Choisces.Count);
                    //    if (!CheckRange(value, catcount, variabletype, operatorr) && !(variabletype == QC4Common.Common.Constants.AnswerType.FA || variabletype == QC4Common.Common.Constants.AnswerType.N))
                    //    {
                    //        MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_FILTER_CRITERIA_INVALID_VALUE, row) + "\n" + string.Format(LocalResource.ERR_MSG_INTEGRATE_SET_INTEGER_RANGE_OF, "1", catcount.ToString()));
                    //        return false;
                    //    }
                    //    if (!(variabletype == QC4Common.Common.Constants.AnswerType.FA))
                    //    {
                    //        wMsg = CheckValue(value, catcount, variabletype, operatorr, row);
                    //        if (wMsg != "")
                    //        {
                    //            MessageDialog.ErrorOk(wMsg);
                    //            return false;
                    //        }
                    //    }
                    //}
                }
                else
                {
                    if (operatorr != "=" && operatorr != "<>")
                    {
                        MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_FILTER_CRITERIA_NOT_OP, row) + "\n" + wMsg);
                        return false;
                    }
                }

                return valid;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
            return valid;
        }
        public bool CheckCriteriaControlValues1(object selectedVar, string variable, string operatorr, string value, string variabletype, int row)
        {
            int catcount = 0;
            string wMsg = "";
            bool valid = true;
            try
            {
                if (!(value.Equals("DK") || value.Equals("*")))
                {
                    value = value.TrimStart().TrimEnd();
                    if (string.IsNullOrEmpty(value))
                    {
                        MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_DATAEXPORT_SET_NUMERIC, row));
                        return false;
                    }
                    else
                    if (variable != "" && variable != null)
                    {
                        catcount = ((DataExport)selectedVar).Choisces.Count == 0 ? 0 : Convert.ToInt32(((DataExport)selectedVar).Choisces.Count);
                        if (!CheckValue(value, catcount, variabletype, operatorr) && !(value.Equals("DK") || value.Equals("*")) && !(variabletype == QC4Common.Common.Constants.AnswerType.FA))
                        {
                            if (NumberCheck.Error_Mesage != "")
                            {
                                if (NumberCheck.Error_Mesage == "ERR_MSG_DATAEXPORT_SET_NUMERIC")
                                    MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_DATAEXPORT_SET_NUMERIC, row));
                            }
                            else
                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_FILTER_CRITERIA_NOT_OP, row));
                            return false;
                        }
                    }
                   
                }
                else
                {
                    if (operatorr != "=" && operatorr != "<>")
                    {
                        MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_FILTER_CRITERIA_NOT_OP, row) + "\n" + wMsg);
                        return false;
                    }
                }

                return valid;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
            return valid;
        }

        private string GetWMsg(string v)
        {
            switch (v)
            {
                case "1":
                    return LocalResource.ERR_MSG_FILTER_CRITERIA_INCORRECT_VALUE;
                case "2":
                    return LocalResource.ERR_MSG_FILTER_CRITERIA_NOT_OPERATOR_DUPLICATED;
                case "<>":
                    return LocalResource.ERR_MSG_FILTER_CRITERIA_INCORRECT_VALUE_AGAINST_OP;
                case "!":
                    return LocalResource.ERR_MSG_FILTER_CRITERIA_INCORRECT_VALUE_AGAINST_OP2;
                case ",":
                    return LocalResource.ERR_MSG_DATAEXPORT_SET_NUMERIC;
                case "!1":
                    return LocalResource.ERR_MSG_INTEGRATE_CANNOT_SET_ALONE;
                default:
                    return "";
            }
        }

        public string CheckValue(string textvalue, int categorycount, string type, string operatr, int row)
        {
            //return NumberCheck.CheckFromOption(textvalue, categorycount, type, operatr);
            bool showError = false;
            string errMsg = string.Empty;
            if (operatr != "=" && operatr != "<>")
            {
                if (type == Qc4Launcher.Util.Constants.AnswerType.SA)
                {
                    Regex regex = new Regex(@"(^\d*$)");
                    if (!regex.Match(textvalue).Success)
                    {
                        // MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_DATAEXPORT_SET_NUMERIC, row));
                        //return false;
                        errMsg = string.Format(LocalResource.ERR_MSG_DATAEXPORT_SET_NUMERIC, row);
                    }
                    if (errMsg != "") return errMsg;
                }
                else if (type == Qc4Launcher.Util.Constants.AnswerType.N)
                {
                    Regex regex2 = new Regex(@"(^([(][-])\d*\.?\d*[)]$)|(^\d*\.?\d*$)");
                    if (!regex2.Match(textvalue).Success)
                    {
                        //MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_DATAEXPORT_SET_NUMERIC, row));
                        errMsg = string.Format(LocalResource.ERR_MSG_DATAEXPORT_SET_NUMERIC, row);
                        //return false;
                    }
                    if (errMsg != "") return errMsg;
                }
            }
            else
            {
                if (type == Qc4Launcher.Util.Constants.AnswerType.N)
                {
                    if (!ValidateNtypeCriteria(textvalue, categorycount, true))
                    {
                        //MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_DATAEXPORT_SET_NUMERIC, row));
                        errMsg = string.Format(LocalResource.ERR_MSG_DATAEXPORT_SET_NUMERIC, row);
                        //return false;
                    }
                    if (errMsg != "") return errMsg;
                }
                else if (type == Qc4Launcher.Util.Constants.AnswerType.SA || type == Qc4Launcher.Util.Constants.AnswerType.MA)
                {
                    if (!QC4Common.Validation.NumberCheck.Check(textvalue, 0))
                    {
                        //MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_DATAEXPORT_SET_NUMERIC, row));
                        errMsg = string.Format(LocalResource.ERR_MSG_DATAEXPORT_SET_NUMERIC, row);
                        //return false;
                    }
                    if (errMsg != "") return errMsg;

                    errMsg = ValidateNumericCell(textvalue, 1, categorycount, row, true);
                    if (errMsg != string.Empty)
                        showError = true;
                }
            }

            if (!showError && type != Qc4Launcher.Util.Constants.AnswerType.FA && operatr == "<>" && (textvalue.StartsWith("!") || textvalue.StartsWith("<>")))
            {
                //MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_DATAEXPORT_SET_NUMERIC, row));
                //return false;
                errMsg = string.Format(LocalResource.ERR_MSG_DATAEXPORT_SET_NUMERIC, row);
            }
            if (errMsg != "") return errMsg;
            if (!showError && (type != Qc4Launcher.Util.Constants.AnswerType.FA))
            {
                Regex regexnegativevalues = new Regex(@"^([!]?[(]{1}[-]{1}\d*\.?\d+[)]{1}[-]{1}[(]{1}[-]{1}\d*\.?\d+[)]{1}$)");
                //^(\d+*\.?\d+[-]{1}\d+*\.?\d+$)
                Regex regex = new Regex(@"^([!]?[(]?[-]?\d*\.?\d+[)]?[-]{1}[(]?[-]?\d*\.?\d+[)]?$)");//^([!]?\d*\.?\d+[-]{1}\d*\.?\d+$)
                if (regex.Match(textvalue).Success)//if success range then check reverse order
                {
                    string value = textvalue;
                    value = value.Replace("!", string.Empty);

                    string Div_Char = "@";
                    value = value.Replace("(-", Div_Char);
                    value = value.Replace("-", ",");

                    string[] tempval = value.Split(',');
                    foreach (string str in tempval)
                    {
                        Regex tempregex = new Regex(@"^[(]\d*.?\d+[)]$");//^([!]?\d*\.?\d+[-]{1}\d*\.?\d+$)
                        if (tempregex.Match(str).Success)//if success range then check reverse order
                        {
                            //MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_DATAEXPORT_SET_NUMERIC, row));
                            errMsg = string.Format(LocalResource.ERR_MSG_DATAEXPORT_SET_NUMERIC, row);
                            //return false;
                        }
                        if (errMsg != "") return errMsg;
                    }

                    value = value.Replace("(", "");
                    value = value.Replace(")", "");

                    value = value.Replace(Div_Char, "-");
                    string[] splitval = value.Split(',');
                    bool falsevalue = false;

                    float value1 = 0, value2 = 0;
                    if (!float.TryParse(splitval[0], out value1))
                    {
                        falsevalue = true;
                    }
                    if (!float.TryParse(splitval[1], out value2))
                    {
                        falsevalue = true;
                    }

                    if (falsevalue)
                    {
                        // MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_DATAEXPORT_SET_NUMERIC, row));
                        errMsg = string.Format(LocalResource.ERR_MSG_DATAEXPORT_SET_NUMERIC, row);
                        //return false;
                    }
                    if (errMsg != "") return errMsg;
                    if (value1 > value2)
                    {
                        //MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_DATAEXPORT_SET_NUMERIC, row));
                        errMsg = string.Format(LocalResource.ERR_MSG_DATAEXPORT_SET_NUMERIC, row);
                        //return false;
                    }
                    if (errMsg != "") return errMsg;
                }
                else if (!showError && regexnegativevalues.Match(textvalue).Success)
                {
                    string value = textvalue;
                    string Div_Char = "@";
                    value = value.Replace("(-", Div_Char);
                    value = value.Replace("(", "");
                    value = value.Replace(")", "");
                    value = value.Replace("-", ",");
                    value = value.Replace(Div_Char, "-");
                    string[] splitval = value.Split(',');
                    float value1 = 0, value2 = 0;
                    bool falsevalue = false;
                    if (!float.TryParse(splitval[0], out value1))
                    {
                        falsevalue = true;
                    }
                    if (!float.TryParse(splitval[1], out value2))
                    {
                        falsevalue = true;
                    }
                    if (falsevalue)
                    {
                        //MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_DATAEXPORT_SET_NUMERIC, row));
                        errMsg = string.Format(LocalResource.ERR_MSG_DATAEXPORT_SET_NUMERIC, row);
                        //return false;
                    }
                    if (errMsg != "") return errMsg;
                    if (value1 > value2)
                    {
                        //MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_DATAEXPORT_SET_NUMERIC, row));
                        errMsg = string.Format(LocalResource.ERR_MSG_DATAEXPORT_SET_NUMERIC, row);
                        //return false;
                    }
                    if (errMsg != "") return errMsg;
                }
            }

            //if (showError)
            //{
            //    //MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_DATAEXPORT_SET_NUMERIC, row));
            //    errMsg = string.Format(LocalResource.ERR_MSG_DATAEXPORT_SET_NUMERIC, row);
            //    return errMsg;
            //}

            return errMsg;
        }
        private bool CheckValue(string textvalue, int categorycount, string type, string operatr)
        {
            return NumberCheck.CheckFromOption(textvalue, categorycount, type, operatr);
        }
        public bool CheckRange(string textvalue, int categorycount, string type, string operatr)
        {
            int minVal = 1;
            string val = Regex.Replace(textvalue, @"\s+", "").Replace('/', ',');
            try
            {
                if (operatr != "=" && operatr != "<>")
                {
                    if (VB.Information.IsNumeric(val))
                    {
                        decimal valu = Convert.ToDecimal(val);
                        if (0 < categorycount)
                        {
                            if (valu < minVal)
                            {
                                return false;
                            }

                            if (categorycount < valu)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
            return true;
        }
        QC4Common.Util.FormUtil frmutil = new QC4Common.Util.FormUtil();
        public string CheckIfVariable(string textvalue, string type, string op)
        {
            string errMessage = string.Empty;
            try
            {
                if (!VB.Information.IsNumeric(textvalue))
                {
                    PopulatedDictionary = Definiotion.VariableDictionary;
                    List<QuestionSettings> questionSettings = new List<QuestionSettings>();
                    questionSettings = PopulatedDictionary.Values.ToList();
                    if (questionSettings.Any(q => q.Variable.Equals(textvalue, StringComparison.OrdinalIgnoreCase)))
                    {
                        valueIsVariable = true;
                        QuestionSettings qs = new QuestionSettings();
                        qs = questionSettings.FirstOrDefault(s => String.Equals(s.Variable, textvalue, StringComparison.CurrentCultureIgnoreCase));
                        if (qs != null)
                        {
                            if (type == QC4Common.Common.Constants.AnswerType.MA)
                            {
                                if (type != qs.AnswerType)
                                    errMessage = LocalResource.ERR_MSG_FILTER_CRITERIA_INVALID_VARIABLE_VALUE2;
                            }
                            else if (type == QC4Common.Common.Constants.AnswerType.FA)
                            {
                                if (qs.AnswerType == QC4Common.Common.Constants.AnswerType.MA || qs.AnswerType == QC4Common.Common.Constants.AnswerType.D)
                                    errMessage = LocalResource.ERR_MSG_FILTER_CRITERIA_INVALID_VARIABLE_VALUE3;
                            }
                            else if (type == QC4Common.Common.Constants.AnswerType.SA || type == QC4Common.Common.Constants.AnswerType.N)
                            {
                                if ((op == "=" || op == "<>") && (qs.AnswerType == QC4Common.Common.Constants.AnswerType.MA || qs.AnswerType == QC4Common.Common.Constants.AnswerType.D))
                                    errMessage = LocalResource.ERR_MSG_FILTER_CRITERIA_INVALID_VARIABLE_VALUE;
                                else if (op != "=" && op != "<>" && qs.AnswerType != QC4Common.Common.Constants.AnswerType.SA && qs.AnswerType != QC4Common.Common.Constants.AnswerType.N)
                                    errMessage = LocalResource.ERR_MSG_FILTER_CRITERIA_INVALID_VARIABLE_VALUE1;
                            }
                            else
                                errMessage = "";
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
            return errMessage;
        }

        private bool ValidateNtypeCriteria(string criValue, int catCount, bool ignoreNotEqual = false)
        {
            //ValidateNtypeCondition
            string[] SplitContent;
            string Contents = criValue;
            int val = 0;
            if (Contents.Length == 1 && !Contents.IsIntegerExpression(out val))
            {
                return false;
            }

            if (Contents.Length > 1 && (Contents.StartsWith("-") || Contents.EndsWith("-")))//191 added for criteria start with or end with "-"
                Contents = MinMaxAppendWithMinus(Contents, catCount);

            if (ignoreNotEqual && (Contents.StartsWith("<>") || Contents.StartsWith("!")))
            {
                Contents = Contents.Replace("<>", "");
                Contents = Contents.Replace("!", "");
            }

            if (Contents.Contains("/") || Contents.Contains(","))
            {
                char[] splitchar = { '/', ',' };
                SplitContent = Contents.Split(splitchar);
            }
            else
            {
                SplitContent = new string[] { Contents };
            }
            foreach (string item in SplitContent)
            {
                string value = item;
                string Div_Char = "@";
                string minval = "minVal";

                value = value.Replace("(-", Div_Char);
                value = value.Replace(int.MinValue.ToString(), minval);
                value = value.Replace("(", "");
                value = value.Replace(")", "");
                value = value.Replace("-", ",");
                value = value.Replace(Div_Char, "-");
                value = value.Replace(minval, int.MinValue.ToString());
                string[] split2 = value.Split(',');
                foreach (string s2 in split2)
                {
                    if (string.IsNullOrEmpty(s2))
                        continue;
                    double output = 0;
                    bool err = false;
                    if (!s2.IsDoubleExpression(out output, false, true, true, false))
                    {
                        err = true;
                    }
                    else if (output > int.MaxValue || output < int.MinValue)
                    {
                        err = true;
                    }
                    if (err)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static string MinMaxAppendWithMinus(string value, int catcount)//191 added for criteria start with or end with "-"
        {
            bool isnot = false;
            if (value.StartsWith("!"))//170984 fix for class
            {
                isnot = true;
                value = value.TrimStart('!');
            }
            int categoryCount = catcount == 0 ? int.MaxValue : catcount;
            if (value.StartsWith("-"))
            {
                value = (catcount == 0 ? (int.MinValue).ToString() : "1") + value;
            }//Redmine id:177538 //Redmine id: 176455
            if (value.EndsWith("-"))
            {
                value = value + categoryCount.ToString();
            }
            if (isnot) value = "!" + value;

            return value;
        }

        private string ValidateNumericCell(string criValue, int minvalue, int maxvalue, int criteriarow, bool ignoreNotEqual = false)
        {
            string Contents = criValue;
            if (Contents.Length > 1 && (Contents.StartsWith("-") || Contents.EndsWith("-")))//191 added for criteria start with or end with "-"
                Contents = MinMaxAppendWithMinus(Contents, maxvalue);

            if (ignoreNotEqual && (Contents.StartsWith("<>") || Contents.StartsWith("!")))
            {
                Contents = Contents.Replace("<>", "");
                Contents = Contents.Replace("!", "");
            }
            string message = ValidateNumericCellConent(Contents, minvalue, maxvalue, criteriarow);
            if (!string.IsNullOrEmpty(message))
            {
                //MessageDialog.ErrorOk(message);
                //return false;
                return message;
            }
            return message;
        }

        private string ValidateNumericCellConent(string cellcontents, int minvalue, int maxvalue, int criteriarow)
        {
            string[] SplitContent;
            if (cellcontents.Contains("/") || cellcontents.Contains(","))
            {
                char[] splitchar = { '/', ',' };
                SplitContent = cellcontents.Split(splitchar);
            }
            else
            {
                SplitContent = new string[] { cellcontents };
            }

            foreach (string item in SplitContent)
            {
                try
                {
                    string message = ValidateRange(item, minvalue, maxvalue, criteriarow);
                    if (!string.IsNullOrEmpty(message))
                        return message;

                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("StackTrace:{0}", ex.StackTrace);
                    return string.Format(LocalResource.ERR_MSG_DATAEXPORT_SET_NUMERIC, criteriarow);// "Set a numeric value";
                }
            }
            return string.Empty;

        }
        private string ValidateRange(string Contents, int minvalue, int maxvalue, int criteriarow)
        {
            string[] SplitContent;
            if (Contents.StartsWith("-"))
            {
                Contents = minvalue + Contents;
            }
            if (Contents.EndsWith("-"))
            {
                Contents = Contents + maxvalue;
            }
            SplitContent = Contents.Split('-');
            string[] splitExclusion = SplitContent[0].Split('!');
            try
            {
                if (splitExclusion[1] != "")
                {
                    SplitContent[0] = splitExclusion[1];
                }
            }
            catch { }

            foreach (string item in SplitContent)
            {
                try
                {
                    float value = 0;
                    if (float.TryParse(item, out value))
                    {
                        if (value < minvalue || value > maxvalue)
                        {
                            return string.Format(LocalResource.ERR_MSG_FILTER_CRITERIA_INVALID_VALUE, criteriarow) + "\n" + string.Format(LocalResource.ERR_MSG_INTEGRATE_SET_INTEGER_RANGE_OF, minvalue.ToString(), maxvalue.ToString());
                        }
                    }
                    else
                    {
                        return string.Format(LocalResource.ERR_MSG_DATAEXPORT_SET_NUMERIC, criteriarow);
                    }

                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("StackTrace:{0}", ex.StackTrace);
                    return string.Format(LocalResource.ERR_MSG_DATAEXPORT_SET_NUMERIC, criteriarow);// "Set a numeric value";
                }
            }
            return string.Empty;
        }

        #endregion

        #region Set Criteria Controls while edit and copy process
        DataExport target = null;
        int index = 0;

        public void SetSelectedCriteriaVariable(string variable, int row)
        {
            try
            {
                if (row == 1)
                {
                    target = _qstnvariablDD1.Where(z => z.QuestionVariable == variable).FirstOrDefault();
                    index = target == null ? 0 : _qstnvariablDD1.IndexOf(target);
                    CmbItm1 = index;
                    if (index == 0)
                    {
                        Combo_Conditional_Item_1.IsEnabled = false;
                        Combo_Conditional_Item_1.SelectedValue = 0;
                    }
                    if (index > 0)
                    {
                        Combo_Conditional_Item_1.IsEnabled = true;
                        Combo_Conditional_Item_1.SelectedValue = _qstnvariablDD1[index];
                        Combo_Conditional_Item_1.Foreground = new SolidColorBrush(Colors.Black);
                    }
                }
                else if (row == 2)
                {
                    target = _qstnvariablDD2.Where(z => z.QuestionVariable == variable).FirstOrDefault();
                    index = target == null ? 0 : _qstnvariablDD2.IndexOf(target);
                    CmbItm2 = index;
                    if (index == 0)
                    {
                        Combo_Conditional_Item_2.IsEnabled = false;
                        Combo_Conditional_Item_2.SelectedValue = 0;
                    }
                    if (index > 0)
                    {
                        Combo_Conditional_Item_2.IsEnabled = true;
                        Combo_Conditional_Item_2.SelectedValue = _qstnvariablDD2[index];
                        Combo_Conditional_Item_2.Foreground = new SolidColorBrush(Colors.Black);
                    }
                }
                else if (row == 3)
                {
                    target = _qstnvariablDD3.Where(z => z.QuestionVariable == variable).FirstOrDefault();
                    index = target == null ? 0 : _qstnvariablDD3.IndexOf(target);
                    CmbItm3 = index;
                    if (index == 0)
                    {
                        Combo_Conditional_Item_3.IsEnabled = false;
                        Combo_Conditional_Item_3.SelectedValue = 0;
                    }
                    if (index > 0)
                    {
                        Combo_Conditional_Item_3.IsEnabled = true;
                        Combo_Conditional_Item_3.SelectedValue = _qstnvariablDD3[index];
                        Combo_Conditional_Item_3.Foreground = new SolidColorBrush(Colors.Black);
                    }
                }
                else if (row == 4)
                {
                    target = _qstnvariablDD4.Where(z => z.QuestionVariable == variable).FirstOrDefault();
                    index = target == null ? 0 : _qstnvariablDD4.IndexOf(target);
                    CmbItm4 = index;
                    if (index == 0)
                    {
                        Combo_Conditional_Item_4.IsEnabled = false;
                        Combo_Conditional_Item_4.SelectedValue = 0;
                    }
                    if (index > 0)
                    {
                        Combo_Conditional_Item_4.IsEnabled = true;
                        Combo_Conditional_Item_4.SelectedValue = _qstnvariablDD4[index];
                        Combo_Conditional_Item_4.Foreground = new SolidColorBrush(Colors.Black);
                    }
                }
                else if (row == 5)
                {
                    target = _qstnvariablDD5.Where(z => z.QuestionVariable == variable).FirstOrDefault();
                    index = target == null ? 0 : _qstnvariablDD5.IndexOf(target);
                    CmbItm5 = index;
                    if (index == 0)
                    {
                        Combo_Conditional_Item_5.IsEnabled = false;
                        Combo_Conditional_Item_5.SelectedValue = 0;
                    }
                    if (index > 0)
                    {
                        Combo_Conditional_Item_5.IsEnabled = true;
                        Combo_Conditional_Item_5.SelectedValue = _qstnvariablDD5[index];
                        Combo_Conditional_Item_5.Foreground = new SolidColorBrush(Colors.Black);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        public void SetSelectedCriteriaOperator(string operatr, int row)
        {
            if (row == 1)
            {
                selectedOperator(Combo_Conditional_Operator_1, operatr);
            }
            else if (row == 2)
            {
                selectedOperator(Combo_Conditional_Operator_2, operatr);
            }
            else if (row == 3)
            {
                selectedOperator(Combo_Conditional_Operator_3, operatr);
            }
            else if (row == 4)
            {
                selectedOperator(Combo_Conditional_Operator_4, operatr);
            }
            else if (row == 5)
            {
                selectedOperator(Combo_Conditional_Operator_5, operatr);
            }
        }

        public void SetSelectedCriteriaValue(string value, int row)
        {
            if (row == 1)
            {
                Combo_Conditional_Value_1.Text = value;
            }
            else if (row == 2)
            {
                Combo_Conditional_Value_2.Text = value;
            }
            else if (row == 3)
            {
                Combo_Conditional_Value_3.Text = value;
            }
            else if (row == 4)
            {
                Combo_Conditional_Value_4.Text = value;
            }
            else if (row == 5)
            {
                Combo_Conditional_Value_5.Text = value;
            }
        }

        public void SetSelectedConditionalOption(string condition, int row)
        {
            if (row == 1)
            {
                if (condition == ExcelAddIn.Common.Constants.DP.InstructionAND)
                    Option_Conditional_And_1.IsChecked = true;
                else
                    Option_Conditional_Or_1.IsChecked = true;
            }
            else if (row == 2)
            {
                if (condition == ExcelAddIn.Common.Constants.DP.InstructionAND)
                    Option_Conditional_And_2.IsChecked = true;
                else
                    Option_Conditional_Or_2.IsChecked = true;
            }
            else if (row == 3)
            {
                if (condition == ExcelAddIn.Common.Constants.DP.InstructionAND)
                    Option_Conditional_And_3.IsChecked = true;
                else
                    Option_Conditional_Or_3.IsChecked = true;
            }
            else if (row == 4)
            {
                if (condition == ExcelAddIn.Common.Constants.DP.InstructionAND)
                    Option_Conditional_And_4.IsChecked = true;
                else
                    Option_Conditional_Or_4.IsChecked = true;
            }
        }

        private void selectedOperator(System.Windows.Controls.ComboBox cb, string opertaor)
        {
            try
            {
                if (opertaor != null)
                {
                    cb.IsEnabled = true;

                    if (opertaor.Equals("="))
                        cb.SelectedIndex = 0;
                    else if (opertaor.Equals("<>"))
                        cb.SelectedIndex = 1;
                    else if (opertaor.Equals("<"))
                        cb.SelectedIndex = 2;
                    else if (opertaor.Equals(">"))
                        cb.SelectedIndex = 3;
                    else if (opertaor.Equals("<="))
                        cb.SelectedIndex = 4;
                    else if (opertaor.Equals(">="))
                        cb.SelectedIndex = 5;

                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }
        #endregion

        private void DisableRightMenu(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void Combo_Conditional_Item_1_GotFocus(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.ComboBox sen = ((System.Windows.Controls.ComboBox)sender);
            combo = sen;
        }
    }
}
