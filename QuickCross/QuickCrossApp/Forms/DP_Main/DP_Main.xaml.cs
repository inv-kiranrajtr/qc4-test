using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Qc4Launcher.Util;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Constants = ExcelAddIn.Common.Constants;
using Excel = Microsoft.Office.Interop.Excel;
using ExcelAddIn.Sheets;
using QC4Common.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Data.SQLite;
using Qc4Launcher.DB;
using ExcelAddIn;
using Qc4Launcher.Forms.UserControls;
using Qc4Launcher.Forms.GrossTabulationSetting.Common;
using System.Threading;
using QC4Common;
using excel = Microsoft.Office.Interop.Excel;
using Qc4Launcher.Logic;
using log4net;
using System.Reflection;

namespace Qc4Launcher.Forms.DP_Main
{
    /// <summary>
    /// Interaction logic for DP_Main.xaml
    /// </summary>
    public partial class DP_Main : Window
    {
        Excel.Workbook Workbook;
        MainWindow MainWindow_;
        public List<String> Methods;
        public List<ProcessMethod> ProcessMethods;
        public List<ProcessMethod> DefaultMethods = new List<ProcessMethod>();
        public bool isModified = false;
        public bool isExecuted = false;
        private int rowNumber;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private ObservableCollection<QC4Common.Sheets.DataProcess> _dataProcess_AllProcessList = new ObservableCollection<QC4Common.Sheets.DataProcess>();
        private Dictionary<String, QuestionSettings> PopulatedDictionary = new Dictionary<string, QuestionSettings>();
        private ObservableCollection<DataProcessList> dataProcessList = new ObservableCollection<DataProcessList>();
        private ObservableCollection<DataProcessList> dataProcessListView = new ObservableCollection<DataProcessList>();
        QC4Common.Util.FormUtil frmutil = new QC4Common.Util.FormUtil();
        delegate void GetDatad_CallBack();
        public delegate void dpmainclosedelegate();

        /// <summary>Gets or sets the data process List</summary>
        /// <value>The data process ListView.</value>
        public ObservableCollection<DataProcessList> DataProcessListView
        {
            get
            {
                return dataProcessList;
            }
            set
            {
                dataProcessList = value;
            }
        }

        /// <summary>Initializes a new instance of the <see cref="DP_Main"/> class.</summary>
        /// <param name="workbook">The workbook.</param>
        public DP_Main(MainWindow mainWindow, Excel.Workbook workbook)
        {
            MainWindow_ = mainWindow;
            Workbook = workbook;
            InitializeComponent();
            ReadSettingsOnLoad();
            GetData();
            if (QC4Common.Common.Constants.GlobalMode.Split(',')[0] != "ja-JP")
                Button_Help.Visibility = Visibility.Hidden;
            else
                Button_Help.Visibility = Visibility.Visible;
        }

        /// <summary>Handles the Loaded event of the Window control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Methods = new List<string>();
            Methods.Add(LocalResource.LBL_CREATE_NEW_VARIABLE);
            Methods.Add(LocalResource.LBL_REVISE_DATA);
            Methods.Add(LocalResource.LBL_EXCLUDE);
            Methods.Add(LocalResource.LBL_DELETE_CASES);
            Methods.Add(LocalResource.LBL_OUTPUT_LIST);
            List_DisposalName.ItemsSource = Methods;
            List_DisposalName.SelectedItem = Methods.FirstOrDefault();

            ProcessMethods = new List<ProcessMethod>();
            ProcessMethods.Add(new ProcessMethod() { Name = LocalResource.LBL_RECODE_DESCRIPTION, Type = Util.Constants.ProcessingMethod.RECODE, TagName = Util.Constants.ProcessingMethod.RECODE });
            ProcessMethods.Add(new ProcessMethod() { Name = LocalResource.LBL_INTEGRATE_DESCRIPTION, Type = Util.Constants.ProcessingMethod.INTEGRATE, TagName = Util.Constants.ProcessingMethod.INTEGRATE });
            ProcessMethods.Add(new ProcessMethod() { Name = LocalResource.LBL_CLASS_DESCRIPTION, Type = Util.Constants.ProcessingMethod.CLASS, TagName = Util.Constants.ProcessingMethod.CLASS });
            ProcessMethods.Add(new ProcessMethod() { Name = LocalResource.LBL_MCONVERT_DESCRIPTION, Type = Util.Constants.ProcessingMethod.MCONVERT, TagName = Util.Constants.ProcessingMethod.MCONVERT });
            ProcessMethods.Add(new ProcessMethod() { Name = LocalResource.LBL_COUNT_DESCRIPTION, Type = Util.Constants.ProcessingMethod.COUNT, TagName = Util.Constants.ProcessingMethod.COUNT });
            ProcessMethods.Add(new ProcessMethod() { Name = LocalResource.LBL_ADD_DESCRIPTION, Type = Util.Constants.ProcessingMethod.ADD, TagName = Util.Constants.ProcessingMethod.ADD });
            ProcessMethods.Add(new ProcessMethod() { Name = LocalResource.LBL_JOINT_DESCRIPTION, Type = Util.Constants.ProcessingMethod.JOINT, TagName = Util.Constants.ProcessingMethod.JOINT });
            ProcessMethods.Add(new ProcessMethod() { Name = LocalResource.LBL_MTOS_DESCRIPTION, Type = Util.Constants.ProcessingMethod.MTOS, TagName = Util.Constants.ProcessingMethod.MTOS });
            ProcessMethods.Add(new ProcessMethod() { Name = LocalResource.LBL_GROUP_DESCRIPTION, Type = Util.Constants.ProcessingMethod.GROUP, TagName = Util.Constants.ProcessingMethod.GROUP });
            ProcessMethods.Add(new ProcessMethod() { Name = LocalResource.LBL_COMPUTE_DESCRIPTION, Type = Util.Constants.ProcessingMethod.COMPUTE, TagName = Util.Constants.ProcessingMethod.COMPUTE });
            List_DisposalMethod.ItemsSource = ProcessMethods;
            DefaultMethods = ProcessMethods;
            List_DisposalMethod.SelectedItem = ProcessMethods.FirstOrDefault();
            SaveCheckListValue();
            Set_Delete_Checklist_Value();
        }

        /// <summary>Handles the Click event of the CloseBtn control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            var found = DataProcessListView.All(u => (u.OnOrOff == (LocalResource.CELL_OFF_EN)));
            if (found)
            {
                this.Close();
            }
            else if (isExecuted)
            {
                this.Close();
            }
            else
            {
                if (!isModified)
                {
                    this.Close();
                }
                else
                {
                    MessageBoxResult result;
                    result = MessageBox.Show(LocalResource.CLOSE_DP_WARNING_MESSAGE, LocalResource.TITLE_QC, MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.No)
                    {
                        return;
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }

        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>Handles the Click event of the Undo control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        /// <exception cref="Exception"></exception>
        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            if (QC4Common.Common.DataProcessCommonFunctions.RestoreDataAfterProcess(Workbook))
            {
                Undo.IsEnabled = false;
            }
        }

        /// <summary>Reads the data on load.</summary>
        private void GetData()
        {
            int onof = 0;
            if (QC4Common.Common.Constants.GlobalMode.Split(',')[0] == "ja-JP")//Todo
            {
                onof = 1;
            }
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            PopulatedDictionary = Util.Definiotion.VariableDictionary;
            Microsoft.Office.Interop.Excel.Worksheet SettingSheet = Util.ExcelUtil.GetWorkSheetByCodeName(Workbook, Qc4Launcher.Util.Constants.SheetCodeName.DataProcessS);
            Excel.Range dpstart = SettingSheet.Cells[Constants.DP.ProUIstartRow, Constants.DP.OnOffColumn];
            Excel.Range lastcell = ExcelUtil.EndxlUp(SettingSheet.Cells[Constants.DP.ProUIstartRow, Constants.DP.OnOffColumn]);
            Excel.Range rar = SettingSheet.Range[dpstart, lastcell];
            int count = -1;
            int forCount = 0;
            string digit = string.Empty;
            int? repeats = null;
            foreach (Excel.Range cell in rar.Cells)
            {
                try
                {
                    if (cell.Row >= 5 && !string.IsNullOrEmpty(cell.Text))
                    {
                        QuestionSettings qs1 = new QuestionSettings();
                        Excel.Range start = SettingSheet.Cells[cell.Row, cell.Column];
                        Excel.Range end = ExcelUtil.EndxlRight(start);
                        Excel.Range range = SettingSheet.get_Range(start, end);
                        var obj = range.Value;
                        int itemsCount = obj.Length;
                        string question = null;
                        if (obj[1, 6] != null)
                        {
                            if (obj[1, 6] == QC4Common.Common.Constants.DP.InstructionFOR)
                            {
                                forCount = 1;
                                repeats = int.Parse(obj[1, 10]) - int.Parse(obj[1, 9]) + 1;
                                digit = obj[1, 9];

                            }
                            if (obj[1, 6] == QC4Common.Common.Constants.DP.InstructionNEXT)
                            {
                                forCount = 0;

                            }
                            if (obj[1, 6] == QC4Common.Common.Constants.DP.InstructionOMIT)
                            {
                                count = count + 1;
                                dataProcessList.Add(new DataProcessList()
                                {

                                    OnOrOff = (obj[1, 1].ToString().Trim() == LocalResource.CELL_ON ? LocalResource.CELL_ON_EN : LocalResource.CELL_OFF_EN),
                                    NewVariable = null,
                                    QuestionStatement = null,
                                    ProcessingMethodType = QC4Common.Common.Constants.DP.InstructionOMIT,
                                    Repeats = null,
                                    OriginalItem = FindOriginalItem(SettingSheet, range),
                                    DataProcessIndex = count,
                                    RowNo = cell.Row
                                });
                            }
                            if (obj[1, 6] == QC4Common.Common.Constants.DP.InstructionLISTUP)
                            {
                                count = count + 1;
                                dataProcessList.Add(new DataProcessList()
                                {
                                    OnOrOff = (obj[1, 1].ToString().Trim() == LocalResource.CELL_ON ? LocalResource.CELL_ON_EN : LocalResource.CELL_OFF_EN),
                                    NewVariable = null,
                                    QuestionStatement = null,
                                    ProcessingMethodType = QC4Common.Common.Constants.DP.InstructionLISTUP,
                                    Repeats = null,
                                    OriginalItem = FindOriginalItem(SettingSheet, range),
                                    DataProcessIndex = count,
                                    RowNo = cell.Row
                                });
                            }
                            if (obj[1, 6] == QC4Common.Common.Constants.DP.InstructionLDEL)
                            {
                                count = count + 1;
                                dataProcessList.Add(new DataProcessList()
                                {
                                    OnOrOff = (obj[1, 1].ToString().Trim() == LocalResource.CELL_ON ? LocalResource.CELL_ON_EN : LocalResource.CELL_OFF_EN),
                                    NewVariable = null,
                                    QuestionStatement = null,
                                    ProcessingMethodType = QC4Common.Common.Constants.DP.InstructionDELETE,
                                    Repeats = null,
                                    OriginalItem = null,
                                    DataProcessIndex = count,
                                    RowNo = cell.Row
                                });
                            }
                            if (obj[1, 6] == QC4Common.Common.Constants.DP.InstructionDELETE)
                            {
                                count = count + 1;
                                dataProcessList.Add(new DataProcessList()
                                {
                                    OnOrOff = (obj[1, 1].ToString().Trim() == LocalResource.CELL_ON ? LocalResource.CELL_ON_EN : LocalResource.CELL_OFF_EN),
                                    NewVariable = null,
                                    QuestionStatement = null,
                                    ProcessingMethodType = QC4Common.Common.Constants.DP.InstructionDELETE,
                                    Repeats = null,
                                    OriginalItem = null,
                                    DataProcessIndex = count,
                                    RowNo = cell.Row
                                });
                            }

                            if (obj[1, 7] != null)
                            {
                                // Handles recode repeat items
                                if (obj[1, 8] == QC4Common.Common.Constants.DP.SubstituteOperatorRECODE && forCount == 1)
                                {
                                    string repeatVarible = obj[1, 7];
                                    string repeatSourceVariable = obj[1, 9];
                                    List<string> repeatSourceVariableList = new List<string>();
                                    repeatSourceVariableList.Add(repeatSourceVariable.Replace("\\", LocalResource.LBL_REPEAT_STRING));
                                    bool hasvalue = Util.Definiotion.VariableDictionary.TryGetValue((repeatVarible.Remove(repeatVarible.Length - 3) + digit), out qs1);
                                    if (hasvalue)
                                    {
                                        question = frmutil.EscapeCRLF(qs1.Question);
                                    }
                                    else
                                    {
                                        question = null;
                                    }
                                    count = count + 1;
                                    int row = cell.Row;
                                    dataProcessList.Add(new DataProcessList()
                                    {
                                        OnOrOff = (obj[1, 1].ToString().Trim() == LocalResource.CELL_ON ? LocalResource.CELL_ON_EN : LocalResource.CELL_OFF_EN),
                                        NewVariable = (repeatVarible.Replace("\\", LocalResource.LBL_REPEAT_STRING)),
                                        RepeatNewVariable = (repeatVarible.Remove(repeatVarible.Length - 3) + digit),
                                        QuestionStatement = question,
                                        ProcessingMethodType = obj[1, 8],
                                        Repeats = repeats,
                                        OriginalItem = repeatSourceVariableList,
                                        RepeateSourceVariable = repeatSourceVariableList[0],
                                        DataProcessIndex = count,
                                        RowNo = row
                                    });


                                }
                                else
                                {

                                    repeats = null;
                                    digit = string.Empty;
                                    bool hasvalue = Util.Definiotion.VariableDictionary.TryGetValue(obj[1, 7], out qs1);
                                    if (hasvalue)
                                    {
                                        question = frmutil.EscapeCRLF(qs1.Question);
                                    }
                                    else
                                    {
                                        question = null;
                                    }
                                    count = count + 1;
                                    int row = cell.Row;

                                    //Handles revise data
                                    if (obj[1, 8] == ExcelAddIn.Common.Constants.DP.SubstituteOperatorADD1 || obj[1, 8] == ExcelAddIn.Common.Constants.DP.SubstituteOperatorADD2 || obj[1, 8] == ExcelAddIn.Common.Constants.DP.SubstituteOperatorEQUAL || obj[1, 8] == ExcelAddIn.Common.Constants.DP.SubstituteOperatorMINUS1 || obj[1, 8] == ExcelAddIn.Common.Constants.DP.SubstituteOperatorMINUS2)
                                    {
                                        List<string> reviseDataSourceVariableList = new List<string>();
                                        reviseDataSourceVariableList.Add(obj[1, 7]);
                                        dataProcessList.Add(new DataProcessList()
                                        {
                                            OnOrOff = (obj[1, 1].ToString().Trim() == LocalResource.CELL_ON ? LocalResource.CELL_ON_EN : LocalResource.CELL_OFF_EN),
                                            NewVariable = null,
                                            QuestionStatement = question,
                                            ProcessingMethodType = QC4Common.Common.Constants.DP.SubstituteOperatorADD_MINUS,
                                            Repeats = null,
                                            OriginalItem = reviseDataSourceVariableList,
                                            DataProcessIndex = count,
                                            RowNo = row
                                        });
                                    }

                                    else
                                    {
                                        //Handles compute data process
                                        if (obj[1, 8] == Constants.DP.SubstituteOperatorCOMPUTE)
                                        {
                                            List<string> computeSourceVariableList = new List<string>();
                                            computeSourceVariableList.Add(GetComputeSourceVariable(obj[1, 9]));
                                            dataProcessList.Add(new DataProcessList()
                                            {
                                                OnOrOff = (obj[1, 1].ToString().Trim() == LocalResource.CELL_ON ? LocalResource.CELL_ON_EN : LocalResource.CELL_OFF_EN),
                                                NewVariable = obj[1, 7],
                                                QuestionStatement = question,
                                                ProcessingMethodType = obj[1, 8],
                                                Repeats = null,
                                                OriginalItem = computeSourceVariableList,
                                                DataProcessIndex = count,
                                                RowNo = row
                                            });
                                        }
                                        else
                                        {
                                            if (obj[1, 8] == Constants.DP.SubstituteOperatorADD3)
                                            {
                                                dataProcessList.Add(new DataProcessList()
                                                {
                                                    OnOrOff = (obj[1, 1].ToString().Trim() == LocalResource.CELL_ON ? LocalResource.CELL_ON_EN : LocalResource.CELL_OFF_EN),
                                                    NewVariable = obj[1, 7],
                                                    QuestionStatement = question,
                                                    ProcessingMethodType = Util.Constants.ProcessingMethod.ADD,
                                                    Repeats = null,
                                                    OriginalItem = FindOriginalItem(SettingSheet, range),
                                                    DataProcessIndex = count,
                                                    RowNo = row
                                                });
                                            }
                                            else
                                            {
                                                dataProcessList.Add(new DataProcessList()
                                                {
                                                    OnOrOff = (obj[1, 1].ToString().Trim() == LocalResource.CELL_ON ? LocalResource.CELL_ON_EN : LocalResource.CELL_OFF_EN),
                                                    NewVariable = obj[1, 7],
                                                    QuestionStatement = question,
                                                    ProcessingMethodType = obj[1, 8],
                                                    Repeats = null,
                                                    OriginalItem = FindOriginalItem(SettingSheet, range),
                                                    DataProcessIndex = count,
                                                    RowNo = row
                                                });
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
                    _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                    string exs = ex.Message;
                }
            }
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            if ((DataProcessListView == null) || (DataProcessListView.Count == 0))
            {
                ProcessListViewNoItems.Visibility = Visibility.Visible;
                Execute.IsEnabled = false;
            }
            else
            {
                ProcessListViewNoItems.Visibility = Visibility.Hidden;
                Execute.IsEnabled = true;
            }
        }


        public string GetComputeSourceVariable(string Formula1)
        {
            List<string> qs = new List<string>();
            string cellcontent = Formula1;
            cellcontent = cellcontent.Replace('[', '@');
            cellcontent = cellcontent.Replace(']', '@');
            string[] splitcontent = cellcontent.Split('@');
            string VariableString = "";
            foreach (string item in splitcontent)
            {
                if (Util.Definiotion.VariableDictionary.ContainsKey(item))
                {


                    QuestionSettings qsObject = Util.Definiotion.VariableDictionary[item];
                    if ((qsObject.AnswerType.Equals(Constants.AnswerType.SA) || qsObject.AnswerType.Equals(Constants.AnswerType.N)) ||
                        (qsObject.AnswerType.Equals(Constants.AnswerType.SA) || qsObject.AnswerType.Equals(Constants.AnswerType.N)))
                    {

                        qs.Add(qsObject.Variable);
                    }
                }
            }
            VariableString = string.Join(",", qs);
            return VariableString;
        }
        /// <summary>Finds the original item.</summary>
        /// <param name="SettingSheet">The setting sheet.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        private List<string> FindOriginalItem(Excel.Worksheet sheet, Excel.Range range)
        {
            List<string> Params = new List<string>();
            QuestionSettings qs2 = new QuestionSettings();
            try
            {
                var obj = range.Value;
                int rowval = obj.Length;
                for (int i = 9; i < rowval; i++)
                {
                    if (obj[1, i] != null)
                    {
                        string param = frmutil.EscapeCRLF((obj[1, i].ToString()));
                        bool hasParam = Util.Definiotion.VariableDictionary.TryGetValue(param, out qs2);
                        if (hasParam)
                        {
                            Params.Add(param);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                string message = ex.Message;
            }

            return Params;
        }

        /// <summary>Reads the settings on load.</summary>
        private void ReadSettingsOnLoad()
        {
            // Handles Undo button enabling/disabling
            Excel.Worksheet ws = ExcelUtil.GetWorkSheetBySheetName(Workbook, Constants.SheetType.sh_Data01 + "(Processed)");
            if (ws == null)
            {
                Undo.IsEnabled = false;
            }
            else
            {
                Undo.IsEnabled = true;
            }

            // Handles enabling/disabling of Delete, Edit, Copy, OnOrOff, SingleRightArrow and SingleLefttArrow buttons on loading
            if ((DataProcessListView == null) || (DataProcessListView.Count == 0))
            {
                Delete.IsEnabled = false;
                Edit.IsEnabled = false;
                Copy.IsEnabled = false;
                OnOrOff.IsEnabled = false;
                deleteFromQuestionSetting.IsEnabled = false;
                ButtonSingleRightArrow.IsEnabled = false;
                ButtonSingleRightArrowImage.Opacity = 0.1;
                ButtonSingleLefttArrow.IsEnabled = false;
                ButtonSingleLefttArrowImage.Opacity = 0.1;
            }
            else
            {
                if (DataProcessListView.Count == 1)
                {
                    ButtonSingleRightArrow.IsEnabled = false;
                    ButtonSingleLefttArrow.IsEnabled = false;
                    ButtonSingleRightArrowImage.Opacity = 0.1;
                    ButtonSingleLefttArrowImage.Opacity = 0.1;
                }
                if (lv_ProcessListView.SelectedItem == null)
                {
                    Delete.IsEnabled = false;
                    Edit.IsEnabled = false;
                    Copy.IsEnabled = false;
                    OnOrOff.IsEnabled = false;
                    deleteFromQuestionSetting.IsEnabled = false;
                    ButtonSingleRightArrow.IsEnabled = false;
                    ButtonSingleRightArrowImage.Opacity = 0.1;
                    ButtonSingleLefttArrow.IsEnabled = false;
                    ButtonSingleLefttArrowImage.Opacity = 0.1;
                }
            }
        }

        /// <summary>Handles the SelectionChanged event of the List_DisposalName control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void List_DisposalName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            string selectedProcessingType = List_DisposalName.SelectedItem.ToString();
            ProcessMethods = new List<ProcessMethod>();

            if (selectedProcessingType == LocalResource.LBL_CREATE_NEW_VARIABLE)
            {
                ProcessMethods = DefaultMethods;
            }
            else if (selectedProcessingType == LocalResource.LBL_REVISE_DATA)
            {
                ProcessMethods.Add(new ProcessMethod() { Name = LocalResource.LBL_ADD_MINUS_DESCRIPTION, Type = LocalResource.LBL_REVISE_DATA, TagName = QC4Common.Common.Constants.DP.SubstituteOperatorADD_MINUS });
            }
            else if (selectedProcessingType == LocalResource.LBL_EXCLUDE)
            {
                ProcessMethods.Add(new ProcessMethod() { Name = LocalResource.LBL_OMIT_DESCRIPTION, Type = LocalResource.LBL_EXCLUDE, TagName = QC4Common.Common.Constants.DP.InstructionOMIT });
            }
            else if (selectedProcessingType == LocalResource.LBL_DELETE_CASES)
            {
                ProcessMethods.Add(new ProcessMethod() { Name = LocalResource.LBL_DELETE_CASES_DESCRIPTION, Type = LocalResource.LBL_DELETE_CASES, TagName = QC4Common.Common.Constants.DP.InstructionDELETE });
            }
            else if (selectedProcessingType == LocalResource.LBL_OUTPUT_LIST)
            {
                ProcessMethods.Add(new ProcessMethod() { Name = LocalResource.LBL_OUTPUT_LIST_DESCRIPTION, Type = LocalResource.LBL_OUTPUT_LIST, TagName = QC4Common.Common.Constants.DP.InstructionLISTUP });
            }

            List_DisposalMethod.ItemsSource = ProcessMethods;
            List_DisposalMethod.SelectedItem = ProcessMethods.FirstOrDefault();


        }

        /// <summary>Called when [ On/Off click].</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OnOrOff_Click(object sender, RoutedEventArgs e)
        {
            DataProcessHelper dataProcessHelper = new DataProcessHelper();
            Microsoft.Office.Interop.Excel.Worksheet dataProcessSheet = Util.ExcelUtil.GetWorkSheetByCodeName(Workbook, Qc4Launcher.Util.Constants.SheetCodeName.DataProcessS);
            foreach (var selectedItem in lv_ProcessListView.SelectedItems)
            {
                dataProcessHelper.ONorOFFDataProcess((selectedItem as DataProcessList).RowNo, (selectedItem as DataProcessList).Repeats, dataProcessSheet, (selectedItem as DataProcessList).ProcessingMethodType);
                if (string.Equals((selectedItem as DataProcessList).OnOrOff, LocalResource.CELL_OFF_EN))
                {
                    (selectedItem as DataProcessList).OnOrOff = LocalResource.CELL_ON_EN;
                    isModified = true;
                }
                else
                {
                    (selectedItem as DataProcessList).OnOrOff = LocalResource.CELL_OFF_EN;
                }
            }
        }

        /// <summary>Handles the Click event of the Delete control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lv_ProcessListView.SelectedItems != null)
                {
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                    isModified = true;
                    var items = lv_ProcessListView.SelectedItems.Cast<DataProcessList>().ToList().OrderBy(a => a.RowNo);
                    DataProcessHelper dataProcessHelper = new DataProcessHelper();
                    int count = 0;
                    int repeatCount = 0;
                    MessageBoxResult result;

                    Microsoft.Office.Interop.Excel.Worksheet dataProcessSheet = Util.ExcelUtil.GetWorkSheetByCodeName(Workbook, Qc4Launcher.Util.Constants.SheetCodeName.DataProcessS);
                    if (deleteFromQuestionSetting.IsChecked == false)
                    {

                        result = MessageBox.Show(String.Format(LocalResource.ALERT_DELETE_DATA_PROCESSING), LocalResource.TITLE_QC, MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result != MessageBoxResult.Yes)
                        {
                            return;
                        }
                        else
                        {
                            foreach (DataProcessList item in items)
                            {
                                if (item.Repeats != null)
                                {
                                    dataProcessHelper.DeleteFromDataProcesssheet(item.RowNo, dataProcessSheet, item.Repeats.Value, item.ProcessingMethodType);
                                }
                                else
                                {
                                    dataProcessHelper.DeleteFromDataProcesssheet(item.RowNo, dataProcessSheet, 0, item.ProcessingMethodType);
                                }
                                DataProcessListView.Remove(item);
                            }
                            dataProcessList = new ObservableCollection<DataProcessList>();
                            GetData(); // Update dataprocess list.
                            lv_ProcessListView.ItemsSource = DataProcessListView;
                        }

                    }
                    else
                    {
                        result = MessageBox.Show(String.Format(LocalResource.ALERT_DELETE_FROM_QS_DATA_PROCESSING), LocalResource.TITLE_QC, MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result != MessageBoxResult.Yes)
                        {
                            return;
                        }
                        else
                        {
                            Util.QS.QuestionDelete questionDelete = new Util.QS.QuestionDelete();
                            List<string> listToBeDeleted = new List<string>();
                            QuestionSettings qs1 = new QuestionSettings();
                            foreach (DataProcessList item in items)//All selected items
                            {

                                if ((item as DataProcessList).NewVariable != null)
                                {
                                    string newVariable = (item as DataProcessList).NewVariable;
                                    if ((item as DataProcessList).Repeats == null)
                                    {
                                        newVariable = (item as DataProcessList).NewVariable;
                                    }
                                    else
                                    {
                                        newVariable = (item as DataProcessList).RepeatNewVariable;
                                    }
                                    bool hasvalue = Util.Definiotion.VariableDictionary.TryGetValue(newVariable, out qs1);
                                    if (hasvalue && qs1.QuestionFlag == "New")
                                    {

                                        if ((item as DataProcessList).Repeats != null)
                                        {
                                            questionDelete.StartQuestionDelete(Workbook, newVariable, false, (item as DataProcessList).Repeats.Value);
                                            count = count + 1;
                                        }
                                        else
                                        {
                                            questionDelete.StartQuestionDelete(Workbook, newVariable, false);
                                            count = count + 1;
                                        }

                                    }
                                    else if (!hasvalue)
                                    {
                                        if (item.Repeats != null)
                                        {

                                            dataProcessHelper.DeleteDataProcess(item.RowNo - (3 * repeatCount) - count - 1, dataProcessSheet, 3, item.ProcessingMethodType);
                                            repeatCount = repeatCount + 1;
                                        }
                                        else
                                        {

                                            dataProcessHelper.DeleteDataProcess(item.RowNo - (3 * repeatCount) - count, dataProcessSheet, 1, item.ProcessingMethodType);
                                            count = count + 1;
                                        }
                                    }
                                }
                                else
                                {
                                    //Deletes only from data process sheet.
                                    if (item.Repeats != null)
                                    {

                                        dataProcessHelper.DeleteDataProcess(item.RowNo - (3 * repeatCount) - count - 1, dataProcessSheet, 3, item.ProcessingMethodType);
                                        repeatCount = repeatCount + 1;
                                    }
                                    else
                                    {

                                        dataProcessHelper.DeleteDataProcess(item.RowNo - (3 * repeatCount) - count, dataProcessSheet, 1, item.ProcessingMethodType);
                                        count = count + 1;
                                    }


                                    DataProcessListView.Remove(item);
                                }
                                dataProcessList = new ObservableCollection<DataProcessList>();
                                GetData(); // Update dataprocess list.
                                lv_ProcessListView.ItemsSource = DataProcessListView;

                            }
                            if (deleteFromQuestionSetting.IsChecked.Value == true)
                            {
                                deleteFromQuestionSetting.IsChecked = false;
                            }
                        }

                    }
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    if (DataProcessListView.Count == 1)
                    {

                        ButtonSingleRightArrow.IsEnabled = false;
                        ButtonSingleRightArrowImage.Opacity = 0.1;
                        ButtonSingleLefttArrow.IsEnabled = false;
                        ButtonSingleLefttArrowImage.Opacity = 0.1;
                    }

                    lv_ProcessListView.ItemsSource = DataProcessListView;
                    lv_ProcessListView.SelectedItem = DataProcessListView.LastOrDefault();

                    if (DataProcessListView == null || DataProcessListView.Count == 0)
                    {

                        Delete.IsEnabled = false;
                        deleteFromQuestionSetting.IsEnabled = false;
                        OnOrOff.IsEnabled = false;
                    }
                    // Handles Undo button enabling/disabling
                    Excel.Worksheet ws = ExcelUtil.GetWorkSheetBySheetName(Workbook, Constants.SheetType.sh_Data01 + "(Processed)");
                    if (ws == null)
                    {
                        Undo.IsEnabled = false;
                    }
                    else
                    {
                        Undo.IsEnabled = true;
                    }

                }
            }

            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            DataProcessList dataProcess = new DataProcessList();
            dataProcess = (lv_ProcessListView.SelectedItem) as DataProcessList;

            try
            {
                if (dataProcess != null)
                {
                    int readRow = dataProcess.RowNo;
                    int writeRow = DataProcessListView[DataProcessListView.Count - 1].RowNo + 1;
                    if (DataProcessListView[DataProcessListView.Count - 1].Repeats != null)
                    {
                        writeRow = DataProcessListView[DataProcessListView.Count - 1].RowNo + 1 + 1;
                    }
                    switch (dataProcess.ProcessingMethodType)
                    {
                        case QC4Common.Common.Constants.DP.SubstituteOperatorRECODE:
                            RECODE recodeWindow = new RECODE(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Copy, dataProcess);
                            try
                            {
                                this.Hide();
                                recodeWindow.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(recodeWindow.isModifiedProcess);
                            }
                            break;

                        case QC4Common.Common.Constants.DP.SubstituteOperatorAVG:
                        case QC4Common.Common.Constants.DP.SubstituteOperatorMAX:
                        case QC4Common.Common.Constants.DP.SubstituteOperatorMIN:
                        case QC4Common.Common.Constants.DP.SubstituteOperatorSUM:
                            Group groupWindow = new Group(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Copy);
                            try
                            {
                                this.Hide();
                                groupWindow.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(groupWindow.isModifiedProcess);
                            }
                            break;

                        case QC4Common.Common.Constants.DP.SubstituteOperatorADD_MINUS:
                            ReviseData reviseData = new ReviseData(Workbook, readRow, writeRow, Util.Constants.ProcessingType.ReviseData, QC4Common.Common.Constants.STD_DP.Process_Copy);
                            try
                            {
                                this.Hide();
                                reviseData.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(reviseData.isModifiedProcess);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.SubstituteOperatorINTEGRATE:
                            Integrate integrate = new Integrate(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Copy);
                            try
                            {
                                this.Hide();
                                integrate.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(integrate.isModifiedProcess);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.SubstituteOperatorMTOS:

                            MTOS mtosWindow = new MTOS(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Copy);
                            try
                            {
                                this.Hide();
                                mtosWindow.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(mtosWindow.isModifiedProcess);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.SubstituteOperatorCOMPUTE:
                            Compute cmpt = new Compute(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Copy);
                            try
                            {
                                this.Hide();

                                cmpt.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(cmpt.isModifiedProcess);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.SubstituteOperatorMCONVERT:
                            MConvert mconvert = new MConvert(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Copy);
                            try
                            {

                                this.Hide();
                                mconvert.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(mconvert.isModifiedProcess);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.SubstituteOperatorCLASS:
                            Class classWindow = new Class(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Copy);
                            try
                            {
                                this.Hide();
                                classWindow.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(classWindow.isModifiedProcess);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.InstructionOMIT:
                            Exclude exclude = new Exclude(Workbook, readRow, writeRow, Util.Constants.ProcessingType.Exclude, QC4Common.Common.Constants.STD_DP.Process_Copy);
                            try
                            {
                                this.Hide();
                                exclude.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(exclude.isModifiedProcess);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.InstructionLISTUP:
                            OutputList outputlist = new OutputList(Workbook, readRow, writeRow, Util.Constants.ProcessingType.OutputList, QC4Common.Common.Constants.STD_DP.Process_Copy);
                            try
                            {
                                this.Hide();
                                outputlist.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(outputlist.isModifiedProcess);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.SubstituteOperatorJOINT:
                            Joint joint = new Joint(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Copy);
                            try
                            {
                                this.Hide();
                                joint.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(joint.isModifiedProcess);
                            }
                            break;
                        case Util.Constants.ProcessingMethod.ADD:
                            ADD add = new ADD(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Copy);
                            try
                            {
                                this.Hide();
                                add.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(add.isModifiedProcess);
                            }
                            break;

                        case QC4Common.Common.Constants.DP.SubstituteOperatorCOUNT:
                            COUNT countWindow = new COUNT(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Copy);
                            try
                            {
                                this.Hide();
                                countWindow.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(countWindow.isModifiedProcess);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.SubstituteOperatorDELETE:

                            DeleteCases deleteCasesWindow = new DeleteCases(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Copy);
                            try
                            {
                                this.Hide();
                                deleteCasesWindow.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(deleteCasesWindow.isModifiedProcess);
                            }
                            break;
                        default:
                            break;
                    }

                }

            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                string exception = ex.Message;
            }

        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            DataProcessList dataProcess = new DataProcessList();
            dataProcess = (lv_ProcessListView.SelectedItem) as DataProcessList;

            try
            {
                if (dataProcess != null)
                {
                    int readRow = dataProcess.RowNo;
                    int writeRow = dataProcess.RowNo;
                    bool isRepeatCase = false;
                    switch (dataProcess.ProcessingMethodType)
                    {
                        case QC4Common.Common.Constants.DP.SubstituteOperatorRECODE:
                            if (dataProcess.Repeats != null)
                            {
                                writeRow = writeRow - 1;
                                isRepeatCase = true;
                            }
                            RECODE recodeWindow = new RECODE(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Edit, dataProcess, isRepeatCase);
                            try
                            {
                                this.Hide();

                                recodeWindow.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(recodeWindow.isModifiedProcess, true);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.SubstituteOperatorAVG:
                        case QC4Common.Common.Constants.DP.SubstituteOperatorMAX:
                        case QC4Common.Common.Constants.DP.SubstituteOperatorMIN:
                        case QC4Common.Common.Constants.DP.SubstituteOperatorSUM:
                            Group groupWindow = new Group(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Edit);
                            try
                            {
                                this.Hide();
                                groupWindow.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(groupWindow.isModifiedProcess, true);
                            }
                            break;

                        case QC4Common.Common.Constants.DP.SubstituteOperatorADD_MINUS:
                            ReviseData reviseData = new ReviseData(Workbook, readRow, writeRow, Util.Constants.ProcessingType.ReviseData, QC4Common.Common.Constants.STD_DP.Process_Edit);
                            try
                            {
                                this.Hide();
                                reviseData.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(reviseData.isModifiedProcess, true);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.SubstituteOperatorINTEGRATE:
                            Integrate integrate = new Integrate(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Edit);
                            try
                            {

                                this.Hide();
                                integrate.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(integrate.isModifiedProcess, true);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.SubstituteOperatorMCONVERT:
                            MConvert mconvert = new MConvert(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Edit);
                            try
                            {

                                this.Hide();
                                mconvert.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(mconvert.isModifiedProcess, true);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.SubstituteOperatorMTOS:

                            MTOS mtosWindow = new MTOS(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Edit);
                            try
                            {

                                this.Hide();
                                mtosWindow.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {

                                this.Show();
                                UpdateDP_Main(mtosWindow.isModifiedProcess, true);
                            }
                            break;

                        case QC4Common.Common.Constants.DP.SubstituteOperatorCOMPUTE:

                            Compute comp = new Compute(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Edit);
                            try
                            {

                                this.Hide();
                                comp.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {

                                this.Show();
                                UpdateDP_Main(comp.isModifiedProcess, true);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.SubstituteOperatorCLASS:
                            Class classWindow = new Class(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Edit);
                            try
                            {
                                this.Hide();
                                classWindow.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(classWindow.isModifiedProcess, true);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.InstructionOMIT:
                            Exclude exclude = new Exclude(Workbook, readRow, writeRow, Util.Constants.ProcessingType.Exclude, QC4Common.Common.Constants.STD_DP.Process_Edit);
                            try
                            {
                                this.Hide();
                                exclude.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(exclude.isModifiedProcess, true);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.InstructionLISTUP:
                            OutputList outputlist = new OutputList(Workbook, readRow, writeRow, Util.Constants.ProcessingType.OutputList, QC4Common.Common.Constants.STD_DP.Process_Edit);
                            try
                            {
                                this.Hide();
                                outputlist.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(outputlist.isModifiedProcess);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.SubstituteOperatorCOUNT:
                            COUNT countWindow = new COUNT(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Edit);
                            try
                            {
                                this.Hide();
                                countWindow.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(countWindow.isModifiedProcess, true);
                            }
                            break;
                        case Util.Constants.ProcessingMethod.ADD:
                            ADD add = new ADD(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Edit);
                            try
                            {
                                this.Hide();


                                add.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(add.isModifiedProcess, true);

                            }
                            break;
                        case QC4Common.Common.Constants.DP.SubstituteOperatorDELETE:

                            DeleteCases deleteCasesWindow = new DeleteCases(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Edit);
                            try
                            {

                                this.Hide();
                                deleteCasesWindow.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {

                                this.Show();
                                UpdateDP_Main(deleteCasesWindow.isModifiedProcess, true);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.SubstituteOperatorJOINT:
                            Joint joint = new Joint(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Edit);
                            try
                            {

                                this.Hide();
                                joint.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(joint.isModifiedProcess, true);

                            }
                            break;
                        default:
                            break;
                    }

                }

            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                string exception = ex.Message;
            }
        }

        /// <summary>Handles the Click event of the ButtonSingleRightArrow control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonSingleRightArrow_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (lv_ProcessListView.SelectedIndex > 0)
                {
                    isModified = true;
                    int currentSelectedIndex = lv_ProcessListView.SelectedIndex;
                    int rowno = (lv_ProcessListView.SelectedItem as DataProcessList).RowNo;
                    int currentIndex = (lv_ProcessListView.SelectedItem as DataProcessList).DataProcessIndex;
                    bool isRepeatProcess = ((lv_ProcessListView.SelectedItem as DataProcessList).Repeats == null) ? false : true;
                    string processType = (lv_ProcessListView.SelectedItem as DataProcessList).ProcessingMethodType;
                    Excel.Worksheet dataProcessSheet = Util.ExcelUtil.GetWorkSheetByCodeName(Workbook, Qc4Launcher.Util.Constants.SheetCodeName.DataProcessS);
                    DataProcessHelper dpHelper = new DataProcessHelper();
                    int startRow = 5;
                    int endRow = 5;

                    switch (processType)
                    {
                        case QC4Common.Common.Constants.DP.InstructionLISTUP:
                            startRow = rowno - GetMultipleCriteriaRowsCount(rowno, dataProcessSheet) + 1;
                            endRow = rowno;
                            break;
                        case QC4Common.Common.Constants.DP.InstructionOMIT:
                            startRow = rowno - GetMultipleCriteriaRowsCount(rowno, dataProcessSheet) + 1;
                            endRow = rowno;
                            break;
                        case QC4Common.Common.Constants.DP.SubstituteOperatorADD_MINUS:
                            startRow = rowno - GetMultipleCriteriaRowsCount(rowno, dataProcessSheet) + 1;
                            endRow = rowno;
                            break;

                        case QC4Common.Common.Constants.DP.SubstituteOperatorDELETE:
                            startRow = rowno - GetMultipleCriteriaRowsCount(rowno, dataProcessSheet) + 1;
                            endRow = rowno;

                            break;
                        case QC4Common.Common.Constants.DP.SubstituteOperatorRECODE:
                            if (isRepeatProcess)
                            {
                                startRow = rowno - 1;
                                endRow = rowno + 1;
                            }
                            else
                            {
                                startRow = rowno;
                                endRow = rowno;
                            }

                            break;
                        default:
                            startRow = rowno;
                            endRow = rowno;
                            break;
                    }

                    int aboveInstructionRowsCount = GetRowsCountOfInstruction(currentIndex - 1);
                    for (int i = 0; i < aboveInstructionRowsCount; i++)
                    {
                        dpHelper.DP_Up_Down_Row(dataProcessSheet, startRow - i, endRow - i, false);
                    }

                    dataProcessList = new ObservableCollection<DataProcessList>();
                    GetData(); // Update dataprocess list.
                    lv_ProcessListView.ItemsSource = DataProcessListView;
                    lv_ProcessListView.SelectedIndex = currentSelectedIndex - 1;

                    if (lv_ProcessListView.SelectedIndex == 0)
                    {
                        ButtonSingleRightArrow.IsEnabled = false;
                        ButtonSingleRightArrowImage.Opacity = 0.1;
                        if (lv_ProcessListView.Items.Count == 2)
                        {
                            ButtonSingleLefttArrow.IsEnabled = true;
                            ButtonSingleLefttArrowImage.Opacity = 1;
                        }
                    }
                    else
                    {
                        ButtonSingleLefttArrow.IsEnabled = true;
                        ButtonSingleLefttArrowImage.Opacity = 1;
                    }
                }
                // Handles selection on scrolling items
                lv_ProcessListView.ScrollIntoView(lv_ProcessListView.SelectedItem);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        /// <summary>Handles the Click event of the ButtonSingleLefttArrow control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonSingleLefttArrow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lv_ProcessListView.SelectedIndex < lv_ProcessListView.Items.Count - 1)
                {
                    isModified = true;
                    int currentSelectedIndex = lv_ProcessListView.SelectedIndex;
                    int rowno = (lv_ProcessListView.SelectedItem as DataProcessList).RowNo;
                    int currentIndex = (lv_ProcessListView.SelectedItem as DataProcessList).DataProcessIndex;
                    bool isRepeatProcess = ((lv_ProcessListView.SelectedItem as DataProcessList).Repeats == null) ? false : true;
                    string processType = (lv_ProcessListView.SelectedItem as DataProcessList).ProcessingMethodType;
                    Excel.Worksheet dataProcessSheet = Util.ExcelUtil.GetWorkSheetByCodeName(Workbook, Qc4Launcher.Util.Constants.SheetCodeName.DataProcessS);
                    DataProcessHelper dpHelper = new DataProcessHelper();
                    int startRow = 5;
                    int endRow = 5;

                    switch (processType)
                    {
                        case QC4Common.Common.Constants.DP.InstructionLISTUP:
                            startRow = rowno - GetMultipleCriteriaRowsCount(rowno, dataProcessSheet) + 1;
                            endRow = rowno;
                            break;
                        case QC4Common.Common.Constants.DP.InstructionOMIT:
                            startRow = rowno - GetMultipleCriteriaRowsCount(rowno, dataProcessSheet) + 1;
                            endRow = rowno;
                            break;
                        case QC4Common.Common.Constants.DP.SubstituteOperatorADD_MINUS:
                            startRow = rowno - GetMultipleCriteriaRowsCount(rowno, dataProcessSheet) + 1;
                            endRow = rowno;
                            break;

                        case QC4Common.Common.Constants.DP.SubstituteOperatorDELETE:
                            startRow = rowno - GetMultipleCriteriaRowsCount(rowno, dataProcessSheet) + 1;
                            endRow = rowno;

                            break;
                        case QC4Common.Common.Constants.DP.SubstituteOperatorRECODE:
                            if (isRepeatProcess)
                            {
                                startRow = rowno - 1;
                                endRow = rowno + 1;
                            }
                            else
                            {
                                startRow = rowno;
                                endRow = rowno;
                            }

                            break;
                        default:
                            startRow = rowno;
                            endRow = rowno;
                            break;
                    }

                    int belowInstructionRowsCount = GetRowsCountOfInstruction(currentIndex + 1);
                    for (int i = 0; i < belowInstructionRowsCount; i++)
                    {
                        dpHelper.DP_Up_Down_Row(dataProcessSheet, startRow + i, endRow + i, true);
                    }


                    dataProcessList = new ObservableCollection<DataProcessList>();
                    GetData(); // Update dataprocess list.
                    lv_ProcessListView.ItemsSource = DataProcessListView;
                    lv_ProcessListView.SelectedIndex = currentSelectedIndex + 1;

                    if (lv_ProcessListView.SelectedIndex == lv_ProcessListView.Items.Count - 1)
                    {
                        ButtonSingleLefttArrow.IsEnabled = false;
                        ButtonSingleLefttArrowImage.Opacity = 0.1;
                        if (lv_ProcessListView.Items.Count == 2)
                        {
                            ButtonSingleRightArrow.IsEnabled = true;
                            ButtonSingleRightArrowImage.Opacity = 1;
                        }
                    }
                    else
                    {
                        ButtonSingleRightArrow.IsEnabled = true;
                        ButtonSingleRightArrowImage.Opacity = 1;
                    }
                }
                // Handles selection on scrolling items
                lv_ProcessListView.ScrollIntoView(lv_ProcessListView.SelectedItem);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

        }

        private int GetRowsCountOfInstruction(int index)
        {
            int totalRows = 1;
            try
            {
                Microsoft.Office.Interop.Excel.Worksheet dataProcessSheet = Util.ExcelUtil.GetWorkSheetByCodeName(Workbook, Qc4Launcher.Util.Constants.SheetCodeName.DataProcessS);
                DataProcessList dataProcessList = DataProcessListView[index];
                int rowNo = dataProcessList.RowNo;
                string processType = dataProcessList.ProcessingMethodType;
                switch (processType)
                {
                    case QC4Common.Common.Constants.DP.InstructionLISTUP:
                        totalRows = GetMultipleCriteriaRowsCount(rowNo, dataProcessSheet);
                        break;
                    case QC4Common.Common.Constants.DP.InstructionOMIT:
                        totalRows = GetMultipleCriteriaRowsCount(rowNo, dataProcessSheet);
                        break;
                    case QC4Common.Common.Constants.DP.SubstituteOperatorADD_MINUS:
                        totalRows = GetMultipleCriteriaRowsCount(rowNo, dataProcessSheet);
                        break;

                    case QC4Common.Common.Constants.DP.SubstituteOperatorDELETE:
                        totalRows = GetMultipleCriteriaRowsCount(rowNo, dataProcessSheet);

                        break;
                    case QC4Common.Common.Constants.DP.SubstituteOperatorRECODE:
                        if (dataProcessList.Repeats != null)
                        {
                            totalRows = 3;
                        }
                        else
                        {
                            totalRows = 1;
                        }

                        break;
                    default:
                        totalRows = 1;
                        break;
                }

            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
            return totalRows;

        }

        private int GetMultipleCriteriaRowsCount(int rowN0, Excel.Worksheet dataProcessSheet)
        {
            int totalRows = 1;
            for (int i = rowN0; i > rowN0 - 4; i--)
            {
                Excel.Range range = dataProcessSheet.Cells[i - 1, 8];

                if (range.Value == QC4Common.Common.Constants.DP.InstructionAND || range.Value == QC4Common.Common.Constants.DP.InstructionOR)
                {
                    totalRows++;
                }
                else
                {
                    break;
                }
            }

            return totalRows;
        }


        /// <summary>
        /// Represents Data Process List
        /// </summary>
        public class DataProcessList : INotifyPropertyChanged
        {
            private string onOrOff;
            private string newVariable;
            private string questionStatement;
            private string processingMethodType;
            private string command;
            private int? repeats;
            private int dataProcessIndex;
            private int rowNo;
            private bool invalid = false;
            private string repeatNewVariable;
            private string repeateSourceVariable;
            private List<string> originalItem;

            public string OnOrOff
            {
                get
                {
                    return onOrOff;
                }
                set
                {
                    onOrOff = value;
                    NotifyPropertyChanged();
                }
            }
            public bool Invalid
            {
                get
                {
                    return invalid;
                }
                set
                {
                    invalid = value;
                    NotifyPropertyChanged();
                }
            }

            public string NewVariable
            {
                get
                {
                    return newVariable;
                }
                set
                {
                    newVariable = value;
                }
            }
            public string RepeatNewVariable
            {
                get
                {
                    return repeatNewVariable;
                }
                set
                {
                    repeatNewVariable = value;
                }
            }
            public string RepeateSourceVariable
            {
                get
                {
                    return repeateSourceVariable;
                }
                set
                {
                    repeateSourceVariable = value;
                }
            }

            public string QuestionStatement
            {
                get
                {
                    return questionStatement;
                }
                set
                {
                    questionStatement = value;
                }
            }

            public string ProcessingMethodType
            {
                get
                {
                    return processingMethodType;
                }
                set
                {
                    processingMethodType = value;
                }
            }

            public string Command
            {
                get
                {
                    return command;
                }
                set
                {
                    command = value;
                }
            }

            public int? Repeats
            {
                get
                {
                    return repeats;
                }
                set
                {
                    repeats = value;
                }
            }

            public List<string> OriginalItem
            {
                get
                {
                    return originalItem;
                }
                set
                {
                    originalItem = new List<string>();
                    originalItem = value;
                }
            }
            public string OriginalItems
            {
                get
                {
                    if (OriginalItem != null)
                    {
                        return string.Join(", ", OriginalItem);
                    }
                    return string.Empty;
                }
            }
            public int DataProcessIndex
            {
                get
                {
                    return dataProcessIndex;
                }
                set
                {
                    dataProcessIndex = value;
                }
            }
            public int RowNo
            {
                get
                {
                    return rowNo;
                }
                set
                {
                    rowNo = value;
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            // This method is called by the Set accessor of each property. 
            // The CallerMemberName attribute that is applied to the optional propertyName 
            // parameter causes the property name of the caller to be substituted as an argument. 
            private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }
        /// <summary>
        /// Represents Process Method
        /// </summary>
        public class ProcessMethod
        {
            private string name;
            private string type;
            private string tag;

            public string Name
            {
                get
                {
                    return name;
                }
                set
                {
                    name = value;
                }
            }

            public string Type
            {
                get
                {
                    return type;
                }
                set
                {
                    type = value;
                }
            }
            public string TagName
            {
                get
                {
                    return tag;
                }
                set
                {
                    tag = value;
                }
            }
        }

        private void Button_Help_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>Handles the SelectionChanged event of the Lv_ProcessListView control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void Lv_ProcessListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handles Execute button enabling/disabling
            if (lv_ProcessListView.Items.Count == 0)
            {
                Execute.IsEnabled = false;
                Delete.IsEnabled = false;
                Edit.IsEnabled = false;
                Copy.IsEnabled = false;
                ButtonSingleRightArrow.IsEnabled = false;
                ButtonSingleRightArrowImage.Opacity = 0.1;
                ButtonSingleLefttArrow.IsEnabled = false;
                ButtonSingleLefttArrowImage.Opacity = 0.1;
                OnOrOff.IsEnabled = false;
                deleteFromQuestionSetting.IsEnabled = false;
                ProcessListViewNoItems.Visibility = Visibility.Visible;
            }
            if (lv_ProcessListView.SelectedItems.Count > 1)//multi selection
            {
                Edit.IsEnabled = false;
                Copy.IsEnabled = false;
                ButtonSingleRightArrow.IsEnabled = false;
                ButtonSingleRightArrowImage.Opacity = 0.1;
                ButtonSingleLefttArrow.IsEnabled = false;
                ButtonSingleLefttArrowImage.Opacity = 0.1;
            }
            else
            {
                if (lv_ProcessListView.SelectedIndex >= 0)
                {
                    // Handles Right arrow button enabling/disabling
                    if (lv_ProcessListView.SelectedIndex == 0)
                    {
                        ButtonSingleRightArrow.IsEnabled = false;
                        ButtonSingleRightArrowImage.Opacity = 0.1;
                    }
                    else
                    {
                        ButtonSingleRightArrow.IsEnabled = true;
                        ButtonSingleRightArrowImage.Opacity = 1;
                    }

                    // Handles Left arrow button enabling/disabling
                    if (lv_ProcessListView.SelectedIndex == DataProcessListView.Count - 1)
                    {
                        ButtonSingleLefttArrow.IsEnabled = false;
                        ButtonSingleLefttArrowImage.Opacity = 0.1;
                    }
                    else
                    {
                        ButtonSingleLefttArrow.IsEnabled = true;
                        ButtonSingleLefttArrowImage.Opacity = 1;
                    }
                }
            }
            if (lv_ProcessListView.SelectedItems.Count == 1)
            {
                Edit.IsEnabled = true;
                Copy.IsEnabled = true;
                Delete.IsEnabled = true;
                OnOrOff.IsEnabled = true;
                deleteFromQuestionSetting.IsEnabled = true;
            }

        }

        private void Create_Process_Click(object sender, RoutedEventArgs e)
        {
            ProcessMethod selectedProcessMethod = new ProcessMethod();
            selectedProcessMethod = (List_DisposalMethod.SelectedItem) as ProcessMethod;
            try
            {
                if (selectedProcessMethod != null)
                {
                    int readRow = -1;
                    int writeRow = 5;
                    if (DataProcessListView.Count == 0)
                    {
                        writeRow = 5;
                    }
                    else
                    {
                        if (DataProcessListView[DataProcessListView.Count - 1].Repeats != null) // checks whether the last row is repeat recode process. If yes, adds rownumber by one(NEXT row is added).
                        {
                            writeRow = DataProcessListView[DataProcessListView.Count - 1].RowNo + 1 + 1;
                        }
                        else
                        {
                            writeRow = DataProcessListView[DataProcessListView.Count - 1].RowNo + 1;
                        }
                    }

                    switch (selectedProcessMethod.TagName)
                    {
                        case QC4Common.Common.Constants.DP.SubstituteOperatorRECODE:
                            RECODE recodeWindow = new RECODE(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Create, null);
                            try
                            {
                                this.Hide();

                                recodeWindow.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(recodeWindow.isModifiedProcess);
                            }
                            break;

                        case "GROUP":
                            Group groupWindow = new Group(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Create);
                            try
                            {
                                this.Hide();
                                groupWindow.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(groupWindow.isModifiedProcess);
                            }
                            break;

                        case QC4Common.Common.Constants.DP.SubstituteOperatorINTEGRATE:
                            Integrate Integrate = new Integrate(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Create);
                            try
                            {
                                this.Hide();
                                Integrate.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(Integrate.isModifiedProcess);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.SubstituteOperatorMTOS:

                            MTOS mtosWindow = new MTOS(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Create);
                            try
                            {
                                this.Hide();
                                mtosWindow.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(mtosWindow.isModifiedProcess);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.SubstituteOperatorADD_MINUS:
                            ReviseData reviseData = new ReviseData(Workbook, readRow, writeRow, Util.Constants.ProcessingType.ReviseData, QC4Common.Common.Constants.STD_DP.Process_Create);
                            try
                            {
                                this.Hide();
                                reviseData.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(reviseData.isModifiedProcess);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.SubstituteOperatorMCONVERT:
                            MConvert mconvert = new MConvert(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Create);
                            try
                            {

                                this.Hide();
                                mconvert.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(mconvert.isModifiedProcess);
                            }
                            break;


                        case QC4Common.Common.Constants.DP.SubstituteOperatorCLASS:
                            Class classWindow = new Class(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Create);
                            try
                            {
                                this.Hide();

                                classWindow.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(classWindow.isModifiedProcess);

                            }
                            break;
                        case QC4Common.Common.Constants.DP.SubstituteOperatorCOMPUTE:
                            Compute cmpt = new Compute(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Create);
                            try
                            {
                                this.Hide();
                                cmpt.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(cmpt.isModifiedProcess);

                            }
                            break;
                        case QC4Common.Common.Constants.DP.InstructionOMIT:
                            Exclude exclude = new Exclude(Workbook, readRow, writeRow, Util.Constants.ProcessingType.Exclude, QC4Common.Common.Constants.STD_DP.Process_Create);
                            try
                            {

                                // this.Hide();
                                this.Hide();
                                exclude.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(exclude.isModifiedProcess);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.SubstituteOperatorCOUNT:
                            COUNT countWindow = new COUNT(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Create);
                            try
                            {
                                this.Hide();
                                countWindow.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(countWindow.isModifiedProcess);

                            }
                            break;
                        case "ADD":
                            ADD cmptadd = new ADD(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Create);

                            try
                            {
                                this.Hide();
                                cmptadd.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(cmptadd.isModifiedProcess);
                            }
                            break;

                        case QC4Common.Common.Constants.DP.SubstituteOperatorDELETE:
                            DeleteCases deleteCases = new DeleteCases(Workbook, readRow, writeRow, Util.Constants.ProcessingType.DeleteCases, QC4Common.Common.Constants.STD_DP.Process_Create);
                            try
                            {
                                this.Hide();
                                deleteCases.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(deleteCases.isModifiedProcess);
                            }
                            break;

                        case QC4Common.Common.Constants.DP.InstructionLISTUP:
                            OutputList outputlist = new OutputList(Workbook, readRow, writeRow, Util.Constants.ProcessingType.OutputList, QC4Common.Common.Constants.STD_DP.Process_Create);
                            try
                            {

                                this.Hide();
                                outputlist.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(outputlist.isModifiedProcess);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.SubstituteOperatorJOINT:
                            Joint joint = new Joint(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Create);
                            try
                            {
                                this.Hide();
                                joint.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(joint.isModifiedProcess);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                string exception = ex.Message;
            }
        }

        private void Lv_ProcessListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            var originalSource = (DependencyObject)e.OriginalSource;
            while ((originalSource != null) && !(originalSource is System.Windows.Controls.DataGridRow)) originalSource = VisualTreeHelper.GetParent(originalSource);
            if (originalSource == null) return;
            while ((originalSource != null) && (originalSource is System.Windows.Shapes.Rectangle))
            {
                return;
            }
            if (originalSource == null)
            {
                return;
            }
            DataProcessList dataProcess = new DataProcessList();
            dataProcess = (lv_ProcessListView.SelectedItem) as DataProcessList;

            try
            {
                if (dataProcess != null)
                {
                    int readRow = dataProcess.RowNo;
                    int writeRow = dataProcess.RowNo;
                    bool isRepeatCase = false;
                    switch (dataProcess.ProcessingMethodType)
                    {
                        case QC4Common.Common.Constants.DP.SubstituteOperatorRECODE:
                            if (dataProcess.Repeats != null)
                            {
                                writeRow = writeRow - 1;
                                isRepeatCase = true;
                            }
                            RECODE recodeWindow = new RECODE(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Edit, dataProcess, isRepeatCase);
                            try
                            {
                                this.Hide();

                                recodeWindow.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(recodeWindow.isModifiedProcess, true);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.SubstituteOperatorAVG:
                        case QC4Common.Common.Constants.DP.SubstituteOperatorMAX:
                        case QC4Common.Common.Constants.DP.SubstituteOperatorMIN:
                        case QC4Common.Common.Constants.DP.SubstituteOperatorSUM:
                            Group groupWindow = new Group(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Edit);
                            try
                            {
                                this.Hide();
                                groupWindow.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(groupWindow.isModifiedProcess, true);
                            }
                            break;

                        case QC4Common.Common.Constants.DP.SubstituteOperatorADD_MINUS:
                            ReviseData reviseData = new ReviseData(Workbook, readRow, writeRow, Util.Constants.ProcessingType.ReviseData, QC4Common.Common.Constants.STD_DP.Process_Edit);
                            try
                            {
                                this.Hide();
                                reviseData.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(reviseData.isModifiedProcess, true);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.SubstituteOperatorINTEGRATE:
                            Integrate integrate = new Integrate(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Edit);
                            try
                            {

                                this.Hide();
                                integrate.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(integrate.isModifiedProcess, true);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.SubstituteOperatorMCONVERT:
                            MConvert mconvert = new MConvert(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Edit);
                            try
                            {

                                this.Hide();
                                mconvert.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(mconvert.isModifiedProcess, true);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.SubstituteOperatorMTOS:

                            MTOS mtosWindow = new MTOS(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Edit);
                            try
                            {
                                this.Hide();
                                mtosWindow.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {

                                this.Show();
                                UpdateDP_Main(mtosWindow.isModifiedProcess, true);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.SubstituteOperatorCOMPUTE:

                            Compute comp = new Compute(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Edit);
                            try
                            {

                                this.Hide();
                                comp.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {

                                this.Show();
                                UpdateDP_Main(comp.isModifiedProcess, true);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.SubstituteOperatorCLASS:
                            Class classWindow = new Class(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Edit);
                            try
                            {
                                this.Hide();
                                classWindow.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(classWindow.isModifiedProcess, true);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.InstructionOMIT:
                            Exclude exclude = new Exclude(Workbook, readRow, writeRow, Util.Constants.ProcessingType.Exclude, QC4Common.Common.Constants.STD_DP.Process_Edit);
                            try
                            {
                                this.Hide();
                                exclude.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(exclude.isModifiedProcess, true);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.InstructionLISTUP:
                            OutputList outputlist = new OutputList(Workbook, readRow, writeRow, Util.Constants.ProcessingType.OutputList, QC4Common.Common.Constants.STD_DP.Process_Edit);
                            try
                            {
                                this.Hide();
                                outputlist.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(outputlist.isModifiedProcess);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.SubstituteOperatorJOINT:
                            Joint joint = new Joint(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Edit);
                            try
                            {

                                this.Hide();
                                joint.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(joint.isModifiedProcess, true);
                            }
                            break;
                        case Util.Constants.ProcessingMethod.ADD:
                            ADD add = new ADD(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Edit);
                            try
                            {
                                this.Hide();

                                add.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(add.isModifiedProcess, true);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.SubstituteOperatorCOUNT:
                            COUNT countWindow = new COUNT(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Edit);
                            try
                            {
                                this.Hide();
                                countWindow.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(countWindow.isModifiedProcess, true);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.SubstituteOperatorDELETE:

                            DeleteCases deleteCasesWindow = new DeleteCases(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Edit);
                            try
                            {

                                this.Hide();
                                deleteCasesWindow.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {

                                this.Show();
                                UpdateDP_Main(deleteCasesWindow.isModifiedProcess, true);
                            }
                            break;
                        default:
                            break;
                    }

                }

            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                string exception = ex.Message;
            }
        }

        private void List_DisposalMethod_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ProcessMethod selectedProcessMethod = new ProcessMethod();
            selectedProcessMethod = (List_DisposalMethod.SelectedItem) as ProcessMethod;
            try
            {
                if (selectedProcessMethod != null)
                {
                    int readRow = -1;
                    int writeRow = 5;
                    if (DataProcessListView.Count == 0)
                    {
                        writeRow = 5;
                    }
                    else
                    {
                        if (DataProcessListView[DataProcessListView.Count - 1].Repeats != null) // checks whether the last row is repeat recode process. If yes, adds rownumber by one(NEXT row is added).
                        {
                            writeRow = DataProcessListView[DataProcessListView.Count - 1].RowNo + 1 + 1;
                        }
                        else
                        {
                            writeRow = DataProcessListView[DataProcessListView.Count - 1].RowNo + 1;
                        }
                    }
                    switch (selectedProcessMethod.TagName)
                    {
                        case QC4Common.Common.Constants.DP.SubstituteOperatorRECODE:
                            RECODE recodeWindow = new RECODE(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Create);
                            try
                            {
                                this.Hide();

                                recodeWindow.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(recodeWindow.isModifiedProcess);

                            }
                            break;

                        case "GROUP":
                            Group groupWindow = new Group(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Create);
                            try
                            {
                                this.Hide();
                                groupWindow.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(groupWindow.isModifiedProcess);

                            }
                            break;

                        case QC4Common.Common.Constants.DP.SubstituteOperatorINTEGRATE:
                            Integrate integrate = new Integrate(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Create);
                            try
                            {

                                this.Hide();
                                integrate.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(integrate.isModifiedProcess);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.SubstituteOperatorMTOS:
                            MTOS mtosWindow = new MTOS(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Create);
                            this.Hide();
                            try
                            {

                                mtosWindow.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(mtosWindow.isModifiedProcess);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.SubstituteOperatorADD_MINUS:
                            ReviseData reviseData = new ReviseData(Workbook, readRow, writeRow, Util.Constants.ProcessingType.ReviseData, QC4Common.Common.Constants.STD_DP.Process_Create);
                            try
                            {
                                this.Hide();
                                reviseData.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(reviseData.isModifiedProcess);
                            }
                            break;

                        case QC4Common.Common.Constants.DP.SubstituteOperatorCLASS:
                            Class classWindow = new Class(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Create);
                            try
                            {
                                this.Hide();
                                classWindow.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(classWindow.isModifiedProcess);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.SubstituteOperatorMCONVERT:
                            MConvert mconvert = new MConvert(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Create);
                            try
                            {

                                this.Hide();
                                mconvert.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(mconvert.isModifiedProcess);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.SubstituteOperatorCOMPUTE:
                            Compute cmpt = new Compute(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Create);
                            try
                            {
                                this.Hide();
                                cmpt.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(cmpt.isModifiedProcess);

                            }
                            break;

                        case QC4Common.Common.Constants.DP.InstructionOMIT:
                            Exclude exclude = new Exclude(Workbook, readRow, writeRow, Util.Constants.ProcessingType.Exclude, QC4Common.Common.Constants.STD_DP.Process_Create);
                            try
                            {

                                this.Hide();
                                exclude.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(exclude.isModifiedProcess);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.SubstituteOperatorCOUNT:
                            COUNT countWindow = new COUNT(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Create);
                            try
                            {
                                this.Hide();
                                countWindow.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(countWindow.isModifiedProcess);

                            }
                            break;

                        case "ADD":
                            ADD add = new ADD(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Create);
                            try
                            {
                                this.Hide();
                                add.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(add.isModifiedProcess);

                            }
                            break;

                        case QC4Common.Common.Constants.DP.SubstituteOperatorDELETE:
                            DeleteCases deleteCases = new DeleteCases(Workbook, readRow, writeRow, Util.Constants.ProcessingType.DeleteCases, QC4Common.Common.Constants.STD_DP.Process_Create);
                            try
                            {
                                this.Hide();
                                deleteCases.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(deleteCases.isModifiedProcess);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.InstructionLISTUP:
                            OutputList outputlist = new OutputList(Workbook, readRow, writeRow, Util.Constants.ProcessingType.OutputList, QC4Common.Common.Constants.STD_DP.Process_Create);
                            try
                            {
                                this.Hide();
                                outputlist.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(outputlist.isModifiedProcess);
                            }
                            break;
                        case QC4Common.Common.Constants.DP.SubstituteOperatorJOINT:
                            Joint joint = new Joint(Workbook, readRow, writeRow, Util.Constants.ProcessingType.CreateNewVariable, QC4Common.Common.Constants.STD_DP.Process_Create);
                            try
                            {

                                this.Hide();
                                joint.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                string exs = ex.Message;
                            }
                            finally
                            {
                                this.Show();
                                UpdateDP_Main(joint.isModifiedProcess);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                string exception = ex.Message;
            }

        }

        private void UpdateDP_Main(bool isSaved, bool isEdit = false)
        {
            int currentSelectedIndex = lv_ProcessListView.SelectedIndex;
            if (isSaved)
            {
                dataProcessList = new ObservableCollection<DataProcessList>();
                GetData(); // Update dataprocess list.
                lv_ProcessListView.ItemsSource = DataProcessListView;
            }
            if (isSaved && !isEdit) //Not required when edit process is saved
            {
                if (DataProcessListView.Count == 1)
                {
                    lv_ProcessListView.SelectedItem = DataProcessListView.First();
                }
                else if (DataProcessListView.Count > 1)
                {
                    lv_ProcessListView.SelectedItem = DataProcessListView.LastOrDefault();
                }
            }
            if (isSaved && isEdit)
            {
                lv_ProcessListView.SelectedIndex = currentSelectedIndex;
            }

            if (this.isModified)
            {
                this.isModified = true;

            }
            else
            {
                this.isModified = isSaved;
            }
            if (lv_ProcessListView.SelectedItem != null)
            {
                lv_ProcessListView.ScrollIntoView(lv_ProcessListView.SelectedItem);
            }

        }

        private void Button_Help_Click_1(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(QC4Common.Classes.Help.GetHelpLink(QC4Common.Common.Constants.HelpButtonType.DATAPROCESSING));
        }

        private void DeleteFromQuestionSetting_Click(object sender, RoutedEventArgs e)
        {
        }

        private void CheckList_Click(object sender, RoutedEventArgs e)
        {
            SaveCheckListValue();
        }

        private void SaveCheckListValue()
        {
            excel.Worksheet settingssheet = Util.ExcelUtil.GetWorkSheetByCodeName(Workbook, Qc4Launcher.Util.Constants.SheetCodeName.Setting);
            excel.Range dpcell = settingssheet.Cells[QC4Common.Common.Constants.STD_DP.CheckList_Row, QC4Common.Common.Constants.STD_DP.CheckList_Column];
            if (checkList.IsChecked == true)
            {
                dpcell.Value = QC4Common.Common.Constants.STD_DP.CheckList_ON;
            }
            else
            {
                dpcell.Value = QC4Common.Common.Constants.STD_DP.CheckList_OFF;
            }
        }

        private void Execute_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var found = DataProcessListView.Any(u => (u.OnOrOff == LocalResource.CELL_ON_EN));
                int rowNo = -1;
                if (found)
                {
                    if (CheckBeforeExecute(out rowNo))
                    {
                        try
                        {
                            QC4Common.DP.CallDP calldp = new QC4Common.DP.CallDP();
                            /*isExecuted = true;*/
                            SaveCheckListValue();
                            MainWindow.ismultivariate = MainWindow.ismultivariate && QC4Common.Common.CommonFlag.IsMultivariateUpdated(Workbook);
                            QC4Common.Common.CommonFlag.SetIsMultivariateUpdated(Workbook, true);
                            CloseDPMAin();
                            calldp.DPExecute(Workbook.Application.ActiveWorkbook, true);
                        }
                        catch (Exception ex)
                        {
                            _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                        }
                    }
                    else
                    {
                        // Set red color for invalid entry.
                        int row = -1;
                        if ((DataProcessListView.First(x => x.RowNo == rowNo) as DataProcessList) != null)
                        {
                            row = (DataProcessListView.First(x => x.RowNo == rowNo) as DataProcessList).DataProcessIndex;
                            DataProcessListView[row].Invalid = true;
                        }
                        MessageDialog.ErrorOk(LocalResource.ERR_EXECUTE_DATA_PROCESSING);
                    }
                }
                else
                {
                    MessageDialog.ErrorOk(LocalResource.ERR_MSG_NO_EXECUTABLE_ENTRIES);
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }


        }
        private void CloseDPMAin()
        {
            this.Close();
        }

        private void ExecuteDP()
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            excel.Worksheet settingssheet = Util.ExcelUtil.GetWorkSheetByCodeName(Workbook, Qc4Launcher.Util.Constants.SheetCodeName.Setting);
            excel.Range dpcell = settingssheet.Cells[QC4Common.Common.Constants.STD_DP.Execute_Row, QC4Common.Common.Constants.STD_DP.Execute_Column];
            dpcell.Value = QC4Common.Common.Constants.STD_DP.Execute_KeyWord;
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
        }
        static void MyCallBack(IAsyncResult async)

        {

            System.Runtime.Remoting.Messaging.AsyncResult ar = (System.Runtime.Remoting.Messaging.AsyncResult)async;

            dpmainclosedelegate del = (dpmainclosedelegate)ar.AsyncDelegate;

            del.EndInvoke(async);

        }
        private void Lv_ProcessListView_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void Lv_ProcessListView_CurrentCellChanged(object sender, EventArgs e)
        {
            var dg = (System.Windows.Controls.DataGrid)sender;
            ExpGrid = dg;
            dg.Focus();
        }

        private void DP_Closed(object sender, EventArgs e)
        {
            MainWindow.isExecuteClicked = isExecuted;
            MainWindow_.Show();
        }

        private void deleteFromQuestionSetting_Check(object sender, RoutedEventArgs e)
        {
            bool checkvalue = false;
            if (deleteFromQuestionSetting.IsChecked == true)
            { checkvalue = true; }
            else if (deleteFromQuestionSetting.IsChecked == false)
            { checkvalue = false; }
            frmutil.SetCellValueInSettings(QC4Common.Common.Constants.STD_DP.DP_Main_Delete_Row, QC4Common.Common.Constants.STD_DP.DP_Main_Delete_Column, Workbook, checkvalue.ToString());
        }

        private void checkList_Check(object sender, RoutedEventArgs e)
        {
            bool checkvalue = false;
            if (checkList.IsChecked == true)
            { checkvalue = true; }
            else if (checkList.IsChecked == false)
            { checkvalue = false; }
            frmutil.SetCellValueInSettings(QC4Common.Common.Constants.STD_DP.DP_Main_CheckList_Row, QC4Common.Common.Constants.STD_DP.DP_Main_CheckList_Column, Workbook, checkvalue.ToString());

        }
        private void Set_Delete_Checklist_Value()
        {
            string deletecheckstatus = frmutil.GetCellValueFromSettings(QC4Common.Common.Constants.STD_DP.DP_Main_Delete_Row, QC4Common.Common.Constants.STD_DP.DP_Main_Delete_Column, Workbook);
            string checklistcheckstatus = frmutil.GetCellValueFromSettings(QC4Common.Common.Constants.STD_DP.DP_Main_CheckList_Row, QC4Common.Common.Constants.STD_DP.DP_Main_CheckList_Column, Workbook);
            deleteFromQuestionSetting.IsChecked = frmutil.GetBoolValueFromString(deletecheckstatus);
            checkList.IsChecked = frmutil.GetBoolValueFromString(checklistcheckstatus);
        }

        private void Button_Help_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                create_Process.Focus();
                e.Handled = true;
            }
        }

        private void Lv_ProcessListView_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                create_Process.Focus();
                e.Handled = true;
            }
            else
                QC4Common.Util.GridCommonFunc.Arrow(sender, e);
        }

        private void DP_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
        #region Handling Datagrid events
        System.Windows.Controls.DataGrid ExpGrid = null;

        private void b1SetColor(object sender, MouseButtonEventArgs e)
        {
            if (ExpGrid != null)
                ExpGrid.Focus();
        }
        #endregion
        public bool CheckBeforeExecute(out int rowNumber)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            Microsoft.Office.Interop.Excel.Worksheet dpSettingSheet = Util.ExcelUtil.GetWorkSheetByCodeName(Workbook, Qc4Launcher.Util.Constants.SheetCodeName.DataProcessS);
            Excel.Range dpstart = dpSettingSheet.Cells[Constants.DP.ProUIstartRow, Constants.DP.OnOffColumn];
            Excel.Range lastcell = ExcelUtil.EndxlUp(dpSettingSheet.Cells[Constants.DP.ProUIstartRow, Constants.DP.OnOffColumn]);
            Excel.Range rar = dpSettingSheet.Range[dpstart, lastcell];
            int forCount = 0;
            bool isListup = false;
            string digit = string.Empty;
            int? repeats = null;
            DataProcessHelper dbHelper = new DataProcessHelper();
            excel.Worksheet settingsSheet = Util.ExcelUtil.GetWorkSheetByCodeName(Workbook, Qc4Launcher.Util.Constants.SheetCodeName.Setting);
            excel.Range listupCell = settingsSheet.Cells[QC4Common.Common.Constants.STD_DP.isListUP_Row, QC4Common.Common.Constants.STD_DP.isListUP_Col];
            listupCell.Value = false;
            foreach (Excel.Range cell in rar.Cells)
            {
                try
                {
                    if (!string.IsNullOrEmpty(cell.Text))
                    {
                        Excel.Range start = dpSettingSheet.Cells[cell.Row, cell.Column];
                        Excel.Range end = dbHelper.GetLastCellInRow(start);
                        Excel.Range range = dpSettingSheet.get_Range(start, end);
                        var obj = range.Value;
                        int itemsCount = obj.Length;
                        string then_Operator = string.Empty;
                        var obj1 = listupCell.Value;

                        if ((obj[1, 1] == AddinResource.CELL_ON) || (obj[1, 1] == LocalResource.CELL_ON))
                        {
                            if (obj[1, 6] != null)
                            {
                                var criVariable = obj[1, 3];
                                var instrn = obj[1, 6];
                                string newvariable = string.Empty;
                                switch (instrn)
                                {
                                    case QC4Common.Common.Constants.DP.InstructionLISTUP:
                                        listupCell.Value = true;
                                        isListup = true;
                                        if (criVariable != null)
                                        {
                                            if (!CheckCriteriaByRow(obj))
                                            {
                                                rowNumber = cell.Row;
                                                return false;
                                            }
                                        }
                                        for (int i = 9; i <= itemsCount; i++)
                                        {
                                            if (obj[1, i] != null && !PopulatedDictionary.ContainsKey(obj[1, i]))
                                            {
                                                rowNumber = cell.Row;
                                                return false;
                                            }
                                        }
                                        break;
                                    case QC4Common.Common.Constants.DP.InstructionOMIT:
                                        if (criVariable != null)
                                        {
                                            if (!CheckCriteriaByRow(obj))
                                            {
                                                rowNumber = cell.Row;
                                                return false;
                                            }
                                        }
                                        for (int i = 9; i <= itemsCount; i++)
                                        {
                                            if (obj[1, i] != null && !PopulatedDictionary.ContainsKey(obj[1, i]))
                                            {
                                                rowNumber = cell.Row;
                                                return false;
                                            }
                                        }
                                        break;
                                    case QC4Common.Common.Constants.DP.InstructionDELETE:
                                    case QC4Common.Common.Constants.DP.InstructionAND:
                                    case QC4Common.Common.Constants.DP.InstructionOR:
                                        if (criVariable != null)
                                        {
                                            if (!CheckCriteriaByRow(obj))
                                            {
                                                rowNumber = cell.Row;
                                                return false;
                                            }
                                        }
                                        break;
                                    case QC4Common.Common.Constants.DP.InstructionLDEL:
                                        int ldelValue = int.Parse(obj[1, 9]);
                                        bool isValue = false;
                                        Excel.Worksheet ldelSheet = Util.ExcelUtil.GetWorkSheetByCodeName(Workbook, Qc4Launcher.Util.Constants.SheetCodeName.LDEL);
                                        Excel.Range firstParam = ldelSheet.Cells[2, 2];
                                        Excel.Range lastParam = dbHelper.GetLastCellInRow(firstParam);
                                        Excel.Range rangeLDEL = ldelSheet.Range[firstParam, lastParam];
                                        foreach (Excel.Range param in rangeLDEL.Cells)
                                        {
                                            int val = 0;
                                            if (int.TryParse(param.Text, out val) && int.Parse(param.Text) == ldelValue && param.Column > 1)
                                                isValue = true;
                                        }
                                        if (!isValue)
                                        {
                                            rowNumber = cell.Row;
                                            return false;
                                        }
                                        break;
                                    case QC4Common.Common.Constants.DP.InstructionFOR:
                                        forCount = 1;
                                        repeats = int.Parse(obj[1, 10]) - int.Parse(obj[1, 9]) + 1;
                                        digit = obj[1, 9];
                                        break;
                                    case QC4Common.Common.Constants.DP.InstructionNEXT:
                                        forCount = 0;
                                        break;
                                    case QC4Common.Common.Constants.DP.InstructionTHEN:
                                        //obj1,8 here for instructions
                                        if (itemsCount >= 8)
                                        {
                                            then_Operator = obj[1, 8];
                                        }
                                        switch (then_Operator)
                                        {
                                            case QC4Common.Common.Constants.DP.SubstituteOperatorRECODE:
                                                if (forCount == 1)
                                                {
                                                    QuestionSettings qs1 = new QuestionSettings();
                                                    string repeatVarible = obj[1, 7];
                                                    var variable = obj[1, 7];
                                                    for (int i = int.Parse(digit); i <= repeats; i++)
                                                    {
                                                        variable = (repeatVarible.Remove(repeatVarible.Length - 3) + i);
                                                        bool hasvalue = Util.Definiotion.VariableDictionary.TryGetValue(variable, out qs1);
                                                        if (hasvalue)
                                                        {
                                                            int countRepeat = 0;
                                                            int choiceCount = qs1.Choices.Count;
                                                            if (qs1.AnswerType == "SA" || qs1.AnswerType == "MA")
                                                            {
                                                                for (int j = 10; j <= itemsCount; j++)
                                                                {
                                                                    if (obj[1, j] != null)
                                                                    {
                                                                        countRepeat++;
                                                                    }
                                                                    else
                                                                    {
                                                                        break;
                                                                    }
                                                                }

                                                                if (choiceCount != countRepeat)
                                                                {
                                                                    rowNumber = cell.Row;
                                                                    return false;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                rowNumber = cell.Row;
                                                                return false;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            rowNumber = cell.Row;
                                                            return false;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (PopulatedDictionary.ContainsKey(obj[1, 7]))
                                                    {
                                                        var variable = PopulatedDictionary[obj[1, 7]];
                                                        int countRecode = 0;
                                                        int choiceCount = PopulatedDictionary[obj[1, 7]].Choices.Count;
                                                        if (variable.AnswerType == "SA" || variable.AnswerType == "MA")
                                                        {
                                                            for (int i = 10; i <= itemsCount; i++)
                                                            {
                                                                if (obj[1, i] != null)
                                                                {
                                                                    countRecode++;
                                                                }
                                                                else
                                                                {
                                                                    break;
                                                                }
                                                            }

                                                            if (choiceCount != countRecode)
                                                            {
                                                                rowNumber = cell.Row;
                                                                return false;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            rowNumber = cell.Row;
                                                            return false;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        rowNumber = cell.Row;
                                                        return false;
                                                    }
                                                }


                                                break;
                                            case QC4Common.Common.Constants.DP.SubstituteOperatorCLASS:
                                                if ((PopulatedDictionary.ContainsKey(obj[1, 7])))
                                                {
                                                    var variable = PopulatedDictionary[obj[1, 7]];
                                                    int countClass = 0;
                                                    int choiceCount = PopulatedDictionary[obj[1, 7]].Choices.Count;
                                                    if (variable.AnswerType == "SA")
                                                    {
                                                        for (int i = 12; i <= itemsCount; i++)
                                                        {
                                                            if (obj[1, i] != null)
                                                            {
                                                                countClass++;
                                                            }
                                                            else
                                                            {
                                                                break;
                                                            }
                                                        }

                                                        if (choiceCount != countClass)
                                                        {
                                                            rowNumber = cell.Row;
                                                            return false;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        rowNumber = cell.Row;
                                                        return false;
                                                    }
                                                }
                                                else
                                                {
                                                    rowNumber = cell.Row;
                                                    return false;
                                                }
                                                break;
                                            case QC4Common.Common.Constants.DP.SubstituteOperatorCOUNT:
                                                if ((PopulatedDictionary.ContainsKey(obj[1, 7])))
                                                {
                                                    var variable = PopulatedDictionary[obj[1, 7]];
                                                    int count1 = 0;
                                                    int choiceCount = PopulatedDictionary[obj[1, 7]].Choices.Count;
                                                    if (variable.AnswerType == "SA" || variable.AnswerType == "N")
                                                    {
                                                        for (int i = 11; i <= itemsCount; i++)
                                                        {
                                                            if (obj[1, i] != null)
                                                            {
                                                                count1++;
                                                            }
                                                            else
                                                            {
                                                                break;
                                                            }
                                                        }

                                                        if (choiceCount != count1)
                                                        {
                                                            rowNumber = cell.Row;
                                                            return false;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    rowNumber = cell.Row;
                                                    return false;
                                                }
                                                break;
                                            case QC4Common.Common.Constants.DP.SubstituteOperatorINTEGRATE:
                                                rowNumber = cell.Row;
                                                newvariable = Convert.ToString(obj[1, QC4Common.Common.Constants.STD_DP.NewvariableColumn]);
                                                if (string.IsNullOrEmpty(newvariable))// == null || param1.Value == "")
                                                {

                                                    return false;
                                                }
                                                else if (!PopulatedDictionary.ContainsKey(newvariable) || (Util.Definiotion.VariableDictionary[newvariable].AnswerType != QC4Common.Common.Constants.AnswerType.MA && Util.Definiotion.VariableDictionary[newvariable].AnswerType != QC4Common.Common.Constants.AnswerType.SA))// == null || param1.Value == "")
                                                {

                                                    return false;
                                                }
                                                int paramcount = 0;
                                                if (!int.TryParse(Convert.ToString(obj[1, QC4Common.Common.Constants.STD_DP.SubstituteOperatorColumn]), out paramcount))
                                                {

                                                    return false;
                                                }
                                                if (paramcount == 0)
                                                {

                                                    return false;
                                                }
                                                int count = QC4Common.Common.Constants.STD_DP.SubstituteOperatorColumn + (paramcount * 2);
                                                string[] choicevalues = Convert.ToString(obj[1, count]).Split(';');
                                                int ci = 0;
                                                for (int i = QC4Common.Common.Constants.STD_DP.SubstituteParam1Column; i <= count; i++)
                                                {
                                                    string svariablename = Convert.ToString(obj[1, i]);
                                                    if (i % 2 != 0)
                                                    {
                                                        if (!PopulatedDictionary.ContainsKey(svariablename) || (Util.Definiotion.VariableDictionary[svariablename].AnswerType != QC4Common.Common.Constants.AnswerType.N && Util.Definiotion.VariableDictionary[svariablename].AnswerType != QC4Common.Common.Constants.AnswerType.MA && Util.Definiotion.VariableDictionary[svariablename].AnswerType != QC4Common.Common.Constants.AnswerType.SA))// == null || param1.Value == "")
                                                        {

                                                            return false;
                                                        }
                                                        else if (ci < choicevalues.Length && (Util.Definiotion.VariableDictionary[svariablename].AnswerType == QC4Common.Common.Constants.AnswerType.SA || Util.Definiotion.VariableDictionary[svariablename].AnswerType == QC4Common.Common.Constants.AnswerType.MA))
                                                        {
                                                            int sval = 0;
                                                            if (!string.IsNullOrEmpty(Convert.ToString(choicevalues[ci])) && int.TryParse(Convert.ToString(choicevalues[ci]), out sval))
                                                            {
                                                                if (Util.Definiotion.VariableDictionary[svariablename].Choices.Count < sval)
                                                                {
                                                                    return false;
                                                                }
                                                            }
                                                        }

                                                        ci++;
                                                    }
                                                }

                                                break;
                                            case QC4Common.Common.Constants.DP.SubstituteOperatorJOINT:

                                                rowNumber = cell.Row;
                                                newvariable = Convert.ToString(obj[1, QC4Common.Common.Constants.STD_DP.NewvariableColumn]);
                                                if (string.IsNullOrEmpty(newvariable))
                                                {
                                                    return false;
                                                }
                                                else if (!PopulatedDictionary.ContainsKey(newvariable) || (Util.Definiotion.VariableDictionary[newvariable].AnswerType != QC4Common.Common.Constants.AnswerType.MA))
                                                {
                                                    return false;
                                                }
                                                List<string> paramvalues = new List<string>();
                                                for (int i = QC4Common.Common.Constants.STD_DP.SubstituteVariableColumn; i <= itemsCount; i += 2)// QC4Common.Common.Constants.STD_DP.SubstituteVariableColumn + QC4Common.Common.Constants.STD_DP.SubstituteVariableColumn
                                                {
                                                    string sourcevariablename = Convert.ToString(obj[1, i]);
                                                    if (i != QC4Common.Common.Constants.STD_DP.SubstituteVariableColumn && string.IsNullOrEmpty(sourcevariablename))
                                                    {
                                                        rowNumber = -1;
                                                        return true;
                                                    }
                                                    if (string.IsNullOrEmpty(sourcevariablename) || !PopulatedDictionary.ContainsKey(sourcevariablename) || ((Util.Definiotion.VariableDictionary[sourcevariablename].AnswerType != QC4Common.Common.Constants.AnswerType.SA) && (Util.Definiotion.VariableDictionary[sourcevariablename].AnswerType != QC4Common.Common.Constants.AnswerType.MA)))
                                                    {
                                                        return false;
                                                    }
                                                    string[] splitvalues = Convert.ToString(obj[1, i + 1]).Split(Convert.ToChar(LocalResource.GRID_LBL_INTEGRATE_SEPERATOR));
                                                    int min = 1;
                                                    int max = Util.Definiotion.VariableDictionary[sourcevariablename].Choices.Count;
                                                    try
                                                    {
                                                        int.TryParse(Convert.ToString(splitvalues[0]), out min);
                                                        int.TryParse(Convert.ToString(splitvalues[1]), out max);
                                                    }
                                                    catch
                                                    {
                                                        return false;
                                                    }
                                                    if (Util.Definiotion.VariableDictionary[sourcevariablename].Choices.Count < min || Util.Definiotion.VariableDictionary[sourcevariablename].Choices.Count < max)
                                                    {
                                                        return false;
                                                    }
                                                    paramvalues.Add(Convert.ToString(obj[1, i + 1]));
                                                }
                                                break;
                                            case QC4Common.Common.Constants.DP.SubstituteOperatorMCONVERT:
                                                rowNumber = cell.Row;
                                                newvariable = Convert.ToString(obj[1, QC4Common.Common.Constants.STD_DP.NewvariableColumn]);
                                                if (string.IsNullOrEmpty(newvariable))
                                                {
                                                    return false;
                                                }
                                                else if (!PopulatedDictionary.ContainsKey(newvariable) || (Util.Definiotion.VariableDictionary[newvariable].AnswerType != QC4Common.Common.Constants.AnswerType.MA))
                                                {
                                                    return false;
                                                }
                                                string newvariabletype = Util.Definiotion.VariableDictionary[newvariable].AnswerType;
                                                int newvariablechoicecount = Util.Definiotion.VariableDictionary[newvariable].Choices.Count;
                                                string[] selectedchoice = Convert.ToString(obj[1, QC4Common.Common.Constants.STD_DP.SubstituteOperatorColumn]).Split(';');
                                                int largestchoiceselected = 0;
                                                int outvalue = 0;
                                                for (int i = 0; i < selectedchoice.Length; i++)
                                                {
                                                    if (!string.IsNullOrEmpty(Convert.ToString(selectedchoice[i])))
                                                    {
                                                        try
                                                        {
                                                            int.TryParse(Convert.ToString(selectedchoice[i]), out outvalue);

                                                            if (largestchoiceselected < outvalue)
                                                            {
                                                                largestchoiceselected = outvalue;
                                                            }

                                                        }
                                                        catch { }
                                                    }
                                                }

                                                for (int i = QC4Common.Common.Constants.STD_DP.SubstituteParam1Column; i <= newvariablechoicecount; i++)
                                                {
                                                    string sourcevarname = Convert.ToString(obj[1, i]);
                                                    if (string.IsNullOrEmpty(sourcevarname) || !PopulatedDictionary.ContainsKey(sourcevarname))
                                                    {
                                                        return false;
                                                    }
                                                    string firstparamsourcevariablename = Convert.ToString(obj[1, QC4Common.Common.Constants.STD_DP.SubstituteParam1Column]);
                                                    if ((Util.Definiotion.VariableDictionary[sourcevarname].Choices.Count < largestchoiceselected) ||
                                                        (Util.Definiotion.VariableDictionary[firstparamsourcevariablename].AnswerType != Util.Definiotion.VariableDictionary[sourcevarname].AnswerType) ||
                                                        (Util.Definiotion.VariableDictionary[firstparamsourcevariablename].Choices.Count != Util.Definiotion.VariableDictionary[sourcevarname].Choices.Count)
                                                        )
                                                    {
                                                        return false;
                                                    }
                                                }
                                                try
                                                {
                                                    int sourcevariablecountinsheet = 0;
                                                    for (int i = QC4Common.Common.Constants.STD_DP.SubstituteParam1Column; i <= itemsCount; i++)// QC4Common.Common.Constants.STD_DP.MAX_DP_COLUMN
                                                    {
                                                        string sourcevarname = Convert.ToString(obj[1, i]);
                                                        if (string.IsNullOrEmpty(sourcevarname))
                                                        {
                                                            break;
                                                        }
                                                        sourcevariablecountinsheet++;
                                                    }
                                                    if (sourcevariablecountinsheet != newvariablechoicecount)
                                                    {
                                                        return false;
                                                    }

                                                }
                                                catch { }
                                                break;
                                            case QC4Common.Common.Constants.DP.SubstituteOperatorMTOS:
                                                if (PopulatedDictionary.ContainsKey(obj[1, 7]))
                                                {
                                                    var MTOSVariable = PopulatedDictionary[obj[1, 7]];
                                                    if (MTOSVariable.AnswerType != "SA") { rowNumber = cell.Row; return false; } else { }

                                                }
                                                else
                                                {
                                                    rowNumber = cell.Row;
                                                    return false;
                                                }
                                                break;
                                            case QC4Common.Common.Constants.DP.SubstituteOperatorCOMPUTE:
                                                if (PopulatedDictionary.ContainsKey(obj[1, 7]))
                                                {
                                                    var computeVariable = PopulatedDictionary[obj[1, 7]];
                                                    if (computeVariable.AnswerType != "N") { rowNumber = cell.Row; return false; }
                                                    else
                                                    {

                                                    }
                                                }
                                                else
                                                {
                                                    rowNumber = cell.Row;
                                                    return false;
                                                }
                                                break;
                                            case QC4Common.Common.Constants.DP.SubstituteOperatorADD:
                                                if (PopulatedDictionary.ContainsKey(obj[1, 7]))
                                                {
                                                    var computeVariable = PopulatedDictionary[obj[1, 7]];
                                                    int ChoiceCount = computeVariable.Choices.Count;
                                                    if (computeVariable.AnswerType != "MA") { rowNumber = cell.Row; return false; } else { }

                                                }
                                                else
                                                {
                                                    rowNumber = cell.Row;
                                                    return false;
                                                }
                                                break;
                                            case QC4Common.Common.Constants.DP.SubstituteOperatorEQUAL:
                                                if (criVariable != null)
                                                {
                                                    if (!CheckCriteriaByRow(obj))
                                                    {
                                                        rowNumber = cell.Row;
                                                        return false;
                                                    }
                                                }
                                                var reviseVariable = PopulatedDictionary[obj[1, 7]];
                                                string reviseValue = obj[1, 9];
                                                if (obj[1, 7] != null && PopulatedDictionary.ContainsKey(obj[1, 7]))
                                                {

                                                    if (!(reviseValue.Equals("DK") || reviseValue.Equals("*")))
                                                    {
                                                        if (reviseVariable.AnswerType == "N")
                                                        {
                                                            if (!float.TryParse(reviseValue, out _) && !PopulatedDictionary.ContainsKey(obj[1, 9]))
                                                            { rowNumber = cell.Row; return false; }
                                                            else if (PopulatedDictionary.ContainsKey(obj[1, 9]) && PopulatedDictionary[obj[1, 9]].AnswerType != "SA" && PopulatedDictionary[obj[1, 9]].AnswerType != "N")
                                                            { rowNumber = cell.Row; return false; }
                                                        }
                                                        else if (reviseVariable.AnswerType == "SA")
                                                        {
                                                            if (int.TryParse(reviseValue, out _) && (int.Parse(reviseValue) > reviseVariable.Choices.Count))
                                                            { rowNumber = cell.Row; return false; }
                                                            else if (!int.TryParse(reviseValue, out _) && !PopulatedDictionary.ContainsKey(obj[1, 9]))
                                                            { rowNumber = cell.Row; return false; }
                                                            else if (PopulatedDictionary.ContainsKey(obj[1, 9]) && PopulatedDictionary[obj[1, 9]].AnswerType != "SA")
                                                            { rowNumber = cell.Row; return false; }
                                                        }
                                                        else if (reviseVariable.AnswerType == "MA")
                                                        {
                                                            if (int.TryParse(reviseValue, out _) && (int.Parse(reviseValue) > reviseVariable.Choices.Count))
                                                            { rowNumber = cell.Row; return false; }
                                                            else if (!int.TryParse(reviseValue, out _) && !PopulatedDictionary.ContainsKey(obj[1, 9]))
                                                            { rowNumber = cell.Row; return false; }
                                                            else if (PopulatedDictionary.ContainsKey(obj[1, 9]) && PopulatedDictionary[obj[1, 9]].AnswerType != "SA" && PopulatedDictionary[obj[1, 9]].AnswerType != "MA")
                                                            { rowNumber = cell.Row; return false; }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    rowNumber = cell.Row;
                                                    return false;
                                                }
                                                break;
                                            case QC4Common.Common.Constants.DP.SubstituteOperatorADD1:
                                            case QC4Common.Common.Constants.DP.SubstituteOperatorADD2:
                                            case QC4Common.Common.Constants.DP.SubstituteOperatorMINUS1:
                                            case QC4Common.Common.Constants.DP.SubstituteOperatorMINUS2:
                                                if (criVariable != null)
                                                {
                                                    if (!CheckCriteriaByRow(obj))
                                                    {
                                                        rowNumber = cell.Row;
                                                        return false;
                                                    }
                                                }
                                                var revisVariable = PopulatedDictionary[obj[1, 7]];
                                                string revisValue = obj[1, 9];
                                                if (revisValue.Equals("DK") || revisValue.Equals("*"))
                                                { rowNumber = cell.Row; return false; }
                                                if (obj[1, 7] != null && PopulatedDictionary.ContainsKey(obj[1, 7]))
                                                {
                                                    if (PopulatedDictionary[obj[1, 7]].AnswerType != "MA")
                                                    { rowNumber = cell.Row; return false; }
                                                    else
                                                    {
                                                        if (int.TryParse(revisValue, out _) && (int.Parse(revisValue) > revisVariable.Choices.Count))
                                                        { rowNumber = cell.Row; return false; }
                                                        else if (!int.TryParse(revisValue, out _) && PopulatedDictionary.ContainsKey(obj[1, 9]) && PopulatedDictionary[obj[1, 9]].AnswerType != "SA" && PopulatedDictionary[obj[1, 9]].AnswerType != "MA")
                                                        { rowNumber = cell.Row; return false; }
                                                        else if (!int.TryParse(revisValue, out _) && !PopulatedDictionary.ContainsKey(obj[1, 9]))
                                                        {
                                                            ReviseData reviseData = new ReviseData();
                                                            if (!reviseData.ValidateFn1(revisValue, revisVariable.Choices.Count) && !reviseData.ValidateFn2(revisValue, revisVariable.Choices.Count))
                                                            { rowNumber = cell.Row; return false; }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    rowNumber = cell.Row;
                                                    return false;
                                                }
                                                break;
                                            case QC4Common.Common.Constants.DP.SubstituteOperatorAVG:
                                            case QC4Common.Common.Constants.DP.SubstituteOperatorMAX:
                                            case QC4Common.Common.Constants.DP.SubstituteOperatorMIN:
                                            case QC4Common.Common.Constants.DP.SubstituteOperatorSUM:
                                                for (int i = 9; i <= itemsCount; i++)
                                                {
                                                    if (obj[1, i] != null)
                                                    {
                                                        if (!PopulatedDictionary.ContainsKey(obj[1, i]))
                                                        {
                                                            rowNumber = cell.Row;
                                                            return false;
                                                        }
                                                        else
                                                        {
                                                            var variable = PopulatedDictionary[obj[1, i]];
                                                            if (variable.AnswerType != "SA" && variable.AnswerType != "N")
                                                            {
                                                                rowNumber = cell.Row;
                                                                return false;
                                                            }
                                                        }
                                                    }
                                                }
                                                if (PopulatedDictionary.ContainsKey(obj[1, 7]))
                                                {
                                                    var variable = PopulatedDictionary[obj[1, 7]];
                                                    if (variable.AnswerType != "N")
                                                    {
                                                        rowNumber = cell.Row;
                                                        return false;
                                                    }
                                                }
                                                else
                                                {
                                                    rowNumber = cell.Row;
                                                    return false;
                                                }
                                                break;
                                        }
                                        break;
                                    default:
                                        listupCell.Value = false;
                                        break;
                                }

                            }
                            else
                            {
                                rowNumber = cell.Row;
                                return false;
                            }

                        }

                    }
                    else
                    {
                        rowNumber = cell.Row;
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                    rowNumber = cell.Row;
                    return false;
                }
            }
            if (!isListup)
            {
                listupCell.Value = false;
            }
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            rowNumber = -1;
            return true;
        }

        private bool CheckCriteriaByRow(object[,] row)
        {
            CriteriaControl criteriaControl = new CriteriaControl();
            string crivariable = row[1, 3] == null ? "" : row[1, 3].ToString();
            string crioperator = row[1, 4] == null ? "" : row[1, 4].ToString();
            string crivalue = row[1, 5] == null ? "" : row[1, 5].ToString();

            if (!string.IsNullOrEmpty(crivariable) && !PopulatedDictionary.ContainsKey(crivariable))//variable in question settings or not?
                return false;
            if (string.IsNullOrEmpty(crivariable))
                return false;
            else if (string.IsNullOrEmpty(crioperator))
                return false;
            else if (string.IsNullOrEmpty(crivalue))
                return false;

            string answerType = PopulatedDictionary[crivariable].AnswerType;
            int catcount = 0;
            if ((PopulatedDictionary[crivariable].AnswerType == Constants.AnswerType.MA || PopulatedDictionary[crivariable].AnswerType == Constants.AnswerType.FA) && (crioperator != "=" && crioperator != "<>"))
                return false;

            if (PopulatedDictionary[crivariable].AnswerType != Constants.AnswerType.FA && crivalue.Contains("<>"))
                return false;

            string err = string.Empty;
            if (!(crivalue.Equals("DK") || crivalue.Equals("*")))
            {
                err = criteriaControl.CheckIfVariable(crivalue, answerType, crioperator);
                if (err != "")
                    return false;

                List<QuestionSettings> questionSettings = new List<QuestionSettings>();
                questionSettings = PopulatedDictionary.Values.ToList();
                if (!questionSettings.Any(q => q.Variable.Equals(crivalue, StringComparison.OrdinalIgnoreCase)))
                {
                    catcount = PopulatedDictionary[crivariable].Choices.Count == 0 ? 0 : Convert.ToInt32(PopulatedDictionary[crivariable].Choices.Count);
                    if (answerType != Constants.AnswerType.N && answerType != Constants.AnswerType.FA)
                    {
                        if (!criteriaControl.CheckRange(crivalue, catcount, answerType, crioperator))
                            return false;
                    }
                    if (!(answerType == QC4Common.Common.Constants.AnswerType.FA))
                    {
                        err = criteriaControl.CheckValue(crivalue, catcount, answerType, crioperator, 1);
                        if (err != "")
                            return false;
                    }
                }

            }
            return true;
        }
    }
}
