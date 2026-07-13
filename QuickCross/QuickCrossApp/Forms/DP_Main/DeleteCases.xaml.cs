using System;
using System.Collections.Generic;
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
using Excel = Microsoft.Office.Interop.Excel;
using QC4Common.Model;
using QC4Common.Util;
using Qc4Launcher.Util;
using static FilterSettingsView.FilterSettingsClass;
using ExcelUtil = Qc4Launcher.Util.ExcelUtil;
using System.Text.RegularExpressions;
using Qc4Launcher.Logic;
using System.Data;
using Qc4Launcher.DB;
using System.Data.SQLite;
using log4net;
using System.Reflection;
using Qc4Launcher.Classes;
using Microsoft.VisualBasic.FileIO;


namespace Qc4Launcher.Forms.DP_Main
{
    /// <summary>
    /// Interaction logic for DeleteCases.xaml
    /// </summary>
    public partial class DeleteCases : Window
    {
        public static Excel.Workbook workbook;
        private Dictionary<String, QuestionSettings> dictionary = new Dictionary<string, QuestionSettings>();
        private List<DataExport> dataFromSheet = new List<DataExport>();
        private List<DataExport> dataFromListSheet = new List<DataExport>();
        List<string> sampleIDList = new List<string>();
        IDictionary<string, string> sampleIDListDict = new Dictionary<string, string>();////only for cheking
        List<string> idsToLDELSheet = new List<string>();
        List<string> selectedIDs = new List<string>();
        List<int> sampleIDIndex = new List<int>();
        private static string tableName = "answers";
        private int editParam;
        private int ldelParamCol;
        public static Excel.Range range;
        private string processingType;
        private string processingOption;
        private int readRow;
        private int writeRow;
        private bool isEditDeleteProcess = false;
        private string substOperator = string.Empty;
        private string substParam = string.Empty;
        private bool isNewQuestion = false;
        private bool isUpdateQuestion = false;
        public bool isModifiedProcess = false;
        private DataTable dtSampleIDs = null;
        private int totalCount;
        private int successCount;
        private string fileName = null;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private FormUtil formUtil = new FormUtil();
        private DataProcessHelper dbHelper = new DataProcessHelper();


        public DeleteCases(Excel.Workbook Workbook, int read_row, int write_row, string stdProcessingtype, string stdprocessingoption)
        {
            workbook = Workbook;
            readRow = read_row;
            writeRow = write_row;
            processingType = stdProcessingtype;
            processingOption = stdprocessingoption;
            InitializeComponent();
            GetSampleIDList();
            this.Criteria_Control.Combo_Conditional_Value_1.TextChanged += TextChangedOnCriteriaValue; //to catch the text change in criteria Value textbox
            TabOnCriteriaControl();
        }


        /// <summary>Handles the Loaded event of the Window control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Criteria_Control.LoadingData(workbook);
            SetCriteriaControlMargin();

            if (processingOption == QC4Common.Common.Constants.STD_DP.Process_Create)
            {
                Specify_Criteria.IsChecked = true;
                Criteria_Control.EnableCriteriaControl();
                Grid_SampleIDList.DataContext = null;
                Textbox_ProcessMethod.Foreground = new SolidColorBrush(Colors.Black);
            }
            else if (processingOption == QC4Common.Common.Constants.STD_DP.Process_Edit || processingOption == QC4Common.Common.Constants.STD_DP.Process_Copy)
            {
                Excel.Worksheet dataProcessSheet = Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Qc4Launcher.Util.Constants.SheetCodeName.DataProcessS);
                Excel.Range instrn = dataProcessSheet.Cells[readRow, QC4Common.Common.Constants.DP.InstructionColumn];
                if (instrn.Text == QC4Common.Common.Constants.DP.InstructionDELETE)
                {
                    Excel.Range instrnRow = dataProcessSheet.Cells[readRow, 1];
                    int firstRow = dbHelper.GetCurrentProcessFirstRow(readRow, workbook);
                    Excel.Range dpStart = dataProcessSheet.Cells[firstRow, 1];
                    Excel.Range dpEnd = dbHelper.GetLastCellInRow(instrnRow);
                    Excel.Range range_Edit_Copy = dataProcessSheet.Range[dpStart, dpEnd]; //read the process from hidden sheet

                    if (range_Edit_Copy.Cells.Count > 1)
                    {
                        var obj = range_Edit_Copy.Value;
                        //EDIT_OR_COPY_Process_Details.
                        if (obj[1, 3] != null)
                        {
                            Specify_Criteria.IsChecked = true;
                            Criteria_Control.EnableCriteriaControl();
                            PopulateValuesDELETEInstruction(obj);
                        }
                    }
                    if (processingOption == QC4Common.Common.Constants.STD_DP.Process_Edit)
                    {
                        isEditDeleteProcess = true;
                        writeRow = firstRow;
                    }

                }
                else if (instrn.Text == QC4Common.Common.Constants.DP.InstructionLDEL)
                {
                    Excel.Range rowstart = dataProcessSheet.Cells[readRow, 1];
                    Excel.Range rowend = dbHelper.GetLastCellInRow(rowstart);
                    Excel.Range range_Edit_Copy = dataProcessSheet.Range[rowstart, rowend];

                    if (range_Edit_Copy.Cells.Count > 1)
                    {
                        var obj = range_Edit_Copy.Value;
                        //EDIT_OR_COPY_Process_Details.
                        if (obj[1, 3] != null)
                        {
                            Specify_SampleID.IsChecked = true;
                            EnableLDELControls();
                            Criteria_Control.DisableCriteriaControl();
                            editParam = int.Parse(obj[1, QC4Common.Common.Constants.DP.SubstituteParam1Column]);
                            PopulateValuesLDELInstruction(editParam);
                        }
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

        /// <summary>
        /// Handles the logic to set the margin, width etc of criteria controls.
        /// </summary>
        private void SetCriteriaControlMargin()
        {
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
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
        }


        /// <summary>Handles the click event of Save button to save the instruction to the hidden sheet.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Command_Entry_Click(object sender, RoutedEventArgs e)
        {
            if (Specify_Criteria.IsChecked == true)
            {
                Save_DELETEProcess();
            }
            else if (Specify_SampleID.IsChecked == true)
            {
                Save_LDELProcess();
            }
        }

        /// <summary>Handles the Close event of the Window control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Command_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>To disable the mouse right click event on the Window control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void DisableRightMenu(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        public static void GetTableName(Excel.Workbook book)
        {
            if (DBHelper.checkAfterProcess(book))
            {
                tableName = "data_after_process";
            }
        }

        /// <summary>Handles the click event of Radio button, logic for identifiying the instruction.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        /// <exception cref="Exception"></exception>
        private void OnRadioButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                string buttonName = ((RadioButton)sender).Name;
                var button = (RadioButton)sender;
                if (buttonName == "Specify_Criteria" && button.IsChecked == true)//DELETE instruction
                {
                    Criteria_Control.EnableCriteriaControl();
                    DisableLDELControls();
                    if (Criteria_Control.Combo_Conditional_Value_1.Text != string.Empty)
                        Command_Entry.IsEnabled = true;
                    else
                        Command_Entry.IsEnabled = false;
                }
                else if (buttonName == "Specify_SampleID" && button.IsChecked == true)//LDEL instruction
                {
                    EnableLDELControls();
                    Criteria_Control.DisableCriteriaControl();
                    if (Grid_SampleIDList.Items.Count == 0)
                    {
                        Command_Entry.IsEnabled = false;
                    }
                    else
                        Command_Entry.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }


        #region DELETE Functionalities
        /// <summary>Handles the textchanged event of Conditional Value textbox in 1st row of Criteria section.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
        private void TextChangedOnCriteriaValue(object sender, TextChangedEventArgs e)
        {
            if (Criteria_Control.Combo_Conditional_Value_1.Text != string.Empty)
                Command_Entry.IsEnabled = true;
            else
                Command_Entry.IsEnabled = false;
        }

        private void TabOnCriteriaControl()
        {
            this.Criteria_Control.Combo_Conditional_Item_1.TabIndex = 2;
            this.Criteria_Control.Combo_Conditional_Operator_1.TabIndex = 3;
            this.Criteria_Control.Combo_Conditional_Value_1.TabIndex = 4;
            this.Criteria_Control.BTnFilter1.TabIndex = 5;
            this.Criteria_Control.Option_Conditional_And_1.TabIndex = 6;
            this.Criteria_Control.Option_Conditional_Or_1.TabIndex = 7;

            this.Criteria_Control.Combo_Conditional_Item_2.TabIndex = 8;
            this.Criteria_Control.Combo_Conditional_Operator_2.TabIndex = 9;
            this.Criteria_Control.Combo_Conditional_Value_2.TabIndex = 10;
            this.Criteria_Control.BTnFilter2.TabIndex = 11;
            this.Criteria_Control.Option_Conditional_And_2.TabIndex = 12;
            this.Criteria_Control.Option_Conditional_Or_2.TabIndex = 13;

            this.Criteria_Control.Combo_Conditional_Item_3.TabIndex = 14;
            this.Criteria_Control.Combo_Conditional_Operator_3.TabIndex = 15;
            this.Criteria_Control.Combo_Conditional_Value_3.TabIndex = 16;
            this.Criteria_Control.BTnFilter3.TabIndex = 17;
            this.Criteria_Control.Option_Conditional_And_3.TabIndex = 18;
            this.Criteria_Control.Option_Conditional_Or_3.TabIndex = 19;

            this.Criteria_Control.Combo_Conditional_Item_4.TabIndex = 20;
            this.Criteria_Control.Combo_Conditional_Operator_4.TabIndex = 21;
            this.Criteria_Control.Combo_Conditional_Value_4.TabIndex = 22;
            this.Criteria_Control.BTnFilter4.TabIndex = 23;
            this.Criteria_Control.Option_Conditional_And_4.TabIndex = 24;
            this.Criteria_Control.Option_Conditional_Or_4.TabIndex = 25;

            this.Criteria_Control.Combo_Conditional_Item_5.TabIndex = 26;
            this.Criteria_Control.Combo_Conditional_Operator_5.TabIndex = 27;
            this.Criteria_Control.Combo_Conditional_Value_5.TabIndex = 28;
            this.Criteria_Control.BTnFilter5.TabIndex = 29;
        }

        /// <summary>
        /// Handles the logic of reading values from the window control and saving DELETE instruction.
        /// </summary>
        private void Save_DELETEProcess()
        {
            List<QuestionSettings> questionSettings = new List<QuestionSettings>();
            questionSettings = Definiotion.VariableDictionary.Values.ToList();
            QuestionSettings qs = new QuestionSettings();
            if (!Criteria_Control.ValidateCriteriaControls(true)) //validate the values given in criteria
            {
                return;
            }
            else
            {
                try
                {
                    isNewQuestion = false;//because it's not a new question
                    isUpdateQuestion = false;//because ther's no need to update QS 
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

                    int colCount = (QC4Common.Common.Constants.DP.InstructionColumn - QC4Common.Common.Constants.DP.OnOffColumn) + 1; //to identify upto which column we have to write values into the sheet in a single row
                    string[,] dpDeleteInstructions = new string[rowCount, QC4Common.Common.Constants.DP.MAX_DP_COLUMN - QC4Common.Common.Constants.DP.OnOffColumn];//SAVE Array for the corresponding row in the sheet
                    for (int i = 0; i < rowCount; i++)
                    {
                        for (int j = 0; j < colCount; j++)
                        {
                            switch (j)
                            {
                                case 0://onoff
                                    dpDeleteInstructions[i, j] = LocalResource.CELL_ON;
                                    break;

                                case 1://checkcross
                                    break;

                                case 2://criteria variable

                                    if (i == 0 && !string.IsNullOrEmpty(Criteria_Control.Combo_Conditional_Item_1.Text))
                                        dpDeleteInstructions[i, j] = Criteria_Control.Combo_Conditional_Item_1.Text;
                                    else if (i == 1 && !string.IsNullOrEmpty(Criteria_Control.Combo_Conditional_Item_2.Text))
                                        dpDeleteInstructions[i, j] = Criteria_Control.Combo_Conditional_Item_2.Text;
                                    else if (i == 2 && !string.IsNullOrEmpty(Criteria_Control.Combo_Conditional_Item_3.Text))
                                        dpDeleteInstructions[i, j] = Criteria_Control.Combo_Conditional_Item_3.Text;
                                    else if (i == 3 && !string.IsNullOrEmpty(Criteria_Control.Combo_Conditional_Item_4.Text))
                                        dpDeleteInstructions[i, j] = Criteria_Control.Combo_Conditional_Item_4.Text;
                                    else if (i == 4 && !string.IsNullOrEmpty(Criteria_Control.Combo_Conditional_Item_5.Text))
                                        dpDeleteInstructions[i, j] = Criteria_Control.Combo_Conditional_Item_5.Text;

                                    break;

                                case 3://criteria operator

                                    if (i == 0 && !string.IsNullOrEmpty(Criteria_Control.Combo_Conditional_Operator_1.Text))
                                        dpDeleteInstructions[i, j] = Criteria_Control.Combo_Conditional_Operator_1.Text;
                                    else if (i == 1 && !string.IsNullOrEmpty(Criteria_Control.Combo_Conditional_Operator_2.Text))
                                        dpDeleteInstructions[i, j] = Criteria_Control.Combo_Conditional_Operator_2.Text;
                                    else if (i == 2 && !string.IsNullOrEmpty(Criteria_Control.Combo_Conditional_Operator_3.Text))
                                        dpDeleteInstructions[i, j] = Criteria_Control.Combo_Conditional_Operator_3.Text;
                                    else if (i == 3 && !string.IsNullOrEmpty(Criteria_Control.Combo_Conditional_Operator_4.Text))
                                        dpDeleteInstructions[i, j] = Criteria_Control.Combo_Conditional_Operator_4.Text;
                                    else if (i == 4 && !string.IsNullOrEmpty(Criteria_Control.Combo_Conditional_Operator_5.Text))
                                        dpDeleteInstructions[i, j] = Criteria_Control.Combo_Conditional_Operator_5.Text;

                                    break;

                                case 4://criteria value

                                    if (i == 0 && !string.IsNullOrEmpty(Criteria_Control.Combo_Conditional_Value_1.Text))
                                    {
                                        qs = questionSettings.FirstOrDefault(q => q.Variable.Equals(Criteria_Control.Combo_Conditional_Value_1.Text, StringComparison.OrdinalIgnoreCase));
                                        if (qs != null)
                                        {
                                            dpDeleteInstructions[i, j] = qs.Variable;
                                        }
                                        else
                                        {
                                            dpDeleteInstructions[i, j] = Criteria_Control.Combo_Conditional_Value_1.Text;
                                        }
                                    }
                                    else if (i == 1 && !string.IsNullOrEmpty(Criteria_Control.Combo_Conditional_Value_2.Text))
                                    {
                                        qs = questionSettings.FirstOrDefault(q => q.Variable.Equals(Criteria_Control.Combo_Conditional_Value_2.Text, StringComparison.OrdinalIgnoreCase));
                                        if (qs != null)
                                        {
                                            dpDeleteInstructions[i, j] = qs.Variable;
                                        }
                                        else
                                        {
                                            dpDeleteInstructions[i, j] = Criteria_Control.Combo_Conditional_Value_2.Text;
                                        }

                                    }
                                    else if (i == 2 && !string.IsNullOrEmpty(Criteria_Control.Combo_Conditional_Value_3.Text))
                                    {
                                        qs = questionSettings.FirstOrDefault(q => q.Variable.Equals(Criteria_Control.Combo_Conditional_Value_3.Text, StringComparison.OrdinalIgnoreCase));
                                        if (qs != null)
                                        {
                                            dpDeleteInstructions[i, j] = qs.Variable;
                                        }
                                        else
                                        {
                                            dpDeleteInstructions[i, j] = Criteria_Control.Combo_Conditional_Value_3.Text;
                                        }
                                    }
                                    else if (i == 3 && !string.IsNullOrEmpty(Criteria_Control.Combo_Conditional_Value_4.Text))
                                    {
                                        qs = questionSettings.FirstOrDefault(q => q.Variable.Equals(Criteria_Control.Combo_Conditional_Value_4.Text, StringComparison.OrdinalIgnoreCase));
                                        if (qs != null)
                                        {
                                            dpDeleteInstructions[i, j] = qs.Variable;
                                        }
                                        else
                                        {
                                            dpDeleteInstructions[i, j] = Criteria_Control.Combo_Conditional_Value_4.Text;
                                        }
                                    }
                                    else if (i == 4 && !string.IsNullOrEmpty(Criteria_Control.Combo_Conditional_Value_5.Text))
                                    {
                                        qs = questionSettings.FirstOrDefault(q => q.Variable.Equals(Criteria_Control.Combo_Conditional_Value_5.Text, StringComparison.OrdinalIgnoreCase));
                                        if (qs != null)
                                        {
                                            dpDeleteInstructions[i, j] = qs.Variable;
                                        }
                                        else
                                        {
                                            dpDeleteInstructions[i, j] = Criteria_Control.Combo_Conditional_Value_5.Text;
                                        }
                                    }

                                    break;

                                case 5://AND || OR|| DELETE

                                    if (i == 0)
                                    {
                                        if (Criteria_Control.Option_Conditional_And_1.IsChecked == false && Criteria_Control.Option_Conditional_Or_1.IsChecked == false)
                                            dpDeleteInstructions[i, j] = ExcelAddIn.Common.Constants.DP.InstructionDELETE;
                                        else if (Criteria_Control.Option_Conditional_And_1.IsChecked == true && Criteria_Control.Option_Conditional_Or_1.IsChecked == false)
                                            dpDeleteInstructions[i, j] = ExcelAddIn.Common.Constants.DP.InstructionAND;
                                        else if (Criteria_Control.Option_Conditional_And_1.IsChecked == false && Criteria_Control.Option_Conditional_Or_1.IsChecked == true)
                                            dpDeleteInstructions[i, j] = ExcelAddIn.Common.Constants.DP.InstructionOR;
                                    }
                                    else if (i == 1)
                                    {
                                        if (Criteria_Control.Option_Conditional_And_2.IsChecked == false && Criteria_Control.Option_Conditional_Or_2.IsChecked == false)
                                            dpDeleteInstructions[i, j] = ExcelAddIn.Common.Constants.DP.InstructionDELETE;
                                        else if (Criteria_Control.Option_Conditional_And_2.IsChecked == true && Criteria_Control.Option_Conditional_Or_2.IsChecked == false)
                                            dpDeleteInstructions[i, j] = ExcelAddIn.Common.Constants.DP.InstructionAND;
                                        else if (Criteria_Control.Option_Conditional_And_2.IsChecked == false && Criteria_Control.Option_Conditional_Or_2.IsChecked == true)
                                            dpDeleteInstructions[i, j] = ExcelAddIn.Common.Constants.DP.InstructionOR;
                                    }
                                    else if (i == 2)
                                    {
                                        if (Criteria_Control.Option_Conditional_And_3.IsChecked == false && Criteria_Control.Option_Conditional_Or_3.IsChecked == false)
                                            dpDeleteInstructions[i, j] = ExcelAddIn.Common.Constants.DP.InstructionDELETE;
                                        else if (Criteria_Control.Option_Conditional_And_3.IsChecked == true && Criteria_Control.Option_Conditional_Or_3.IsChecked == false)
                                            dpDeleteInstructions[i, j] = ExcelAddIn.Common.Constants.DP.InstructionAND;
                                        else if (Criteria_Control.Option_Conditional_And_3.IsChecked == false && Criteria_Control.Option_Conditional_Or_3.IsChecked == true)
                                            dpDeleteInstructions[i, j] = ExcelAddIn.Common.Constants.DP.InstructionOR;
                                    }
                                    else if (i == 3)
                                    {
                                        if (Criteria_Control.Option_Conditional_And_4.IsChecked == false && Criteria_Control.Option_Conditional_Or_4.IsChecked == false)
                                            dpDeleteInstructions[i, j] = ExcelAddIn.Common.Constants.DP.InstructionDELETE;
                                        else if (Criteria_Control.Option_Conditional_And_4.IsChecked == true && Criteria_Control.Option_Conditional_Or_4.IsChecked == false)
                                            dpDeleteInstructions[i, j] = ExcelAddIn.Common.Constants.DP.InstructionAND;
                                        else if (Criteria_Control.Option_Conditional_And_4.IsChecked == false && Criteria_Control.Option_Conditional_Or_4.IsChecked == true)
                                            dpDeleteInstructions[i, j] = ExcelAddIn.Common.Constants.DP.InstructionOR;
                                    }
                                    else if (i == 4)
                                        dpDeleteInstructions[i, j] = ExcelAddIn.Common.Constants.DP.InstructionDELETE;


                                    break;

                                case 6://no substitute variable
                                    break;

                                case 7://no substitute operator
                                    break;

                                default://no substitute parameters
                                    break;

                            }
                        }
                    }
                    if (dbHelper.WriteProcess(workbook, Util.Constants.ProcessingType.DeleteCases, null, string.Empty, string.Empty,
                       0, null, command, dpDeleteInstructions, isNewQuestion, writeRow, processingOption, null, isUpdateQuestion))//need to pass the entire row from here for saving 
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


        /// <summary>Logic for populating Criteria section for DELETE- Edit and Copy processes.</summary>
        /// <param name="objArray">The values stored in the range, used to retrieve command, new variable and all parameters.</param>
        /// <exception cref="Exception"></exception>
        private void PopulateValuesDELETEInstruction(object[,] objArray)
        {
            try
            {
                for (int i = 1; i <= objArray.GetLength(0); i++)
                {
                    if (objArray[i, QC4Common.Common.Constants.DP.CriteriaVariableColumn] != null)
                    {
                        string criteriaVariable = objArray[i, QC4Common.Common.Constants.DP.CriteriaVariableColumn].ToString();
                        Criteria_Control.SetSelectedCriteriaVariable(criteriaVariable, i);
                        Criteria_Control.SetSelectedCriteriaOperator(objArray[i, QC4Common.Common.Constants.DP.CriteriaOperatorColumn].ToString(), i);
                        Criteria_Control.SetSelectedCriteriaValue(objArray[i, QC4Common.Common.Constants.DP.CriteriavalueColumn].ToString(), i);
                    }
                    string instruction = objArray[i, QC4Common.Common.Constants.DP.InstructionColumn].ToString();
                    if (instruction == QC4Common.Common.Constants.DP.InstructionAND || instruction == QC4Common.Common.Constants.DP.InstructionOR)
                    {
                        Criteria_Control.SetSelectedConditionalOption(instruction, i);
                    }
                    else if (instruction == QC4Common.Common.Constants.DP.InstructionDELETE)
                    {
                        Command_Entry.IsEnabled = true;
                    }
                }

            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }
        #endregion

        #region LDEL functionalities
        /// <summary>Handles the logic to enable the right side controls when "Specify SampleID" radio button is clicked.</summary>
        private void EnableLDELControls()
        {
            Label_SampleIDListFile.IsEnabled = true;
            Label_SampleIDListFile.Foreground = Brushes.Black;
            Button_ReadFile.IsEnabled = true;
            Button_ReadFile.Foreground = Brushes.Black;
            Textbox_ShowCases.IsEnabled = true;
            Grid_SampleIDList.IsEnabled = true;
            Grid_SampleIDList.Background = Brushes.White;
            if (selectedIDs.Count > 0)
            {
                for (int i = 0; i < Grid_SampleIDList.Items.Count; i++)
                {
                    DataRowView row = (DataRowView)Grid_SampleIDList.Items[i];
                    string item = row["SampleID"].ToString();
                    if (selectedIDs.Contains(item))
                    {
                        Grid_SampleIDList.SelectedItems.Add(row);
                    }
                }
                selectedIDs.Clear();
            }
            Label_SpecifyID.IsEnabled = true;
            Label_SpecifyID.Foreground = Brushes.Black;
            Textbox_SampleID.IsEnabled = true;
            Textbox_SampleID.Background = Brushes.White;
            if (!string.IsNullOrEmpty(Textbox_SampleID.Text))
            {
                Button_Add.IsEnabled = true;
                Button_Add.Foreground = Brushes.Black;
            }
            if (Grid_SampleIDList.SelectedItems.Count > 0)
            {
                Button_Delete.IsEnabled = true;
                Button_Delete.Foreground = Brushes.Black;
            }
        }

        /// <summary>Handles the logic to disable the right side controls when "Specify Criteria" radio button is clicked.</summary>
        private void DisableLDELControls()
        {
            Label_SampleIDListFile.IsEnabled = false;
            Label_SampleIDListFile.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
            Button_ReadFile.IsEnabled = false;
            Button_ReadFile.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
            Textbox_ShowCases.IsEnabled = false;
            Grid_SampleIDList.IsEnabled = false;
            Grid_SampleIDList.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F0F0F0"));
            selectedIDs = new List<string>();
            if (Grid_SampleIDList.SelectedItem != null)
            {
                foreach (DataRowView row in Grid_SampleIDList.SelectedItems)
                {
                    string item = row["SampleID"].ToString();
                    selectedIDs.Add(item);
                }
                Grid_SampleIDList.UnselectAll();
            }
            Label_SpecifyID.IsEnabled = false;
            Label_SpecifyID.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
            Textbox_SampleID.IsEnabled = false;
            Textbox_SampleID.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F0F0F0"));
            Button_Add.IsEnabled = false;
            Button_Add.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
            Button_Delete.IsEnabled = false;
            Button_Delete.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
        }


        /// <summary>Handles the logic to get all the SampleIDs in the opened qc4 file.</summary>
        private void GetSampleIDList()
        {
            DataTable data = new DataTable();
            sampleIDList = new List<string>();
            sampleIDIndex = new List<int>();
            using (SQLiteConnection con = DBHelper.GetConnection(DBHelper.GetConnectionString(workbook)))
            {
                try
                {
                    con.Open();
                    string sql = "SELECT sample_id FROM answers ";// + tableName;
                    data = DBHelper.GetDataTable(sql, con);
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        DataRow dr = data.Rows[i];
                        sampleIDList.Add(dr.ItemArray[0].ToString());
                        sampleIDIndex.Add(i + 1); //saving the index of the corresponding sampleID for sorting the ids in the grid'
                        if (!sampleIDListDict.ContainsKey(dr.ItemArray[0].ToString()))
                        {
                            sampleIDListDict.Add(dr.ItemArray[0].ToString(), Convert.ToString(i + 1));
                        }
                    }
                }
                catch (Exception ex)
                {
                    _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                }
                finally
                {
                    con.Close();
                }
            }
        }


        /// <summary>Handles the logic of the click event of 'Read File' button control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private async void Button_ReadFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.Wait;

                Dispatcher.Invoke(() =>
                {
                    Textbox_ShowCases.Text = string.Empty;
                });
                Microsoft.Win32.OpenFileDialog openDialog = new Microsoft.Win32.OpenFileDialog();
                openDialog.Filter = "Text files (*.txt)|*.txt"; //only textfiles are allowed
                if (openDialog.ShowDialog() == true)
                    await Task.Run(() => ReadTextFile(openDialog.FileName)); //get the path of selected file here
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }
        }


        /// <summary>Handles the logic of reading values from uploaded text file.</summary>
        /// <param name="filepath">The path of uploaded file.</param>
        /// <exception cref="Exception"></exception>
        private void ReadTextFile(string filepath)
        {
            try
            {
                Encoding enc = Encoding.GetEncoding("utf-8");
                List<string[]> sampleids = new List<string[]>();
                using (TextFieldParser parser = new TextFieldParser(filepath, enc))
                {
                    parser.Delimiters = new[] { "/r/n" };
                    while (!parser.EndOfData)
                    {
                        string[] fields = null;
                        try
                        {
                            fields = parser.ReadFields();
                            sampleids.Add(fields);
                        }
                        catch { }
                    }
                }
                string[] fullFilePath = filepath.Split('\\');
                fileName = fullFilePath[fullFilePath.Length - 1];
                string line = string.Empty;
                idsToLDELSheet = new List<string>();
                List<string> idsFromFile = new List<string>();
                List<int> idsIndices = new List<int>();
                totalCount = 0;
                successCount = 0;
                IDictionary<string, string> idsToLDELSheetDict = new Dictionary<string, string>();

                dtSampleIDs = new DataTable();
                Dispatcher.Invoke(() =>
                {
                    Grid_SampleIDList.ItemsSource = dtSampleIDs.DefaultView;
                    Grid_SampleIDList.DataContext = dtSampleIDs;
                });
                foreach (string[] linedat in sampleids)
                {
                    line = Convert.ToString(linedat[0]);
                    if (idsToLDELSheet.Count > QC4Common.Common.Constants.STD_DP.DeleteCaseMAXRowCount)
                    {
                        idsToLDELSheet.Clear();
                        idsFromFile.Clear();
                        idsIndices.Clear();
                        MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_DELETE_THE_NUMBER_OF_CASES_CAN_BE_READ_AT_A_TIME_EXCEEDS, QC4Common.Common.Constants.STD_DP.DeleteCaseMAXRowCount));
                        return;
                    }

                    if (!idsToLDELSheetDict.ContainsKey(line))
                    {
                        totalCount++;
                        idsToLDELSheet.Add(line);
                        idsToLDELSheetDict.Add(line, line);
                        if (sampleIDListDict.ContainsKey(line))
                        {
                            successCount++;
                            idsFromFile.Add(line);
                            int indx = sampleIDList.FindIndex(id => id == line);
                            idsIndices.Add(indx);
                        }
                    }

                }
                Dispatcher.Invoke(() =>
                {
                    Textbox_ShowCases.Text = string.Format(LocalResource.LDEL_SHOW_SAMPLEID_COUNT, successCount, totalCount); //set the valid SampleID count in the textbox
                });

                if (successCount > 0)
                {
                    Dispatcher.Invoke(() =>
                    {
                        dtSampleIDs = FillDataGrid(idsIndices.ToArray(), idsFromFile.ToArray());

                        DataTable test = dtSampleIDs.Clone();
                        test.Columns["Index"].DataType = Type.GetType("System.Int32");
                        foreach (DataRow dr1 in dtSampleIDs.Rows)
                        {
                            test.ImportRow(dr1);
                        }
                        test.AcceptChanges();
                        DataView dv = test.DefaultView;
                        dv.Sort = "Index ASC";
                        dtSampleIDs = dv.ToTable();
                        Grid_SampleIDList.ItemsSource = dtSampleIDs.DefaultView;
                        Grid_SampleIDList.DataContext = dtSampleIDs; //load the valid SampleIDs in the grid after sorting 
                                                                     //Command_Entry.IsEnabled = true;
                    });
                }
                else if (successCount == 0)
                {
                    Dispatcher.Invoke(() =>
                    {
                        dtSampleIDs = CreateTable();
                    });
                }
                MessageDialog.Info(LocalResource.IM_PROGRESSBAR_READING_COMPLETED);

            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }


        /// <summary>Handles the logic of the click event of 'Add' button control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        /// <exception cref="Exception"></exception>
        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            string newSampleID = Textbox_SampleID.Text;
            DataRow dr;
            bool exists = false;
            if (dtSampleIDs == null)
            {
                exists = false;
                dtSampleIDs = CreateTable();
            }
            else
            {
                exists = dtSampleIDs.AsEnumerable().Any(row => newSampleID == row.Field<String>("SampleID")); //if given id is already there in the grid
            }
            if (exists)
            {
                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_LDEL_SAMPLEID_ALREADY_SET, newSampleID));
            }
            else if (sampleIDList.Contains(newSampleID)) //if given id exists in the SampleID list
            {
                try
                {
                    int index = sampleIDList.FindIndex(a => a == newSampleID);
                    dr = dtSampleIDs.NewRow();
                    dr["Index"] = index;
                    dr["SampleID"] = newSampleID;
                    dtSampleIDs.Rows.Add(dr);
                    dtSampleIDs.AcceptChanges(); //add new id to the datatable

                    DataTable test = dtSampleIDs.Clone();
                    test.Columns["Index"].DataType = Type.GetType("System.Int32");
                    foreach (DataRow dr1 in dtSampleIDs.Rows)
                    {
                        test.ImportRow(dr1);
                    }
                    test.AcceptChanges();
                    DataView dv = test.DefaultView;
                    dv.Sort = "Index ASC";
                    dtSampleIDs = dv.ToTable(); //values after sorting into the datatable


                    Grid_SampleIDList.ItemsSource = dtSampleIDs.DefaultView;
                    Grid_SampleIDList.DataContext = dtSampleIDs;
                    Textbox_ShowCases.Text = string.Format(LocalResource.LDEL_SHOW_SAMPLEID_COUNT, ++successCount, ++totalCount);//update the sampleID count
                    idsToLDELSheet.Add(newSampleID);//add new id to LDEL list
                    MessageDialog.Info(LocalResource.LDEL_MSG_ADDED);
                    //default settings to Add section
                    Textbox_SampleID.Text = string.Empty;
                    Button_Add.IsEnabled = false;
                    Button_Add.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                }
                catch (Exception ex)
                {
                    _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                }
            }
            else
            {
                MessageDialog.ErrorOk(LocalResource.ERR_MSG_LDEL_SAMPLEID_DOESNOT_EXIST);

            }
        }


        /// <summary>Handles the logic of the text changed event of textbox control in LDEL section.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
        private void Textbox_SampleID_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(Textbox_SampleID.Text.TrimStart().TrimEnd()))
            {
                Button_Add.IsEnabled = false;
                Button_Add.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
            }
            else
            {
                Button_Add.IsEnabled = true;
                Button_Add.Foreground = Brushes.Black;
            }
        }



        /// <summary>Handles the logic of the click event of 'Delete' button control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        /// <exception cref="Exception"></exception>
        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (Grid_SampleIDList.SelectedItems.Count > 0)
            {
                List<string> itemsToDelete = new List<string>();
                List<int> indicesToFocus = new List<int>();
                int deleteCount = 0;
                System.Windows.Forms.DialogResult result;
                result = MessageDialog.InfoYesNo(LocalResource.ALERT_DELETE_DATA_PROCESSING);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    try
                    {
                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                        foreach (DataRowView drv in Grid_SampleIDList.SelectedItems)
                        {
                            string item = drv["SampleID"].ToString();
                            itemsToDelete.Add(item);
                            if (idsToLDELSheet.Contains(item))
                                idsToLDELSheet.Remove(item);
                        }

                        //keep the indices of the items to be deleted
                        for (int i = 0; i < Grid_SampleIDList.Items.Count; i++)
                        {
                            DataRowView row = (DataRowView)Grid_SampleIDList.Items[i];
                            string item = row["SampleID"].ToString();
                            if (itemsToDelete.Contains(item) && indicesToFocus.Contains(i - 1))
                                indicesToFocus.Add(i);
                            if (itemsToDelete.Contains(item) && !indicesToFocus.Contains(i - 1))
                            {
                                indicesToFocus.Add(i - deleteCount);
                                deleteCount++;
                            }
                        }

                        //delete selected items from the grid 
                        for (int j = 0; j < dtSampleIDs.Rows.Count; j++)
                        {
                            DataRow datarow = dtSampleIDs.Rows[j];
                            if (itemsToDelete.Contains(datarow["SampleID"].ToString()))
                            {
                                dtSampleIDs.Rows[j].Delete();
                                dtSampleIDs.AcceptChanges();
                                successCount--;
                                totalCount--;
                                j--;
                            }
                        }

                        if (dtSampleIDs.Rows.Count == 0)
                            Grid_SampleIDList.DataContext = null;
                        Grid_SampleIDList.DataContext = dtSampleIDs;
                        Textbox_ShowCases.Text = string.Format(LocalResource.LDEL_SHOW_SAMPLEID_COUNT, successCount, totalCount); //update the sampleID count

                        //keep items selected in the corresponding indices
                        for (int i = 0; i < Grid_SampleIDList.Items.Count; i++)
                        {
                            if (indicesToFocus.Contains(i))
                            {
                                DataRowView row = (DataRowView)Grid_SampleIDList.Items[i];
                                Grid_SampleIDList.SelectedItems.Add(row);
                            }
                        }
                        if (Grid_SampleIDList.SelectedItems.Count == 0)
                        {
                            int c = Grid_SampleIDList.Items.Count;
                            if (c > 0)
                            {
                                DataRowView row = (DataRowView)Grid_SampleIDList.Items[c - 1];
                                Grid_SampleIDList.SelectedItems.Add(row);
                            }
                        }
                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    }
                    catch (Exception ex)
                    {
                        _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                    }
                }
            }
        }

        /// <summary>
        /// Handles the logic of reading values from the window control and saving LDEL instruction.
        /// </summary>
        private void Save_LDELProcess()
        {
            try
            {
                //get parameter value to be entered into LDEL sheet
                Excel.Worksheet ldelSheet = Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Qc4Launcher.Util.Constants.SheetCodeName.LDEL);
                int newParam = 1;
                Excel.Range lastParam = null;
                if (processingOption == QC4Common.Common.Constants.STD_DP.Process_Edit && !isEditDeleteProcess)
                {
                    newParam = editParam;
                }
                else
                {
                    Excel.Range paramRow = ldelSheet.Cells[2, 2];
                    lastParam = dbHelper.GetLastCellInRow(paramRow);
                    if (lastParam.Column > 1)
                    {
                        newParam = 0;
                        string xcellvalue = string.Empty;
                        Excel.Range ldelrange = ldelSheet.Range[paramRow, lastParam];
                        if (ldelrange.Cells.Count > 1)
                        {
                            var ldelcell = ldelrange.Value;
                            foreach (object rangevalue in ldelcell)
                            {
                                string headervalue = Convert.ToString(rangevalue);
                                int val = 0;
                                int.TryParse(headervalue, out val);
                                if (val > newParam)
                                {
                                    newParam = val;
                                }
                            }
                        }
                        else
                        {
                            xcellvalue = ldelrange.Value;
                            int.TryParse(xcellvalue, out newParam);
                        }
                        newParam = newParam + 1;
                    }
                }

                //save the values to an array, get corresponding cell range, save into LDEL sheet
                string[,] toLDELSheet = new string[QC4Common.Common.Constants.ExcelRowColumnMax.ExcelMaxRow, 1];
                toLDELSheet[0, 0] = fileName;
                toLDELSheet[1, 0] = newParam.ToString();
                for (int i = 0; i < idsToLDELSheet.Count; i++)
                {
                    toLDELSheet[i + 2, 0] = idsToLDELSheet[i];
                }
                if (processingOption == QC4Common.Common.Constants.STD_DP.Process_Edit && !isEditDeleteProcess)
                {
                    Excel.Range startrow = ldelSheet.Cells[1, ldelParamCol];
                    Excel.Range endRow = ldelSheet.Cells[toLDELSheet.GetLength(0), ldelParamCol];
                    Excel.Range range_LDEL = ldelSheet.Range[startrow, endRow];
                    range_LDEL.Value = toLDELSheet;
                }
                else
                {
                    Excel.Range startrow = ldelSheet.Cells[1, lastParam.Column + 1];
                    Excel.Range endRow = ldelSheet.Cells[toLDELSheet.GetLength(0), lastParam.Column + 1];
                    Excel.Range range_LDEL = ldelSheet.Range[startrow, endRow];
                    range_LDEL.Value = toLDELSheet;
                }


                //save LDEL instruction in DataProcess hidden sheet
                isNewQuestion = false;
                isUpdateQuestion = false;
                string[,] dpLdelInstruction = new string[1, QC4Common.Common.Constants.DP.MAX_DP_COLUMN - QC4Common.Common.Constants.DP.OnOffColumn];
                int columncount = (QC4Common.Common.Constants.DP.SubstituteParam1Column - QC4Common.Common.Constants.DP.OnOffColumn) + 1;
                for (int i = 0; i < columncount; i++)
                {
                    switch (i)
                    {
                        case 0://onoff
                            dpLdelInstruction[0, i] = LocalResource.CELL_ON;
                            break;

                        case 1://checkcross
                            break;

                        case 2://criteria
                            break;

                        case 3://criteria
                            break;

                        case 4://criteria
                            break;

                        case 5://ldel
                            dpLdelInstruction[0, i] = ExcelAddIn.Common.Constants.DP.InstructionLDEL;
                            break;

                        case 6://substitute var
                            break;

                        case 7://substitute operator
                            break;
                        default:
                            dpLdelInstruction[0, i] = newParam.ToString();
                            break;

                    }
                }

                if (dbHelper.WriteProcess(workbook, Util.Constants.ProcessingType.DeleteCases, null, null, null,
                    0, null, ExcelAddIn.Common.Constants.DP.InstructionLDEL, dpLdelInstruction, isNewQuestion, writeRow, processingOption, null, isUpdateQuestion))//need to pass the entire row from here for saving 
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

        /// <summary>Logic for populating Criteria section for DELETE- Edit and Copy processes.</summary>
        /// <param name="objArray">The values stored in the range, used to retrieve command, new variable and all parameters.</param>
        /// <exception cref="Exception"></exception>
        private void PopulateValuesLDELInstruction(int param)
        {
            try
            {
                //get values from LDEL sheet corresponding to the parameter
                ldelParamCol = 2;
                Excel.Worksheet ldelSheet = Util.ExcelUtil.GetWorkSheetByCodeName(workbook, Qc4Launcher.Util.Constants.SheetCodeName.LDEL);
                Excel.Range firstParam = ldelSheet.Cells[2, 2];
                Excel.Range lastParam = dbHelper.GetLastCellInRow(firstParam);
                Excel.Range range = ldelSheet.Range[firstParam, lastParam];
                foreach (Excel.Range cell in range.Cells)
                {
                    int val = 0;
                    if (int.TryParse(cell.Text, out val) && int.Parse(cell.Text) == param && cell.Column > 1)
                        ldelParamCol = cell.Column;
                }

                //add values from LDEL sheet to the list
                idsToLDELSheet = new List<string>();
                Excel.Range firstVal = ldelSheet.Cells[3, ldelParamCol];
                Excel.Range lastVal = ExcelUtil.EndxlUp(firstVal);
                Excel.Range range_sampleid = ldelSheet.Range[firstVal, lastVal];

                if (range_sampleid.Cells.Count == 1)
                {
                    idsToLDELSheet.Add(range_sampleid.Value);
                }
                else if (range_sampleid.Cells.Count > 1)
                {
                    var objAry = range_sampleid.Value;
                    int max = objAry.GetLength(0);
                    for (int i = 1; i <= max; i++)
                    {
                        if (objAry[i, 1] != null)
                        {
                            idsToLDELSheet.Add(objAry[i, 1].ToString());
                        }
                    }
                }

                //populate values to the form
                totalCount = idsToLDELSheet.Count;
                List<string> idsFromFile = new List<string>();
                List<int> idsIndices = new List<int>();
                successCount = 0;
                dtSampleIDs = new DataTable();
                Grid_SampleIDList.ItemsSource = dtSampleIDs.DefaultView;
                Grid_SampleIDList.DataContext = dtSampleIDs;
                foreach (string id in idsToLDELSheet)
                {
                    if (sampleIDList.Contains(id)) //comparing the value from sheet with SampleID list
                    {
                        successCount++;
                        idsFromFile.Add(id);
                        int indx = sampleIDList.FindIndex(i => i == id);
                        idsIndices.Add(indx);
                    }
                }
                Textbox_ShowCases.Text = string.Format(LocalResource.LDEL_SHOW_SAMPLEID_COUNT, successCount, totalCount); //set the valid SampleID count in the textbox
                if (successCount > 0)
                {
                    dtSampleIDs = FillDataGrid(idsIndices.ToArray(), idsFromFile.ToArray());
                    DataTable test = dtSampleIDs.Clone();
                    test.Columns["Index"].DataType = Type.GetType("System.Int32");
                    foreach (DataRow dr1 in dtSampleIDs.Rows)
                    {
                        test.ImportRow(dr1);
                    }
                    test.AcceptChanges();
                    DataView dv = test.DefaultView;
                    dv.Sort = "Index ASC";
                    dtSampleIDs = dv.ToTable();
                    Grid_SampleIDList.ItemsSource = dtSampleIDs.DefaultView;
                    Grid_SampleIDList.DataContext = dtSampleIDs; //load the valid SampleIDs in the grid after sorting
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

        }

        #region DataGrid Related Methods and Event handlers
        System.Windows.Controls.DataGrid ExpGrid = null;
        /// <summary>
        /// Handles the logic to create datatable for binding into SampleID grid
        /// </summary>
        private DataTable CreateTable()
        {
            DataTable griddata = new DataTable();
            griddata.Columns.Add("Index");
            griddata.Columns.Add("SampleID");
            return griddata;
        }

        /// <summary>
        /// Handles the logic to fill data into the datatable
        /// </summary>
        /// <param name="inddex"> Integer array to fill data in 1st column, Index</param>
        /// <param name="ids"> String array to fill data in 2nd coloumn, SampleID</param>
        private DataTable FillDataGrid(int[] inddex, string[] ids)
        {
            DataTable griddata = CreateTable();
            DataRow dr;
            for (int i = 1; i <= inddex.Count(); i++)
            {
                dr = griddata.NewRow();
                dr["Index"] = inddex[i - 1];
                dr["SampleID"] = ids[i - 1];
                griddata.Rows.Add(dr);
            }
            return griddata;
        }

        /// <summary>Handles the logic while loading a row of data into the grid</summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">The <see cref="DataGridRowEventArgs"/> instance containing the event data.</param>
        private void Grid_SampleIDList_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
            var x = Grid_SampleIDList.CurrentCell;
        }

        /// <summary>Handles the logic while items in the grid has changed</summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private void Grid_SampleIDList_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Grid_SampleIDList.Items.Count == 0)
            {
                Command_Entry.IsEnabled = false;
            }
            else
                Command_Entry.IsEnabled = true;
        }

        /// <summary>Handles the logic while changing the item selection in the grid</summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void Grid_SampleIDList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Grid_SampleIDList.SelectedItems.Count > 0)
            {
                Button_Delete.IsEnabled = true;
                Button_Delete.Foreground = Brushes.Black;
            }
            else
            {
                Button_Delete.IsEnabled = false;
                Button_Delete.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
            }

        }

        /// <summary>Handles the logic while changing the current cell selection in the grid</summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Grid_CurrentCellChanged(object sender, EventArgs e)
        {
            var dg = (System.Windows.Controls.DataGrid)sender;
            ExpGrid = dg;
            dg.Focus();
        }

        /// <summary>Handles the logic of key down event in the Grid control (tab, up and down arrow etc)</summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void Grid_SampleIDList_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                Textbox_SampleID.Focus();
            }
            else
                QC4Common.Util.GridCommonFunc.Arrow(sender, e);
        }

        private void b1SetColor(object sender, MouseButtonEventArgs e)
        {
            if (ExpGrid != null)
                ExpGrid.Focus();
        }

        #endregion

        #endregion

        private void Button_ReadFile_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                Grid_SampleIDList.Focus();
                Style style = Application.Current.FindResource("MyFocusVisual") as Style;
                Grid_SampleIDList.FocusVisualStyle = style;
                if (Grid_SampleIDList.SelectedItem == null && Grid_SampleIDList.Items.Count > 0)
                    Grid_SampleIDList.SelectedIndex = 0;
            }
        }

        private void Command_Cancel_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                Specify_Criteria.Focus();
            }
        }
    }
}
