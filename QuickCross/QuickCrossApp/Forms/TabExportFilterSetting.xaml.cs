
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Excel = Microsoft.Office.Interop.Excel;
using Vb = Microsoft.VisualBasic;
using System.Windows.Forms;
using System.Linq;
using System.Windows.Media;
using System.Windows.Input;
using Qc4Launcher.Util;
using System.Text;
using Qc4Launcher.Classes;
using FilterSettingsView;
using static FilterSettingsView.FilterSettingsClass;
using Qc4Launcher.Logic;
using QC4Common.Model;
using log4net;
using System.Reflection;
using QC4Common.Validation;
using System.IO;
using System.Data.SQLite;

namespace Qc4Launcher.Forms
{
    /// <summary>
    /// Interaction logic for TabExportFilterSetting.xaml
    /// </summary>
    public partial class TabExportFilterSetting : Window
    {
        Excel.Workbook Workbook;
        MainWindow MainWndw = null;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        int CmbClsy = 0;
        int CmbItm1 = 0;
        int CmbItm2 = 0;
        int CmbItm3 = 0;
        int CmbItm4 = 0;
        int CmbItm5 = 0;
        bool CloseNotFromBtn = true;

        private string Combo_Conditional_Item_1selectedQuestionVariable = string.Empty;
        private string Combo_Conditional_Item_2selectedQuestionVariable = string.Empty;
        private string Combo_Conditional_Item_3selectedQuestionVariable = string.Empty;
        private string Combo_Conditional_Item_4selectedQuestionVariable = string.Empty;
        private string Combo_Conditional_Item_5selectedQuestionVariable = string.Empty;
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

        private ObservableCollection<DataExport> _dataExport_LBVariablesToExport = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _dataExport_ListBoxCommonCopy = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD1 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD2 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD3 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD4 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD5 = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablDD6 = new ObservableCollection<DataExport>();

        private List<DataExport> dataFromSheet = new List<DataExport>();//private List<DataExport> dataFromSheet = new List<DataExport>();
        private List<DataExport> Combo_conditional = new List<DataExport>();
        private List<DataExport> Combo_Classify = new List<DataExport>();
        private Dictionary<String, QuestionSettings> PopulatedDictionary = new Dictionary<string, QuestionSettings>();
        private List<String> elementsInSheet = new List<String>();
        private int lastIndexinAdvancedSettingsExcelSheet = 0;

        private Excel.Range rarCom;
        private Dictionary<string, System.Windows.Controls.Control> controlObj = new Dictionary<string, System.Windows.Controls.Control>();
        private Dictionary<string, String> ReadValueFromExcel = new Dictionary<string, String>();

        private string SelectedFile = "";
        public string SelectedFileFormat = string.Empty;
        bool IsInitialLoad = false;
        public bool IsNewExist = false;

        private string filesextension = "";
        FilterSettingsView.FilterSettingsClass fs = new FilterSettingsClass();
        bool IsProVersion = false;

        public TabExportFilterSetting(Excel.Workbook workbook, string filePath,bool isAllow=true,MainWindow main=null)
        {
            MainWndw = main;
            Workbook = workbook;
            if (isAllow && QC4Common.DB.DBHelper.CheckIfNewExists(Workbook, Definiotion.VariableDictionary.Select(x => x.Value).ToList()) > 0)
            {
                IsNewExist = true;
                return;
            }
            MainWndw.Hide();
            InitializeComponent();
            SelectedFile = filePath;
            var cbi2 = new ComboBoxItem();
            cbi2.Content = LocalResource.DO_OUTPUT_FORMAT_QC4;
            Combo_Output_FileType.Items.Add(cbi2);

            if (CommonFunction.ActivationKeyChecking())
            {
                //var cbi1 = new ComboBoxItem();
                //cbi1.Content = LocalResource.DO_OUTPUT_FORMAT_QC3;
                //Combo_Output_FileType.Items.Add(cbi1);
                
                var cbi3 = new ComboBoxItem();
                cbi3.Content = LocalResource.DO_OUTPUT_FORMAT_QLAYOUT;
                Combo_Output_FileType.Items.Add(cbi3);

                // Add the R2D3 ComboBoxItem
                var cbi4 = new ComboBoxItem
                {
                    Content = LocalResource.DO_OUTPUT_FORMAT_R2D3
                };
                Combo_Output_FileType.Items.Add(cbi4);

                Check_Vertical.Visibility = Visibility.Visible;
                IsProVersion = true;
                //Combo_Output_FileType.Items.Add(LocalResource.DO_OUTPUT_FORMAT_QC4);
                //Combo_Output_FileType.Items.Add(LocalResource.DO_OUTPUT_FORMAT_QLAYOUT);
            }

            //ToDelete
            //Combo_Output_FileType.SelectedIndex = 0;
            //Combo_Output_Type.SelectedIndex = 0;
            //Combo_NonAnser.SelectedIndex = 0;
            //Combo_NonApplying.SelectedIndex = 0;
            //ENd
        }

        public static Excel.Range EndxlUp(Excel.Range targetCell)
        {
            Excel.Range targetColumn = targetCell.Cells[1].EntireColumn;
            return targetColumn.Cells[targetColumn.Cells.Count].End(Excel.XlDirection.xlUp);
        }

        private void CBMA_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = Combo_Output_Type.SelectedIndex;
            if (selectedIndex == 0)
            {
                Combo_NonApplying.SelectedIndex = 0;
            }
            else if (selectedIndex == 1)
            {
                Combo_NonApplying.SelectedIndex = 1;
            }
            else if (selectedIndex == 2)
            {
                Combo_NonApplying.SelectedIndex = 0;
            }
        }

        private void Check_Refine_Condition_Checked(object sender, RoutedEventArgs e)
        {
            ConditionCheck();
        }

        private void ConditionCheck()
        {
            if (Check_Refine_Condition.IsChecked == true)
            {
                lblCriteriaVariable.IsEnabled = true;
                lblOperator.IsEnabled = true;
                lblValue.IsEnabled = true;
                lblCriteriaVariable.Foreground = new SolidColorBrush(Colors.Black);
                lblOperator.Foreground = new SolidColorBrush(Colors.Black);
                lblValue.Foreground = new SolidColorBrush(Colors.Black);
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

                EnableRadioOnLoad();

                Combo_Conditional_Value_1.Foreground = new SolidColorBrush(Colors.Black);
                Combo_Conditional_Item_1.Foreground = new SolidColorBrush(Colors.Black);
                Combo_Conditional_Operator_1.Foreground = new SolidColorBrush(Colors.Black);
                BTnFilter1.Foreground = new SolidColorBrush(Colors.Black);

                Combo_Conditional_Value_2.Foreground = new SolidColorBrush(Colors.Black);
                Combo_Conditional_Item_2.Foreground = new SolidColorBrush(Colors.Black);
                Combo_Conditional_Operator_2.Foreground = new SolidColorBrush(Colors.Black);
                BTnFilter2.Foreground = new SolidColorBrush(Colors.Black);

                Combo_Conditional_Value_3.Foreground = new SolidColorBrush(Colors.Black);
                Combo_Conditional_Item_3.Foreground = new SolidColorBrush(Colors.Black);
                Combo_Conditional_Operator_3.Foreground = new SolidColorBrush(Colors.Black);
                BTnFilter3.Foreground = new SolidColorBrush(Colors.Black);

                Combo_Conditional_Value_4.Foreground = new SolidColorBrush(Colors.Black);
                Combo_Conditional_Item_4.Foreground = new SolidColorBrush(Colors.Black);
                Combo_Conditional_Operator_4.Foreground = new SolidColorBrush(Colors.Black);
                BTnFilter4.Foreground = new SolidColorBrush(Colors.Black);

                Combo_Conditional_Value_5.Foreground = new SolidColorBrush(Colors.Black);
                Combo_Conditional_Item_5.Foreground = new SolidColorBrush(Colors.Black);
                Combo_Conditional_Operator_5.Foreground = new SolidColorBrush(Colors.Black);
                //radio text unfade
                Option_Conditional_And_1.Foreground = new SolidColorBrush(Colors.Black);
                Option_Conditional_And_2.Foreground = new SolidColorBrush(Colors.Black);
                Option_Conditional_And_3.Foreground = new SolidColorBrush(Colors.Black);
                Option_Conditional_And_4.Foreground = new SolidColorBrush(Colors.Black);

                Option_Conditional_Or_1.Foreground = new SolidColorBrush(Colors.Black);
                Option_Conditional_Or_2.Foreground = new SolidColorBrush(Colors.Black);
                Option_Conditional_Or_3.Foreground = new SolidColorBrush(Colors.Black);
                Option_Conditional_Or_4.Foreground = new SolidColorBrush(Colors.Black);
            }
            if (Check_Refine_Condition.IsChecked == false)
            {
                //----------Column Heading Enabled ---------------------

                lblCriteriaVariable.IsEnabled = false;
                lblOperator.IsEnabled = false;
                lblValue.IsEnabled = false;
                lblCriteriaVariable.Foreground = new SolidColorBrush(Colors.DimGray);
                lblOperator.Foreground = new SolidColorBrush(Colors.DimGray);
                lblValue.Foreground = new SolidColorBrush(Colors.DimGray);
                //-----------------------Row wise Enabled--------------------------------
                Combo_Conditional_Item_1.IsEnabled = false;
                Combo_Conditional_Operator_1.IsEnabled = false;
                Combo_Conditional_Value_1.IsEnabled = false;
                BTnFilter1.IsEnabled = false;
                Option_Conditional_And_1.IsEnabled = false;
                Option_Conditional_Or_1.IsEnabled = false;
                Combo_Conditional_Item_1.Foreground = new SolidColorBrush(Colors.DimGray);
                Combo_Conditional_Operator_1.Foreground = new SolidColorBrush(Colors.DimGray);


                Combo_Conditional_Item_2.IsEnabled = false;
                Combo_Conditional_Operator_2.IsEnabled = false;
                Combo_Conditional_Value_2.IsEnabled = false;
                BTnFilter2.IsEnabled = false;
                Option_Conditional_And_2.IsEnabled = false;
                Option_Conditional_Or_2.IsEnabled = false;
                Combo_Conditional_Item_2.Foreground = new SolidColorBrush(Colors.DimGray);
                Combo_Conditional_Operator_2.Foreground = new SolidColorBrush(Colors.DimGray);

                Combo_Conditional_Item_3.IsEnabled = false;
                Combo_Conditional_Operator_3.IsEnabled = false;
                Combo_Conditional_Value_3.IsEnabled = false;
                BTnFilter3.IsEnabled = false;
                Option_Conditional_And_3.IsEnabled = false;
                Option_Conditional_Or_3.IsEnabled = false;
                Combo_Conditional_Item_3.Foreground = new SolidColorBrush(Colors.DimGray);
                Combo_Conditional_Operator_3.Foreground = new SolidColorBrush(Colors.DimGray);

                Combo_Conditional_Item_4.IsEnabled = false;
                Combo_Conditional_Operator_4.IsEnabled = false;
                Combo_Conditional_Value_4.IsEnabled = false;
                BTnFilter4.IsEnabled = false;
                Option_Conditional_And_4.IsEnabled = false;
                Option_Conditional_Or_4.IsEnabled = false;
                Combo_Conditional_Item_4.Foreground = new SolidColorBrush(Colors.DimGray);
                Combo_Conditional_Operator_4.Foreground = new SolidColorBrush(Colors.DimGray);


                Combo_Conditional_Item_5.IsEnabled = false;
                Combo_Conditional_Operator_5.IsEnabled = false;
                Combo_Conditional_Value_5.IsEnabled = false;
                BTnFilter5.IsEnabled = false;
                Combo_Conditional_Item_5.Foreground = new SolidColorBrush(Colors.DimGray);
                Combo_Conditional_Operator_5.Foreground = new SolidColorBrush(Colors.DimGray);

                //fading radio text
                Option_Conditional_And_1.Foreground = new SolidColorBrush(Colors.DimGray);
                Option_Conditional_And_2.Foreground = new SolidColorBrush(Colors.DimGray);
                Option_Conditional_And_3.Foreground = new SolidColorBrush(Colors.DimGray);
                Option_Conditional_And_4.Foreground = new SolidColorBrush(Colors.DimGray);

                Option_Conditional_Or_1.Foreground = new SolidColorBrush(Colors.DimGray);
                Option_Conditional_Or_2.Foreground = new SolidColorBrush(Colors.DimGray);
                Option_Conditional_Or_3.Foreground = new SolidColorBrush(Colors.DimGray);
                Option_Conditional_Or_4.Foreground = new SolidColorBrush(Colors.DimGray);

            }
        }

        private void Combo_Classify_Item_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Combo_Classify_Item.IsEnabled = true;
                QC4Common.Util.FormUtil formUtil = new QC4Common.Util.FormUtil();
                int selectedindex = Combo_Classify_Item.SelectedIndex;
                string NoOfChoice = string.Empty;
                if (selectedindex > 0)
                {

                    this.TBAnsType.Text = _qstnvariablDD1[selectedindex].QuestionVariableType.Split('/')[0];
                    try
                    {
                        _qstnvariablDD1[selectedindex].QuestionChoiceNo = _qstnvariablDD1[selectedindex].QuestionVariableType.Split('/')[1];
                    }
                    catch(Exception ex) { _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException); }
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
            catch(Exception ex) { _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException); }
        }

        private void ListLoading()
        {
            //List<String> removItem = new List<String>();
            bool set = false;
            var SettingSheet = ExcelUtil.GetWorkSheetByCodeName(Workbook, Constants.SheetCodeName.DetailsSetting);
            Excel.Range rar = SettingSheet.get_Range("C:C");
            var obj = rar.Value;
            int rowValue = obj.GetLength(0);
            String[] dictKeys = PopulatedDictionary.Keys.ToArray<String>();

            
            for (int i = 2; i < rowValue; i++)
            {
                if (obj[i, 1] != null)
                {
                    if (PopulatedDictionary.ContainsKey(obj[i, 1])) {
                        QuestionSettings qs = PopulatedDictionary[obj[i, 1]];
                        var ite = _dataExport_LBVariablesToExport.Where(x=>x.QuestionVariable== qs.Variable).ToList();
                        set = true;

                        if (qs.CategoryCount != 0)
                            _dataExport_ListBoxCommonCopy.Add(new DataExport() { QuestionVariable = qs.Variable, QuestionVariableType = qs.AnswerType + "/" + qs.CategoryCount, Question = qs.Question, QuestionIndex = ite[0].QuestionIndex, QuestionChoiceNo = qs.QuestionNumber, Choisces = qs.Choices });
                        else
                            _dataExport_ListBoxCommonCopy.Add(new DataExport() { QuestionVariable = qs.Variable, QuestionVariableType = qs.AnswerType, Question = qs.Question, QuestionIndex = ite[0].QuestionIndex, QuestionChoiceNo = qs.QuestionNumber, Choisces = qs.Choices });

                        //removItem.Add(qs.Variable);
                        _dataExport_LBVariablesToExport.Remove(ite[0]);

                        set = true;
                    }
                }
                else
                {

                    break;
                }
            }

            if (set)
            {
                

                this.LBVariablesToExport.DataContext = _dataExport_LBVariablesToExport;
                this.ListBoxCommonCopy.DataContext = _dataExport_ListBoxCommonCopy;
            }
            else
            {//191
                if (dictKeys.Contains(Constants.QuestionVariableValue.QuestionVariableItem))
                {
                    int loc = 0;
                    for (int i = 0; i < dictKeys.Count<String>(); i++)
                    {
                        if (dictKeys[i] == Constants.QuestionVariableValue.QuestionVariableItem)
                        {
                            loc = i;
                            break;
                        }
                    }
                    _dataExport_ListBoxCommonCopy.Add(_dataExport_LBVariablesToExport.ElementAt(loc));
                    _dataExport_LBVariablesToExport.RemoveAt(loc);
                    this.LBVariablesToExport.DataContext = _dataExport_LBVariablesToExport;
                    this.ListBoxCommonCopy.DataContext = _dataExport_ListBoxCommonCopy;


                }
                else
                {
                    _dataExport_ListBoxCommonCopy.Add(_dataExport_LBVariablesToExport.ElementAt(0));
                    _dataExport_LBVariablesToExport.RemoveAt(0);
                    this.LBVariablesToExport.DataContext = _dataExport_LBVariablesToExport;
                    this.ListBoxCommonCopy.DataContext = _dataExport_ListBoxCommonCopy;
                }
            }

        }

        private void LoadingData()
        {
            PopulatedDictionary = Util.Definiotion.VariableDictionary;
            fs.LoadingData(PopulatedDictionary, dataFromSheet, out _dataExport_LBVariablesToExport, out _qstnvariablDD1, out _qstnvariablDD2, out _qstnvariablDD3,
               out _qstnvariablDD4, out _qstnvariablDD5, out _qstnvariablDD6);

         
            this.LBVariablesToExport.DataContext = _dataExport_LBVariablesToExport;
            this.Combo_Classify_Item.DataContext = _qstnvariablDD1;
            this.Combo_Conditional_Item_1.DataContext = _qstnvariablDD2;
            this.Combo_Conditional_Item_2.DataContext = _qstnvariablDD3;
            this.Combo_Conditional_Item_3.DataContext = _qstnvariablDD4;
            this.Combo_Conditional_Item_4.DataContext = _qstnvariablDD5;
            this.Combo_Conditional_Item_5.DataContext = _qstnvariablDD6;

            Text_Output_Path.Text = System.IO.Path.GetDirectoryName(SelectedFile);
        }


        private void FilterSettingsOnLoadVisisbility()
        {
            this.Check_Refine_Condition.IsChecked = false;
            this.Check_Vertical.IsChecked = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IsInitialLoad = true;

            EnableRadioOnLoad();
            LoadingData();
            FilterSettingsOnLoadVisisbility();            
            ReadSettingsOnLoad();

            ConditionCheck();
			GetExtension();
        }

        #region ArrowButtonClick

        private void ButtonSingleRightArrow_Click(object sender, RoutedEventArgs e)
        {
            List<DataExport> removeFromList = new List<DataExport>();
            foreach (DataExport item in LBVariablesToExport.SelectedItems)
            {
                if (item != null)
                {
                    removeFromList.Add(item);
                }
            }
            var items = removeFromList.OrderBy(x => x.QuestionIndex);
            foreach (DataExport item in items)
            {
                if (item != null)
                {
                    _dataExport_ListBoxCommonCopy.Add(item);
                    _dataExport_LBVariablesToExport.Remove(item);
                }
            }

            this.LBVariablesToExport.DataContext = _dataExport_LBVariablesToExport;
            this.ListBoxCommonCopy.DataContext = _dataExport_ListBoxCommonCopy;

        }

        private void ButtonDblRightArrow_Click(object sender, RoutedEventArgs e)
        {
            _dataExport_LBVariablesToExport.Clear();
            _dataExport_ListBoxCommonCopy.Clear();
            foreach (DataExport item in dataFromSheet)
            {
                if (item != null)
                {
                    _dataExport_ListBoxCommonCopy.Add(item);
                }
            }
            this.ListBoxCommonCopy.DataContext = _dataExport_ListBoxCommonCopy;
            this.LBVariablesToExport.DataContext = _dataExport_LBVariablesToExport;
        }

        private void ButtonSingleLefttArrow_Click(object sender, RoutedEventArgs e)
        {
            List<DataExport> removeFromList = new List<DataExport>();
            List<DataExport> sortList = new List<DataExport>();
            bool setIndex = false;
            bool alreadySet = false;
            int position = 0;
            foreach (DataExport item in _dataExport_LBVariablesToExport)
            {
                if (item != null)
                {
                    sortList.Add(item);
                }
            }


            foreach (DataExport item in ListBoxCommonCopy.SelectedItems)
            {
                if (item != null && item.QuestionVariable != Util.Constants.QuestionVariableValue.QuestionVariableItem)
                {
                    removeFromList.Add(item);
                    if (_dataExport_LBVariablesToExport.Count == 0)
                    {
                        sortList.Insert(0, item);
                    }
                    else if (_dataExport_LBVariablesToExport.Count < item.QuestionIndex + 1)
                    {
                        int i = 0;
                        foreach (var items in _dataExport_LBVariablesToExport)
                        {
                            if (items.QuestionIndex > item.QuestionIndex)
                            {
                                if (!setIndex)
                                {
                                    sortList.Insert(0, item);
                                    alreadySet = true;
                                    break;
                                }
                                else
                                {
                                    sortList.Insert(position, item);
                                    break;
                                }
                            }
                            else
                            {
                                setIndex = true;
                                position = i + 1;
                            }
                            if (i == _dataExport_LBVariablesToExport.Count - 1 && !alreadySet)
                            {
                                sortList.Insert(position, item);
                            }
                            ++i;
                        }
                        setIndex = false;
                        alreadySet = false;
                    }
                    else
                    {
                        sortList.Insert(item.QuestionIndex, item);
                    }
                }
            }
            foreach (DataExport item in removeFromList)
            {
                if (item != null)
                {
                    _dataExport_ListBoxCommonCopy.Remove(item);
                }
            }
            _dataExport_LBVariablesToExport.Clear();
            sortList=sortList.OrderBy(x => x.QuestionIndex).ToList();
            foreach (DataExport item in sortList)
            {
                if (item != null)
                {
                    _dataExport_LBVariablesToExport.Add(item);
                }
            }

            this.LBVariablesToExport.DataContext = _dataExport_LBVariablesToExport;
            this.ListBoxCommonCopy.DataContext = _dataExport_ListBoxCommonCopy;
        }

        private void ButtonDblLeftArrow_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxCommonCopy.Items.Count <= 0)
            {
                return;
            }
            _dataExport_ListBoxCommonCopy.Clear();
            _dataExport_LBVariablesToExport.Clear();
            List<DataExport> removeFromList = new List<DataExport>();
            foreach (DataExport item in dataFromSheet.Skip(1))//-----------------------191
            {
                if (item != null)
                {
                    _dataExport_LBVariablesToExport.Add(item);
                }
            }
            _dataExport_ListBoxCommonCopy.Add(dataFromSheet.First());//-----------------------191

            this.LBVariablesToExport.DataContext = _dataExport_LBVariablesToExport.OrderBy(x=>x.QuestionIndex);

        }
        #endregion

        #region ExportSettingVisibilityOnChoice

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedFileFormat = e.AddedItems.Count>0? (e.AddedItems[0] as ComboBoxItem).Content as string: LocalResource.DO_OUTPUT_FORMAT_EXCEL;

            SetControls(SelectedFileFormat);
		}

        private void SetControls(string selectedFileFormat,string initialNoAnsVal="", string initialNoAnsValForSpss = "",string cBMAVLVLForSpss=""
            , string outputType = "")
        {
            if(SelectedFileFormat == LocalResource.DO_OUTPUT_FORMAT_QC3
               || SelectedFileFormat == LocalResource.DO_OUTPUT_FORMAT_QC4 
               || SelectedFileFormat == LocalResource.DO_OUTPUT_FORMAT_QLAYOUT
               || SelectedFileFormat == LocalResource.DO_OUTPUT_FORMAT_R2D3)
            {
                Combo_Output_Type.Foreground = new SolidColorBrush(Colors.DimGray);
            }
            else
            {

                Combo_Output_Type.Foreground = new SolidColorBrush(Colors.Black);
            }
            if (SelectedFileFormat == LocalResource.DO_OUTPUT_FORMAT_EXCEL
               || SelectedFileFormat == LocalResource.DO_OUTPUT_FORMAT_QC3
               || SelectedFileFormat == LocalResource.DO_OUTPUT_FORMAT_QC4
               || SelectedFileFormat == LocalResource.DO_OUTPUT_FORMAT_CSV
               || SelectedFileFormat == LocalResource.DO_OUTPUT_FORMAT_TAB
               || SelectedFileFormat == LocalResource.DO_OUTPUT_FORMAT_QLAYOUT
               || SelectedFileFormat == LocalResource.DO_OUTPUT_FORMAT_R2D3)
            {
                bool enableFlag = true;
                bool UTF_Flag = false;
                if (SelectedFileFormat == LocalResource.DO_OUTPUT_FORMAT_QC4
                    || SelectedFileFormat == LocalResource.DO_OUTPUT_FORMAT_QC3
                    || SelectedFileFormat == LocalResource.DO_OUTPUT_FORMAT_QLAYOUT
                    || SelectedFileFormat == LocalResource.DO_OUTPUT_FORMAT_R2D3)
                {
                    enableFlag = false;

                }

                if (SelectedFileFormat == LocalResource.DO_OUTPUT_FORMAT_CSV
                   || SelectedFileFormat == LocalResource.DO_OUTPUT_FORMAT_TAB
                   || SelectedFileFormat == LocalResource.DO_OUTPUT_FORMAT_QLAYOUT
                   || SelectedFileFormat == LocalResource.DO_OUTPUT_FORMAT_R2D3)
                {
                    UTF_Flag = true;
                }

                Combo_NonAnser.IsEnabled = enableFlag;
                Combo_Output_Type.IsEnabled = enableFlag;
                Combo_NonApplying.IsEnabled = enableFlag;

                Check_Vertical.IsEnabled = enableFlag;
                
                Check_Vertical.Visibility = Visibility.Visible;
                Combo_Output_Type.Visibility = Visibility.Visible;
                TBMAVariables.Visibility = Visibility.Hidden;
                Option_Direct.Visibility = Visibility.Hidden;
                Option_Off_Zero.Visibility = Visibility.Hidden;
                Option_Off_DK.Visibility = Visibility.Hidden;
                MDNumbers.Visibility = Visibility.Hidden;
                Option_Zero.Visibility = Visibility.Hidden;
                Option_DK.Visibility = Visibility.Hidden;
                TBET.Visibility = Visibility.Visible;
                LABEL_IFNA_SPSS.Visibility = Visibility.Hidden;
                LABEL_CHARACTER_FOR_EXCLUDED_SPSS.Visibility = Visibility.Hidden;
                TBIndicatorForNoQuestionVariableSPSSSAMA.Visibility = Visibility.Hidden;
                CBIndicatorForNoQuestionVariableSPSSSAMA.Visibility = Visibility.Hidden;
                UNICODE.Visibility = Visibility.Hidden;
                UTF.Visibility = UTF_Flag ? Visibility.Visible : Visibility.Hidden;
                CBMAVLVL.Visibility = Visibility.Hidden;
                LABEL_IndicatorFNA.Visibility = Visibility.Visible;
                OPMAChoiceSPSS.Visibility = Visibility.Hidden;
                LABEL_CFE.Visibility = Visibility.Visible;
                LABEL_IndicatorFNA.Visibility = Visibility.Visible;
                Combo_NonAnser.Visibility = Visibility.Visible;

                if (outputType != "")
                    Combo_Output_Type.Text = outputType;
                else
                    Combo_Output_Type.SelectedIndex = 0;

                if (initialNoAnsVal != "")
                    Combo_NonAnser.Text = initialNoAnsVal;
                else
                    Combo_NonAnser.SelectedIndex = 0;

                Combo_NonApplying.Visibility = Visibility.Visible;
                if (Combo_NonApplying.IsEnabled == true)
                    Combo_NonApplying.Foreground = new SolidColorBrush(Colors.Black);
                else Combo_NonApplying.Foreground = new SolidColorBrush(Colors.DimGray);
                if (Combo_NonAnser.IsEnabled == true)
                    Combo_NonAnser.Foreground = new SolidColorBrush(Colors.Black);
                else Combo_NonAnser.Foreground = new SolidColorBrush(Colors.DimGray);
            }
            else if (SelectedFileFormat == LocalResource.DO_OUTPUT_FORMAT_SPSS)
            {
                //Check_Split.Visibility = Visibility.Hidden;
                Check_Vertical.Visibility = Visibility.Hidden;
                TBET.Visibility = Visibility.Hidden;
                Combo_Output_Type.Visibility = Visibility.Hidden;
                TBMAVariables.Visibility = Visibility.Hidden;
                Option_Direct.Visibility = Visibility.Hidden;
                Option_Off_Zero.Visibility = Visibility.Hidden;
                Option_Off_DK.Visibility = Visibility.Hidden;
                MDNumbers.Visibility = Visibility.Hidden;
                Option_Zero.Visibility = Visibility.Hidden;
                Option_DK.Visibility = Visibility.Hidden;
                //CBIndicatorForNoAnswerSPSSSAMA.Visibility = Visibility.Visible;
                LABEL_CHARACTER_FOR_EXCLUDED_SPSS.Visibility = Visibility.Visible;
                //TBIndicatorForNoQuestionVariableSPSSSAMA.Visibility= Visibility.Visible;
                CBIndicatorForNoQuestionVariableSPSSSAMA.Visibility = Visibility.Visible;
                LABEL_IFNA_SPSS.Visibility = Visibility.Visible;
                //CBIndicatorForNoAnswerSPSSSAMA.Visibility = Visibility.Hidden;
                UNICODE.Visibility = Visibility.Visible;
                UTF.Visibility = Visibility.Hidden;
                CBMAVLVL.Visibility = Visibility.Visible;
                LABEL_CFE.Visibility = Visibility.Hidden;
                OPMAChoiceSPSS.Visibility = Visibility.Visible;
                //Text_Change_File.Visibility = Visibility.Visible;
                //Text_Change_File.IsEnabled = false;
                LABEL_IndicatorFNA.Visibility = Visibility.Hidden;
                Combo_NonAnser.Visibility = Visibility.Hidden;
                Combo_NonApplying.Visibility = Visibility.Visible;
                Combo_NonApplying.IsEnabled = false;
                Combo_NonApplying.SelectedIndex = 0;
                if (cBMAVLVLForSpss != "")
                    CBMAVLVL.Text = cBMAVLVLForSpss;
                else
                    CBMAVLVL.SelectedIndex = 0;

                if (initialNoAnsValForSpss != "")
                    CBIndicatorForNoQuestionVariableSPSSSAMA.Text = initialNoAnsValForSpss;
                else
                    CBIndicatorForNoQuestionVariableSPSSSAMA.SelectedIndex = 0;

                Combo_NonApplying.Foreground = new SolidColorBrush(Colors.DimGray);
                //Combo_NonAnser.Foreground = new SolidColorBrush(Colors.DimGray);
            }

            //else if (SelectedFileFormat == "Qlayout/Qrawdata")
            //{
            //	Combo_NonAnser.IsEnabled = true;
            //	//Check_Split.Visibility = Visibility.Visible;
            //	Check_Vertical.Visibility = Visibility.Hidden;
            //	//Check_Split.IsEnabled = true;
            //	//Check_Vertical.IsEnabled = false;				
            //	Combo_Output_Type.IsEnabled = false;
            //	Combo_Output_Type.Visibility = Visibility.Visible;
            //	TBMAVariables.Visibility = Visibility.Hidden;
            //	Option_Direct.Visibility = Visibility.Hidden;
            //	Option_Off_Zero.Visibility = Visibility.Hidden;
            //	Option_Off_DK.Visibility = Visibility.Hidden;
            //	MDNumbers.Visibility = Visibility.Hidden;
            //	Option_Zero.Visibility = Visibility.Hidden;
            //	Option_DK.Visibility = Visibility.Hidden;
            //	TBET.Visibility = Visibility.Visible;
            //	LABEL_IFNA_SPSS.Visibility = Visibility.Hidden;
            //	// CBIndicatorForExcludedSPSS.Visibility = Visibility.Hidden;
            //	LABEL_CHARACTER_FOR_EXCLUDED_SPSS.Visibility = Visibility.Hidden;
            //	TBIndicatorForNoQuestionVariableSPSSSAMA.Visibility = Visibility.Hidden;
            //	CBIndicatorForNoQuestionVariableSPSSSAMA.Visibility = Visibility.Hidden;
            //	UNICODE.Visibility = Visibility.Hidden;
            //	CBMAVLVL.Visibility = Visibility.Hidden;
            //	LABEL_IndicatorFNA.Visibility = Visibility.Visible;
            //	LABEL_CFE.Visibility = Visibility.Visible;
            //	OPMAChoiceSPSS.Visibility = Visibility.Hidden;
            //	//Text_Change_File.Visibility = Visibility.Visible;
            //	//Text_Change_File.IsEnabled = true;
            //	LABEL_IndicatorFNA.Visibility = Visibility.Visible;
            //	Combo_NonAnser.Visibility = Visibility.Visible;
            //	Combo_NonAnser.IsEnabled = false;
            //	Combo_NonAnser.SelectedIndex = 0;
            //	CBIndicatorForNoAnswerSPSSSAMA.Visibility = Visibility.Hidden;
            //	Combo_NonApplying.Visibility = Visibility.Visible;
            //	Combo_NonApplying.IsEnabled = false;
            //	Combo_NonApplying.SelectedIndex = 1;
            //}


            //for extesion settings
            GetExtension();
        }
        #endregion

        private void GetExtension()
		{
			if (SelectedFileFormat == LocalResource.DO_OUTPUT_FORMAT_EXCEL)
			{
				filesextension = "Excel file(*.xls;*.xlsx)| *.xls;*.xlsx";
			}
			if (SelectedFileFormat == LocalResource.DO_OUTPUT_FORMAT_QC3)
			{
				filesextension = "Qc3 file(*.qc3x)| *.qc3x";
			}
			if (SelectedFileFormat == LocalResource.DO_OUTPUT_FORMAT_QC4)
			{
				filesextension = "Qc4 file(*.qc4)| *.qc4";
			}
			if (SelectedFileFormat == LocalResource.DO_OUTPUT_FORMAT_CSV)
			{
				filesextension = "Csv file(*.csv )| *.csv";
			}
			if (SelectedFileFormat == LocalResource.DO_OUTPUT_FORMAT_TAB)
			{
				filesextension = "Text file(*.txt )| *.txt";
			}
			if (SelectedFileFormat == LocalResource.DO_OUTPUT_FORMAT_QLAYOUT)
			{
				filesextension = "All files(*.*)| *.*";
			}
			else if (SelectedFileFormat == LocalResource.DO_OUTPUT_FORMAT_SPSS)
			{
				filesextension = "All files(*.*)| *.*";
			}
            if (SelectedFileFormat == LocalResource.DO_OUTPUT_FORMAT_R2D3)
            {
                filesextension = "Csv file(*.csv )| *.csv";
            }
        }
        #region RBOR

        private void Option_Conditional_Or_1_Checked(object sender, RoutedEventArgs e)
        {
            Combo_Conditional_Item_2.IsEnabled = true;
            if (Combo_Conditional_Item_2.SelectedIndex < 1)
            {
                if (!IsInitialLoad)
                {
                    if (_qstnvariablDD3[0].QuestionVariable == "" || _qstnvariablDD3[0].QuestionVariable == null)
                        Combo_Conditional_Item_2.SelectedIndex = 1;
                    else
                        Combo_Conditional_Item_2.SelectedIndex = 0;
                }
            }
            if(!IsInitialLoad)
            {
                if (_qstnvariablDD2[0].QuestionVariable == "" || _qstnvariablDD2[0].QuestionVariable == null)
                    _qstnvariablDD2.RemoveAt(0);
            }
        }

        private void Option_Conditional_Or_2_Checked(object sender, RoutedEventArgs e)
        {
            Combo_Conditional_Item_3.IsEnabled = true;
            if (Combo_Conditional_Item_3.SelectedIndex < 1)
            {
                if (!IsInitialLoad)
                {
                    if (_qstnvariablDD4[0].QuestionVariable == "" || _qstnvariablDD4[0].QuestionVariable == null)
                        Combo_Conditional_Item_3.SelectedIndex = 1;
                    else
                        Combo_Conditional_Item_3.SelectedIndex = 0;
                }
            }
            if(!IsInitialLoad)
            {
                if (_qstnvariablDD3[0].QuestionVariable == "" || _qstnvariablDD3[0].QuestionVariable == null)
                    _qstnvariablDD3.RemoveAt(0);
            }
        }

        private void Option_Conditional_Or_3_Checked(object sender, RoutedEventArgs e)
        {
            Combo_Conditional_Item_4.IsEnabled = true;
            if (Combo_Conditional_Item_4.SelectedIndex < 1)
            {
                if (!IsInitialLoad)
                {
                    if (_qstnvariablDD5[0].QuestionVariable == "" || _qstnvariablDD5[0].QuestionVariable == null)
                        Combo_Conditional_Item_4.SelectedIndex = 1;
                    else
                        Combo_Conditional_Item_4.SelectedIndex = 0;
                }
            }
            if(!IsInitialLoad)
            {
                if (_qstnvariablDD4[0].QuestionVariable == "" || _qstnvariablDD4[0].QuestionVariable == null)
                    _qstnvariablDD4.RemoveAt(0);
            }
        }

        private void Option_Conditional_Or_4_Checked(object sender, RoutedEventArgs e)
        {
            Combo_Conditional_Item_5.IsEnabled = true;
            if (Combo_Conditional_Item_5.SelectedIndex < 1)
            {
                if (!IsInitialLoad)
                {
                    Combo_Conditional_Item_5.SelectedIndex = 1;
                }
            }
            if (!IsInitialLoad)
            {
                if (_qstnvariablDD5[0].QuestionVariable == "" || _qstnvariablDD5[0].QuestionVariable == null)
                    _qstnvariablDD5.RemoveAt(0);
            }
        }

        #endregion

        #region RBAND

        private void Option_Conditional_And_1_Checked(object sender, RoutedEventArgs e)
        {
            Combo_Conditional_Item_2.IsEnabled = true;
            if (Combo_Conditional_Item_2.SelectedIndex < 1)
            {
                if (!IsInitialLoad)
                {
                    if (_qstnvariablDD3[0].QuestionVariable == "" || _qstnvariablDD3[0].QuestionVariable == null)
                        Combo_Conditional_Item_2.SelectedIndex = 1;
                    else
                        Combo_Conditional_Item_2.SelectedIndex = 0;

                }
            }
            if(!IsInitialLoad)
            {
                if (_qstnvariablDD2[0].QuestionVariable == "" || _qstnvariablDD2[0].QuestionVariable == null)
                    _qstnvariablDD2.RemoveAt(0);
            }
        }

        private void Option_Conditional_And_2_Checked(object sender, RoutedEventArgs e)
        {
            Combo_Conditional_Item_3.IsEnabled = true;
            if (Combo_Conditional_Item_3.SelectedIndex < 1)
            {
                if (!IsInitialLoad)
                {
                    if (_qstnvariablDD4[0].QuestionVariable == "" || _qstnvariablDD4[0].QuestionVariable == null)
                        Combo_Conditional_Item_3.SelectedIndex = 1;
                    else
                        Combo_Conditional_Item_3.SelectedIndex = 0;
                }
            }
            if (!IsInitialLoad)
            {
                if (_qstnvariablDD3[0].QuestionVariable == "" || _qstnvariablDD3[0].QuestionVariable == null)
                    _qstnvariablDD3.RemoveAt(0);
            }
        }

        private void Option_Conditional_And_3_Checked(object sender, RoutedEventArgs e)
        {
            Combo_Conditional_Item_4.IsEnabled = true;
            if (Combo_Conditional_Item_4.SelectedIndex < 1)
            {
                if (!IsInitialLoad)
                {
                    if (_qstnvariablDD5[0].QuestionVariable == "" || _qstnvariablDD5[0].QuestionVariable == null)
                        Combo_Conditional_Item_4.SelectedIndex = 1;
                    else
                        Combo_Conditional_Item_4.SelectedIndex = 0;
                }
            }
            if (!IsInitialLoad)
            {
                if (_qstnvariablDD4[0].QuestionVariable == "" || _qstnvariablDD4[0].QuestionVariable == null)
                    _qstnvariablDD4.RemoveAt(0);
            }
        }

        private void Option_Conditional_And_4_Checked(object sender, RoutedEventArgs e)
        {
            Combo_Conditional_Item_5.IsEnabled = true;
            if (Combo_Conditional_Item_5.SelectedIndex < 1)
            {
                if (!IsInitialLoad)
                {
                    Combo_Conditional_Item_5.SelectedIndex = 1;
                }
            }
            if(!IsInitialLoad)
            {
                if (_qstnvariablDD5[0].QuestionVariable == "" || _qstnvariablDD5[0].QuestionVariable == null)
                    _qstnvariablDD5.RemoveAt(0);
            }
        }

        #endregion

        #region ButtonFilter

        private string ChoiceSelection(List<String> CBcvChoices)
        {
            String selectedChoice = "";
            if (Combo_Conditional_Item_1selectedQuestionVariableType != Constants.AnswerType.FA)
            {
                FilterSettingValue popUp = new FilterSettingValue(CBcvChoices, LocalResource.TEXT_FILTER_VALUE_DK_NO_ANSWER, LocalResource.TEXT_FILTER_VALUE_STAR_EXCLUDED);
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
            return selectedChoice;
        }

        private void BTnFilter1_Click(object sender, RoutedEventArgs e)
        {
            string value = ChoiceSelection(Combo_Conditional_Item_1Choices);
            if (value != null && value != "")
                Combo_Conditional_Value_1.Text = value;
        }

        private void BTnFilter2_Click(object sender, RoutedEventArgs e)
        {
            string value = ChoiceSelection(Combo_Conditional_Item_2Choices);
            if (value != null && value != "")
                Combo_Conditional_Value_2.Text = value;

        }

        private void BTnFilter3_Click(object sender, RoutedEventArgs e)
        {
            string value = ChoiceSelection(Combo_Conditional_Item_3Choices);
            if (value != null && value != "")
                Combo_Conditional_Value_3.Text = value;
        }


        private void BTnFilter4_Click(object sender, RoutedEventArgs e)
        {
            string value = ChoiceSelection(Combo_Conditional_Item_4Choices);
            if (value != null && value != "")
                Combo_Conditional_Value_4.Text = value;
        }

        private void BTnFilter5_Click(object sender, RoutedEventArgs e)
        {
            string value = ChoiceSelection(Combo_Conditional_Item_5Choices);
            if (value != null && value != "")
                Combo_Conditional_Value_5.Text = value;
        }
        #endregion

        #region CBOValueSelectionChanged

        private void CBOValue2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Option_Conditional_And_2.IsEnabled = true;
            Option_Conditional_Or_2.IsEnabled = true;
            BTnFilter2.IsEnabled = true;
        }

        private void CBOValue3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BTnFilter3.IsEnabled = true;
            Option_Conditional_And_3.IsEnabled = true;
            Option_Conditional_Or_3.IsEnabled = true;
        }

        private void CBOValue4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BTnFilter4.IsEnabled = true;
            Option_Conditional_And_4.IsEnabled = true;
            Option_Conditional_Or_4.IsEnabled = true;
        }

        private void CBOValue5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BTnFilter5.IsEnabled = true;
        }

        #endregion

        #region CBOperatorSelectionChanged

        private void Combo_Conditional_Operator_1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Check_Refine_Condition.IsChecked==true&& Combo_Conditional_Operator_1.SelectedIndex >= 0)
            {
                Combo_Conditional_Value_1.IsEnabled = true;
                BTnFilter1.IsEnabled = true;
            }
        }

        private void Combo_Conditional_Operator_2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Check_Refine_Condition.IsChecked == true && Combo_Conditional_Operator_2.SelectedIndex >= 0)
            {
                Combo_Conditional_Value_2.IsEnabled = true;
                BTnFilter2.IsEnabled = true;
            }
        }

        private void Combo_Conditional_Operator_3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Check_Refine_Condition.IsChecked == true && Combo_Conditional_Operator_3.SelectedIndex >= 0)
            {
                Combo_Conditional_Value_3.IsEnabled = true;
                BTnFilter3.IsEnabled = true;
            }
        }

        private void Combo_Conditional_Operator_4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Check_Refine_Condition.IsChecked == true && Combo_Conditional_Operator_4.SelectedIndex >= 0)
            {
                Combo_Conditional_Value_4.IsEnabled = true;
                BTnFilter4.IsEnabled = true;
            }
        }

        private void Combo_Conditional_Operator_5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Check_Refine_Condition.IsChecked==true&&Combo_Conditional_Operator_5.SelectedIndex >= 0)
            {
                Combo_Conditional_Value_5.IsEnabled = true;
                BTnFilter5.IsEnabled = true;
            }
        }

        #endregion

        #region CBCVSelections

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

        private void OperatorLoading(String QuestionVariableType, System.Windows.Controls.ComboBox CBOperator, System.Windows.Controls.ComboBox Conditional)
        {
            string[] CBcvselectedQuestionVariableType = QuestionVariableType.Split(new Char[] { '/' });
            Combo_Conditional_Item_1selectedQuestionVariableType = CBcvselectedQuestionVariableType[0].ToString();
            if (CBcvselectedQuestionVariableType[0] == Constants.AnswerType.SA || CBcvselectedQuestionVariableType[0] == Constants.AnswerType.N)
            {
                CBOperator.Items.Clear();
                //CBOperator.Items.Add(LocalResource.GTT_FS_CB_ITEM_VALUE_EQUALS);
                //CBOperator.Items.Add(LocalResource.GTT_FS_CB_ITEM_VALUE_LESS_OR_GREATER);
                //CBOperator.Items.Add(LocalResource.GTT_FS_CB_ITEM_VALUE_LESS_THAN);
                //CBOperator.Items.Add(LocalResource.GTT_FS_CB_ITEM_VALUE_GREATER_THAN);
                //CBOperator.Items.Add(LocalResource.GTT_FS_CB_ITEM_VALUE_LESS_THAN_OR_EQUAL_TO);
                //CBOperator.Items.Add(LocalResource.GTT_FS_CB_ITEM_VALUE_GREATER_THAN_OR_EQUAL_TO);

                CBOperator.Items.Add("=");
                CBOperator.Items.Add("<>");
                CBOperator.Items.Add("<");
                CBOperator.Items.Add(">");
                CBOperator.Items.Add("<=");
                CBOperator.Items.Add(">=");
            }
            else if (CBcvselectedQuestionVariableType[0] == Constants.AnswerType.FA || CBcvselectedQuestionVariableType[0] == Constants.AnswerType.MA)
            {
                CBOperator.Items.Clear();
                CBOperator.Items.Add(LocalResource.GTT_FS_CB_ITEM_VALUE_EQUALS);
                CBOperator.Items.Add(LocalResource.GTT_FS_CB_ITEM_VALUE_LESS_OR_GREATER);
            }
            CBOperator.IsEnabled = Conditional.IsEnabled;
        }

        private void Combo_Conditional_Item_1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedindex = Combo_Conditional_Item_1.SelectedIndex;
            if ((selectedindex > 0 && selectedindex != LastSelected) || (selectedindex==0&& _qstnvariablDD2[selectedindex].QuestionVariable!= "" && LastSelectedText == ""))
            {
                Combo_Conditional_Item_1selectedQuestionVariableType = _qstnvariablDD2[selectedindex].QuestionVariableType;
                Combo_Conditional_Item_1Choices = _qstnvariablDD2[selectedindex].Choisces;
                if (Combo_Conditional_Item_1selectedQuestionVariableType != null)
                {
                    OperatorLoading(Combo_Conditional_Item_1selectedQuestionVariableType, Combo_Conditional_Operator_1, Combo_Conditional_Item_1);
                }
                if (!IsInitialLoad)
                {
                    Combo_Conditional_Operator_1.IsEnabled = true;
                    Combo_Conditional_Operator_1.SelectedIndex = -1;
                    Combo_Conditional_Value_1.Text = "";
                    Combo_Conditional_Value_1.IsEnabled = false;
                    BTnFilter1.IsEnabled = false;
                    //Option_Conditional_And_1.IsChecked = false;
                    //Option_Conditional_Or_1.IsChecked = false;
                    Option_Conditional_And_1.IsEnabled = false;
                    Option_Conditional_Or_1.IsEnabled = false;
                }

            }
            else
            {
                if (LastSelected == 0 && LastSelectedText=="")
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
                }
            }
            if (IsInitialLoad)
            {
                if (CmbItm2 > 0)
                    _qstnvariablDD2.RemoveAt(0);
                if (this.Check_Refine_Condition.IsChecked == false)
                    Combo_Conditional_Item_1.IsEnabled = false;
                //Check_Refine_Condition_Checked(null, null);
            }
            Combo_Conditional_Item_1.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void Combo_Conditional_Item_2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedindex = Combo_Conditional_Item_2.SelectedIndex;
            if (selectedindex > 0 && selectedindex != LastSelected || (selectedindex == 0 && _qstnvariablDD3[selectedindex].QuestionVariableType != "" && LastSelectedText == ""))
            {
                Combo_Conditional_Item_2selectedQuestionVariableType = _qstnvariablDD3[selectedindex].QuestionVariableType;
                Combo_Conditional_Item_2Choices = _qstnvariablDD3[selectedindex].Choisces;
                if (Combo_Conditional_Item_2selectedQuestionVariableType != null)
                {
                    OperatorLoading(Combo_Conditional_Item_2selectedQuestionVariableType, Combo_Conditional_Operator_2, Combo_Conditional_Item_2);
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
                    if (selectedindex != -1 && _qstnvariablDD2[selectedindex].QuestionVariable != "")
                        _qstnvariablDD2.Insert(0, DataExportObjectCreator());
                }

            }
            if (IsInitialLoad && CmbItm3 > 0)
                _qstnvariablDD3.RemoveAt(0);
            Combo_Conditional_Item_2.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void Combo_Conditional_Item_3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedindex = Combo_Conditional_Item_3.SelectedIndex;
            if (selectedindex > 0 && selectedindex != LastSelected || (selectedindex == 0 && _qstnvariablDD4[selectedindex].QuestionVariableType != "" && LastSelectedText == ""))
            {
                Combo_Conditional_Item_3selectedQuestionVariableType = _qstnvariablDD4[selectedindex].QuestionVariableType;
                Combo_Conditional_Item_3Choices = _qstnvariablDD4[selectedindex].Choisces;
                if (Combo_Conditional_Item_3selectedQuestionVariableType != null)
                {
                    OperatorLoading(Combo_Conditional_Item_3selectedQuestionVariableType, Combo_Conditional_Operator_3, Combo_Conditional_Item_3);
                }
                //Combo_Conditional_Operator_3.IsEnabled = true;
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
                    if (selectedindex != -1 && _qstnvariablDD3[selectedindex].QuestionVariable != "")
                        _qstnvariablDD3.Insert(0, DataExportObjectCreator());
                }
            }

            if (IsInitialLoad && CmbItm4 > 0)
                _qstnvariablDD4.RemoveAt(0);
            Combo_Conditional_Item_3.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void Combo_Conditional_Item_4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedindex = Combo_Conditional_Item_4.SelectedIndex;
            if (selectedindex > 0 && selectedindex != LastSelected || (selectedindex == 0 && _qstnvariablDD5[selectedindex].QuestionVariableType != "" && LastSelectedText == ""))
            {
                Combo_Conditional_Item_4selectedQuestionVariableType = _qstnvariablDD5[selectedindex].QuestionVariableType;
                Combo_Conditional_Item_4Choices = _qstnvariablDD5[selectedindex].Choisces;
                if (Combo_Conditional_Item_4selectedQuestionVariableType != null)
                {
                    OperatorLoading(Combo_Conditional_Item_4selectedQuestionVariableType, Combo_Conditional_Operator_4, Combo_Conditional_Item_4);
                }
                //Combo_Conditional_Operator_4.IsEnabled = true;
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
                    if (selectedindex != -1 && _qstnvariablDD4[selectedindex].QuestionVariable != "")
                        _qstnvariablDD4.Insert(0, DataExportObjectCreator());
                }
            }
            if (IsInitialLoad && CmbItm5 > 0)
                _qstnvariablDD5.RemoveAt(0);
            Combo_Conditional_Item_4.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void Combo_Conditional_Item_5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedindex = Combo_Conditional_Item_5.SelectedIndex;
            if (selectedindex > 0 && selectedindex != LastSelected)
            {
                Combo_Conditional_Item_5selectedQuestionVariableType = _qstnvariablDD6[selectedindex].QuestionVariableType;
                Combo_Conditional_Item_5Choices = _qstnvariablDD6[selectedindex].Choisces;
                if (Combo_Conditional_Item_4selectedQuestionVariableType != null)
                {
                    OperatorLoading(Combo_Conditional_Item_5selectedQuestionVariableType, Combo_Conditional_Operator_5, Combo_Conditional_Item_5);
                }
                //Combo_Conditional_Operator_5.IsEnabled = true;
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
                    if (selectedindex != -1 && _qstnvariablDD5[selectedindex].QuestionVariable != "")
                        _qstnvariablDD5.Insert(0, DataExportObjectCreator());
                }
            }
            if (IsInitialLoad)
                IsInitialLoad = false;
            Combo_Conditional_Item_5.Foreground = new SolidColorBrush(Colors.Black);
        }
        #endregion

        #region ReadSeittingsonLoad

        public void GetName(System.Windows.Controls.Control x)
        {
            if (x is System.Windows.Controls.ComboBox)
                controlObj.Add(Name = "F_Do_Output_" + ((System.Windows.Controls.ComboBox)x).Name + "_", ((System.Windows.Controls.ComboBox)x));

            else if (x is System.Windows.Controls.TextBox)
                controlObj.Add(Name = "F_Do_Output_" + ((System.Windows.Controls.TextBox)x).Name + "_", ((System.Windows.Controls.TextBox)x));

            else if (x is System.Windows.Controls.RadioButton)
                controlObj.Add(Name = "F_Do_Output_" + ((System.Windows.Controls.RadioButton)x).Name + "_", ((System.Windows.Controls.RadioButton)x));

            else if (x is System.Windows.Controls.CheckBox)
                controlObj.Add(Name = "F_Do_Output_" + ((System.Windows.Controls.CheckBox)x).Name + "_", ((System.Windows.Controls.CheckBox)x));
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
            catch(Exception ex) { _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException); }
        }

        private void ReadSettingsOnLoad()
        {
            try
            {
                //----------------------------------------------------------------LOADING THE LEFT AND RIGHT LISTBOX

                ListLoading();

                //--------------------------------------------------------------------GETTING ALL THE KEYS OF CONTROLS 

                foreach (System.Windows.Controls.ComboBox tb
                     in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this))
                {
                    GetName(tb);
                }
                foreach (System.Windows.Controls.RadioButton tb
                    in FindControls.FindLogicalChildren<System.Windows.Controls.RadioButton>(this))
                {
                    GetName(tb);
                }

                foreach (System.Windows.Controls.TextBox tb
                    in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this))
                {
                    GetName(tb);
                }
                foreach (System.Windows.Controls.CheckBox tb
                    in FindControls.FindLogicalChildren<System.Windows.Controls.CheckBox>(this))
                {
                    GetName(tb);
                }
                // ------------------------------------------------------------------------------READING KEYS FROM SHEET

                var SettingSheet = Util.ExcelUtil.GetWorkSheetByCodeName(Workbook, Constants.SheetCodeName.DetailsSetting);
                rarCom = SettingSheet.get_Range("A1", "C1000");
                var obj = rarCom.Value;
                int rowvalue = /*obj.GetLength(0)*/1000;

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
                /* target = _qstnvariablDD2.Where(z => z.QuestionVariable == val).FirstOrDefault();
                                    index = target == null ? -1 : _qstnvariablDD2.IndexOf(target);*/
                //----------------------------------------------------------------------------READING THE VALUE FROM SHEET AND ASSIGNING TO THE CONTROLS
                //bool lastItem = false;
                DataExport target = null; string val = "";
                bool ddlSaveOrNot = false;
                string NoAnswerVal = "";
                string NoAnswerValForSpss = "";
                string CBMAVLVLForSpss = "";
                string OutputType = "";
                bool isComboSet = false;
                for (int i = 0; i < elementsInSheet.Count; i++)
                {
                    if (controlObj.ContainsKey(elementsInSheet[i]))
                    {
                        if (controlObj[elementsInSheet[i]] is System.Windows.Controls.ComboBox)
                        {
                            isComboSet = true;
                            val = null == obj[i + 2, 2] ? "" : obj[i + 2, 2].ToString();
                            int index = Combo_conditional.FindIndex(a => a.QuestionVariable.Equals(obj[i + 2, 2]));

                            if (((System.Windows.Controls.ComboBox)controlObj[elementsInSheet[i]]).Name == "Combo_Classify_Item")
                            {
                                target = _qstnvariablDD1.Where(z => z.QuestionVariable == val).FirstOrDefault();
                                index = target == null ? 0 : _qstnvariablDD1.IndexOf(target);
                                //index = Combo_Classify.FindIndex(a => a.QuestionVariable.Equals(obj[i + 2, 2]));//original code of sreehari
                                Combo_Classify_Item.SelectedIndex = index;
                                Combo_Classify_Item.IsEnabled = true;
                                CmbClsy = index;
                            }
                            else if (((System.Windows.Controls.ComboBox)controlObj[elementsInSheet[i]]).Name == "Combo_Conditional_Item_5")
                            {
                                target = _qstnvariablDD6.Where(z => z.QuestionVariable == val).FirstOrDefault();
                                index = target == null ? 0 : _qstnvariablDD6.IndexOf(target);
                                CmbItm5 = index;
                                if (index == 0)
                                {
                                    Combo_Conditional_Item_5.IsEnabled = false;
                                    Combo_Conditional_Item_5.SelectedValue = 0;
                                }
                                if (index > 0)
                                {
                                    Combo_Conditional_Item_5.SelectedValue = _qstnvariablDD6[index];
                                    Combo_Conditional_Item_5.IsEnabled = true;
                                    Combo_Conditional_Item_5.Foreground = new SolidColorBrush(Colors.Black);
                                }

                            }
                            else if (((System.Windows.Controls.ComboBox)controlObj[elementsInSheet[i]]).Name == "Combo_Conditional_Item_4")
                            {
                                target = _qstnvariablDD5.Where(z => z.QuestionVariable == val).FirstOrDefault();
                                index = target == null ? 0 : _qstnvariablDD5.IndexOf(target);
                                CmbItm4 = index;
                                if (index == 0)
                                {
                                    Combo_Conditional_Item_4.IsEnabled = false;
                                    Combo_Conditional_Item_4.SelectedValue = 0;
                                }
                                if (index > 0)
                                {
                                    Combo_Conditional_Item_4.SelectedValue = _qstnvariablDD5[index];
                                    Combo_Conditional_Item_4.IsEnabled = true;
                                    Combo_Conditional_Item_4.Foreground = new SolidColorBrush(Colors.Black);
                                }
                            }
                            else if (((System.Windows.Controls.ComboBox)controlObj[elementsInSheet[i]]).Name == "Combo_Conditional_Item_3")
                            {
                                target = _qstnvariablDD4.Where(z => z.QuestionVariable == val).FirstOrDefault();
                                index = target == null ? 0 : _qstnvariablDD4.IndexOf(target);
                                CmbItm3 = index;
                                if (index == 0)
                                {
                                    Combo_Conditional_Item_3.SelectedValue = 0;
                                    Combo_Conditional_Item_3.IsEnabled = false;
                                }
                                if (index > 0)
                                {
                                    Combo_Conditional_Item_3.SelectedValue = _qstnvariablDD4[index];
                                    Combo_Conditional_Item_3.IsEnabled = true;
                                    Combo_Conditional_Item_3.Foreground = new SolidColorBrush(Colors.Black);
                                }
                            }
                            else if (((System.Windows.Controls.ComboBox)controlObj[elementsInSheet[i]]).Name == "Combo_Conditional_Item_2")
                            {

                                target = _qstnvariablDD3.Where(z => z.QuestionVariable == val).FirstOrDefault();
                                index = target == null ? 0 : _qstnvariablDD3.IndexOf(target);
                                CmbItm2 = index;
                                if (index == 0)
                                {
                                    Combo_Conditional_Item_2.IsEnabled = false;
                                    Combo_Conditional_Item_2.SelectedValue = 0;
                                }
                                if (index > 0)
                                {
                                    Combo_Conditional_Item_2.SelectedValue = _qstnvariablDD3[index];
                                    Combo_Conditional_Item_2.IsEnabled = true;
                                    Combo_Conditional_Item_2.Foreground = new SolidColorBrush(Colors.Black);
                                }
                            }
                            else if (((System.Windows.Controls.ComboBox)controlObj[elementsInSheet[i]]).Name == "Combo_Conditional_Item_1")
                            {
                                target = _qstnvariablDD2.Where(z => z.QuestionVariable == val).FirstOrDefault();
                                index = target == null ? 0 : _qstnvariablDD2.IndexOf(target);
                                CmbItm1 = index;
                                if (index == 0)
                                {
                                    Combo_Conditional_Item_1.IsEnabled = false;
                                    Combo_Conditional_Item_1.SelectedValue = 0;
                                }
                                if (index > 0)
                                {
                                    Combo_Conditional_Item_1.IsEnabled = true;
                                    Combo_Conditional_Item_1.SelectedValue = _qstnvariablDD2[index];
                                    Combo_Conditional_Item_1.Foreground = new SolidColorBrush(Colors.Black);
                                }
                            }
                            else if (((System.Windows.Controls.ComboBox)controlObj[elementsInSheet[i]]).Name == "Combo_Conditional_Operator_1")
                            {
                                //  ((System.Windows.Controls.ComboBox)controlObj[elementsInSheet[i]]).SelectedItem = val;
                                selectedOperator(Combo_Conditional_Operator_1, obj[i + 2, 2]);
                            }
                            else if (((System.Windows.Controls.ComboBox)controlObj[elementsInSheet[i]]).Name == "Combo_Conditional_Operator_2")
                            {
                                selectedOperator(Combo_Conditional_Operator_2, obj[i + 2, 2]);
                            }
                            else if (((System.Windows.Controls.ComboBox)controlObj[elementsInSheet[i]]).Name == "Combo_Conditional_Operator_3")
                            {
                                selectedOperator(Combo_Conditional_Operator_3, obj[i + 2, 2]);
                            }
                            else if (((System.Windows.Controls.ComboBox)controlObj[elementsInSheet[i]]).Name == "Combo_Conditional_Operator_4")
                            {
                                selectedOperator(Combo_Conditional_Operator_4, obj[i + 2, 2]);
                            }
                            else if (((System.Windows.Controls.ComboBox)controlObj[elementsInSheet[i]]).Name == "Combo_Conditional_Operator_5")
                            {
                                selectedOperator(Combo_Conditional_Operator_5, obj[i + 2, 2]);
                            }
                            else if (((System.Windows.Controls.ComboBox)controlObj[elementsInSheet[i]]).Name == "Combo_NonAnser")
                            {
                                string text = obj[i + 2, 2] != null ? obj[i + 2, 2].ToString() : "";
                                NoAnswerVal = GetText(text);
                                ((System.Windows.Controls.ComboBox)controlObj[elementsInSheet[i]]).Text = GetText(text);
                                ((System.Windows.Controls.ComboBox)controlObj[elementsInSheet[i]]).IsEnabled = true;
                            }
                            else if (((System.Windows.Controls.ComboBox)controlObj[elementsInSheet[i]]).Name == "CBIndicatorForNoAnswerSPSSSAMA")
                            {
                                string text = obj[i + 2, 2] != null ? obj[i + 2, 2].ToString() : "";
                                NoAnswerValForSpss = GetText(text);
                                ((System.Windows.Controls.ComboBox)controlObj[elementsInSheet[i]]).Text = GetText(text);
                                ((System.Windows.Controls.ComboBox)controlObj[elementsInSheet[i]]).IsEnabled = true;
                            }
                            else if (((System.Windows.Controls.ComboBox)controlObj[elementsInSheet[i]]).Name == "CBMAVLVL")
                            {
                                string text = obj[i + 2, 2] != null ? obj[i + 2, 2].ToString() : "";
                                CBMAVLVLForSpss = GetText(text);
                                ((System.Windows.Controls.ComboBox)controlObj[elementsInSheet[i]]).Text = GetText(text);
                                ((System.Windows.Controls.ComboBox)controlObj[elementsInSheet[i]]).IsEnabled = true;
                            }
                            else if (((System.Windows.Controls.ComboBox)controlObj[elementsInSheet[i]]).Name == "Combo_Output_Type")
                            {
                                string text = obj[i + 2, 2] != null ? obj[i + 2, 2].ToString() : "";
                                OutputType = GetText(text);
                                ((System.Windows.Controls.ComboBox)controlObj[elementsInSheet[i]]).Text = GetText(text);
                                ((System.Windows.Controls.ComboBox)controlObj[elementsInSheet[i]]).IsEnabled = true;
                            }
                            else if (((System.Windows.Controls.ComboBox)controlObj[elementsInSheet[i]]).Name == "CBIndicatorForNoQuestionVariableSPSSSAMA")
                            {
                                string text = obj[i + 2, 2] != null ? obj[i + 2, 2].ToString() : "";
                                NoAnswerValForSpss = GetText(text);
                                ((System.Windows.Controls.ComboBox)controlObj[elementsInSheet[i]]).Text = GetText(text);
                                ((System.Windows.Controls.ComboBox)controlObj[elementsInSheet[i]]).IsEnabled = true;
                            }
                            else
                            {
                                ddlSaveOrNot = true;
                                string text = obj[i + 2, 2] != null ? obj[i + 2, 2].ToString() : "";
                                ((System.Windows.Controls.ComboBox)controlObj[elementsInSheet[i]]).Text = GetText(text);
                                ((System.Windows.Controls.ComboBox)controlObj[elementsInSheet[i]]).IsEnabled = true;
                            }

                        }
                        if (controlObj[elementsInSheet[i]] is System.Windows.Controls.TextBox)
                        {
                            ((System.Windows.Controls.TextBox)controlObj[elementsInSheet[i]]).Text = obj[i + 2, 2];
                        }
                        if (controlObj[elementsInSheet[i]] is System.Windows.Controls.RadioButton)
                        {
                            if (obj[i + 2, 2] == "True")
                                ((System.Windows.Controls.RadioButton)controlObj[elementsInSheet[i]]).IsChecked = true;
                            else
                                ((System.Windows.Controls.RadioButton)controlObj[elementsInSheet[i]]).IsChecked = false;
                        }
                        if (controlObj[elementsInSheet[i]] is System.Windows.Controls.CheckBox)
                        {
                            if (obj[i + 2, 2] == "True")
                                ((System.Windows.Controls.CheckBox)controlObj[elementsInSheet[i]]).IsChecked = true;
                            else
                                ((System.Windows.Controls.CheckBox)controlObj[elementsInSheet[i]]).IsChecked = false;
                        }
                    }
                    else
                    {
                        if(i == (elementsInSheet.Count-1) && !ddlSaveOrNot)
                        {
                            isComboSet = true;
                            Combo_Output_FileType.SelectedIndex = 0;
                            Combo_Output_Type.SelectedIndex = 0;
                            Combo_NonAnser.SelectedIndex = 0;
                            Combo_NonApplying.SelectedIndex = 0;
                        }
                    }
                }
                if (!isComboSet)
                {
                    Combo_Output_FileType.SelectedIndex = 0;
                    Combo_Output_Type.SelectedIndex = 0;
                    Combo_NonAnser.SelectedIndex = 0;
                    Combo_NonApplying.SelectedIndex = 0;
                }
                SetControls(SelectedFileFormat, NoAnswerVal, NoAnswerValForSpss, CBMAVLVLForSpss, OutputType);
                if (!elementsInSheet.Contains("F_Do_Output_Text_Output_Path_"))
                {
                    string filename, filepath = null, fullPath = null;
                    filename = System.IO.Path.GetFileNameWithoutExtension(SelectedFile);
                    filepath = System.IO.Path.GetDirectoryName(SelectedFile);
                    bool isNetworkPath = IsNetworkPath(System.IO.Path.GetDirectoryName(SelectedFile));
                    if (isNetworkPath == true)
                    {                       
                        fullPath = filepath + "\\" + filename.Split('_')[0] + Constants.ExportPathDefaultFileName;
                    }
                    else
                    {                                                
                        fullPath = filepath + "\\" + filename.Split('_')[0] + Constants.ExportPathDefaultFileName;
                        fullPath = fullPath.Replace("\\\\", "\\");
                    }                 
                    this.Text_Output_Path.Text = fullPath;
                }
                //-----------------------------------------------------------------------------IF KEY NOT PRESENT ADDING KEY TO THE 'A' SHEET TO THE LASTINDEX
                bool settingFirst = false;
                String[] keys = controlObj.Keys.ToArray<String>();
                for (int i = 0; i < keys.Count(); i++)
                {
                    if (!(elementsInSheet.Contains(keys[i])))
                    {
                        obj[lastIndexinAdvancedSettingsExcelSheet, 1] = keys[i];
                        ++lastIndexinAdvancedSettingsExcelSheet;
                        settingFirst = true;
                    }
                }
                if (settingFirst)
                {
                    rarCom.Value2 = obj;

                }
            }
            catch(Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private string GetText(string text)
        {
            if (QC4Common.Common.Constants.GlobalMode != "ja-JP," + Util.Constants.QCFont.MS_Gothic)
            {
                switch (text)
                {
                    case QC4Common.Common.Constants.DO_OUTPUT_FORMAT_EXCELEN:
                    case QC4Common.Common.Constants.DO_OUTPUT_FORMAT_EXCELJP:
                        return QC4Common.Common.Constants.DO_OUTPUT_FORMAT_EXCELEN;
                    case QC4Common.Common.Constants.DO_OUTPUT_FORMAT_CSVEN:
                    case QC4Common.Common.Constants.DO_OUTPUT_FORMAT_CSVJP:
                        return QC4Common.Common.Constants.DO_OUTPUT_FORMAT_CSVEN;
                    case QC4Common.Common.Constants.DO_OUTPUT_FORMAT_TABEN:
                    case QC4Common.Common.Constants.DO_OUTPUT_FORMAT_TABJP:
                        return QC4Common.Common.Constants.DO_OUTPUT_FORMAT_TABEN;
                    case QC4Common.Common.Constants.DO_OUTPUT_FORMAT_QC3EN:
                    case QC4Common.Common.Constants.DO_OUTPUT_FORMAT_QC3JP:
                        return QC4Common.Common.Constants.DO_OUTPUT_FORMAT_QC3EN;
                    case QC4Common.Common.Constants.DO_OUTPUT_FORMAT_QC4EN:
                    case QC4Common.Common.Constants.DO_OUTPUT_FORMAT_QC4JP:
                        return QC4Common.Common.Constants.DO_OUTPUT_FORMAT_QC4EN;
                    case QC4Common.Common.Constants.DO_OUTPUT_FORMAT_R2D3EN:
                    case QC4Common.Common.Constants.DO_OUTPUT_FORMAT_R2D3JP:
                        return QC4Common.Common.Constants.DO_OUTPUT_FORMAT_R2D3EN;


                    case QC4Common.Common.Constants.CB_ITEM_01_MA01EN:
                    case QC4Common.Common.Constants.CB_ITEM_01_MA01JP:
                        return QC4Common.Common.Constants.CB_ITEM_01_MA01EN;
                    case QC4Common.Common.Constants.CB_ITEM_02_MA_COMMA_SEPERATEDEN:
                    case QC4Common.Common.Constants.CB_ITEM_02_MA_COMMA_SEPERATEDJP:
                        return QC4Common.Common.Constants.CB_ITEM_02_MA_COMMA_SEPERATEDEN;
                    case QC4Common.Common.Constants.LABEL_MULTIDIGIT_NUMBERSEN:
                    case QC4Common.Common.Constants.LABEL_MULTIDIGIT_NUMBERSJP:
                        return QC4Common.Common.Constants.LABEL_MULTIDIGIT_NUMBERSEN;

                    case QC4Common.Common.Constants.LABEL_BLANKEN:
                    case QC4Common.Common.Constants.LABEL_BLANKJP:
                        return QC4Common.Common.Constants.LABEL_BLANKEN;

                    case QC4Common.Common.Constants.CB_MA_SPSS_01JP:
                    case QC4Common.Common.Constants.CB_MA_SPSS_01EN:
                        return QC4Common.Common.Constants.CB_MA_SPSS_01EN;
                    case QC4Common.Common.Constants.CB_MA_SPSS_02JP:
                    case QC4Common.Common.Constants.CB_MA_SPSS_02EN:
                        return QC4Common.Common.Constants.CB_MA_SPSS_02EN;
                    default:
                        return text;
                }
            }
            else
            {
                switch (text)
                {
                    case QC4Common.Common.Constants.DO_OUTPUT_FORMAT_EXCELJP:
                    case QC4Common.Common.Constants.DO_OUTPUT_FORMAT_EXCELEN:
                        return QC4Common.Common.Constants.DO_OUTPUT_FORMAT_EXCELJP;
                    case QC4Common.Common.Constants.DO_OUTPUT_FORMAT_CSVJP:
                    case QC4Common.Common.Constants.DO_OUTPUT_FORMAT_CSVEN:
                        return QC4Common.Common.Constants.DO_OUTPUT_FORMAT_CSVJP;
                    case QC4Common.Common.Constants.DO_OUTPUT_FORMAT_TABJP:
                    case QC4Common.Common.Constants.DO_OUTPUT_FORMAT_TABEN:
                        return QC4Common.Common.Constants.DO_OUTPUT_FORMAT_TABJP;
                    case QC4Common.Common.Constants.DO_OUTPUT_FORMAT_QC3JP:
                    case QC4Common.Common.Constants.DO_OUTPUT_FORMAT_QC3EN:
                        return QC4Common.Common.Constants.DO_OUTPUT_FORMAT_QC3JP;
                    case QC4Common.Common.Constants.DO_OUTPUT_FORMAT_QC4JP:
                    case QC4Common.Common.Constants.DO_OUTPUT_FORMAT_QC4EN:
                        return QC4Common.Common.Constants.DO_OUTPUT_FORMAT_QC4JP;
                    case QC4Common.Common.Constants.DO_OUTPUT_FORMAT_R2D3JP:
                    case QC4Common.Common.Constants.DO_OUTPUT_FORMAT_R2D3EN:
                        return QC4Common.Common.Constants.DO_OUTPUT_FORMAT_R2D3JP;


                    case QC4Common.Common.Constants.CB_ITEM_01_MA01EN:
                    case QC4Common.Common.Constants.CB_ITEM_01_MA01JP:
                        return QC4Common.Common.Constants.CB_ITEM_01_MA01JP;
                    case QC4Common.Common.Constants.CB_ITEM_02_MA_COMMA_SEPERATEDEN:
                    case QC4Common.Common.Constants.CB_ITEM_02_MA_COMMA_SEPERATEDJP:
                        return QC4Common.Common.Constants.CB_ITEM_02_MA_COMMA_SEPERATEDJP;
                    case QC4Common.Common.Constants.LABEL_MULTIDIGIT_NUMBERSEN:
                    case QC4Common.Common.Constants.LABEL_MULTIDIGIT_NUMBERSJP:
                        return QC4Common.Common.Constants.LABEL_MULTIDIGIT_NUMBERSJP;

                    case QC4Common.Common.Constants.LABEL_BLANKEN:
                    case QC4Common.Common.Constants.LABEL_BLANKJP:
                        return QC4Common.Common.Constants.LABEL_BLANKJP;

                    case QC4Common.Common.Constants.CB_MA_SPSS_01JP:
                    case QC4Common.Common.Constants.CB_MA_SPSS_01EN:
                        return QC4Common.Common.Constants.CB_MA_SPSS_01JP;
                    case QC4Common.Common.Constants.CB_MA_SPSS_02JP:
                    case QC4Common.Common.Constants.CB_MA_SPSS_02EN:
                        return QC4Common.Common.Constants.CB_MA_SPSS_02JP;
                    default:
                        return text;
                }
            }
        }
        #endregion

        #region saveSettings

        public void readEXcel()
        {
            elementsInSheet.Clear();
            var SettingSheet = ExcelUtil.GetWorkSheetByCodeName(Workbook, Constants.SheetCodeName.DetailsSetting);
            Excel.Range rar = SettingSheet.get_Range("A1", "C1000");
            var obj = rar.Value;
            int rowvalue = /*obj.GetLength(0)*/1000;

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


        public void GetValue(System.Windows.Controls.Control x)
        {
            if (x is System.Windows.Controls.ComboBox)
            {
                var myObject = ((System.Windows.Controls.ComboBox)x).SelectedValue as FilterSettingsView.FilterSettingsClass.DataExport;
                //bool isClasy = CheckComboBoxSelectedOrNot(x);
                if (myObject != null)
                    ReadValueFromExcel.Add("F_Do_Output_" + ((System.Windows.Controls.ComboBox)x).Name + "_", ((FilterSettingsView.FilterSettingsClass.DataExport)((System.Windows.Controls.ComboBox)x).SelectedItem).QuestionVariable);
                //else if (isClasy)
                //    ReadValueFromExcel.Add("F_Do_Output_" + ((System.Windows.Controls.ComboBox)x).Name + "_", ((System.Windows.Controls.ComboBox)x).Text);
                else
                    ReadValueFromExcel.Add("F_Do_Output_" + ((System.Windows.Controls.ComboBox)x).Name + "_", ((System.Windows.Controls.ComboBox)x).Text);

                ////var bindedObj =
                ///}
                ////if (bindedObj != null)

                //else
                //    ReadValueFromExcel.Add("F_Do_Output_" + ((System.Windows.Controls.ComboBox)x).Name + "_", "");
            }
            else if (x is System.Windows.Controls.TextBox)
                ReadValueFromExcel.Add("F_Do_Output_" + ((System.Windows.Controls.TextBox)x).Name + "_", ((System.Windows.Controls.TextBox)x).Text);

            else if (x is System.Windows.Controls.RadioButton)
                ReadValueFromExcel.Add("F_Do_Output_" + ((System.Windows.Controls.RadioButton)x).Name + "_", ((System.Windows.Controls.RadioButton)x).IsChecked.ToString());

            else if (x is System.Windows.Controls.CheckBox)
                ReadValueFromExcel.Add("F_Do_Output_" + ((System.Windows.Controls.CheckBox)x).Name + "_", ((System.Windows.Controls.CheckBox)x).IsChecked.ToString());
        }

        private bool CheckComboBoxSelectedOrNot(System.Windows.Controls.Control x)
        {
            switch(x.Name)
            {
                case "Combo_Classify_Item":
                    return CmbClsy==0?false:true;
                case "Combo_Conditional_Item_1":
                    return CmbItm1 == 0? false : true;
                case "Combo_Conditional_Item_2":
                    return CmbItm2 == 0 ? false : true;
                case "Combo_Conditional_Item_3":
                    return CmbItm3 ==0 ? false : true;
                case "Combo_Conditional_Item_4":
                    return CmbItm4 == 0 ? false : true;
                case "Combo_Conditional_Item_5":
                    return CmbItm5 == 0 ? false : true;
                default:
                    return true;
            }
        }

        internal void SaveSettings()
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {
                //----------------------------------------------------------GETTING THE VALUES OF EACH CONTROLS TO SAVE THE SETTING IN SHEET
                ReadValueFromExcel.Clear();
               

                List<string> contolname = new List<string>();
                foreach (System.Windows.Controls.ComboBox tb
                    in FindControls.FindLogicalChildren<System.Windows.Controls.ComboBox>(this))
                {
                    GetValue(tb);
                }
                foreach (System.Windows.Controls.RadioButton tb
                    in FindControls.FindLogicalChildren<System.Windows.Controls.RadioButton>(this))
                {
                    GetValue(tb);
                }

                foreach (System.Windows.Controls.TextBox tb
                    in FindControls.FindLogicalChildren<System.Windows.Controls.TextBox>(this))
                {
                    GetValue(tb);
                }
                foreach (System.Windows.Controls.CheckBox tb
                    in FindControls.FindLogicalChildren<System.Windows.Controls.CheckBox>(this))
                {
                    GetValue(tb);
                }

                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                readEXcel();

                //----------------------------------------------------------------------SETTING THE VALUE TO CORRESPONDING B CELLS IN THE SHEET

                var SettingSheet = ExcelUtil.GetWorkSheetByCodeName(Workbook, Constants.SheetCodeName.DetailsSetting);
                Excel.Range rar = SettingSheet.get_Range("A1", "B1000");
                var obj = rar.Value;
                int rowvalue = 1000;
                for (int i = 2; i < rowvalue; i++)
                {
                    if (obj[i, 1] != null)
                    {
                        if (ReadValueFromExcel.ContainsKey(elementsInSheet[i - 2]))
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

                //---------------------------------------------------------------------------SETTING THE SELECTED VALUE OF LISTBOX TO THE C Cell


                Excel.Range rar2 = SettingSheet.Range["C2"];
                Excel.Range rarEnd = ExcelUtil.EndxlUp(rar2);

                SettingSheet.get_Range(rar2, rarEnd).ClearContents();
				rarEnd = rar2.Offset[_dataExport_ListBoxCommonCopy.Count, 0];

				rar2 = SettingSheet.get_Range(rar2, rarEnd);
				var obj2 = rar2.Value;

                int k = 0;
                foreach (var a in _dataExport_ListBoxCommonCopy)
                {
					obj2[k + 1, 1] = a.QuestionVariable;
					++k;
                }
                rar2.Value2 = obj2;
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Arrow;
            }));
        }
        #endregion



#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        private async void Exportpathbtn_Click(object sender, RoutedEventArgs e)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            string fName = null;
            string FileName = Text_Output_Path.Text;
            if (FileName.Length > 260)
            {
                MessageDialog.Warning(LocalResource.MSG_PATH_TOO_LONG);
                return;
                
            }
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.Filter = filesextension;
            dlg.RestoreDirectory = true;
            string initDir = System.IO.Path.GetDirectoryName(FileName);
            if (!Directory.Exists(initDir))
                initDir = "";
            dlg.InitialDirectory = initDir;
            dlg.FileName = System.IO.Path.GetFileNameWithoutExtension(FileName);
            if (dlg.ShowDialog() == true)
            {
                fName = System.IO.Path.GetFileNameWithoutExtension(dlg.FileName);
                string fPath = System.IO.Path.GetDirectoryName(dlg.FileName);
                bool isNetworkPath = IsNetworkPath(fPath);
                if (isNetworkPath == true)
                {
                    fName = fPath + "\\" + fName;
                }
                else
                {
                    fName = fPath + "\\" + fName;
                    fName = fName.Replace("\\\\", "\\");
                }
                Text_Output_Path.Text = fName;
            }       

            //191  
            //Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();           
            //openFileDialog.Filter = "All files (*.*)|*.*";
            //openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //if (openFileDialog.ShowDialog() ==true )
            //    Text_Output_Path.Text = openFileDialog.FileName;

            //old code
            ////using (var fbd = new FolderBrowserDialog())
            ////{
            ////	DialogResult result = fbd.ShowDialog();
            ////	if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            ////	{
            ////		Text_Output_Path.Text = fbd.SelectedPath;
            ////		SelectedFile = fbd.SelectedPath + @"\" + System.IO.Path.GetFileName(SelectedFile);
            ////	}
            ////}
        }

        public bool IsNetworkPath(string path)
        {
            if (!path.StartsWith(@"/") && !path.StartsWith(@"\"))
            {
                string rootPath = System.IO.Path.GetPathRoot(path); // get drive's letter
                System.IO.DriveInfo driveInfo = new System.IO.DriveInfo(rootPath); // get info about the drive
                return driveInfo.DriveType == DriveType.Network; // return true if a network drive
            }

            return true; // is a UNC path
        }

        private void Cdfpathbtn_Click(object sender, RoutedEventArgs e)
        {
            string selectedfileName = SelectedFile;
            string FileName = selectedfileName.Substring(selectedfileName.LastIndexOf("\\") + 1);
            string fileName = string.Empty;
            string destFile = string.Empty;
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    //Text_Change_File.Text = string.Empty;
                    String CombinedPath = System.IO.Path.Combine(fbd.SelectedPath, FileName);
                    //Text_Change_File.Text = CombinedPath;
                }
            }
        }
        private void IsVariableProcessed()
        {
            string variable = ((DataExport)(Combo_Conditional_Item_1).SelectedItem).QuestionVariable;
            
            if (String.IsNullOrEmpty(variable) && true)
            {
                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_DATAEXPORT_FILTER_VALIDATION_VARIABLE, 1));
                return;
            }
            //
            
            if (String.IsNullOrEmpty(variable) && true)
            {
                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_DATAEXPORT_FILTER_VALIDATION_VARIABLE, 1));
                return;
            }
            //
            
            if (String.IsNullOrEmpty(variable) && true)
            {
                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_DATAEXPORT_FILTER_VALIDATION_VARIABLE, 1));
                return;
            }
            //
           
            if (String.IsNullOrEmpty(variable) && true)
            {
                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_DATAEXPORT_FILTER_VALIDATION_VARIABLE, 1));
                return;
            }
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            //wpftrial.usrctrl uc = new usrctrl();
            //FilterSettingsView.FilterControlDesign fdesign = new FilterControlDesign();
            //this.Container_panel.Children.Add(fdesign);


            //Window window = new Window();
            //StackPanel s = new StackPanel();
            //s.Children.Add(uc);
            //window.Content=s;
            //window.Show();


            //MainWindow parentShell = Window.GetWindow(this) as MainWindow;
            //System.Windows.Controls.TextBox txtval = parentShell.FindName("txtBox") as System.Windows.Controls.TextBox;
            //System.Windows.MessageBox.Show(txtval.Text);
            // WpfControlLibrary1.UserControl1 w = new WpfControlLibrary1.UserControl1();

            // FindControls.FindLogicalChildren<System.Windows.Controls.Button>(w);

            //foreach (System.Windows.Controls.Button b
            //  in FindControls.FindLogicalChildren<System.Windows.Controls.Button>(w))
            //{
            //    GetName(b);

            //}

            //validation for  criteria selection   --------------------------------------------------------------------------------
            //validation for value in txt field  SA,MA,N,FA
            try
            {
                if (!CheckRefine())
                    return;

                //
                ExportClick();
                //Util.ProgressBar progress = new Util.ProgressBar();
                //progress.progress = new ProgressBar(false);
                //progress.progress.Owner = this;
                //new System.Threading.Thread(() => ExportClick(progress)).Start();
                //progress.progress.ShowDialog();

                //if (!res)
                //{
                //	MessageDialog.ErrorOk("DataOutput failed");
                //}
            }
            catch (Exception ex)
            {

            }

        }

        private bool CheckRefine()
        {
            if (Check_Refine_Condition.IsChecked == true)
            {
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
            switch(v)
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

        private void ExportClick(Util.ProgressBar pb = null)
        {
            string FileName = Text_Output_Path.Text;
            if (FileName.Length > 260 )
            {
                
                MessageDialog.Warning(LocalResource.MSG_PATH_TOO_LONG);
                return;
            }
            bool isAnExist = false;
            for (int i=0;i< _dataExport_ListBoxCommonCopy.Count;i++)
            {
                if (Definiotion.VariableDictionary[_dataExport_ListBoxCommonCopy[i].QuestionVariable].QuestionFlag == "An")
                {
                    isAnExist = true;
                    break;
                }
            }
            if(isAnExist)
            {
                if(CountMissmatch(Workbook))
                    MessageDialog.Info(LocalResource.EX_DIFFERENCE_BETWEEN_DATASHEET_AND_MULTIVARIATE);
            }
            SaveSettings();
            CloseNotFromBtn = false;
            List<FilterSettingsCr> filterSettingsCrs = null;
            ReadFilterSettings(ref filterSettingsCrs);

            string[] questionVariables = _dataExport_ListBoxCommonCopy.Select(a => a.QuestionVariable).ToArray();
            string cmbtext = Combo_Classify_Item.SelectedIndex > 0 ? ((DataExport)(Combo_Classify_Item).SelectedItem).QuestionVariable : "";
            string noAns = Combo_Output_FileType.Text == LocalResource.DO_OUTPUT_FORMAT_SPSS ? CBIndicatorForNoQuestionVariableSPSSSAMA.Text : Combo_NonAnser.Text;
            DataOutput dao = new DataOutput(Workbook, Text_Output_Path.Text, Combo_Output_FileType.Text
                    , Check_Vertical.IsChecked.Value, Combo_Output_Type.Text, noAns
                    , Combo_NonApplying.Text, CBMAVLVL.Text, UNICODE.IsChecked.Value, cmbtext
                    , questionVariables, filterSettingsCrs);
            // this.Close();//191 ;closing filter window
            //  this.Close();//191 ;closing filter window
            dao.OutputMainStart(MainWndw, this);
            //   this.Close();//191 ;closing filter window
        }

        private bool CountMissmatch(Excel.Workbook workbook)
        {
            string connectionString = DB.DBHelper.GetConnectionString(workbook);
            string TableName = "answers";
            int dataCount = 0;
            int multivariateCount = 0;
            if (Qc4Launcher.DB.DBHelper.checkAfterProcess(workbook))
            {
                TableName = "data_after_process";
            }
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                con.Open();
                using (SQLiteCommand command = con.CreateCommand())
                {
                    command.CommandText = "select count(sort_no) from "+ TableName;
                    dataCount = Convert.ToInt32(command.ExecuteScalar());

                    command.CommandText = "select count(sort_no) from multivariate";
                    multivariateCount = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            return multivariateCount != dataCount;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckRefine())
                return;
            CloseNotFromBtn = false;
            SaveSettings();
            this.Close();
        }

        private void Usrctrl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void ReadFilterSettings(ref List<FilterSettingsCr> filterSettings)
        {
            if (Check_Refine_Condition.IsChecked != null)
            {
                if (!Convert.ToBoolean(Check_Refine_Condition.IsChecked))
                {
                    return;
                }
            }

            filterSettings = new List<FilterSettingsCr>();
            string variable = "";
            string question = "";
            if (Combo_Conditional_Operator_1.SelectedIndex != -1)
            {
                variable = ((DataExport)(Combo_Conditional_Item_1).SelectedItem).QuestionVariable;
                question = ((DataExport)(Combo_Conditional_Item_1).SelectedItem).Question;
            }
            string operatorr = Combo_Conditional_Operator_1.Text;
            string value = Combo_Conditional_Value_1.Text;
            if (String.IsNullOrEmpty(variable) || String.IsNullOrEmpty(operatorr) || String.IsNullOrEmpty(value))
            {
                return;
            }
            filterSettings.Add(new FilterSettingsCr(variable, operatorr, value, null, question));

            bool comboOr = false;
            bool comboAnd = false;
            if (Option_Conditional_And_1.IsChecked != null)
            {
                comboAnd = Convert.ToBoolean(Option_Conditional_And_1.IsChecked);
            }
            if (Option_Conditional_Or_1.IsChecked != null)
            {
                comboOr = Convert.ToBoolean(Option_Conditional_Or_1.IsChecked);
            }
            if (Combo_Conditional_Operator_2.SelectedIndex != -1)
            {
                variable = ((DataExport)(Combo_Conditional_Item_2).SelectedItem).QuestionVariable;
                question = ((DataExport)(Combo_Conditional_Item_2).SelectedItem).Question;
            }
            else
                variable = "";
            operatorr = Combo_Conditional_Operator_2.Text;
            value = Combo_Conditional_Value_2.Text;
            if (String.IsNullOrEmpty(variable) || String.IsNullOrEmpty(operatorr) || String.IsNullOrEmpty(value) || !(comboAnd || comboOr))
            {
                return;
            }
            filterSettings.Add(new FilterSettingsCr(variable, operatorr, value, comboOr ? "|" : "&",question));

            if (Option_Conditional_And_2.IsChecked != null)
            {
                comboAnd = Convert.ToBoolean(Option_Conditional_And_2.IsChecked);
            }
            if (Option_Conditional_Or_2.IsChecked != null)
            {
                comboOr = Convert.ToBoolean(Option_Conditional_Or_2.IsChecked);
            }
            if (Combo_Conditional_Operator_3.SelectedIndex != -1)
            {
                variable = ((DataExport)(Combo_Conditional_Item_3).SelectedItem).QuestionVariable;
                question = ((DataExport)(Combo_Conditional_Item_3).SelectedItem).Question;
            }
            else
                variable = "";
            operatorr = Combo_Conditional_Operator_3.Text;
            value = Combo_Conditional_Value_3.Text;
            if (String.IsNullOrEmpty(variable) || String.IsNullOrEmpty(operatorr) || String.IsNullOrEmpty(value) || !(comboAnd || comboOr))
            {
                return;
            }
            filterSettings.Add(new FilterSettingsCr(variable, operatorr, value, comboOr ? "|" : "&",question));

            if (Option_Conditional_And_3.IsChecked != null)
            {
                comboAnd = Convert.ToBoolean(Option_Conditional_And_3.IsChecked);
            }
            if (Option_Conditional_Or_3.IsChecked != null)
            {
                comboOr = Convert.ToBoolean(Option_Conditional_Or_3.IsChecked);
            }
            if (Combo_Conditional_Operator_4.SelectedIndex != -1)
            {
                variable = ((DataExport)(Combo_Conditional_Item_4).SelectedItem).QuestionVariable;
                question = ((DataExport)(Combo_Conditional_Item_4).SelectedItem).Question;
            }
            else
                variable = "";
            operatorr = Combo_Conditional_Operator_4.Text;
            value = Combo_Conditional_Value_4.Text;
            if (String.IsNullOrEmpty(variable) || String.IsNullOrEmpty(operatorr) || String.IsNullOrEmpty(value) || !(comboAnd || comboOr))
            {
                return;
            }
            filterSettings.Add(new FilterSettingsCr(variable, operatorr, value, comboOr ? "|" : "&",question));

            if (Option_Conditional_And_4.IsChecked != null)
            {
                comboAnd = Convert.ToBoolean(Option_Conditional_And_4.IsChecked);
            }
            if (Option_Conditional_Or_4.IsChecked != null)
            {
                comboOr = Convert.ToBoolean(Option_Conditional_Or_4.IsChecked);
            }
            if (Combo_Conditional_Operator_5.SelectedIndex != -1)
            {
                variable = ((DataExport)(Combo_Conditional_Item_5).SelectedItem).QuestionVariable;
                question = ((DataExport)(Combo_Conditional_Item_5).SelectedItem).Question;
            }
            else
                variable = "";
            operatorr = Combo_Conditional_Operator_5.Text;
            value = Combo_Conditional_Value_5.Text;
            if (String.IsNullOrEmpty(variable) || String.IsNullOrEmpty(operatorr) || String.IsNullOrEmpty(value) || !(comboAnd || comboOr))
            {
                return;
            }
            filterSettings.Add(new FilterSettingsCr(variable, operatorr, value, comboOr ? "|" : "&",question));
        }
        private void EnableRadioOnLoad()
        {
            if (Combo_Conditional_Value_1.Text != "" && Combo_Conditional_Value_1.Text != null)// Combo_Conditional_Value_1.IsEnabled == true &&
            {
                Option_Conditional_And_1.IsEnabled = true;
                Option_Conditional_Or_1.IsEnabled = true;
            }
            if (Combo_Conditional_Value_2.Text != "" && Combo_Conditional_Value_2.Text != null)
            {
                Option_Conditional_And_2.IsEnabled = true;
                Option_Conditional_Or_2.IsEnabled = true;
            }
            if (Combo_Conditional_Value_3.Text != "" && Combo_Conditional_Value_3.Text != null)
            {
                Option_Conditional_And_3.IsEnabled = true;
                Option_Conditional_Or_3.IsEnabled = true;
            }
            if (Combo_Conditional_Value_4.Text != "" && Combo_Conditional_Value_4.Text != null)
            {
                Option_Conditional_And_4.IsEnabled = true;
                Option_Conditional_Or_4.IsEnabled = true;
            }

        }
        
        private void KeyPress(object sender, System.Windows.Input.KeyEventArgs e)//trial- on construction- for delete press on combo &-select by typing
        {
            System.Windows.Controls.ComboBox sen = ((System.Windows.Controls.ComboBox)sender);
            if ((Key.Delete == e.Key || ((sen.SelectedIndex ==-1 && sen.Text=="")||(sen.SelectedIndex == -1 && sen.Text != "")))  && (this.Combo_Conditional_Item_1.IsKeyboardFocusWithin || this.Combo_Conditional_Item_1.IsDropDownOpen) &&
                (Option_Conditional_And_1.IsChecked == false && Option_Conditional_Or_1.IsChecked == false) )
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
            else if ((Key.Delete == e.Key || ((sen.SelectedIndex == -1 && sen.Text == "") || (sen.SelectedIndex == -1 && sen.Text != ""))) && (this.Combo_Conditional_Item_5.IsKeyboardFocusWithin || this.Combo_Conditional_Item_5.IsDropDownOpen) )
            {
                LastSelected = 0;
                this.Combo_Conditional_Item_5.SelectedIndex = 0;
            }
            else if ((Key.Delete == e.Key || ((sen.SelectedIndex == -1 && sen.Text == "") || (sen.SelectedIndex == -1 && sen.Text != ""))) && (this.Combo_Classify_Item.IsKeyboardFocusWithin || this.Combo_Classify_Item.IsDropDownOpen))
            {
                LastSelected = 0;
                this.Combo_Classify_Item.SelectedIndex = 0;
            }
            else if (sen.SelectedItem==null && (sen.IsKeyboardFocusWithin || sen.IsDropDownOpen))
            {
                if (sen.Name!= "Combo_Classify_Item")
                    sen.SelectedIndex = LastSelected;
            }


            //if (e.Key == Key.Delete)
            //{
            //    if (((System.Windows.Controls.ComboBox)sender).Name == "Combo_Conditional_Item_1")
            //    {
            //        if (_qstnvariablDD1[0].QuestionVariable == "" || _qstnvariablDD1[0].QuestionVariable == null)
            //        {
            //            ((System.Windows.Controls.ComboBox)sender).SelectedIndex = 0;
            //        }
            //    }
            //    else if (((System.Windows.Controls.ComboBox)sender).Name == "Combo_Conditional_Item_2")
            //    {
            //    }
            //    else if (((System.Windows.Controls.ComboBox)sender).Name == "Combo_Conditional_Item_3")
            //    {
            //    }
            //    else if (((System.Windows.Controls.ComboBox)sender).Name == "Combo_Conditional_Item_4")
            //    {
            //    }
            //    else if (((System.Windows.Controls.ComboBox)sender).Name == "Combo_Conditional_Item_5")
            //    {
            //    }

            //}
            //if (e.Key == Key.Back)
            //{
            //    if (keystring.Length <= 1) keystring = "";
            //    else
            //     keystring = keystring.Remove(keystring.Length-1, 1);
            //}
            //else
            //{
            //    keystring += e.Key.ToString();
            //    DataExport target = _qstnvariablDD1.Where(z => z.QuestionVariable.Contains(keystring)).FirstOrDefault();//p.Name.Contains(e.Text)
            //    int index = target == null ? -1 : _qstnvariablDD1.IndexOf(target);
            //    ((System.Windows.Controls.ComboBox)sender).SelectedIndex = index+1;//bcs of null in first loc
            //}
        }
        private static readonly string[] SuggestionValues = {
            "DK"
        };
        private void OnTextBoxChange(object sender, TextChangedEventArgs e)
        {
            if (Keyboard.IsKeyUp(Key.Back))
            {
                string _currentInput = "";
                string _currentSuggestion = "";
                string _currentText = "";

                int _selectionStart;
                int _selectionLength;
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
            }
            else if (((System.Windows.Controls.TextBox)sender) == Combo_Conditional_Value_2)
            {
                Option_Conditional_And_2.IsEnabled = enable;
                Option_Conditional_Or_2.IsEnabled = enable;
            }
            else if (((System.Windows.Controls.TextBox)sender) == Combo_Conditional_Value_3)
            {
                Option_Conditional_And_3.IsEnabled = enable;
                Option_Conditional_Or_3.IsEnabled = enable;
            }
            else if (((System.Windows.Controls.TextBox)sender) == Combo_Conditional_Value_4)
            {
                Option_Conditional_And_4.IsEnabled = enable;
                Option_Conditional_Or_4.IsEnabled = enable;
            }


        }

        private bool CheckValue(string textvalue, int categorycount,string type,string operatr)
        {
            return QC4Common.Validation.NumberCheck.CheckFromOption(textvalue, categorycount,type, operatr);
        }

        bool FirstFocus = true;
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
        System.Windows.Controls.ComboBox combo = null;
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

        private void Combo_Classify_Item_GotFocus(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.ComboBox sen = ((System.Windows.Controls.ComboBox)sender);
            combo = sen;
        }
        int LastSelected = 0;
        string LastSelectedText = "";
        private void Combo_Conditional_Item_1_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            System.Windows.Controls.TextBox txt = null;
            if (e.OriginalSource is System.Windows.Controls.TextBox)
                txt = (System.Windows.Controls.TextBox)e.OriginalSource;            

            if (combo.SelectedIndex != 0)
                LastSelected = combo.SelectedIndex;
            else if (combo.SelectedIndex == 0)
            {
                LastSelectedText = combo.Text;
            }
            if (Key.Delete == e.Key && (this.Combo_Conditional_Item_1.IsKeyboardFocusWithin || this.Combo_Conditional_Item_1.IsDropDownOpen) &&
                (Option_Conditional_And_1.IsChecked == false && Option_Conditional_Or_1.IsChecked == false))
            {
                LastSelectedText="";
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
            else if (Key.Delete == e.Key && (this.Combo_Classify_Item.IsKeyboardFocusWithin || this.Combo_Classify_Item.IsDropDownOpen))
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
        
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FirstFocus = true;
        }
        
        private void TabControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Filter.IsSelected && e.OriginalSource == ExportTextBlock)
            {
                ExportSet.IsSelected = true;
            }
            else if (ExportSet.IsSelected && e.OriginalSource == FilterTxtBlock)
            {
                Filter.IsSelected = true;
            }
        }
        //string PreviousSearchKey = "";
        //private void LBVariablesToExport_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        //{
        //    char c;
        //    bool isChar = Char.TryParse(e.Key.ToString(), out c);
        //    int currentSelectedIndex = LBVariablesToExport.SelectedIndex;
        //    if (isChar && (char.IsLetter(c) || char.IsDigit(c)))
        //    {
        //        var items = LBVariablesToExport.Items.Cast<DataExport>().Where(x => x.QuestionVariable.IndexOf(e.Key.ToString().ToUpper()) == 0 || x.QuestionVariable.IndexOf(e.Key.ToString().ToLower()) == 0).ToList();
        //        if (PreviousSearchKey == e.Key.ToString() && items.Count > 1)
        //        {
        //            int searchIndex = 0;
        //            int diff = 0;
        //            for (int i = 0; i < items.Count; i++)
        //            {
        //                searchIndex = LBVariablesToExport.Items.IndexOf(items[i]);
        //                diff = currentSelectedIndex - searchIndex;
        //                if (diff == 0)
        //                {
        //                    if ((i + 1) < items.Count)
        //                    {
        //                        LBVariablesToExport.SelectedIndex = LBVariablesToExport.Items.IndexOf(items[i + 1]);
        //                    }
        //                    else
        //                        LBVariablesToExport.SelectedItem = items[0];
        //                    break;
        //                }
        //            }
        //            if (diff != 0)
        //                LBVariablesToExport.SelectedItem = items[0];
        //            PreviousSearchKey = e.Key.ToString();
        //        }
        //        else if (items.Count > 0)
        //        {
        //            LBVariablesToExport.SelectedItem = items[0];
        //            PreviousSearchKey = e.Key.ToString();
        //        }
        //    }
        //    else if (e.Key == Key.Down)
        //    {
        //        e.Handled = true;
        //        LBVariablesToExport.SelectedIndex = currentSelectedIndex + 1;

        //    }
        //    else if (e.Key == Key.Up)
        //    {
        //        e.Handled = true;
        //        if (currentSelectedIndex != 0)
        //            LBVariablesToExport.SelectedIndex = currentSelectedIndex - 1;
        //    }
        //}

        //private void LBVariablesToExport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    //if (e.AddedItems.Count == 1 && LBVariablesToExport.SelectionMode != System.Windows.Controls.SelectionMode.Multiple)
        //    //{
        //    //    LBVariablesToExport.ScrollIntoView(e.AddedItems[0]);
        //    //}
        //}

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (CloseNotFromBtn)
            {
                if (!CheckRefine())
                    e.Cancel = true;
                SaveSettings();
            }
        }


        private void LBVariablesToExport_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            QC4Common.Util.GridCommonFunc.Arrow(sender, e);
            System.Windows.Controls.DataGrid grid = sender as System.Windows.Controls.DataGrid;
            if (e.Key == Key.Tab && grid.Name is "LBVariablesToExport")
            {
                e.Handled = true;
                ButtonDblRightArrow.Focus();
            }
            else if(e.Key == Key.Tab)
            {
                e.Handled = true;
                Combo_Output_FileType.Focus();
            }
        }

        System.Windows.Controls.DataGrid ExpGrid = null;
        private void LBVariablesToExport_CurrentCellChanged(object sender, EventArgs e)
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

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWndw.Show();
            MainWndw.Activate();
        }

        private void UTF_Checked(object sender, RoutedEventArgs e)
        {
            Constants.DataOutput.defaultEncoding = UTF.IsChecked.Value ? "UTF-8" : "Shift-JIS";
        }

        //static int Indx = 0;
        //static bool IsFirst = false;
        //static bool IsPressed = false;
        //static int StartIndx = 0;
        //private void ListBoxItem_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        //{
        //    //ListBoxMouseEnter(LBVariablesToExport, sender, e);
        //}

        //private void ListBoxItem_MouseEnter1(object sender, System.Windows.Input.MouseEventArgs e)
        //{
        //    ListBoxMouseEnter(ListBoxCommonCopy, sender, e);
        //}

        //private void LBVariablesToExport_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    ListBoxPreviewMouseDown(e);
        //}

        //private void ListBoxCommonCopy_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    ListBoxPreviewMouseDown(e);
        //}

        //private void ListBoxPreviewMouseDown(MouseButtonEventArgs e)
        //{
        //    if (e.ClickCount > 0)
        //        IsFirst = true;
        //}

        //private void ListBoxMouseEnter(System.Windows.Controls.ListBox listBox,object sender, System.Windows.Input.MouseEventArgs e)
        //{
        //    ListBoxItem lbi = sender as ListBoxItem;
        //    if (e.LeftButton == MouseButtonState.Released)
        //    {
        //        IsFirst = true;
        //        IsPressed = false;
        //        Indx = listBox.Items.IndexOf((lbi.DataContext as DataExport));
        //        StartIndx = Indx;
        //        listBox.SelectionMode = System.Windows.Controls.SelectionMode.Extended;
        //    }
        //    if (e.LeftButton == MouseButtonState.Pressed)
        //    {
        //        IsPressed = true;
        //        if (IsFirst)
        //        {
        //            if (listBox.SelectedItems.Count > 1)
        //            {
        //                listBox.SelectedItems.Clear();
        //            }
        //            else
        //            {
        //                object current = listBox.SelectedItem;
        //                listBox.SelectedItems.Clear();
        //                listBox.SelectedItems.Add(current);
        //            }
        //        }
        //        listBox.SelectionMode = System.Windows.Controls.SelectionMode.Multiple;
        //        if (Indx >= 0)
        //            listBox.SelectedItems.Add(listBox.Items[Indx]);
        //        Indx = listBox.Items.IndexOf((lbi.DataContext as DataExport));
        //        IEnumerable<DataExport> items = listBox.SelectedItems.Cast<FilterSettingsView.FilterSettingsClass.DataExport>();
        //        int lastSelectedIndx = listBox.Items.IndexOf(items.LastOrDefault());
        //        int bigIndx = listBox.Items.IndexOf(items.OrderBy(x => x.QuestionIndex).Last());
        //        if (lastSelectedIndx > Indx && items.Count() > 1 && bigIndx <= Indx + 1 && !IsFirst)
        //        {
        //            listBox.SelectedItems.Remove(items.LastOrDefault());
        //        }
        //        else if (lastSelectedIndx < Indx && items.Count() > 1 && bigIndx >= Indx && !IsFirst && StartIndx >= Indx)
        //        {
        //            listBox.SelectedItems.Remove(items.OrderBy(x => x.QuestionIndex).FirstOrDefault());
        //        }
        //        listBox.SelectedItems.Add(lbi);
        //        lbi.IsSelected = true;
        //        IsFirst = false;
        //        lbi.Focus();
        //        for (int i = 0; i < listBox.Items.Count; i++)
        //            (listBox.Items[i] as DataExport).ItemIndex = i;
        //        List<DataExport> data = listBox.SelectedItems.Cast<DataExport>().OrderBy(x => x.ItemIndex).ToList();
        //        int firstIndex = data[0].ItemIndex;
        //        int lastIndex = data[data.Count - 1].ItemIndex;
        //        for (int i = firstIndex; i < lastIndex; i++)
        //        {
        //            if (!data.Contains(listBox.Items[i]))
        //                listBox.SelectedItems.Add(listBox.Items[i]);
        //        }
        //    }
        //}

        //private void LBVariablesToExport_ScrollChanged(object sender, ScrollChangedEventArgs e)
        //{
        //    //ListBoxScrollChanged(LBVariablesToExport, sender, e);
        //}

        //private void ListBoxCommonCopy_ScrollChanged(object sender, ScrollChangedEventArgs e)
        //{
        //    ListBoxScrollChanged(ListBoxCommonCopy, sender, e);
        //}
        //private void ListBoxScrollChanged(System.Windows.Controls.ListBox listBox, object sender, ScrollChangedEventArgs e)
        //{
        //    if (IsPressed)
        //    {
        //        if (e.VerticalChange > 0.0)
        //        {
        //            if (listBox.Items.Count > 0)
        //            {
        //                IEnumerable<DataExport> items = listBox.SelectedItems.Cast<FilterSettingsView.FilterSettingsClass.DataExport>().OrderBy(x => x.QuestionIndex);

        //                listBox.SelectedItems.Add(listBox.Items[10 + Convert.ToInt32(e.VerticalOffset)]);

        //                for (int i = StartIndx; i <= listBox.Items.IndexOf(items.Last()); i++)
        //                {
        //                    if (listBox.Items.IndexOf(items.OrderBy(x => x.QuestionIndex).First()) <= StartIndx)
        //                        break;
        //                    listBox.SelectedItems.Add(listBox.Items[i]);
        //                }
        //                if (listBox.SelectedItems.Cast<FilterSettingsView.FilterSettingsClass.DataExport>().OrderBy(x => x.QuestionIndex).First().QuestionIndex < StartIndx)
        //                {
        //                    listBox.SelectedItems.Remove(listBox.Items[11]);
        //                }
        //                IsFirst = false;
        //            }
        //        }
        //        else
        //        {
        //            if (listBox.Items.Count > 0)
        //            {
        //                listBox.SelectedItems.Add(listBox.Items[Convert.ToInt32(e.VerticalOffset)]);
        //                IEnumerable<DataExport> items = listBox.SelectedItems.Cast<FilterSettingsView.FilterSettingsClass.DataExport>().OrderBy(x => x.QuestionIndex);
        //                for (int i = listBox.Items.IndexOf(items.Last()); StartIndx < i; i--)
        //                {
        //                    if (listBox.Items.IndexOf(items.First()) >= StartIndx)
        //                        break;
        //                    listBox.SelectedItems.Remove(listBox.Items[i]);
        //                }
        //                IsFirst = false;
        //            }
        //        }
        //        for (int i = 0; i < listBox.Items.Count; i++)
        //            (listBox.Items[i] as DataExport).ItemIndex = i;
        //        List<DataExport> data = listBox.SelectedItems.Cast<DataExport>().OrderBy(x => x.ItemIndex).ToList();
        //        int firstIndex = data[0].ItemIndex;
        //        int lastIndex = data[data.Count - 1].ItemIndex;
        //        for (int i = firstIndex; i < lastIndex; i++)
        //        {
        //            if (!data.Contains(listBox.Items[i]))
        //                listBox.SelectedItems.Add(listBox.Items[i]);
        //        }
        //    }
        //}

        //private void LBVariablesToExport_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    ListBoxPreviewMouseUp();
        //}

        //private void ListBoxCommonCopy_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    ListBoxPreviewMouseUp();
        //}

        //private void ListBoxPreviewMouseUp()
        //{
        //    IsPressed = false;
        //}

    }
}

