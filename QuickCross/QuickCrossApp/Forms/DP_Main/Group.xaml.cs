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
using Qc4Launcher.Logic.DataImport;

namespace Qc4Launcher.Forms.DP_Main
{
    /// <summary>
    /// Interaction logic for Group.xaml
    /// </summary>
    public partial class Group : System.Windows.Window
    {
        private static List<String> commands;//list for storing the commands
        private static List<String> answertypes;//list for storing answer types for filtering variables
        private static List<string> choiceListSA; //list for storing SA type variables from the sheet
        private static List<string> choiceListN; // list for storing N type variables from the sheet
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
        public bool isModifiedProcess = false;
        private bool isTabInGrid = false;
        private string processingType;
        private string processingOption;
        private bool isNewVariableSet = false;
        public static Excel.Range range;
        private DataTable dt_Origin = null, dt_selecteditems = null, dt_defaultTable = null;
        private static QuestionSettings question = new QuestionSettings(); //to get the details of selected question 
        private DataProcessHelper dbHelper = new DataProcessHelper();
        private QC4Common.Util.QSUtil qsUtil = new QC4Common.Util.QSUtil();
        private QC4Common.Util.FormUtil formUtil = new QC4Common.Util.FormUtil();
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        public Group(Excel.Workbook Workbook, int read_row, int write_row, string stdProcessingtype, string stdprocessingoption)
        {
            workbook = Workbook;
            readRow = read_row;
            writeRow = write_row;
            processingType = stdProcessingtype;
            processingOption = stdprocessingoption;
            dictionary = Definiotion.VariableDictionary;
            InitializeComponent();
            LoadControls();
        }

       
        private void Command_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>Logic for loading data to the controls while the window is loaded,during Create, Edit and Copy.</summary>
        /// <param name="sender">The source of the event, Group window.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
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
                        //EDIT_OR_COPY_Process_Details.
                        if (obj[1, 3] != null)
                        {
                            PopulateValuesToModify(range_Edit_Copy, obj, processingOption);
                        }
                    }
                    if (processingOption == QC4Common.Common.Constants.STD_DP.Process_Edit)
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
            catch(Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        /// <summary>For loading the controls and default values into Group window.</summary>
        /// <exception cref="Exception"></exception>
        private void LoadControls()
        {
            try
            {
                LoadVariablesFromSheet();
                //add values to the combobox for Answertypes for filtering variables
                answertypes = new List<string>();
                answertypes.Add("N");
                answertypes.Add("SA");
                Combo_OriginItem_ListFilter.ItemsSource = answertypes;
                Combo_OriginItem_ListFilter.SelectedItem = answertypes.FirstOrDefault();
                Text_NewItem_AnswerType.Text = answertypes.FirstOrDefault();
                // add values to the combobox for Commands
                commands = new List<string>();
                commands.Add("MAX");
                commands.Add("MIN");
                commands.Add("AVG");
                commands.Add("SUM");
                Combo_Commands.ItemsSource = commands;
                Combo_Commands.SelectedItem = commands.FirstOrDefault();
            }
            
            catch(Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }


        /// <summary>Logic for handling the change in Command selection.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void Combo_Commands_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string selectedCommand = Combo_Commands.SelectedItem.ToString(); //label changes according to the command changes
                switch (selectedCommand)
                {
                    case "MAX":
                        Text_Command_Desc.Text = LocalResource.LBL_COMMAND_MAX_DESC;
                        Textbox_ProcessMethod.Text = LocalResource.LBL_COMMAND_MAX_METHOD;
                        break;

                    case "MIN":
                        Text_Command_Desc.Text = LocalResource.LBL_COMMAND_MIN_DESC;
                        Textbox_ProcessMethod.Text = LocalResource.LBL_COMMAND_MIN_METHOD;
                        break;

                    case "AVG":
                        Text_Command_Desc.Text = LocalResource.LBL_COMMAND_AVG_DESC;
                        Textbox_ProcessMethod.Text = LocalResource.LBL_COMMAND_AVG_METHOD;
                        break;

                    case "SUM":
                        Text_Command_Desc.Text = LocalResource.LBL_COMMAND_SUM_DESC;
                        Textbox_ProcessMethod.Text = LocalResource.LBL_COMMAND_SUM_METHOD;
                        break;

                    default:
                        break;
                }
                Combo_OriginItem_ListFilter.SelectedItem = answertypes.FirstOrDefault();
                if (Grid_OriginItem_ChoiceList2.Items.Count > 0) { Command_Entry.IsEnabled = true; } else { Command_Entry.IsEnabled = false; }
            }
            catch(Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }


        /// <summary>Logic for handling the changes during Answer type selection for filtering variables.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        /// <exception cref="Exception"></exception>
        private void Combo_OriginItem_ListFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //controls and values of the form changes according to the selection change of Answer type
            Grid_OriginItem_ChoiceList1.DataContext = null;
            dt_Origin = CreateTable();
            dt_defaultTable = CreateTable();
            dt_selecteditems = CreateTable();
            //Grid_OriginItem_ChoiceList2.DataContext = dt_selecteditems;
            string selectedFilterType = Combo_OriginItem_ListFilter.SelectedItem.ToString();
            try
            {
                switch (selectedFilterType)
                {
                    case "N":
                        dt_Origin = FillDataGrid(indexListN.ToArray(), choiceListN.ToArray());
                        dt_defaultTable = FillDataGrid(indexListN.ToArray(), choiceListN.ToArray());
                        Grid_OriginItem_ChoiceList1.DataContext = dt_Origin;
                        ResetForm(processingOption);
                        break;

                    case "SA":
                        dt_Origin = FillDataGrid(indexListSA.ToArray(), choiceListSA.ToArray());
                        dt_defaultTable = FillDataGrid(indexListSA.ToArray(), choiceListSA.ToArray());
                        Grid_OriginItem_ChoiceList1.DataContext = dt_Origin;
                        ResetForm(processingOption);
                        Lbl_No_of_Choices.Visibility = Visibility.Visible;
                        Text_OriginItem_SelectCount.Visibility = Visibility.Visible;
                        break;

                    default:
                        break;
                }
            }
            catch(Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
           
        }


        /// <summary>For resetting the controls and default values of Group window.</summary>
        /// <param name="prcOption">The processing option value that indicates whether the window is in Create, Edit or Copy process.</param>
        private void ResetForm(string prcOption)
        {
            try
            {
                Grid_OriginItem_ChoiceList2.DataContext = null;
                Lbl_No_of_Choices.Visibility = Visibility.Hidden;
                Text_OriginItem_SelectCount.Visibility = Visibility.Hidden;
                if (prcOption == QC4Common.Common.Constants.STD_DP.Process_Create)
                {
                    Combo_NewItem_Item.Text = string.Empty;
                    Text_NewItem_Question.Text = string.Empty;
                    Combo_NewItem_Item.IsEnabled = false;
                    Text_NewItem_Question.IsEnabled = false;
                    Button_Search.IsEnabled = false;
                    Button_Search.Opacity = 0.5;
                    Command_Entry.IsEnabled = false;
                    Combo_NewItem_Item.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
                    Text_NewItem_Question.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
                }
                else if (prcOption == QC4Common.Common.Constants.STD_DP.Process_Edit)
                {
                    Combo_NewItem_Item.IsEnabled = true;
                    Combo_NewItem_Item.IsReadOnly = true;
                    Combo_NewItem_Item.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
                    Text_NewItem_Question.IsEnabled = true;
                    Text_NewItem_Question.Background = Brushes.White;
                    Button_Search.IsEnabled = false;
                    Button_Search.Opacity = 0.5;
                    Command_Entry.IsEnabled = true;
                }
                else if (prcOption == QC4Common.Common.Constants.STD_DP.Process_Copy && isCopyProcessInitialLoad)
                {
                    Combo_NewItem_Item.IsEnabled = true;
                    Combo_NewItem_Item.IsReadOnly = false;
                    Combo_NewItem_Item.Background = Brushes.White;
                    Text_NewItem_Question.IsEnabled = true;
                    Text_NewItem_Question.Background = Brushes.White;
                    Button_Search.IsEnabled = true;
                    Button_Search.Opacity = 1.0;
                    Command_Entry.IsEnabled = true;
                }
            }
            catch(Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }


        /// <summary>For reading variables from the sheet and saving into corresponding lists.</summary>
        /// <exception cref="Exception"></exception>
        private void LoadVariablesFromSheet()
        {
            choiceListSA = new List<string>();
            choiceListN = new List<string>();
            indexListN = new List<int>();
            indexListSA = new List<int>();
            int variableIndexN = 0;
            int variableIndexSA = 0;
            var SettingSheet = ExcelUtil.GetWorkSheetByCodeName(workbook, "Sheet91");
            range = SettingSheet.get_Range("List_Item_SAN");
            try
            {
                if (range.Cells.Count > 1)
                {
                    object[,] rangearray = range.Value;
                    for (int i = 2; i <= rangearray.GetLength(0); i++)
                    {
                        if (Convert.ToString(rangearray[i, 1]) != string.Empty && dictionary.ContainsKey(Convert.ToString(rangearray[i, 1])))
                        {
                            question = dictionary[Convert.ToString(rangearray[i, 1])];
                            if (question.AnswerType == QuestionType.N.ToString())
                            {
                                choiceListN.Add(Convert.ToString(rangearray[i, 1]));
                                variableIndexN++;
                                indexListN.Add(variableIndexN);
                            }
                            else if (question.AnswerType == QuestionType.SA.ToString())
                            {
                                choiceListSA.Add(Convert.ToString(rangearray[i, 1]));
                                variableIndexSA++;
                                indexListSA.Add(variableIndexSA);
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
                            question = dictionary[Convert.ToString(range.Value)];
                            if (question.AnswerType == QuestionType.N.ToString())
                            {
                                choiceListN.Add(Convert.ToString(range.Value));
                                variableIndexN++;
                                indexListN.Add(variableIndexN);
                            }
                            else if (question.AnswerType == QuestionType.SA.ToString())
                            {
                                choiceListSA.Add(Convert.ToString(range.Value));
                                variableIndexSA++;
                                indexListSA.Add(variableIndexSA);
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


        /// <summary>Logic for populating the Group window for Edit and Copy processes.</summary>
        /// <param name="dpRange">The range in the hidden sheet where the process was saved.</param>
        /// <param name="objArray">The values stored in the range, used to retrieve command, new variable and all parameters.</param>
        /// <param name="toModify">To indicate whether we need to edit the process or to copy the process and create new.</param>
        private void PopulateValuesToModify(Excel.Range dpRange, object[,] objArray, string toModify)
        {
            try
            {
                //New Variable Name in Textbox
                string editedName = string.Empty;
                string editedQuestionText = string.Empty;
                string savedVariableName = objArray[1, QC4Common.Common.Constants.DP.SubstituteVariableColumn].ToString();
                if (processingOption == QC4Common.Common.Constants.STD_DP.Process_Edit)
                    editedName = savedVariableName;
                else if (processingOption == QC4Common.Common.Constants.STD_DP.Process_Copy)
                {
                    isCopyProcessInitialLoad = true;
                    editedName = qsUtil.GetVariableName(savedVariableName, dictionary.Values.ToList());
                }
              
                //New variable QuestionText
                QuestionSettings qs = new QuestionSettings();
                bool hasvalue = Util.Definiotion.VariableDictionary.TryGetValue(savedVariableName, out qs);
                if (qs != null)
                {
                    editedQuestionText = qs.Question;
                    //Processing command in Combobox
                    string savedCommand = objArray[1, QC4Common.Common.Constants.DP.SubstituteOperatorColumn].ToString();
                    foreach (string cm in commands)
                    {
                        if (cm == savedCommand)
                        {
                            Combo_Commands.SelectedItem = cm;
                            break;
                        }
                    }

                    //Details of SourceVariable in Corresponding controls
                    dt_defaultTable = CreateTable();
                    dt_Origin = CreateTable();
                    string firstParam = objArray[1, QC4Common.Common.Constants.DP.SubstituteParam1Column].ToString();
                    qs = dictionary[firstParam];
                    if (qs.AnswerType == QuestionType.N.ToString())
                    {
                        Combo_OriginItem_ListFilter.SelectedItem = answertypes.FirstOrDefault(); //AnswerType of source variable in combobox
                        dt_defaultTable = FillDataGrid(indexListN.ToArray(), choiceListN.ToArray());
                        dt_Origin = FillDataGrid(indexListN.ToArray(), choiceListN.ToArray());
                    }
                    else if (qs.AnswerType == QuestionType.SA.ToString())
                    {
                        Combo_OriginItem_ListFilter.SelectedItem = answertypes[1]; //AnswerType of source variable in combobox
                        dt_defaultTable = FillDataGrid(indexListSA.ToArray(), choiceListSA.ToArray());
                        dt_Origin = FillDataGrid(indexListSA.ToArray(), choiceListSA.ToArray());
                    }

                    if (processingOption == QC4Common.Common.Constants.STD_DP.Process_Edit)
                    {
                        DataRow dr = dt_defaultTable.Rows[0];
                        qs = dictionary[dr[1].ToString()];
                        Text_OriginItem_Item.Text = qs.Variable; //name of source variable in textbox
                        Text_OriginItem_AnswerType.Text = qs.AnswerType; //AnswerType of source variable in textbox
                        if (qs.AnswerType == QuestionType.SA.ToString())
                            Text_OriginItem_SelectCount.Text = qs.CategoryCount.ToString();
                        Text_OriginItem_Question.Text = qs.Question;//question text of source variable
                    }


                    //Populate variable list in Grid1 and Grid2
                    dt_selecteditems = CreateTable();
                    Grid_OriginItem_ChoiceList1.DataContext = null;
                    for (int i = QC4Common.Common.Constants.DP.SubstituteParam1Column; i <= objArray.Length; i++)
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

                    ResetForm(processingOption);
                    Grid_OriginItem_ChoiceList2.DataContext = dt_selecteditems;
                    Grid_OriginItem_ChoiceList1.DataContext = dt_Origin;
                    if (qs.AnswerType == QuestionType.N.ToString())
                    {
                        Lbl_No_of_Choices.Visibility = Visibility.Hidden;
                        Text_OriginItem_SelectCount.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        Lbl_No_of_Choices.Visibility = Visibility.Visible;
                        Text_OriginItem_SelectCount.Visibility = Visibility.Visible;
                    }
                    Combo_NewItem_Item.Text = editedName;
                    Text_NewItem_Question.Text = editedQuestionText;
                    isCopyProcessInitialLoad = false;

                }
                
            }
            catch(Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        /// <summary>Logic for adding selected variables from Grid1 to Grid2.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        /// <exception cref="Exception"></exception>
        private void Command_OriginItem_Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRow dr;
                //DataTable tempTable = CreateTable();
                variables = new List<string>();
                //dt_selecteditems = CreateTable();
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
                if (dt_selecteditems.Rows.Count == 0)
                    Grid_OriginItem_ChoiceList2.DataContext = null; 
                else
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
                        continue;
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
                Grid_OriginItem_ChoiceList1.DataContext = null;
                Grid_OriginItem_ChoiceList1.DataContext = dt_Origin;
                Text_OriginItem_Item.Text = string.Empty;
                Text_OriginItem_AnswerType.Text = string.Empty;
                Text_OriginItem_SelectCount.Text = string.Empty;
                Text_OriginItem_Question.Text = string.Empty;
                //setting values for new variable handled on the events Grid_OriginItem_ChoiceList2_DataContextChanged()

            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

        }


        /// <summary>Logic for removing selected variables from Grid2 and adding to Grid1.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        /// <exception cref="Exception"></exception>
        private void Command_OriginItem_Remove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRow dr;
                variables = new List<string>();
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
                        continue;
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
                {
                    Grid_OriginItem_ChoiceList2.DataContext = null;
                    isNewVariableSet = false;
                }
                else
                    Grid_OriginItem_ChoiceList2.DataContext = dt_selecteditems;

                Text_OriginItem_Item.Text = string.Empty;
                Text_OriginItem_AnswerType.Text = string.Empty;
                Text_OriginItem_SelectCount.Text = string.Empty;
                Text_OriginItem_Question.Text = string.Empty;
                //clearing new variable values while Grid2 is empty handled on the event Grid_OriginItem_ChoiceList2_DatacontextChanged()
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }


        /// <summary>Logic for handling the changes during Grid1 item selection .</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        /// <exception cref="Exception"></exception>
        private void Grid_OriginItem_ChoiceList1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if(Grid_OriginItem_ChoiceList1.SelectedItem != null && !isTabInGrid)
                {
                    DataRowView drv = Grid_OriginItem_ChoiceList1.SelectedItems.Cast<DataRowView>().LastOrDefault();
                    string variable = drv["Variable"].ToString();
                    question = dictionary[variable];
                    Text_OriginItem_Item.Text = question.Variable;
                    Text_OriginItem_AnswerType.Text = question.AnswerType;
                    Text_OriginItem_Question.Text = question.Question;
                    Text_OriginItem_SelectCount.Text = Convert.ToString(question.CategoryCount);
                }
                
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }   
        }


        /// <summary>Logic for handling the changes during Grid2 item selection .</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        /// <exception cref="Exception"></exception>
        private void Grid_OriginItem_ChoiceList2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (Grid_OriginItem_ChoiceList2.SelectedItem != null && !isTabInGrid)
                {
                    DataRowView drv = Grid_OriginItem_ChoiceList2.SelectedItems.Cast<DataRowView>().LastOrDefault();
                    string variable = drv["Variable"].ToString();
                    question = dictionary[variable];
                    Text_OriginItem_Item.Text = question.Variable;
                    Text_OriginItem_AnswerType.Text = question.AnswerType;
                    Text_OriginItem_Question.Text = question.Question;
                    Text_OriginItem_SelectCount.Text = Convert.ToString(question.CategoryCount);
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        /// <summary>Logic for handling Magnifying glass button click event for searching new variable.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (MagnifyingGlassButton.VariableList != null)
            {
                Combo_NewItem_Item.Text = MagnifyingGlassButton.VariableList.Variable;
                newVariableNameTemp = MagnifyingGlassButton.VariableList.Variable;
                Text_NewItem_Question.Text = (formUtil.UnEscapeCRLF(MagnifyingGlassButton.VariableList.Title) + " " + formUtil.UnEscapeCRLF(MagnifyingGlassButton.VariableList.Question)).TrimEnd().TrimStart();
                newVariableQuestionTemp = Text_NewItem_Question.Text;
                isNewQuestion = false;
            }
            
        }

        /// <summary>Logic for handling the single quote at the begining of a text .</summary>
        /// <param name="text">The text to be validated.</param>
        /// <returns >The validated text</returns>
        private string ValidationSingleQuote(string text)
        {
            if (text.StartsWith("'"))
            {
                text = "'" + text;
            }
            return text;

        }

        /// <summary>Logic for saving the Group Process to the hidden sheet with all parameters.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Command_Entry_Click(object sender, RoutedEventArgs e)
        {
            string command = Combo_Commands.SelectedItem.ToString();
            string newVariable = ((Combo_NewItem_Item.Text).TrimStart().TrimEnd());
            string newQuestion = ((Text_NewItem_Question.Text).TrimStart().TrimEnd());
            List<DataGT> variablesChoiceList2 = new List<DataGT>();
            if (string.IsNullOrEmpty(newQuestion))
            {
                MessageDialog.ErrorOk(LocalResource.ALERT_DP_NO_QUESTION);
            }
            else
            {
                try
                {
                    //check whether user has edited the variable name or question text
                    if(processingOption != QC4Common.Common.Constants.STD_DP.Process_Edit && (newVariable != newVariableNameTemp || newQuestion != newVariableQuestionTemp))
                    {
                        isNewQuestion = true;
                    }
                    if (!string.IsNullOrEmpty(newVariable)  && dictionary.ContainsKey(newVariable))
                    {
                        QuestionSettings qs = dictionary[newVariable];
                        if (qs.AnswerType != Text_NewItem_AnswerType.Text || qs.QuestionFlag != QC4Common.Common.Constants.QuestionFlag.New)
                        {
                            isNewQuestion = true;
                        }
                        else if (qs.AnswerType == Text_NewItem_AnswerType.Text && qs.QuestionFlag == QC4Common.Common.Constants.QuestionFlag.New)
                        {
                                isNewQuestion = false;
                        }
                        if (formUtil.UnEscapeCRLF(qs.Question) != newQuestion)
                        {
                            isUpdateQuestion = true;
                        }
                    }

                    // check whether the new variable name already exists in VariableDictionary
                    if (processingOption != QC4Common.Common.Constants.STD_DP.Process_Edit && (!formUtil.IsVariableNameExists(newVariable, dictionary.Values.ToList(), 1) || string.IsNullOrEmpty(newVariable)))
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
                                newVariable = qsUtil.GetVariableName(newVariable, dictionary.Values.ToList());
                                Combo_NewItem_Item.Text = newVariable;
                                isNewQuestion = true;
                                isUpdateQuestion = false;
                            }
                            else
                            {
                                DataRow dr = dt_selecteditems.Rows[0];
                                string firstSourceVariable = dr[1].ToString();
                                newVariable = qsUtil.GetVariableName(firstSourceVariable, dictionary.Values.ToList());
                                Combo_NewItem_Item.Text = newVariable;
                                isNewQuestion = true;
                                isUpdateQuestion = false;
                            }
                        }
                        else if (result == System.Windows.Forms.DialogResult.No)
                        {
                            return;
                        }
                    }
                    if (processingOption == QC4Common.Common.Constants.STD_DP.Process_Edit && string.IsNullOrEmpty(newVariable))
                    {
                        string errormsg = LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_NEW_ITEM_EMPTY + "\n" + LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_RENAME_NEW_VARIABLE_AUTOMATICALLY;
                        System.Windows.Forms.DialogResult result;
                        result = MessageDialog.InfoYesNo(errormsg);
                        if (result == System.Windows.Forms.DialogResult.Yes)
                        {
                            DataRow dr = dt_selecteditems.Rows[0];
                            string firstSourceVariable = dr[1].ToString();
                            newVariable = qsUtil.GetVariableName(firstSourceVariable, dictionary.Values.ToList());
                            Combo_NewItem_Item.Text = newVariable;
                            isNewQuestion = true;
                            isUpdateQuestion = false;
                        }
                        else if (result == System.Windows.Forms.DialogResult.No)
                        {
                            return;
                        }
                    }
                    if(processingOption != QC4Common.Common.Constants.STD_DP.Process_Edit && !string.IsNullOrEmpty(newVariable))
                    {
                        // check the maxlength of newVariable
                        if (formUtil.IsVariableLengthExceeds(newVariable))
                        {
                            MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_QUESTION_LENGTH_EXCEED, QC4Common.Common.Constants.QsVariableMaxLength)); //ERR_MSG_QUESTION_LENGTH_EXCEED
                            return;
                        }
                        //check the newVariable name is valid or not
                        Util.QS.NewQuestionValidation validation = new Util.QS.NewQuestionValidation(newVariable);
                        if (!validation.Validation_Variable(true))
                        {
                            return;
                        }
                    }

                    //create parameter list of selected variables in Grid2
                    string[] paramList = new string[dt_selecteditems.Rows.Count];
                    for (int i = 0; i < dt_selecteditems.Rows.Count; i++)
                    {
                        DataRow dr = dt_selecteditems.Rows[i];
                        paramList[i] = dr[1].ToString();
                    }

                    int columncount = (QC4Common.Common.Constants.DP.SubstituteOperatorColumn - QC4Common.Common.Constants.DP.OnOffColumn) + paramList.Count() +1; //to identify upto which column we have to write values into the sheet in a single row
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
                                dpsaveinstructios[0, i] = command;
                                break;
                            default:
                                dpsaveinstructios[0, i] = paramList[param];
                                param++;
                                break;

                        }
                    }
                    if (newQuestion.Length > QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit)
                    {
                        newQuestion = newQuestion.PadRight(QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit).Substring(0, QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit);
                    }

                    if (dbHelper.WriteProcess(workbook, Util.Constants.ProcessingType.CreateNewVariable, newVariable, Text_NewItem_AnswerType.Text, newQuestion,
                        0, null, command, dpsaveinstructios, isNewQuestion, writeRow, processingOption, null, isUpdateQuestion))//need to pass the entire row from here for saving 
                    {
                        MessageDialog.Info(LocalResource.ADDQS_INFO_MSG_SUCCESS);
                        isModifiedProcess = true;
                        this.Close();
                    }
                    else
                    {
                        MessageDialog.ErrorOk(string.Format(LocalResource.COMPUTE_ERROR_MSG_CHANGE_VARIABLE_NAME));
                    }
                }
                catch (Exception ex)
                {
                    _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                }
            }
            
        }

        #region Handling Datagrid events
        System.Windows.Controls.DataGrid ExpGrid = null;

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
                dr["Index"] = inddex[i-1];
                dr["Variable"] = variables[i - 1];
                griddata.Rows.Add(dr);
            }
            return griddata;
        }

        private void Grid_OriginItem_ChoiceList1_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
            var x = Grid_OriginItem_ChoiceList1.CurrentCell;
        }


        /// <summary>Logic for setting values for new variable while Grid2 is modified.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private void Grid_OriginItem_ChoiceList2_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
               
                if (Grid_OriginItem_ChoiceList2.DataContext != null)
                {
                    Command_Entry.IsEnabled = true;
                    if (processingOption != QC4Common.Common.Constants.STD_DP.Process_Edit)
                    {
                        Combo_NewItem_Item.IsEnabled = true;
                        Text_NewItem_Question.IsEnabled = true;
                        Button_Search.IsEnabled = true;
                        Button_Search.Opacity = 1;
                        Combo_NewItem_Item.Background = Brushes.White;
                        Text_NewItem_Question.Background = Brushes.White;
                        if (Combo_NewItem_Item.Text == string.Empty && isNewVariableSet == false)
                        {
                            DataRow datarow = dt_selecteditems.Rows[0];
                            question = dictionary[datarow["Variable"].ToString()];
                            Combo_NewItem_Item.Text = qsUtil.GetVariableName(question.Variable, dictionary.Values.ToList()); //creating new variable with the first selected variable 
                            isNewQuestion = true;
                            Text_NewItem_Question.Text = (question.TableHeading + " " + question.Question).TrimStart().TrimEnd();
                            isNewVariableSet = true;
                        }
                    }
                    if (processingOption == QC4Common.Common.Constants.STD_DP.Process_Edit && Combo_NewItem_Item.Text != string.Empty)
                    {
                        Combo_NewItem_Item.IsEnabled = true;
                        Text_NewItem_Question.IsEnabled = true;
                        Button_Search.IsEnabled = false;
                        Button_Search.Opacity = 0.5;
                        Text_NewItem_Question.Background = Brushes.White;
                    }

                }
                else if (Grid_OriginItem_ChoiceList2.DataContext == null)
                {
                    Command_Entry.IsEnabled = false;
                    if (processingOption != QC4Common.Common.Constants.STD_DP.Process_Edit)
                    {
                        Combo_NewItem_Item.Text = string.Empty;
                        Text_NewItem_Question.Text = string.Empty;
                        Combo_NewItem_Item.IsEnabled = false;
                        Text_NewItem_Question.IsEnabled = false;
                        Button_Search.IsEnabled = false;
                        Button_Search.Opacity = 0.5;
                        Combo_NewItem_Item.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
                        Text_NewItem_Question.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

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
            isTabInGrid = false;
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                Command_OriginItem_Add.Focus();
            }
            else
                QC4Common.Util.GridCommonFunc.Arrow(sender, e);
        }

        private void Grid_OriginItem_ChoiceList2_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            isTabInGrid = false;
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                Command_OriginItem_Remove.Focus();
            }
            else
                QC4Common.Util.GridCommonFunc.Arrow(sender, e);
        }

        #endregion

        #region KeyDown Events of other controls to handle tab movement
        private void Command_Cancel_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                Combo_OriginItem_ListFilter.Focus();
            }
        }

        private void Combo_OriginItem_ListFilter_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                Grid_OriginItem_ChoiceList1.Focus();
                Style style = Application.Current.FindResource("MyFocusVisual") as Style;
                Grid_OriginItem_ChoiceList1.FocusVisualStyle = style;
                if (Grid_OriginItem_ChoiceList1.SelectedItem == null && Grid_OriginItem_ChoiceList1.Items.Count > 0)
                {
                    isTabInGrid = true;
                    Grid_OriginItem_ChoiceList1.SelectedIndex = 0;
                }
                   
            }
        }

        private void Command_OriginItem_Remove_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                Combo_NewItem_Item.Focus();
            }
        }

        private void Command_OriginItem_Remove_GotMouseCapture(object sender, MouseEventArgs e)
        {
            if (Grid_OriginItem_ChoiceList2.SelectedItem != null)
            {
                DataRowView drv = Grid_OriginItem_ChoiceList2.SelectedItems.Cast<DataRowView>().LastOrDefault();
                string variable = drv["Variable"].ToString();
                question = dictionary[variable];
                Text_OriginItem_AnswerType.Text = question.AnswerType;
                Text_OriginItem_Item.Text = question.Variable;
                Text_OriginItem_Question.Text = question.Question;
                Text_OriginItem_SelectCount.Text = Convert.ToString(question.CategoryCount);
            }
            else
            {

                Text_OriginItem_AnswerType.Text = string.Empty;
                Text_OriginItem_Item.Text = string.Empty;
                Text_OriginItem_SelectCount.Text = string.Empty;
                Text_OriginItem_Question.Text = string.Empty;

            }
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
                    Text_OriginItem_AnswerType.Text = question.AnswerType;
                    Text_OriginItem_Item.Text = question.Variable;
                    Text_OriginItem_Question.Text = question.Question;
                    Text_OriginItem_SelectCount.Text = Convert.ToString(question.CategoryCount);
                }
                else
                {

                    Text_OriginItem_AnswerType.Text = string.Empty;
                    Text_OriginItem_Item.Text = string.Empty;
                    Text_OriginItem_SelectCount.Text = string.Empty;
                    Text_OriginItem_Question.Text = string.Empty;

                }
            }
            catch (Exception ex) { }
        }
        DataProcessNewLoad dataLoad = new DataProcessNewLoad();
        private void Combo_NewItem_Item_SelectionChanged(object sender, RoutedEventArgs e)
        {
            QuestionSettings qs = dataLoad.Txt_Change_New_Item(Combo_NewItem_Item.Text);
            if (qs != null)
            {
                Text_NewItem_Question.Text = qs.Question;
            }
        }

        private void Command_OriginItem_Add_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                Grid_OriginItem_ChoiceList2.Focus();
                Style style = Application.Current.FindResource("MyFocusVisual") as Style;
                Grid_OriginItem_ChoiceList2.FocusVisualStyle = style;
                if (Grid_OriginItem_ChoiceList2.SelectedItem == null && Grid_OriginItem_ChoiceList2.Items.Count > 0)
                {
                    isTabInGrid = true;
                    Grid_OriginItem_ChoiceList2.SelectedIndex = 0;
                }
                    
            }
        }
        #endregion

        /// <summary>
        /// To diasble right click Copy and Paste in the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisableRightMenu(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }
}
