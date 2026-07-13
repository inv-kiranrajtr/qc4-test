
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

namespace Qc4Launcher.Forms.Cross_Tabulation
{
    /// <summary>
    /// Interaction logic for TabulationSetting.xaml
    /// </summary>
    public partial class TabulationSetting : UserControl
    {
        excel.Workbook Workbook;
        MainWindow MainWindow;
        bool IsCreateReport = false;
        bool ISWBAdded = false;
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private ProgressBar progress = null;
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

        private ObservableCollection<DataExport> _dataExport_LBVariablesToExport = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _dataExport_ListBoxCommonCopy = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD1 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD2 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD3 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD4 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD5 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD6 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablnumeric = new ObservableCollection<DataExport>();

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



        public TabulationSetting(excel.Workbook workbook, string filePath)
        {
            InitializeComponent();
         //   Combo_Summary_WeightBack.IsEnabled = false;
            Check_Summary_SignificantDifferece_Test.IsChecked = false;
            Workbook = workbook;
            dataLoaded();
            Form_loaded();
            LoadingData();
            SelectedFile = filePath;
            Workbook = workbook;
            SelectedFile = filePath;
            Check_Summary_SignificantDifferece_Test_Check();



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

        }


        private void Text_Summary_Mark_N_Equal_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void Text_Summary_Mark_N_Equal_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void Text_Summary_Mark_N_Equal_KeyUp(object sender, KeyEventArgs e)
        {

        }
        private void LoadInitialValuesToTabulationControls()
        {

            this.Text_Summary_Mark_N_Equal.Text = Constants.MarkingValue < 0 ? "0" : Constants.MarkingValue.ToString();
            this.Text_Summary_Rate_Difference1.Text = Constants.DifferenceSet1Value.ToString();
            this.Text_Summary_Rate_Difference2.Text = Constants.DifferenceSet2Value.ToString();
            this.Check_Summary_Mark_Ranking.IsChecked = false;
            this.Check_Summary_Mark_Ratio1.IsChecked = true;
            this.Combo_Summary_Rate_Difference1.SelectedIndex = 0;
            this.Combo_Summary_Rate_Difference2.SelectedIndex = 0;
            //  this.Combo_SignificantDifference_Test.SelectedIndex = 0;
            this.Combo_Summary_Rate_Difference1.IsEnabled = true;
            this.Combo_Summary_Rate_Difference2.IsEnabled = true;
            this.BTN_SET_1_UP.IsEnabled = true;

            this.BTN_SET_1_DOWN.IsEnabled = true;
            this.BTN_SET_2_UP.IsEnabled = true;
            this.BTN_SET_2_DOWN.IsEnabled = true;
            this.Text_Summary_Rate_Difference1.IsEnabled = true;
            this.Text_Summary_Rate_Difference2.IsEnabled = true;
            this.Check_Summary_SignificantDifferece_Test.IsChecked = false;
            //  this.Combo_SignificantDifference_Test.IsEnabled = false;
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
            // this.Check_All_Base.IsChecked = false;
            this.Option_Output_SheetType_One.IsChecked = false;
            this.Option_Output_SheetType_Plural.IsChecked = true;
            this.Check_Output_Cross_N_Par.IsChecked = true;
            this.Check_Output_Cross_N.IsChecked = true;
            this.Check_Output_Cross_Par.IsChecked = true;
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
        public void Form_loaded()
        {

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
                        obj[i, 2] = ReadValueFromExcel[obj[i, 1]];
                        if (elementsInSheet[i - 2] == "F_Cr_Cross_AddUp_Text_Summary_Change_Hyoutou_S" || elementsInSheet[i - 2] == "F_Cr_Cross_AddUp_Text_Summary_Change_Non_S")
                        {
                            for (int j = 2; j < rowvalue; j++)
                            {
                                if (obj[j, 1] != null && obj[j, 1].ToString() == "F_Gt_GT_AddUp_Text_Summary_Change_Hyoutou_S" && elementsInSheet[i - 2] == "F_Cr_Cross_AddUp_Text_Summary_Change_Hyoutou_S")
                                {
                                    obj[j, 2] = ReadValueFromExcel[obj[i, 1]];
                                }
                                else if (obj[j, 1] != null && obj[j, 1].ToString() == "F_Gt_GT_AddUp_Text_Summary_Change_Non_S" && elementsInSheet[i - 2] == "F_Cr_Cross_AddUp_Text_Summary_Change_Non_S")
                                {
                                    obj[j, 2] = ReadValueFromExcel[obj[i, 1]];
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
            Workbook.Save();
        }
        private void GetValuesOfControls()
        {
            ReadValueFromExcel.Clear();

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
        private void LoadingData()
        {
            fs.LoadingData(out _dataExport_LBVariablesToExport, out _qstnvariablDD1, out _qstnvariablDD2);

            _qstnvariablDD3 = new ObservableCollection<DataExport>(_qstnvariablDD2);
            _qstnvariablDD4 = new ObservableCollection<DataExport>(_qstnvariablDD2);
            _qstnvariablDD5 = new ObservableCollection<DataExport>(_qstnvariablDD2);
            _qstnvariablDD6 = new ObservableCollection<DataExport>(_qstnvariablDD2);
            GetNType();

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


        private void Text_Summary_Mark_N_Equal_KeyUp_1(object sender, KeyEventArgs e)
        {
            int res = 0;
            if (((TextBox)sender).Name == "Text_Summary_Mark_N_Equal")
            {
                int mxval = DB.DataOutput.Rowcount(Workbook);
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
                    this.Text_Summary_Mark_N_Equal.Text = "0";
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
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");//"[^0-9]+"

            e.Handled = regex.IsMatch(e.Text);
        }
        private void textBox_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }
        private void Check_Summary_Rate_Difference1_Checked(object sender, RoutedEventArgs e)
        {
            Check_Summary_Rate_Difference1_Check();
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
        private void text_LostFocus(object sender, RoutedEventArgs e)
        {
            int res = 0;
            if (((TextBox)sender).Name == "Text_Summary_Mark_N_Equal")
            {
                int maxVal = DB.DataOutput.RowCount(Workbook);
                // string s = this.Text_Summary_Mark_N_Equal.Text.Replace(" ",string.Empty);
                if (this.Text_Summary_Mark_N_Equal.Text != null && this.Text_Summary_Mark_N_Equal.Text != "" && (int.TryParse(this.Text_Summary_Mark_N_Equal.Text.Replace(" ", string.Empty), out res)))
                {
                    int numval = Convert.ToInt32(this.Text_Summary_Mark_N_Equal.Text.Replace(" ", string.Empty));
                    if (numval > maxVal)
                    {
                        if (numval <= 0)
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
                    this.Text_Summary_Mark_N_Equal.Text = "0";
                }
                //  if(!string .IsNullOrEmpty( this.Text_Summary_Mark_N_Equal.Text)) Text_Summary_Mark_N_Equal.Select(Text_Summary_Mark_N_Equal.Text.Length, 0);
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
        Button Sender = null;
        private void BTN_SET_1_UP_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Sender = ((Button)sender);
            SetRate();
            Thread.Sleep(200);
            dispatcherTimer.Start();
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

        private void Check_Summary_WeightBack_Checked(object sender, RoutedEventArgs e)
        {

        }

        private string SelectedFile = "";
        private void BTN_WEIGHT_Click(object sender, RoutedEventArgs e)
        {
            weightbak = false;
            if (Combo_Summary_WeightBack.Text.Contains(Constants.WeightBack))
            { weightbak = true; }
            SampleWeightBack swb = new SampleWeightBack(Workbook, SelectedFile, _qstnvariablDD1, _qstnvariablnumeric, ref weightbak);
            swb.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            swb.Owner = Window.GetWindow(this);
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
        private void Check_Summary_WeightBack_Check()
        {
            if (Check_Summary_WeightBack.IsChecked == true)
            {
                this.Combo_Summary_WeightBack.IsEnabled = true;
                this.BTN_WEIGHT.IsEnabled = true;
                this.OutputUnweightbackedTotalCheck.IsEnabled = true;
                if (this.OutputUnweightbackedTotalCheck.IsChecked == true)
                    this.UnweightbackedBaseCheck.IsEnabled = true;

            }
            else
            {
                this.Combo_Summary_WeightBack.IsEnabled = false;
                this.BTN_WEIGHT.IsEnabled = false;
                this.OutputUnweightbackedTotalCheck.IsEnabled = false;
                this.UnweightbackedBaseCheck.IsEnabled = false;

            }
        }
        private void Check_Summary_SignificantDifferece_Test_Checked(object sender, RoutedEventArgs e)
        {
            
                Check_Summary_SignificantDifferece_Test_Check();

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
        private void BTN_SET_1_UP_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            dispatcherTimer.Stop();
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            SetRate();
        }
        private void Combo_SignificantDifference_Test_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        //CheckBox Custom Event Handler
        public class MyCheckBoxClickEventArgs : EventArgs
        {
            public string sendr { get; set; }
            public bool Check { get; set; }
        }
        public delegate void MyCheckBoxClickEventHandler(object sender, MyCheckBoxClickEventArgs e);
        public event MyCheckBoxClickEventHandler MyCheckBoxClick;
        private void OnCheck(object sender, RoutedEventArgs e)
        {
            
            if (Check_Summary_WeightBack.IsChecked == true)
            {
                this.Combo_Summary_WeightBack.IsEnabled = true;
                this.BTN_WEIGHT.IsEnabled = true;
                this.OutputUnweightbackedTotalCheck.IsEnabled = true;
                if (this.OutputUnweightbackedTotalCheck.IsChecked == true)
                {
                    this.UnweightbackedBaseCheck.IsEnabled = true;
                }
                 
            }
            else
            {

                this.Combo_Summary_WeightBack.IsEnabled = false;
                this.BTN_WEIGHT.IsEnabled = false;
                this.OutputUnweightbackedTotalCheck.IsEnabled = false;
                this.UnweightbackedBaseCheck.IsEnabled = false;
                if (checkflag)
                {
                    Check_Summary_WeightBack_Check();
                }

            }
            MyCheckBoxClick?.Invoke(this,
                    new MyCheckBoxClickEventArgs() { sendr = ((CheckBox)sender).Name, Check = (bool)((CheckBox)sender).IsChecked });


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
        private void dataLoaded()

            {
            try
            {
                foreach (RadioButton tb
                        in FindControls.FindLogicalChildren<RadioButton>(Output_summary))
                {
                    GetName_Page_Refine(tb);
                }

                foreach (TextBox tb
                    in FindControls.FindLogicalChildren<TextBox>(Tabulation_options))
                {
                    GetName_Page_Refine(tb);
                }
                foreach (CheckBox tb
                    in FindControls.FindLogicalChildren<CheckBox>(Output_summary))
                {
                    GetName_Page_Refine(tb);
                }
                foreach (CheckBox tb
                   in FindControls.FindLogicalChildren<CheckBox>(WeightBack))
                {
                    GetName_Page_Refine(tb);
                }
                foreach (CheckBox tb
                  in FindControls.FindLogicalChildren<CheckBox>(Making_group))
                {
                    GetName_Page_Refine(tb);
                }
                foreach (RadioButton tb
                 in FindControls.FindLogicalChildren<RadioButton>(Making_group))
                {
                    GetName_Page_Refine(tb);
                }
                foreach (CheckBox tb
                 in FindControls.FindLogicalChildren<CheckBox>(crossanswer))
                {
                    GetName_Page_Refine(tb);
                }

                LoadInitialValuesToTabulationControls();
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
                for (int i = 0; i < elementsInSheet.Count; i++)
                {
                    if (controlObj.ContainsKey(elementsInSheet[i]))
                    {
                        if (controlObj[elementsInSheet[i]] is TextBox)
                        {
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
                            {
                                string str = null == obj[i + 2, 2] ? "" : obj[i + 2, 2].ToString();
                                if (str.Equals("True"))
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
                        elementsInSheet.Add(keys[i]);
                    }
                }
                if (settingFirst)
                {
                    rar.Value2 = obj;

                }

                checkflag = true;

                if (this.Check_Summary_Mark_Ratio1.IsChecked == false)

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

                FilterControlDesign.MyCheckBoxClickEventArgs cargs = new FilterControlDesign.MyCheckBoxClickEventArgs();
                cargs.Check = filtersettingcheckflag;
                cargs.sendr = "Check_Refine_Condition";
            }
            catch
            {

            }
        }

        private void Check_Difference_Marking_Checked(object sender, RoutedEventArgs e)
        {
            Check_Difference_Marking_Check();
        }
        private void Check_Difference_Marking_Check()
        {
            if (Check_Summary_Mark_Ratio1.IsChecked == true)
            {
                this.Check_Summary_Rate_Difference1.IsChecked = false;
                this.Check_Summary_Rate_Difference2.IsChecked = false;
                this.Check_Summary_Rate_Difference1.IsEnabled = true; ;
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
                this.Check_Summary_Rate_Difference1.IsEnabled = false; ;
                this.Check_Summary_Rate_Difference2.IsEnabled = false;
            }
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

        private void Combo_Summary_WeightBack_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {

        }

        private void Combo_Summary_WeightBack_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Combo_Summary_WeightBack.IsDropDownOpen = true;
        }

        private void Combo_Classify_Item_Loaded(object sender, RoutedEventArgs e)
        {
            
            System.Windows.Controls.TextBox textBox = Combo_Summary_WeightBack.Template.FindName("PART_EditableTextBox", Combo_Summary_WeightBack) as System.Windows.Controls.TextBox;
            if (textBox != null)
            {
                textBox.AddHandler(System.Windows.Controls.TextBox.PreviewMouseDownEvent, new MouseButtonEventHandler(textBox_PreviewMouseDown), true);
            }
        }
        void textBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Combo_Summary_WeightBack.IsDropDownOpen = true;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (e.AddedItems.Count > 0)
            {
               
            }
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

        private void Rd_btn_chk2_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Combo_SignificantDifference_Test_SelectionChanged(object sender, RoutedEventArgs e)
        {
            try
            {

                bool selectedindex = (bool)rd_btn_chk2.IsChecked;

                if (selectedindex == true)
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
            catch { }
        }

        private void Rd_btn_chk_Unchecked(object sender, RoutedEventArgs e)
        {
            Check_Summary_SignificantDifferece_Test_Check();
        }
    }
}

