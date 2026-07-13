using QC4Common.Model;
using Qc4Launcher.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vb = Microsoft.VisualBasic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static Qc4Launcher.Forms.Multivariate_Analysis.CollectionClass;
using Excel = Microsoft.Office.Interop.Excel;
using Constants = Qc4Launcher.Util.Constants;
using Qc4Launcher.Logic.MultiVariate;
using System.ComponentModel;
using log4net;
using System.Reflection;
using QC4Common.Validation;
using static FilterSettingsView.FilterSettingsClass;
using FilterSettingsView;
using Qc4Launcher.Classes;
using System.Windows.Interop;
using QC4Common.Logic;

namespace Qc4Launcher.Forms.Multivariate_Analysis
{
    /// <summary>
    /// Interaction logic for Factor_Analysis.xaml
    /// </summary>
    public partial class Factor_Analysis : Window
    {



        Excel.Workbook Workbook;
        public static Dictionary<string, QuestionSettings> dictionary = new Dictionary<string, QuestionSettings>();
        SourceVariableFromList sourceGetList = new SourceVariableFromList();
        ObservableCollection<psmQuestions> SourceVariableList = new ObservableCollection<psmQuestions>();
        Dictionary<string, QuestionSettings> variableDictionary = new Dictionary<string, QuestionSettings>();
        Window MainWindow;
        private ProgressBar progress = null;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public Factor_Analysis(Excel.Workbook workbook, Window mV_Main)
        {
            InitializeComponent();
            MainWindow = mV_Main;
            Workbook = workbook;
            fdesign.MyComboBoxSelectionChanged += new FilterControlMulti.MyComboBoxSelectionChangedEventHandler(OnSelectionChanged);

            fdesign.MyButtonClick += new FilterControlMulti.MyButtonClickEventEventHandler(OnButtonClick);
            fdesign.MyCheckBoxClick += new FilterControlMulti.MyCheckBoxClickEventHandler(OnCheck);
            fdesign.MyRadioButtonClick += new FilterControlMulti.MyRadioButtonClickEventHandler(OnRadioClick);
            fdesign.MyTextBoxChange += new FilterControlMulti.MyTextBoxChangeEventHandler(OnTextBoxChange);
            IsInitialLoad = true;
            LoadingData();
            loadfilter();
            Factor_Rotation.IsChecked = true;
            Num_factor_extracted_radiobtn.IsChecked = true;
            Checkbox_Score_reverse_order.IsChecked = true;
            checkbox_check_result.IsChecked = true;
            radio_promax.IsChecked = true;
            numericUpDown.IsEnabled = false;
            rdo_Maximum_Likelihood.IsChecked = true;

            variableDictionary = Definiotion.VariableDictionary;

        }bool saveData = false;
        public void loadfilter()
        {
            try
            {
                var res = Util.ExcelUtil.GetWorkSheetBySheetName(Workbook, Constants.SheetType.sh_Sheet2); //GetWorkSheetByCodeName(Workbook, Constants.SheetCodeName.MultiVariate);
                Excel.Range crstart = res.Cells[5, 2];
                Excel.Range crlast = res.Cells[10, 235];
                Excel.Range rar = res.get_Range(crstart, crlast);
                // Excel.Range rar = res.UsedRange;
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
                                        CmbItm1 = index;
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
                                    if (tb.Name == "Combo_Conditional_Operator_1" && i == 1)
                                    {
                                        if (val[i, 3] != null) { saveData = true; CmbOpItm1 = val[i, 3]; }
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
        private void Multi_Process_Grid_CurrentCellChanged(object sender, EventArgs e)
        {
            var dg = (System.Windows.Controls.DataGrid)sender;
            ExpGrid = dg;
            dg.Focus();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // LoadingData();
            SourceVariableList = new ObservableCollection<psmQuestions>(sourceGetList.GetVariableFromList(Workbook, "List_Item_SAN"));
            psmQuestions insta = new psmQuestions();
            sourceGetList.RemoveItem(SourceVariableList, insta);


            try
            {
                var res = Util.ExcelUtil.GetWorkSheetBySheetName(Workbook, Constants.SheetType.sh_Sheet2); //GetWorkSheetByCodeName(Workbook, Constants.SheetCodeName.MultiVariate);
                Excel.Range crstart = res.Cells[5, 2];
                Excel.Range crlast = res.Cells[10, 235];
                Excel.Range rar = res.get_Range(crstart, crlast);
                // Excel.Range rar = res.UsedRange;
                var val = rar.Value;
                if (val[1, 1] != null)
                {
                    for (int i = 1; i <= val.GetLength(0); i++)
                    {
                        if (val[i, 1] != null)
                        {

                            string instruction = val[i, 5].ToString();


                            if (instruction == ProcessingMethod.FACTOR_ANALYSIS)
                            {
                                int colnum = 13;
                                ObservableCollection<psmQuestions> list = new ObservableCollection<psmQuestions>();
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

                                if (val[i, 7] == "1")
                                {
                                    radio_btn_varimax.IsChecked = true;
                                }
                                else if (val[i, 7] == "2")
                                {
                                    radio_promax.IsChecked = true;
                                }
                                else if (val[i, 7] == "3")
                                {
                                    rdio_none.IsChecked = true;
                                }

                                if (val[i, 8] == "1")
                                {
                                    rdo_Principal_Components.IsChecked = true;
                                }
                                else if (val[i, 8] == "2")
                                {
                                    rdo_Maximum_Likelihood.IsChecked = true;
                                }
                                if (val[i, 9] == "1")
                                {
                                    Num_factor_extracted_radiobtn.IsChecked = true;
                                }
                                else if (val[i, 9] == "2")
                                {
                                    rdo_adopt_factor.IsChecked = true;
                                }
                                if (val[i, 10] != null)
                                {
                                    numericUpDown.NUDTextBox.Text = val[i, 10].ToString();
                                }
                                if (val[i, 11] == "1")
                                {
                                    Checkbox_Score_reverse_order.IsChecked = true;
                                }
                                else if (val[i, 11] == "0")
                                {
                                    Checkbox_Score_reverse_order.IsChecked = false;
                                }
                                if (val[i, 12] == "0")
                                {
                                    checkbox_check_result.IsChecked = false;
                                }
                                else if (val[i, 12] == "1")
                                {
                                    checkbox_check_result.IsChecked = true;
                                }
                                while (colnum <= val.GetLength(1))
                                {
                                    if (val[i, colnum] != null)
                                    {

                                        if (SourceVariableList.Any(x => x.Variable == val[i, colnum].ToString()))
                                        {
                                            var find = SourceVariableList.FirstOrDefault(x => x.Variable == val[i, colnum].ToString());
                                            if (find != null)
                                            {
                                                list.Add(find);
                                            }
                                        }
                                    }
                                    colnum++;
                                }
                                if (list.Count > 0)
                                {
                                    foreach (psmQuestions item in list)
                                    {
                                        selectedList.Add(item);
                                    }
                                    foreach (psmQuestions crs in selectedList)
                                    {
                                        SourceVariableList.Remove(crs);
                                    }
                                    if (selectedList.Count > 2)
                                    {
                                        btn_Execute.IsEnabled = true;
                                        int select = 0;
                                        if (selectedList.Count < 51)
                                        {
                                            select = selectedList.Count - 1;
                                        }
                                        else
                                        {
                                            select = 50;
                                        }
                                        numericUpDown.loadData(1, (select), numericUpDown.NUDTextBox.Text);
                                        if (val[i, 10] != null)
                                        {
                                            numericUpDown.NUDTextBox.Text = val[i, 10].ToString();
                                        }
                                        if (Num_factor_extracted_radiobtn.IsChecked == true)
                                        {
                                            numericUpDown.IsEnabled = true;
                                        }
                                        else
                                        {
                                            numericUpDown.IsEnabled = false;
                                        }
                                    }
                                    selectedList.Sort(k => k.OrderNo);
                                    SourceVariableList.Sort(k => k.OrderNo);
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
            Selected_grid.ItemsSource = selectedList;
            btnactiveFunc();
        }


        private void Btn_close_Click(object sender, RoutedEventArgs e)
        {

            this.Close();

        }
        ObservableCollection<psmQuestions> selectedList = new ObservableCollection<psmQuestions>();
        ObservableCollection<psmQuestions> listdelete = new ObservableCollection<psmQuestions>();

        private void Btn_rgt_arrw_Click(object sender, RoutedEventArgs e)
        {
            if (Multi_Process_Grid.SelectedItems.Count > 0)
            {
                if (((Multi_Process_Grid.SelectedItems.Count) + (Selected_grid.Items.Count)) <= 100)
                {
                    foreach (psmQuestions crs in Multi_Process_Grid.SelectedItems)
                    {
                        selectedList.Add(crs);
                    }
                    foreach (psmQuestions crs in selectedList)
                    {
                        SourceVariableList.Remove(crs);
                    }

                    selectedList.Sort(k => k.OrderNo);
                    SourceVariableList.Sort(k => k.OrderNo);
                }
                else
                {
                    MessageBox.Show(LocalResource.MULTI_CLUSTOR_MSGBX_WITHIN_100, "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if (Multi_Process_Grid != null)
                {
                    ScrollViewer scrollViewer = GetVisualChild<ScrollViewer>(Multi_Process_Grid);
                    if (scrollViewer != null)
                    {
                        scrollViewer.ScrollToTop();
                    }
                }
                if (Selected_grid != null)
                {
                    ScrollViewer scrollViewer = GetVisualChild<ScrollViewer>(Selected_grid);
                    if (scrollViewer != null)
                    {
                        scrollViewer.ScrollToTop();
                    }
                }
            }
            btnactiveFunc();
        }

        private void Lbl_Single_Left_Arrow_Click(object sender, RoutedEventArgs e)
        {
            if (Selected_grid.SelectedItems.Count > 0)
            {
                foreach (psmQuestions crs in Selected_grid.SelectedItems)
                {
                    SourceVariableList.Add(crs);
                }
                foreach (psmQuestions crs in SourceVariableList)
                {
                    selectedList.Remove(crs);
                }

                selectedList.Sort(k => k.OrderNo);
                SourceVariableList.Sort(k => k.OrderNo);

                int select = 0;
                if (selectedList.Count < 51)
                {
                    select = selectedList.Count - 1;
                }
                else
                {
                    select = 50;
                }
                int txtVal = select;
                try
                {
                    int txt;
                    if (string.IsNullOrEmpty(numericUpDown.NUDTextBox.Text))
                    {
                        txt = 0;
                    }
                    else
                    {
                        txt = Convert.ToInt32(numericUpDown.NUDTextBox.Text);
                    }
                    numericUpDown.loadData(1, (select), txt >= select ? select.ToString() : numericUpDown.NUDTextBox.Text);
                    if (Selected_grid != null)
                    {
                        ScrollViewer scrollViewer = GetVisualChild<ScrollViewer>(Selected_grid);
                        if (scrollViewer != null)
                        {
                            scrollViewer.ScrollToTop();
                        }
                    }

                }
                catch { }
                if (Multi_Process_Grid != null)
                {
                    ScrollViewer scrollViewer = GetVisualChild<ScrollViewer>(Multi_Process_Grid);
                    if (scrollViewer != null)
                    {
                        scrollViewer.ScrollToTop();
                    }
                }
            }
            btnactiveFunc();
        }

        private void Factor_Rotation_Checked(object sender, RoutedEventArgs e)
        {
            if (Factor_Rotation.IsChecked == true)
            {
                groupbox_rotation_method.IsEnabled = true;

                txt_ortho_rotation.IsEnabled = true;
                txt_ortho_rotation.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));

                txt_orgue_rotation.IsEnabled = true;
                txt_orgue_rotation.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));

                grp_header.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
            }
            else
            {
                groupbox_rotation_method.IsEnabled = false;
                txt_ortho_rotation.IsEnabled = false;
                txt_ortho_rotation.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));

                txt_orgue_rotation.IsEnabled = false;
                txt_orgue_rotation.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));

                grp_header.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (Num_factor_extracted_radiobtn.IsChecked == false)
            {
                numericUpDown.IsEnabled = false;
            }
            else
            {
                if (Selected_grid.Items.Count > 2)
                {

                    numericUpDown.IsEnabled = true;
                }
            }

        }
        public void btnactiveFunc()
        {
            if (Selected_grid.Items.Count > 0)
            {
                btn_Execute.IsEnabled = true;

            }
            else
            {
                btn_Execute.IsEnabled = false;
            }



            if (Selected_grid.Items.Count >= 3)
            {
                if (string.IsNullOrEmpty(numericUpDown.NUDTextBox.Text))
                {
                    int select = 0;
                    if (selectedList.Count < 51)
                    {
                        select = selectedList.Count - 1;
                    }
                    else
                    {
                        select = 50;
                    }
                    numericUpDown.loadData(1, (select), "2");

                }
                if (Num_factor_extracted_radiobtn.IsChecked == true)
                {
                    numericUpDown.IsEnabled = true;
                    int select = 0;
                    if (selectedList.Count < 51)
                    {
                        select = selectedList.Count - 1;
                    }
                    else
                    {
                        select = 50;
                    }
                    numericUpDown.loadData(1, (select), numericUpDown.NUDTextBox.Text);

                }




            }
            else
            {
                numericUpDown.IsEnabled = false;
                numericUpDown.NUDTextBox.Text = null;
            }
        }
        private void Selected_grid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void Multi_Process_Grid_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Multi_Process_Grid_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {

        }

        private void Btn_Execute_Click(object sender, RoutedEventArgs e)
        {
            int txtval = 0;
            int.TryParse(numericUpDown.NUDTextBox.Text, out txtval);
            if ((radio_btn_varimax.IsChecked == true || radio_promax.IsChecked == true) && (txtval) == 1 && Num_factor_extracted_radiobtn.IsChecked==true)
            {
                MessageBox.Show(LocalResource.MULTI_FACTOR_ERROR_ROTATION, "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Selected_grid.Items.Count < 3)
            {
                MessageBox.Show(LocalResource.MULTI_FACTOR_MSGBX_SET_3OR_MORE, "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            else
            {
                if (!CheckRefine())
                {
                    return;
                }
                try
                {
                    Savesettings();
                    Close();
                }
                catch (Exception ex)
                {

                }
            }
            try
            {
                if (!MultivariateTable.CreateMultivariateTable(Workbook))
                {
                    MessageBox.Show("Error", "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch { }
            bool mlock = true;
            try
            {
                MainWindow.Show();
                string errmsg = string.Empty;
                using (new SingleGlobalInstance(10, Workbook)) //10ms timeout on global lock
                {
                    Factor factor = new Factor();
                    mlock = false;

                    System.ComponentModel.BackgroundWorker backgroundWorker1 = new System.ComponentModel.BackgroundWorker();

                    bool retvalue = false;
                    progress = new ProgressBar(LocalResource.TITLE_CROSS_TAB);
                    WindowInteropHelper wih = new WindowInteropHelper(progress);
                    var winh = new WindowInteropHelper(MainWindow);
                    wih.Owner = winh.Handle;
                    factor.OnWorkerComplete += new Factor.OnWorkerMethodCompleteDelegate(OnWorkerMethodCompletePSM);
                    backgroundWorker1.WorkerReportsProgress = true;
                    backgroundWorker1.WorkerSupportsCancellation = true;
                    backgroundWorker1.DoWork += new DoWorkEventHandler((object senderr, DoWorkEventArgs ee) =>

                   retvalue = factor.ExecuteFactor(Workbook, out errmsg)
                    );
                    backgroundWorker1.RunWorkerAsync();
                    progress.ShowDialog();
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (!string.IsNullOrEmpty(errmsg))
                {
                    //error msg
                    MessageBox.Show(errmsg, "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error);

                    Factor_Analysis f = new Factor_Analysis(Workbook, MainWindow);

                    MainWindow.Hide();
                    try
                    {
                        f.ShowDialog();
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
                if (mlock)
                {
                    Factor_Analysis f = new Factor_Analysis(Workbook, MainWindow);

                    MainWindow.Hide();
                    try
                    {
                        f.ShowDialog();
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
        public void Savesettings()
        {
            string[] paramList = new string[Selected_grid.Items.Count];
            for (int i = 0; i < Selected_grid.Items.Count; i++)
            {
                psmQuestions dat = (psmQuestions)Selected_grid.Items[i];
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
            int colCount = (12) + (Selected_grid.Items.Count);
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
                                    SelectedVariable[i, j] = ProcessingMethod.FACTOR_ANALYSIS;
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
                        case 6:
                            if (!string.IsNullOrEmpty(SelectedVariable[i, 4]) && (SelectedVariable[i, 4] == ProcessingMethod.FACTOR_ANALYSIS))
                            {
                                if (radio_btn_varimax.IsChecked == true)
                                {
                                    SelectedVariable[i, j] = "1";
                                }
                                else if (radio_promax.IsChecked == true)
                                {
                                    SelectedVariable[i, j] = "2";
                                }
                                else
                                {
                                    SelectedVariable[i, j] = "3";
                                }
                            }
                            break;
                        case 7://no substitute variable
                            if (!string.IsNullOrEmpty(SelectedVariable[i, 4]) && (SelectedVariable[i, 4] == ProcessingMethod.FACTOR_ANALYSIS))
                            {
                                if (rdo_Principal_Components.IsChecked == true)
                                {
                                    SelectedVariable[i, j] = "1";
                                }
                                else
                                {
                                    SelectedVariable[i, j] = "2";
                                }
                            }
                            break;
                        case 8:
                            if (!string.IsNullOrEmpty(SelectedVariable[i, 4]) && (SelectedVariable[i, 4] == ProcessingMethod.FACTOR_ANALYSIS))
                            {
                                if (Num_factor_extracted_radiobtn.IsChecked == true)
                                {
                                    SelectedVariable[i, j] = "1";
                                }
                                else if (rdo_adopt_factor.IsChecked == true)
                                {
                                    SelectedVariable[i, j] = "2";
                                }

                            }
                            break;
                        case 9:
                            SelectedVariable[i, j] = numericUpDown.NUDTextBox.Text;
                            //no substitute operator
                            break;

                        case 10:
                            if (!string.IsNullOrEmpty(SelectedVariable[i, 4]) && (SelectedVariable[i, 4] == ProcessingMethod.FACTOR_ANALYSIS))
                            {
                                if (Checkbox_Score_reverse_order.IsChecked == true)
                                {
                                    SelectedVariable[i, j] = "1";

                                }
                                else { SelectedVariable[i, j] = "0"; }
                            }

                            break;

                        case 11:
                            if (!string.IsNullOrEmpty(SelectedVariable[i, 4]) && (SelectedVariable[i, 4] == ProcessingMethod.FACTOR_ANALYSIS))
                            {
                                if (checkbox_check_result.IsChecked == true)
                                {
                                    SelectedVariable[i, j] = "1";

                                }
                                else { SelectedVariable[i, j] = "0"; }
                            }
                            break;
                        default://parameter list
                            if (!string.IsNullOrEmpty(SelectedVariable[i, 4]) && (SelectedVariable[i, 4] == ProcessingMethod.FACTOR_ANALYSIS))
                            {
                                SelectedVariable[i, j] = paramList[param];
                                param++;
                            }
                            break;

                    }
                }
            }

            sourceGetList.WriteFilterrSettings(Workbook, SelectedVariable, 10, 5);
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
            else if (grid != null && e.Key == Key.Tab && grid.Name is "Selected_grid")
            {
                e.Handled = true;
                rdio_none.Focus();
            }
            QC4Common.Util.GridCommonFunc.Arrow(sender, e);
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
            //if ((sender as TabControl).SelectedIndex == 1 && FisrtTabFocus)
            //{
            //    Page_GT_Summary.Focus();
            //    FisrtTabFocus = false;
            //}
            //else if ((sender as TabControl).SelectedIndex != 1)
            //{
            //    FisrtTabFocus = true;
            //}
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                if (Page_GT_Summary.IsFocused)
                {
                    Style style = Application.Current.FindResource("MyFocusVisual") as Style;
                    Multi_Process_Grid.FocusVisualStyle = null;
                    Multi_Process_Grid.Focus();
                    if (Multi_Process_Grid.SelectedIndex < 0 && Multi_Process_Grid.Items.Count > 1)
                    {
                        Multi_Process_Grid.SelectedIndex = 0;
                    }
                    e.Handled = true;
                }
                else if (Lbl_Single_Left_Arrow.IsFocused)
                {
                    if (Selected_grid.Items.Count > 0)
                    {
                        Style style = Application.Current.FindResource("MyFocusVisual") as Style;
                        Selected_grid.FocusVisualStyle = null;
                        Selected_grid.Focus();
                        if (Selected_grid.SelectedIndex < 0 && Selected_grid.Items.Count > 0)
                        {
                            Selected_grid.SelectedIndex = 0;
                        }
                        e.Handled = true;
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

            try
            {
                Savesettings();
            }
            catch (Exception ex)
            {

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

        private void Rdio_none_Click(object sender, RoutedEventArgs e)
        {
            rdo_Maximum_Likelihood.IsChecked = true;
        }

        private void Radio_btn_varimax_Click(object sender, RoutedEventArgs e)
        {
            rdo_Principal_Components.IsChecked = true;
        }

        private void Radio_promax_Click(object sender, RoutedEventArgs e)
        {
            rdo_Maximum_Likelihood.IsChecked = true;
        }
    }
}
