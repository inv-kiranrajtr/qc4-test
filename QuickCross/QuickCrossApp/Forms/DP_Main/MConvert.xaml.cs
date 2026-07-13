using ExcelAddIn.Common;
using QC4Common.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
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
using excel = Microsoft.Office.Interop.Excel;
using log4net;
using System.Reflection;
using Qc4Launcher.Logic.DataImport;

namespace Qc4Launcher.Forms.DP_Main
{
    /// <summary>
    /// Interaction logic for MConvert.xaml
    /// </summary>
    public partial class MConvert : Window
    {
        excel.Workbook workbook;
        int readrow;
        int writerow;
        string Processingtype;
        string processingoption;
        string tableheading;
        bool iseditorcopy = false;
        QC4Common.Util.FormUtil frmutil = new QC4Common.Util.FormUtil();
        Logic.DataProcessHelper dphelper = new Logic.DataProcessHelper();
        System.Windows.Controls.DataGrid ExpGrid = null;
        public DataTable dt = null;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static List<string> MASAvariables = new List<string>();
        private Dictionary<String, QuestionSettings> PopulatedDictionary = new Dictionary<string, QuestionSettings>();
        private ObservableCollection<SourceVariableList> sourceVariableList1 = new ObservableCollection<SourceVariableList>();
        private ObservableCollection<SourceVariableList> CollectionChoiceList1 = new ObservableCollection<SourceVariableList>();
        private ObservableCollection<SourceVariableList> CollectionChoiceList2 = new ObservableCollection<SourceVariableList>();
        ObservableCollection<ChoiceList> CollectionChoiceListSelect1 = new ObservableCollection<ChoiceList>();
        ObservableCollection<ChoiceList> CollectionChoiceListSelect2 = new ObservableCollection<ChoiceList>();
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
        public ObservableCollection<SourceVariableList> CollectionChoiceListview1
        {
            get
            {
                return CollectionChoiceList1;
            }
            set
            {
                CollectionChoiceList1 = value;
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
        private SourceVariableList NewvariableNullItem()
        {
            return (new SourceVariableList()
            {
                Variable = string.Empty,
                AnswerType = string.Empty,
                Question = string.Empty,
                Tableheading = string.Empty,
                Choices = null
            });
        }
        public ObservableCollection<ChoiceList> ChoiceListView
        {
            get
            {
                return CollectionChoiceListSelect1;
            }
            set
            {
                CollectionChoiceListSelect1 = value;
            }
        }
        public ObservableCollection<ChoiceList> ChoiceListView2
        {
            get
            {
                return CollectionChoiceListSelect2;
            }
            set
            {
                CollectionChoiceListSelect2 = value;
            }
        }
        public class ChoiceList
        {
            private int slno;
            private string choice;

            public int SLNO
            {
                get
                {
                    return slno;
                }
                set
                {
                    slno = value;
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

        }
        DataTable gridchoice;
        DataTable gridvalidation;

        public bool isModifiedProcess = false;
        string clipboardText = "";
        public MConvert(excel.Workbook wb, int stdreadrow, int stdwriterow, string stdProcessingtype, string stdprocessingoption)
        {
            InitializeComponent();
            workbook = wb;
            readrow = stdreadrow;
            writerow = stdwriterow;
            Processingtype = stdProcessingtype;
            processingoption = stdprocessingoption;
            if (QC4Common.Common.Constants.GlobalMode.Split(',')[0] != "ja-JP")
                Button_Help.Visibility = Visibility.Hidden;
            else
                Button_Help.Visibility = Visibility.Visible;
        }

        private void Combo_OriginItem_Item_DropDownClosed(object sender, EventArgs e)
        {
            if (iseditorcopy == false)
            {
                System.Windows.Controls.ComboBox sourceVariable = ((System.Windows.Controls.ComboBox)sender);
                if (sourceVariable.SelectedItem != null)
                {
                    SourceVariableList list = (SourceVariableList)(sourceVariable.SelectedItem);
                    Text_OriginItem_AnswerType.Text = frmutil.EscapeCRLF(list.AnswerType);
                    Text_OriginItem_SelectCount.Text = (list.Choices.Count).ToString();
                    Text_OriginItem_Question.Text = frmutil.UnEscapeCRLF(list.Question);// QC4Common.Classes.Help.RemoveCRLFCharacters(list.Question);
                    tableheading = frmutil.Addsinglequete(frmutil.UnEscapeCRLF(list.Tableheading));
                    //Text_NewItem_Question.Text = tableheading;// list.Tableheading;

                    string newvarname = list.Variable;
                    // if (!frmutil.IsVariableNameExists(newvarname, PopulatedDictionary.Values.ToList(), 2))
                    {
                        QC4Common.Util.QSUtil qsutil = new QC4Common.Util.QSUtil();
                        newvarname = qsutil.GetVariableName(newvarname, PopulatedDictionary.Values.ToList());
                    }
                    Combo_NewItem_Item.Text = newvarname;
                    NewItemSearchbutton.IsEnabled = true;
                    Combo_NewItem_Item.IsEnabled = true;//  Combo_NewItem_Item.IsEnabled = true;
                    NewItemSearchbutton.Opacity = 1;
                    Text_NewItem_Question.IsEnabled = true;
                    Text_NewItem_Question.IsReadOnly = false;
                    Combo_NewItem_Item.Background = Brushes.White;
                    Style style = Application.Current.FindResource("NormalTextBoxMultiLine") as Style;
                    Combo_NewItem_Item.Style = style;
                    Text_NewItem_Question.Background = Brushes.White;
                    Combo_NewItem_Item.IsReadOnly = false;






                    LoadEntryVariables(frmutil.EscapeCRLF(list.AnswerType), (list.Choices));

                    LoadChoicesOfVariable(list.Choices);


                    //  List_OriginItem_ChoiceListSelect1.DataContext = CollectionChoiceListSelect1;// GridChoiceListSelect1;
                    CollectionChoiceListSelect2 = new ObservableCollection<ChoiceList>();
                    CollectionChoiceList2 = new ObservableCollection<SourceVariableList>();
                    List_OriginItem_ChoiceList2.DataContext = CollectionChoiceList2;
                    List_OriginItem_ChoiceListSelect2.DataContext = CollectionChoiceListSelect2;
                    CreateNewQuestionFromGrid();
                }

            }
            else if (Combo_NewItem_SelectCount.SelectedIndex > 0)
            { iseditorcopy = false; }
        }
        private void LoadEntryVariables(string variabletype, List<string> choices, List<string> selectedchoices = null)
        {
            int choicecount = 0;
            if (choices != null)
            {
                choicecount = choices.Count;
            }
            CollectionChoiceList1 = new ObservableCollection<SourceVariableList>();
            foreach (KeyValuePair<string, QuestionSettings> item in PopulatedDictionary)
            {
                QuestionSettings qs = item.Value;
                if (qs.AnswerType == variabletype && qs.CategoryCount == choicecount)
                {
                    bool issametype = true;
                    for (int i = 0; i < choicecount; i++)
                    {
                        if (!choices[i].Equals(qs.Choices[i]))
                        { issametype = false; }
                    }

                    if (issametype)
                    {

                        if ((processingoption == QC4Common.Common.Constants.STD_DP.Process_Edit || processingoption == QC4Common.Common.Constants.STD_DP.Process_Copy) && (selectedchoices != null && selectedchoices.Contains(qs.Variable)))
                        {
                            CollectionChoiceList2.Add(new SourceVariableList()
                            {
                                QuestionRowNumber = qs.RowNumber,
                                Variable = frmutil.UnEscapeCRLF(qs.Variable),
                                AnswerType = qs.AnswerType,
                                Question = frmutil.UnEscapeCRLF(qs.Question),
                                Tableheading = frmutil.UnEscapeCRLF(qs.TableHeading),
                                Choices = qs.Choices
                            });
                        }
                        else
                        {
                            CollectionChoiceList1.Add(new SourceVariableList()
                            {
                                QuestionRowNumber = qs.RowNumber,
                                Variable = frmutil.UnEscapeCRLF(qs.Variable),
                                AnswerType = qs.AnswerType,
                                Question = frmutil.UnEscapeCRLF(qs.Question),
                                Tableheading = frmutil.UnEscapeCRLF(qs.TableHeading),
                                Choices = qs.Choices
                            });
                        }
                    }
                }
            }
            List_OriginItem_ChoiceList1.DataContext = CollectionChoiceList1;// GridChoiceList1;
        }
        private void LoadChoicesOfVariable(List<string> choices, List<int> selectedchoices = null)
        {
            CollectionChoiceListSelect1 = new ObservableCollection<ChoiceList>();

            for (int i = 0; i < choices.Count; i++)
            {
                if ((processingoption == QC4Common.Common.Constants.STD_DP.Process_Edit || processingoption == QC4Common.Common.Constants.STD_DP.Process_Copy) && (selectedchoices != null && selectedchoices.Contains(i + 1)))
                {
                    CollectionChoiceListSelect2.Add(new ChoiceList()
                    {
                        SLNO = i + 1,
                        Choice = frmutil.EscapeCRLF(choices[i])
                    });
                }
                else
                {
                    CollectionChoiceListSelect1.Add(new ChoiceList()
                    {
                        SLNO = i + 1,
                        Choice = frmutil.EscapeCRLF(choices[i])
                    });
                }
            }
            List_OriginItem_ChoiceListSelect1.DataContext = CollectionChoiceListSelect1;
        }
        private void Combo_NewItem_SelectCount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {




            System.Windows.Controls.ComboBox combo = (System.Windows.Controls.ComboBox)sender;
            int rowcount = 0;

            if (Combo_NewItem_SelectCount.SelectedIndex == 0)
            {
                rowcount = Qc4Launcher.Util.Constants.MaxChoiceCount;
            }
            else
            {
                if (!int.TryParse(combo.SelectedIndex.ToString(), out rowcount))
                {
                    rowcount = Qc4Launcher.Util.Constants.MaxChoiceCount;
                }
                else
                {
                    rowcount = int.Parse(combo.SelectedIndex.ToString());
                    if (rowcount > Qc4Launcher.Util.Constants.MaxChoiceCount)
                    {
                        rowcount = Qc4Launcher.Util.Constants.MaxChoiceCount;
                    }
                }
            }
            ChangeRowCount(rowcount);
        }

        private void Command_Entry_Click(object sender, RoutedEventArgs e)
        {
            bool isnewquestion = true;
            bool isupdatequestion = false;
            int exclude = 0;
            string newvariable = Combo_NewItem_Item.Text;
            newvariable = newvariable.TrimStart().TrimEnd();
            string answertype = Combo_NewItem_AnswerType.Text;
            string question = Text_NewItem_Question.Text;
            question = question.TrimStart().TrimEnd();
            int sourcevariablechoicecount = 0;
            int lasterrorrow = -1;
            if (!string.IsNullOrEmpty(Text_OriginItem_SelectCount.Text))
            {
                int scatcout = 0;
                if (Int32.TryParse(Text_OriginItem_SelectCount.Text, out scatcout))
                {
                    sourcevariablechoicecount = scatcout;
                }
            }
            if (string.IsNullOrEmpty(newvariable) || !frmutil.IsVariableNameExists(newvariable, PopulatedDictionary.Values.ToList(), 1))
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
                    // string variablename = Combo_NewItem_Item.Text;
                    if (string.IsNullOrEmpty(newvariable))
                    {
                        newvariable = Combo_OriginItem_Item.Text;
                    }
                    //get new variable name
                    QC4Common.Util.QSUtil qsutil = new QC4Common.Util.QSUtil();
                    Combo_NewItem_Item.Text = newvariable = qsutil.GetVariableName(newvariable, PopulatedDictionary.Values.ToList());
                }
                else if (result == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }

            if (string.IsNullOrEmpty(question))
            {
                MessageDialog.ErrorOk(LocalResource.ADDQS_ERROR_MSG_QUSTION_TEXT_MISS);
                return;
            }

            if (question.Length > QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit)
            {
                question = question.PadRight(QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit).Substring(0, QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit);
                Text_NewItem_Question.Text = question;
                //ERR_MSG_MAX_CHAR_LIMIT_EXCEEDS
                // MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_MAX_CHAR_LIMIT_EXCEEDS, QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit));
                // Text_NewItem_Question.Focus();
                // return;
            }
            /*List_OriginItem_ChoiceList2.DataContext = Col
            List_OriginItem_ChoiceListSelect2.DataContext*/
            int choicegridcount = -1;
            // if (Combo_NewItem_SelectCount.SelectedIndex == 0)
            {
                choicegridcount = frmutil.GetLastRow(gridchoice, 1);
            }
            if (choicegridcount == -1 && Combo_NewItem_SelectCount.SelectedIndex == 0)
            {
                MessageDialog.ErrorOk(LocalResource.ADD_DP_RECODE_ERROR_MSG_NO_CHOICE_SET);
                return;
            }
            lasterrorrow = -1;
            if ((choicegridcount == -1 && Combo_NewItem_SelectCount.SelectedIndex > 0) || (choicegridcount < Combo_NewItem_SelectCount.SelectedIndex))//            if ((choicegridcount == -1 && Combo_NewItem_SelectCount.SelectedIndex > 0))// 
            {
                //choicecount and not filled fully --with count
                //Match "Number of choices" and the number of "choice" for New variable.
                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_CHOICECOUNT_NOT_MATCH_WITH_CHOICES, LocalResource.LBL_NO_OF_CHOICES, LocalResource.LABEL_CHOICE));
                //int c = choicegridcount == -1 ? 0 : choicegridcount;
                //for (int i = 0; i <= c; i++)
                //{
                //    if (string.IsNullOrEmpty(Convert.ToString(gridchoice.Rows[i][1])) || string.IsNullOrEmpty(Convert.ToString(gridchoice.Rows[i][1]).TrimStart().TrimEnd()))
                //    {
                //        frmutil.SetErrorForGrid(gridnewvariable, i, 1, QC4Common.Common.Constants.STD_DP.Background);//Data process- Joint - Red color when the column is blank not updating properly
                //        return;
                //    }
                //    else { frmutil.SetErrorForGrid(gridnewvariable, i, 1, QC4Common.Common.Constants.STD_DP.Background, true); }
                //}
                // frmutil.SetErrorForGrid(gridnewvariable, 0, 1, QC4Common.Common.Constants.STD_DP.Background);
                //DataGridRow row = (DataGridRow)gridnewvariable.ItemContainerGenerator.ContainerFromIndex(0);
                ////gridnewvariable
                //DataGridCellsPresenter presenter = Qc4Launcher.Util.CommonFunction.GetVisualChild<DataGridCellsPresenter>(row);
                //DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(1);
                //cell.Background = Brushes.Red;
                return;
            }
            //else //if (lasterrorrow == 1)
            //{
            //    frmutil.SetErrorForGrid(gridnewvariable, 0, 1, QC4Common.Common.Constants.STD_DP.Background, true);
            //}
            int choicecount = 0;
            if (Combo_NewItem_SelectCount.SelectedIndex == 0)
            {
                choicecount = choicegridcount;// Qc4Launcher.Util.Constants.MaxChoiceCount;//Common.Constants.comboautovalue 
            }
            else
            {
                choicecount = Convert.ToInt32(Combo_NewItem_SelectCount.Text);
            }
            if (List_OriginItem_ChoiceList2.Items.Count != choicecount)//(List_OriginItem_ChoiceList2.Items.Count < choicecount)//(List_OriginItem_ChoiceList2.Items.Count <= 0 || List_OriginItem_ChoiceListSelect2.Items.Count <= 0)
            {
                //MCONVERT_NEW_QUESTION_CHOICE_VALIDATION
                MessageDialog.ErrorOk(LocalResource.MCONVERT_NEW_QUESTION_CHOICE_VALIDATION);
                return;
            }
            //if (sourcevariablechoicecount > choicecount)//(List_OriginItem_ChoiceList2.Items.Count <= 0 || List_OriginItem_ChoiceListSelect2.Items.Count <= 0)
            //{
            //    //MCONVERT_NEW_QUESTION_CHOICE_VALIDATION
            //    MessageDialog.ErrorOk(LocalResource.MCONVERT_NEW_QUESTION_CHOICE_VALIDATION_WITH_ENTRY_VARIABLES);
            //    return;
            //}
            lasterrorrow = -1;
            for (int i = 0; i < choicecount; i++)
            {
                if (string.IsNullOrEmpty(Convert.ToString(gridchoice.Rows[i][1])) || string.IsNullOrEmpty(Convert.ToString(gridchoice.Rows[i][1]).TrimStart().TrimEnd()))
                {
                    MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT_, LocalResource.LABEL_CHOICE));
                    gridchoice.Rows[i][2] = true;
                    // frmutil.SetErrorForGrid(gridnewvariable, i, 1, QC4Common.Common.Constants.STD_DP.Background);
                    //DataGridRow row = (DataGridRow)gridnewvariable.ItemContainerGenerator.ContainerFromIndex(i);
                    ////gridnewvariable
                    //DataGridCellsPresenter presenter = frmutil.GetVisualChild<DataGridCellsPresenter>(row);
                    //DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(1);
                    //cell.Background = Brushes.Red;
                    // gridvalidation.Rows[i][0] = true;
                    lasterrorrow = i;
                    return;
                }
                else //if (lasterrorrow == i)
                {
                    gridchoice.Rows[i][2] = false;
                    //  frmutil.SetErrorForGrid(gridnewvariable, i, 1, QC4Common.Common.Constants.STD_DP.Background, true);
                }

                if (Convert.ToString(gridchoice.Rows[i][1]).Length > QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit)
                {
                    gridchoice.Rows[i][1] = gridchoice.Rows[i][1].ToString().PadRight(QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit).Substring(0, QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit);
                    // MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_MAX_CHAR_LIMIT_EXCEEDS, QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit));
                    //MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + string.Format(LocalResource.ERR_MSG_MAX_CHAR_LIMIT_EXCEEDS, QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit));
                    //frmutil.SetErrorForGrid(gridnewvariable, i, 1, QC4Common.Common.Constants.STD_DP.Foreground);
                    //lasterrorrow = i;
                    //return;
                }
                //else //if (lasterrorrow == i)
                //{
                //    frmutil.SetErrorForGrid(gridnewvariable, i, 1, QC4Common.Common.Constants.STD_DP.Foreground, true);
                //}
            }
            if ((Util.Definiotion.VariableDictionary.ContainsKey(newvariable)) && (
                (Util.Definiotion.VariableDictionary[newvariable].AnswerType != answertype) ||
               (Util.Definiotion.VariableDictionary[newvariable].CategoryCount != choicecount))//&& frmutil.IsVariableNameExists(newvariable, PopulatedDictionary.Values.ToList(), 1)
               )
            {
                System.Windows.Forms.DialogResult result;
                result = MessageDialog.InfoYesNo(LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_ITEM_NAME_ALREADY_EXISTS + "\n" + LocalResource.ERR_MSG_INTEGRATE_CONFIRMATION_RENAME_NEW_VARIABLE_AUTOMATICALLY);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    // string variablename = Combo_NewItem_Item.Text;

                    //get new variable name
                    QC4Common.Util.QSUtil qsutil = new QC4Common.Util.QSUtil();
                    newvariable = Combo_NewItem_Item.Text = qsutil.GetVariableName(newvariable, PopulatedDictionary.Values.ToList());
                }
                else if (result == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
                //for (int i = 0; i < choicecount; i++)
                //{
                //    if (!Convert.ToString(gridchoice.Rows[i][1]).Equals((Util.Definiotion.VariableDictionary[Combo_NewItem_Item.Text]).Choices[i]))
                //    {
                //        isnewquestion = true;
                //    }
                //}

                isnewquestion = true;
            }
            else if ((Util.Definiotion.VariableDictionary.ContainsKey(newvariable)) &&
               (Util.Definiotion.VariableDictionary[newvariable].AnswerType == answertype) &&
              (Util.Definiotion.VariableDictionary[newvariable].CategoryCount == choicecount)
              )
            {
                if ((Util.Definiotion.VariableDictionary[newvariable].Question != question))
                {
                    isupdatequestion = true;
                }
                for (int i = 0; i < choicecount; i++)
                {
                    if (!Convert.ToString(gridchoice.Rows[i][1]).Equals((Util.Definiotion.VariableDictionary[newvariable]).Choices[i]))
                    {
                        isupdatequestion = true;
                    }
                }

                isnewquestion = false;
            }
            if (frmutil.IsVariableLengthExceeds(newvariable))
            {
                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_QUESTION_LENGTH_EXCEED, QC4Common.Common.Constants.QsVariableMaxLength)); //ERR_MSG_QUESTION_LENGTH_EXCEED
                return;
            }
            Util.QS.NewQuestionValidation validation = new Util.QS.NewQuestionValidation(newvariable);
            if (!validation.Validation_Variable(true))
            {
                return;
            }
            //need to check the combo choice count and choices text
            string[,] dpsaveinstructios = new string[1, (QC4Common.Common.Constants.DP.MAX_DP_COLUMN - QC4Common.Common.Constants.DP.OnOffColumn) + 1];
            if (Check_After_Unfall.IsChecked == true)
            {
                exclude = 1;
            }
            int columncount = 10 + List_OriginItem_ChoiceList2.Items.Count;
            int listpos = 0;
            try {

                CollectionChoiceList2 = new ObservableCollection<SourceVariableList>(CollectionChoiceList2.OrderBy(i => i.QuestionRowNumber));//#199966
            }
            catch { }
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
                        dpsaveinstructios[0, i] = newvariable;
                        break;
                    case 7://instruction
                        dpsaveinstructios[0, i] = Constants.DP.SubstituteOperatorMCONVERT;
                        break;
                    case 8://exclude-integrate
                        dpsaveinstructios[0, i] = exclude.ToString();
                        break;
                    case 9://no of vars for each all choices

                        string choices = string.Empty;

                        foreach (ChoiceList c in CollectionChoiceListSelect2)
                        {
                            if (choices.Equals(string.Empty))
                            {
                                choices = c.SLNO.ToString();
                            }
                            else
                            {
                                choices += "," + c.SLNO.ToString();
                            }
                        }
                        dpsaveinstructios[0, i] = choices;
                        break;
                    default:
                        //variable and values
                        //sourceVariableList1 variables
                        dpsaveinstructios[0, i] = CollectionChoiceList2[listpos].Variable;
                        listpos++;
                        break;

                }
            }

            DataTable newvariablechoicesdt = ((DataView)gridnewvariable.ItemsSource).ToTable();
            newvariablechoicesdt = frmutil.UnEscapeCRLFFromAllRows(newvariablechoicesdt);
            string[] dt_Choices_columns = frmutil.GetGridList();
            if (dphelper.WriteProcess(workbook, Util.Constants.ProcessingType.CreateNewVariable, newvariable, answertype, question,
                 choicecount, newvariablechoicesdt.DefaultView.ToTable(false, dt_Choices_columns), Constants.DP.SubstituteOperatorMCONVERT, dpsaveinstructios, isnewquestion, writerow, QC4Common.Common.Constants.STD_DP.Process_Create, null, isupdatequestion))//need to pass row num from here for saving 
            {
                MessageDialog.Info(LocalResource.DATAPROSESS_SAVED_SUCCESSFULLY);
                isModifiedProcess = true;
                this.Close();

            }

        }

        private void Command_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NewItemSearchbutton_Click(object sender, RoutedEventArgs e)
        {
            if (MagnifyingGlassButton.VariableList != null)
            {
                Combo_NewItem_Item.Text = MagnifyingGlassButton.VariableList.Variable;
                //Combo_NewItem_AnswerType.ItemsSource = MagnifyingGlassButton.VariableList.AswerTypes;
                // Combo_NewItem_AnswerType.Text = MagnifyingGlassButton.VariableList.AnswerType;
                Text_NewItem_Question.Text = frmutil.Addsinglequete(frmutil.UnEscapeCRLF(MagnifyingGlassButton.VariableList.Question));
                // Combo_NewItem_SelectCount.SelectedIndex = MagnifyingGlassButton.VariableList.Choices.Count;
                // Load Choices and bind DT 
                // gridchoice = frmutil.ClearDataTableColumn(gridchoice, 1);
                //for (int i = 0; i < MagnifyingGlassButton.VariableList.Choices.Count; i++)
                //{
                //    gridchoice.Rows[i][1] = MagnifyingGlassButton.VariableList.Choices[i];
                //}

            }
            this.MagnifyingGlassButton.Focus();

        }

        private void data_grid_NewItemChoices_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {

        }

        private void Button_Help_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(QC4Common.Classes.Help.GetHelpLink(QC4Common.Common.Constants.HelpButtonType.MCONVERT));
        }



        private void Combo_NewItem_SelectCount_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Key.Tab == e.Key)
            {
                e.Handled = true;
                if (Text_NewItem_Question.IsReadOnly == false)
                {
                    this.Text_NewItem_Question.Focus();
                }
                else
                {
                    this.gridnewvariable.Focus();
                }
            }
        }

        private void Combo_NewItem_SelectCount_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void Combo_NewItem_AnswerType_DropDownOpened(object sender, EventArgs e)
        {

        }

        private void Command_OriginItem_Move_Click(object sender, RoutedEventArgs e)
        {
            AddRemoveChoiceList(true);
            FillGridNewVariable();
            EnableSaveButton();
            CreateNewVariableQuestionText();
        }
        private void AddRemoveChoiceList(bool isadd)
        {
            if (isadd)//add to right side
            {
                var items = List_OriginItem_ChoiceList1.SelectedItems.Cast<SourceVariableList>().ToList();
                if (items != null && items.Count > 0)
                {
                    foreach (SourceVariableList item in items)
                    {
                        CollectionChoiceList2.Add(item);
                        CollectionChoiceList1.Remove(item);
                    }
                }
            }
            else//remove from right side
            {
                var items = List_OriginItem_ChoiceList2.SelectedItems.Cast<SourceVariableList>().ToList();
                if (items != null && items.Count > 0)
                {
                    foreach (SourceVariableList item in items)
                    {
                        CollectionChoiceList1.Add(item);
                        CollectionChoiceList2.Remove(item);
                    }
                }
            }



            List_OriginItem_ChoiceList1.DataContext = CollectionChoiceList1.OrderBy(p => p.QuestionRowNumber);
            List_OriginItem_ChoiceList2.DataContext = CollectionChoiceList2.OrderBy(s => s.QuestionRowNumber);
        }
        private void Command_OriginItem_Remove_Click(object sender, RoutedEventArgs e)
        {
            AddRemoveChoiceList(false);
            FillGridNewVariable();
            EnableSaveButton();
            CreateNewVariableQuestionText();
        }

        private void Command_OriginItem_MoveSelect_Click(object sender, RoutedEventArgs e)
        {

            AddRemoveChoiceListSelect2(true);

            CreateNewVariableQuestionText();//need to update som sattements whic get values from grid

            EnableSaveButton();

        }
        private void AddRemoveChoiceListSelect2(bool isadd)
        {

            if (isadd)//add to right side
            {
                var items = List_OriginItem_ChoiceListSelect1.SelectedItems.Cast<ChoiceList>().ToList();
                if (items != null && items.Count > 0)
                {
                    foreach (ChoiceList item in items)
                    {
                        CollectionChoiceListSelect2.Add(item);
                        CollectionChoiceListSelect1.Remove(item);
                    }
                }
            }
            else//remove from right side
            {
                var items = List_OriginItem_ChoiceListSelect2.SelectedItems.Cast<ChoiceList>().ToList();
                if (items != null && items.Count > 0)
                {
                    foreach (ChoiceList item in items)
                    {
                        CollectionChoiceListSelect1.Add(item);
                        CollectionChoiceListSelect2.Remove(item);
                    }
                }
            }



            List_OriginItem_ChoiceListSelect1.DataContext = CollectionChoiceListSelect1.OrderBy(p => p.SLNO);
            List_OriginItem_ChoiceListSelect2.DataContext = CollectionChoiceListSelect2.OrderBy(s => s.SLNO);
        }
        private void Command_OriginItem_RemoveSelect_Click(object sender, RoutedEventArgs e)
        {
            AddRemoveChoiceListSelect2(false);

            CreateNewVariableQuestionText();//need to update som sattements whic get values from grid

            EnableSaveButton();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                NewItemSearchbutton.IsEnabled = false;
                NewItemSearchbutton.Opacity = 0.5;
                Command_Entry.IsEnabled = false;
                //Text_NewItem_Question.IsEnabled = false;
                //Text_NewItem_Question.IsReadOnly = true;

                // Combo_NewItem_Item.Background = "";
                Style style = Application.Current.FindResource("NormalTextBoxMultiLine_READONLY") as Style;
                Combo_NewItem_Item.Style = style;
                Combo_NewItem_Item.IsEnabled = false;

                //Style styletxt = Application.Current.FindResource("NormalTextBox_READONLY") as Style;
                //Text_NewItem_Question.Style = styletxt;
                //Combo_NewItem_Item.IsEnabled = false;

                Combo_NewItem_AnswerType.Text = Util.Constants.AnswerType.MA;

                string[] choicesList = frmutil.LoadComboWithChoices(Qc4Launcher.Util.Constants.MaxChoiceCount);
                Combo_NewItem_SelectCount.ItemsSource = choicesList;
                Combo_NewItem_SelectCount.SelectedIndex = 0;

                PopulateMAVariableList();
                this.Combo_OriginItem_Item.DataContext = SourceVariableListView1;
                LoadGridValues(Qc4Launcher.Util.Constants.MaxChoiceCount, null);

                try
                {
                    Text_OriginItem_AnswerType.IsTabStop = false;
                    Text_OriginItem_SelectCount.IsTabStop = false;
                    Text_OriginItem_Question.IsTabStop = false;
                    Combo_OriginItem_Item.Focus();
                }
                catch { }

                if (processingoption == QC4Common.Common.Constants.STD_DP.Process_Edit || processingoption == QC4Common.Common.Constants.STD_DP.Process_Copy)
                {
                    iseditorcopy = true;
                    string[,] objarray = dphelper.GetRangevalues(readrow, writerow, workbook);
                    SetValues(objarray);
                    Command_Entry.IsEnabled = true;
                    if (processingoption == QC4Common.Common.Constants.STD_DP.Process_Edit)
                    {
                        Color Combo_ProcessTextboxColor = (Color)ColorConverter.ConvertFromString(QC4Common.Common.Constants.STD_DP.ProcessTextboxColor);
                        Combo_Process.Background = new System.Windows.Media.SolidColorBrush(Combo_ProcessTextboxColor);
                        Color Combo_ProcessTextboxforeColor = (Color)ColorConverter.ConvertFromString(QC4Common.Common.Constants.STD_DP.ProcessTextboxforeColor);
                        Combo_Process.Foreground = new System.Windows.Media.SolidColorBrush(Combo_ProcessTextboxforeColor);

                        Combo_OriginItem_Item.IsEnabled = false;
                        Combo_OriginItem_Item.IsReadOnly = true;
                        Combo_NewItem_Item.IsReadOnly = true;

                        Combo_NewItem_SelectCount.IsEnabled = false;
                        Combo_NewItem_SelectCount.IsTabStop = false;

                        // Combo_NewItem_Item.IsEnabled = false;
                        Combo_NewItem_Item.IsTabStop = false;
                        NewItemSearchbutton.IsEnabled = false;
                        NewItemSearchbutton.Opacity = 0.5;
                        NewItemSearchbutton.IsTabStop = false;

                        List_OriginItem_ChoiceList1.IsReadOnly = true;
                        List_OriginItem_ChoiceList1.IsEnabled = false;
                        List_OriginItem_ChoiceList1.IsTabStop = false;
                        List_OriginItem_ChoiceList2.IsReadOnly = true;
                        List_OriginItem_ChoiceList2.IsEnabled = false;
                        List_OriginItem_ChoiceList2.IsTabStop = false;
                        Command_OriginItem_Move.IsEnabled = false;
                        Command_OriginItem_Move.IsTabStop = false;
                        Command_OriginItem_Remove.IsEnabled = false;
                        Text_NewItem_Question.IsEnabled = true;
                        Text_NewItem_Question.IsReadOnly = false;
                        Text_NewItem_Question.Background = Brushes.White;

                    }
                    else
                    {
                        Combo_NewItem_Item.IsEnabled = true;
                        Combo_NewItem_Item.IsReadOnly = false;

                        Combo_NewItem_Item.Background = Brushes.White;
                        Style stylee = Application.Current.FindResource("NormalTextBoxMultiLine") as Style;
                        Combo_NewItem_Item.Style = stylee;


                        NewItemSearchbutton.IsEnabled = true;
                        NewItemSearchbutton.Opacity = 1;

                        Text_NewItem_Question.IsEnabled = true;
                        Text_NewItem_Question.IsReadOnly = false;
                        Text_NewItem_Question.Background = Brushes.White;

                    }

                    List_OriginItem_ChoiceList2.DataContext = CollectionChoiceList2;
                    List_OriginItem_ChoiceListSelect2.DataContext = CollectionChoiceListSelect2;
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }
        private void PopulateMAVariableList()
        {
            var SettingSheet = ExcelUtil.GetWorkSheetByCodeName(workbook, "Sheet91");
            PopulatedDictionary = Util.Definiotion.VariableDictionary;

            if (SettingSheet != null)
            {
                Microsoft.Office.Interop.Excel.Range Range = SettingSheet.get_Range("List_Item_SAMA");
                if (Range != null)
                {
                    MASAvariables.Clear();

                    if (Range.Count == 1)
                    {
                        MASAvariables.Add(Convert.ToString(Range.Value));
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
                                        sourceVariableList1.Add(new SourceVariableList()
                                        {
                                            Variable = frmutil.EscapeCRLF(qs.Variable),
                                            AnswerType = qs.AnswerType,
                                            Question = frmutil.EscapeCRLF(qs.Question),
                                            Tableheading = frmutil.EscapeCRLF(qs.TableHeading),
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

        private DataTable CreateChoiceTable()
        {
            DataTable griddata = new DataTable();
            griddata.Columns.Add("SL");
            griddata.Columns.Add("Choice");
            griddata.Columns.Add("IsBlank");
            return griddata;
        }
        private DataTable CreateChoiceListSelectTable()
        {
            DataTable GridChoiceListSelect1 = new DataTable();
            GridChoiceListSelect1.Columns.Add("SL");
            GridChoiceListSelect1.Columns.Add("Choice");
            return GridChoiceListSelect1;
        }
        private DataTable CreateChoiceListTable()
        {
            DataTable GridChoiceListSelect1 = new DataTable();
            GridChoiceListSelect1.Columns.Add("Variablename");
            return GridChoiceListSelect1;
        }
        private void LoadGridValues(int rowcount, List<string> choices)
        {
            gridchoice = CreateChoiceTable();
            DataRow drchoice;
            for (int i = 0; i < rowcount; i++)
            {
                drchoice = gridchoice.NewRow();
                try
                {
                    drchoice["SL"] = (i + 1).ToString();
                    drchoice["Choice"] = string.Empty;
                    drchoice["IsBlank"] = false;
                    gridchoice.Rows.Add(drchoice);
                }
                catch { }
            }
            gridnewvariable.DataContext = gridchoice;
        }
        private void LoadChoiceListSelectGridValues(int rowcount, List<string> choices)
        {
            // GridChoiceListSelect1 = CreateChoiceTable();
            DataRow drchoice;
            for (int i = 0; i < rowcount; i++)
            {


            }
            // List_OriginItem_ChoiceListSelect1.DataContext = GridChoiceListSelect1;
        }

        private void SetComboValue(object sender, int sourcevariable)
        {

        }
        private void ChangeRowCount(int rownum)
        {

            try
            {
                if (rownum < 0)
                {
                    return;
                }
                int rowcount = gridchoice == null ? Qc4Launcher.Util.Constants.MaxChoiceCount : gridchoice.Rows.Count;
                if (rownum <= rowcount)
                {
                    for (int i = rowcount - 1; i >= rownum; i--)
                    {
                        gridchoice.Rows.RemoveAt(i);
                    }
                }
                else
                {
                    DataRow drchoice;
                    for (int i = 0; i < rownum - rowcount; i++)
                    {

                        drchoice = gridchoice.NewRow();
                        try
                        {
                            drchoice["SL"] = (i + rowcount + 1).ToString();
                            drchoice["Choice"] = string.Empty;
                            gridchoice.Rows.Add(drchoice);
                        }
                        catch (Exception ex) { }
                    }
                }
                gridnewvariable.DataContext = gridchoice;
            }
            catch (Exception e) { }
        }
        private void CreateNewQuestionFromGrid()
        {
            string creatednewquestion = string.Empty;

            //logic getting variaiav=ble choices from grid

            string variablechoice = string.Empty;
            var items = List_OriginItem_ChoiceListSelect2.Items.Cast<ChoiceList>().ToList();
            if (items != null && items.Count > 0)
            {
                foreach (ChoiceList item in items)
                    creatednewquestion += string.Format(" {0}{1} {2}{3}", LocalResource.MCONVERT_OPEN_SQUARE_BRACKET, item.SLNO, frmutil.Addsinglequete(frmutil.UnEscapeCRLF(item.Choice)), LocalResource.MCONVERT_CLOSE_SQUARE_BRACKET);
            }
            Text_NewItem_Question.Text = frmutil.UnEscapeCRLF(tableheading) + creatednewquestion;
        }

        private void Gridnewvariable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void EnableSaveButton()
        {
            if (List_OriginItem_ChoiceList2.Items.Count > 0 && List_OriginItem_ChoiceListSelect2.Items.Count > 0)
            {
                Command_Entry.IsEnabled = true;
            }
            else
            {
                Command_Entry.IsEnabled = false;
            }
        }

        private void Cmmand_OriginItem_Move_Click(object sender, RoutedEventArgs e)
        {
            EnableSaveButton();
        }
        private void FillGridNewVariable()
        {


            var items = List_OriginItem_ChoiceList2.Items.Cast<SourceVariableList>().ToList();
            if (items != null && items.Count > 0)
            {
                Combo_NewItem_SelectCount.SelectedIndex = List_OriginItem_ChoiceList2.Items.Count;
                int i = 0;
                foreach (SourceVariableList item in items)
                {
                    gridchoice.Rows[i]["Choice"] = frmutil.EscapeCRLF(item.Question);
                    i++;
                }
            }
            else
            {
                LoadGridValues(Qc4Launcher.Util.Constants.MaxChoiceCount, null);
                Combo_NewItem_SelectCount.SelectedIndex = 0;
            }

        }
        private void SetValues(string[,] values)
        {
            int choicecount = 0;
            int exclude = 0;
            string newquestionvariable = string.Empty;
            string question = string.Empty;
            string sourcevariablename = string.Empty;
            int index = -1;
            string choices = string.Empty;
            if (Util.Definiotion.VariableDictionary.ContainsKey(Convert.ToString(values[0, 6])))
            {
                newquestionvariable = Convert.ToString(values[0, 6]);
                question = (Util.Definiotion.VariableDictionary[newquestionvariable].Question);
                choicecount = (Util.Definiotion.VariableDictionary[newquestionvariable].CategoryCount);
            }
            Combo_NewItem_Item.Text = newquestionvariable;
            Text_NewItem_Question.Text = question;
            Combo_NewItem_SelectCount.SelectedIndex = choicecount;
            if (!string.IsNullOrEmpty(Convert.ToString(values[0, 8])))
            {
                exclude = int.Parse(Convert.ToString(values[0, 8]));
            }
            if (exclude == 1)
            {
                Check_After_Unfall.IsChecked = true;
            }
            else
            {
                Check_After_Unfall.IsChecked = false;
            }
            for (int i = 0; i < choicecount; i++)
            {
                gridchoice.Rows[i][1] = frmutil.EscapeCRLF((Util.Definiotion.VariableDictionary[newquestionvariable]).Choices[i]);
            }
            choices = Convert.ToString(values[0, 9]);
            sourcevariablename = Convert.ToString(values[0, 10]);
            Combo_OriginItem_Item.SelectedIndex = GetIndexByVariableName(sourcevariablename, SourceVariableListView1);
            Text_OriginItem_AnswerType.Text = (Util.Definiotion.VariableDictionary[sourcevariablename]).AnswerType;
            Text_OriginItem_SelectCount.Text = (Util.Definiotion.VariableDictionary[sourcevariablename]).CategoryCount.ToString();
            Text_OriginItem_Question.Text = (Util.Definiotion.VariableDictionary[sourcevariablename]).Question;
            List<string> variablenames = new List<string>();
            List<int> choicenames = new List<int>();
            for (int i = 10; i < 10 + choicecount; i++)
            {
                variablenames.Add(Convert.ToString(values[0, i]));
            }
            LoadEntryVariables(frmutil.EscapeCRLF((Util.Definiotion.VariableDictionary[sourcevariablename]).AnswerType), (Util.Definiotion.VariableDictionary[sourcevariablename]).Choices, variablenames);
            Text_NewItem_Question.Text = question;
            tableheading = (Util.Definiotion.VariableDictionary[sourcevariablename]).TableHeading;
            //CreateNewQuestionFromGrid();
            choicenames = frmutil.GetListfromCommaseperatedValues(choices);
            LoadChoicesOfVariable((Util.Definiotion.VariableDictionary[sourcevariablename]).Choices, choicenames);
            if (processingoption == QC4Common.Common.Constants.STD_DP.Process_Copy)
            {
                QC4Common.Util.QSUtil qsutil = new QC4Common.Util.QSUtil();
                Combo_NewItem_Item.Text = newquestionvariable = qsutil.GetVariableName(newquestionvariable, PopulatedDictionary.Values.ToList());
            }
        }
        public int GetIndexByVariableName(string variablename, ObservableCollection<SourceVariableList> SourceVariableListView)
        {
            int index = 0;
            SourceVariableList svlist = SourceVariableListView.Where(z => z.Variable == variablename).FirstOrDefault();
            if (svlist != null)
            {
                index = SourceVariableListView.IndexOf(svlist);
            }
            else
            {
                index = -1;
            }
            return index;
        }
        private void DataGrid_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {// PreviewKeyDown="DataGrid_OnPreviewKeyDown"
            if (e.Key == Key.Tab) { gridnewvariable.ScrollIntoView(gridnewvariable.Items[0]); gridnewvariable.SelectedIndex = 0; }
            var uiElement = e.OriginalSource as UIElement;
            bool _ShiftModifierPressed = (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift));
            if (!_ShiftModifierPressed && (e.Key == Key.Enter) && uiElement != null)//|| Key.Tab == e.Key
            {
                e.Handled = true;
                if (gridnewvariable.SelectedIndex == (gridnewvariable.Items.Count - 1))
                {
                    if (Command_Entry.IsEnabled == true)
                    {
                        this.Command_Entry.Focus();
                    }
                    else
                    {
                        this.Command_Cancel.Focus();
                    }
                    return;
                }
                int row = gridnewvariable.SelectedIndex == -1 ? 0 : gridnewvariable.SelectedIndex;
                gridnewvariable.SelectedIndex = row + 1;
                uiElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Down));
                e.Handled = true;
            }
            else
            {
                QC4Common.Util.GridCommonFunc.Arrow(sender, e);
            }
            bool _altModifierPressed = (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl));
            if (_altModifierPressed && Keyboard.IsKeyDown(Key.V))
            {

                HandleCopyPaste(sender, e);
            }
            if (e.Key == Key.Delete)
            {
                try
                {
                    int RowIndex = 0;
                    if ((gridnewvariable.SelectedItems != null) && (gridnewvariable.SelectedItems.Count > 0))
                    {
                        for (int i = 0; i < gridnewvariable.SelectedItems.Count; i++)
                        {
                            var presentRow = (System.Data.DataRowView)gridnewvariable.SelectedItems[i];
                            RowIndex = Convert.ToInt32(presentRow.Row.ItemArray[0].ToString());
                            gridchoice.Rows[RowIndex - 1][1] = string.Empty;

                        }
                    }
                }
                catch { }
            }
        }
        int gridselectedindex = -1;
        private void Choices_grid_CurrentCellChanged(object sender, EventArgs e)
        {
            string name = string.Empty;
            var dg = (System.Windows.Controls.DataGrid)sender;
            try { name = dg.Name; } catch { }
            if (name == List_OriginItem_ChoiceList1.Name)
            {
                gridselectedindex = List_OriginItem_ChoiceList1.SelectedIndex;
            }
            else if (name == List_OriginItem_ChoiceList2.Name)
            {
                gridselectedindex = List_OriginItem_ChoiceList2.SelectedIndex;
            }
            ExpGrid = dg;
            dg.Focus();
        }
        private void Combo_NewItem_SelectCount_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            System.Windows.Controls.ComboBox combo = (System.Windows.Controls.ComboBox)sender;

            int result = 0;
            if (int.TryParse(combo.Text, out result))
            {
                if (!(result <= Qc4Launcher.Util.Constants.MaxChoiceCount && result >= 1))
                {
                    MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_CHOICE_COUNT_EXCEED_LIMIT, QC4Common.CommonResource.LBL_AUTO, "1-" + Qc4Launcher.Util.Constants.MaxChoiceCount, LocalResource.LBL_NO_OF_CHOICES));
                    combo.SelectedIndex = Qc4Launcher.Util.Constants.MaxChoiceCount;
                }
            }
            else
            {
                if (!(combo.Text == QC4Common.CommonResource.LBL_AUTO))
                {
                    MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_CHOICE_INVALID_VALUE, LocalResource.LBL_NO_OF_CHOICES, LocalResource.ERR_MSG_INTEGRATE_CHOICE_AUTO));
                    combo.SelectedIndex = Qc4Launcher.Util.Constants.MaxChoiceCount;
                }
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
                System.Windows.Controls.ComboBox sen = ((System.Windows.Controls.ComboBox)sender);
                if ((Key.Back == e.Key || ((sen.SelectedIndex == -1 && sen.Text == "") || (sen.SelectedIndex == -1 && sen.Text != ""))) && (comboBox.IsKeyboardFocusWithin || comboBox.IsDropDownOpen))
                {
                    //LastSelected = 0;
                    //comboBox.SelectedIndex = 0;
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
                _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
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
                //else if (comboBox.SelectedIndex > 0)
                //    LastSelected = comboBox.SelectedIndex;
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
                _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void Combo_OriginItem_Item_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Key.Tab != e.Key)
            {
                Combo_KeyDown(Combo_OriginItem_Item, sender, e);
            }
            else if (Key.Tab == e.Key)
            {
                e.Handled = true;
                this.List_OriginItem_ChoiceList1.Focus();
            }
        }
        private void Combo_OriginItem_Item_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (Key.Tab != e.Key && !frmutil.IsSystemKey(e))
            {
                Combo_KeyUp(Combo_OriginItem_Item, sender, e);
            }
        }

        private void Combo_sourceVariable_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (!FirstFocus)
            {
                System.Windows.Controls.ComboBox sen = ((System.Windows.Controls.ComboBox)sender);
                //sen.IsDropDownOpen = true;
            }
            else
                FirstFocus = false;
        }


        #endregion

        private void DisableRightMenu(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void FocusNextControlForGrid(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                if (Combo_OriginItem_Item.IsFocused)
                {
                    List_OriginItem_ChoiceList1.Focus();
                    e.Handled = true;
                }
                else if (List_OriginItem_ChoiceList1.IsFocused)
                {
                    Command_OriginItem_Move.Focus();
                    e.Handled = true;
                }
                else if (Command_OriginItem_Remove.IsFocused)
                {
                    List_OriginItem_ChoiceList2.Focus();
                }
                else if (List_OriginItem_ChoiceList2.IsFocused)
                {
                    List_OriginItem_ChoiceListSelect1.Focus();
                }
                else if (List_OriginItem_ChoiceListSelect1.IsFocused)
                {
                    Command_OriginItem_MoveSelect.Focus();
                }
                else if (Command_OriginItem_RemoveSelect.IsFocused)
                {
                    List_OriginItem_ChoiceListSelect2.Focus();
                }
                else if (Check_After_Unfall.IsFocused)
                {
                    if (Combo_NewItem_Item.IsEnabled)
                    { Combo_NewItem_Item.Focus(); }
                    else { Text_NewItem_Question.Focus(); }

                }
                else if (Text_NewItem_Question.IsFocused)
                {
                    gridnewvariable.Focus();
                }


            }
        }

        private void Grid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                if (((DataGrid)sender).Name == "List_OriginItem_ChoiceList2")
                {
                    List_OriginItem_ChoiceListSelect1.Focus();
                }
                else if (((DataGrid)sender).Name == "List_OriginItem_ChoiceListSelect1")
                {
                    Command_OriginItem_MoveSelect.Focus();
                }
                else if (((DataGrid)sender).Name == "List_OriginItem_ChoiceListSelect2")
                {
                    Check_After_Unfall.Focus();
                }

            }
            else
                QC4Common.Util.GridCommonFunc.Arrow(sender, e);
        }
        private void b1SetColor(object sender, MouseButtonEventArgs e)
        {
            if (ExpGrid != null)
                ExpGrid.Focus();
        }
        private void HandleCopyPaste(object sender, KeyEventArgs e)
        {
            var uiElement = e.OriginalSource as UIElement;
            try
            {
                Util.QS.GridCopyPaste copyPaste = new Util.QS.GridCopyPaste();
                var data = copyPaste.PastetoDatagrid(sender);
                int datagridColumn = gridnewvariable.CurrentCell.Column.DisplayIndex;
                DataGridCell cell = frmutil.GetCell(gridnewvariable, gridnewvariable.SelectedIndex, datagridColumn);
                if (!cell.IsEditing)
                {
                    e.Handled = true;
                    int No_Row = copyPaste.No_Row;
                    int No_Column = copyPaste.No_Columns;
                    if (data != null)
                    {
                        e.Handled = true;
                        int datagridRow = gridnewvariable.SelectedIndex;
                        if (gridnewvariable.CurrentCell.Column.DisplayIndex == 1)
                        {
                            //selection in choice
                            if (No_Column > 1 || No_Row > gridnewvariable.Items.Count - (gridnewvariable.SelectedIndex) + 1)//10 row of grid 
                            {
                                MessageDialog.ErrorOk(LocalResource.COPY_ERROR_MSG);
                                e.Handled = true;
                            }
                            else
                            {
                                int RowIndex = gridnewvariable.SelectedIndex;
                                for (int i = 0; i < No_Row; i++, RowIndex++)
                                {
                                    for (int j = 1, col = 1; j <= No_Column; j++, col++)
                                    {
                                        //if (col == 1)
                                        //{
                                        //    if (data[i, (j - 1)].ToString() == "=" || data[i, (j - 1)].ToString() == "<>")
                                        //    {
                                        //        dt.Rows[RowIndex][col] = data[i, (j - 1)].ToString();
                                        //    }
                                        //}
                                        //else
                                        //{
                                        //    dt.Rows[RowIndex][col] = data[i, (j - 1)].ToString();
                                        //}

                                        gridchoice.Rows[RowIndex][col] = Convert.ToString(data[i, (j - 1)]);
                                    }

                                }

                            }
                        }
                        //else if (gridnewvariable.CurrentCell.Column.DisplayIndex == 4)
                        //{
                        //    if (No_Column > 1 || No_Row > 10 - (gridnewvariable.SelectedIndex + 1))
                        //    {
                        //        MessageDialog.ErrorOk(LocalResource.COPY_ERROR_MSG);
                        //    }
                        //    else
                        //    {
                        //        int RowIndex = gridnewvariable.SelectedIndex;
                        //        for (int i = 0; i < No_Row; i++, RowIndex++)
                        //        {
                        //            for (int j = 1, col = 3; j <= No_Column; j++, col++)
                        //            {
                        //                dt.Rows[RowIndex][col] = data[i, (j - 1)].ToString();
                        //            }
                        //        }
                        //    }
                        //}

                    }
                }
                else
                {
                    if (Clipboard.ContainsText(TextDataFormat.UnicodeText))
                    {
                        clipboardText = string.Empty;
                        clipboardText = Clipboard.GetText(TextDataFormat.UnicodeText);
                    }
                    Clipboard.SetText(Convert.ToString(data[0, 0]));//Clipboard.SetText(data[0, 0].ToString());
                }
            }
            catch (Exception ex) { _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException); }
        }


        private void Command_OriginItem_Remove_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                if (List_OriginItem_ChoiceList2.IsEnabled == true)
                {
                    this.List_OriginItem_ChoiceList2.Focus();
                }
                else if (List_OriginItem_ChoiceListSelect1.IsEnabled == true)
                {
                    this.List_OriginItem_ChoiceListSelect1.Focus();
                }
                else
                {
                    Command_OriginItem_MoveSelect.Focus();
                }
            }
        }

        private void Command_OriginItem_RemoveSelect_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                if (List_OriginItem_ChoiceListSelect2.IsEnabled == true)
                {
                    this.List_OriginItem_ChoiceListSelect2.Focus();
                }
                else
                {
                    Check_After_Unfall.Focus();
                }
            }
        }
        private void Check_After_Unfall_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                if (Combo_NewItem_Item.IsEnabled == true)
                {
                    this.Combo_NewItem_Item.Focus();
                }
                else
                {
                    this.Combo_NewItem_SelectCount.Focus();
                }
            }
        }
        private void Button_Help_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                this.Combo_OriginItem_Item.Focus();
            }
        }
        private void Text_NewItem_Question_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Key.Tab == e.Key)
            {
                e.Handled = true;
                this.gridnewvariable.Focus();
            }
        }
        private void CreateNewVariableQuestionText()
        {
            if (List_OriginItem_ChoiceList2 != null)
            {
                string tableheading = string.Empty;
                string sourcevariable = Combo_OriginItem_Item.Text;
                string newHeading = string.Empty;
                List<string> tableHeadings = new List<string>();
                if (!string.IsNullOrEmpty(sourcevariable) && Util.Definiotion.VariableDictionary.ContainsKey(sourcevariable))
                {
                    tableheading = Util.Definiotion.VariableDictionary[sourcevariable].TableHeading;
                    tableHeadings.Add(Util.Definiotion.VariableDictionary[sourcevariable].TableHeading);
                }
                foreach (SourceVariableList variable in List_OriginItem_ChoiceList2.Items)
                {
                    if (!string.IsNullOrEmpty(variable.Variable) && Util.Definiotion.VariableDictionary.ContainsKey(variable.Variable) && !string.Equals(sourcevariable, variable.Variable))
                    {
                        newHeading = Util.Definiotion.VariableDictionary[variable.Variable].TableHeading;
                        if (newHeading != tableheading.Trim() && !tableHeadings.Contains(newHeading))
                        {
                            tableheading += " ";
                            tableheading += newHeading;
                            tableHeadings.Add(newHeading);
                        }
                    }
                }
                if (string.IsNullOrEmpty(tableheading) && Util.Definiotion.VariableDictionary.ContainsKey(sourcevariable))
                {
                    tableheading = Util.Definiotion.VariableDictionary[sourcevariable].TableHeading;
                }
                string creatednewquestion = string.Empty;

                //logic getting variaiav=ble choices from grid

                var items = List_OriginItem_ChoiceListSelect2.Items.Cast<ChoiceList>().ToList();
                if (items != null && items.Count > 0)
                {
                    foreach (ChoiceList item in items)
                        creatednewquestion += string.Format(" {0}{1} {2}{3}", LocalResource.MCONVERT_OPEN_SQUARE_BRACKET, item.SLNO, frmutil.Addsinglequete(frmutil.UnEscapeCRLF(item.Choice)), LocalResource.MCONVERT_CLOSE_SQUARE_BRACKET);
                }
                Text_NewItem_Question.Text = frmutil.UnEscapeCRLF(tableheading) + creatednewquestion;
                //Text_NewItem_Question.Text = tableheading;
            }
            else
            {
                Text_NewItem_Question.Text = string.Empty;
            }

        }

        private void List_OriginItem_ChoiceList1_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

            try
            {
                if ((SourceVariableList)List_OriginItem_ChoiceList1.SelectedItem != null)
                {
                    Text_OriginItem_Question.Text = ((SourceVariableList)List_OriginItem_ChoiceList1.SelectedItem).Question;
                }
            }
            catch { }
        }
        private void List_OriginItem_ChoiceList2_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if ((SourceVariableList)List_OriginItem_ChoiceList2.SelectedItem != null)
                {
                    Text_OriginItem_Question.Text = ((SourceVariableList)List_OriginItem_ChoiceList2.SelectedItem).Question;
                }
            }
            catch { }
        }

        private void List_OriginItem_ChoiceList1_SelectedCellSelected(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((SourceVariableList)List_OriginItem_ChoiceList2.SelectedItem != null)
                {
                    Text_OriginItem_Question.Text = ((SourceVariableList)List_OriginItem_ChoiceList2.SelectedItem).Question;
                }
            }
            catch { }
        }

        private void List_OriginItem_ChoiceList1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if ((SourceVariableList)List_OriginItem_ChoiceList1.SelectedItem != null)
                {
                    Text_OriginItem_Question.Text = ((SourceVariableList)List_OriginItem_ChoiceList1.SelectedItems[List_OriginItem_ChoiceList1.SelectedItems.Count - 1]).Question; //((SourceVariableList)List_OriginItem_ChoiceList1.SelectedItem).Question;
                }
            }
            catch { }
        }
        private void List_OriginItem_ChoiceList2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if ((SourceVariableList)List_OriginItem_ChoiceList2.SelectedItem != null)
                {
                    Text_OriginItem_Question.Text = ((SourceVariableList)List_OriginItem_ChoiceList2.SelectedItems[List_OriginItem_ChoiceList2.SelectedItems.Count - 1]).Question;// ((SourceVariableList)List_OriginItem_ChoiceList2.SelectedItem).Question;
                }
            }
            catch { }
        }

        private void List_OriginItem_ChoiceList1_GotFocus(object sender, RoutedEventArgs e)
        {

            int index = List_OriginItem_ChoiceList1.SelectedIndex;
            if (index >= 0)
            {
                Text_OriginItem_Question.Text = CollectionChoiceList1[index].Question;
            }
        }

        private void List_OriginItem_ChoiceList2_GotFocus(object sender, RoutedEventArgs e)
        {
            int index = List_OriginItem_ChoiceList2.SelectedIndex;
            if (index >= 0)
            {
                Text_OriginItem_Question.Text = CollectionChoiceList2[index].Question;
            }

        }

        private void List_OriginItem_ChoiceList1_GotMouseCapture(object sender, MouseEventArgs e)
        {
            try
            {
                if ((SourceVariableList)List_OriginItem_ChoiceList1.SelectedItem != null)
                {
                    Text_OriginItem_Question.Text = ((SourceVariableList)List_OriginItem_ChoiceList1.SelectedItems[List_OriginItem_ChoiceList1.SelectedItems.Count - 1]).Question;// ((SourceVariableList)List_OriginItem_ChoiceList1.SelectedItem).Question;
                }
            }
            catch { }
        }

        private void List_OriginItem_ChoiceList2_GotMouseCapture(object sender, MouseEventArgs e)
        {
            try
            {
                if ((SourceVariableList)List_OriginItem_ChoiceList2.SelectedItem != null)
                {
                    Text_OriginItem_Question.Text = ((SourceVariableList)List_OriginItem_ChoiceList2.SelectedItems[List_OriginItem_ChoiceList2.SelectedItems.Count - 1]).Question;// ((SourceVariableList)List_OriginItem_ChoiceList2.SelectedItem).Question;
                }
            }
            catch { }
        }

        private void List_OriginItem_ChoiceList2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (List_OriginItem_ChoiceList2.SelectedItems != null)
                {
                    string q = ((SourceVariableList)List_OriginItem_ChoiceList2.SelectedItems[List_OriginItem_ChoiceList2.SelectedItems.Count - 1]).Question;
                    Text_OriginItem_Question.Text = q;
                }
            }
            catch { }

        }

        private void List_OriginItem_ChoiceList1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (List_OriginItem_ChoiceList1.SelectedItems != null)
                {
                    string q = ((SourceVariableList)List_OriginItem_ChoiceList1.SelectedItems[List_OriginItem_ChoiceList1.SelectedItems.Count - 1]).Question;//.Cast<DataRowView>().LastOrDefault();

                    Text_OriginItem_Question.Text = q;
                }
            }
            catch { }
        }

        private void Gridnewvariable_CopyingRowClipboardContent(object sender, DataGridRowClipboardEventArgs e)
        {
            try
            {
                e.ClipboardRowContent.RemoveAt(0);
            }
            catch { }
        }
        private DataTable CreateGridValidation()
        {
            DataTable griddata = new DataTable();
            griddata.Columns.Add("IsChoiceEmpty");
            griddata.Columns.Add("IsChoiceInvalid");
            return griddata;
        }
        private void LoadGridValidation(int rowcount)
        {
            gridvalidation = CreateGridValidation();
            DataRow drchoice;
            for (int i = 0; i < rowcount; i++)
            {
                drchoice = gridvalidation.NewRow();
                try
                {
                    drchoice["IsChoiceEmpty"] = false;
                    drchoice["IsChoiceInvalid"] = false;
                    gridvalidation.Rows.Add(drchoice);
                }
                catch { }
            }
        }

        private void Gridnewvariable_IsMouseCaptureWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (gridnewvariable != null && gridnewvariable.CurrentCell != null && gridnewvariable.CurrentCell.Column != null && gridnewvariable.CurrentCell.Column.DisplayIndex == 1)
            {
                gridchoice.Rows[gridnewvariable.SelectedIndex][2] = false;
                // frmutil.SetErrorForGrid(gridnewvariable, gridnewvariable.SelectedIndex, gridnewvariable.CurrentCell.Column.DisplayIndex, QC4Common.Common.Constants.STD_DP.Background, true);
                frmutil.SetErrorForGrid(gridnewvariable, gridnewvariable.SelectedIndex, gridnewvariable.CurrentCell.Column.DisplayIndex, QC4Common.Common.Constants.STD_DP.Foreground, true);
            }
        }

        private void List_OriginItem_ChoiceListSelect1_CopyingRowClipboardContent(object sender, DataGridRowClipboardEventArgs e)
        {
            try
            {
                //Removes < column from copying
                e.ClipboardRowContent.RemoveAt(0);
            }
            catch (Exception ex) { _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException); }
        }

        private void List_OriginItem_ChoiceListSelect2_CopyingRowClipboardContent(object sender, DataGridRowClipboardEventArgs e)
        {
            try
            {
                //Removes < column from copying
                e.ClipboardRowContent.RemoveAt(0);
            }
            catch (Exception ex) { _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException); }
        }

        private void Button_Help_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {

        }

        private void Command_OriginItem_Remove_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {

        }

        private void Check_After_Unfall_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {

        }

        private void Gridnewvariable_PreviewKeyUp(object sender, KeyEventArgs e)
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
                int datagridColumn = gridnewvariable.CurrentCell.Column.DisplayIndex;
                DataGridCell cell = frmutil.GetCell(gridnewvariable, gridnewvariable.SelectedIndex, datagridColumn);
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

            }
        }

        private void Command_Cancel_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Key.Tab == e.Key)
            {
                Button_Help.Focus();
                e.Handled = true;
            }
        }
        DataProcessNewLoad dataLoad = new DataProcessNewLoad();
        private void Combo_NewItem_Item_TextChanged(object sender, TextChangedEventArgs e)
        {
            QuestionSettings qs = dataLoad.Txt_Change_New_Item(Combo_NewItem_Item.Text);
            if (qs != null)
            {

                Text_NewItem_Question.Text = frmutil.Addsinglequete(frmutil.UnEscapeCRLF(qs.Question));


            }
        }
    }
}
