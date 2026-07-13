using FilterSettingsView;
using log4net;
using Macromill.QCWeb.Tabulation;
using QC4Common.Logic;
using QC4Common.Model;
using QC4Common.Validation;
using Qc4Launcher.Classes;
using Qc4Launcher.Logic.MultiVariate;
using Qc4Launcher.Util;
using System;
using System.Collections.Generic;
using Vb = Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static FilterSettingsView.FilterSettingsClass;
using static Qc4Launcher.Forms.Multivariate_Analysis.CollectionClass;
using Excel = Microsoft.Office.Interop.Excel;

namespace Qc4Launcher.Forms.Multivariate_Analysis
{
    /// <summary>
    /// Interaction logic for PSM_ANALYSIS.xaml
    /// </summary>
    public partial class PSM_ANALYSIS : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        Dictionary<string, QuestionSettings> variableDictionary = new Dictionary<string, QuestionSettings>();
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private ProgressBar progress = null;
        Excel.Application XlApp = null;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private ObservableCollection<psmQuestions> _list;
        public ObservableCollection<psmQuestions> txt_Expensive_variable_List = new ObservableCollection<psmQuestions>();
        Excel.Workbook Worksheet;
        Window MainWindow;
        Thread thread;
        private QuestionSettings question = new QuestionSettings();
        public static Dictionary<string, QuestionSettings> dictionary = new Dictionary<string, QuestionSettings>();
        SourceVariableFromList sourceGetList = new SourceVariableFromList();
        ObservableCollection<psmQuestions> SourceVariableList = new ObservableCollection<psmQuestions>();
        ObservableCollection<psmQuestions> SourceNtype = new ObservableCollection<psmQuestions>();
        ObservableCollection<psmQuestions> SourceSAtype = new ObservableCollection<psmQuestions>();
        psmQuestions insta = new psmQuestions();
        List<string> drpSANList = new List<string>();
        public List<string> comboGraphValues = new List<string>();
        public bool InitialLoad = true;
        public PSM_ANALYSIS(Excel.Workbook workbook, Window mV_Main, Excel.Application XlApp)
        {
            //Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            Worksheet = workbook;
            MainWindow = mV_Main;
            InitializeComponent();
            this.XlApp = XlApp;
            set_valid_values_chbx.IsChecked = true;
            dictionary = Definiotion.VariableDictionary;
            variableDictionary = Definiotion.VariableDictionary;
            fdesign.MyComboBoxSelectionChanged += new FilterControlMulti.MyComboBoxSelectionChangedEventHandler(OnSelectionChanged);

            fdesign.MyButtonClick += new FilterControlMulti.MyButtonClickEventEventHandler(OnButtonClick);
            fdesign.MyCheckBoxClick += new FilterControlMulti.MyCheckBoxClickEventHandler(OnCheck);
            fdesign.MyRadioButtonClick += new FilterControlMulti.MyRadioButtonClickEventHandler(OnRadioClick);
            fdesign.MyTextBoxChange += new FilterControlMulti.MyTextBoxChangeEventHandler(OnTextBoxChange);
            IsInitialLoad = true;
            LoadingData();
            WritefilterData();
            set_valid_values_chbx.IsChecked = false;
            Expensive_color_txtbx.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DA9694"));
            TooExpensive_color_txtbx.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#C0504D"));
            Cheep_color_txtbx.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#95B3D7"));
            TooCheep_color_txtbx.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#4F81BD"));

            comboGraphValues.Add("No");
            comboGraphValues.Add("Horizontal");
            comboGraphValues.Add("Vertical");
            comboGraphValues.Add("Right Upper Diagonal Line");
            comboGraphValues.Add("Right Lower Diagonal Line");
            comboGraphValues.Add("From the Corner");
            comboGraphValues.Add("From the Center");

            background_combo.ItemsSource = comboGraphValues;

            background_combo.SelectedIndex = 0;

        }
        private Dictionary<string, Control> controlObj = new Dictionary<string, Control>();
        private List<String> Combo_Conditional_Item_1Choices = new List<string>();
        private List<String> Combo_Conditional_Item_2Choices = new List<string>();
        private List<String> Combo_Conditional_Item_3Choices = new List<string>();
        private List<String> Combo_Conditional_Item_4Choices = new List<string>();
        private List<String> Combo_Conditional_Item_5Choices = new List<string>();

        private string Combo_Conditional_Item_1selectedQuestionVariableType = string.Empty;
        private string Combo_Conditional_Item_2selectedQuestionVariableType = string.Empty;
        private string Combo_Conditional_Item_3selectedQuestionVariableType = string.Empty;
        private string Combo_Conditional_Item_4selectedQuestionVariableType = string.Empty;
        private string Combo_Conditional_Item_5selectedQuestionVariableType = string.Empty;

        private ObservableCollection<DataExport> _dataExport_LBVariablesToExport = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _dataExport_ListBoxCommonCopy = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD1 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD2 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD3 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD4 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD5 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD6 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablnumeric = new ObservableCollection<DataExport>();
        private ComboBox CmbClassify;
        private CheckBox ChkCriteria;
        private ComboBox CmbCriteria;
        int CmbClsy = 0;
        int CmbItm1 = 0;
        int CmbItm2 = 0;
        int CmbItm3 = 0;
        int CmbItm4 = 0;
        int CmbItm5 = 0;
        string CmbOpItm1 = "";
        string CmbOpItm2 = "";
        string CmbOpItm3 = "";
        string CmbOpItm4 = "";
        string CmbOpItm5 = "";
        bool IsInitialLoad = false;
        public Excel.Shape OptionSettingsMsg { get; set; }
        bool Combo_Conditional_Initial1 = false;
        bool Combo_Conditional_Initial2 = false;
        bool Combo_Conditional_Initial3 = false;
        bool Combo_Conditional_Initial4 = false;
        bool Combo_Conditional_Initial5 = false;
        private static readonly string[] SuggestionValues = {
            "DK"
        };

        private void OnCheck(object sender, RoutedEventArgs e)
        {
            if (Check_Refine_Condition.IsChecked == true)
            {
                fdesign.check(true);
            }
            if (Check_Refine_Condition.IsChecked == false)
            {
                fdesign.check(false);
            }
        }
        private void OnTextBoxChange(object sender, FilterControlMulti.MyTextBoxChangeEventArgs e)//TextChangedEventArgs args
        {


            if (e.sendr == "Combo_Conditional_Value_1" || e.sendr == "Combo_Conditional_Value_2" || e.sendr == "Combo_Conditional_Value_3" || e.sendr == "Combo_Conditional_Value_4" || e.sendr == "Combo_Conditional_Value_5")
            {


                bool enable = e.Text.Length > 0 ? true : false;
                foreach (TextBox tb
                in FindControls.FindLogicalChildren<TextBox>(this.Page_Refine))
                {
                    if (tb.Name == "Combo_Conditional_Value_1" && e.sendr == "Combo_Conditional_Value_1")
                    {


                        foreach (RadioButton rb
            in FindControls.FindLogicalChildren<RadioButton>(this.Page_Refine))
                        {
                            if (rb.Name == "Option_Conditional_And_1") rb.IsEnabled = enable;
                            else if (rb.Name == "Option_Conditional_Or_1") rb.IsEnabled = enable;
                        }
                    }
                    else if (tb.Name == "Combo_Conditional_Value_2" && e.sendr == "Combo_Conditional_Value_2")
                    {
                        foreach (RadioButton rb
              in FindControls.FindLogicalChildren<RadioButton>(this.Page_Refine))
                        {
                            if (rb.Name == "Option_Conditional_And_2") rb.IsEnabled = enable;
                            else if (rb.Name == "Option_Conditional_Or_2") rb.IsEnabled = enable;
                        }
                    }
                    else if (tb.Name == "Combo_Conditional_Value_3" && e.sendr == "Combo_Conditional_Value_3")
                    {
                        foreach (RadioButton rb
              in FindControls.FindLogicalChildren<RadioButton>(this.Page_Refine))
                        {
                            if (rb.Name == "Option_Conditional_And_3") rb.IsEnabled = enable;
                            else if (rb.Name == "Option_Conditional_Or_3") rb.IsEnabled = enable;
                        }
                    }
                    else if (tb.Name == "Combo_Conditional_Value_4" && e.sendr == "Combo_Conditional_Value_4")
                    {
                        foreach (RadioButton rb
              in FindControls.FindLogicalChildren<RadioButton>(this.Page_Refine))
                        {
                            if (rb.Name == "Option_Conditional_And_4") rb.IsEnabled = enable;
                            else if (rb.Name == "Option_Conditional_Or_4") rb.IsEnabled = enable;
                        }
                    }
                    else if (tb.Name == "Combo_Conditional_Value_5" && e.sendr == "Combo_Conditional_Value_5")
                    { }
                }
            }
        }

        private void OnRadioClick(object sender, FilterControlMulti.MyRadioButtonClickEventArgs e) //use for RadioClick click in filtersettings usercontrol
        {
            if ((e.sendr == "Option_Conditional_And_1" || e.sendr == "Option_Conditional_Or_1") && e.Check == true)
            {
                foreach (ComboBox tb
           in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
                {
                    if (tb.Name == "Combo_Conditional_Item_2")
                    {
                        tb.IsEnabled = true;
                        if (tb.SelectedIndex < 1)
                        {
                            if (!IsInitialLoad)
                            {
                                if (_qstnvariablDD3[0].QuestionVariable == "" || _qstnvariablDD3[0].QuestionVariable == null)
                                    tb.SelectedIndex = 1;
                                else
                                    tb.SelectedIndex = 0;
                            }
                        }
                        if (!IsInitialLoad)
                        {
                            if (_qstnvariablDD2[0].QuestionVariable == "" || _qstnvariablDD2[0].QuestionVariable == null)
                                _qstnvariablDD2.RemoveAt(0);
                        }
                        return;
                    }
                }
            }
            else if ((e.sendr == "Option_Conditional_And_2" || e.sendr == "Option_Conditional_Or_2") && e.Check == true)
            {
                foreach (ComboBox tb
           in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
                {
                    if (tb.Name == "Combo_Conditional_Item_3")
                    {
                        tb.IsEnabled = true;
                        if (tb.SelectedIndex < 1)
                        {
                            if (!IsInitialLoad)
                            {
                                if (_qstnvariablDD4[0].QuestionVariable == "" || _qstnvariablDD4[0].QuestionVariable == null)
                                    tb.SelectedIndex = 1;
                                else
                                    tb.SelectedIndex = 0;
                            }
                        }
                        if (!IsInitialLoad)
                        {
                            if (_qstnvariablDD3[0].QuestionVariable == "" || _qstnvariablDD3[0].QuestionVariable == null)
                                _qstnvariablDD3.RemoveAt(0);
                        }
                        return;
                    }
                }
            }
            else if ((e.sendr == "Option_Conditional_And_3" || e.sendr == "Option_Conditional_Or_3") && e.Check == true)
            {
                foreach (ComboBox tb
           in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
                {
                    if (tb.Name == "Combo_Conditional_Item_4")
                    {
                        tb.IsEnabled = true;

                        if (tb.SelectedIndex < 1)
                        {
                            if (!IsInitialLoad)
                            {
                                if (_qstnvariablDD5[0].QuestionVariable == "" || _qstnvariablDD5[0].QuestionVariable == null)
                                    tb.SelectedIndex = 1;
                                else
                                    tb.SelectedIndex = 0;
                            }
                        }
                        if (!IsInitialLoad)
                        {
                            if (_qstnvariablDD4[0].QuestionVariable == "" || _qstnvariablDD4[0].QuestionVariable == null)
                                _qstnvariablDD4.RemoveAt(0);
                        }
                        return;
                    }
                }
            }
            else if ((e.sendr == "Option_Conditional_And_4" || e.sendr == "Option_Conditional_Or_4") && e.Check == true)
            {
                foreach (ComboBox tb
           in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
                {
                    if (tb.Name == "Combo_Conditional_Item_5")
                    {
                        tb.IsEnabled = true;
                        if (tb.SelectedIndex < 1)
                        {
                            if (!IsInitialLoad)
                            {
                                tb.SelectedIndex = 1;
                            }
                        }
                        if (!IsInitialLoad)
                        {
                            if (_qstnvariablDD5[0].QuestionVariable == "" || _qstnvariablDD5[0].QuestionVariable == null)
                                _qstnvariablDD5.RemoveAt(0);
                        }
                        return;
                    }
                }
            }

        }
        private void Enable_Combo_Conditional_Items()
        {
            foreach (ComboBox cb
          in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
            {
                if (cb.Name == "Combo_Conditional_Item_1") cb.IsEnabled = true;

            }
            foreach (RadioButton tb
             in FindControls.FindLogicalChildren<RadioButton>(this.Page_Refine))
            {
                if ((tb.Name == "Option_Conditional_And_1" || tb.Name == "Option_Conditional_Or_1") && tb.IsChecked == true)
                {
                    foreach (ComboBox cb
            in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
                    {
                        if (cb.Name == "Combo_Conditional_Item_2") cb.IsEnabled = true;

                    }
                }
                else if ((tb.Name == "Option_Conditional_And_2" || tb.Name == "Option_Conditional_Or_2") && tb.IsChecked == true)
                {
                    foreach (ComboBox cb
            in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
                    {
                        if (cb.Name == "Combo_Conditional_Item_3") cb.IsEnabled = true;

                    }
                }
                else if ((tb.Name == "Option_Conditional_And_3" || tb.Name == "Option_Conditional_Or_3") && tb.IsChecked == true)
                {
                    foreach (ComboBox cb
            in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
                    {
                        if (cb.Name == "Combo_Conditional_Item_4") cb.IsEnabled = true;

                    }
                }
                else if ((tb.Name == "Option_Conditional_And_4" || tb.Name == "Option_Conditional_Or_4") && tb.IsChecked == true)
                {
                    foreach (ComboBox cb
           in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
                    {
                        if (cb.Name == "Combo_Conditional_Item_5") cb.IsEnabled = true;

                    }
                }

            }
        }

        private void EnableDisable_RadioButtons()
        {
            foreach (TextBox tb
           in FindControls.FindLogicalChildren<TextBox>(this.Page_Refine))
            {
                if (tb.Name == "Combo_Conditional_Value_1" && (tb.Text != "" && tb.Text != null))
                {
                    foreach (RadioButton rb
        in FindControls.FindLogicalChildren<RadioButton>(this.Page_Refine))
                    {
                        if (rb.Name == "Option_Conditional_And_1") rb.IsEnabled = true;
                        else if (rb.Name == "Option_Conditional_Or_1") rb.IsEnabled = true;

                    }
                }
                else if (tb.Name == "Combo_Conditional_Value_2" && (tb.Text != "" && tb.Text != null))
                {
                    foreach (RadioButton rb
          in FindControls.FindLogicalChildren<RadioButton>(this.Page_Refine))
                    {
                        if (rb.Name == "Option_Conditional_And_2") rb.IsEnabled = true;
                        else if (rb.Name == "Option_Conditional_Or_2") rb.IsEnabled = true;

                    }
                }
                else if (tb.Name == "Combo_Conditional_Value_3" && tb.Text != "" && tb.Text != null)
                {
                    foreach (RadioButton rb
         in FindControls.FindLogicalChildren<RadioButton>(this.Page_Refine))
                    {
                        if (rb.Name == "Option_Conditional_And_3") { rb.IsEnabled = true; rb.IsEnabled = true; }
                        else if (rb.Name == "Option_Conditional_Or_3") rb.IsEnabled = true;

                    }
                }
                else if (tb.Name == "Combo_Conditional_Value_4" && tb.Text != "" && tb.Text != null)
                {
                    foreach (RadioButton rb
          in FindControls.FindLogicalChildren<RadioButton>(this.Page_Refine))
                    {
                        if (rb.Name == "Option_Conditional_And_4") rb.IsEnabled = true;
                        else if (rb.Name == "Option_Conditional_Or_4") rb.IsEnabled = true;

                    }
                }
            }
        }
        private void EnableDisable_Load_RadioButtons()
        {
            foreach (TextBox tb
           in FindControls.FindLogicalChildren<TextBox>(this.Page_Refine))
            {
                if (tb.Name == "Combo_Conditional_Value_1" && (tb.Text != "" && tb.Text != null))
                {
                    foreach (RadioButton rb
        in FindControls.FindLogicalChildren<RadioButton>(this.Page_Refine))
                    {
                        if (rb.Name == "Option_Conditional_And_1") rb.IsEnabled = true;
                        else if (rb.Name == "Option_Conditional_Or_1") rb.IsEnabled = true;

                    }
                }
                else if (tb.Name == "Combo_Conditional_Value_2" && (tb.Text != "" && tb.Text != null))
                {
                    foreach (RadioButton rb
          in FindControls.FindLogicalChildren<RadioButton>(this.Page_Refine))
                    {
                        if (rb.Name == "Option_Conditional_And_2") rb.IsEnabled = true;
                        else if (rb.Name == "Option_Conditional_Or_2") rb.IsEnabled = true;

                    }
                }
                else if (tb.Name == "Combo_Conditional_Value_3" && tb.Text != "" && tb.Text != null)
                {
                    foreach (RadioButton rb
         in FindControls.FindLogicalChildren<RadioButton>(this.Page_Refine))
                    {
                        if (rb.Name == "Option_Conditional_And_3") { rb.IsEnabled = true; rb.IsChecked = true; }
                        else if (rb.Name == "Option_Conditional_Or_3") rb.IsEnabled = true;

                    }
                }
                else if (tb.Name == "Combo_Conditional_Value_4" && tb.Text != "" && tb.Text != null)
                {
                    foreach (RadioButton rb
          in FindControls.FindLogicalChildren<RadioButton>(this.Page_Refine))
                    {
                        if (rb.Name == "Option_Conditional_And_4") rb.IsEnabled = true;
                        else if (rb.Name == "Option_Conditional_Or_4") rb.IsEnabled = true;

                    }
                }
            }
        }

        private void OnCheck(object sender, FilterControlMulti.MyCheckBoxClickEventArgs e)
        {

            if (e.sendr == "Check_Refine_Condition1" && !IsInitialLoad)
            {
                if (e.Check == true)
                {
                    ComboBox Combo_Conditional_Item_1 = null;
                    ComboBox Combo_Conditional_Item_2 = null;
                    ComboBox Combo_Conditional_Item_3 = null;
                    ComboBox Combo_Conditional_Item_4 = null;
                    ComboBox Combo_Conditional_Item_5 = null;
                    ComboBox Combo_Conditional_Operator_1 = null;
                    ComboBox Combo_Conditional_Operator_2 = null;
                    ComboBox Combo_Conditional_Operator_3 = null;
                    ComboBox Combo_Conditional_Operator_4 = null;
                    ComboBox Combo_Conditional_Operator_5 = null;
                    TextBox Combo_Conditional_Value_1 = null;
                    TextBox Combo_Conditional_Value_2 = null;
                    TextBox Combo_Conditional_Value_3 = null;
                    TextBox Combo_Conditional_Value_4 = null;
                    TextBox Combo_Conditional_Value_5 = null;
                    foreach (ComboBox tb
            in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Item_1") Combo_Conditional_Item_1 = tb;
                        else if (tb.Name == "Combo_Conditional_Item_2") Combo_Conditional_Item_2 = tb;
                        else if (tb.Name == "Combo_Conditional_Item_3") Combo_Conditional_Item_3 = tb;
                        else if (tb.Name == "Combo_Conditional_Item_4") Combo_Conditional_Item_4 = tb;
                        else if (tb.Name == "Combo_Conditional_Item_5") Combo_Conditional_Item_5 = tb;
                        else if (tb.Name == "Combo_Conditional_Operator_1") Combo_Conditional_Operator_1 = tb;
                        else if (tb.Name == "Combo_Conditional_Operator_2") Combo_Conditional_Operator_2 = tb;
                        else if (tb.Name == "Combo_Conditional_Operator_3") Combo_Conditional_Operator_3 = tb;
                        else if (tb.Name == "Combo_Conditional_Operator_4") Combo_Conditional_Operator_4 = tb;
                        else if (tb.Name == "Combo_Conditional_Operator_5") Combo_Conditional_Operator_5 = tb;
                    }
                    foreach (TextBox tb
                in FindControls.FindLogicalChildren<TextBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Value_1") { Combo_Conditional_Value_1 = tb; }
                        else if (tb.Name == "Combo_Conditional_Value_2") { Combo_Conditional_Value_2 = tb; }
                        else if (tb.Name == "Combo_Conditional_Value_3") { Combo_Conditional_Value_3 = tb; }
                        else if (tb.Name == "Combo_Conditional_Value_4") { Combo_Conditional_Value_4 = tb; }
                        else if (tb.Name == "Combo_Conditional_Value_5") { Combo_Conditional_Value_5 = tb; }
                    }
                    Button BTnFilter1 = null;
                    Button BTnFilter2 = null;
                    Button BTnFilter3 = null;
                    Button BTnFilter4 = null;
                    Button BTnFilter5 = null;
                    foreach (Button tb
            in FindControls.FindLogicalChildren<Button>(this.Page_Refine))
                    {
                        if (tb.Name == "BTnFilter1") BTnFilter1 = tb;
                        else if (tb.Name == "BTnFilter2") BTnFilter2 = tb;
                        else if (tb.Name == "BTnFilter3") BTnFilter3 = tb;
                        else if (tb.Name == "BTnFilter4") BTnFilter4 = tb;
                        else if (tb.Name == "BTnFilter5") BTnFilter5 = tb;
                    }
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
                    }
                    if (Combo_Conditional_Operator_2.SelectedIndex >= 0)
                    {
                        Combo_Conditional_Value_2.IsEnabled = true;
                        BTnFilter2.IsEnabled = true;
                    }
                    if (Combo_Conditional_Operator_3.SelectedIndex >= 0)
                    {
                        Combo_Conditional_Value_3.IsEnabled = true;
                        BTnFilter3.IsEnabled = true;
                    }
                    if (Combo_Conditional_Operator_4.SelectedIndex >= 0)
                    {
                        Combo_Conditional_Value_4.IsEnabled = true;
                        BTnFilter4.IsEnabled = true;
                    }
                    if (Combo_Conditional_Operator_5.SelectedIndex >= 0)
                    {
                        Combo_Conditional_Value_5.IsEnabled = true;
                        BTnFilter5.IsEnabled = true;
                    }

                    EnableDisable_RadioButtons();

                }
                else if (e.Check == false)
                {
                    foreach (Label tb
              in FindControls.FindLogicalChildren<Label>(this.Page_Refine))
                    {
                        if (tb.Name == "lblCriteriaVariable") tb.IsEnabled = false;
                        else if (tb.Name == "lblOperator") tb.IsEnabled = false;
                        else if (tb.Name == "lblValue") tb.IsEnabled = false;
                    }

                    foreach (ComboBox tb
              in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Item_1") tb.IsEnabled = false;
                        else if (tb.Name == "Combo_Conditional_Operator_1") tb.IsEnabled = false;
                        else if (tb.Name == "Combo_Conditional_Item_2") tb.IsEnabled = false;
                        else if (tb.Name == "Combo_Conditional_Operator_2") tb.IsEnabled = false;
                        else if (tb.Name == "Combo_Conditional_Item_3") tb.IsEnabled = false;
                        else if (tb.Name == "Combo_Conditional_Operator_3") tb.IsEnabled = false;
                        else if (tb.Name == "Combo_Conditional_Item_4") tb.IsEnabled = false;
                        else if (tb.Name == "Combo_Conditional_Operator_4") tb.IsEnabled = false;
                        else if (tb.Name == "Combo_Conditional_Item_5") tb.IsEnabled = false;
                        else if (tb.Name == "Combo_Conditional_Operator_5") tb.IsEnabled = false;

                    }
                    foreach (TextBox tb
              in FindControls.FindLogicalChildren<TextBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Value_1") tb.IsEnabled = false;
                        else if (tb.Name == "Combo_Conditional_Value_2") tb.IsEnabled = false;
                        else if (tb.Name == "Combo_Conditional_Value_3") tb.IsEnabled = false;
                        else if (tb.Name == "Combo_Conditional_Value_4") tb.IsEnabled = false;
                        else if (tb.Name == "Combo_Conditional_Value_5") tb.IsEnabled = false;
                    }
                    foreach (RadioButton tb
              in FindControls.FindLogicalChildren<RadioButton>(this.Page_Refine))
                    {
                        if (tb.Name == "Option_Conditional_And_1") tb.IsEnabled = false;
                        else if (tb.Name == "Option_Conditional_Or_1") tb.IsEnabled = false;
                        else if (tb.Name == "Option_Conditional_And_2") tb.IsEnabled = false;
                        else if (tb.Name == "Option_Conditional_Or_2") tb.IsEnabled = false;
                        else if (tb.Name == "Option_Conditional_And_3") tb.IsEnabled = false;
                        else if (tb.Name == "Option_Conditional_Or_3") tb.IsEnabled = false;
                        else if (tb.Name == "Option_Conditional_And_4") tb.IsEnabled = false;
                        else if (tb.Name == "Option_Conditional_Or_4") tb.IsEnabled = false;
                    }
                    foreach (Button tb
            in FindControls.FindLogicalChildren<Button>(this.Page_Refine))
                    {
                        if (tb.Name == "BTnFilter1") tb.IsEnabled = false;
                        else if (tb.Name == "BTnFilter2") tb.IsEnabled = false;
                        else if (tb.Name == "BTnFilter3") tb.IsEnabled = false;
                        else if (tb.Name == "BTnFilter4") tb.IsEnabled = false;
                        else if (tb.Name == "BTnFilter5") tb.IsEnabled = false;
                    }
                }
            }
        }
        private string ChoiceSelection(string QuestionVariableType, List<String> CBcvChoices)
        {
            String selectedChoice = "";
            // string Combo_Conditional_Item_1selectedQuestionVariableType = "";//need to chasnge
            if (QuestionVariableType != "FA")
            {
                FilterSettingValue popUp = new FilterSettingValue(CBcvChoices, LocalResource.TEXT_FILTER_VALUE_DK_NO_ANSWER, LocalResource.TEXT_FILTER_VALUE_STAR_EXCLUDED);//TEXT_FILTER_VALUE_DK_NO_ANSWER  TEXT_FILTER_VALUE_STAR_EXCLUDED
                popUp.Owner = this;
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
                popUp.Owner = this;
                popUp.ShowDialog();
                if (string.IsNullOrEmpty(popUp.CurrentValue) == false)
                {
                    if (popUp.CurrentValue != LocalResource.TEXT_FILTER_VALUE_DK_NO_ANSWER)
                        selectedChoice = popUp.CurrentValue.Split(':')[0];
                    else
                        selectedChoice = popUp.CurrentValue;
                }
            }
            if (selectedChoice == LocalResource.TEXT_FILTER_VALUE_DK_NO_ANSWER || selectedChoice == LocalResource.TEXT_FILTER_VALUE_STAR_EXCLUDED)////TEXT_FILTER_VALUE_DK_NO_ANSWER  TEXT_FILTER_VALUE_STAR_EXCLUDED
                selectedChoice = selectedChoice.Split('.')[0];
            return selectedChoice;
        }

        private void OnButtonClick(object sender, FilterControlMulti.MyButtonClickEventArgs e) //use for button click in filtersettings usercontrol
        {

            if (e.sendr == "BTnFilter1")//criteria value button1
            {

                foreach (TextBox tb
               in FindControls.FindLogicalChildren<TextBox>(this.Page_Refine))
                {
                    if (tb.Name == "Combo_Conditional_Value_1")
                    {
                        string value = ChoiceSelection(Combo_Conditional_Item_1selectedQuestionVariableType, Combo_Conditional_Item_1Choices);
                        if (value != null && value != "")
                            tb.Text = value;
                        break;
                    }
                }
            }
            else if (e.sendr == "BTnFilter2")//criteria value button2
            {

                foreach (TextBox tb
               in FindControls.FindLogicalChildren<TextBox>(this.Page_Refine))
                {
                    if (tb.Name == "Combo_Conditional_Value_2")
                    {
                        string value = ChoiceSelection(Combo_Conditional_Item_2selectedQuestionVariableType, Combo_Conditional_Item_2Choices);
                        if (value != null && value != "")
                            tb.Text = value;
                        break;
                    }
                }
            }
            else if (e.sendr == "BTnFilter3")//criteria value button3
            {

                foreach (TextBox tb
               in FindControls.FindLogicalChildren<TextBox>(this.Page_Refine))
                {
                    if (tb.Name == "Combo_Conditional_Value_3")
                    {
                        string value = ChoiceSelection(Combo_Conditional_Item_3selectedQuestionVariableType, Combo_Conditional_Item_3Choices);
                        if (value != null && value != "")
                            tb.Text = value;
                        break;
                    }
                }
            }
            else if (e.sendr == "BTnFilter4")//criteria value button4
            {

                foreach (TextBox tb
               in FindControls.FindLogicalChildren<TextBox>(this.Page_Refine))
                {
                    if (tb.Name == "Combo_Conditional_Value_4")
                    {
                        string value = ChoiceSelection(Combo_Conditional_Item_4selectedQuestionVariableType, Combo_Conditional_Item_4Choices);
                        if (value != null && value != "")
                            tb.Text = value;
                        break;
                    }
                }
            }
            else if (e.sendr == "BTnFilter5")//criteria value button5
            {

                foreach (TextBox tb
               in FindControls.FindLogicalChildren<TextBox>(this.Page_Refine))
                {
                    if (tb.Name == "Combo_Conditional_Value_5")
                    {
                        string value = ChoiceSelection(Combo_Conditional_Item_5selectedQuestionVariableType, Combo_Conditional_Item_5Choices);
                        if (value != null && value != "")
                            tb.Text = value;
                        break;
                    }
                }
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
        private QC4Common.Util.FormUtil formUtil = new QC4Common.Util.FormUtil();
        private void OnSelectionChanged(object sender, FilterControlMulti.MyComboBoxSelectionChangedEventArgs e)//use for ComboSelectionChange in filtersettings usercontrol
        {
            if (e.sendr == "Combo_Conditional_Item_1")
            {
                if (e.LastSelectedText == "" && e.LastSelected == 0 && (((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).QuestionVariable == null || ((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).QuestionVariable == ""))
                {

                    foreach (ComboBox tb
              in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Operator_1")
                        {
                            tb.Items.Clear();
                            tb.IsEnabled = false;
                        }
                    }
                    foreach (TextBox tb
            in FindControls.FindLogicalChildren<TextBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Value_1") { tb.Text = ""; tb.IsEnabled = false; }
                    }
                    foreach (Button tb
           in FindControls.FindLogicalChildren<Button>(this.Page_Refine))
                    {
                        if (tb.Name == "BTnFilter1") tb.IsEnabled = false;
                    }
                    foreach (RadioButton tb
             in FindControls.FindLogicalChildren<RadioButton>(this.Page_Refine))
                    {
                        if (tb.Name == "Option_Conditional_And_1") { tb.IsChecked = false; tb.IsEnabled = false; }// tb.IsEnabled = false;
                        else if (tb.Name == "Option_Conditional_Or_1") { tb.IsChecked = false; tb.IsEnabled = false; }// tb.IsEnabled = false;

                    }


                    if (IsInitialLoad)
                    {
                        Combo_Conditional_Initial1 = true;
                    }
                }
                else if ((e.SelectedIndex > 0 && e.LastSelected != e.SelectedIndex) || (e.LastSelectedText == "" && e.SelectedIndex == 0 && _qstnvariablDD2[e.SelectedIndex].QuestionVariableType != ""))
                {
                    Combo_Conditional_Item_1Choices = ((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).Choisces;// _qstnvariablDD2[selectedindex].Choisces;
                    Combo_Conditional_Item_1selectedQuestionVariableType = ((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).QuestionVariableType.Split('/')[0];
                    ComboBox Combo_Conditional_Operator = null;
                    foreach (ComboBox tb
                   in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Operator_1")
                        {
                            Combo_Conditional_Operator = tb;
                            OperatorLoading(((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).QuestionVariableType.Split('/')[0], tb);
                            break;
                        }
                    }
                    if (IsInitialLoad)
                    {
                        Combo_Conditional_Initial1 = true;
                        if (CmbOpItm1 != "")
                        {
                            Combo_Conditional_Operator.SelectedIndex = -1;
                            Combo_Conditional_Operator.SelectedItem = CmbOpItm1;
                        }
                        else
                            Combo_Conditional_Operator.SelectedIndex = -1;
                    }
                    if (!IsInitialLoad)
                    {
                        Combo_Conditional_Operator.IsEnabled = true;
                        Combo_Conditional_Operator.SelectedIndex = -1;
                        foreach (TextBox tb
                       in FindControls.FindLogicalChildren<TextBox>(this.Page_Refine))
                        {
                            if (tb.Name == "Combo_Conditional_Value_1")
                            {
                                tb.Text = "";
                                tb.IsEnabled = false;
                                break;
                            }
                        }
                        foreach (Button tb
                       in FindControls.FindLogicalChildren<Button>(this.Page_Refine))
                        {
                            if (tb.Name == "BTnFilter1")
                            {
                                tb.IsEnabled = false;
                                break;
                            }
                        }
                        foreach (RadioButton tb
                       in FindControls.FindLogicalChildren<RadioButton>(this.Page_Refine))
                        {
                            if (tb.Name == "Option_Conditional_And_1")
                            {
                                tb.IsEnabled = false;
                            }
                            else if (tb.Name == "Option_Conditional_Or_1")
                            {
                                tb.IsEnabled = false;
                            }
                        }
                    }
                }
                if (IsInitialLoad && CmbItm2 > 0)
                    _qstnvariablDD2.RemoveAt(0);
            }
            else if (e.sendr == "Combo_Conditional_Item_2")
            {
                if (e.LastSelectedText == "" && e.LastSelected == 0 && (((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).QuestionVariable == null || ((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).QuestionVariable == ""))
                {
                    foreach (ComboBox tb
           in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Item_2")
                        {
                            tb.IsEnabled = false; break;
                        }
                    }

                    foreach (ComboBox tb
              in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Operator_2")
                        {
                            tb.Items.Clear(); tb.IsEnabled = false;
                            break;
                        }
                    }
                    foreach (TextBox tb
            in FindControls.FindLogicalChildren<TextBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Value_2") { tb.Text = ""; tb.IsEnabled = false; break; }
                    }
                    foreach (Button tb
           in FindControls.FindLogicalChildren<Button>(this.Page_Refine))
                    {
                        if (tb.Name == "BTnFilter2") { tb.IsEnabled = false; break; }
                    }
                    foreach (RadioButton tb
             in FindControls.FindLogicalChildren<RadioButton>(this.Page_Refine))
                    {
                        if (tb.Name == "Option_Conditional_And_1") { tb.IsChecked = false; }// tb.IsEnabled = false;
                        else if (tb.Name == "Option_Conditional_Or_1") { tb.IsChecked = false; }
                        else if (tb.Name == "Option_Conditional_And_2") { tb.IsChecked = false; tb.IsEnabled = false; }
                        else if (tb.Name == "Option_Conditional_Or_2") { tb.IsChecked = false; tb.IsEnabled = false; }


                    }
                    if (_qstnvariablDD2[e.SelectedIndex].QuestionVariable != "")
                        _qstnvariablDD2.Insert(0, DataExportObjectCreator());

                    if (IsInitialLoad)
                    {
                        Combo_Conditional_Initial2 = true;
                    }
                }
                else if ((e.SelectedIndex > 0 && e.LastSelected != e.SelectedIndex) || (e.LastSelectedText == "" && e.SelectedIndex == 0 && _qstnvariablDD3[e.SelectedIndex].QuestionVariableType != ""))
                {
                    //QuestionVariableType //QuestionVariable
                    Combo_Conditional_Item_2Choices = ((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).Choisces;
                    Combo_Conditional_Item_2selectedQuestionVariableType = ((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).QuestionVariableType.Split('/')[0];
                    ComboBox Combo_Conditional_Operator = null;
                    foreach (ComboBox tb
                   in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Operator_2")
                        {
                            Combo_Conditional_Operator = tb;
                            OperatorLoading(((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).QuestionVariableType.Split('/')[0], tb);
                            break;
                        }
                    }

                    if (IsInitialLoad)
                    {
                        Combo_Conditional_Initial2 = true;
                        if (CmbOpItm2 != "")
                        {
                            Combo_Conditional_Operator.SelectedIndex = -1;
                            Combo_Conditional_Operator.SelectedItem = CmbOpItm2;
                        }
                        else
                            Combo_Conditional_Operator.SelectedIndex = -1;
                    }
                    if (!IsInitialLoad)
                    {
                        Combo_Conditional_Operator.IsEnabled = true;
                        Combo_Conditional_Operator.SelectedIndex = -1;
                        foreach (TextBox tb
                 in FindControls.FindLogicalChildren<TextBox>(this.Page_Refine))
                        {
                            if (tb.Name == "Combo_Conditional_Value_2")
                            {
                                tb.Text = "";
                                tb.IsEnabled = false;
                                break;
                            }
                        }
                        foreach (Button tb
                       in FindControls.FindLogicalChildren<Button>(this.Page_Refine))
                        {
                            if (tb.Name == "BTnFilter2")
                            {
                                tb.IsEnabled = false;
                                break;
                            }
                        }
                        foreach (RadioButton tb
                       in FindControls.FindLogicalChildren<RadioButton>(this.Page_Refine))
                        {
                            if (tb.Name == "Option_Conditional_And_2")
                            {
                                tb.IsEnabled = false;
                            }
                            else if (tb.Name == "Option_Conditional_Or_2")
                            {
                                tb.IsEnabled = false;
                            }
                        }
                    }
                }
                if (IsInitialLoad && CmbItm3 > 0)
                    _qstnvariablDD3.RemoveAt(0);
            }
            else if (e.sendr == "Combo_Conditional_Item_3")
            {
                if (e.LastSelectedText == "" && e.LastSelected == 0 && (((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).QuestionVariable == null || ((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).QuestionVariable == ""))
                {
                    foreach (ComboBox tb
           in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Item_3")
                        {
                            tb.IsEnabled = (bool)false;
                        }
                    }

                    foreach (ComboBox tb
              in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Operator_3")
                        {
                            tb.Items.Clear(); tb.IsEnabled = false;
                        }
                    }
                    foreach (TextBox tb
            in FindControls.FindLogicalChildren<TextBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Value_3") { tb.Text = ""; tb.IsEnabled = false; }
                    }
                    foreach (Button tb
           in FindControls.FindLogicalChildren<Button>(this.Page_Refine))
                    {
                        if (tb.Name == "BTnFilter3") tb.IsEnabled = false;
                    }
                    foreach (RadioButton tb
             in FindControls.FindLogicalChildren<RadioButton>(this.Page_Refine))
                    {
                        if (tb.Name == "Option_Conditional_And_2") { tb.IsChecked = false; }
                        else if (tb.Name == "Option_Conditional_Or_2") { tb.IsChecked = false; }
                        else if (tb.Name == "Option_Conditional_And_3") { tb.IsChecked = false; tb.IsEnabled = false; }
                        else if (tb.Name == "Option_Conditional_Or_3") { tb.IsChecked = false; tb.IsEnabled = false; }

                    }
                    foreach (TextBox tb
                 in FindControls.FindLogicalChildren<TextBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Value_3") tb.Text = "";
                    }
                    if (_qstnvariablDD3[e.SelectedIndex].QuestionVariable != "")
                        _qstnvariablDD3.Insert(0, DataExportObjectCreator());
                    if (IsInitialLoad)
                    {
                        Combo_Conditional_Initial3 = true;
                    }
                }
                else if ((e.SelectedIndex > 0 && e.LastSelected != e.SelectedIndex) || (e.LastSelectedText == "" && e.SelectedIndex == 0 && _qstnvariablDD4[e.SelectedIndex].QuestionVariableType != ""))
                {
                    //QuestionVariableType //QuestionVariable
                    Combo_Conditional_Item_3Choices = ((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).Choisces;
                    Combo_Conditional_Item_3selectedQuestionVariableType = ((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).QuestionVariableType.Split('/')[0];
                    ComboBox Combo_Conditional_Operator = null;
                    foreach (ComboBox tb
                   in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Operator_3")
                        {
                            Combo_Conditional_Operator = tb;
                            OperatorLoading(((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).QuestionVariableType.Split('/')[0], tb);
                            break;
                        }
                    }

                    if (IsInitialLoad)
                    {
                        Combo_Conditional_Initial3 = true;
                        if (CmbOpItm3 != "")
                        {
                            Combo_Conditional_Operator.SelectedIndex = -1;
                            Combo_Conditional_Operator.SelectedItem = CmbOpItm3;
                        }
                        else
                            Combo_Conditional_Operator.SelectedIndex = -1;
                    }
                    if (!IsInitialLoad)
                    {
                        Combo_Conditional_Operator.IsEnabled = true;
                        Combo_Conditional_Operator.SelectedIndex = -1;
                        foreach (TextBox tb
                 in FindControls.FindLogicalChildren<TextBox>(this.Page_Refine))
                        {
                            if (tb.Name == "Combo_Conditional_Value_3")
                            {
                                tb.Text = "";
                                tb.IsEnabled = false;
                                break;
                            }
                        }
                        foreach (Button tb
                       in FindControls.FindLogicalChildren<Button>(this.Page_Refine))
                        {
                            if (tb.Name == "BTnFilter3")
                            {
                                tb.IsEnabled = false;
                                break;
                            }
                        }
                        foreach (RadioButton tb
                       in FindControls.FindLogicalChildren<RadioButton>(this.Page_Refine))
                        {
                            if (tb.Name == "Option_Conditional_And_3")
                            {
                                tb.IsEnabled = false;
                            }
                            else if (tb.Name == "Option_Conditional_Or_3")
                            {
                                tb.IsEnabled = false;
                            }
                        }
                    }
                }
                if (IsInitialLoad && CmbItm4 > 0)
                    _qstnvariablDD4.RemoveAt(0);
            }
            else if (e.sendr == "Combo_Conditional_Item_4")
            {
                if (e.LastSelectedText == "" && e.LastSelected == 0 && (((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).QuestionVariable == null || ((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).QuestionVariable == ""))
                {
                    foreach (ComboBox tb
           in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Item_4")
                        {
                            tb.IsEnabled = false;
                        }
                    }
                    foreach (ComboBox tb
              in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Operator_4")
                        {
                            tb.IsEnabled = false;
                            tb.Items.Clear();
                        }
                    }
                    foreach (TextBox tb
            in FindControls.FindLogicalChildren<TextBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Value_4") { tb.Text = ""; tb.IsEnabled = false; break; }
                    }
                    foreach (Button tb
           in FindControls.FindLogicalChildren<Button>(this.Page_Refine))
                    {
                        if (tb.Name == "BTnFilter4") { tb.IsEnabled = false; break; }
                    }
                    foreach (RadioButton tb
             in FindControls.FindLogicalChildren<RadioButton>(this.Page_Refine))
                    {
                        if (tb.Name == "Option_Conditional_And_3") { tb.IsChecked = false; }
                        else if (tb.Name == "Option_Conditional_Or_3") { tb.IsChecked = false; }
                        else if (tb.Name == "Option_Conditional_And_4") { tb.IsChecked = false; tb.IsEnabled = false; }
                        else if (tb.Name == "Option_Conditional_Or_4") { tb.IsChecked = false; tb.IsEnabled = false; }

                    }
                    if (_qstnvariablDD4[e.SelectedIndex].QuestionVariable != "")
                        _qstnvariablDD4.Insert(0, DataExportObjectCreator());
                    if (IsInitialLoad)
                    {
                        Combo_Conditional_Initial4 = true;
                    }
                }
                else if ((e.SelectedIndex > 0 && e.LastSelected != e.SelectedIndex) || (e.LastSelectedText == "" && e.SelectedIndex == 0 && _qstnvariablDD5[e.SelectedIndex].QuestionVariableType != ""))
                {
                    //QuestionVariableType //QuestionVariable
                    Combo_Conditional_Item_4Choices = ((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).Choisces;
                    Combo_Conditional_Item_4selectedQuestionVariableType = ((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).QuestionVariableType.Split('/')[0];
                    ComboBox Combo_Conditional_Operator = null;
                    foreach (ComboBox tb
                   in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Operator_4")
                        {
                            Combo_Conditional_Operator = tb;
                            OperatorLoading(((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).QuestionVariableType.Split('/')[0], tb);
                            break;
                        }
                    }

                    if (IsInitialLoad)
                    {
                        Combo_Conditional_Initial4 = true;
                        if (CmbOpItm4 != "")
                        {
                            Combo_Conditional_Operator.SelectedIndex = -1;
                            Combo_Conditional_Operator.SelectedItem = CmbOpItm4;
                        }
                        else
                            Combo_Conditional_Operator.SelectedIndex = -1;
                    }
                    if (!IsInitialLoad)
                    {
                        Combo_Conditional_Operator.IsEnabled = true;
                        Combo_Conditional_Operator.SelectedIndex = -1;
                        foreach (TextBox tb
                 in FindControls.FindLogicalChildren<TextBox>(this.Page_Refine))
                        {
                            if (tb.Name == "Combo_Conditional_Value_4")
                            {
                                tb.Text = "";
                                tb.IsEnabled = false;
                                break;
                            }
                        }
                        foreach (Button tb
                       in FindControls.FindLogicalChildren<Button>(this.Page_Refine))
                        {
                            if (tb.Name == "BTnFilter4")
                            {
                                tb.IsEnabled = false;
                                break;
                            }
                        }
                        foreach (RadioButton tb
                       in FindControls.FindLogicalChildren<RadioButton>(this.Page_Refine))
                        {
                            if (tb.Name == "Option_Conditional_And_4")
                            {
                                tb.IsEnabled = false;
                            }
                            else if (tb.Name == "Option_Conditional_Or_4")
                            {
                                tb.IsEnabled = false;
                            }
                        }
                    }
                }

                if (IsInitialLoad && CmbItm5 > 0)
                    _qstnvariablDD5.RemoveAt(0);
            }
            else if (e.sendr == "Combo_Conditional_Item_5")
            {
                if (e.LastSelected == 0 && (((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).QuestionVariable == null || ((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).QuestionVariable == ""))
                {
                    foreach (ComboBox tb
           in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Item_5")
                        {
                            tb.IsEnabled = false;
                        }
                    }
                    foreach (ComboBox tb
              in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Operator_5")
                        {
                            tb.IsEnabled = false;
                            tb.Items.Clear();
                        }
                    }
                    foreach (TextBox tb
            in FindControls.FindLogicalChildren<TextBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Value_5") { tb.Text = ""; tb.IsEnabled = false; break; }
                    }
                    foreach (Button tb
           in FindControls.FindLogicalChildren<Button>(this.Page_Refine))
                    {
                        if (tb.Name == "BTnFilter5") { tb.IsEnabled = false; break; }
                    }
                    foreach (RadioButton tb
            in FindControls.FindLogicalChildren<RadioButton>(this.Page_Refine))
                    {

                        if (tb.Name == "Option_Conditional_And_4") { tb.IsChecked = false; }
                        else if (tb.Name == "Option_Conditional_Or_4") { tb.IsChecked = false; }

                    }

                    if (_qstnvariablDD5[e.SelectedIndex].QuestionVariable != "")
                        _qstnvariablDD5.Insert(0, DataExportObjectCreator());
                    if (IsInitialLoad)
                    {
                        Combo_Conditional_Initial5 = true;
                    }
                }
                else if (e.LastSelected != e.SelectedIndex)
                {
                    //QuestionVariableType //QuestionVariable
                    Combo_Conditional_Item_5Choices = ((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).Choisces;
                    Combo_Conditional_Item_5selectedQuestionVariableType = ((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).QuestionVariableType.Split('/')[0];
                    ComboBox Combo_Conditional_Operator = null;
                    foreach (ComboBox tb
                   in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Operator_5")
                        {
                            Combo_Conditional_Operator = tb;
                            OperatorLoading(((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).QuestionVariableType.Split('/')[0], tb);
                            break;
                        }
                    }

                    if (IsInitialLoad)
                    {
                        Combo_Conditional_Initial5 = true;
                        if (CmbOpItm5 != "")
                        {
                            Combo_Conditional_Operator.SelectedIndex = -1;
                            Combo_Conditional_Operator.SelectedItem = CmbOpItm5;
                        }
                        else
                            Combo_Conditional_Operator.SelectedIndex = -1;
                    }
                    if (!IsInitialLoad)
                    {
                        Combo_Conditional_Operator.IsEnabled = true;
                        Combo_Conditional_Operator.SelectedIndex = -1;
                        foreach (TextBox tb
                 in FindControls.FindLogicalChildren<TextBox>(this.Page_Refine))
                        {
                            if (tb.Name == "Combo_Conditional_Value_5")
                            {
                                tb.Text = "";
                                tb.IsEnabled = false;
                                break;
                            }
                        }
                        foreach (Button tb
                       in FindControls.FindLogicalChildren<Button>(this.Page_Refine))
                        {
                            if (tb.Name == "BTnFilter5")
                            {
                                tb.IsEnabled = false;
                                break;
                            }
                        }
                    }
                }
                if (IsInitialLoad)
                {
                    IsInitialLoad = false;
                    bool isChecked = false;
                    foreach (CheckBox tb
                       in FindControls.FindLogicalChildren<CheckBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Check_Refine_Condition1")
                        {
                            isChecked = tb.IsChecked == true ? true : false;
                            break;
                        }
                    }
                    if (!isChecked)
                        OnCheck(null, new FilterControlMulti.MyCheckBoxClickEventArgs() { sendr = "Check_Refine_Condition1", Check = isChecked });
                }
            }
            else if (e.sendr == "Combo_Conditional_Operator_1")
            {
                ComboBox Combo_Conditional_Value = null;
                foreach (ComboBox tb
              in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
                {
                    if (tb.Name == "Combo_Conditional_Operator_1") { Combo_Conditional_Value = tb; break; }
                }
                if (Combo_Conditional_Value.SelectedIndex >= 0 && Combo_Conditional_Initial1)
                {
                    foreach (TextBox tb
                  in FindControls.FindLogicalChildren<TextBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Value_1") { tb.IsEnabled = true; break; }
                    }
                    foreach (Button tb
               in FindControls.FindLogicalChildren<Button>(this.Page_Refine))
                    {
                        if (tb.Name == "BTnFilter1") { tb.IsEnabled = true; break; }
                    }
                }
            }
            else if (e.sendr == "Combo_Conditional_Operator_2")
            {
                ComboBox Combo_Conditional_Value = null;
                foreach (ComboBox tb
              in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
                {
                    if (tb.Name == "Combo_Conditional_Operator_2") { Combo_Conditional_Value = tb; break; }
                }
                if (Combo_Conditional_Value.SelectedIndex >= 0 && Combo_Conditional_Initial2)
                {
                    foreach (TextBox tb
              in FindControls.FindLogicalChildren<TextBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Value_2") { tb.IsEnabled = true; break; }
                    }
                    foreach (Button tb
               in FindControls.FindLogicalChildren<Button>(this.Page_Refine))
                    {
                        if (tb.Name == "BTnFilter2") { tb.IsEnabled = true; break; }
                    }
                }
            }
            else if (e.sendr == "Combo_Conditional_Operator_3")
            {
                ComboBox Combo_Conditional_Value = null;
                foreach (ComboBox tb
              in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
                {
                    if (tb.Name == "Combo_Conditional_Operator_3") { Combo_Conditional_Value = tb; break; }
                }
                if (Combo_Conditional_Value.SelectedIndex >= 0 && Combo_Conditional_Initial3)
                {
                    foreach (TextBox tb
              in FindControls.FindLogicalChildren<TextBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Value_3") { tb.IsEnabled = true; break; }
                    }
                    foreach (Button tb
               in FindControls.FindLogicalChildren<Button>(this.Page_Refine))
                    {
                        if (tb.Name == "BTnFilter3") { tb.IsEnabled = true; break; }
                    }
                }
            }
            else if (e.sendr == "Combo_Conditional_Operator_4")
            {
                ComboBox Combo_Conditional_Value = null;
                foreach (ComboBox tb
              in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
                {
                    if (tb.Name == "Combo_Conditional_Operator_4") { Combo_Conditional_Value = tb; break; }
                }
                if (Combo_Conditional_Value.SelectedIndex >= 0 && Combo_Conditional_Initial4)
                {
                    foreach (TextBox tb
              in FindControls.FindLogicalChildren<TextBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Value_4") { tb.IsEnabled = true; break; }
                    }
                    foreach (Button tb
               in FindControls.FindLogicalChildren<Button>(this.Page_Refine))
                    {
                        if (tb.Name == "BTnFilter4") { tb.IsEnabled = true; break; }
                    }
                }
            }
            else if (e.sendr == "Combo_Conditional_Operator_5")
            {
                ComboBox Combo_Conditional_Value = null;
                foreach (ComboBox tb
              in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
                {
                    if (tb.Name == "Combo_Conditional_Operator_5") { Combo_Conditional_Value = tb; break; }
                }
                if (Combo_Conditional_Value.SelectedIndex >= 0 && Combo_Conditional_Initial5)
                {
                    foreach (TextBox tb
              in FindControls.FindLogicalChildren<TextBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Value_5") { tb.IsEnabled = true; break; }
                    }
                    foreach (Button tb
               in FindControls.FindLogicalChildren<Button>(this.Page_Refine))
                    {
                        if (tb.Name == "BTnFilter5") { tb.IsEnabled = true; break; }
                    }
                }
            }
        }
        FilterSettings fs = new FilterSettings();
        private void LoadingData()
        {
            fs.LoadingData(out _dataExport_LBVariablesToExport, out _qstnvariablDD1, out _qstnvariablDD2);

            _qstnvariablDD3 = new ObservableCollection<DataExport>(_qstnvariablDD2);
            _qstnvariablDD4 = new ObservableCollection<DataExport>(_qstnvariablDD2);
            _qstnvariablDD5 = new ObservableCollection<DataExport>(_qstnvariablDD2);
            _qstnvariablDD6 = new ObservableCollection<DataExport>(_qstnvariablDD2);
            // GetNType();


            foreach (ComboBox tb
                in FindControls.FindLogicalChildren<ComboBox>(this))
            {


                if (tb.Name == "LBVariablesToExport")
                {
                    tb.DataContext = _dataExport_LBVariablesToExport;
                }
                else if (tb.Name == "Combo_Classify_Item")
                {
                    CmbClassify = tb;
                    tb.DataContext = _qstnvariablDD1;
                }
                else if (tb.Name == "Combo_Conditional_Item_1")
                {
                    CmbCriteria = tb;
                    tb.DataContext = _qstnvariablDD2;
                }
                else if (tb.Name == "Combo_Conditional_Item_2")
                {
                    tb.DataContext = _qstnvariablDD3;
                }
                else if (tb.Name == "Combo_Conditional_Item_3")
                {
                    tb.DataContext = _qstnvariablDD4;
                }
                else if (tb.Name == "Combo_Conditional_Item_4")
                {
                    tb.DataContext = _qstnvariablDD5;
                }
                else if (tb.Name == "Combo_Conditional_Item_5")
                {
                    tb.DataContext = _qstnvariablDD6;
                }
                else if (tb.Name == "Combo_Summary_WeightBack")
                {
                    tb.DataContext = _qstnvariablnumeric;
                }
            }

        }

        private void OperatorLoading(String QuestionVariableType, ComboBox CBOperator)
        {
            string[] CBcvselectedQuestionVariableType = QuestionVariableType.Split(new Char[] { '/' });
            Combo_Conditional_Item_1selectedQuestionVariableType = CBcvselectedQuestionVariableType[0].ToString();
            if (QuestionVariableType == "SA" || QuestionVariableType == "N")
            {
                CBOperator.Items.Clear();
                CBOperator.Items.Add("=");
                CBOperator.Items.Add("<>");
                CBOperator.Items.Add("<");
                CBOperator.Items.Add(">");
                CBOperator.Items.Add("<=");
                CBOperator.Items.Add(">=");
                // CBOperator.DataContext = Enum.GetValues(typeof(Util.Enums.SA_N_Operators));
            }
            else if (QuestionVariableType == "FA" || QuestionVariableType == "MA")
            {
                CBOperator.Items.Clear();
                CBOperator.Items.Add("=");
                CBOperator.Items.Add("<>");
            }
            CBOperator.SelectedIndex = -1;
            CBOperator.IsEnabled = true;
        }
        private bool CheckValue(string textvalue, int categorycount, string type, string operatr)
        {
            return NumberCheck.CheckFromOption(textvalue, categorycount, type, operatr);
        }
        private bool CheckRefine()
        {


            bool validpath = false;
            bool combohasvalue = false;




            bool Check_Refine_Condition = false;
            foreach (CheckBox tb
                in FindControls.FindLogicalChildren<CheckBox>(this.Page_Refine))
            {
                if (tb.Name == "Check_Refine_Condition1")
                {
                    Check_Refine_Condition = tb.IsChecked == true ? true : false;
                    break;
                }
            }
            if (Check_Refine_Condition)
            {

                ComboBox Combo_Conditional_Item_1 = null;
                ComboBox Combo_Conditional_Item_2 = null;
                ComboBox Combo_Conditional_Item_3 = null;
                ComboBox Combo_Conditional_Item_4 = null;
                ComboBox Combo_Conditional_Item_5 = null;
                ComboBox Combo_Conditional_Operator_1 = null;
                ComboBox Combo_Conditional_Operator_2 = null;
                ComboBox Combo_Conditional_Operator_3 = null;
                ComboBox Combo_Conditional_Operator_4 = null;
                ComboBox Combo_Conditional_Operator_5 = null;
                TextBox Combo_Conditional_Value_1 = null;
                TextBox Combo_Conditional_Value_2 = null;
                TextBox Combo_Conditional_Value_3 = null;
                TextBox Combo_Conditional_Value_4 = null;
                TextBox Combo_Conditional_Value_5 = null;
                foreach (ComboBox tb
                in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
                {
                    switch (tb.Name)
                    {
                        case "Combo_Conditional_Item_1":
                            Combo_Conditional_Item_1 = tb;
                            break;
                        case "Combo_Conditional_Item_2":
                            Combo_Conditional_Item_2 = tb;
                            break;
                        case "Combo_Conditional_Item_3":
                            Combo_Conditional_Item_3 = tb;
                            break;
                        case "Combo_Conditional_Item_4":
                            Combo_Conditional_Item_4 = tb;
                            break;
                        case "Combo_Conditional_Item_5":
                            Combo_Conditional_Item_5 = tb;
                            break;
                        case "Combo_Conditional_Operator_1":
                            Combo_Conditional_Operator_1 = tb;
                            break;
                        case "Combo_Conditional_Operator_2":
                            Combo_Conditional_Operator_2 = tb;
                            break;
                        case "Combo_Conditional_Operator_3":
                            Combo_Conditional_Operator_3 = tb;
                            break;
                        case "Combo_Conditional_Operator_4":
                            Combo_Conditional_Operator_4 = tb;
                            break;
                        case "Combo_Conditional_Operator_5":
                            Combo_Conditional_Operator_5 = tb;
                            break;
                    }
                }
                foreach (TextBox tb
                in FindControls.FindLogicalChildren<TextBox>(this.Page_Refine))
                {
                    switch (tb.Name)
                    {
                        case "Combo_Conditional_Value_1":
                            Combo_Conditional_Value_1 = tb;
                            break;
                        case "Combo_Conditional_Value_2":
                            Combo_Conditional_Value_2 = tb;
                            break;
                        case "Combo_Conditional_Value_3":
                            Combo_Conditional_Value_3 = tb;
                            break;
                        case "Combo_Conditional_Value_4":
                            Combo_Conditional_Value_4 = tb;
                            break;
                        case "Combo_Conditional_Value_5":
                            Combo_Conditional_Value_5 = tb;
                            break;
                    }
                }

                NumberCheck.Error_Mesage = "";
                string variabletype = "";
                int catcount = 0;
                string wMsg = "";
                object selectedVar = Combo_Conditional_Item_1.SelectedItem;
                string variable = selectedVar == null || ((DataExport)selectedVar).QuestionVariable == "" ? "" : ((DataExport)(Combo_Conditional_Item_1).SelectedItem).QuestionVariable;
                string operatorr = Combo_Conditional_Operator_1.Text;
                string value = Combo_Conditional_Value_1.Text;
                if (selectedVar != null)
                    variabletype = ((DataExport)(Combo_Conditional_Item_1).SelectedItem).QuestionVariableType == null ? "" : (((DataExport)(Combo_Conditional_Item_1).SelectedItem).QuestionVariableType.Split('/')[0]);
                if (((selectedVar == null || String.IsNullOrEmpty(variable)) && (!String.IsNullOrEmpty(operatorr) || !String.IsNullOrEmpty(value))) || (!String.IsNullOrEmpty(variable) && (String.IsNullOrEmpty(operatorr) || String.IsNullOrEmpty(value))))
                {
                    if (selectedVar == null)
                        Combo_Conditional_Item_1.Text = "";
                    MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_FILTER_CRITERIA_VARIABLE_MISSING, 1));//string.Format(AddinResource.DECST_PARAMN_TOOLTIPHEADER, i)
                    return false;
                }
                else
                {
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
                    if (variable != "" && variable != null)
                    {
                        catcount = ((DataExport)(Combo_Conditional_Item_1).SelectedItem).Choisces.Count == 0 ? 0 : Convert.ToInt32(((DataExport)(Combo_Conditional_Item_1).SelectedItem).Choisces.Count);
                        if (!CheckValue(Combo_Conditional_Value_1.Text, catcount, variabletype, operatorr) && !(Combo_Conditional_Value_1.Text.Equals("DK") || Combo_Conditional_Value_1.Text.Equals("*")) && !(variabletype == QC4Common.Common.Constants.AnswerType.FA))
                        {
                            if (NumberCheck.Error_Mesage != "")
                            {
                                if (NumberCheck.Error_Mesage == "ERR_MSG_DATAEXPORT_SET_NUMERIC")
                                    MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_DATAEXPORT_SET_NUMERIC, 1));
                            }
                            else if (Vb.Information.IsNumeric(value))
                            {
                                if (Convert.ToDecimal(value) <= 0 || Convert.ToDecimal(value) > catcount)
                                {
                                    MessageDialog.ErrorOk(string.Format(LocalResource.FILTER_SA_MA_INVALID, 1, ("1-" + catcount)));
                                }
                                else
                                {
                                    MessageDialog.ErrorOk(string.Format(LocalResource.FILTER_SA_MA_INVALID, 1, ("1-" + catcount)));

                                }
                            }
                            else
                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_FILTER_CRITERIA_NOT_OP, 1));
                            return false;
                        }
                    }
                }
                selectedVar = Combo_Conditional_Item_2.SelectedItem;
                variable = selectedVar == null || ((DataExport)selectedVar).QuestionVariable == "" ? "" : ((DataExport)(Combo_Conditional_Item_2).SelectedItem).QuestionVariable;
                operatorr = Combo_Conditional_Operator_2.Text;
                value = Combo_Conditional_Value_2.Text;
                if (selectedVar != null)
                    variabletype = ((DataExport)(Combo_Conditional_Item_2).SelectedItem).QuestionVariableType == null ? "" : (((DataExport)(Combo_Conditional_Item_2).SelectedItem).QuestionVariableType.Split('/')[0]);
                if (((selectedVar == null || String.IsNullOrEmpty(variable)) && (!String.IsNullOrEmpty(operatorr) || !String.IsNullOrEmpty(value))) || (!String.IsNullOrEmpty(variable) && (String.IsNullOrEmpty(operatorr) || String.IsNullOrEmpty(value))))
                {
                    if (selectedVar == null)
                        Combo_Conditional_Item_2.Text = "";
                    MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_FILTER_CRITERIA_VARIABLE_MISSING, 2));
                    return false;
                }
                else
                {
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
                    if (variable != "" && variable != null)
                    {
                        catcount = ((DataExport)(Combo_Conditional_Item_2).SelectedItem).Choisces.Count == 0 ? 0 : Convert.ToInt32(((DataExport)(Combo_Conditional_Item_2).SelectedItem).Choisces.Count);
                        if (!CheckValue(Combo_Conditional_Value_2.Text, catcount, variabletype, operatorr) && !(Combo_Conditional_Value_2.Text.Equals("DK") || Combo_Conditional_Value_2.Text.Equals("*")) && !(variabletype == QC4Common.Common.Constants.AnswerType.FA))
                        {
                            if (NumberCheck.Error_Mesage != "")
                            {
                                if (NumberCheck.Error_Mesage == "ERR_MSG_DATAEXPORT_SET_NUMERIC")
                                    MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_DATAEXPORT_SET_NUMERIC, 2));
                            }
                            else if (Vb.Information.IsNumeric(value))
                            {
                                if (Convert.ToDecimal(value) <= 0 || Convert.ToDecimal(value) > catcount)
                                {
                                    MessageDialog.ErrorOk(string.Format(LocalResource.FILTER_SA_MA_INVALID, 2, ("1-" + catcount)));
                                }
                                else
                                {
                                    MessageDialog.ErrorOk(string.Format(LocalResource.FILTER_SA_MA_INVALID, 2, ("1-" + catcount)));

                                }
                            }
                            else
                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_FILTER_CRITERIA_NOT_OP, 2));
                            return false;
                        }
                    }
                }
                selectedVar = Combo_Conditional_Item_3.SelectedItem;
                variable = selectedVar == null || ((DataExport)selectedVar).QuestionVariable == "" ? "" : ((DataExport)(Combo_Conditional_Item_3).SelectedItem).QuestionVariable;
                operatorr = Combo_Conditional_Operator_3.Text;
                value = Combo_Conditional_Value_3.Text;
                if (selectedVar != null)
                    variabletype = ((DataExport)(Combo_Conditional_Item_3).SelectedItem).QuestionVariableType == null ? "" : (((DataExport)(Combo_Conditional_Item_3).SelectedItem).QuestionVariableType.Split('/')[0]);
                if (((selectedVar == null || String.IsNullOrEmpty(variable)) && (!String.IsNullOrEmpty(operatorr) || !String.IsNullOrEmpty(value))) || (!String.IsNullOrEmpty(variable) && (String.IsNullOrEmpty(operatorr) || String.IsNullOrEmpty(value))))
                {
                    if (selectedVar == null)
                        Combo_Conditional_Item_3.Text = "";
                    MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_FILTER_CRITERIA_VARIABLE_MISSING, 3));
                    return false;
                }
                else
                {
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
                    if (variable != "" && variable != null)
                    {
                        catcount = ((DataExport)(Combo_Conditional_Item_3).SelectedItem).Choisces.Count == 0 ? 0 : Convert.ToInt32(((DataExport)(Combo_Conditional_Item_3).SelectedItem).Choisces.Count);
                        if (!CheckValue(Combo_Conditional_Value_3.Text, catcount, variabletype, operatorr) && !(Combo_Conditional_Value_3.Text.Equals("DK") || Combo_Conditional_Value_3.Text.Equals("*")) && !(variabletype == QC4Common.Common.Constants.AnswerType.FA))
                        {
                            if (NumberCheck.Error_Mesage != "")
                            {
                                if (NumberCheck.Error_Mesage == "ERR_MSG_DATAEXPORT_SET_NUMERIC")
                                    MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_DATAEXPORT_SET_NUMERIC, 3));
                            }
                            else if (Vb.Information.IsNumeric(value))
                            {
                                if (Convert.ToDecimal(value) <= 0 || Convert.ToDecimal(value) > catcount)
                                {
                                    MessageDialog.ErrorOk(string.Format(LocalResource.FILTER_SA_MA_INVALID, 3, ("1-" + catcount)));
                                }
                                else
                                {
                                    MessageDialog.ErrorOk(string.Format(LocalResource.FILTER_SA_MA_INVALID, 3, ("1-" + catcount)));

                                }
                            }
                            else
                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_FILTER_CRITERIA_NOT_OP, 3));
                            return false;
                        }
                    }
                }
                selectedVar = Combo_Conditional_Item_4.SelectedItem;
                variable = selectedVar == null || ((DataExport)selectedVar).QuestionVariable == "" ? "" : ((DataExport)(Combo_Conditional_Item_4).SelectedItem).QuestionVariable;
                operatorr = Combo_Conditional_Operator_4.Text;
                value = Combo_Conditional_Value_4.Text;
                if (selectedVar != null)
                    variabletype = ((DataExport)(Combo_Conditional_Item_4).SelectedItem).QuestionVariableType == null ? "" : (((DataExport)(Combo_Conditional_Item_4).SelectedItem).QuestionVariableType.Split('/')[0]);
                if (((selectedVar == null || String.IsNullOrEmpty(variable)) && (!String.IsNullOrEmpty(operatorr) || !String.IsNullOrEmpty(value))) || (!String.IsNullOrEmpty(variable) && (String.IsNullOrEmpty(operatorr) || String.IsNullOrEmpty(value))))
                {
                    if (selectedVar == null)
                        Combo_Conditional_Item_4.Text = "";
                    MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_FILTER_CRITERIA_VARIABLE_MISSING, 4));
                    return false;
                }
                else
                {
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
                    if (variable != "" && variable != null)
                    {
                        catcount = ((DataExport)(Combo_Conditional_Item_4).SelectedItem).Choisces.Count == 0 ? 0 : Convert.ToInt32(((DataExport)(Combo_Conditional_Item_4).SelectedItem).Choisces.Count);
                        if (!CheckValue(Combo_Conditional_Value_4.Text, catcount, variabletype, operatorr) && !(Combo_Conditional_Value_4.Text.Equals("DK") || Combo_Conditional_Value_4.Text.Equals("*")) && !(variabletype == QC4Common.Common.Constants.AnswerType.FA))
                        {
                            if (NumberCheck.Error_Mesage != "")
                            {
                                if (NumberCheck.Error_Mesage == "ERR_MSG_DATAEXPORT_SET_NUMERIC")
                                    MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_DATAEXPORT_SET_NUMERIC, 4));
                            }
                            else if (Vb.Information.IsNumeric(value))
                            {
                                if (Convert.ToDecimal(value) <= 0 || Convert.ToDecimal(value) > catcount)
                                {
                                    MessageDialog.ErrorOk(string.Format(LocalResource.FILTER_SA_MA_INVALID, 4, ("1-" + catcount)));
                                }
                                else
                                {
                                    MessageDialog.ErrorOk(string.Format(LocalResource.FILTER_SA_MA_INVALID, 4, ("1-" + catcount)));

                                }
                            }
                            else
                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_FILTER_CRITERIA_NOT_OP, 4));
                            return false;
                        }
                    }
                }
                selectedVar = Combo_Conditional_Item_5.SelectedItem;
                variable = selectedVar == null || ((DataExport)selectedVar).QuestionVariable == "" ? "" : ((DataExport)(Combo_Conditional_Item_5).SelectedItem).QuestionVariable;
                operatorr = Combo_Conditional_Operator_5.Text;
                value = Combo_Conditional_Value_5.Text;
                if (selectedVar != null)
                    variabletype = ((DataExport)(Combo_Conditional_Item_5).SelectedItem).QuestionVariableType == null ? "" : (((DataExport)(Combo_Conditional_Item_5).SelectedItem).QuestionVariableType.Split('/')[0]);
                if (((selectedVar == null || String.IsNullOrEmpty(variable)) && (!String.IsNullOrEmpty(operatorr) || !String.IsNullOrEmpty(value))) || (!String.IsNullOrEmpty(variable) && (String.IsNullOrEmpty(operatorr) || String.IsNullOrEmpty(value))))
                {
                    if (selectedVar == null)
                        Combo_Conditional_Item_5.Text = "";
                    MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_FILTER_CRITERIA_VARIABLE_MISSING, 5));
                    return false;
                }
                else
                {
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
                    if (variable != "" && variable != null)
                    {

                        catcount = ((DataExport)(Combo_Conditional_Item_5).SelectedItem).Choisces.Count == 0 ? 0 : Convert.ToInt32(((DataExport)(Combo_Conditional_Item_5).SelectedItem).Choisces.Count);
                        if (!CheckValue(Combo_Conditional_Value_5.Text, catcount, variabletype, operatorr) && !(Combo_Conditional_Value_5.Text.Equals("DK") || Combo_Conditional_Value_5.Text.Equals("*")) && !(variabletype == QC4Common.Common.Constants.AnswerType.FA))
                        {
                            if (NumberCheck.Error_Mesage != "")
                            {
                                if (NumberCheck.Error_Mesage == "ERR_MSG_DATAEXPORT_SET_NUMERIC")
                                    MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_DATAEXPORT_SET_NUMERIC, 5));
                            }
                            else if (Vb.Information.IsNumeric(value))
                            {
                                if (Convert.ToDecimal(value) <= 0 || Convert.ToDecimal(value) > catcount)
                                {
                                    MessageDialog.ErrorOk(string.Format(LocalResource.FILTER_SA_MA_INVALID, 5, ("1-" + catcount)));
                                }
                                else
                                {
                                    MessageDialog.ErrorOk(string.Format(LocalResource.FILTER_SA_MA_INVALID, 5, ("1-" + catcount)));

                                }
                            }
                            else
                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_FILTER_CRITERIA_NOT_OP, 5));
                            return false;
                        }
                    }
                }
                return true;
            }
            else
                return true;
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

        private void DisableRightMenu(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }
        private void b1SetColor(object sender, MouseButtonEventArgs e)
        {
            if (ExpGrid != null)
                ExpGrid.Focus();
        }

        System.Windows.Controls.DataGrid ExpGrid = null;
        private void Multi_Process_Grid_CurrentCellChanged(object sender, EventArgs e)
        {

            var dg = (System.Windows.Controls.DataGrid)sender;
            ExpGrid = dg;
            dg.Focus();
        }

        private void Btn_close_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Savesettings();
            }
            catch (Exception ex)
            {

            }
            this.Close();

        }
        private bool handleSelection = true;
        private void Combo_Answer_type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (!string.IsNullOrEmpty(txt_Expensive_variable.Text) || !string.IsNullOrEmpty(txt_Cheap_variable.Text) || !string.IsNullOrEmpty(txt_TooExpensive_variable.Text) || !string.IsNullOrEmpty(txt_Tocheap_variable.Text))
            {
                if (handleSelection)
                {
                    string errormsg = LocalResource.MULTI_PSM_MSGBX_CHANGING_AN_ANSWERTYPE;
                    System.Windows.Forms.DialogResult result;
                    result = MessageDialog.InfoYesNo(errormsg, System.Windows.Forms.MessageBoxIcon.Question);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        txt_Expensive_variable.Text = string.Empty;
                        txt_Cheap_variable.Text = string.Empty;
                        txt_TooExpensive_variable.Text = string.Empty;
                        txt_Tocheap_variable.Text = string.Empty;
                        txt_Expensive_variable_List.Clear();
                        txt_Cheap_variable_List.Clear();
                        txt_Tocheap_variable_List.Clear();
                        txt_TooExpensive_variable_List.Clear();
                        LoadData();

                        txt_min_val2.Text = string.Empty;
                        txt_max_val2.Text = string.Empty;
                        txt_mean_val2.Text = string.Empty;

                        txt_min_val1.Text = string.Empty;
                        txt_max_val1.Text = string.Empty;
                        txt_mean_val1.Text = string.Empty;

                        txt_min_val3.Text = string.Empty;
                        txt_max_val3.Text = string.Empty;
                        txt_mean_val3.Text = string.Empty;

                        txt_min_val4.Text = string.Empty;
                        txt_max_val4.Text = string.Empty;
                        txt_mean_val4.Text = string.Empty;

                        txtbx1.Text = string.Empty;
                        txtbx2.Text = string.Empty;
                        txtbx3.Text = string.Empty;
                        txtbx4.Text = string.Empty;
                        txtbx5.Text = string.Empty;
                        txtbx6.Text = string.Empty;
                        txtbx7.Text = string.Empty;
                        txtbx8.Text = string.Empty;
                        txtbx9.Text = string.Empty;
                        txtbx10.Text = string.Empty;
                        txtbx11.Text = string.Empty;
                        txtbx12.Text = string.Empty;
                        txtbx13.Text = string.Empty;
                        txtbx14.Text = string.Empty;
                        txtbx15.Text = string.Empty;
                        txtbx16.Text = string.Empty;
                        txtbx17.Text = string.Empty;
                        txtbx18.Text = string.Empty;
                        txtbx19.Text = string.Empty;
                        txtbx20.Text = string.Empty;
                        txtbx21.Text = string.Empty;
                        txtbx22.Text = string.Empty;
                        txtbx23.Text = string.Empty;
                        txtbx24.Text = string.Empty;
                        txtbx25.Text = string.Empty;
                        txtbx26.Text = string.Empty;
                        txtbx27.Text = string.Empty;
                        txtbx28.Text = string.Empty;



                    }
                    else
                    {
                        handleSelection = false;
                        combo_Answer_type.SelectedItem = e.RemovedItems[0];
                        return;
                    }
                }
                handleSelection = true;
            }
            Multi_Process_Grid.DataContext = null;
            string selectedFilterType = combo_Answer_type.SelectedItem.ToString();

            try
            {
                switch (selectedFilterType)
                {
                    case "N":
                        Multi_Process_Grid.DataContext = SourceNtype;
                        break;

                    case "SA":
                        Multi_Process_Grid.DataContext = SourceSAtype;
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void Btn_rgt_arrw_Click(object sender, RoutedEventArgs e)
        {
            if (Multi_Process_Grid.SelectedItem != null)
            {
                psmQuestions crs = (psmQuestions)Multi_Process_Grid.SelectedItem;
                if (crs.AnswerType == QuestionType.N.ToString())
                {
                    if (!string.IsNullOrEmpty(txt_Expensive_variable.Text))
                    {
                        if (txt_Expensive_variable_List[0].AnswerType == QuestionType.N.ToString()) { SourceNtype.Add(txt_Expensive_variable_List[0]); SourceNtype.Sort(k => k.OrderNo); }
                        else { SourceSAtype.Add(txt_Expensive_variable_List[0]); SourceSAtype.Sort(k => k.OrderNo); }
                        txt_Expensive_variable_List.Clear();
                        txt_Expensive_variable_List.Add(crs);
                        SourceNtype.Remove(crs);
                        txt_Expensive_variable.Text = txt_Expensive_variable_List[0].Variable;


                    }
                    else
                    {
                        txt_Expensive_variable_List.Clear();
                        txt_Expensive_variable_List.Add(crs);
                        SourceNtype.Remove(crs);
                        txt_Expensive_variable.Text = txt_Expensive_variable_List[0].Variable;
                    }

                }
                else
                {
                    if (!string.IsNullOrEmpty(txt_Expensive_variable.Text))
                    {
                        if (txt_Expensive_variable_List[0].AnswerType == QuestionType.N.ToString()) { SourceNtype.Add(txt_Expensive_variable_List[0]); SourceNtype.Sort(k => k.OrderNo); }
                        else { SourceSAtype.Add(txt_Expensive_variable_List[0]); SourceSAtype.Sort(k => k.OrderNo); }
                        txt_Expensive_variable_List.Clear();
                        txt_Expensive_variable_List.Add(crs);
                        SourceSAtype.Remove(crs);
                        txt_Expensive_variable.Text = txt_Expensive_variable_List[0].Variable;


                    }
                    else
                    {
                        txt_Expensive_variable_List.Clear();
                        txt_Expensive_variable_List.Add(crs);
                        SourceSAtype.Remove(crs);
                        txt_Expensive_variable.Text = txt_Expensive_variable_List[0].Variable;
                    }
                }
                if (Multi_Process_Grid != null)
                {
                    ScrollViewer scrollViewer = GetVisualChild<ScrollViewer>(Multi_Process_Grid);
                    if (scrollViewer != null)
                    {
                        scrollViewer.ScrollToTop();
                    }
                }

            }

            Multi_Process_Grid.Focus();
        }
        public void LoadData()
        {
            SourceVariableList = new ObservableCollection<psmQuestions>(sourceGetList.GetVariableFromList(Worksheet, "List_Item_SAN"));
            SourceNtype = sourceGetList.GetNVariable(SourceVariableList).ListN;
            SourceSAtype = sourceGetList.GetNVariable(SourceVariableList).ListSA;
            sourceGetList.RemoveItem(SourceNtype, insta);








        }
        public class FilterData
        {
            public QuestionSettings variable { get; set; }
            public string Operator { get; set; }
            public string Value { get; set; }
            public string AndOr { get; set; }
        }
        List<FilterData> filtervalues = new List<FilterData>();
        public void WritefilterData()
        {

            try
            {
                var res = Util.ExcelUtil.GetWorkSheetBySheetName(Worksheet, Constants.SheetType.sh_Sheet2); //GetWorkSheetByCodeName(Worksheet, Constants.SheetCodeName.MultiVariate);
                Excel.Range crstart = res.Cells[17, 2];
                Excel.Range crlast = res.Cells[22, 235];
                Excel.Range rar = res.get_Range(crstart, crlast);
                var val = rar.Value;

                if (val[1, 1] != null)
                {

                    for (int i = 1; i <= val.GetLength(0); i++)
                    {
                        FilterData obj = new FilterData();
                        if (val[i, 1] != null)
                        {


                            if (val[i, 1] != null)
                            {
                                string criteriaVariable = val[i, 2];
                                if (i == 1)
                                {
                                    // Check_Refine_Condition.IsChecked = true;
                                    //Criteria_Control.EnableCriteriaControl();
                                }
                                DataExport target = null;
                                int index = 0;
                                bool enable = true;
                                foreach (ComboBox tb in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
                                {
                                    if (tb.Name == "Combo_Conditional_Item_1" && i == 1)
                                    {
                                        target = _qstnvariablDD2.Where(z => z.QuestionVariable == criteriaVariable).FirstOrDefault();
                                        index = target == null ? 0 : _qstnvariablDD2.IndexOf(target);
                                        CmbItm1 = index;
                                        if (index == 0)
                                        {
                                            tb.IsEnabled = false;
                                            tb.SelectedValue = 0;
                                        }
                                        if (index > 0)
                                        {
                                            tb.SelectedValue = _qstnvariablDD2[index];
                                            tb.IsEnabled = true;
                                            tb.Foreground = new SolidColorBrush(Colors.Black);
                                        }
                                    }
                                    else if (tb.Name == "Combo_Conditional_Item_2" && i == 2)
                                    {
                                        target = _qstnvariablDD3.Where(z => z.QuestionVariable == criteriaVariable).FirstOrDefault();
                                        index = target == null ? 0 : _qstnvariablDD3.IndexOf(target);
                                        CmbItm2 = index;
                                        if (index == 0)
                                        {
                                            tb.IsEnabled = false;
                                            tb.SelectedValue = 0;
                                        }
                                        if (index > 0)
                                        {
                                            tb.SelectedValue = _qstnvariablDD3[index];
                                            tb.IsEnabled = true;
                                            tb.Foreground = new SolidColorBrush(Colors.Black);
                                        }
                                    }
                                    else if (tb.Name == "Combo_Conditional_Item_3" && i == 3)
                                    {
                                        target = _qstnvariablDD4.Where(z => z.QuestionVariable == criteriaVariable).FirstOrDefault();
                                        index = target == null ? 0 : _qstnvariablDD4.IndexOf(target);
                                        CmbItm3 = index;
                                        if (index == 0)
                                        {
                                            tb.IsEnabled = false;
                                            tb.SelectedValue = 0;
                                        }
                                        if (index > 0)
                                        {
                                            tb.SelectedValue = _qstnvariablDD4[index];
                                            tb.IsEnabled = true;
                                            tb.Foreground = new SolidColorBrush(Colors.Black);
                                        }
                                    }
                                    else if (tb.Name == "Combo_Conditional_Item_4" && i == 4)
                                    {
                                        target = _qstnvariablDD5.Where(z => z.QuestionVariable == criteriaVariable).FirstOrDefault();
                                        index = target == null ? 0 : _qstnvariablDD5.IndexOf(target);
                                        CmbItm4 = index;
                                        if (index == 0)
                                        {
                                            tb.IsEnabled = false;
                                            tb.SelectedValue = 0;
                                        }
                                        if (index > 0)
                                        {
                                            tb.SelectedValue = _qstnvariablDD5[index];
                                            tb.IsEnabled = true;
                                            tb.Foreground = new SolidColorBrush(Colors.Black);
                                        }
                                    }
                                    else if (tb.Name == "Combo_Conditional_Item_5" && i == 5)
                                    {
                                        target = _qstnvariablDD6.Where(z => z.QuestionVariable == criteriaVariable).FirstOrDefault();
                                        index = target == null ? 0 : _qstnvariablDD6.IndexOf(target);
                                        CmbItm5 = index;
                                        if (index == 0)
                                        {
                                            tb.IsEnabled = false;
                                            tb.SelectedValue = 0;
                                        }
                                        if (index > 0)
                                        {
                                            tb.SelectedValue = _qstnvariablDD6[index];
                                            tb.IsEnabled = true;
                                            tb.Foreground = new SolidColorBrush(Colors.Black);
                                        }
                                    }
                                    else if (tb.Name == "Combo_Conditional_Operator_1" && i == 1)
                                    {
                                        if (val[i, 3] != null) { CmbOpItm1 = val[i, 3]; }
                                    }
                                    else if (tb.Name == "Combo_Conditional_Operator_2" && i == 2)
                                    {
                                        if (val[i, 3] != null)
                                        {
                                            CmbOpItm2 = val[i, 3];
                                        }
                                    }
                                    else if (tb.Name == "Combo_Conditional_Operator_3" && i == 3)
                                    {
                                        if (val[i, 3] != null) { CmbOpItm3 = val[i, 3]; }
                                    }
                                    else if (tb.Name == "Combo_Conditional_Operator_4" && i == 4)
                                    {
                                        if (val[i, 3] != null) { CmbOpItm4 = val[i, 3]; }
                                    }
                                    else if (tb.Name == "Combo_Conditional_Operator_5" && i == 5)
                                    {
                                        if (val[i, 3] != null) { CmbOpItm5 = val[i, 3]; }
                                    }
                                }
                                foreach (TextBox tb in FindControls.FindLogicalChildren<TextBox>(this.Page_Refine))
                                {
                                    if (tb.Name == "Combo_Conditional_Value_1" && i == 1)
                                    {
                                        if (val[i, 4] != null)
                                        {
                                            tb.Text = val[i, 4];
                                        }
                                    }
                                    else if (tb.Name == "Combo_Conditional_Value_2" && i == 2)
                                    {
                                        if (val[i, 4] != null)
                                        {
                                            tb.Text = val[i, 4];
                                        }
                                    }
                                    else if (tb.Name == "Combo_Conditional_Value_3" && i == 3)
                                    {
                                        if (val[i, 4] != null)
                                        {
                                            tb.Text = val[i, 4];
                                        }
                                    }
                                    else if (tb.Name == "Combo_Conditional_Value_4" && i == 4)
                                    {
                                        if (val[i, 4] != null)
                                        {
                                            tb.Text = val[i, 4];
                                        }
                                    }
                                    else if (tb.Name == "Combo_Conditional_Value_5" && i == 5)
                                    {
                                        if (val[i, 4] != null)
                                        {
                                            tb.Text = val[i, 4];
                                        }
                                    }

                                }
                                string instruction = val[i, 5].ToString();
                                if (instruction == QC4Common.Common.Constants.DP.InstructionAND || instruction == QC4Common.Common.Constants.DP.InstructionOR)
                                {
                                    foreach (RadioButton tb in FindControls.FindLogicalChildren<RadioButton>(this.Page_Refine))
                                    {
                                        if (i == 1 && (instruction == QC4Common.Common.Constants.DP.InstructionAND) && tb.Name == "Option_Conditional_And_1")
                                        {
                                            tb.IsChecked = true;
                                        }
                                        else if (i == 1 && (instruction == QC4Common.Common.Constants.DP.InstructionOR) && tb.Name == "Option_Conditional_Or_1")
                                        {
                                            tb.IsChecked = true;
                                        }
                                        else
                                        if (i == 2 && (instruction == QC4Common.Common.Constants.DP.InstructionAND) && tb.Name == "Option_Conditional_And_2")
                                        {
                                            tb.IsChecked = true;
                                        }
                                        else if (i == 2 && (instruction == QC4Common.Common.Constants.DP.InstructionOR) && tb.Name == "Option_Conditional_Or_2")
                                        {
                                            tb.IsChecked = true;
                                        }
                                        else
                                        if (i == 3 && (instruction == QC4Common.Common.Constants.DP.InstructionAND) && tb.Name == "Option_Conditional_And_3")
                                        {
                                            tb.IsChecked = true;
                                        }
                                        else if (i == 3 && (instruction == QC4Common.Common.Constants.DP.InstructionOR) && tb.Name == "Option_Conditional_Or_3")
                                        {
                                            tb.IsChecked = true;
                                        }

                                        if (i == 4 && (instruction == QC4Common.Common.Constants.DP.InstructionAND) && tb.Name == "Option_Conditional_And_4")
                                        {
                                            tb.IsChecked = true;
                                        }
                                        else if (i == 4 && (instruction == QC4Common.Common.Constants.DP.InstructionOR) && tb.Name == "Option_Conditional_Or_4")
                                        {
                                            tb.IsChecked = true;
                                        }
                                    }
                                }


                            }

                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                //  Mouse.OverrideCursor = null;
            }

        }
        BackgroundWorker backgroundWorker = new BackgroundWorker();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


            Thread thrd = new Thread(new ThreadStart(() =>
            {


                drpSANList.Add("N");
                drpSANList.Add("SA");
                LoadData();
                Dispatcher.Invoke(() =>
                {
                    combo_Answer_type.ItemsSource = drpSANList;
                    combo_Answer_type.SelectedItem = drpSANList.FirstOrDefault();

                    LoadFromSheet();
                });

            }


           ));

            thrd.Start();

        }
        string checkValue;
        private void LoadFromSheet()
        {
            // Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                var res = Util.ExcelUtil.GetWorkSheetBySheetName(Worksheet, Constants.SheetType.sh_Sheet2); //GetWorkSheetByCodeName(Worksheet, Constants.SheetCodeName.MultiVariate);
                Excel.Range crstart = res.Cells[17, 2];
                Excel.Range crlast = res.Cells[22, 235];
                Excel.Range rar = res.get_Range(crstart, crlast);
                var val = rar.Value;

                if (val[1, 1] != null)
                {

                    for (int i = 1; i <= val.GetLength(0); i++)
                    {

                        if (val[i, 1] != null)
                        {


                            string instruction = val[i, 5].ToString();



                            if (instruction == ProcessingMethod.PSM_ANALYSIS)
                            {
                                int colnum = 10;
                                ObservableCollection<psmQuestions> list = new ObservableCollection<psmQuestions>();
                                if (val[i, 6] == "0")
                                {
                                    Check_Refine_Condition.IsChecked = false;
                                }
                                else if (val[i, 6] == "1")
                                {
                                    Check_Refine_Condition.IsChecked = true;
                                }
                                if (val[i, 7] != null)
                                {
                                    if (val[i, 7] == "N")
                                    {
                                        combo_Answer_type.SelectedItem = drpSANList.FirstOrDefault();
                                    }
                                    else
                                    {
                                        combo_Answer_type.SelectedIndex = 1;
                                    }


                                }




                                if (val[i, 8] != null)
                                {
                                    if (SourceVariableList.Any(x => x.Variable == val[i, 8]))
                                    {
                                        if (val[i, 7] == QuestionType.N.ToString())
                                        {
                                            string variable = val[i, 8];
                                            var find = SourceNtype.FirstOrDefault(x => x.Variable == variable);
                                            txt_Expensive_variable_List.Clear();
                                            if (find != null)
                                            {
                                                txt_Expensive_variable_List.Add(find);
                                                SourceNtype.Remove(find);
                                                txt_Expensive_variable.Text = variable;
                                            }

                                        }
                                        else
                                        {
                                            string variable = val[i, 8];
                                            var find = SourceSAtype.FirstOrDefault(x => x.Variable == variable);
                                            txt_Expensive_variable_List.Clear();
                                            if (find != null)
                                            {
                                                txt_Expensive_variable_List.Add(find);
                                                SourceSAtype.Remove(find);
                                                txt_Expensive_variable.Text = variable;
                                            }
                                        }
                                    }

                                }
                                if (val[i, 9] != null)
                                {
                                    if (SourceVariableList.Any(x => x.Variable == val[i, 9]))
                                    {
                                        if (val[i, 7] == QuestionType.N.ToString())
                                        {
                                            string variable = val[i, 9];
                                            var find = SourceNtype.FirstOrDefault(x => x.Variable == variable);
                                            txt_Cheap_variable_List.Clear();
                                            if (find != null)
                                            {
                                                txt_Cheap_variable_List.Add(find);
                                                SourceNtype.Remove(find);
                                                txt_Cheap_variable.Text = variable;
                                            }
                                        }
                                        else
                                        {
                                            string variable = val[i, 9];
                                            var find = SourceSAtype.FirstOrDefault(x => x.Variable == variable);
                                            txt_Cheap_variable_List.Clear();
                                            if (find != null)
                                            {
                                                txt_Cheap_variable_List.Add(find);
                                                SourceSAtype.Remove(find);
                                                txt_Cheap_variable.Text = variable;
                                            }
                                        }
                                    }

                                }

                                if (val[i, 10] != null)
                                {
                                    if (SourceVariableList.Any(x => x.Variable == val[i, 10]))
                                    {
                                        if (val[i, 7] == QuestionType.N.ToString())
                                        {
                                            string variable = val[i, 10];
                                            var find = SourceNtype.FirstOrDefault(x => x.Variable == variable);
                                            txt_TooExpensive_variable_List.Clear();
                                            if (find != null)
                                            {
                                                txt_TooExpensive_variable_List.Add(find);
                                                SourceNtype.Remove(find);
                                                txt_TooExpensive_variable.Text = variable;
                                                // txt_TooExpensive_variable.Text = variable;
                                            }
                                        }
                                        else
                                        {
                                            string variable = val[i, 10];
                                            var find = SourceSAtype.FirstOrDefault(x => x.Variable == variable);
                                            txt_TooExpensive_variable_List.Clear();
                                            if (find != null)
                                            {
                                                txt_TooExpensive_variable_List.Add(find);
                                                SourceSAtype.Remove(find);
                                                txt_TooExpensive_variable.Text = variable;
                                            }
                                        }
                                    }

                                }
                                if (val[i, 11] != null)
                                {
                                    if (SourceVariableList.Any(x => x.Variable == val[i, 11]))
                                    {
                                        if (val[i, 7] == QuestionType.N.ToString())
                                        {
                                            string variable = val[i, 11];
                                            var find = SourceNtype.FirstOrDefault(x => x.Variable == variable);
                                            txt_Tocheap_variable_List.Clear();
                                            if (find != null)
                                            {
                                                txt_Tocheap_variable_List.Add(find);
                                                SourceNtype.Remove(find);
                                                txt_Tocheap_variable.Text = variable;
                                            }
                                        }
                                        else
                                        {
                                            string variable = val[i, 11];
                                            var find = SourceSAtype.FirstOrDefault(x => x.Variable == variable);
                                            txt_Tocheap_variable_List.Clear();
                                            if (find != null)
                                            {
                                                txt_Tocheap_variable_List.Add(find);
                                                SourceSAtype.Remove(find);
                                                txt_Tocheap_variable.Text = variable;
                                            }
                                        }
                                    }

                                }
                                if (val[i, 12] != null)
                                {
                                    txt_graph_displayrng.Text = val[i, 12];
                                }
                                if (val[i, 13] != null)
                                {
                                    txt_grah_range.Text = val[i, 13];
                                }
                                if (val[i, 14] != null)
                                {
                                    txt_scale_interval.Text = val[i, 14];
                                }
                                if (val[i, 15] != null)
                                {
                                    if (val[i, 15] == "0")
                                    {
                                        Check_Setting_Cross_Group.IsChecked = false;
                                    }
                                    else
                                    {
                                        Check_Setting_Cross_Group.IsChecked = true;
                                    }
                                }
                                if (val[i, 16] != null)
                                {
                                    if (val[i, 16] == "0")
                                    {
                                        set_valid_values_chbx.IsChecked = false;
                                    }
                                    else
                                    {
                                        set_valid_values_chbx.IsChecked = true;
                                    }
                                }
                                if (val[i, 17] != null)
                                {
                                    txt_Expensive_variable1.Text = val[i, 17];
                                }
                                if (val[i, 18] != null)
                                {
                                    txt_Cheap_variable1.Text = val[i, 18];
                                }
                                if (val[i, 19] != null)
                                {
                                    txt_tooExpensve_variable1.Text = val[i, 19];
                                }
                                if (val[i, 20] != null)
                                {
                                    txt_toocheap_variable1.Text = val[i, 20];
                                }

                                if (val[i, 21] != null)
                                {
                                    txtbx_expnsive.Text = val[i, 21];
                                }
                                if (val[i, 22] != null)
                                {
                                    txtbx_cheap.Text = val[i, 22];
                                }
                                if (val[i, 23] != null)
                                {
                                    txtbx_tooexpnsive.Text = val[i, 23];
                                }
                                if (val[i, 24] != null)
                                {
                                    txtbx_tooCheep.Text = val[i, 24];
                                }

                            }
                        }


                    }

                }
                else
                {
                    Check_Setting_Cross_Group.IsChecked = true;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                
            }

        }
        public void Savesettings()
        {
            
            List<QuestionSettings> questionSettings = new List<QuestionSettings>();
            questionSettings = dictionary.Values.ToList();
            QuestionSettings qs = new QuestionSettings();
            int rowCount = 1;
            string command = string.Empty;
            foreach (ComboBox tb in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
            {
                if (tb.Name == "Combo_Conditional_Item_2" && !string.IsNullOrEmpty(tb.Text))
                {
                    rowCount = 2;
                }
                else if (tb.Name == "Combo_Conditional_Item_3" && !string.IsNullOrEmpty(tb.Text))
                {
                    rowCount = 3;
                }
                else if (tb.Name == "Combo_Conditional_Item_4" && !string.IsNullOrEmpty(tb.Text))
                {
                    rowCount = 4;
                }
                else if (tb.Name == "Combo_Conditional_Item_5" && !string.IsNullOrEmpty(tb.Text))
                {
                    rowCount = 5;
                }
            }

            string[,] SelectedVariable = new string[rowCount, (235 - 3)];
            int param = 0;
            int colCount = 50;
            for (int i = 0; i < rowCount; i++)
            {
                param = 0;
                for (int j = 0; j < colCount; j++)
                {
                    switch (j)
                    {
                        case 0://onoff
                               // if (i == 0)
                            SelectedVariable[i, j] = LocalResource.CELL_ON;
                            break;



                        case 1://criteria variable

                            foreach (ComboBox tb in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
                            {
                                if (tb.Name == "Combo_Conditional_Item_1" && !string.IsNullOrEmpty(tb.Text) && i == 0)
                                {
                                    SelectedVariable[i, j] = tb.Text;
                                }
                                else if (tb.Name == "Combo_Conditional_Item_2" && !string.IsNullOrEmpty(tb.Text) && i == 1)
                                {
                                    SelectedVariable[i, j] = tb.Text;
                                }
                                else if (tb.Name == "Combo_Conditional_Item_3" && !string.IsNullOrEmpty(tb.Text) && i == 2)
                                {
                                    SelectedVariable[i, j] = tb.Text;
                                }
                                else if (tb.Name == "Combo_Conditional_Item_4" && !string.IsNullOrEmpty(tb.Text) && i == 3)
                                {
                                    SelectedVariable[i, j] = tb.Text;
                                }
                                else if (tb.Name == "Combo_Conditional_Item_5" && !string.IsNullOrEmpty(tb.Text) && i == 4)
                                {
                                    SelectedVariable[i, j] = tb.Text;
                                }
                            }

                            break;

                        case 2://criteria operator
                            foreach (ComboBox tb in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
                            {
                                if (tb.Name == "Combo_Conditional_Operator_1" && !string.IsNullOrEmpty(tb.Text) && i == 0)
                                {
                                    SelectedVariable[i, j] = tb.Text;
                                }
                                else if (tb.Name == "Combo_Conditional_Operator_2" && !string.IsNullOrEmpty(tb.Text) && i == 1)
                                {
                                    SelectedVariable[i, j] = tb.Text;
                                }
                                else if (tb.Name == "Combo_Conditional_Operator_3" && !string.IsNullOrEmpty(tb.Text) && i == 2)
                                {
                                    SelectedVariable[i, j] = tb.Text;
                                }
                                else if (tb.Name == "Combo_Conditional_Operator_4" && !string.IsNullOrEmpty(tb.Text) && i == 3)
                                {
                                    SelectedVariable[i, j] = tb.Text;
                                }
                                else if (tb.Name == "Combo_Conditional_Operator_5" && !string.IsNullOrEmpty(tb.Text) && i == 4)
                                {
                                    SelectedVariable[i, j] = tb.Text;
                                }
                            }

                            break;
                        case 3://criteria value
                            foreach (TextBox tb in FindControls.FindLogicalChildren<TextBox>(this.Page_Refine))
                            {
                                if (tb.Name == "Combo_Conditional_Value_1" && !string.IsNullOrEmpty(tb.Text) && i == 0)
                                {
                                    SelectedVariable[i, j] = tb.Text;
                                }
                                else if (tb.Name == "Combo_Conditional_Value_2" && !string.IsNullOrEmpty(tb.Text) && i == 1)
                                {
                                    SelectedVariable[i, j] = tb.Text;
                                }
                                else if (tb.Name == "Combo_Conditional_Value_3" && !string.IsNullOrEmpty(tb.Text) && i == 2)
                                {
                                    SelectedVariable[i, j] = tb.Text;
                                }
                                else if (tb.Name == "Combo_Conditional_Value_4" && !string.IsNullOrEmpty(tb.Text) && i == 3)
                                {
                                    SelectedVariable[i, j] = tb.Text;
                                }
                                else if (tb.Name == "Combo_Conditional_Value_5" && !string.IsNullOrEmpty(tb.Text) && i == 4)
                                {
                                    SelectedVariable[i, j] = tb.Text;
                                }

                            }
                            //
                            break;
                        case 4:
                            foreach (RadioButton tb in FindControls.FindLogicalChildren<RadioButton>(this.Page_Refine))
                            {
                                if (i == 0 && (tb.Name == "Option_Conditional_And_1" || tb.Name == "Option_Conditional_Or_1"))
                                {
                                    if (tb.IsChecked == true && tb.Name == "Option_Conditional_And_1")
                                    {
                                        SelectedVariable[i, j] = ExcelAddIn.Common.Constants.DP.InstructionAND;
                                        break;
                                    }
                                    else if (tb.IsChecked == true && tb.Name == "Option_Conditional_Or_1")
                                    {
                                        SelectedVariable[i, j] = ExcelAddIn.Common.Constants.DP.InstructionOR;
                                        break;
                                    }

                                }
                                else
                                     if (i == 1 && (tb.Name == "Option_Conditional_And_2" || tb.Name == "Option_Conditional_Or_2"))
                                {
                                    if (tb.IsChecked == true && tb.Name == "Option_Conditional_And_2")
                                    {
                                        SelectedVariable[i, j] = ExcelAddIn.Common.Constants.DP.InstructionAND;
                                        break;
                                    }
                                    else if (tb.IsChecked == true && tb.Name == "Option_Conditional_Or_2")
                                    {
                                        SelectedVariable[i, j] = ExcelAddIn.Common.Constants.DP.InstructionOR;
                                        break;
                                    }

                                }
                                else if (i == 2 && (tb.Name == "Option_Conditional_And_3" || tb.Name == "Option_Conditional_Or_3"))
                                {
                                    if (tb.IsChecked == true && tb.Name == "Option_Conditional_And_3")
                                    {
                                        SelectedVariable[i, j] = ExcelAddIn.Common.Constants.DP.InstructionAND;
                                        break;
                                    }
                                    else if (tb.IsChecked == true && tb.Name == "Option_Conditional_Or_3")
                                    {
                                        SelectedVariable[i, j] = ExcelAddIn.Common.Constants.DP.InstructionOR;
                                        break;
                                    }

                                }
                                else if (i == 3 && (tb.Name == "Option_Conditional_And_4" || tb.Name == "Option_Conditional_Or_4"))
                                {
                                    if (tb.IsChecked == true && tb.Name == "Option_Conditional_And_4")
                                    {
                                        SelectedVariable[i, j] = ExcelAddIn.Common.Constants.DP.InstructionAND;
                                        break;
                                    }
                                    else if (tb.IsChecked == true && tb.Name == "Option_Conditional_Or_4")
                                    {
                                        SelectedVariable[i, j] = ExcelAddIn.Common.Constants.DP.InstructionOR;
                                        break;
                                    }

                                }
                                else
                                {
                                    SelectedVariable[i, j] = ProcessingMethod.PSM_ANALYSIS;
                                }

                            }
                            break;
                        case 5://checkcross
                            if (Check_Refine_Condition.IsChecked == true)
                            {
                                SelectedVariable[i, j] = "1";
                            }
                            else
                            {
                                SelectedVariable[i, j] = "0";
                            }
                            break;
                        case 6://checkcross
                            if (!string.IsNullOrEmpty(SelectedVariable[i, 4]) && (SelectedVariable[i, 4] == ProcessingMethod.PSM_ANALYSIS))
                            {

                                SelectedVariable[i, j] = combo_Answer_type.SelectedValue.ToString();

                            }
                            break;
                        case 7://checkcross
                            if (!string.IsNullOrEmpty(SelectedVariable[i, 4]) && (SelectedVariable[i, 4] == ProcessingMethod.PSM_ANALYSIS))
                            {
                                if (!string.IsNullOrWhiteSpace(txt_Expensive_variable.Text))
                                {
                                    SelectedVariable[i, j] = txt_Expensive_variable.Text;
                                }
                            }
                            break;
                        case 8://no substitute variable
                            if (!string.IsNullOrEmpty(SelectedVariable[i, 4]) && (SelectedVariable[i, 4] == ProcessingMethod.PSM_ANALYSIS))
                            {

                                if (!string.IsNullOrWhiteSpace(txt_Cheap_variable.Text))
                                {
                                    SelectedVariable[i, j] = txt_Cheap_variable.Text;
                                }

                            }
                            break;
                        case 9:
                            if (!string.IsNullOrEmpty(SelectedVariable[i, 4]) && (SelectedVariable[i, 4] == ProcessingMethod.PSM_ANALYSIS))
                            {
                                if (!string.IsNullOrWhiteSpace(txt_TooExpensive_variable.Text))
                                {
                                    SelectedVariable[i, j] = txt_TooExpensive_variable.Text;
                                }
                            }
                            break;
                        case 10:
                            if (!string.IsNullOrEmpty(SelectedVariable[i, 4]) && (SelectedVariable[i, 4] == ProcessingMethod.PSM_ANALYSIS))
                            {
                                if (!string.IsNullOrWhiteSpace(txt_Tocheap_variable.Text))
                                {
                                    SelectedVariable[i, j] = txt_Tocheap_variable.Text;
                                }
                            }
                            break;
                        case 11:
                            if (!string.IsNullOrEmpty(SelectedVariable[i, 4]) && (SelectedVariable[i, 4] == ProcessingMethod.PSM_ANALYSIS))
                            {
                                if (!string.IsNullOrWhiteSpace(txt_graph_displayrng.Text))
                                {
                                    SelectedVariable[i, j] = txt_graph_displayrng.Text;
                                }
                            }
                            break;
                        case 12:
                            if (!string.IsNullOrEmpty(SelectedVariable[i, 4]) && (SelectedVariable[i, 4] == ProcessingMethod.PSM_ANALYSIS))
                            {
                                if (!string.IsNullOrWhiteSpace(txt_grah_range.Text))
                                {
                                    SelectedVariable[i, j] = txt_grah_range.Text;
                                }
                            }
                            break;
                        case 13:
                            if (!string.IsNullOrEmpty(SelectedVariable[i, 4]) && (SelectedVariable[i, 4] == ProcessingMethod.PSM_ANALYSIS))
                            {
                                if (!string.IsNullOrWhiteSpace(txt_scale_interval.Text))
                                {
                                    SelectedVariable[i, j] = txt_scale_interval.Text;
                                }
                            }
                            break;
                        case 14:
                            if (!string.IsNullOrEmpty(SelectedVariable[i, 4]) && (SelectedVariable[i, 4] == ProcessingMethod.PSM_ANALYSIS))
                            {
                                if (Check_Setting_Cross_Group.IsChecked == true)
                                {
                                    SelectedVariable[i, j] = "1";
                                }
                                else
                                {
                                    SelectedVariable[i, j] = "0";
                                }
                            }
                            break;
                        case 15:
                            if (!string.IsNullOrEmpty(SelectedVariable[i, 4]) && (SelectedVariable[i, 4] == ProcessingMethod.PSM_ANALYSIS))
                            {
                                if (set_valid_values_chbx.IsChecked == true)
                                {
                                    SelectedVariable[i, j] = "1";
                                }
                                else
                                {
                                    SelectedVariable[i, j] = "0";
                                }
                            }
                            break;
                        case 16:
                            if (!string.IsNullOrEmpty(SelectedVariable[i, 4]) && (SelectedVariable[i, 4] == ProcessingMethod.PSM_ANALYSIS))
                            {
                                if (!string.IsNullOrWhiteSpace(txt_Expensive_variable1.Text))
                                {
                                    SelectedVariable[i, j] = txt_Expensive_variable1.Text;
                                }

                            }
                            break;
                        case 17:
                            if (!string.IsNullOrEmpty(SelectedVariable[i, 4]) && (SelectedVariable[i, 4] == ProcessingMethod.PSM_ANALYSIS))
                            {
                                if (!string.IsNullOrWhiteSpace(txt_Cheap_variable1.Text))
                                {
                                    SelectedVariable[i, j] = txt_Cheap_variable1.Text;
                                }

                            }
                            break;
                        case 18:
                            if (!string.IsNullOrEmpty(SelectedVariable[i, 4]) && (SelectedVariable[i, 4] == ProcessingMethod.PSM_ANALYSIS))
                            {
                                if (!string.IsNullOrWhiteSpace(txt_tooExpensve_variable1.Text))
                                {
                                    SelectedVariable[i, j] = txt_tooExpensve_variable1.Text;
                                }

                            }
                            break;
                        case 19:
                            if (!string.IsNullOrEmpty(SelectedVariable[i, 4]) && (SelectedVariable[i, 4] == ProcessingMethod.PSM_ANALYSIS))
                            {
                                if (!string.IsNullOrWhiteSpace(txt_toocheap_variable1.Text))
                                {
                                    SelectedVariable[i, j] = txt_toocheap_variable1.Text;
                                }

                            }
                            break;
                        case 20:
                            if (!string.IsNullOrEmpty(SelectedVariable[i, 4]) && (SelectedVariable[i, 4] == ProcessingMethod.PSM_ANALYSIS))
                            {
                                if (!string.IsNullOrWhiteSpace(txtbx_expnsive.Text))
                                {
                                    SelectedVariable[i, j] = txtbx_expnsive.Text;
                                }

                            }
                            break;
                        case 21:
                            if (!string.IsNullOrEmpty(SelectedVariable[i, 4]) && (SelectedVariable[i, 4] == ProcessingMethod.PSM_ANALYSIS))
                            {
                                if (!string.IsNullOrWhiteSpace(txtbx_cheap.Text))
                                {
                                    SelectedVariable[i, j] = txtbx_cheap.Text;
                                }

                            }
                            break;
                        case 22:
                            if (!string.IsNullOrEmpty(SelectedVariable[i, 4]) && (SelectedVariable[i, 4] == ProcessingMethod.PSM_ANALYSIS))
                            {
                                if (!string.IsNullOrWhiteSpace(txtbx_tooexpnsive.Text))
                                {
                                    SelectedVariable[i, j] = txtbx_tooexpnsive.Text;
                                }

                            }
                            break;
                        case 23:
                            if (!string.IsNullOrEmpty(SelectedVariable[i, 4]) && (SelectedVariable[i, 4] == ProcessingMethod.PSM_ANALYSIS))
                            {
                                if (!string.IsNullOrWhiteSpace(txtbx_tooCheep.Text))
                                {
                                    SelectedVariable[i, j] = txtbx_tooCheep.Text;
                                }

                            }
                            break;

                        default:
                            break;

                    }
                }
            }

            sourceGetList.WriteFilterrSettings(Worksheet, SelectedVariable, 22, 17);
        }


        private void Lbl_Single_Left_Arrow_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_Expensive_variable.Text))
            {
                if (txt_Expensive_variable_List[0].AnswerType == QuestionType.N.ToString())
                {
                    SourceNtype.Add(txt_Expensive_variable_List[0]);
                    SourceNtype.Sort(k => k.OrderNo);
                }
                else
                {
                    SourceSAtype.Add(txt_Expensive_variable_List[0]);
                    SourceSAtype.Sort(k => k.OrderNo);
                }
                txt_Expensive_variable_List.Clear();
                txt_Expensive_variable.Text = string.Empty;
                txt_min_val1.Text = string.Empty;
                txt_max_val1.Text = string.Empty;
                txt_mean_val1.Text = string.Empty;
                txtbx1.Text = string.Empty;
                txtbx5.Text = string.Empty;
                txtbx9.Text = string.Empty;
                txtbx13.Text = string.Empty;
                txtbx17.Text = string.Empty;
                txtbx21.Text = string.Empty;
                txtbx25.Text = string.Empty;
            }
            if (Multi_Process_Grid != null)
            {
                ScrollViewer scrollViewer = GetVisualChild<ScrollViewer>(Multi_Process_Grid);
                if (scrollViewer != null)
                {
                    scrollViewer.ScrollToTop();
                }
            }
        }


        ObservableCollection<psmQuestions> txt_Cheap_variable_List = new ObservableCollection<psmQuestions>();
        private void Btn_rgt_arrw2_Click(object sender, RoutedEventArgs e)
        {
            if (Multi_Process_Grid.SelectedItem != null)
            {

                psmQuestions crs = (psmQuestions)Multi_Process_Grid.SelectedItem;
                if (crs.AnswerType == QuestionType.N.ToString())
                {
                    if (!string.IsNullOrEmpty(txt_Cheap_variable.Text))
                    {
                        if (txt_Cheap_variable_List[0].AnswerType == QuestionType.N.ToString()) { SourceNtype.Add(txt_Cheap_variable_List[0]); SourceNtype.Sort(k => k.OrderNo); }
                        else { SourceSAtype.Add(txt_Cheap_variable_List[0]); SourceSAtype.Sort(k => k.OrderNo); }
                        txt_Cheap_variable_List.Clear();
                        txt_Cheap_variable_List.Add(crs);
                        txt_Cheap_variable.Text = txt_Cheap_variable_List[0].Variable;
                        SourceNtype.Remove(crs);


                    }
                    else
                    {
                        txt_Cheap_variable_List.Clear();
                        txt_Cheap_variable_List.Add(crs);
                        txt_Cheap_variable.Text = txt_Cheap_variable_List[0].Variable;
                        SourceNtype.Remove(crs);
                    }

                }
                else
                {
                    if (!string.IsNullOrEmpty(txt_Cheap_variable.Text))
                    {
                        if (txt_Cheap_variable_List[0].AnswerType == QuestionType.N.ToString()) { SourceNtype.Add(txt_Cheap_variable_List[0]); SourceNtype.Sort(k => k.OrderNo); }
                        else { SourceSAtype.Add(txt_Cheap_variable_List[0]); SourceSAtype.Sort(k => k.OrderNo); }
                        txt_Cheap_variable_List.Clear();
                        txt_Cheap_variable_List.Add(crs);
                        txt_Cheap_variable.Text = txt_Cheap_variable_List[0].Variable;
                        SourceSAtype.Remove(crs);


                    }
                    else
                    {
                        txt_Cheap_variable_List.Clear();
                        txt_Cheap_variable_List.Add(crs);
                        txt_Cheap_variable.Text = txt_Cheap_variable_List[0].Variable;
                        SourceSAtype.Remove(crs);
                    }
                }
                if (Multi_Process_Grid != null)
                {
                    ScrollViewer scrollViewer = GetVisualChild<ScrollViewer>(Multi_Process_Grid);
                    if (scrollViewer != null)
                    {
                        scrollViewer.ScrollToTop();
                    }
                }

            }
            if (set_valid_values_chbx.IsChecked == true && isDataProcessed)
            {
                MessageDialog.Info(LocalResource.MULTI_PSM_UPDATE_VALID_VALUE_SETTING);
            }
            Multi_Process_Grid.Focus();

        }
        double min, max, avg, count;
        double meadian, mode, deviation, meanplus;
        QC4Common.Util.FormUtil frmutil = new QC4Common.Util.FormUtil();
        psmCalcus PsmCalcus = new psmCalcus();
        private void Txt_Expensive_variable_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (!string.IsNullOrEmpty(txt_Expensive_variable.Text))

            {

                if (!string.IsNullOrEmpty(txt_Expensive_variable.Text))

                {
                    if (Util.Definiotion.VariableDictionary.ContainsKey(txt_Expensive_variable.Text))
                    {
                        if (Util.Definiotion.VariableDictionary[txt_Expensive_variable.Text].QuestionFlag != "New")
                        {
                            isDataProcessed = true;

                            try
                            {

                                if (Util.Definiotion.VariableDictionary[txt_Expensive_variable.Text].AnswerType == "N")
                                {
                                    PsmCalcus.psmTabulate(Util.Definiotion.VariableDictionary[txt_Expensive_variable.Text], Worksheet, XlApp, ref avg, ref meadian, ref deviation, ref mode, ref meanplus, ref min, ref max);

                                    txt_min_val1.Text = min.ToString();
                                    txt_max_val1.Text = max.ToString();
                                    txt_mean_val1.Text = avg.ToString();

                                    txtbx1.Text = avg.ToString();
                                    txtbx5.Text = min.ToString();
                                    txtbx9.Text = max.ToString();

                                    txtbx13.Text = deviation.ToString();
                                    txtbx17.Text = meadian.ToString();
                                    if (!double.IsNaN(mode))
                                    {
                                        txtbx21.Text = mode.ToString();
                                    }

                                    txtbx25.Text = meanplus.ToString();
                                    txtbx_expnsive.Text = meanplus.ToString();

                                    txt_grah_range.Text = meanplus.ToString();
                                    txt_graph_displayrng.Text = "0";
                                    ScaleInterval((meanplus));
                                    txt_Expensive_variable1.Text = MinScaleInterval(avg, deviation).ToString();

                                }
                                else
                                {
                                    PsmCalcus.psmTabulate(Util.Definiotion.VariableDictionary[txt_Expensive_variable.Text], Worksheet, XlApp, ref avg, ref meadian, ref deviation, ref mode, ref meanplus, ref min, ref max);

                                    txt_min_val1.Text = min.ToString();
                                    txt_max_val1.Text = max.ToString();
                                    txt_mean_val1.Text = avg.ToString();

                                    txtbx1.Text = avg.ToString();
                                    txtbx5.Text = min.ToString();
                                    txtbx9.Text = max.ToString();

                                    txtbx13.Text = deviation.ToString();
                                    txtbx17.Text = meadian.ToString();
                                    if (!double.IsNaN(mode))
                                    {
                                        txtbx21.Text = mode.ToString();
                                    }
                                    txtbx25.Text = meanplus.ToString();
                                    txtbx_expnsive.Text = meanplus.ToString();

                                    txt_grah_range.Text = meanplus.ToString();
                                    txt_graph_displayrng.Text = "0";
                                    ScaleInterval((meanplus));
                                    txt_Expensive_variable1.Text = MinScaleInterval(avg, deviation).ToString();
                                }
                            }
                            catch (Exception ex) { }
                        }
                        else
                        {
                            isDataProcessed = frmutil.IsDataProcessed((Util.Definiotion.VariableDictionary[txt_Expensive_variable.Text].ItemId) == 0 ? "sample_id" : "q_" + (Util.Definiotion.VariableDictionary[txt_Expensive_variable.Text].ItemId).ToString(), Worksheet, true);

                            try
                            {

                                if (Util.Definiotion.VariableDictionary[txt_Expensive_variable.Text].AnswerType == "N")
                                {
                                    PsmCalcus.psmTabulate(Util.Definiotion.VariableDictionary[txt_Expensive_variable.Text], Worksheet, XlApp, ref avg, ref meadian, ref deviation, ref mode, ref meanplus, ref min, ref max);
                                    txt_min_val1.Text = min.ToString();
                                    txt_max_val1.Text = max.ToString();
                                    txt_mean_val1.Text = avg.ToString();

                                    txtbx1.Text = avg.ToString();
                                    txtbx5.Text = min.ToString();
                                    txtbx9.Text = max.ToString();

                                    txtbx13.Text = deviation.ToString();
                                    txtbx17.Text = meadian.ToString();
                                    if (!double.IsNaN(mode))
                                    {
                                        txtbx21.Text = mode.ToString();
                                    }
                                    txtbx25.Text = meanplus.ToString();
                                    txtbx_expnsive.Text = meanplus.ToString();

                                    txt_grah_range.Text = meanplus.ToString();
                                    txt_graph_displayrng.Text = "0";
                                    ScaleInterval((meanplus));
                                    txt_Expensive_variable1.Text = MinScaleInterval(avg, deviation).ToString();
                                }
                                else
                                {
                                    PsmCalcus.psmTabulate(Util.Definiotion.VariableDictionary[txt_Expensive_variable.Text], Worksheet, XlApp, ref avg, ref meadian, ref deviation, ref mode, ref meanplus, ref min, ref max);
                                    txt_min_val1.Text = min.ToString();
                                    txt_max_val1.Text = max.ToString();
                                    txt_mean_val1.Text = avg.ToString();

                                    txtbx1.Text = avg.ToString();
                                    txtbx5.Text = min.ToString();
                                    txtbx9.Text = max.ToString();

                                    txtbx13.Text = deviation.ToString();
                                    txtbx17.Text = meadian.ToString();
                                    if (!double.IsNaN(mode))
                                    {
                                        txtbx21.Text = mode.ToString();
                                    }


                                    txtbx25.Text = meanplus.ToString();
                                    txtbx_expnsive.Text = meanplus.ToString();

                                    txt_grah_range.Text = meanplus.ToString();
                                    txt_graph_displayrng.Text = "0";
                                    ScaleInterval((meanplus));
                                    txt_Expensive_variable1.Text = MinScaleInterval(avg, deviation).ToString();
                                }
                            }
                            catch (Exception ex)
                            {
                                txt_min_val1.Text = string.Empty;
                                txt_max_val1.Text = string.Empty;
                                txt_mean_val1.Text = string.Empty;

                                txtbx13.Text = string.Empty;
                                txtbx17.Text = string.Empty;
                                txtbx21.Text = string.Empty;
                                txtbx25.Text = string.Empty;


                                txtbx1.Text = string.Empty;
                                txtbx5.Text = string.Empty;
                                txtbx9.Text = string.Empty;

                            }

                        }
                    }



                }


                if (!InitialLoad && !string.IsNullOrEmpty(txt_Expensive_variable.Text) && isDataProcessed)
                {
                    if (set_valid_values_chbx.IsChecked == true)
                    {
                        MessageDialog.Info(LocalResource.MULTI_PSM_UPDATE_PRICE_RANGE);
                    }
                    else
                    {
                        MessageDialog.Info(LocalResource.MULTI_PSM_MSGBX_UPDATE_GRAPH_DISPLAY_RANGE);
                    }

                    

                }
                else
                {
                    InitialLoad = false;
                }

            }

            if (!string.IsNullOrEmpty(txt_Expensive_variable.Text) && !string.IsNullOrEmpty(txt_Cheap_variable.Text) && !string.IsNullOrEmpty(txt_TooExpensive_variable.Text) && !string.IsNullOrEmpty(txt_Tocheap_variable.Text))
            {
                btn_Execute.IsEnabled = true;
            }
            else { btn_Execute.IsEnabled = false; }

        }
        public double MinScaleInterval(double Average, double deviation)
        {
            double minScale = 0;
            minScale = Average - (deviation * 2);

            if (minScale < 1)
            {
                minScale = 0;
            }
            minScale = Worksheet.Application.Evaluate("=Rounddown(" + minScale + ",0)");
            return minScale;
        }
        public void ScaleInterval(double calc_num)
        {
            try
            {
                string wknum = "=ROUNDDOWN(" + calc_num + "/15,0)";
                double wk_Num = Worksheet.Application.Evaluate(wknum);

                wk_Num = ((wk_Num.ToString().Count() - 1) * (-1));
                string equa = "=ROUND((" + calc_num + "/15)," + wk_Num + " )";

                var evalres = Worksheet.Application.Evaluate(equa);
                if (evalres > 0)
                {
                    txt_scale_interval.Text = evalres.ToString();
                }
                else
                {
                    txt_scale_interval.Text = "1";
                }
            }
            catch (Exception ex)
            {

            }
        }
        ObservableCollection<psmQuestions> txt_TooExpensive_variable_List = new ObservableCollection<psmQuestions>();
        private void Btn_rgt_arrw3_Click(object sender, RoutedEventArgs e)
        {
            if (Multi_Process_Grid.SelectedItem != null)
            {
                psmQuestions crs = (psmQuestions)Multi_Process_Grid.SelectedItem;
                if (crs.AnswerType == QuestionType.N.ToString())
                {
                    if (!string.IsNullOrEmpty(txt_TooExpensive_variable.Text))
                    {
                        if (txt_TooExpensive_variable_List[0].AnswerType == QuestionType.N.ToString()) { SourceNtype.Add(txt_TooExpensive_variable_List[0]); SourceNtype.Sort(k => k.OrderNo); }
                        else { SourceSAtype.Add(txt_TooExpensive_variable_List[0]); SourceSAtype.Sort(k => k.OrderNo); }
                        txt_TooExpensive_variable_List.Clear();
                        txt_TooExpensive_variable_List.Add(crs);
                        txt_TooExpensive_variable.Text = txt_TooExpensive_variable_List[0].Variable;
                        SourceNtype.Remove(crs);


                    }
                    else
                    {
                        txt_TooExpensive_variable_List.Clear();
                        txt_TooExpensive_variable_List.Add(crs);
                        txt_TooExpensive_variable.Text = txt_TooExpensive_variable_List[0].Variable;
                        SourceNtype.Remove(crs);
                    }

                }
                else
                {
                    if (!string.IsNullOrEmpty(txt_TooExpensive_variable.Text))
                    {
                        if (txt_TooExpensive_variable_List[0].AnswerType == QuestionType.N.ToString()) { SourceNtype.Add(txt_TooExpensive_variable_List[0]); SourceNtype.Sort(k => k.OrderNo); }
                        else { SourceSAtype.Add(txt_TooExpensive_variable_List[0]); SourceSAtype.Sort(k => k.OrderNo); }
                        txt_TooExpensive_variable_List.Clear();
                        txt_TooExpensive_variable_List.Add(crs);
                        txt_TooExpensive_variable.Text = txt_TooExpensive_variable_List[0].Variable;
                        SourceSAtype.Remove(crs);


                    }
                    else
                    {
                        txt_TooExpensive_variable_List.Clear();
                        txt_TooExpensive_variable_List.Add(crs);
                        txt_TooExpensive_variable.Text = txt_TooExpensive_variable_List[0].Variable;
                        SourceSAtype.Remove(crs);
                    }
                }

                if (Multi_Process_Grid != null)
                {
                    ScrollViewer scrollViewer = GetVisualChild<ScrollViewer>(Multi_Process_Grid);
                    if (scrollViewer != null)
                    {
                        scrollViewer.ScrollToTop();
                    }
                }

            }
            if (set_valid_values_chbx.IsChecked == true && isDataProcessed)
            {
                MessageDialog.Info(LocalResource.MULTI_PSM_UPDATE_VALID_VALUE_SETTING);
            }
            Multi_Process_Grid.Focus();
        }

        private void Txt_Cheap_variable_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_Cheap_variable.Text))

            {

                if (Util.Definiotion.VariableDictionary.ContainsKey(txt_Cheap_variable.Text))
                {
                    if (Util.Definiotion.VariableDictionary[txt_Cheap_variable.Text].QuestionFlag != "New")
                    {
                        isDataProcessed = true;
                        try
                        {

                            if (Util.Definiotion.VariableDictionary[txt_Cheap_variable.Text].AnswerType == "N")
                            {
                                PsmCalcus.psmTabulate(Util.Definiotion.VariableDictionary[txt_Cheap_variable.Text], Worksheet, XlApp, ref avg, ref meadian, ref deviation, ref mode, ref meanplus, ref min, ref max);
                                txt_min_val2.Text = min.ToString();
                                txt_max_val2.Text = max.ToString();
                                txt_mean_val2.Text = avg.ToString();

                                txtbx2.Text = avg.ToString();
                                txtbx6.Text = min.ToString();
                                txtbx10.Text = max.ToString();

                                txtbx14.Text = deviation.ToString();
                                txtbx18.Text = meadian.ToString();
                                if (!double.IsNaN(mode))
                                {
                                    txtbx22.Text = mode.ToString();
                                }
                                txtbx26.Text = meanplus.ToString();
                                txtbx_cheap.Text = meanplus.ToString();

                                txt_Cheap_variable1.Text = MinScaleInterval(avg, deviation).ToString();
                            }
                            else
                            {
                                PsmCalcus.psmTabulate(Util.Definiotion.VariableDictionary[txt_Cheap_variable.Text], Worksheet, XlApp, ref avg, ref meadian, ref deviation, ref mode, ref meanplus, ref min, ref max);
                                txt_min_val2.Text = min.ToString();
                                txt_max_val2.Text = max.ToString();
                                txt_mean_val2.Text = avg.ToString();

                                txtbx2.Text = avg.ToString();
                                txtbx6.Text = min.ToString();
                                txtbx10.Text = max.ToString();

                                txtbx14.Text = deviation.ToString();
                                txtbx18.Text = meadian.ToString();
                                if (!double.IsNaN(mode))
                                {
                                    txtbx22.Text = mode.ToString();
                                }
                                txtbx26.Text = meanplus.ToString();
                                txtbx_cheap.Text = meanplus.ToString();

                                txt_Cheap_variable1.Text = MinScaleInterval(avg, deviation).ToString();
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else
                    {
                        isDataProcessed = frmutil.IsDataProcessed((Util.Definiotion.VariableDictionary[txt_Cheap_variable.Text].ItemId) == 0 ? "sample_id" : "q_" + (Util.Definiotion.VariableDictionary[txt_Cheap_variable.Text].ItemId).ToString(), Worksheet, true);
                        try
                        {

                            if (Util.Definiotion.VariableDictionary[txt_Cheap_variable.Text].AnswerType == "N")
                            {
                                PsmCalcus.psmTabulate(Util.Definiotion.VariableDictionary[txt_Cheap_variable.Text], Worksheet, XlApp, ref avg, ref meadian, ref deviation, ref mode, ref meanplus, ref min, ref max);
                                txt_min_val2.Text = min.ToString();
                                txt_max_val2.Text = max.ToString();
                                txt_mean_val2.Text = avg.ToString();

                                txtbx2.Text = avg.ToString();
                                txtbx6.Text = min.ToString();
                                txtbx10.Text = max.ToString();

                                txtbx14.Text = deviation.ToString();
                                txtbx18.Text = meadian.ToString();
                                if (!double.IsNaN(mode))
                                {
                                    txtbx22.Text = mode.ToString();
                                }
                                txtbx26.Text = meanplus.ToString();
                                txtbx_cheap.Text = meanplus.ToString();

                                txt_Cheap_variable1.Text = MinScaleInterval(avg, deviation).ToString();
                            }
                            else
                            {
                                PsmCalcus.psmTabulate(Util.Definiotion.VariableDictionary[txt_Cheap_variable.Text], Worksheet, XlApp, ref avg, ref meadian, ref deviation, ref mode, ref meanplus, ref min, ref max);
                                txt_min_val2.Text = min.ToString();
                                txt_max_val2.Text = max.ToString();
                                txt_mean_val2.Text = avg.ToString();

                                txtbx2.Text = avg.ToString();
                                txtbx6.Text = min.ToString();
                                txtbx10.Text = max.ToString();

                                txtbx14.Text = deviation.ToString();
                                txtbx18.Text = meadian.ToString();
                                if (!double.IsNaN(mode))
                                {
                                    txtbx22.Text = mode.ToString();
                                }


                                txtbx26.Text = meanplus.ToString();
                                txtbx_cheap.Text = meanplus.ToString();

                                txt_Cheap_variable1.Text = MinScaleInterval(avg, deviation).ToString();
                            }
                        }
                        catch (Exception ex)
                        {

                            txt_min_val2.Text = string.Empty;
                            txt_max_val2.Text = string.Empty;
                            txt_mean_val2.Text = string.Empty;

                            txtbx14.Text = string.Empty;
                            txtbx18.Text = string.Empty;
                            txtbx22.Text = string.Empty;
                            txtbx26.Text = string.Empty;

                            txtbx2.Text = string.Empty;
                            txtbx6.Text = string.Empty;
                            txtbx10.Text = string.Empty;
                        }
                    }



                }
            }

            if (!string.IsNullOrEmpty(txt_Expensive_variable.Text) && !string.IsNullOrEmpty(txt_Cheap_variable.Text) && !string.IsNullOrEmpty(txt_TooExpensive_variable.Text) && !string.IsNullOrEmpty(txt_Tocheap_variable.Text))
            {
                btn_Execute.IsEnabled = true;
            }
            else { btn_Execute.IsEnabled = false; }

        }
        bool isDataProcessed = true;
        private void Txt_TooExpensive_variable_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_TooExpensive_variable.Text))

            {

                if (Util.Definiotion.VariableDictionary.ContainsKey(txt_TooExpensive_variable.Text))
                {
                    if (Util.Definiotion.VariableDictionary[txt_TooExpensive_variable.Text].QuestionFlag != "New")
                    {
                        isDataProcessed = true;

                        if (Util.Definiotion.VariableDictionary[txt_TooExpensive_variable.Text].AnswerType == "N")
                        {
                            PsmCalcus.psmTabulate(Util.Definiotion.VariableDictionary[txt_TooExpensive_variable.Text], Worksheet, XlApp, ref avg, ref meadian, ref deviation, ref mode, ref meanplus, ref min, ref max);
                            txt_min_val3.Text = min.ToString();
                            txt_max_val3.Text = max.ToString();
                            txt_mean_val3.Text = avg.ToString();

                            txtbx3.Text = avg.ToString();
                            txtbx7.Text = min.ToString();
                            txtbx11.Text = max.ToString();

                            txtbx15.Text = deviation.ToString();
                            txtbx19.Text = meadian.ToString();
                            if (!double.IsNaN(mode))
                            {
                                txtbx23.Text = mode.ToString();

                            }

                            txtbx27.Text = meanplus.ToString();
                            txtbx_tooexpnsive.Text = meanplus.ToString();
                            txt_tooExpensve_variable1.Text = MinScaleInterval(avg, deviation).ToString();
                        }
                        else
                        {
                            PsmCalcus.psmTabulate(Util.Definiotion.VariableDictionary[txt_TooExpensive_variable.Text], Worksheet, XlApp, ref avg, ref meadian, ref deviation, ref mode, ref meanplus, ref min, ref max);
                            txt_min_val3.Text = min.ToString();
                            txt_max_val3.Text = max.ToString();
                            txt_mean_val3.Text = avg.ToString();

                            txtbx3.Text = avg.ToString();
                            txtbx7.Text = min.ToString();
                            txtbx11.Text = max.ToString();

                            txtbx15.Text = deviation.ToString();
                            txtbx19.Text = meadian.ToString();
                            if (!double.IsNaN(mode))
                            {
                                txtbx23.Text = mode.ToString();
                            }

                            txtbx27.Text = meanplus.ToString();
                            txtbx_tooexpnsive.Text = meanplus.ToString();
                            txt_tooExpensve_variable1.Text = "0";
                        }
                    }
                    else
                    {
                        isDataProcessed = frmutil.IsDataProcessed((Util.Definiotion.VariableDictionary[txt_TooExpensive_variable.Text].ItemId) == 0 ? "sample_id" : "q_" + (Util.Definiotion.VariableDictionary[txt_TooExpensive_variable.Text].ItemId).ToString(), Worksheet, true);

                        try
                        {

                            if (Util.Definiotion.VariableDictionary[txt_TooExpensive_variable.Text].AnswerType == "N")
                            {
                                PsmCalcus.psmTabulate(Util.Definiotion.VariableDictionary[txt_TooExpensive_variable.Text], Worksheet, XlApp, ref avg, ref meadian, ref deviation, ref mode, ref meanplus, ref min, ref max);
                                txt_min_val3.Text = min.ToString();
                                txt_max_val3.Text = max.ToString();
                                txt_mean_val3.Text = avg.ToString();

                                txtbx3.Text = avg.ToString();
                                txtbx7.Text = min.ToString();
                                txtbx11.Text = max.ToString();

                                txtbx15.Text = deviation.ToString();
                                txtbx19.Text = meadian.ToString();
                                if (!double.IsNaN(mode))
                                {
                                    txtbx23.Text = mode.ToString();
                                }


                                txtbx27.Text = meanplus.ToString();
                                txtbx_tooexpnsive.Text = meanplus.ToString();
                                txt_tooExpensve_variable1.Text = MinScaleInterval(avg, deviation).ToString();
                            }
                            else
                            {
                                PsmCalcus.psmTabulate(Util.Definiotion.VariableDictionary[txt_TooExpensive_variable.Text], Worksheet, XlApp, ref avg, ref meadian, ref deviation, ref mode, ref meanplus, ref min, ref max);
                                txt_min_val3.Text = min.ToString();
                                txt_max_val3.Text = max.ToString();
                                txt_mean_val3.Text = avg.ToString();

                                txtbx3.Text = avg.ToString();
                                txtbx7.Text = min.ToString();
                                txtbx11.Text = max.ToString();

                                txtbx15.Text = deviation.ToString();
                                txtbx19.Text = meadian.ToString();
                                if (!double.IsNaN(mode))
                                {
                                    txtbx23.Text = mode.ToString();

                                }

                                txtbx27.Text = meanplus.ToString();
                                txtbx_tooexpnsive.Text = meanplus.ToString();
                                txt_tooExpensve_variable1.Text = MinScaleInterval(avg, deviation).ToString();
                            }
                        }
                        catch (Exception ex)
                        {

                            txt_min_val3.Text = string.Empty;
                            txt_max_val3.Text = string.Empty;
                            txt_mean_val3.Text = string.Empty;

                            txtbx15.Text = string.Empty;
                            txtbx19.Text = string.Empty;
                            txtbx23.Text = string.Empty;
                            txtbx27.Text = string.Empty;

                            txtbx3.Text = string.Empty;
                            txtbx7.Text = string.Empty;
                            txtbx11.Text = string.Empty;
                        }
                    }



                }


            }
            if (!string.IsNullOrEmpty(txt_Expensive_variable.Text) && !string.IsNullOrEmpty(txt_Cheap_variable.Text) && !string.IsNullOrEmpty(txt_TooExpensive_variable.Text) && !string.IsNullOrEmpty(txt_Tocheap_variable.Text))
            {
                btn_Execute.IsEnabled = true;
            }
            else { btn_Execute.IsEnabled = false; }

        }

        private void Txt_Tocheap_variable_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_Tocheap_variable.Text))

            {

                if (Util.Definiotion.VariableDictionary.ContainsKey(txt_Tocheap_variable.Text))
                {
                    if (Util.Definiotion.VariableDictionary[txt_Tocheap_variable.Text].QuestionFlag != "New")
                    {
                        isDataProcessed = true;

                        if (Util.Definiotion.VariableDictionary[txt_Tocheap_variable.Text].AnswerType == "N")
                        {
                            PsmCalcus.psmTabulate(Util.Definiotion.VariableDictionary[txt_Tocheap_variable.Text], Worksheet, XlApp, ref avg, ref meadian, ref deviation, ref mode, ref meanplus, ref min, ref max);
                            txt_min_val4.Text = min.ToString();
                            txt_max_val4.Text = max.ToString();
                            txt_mean_val4.Text = avg.ToString();

                            txtbx4.Text = avg.ToString();
                            txtbx8.Text = min.ToString();
                            txtbx12.Text = max.ToString();

                            txtbx16.Text = deviation.ToString();
                            txtbx20.Text = meadian.ToString();
                            if (!double.IsNaN(mode))
                            {
                                txtbx24.Text = mode.ToString();
                            }
                            txtbx28.Text = meanplus.ToString();
                            txtbx_tooCheep.Text = meanplus.ToString();
                            txt_toocheap_variable1.Text = MinScaleInterval(avg, deviation).ToString();
                        }
                        else
                        {
                            PsmCalcus.psmTabulate(Util.Definiotion.VariableDictionary[txt_Tocheap_variable.Text], Worksheet, XlApp, ref avg, ref meadian, ref deviation, ref mode, ref meanplus, ref min, ref max);
                            txt_min_val4.Text = min.ToString();
                            txt_max_val4.Text = max.ToString();
                            txt_mean_val4.Text = avg.ToString();

                            txtbx4.Text = avg.ToString();
                            txtbx8.Text = min.ToString();
                            txtbx12.Text = max.ToString();

                            txtbx16.Text = deviation.ToString();
                            txtbx20.Text = meadian.ToString();
                            if (!double.IsNaN(mode))
                            {
                                txtbx24.Text = mode.ToString();
                            }

                            txtbx28.Text = meanplus.ToString();
                            txtbx_tooCheep.Text = meanplus.ToString();
                            txt_toocheap_variable1.Text = MinScaleInterval(avg, deviation).ToString();
                        }
                    }
                    else
                    {
                        isDataProcessed = frmutil.IsDataProcessed((Util.Definiotion.VariableDictionary[txt_Tocheap_variable.Text].ItemId) == 0 ? "sample_id" : "q_" + (Util.Definiotion.VariableDictionary[txt_Tocheap_variable.Text].ItemId).ToString(), Worksheet, true);

                        try
                        {

                            if (Util.Definiotion.VariableDictionary[txt_Tocheap_variable.Text].AnswerType == "N")
                            {
                                PsmCalcus.psmTabulate(Util.Definiotion.VariableDictionary[txt_Tocheap_variable.Text], Worksheet, XlApp, ref avg, ref meadian, ref deviation, ref mode, ref meanplus, ref min, ref max);
                                txt_min_val4.Text = min.ToString();
                                txt_max_val4.Text = max.ToString();
                                txt_mean_val4.Text = avg.ToString();

                                txtbx4.Text = avg.ToString();
                                txtbx8.Text = min.ToString();
                                txtbx12.Text = max.ToString();

                                txtbx16.Text = deviation.ToString();
                                txtbx20.Text = meadian.ToString();
                                if (!double.IsNaN(mode))
                                {
                                    txtbx24.Text = mode.ToString();

                                }

                                txtbx28.Text = meanplus.ToString();
                                txtbx_tooCheep.Text = meanplus.ToString();
                                txt_toocheap_variable1.Text = MinScaleInterval(avg, deviation).ToString();
                            }
                            else
                            {
                                PsmCalcus.psmTabulate(Util.Definiotion.VariableDictionary[txt_Tocheap_variable.Text], Worksheet, XlApp, ref avg, ref meadian, ref deviation, ref mode, ref meanplus, ref min, ref max);
                                txt_min_val4.Text = min.ToString();
                                txt_max_val4.Text = max.ToString();
                                txt_mean_val4.Text = avg.ToString();

                                txtbx4.Text = avg.ToString();
                                txtbx8.Text = min.ToString();
                                txtbx12.Text = max.ToString();

                                txtbx16.Text = deviation.ToString();
                                txtbx20.Text = meadian.ToString();
                                if (!double.IsNaN(mode))
                                {
                                    txtbx24.Text = mode.ToString();

                                }

                                txtbx28.Text = meanplus.ToString();
                                txtbx_tooCheep.Text = meanplus.ToString();
                                txt_toocheap_variable1.Text = MinScaleInterval(avg, deviation).ToString();
                            }
                        }
                        catch (Exception ex)
                        {

                            txt_min_val4.Text = string.Empty;
                            txt_max_val4.Text = string.Empty;
                            txt_mean_val4.Text = string.Empty;

                            txtbx16.Text = string.Empty;
                            txtbx20.Text = string.Empty;
                            txtbx24.Text = string.Empty;
                            txtbx28.Text = string.Empty;

                            txtbx4.Text = string.Empty;
                            txtbx8.Text = string.Empty;
                            txtbx12.Text = string.Empty;
                        }
                    }



                }


            }

            if (!string.IsNullOrEmpty(txt_Expensive_variable.Text) && !string.IsNullOrEmpty(txt_Cheap_variable.Text) && !string.IsNullOrEmpty(txt_TooExpensive_variable.Text) && !string.IsNullOrEmpty(txt_Tocheap_variable.Text))
            {
                btn_Execute.IsEnabled = true;
            }
            else { btn_Execute.IsEnabled = false; }

        }

        private void Lbl_Single_Left_Arrow2_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_Cheap_variable.Text))
            {

                if (txt_Cheap_variable_List[0].AnswerType == QuestionType.N.ToString())
                {
                    SourceNtype.Add(txt_Cheap_variable_List[0]);
                    SourceNtype.Sort(k => k.OrderNo);
                }
                else
                {
                    SourceSAtype.Add(txt_Cheap_variable_List[0]);
                    SourceSAtype.Sort(k => k.OrderNo);
                }
                txt_Cheap_variable_List.Clear();
                txt_Cheap_variable.Text = string.Empty;
                txt_min_val2.Text = string.Empty;
                txt_max_val2.Text = string.Empty;
                txt_mean_val2.Text = string.Empty;
                txtbx2.Text = string.Empty;
                txtbx6.Text = string.Empty;
                txtbx10.Text = string.Empty;
                txtbx14.Text = string.Empty;
                txtbx18.Text = string.Empty;
                txtbx22.Text = string.Empty;
                txtbx26.Text = string.Empty;

                if (Multi_Process_Grid != null)
                {
                    ScrollViewer scrollViewer = GetVisualChild<ScrollViewer>(Multi_Process_Grid);
                    if (scrollViewer != null)
                    {
                        scrollViewer.ScrollToTop();
                    }
                }
            }

        }

        private void Lbl_Single_Left_Arrow3_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_TooExpensive_variable.Text))
            {
                if (txt_TooExpensive_variable_List[0].AnswerType == QuestionType.N.ToString())
                {
                    SourceNtype.Add(txt_TooExpensive_variable_List[0]);
                    SourceNtype.Sort(k => k.OrderNo);
                }
                else
                {
                    SourceSAtype.Add(txt_TooExpensive_variable_List[0]);
                    SourceSAtype.Sort(k => k.OrderNo);
                }
                txt_TooExpensive_variable_List.Clear();
                txt_TooExpensive_variable.Text = string.Empty;
                txt_min_val3.Text = string.Empty;
                txt_max_val3.Text = string.Empty;
                txt_mean_val3.Text = string.Empty;
                txtbx3.Text = string.Empty;
                txtbx7.Text = string.Empty;
                txtbx11.Text = string.Empty;
                txtbx15.Text = string.Empty;
                txtbx19.Text = string.Empty;
                txtbx23.Text = string.Empty;
                txtbx27.Text = string.Empty;
                if (Multi_Process_Grid != null)
                {
                    ScrollViewer scrollViewer = GetVisualChild<ScrollViewer>(Multi_Process_Grid);
                    if (scrollViewer != null)
                    {
                        scrollViewer.ScrollToTop();
                    }
                }
            }
        }

        private void Lbl_Single_Left_Arrow4_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_Tocheap_variable.Text))
            {
                if (txt_Tocheap_variable_List[0].AnswerType == QuestionType.N.ToString())
                {
                    SourceNtype.Add(txt_Tocheap_variable_List[0]);
                    SourceNtype.Sort(k => k.OrderNo);
                }
                else
                {
                    SourceSAtype.Add(txt_Tocheap_variable_List[0]);
                    SourceSAtype.Sort(k => k.OrderNo);
                }
                txt_Tocheap_variable_List.Clear();
                txt_Tocheap_variable.Text = string.Empty;
                txt_min_val4.Text = string.Empty;
                txt_max_val4.Text = string.Empty;
                txt_mean_val4.Text = string.Empty;
                txtbx4.Text = string.Empty;
                txtbx8.Text = string.Empty;
                txtbx12.Text = string.Empty;
                txtbx16.Text = string.Empty;
                txtbx20.Text = string.Empty;
                txtbx24.Text = string.Empty;
                txtbx28.Text = string.Empty;
                if (Multi_Process_Grid != null)
                {
                    ScrollViewer scrollViewer = GetVisualChild<ScrollViewer>(Multi_Process_Grid);
                    if (scrollViewer != null)
                    {
                        scrollViewer.ScrollToTop();
                    }
                }
            }
        }

        ObservableCollection<psmQuestions> txt_Tocheap_variable_List = new ObservableCollection<psmQuestions>();

        private void Set_valid_values_chbx_Checked(object sender, RoutedEventArgs e)
        {
            if (set_valid_values_chbx.IsChecked == false)
            {
                txt1.IsEnabled = false;
                txt1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txt2.IsEnabled = false;
                txt2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txt3.IsEnabled = false;
                txt3.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txt4.IsEnabled = false;
                txt4.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txt5.IsEnabled = false;
                txt5.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txt6.IsEnabled = false;
                txt6.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txt7.IsEnabled = false;
                txt7.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txt8.IsEnabled = false;
                txt8.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txt9.IsEnabled = false;
                txt9.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txt10.IsEnabled = false;
                txt10.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txt11.IsEnabled = false;
                txt11.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txt12.IsEnabled = false;
                txt12.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txt13.IsEnabled = false;
                txt13.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txt14.IsEnabled = false;
                txt14.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txt15.IsEnabled = false;
                txt15.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txt16.IsEnabled = false;
                txt16.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txt17.IsEnabled = false;
                txt17.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txt18.IsEnabled = false;
                txt18.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txt19.IsEnabled = false;
                txt19.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txt20.IsEnabled = false;
                txt20.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));


                txt_Expensive_variable1.IsEnabled = false;
                txt_Expensive_variable1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));

                txt_Cheap_variable1.IsEnabled = false;
                txt_Cheap_variable1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));

                txt_tooExpensve_variable1.IsEnabled = false;
                txt_tooExpensve_variable1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));

                txt_toocheap_variable1.IsEnabled = false;
                txt_toocheap_variable1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));

                txtbx_expnsive.IsEnabled = false;
                txtbx_expnsive.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));

                txtbx_cheap.IsEnabled = false;
                txtbx_cheap.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));

                txtbx_tooexpnsive.IsEnabled = false;
                txtbx_tooexpnsive.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));

                txtbx_tooCheep.IsEnabled = false;
                txtbx_tooCheep.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));

                txtbx1.IsEnabled = false;
                txtbx1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txtbx2.IsEnabled = false;
                txtbx2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txtbx3.IsEnabled = false;
                txtbx3.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txtbx4.IsEnabled = false;
                txtbx4.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txtbx5.IsEnabled = false;
                txtbx5.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txtbx6.IsEnabled = false;
                txtbx6.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txtbx7.IsEnabled = false;
                txtbx7.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txtbx8.IsEnabled = false;
                txtbx8.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txtbx9.IsEnabled = false;
                txtbx9.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txtbx10.IsEnabled = false;
                txtbx10.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txtbx11.IsEnabled = false;
                txtbx11.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txtbx12.IsEnabled = false;
                txtbx12.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txtbx13.IsEnabled = false;
                txtbx13.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txtbx14.IsEnabled = false;
                txtbx14.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txtbx15.IsEnabled = false;
                txtbx15.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txtbx16.IsEnabled = false;
                txtbx16.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txtbx17.IsEnabled = false;
                txtbx17.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txtbx18.IsEnabled = false;
                txtbx18.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txtbx19.IsEnabled = false;
                txtbx19.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txtbx20.IsEnabled = false;
                txtbx20.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txtbx21.IsEnabled = false;
                txtbx21.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txtbx22.IsEnabled = false;
                txtbx22.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txtbx23.IsEnabled = false;
                txtbx23.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txtbx24.IsEnabled = false;
                txtbx24.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txtbx25.IsEnabled = false;
                txtbx25.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txtbx26.IsEnabled = false;
                txtbx26.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txtbx27.IsEnabled = false;
                txtbx27.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txtbx28.IsEnabled = false;
                txtbx28.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));






            }
            else
            {

                txtbx_tooexpnsive.IsEnabled = true;
                txtbx_tooexpnsive.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));

                txtbx_tooCheep.IsEnabled = true;
                txtbx_tooCheep.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));

                txtbx_cheap.IsEnabled = true;
                txtbx_cheap.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));

                txtbx_expnsive.IsEnabled = true;
                txtbx_expnsive.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));

                txt_Expensive_variable1.IsEnabled = true;
                txt_Expensive_variable1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));

                txt_Cheap_variable1.IsEnabled = true;
                txt_Cheap_variable1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));

                txt_tooExpensve_variable1.IsEnabled = true;
                txt_tooExpensve_variable1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));

                txt_toocheap_variable1.IsEnabled = true;
                txt_toocheap_variable1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));


                txtbx1.IsEnabled = true;
                txtbx1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txtbx2.IsEnabled = true;
                txtbx2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txtbx3.IsEnabled = true;
                txtbx3.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txtbx4.IsEnabled = true;
                txtbx4.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txtbx5.IsEnabled = true;
                txtbx5.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txtbx6.IsEnabled = true;
                txtbx6.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txtbx7.IsEnabled = true;
                txtbx7.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txtbx8.IsEnabled = true;
                txtbx8.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txtbx9.IsEnabled = true;
                txtbx9.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txtbx10.IsEnabled = true;
                txtbx10.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txtbx11.IsEnabled = true;
                txtbx11.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txtbx12.IsEnabled = true;
                txtbx12.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txtbx13.IsEnabled = true;
                txtbx13.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txtbx14.IsEnabled = true;
                txtbx14.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txtbx15.IsEnabled = true;
                txtbx15.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txtbx16.IsEnabled = true;
                txtbx16.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txtbx17.IsEnabled = true;
                txtbx17.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txtbx18.IsEnabled = true;
                txtbx18.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txtbx19.IsEnabled = true;
                txtbx19.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txtbx20.IsEnabled = true;
                txtbx20.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txtbx21.IsEnabled = true;
                txtbx21.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txtbx22.IsEnabled = true;
                txtbx22.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txtbx23.IsEnabled = true;
                txtbx23.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txtbx24.IsEnabled = true;
                txtbx24.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txtbx25.IsEnabled = true;
                txtbx25.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txtbx26.IsEnabled = true;
                txtbx26.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txtbx27.IsEnabled = true;
                txtbx27.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txtbx28.IsEnabled = true;
                txtbx28.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));


                txt1.IsEnabled = true;
                txt1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txt2.IsEnabled = true;
                txt2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txt3.IsEnabled = true;
                txt3.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txt4.IsEnabled = true;
                txt4.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txt5.IsEnabled = true;
                txt5.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txt6.IsEnabled = true;
                txt6.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txt7.IsEnabled = true;
                txt7.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txt8.IsEnabled = true;
                txt8.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txt9.IsEnabled = true;
                txt9.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txt10.IsEnabled = true;
                txt10.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txt11.IsEnabled = true;
                txt11.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txt12.IsEnabled = true;
                txt12.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txt13.IsEnabled = true;
                txt13.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txt14.IsEnabled = true;
                txt14.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txt15.IsEnabled = true;
                txt15.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txt16.IsEnabled = true;
                txt16.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txt17.IsEnabled = true;
                txt17.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txt18.IsEnabled = true;
                txt18.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txt19.IsEnabled = true;
                txt19.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txt20.IsEnabled = true;
                txt20.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));

            }
        }


        private void Backgroud_color_txtbx_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Controls.TextBox txtbox = (System.Windows.Controls.TextBox)sender;
            System.Windows.Forms.ColorDialog dlg = new System.Windows.Forms.ColorDialog();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtbox.Background = new SolidColorBrush(Color.FromArgb(dlg.Color.A, dlg.Color.R, dlg.Color.G, dlg.Color.B));
            }
        }

        private void Btn_rgt_arrw4_Click(object sender, RoutedEventArgs e)
        {

            if (Multi_Process_Grid.SelectedItem != null)
            {
                psmQuestions crs = (psmQuestions)Multi_Process_Grid.SelectedItem;
                if (crs.AnswerType == QuestionType.N.ToString())
                {
                    if (!string.IsNullOrEmpty(txt_Tocheap_variable.Text))
                    {
                        if (txt_Tocheap_variable_List[0].AnswerType == QuestionType.N.ToString()) { SourceNtype.Add(txt_Tocheap_variable_List[0]); SourceNtype.Sort(k => k.OrderNo); }
                        else { SourceSAtype.Add(txt_Tocheap_variable_List[0]); SourceSAtype.Sort(k => k.OrderNo); }
                        txt_Tocheap_variable_List.Clear();
                        txt_Tocheap_variable_List.Add(crs);
                        txt_Tocheap_variable.Text = txt_Tocheap_variable_List[0].Variable;
                        SourceNtype.Remove(crs);


                    }
                    else
                    {
                        txt_Tocheap_variable_List.Clear();
                        txt_Tocheap_variable_List.Add(crs);
                        txt_Tocheap_variable.Text = txt_Tocheap_variable_List[0].Variable;
                        SourceNtype.Remove(crs);
                    }

                }
                else
                {
                    if (!string.IsNullOrEmpty(txt_Tocheap_variable.Text))
                    {
                        if (txt_Tocheap_variable_List[0].AnswerType == QuestionType.N.ToString()) { SourceNtype.Add(txt_Tocheap_variable_List[0]); SourceNtype.Sort(k => k.OrderNo); }
                        else { SourceSAtype.Add(txt_Tocheap_variable_List[0]); SourceSAtype.Sort(k => k.OrderNo); }
                        txt_Tocheap_variable_List.Clear();
                        txt_Tocheap_variable_List.Add(crs);
                        txt_Tocheap_variable.Text = txt_Tocheap_variable_List[0].Variable;
                        SourceSAtype.Remove(crs);


                    }
                    else
                    {
                        txt_Tocheap_variable_List.Clear();
                        txt_Tocheap_variable_List.Add(crs);
                        txt_Tocheap_variable.Text = txt_Tocheap_variable_List[0].Variable;
                        SourceSAtype.Remove(crs);
                    }
                }

                if (Multi_Process_Grid != null)
                {
                    ScrollViewer scrollViewer = GetVisualChild<ScrollViewer>(Multi_Process_Grid);
                    if (scrollViewer != null)
                    {
                        scrollViewer.ScrollToTop();
                    }
                }
            }
            if (set_valid_values_chbx.IsChecked == true && isDataProcessed)
            {
                MessageDialog.Info(LocalResource.MULTI_PSM_UPDATE_VALID_VALUE_SETTING);
            }
            Multi_Process_Grid.Focus();

        }

        private void TabControl_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl) //if this event fired from TabControl then enter
            {
                if (Page_GT_Summary.IsSelected)
                {
                    Page_GT_Summary.Focus();
                }
                else if (Page_Refine.IsSelected)
                {
                    Page_Refine.Focus();
                }
                else if (Page_Refine.IsSelected)
                {
                    Page_Refine.Focus();
                }
            }
        }

        private void Btn_Execute_Click(object sender, RoutedEventArgs e)
        {
            double parsedvalue = 0;
            double parsedvalue2 = 0;
            double paselimit = 0;
            double parseexpensive = 0;
            double tooexpensive = 0;
            double parsecheap = 0;
            double toocheap = 0;
            double parseexpensive1 = 0;
            double tooexpensive1 = 0;
            double parsecheap1 = 0;
            double toocheap1 = 0;
            //Regex r = new Regex(@"^.$");
            Regex r = new Regex(@"^\d +?\.\d +?$");

            if (string.IsNullOrWhiteSpace(txt_graph_displayrng.Text) || string.IsNullOrWhiteSpace(txt_grah_range.Text))
            {
                MessageDialog.ErrorOk(LocalResource.MULTI_PSM_GRAPH_DISPLAY_RANGE);
                return;
            }
            if (double.TryParse(txt_graph_displayrng.Text, out paselimit) && !double.IsNaN(paselimit))
            {

                if (double.TryParse(txt_scale_interval.Text, out parsedvalue) && !double.IsNaN(parsedvalue))
                {


                    if (double.TryParse(txt_grah_range.Text, out parsedvalue2) && !double.IsNaN(parsedvalue2))
                    {
                        if (parsedvalue <= 0)
                        {
                            string msg = string.Format(LocalResource.MULTI_PSM_SCALE_LESS_ZERO);
                            MessageDialog.ErrorOk(msg);
                            return;
                        }
                        if (parsedvalue2 < paselimit)
                        {
                            string msg = string.Format(LocalResource.MULTI_PSM_GRAPH_ERROR);
                            MessageDialog.ErrorOk(msg);
                            return;
                        }
                        if ((parsedvalue2 - paselimit) <= 3)
                        {
                            string msg = string.Format(LocalResource.MULTI_PSM_GRAPH_DISPLAY_RANGE_TOO_LIMITED, 4);
                            MessageDialog.ErrorOk(msg);
                            return;
                        }
                        double result = (double)((parsedvalue2 - paselimit) / parsedvalue);
                        if (result < 4 && parsedvalue > 0)
                        {
                            MessageDialog.ErrorOk(LocalResource.MULTI_PSM_MSGBX_SCALE_INTERVAL_TOO_LARGE);
                            return;
                        }
                        else if (result > 249 || parsedvalue <= 0)
                        {
                            MessageDialog.ErrorOk(LocalResource.MULTI_PSM_MSGBX_SCALE_INTERVAL_TOO_SMALL);
                            return;
                        }
                    }
                    else
                    {
                        MessageDialog.ErrorOk(LocalResource.MULTI_PSM_MSGBX_SET_NUMERIC_VALUE_INGRAPH_DISPLAY);
                        return;
                    }
                }
                else
                {
                    MessageDialog.ErrorOk(LocalResource.MULTI_PSM_SCALE_NON_NUMERIC);
                    return;
                }

                if (Convert.ToDouble(txt_grah_range.Text) % 1 != 0 || Convert.ToDouble(txt_graph_displayrng.Text) % 1 != 0)
                {
                    string errormsg = "目盛間隔には小数点は設定できません";
                    MessageBoxResult result = MessageBox.Show(LocalResource.MULTI_PSM_SCALE_DECIMAL, "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (Convert.ToDouble(txt_scale_interval.Text) % 1 != 0)
                {
                    MessageBoxResult result = MessageBox.Show(LocalResource.MULTI_PSM_SCALE_INTERVAL_DECIMAL, "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                MessageDialog.ErrorOk(LocalResource.MULTI_PSM_MSGBX_SET_NUMERIC_VALUE_INGRAPH_DISPLAY);
                return;
            }
            if (string.IsNullOrEmpty(txt_Expensive_variable1.Text) || string.IsNullOrEmpty(txt_Cheap_variable1.Text) || string.IsNullOrEmpty(txt_tooExpensve_variable1.Text)
               || string.IsNullOrEmpty(txt_tooExpensve_variable1.Text) || string.IsNullOrEmpty(txt_toocheap_variable1.Text) || string.IsNullOrEmpty(txtbx_expnsive.Text) ||
               string.IsNullOrEmpty(txtbx_expnsive.Text) || string.IsNullOrEmpty(txtbx_cheap.Text) || string.IsNullOrEmpty(txtbx_tooexpnsive.Text) || string.IsNullOrEmpty(txtbx_tooCheep.Text)
               )
            {
                string errormsg = LocalResource.MULTI_PSM_MSGBX_RESTRICTIONS_WILL_NOT_APPLIED;
                MessageBoxResult result = MessageBox.Show(errormsg, "QuickCross", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                {
                    return;
                }

            }
            if (set_valid_values_chbx.IsChecked == true)
            {
                if (!double.TryParse(txt_Expensive_variable1.Text, out parseexpensive) || !double.TryParse(txt_Cheap_variable1.Text, out parsecheap) ||
                    !double.TryParse(txt_tooExpensve_variable1.Text, out tooexpensive) || !double.TryParse(txt_toocheap_variable1.Text, out toocheap) ||
                   !double.TryParse(txtbx_expnsive.Text, out parseexpensive1) || !double.TryParse(txtbx_cheap.Text, out parsecheap1) || !double.TryParse(txtbx_tooexpnsive.Text, out parseexpensive1)
                   || !double.TryParse(txtbx_tooCheep.Text, out toocheap1))
                {
                    MessageDialog.ErrorOk(LocalResource.MULTI_PSM_MSGBX_SET_NUMERIC_VALUE_GREATER_ZERO);
                    return;
                }
                else if (Convert.ToDouble(txt_Expensive_variable1.Text) < 0 || Convert.ToDouble(txtbx_expnsive.Text) < 0 || Convert.ToDouble(txt_Cheap_variable1.Text) < 0 ||
                    Convert.ToDouble(txtbx_cheap.Text) < 0 || Convert.ToDouble(txtbx_tooexpnsive.Text) < 0 || Convert.ToDouble(txt_tooExpensve_variable1.Text) < 0 ||
                    Convert.ToDouble(txt_toocheap_variable1.Text) < 0 || Convert.ToDouble(txtbx_tooCheep.Text) < 0)
                {
                    MessageDialog.ErrorOk(LocalResource.MULTI_PSM_MSGBX_SET_NUMERIC_VALUE_GREATER_ZERO);
                    return;
                }
                else if (Convert.ToDouble(txt_Expensive_variable1.Text) %1!=0 || Convert.ToDouble(txtbx_expnsive.Text) % 1 != 0 || Convert.ToDouble(txt_Cheap_variable1.Text) % 1 != 0||
                   Convert.ToDouble(txtbx_cheap.Text) % 1 != 0|| Convert.ToDouble(txtbx_tooexpnsive.Text) % 1 != 0 || Convert.ToDouble(txt_tooExpensve_variable1.Text) % 1 != 0 ||
                   Convert.ToDouble(txt_toocheap_variable1.Text) % 1 != 0 || Convert.ToDouble(txtbx_tooCheep.Text) % 1 != 0)
                {
                    MessageDialog.ErrorOk(LocalResource.MULTI_PSM_VALIDVALUE_DECIMAL);
                    return;
                }
            }
            else if (!CheckRefine())
            {
                return;

            }

            Close();
            MainWindow.Show();
            try
            {
                psmTabulate();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

        }

        private void OnWorkerMethodCompletePSM(double value, string status, bool isForceStop = false, bool retainThread = false, bool close = false, bool disableCancel = false)
        {
            progress.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
            new Action(
            delegate ()
            {
                if (close)
                {
                    if (retainThread)
                        progress.UpdateProgressBar(progress.getPbValue(), status, isForceStop, retainThread, disableCancel);
                    else
                        progress.Close();
                }
                else
                {
                    progress.UpdateProgressBar(value, status, isForceStop, retainThread, disableCancel);
                }
            }
            ));
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        private void psmTabulate()
        {
            bool result = true;
            try
            {
                using (new SingleGlobalInstance(10, Worksheet)) //10ms timeout on global lock
                {
                    PSMCalc pSMCalc = new PSMCalc();
                    System.ComponentModel.BackgroundWorker backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
                    progress = new ProgressBar(LocalResource.TITLE_CROSS_TAB);
                    WindowInteropHelper wih = new WindowInteropHelper(progress);
                    WindowInteropHelper wihMain = new WindowInteropHelper(MainWindow);
                    wih.Owner = wihMain.Handle;
                    IntPtr pbIntPtr = wihMain.Handle;
                    SetParent(wih.Handle, wihMain.Handle);
                    Worksheet.Application.Interactive = false;
                    pSMCalc.OnWorkerComplete += new PSMCalc.OnWorkerMethodCompleteDelegate(OnWorkerMethodCompletePSM);
                    backgroundWorker1.WorkerReportsProgress = true;
                    backgroundWorker1.WorkerSupportsCancellation = true;
                    backgroundWorker1.DoWork += new DoWorkEventHandler((object sender, DoWorkEventArgs e) =>
                     result = pSMCalc.Tabulate(Worksheet, sender, e, window: MainWindow as Window, pb: pbIntPtr)
                    );
                    backgroundWorker1.RunWorkerAsync();
                    progress.ShowDialog();
                    if (pSMCalc.childExcelApp != IntPtr.Zero)
                    {
                        try
                        {
                            SetForegroundWindow(pSMCalc.childExcelApp);
                        }
                        catch { }
                    }
                }
            }
            finally
            {
                Worksheet.Application.Interactive = true;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (!result)
                {
                    // this.ShowDialog();
                    PSM_ANALYSIS psm = new PSM_ANALYSIS(Worksheet, MainWindow, XlApp);
                    MainWindow.Hide();
                    try
                    {
                        psm.ShowDialog();

                    }
                    catch
                    {

                    }
                    finally
                    {
                        if (MainWindow.Visibility == Visibility.Hidden)
                        {
                            
                        }
                    }
                }
                else
                {
                    
                }
            }
        }

        private void Btn_Execute_Click_1(object sender, RoutedEventArgs e)
        {

        }
        private static T GetVisualChild<T>(DependencyObject parent) where T : Visual
        {
            T child = default(T);

            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (!CheckRefine())
            {
                e.Cancel = true;
            }


            try
            {
                Savesettings();
            }
            catch (Exception ex)
            {

            }

        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            
        }

       


        private void List_GT_Summary_CurrentCellChanged(object sender, EventArgs e)
        {
            var dg = (System.Windows.Controls.DataGrid)sender;
            ExpGrid = dg;
            dg.Focus();
        }

        private void List_GT_Summary_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            System.Windows.Controls.DataGrid grid = null;
            System.Windows.Controls.Button btn = null;
            System.Windows.Controls.CheckBox chk = null;

            if (sender is DataGrid)
                grid = sender as System.Windows.Controls.DataGrid;
            else if (sender is Button)
                btn = sender as System.Windows.Controls.Button;
            else if (sender is CheckBox)
                chk = sender as System.Windows.Controls.CheckBox;

            if (grid != null && e.Key == Key.Tab && grid.Name is "Multi_Process_Grid")
            {
                e.Handled = true;
                btn_rgt_arrw.Focus();
            }

            QC4Common.Util.GridCommonFunc.Arrow(sender, e);
        }
        bool FisrtTabFocus = true;
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as TabControl).SelectedIndex == 1 && FisrtTabFocus)
            {
                // Page_GT_Narrow.Focus();
                FisrtTabFocus = false;
            }
            else if ((sender as TabControl).SelectedIndex != 1)
            {
                FisrtTabFocus = true;
            }
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                if (combo_Answer_type.IsFocused)
                {
                    Multi_Process_Grid.FocusVisualStyle = null;
                    Multi_Process_Grid.Focus();
                    if (Multi_Process_Grid.SelectedIndex < 0 && Multi_Process_Grid.Items.Count >= 1)
                    {
                        Multi_Process_Grid.SelectedIndex = 0;
                    }
                }
                else if (btn_rgt_arrw.IsFocused)
                {
                    txt_Expensive_variable.Focus();
                    e.Handled = true;
                }
                else if (txt_Expensive_variable.IsFocused)
                {
                    Lbl_Single_Left_Arrow.Focus();
                    e.Handled = true;
                }
                else if (Lbl_Single_Left_Arrow.IsFocused)
                {
                    btn_rgt_arrw2.Focus();
                    e.Handled = true;
                }
                else if (btn_rgt_arrw2.IsFocused)
                {
                    txt_Cheap_variable.Focus();
                    e.Handled = true;
                }
                else if (txt_Cheap_variable.IsFocused)
                {
                    Lbl_Single_Left_Arrow2.Focus();
                    e.Handled = true;
                }
                else if (Lbl_Single_Left_Arrow2.IsFocused)
                {
                    btn_rgt_arrw3.Focus();
                    e.Handled = true;
                }
                else if (btn_rgt_arrw3.IsFocused)
                {
                    txt_TooExpensive_variable.Focus();
                    e.Handled = true;
                }
                else if (txt_TooExpensive_variable.IsFocused)
                {
                    Lbl_Single_Left_Arrow3.Focus();
                    e.Handled = true;
                }
                else if (Lbl_Single_Left_Arrow3.IsFocused)
                {
                    btn_rgt_arrw4.Focus();
                    e.Handled = true;
                }
                else if (btn_rgt_arrw4.IsFocused)
                {
                    txt_Tocheap_variable.Focus();
                    e.Handled = true;
                }
                else if (txt_Tocheap_variable.IsFocused)
                {
                    Lbl_Single_Left_Arrow4.Focus();
                    e.Handled = true;
                }
                else if (Lbl_Single_Left_Arrow4.IsFocused)
                {
                    txt_graph_displayrng.Focus();
                    e.Handled = true;
                }
                else if (txt_Expensive_variable1.IsFocused)
                {
                    txtbx_expnsive.Focus();
                    e.Handled = true;
                }
                else if (txtbx_expnsive.IsFocused)
                {
                    txt_Cheap_variable1.Focus();
                    e.Handled = true;
                }
                else if (txt_Cheap_variable1.IsFocused)
                {
                    txtbx_cheap.Focus();
                    e.Handled = true;
                }
                else if (txtbx_cheap.IsFocused)
                {
                    txt_tooExpensve_variable1.Focus();
                    e.Handled = true;
                }
                else if (txt_tooExpensve_variable1.IsFocused)
                {
                    txtbx_tooexpnsive.Focus();
                    e.Handled = true;
                }
                else if (txtbx_tooexpnsive.IsFocused)
                {
                    txt_toocheap_variable1.Focus();
                    e.Handled = true;
                }
                else if (txt_toocheap_variable1.IsFocused)
                {
                    txtbx_tooCheep.Focus();
                    e.Handled = true;
                }
            }
        }
    }
}
