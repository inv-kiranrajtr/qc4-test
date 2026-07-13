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
using Qc4Launcher.Forms.Cross_Tabulation;
using System.Text.RegularExpressions;
using Constants = ExcelAddIn.Common.Constants;
using ExcelAddIn;
using log4net;
using System.Reflection;
using System.Threading;
using Qc4Launcher.Logic.DataImport;

namespace Qc4Launcher.Forms.DP_Main
{
    /// <summary>
    /// Interaction logic for Compute.xaml
    /// </summary>
    public partial class Compute : Window
    {

        Excel.Workbook Worksheet;
        QC4Common.Util.FormUtil frmutil = new QC4Common.Util.FormUtil();
        double min = 0, max = 0, avg = 0;
        public static Excel.Range range;
        MainWindow MainWindow;
        DP_Main dpMain;
        private string newVariableNameTemp = string.Empty;
        private string newVariableQuestionTemp = string.Empty;
        public bool isModifiedProcess = false;
        private bool isNewQuestion = false;
        bool isloaded = false;
        private static List<String> commands;//list for storing the commands
        private static List<String> answertypes;//list for storing answer types for filtering variables
        private static List<CrossQuestionSetting> choiceListSA; //list for storing SA type variables from the sheet
        private static List<CrossQuestionSetting> choiceListN; // list for storing N type variables from the sheet
        private Dictionary<String, QuestionSettings> dictionary = new Dictionary<string, QuestionSettings>(); //to get the variable list
        private static QuestionSettings question = new QuestionSettings(); //to get the details of selected question 
        List<CrossQuestionSetting> mgList;
        private QC4Common.Util.QSUtil qsUtil = new QC4Common.Util.QSUtil();
        string variablenum = "";
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private void NewItemSearchbutton_Click(object sender, RoutedEventArgs e)
        {
            
            
            if (MagnifyingGlassButton.VariableList != null)
            {
                txt_new_variable.Text = MagnifyingGlassButton.VariableList.Variable;
                newVariableNameTemp = MagnifyingGlassButton.VariableList.Variable;

                if (!string.IsNullOrEmpty(MagnifyingGlassButton.VariableList.Title))
                {
                    Text_NewItem_Question.Text = frmutil.UnEscapeCRLF(MagnifyingGlassButton.VariableList.Title) + " " + frmutil.UnEscapeCRLF(MagnifyingGlassButton.VariableList.Question);
                }
                else
                {
                    Text_NewItem_Question.Text = frmutil.UnEscapeCRLF(MagnifyingGlassButton.VariableList.Question);
                }
                newVariableQuestionTemp = frmutil.EscapeCRLF( Text_NewItem_Question.Text);
                isNewQuestion = false;
            }
        }
       
        private void MagnifyingGlassButton_Loaded(object sender, RoutedEventArgs e)
        {

        }
       
        public String GetVariableName(String variable, List<QC4Common.Model.QuestionSettings> variableList)
        {
            if (!variableList.Any(q => q.Variable.Equals(variable, StringComparison.OrdinalIgnoreCase)))
            {
                return variable;
            }
            Regex regex = new Regex(@"^NUM(\d+)(.*)");
            Match match = regex.Match(variable);
            if (match.Success)
            {
                variable = GenerateVariableName1(match.Groups[2].Value, variableList, Convert.ToInt32(match.Groups[1].Value) + 1);
            }
            else
            {
                 regex = new Regex(@"^N(\d+)(.*)");
                 match = regex.Match(variable);
                if (match.Success)
                {
                    variable = GenerateVariableName(match.Groups[2].Value, variableList, Convert.ToInt32(match.Groups[1].Value) + 1);
                }
                else
                {
                    regex = new Regex(@"^NUM(.*)");
                    match = regex.Match(variable);
                    if (match.Success)
                    {
                        variable = GenerateVariableName1(match.Groups[2].Value, variableList, Convert.ToInt32(match.Groups[1].Value) + 1);
                    }
                    else
                    {
                        regex = new Regex(@"^N(.*)");
                        match = regex.Match(variable);
                        if (match.Success)
                        {
                            variable = GenerateVariableName(match.Groups[1].Value, variableList, 1);
                        }
                        else
                        {
                            variable = GenerateVariableName(variable, variableList);
                        }
                    }
                }
               
            }
            return variable;
        }
       static Boolean isoke = false;
       static string newsa = "";
        private static string GenerateVariableName1(string varible, List<QC4Common.Model.QuestionSettings> variableList, int times = 0)
        {
            String str = "NUM" + (times == 0 ? "" : times.ToString()) + varible;
            if (variableList.Any(q => q.Variable.Equals(str)))
            {
                str = GenerateVariableName1(varible, variableList, times + 1);
            }
            return str;
        }private static string GenerateVariableName(string varible, List<QC4Common.Model.QuestionSettings> variableList, int times = 0)
        {
            String str = "N" + (times == 0 ? "" : times.ToString()) + varible;
            if (variableList.Any(q => q.Variable.Equals(str, StringComparison.OrdinalIgnoreCase)))
            {
                str = GenerateVariableName(varible, variableList, times + 1);
            }
            return str;
        }
        private void Combo_OriginItem_ListFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List_OriginItem_ChoiceList1.DataContext = null;
            string selectedFilterType = Combo_OriginItem_ListFilter.SelectedItem.ToString();
            try
            {
                switch (selectedFilterType)
                {
                    case "N":
                        List_OriginItem_ChoiceList1.DataContext = choiceListN;
                       
                        break;

                    case "SA":
                        List_OriginItem_ChoiceList1.DataContext = choiceListSA;
                       


                        break;

                    default:
                        
                        break;
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void List_OriginItem_ChoiceList1_MouseEnter(object sender, MouseEventArgs e)
        {
            
        }

        private void List_OriginItem_ChoiceList1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            

        }

        private void List_OriginItem_ChoiceList1_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            ListBoxPreviewMouseUp();
        }

        private void List_OriginItem_ChoiceList1_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ListBoxPreviewMouseDown(e);
        }



        List<string> OperatorList = new List<string>();
        public bool NormalExpression()
        {
            if (!string.IsNullOrEmpty(FormulaString))
            {
                char firstString = FormulaString[0];
                char lastString = FormulaString.Last();

                bool hasOperator = false;
                bool hasOperator1 = false;
                string previousOperator = string.Empty;
                foreach (var opr in OperatorList)
                {
                    hasOperator = FormulaString.Contains(opr);
                    if (hasOperator)
                    {
                        hasOperator1 = true;
                       
                    }
                }

               
                if (hasOperator1 == false)
                {
                    var pattern = @"\[(.*?)\]";
                    var matches = Regex.Matches(FormulaString, pattern);
                    if (matches.Count == 1)
                    {
                        return true;
                    }
                    else
                    {
                        FormulaString = FormulaString.Trim();
                        if (lastString == Convert.ToChar(")") && firstString == Convert.ToChar("("))
                        {
                            return true;
                        }
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        System.Windows.Controls.DataGrid ExpGrid = null;
        private void b1SetColor(object sender, MouseButtonEventArgs e)
        {
            if (ExpGrid != null)
                ExpGrid.Focus();
        }
        private void Grid_OriginItem_ChoiceList1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                Plus_Indicator.Focus();
                e.Handled = true;
               
            }
            else
                QC4Common.Util.GridCommonFunc.Arrow(sender, e);
        }

        private void Grid_CurrentCellChanged(object sender, EventArgs e)
        {
            var dg = (System.Windows.Controls.DataGrid)sender;
            ExpGrid = dg;
            dg.Focus();
        }


        private void List_OriginItem_ChoiceList1_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
          
        }

        private void ButtonSingleRightArrow_Click(object sender, RoutedEventArgs e)
        {
            if (List_OriginItem_ChoiceList1.SelectedItems.Count > 0)
            {
                foreach (CrossQuestionSetting item in List_OriginItem_ChoiceList1.SelectedItems)
                {
                    string variable = string.Empty;
                    variable = "[" + item.Variable + "]";

                    bool haveSelection = Text_Formula.SelectionLength > 0;
                    selectionIndex = Text_Formula.SelectionStart;

                    string text = (haveSelection) ? Text_Formula.Text.Remove(selectionIndex, Text_Formula.SelectionLength) : Text_Formula.Text;
                    Text_Formula.Text = text.Insert(selectionIndex, variable);
                    selectionIndex = selectionIndex + variable.Count();
                    Text_Formula.Select(selectionIndex, 0);
                    //if (haveSelection)
                    //{
                    //    Text_Formula.SelectionStart = selectionIndex;
                    //    Text_Formula.SelectionLength = variable.Length;
                    //}

                }
                FormulaString = Text_Formula.Text;
            }
        }
        List<CrossQuestionSetting> allVariables = new List<CrossQuestionSetting>();
        public List<string> formulaVariables = new List<string>();
        public List<string> FormulaOperators = new List<string>();
        public void CheckFormula()
        {
            allVariables.AddRange(choiceListSA);
            allVariables.AddRange(choiceListN);
            int OpenBracket = 0;
            int CloseBracket = 0;
            FormulaOperators.Clear();
            formulaVariables.Clear();
            if (!string.IsNullOrEmpty(FormulaString))
            {
                char lastString = FormulaString.Last();
               
                bool bracketContain = FormulaString.Contains("{");
                if (bracketContain)
                {
                    isvalidformula = false;
                }
                string upcase = FormulaString;

                Regex regex = new Regex(@"^(.*)(SIN)(\d+)(.*)");
                Match match = regex.Match(upcase.ToUpper());
                if (match.Success)
                {
                    isvalidformula = false;
                }


                regex = new Regex(@"^(.*)(COS)(\d+)(.*)");
                match = regex.Match(upcase.ToUpper());
                if (match.Success)
                {
                    isvalidformula = false;
                }
                regex = new Regex(@"^(.*)(TAN)(\d+)(.*)");
                match = regex.Match(upcase.ToUpper());
                if (match.Success)
                {
                    isvalidformula = false;
                }
            }
              
           
        }
        private bool isUpdateQuestion = false;
        private QC4Common.Util.FormUtil formUtil = new QC4Common.Util.FormUtil();
        private void Command_Entry_Click(object sender, RoutedEventArgs e)
        {
            string command = QC4Common.Common.Constants.DP.SubstituteOperatorCOMPUTE;
            //check whether user has edited the variable name or question text
            if (processingOption != QC4Common.Common.Constants.STD_DP.Process_Edit && (txt_new_variable.Text != newVariableNameTemp || Text_NewItem_Question.Text != newVariableQuestionTemp))
            {
                isNewQuestion = true;
            }
            if (!string.IsNullOrWhiteSpace(txt_new_variable.Text) && dictionary.ContainsKey(txt_new_variable.Text))
            {
                QuestionSettings qs = dictionary[txt_new_variable.Text];
                if (qs.AnswerType != Text_New_Item.Text || qs.QuestionFlag != QC4Common.Common.Constants.QuestionFlag.New)
                {
                    isNewQuestion = true;
                }
                else if (qs.AnswerType == Text_New_Item.Text && qs.QuestionFlag == QC4Common.Common.Constants.QuestionFlag.New)
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
                SaveTosheet(Text_Formula.Text);
                ValidateCOMPUTEParam();
                CheckFormula();
                if (!saorma) { return; }
                if (isvalidformula)
                {
                    if ((Util.Definiotion.VariableDictionary.ContainsKey(newVariable)) && (
                  (Util.Definiotion.VariableDictionary[newVariable].AnswerType != Text_New_Item.Text) ))
                    {
                        System.Windows.Forms.DialogResult result;
                        result = MessageDialog.InfoYesNo(LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_ITEM_NAME_ALREADY_EXISTS + "\n" + LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_RENAME_NEW_VARIABLE_AUTOMATICALLY);
                        if (result == System.Windows.Forms.DialogResult.Yes)
                        {
                            //get new variable name
                            QC4Common.Util.QSUtil qsutil = new QC4Common.Util.QSUtil();
                            newVariable = txt_new_variable.Text = GetVariableName(newVariable, dictionary.Values.ToList());
                        }
                        else if (result == System.Windows.Forms.DialogResult.No)
                        {
                            return;
                        }

                        isNewQuestion = true;
                        isUpdateQuestion = false;
                    }
                    else if ((Util.Definiotion.VariableDictionary.ContainsKey(newVariable)) &&
                        (Util.Definiotion.VariableDictionary[newVariable].AnswerType == Text_New_Item.Text)
                        )
                    {
                        isUpdateQuestion = true;
                        isNewQuestion = false;
                      
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
                            if (!string.IsNullOrWhiteSpace(txt_new_variable.Text))
                            {
                                
                               string variableRename = GetVariableName(txt_new_variable.Text, dictionary.Values.ToList());
                                txt_new_variable.Text = variableRename;
                                isNewQuestion = true;
                                isUpdateQuestion = false;
                            }
                            else
                            {
                                txt_new_variable.Text = "NUM1";
                                txt_new_variable.Text = GetVariableName(txt_new_variable.Text, dictionary.Values.ToList());
                               
                                isNewQuestion = true;
                                isUpdateQuestion = false;
                            }
                        }
                        else if (result == System.Windows.Forms.DialogResult.No)
                        {
                            return;
                        }
                    }
                    
                    if (processingOption != QC4Common.Common.Constants.STD_DP.Process_Edit && !string.IsNullOrEmpty(txt_new_variable.Text))
                    {
                        
                        if (formUtil.IsVariableLengthExceeds(txt_new_variable.Text))
                        {
                            MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_QUESTION_LENGTH_EXCEED, QC4Common.Common.Constants.QsVariableMaxLength)); //ERR_MSG_QUESTION_LENGTH_EXCEED
                            return;
                        }
                        //check the newVariable name is valid or not
                        Util.QS.NewQuestionValidation validation = new Util.QS.NewQuestionValidation(txt_new_variable.Text);
                        if (!validation.Validation_Variable(true))
                        {
                           
                            return;
                        }
                    }

                   
                    int columncount = 9; //to identify upto which column we have to write values into the sheet in a single row
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
                                    dpsaveinstructios[0, i] = Text_Formula.Text;
                                    break;

                            }
                        }

                    if (qstndata.Length > QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit)
                    {
                        qstndata = qstndata.PadRight(QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit).Substring(0, QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit);
                    }

                    //DataProcessHelper dbHelper = new DataProcessHelper();
                    if (dbHelper.WriteProcess(Worksheet, Util.Constants.ProcessingType.CreateNewVariable, txt_new_variable.Text, Text_New_Item.Text, qstndata,
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
                else
                {
                    MessageBox.Show(LocalResource.COMPUTE_MSG_INVALID_FORMULA, "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

           
         
        }
        public string SaveTosheet(string Formula1)
        {
            List<string> qs = new List<string>();
            string cellcontent = Formula1;
            cellcontent = cellcontent.Replace('[', '@');
            cellcontent = cellcontent.Replace(']', '@');
            string[] splitcontent = cellcontent.Split('@');
            string VariableString = "";
            foreach (string item in splitcontent)
            {
                if (dictionary.ContainsKey(item))
                {

                    
                    QuestionSettings qsObject = dictionary[item];
                    if ((qsObject.AnswerType.Equals(Constants.AnswerType.SA) || qsObject.AnswerType.Equals(Constants.AnswerType.N)) ||
                        (qsObject.AnswerType.Equals(Constants.AnswerType.SA) || qsObject.AnswerType.Equals(Constants.AnswerType.N)))
                    {
                        
                        qs.Add(qsObject.Variable);
                    }
                }
            }
            VariableString=string.Join(",", qs);
            return VariableString;
        }
        bool isvalidformula = true;
        bool saorma = true;
        private void ValidateCOMPUTEParam()
        {
            saorma = true;
            isvalidformula = true;
            string formatedcontents = "";
            string cellcontent = Text_Formula.Text;
            cellcontent = cellcontent.Replace('[', '@');
            cellcontent = cellcontent.Replace(']', '@');
            string[] splitcontent = cellcontent.Split('@');
            foreach (string item in splitcontent)
            {
                if (dictionary.ContainsKey(item))
                {
                    
                   // QuestionSettings qssubvariable = dictionary[item];
                    QuestionSettings qsObject = dictionary[item];
                    if ((qsObject.AnswerType.Equals(Constants.AnswerType.SA) || qsObject.AnswerType.Equals(Constants.AnswerType.N))||
                        (qsObject.AnswerType.Equals(Constants.AnswerType.SA) || qsObject.AnswerType.Equals(Constants.AnswerType.N) ))
                    {
                        formatedcontents += "1";
                    }
                    else
                    {
                        isvalidformula = false;
                        MessageBox.Show(LocalResource.COMPUTE_MSG_INVALID_VARIABLE, "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error);
                        saorma = false;
                        return;
                    }
                }
                else
                {
                    formatedcontents += item;
                }
            }
            if (!formatedcontents[0].Equals(Constants.EqEqual))
            {
                formatedcontents = "=" + formatedcontents;
            }

            //Excel.Worksheet Sheet=new Excel.Worksheet();
            var evalres = Worksheet.Application.Evaluate(formatedcontents);
            switch (evalres)
            {
                case Constants.DP.ErrDiv0:
                case Constants.DP.ErrGettingData:
                case Constants.DP.ErrName:
                case Constants.DP.ErrNA:
                case Constants.DP.ErrNull:
                case Constants.DP.ErrNum:
                case Constants.DP.ErrRef:
                case Constants.DP.ErrValue:
                    isvalidformula = false;
                   



                    break;
            }

            
           
        }

        private void ValidateCorrectData(Excel.Range changedCell, string targetVariable)
        {
           
        }

        private void Command_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        List<string> PrecedentOp = new List<string>();
        private string processingType;
        private string processingOption;
        private int readRow;
        private int writeRow;
        public Compute(Excel.Workbook workbook, int read_row, int write_row, string stdProcessingtype, string stdprocessingoption)
        {
            InitializeComponent();
          
            

            OperatorList.Add("+");
            OperatorList.Add("-");
            OperatorList.Add("*");
            OperatorList.Add("/");
            OperatorList.Add("(");
            OperatorList.Add(")");
           
            Command_Entry.IsEnabled = false;

            
            readRow = read_row;
            writeRow = write_row;
            processingType = stdProcessingtype;
            processingOption = stdprocessingoption;
            Worksheet = workbook;
            dictionary = Definiotion.VariableDictionary;
            lb_recode.Text = LocalResource.COMPUTE_METHODS;
            lb_command.Text = Util.Constants.ProcessingMethod.COMPUTE;
            LoadControls();
            
        }
        private void LoadVariablesFromSheet()
        {
            mgList = new List<CrossQuestionSetting>();
            choiceListSA = new List<CrossQuestionSetting>();
            choiceListN = new List<CrossQuestionSetting>();
            int variableIndexN = 0;
            int variableIndexSA = 0;

            var SettingSheet = ExcelUtil.GetWorkSheetByCodeName(Worksheet, "Sheet91");
            range = SettingSheet.get_Range("List_Item_SAN");
            if (range.Cells.Count > 0)
            {
              
                try
                {
                    if (range.Cells.Count == 1)
                    {
                        if (range.Value != null)
                        {
                           if(Convert.ToString(range.Value)!=string.Empty&& dictionary.ContainsKey(Convert.ToString(range.Value)))
                            {
                                question = dictionary[Convert.ToString(range.Value)];
                                if (question.AnswerType == QuestionType.N.ToString())
                                {
                                    CrossQuestionSetting qs = new CrossQuestionSetting();
                                    qs.Variable = QC4Common.Classes.Help.RemoveCRLFCharacters(question.Variable);
                                    qs.Question = formUtil.EscapeCRLF(question.Question);
                                    qs.AnswerType = QC4Common.Classes.Help.RemoveCRLFCharacters(question.AnswerType);
                                    qs.decid = variableIndexN;
                                    choiceListN.Add(qs);

                                    variableIndexN++;
                                }
                                else if (question.AnswerType == QuestionType.SA.ToString())
                                {
                                    CrossQuestionSetting qs = new CrossQuestionSetting();
                                    qs.Variable = QC4Common.Classes.Help.RemoveCRLFCharacters(question.Variable);
                                    qs.Question = formUtil.EscapeCRLF(question.Question);
                                    qs.AnswerType = QC4Common.Classes.Help.RemoveCRLFCharacters(question.AnswerType) + "/" + question.CategoryCount;
                                    qs.decid = variableIndexN;
                                    choiceListSA.Add(qs);
                                    variableIndexSA++;
                                }
                               
                            }
                        }
                    }
                    else
                    {
                        object[,] rangearray = range.Value;
                        for (int i = 1; i <= rangearray.GetLength(0); i++)
                        {
                            if (Convert.ToString(rangearray[i, 1]) != string.Empty && dictionary.ContainsKey(Convert.ToString(rangearray[i, 1])))
                            {

                                question = dictionary[Convert.ToString(rangearray[i, 1])];
                                if (question.AnswerType == QuestionType.N.ToString())
                                {
                                    CrossQuestionSetting qs = new CrossQuestionSetting();
                                    qs.Variable = QC4Common.Classes.Help.RemoveCRLFCharacters(question.Variable);
                                    qs.Question = formUtil.EscapeCRLF(question.Question);
                                    qs.AnswerType = QC4Common.Classes.Help.RemoveCRLFCharacters(question.AnswerType);
                                    qs.decid = variableIndexN;
                                    choiceListN.Add(qs);

                                    variableIndexN++;
                                }
                                else if (question.AnswerType == QuestionType.SA.ToString())
                                {
                                    CrossQuestionSetting qs = new CrossQuestionSetting();
                                    qs.Variable = QC4Common.Classes.Help.RemoveCRLFCharacters(question.Variable);
                                    qs.Question = formUtil.EscapeCRLF(question.Question);
                                    qs.AnswerType = QC4Common.Classes.Help.RemoveCRLFCharacters(question.AnswerType) + "/" + question.CategoryCount;
                                    qs.decid = variableIndexN;
                                    choiceListSA.Add(qs);
                                    variableIndexSA++;
                                }
                                else
                                    continue;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                }

            }
            else
            {
                try
                {
                    if (range.Text != null && range.Text != string.Empty)
                    {
                        if (Convert.ToString(range.Value) != string.Empty && dictionary.ContainsKey(Convert.ToString(range.Value)))
                        {
                            question = dictionary[Convert.ToString(range.Value)];
                            if (question.AnswerType == QuestionType.N.ToString())
                            {
                                CrossQuestionSetting qs = new CrossQuestionSetting();
                                qs.Variable = QC4Common.Classes.Help.RemoveCRLFCharacters(question.Variable);
                                qs.Question = QC4Common.Classes.Help.RemoveCRLFCharacters(question.Question);
                                qs.AnswerType = QC4Common.Classes.Help.RemoveCRLFCharacters(question.AnswerType);
                                qs.decid = variableIndexN;
                                choiceListN.Add(qs);

                                variableIndexN++;
                            }
                            else if (question.AnswerType == QuestionType.SA.ToString())
                            {
                                CrossQuestionSetting qs = new CrossQuestionSetting();
                                qs.Variable = QC4Common.Classes.Help.RemoveCRLFCharacters(question.Variable);
                                qs.Question = QC4Common.Classes.Help.RemoveCRLFCharacters(question.Question);
                                qs.AnswerType = QC4Common.Classes.Help.RemoveCRLFCharacters(question.AnswerType) + "/" + question.CategoryCount;
                                qs.decid = variableIndexN;
                                choiceListSA.Add(qs);
                                variableIndexSA++;
                            }
                        }
                    }

                }
                catch(Exception ex)
                {
                    _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                }
            }
        }
        static int Indx = 0;
        static bool IsFirst = false;
        static bool IsPressed = false;
        static int StartIndx = 0;
        public static void ListBoxMouseEnter(System.Windows.Controls.ListBox listBox, object sender, System.Windows.Input.MouseEventArgs e)
        {
            try
            {
                ListBoxItem lbi = sender as ListBoxItem;
                if (e.LeftButton == MouseButtonState.Released)
                {
                    IsFirst = true;
                    IsPressed = false;
                    Indx = listBox.Items.IndexOf((lbi.DataContext as CrossQuestionSetting));
                    StartIndx = Indx;
                    listBox.SelectionMode = System.Windows.Controls.SelectionMode.Extended;
                }
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    IsPressed = true;
                    if (IsFirst)
                    {
                        if (listBox.SelectedItems.Count > 1)
                        {
                            listBox.SelectedItems.Clear();
                        }
                        else
                        {
                            object current = listBox.SelectedItem;
                            listBox.SelectedItems.Clear();
                            listBox.SelectedItems.Add(current);
                        }
                    }
                    listBox.SelectionMode = System.Windows.Controls.SelectionMode.Multiple;
                    if (listBox.SelectedItems.Count > 0)
                    {
                        if (Indx >= 0)
                            listBox.SelectedItems.Add(listBox.Items[Indx]);
                        Indx = listBox.Items.IndexOf((lbi.DataContext as CrossQuestionSetting));
                        IEnumerable<CrossQuestionSetting> items = listBox.SelectedItems.Cast<CrossQuestionSetting>();
                        int lastSelectedIndx = listBox.Items.IndexOf(items.LastOrDefault());
                        int bigIndx = listBox.Items.IndexOf(items.OrderBy(x => x.decid).Last());
                        if (lastSelectedIndx > Indx && items.Count() > 1 && bigIndx <= Indx + 1 && !IsFirst)
                        {
                            listBox.SelectedItems.Remove(items.LastOrDefault());
                        }
                        else if (lastSelectedIndx < Indx && items.Count() > 1 && bigIndx >= Indx && !IsFirst && StartIndx >= Indx)
                        {
                            listBox.SelectedItems.Remove(items.OrderBy(x => x.decid).FirstOrDefault());
                        }
                        listBox.SelectedItems.Add(lbi);
                        lbi.IsSelected = true;
                        IsFirst = false;
                        lbi.Focus();
                    }
                    else  //if-else added
                    {
                        listBox.SelectedItems.Clear();
                        Indx = listBox.Items.IndexOf((lbi.DataContext as CrossQuestionSetting));
                        listBox.SelectedItems.Add(lbi);
                        lbi.IsSelected = true;
                        IsFirst = false;
                        lbi.Focus();
                    }
                    for (int i = 0; i < listBox.Items.Count; i++)
                        (listBox.Items[i] as CrossQuestionSetting).decid = i;
                    List<CrossQuestionSetting> data = listBox.SelectedItems.Cast<CrossQuestionSetting>().OrderBy(x => x.decid).ToList();
                    int firstIndex = data[0].decid;
                    int lastIndex = data[data.Count - 1].decid;
                    for (int i = firstIndex; i < lastIndex; i++)
                    {
                        if (!data.Contains(listBox.Items[i]))
                            listBox.SelectedItems.Add(listBox.Items[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }


        public static void ListBoxPreviewMouseDown(MouseButtonEventArgs e)
        {
            if (e.ClickCount > 0)
                IsFirst = true;
        }

        public static void ListBoxPreviewMouseUp()
        {
            IsPressed = false;
        }

        public static void ListBoxScrollChanged(System.Windows.Controls.ListBox listBox, object sender, ScrollChangedEventArgs e, int showedLastItemIndex)
        {
            try
            {
                if (IsPressed)
                {
                    if (e.VerticalChange > 0.0)
                    {
                        if (listBox.Items.Count > 0)
                        {
                            IEnumerable<CrossQuestionSetting> items = listBox.SelectedItems.Cast<CrossQuestionSetting>().OrderBy(x => x.decid);

                            listBox.SelectedItems.Add(listBox.Items[showedLastItemIndex + Convert.ToInt32(e.VerticalOffset)]);

                            for (int i = StartIndx; i <= listBox.Items.IndexOf(items.Last()); i++)
                            {
                                if (listBox.Items.IndexOf(items.OrderBy(x => x.decid).First()) <= StartIndx)
                                    break;
                                listBox.SelectedItems.Add(listBox.Items[i]);
                            }
                            if (listBox.SelectedItems.Cast<CrossQuestionSetting>().OrderBy(x => x.decid).First().decid < StartIndx)
                            {
                                listBox.SelectedItems.Remove(listBox.Items[11]);
                            }
                            IsFirst = false;
                        }
                    }
                    else
                    {
                        if (listBox.Items.Count > 0)
                        {
                            listBox.SelectedItems.Add(listBox.Items[Convert.ToInt32(e.VerticalOffset)]);
                            IEnumerable<CrossQuestionSetting> items = listBox.SelectedItems.Cast<CrossQuestionSetting>().OrderBy(x => x.decid);
                            for (int i = listBox.Items.IndexOf(items.Last()); StartIndx < i; i--)
                            {
                                if (listBox.Items.IndexOf(items.First()) >= StartIndx)
                                    break;
                                listBox.SelectedItems.Remove(listBox.Items[i]);
                            }
                            IsFirst = false;
                        }
                    }
                    for (int i = 0; i < listBox.Items.Count; i++)
                        (listBox.Items[i] as CrossQuestionSetting).decid = i;
                    List<CrossQuestionSetting> data = listBox.SelectedItems.Cast<CrossQuestionSetting>().OrderBy(x => x.decid).ToList();
                    int firstIndex = data[0].decid;
                    int lastIndex = data[data.Count - 1].decid;
                    for (int i = firstIndex; i < lastIndex; i++)
                    {
                        if (!data.Contains(listBox.Items[i]))
                            listBox.SelectedItems.Add(listBox.Items[i]);
                    }
                }
            }
            catch(Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

        }
        int selectionIndex = 0;
        string FormulaString = string.Empty;
        private void List_OriginItem_ChoiceList1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DependencyObject obj = (DependencyObject)e.OriginalSource;
            while (obj != null && obj != List_OriginItem_ChoiceList1)
            {
                if (obj.GetType() == typeof(Grid))
                {
                    
                    foreach (CrossQuestionSetting item in List_OriginItem_ChoiceList1.SelectedItems)
                    {
                        string variable = string.Empty;
                        variable = "[" + item.Variable + "]";

                        bool haveSelection = Text_Formula.SelectionLength > 0;
                        selectionIndex = Text_Formula.SelectionStart;

                        string text = (haveSelection) ? Text_Formula.Text.Remove(selectionIndex, Text_Formula.SelectionLength) : Text_Formula.Text;
                        Text_Formula.Text = text.Insert(selectionIndex, variable);
                        selectionIndex = selectionIndex+ variable.Count();
                        Text_Formula.Select(selectionIndex, 0);

                        
                    }
                    FormulaString = Text_Formula.Text;

                    break;
                }
                obj = VisualTreeHelper.GetParent(obj);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Text_Formula.Text = string.Empty;
            FormulaString = string.Empty;
        }
        string Operator = string.Empty;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Operator = "+";
            bool haveSelection = Text_Formula.SelectionLength > 0;
            selectionIndex = Text_Formula.SelectionStart;
            string text = (haveSelection) ? Text_Formula.Text.Remove(selectionIndex, Text_Formula.SelectionLength) : Text_Formula.Text;
            Text_Formula.Text = text.Insert(selectionIndex, Operator);
            selectionIndex = selectionIndex + Operator.Count();
            Text_Formula.Select(selectionIndex, 0);
          
            FormulaString = Text_Formula.Text;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Operator = "-";
            bool haveSelection = Text_Formula.SelectionLength > 0;
            selectionIndex = Text_Formula.SelectionStart;
            string text = (haveSelection) ? Text_Formula.Text.Remove(selectionIndex, Text_Formula.SelectionLength) : Text_Formula.Text;
            Text_Formula.Text = text.Insert(selectionIndex, Operator);
            selectionIndex = selectionIndex + Operator.Count();
            Text_Formula.Select(selectionIndex, 0);
           
            FormulaString = Text_Formula.Text;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Operator = "*";
            bool haveSelection = Text_Formula.SelectionLength > 0;
            selectionIndex = Text_Formula.SelectionStart;
            string text = (haveSelection) ? Text_Formula.Text.Remove(selectionIndex, Text_Formula.SelectionLength) : Text_Formula.Text;
            Text_Formula.Text = text.Insert(selectionIndex, Operator);
            selectionIndex = selectionIndex + Operator.Count();
            Text_Formula.Select(selectionIndex, 0);
          
            FormulaString = Text_Formula.Text;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Operator = "/";
            bool haveSelection = Text_Formula.SelectionLength > 0;
            selectionIndex = Text_Formula.SelectionStart;
            string text = (haveSelection) ? Text_Formula.Text.Remove(selectionIndex, Text_Formula.SelectionLength) : Text_Formula.Text;
            Text_Formula.Text = text.Insert(selectionIndex, Operator);
            selectionIndex = selectionIndex + Operator.Count();
            Text_Formula.Select(selectionIndex, 0);
           
            FormulaString = Text_Formula.Text;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Operator = "(";
           
            
            bool haveSelection = Text_Formula.SelectionLength > 0;
            selectionIndex = Text_Formula.SelectionStart;
            string text = (haveSelection) ? Text_Formula.Text.Remove(selectionIndex, Text_Formula.SelectionLength) : Text_Formula.Text;
            Text_Formula.Text = text.Insert(selectionIndex, Operator);
            selectionIndex = selectionIndex + Operator.Count();
            Text_Formula.Select(selectionIndex, 0);
           
            FormulaString = Text_Formula.Text;
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Operator = ")";
            bool haveSelection = Text_Formula.SelectionLength > 0;
            selectionIndex = Text_Formula.SelectionStart;
            string text = (haveSelection) ? Text_Formula.Text.Remove(selectionIndex, Text_Formula.SelectionLength) : Text_Formula.Text;
            Text_Formula.Text = text.Insert(selectionIndex, Operator);
            selectionIndex = selectionIndex + Operator.Count();
            Text_Formula.Select(selectionIndex, 0);
           
            FormulaString = Text_Formula.Text;
        }

      
        private void Text_Formula_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(Text_Formula.Text))
            {
                selectionIndex = 0;
                Command_Entry.IsEnabled = false;
                FormulaString = Text_Formula.Text;
            }
            else
            {
                FormulaString = Text_Formula.Text;
                Command_Entry.IsEnabled = true;
                Text_Formula.Focus();
                Text_Formula.SelectionLength=0;
            }
        }

       

        private void Text_Formula_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }
        DataProcessNewLoad dataLoad = new DataProcessNewLoad();
        private void Txt_new_variable_SelectionChanged(object sender, RoutedEventArgs e)
        {
            QuestionSettings qs = dataLoad.Txt_Change_New_Item(txt_new_variable.Text);
            if (qs != null)
            {
                Text_NewItem_Question.Text = qs.Question;
            }
        }
        private DataProcessHelper dbHelper = new DataProcessHelper();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
                if (processingOption == QC4Common.Common.Constants.STD_DP.Process_Edit)
            {
                Color newVariableTextboxColor = (Color)ColorConverter.ConvertFromString("#F0F0F0");
                txt_new_variable.Background = new System.Windows.Media.SolidColorBrush(newVariableTextboxColor);
                Color editProcessMethodColor = (Color)ColorConverter.ConvertFromString("#FFADADAD");
                lb_recode.Foreground= new System.Windows.Media.SolidColorBrush(editProcessMethodColor);
                lb_recode.Background = new System.Windows.Media.SolidColorBrush(newVariableTextboxColor);
                NewItemSearchbutton.IsEnabled = false;
            }

            if (processingOption == QC4Common.Common.Constants.STD_DP.Process_Edit || processingOption == QC4Common.Common.Constants.STD_DP.Process_Copy)
            {
                txt_new_variable.IsReadOnly = true;
                txt_new_variable.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
                NewItemSearchbutton.IsEnabled = false;
                NewItemSearchbutton.Opacity = 0.5;
                Excel.Worksheet dataProcessSheet = Util.ExcelUtil.GetWorkSheetByCodeName(Worksheet, Qc4Launcher.Util.Constants.SheetCodeName.DataProcessS);
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
            }
        }
        private void PopulateValuesToModify(Excel.Range dpRange, object[,] objArray, string toModify)
        {
            try
            {
                //New Variable Name in Textbox
                string savedVariableName = objArray[1, QC4Common.Common.Constants.DP.SubstituteVariableColumn].ToString();
                if (processingOption == QC4Common.Common.Constants.STD_DP.Process_Edit)
                {
                    txt_new_variable.Text = string.Empty;
                    if (dictionary.ContainsKey(savedVariableName))
                    {
                        QuestionSettings qs = dictionary[savedVariableName];

                        txt_new_variable.Text = savedVariableName;
                        Text_NewItem_Question.Text = (qs.Question);
                    }
                }
                    

                else if (processingOption == QC4Common.Common.Constants.STD_DP.Process_Copy)
                {
                    txt_new_variable.Text = GetVariableName("NUM1", dictionary.Values.ToList());
                    txt_new_variable.IsReadOnly = false;
                    NewItemSearchbutton.IsEnabled = true;
                    NewItemSearchbutton.Opacity = 1;
                    txt_new_variable.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    if (dictionary.ContainsKey(savedVariableName))
                    {
                        QuestionSettings qs = dictionary[savedVariableName];

                       // txt_new_variable.Text = savedVariableName;
                        Text_NewItem_Question.Text = (qs.Question);
                    }
                    else
                    {

                    }
                }
                //New variable QuestionText
               
                Text_Formula.Text = objArray[1, 11].ToString();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private string ValidationSingleQuote(string text)
        {
            if (text.StartsWith("'"))
            {
                text = "'" + text;
            }
            return text;

        }
        private void LoadControls()
        {
            try
            {
                
                LoadVariablesFromSheet();
              
                answertypes = new List<string>();
                answertypes.Add("N");
                answertypes.Add("SA");
                Combo_OriginItem_ListFilter.ItemsSource = answertypes;
                Combo_OriginItem_ListFilter.SelectedItem = answertypes.FirstOrDefault();
               
                    txt_new_variable.Text = GetVariableName("NUM1", dictionary.Values.ToList());
               
                

            }

            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }
        private void DisableRightMenu(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
        private void Window_Closed(object sender, EventArgs e)
        {
           
        }
    }
}
