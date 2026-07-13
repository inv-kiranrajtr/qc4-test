using ExcelAddIn.Common;
using log4net;
using QC4Common.Model;
using Qc4Launcher.Logic.DataImport;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Qc4Launcher.Forms.DP_Main
{
    /// <summary>
    /// Interaction logic for Class.xaml
    /// </summary>
    public partial class Class : Window
    {
        private Dictionary<String, QuestionSettings> PopulatedDictionary = new Dictionary<string, QuestionSettings>();
        ObservableCollection<ChoiceList> choiceList = new ObservableCollection<ChoiceList>();
        Microsoft.Office.Interop.Excel.Workbook Workbook;
        int ReadRow;
        int WriteRow;
        string ProcessingType;
        string ProcessingOption;
        private bool isNewQuestion = true;
        private bool isUpdateQuestion = false;
        public bool isModifiedProcess = false;
        bool isAGESelected = false;
        QC4Common.Util.FormUtil frmutil = new QC4Common.Util.FormUtil();
        private ObservableCollection<SourceVariableList> sourceVariableList = new ObservableCollection<SourceVariableList>();
        double min = 0, max = 0, avg = 0;
        string[] choicesList = new string[201];
        bool IsExistingNewItemAdded = false;
        string newVariableBeforeEdit;
        string inputUnitsBefore = string.Empty;
        bool isThousandSeparatorChecked = false;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        #region Properties
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
        public ObservableCollection<ChoiceList> ChoiceListView
        {
            get
            {
                return choiceList;
            }
            set
            {
                choiceList = value;
            }
        }
        #endregion
        public Class(Microsoft.Office.Interop.Excel.Workbook workbook, int readRow, int writeRow, string processingType, string processingOption)
        {
            Workbook = workbook;
            ReadRow = readRow;
            WriteRow = writeRow;
            ProcessingType = processingType;
            ProcessingOption = processingOption;
            InitializeComponent();
            if (QC4Common.Common.Constants.GlobalMode.Split(',')[0] != "ja-JP")
                Button_Help.Visibility = Visibility.Hidden;
            else
                Button_Help.Visibility = Visibility.Visible;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lb_class.Text = LocalResource.LBL_CLASS_DESCRIPTION;
            lb_command.Text = Util.Constants.ProcessingMethod.CLASS;
            Save.IsEnabled = false;
            NewItemSearchbutton.IsEnabled = false;
            NewItemSearchbutton.Opacity = 0.5;
            PopulateNVariableList();
            int[] choices = new int[1001];
            choices = Enumerable.Range(0, 1001).ToArray();
            choicesList = Array.ConvertAll(choices, ele => ele.ToString());
            choicesList[0] = LocalResource.LBL_AUTO;
            choicesCount.ItemsSource = choicesList;
            ViewAllChoices();
            if (ProcessingOption == QC4Common.Common.Constants.STD_DP.Process_Edit || ProcessingOption == QC4Common.Common.Constants.STD_DP.Process_Copy)
            {
                PopulateClassData(Workbook, ReadRow);
            }
        }

        private void ViewAllChoices()
        {
            ChoiceListView = new ObservableCollection<ChoiceList>();
            int count = 0;
            foreach (var item in choicesList)
            {
                if (item != LocalResource.LBL_AUTO)
                {
                    count = count + 1;

                    ChoiceListView.Add(new ChoiceList()
                    {
                        Id = count,
                        Tild = "~"
                    });
                }
            }
            data_grid.ItemsSource = ChoiceListView;
        }
        private void Window_Closed(object sender, EventArgs e)
        {

        }

        private void Button_Help_Click_1(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(QC4Common.Classes.Help.GetHelpLink(QC4Common.Common.Constants.HelpButtonType.CLASS));
        }

        private void MagnifyingGlassButton_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void NewItemSearchbutton_Click(object sender, RoutedEventArgs e)
        {
            if (MagnifyingGlassButton.VariableList != null)
            {
                txt_new_variable.Text = MagnifyingGlassButton.VariableList.Variable;
                Text_NewItem_Question.Text = (frmutil.UnEscapeCRLF(MagnifyingGlassButton.VariableList.Title) + " " + frmutil.UnEscapeCRLF(MagnifyingGlassButton.VariableList.Question)).TrimStart().TrimEnd();
                choicesCount.SelectedIndex = MagnifyingGlassButton.VariableList.Choices.Count;
                if (ChoiceListView.Count == MagnifyingGlassButton.VariableList.Choices.Count)
                {

                    for (int i = 0; i < choicesCount.SelectedIndex; i++)
                    {
                        ChoiceListView[i].Choice = frmutil.EscapeCRLF(MagnifyingGlassButton.VariableList.Choices[i]);
                        ChoiceListView[i].LowerLimit = null;
                        ChoiceListView[i].UpperLimit = null;
                    }
                    CollectionViewSource.GetDefaultView(data_grid.ItemsSource).Refresh();
                }
                else
                {
                    IsExistingNewItemAdded = true;
                }
            }
        }


        private void ChoicesCount_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            System.Windows.Controls.ComboBox combo = (System.Windows.Controls.ComboBox)sender;

            int result = 0;
            if (int.TryParse(combo.Text, out result))
            {
                if (!(result <= 1000 && result >= 1))
                {
                    MessageDialog.ErrorOk(LocalResource.RECODE_ALERT_MSG_COMBO_CATEGORY);
                    combo.SelectedIndex = 1000;
                }
            }
            else
            {
                if (!(combo.Text == LocalResource.LBL_AUTO))
                {
                    MessageDialog.ErrorOk(LocalResource.RECODE_ALERT_MSG_COMBO_CATEGORY);
                    combo.SelectedIndex = 1000;
                }
            }
        }

        private void ChoicesCount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            ObservableCollection<ChoiceList> cList = new ObservableCollection<ChoiceList>();

            var found = ChoiceListView.LastOrDefault(u => (u.Choice != null) || (u.LowerLimit != null) || (u.UpperLimit != null));
            int length = ChoiceListView.IndexOf(found);
            if (length > -1)
            {
                for (int i = 0; i <= length; i++)
                {
                    cList.Add(ChoiceListView[i]);
                }
            }
            if (cList.Count != 0)
            {
                for (int i = 0; i < cList.Count; i++)
                {
                    ChoiceListView[i] = cList[i];
                }
            }
            else
            {
                ChoiceListView = new ObservableCollection<ChoiceList>();
            }

            int selectedchoicecount = combo.SelectedIndex;
            if (selectedchoicecount >= 0)
            {
                if (combo.SelectedIndex == 0)
                {
                    selectedchoicecount = 1000;
                }

                if (ChoiceListView.Count > selectedchoicecount)
                {
                    for (int i = ChoiceListView.Count; i > selectedchoicecount; i--)
                    {
                        ChoiceListView.RemoveAt(i - 1);
                    }
                }
                else
                {

                    for (int i = ChoiceListView.Count; i < selectedchoicecount; i++)
                    {
                        ChoiceListView.Add(new ChoiceList()
                        {
                            Id = i + 1,
                            Choice = null,
                            LowerLimit = null,
                            Tild = "~",
                            UpperLimit = null

                        });
                    }
                }
            }

            if (IsExistingNewItemAdded)
            {
                for (int i = 0; i < ChoiceListView.Count; i++)
                {
                    ChoiceListView[i].Choice = "cat. " + (i + 1);
                }
            }
            data_grid.ItemsSource = ChoiceListView;
        }


        private void Save_Click(object sender, RoutedEventArgs e)
        {
            bool isAllEmpty = false;
            isNewQuestion = true;
            isUpdateQuestion = false;
            bool isNewVariableAndOnlyUpdationNeeded = false;
            data_grid.SelectedIndex = -1;
            try { txt_new_variable.Text = txt_new_variable.Text.TrimStart().TrimEnd(); } catch (Exception ex) { _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException); }
            string newvariable = ((txt_new_variable.Text).TrimStart().TrimEnd());
            QC4Common.Util.QSUtil qsutil = new QC4Common.Util.QSUtil();
            var found = ChoiceListView.LastOrDefault(u => (!string.IsNullOrEmpty(u.Choice)) || (!string.IsNullOrEmpty(u.LowerLimit)) || (!string.IsNullOrEmpty(u.UpperLimit)));

            int foundIndex = ChoiceListView.IndexOf(found);
            int numberOfChoices = choicesCount.SelectedIndex;
            if (choicesCount.SelectedIndex == 0)
            {
                numberOfChoices = foundIndex + 1;
            }
            else
            {
                numberOfChoices = choicesCount.SelectedIndex;
            }

            if ((ProcessingOption != QC4Common.Common.Constants.STD_DP.Process_Edit) && (string.IsNullOrEmpty(newvariable) || !frmutil.IsVariableNameExists(newvariable, PopulatedDictionary.Values.ToList(), 1)))
            {

                string errormsg = LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_NEW_ITEM_EMPTY + "\n" + LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_RENAME_NEW_VARIABLE_AUTOMATICALLY;
                if (!string.IsNullOrEmpty(newvariable))
                {
                    errormsg = LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_ITEM_NAME_ALREADY_EXISTS + "\n" + LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_RENAME_NEW_VARIABLE_AUTOMATICALLY;
                }
                System.Windows.Forms.DialogResult result;
                result = MessageDialog.InfoYesNo(errormsg);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    if (string.IsNullOrEmpty(newvariable))
                    {
                        //get new variable name
                        newvariable = qsutil.GetVariableName(Combo_sourceVariableNType.Text, PopulatedDictionary.Values.ToList());
                        txt_new_variable.Text = newvariable;
                        isNewQuestion = true;
                        isUpdateQuestion = false;
                    }
                    else
                    {
                        //get new variable name
                        newvariable = qsutil.GetVariableName(newvariable, PopulatedDictionary.Values.ToList());
                        txt_new_variable.Text = newvariable;
                        isNewQuestion = true;
                        isUpdateQuestion = false;
                    }


                }
                else if (result == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }

            if ((Util.Definiotion.VariableDictionary.ContainsKey(newvariable)) && (
    (Util.Definiotion.VariableDictionary[newvariable].AnswerType != answerType.Text) ||
   (Util.Definiotion.VariableDictionary[newvariable].CategoryCount != numberOfChoices)))
            {
                System.Windows.Forms.DialogResult result;
                result = MessageDialog.InfoYesNo(LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_ITEM_NAME_ALREADY_EXISTS + "\n" + LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_RENAME_NEW_VARIABLE_AUTOMATICALLY);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    //get new variable name
                    newvariable = txt_new_variable.Text = qsutil.GetVariableName(newvariable, PopulatedDictionary.Values.ToList());
                }
                else if (result == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                isNewQuestion = true;
                isUpdateQuestion = false;
            }
            else if ((Util.Definiotion.VariableDictionary.ContainsKey(newvariable)) &&
               (Util.Definiotion.VariableDictionary[newvariable].AnswerType == answerType.Text) &&
              (Util.Definiotion.VariableDictionary[newvariable].CategoryCount == numberOfChoices)
              )
            {
                isUpdateQuestion = true;
                isNewQuestion = false;
                isNewVariableAndOnlyUpdationNeeded = true;
            }

            if (ProcessingOption == QC4Common.Common.Constants.STD_DP.Process_Edit && (string.IsNullOrEmpty(txt_new_variable.Text)))
            {
                string errormsg = LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_NEW_ITEM_EMPTY + "\n" + LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_RENAME_NEW_VARIABLE_AUTOMATICALLY;
                System.Windows.Forms.DialogResult result;
                result = MessageDialog.InfoYesNo(errormsg);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    //get new variable name
                    newvariable = qsutil.GetVariableName(Combo_sourceVariableNType.Text, PopulatedDictionary.Values.ToList());
                    txt_new_variable.Text = newvariable;
                    isNewQuestion = true;
                    isUpdateQuestion = false;
                }
                else if (result == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

            }


            if (string.IsNullOrEmpty(Text_NewItem_Question.Text.TrimStart().TrimEnd()))
            {
                MessageDialog.ErrorOk(LocalResource.ADDQS_ERROR_MSG_QUSTION_TEXT_MISS);
                return;
            }
            if (frmutil.IsVariableLengthExceeds(newvariable))
            {
                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_QUESTION_LENGTH_EXCEED, QC4Common.Common.Constants.QsVariableMaxLength)); //ERR_MSG_QUESTION_LENGTH_EXCEED
                return;
            }

            //Check the newVariable name is valid or not
            if (ProcessingOption == QC4Common.Common.Constants.STD_DP.Process_Edit)
            {
                if (newVariableBeforeEdit != newvariable)
                {
                    if (!frmutil.IsVariableNameExists(newvariable, PopulatedDictionary.Values.ToList(), 2))
                    {
                        string errormsg = LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_NEW_ITEM_EMPTY + "\n" + LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_RENAME_NEW_VARIABLE_AUTOMATICALLY;
                        if (!string.IsNullOrEmpty(newvariable))
                        {
                            errormsg = LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_ITEM_NAME_ALREADY_EXISTS + "\n" + LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_RENAME_NEW_VARIABLE_AUTOMATICALLY;
                        }
                        System.Windows.Forms.DialogResult result;
                        result = MessageDialog.InfoYesNo(errormsg);
                        if (result == System.Windows.Forms.DialogResult.Yes)
                        {
                            if (string.IsNullOrEmpty(newvariable))
                            {
                                //get new variable name
                                newvariable = qsutil.GetVariableName(newvariable, PopulatedDictionary.Values.ToList());
                                txt_new_variable.Text = newvariable;
                                isNewQuestion = true;
                                isUpdateQuestion = false;
                            }
                            else
                            {
                                //get new variable name
                                newvariable = qsutil.GetVariableName(newvariable, PopulatedDictionary.Values.ToList());
                                txt_new_variable.Text = newvariable;
                                isNewQuestion = true;
                                isUpdateQuestion = false;
                            }


                        }
                        else if (result == System.Windows.Forms.DialogResult.No)
                        {
                            return;
                        }

                        Util.QS.NewQuestionValidation validation = new Util.QS.NewQuestionValidation(newvariable);
                        if (!validation.Validation_Variable())
                        {
                            return;
                        }
                        isUpdateQuestion = false;
                        isNewQuestion = true;
                    }

                }
                else
                {
                    isUpdateQuestion = true;
                    isNewQuestion = false;
                }


            }
            else
            {
                Util.QS.NewQuestionValidation validation = new Util.QS.NewQuestionValidation(newvariable);
                if (!isNewVariableAndOnlyUpdationNeeded && !validation.Validation_Variable())
                {
                    return;
                }
            }


            if (found == null && choicesCount.SelectedIndex == 0)
            {
                MessageDialog.ErrorOk(LocalResource.ADD_DP_RECODE_ERROR_MSG_NO_CHOICE_SET);
                return;
            }
            else if (found == null && choicesCount.SelectedIndex != 0)
            {
                MessageDialog.ErrorOk(LocalResource.ADD_DP_RECODE_ERROR_MSG_NO_MATCH_NUMBER_OF_CHOICES);
                return;
            }
            else
            {
                for (int i = 0; i <= foundIndex; i++)
                {
                    string cellChoice = Convert.ToString(ChoiceListView[i].Choice);
                    string cellvalue1 = Convert.ToString(ChoiceListView[i].LowerLimit);
                    string cellvalue2 = Convert.ToString(ChoiceListView[i].UpperLimit);

                    if (cellChoice == null || string.IsNullOrEmpty(cellChoice.TrimEnd().TrimStart()))
                    {
                        ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_RECODE_CHOICE_IS_NOT_INPUT));
                        SetInvalidBGChoice(i);
                        return;
                    }
                    if (!string.IsNullOrEmpty(cellvalue1))
                    {
                        cellvalue1 = cellvalue1.TrimEnd().TrimStart();
                    }
                    if (!string.IsNullOrEmpty(cellvalue2))
                    {
                        cellvalue2 = cellvalue2.TrimEnd().TrimStart();
                    }
                    if (cellvalue1 != null || cellvalue2 != null)
                    {
                        bool isValid = false;
                        string[] limits = new string[2];
                        string[] limitsWithBrackets = new string[2];
                        string  cellvalue1WithBrackets = cellvalue1;
                        string cellvalue2WithBrackets = cellvalue2;
                        limitsWithBrackets[0] = cellvalue1WithBrackets;
                        limitsWithBrackets[1] = cellvalue2WithBrackets;
                        cellvalue1 = frmutil.ReplaceBrackets(cellvalue1);
                        cellvalue2 = frmutil.ReplaceBrackets(cellvalue2);
                        limits[0] = cellvalue1;
                        limits[1] = cellvalue2;

                        if (CheckOnlyBracketsPresent(limitsWithBrackets[0]))
                        {
                            ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_SET_A_NUMERIC_VALUE));
                            SetInvalidColorLowerLimit(i);
                            return;
                        }
                        if (CheckOnlyBracketsPresent(limitsWithBrackets[1]))
                        {
                            ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_SET_A_NUMERIC_VALUE));
                            SetInvalidColorUpperLimit(i);
                            return;
                        }
                        if (cellvalue1 != null && cellvalue2 != null)
                        {
                            if (cellvalue1.TrimEnd().TrimStart() == "" && cellvalue2.TrimEnd().TrimStart() == "")
                            {
                                ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_CLASS_INPUT_EITHER_LOWER_OR_UPPER_LIMIT));
                                SetInvalidBGLowerLimit(i);
                                SetInvalidBGUpperLimit(i);
                                return;
                            }
                        }

                        if (cellvalue1 == null && cellvalue2.TrimEnd().TrimStart() == "")
                        {
                            ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_CLASS_INPUT_EITHER_LOWER_OR_UPPER_LIMIT));
                            SetInvalidBGLowerLimit(i);
                            SetInvalidBGUpperLimit(i);
                            return;
                        }
                        if (cellvalue2 == null && cellvalue1.TrimEnd().TrimStart() == "")
                        {
                            ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_CLASS_INPUT_EITHER_LOWER_OR_UPPER_LIMIT));
                            SetInvalidBGLowerLimit(i);
                            SetInvalidBGUpperLimit(i);
                            return;
                        }
                        for (int j = 0; j < 2; j++)
                        {
                            if (limits[j] != null)
                            {
                                if (limits[j] != "" && limits[j].TrimEnd().TrimStart() == "")
                                {
                                    ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_SET_A_NUMERIC_VALUE));
                                    if (j == 0)
                                    {
                                        SetInvalidBGLowerLimit(i);
                                    }
                                    else if (j == 1)
                                    {
                                        SetInvalidBGUpperLimit(i);
                                    }

                                    return;
                                }
                                float result = 0;
                                bool isInteger = float.TryParse(limits[j], out result);
                                if (!isInteger && !limits[j].Contains("!"))
                                {
                                    if (limits[j] != null && limits[j] != "")
                                    {
                                        if (limits[j].ToCharArray().Count(c => c == '-') == 1 && limits[j].IndexOf("-") == 0)
                                        {

                                        }
                                        else
                                        {
                                            if (!Regex.IsMatch(limits[j], @"^\d+$"))
                                            {
                                                ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_SET_A_NUMERIC_VALUE));
                                                if (j == 0)
                                                {
                                                    SetInvalidColorLowerLimit(i);
                                                }
                                                else if (j == 1)
                                                {
                                                    SetInvalidColorUpperLimit(i);
                                                }

                                                return;
                                            }
                                            else if (limits[j].Length >= 309)
                                            {
                                                ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_SET_A_NUMERIC_VALUE));
                                                if (j == 0)
                                                {
                                                    SetInvalidColorLowerLimit(i);
                                                }
                                                else if (j == 1)
                                                {
                                                    SetInvalidColorUpperLimit(i);
                                                }

                                                return;
                                            }

                                        }
                                    }


                                }
                                if (j == 1 && limits[j].Contains("!"))
                                {
                                    MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, (i + 1)) + "\n" + (LocalResource.ERR_MSG_RECODE_CANNOT_SET_ELSEWHERE_THAN_AT_START));
                                    SetInvalidColorUpperLimit(i);
                                    return;
                                }
                                if (j == 0 && limits[j].Contains("!") && limits[j].Count() == 1 && (cellvalue2 == null || cellvalue2 == ""))
                                {
                                    MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, (i + 1)) + "\n" + (LocalResource.ERR_MSG_RECODE_INCORRECT_RANGE_SPECIFICATION));
                                    SetInvalidColorLowerLimit(i);
                                    return;
                                }
                                if (j == 0 && limits[j].ToCharArray().Count(c => c == '!') > 1)
                                {
                                    MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, (i + 1)) + "\n" + (LocalResource.ERR_MSG_RECODE_CANNOT_SET_ELSEWHERE_THAN_AT_START));
                                    SetInvalidColorLowerLimit(i);
                                    return;
                                }
                                if (j == 0 && limitsWithBrackets[j].Contains("!") && limitsWithBrackets[j].IndexOf("!") != 0)
                                {
                                    MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, (i + 1)) + "\n" + (LocalResource.ERR_MSG_RECODE_CANNOT_SET_ELSEWHERE_THAN_AT_START));
                                    SetInvalidColorLowerLimit(i);
                                    return;
                                }
                                if (!frmutil.IsLimitPresent(limits[j]))
                                {
                                    if (limits[j] != null)
                                    {
                                        string entry = frmutil.TrimStartEqualNotequal(Convert.ToString(limits[j]));
                                        if (entry.ToCharArray().Count(c => c == '-') == 1 && entry.IndexOf("-") == 0)
                                        {
                                            if (limits[j] == "-0")
                                            {
                                                ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_MULTIPLE_LIMIT_SET));
                                                if (j == 0)
                                                {
                                                    SetInvalidColorLowerLimit(i);
                                                }
                                                else if (j == 1)
                                                {
                                                    SetInvalidColorUpperLimit(i);
                                                }
                                                return;
                                            }
                                            if ((limitsWithBrackets[j].Contains("(")))
                                            {
                                                if (limitsWithBrackets[j].IndexOf("(") == 0)
                                                {
                                                    //Valid entry
                                                    isValid = true;
                                                }
                                                else if ((limitsWithBrackets[j].Contains('!') && limitsWithBrackets[j].IndexOf('!') == 0 && limitsWithBrackets[j].IndexOf('(') == 1))
                                                {
                                                    //Valid entry
                                                    isValid = true;
                                                }

                                            }
                                            else
                                            {

                                            }

                                        }
                                        else
                                        {
                                            ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_MULTIPLE_LIMIT_SET));
                                            if (j == 0)
                                            {
                                                SetInvalidColorLowerLimit(i);
                                            }
                                            else if (j == 1)
                                            {
                                                SetInvalidColorUpperLimit(i);
                                            }
                                            return;
                                        }
                                    }

                                }
                                if (!frmutil.IsNumeric(frmutil.TrimStartEqualNotequal(limits[j])) && !isValid)
                                {
                                    if (!Regex.IsMatch(frmutil.TrimStartEqualNotequal(limits[j]), @"^\d+$"))
                                    {
                                        ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_SET_A_NUMERIC_VALUE));
                                        if (j == 0)
                                        {
                                            SetInvalidColorLowerLimit(i);
                                        }
                                        else if (j == 1)
                                        {
                                            SetInvalidColorUpperLimit(i);
                                        }
                                        return;
                                    }
                                    else if (limits[j].Length >= 309)
                                    {
                                        ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_SET_A_NUMERIC_VALUE));
                                        if (j == 0)
                                        {
                                            SetInvalidColorLowerLimit(i);
                                        }
                                        else if (j == 1)
                                        {
                                            SetInvalidColorUpperLimit(i);
                                        }
                                        return;
                                    }
                                }
                                
                                if ((limits[j]).Contains("/"))
                                {
                                    ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_CANNOT_SPECIFY_SPLIT_RANGE));
                                    if (j == 0)
                                    {
                                        SetInvalidColorLowerLimit(i);
                                    }
                                    else if (j == 1)
                                    {
                                        SetInvalidColorUpperLimit(i);
                                    }
                                    return;
                                }
                                if ((limits[j]).Contains(","))
                                {
                                    ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_CANNOT_SPECIFY_SPLIT_RANGE));
                                    if (j == 0)
                                    {
                                        SetInvalidColorLowerLimit(i);
                                    }
                                    else if (j == 1)
                                    {
                                        SetInvalidColorUpperLimit(i);
                                    }
                                    return;
                                }
                            }


                        }
                        if (cellvalue1 != null && cellvalue1.Contains("!"))
                        {
                            cellvalue1 = cellvalue1.Remove(0, 1);
                            if (!frmutil.IsLowerLimitLesser(cellvalue1, cellvalue2))
                            {
                                ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_SET_LOWER_LIMIT_LESS_THAN_UPPER_LIMIT));
                                SetInvalidColorLowerLimit(i);
                                return;
                            }
                        }
                        else
                        {
                            if (!frmutil.IsLowerLimitLesser(cellvalue1, cellvalue2))
                            {
                                try
                                {
                                    if (Regex.IsMatch(cellvalue1, @"^\d+$") && Regex.IsMatch(cellvalue2, @"^\d+$"))
                                    {
                                        BigInteger number1 = BigInteger.Parse(cellvalue1);
                                        BigInteger number2 = BigInteger.Parse(cellvalue2);
                                        int idd = BigInteger.Compare(number1, number2);
                                        if (!(BigInteger.Compare(number1, number2) < 0))
                                        {
                                            ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_SET_LOWER_LIMIT_LESS_THAN_UPPER_LIMIT));
                                            SetInvalidColorLowerLimit(i);
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_SET_LOWER_LIMIT_LESS_THAN_UPPER_LIMIT));
                                        SetInvalidColorLowerLimit(i);
                                        return;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                                    ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_SET_LOWER_LIMIT_LESS_THAN_UPPER_LIMIT));
                                    SetInvalidColorLowerLimit(i);
                                    return;
                                }


                            }
                        }


                    }
                    else
                    {
                        ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_CLASS_INPUT_EITHER_LOWER_OR_UPPER_LIMIT));
                        SetInvalidBGLowerLimit(i);
                        SetInvalidBGUpperLimit(i);
                        return;
                    }

                }
            }


            try { txt_new_variable.Text = txt_new_variable.Text.TrimStart().TrimEnd(); } catch (Exception ex) { _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException); }
            var dataSet = new DataSet();
            var dataTable = new DataTable();
            dataSet.Tables.Add(dataTable);

            dataTable.Columns.Add("Index");
            dataTable.Columns.Add("Choice");
            dataTable.Columns.Add("LowerLimit");
            dataTable.Columns.Add("UpperLimit");

            //Handles save if (Auto) is selected
            if (choicesCount.SelectedIndex == 0)
            {
                List<ChoiceList> valueEnteredChoices = new List<ChoiceList>();
                var foundAuto = ChoiceListView.LastOrDefault(u => (!string.IsNullOrEmpty(u.Choice)) || (!string.IsNullOrEmpty(u.LowerLimit)) || (!string.IsNullOrEmpty(u.UpperLimit)));
                int foundAutoIndex = ChoiceListView.IndexOf(foundAuto);
                foreach (var element in data_grid.ItemsSource)
                {
                    if ((element as ChoiceList).Id <= foundAutoIndex + 1)
                    {
                        (element as ChoiceList).LowerLimit = frmutil.ReplaceBrackets((element as ChoiceList).LowerLimit);
                        (element as ChoiceList).UpperLimit = frmutil.ReplaceBrackets((element as ChoiceList).UpperLimit);
                        DataRow newRow = dataTable.NewRow();
                        newRow["Index"] = (element as ChoiceList).Id;
                        newRow["Choice"] = (element as ChoiceList).Choice;
                        newRow["LowerLimit"] = (element as ChoiceList).LowerLimit;
                        newRow["UpperLimit"] = (element as ChoiceList).UpperLimit;

                        dataTable.Rows.Add(newRow);
                    }
                    else
                    {
                        break;
                    }

                }
                SaveClassProcess(dataTable, isNewQuestion, isUpdateQuestion);
            }
            //Handles save if number of choices is selected
            else
            {
                if (foundIndex < choicesCount.SelectedIndex - 1)
                {
                    MessageDialog.ErrorOk(LocalResource.ADD_DP_RECODE_ERROR_MSG_NO_MATCH_NUMBER_OF_CHOICES);
                    return;
                }
                foreach (var element in data_grid.ItemsSource)
                {
                    (element as ChoiceList).LowerLimit = frmutil.ReplaceBrackets((element as ChoiceList).LowerLimit);
                    (element as ChoiceList).UpperLimit = frmutil.ReplaceBrackets((element as ChoiceList).UpperLimit);
                    DataRow newRow = dataTable.NewRow();
                    newRow["Index"] = (element as ChoiceList).Id;
                    newRow["Choice"] = (element as ChoiceList).Choice;
                    newRow["LowerLimit"] = (element as ChoiceList).LowerLimit;
                    newRow["UpperLimit"] = (element as ChoiceList).UpperLimit;

                    dataTable.Rows.Add(newRow);
                }
                if (dataTable.Rows.Count == ChoiceListView.Count)
                {
                    SaveClassProcess(dataTable, isNewQuestion, isUpdateQuestion);
                }
            }

        }

        private void SaveClassProcess(DataTable dt, bool isNew, bool isUpdate)
        {
            try
            {
                string[] dt_Choices_columns = new string[2];
                dt_Choices_columns[0] = "Index";
                dt_Choices_columns[1] = "Choice";
                string[] dt_Choices_columns2 = new string[2];
                dt_Choices_columns2[0] = "LowerLimit";
                dt_Choices_columns2[1] = "UpperLimit";
                DataTable dtLimits = dt.DefaultView.ToTable(false, dt_Choices_columns2);
                DataTable dtChoice = dt.DefaultView.ToTable(false, dt_Choices_columns);
                string[] paramList = new string[dtLimits.Rows.Count + 1 + 2];
                paramList[0] = Combo_sourceVariableNType.Text;
                paramList[1] = GetUpper_Limit_Relation();
                if (chk_noAnswer.IsChecked == true)
                {
                    paramList[2] = "2";
                }
                else
                {
                    paramList[2] = "1";
                }
                for (int i = 1; i <= dtLimits.Rows.Count; i++)
                {
                    string lowerLimit = string.Empty;
                    string upperLimit = string.Empty;
                    if (dtLimits.Rows[i - 1][0] == null)
                    {
                        lowerLimit = null;
                    }
                    else
                    {
                        if (dtLimits.Rows[i - 1][0].ToString().TrimEnd().TrimStart() != "")
                        {
                            if (dtLimits.Rows[i - 1][0].ToString().IndexOf("!") == 0)
                            {
                                string lower_Lmt = dtLimits.Rows[i - 1][0].ToString().Remove(0, 1);
                                if (lower_Lmt != "")
                                {
                                    if (Convert.ToDouble(lower_Lmt) < 0)
                                    {
                                        lowerLimit = "!" + "(" + lower_Lmt + ")";
                                    }
                                    else
                                    {
                                        lowerLimit = "!" + lower_Lmt;
                                    }
                                }
                                else
                                {
                                    lowerLimit = "!";
                                }

                            }
                            else
                            {
                                if (Convert.ToDouble(dtLimits.Rows[i - 1][0].ToString()) < 0)
                                {
                                    lowerLimit = "(" + dtLimits.Rows[i - 1][0].ToString() + ")";
                                }
                                else
                                {
                                    lowerLimit = dtLimits.Rows[i - 1][0].ToString();
                                }
                            }

                        }
                        else
                        {
                            lowerLimit = null;
                        }

                    }
                    if (dtLimits.Rows[i - 1][1] == null)
                    {
                        upperLimit = null;
                    }
                    else
                    {
                        if (dtLimits.Rows[i - 1][1].ToString().TrimEnd().TrimStart() != "")
                        {
                            if (Convert.ToDouble(dtLimits.Rows[i - 1][1].ToString()) < 0)
                            {
                                upperLimit = "(" + dtLimits.Rows[i - 1][1].ToString() + ")";
                            }
                            else
                            {
                                upperLimit = dtLimits.Rows[i - 1][1].ToString();
                            }
                        }
                        else
                        {
                            upperLimit = null;
                        }

                    }


                    paramList[2 + i] = lowerLimit + "-" + upperLimit;
                }
                string command = QC4Common.Common.Constants.DP.SubstituteOperatorCLASS;

                // total number of columns(column upto operator + source+ lower_upper relation secify + no answer+ params)
                int columncount = 8 + 1 + 1 + 1 + (dtLimits.Rows.Count);
                string[,] dpsaveinstructios = new string[1, QC4Common.Common.Constants.DP.MAX_DP_COLUMN];//SAVE Array for the corresponding row in the sheet
                int param = 0;
                for (int i = 0; i < columncount; i++)
                {
                    switch (i)
                    {
                        case 0://onoff
                            dpsaveinstructios[0, i] = LocalResource.CELL_ON;
                            break;

                        case 1://checkcross
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
                            dpsaveinstructios[0, i] = command;
                            break;
                        default:
                            dpsaveinstructios[0, i] = paramList[param];
                            param++;
                            break;

                    }
                }

                for (int i = 1; i <= dtChoice.Rows.Count; i++)
                {
                    if (dtChoice.Rows[i - 1][1].ToString().Length > QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit)
                    {
                        dtChoice.Rows[i - 1][1] = dtChoice.Rows[i - 1][1].ToString().PadRight(QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit).Substring(0, QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit);
                    }
                }

                string question = Text_NewItem_Question.Text;
                if (question.Length > QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit)
                {
                    question = question.PadRight(QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit).Substring(0, QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit);
                }
                // Save to sheet
                Logic.DataProcessHelper dphelper = new Logic.DataProcessHelper();
                if (dphelper.WriteProcess(Workbook, Util.Constants.ProcessingType.CreateNewVariable, txt_new_variable.Text, answerType.Text, question, dt.Rows.Count, dtChoice, Constants.DP.SubstituteOperatorCLASS, dpsaveinstructios, isNew, WriteRow, ProcessingOption, null, isUpdate))//need to pass row num from here for saving 
                {

                    MessageDialog.Info(LocalResource.ADDQS_INFO_MSG_SUCCESS);
                    isModifiedProcess = true;
                    this.Close();

                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

        }

        private string GetUpper_Limit_Relation()
        {
            string relation = string.Empty;
            if (checkbox_greater_or_equal.IsChecked == true)
            {
                if (checkbox_less_or_equal1.IsChecked == true)
                {
                    relation = "1";
                }
                else
                {
                    relation = "2";
                }
            }
            else
            {
                if (checkbox_less_or_equal1.IsChecked == true)
                {
                    relation = "3";
                }
                else
                {
                    relation = "4";
                }
            }
            return relation;
        }


        private void PopulateClassData(Microsoft.Office.Interop.Excel.Workbook workbook, int readRow)
        {
            Microsoft.Office.Interop.Excel.Worksheet dataProcessSheet = Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Qc4Launcher.Util.Constants.SheetCodeName.DataProcessS);
            Microsoft.Office.Interop.Excel.Range rowstart = dataProcessSheet.Cells[readRow, 1];
            Microsoft.Office.Interop.Excel.Range rowend = dataProcessSheet.Cells[readRow, ExcelAddIn.Common.Constants.DP.MAX_DP_COLUMN];
            Microsoft.Office.Interop.Excel.Range range_EDIT_OR_COPY_Process = dataProcessSheet.Range[rowstart, rowend];

            try
            {
                SourceVariableList EDIT_OR_COPY_Process_Details = new SourceVariableList();
                List<string> Limts = new List<string>();
                List<string> lowerLimts = new List<string>();
                List<string> upperLimts = new List<string>();
                string noAnswer = "1";
                string limitsRelation = "1";

                if (range_EDIT_OR_COPY_Process.Cells.Count > 1)
                {
                    QuestionSettings qs = new QuestionSettings();
                    var obj = range_EDIT_OR_COPY_Process.Value;
                    //EDIT_OR_COPY_Process_Details.
                    if (obj[1, 3] != null)
                    {

                        EDIT_OR_COPY_Process_Details.Variable = obj[1, 11];
                        bool hasvalue = Util.Definiotion.VariableDictionary.TryGetValue(obj[1, ExcelAddIn.Common.Constants.DP.SubstituteVariableColumn], out qs);
                        if (qs != null)
                        {
                            noAnswer = obj[1, 13];
                            limitsRelation = obj[1, 12];

                            for (int i = 1; i <= qs.Choices.Count; i++)
                            {
                                Limts.Add(obj[1, 11 + 2 + i]);
                            }

                        }
                        SourceVariableList found = SourceVariableListView.FirstOrDefault(u => (u.Variable == EDIT_OR_COPY_Process_Details.Variable));
                        Combo_sourceVariableNType.SelectedIndex = SourceVariableListView.IndexOf(found);

                        if (ProcessingOption == QC4Common.Common.Constants.STD_DP.Process_Edit)
                        {
                            Color newVariableTextboxColor = (Color)ColorConverter.ConvertFromString("#F0F0F0");
                            Color editProcessMethodColor = (Color)ColorConverter.ConvertFromString("#FFADADAD");
                            lb_class.Background = new System.Windows.Media.SolidColorBrush(newVariableTextboxColor);
                            lb_class.Foreground = new System.Windows.Media.SolidColorBrush(editProcessMethodColor);
                            if (qs != null)
                            {
                                txt_new_variable.Text = qs.Variable;
                            }
                            else
                            {
                                txt_new_variable.Text = string.Empty;
                            }
                            txt_new_variable.IsReadOnly = true;
                            txt_new_variable.Background = new System.Windows.Media.SolidColorBrush(newVariableTextboxColor);
                            NewItemSearchbutton.IsEnabled = false;
                        }
                        else if (ProcessingOption == QC4Common.Common.Constants.STD_DP.Process_Copy)
                        {
                            if (qs != null)
                            {
                                QC4Common.Util.QSUtil qsutil = new QC4Common.Util.QSUtil();
                                txt_new_variable.Text = qsutil.GetVariableName(qs.Variable, PopulatedDictionary.Values.ToList());
                            }
                            else
                            {
                                txt_new_variable.Text = string.Empty;
                            }
                            
                        }

                        if (qs!= null)
                        {
                            answerType.Text = qs.AnswerType;
                            choicesCount.SelectedIndex = qs.Choices.Count;
                            Text_NewItem_Question.Text = qs.Question;
                            string not = string.Empty;
                            for (int i = 0; i < qs.Choices.Count; i++)
                            {
                                ChoiceListView[i].Choice = frmutil.EscapeCRLF(qs.Choices[i]);
                                not = frmutil.GetOperator(Convert.ToString(Limts[i]));
                                if (Limts[i] != null)
                                {
                                    Limts[i] = frmutil.TrimStartEqualNotequal(Convert.ToString(Limts[i]));
                                    if (Limts[i].IndexOf("-") == 0)
                                    {
                                        ChoiceListView[i].LowerLimit = null;
                                        ChoiceListView[i].UpperLimit = Limts[i].Remove(0, 1).Replace("(", string.Empty).Replace(")", string.Empty);
                                        //double resultUpperLimit = 0;
                                        //bool isFloatUpperLimit = double.TryParse(ChoiceListView[i].UpperLimit, out resultUpperLimit);
                                        //if (isFloatUpperLimit && Convert.ToDouble(ChoiceListView[i].UpperLimit) < 0)
                                        //{
                                        //    ChoiceListView[i].UpperLimit = Limts[i].Remove(0, 1);
                                        //}
                                    }
                                    else if (Limts[i].LastIndexOf("-") == Limts[i].Length - 1)
                                    {
                                        ChoiceListView[i].UpperLimit = null;
                                        ChoiceListView[i].LowerLimit = Limts[i].Remove((Limts[i].Length - 1), 1).Replace("(", string.Empty).Replace(")", string.Empty);
                                        //double resultLowerLimit = 0;
                                        //bool isFloatLowerLimit = double.TryParse(ChoiceListView[i].LowerLimit, out resultLowerLimit);
                                        //if (isFloatLowerLimit && Convert.ToDouble(ChoiceListView[i].LowerLimit) < 0)
                                        //{
                                        //    ChoiceListView[i].LowerLimit = Limts[i].Remove((Limts[i].Length - 1), 1);
                                        //}
                                    }
                                    else
                                    {
                                        int count = Limts[i].ToCharArray().Count(c => c == '-');
                                        if (count == 1)
                                        {
                                            string[] splittedValues = Limts[i].Split('-');
                                            ChoiceListView[i].LowerLimit = splittedValues[0];
                                            ChoiceListView[i].UpperLimit = splittedValues[1];
                                        }
                                        else
                                        {
                                            int indexofHiphen = Limts[i].IndexOf(')') + 1;
                                            int length = Limts[i].Length;
                                            ChoiceListView[i].LowerLimit = Limts[i].Remove(indexofHiphen, length - indexofHiphen).Replace("(", string.Empty).Replace(")", string.Empty);
                                            //double resultLowerLimit = 0;
                                            //bool isFloatLowerLimit = double.TryParse(ChoiceListView[i].LowerLimit, out resultLowerLimit);
                                            //if (isFloatLowerLimit && Convert.ToDouble(ChoiceListView[i].LowerLimit) < 0)
                                            //{
                                            //    ChoiceListView[i].LowerLimit = Limts[i].Remove(indexofHiphen, length - indexofHiphen);
                                            //}
                                            ChoiceListView[i].UpperLimit = Limts[i].Remove(0, indexofHiphen + 1).Replace("(", string.Empty).Replace(")", string.Empty);

                                            //double resultUpperLimit = 0;
                                            //bool isFloatUpperLimit = double.TryParse(ChoiceListView[i].UpperLimit, out resultUpperLimit);
                                            //if (isFloatUpperLimit && Convert.ToDouble(ChoiceListView[i].UpperLimit) < 0)
                                            //{
                                            //    ChoiceListView[i].UpperLimit = Limts[i].Remove(0, indexofHiphen + 1);
                                            //}
                                        }


                                    }
                                    if (!string.IsNullOrEmpty(not))
                                    {
                                        ChoiceListView[i].LowerLimit = not + ChoiceListView[i].LowerLimit;
                                    }

                                }
                                else
                                {
                                    ChoiceListView[i].LowerLimit = null;
                                    ChoiceListView[i].UpperLimit = null;
                                }

                            }
                        }
                        else
                        {
                            choicesCount.SelectedIndex = 0;
                            Text_NewItem_Question.Text = string.Empty;
                        }
                        

                        //Set No answer value
                        if (noAnswer == "1")
                        {
                            chk_noAnswer.IsChecked = false;
                        }
                        else
                        {
                            chk_noAnswer.IsChecked = true;
                        }

                        // Set upper and lower limit relation specify

                        switch (limitsRelation)
                        {
                            case "1":
                                checkbox_greater_or_equal.IsChecked = true;
                                checkbox_less_or_equal1.IsChecked = true;
                                break;
                            case "2":
                                checkbox_greater_or_equal.IsChecked = true;
                                checkbox_less.IsChecked = true;
                                break;
                            case "3":
                                checkbox_greater.IsChecked = true;
                                checkbox_less_or_equal1.IsChecked = true;
                                break;
                            case "4":
                                checkbox_greater.IsChecked = true;
                                checkbox_less.IsChecked = true;
                                break;
                            default:
                                checkbox_greater_or_equal.IsChecked = true;
                                checkbox_less_or_equal1.IsChecked = true;
                                break;
                        }


                    }

                    // Assign to temp variable
                    newVariableBeforeEdit = txt_new_variable.Text;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

        }
        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SetChoiceLabels_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                bool isAllEmpty = false;
                var found = ChoiceListView.LastOrDefault(u => (u.Choice != null) || (u.LowerLimit != null) || (u.UpperLimit != null));
                int foundIndex = ChoiceListView.IndexOf(found);
                Class_SetChoiceLabelswindow setChoiceLabelsWindow = new Class_SetChoiceLabelswindow(inputUnitsBefore, isThousandSeparatorChecked);
                if (found == null)
                {
                    setChoiceLabelsWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                    setChoiceLabelsWindow.Owner = this;
                    setChoiceLabelsWindow.ShowDialog();
                    setChoiceLabelsWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    if (setChoiceLabelsWindow.IsExecuted)
                    {
                        inputUnitsBefore = setChoiceLabelsWindow.InputUnits;
                        isThousandSeparatorChecked = setChoiceLabelsWindow.IsThousandSeparatorEnabled;
                    }
                    else
                    {
                        inputUnitsBefore = setChoiceLabelsWindow.InputUnits;
                        isThousandSeparatorChecked = setChoiceLabelsWindow.IsThousandSeparatorEnabled;
                    }
                }
                else
                {
                    for (int i = 0; i <= foundIndex; i++)
                    {
                        string cellChoice = Convert.ToString(ChoiceListView[i].Choice);
                        string cellvalue1 = Convert.ToString(ChoiceListView[i].LowerLimit);
                        string cellvalue2 = Convert.ToString(ChoiceListView[i].UpperLimit);

                        if (cellChoice == null || string.IsNullOrEmpty(cellChoice.TrimEnd().TrimStart()))
                        {

                        }
                        if (!string.IsNullOrEmpty(cellvalue1))
                        {
                            cellvalue1 = cellvalue1.TrimEnd().TrimStart();
                        }
                        if (!string.IsNullOrEmpty(cellvalue2))
                        {
                            cellvalue2 = cellvalue2.TrimEnd().TrimStart();
                        }

                        if (cellvalue1 != null || cellvalue2 != null)
                        {
                            bool isValid = false;
                            string[] limits = new string[2];
                            string[] limitsWithBrackets = new string[2];
                            string cellvalue1WithBrackets = cellvalue1;
                            string cellvalue2WithBrackets = cellvalue2;
                            limitsWithBrackets[0] = cellvalue1WithBrackets;
                            limitsWithBrackets[1] = cellvalue2WithBrackets;
                            cellvalue1 = frmutil.ReplaceBrackets(cellvalue1);
                            cellvalue2 = frmutil.ReplaceBrackets(cellvalue2);
                            limits[0] = cellvalue1;
                            limits[1] = cellvalue2;
 

                            for (int j = 0; j < 2; j++)
                            {
                                if (CheckOnlyBracketsPresent(limitsWithBrackets[j]))
                                {
                                    ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_SET_A_NUMERIC_VALUE));
                                    if (j == 0)
                                    {
                                        SetInvalidColorLowerLimit(i);
                                    }
                                    else if (j == 1)
                                    {
                                        SetInvalidColorUpperLimit(i);
                                    }
                                    return;
                                }
                                if (limits[j] != null && limits[j].TrimEnd().TrimStart() != "")
                                {
                                    float result = 0;
                                    bool isInteger = float.TryParse(limits[j], out result);

                                    if (!frmutil.IsLimitPresent(limits[j]))
                                    {
                                        string entry = frmutil.TrimStartEqualNotequal(Convert.ToString(limits[j]));
                                        if (entry.ToCharArray().Count(c => c == '-') == 1 && entry.IndexOf("-") == 0)
                                        {
                                            if (entry == "-0")
                                            {
                                                ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_MULTIPLE_LIMIT_SET));
                                                if (j == 0)
                                                {
                                                    SetInvalidColorLowerLimit(i);
                                                }
                                                else if (j == 1)
                                                {
                                                    SetInvalidColorUpperLimit(i);
                                                }
                                                return;
                                            }
                                            if ((limitsWithBrackets[j].Contains("(")))
                                            {
                                                if (limitsWithBrackets[j].IndexOf("(") == 0)
                                                {
                                                    //Valid entry
                                                    isValid = true;
                                                }
                                                else if ((limitsWithBrackets[j].Contains('!') && limitsWithBrackets[j].IndexOf('!') == 0 && limitsWithBrackets[j].IndexOf('(') == 1))
                                                {
                                                    isValid = true;
                                                    //Valid entry
                                                }

                                            }
                                            else
                                            {

                                                isValid = true;
                                                //Valid entry
                                            }

                                        }
                                        else
                                        {
                                            ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_MULTIPLE_LIMIT_SET));
                                            if (j == 0)
                                            {
                                                SetInvalidColorLowerLimit(i);
                                            }
                                            else if (j == 1)
                                            {
                                                SetInvalidColorUpperLimit(i);
                                            }
                                            return;
                                        }
                                    }
                                    
                                    if (!isInteger && !limits[j].Contains("!"))
                                    {
                                        ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_SET_A_NUMERIC_VALUE));
                                        if (j == 0)
                                        {
                                            SetInvalidColorLowerLimit(i);
                                        }
                                        else if (j == 1)
                                        {
                                            SetInvalidColorUpperLimit(i);
                                        }
                                        return;
                                    }
                                    if (j == 1 && limits[j].Contains("!"))
                                    {
                                        MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, (i + 1)) + "\n" + (LocalResource.ERR_MSG_RECODE_CANNOT_SET_ELSEWHERE_THAN_AT_START));
                                        SetInvalidColorUpperLimit(i);
                                        return;
                                    }
                                    if (j == 0 && limits[j].Contains("!") && limits[j].Count() == 1 && (cellvalue2 == null || cellvalue2 == ""))
                                    {
                                        MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, (i + 1)) + "\n" + (LocalResource.ERR_MSG_RECODE_INCORRECT_RANGE_SPECIFICATION));
                                        SetInvalidColorLowerLimit(i);
                                        return;
                                    }
                                    if (j == 0 && limits[j].ToCharArray().Count(c => c == '!') > 1)
                                    {
                                        MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, (i + 1)) + "\n" + (LocalResource.ERR_MSG_RECODE_CANNOT_SET_ELSEWHERE_THAN_AT_START));
                                        SetInvalidColorLowerLimit(i);
                                        return;
                                    }
                                    if (j == 0 && limitsWithBrackets[j].Contains("!") && limitsWithBrackets[j].IndexOf("!") != 0)
                                    {
                                        MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, (i + 1)) + "\n" + (LocalResource.ERR_MSG_RECODE_CANNOT_SET_ELSEWHERE_THAN_AT_START));
                                        SetInvalidColorLowerLimit(i);
                                        return;
                                    }
                                    if (!isInteger && !frmutil.IsNumeric(limits[j]) && !isValid)
                                    {
                                        ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_SET_A_NUMERIC_VALUE));
                                        if (j == 0)
                                        {
                                            SetInvalidColorLowerLimit(i);
                                        }
                                        else if (j == 1)
                                        {
                                            SetInvalidColorUpperLimit(i);
                                        }
                                        return;
                                    }
                                    if (!isInteger && !frmutil.IsLimitPresent(limits[j]) && !isValid)
                                    {
                                        ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_MULTIPLE_LIMIT_SET));
                                        if (j == 0)
                                        {
                                            SetInvalidColorLowerLimit(i);
                                        }
                                        else if (j == 1)
                                        {
                                            SetInvalidColorUpperLimit(i);
                                        }
                                        return;
                                    }
                                    if ((limits[j]).Contains("/"))
                                    {
                                        ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_CANNOT_SPECIFY_SPLIT_RANGE));
                                        if (j == 0)
                                        {
                                            SetInvalidColorLowerLimit(i);
                                        }
                                        else if (j == 1)
                                        {
                                            SetInvalidColorUpperLimit(i);
                                        }
                                        return;
                                    }
                                    if ((limits[j]).Contains(","))
                                    {
                                        ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_CANNOT_SPECIFY_SPLIT_RANGE));
                                        if (j == 0)
                                        {
                                            SetInvalidColorLowerLimit(i);
                                        }
                                        else if (j == 1)
                                        {
                                            SetInvalidColorUpperLimit(i);
                                        }
                                        return;
                                    }
                                }


                            }
                            if (cellvalue1 != null && cellvalue1.Contains("!"))
                            {
                                cellvalue1 = cellvalue1.Remove(0, 1);
                                if (!frmutil.IsLowerLimitLesser(cellvalue1, cellvalue2))
                                {
                                    ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_SET_LOWER_LIMIT_LESS_THAN_UPPER_LIMIT));
                                    SetInvalidColorLowerLimit(i);
                                    return;
                                }
                            }
                            else
                            {
                                if (!frmutil.IsLowerLimitLesser(cellvalue1, cellvalue2))
                                {
                                    ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_SET_LOWER_LIMIT_LESS_THAN_UPPER_LIMIT));
                                    SetInvalidColorLowerLimit(i);
                                    return;
                                }
                            }


                        }

                    }
                }

                setChoiceLabelsWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                setChoiceLabelsWindow.Owner = this;
                setChoiceLabelsWindow.ShowDialog();
                setChoiceLabelsWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                if (setChoiceLabelsWindow.IsExecuted)
                {
                    for (int i = 0; i <= foundIndex; i++)
                    {
                        if (!string.IsNullOrEmpty(ChoiceListView[i].UpperLimit))
                        {
                            ChoiceListView[i].UpperLimit = ChoiceListView[i].UpperLimit.TrimStart().TrimEnd();
                        }
                        if (!string.IsNullOrEmpty(ChoiceListView[i].LowerLimit))
                        {
                            ChoiceListView[i].LowerLimit = ChoiceListView[i].LowerLimit.TrimStart().TrimEnd();
                        }
                        if (ChoiceListView[i].UpperLimit == null && ChoiceListView[i].LowerLimit == null)
                        {
                            ChoiceListView[i].Choice = ChoiceListView[i].Choice;
                        }
                        else if (ChoiceListView[i].UpperLimit == null || ChoiceListView[i].UpperLimit == "" || ChoiceListView[i].LowerLimit == null || ChoiceListView[i].LowerLimit == "")
                        {
                            if (ChoiceListView[i].UpperLimit == null || ChoiceListView[i].UpperLimit == "")
                            {
                                if (setChoiceLabelsWindow.IsThousandSeparatorEnabled)
                                {
                                    if (!string.IsNullOrEmpty(ChoiceListView[i].LowerLimit))
                                    {
                                      string output = string.Format("{0:N10}", Convert.ToDouble(frmutil.TrimStartEqualNotequal(frmutil.ReplaceBrackets(ChoiceListView[i].LowerLimit)))).TrimEnd('0');
                                        if (output.IndexOf('.') == output.Length - 1)
                                        {
                                            output = output.Remove(output.IndexOf('.'));
                                        }
                                        if (frmutil.TrimStartEqualNotequal(ChoiceListView[i].LowerLimit).IndexOf('(') == 0)
                                        {
                                            if (ChoiceListView[i].LowerLimit.Contains('!'))
                                            {
                                                output = "!"+"(" + output + ")";
                                            }
                                            else
                                            {
                                                output = "(" + output + ")";
                                            }
                                        }
                                        ChoiceListView[i].Choice = output + setChoiceLabelsWindow.InputUnits + LocalResource.LBL_TILDE;
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(ChoiceListView[i].LowerLimit))
                                    {
                                        ChoiceListView[i].Choice = ChoiceListView[i].LowerLimit + setChoiceLabelsWindow.InputUnits + LocalResource.LBL_TILDE;
                                    }
                                }
                            }
                            else if (ChoiceListView[i].LowerLimit == null || ChoiceListView[i].LowerLimit == "")
                            {
                                if (setChoiceLabelsWindow.IsThousandSeparatorEnabled)
                                {
                                    if (!string .IsNullOrEmpty(ChoiceListView[i].UpperLimit))
                                    {
                                        string output = string.Format("{0:N10}", Convert.ToDouble(frmutil.TrimStartEqualNotequal(frmutil.ReplaceBrackets(ChoiceListView[i].UpperLimit)))).TrimEnd('0');
                                        if (output.IndexOf('.') == output.Length - 1)
                                        {
                                            output = output.Remove(output.IndexOf('.'));
                                        }
                                        if (ChoiceListView[i].UpperLimit.IndexOf('(') == 0)
                                        {
                                            output = "(" + output + ")";
                                        }
                                        ChoiceListView[i].Choice = LocalResource.LBL_TILDE + output + setChoiceLabelsWindow.InputUnits;
                                    }
                      
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(ChoiceListView[i].UpperLimit))
                                    {
                                        ChoiceListView[i].Choice = LocalResource.LBL_TILDE + ChoiceListView[i].UpperLimit + setChoiceLabelsWindow.InputUnits;
                                    }
                                }

                            }
                        }
                        else
                        {
                            if (setChoiceLabelsWindow.IsThousandSeparatorEnabled)
                            {
                                if (!string.IsNullOrEmpty(ChoiceListView[i].LowerLimit) && !string.IsNullOrEmpty(ChoiceListView[i].UpperLimit))
                                {
                                    string output = string.Format("{0:N10}", Convert.ToDouble(frmutil.TrimStartEqualNotequal(frmutil.ReplaceBrackets(ChoiceListView[i].LowerLimit)))).TrimEnd('0');
                                    string output2 = string.Format("{0:N10}", Convert.ToDouble(frmutil.TrimStartEqualNotequal(frmutil.ReplaceBrackets(ChoiceListView[i].UpperLimit)))).TrimEnd('0');
                                    if (output.IndexOf('.') == output.Length - 1)
                                    {
                                        output = output.Remove(output.IndexOf('.'));
                                    }
                                    if (output2.IndexOf('.') == output2.Length - 1)
                                    {
                                        output2 = output2.Remove(output2.IndexOf('.'));
                                    }
                                    if (frmutil.TrimStartEqualNotequal(ChoiceListView[i].LowerLimit).IndexOf('(') == 0)
                                    {
                                        if (ChoiceListView[i].LowerLimit.Contains('!'))
                                        {
                                            output = "!" + "(" + output + ")";
                                        }
                                        else
                                        {
                                            output = "(" + output + ")";
                                        }
                                    }
                                    else if (ChoiceListView[i].LowerLimit.Contains('!') && ChoiceListView[i].LowerLimit.IndexOf('!') ==0)
                                    {
                                        output = "!" + output;
                                    }
                                    if (ChoiceListView[i].UpperLimit.IndexOf('(') == 0)
                                    {
                                        output2 = "(" + output2 + ")";
                                    }
                                    ChoiceListView[i].Choice = output + setChoiceLabelsWindow.InputUnits + LocalResource.LBL_TILDE + output2 + setChoiceLabelsWindow.InputUnits;
                                }

                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(ChoiceListView[i].LowerLimit) || !string.IsNullOrEmpty(ChoiceListView[i].UpperLimit))
                                {
                                    ChoiceListView[i].Choice = ChoiceListView[i].LowerLimit + setChoiceLabelsWindow.InputUnits + LocalResource.LBL_TILDE + ChoiceListView[i].UpperLimit + setChoiceLabelsWindow.InputUnits;
                                }
                            }

                        }
                    }

                    inputUnitsBefore = setChoiceLabelsWindow.InputUnits;
                    isThousandSeparatorChecked = setChoiceLabelsWindow.IsThousandSeparatorEnabled;
                    CollectionViewSource.GetDefaultView(data_grid.ItemsSource).Refresh();
                }
                else
                {
                    inputUnitsBefore = setChoiceLabelsWindow.InputUnits;
                    isThousandSeparatorChecked = setChoiceLabelsWindow.IsThousandSeparatorEnabled;
                }


            }
            catch (Exception ex)
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }


        }

        private void Combo_sourceVariableNType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Windows.Controls.ComboBox sourceVariable = ((System.Windows.Controls.ComboBox)sender);
            if (ProcessingOption != QC4Common.Common.Constants.STD_DP.Process_Edit)
            {

                if (sourceVariable.SelectedItem != null)
                {
                    txt_answerType.IsEnabled = true;
                    txt_source_Question.IsEnabled = true;
                    txt_new_variable.IsEnabled = true;
                    txt_new_variable.Background = Brushes.White;
                    answerType.IsEnabled = true;
                    Text_NewItem_Question.IsEnabled = true;
                    Text_NewItem_Question.Background = Brushes.White;

                    SourceVariableList list = new SourceVariableList();
                    list = (SourceVariableList)(sourceVariable.SelectedItem);

                    NewItemSearchbutton.IsEnabled = true;
                    NewItemSearchbutton.Opacity = 1;
                    Save.IsEnabled = true;
                    txt_answerType.Text = list.AnswerType;
                    txt_source_Question.Text = frmutil.UnEscapeCRLF(list.Question);
                    QC4Common.Util.QSUtil qsutil = new QC4Common.Util.QSUtil();
                    txt_new_variable.Text = qsutil.GetVariableName(list.Variable, PopulatedDictionary.Values.ToList());
                    answerType.Text = "SA";

                    Text_NewItem_Question.Text = (frmutil.UnEscapeCRLF(Util.Definiotion.VariableDictionary[list.Variable].TableHeading) + " " + frmutil.UnEscapeCRLF(list.Question)).TrimEnd().TrimStart();

                    if (Util.Definiotion.VariableDictionary.ContainsKey(list.Variable))
                    {
                        if (Util.Definiotion.VariableDictionary[list.Variable].QuestionFlag != "New")
                        {
                            frmutil.NtypeGetMinMaxAvg((Util.Definiotion.VariableDictionary[list.Variable].ItemId) == 0 ? "sample_id" : "q_" + (Util.Definiotion.VariableDictionary[list.Variable].ItemId).ToString(), Workbook, out min, out max, out avg);
                            txt_min.Text = min.ToString();
                            txt_max.Text = max.ToString();
                            txt_mean.Text = avg.ToString();
                        }
                        else
                        {
                            txt_min.Text = string.Empty;
                            txt_max.Text = string.Empty;
                            txt_mean.Text = string.Empty;
                        }

                    }

                    //Handles if selected variable is AGE - N type
                    if (list.Variable == "AGE")
                    {
                        choicesCount.SelectedIndex = int.Parse(txt_max.Text) / 10;
                        for (int i = 0; i < choicesCount.SelectedIndex; i++)
                        {
                            if (i == 0)
                            {
                                string choice = ((i + 1) * 10).ToString() + LocalResource.LBL_CLASS_TEENS + " " + LocalResource.LBL_CLASS_OR_UNDER;
                                ChoiceListView[i].Choice = choice;
                                ChoiceListView[i].LowerLimit = null;
                                ChoiceListView[i].UpperLimit = ((((i + 1) * 10)) + 9).ToString();
                            }
                            else
                            {
                                string choice = ((i + 1) * 10).ToString() + LocalResource.LBL_CLASS_TEENS;
                                ChoiceListView[i].Choice = choice;
                                ChoiceListView[i].LowerLimit = ((i + 1) * 10).ToString();
                                ChoiceListView[i].UpperLimit = ((((i + 1) * 10)) + 9).ToString();
                            }

                        }
                        CollectionViewSource.GetDefaultView(data_grid.ItemsSource).Refresh();
                        isAGESelected = true;
                    }
                    else if (isAGESelected)
                    {
                        choicesCount.SelectedIndex = 0;
                        for (int i = 0; i < ChoiceListView.Count; i++)
                        {
                            ChoiceListView[i].Choice = null;
                            ChoiceListView[i].LowerLimit = null;
                            ChoiceListView[i].UpperLimit = null;
                        }
                        CollectionViewSource.GetDefaultView(data_grid.ItemsSource).Refresh();
                        isAGESelected = false;
                    }
                }
            }
            else
            {
                if (sourceVariable.SelectedItem != null)
                {
                    txt_answerType.IsEnabled = true;
                    txt_source_Question.IsEnabled = true;
                    txt_new_variable.IsEnabled = true;
                    answerType.IsEnabled = true;
                    Text_NewItem_Question.IsEnabled = true;
                    Text_NewItem_Question.Background = Brushes.White;
                    Save.IsEnabled = true;

                    SourceVariableList list = new SourceVariableList();
                    list = (SourceVariableList)(sourceVariable.SelectedItem);
                    txt_answerType.Text = list.AnswerType;
                    txt_source_Question.Text = frmutil.UnEscapeCRLF(list.Question);
                    if (Util.Definiotion.VariableDictionary.ContainsKey(list.Variable))
                    {
                        if (Util.Definiotion.VariableDictionary[list.Variable].QuestionFlag != "New")
                        {
                            frmutil.NtypeGetMinMaxAvg((Util.Definiotion.VariableDictionary[list.Variable].ItemId) == 0 ? "sample_id" : "q_" + (Util.Definiotion.VariableDictionary[list.Variable].ItemId).ToString(), Workbook, out min, out max, out avg);
                            txt_min.Text = min.ToString();
                            txt_max.Text = max.ToString();
                            txt_mean.Text = avg.ToString();
                        }
                        else
                        {
                            txt_min.Text = string.Empty;
                            txt_max.Text = string.Empty;
                            txt_mean.Text = string.Empty;
                        }

                    }

                }

            }
        }

        private void Combo_sourceVariableNType_DropDownClosed(object sender, EventArgs e)
        {
            System.Windows.Controls.ComboBox sen = ((System.Windows.Controls.ComboBox)sender);
            if (sen.SelectedItem != null)
            {
                var comboItem = sen.SelectedItem;
                int index = sen.Items.IndexOf(comboItem);
                sen.SelectedIndex = index;
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
        private void Combo_sourceVariableNType_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (!FirstFocus)
            {
                System.Windows.Controls.ComboBox sen = ((System.Windows.Controls.ComboBox)sender);
            }
            else
                FirstFocus = false;
        }
        #endregion

        private void Combo_sourceVariableNType_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (Key.Tab != e.Key && !frmutil.IsSystemKey(e))
            {
                Combo_KeyUp(Combo_sourceVariableNType, sender, e);
            }
        }

        private void Combo_sourceVariableNType_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Combo_KeyDown(Combo_sourceVariableNType, sender, e);
        }

        private void PopulateNVariableList()
        {
            var SettingSheet = ExcelUtil.GetWorkSheetByCodeName(Workbook, "Sheet91");
            PopulatedDictionary = Util.Definiotion.VariableDictionary;

            if (SettingSheet != null)
            {
                Microsoft.Office.Interop.Excel.Range Range = SettingSheet.get_Range("List_Item_N");
                if (Range != null)
                {
                    if (Range.Cells.Count == 1)
                    {
                        foreach (KeyValuePair<string, QuestionSettings> item in PopulatedDictionary)
                        {
                            QuestionSettings qs = item.Value;
                            if (qs.Variable == Range.Value)
                            {
                                sourceVariableList.Add(new SourceVariableList()
                                {
                                    Variable = frmutil.EscapeCRLF(qs.Variable),
                                    AnswerType = qs.AnswerType,
                                    Question = frmutil.EscapeCRLF(qs.Question),
                                });

                            }
                        }
                    }
                    else
                    {
                        var objAry = Range.Value;
                        int max = objAry.GetLength(0);
                        for (int i = 1; i <= max; i++)
                        {
                            if (objAry[i, 1] != null)
                            {
                                foreach (KeyValuePair<string, QuestionSettings> item in PopulatedDictionary)
                                {
                                    QuestionSettings qs = item.Value;
                                    if (qs.Variable == objAry[i, 1])
                                    {
                                        sourceVariableList.Add(new SourceVariableList()
                                        {
                                            Variable = frmutil.EscapeCRLF(qs.Variable),
                                            AnswerType = qs.AnswerType,
                                            Question = frmutil.EscapeCRLF(qs.Question),
                                        });

                                    }
                                }
                            }
                        }
                    }
                }
            }

        }

        private void Window_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
        string clipboardText = "";
        private void HandleCopyPaste(object sender, KeyEventArgs e)
        {
            int datagridColumn = data_grid.CurrentCell.Column.DisplayIndex;
            if (data_grid.SelectedIndex == -1) { data_grid.SelectedIndex = 0; }
            DataGridCell cell = frmutil.GetCell(data_grid, data_grid.SelectedIndex, datagridColumn);
            bool _altModifierPressed = (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl));
            if (_altModifierPressed && Keyboard.IsKeyDown(Key.V))
            {
                var uiElement = e.OriginalSource as UIElement;
                try
                {
                    Util.QS.GridCopyPaste copyPaste = new Util.QS.GridCopyPaste();
                    var data = copyPaste.PastetoDatagrid(sender);
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
                                if (No_Column > 3)
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
                                            if (col == 1)
                                            {
                                                if (data[i, (j - 1)] == null)
                                                {
                                                    ChoiceListView[RowIndex].Choice = null;
                                                }
                                                else
                                                {
                                                    ChoiceListView[RowIndex].Choice = data[i, (j - 1)].ToString();
                                                }

                                            }
                                            else if (col == 2)
                                            {
                                                if (data[i, (j - 1)] == null)
                                                {
                                                    ChoiceListView[RowIndex].LowerLimit = null;
                                                }
                                                else
                                                {
                                                    ChoiceListView[RowIndex].LowerLimit = data[i, (j - 1)].ToString();
                                                }
                                            }
                                            else if (col == 3)
                                            {
                                                if (data[i, (j - 1)] == null)
                                                {
                                                    ChoiceListView[RowIndex].UpperLimit = null;
                                                }
                                                else
                                                {
                                                    ChoiceListView[RowIndex].UpperLimit = data[i, (j - 1)].ToString();
                                                }

                                            }

                                        }

                                    }

                                }
                            }
                            else if (data_grid.CurrentCell.Column.DisplayIndex == 2)
                            {
                                if (No_Column > 2)
                                {
                                    MessageDialog.ErrorOk(LocalResource.COPY_ERROR_MSG);
                                    e.Handled = true;
                                }
                                else
                                {
                                    int RowIndex = data_grid.SelectedIndex;
                                    for (int i = 0; i < No_Row; i++, RowIndex++)
                                    {
                                        for (int j = 1, col = 2; j <= No_Column; j++, col++)
                                        {
                                            if (col == 2)
                                            {
                                                if (data[i, (j - 1)] == null)
                                                {
                                                    ChoiceListView[RowIndex].LowerLimit = null;
                                                }
                                                else
                                                {
                                                    ChoiceListView[RowIndex].LowerLimit = data[i, (j - 1)].ToString();
                                                }

                                            }
                                            else if (col == 3)
                                            {
                                                if (data[i, (j - 1)] == null)
                                                {
                                                    ChoiceListView[RowIndex].UpperLimit = null;
                                                }
                                                else
                                                {
                                                    ChoiceListView[RowIndex].UpperLimit = data[i, (j - 1)].ToString();
                                                }

                                            }
                                        }
                                    }
                                }
                            }
                            else if (data_grid.CurrentCell.Column.DisplayIndex == 4)
                            {
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
                                        for (int j = 1, col = 3; j <= No_Column; j++, col++)
                                        {
                                            if (data[i, (j - 1)] == null)
                                            {
                                                ChoiceListView[RowIndex].UpperLimit = null;
                                            }
                                            else
                                            {
                                                ChoiceListView[RowIndex].UpperLimit = data[i, (j - 1)].ToString();
                                            }

                                        }
                                    }
                                }
                            }
                            data_grid.CommitEdit();
                            data_grid.CommitEdit();
                            CollectionViewSource.GetDefaultView(data_grid.ItemsSource).Refresh();
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
                    if ((data_grid.SelectedItems != null) && (data_grid.SelectedItems.Count > 0))
                    {

                        foreach (var item in data_grid.SelectedItems)
                        {
                            var id = (ChoiceListView.First(x => x == item) as ChoiceList).Id;
                            ChoiceListView[id - 1].Choice = null;
                            ChoiceListView[id - 1].LowerLimit = null;
                            ChoiceListView[id - 1].UpperLimit = null;
                        }
                        this.data_grid.CommitEdit();
                        this.data_grid.CommitEdit();
                        CollectionViewSource.GetDefaultView(data_grid.ItemsSource).Refresh();
                        data_grid.Focus();
                        data_grid.FocusVisualStyle = null;
                    }
                }
                catch (Exception ex) { _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException); }
            }
        }

        private void Data_grid_CopyingRowClipboardContent(object sender, DataGridRowClipboardEventArgs e)
        {
            try
            {
                //Removes id column from copying
                e.ClipboardRowContent.RemoveAt(0);

                //Removes "~" column from copying
                e.ClipboardRowContent.RemoveAt(2);
            }
            catch (Exception ex) { _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException); }
        }

        /// <summary>Sets the color of the invalid cell.</summary>
        /// <param name="index">The index.</param>
        private void SetInvalidColorLowerLimit(int index)
        {
            ChoiceListView[index].IsLowerLimitInvalid = true;
            CollectionViewSource.GetDefaultView(data_grid.ItemsSource).Refresh();

        }
        /// <summary>Sets the color of the invalid cell.</summary>
        /// <param name="index">The index.</param>
        private void SetInvalidColorUpperLimit(int index)
        {
            ChoiceListView[index].IsUpperLimitInvalid = true;
            CollectionViewSource.GetDefaultView(data_grid.ItemsSource).Refresh();

        }
        /// <summary>Sets the BG color of the invalid criteria cell.</summary>
        /// <param name="index">The index.</param>
        private void SetInvalidBGLowerLimit(int index)
        {
            ChoiceListView[index].IsLowerLimitEmpty = true;
            CollectionViewSource.GetDefaultView(data_grid.ItemsSource).Refresh();

        }
        /// <summary>Sets the BG color of the invalid criteria cell.</summary>
        /// <param name="index">The index.</param>
        private void SetInvalidBGUpperLimit(int index)
        {
            ChoiceListView[index].IsUpperLimitEmpty = true;
            CollectionViewSource.GetDefaultView(data_grid.ItemsSource).Refresh();

        }
        /// <summary>Sets the BG color of the invalid choice cell.</summary>
        /// <param name="index">The index.</param>
        private void SetInvalidBGChoice(int index)
        {
            ChoiceListView[index].IsChoiceEmpty = true;
            CollectionViewSource.GetDefaultView(data_grid.ItemsSource).Refresh();

        }

        /// <summary>Checks whether only brackets present in the given string.</summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        private bool CheckOnlyBracketsPresent(string input)
        {
            bool isOnlyBrackes = false;
            if (!string.IsNullOrEmpty(input))
            {
                input = input.TrimEnd().TrimStart();
                int n = input.Length;
                int count = 0;
                for (int i = 0; i <= n - 1; i++)
                {
                    if (input[i] == '(' || input[i] == ')')
                    {
                        count++;
                    }
                    else
                    {
                        isOnlyBrackes = false;
                        return isOnlyBrackes;
                    }
                }
                if (count == n)
                {
                    if (input != string.Empty)
                    {
                        isOnlyBrackes = true;
                    } 
                }
            }
            else
            {
                isOnlyBrackes = false;
            }

            return isOnlyBrackes;

        }

        private void Data_grid_IsMouseCaptureWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            try
            {
                if (data_grid.SelectedItems != null)
                {
                    var items = data_grid.SelectedItems.Cast<ChoiceList>().ToList();
                    if (items.Count == 1)
                    {
                        if (items[0].IsChoiceEmpty == true || items[0].IsLowerLimitEmpty == true || items[0].IsUpperLimitEmpty == true || items[0].IsLowerLimitInvalid == true || items[0].IsUpperLimitInvalid == true)
                        {
                            items[0].IsChoiceEmpty = false;
                            items[0].IsLowerLimitEmpty = false;
                            items[0].IsUpperLimitEmpty = false;
                            items[0].IsLowerLimitInvalid = false;
                            items[0].IsUpperLimitInvalid = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

        }

        private void Data_grid_PreviewKeyUp(object sender, KeyEventArgs e)
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
        DataProcessNewLoad dataLoad = new DataProcessNewLoad();
        private void Txt_new_variable_TextChanged(object sender, TextChangedEventArgs e)
        {
            QuestionSettings qs = dataLoad.Txt_Change_New_Item(txt_new_variable.Text);
            if (qs != null)
            {
               
                Text_NewItem_Question.Text = (frmutil.UnEscapeCRLF(qs.TableHeading) + " " + frmutil.UnEscapeCRLF(qs.Question)).TrimStart().TrimEnd();
                choicesCount.SelectedIndex = qs.Choices.Count;
                if (ChoiceListView.Count == qs.Choices.Count)
                {

                    for (int i = 0; i < choicesCount.SelectedIndex; i++)
                    {
                        ChoiceListView[i].Choice = frmutil.EscapeCRLF(qs.Choices[i]);
                        ChoiceListView[i].LowerLimit = null;
                        ChoiceListView[i].UpperLimit = null;
                    }
                    CollectionViewSource.GetDefaultView(data_grid.ItemsSource).Refresh();
                }
                else
                {
                    IsExistingNewItemAdded = true;
                }
            }
        }

        /// <summary></summary>
        public class SourceVariableList
        {
            private string variable;
            private string answertype;
            private string question;

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
        }
        /// <summary></summary>
        public class ChoiceList : INotifyPropertyChanged
        {
            private int id;
            private string upperLimit;
            private string tild;
            private string choice;
            private string lowerLimit;
            private bool isLowerLimitInvalid = false;
            private bool isLowerLimitEmpty = false;
            private bool isUpperLimitInvalid = false;
            private bool isUpperLimitEmpty = false;
            private bool isChoiceEmpty = false;

            public int Id
            {
                get
                {
                    return id;
                }
                set
                {
                    id = value;
                }
            }
            public bool IsLowerLimitInvalid
            {
                get
                {
                    return isLowerLimitInvalid;
                }
                set
                {
                    isLowerLimitInvalid = value;
                    NotifyPropertyChanged();
                }
            }
            public bool IsLowerLimitEmpty
            {
                get
                {
                    return isLowerLimitEmpty;
                }
                set
                {
                    isLowerLimitEmpty = value;
                    NotifyPropertyChanged();
                }
            }
            public bool IsUpperLimitInvalid
            {
                get
                {
                    return isUpperLimitInvalid;
                }
                set
                {
                    isUpperLimitInvalid = value;
                    NotifyPropertyChanged();
                }
            }
            public bool IsUpperLimitEmpty
            {
                get
                {
                    return isUpperLimitEmpty;
                }
                set
                {
                    isUpperLimitEmpty = value;
                    NotifyPropertyChanged();
                }
            }
            public bool IsChoiceEmpty
            {
                get
                {
                    return isChoiceEmpty;
                }
                set
                {
                    isChoiceEmpty = value;
                    NotifyPropertyChanged();
                }
            }

            public string UpperLimit
            {
                get
                {
                    return upperLimit;
                }
                set
                {
                    upperLimit = value;
                }
            }
            public string Choice
            {
                get
                {
                    return choice;
                }
                set
                {
                    choice = value;
                }
            }

            public string LowerLimit
            {
                get
                {
                    return lowerLimit;
                }
                set
                {
                    lowerLimit = value;
                }
            }
            public string Tild
            {
                get
                {
                    return tild;
                }
                set
                {
                    tild = value;
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


    }
}
