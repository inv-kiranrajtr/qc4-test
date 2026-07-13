using ExcelAddIn.Common;
using QC4Common.Model;
using Qc4Launcher.Forms.GrossTabulationSetting.Common;
using Qc4Launcher.Logic;
using Qc4Launcher.Util;
using System;
using System.Collections.Generic;
using System.Data;
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
using static FilterSettingsView.FilterSettingsClass;
using Excel = Microsoft.Office.Interop.Excel;
using log4net;
using System.Reflection;

namespace Qc4Launcher.Forms.DP_Main
{
    /// <summary>
    /// Interaction logic for OutputList.xaml
    /// </summary>
    public partial class OutputList : Window
    {
        public static Microsoft.Office.Interop.Excel.Workbook workbook;
        private static List<DataGT> choiceList;
        public static Excel.Range range;
        private Dictionary<String, QuestionSettings> dictionary = new Dictionary<string, QuestionSettings>();
        private static QuestionSettings question = new QuestionSettings();
        DataTable dt_Origin = null, dt_selecteditems = null;
        private DataProcessHelper dbHelper = new DataProcessHelper();
        private int readRow;
        private int writeRow;
        private string processingType;
        private string processingOption;
        private bool isNewQuestion = false;
        private bool isUpdateQuestion = false;
        public bool isModifiedProcess = false;
        private static bool isEditOrCopy = false;
        private bool isTabInGrid = false;
        private static List<String> variables = new List<string>();
        DataTable dt_defaultTable;
        System.Windows.Controls.DataGrid ExpGrid = null;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public OutputList(Excel.Workbook Workbook, int read_row, int write_row, string stdProcessingtype, string stdprocessingoption)
        {
            workbook = Workbook;
            readRow = read_row;
            writeRow = write_row;
            processingType = stdProcessingtype;
            processingOption = stdprocessingoption;
            InitializeComponent();
            dictionary = Definiotion.VariableDictionary;
            TabOnCriteriaControl();
        }

        private void TabOnCriteriaControl()
        {
            this.Criteria_Control.Combo_Conditional_Item_1.TabIndex = 1;
            this.Criteria_Control.Combo_Conditional_Operator_1.TabIndex = 2;
            this.Criteria_Control.Combo_Conditional_Value_1.TabIndex = 3;
            this.Criteria_Control.BTnFilter1.TabIndex = 4;
            this.Criteria_Control.Option_Conditional_And_1.TabIndex = 5;
            this.Criteria_Control.Option_Conditional_Or_1.TabIndex = 6;

            this.Criteria_Control.Combo_Conditional_Item_2.TabIndex = 7;
            this.Criteria_Control.Combo_Conditional_Operator_2.TabIndex = 8;
            this.Criteria_Control.Combo_Conditional_Value_2.TabIndex = 9;
            this.Criteria_Control.BTnFilter2.TabIndex = 10;
            this.Criteria_Control.Option_Conditional_And_2.TabIndex = 11;
            this.Criteria_Control.Option_Conditional_Or_2.TabIndex = 12;

            this.Criteria_Control.Combo_Conditional_Item_3.TabIndex = 13;
            this.Criteria_Control.Combo_Conditional_Operator_3.TabIndex = 14;
            this.Criteria_Control.Combo_Conditional_Value_3.TabIndex = 15;
            this.Criteria_Control.BTnFilter3.TabIndex = 16;
            this.Criteria_Control.Option_Conditional_And_3.TabIndex = 17;
            this.Criteria_Control.Option_Conditional_Or_3.TabIndex = 18;

            this.Criteria_Control.Combo_Conditional_Item_4.TabIndex = 19;
            this.Criteria_Control.Combo_Conditional_Operator_4.TabIndex = 20;
            this.Criteria_Control.Combo_Conditional_Value_4.TabIndex = 21;
            this.Criteria_Control.BTnFilter4.TabIndex = 22;
            this.Criteria_Control.Option_Conditional_And_4.TabIndex = 23;
            this.Criteria_Control.Option_Conditional_Or_4.TabIndex = 24;

            this.Criteria_Control.Combo_Conditional_Item_5.TabIndex = 25;
            this.Criteria_Control.Combo_Conditional_Operator_5.TabIndex = 26;
            this.Criteria_Control.Combo_Conditional_Value_5.TabIndex = 27;
            this.Criteria_Control.BTnFilter5.TabIndex = 28;
        }

        private void LoadData()
        {
            int variableIndex = 0;
            choiceList = new List<DataGT>();
            var SettingSheet = ExcelAddIn.Common.ExcelUtil.GetWorkSheetByCodeName(workbook, "Sheet91");
            var Variable = new List<string>();
            dt_selecteditems = CreateTable();
            dt_defaultTable = CreateTable();

            range = SettingSheet.get_Range("List_Item_ALLD");

            try
            {
                if (range.Cells.Count > 1)
                {
                    var rangearray = range.Value;
                    for (int i = 1; i <= rangearray.GetLength(0); i++)
                    {
                        if (Convert.ToString(rangearray[i, 1]) != string.Empty && dictionary.ContainsKey(Convert.ToString(rangearray[i, 1])))
                        {
                            question = dictionary[Convert.ToString(rangearray[i, 1])];
                            Variable.Add(question.Variable);
                        }
                    }
                }
                else if (range.Cells.Count == 1)
                {
                    string value = range.Value;
                    if (value != string.Empty && dictionary.ContainsKey(value))
                    {
                        question = dictionary[value];
                        Variable.Add(question.Variable);
                    }
                }

                dt_defaultTable = FillDataGrid(Variable.Count, Variable.ToArray());
                dt_Origin = FillDataGrid(Variable.Count, Variable.ToArray());
                data_grid.DataContext = dt_Origin;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

        }

        private DataTable CreateTable()
        {
            DataTable griddata = new DataTable();
            griddata.Columns.Add("No");
            griddata.Columns.Add("Choice");
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
                dr["Choice"] = choices[i - 1];
                griddata.Rows.Add(dr);
            }
            return griddata;
        }

        private void Command_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private string ValidationSingleQuote(string text)
        {
            if (text.StartsWith("'"))
            {
                text = "'" + text;
            }
            return text;

        }

        private void Command_OriginItem_Add_Click(object sender, RoutedEventArgs e)
        {

            DataRow dr;
            variables = new List<string>();
            try
            {
                foreach (DataRowView drv in data_grid.SelectedItems)
                {

                    string no = drv["No"].ToString();
                    string choice = drv["Choice"].ToString();
                    dr = dt_selecteditems.NewRow();
                    dr["No"] = no;
                    dr["Choice"] = choice;
                    dt_selecteditems.Rows.Add(dr);

                }           
                dt_selecteditems.DefaultView.Sort = "No";
                dt_selecteditems.AcceptChanges();
                DataTable test = dt_selecteditems.Clone();
                test.Columns["No"].DataType = Type.GetType("System.Int32");
                foreach (DataRow dr1 in dt_selecteditems.Rows)
                {
                    test.ImportRow(dr1);
                }
                test.AcceptChanges();
                DataView dv = test.DefaultView;
                dv.Sort = "No ASC";
                dt_selecteditems = dv.ToTable();
                selecteditems_data_grid.DataContext = dt_selecteditems;
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

                data_grid.DataContext = dt_Origin;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

        }

        private void Command_OriginItem_Remove_Click(object sender, RoutedEventArgs e)
        {

            DataRow dr;
            variables = new List<string>();
            try
            {
                foreach (DataRowView drv in selecteditems_data_grid.SelectedItems)
                {
                    int no = int.Parse(drv["No"].ToString());
                    string choice = drv["Choice"].ToString();
                    dr = dt_Origin.NewRow();
                    dr["No"] = no;
                    dr["Choice"] = choice;
                    dt_Origin.Rows.Add(dr);
                }
                DataTable test = dt_Origin.Clone();
                test.Columns["No"].DataType = Type.GetType("System.Int32");
                foreach (DataRow dr1 in dt_Origin.Rows)
                {
                    test.ImportRow(dr1);
                }
                test.AcceptChanges();
                DataView dv = test.DefaultView;
                dv.Sort = "No ASC";
                dt_Origin = dv.ToTable();
                data_grid.DataContext = dt_Origin;

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
                    selecteditems_data_grid.DataContext = null;
                else
                    selecteditems_data_grid.DataContext = dt_selecteditems;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void Data_grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (data_grid.SelectedItem != null && !isTabInGrid)
                {
                    DataRowView drv = data_grid.SelectedItems.Cast<DataRowView>().LastOrDefault();
                    string variable = drv["Choice"].ToString();
                    question = dictionary[variable];
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

        private void Selecteditems_data_grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (selecteditems_data_grid.SelectedItem != null && !isTabInGrid)
                {
                    DataRowView drv = selecteditems_data_grid.SelectedItems.Cast<DataRowView>().LastOrDefault();
                    string variable = drv["Choice"].ToString();
                    question = dictionary[variable];
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

        private void Command_Entry_Click(object sender, RoutedEventArgs e)
        {
            List<QuestionSettings> questionSettings = new List<QuestionSettings>();
            questionSettings = dictionary.Values.ToList();
            QuestionSettings qs = new QuestionSettings();
            string[] paramList = new string[dt_selecteditems.Rows.Count];
            for (int i = 0; i < dt_selecteditems.Rows.Count; i++)
            {
                DataRow dr = dt_selecteditems.Rows[i];
                paramList[i] = dr[1].ToString();
            }
            isNewQuestion = false;//because it's not a new question
            isUpdateQuestion = false;//because ther's no need to update QS 

            if (!Criteria_Control.ValidateCriteriaControls())
            {
                return;
            }
            else
            {
                int rowCount = 1;
                string command = string.Empty;
                if (!string.IsNullOrEmpty(Criteria_Control.Combo_Conditional_Item_2.Text))
                    rowCount = 2;
                if (!string.IsNullOrEmpty(Criteria_Control.Combo_Conditional_Item_3.Text))
                    rowCount = 3;
                if (!string.IsNullOrEmpty(Criteria_Control.Combo_Conditional_Item_4.Text))
                    rowCount = 4;
                if (!string.IsNullOrEmpty(Criteria_Control.Combo_Conditional_Item_5.Text))
                    rowCount = 5;
                int param = 0;
                int colCount = (QC4Common.Common.Constants.DP.SubstituteParam1Column - QC4Common.Common.Constants.DP.OnOffColumn) + (dt_selecteditems.Rows.Count); //to identify upto which column we have to write values into the sheet in a single row
                string[,] dpAddMinusInstructions = new string[rowCount, QC4Common.Common.Constants.DP.MAX_DP_COLUMN - QC4Common.Common.Constants.DP.OnOffColumn];//SAVE Array for the corresponding row in the sheet

                try
                {
                    for (int i = 0; i < rowCount; i++)
                    {
                        param = 0;
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
                                    break;

                                case 3://criteria operator
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
                                    break;

                                case 4://criteria value
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
                                    break;

                                case 5://AND || OR|| LISTUP
                                    if (i == 0)
                                    {
                                        if (Criteria_Control.Option_Conditional_And_1.IsChecked == false && Criteria_Control.Option_Conditional_Or_1.IsChecked == false)
                                            dpAddMinusInstructions[i, j] = ExcelAddIn.Common.Constants.DP.InstructionLISTUP;
                                        else if (Criteria_Control.Option_Conditional_And_1.IsChecked == true && Criteria_Control.Option_Conditional_Or_1.IsChecked == false)
                                            dpAddMinusInstructions[i, j] = ExcelAddIn.Common.Constants.DP.InstructionAND;
                                        else if (Criteria_Control.Option_Conditional_And_1.IsChecked == false && Criteria_Control.Option_Conditional_Or_1.IsChecked == true)
                                            dpAddMinusInstructions[i, j] = ExcelAddIn.Common.Constants.DP.InstructionOR;
                                    }
                                    else if (i == 1)
                                    {
                                        if (Criteria_Control.Option_Conditional_And_2.IsChecked == false && Criteria_Control.Option_Conditional_Or_2.IsChecked == false)
                                            dpAddMinusInstructions[i, j] = ExcelAddIn.Common.Constants.DP.InstructionLISTUP;
                                        else if (Criteria_Control.Option_Conditional_And_2.IsChecked == true && Criteria_Control.Option_Conditional_Or_2.IsChecked == false)
                                            dpAddMinusInstructions[i, j] = ExcelAddIn.Common.Constants.DP.InstructionAND;
                                        else if (Criteria_Control.Option_Conditional_And_2.IsChecked == false && Criteria_Control.Option_Conditional_Or_2.IsChecked == true)
                                            dpAddMinusInstructions[i, j] = ExcelAddIn.Common.Constants.DP.InstructionOR;
                                    }
                                    else if (i == 2)
                                    {
                                        if (Criteria_Control.Option_Conditional_And_3.IsChecked == false && Criteria_Control.Option_Conditional_Or_3.IsChecked == false)
                                            dpAddMinusInstructions[i, j] = ExcelAddIn.Common.Constants.DP.InstructionLISTUP;
                                        else if (Criteria_Control.Option_Conditional_And_3.IsChecked == true && Criteria_Control.Option_Conditional_Or_3.IsChecked == false)
                                            dpAddMinusInstructions[i, j] = ExcelAddIn.Common.Constants.DP.InstructionAND;
                                        else if (Criteria_Control.Option_Conditional_And_3.IsChecked == false && Criteria_Control.Option_Conditional_Or_3.IsChecked == true)
                                            dpAddMinusInstructions[i, j] = ExcelAddIn.Common.Constants.DP.InstructionOR;
                                    }
                                    else if (i == 3)
                                    {
                                        if (Criteria_Control.Option_Conditional_And_4.IsChecked == false && Criteria_Control.Option_Conditional_Or_4.IsChecked == false)
                                            dpAddMinusInstructions[i, j] = ExcelAddIn.Common.Constants.DP.InstructionLISTUP;
                                        else if (Criteria_Control.Option_Conditional_And_4.IsChecked == true && Criteria_Control.Option_Conditional_Or_4.IsChecked == false)
                                            dpAddMinusInstructions[i, j] = ExcelAddIn.Common.Constants.DP.InstructionAND;
                                        else if (Criteria_Control.Option_Conditional_And_4.IsChecked == false && Criteria_Control.Option_Conditional_Or_4.IsChecked == true)
                                            dpAddMinusInstructions[i, j] = ExcelAddIn.Common.Constants.DP.InstructionOR;
                                    }
                                    else if (i == 4)
                                        dpAddMinusInstructions[i, j] = ExcelAddIn.Common.Constants.DP.InstructionLISTUP;
                                    break;

                                case 6://no substitute variable
                                    break;

                                case 7:
                                    //no substitute operator
                                    break;

                                default://parameter list
                                    if (!string.IsNullOrEmpty(dpAddMinusInstructions[i, 5]) && (dpAddMinusInstructions[i, 5] == ExcelAddIn.Common.Constants.DP.InstructionLISTUP))
                                    {
                                        dpAddMinusInstructions[i, j] = paramList[param];
                                        param++;
                                    }
                                    break;

                            }
                        }
                    }

                    if (dbHelper.WriteProcess(workbook, Util.Constants.ProcessingType.Exclude, null, null, null,
                        int.Parse(Text_OriginItem_SelectCount.Text), null, command, dpAddMinusInstructions, isNewQuestion, writeRow, processingOption, null, isUpdateQuestion))//need to pass the entire row from here for saving 
                    {
                        ExcelAddIn.Common.MessageDialog.Info(LocalResource.ADDQS_INFO_MSG_SUCCESS);
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

        private void selecteditems_data_grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (selecteditems_data_grid.Items.Count != 0)
            {
                Command_Entry.IsEnabled = true;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Criteria_Control.EnableCriteriaControl();
            Criteria_Control.LoadingData(workbook);
            Criteria_Control.Combo_Conditional_Item_1.Width = 120;
            Criteria_Control.Combo_Conditional_Item_2.Width = 120;
            Criteria_Control.Combo_Conditional_Item_3.Width = 120;
            Criteria_Control.Combo_Conditional_Item_4.Width = 120;
            Criteria_Control.Combo_Conditional_Item_5.Width = 120;
            Criteria_Control.Combo_Conditional_Operator_1.Width = 70;
            Criteria_Control.Combo_Conditional_Operator_2.Width = 70;
            Criteria_Control.Combo_Conditional_Operator_3.Width = 70;
            Criteria_Control.Combo_Conditional_Operator_4.Width = 70;
            Criteria_Control.Combo_Conditional_Operator_5.Width = 70;
            Criteria_Control.Combo_Conditional_Value_1.Width = 70;
            Criteria_Control.Combo_Conditional_Value_2.Width = 70;
            Criteria_Control.Combo_Conditional_Value_3.Width = 70;
            Criteria_Control.Combo_Conditional_Value_4.Width = 70;
            Criteria_Control.Combo_Conditional_Value_5.Width = 70;
            Thickness margin = Criteria_Control.Option_Conditional_Or_1.Margin;
            margin.Left = 57;
            Criteria_Control.Option_Conditional_Or_1.Margin = margin;
            Criteria_Control.Option_Conditional_Or_2.Margin = margin;
            Criteria_Control.Option_Conditional_Or_3.Margin = margin;
            Criteria_Control.Option_Conditional_Or_4.Margin = margin;

            try
            {
                LoadData();
                if (processingOption == QC4Common.Common.Constants.STD_DP.Process_Edit || processingOption == QC4Common.Common.Constants.STD_DP.Process_Copy)
                {
                    Excel.Worksheet dataProcessSheet = Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Qc4Launcher.Util.Constants.SheetCodeName.DataProcessS);
                    Excel.Range thenRow = dataProcessSheet.Cells[readRow, 1];
                    int firstRow = dbHelper.GetCurrentProcessFirstRow(readRow, workbook);
                    Excel.Range dpStart = dataProcessSheet.Cells[firstRow, 1];
                    Excel.Range dpEnd = dbHelper.GetLastCellInRow(thenRow);
                    var obj1 = dpEnd.Value;
                    Excel.Range range_Edit_Copy = dataProcessSheet.Range[dpStart, dpEnd];


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

        private void selecteditems_data_grid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (selecteditems_data_grid.DataContext != null)
            {
                if (dt_selecteditems.Rows.Count > 0)
                {
                    Command_Entry.IsEnabled = true;
                }
                else
                { Command_Entry.IsEnabled = false; }
            }
            else if (selecteditems_data_grid.DataContext == null)
            {
                Command_Entry.IsEnabled = false;
            }
            if (selecteditems_data_grid.Items.Count >= 1000)
            {
                ExcelAddIn.Common.MessageDialog.ErrorOk(LocalResource.ERR_MSG_LISTUP);
                return;
            }
        }

        private void grid_CurrentCellChanged(object sender, EventArgs e)
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
        private void data_grid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            isTabInGrid = false;
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                if (Command_Entry.IsEnabled)
                    Command_Entry.Focus();
                else
                    Command_Cancel.Focus();
            }
            else
                QC4Common.Util.GridCommonFunc.Arrow(sender, e);
        }

        private void selecteditems_data_grid_PreviewKeyDown(object sender, KeyEventArgs e)
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

        private void Command_OriginItem_Add_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                selecteditems_data_grid.Focus();
                Style style = Application.Current.FindResource("MyFocusVisual") as Style;
                selecteditems_data_grid.FocusVisualStyle = style;
                if (selecteditems_data_grid.SelectedItem == null && selecteditems_data_grid.Items.Count > 0)
                {
                    isTabInGrid = true;
                    selecteditems_data_grid.SelectedIndex = 0;
                }
            }
        }

        private void Command_OriginItem_Remove_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                data_grid.Focus();
                Style style = Application.Current.FindResource("MyFocusVisual") as Style;
                data_grid.FocusVisualStyle = style;
                if (data_grid.SelectedItem == null && data_grid.Items.Count > 0)
                {
                    isTabInGrid = true;
                    data_grid.SelectedIndex = 0;
                }
            }
        }

        private void Command_Cancel_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                this.Criteria_Control.Combo_Conditional_Item_1.Focus();
            }
        }

        private void Data_grid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            var dg = (System.Windows.Controls.DataGrid)sender;
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
            var x = dg.CurrentCell;
        }

        private void Selecteditems_data_grid_GotMouseCapture(object sender, MouseEventArgs e)
        {
            if (selecteditems_data_grid.SelectedItem != null)
            {
                DataRowView drv = selecteditems_data_grid.SelectedItems.Cast<DataRowView>().LastOrDefault();
                string variable = drv["Choice"].ToString();
                question = dictionary[variable];
                Text_OriginItem_AnswerType.Text = question.AnswerType;
                Text_OriginItem_Question.Text = question.Question;
                Text_OriginItem_SelectCount.Text = Convert.ToString(question.CategoryCount);
            }
            else
            {
                Text_OriginItem_AnswerType.Text = string.Empty;
                Text_OriginItem_SelectCount.Text = string.Empty;
                Text_OriginItem_Question.Text = string.Empty;

            }
        }

        private void Data_grid_GotMouseCapture(object sender, MouseEventArgs e)
        {
            if (data_grid.SelectedItem != null)
            {
                DataRowView drv = data_grid.SelectedItems.Cast<DataRowView>().LastOrDefault();
                string variable = drv["Choice"].ToString();
                question = dictionary[variable];
                Text_OriginItem_AnswerType.Text = question.AnswerType;
                Text_OriginItem_Question.Text = question.Question;
                Text_OriginItem_SelectCount.Text = Convert.ToString(question.CategoryCount);
            }
            else
            {
                Text_OriginItem_AnswerType.Text = string.Empty;
                Text_OriginItem_SelectCount.Text = string.Empty;
                Text_OriginItem_Question.Text = string.Empty;

            }
        }

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
                    else if (instruction == QC4Common.Common.Constants.DP.InstructionLISTUP)
                    {
                        int colnum = QC4Common.Common.Constants.DP.SubstituteParam1Column;
                        dt_Origin = CreateTable();
                        dt_selecteditems = CreateTable();
                        variables = new List<string>();

                        while (colnum <= objArray.GetLength(1))
                        {
                            string variable = objArray[i, colnum].ToString();

                            if (colnum >= 11)
                                variables.Add(variable);
                            colnum++;
                        }
                        for (int j = 0; j < dt_defaultTable.Rows.Count; j++)
                        {
                            DataRow dr = dt_defaultTable.Rows[j];
                            if (variables.Contains(dr[1].ToString()))
                            {
                                DataRow dr1 = dt_selecteditems.NewRow();
                                dr1[0] = dr[0];
                                dr1[1] = dr[1];
                                dt_selecteditems.Rows.Add(dr1);
                                dt_selecteditems.AcceptChanges();
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

                        data_grid.DataContext = null;
                        selecteditems_data_grid.DataContext = null;
                        data_grid.DataContext = dt_Origin;
                        selecteditems_data_grid.DataContext = dt_selecteditems;
                        DataRow dataRow = dt_defaultTable.Rows[0];
                        int no = int.Parse(dataRow[0].ToString());
                        string choice = dataRow[1].ToString();
                        question = dictionary[choice];
                        Text_OriginItem_AnswerType.Text = question.AnswerType;
                        Text_OriginItem_Question.Text = question.Question;
                        Text_OriginItem_SelectCount.Text = Convert.ToString(question.CategoryCount);

                    }
                }

            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }
    }
}
