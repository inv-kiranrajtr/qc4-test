using ExcelAddIn.Common;
using ExcelAddIn.Sheets;
using log4net;
using QC4Common.DB;
using QC4Common.Model;
using Qc4Launcher.Logic;
using Qc4Launcher.Logic.DataImport;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
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
using static Qc4Launcher.Forms.DP_Main.DP_Main;

namespace Qc4Launcher.Forms.DP_Main
{
    /// <summary>
    /// Interaction logic for RECODE.xaml
    /// </summary>
    public partial class RECODE : Window
    {
        public static List<string> MASAvariables = new List<string>();
        QC4Common.Util.FormUtil frmutil = new QC4Common.Util.FormUtil();
        ObservableCollection<ChoiceList> choiceList = new ObservableCollection<ChoiceList>();
        List<SourceVariableList> repeatsList = new List<SourceVariableList>();
        public bool isRepeatEnabled = false;
        string[] choicesList = new string[201];
        string[] repeatParams = new string[3];
        public static string currentSourceVariable = string.Empty;
        Microsoft.Office.Interop.Excel.Workbook Workbook;
        int ReadRow;
        int WriteRow;
        bool isRepeatCase = false;
        private bool isNewQuestion = true;
        private bool isUpdateQuestion = false;
        public bool isModifiedProcess = false;
        private string newVariablePrevious = string.Empty;
        DataProcessList dataProcessList = new DataProcessList();
        string ProcessingType;
        string ProcessingOption;
        private Dictionary<String, QuestionSettings> PopulatedDictionary = new Dictionary<string, QuestionSettings>();
        private ObservableCollection<SourceVariableList> sourceVariableList = new ObservableCollection<SourceVariableList>();
        private bool isRepeatVisible;
        string newVariableBeforeEdit;
        string questionTextBeforeEdit;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private int repeatCurrentDigit = 0;

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
        public bool IsRepeatVisible
        {
            get
            {
                return isRepeatVisible;
            }
            set
            {
                isRepeatVisible = value;
            }
        }

        #endregion
        public RECODE(Microsoft.Office.Interop.Excel.Workbook workbook, int readRow, int writeRow, string processingType, string processingOption, DataProcessList DPList = null, bool isRepeat = false)
        {
            Workbook = workbook;
            ReadRow = readRow;
            WriteRow = writeRow;
            isRepeatCase = isRepeat;
            dataProcessList = DPList;
            ProcessingType = processingType;
            ProcessingOption = processingOption;
            InitializeComponent();
            if (QC4Common.Common.Constants.GlobalMode.Split(',')[0] != "ja-JP")
                Button_Help.Visibility = Visibility.Hidden;
            else
                Button_Help.Visibility = Visibility.Visible;

        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
        }
        bool InitialLoad = true;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Save.IsEnabled = false;
            btn_summarizeChoices.IsEnabled = false;
            NewItemSearchbutton.IsEnabled = false;
            NewItemSearchbutton.Opacity = 0.5;
            lb_recode.Text = LocalResource.LBL_RECODE_DESCRIPTION;
            lb_command.Text = Util.Constants.ProcessingMethod.RECODE;
            int[] choices = new int[1001];
            choices = Enumerable.Range(0, 1001).ToArray();
            choicesList = Array.ConvertAll(choices, ele => ele.ToString());
            choicesList[0] = LocalResource.LBL_AUTO;
            choicesCount.ItemsSource = choicesList;
            PopulateMASAVariableList();
            ViewAllChoices();
            IsRepeatVisible = true;
            if (ProcessingOption == QC4Common.Common.Constants.STD_DP.Process_Edit || ProcessingOption == QC4Common.Common.Constants.STD_DP.Process_Copy)
            {
                PopulateRecodeData(Workbook, ReadRow, dataProcessList);
            }
            InitialLoad = false;
        }

        private void Button_Help_Click_1(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(QC4Common.Classes.Help.GetHelpLink(QC4Common.Common.Constants.HelpButtonType.RECODE));
        }

        private void MagnifyingGlassButton_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void NewItemSearchbutton_Click(object sender, RoutedEventArgs e)
        {
            if (MagnifyingGlassButton.VariableList != null)
            {
                txt_new_variable.Text = MagnifyingGlassButton.VariableList.Variable;
                if (ProcessingOption == QC4Common.Common.Constants.STD_DP.Process_Edit)
                {
                    btn_summarizeChoices.IsEnabled = true;
                    if (answerType.Text == "MA")
                    {
                        answerType.ItemsSource = MagnifyingGlassButton.VariableList.AswerTypes;
                        answerType.SelectedItem = "MA";
                        answerType.IsEnabled = false;
                    }
                    else if (answerType.Text == "SA")
                    {
                        answerType.ItemsSource = MagnifyingGlassButton.VariableList.AswerTypes;
                        answerType.SelectedItem = "SA";
                        answerType.IsEnabled = true;
                    }
                }

                Text_NewItem_Question.Text = (frmutil.UnEscapeCRLF(MagnifyingGlassButton.VariableList.Title) + " " + frmutil.UnEscapeCRLF(MagnifyingGlassButton.VariableList.Question)).TrimEnd().TrimStart();
                choicesCount.IsEnabled = true;
                choicesCount.IsEditable = true;
                choicesCount.SelectedIndex = MagnifyingGlassButton.VariableList.Choices.Count;
                for (int i = 0; i < choicesCount.SelectedIndex; i++)
                {
                    ChoiceListView[i].AllChoices = frmutil.EscapeCRLF(MagnifyingGlassButton.VariableList.Choices[i]);
                    ChoiceListView[i].SelectedOperator = null;
                    ChoiceListView[i].Criteria = null;
                }
                CollectionViewSource.GetDefaultView(data_grid.ItemsSource).Refresh();
            }
        }
        private void PopulateRecodeData(Microsoft.Office.Interop.Excel.Workbook workbook, int readRow, DataProcessList dpList)
        {
            Microsoft.Office.Interop.Excel.Worksheet dataProcessSheet = Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Qc4Launcher.Util.Constants.SheetCodeName.DataProcessS);
            Microsoft.Office.Interop.Excel.Range rowstart = dataProcessSheet.Cells[readRow, 1];
            Microsoft.Office.Interop.Excel.Range rowend = dataProcessSheet.Cells[readRow, ExcelAddIn.Common.Constants.DP.MAX_DP_COLUMN];
            Microsoft.Office.Interop.Excel.Range range_EDIT_OR_COPY_Process = dataProcessSheet.Range[rowstart, rowend];

            try
            {
                SourceVariableList EDIT_OR_COPY_Process_Details = new SourceVariableList();
                List<string> criterias = new List<string>();
                if (range_EDIT_OR_COPY_Process.Cells.Count > 1)
                {
                    QuestionSettings qs = new QuestionSettings();
                    QuestionSettings qs_RepeatVariable = new QuestionSettings();
                    var obj = range_EDIT_OR_COPY_Process.Value;
                    //EDIT OR COPY Process_Details.
                    if (obj[1, 3] != null)
                    {
                        if (obj[1, 11].Contains("[\\]"))
                        {

                            if (ProcessingOption == QC4Common.Common.Constants.STD_DP.Process_Copy)
                            {
                                txt_new_variable.Text = GetVariableNameWith_K(dpList.RepeatNewVariable, PopulatedDictionary.Values.ToList());
                                choicesCount.IsEditable = true;
                                choicesCount.IsEnabled = true;
                                answerType.IsEnabled = true;
                                btn_summarizeChoices.IsEnabled = true;
                            }
                            int digit = GetDigitFromSourceVariable(dpList.RepeatNewVariable);
                            string repeatVarible = obj[1, 11];

                            EDIT_OR_COPY_Process_Details.Variable = dpList.RepeatNewVariable;
                            SourceVariableList found = SourceVariableListView.FirstOrDefault(u => (u.Variable == (repeatVarible.Remove(repeatVarible.Length - 3) + digit)));

                            Combo_sourceVariable.SelectedIndex = SourceVariableListView.IndexOf(found);
                            checkBox_from_Nq1.IsChecked = true;
                            bool hasvalue = Util.Definiotion.VariableDictionary.TryGetValue(dpList.RepeatNewVariable, out qs);
                            if (qs != null)
                            {
                                for (int i = 1; i <= qs.Choices.Count; i++)
                                {
                                    criterias.Add(obj[1, 11 + i]);
                                }

                                newVariablePrevious = dpList.RepeatNewVariable;
                                answerType.SelectedValue = qs.AnswerType;
                                choicesCount.SelectedIndex = qs.Choices.Count;
                                for (int i = 0; i < qs.Choices.Count; i++)
                                {
                                    ChoiceListView[i].AllChoices = frmutil.EscapeCRLF(qs.Choices[i]);

                                    if (criterias[i].IndexOf("!") == 0)
                                    {
                                        ChoiceListView[i].SelectedOperator = "<>";
                                        ChoiceListView[i].Criteria = criterias[i].Remove(0, 1);
                                    }
                                    else
                                    {
                                        ChoiceListView[i].SelectedOperator = "=";
                                        ChoiceListView[i].Criteria = criterias[i];
                                    }

                                }
                                Text_NewItem_Question.Text = qs.Question;
                            }

                            if (dpList.Repeats.HasValue)
                            {
                                cmb_repeat.SelectedIndex = dpList.Repeats.Value - 1;
                            }

                            if (ProcessingOption == QC4Common.Common.Constants.STD_DP.Process_Edit)
                            {
                                Color newVariableTextboxColor = (Color)ColorConverter.ConvertFromString("#F0F0F0");
                                Color editProcessMethodColor = (Color)ColorConverter.ConvertFromString("#FFADADAD");
                                lb_recode.Background = new System.Windows.Media.SolidColorBrush(newVariableTextboxColor);
                                lb_recode.Foreground = new System.Windows.Media.SolidColorBrush(editProcessMethodColor);
                                txt_new_variable.Text = dpList.RepeatNewVariable;
                                choicesCount.IsEditable = false;
                                choicesCount.IsEnabled = false;
                                answerType.IsEnabled = false;
                                btn_summarizeChoices.IsEnabled = false;
                            }


                        }
                        else
                        {
                            if (ProcessingOption == QC4Common.Common.Constants.STD_DP.Process_Copy)
                            {
                                QC4Common.Util.QSUtil qsutil = new QC4Common.Util.QSUtil();
                                txt_new_variable.Text = qsutil.GetVariableName(dpList.NewVariable, PopulatedDictionary.Values.ToList());
                                choicesCount.IsEditable = true;
                                choicesCount.IsEnabled = true;
                                answerType.IsEnabled = true;
                                btn_summarizeChoices.IsEnabled = true;
                            }
                            EDIT_OR_COPY_Process_Details.Variable = obj[1, 11];
                            bool hasvalue = Util.Definiotion.VariableDictionary.TryGetValue(obj[1, ExcelAddIn.Common.Constants.DP.SubstituteVariableColumn], out qs);
                            if (qs != null)
                            {
                                for (int i = 1; i <= qs.Choices.Count; i++)
                                {
                                    criterias.Add(obj[1, 11 + i]);
                                }
                                newVariablePrevious = qs.Variable;
                                answerType.SelectedItem = qs.AnswerType;
                                choicesCount.SelectedIndex = qs.Choices.Count;
                                for (int i = 0; i < qs.Choices.Count; i++)
                                {
                                    ChoiceListView[i].AllChoices = frmutil.EscapeCRLF(qs.Choices[i]);

                                    if (criterias[i] != null)
                                    {
                                        if (criterias[i].IndexOf("!") == 0)
                                        {
                                            ChoiceListView[i].SelectedOperator = "<>";
                                            ChoiceListView[i].Criteria = criterias[i].Remove(0, 1);
                                        }
                                        else
                                        {
                                            ChoiceListView[i].SelectedOperator = "=";
                                            ChoiceListView[i].Criteria = criterias[i];
                                        }
                                    }
                                    else
                                    {
                                        ChoiceListView[i].SelectedOperator = null;
                                        ChoiceListView[i].Criteria = null;
                                    }

                                }
                            }
                            SourceVariableList found = SourceVariableListView.FirstOrDefault(u => (u.Variable == EDIT_OR_COPY_Process_Details.Variable));
                            Combo_sourceVariable.SelectedIndex = SourceVariableListView.IndexOf(found);


                            if (ProcessingOption == QC4Common.Common.Constants.STD_DP.Process_Edit)
                            {
                                Color newVariableTextboxColor = (Color)ColorConverter.ConvertFromString("#F0F0F0");
                                Color editProcessMethodColor = (Color)ColorConverter.ConvertFromString("#FFADADAD");
                                lb_recode.Background = new System.Windows.Media.SolidColorBrush(newVariableTextboxColor);
                                lb_recode.Foreground = new System.Windows.Media.SolidColorBrush(editProcessMethodColor);
                                if (qs != null)
                                {
                                    txt_new_variable.Text = qs.Variable;
                                    answerType.SelectedItem = qs.AnswerType;
                                }
                                else
                                {
                                    txt_new_variable.Text = string.Empty;
                                }
                                choicesCount.IsEditable = false;
                                choicesCount.IsEnabled = false;
                                answerType.IsEnabled = false;
                                btn_summarizeChoices.IsEnabled = false;
                            }
                            if (ProcessingOption == QC4Common.Common.Constants.STD_DP.Process_Copy)
                            {
                                if (qs != null)
                                {
                                    answerType.SelectedItem = qs.AnswerType;
                                    QC4Common.Util.QSUtil qsutil = new QC4Common.Util.QSUtil();
                                    txt_new_variable.Text = qsutil.GetVariableName(dpList.NewVariable, PopulatedDictionary.Values.ToList());
                                }
                            }
                            if (qs != null)
                            {
                                Text_NewItem_Question.Text = qs.Question;
                            }
                            else
                            {

                                Text_NewItem_Question.Text = string.Empty;
                                txt_new_variable.Text = string.Empty;
                            }

                        }


                    }
                    // assign to temp variable
                    newVariableBeforeEdit = txt_new_variable.Text;

                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

        }
        private void PopulateMASAVariableList()
        {
            var SettingSheet = ExcelUtil.GetWorkSheetByCodeName(Workbook, "Sheet91");
            PopulatedDictionary = Util.Definiotion.VariableDictionary;

            if (SettingSheet != null)
            {
                Microsoft.Office.Interop.Excel.Range Range = SettingSheet.get_Range("List_Item_SAMA");
                if (Range != null)
                {
                    MASAvariables.Clear();

                    if (Range.Cells.Count == 1)
                    {
                        MASAvariables.Add(Range.Value);
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
                                    Title = frmutil.EscapeCRLF(qs.TableHeading),
                                    Choices = qs.Choices
                                });

                            }
                        }
                    }
                    else if (Range.Cells.Count > 1)
                    {
                        var objAry = Range.Value;
                        int max = objAry.GetLength(0);
                        for (int i = 1; i <= max; i++)
                        {
                            if (objAry[i, 1] != null)
                            {

                                MASAvariables.Add(objAry[i, 1].ToString());
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
                                            Title = frmutil.EscapeCRLF(qs.TableHeading),
                                            Choices = qs.Choices
                                        });

                                    }
                                }
                            }
                        }
                    }
                }
            }


        }
        public class SourceVariableList
        {
            private string variable;
            private string answertype;
            private string question;
            private string title;
            private bool isRepeatEnabled;
            private List<string> choices;
            private List<string> aswerTypes;

            public string Variable
            {
                get
                {
                    if (IsRepeatEnabled)
                    {
                        variable = currentSourceVariable;

                    }
                    return variable;
                }
                set
                {
                    variable = value;
                }
            }
            public bool IsRepeatEnabled
            {
                get
                {
                    return isRepeatEnabled;
                }
                set
                {
                    isRepeatEnabled = value;
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
            public string Title
            {
                get
                {
                    return title;
                }
                set
                {
                    title = value;
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
            public List<string> AswerTypes
            {
                get
                {
                    return aswerTypes;
                }
                set
                {
                    aswerTypes = value;
                }
            }
        }

        private void Combo_sourceVariable_DropDownClosed(object sender, EventArgs e)
        {
            System.Windows.Controls.ComboBox sen = ((System.Windows.Controls.ComboBox)sender);
            if (sen.SelectedItem != null)
            {
                var comboItem = sen.SelectedItem;
                int index = sen.Items.IndexOf(comboItem);
                sen.SelectedIndex = index;
            }
            if (isRepeatEnabled)
            {
                Combo_sourceVariable.SelectedItem = SourceVariableListView.FirstOrDefault(u => u.Variable == currentSourceVariable);
            }


        }


        private void SearchExistingSourceVariables(int digit, SourceVariableList sourceList)
        {

            foreach (var item in sourceVariableList)
            {
                if (repeatCurrentDigit == 0 || digit == repeatCurrentDigit)
                {
                    int itemDigit = GetDigitFromSourceVariable(item.Variable);
                    if ((itemDigit == (digit + 1)) && (item.AnswerType == sourceList.AnswerType) && (item.Choices.Count == sourceList.Choices.Count))
                    {
                       // Regex regex = new Regex(@"^(.*)(\d+)");
                       // Match match = regex.Match(item.Variable);
                        string svar = GetVariablenameWithoutLastDigitsFromSourceVariable(sourceList.Variable);
                        string tvar = GetVariablenameWithoutLastDigitsFromSourceVariable(item.Variable); 
                      /*  if (match.Success)
                        {
                            tvar = (match.Groups[1].Value);
                        }*/
                      //  match = regex.Match(sourceList.Variable);
                       /* if (match.Success)
                        {
                            svar = (match.Groups[1].Value);
                        }*/
                        if (item.Choices.SequenceEqual(sourceList.Choices) && svar.Equals(tvar))
                        {
                            repeatsList.Add(item);
                            repeatCurrentDigit = itemDigit;
                            SearchExistingSourceVariables(itemDigit, item as SourceVariableList);
                        }
                    }
                }
            }
        }

        private void ChoicesCount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            ObservableCollection<ChoiceList> cList = new ObservableCollection<ChoiceList>();

            var found = ChoiceListView.LastOrDefault(u => (u.AllChoices != null) || (u.SelectedOperator != null) || (u.Criteria != null));
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
                            AllChoices = null,
                            Choices = null,
                            Criteria = null,
                            SelectedOperator = null

                        });
                    }
                }
            }

            data_grid.ItemsSource = ChoiceListView;

        }

        private void SummarizeChoices_Click(object sender, RoutedEventArgs e)
        {
            List<string> choices = new List<string>();
            try
            {
                if (choices_grid.ItemsSource != null)
                {
                    DataTable choiceDt = ((DataView)choices_grid.ItemsSource).ToTable();
                    choiceDt.DefaultView.ToTable(false, "Choice");
                    SummarizeChoices summarizeChoices = new SummarizeChoices(choiceDt);
                    summarizeChoices.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                    summarizeChoices.Owner = this;
                    summarizeChoices.ShowDialog();
                    summarizeChoices.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    var x = summarizeChoices.SummarizedChoice;
                    if (summarizeChoices.SummarizedChoice.IsOk)
                    {
                        ObservableCollection<ChoiceList> summarizedChoiceListEnd = new ObservableCollection<ChoiceList>();
                        ObservableCollection<ChoiceList> summarizedChoiceListStart = new ObservableCollection<ChoiceList>();
                        ObservableCollection<ChoiceList> summarizedChoiceListSource = new ObservableCollection<ChoiceList>();
                        bool isTopStart = false;
                        bool isBottomStart = false;
                        var found = ChoiceListView.LastOrDefault(u => (u.AllChoices != null) || (u.SelectedOperator != null) || (u.Criteria != null));
                        int foundIndex = ChoiceListView.IndexOf(found);
                        var foundNotAuto = ChoiceListView.LastOrDefault(u => (u.AllChoices != null) || (u.SelectedOperator != null) || (u.Criteria != null));
                        int foundNotAutoIndex = ChoiceListView.IndexOf(foundNotAuto);


                        if (summarizeChoices.SummarizedChoice.IsTop)
                        {
                            int result = 0;
                            bool isInteger = int.TryParse(summarizeChoices.SummarizedChoice.TopSelectedChoice, out result);
                            if (result == 1)
                            {
                                if (summarizeChoices.SummarizedChoice.TopSelectedPosition == LocalResource.LBL_START)
                                {
                                    isTopStart = true;
                                    summarizedChoiceListStart.Add(new ChoiceList()
                                    {
                                        AllChoices = LocalResource.LBL_TOP + summarizeChoices.SummarizedChoice.TopSelectedChoice.ToString(),
                                        Criteria = "1",
                                        SelectedOperator = "="
                                    });
                                }
                                else
                                {
                                    summarizedChoiceListEnd.Add(new ChoiceList()
                                    {
                                        AllChoices = LocalResource.LBL_TOP + summarizeChoices.SummarizedChoice.TopSelectedChoice.ToString(),
                                        Criteria = "1",
                                        SelectedOperator = "="
                                    });
                                }
                            }
                            else
                            {
                                if (summarizeChoices.SummarizedChoice.TopSelectedPosition == LocalResource.LBL_START)
                                {
                                    isTopStart = true;
                                    summarizedChoiceListStart.Add(new ChoiceList()
                                    {
                                        AllChoices = LocalResource.LBL_TOP + summarizeChoices.SummarizedChoice.TopSelectedChoice.ToString(),
                                        Criteria = "1" + "-" + summarizeChoices.SummarizedChoice.TopSelectedChoice.ToString(),
                                        SelectedOperator = "="
                                    });
                                }
                                else
                                {
                                    summarizedChoiceListEnd.Add(new ChoiceList()
                                    {
                                        AllChoices = LocalResource.LBL_TOP + summarizeChoices.SummarizedChoice.TopSelectedChoice.ToString(),
                                        Criteria = "1" + "-" + summarizeChoices.SummarizedChoice.TopSelectedChoice.ToString(),
                                        SelectedOperator = "="
                                    });
                                }

                            }

                        }
                        if (summarizeChoices.SummarizedChoice.IsBottom)
                        {

                            int result = 0;
                            bool isInteger = int.TryParse(summarizeChoices.SummarizedChoice.BottomSelectedChoice, out result);
                            if (result == 1)
                            {
                                if (summarizeChoices.SummarizedChoice.BottomSelectedPosition == LocalResource.LBL_START)
                                {
                                    isBottomStart = true;
                                    summarizedChoiceListStart.Add(new ChoiceList()
                                    {
                                        AllChoices = LocalResource.LBL_BOTTOM + summarizeChoices.SummarizedChoice.BottomSelectedChoice.ToString(),
                                        Criteria = choices_grid.Items.Count.ToString(),
                                        SelectedOperator = "="
                                    });
                                }
                                else
                                {
                                    summarizedChoiceListEnd.Add(new ChoiceList()
                                    {
                                        AllChoices = LocalResource.LBL_BOTTOM + summarizeChoices.SummarizedChoice.BottomSelectedChoice.ToString(),
                                        Criteria = choices_grid.Items.Count.ToString(),
                                        SelectedOperator = "="
                                    });
                                }

                            }
                            else
                            {
                                if (summarizeChoices.SummarizedChoice.BottomSelectedPosition == LocalResource.LBL_START)
                                {
                                    isBottomStart = true;
                                    summarizedChoiceListStart.Add(new ChoiceList()
                                    {
                                        AllChoices = LocalResource.LBL_BOTTOM + summarizeChoices.SummarizedChoice.BottomSelectedChoice.ToString(),
                                        Criteria = (choices_grid.Items.Count - result + 1).ToString() + "-" + choices_grid.Items.Count.ToString(),
                                        SelectedOperator = "="
                                    });
                                }
                                else
                                {
                                    summarizedChoiceListEnd.Add(new ChoiceList()
                                    {
                                        AllChoices = LocalResource.LBL_BOTTOM + summarizeChoices.SummarizedChoice.BottomSelectedChoice.ToString(),
                                        Criteria = (choices_grid.Items.Count - result + 1).ToString() + "-" + choices_grid.Items.Count.ToString(),
                                        SelectedOperator = "="
                                    });
                                }

                            }
                        }


                        if (!summarizeChoices.SummarizedChoice.IsIncludeSourceChoices)
                        {
                            AddSummarizedChoices(found, summarizedChoiceListStart, summarizedChoiceListEnd, summarizedChoiceListSource, isTopStart, isBottomStart, summarizeChoices.SummarizedChoice.IsIncludeSourceChoices);
                        }
                        else
                        {
                            foreach (var item in choices_grid.Items)
                            {
                                System.Data.DataRowView selectedFile = (System.Data.DataRowView)item;
                                string selectedIndex = (Convert.ToString(selectedFile.Row.ItemArray[0]));
                                int index = int.Parse(selectedIndex);
                                string str = Convert.ToString(selectedFile.Row.ItemArray[1]);
                                summarizedChoiceListSource.Add(new ChoiceList()
                                {
                                    Id = index,
                                    AllChoices = str,
                                    Criteria = selectedIndex,
                                    SelectedOperator = "="
                                });
                            }
                            if (choicesCount.Text == LocalResource.LBL_AUTO)
                            {
                                if (found != null)
                                {
                                    MessageBoxResult result;
                                    result = MessageBox.Show(String.Format(LocalResource.ALERT_CLEAR_SETTING_FOR_CHOICES), "QuickCross", MessageBoxButton.YesNo, MessageBoxImage.Question);
                                    if (result != MessageBoxResult.Yes)
                                    {
                                        return;
                                    }
                                    else
                                    {
                                        for (int i = 0; i <= foundIndex; i++)
                                        {
                                            ChoiceListView[i].AllChoices = null;
                                            ChoiceListView[i].SelectedOperator = null;
                                            ChoiceListView[i].Criteria = null;
                                        }
                                        AddSummarizedChoices(found, summarizedChoiceListStart, summarizedChoiceListEnd, summarizedChoiceListSource, isTopStart, isBottomStart, summarizeChoices.SummarizedChoice.IsIncludeSourceChoices);

                                    }

                                }
                                else
                                {
                                    AddSummarizedChoices(found, summarizedChoiceListStart, summarizedChoiceListEnd, summarizedChoiceListSource, isTopStart, isBottomStart, summarizeChoices.SummarizedChoice.IsIncludeSourceChoices);

                                }
                            }
                            else
                            {
                                if (foundNotAuto != null)
                                {
                                    MessageBoxResult result;
                                    result = MessageBox.Show(String.Format(LocalResource.ALERT_CLEAR_SETTING_FOR_CHOICES), "QuickCross", MessageBoxButton.YesNo, MessageBoxImage.Question);
                                    if (result != MessageBoxResult.Yes)
                                    {
                                        return;
                                    }
                                    else
                                    {
                                        for (int i = 0; i <= foundIndex; i++)
                                        {
                                            ChoiceListView[i].AllChoices = "";
                                            ChoiceListView[i].SelectedOperator = "";
                                            ChoiceListView[i].Criteria = "";
                                        }
                                        AddSummarizedChoices(found, summarizedChoiceListStart, summarizedChoiceListEnd, summarizedChoiceListSource, isTopStart, isBottomStart, summarizeChoices.SummarizedChoice.IsIncludeSourceChoices);

                                    }

                                }
                                else
                                {
                                    AddSummarizedChoices(found, summarizedChoiceListStart, summarizedChoiceListEnd, summarizedChoiceListSource, isTopStart, isBottomStart, summarizeChoices.SummarizedChoice.IsIncludeSourceChoices);

                                }
                            }


                        }
                        data_grid.ItemsSource = ChoiceListView;
                        CollectionViewSource.GetDefaultView(data_grid.ItemsSource).Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                data_grid.SelectedIndex = -1;
                try { txt_new_variable.Text = txt_new_variable.Text.TrimStart().TrimEnd(); } catch (Exception ex) { _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException); }
                string newvariable = txt_new_variable.Text;
                var dataSet = new DataSet();
                var dataTable = new DataTable();
                dataSet.Tables.Add(dataTable);

                dataTable.Columns.Add("Index");
                dataTable.Columns.Add("Choice");
                dataTable.Columns.Add("Operator");
                dataTable.Columns.Add("Criteria");

                if (ChoiceListView.Count <= 1000 && choicesCount.SelectedIndex != 0)
                {

                    bool isAllEmpty = false;
                    bool isOverlappedCriteria = false;
                    bool isNewVariableAndOnlyUpdationNeeded = false;
                    isNewQuestion = true;
                    int AllEmptyCount = 0;
                    var found = ChoiceListView.LastOrDefault(u => (!string.IsNullOrEmpty(u.AllChoices)) || (!string.IsNullOrEmpty(u.SelectedOperator)) || (!string.IsNullOrEmpty(u.Criteria)));
                    int foundIndex = ChoiceListView.IndexOf(found);
                    if ((found == null && choicesCount.SelectedIndex != 0))
                    {
                        MessageDialog.ErrorOk(LocalResource.ADD_DP_RECODE_ERROR_MSG_NO_MATCH_NUMBER_OF_CHOICES);
                        return;
                    }
                    else if (found != null && choicesCount.SelectedIndex != 0)
                    {
                        AllEmptyCount = 0;
                        foreach (var element in data_grid.ItemsSource)
                        {
                            if ((element as ChoiceList).AllChoices == null || (element as ChoiceList).AllChoices.TrimEnd().TrimStart() == "")
                            {
                                AllEmptyCount++;
                            }
                        }
                        if (AllEmptyCount == choicesCount.SelectedIndex)
                        {
                            MessageDialog.ErrorOk(LocalResource.ADD_DP_RECODE_ERROR_MSG_NO_MATCH_NUMBER_OF_CHOICES);
                            return;
                        }
                    }
                    if (foundIndex + 1 < choicesCount.SelectedIndex)
                    {
                        MessageDialog.ErrorOk(LocalResource.ADD_DP_RECODE_ERROR_MSG_NO_MATCH_NUMBER_OF_CHOICES);
                        return;
                    }

                    if ((ProcessingOption != QC4Common.Common.Constants.STD_DP.Process_Edit) && (string.IsNullOrEmpty(txt_new_variable.Text) || !frmutil.IsVariableNameExists(txt_new_variable.Text, PopulatedDictionary.Values.ToList(), 1)))
                    {

                        string errormsg = LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_NEW_ITEM_EMPTY + "\n" + LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_RENAME_NEW_VARIABLE_AUTOMATICALLY;
                        if (!string.IsNullOrEmpty(txt_new_variable.Text))
                        {
                            errormsg = LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_ITEM_NAME_ALREADY_EXISTS + "\n" + LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_RENAME_NEW_VARIABLE_AUTOMATICALLY;
                        }
                        System.Windows.Forms.DialogResult result;
                        result = MessageDialog.InfoYesNo(errormsg);
                        if (result == System.Windows.Forms.DialogResult.Yes)
                        {
                            string variablename = txt_new_variable.Text;
                            if (string.IsNullOrEmpty(txt_new_variable.Text))
                            {
                                //get new variable name
                                QC4Common.Util.QSUtil qsutil = new QC4Common.Util.QSUtil();
                                newvariable = qsutil.GetVariableName(Combo_sourceVariable.Text, PopulatedDictionary.Values.ToList());
                                txt_new_variable.Text = newvariable;
                                isNewQuestion = true;
                                isUpdateQuestion = false;
                            }
                            else
                            {
                                //get new variable name
                                QC4Common.Util.QSUtil qsutil = new QC4Common.Util.QSUtil();
                                newvariable = qsutil.GetVariableName(variablename, PopulatedDictionary.Values.ToList());
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

                    if (ProcessingOption == QC4Common.Common.Constants.STD_DP.Process_Edit)
                    {
                        isNewQuestion = false;
                        isUpdateQuestion = true;
                    }

                    if (ProcessingOption == QC4Common.Common.Constants.STD_DP.Process_Edit && (string.IsNullOrEmpty(txt_new_variable.Text)))
                    {
                        string errormsg = LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_NEW_ITEM_EMPTY + "\n" + LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_RENAME_NEW_VARIABLE_AUTOMATICALLY;
                        System.Windows.Forms.DialogResult result;
                        result = MessageDialog.InfoYesNo(errormsg);
                        if (result == System.Windows.Forms.DialogResult.Yes)
                        {
                            //get new variable name
                            QC4Common.Util.QSUtil qsutil = new QC4Common.Util.QSUtil();
                            newvariable = qsutil.GetVariableName(Combo_sourceVariable.Text, PopulatedDictionary.Values.ToList());
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
                    else if (isAllEmpty)
                    {
                        MessageDialog.ErrorOk(LocalResource.ADD_DP_RECODE_ERROR_MSG_NO_CHOICE_SET);
                        return;
                    }

                    else
                    {
                        foreach (var element in data_grid.ItemsSource)
                        {
                            DataRow newRow = dataTable.NewRow();
                            int result = 0;
                            bool isInteger = false;
                            bool isInvalidCharacter = false;
                            bool isAdjusentItemsPresent = false;
                            bool isNotEqualToSatrt = false;
                            bool isOnlyNotEqual = false;
                            bool isNotEqualToSomewhere = false;
                            bool isInvalidChoicenumber = false;
                            bool isMultipleHiphen = false;
                            bool ishiphen = false;
                            bool isIncorrectRange = false;
                            char[] spliters = new char[] { '-', '/', ',' };

                            if ((element as ChoiceList).Criteria != null && (element as ChoiceList).Criteria.TrimEnd().TrimStart() != "")
                            {
                                string[] splittedValues = (element as ChoiceList).Criteria.Split(spliters);
                                int splittedValuesresult = 0;
                                string criteriaValue = (element as ChoiceList).Criteria;

                                for (int i = 0; i < splittedValues.Length; i++)
                                {
                                    if ((element as ChoiceList).Criteria.Count(x => x == '-') > 1)
                                    {
                                        if ((element as ChoiceList).Criteria.Contains(",") || (element as ChoiceList).Criteria.Contains("/"))
                                        {
                                            char[] split = new char[] { '/', ',' };
                                            string[] Values = (element as ChoiceList).Criteria.Split(split);
                                            for (int j = 0; j < Values.Length; j++)
                                            {
                                                if (Values[j].Count(x => x == '-') > 1)
                                                {
                                                    isMultipleHiphen = true;
                                                    break;
                                                }
                                                else
                                                {
                                                    isMultipleHiphen = false;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            isMultipleHiphen = true;
                                            break;
                                        }
                                    }
                                    if (CheckSeparator((element as ChoiceList).Criteria))
                                    {
                                        isAdjusentItemsPresent = true;
                                        break;

                                    }

                                    if (criteriaValue[criteriaValue.Length - 1] == ',' || criteriaValue[criteriaValue.Length - 1] == '/')
                                    {
                                        isAdjusentItemsPresent = true;
                                        break;
                                    }
                                    if (criteriaValue[0] == ',' || criteriaValue[0] == '/')
                                    {
                                        isAdjusentItemsPresent = true;
                                        break;
                                    }
                                    if (criteriaValue[criteriaValue.Length - 1] == '-')
                                    {
                                        if (criteriaValue.Length > 1)
                                        {
                                            if (criteriaValue[criteriaValue.Length - 2] == '/' || criteriaValue[criteriaValue.Length - 2] == ',')
                                            {
                                                isIncorrectRange = true;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            isIncorrectRange = true;
                                            break;
                                        }
                                    }
                                    if (splittedValues[i].Contains("!"))
                                    {
                                        if ((element as ChoiceList).Criteria == "!")
                                        {
                                            isOnlyNotEqual = true;
                                            break;
                                        }
                                        if (i == 0 && (splittedValues[i].IndexOf("!") == 0))
                                        {
                                            isNotEqualToSatrt = true;
                                            break;
                                        }
                                        else if (i == 0 && (splittedValues[i].IndexOf("!") != 0))
                                        {
                                            isNotEqualToSomewhere = true;
                                            break;
                                        }
                                        else if (i != 0 && (splittedValues[i].Contains("!")))
                                        {
                                            isNotEqualToSomewhere = true;
                                            break;
                                        }
                                    }
                                    if (!int.TryParse(splittedValues[i], out splittedValuesresult))
                                    {
                                        if (splittedValues[i] != "")
                                        {
                                            isInvalidCharacter = true;

                                            string criteria = (element as ChoiceList).Criteria;
                                            if (criteria.ToLower().IndexOf('-') == criteria.Length - 1)
                                            {
                                                ishiphen = true;
                                                isInvalidCharacter = false;
                                            }
                                            if (splittedValues[i].Contains("."))
                                            {
                                                isInvalidCharacter = true;
                                                ishiphen = false;
                                                break;
                                            }
                                        }

                                    }
                                    else
                                    {
                                        if (splittedValues[i] == "0")
                                        {
                                            isInvalidChoicenumber = true;
                                            break;
                                        }
                                    }

                                    if ((!(splittedValuesresult <= choices_grid.Items.Count)))
                                    {
                                        if ((splittedValuesresult >= 0))
                                        {
                                            isInvalidChoicenumber = true;
                                            break;
                                        }

                                    }
                                }
                            }
                            else
                            {

                            }

                            isInteger = int.TryParse((element as ChoiceList).Criteria, out result);

                            // Check data entered
                            if ((element as ChoiceList).AllChoices == null || (element as ChoiceList).AllChoices.TrimEnd().TrimStart() == "" || (element as ChoiceList).SelectedOperator == null || (element as ChoiceList).Criteria == null || (element as ChoiceList).Criteria.TrimEnd().TrimStart() == "")
                            {

                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, (dataTable.Rows.Count + 1)) + "\n" + (LocalResource.ERR_MSG_RECODE_A_BLANK_IS_SET));
                                if ((element as ChoiceList).AllChoices == null || (element as ChoiceList).AllChoices.TrimEnd().TrimStart() == "")
                                {
                                    SetInvalidBGChoice(dataTable.Rows.Count);
                                }
                                else if ((element as ChoiceList).SelectedOperator == null)
                                {
                                    SetInvalidBGOperator(dataTable.Rows.Count);
                                }
                                else if ((element as ChoiceList).Criteria == null || (element as ChoiceList).Criteria.TrimStart().TrimEnd() == "")
                                {
                                    SetInvalidBGCriteria(dataTable.Rows.Count);
                                }

                                break;
                            }
                            else if ((element as ChoiceList).SelectedOperator != "=" && (element as ChoiceList).SelectedOperator != "<>")
                            {
                                MessageDialog.ErrorOk(LocalResource.ERR_MSG_RECODE_OPERATOR_SELECTION_IS_INVALID);
                                SetInvalidBGOperator(dataTable.Rows.Count);
                                break;

                            }
                            else if (isMultipleHiphen)
                            {
                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, (dataTable.Rows.Count + 1)) + "\n" + (LocalResource.ERR_MSG_RECODE_MULTIPLE_HYPHEN_ARE_SET));
                                SetInvalidColor(dataTable.Rows.Count);
                                break;
                            }
                            else if (isAdjusentItemsPresent)
                            {
                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, (dataTable.Rows.Count + 1)) + "\n" + (LocalResource.ERR_MSG_RECODE_A_BLANK_IS_SET));
                                SetInvalidColor(dataTable.Rows.Count);
                                break;
                            }
                            else if (isIncorrectRange)
                            {
                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, (dataTable.Rows.Count + 1)) + "\n" + (LocalResource.ERR_MSG_RECODE_INCORRECT_RANGE_SPECIFICATION));
                                SetInvalidColor(dataTable.Rows.Count);
                                break;
                            }
                            else if (isOnlyNotEqual)
                            {
                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, (dataTable.Rows.Count + 1)) + "\n" + (LocalResource.ERR_MSG_RECODE_CANNOT_SET_ALONE));
                                SetInvalidColor(dataTable.Rows.Count);
                                break;
                            }
                            else if (isNotEqualToSatrt)
                            {
                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, (dataTable.Rows.Count + 1)) + "\n" + (LocalResource.ERR_MSG_RECODE_INCLUDED_IN_THE_CRITERIA));
                                SetInvalidColor(dataTable.Rows.Count);
                                break;
                            }
                            else if (isNotEqualToSomewhere)
                            {
                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, (dataTable.Rows.Count + 1)) + "\n" + (LocalResource.ERR_MSG_RECODE_CANNOT_SET_ELSEWHERE_THAN_AT_START));
                                SetInvalidColor(dataTable.Rows.Count);
                                break;
                            }

                            else if (isInvalidChoicenumber)
                            {
                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, (dataTable.Rows.Count + 1)) + "\n" + string.Format(LocalResource.ERR_MSG_RECODE_SET_AN_INTEGER_IN_THE_RANGE_OF, "1", (choices_grid.Items.Count).ToString()));
                                SetInvalidColor(dataTable.Rows.Count);
                                break;
                            }
                            else if ((!isInteger && isInvalidCharacter) && !ishiphen)
                            {
                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, (dataTable.Rows.Count + 1)) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_SET_A_NUMERIC_VALUE));
                                SetInvalidColor(dataTable.Rows.Count);
                                break;
                            }

                            else if (isInteger && !(result <= choices_grid.Items.Count))
                            {
                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, (dataTable.Rows.Count + 1)) + "\n" + string.Format(LocalResource.ERR_MSG_RECODE_SET_AN_INTEGER_IN_THE_RANGE_OF, "1", (choices_grid.Items.Count).ToString()));
                                SetInvalidColor(dataTable.Rows.Count);
                                break;

                            }

                            else
                            {
                                newRow["Index"] = (element as ChoiceList).Id;
                                newRow["Choice"] = (element as ChoiceList).AllChoices;
                                newRow["Operator"] = (element as ChoiceList).SelectedOperator;
                                newRow["Criteria"] = (element as ChoiceList).Criteria;

                                dataTable.Rows.Add(newRow);
                            }
                        }
                        if (dataTable.Rows.Count == ChoiceListView.Count)
                        {
                            string[] dt_Choices_columns = new string[2];
                            dt_Choices_columns[0] = "Index";
                            dt_Choices_columns[1] = "Choice";
                            string[] dt_Choices_columns2 = new string[2];
                            dt_Choices_columns2[0] = "Operator";
                            dt_Choices_columns2[1] = "Criteria";
                            DataTable dtCriteria = dataTable.DefaultView.ToTable(false, dt_Choices_columns2);
                            DataTable dtChoice = dataTable.DefaultView.ToTable(false, dt_Choices_columns);
                            string[] paramList = new string[dtCriteria.Rows.Count + 1];
                            paramList[0] = Combo_sourceVariable.Text;

                            DataProcessHelper dataProcessHelper = new DataProcessHelper();
                            string command = QC4Common.Common.Constants.DP.SubstituteOperatorRECODE;

                            for (int i = 1; i <= dtChoice.Rows.Count; i++)
                            {
                                if (dtChoice.Rows[i - 1][1].ToString().Length > QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit)
                                {
                                    dtChoice.Rows[i - 1][1] = dtChoice.Rows[i - 1][1].ToString().PadRight(QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit).Substring(0, QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit);
                                }
                            }

                            for (int i = 1; i <= dtCriteria.Rows.Count; i++)
                            {
                                if (dtCriteria.Rows[i - 1][0].ToString() == "=")
                                {
                                    paramList[i] = dtCriteria.Rows[i - 1][1].ToString();
                                }
                                else if (dtCriteria.Rows[i - 1][0].ToString() == "<>")
                                {
                                    paramList[i] = "!" + dtCriteria.Rows[i - 1][1].ToString();
                                }

                            }

                            // Shows error message if variable length exceeds it's maximum length
                            if (frmutil.IsVariableLengthExceeds(txt_new_variable.Text))
                            {
                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_QUESTION_LENGTH_EXCEED, QC4Common.Common.Constants.QsVariableMaxLength));
                                return;
                            }


                            if (paramList.Length > 2 && answerType.SelectedValue.ToString() == "SA")
                            {
                                if (dataTable.Rows.Count > 1)
                                {
                                    isOverlappedCriteria = CheckOverlappedCriteria(dtCriteria);
                                }
                                if (isOverlappedCriteria)
                                {
                                    if (ProcessingOption != QC4Common.Common.Constants.STD_DP.Process_Edit)
                                    {
                                        MessageBoxResult result;
                                        result = MessageBox.Show(LocalResource.RECODE_OVERLAPPING_CHOICE_WARNING_MESSAGE, LocalResource.TITLE_QC, MessageBoxButton.YesNo, MessageBoxImage.Warning);
                                        if (result == MessageBoxResult.No)
                                        {
                                            return;
                                        }
                                        else
                                        {
                                            answerType.SelectedItem = "MA";
                                        }
                                    }
                                    else if (newVariableBeforeEdit != txt_new_variable.Text)
                                    {
                                        MessageBoxResult result;
                                        result = MessageBox.Show(LocalResource.RECODE_OVERLAPPING_CHOICE_WARNING_MESSAGE, LocalResource.TITLE_QC, MessageBoxButton.YesNo, MessageBoxImage.Warning);
                                        if (result == MessageBoxResult.No)
                                        {
                                            return;
                                        }
                                        else
                                        {
                                            answerType.SelectedItem = "MA";
                                        }

                                    }

                                }

                            }
                            if ((Util.Definiotion.VariableDictionary.ContainsKey(newvariable)) && (
                    (Util.Definiotion.VariableDictionary[newvariable].AnswerType != answerType.Text) ||
                    (Util.Definiotion.VariableDictionary[newvariable].CategoryCount != choicesCount.SelectedIndex)))
                            {
                                System.Windows.Forms.DialogResult result;
                                result = MessageDialog.InfoYesNo(LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_ITEM_NAME_ALREADY_EXISTS + "\n" + LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_RENAME_NEW_VARIABLE_AUTOMATICALLY);
                                if (result == System.Windows.Forms.DialogResult.Yes)
                                {
                                    //get new variable name
                                    QC4Common.Util.QSUtil qsutil = new QC4Common.Util.QSUtil();
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
                                (Util.Definiotion.VariableDictionary[newvariable].CategoryCount == choicesCount.SelectedIndex)
                                )
                            {
                                isUpdateQuestion = true;
                                isNewQuestion = false;
                                isNewVariableAndOnlyUpdationNeeded = true;
                            }

                            //Check the newVariable name is valid or not
                            if (ProcessingOption == QC4Common.Common.Constants.STD_DP.Process_Edit)
                            {
                                if (newVariableBeforeEdit != txt_new_variable.Text)
                                {
                                    if (!(Util.Definiotion.VariableDictionary.ContainsKey(txt_new_variable.Text)))
                                    {
                                        isUpdateQuestion = false;
                                        isNewQuestion = true;
                                    }

                                    if ((string.IsNullOrEmpty(txt_new_variable.Text)) || !frmutil.IsVariableNameExists(txt_new_variable.Text, PopulatedDictionary.Values.ToList(), 1))
                                    {

                                        string errormsg = LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_NEW_ITEM_EMPTY + "\n" + LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_RENAME_NEW_VARIABLE_AUTOMATICALLY;
                                        if (!string.IsNullOrEmpty(txt_new_variable.Text))
                                        {
                                            errormsg = LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_ITEM_NAME_ALREADY_EXISTS + "\n" + LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_RENAME_NEW_VARIABLE_AUTOMATICALLY;
                                        }
                                        System.Windows.Forms.DialogResult result;
                                        result = MessageDialog.InfoYesNo(errormsg);
                                        if (result == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            string variablename = txt_new_variable.Text;
                                            if (string.IsNullOrEmpty(txt_new_variable.Text))
                                            {
                                                //get new variable name
                                                QC4Common.Util.QSUtil qsutil = new QC4Common.Util.QSUtil();
                                                txt_new_variable.Text = qsutil.GetVariableName(Combo_sourceVariable.Text, PopulatedDictionary.Values.ToList());
                                                isNewQuestion = true;
                                                isUpdateQuestion = false;
                                            }
                                            else
                                            {
                                                //get new variable name
                                                QC4Common.Util.QSUtil qsutil = new QC4Common.Util.QSUtil();
                                                txt_new_variable.Text = qsutil.GetVariableName(variablename, PopulatedDictionary.Values.ToList());
                                                isNewQuestion = true;
                                                isUpdateQuestion = false;
                                            }


                                        }
                                        else if (result == System.Windows.Forms.DialogResult.No)
                                        {
                                            return;
                                        }

                                        Util.QS.NewQuestionValidation validation = new Util.QS.NewQuestionValidation(txt_new_variable.Text);
                                        if (!isNewVariableAndOnlyUpdationNeeded && !validation.Validation_Variable())
                                        {
                                            return;
                                        }

                                    }
                                    else
                                    {
                                        Util.QS.NewQuestionValidation validation = new Util.QS.NewQuestionValidation(txt_new_variable.Text.TrimEnd().TrimStart());
                                        if (!isNewVariableAndOnlyUpdationNeeded && !validation.Validation_Variable())
                                        {
                                            return;
                                        }
                                        if (checkBox_from_Nq1.IsChecked == true)
                                        {
                                            isUpdateQuestion = false;
                                            isNewQuestion = true;
                                        }

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
                                Util.QS.NewQuestionValidation validation = new Util.QS.NewQuestionValidation(txt_new_variable.Text);
                                if (!isNewVariableAndOnlyUpdationNeeded && !validation.Validation_Variable())
                                {
                                    return;
                                }
                            }

                            /// Save settings.
                            Logic.DataProcessHelper dphelper = new Logic.DataProcessHelper();
                            int columncount = 8 + (dtCriteria.Rows.Count + 1); // total number of columns(column upto operator + all parameters)
                            string[,] dpsaveinstructios;
                            string[] forEntry = new string[11];
                            string[] recodeEntry = new string[columncount];

                            recodeEntry[0] = LocalResource.CELL_ON;
                            recodeEntry[5] = ExcelAddIn.Common.Constants.DP.InstructionTHEN;

                            if (checkBox_from_Nq1.IsChecked == true)
                            {
                                repeatParams[1] = (cmb_repeat.SelectedIndex + int.Parse(repeatParams[0])).ToString();
                                repeatParams[2] = "1";

                                forEntry[0] = LocalResource.CELL_ON;
                                forEntry[5] = ExcelAddIn.Common.Constants.DP.InstructionFOR;
                                forEntry[8] = repeatParams[0];
                                forEntry[9] = repeatParams[1];
                                forEntry[10] = repeatParams[2];

                                paramList[0] = ReplaceRepeatSourceVariable(paramList[0], repeatParams[0]);
                                recodeEntry[6] = ReplaceRepeatSourceVariable(txt_new_variable.Text, repeatParams[0]);
                                recodeEntry[7] = QC4Common.Common.Constants.DP.SubstituteOperatorRECODE;
                                for (int i = 0; i < paramList.Length; i++)
                                {
                                    recodeEntry[8 + i] = paramList[i];
                                }

                                dpsaveinstructios = GetRecodeSaveArray(columncount, paramList, forEntry, recodeEntry);

                                // Creates repeat questions
                                List<RepeatQuestionSetting> repeatQuestionSettingList = new List<RepeatQuestionSetting>();
                                for (int i = 0; i <= cmb_repeat.SelectedIndex; i++)
                                {
                                    string questionText = Text_NewItem_Question.Text;
                                    string tableHeading = string.Empty;
                                    string lastDigigt = (i + int.Parse(repeatParams[0])).ToString();

                                    string variablename = txt_new_variable.Text.Remove(txt_new_variable.Text.Length - repeatParams[0].Length);
                                    string newVariable = variablename;

                                    if (i == 0)
                                    {
                                        questionText = Text_NewItem_Question.Text;
                                        tableHeading = (Combo_sourceVariable.SelectedItem as SourceVariableList).Title;
                                    }
                                    else
                                    {
                                        string sourceVariableName = repeatsList[i - 1].Variable;
                                        questionText = frmutil.UnEscapeCRLF(repeatsList[i - 1].Question).TrimEnd().TrimStart();
                                        tableHeading = frmutil.UnEscapeCRLF(repeatsList[i - 1].Title);
                                    }
                                    if (questionText.Length > QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit)
                                    {
                                        questionText = questionText.PadRight(QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit).Substring(0, QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit);
                                    }
                                    if (tableHeading.Length > QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit)
                                    {
                                        tableHeading = tableHeading.PadRight(QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit).Substring(0, QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit);
                                    }
                                    List<QuestionSettings> questionSettings = new List<QuestionSettings>();
                                    questionSettings = PopulatedDictionary.Values.ToList();
                                    if (questionSettings.Any(q => q.Variable.Equals(newVariable + lastDigigt, StringComparison.OrdinalIgnoreCase)) && ProcessingOption != QC4Common.Common.Constants.STD_DP.Process_Edit)
                                    {
                                        newVariable = GetVariableNameWith_K(newVariable + lastDigigt, PopulatedDictionary.Values.ToList());
                                    }
                                    else
                                    {
                                        newVariable = newVariable + lastDigigt;
                                    }
                                    repeatQuestionSettingList.Add(new RepeatQuestionSetting() { Variable = newVariable, Question = questionText, TableHeading = tableHeading, AnswerType = answerType.SelectedValue.ToString(), ChoiceIndex = dataTable.Rows.Count, Choices = dtChoice });
                                }

                                string question = Text_NewItem_Question.Text;
                                if (question.Length > QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit)
                                {
                                    question = question.PadRight(QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit).Substring(0, QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit);
                                }

                                if (dphelper.WriteProcess(Workbook, Util.Constants.ProcessingType.CreateNewVariable, txt_new_variable.Text, answerType.SelectedValue.ToString(), question, dataTable.Rows.Count, dtChoice, Constants.DP.SubstituteOperatorRECODE, dpsaveinstructios, isNewQuestion, WriteRow, ProcessingOption, repeatQuestionSettingList, isUpdateQuestion))//need to pass row num from here for saving 
                                {

                                    MessageDialog.Info(LocalResource.ADDQS_INFO_MSG_SUCCESS);
                                    isModifiedProcess = true;
                                    this.Close();

                                }

                            }
                            else
                            {
                                recodeEntry[6] = txt_new_variable.Text;
                                recodeEntry[7] = QC4Common.Common.Constants.DP.SubstituteOperatorRECODE;
                                for (int i = 0; i < paramList.Length; i++)
                                {
                                    recodeEntry[8 + i] = paramList[i];
                                }

                                dpsaveinstructios = GetRecodeSaveArray(columncount, paramList, null, recodeEntry);
                                if (isRepeatCase)
                                {
                                    isNewQuestion = true;
                                    isUpdateQuestion = false;
                                }
                                string question = Text_NewItem_Question.Text;
                                if (question.Length > QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit)
                                {
                                    question = question.PadRight(QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit).Substring(0, QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit);
                                }
                                if (dphelper.WriteProcess(Workbook, Util.Constants.ProcessingType.CreateNewVariable, txt_new_variable.Text, answerType.SelectedValue.ToString(), question, dataTable.Rows.Count, dtChoice, Constants.DP.SubstituteOperatorRECODE, dpsaveinstructios, isNewQuestion, WriteRow, ProcessingOption, null, isUpdateQuestion))//need to pass row num from here for saving 
                                {

                                    MessageDialog.Info(LocalResource.ADDQS_INFO_MSG_SUCCESS);
                                    isModifiedProcess = true;
                                    this.Close();

                                }
                            }
                        }

                    }

                }
                else
                {
                    bool isAllEmpty = false;
                    bool isOverlappedCriteria = false;
                    bool isNewVariableAndOnlyUpdationNeeded = false;
                    bool isChoiceNoSet = false;
                    int choiceNoSetIndex = 0;
                    isNewQuestion = true;
                    int AllEmptyCount = 0;

                    List<ChoiceList> valueEnteredChoices = new List<ChoiceList>();
                    var found = ChoiceListView.LastOrDefault(u => (!string.IsNullOrEmpty(u.AllChoices)) || (!string.IsNullOrEmpty(u.SelectedOperator)) || (!string.IsNullOrEmpty(u.Criteria)));
                    int foundAutoIndex = ChoiceListView.IndexOf(found);
                    if (found == null)
                    {
                        MessageDialog.ErrorOk(LocalResource.ADD_DP_RECODE_ERROR_MSG_NO_CHOICE_SET);
                        return;
                    }
                    for (int i = 0; i < foundAutoIndex; i++)
                    {
                        if (ChoiceListView[i].AllChoices == null && (ChoiceListView[i].SelectedOperator == null) && (ChoiceListView[i].Criteria == null))
                        {
                            isChoiceNoSet = true;
                            choiceNoSetIndex = i + 1;
                            break;
                        }
                    }
                    for (int i = 0; i <= foundAutoIndex; i++)
                    {
                        if (ChoiceListView[i].AllChoices == null || ChoiceListView[i].AllChoices.TrimEnd().TrimStart() == "")
                        {
                            AllEmptyCount++;
                        }
                        if (AllEmptyCount == foundAutoIndex + 1)
                        {
                            MessageDialog.ErrorOk(LocalResource.ADD_DP_RECODE_ERROR_MSG_NO_CHOICE_SET);
                            return;
                        }
                    }
                    for (int i = 0; i <= foundAutoIndex; i++)
                    {
                        valueEnteredChoices.Add(ChoiceListView[i]);
                    }



                    if ((ProcessingOption != QC4Common.Common.Constants.STD_DP.Process_Edit) && (string.IsNullOrEmpty(txt_new_variable.Text) || !frmutil.IsVariableNameExists(txt_new_variable.Text, PopulatedDictionary.Values.ToList(), 1)))
                    {

                        string errormsg = LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_NEW_ITEM_EMPTY + "\n" + LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_RENAME_NEW_VARIABLE_AUTOMATICALLY;
                        if (!string.IsNullOrEmpty(txt_new_variable.Text))
                        {
                            errormsg = LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_ITEM_NAME_ALREADY_EXISTS + "\n" + LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_RENAME_NEW_VARIABLE_AUTOMATICALLY;
                        }
                        System.Windows.Forms.DialogResult result;
                        result = MessageDialog.InfoYesNo(errormsg);
                        if (result == System.Windows.Forms.DialogResult.Yes)
                        {
                            string variablename = txt_new_variable.Text;
                            if (string.IsNullOrEmpty(txt_new_variable.Text))
                            {
                                //get new variable name
                                QC4Common.Util.QSUtil qsutil = new QC4Common.Util.QSUtil();
                                newvariable = qsutil.GetVariableName(Combo_sourceVariable.Text, PopulatedDictionary.Values.ToList());
                                txt_new_variable.Text = newvariable;
                                isNewQuestion = true;
                                isUpdateQuestion = false;
                            }
                            else
                            {
                                //get new variable name
                                QC4Common.Util.QSUtil qsutil = new QC4Common.Util.QSUtil();
                                newvariable = qsutil.GetVariableName(variablename, PopulatedDictionary.Values.ToList());
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
                    if (ProcessingOption == QC4Common.Common.Constants.STD_DP.Process_Edit && (string.IsNullOrEmpty(txt_new_variable.Text)))
                    {
                        string errormsg = LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_NEW_ITEM_EMPTY + "\n" + LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_RENAME_NEW_VARIABLE_AUTOMATICALLY;
                        System.Windows.Forms.DialogResult result;
                        result = MessageDialog.InfoYesNo(errormsg);
                        if (result == System.Windows.Forms.DialogResult.Yes)
                        {
                            //get new variable name
                            QC4Common.Util.QSUtil qsutil = new QC4Common.Util.QSUtil();
                            newvariable = qsutil.GetVariableName(Combo_sourceVariable.Text, PopulatedDictionary.Values.ToList());
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
                    else
                    {
                        foreach (var element in valueEnteredChoices)
                        {
                            DataRow newRow = dataTable.NewRow();
                            int result = 0;
                            bool isInteger = false;
                            bool isInvalidCharacter = false;
                            bool isInvalidChoicenumber = false;
                            bool isMultipleHiphen = false;
                            bool isHiphenAcceptable = false;
                            bool ishiphen = false;
                            bool isNotEqualToSatrt = false;
                            bool isNotEqualToSomewhere = false;
                            bool isOnlyNotEqual = false;
                            bool isAdjusentItemsPresent = false;
                            bool isIncorrectRange = false;

                            char[] spliters = new char[] { '-', '/', ',' };
                            if ((element as ChoiceList).Criteria != null && (element as ChoiceList).Criteria.TrimEnd().TrimStart() != "")
                            {

                                if ((element as ChoiceList).Criteria != null)
                                {
                                    string[] splittedValues = (element as ChoiceList).Criteria.Split(spliters);
                                    int splittedValuesresult = 0;
                                    string criteriaValue = (element as ChoiceList).Criteria;

                                    for (int i = 0; i < splittedValues.Length; i++)
                                    {
                                        if ((element as ChoiceList).Criteria.Count(x => x == '-') > 1)
                                        {
                                            if ((element as ChoiceList).Criteria.Contains(",") || (element as ChoiceList).Criteria.Contains("/"))
                                            {
                                                char[] split = new char[] { '/', ',' };
                                                string[] Values = (element as ChoiceList).Criteria.Split(split);
                                                for (int j = 0; j < Values.Length; j++)
                                                {
                                                    if (Values[j].Count(x => x == '-') > 1)
                                                    {
                                                        isMultipleHiphen = true;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        isMultipleHiphen = false;
                                                        isHiphenAcceptable = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                isMultipleHiphen = true;
                                                break;
                                            }

                                        }
                                        if (CheckSeparator((element as ChoiceList).Criteria))
                                        {
                                            isAdjusentItemsPresent = true;
                                            break;

                                        }
                                        if (criteriaValue[criteriaValue.Length - 1] == ',' || criteriaValue[criteriaValue.Length - 1] == '/')
                                        {
                                            isAdjusentItemsPresent = true;
                                            break;
                                        }
                                        if (criteriaValue[0] == ',' || criteriaValue[0] == '/')
                                        {
                                            isAdjusentItemsPresent = true;
                                            break;
                                        }
                                        if (criteriaValue[criteriaValue.Length - 1] == '-')
                                        {
                                            if (criteriaValue.Length > 1)
                                            {
                                                if (criteriaValue[criteriaValue.Length - 2] == '/' || criteriaValue[criteriaValue.Length - 2] == ',')
                                                {
                                                    isIncorrectRange = true;
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                isIncorrectRange = true;
                                                break;
                                            }
                                        }
                                        if (splittedValues[i].Contains("!"))
                                        {
                                            if ((element as ChoiceList).Criteria == "!")
                                            {
                                                isOnlyNotEqual = true;
                                                break;
                                            }
                                            if (i == 0 && (splittedValues[i].IndexOf("!") == 0))
                                            {
                                                isNotEqualToSatrt = true;
                                                break;
                                            }
                                            else if (i == 0 && (splittedValues[i].IndexOf("!") != 0))
                                            {
                                                isNotEqualToSomewhere = true;
                                                break;
                                            }
                                            else if (i != 0 && (splittedValues[i].Contains("!")))
                                            {
                                                isNotEqualToSomewhere = true;
                                                break;
                                            }
                                        }

                                        if (!int.TryParse(splittedValues[i], out splittedValuesresult))
                                        {
                                            if (splittedValues[i] != "")
                                            {
                                                isInvalidCharacter = true;

                                                string criteria = (element as ChoiceList).Criteria;
                                                if (criteria.ToLower().IndexOf('-') == criteria.Length - 1)
                                                {
                                                    ishiphen = true;
                                                    isInvalidCharacter = false;
                                                }
                                                if (splittedValues[i].Contains("."))
                                                {
                                                    isInvalidCharacter = true;
                                                    ishiphen = false;
                                                    break;
                                                }
                                            }

                                        }
                                        else
                                        {
                                            if (splittedValues[i] == "0")
                                            {
                                                isInvalidChoicenumber = true;
                                                break;
                                            }
                                        }

                                        if ((!(splittedValuesresult <= choices_grid.Items.Count)))
                                        {
                                            if ((splittedValuesresult >= 0))
                                            {
                                                isInvalidChoicenumber = true;
                                                break;
                                            }

                                        }
                                    }


                                }

                            }
                            else
                            {

                            }

                            isInteger = int.TryParse((element as ChoiceList).Criteria, out result);
                            if ((element as ChoiceList).AllChoices == null || (element as ChoiceList).AllChoices.TrimStart().TrimEnd() == "")
                            {
                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, ((element as ChoiceList).Id)) + "\n" + (LocalResource.ERR_MSG_RECODE_CHOICE_IS_NOT_INPUT));
                                SetInvalidBGChoice(((element as ChoiceList).Id) - 1);
                                break;
                            }

                            // Check data entered
                            if ((element as ChoiceList).SelectedOperator == null || (element as ChoiceList).Criteria == null || (element as ChoiceList).SelectedOperator == "" || ((element as ChoiceList).Criteria.TrimEnd().TrimStart() == ""))
                            {
                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, (dataTable.Rows.Count + 1)) + "\n" + (LocalResource.ERR_MSG_RECODE_A_BLANK_IS_SET));
                                if ((element as ChoiceList).SelectedOperator == null || (element as ChoiceList).SelectedOperator == "")
                                {
                                    SetInvalidBGOperator(dataTable.Rows.Count);
                                }
                                else if ((element as ChoiceList).Criteria == null || ((element as ChoiceList).Criteria.TrimEnd().TrimStart() == ""))
                                {
                                    SetInvalidBGCriteria(dataTable.Rows.Count);
                                }
                                break;
                            }
                            else if (isAdjusentItemsPresent)
                            {
                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, (dataTable.Rows.Count + 1)) + "\n" + (LocalResource.ERR_MSG_RECODE_A_BLANK_IS_SET));
                                SetInvalidColor(dataTable.Rows.Count);
                                break;
                            }
                            else if (isIncorrectRange)
                            {
                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, (dataTable.Rows.Count + 1)) + "\n" + (LocalResource.ERR_MSG_RECODE_INCORRECT_RANGE_SPECIFICATION));
                                SetInvalidColor(dataTable.Rows.Count);
                                break;
                            }
                            else if (isChoiceNoSet)
                            {
                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, (choiceNoSetIndex)) + "\n" + (LocalResource.ERR_MSG_RECODE_CHOICE_IS_NOT_INPUT));
                                SetInvalidBGChoice(choiceNoSetIndex - 1);
                                break;
                            }
                            else if ((element as ChoiceList).SelectedOperator != "=" && (element as ChoiceList).SelectedOperator != "<>")
                            {
                                MessageDialog.ErrorOk(LocalResource.ERR_MSG_RECODE_OPERATOR_SELECTION_IS_INVALID);
                                SetInvalidBGOperator(dataTable.Rows.Count);
                                break;
                            }
                            else if (isMultipleHiphen)
                            {
                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, (dataTable.Rows.Count + 1)) + "\n" + (LocalResource.ERR_MSG_RECODE_MULTIPLE_HYPHEN_ARE_SET));
                                SetInvalidColor(dataTable.Rows.Count);
                                break;
                            }
                            else if (isOnlyNotEqual)
                            {
                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, (dataTable.Rows.Count + 1)) + "\n" + (LocalResource.ERR_MSG_RECODE_CANNOT_SET_ALONE));
                                SetInvalidColor(dataTable.Rows.Count);
                                break;
                            }
                            else if (isNotEqualToSatrt)
                            {
                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, (dataTable.Rows.Count + 1)) + "\n" + (LocalResource.ERR_MSG_RECODE_INCLUDED_IN_THE_CRITERIA));
                                SetInvalidColor(dataTable.Rows.Count);
                                break;
                            }
                            else if (isNotEqualToSomewhere)
                            {
                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, (dataTable.Rows.Count + 1)) + "\n" + (LocalResource.ERR_MSG_RECODE_CANNOT_SET_ELSEWHERE_THAN_AT_START));
                                SetInvalidColor(dataTable.Rows.Count);

                                break;
                            }
                            else if (isInvalidChoicenumber)
                            {
                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, (dataTable.Rows.Count + 1)) + "\n" + string.Format(LocalResource.ERR_MSG_RECODE_SET_AN_INTEGER_IN_THE_RANGE_OF, "1", (choices_grid.Items.Count).ToString()));
                                SetInvalidColor(dataTable.Rows.Count);
                                break;
                            }
                            else if ((!isInteger && isInvalidCharacter) && !ishiphen)
                            {
                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, (dataTable.Rows.Count + 1)) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_SET_A_NUMERIC_VALUE));
                                SetInvalidColor(dataTable.Rows.Count);
                                break;
                            }

                            else if (isInteger && !(result <= choices_grid.Items.Count))
                            {
                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, (dataTable.Rows.Count + 1)) + "\n" + string.Format(LocalResource.ERR_MSG_RECODE_SET_AN_INTEGER_IN_THE_RANGE_OF, "1", (choices_grid.Items.Count).ToString()));
                                SetInvalidColor(dataTable.Rows.Count);
                                break;
                            }

                            else
                            {
                                newRow["Index"] = (element as ChoiceList).Id;
                                newRow["Choice"] = (element as ChoiceList).AllChoices;
                                newRow["Operator"] = (element as ChoiceList).SelectedOperator;
                                newRow["Criteria"] = (element as ChoiceList).Criteria;

                                dataTable.Rows.Add(newRow);

                            }
                        }
                        if (dataTable.Rows.Count == valueEnteredChoices.Count)
                        {
                            string[] dt_Choices_columns = new string[2];
                            dt_Choices_columns[0] = "Index";
                            dt_Choices_columns[1] = "Choice";
                            string[] dt_Choices_columns2 = new string[2];
                            dt_Choices_columns2[0] = "Operator";
                            dt_Choices_columns2[1] = "Criteria";
                            DataTable dtCriteria = dataTable.DefaultView.ToTable(false, dt_Choices_columns2);
                            DataTable dtChoice = dataTable.DefaultView.ToTable(false, dt_Choices_columns);
                            string[] paramList = new string[dtCriteria.Rows.Count + 1];
                            paramList[0] = Combo_sourceVariable.Text;

                            DataProcessHelper dataProcessHelper = new DataProcessHelper();
                            string command = QC4Common.Common.Constants.DP.SubstituteOperatorRECODE;

                            for (int i = 1; i <= dtChoice.Rows.Count; i++)
                            {
                                if (dtChoice.Rows[i - 1][1].ToString().Length > QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit)
                                {
                                    dtChoice.Rows[i - 1][1] = dtChoice.Rows[i - 1][1].ToString().PadRight(QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit).Substring(0, QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit);
                                }
                            }

                            for (int i = 1; i <= dtCriteria.Rows.Count; i++)
                            {
                                if (dtCriteria.Rows[i - 1][0].ToString() == "=")
                                {
                                    paramList[i] = dtCriteria.Rows[i - 1][1].ToString();
                                }
                                else if (dtCriteria.Rows[i - 1][0].ToString() == "<>")
                                {
                                    paramList[i] = "!" + dtCriteria.Rows[i - 1][1].ToString();
                                }

                            }

                            if (paramList.Length > 2 && answerType.SelectedValue.ToString() == "SA")
                            {
                                if (dataTable.Rows.Count > 1)
                                {
                                    isOverlappedCriteria = CheckOverlappedCriteria(dtCriteria);
                                }
                                if (isOverlappedCriteria)
                                {
                                    if (ProcessingOption != QC4Common.Common.Constants.STD_DP.Process_Edit)
                                    {
                                        MessageBoxResult result;
                                        result = MessageBox.Show(LocalResource.RECODE_OVERLAPPING_CHOICE_WARNING_MESSAGE, LocalResource.TITLE_QC, MessageBoxButton.YesNo, MessageBoxImage.Warning);
                                        if (result == MessageBoxResult.No)
                                        {
                                            return;
                                        }
                                        else
                                        {
                                            answerType.SelectedItem = "MA";
                                        }
                                    }
                                    else if (newVariableBeforeEdit != txt_new_variable.Text)
                                    {
                                        MessageBoxResult result;
                                        result = MessageBox.Show(LocalResource.RECODE_OVERLAPPING_CHOICE_WARNING_MESSAGE, LocalResource.TITLE_QC, MessageBoxButton.YesNo, MessageBoxImage.Warning);
                                        if (result == MessageBoxResult.No)
                                        {
                                            return;
                                        }
                                        else
                                        {
                                            answerType.SelectedItem = "MA";
                                        }

                                    }

                                }

                            }

                            if ((Util.Definiotion.VariableDictionary.ContainsKey(newvariable)) && (
                    (Util.Definiotion.VariableDictionary[newvariable].AnswerType != answerType.Text) ||
                    (Util.Definiotion.VariableDictionary[newvariable].CategoryCount != valueEnteredChoices.Count)))
                            {
                                System.Windows.Forms.DialogResult result;
                                result = MessageDialog.InfoYesNo(LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_ITEM_NAME_ALREADY_EXISTS + "\n" + LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_RENAME_NEW_VARIABLE_AUTOMATICALLY);
                                if (result == System.Windows.Forms.DialogResult.Yes)
                                {
                                    //get new variable name
                                    QC4Common.Util.QSUtil qsutil = new QC4Common.Util.QSUtil();
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
                              (Util.Definiotion.VariableDictionary[newvariable].CategoryCount == valueEnteredChoices.Count)
                              )
                            {
                                isUpdateQuestion = true;
                                isNewQuestion = false;
                                isNewVariableAndOnlyUpdationNeeded = true;
                            }

                            if (frmutil.IsVariableLengthExceeds(txt_new_variable.Text))
                            {
                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_QUESTION_LENGTH_EXCEED, QC4Common.Common.Constants.QsVariableMaxLength)); //ERR_MSG_QUESTION_LENGTH_EXCEED
                                return;
                            }

                            //check the newVariable name is valid or not
                            if (ProcessingOption == QC4Common.Common.Constants.STD_DP.Process_Edit)
                            {
                                if (newVariableBeforeEdit != txt_new_variable.Text)
                                {
                                    if (!(Util.Definiotion.VariableDictionary.ContainsKey(txt_new_variable.Text)))
                                    {
                                        isUpdateQuestion = false;
                                        isNewQuestion = true;
                                    }

                                    if ((string.IsNullOrEmpty(txt_new_variable.Text)) || !frmutil.IsVariableNameExists(txt_new_variable.Text, PopulatedDictionary.Values.ToList(), 1))
                                    {
                                        string errormsg = LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_NEW_ITEM_EMPTY + "\n" + LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_RENAME_NEW_VARIABLE_AUTOMATICALLY;
                                        if (!string.IsNullOrEmpty(txt_new_variable.Text))
                                        {
                                            errormsg = LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_ITEM_NAME_ALREADY_EXISTS + "\n" + LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_RENAME_NEW_VARIABLE_AUTOMATICALLY;
                                        }
                                        System.Windows.Forms.DialogResult result;
                                        result = MessageDialog.InfoYesNo(errormsg);
                                        if (result == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            string variablename = txt_new_variable.Text;
                                            if (string.IsNullOrEmpty(txt_new_variable.Text))
                                            {
                                                //get new variable name
                                                QC4Common.Util.QSUtil qsutil = new QC4Common.Util.QSUtil();
                                                txt_new_variable.Text = qsutil.GetVariableName(Combo_sourceVariable.Text, PopulatedDictionary.Values.ToList());
                                                isNewQuestion = true;
                                                isUpdateQuestion = false;
                                            }
                                            else
                                            {
                                                //get new variable name
                                                QC4Common.Util.QSUtil qsutil = new QC4Common.Util.QSUtil();
                                                txt_new_variable.Text = qsutil.GetVariableName(variablename, PopulatedDictionary.Values.ToList());
                                                isNewQuestion = true;
                                                isUpdateQuestion = false;
                                            }


                                        }
                                        else if (result == System.Windows.Forms.DialogResult.No)
                                        {
                                            return;
                                        }

                                        Util.QS.NewQuestionValidation validation = new Util.QS.NewQuestionValidation(txt_new_variable.Text);
                                        if (!isNewVariableAndOnlyUpdationNeeded && !validation.Validation_Variable())
                                        {
                                            return;
                                        }

                                    }
                                    else
                                    {
                                        Util.QS.NewQuestionValidation validation = new Util.QS.NewQuestionValidation(txt_new_variable.Text.TrimEnd().TrimStart());
                                        if (!isNewVariableAndOnlyUpdationNeeded && !validation.Validation_Variable())
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
                                Util.QS.NewQuestionValidation validation = new Util.QS.NewQuestionValidation(txt_new_variable.Text.TrimEnd().TrimStart());
                                if (!isNewVariableAndOnlyUpdationNeeded && !validation.Validation_Variable())
                                {
                                    return;
                                }
                            }

                            /// Save settings.
                            Logic.DataProcessHelper dphelper = new Logic.DataProcessHelper();
                            int columncount = 8 + (dtCriteria.Rows.Count + 1); // total number of columns(column upto operator + all parameters)
                            string[,] dpsaveinstructios;
                            string[] forEntry = new string[11];
                            string[] recodeEntry = new string[columncount];

                            recodeEntry[0] = LocalResource.CELL_ON;
                            recodeEntry[5] = ExcelAddIn.Common.Constants.DP.InstructionTHEN;


                            if (checkBox_from_Nq1.IsChecked == true)
                            {
                                repeatParams[1] = (cmb_repeat.SelectedIndex + int.Parse(repeatParams[0])).ToString();
                                repeatParams[2] = "1";

                                forEntry[0] = LocalResource.CELL_ON;
                                forEntry[5] = ExcelAddIn.Common.Constants.DP.InstructionFOR;
                                forEntry[8] = repeatParams[0];
                                forEntry[9] = repeatParams[1];
                                forEntry[10] = repeatParams[2];

                                paramList[0] = ReplaceRepeatSourceVariable(paramList[0], repeatParams[0]);
                                recodeEntry[6] = ReplaceRepeatSourceVariable(txt_new_variable.Text, repeatParams[0]);
                                recodeEntry[7] = QC4Common.Common.Constants.DP.SubstituteOperatorRECODE;
                                for (int i = 0; i < paramList.Length; i++)
                                {
                                    recodeEntry[8 + i] = paramList[i];
                                }

                                // Gets save instruction array
                                dpsaveinstructios = GetRecodeSaveArray(columncount, paramList, forEntry, recodeEntry);

                                // Creates repeat questions
                                List<RepeatQuestionSetting> repeatQuestionSettingList = new List<RepeatQuestionSetting>();
                                for (int i = 0; i <= cmb_repeat.SelectedIndex; i++)
                                {
                                    string questionText = Text_NewItem_Question.Text;
                                    string tableHeading = string.Empty;
                                    string lastDigigt = (i + int.Parse(repeatParams[0])).ToString();

                                    string variablename = txt_new_variable.Text.Remove(txt_new_variable.Text.Length - repeatParams[0].Length);
                                    string newVariable = variablename;

                                    if (i == 0)
                                    {
                                        questionText = Text_NewItem_Question.Text;
                                        tableHeading = frmutil.UnEscapeCRLF((Combo_sourceVariable.SelectedItem as SourceVariableList).Title);//https://redmine.macromill.com/issues/208325
                                    }
                                    else
                                    {
                                        string sourceVariableName = repeatsList[i - 1].Variable;
                                        questionText = frmutil.UnEscapeCRLF(repeatsList[i - 1].Question).TrimEnd().TrimStart();
                                        tableHeading = frmutil.UnEscapeCRLF(repeatsList[i - 1].Title);
                                    }
                                    if (questionText.Length > QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit)
                                    {
                                        questionText = questionText.PadRight(QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit).Substring(0, QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit);
                                    }
                                    if (tableHeading.Length > QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit)
                                    {
                                        tableHeading = tableHeading.PadRight(QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit).Substring(0, QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit);
                                    }
                                    List<QuestionSettings> questionSettings = new List<QuestionSettings>();
                                    questionSettings = PopulatedDictionary.Values.ToList();
                                    if (questionSettings.Any(q => q.Variable.Equals(newVariable + lastDigigt, StringComparison.OrdinalIgnoreCase)) && (ProcessingOption != QC4Common.Common.Constants.STD_DP.Process_Edit))
                                    {
                                        newVariable = GetVariableNameWith_K(newVariable + lastDigigt, PopulatedDictionary.Values.ToList());
                                    }
                                    else
                                    {
                                        newVariable = newVariable + lastDigigt;
                                    }

                                    repeatQuestionSettingList.Add(new RepeatQuestionSetting() { Variable = newVariable, Question = questionText, TableHeading = tableHeading, AnswerType = answerType.SelectedValue.ToString(), ChoiceIndex = dataTable.Rows.Count, Choices = dtChoice });
                                }
                                string question = Text_NewItem_Question.Text;
                                if (question.Length > QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit)
                                {
                                    question = question.PadRight(QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit).Substring(0, QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit);
                                }

                                if (dphelper.WriteProcess(Workbook, Util.Constants.ProcessingType.CreateNewVariable, txt_new_variable.Text, answerType.SelectedValue.ToString(), question, dataTable.Rows.Count, dtChoice, Constants.DP.SubstituteOperatorRECODE, dpsaveinstructios, isNewQuestion, WriteRow, ProcessingOption, repeatQuestionSettingList, isUpdateQuestion))//need to pass row num from here for saving 
                                {

                                    MessageDialog.Info(LocalResource.ADDQS_INFO_MSG_SUCCESS);
                                    isModifiedProcess = true;
                                    this.Close();

                                }

                            }
                            else
                            {
                                recodeEntry[6] = txt_new_variable.Text;
                                recodeEntry[7] = QC4Common.Common.Constants.DP.SubstituteOperatorRECODE;

                                for (int i = 0; i < paramList.Length; i++)
                                {
                                    recodeEntry[8 + i] = paramList[i];
                                }

                                dpsaveinstructios = GetRecodeSaveArray(columncount, paramList, null, recodeEntry);
                                if (isRepeatCase)
                                {
                                    isNewQuestion = true;
                                    isUpdateQuestion = false;
                                }
                                string question = Text_NewItem_Question.Text;
                                if (question.Length > QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit)
                                {
                                    question = question.PadRight(QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit).Substring(0, QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit);
                                }
                                if (dphelper.WriteProcess(Workbook, Util.Constants.ProcessingType.CreateNewVariable, txt_new_variable.Text, answerType.SelectedValue.ToString(), question, dataTable.Rows.Count, dtChoice, Constants.DP.SubstituteOperatorRECODE, dpsaveinstructios, isNewQuestion, WriteRow, ProcessingOption, null, isUpdateQuestion))//need to pass row num from here for saving 
                                {

                                    MessageDialog.Info(LocalResource.ADDQS_INFO_MSG_SUCCESS);
                                    isModifiedProcess = true;
                                    this.Close();

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


        private string[,] GetRecodeSaveArray(int columncount, string[] param, string[] for_entries, string[] recode_entries)
        {
            if (for_entries != null)
            {
                if (columncount < for_entries.Length)
                {
                    columncount = for_entries.Length;
                }
            }

            int rowCount = 1;
            string[,] dpsaveinstructios;
            string nextEntry = "NEXT";
            if (checkBox_from_Nq1.IsChecked == true)
            {
                rowCount = 3;
                dpsaveinstructios = new string[rowCount, (QC4Common.Common.Constants.DP.MAX_DP_COLUMN - QC4Common.Common.Constants.DP.OnOffColumn) + 1];//SAVE ARRAY
                for (int i = 0; i < for_entries.Length; i++)
                {
                    dpsaveinstructios[0, i] = for_entries[i];
                }
                for (int i = 0; i < recode_entries.Length; i++)
                {
                    dpsaveinstructios[1, i] = recode_entries[i];
                }
                dpsaveinstructios[rowCount - 1, 5] = nextEntry;
                dpsaveinstructios[rowCount - 1, 0] = LocalResource.CELL_ON;
            }
            else
            {
                rowCount = 1;
                dpsaveinstructios = new string[rowCount, (QC4Common.Common.Constants.DP.MAX_DP_COLUMN - QC4Common.Common.Constants.DP.OnOffColumn) + 1];//SAVE ARRAY
                for (int i = 0; i < columncount; i++)
                {
                    dpsaveinstructios[rowCount - 1, i] = recode_entries[i];
                }
            }

            return dpsaveinstructios;

        }

        private void Btn_RightArrow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((choices_grid.SelectedItems != null) && (choices_grid.SelectedItems.Count > 0))
                {
                    ChoiceCriteria choiceCriteria = new ChoiceCriteria();
                    choiceCriteria.Choices = new List<string>();
                    choiceCriteria.Criteria = new List<string>();

                    for (int i = 0; i < choices_grid.SelectedItems.Count; i++)
                    {
                        System.Data.DataRowView selectedFile = (System.Data.DataRowView)choices_grid.SelectedItems[i];
                        string selectedIndex = Convert.ToString(selectedFile.Row.ItemArray[0]);
                        choiceCriteria.Criteria.Add((selectedIndex));
                        string str = Convert.ToString(selectedFile.Row.ItemArray[1]);
                        choiceCriteria.Choices.Add((str));
                    }

                    int countValue = 0;
                    int[] a = new int[choiceCriteria.Criteria.Count];
                    foreach (var criteria in choiceCriteria.Criteria)
                    {
                        int.TryParse(criteria, out a[countValue]);
                        countValue++;
                    }

                    try
                    {
                        DataGridCellInfo currentCell = data_grid.CurrentCell;
                        (currentCell.Item as ChoiceList).AllChoices = string.Join("  ", choiceCriteria.Choices);
                        (currentCell.Item as ChoiceList).Criteria = QC4Common.Classes.Help.GetCriteriaValue(a, choiceCriteria.Criteria.Count);
                        (currentCell.Item as ChoiceList).SelectedOperator = "=";
                        CollectionViewSource.GetDefaultView(data_grid.ItemsSource).Refresh();
                    }
                    catch (Exception ex)
                    {
                        string message = ex.Message;
                        _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                    }

                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }


        }


        private void Combo_sourceVariable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Windows.Controls.ComboBox sourceVariable = ((System.Windows.Controls.ComboBox)sender);
            currentSourceVariable = Combo_sourceVariable.Text;
            repeatCurrentDigit = 0;
            if (checkBox_from_Nq1.IsChecked == true && isRepeatEnabled == false)
            {
                isRepeatEnabled = true;
                MessageDialog.ErrorOk(LocalResource.ALERT_REPEAT_RECODE);
                return;
            }
            else
            {
                isRepeatEnabled = false;
            }

            List<string> sourceAnswerTypes;
            var x = sourceVariable.SelectedItem;
            if (sourceVariable.SelectedItem != null && checkBox_from_Nq1.IsChecked == false)
            {
                sourceAnswerTypes = new List<string>();
                choicesCount.IsEditable = true;
                choicesCount.IsEnabled = true;
                txt_answerType.IsEnabled = true;
                txt_answerTypeWithChoiceCount.IsEnabled = true;
                txt_source_Question.IsEnabled = true;
                txt_new_variable.IsEnabled = true;
                txt_new_variable.Background = Brushes.White;

                answerType.IsEnabled = true;
                Text_NewItem_Question.IsEnabled = true;
                Text_NewItem_Question.Background = Brushes.White;
                choices_grid.IsEnabled = true;
                choices_grid.Background = Brushes.White;

                SourceVariableList list = new SourceVariableList();
                list = (SourceVariableList)(sourceVariable.SelectedItem);

                //buttons are enabled
                NewItemSearchbutton.IsEnabled = true;
                NewItemSearchbutton.Opacity = 1;

                Save.IsEnabled = true;

                //Enabling or disabling of summarize choice button
                if (list.Choices.Count > 1)
                {
                    btn_summarizeChoices.IsEnabled = true;
                }
                else
                {
                    btn_summarizeChoices.IsEnabled = false;
                }


                int digit = GetDigitFromSourceVariable(list.Variable);
                repeatsList.Clear();
                if (digit >= 0)
                {
                    SearchExistingSourceVariables(digit, list);
                }


                txt_answerType.Text = list.AnswerType;
                txt_answerTypeWithChoiceCount.Text = (list.Choices.Count).ToString();
                txt_source_Question.Text = frmutil.UnEscapeCRLF(list.Question);
                for (int i = 0; i < list.Choices.Count; i++)
                {
                  //  list.Choices[i] = frmutil.EscapeCRLF(list.Choices[i]);
                }


                var dataSet = new DataSet();
                var dataTable = new DataTable();
                dataSet.Tables.Add(dataTable);
                int count = 0;
                dataTable.Columns.Add("Index");
                dataTable.Columns.Add("Choice");
                foreach (var item in list.Choices)
                {
                    DataRow newRow = dataTable.NewRow();
                    count = count + 1;
                    newRow["Index"] = count;
                    newRow["Choice"] = frmutil.EscapeCRLF(item);

                    dataTable.Rows.Add(newRow);
                }
                choices_grid.ItemsSource = dataTable.DefaultView;

                //Get new variable name
                QC4Common.Util.QSUtil qsutil = new QC4Common.Util.QSUtil();
                txt_new_variable.Text = qsutil.GetVariableName(list.Variable, PopulatedDictionary.Values.ToList());

                sourceAnswerTypes.Add("MA");
                sourceAnswerTypes.Add("SA");
                answerType.ItemsSource = sourceAnswerTypes;
                answerType.SelectedItem = list.AnswerType;
                if (list.AnswerType == "MA")
                {
                    answerType.IsEnabled = false;
                }
                Text_NewItem_Question.Text = (frmutil.UnEscapeCRLF(Util.Definiotion.VariableDictionary[list.Variable].TableHeading) + " " + frmutil.UnEscapeCRLF(list.Question)).TrimEnd().TrimStart();
                questionTextBeforeEdit = Text_NewItem_Question.Text;
                //Recode repeats times
                if (repeatsList.Count > 0)
                {
                    repeats_section.Visibility = Visibility.Visible;
                    List<int> repeats = new List<int>();
                    for (int i = 0; i <= repeatsList.Count; i++)
                    {
                        repeats.Add(i + 1);
                    }
                    cmb_repeat.ItemsSource = repeats;
                    if (QC4Common.Common.Constants.GlobalMode != "ja-JP," + Util.Constants.QCFont.MS_Gothic)
                        checkBox_from_Nq1.Content = LocalResource.LBL_FROM + " " + list.Variable;
                    else
                        checkBox_from_Nq1.Content = list.Variable + LocalResource.LBL_FROM;
                    repeatParams[0] = digit.ToString();
                    repeatParams[1] = digit.ToString();
                    repeatParams[2] = "1";
                }
                else
                {
                    repeats_section.Visibility = Visibility.Hidden;
                }

                // Set answer type for new item selection window
                MagnifyingGlassButton.AnswerType = txt_answerType.Text;
            }
            else
            {
                if (checkBox_from_Nq1.IsChecked != true)
                {
                    Combo_sourceVariable.Text = "";
                    txt_answerType.Text = "";
                    txt_answerTypeWithChoiceCount.Text = "";
                    txt_source_Question.Text = "";
                    txt_new_variable.Text = "";
                    answerType.ItemsSource = null;
                    Text_NewItem_Question.Text = "";
                    choicesCount.SelectedIndex = 0;
                    Save.IsEnabled = false;
                    btn_summarizeChoices.IsEnabled = false;
                    NewItemSearchbutton.IsEnabled = false;
                    NewItemSearchbutton.Opacity = 0.5;
                    choices_grid.ItemsSource = null;
                }
            }

        }

        /// <summary>Generates the new name of the variable starting with N,N1,N2,N3..</summary>
        /// <param name="variable">The variable.</param>
        /// <returns></returns>
        private string GenerateNewVariableName(string variable)
        {
            string newVariable = "N" + variable;
            string digit;
            if (variable.IndexOf("N") == 0)
            {
                if (Char.IsDigit(variable[1]))
                {
                    digit = variable[1].ToString();
                    for (int i = 2; i < variable.Length; i++)
                    {
                        if (Char.IsDigit(variable[i]))
                        {
                            digit += variable[i];
                        }

                        else

                        {
                            variable = variable.Remove(0, 1);
                            newVariable = "N" + (int.Parse(digit) + 1).ToString() + variable.Remove(variable.IndexOf(digit), digit.Length);
                            break;
                        }
                    }
                }
                else
                {
                    newVariable = "N1" + variable.Remove(0, 1);
                }
            }
            else
            {
                newVariable = "N" + variable;
            }
            return newVariable;
        }


        private void Data_grid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void Data_grid_LoadingRowDetails(object sender, DataGridRowDetailsEventArgs e)
        {
            var x = e.Row;
        }

        public class ChoiceList : INotifyPropertyChanged
        {
            private int id;
            private string criteria;
            private string allChoices;
            private List<string> choices;
            private List<string> op;
            private string selectedOperator;
            private bool isCriteriaInvalid = false;
            private bool isCriteriaEmpty = false;
            private bool isChoiceEmpty = false;
            private bool isOperatorEmpty = false;
            private bool isChoiceInvalid = false;

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
            public string Criteria
            {
                get
                {
                    return criteria;
                }
                set
                {
                    criteria = value;
                }
            }
            public bool IsCriteriaInvalid
            {
                get
                {
                    return isCriteriaInvalid;
                }
                set
                {
                    isCriteriaInvalid = value;
                    NotifyPropertyChanged();
                }
            }
            public bool IsCriteriaEmpty
            {
                get
                {
                    return isCriteriaEmpty;
                }
                set
                {
                    isCriteriaEmpty = value;
                    NotifyPropertyChanged();
                }
            }
            public bool IsOperatorEmpty
            {
                get
                {
                    return isOperatorEmpty;
                }
                set
                {
                    isOperatorEmpty = value;
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
            public bool IsChoiceInvalid
            {
                get
                {
                    return isChoiceInvalid;
                }
                set
                {
                    isChoiceInvalid = value;
                    NotifyPropertyChanged();
                }
            }
            public string AllChoices
            {
                get
                {
                    return allChoices;
                }
                set
                {
                    allChoices = value;
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
            public List<string> Operator
            {
                get
                {
                    op = new List<string>() { "=", "<>" };
                    return op;
                }
                set
                {
                    op = value;
                }
            }
            public string SelectedOperator
            {
                get
                {
                    return selectedOperator;
                }
                set
                {
                    selectedOperator = value;
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


                    });
                }
            }
            data_grid.ItemsSource = ChoiceListView;
        }

        private void From_Nq1_Click(object sender, RoutedEventArgs e)
        {
        }

        private void AnswerType_DropDownOpened(object sender, EventArgs e)
        {
        }
        public class VisibilityConverter : IValueConverter
        {
            public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                bool visibility = false;
                if (value is bool)
                {
                    visibility = (bool)value;
                }
                return visibility ? Visibility.Visible : Visibility.Collapsed;
            }
            public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                Visibility visibility = (Visibility)value;
                return (visibility == Visibility.Visible);
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

        private void Data_grid_LoadingRow_1(object sender, DataGridRowEventArgs e)
        {

        }

        private void Window_Closed(object sender, EventArgs e)
        {
        }
        public int GetLastRow(DataTable dt)
        {
            int row = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[i][1])))
                {
                    row = i + 1;
                }
            }
            return row;
        }

        public class ChoiceCriteria
        {
            private List<string> choices;
            private List<string> criteria;
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
            public List<string> Criteria
            {
                get
                {
                    return criteria;
                }
                set
                {
                    criteria = value;
                }
            }
        }

        public void AddSummarizedChoices(ChoiceList foundChoiceData, ObservableCollection<ChoiceList> start, ObservableCollection<ChoiceList> end, ObservableCollection<ChoiceList> source, bool isTopStart, bool isBottomStart, bool isSourceVariableEnabled)
        {
            int index = ChoiceListView.IndexOf(foundChoiceData);


            if (choicesCount.Text == LocalResource.LBL_AUTO)
            {
                if (isSourceVariableEnabled)
                {
                    int i = 1;
                    if (end.Count != 0 || start.Count != 0 || source.Count != 0)
                    {
                        foreach (var summarizedChoice in start)
                        {
                            summarizedChoice.Id = i;
                            ChoiceListView[i - 1] = summarizedChoice;
                            i = i + 1;
                        }
                        foreach (var summarizedChoice in source)
                        {
                            summarizedChoice.Id = i;
                            ChoiceListView[i - 1] = summarizedChoice;
                            i = i + 1;
                        }
                        foreach (var summarizedChoice in end)
                        {
                            summarizedChoice.Id = i;
                            ChoiceListView[i - 1] = summarizedChoice;
                            i = i + 1;
                        }
                    }
                }

                else
                {
                    if (foundChoiceData == null)
                    {
                        int i = 1;
                        if (end.Count != 0 || start.Count != 0 || source.Count != 0)
                        {
                            foreach (var summarizedChoice in start)
                            {
                                summarizedChoice.Id = i;
                                ChoiceListView[i - 1] = summarizedChoice;
                                i = i + 1;
                            }
                            foreach (var summarizedChoice in end)
                            {
                                summarizedChoice.Id = i;
                                ChoiceListView[i - 1] = summarizedChoice;
                                i = i + 1;
                            }
                        }
                    }
                    else
                    {
                        int i = 0;
                        int j = 1;
                        ObservableCollection<ChoiceList> choiceListNew = new ObservableCollection<ChoiceList>();
                        choiceListNew = start;
                        for (i = 0; i <= index; i++)
                        {
                            choiceListNew.Add(new ChoiceList() { AllChoices = ChoiceListView[i].AllChoices, Criteria = ChoiceListView[i].Criteria, SelectedOperator = ChoiceListView[i].SelectedOperator });
                        }
                        foreach (var summarizedChoice in end)
                        {
                            choiceListNew.Add(summarizedChoice);
                        }
                        foreach (var summarizedChoice in choiceListNew)
                        {
                            summarizedChoice.Id = j;
                            ChoiceListView[j - 1] = summarizedChoice;
                            j = j + 1;
                        }
                    }
                }
            }
            else
            {
                var found = ChoiceListView.LastOrDefault(u => (u.AllChoices != null) || (u.SelectedOperator != null) || (u.Criteria != null));
                int length = ChoiceListView.IndexOf(found) + 1 + start.Count + source.Count + end.Count;

                if (isSourceVariableEnabled)
                {
                    length = start.Count + source.Count + end.Count;
                    if (ChoiceListView.Count < length)
                    {
                        for (int j = ChoiceListView.Count; j < length; j++)
                        {
                            ChoiceListView.Add(new ChoiceList() { Id = j + 1, AllChoices = null, Criteria = null, SelectedOperator = null });
                        }
                    }
                    int i = 1;
                    if (end.Count != 0 || start.Count != 0 || source.Count != 0)
                    {
                        foreach (var summarizedChoice in start)
                        {
                            summarizedChoice.Id = i;
                            ChoiceListView[i - 1] = summarizedChoice;
                            i = i + 1;
                        }
                        foreach (var summarizedChoice in source)
                        {
                            summarizedChoice.Id = i;
                            ChoiceListView[i - 1] = summarizedChoice;
                            i = i + 1;
                        }
                        foreach (var summarizedChoice in end)
                        {
                            summarizedChoice.Id = i;
                            ChoiceListView[i - 1] = summarizedChoice;
                            i = i + 1;
                        }
                    }
                }

                else
                {
                    if (ChoiceListView.Count < length)
                    {
                        for (int i = ChoiceListView.Count; i < length; i++)
                        {
                            ChoiceListView.Add(new ChoiceList() { Id = i + 1, AllChoices = null, Criteria = null, SelectedOperator = null });
                        }
                    }

                    if (found == null)
                    {
                        int i = 1;
                        if (end.Count != 0 || start.Count != 0 || source.Count != 0)
                        {
                            foreach (var summarizedChoice in start)
                            {
                                summarizedChoice.Id = i;
                                ChoiceListView[i - 1] = summarizedChoice;
                                i = i + 1;
                            }
                            foreach (var summarizedChoice in end)
                            {
                                summarizedChoice.Id = i;
                                ChoiceListView[i - 1] = summarizedChoice;
                                i = i + 1;
                            }
                        }
                    }
                    else
                    {
                        int i = 0;
                        int j = 1;
                        int foundAutoIndex = ChoiceListView.IndexOf(found);
                        ObservableCollection<ChoiceList> choiceListNew = new ObservableCollection<ChoiceList>();
                        choiceListNew = start;
                        for (i = 0; i <= foundAutoIndex; i++)
                        {
                            choiceListNew.Add(new ChoiceList() { AllChoices = ChoiceListView[i].AllChoices, Criteria = ChoiceListView[i].Criteria, SelectedOperator = ChoiceListView[i].SelectedOperator });
                        }
                        foreach (var summarizedChoice in end)
                        {
                            choiceListNew.Add(summarizedChoice);
                        }
                        foreach (var summarizedChoice in choiceListNew)
                        {
                            summarizedChoice.Id = j;
                            ChoiceListView[j - 1] = summarizedChoice;
                            j = j + 1;
                        }
                    }
                }
            }
            if (ChoiceListView.Count > choicesCount.SelectedIndex)
            {
                if (ChoiceListView.Count == 1000)
                {
                    choicesCount.SelectedIndex = 0;
                }
                else
                {
                    choicesCount.SelectedIndex = ChoiceListView.Count;
                }

            }
        }

        public int GetDigitFromSourceVariable(string sourceVariable)
        {
            string digit = string.Empty;
            int val;

            for (int i = 0; i < sourceVariable.Length; i++)
            {
                if (Char.IsDigit(sourceVariable[i]))
                    digit += sourceVariable[i];
                else
                    digit = string.Empty;
            }

            if (digit.Length > 0)
            {
                val = int.Parse(digit.Normalize(NormalizationForm.FormKC));
            }
            else
            {
                val = -1;
            }
            return val;
        }
        public string GetVariablenameWithoutLastDigitsFromSourceVariable(string sourceVariable)
        {
            string variablename = string.Empty;
            int pos = 0;
            sourceVariable = stringreverse(sourceVariable);
            for (int i = 0; i < sourceVariable.Length; i++)
            {
                if (!Char.IsDigit(sourceVariable[i]))
                {
                    pos = i;
                    break;
                }
            }
            variablename = sourceVariable.Substring(pos);
            variablename = stringreverse(variablename);
            return variablename;
        }
        public string stringreverse(string name)
        {
            char[] charArray = name.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        public String GetVariableNameWith_K(String variable, List<QC4Common.Model.QuestionSettings> variableList)
        {
            if (!variableList.Any(q => q.Variable.Equals(variable, StringComparison.OrdinalIgnoreCase)))
            {
                return variable;
            }

            Regex regex = new Regex(@"^K(\d+)(.*)");
            Match match = regex.Match(variable);
            if (match.Success)
            {
                variable = GenerateVariableNameWith_K(match.Groups[2].Value, variableList, Convert.ToInt32(match.Groups[1].Value) + 1);
            }
            else
            {
                regex = new Regex(@"^K(.*)");
                match = regex.Match(variable);
                if (match.Success)
                {
                    variable = GenerateVariableNameWith_K(match.Groups[1].Value, variableList, 1);
                }
                else
                {
                    variable = GenerateVariableNameWith_K(variable, variableList);
                }
            }
            return variable;
        }
        public bool CheckOverlappedChoices(DataTable choices)
        {
            bool isOverlapped = false;
            int k = 0;
            int choiceCountSelected = 0;
            if (choicesCount.SelectedIndex == 0)
            {
                choiceCountSelected = 1000;
            }
            else
            {
                choiceCountSelected = choicesCount.SelectedIndex;
            }
            int arraySize = choiceCountSelected * choices_grid.Items.Count;
            string[] splittedChoices = new string[arraySize];
            for (int i = 1; i <= choices.Rows.Count; i++)
            {
                string[] Choices = (choices.Rows[i - 1][1].ToString()).Split(' ');
                for (int j = 0; j < Choices.Length; j++)
                {
                    if (Choices[j] != "")
                    {
                        splittedChoices[k] = Choices[j];
                        k = k + 1;
                    }
                }
            }
            splittedChoices = splittedChoices.Where(choice => !string.IsNullOrEmpty(choice)).ToArray();
            if (splittedChoices.Distinct().Count() != splittedChoices.Count())
            {
                isOverlapped = true;
            }
            else
            {
                isOverlapped = false;
            }
            return isOverlapped;
        }
        public bool CheckOverlappedCriteria(DataTable criterias)
        {
            bool isOverlapped = false;
            char[] spliters = new char[] { '-', '/', ',' };
            string[] criteria = new string[criterias.Rows.Count];
            List<string> paramValues = new List<string>();
            for (int i = 1; i <= criterias.Rows.Count; i++)
            {
                if (criterias.Rows[i - 1][0].ToString() == "=")
                {
                    paramValues.Add(criterias.Rows[i - 1][1].ToString());
                }
                else if (criterias.Rows[i - 1][0].ToString() == "<>")
                {
                    paramValues.Add("!" + criterias.Rows[i - 1][1].ToString());
                }

            }
            if (frmutil.IsOverlap(paramValues, choices_grid.Items.Count))
            {
                isOverlapped = true;
                if (criteria.Count() == 2)
                {
                    if (criterias.Rows[0][0] == criterias.Rows[1][0])
                    {
                        isOverlapped = true;
                    }
                    else
                    {
                        isOverlapped = false;
                    }

                }
            }
            else
            {
                isOverlapped = false;
            }
            return isOverlapped;
        }
        private static string GenerateVariableNameWith_K(string varible, List<QC4Common.Model.QuestionSettings> variableList, int times = 0)
        {
            String str = "K" + (times == 0 ? "" : times.ToString()) + varible;
            //Fix for #272219 - Code is modified so that if there exist any variable starts with value of variable to be named is omitted
            //and therefore Integer value succeding the K is incremented by one
            String str2 = str;
            Match match = Regex.Match(str, @"\d+$");
            if (match.Success)
            {
                // If there's a match, remove the matched numbers from the string
                str2 = str2.Substring(0, match.Index);
            }
            if (variableList.Any(q => q.Variable.StartsWith(str2)))
            {
                str = GenerateVariableNameWith_K(varible, variableList, times + 1);
            }
              return str;
        }
        public string ReplaceRepeatSourceVariable(string actualSourceVariable, string digit)
        {
            actualSourceVariable = actualSourceVariable.Remove(actualSourceVariable.LastIndexOf(digit)) + "[\\]";
            return actualSourceVariable;
        }

        private void CheckBox_from_Nq1_Checked(object sender, RoutedEventArgs e)
        {
            if (checkBox_from_Nq1.IsChecked == true)
            {
                cmb_repeat.IsEnabled = true;
                txt_repeat.IsEnabled = true;
                Color color = (Color)ColorConverter.ConvertFromString("#FF333333");
                txt_repeat.Foreground = new System.Windows.Media.SolidColorBrush(color);
                if (cmb_repeat.SelectedValue == null)
                {
                    cmb_repeat.SelectedValue = cmb_repeat.Items.Count;
                }
                txt_new_variable.Text = GetVariableNameWith_K(Combo_sourceVariable.Text, PopulatedDictionary.Values.ToList());
                txt_new_variable.IsReadOnly = true;
                Color newVariableTextboxColor = (Color)ColorConverter.ConvertFromString("#F0F0F0");
                txt_new_variable.Background = new System.Windows.Media.SolidColorBrush(newVariableTextboxColor);
                NewItemSearchbutton.IsEnabled = false;
                NewItemSearchbutton.Opacity = 0.5;
                Text_NewItem_Question.Text = txt_source_Question.Text;
            }
        }

        private void CheckBox_from_Nq1_Unchecked(object sender, RoutedEventArgs e)
        {
            if (checkBox_from_Nq1.IsChecked == false)
            {
                cmb_repeat.IsEnabled = false;
                txt_repeat.IsEnabled = false;
                Color color = (Color)ColorConverter.ConvertFromString("#999999");
                txt_repeat.Foreground = new System.Windows.Media.SolidColorBrush(color);
                QC4Common.Util.QSUtil qsutil = new QC4Common.Util.QSUtil();
                txt_new_variable.Text = qsutil.GetVariableName(Combo_sourceVariable.Text, PopulatedDictionary.Values.ToList());
                txt_new_variable.IsReadOnly = false;
                txt_new_variable.Background = Brushes.White;
                NewItemSearchbutton.IsEnabled = true;
                NewItemSearchbutton.Opacity = 1;
                Text_NewItem_Question.Text = questionTextBeforeEdit;
            }
        }

        private void Combo_sourceVariable_DropDownOpened(object sender, EventArgs e)
        {

        }

        private void Combo_sourceVariable_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        public class RepeatQuestionSetting
        {
            private string variable;
            private string answerType;
            private string question;
            private int choiceIndex;
            private string tableHeading;
            private DataTable choices;
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
                    return answerType;
                }
                set
                {
                    answerType = value;
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
            public string TableHeading
            {
                get
                {
                    return tableHeading;
                }
                set
                {
                    tableHeading = value;
                }
            }
            public int ChoiceIndex
            {
                get
                {
                    return choiceIndex;
                }
                set
                {
                    choiceIndex = value;
                }
            }
            public DataTable Choices
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
        }
        #region Handling Datagrid events
        System.Windows.Controls.DataGrid ExpGrid = null;
        private void Choices_grid_CurrentCellChanged(object sender, EventArgs e)
        {
            var dg = (System.Windows.Controls.DataGrid)sender;
            ExpGrid = dg;
            dg.Focus();
        }


        private void Combo_sourceVariable_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (Key.Tab != e.Key && !frmutil.IsSystemKey(e))
            {
                Combo_KeyUp(Combo_sourceVariable, sender, e);
            }

        }

        #region Comboboxes Event handlers
        System.Windows.Controls.ComboBox combo = null;
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

        private void Combo_sourceVariable_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Combo_KeyDown(Combo_sourceVariable, sender, e);
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
            System.Windows.Controls.ComboBox sen = ((System.Windows.Controls.ComboBox)sender);
            combo = sen;
        }

        private void Window_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void Txt_new_variable_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (ProcessingOption == QC4Common.Common.Constants.STD_DP.Process_Edit)
            {
                if (newVariableBeforeEdit != txt_new_variable.Text)
                {
                    choicesCount.IsEditable = true;
                    choicesCount.IsEnabled = true;
                    if (txt_answerType.Text != "MA")
                    {
                        answerType.IsEnabled = true;
                    }
                    btn_summarizeChoices.IsEnabled = true;
                }
            }
        }

        private void Choices_grid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                txt_new_variable.Focus();
                e.Handled = true;
            }
            else
                QC4Common.Util.GridCommonFunc.Arrow(sender, e);
        }
        #endregion

        private void b1SetColor(object sender, MouseButtonEventArgs e)
        {
            if (ExpGrid != null)
                ExpGrid.Focus();
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
                            if (data_grid.CurrentCell.Column.DisplayIndex == 2)
                            {
                                //Selection in choice
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
                                            if (col == 2)
                                            {
                                                if (data[i, (j - 1)] == null)
                                                {
                                                    ChoiceListView[RowIndex].SelectedOperator = null;
                                                }
                                                else
                                                {
                                                    if (data[i, (j - 1)].ToString() == "=" || data[i, (j - 1)].ToString() == "<>")
                                                    {
                                                        ChoiceListView[RowIndex].SelectedOperator = data[i, (j - 1)].ToString();
                                                    }
                                                }

                                            }
                                            else if (col == 1)
                                            {
                                                if (data[i, (j - 1)] == null)
                                                {
                                                    ChoiceListView[RowIndex].AllChoices = null;
                                                }
                                                else
                                                {
                                                    ChoiceListView[RowIndex].AllChoices = data[i, (j - 1)].ToString();
                                                }

                                            }
                                            else if (col == 3)
                                            {
                                                if (data[i, (j - 1)] == null)
                                                {
                                                    ChoiceListView[RowIndex].Criteria = null;
                                                }
                                                else
                                                {
                                                    ChoiceListView[RowIndex].Criteria = data[i, (j - 1)].ToString();
                                                }

                                            }

                                        }

                                    }

                                }
                            }
                            else if (data_grid.CurrentCell.Column.DisplayIndex == 3)
                            {
                                //Selection in choice
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
                                        for (int j = 1, col = 1; j <= No_Column; j++, col++)
                                        {
                                            if (col == 1)
                                            {
                                                if (data[i, (j - 1)] == null)
                                                {
                                                    ChoiceListView[RowIndex].SelectedOperator = null;
                                                }
                                                else
                                                {
                                                    if (data[i, (j - 1)].ToString() == "=" || data[i, (j - 1)].ToString() == "<>")
                                                    {
                                                        ChoiceListView[RowIndex].SelectedOperator = data[i, (j - 1)].ToString();
                                                    }
                                                }

                                            }
                                            else if (col == 2)
                                            {
                                                if (data[i, (j - 1)] == null)
                                                {
                                                    ChoiceListView[RowIndex].Criteria = null;
                                                }
                                                else
                                                {
                                                    ChoiceListView[RowIndex].Criteria = data[i, (j - 1)].ToString();
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
                                                ChoiceListView[RowIndex].Criteria = null;
                                            }
                                            else
                                            {
                                                ChoiceListView[RowIndex].Criteria = data[i, (j - 1)].ToString();
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
                            if (Clipboard.ContainsText(TextDataFormat.UnicodeText))
                            {
                                clipboardText = string.Empty;
                                clipboardText = Clipboard.GetText(TextDataFormat.UnicodeText);
                            }
                            Clipboard.SetText(data[0, 0].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                }
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
                            ChoiceListView[id - 1].AllChoices = null;
                            ChoiceListView[id - 1].SelectedOperator = null;
                            ChoiceListView[id - 1].Criteria = null;
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
                //Removes < column from copying
                e.ClipboardRowContent.RemoveAt(0);
                //Removes id column from copying
                e.ClipboardRowContent.RemoveAt(0);
            }
            catch (Exception ex) { _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException); }
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
                        if (items[0].IsChoiceEmpty == true || items[0].IsCriteriaEmpty == true || items[0].IsOperatorEmpty == true || items[0].IsChoiceInvalid == true || items[0].IsCriteriaInvalid == true)
                        {
                            items[0].IsChoiceEmpty = false;
                            items[0].IsCriteriaEmpty = false;
                            items[0].IsOperatorEmpty = false;
                            items[0].IsChoiceInvalid = false;
                            items[0].IsCriteriaInvalid = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

        }

        /// <summary>Sets the color of the invalid cell.</summary>
        /// <param name="index">The index.</param>
        private void SetInvalidColor(int index)
        {
            ChoiceListView[index].IsCriteriaInvalid = true;
            CollectionViewSource.GetDefaultView(data_grid.ItemsSource).Refresh();

        }
        /// <summary>Sets the BG color of the invalid criteria cell.</summary>
        /// <param name="index">The index.</param>
        private void SetInvalidBGCriteria(int index)
        {
            ChoiceListView[index].IsCriteriaEmpty = true;
            CollectionViewSource.GetDefaultView(data_grid.ItemsSource).Refresh();

        }
        /// <summary>Sets the BG color of the invalid criteria cell.</summary>
        /// <param name="index">The index.</param>
        private void SetInvalidBGOperator(int index)
        {
            ChoiceListView[index].IsOperatorEmpty = true;
            CollectionViewSource.GetDefaultView(data_grid.ItemsSource).Refresh();

        }
        /// <summary>Sets the BG color of the invalid choice cell.</summary>
        /// <param name="index">The index.</param>
        private void SetInvalidBGChoice(int index)
        {
            ChoiceListView[index].IsChoiceEmpty = true;
            CollectionViewSource.GetDefaultView(data_grid.ItemsSource).Refresh();

        }

        private bool CheckSeparator(string str)
        {
            int n = str.Length;
            if (n <= 2)
                return false;
            bool isAdjusentItems = false;

            // Traverse the string 
            for (int i = 0; i < n - 1; i++)

                if (str[i] == ',' || str[i] == '/')
                {
                    // Increment the count if the previous 
                    // and next character is same 
                    if (str[i + 1] == ',' || str[i + 1] == '/')
                    {
                        isAdjusentItems = true;
                        break;
                    }

                }

            return isAdjusentItems;
        }

        private void Cmb_dataGrid_operator_KeyDown(object sender, KeyEventArgs e)
        {
            System.Windows.Controls.ComboBox sen = ((System.Windows.Controls.ComboBox)sender);
            if (Key.Delete == e.Key || Key.Back == e.Key)
            {
                if (sen.SelectedIndex != -1)
                {
                    sen.SelectedIndex = -1;
                }
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
            if (!InitialLoad)
            {
                QuestionSettings qs = dataLoad.Txt_Change_New_Item(txt_new_variable.Text);
                if (qs != null)
                {
                    Text_NewItem_Question.Text = qs.Question;
                    btn_summarizeChoices.IsEnabled = true;
                    if (qs.AnswerType == "MA" || qs.AnswerType == "SA")
                    {
                        answerType.SelectedItem = qs.AnswerType;
                        Text_NewItem_Question.Text = (frmutil.UnEscapeCRLF(qs.TableHeading) + " " + frmutil.UnEscapeCRLF(qs.Question)).TrimEnd().TrimStart();
                        choicesCount.IsEnabled = true;
                        choicesCount.IsEditable = true;
                        choicesCount.SelectedIndex = qs.Choices.Count;
                        for (int i = 0; i < choicesCount.SelectedIndex; i++)
                        {
                            ChoiceListView[i].AllChoices = frmutil.EscapeCRLF(qs.Choices[i]);
                            ChoiceListView[i].SelectedOperator = null;
                            ChoiceListView[i].Criteria = null;
                        }
                        CollectionViewSource.GetDefaultView(data_grid.ItemsSource).Refresh();
                    }
                    if (answerType.Text == "MA")
                    {

                        answerType.SelectedItem = "MA";
                        answerType.IsEnabled = false;
                    }
                    else if (answerType.Text == "SA")
                    {
                        answerType.SelectedItem = "SA";
                        answerType.IsEnabled = true;
                    }

                }
            }
        }
    }
}
