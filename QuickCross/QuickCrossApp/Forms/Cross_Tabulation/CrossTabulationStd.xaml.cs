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
using Vb = Microsoft.VisualBasic;
using Qc4Launcher.Util;
using excel = Microsoft.Office.Interop.Excel;
using Qc4Launcher.Classes;
using System.Collections.ObjectModel;
using static FilterSettingsView.FilterSettingsClass;
using System.Text.RegularExpressions;
using static ExcelAddIn.Common.Constants;
using Constant = ExcelAddIn.Common.Constants;
using Qc4Launcher.Logic;
using System.ComponentModel;
using QC4Common.Model;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.Threading;
using log4net;
using System.Reflection;
using QC4Common.Validation;
using System.Windows.Threading;
using QC4Common.Logic;
using Microsoft.WindowsAPICodePack.Dialogs;
using Qc4Launcher.Forms.Cross_Tabulation;
using Qc4Launcher.Forms.GrossTabulationSetting;
using QC4Common.Classes.HatchColor;
using Qc4Launcher.Classes.HatchColor;

namespace Qc4Launcher.Forms.Cross_Tabulation
{
    /// <summary>
    /// Interaction logic for CrossTabulationStd.xaml
    /// </summary>
    public partial class CrossTabulationStd : Window
    {
        public static bool IsInitialized = false;
        public static bool LayoutCheckedChanged = false;
        string Option_Setting_Output_Lateral_PreviousVal = null;
        string Option_Setting_Output_Vertical_PreviousVal = null;
        excel.Workbook Workbook;
        bool IsCreateReport = false;
        bool ISWBAdded = false;
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private ProgressBar progress = null;
        private string SelectedFile = "";
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
        public excel.Shape OptionSettingsMsg { get; set; }

        bool Combo_Conditional_Initial1 = false;
        bool Combo_Conditional_Initial2 = false;
        bool Combo_Conditional_Initial3 = false;
        bool Combo_Conditional_Initial4 = false;
        bool Combo_Conditional_Initial5 = false;
        bool CloseNotFromBtn = true;

        bool CROSS_TXT_BX_ALL_CLRF = false;
        bool CROSS_TXT_BX_ALL2_CLRF = false;
        bool CROSS_TXT_BX_NONE_CLRF = false;

        private ObservableCollection<DataExport> _dataExport_LBVariablesToExport = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _dataExport_ListBoxCommonCopy = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD1 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD2 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD3 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD4 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD5 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD6 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablnumeric = new ObservableCollection<DataExport>();
        private RadioButton Option_Setting_Output_Vertical = null;
        private RadioButton Option_Setting_Output_Lateral = null;

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

        private Dictionary<string, Control> controlObj = new Dictionary<string, Control>();

        private List<String> elementsInSheet = new List<String>();
        private int lastIndexinAdvancedSettingsExcelSheet = 0;

        private Dictionary<string, String> ReadValueFromExcel = new Dictionary<string, String>();
        private bool checkflag = false;
        FilterSettings fs = new FilterSettings();
        FilterSettingsView.FilterSettingsClass fsc = new FilterSettingsClass();
        bool weightbak;
        public static bool weighted;
        public bool filtersettingcheckflag;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        MainWindow MainWindow;
        TabBinding tabbindgs = new TabBinding();
        public CrossTabulationStd(MainWindow main, excel.Workbook workbook, string filePath)
        {
            IsInitialized = false;
            LayoutCheckedChanged = false;
            InitializeComponent();
            if (Text_Summary_Change_Hyoutou.Text == "")
            {
                Text_Summary_Change_Hyoutou.Text = LocalResource.CROSS_TXT_BX_ALL;
            }
            if (Text_Summary_Change_Hyosoku.Text == "")
            {
                Text_Summary_Change_Hyosoku.Text = LocalResource.CROSS_TXT_BX_ALL;
            }
            if (Text_Summary_Change_Non.Text == "")
            {
                Text_Summary_Change_Non.Text = LocalResource.CROSS_TXT_BX_NOANSWER;
            }
            txt_base_setting.Visibility = Visibility.Hidden;
            txt_answer_output.Visibility = Visibility.Hidden;
            txt_waitback.Visibility = Visibility.Hidden;
            txt_refinement.Visibility = Visibility.Hidden;
            txt_classific_item.Visibility = Visibility.Hidden;
            MainWindow = main;
            FilterSettingsView.FilterControlDesign fdesign = new FilterControlDesign();
            this.GridContainer.Children.Add(fdesign);

            Workbook = workbook;
            SelectedFile = filePath;

            Cross_tab_Settings crost = new Cross_tab_Settings();
            Tabulation_setting.Children.Add(crost);
            Graph.DataContext = tabbindgs;
            tabItems = Forms.Cross_Tabulation.TabBinding.tabItems;
            fdesign.MyComboBoxSelectionChanged += new FilterControlDesign.MyComboBoxSelectionChangedEventHandler(OnSelectionChanged);
            fdesign.MyButtonClick += new FilterControlDesign.MyButtonClickEventEventHandler(OnButtonClick);
            fdesign.MyCheckBoxClick += new FilterControlDesign.MyCheckBoxClickEventHandler(OnCheck);
            fdesign.MyRadioButtonClick += new FilterControlDesign.MyRadioButtonClickEventHandler(OnRadioClick);
            fdesign.MyTextBoxChange += new FilterControlDesign.MyTextBoxChangeEventHandler(OnTextBoxChange);
            //
            CROSS_TXT_BX_ALL_CLRF = false;
            CROSS_TXT_BX_ALL2_CLRF = false;
            CROSS_TXT_BX_NONE_CLRF = false;
        }
        private void DisableRightMenu(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 25);
            try
            {
                HatchColorCommon.InitialiseColorPreset(this.Combo_Color_Settings_S);

                if (this.IsActive)
                {
                    this.Focus();
                }

                checkflag = false;
                IsInitialLoad = true;
                LoadingData();
                getData();
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
                foreach (RadioButton tb
                     in FindControls.FindLogicalChildren<RadioButton>(this.Tabulation_setting))
                {
                    Get_setting_data(tb);
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
                in FindControls.FindLogicalChildren<ComboBox>(this.Page_Summary))
                {
                    GetName_Page_Summary(tb);
                }
                foreach (RadioButton tb
                    in FindControls.FindLogicalChildren<RadioButton>(this.Page_Summary))
                {
                    GetName_Page_Summary(tb);
                }
                foreach (RadioButton tb
                   in FindControls.FindLogicalChildren<RadioButton>(this.Tabulation_setting))
                {
                    if (tb.Name == "Option_Setting_Output_Vertical")
                        Option_Setting_Output_Vertical = tb;
                    else if (tb.Name == "Option_Setting_Output_Lateral")
                        Option_Setting_Output_Lateral = tb;

                    GetTabsetting(tb);
                }

                foreach (TextBox tb
                    in FindControls.FindLogicalChildren<TextBox>(this.Page_Summary))
                {
                    GetName_Page_Summary(tb);
                }
                foreach (CheckBox tb
                    in FindControls.FindLogicalChildren<CheckBox>(this.Page_Summary))
                {
                    GetName_Page_Summary(tb);
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
                bool textAdded = false;
                bool isPercentage = false;
                for (int i = 0; i < elementsInSheet.Count; i++)
                {
                    if (controlObj.ContainsKey(elementsInSheet[i]))
                    {
                        ComboBox cmb = controlObj[elementsInSheet[i]] as ComboBox;

                        if (cmb != null)
                        {
                            string s = elementsInSheet[i];

                            // Check if the string 's' contains the name of the Combo_Color_Settings_S control.
                            if (s.Contains(this.Combo_Color_Settings_S.Name))
                            {
                                // Find a color preset with the specified name.
                                ColorPreset selectedColorPreset = ColorPresetStore.GetColorPresetByName(obj[i + 2, 2]);

                                // If a matching color preset is found, set it as the selected value of the ComboBox control.
                                if (selectedColorPreset != null)
                                {
                                    ((ComboBox)controlObj[elementsInSheet[i]]).SelectedValue = selectedColorPreset.Name;
                                }
                            }
                            else if (s.Contains("F_Cr_Cross_AddUp_Combo"))//for this saving and retreiving combo index value
                            {
                                string sval = null == obj[i + 2, 2] ? "" : obj[i + 2, 2];
                                int b;
                                if (s.Contains("Combo_Summary_WeightBack"))
                                {
                                    if (sval != "")
                                        WeightBac_Selected_Index = true;
                                }
                                if (int.TryParse(sval, out b))//bcs of weight back cheking this
                                {
                                    int val = null == obj[i + 2, 2] ? 0 : Convert.ToInt32(obj[i + 2, 2]);

                                    ((ComboBox)controlObj[elementsInSheet[i]]).SelectedIndex = val;
                                    ((ComboBox)controlObj[elementsInSheet[i]]).IsEnabled = true;
                                }
                                else
                                {//add logic here for filter settings combobox items

                                    string val = null == obj[i + 2, 2] ? "" : obj[i + 2, 2].ToString();
                                    DataExport target = null;
                                    int index = 0;
                                    bool enable = true;
                                    if (s.Contains("Combo_Classify_Item"))// Combo_Conditional_Item_1
                                    {
                                        target = _qstnvariablDD1.Where(z => z.QuestionVariable == val).FirstOrDefault();
                                        index = target == null ? 0 : _qstnvariablDD1.IndexOf(target);
                                        cmb.SelectedIndex = index;
                                        cmb.IsEnabled = true;
                                        CmbClsy = index;
                                    }
                                    else if (s.Contains("Combo_Conditional_Item_1"))// 
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
                                    else if (s.Contains("Combo_Conditional_Item_2"))// 
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
                                    else if (s.Contains("Combo_Conditional_Item_3"))// 
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
                                    else if (s.Contains("Combo_Conditional_Item_4"))// 
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
                                    else if (s.Contains("Combo_Conditional_Item_5"))// 
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
                                        //((ComboBox)controlObj[elementsInSheet[i]]).SelectedItem = val;
                                        ((ComboBox)controlObj[elementsInSheet[i]]).IsEnabled = enable;// true;

                                        switch (((ComboBox)controlObj[elementsInSheet[i]]).Name)
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
                                        ((ComboBox)controlObj[elementsInSheet[i]]).IsEnabled = enable;// true;
                                    }
                                    else
                                    {
                                        ((ComboBox)controlObj[elementsInSheet[i]]).Text = val;
                                        ((ComboBox)controlObj[elementsInSheet[i]]).IsEnabled = enable;// true;
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
                            textAdded = true;

                            if (Convert.ToString(obj[i + 2, 1]) == "F_Cr_Cross_AddUp_Text_Summary_Change_Hyoutou_S" && Convert.ToString(obj[i + 2, 2]) == QC4Common.Common.Constants.CRLFchar)
                            {
                                CROSS_TXT_BX_ALL_CLRF = true;
                                ((TextBox)controlObj[elementsInSheet[i]]).Text = LocalResource.CROSS_TXT_BX_ALL;
                            }
                            else if (Convert.ToString(obj[i + 2, 1]) == "F_Cr_Cross_AddUp_Text_Summary_Change_Hyosoku_S" && Convert.ToString(obj[i + 2, 2]) == QC4Common.Common.Constants.CRLFchar)
                            {
                                CROSS_TXT_BX_ALL2_CLRF = true;
                                ((TextBox)controlObj[elementsInSheet[i]]).Text = LocalResource.CROSS_TXT_BX_ALL;
                            }
                            else if (Convert.ToString(obj[i + 2, 1]) == "F_Cr_Cross_AddUp_Text_Summary_Change_Non_S" && Convert.ToString(obj[i + 2, 2]) == QC4Common.Common.Constants.CRLFchar)
                            {
                                CROSS_TXT_BX_NONE_CLRF = true;
                                ((TextBox)controlObj[elementsInSheet[i]]).Text = LocalResource.CROSS_TXT_BX_NOANSWER;
                            }
                            else
                            {
                                ((TextBox)controlObj[elementsInSheet[i]]).Text = obj[i + 2, 2];
                            }
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
                            if ((((RadioButton)controlObj[elementsInSheet[i]]).Name == "Option_Setting_Output_Vertical" ||
                                ((RadioButton)controlObj[elementsInSheet[i]]).Name == "Option_Setting_Output_Lateral") && !str.Equals(""))
                            {
                                isPercentage = true;
                                LayoutCheckedChanged = true;
                                if (((RadioButton)controlObj[elementsInSheet[i]]).Name == "Option_Setting_Output_Vertical")
                                    Option_Setting_Output_Vertical_PreviousVal = obj[i + 2, 2];
                                else
                                    Option_Setting_Output_Lateral_PreviousVal = obj[i + 2, 2];
                            }
                        }
                        if (controlObj[elementsInSheet[i]] is CheckBox)
                        {
                            {
                                string str = null == obj[i + 2, 2] ? "" : obj[i + 2, 2].ToString();
                                if (str.ToLower().Equals("true"))
                                {
                                    ((CheckBox)controlObj[elementsInSheet[i]]).IsChecked = true;
                                }
                                else
                                {
                                    if (elementsInSheet[i].Contains("OutputUnweightbackedTotalCheck"))
                                        UnweightbackedBaseCheck.IsEnabled = false;
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
                if (!textAdded)
                {
                    CROSS_TXT_BX_ALL_CLRF = true;
                    CROSS_TXT_BX_ALL2_CLRF = true;
                    CROSS_TXT_BX_NONE_CLRF = true;
                }
                if (textAdded && !CROSS_TXT_BX_ALL2_CLRF)
                    CROSS_TXT_BX_ALL2_CLRF = true;
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
                        elementsInSheet.Add(keys[i]);
                    }
                }
                if (settingFirst)
                {
                    rar.Value2 = obj;

                }

                checkflag = true;

                if (this.Check_Summary_Mark_Ratio1.IsChecked == false)
                    Check_Difference_Marking_Check();
                if (this.Check_Summary_SignificantDifferece_Test.IsChecked == false)
                    Check_Summary_SignificantDifferece_Test_Check();
                if (this.Check_Summary_Rate_Difference1.IsChecked == false)
                    Check_Summary_Rate_Difference1_Check();
                if (this.Check_Summary_Rate_Difference2.IsChecked == false)
                    Check_Summary_Rate_Difference2_Check();
                if (this.Check_Summary_WeightBack.IsChecked == false)
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
                EnableDisableExportPathButton();
                Check_Summary_WeightBack_Check();
                checkcheckbox();
                Check_Summary_SignificantDifferece_Test_Check();
                if (!isPercentage)
                {
                    if (QC4Common.Common.Constants.GlobalMode.Split(',')[0] == "en-US")
                        Option_Setting_Output_Vertical.IsChecked = true;
                    else
                        Option_Setting_Output_Lateral.IsChecked = true;
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
            IsInitialized = true;
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
            //filtersettings initial values-----------------------------------------------------------------------------
            foreach (CheckBox tb
               in FindControls.FindLogicalChildren<CheckBox>(this.Page_Refine))
            {
                if (tb.Name == "Check_Refine_Condition")
                {
                    tb.IsChecked = false;
                    filtersettingcheckflag = false;
                    break;
                }
            }

            this.Check_Summary_Non1.IsChecked = true;
            this.Check_Summary_Combine_Banners.IsChecked = false;
            this.Check_Summary_Combine_Banners.IsEnabled = false;
            //this.Check_Summary_Non2.IsChecked = false;
            this.Text_Summary_Mark_N_Equal.Text = Constants.MarkingValue < 30 ? "30" : Constants.MarkingValue.ToString();
            this.Text_Summary_Rate_Difference1.Text = Constants.DifferenceSet1Value.ToString();
            this.Text_Summary_Rate_Difference2.Text = Constants.DifferenceSet2Value.ToString();
            this.Check_Summary_Mark_Ranking.IsChecked = false;
            this.Check_Summary_Mark_Ratio1.IsChecked = true;
            this.Combo_Summary_Rate_Difference1.SelectedIndex = 0;
            this.Combo_Summary_Rate_Difference2.SelectedIndex = 0;

            this.Combo_Summary_Rate_Difference1.IsEnabled = true;
            this.Combo_Summary_Rate_Difference2.IsEnabled = true;
            this.BTN_SET_1_UP.IsEnabled = true;

            this.BTN_SET_1_DOWN.IsEnabled = true;
            this.BTN_SET_2_UP.IsEnabled = true;
            this.BTN_SET_2_DOWN.IsEnabled = true;
            this.Text_Summary_Rate_Difference1.IsEnabled = true;
            this.Text_Summary_Rate_Difference2.IsEnabled = true;
            this.Check_Summary_SignificantDifferece_Test.IsChecked = false;

            this.Check_Par_99.IsChecked = false;
            this.Check_Par_95.IsChecked = true;
            this.Check_Par_90.IsChecked = true;
            this.Check_Par_99.IsEnabled = false;
            this.Check_Par_95.IsEnabled = false;
            this.Check_Par_90.IsEnabled = false;
            this.Check_Summary_Rate_Difference1.IsChecked = true;
            this.Check_Summary_Rate_Difference2.IsChecked = true;
            this.Check_Summary_Rate_Difference1.IsEnabled = true;
            this.Check_Summary_Rate_Difference2.IsEnabled = true;
            this.Check_Summary_WeightBack.IsChecked = false;
            this.OutputUnweightbackedTotalCheck.IsChecked = true;
            if (this.OutputUnweightbackedTotalCheck.IsChecked == true)
                this.UnweightbackedBaseCheck.IsEnabled = true;
            else
                this.UnweightbackedBaseCheck.IsEnabled = false;

            this.UnweightbackedBaseCheck.IsChecked = true;

            this.Option_Output_SheetType_One.IsChecked = true;
            this.Option_Output_SheetType_Plural.IsChecked = false;
            this.Check_Output_Cross_N_Par.IsChecked = true;
            this.Check_Output_Cross_N.IsChecked = true;
            this.Check_Output_Cross_Par.IsChecked = true;
        }
        private void Check_Difference_Marking_Checked(object sender, RoutedEventArgs e)
        {
            if (checkflag)
            {
                Check_Difference_Marking_Check();
            }
        }
        private void Check_Difference_Marking_Check()
        {
            if (Check_Summary_Mark_Ratio1.IsChecked == true)
            {
                this.Check_Summary_Rate_Difference1.IsChecked = false;
                this.Check_Summary_Rate_Difference2.IsChecked = false;
                this.Check_Summary_Rate_Difference1.IsEnabled = true;
                this.Check_Summary_Rate_Difference2.IsEnabled = true;
                this.Combo_Summary_Rate_Difference1.IsEnabled = false;
                this.Combo_Summary_Rate_Difference2.IsEnabled = false;
                this.BTN_SET_1_UP.IsEnabled = false;
                this.BTN_SET_1_DOWN.IsEnabled = false;
                this.BTN_SET_2_UP.IsEnabled = false;
                this.BTN_SET_2_DOWN.IsEnabled = false;
                this.Text_Summary_Rate_Difference1.IsEnabled = false;
                this.Text_Summary_Rate_Difference2.IsEnabled = false;
            }
            else
            {
                this.Check_Summary_Rate_Difference1.IsChecked = false;
                this.Check_Summary_Rate_Difference2.IsChecked = false;
                this.Check_Summary_Rate_Difference1.IsEnabled = false;
                this.Check_Summary_Rate_Difference2.IsEnabled = false;
            }
        }
        private void Check_Summary_Rate_Difference1_Checked(object sender, RoutedEventArgs e)
        {
            Check_Summary_Rate_Difference1_Check();
            HatchColorCommon.Combo_Color_Settings_Status_Update(this.Check_Summary_Rate_Difference1, this.Check_Summary_Rate_Difference2, this.Combo_Color_Settings_S);
        }
        private void Check_Summary_Rate_Difference1_Check()
        {
            if (Check_Summary_Rate_Difference1.IsChecked == true)
            {
                this.Combo_Summary_Rate_Difference1.IsEnabled = true;
                this.BTN_SET_1_UP.IsEnabled = true;
                this.BTN_SET_1_DOWN.IsEnabled = true;
                this.Text_Summary_Rate_Difference1.IsEnabled = true;
            }
            else
            {
                this.Combo_Summary_Rate_Difference1.IsEnabled = false;
                this.BTN_SET_1_UP.IsEnabled = false;
                this.BTN_SET_1_DOWN.IsEnabled = false;
                this.Text_Summary_Rate_Difference1.IsEnabled = false;
            }
        }
        private void Check_Summary_Rate_Difference2_Checked(object sender, RoutedEventArgs e)
        {
            Check_Summary_Rate_Difference2_Check();
            HatchColorCommon.Combo_Color_Settings_Status_Update(this.Check_Summary_Rate_Difference1, this.Check_Summary_Rate_Difference2, this.Combo_Color_Settings_S);
        }
        private void Check_Summary_Rate_Difference2_Check()
        {
            if (Check_Summary_Rate_Difference2.IsChecked == true)
            {
                this.Combo_Summary_Rate_Difference2.IsEnabled = true;
                this.BTN_SET_2_UP.IsEnabled = true;
                this.BTN_SET_2_DOWN.IsEnabled = true;
                this.Text_Summary_Rate_Difference2.IsEnabled = true;
            }
            else
            {
                this.Combo_Summary_Rate_Difference2.IsEnabled = false;
                this.BTN_SET_2_UP.IsEnabled = false;
                this.BTN_SET_2_DOWN.IsEnabled = false;
                this.Text_Summary_Rate_Difference2.IsEnabled = false;
            }
        }

        private void Check_Significance_Level_Checked(object sender, RoutedEventArgs e)
        {
            if (checkflag)
            {
                if (Check_Par_99.IsChecked == true && Check_Par_95.IsChecked == true && Check_Par_90.IsChecked == true)
                {
                    MessageDialog.ErrorOk(LocalResource.ERR_MSG_CROSS_SIGNIFICANCE_CHECK_ALL);//ERR_MSG_CROSS_SIGNIFICANCE_CHECK_ALL//MessageBox.Show("Only two can be selected at the same time");
                    CheckBox c = (CheckBox)sender;
                    c.IsChecked = false;
                }
            }
        }

        private void Check_Summary_SignificantDifferece_Test_Checked(object sender, RoutedEventArgs e)
        {
            if (checkflag)
            {
                Check_Summary_SignificantDifferece_Test_Check();
            }
        }
        private void Check_Summary_SignificantDifferece_Test_Check()
        {
            try
            {
                if (Check_Summary_SignificantDifferece_Test.IsChecked == true)
                {
                    this.rd_btn_chk.IsEnabled = true;
                    this.rd_btn_chk2.IsEnabled = true;

                    if (rd_btn_chk2.IsChecked == true)
                    {

                        this.Check_Par_99.IsEnabled = true;
                        this.Check_Par_95.IsEnabled = true;
                        this.Check_Par_90.IsEnabled = true;
                    }
                    else
                    {
                        this.Check_Par_99.IsEnabled = false;
                        this.Check_Par_95.IsEnabled = false;
                        this.Check_Par_90.IsEnabled = false;
                    }
                }
                else
                {
                    this.rd_btn_chk.IsEnabled = false;
                    this.rd_btn_chk2.IsEnabled = false;
                    this.Check_Par_99.IsEnabled = false;
                    this.Check_Par_95.IsEnabled = false;
                    this.Check_Par_90.IsEnabled = false;
                }
            }
            catch { }
        }
        private void Combo_SignificantDifference_Test_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {


            }
            catch { }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");//"[^0-9]+"

            e.Handled = regex.IsMatch(e.Text);
        }
        private void textBox_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Paste)/*e.Command == ApplicationCommands.Copy || e.Command == ApplicationCommands.Cut ||*/
            {
                e.Handled = true;
            }
        }

        private void text_LostFocus(object sender, RoutedEventArgs e)
        {
            int res = 0;
            if (((TextBox)sender).Name == "Text_Summary_Mark_N_Equal")
            {
                int maxVal = DB.DataOutput.RowCount(Workbook);
                if (this.Text_Summary_Mark_N_Equal.Text != null && this.Text_Summary_Mark_N_Equal.Text != "" && (int.TryParse(this.Text_Summary_Mark_N_Equal.Text.Replace(" ", string.Empty), out res)))
                {
                    int numval = Convert.ToInt32(this.Text_Summary_Mark_N_Equal.Text.Replace(" ", string.Empty));
                    if (numval > maxVal)
                    {
                        if (numval <= 30)
                            this.Text_Summary_Mark_N_Equal.Text = numval.ToString();
                        else
                        {
                            this.Text_Summary_Mark_N_Equal.Text = maxVal.ToString();
                        }
                        Text_Summary_Mark_N_Equal.Select(Text_Summary_Mark_N_Equal.Text.Length, 0);
                    }
                    else { this.Text_Summary_Mark_N_Equal.Text = numval.ToString(); }
                }
                else
                {
                    this.Text_Summary_Mark_N_Equal.Text = "30";
                }
            }
            else if (((TextBox)sender).Name == "Text_Summary_Rate_Difference1")
            {
                if (this.Text_Summary_Rate_Difference1.Text != null && this.Text_Summary_Rate_Difference1.Text != "" && (int.TryParse(this.Text_Summary_Rate_Difference1.Text, out res)))
                {

                    int numval = Convert.ToInt32(this.Text_Summary_Rate_Difference1.Text);
                    if (numval > Constants.DifferenceSetMaxValue)
                    {
                        this.Text_Summary_Rate_Difference1.Text = Constants.DifferenceSetMaxValue.ToString();
                    }
                    if (numval < Constants.DifferenceSetMinValue)
                    {
                        this.Text_Summary_Rate_Difference1.Text = Constants.DifferenceSetMinValue.ToString();
                    }
                }
                else
                {
                    this.Text_Summary_Rate_Difference1.Text = Constants.DifferenceSetMinValue.ToString();
                }
            }
            else if (((TextBox)sender).Name == "Text_Summary_Rate_Difference2")
            {
                if (this.Text_Summary_Rate_Difference2.Text != null && this.Text_Summary_Rate_Difference2.Text != "" && (int.TryParse(this.Text_Summary_Rate_Difference2.Text, out res)))
                {

                    int numval = Convert.ToInt32(this.Text_Summary_Rate_Difference2.Text);
                    if (numval > Constants.DifferenceSetMaxValue)
                    {
                        this.Text_Summary_Rate_Difference2.Text = Constants.DifferenceSetMaxValue.ToString();
                    }
                    if (numval < Constants.DifferenceSetMinValue)
                    {
                        this.Text_Summary_Rate_Difference2.Text = Constants.DifferenceSetMinValue.ToString();
                    }
                }
                else
                {
                    this.Text_Summary_Rate_Difference2.Text = Constants.DifferenceSetMinValue.ToString();
                }
            }

        }

        public void GetName_Page_Refine(Control x)
        {
            if (x is ComboBox)
                controlObj.Add(Name = "F_Cr_Cross_AddUp_" + ((ComboBox)x).Name + "_S", ((ComboBox)x));

            else if (x is TextBox)
                controlObj.Add(Name = "F_Cr_Cross_AddUp_" + ((TextBox)x).Name + "_S", ((TextBox)x));

            else if (x is RadioButton)
                controlObj.Add(Name = "F_Cr_Cross_AddUp_" + ((RadioButton)x).Name + "_S", ((RadioButton)x));

            else if (x is CheckBox)
                controlObj.Add(Name = "F_Cr_Cross_AddUp_" + ((CheckBox)x).Name + "_S", ((CheckBox)x));
        }
        public void GetName_Page_Summary(Control x)
        {
            ComboBox cmb = x as ComboBox;

            if (cmb != null)
            {
                // Check if the name of the ComboBox matches Combo_Color_Settings_S
                if (cmb.Name.Equals(this.Combo_Color_Settings_S.Name))
                {
                    // If it matches, add Combo_Color_Settings_S to the 'controlObj' dictionary.
                    controlObj.Add(cmb.Name, cmb);
                }
                else
                {
                    controlObj.Add(Name = "F_Cr_Cross_AddUp_" + ((ComboBox)x).Name + "_S", ((ComboBox)x));
                }

            }
            else if (x is TextBox)
            {
                controlObj.Add(Name = "F_Cr_Cross_AddUp_" + ((TextBox)x).Name + "_S", ((TextBox)x));
            }

            else if (x is RadioButton)
            {
                controlObj.Add(Name = "F_Cr_Cross_AddUp_" + ((RadioButton)x).Name + "_S", ((RadioButton)x));
            }

            else if (x is CheckBox)
            {
                controlObj.Add(Name = "F_Cr_Cross_AddUp_" + ((CheckBox)x).Name + "_S", ((CheckBox)x));
            }
        }
        private void BTN_CROSSTABULATE_Click(object sender, RoutedEventArgs e)
        {
            if (Check_Summary_SignificantDifferece_Test.IsChecked == true)
            {
                if (rd_btn_chk2.IsChecked == true)
                {
                    if (Check_Par_99.IsChecked == true && Check_Par_95.IsChecked == true && Check_Par_90.IsChecked == true)
                    {
                        MessageDialog.ErrorOk(LocalResource.CROSS_MSG_BOX_CHECK_PAR);
                        return;
                    }
                    if (Check_Par_99.IsChecked == false && Check_Par_95.IsChecked == false && Check_Par_90.IsChecked == false)
                    {
                        MessageDialog.ErrorOk(LocalResource.CROSS_MSG_UNCHECK_PAR);
                        return;
                    }
                }
            }

            if (Check_Output_Cross_N_Par.IsChecked == false && Check_Output_Cross_N.IsChecked == false && Check_Output_Cross_Par.IsChecked == false)
            {
                MessageDialog.ErrorOk(LocalResource.OUTPUT_SHEET_IS_NOT_SET);
                return;
            }
            if (isactiveAll() == false) { MessageBox.Show(LocalResource.CROSS_NOVALID_SETTING, "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error); return; }
            if (Check_Summary_WeightBack.IsChecked == true)
            {
                if (string.IsNullOrEmpty(Combo_Summary_WeightBack.Text))
                {
                    MessageDialog.ErrorOk(LocalResource.GT_NO_WEIGHTING);

                    return;
                }
            }
            if (validAxis1())
            {
                if (tabValidate())
                {
                    SetOptionSettingsMessage();
                    GetValuesOfControls();
                    SaveSettings();
                    Savestdvalues();
                    CloseNotFromBtn = false;
                    IsCreateReport = false;
                    this.Close();
                }
                else
                {
                    if (validdata == true)
                    {
                        MessageBox.Show(LocalResource.CROSS_NOVALID_SETTING, "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        validdata = true;
                    }
                    return;
                }
            }


        }
        private bool CheckValue(string textvalue, int categorycount, string type, string operatr)
        {
            return NumberCheck.CheckFromOption(textvalue, categorycount, type, operatr);
        }
        private bool BTN_CROSSTABULATE_Click(object sender1, RoutedEventArgs e1, bool isReport = false)
        {

            if (!CheckRefine())
                return false;

            if (checkUnprocessedNewQuestionDialog(Workbook))
            {
                return false;
            }

            SetOptionSettingsMessage();

            GetValuesOfControls();

            SaveSettings();

            SetForegroundWindow((IntPtr)Workbook.Application.Hwnd);

            return true;
        }

        private bool CheckRefine()
        {
            if (Check_Summary_WeightBack.IsChecked == true && Combo_Summary_WeightBack.Text == "")
            {
                MessageDialog.ErrorOk(LocalResource.GT_NO_WEIGHTING);
                return false;
            }
            if (Check_Summary_WeightBack.IsChecked == true && QC4Common.DB.DBHelper.checkNegetiveNumberInData(Workbook, Combo_Summary_WeightBack.Text))
            {
                MessageDialog.ErrorOk(LocalResource.WB_NEGATIVE_VALIDATION);
                return false;
            }
            if (Check_Summary_SignificantDifferece_Test.IsChecked == true)
            {
                if (rd_btn_chk2.IsChecked == true)
                {
                    if (Check_Par_99.IsChecked == true && Check_Par_95.IsChecked == true && Check_Par_90.IsChecked == true)
                    {
                        MessageDialog.ErrorOk(LocalResource.CROSS_MSG_BOX_CHECK_PAR);
                        return false;
                    }
                    if (Check_Par_99.IsChecked == false && Check_Par_95.IsChecked == false && Check_Par_90.IsChecked == false)
                    {
                        MessageDialog.ErrorOk(LocalResource.CROSS_MSG_UNCHECK_PAR);
                        return false;
                    }
                }
            }


            bool validpath = false;
            bool combohasvalue = false;
            foreach (ComboBox cb
      in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
            {
                if (cb.Name == "Combo_Classify_Item")
                {
                    if (cb.SelectedItem != null && (((FilterSettingsView.FilterSettingsClass.DataExport)cb.SelectedItem).QuestionVariable != null &&
                   ((FilterSettingsView.FilterSettingsClass.DataExport)cb.SelectedItem).QuestionVariable != ""))
                        combohasvalue = true;
                    break;
                }
            }
            foreach (TextBox tb
         in FindControls.FindLogicalChildren<TextBox>(this.Page_Refine))
            {
                if (tb.Name == "Combo_Classify_FolderPath" && (tb.Text != "" && tb.Text != null))
                {
                    validpath = true;
                    break;
                }
            }
            if (validpath == false && combohasvalue == true)
            {
                MessageDialog.ErrorOk(LocalResource.ERR_MSG_CROSS_PATH_NULL);
                return false;
            }

            if (Check_Summary_Mark_Ratio1.IsChecked == true)
            {
                if (Check_Summary_Rate_Difference1.IsChecked == false && Check_Summary_Rate_Difference2.IsChecked == false)
                {
                    MessageDialog.ErrorOk(LocalResource.ERR_MSG_CROSS_MARKING_DIFFERENCE);
                    return false;
                }
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
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        private void crossTabulate(bool isReport = false)
        {
            try
            {
                //using (new SingleGlobalInstance(10, Workbook, MainWindow)) //10ms timeout on global lock
                {
                    excel.Worksheet CrossSheet = ExcelUtil.GetWorkSheetByCodeName(Workbook, Constants.SheetCodeName.CrossTabulationS);
                    CrossTabulationQC crossTabulationQC = new CrossTabulationQC();
                    System.ComponentModel.BackgroundWorker backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
                    progress = new ProgressBar(LocalResource.TITLE_CROSS_TAB, backgroundWorker1);
                    WindowInteropHelper wih = new WindowInteropHelper(progress);
                    WindowInteropHelper wihMain = new WindowInteropHelper(MainWindow);
                    wih.Owner = wihMain.Handle;
                    IntPtr pbIntPtr = wihMain.Handle;
                    SetParent(wih.Handle, wihMain.Handle);//setting parent as xcel// SetParent((IntPtr)excelWorkbook.Application.Hwnd, wih.Handle);
                    crossTabulationQC.OnWorkerComplete += new CrossTabulationQC.OnWorkerMethodCompleteDelegate(OnWorkerMethodCompleteCR);
                    backgroundWorker1.WorkerReportsProgress = true;
                    backgroundWorker1.WorkerSupportsCancellation = true;
                    backgroundWorker1.DoWork += new DoWorkEventHandler((object sender, DoWorkEventArgs e) =>
                        crossTabulationQC.Tabulate(Workbook, CrossSheet, sender, e, isReport, window: MainWindow as Window, pb: pbIntPtr)
                    );
                    backgroundWorker1.RunWorkerAsync();
                    progress.ShowDialog();

                    if (crossTabulationQC.childExcelApp != IntPtr.Zero)
                    {
                        try
                        {
                            SetForegroundWindow(crossTabulationQC.childExcelApp);
                        }
                        catch { }
                    }
                }
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        public static bool checkUnprocessedNewQuestionDialog(excel.Workbook workBook)
        {
            if (CrossTabulationQC.checkUnprocessedNewQuestion(workBook))
            {
                System.Windows.Forms.DialogResult dialogResult = MessageDialog.ShowMessageOnWorkBook(LocalResource.ALERT_UN_PROCESSED_VARIABLE_EXIST, Enums.MessageType.InfoYesNo, workBook);
                if (dialogResult == System.Windows.Forms.DialogResult.No)
                {
                    MessageDialog.ShowMessageOnWorkBook(LocalResource.ALERT_EXECUTE_DP_OR_DELET_VAR, Enums.MessageType.Info, workBook);
                    return true;
                }
            }
            return false;
        }

        private void OnWorkerMethodComplete(double value, string status)
        {
            progress.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
            new Action(
            delegate ()
            {
                progress.UpdateProgressBar(value, status);
            }
            ));
        }

        private void OnWorkerMethodCompleteCR(double value, string status, bool isForceStop = false, bool retainThread = false, bool close = false,
            bool disableCancel = false)
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
        public bool singleGT()
        {


            foreach (var item in TabBinding.tabItems)
            {
                if (item.TabConten.Option_Setting_Summary_Single.IsChecked == true)
                {
                    if (item.TabConten.List_Setting_Cross_Hyotou.Items.Count > 0)
                    {


                        return true;
                    }

                }
            }

            return false;
        }
        bool validdata = true;
        bool onorendup = true;
        public bool tabValidate()
        {

            foreach (var item in TabBinding.tabItems)
            {
                if (item.isactive == true)
                {

                    if (item.TabConten.Option_Setting_Summary_Double.IsChecked == true)
                    {

                        if (item.TabConten.List_Setting_Cross_Hyosoku2.Items.Count == 0 || item.TabConten.List_Setting_Cross_Hyotou.Items.Count == 0)
                        {
                            if (item.TabConten.List_Setting_Cross_Hyosoku2.Items.Count > 0)
                            {
                                if (item.TabConten.List_Setting_Cross_Hyotou.Items.Count == 0)
                                {
                                    MessageBox.Show(LocalResource.CROSS_MSGBOX_3SIDE + "\n" + LocalResource.CROSS_MSGBOX_UNDETERMINATED + "[" + item.Header + "]", "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error);
                                    validdata = false;
                                    return false;
                                }
                            }
                            else if (item.TabConten.List_Setting_Cross_Hyotou.Items.Count > 0)
                            {
                                if (item.TabConten.List_Setting_Cross_Hyosoku2.Items.Count == 0)
                                {
                                    MessageBox.Show(LocalResource.CROSS_MSGBOX_2SIDE + "\n" + LocalResource.CROSS_MSGBOX_UNDETERMINATED + "[" + item.Header + "]", "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error);
                                    validdata = false;
                                    return false;
                                }
                            }
                        }

                        else if (item.TabConten.List_Setting_Cross_Hyosoku2.Items.Count > 0 && item.TabConten.List_Setting_Cross_Hyotou.Items.Count > 0)
                        {

                        }

                    }
                    if (item.TabConten.Option_Setting_Summary_Triple.IsChecked == true)
                    {
                        if (string.IsNullOrEmpty(item.TabConten.Text_Setting_Cross_Hyosoku1.Text) || item.TabConten.List_Setting_Cross_Hyosoku2.Items.Count == 0 || item.TabConten.List_Setting_Cross_Hyotou.Items.Count == 0)
                        {
                            if (!string.IsNullOrEmpty(item.TabConten.Text_Setting_Cross_Hyosoku1.Text))
                            {
                                if (item.TabConten.List_Setting_Cross_Hyosoku2.Items.Count == 0)
                                {
                                    MessageBox.Show(LocalResource.CROSS_MSGBOX_2SIDE + "\n" + LocalResource.CROSS_MSGBOX_UNDETERMINATED + "[" + item.Header + "]", "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error);
                                    validdata = false;
                                    return false;
                                }
                                if (item.TabConten.List_Setting_Cross_Hyotou.Items.Count == 0)
                                {
                                    MessageBox.Show(LocalResource.CROSS_MSGBOX_3SIDE + "\n" + LocalResource.CROSS_MSGBOX_UNDETERMINATED + "[" + item.Header + "]", "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error);
                                    validdata = false;
                                    return false;
                                }
                            }
                            else if (item.TabConten.List_Setting_Cross_Hyosoku2.Items.Count > 0)
                            {
                                if (string.IsNullOrEmpty(item.TabConten.Text_Setting_Cross_Hyosoku1.Text))
                                {
                                    MessageBox.Show(LocalResource.CROSS_MSGBOX_1SIDE + "\n" + LocalResource.CROSS_MSGBOX_UNDETERMINATED + "[" + item.Header + "]", "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error);
                                    validdata = false;
                                    return false;
                                }
                                if (item.TabConten.List_Setting_Cross_Hyotou.Items.Count == 0)
                                {
                                    MessageBox.Show(LocalResource.CROSS_MSGBOX_3SIDE + "\n" + LocalResource.CROSS_MSGBOX_UNDETERMINATED + "[" + item.Header + "]", "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error);
                                    validdata = false;
                                    return false;
                                }


                            }
                            else if (item.TabConten.List_Setting_Cross_Hyotou.Items.Count > 0)
                            {
                                if (string.IsNullOrEmpty(item.TabConten.Text_Setting_Cross_Hyosoku1.Text))
                                {
                                    MessageBox.Show(LocalResource.CROSS_MSGBOX_1SIDE + "\n" + LocalResource.CROSS_MSGBOX_UNDETERMINATED + "[" + item.Header + "]", "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error);
                                    validdata = false;
                                    return false;
                                }
                                if (item.TabConten.List_Setting_Cross_Hyosoku2.Items.Count == 0)
                                {
                                    MessageBox.Show(LocalResource.CROSS_MSGBOX_2SIDE + "\n" + LocalResource.CROSS_MSGBOX_UNDETERMINATED + "[" + item.Header + "]", "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error);
                                    validdata = false;
                                    return false;
                                }
                            }
                        }
                    }

                }
            }

            foreach (var item in TabBinding.tabItems)
            {
                if (item.TabConten.Option_Setting_Summary_Single.IsChecked == true)
                {
                    if (item.TabConten.List_Setting_Cross_Hyotou.Items.Count > 0)
                    {
                        if (item.isactive == false) { onorendup = false; } else { onorendup = true; return true; }

                    }


                }
                if (item.TabConten.Option_Setting_Summary_Triple.IsChecked == true)
                {
                    if (!string.IsNullOrEmpty(item.TabConten.Text_Setting_Cross_Hyosoku1.Text) && item.TabConten.List_Setting_Cross_Hyosoku2.Items.Count > 0 && item.TabConten.List_Setting_Cross_Hyotou.Items.Count > 0)
                    {
                        if (item.isactive == false) { onorendup = false; } else { onorendup = true; return true; }

                    }
                }
                if (item.TabConten.Option_Setting_Summary_Double.IsChecked == true)
                {
                    if (item.TabConten.List_Setting_Cross_Hyosoku2.Items.Count > 0 && item.TabConten.List_Setting_Cross_Hyotou.Items.Count > 0)
                    {
                        if (item.isactive == false) { onorendup = false; } else { onorendup = true; return true; }

                    }
                }
            }

            return false;
        }
        public bool isactiveAll()
        {
            foreach (var item in TabBinding.tabItems)
            {
                if (item.isactive == true)
                {
                    return true;
                }
            }
            return false;
        }

        private void BTN_CREATEREPORT_Click(object sender, RoutedEventArgs e)
        {


            if (Check_Summary_SignificantDifferece_Test.IsChecked == true)
            {
                if (rd_btn_chk2.IsChecked == true)
                {
                    if (Check_Par_99.IsChecked == true && Check_Par_95.IsChecked == true && Check_Par_90.IsChecked == true)
                    {
                        MessageDialog.ErrorOk(LocalResource.CROSS_MSG_BOX_CHECK_PAR);
                        return;
                    }
                    if (Check_Par_99.IsChecked == false && Check_Par_95.IsChecked == false && Check_Par_90.IsChecked == false)
                    {
                        MessageDialog.ErrorOk(LocalResource.CROSS_MSG_UNCHECK_PAR);
                        return;
                    }
                }
            }




            if (Check_Summary_WeightBack.IsChecked == true)
            {
                if (string.IsNullOrEmpty(Combo_Summary_WeightBack.Text))
                {
                    MessageDialog.ErrorOk(LocalResource.GT_NO_WEIGHTING);
                    return;
                }
            }
            if (isactiveAll() == false) { MessageBox.Show(LocalResource.CROSS_NOVALID_SETTING, "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error); return; }
            if (validAxis1())
            {

                if (tabValidate())
                {
                    if (onorendup == false) { MessageBox.Show(LocalResource.CROSS_NOVALID_SETTING, "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error); return; }

                    SetOptionSettingsMessage();
                    GetValuesOfControls();
                    SaveSettings();
                    Savestdvalues();
                    CloseNotFromBtn = false;
                    IsCreateReport = true;
                    this.Close();
                }
                else
                {
                    if (validdata == true)
                    {
                        MessageBox.Show(LocalResource.CROSS_NOVALID_SETTING, "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        validdata = true;
                    }
                    return;
                }
            }


        }

        public void readEXcel()
        {
            elementsInSheet.Clear();
            var SettingSheet = ExcelUtil.GetWorkSheetByCodeName(Workbook, Constants.SheetCodeName.DetailsSetting);

            Microsoft.Office.Interop.Excel.Range last = SettingSheet.Cells.SpecialCells(Microsoft.Office.Interop.Excel.XlCellType.xlCellTypeLastCell, Type.Missing);

            Microsoft.Office.Interop.Excel.Range rar = SettingSheet.get_Range("A1", last);
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

        public void GetValue_Page_Refine(Control x)
        {
            if (x is ComboBox)
            {
                var myObject = ((ComboBox)x).SelectedItem as FilterSettingsClass.DataExport;

                if (myObject != null)
                    ReadValueFromExcel.Add("F_Cr_Cross_AddUp_" + ((ComboBox)x).Name + "_S", ((FilterSettingsClass.DataExport)((ComboBox)x).SelectedItem).QuestionVariable);
                else
                    ReadValueFromExcel.Add("F_Cr_Cross_AddUp_" + ((ComboBox)x).Name + "_S", ((ComboBox)x).Text);
            }
            else if (x is TextBox)
                ReadValueFromExcel.Add("F_Cr_Cross_AddUp_" + ((TextBox)x).Name + "_S", ((TextBox)x).Text);
            else if (x is RadioButton)
                ReadValueFromExcel.Add("F_Cr_Cross_AddUp_" + ((RadioButton)x).Name + "_S", ((RadioButton)x).IsChecked.ToString());

            else if (x is CheckBox)
                ReadValueFromExcel.Add("F_Cr_Cross_AddUp_" + ((CheckBox)x).Name + "_S", ((CheckBox)x).IsChecked.ToString());
        }
        public void GetTabsetting(Control x)
        {
            if (x is RadioButton)
                ReadValueFromExcel.Add("F_Cr_Cross_AddUp_" + ((RadioButton)x).Name + "_S", ((RadioButton)x).IsChecked.ToString());

        }


        public void Get_setting_data(Control x)
        {
            if (x is RadioButton)
                controlObj.Add(Name = "F_Cr_Cross_AddUp_" + ((RadioButton)x).Name + "_S", ((RadioButton)x));
        }
        public void GetSettingVal(Control x)
        {
            if (x is RadioButton)
                ReadValueFromExcel.Add("F_Cr_Cross_AddUp_" + ((RadioButton)x).Name + "_S", ((RadioButton)x).IsChecked.ToString());
        }


        public void GetValue_Page_Summary(Control x)
        {
            ComboBox cmb = x as ComboBox;

            if (cmb != null)
            {
                if (((ComboBox)x).Name == "Combo_Summary_WeightBack")
                {
                    var myObject = ((ComboBox)x).SelectedItem as DataExport;

                    if (myObject != null)
                        ReadValueFromExcel.Add("F_Cr_Cross_AddUp_" + ((ComboBox)x).Name + "_S", ((DataExport)((ComboBox)x).SelectedItem).QuestionVariable);
                    else
                        ReadValueFromExcel.Add("F_Cr_Cross_AddUp_" + ((ComboBox)x).Name + "_S", ((ComboBox)x).Text);
                }
                // Check if the name of the ComboBox matches Combo_Color_Settings_S.
                else if (cmb.Name.Equals(this.Combo_Color_Settings_S.Name))
                {
                    // If the name matches Combo_Color_Settings_S, read the selected value from the ComboBox and add it to ReadValueFromExcel.
                    ReadValueFromExcel.Add(cmb.Name, (string)cmb.SelectedValue);
                }
                else
                    ReadValueFromExcel.Add("F_Cr_Cross_AddUp_" + ((ComboBox)x).Name + "_S", ((ComboBox)x).SelectedIndex.ToString());
            }
            else if (x is TextBox)
                ReadValueFromExcel.Add("F_Cr_Cross_AddUp_" + ((TextBox)x).Name + "_S", ((TextBox)x).Text);
            else if (x is RadioButton)
                ReadValueFromExcel.Add("F_Cr_Cross_AddUp_" + ((RadioButton)x).Name + "_S", ((RadioButton)x).IsChecked.ToString());
            else if (x is CheckBox)
                ReadValueFromExcel.Add("F_Cr_Cross_AddUp_" + ((CheckBox)x).Name + "_S", ((CheckBox)x).IsChecked.ToString());
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
                txt_waitback.Visibility = Visibility.Visible;
                this.Combo_Summary_WeightBack.IsEnabled = true;
                this.BTN_WEIGHT.IsEnabled = true;
                this.OutputUnweightbackedTotalCheck.IsEnabled = true;
                if (this.OutputUnweightbackedTotalCheck.IsChecked == true)
                    this.UnweightbackedBaseCheck.IsEnabled = true;

            }
            else
            {
                txt_waitback.Visibility = Visibility.Hidden;

                this.Combo_Summary_WeightBack.IsEnabled = false;
                this.BTN_WEIGHT.IsEnabled = false;
                this.OutputUnweightbackedTotalCheck.IsEnabled = false;
                this.UnweightbackedBaseCheck.IsEnabled = false;

            }
        }
        private void OutputUnweightbackedTotalCheck_Checked(object sender, RoutedEventArgs e)
        {
            if (checkflag)
            {
                if (OutputUnweightbackedTotalCheck.IsChecked == true)
                {
                    this.UnweightbackedBaseCheck.IsEnabled = true;
                }
                else
                {
                    this.UnweightbackedBaseCheck.IsEnabled = false;
                }
            }
        }
        private QC4Common.Util.FormUtil formUtil = new QC4Common.Util.FormUtil();
        public bool validAxis1()
        {
            bool isfine = true;
            Dictionary<string, QuestionSettings> variableDictionary1 = new Dictionary<string, QuestionSettings>();
            variableDictionary1 = Definiotion.VariableDictionary;
            foreach (var item in TabBinding.tabItems)
            {

                try
                {
                    if (item.TabConten.Option_Setting_Summary_Triple.IsChecked == true)
                    {
                        if (!string.IsNullOrEmpty(item.TabConten.Text_Setting_Cross_Hyosoku1.Text))
                        {

                            bool gtoke = true;
                            string res1 = string.Empty;
                            string uppercase = (item.TabConten.Text_Setting_Cross_Hyosoku1.Text);
                            if (variableDictionary1.ContainsKey(uppercase))
                            {

                            }
                            else
                            {
                                gtoke = false;
                            }
                            if (gtoke == false)
                            {
                                res1 = variableDictionary1[item.TabConten.Text_Setting_Cross_Hyosoku1.Text].AnswerType;
                            }
                            else
                            {
                                res1 = variableDictionary1[uppercase].AnswerType;
                            }

                            if (res1 == "N" || res1 == "FA" || res1 == "D")
                            {
                                MessageBox.Show(LocalResource.CROSS_INAVLID_ITEM_AXIS1, "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error);
                                return false;
                            }
                            else
                            {


                            }

                        }

                        if (item.TabConten.List_Setting_Cross_Hyosoku2.Items.Count > 0)
                        {
                            foreach (var item1 in item.TabConten._lst_row2_list)
                            {
                                string data = item1.Variable;
                                if (variableDictionary1.ContainsKey(data))
                                {
                                    var Result = variableDictionary1[data];
                                    if (Result.AnswerType == "N" || Result.AnswerType == "FA" || Result.AnswerType == "D")
                                    {
                                        MessageBox.Show(LocalResource.CROSS_INAVLID_ITEM_AXIS2, "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error);
                                        return false;
                                    }
                                }
                            }
                        }
                        if (item.TabConten.List_Setting_Cross_Hyotou.Items.Count > 0)
                        {
                            foreach (var item1 in item.TabConten._lst_row3_list)
                            {
                                string data = item1.Variable;
                                if (variableDictionary1.ContainsKey(data))
                                {
                                    var Result = variableDictionary1[data];
                                    if (Result.AnswerType == "FA" || Result.AnswerType == "D")
                                    {
                                        MessageBox.Show(LocalResource.CROSS_INAVLID_ITEM_AXIS3, "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error);
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                    else if (item.TabConten.Option_Setting_Summary_Double.IsChecked == true)
                    {
                        if (item.TabConten.List_Setting_Cross_Hyosoku2.Items.Count > 0)
                        {
                            foreach (var item1 in item.TabConten._lst_row2_list)
                            {
                                string data = item1.Variable;
                                if (variableDictionary1.ContainsKey(data))
                                {
                                    var Result = variableDictionary1[data];
                                    if (Result.AnswerType == "N" || Result.AnswerType == "FA" || Result.AnswerType == "D")
                                    {
                                        MessageBox.Show(LocalResource.CROSS_INAVLID_ITEM_AXIS2, "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error);
                                        return false;
                                    }
                                }
                            }
                        }
                        if (item.TabConten.List_Setting_Cross_Hyotou.Items.Count > 0)
                        {
                            foreach (var item1 in item.TabConten._lst_row3_list)
                            {
                                string data = item1.Variable;
                                if (variableDictionary1.ContainsKey(data))
                                {
                                    var Result = variableDictionary1[data];
                                    if (Result.AnswerType == "FA" || Result.AnswerType == "D")
                                    {
                                        MessageBox.Show(LocalResource.CROSS_INAVLID_ITEM_AXIS3, "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error);
                                        return false;
                                    }
                                }
                            }
                        }
                    }

                    else if (item.TabConten.Option_Setting_Summary_Single.IsChecked == true)
                    {
                        if (item.TabConten.List_Setting_Cross_Hyotou.Items.Count > 0)
                        {
                            foreach (var item1 in item.TabConten._lst_row3_list)
                            {
                                string data = item1.Variable;
                                if (variableDictionary1.ContainsKey(data))
                                {
                                    var Result = variableDictionary1[data];
                                    if (Result.AnswerType == "FA" || Result.AnswerType == "D")
                                    {
                                        MessageBox.Show(LocalResource.CROSS_INAVLID_ITEM_AXIS3, "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error);
                                        return false;
                                    }
                                }
                            }
                        }
                    }

                    else
                    {
                        if (!CloseNotFromBtn)
                        {

                        }
                    }
                }
                catch
                {


                    isfine = false;
                }
            }
            return isfine;
        }
        private void BTN_CLOSE_Click(object sender, RoutedEventArgs e)
        {


            this.Close();

        }

        private void SaveSettings()
        {
            var SettingSheet = ExcelUtil.GetWorkSheetByCodeName(Workbook, Constants.SheetCodeName.DetailsSetting);
            int rcell = SettingSheet.UsedRange.Rows.Count;
            rcell = rcell + 3;
            string ragecell = "B" + rcell.ToString();
            Microsoft.Office.Interop.Excel.Range rar = SettingSheet.get_Range("A1", ragecell);
            var obj = rar.Value;
            int rowvalue = rar.Rows.Count;

            for (int i = 2; i < rowvalue; i++)
            {
                if (obj[i, 1] != null)
                {
                    if (ReadValueFromExcel.ContainsKey(elementsInSheet[i - 2]))
                    {
                        if (obj[i, 1].ToString() == "F_Cr_Cross_AddUp_Text_Summary_Change_Hyoutou_S" || obj[i, 1].ToString() == "F_Cr_Cross_AddUp_Text_Summary_Change_Non_S"
                            || obj[i, 1].ToString() == "F_Cr_Cross_AddUp_Text_Summary_Change_Hyosoku_S")
                        {
                            if (obj[i, 1].ToString() == "F_Cr_Cross_AddUp_Text_Summary_Change_Hyoutou_S")
                            {
                                obj[i, 2] = ReadValueFromExcel[obj[i, 1]] == LocalResource.CROSS_TXT_BX_ALL ? CROSS_TXT_BX_ALL_CLRF ? QC4Common.Common.Constants.CRLFchar : ReadValueFromExcel[obj[i, 1]] : ReadValueFromExcel[obj[i, 1]];
                            }
                            else if (obj[i, 1].ToString() == "F_Cr_Cross_AddUp_Text_Summary_Change_Hyosoku_S")
                            {
                                obj[i, 2] = ReadValueFromExcel[obj[i, 1]] == LocalResource.CROSS_TXT_BX_ALL ? CROSS_TXT_BX_ALL2_CLRF ? QC4Common.Common.Constants.CRLFchar : ReadValueFromExcel[obj[i, 1]] : ReadValueFromExcel[obj[i, 1]];
                            }
                            else if (obj[i, 1].ToString() == "F_Cr_Cross_AddUp_Text_Summary_Change_Non_S")
                            {
                                obj[i, 2] = ReadValueFromExcel[obj[i, 1]] == LocalResource.CROSS_TXT_BX_NOANSWER ? CROSS_TXT_BX_NONE_CLRF ? QC4Common.Common.Constants.CRLFchar : ReadValueFromExcel[obj[i, 1]] : ReadValueFromExcel[obj[i, 1]];
                            }
                        }
                        else
                        {
                            obj[i, 2] = ReadValueFromExcel[obj[i, 1]];
                        }
                    }

                    if ((obj[i, 1].ToString() == "F_Cr_Cross_AddUp_Option_Setting_Output_Lateral_S" || obj[i, 1].ToString() == "F_Cr_Cross_AddUp_Option_Setting_Output_Vertical_S")
                        && !LayoutCheckedChanged)
                    {
                        if (obj[i, 1].ToString() == "F_Cr_Cross_AddUp_Option_Setting_Output_Lateral_S")
                            obj[i, 2] = Option_Setting_Output_Lateral_PreviousVal;
                        else
                            obj[i, 2] = Option_Setting_Output_Vertical_PreviousVal;
                        if (QC4Common.Common.Constants.GlobalMode.Split(',')[0] == "ja-JP")
                            QC4Common.Common.Constants.IsRow = true;
                        else
                            QC4Common.Common.Constants.IsRow = false;
                    }
                }
                else
                {
                    break;
                }
            }
            if (LayoutCheckedChanged)
            {
                QC4Common.Common.Constants.IsRow = null;
            }
            rar.Value2 = obj;
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            //Workbook.Save();
        }

        private void ViewMsgInCrossSheet()
        {

            string crossmessage = "";


            foreach (ComboBox cb
     in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
            {
                if (cb.Name == "Combo_Classify_Item")
                {
                    if (cb.SelectedItem != null && (((FilterSettingsView.FilterSettingsClass.DataExport)cb.SelectedItem).QuestionVariable != null &&
                   ((FilterSettingsView.FilterSettingsClass.DataExport)cb.SelectedItem).QuestionVariable != ""))
                        crossmessage += string.Format(LocalResource.INFO_CROSS_SHEET_MSG_BASE_VARIABLE_SET, ((FilterSettingsView.FilterSettingsClass.DataExport)cb.SelectedItem).QuestionVariable);
                }
            }

            foreach (CheckBox tb
               in FindControls.FindLogicalChildren<CheckBox>(this.Page_Refine))
            {
                if (tb.Name == "Check_Refine_Condition")
                {
                    if (tb.IsChecked == true)
                    {
                        crossmessage += LocalResource.INFO_CROSS_SHEET_MSG_FILTERING_CRITERIA_SET;
                    }
                }
            }
            if (crossmessage != "")
            {
                crossmessage += "\n";
                crossmessage += LocalResource.INFO_CROSS_SHEET_MSG_MAKE_CHANGE_FROM_OPTIONS;
            }
            //write in excel file  sheet of cross k2 cell
            excel.Worksheet CrossSheet = ExcelUtil.GetWorkSheetByCodeName(Workbook, Constants.SheetCodeName.CrossTabulation);
            Microsoft.Office.Interop.Excel.Range crossrange = CrossSheet.get_Range("m2");
            crossrange.Font.Color = System.Drawing.Color.Red;
            crossrange.Value = crossmessage;
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
                        OptionSettingsMsg.TextFrame.Characters(Type.Missing, Type.Missing).Text += Environment.NewLine + LocalResource.FILTER_CRITERIA_MSG2 + Environment.NewLine + LocalResource.FILTER_CRITERIA_MSG3;
                    else
                        OptionSettingsMsg.TextFrame.Characters(Type.Missing, Type.Missing).Text += Environment.NewLine + LocalResource.FILTER_CRITERIA_MSG3;
                }
                else if (IsChecked == true && CmbCriteria.SelectedIndex > 0)
                {
                    if (IsChecked == true && CmbCriteria.SelectedIndex > 0)
                        OptionSettingsMsg.TextFrame.Characters(Type.Missing, Type.Missing).Text += LocalResource.FILTER_CRITERIA_MSG2 + Environment.NewLine + LocalResource.FILTER_CRITERIA_MSG3;
                }
            }
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
            catch (Exception ex) { _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException); }
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

            foreach (ComboBox tb
                in FindControls.FindLogicalChildren<ComboBox>(this.Page_Summary))
            {
                GetValue_Page_Summary(tb);
            }
            foreach (RadioButton tb
                in FindControls.FindLogicalChildren<RadioButton>(this.Page_Summary))
            {
                GetValue_Page_Summary(tb);
            }

            foreach (TextBox tb
                in FindControls.FindLogicalChildren<TextBox>(this.Page_Summary))
            {
                GetValue_Page_Summary(tb);
            }
            foreach (CheckBox tb
                in FindControls.FindLogicalChildren<CheckBox>(this.Page_Summary))
            {
                GetValue_Page_Summary(tb);
            }

            foreach (RadioButton tb
                in FindControls.FindLogicalChildren<RadioButton>(this.Tabulation_setting))
            {
                GetSettingVal(tb);
            }
        }
        private void Check_Output_Settings_Unchecked(object sender, RoutedEventArgs e)
        {
            if (checkflag)
            {
                if (Check_Output_Cross_N_Par.IsChecked == false && Check_Output_Cross_N.IsChecked == false && Check_Output_Cross_Par.IsChecked == false)
                {
                    BTN_CROSSTABULATE.IsEnabled = false;
                    BTN_CLOSE.IsEnabled = false;
                }
                else
                {
                    BTN_CROSSTABULATE.IsEnabled = true;
                    BTN_CLOSE.IsEnabled = true;
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
        //Code Below For Filter Settings UI------------------------------------------------------------------------------------------------------------------------
        FilterControlDesign fdobj = new FilterControlDesign();
        private void OnSelectionChanged(object sender, FilterControlDesign.MyComboBoxSelectionChangedEventArgs e)//use for ComboSelectionChange in filtersettings usercontrol
        {
            if (e.sendr == "Combo_Classify_Item")
            {
                if (e.SelectedIndex > 0)
                {
                    txt_classific_item.Visibility = Visibility.Visible;
                }
                else
                {
                    txt_classific_item.Visibility = Visibility.Hidden;
                }
                if (((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).QuestionVariableType == null || ((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).QuestionVariableType == "")
                {
                    foreach (TextBox tb
              in FindControls.FindLogicalChildren<TextBox>(this.Page_Refine))
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
                    foreach (Button tb
          in FindControls.FindLogicalChildren<Button>(this.Page_Refine))
                    {
                        if (tb.Name == "exportpathbtn") tb.IsEnabled = false;
                    }
                    return;
                }
                foreach (TextBox tb
               in FindControls.FindLogicalChildren<TextBox>(this.Page_Refine))
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
                //fdobj.SetCriteriaValues(anstype,noofchoices,question);
            }
            else if (e.sendr == "Combo_Conditional_Item_1")
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
        private void OnCheck(object sender, FilterControlDesign.MyCheckBoxClickEventArgs e)
        {
            if (e.sendr == "Check_Refine_Condition")
            {
                if ((e.Check))
                {
                    txt_refinement.Visibility = Visibility.Visible;
                }
                else
                {
                    txt_refinement.Visibility = Visibility.Hidden;
                }
            }
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
        private void EnableDisableExportPathButton()
        {
            bool enableexportpathbtn = false;

            foreach (ComboBox cb
      in FindControls.FindLogicalChildren<ComboBox>(this.Page_Refine))
            {
                if (cb.Name == "Combo_Classify_Item")// Combo_Conditional_Item_1
                {
                    if (cb.SelectedItem != null && (((FilterSettingsView.FilterSettingsClass.DataExport)cb.SelectedItem).QuestionVariable != null &&
                   ((FilterSettingsView.FilterSettingsClass.DataExport)cb.SelectedItem).QuestionVariable != ""))
                        enableexportpathbtn = true;
                    break;
                }
            }
            foreach (Button tb
          in FindControls.FindLogicalChildren<Button>(this.Page_Refine))
            {
                if (tb.Name == "exportpathbtn")
                {
                    tb.IsEnabled = enableexportpathbtn;
                    break;
                }
            }
        }
        private static readonly string[] SuggestionValues = {
            "DK"
        };
        private void OnTextBoxChange(object sender, FilterSettingsView.FilterControlDesign.MyTextBoxChangeEventArgs e)//TextChangedEventArgs args
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

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            //SetForegroundWindow((IntPtr)Workbook.Application.Hwnd);
            if (!CloseNotFromBtn)
            {

                if (!CheckRefine())
                {
                    CloseNotFromBtn = true;
                    e.Cancel = true;
                    return;
                }
                if (checkUnprocessedNewQuestionDialog(Workbook))
                {
                    CloseNotFromBtn = true;
                    e.Cancel = true;
                    return;
                }
            }
            else
            {

                if (!CheckRefine())
                {
                    CloseNotFromBtn = true;
                    e.Cancel = true;
                    return;
                }
                SetOptionSettingsMessage();
                GetValuesOfControls();
                SaveSettings();
                Savestdvalues();
                Workbook.Application.Cursor = excel.XlMousePointer.xlDefault;
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
                numval = Convert.ToInt32(Text_Summary_Rate_Difference1.Text);
                numval++;
                if (numval > Constants.DifferenceSetMaxValue)
                    numval = Constants.DifferenceSetMaxValue;
                Text_Summary_Rate_Difference1.Text = numval.ToString();
            }
            else if (Sender.Name == "BTN_SET_2_UP")
            {
                numval = Convert.ToInt32(Text_Summary_Rate_Difference2.Text);
                numval++;
                if (numval > Constants.DifferenceSetMaxValue)
                    numval = Constants.DifferenceSetMaxValue;
                Text_Summary_Rate_Difference2.Text = numval.ToString();
            }
            else if (Sender.Name == "BTN_SET_1_DOWN")
            {
                numval = Convert.ToInt32(Text_Summary_Rate_Difference1.Text);
                numval--;
                if (numval < Constants.DifferenceSetMinValue)
                    numval = Constants.DifferenceSetMinValue;
                Text_Summary_Rate_Difference1.Text = numval.ToString();
            }
            else if (Sender.Name == "BTN_SET_2_DOWN")
            {
                numval = Convert.ToInt32(Text_Summary_Rate_Difference2.Text);
                numval--;
                if (numval < Constants.DifferenceSetMinValue)
                    numval = Constants.DifferenceSetMinValue;
                Text_Summary_Rate_Difference2.Text = numval.ToString();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow.Show();
            if (!CloseNotFromBtn)
            {
                try
                {
                    crossTabulate(IsCreateReport);
                }
                catch (Exception ex)
                {
                    if (!(ex is TimeoutException))
                        _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                }
            }
        }

        private void Combo_Classify_Item_Loaded(object sender, RoutedEventArgs e)
        {

        }
        int LastSelected = 0;
        private void Combo_Summary_WeightBack_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            System.Windows.Controls.ComboBox sen = ((System.Windows.Controls.ComboBox)sender);
            if ((Key.Delete == e.Key || ((sen.SelectedIndex == -1 && sen.Text == "") || (sen.SelectedIndex == -1 && sen.Text != ""))) && (this.Combo_Summary_WeightBack.IsKeyboardFocusWithin || this.Combo_Summary_WeightBack.IsDropDownOpen))
            {
                LastSelected = 0;
                this.Combo_Summary_WeightBack.SelectedIndex = 0;
            }
        }
        System.Windows.Controls.ComboBox combo = null;
        string LastSelectedText = "";
        private void Combo_Summary_WeightBack_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Combo_Summary_WeightBack.SelectedIndex != 0)
                LastSelected = Combo_Summary_WeightBack.SelectedIndex;
            else if (Combo_Summary_WeightBack.SelectedIndex == 0)
            {
                LastSelectedText = Combo_Summary_WeightBack.Text;
            }
            if (Key.Delete == e.Key && (this.Combo_Summary_WeightBack.IsKeyboardFocusWithin || this.Combo_Summary_WeightBack.IsDropDownOpen))
            {
                LastSelectedText = "";
                LastSelected = -1;
                e.Handled = false;
            }
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Combo_Summary_WeightBack_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {

        }

        private void Combo_Classify_Item_DropDownClosed(object sender, EventArgs e)
        {

            if (Combo_Summary_WeightBack.SelectedItem != null)
            {
                Combo_Summary_WeightBack.Text = ((DataExport)(Combo_Summary_WeightBack.SelectedItem)).QuestionVariable;
            }
            else
            {
                Combo_Summary_WeightBack.Text = "";
            }
        }
        ObservableCollection<VMTabItems> tabItemsdet = new ObservableCollection<VMTabItems>();
        public void Savestdvalues()
        {
            // Dictionary<string, QuestionDetails> csd = new Dictionary<string, QuestionDetails>();
            Dictionary<string, QuestionSettings> variableDictionary1 = new Dictionary<string, QuestionSettings>();
            variableDictionary1 = Definiotion.VariableDictionary;
            try
            {
                tabItemsdet = TabBinding.tabItems;
                int n = 0;
                foreach (var cc in tabItemsdet)
                {
                    if (cc.TabConten._lst_row3_list.Count > 0)
                    {
                        foreach (var cc1 in cc.TabConten._lst_row3_list)
                        {
                            n++;
                        }
                    }
                    else
                    {
                        n++;
                    }
                }
                int m = 0;
                foreach (var cc in tabItemsdet)
                {
                    if (cc.TabConten._lst_row2_list.Count > 0)
                    {
                        foreach (var cc1 in cc.TabConten._lst_row2_list)
                        {
                            m++;
                        }
                    }
                    else
                    {
                        m++;
                    }
                }
                m = m + 7;
                n = n + 15;

                Workbook = Workbook;


                var res = Util.ExcelUtil.GetWorkSheetByCodeName(Workbook, Constants.SheetCodeName.CrossTabulationS);



                excel.Range crstart = res.Cells[1, 1];
                excel.Range crlast = res.Cells[n, m];
                // excel.Range rar = res.get_Range(crstart, crlast);
                excel.Range rar = res.UsedRange;

                object om = rar.Value;




                //int maxRow = array.GetLength(0);
                //int MaxCol= array.GetLength(1);

                int rowval = rar.Rows.Count;
                int colval = rar.Columns.Count;

                if (n >= rowval)
                {
                    rar = res.get_Range(crstart, crlast);
                }


                int last = crlast.Count;
                int index = 15;
                int endRow = 12;

                if (res.Cells[1, 7] != null)
                {
                    excel.Range startCell = res.Cells[1, 7];
                    excel.Range endcell = res.Cells[rowval, colval];
                    excel.Range myRange = res.Range[startCell, endcell];
                    var ob = myRange.Value;
                    if (ob[1, 1] != null)
                    {
                        excel.Range rng = myRange.SpecialCells(excel.XlCellType.xlCellTypeConstants);
                        rng.Value = null;

                        // rar.Value = rng.Value;
                    }

                }
                var sd = res.Cells[15, 1];
                if (res.Cells[15, 1] != null)//errror
                {

                    excel.Range startCel2 = res.Cells[15, 1];
                    excel.Range endCell2 = res.Cells[rowval, colval];
                    excel.Range myrange1 = res.Range[startCel2, endCell2];
                    var ob = myrange1.Value;
                    if (ob[1, 1] != null)
                    {
                        excel.Range rng2 = myrange1.SpecialCells(excel.XlCellType.xlCellTypeConstants);
                        rng2.Value = null;
                        //rar.Value = rng2.Value;
                    }


                    int ad = 0;
                }

                var array = rar.Value;


                if (tabItemsdet.Count > 0)
                {

                    int i = 15;
                    int j = 7;

                    foreach (var item in tabItemsdet)
                    {
                        string variable = "";
                        string anstp = "";
                        string catcount = "";
                        string isdatacheck = "";

                        string Onorofff = "●";
                        if (item.isactive == false)
                        {
                            Onorofff = "○";
                        }
                        if (item.TabConten.Check_Setting_Cross_Group.IsChecked == true)
                        {
                            isdatacheck = "●";
                        }

                        if (item.TabConten.Option_Setting_Summary_Triple.IsChecked == true)
                        {
                            string CatId = "";
                            if (item.TabConten.Text_Setting_Cross_Hyosoku1.Text != null || item.TabConten.Text_Setting_Cross_Hyosoku1.Text != "")
                            {


                                try
                                {

                                    if (variableDictionary1.ContainsKey(item.TabConten.Text_Setting_Cross_Hyosoku1.Text))
                                    {
                                        var res1 = variableDictionary1[item.TabConten.Text_Setting_Cross_Hyosoku1.Text];

                                        variable = item.TabConten.Text_Setting_Cross_Hyosoku1.Text;
                                        anstp = res1.AnswerType;
                                        catcount = res1.CategoryCount.ToString();

                                    }
                                    else
                                    {

                                    }

                                }
                                catch
                                {
                                }

                            }





                            if (item.TabConten._lst_row2_list.Count > 0)
                            {
                                List<int> des = new List<int>();
                                foreach (var item1 in item.TabConten._lst_row2_list)
                                {
                                    int num = 0;

                                    num++;


                                    array[5, j] = variable;
                                    array[6, j] = anstp;
                                    array[7, j] = catcount;
                                    array[8, j] = item1.Variable;

                                    array[9, j] = item1.AnswerType.Split('/')[0]; ;
                                    array[10, j] = item1.CategoryCount;

                                    des = new List<int>();
                                    foreach (var data in item.gpDesign._dataExport_ListBoxCommonCopy)
                                    {
                                        if (item1.Variable == data.Variable)
                                        {
                                            des.Add(data.Choiceid);
                                        }
                                    }
                                    // CatId = string.Join(",", des);
                                    //  array[11, j] = CatId;
                                    array[12, j] = isdatacheck;
                                    array[2, j] = 2 + "<>" + item.isactive;
                                    array[1, j] = item.Header;
                                    j++;
                                }
                            }
                            else
                            {
                                array[5, j] = variable;
                                array[6, j] = anstp;
                                array[7, j] = catcount;
                                array[12, j] = isdatacheck;
                                array[2, j] = 2 + "<>" + item.isactive;
                                array[1, j] = item.Header;



                                //array[11, j] = CatId;
                                j++;
                                int d = i + item.TabConten._lst_row3_list.Count;
                            }




                            if (item.TabConten._lst_row3_list.Count > 0)
                            {
                                //string CatId = "";

                                int count = 0;
                                foreach (var item2 in item.TabConten._lst_row3_list)
                                {
                                    count = i;
                                    array[i, 1] = item.Header;
                                    array[i, 3] = item2.Variable;
                                    array[i, 4] = item2.AnswerType.Split('/')[0];
                                    array[i, 5] = item2.CategoryCount;


                                    if (item.TabConten._lst_row2_list.Count > 0)
                                    {
                                        int k = j - 1;
                                        foreach (var row2 in item.TabConten._lst_row2_list)
                                        {
                                            array[i, k] = Onorofff;
                                            k--;
                                        }
                                    }
                                    else
                                    {
                                        int k = j - 1;
                                        array[i, k] = Onorofff;
                                        k--;
                                    }



                                    i++;


                                }
                            }
                            else
                            {
                                array[i, 1] = item.Header;
                                // res.Cells[i, 1] = 3;
                                if (item.TabConten._lst_row2_list.Count > 0)
                                {
                                    int k = j - 1;
                                    foreach (var row2 in item.TabConten._lst_row2_list)
                                    {
                                        array[i, k] = Onorofff;
                                        k--;
                                    }
                                }
                                else
                                {
                                    //j--;
                                    int k = j - 1;
                                    array[i, k] = Onorofff;
                                    k--;
                                }
                                i++;
                            }



                        }

                        if (item.TabConten.Option_Setting_Summary_Double.IsChecked == true)
                        {
                            string CatId = "";
                            if (item.TabConten.dataList.Count > 0)
                            {
                                foreach (var it in item.TabConten.dataList)
                                {
                                    variable = it.Variable;
                                    anstp = it.AnswerType.Split('/')[0];
                                    catcount = it.CategoryCount.ToString();
                                }
                            }

                            if (item.TabConten._lst_row2_list.Count > 0)
                            {
                                List<int> des = new List<int>();
                                foreach (var item1 in item.TabConten._lst_row2_list)
                                {
                                    des = new List<int>();
                                    array[5, j] = "";
                                    array[6, j] = "";
                                    array[7, j] = "";
                                    array[8, j] = item1.Variable;
                                    array[9, j] = item1.AnswerType.Split('/')[0]; ;
                                    array[10, j] = item1.CategoryCount;
                                    array[12, j] = isdatacheck;
                                    array[2, j] = 1 + "<>" + item.isactive;
                                    array[1, j] = item.Header;

                                    foreach (var data in item.gpDesign._dataExport_ListBoxCommonCopy)
                                    {
                                        if (item1.Variable == data.Variable)
                                        {
                                            des.Add(data.Choiceid);
                                        }
                                    }

                                    CatId = string.Join(",", des);
                                    array[11, j] = CatId;
                                    j++;
                                }
                            }
                            else
                            {
                                array[5, j] = "";
                                array[6, j] = "";
                                array[7, j] = "";
                                array[12, j] = isdatacheck;
                                array[2, j] = 1 + "<>" + item.isactive;
                                array[1, j] = item.Header;

                                j++;
                                int d = i + item.TabConten._lst_row3_list.Count;
                            }




                            if (item.TabConten._lst_row3_list.Count > 0)
                            {

                                int count = 0;
                                foreach (var item2 in item.TabConten._lst_row3_list)
                                {
                                    count = i;

                                    array[i, 1] = item.Header;
                                    array[i, 3] = item2.Variable;
                                    array[i, 4] = item2.AnswerType.Split('/')[0];
                                    array[i, 5] = item2.CategoryCount;


                                    if (item.TabConten._lst_row2_list.Count > 0)
                                    {
                                        int k = j - 1;
                                        foreach (var row2 in item.TabConten._lst_row2_list)
                                        {
                                            array[i, k] = Onorofff;
                                            k--;
                                        }
                                    }
                                    else
                                    {
                                        int k = j - 1;
                                        array[i, k] = Onorofff;
                                        k--;
                                    }



                                    i++;


                                }
                            }
                            else
                            {
                                // res.Cells[i, 1] = 2;
                                array[i, 1] = item.Header;
                                if (item.TabConten._lst_row2_list.Count > 0)
                                {
                                    int k = j - 1;
                                    foreach (var row2 in item.TabConten._lst_row2_list)
                                    {
                                        array[i, k] = Onorofff;
                                        k--;
                                    }
                                }
                                else
                                {
                                    //j--;
                                    int k = j - 1;
                                    array[i, k] = Onorofff;
                                    k--;
                                }
                                i++;
                            }

                        }




                        if (item.TabConten.Option_Setting_Summary_Single.IsChecked == true)
                        {
                            string CatId = "";

                            if (item.TabConten._lst_row3_list.Count > 0)
                            {

                                int count = 0;
                                foreach (var item2 in item.TabConten._lst_row3_list)
                                {
                                    count = i;
                                    // res.Cells[i, 1] = 1;
                                    array[i, 1] = item.Header;
                                    array[i, 3] = item2.Variable;
                                    array[i, 4] = item2.AnswerType.Split('/')[0];
                                    array[i, 5] = item2.CategoryCount;


                                    int k = j;
                                    array[1, j] = item.Header;
                                    array[i, k] = Onorofff;
                                    array[12, j] = isdatacheck;
                                    array[2, j] = 0 + "<>" + item.isactive;
                                    k--;
                                    i++;


                                }
                            }
                            else
                            {
                                array[i, 1] = item.Header;
                                // res.Cells[i, 1] =1;
                                if (item.TabConten._lst_row2_list.Count > 0)
                                {
                                    int k = j;
                                    array[12, j] = isdatacheck;
                                    array[2, j] = 0 + "<>" + item.isactive;
                                    array[1, j] = item.Header;
                                    array[i, k] = Onorofff;
                                    k--;
                                    // j++;

                                }
                                else
                                {
                                    //j--;
                                    int k = j;
                                    array[1, j] = item.Header;
                                    array[12, j] = isdatacheck;
                                    array[2, j] = 0 + "<>" + item.isactive;
                                    array[i, k] = Onorofff;
                                    k--;
                                }
                                i++;


                            }
                            j++;

                            //else
                            //{
                            //    if (item.TabConten._lst_row2_list.Count > 0)
                            //    {
                            //        List<int> des = new List<int>();
                            //        foreach (var item1 in item.TabConten._lst_row2_list)
                            //        {
                            //            int num = 0;

                            //            num++;


                            //            array[5, j] = variable;
                            //            array[6, j] = anstp;
                            //            array[7, j] = catcount;
                            //            array[8, j] = item1.Variable;

                            //            array[9, j] = item1.AnswerType.Split('/')[0]; ;
                            //            array[10, j] = item1.CategoryCount;

                            //            des = new List<int>();
                            //            foreach (var data in item.gpDesign._dataExport_ListBoxCommonCopy)
                            //            {
                            //                if (item1.Variable == data.Variable)
                            //                {
                            //                    des.Add(data.Choiceid);
                            //                }
                            //            }
                            //            // CatId = string.Join(",", des);
                            //            //  array[11, j] = CatId;
                            //            array[12, j] = isdatacheck;
                            //            array[2, j] = 0 + "<>" + item.isactive;
                            //            array[1, j] = item.Header;
                            //            j++;
                            //        }
                            //    }
                            //    else
                            //    {
                            //        array[5, j] = variable;
                            //        array[6, j] = anstp;
                            //        array[7, j] = catcount;
                            //        array[12, j] = isdatacheck;
                            //        array[2, j] = 0 + "<>" + item.isactive;
                            //        array[1, j] = item.Header;



                            //        //array[11, j] = CatId;
                            //        j++;
                            //        int d = i + item.TabConten._lst_row3_list.Count;
                            //    }

                            //}
                        }


                    }

                }

                rar.Value = array;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        public string tabname = "";
        public static ObservableCollection<Dem> getlistdata = new ObservableCollection<Dem>();
        ObservableCollection<Dem> datafromsheet = new ObservableCollection<Dem>();
        ObservableCollection<string> row2variable = new ObservableCollection<string>();
        public Dictionary<string, Dem> crossdictionary = new Dictionary<string, Dem>();

        public async void getData()
        {
            isfromcros = true;


            bool set = false;

            var CrossSheet = ExcelUtil.GetWorkSheetByCodeName(Workbook, Constants.SheetCodeName.CrossTabulationS);

            excel.Range ur = CrossSheet.UsedRange;
            var obj = ur.Value;

            int rowCount = ur.Rows.Count;
            int latRow = ur.Row;
            int colCount = ur.Columns.Count;
            int lastColumn = ur.Column;



            if (colCount >= 7)
            {

                List<string> lst = new List<string>();
                for (int j = 7; j <= colCount; j++)
                {

                    if (obj[1, j] != null)
                    {
                        List<choiceClass> stringChoice = new List<choiceClass>();
                        Dem dem = new Dem();

                        dem.row2list = new List<string>();
                        dem.listcommon = new List<choiceClass>();
                        dem.tabname = obj[1, j];
                        dem.onoffandRow = obj[2, j];
                        dem.Choices = obj[11, j];

                        if (obj[8, j] != null)
                        {
                            dem.variable = obj[8, j];
                            if (obj[11, j] != null)
                            {
                                choiceClass cd = new choiceClass();
                                cd.Variable = obj[8, j];
                                cd.Choices = obj[11, j];
                                dem.listcommon.Add(cd);
                            }

                        }
                        dem.singlevariable = obj[5, j];
                        dem.checbox = obj[12, j];


                        dem.subChoices = obj[11, j];


                        for (int i = 15; i <= rowCount; i++)
                        {
                            if (obj[i, j] != null)
                            {
                                if (obj[i, 1] == obj[1, j])
                                {
                                    dem.onoroff = obj[i, j];
                                    dem.row2list.Add(obj[i, 3]);
                                }
                            }
                        }
                        datafromsheet.Add(dem);
                    }


                }

            }



            List<Dem> newList = new List<Dem>();
            List<choiceClass> choicesss = new List<choiceClass>();

            Dem dem1 = new Dem();
            List<choiceClass> lst3 = new List<choiceClass>();

            List<string> lst1 = new List<string>();
            List<string> lst2 = new List<string>();
            List<string> lst4 = new List<string>();
            if (datafromsheet.Count > 0)
            {

                string header = datafromsheet[0].tabname;
                foreach (var item in datafromsheet)
                {

                    if (item.tabname == header)
                    {
                        dem1.tabname = item.tabname;
                        lst1.Add(item.variable);
                        lst4.Add(item.Choices);


                        lst3 = item.listcommon;
                        // dem1.listcommon = item.listcommon;
                        dem1.singlevariable = item.singlevariable;
                        dem1.onoroff = item.onoroff;
                        dem1.onoffandRow = item.onoffandRow;
                        dem1.row2list = item.row2list;
                        dem1.checbox = item.checbox;
                        dem1.subChoices = item.subChoices;

                    }
                    else
                    {
                        dem1.listcommon = lst3;
                        dem1.row3list = lst1;
                        dem1.listcommon1 = lst4;
                        newList.Add(dem1);
                        dem1 = new Dem();
                        lst1 = new List<string>();
                        lst2 = new List<string>();
                        lst3 = new List<choiceClass>();

                        lst4 = new List<string>();
                        lst3 = item.listcommon;

                        dem1.tabname = item.tabname;
                        dem1.onoffandRow = item.onoffandRow;
                        dem1.singlevariable = item.singlevariable;
                        lst1.Add(item.variable);
                        lst4.Add(item.Choices);
                        dem1.checbox = item.checbox;
                        dem1.onoroff = item.onoroff;
                        dem1.row2list = item.row2list;
                        dem1.subChoices = item.subChoices;
                        dem1.listcommon = lst3;
                        header = item.tabname;

                    }
                }
                dem1.listcommon = lst3;
                dem1.listcommon1 = lst4;
                dem1.row3list = lst1;
                newList.Add(dem1);
            }






            if (datafromsheet.Count > 0)
            {
                addItem(newList);
            }


        }


        EditTabulation tab;
        Dictionary<string, QuestionSettings> variableDictionary = new Dictionary<string, QuestionSettings>();
        private ObservableCollection<CrossQuestionSetting> _dataExport_LBVariablesToExport1 = new ObservableCollection<CrossQuestionSetting>();
        private ObservableCollection<CrossQuestionSetting> _dataExport_LBVariablesToExport2 = new ObservableCollection<CrossQuestionSetting>();
        private ObservableCollection<CrossQuestionSetting> _dataExport_LBVariablesToExport3 = new ObservableCollection<CrossQuestionSetting>();
        private ObservableCollection<CrossQuestionSetting> _dataExport_LBVariablesToExport4 = new ObservableCollection<CrossQuestionSetting>();


        public static bool isfromcros = false;
        public ObservableCollection<VMTabItems> tabItems = new ObservableCollection<VMTabItems>();

        public void addItem(List<Dem> list)
        {
            try
            {
                variableDictionary = Util.Definiotion.VariableDictionary;
                int count1 = -1;
                foreach (KeyValuePair<string, QuestionSettings> item in variableDictionary)
                {
                    count1++;
                    QuestionSettings qs = item.Value;
                    if (qs.Variable != "" && qs.Variable != "SAMPLEID")
                    {
                        if (qs.CategoryCount != 0)
                        {
                            CrossQuestionSetting qsts = new CrossQuestionSetting();
                            QuestionSettings qst = new QuestionSettings();
                            qsts.TableHeading = qs.TableHeading;
                            qsts.Variable = qs.Variable;
                            qsts.AnswerType = qs.AnswerType + "/" + qs.CategoryCount;
                            qsts.Question = qs.Question;
                            qsts.Choices = qs.Choices;
                            qsts.decid = count1;

                            qsts.CategoryCount = qs.CategoryCount;
                            _dataExport_LBVariablesToExport1.Add(qsts);
                        }
                        else
                        {
                            CrossQuestionSetting qsts = new CrossQuestionSetting();
                            QuestionSettings qst = new QuestionSettings();
                            qsts.TableHeading = qs.TableHeading;
                            qsts.Variable = qs.Variable;
                            qsts.AnswerType = qs.AnswerType;
                            qsts.Question = qs.Question;
                            qsts.Choices = qs.Choices;
                            qsts.CategoryCount = qs.CategoryCount;
                            qsts.decid = count1;
                            _dataExport_LBVariablesToExport1.Add(qsts);
                        }

                    }
                }
                VMTabItems _tabItem;
                TabContent tbcontent;
                GraphOptions gpDesign;
                string tbnameHeader = "";
                if (list.Count > 0)
                {

                    foreach (var item in list)
                    {
                        string header = "";


                        if (header != item.tabname)
                        {
                            string re = Regex.Replace(item.tabname, @"\D", "");

                            tabname = LocalResource.CROS_TABULATION_TAB + " " + re;
                            tbnameHeader = tabname;
                            _tabItem = new VMTabItems();
                            int count = TabBinding.tabItems.Count + 1;
                            _tabItem.Tabid = count;
                            tbcontent = new TabContent();
                            tbcontent.Name = "tabs" + count.ToString();
                            gpDesign = new GraphOptions();
                            gpDesign.Name = "tabs" + count.ToString();

                            List<choiceClass> choics1 = new List<choiceClass>();
                            List<string> graphrt = new List<string>();
                            if (item.listcommon1 != null)
                            {
                                for (int i = 0; i < item.row3list.Count; i++)
                                {
                                    for (int j = 0; j < item.listcommon1.Count; j++)
                                    {
                                        if (i == j)
                                        {
                                            if (!string.IsNullOrEmpty(item.listcommon1[i]) && !string.IsNullOrEmpty(item.row3list[j]))
                                            {
                                                choiceClass cd = new choiceClass();
                                                cd.Choices = item.listcommon1[j];
                                                cd.Variable = item.row3list[i];
                                                choics1.Add(cd);
                                            }
                                        }
                                    }
                                }
                            }



                            List<choiceClass> choics = new List<choiceClass>();
                            if (item.listcommon != null)
                            {
                                foreach (var choicid in choics1)
                                {

                                    if (choicid.Choices != "" && choicid.Choices != null)
                                    {
                                        String[] spearator = { "," };
                                        Int32 count3 = 2;
                                        string[] strlist = choicid.Choices.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                        foreach (var it in strlist)
                                        {
                                            choiceClass cd = new choiceClass();
                                            cd.Choices = it;
                                            cd.Variable = choicid.Variable;
                                            choics.Add(cd);
                                        }
                                    }
                                }

                            }


                            string CheckOn = "";
                            int a = 0;
                            if (item.onoffandRow != null)
                            {
                                String[] spearator = { "<>" };
                                Int32 count3 = 2;
                                String[] strlist = item.onoffandRow.Split(spearator, count3, StringSplitOptions.RemoveEmptyEntries);

                                CheckOn = strlist[1];
                                a = Convert.ToInt32(strlist[0]);
                            }
                            if (item.checbox == "●")
                            {
                                tbcontent.Check_Setting_Cross_Group.IsChecked = true;
                            }
                            else if (item.checbox == "")
                            {
                                tbcontent.Check_Setting_Cross_Group.IsChecked = false;
                            }

                            if (a == 0)
                            {
                                tbcontent.Option_Setting_Summary_Single.IsChecked = true;
                            }
                            if (a == 1)
                            {
                                tbcontent.Option_Setting_Summary_Double.IsChecked = true;
                            }
                            if (a == 2)
                            {
                                tbcontent.Option_Setting_Summary_Triple.IsChecked = true;
                            }




                            _tabItem.TabConten = tbcontent;

                            if (item.singlevariable != null)
                            {
                                if (variableDictionary.ContainsKey(item.singlevariable))
                                {
                                    _tabItem.TabConten.Text_Setting_Cross_Hyosoku1.Text = item.singlevariable;
                                }
                            }
                            foreach (var row1 in item.row2list)
                            {
                                foreach (var item1 in _dataExport_LBVariablesToExport1)
                                {
                                    if (row1 == item1.Variable)
                                    {
                                        _tabItem.TabConten._lst_row3_list.Add(item1);
                                    }
                                }

                            }

                            foreach (var row1 in item.row3list)
                            {
                                foreach (var item1 in _dataExport_LBVariablesToExport1)
                                {
                                    if (row1 == item1.Variable)
                                    {
                                        _tabItem.TabConten._lst_row2_list.Add(item1);

                                    }
                                }

                            }
                            _tabItem.gpDesign = gpDesign;
                            _tabItem.lst = new ObservableCollection<CrossQuestionSetting>();
                            _tabItem.Header = tbnameHeader;
                            bool isactive = true;
                            string onoroffff = LocalResource.CELL_ON;
                            if (CheckOn == "False")
                            {
                                isactive = false;
                                onoroffff = LocalResource.CELL_OFF;
                            }
                            _tabItem.isactive = isactive;
                            _tabItem.OnorOff = onoroffff;
                            ObservableCollection<CrossQuestionSetting> qst = new ObservableCollection<CrossQuestionSetting>();



                            TabBinding.tabItems.Add(_tabItem);
                            tbcontent.loaditemdata(_tabItem.TabConten._lst_row2_list);
                            tbcontent.update();

                            foreach (var it1 in choics)
                            {
                                foreach (var it2 in _tabItem.gpDesign._data)
                                {
                                    if (it1.Variable == it2.Variable)
                                    {
                                        int id = 0;
                                        Int32.TryParse(it1.Choices, out id);
                                        if (id == it2.Choiceid)
                                        {
                                            gpDesign._dataExport_ListBoxCommonCopy.Add(it2);
                                        }


                                    }

                                }
                            }
                            gpDesign.getdata();

                            ICollectionView collectionView1 = CollectionViewSource.GetDefaultView(tabItems);

                            if (collectionView1 != null)
                            {
                                collectionView1.MoveCurrentTo(_tabItem);
                            }

                        }
                    }

                }



            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }
        public void checkcheckbox()
        {
            try
            {
                if (Check_All_Base.IsChecked == true)
                {
                    txt_base_setting.Visibility = Visibility.Visible;
                }
                else if (Check_All_Base.IsChecked == false)
                {
                    txt_base_setting.Visibility = Visibility.Hidden;
                }
                if (Check_Summary_Non1.IsChecked == true)
                {
                    txt_answer_output.Visibility = Visibility.Hidden;
                }
                else if (Check_Summary_Non1.IsChecked == false)
                {
                    txt_answer_output.Visibility = Visibility.Visible;
                }
            }
            catch { }
        }
        private void Chk_total_rspnd_Checked(object sender, RoutedEventArgs e)
        {
            checkcheckbox();
        }

        /// <summary>
        /// changes the combine banner checkbox state while tab selection changes.
        /// </summary>
        private void Tabcross_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (((System.Windows.Controls.TabControl)sender).SelectedIndex == 2)
                {
                    //if multiple cross and the column % is selected, then enable the combine banner option otherwise disable it.
                    if (Option_Output_SheetType_Plural.IsChecked == true && Option_Setting_Output_Vertical.IsChecked == true)
                        Check_Summary_Combine_Banners.IsEnabled = true;
                    else
                        Check_Summary_Combine_Banners.IsEnabled = false;
                }

                if (Graph != null)
                {
                    Graph.SelectedIndex = Cross_tab_Settings.selectedindex;
                }
            }
            catch
            {
                // Exception handling: No specific action required.
            }
        }

        private void Graph_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Cross_tab_Settings.selectedindex = Graph.SelectedIndex;
        }

        private void Check_Output_Cross_N_Par_Checked(object sender, RoutedEventArgs e)
        {
            if (Check_Output_Cross_N.IsChecked == false && Check_Output_Cross_N_Par.IsChecked == false && Check_Output_Cross_Par.IsChecked == false)
            {
                BTN_CROSSTABULATE.IsEnabled = false;
                BTN_CLOSE.IsEnabled = false;
            }
            else
            {
                BTN_CROSSTABULATE.IsEnabled = true;
                BTN_CLOSE.IsEnabled = true;
            }
        }

        /// <summary>
        /// Handles the Checked event for the 'Option_Output_SheetType_Plural' checkbox. 
        /// If both the 'Option_Output_SheetType_Plural' and 'Option_Setting_Output_Vertical' checkboxes are checked,
        /// it enables the 'Check_Summary_Combine_Banners' checkbox. 
        /// Otherwise, it disables the 'Check_Summary_Combine_Banners' checkbox.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void Option_Output_SheetType_Plural_Checked(object sender, RoutedEventArgs e)
        {
            if (Option_Output_SheetType_Plural.IsChecked == true && Option_Setting_Output_Vertical.IsChecked == true)
                Check_Summary_Combine_Banners.IsEnabled = true;
            else
                Check_Summary_Combine_Banners.IsEnabled = false;
        }
    }
    public class Dem
    {
        public string onoffandRow { get; set; }
        public string tabname { get; set; }
        // public int radiobutton { get; set; }
        public string onoroff { get; set; }
        public string variable { get; set; }
        public string checbox { get; set; }
        public string subChoices { get; set; }
        //public List<string> subChoicesid { get; set; }
        public string singlevariable { get; set; }
        public List<string> row2list { get; set; }
        public List<string> row3list { get; set; }
        public List<choiceClass> listcommon { get; set; }
        public List<string> listcommon1 { get; set; }
        public string Variable { get; set; }
        public string Choices { get; set; }
    }
    public class choiceClass
    {
        public string Variable { get; set; }
        public string Choices { get; set; }
    }
}
