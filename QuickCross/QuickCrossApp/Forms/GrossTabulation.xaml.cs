using Vb = Microsoft.VisualBasic;
using FilterSettingsView;
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
using Qc4Launcher.Util;
using excel = Microsoft.Office.Interop.Excel;
using Qc4Launcher.Classes;
using System.Collections.ObjectModel;
using static FilterSettingsView.FilterSettingsClass;
using System.Text.RegularExpressions;
using static ExcelAddIn.Common.Constants;
using Qc4Launcher.Logic;
using Qc4Launcher.Logic.Gross_Tabulation;
using QC4Common.Model;
using QC4Common.Validation;
using System.Windows.Threading;
using System.Threading;
using log4net;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Qc4Launcher.Forms
{
    /// <summary>
    /// Interaction logic for GrossTabulation.xaml
    /// </summary>
    public partial class GrossTabulation : Window
    {
        excel.Workbook Workbook;
        bool ISWBAdded = false;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private ComboBox CmbClassify;
        private CheckBox ChkCriteria;
        private ComboBox CmbCriteria;
        public excel.Shape OptionSettingsMsg { get; set; }

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
        bool Combo_Conditional_Initial1 = false;
        bool Combo_Conditional_Initial2 = false;
        bool Combo_Conditional_Initial3 = false;
        bool Combo_Conditional_Initial4 = false;
        bool Combo_Conditional_Initial5 = false;
        bool CloseNotFromBtn = true;

        private string SelectedFile = "";
        bool IsInitialLoad = false;

        private ObservableCollection<DataExport> _dataExport_LBVariablesToExport = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _dataExport_ListBoxCommonCopy = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD1 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD2 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD3 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD4 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD5 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD6 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablnumeric = new ObservableCollection<DataExport>();

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

        private List<string> elementsInSheet = new List<string>();
        private int lastIndexinAdvancedSettingsExcelSheet = 0;

        private Dictionary<string, String> ReadValueFromExcel = new Dictionary<string, String>();
        private bool checkflag = false;
        FilterSettings fs = new FilterSettings();
        FilterSettingsClass fsc = new FilterSettingsClass();
        bool weightbak;
        public static bool weighted;
        public bool filtersettingcheckflag;

        public GrossTabulation(excel.Workbook workbook, string filePath)
        {
            InitializeComponent();
            FilterControlDesign fdesign = new FilterControlDesign();
            this.GridContainer.Children.Add(fdesign);
            Workbook = workbook;
            SelectedFile = filePath;
            fdesign.MyComboBoxSelectionChanged += new FilterControlDesign.MyComboBoxSelectionChangedEventHandler(OnSelectionChanged);
            fdesign.MyButtonClick += new FilterControlDesign.MyButtonClickEventEventHandler(OnButtonClick);
            fdesign.MyCheckBoxClick += new FilterControlDesign.MyCheckBoxClickEventHandler(OnCheck);
            fdesign.MyRadioButtonClick += new FilterControlDesign.MyRadioButtonClickEventHandler(OnRadioClick);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 25);
            try
            {
                if (this.IsActive)
                {
                    this.Focus();
                }
                checkflag = false;
                IsInitialLoad = true;
                LoadingData();

                LoadInitialValuesToTabulationControls();

                foreach (ComboBox tb
                  in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
                {
                    GetName_Page_Refine(tb);
                }
                foreach (RadioButton tb
                    in FindControls.FindLogicalChildren<RadioButton>(this.Page_Refine))
                {
                    GetName_Page_Refine(tb);
                }

                foreach (TextBox tb
                    in FindControls.FindLogicalChildren<TextBox>(this.Page_Refine))
                {
                    GetName_Page_Refine(tb);
                }
                foreach (CheckBox tb
                    in FindControls.FindLogicalChildren<CheckBox>(this.Page_Refine))
                {
                    GetName_Page_Refine(tb);
                }

                foreach (ComboBox tb
                in FindControls.FindLogicalChildren<ComboBox>(this.Page_Output))
                {
                    GetName_Page_Output(tb);
                }

                foreach (TextBox tb
                    in FindControls.FindLogicalChildren<TextBox>(this.Page_Output))
                {
                    GetName_Page_Output(tb);
                }
                foreach (CheckBox tb
                    in FindControls.FindLogicalChildren<CheckBox>(this.Page_Output))
                {
                    GetName_Page_Output(tb);
                }

                elementsInSheet.Clear();
                var SettingSheet = ExcelUtil.GetWorkSheetByCodeName(Workbook, Constants.SheetCodeName.DetailsSetting);
                excel.Range last = SettingSheet.Cells.SpecialCells(excel.XlCellType.xlCellTypeLastCell, Type.Missing);
                int rcell = SettingSheet.UsedRange.Rows.Count;
                rcell = rcell + controlObj.Count;
                rcell = rcell + 2;
                string ragecell = "C" + rcell.ToString();
                excel.Range rar = SettingSheet.get_Range("A1", ragecell);
                var obj = rar.Value;
                int rowvalue = rar.Rows.Count + 1;

                for (int i = 2; i < rowvalue; i++)
                {
                    if (obj[i, 1] != null)
                        elementsInSheet.Add((obj[i, 1]));
                    else
                    {
                        lastIndexinAdvancedSettingsExcelSheet = i;
                        break;
                    }
                    lastIndexinAdvancedSettingsExcelSheet = i;
                }

                if (!ISWBAdded)
                {
                    _qstnvariablnumeric.Insert(0, DataExportObjectCreator());
                    Combo_Summary_WeightBack.SelectedIndex = 0;
                }
                bool WeightBac_Selected_Index = false;

                for (int i = 0; i < elementsInSheet.Count; i++)
                {
                    if (controlObj.ContainsKey(elementsInSheet[i]))
                    {
                        if (controlObj[elementsInSheet[i]] is ComboBox)
                        {
                            string s = elementsInSheet[i];
                            if (s.Contains("F_Cr_Cross_AddUp_" + GT.ComboBoxTypeName))
                            {
                                string sval = null == obj[i + 2, 2] ? "" : obj[i + 2, 2];
                                int b;
                                if (s.Contains("Combo_Summary_WeightBack"))
                                {
                                    if (sval != "")
                                        WeightBac_Selected_Index = true;
                                }
                                if (int.TryParse(sval, out b))
                                {
                                    int val1 = null == obj[i + 2, 2] ? 0 : Convert.ToInt32(obj[i + 2, 2]);

                                    ((ComboBox)controlObj[elementsInSheet[i]]).SelectedIndex = val1;
                                    ((ComboBox)controlObj[elementsInSheet[i]]).IsEnabled = true;
                                }
                                string val = null == obj[i + 2, 2] ? "" : obj[i + 2, 2].ToString();
                                DataExport target = null;
                                int index = 0;
                                target = _qstnvariablnumeric.Where(z => z.QuestionVariable == val).FirstOrDefault();
                                index = target == null ? 0 : _qstnvariablnumeric.IndexOf(target);

                                if (index >= 0)
                                {
                                    ((ComboBox)controlObj[elementsInSheet[i]]).SelectedIndex = index;
                                }
                                else
                                {
                                    ((ComboBox)controlObj[elementsInSheet[i]]).Text = val;
                                }
                            }
                            else if (s.Contains(GT.FormControlPrefixAddup + GT.ComboBoxTypeName))
                            {
                                string sval = null == obj[i + 2, 2] ? "" : obj[i + 2, 2];
                                int b;
                                if (s.Contains("Combo_Summary_WeightBack"))
                                {
                                    if (sval != "")
                                        WeightBac_Selected_Index = true;
                                }
                                if (int.TryParse(sval, out b))
                                {
                                    int val = null == obj[i + 2, 2] ? 0 : Convert.ToInt32(obj[i + 2, 2]);

                                    ((ComboBox)controlObj[elementsInSheet[i]]).SelectedIndex = val;
                                    ((ComboBox)controlObj[elementsInSheet[i]]).IsEnabled = true;
                                }
                                else
                                {

                                    ComboBox cmb = ((System.Windows.Controls.ComboBox)controlObj[elementsInSheet[i]]);

                                    string val = null == obj[i + 2, 2] ? "" : obj[i + 2, 2].ToString();
                                    DataExport target = null;
                                    int index = 0;
                                    bool enable = true;
                                    if (s.Contains("Combo_Classify_Item"))
                                    {
                                        target = _qstnvariablDD1.Where(z => z.QuestionVariable == val).FirstOrDefault();
                                        index = target == null ? 0 : _qstnvariablDD1.IndexOf(target);
                                        cmb.SelectedIndex = index;
                                        cmb.IsEnabled = true;
                                        CmbClsy = index;
                                    }
                                    else if (s.Contains("Combo_Conditional_Item_1"))
                                    {
                                        target = _qstnvariablDD2.Where(z => z.QuestionVariable == val).FirstOrDefault();
                                        index = target == null ? 0 : _qstnvariablDD2.IndexOf(target);
                                        CmbItm1 = index;
                                        if (index == 0)
                                        {
                                            cmb.IsEnabled = false;
                                            cmb.SelectedValue = 0;
                                        }
                                        if (index > 0)
                                        {
                                            cmb.SelectedValue = _qstnvariablDD2[index];
                                            cmb.IsEnabled = true;
                                            cmb.Foreground = new SolidColorBrush(Colors.Black);
                                        }
                                    }
                                    else if (s.Contains("Combo_Conditional_Item_2"))
                                    {
                                        target = _qstnvariablDD3.Where(z => z.QuestionVariable == val).FirstOrDefault();
                                        index = target == null ? 0 : _qstnvariablDD3.IndexOf(target);
                                        CmbItm2 = index;
                                        if (index == 0)
                                        {
                                            cmb.IsEnabled = false;
                                            cmb.SelectedValue = 0;
                                        }
                                        if (index > 0)
                                        {
                                            cmb.SelectedValue = _qstnvariablDD3[index];
                                            cmb.IsEnabled = true;
                                            cmb.Foreground = new SolidColorBrush(Colors.Black);
                                        }
                                    }
                                    else if (s.Contains("Combo_Conditional_Item_3"))
                                    {
                                        target = _qstnvariablDD4.Where(z => z.QuestionVariable == val).FirstOrDefault();
                                        index = target == null ? 0 : _qstnvariablDD4.IndexOf(target);
                                        CmbItm3 = index;
                                        if (index == 0)
                                        {
                                            cmb.IsEnabled = false;
                                            cmb.SelectedValue = 0;
                                        }
                                        if (index > 0)
                                        {
                                            cmb.SelectedValue = _qstnvariablDD4[index];
                                            cmb.IsEnabled = true;
                                            cmb.Foreground = new SolidColorBrush(Colors.Black);
                                        }
                                    }
                                    else if (s.Contains("Combo_Conditional_Item_4")) 
                                    {
                                        target = _qstnvariablDD5.Where(z => z.QuestionVariable == val).FirstOrDefault();
                                        index = target == null ? 0 : _qstnvariablDD5.IndexOf(target);
                                        CmbItm4 = index;
                                        if (index == 0)
                                        {
                                            cmb.IsEnabled = false;
                                            cmb.SelectedValue = 0;
                                        }
                                        if (index > 0)
                                        {
                                            cmb.SelectedValue = _qstnvariablDD5[index];
                                            cmb.IsEnabled = true;
                                            cmb.Foreground = new SolidColorBrush(Colors.Black);
                                        }
                                    }
                                    else if (s.Contains("Combo_Conditional_Item_5")) 
                                    {
                                        target = _qstnvariablDD6.Where(z => z.QuestionVariable == val).FirstOrDefault();
                                        index = target == null ? 0 : _qstnvariablDD6.IndexOf(target);
                                        CmbItm5 = index;
                                        if (index == 0)
                                        {
                                            cmb.IsEnabled = false;
                                            cmb.SelectedValue = 0;
                                        }
                                        if (index > 0)
                                        {
                                            cmb.SelectedValue = _qstnvariablDD6[index];
                                            cmb.IsEnabled = true;
                                            cmb.Foreground = new SolidColorBrush(Colors.Black);
                                        }
                                    }
                                    else if (s.Contains("Combo_Conditional_Operator_1") || s.Contains("Combo_Conditional_Operator_2") || s.Contains("Combo_Conditional_Operator_3") || s.Contains("Combo_Conditional_Operator_4") || s.Contains("Combo_Conditional_Operator_5"))// 
                                    {
                                        ((ComboBox)controlObj[elementsInSheet[i]]).IsEnabled = true;
                                        switch (((System.Windows.Controls.ComboBox)controlObj[elementsInSheet[i]]).Name)
                                        {
                                            case "Combo_Conditional_Operator_1":
                                                CmbOpItm1 = val;
                                                break;
                                            case "Combo_Conditional_Operator_2":
                                                CmbOpItm2 = val;
                                                break;
                                            case "Combo_Conditional_Operator_3":
                                                CmbOpItm3 = val;
                                                break;
                                            case "Combo_Conditional_Operator_4":
                                                CmbOpItm4 = val;
                                                break;
                                            case "Combo_Conditional_Operator_5":
                                                CmbOpItm5 = val;
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        target = _qstnvariablnumeric.Where(z => z.QuestionVariable == val).FirstOrDefault();
                                        index = target == null ? 0 : _qstnvariablnumeric.IndexOf(target);
                                    }
                                    if (index >= 0)
                                    {
                                        ((ComboBox)controlObj[elementsInSheet[i]]).SelectedIndex = index;
                                        ((ComboBox)controlObj[elementsInSheet[i]]).IsEnabled = enable;
                                    }
                                    else
                                    {
                                        ((ComboBox)controlObj[elementsInSheet[i]]).Text = val;
                                        ((ComboBox)controlObj[elementsInSheet[i]]).IsEnabled = enable;
                                    }
                                }
                            }
                            else
                            {
                                string val = null == obj[i + 2, 2] ? "" : obj[i + 2, 2].ToString();
                                ((ComboBox)controlObj[elementsInSheet[i]]).Text = val;
                                ((ComboBox)controlObj[elementsInSheet[i]]).IsEnabled = true;
                            }
                        }

                        if (controlObj[elementsInSheet[i]] is TextBox)
                        {
                            if (Convert.ToString(obj[i + 2, 1]) == "F_Cr_Cross_AddUp_Text_Summary_Change_Hyoutou_P" && Convert.ToString(obj[i + 2, 2]) == QC4Common.Common.Constants.CRLFchar)
                            {
                                ((TextBox)controlObj[elementsInSheet[i]]).Text = LocalResource.CROSS_TXT_BX_ALL;
                            }
                            else if (Convert.ToString(obj[i + 2, 1]) == "F_Cr_Cross_AddUp_Text_Summary_Change_Hyosoku_P" && Convert.ToString(obj[i + 2, 2]) == QC4Common.Common.Constants.CRLFchar)
                            {
                                ((TextBox)controlObj[elementsInSheet[i]]).Text = LocalResource.CROSS_TXT_BX_ALL;
                            }
                            else if (Convert.ToString(obj[i + 2, 1]) == "F_Cr_Cross_AddUp_Text_Summary_Change_Non_P" && Convert.ToString(obj[i + 2, 2]) == QC4Common.Common.Constants.CRLFchar)
                            {
                                ((TextBox)controlObj[elementsInSheet[i]]).Text = LocalResource.CROSS_TXT_BX_NOANSWER;
                            }
                            else
                                ((TextBox)controlObj[elementsInSheet[i]]).Text = obj[i + 2, 2];
                        }

                        if (controlObj[elementsInSheet[i]] is RadioButton)
                        {
                            string str = null == obj[i + 2, 2] ? "" : obj[i + 2, 2].ToString();
                            if (str.Equals("True"))
                            {
                                ((RadioButton)controlObj[elementsInSheet[i]]).IsChecked = true;

                            }
                            else
                                ((RadioButton)controlObj[elementsInSheet[i]]).IsChecked = false;
                        }

                        if (controlObj[elementsInSheet[i]] is CheckBox)
                        {
                            string s = elementsInSheet[i];
                            if(s.Contains("OutputUnweightbackedTotalCheck"))
                            {
                                if (s.Equals("F_Gt_GT_AddUp_OutputUnweightbackedTotalCheck_P"))
                                {
                                    string str = null == obj[i + 2, 2] ? "" : obj[i + 2, 2].ToString();

                                    if (str.ToLower().Equals("true"))
                                    {
                                        ((CheckBox)controlObj[elementsInSheet[i]]).IsChecked = true;
                                    }
                                    else if (str.ToLower().Equals("false"))
                                    {
                                        ((CheckBox)controlObj[elementsInSheet[i]]).IsChecked = false;
                                    }
                                }
                            }
                            else
                            {
                                string str = null == obj[i + 2, 2] ? "" : obj[i + 2, 2].ToString();

                                if (str.ToLower().Equals("true"))
                                {
                                    ((CheckBox)controlObj[elementsInSheet[i]]).IsChecked = true;
                                }
                                else
                                {
                                    ((CheckBox)controlObj[elementsInSheet[i]]).IsChecked = false;
                                }
                            }
                        }

                    }
                }
                if (ISWBAdded && !WeightBac_Selected_Index)
                {
                    _qstnvariablnumeric.Insert(0, DataExportObjectCreator());
                    Combo_Summary_WeightBack.SelectedIndex = 0;
                }
                bool settingFirst = false;
                GetValuesOfControls();
                String[] keys = controlObj.Keys.ToArray<String>();

                for (int i = 0; i < keys.Count(); i++)
                {
                    if (!(elementsInSheet.Contains(keys[i])))
                    {
                        obj[lastIndexinAdvancedSettingsExcelSheet, 1] = keys[i];
                        obj[lastIndexinAdvancedSettingsExcelSheet, 2] = ReadValueFromExcel[keys[i]];
                        ++lastIndexinAdvancedSettingsExcelSheet;
                        settingFirst = true;
                    }
                }
                if (settingFirst)
                {
                    rar.Value2 = obj;

                }
                checkflag = true;

                Check_Summary_WeightBack_Check();

                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                readEXcel();

                foreach (CheckBox tb
                 in FindControls.FindLogicalChildren<CheckBox>(this.Page_Refine))
                {
                    if (tb.Name == "Check_Refine_Condition")
                    {
                        ChkCriteria = tb;
                        filtersettingcheckflag = (bool)tb.IsChecked;
                        break;
                    }
                }
                FilterControlDesign.MyCheckBoxClickEventArgs cargs = new FilterControlDesign.MyCheckBoxClickEventArgs();
                cargs.Check = filtersettingcheckflag;
                cargs.sendr = "Check_Refine_Condition";
                this.OnCheck(this, cargs);

                UpdateRatioSettings();

            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void GetNType()
        {
            for (int i = 0; i < _qstnvariablDD2.Count; i++)
            {
                if (_qstnvariablDD2[i].QuestionVariableType.Contains(AnswerType.N) && _qstnvariablDD2[i].QuestionVariable != "SAMPLEID")
                {
                    _qstnvariablnumeric.Add(_qstnvariablDD2[i]);
                }
            }
            var SettingSheet = ExcelUtil.GetWorkSheetByCodeName(Workbook, Constants.SheetCodeName.Setting);
            excel.Range range1 = SettingSheet.UsedRange;
            excel.Range starting = SettingSheet.Cells[2, 9];
            excel.Range ending = ExcelUtil.EndxlUp(starting).Offset[1, 3];
            excel.Range weightrange = SettingSheet.get_Range(starting, ending);
            excel.Range rar = SettingSheet.get_Range(starting, ending);
            object[,] obj = rar.Value;
            if (obj != null && obj.GetUpperBound(0) > 0 && obj.GetUpperBound(1) > 1 && obj[1, 1] != null && obj[1, 1].ToString() == Constants.WeightBack
                && obj[1, 2] != null && obj[1, 1].ToString().Length > 0)
            {
                DataExport d = new DataExport();
                d.Question = Constants.WeightBack;
                d.QuestionVariable = Constants.WeightBack;
                d.QuestionVariableType = Constants.WeightBack;
                d.QuestionChoiceNo = Constants.WeightBack;
                d.QuestionIndex = 0;
                _qstnvariablnumeric.Add(d);
                ISWBAdded = true;
            }
        }
        private void LoadingData()
        {
            fs.LoadingData(out _dataExport_LBVariablesToExport, out _qstnvariablDD1, out _qstnvariablDD2);
            _qstnvariablDD3 = new ObservableCollection<DataExport>(_qstnvariablDD2);
            _qstnvariablDD4 = new ObservableCollection<DataExport>(_qstnvariablDD2);
            _qstnvariablDD5 = new ObservableCollection<DataExport>(_qstnvariablDD2);
            _qstnvariablDD6 = new ObservableCollection<DataExport>(_qstnvariablDD2);
            GetNType();

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

        private void LoadInitialValuesToTabulationControls()
        {

            this.Check_Summary_WeightBack.IsChecked = false;
            this.OutputUnweightbackedTotalCheck.IsChecked = true;

            this.Check_All_Base.IsChecked = false;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void textBox_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }
        private void text_LostFocus(object sender, RoutedEventArgs e)
        {
            int res = 0;
            if (((TextBox)sender).Name == "Text_Outputs_Ratios_EqualToLessThan")
            {

                if (this.Text_Outputs_Ratios_EqualToLessThan.Text != null && this.Text_Outputs_Ratios_EqualToLessThan.Text != "" && (int.TryParse(this.Text_Outputs_Ratios_EqualToLessThan.Text, out res)))
                {
                    int numval = Convert.ToInt32(this.Text_Outputs_Ratios_EqualToLessThan.Text);
                    if (numval > Constants.MarkingMaxValue)
                    {
                        this.Text_Outputs_Ratios_EqualToLessThan.Text = Constants.MarkingMaxValue.ToString();
                    }
                }
                else
                {
                    this.Text_Outputs_Ratios_EqualToLessThan.Text = Constants.MarkingMinValue.ToString();
                }
            }
        }

        public void GetName_Page_Output(Control x)
        {
            if (x is ComboBox)
            {
                if (((ComboBox)x).Name == "Combo_Summary_WeightBack")
                    controlObj.Add(Name = "F_Cr_Cross_AddUp_" + ((ComboBox)x).Name + GT.FormControlPostFixPro, ((ComboBox)x));
                else
                    controlObj.Add(Name = GT.FormControlPrefixAddup + ((ComboBox)x).Name + GT.FormControlPostFixPro, ((ComboBox)x));
            }
            else if (x is TextBox)
            {
                if (((TextBox)x).Name == "Text_Summary_Change_Hyoutou" || ((TextBox)x).Name == "Text_Summary_Change_Non")
                    controlObj.Add(Name = "F_Cr_Cross_AddUp_" + ((TextBox)x).Name + "_P", ((TextBox)x));
                else
                    controlObj.Add(Name = GT.FormControlPrefixAddup + ((TextBox)x).Name + "_P", ((TextBox)x));
            }
            else if (x is CheckBox)
            {
                if (((CheckBox)x).Name == "Check_Summary_WeightBack")
                    controlObj.Add(Name = "F_Cr_Cross_AddUp_" + ((CheckBox)x).Name + GT.FormControlPostFixPro, ((CheckBox)x));
                else
                    controlObj.Add(Name = GT.FormControlPrefixAddup + ((CheckBox)x).Name + GT.FormControlPostFixPro, ((CheckBox)x));
            }
        }

        private void BTN_GROSSTABULATE_Click(object sender, RoutedEventArgs e)
        {
            CloseNotFromBtn = false;

            this.Close();
        }

        private void SaveSettings()
        {
            var SettingSheet = ExcelUtil.GetWorkSheetByCodeName(Workbook, Constants.SheetCodeName.DetailsSetting);
            int rcell = SettingSheet.UsedRange.Rows.Count;
            rcell = rcell + 3;
            string ragecell = "B" + rcell.ToString();
            excel.Range rar = SettingSheet.get_Range("A1", ragecell);
            var obj = rar.Value;
            int rowvalue = rar.Rows.Count;
            for (int i = 2; i < rowvalue; i++)
            {
                if (obj[i, 1] != null)
                {
                    if (ReadValueFromExcel.ContainsKey(elementsInSheet[i - 2]))
                    {
                        if (obj[i, 1].ToString() == "F_Cr_Cross_AddUp_Text_Summary_Change_Hyoutou_P" || obj[i, 1].ToString() == "F_Cr_Cross_AddUp_Text_Summary_Change_Non_P")
                            obj[i, 2] = obj[i, 2] == QC4Common.Common.Constants.CRLFchar ? QC4Common.Common.Constants.CRLFchar : ReadValueFromExcel[obj[i, 1]];
                        else
                            obj[i, 2] = ReadValueFromExcel[obj[i, 1]];

                        if (elementsInSheet[i - 2] == "F_Cr_Cross_AddUp_Text_Summary_Change_Hyoutou_P" || elementsInSheet[i - 2] == "F_Cr_Cross_AddUp_Text_Summary_Change_Non_P")
                        {
                            for (int j = 2; j < rowvalue; j++)
                            {
                                if (obj[j, 1] != null && obj[j, 1].ToString() == "F_Cr_Cross_AddUp_Text_Summary_Change_Hyoutou_P" && elementsInSheet[i - 2] == "F_Cr_Cross_AddUp_Text_Summary_Change_Hyoutou_P")
                                {
                                    obj[j, 2] = ReadValueFromExcel[obj[i, 1]] == LocalResource.CROSS_TXT_BX_ALL ? obj[j, 2] : ReadValueFromExcel[obj[i, 1]];
                                }
                                else if (obj[j, 1] != null && obj[j, 1].ToString() == "F_Cr_Cross_AddUp_Text_Summary_Change_Non_P" && elementsInSheet[i - 2] == "F_Cr_Cross_AddUp_Text_Summary_Change_Non_P")
                                {
                                    obj[j, 2] = ReadValueFromExcel[obj[i, 1]] == LocalResource.CROSS_TXT_BX_NOANSWER ? obj[j, 2] : ReadValueFromExcel[obj[i, 1]];
                                }
                            }
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            rar.Value2 = obj;
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
        }

        private bool CheckRefine()
        {
            if (Check_Summary_WeightBack.IsChecked == true && Combo_Summary_WeightBack.Text == "")
            {
                MessageDialog.ErrorOk(LocalResource.GT_NO_WEIGHTING, this);
                return false;
            }
            if (Check_Summary_WeightBack.IsChecked == true && QC4Common.DB.DBHelper.checkNegetiveNumberInData(Workbook, Combo_Summary_WeightBack.Text))
            {
                MessageDialog.ErrorOk(LocalResource.WB_NEGATIVE_VALIDATION, this);
                return false;
            }
            bool validpath = false;
            bool combohasvalue = false;
            foreach (ComboBox cb in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
            {
                if (cb.Name == "Combo_Classify_Item")
                {
                    if (cb.SelectedItem != null && (((DataExport)cb.SelectedItem).QuestionVariable != null &&
                   ((DataExport)cb.SelectedItem).QuestionVariable != ""))
                        combohasvalue = true;
                    break;
                }
            }
            foreach (TextBox tb in FindControls.FindLogicalChildren<TextBox>(this.Page_Refine))
            {
                if (tb.Name == "Combo_Classify_FolderPath" && (tb.Text != "" && tb.Text != null))
                {
                    validpath = true;
                    break;
                }
            }
            if (validpath == false && combohasvalue == true)
            {
                MessageDialog.ErrorOk(LocalResource.ERR_MSG_CROSS_PATH_NULL, this);
                CloseNotFromBtn = true;
                return false;
            }
            bool Check_Refine_Condition = false;
            foreach (CheckBox tb
                in FindControls.FindLogicalChildren<CheckBox>(this.Page_Refine))
            {
                if (tb.Name == "Check_Refine_Condition")
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
                    MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_FILTER_CRITERIA_VARIABLE_MISSING, 1), this);//string.Format(AddinResource.DECST_PARAMN_TOOLTIPHEADER, i)
                    return false;
                }
                else
                {
                    wMsg = GetWMsg(NumberCheck.CheckNotOperator(value, operatorr, variabletype));
                    if (wMsg != "")
                    {
                        MessageDialog.ErrorOk(string.Format(wMsg, 1), this);
                        return false;
                    }
                    wMsg = GetWMsg(NumberCheck.CheckValueAgainstOP(value, operatorr, variabletype));
                    if (wMsg != "")
                    {
                        MessageDialog.ErrorOk(string.Format(wMsg, 1), this);
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
                                    MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_DATAEXPORT_SET_NUMERIC, 1), this);
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
                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_FILTER_CRITERIA_NOT_OP, 1), this);
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
                    MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_FILTER_CRITERIA_VARIABLE_MISSING, 2), this);
                    return false;
                }
                else
                {
                    wMsg = GetWMsg(NumberCheck.CheckNotOperator(value, operatorr, variabletype));
                    if (wMsg != "")
                    {
                        MessageDialog.ErrorOk(string.Format(wMsg, 2), this);
                        return false;
                    }
                    wMsg = GetWMsg(NumberCheck.CheckValueAgainstOP(value, operatorr, variabletype));
                    if (wMsg != "")
                    {
                        MessageDialog.ErrorOk(string.Format(wMsg, 2), this);
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
                                    MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_DATAEXPORT_SET_NUMERIC, 2), this);
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
                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_FILTER_CRITERIA_NOT_OP, 2), this);
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
                    MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_FILTER_CRITERIA_VARIABLE_MISSING, 3), this);
                    return false;
                }
                else
                {
                    wMsg = GetWMsg(NumberCheck.CheckNotOperator(value, operatorr, variabletype));
                    if (wMsg != "")
                    {
                        MessageDialog.ErrorOk(string.Format(wMsg, 3), this);
                        return false;
                    }
                    wMsg = GetWMsg(NumberCheck.CheckValueAgainstOP(value, operatorr, variabletype));
                    if (wMsg != "")
                    {
                        MessageDialog.ErrorOk(string.Format(wMsg, 3), this);
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
                                    MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_DATAEXPORT_SET_NUMERIC, 3), this);
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
                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_FILTER_CRITERIA_NOT_OP, 3), this);
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
                    MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_FILTER_CRITERIA_VARIABLE_MISSING, 4), this);
                    return false;
                }
                else
                {
                    wMsg = GetWMsg(NumberCheck.CheckNotOperator(value, operatorr, variabletype));
                    if (wMsg != "")
                    {
                        MessageDialog.ErrorOk(string.Format(wMsg, 4), this);
                        return false;
                    }
                    wMsg = GetWMsg(NumberCheck.CheckValueAgainstOP(value, operatorr, variabletype));
                    if (wMsg != "")
                    {
                        MessageDialog.ErrorOk(string.Format(wMsg, 4), this);
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
                                    MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_DATAEXPORT_SET_NUMERIC, 4), this);
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
                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_FILTER_CRITERIA_NOT_OP, 4), this);
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
                    MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_FILTER_CRITERIA_VARIABLE_MISSING, 5), this);
                    return false;
                }
                else
                {
                    wMsg = GetWMsg(NumberCheck.CheckNotOperator(value, operatorr, variabletype));
                    if (wMsg != "")
                    {
                        MessageDialog.ErrorOk(string.Format(wMsg, 5), this);
                        return false;
                    }
                    wMsg = GetWMsg(NumberCheck.CheckValueAgainstOP(value, operatorr, variabletype));
                    if (wMsg != "")
                    {
                        MessageDialog.ErrorOk(string.Format(wMsg, 5), this);
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
                                    MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_DATAEXPORT_SET_NUMERIC, 5), this);
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
                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_FILTER_CRITERIA_NOT_OP, 5), this);
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
        private bool CheckValue(string textvalue, int categorycount, string type, string operatr)
        {
            return NumberCheck.CheckFromOption(textvalue, categorycount, type, operatr);
        }

        private void SetOptionSettingsMessage()
        {
            if (OptionSettingsMsg != null)
            {
                OptionSettingsMsg.TextFrame.Characters(Type.Missing, Type.Missing).Text = "";
                string text = CmbClassify.Text;
                bool? IsChecked = ChkCriteria.IsChecked;
                if (CmbClassify.SelectedIndex > 0)
                {
                    OptionSettingsMsg.TextFrame.Characters(Type.Missing, Type.Missing).Text = String.Format(LocalResource.FILTER_CRITERIA_MSG1, text);
                    if (IsChecked == true && CmbCriteria.SelectedIndex > 0)
                        OptionSettingsMsg.TextFrame.Characters(Type.Missing, Type.Missing).Text += "\n" + LocalResource.FILTER_CRITERIA_MSG2 + "\n" + LocalResource.FILTER_CRITERIA_MSG3;
                    else
                        OptionSettingsMsg.TextFrame.Characters(Type.Missing, Type.Missing).Text += "\n" + LocalResource.FILTER_CRITERIA_MSG3;
                }
                else if (IsChecked == true && CmbCriteria.SelectedIndex > 0)
                {
                    if (IsChecked == true && CmbCriteria.SelectedIndex > 0)
                        OptionSettingsMsg.TextFrame.Characters(Type.Missing, Type.Missing).Text += LocalResource.FILTER_CRITERIA_MSG2 + "\n" + LocalResource.FILTER_CRITERIA_MSG3;
                }
            }
        }

        private void BTN_CREATEREPORT_Click(object sender, RoutedEventArgs e)
        {
            SetOptionSettingsMessage();
            var SettingSheet = ExcelUtil.GetWorkSheetByCodeName(Workbook, Constants.SheetCodeName.DetailsSetting);

            int iTotalColumns = SettingSheet.UsedRange.Columns.Count;
            int iTotalRows = SettingSheet.UsedRange.Rows.Count;

            //These two lines do the magic.
            SettingSheet.Columns.ClearFormats();
            SettingSheet.Rows.ClearFormats();

            iTotalColumns = SettingSheet.UsedRange.Columns.Count;
            iTotalRows = SettingSheet.UsedRange.Rows.Count;

            excel.Range last = SettingSheet.Cells.SpecialCells(excel.XlCellType.xlCellTypeLastCell, Type.Missing);
            excel.Range range = SettingSheet.get_Range("A1", last);
        }

        public void readEXcel()
        {
            elementsInSheet.Clear();
            var SettingSheet = ExcelUtil.GetWorkSheetByCodeName(Workbook, Constants.SheetCodeName.DetailsSetting);

            excel.Range last = SettingSheet.Cells.SpecialCells(excel.XlCellType.xlCellTypeLastCell, Type.Missing);

            excel.Range rar = SettingSheet.get_Range("A1", last);
            var obj = rar.Value;
            int rowvalue = rowvalue = rar.Rows.Count + 1;

            for (int i = 2; i < rowvalue; i++)
            {
                if (obj[i, 1] != null)
                    elementsInSheet.Add((obj[i, 1]));
                else
                {
                    lastIndexinAdvancedSettingsExcelSheet = i;
                    break;
                }
            }

        }

        public void GetValue_Page_Output(Control x)
        {
            if (x is ComboBox)
            {
                if (((ComboBox)x).Name == GT.ComboBoxTypeName + "_Summary_WeightBack")
                {
                    try
                    {
                        var itm = ((ComboBox)x).SelectedItem;
                        if (itm != null)
                            ReadValueFromExcel.Add("F_Cr_Cross_AddUp_" + ((ComboBox)x).Name + GT.FormControlPostFixPro, ((DataExport)itm).QuestionVariable);
                        else
                            ReadValueFromExcel.Add("F_Cr_Cross_AddUp_" + ((ComboBox)x).Name + GT.FormControlPostFixPro, "");
                    }
                    catch { ReadValueFromExcel.Add("F_Cr_Cross_AddUp_" + ((ComboBox)x).Name + GT.FormControlPostFixPro, ""); }
                }
                else
                    ReadValueFromExcel.Add(GT.FormControlPrefixAddup + ((ComboBox)x).Name + GT.FormControlPostFixPro, ((ComboBox)x).SelectedIndex.ToString());
            }
            else if (x is TextBox)
            {
                if (((TextBox)x).Name == "Text_Summary_Change_Hyoutou" || ((TextBox)x).Name == "Text_Summary_Change_Non")
                    ReadValueFromExcel.Add(Name = "F_Cr_Cross_AddUp_" + ((TextBox)x).Name + GT.FormControlPostFixPro, ((TextBox)x).Text);
                else
                    ReadValueFromExcel.Add(GT.FormControlPrefixAddup + ((TextBox)x).Name + GT.FormControlPostFixPro, ((TextBox)x).Text);
            }
            else if (x is RadioButton)
                ReadValueFromExcel.Add(GT.FormControlPrefixAddup + ((RadioButton)x).Name + GT.FormControlPostFixPro, ((RadioButton)x).IsChecked.ToString());

            else if (x is CheckBox)
            {
                if (((CheckBox)x).Name == "Check_Summary_WeightBack")
                    ReadValueFromExcel.Add("F_Cr_Cross_AddUp_" + ((CheckBox)x).Name + GT.FormControlPostFixPro, ((CheckBox)x).IsChecked.ToString());
                else
                    ReadValueFromExcel.Add(GT.FormControlPrefixAddup + ((CheckBox)x).Name + GT.FormControlPostFixPro, ((CheckBox)x).IsChecked.ToString());
            }
        }

        private void Check_Summary_WeightBack_Checked(object sender, RoutedEventArgs e)
        {
            if (checkflag)
            {
                Check_Summary_WeightBack_Check();
            }
        }
        private void Check_Summary_WeightBack_Check()
        {
            if (Check_Summary_WeightBack.IsChecked == true)
            {
                this.Combo_Summary_WeightBack.IsEnabled = true;
                this.BTN_WEIGHT.IsEnabled = true;
                this.OutputUnweightbackedTotalCheck.IsEnabled = true;

            }
            else
            {
                this.Combo_Summary_WeightBack.IsEnabled = false;
                this.BTN_WEIGHT.IsEnabled = false;
                this.OutputUnweightbackedTotalCheck.IsEnabled = false;

            }
        }
        private void OutputUnweightbackedTotalCheck_Checked(object sender, RoutedEventArgs e)
        {
        }

        private void BTN_CLOSE_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void GetName_Page_Refine(Control x)
        {
            if (x is ComboBox)
                controlObj.Add(Name = GT.FormControlPrefixAddup + ((ComboBox)x).Name + GT.FormControlPostFixPro, ((ComboBox)x));

            else if (x is TextBox)
                controlObj.Add(Name = GT.FormControlPrefixAddup + ((TextBox)x).Name + GT.FormControlPostFixPro, ((TextBox)x));

            else if (x is RadioButton)
                controlObj.Add(Name = GT.FormControlPrefixAddup + ((RadioButton)x).Name + GT.FormControlPostFixPro, ((RadioButton)x));

            else if (x is CheckBox)
                controlObj.Add(Name = GT.FormControlPrefixAddup + ((CheckBox)x).Name + GT.FormControlPostFixPro, ((CheckBox)x));
        }


        private void BTN_WEIGHT_Click(object sender, RoutedEventArgs e)
        {
            weightbak = false;
            if (Combo_Summary_WeightBack.Text.Contains(Constants.WeightBack))
            { weightbak = true; }
            SampleWeightBack swb = new SampleWeightBack(Workbook, SelectedFile, _qstnvariablDD1, _qstnvariablnumeric, ref weightbak);
            swb.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            swb.Owner = this;
            swb.ShowDialog();
            try
            {
                if (weighted)
                {
                    SetWeightBack();
                    if (_qstnvariablnumeric[0].QuestionVariable == "")
                        _qstnvariablnumeric.RemoveAt(0);
                    DataExport ctarget = _qstnvariablnumeric.Where(z => z.QuestionVariable == Constants.WeightBack).FirstOrDefault();
                    int index = ctarget == null ? -1 : _qstnvariablnumeric.IndexOf(ctarget);
                    Combo_Summary_WeightBack.SelectedIndex = index;
                    weighted = false;
                }
            }
            catch { }
        }

        private void SetWeightBack()
        {
            var val = _qstnvariablnumeric.Where(x => x.QuestionVariable == Constants.WeightBack).FirstOrDefault();
            if (val == null)
            {
                DataExport d = new DataExport();
                d.Question = Constants.WeightBack;
                d.QuestionVariable = Constants.WeightBack;
                d.QuestionVariableType = Constants.WeightBack;
                d.QuestionChoiceNo = Constants.WeightBack;
                d.QuestionIndex = 0;
                _qstnvariablnumeric.Add(d);
            }
        }

        public void SelectWeightCombo(ObservableCollection<DataExport> qstnvariablnumeric)
        {

            DataExport ctarget = qstnvariablnumeric.Where(z => z.QuestionVariable == Constants.WeightBack).FirstOrDefault();
            int index = ctarget == null ? -1 : qstnvariablnumeric.IndexOf(ctarget);
            Combo_Summary_WeightBack.SelectedIndex = index;
        }
        private void GetValuesOfControls()
        {
            ReadValueFromExcel.Clear();

            foreach (ComboBox tb
               in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
            {
                GetValue_Page_Refine(tb);
            }
            foreach (RadioButton tb
                in FindControls.FindLogicalChildren<RadioButton>(this.Page_Refine))
            {
                GetValue_Page_Refine(tb);
            }

            foreach (TextBox tb
                in FindControls.FindLogicalChildren<TextBox>(this.Page_Refine))
            {
                GetValue_Page_Refine(tb);
            }
            foreach (CheckBox tb
                in FindControls.FindLogicalChildren<CheckBox>(this.Page_Refine))
            {
                GetValue_Page_Refine(tb);
            }

            //For cross OutPut 
            foreach (ComboBox tb
                in FindControls.FindLogicalChildren<ComboBox>(this.Page_Output))
            {
                GetValue_Page_Output(tb);
            }
            foreach (TextBox tb
                in FindControls.FindLogicalChildren<TextBox>(this.Page_Output))
            {
                GetValue_Page_Output(tb);
            }
            foreach (CheckBox tb
                in FindControls.FindLogicalChildren<CheckBox>(this.Page_Output))
            {
                GetValue_Page_Output(tb);
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
        FilterControlDesign fdobj = new FilterControlDesign();
        private void OnSelectionChanged(object sender, FilterControlDesign.MyComboBoxSelectionChangedEventArgs e)
        {
            if (e.sendr == "Combo_Classify_Item")
            {
                QC4Common.Util.FormUtil formUtil = new QC4Common.Util.FormUtil();
                if (((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).QuestionVariableType == null || ((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).QuestionVariableType == "")
                {
                    foreach (System.Windows.Controls.TextBox tb
              in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this.Page_Refine))
                    {
                        if (tb.Name == "TBAnsType")
                        {
                            tb.Text = "";
                        }
                        if (tb.Name == "TBNoOfChoice")
                        {
                            tb.Text = "";
                        }
                        else if (tb.Name == "TAQuestion")
                            tb.Text = "";
                        else if (tb.Name == "Combo_Classify_FolderPath")
                            tb.Text = "";
                    }
                    foreach (System.Windows.Controls.Button tb
          in FindControls.FindLogicalChildren<System.Windows.Controls.Button>(this.Page_Refine))
                    {
                        if (tb.Name == "exportpathbtn") tb.IsEnabled = false;
                    }
                    return;
                }
                foreach (System.Windows.Controls.TextBox tb
               in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this.Page_Refine))
                {
                    if (tb.Name == "TBAnsType")
                    {
                        tb.Text = ((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).QuestionVariableType.Split('/')[0];
                    }
                    if (tb.Name == "TBNoOfChoice")
                    {
                        tb.Text = ((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).QuestionVariableType.Split('/')[1];
                    }
                    else if (tb.Name == "TAQuestion")
                        tb.Text = formUtil.UnEscapeCRLF(((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).Question);

                }
                EnableDisableExportPathButton();
            }
            else if (e.sendr == "Combo_Conditional_Item_1")
            {
                if (e.LastSelectedText == "" && e.LastSelected == 0 && (((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).QuestionVariable == null || ((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).QuestionVariable == ""))
                {

                    foreach (System.Windows.Controls.ComboBox tb
              in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Operator_1")
                        {
                            tb.Items.Clear();
                            tb.IsEnabled = false;
                        }
                    }
                    foreach (System.Windows.Controls.TextBox tb
            in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Value_1") { tb.Text = ""; tb.IsEnabled = false; }
                    }
                    foreach (System.Windows.Controls.Button tb
           in FindControls.FindLogicalChildren<System.Windows.Controls.Button>(this.Page_Refine))
                    {
                        if (tb.Name == "BTnFilter1") tb.IsEnabled = false;
                    }
                    foreach (System.Windows.Controls.RadioButton tb
             in FindControls.FindLogicalChildren<System.Windows.Controls.RadioButton>(this.Page_Refine))
                    {
                        if (tb.Name == "Option_Conditional_And_1") { tb.IsChecked = false; tb.IsEnabled = false; }
                        else if (tb.Name == "Option_Conditional_Or_1") { tb.IsChecked = false; tb.IsEnabled = false; }

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
                    foreach (System.Windows.Controls.ComboBox tb
                   in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_Refine))
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
                        foreach (System.Windows.Controls.TextBox tb
                       in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this.Page_Refine))
                        {
                            if (tb.Name == "Combo_Conditional_Value_1")
                            {
                                tb.Text = "";
                                tb.IsEnabled = false;
                                break;
                            }
                        }
                        foreach (System.Windows.Controls.Button tb
                       in FindControls.FindLogicalChildren<System.Windows.Controls.Button>(this.Page_Refine))
                        {
                            if (tb.Name == "BTnFilter1")
                            {
                                tb.IsEnabled = false;
                                break;
                            }
                        }
                        foreach (System.Windows.Controls.RadioButton tb
                       in FindControls.FindLogicalChildren<System.Windows.Controls.RadioButton>(this.Page_Refine))
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
                    foreach (System.Windows.Controls.ComboBox tb
           in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Item_2")
                        {
                            tb.IsEnabled = false; break;
                        }
                    }

                    foreach (System.Windows.Controls.ComboBox tb
              in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Operator_2")
                        {
                            tb.Items.Clear(); tb.IsEnabled = false;
                            break;
                        }
                    }
                    foreach (System.Windows.Controls.TextBox tb
            in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Value_2") { tb.Text = ""; tb.IsEnabled = false; break; }
                    }
                    foreach (System.Windows.Controls.Button tb
           in FindControls.FindLogicalChildren<System.Windows.Controls.Button>(this.Page_Refine))
                    {
                        if (tb.Name == "BTnFilter2") { tb.IsEnabled = false; break; }
                    }
                    foreach (System.Windows.Controls.RadioButton tb
             in FindControls.FindLogicalChildren<System.Windows.Controls.RadioButton>(this.Page_Refine))
                    {
                        if (tb.Name == "Option_Conditional_And_1") { tb.IsChecked = false; }
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
                    Combo_Conditional_Item_2Choices = ((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).Choisces;
                    Combo_Conditional_Item_2selectedQuestionVariableType = ((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).QuestionVariableType.Split('/')[0];
                    ComboBox Combo_Conditional_Operator = null;
                    foreach (System.Windows.Controls.ComboBox tb
                   in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_Refine))
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
                        foreach (System.Windows.Controls.TextBox tb
                 in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this.Page_Refine))
                        {
                            if (tb.Name == "Combo_Conditional_Value_2")
                            {
                                tb.Text = "";
                                tb.IsEnabled = false;
                                break;
                            }
                        }
                        foreach (System.Windows.Controls.Button tb
                       in FindControls.FindLogicalChildren<System.Windows.Controls.Button>(this.Page_Refine))
                        {
                            if (tb.Name == "BTnFilter2")
                            {
                                tb.IsEnabled = false;
                                break;
                            }
                        }
                        foreach (System.Windows.Controls.RadioButton tb
                       in FindControls.FindLogicalChildren<System.Windows.Controls.RadioButton>(this.Page_Refine))
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
                    foreach (System.Windows.Controls.ComboBox tb
           in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Item_3")
                        {
                            tb.IsEnabled = (bool)false;
                        }
                    }

                    foreach (System.Windows.Controls.ComboBox tb
              in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Operator_3")
                        {
                            tb.Items.Clear(); tb.IsEnabled = false;
                        }
                    }
                    foreach (System.Windows.Controls.TextBox tb
            in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Value_3") { tb.Text = ""; tb.IsEnabled = false; }
                    }
                    foreach (System.Windows.Controls.Button tb
           in FindControls.FindLogicalChildren<System.Windows.Controls.Button>(this.Page_Refine))
                    {
                        if (tb.Name == "BTnFilter3") tb.IsEnabled = false;
                    }
                    foreach (System.Windows.Controls.RadioButton tb
             in FindControls.FindLogicalChildren<System.Windows.Controls.RadioButton>(this.Page_Refine))
                    {
                        if (tb.Name == "Option_Conditional_And_2") { tb.IsChecked = false; }
                        else if (tb.Name == "Option_Conditional_Or_2") { tb.IsChecked = false; }
                        else if (tb.Name == "Option_Conditional_And_3") { tb.IsChecked = false; tb.IsEnabled = false; }
                        else if (tb.Name == "Option_Conditional_Or_3") { tb.IsChecked = false; tb.IsEnabled = false; }

                    }
                    foreach (System.Windows.Controls.TextBox tb
                 in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this.Page_Refine))
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
                    Combo_Conditional_Item_3Choices = ((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).Choisces;
                    Combo_Conditional_Item_3selectedQuestionVariableType = ((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).QuestionVariableType.Split('/')[0];
                    ComboBox Combo_Conditional_Operator = null;
                    foreach (System.Windows.Controls.ComboBox tb
                   in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_Refine))
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
                        foreach (System.Windows.Controls.TextBox tb
                 in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this.Page_Refine))
                        {
                            if (tb.Name == "Combo_Conditional_Value_3")
                            {
                                tb.Text = "";
                                tb.IsEnabled = false;
                                break;
                            }
                        }
                        foreach (System.Windows.Controls.Button tb
                       in FindControls.FindLogicalChildren<System.Windows.Controls.Button>(this.Page_Refine))
                        {
                            if (tb.Name == "BTnFilter3")
                            {
                                tb.IsEnabled = false;
                                break;
                            }
                        }
                        foreach (System.Windows.Controls.RadioButton tb
                       in FindControls.FindLogicalChildren<System.Windows.Controls.RadioButton>(this.Page_Refine))
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
                    foreach (System.Windows.Controls.ComboBox tb
           in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Item_4")
                        {
                            tb.IsEnabled = false;
                        }
                    }
                    foreach (System.Windows.Controls.ComboBox tb
              in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Operator_4")
                        {
                            tb.IsEnabled = false;
                            tb.Items.Clear();
                        }
                    }
                    foreach (System.Windows.Controls.TextBox tb
            in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Value_4") { tb.Text = ""; tb.IsEnabled = false; break; }
                    }
                    foreach (System.Windows.Controls.Button tb
           in FindControls.FindLogicalChildren<System.Windows.Controls.Button>(this.Page_Refine))
                    {
                        if (tb.Name == "BTnFilter4") { tb.IsEnabled = false; break; }
                    }
                    foreach (System.Windows.Controls.RadioButton tb
             in FindControls.FindLogicalChildren<System.Windows.Controls.RadioButton>(this.Page_Refine))
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
                    Combo_Conditional_Item_4Choices = ((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).Choisces;
                    Combo_Conditional_Item_4selectedQuestionVariableType = ((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).QuestionVariableType.Split('/')[0];
                    ComboBox Combo_Conditional_Operator = null;
                    foreach (System.Windows.Controls.ComboBox tb
                   in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_Refine))
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
                        foreach (System.Windows.Controls.TextBox tb
                 in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this.Page_Refine))
                        {
                            if (tb.Name == "Combo_Conditional_Value_4")
                            {
                                tb.Text = "";
                                tb.IsEnabled = false;
                                break;
                            }
                        }
                        foreach (System.Windows.Controls.Button tb
                       in FindControls.FindLogicalChildren<System.Windows.Controls.Button>(this.Page_Refine))
                        {
                            if (tb.Name == "BTnFilter4")
                            {
                                tb.IsEnabled = false;
                                break;
                            }
                        }
                        foreach (System.Windows.Controls.RadioButton tb
                       in FindControls.FindLogicalChildren<System.Windows.Controls.RadioButton>(this.Page_Refine))
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
                    foreach (System.Windows.Controls.ComboBox tb
           in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Item_5")
                        {
                            tb.IsEnabled = false;
                        }
                    }
                    foreach (System.Windows.Controls.ComboBox tb
              in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Operator_5")
                        {
                            tb.IsEnabled = false;
                            tb.Items.Clear();
                        }
                    }
                    foreach (System.Windows.Controls.TextBox tb
            in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Value_5") { tb.Text = ""; tb.IsEnabled = false; break; }
                    }
                    foreach (System.Windows.Controls.Button tb
           in FindControls.FindLogicalChildren<System.Windows.Controls.Button>(this.Page_Refine))
                    {
                        if (tb.Name == "BTnFilter5") { tb.IsEnabled = false; break; }
                    }
                    foreach (System.Windows.Controls.RadioButton tb
            in FindControls.FindLogicalChildren<System.Windows.Controls.RadioButton>(this.Page_Refine))
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
                    Combo_Conditional_Item_5Choices = ((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).Choisces;
                    Combo_Conditional_Item_5selectedQuestionVariableType = ((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).QuestionVariableType.Split('/')[0];
                    ComboBox Combo_Conditional_Operator = null;
                    foreach (System.Windows.Controls.ComboBox tb
                   in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_Refine))
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
                        foreach (System.Windows.Controls.TextBox tb
                 in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this.Page_Refine))
                        {
                            if (tb.Name == "Combo_Conditional_Value_5")
                            {
                                tb.Text = "";
                                tb.IsEnabled = false;
                                break;
                            }
                        }
                        foreach (System.Windows.Controls.Button tb
                       in FindControls.FindLogicalChildren<System.Windows.Controls.Button>(this.Page_Refine))
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
                    foreach (System.Windows.Controls.CheckBox tb
                       in FindControls.FindLogicalChildren<System.Windows.Controls.CheckBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Check_Refine_Condition")
                        {
                            isChecked = tb.IsChecked == true ? true : false;
                            break;
                        }
                    }
                    if (!isChecked)
                        OnCheck(null, new FilterControlDesign.MyCheckBoxClickEventArgs() { sendr = "Check_Refine_Condition", Check = isChecked });
                }
            }
            else if (e.sendr == "Combo_Conditional_Operator_1")
            {
                ComboBox Combo_Conditional_Value = null;
                foreach (System.Windows.Controls.ComboBox tb
              in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_Refine))
                {
                    if (tb.Name == "Combo_Conditional_Operator_1") { Combo_Conditional_Value = tb; break; }
                }
                if (Combo_Conditional_Value.SelectedIndex >= 0 && Combo_Conditional_Initial1)
                {
                    foreach (System.Windows.Controls.TextBox tb
                  in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Value_1") { tb.IsEnabled = true; break; }
                    }
                    foreach (System.Windows.Controls.Button tb
               in FindControls.FindLogicalChildren<System.Windows.Controls.Button>(this.Page_Refine))
                    {
                        if (tb.Name == "BTnFilter1") { tb.IsEnabled = true; break; }
                    }
                }
            }
            else if (e.sendr == "Combo_Conditional_Operator_2")
            {
                ComboBox Combo_Conditional_Value = null;
                foreach (System.Windows.Controls.ComboBox tb
              in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_Refine))
                {
                    if (tb.Name == "Combo_Conditional_Operator_2") { Combo_Conditional_Value = tb; break; }
                }
                if (Combo_Conditional_Value.SelectedIndex >= 0 && Combo_Conditional_Initial2)
                {
                    foreach (System.Windows.Controls.TextBox tb
              in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Value_2") { tb.IsEnabled = true; break; }
                    }
                    foreach (System.Windows.Controls.Button tb
               in FindControls.FindLogicalChildren<System.Windows.Controls.Button>(this.Page_Refine))
                    {
                        if (tb.Name == "BTnFilter2") { tb.IsEnabled = true; break; }
                    }
                }
            }
            else if (e.sendr == "Combo_Conditional_Operator_3")
            {
                ComboBox Combo_Conditional_Value = null;
                foreach (System.Windows.Controls.ComboBox tb
              in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_Refine))
                {
                    if (tb.Name == "Combo_Conditional_Operator_3") { Combo_Conditional_Value = tb; break; }
                }
                if (Combo_Conditional_Value.SelectedIndex >= 0 && Combo_Conditional_Initial3)
                {
                    foreach (System.Windows.Controls.TextBox tb
              in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Value_3") { tb.IsEnabled = true; break; }
                    }
                    foreach (System.Windows.Controls.Button tb
               in FindControls.FindLogicalChildren<System.Windows.Controls.Button>(this.Page_Refine))
                    {
                        if (tb.Name == "BTnFilter3") { tb.IsEnabled = true; break; }
                    }
                }
            }
            else if (e.sendr == "Combo_Conditional_Operator_4")
            {
                ComboBox Combo_Conditional_Value = null;
                foreach (System.Windows.Controls.ComboBox tb
              in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_Refine))
                {
                    if (tb.Name == "Combo_Conditional_Operator_4") { Combo_Conditional_Value = tb; break; }
                }
                if (Combo_Conditional_Value.SelectedIndex >= 0 && Combo_Conditional_Initial4)
                {
                    foreach (System.Windows.Controls.TextBox tb
              in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Value_4") { tb.IsEnabled = true; break; }
                    }
                    foreach (System.Windows.Controls.Button tb
               in FindControls.FindLogicalChildren<System.Windows.Controls.Button>(this.Page_Refine))
                    {
                        if (tb.Name == "BTnFilter4") { tb.IsEnabled = true; break; }
                    }
                }
            }
            else if (e.sendr == "Combo_Conditional_Operator_5")
            {
                ComboBox Combo_Conditional_Value = null;
                foreach (System.Windows.Controls.ComboBox tb
              in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_Refine))
                {
                    if (tb.Name == "Combo_Conditional_Operator_5") { Combo_Conditional_Value = tb; break; }
                }
                if (Combo_Conditional_Value.SelectedIndex >= 0 && Combo_Conditional_Initial5)
                {
                    foreach (System.Windows.Controls.TextBox tb
              in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Value_5") { tb.IsEnabled = true; break; }
                    }
                    foreach (System.Windows.Controls.Button tb
               in FindControls.FindLogicalChildren<System.Windows.Controls.Button>(this.Page_Refine))
                    {
                        if (tb.Name == "BTnFilter5") { tb.IsEnabled = true; break; }
                    }
                }
            }
        }

        public class MyComboBoxItem
        {
            public string Text { get; set; }
        }
        private void OnButtonClick(object sender, FilterControlDesign.MyButtonClickEventArgs e) //use for button click in filtersettings usercontrol
        {
            if (e.sendr == "exportpathbtn")//OnBrowseButtonClick
            {
                var dialog = new CommonOpenFileDialog();
                dialog.IsFolderPicker = true;

                if (dialog.ShowDialog(this) == CommonFileDialogResult.Ok)
                {
                    foreach (TextBox tb
                in FindControls.FindLogicalChildren<TextBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Classify_FolderPath")//Text_Output_Path  Combo_Classify_FolderPath
                        {
                            tb.Text = dialog.FileName;
                        }
                    }
                }
            }
            else if (e.sendr == "BTnFilter1")//criteria value button1
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
            }
            else if (QuestionVariableType == "FA" || QuestionVariableType == "MA")
            {
                CBOperator.Items.Clear();
                CBOperator.Items.Add("=");
                CBOperator.Items.Add("<>");
            }
            CBOperator.IsEnabled = true;
        }
        private void OnCheck(object sender, FilterControlDesign.MyCheckBoxClickEventArgs e) //use for button click in filtersettings usercontrol
        {
            if (e.sendr == "Check_Refine_Condition" && !IsInitialLoad)
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
                    foreach (System.Windows.Controls.ComboBox tb
            in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_Refine))
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
                    foreach (System.Windows.Controls.TextBox tb
                in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this.Page_Refine))
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
                    foreach (System.Windows.Controls.Button tb
            in FindControls.FindLogicalChildren<System.Windows.Controls.Button>(this.Page_Refine))
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
                    foreach (System.Windows.Controls.Label tb
              in FindControls.FindLogicalChildren<System.Windows.Controls.Label>(this.Page_Refine))
                    {
                        if (tb.Name == "lblCriteriaVariable") tb.IsEnabled = false;
                        else if (tb.Name == "lblOperator") tb.IsEnabled = false;
                        else if (tb.Name == "lblValue") tb.IsEnabled = false;
                    }

                    foreach (System.Windows.Controls.ComboBox tb
              in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_Refine))
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
                    foreach (System.Windows.Controls.TextBox tb
              in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this.Page_Refine))
                    {
                        if (tb.Name == "Combo_Conditional_Value_1") tb.IsEnabled = false;
                        else if (tb.Name == "Combo_Conditional_Value_2") tb.IsEnabled = false;
                        else if (tb.Name == "Combo_Conditional_Value_3") tb.IsEnabled = false;
                        else if (tb.Name == "Combo_Conditional_Value_4") tb.IsEnabled = false;
                        else if (tb.Name == "Combo_Conditional_Value_5") tb.IsEnabled = false;
                    }
                    foreach (System.Windows.Controls.RadioButton tb
              in FindControls.FindLogicalChildren<System.Windows.Controls.RadioButton>(this.Page_Refine))
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
                    foreach (System.Windows.Controls.Button tb
            in FindControls.FindLogicalChildren<System.Windows.Controls.Button>(this.Page_Refine))
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
        private void OnRadioClick(object sender, FilterControlDesign.MyRadioButtonClickEventArgs e) //use for RadioClick click in filtersettings usercontrol
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

        private void DisableComboItems()
        {

            foreach (ComboBox tb
      in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
            {
                if (tb.Name == "Combo_Conditional_Item_1") tb.IsEnabled = false;
                else if (tb.Name == "Combo_Conditional_Item_2") tb.IsEnabled = false;

                else if (tb.Name == "Combo_Conditional_Item_3") tb.IsEnabled = false;

                else if (tb.Name == "Combo_Conditional_Item_4") tb.IsEnabled = false;

                else if (tb.Name == "Combo_Conditional_Item_5") tb.IsEnabled = false;


            }
        }
        private void EnableComboItemsOnLoad(bool checkval)
        {
            if (true == checkval)
            {
                foreach (ComboBox tb
          in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
                {
                    if (tb.Name == "Combo_Conditional_Item_1") tb.IsEnabled = true;//&& tb.Text != "" && tb.SelectedItem != null
                    else if (tb.Name == "Combo_Conditional_Operator_1" && tb.Text != "" && tb.SelectedItem != null) tb.IsEnabled = true; // (FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem)
                    else if (tb.Name == "Combo_Conditional_Item_2" && (FilterSettingsView.FilterSettingsClass.DataExport)tb.SelectedItem != null)
                    {
                        if ((((FilterSettingsView.FilterSettingsClass.DataExport)tb.SelectedItem).QuestionVariable != null &&
                    ((FilterSettingsView.FilterSettingsClass.DataExport)tb.SelectedItem).QuestionVariable != ""))
                            tb.IsEnabled = true;
                        else tb.IsEnabled = false;
                    }

                    else if (tb.Name == "Combo_Conditional_Item_3" && (FilterSettingsView.FilterSettingsClass.DataExport)tb.SelectedItem != null)
                    {
                        if ((((FilterSettingsView.FilterSettingsClass.DataExport)tb.SelectedItem).QuestionVariable != null &&
                     ((FilterSettingsView.FilterSettingsClass.DataExport)tb.SelectedItem).QuestionVariable != ""))
                            tb.IsEnabled = true;
                        else tb.IsEnabled = false;
                    }

                    else if (tb.Name == "Combo_Conditional_Item_4" && (FilterSettingsView.FilterSettingsClass.DataExport)tb.SelectedItem != null)
                    {
                        if ((((FilterSettingsView.FilterSettingsClass.DataExport)tb.SelectedItem).QuestionVariable != null &&
                   ((FilterSettingsView.FilterSettingsClass.DataExport)tb.SelectedItem).QuestionVariable != ""))
                            tb.IsEnabled = true;
                        else tb.IsEnabled = false;
                    }

                    else if (tb.Name == "Combo_Conditional_Item_5" && (FilterSettingsView.FilterSettingsClass.DataExport)tb.SelectedItem != null)
                    {
                        if ((((FilterSettingsView.FilterSettingsClass.DataExport)tb.SelectedItem).QuestionVariable != null &&
                   ((FilterSettingsView.FilterSettingsClass.DataExport)tb.SelectedItem).QuestionVariable != ""))
                            tb.IsEnabled = true;
                        else tb.IsEnabled = false;
                    }
                }
            }
            else { DisableComboItems(); }
        }

        public void GetValue_Page_Refine(Control x)
        {
            if (x is ComboBox)
            {
                var myObject = ((ComboBox)x).SelectedItem as DataExport;

                if (myObject != null)
                    ReadValueFromExcel.Add(GT.FormControlPrefixAddup + ((ComboBox)x).Name + GT.FormControlPostFixPro, ((DataExport)((ComboBox)x).SelectedItem).QuestionVariable);
                else
                    ReadValueFromExcel.Add(GT.FormControlPrefixAddup + ((ComboBox)x).Name + GT.FormControlPostFixPro, ((ComboBox)x).Text);
            }
            else if (x is TextBox)
                ReadValueFromExcel.Add(GT.FormControlPrefixAddup + ((TextBox)x).Name + GT.FormControlPostFixPro, ((TextBox)x).Text);

            else if (x is RadioButton)
                ReadValueFromExcel.Add(GT.FormControlPrefixAddup + ((RadioButton)x).Name + GT.FormControlPostFixPro, ((RadioButton)x).IsChecked.ToString());

            else if (x is CheckBox)
                ReadValueFromExcel.Add(GT.FormControlPrefixAddup + ((CheckBox)x).Name + GT.FormControlPostFixPro, ((CheckBox)x).IsChecked.ToString());
        }

        private void EnableDisableExportPathButton()
        {
            bool enableexportpathbtn = false;

            foreach (ComboBox cb
      in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
            {
                if (cb.Name == "Combo_Classify_Item")
                {
                    if (cb.SelectedItem != null && (((DataExport)cb.SelectedItem).QuestionVariable != null &&
                   ((DataExport)cb.SelectedItem).QuestionVariable != ""))
                    {
                        enableexportpathbtn = true;
                        break;
                    }
                }
            }
            foreach (Button tb
          in FindControls.FindLogicalChildren<Button>(this.Page_Refine))
            {
                if (tb.Name == "exportpathbtn") { tb.IsEnabled = enableexportpathbtn; break; }
            }
        }

        private void Check_Output_Hide_ChoiceFor_PieChart_Click(object sender, RoutedEventArgs e)
        {
            UpdateRatioSettings();
        }

        private void UpdateRatioSettings()
        {
            if (Check_Output_Hide_ChoiceFor_PieChart.IsChecked == true)
            {
                Text_Outputs_Ratios_EqualToLessThan.IsEnabled = true;
                BTN_SET_1_UP.IsEnabled = true;
                BTN_SET_1_DOWN.IsEnabled = true;
            }
            else
            {
                Text_Outputs_Ratios_EqualToLessThan.IsEnabled = false;
                BTN_SET_1_UP.IsEnabled = false;
                BTN_SET_1_DOWN.IsEnabled = false;
            }
        }

        private void Text_Outputs_Ratios_EqualToLessThan_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Text_Outputs_Ratios_EqualToLessThan.Text.Trim().Length > 0)
            {
                SetOutputRatio();
            }
        }

        private void SetOutputRatio()
        {
            int numval = 0;
            try
            {
                numval = Convert.ToInt32(Text_Outputs_Ratios_EqualToLessThan.Text);
            }
            catch
            {
                numval = 0;
            }

            if (numval < Constants.GT.DifferenceSetMinValueGT)
                numval = Constants.GT.DifferenceSetMinValueGT;
            if (numval > Constants.GT.DifferenceSetMaxValueGT)
                numval = Constants.GT.DifferenceSetMaxValueGT;
            Text_Outputs_Ratios_EqualToLessThan.Text = numval.ToString();
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SetForegroundWindow((IntPtr)Workbook.Application.Hwnd);
            if (CloseNotFromBtn)
            {
                if (!CheckRefine())
                {
                    CloseNotFromBtn = true;
                    e.Cancel = true;
                    return;
                }
                SetOutputRatio();
                SetOptionSettingsMessage();
                GetValuesOfControls();
                SaveSettings();
                Workbook.Application.Cursor = excel.XlMousePointer.xlDefault;
            }
            else
            {
                SetOutputRatio();

                if (!CheckRefine())
                {
                    CloseNotFromBtn = true;
                    e.Cancel = true;
                    return;
                }

                if (CrossTabulation.checkUnprocessedNewQuestionDialog(Workbook, new System.Windows.Interop.WindowInteropHelper(this).Handle))
                {
                    CloseNotFromBtn = true;
                    e.Cancel = true;
                    return;
                }

                SetOptionSettingsMessage();

                GetValuesOfControls();

                SaveSettings();

            }
        }
        Button Sender = null;
        private void BTN_SET_1_UP_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Sender = ((Button)sender);
            SetRate();
            Thread.Sleep(200);
            dispatcherTimer.Start();
        }

        private void BTN_SET_1_UP_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            dispatcherTimer.Stop();
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            SetRate();
        }

        private void SetRate()
        {
            int numval = 0;
            if (Sender.Name == "BTN_SET_1_UP")
            {
                numval = Convert.ToInt32(Text_Outputs_Ratios_EqualToLessThan.Text);
                numval++;
                if (numval > Constants.GT.DifferenceSetMaxValueGT)
                    numval = Constants.GT.DifferenceSetMaxValueGT;
                Text_Outputs_Ratios_EqualToLessThan.Text = numval.ToString();
            }
            else if (Sender.Name == "BTN_SET_2_UP")
            {
                numval = Convert.ToInt32(Text_Outputs_Ratios_EqualToLessThan.Text);
                numval++;
                if (numval > Constants.GT.DifferenceSetMaxValueGT)
                    numval = Constants.GT.DifferenceSetMaxValueGT;
                Text_Outputs_Ratios_EqualToLessThan.Text = numval.ToString();
            }
            else if (Sender.Name == "BTN_SET_1_DOWN")
            {
                numval = Convert.ToInt32(Text_Outputs_Ratios_EqualToLessThan.Text);
                numval--;
                if (numval < Constants.GT.DifferenceSetMinValueGT)
                    numval = Constants.GT.DifferenceSetMinValueGT;
                Text_Outputs_Ratios_EqualToLessThan.Text = numval.ToString();
            }
            else if (Sender.Name == "BTN_SET_2_DOWN")
            {
                numval = Convert.ToInt32(Text_Outputs_Ratios_EqualToLessThan.Text);
                numval--;
                if (numval < Constants.GT.DifferenceSetMinValueGT)
                    numval = Constants.GT.DifferenceSetMinValueGT;
                Text_Outputs_Ratios_EqualToLessThan.Text = numval.ToString();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (!CloseNotFromBtn)
            {
                try
                {
                    string version = "P";
                    excel.Worksheet GrossSheet = ExcelUtil.GetWorkSheetByCodeName(Workbook, Constants.SheetCodeName.GTTabulation);
                    new MainWindow().GrossTabulate(Workbook, GrossSheet, version, true);
                }
                catch (Exception ex)
                {
                    _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                }
            }
        }

        System.Windows.Controls.ComboBox combo = null;
        string LastSelectedText = "";
        int LastSelected = 0;
        private void Combo_Summary_WeightBack_Loaded(object sender, RoutedEventArgs e)
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

        private void Combo_Summary_WeightBack_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            System.Windows.Controls.ComboBox sen = ((System.Windows.Controls.ComboBox)sender);
            if ((Key.Delete == e.Key || ((sen.SelectedIndex == -1 && sen.Text == "") || (sen.SelectedIndex == -1 && sen.Text != ""))) && (this.Combo_Summary_WeightBack.IsKeyboardFocusWithin || this.Combo_Summary_WeightBack.IsDropDownOpen))
            {
                LastSelected = 0;
                this.Combo_Summary_WeightBack.SelectedIndex = 0;
            }
            else if (sen.SelectedItem == null && (sen.IsKeyboardFocusWithin || sen.IsDropDownOpen))
            {
                if (sen.Name != "Combo_Summary_WeightBack")
                    sen.SelectedIndex = LastSelected;
            }
        }

        private void Combo_Summary_WeightBack_PreviewKeyDown(object sender, KeyEventArgs e)
        {
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
            if (Key.Delete == e.Key && (this.Combo_Summary_WeightBack.IsKeyboardFocusWithin || this.Combo_Summary_WeightBack.IsDropDownOpen))
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

        private void Combo_Summary_WeightBack_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            System.Windows.Controls.ComboBox sen = ((System.Windows.Controls.ComboBox)sender);
            combo = sen;
        }

        private void Combo_Summary_WeightBack_DropDownClosed(object sender, EventArgs e)
        {
            System.Windows.Controls.ComboBox sen = ((System.Windows.Controls.ComboBox)sender);
            if (sen.SelectedItem != null)
            {
                sen.Text = ((FilterSettingsClass.DataExport)(sen.SelectedItem)).QuestionVariable;
            }
            else
            {
                sen.Text = "";
            }
        }
    }
}
