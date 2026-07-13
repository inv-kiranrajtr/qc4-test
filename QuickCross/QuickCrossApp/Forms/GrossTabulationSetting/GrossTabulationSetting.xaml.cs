using FilterSettingsView;
using Microsoft.WindowsAPICodePack.Dialogs;
using excel = Microsoft.Office.Interop.Excel;
using Qc4Launcher.Classes;
using Qc4Launcher.Util;
using System;
using Vb = Microsoft.VisualBasic;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static ExcelAddIn.Common.Constants;
using static FilterSettingsView.FilterSettingsClass;
using Constants = Qc4Launcher.Util.Constants;
using log4net;
using System.Reflection;
using Qc4Launcher.Forms.GrossTabulationSetting.Common;
using QC4Common.Validation;
using System.Text.RegularExpressions;
using System.Windows.Threading;
using System.Threading;
using System.Runtime.InteropServices;
using ExcelAddIn;

namespace Qc4Launcher.Forms.GrossTabulationSetting
{
    /// <summary>
    /// Interaction logic for GrossTabulationSetting.xaml
    /// </summary>
    public partial class GrossTabulationSetting : Window
    {
        excel.Workbook Workbook;
        bool ISWBAdded = false;
        int CmbClsy = 0;
        int CmbItm1 = 0;
        int CmbItm2 = 0;
        int CmbItm3 = 0;
        int CmbItm4 = 0;
        int CmbItm5 = 0;
        QC4Common.Util.FormUtil formUtil = new QC4Common.Util.FormUtil();

        string CmbOpItm1 = "";
        string CmbOpItm2 = "";
        string CmbOpItm3 = "";
        string CmbOpItm4 = "";
        string CmbOpItm5 = "";

        private string SelectedFile = "";

        private CheckBox ChkCriteria;
        private ComboBox CmbClassify;
        private ComboBox CmbCriteria;

        DispatcherTimer dispatcherTimer = new DispatcherTimer();

        bool IsInitialLoad = false;
        bool Combo_Conditional_Initial1 = false;
        bool Combo_Conditional_Initial2 = false;
        bool Combo_Conditional_Initial3 = false;
        bool Combo_Conditional_Initial4 = false;
        bool Combo_Conditional_Initial5 = false;
        bool CloseNotFromBtn = true;
        bool ExecuteNotFromBtn = false;

        bool CROSS_TXT_BX_ALL_CLRF = false;
        bool CROSS_TXT_BX_NONE_CLRF = false;

        private bool checkflag = false;

        private List<String> Combo_Conditional_Item_1Choices = new List<string>();
        private List<String> Combo_Conditional_Item_2Choices = new List<string>();
        private List<String> Combo_Conditional_Item_3Choices = new List<string>();
        private List<String> Combo_Conditional_Item_4Choices = new List<string>();
        private List<String> Combo_Conditional_Item_5Choices = new List<string>();
        public ObservableCollection<DataGT> _dataExport_LBVariablesForGT = new ObservableCollection<DataGT>();
        public ObservableCollection<DataGT> _dataExport_LBVariablesForGTST = new ObservableCollection<DataGT>();
        public ObservableCollection<DataGT> _dataExport_LBVariablesForGTST_Right = new ObservableCollection<DataGT>();
        public ObservableCollection<DataGT> _dataExport_LBVariablesForGTST_Left = new ObservableCollection<DataGT>();
        public ObservableCollection<DataGT> _dataExport_LBVariablesForGTSTMTS_Right = new ObservableCollection<DataGT>();
        public ObservableCollection<DataGT> _dataExport_LBVariablesForGTSTMTS_Left = new ObservableCollection<DataGT>();

        private string Combo_Conditional_Item_1selectedQuestionVariableType = string.Empty;
        private string Combo_Conditional_Item_2selectedQuestionVariableType = string.Empty;
        private string Combo_Conditional_Item_3selectedQuestionVariableType = string.Empty;
        private string Combo_Conditional_Item_4selectedQuestionVariableType = string.Empty;
        private string Combo_Conditional_Item_5selectedQuestionVariableType = string.Empty;

        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private ObservableCollection<DataExport> _dataExport_LBVariablesToExport = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _dataExport_ListBoxCommonCopy = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD1 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD2 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD3 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD4 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD5 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD6 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablnumeric = new ObservableCollection<DataExport>();

        private List<string> elementsInSheet = new List<string>();

        private Dictionary<string, Control> controlObj = new Dictionary<string, Control>();
        private Dictionary<string, String> ReadValueFromExcel = new Dictionary<string, String>();

        private int lastIndexinAdvancedSettingsExcelSheet = 0;

        FilterSettings fs = new FilterSettings();
        FilterSettingsClass fsc = new FilterSettingsClass();

        bool weightbak;
        public static bool weighted;
        public bool filtersettingcheckflag;
        MainWindow MainWindow;
        public static DataGT SelectedItem;
        public List<DataGT> GTTabulationItems = new List<DataGT>();
        public int AddedItemIndex = -1;
        System.Windows.Controls.DataGrid ExpGrid = null;

        public GrossTabulationSetting(excel.Workbook workbook, MainWindow mainWindow, string filePath)
        {
            MainWindow = mainWindow;
            InitializeComponent();
            ChangeButtonState();         
            Workbook = workbook;
            MainWindow.Hide();
            SelectedFile = filePath;
            FilterSettingsView.FilterControlDesignForGT fdesign = new FilterSettingsView.FilterControlDesignForGT();
            this.GridContainer.Children.Add(fdesign);
            fdesign.MyComboBoxSelectionChanged += new FilterControlDesignForGT.MyComboBoxSelectionChangedEventHandler(OnSelectionChanged);
            fdesign.MyButtonClick += new FilterControlDesignForGT.MyButtonClickEventEventHandler(OnButtonClick);
            fdesign.MyCheckBoxClick += new FilterControlDesignForGT.MyCheckBoxClickEventHandler(OnCheck);
            fdesign.MyRadioButtonClick += new FilterControlDesignForGT.MyRadioButtonClickEventHandler(OnRadioClick);

            CROSS_TXT_BX_ALL_CLRF = false;
            CROSS_TXT_BX_NONE_CLRF = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (QC4Common.Common.Constants.GlobalMode != "ja-JP," + Util.Constants.QCFont.MS_Gothic)
            {
                Command_GT_Summary_Output.FontSize = 10.0;
            }
            else
            {
                Command_GT_Summary_Output.FontSize = Command_GT_Summary_Initialize.FontSize;
            }
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 25);
            try
            {               
                LoadQSItems();
                LoadList();
                LoadStatisticalTestList();
                checkflag = false;
                IsInitialLoad = true;
                LoadingData();

                foreach (ComboBox tb
                  in FindControls.FindLogicalChildren<ComboBox>(this.Page_GT_Narrow))
                {
                    GetName_Page_GT_Narrow(tb);
                }
                foreach (RadioButton tb
                    in FindControls.FindLogicalChildren<RadioButton>(this.Page_GT_Narrow))
                {
                    GetName_Page_GT_Narrow(tb);
                }

                foreach (TextBox tb
                    in FindControls.FindLogicalChildren<TextBox>(this.Page_GT_Narrow))
                {
                    GetName_Page_GT_Narrow(tb);
                }
                foreach (CheckBox tb
                    in FindControls.FindLogicalChildren<CheckBox>(this.Page_GT_Narrow))
                {
                    GetName_Page_GT_Narrow(tb);
                }
               
                foreach (ComboBox tb
                  in FindControls.FindLogicalChildren<ComboBox>(this.Page_GT_Output))
                {
                    GetName_Page_GT_Narrow(tb);
                }
               
                foreach (TextBox tb
                    in FindControls.FindLogicalChildren<TextBox>(this.Page_GT_Output))
                {
                    GetName_Page_GT_Narrow(tb);
                }
                foreach (CheckBox tb
                    in FindControls.FindLogicalChildren<CheckBox>(this.Page_GT_Output))
                {
                    GetName_Page_GT_Narrow(tb);
                }
                foreach (CheckBox tb
                    in FindControls.FindLogicalChildren<CheckBox>(this.Page_GT_Test))
                {
                    GetName_Page_GT_Narrow(tb);
                }
                

                elementsInSheet.Clear();
                var SettingSheet = ExcelUtil.GetWorkSheetByCodeName(Workbook, Constants.SheetCodeName.DetailsSetting);
                excel.Range last = SettingSheet.Cells.SpecialCells(excel.XlCellType.xlCellTypeLastCell, Type.Missing);
                int rcell = SettingSheet.UsedRange.Rows.Count;
                rcell = rcell + controlObj.Count;
                rcell = rcell + 2;
                string ragecell = "C" + rcell.ToString();
                excel.Range rar = SettingSheet.get_Range("A1", ragecell);
                object[,] obj = rar.Value;
                int rowvalue = rar.Rows.Count + 1;

                for (int i = 2; i < rowvalue; i++)
                {
                    if (obj[i, 1] != null)
                        elementsInSheet.Add((obj[i, 1].ToString()));
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
                for (int i = 0; i < elementsInSheet.Count; i++)
                {
                    if (controlObj.ContainsKey(elementsInSheet[i]))
                    {
                        if (controlObj[elementsInSheet[i]] is ComboBox)
                        {
                            string s = elementsInSheet[i];
                            if (s.Contains(GT.FormControlPrefixAddup + GT.ComboBoxTypeName) || s.Contains("F_Cr_Cross_AddUp_" + GT.ComboBoxTypeName))
                            {
                                string sval = null == obj[i + 2, 2] ? "" : obj[i + 2, 2].ToString();
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
                                {//add logic here for filter settings combobox items

                                    ComboBox cmb = ((System.Windows.Controls.ComboBox)controlObj[elementsInSheet[i]]);

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
                            else if (Convert.ToString(obj[i + 2, 1]) == "F_Cr_Cross_AddUp_Text_Summary_Change_Non_S" && Convert.ToString(obj[i + 2, 2]) == QC4Common.Common.Constants.CRLFchar)
                            {
                                CROSS_TXT_BX_NONE_CLRF = true;
                                ((TextBox)controlObj[elementsInSheet[i]]).Text = LocalResource.CROSS_TXT_BX_NOANSWER;
                            }
                            else
                            {
                                ((TextBox)controlObj[elementsInSheet[i]]).Text = obj[i + 2, 2] == null ? "" : obj[i + 2, 2].ToString();
                                if (((TextBox)controlObj[elementsInSheet[i]]).Text != "" && ((TextBox)controlObj[elementsInSheet[i]]).Name == "Combo_Conditional_Value_1")
                                {
                                    foreach (RadioButton rb
                   in FindControls.FindLogicalChildren<RadioButton>(this.Page_GT_Narrow))
                                    {
                                        if (rb.Name == "Option_Conditional_And_1")
                                        {
                                            rb.Foreground = new SolidColorBrush(Colors.Black);
                                            rb.IsEnabled = true;
                                        }
                                        if (rb.Name == "Option_Conditional_Or_1")
                                        {
                                            rb.Foreground = new SolidColorBrush(Colors.Black);
                                            rb.IsEnabled = true;
                                        }
                                    }
                                }
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
                                    ((CheckBox)controlObj[elementsInSheet[i]]).IsChecked = false;
                                }
                            }
                        }

                    }
                }
                if (ISWBAdded && !WeightBac_Selected_Index)
                {
                    _qstnvariablnumeric.Insert(0, DataExportObjectCreator());
                }

                if (!textAdded)
                {
                    CROSS_TXT_BX_ALL_CLRF = true;
                    CROSS_TXT_BX_NONE_CLRF = true;
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

                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                readEXcel();

                foreach (CheckBox tb
                 in FindControls.FindLogicalChildren<CheckBox>(this.Page_GT_Narrow))
                {
                    if (tb.Name == "Check_Refine_Condition")
                    {
                        ChkCriteria = tb;
                        filtersettingcheckflag = (bool)tb.IsChecked;
                        break;
                    }
                }
                FilterControlDesignForGT.MyCheckBoxClickEventArgs cargs = new FilterControlDesignForGT.MyCheckBoxClickEventArgs();
                cargs.Check = filtersettingcheckflag;
                cargs.sendr = "Check_Refine_Condition";
                this.OnCheck(this, cargs);
                
                if(Check_Summary_WeightBack.IsChecked == true)
                    WeightBackControlState(true, Colors.Black);
                else
                    WeightBackControlState(false, Colors.DimGray);

                if(Check_Rate.IsChecked == true)
                    CheckRateControlState(true);
                else
                    CheckRateControlState(false);                
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

            List_GT_Summary.Focus();
        }

        public void LoadList()
        {
            try
            {
                _dataExport_LBVariablesForGT.Clear();

                for (int i = 0; i < GTTabulationItems.Count; i++)
                {
                    DataGT data = GTTabulationItems[i];
                    _dataExport_LBVariablesForGT.Add(new DataGT()
                    {
                        QuestionIndex = data.QuestionIndex,
                        Question = data.Question,
                        QuestionVariable = data.QuestionNumber == 0 ? data.QuestionVariable : data.QuestionVariable + ",...",                     
                        Graph = data.Graph,
                        ONOFF = data.ONOFF,
                        QSType = data.QSType,
                        Variable = data.Variable,
                        CategoryCount = data.CategoryCount,
                        QuestionNumber = data.QuestionNumber,
                        QSTypeShort=data.QSTypeShort,
                        Test = data.Test,
                        GTFlag = data.GTFlag==null?"": data.GTFlag
                    });
                    if (data.QuestionNumber > 0)
                    {
                        for (int j = 1; j < data.QuestionNumber; j++)
                            i++;
                    }
                }

                List_GT_Summary.ItemsSource = _dataExport_LBVariablesForGT;

                if (List_GT_Summary.Items.Count > 0)
                {
                    List_GT_Summary.ScrollIntoView(List_GT_Summary.Items[0]);
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
            }
        }

        private void LoadStatisticalTestList()
        {
            try
            {
                _dataExport_LBVariablesForGTST.Clear();
                _dataExport_LBVariablesForGTST_Right.Clear();
                _dataExport_LBVariablesForGTSTMTS_Right.Clear();
                var lBVariablesForGT = _dataExport_LBVariablesForGT.Where(p => p.QSType != "GT-N").ToList();
                for (int i = 0; i < lBVariablesForGT.Count; i++)
                {
                    DataGT data = lBVariablesForGT[i];
                    _dataExport_LBVariablesForGTST.Add(new DataGT()
                    {
                        QuestionIndex = data.QuestionIndex,
                        Question = data.Question,
                        QuestionVariable = data.QuestionVariable,
                        ONOFF = data.ONOFF,
                        QSType = data.QSType,
                        Variable = data.Variable,
                        Test = data.Test
                    });
                }

                int c = _dataExport_LBVariablesForGTST.Count;
                List<DataGT> removedItem = new List<DataGT>();
                for (int i = 0; i < c; i++)
                {
                    if (_dataExport_LBVariablesForGTST[i].Test == "1")
                    {
                        _dataExport_LBVariablesForGTST_Right.Add(_dataExport_LBVariablesForGTST[i]);
                        removedItem.Add(_dataExport_LBVariablesForGTST[i]);
                    }
                    else if (_dataExport_LBVariablesForGTST[i].Test == "2")
                    {
                        _dataExport_LBVariablesForGTSTMTS_Right.Add(_dataExport_LBVariablesForGTST[i]);
                        removedItem.Add(_dataExport_LBVariablesForGTST[i]);
                    }
                }
                for (int i = 0; i < removedItem.Count; i++)
                {
                    _dataExport_LBVariablesForGTST.Remove(removedItem[i]);
                }
                List_Test_Select.ItemsSource = _dataExport_LBVariablesForGTST_Right;
                List_Test_Item.ItemsSource = _dataExport_LBVariablesForGTSTMTS_Right;
                List_Test.ItemsSource = _dataExport_LBVariablesForGTST;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
            }
        }

        private List<DataGT> LoadQSItems()
        {
            GTTabulationItems.Clear();
            excel.Worksheet gtSheet = Util.ExcelUtil.GetWorkSheetByCodeName(Workbook, Util.Constants.SheetCodeName.GTTabulationS);
            excel.Range starCell = gtSheet.Cells[ExcelAddIn.Common.Constants.GT.GtRowDataStart, 1];
            excel.Range gtEnd = Util.ExcelUtil.EndxlUp(starCell);
            excel.Range valuesRange = gtSheet.get_Range(starCell, gtEnd.Offset[0, 205]);
            object[,] obj = valuesRange.Value;
            int qIndex = 0;

            excel.Worksheet qsSheet = Util.ExcelUtil.GetWorkSheetByCodeName(Workbook, Util.Constants.SheetCodeName.QuestionSetting);
            excel.Range qsStart = qsSheet.Cells[ExcelAddIn.Common.Constants.QS.QsRowDataStart, ExcelAddIn.Common.Constants.QS.QsColStart];
            excel.Range qsEnd = Util.ExcelUtil.EndxlUp(qsStart);
            excel.Range qsTotal = qsSheet.get_Range(qsStart.Offset[0, -1], qsEnd.Offset[0, 7]);
            Object[,] objAry = qsTotal.Value2;
            Dictionary<string, DataGT> qstions = new Dictionary<string, DataGT>();
            for (int i = 1; i <= objAry.GetLength(0); i++)
            {
                qstions.Add(objAry[i, 2].ToString(), new DataGT
                {
                    Question = objAry[i, 8] == null ? "" : objAry[i, 8].ToString(),
                    CategoryCount = objAry[i, 4] == null || objAry[i, 4].ToString() == "" ? 0 : Convert.ToInt32(objAry[i, 4].ToString()),
                    QSTypeShort = objAry[i, 3].ToString(),
                    QSHeading = objAry[i, 7] == null ? "" : formUtil.EscapeCRLF(objAry[i, 7].ToString())
                });
            }
            for (int i = 1; i <= obj.GetLength(0); i++)
            {
                if (obj[i, 7] == null)
                    break;
                string graph = obj[i, 5] == null ? "" : obj[i, 5].ToString();
                string onOff = obj[i, 2].ToString();
                string qsType = obj[i, 3].ToString();
                DataGT qsData = qstions.ContainsKey(obj[i, 7].ToString())? qstions[obj[i, 7].ToString()]:null;
                if (qsData == null)
                    continue;
                int qsNumIndx = 0;
                int qsNum = -1;
                string test = obj[i, 4] == null ? "" : obj[i, 4].ToString();
                string flag = obj[i, 1] == null ? "" : obj[i, 1].ToString();
                for (int j = 7; j <= 206; j++)
                {
                    if (obj[i, j] == null)
                        continue;
                    qsNum++;
                }
                for (int j = 7; j <= 206; j++)
                {
                    if (obj[i, j] == null)
                        continue;
                    string head = obj[i, 6] == null ? "" : obj[i, 6].ToString();
                    GTTabulationItems.Add(new DataGT()
                    {
                        QuestionIndex = qIndex,
                        Question = formUtil.EscapeCRLF(qsData.Question),
                        QuestionVariable = qsData.Question == "" ? obj[i, j].ToString() : obj[i, j].ToString() + "(" + formUtil.EscapeCRLF(qsData.Question) + ")",
                        Graph = CommonFunc.GetGraphByLanguage(graph),
                        ONOFF = GetONOFFWordbyLang(onOff),
                        QSType = qsType,
                        Variable = obj[i, j].ToString(),
                        CategoryCount = qsData.CategoryCount,
                        QuestionNumber = qsNum > 0 ? qsNum + 1 : qsNum,
                        QSTypeShort = qsData.QSTypeShort,
                        QsTypePlusCatCount = qsData.CategoryCount > 0 ? qsData.QSTypeShort + "/" + qsData.CategoryCount : qsData.QSTypeShort,
                        QsNumberIndex = qsNumIndx,
                        QSHeading = formUtil.EscapeCRLF(head),
                        GTFlag = flag,
                        Test = test
                    });
                    qIndex++;
                    qsNumIndx++;
                    qsNum = 0;
                }
            }
            return GTTabulationItems;
        }

        private string GetONOFFWordbyLang(string onOff)
        {
            if(onOff== QC4Common.Common.Constants.MarkWhiteCircle || onOff == QC4Common.Common.Constants.MarkON)
            {
                return LocalResource.CELL_ON_EN;
            }
            else
            {
                return LocalResource.CELL_OFF_EN;
            }
        }

        private void GetValuesOfControls()
        {
            ReadValueFromExcel.Clear();

            foreach (ComboBox tb
               in FindControls.FindLogicalChildren<ComboBox>(this.Page_GT_Narrow))
            {
                GetValue_Page_Refine(tb);
            }
            foreach (RadioButton tb
                in FindControls.FindLogicalChildren<RadioButton>(this.Page_GT_Narrow))
            {
                GetValue_Page_Refine(tb);
            }

            foreach (TextBox tb
                in FindControls.FindLogicalChildren<TextBox>(this.Page_GT_Narrow))
            {
                GetValue_Page_Refine(tb);
            }
            foreach (CheckBox tb
                in FindControls.FindLogicalChildren<CheckBox>(this.Page_GT_Narrow))
            {
                GetValue_Page_Refine(tb);
            }
            //
            foreach (ComboBox tb
              in FindControls.FindLogicalChildren<ComboBox>(this.Page_GT_Output))
            {
                GetValue_Page_Output(tb);
            }           
            foreach (TextBox tb
                in FindControls.FindLogicalChildren<TextBox>(this.Page_GT_Output))
            {
                GetValue_Page_Refine(tb);
            }
            foreach (CheckBox tb
                in FindControls.FindLogicalChildren<CheckBox>(this.Page_GT_Output))
            {
                GetValue_Page_Output(tb);
            }
            foreach (CheckBox tb
               in FindControls.FindLogicalChildren<CheckBox>(this.Page_GT_Test))
            {
                GetValue_Page_Refine(tb);
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
                            ReadValueFromExcel.Add("F_Cr_Cross_AddUp_" + ((ComboBox)x).Name + GT.FormControlPostFixStd, ((DataExport)itm).QuestionVariable);
                        else
                            ReadValueFromExcel.Add("F_Cr_Cross_AddUp_" + ((ComboBox)x).Name + GT.FormControlPostFixStd, "");
                    }
                    catch { ReadValueFromExcel.Add("F_Cr_Cross_AddUp_" + ((ComboBox)x).Name + GT.FormControlPostFixStd, ""); }
                }
                else
                    ReadValueFromExcel.Add(GT.FormControlPrefixAddup + ((ComboBox)x).Name + GT.FormControlPostFixStd, ((ComboBox)x).SelectedIndex.ToString());
            }
            else if (x is CheckBox)
            {
                if (((CheckBox)x).Name == "Check_Summary_WeightBack")
                    ReadValueFromExcel.Add("F_Cr_Cross_AddUp_" + ((CheckBox)x).Name + GT.FormControlPostFixStd, ((CheckBox)x).IsChecked.ToString());
                else
                    ReadValueFromExcel.Add(GT.FormControlPrefixAddup + ((CheckBox)x).Name + GT.FormControlPostFixStd, ((CheckBox)x).IsChecked.ToString());
            }
        }
        public void GetValue_Page_Refine(Control x)
        {
            if (x is ComboBox)
            {
                var myObject = ((ComboBox)x).SelectedItem as DataExport;

                if (myObject != null)
                    ReadValueFromExcel.Add(GT.FormControlPrefixAddup + ((ComboBox)x).Name + GT.FormControlPostFixStd, ((DataExport)((ComboBox)x).SelectedItem).QuestionVariable);
                else
                    ReadValueFromExcel.Add(GT.FormControlPrefixAddup + ((ComboBox)x).Name + GT.FormControlPostFixStd, ((ComboBox)x).Text);
            }
            else if (x is TextBox)
            {
                if (((TextBox)x).Name == "Text_Summary_Change_Hyoutou" || ((TextBox)x).Name == "Text_Summary_Change_Non")
                    ReadValueFromExcel.Add(Name = "F_Cr_Cross_AddUp_" + ((TextBox)x).Name + GT.FormControlPostFixStd, ((TextBox)x).Text);
                else
                    ReadValueFromExcel.Add(GT.FormControlPrefixAddup + ((TextBox)x).Name + GT.FormControlPostFixStd, ((TextBox)x).Text);
            }
            else if (x is RadioButton)
                ReadValueFromExcel.Add(GT.FormControlPrefixAddup + ((RadioButton)x).Name + GT.FormControlPostFixStd, ((RadioButton)x).IsChecked.ToString());

            else if (x is CheckBox)
                ReadValueFromExcel.Add(GT.FormControlPrefixAddup + ((CheckBox)x).Name + GT.FormControlPostFixStd, ((CheckBox)x).IsChecked.ToString());
        }

        public void GetName_Page_GT_Narrow(Control x)
        {
            if (x is ComboBox)
            {
                if (((ComboBox)x).Name == "Combo_Summary_WeightBack")
                    controlObj.Add(Name = "F_Cr_Cross_AddUp_" + ((ComboBox)x).Name + GT.FormControlPostFixStd, ((ComboBox)x));
                else
                    controlObj.Add(Name = GT.FormControlPrefixAddup + ((ComboBox)x).Name + GT.FormControlPostFixStd, ((ComboBox)x));
            }
            else if (x is CheckBox)
            {
                if (((CheckBox)x).Name == "Check_Summary_WeightBack")
                    controlObj.Add(Name = "F_Cr_Cross_AddUp_" + ((CheckBox)x).Name + GT.FormControlPostFixStd, ((CheckBox)x));
                else
                    controlObj.Add(Name = GT.FormControlPrefixAddup + ((CheckBox)x).Name + GT.FormControlPostFixStd, ((CheckBox)x));
            }
            else if (x is TextBox)
            {
                if (((TextBox)x).Name == "Text_Summary_Change_Hyoutou" || ((TextBox)x).Name == "Text_Summary_Change_Non")
                    controlObj.Add(Name = "F_Cr_Cross_AddUp_" + ((TextBox)x).Name + GT.FormControlPostFixStd, ((TextBox)x));
                else
                    controlObj.Add(Name = GT.FormControlPrefixAddup + ((TextBox)x).Name + GT.FormControlPostFixStd, ((TextBox)x));
            }
            else if (x is RadioButton)
                controlObj.Add(Name = GT.FormControlPrefixAddup + ((RadioButton)x).Name + GT.FormControlPostFixStd, ((RadioButton)x));
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
        FilterControlDesign fdobj = new FilterControlDesign();
        private void OnSelectionChanged(object sender, FilterControlDesignForGT.MyComboBoxSelectionChangedEventArgs e)
        {
            if (e.sendr == "Combo_Classify_Item")
            {
                if (((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).QuestionVariableType == null || ((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).QuestionVariableType == "")
                {
                    Text_GT_Filter_Message.Text = "";
                    foreach (System.Windows.Controls.TextBox tb
              in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this.Page_GT_Narrow))
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
          in FindControls.FindLogicalChildren<System.Windows.Controls.Button>(this.Page_GT_Narrow))
                    {
                        if (tb.Name == "exportpathbtn") tb.IsEnabled = false;
                    }
                    foreach (System.Windows.Controls.TextBlock tb
          in FindControls.FindLogicalChildren<System.Windows.Controls.TextBlock>(this.Page_GT_Narrow))
                    {
                        if (tb.Name == "Label_Export_Path") { tb.IsEnabled = false;
                            tb.Foreground = new SolidColorBrush(Colors.DimGray);
                        }
                    }
                    return;
                }
                Text_GT_Filter_Message.Text = LocalResource.GT_TABULATION_FILTER;
                foreach (System.Windows.Controls.TextBox tb
               in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this.Page_GT_Narrow))
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
              in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_GT_Narrow))
                    {
                        if (tb.Name == "Combo_Conditional_Operator_1")
                        {
                            tb.Items.Clear();
                            tb.IsEnabled = false;
                        }
                    }
                    foreach (System.Windows.Controls.TextBox tb
            in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this.Page_GT_Narrow))
                    {
                        if (tb.Name == "Combo_Conditional_Value_1") { tb.Text = ""; tb.IsEnabled = false; }
                    }
                    foreach (System.Windows.Controls.Button tb
           in FindControls.FindLogicalChildren<System.Windows.Controls.Button>(this.Page_GT_Narrow))
                    {
                        if (tb.Name == "BTnFilter1") tb.IsEnabled = false;
                    }
                    foreach (System.Windows.Controls.RadioButton tb
             in FindControls.FindLogicalChildren<System.Windows.Controls.RadioButton>(this.Page_GT_Narrow))
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
                   in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_GT_Narrow))
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
                       in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this.Page_GT_Narrow))
                        {
                            if (tb.Name == "Combo_Conditional_Value_1")
                            {
                                tb.Text = "";
                                tb.IsEnabled = false;
                                break;
                            }
                        }
                        foreach (System.Windows.Controls.Button tb
                       in FindControls.FindLogicalChildren<System.Windows.Controls.Button>(this.Page_GT_Narrow))
                        {
                            if (tb.Name == "BTnFilter1")
                            {
                                tb.IsEnabled = false;
                                break;
                            }
                        }
                        foreach (System.Windows.Controls.RadioButton tb
                       in FindControls.FindLogicalChildren<System.Windows.Controls.RadioButton>(this.Page_GT_Narrow))
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
           in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_GT_Narrow))
                    {
                        if (tb.Name == "Combo_Conditional_Item_2")
                        {
                            tb.IsEnabled = false; break;
                        }
                    }

                    foreach (System.Windows.Controls.ComboBox tb
              in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_GT_Narrow))
                    {
                        if (tb.Name == "Combo_Conditional_Operator_2")
                        {
                            tb.Items.Clear(); tb.IsEnabled = false;
                            break;
                        }
                    }
                    foreach (System.Windows.Controls.TextBox tb
            in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this.Page_GT_Narrow))
                    {
                        if (tb.Name == "Combo_Conditional_Value_2") { tb.Text = ""; tb.IsEnabled = false; break; }
                    }
                    foreach (System.Windows.Controls.Button tb
           in FindControls.FindLogicalChildren<System.Windows.Controls.Button>(this.Page_GT_Narrow))
                    {
                        if (tb.Name == "BTnFilter2") { tb.IsEnabled = false; break; }
                    }
                    foreach (System.Windows.Controls.RadioButton tb
             in FindControls.FindLogicalChildren<System.Windows.Controls.RadioButton>(this.Page_GT_Narrow))
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
                    Combo_Conditional_Item_2Choices = ((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).Choisces;
                    Combo_Conditional_Item_2selectedQuestionVariableType = ((FilterSettingsView.FilterSettingsClass.DataExport)e.MyComboBoxItem).QuestionVariableType.Split('/')[0];
                    ComboBox Combo_Conditional_Operator = null;
                    foreach (System.Windows.Controls.ComboBox tb
                   in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_GT_Narrow))
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
                 in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this.Page_GT_Narrow))
                        {
                            if (tb.Name == "Combo_Conditional_Value_2")
                            {
                                tb.Text = "";
                                tb.IsEnabled = false;
                                break;
                            }
                        }
                        foreach (System.Windows.Controls.Button tb
                       in FindControls.FindLogicalChildren<System.Windows.Controls.Button>(this.Page_GT_Narrow))
                        {
                            if (tb.Name == "BTnFilter2")
                            {
                                tb.IsEnabled = false;
                                break;
                            }
                        }
                        foreach (System.Windows.Controls.RadioButton tb
                       in FindControls.FindLogicalChildren<System.Windows.Controls.RadioButton>(this.Page_GT_Narrow))
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
           in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_GT_Narrow))
                    {
                        if (tb.Name == "Combo_Conditional_Item_3")
                        {
                            tb.IsEnabled = (bool)false;
                        }
                    }

                    foreach (System.Windows.Controls.ComboBox tb
              in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_GT_Narrow))
                    {
                        if (tb.Name == "Combo_Conditional_Operator_3")
                        {
                            tb.Items.Clear(); tb.IsEnabled = false;
                        }
                    }
                    foreach (System.Windows.Controls.TextBox tb
            in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this.Page_GT_Narrow))
                    {
                        if (tb.Name == "Combo_Conditional_Value_3") { tb.Text = ""; tb.IsEnabled = false; }
                    }
                    foreach (System.Windows.Controls.Button tb
           in FindControls.FindLogicalChildren<System.Windows.Controls.Button>(this.Page_GT_Narrow))
                    {
                        if (tb.Name == "BTnFilter3") tb.IsEnabled = false;
                    }
                    foreach (System.Windows.Controls.RadioButton tb
             in FindControls.FindLogicalChildren<System.Windows.Controls.RadioButton>(this.Page_GT_Narrow))
                    {
                        if (tb.Name == "Option_Conditional_And_2") { tb.IsChecked = false; }
                        else if (tb.Name == "Option_Conditional_Or_2") { tb.IsChecked = false; }
                        else if (tb.Name == "Option_Conditional_And_3") { tb.IsChecked = false; tb.IsEnabled = false; }
                        else if (tb.Name == "Option_Conditional_Or_3") { tb.IsChecked = false; tb.IsEnabled = false; }

                    }
                    foreach (System.Windows.Controls.TextBox tb
                 in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this.Page_GT_Narrow))
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
                   in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_GT_Narrow))
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
                 in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this.Page_GT_Narrow))
                        {
                            if (tb.Name == "Combo_Conditional_Value_3")
                            {
                                tb.Text = "";
                                tb.IsEnabled = false;
                                break;
                            }
                        }
                        foreach (System.Windows.Controls.Button tb
                       in FindControls.FindLogicalChildren<System.Windows.Controls.Button>(this.Page_GT_Narrow))
                        {
                            if (tb.Name == "BTnFilter3")
                            {
                                tb.IsEnabled = false;
                                break;
                            }
                        }
                        foreach (System.Windows.Controls.RadioButton tb
                       in FindControls.FindLogicalChildren<System.Windows.Controls.RadioButton>(this.Page_GT_Narrow))
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
           in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_GT_Narrow))
                    {
                        if (tb.Name == "Combo_Conditional_Item_4")
                        {
                            tb.IsEnabled = false;
                        }
                    }
                    foreach (System.Windows.Controls.ComboBox tb
              in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_GT_Narrow))
                    {
                        if (tb.Name == "Combo_Conditional_Operator_4")
                        {
                            tb.IsEnabled = false;
                            tb.Items.Clear();
                        }
                    }
                    foreach (System.Windows.Controls.TextBox tb
            in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this.Page_GT_Narrow))
                    {
                        if (tb.Name == "Combo_Conditional_Value_4") { tb.Text = ""; tb.IsEnabled = false; break; }
                    }
                    foreach (System.Windows.Controls.Button tb
           in FindControls.FindLogicalChildren<System.Windows.Controls.Button>(this.Page_GT_Narrow))
                    {
                        if (tb.Name == "BTnFilter4") { tb.IsEnabled = false; break; }
                    }
                    foreach (System.Windows.Controls.RadioButton tb
             in FindControls.FindLogicalChildren<System.Windows.Controls.RadioButton>(this.Page_GT_Narrow))
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
                   in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_GT_Narrow))
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
                 in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this.Page_GT_Narrow))
                        {
                            if (tb.Name == "Combo_Conditional_Value_4")
                            {
                                tb.Text = "";
                                tb.IsEnabled = false;
                                break;
                            }
                        }
                        foreach (System.Windows.Controls.Button tb
                       in FindControls.FindLogicalChildren<System.Windows.Controls.Button>(this.Page_GT_Narrow))
                        {
                            if (tb.Name == "BTnFilter4")
                            {
                                tb.IsEnabled = false;
                                break;
                            }
                        }
                        foreach (System.Windows.Controls.RadioButton tb
                       in FindControls.FindLogicalChildren<System.Windows.Controls.RadioButton>(this.Page_GT_Narrow))
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
           in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_GT_Narrow))
                    {
                        if (tb.Name == "Combo_Conditional_Item_5")
                        {
                            tb.IsEnabled = false;
                        }
                    }
                    foreach (System.Windows.Controls.ComboBox tb
              in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_GT_Narrow))
                    {
                        if (tb.Name == "Combo_Conditional_Operator_5")
                        {
                            tb.IsEnabled = false;
                            tb.Items.Clear();
                        }
                    }
                    foreach (System.Windows.Controls.TextBox tb
            in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this.Page_GT_Narrow))
                    {
                        if (tb.Name == "Combo_Conditional_Value_5") { tb.Text = ""; tb.IsEnabled = false; break; }
                    }
                    foreach (System.Windows.Controls.Button tb
           in FindControls.FindLogicalChildren<System.Windows.Controls.Button>(this.Page_GT_Narrow))
                    {
                        if (tb.Name == "BTnFilter5") { tb.IsEnabled = false; break; }
                    }
                    foreach (System.Windows.Controls.RadioButton tb
            in FindControls.FindLogicalChildren<System.Windows.Controls.RadioButton>(this.Page_GT_Narrow))
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
                   in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_GT_Narrow))
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
                 in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this.Page_GT_Narrow))
                        {
                            if (tb.Name == "Combo_Conditional_Value_5")
                            {
                                tb.Text = "";
                                tb.IsEnabled = false;
                                break;
                            }
                        }
                        foreach (System.Windows.Controls.Button tb
                       in FindControls.FindLogicalChildren<System.Windows.Controls.Button>(this.Page_GT_Narrow))
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
                       in FindControls.FindLogicalChildren<System.Windows.Controls.CheckBox>(this.Page_GT_Narrow))
                    {
                        if (tb.Name == "Check_Refine_Condition")
                        {
                            isChecked = tb.IsChecked == true ? true : false;
                            break;
                        }
                    }
                    if (!isChecked)
                        OnCheck(null, new FilterControlDesignForGT.MyCheckBoxClickEventArgs() { sendr = "Check_Refine_Condition", Check = isChecked });
                }
            }
            else if (e.sendr == "Combo_Conditional_Operator_1")
            {
                ComboBox Combo_Conditional_Value = null;
                foreach (System.Windows.Controls.ComboBox tb
              in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_GT_Narrow))
                {
                    if (tb.Name == "Combo_Conditional_Operator_1") { Combo_Conditional_Value = tb; break; }
                }
                if (Combo_Conditional_Value.SelectedIndex >= 0 && Combo_Conditional_Initial1)
                {
                    foreach (System.Windows.Controls.TextBox tb
                  in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this.Page_GT_Narrow))
                    {
                        if (tb.Name == "Combo_Conditional_Value_1") { tb.IsEnabled = true; break; }
                    }
                    foreach (System.Windows.Controls.Button tb
               in FindControls.FindLogicalChildren<System.Windows.Controls.Button>(this.Page_GT_Narrow))
                    {
                        if (tb.Name == "BTnFilter1") { tb.IsEnabled = true; break; }
                    }
                }
            }
            else if (e.sendr == "Combo_Conditional_Operator_2")
            {
                ComboBox Combo_Conditional_Value = null;
                foreach (System.Windows.Controls.ComboBox tb
              in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_GT_Narrow))
                {
                    if (tb.Name == "Combo_Conditional_Operator_2") { Combo_Conditional_Value = tb; break; }
                }
                if (Combo_Conditional_Value.SelectedIndex >= 0 && Combo_Conditional_Initial2)
                {
                    foreach (System.Windows.Controls.TextBox tb
              in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this.Page_GT_Narrow))
                    {
                        if (tb.Name == "Combo_Conditional_Value_2") { tb.IsEnabled = true; break; }
                    }
                    foreach (System.Windows.Controls.Button tb
               in FindControls.FindLogicalChildren<System.Windows.Controls.Button>(this.Page_GT_Narrow))
                    {
                        if (tb.Name == "BTnFilter2") { tb.IsEnabled = true; break; }
                    }
                }
            }
            else if (e.sendr == "Combo_Conditional_Operator_3")
            {
                ComboBox Combo_Conditional_Value = null;
                foreach (System.Windows.Controls.ComboBox tb
              in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_GT_Narrow))
                {
                    if (tb.Name == "Combo_Conditional_Operator_3") { Combo_Conditional_Value = tb; break; }
                }
                if (Combo_Conditional_Value.SelectedIndex >= 0 && Combo_Conditional_Initial3)
                {
                    foreach (System.Windows.Controls.TextBox tb
              in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this.Page_GT_Narrow))
                    {
                        if (tb.Name == "Combo_Conditional_Value_3") { tb.IsEnabled = true; break; }
                    }
                    foreach (System.Windows.Controls.Button tb
               in FindControls.FindLogicalChildren<System.Windows.Controls.Button>(this.Page_GT_Narrow))
                    {
                        if (tb.Name == "BTnFilter3") { tb.IsEnabled = true; break; }
                    }
                }
            }
            else if (e.sendr == "Combo_Conditional_Operator_4")
            {
                ComboBox Combo_Conditional_Value = null;
                foreach (System.Windows.Controls.ComboBox tb
              in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_GT_Narrow))
                {
                    if (tb.Name == "Combo_Conditional_Operator_4") { Combo_Conditional_Value = tb; break; }
                }
                if (Combo_Conditional_Value.SelectedIndex >= 0 && Combo_Conditional_Initial4)
                {
                    foreach (System.Windows.Controls.TextBox tb
              in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this.Page_GT_Narrow))
                    {
                        if (tb.Name == "Combo_Conditional_Value_4") { tb.IsEnabled = true; break; }
                    }
                    foreach (System.Windows.Controls.Button tb
               in FindControls.FindLogicalChildren<System.Windows.Controls.Button>(this.Page_GT_Narrow))
                    {
                        if (tb.Name == "BTnFilter4") { tb.IsEnabled = true; break; }
                    }
                }
            }
            else if (e.sendr == "Combo_Conditional_Operator_5")
            {
                ComboBox Combo_Conditional_Value = null;
                foreach (System.Windows.Controls.ComboBox tb
              in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_GT_Narrow))
                {
                    if (tb.Name == "Combo_Conditional_Operator_5") { Combo_Conditional_Value = tb; break; }
                }
                if (Combo_Conditional_Value.SelectedIndex >= 0 && Combo_Conditional_Initial5)
                {
                    foreach (System.Windows.Controls.TextBox tb
              in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this.Page_GT_Narrow))
                    {
                        if (tb.Name == "Combo_Conditional_Value_5") { tb.IsEnabled = true; break; }
                    }
                    foreach (System.Windows.Controls.Button tb
               in FindControls.FindLogicalChildren<System.Windows.Controls.Button>(this.Page_GT_Narrow))
                    {
                        if (tb.Name == "BTnFilter5") { tb.IsEnabled = true; break; }
                    }
                }
            }
        }

        private void EnableDisableExportPathButton()
        {
            bool enableexportpathbtn = false;

            foreach (ComboBox cb
      in FindControls.FindLogicalChildren<ComboBox>(this.Page_GT_Narrow))
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
          in FindControls.FindLogicalChildren<Button>(this.Page_GT_Narrow))
            {
                if (tb.Name == "exportpathbtn") { tb.IsEnabled = enableexportpathbtn; break; }
            }
            foreach (TextBlock tb
          in FindControls.FindLogicalChildren<TextBlock>(this.Page_GT_Narrow))
            {
                if (tb.Name == "Label_Export_Path") { tb.IsEnabled = enableexportpathbtn;
                    if(tb.IsEnabled)
                    tb.Foreground = new SolidColorBrush(Colors.Black);
                    else
                        tb.Foreground = new SolidColorBrush(Colors.DimGray);
                    break; }
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

        private void OnCheck(object sender, FilterControlDesignForGT.MyCheckBoxClickEventArgs e)
        {
            if (e.sendr == "Check_Refine_Condition" && !IsInitialLoad)
            {
                if (e.Check == true)
                {
                    foreach (System.Windows.Controls.TextBlock tb
               in FindControls.FindLogicalChildren<System.Windows.Controls.TextBlock>(this.Page_GT_Narrow))
                    {
                        if(tb.Name== "lblCriteriaVariable")
                            tb.Foreground= new SolidColorBrush(Colors.Black);
                        if (tb.Name == "lblOperator")
                            tb.Foreground = new SolidColorBrush(Colors.Black);
                        if (tb.Name == "lblValue")
                            tb.Foreground = new SolidColorBrush(Colors.Black);
                    }
                    Text_GT_Refine_Message.Text = LocalResource.GT_TABULATION_REFINE;
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
            in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_GT_Narrow))
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
                in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this.Page_GT_Narrow))
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
            in FindControls.FindLogicalChildren<System.Windows.Controls.Button>(this.Page_GT_Narrow))
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
                    Text_GT_Refine_Message.Text = "";
                    foreach (System.Windows.Controls.Label tb
              in FindControls.FindLogicalChildren<System.Windows.Controls.Label>(this.Page_GT_Narrow))
                    {
                        if (tb.Name == "lblCriteriaVariable") tb.IsEnabled = false;
                        else if (tb.Name == "lblOperator") tb.IsEnabled = false;
                        else if (tb.Name == "lblValue") tb.IsEnabled = false;
                    }
                    foreach (System.Windows.Controls.TextBlock tb
               in FindControls.FindLogicalChildren<System.Windows.Controls.TextBlock>(this.Page_GT_Narrow))
                    {
                        if (tb.Name == "lblCriteriaVariable")
                            tb.Foreground = new SolidColorBrush(Colors.DimGray);
                        if (tb.Name == "lblOperator")
                            tb.Foreground = new SolidColorBrush(Colors.DimGray);
                        if (tb.Name == "lblValue")
                            tb.Foreground = new SolidColorBrush(Colors.DimGray);
                    }

                    foreach (System.Windows.Controls.ComboBox tb
              in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this.Page_GT_Narrow))
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
              in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this.Page_GT_Narrow))
                    {
                        if (tb.Name == "Combo_Conditional_Value_1") tb.IsEnabled = false;
                        else if (tb.Name == "Combo_Conditional_Value_2") tb.IsEnabled = false;
                        else if (tb.Name == "Combo_Conditional_Value_3") tb.IsEnabled = false;
                        else if (tb.Name == "Combo_Conditional_Value_4") tb.IsEnabled = false;
                        else if (tb.Name == "Combo_Conditional_Value_5") tb.IsEnabled = false;
                    }
                    foreach (System.Windows.Controls.RadioButton tb
              in FindControls.FindLogicalChildren<System.Windows.Controls.RadioButton>(this.Page_GT_Narrow))
                    {
                        if (tb.Name == "Option_Conditional_And_1") { tb.IsEnabled = false;tb.Foreground = new SolidColorBrush(Colors.DimGray); }
                        else if (tb.Name == "Option_Conditional_Or_1") { tb.IsEnabled = false; tb.Foreground = new SolidColorBrush(Colors.DimGray); }
                        else if (tb.Name == "Option_Conditional_And_2") { tb.IsEnabled = false; tb.Foreground = new SolidColorBrush(Colors.DimGray); }
                        else if (tb.Name == "Option_Conditional_Or_2") { tb.IsEnabled = false; tb.Foreground = new SolidColorBrush(Colors.DimGray); }
                        else if (tb.Name == "Option_Conditional_And_3") { tb.IsEnabled = false; tb.Foreground = new SolidColorBrush(Colors.DimGray); }
                        else if (tb.Name == "Option_Conditional_Or_3") { tb.IsEnabled = false; tb.Foreground = new SolidColorBrush(Colors.DimGray); }
                        else if (tb.Name == "Option_Conditional_And_4") { tb.IsEnabled = false; tb.Foreground = new SolidColorBrush(Colors.DimGray); }
                        else if (tb.Name == "Option_Conditional_Or_4") { tb.IsEnabled = false; tb.Foreground = new SolidColorBrush(Colors.DimGray); }
                    }
                    foreach (System.Windows.Controls.Button tb
            in FindControls.FindLogicalChildren<System.Windows.Controls.Button>(this.Page_GT_Narrow))
                    {
                        if (tb.Name == "BTnFilter1") tb.IsEnabled = false;
                        else if (tb.Name == "BTnFilter2") tb.IsEnabled = false;
                        else if (tb.Name == "BTnFilter3") tb.IsEnabled = false;
                        else if (tb.Name == "BTnFilter4") tb.IsEnabled = false;
                        else if (tb.Name == "BTnFilter5") tb.IsEnabled = false;
                    }
                }
            }
            else if(e.sendr == "Check_Refine_Condition" && IsInitialLoad)
            {
                if (e.Check == true)
                {
                    Text_GT_Refine_Message.Text = LocalResource.GT_TABULATION_REFINE;
                }
                else
                    Text_GT_Refine_Message.Text = "";
                foreach (System.Windows.Controls.RadioButton tb
             in FindControls.FindLogicalChildren<System.Windows.Controls.RadioButton>(this.Page_GT_Narrow))
                {
                    if (tb.Name == "Option_Conditional_And_1") { if (tb.IsEnabled == false) tb.Foreground = new SolidColorBrush(Colors.DimGray); else tb.Foreground = new SolidColorBrush(Colors.Black); }
                    else if (tb.Name == "Option_Conditional_Or_1") { if (tb.IsEnabled == false) tb.Foreground = new SolidColorBrush(Colors.DimGray); else tb.Foreground = new SolidColorBrush(Colors.Black); }
                    else if (tb.Name == "Option_Conditional_And_2") { if (tb.IsEnabled == false) tb.Foreground = new SolidColorBrush(Colors.DimGray); else tb.Foreground = new SolidColorBrush(Colors.Black); }
                    else if (tb.Name == "Option_Conditional_Or_2") { if (tb.IsEnabled == false) tb.Foreground = new SolidColorBrush(Colors.DimGray); else tb.Foreground = new SolidColorBrush(Colors.Black); }
                    else if (tb.Name == "Option_Conditional_And_3") { if (tb.IsEnabled == false) tb.Foreground = new SolidColorBrush(Colors.DimGray); else tb.Foreground = new SolidColorBrush(Colors.Black); }
                    else if (tb.Name == "Option_Conditional_Or_3") { if (tb.IsEnabled == false) tb.Foreground = new SolidColorBrush(Colors.DimGray); else tb.Foreground = new SolidColorBrush(Colors.Black); }
                    else if (tb.Name == "Option_Conditional_And_4") { if (tb.IsEnabled == false) tb.Foreground = new SolidColorBrush(Colors.DimGray); else tb.Foreground = new SolidColorBrush(Colors.Black); }
                    else if (tb.Name == "Option_Conditional_Or_4") { if (tb.IsEnabled == false) tb.Foreground = new SolidColorBrush(Colors.DimGray); else tb.Foreground = new SolidColorBrush(Colors.Black); }
                }
            }
        }

        private void EnableDisable_RadioButtons()
        {
            foreach (TextBox tb
           in FindControls.FindLogicalChildren<TextBox>(this.Page_GT_Narrow))
            {
                if (tb.Name == "Combo_Conditional_Value_1" && (tb.Text != "" && tb.Text != null))
                {
                    foreach (RadioButton rb
        in FindControls.FindLogicalChildren<RadioButton>(this.Page_GT_Narrow))
                    {
                        if (rb.Name == "Option_Conditional_And_1") { rb.IsEnabled = true; rb.Foreground = new SolidColorBrush(Colors.Black); }
                        else if (rb.Name == "Option_Conditional_Or_1") { rb.IsEnabled = true; rb.Foreground = new SolidColorBrush(Colors.Black); }

                    }
                }
                else if (tb.Name == "Combo_Conditional_Value_2" && (tb.Text != "" && tb.Text != null))
                {
                    foreach (RadioButton rb
          in FindControls.FindLogicalChildren<RadioButton>(this.Page_GT_Narrow))
                    {
                        if (rb.Name == "Option_Conditional_And_2") { rb.IsEnabled = true; rb.Foreground = new SolidColorBrush(Colors.Black); }
                        else if (rb.Name == "Option_Conditional_Or_2") { rb.IsEnabled = true; rb.Foreground = new SolidColorBrush(Colors.Black); }

                    }
                }
                else if (tb.Name == "Combo_Conditional_Value_3" && tb.Text != "" && tb.Text != null)
                {
                    foreach (RadioButton rb
         in FindControls.FindLogicalChildren<RadioButton>(this.Page_GT_Narrow))
                    {
                        if (rb.Name == "Option_Conditional_And_3") { rb.IsEnabled = true; rb.Foreground = new SolidColorBrush(Colors.Black); }
                        else if (rb.Name == "Option_Conditional_Or_3") { rb.IsEnabled = true; rb.Foreground = new SolidColorBrush(Colors.Black); }

                    }
                }
                else if (tb.Name == "Combo_Conditional_Value_4" && tb.Text != "" && tb.Text != null)
                {
                    foreach (RadioButton rb
          in FindControls.FindLogicalChildren<RadioButton>(this.Page_GT_Narrow))
                    {
                        if (rb.Name == "Option_Conditional_And_4") { rb.IsEnabled = true; rb.Foreground = new SolidColorBrush(Colors.Black); }
                        else if (rb.Name == "Option_Conditional_Or_4") { rb.IsEnabled = true; rb.Foreground = new SolidColorBrush(Colors.Black); }

                    }
                }
            }
        }

        private void EnableDisable_Load_RadioButtons()
        {
            foreach (TextBox tb
           in FindControls.FindLogicalChildren<TextBox>(this.Page_GT_Narrow))
            {
                if (tb.Name == "Combo_Conditional_Value_1" && (tb.Text != "" && tb.Text != null))
                {
                    foreach (RadioButton rb
        in FindControls.FindLogicalChildren<RadioButton>(this.Page_GT_Narrow))
                    {
                        if (rb.Name == "Option_Conditional_And_1") rb.IsEnabled = true;
                        else if (rb.Name == "Option_Conditional_Or_1") rb.IsEnabled = true;

                    }
                }
                else if (tb.Name == "Combo_Conditional_Value_2" && (tb.Text != "" && tb.Text != null))
                {
                    foreach (RadioButton rb
          in FindControls.FindLogicalChildren<RadioButton>(this.Page_GT_Narrow))
                    {
                        if (rb.Name == "Option_Conditional_And_2") rb.IsEnabled = true;
                        else if (rb.Name == "Option_Conditional_Or_2") rb.IsEnabled = true;

                    }
                }
                else if (tb.Name == "Combo_Conditional_Value_3" && tb.Text != "" && tb.Text != null)
                {
                    foreach (RadioButton rb
         in FindControls.FindLogicalChildren<RadioButton>(this.Page_GT_Narrow))
                    {
                        if (rb.Name == "Option_Conditional_And_3") { rb.IsEnabled = true; rb.IsChecked = true; }
                        else if (rb.Name == "Option_Conditional_Or_3") rb.IsEnabled = true;

                    }
                }
                else if (tb.Name == "Combo_Conditional_Value_4" && tb.Text != "" && tb.Text != null)
                {
                    foreach (RadioButton rb
          in FindControls.FindLogicalChildren<RadioButton>(this.Page_GT_Narrow))
                    {
                        if (rb.Name == "Option_Conditional_And_4") rb.IsEnabled = true;
                        else if (rb.Name == "Option_Conditional_Or_4") rb.IsEnabled = true;

                    }
                }
            }
        }

          private void OnButtonClick(object sender, FilterControlDesignForGT.MyButtonClickEventArgs e) 
        {
            if (e.sendr == "exportpathbtn")
            {
                var dialog = new CommonOpenFileDialog();
                dialog.IsFolderPicker = true;

                if (dialog.ShowDialog(this) == CommonFileDialogResult.Ok)
                {
                    foreach (TextBox tb
                in FindControls.FindLogicalChildren<TextBox>(this.Page_GT_Narrow))
                    {
                        if (tb.Name == "Combo_Classify_FolderPath")
                        {
                            tb.Text = dialog.FileName;
                        }
                    }
                }
            }
            else if (e.sendr == "BTnFilter1")
            {
              
              
                foreach (TextBox tb
               in FindControls.FindLogicalChildren<TextBox>(this.Page_GT_Narrow))
                {
                    if (tb.Name == "Combo_Conditional_Value_1")
                    {
                        string value = ChoiceSelection(Combo_Conditional_Item_1selectedQuestionVariableType, Combo_Conditional_Item_1Choices);
                        if (value != null && value != "")
                        {
                            tb.Text = value;
                            foreach (RadioButton rb
               in FindControls.FindLogicalChildren<RadioButton>(this.Page_GT_Narrow))
                            {
                                if (rb.Name == "Option_Conditional_And_1")
                                    rb.Foreground = new SolidColorBrush(Colors.Black);
                                if(rb.Name == "Option_Conditional_Or_1")
                                    rb.Foreground = new SolidColorBrush(Colors.Black);
                            }
                        }
                        break;
                    }
                }
            }
            else if (e.sendr == "BTnFilter2")
            {
             
                foreach (TextBox tb
               in FindControls.FindLogicalChildren<TextBox>(this.Page_GT_Narrow))
                {
                    if (tb.Name == "Combo_Conditional_Value_2")
                    {
                        string value = ChoiceSelection(Combo_Conditional_Item_2selectedQuestionVariableType, Combo_Conditional_Item_2Choices);
                        if (value != null && value != "")
                        {
                            tb.Text = value;
                            foreach (RadioButton rb
               in FindControls.FindLogicalChildren<RadioButton>(this.Page_GT_Narrow))
                            {
                                if (rb.Name == "Option_Conditional_And_2")
                                    rb.Foreground = new SolidColorBrush(Colors.Black);
                                if (rb.Name == "Option_Conditional_Or_2")
                                    rb.Foreground = new SolidColorBrush(Colors.Black);
                            }
                        }
                        break;
                    }
                }
            }
            else if (e.sendr == "BTnFilter3")
            {
             
                foreach (TextBox tb
               in FindControls.FindLogicalChildren<TextBox>(this.Page_GT_Narrow))
                {
                    if (tb.Name == "Combo_Conditional_Value_3")
                    {
                        string value = ChoiceSelection(Combo_Conditional_Item_3selectedQuestionVariableType, Combo_Conditional_Item_3Choices);
                        if (value != null && value != "")
                        {
                            tb.Text = value;
                            foreach (RadioButton rb
               in FindControls.FindLogicalChildren<RadioButton>(this.Page_GT_Narrow))
                            {
                                if (rb.Name == "Option_Conditional_And_3")
                                    rb.Foreground = new SolidColorBrush(Colors.Black);
                                if (rb.Name == "Option_Conditional_Or_3")
                                    rb.Foreground = new SolidColorBrush(Colors.Black);
                            }
                        }
                        break;
                    }
                }
            }
            else if (e.sendr == "BTnFilter4")
            {
              
                foreach (TextBox tb
               in FindControls.FindLogicalChildren<TextBox>(this.Page_GT_Narrow))
                {
                    if (tb.Name == "Combo_Conditional_Value_4")
                    {
                        string value = ChoiceSelection(Combo_Conditional_Item_4selectedQuestionVariableType, Combo_Conditional_Item_4Choices);
                        if (value != null && value != "")
                        {
                            tb.Text = value;
                            foreach (RadioButton rb
               in FindControls.FindLogicalChildren<RadioButton>(this.Page_GT_Narrow))
                            {
                                if (rb.Name == "Option_Conditional_And_4")
                                    rb.Foreground = new SolidColorBrush(Colors.Black);
                                if (rb.Name == "Option_Conditional_Or_4")
                                    rb.Foreground = new SolidColorBrush(Colors.Black);
                            }
                        }
                        break;
                    }
                }
            }
            else if (e.sendr == "BTnFilter5")
            {
             
                foreach (TextBox tb
               in FindControls.FindLogicalChildren<TextBox>(this.Page_GT_Narrow))
                {
                    if (tb.Name == "Combo_Conditional_Value_5")
                    {
                        string value = ChoiceSelection(Combo_Conditional_Item_5selectedQuestionVariableType, Combo_Conditional_Item_5Choices);
                        if (value != null && value != "")
                        {
                            tb.Text = value;
                            foreach (RadioButton rb
               in FindControls.FindLogicalChildren<RadioButton>(this.Page_GT_Narrow))
                            {
                                if (rb.Name == "Option_Conditional_And_5")
                                    rb.Foreground = new SolidColorBrush(Colors.Black);
                                if (rb.Name == "Option_Conditional_Or_5")
                                    rb.Foreground = new SolidColorBrush(Colors.Black);
                            }
                        }
                        break;
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
            if (selectedChoice == LocalResource.TEXT_FILTER_VALUE_DK_NO_ANSWER || selectedChoice == LocalResource.TEXT_FILTER_VALUE_STAR_EXCLUDED)
                selectedChoice = selectedChoice.Split('.')[0];
            return selectedChoice;
        }

        private void OnRadioClick(object sender, FilterControlDesignForGT.MyRadioButtonClickEventArgs e) 
        {
            if ((e.sendr == "Option_Conditional_And_1" || e.sendr == "Option_Conditional_Or_1") && e.Check == true)
            {
                foreach (ComboBox tb
           in FindControls.FindLogicalChildren<ComboBox>(this.Page_GT_Narrow))
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
           in FindControls.FindLogicalChildren<ComboBox>(this.Page_GT_Narrow))
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
           in FindControls.FindLogicalChildren<ComboBox>(this.Page_GT_Narrow))
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
           in FindControls.FindLogicalChildren<ComboBox>(this.Page_GT_Narrow))
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

        private void ChangeButtonState()
        {
            Command_GT_Summary_Edit.IsEnabled = false;
            Command_GT_Summary_Copy.IsEnabled = false;
        }

        private void Command_GT_Summary_New_Click(object sender, RoutedEventArgs e)
        {
            GTSelect gTSelect = new GTSelect(this, Workbook);
            gTSelect.Owner = this;
            gTSelect.ShowDialog();
            if (AddedItemIndex >= 0)
            {
                LoadList();
                List_GT_Summary.Focus();
                List_GT_Summary.SelectedIndex = _dataExport_LBVariablesForGT.IndexOf(_dataExport_LBVariablesForGT.Where(x => x.QuestionIndex == AddedItemIndex).First());
                List_GT_Summary.ScrollIntoView(List_GT_Summary.Items[List_GT_Summary.SelectedIndex]);
                AddedItemIndex = -1;
            }
            LoadStatisticalTestList();
        }

        private void Command_GT_Summary_Edit_Click(object sender, RoutedEventArgs e)
        {
            Edit();
        }
        
        private void Command_GT_Summary_Copy_Click(object sender, RoutedEventArgs e)
        {
            if (List_GT_Summary.SelectedItem != null)
            {
                DataGT dataGt = List_GT_Summary.SelectedItem as DataGT;
                AddGTSettings addGTSettings = new AddGTSettings(CommonFunc.GetFullTypeNameByAnsType(dataGt.QSType), this, Workbook, "COPY", dataGt);
                addGTSettings.ShowDialog();
                if (AddedItemIndex >= 0)
                {
                    LoadList();
                    List_GT_Summary.Focus();
                    List_GT_Summary.SelectedIndex = _dataExport_LBVariablesForGT.IndexOf(_dataExport_LBVariablesForGT.Where(x => x.QuestionIndex == AddedItemIndex).First());
                    List_GT_Summary.ScrollIntoView(List_GT_Summary.Items[List_GT_Summary.SelectedIndex]);
                    AddedItemIndex = -1;
                }
            }
            LoadStatisticalTestList();
        }

        private void Command_GT_Summary_Output_Click(object sender, RoutedEventArgs e)
        {
            GTOutputOrder gTOutputOrder = new GTOutputOrder(_dataExport_LBVariablesForGT,this);
            this.Hide();
            gTOutputOrder.ShowDialog();
            this.Show();
            LoadList();
            LoadStatisticalTestList();
        }

        private void Command_Summary_WeightBack_Click(object sender, RoutedEventArgs e)
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
        
        private void List_GT_Summary_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (List_GT_Summary.SelectedItems.Count == 1)
            {
                Command_GT_Summary_Copy.IsEnabled = true;
                Command_GT_Summary_Edit.IsEnabled = true;
            }
            else
            {
                Command_GT_Summary_Copy.IsEnabled = false;
                Command_GT_Summary_Edit.IsEnabled = false;
            }
            if(List_GT_Summary.SelectedItems.Count>0)
            {
                Command_GT_Summary_Delete.IsEnabled = true;
                Command_GT_Summary_Change.IsEnabled = true;
            }
            else
            {
                Command_GT_Summary_Delete.IsEnabled = false;
                Command_GT_Summary_Change.IsEnabled = false;
            }

        }

        private void Command_GT_Summary_Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int currentIndex = 0;
                if (List_GT_Summary.SelectedItems.Count > 0)
                {
                    DataGT data = List_GT_Summary.SelectedItems[List_GT_Summary.SelectedItems.Count - 1] as DataGT;
                    currentIndex = _dataExport_LBVariablesForGT.IndexOf(data);
                    if ((_dataExport_LBVariablesForGT.Count - List_GT_Summary.SelectedItems.Count) > ((currentIndex + 1) - List_GT_Summary.SelectedItems.Count))
                        currentIndex = ((currentIndex + 1) - List_GT_Summary.SelectedItems.Count);
                    else
                        currentIndex = -1;
                    System.Windows.Forms.DialogResult result = MessageDialog.InfoYesNo(LocalResource.GT_TABULATION_DELETE);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        List<DataGT> items = List_GT_Summary.SelectedItems.Cast<DataGT>().ToList();                       
                        List<DataGT> removedIndex = new List<DataGT>();                       
                        for(int t = 0; t < items.Count; t++)
                        {
                            DataGT itm = GTTabulationItems.Where(x => x.QuestionIndex == items[t].QuestionIndex).First();
                            int qsNum = itm.QuestionNumber;
                            int startIndx = itm.QuestionIndex;
                            if (itm.QuestionNumber>0)
                            {
                                for (int j = 0; j < qsNum; j++)
                                {
                                    removedIndex.Add(GTTabulationItems[startIndx]);
                                    startIndx++;
                                }
                            }
                            else
                                removedIndex.Add(itm);
                        }
                        for (int i = 0; i < removedIndex.Count; i++)
                        {
                            GTTabulationItems.Remove(removedIndex[i]);
                        }
                        for (int i = 0; i < GTTabulationItems.Count; i++)
                            GTTabulationItems[i].QuestionIndex = i;
                        LoadList();
                        if (currentIndex >=0)
                            List_GT_Summary.SelectedIndex = currentIndex;
                        else if (List_GT_Summary.Items.Count > 0)
                            List_GT_Summary.SelectedItem = List_GT_Summary.Items[List_GT_Summary.Items.Count - 1];
                        if(List_GT_Summary.SelectedItems.Count>0)
                        {
                            List_GT_Summary.Focus();
                            List_GT_Summary.ScrollIntoView(List_GT_Summary.SelectedItem as DataGT);
                        }
                    }

                    LoadStatisticalTestList();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void Command_GT_Summary_Change_Click(object sender, RoutedEventArgs e)
        {
            List<DataGT> items = List_GT_Summary.SelectedItems.Cast<DataGT>().ToList();           
            List<DataGT> itm = new List<DataGT>();
            List<int> indx = new List<int>();
            for (int t = 0; t < items.Count; t++)
            {
                DataGT item = _dataExport_LBVariablesForGT.Where(x => x.QuestionIndex == items[t].QuestionIndex).First();
                indx.Add(_dataExport_LBVariablesForGT.IndexOf(item));
                string cellONOFF = "";
                if (item.ONOFF == LocalResource.CELL_ON_EN)
                {
                    cellONOFF = LocalResource.CELL_OFF_EN;
                }
                else
                {
                    cellONOFF = LocalResource.CELL_ON_EN;
                }
                GTTabulationItems.Where(x => x.QuestionIndex == item.QuestionIndex).First().ONOFF = cellONOFF;
                _dataExport_LBVariablesForGT.Where(x => x == item).First().ONOFF = cellONOFF;

            }
            for (int i = 0; i < _dataExport_LBVariablesForGT.Count; i++)
                itm.Add(_dataExport_LBVariablesForGT[i]);
            _dataExport_LBVariablesForGT.Clear();
            for (int i = 0; i < itm.Count; i++)
                _dataExport_LBVariablesForGT.Add(itm[i]);
            List_GT_Summary.ItemsSource = _dataExport_LBVariablesForGT;
            for (int t = 0; t < indx.Count; t++)
            {
                List_GT_Summary.SelectedItems.Add(_dataExport_LBVariablesForGT[indx[t]]);
            }          
            LoadStatisticalTestList();        
        }

        private void Command_GT_Summary_Automatic_Click(object sender, RoutedEventArgs e)
        {
            if (Command_GT_Summary_Automatic.Content.ToString() == LocalResource.GT_TABULATION_ALLX)
            {
                Command_GT_Summary_Automatic.Content = LocalResource.GT_TABULATION_ALLO;
                List<DataGT> itm = new List<DataGT>();
                for (int i = 0; i < _dataExport_LBVariablesForGT.Count; i++)
                {
                    _dataExport_LBVariablesForGT[i].ONOFF = LocalResource.CELL_OFF_EN;
                }
                for (int i = 0; i < GTTabulationItems.Count; i++)
                {
                    GTTabulationItems[i].ONOFF = LocalResource.CELL_OFF_EN;
                }
                for (int i = 0; i < _dataExport_LBVariablesForGT.Count; i++)
                    itm.Add(_dataExport_LBVariablesForGT[i]);
                _dataExport_LBVariablesForGT.Clear();
                for (int i = 0; i < itm.Count; i++)
                    _dataExport_LBVariablesForGT.Add(itm[i]);
                List_GT_Summary.ItemsSource = _dataExport_LBVariablesForGT;
            }
            else
            {
                Command_GT_Summary_Automatic.Content = LocalResource.GT_TABULATION_ALLX;
                List<DataGT> itm = new List<DataGT>();
                for (int i = 0; i < _dataExport_LBVariablesForGT.Count; i++)
                {
                    _dataExport_LBVariablesForGT[i].ONOFF = LocalResource.CELL_ON_EN;
                }
                for (int i = 0; i < GTTabulationItems.Count; i++)
                {
                    GTTabulationItems[i].ONOFF = LocalResource.CELL_ON_EN;
                }
                for (int i = 0; i < _dataExport_LBVariablesForGT.Count; i++)
                    itm.Add(_dataExport_LBVariablesForGT[i]);
                _dataExport_LBVariablesForGT.Clear();
                for (int i = 0; i < itm.Count; i++)
                    _dataExport_LBVariablesForGT.Add(itm[i]);
                List_GT_Summary.ItemsSource = _dataExport_LBVariablesForGT;
            }

            LoadStatisticalTestList();
        }

        private void Command_GT_Summary_Initialize_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.DialogResult res = System.Windows.Forms.DialogResult.Yes;
                if (List_GT_Summary.Items.Count > 0)
                    res = MessageDialog.InfoYesNo(LocalResource.GT_AUTO_SETTNG_CONFIRM,System.Windows.Forms.MessageBoxIcon.Question);
                if (res == System.Windows.Forms.DialogResult.Yes)
                {
                    Command_GT_Summary_Automatic.Content = LocalResource.GT_TABULATION_ALLX;
                    SaveGT();
                    QC4Common.Common.GTAutoSetting.LoadDefaultDataToGTHiddenSheet(Workbook);
                    LoadQSItems();
                    LoadList();
                    LoadStatisticalTestList();
                }
            }
            catch(Exception ex)
            {

            }
        }        

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow.Show();
            if (!CloseNotFromBtn)
            {
                try
                {
                    string version = "S";
                    excel.Worksheet GrossSheet = ExcelUtil.GetWorkSheetByCodeName(Workbook, Constants.SheetCodeName.GTTabulationS);
                    MainWindow.GrossTabulate(Workbook, GrossSheet, version, true, true);
                }
                catch (Exception ex)
                {
                    _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                }
            }           
        }

        private void Command_Execute_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            SaveGT(); ExecuteNotFromBtn = true;
            if (!ExcelAddIn.Common.Change.ValidateGTTab(null, true, Workbook.Worksheets, isFromSTD: true, ptr: new System.Windows.Interop.WindowInteropHelper(this).Handle))
                return;
            if (CheckValidSettings())
            {
                MessageDialog.ErrorOk(LocalResource.GT_EMPTY);
                return;
            }
            CloseNotFromBtn = false;
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            this.Close();
        }

        private bool CheckValidSettings()
        {
            return _dataExport_LBVariablesForGT.All(x => x.ONOFF == LocalResource.CELL_OFF_EN);
        }

        private void Command_Cancel_Click(object sender, RoutedEventArgs e)
        {           
            this.Close();
        }

        private void Edit()
        {
            if (List_GT_Summary.SelectedItem != null)
            {
                DataGT dataGt = List_GT_Summary.SelectedItem as DataGT;
                AddGTSettings addGTSettings = new AddGTSettings(CommonFunc.GetFullTypeNameByAnsType(dataGt.QSType), this, Workbook, "EDIT", dataGt);
                addGTSettings.ShowDialog();
                List_GT_Summary.Focus();
                List_GT_Summary.SelectedItem = SelectedItem;
                List_GT_Summary.ScrollIntoView(List_GT_Summary.Items[List_GT_Summary.SelectedIndex]);
            }
            LoadStatisticalTestList();
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!CloseNotFromBtn)
            {
                if (CrossTabulation.checkUnprocessedNewQuestionDialog(Workbook, new System.Windows.Interop.WindowInteropHelper(this).Handle))
                {
                    CloseNotFromBtn = true;
                    e.Cancel = true;
                    return;
                }
            }

            if (!CheckRefine())
            {
                CloseNotFromBtn = true;
                e.Cancel = true;
                return;
            }
            GetValuesOfControls();
            SaveSettings();
            SaveST();
            if (!ExecuteNotFromBtn)
                SaveGT();
        }

        private void SaveST()
        {
            for (int j = 0; j < _dataExport_LBVariablesForGTST_Right.Count; j++)
            {
                GTTabulationItems.Where(x => x.QuestionIndex == _dataExport_LBVariablesForGTST_Right[j].QuestionIndex).First().Test = _dataExport_LBVariablesForGTST_Right[j].Test;
            }
            for (int j = 0; j < _dataExport_LBVariablesForGTSTMTS_Right.Count; j++)
            {
                GTTabulationItems.Where(x => x.QuestionIndex == _dataExport_LBVariablesForGTSTMTS_Right[j].QuestionIndex).First().Test = _dataExport_LBVariablesForGTSTMTS_Right[j].Test;
            }
            for (int j = 0; j < _dataExport_LBVariablesForGTST.Count; j++)
            {
                GTTabulationItems.Where(x => x.QuestionIndex == _dataExport_LBVariablesForGTST[j].QuestionIndex).First().Test = _dataExport_LBVariablesForGTST[j].Test;
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
        private void SaveGT()
        {
            try
            {
                excel.Worksheet GtHiddenSheet = Util.ExcelUtil.GetWorkSheetByCodeName(Workbook, Util.Constants.SheetCodeName.GTTabulationS);
                excel.Range starCell = GtHiddenSheet.Cells[Constants.GT.GtRowDataStart, 1];
                excel.Range gtEnd = Util.ExcelUtil.EndxlUp(starCell);
                excel.Range valuesRange = GtHiddenSheet.get_Range(starCell, gtEnd.Offset[0, 205]);
                int len = 0;
                if (valuesRange.Value is string)
                    len = 1;
                else
                    len = ((object[,])valuesRange.Value).GetLength(0);
                if (_dataExport_LBVariablesForGT.Count > len)
                    len = _dataExport_LBVariablesForGT.Count;
                object[,] objs = new object[len, 206];
                int row = 0;
                for (int t = 0; t < GTTabulationItems.Count; t++)
                {
                    DataGT item = GTTabulationItems[t];

                    objs[row, 0] = item.GTFlag;
                    objs[row, 1] = item.ONOFF;
                    objs[row, 2] = item.QSType;
                    objs[row, 3] = item.Test;
                    objs[row, 4] = item.Graph;
                    objs[row, 5] = item.QSType == Constants.GT.GTN || item.QSType == Constants.GT.GTSA || item.QSType == Constants.GT.GTMA ? "" : formUtil.UnEscapeCRLF(item.QSHeading);
                    objs[row, 6] = item.Variable;

                    int qsNum = item.QuestionNumber;
                    if (qsNum > 0)
                    {
                        for (int j = 1; j < qsNum; j++)
                        {
                            t++;
                            objs[row, 6 + j] = GTTabulationItems[t].Variable;
                        }
                    }
                    row++;
                }
                GtHiddenSheet.Cells[Constants.GT.GtRowDataStart, 1].Resize[len, 206].Value = objs;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
            }
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
                    if (obj[i, 1].ToString() == "F_Cr_Cross_AddUp_Text_Summary_Change_Hyoutou_S" || obj[i, 1].ToString() == "F_Cr_Cross_AddUp_Text_Summary_Change_Non_S")
                    {
                        if (obj[i, 1].ToString() == "F_Cr_Cross_AddUp_Text_Summary_Change_Hyoutou_S")
                        {
                            obj[i, 2] = ReadValueFromExcel[obj[i, 1]] == LocalResource.CROSS_TXT_BX_ALL ? CROSS_TXT_BX_ALL_CLRF ? QC4Common.Common.Constants.CRLFchar : ReadValueFromExcel[obj[i, 1]] : ReadValueFromExcel[obj[i, 1]];
                        }
                        else if (obj[i, 1].ToString() == "F_Cr_Cross_AddUp_Text_Summary_Change_Non_S")
                        {
                            obj[i, 2] = ReadValueFromExcel[obj[i, 1]] == LocalResource.CROSS_TXT_BX_NOANSWER ? CROSS_TXT_BX_NONE_CLRF ? QC4Common.Common.Constants.CRLFchar : ReadValueFromExcel[obj[i, 1]] : ReadValueFromExcel[obj[i, 1]];
                        }
                    }
                    else if (ReadValueFromExcel.ContainsKey(elementsInSheet[i - 2]))
                    {
                        obj[i, 2] = ReadValueFromExcel[obj[i, 1]];
                    }
                }
                else
                {
                    break;
                }
            }
            rar.Value2 = obj;
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
        private bool CheckRefine()
        {
            if (Check_Advantage_99.IsChecked == true && Check_Advantage_95.IsChecked == true && Check_Advantage_90.IsChecked == true)
            {
                MessageDialog.ErrorOk(LocalResource.GT_VALIDATION_SIGNTEST_MAX);
                return false;
            }
            if (Check_Advantage_99.IsChecked == false && Check_Advantage_95.IsChecked == false && Check_Advantage_90.IsChecked == false)
            {
                MessageDialog.ErrorOk(LocalResource.GT_VALIDATION_SIGNTEST_MIN);
                return false;
            }
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
            bool validpath = false;
            bool combohasvalue = false;
            foreach (ComboBox cb in FindControls.FindLogicalChildren<ComboBox>(this.Page_GT_Narrow))
            {
                if (cb.Name == "Combo_Classify_Item")
                {
                    if (cb.SelectedItem != null && (((DataExport)cb.SelectedItem).QuestionVariable != null &&
                   ((DataExport)cb.SelectedItem).QuestionVariable != ""))
                        combohasvalue = true;
                    break;
                }
            }
            foreach (TextBox tb in FindControls.FindLogicalChildren<TextBox>(this.Page_GT_Narrow))
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
            bool Check_Refine_Condition = false;
            foreach (CheckBox tb
                in FindControls.FindLogicalChildren<CheckBox>(this.Page_GT_Narrow))
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
                in FindControls.FindLogicalChildren<ComboBox>(this.Page_GT_Narrow))
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
                in FindControls.FindLogicalChildren<TextBox>(this.Page_GT_Narrow))
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
                    MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_FILTER_CRITERIA_VARIABLE_MISSING, 1));
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

        private void List_GT_Summary_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            Edit();
        }

        private void Check_Summary_WeightBack_Checked(object sender, RoutedEventArgs e)
        {           
            WeightBackControlState(true,Colors.Black);
            if (Combo_Summary_WeightBack.SelectedIndex > 0)
            {
                Text_GT_WB_Message.Text = LocalResource.GT_TABULATION_WB;
            }

        }

        private void Check_Summary_WeightBack_Unchecked(object sender, RoutedEventArgs e)
        {
            WeightBackControlState(false, Colors.DimGray);
            Text_GT_WB_Message.Text = "";
        }

        private void Check_Rate_Checked(object sender, RoutedEventArgs e)
        {
            CheckRateControlState(true);
        }

        private void Check_Rate_Unchecked(object sender, RoutedEventArgs e)
        {
            Text_Input_Rate.Text = "0";
            CheckRateControlState(false);
        }

        private void CheckRateControlState(bool state)
        {
            Text_Input_Rate.IsEnabled = state;
            Btn_Set_Up.IsEnabled = state;
            Btn_Set_Down.IsEnabled = state;
        }

        private void WeightBackControlState(bool state, System.Windows.Media.Color color)
        {
            Combo_Summary_WeightBack.IsEnabled = state;
            Command_Summary_WeightBack.IsEnabled = state;
            Check_Output_Sort.IsEnabled = state;
            Check_Output_Sort.Foreground = new SolidColorBrush(color);
        }

        private void Text_Input_Rate_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Text_Input_Rate.Text.Trim().Length > 0)
            {
                SetOutputRatio();
            }
        }

        private void SetOutputRatio()
        {
            int numval = 0;
            try
            {
                numval = Convert.ToInt32(Text_Input_Rate.Text);
            }
            catch
            {
                numval = 0;
            }

            if (numval < Constants.GT.DifferenceSetMinValueGT)
                numval = Constants.GT.DifferenceSetMinValueGT;
            if (numval > Constants.GT.DifferenceSetMaxValueGT)
                numval = Constants.GT.DifferenceSetMaxValueGT;
            Text_Input_Rate.Text = numval.ToString();
        }

        private void Text_Input_Rate_LostFocus(object sender, RoutedEventArgs e)
        {
            int res = 0;
            if (((TextBox)sender).Name == "Text_Outputs_Ratios_EqualToLessThan")
            {

                if (this.Text_Input_Rate.Text != null && this.Text_Input_Rate.Text != "" && (int.TryParse(this.Text_Input_Rate.Text, out res)))
                {
                    int numval = Convert.ToInt32(this.Text_Input_Rate.Text);
                    if (numval > Constants.MarkingMaxValue)
                    {
                        this.Text_Input_Rate.Text = Constants.MarkingMaxValue.ToString();
                    }
                }
                else
                {
                    this.Text_Input_Rate.Text = Constants.MarkingMinValue.ToString();
                }
            }
        }

        private void Text_Input_Rate_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }

        private void Text_Input_Rate_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        Button Sender = null;
        private void Btn_Set_Up_PreviewMouseUp(object sender, MouseButtonEventArgs e)
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
            if (Sender.Name == "Btn_Set_Up")
            {
                numval = Convert.ToInt32(Text_Input_Rate.Text);
                numval++;
                if (numval > Constants.GT.DifferenceSetMaxValueGT)
                    numval = Constants.GT.DifferenceSetMaxValueGT;
                Text_Input_Rate.Text = numval.ToString();
            }
            else if (Sender.Name == "Btn_Set_Up")
            {
                numval = Convert.ToInt32(Text_Input_Rate.Text);
                numval++;
                if (numval > Constants.GT.DifferenceSetMaxValueGT)
                    numval = Constants.GT.DifferenceSetMaxValueGT;
                Text_Input_Rate.Text = numval.ToString();
            }
            else if (Sender.Name == "Btn_Set_Down")
            {
                numval = Convert.ToInt32(Text_Input_Rate.Text);
                numval--;
                if (numval < Constants.GT.DifferenceSetMinValueGT)
                    numval = Constants.GT.DifferenceSetMinValueGT;
                Text_Input_Rate.Text = numval.ToString();
            }
            else if (Sender.Name == "Btn_Set_Down")
            {
                numval = Convert.ToInt32(Text_Input_Rate.Text);
                numval--;
                if (numval < Constants.GT.DifferenceSetMinValueGT)
                    numval = Constants.GT.DifferenceSetMinValueGT;
                Text_Input_Rate.Text = numval.ToString();
            }
        }

        private void Btn_Set_Up_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Sender = ((Button)sender);
            SetRate();
            Thread.Sleep(200);
            dispatcherTimer.Start();
        }

        private void Command_Select_Right_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var items = List_Test.SelectedItems.Cast<DataGT>().ToList();
                if (items.Count > 0)
                {
                    List<DataGT> gtSAMA = items.Where(p => p.QSType == "GT-MTS" || p.QSType == "GT-SA" || p.QSType == "GT-RNK" || p.QSType == "GT-MTM" || p.QSType == "GT-MA" || p.QSType == "GT-N").ToList();

                    if (gtSAMA.Count < 1)
                    {
                        MessageDialog.ErrorOk(LocalResource.CANNOT_SELECT_TABLE_TYPE_ERROR);
                        return;
                    }
                    else if (gtSAMA.Count != items.Count)
                    {
                        List<DataGT> gtRATMTN = items.Where(p => p.QSType == "GT-RAT" || p.QSType == "GT-MTN").ToList();
                        foreach (var sItem in gtRATMTN)
                        {
                            List_Test.SelectedItems.Remove(sItem);
                        }
                        MessageDialog.Warning(LocalResource.TABLE_TYPES_EXCLUDED_WARNING);
                    }
                    foreach (var item in gtSAMA)
                    {
                        item.Test = "1";
                        _dataExport_LBVariablesForGTST_Right.Add(item);
                        _dataExport_LBVariablesForGTST.Remove(item);
                        _dataExport_LBVariablesForGT.Where(x => x.QuestionIndex == item.QuestionIndex).First().Test = item.Test;
                        GTTabulationItems.Where(x => x.QuestionIndex == item.QuestionIndex).First().Test = item.Test;
                    }
                    _dataExport_LBVariablesForGTST_Right.OrderBy(p => p.QuestionNumber);
                }
                LoadListTestBox();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
            }
        }

        private void LoadListTestBox()
        {
            List_Test.ItemsSource = _dataExport_LBVariablesForGTST.OrderBy(p => p.QuestionIndex);
            List_Test_Select.ItemsSource = _dataExport_LBVariablesForGTST_Right.OrderBy(p => p.QuestionIndex);
            List_Test_Item.ItemsSource = _dataExport_LBVariablesForGTSTMTS_Right.OrderBy(p => p.QuestionIndex); 
        }

        private void Command_Select_Left_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var items = List_Test_Select.SelectedItems.Cast<DataGT>().ToList();
                if (items.Count > 0)
                {
                    foreach (var item in items)
                    {
                        item.Test = "";
                        _dataExport_LBVariablesForGTST.Add(item);
                        _dataExport_LBVariablesForGTST_Right.Remove(item);
                        _dataExport_LBVariablesForGT.Where(x => x.QuestionIndex == item.QuestionIndex).First().Test = item.Test;
                        GTTabulationItems.Where(x => x.QuestionIndex == item.QuestionIndex).First().Test = item.Test;
                    }
                }
                LoadListTestBox();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
            }
        }

        private void Command_Question_Right_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var items = List_Test.SelectedItems.Cast<DataGT>().ToList();
                if (items.Count > 0)
                {
                    List<DataGT> gtMTS = items.Where(p => p.QSType == "GT-MTS" || p.QSType == "GT-MTN" || p.QSType == "GT-RNK" || p.QSType == "GT-MTM" || p.QSType == "GT-RAT").ToList();

                    if (gtMTS.Count < 1)
                    {
                        MessageDialog.ErrorOk(LocalResource.TOTAL_TABLE_TYPES_CANNOT_BE_SELECTED_ERROR);
                        return;
                    }
                    else if (gtMTS.Count != items.Count)
                    {
                        List<DataGT> gtSAMAN = items.Where(p => p.QSType == "GT-SA" || p.QSType == "GT-MA" || p.QSType == "GT-N").ToList();
                        foreach (var sItem in gtSAMAN)
                        {
                            List_Test.SelectedItems.Remove(sItem);
                        }
                        MessageDialog.Warning(LocalResource.TOTAL_TABLE_TYPES_CANNOT_BE_SELECTED_WARNING);
                    }
                    foreach (var item in gtMTS)
                    {
                        item.Test = "2";
                        _dataExport_LBVariablesForGTSTMTS_Right.Add(item);
                        _dataExport_LBVariablesForGTST.Remove(item);
                        _dataExport_LBVariablesForGT.Where(x => x.QuestionIndex == item.QuestionIndex).First().Test = item.Test;
                        GTTabulationItems.Where(x => x.QuestionIndex == item.QuestionIndex).First().Test = item.Test;
                    }
                }
                LoadListTestBox();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
            }
        }

        private void Command_Question_Left_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var items = List_Test_Item.SelectedItems.Cast<DataGT>().ToList();
                if (items.Count > 0)
                {
                    foreach (var item in items)
                    {
                        item.Test = "";
                        _dataExport_LBVariablesForGTST.Add(item);
                        _dataExport_LBVariablesForGTSTMTS_Right.Remove(item);
                        _dataExport_LBVariablesForGT.Where(x => x.QuestionIndex == item.QuestionIndex).First().Test = item.Test;
                        GTTabulationItems.Where(x => x.QuestionIndex == item.QuestionIndex).First().Test = item.Test;
                    }
                }
                LoadListTestBox();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
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

        private void Combo_Summary_WeightBack_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Check_Summary_WeightBack.IsChecked == true)
            {
                if (Combo_Summary_WeightBack.SelectedIndex > 0)
                {
                    Text_GT_WB_Message.Text = LocalResource.GT_TABULATION_WB;
                }
                else
                {
                    Text_GT_WB_Message.Text = "";
                }
            }
            else
            {
                Text_GT_WB_Message.Text = "";
            }
        }

        private void Check_All_Base_Checked(object sender, RoutedEventArgs e)
        {
            Text_GT_Base_Message.Text = LocalResource.GT_TABULATION_BASE;
        }

        private void Check_All_Base_Unchecked(object sender, RoutedEventArgs e)
        {
            Text_GT_Base_Message.Text = "";
        }      

        private void List_GT_Summary_SelectionChanged(object sender, SelectedCellsChangedEventArgs e)
        {

            if (List_GT_Summary.SelectedItems.Count == 1)
            {
                Command_GT_Summary_Copy.IsEnabled = true;
                Command_GT_Summary_Edit.IsEnabled = true;
            }
            else
            {
                Command_GT_Summary_Copy.IsEnabled = false;
                Command_GT_Summary_Edit.IsEnabled = false;
            }
            if (List_GT_Summary.SelectedItems.Count > 0)
            {
                Command_GT_Summary_Delete.IsEnabled = true;
                Command_GT_Summary_Change.IsEnabled = true;
            }
            else
            {
                Command_GT_Summary_Delete.IsEnabled = false;
                Command_GT_Summary_Change.IsEnabled = false;
            }
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            Edit();
        }

        private void List_GT_Summary_CurrentCellChanged(object sender, EventArgs e)
        {
            var dg = (System.Windows.Controls.DataGrid)sender;
            ExpGrid = dg;
            dg.Focus();
        }

        private void b1SetColor(object sender, MouseButtonEventArgs e)
        {
            if (ExpGrid != null)
                ExpGrid.Focus();
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

            if (grid != null && e.Key == Key.Tab && grid.Name is "List_Test")
            {
                e.Handled = true;
                Command_Select_Right.Focus();
            }
            else if (btn != null && e.Key == Key.Tab && btn.Name is "Command_Select_Left")
            {
                e.Handled = true;
                Style style = Application.Current.FindResource("MyFocusVisual") as Style;
                List_Test_Select.FocusVisualStyle = style;
                List_Test_Select.Focus();
            }
            else if (chk != null && e.Key == Key.Tab && chk.Name is "Check_Advantage_99")
            {
                e.Handled = true;
                Command_Execute.Focus();
            }
            else if (grid != null && e.Key == Key.Tab && grid.Name is "List_Test_Select")
            {
                e.Handled = true;
                Command_Question_Right.Focus();
            }
            else if (btn != null && e.Key == Key.Tab && btn.Name is "Command_Question_Left")
            {
                e.Handled = true;
                Style style = Application.Current.FindResource("MyFocusVisual") as Style;
                List_Test_Item.FocusVisualStyle = style;
                List_Test_Item.Focus();
            }
            else if (grid != null && e.Key == Key.Tab && grid.Name is "List_Test_Item")
            {
                e.Handled = true;
                Check_Advantage_90.Focus();
            }
            else if (grid != null && e.Key == Key.Tab && grid.Name is "List_GT_Summary")
            {
                e.Handled = true;
                Command_GT_Summary_New.Focus();
            }
            else if (Page_GT_Test.IsFocused && e.Key == Key.Tab)
            {
                e.Handled = true;
                List_GT_Summary.Focus();
            }
            QC4Common.Util.GridCommonFunc.Arrow(sender, e);
        }

        private void Window_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
        
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                if (Page_GT_Summary.IsFocused)
                {
                    Style style = Application.Current.FindResource("MyFocusVisual") as Style;
                    List_GT_Summary.FocusVisualStyle = style;
                    List_GT_Summary.Focus();
                    e.Handled = true;
                }
                else if (Page_GT_Summary.IsFocused)
                {
                    FilterControlDesignForGT.FirstFocus = true;
                }
            }
        }

        bool FisrtTabFocus = true;
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as TabControl).SelectedIndex == 1 && FisrtTabFocus)
            {
                Page_GT_Narrow.Focus();
                FisrtTabFocus = false;
            }
            else if((sender as TabControl).SelectedIndex != 1)
            {
                FisrtTabFocus = true;
            }
        }
    }
}
