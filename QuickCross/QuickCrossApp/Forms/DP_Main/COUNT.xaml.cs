using ExcelAddIn.Common;
using log4net;
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
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static FilterSettingsView.FilterSettingsClass;
using static Qc4Launcher.Forms.DP_Main.DP_Main;

namespace Qc4Launcher.Forms.DP_Main
{
    /// <summary>
    /// Interaction logic for COUNT.xaml
    /// </summary>
    public partial class COUNT : Window
    {
        public static List<string> MASAvariables = new List<string>();
        QC4Common.Util.FormUtil frmutil = new QC4Common.Util.FormUtil();
        ObservableCollection<ChoiceList> choiceList = new ObservableCollection<ChoiceList>();
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        List<SourceVariableList> repeatsList = new List<SourceVariableList>();
        public bool isRepeatEnabled = false;
        string[] choicesList = new string[201];
        string[] repeatParams = new string[3];
        DataTable dt;
        public static string currentSourceVariable = string.Empty;
        Microsoft.Office.Interop.Excel.Workbook Workbook;
        int ReadRow;
        int WriteRow;
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
        public COUNT(Microsoft.Office.Interop.Excel.Workbook workbook, int readRow, int writeRow, string processingType, string processingOption)
        {
            Workbook = workbook;
            ReadRow = readRow;
            WriteRow = writeRow;
            ProcessingType = processingType;
            ProcessingOption = processingOption;
            InitializeComponent();
            lb_command.Text = Util.Constants.ProcessingMethod.COUNT;
            choicerangetxt.Visibility = Visibility.Hidden;
            choiceRange1.Visibility = Visibility.Hidden;
            choiceRange2.Visibility = Visibility.Hidden;
            choicerangetxt1.Visibility = Visibility.Hidden;
            input_axilary_comb.Visibility = Visibility.Hidden;
            choiceranget.Visibility = Visibility.Hidden;


        }
        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Save.IsEnabled = false;
            NewItemSearchbutton.IsEnabled = false;
            NewItemSearchbutton.Opacity = 0.5;


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
                PopulateCountData(Workbook, ReadRow);
            }

        }

        private void Button_Help_Click_1(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(QC4Common.Classes.Help.GetHelpLink("COUNT"));
        }

        private void MagnifyingGlassButton_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void NewItemSearchbutton_Click(object sender, RoutedEventArgs e)
        {
            if (MagnifyingGlassButton.VariableList != null)
            {
                txt_new_variable.Text = MagnifyingGlassButton.VariableList.Variable;
                if (txt_answerType.Text == "SA")
                {
                    answerType.ItemsSource = MagnifyingGlassButton.VariableList.AswerTypes;
                    answerType.SelectedItem = "SA";
                }
                else
                {

                    answerType.ItemsSource = MagnifyingGlassButton.VariableList.AswerTypes;
                    answerType.SelectedItem = MagnifyingGlassButton.VariableList.AnswerType;
                    answerType.IsEnabled = true;


                }
                Text_NewItem_Question.Text = (frmutil.UnEscapeCRLF(MagnifyingGlassButton.VariableList.Title) + " " + frmutil.UnEscapeCRLF(MagnifyingGlassButton.VariableList.Question)).TrimEnd().TrimStart();
                choicesCount.SelectedIndex = MagnifyingGlassButton.VariableList.Choices.Count;
                for (int i = 0; i < choicesCount.SelectedIndex; i++)
                {
                    ChoiceListView[i].Choices = frmutil.EscapeCRLF(MagnifyingGlassButton.VariableList.Choices[i]);
                    ChoiceListView[i].SelectedOperator = null;
                    ChoiceListView[i].SelectedOperator1 = null;
                    ChoiceListView[i].Criteria = null;
                }
                CollectionViewSource.GetDefaultView(data_grid.ItemsSource).Refresh();
            }
        }
        private void PopulateRecodeData(Microsoft.Office.Interop.Excel.Workbook workbook, int readRow, DataProcessList dpList)
        {


        }
        private void PopulateMASAVariableList()
        {
            var SettingSheet = ExcelUtil.GetWorkSheetByCodeName(Workbook, "Sheet91");
            PopulatedDictionary = Util.Definiotion.VariableDictionary;

            if (SettingSheet != null)
            {
                Microsoft.Office.Interop.Excel.Range Range = SettingSheet.get_Range("List_Item_MA");
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
                                    TableHeadding = frmutil.EscapeCRLF(qs.TableHeading),
                                    Choices = qs.Choices

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
                                            TableHeadding = frmutil.EscapeCRLF(qs.TableHeading),
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
            private string tableheading;
            private string variable;
            private string answertype;
            private string question;
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
            public string qstxtdata
            {
                get
                {
                    return (tableheading + " " + question + LocalResource.LBL_NUMBER_OF_ANSWERS).TrimEnd().TrimStart();
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
            public string TableHeadding
            {
                get
                {
                    return tableheading;
                }
                set
                {
                    tableheading = value;
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
                int itemDigit = GetDigitFromSourceVariable(item.Variable);
                if ((itemDigit == (digit + 1)) && (item.AnswerType == sourceList.AnswerType) && (item.Choices.Count == sourceList.Choices.Count))
                {
                    if (item.Choices.SequenceEqual(sourceList.Choices))
                    {
                        repeatsList.Add(item);
                        SearchExistingSourceVariables(GetDigitFromSourceVariable(item.Variable), item as SourceVariableList);
                    }
                }
            }
        }

        private void ChoicesCount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            ObservableCollection<ChoiceList> cList = new ObservableCollection<ChoiceList>();

            var found = ChoiceListView.LastOrDefault(u => (u.Choices != null) || (u.SelectedOperator != null) || (u.SelectedOperator1 != null) || (u.Criteria != null));
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
                            Operator = lower,
                            Operator1 = upper,
                            Tild = "~",
                            Criteria = null,
                            SelectedOperator = null,
                            SelectedOperator1 = null

                        });
                    }
                }
            }

            data_grid.ItemsSource = ChoiceListView;

        }

        private void SummarizeChoices_Click(object sender, RoutedEventArgs e)
        {


        }


        private string[,] GetRecodeSaveArray(int columncount, string[] param, string[] for_entries, string[] recode_entries)
        {
            return null;

        }

        private void Btn_RightArrow_Click(object sender, RoutedEventArgs e)
        {


        }
        public void AnswertypeLoad()
        {
            List<string> sourceAnswerTypes;
            sourceAnswerTypes = new List<string>();
            sourceAnswerTypes.Add("SA");
            sourceAnswerTypes.Add("N");
            answerType.ItemsSource = sourceAnswerTypes;
            answerType.SelectedIndex = 0;
        }
        bool firstLoad = true;
        private void Combo_sourceVariable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Windows.Controls.ComboBox sourceVariable = ((System.Windows.Controls.ComboBox)sender);
            currentSourceVariable = Combo_sourceVariable.Text;
            if (firstLoad)
            {
                AnswertypeLoad();
            }


            firstLoad = false;
            var x = sourceVariable.SelectedItem;
            if (ProcessingOption != QC4Common.Common.Constants.STD_DP.Process_Edit)
            {
                if (sourceVariable.SelectedItem != null)
                {
                    choicerangetxt.Visibility = Visibility.Visible;
                    choiceRange1.Visibility = Visibility.Visible;
                    choiceRange2.Visibility = Visibility.Visible;
                    choicerangetxt1.Visibility = Visibility.Visible;
                    if (answerType.Text == "SA")
                    {
                        input_axilary_comb.Visibility = Visibility.Visible;
                        choiceranget.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        input_axilary_comb.Visibility = Visibility.Hidden;
                        choiceranget.Visibility = Visibility.Hidden;
                    }

                    choicesCount.IsEditable = true;
                    choicesCount.IsEnabled = true;
                    txt_answerType.IsEnabled = true;
                    txt_answerTypeWithChoiceCount.IsEnabled = true;
                    txt_source_Question.IsEnabled = true;
                    txt_new_variable.IsEnabled = true;
                    choicebtn.IsEnabled = true;
                    txt_new_variable.Background = Brushes.White;
                    answerType.IsEnabled = true;
                    Text_NewItem_Question.IsEnabled = true;
                    Text_NewItem_Question.Background = Brushes.White;
                    choices_grid.IsEnabled = true;
                    choices_grid.Background = Brushes.White;

                    SourceVariableList list = new SourceVariableList();
                    list = (SourceVariableList)(sourceVariable.SelectedItem);

                    //Buttons are enabled
                    NewItemSearchbutton.IsEnabled = true;
                    NewItemSearchbutton.Opacity = 1;
                    Save.IsEnabled = true;



                    int digit = GetDigitFromSourceVariable(list.Variable);
                    repeatsList.Clear();
                    if (digit >= 0)
                    {
                        SearchExistingSourceVariables(digit, list);
                    }


                    txt_answerType.Text = list.AnswerType;
                    txt_answerTypeWithChoiceCount.Text = (list.Choices.Count).ToString();
                    txt_source_Question.Text = frmutil.UnEscapeCRLF(list.Question);
                    lower = new List<string>();
                    upper = new List<string>();
                    CommonChoice = new List<string>();
                    axilaryDrop = new List<string>();
                    for (int i = 0; i < list.Choices.Count + 1; i++)
                    {

                        lower.Add((i).ToString());
                        upper.Add((i).ToString());

                    }
                    for (int i = 0; i < list.Choices.Count; i++)
                    {
                        list.Choices[i] = frmutil.EscapeCRLF(list.Choices[i]);
                        CommonChoice.Add((i + 1).ToString());
                        if (i <= 9)
                        {
                            axilaryDrop.Add(string.Format(LocalResource.LBL_ONE_BY_ONE, (i + 1)));
                        }
                    }
                    for (int i = 0; i < ChoiceListView.Count; i++)
                    {
                        ChoiceListView[i].Operator = lower;
                        ChoiceListView[i].Operator1 = upper;
                    }
                    choiceRange1.ItemsSource = CommonChoice;
                    choiceRange2.ItemsSource = CommonChoice;
                    input_axilary_comb.ItemsSource = axilaryDrop;
                    choiceRange1.SelectedIndex = 0;
                    choiceRange2.SelectedIndex = CommonChoice.Count - 1;
                    data_grid.ItemsSource = ChoiceListView;

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
                        newRow["Choice"] = item;

                        dataTable.Rows.Add(newRow);
                    }

                    choices_grid.ItemsSource = dataTable.DefaultView;

                    QC4Common.Util.QSUtil qsutil = new QC4Common.Util.QSUtil();
                    txt_new_variable.Text = qsutil.GetVariableName(list.Variable, PopulatedDictionary.Values.ToList());


                    Text_NewItem_Question.Text = frmutil.UnEscapeCRLF(list.qstxtdata);


                }
                else
                {
                    Combo_sourceVariable.Text = "";
                    txt_answerType.Text = "";
                    txt_answerTypeWithChoiceCount.Text = "";
                    txt_source_Question.Text = "";
                    txt_new_variable.Text = "";
                  //  answerType.SelectedIndex = -1;
                    input_axilary_comb.ItemsSource = null;
                    Text_NewItem_Question.Text = "";
                    choicesCount.SelectedIndex = 0;
                    Save.IsEnabled = false;
                    NewItemSearchbutton.IsEnabled = false;
                    NewItemSearchbutton.Opacity = 0.5;
                    choices_grid.ItemsSource = null;
                }
            }
            else
            {
                if (sourceVariable.SelectedItem != null)
                {
                    choicerangetxt.Visibility = Visibility.Visible;
                    choiceRange1.Visibility = Visibility.Visible;
                    choiceRange2.Visibility = Visibility.Visible;
                    choicerangetxt1.Visibility = Visibility.Visible;
                    input_axilary_comb.Visibility = Visibility.Hidden;
                    choiceranget.Visibility = Visibility.Hidden;
                    choicesCount.IsEditable = true;
                    choicesCount.IsEnabled = true;
                    txt_answerType.IsEnabled = true;
                    txt_source_Question.IsEnabled = true;
                    txt_new_variable.IsEnabled = true;
                    answerType.IsEnabled = false;
                    txt_answerTypeWithChoiceCount.IsEnabled = true;
                    Text_NewItem_Question.IsEnabled = true;
                    Text_NewItem_Question.Background = Brushes.White;
                    Save.IsEnabled = true;
                    choices_grid.IsEnabled = true;
                    choices_grid.Background = Brushes.White;
                    choicebtn.IsEnabled = true;

                    SourceVariableList list = new SourceVariableList();
                    list = (SourceVariableList)(sourceVariable.SelectedItem);
                    txt_answerType.Text = list.AnswerType;
                    txt_source_Question.Text = frmutil.UnEscapeCRLF(list.Question);
                    txt_answerTypeWithChoiceCount.Text = (list.Choices.Count).ToString();

                    lower = new List<string>();
                    upper = new List<string>();
                    CommonChoice = new List<string>();
                    axilaryDrop = new List<string>();
                    for (int i = 0; i < list.Choices.Count + 1; i++)
                    {

                        lower.Add((i).ToString());
                        upper.Add((i).ToString());

                    }
                    for (int i = 0; i < list.Choices.Count; i++)
                    {
                        list.Choices[i] = frmutil.EscapeCRLF(list.Choices[i]);
                        CommonChoice.Add((i + 1).ToString());
                    }
                    for (int i = 0; i < ChoiceListView.Count; i++)
                    {
                        ChoiceListView[i].Operator = lower;
                        ChoiceListView[i].Operator1 = upper;
                    }

                    choiceRange1.ItemsSource = CommonChoice;
                    choiceRange2.ItemsSource = CommonChoice;
                    choiceRange1.SelectedIndex = 0;
                    choiceRange2.SelectedIndex = CommonChoice.Count - 1;

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
                        newRow["Choice"] = item;

                        dataTable.Rows.Add(newRow);
                    }

                    choices_grid.ItemsSource = dataTable.DefaultView;

                }
            }


        }
        public List<string> lower;
        public List<string> upper;
        public List<string> CommonChoice;
        public List<string> axilaryDrop;
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
            private string choices;
            private List<string> op;
            private List<string> op1;
            public string tild;
            private string selectedOperator;
            private string selectedOperator1;
            private bool isChoiceEmpty = false;
            private bool isLowerEmpty = false;
            private bool isUpperEmpty = false;
            private bool isLowerInvalid = false;
            private bool isUpperInvalid = false;
            public string Tild
            {
                get { return tild; }
                set { tild = value; }
            }
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
            public bool IsChoiceEmpty
            {
                get
                {
                    return isChoiceEmpty;
                }
                set
                {
                    isChoiceEmpty = value;
                    RaisePropertyChanged();
                }
            }
            public bool IsLowerEmpty
            {
                get
                {
                    return isLowerEmpty;
                }
                set
                {
                    isLowerEmpty = value;
                    RaisePropertyChanged();
                }
            }
            public bool IsUpperEmpty
            {
                get
                {
                    return isUpperEmpty;
                }
                set
                {
                    isUpperEmpty = value;
                    RaisePropertyChanged();
                }
            }
            public bool IsLowerInvalid
            {
                get
                {
                    return isLowerInvalid;
                }
                set
                {
                    isLowerInvalid = value;
                    RaisePropertyChanged();
                }
            }
            public bool IsUpperInvalid
            {
                get
                {
                    return isUpperInvalid;
                }
                set
                {
                    isUpperInvalid = value;
                    RaisePropertyChanged();
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
            public string Choices
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

                    return op;
                }
                set
                {
                    op = value;
                    RaisePropertyChanged();
                }
            }
            public List<string> Operator1
            {
                get
                {

                    return op1;
                }
                set
                {
                    op1 = value;
                    RaisePropertyChanged();
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
            public string SelectedOperator1
            {
                get
                {
                    return selectedOperator1;
                }
                set
                {
                    selectedOperator1 = value;
                }
            }
            public event PropertyChangedEventHandler PropertyChanged;
            protected void RaisePropertyChanged([CallerMemberName]string propertyName = "")
            {
                var handler = PropertyChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(propertyName));
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
                        Tild = "~",
                        Operator = lower,
                        Operator1 = upper

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
        private static string GenerateVariableNameWith_K(string varible, List<QC4Common.Model.QuestionSettings> variableList, int times = 0)
        {
            String str = "K" + (times == 0 ? "" : times.ToString()) + varible;
            if (variableList.Any(q => q.Variable.Equals(str)))
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

        }

        private void CheckBox_from_Nq1_Unchecked(object sender, RoutedEventArgs e)
        {

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

        private void Choices_grid_CurrentCellChanged(object sender, EventArgs e)
        {
            var dg = (System.Windows.Controls.DataGrid)sender;
            ExpGrid = dg;
            dg.Focus();
        }

        private void Data_grid_LoadingRow_2(object sender, DataGridRowEventArgs e)
        {

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

        private void Command_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AnswerType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string ch = answerType.SelectedItem.ToString();
                switch (ch)
                {
                    case "SA":
                        data_grid.Visibility = Visibility.Visible;
                        choicesCount.Visibility = Visibility.Visible;
                        txtach.Visibility = Visibility.Visible;
                        choicebtn.Visibility = Visibility.Visible;
                        input_axilary_comb.Visibility = Visibility.Visible;
                        choiceranget.Visibility = Visibility.Visible;
                        break;
                    case "N":
                        data_grid.Visibility = Visibility.Hidden;
                        choicesCount.Visibility = Visibility.Hidden;
                        txtach.Visibility = Visibility.Hidden;
                        choicebtn.Visibility = Visibility.Hidden;
                        input_axilary_comb.Visibility = Visibility.Hidden;
                        choiceranget.Visibility = Visibility.Hidden;
                        break;
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void Data_grid_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            DependencyObject DepObject = (DependencyObject)e.OriginalSource;

            while ((DepObject != null) && !(DepObject is DataGridColumnHeader))
            {
                DepObject = VisualTreeHelper.GetParent(DepObject);
            }

            if (DepObject == null)
            {
                return;
            }

            if (DepObject is DataGridColumnHeader)
            {
                data_grid.ContextMenu.Visibility = Visibility.Collapsed;
            }
            else
            {
                data_grid.ContextMenu.Visibility = Visibility.Visible;
            }
        }

        public void axilaryvaludatagrid(int roundoff, int casedata, int balance)
        {
            try
            {
                for (int i = 0; i < ChoiceListView.Count; i++)
                {
                    ChoiceListView[i].Choices = null;
                    ChoiceListView[i].SelectedOperator = null;
                    ChoiceListView[i].SelectedOperator1 = null;
                }
                for (int i = 0; i <= roundoff; i++)
                {
                    if (i == 0)
                    {
                        ChoiceListView[i].Choices = i.ToString() + LocalResource.LBL_CHOICE_LABEL_PIECES;
                        ChoiceListView[i].SelectedOperator = i.ToString();
                        ChoiceListView[i].SelectedOperator1 = i.ToString();

                    }
                    else if (ChoiceListView.Count > roundoff)
                    {
                        if (i == 1)
                        {

                            ChoiceListView[i].SelectedOperator = i.ToString();
                            ChoiceListView[i].SelectedOperator1 = (i + casedata).ToString();
                            if (ChoiceListView[i].SelectedOperator == ChoiceListView[i].SelectedOperator1)
                            {
                                ChoiceListView[i].Choices = i.ToString() + LocalResource.LBL_CHOICE_LABEL_PIECES; ;
                            }
                            else
                            {
                                ChoiceListView[i].Choices = i.ToString() + LocalResource.LBL_TILDE + (i + casedata).ToString() + LocalResource.LBL_CHOICE_LABEL_PIECES;
                            }
                        }
                        else
                        {
                            ChoiceListView[i].SelectedOperator = (Convert.ToDouble(ChoiceListView[(i - 1)].SelectedOperator1) + 1).ToString();
                            ChoiceListView[i].SelectedOperator1 = (Convert.ToDouble(ChoiceListView[(i)].SelectedOperator) + casedata).ToString();
                            if (ChoiceListView[i].SelectedOperator == ChoiceListView[i].SelectedOperator1)
                            {
                                ChoiceListView[i].Choices = (Convert.ToDouble(ChoiceListView[(i)].SelectedOperator1)).ToString() + LocalResource.LBL_CHOICE_LABEL_PIECES;
                            }
                            else
                            {
                                ChoiceListView[i].Choices = (Convert.ToDouble(ChoiceListView[(i - 1)].SelectedOperator1) + 1).ToString() + LocalResource.LBL_TILDE + (Convert.ToDouble(ChoiceListView[(i)].SelectedOperator) + casedata).ToString() + LocalResource.LBL_CHOICE_LABEL_PIECES;

                            }

                        }
                    }
                    else
                    {
                        if (i >= (ChoiceListView.Count))
                        {
                            break;
                        }
                        if (i == 1)
                        {

                            ChoiceListView[i].SelectedOperator = i.ToString();
                            ChoiceListView[i].SelectedOperator1 = (i + casedata).ToString();
                            if (ChoiceListView[i].SelectedOperator == ChoiceListView[i].SelectedOperator1)
                            {
                                ChoiceListView[i].Choices = i.ToString() + LocalResource.LBL_CHOICE_LABEL_PIECES;
                            }
                            else
                            {
                                ChoiceListView[i].Choices = i.ToString() + LocalResource.LBL_TILDE + (i + casedata).ToString() + LocalResource.LBL_CHOICE_LABEL_PIECES;
                            }

                        }
                        else
                        {
                            ChoiceListView[i].SelectedOperator = (Convert.ToDouble(ChoiceListView[(i - 1)].SelectedOperator1) + 1).ToString();
                            ChoiceListView[i].SelectedOperator1 = (Convert.ToDouble(ChoiceListView[(i)].SelectedOperator) + casedata).ToString();
                            if (ChoiceListView[i].SelectedOperator == ChoiceListView[i].SelectedOperator1)
                            {
                                ChoiceListView[i].Choices = (Convert.ToDouble(ChoiceListView[(i)].SelectedOperator1)).ToString() + LocalResource.LBL_CHOICE_LABEL_PIECES;
                            }
                            else
                            {
                                ChoiceListView[i].Choices = (Convert.ToDouble(ChoiceListView[(i - 1)].SelectedOperator1) + 1).ToString() + LocalResource.LBL_TILDE + (Convert.ToDouble(ChoiceListView[(i)].SelectedOperator) + casedata).ToString() + LocalResource.LBL_CHOICE_LABEL_PIECES;
                            }

                        }
                    }
                }
                var found = ChoiceListView.LastOrDefault(u => (u.Choices != null) || (u.SelectedOperator != null) || (u.SelectedOperator1 != null));
                int foundIndex = ChoiceListView.IndexOf(found);
                if (balance > 0)
                {
                    int i = (foundIndex + 1);
                    if (i < ChoiceListView.Count)
                    {
                        if (balance == 1)
                        {

                            ChoiceListView[i].SelectedOperator = (Convert.ToDouble(ChoiceListView[(i - 1)].SelectedOperator1) + 1).ToString();
                            ChoiceListView[i].SelectedOperator1 = (Convert.ToDouble(ChoiceListView[(i - 1)].SelectedOperator1) + 1).ToString();
                            ChoiceListView[i].Choices = (Convert.ToDouble(ChoiceListView[(i - 1)].SelectedOperator1) + 1).ToString() + LocalResource.LBL_CHOICE_LABEL_PIECES;

                        }
                        else
                        {
                            ChoiceListView[i].SelectedOperator = (Convert.ToDouble(ChoiceListView[(i - 1)].SelectedOperator1) + 1).ToString();
                            ChoiceListView[i].SelectedOperator1 = (Convert.ToDouble((txt_answerTypeWithChoiceCount.Text).TrimEnd().TrimStart())).ToString();
                            ChoiceListView[i].Choices = (Convert.ToDouble(ChoiceListView[(i - 1)].SelectedOperator1) + 1).ToString() + LocalResource.LBL_TILDE + (Convert.ToDouble((txt_answerTypeWithChoiceCount.Text).TrimEnd().TrimStart())).ToString() + LocalResource.LBL_CHOICE_LABEL_PIECES;

                        }
                    }

                }

            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }
        private void Input_axilary_comb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Windows.Controls.ComboBox sourceVariable = ((System.Windows.Controls.ComboBox)sender);
            var x = sourceVariable.SelectedItem;
            if (x != null)
            {
                int selectediIndex = sourceVariable.SelectedIndex;
                SourceVariableList list = new SourceVariableList();
                list = (SourceVariableList)(Combo_sourceVariable.SelectedItem);
                if (list.Choices.Count > 0)
                {
                    int roundoff = (list.Choices.Count) / (selectediIndex + 1);
                    int balance = (list.Choices.Count) % (selectediIndex + 1);
                    switch (selectediIndex)
                    {
                        case 0:
                            axilaryvaludatagrid(roundoff, 0, balance);

                            break;
                        case 1:
                            axilaryvaludatagrid(roundoff, 1, balance);
                            break;
                        case 2:
                            axilaryvaludatagrid(roundoff, 2, balance);
                            break;
                        case 3:
                            axilaryvaludatagrid(roundoff, 3, balance);
                            break;
                        case 4:
                            axilaryvaludatagrid(roundoff, 4, balance);
                            break;
                        case 5:
                            axilaryvaludatagrid(roundoff, 5, balance);
                            break;
                        case 6:
                            axilaryvaludatagrid(roundoff, 6, balance);
                            break;
                        case 7:
                            axilaryvaludatagrid(roundoff, 7, balance);
                            break;
                        case 8:
                            axilaryvaludatagrid(roundoff, 8, balance);
                            break;
                        case 9:
                            axilaryvaludatagrid(roundoff, 9, balance);
                            break;

                        default:
                            for (int i = 0; i < ChoiceListView.Count; i++)
                            {
                                ChoiceListView[i].Choices = null;
                                ChoiceListView[i].SelectedOperator = null;
                                ChoiceListView[i].SelectedOperator1 = null;
                            }
                            break;
                    }
                }
            }
            data_grid.ItemsSource = ChoiceListView;
            CollectionViewSource.GetDefaultView(data_grid.ItemsSource).Refresh();
        }

        private void Choicebtn_Click(object sender, RoutedEventArgs e)
        {
            var found = ChoiceListView.LastOrDefault(u => (u.Choices != null) || (u.SelectedOperator != null) || (u.SelectedOperator1 != null));
            int foundIndex = ChoiceListView.IndexOf(found);
            if (found != null)
            {

                if (!ValidateCountData())
                {
                    return;
                }


                for (int i = 0; i <= foundIndex; i++)
                {
                    if (ChoiceListView[i].SelectedOperator == null || ChoiceListView[i].SelectedOperator == "" || ChoiceListView[i].SelectedOperator1 == null || ChoiceListView[i].SelectedOperator1 == "")
                    {
                        if (ChoiceListView[i].SelectedOperator == null && ChoiceListView[i].SelectedOperator1 == null)
                        {
                            ChoiceListView[i].Choices = ChoiceListView[i].Choices;
                        }
                        else
                        {
                            if (ChoiceListView[i].SelectedOperator == null || ChoiceListView[i].SelectedOperator == "" && ChoiceListView[i].SelectedOperator1 != null)
                            {
                                ChoiceListView[i].Choices = LocalResource.LBL_TILDE + ChoiceListView[i].SelectedOperator1 + LocalResource.LBL_CHOICE_LABEL_PIECES;
                            }
                            if (ChoiceListView[i].SelectedOperator1 == null || ChoiceListView[i].SelectedOperator1 == "" && ChoiceListView[i].SelectedOperator != null)
                            {
                                ChoiceListView[i].Choices = ChoiceListView[i].SelectedOperator + LocalResource.LBL_CHOICE_LABEL_PIECES + LocalResource.LBL_TILDE;
                            }
                        }

                    }
                    else
                    {
                        for (int j = i; j <= foundIndex; j++)
                        {

                            if (i != j)
                            {
                                if (ChoiceListView[j].SelectedOperator != null)
                                {
                                    if (Convert.ToDouble(ChoiceListView[i].SelectedOperator) >= Convert.ToDouble(ChoiceListView[j].SelectedOperator))
                                    {
                                        ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, ((j + 1).ToString())) + "\n" + (LocalResource.ERR_MSG_COUNT_RANGE_OF_CHOICES_ARE_OVERLAPPED));
                                        SetInvalidColorLowerLimit(j);
                                        SetInvalidColorUpperLimit(j);
                                        return;
                                    }
                                    if (Convert.ToDouble(ChoiceListView[i].SelectedOperator1) >= Convert.ToDouble(ChoiceListView[j].SelectedOperator))
                                    {
                                        ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, ((j + 1).ToString())) + "\n" + (LocalResource.ERR_MSG_COUNT_RANGE_OF_CHOICES_ARE_OVERLAPPED));
                                        SetInvalidColorLowerLimit(j);
                                        SetInvalidColorUpperLimit(j);
                                        return;
                                    }
                                }

                            }
                        }
                        if (ChoiceListView[i].SelectedOperator == ChoiceListView[i].SelectedOperator1)
                        {
                            ChoiceListView[i].Choices = ChoiceListView[i].SelectedOperator + LocalResource.LBL_CHOICE_LABEL_PIECES;
                        }
                        else
                        {
                            ChoiceListView[i].Choices = ChoiceListView[i].SelectedOperator + LocalResource.LBL_TILDE + ChoiceListView[i].SelectedOperator1 + LocalResource.LBL_CHOICE_LABEL_PIECES;
                        }

                    }
                }
            }
            CollectionViewSource.GetDefaultView(data_grid.ItemsSource).Refresh();
        }

        private bool ValidateCountData()
        {
            bool isValid = true;
            List<string> limitValues = new List<string>();
            var found = ChoiceListView.LastOrDefault(u => (u.Choices != null) || (u.SelectedOperator != null) || (u.SelectedOperator1 != null));
            int foundIndex = ChoiceListView.IndexOf(found);
            if (choiceRange1.SelectedIndex > choiceRange2.SelectedIndex)
            {
                ExcelAddIn.Common.MessageDialog.ErrorOk(LocalResource.ERR_MSG_COUNT_START_LESS_THAN_RIGHT);
                isValid = false;
                return isValid;
            }
            for (int i = 0; i <= foundIndex; i++)
            {
                string cellChoice = Convert.ToString(ChoiceListView[i].Choices);
                string cellvalue1 = Convert.ToString(ChoiceListView[i].SelectedOperator);
                string cellvalue2 = Convert.ToString(ChoiceListView[i].SelectedOperator1);
                if (cellvalue1 == null && cellvalue2 == "0")
                {
                    cellvalue1 = "0";
                }
                if (cellvalue1 != null || cellvalue2 != null)
                {
                    limitValues.Add(cellvalue1 + "-" + cellvalue2);
                }
                if (cellvalue1 != null && cellvalue2 != null)
                {
                    if (!frmutil.IsNumeric(cellvalue1) || !frmutil.IsNumeric(cellvalue2))
                    {
                        ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, ((i + 1).ToString())) + "\n" + (LocalResource.ERR_MSG_COUNT_RANGE_OF_CHOICES_ARE_OVERLAPPED));
                        if (!frmutil.IsNumeric(cellvalue1))
                        {
                            SetInvalidColorLowerLimit(i);
                        }
                        if (!frmutil.IsNumeric(cellvalue2))
                        {
                            SetInvalidColorUpperLimit(i);
                        }
                        isValid = false;
                        return isValid;
                    }
                    if (!frmutil.IsLowerLimitLesser(cellvalue1, cellvalue2))
                    {
                        ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, ((i + 1).ToString())) + "\n" + (LocalResource.ERR_MGS_COUNT_SET_VALUE_LOWER_LIMIT_LESS_THAN_UPPER_LIMIT));
                        SetInvalidColorLowerLimit(i);
                        isValid = false;
                        return isValid;
                    }

                }

                if (i >= 1)
                {
                    if (frmutil.IsOverlap(limitValues, choices_grid.Items.Count))
                    {
                        ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, ((i + 1).ToString())) + "\n" + (LocalResource.ERR_MSG_COUNT_RANGE_OF_CHOICES_ARE_OVERLAPPED));
                        SetInvalidColorLowerLimit(i);
                        SetInvalidColorUpperLimit(i);
                        isValid = false;
                        return isValid;
                    }
                    if (limitValues.Count>0 && limitValues[0] == "0-0")
                    {
                        if (cellvalue1 == null && cellvalue2 != null)
                        {
                            ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, ((i + 1).ToString())) + "\n" + (LocalResource.ERR_MSG_COUNT_RANGE_OF_CHOICES_ARE_OVERLAPPED));
                            SetInvalidColorLowerLimit(i);
                            SetInvalidColorUpperLimit(i);
                            isValid = false;
                            return isValid;
                        }
                    }

                }

            }

            return isValid;
        }
        private bool ValidateDataSave()
        {
            bool isValid = true;
            int AllEmptyCount = 0;
            List<string> limitValues = new List<string>();
            var found = ChoiceListView.LastOrDefault(u => (!string.IsNullOrEmpty(u.Choices)) || (!string.IsNullOrEmpty(u.SelectedOperator)) || (!string.IsNullOrEmpty(u.SelectedOperator1)));
            int foundIndex = ChoiceListView.IndexOf(found);
            int k = 0;
            if (found == null && choicesCount.SelectedIndex == 0)
            {
                MessageDialog.ErrorOk(LocalResource.ADD_DP_RECODE_ERROR_MSG_NO_CHOICE_SET);
                isValid = false;
                return isValid;
            }
            else if (found != null && choicesCount.SelectedIndex == 0)
            {
                AllEmptyCount = 0;
                for (int i = 0; i <= foundIndex; i++)
                {
                    if (ChoiceListView[i].Choices == null || ChoiceListView[i].Choices.TrimEnd().TrimStart() == "")
                    {
                        AllEmptyCount++;
                    }
                    if (AllEmptyCount == foundIndex + 1)
                    {
                        MessageDialog.ErrorOk(LocalResource.ADD_DP_RECODE_ERROR_MSG_NO_CHOICE_SET);
                        isValid = false;
                        return isValid;
                    }
                }
            }
            else if (found == null && choicesCount.SelectedIndex != 0)
            {
                MessageDialog.ErrorOk(LocalResource.ADD_DP_RECODE_ERROR_MSG_NO_MATCH_NUMBER_OF_CHOICES);
                isValid = false;
                return isValid;
            }
            else if (found != null && choicesCount.SelectedIndex != 0 && (foundIndex + 1) < choicesCount.SelectedIndex)
            {
                MessageDialog.ErrorOk(LocalResource.ADD_DP_RECODE_ERROR_MSG_NO_MATCH_NUMBER_OF_CHOICES);
                isValid = false;
                return isValid;
            }
            else if (found != null && choicesCount.SelectedIndex != 0)
            {
                AllEmptyCount = 0;
                foreach (var element in data_grid.ItemsSource)
                {
                    if ((element as ChoiceList).Choices == null || (element as ChoiceList).Choices.TrimEnd().TrimStart() == "")
                    {
                        AllEmptyCount++;
                    }
                }
                if (AllEmptyCount == choicesCount.SelectedIndex)
                {
                    MessageDialog.ErrorOk(LocalResource.ADD_DP_RECODE_ERROR_MSG_NO_MATCH_NUMBER_OF_CHOICES);
                    isValid = false;
                    return isValid;
                }
            }
 
            if (choiceRange1.SelectedIndex > choiceRange2.SelectedIndex)
            {
                ExcelAddIn.Common.MessageDialog.ErrorOk(LocalResource.ERR_MSG_COUNT_START_LESS_THAN_RIGHT);
                isValid = false;
                return isValid;
            }
            for (int i = 0; i <= foundIndex; i++)
            {
                string cellChoice = Convert.ToString(ChoiceListView[i].Choices);
                string cellvalue1 = Convert.ToString(ChoiceListView[i].SelectedOperator);
                string cellvalue2 = Convert.ToString(ChoiceListView[i].SelectedOperator1);

                if (cellChoice == null || cellChoice.TrimStart().TrimEnd() == "")
                {
                    MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, (i + 1)) + "\n" + (LocalResource.ERR_MSG_RECODE_CHOICE_IS_NOT_INPUT));
                    SetInvalidBGChoice(i);
                    isValid = false;
                    return isValid;
                }
                if (cellvalue1 != null && cellvalue2 != null)
                {
                    if (!frmutil.IsLowerLimitLesser(cellvalue1, cellvalue2))
                    {
                        ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, ((i + 1).ToString())) + "\n" + (LocalResource.ERR_MGS_COUNT_SET_VALUE_LOWER_LIMIT_LESS_THAN_UPPER_LIMIT));
                        SetInvalidColorLowerLimit(i);
                        isValid = false;
                        return isValid;
                    }
                }
                else if (cellvalue1 == null && cellvalue2 == null)
                {
                    ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_CLASS_INPUT_EITHER_LOWER_OR_UPPER_LIMIT));
                    SetInvalidBGLowerLimit(i);
                    SetInvalidBGUpperLimit(i);
                    isValid = false;
                    return isValid;
                }
                if (cellvalue1== null && cellvalue2 == "0")
                {
                    cellvalue1 = "0";
                }
                limitValues.Add(cellvalue1 + "-" + cellvalue2);
                if (i >= 1)
                {
                    if (frmutil.IsOverlap(limitValues, choices_grid.Items.Count))
                    {
                        ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, ((i + 1).ToString())) + "\n" + (LocalResource.ERR_MSG_COUNT_RANGE_OF_CHOICES_ARE_OVERLAPPED));
                        SetInvalidColorLowerLimit(i);
                        SetInvalidColorUpperLimit(i);
                        isValid = false;
                        return isValid;
                    }
                    if (limitValues.Count > 0 && limitValues[0] == "0-0")
                    {
                        if (cellvalue1 == null && cellvalue2 != null)
                        {
                            ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, ((i + 1).ToString())) + "\n" + (LocalResource.ERR_MSG_COUNT_RANGE_OF_CHOICES_ARE_OVERLAPPED));
                            SetInvalidColorLowerLimit(i);
                            SetInvalidColorUpperLimit(i);
                            isValid = false;
                            return isValid;
                        }
                    }

                }
            }
            return isValid;
        }
        private void DisableRightMenu(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
        private void Combo_sourceVariable_GotFocus_1(object sender, RoutedEventArgs e)
        {

        }
        private void PopulateCountData(Microsoft.Office.Interop.Excel.Workbook workbook, int readRow)
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
                            for (int i = 1; i <= qs.Choices.Count; i++)
                            {
                                Limts.Add(obj[1, 11 + 1 + i]);
                            }

                        }
                        SourceVariableList found = SourceVariableListView.FirstOrDefault(u => (u.Variable == EDIT_OR_COPY_Process_Details.Variable));
                        Combo_sourceVariable.SelectedIndex = SourceVariableListView.IndexOf(found);
                        string[] splittedChoiceRangeValues = obj[1, 12].Split('-');
                        choiceRange1.SelectedIndex = int.Parse(splittedChoiceRangeValues[0]) - 1;
                        choiceRange2.SelectedIndex = int.Parse(splittedChoiceRangeValues[1]) - 1;

                        if (ProcessingOption == QC4Common.Common.Constants.STD_DP.Process_Edit)
                        {
                            Color newVariableTextboxColor = (Color)ColorConverter.ConvertFromString("#F0F0F0");
                            Color editProcessMethodColor = (Color)ColorConverter.ConvertFromString("#FFADADAD");

                            Textbox_ProcessMethod.Background = new System.Windows.Media.SolidColorBrush(newVariableTextboxColor);
                            Textbox_ProcessMethod.Foreground = new System.Windows.Media.SolidColorBrush(editProcessMethodColor);
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
                            NewItemSearchbutton.Opacity = 0.5;
                            answerType.IsEnabled = false;
                            choicesCount.IsEditable = false;
                            choicesCount.IsEnabled = false;
                            choicesCount.Background = new System.Windows.Media.SolidColorBrush(newVariableTextboxColor);

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
                        if (qs != null)
                        {
                            answerType.Text = qs.AnswerType;
                            choicesCount.SelectedIndex = qs.Choices.Count;
                            Text_NewItem_Question.Text = qs.Question;
                            for (int i = 0; i < qs.Choices.Count; i++)
                            {
                                ChoiceListView[i].Choices = frmutil.EscapeCRLF(qs.Choices[i]);
                                if (Limts[i] != null)
                                {
                                    if (Limts[i].IndexOf("-") == 0)
                                    {
                                        ChoiceListView[i].SelectedOperator = null;
                                        ChoiceListView[i].SelectedOperator1 = Limts[i].Remove(0, 1).Replace("(", string.Empty).Replace(")", string.Empty);
                                    }
                                    else if (Limts[i].LastIndexOf("-") == Limts[i].Length - 1)
                                    {
                                        ChoiceListView[i].SelectedOperator1 = null;
                                        ChoiceListView[i].SelectedOperator = Limts[i].Remove((Limts[i].Length - 1), 1).Replace("(", string.Empty).Replace(")", string.Empty);
                                    }
                                    else
                                    {
                                        int count = Limts[i].ToCharArray().Count(c => c == '-');
                                        if (count == 1)
                                        {
                                            string[] splittedValues = Limts[i].Split('-');
                                            ChoiceListView[i].SelectedOperator = splittedValues[0];
                                            ChoiceListView[i].SelectedOperator1 = splittedValues[1];
                                        }

                                    }
                                }
                                else
                                {
                                    ChoiceListView[i].SelectedOperator = null;
                                    ChoiceListView[i].SelectedOperator1 = null;
                                }

                            }
                        }
                        else
                        {
                            choicesCount.SelectedIndex = 0;
                            Text_NewItem_Question.Text = string.Empty;
                        }


                        // Assign to temp variable
                        newVariableBeforeEdit = txt_new_variable.Text;
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
                string ch = answerType.SelectedItem.ToString();

                // Validates count data before save.
                if (ch != "N" && !ValidateDataSave())
                {
                    return;
                }
                if (ch == "N")
                {
                    if (choiceRange1.SelectedIndex > choiceRange2.SelectedIndex)
                    {
                        ExcelAddIn.Common.MessageDialog.ErrorOk(LocalResource.ERR_MSG_COUNT_START_LESS_THAN_RIGHT);
                        return;
                    }
                }
                string newVariable = ((txt_new_variable.Text).TrimStart().TrimEnd());
                string newQuestion = ((Text_NewItem_Question.Text).TrimStart().TrimEnd());

                if (newQuestion.Length > QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit)
                {
                    newQuestion = newQuestion.PadRight(QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit).Substring(0, QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit);
                }

                QC4Common.Util.QSUtil qsutil = new QC4Common.Util.QSUtil();
                Logic.DataProcessHelper dphelper = new Logic.DataProcessHelper();

                var found = ChoiceListView.LastOrDefault(u => (!string.IsNullOrEmpty(u.Choices)) || (!string.IsNullOrEmpty(u.SelectedOperator)) || (!string.IsNullOrEmpty(u.SelectedOperator1)));

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

                // Check whether the new variable name already exists in VariableDictionary
                if (ProcessingOption != QC4Common.Common.Constants.STD_DP.Process_Edit && (!frmutil.IsVariableNameExists(newVariable, PopulatedDictionary.Values.ToList(), 1) || string.IsNullOrEmpty(newVariable)))
                {
                    string errormsg = LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_NEW_ITEM_EMPTY + "\n" + LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_RENAME_NEW_VARIABLE_AUTOMATICALLY;
                    if (!string.IsNullOrEmpty(newVariable))
                    {
                        errormsg = LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_ITEM_NAME_ALREADY_EXISTS + "\n" + LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_RENAME_NEW_VARIABLE_AUTOMATICALLY;
                    }
                    System.Windows.Forms.DialogResult result;
                    result = MessageDialog.InfoYesNo(errormsg);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (!string.IsNullOrEmpty(newVariable))
                        {
                            newVariable = qsutil.GetVariableName(newVariable, PopulatedDictionary.Values.ToList());
                            txt_new_variable.Text = newVariable;
                            isNewQuestion = true;
                            isUpdateQuestion = false;
                        }
                        else
                        {
                            newVariable = qsutil.GetVariableName(Combo_sourceVariable.Text, PopulatedDictionary.Values.ToList());
                            txt_new_variable.Text = newVariable;
                            isNewQuestion = true;
                            isUpdateQuestion = false;
                        }
                    }
                    else if (result == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                }
                if ((Util.Definiotion.VariableDictionary.ContainsKey(newVariable)) && (
              (Util.Definiotion.VariableDictionary[newVariable].AnswerType != answerType.SelectedItem.ToString()) ||
             (Util.Definiotion.VariableDictionary[newVariable].CategoryCount != numberOfChoices)))
                {
                    System.Windows.Forms.DialogResult result;
                    result = MessageDialog.InfoYesNo(LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_ITEM_NAME_ALREADY_EXISTS + "\n" + LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_RENAME_NEW_VARIABLE_AUTOMATICALLY);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        //Get new variable name
                        newVariable = txt_new_variable.Text = qsutil.GetVariableName(newVariable, PopulatedDictionary.Values.ToList());
                    }
                    else if (result == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }

                    isNewQuestion = true;
                    isUpdateQuestion = false;
                }
                else if ((Util.Definiotion.VariableDictionary.ContainsKey(newVariable)) &&
              (Util.Definiotion.VariableDictionary[newVariable].AnswerType == answerType.SelectedItem.ToString()) &&
             (Util.Definiotion.VariableDictionary[newVariable].CategoryCount == numberOfChoices)
             )
                {
                    if ((Util.Definiotion.VariableDictionary[newVariable].Question != Text_NewItem_Question.Text))
                    {
                        isUpdateQuestion = true;
                    }
                    for (int i = 0; i < choicesCount.SelectedIndex; i++)
                    {
                        if (!Convert.ToString(ChoiceListView[i].Choices).Equals((Util.Definiotion.VariableDictionary[newVariable]).Choices[i]))
                        {
                            isUpdateQuestion = true;
                        }
                    }

                    isNewQuestion = false;
                }

                if (ProcessingOption == QC4Common.Common.Constants.STD_DP.Process_Edit && (string.IsNullOrEmpty(txt_new_variable.Text)))
                {
                    string errormsg = LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_NEW_ITEM_EMPTY + "\n" + LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_RENAME_NEW_VARIABLE_AUTOMATICALLY;
                    System.Windows.Forms.DialogResult result;
                    result = MessageDialog.InfoYesNo(errormsg);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        //Get new variable name
                        newVariable = qsutil.GetVariableName(Combo_sourceVariable.Text, PopulatedDictionary.Values.ToList());
                        txt_new_variable.Text = newVariable;
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
                if (ProcessingOption != QC4Common.Common.Constants.STD_DP.Process_Edit && !string.IsNullOrEmpty(newVariable))
                {
                    // Check the maxlength of newVariable
                    if (frmutil.IsVariableLengthExceeds(newVariable))
                    {
                        MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_QUESTION_LENGTH_EXCEED, QC4Common.Common.Constants.QsVariableMaxLength)); //ERR_MSG_QUESTION_LENGTH_EXCEED
                        return;
                    }
                    // Check the newVariable name is valid or not
                    Util.QS.NewQuestionValidation validation = new Util.QS.NewQuestionValidation(newVariable);
                    if (!validation.Validation_Variable(true))
                    {
                        return;
                    }
                }

                switch (ch)
                {
                    case "SA":
                        var dataSet = new DataSet();
                        var dataTable = new DataTable();
                        dataSet.Tables.Add(dataTable);

                        dataTable.Columns.Add("Index");
                        dataTable.Columns.Add("Choice");
                        dataTable.Columns.Add("SelectedOperator");
                        dataTable.Columns.Add("SelectedOperator1");
                        if (choicesCount.SelectedIndex == 0)
                        {
                            List<ChoiceList> valueEnteredChoices = new List<ChoiceList>();
                            var foundAuto = ChoiceListView.LastOrDefault(u => (!string.IsNullOrEmpty(u.Choices)) || (!string.IsNullOrEmpty(u.SelectedOperator)) || (!string.IsNullOrEmpty(u.SelectedOperator1)));
                            int foundAutoIndex = ChoiceListView.IndexOf(foundAuto);
                            foreach (var element in data_grid.ItemsSource)
                            {
                                if ((element as ChoiceList).Id <= foundAutoIndex + 1)
                                {
                                    DataRow newRow = dataTable.NewRow();
                                    newRow["Index"] = (element as ChoiceList).Id;
                                    newRow["Choice"] = (element as ChoiceList).Choices;
                                    newRow["SelectedOperator"] = (element as ChoiceList).SelectedOperator;
                                    newRow["SelectedOperator1"] = (element as ChoiceList).SelectedOperator1;

                                    dataTable.Rows.Add(newRow);
                                }
                                else
                                {
                                    break;
                                }

                            }
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
                                DataRow newRow = dataTable.NewRow();
                                newRow["Index"] = (element as ChoiceList).Id;
                                newRow["Choice"] = (element as ChoiceList).Choices;
                                newRow["SelectedOperator"] = (element as ChoiceList).SelectedOperator;
                                newRow["SelectedOperator1"] = (element as ChoiceList).SelectedOperator1;

                                dataTable.Rows.Add(newRow);
                            }
                        }


                        string[] dt_Choices_columns = new string[2];
                        dt_Choices_columns[0] = "Index";
                        dt_Choices_columns[1] = "Choice";
                        string[] dt_Choices_columns2 = new string[2];
                        dt_Choices_columns2[0] = "SelectedOperator";
                        dt_Choices_columns2[1] = "SelectedOperator1";
                        DataTable dtLimits = dataTable.DefaultView.ToTable(false, dt_Choices_columns2);
                        DataTable dtChoice = dataTable.DefaultView.ToTable(false, dt_Choices_columns);


                        string[] paramListSA = new string[dtLimits.Rows.Count + 2];
                        paramListSA[0] = Combo_sourceVariable.Text;
                        paramListSA[1] = choiceRange1.SelectedItem.ToString() + "-" + choiceRange2.SelectedItem.ToString();
                        for (int i = 1; i <= dtLimits.Rows.Count; i++)
                        {
                            paramListSA[1 + i] = dtLimits.Rows[i - 1][0].ToString() + "-" + dtLimits.Rows[i - 1][1].ToString();
                        }
                        for (int i = 1; i <= dtChoice.Rows.Count; i++)
                        {
                            if (dtChoice.Rows[i - 1][1].ToString().Length > QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit)
                            {
                                dtChoice.Rows[i - 1][1] = dtChoice.Rows[i - 1][1].ToString().PadRight(QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit).Substring(0, QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit);
                            }
                        }
                        int columncountSA = (QC4Common.Common.Constants.DP.SubstituteOperatorColumn - QC4Common.Common.Constants.DP.OnOffColumn) + paramListSA.Count() + 1; //to identify upto which column we have to write values into the sheet in a single row
                        if (dphelper.WriteProcess(Workbook, Util.Constants.ProcessingType.CreateNewVariable, newVariable, answerType.SelectedValue.ToString(), newQuestion,
                            dataTable.Rows.Count, dtChoice, Constants.DP.SubstituteOperatorCOUNT, GetDPSaveInstructions(paramListSA, columncountSA, newVariable), isNewQuestion, WriteRow, ProcessingOption, null, isUpdateQuestion))//need to pass row num from here for saving 
                        {

                            MessageDialog.Info(LocalResource.ADDQS_INFO_MSG_SUCCESS);
                            isModifiedProcess = true;
                            this.Close();

                        }


                        break;
                    case "N":
                        string[] paramList = new string[2];
                        paramList[0] = Combo_sourceVariable.Text;
                        paramList[1] = choiceRange1.SelectedItem.ToString() + "-" + choiceRange2.SelectedItem.ToString();
                        int columncount = (QC4Common.Common.Constants.DP.SubstituteOperatorColumn - QC4Common.Common.Constants.DP.OnOffColumn) + paramList.Count() + 1; //to identify upto which column we have to write values into the sheet in a single row
                        if (dphelper.WriteProcess(Workbook, Util.Constants.ProcessingType.CreateNewVariable, newVariable, answerType.SelectedValue.ToString(), newQuestion,
                       0, null, Constants.DP.SubstituteOperatorCOUNT, GetDPSaveInstructions(paramList, columncount, newVariable), isNewQuestion, WriteRow, ProcessingOption, null, isUpdateQuestion))//need to pass row num from here for saving 
                        {

                            MessageDialog.Info(LocalResource.ADDQS_INFO_MSG_SUCCESS);
                            isModifiedProcess = true;
                            this.Close();

                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }
        public bool CheckOverlappedLimits(DataTable criterias)
        {
            bool isOverlapped = false;
            char[] spliters = new char[] { '-', '/', ',' };
            string[] criteria = new string[criterias.Rows.Count];
            List<string> paramValues = new List<string>();
            for (int i = 1; i <= criterias.Rows.Count; i++)
            {
                criteria[i - 1] = criterias.Rows[i - 1][1].ToString();
                paramValues.Add(criterias.Rows[i - 1][1].ToString());

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
        private string[,] GetDPSaveInstructions(string[] paramList, int columncount, string newVariable)
        {

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
                        dpsaveinstructios[0, i] = newVariable;
                        break;

                    case 7://instruction
                        dpsaveinstructios[0, i] = Constants.DP.SubstituteOperatorCOUNT;
                        break;
                    default:
                        dpsaveinstructios[0, i] = paramList[param];
                        param++;
                        break;

                }
            }
            return dpsaveinstructios;
        }

        #region Handling Datagrid events
        System.Windows.Controls.DataGrid ExpGrid = null;
        private void b1SetColor(object sender, MouseButtonEventArgs e)
        {
            if (ExpGrid != null)
                ExpGrid.Focus();
        }
        #endregion

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
                                            if (col == 2)
                                            {
                                                if (data[i, (j - 1)] == null)
                                                {
                                                    ChoiceListView[RowIndex].SelectedOperator = null;
                                                }
                                                else
                                                {
                                                    if (ChoiceListView[RowIndex].Operator != null)
                                                    {
                                                        ChoiceListView[RowIndex].SelectedOperator = data[i, (j - 1)].ToString();
                                                    }
                                                }

                                            }
                                            else if (col == 1)
                                            {
                                                if (data[i, (j - 1)] == null)
                                                {
                                                    ChoiceListView[RowIndex].Choices = null;
                                                }
                                                else
                                                {
                                                    ChoiceListView[RowIndex].Choices = data[i, (j - 1)].ToString();
                                                }

                                            }
                                            else if (col == 3)
                                            {
                                                if (data[i, (j - 1)] == null)
                                                {
                                                    ChoiceListView[RowIndex].SelectedOperator1 = null;
                                                }
                                                else
                                                {
                                                    if (ChoiceListView[RowIndex].Operator1 != null)
                                                    {
                                                        ChoiceListView[RowIndex].SelectedOperator1 = data[i, (j - 1)].ToString();
                                                    }
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
                                                    ChoiceListView[RowIndex].SelectedOperator = null;
                                                }
                                                else
                                                {
                                                    if (ChoiceListView[RowIndex].Operator != null)
                                                    {
                                                        ChoiceListView[RowIndex].SelectedOperator = data[i, (j - 1)].ToString();
                                                    }
                                                }

                                            }
                                            else if (col == 3)
                                            {
                                                if (data[i, (j - 1)] == null)
                                                {
                                                    ChoiceListView[RowIndex].SelectedOperator1 = null;
                                                }
                                                else
                                                {
                                                    if (ChoiceListView[RowIndex].Operator1 != null)
                                                    {
                                                        ChoiceListView[RowIndex].SelectedOperator1 = data[i, (j - 1)].ToString();
                                                    }
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
                                                ChoiceListView[RowIndex].SelectedOperator1 = null;
                                            }
                                            else
                                            {
                                                if (ChoiceListView[RowIndex].Operator1 != null)
                                                {
                                                    ChoiceListView[RowIndex].SelectedOperator1 = data[i, (j - 1)].ToString();
                                                }
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
                            ChoiceListView[id - 1].Choices = null;
                            ChoiceListView[id - 1].SelectedOperator = null;
                            ChoiceListView[id - 1].SelectedOperator1 = null;
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

        private void Input_axilary_comb_PreviewKeyDown(object sender, KeyEventArgs e)
        {
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

        /// <summary>Sets the BG color of the invalid choice cell.</summary>
        /// <param name="index">The index.</param>
        private void SetInvalidBGChoice(int index)
        {
            ChoiceListView[index].IsChoiceEmpty = true;
            CollectionViewSource.GetDefaultView(data_grid.ItemsSource).Refresh();

        }
        /// <summary>Sets the BG color of the invalid Lower Limit cell.</summary>
        /// <param name="index">The index.</param>
        private void SetInvalidBGLowerLimit(int index)
        {
            ChoiceListView[index].IsLowerEmpty = true;
            CollectionViewSource.GetDefaultView(data_grid.ItemsSource).Refresh();

        }
        /// <summary>Sets the BG color of the invalid Upper Limit cell.</summary>
        /// <param name="index">The index.</param>
        private void SetInvalidBGUpperLimit(int index)
        {
            ChoiceListView[index].IsUpperEmpty = true;
            CollectionViewSource.GetDefaultView(data_grid.ItemsSource).Refresh();

        }
        /// <summary>Set red color for the invalid Lower Limit value.</summary>
        /// <param name="index">The index.</param>
        private void SetInvalidColorLowerLimit(int index)
        {
            ChoiceListView[index].IsLowerInvalid = true;
            CollectionViewSource.GetDefaultView(data_grid.ItemsSource).Refresh();

        }
        /// <summary>Set red color for the invalid Upper Limit value.</summary>
        /// <param name="index">The index.</param>
        private void SetInvalidColorUpperLimit(int index)
        {
            ChoiceListView[index].IsUpperInvalid = true;
            CollectionViewSource.GetDefaultView(data_grid.ItemsSource).Refresh();

        }

        private void Cmb_dataGrid_operator1_KeyDown(object sender, KeyEventArgs e)
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

        private void Data_grid_IsMouseCaptureWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                if (data_grid.SelectedItems != null)
                {
                    var items = data_grid.SelectedItems.Cast<ChoiceList>().ToList();
                    if (items.Count == 1)
                    {
                        if (items[0].IsChoiceEmpty == true || items[0].IsLowerEmpty == true || items[0].IsUpperEmpty == true || items[0].IsLowerInvalid == true || items[0].IsUpperInvalid == true)
                        {
                            items[0].IsChoiceEmpty = false;
                            items[0].IsLowerEmpty = false;
                            items[0].IsUpperEmpty = false;
                            items[0].IsLowerInvalid = false;
                            items[0].IsUpperInvalid = false;
                        }
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

        private void Cmb_dataGrid_operator1_DropDownOpened(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Controls.ComboBox sen = ((System.Windows.Controls.ComboBox)sender);
                if (sen.DataContext != null)
                {
                    var selectedChoiceList = sen.DataContext as ChoiceList;
                    if (selectedChoiceList.IsChoiceEmpty == true || selectedChoiceList.IsLowerEmpty == true || selectedChoiceList.IsUpperEmpty == true || selectedChoiceList.IsLowerInvalid == true || selectedChoiceList.IsUpperInvalid == true)
                    {
                        selectedChoiceList.IsChoiceEmpty = false;
                        selectedChoiceList.IsLowerEmpty = false;
                        selectedChoiceList.IsUpperEmpty = false;
                        selectedChoiceList.IsLowerInvalid = false;
                        selectedChoiceList.IsUpperInvalid = false;
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
                Text_NewItem_Question.Text = qs.Question;
                
                if (txt_answerType.Text == "SA")
                {
                    //answerType.ItemsSource =qs.AnswerType;
                    answerType.SelectedItem = "SA";
                }
                else
                {

                    //answerType.ItemsSource = qs.AnswerType;
                    answerType.SelectedItem =qs.AnswerType;
                    answerType.IsEnabled = true;


                }
                Text_NewItem_Question.Text = (frmutil.UnEscapeCRLF(qs.TableHeading) + " " + frmutil.UnEscapeCRLF(qs.Question)).TrimEnd().TrimStart();
                choicesCount.SelectedIndex =qs.Choices.Count;
                for (int i = 0; i < choicesCount.SelectedIndex; i++)
                {
                    ChoiceListView[i].Choices = frmutil.EscapeCRLF(qs.Choices[i]);
                    ChoiceListView[i].SelectedOperator = null;
                    ChoiceListView[i].SelectedOperator1 = null;
                    ChoiceListView[i].Criteria = null;
                }
                CollectionViewSource.GetDefaultView(data_grid.ItemsSource).Refresh();
            }
        }
    }
}
