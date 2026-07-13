using QC4Common.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Vb = Microsoft.VisualBasic;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Constants = Qc4Launcher.Util.Constants;
using static Qc4Launcher.Forms.Multivariate_Analysis.CollectionClass;
using Excel = Microsoft.Office.Interop.Excel;
using Qc4Launcher.Logic.MultiVariate;
using Qc4Launcher.Util;
using System.Reflection;
using log4net;
using System.Runtime.InteropServices;
using System.ComponentModel;
using FilterSettingsView;
using static FilterSettingsView.FilterSettingsClass;
using Qc4Launcher.Classes;
using QC4Common.Validation;
using System.Windows.Interop;
using QC4Common.Logic;

namespace Qc4Launcher.Forms.Multivariate_Analysis
{
    /// <summary>
    /// Interaction logic for CSPortfolioAnalysis.xaml
    /// </summary>
    public partial class CSPortfolioAnalysis : Window
    {
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





        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        Dictionary<string, QuestionSettings> variableDictionary = new Dictionary<string, QuestionSettings>();
        Excel.Workbook Workbook;
        public List<string> comboGraphValues = new List<string>();
        Window MainWindow;
        private ProgressBar progress = null;
        public CSPortfolioAnalysis(Excel.Workbook workbook, Window mV_Main)
        {
            Workbook = workbook;
            InitializeComponent();
            variableDictionary = Definiotion.VariableDictionary;
            MainWindow = mV_Main;
            fdesign.MyComboBoxSelectionChanged += new FilterControlMulti.MyComboBoxSelectionChangedEventHandler(OnSelectionChanged);

            fdesign.MyButtonClick += new FilterControlMulti.MyButtonClickEventEventHandler(OnButtonClick);
            fdesign.MyCheckBoxClick += new FilterControlMulti.MyCheckBoxClickEventHandler(OnCheck);
            fdesign.MyRadioButtonClick += new FilterControlMulti.MyRadioButtonClickEventHandler(OnRadioClick);
            fdesign.MyTextBoxChange += new FilterControlMulti.MyTextBoxChangeEventHandler(OnTextBoxChange);
            IsInitialLoad = true;
            LoadingData();
            LoadFilter();
            comboGraphValues.Add("No");
            comboGraphValues.Add("Horizontal");
            comboGraphValues.Add("Vertical");
            comboGraphValues.Add("Right Upper Diagonal Line");
            comboGraphValues.Add("Right Lower Diagonal Line");
            comboGraphValues.Add("From the Corner");
            comboGraphValues.Add("From the Center");

            background_combo.ItemsSource = comboGraphValues;
            Reset();



        }

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
                    //QuestionVariableType //QuestionVariable             
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

        private void b1SetColor(object sender, MouseButtonEventArgs e)
        {
            if (ExpGrid != null)
                ExpGrid.Focus();
        }
        private void DisableRightMenu(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
        System.Windows.Controls.DataGrid ExpGrid = null;
        private void List_GT_Summary_CurrentCellChanged(object sender, EventArgs e)
        {
            var dg = (System.Windows.Controls.DataGrid)sender;
            ExpGrid = dg;
            dg.Focus();
        }
        public void Reset()
        {
            TooExpensive_color_txtbx.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#262626"));
            Expensive_color_txtbx.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#0070C0"));
            backgroud_color_txtbx.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
            Cheep_color_txtbx.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#0070C0"));

            background_combo.SelectedIndex = 0;
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
            if (btn != null && e.Key == Key.Tab && btn.Name is "Lbl_Single_Left_Arrow" && txt_Selected_Variables.Count > 0)
            {

                btn_rgt_arrw_choice.Focus();
                e.Handled = true;
            }
            else if (btn != null && e.Key == Key.Tab && btn.Name is "Lbl_Single_Left_Arrow")
            {
                btn_rgt_arrw2.Focus();
                e.Handled = true;
            }
            if (btn != null && e.Key == Key.Tab && btn.Name is "Lbl_Single_Left_Arrow2" && Selected_grid2.Items.Count > 0)
            {

                btn_rgt_arrw_choice3.Focus();
                e.Handled = true;
            }
            else if (btn != null && e.Key == Key.Tab && btn.Name is "Lbl_Single_Left_Arrow2")
            {
                Btn_close.Focus();
                e.Handled = true;
            }

            else if (grid != null && e.Key == Key.Tab && grid.Name is "Multi_Process_Grid")
            {

                btn_rgt_arrw.Focus();
                e.Handled = true;
            }
            else if (grid != null && e.Key == Key.Tab && grid.Name is "selected_choicegrid")
            {

                Lbl_Single_Left_Arrow_choice.Focus();
                e.Handled = true;
            }
            else if (grid != null && e.Key == Key.Tab && grid.Name is "Selected_grid")
            {

                btn_rgt_arrw2.Focus();
                e.Handled = true;
            }
            else if (grid != null && e.Key == Key.Tab && grid.Name is "Selected_grid2")
            {

                Lbl_Single_Left_Arrow2.Focus();
                e.Handled = true;
            }
            else if (grid != null && e.Key == Key.Tab && grid.Name is "Selected_grid4")
            {

                Lbl_Single_Left_Arrow_choice3.Focus();
                e.Handled = true;
            }
            else if (grid != null && e.Key == Key.Tab && grid.Name is "Selected_grid3")
            {
                if (Selected_grid4.Items.Count > 0)
                {
                    btn_Execute.Focus();
                    e.Handled = true;
                }
                else
                {
                    Btn_close.Focus();
                    e.Handled = true;
                }
            }


            QC4Common.Util.GridCommonFunc.Arrow(sender, e);
        }
        private void Window_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                if (Page_GT_Summary.IsFocused)
                {


                    Multi_Process_Grid.FocusVisualStyle = null;
                    Multi_Process_Grid.Focus();
                    if (Multi_Process_Grid.SelectedIndex < 0 && Multi_Process_Grid.Items.Count >= 1)
                    {
                        Multi_Process_Grid.SelectedIndex = 0;
                    }
                    e.Handled = true;
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
                else if (btn_rgt_arrw_choice.IsFocused)
                {
                    selected_choicegrid.FocusVisualStyle = null;
                    selected_choicegrid.Focus();
                    if (selected_choicegrid.SelectedIndex < 0 && selected_choicegrid.Items.Count >= 1)
                    {
                        selected_choicegrid.SelectedIndex = 0;
                    }
                    e.Handled = true;
                }

                else if (Lbl_Single_Left_Arrow_choice.IsFocused)
                {
                    Selected_grid.FocusVisualStyle = null;
                    Selected_grid.Focus();
                    if (Selected_grid.SelectedIndex < 0 && Selected_grid.Items.Count >= 1)
                    {
                        Selected_grid.SelectedIndex = 0;
                    }
                    e.Handled = true;
                }

                else if (btn_rgt_arrw2.IsFocused)
                {
                    Selected_grid2.FocusVisualStyle = null;
                    Selected_grid2.Focus();
                    if (Selected_grid2.SelectedIndex < 0 && Selected_grid2.Items.Count >= 1)
                    {
                        Selected_grid2.SelectedIndex = 0;
                    }
                    e.Handled = true;
                }

                else if (btn_rgt_arrw_choice3.IsFocused)
                {
                    Selected_grid4.FocusVisualStyle = null;
                    Selected_grid4.Focus();
                    if (Selected_grid4.SelectedIndex < 0 && Selected_grid4.Items.Count >= 1)
                    {
                        Selected_grid4.SelectedIndex = 0;
                    }
                    e.Handled = true;
                }
                else if (Lbl_Single_Left_Arrow_choice3.IsFocused)
                {
                    Selected_grid3.FocusVisualStyle = null;
                    Selected_grid3.Focus();
                    if (Selected_grid3.SelectedIndex < 0 && Selected_grid3.Items.Count >= 1)
                    {
                        Selected_grid3.SelectedIndex = 0;
                    }
                    e.Handled = true;
                }
                // else if (txt_Expensive_variable.IsFocused&& SelectedChoices2.Count>0 && SelectedChoices5.Count>0)
                //  {
                //      btn_Execute.Focus();
                //      e.Handled = true;
                //  }else if (txt_Expensive_variable.IsFocused)
                //  {
                //      Btn_close.Focus();
                //      e.Handled = true;
                //  }
            }
        }
        bool FisrtTabFocus = true;
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl) //if this event fired from TabControl then enter
            {
                if (Page_GT_Summary.IsSelected)
                {
                    Page_GT_Summary.Focus();
                }
                else
                {

                }
            }
        }
        public void Savesettings()
        {
            string[] paramList = new string[Selected_grid2.Items.Count];
            for (int i = 0; i < Selected_grid2.Items.Count; i++)
            {
                psmQuestions dat = (psmQuestions)Selected_grid2.Items[i];
                paramList[i] = dat.Variable;
            }
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
            int colCount = (9) + (Selected_grid2.Items.Count);
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
                                    SelectedVariable[i, j] = ProcessingMethod.CSPORTFOLIO_ANALYSIS;
                                }
                            }
                            break;
                        case 5://checkcross
                            if (!string.IsNullOrEmpty(SelectedVariable[i, 4]) && (SelectedVariable[i, 4] == ProcessingMethod.CSPORTFOLIO_ANALYSIS))
                            {
                                if (Check_Refine_Condition.IsChecked == true)
                                {
                                    SelectedVariable[i, j] = "1";
                                }
                                else
                                {
                                    SelectedVariable[i, j] = "0";
                                }

                            }
                            break;

                        case 6://checkcross
                            if (!string.IsNullOrEmpty(SelectedVariable[i, 4]) && (SelectedVariable[i, 4] == ProcessingMethod.CSPORTFOLIO_ANALYSIS))
                            {
                                if (txt_Selected_Variables.Count > 0)
                                {
                                    SelectedVariable[i, j] = txt_Selected_Variables[0].Variable;
                                }
                            }
                            break;
                        case 7://no substitute variable
                            if (!string.IsNullOrEmpty(SelectedVariable[i, 4]) && (SelectedVariable[i, 4] == ProcessingMethod.CSPORTFOLIO_ANALYSIS))
                            {
                                string choice = string.Empty;
                                if (selected_choicegrid.Items.Count > 0)
                                {
                                    choice = string.Join(",", SelectedChoices2.Select(x => x.orderId));
                                }
                                SelectedVariable[i, j] = choice;
                            }
                            break;
                        case 8:
                            if (!string.IsNullOrEmpty(SelectedVariable[i, 4]) && (SelectedVariable[i, 4] == ProcessingMethod.CSPORTFOLIO_ANALYSIS))
                            {
                                string choice2 = string.Empty;
                                if (Selected_grid4.Items.Count > 0)
                                {
                                    choice2 = string.Join(",", SelectedChoices5.Select(x => x.orderId));
                                }
                                SelectedVariable[i, j] = choice2;
                            }
                            break;

                        default://parameter list
                            if (!string.IsNullOrEmpty(SelectedVariable[i, 4]) && (SelectedVariable[i, 4] == ProcessingMethod.CSPORTFOLIO_ANALYSIS))
                            {
                                SelectedVariable[i, j] = paramList[param];
                                param++;
                            }
                            break;

                    }
                }
            }

            sourceGetList.WriteFilterrSettings(Workbook, SelectedVariable, 34, 29);
        }


        public static Dictionary<string, QuestionSettings> dictionary = new Dictionary<string, QuestionSettings>();
        SourceVariableFromList sourceGetList = new SourceVariableFromList();
        ObservableCollection<psmQuestions> SourceVariableList = new ObservableCollection<psmQuestions>();
        ObservableCollection<Choiceclass> SelectedChoices1 = new ObservableCollection<Choiceclass>();
        ObservableCollection<Choiceclass> SelectedChoices2 = new ObservableCollection<Choiceclass>();
        ObservableCollection<psmQuestions> SelectedChoices3 = new ObservableCollection<psmQuestions>();
        ObservableCollection<Choiceclass> SelectedChoices4 = new ObservableCollection<Choiceclass>();
        ObservableCollection<Choiceclass> SelectedChoices5 = new ObservableCollection<Choiceclass>();
        public void LoadFilter()
        {
            try
            {
                var res = Util.ExcelUtil.GetWorkSheetBySheetName(Workbook, Constants.SheetType.sh_Sheet2); //GetWorkSheetByCodeName(Workbook, Constants.SheetCodeName.MultiVariate);
                Excel.Range crstart = res.Cells[29, 2];
                Excel.Range crlast = res.Cells[34, 235];
                Excel.Range rar = res.get_Range(crstart, crlast);
                var val = rar.Value;
                if (val[1, 1] != null)
                {
                    for (int i = 1; i <= val.GetLength(0); i++)
                    {
                        if (val[i, 1] != null)
                        {
                            if (val[i, 1] != null)
                            {
                                string criteriaVariable = val[i, 2];
                                if (i == 1)
                                {
                                    //Check_Refine_Condition.IsChecked = true;
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
            catch
            {

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //  LoadingData();
            txtchoice.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
            txtchoice1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
            txtchoice2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
            txtchoice3.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
            Lbl_Single_Left_Arrow_choice.IsEnabled = false;
            btn_rgt_arrw_choice.IsEnabled = false;
            btn_rgt_arrw_choice3.IsEnabled = false;
            Lbl_Single_Left_Arrow_choice3.IsEnabled = false;




            SourceVariableList = new ObservableCollection<psmQuestions>(sourceGetList.GetVariableFromList(Workbook, "List_Item_SA"));
            psmQuestions insta = new psmQuestions();
            sourceGetList.RemoveItem(SourceVariableList, insta);


            try
            {
                var res = Util.ExcelUtil.GetWorkSheetBySheetName(Workbook, Constants.SheetType.sh_Sheet2); //GetWorkSheetByCodeName(Workbook, Constants.SheetCodeName.MultiVariate);
                Excel.Range crstart = res.Cells[29, 2];
                Excel.Range crlast = res.Cells[34, 235];
                Excel.Range rar = res.get_Range(crstart, crlast);
                var val = rar.Value;
                if (val[1, 1] != null)
                {
                    for (int i = 1; i <= val.GetLength(0); i++)
                    {
                        if (val[i, 1] != null)
                        {

                            string instruction = val[i, 5].ToString();
                            if (instruction == ProcessingMethod.CSPORTFOLIO_ANALYSIS)
                            {
                                int colnum = 10;
                                ObservableCollection<psmQuestions> list = new ObservableCollection<psmQuestions>();


                                if (val[i, 7] != null)
                                {
                                    if (SourceVariableList.Any(x => x.Variable == val[i, 7]))
                                    {
                                        var find = SourceVariableList.FirstOrDefault(x => x.Variable == val[i, 7].ToString());

                                        txt_Selected_Variables.Add(find);
                                        if (txt_Selected_Variables.Count > 0)
                                        {
                                            psmQuestions psmQuestions = new psmQuestions();
                                            psmQuestions = txt_Selected_Variables[0];
                                            SourceVariableList.Remove(psmQuestions);
                                        }
                                        txt_Expensive_variable.Text = txt_Selected_Variables[0].Variable;
                                        if (txt_Selected_Variables[0].Choices.Count > 0)
                                        {
                                            SelectedChoices1.Clear();
                                            SelectedChoices2.Clear();
                                            int k = 1;
                                            foreach (string item in txt_Selected_Variables[0].Choices)
                                            {
                                                Choiceclass choicedata = new Choiceclass();
                                                choicedata.choice = item;
                                                choicedata.orderId = k;
                                                k++;
                                                SelectedChoices1.Add(choicedata);
                                            }
                                            txtchoice.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                                            txtchoice1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                                            Lbl_Single_Left_Arrow_choice.IsEnabled = true;
                                            btn_rgt_arrw_choice.IsEnabled = true;
                                        }
                                    }
                                }
                                if (val[i, 8] != null)
                                {
                                    string selectedChoice = val[i, 8].ToString();
                                    String[] spearator = { "," };

                                    string[] strlist = selectedChoice.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    if (strlist.Count() > 0)
                                    {
                                        foreach (Choiceclass items in SelectedChoices1)
                                        {
                                            foreach (var ch in strlist)
                                            {
                                                int parse = 0;
                                                int.TryParse(ch, out parse);
                                                if (items.orderId == parse)
                                                {
                                                    SelectedChoices2.Add(items);
                                                }
                                            }
                                        }
                                        if (SelectedChoices2.Count > 0)
                                        {
                                            foreach (Choiceclass item in SelectedChoices2)
                                            {
                                                SelectedChoices1.Remove(item);
                                            }
                                        }
                                        SelectedChoices2.Sort(k => k.orderId);
                                        SelectedChoices1.Sort(k => k.orderId);
                                    }

                                    while (colnum <= val.GetLength(1))
                                    {
                                        if (val[i, colnum] != null)
                                        {
                                            if (SourceVariableList.Any(x => x.Variable == val[i, colnum]))
                                            {
                                                var find = SourceVariableList.FirstOrDefault(x => x.Variable == val[i, colnum].ToString());

                                                list.Add(find);
                                            }
                                        }
                                        colnum++;
                                    }
                                    if (list.Count > 0)
                                    {
                                        foreach (psmQuestions item in list)
                                        {
                                            SelectedChoices3.Add(item);
                                        }
                                        foreach (psmQuestions crs in SelectedChoices3)
                                        {
                                            SourceVariableList.Remove(crs);
                                        }

                                        SelectedChoices3.Sort(k => k.OrderNo);
                                        SourceVariableList.Sort(k => k.OrderNo);
                                        if (SelectedChoices3.Count > 0)
                                        {
                                            if (SelectedChoices3[0].Choices.Count > 0)
                                            {
                                                SelectedChoices4.Clear();
                                                SelectedChoices5.Clear();
                                                int k = 1;
                                                foreach (string item in SelectedChoices3[0].Choices)
                                                {
                                                    Choiceclass choicedata = new Choiceclass();
                                                    choicedata.choice = item;
                                                    choicedata.orderId = k;
                                                    k++;
                                                    SelectedChoices4.Add(choicedata);
                                                }
                                                txtchoice2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                                                txtchoice3.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                                                btn_rgt_arrw_choice3.IsEnabled = true;
                                                Lbl_Single_Left_Arrow_choice3.IsEnabled = true;
                                                Selected_grid3.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                                                Selected_grid4.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));

                                            }
                                        }
                                    }

                                }

                                if (val[i, 9] != null)
                                {
                                    string selectedChoice1 = val[i, 9].ToString();
                                    String[] spearator1 = { "," };

                                    string[] strlist1 = selectedChoice1.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    if (strlist1.Count() > 0)
                                    {
                                        foreach (Choiceclass items in SelectedChoices4)
                                        {
                                            foreach (var ch in strlist1)
                                            {
                                                int parse = 0;
                                                int.TryParse(ch, out parse);
                                                if (items.orderId == parse)
                                                {
                                                    SelectedChoices5.Add(items);
                                                }
                                            }
                                        }
                                        if (SelectedChoices5.Count > 0)
                                        {
                                            foreach (Choiceclass item in SelectedChoices5)
                                            {
                                                SelectedChoices4.Remove(item);
                                            }
                                        }
                                        SelectedChoices4.Sort(k => k.orderId);
                                        SelectedChoices5.Sort(k => k.orderId);
                                    }
                                }

                                if (val[i, 6] == "0")
                                {
                                    Check_Refine_Condition.IsChecked = false;
                                    fdesign.check(false);

                                }
                                else if (val[i, 6] == "1")
                                {
                                    Check_Refine_Condition.IsChecked = true;
                                    fdesign.check(true);

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }











            Multi_Process_Grid.ItemsSource = SourceVariableList;
            Selected_grid.ItemsSource = SelectedChoices1;
            selected_choicegrid.ItemsSource = SelectedChoices2;
            Selected_grid2.ItemsSource = SelectedChoices3;
            Selected_grid3.ItemsSource = SelectedChoices4;
            Selected_grid4.ItemsSource = SelectedChoices5;
            orderId = SourceVariableList.Count();
            enableButton();
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
        ObservableCollection<psmQuestions> txt_Selected_Variables = new ObservableCollection<psmQuestions>();

        private void Btn_rgt_arrw_Click(object sender, RoutedEventArgs e)
        {
            if (Multi_Process_Grid.SelectedItem != null)
            {
                if (Multi_Process_Grid.SelectedItems.Count > 1)
                {
                    MessageBox.Show(LocalResource.MULTI_CS_MSGBX_ONLY_ONE_VARIABLE_CAN_BE_SELECTED, "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                psmQuestions crs = (psmQuestions)Multi_Process_Grid.SelectedItem;


                if (txt_Selected_Variables.Count > 0)
                {
                    SourceVariableList.Add(txt_Selected_Variables[0]); SourceVariableList.Sort(k => k.OrderNo);
                    txt_Selected_Variables.Clear();
                    SelectedChoices1.Clear();
                    txt_Selected_Variables.Add(crs);
                    SourceVariableList.Remove(crs);
                    txt_Expensive_variable.Text = txt_Selected_Variables[0].Variable;



                }
                else
                {
                    txt_Selected_Variables.Clear();
                    SelectedChoices1.Clear();
                    txt_Selected_Variables.Add(crs);
                    SourceVariableList.Remove(crs);
                    txt_Expensive_variable.Text = txt_Selected_Variables[0].Variable;
                    Selected_grid.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F0F0F0"));
                    selected_choicegrid.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F0F0F0"));
                }

                if (txt_Selected_Variables.Count > 0)
                {
                    Selected_grid.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                    selected_choicegrid.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                    if (txt_Selected_Variables[0].Choices.Count > 0)
                    {
                        SelectedChoices1.Clear();
                        SelectedChoices2.Clear();
                        int i = 1;
                        foreach (string item in txt_Selected_Variables[0].Choices)
                        {
                            Choiceclass choicedata = new Choiceclass();
                            choicedata.choice = item;
                            choicedata.orderId = i;
                            i++;
                            SelectedChoices1.Add(choicedata);
                        }
                        txtchoice.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                        txtchoice1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                        Lbl_Single_Left_Arrow_choice.IsEnabled = true;
                        btn_rgt_arrw_choice.IsEnabled = true;
                    }
                }
                else
                {
                    SelectedChoices1.Clear();

                }
                enableButton();
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
        private void Btn_rgt_arrw_choice_Click(object sender, RoutedEventArgs e)
        {
            if (Selected_grid.SelectedItems.Count > 0)
            {
                foreach (Choiceclass item in Selected_grid.SelectedItems)
                {

                    SelectedChoices2.Add(item);

                    //
                }
                SelectedChoices2.Sort(k => k.orderId);
                if (Selected_grid != null)
                {
                    ScrollViewer scrollViewer = GetVisualChild<ScrollViewer>(Selected_grid);
                    if (scrollViewer != null)
                    {
                        scrollViewer.ScrollToTop();
                    }
                }
                if (selected_choicegrid != null)
                {
                    ScrollViewer scrollViewer = GetVisualChild<ScrollViewer>(selected_choicegrid);
                    if (scrollViewer != null)
                    {
                        scrollViewer.ScrollToTop();
                    }
                }
            }
            if (SelectedChoices2.Count > 0)
            {
                foreach (var item in SelectedChoices2)
                {
                    SelectedChoices1.Remove(item);
                }
            }
            enableButton();
        }

        private void Lbl_Single_Left_Arrow_Click(object sender, RoutedEventArgs e)
        {
            if (txt_Selected_Variables.Count > 0)
            {
                SourceVariableList.Add(txt_Selected_Variables[0]);
                SourceVariableList.Sort(k => k.OrderNo);
                txt_Selected_Variables.Clear();
                SelectedChoices1.Clear();
                SelectedChoices2.Clear();
                txt_Expensive_variable.Text = string.Empty;
                if (Multi_Process_Grid != null)
                {
                    ScrollViewer scrollViewer = GetVisualChild<ScrollViewer>(Multi_Process_Grid);
                    if (scrollViewer != null)
                    {
                        scrollViewer.ScrollToTop();
                    }
                }
            }
            if (txt_Selected_Variables.Count == 0)
            {
                txtchoice.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txtchoice1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                Selected_grid.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F0F0F0"));
                selected_choicegrid.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F0F0F0"));
                Lbl_Single_Left_Arrow_choice.IsEnabled = false;
                btn_rgt_arrw_choice.IsEnabled = false;
            }
            enableButton();
        }

        private void Lbl_Single_Left_Arrow_choice_Click(object sender, RoutedEventArgs e)
        {
            if (selected_choicegrid.SelectedItems.Count > 0)
            {
                foreach (Choiceclass item in selected_choicegrid.SelectedItems)
                {
                    SelectedChoices1.Add(item);

                }
                SelectedChoices1.Sort(k => k.orderId);
                foreach (var item in SelectedChoices1)
                {
                    SelectedChoices2.Remove(item);
                }
                if (Selected_grid != null)
                {
                    ScrollViewer scrollViewer = GetVisualChild<ScrollViewer>(Selected_grid);
                    if (scrollViewer != null)
                    {
                        scrollViewer.ScrollToTop();
                    }
                }
                if (selected_choicegrid != null)
                {
                    ScrollViewer scrollViewer = GetVisualChild<ScrollViewer>(selected_choicegrid);
                    if (scrollViewer != null)
                    {
                        scrollViewer.ScrollToTop();
                    }
                }
            }
            enableButton();
        }
        public int orderId = 0;
        string selectedVariable = string.Empty;
        private void Btn_rgt_arrw2_Click(object sender, RoutedEventArgs e)
        {
            if (Multi_Process_Grid.SelectedItems.Count > 0)
            {
                int count = (Multi_Process_Grid.SelectedItems.Count) + (Selected_grid2.Items.Count);
                if (count <= 100)
                {
                    if (SelectedChoices3.Count == 0)
                    {
                        selectedVariable = string.Empty;
                    }
                    else if (SelectedChoices3.Count > 0)
                    {
                        selectedVariable = SelectedChoices3[0].Variable;
                    }
                    foreach (psmQuestions item in Multi_Process_Grid.SelectedItems)
                    {
                        SelectedChoices3.Add(item);

                    }
                    SelectedChoices3.Sort(k => k.OrderNo);

                    if (SelectedChoices3.Count > 0)
                    {
                        foreach (psmQuestions item in SelectedChoices3)
                        {
                            SourceVariableList.Remove(item);
                        }
                        if (selectedVariable == string.Empty || selectedVariable != SelectedChoices3[0].Variable)
                        {
                            SelectedChoices4.Clear();
                            SelectedChoices5.Clear();
                            int i = 1;
                            foreach (var item in SelectedChoices3[0].Choices)
                            {
                                Choiceclass ch = new Choiceclass();
                                ch.choice = item;
                                ch.orderId = i;
                                i++;
                                SelectedChoices4.Add(ch);

                            }
                            SelectedChoices3.Sort(k => k.OrderNo);
                            selectedVariable = SelectedChoices3[0].Variable;
                        }
                    }
                    txtchoice2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                    txtchoice3.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                    btn_rgt_arrw_choice3.IsEnabled = true;
                    Lbl_Single_Left_Arrow_choice3.IsEnabled = true;
                    Selected_grid3.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                    Selected_grid4.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                    if (Multi_Process_Grid != null)
                    {
                        ScrollViewer scrollViewer = GetVisualChild<ScrollViewer>(Multi_Process_Grid);
                        if (scrollViewer != null)
                        {
                            scrollViewer.ScrollToTop();
                        }
                    }
                    if (Selected_grid2 != null)
                    {
                        ScrollViewer scrollViewer = GetVisualChild<ScrollViewer>(Selected_grid2);
                        if (scrollViewer != null)
                        {
                            scrollViewer.ScrollToTop();
                        }
                    }
                }
                else
                {
                    MessageBox.Show(LocalResource.MULTI_CLUSTOR_MSGBX_WITHIN_100, "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        private void Lbl_Single_Left_Arrow2_Click(object sender, RoutedEventArgs e)
        {
            if (Selected_grid2.SelectedItems.Count > 0)
            {

                foreach (psmQuestions item in Selected_grid2.SelectedItems)
                {
                    SourceVariableList.Add(item);

                }
                foreach (psmQuestions crs in SourceVariableList)
                {
                    SelectedChoices3.Remove(crs);
                }
                SourceVariableList.Sort(k => k.OrderNo);
                SelectedChoices3.Sort(k => k.OrderNo);
                if (SelectedChoices3.Count > 0)
                {
                    if (selectedVariable != SelectedChoices3[0].Variable)
                    {
                        SelectedChoices5.Clear();
                    }
                    //txtchoice2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                    //txtchoice3.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                    //btn_rgt_arrw_choice3.IsEnabled = true;
                    //Lbl_Single_Left_Arrow_choice3.IsEnabled = true;
                    //Selected_grid3.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                    //Selected_grid4.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                }
                else
                {
                    txtchoice2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                    txtchoice3.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                    btn_rgt_arrw_choice3.IsEnabled = false;
                    Lbl_Single_Left_Arrow_choice3.IsEnabled = false;
                }
                if (SelectedChoices3.Count > 0 && selectedVariable != SelectedChoices3[0].Variable)
                {
                    foreach (psmQuestions item in SelectedChoices3)
                    {
                        SourceVariableList.Remove(item);
                    }


                    SelectedChoices4.Clear();
                    int i = 0;
                    foreach (var item in SelectedChoices3[0].Choices)
                    {
                        Choiceclass ch = new Choiceclass();
                        ch.choice = item;
                        ch.orderId = i;
                        i++;
                        SelectedChoices4.Add(ch);
                        SelectedChoices4.Sort(k => k.orderId);
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

            if (Selected_grid2.Items.Count == 0)
            {
                SelectedChoices4.Clear();
                SelectedChoices5.Clear();

                Selected_grid3.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F0F0F0"));
                Selected_grid4.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F0F0F0"));

            }
            enableButton();
        }

        private void Btn_rgt_arrw_choice3_Click(object sender, RoutedEventArgs e)
        {
            if (Selected_grid3.SelectedItems.Count > 0)
            {
                foreach (Choiceclass item in Selected_grid3.SelectedItems)
                {
                    SelectedChoices5.Add(item);

                }
                SelectedChoices5.Sort(k => k.orderId);
                foreach (Choiceclass item in SelectedChoices5)
                {
                    SelectedChoices4.Remove(item);
                }
                if (Selected_grid3 != null)
                {
                    ScrollViewer scrollViewer = GetVisualChild<ScrollViewer>(Selected_grid3);
                    if (scrollViewer != null)
                    {
                        scrollViewer.ScrollToTop();
                    }
                }
                if (Selected_grid4 != null)
                {
                    ScrollViewer scrollViewer = GetVisualChild<ScrollViewer>(Selected_grid4);
                    if (scrollViewer != null)
                    {
                        scrollViewer.ScrollToTop();
                    }
                }
            }
            enableButton();
        }
        //private void OnCheck(object sender, RoutedEventArgs e)
        //{
        //    if (Check_Criteria.IsChecked == true)
        //        Criteria_Control.ReviseData_Checkbox_OnClick(true);
        //    else
        //        Criteria_Control.ReviseData_Checkbox_OnClick(false);
        //}

        private void Backgroud_color_txtbx_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            System.Windows.Controls.TextBox txtbox = (System.Windows.Controls.TextBox)sender;
            System.Windows.Forms.ColorDialog dlg = new System.Windows.Forms.ColorDialog();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtbox.Background = new SolidColorBrush(Color.FromArgb(dlg.Color.A, dlg.Color.R, dlg.Color.G, dlg.Color.B));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }
        public void enableButton()
        {
            if (Selected_grid4.Items.Count > 0 && selected_choicegrid.Items.Count > 0)
            {
                btn_Execute.IsEnabled = true;
            }
            else
            {
                btn_Execute.IsEnabled = false;
            }
        }
        private void Lbl_Single_Left_Arrow_choice3_Click(object sender, RoutedEventArgs e)
        {
            if (Selected_grid4.SelectedItems.Count > 0)
            {
                foreach (Choiceclass item in Selected_grid4.SelectedItems)
                {
                    SelectedChoices4.Add(item);

                }
                SelectedChoices4.Sort(k => k.orderId);
                foreach (Choiceclass item in SelectedChoices4)
                {
                    SelectedChoices5.Remove(item);
                }
                if (Selected_grid3 != null)
                {
                    ScrollViewer scrollViewer = GetVisualChild<ScrollViewer>(Selected_grid3);
                    if (scrollViewer != null)
                    {
                        scrollViewer.ScrollToTop();
                    }
                }
                if (Selected_grid4 != null)
                {
                    ScrollViewer scrollViewer = GetVisualChild<ScrollViewer>(Selected_grid4);
                    if (scrollViewer != null)
                    {
                        scrollViewer.ScrollToTop();
                    }
                }
            }

            enableButton();
        }

        private void Btn_Execute_Click(object sender, RoutedEventArgs e)
        {
            if (txt_Selected_Variables[0].Choices.Count != SelectedChoices3[0].Choices.Count)
            {
                MessageBox.Show(LocalResource.MULTI_CS_MSGBX_NUMB_OVERALL_EVALUATION_DONOT_MATCH, "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                foreach (psmQuestions item in SelectedChoices3)
                {
                    if (item.Choices.Count != txt_Selected_Variables[0].Choices.Count)
                    {
                        string msg = string.Format(LocalResource.MULTI_CS_MSGBX_INDIVIDUAL_EVALUATION, item.Variable);
                        MessageBox.Show(msg, "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }

            if (!CheckRefine())
            {

                return;

            }
            Close();
            bool mlock= true;
            try
            {
                Savesettings();
                MainWindow.Show();
                string errmsg = string.Empty;
                using (new SingleGlobalInstance(10, Workbook)) //10ms timeout on global lock
                {
                    CS_Portfolio csp = new CS_Portfolio();
                    mlock = false;
                   
                    System.ComponentModel.BackgroundWorker backgroundWorker1 = new System.ComponentModel.BackgroundWorker();

                    bool retvalue = false;
                    progress = new ProgressBar(LocalResource.TITLE_CROSS_TAB);
                    WindowInteropHelper wih = new WindowInteropHelper(progress);
                    var winh = new WindowInteropHelper(MainWindow);
                    wih.Owner = winh.Handle;
                    csp.OnWorkerComplete += new CS_Portfolio.OnWorkerMethodCompleteDelegate(OnWorkerMethodCompletePSM);
                    backgroundWorker1.WorkerReportsProgress = true;
                    backgroundWorker1.WorkerSupportsCancellation = true;
                    backgroundWorker1.DoWork += new DoWorkEventHandler((object senderr, DoWorkEventArgs ee) =>

                   retvalue = csp.ExecutePortfolio(Workbook, out errmsg)
                    );
                    backgroundWorker1.RunWorkerAsync();
                    progress.ShowDialog();
                }
                if (!string.IsNullOrEmpty(errmsg))
                {
                    //error msg
                    MessageBox.Show(errmsg, "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error);

                    CSPortfolioAnalysis cs = new CSPortfolioAnalysis(Workbook, MainWindow);

                    MainWindow.Hide();
                    try
                    {
                        cs.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        //_log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                        string exs = ex.Message;
                    }
                    finally
                    {

                    }
                }

            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                return;
            }
            finally
            {
               if( mlock )
                {
                    CSPortfolioAnalysis cs = new CSPortfolioAnalysis(Workbook, MainWindow);

                    MainWindow.Hide();
                    try
                    {
                        cs.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        //_log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                        string exs = ex.Message;
                    }
                    finally
                    {

                    }
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!CheckRefine())
            {

                e.Cancel = true;

            }
            else
            {
                try
                {
                    Savesettings();
                }
                catch (Exception ex)
                {

                }
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        //private void psmTabulate()
        //{
        //    try
        //    {
        //        using (new QC4Common.Logic.SingleGlobalInstance(10, Worksheet)) //10ms timeout on global lock
        //        {
        //            PSMCalc pSMCalc = new PSMCalc();
        //            System.ComponentModel.BackgroundWorker backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
        //            progress = new ProgressBar(LocalResource.TITLE_CROSS_TAB);
        //            WindowInteropHelper wih = new WindowInteropHelper(progress);
        //            WindowInteropHelper wihMain = new WindowInteropHelper(MainWindow);
        //            wih.Owner = wihMain.Handle;
        //            IntPtr pbIntPtr = wihMain.Handle;
        //            SetParent(wih.Handle, wihMain.Handle);
        //            Worksheet.Application.Interactive = false;
        //            pSMCalc.OnWorkerComplete += new PSMCalc.OnWorkerMethodCompleteDelegate(OnWorkerMethodCompletePSM);
        //            backgroundWorker1.WorkerReportsProgress = true;
        //            backgroundWorker1.WorkerSupportsCancellation = true;
        //            backgroundWorker1.DoWork += new DoWorkEventHandler((object sender, DoWorkEventArgs e) =>
        //                pSMCalc.Tabulate(Worksheet, sender, e, window: MainWindow as Window, pb: pbIntPtr)
        //            );
        //            backgroundWorker1.RunWorkerAsync();
        //            progress.ShowDialog();
        //            if (pSMCalc.childExcelApp != IntPtr.Zero)
        //            {
        //                try
        //                {
        //                    SetForegroundWindow(pSMCalc.childExcelApp);
        //                }
        //                catch { }
        //            }
        //        }
        //    }
        //    finally
        //    {
        //        Worksheet.Application.Interactive = true;
        //        GC.Collect();
        //        GC.WaitForPendingFinalizers();
        //    }
        //}
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

        private void Txt_Expensive_variable_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_Selected_Variables.Count > 0)
            {
                Selected_grid.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                selected_choicegrid.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
            }
        }
    }
}
