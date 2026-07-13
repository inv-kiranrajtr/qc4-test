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
using Qc4Launcher.Util;
using static FilterSettingsView.FilterSettingsClass;
using Qc4Launcher.Forms.GrossTabulationSetting.Common;
using Qc4Launcher.Logic;
using System.Text.RegularExpressions;
using System.Data;
using log4net;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Controls.Primitives;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Qc4Launcher.Logic.DataImport;

namespace Qc4Launcher.Forms.DP_Main
{
    /// <summary>
    /// Interaction logic for ADD.xaml
    /// </summary>
    public partial class ADD : Window
    {
        QC4Common.Util.FormUtil frmutil = new QC4Common.Util.FormUtil();
        private static List<String> commands;//list for storing the commands
        private static List<String> answertypes;//list for storing answer types for filtering variables
        private static List<string> choiceListSAMA; //list for storing SA type variables from the sheet
        private static List<int> indexListSA;
        private static List<int> indexListN;
        private static List<String> variables;
        private Dictionary<String, QuestionSettings> dictionary = new Dictionary<string, QuestionSettings>(); //to get the variable list
        public static Excel.Workbook workbook;
        private string newVariableNameTemp = string.Empty;
        private string newVariableQuestionTemp = string.Empty;
        private int readRow;
        private int writeRow;
        private bool isNewQuestion = false;
        private bool isUpdateQuestion = false;
        private bool isCopyProcessInitialLoad = false;
        string[] choicesList = new string[201];
        string[] repeatParams = new string[3];
        public bool isModifiedProcess = false;
        private string processingType;
        private string processingOption;
        public static Excel.Range range;
        private DataTable dt_Origin = null, dt_selecteditems = null, dt_defaultTable = null;
        private static QuestionSettings question = new QuestionSettings(); //to get the details of selected question 
        private DataProcessHelper dbHelper = new DataProcessHelper();
        private QC4Common.Util.QSUtil qsUtil = new QC4Common.Util.QSUtil();
        private QC4Common.Util.FormUtil formUtil = new QC4Common.Util.FormUtil();
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private ObservableCollection<SourceVariableList> sourceVariableList1 = new ObservableCollection<SourceVariableList>();
        ObservableCollection<ChoiceList> choiceList = new ObservableCollection<ChoiceList>();

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
        public ObservableCollection<SourceVariableList> SourceVariableListView1
        {
            get
            {
                return sourceVariableList1;
            }
            set
            {
                sourceVariableList1 = value;
            }
        }

        public class SourceVariableList
        {
            private int questionrownumber;
            private string variable;
            private string answertype;
            private string question;
            private string tableheading;
            private List<string> choices;
            private List<string> aswerTypes;

            public int QuestionRowNumber
            {
                get { return questionrownumber; }
                set { questionrownumber = value; }
            }

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
                    if (choices != null)
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
                    else return null;
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
            public string Tableheading
            {
                get { return tableheading; }
                set { tableheading = value; }
            }
        }
        private DataTable CreateTable()
        {
            DataTable griddata = new DataTable();
            griddata.Columns.Add("Index");
            griddata.Columns.Add("Variable");
            return griddata;
        }
        private DataTable FillDataGrid(int[] inddex, string[] variables)
        {
            DataTable griddata = CreateTable();
            DataRow dr;
            for (int i = 1; i <= inddex.Count(); i++)
            {
                dr = griddata.NewRow();
                dr["Index"] = inddex[i - 1];
                dr["Variable"] = variables[i - 1];
                griddata.Rows.Add(dr);
            }
            return griddata;
        }

        public ADD(Microsoft.Office.Interop.Excel.Workbook Workbook, int stdreadrow, int stdwriterow, string stdProcessingtype, string stdprocessingoption)
        {
            workbook = Workbook;
            readRow = stdreadrow;
            writeRow = stdwriterow;
            processingType = stdProcessingtype;
            processingOption = stdprocessingoption;
            choiceListSAMA = new List<string>();
            indexListN = new List<int>();
            indexListSA = new List<int>();
            dictionary = Definiotion.VariableDictionary;
            InitializeComponent();
            try
            {
                txt_new_variable.IsEnabled = false;
                NewItemSearchbutton.IsEnabled = false;
                Text_NewItem_Question.IsEnabled = false;
                NewItemSearchbutton.Opacity = 0.5;

                int[] choices = new int[1001];
                choices = Enumerable.Range(0, 1001).ToArray();
                choicesList = Array.ConvertAll(choices, ele => ele.ToString());
                choicesList[0] = LocalResource.LBL_AUTO;
                choicesCount.ItemsSource = choicesList;
                Text_NewItem_AnswerType.Text = "MA";
                LoadVariablesFromSheet();


                dt_Origin = CreateTable();
                dt_defaultTable = CreateTable();
                dt_selecteditems = CreateTable();

                dt_Origin = FillDataGrid(indexListN.ToArray(), choiceListSAMA.ToArray());
                dt_defaultTable = FillDataGrid(indexListN.ToArray(), choiceListSAMA.ToArray());
                Grid_OriginItem_ChoiceList1.DataContext = dt_Origin;
                Grid_OriginItem_ChoiceList1.UpdateLayout();
               
            }
            catch (Exception ex)
            {

            }
            try
            {
                Grid_OriginItem_ChoiceList1.Focus();

            }
            catch (Exception ex)
            {

            }


        }


        private void LoadVariablesFromSheet()
        {
            choiceListSAMA = new List<string>();
            indexListN = new List<int>();
            indexListSA = new List<int>();
            var SettingSheet = ExcelUtil.GetWorkSheetByCodeName(workbook, "Sheet91");

            int variableIndexN = 0;
            int variableIndexSA = 0;

            if (SettingSheet != null)
            {
                Microsoft.Office.Interop.Excel.Range Range = SettingSheet.get_Range("List_Item_SAMA");
                if (Range != null)
                {


                    if (Range.Cells.Count == 1)
                    {
                        if (Range.Value != null)
                        {
                         
                            if (Convert.ToString(Range.Value) != string.Empty && dictionary.ContainsKey(Convert.ToString(Range.Value)))
                            {
                                question = dictionary[Convert.ToString(Range.Value)];
                                choiceListSAMA.Add(Convert.ToString(Range.Value));
                                variableIndexN++;
                                indexListN.Add(variableIndexN);

                            }
                        }


                    }
                    else
                    {
                        var objAry = Range.Value;
                        if (objAry != null)
                        {
                            int max = objAry.GetLength(0);
                            if (max > 0)
                            {
                                for (int i = 1; i <= max; i++)
                                {
                                    if (objAry[i, 1] != null)
                                    {

                                        if (Convert.ToString(objAry[i, 1]) != string.Empty && dictionary.ContainsKey(Convert.ToString(objAry[i, 1])))
                                        {
                                            question = dictionary[Convert.ToString(objAry[i, 1])];

                                            choiceListSAMA.Add(Convert.ToString(objAry[i, 1]));
                                            variableIndexN++;
                                            indexListN.Add(variableIndexN);

                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }
        }


        private void DisableRightMenu(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        System.Windows.Controls.DataGrid ExpGrid = null;
        private void b1SetColor(object sender, MouseButtonEventArgs e)
        {
            if (ExpGrid != null)
                ExpGrid.Focus();
        }

        private void Grid_CurrentCellChanged(object sender, EventArgs e)
        {
            var dg = (System.Windows.Controls.DataGrid)sender;
            ExpGrid = dg;
            dg.Focus();

        }

        private void Grid_OriginItem_ChoiceList1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftShift) && Key.Tab == e.Key)
            {
                Command_Cancel.Focus();
                e.Handled = true;
            }
            else
           if (e.Key == Key.Tab)
            {
            }
            else
                QC4Common.Util.GridCommonFunc.Arrow(sender, e);
        }

        private void Grid_OriginItem_ChoiceList2_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
            }
            else
                QC4Common.Util.GridCommonFunc.Arrow(sender, e);
        }



        private void Command_OriginItem_Add_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftShift) && Key.Tab == e.Key)
            {
                Command_OriginItem_Add.Focus();
                e.Handled = true;
            }
            else if (e.Key == Key.Tab)
            {
                Grid_OriginItem_ChoiceList2.Focus();
            }
        }
        Logic.DataProcessHelper dphelper = new Logic.DataProcessHelper();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (processingOption == QC4Common.Common.Constants.STD_DP.Process_Edit || processingOption == QC4Common.Common.Constants.STD_DP.Process_Copy)
            {
                Excel.Worksheet dataProcessSheet = Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Qc4Launcher.Util.Constants.SheetCodeName.DataProcessS);
                Excel.Range rowstart = dataProcessSheet.Cells[readRow, 1];
                Excel.Range rowend = dbHelper.GetLastCellInRow(rowstart);
                Excel.Range range_Edit_Copy = dataProcessSheet.Range[rowstart, rowend];
                if (range_Edit_Copy.Cells.Count > 1)
                {
                    QuestionSettings qs = new QuestionSettings();
                    var obj = range_Edit_Copy.Value;
                    if (obj[1, 3] != null)
                    {
                        PopulateValuesToModify(range_Edit_Copy, obj, processingOption);
                    }
                }
                string[,] objarray = dphelper.GetRangevalues(readRow, writeRow, workbook);
                if (processingOption == QC4Common.Common.Constants.STD_DP.Process_Edit)
                {

                    Color newVariableTextboxColor = (Color)ColorConverter.ConvertFromString("#F0F0F0");
                    Color editProcessMethodColor = (Color)ColorConverter.ConvertFromString("#FFADADAD");

                    Textbox_ProcessMethod.Background = new System.Windows.Media.SolidColorBrush(newVariableTextboxColor);
                    Textbox_ProcessMethod.Foreground = new System.Windows.Media.SolidColorBrush(editProcessMethodColor);
                    txt_new_variable.IsEnabled = false;
                    choicesCount.IsEditable = false;
                    choicesCount.IsEnabled = false;
                }

            }
           
        }
        string savedVariableName;
        private void PopulateValuesToModify(Excel.Range dpRange, object[,] objArray, string toModify)
        {
            try
            {
                string editedName = string.Empty;
                string editedQuestionText = string.Empty;
                savedVariableName = objArray[1, QC4Common.Common.Constants.DP.SubstituteVariableColumn].ToString();
                if (processingOption == QC4Common.Common.Constants.STD_DP.Process_Edit)
                {
                    editedName = savedVariableName;

                }

                else if (processingOption == QC4Common.Common.Constants.STD_DP.Process_Copy)
                {
                    isCopyProcessInitialLoad = true;
                    if (dictionary.ContainsKey(savedVariableName))
                    {
                        editedName = qsUtil.GetVariableName(savedVariableName, dictionary.Values.ToList());
                    }
                }

                //New variable QuestionText
                if (dictionary.ContainsKey(savedVariableName))
                {
                    QuestionSettings qs = dictionary[savedVariableName];
                    editedQuestionText = qs.Question;
                    txt_new_variable.Text = editedName;
                    Text_NewItem_Question.Text = editedQuestionText;
                    choicesCount.SelectedIndex = qs.CategoryCount;
                    for (int i = 0; i < qs.CategoryCount; i++)
                    {
                        ChoiceListView[i].Choice = formUtil.EscapeCRLF( qs.Choices[i]);
                    }

                }
                else
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        ChoiceListView[i].Choice = null;
                    }
                }

                string firstParam = objArray[1, QC4Common.Common.Constants.DP.SubstituteParam1Column + 1].ToString();
                string firstParam1 = objArray[1, QC4Common.Common.Constants.DP.SubstituteParam1Column].ToString();
                string savedCommand = objArray[1, QC4Common.Common.Constants.DP.SubstituteOperatorColumn].ToString();

                dt_defaultTable = CreateTable();
                dt_Origin = CreateTable();


                if (!string.IsNullOrWhiteSpace(firstParam1))
                {
                    if (firstParam1 == "0") { exclude_chk.IsChecked = false; } else if (firstParam1 == "1") { exclude_chk.IsChecked = true; }
                }
                QuestionSettings qs1 = dictionary[firstParam];
                if (qs1.AnswerType == QuestionType.SA.ToString() || qs1.AnswerType == QuestionType.MA.ToString())
                {

                    dt_defaultTable = FillDataGrid(indexListN.ToArray(), choiceListSAMA.ToArray());
                    dt_Origin = FillDataGrid(indexListN.ToArray(), choiceListSAMA.ToArray());
                }
                dt_selecteditems = CreateTable();
                Grid_OriginItem_ChoiceList1.DataContext = null;
                for (int i = (QC4Common.Common.Constants.DP.SubstituteParam1Column + 1); i <= objArray.Length; i++)
                {
                    string variable = objArray[1, i].ToString();

                    for (int j = 0; j < dt_Origin.Rows.Count; j++)
                    {
                        DataRow dr = dt_Origin.Rows[j];
                        if (dr[1].ToString() == variable)
                        {
                            DataRow dr1 = dt_selecteditems.NewRow();
                            dr1[0] = dr[0];
                            dr1[1] = dr[1];
                            dt_selecteditems.Rows.Add(dr1);
                            dt_selecteditems.AcceptChanges();
                            dt_Origin.Rows[j].Delete();
                            dt_Origin.AcceptChanges();
                        }
                    }
                }
                Grid_OriginItem_ChoiceList2.DataContext = dt_selecteditems;
                Grid_OriginItem_ChoiceList1.DataContext = dt_Origin;

            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }


        private void Grid_OriginItem_ChoiceList1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!Keyboard.IsKeyDown(Key.Tab))
            {

                if (Grid_OriginItem_ChoiceList1.SelectedItem != null)
                {

                    DataRowView drv = Grid_OriginItem_ChoiceList1.SelectedItems.Cast<DataRowView>().LastOrDefault();
                    string variable = drv["Variable"].ToString();
                    question = dictionary[variable];
                    txt_answerType.Text = question.AnswerType;
                    Text_OriginItem_Item.Text = question.Variable;
                    txt_source_Question.Text = question.Question;
                    txt_answerTypeWithChoiceCount.Text = Convert.ToString(question.CategoryCount);
                }
                else
                {

                    txt_answerType.Text = string.Empty;
                    Text_OriginItem_Item.Text = string.Empty;
                    txt_answerTypeWithChoiceCount.Text = string.Empty;
                    txt_source_Question.Text = string.Empty;

                }
            }
        }

        private void Command_OriginItem_Add_Click(object sender, RoutedEventArgs e)
        {

            if (Grid_OriginItem_ChoiceList1.SelectedItems.Count <= 0) { return; }

            DataRow dr;
            variables = new List<string>();
            try
            {
                foreach (DataRowView drv in Grid_OriginItem_ChoiceList1.SelectedItems)
                {

                    string no = drv["Index"].ToString();
                    string choice = drv["Variable"].ToString();
                    dr = dt_selecteditems.NewRow();
                    dr["Index"] = no;
                    dr["Variable"] = choice;
                    dt_selecteditems.Rows.Add(dr);

                }
                dt_selecteditems.DefaultView.Sort = "Index";
                dt_selecteditems.AcceptChanges();
                DataTable test = dt_selecteditems.Clone();
                test.Columns["Index"].DataType = Type.GetType("System.Int32");
                foreach (DataRow dr1 in dt_selecteditems.Rows)
                {
                    test.ImportRow(dr1);
                }
                test.AcceptChanges();
                DataView dv = test.DefaultView;
                dv.Sort = "Index ASC";
                dt_selecteditems = dv.ToTable();
                Grid_OriginItem_ChoiceList2.DataContext = dt_selecteditems;
                for (int i = 0; i < dt_selecteditems.Rows.Count; i++)
                {
                    DataRow datarow1;
                    datarow1 = dt_selecteditems.Rows[i];
                    variables.Add(datarow1[1].ToString());
                }

                dt_Origin = CreateTable();
                for (int j = 0; j < dt_defaultTable.Rows.Count; j++)
                {
                    dr = dt_defaultTable.Rows[j];
                    if (variables.Contains(dr[1].ToString()))
                    {

                    }
                    else
                    {
                        DataRow dr1 = dt_Origin.NewRow();
                        dr1[0] = dr[0];
                        dr1[1] = dr[1];
                        dt_Origin.Rows.Add(dr1);
                        dt_Origin.AcceptChanges();
                    }

                }

                Grid_OriginItem_ChoiceList1.DataContext = dt_Origin;



                if (processingOption != QC4Common.Common.Constants.STD_DP.Process_Edit)
                {
                    if (Grid_OriginItem_ChoiceList2.Items.Count > 0)
                    {
                        AppendTableHeadding = null;

                        foreach (DataRowView drv in Grid_OriginItem_ChoiceList2.Items)
                        {

                            string no = drv["Index"].ToString();
                            string choice = drv["Variable"].ToString();
                            question = dictionary[choice];
                            if (!string.IsNullOrWhiteSpace(question.TableHeading))
                            {
                                if (string.IsNullOrWhiteSpace(AppendTableHeadding))
                                {
                                    AppendTableHeadding += question.TableHeading + " ";
                                }
                                else
                                {
                                    if (!AppendTableHeadding.Contains(question.TableHeading))
                                    {
                                        AppendTableHeadding += question.TableHeading + " ";
                                    }
                                }

                            }


                        }

                        if (string.IsNullOrWhiteSpace(Text_NewItem_Question.Text)) { }
                        Text_NewItem_Question.Text = AppendTableHeadding;
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

        }

        private void Command_OriginItem_Remove_Click(object sender, RoutedEventArgs e)
        {


            if (Grid_OriginItem_ChoiceList2.SelectedItems.Count <= 0) { return; }

            DataRow dr;
            variables = new List<string>();
            try
            {
                foreach (DataRowView drv in Grid_OriginItem_ChoiceList2.SelectedItems)
                {
                    int no = int.Parse(drv["Index"].ToString());
                    string choice = drv["Variable"].ToString();
                    dr = dt_Origin.NewRow();
                    dr["Index"] = no;
                    dr["Variable"] = choice;
                    dt_Origin.Rows.Add(dr);
                }
                DataTable test = dt_Origin.Clone();
                test.Columns["Index"].DataType = Type.GetType("System.Int32");
                foreach (DataRow dr1 in dt_Origin.Rows)
                {
                    test.ImportRow(dr1);
                }
                test.AcceptChanges();
                DataView dv = test.DefaultView;
                dv.Sort = "Index ASC";
                dt_Origin = dv.ToTable();
                Grid_OriginItem_ChoiceList1.DataContext = dt_Origin;

                for (int i = 0; i < dt_Origin.Rows.Count; i++)
                {
                    DataRow datarow1;
                    datarow1 = dt_Origin.Rows[i];
                    variables.Add(datarow1[1].ToString());
                }

                dt_selecteditems = CreateTable();
                for (int j = 0; j < dt_defaultTable.Rows.Count; j++)
                {
                    dr = dt_defaultTable.Rows[j];
                    if (variables.Contains(dr[1].ToString()))
                    {

                    }
                    else
                    {
                        DataRow dr1 = dt_selecteditems.NewRow();
                        dr1[0] = dr[0];
                        dr1[1] = dr[1];
                        dt_selecteditems.Rows.Add(dr1);
                        dt_selecteditems.AcceptChanges();
                    }

                }
                if (dt_selecteditems.Rows.Count == 0)
                    Grid_OriginItem_ChoiceList2.DataContext = null;
                else
                    Grid_OriginItem_ChoiceList2.DataContext = dt_selecteditems;

                if (dt_selecteditems.Rows.Count > 0) { Save.IsEnabled = true; } else { Save.IsEnabled = false; }
                if (processingOption != QC4Common.Common.Constants.STD_DP.Process_Edit)
                {
                    if (Grid_OriginItem_ChoiceList2.Items.Count > 0)
                    {
                        AppendTableHeadding = null;

                        foreach (DataRowView drv in Grid_OriginItem_ChoiceList2.Items)
                        {

                            string no = drv["Index"].ToString();
                            string choice = drv["Variable"].ToString();
                            question = dictionary[choice];
                            if (!string.IsNullOrWhiteSpace(question.TableHeading))
                            {
                                if (string.IsNullOrWhiteSpace(AppendTableHeadding))
                                {
                                    AppendTableHeadding += question.TableHeading + " ";
                                }
                                else
                                {
                                    if (!AppendTableHeadding.Contains(question.TableHeading))
                                    {
                                        AppendTableHeadding += question.TableHeading + " ";
                                    }
                                }

                            }


                        }
                        Text_NewItem_Question.Text = AppendTableHeadding;
                    }
                }

                if (dt_selecteditems.Rows.Count == 0)
                {
                    if (processingOption != QC4Common.Common.Constants.STD_DP.Process_Edit)
                    {
                        txt_new_variable.Text = null;
                        Text_NewItem_Question.Text = null;
                        choicesCount.SelectedIndex = 0;

                        NewItemSearchbutton.IsEnabled = false;
                        NewItemSearchbutton.Opacity = 0.5;
                        txt_new_variable.IsEnabled = false;

                        Text_NewItem_Question.IsEnabled = false;

                        Color newVariableTextboxColor = (Color)ColorConverter.ConvertFromString("#F0F0F0");
                        txt_new_variable.Background = new System.Windows.Media.SolidColorBrush(newVariableTextboxColor);
                        Text_NewItem_Question.Background = new System.Windows.Media.SolidColorBrush(newVariableTextboxColor);

                        Grid_OriginItem_ChoiceList2.DataContext = null;
                        for (int i = 0; i < ChoiceListView.Count; i++)
                        {
                            ChoiceListView[i].Choice = null;
                        }
                        CollectionViewSource.GetDefaultView(data_grid.ItemsSource).Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }








        }
        public void loadingROw()
        {


            try
            {
                if (Grid_OriginItem_ChoiceList2.Items.Count > 0)
                {
                    if (processingOption != QC4Common.Common.Constants.STD_DP.Process_Edit)
                    {
                        txt_new_variable.IsEnabled = true;
                        Text_NewItem_Question.IsEnabled = true;
                        NewItemSearchbutton.IsEnabled = true;
                        NewItemSearchbutton.Opacity = 1;
                        txt_new_variable.Background = Brushes.White;
                        Text_NewItem_Question.Background = Brushes.White;
                        if (txt_new_variable.Text == string.Empty)
                        {
                            DataRow datarow = dt_selecteditems.Rows[0];
                            question = dictionary[datarow["Variable"].ToString()];
                            txt_new_variable.Text = qsUtil.GetVariableName(question.Variable, dictionary.Values.ToList()); //creating new variable with the first selected variable 
                            isNewQuestion = true;
                            choicesCount.SelectedItem = Convert.ToString(question.CategoryCount);
                            Text_NewItem_Question.Text = formUtil.EscapeCRLF(question.TableHeading);
                            for (int i = 0; i < question.Choices.Count; i++)
                            {
                                ChoiceListView[i].Choice = formUtil.EscapeCRLF(question.Choices[i]);
                            }
                            CollectionViewSource.GetDefaultView(data_grid.ItemsSource).Refresh();
                        }
                        else
                        {
                            if (Grid_OriginItem_ChoiceList2.Items.Count > 0)
                            {
                                AppendTableHeadding = null;
                                AppendTableHeadding = Text_NewItem_Question.Text;
                                foreach (DataRowView drv in Grid_OriginItem_ChoiceList2.Items)
                                {

                                    string no = drv["Index"].ToString();
                                    string choice = drv["Variable"].ToString();
                                    question = dictionary[choice];
                                    if (!string.IsNullOrWhiteSpace(question.TableHeading))
                                    {
                                        AppendTableHeadding += question.TableHeading + " ";
                                    }


                                }
                                Text_NewItem_Question.Text = AppendTableHeadding;
                            }
                        }
                    }
                    if (processingOption == QC4Common.Common.Constants.STD_DP.Process_Edit && txt_new_variable.Text != string.Empty)
                    {
                        txt_new_variable.IsEnabled = true;
                        txt_new_variable.IsReadOnly = true;
                        txt_new_variable.IsTabStop = false;
                        Text_NewItem_Question.IsEnabled = true;
                        Save.IsEnabled = true;
                        NewItemSearchbutton.IsEnabled = false;
                        NewItemSearchbutton.Opacity = 0.5;
                        Text_NewItem_Question.Background = Brushes.White;

                    }
                    if ((processingOption == QC4Common.Common.Constants.STD_DP.Process_Copy) && txt_new_variable.Text != string.Empty)
                    {
                        Save.IsEnabled = true;
                    }
                }
                else
                {
                    txt_new_variable.Text = null;
                    Text_NewItem_Question.Text = null;
                    choicesCount.SelectedIndex = 0;
                    txt_new_variable.IsEnabled = true;
                    Text_NewItem_Question.IsEnabled = true;
                    NewItemSearchbutton.IsEnabled = false;
                    NewItemSearchbutton.Opacity = 0.5;
                    Text_NewItem_Question.Background = Brushes.White;

                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

        }
        private void Grid_OriginItem_ChoiceList2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!Keyboard.IsKeyDown(Key.Tab))
            {
                if (Grid_OriginItem_ChoiceList2.SelectedItem != null)
                {
                    DataRowView drv = Grid_OriginItem_ChoiceList2.SelectedItems.Cast<DataRowView>().LastOrDefault();
                    string variable = drv["Variable"].ToString();
                    question = dictionary[variable];
                    txt_answerType.Text = question.AnswerType;
                    Text_OriginItem_Item.Text = question.Variable;
                    txt_source_Question.Text = question.Question;
                    txt_answerTypeWithChoiceCount.Text = Convert.ToString(question.CategoryCount);
                }
                else
                {

                    txt_answerType.Text = string.Empty;
                    Text_OriginItem_Item.Text = string.Empty;
                    txt_answerTypeWithChoiceCount.Text = string.Empty;
                    txt_source_Question.Text = string.Empty;

                }
            }
        }


        private void Grid_OriginItem_ChoiceList2_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            if (Grid_OriginItem_ChoiceList2.Items.Count > 0)
            {
                Save.IsEnabled = true;
            }
            else
            {
                Save.IsEnabled = false;
            }
        }
        string AppendTableHeadding = null;
        private void Grid_OriginItem_ChoiceList2_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            try
            {
                if (Grid_OriginItem_ChoiceList2.Items.Count > 0)
                {
                    if (processingOption != QC4Common.Common.Constants.STD_DP.Process_Edit)
                    {
                        txt_new_variable.IsEnabled = true;
                        Text_NewItem_Question.IsEnabled = true;
                        NewItemSearchbutton.IsEnabled = true;
                        NewItemSearchbutton.Opacity = 1;
                        txt_new_variable.Background = Brushes.White;
                        Text_NewItem_Question.Background = Brushes.White;
                        if (txt_new_variable.Text == string.Empty)
                        {

                            DataRow datarow = dt_selecteditems.Rows[0];
                            question = dictionary[datarow["Variable"].ToString()];
                            txt_new_variable.Text = qsUtil.GetVariableName(question.Variable, dictionary.Values.ToList()); //creating new variable with the first selected variable 
                            isNewQuestion = true;
                            if (processingOption == QC4Common.Common.Constants.STD_DP.Process_Copy)
                            {
                                choicesCount.SelectedIndex = 0;
                            }
                            else
                            {
                                choicesCount.SelectedItem = Convert.ToString(question.CategoryCount);
                            }

                            if (processingOption != QC4Common.Common.Constants.STD_DP.Process_Copy)
                            {
                                for (int i = 0; i < question.Choices.Count; i++)
                                {
                                    ChoiceListView[i].Choice = formUtil.EscapeCRLF(question.Choices[i]);
                                }
                            }
                            CollectionViewSource.GetDefaultView(data_grid.ItemsSource).Refresh();
                            processingOption = QC4Common.Common.Constants.STD_DP.Process_Create;
                        }
                        else
                        {

                        }
                    }
                    if (processingOption == QC4Common.Common.Constants.STD_DP.Process_Edit && txt_new_variable.Text != string.Empty)
                    {
                        txt_new_variable.IsEnabled = true;
                        txt_new_variable.IsReadOnly = true;
                        txt_new_variable.IsTabStop = false;
                        Text_NewItem_Question.IsEnabled = true;
                        Save.IsEnabled = true;
                        NewItemSearchbutton.IsEnabled = false;
                        NewItemSearchbutton.Opacity = 0.5;
                        Text_NewItem_Question.Background = Brushes.White;

                    }
                    if ((processingOption == QC4Common.Common.Constants.STD_DP.Process_Copy) && txt_new_variable.Text != string.Empty)
                    {
                    }
                }
                else
                {
                    txt_new_variable.Text = null;
                    Text_NewItem_Question.Text = null;
                    choicesCount.SelectedIndex = 0;
                    txt_new_variable.IsEnabled = true;
                    Text_NewItem_Question.IsEnabled = true;
                    NewItemSearchbutton.IsEnabled = false;
                    NewItemSearchbutton.Opacity = 0.5;
                    Text_NewItem_Question.Background = Brushes.White;

                }
                if (Grid_OriginItem_ChoiceList2.Items.Count > 0)
                {
                    Save.IsEnabled = true;
                }
                else
                {
                    Save.IsEnabled = false;
                }

            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

        }

        private void NewItemSearchbutton_Click(object sender, RoutedEventArgs e)
        {
            if (MagnifyingGlassButton.VariableList != null)
            {
                txt_new_variable.Text = MagnifyingGlassButton.VariableList.Variable;
                if (!string.IsNullOrEmpty(MagnifyingGlassButton.VariableList.Title))
                {
                    Text_NewItem_Question.Text = frmutil.UnEscapeCRLF(MagnifyingGlassButton.VariableList.Title) + " " + frmutil.UnEscapeCRLF(MagnifyingGlassButton.VariableList.Question);
                }
                else
                {
                    Text_NewItem_Question.Text = frmutil.UnEscapeCRLF(MagnifyingGlassButton.VariableList.Question);
                }
                choicesCount.SelectedIndex = MagnifyingGlassButton.VariableList.Choices.Count;
                for (int i = 0; i < choicesCount.SelectedIndex; i++)
                {
                    ChoiceListView[i].Choice = frmutil.EscapeCRLF(MagnifyingGlassButton.VariableList.Choices[i]);

                }

                CollectionViewSource.GetDefaultView(data_grid.ItemsSource).Refresh();
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

            var found = ChoiceListView.LastOrDefault(u => (u.Choice != null));
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
                        });
                    }
                }
            }
            data_grid.ItemsSource = ChoiceListView;
        }

        private void Save_Click_1(object sender, RoutedEventArgs e)
        {
            data_grid.SelectedIndex = -1;
            int choicecountdata = 0;
            var dataSet = new DataSet();
            var dataTable = new DataTable();
            var found = ChoiceListView.LastOrDefault(u => (!string.IsNullOrWhiteSpace(u.Choice)));
            int foundIndex = ChoiceListView.IndexOf(found);

            List<DataGT> variablesChoiceList2 = new List<DataGT>();
            if (processingOption != QC4Common.Common.Constants.STD_DP.Process_Edit && (txt_new_variable.Text != newVariableNameTemp || Text_NewItem_Question.Text != newVariableQuestionTemp))
            {
                isNewQuestion = true;
            }
            if (!string.IsNullOrWhiteSpace(txt_new_variable.Text) && dictionary.ContainsKey(txt_new_variable.Text))
            {
                QuestionSettings qs = dictionary[txt_new_variable.Text];
                if (qs.AnswerType != Text_NewItem_AnswerType.Text || qs.QuestionFlag != QC4Common.Common.Constants.QuestionFlag.New)
                {
                    isNewQuestion = true;
                }
                else if (qs.AnswerType == Text_NewItem_AnswerType.Text && qs.QuestionFlag == QC4Common.Common.Constants.QuestionFlag.New)
                {
                    isNewQuestion = false;
                }
                if (formUtil.UnEscapeCRLF(qs.Question) != Text_NewItem_Question.Text)
                {
                    isUpdateQuestion = true;
                }
            }

            if (string.IsNullOrWhiteSpace(Text_NewItem_Question.Text))
            {
                MessageBox.Show(LocalResource.ALERT_DP_NO_QUESTION, "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                string newVariable = ((txt_new_variable.Text).TrimStart().TrimEnd());
                string qstndata = ((Text_NewItem_Question.Text).TrimStart()).TrimEnd();
                int cout = 0;
                if (foundIndex < 0)
                {

                    ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ADD_DP_RECODE_ERROR_MSG_NO_CHOICE_SET));

                    return;
                }

                if (choicesCount.SelectedIndex > 0) { choicecountdata = ChoiceListView.Count; } else { choicecountdata = foundIndex + 1; }


                if (choicesCount.SelectedIndex > 0)
                {

                    for (int i = 0; i < ChoiceListView.Count; i++)
                    {
                        if (ChoiceListView.Count > (foundIndex + 1))
                        {
                            MessageDialog.ErrorOk(LocalResource.ADD_DP_RECODE_ERROR_MSG_NO_MATCH_NUMBER_OF_CHOICES);
                            return;

                        }
                        else
                        {
                            string cellChoice = Convert.ToString(ChoiceListView[i].Choice);
                            if (string.IsNullOrWhiteSpace(cellChoice))
                            {

                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, (i + 1).ToString()) + "\n" + (LocalResource.ERR_MSG_RECODE_CHOICE_IS_NOT_INPUT));
                                SetInvalidBGChoice(i);
                                return;
                            }
                            else
                            {
                            }
                        }
                    }
                }
                else
                {

                    for (int i = 0; i <= foundIndex; i++)
                    {
                        string cellChoice = Convert.ToString(ChoiceListView[i].Choice);
                        if (string.IsNullOrWhiteSpace(cellChoice))
                        {

                            MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, (i + 1).ToString()) + "\n" + (LocalResource.ERR_MSG_RECODE_CHOICE_IS_NOT_INPUT));
                            SetInvalidBGChoice(i);
                            return;
                        }
                        else
                        {
                        }
                    }
                }



                if (string.IsNullOrEmpty(newVariable) || !frmutil.IsVariableNameExists(newVariable, dictionary.Values.ToList(), 1))
                {
                    string errormsg = LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_NEW_ITEM_EMPTY + "\n" + LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_RENAME_NEW_VARIABLE_AUTOMATICALLY;

                    if (!string.IsNullOrWhiteSpace(txt_new_variable.Text))
                    {
                        errormsg = LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_ITEM_NAME_ALREADY_EXISTS + "\n" + LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_RENAME_NEW_VARIABLE_AUTOMATICALLY;
                    }
                    System.Windows.Forms.DialogResult result;
                    result = MessageDialog.InfoYesNo(errormsg);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (!string.IsNullOrEmpty(newVariable))
                        {
                            newVariable = qsUtil.GetVariableName(newVariable, dictionary.Values.ToList());
                            txt_new_variable.Text = newVariable;
                            isNewQuestion = true;
                            isUpdateQuestion = false;
                        }
                        else
                        {
                            DataRow dr = dt_selecteditems.Rows[0];
                            string firstSourceVariable = dr[1].ToString();
                            newVariable = qsUtil.GetVariableName(firstSourceVariable, dictionary.Values.ToList());
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
                string ExistingVariableAns = string.Empty;
                string variable = txt_new_variable.Text;
                int ChCatCount = 0;
                if (processingOption == QC4Common.Common.Constants.STD_DP.Process_Edit) { isUpdateQuestion = true; }
                if (processingOption != QC4Common.Common.Constants.STD_DP.Process_Edit)
                {
                    if ((Util.Definiotion.VariableDictionary.ContainsKey(variable)))
                    {
                        ExistingVariableAns = Util.Definiotion.VariableDictionary[variable].AnswerType;
                        ChCatCount = Util.Definiotion.VariableDictionary[variable].CategoryCount;
                        if ((ExistingVariableAns != txt_answerType.Text) && (ChCatCount != choicecountdata))
                        {
                            string errormsg = LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_NEW_ITEM_EMPTY + "\n" + LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_RENAME_NEW_VARIABLE_AUTOMATICALLY;

                            if (!string.IsNullOrWhiteSpace(txt_new_variable.Text))
                            {
                                errormsg = LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_ITEM_NAME_ALREADY_EXISTS + "\n" + LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_RENAME_NEW_VARIABLE_AUTOMATICALLY;
                            }
                            System.Windows.Forms.DialogResult result;
                            result = MessageDialog.InfoYesNo(errormsg);
                            if (result == System.Windows.Forms.DialogResult.Yes)
                            {
                                if (!string.IsNullOrEmpty(newVariable))
                                {
                                    newVariable = qsUtil.GetVariableName(newVariable, dictionary.Values.ToList());
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

                    }
                }
                if (processingOption != QC4Common.Common.Constants.STD_DP.Process_Edit && !string.IsNullOrEmpty(txt_new_variable.Text))
                {
                    // check the maxlength of newVariable
                    if (formUtil.IsVariableLengthExceeds(txt_new_variable.Text))
                    {
                        MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_QUESTION_LENGTH_EXCEED, QC4Common.Common.Constants.QsVariableMaxLength)); //                        return;
                    }
                    //check the newVariable name is valid or not
                    Util.QS.NewQuestionValidation validation = new Util.QS.NewQuestionValidation(txt_new_variable.Text);
                    if (!validation.Validation_Variable(true))
                    {
                        return;
                    }
                }
                dataSet.Tables.Add(dataTable);

                dataTable.Columns.Add("Index");
                dataTable.Columns.Add("Choice");
                if (choicesCount.SelectedIndex == 0)
                {
                    List<ChoiceList> valueEnteredChoices = new List<ChoiceList>();
                    var foundAuto = ChoiceListView.LastOrDefault(u => (u.Choice != null));
                    int foundAutoIndex = ChoiceListView.IndexOf(foundAuto);
                    foreach (var element in data_grid.ItemsSource)
                    {
                        if ((element as ChoiceList).Id <= foundAutoIndex + 1)
                        {

                            DataRow newRow = dataTable.NewRow();
                            newRow["Index"] = (element as ChoiceList).Id;
                            newRow["Choice"] = (element as ChoiceList).Choice;
                            dataTable.Rows.Add(newRow);
                        }
                        else
                        {
                            break;
                        }

                    }

                }
                else
                {
                    if (foundIndex < choicesCount.SelectedIndex - 1)
                    {
                        return;
                    }
                    foreach (var element in data_grid.ItemsSource)
                    {

                        DataRow newRow = dataTable.NewRow();
                        newRow["Index"] = (element as ChoiceList).Id;
                        newRow["Choice"] = (element as ChoiceList).Choice;
                        dataTable.Rows.Add(newRow);
                    }

                }





                //create parameter list of selected variables in Grid2
                string[] paramList = new string[dt_selecteditems.Rows.Count + 1];
                if (exclude_chk.IsChecked == true)
                {
                    paramList[0] = "1";
                }
                else
                {
                    paramList[0] = "0";
                }
                for (int i = 0; i < (dt_selecteditems.Rows.Count); i++)
                {
                    DataRow dr = dt_selecteditems.Rows[i];
                    paramList[(i + 1)] = dr[1].ToString();
                }
                string command = ExcelAddIn.Common.Constants.DP.SubstituteOperatorADD3;
                // int columncount = (QC4Common.Common.Constants.DP.SubstituteOperatorColumn - QC4Common.Common.Constants.DP.OnOffColumn) ; //to identify upto which column we have to write values into the sheet in a single row
                int columncount = (QC4Common.Common.Constants.DP.SubstituteOperatorColumn - QC4Common.Common.Constants.DP.OnOffColumn) + paramList.Count() + 1; //to identify upto which column we have to write values into the sheet in a single row
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
                for (int i = 1; i <= dataTable.Rows.Count; i++)
                {
                    if (dataTable.Rows[i - 1][1].ToString().Length > QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit)
                    {
                        dataTable.Rows[i - 1][1] = dataTable.Rows[i - 1][1].ToString().PadRight(QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit).Substring(0, QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit);
                    }
                }
                string Qstntext = (Text_NewItem_Question.Text);
                if (Qstntext.Length > QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit)
                {
                    Qstntext = Qstntext.PadRight(QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit).Substring(0, QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit);
                }

                if (dbHelper.WriteProcess(workbook, Util.Constants.ProcessingType.CreateNewVariable, (txt_new_variable.Text.TrimEnd()).TrimStart(), Text_NewItem_AnswerType.Text, Qstntext,
             choicecountdata, dataTable, command, dpsaveinstructios, isNewQuestion, writeRow, processingOption, null, isUpdateQuestion))//need to pass the entire row from here for saving 
                {
                    MessageDialog.Info(LocalResource.ADDQS_INFO_MSG_SUCCESS);
                    isModifiedProcess = true;
                    this.Close();
                }




            }

        }
        public void SaveClassProcess(DataTable dt, bool isNew, bool isUpdate)
        {

        }
        string ProcessingOption;
        private void data_grid_CurrentCellChanged(object sender, EventArgs e)
        {
            var dg = (System.Windows.Controls.DataGrid)sender;
            ExpGrid = dg;
            dg.Focus();
        }

        string clipboardText = "";
        private async void HandleCopyPaste(object sender, KeyEventArgs e)
        {
            int datagridColumn = data_grid.CurrentCell.Column.DisplayIndex;
            if (data_grid.SelectedIndex == -1) { data_grid.SelectedIndex = 0;}
            DataGridCell cell = frmutil.GetCell(data_grid, data_grid.SelectedIndex, datagridColumn);


            bool _altModifierPressed = (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl));
            if (_altModifierPressed && Keyboard.IsKeyDown(Key.V))
            {
                var uiElement = e.OriginalSource as UIElement;
                try
                {
                    Util.QS.GridCopyPaste copyPaste = new Util.QS.GridCopyPaste();
                    var data = copyPaste.PastetoDatagrid(sender);
                    if (data == null) { e.Handled = true; }
                    if (!cell.IsEditing)
                    {

                        int No_Row = copyPaste.No_Row;
                        int No_Column = copyPaste.No_Columns;
                        if (data != null)
                        {
                            e.Handled = true;
                            int datagridRow = data_grid.SelectedIndex;
                            if (data_grid.CurrentCell.Column.DisplayIndex == 1)
                            {
                                //selection in choice
                                if (No_Column > 1 || No_Row > data_grid.Items.Count - (data_grid.SelectedIndex) + 1)
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
                                                    ChoiceListView[RowIndex].Choice = null;
                                                }
                                                else
                                                {

                                                }

                                            }
                                            else if (col == 1)
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

                                        }

                                    }

                                }
                            }


                        }
                        this.data_grid.CommitEdit();
                        this.data_grid.CommitEdit();
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
                catch (Exception ex) { _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException); }

            }


            if (e.Key == Key.Delete)
            {
                try
                {
                    int RowIndex = 0;
                    if ((data_grid.SelectedItems != null) && (data_grid.SelectedItems.Count > 0))
                    {

                        foreach (var item in data_grid.SelectedItems)
                        {
                            var id = (ChoiceListView.First(x => x == item) as ChoiceList).Id;
                            ChoiceListView[id - 1].Choice = string.Empty;
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
        private void data_grid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            QC4Common.Util.GridCommonFunc.Arrow(sender, e);
        }

        private void Data_grid_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {

        }

        private void Txt_new_variable_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void Command_OriginItem_Add_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                Grid_OriginItem_ChoiceList2.Focus();
                Style style = Application.Current.FindResource("MyFocusVisual") as Style;
                Grid_OriginItem_ChoiceList1.FocusVisualStyle = style;
                if (Grid_OriginItem_ChoiceList2.SelectedItem == null && Grid_OriginItem_ChoiceList2.Items.Count > 0)
                {
                    // isTabInGrid = true;
                    Grid_OriginItem_ChoiceList2.SelectedIndex = 0;
                }
            }
        }

        private void Command_Cancel_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftShift) && Key.Tab == e.Key)
            {
                Save.Focus();
                e.Handled = true;
            }
            else if (e.Key == Key.Tab)
            {
                e.Handled = true;
                Grid_OriginItem_ChoiceList1.Focus();
                Style style = Application.Current.FindResource("MyFocusVisual") as Style;
                Grid_OriginItem_ChoiceList1.FocusVisualStyle = style;
                if (Grid_OriginItem_ChoiceList1.SelectedItem == null && Grid_OriginItem_ChoiceList1.Items.Count > 0)
                {
                    
                }
            }
        }

        public static void SelectRowByIndex(DataGrid dataGrid, int rowIndex)
        {

            dataGrid.SelectedItems.Clear();
            /* set the SelectedItem property */
            object item = dataGrid.Items[rowIndex]; // = Product X
            dataGrid.SelectedItem = item;

            DataGridRow row = dataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex) as DataGridRow;
            if (row == null)
            {
                /* bring the data item (Product object) into view
                 * in case it has been virtualized away */
                dataGrid.ScrollIntoView(item);
                row = dataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex) as DataGridRow;
            }
            //TODO: Retrieve and focus a DataGridCell object
        }

        private void Grid_OriginItem_ChoiceList1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftShift))
            {


            }

        }


        private void DoCheckRow(object sender, MouseButtonEventArgs e)
        {
            DataGridCell cell = sender as DataGridCell;
        }

        private void Grid_OriginItem_ChoiceList1_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Grid_OriginItem_ChoiceList1_GotMouseCapture(object sender, MouseEventArgs e)
        {
            try
            {
                if (Grid_OriginItem_ChoiceList1.SelectedItem != null)
                {

                    DataRowView drv = Grid_OriginItem_ChoiceList1.SelectedItems.Cast<DataRowView>().LastOrDefault();
                    string variable = drv["Variable"].ToString();
                    question = dictionary[variable];
                    txt_answerType.Text = question.AnswerType;
                    Text_OriginItem_Item.Text = question.Variable;
                    txt_source_Question.Text = question.Question;
                    txt_answerTypeWithChoiceCount.Text = Convert.ToString(question.CategoryCount);
                }
                else
                {

                    txt_answerType.Text = string.Empty;
                    Text_OriginItem_Item.Text = string.Empty;
                    txt_answerTypeWithChoiceCount.Text = string.Empty;
                    txt_source_Question.Text = string.Empty;

                }
            }
            catch (Exception ex) { }
        }

        private void Grid_OriginItem_ChoiceList2_GotMouseCapture(object sender, MouseEventArgs e)
        {
            if (Grid_OriginItem_ChoiceList2.SelectedItem != null)
            {
                DataRowView drv = Grid_OriginItem_ChoiceList2.SelectedItems.Cast<DataRowView>().LastOrDefault();
                string variable = drv["Variable"].ToString();
                question = dictionary[variable];
                txt_answerType.Text = question.AnswerType;
                Text_OriginItem_Item.Text = question.Variable;
                txt_source_Question.Text = question.Question;
                txt_answerTypeWithChoiceCount.Text = Convert.ToString(question.CategoryCount);
            }
            else
            {

                txt_answerType.Text = string.Empty;
                Text_OriginItem_Item.Text = string.Empty;
                txt_answerTypeWithChoiceCount.Text = string.Empty;
                txt_source_Question.Text = string.Empty;

            }
        }

        private void Data_grid_CopyingRowClipboardContent(object sender, DataGridRowClipboardEventArgs e)
        {
            try
            {
                e.ClipboardRowContent.RemoveAt(0);
            }
            catch (Exception ex) { _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException); }
        }

        private void MagnifyingGlassButton_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void SetInvalidBGChoice(int index)
        {
            ChoiceListView[index].IsChoiceEmpty = true;
            // CollectionViewSource.GetDefaultView(data_grid.ItemsSource).Refresh();


        }
        private void Data_grid_IsMouseCaptureWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                if (data_grid.SelectedItems != null)
                {
                    var datagridColumn = data_grid.CurrentCell.Item;

                    var items = data_grid.SelectedItems.Cast<ChoiceList>().ToList();
                    if (items.Count == 1)
                    {
                        if (items[0].IsChoiceEmpty == true)
                        {
                            items[0].IsChoiceEmpty = false;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void Data_grid_CurrentCellChanged_1(object sender, EventArgs e)
        {

        }

        private void HandleCopyPaste1(object sender, KeyEventArgs e)

        {

            bool _altModifierPressed = (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl));
            bool vbtn = Keyboard.IsKeyDown(Key.V);
            if (_altModifierPressed && Keyboard.IsKeyDown(Key.V))
            {
                string text = "Text to insert";
                TextBox textBox = sender as TextBox;
                textBox.SelectedText = text;
                textBox.SelectionStart = textBox.SelectionStart + text.Length;
                textBox.SelectionLength = 0;
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

        private void Data_grid_PreparingCellForEdit(object sender, DataGridPreparingCellForEditEventArgs e)
        {

        }

        private void Data_grid_PreviewKeyUp_1(object sender, KeyEventArgs e)
        {

        }
        DataProcessNewLoad dataLoad = new DataProcessNewLoad();

        private void Txt_new_variable_TextChanged(object sender, TextChangedEventArgs e)
        {
            QuestionSettings qs = dataLoad.Txt_Change_New_Item(txt_new_variable.Text);
            if (qs != null)
            {
                if (!string.IsNullOrEmpty(qs.TableHeading))
                {
                    Text_NewItem_Question.Text = frmutil.UnEscapeCRLF(qs.TableHeading) + " " + frmutil.UnEscapeCRLF(qs.Question);
                }
                else
                {
                    Text_NewItem_Question.Text = frmutil.UnEscapeCRLF(qs.Question);
                }
                choicesCount.SelectedIndex = qs.Choices.Count;
                for (int i = 0; i < choicesCount.SelectedIndex; i++)
                {
                    ChoiceListView[i].Choice = frmutil.EscapeCRLF(qs.Choices[i]);

                }

                CollectionViewSource.GetDefaultView(data_grid.ItemsSource).Refresh();

            }
        }
        private void Command_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public class ChoiceList : INotifyPropertyChanged
        {
            private int id;
            private string choice;
            private bool isChoiceEmpty = false;
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
            public event PropertyChangedEventHandler PropertyChanged;
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
