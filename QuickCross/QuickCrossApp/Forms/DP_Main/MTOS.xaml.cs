using ExcelAddIn.Common;
using QC4Common.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Excel = Microsoft.Office.Interop.Excel;
using static FilterSettingsView.FilterSettingsClass;
using log4net;
using System.Reflection;
using Qc4Launcher.Logic;
using Qc4Launcher.Logic.DataImport;

namespace Qc4Launcher.Forms.DP_Main
{
    /// <summary>
    /// Interaction logic for MTOS.xaml
    /// </summary>
    public partial class MTOS : Window
    {
        QC4Common.Util.FormUtil frmutil = new QC4Common.Util.FormUtil();
        Excel.Workbook Workbook;
        DataTable dt, source_datatable;
        System.Windows.Controls.DataGrid ExpGrid = null;
        private int PreviousIndex = 1000;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        int RowNumberRead, RowNumberwrite;
        string processingType;
        string ProcessingOption;
        string Edit_SourceVariable;
        private bool isNewQuestion = false;
        public static List<string> MAvariables = new List<string>();
        string[] choicesList = new string[201];
        string[] Choices;
        public bool isModifiedProcess = false;
        private static List<DataGT> choiceListMA;
        bool choiceloadFirst = true;
        private Dictionary<String, QuestionSettings> PopulatedDictionary = new Dictionary<string, QuestionSettings>();
        private QC4Common.Util.FormUtil formUtil = new QC4Common.Util.FormUtil();
        private QC4Common.Util.QSUtil qsUtil = new QC4Common.Util.QSUtil();
        string clipboardText = "";
        private ObservableCollection<SourceVariableList> sourceVariableList = new ObservableCollection<SourceVariableList>();

        public ObservableCollection<SourceVariableList> SourceVariableListView
        {
            get
            {
                return sourceVariableList;
            }
            set
            {
                sourceVariableList = value;
            }
        }

        public MTOS(Excel.Workbook workbook, int readRow, int writeRow, string processing_type, string processing_option)
        {
            InitializeComponent();
            this.Workbook = workbook;
            processingType = processing_type;
            ProcessingOption = processing_option;
            RowNumberRead = readRow;
            RowNumberwrite = writeRow;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                int[] choices = new int[1001];
                Button_Search.IsEnabled = false;
                Button_Search.Opacity = .5;
                choices = Enumerable.Range(0, 1001).ToArray();
                choicesList = Array.ConvertAll(choices, ele => ele.ToString());
                choicesList[0] = LocalResource.LBL_AUTO;
                choicesCount.ItemsSource = choicesList;
                PopulateMAVariableList();
                string[] empty = new string[1000];
                dt = FillDataGrid(1000, empty);
                data_grid.DataContext = dt;
                choiceloadFirst = false;

                Color newVariableTextboxColor = (Color)ColorConverter.ConvertFromString("#F0F0F0");
                Text_New_Question.Background = new System.Windows.Media.SolidColorBrush(newVariableTextboxColor);
                txt_new_variable.Background = new System.Windows.Media.SolidColorBrush(newVariableTextboxColor);

                if (ProcessingOption == QC4Common.Common.Constants.STD_DP.Process_Edit || ProcessingOption == QC4Common.Common.Constants.STD_DP.Process_Copy)
                {
                    Excel.Worksheet dataProcessSheet = Util.ExcelUtil.GetWorkSheetByCodeName(Workbook, Qc4Launcher.Util.Constants.SheetCodeName.DataProcessS);
                    Excel.Range rowstart = dataProcessSheet.Cells[RowNumberRead, 1];
                    Excel.Range rowend = GetLastCellInRow(rowstart);
                    Excel.Range range_Edit_Copy = dataProcessSheet.Range[rowstart, rowend];
                    if (range_Edit_Copy.Cells.Count > 1)
                    {
                        QuestionSettings qs = new QuestionSettings();
                        var obj = range_Edit_Copy.Value;

                        //EDIT_OR_COPY_Process_Details.
                        Edit_Copy(range_Edit_Copy, ProcessingOption);

                    }
                    if (ProcessingOption == QC4Common.Common.Constants.STD_DP.Process_Edit)
                    {
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
        private void Edit_Copy(Excel.Range range_Edit_Copy, string ProcessingOption)
        {
            try
            {
                var obj = range_Edit_Copy.Value;
                string Newvariable = obj[1, QC4Common.Common.Constants.DP.SubstituteVariableColumn];
                string SourceVariable = obj[1, QC4Common.Common.Constants.DP.SubstituteParam1Column];
                Edit_SourceVariable = SourceVariable;
                QuestionSettings qsSource = new QuestionSettings();
                bool hasvalue = Util.Definiotion.VariableDictionary.TryGetValue(Newvariable, out qsSource);
                if (qsSource != null)
                {
                    string[] Choices = qsSource.Choices.ToArray();
                    txt_answerType.Text = qsSource.AnswerType;
                    txtSourceChoiceCount.Text = qsSource.Choices.Count.ToString();
                    txt_source_question.Text = frmutil.UnEscapeCRLF(qsSource.Question);
                    for (int i = 0; i < qsSource.Choices.Count; i++)
                    {
                        Choices[i] = Choices[i];
                    }
                    source_datatable = FillDataGrid(Choices.Length, Choices);
                    choices_grid.DataContext = source_datatable;
                }

                Combo_sourceVariable.Text = SourceVariable;
                txt_new_variable.IsEnabled = false;
                Text_New_Question.IsEnabled = true;
                SaveBtn.IsEnabled = true;
                Text_New_Question.Background = Brushes.White;
                txt_new_variable.Background = Brushes.White;

                choices_grid.IsEnabled = true;
                choices_grid.Background = Brushes.White;
                QuestionSettings qsNew = new QuestionSettings();
                bool hasvalue1 = Util.Definiotion.VariableDictionary.TryGetValue(Newvariable, out qsNew);
                if (qsNew != null)
                {
                    Choices = qsNew.Choices.ToArray();
                    for (int i = 0; i < qsNew.Choices.Count; i++)
                    {
                        Choices[i] = Choices[i];
                    }
                    string processingMethod = obj[1, QC4Common.Common.Constants.DP.SubstituteParam1Column + 1];
                    Cmb_Processing_Method.SelectedIndex = int.Parse(processingMethod) - 1;
                    Text_New_Question.Text = frmutil.UnEscapeCRLF(qsNew.Question);
                    choicesCount.SelectedIndex = qsNew.Choices.Count;
                    dt = FillDataGrid(Choices.Length, Choices);
                    data_grid.DataContext = dt;
                }

                if (ProcessingOption == QC4Common.Common.Constants.STD_DP.Process_Edit)
                {
                    txt_new_variable.Text = Newvariable;
                    txt_new_variable.IsEnabled = false;
                    txt_new_variable.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
                    choicesCount.IsEnabled = false;
                    Button_Search.IsEnabled = false;

                }
                else if (ProcessingOption == QC4Common.Common.Constants.STD_DP.Process_Copy)
                {
                    txt_new_variable.Text = qsUtil.GetVariableName(Newvariable, PopulatedDictionary.Values.ToList());
                    txt_new_variable.IsEnabled = true;
                    Button_Search.IsEnabled = true;
                }
                if (qsNew == null)
                {
                    txt_new_variable.Text = string.Empty;
                    Text_New_Question.Text = string.Empty;
                    string[] empty = new string[1000];
                    dt = FillDataGrid(1000, empty);
                    data_grid.DataContext = dt;
                    choicesCount.SelectedIndex = 0;
                    Cmb_Processing_Method.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        public Excel.Range GetLastCellInRow(Excel.Range targetCell)
        {
            Excel.Range targetColumn = targetCell.Cells[1].EntireRow;
            return targetColumn.Cells[targetColumn.Cells.Count].End(Excel.XlDirection.xlToLeft);
        }

        private void PopulateMAVariableList()
        {
            var SettingSheet = ExcelUtil.GetWorkSheetByCodeName(Workbook, "Sheet91");
            PopulatedDictionary = Util.Definiotion.VariableDictionary;
            try
            {
                if (SettingSheet != null)
                {
                    Microsoft.Office.Interop.Excel.Range Range = SettingSheet.get_Range("List_Item_MA");
                    if (Range != null)
                    {
                        MAvariables.Clear();
                        var objAry = Range.Value;
                        if (Range.Count == 1)
                        {
                            MAvariables.Add(objAry);
                            if (PopulatedDictionary.ContainsKey(objAry))
                            {
                                sourceVariableList.Add(new SourceVariableList()
                                {
                                    Variable = PopulatedDictionary[objAry].Variable,
                                    AnswerType = PopulatedDictionary[objAry].AnswerType,
                                    Question = frmutil.EscapeCRLF(PopulatedDictionary[objAry].Question),
                                    Choices = PopulatedDictionary[objAry].Choices,
                                });
                            }
                        }
                        else
                        {
                            int max = objAry.GetLength(0);
                            for (int i = 1; i <= max; i++)
                            {
                                if (objAry[i, 1] != null)
                                {

                                    MAvariables.Add(objAry[i, 1].ToString());
                                    foreach (KeyValuePair<string, QuestionSettings> item in PopulatedDictionary)
                                    {
                                        QuestionSettings qs = item.Value;
                                        if (qs.Variable == objAry[i, 1])
                                        {
                                            sourceVariableList.Add(new SourceVariableList()
                                            {
                                                Variable = qs.Variable,
                                                AnswerType = qs.AnswerType,
                                                Question = frmutil.EscapeCRLF(qs.Question),
                                                Choices = qs.Choices,
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
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public class SourceVariableList
        {
            private string variable;
            private string answertype;
            private string question;
            private List<string> choices;
            public string Variable
            {
                get
                {
                    return variable;
                }
                set
                {
                    variable = value;
                }
            }

            public string AnswerType
            {
                get
                {
                    return answertype;
                }
                set
                {
                    answertype = value;
                }
            }
            public string Question
            {
                get
                {
                    return question;
                }
                set
                {
                    question = value;
                }
            }
            public List<string> Choices
            {
                get
                {
                    return choices;
                }
                set
                {

                    choices = value;

                }

            }

            public string AnswerTypeWithNoOfChoices
            {
                get
                {
                    if (choices.Count == 0)
                    {
                        return answertype;
                    }
                    else
                    {
                        return string.Join("/", answertype, choices.Count);
                    }
                }
            }

        }


        private void Combo_sourceVariable_DropDownClosed(object sender, EventArgs e)
        {

        }

        private void NewItemSearchbutton_Click(object sender, RoutedEventArgs e)
        {
            if (MagnifyingGlassButton.VariableList != null)
            {
                string NewVariable = MagnifyingGlassButton.VariableList.Variable;
                bool flag = false;
                txt_new_variable.IsEnabled = true;
                Text_New_Question.IsEnabled = true;
                Text_New_Question.Background = Brushes.White;
                txt_new_variable.Background = Brushes.White;
                txt_new_variable.Text = NewVariable;
                Text_New_Question.Text = (frmutil.UnEscapeCRLF(MagnifyingGlassButton.VariableList.Title) + " " + frmutil.UnEscapeCRLF(MagnifyingGlassButton.VariableList.Question)).TrimStart().TrimEnd();
                choicesCount.SelectedIndex = MagnifyingGlassButton.VariableList.Choices.Count;
                Choices = MagnifyingGlassButton.VariableList.Choices.ToArray();
                dt = FillDataGrid(Choices.Length, Choices);
                data_grid.DataContext = dt;
            }
        }

        private DataTable CreateTable()
        {
            DataTable griddata = new DataTable();
            griddata.Columns.Add("No");
            griddata.Columns.Add("Choice");
            griddata.Columns.Add("IsBlank");
            return griddata;
        }
        private DataTable FillDataGrid(int limit, string[] choices)
        {
            DataTable griddata = CreateTable();
            DataRow dr;
            for (int i = 1; i <= limit; i++)
            {
                dr = griddata.NewRow();
                dr["No"] = i;
                dr["Choice"] = frmutil.EscapeCRLF(choices[i - 1]);
                dr["IsBlank"] = false;
                griddata.Rows.Add(dr);
            }
            return griddata;
        }

        private void MagnifyingGlassButton_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void ChoicesCount_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            System.Windows.Controls.ComboBox combo = (System.Windows.Controls.ComboBox)sender;
            int result = 0;

            try
            {
                if (int.TryParse(combo.Text, out result))
                {
                    if (result > 1000)
                    {
                        MessageDialog.ErrorOk(LocalResource.MSG_ALERT_COMBOBOXLIMIT);
                        combo.SelectedIndex = 1000;
                    }
                    else if (result == 0)
                    {
                        MessageDialog.ErrorOk(LocalResource.MSG_ALERT_COMBOBOXLIMIT);
                        combo.SelectedIndex = 1;
                    }
                    else if (result < 0)
                    {

                        MessageDialog.ErrorOk(LocalResource.MSG_ALERT_COMBOBOXLIMIT);
                    }
                }
                else
                {
                    if (!(combo.Text == LocalResource.LBL_AUTO))
                    {
                        MessageDialog.ErrorOk(LocalResource.RECODE_ALERT_MSG_COMBO_CATEGORY);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }


        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try { txt_new_variable.Text = txt_new_variable.Text.TrimStart().TrimEnd(); } catch { }
            bool Emptychoice = false;
            bool Isupdated = false;
            string newvariable = txt_new_variable.Text;
            Util.QS.NewQuestionValidation validation = new Util.QS.NewQuestionValidation(txt_new_variable.Text.TrimStart().TrimEnd());

            try
            {
                if (ProcessingOption == QC4Common.Common.Constants.STD_DP.Process_Edit)
                {
                    if (string.IsNullOrEmpty(newvariable))
                    {
                        string errormsg = LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_NEW_ITEM_EMPTY + "\n" + LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_RENAME_NEW_VARIABLE_AUTOMATICALLY;
                        System.Windows.Forms.DialogResult result;
                        result = MessageDialog.InfoYesNo(errormsg);
                        if (result == System.Windows.Forms.DialogResult.Yes)
                        {
                            //Get new variable name
                            QC4Common.Util.QSUtil qsutil = new QC4Common.Util.QSUtil();
                            newvariable = qsutil.GetVariableName(Combo_sourceVariable.Text, PopulatedDictionary.Values.ToList());
                            txt_new_variable.Text = newvariable;
                            isNewQuestion = true;
                            Isupdated = false;
                        }
                        else if (result == System.Windows.Forms.DialogResult.No)
                        {
                            return;
                        }
                    }
                    else
                    {
                        QuestionSettings qs = new QuestionSettings();
                        bool hasvalue = Util.Definiotion.VariableDictionary.TryGetValue(newvariable, out qs);
                        if (qs != null)
                        {
                            if (Text_New_Item.Text == qs.AnswerType && choicesCount.Text == qs.Choices.Count.ToString() && qs.Variable == txt_new_variable.Text)
                            {
                                if (!qs.Question.Equals(Text_New_Question.Text))
                                {
                                    Isupdated = true;
                                    isNewQuestion = false;
                                }

                                string[] Choices = qs.Choices.ToArray();
                                int i = 0;

                                foreach (DataRow dr in dt.Rows)
                                {
                                    if (!Convert.ToString(dr[1]).Equals(Convert.ToString(Choices[i])))// if (dr[1].ToString() != Choices[i])
                                    {
                                        isNewQuestion = false;
                                        Isupdated = true;
                                        break;
                                    }
                                    i++;
                                }

                            }
                        }
                    }

                }
                else if (ProcessingOption == QC4Common.Common.Constants.STD_DP.Process_Create || ProcessingOption == QC4Common.Common.Constants.STD_DP.Process_Copy)
                {
                    isNewQuestion = true;
                }
                string emptyChoiceRowNumber = "";
                int BlankChoiceCount = 0;
                int choiceCountEntered = 0;
                choiceCountEntered = frmutil.GetLastRow(dt, 1);

                int numberOfChoices = choicesCount.SelectedIndex;
                if (choicesCount.SelectedIndex == 0)
                {
                    numberOfChoices = choiceCountEntered;
                }
                else
                {
                    numberOfChoices = choicesCount.SelectedIndex;
                }
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr[1].ToString().TrimStart().TrimEnd() == string.Empty)
                    {
                        Emptychoice = true;
                        if (BlankChoiceCount == 0)
                        {
                            emptyChoiceRowNumber = dr[0].ToString();
                        }
                        BlankChoiceCount++;
                        break;
                    }
                }
                string newqn = Text_New_Question.Text.TrimStart().TrimEnd();

                if (newqn == "")
                {
                    MessageDialog.ErrorOk(LocalResource.ADDQS_ERROR_MSG_QUSTION_TEXT_MISS);
                    return;
                }
                if (choiceCountEntered == -1 && choicesCount.SelectedIndex == 0)
                {
                    MessageDialog.ErrorOk(LocalResource.ADD_DP_RECODE_ERROR_MSG_NO_CHOICE_SET);
                    return;
                }
                else if (choiceCountEntered == -1 && choicesCount.SelectedIndex != 0)
                {
                    MessageDialog.ErrorOk(LocalResource.ADD_DP_RECODE_ERROR_MSG_NO_MATCH_NUMBER_OF_CHOICES);
                    return;
                }
                for (int i = 0; i < choiceCountEntered; i++)
                {
                    if (string.IsNullOrEmpty(Convert.ToString(dt.Rows[i][1])) || string.IsNullOrEmpty(Convert.ToString(dt.Rows[i][1]).TrimStart().TrimEnd()))
                    {
                        MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, (emptyChoiceRowNumber)) + "\n" + (LocalResource.ERR_MSG_RECODE_CHOICE_IS_NOT_INPUT));
                        dt.Rows[i][2] = true;
                        return;
                    }
                    else
                    {
                        dt.Rows[i][2] = false;
                    }
                }
                if ((ProcessingOption == QC4Common.Common.Constants.STD_DP.Process_Edit) && (isNewQuestion == false) && (string.IsNullOrEmpty(txt_new_variable.Text.TrimStart().TrimEnd()) || !frmutil.IsVariableNameExists(txt_new_variable.Text, PopulatedDictionary.Values.ToList())))
                {

                }
                else if (string.IsNullOrEmpty(txt_new_variable.Text.TrimStart().TrimEnd()) || !frmutil.IsVariableNameExists(txt_new_variable.Text.TrimStart().TrimEnd(), PopulatedDictionary.Values.ToList(), 1))
                {
                    string errormsg = LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_NEW_ITEM_EMPTY + "\n" + LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_RENAME_NEW_VARIABLE_AUTOMATICALLY;
                    if (!string.IsNullOrEmpty(txt_new_variable.Text.TrimStart().TrimEnd()))
                    {
                        errormsg = LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_ITEM_NAME_ALREADY_EXISTS + "\n" + LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_RENAME_NEW_VARIABLE_AUTOMATICALLY;
                    }
                    System.Windows.Forms.DialogResult result;
                    result = MessageDialog.InfoYesNo(errormsg);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        string variablename = txt_new_variable.Text.TrimStart().TrimEnd();
                        if (string.IsNullOrEmpty(txt_new_variable.Text.TrimStart().TrimEnd()))
                        {
                            variablename = Combo_sourceVariable.Text;
                        }
                        //Get new variable name
                        QC4Common.Util.QSUtil qsutil = new QC4Common.Util.QSUtil();
                        txt_new_variable.Text = qsutil.GetVariableName(variablename, PopulatedDictionary.Values.ToList());
                    }
                    else if (result == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                }
                if ((Util.Definiotion.VariableDictionary.ContainsKey(txt_new_variable.Text)) && (
                    (Util.Definiotion.VariableDictionary[txt_new_variable.Text].AnswerType != Text_New_Item.Text) ||
                    (Util.Definiotion.VariableDictionary[txt_new_variable.Text].CategoryCount != numberOfChoices)))
                {
                    System.Windows.Forms.DialogResult result;
                    result = MessageDialog.InfoYesNo(LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_ITEM_NAME_ALREADY_EXISTS + "\n" + LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_RENAME_NEW_VARIABLE_AUTOMATICALLY);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        //Get new variable name
                        QC4Common.Util.QSUtil qsutil = new QC4Common.Util.QSUtil();
                        txt_new_variable.Text = qsutil.GetVariableName(txt_new_variable.Text, PopulatedDictionary.Values.ToList());
                    }
                    else if (result == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }

                    isNewQuestion = true;
                    Isupdated = false;
                }
                else if ((Util.Definiotion.VariableDictionary.ContainsKey(txt_new_variable.Text)) &&
                    (Util.Definiotion.VariableDictionary[txt_new_variable.Text].AnswerType == Text_New_Item.Text) &&
                    (Util.Definiotion.VariableDictionary[txt_new_variable.Text].CategoryCount == numberOfChoices)
                    )
                {
                    Isupdated = true;
                    isNewQuestion = false;
                }
                if (formUtil.IsVariableLengthExceeds(txt_new_variable.Text.TrimStart().TrimEnd()))
                {
                    //ERR_MSG_QUESTION_LENGTH_EXCEED
                    MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_QUESTION_LENGTH_EXCEED, QC4Common.Common.Constants.QsVariableMaxLength));
                    return;
                }
                if (ProcessingOption != QC4Common.Common.Constants.STD_DP.Process_Edit && !string.IsNullOrEmpty(txt_new_variable.Text.TrimStart().TrimEnd()))
                {
                    // check the maxlength of newVariable
                    if (formUtil.IsVariableLengthExceeds(txt_new_variable.Text.TrimStart().TrimEnd()))
                    {
                        //ERR_MSG_QUESTION_LENGTH_EXCEED
                        MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_QUESTION_LENGTH_EXCEED, QC4Common.Common.Constants.QsVariableMaxLength));
                        return;
                    }

                    //check the newVariable name is valid or not
                    validation = new Util.QS.NewQuestionValidation(txt_new_variable.Text.TrimStart().TrimEnd());
                    if (!validation.Validation_Variable(true))
                    {
                        return;
                    }
                }


                if (BlankChoiceCount == dt.Rows.Count)
                {
                    MessageDialog.ErrorOk(LocalResource.ADD_DP_RECODE_ERROR_MSG_NO_MATCH_NUMBER_OF_CHOICES);
                    return;
                }
                else if (Emptychoice && choicesCount.SelectedIndex != 0)
                {
                    MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_CHOICECOUNT_NOT_MATCH_WITH_CHOICES, LocalResource.LBL_NO_OF_CHOICES, LocalResource.LABEL_CHOICE));
                    return;
                }
                else
                {
                    string[] paramList = new string[2];
                    paramList[0] = Combo_sourceVariable.Text;
                    paramList[1] = (Cmb_Processing_Method.SelectedIndex + 1).ToString();

                    DataProcessHelper dataProcessHelper = new DataProcessHelper();

                    int columncount = 10;

                    string[,] dpsaveinstructios = new string[1, columncount];//SAVE ARRAY   
                    for (int i = 0; i < columncount; i++)
                    {
                        switch (i)
                        {
                            case 0://onoff
                                dpsaveinstructios[0, i] = LocalResource.CELL_ON;
                                break;

                            case 1:
                                break;

                            case 2://criteria
                                break;
                            case 3://criteria
                                break;
                            case 4://criteria
                                break;
                            case 5://then
                                dpsaveinstructios[0, i] = ExcelAddIn.Common.Constants.DP.InstructionTHEN;
                                break;
                            case 6://newvar
                                dpsaveinstructios[0, i] = txt_new_variable.Text;
                                break;
                            case 7://instruction
                                dpsaveinstructios[0, i] = Constants.DP.SubstituteOperatorMTOS;
                                break;
                            case 8://exclude-integrate
                                dpsaveinstructios[0, i] = Combo_sourceVariable.Text;
                                break;
                            case 9://no of vars
                                dpsaveinstructios[0, i] = (Cmb_Processing_Method.SelectedIndex + 1).ToString();
                                break;

                        }
                    }
                    dt = frmutil.UnEscapeCRLFFromAllRows(dt);
                    string newQuestion = ((Text_New_Question.Text).TrimStart().TrimEnd());

                    if (newQuestion.Length > QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit)
                    {
                        newQuestion = newQuestion.PadRight(QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit).Substring(0, QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit);
                    }
                    for (int i = 1; i <= dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i - 1][1].ToString().Length > QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit)
                        {
                            dt.Rows[i - 1][1] = dt.Rows[i - 1][1].ToString().PadRight(QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit).Substring(0, QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit);
                        }
                    }
                    // Getting choice data from existing dt table.
                    string[] dt_Choices_columns = new string[2];
                    dt_Choices_columns[0] = "No";
                    dt_Choices_columns[1] = "Choice";
                    DataTable dtChoice = dt.DefaultView.ToTable(false, dt_Choices_columns);
                    Logic.DataProcessHelper dphelper = new Logic.DataProcessHelper();
                    if (dphelper.WriteProcess(Workbook, Util.Constants.ProcessingType.CreateNewVariable, txt_new_variable.Text, Text_New_Item.Text, newQuestion,
                        choiceCountEntered, dtChoice, Constants.DP.SubstituteOperatorMTOS, dpsaveinstructios, isNewQuestion, RowNumberwrite, QC4Common.Common.Constants.STD_DP.Process_Create, isupdatequest: Isupdated))//need to pass row num from here for saving 
                    {

                        MessageDialog.Info(LocalResource.ADDQS_INFO_MSG_SUCCESS);
                        isModifiedProcess = true;
                        this.Close();

                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

        }
        DataProcessNewLoad dataLoad = new DataProcessNewLoad();
        private void Txt_new_variable_TextChanged(object sender, TextChangedEventArgs e)
        {
            QuestionSettings qs = dataLoad.Txt_Change_New_Item(txt_new_variable.Text);
            if (qs != null)
            {

                txt_new_variable.IsEnabled = true;
                Text_New_Question.IsEnabled = true;
                Text_New_Question.Background = Brushes.White;
                txt_new_variable.Background = Brushes.White;

                Text_New_Question.Text = (frmutil.UnEscapeCRLF(qs.TableHeading) + " " + frmutil.UnEscapeCRLF(qs.Question)).TrimStart().TrimEnd();
                if (qs.Choices.Count() > 0)
                {
                    choicesCount.SelectedIndex = qs.Choices.Count;
                    Choices = qs.Choices.ToArray();

                    dt = FillDataGrid(Choices.Length, Choices);
                    data_grid.DataContext = dt;
                }
            }

        }

        private void ChoicesCount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Windows.Controls.ComboBox combo = sender as System.Windows.Controls.ComboBox;
            int limit = combo.SelectedIndex;

            try
            {
                if (limit == -1)
                {
                    dt.Clear();
                    SaveBtn.IsEnabled = false;
                }
                else if (choiceloadFirst == false)
                {
                    SaveBtn.IsEnabled = true;
                }

                if (limit >= 0 && choiceloadFirst == false)
                {
                    if (limit == 0)
                    {
                        limit = 1000;
                    }
                    if (PreviousIndex < limit)
                    {
                        DataRow dr;
                        if (PreviousIndex == 0)
                        {
                            dt.Clear();
                        }
                        for (; PreviousIndex < limit; PreviousIndex++)
                        {
                            dr = dt.NewRow();
                            dr[0] = PreviousIndex + 1;
                            dr[1] = string.Empty;
                            dr[2] = false;
                            dt.Rows.Add(dr);
                        }
                    }
                    else
                    {
                        int dtcount = dt.Rows.Count;
                        for (; limit < dtcount; dtcount--)
                        {
                            dt.Rows.RemoveAt(dtcount - 1);
                        }

                    }
                }
                if (limit == -1)
                {
                    PreviousIndex = 0;
                }
                else
                {
                    PreviousIndex = limit;

                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

        }

        private void Lst_source_choices_KeyUp(object sender, KeyEventArgs e)
        {

        }


        private void Choice_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }

        private void Combo_sourceVariable_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Combo_KeyDown(Combo_sourceVariable, sender, e);
        }

        private void Combo_sourceVariable_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (Key.Tab != e.Key && !frmutil.IsSystemKey(e))
            {
                Combo_KeyUp(Combo_sourceVariable, sender, e);
            }
        }

        #region Comboboxes Event handlers
        bool FirstFocus = true;
        int LastSelected = 0;
        string LastSelectedText = "";
        private void Combo_KeyUp(ComboBox comboBox, object sender, KeyEventArgs e)
        {
            try
            {

                if (Key.Tab != e.Key)
                {
                    System.Windows.Controls.ComboBox sen = ((System.Windows.Controls.ComboBox)sender);
                    if (Key.Enter == e.Key && sen.SelectedItem == null)
                    {
                        return;
                    }
                    if ((Key.Back == e.Key || ((sen.SelectedIndex == -1 && sen.Text == "") || (sen.SelectedIndex == -1 && sen.Text != ""))) && (comboBox.IsKeyboardFocusWithin || comboBox.IsDropDownOpen))
                    {
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
                if (Key.Tab != e.Key)
                {
                    TextBox txt = null;
                    if (e.OriginalSource is TextBox)
                        txt = (TextBox)e.OriginalSource;

                    if (comboBox.SelectedIndex < 0)
                        LastSelected = 0;
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
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void Combo_sourceVariable_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (!FirstFocus)
            {
                System.Windows.Controls.ComboBox sen = ((System.Windows.Controls.ComboBox)sender);
            }
            else
                FirstFocus = false;
        }
        #endregion

        private void Combo_sourceVariable_GotFocus(object sender, RoutedEventArgs e)
        {
        }

        private void Text_New_Question_TextChanged(object sender, TextChangedEventArgs e)
        {
            Text_New_Question.Text = frmutil.UnEscapeCRLF(Text_New_Question.Text);
        }

        private void choices_grid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            QC4Common.Util.GridCommonFunc.Arrow(sender, e);
        }

        private void Choices_grid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
            var x = choices_grid.CurrentCell;
        }

        private void data_grid_CurrentCellChanged(object sender, EventArgs e)
        {
        }

        private void DisableRightMenu(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void Text_New_Question_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                data_grid.Focus();
                Style style = Application.Current.FindResource("MyFocusVisual") as Style;
                data_grid.FocusVisualStyle = style;
                if (data_grid.SelectedItem == null && data_grid.Items.Count > 0)
                    data_grid.SelectedIndex = 0;
            }
        }

        private void CloseBtn_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                Combo_sourceVariable.Focus();
            }
        }

        private void Gridnewvariable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Window_Closed(object sender, EventArgs e)
        {
        }
        private void Choices_grid_CurrentCellChanged(object sender, EventArgs e)
        {
            var dg = (System.Windows.Controls.DataGrid)sender;
            ExpGrid = dg;
            dg.Focus();
        }

        private void Combo_sourceVariable_DropDownClosed(object sender, SelectionChangedEventArgs e)
        {
            choiceloadFirst = true;
            choiceListMA = new List<DataGT>();
            System.Windows.Controls.ComboBox sourceVariable = ((System.Windows.Controls.ComboBox)sender);

            try
            {
                if (sourceVariable.SelectedItem != null)
                {
                    SourceVariableList list = new SourceVariableList();
                    list = (SourceVariableList)(sourceVariable.SelectedItem);

                    Text_New_Question.IsEnabled = true;
                    SaveBtn.IsEnabled = true;
                    Text_New_Question.Background = Brushes.White;
                    txt_new_variable.Background = Brushes.White;
                    txt_answerType.Text = list.AnswerType;
                    txtSourceChoiceCount.Text = (list.Choices.Count).ToString();
                    txt_source_question.Text = frmutil.UnEscapeCRLF(list.Question);
                    for (int i = 1; i <= list.Choices.Count; i++)
                    {
                        choiceListMA.Add(new DataGT() { Question = i + ":" + list.Choices[i - 1], QuestionIndex = i });
                    }
                    Choices = list.Choices.ToArray();
                    source_datatable = FillDataGrid(Choices.Length, Choices);

                    choices_grid.IsEnabled = true;
                    choices_grid.Background = Brushes.White;
                    choices_grid.DataContext = source_datatable;
                    Button_Search.IsEnabled = true;
                    Button_Search.Opacity = 1;
                    choiceloadFirst = false;

                    if (ProcessingOption == QC4Common.Common.Constants.STD_DP.Process_Create || ProcessingOption == QC4Common.Common.Constants.STD_DP.Process_Copy)
                    {
                        QC4Common.Util.QSUtil qsutil = new QC4Common.Util.QSUtil();
                        txt_new_variable.Text = qsutil.GetVariableName(list.Variable, PopulatedDictionary.Values.ToList());
                        Text_New_Question.Text = (frmutil.UnEscapeCRLF(Util.Definiotion.VariableDictionary[list.Variable].TableHeading) + " " + frmutil.UnEscapeCRLF(list.Question)).TrimStart().TrimEnd();
                        choicesCount.SelectedIndex = list.Choices.Count;
                        Choices = list.Choices.ToArray();
                        dt = FillDataGrid(Choices.Length, Choices);
                        data_grid.DataContext = dt;
                        txt_new_variable.IsEnabled = true;

                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
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

        private void Choices_grid_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                txt_new_variable.Focus();
                e.Handled = true;
            }
            else
                QC4Common.Util.GridCommonFunc.Arrow(sender, e);
        }

        private void Data_grid_IsMouseCaptureWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (data_grid != null && data_grid.CurrentCell != null && data_grid.CurrentCell.Column != null && data_grid.CurrentCell.Column.DisplayIndex == 1)
            {
                dt.Rows[data_grid.SelectedIndex][2] = false;
                frmutil.SetErrorForGrid(data_grid, data_grid.SelectedIndex, data_grid.CurrentCell.Column.DisplayIndex, QC4Common.Common.Constants.STD_DP.Foreground, true);
            }
        }

        private void Data_grid_PreviewKeyUp(object sender, KeyEventArgs e)
        {
        }

        private void Data_grid_PreviewKeyUp_1(object sender, KeyEventArgs e)
        {
            try
            {
                bool _altModifierPressed = (Keyboard.IsKeyUp(Key.LeftCtrl) || Keyboard.IsKeyUp(Key.RightCtrl));
                if (_altModifierPressed && e.Key == Key.C)
                {
                    if (Clipboard.ContainsText(TextDataFormat.UnicodeText))
                    {
                        clipboardText = string.Empty;
                        clipboardText = Clipboard.GetText(TextDataFormat.UnicodeText);
                    }
                }
                int datagridColumn = data_grid.CurrentCell.Column.DisplayIndex;
                DataGridCell cell = frmutil.GetCell(data_grid, data_grid.SelectedIndex, datagridColumn);
                if (cell.IsEditing)
                {
                    bool _altModifierPressed1 = (Keyboard.IsKeyUp(Key.LeftCtrl) || Keyboard.IsKeyUp(Key.RightCtrl));
                    if (_altModifierPressed1 && Keyboard.IsKeyUp(Key.V))
                    {
                        if (!string.IsNullOrEmpty(clipboardText))
                        {
                            Clipboard.SetText(clipboardText);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void Txt_new_variable_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void b1SetColor(object sender, MouseButtonEventArgs e)
        {
            if (ExpGrid != null)
                ExpGrid.Focus();
        }
        private void HandleCopyPaste(object sender, KeyEventArgs e)
        {
            bool _altModifierPressed = (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl));
            if (_altModifierPressed && Keyboard.IsKeyDown(Key.V))
            {

                var uiElement = e.OriginalSource as UIElement;
                try
                {
                    Util.QS.GridCopyPaste copyPaste = new Util.QS.GridCopyPaste();
                    var data = copyPaste.PastetoDatagrid(sender);
                    int datagridColumn = data_grid.CurrentCell.Column.DisplayIndex;
                    if (data_grid.SelectedIndex == -1) { data_grid.SelectedIndex = 0; }
                    DataGridCell cell = frmutil.GetCell(data_grid, data_grid.SelectedIndex, datagridColumn);

                    int No_Row = copyPaste.No_Row;
                    int No_Column = copyPaste.No_Columns;
                    if (data != null)
                    {
                        if (!cell.IsEditing)
                        {

                            e.Handled = true;
                            int datagridRow = data_grid.SelectedIndex;
                            if (data_grid.CurrentCell.Column.DisplayIndex == 1)
                            {
                                //selection in choice
                                if (No_Column > 1)
                                {
                                    MessageDialog.ErrorOk(LocalResource.COPY_ERROR_MSG);
                                    e.Handled = true;
                                }
                                else
                                {
                                    int RowIndex = data_grid.SelectedIndex;
                                    for (int i = 0; i < No_Row; i++, RowIndex++)
                                    {
                                        for (int j = 1, col = 1; j <= No_Column; j++, col++)
                                        {
                                            if (data[i, (j - 1)] == null)
                                            {
                                                dt.Rows[RowIndex][col] = null;
                                            }
                                            else
                                            {
                                                dt.Rows[RowIndex][col] = data[i, (j - 1)].ToString();
                                            }

                                        }

                                    }

                                }
                            }
                        }
                        else
                        {
                            string regexReplacedStr = "";
                            if (Clipboard.ContainsText(TextDataFormat.UnicodeText))
                            {
                                clipboardText = string.Empty;
                                clipboardText = Clipboard.GetText(TextDataFormat.UnicodeText);
                            }
                            Clipboard.SetText(data[0, 0].ToString());
                        }
                    }

                }
                catch (Exception ex) { _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException); }
            }
            if (e.Key == Key.Delete)
            {
                try
                {
                    int RowIndex = 0;
                    if ((data_grid.SelectedItems != null) && (data_grid.SelectedItems.Count > 0))
                    {
                        for (int i = 0; i < data_grid.SelectedItems.Count; i++)
                        {
                            var presentRow = (System.Data.DataRowView)data_grid.SelectedItems[i];
                            RowIndex = Convert.ToInt32(presentRow.Row.ItemArray[0].ToString());
                            dt.Rows[RowIndex - 1][1] = string.Empty;

                        }
                    }
                }
                catch (Exception ex) { _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException); }
            }
        }

    }
}
