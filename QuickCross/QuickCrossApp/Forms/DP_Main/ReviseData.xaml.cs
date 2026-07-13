using Macromill.QCWeb.Tabulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Excel = Microsoft.Office.Interop.Excel;
using QC4Common.Model;
using QC4Common.Util;
using Qc4Launcher.Util;
using static FilterSettingsView.FilterSettingsClass;
using System.Collections.ObjectModel;
using FilterSettingsView;
using ExcelUtil = Qc4Launcher.Util.ExcelUtil;
using System.Text.RegularExpressions;
using Qc4Launcher.Logic;
using System.Data;
using VB = Microsoft.VisualBasic;
using log4net;
using System.Reflection;
using System.Text;
using Vb = Microsoft.VisualBasic;
namespace Qc4Launcher.Forms.DP_Main
{
    /// <summary>
    /// Interaction logic for ReviseData.xaml
    /// </summary>
    public partial class ReviseData : Window
    {
        public static Excel.Workbook workbook;
        private static List<String> revisionMethods;
        private Dictionary<String, QuestionSettings> dictionary = new Dictionary<string, QuestionSettings>();
        private List<DataExport> dataFromSheet = new List<DataExport>();
        private List<DataExport> dataFromListSheet = new List<DataExport>();
        private ObservableCollection<DataExport> variablesToRevise = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> revisedVariableList = new ObservableCollection<DataExport>();
        private string variableToRevise_selectedQuestionVariable = string.Empty;
        private string[] variableToRevise_selectedQuestionVariableType;
        private List<String> revisedChoice_SelectCount = new List<string>();
        private static bool isInitialWindowLoad;
        private static bool isSelected = false;
        private static int selectedindex_VariableToRevise;
        public static Excel.Range range;
        private static double min = 0;
        private static double max = 0;
        private static double avg = 0;
        private string processingType;
        private string processingOption;
        private int readRow;
        private int writeRow;
        private string substOperator = string.Empty;
        private string substParam = string.Empty;
        private bool isNewQuestion = false;
        private bool isUpdateQuestion = false;
        public bool isModifiedProcess = false;
        private static bool isFreeEntryClicked = false;
        private static bool isEditOrCopy = false;
        private DataTable dtChoices;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private FormUtil formUtil = new FormUtil();
        private DataProcessHelper dbHelper = new DataProcessHelper();
        Constants.RevisionMethod rvMethod = new Constants.RevisionMethod();
        FormUtil frmutil = new FormUtil();
        bool isimprossedkey = false;//[Redmine id: 189510]
        bool isCtrl_v = false;
        string previewsnumber = string.Empty;
        public ReviseData(Excel.Workbook Workbook, int read_row, int write_row, string stdProcessingtype, string stdprocessingoption)
        {
            workbook = Workbook;
            readRow = read_row;
            writeRow = write_row;
            processingType = stdProcessingtype;
            processingOption = stdprocessingoption;
            InitializeComponent();
            LoadControls();
        }
        public ReviseData()
        {

        }
        // To enable and disable criteria control
        private void OnCheck(object sender, RoutedEventArgs e)
        {
            if (Check_Criteria.IsChecked == true)
                Criteria_Control.ReviseData_Checkbox_OnClick(true);
            else
                Criteria_Control.ReviseData_Checkbox_OnClick(false);
        }

        private void Command_Close_Click(object sender, RoutedEventArgs e)
        {
            isSelected = false;
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Criteria_Control.LoadingData(workbook);
                LoadVariablesToRevise();
                if (processingOption == QC4Common.Common.Constants.STD_DP.Process_Create)
                {
                    isInitialWindowLoad = true;
                    Grid_ReviseItem_Choices.DataContext = null;
                    Textbox_ProcessMethod.Foreground = new SolidColorBrush(Colors.Black);
                }
                else if (processingOption == QC4Common.Common.Constants.STD_DP.Process_Edit || processingOption == QC4Common.Common.Constants.STD_DP.Process_Copy)
                {
                    Excel.Worksheet dataProcessSheet = Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Qc4Launcher.Util.Constants.SheetCodeName.DataProcessS);
                    Excel.Range thenRow = dataProcessSheet.Cells[readRow, 1];
                    int firstRow = dbHelper.GetCurrentProcessFirstRow(readRow, workbook);
                    Excel.Range dpStart = dataProcessSheet.Cells[firstRow, 1];
                    Excel.Range dpEnd = dbHelper.GetLastCellInRow(thenRow);
                    Excel.Range range_Edit_Copy = dataProcessSheet.Range[dpStart, dpEnd]; //read the process from hidden sheet

                    if (range_Edit_Copy.Cells.Count > 1)
                    {
                        QuestionSettings qs = new QuestionSettings();
                        var obj = range_Edit_Copy.Value;
                        //EDIT_OR_COPY_Process_Details.
                        if (obj[1, 3] != null)
                        {
                            isEditOrCopy = true;
                            PopulateValuesToModify(range_Edit_Copy, obj);
                        }
                    }
                    if (processingOption == QC4Common.Common.Constants.STD_DP.Process_Edit)
                    {
                        writeRow = firstRow;
                        Textbox_ProcessMethod.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
                        Textbox_ProcessMethod.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ADADAD"));
                    }
                    else
                    {
                        Textbox_ProcessMethod.Background = new SolidColorBrush(Colors.White);
                        Textbox_ProcessMethod.Foreground = new SolidColorBrush(Colors.Black);
                    }

                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            isSelected = false;
        }

        //To load controls to the window
        private void LoadControls()
        {
            //To add values in Revision method combobox
            revisionMethods = new List<String>();
            revisionMethods.Add(rvMethod.EQUAL_Desc);
            revisionMethods.Add(rvMethod.ADD_Desc);
            revisionMethods.Add(rvMethod.MINUS_Desc);

            Combo_RevisionMethod.ItemsSource = revisionMethods;
            Combo_RevisionMethod.SelectedItem = revisionMethods.FirstOrDefault();
        }

        #region Loading data in comboboxes
        //To load data in combobox Combo_VariablesToRevise
        private void LoadVariablesToRevise()
        {
            try
            {
                dictionary = Definiotion.VariableDictionary;
                String[] dictKeys = dictionary.Keys.ToArray<String>();//to get all the variables in the sheet except SampleID and AnswerDate
                for (int i = 1; i < dictKeys.Count<String>(); i++)
                {
                    QuestionSettings qs = dictionary[dictKeys[i]];
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

                foreach (DataExport item in dataFromSheet)
                {
                    variablesToRevise.Add(item);
                }
                Combo_VariablesToRevise.DataContext = variablesToRevise;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

        }

        //To load data in the combobox Combo_RevisedVariable
        private void Combo_RevisedVariable_LoadData(string answerType)
        {
            Combo_RevisedVariable.DataContext = null;
            revisedVariableList = new ObservableCollection<DataExport>();
            dataFromListSheet = new List<DataExport>();
            var SettingSheet = ExcelUtil.GetWorkSheetByCodeName(workbook, "Sheet91");
            //get variables from the sheet according to the answer type of selected Variable to revise
            if (answerType == QC4Common.Common.Constants.AnswerType.SA)
                range = SettingSheet.get_Range("List_Item_SA");
            if (answerType == QC4Common.Common.Constants.AnswerType.MA)
                range = SettingSheet.get_Range("List_Item_SAMA");
            if (answerType == QC4Common.Common.Constants.AnswerType.N)
                range = SettingSheet.get_Range("List_Item_SAN");

            try
            {
                if (range.Cells.Count > 1)
                {
                    object[,] rangearray = range.Value;
                    for (int i = 1; i <= rangearray.GetLength(0); i++)
                    {
                        if (Convert.ToString(rangearray[i, 1]) != string.Empty && dictionary.ContainsKey(Convert.ToString(rangearray[i, 1])))
                        {
                            QuestionSettings question = dictionary[Convert.ToString(rangearray[i, 1])];
                            if (question.AnswerType == QC4Common.Common.Constants.AnswerType.MA || question.AnswerType == QC4Common.Common.Constants.AnswerType.SA)
                            {
                                dataFromListSheet.Add(new DataExport() { QuestionVariable = question.Variable, QuestionVariableType = question.AnswerType + '/' + question.CategoryCount, Question = formUtil.EscapeCRLF(question.Question), QuestionIndex = i - 1, QuestionChoiceNo = question.QuestionNumber, Choisces = question.Choices });
                            }
                            else if (question.AnswerType == QC4Common.Common.Constants.AnswerType.N)
                            {
                                dataFromListSheet.Add(new DataExport() { QuestionVariable = question.Variable, QuestionVariableType = question.AnswerType, Question = formUtil.EscapeCRLF(question.Question), QuestionIndex = i - 1, QuestionChoiceNo = question.QuestionNumber, Choisces = question.Choices });
                            }
                            else
                                continue;
                        }
                    }
                }
                else
                {
                    if (range.Text != null && range.Text != string.Empty)
                    {
                        if (Convert.ToString(range.Value) != string.Empty && dictionary.ContainsKey(Convert.ToString(range.Value)))
                        {
                            QuestionSettings question = dictionary[Convert.ToString(range.Value)];
                            if (question.AnswerType == QC4Common.Common.Constants.AnswerType.MA || question.AnswerType == QC4Common.Common.Constants.AnswerType.SA)
                            {
                                dataFromListSheet.Add(new DataExport() { QuestionVariable = question.Variable, QuestionVariableType = question.AnswerType + '/' + question.CategoryCount, Question = formUtil.EscapeCRLF(question.Question), QuestionIndex = 0, QuestionChoiceNo = question.QuestionNumber, Choisces = question.Choices });
                            }
                            else if (question.AnswerType == QC4Common.Common.Constants.AnswerType.N)
                            {
                                dataFromListSheet.Add(new DataExport() { QuestionVariable = question.Variable, QuestionVariableType = question.AnswerType, Question = formUtil.EscapeCRLF(question.Question), QuestionIndex = 0, QuestionChoiceNo = question.QuestionNumber, Choisces = question.Choices });
                            }

                        }
                    }
                }
                foreach (DataExport item in dataFromListSheet)
                {
                    revisedVariableList.Add(item);
                }
                Combo_RevisedVariable.DataContext = revisedVariableList;
                Combo_RevisedVariable.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        #endregion

        #region Event handlers for Grid of choices
        System.Windows.Controls.DataGrid ExpGrid = null;
        //datatable to handle the choices
        private DataTable CreateTable()
        {
            DataTable griddata = new DataTable();
            griddata.Columns.Add("No");
            griddata.Columns.Add("Choice");
            return griddata;
        }

        //fill the datagrid with choices
        private DataTable FillDataGrid(int limit, string[] choices)
        {
            DataTable griddata = CreateTable();
            DataRow dr;
            for (int i = 1; i <= limit; i++)
            {
                dr = griddata.NewRow();
                dr["No"] = i;
                dr["Choice"] = formUtil.EscapeCRLF(choices[i - 1]);
                griddata.Rows.Add(dr);
            }
            return griddata;
        }

        private void Choices_grid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
            var x = Grid_ReviseItem_Choices.CurrentCell;
        }

        private void Choices_grid_CurrentCellChanged(object sender, EventArgs e)
        {
            var dg = (System.Windows.Controls.DataGrid)sender;
            ExpGrid = dg;
            dg.Focus();
        }

        private void Grid_ReviseItem_Choices_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            QC4Common.Util.GridCommonFunc.Arrow(sender, e);
        }

        private void b1SetColor(object sender, MouseButtonEventArgs e)
        {
            if (ExpGrid != null)
                ExpGrid.Focus();
        }
        #endregion

        #region Comboboxes Event handlers
        System.Windows.Controls.ComboBox combo = null;
        bool FirstFocus = true;
        int LastSelected = 0;
        string LastSelectedText = "";
        private void Combo_KeyUp(ComboBox comboBox, object sender, KeyEventArgs e)
        {
            if (comboBox.Name == "Combo_RevisedChoice" && variableToRevise_selectedQuestionVariableType[0] == QC4Common.Common.Constants.AnswerType.MA)
            {
                if (string.IsNullOrEmpty(comboBox.Text))
                    Command_Entry.IsEnabled = false;
                else
                    Command_Entry.IsEnabled = true;
                return;
            }
            try
            {
                System.Windows.Controls.ComboBox sen = ((System.Windows.Controls.ComboBox)sender);
                if ((Key.Back == e.Key || ((sen.SelectedIndex == -1 && sen.Text == "") || (sen.SelectedIndex == -1 && sen.Text != ""))) && (comboBox.IsKeyboardFocusWithin || comboBox.IsDropDownOpen))
                {
                    if (Key.Tab == e.Key)
                        sen.SelectedIndex = -1;
                    else
                        sen.SelectedIndex = LastSelected;
                }
                else if (Key.Back == e.Key || (sen.SelectedIndex == -1 && sen.Text == "") || (sen.SelectedIndex == -1 && sen.Text != ""))
                {
                    LastSelected = 0;
                }
                else if (sen.SelectedItem == null && (sen.IsKeyboardFocusWithin || sen.IsDropDownOpen))
                {
                    sen.SelectedIndex = LastSelected;
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void Combo_KeyDown(ComboBox comboBox, object sender, KeyEventArgs e)
        {
            try
            {
                TextBox txt = null;
                if (e.OriginalSource is TextBox)
                    txt = (TextBox)e.OriginalSource;

                if (comboBox.SelectedIndex < 0)
                    LastSelected = 0;
                else if (comboBox.SelectedIndex > 0)
                    LastSelected = comboBox.SelectedIndex;
                else if (comboBox.SelectedIndex != 0)
                {
                    LastSelectedText = comboBox.Text;
                    LastSelected = 0;
                }

                if (Key.Back == e.Key && (comboBox.IsKeyboardFocusWithin || comboBox.IsDropDownOpen))
                {
                    LastSelected = 0;
                    comboBox.SelectedIndex = 0;
                    LastSelectedText = comboBox.Text;

                    e.Handled = false;
                }

                else if ((txt != null && txt.SelectedText.Length == txt.Text.Length && Key.Back == e.Key) && (comboBox.IsKeyboardFocusWithin || comboBox.IsDropDownOpen))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void Combo_VariablesToRevise_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Combo_KeyDown(Combo_VariablesToRevise, sender, e);
        }

        private void Combo_RevisedChoice_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Combo_KeyDown(Combo_RevisedChoice, sender, e);
        }

        private void Combo_RevisedVariable_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Combo_KeyDown(Combo_RevisedVariable, sender, e);
        }

        private void Combo_VariablesToRevise_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (Key.Tab != e.Key && !frmutil.IsSystemKey(e))
            {
                Combo_KeyUp(Combo_VariablesToRevise, sender, e);
            }
        }

        private void Combo_RevisedChoice_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            Combo_KeyUp(Combo_RevisedChoice, sender, e);
        }

        private void Combo_RevisedVariable_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (Key.Tab != e.Key && !frmutil.IsSystemKey(e))
            {
                Combo_KeyUp(Combo_RevisedVariable, sender, e);
            }
        }

        private void Combo_VariabletoRevise_DropDownClosed(object sender, EventArgs e)
        {
            System.Windows.Controls.ComboBox sen = ((System.Windows.Controls.ComboBox)sender);
            if (sen.SelectedItem != null)
            {
                var comboItem = sen.SelectedItem;
                int index = sen.Items.IndexOf(comboItem);
                sen.SelectedIndex = index;
            }
        }

        private void Combo_VariableToRevise_GotFocus(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.ComboBox sen = ((System.Windows.Controls.ComboBox)sender);
            combo = sen;
        }

        private void Combo_VariableToRevise_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (!FirstFocus)
            {
                System.Windows.Controls.ComboBox sen = ((System.Windows.Controls.ComboBox)sender);
            }
            else
            {
                FirstFocus = false;
            }

        }


        private void Combo_VariableToRevise_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isInitialWindowLoad || Combo_VariablesToRevise.SelectedIndex < 0)
            {
               
                isInitialWindowLoad = false;
                Combo_VariablesToRevise.SelectedIndex = -1;
            }
            else if (Combo_VariablesToRevise.SelectedIndex == 0 && !isSelected)
            {
                isSelected = true;
                Combo_VariablesToRevise.SelectedIndex = -1;
            }
            else if (isSelected)
            {
                try
                {
                    Grid_ReviseItem_Choices.DataContext = null;
                    selectedindex_VariableToRevise = Combo_VariablesToRevise.SelectedIndex;
                    variableToRevise_selectedQuestionVariableType = variablesToRevise[selectedindex_VariableToRevise].QuestionVariableType.Split('/');
                    if (variableToRevise_selectedQuestionVariableType.Length == 2)
                    {
                        Text_ReviseItem_AnswerType.Text = variableToRevise_selectedQuestionVariableType[0];//answer type of selected variable
                        Text_ReviseItem_CategoryCount.Text = variableToRevise_selectedQuestionVariableType[1]; //category count of selected variable
                    }
                    else if (variableToRevise_selectedQuestionVariableType.Length == 1) //if N type variable
                    {
                        Text_ReviseItem_AnswerType.Text = variableToRevise_selectedQuestionVariableType[0];//answer type of selected variable
                        Text_ReviseItem_CategoryCount.Text = "0";
                    }
                    Text_ReviseItem_Question.Text = formUtil.UnEscapeCRLF(variablesToRevise[selectedindex_VariableToRevise].Question); //question text of selected variable
                    if (variableToRevise_selectedQuestionVariableType[0] == QC4Common.Common.Constants.AnswerType.N)
                    {
                        //show the reference value instead of choices
                        Grid_ReviseItem_Choices.Visibility = Visibility.Hidden;
                        minmaxavgborder.Visibility = Visibility.Visible;
                        Label_Reference_Value.Visibility = Visibility.Visible;
                        Text_OriginItem_Min.Text = string.Empty;
                        Text_OriginItem_Max.Text = string.Empty;
                        Text_OriginItem_Avg.Text = string.Empty;
                        if (dictionary.ContainsKey(variablesToRevise[selectedindex_VariableToRevise].QuestionVariable) && dictionary[variablesToRevise[selectedindex_VariableToRevise].QuestionVariable].QuestionFlag != QC4Common.Common.Constants.QuestionFlag.New)
                        {
                            FormUtil formUtil = new FormUtil();
                            formUtil.NtypeGetMinMaxAvg("q_" + (dictionary[variablesToRevise[selectedindex_VariableToRevise].QuestionVariable].ItemId).ToString(), workbook, out min, out max, out avg);
                            Text_OriginItem_Min.Text = min.ToString();
                            Text_OriginItem_Max.Text = max.ToString();
                            Text_OriginItem_Avg.Text = avg.ToString();
                        }
                    }
                    else
                    {
                        minmaxavgborder.Visibility = Visibility.Hidden;
                        Label_Reference_Value.Visibility = Visibility.Hidden;
                        Grid_ReviseItem_Choices.Visibility = Visibility.Visible;
                        Grid_ReviseItem_Choices.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
                        dtChoices = FillDataGrid(variablesToRevise[selectedindex_VariableToRevise].Choisces.Count, variablesToRevise[selectedindex_VariableToRevise].Choisces.ToArray());
                        Grid_ReviseItem_Choices.DataContext = dtChoices;
                    }

                    // Changing status of radiobuttons and controls in "Revised Values" section according to the AnswerType of VariableToRevise
                    ResetRevisedValueControls();
                    switch (variableToRevise_selectedQuestionVariableType[0])
                    {
                        case QC4Common.Common.Constants.AnswerType.MA:
                            Option_Free_Entry.IsEnabled = false;
                            Option_Variable.IsEnabled = true;
                            Option_No_Answer.IsEnabled = true;
                            Option_Excluded.IsEnabled = true;
                            Option_Choice.IsEnabled = true;
                            Combo_RevisionMethod.IsEnabled = true;
                            Combo_RevisionMethod.Opacity = 1;
                            Combo_RevisionMethod.Background = new SolidColorBrush(Colors.White);
                            break;

                        case QC4Common.Common.Constants.AnswerType.SA:
                            Combo_RevisionMethod.IsEnabled = false;
                            Combo_RevisionMethod.Opacity = 0.7;
                            Combo_RevisionMethod.SelectedItem = revisionMethods.FirstOrDefault();
                            Option_Free_Entry.IsEnabled = false;
                            Option_Variable.IsEnabled = true;
                            Option_No_Answer.IsEnabled = true;
                            Option_Excluded.IsEnabled = true;
                            Option_Choice.IsEnabled = true;
                            break;

                        case QC4Common.Common.Constants.AnswerType.N:
                            Combo_RevisionMethod.IsEnabled = false;
                            Combo_RevisionMethod.Opacity = 0.7;
                            Combo_RevisionMethod.SelectedItem = revisionMethods.FirstOrDefault();
                            Option_Choice.IsEnabled = false;
                            Option_Variable.IsEnabled = true;
                            Option_No_Answer.IsEnabled = true;
                            Option_Excluded.IsEnabled = true;
                            Option_Free_Entry.IsEnabled = true;
                            break;

                        case QC4Common.Common.Constants.AnswerType.FA:
                            Combo_RevisionMethod.IsEnabled = false;
                            Combo_RevisionMethod.Opacity = 0.7;
                            Combo_RevisionMethod.SelectedItem = revisionMethods.FirstOrDefault();
                            Option_Choice.IsEnabled = false;
                            Option_Variable.IsEnabled = false;
                            Option_No_Answer.IsEnabled = true;
                            Option_Excluded.IsEnabled = true;
                            Option_Free_Entry.IsEnabled = true;
                            break;

                        default:
                            break;
                    }
                    if (isEditOrCopy)
                    {
                        SetSubstituteValuesOnLoad(substOperator, substParam, variableToRevise_selectedQuestionVariableType[0]);
                        isEditOrCopy = false;
                    }
                    //Added as fix for Bug with Redmine Id:#257578 ----> start
                    string selectedRevisionMethod = Combo_RevisionMethod.SelectedItem.ToString();
                    if ((variableToRevise_selectedQuestionVariableType != null) && (variableToRevise_selectedQuestionVariableType[0] == QC4Common.Common.Constants.AnswerType.MA))
                    {
                        if (selectedRevisionMethod != rvMethod.EQUAL_Desc)
                        {
                            Option_Excluded.IsEnabled = false;
                            Option_No_Answer.IsEnabled = false;
                        }
                        else
                        {
                            Option_Excluded.IsEnabled = true;
                            Option_No_Answer.IsEnabled = true;
                        }
                    }
                    //----> End of fix
                }
                catch (Exception ex)
                {
                    _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                }
               
            }
            if (Combo_VariablesToRevise.SelectedIndex < 0)
            {
                Command_Entry.IsEnabled = false;
            }
        }

        private void Combo_RevisionMethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedRevisionMethod = Combo_RevisionMethod.SelectedItem.ToString();
            try
            {
                ResetRevisedValueControls();
                if ((variableToRevise_selectedQuestionVariableType != null) && (variableToRevise_selectedQuestionVariableType[0] == QC4Common.Common.Constants.AnswerType.MA))
                {
                    if (selectedRevisionMethod != rvMethod.EQUAL_Desc)
                    {
                        Option_Excluded.IsEnabled = false;
                        Option_No_Answer.IsEnabled = false;
                    }
                    else
                    {
                        Option_Excluded.IsEnabled = true;
                        Option_No_Answer.IsEnabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void Combo_RevisedVariable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Combo_RevisedVariable.SelectedIndex >= 0 && !string.IsNullOrEmpty(Combo_VariablesToRevise.Text)&& !string.IsNullOrWhiteSpace(Combo_VariablesToRevise.Text))
                Command_Entry.IsEnabled = true;
            else
                Command_Entry.IsEnabled = false;
        }

        private void Combo_RevisedChoice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Combo_RevisedChoice.SelectedIndex >= 0 && !string.IsNullOrEmpty(Combo_VariablesToRevise.Text) && !string.IsNullOrWhiteSpace(Combo_VariablesToRevise.Text))
                Command_Entry.IsEnabled = true;
            else
                Command_Entry.IsEnabled = false;
        }
        #endregion

        private void ResetRevisedValueControls()
        {
            Combo_RevisedChoice.IsEnabled = false;
            Combo_RevisedChoice.DataContext = null;
            Combo_RevisedVariable.IsEnabled = false;
            Combo_RevisedVariable.DataContext = null;
            isFreeEntryClicked = false;
            Textbox_Free_Entry.Text = string.Empty;
            Textbox_Free_Entry.IsEnabled = false;
            Textbox_Free_Entry.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#F0F0F0");
            Option_Choice.IsChecked = false;
            Option_Variable.IsChecked = false;
            Option_No_Answer.IsChecked = false;
            Option_Excluded.IsChecked = false;
            Option_Free_Entry.IsChecked = false;
            Command_Entry.IsEnabled = false;
        }

        #region Radioboxes Event Handlers

        private void RevisedValue_RadioButtonClick(object sender, RoutedEventArgs e)
        {
            string buttonName = ((RadioButton)sender).Name;
            var button = (RadioButton)sender;

            try
            {
                if (buttonName == "Option_Choice" && button.IsChecked == true)
                {
                    Combo_RevisedVariable.IsEnabled = false;
                    Combo_RevisedVariable.DataContext = null;
                    isFreeEntryClicked = false;
                    Textbox_Free_Entry.IsEnabled = false;
                    Textbox_Free_Entry.Text = string.Empty;
                    Textbox_Free_Entry.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#F0F0F0");

                    Combo_RevisedChoice.IsEnabled = true;
                    Combo_RevisedChoice.Background = new SolidColorBrush(Colors.White);
                    Combo_RevisedChoice.DataContext = null;
                    revisedChoice_SelectCount = new List<string>();
                    for (int i = 0; i < variablesToRevise[selectedindex_VariableToRevise].Choisces.Count; i++)
                    {
                        revisedChoice_SelectCount.Add((i + 1).ToString());
                    }
                    Combo_RevisedChoice.DataContext = revisedChoice_SelectCount;
                    Combo_RevisedChoice.SelectedIndex = -1;
                    Command_Entry.IsEnabled = false;
                }
                else if (buttonName == "Option_Variable" && button.IsChecked == true)
                {
                    Combo_RevisedChoice.IsEnabled = false;
                    Combo_RevisedChoice.DataContext = null;
                    isFreeEntryClicked = false;
                    Textbox_Free_Entry.IsEnabled = false;
                    Textbox_Free_Entry.Text = string.Empty;
                    Textbox_Free_Entry.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#F0F0F0");

                    Combo_RevisedVariable.IsEnabled = true;
                    Combo_RevisedVariable.Background = new SolidColorBrush(Colors.White);
                    Combo_RevisedVariable_LoadData(variableToRevise_selectedQuestionVariableType[0]);
                    Command_Entry.IsEnabled = false;
                }
                else if (buttonName == "Option_Free_Entry" && button.IsChecked == true)
                {
                    Combo_RevisedChoice.IsEnabled = false;
                    Combo_RevisedChoice.DataContext = null;
                    Combo_RevisedVariable.DataContext = null;
                    Combo_RevisedVariable.IsEnabled = false;

                    isFreeEntryClicked = true;
                    Textbox_Free_Entry.IsEnabled = true;
                    Textbox_Free_Entry.Background = new SolidColorBrush(Colors.White);
                    isDecimalPressed = false;
                    isMinusPressed = false;
                    Command_Entry.IsEnabled = false;
                }
                else if (buttonName == "Option_No_Answer" && button.IsChecked == true)
                {
                    Combo_RevisedChoice.IsEnabled = false;
                    Combo_RevisedChoice.DataContext = null;
                    Combo_RevisedVariable.IsEnabled = false;
                    Combo_RevisedVariable.DataContext = null;

                    isFreeEntryClicked = false;
                    Textbox_Free_Entry.IsEnabled = false;
                    Textbox_Free_Entry.Text = string.Empty;
                    Textbox_Free_Entry.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#F0F0F0");
                    Command_Entry.IsEnabled = true;
                }
                else if (buttonName == "Option_Excluded" && button.IsChecked == true)
                {
                    Combo_RevisedChoice.IsEnabled = false;
                    Combo_RevisedChoice.DataContext = null;
                    Combo_RevisedVariable.IsEnabled = false;
                    Combo_RevisedVariable.DataContext = null;

                    isFreeEntryClicked = false;
                    Textbox_Free_Entry.Text = string.Empty;
                    Textbox_Free_Entry.IsEnabled = false;
                    Textbox_Free_Entry.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#F0F0F0");
                    Command_Entry.IsEnabled = true;
                }
                if (string.IsNullOrEmpty(Combo_VariablesToRevise.Text)|| string.IsNullOrWhiteSpace(Combo_VariablesToRevise.Text))
                {
                    Command_Entry.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }
        #endregion

        #region Free_Entry Textbox Event handlers

        string newChar = string.Empty;
        string oldText = string.Empty;
        string newText = string.Empty;
        int maxlengthFreeEntry = QC4Common.Common.Constants.STD_DP.ADD_MINUS_MAX_Text_Length;
        int zeroCount = 0;
        bool isDecimalPressed = false;
        bool isMinusPressed = false;
        bool isBackOrDelete = false;

        private void Textbox_Free_Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if ((variableToRevise_selectedQuestionVariableType[0] == QC4Common.Common.Constants.AnswerType.N))//https://app.gluemodel.com/#/project/task/4295061352
                {
                    string input = Textbox_Free_Entry.Text.Normalize(NormalizationForm.FormKC);
                    bool _altModifierPressed = (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl));
                    if (!isimprossedkey && !Keyboard.IsKeyDown(Key.Delete) && !Keyboard.IsKeyDown(Key.Back) && !(_altModifierPressed && Keyboard.IsKeyDown(Key.X)))
                    {
                        if (input.StartsWith("."))
                        {
                            input = "0" + input;
                        }
                        if (input.StartsWith("-."))
                        {
                            input = "-0" + input;
                        }
                        e.Handled = new Regex(@"^[-]?([0-9]+(\.[0-9]*)?)?$").IsMatch(input);
                        if ((e.Handled) && Vb.Information.IsNumeric(input))
                        {
                            if (input.Length > QC4Common.Common.Constants.STD_DP.ADD_MINUS_MAX_Text_Length)
                            {
                                e.Handled = false;
                            }
                            else
                            {
                                previewsnumber = input;
                            }
                        }
                        else if (input.Length == 1 && !(e.Handled))
                        {
                            input = previewsnumber = "0";
                            e.Handled = true;
                        }
                        else if (input.Length > 0 && !(e.Handled))
                            e.Handled = false;

                        if (e.Handled == false && input.Length > 0) input = previewsnumber.Normalize(NormalizationForm.FormKC);

                        try
                        {
                            Textbox_Free_Entry.Text = input.Normalize(NormalizationForm.FormKC);

                            Textbox_Free_Entry.CaretIndex = input.Length;
                           //previewsnumber = "0";
                           if(Textbox_Free_Entry.Text.Length==0)
                            {
                                Textbox_Free_Entry.Text = "0";
                                previewsnumber = "0";
                            }
                        }
                        catch { }
                    }
                }
                if (string.IsNullOrEmpty(Textbox_Free_Entry.Text)|| string.IsNullOrWhiteSpace(Combo_VariablesToRevise.Text))
                    Command_Entry.IsEnabled = false;
                else
                    Command_Entry.IsEnabled = true;

                //if (variableToRevise_selectedQuestionVariableType[0] == QC4Common.Common.Constants.AnswerType.N)
                //{
                //    if (!isBackOrDelete && isFreeEntryClicked)
                //    {
                //        if (!isCtrl_v)
                //        {
                //            if (!isimprossedkey)
                //            {
                //                //Textbox_Free_Entry.Text = oldText + newChar;
                //                string text = oldText + newChar;
                //                if (text.Length <= QC4Common.Common.Constants.STD_DP.ADD_MINUS_MAX_Text_Length)
                //                {
                //                    Textbox_Free_Entry.Text = oldText + newChar; // Textbox_Free_Entry.Text = text.Substring(0, QC4Common.Common.Constants.STD_DP.ADD_MINUS_MAX_Text_Length);
                //                }
                //                Textbox_Free_Entry.SelectionStart = Textbox_Free_Entry.Text.Length;
                //                Textbox_Free_Entry.SelectionLength = 0;
                //            }
                //        }
                //    }

                //}
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void Textbox_Free_Entry_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                oldText = Textbox_Free_Entry.Text;
                bool _altModifierPressed = (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl));
                if ((variableToRevise_selectedQuestionVariableType[0] == QC4Common.Common.Constants.AnswerType.N))
                {
                    string a = e.ImeProcessedKey.ToString();//[Redmine id: 189510]
                    if (a != "None")//[Redmine id: 189510]
                    {
                        isimprossedkey = true;
                    }
                    if (!isimprossedkey)//[Redmine id: 189510]
                    {
                        //https://app.gluemodel.com/#/project/task/4295061352
                        if ((_altModifierPressed && Keyboard.IsKeyDown(Key.V)))
                        {
                            isCtrl_v = true;
                             e.Handled = true;
                            if (Clipboard.ContainsText(TextDataFormat.UnicodeText))
                            {

                                if ((variableToRevise_selectedQuestionVariableType[0] == QC4Common.Common.Constants.AnswerType.N))
                                {
                                    string input = Clipboard.GetText(TextDataFormat.UnicodeText).Normalize(NormalizationForm.FormKC);
                                    if (!string.IsNullOrEmpty(input))
                                    {
                                        string aa = input;
                                        string sub = string.Empty;
                                        string temp = string.Empty;
                                        for (int i = 0; i < aa.Length; i++)
                                        {
                                            if (new Regex(@"^[0-9.-]+$").IsMatch(Convert.ToString(aa[i])))//^[-+]?\d+(\.\d+)?$
                                            {
                                                // if ( (!aa[i].Equals("-") && !aa[i].Equals(".")) ||(aa[i].Equals("-") && !sub.Contains("-")) || (aa[i].Equals(".") && !sub.Contains(".")))

                                                sub = sub + aa[i];

                                                if (string.Equals(sub, "."))
                                                {
                                                    sub = "0.";
                                                }
                                            }
                                        }
                                        aa = sub;
                                        if (aa.Length >= 1)
                                        {
                                            if (aa.StartsWith("."))
                                            {
                                                aa = "0" + aa;
                                            }
                                            if (aa.Contains("-") && aa.Length == 1)
                                            {
                                                aa = "0";
                                            }
                                            if (aa.Contains("-"))
                                            {
                                                int countofseperator = aa.Count(f => (f == '-'));
                                                aa = aa.Trim('-');
                                                aa = "-" + aa;
                                            }
                                            if (!new Regex(@"^[0-9.-]+$").IsMatch(aa))//^[-+]?\d+(\.\d+)?$
                                            {
                                                Textbox_Free_Entry.Text = string.Empty;
                                                Textbox_Free_Entry.Text = "0";
                                                Textbox_Free_Entry.SelectionStart = Textbox_Free_Entry.Text.Length;
                                                Textbox_Free_Entry.SelectionLength = 0;
                                            }
                                            else
                                            {
                                                if (aa.Length > QC4Common.Common.Constants.STD_DP.ADD_MINUS_MAX_Text_Length)
                                                {
                                                    aa = aa.Substring(0, QC4Common.Common.Constants.STD_DP.ADD_MINUS_MAX_Text_Length);
                                                }

                                                if (Textbox_Free_Entry.SelectionLength > 0)
                                                {
                                                    if (Textbox_Free_Entry.SelectionLength == Textbox_Free_Entry.Text.Length)
                                                    {
                                                        Textbox_Free_Entry.Text = string.Empty;
                                                        Textbox_Free_Entry.Text = aa;
                                                    }
                                                    else
                                                    {
                                                        int startIndex = Textbox_Free_Entry.SelectionStart;
                                                        Textbox_Free_Entry.Text = Textbox_Free_Entry.Text.Remove(startIndex, Textbox_Free_Entry.SelectionLength);
                                                        Textbox_Free_Entry.Text = Textbox_Free_Entry.Text.Insert(startIndex, aa);
                                                    }
                                                }
                                                else
                                                {
                                                    Textbox_Free_Entry.Text = Textbox_Free_Entry.Text.Insert(Textbox_Free_Entry.SelectionStart, aa);
                                                }

                                                //Textbox_Free_Entry.Text = Textbox_Free_Entry.Text + aa;
                                                if (Textbox_Free_Entry.Text.Length > QC4Common.Common.Constants.STD_DP.ADD_MINUS_MAX_Text_Length)
                                                {
                                                    Textbox_Free_Entry.Text = Textbox_Free_Entry.Text.Substring(0, QC4Common.Common.Constants.STD_DP.ADD_MINUS_MAX_Text_Length);
                                                }
                                                Textbox_Free_Entry.SelectionStart = Textbox_Free_Entry.Text.Length;
                                                Textbox_Free_Entry.SelectionLength = 0;
                                            }
                                        }
                                        else//Fullwidth alphabetsonly
                                        {
                                            Textbox_Free_Entry.Text = string.Empty;
                                            Textbox_Free_Entry.Text = "0";
                                            Textbox_Free_Entry.SelectionStart = Textbox_Free_Entry.Text.Length;
                                            Textbox_Free_Entry.SelectionLength = 0;
                                        }
                                    }
                                }
                            }


                        }
                        //if (e.Key == Key.Back || e.Key == Key.Delete || e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Home || e.Key == Key.End
                        //    || e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl || e.Key == Key.LeftShift || e.Key == Key.RightShift || e.Key == Key.RightAlt || e.Key == Key.System || e.Key == Key.LWin ||
                        //    (_altModifierPressed && Keyboard.IsKeyDown(Key.V)) || (_altModifierPressed && Keyboard.IsKeyDown(Key.X)) || (_altModifierPressed && Keyboard.IsKeyDown(Key.Z)) || (_altModifierPressed && Keyboard.IsKeyDown(Key.A)) || (_altModifierPressed && Keyboard.IsKeyDown(Key.C)))
                        //{
                        //    if ((_altModifierPressed && Keyboard.IsKeyDown(Key.V)) || (_altModifierPressed && Keyboard.IsKeyDown(Key.X)) || (_altModifierPressed && Keyboard.IsKeyDown(Key.Z)) || (_altModifierPressed && Keyboard.IsKeyDown(Key.A)) || (_altModifierPressed && Keyboard.IsKeyDown(Key.C)))
                        //    {
                        //        isBackOrDelete = true;
                        //        isCtrl_v = true;
                        //    }
                        //    else
                        //    {
                        //        isCtrl_v = false;
                        //    }
                        //    if (e.Key == Key.Back || e.Key == Key.Delete)
                        //    {
                        //        isBackOrDelete = true;
                        //    }
                        //    if ((_altModifierPressed && Keyboard.IsKeyDown(Key.V)))
                        //    {
                        //        isCtrl_v = true;
                        //        e.Handled = true;
                        //        if (Clipboard.ContainsText(TextDataFormat.UnicodeText))
                        //        {

                        //            if ((variableToRevise_selectedQuestionVariableType[0] == QC4Common.Common.Constants.AnswerType.N))
                        //            {
                        //                string input = Clipboard.GetText(TextDataFormat.UnicodeText).Normalize(NormalizationForm.FormKC);
                        //                if (!string.IsNullOrEmpty(input))
                        //                {
                        //                    string aa = input;
                        //                    string sub = string.Empty;
                        //                    string temp = string.Empty;
                        //                    for (int i = 0; i < aa.Length; i++)
                        //                    {
                        //                        if (new Regex(@"^[-+]?\d+(\.\d+)?$").IsMatch(Convert.ToString(aa[i])))
                        //                        {
                        //                            sub = sub + aa[i];
                        //                            if (string.Equals(sub, "."))
                        //                            {
                        //                                sub = "0.";
                        //                            }
                        //                        }
                        //                    }
                        //                    aa = sub;
                        //                    if (aa.Length >= 1)
                        //                    {
                        //                        if (aa.StartsWith("."))
                        //                        {
                        //                            aa = "0" + aa;
                        //                        }
                        //                        if (!new Regex(@"^[-+]?\d+(\.\d+)?$").IsMatch(aa))
                        //                        {
                        //                            Textbox_Free_Entry.Text = string.Empty;
                        //                            Textbox_Free_Entry.Text = "0";
                        //                            Textbox_Free_Entry.SelectionStart = Textbox_Free_Entry.Text.Length;
                        //                            Textbox_Free_Entry.SelectionLength = 0;
                        //                        }
                        //                        else
                        //                        {
                        //                            if (aa.Length > QC4Common.Common.Constants.STD_DP.ADD_MINUS_MAX_Text_Length)
                        //                            {
                        //                                aa = aa.Substring(0, QC4Common.Common.Constants.STD_DP.ADD_MINUS_MAX_Text_Length);
                        //                            }

                        //                            if (Textbox_Free_Entry.SelectionLength > 0)
                        //                            {
                        //                                if (Textbox_Free_Entry.SelectionLength == Textbox_Free_Entry.Text.Length)
                        //                                {
                        //                                    Textbox_Free_Entry.Text = string.Empty;
                        //                                    Textbox_Free_Entry.Text = aa;
                        //                                }
                        //                                else
                        //                                {
                        //                                    int startIndex = Textbox_Free_Entry.SelectionStart;
                        //                                    Textbox_Free_Entry.Text = Textbox_Free_Entry.Text.Remove(startIndex, Textbox_Free_Entry.SelectionLength);
                        //                                    Textbox_Free_Entry.Text = Textbox_Free_Entry.Text.Insert(startIndex, aa);
                        //                                }
                        //                            }
                        //                            else
                        //                            {
                        //                                Textbox_Free_Entry.Text = Textbox_Free_Entry.Text.Insert(Textbox_Free_Entry.SelectionStart, aa);
                        //                            }

                        //                            //Textbox_Free_Entry.Text = Textbox_Free_Entry.Text + aa;
                        //                            if (Textbox_Free_Entry.Text.Length > QC4Common.Common.Constants.STD_DP.ADD_MINUS_MAX_Text_Length)
                        //                            {
                        //                                Textbox_Free_Entry.Text = Textbox_Free_Entry.Text.Substring(0, QC4Common.Common.Constants.STD_DP.ADD_MINUS_MAX_Text_Length);
                        //                            }
                        //                            Textbox_Free_Entry.SelectionStart = Textbox_Free_Entry.Text.Length;
                        //                            Textbox_Free_Entry.SelectionLength = 0;
                        //                        }
                        //                    }
                        //                    else//Fullwidth alphabetsonly
                        //                    {
                        //                        Textbox_Free_Entry.Text = string.Empty;
                        //                        Textbox_Free_Entry.Text = "0";
                        //                        Textbox_Free_Entry.SelectionStart = Textbox_Free_Entry.Text.Length;
                        //                        Textbox_Free_Entry.SelectionLength = 0;
                        //                    }
                        //                }
                        //            }
                        //        }


                        //    }
                        //    return;
                        //}
                        //else if (oldText.Length >= maxlengthFreeEntry && e.Key == Key.Back)
                        //{
                        //    isCtrl_v = false;
                        //    oldText = oldText.Remove(oldText.Length - 1);
                        //    newChar = string.Empty;
                        //}
                        //else if (oldText.Length >= maxlengthFreeEntry)
                        //{
                        //    isCtrl_v = false;
                        //    newChar = string.Empty;
                        //    e.Handled = true;
                        //}
                        //else
                        //{
                        //    if (oldText == string.Empty)
                        //    {
                        //        newChar = string.Empty;
                        //        maxlengthFreeEntry = QC4Common.Common.Constants.STD_DP.ADD_MINUS_MAX_Text_Length;
                        //        zeroCount = 0;
                        //        isDecimalPressed = false;
                        //        isMinusPressed = false;
                        //    }
                        //    switch (e.Key)
                        //    {
                        //        case Key.D0:
                        //        case Key.NumPad0:
                        //            isBackOrDelete = false;
                        //            isCtrl_v = false;
                        //            if (oldText == string.Empty)
                        //                newChar = Convert.ToString(0);
                        //            else if (oldText == Convert.ToString(0))
                        //                newChar = string.Empty;
                        //            else
                        //                newChar = Convert.ToString(0);
                        //            break;

                        //        case Key.D1:
                        //        case Key.NumPad1:
                        //        case Key.D2:
                        //        case Key.NumPad2:
                        //        case Key.D3:
                        //        case Key.NumPad3:
                        //        case Key.D4:
                        //        case Key.NumPad4:
                        //        case Key.D5:
                        //        case Key.NumPad5:
                        //        case Key.D6:
                        //        case Key.NumPad6:
                        //        case Key.D7:
                        //        case Key.NumPad7:
                        //        case Key.D8:
                        //        case Key.NumPad8:
                        //        case Key.D9:
                        //        case Key.NumPad9:
                        //            isBackOrDelete = false;
                        //            isCtrl_v = false;
                        //            if (oldText.Length == 1 && oldText == Convert.ToString(0))
                        //            {
                        //                oldText = string.Empty;
                        //            }
                        //            if (e.Key.ToString().StartsWith("D"))
                        //                newChar = e.Key.ToString().Trim('D');
                        //            else if (e.Key.ToString().StartsWith("Num"))
                        //                newChar = e.Key.ToString().Remove(0, e.Key.ToString().Length - 1);
                        //            break;

                        //        case Key.OemPeriod:
                        //        case Key.Decimal:
                        //            isBackOrDelete = false;
                        //            isCtrl_v = false;
                        //            if (oldText == string.Empty)
                        //                newChar = Convert.ToString(0);
                        //            else if (!isDecimalPressed && oldText != string.Empty)
                        //            {
                        //                isDecimalPressed = true;
                        //                newChar = ".";
                        //            }
                        //            else if (!oldText.Contains("."))
                        //            {
                        //                isDecimalPressed = true;
                        //                newChar = ".";
                        //            }
                        //            else
                        //                newChar = string.Empty;
                        //            break;

                        //        case Key.OemMinus:
                        //        case Key.Subtract:
                        //            isBackOrDelete = false;
                        //            isCtrl_v = false;
                        //            if (!isMinusPressed)
                        //            {
                        //                isMinusPressed = true;
                        //                maxlengthFreeEntry = 14;
                        //                newChar = "-";
                        //                zeroCount = CalculateZeroCount(oldText);
                        //                if (oldText != string.Empty && oldText.Contains("."))
                        //                {
                        //                    if (zeroCount == oldText.Length - 1)
                        //                    {
                        //                        oldText = oldText + Convert.ToString(0);
                        //                        newChar = string.Empty;
                        //                        isMinusPressed = false;
                        //                        maxlengthFreeEntry = QC4Common.Common.Constants.STD_DP.ADD_MINUS_MAX_Text_Length;
                        //                    }
                        //                    else
                        //                    {
                        //                        if (oldText.Contains('-') && newChar.Contains('-'))
                        //                        {
                        //                            newChar = string.Empty;
                        //                        }
                        //                        oldText = newChar + oldText + Convert.ToString(0);
                        //                        newChar = string.Empty;
                        //                    }
                        //                }
                        //                else
                        //                {
                        //                    if (oldText.Contains('-') && newChar.Contains('-'))
                        //                    {
                        //                        newChar = string.Empty;
                        //                    }
                        //                    oldText = newChar + oldText;
                        //                    newChar = string.Empty;
                        //                }

                        //            }
                        //            else if (isMinusPressed)
                        //            {

                        //                if (oldText.Length == 1 && oldText == "-")
                        //                {
                        //                    newChar = Convert.ToString(0);
                        //                    isMinusPressed = false;
                        //                    maxlengthFreeEntry = QC4Common.Common.Constants.STD_DP.ADD_MINUS_MAX_Text_Length;
                        //                }
                        //                else
                        //                    newChar = string.Empty;
                        //            }
                        //            break;

                        //        default:
                        //            isBackOrDelete = false;
                        //            isCtrl_v = false;
                        //            if (oldText == string.Empty)
                        //                newChar = Convert.ToString(0);
                        //            else
                        //                newChar = string.Empty;
                        //            break;
                        //    }
                        //}
                        //Textbox_Free_Entry.Text = string.Empty;
                        //Textbox_Free_Entry.Text = oldText + newChar;
                        //if (Textbox_Free_Entry.Text.Length > QC4Common.Common.Constants.STD_DP.ADD_MINUS_MAX_Text_Length)
                        //{
                        //    Textbox_Free_Entry.Text = Textbox_Free_Entry.Text.Substring(0, QC4Common.Common.Constants.STD_DP.ADD_MINUS_MAX_Text_Length);
                        //}
                        //Textbox_Free_Entry.SelectionStart = Textbox_Free_Entry.Text.Length;
                        //Textbox_Free_Entry.SelectionLength = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void Textbox_Free_Entry_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            //https://app.gluemodel.com/#/project/task/4295061352
            //try
            //{
            //    if ((variableToRevise_selectedQuestionVariableType[0] == QC4Common.Common.Constants.AnswerType.N))
            //    {
            //        if (e.Key != Key.Back && e.Key != Key.Delete && e.Key != Key.Left && e.Key != Key.Right && e.Key != Key.Home && e.Key != Key.End
            //            && e.Key != Key.LeftCtrl && e.Key != Key.RightCtrl && e.Key != Key.LeftShift && e.Key != Key.RightShift && e.Key != Key.RightAlt && e.Key != Key.System && e.Key != Key.LWin && !(isCtrl_v))
            //        {
            //            if (!isimprossedkey)//[Redmine id: 189510]
            //            {
            //                Textbox_Free_Entry.Text = string.Empty;
            //                Textbox_Free_Entry.Text = oldText + newChar;
            //                if (Textbox_Free_Entry.Text.Length > QC4Common.Common.Constants.STD_DP.ADD_MINUS_MAX_Text_Length)
            //                {
            //                    Textbox_Free_Entry.Text = Textbox_Free_Entry.Text.Substring(0, QC4Common.Common.Constants.STD_DP.ADD_MINUS_MAX_Text_Length);
            //                }
            //                Textbox_Free_Entry.SelectionStart = Textbox_Free_Entry.Text.Length;
            //                Textbox_Free_Entry.SelectionLength = 0;
            //            }
            //        }

            //    }
            //}
            //catch (Exception ex)
            //{
            //    _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            //}
        }

        private int CalculateZeroCount(string str)
        {
            int c = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '0')
                    c++;
            }
            return c;

        }
        #endregion

        #region Revise Data process saving to hidden sheet
        /// <summary>Logic for saving the Group Process to the hidden sheet with all parameters.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Command_Entry_Click(object sender, RoutedEventArgs e)
        {
            List<QuestionSettings> questionSettings = new List<QuestionSettings>();
            questionSettings = dictionary.Values.ToList();
            QuestionSettings qs = new QuestionSettings();
            if (Text_ReviseItem_AnswerType.Text == QC4Common.Common.Constants.AnswerType.MA)
            {
                //validate the choice combobox 
                if (Option_Choice.IsChecked == true && !string.IsNullOrEmpty(Combo_RevisedChoice.Text) && !ValidateRevisedChoice(Combo_RevisedChoice.Text, Text_ReviseItem_CategoryCount.Text))
                    return;
            }
            if (Check_Criteria.IsChecked ==true) //validate only if criteria is checked
            {
                if (!Criteria_Control.ValidateCriteriaControls(true))
                {
                    return;
                }
            }
           // else
            {
                try
                {
                    isNewQuestion = false;//because it's not a new question
                    isUpdateQuestion = false;//because ther's no need to update QS 
                    int rowCount = 1;
                    string command = string.Empty;
                    if (Check_Criteria.IsChecked == true) //include left-hand side values only if criteria is checked
                    {
                        if (!string.IsNullOrEmpty(Criteria_Control.Combo_Conditional_Item_2.Text))
                            rowCount = 2;
                        if (!string.IsNullOrEmpty(Criteria_Control.Combo_Conditional_Item_3.Text))
                            rowCount = 3;
                        if (!string.IsNullOrEmpty(Criteria_Control.Combo_Conditional_Item_4.Text))
                            rowCount = 4;
                        if (!string.IsNullOrEmpty(Criteria_Control.Combo_Conditional_Item_5.Text))
                            rowCount = 5;
                    }

                    int colCount = (QC4Common.Common.Constants.DP.SubstituteParam1Column - QC4Common.Common.Constants.DP.OnOffColumn) + 1; //to identify upto which column we have to write values into the sheet in a single row
                    string[,] dpAddMinusInstructions = new string[rowCount, QC4Common.Common.Constants.DP.MAX_DP_COLUMN - QC4Common.Common.Constants.DP.OnOffColumn];//SAVE Array for the corresponding row in the sheet
                    for (int i = 0; i < rowCount; i++)
                    {
                        for (int j = 0; j < colCount; j++)
                        {
                            switch (j)
                            {
                                case 0://onoff
                                       // if (i == 0)
                                    dpAddMinusInstructions[i, j] = LocalResource.CELL_ON;
                                    break;

                                case 1://checkcross
                                    break;

                                case 2://criteria variable
                                    if (Check_Criteria.IsChecked == true) //include left-hand side values only if criteria is checked
                                    {
                                        if (i == 0 && !string.IsNullOrEmpty(Criteria_Control.Combo_Conditional_Item_1.Text))
                                            dpAddMinusInstructions[i, j] = Criteria_Control.Combo_Conditional_Item_1.Text;
                                        else if (i == 1 && !string.IsNullOrEmpty(Criteria_Control.Combo_Conditional_Item_2.Text))
                                            dpAddMinusInstructions[i, j] = Criteria_Control.Combo_Conditional_Item_2.Text;
                                        else if (i == 2 && !string.IsNullOrEmpty(Criteria_Control.Combo_Conditional_Item_3.Text))
                                            dpAddMinusInstructions[i, j] = Criteria_Control.Combo_Conditional_Item_3.Text;
                                        else if (i == 3 && !string.IsNullOrEmpty(Criteria_Control.Combo_Conditional_Item_4.Text))
                                            dpAddMinusInstructions[i, j] = Criteria_Control.Combo_Conditional_Item_4.Text;
                                        else if (i == 4 && !string.IsNullOrEmpty(Criteria_Control.Combo_Conditional_Item_5.Text))
                                            dpAddMinusInstructions[i, j] = Criteria_Control.Combo_Conditional_Item_5.Text;
                                    }

                                    break;

                                case 3://criteria operator
                                    if (Check_Criteria.IsChecked == true) //include left-hand side values only if criteria is checked
                                    {
                                        if (i == 0 && !string.IsNullOrEmpty(Criteria_Control.Combo_Conditional_Operator_1.Text))
                                            dpAddMinusInstructions[i, j] = Criteria_Control.Combo_Conditional_Operator_1.Text;
                                        else if (i == 1 && !string.IsNullOrEmpty(Criteria_Control.Combo_Conditional_Operator_2.Text))
                                            dpAddMinusInstructions[i, j] = Criteria_Control.Combo_Conditional_Operator_2.Text;
                                        else if (i == 2 && !string.IsNullOrEmpty(Criteria_Control.Combo_Conditional_Operator_3.Text))
                                            dpAddMinusInstructions[i, j] = Criteria_Control.Combo_Conditional_Operator_3.Text;
                                        else if (i == 3 && !string.IsNullOrEmpty(Criteria_Control.Combo_Conditional_Operator_4.Text))
                                            dpAddMinusInstructions[i, j] = Criteria_Control.Combo_Conditional_Operator_4.Text;
                                        else if (i == 4 && !string.IsNullOrEmpty(Criteria_Control.Combo_Conditional_Operator_5.Text))
                                            dpAddMinusInstructions[i, j] = Criteria_Control.Combo_Conditional_Operator_5.Text;
                                    }

                                    break;

                                case 4://criteria value
                                    if (Check_Criteria.IsChecked == true) //include left-hand side values only if criteria is checked
                                    {
                                        if (i == 0 && !string.IsNullOrEmpty(Criteria_Control.Combo_Conditional_Value_1.Text))
                                        {
                                            qs = questionSettings.FirstOrDefault(q => q.Variable.Equals(Criteria_Control.Combo_Conditional_Value_1.Text, StringComparison.OrdinalIgnoreCase));
                                            if (qs != null)
                                            {
                                                dpAddMinusInstructions[i, j] = qs.Variable;
                                            }
                                            else
                                            {
                                                dpAddMinusInstructions[i, j] = Criteria_Control.Combo_Conditional_Value_1.Text;
                                            }
                                        }
                                        else if (i == 1 && !string.IsNullOrEmpty(Criteria_Control.Combo_Conditional_Value_2.Text))
                                        {
                                            qs = questionSettings.FirstOrDefault(q => q.Variable.Equals(Criteria_Control.Combo_Conditional_Value_2.Text, StringComparison.OrdinalIgnoreCase));
                                            if (qs != null)
                                            {
                                                dpAddMinusInstructions[i, j] = qs.Variable;
                                            }
                                            else
                                            {
                                                dpAddMinusInstructions[i, j] = Criteria_Control.Combo_Conditional_Value_2.Text;
                                            }

                                        }
                                        else if (i == 2 && !string.IsNullOrEmpty(Criteria_Control.Combo_Conditional_Value_3.Text))
                                        {
                                            qs = questionSettings.FirstOrDefault(q => q.Variable.Equals(Criteria_Control.Combo_Conditional_Value_3.Text, StringComparison.OrdinalIgnoreCase));
                                            if (qs != null)
                                            {
                                                dpAddMinusInstructions[i, j] = qs.Variable;
                                            }
                                            else
                                            {
                                                dpAddMinusInstructions[i, j] = Criteria_Control.Combo_Conditional_Value_3.Text;
                                            }
                                        }
                                        else if (i == 3 && !string.IsNullOrEmpty(Criteria_Control.Combo_Conditional_Value_4.Text))
                                        {
                                            qs = questionSettings.FirstOrDefault(q => q.Variable.Equals(Criteria_Control.Combo_Conditional_Value_4.Text, StringComparison.OrdinalIgnoreCase));
                                            if (qs != null)
                                            {
                                                dpAddMinusInstructions[i, j] = qs.Variable;
                                            }
                                            else
                                            {
                                                dpAddMinusInstructions[i, j] = Criteria_Control.Combo_Conditional_Value_4.Text;
                                            }
                                        }
                                        else if (i == 4 && !string.IsNullOrEmpty(Criteria_Control.Combo_Conditional_Value_5.Text))
                                        {
                                            qs = questionSettings.FirstOrDefault(q => q.Variable.Equals(Criteria_Control.Combo_Conditional_Value_5.Text, StringComparison.OrdinalIgnoreCase));
                                            if (qs != null)
                                            {
                                                dpAddMinusInstructions[i, j] = qs.Variable;
                                            }
                                            else
                                            {
                                                dpAddMinusInstructions[i, j] = Criteria_Control.Combo_Conditional_Value_5.Text;
                                            }
                                        }
                                    }

                                    break;

                                case 5://AND || OR|| THEN
                                    if (Check_Criteria.IsChecked == true) //include left-hand side values only if criteria is checked
                                    {
                                        if (i == 0)
                                        {
                                            if (Criteria_Control.Option_Conditional_And_1.IsChecked == false && Criteria_Control.Option_Conditional_Or_1.IsChecked == false)
                                                dpAddMinusInstructions[i, j] = ExcelAddIn.Common.Constants.DP.InstructionTHEN;
                                            else if (Criteria_Control.Option_Conditional_And_1.IsChecked == true && Criteria_Control.Option_Conditional_Or_1.IsChecked == false)
                                                dpAddMinusInstructions[i, j] = ExcelAddIn.Common.Constants.DP.InstructionAND;
                                            else if (Criteria_Control.Option_Conditional_And_1.IsChecked == false && Criteria_Control.Option_Conditional_Or_1.IsChecked == true)
                                                dpAddMinusInstructions[i, j] = ExcelAddIn.Common.Constants.DP.InstructionOR;
                                        }
                                        else if (i == 1)
                                        {
                                            if (Criteria_Control.Option_Conditional_And_2.IsChecked == false && Criteria_Control.Option_Conditional_Or_2.IsChecked == false)
                                                dpAddMinusInstructions[i, j] = ExcelAddIn.Common.Constants.DP.InstructionTHEN;
                                            else if (Criteria_Control.Option_Conditional_And_2.IsChecked == true && Criteria_Control.Option_Conditional_Or_2.IsChecked == false)
                                                dpAddMinusInstructions[i, j] = ExcelAddIn.Common.Constants.DP.InstructionAND;
                                            else if (Criteria_Control.Option_Conditional_And_2.IsChecked == false && Criteria_Control.Option_Conditional_Or_2.IsChecked == true)
                                                dpAddMinusInstructions[i, j] = ExcelAddIn.Common.Constants.DP.InstructionOR;
                                        }
                                        else if (i == 2)
                                        {
                                            if (Criteria_Control.Option_Conditional_And_3.IsChecked == false && Criteria_Control.Option_Conditional_Or_3.IsChecked == false)
                                                dpAddMinusInstructions[i, j] = ExcelAddIn.Common.Constants.DP.InstructionTHEN;
                                            else if (Criteria_Control.Option_Conditional_And_3.IsChecked == true && Criteria_Control.Option_Conditional_Or_3.IsChecked == false)
                                                dpAddMinusInstructions[i, j] = ExcelAddIn.Common.Constants.DP.InstructionAND;
                                            else if (Criteria_Control.Option_Conditional_And_3.IsChecked == false && Criteria_Control.Option_Conditional_Or_3.IsChecked == true)
                                                dpAddMinusInstructions[i, j] = ExcelAddIn.Common.Constants.DP.InstructionOR;
                                        }
                                        else if (i == 3)
                                        {
                                            if (Criteria_Control.Option_Conditional_And_4.IsChecked == false && Criteria_Control.Option_Conditional_Or_4.IsChecked == false)
                                                dpAddMinusInstructions[i, j] = ExcelAddIn.Common.Constants.DP.InstructionTHEN;
                                            else if (Criteria_Control.Option_Conditional_And_4.IsChecked == true && Criteria_Control.Option_Conditional_Or_4.IsChecked == false)
                                                dpAddMinusInstructions[i, j] = ExcelAddIn.Common.Constants.DP.InstructionAND;
                                            else if (Criteria_Control.Option_Conditional_And_4.IsChecked == false && Criteria_Control.Option_Conditional_Or_4.IsChecked == true)
                                                dpAddMinusInstructions[i, j] = ExcelAddIn.Common.Constants.DP.InstructionOR;
                                        }
                                        else if (i == 4)
                                            dpAddMinusInstructions[i, j] = ExcelAddIn.Common.Constants.DP.InstructionTHEN;
                                    }
                                    else
                                        dpAddMinusInstructions[i, j] = ExcelAddIn.Common.Constants.DP.InstructionTHEN;

                                    break;

                                case 6://variable to revise
                                    if (dpAddMinusInstructions[i, j - 1] == ExcelAddIn.Common.Constants.DP.InstructionTHEN)
                                        dpAddMinusInstructions[i, j] = Combo_VariablesToRevise.Text;
                                    break;

                                case 7://instruction
                                    if (!string.IsNullOrEmpty(dpAddMinusInstructions[i, j - 1]) && (dpAddMinusInstructions[i, j - 1] == Combo_VariablesToRevise.Text))
                                    {
                                        if (Combo_RevisionMethod.Text == rvMethod.EQUAL_Desc)
                                        {
                                            dpAddMinusInstructions[i, j] = ExcelAddIn.Common.Constants.DP.SubstituteOperatorEQUAL;
                                            command = ExcelAddIn.Common.Constants.DP.SubstituteOperatorEQUAL;
                                        }
                                        else if (Combo_RevisionMethod.Text == rvMethod.ADD_Desc && Text_ReviseItem_AnswerType.Text == ExcelAddIn.Common.Constants.AnswerType.MA)
                                        {
                                            if (Option_Choice.IsChecked == true && !string.IsNullOrEmpty(Combo_RevisedChoice.Text))
                                            {
                                                dpAddMinusInstructions[i, j] = ExcelAddIn.Common.Constants.DP.SubstituteOperatorADD2;
                                                command = ExcelAddIn.Common.Constants.DP.SubstituteOperatorADD2;
                                            }
                                            else if (Option_Variable.IsChecked == true && !string.IsNullOrEmpty(Combo_RevisedVariable.Text))
                                            {
                                                dpAddMinusInstructions[i, j] = ExcelAddIn.Common.Constants.DP.SubstituteOperatorADD1;
                                                command = ExcelAddIn.Common.Constants.DP.SubstituteOperatorADD1;
                                            }
                                        }
                                        else if (Combo_RevisionMethod.Text == rvMethod.MINUS_Desc && Text_ReviseItem_AnswerType.Text == ExcelAddIn.Common.Constants.AnswerType.MA)
                                        {
                                            if (Option_Choice.IsChecked == true && !string.IsNullOrEmpty(Combo_RevisedChoice.Text))
                                            {
                                                dpAddMinusInstructions[i, j] = ExcelAddIn.Common.Constants.DP.SubstituteOperatorMINUS2;
                                                command = ExcelAddIn.Common.Constants.DP.SubstituteOperatorMINUS2;
                                            }
                                            else if (Option_Variable.IsChecked == true && !string.IsNullOrEmpty(Combo_RevisedVariable.Text))
                                            {
                                                dpAddMinusInstructions[i, j] = ExcelAddIn.Common.Constants.DP.SubstituteOperatorMINUS1;
                                                command = ExcelAddIn.Common.Constants.DP.SubstituteOperatorMINUS1;
                                            }
                                        }
                                    }
                                    break;

                                default://revised value
                                    if (!string.IsNullOrEmpty(dpAddMinusInstructions[i, j - 1]) && (dpAddMinusInstructions[i, j - 1] == command))
                                    {
                                        if (Option_Choice.IsChecked == true && !string.IsNullOrEmpty(Combo_RevisedChoice.Text))
                                            dpAddMinusInstructions[i, j] = Combo_RevisedChoice.Text;
                                        else if (Option_Variable.IsChecked == true && !string.IsNullOrEmpty(Combo_RevisedVariable.Text))
                                            dpAddMinusInstructions[i, j] = Combo_RevisedVariable.Text;
                                        else if (Option_No_Answer.IsChecked == true)
                                            dpAddMinusInstructions[i, j] = LocalResource.LABEL_DK;
                                        else if (Option_Excluded.IsChecked == true)
                                            dpAddMinusInstructions[i, j] = LocalResource.LABEL_STAR;
                                        else if (Option_Free_Entry.IsChecked == true && !string.IsNullOrEmpty(Textbox_Free_Entry.Text))
                                            dpAddMinusInstructions[i, j] = Textbox_Free_Entry.Text;
                                    }
                                    break;

                            }
                        }
                    }
                    string question = Text_ReviseItem_Question.Text.TrimEnd().TrimEnd();
                    if (question.Length > QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit)
                    {
                        question = question.PadRight(QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit).Substring(0, QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit);
                    }
                    if (dbHelper.WriteProcess(workbook, Util.Constants.ProcessingType.ReviseData, null, Text_ReviseItem_AnswerType.Text, question,
                       int.Parse(Text_ReviseItem_CategoryCount.Text), null, command, dpAddMinusInstructions, isNewQuestion, writeRow, processingOption, null, isUpdateQuestion))//need to pass the entire row from here for saving 
                    {
                        MessageDialog.Info(LocalResource.ADDQS_INFO_MSG_SUCCESS);
                        isModifiedProcess = true;
                        this.Close();
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {
                    _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                }
            }
        }

        private bool ValidateRevisedChoice(string value, string count)
        {
            bool isValid = true;
            int catcount = Convert.ToInt32(count);
            if (!ValidateFn1(value, catcount))
            {
                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_REVISEDATA_IMPROPER_CHOICE) + "\n" + string.Format(LocalResource.ERR_MSG_INTEGRATE_SET_INTEGER_RANGE_OF, "1", catcount.ToString()));
                return false;
            }
            if (!ValidateFn2(value, catcount))
            {
                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_REVISEDATA_IMPROPER_CHOICE) + "\n" + LocalResource.ERR_MSG_INTEGRATE_SET_A_NUMERIC_VALUE);
                return false;
            }
            if (!frmutil.IsNotOtherThanStart(value))
            {
                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_REVISEDATA_IMPROPER_CHOICE) + "\n" + LocalResource.SUBTOTAL_EROOR_ALERT_EXCLA_START);
                return false;
            }
            return isValid;
        }

        public bool ValidateFn1(string textvalue, int categorycount)
        {
            int minVal = 1;
            string val = Regex.Replace(textvalue, @"\s+", "").Replace('/', ',');
            string[] commaSplit = val.Replace("!", "").Split(',');
            try
            {
                if (commaSplit.Length > 1)
                {
                    for (int i = 0; i < commaSplit.Length; i++)
                    {
                        string rplcAt = commaSplit[i].Replace('(', '@').Replace(')', '@');
                        string[] atSplit = rplcAt.Split('@');
                        if (!rplcAt.Contains("@"))
                        {
                            string[] split = commaSplit[i].Split('-');
                            if (split.Length >= 2 && split[0] != "" && split[1] != "")
                                atSplit = split;
                            for (int n = 0; n < atSplit.Length; n++)
                            {
                                if (atSplit[n] != "" && atSplit[n] != "-")
                                {
                                    if (atSplit[n].LastIndexOf('-') == atSplit[n].Length - 1)
                                    {
                                        atSplit[n] = atSplit[n].Remove(atSplit[n].Length - 1);
                                    }
                                    if (VB.Information.IsNumeric(atSplit[n]))
                                    {
                                        decimal valu = Convert.ToDecimal(atSplit[n]);
                                        if ((atSplit.Length == 1 && valu < 0))
                                            valu = valu * -1;
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
                        }
                    }
                }
                else if (commaSplit.Length == 1)
                {
                    string rplcAt = commaSplit[0].Replace('(', '@').Replace(')', '@');
                    string[] atSplit = rplcAt.Split('@');
                    if (!rplcAt.Contains("@"))
                    {
                        string[] split = commaSplit[0].Split('-');
                        if (split.Length >= 2 && split[0] != "" && split[1] != "")
                            atSplit = split;
                    }
                    for (int n = 0; n < atSplit.Length; n++)
                    {
                        if (atSplit[n] != "" && atSplit[n] != "-")
                        {
                            if (atSplit[n].LastIndexOf('-') == atSplit[n].Length - 1)
                            {
                                atSplit[n] = atSplit[n].Remove(atSplit[n].Length - 1);
                            }
                            if (VB.Information.IsNumeric(atSplit[n]))
                            {
                                decimal valu = Convert.ToDecimal(atSplit[n]);
                                if ((atSplit.Length == 1 && valu < 0))
                                    valu = valu * -1;
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
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
            return true;
        }

        public bool ValidateFn2(string value, int maxVal = 0, int minVal = 1, bool blankCheck = false, bool numberObly = false, bool noSplit = false)
        {
            string val = Regex.Replace(value, @"\s+", "").Replace('/', ',');
            string[] commaSplit = val.Replace("!", "").Split(',');
            decimal parentFirstVal = 0;
            decimal parentSecondVal = 0;

            try
            {
                if (commaSplit.Length > 1)
                {
                    for (int i = 0; i < commaSplit.Length; i++)
                    {
                        decimal? childFirstVal = null;
                        decimal? childSecondVal = null;
                        string[] iphanSplit = commaSplit[i].Split('-');
                        int nullCount = 0;
                        for (int j = 0; j < iphanSplit.Length; j++)
                        {
                            if (iphanSplit[j] == "")
                                nullCount++;
                            if (nullCount > 1)
                                return false;
                            if (iphanSplit[j] == "")
                                return false;
                            if (iphanSplit[j].Contains('(') && iphanSplit[j].Contains(')'))
                                return false;
                            if ((iphanSplit[j].Contains('(') && iphanSplit[j].LastIndexOf('(') != 0) ||
                            (iphanSplit[j].Contains(')') && iphanSplit[j].IndexOf(')') != iphanSplit[j].Length - 1))
                                return false;
                            if ((iphanSplit[j].Contains('(') && (iphanSplit.Length < j + 2 || !iphanSplit[j + 1].Contains(')')))
                                || (iphanSplit[j].Contains(')') && (j == 0 || !iphanSplit[j - 1].Contains('('))))
                                return false;
                        }
                        string rplcAt = commaSplit[i].Replace('(', '@').Replace(')', '@');
                        string[] atSplit = rplcAt.Split('@');
                        if (!rplcAt.Contains("@"))
                        {
                            string[] split = commaSplit[i].Split('-');
                            if (split.Length >= 2 && split[0] != "" && split[1] != "")
                                atSplit = split;
                        }
                        bool isRangeValue = false;
                        for (int n = 0; n < atSplit.Length; n++)
                        {
                            if (atSplit[n] != "" && atSplit[n] != "-")
                            {
                                if (atSplit[n].LastIndexOf('-') == atSplit[n].Length - 1)
                                {
                                    atSplit[n] = atSplit[n].Remove(atSplit[n].Length - 1);
                                    isRangeValue = true;
                                }
                                if (!VB.Information.IsNumeric(atSplit[n]))
                                {
                                    return false;
                                }
                                else
                                {
                                    decimal valu = Convert.ToDecimal(atSplit[n]);
                                    if ((atSplit[n].Contains('-') && childFirstVal != null && atSplit[n - 1] != "-" && !isRangeValue && valu < 0) ||
                                        (atSplit.Length == 1 && valu < 0))
                                        valu = valu * -1;
                                    if (0 < maxVal)
                                    {
                                        if (valu < minVal)
                                        {
                                            return false;
                                        }

                                        if (maxVal < valu)
                                        {
                                            return false;
                                        }

                                    }
                                    if (childFirstVal != null && childSecondVal != null)
                                        return false;
                                    if (childFirstVal == null)
                                        childFirstVal = valu;
                                    else if (childSecondVal == null)
                                        childSecondVal = valu;
                                    if (parentFirstVal == 0)
                                        parentFirstVal = valu;
                                    else if (parentSecondVal == 0)
                                        parentSecondVal = valu;
                                    else
                                    {
                                        parentFirstVal = parentSecondVal;
                                        parentSecondVal = valu;
                                    }
                                    if (parentSecondVal != 0 && parentFirstVal >= parentSecondVal)
                                        return false;
                                    if (childSecondVal != 0 && childFirstVal >= childSecondVal)
                                        return false;
                                }
                            }
                        }
                    }
                }
                else if (commaSplit.Length == 1)
                {
                    decimal? childFirstVal = null;
                    decimal? childSecondVal = null;
                    string[] iphanSplit = commaSplit[0].Split('-');
                    int nullCount = 0;
                    for (int j = 0; j < iphanSplit.Length; j++)
                    {
                        if (iphanSplit[j] == "")
                            nullCount++;
                        if (nullCount > 1)
                            return false;
                        if (iphanSplit[j] == "" && (j != 0 && j != iphanSplit.Length - 1))
                            return false;
                        if (iphanSplit[j].Contains('(') && iphanSplit[j].Contains(')'))
                            return false;
                        if ((iphanSplit[j].Contains('(') && iphanSplit[j].LastIndexOf('(') != 0) ||
                        (iphanSplit[j].Contains(')') && iphanSplit[j].IndexOf(')') != iphanSplit[j].Length - 1))
                            return false;
                        if ((iphanSplit[j].Contains('(') && (iphanSplit.Length < j + 2 || !iphanSplit[j + 1].Contains(')')))
                            || (iphanSplit[j].Contains(')') && (j == 0 || !iphanSplit[j - 1].Contains('('))))
                            return false;
                    }
                    string rplcAt = commaSplit[0].Replace('(', '@').Replace(')', '@');
                    string[] atSplit = rplcAt.Split('@');
                    if (!rplcAt.Contains("@"))
                    {
                        string[] split = commaSplit[0].Split('-');
                        if (split.Length >= 2 && split[0] != "" && split[1] != "")
                            atSplit = split;
                    }
                    bool isRangeValue = false;
                    for (int n = 0; n < atSplit.Length; n++)
                    {
                        if (atSplit[n] != "" && atSplit[n] != "-")
                        {
                            if (atSplit[n].LastIndexOf('-') == atSplit[n].Length - 1)
                            {
                                atSplit[n] = atSplit[n].Remove(atSplit[n].Length - 1);
                                isRangeValue = true;
                            }
                            if (!VB.Information.IsNumeric(atSplit[n]))
                            {
                                return false;
                            }
                            else
                            {
                                decimal valu = Convert.ToDecimal(atSplit[n]);
                                if ((atSplit[n].Contains('-') && childFirstVal != null && atSplit[n - 1] != "-" && !isRangeValue && valu < 0) ||
                                    (atSplit.Length == 1 && valu < 0))
                                    valu = valu * -1;
                                if (0 < maxVal)
                                {
                                    if (valu < minVal)
                                    {
                                        return false;
                                    }

                                    if (maxVal < valu)
                                    {
                                        return false;
                                    }

                                }
                                if (childFirstVal != null && childSecondVal != null)
                                    return false;
                                if (childFirstVal == null)
                                    childFirstVal = valu;
                                else if (childSecondVal == null)
                                    childSecondVal = valu;
                                if (childSecondVal != 0 && childFirstVal > childSecondVal)
                                    return false;

                                if (parentFirstVal == 0)
                                    parentFirstVal = valu;
                                else
                                    parentSecondVal = valu;
                                if (parentSecondVal != 0 && parentFirstVal >= parentSecondVal)
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
        #endregion

        #region Revise Data- Edit and Copy process
        /// <summary>Logic for populating Revise data window for Edit and Copy processes.</summary>
        /// <param name="dpRange">The range in the hidden sheet where the process was saved.</param>
        /// <param name="objArray">The values stored in the range, used to retrieve command, new variable and all parameters.</param>
        private void PopulateValuesToModify(Excel.Range dpRange, object[,] objArray)
        {
            try
            {
                for (int i = 1; i <= objArray.GetLength(0); i++)
                {
                    if (objArray[i, QC4Common.Common.Constants.DP.CriteriaVariableColumn] != null)
                    {
                        string criteriaVariable = objArray[i, QC4Common.Common.Constants.DP.CriteriaVariableColumn].ToString();
                        if (i == 1)
                        {
                            Check_Criteria.IsChecked = true;
                            Criteria_Control.EnableCriteriaControl();
                        }
                        Criteria_Control.SetSelectedCriteriaVariable(criteriaVariable, i);
                        Criteria_Control.SetSelectedCriteriaOperator(objArray[i, QC4Common.Common.Constants.DP.CriteriaOperatorColumn].ToString(), i);
                        Criteria_Control.SetSelectedCriteriaValue(objArray[i, QC4Common.Common.Constants.DP.CriteriavalueColumn].ToString(), i);
                    }
                    string instruction = objArray[i, QC4Common.Common.Constants.DP.InstructionColumn].ToString();
                    if (instruction == QC4Common.Common.Constants.DP.InstructionAND || instruction == QC4Common.Common.Constants.DP.InstructionOR)
                    {
                        Criteria_Control.SetSelectedConditionalOption(instruction, i);
                    }
                    else if (instruction == QC4Common.Common.Constants.DP.InstructionTHEN)
                    {
                        string selectedVariable = objArray[i, QC4Common.Common.Constants.DP.SubstituteVariableColumn].ToString();
                        if (!string.IsNullOrEmpty(selectedVariable))
                        {
                            isSelected = true;
                            foreach (var item in variablesToRevise)
                            {
                                if (item.QuestionVariable == selectedVariable)
                                {
                                    Combo_VariablesToRevise.SelectedItem = item;
                                    break;
                                }
                            }
                        }
                        substOperator = objArray[i, QC4Common.Common.Constants.DP.SubstituteOperatorColumn].ToString();
                        substParam = objArray[i, QC4Common.Common.Constants.DP.SubstituteParam1Column].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void SetSubstituteValuesOnLoad(string op, string param, string answertype)
        {
            try
            {
                if (!string.IsNullOrEmpty(op))
                {
                    if (op == QC4Common.Common.Constants.DP.SubstituteOperatorEQUAL)
                        Combo_RevisionMethod.SelectedIndex = 0;
                    else if (op == QC4Common.Common.Constants.DP.SubstituteOperatorADD1 || op == QC4Common.Common.Constants.DP.SubstituteOperatorADD2)
                        Combo_RevisionMethod.SelectedIndex = 1;
                    else if (op == QC4Common.Common.Constants.DP.SubstituteOperatorMINUS1 || op == QC4Common.Common.Constants.DP.SubstituteOperatorMINUS2)
                        Combo_RevisionMethod.SelectedIndex = 2;
                }
                if (!string.IsNullOrEmpty(param))
                {
                    if (float.TryParse(param, out _) && answertype == QC4Common.Common.Constants.AnswerType.N)
                    {
                        Option_Free_Entry.IsChecked = true;
                        Textbox_Free_Entry.IsEnabled = true;
                        Textbox_Free_Entry.Background = new SolidColorBrush(Colors.White);
                        isDecimalPressed = false;
                        isMinusPressed = false;
                        Textbox_Free_Entry.Text = param;
                        isFreeEntryClicked = true;
                    }
                    else if (int.TryParse(param, out _) && (answertype == QC4Common.Common.Constants.AnswerType.MA || answertype == QC4Common.Common.Constants.AnswerType.SA))
                    {
                        Option_Choice.IsChecked = true;
                        var indx = -1;
                        Combo_RevisedChoice.IsEnabled = true;
                        Combo_RevisedChoice.Background = new SolidColorBrush(Colors.White);
                        Combo_RevisedChoice.DataContext = null;
                        revisedChoice_SelectCount = new List<string>();
                        for (int i = 0; i < variablesToRevise[selectedindex_VariableToRevise].Choisces.Count; i++)
                        {
                            var ch = (i + 1).ToString();
                            revisedChoice_SelectCount.Add(ch);
                            if (ch == param)
                                indx = i;
                        }
                        Combo_RevisedChoice.DataContext = revisedChoice_SelectCount;
                        if (indx == -1)
                            Combo_RevisedChoice.Text = param;
                        else
                            Combo_RevisedChoice.SelectedIndex = indx;
                    }
                    else if (param == LocalResource.LABEL_DK)
                        Option_No_Answer.IsChecked = true;
                    else if (param == LocalResource.LABEL_STAR)
                        Option_Excluded.IsChecked = true;
                    else if (answertype == QC4Common.Common.Constants.AnswerType.FA && param != LocalResource.LABEL_DK && param != LocalResource.LABEL_STAR)
                    {
                        Option_Free_Entry.IsChecked = true;
                        Textbox_Free_Entry.IsEnabled = true;
                        Textbox_Free_Entry.Background = new SolidColorBrush(Colors.White);
                        isDecimalPressed = false;
                        isMinusPressed = false;
                        Textbox_Free_Entry.Text = param;
                        isFreeEntryClicked = true;
                    }
                    else
                    {
                        if (answertype == QC4Common.Common.Constants.AnswerType.SA || (answertype == QC4Common.Common.Constants.AnswerType.MA && dictionary.ContainsKey(param)) || (answertype == QC4Common.Common.Constants.AnswerType.N && dictionary.ContainsKey(param)))//[UAT1][Redmine id: 205856]
                        {
                            Option_Variable.IsChecked = true;
                            Combo_RevisedVariable.IsEnabled = true;
                            Combo_RevisedVariable.Background = new SolidColorBrush(Colors.White);
                            Combo_RevisedVariable_LoadData(variableToRevise_selectedQuestionVariableType[0]);
                            foreach (var item in revisedVariableList)
                            {
                                if (item.QuestionVariable == param)
                                {
                                    Combo_RevisedVariable.SelectedItem = item;
                                    break;
                                }
                            }
                        }
                        if (answertype == QC4Common.Common.Constants.AnswerType.MA && !dictionary.ContainsKey(param))
                        {
                            Option_Choice.IsChecked = true;
                            Combo_RevisedChoice.IsEnabled = true;
                            Combo_RevisedChoice.Background = new SolidColorBrush(Colors.White);
                            Combo_RevisedChoice.DataContext = null;
                            revisedChoice_SelectCount = new List<string>();
                            for (int i = 0; i < variablesToRevise[selectedindex_VariableToRevise].Choisces.Count; i++)
                            {
                                var ch = (i + 1).ToString();
                                revisedChoice_SelectCount.Add(ch);
                            }
                            Combo_RevisedChoice.DataContext = revisedChoice_SelectCount;
                            Combo_RevisedChoice.Text = param;
                        }
                    }
                }
                Command_Entry.IsEnabled = true;
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

        private void Choices_grid_CopyingRowClipboardContent(object sender, DataGridRowClipboardEventArgs e)
        {
            try
            {
                //Removes serial column from copying
                e.ClipboardRowContent.RemoveAt(0);
            }
            catch (Exception ex) { _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException); }
        }

        private void Grid_ReviseItem_Choices_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                Option_Choice.Focus();
                e.Handled = true;
            }
            else
                QC4Common.Util.GridCommonFunc.Arrow(sender, e);
        }

        private void Textbox_Free_Entry_LostFocus(object sender, RoutedEventArgs e)//[Redmine id: 189510]
        {
            try
            {
                if ((variableToRevise_selectedQuestionVariableType[0] == QC4Common.Common.Constants.AnswerType.N))////https://app.gluemodel.com/#/project/task/4295061352
                {
                    string input = Textbox_Free_Entry.Text.Normalize(NormalizationForm.FormKC);
                    if (!string.IsNullOrEmpty(input))
                    {
                        string aa = input;
                        string sub = string.Empty;
                        string temp = string.Empty;
                        for (int i = 0; i < aa.Length; i++)
                        {
                            if (new Regex(@"^[0-9.-]+$").IsMatch(Convert.ToString(aa[i])))//-?[0-9]\d*(\.\d+)?$   //@"^[-+]?\d+(\.\d+)?$"
                            {
                                sub = sub + aa[i];
                                if (string.Equals(sub, ".") && !aa.Contains("."))
                                {
                                    sub = "0.";
                                }
                            }
                        }
                        aa = sub;
                        if (aa.Length >= 1)
                        {
                            if (aa.StartsWith("."))
                            {
                                aa = "0" + aa;
                            }
                            if (aa.EndsWith("."))
                            {
                                aa =  aa+ "0";
                            }
                            if (aa.Contains("-") && aa.Length == 1)
                            {
                                aa = "0";
                            }
                            if (aa.Contains("-"))
                            {
                                int countofseperator = aa.Count(f => (f == '-'));
                                aa = aa.Trim('-');
                                aa = "-" + aa;
                            }
                            if (!new Regex(@"^[-+]?\d+(\.\d+)?$").IsMatch(aa))
                            {
                                Textbox_Free_Entry.Text = "0";
                            }
                            else
                            {
                                if (aa.Length > QC4Common.Common.Constants.STD_DP.ADD_MINUS_MAX_Text_Length)
                                {
                                    aa = aa.Substring(0, QC4Common.Common.Constants.STD_DP.ADD_MINUS_MAX_Text_Length);
                                }
                                oldText = aa;
                                Textbox_Free_Entry.Text = aa;
                            }
                        }
                        else//Fullwidth alphabetsonly
                        {
                            oldText = "0";
                            Textbox_Free_Entry.Text = "0";
                        }
                    }
                }
            }
            catch { }
        }

        private void Textbox_Free_Entry_GotFocus(object sender, RoutedEventArgs e)
        {
            if ((variableToRevise_selectedQuestionVariableType[0] == QC4Common.Common.Constants.AnswerType.N))
            {
                System.Windows.Input.InputMethod.SetIsInputMethodEnabled(Textbox_Free_Entry, false);
            }
            else
            {
                System.Windows.Input.InputMethod.SetIsInputMethodEnabled(Textbox_Free_Entry, true);
            }
        }
    }
}
