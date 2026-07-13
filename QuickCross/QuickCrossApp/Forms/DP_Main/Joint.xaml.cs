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
using Qc4Launcher.Forms.UserControls;
using excel = Microsoft.Office.Interop.Excel;
using System.Collections.ObjectModel;
using ExcelAddIn.Common;
using QC4Common.Model;
using System.Data;
using System.Reflection;
using log4net;
using Qc4Launcher.Logic.DataImport;

namespace Qc4Launcher.Forms.DP_Main
{
    /// <summary>
    /// Interaction logic for Joint.xaml
    /// </summary>
    public partial class Joint : Window
    {
        public bool isModifiedProcess = false;
        QC4Common.Util.FormUtil frmutil = new QC4Common.Util.FormUtil();
        excel.Workbook workbook;
        int readrow;
        int writerow;
        string Processingtype;
        string processingoption;
        DataTable gridchoice;
        bool variableselectionchange = false;
        string newvariablenamenamefromsource = string.Empty;
        bool iseditorcopy = false;
        string clipboardText = "";
        int clearselection = -1;
        Logic.DataProcessHelper dphelper = new Logic.DataProcessHelper();
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static List<string> MASAvariables = new List<string>();
        private Dictionary<String, QuestionSettings> PopulatedDictionary = new Dictionary<string, QuestionSettings>();
        private ObservableCollection<SourceVariableList> sourceVariableList1 = new ObservableCollection<SourceVariableList>();
        private ObservableCollection<SourceVariableList> sourceVariableList2 = new ObservableCollection<SourceVariableList>();
        private ObservableCollection<SourceVariableList> sourceVariableList3 = new ObservableCollection<SourceVariableList>();
        private ObservableCollection<SourceVariableList> sourceVariableList4 = new ObservableCollection<SourceVariableList>();
        private ObservableCollection<SourceVariableList> sourceVariableList5 = new ObservableCollection<SourceVariableList>();
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
        public ObservableCollection<SourceVariableList> SourceVariableListView2
        {
            get
            {
                return sourceVariableList2;
            }
            set
            {
                sourceVariableList2 = value;
            }
        }
        public ObservableCollection<SourceVariableList> SourceVariableListView3
        {
            get
            {
                return sourceVariableList3;
            }
            set
            {
                sourceVariableList3 = value;
            }
        }
        public ObservableCollection<SourceVariableList> SourceVariableListView4
        {
            get
            {
                return sourceVariableList4;
            }
            set
            {
                sourceVariableList4 = value;
            }
        }
        public ObservableCollection<SourceVariableList> SourceVariableListView5
        {
            get
            {
                return sourceVariableList5;
            }
            set
            {
                sourceVariableList5 = value;
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
                Choices = null
            });
        }


        public Joint(excel.Workbook wb, int stdreadrow, int stdwriterow, string stdProcessingtype, string stdprocessingoption)
        {
            InitializeComponent();
            workbook = wb;
            readrow = stdreadrow;
            writerow = stdwriterow;
            Processingtype = stdProcessingtype;
            processingoption = stdprocessingoption;


            this.Sourcevariable1.Combo_OriginItem_Item.PreviewKeyDown += Combo_OriginItem_Item_PreviewKeyDown;
            this.Sourcevariable2.Combo_OriginItem_Item.PreviewKeyDown += Combo_OriginItem_Item_PreviewKeyDown;
            this.Sourcevariable3.Combo_OriginItem_Item.PreviewKeyDown += Combo_OriginItem_Item_PreviewKeyDown;
            this.Sourcevariable4.Combo_OriginItem_Item.PreviewKeyDown += Combo_OriginItem_Item_PreviewKeyDown;
            this.Sourcevariable5.Combo_OriginItem_Item.PreviewKeyDown += Combo_OriginItem_Item5_PreviewKeyDown;

            this.Sourcevariable1.Combo_OriginItem_Item.PreviewKeyDown += Combo_OriginItem_Item_PreviewKeyUp;
            this.Sourcevariable2.Combo_OriginItem_Item.PreviewKeyDown += Combo_OriginItem_Item_PreviewKeyUp;
            this.Sourcevariable3.Combo_OriginItem_Item.PreviewKeyDown += Combo_OriginItem_Item_PreviewKeyUp;
            this.Sourcevariable4.Combo_OriginItem_Item.PreviewKeyDown += Combo_OriginItem_Item_PreviewKeyUp;
            this.Sourcevariable5.Combo_OriginItem_Item.PreviewKeyDown += Combo_OriginItem_Item_PreviewKeyUp;


            this.Sourcevariable1.Combo_OriginItem_Item.SelectionChanged -= OnSelectionChanged1;
            this.Sourcevariable2.Combo_OriginItem_Item.SelectionChanged -= OnSelectionChanged2;
            this.Sourcevariable3.Combo_OriginItem_Item.SelectionChanged -= OnSelectionChanged3;
            this.Sourcevariable4.Combo_OriginItem_Item.SelectionChanged -= OnSelectionChanged4;
            this.Sourcevariable5.Combo_OriginItem_Item.SelectionChanged -= OnSelectionChanged5;

            this.Sourcevariable1.Combo_OriginItem_Item.DropDownClosed -= OnSelectionChanged1;
            this.Sourcevariable2.Combo_OriginItem_Item.DropDownClosed -= OnSelectionChanged2;
            this.Sourcevariable3.Combo_OriginItem_Item.DropDownClosed -= OnSelectionChanged3;
            this.Sourcevariable4.Combo_OriginItem_Item.DropDownClosed -= OnSelectionChanged4;
            this.Sourcevariable5.Combo_OriginItem_Item.DropDownClosed -= OnSelectionChanged5;

            this.Sourcevariable1.Combo_OriginItem_Min.SelectionChanged -= MinMaxOnSelectionChanged;
            this.Sourcevariable2.Combo_OriginItem_Min.SelectionChanged -= MinMaxOnSelectionChanged;
            this.Sourcevariable3.Combo_OriginItem_Min.SelectionChanged -= MinMaxOnSelectionChanged;
            this.Sourcevariable4.Combo_OriginItem_Min.SelectionChanged -= MinMaxOnSelectionChanged;
            this.Sourcevariable5.Combo_OriginItem_Min.SelectionChanged -= MinMaxOnSelectionChanged;

            this.Sourcevariable1.Combo_OriginItem_Max.SelectionChanged -= MinMaxOnSelectionChanged;
            this.Sourcevariable2.Combo_OriginItem_Max.SelectionChanged -= MinMaxOnSelectionChanged;
            this.Sourcevariable3.Combo_OriginItem_Max.SelectionChanged -= MinMaxOnSelectionChanged;
            this.Sourcevariable4.Combo_OriginItem_Max.SelectionChanged -= MinMaxOnSelectionChanged;
            this.Sourcevariable5.Combo_OriginItem_Max.SelectionChanged -= MinMaxOnSelectionChanged;
        }

        private void DisableRightMenu(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void Combo_OriginItem_Min1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                this.Sourcevariable1.Combo_OriginItem_Max.Focus();
            }
        }
        private void Combo_OriginItem_Min2_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                this.Sourcevariable2.Combo_OriginItem_Max.Focus();
            }
        }
        private void Combo_OriginItem_Min3_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                this.Sourcevariable3.Combo_OriginItem_Max.Focus();
            }
        }
        private void Combo_OriginItem_Min4_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                this.Sourcevariable4.Combo_OriginItem_Max.Focus();
            }
        }
        private void Combo_OriginItem_Min5_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                this.Sourcevariable5.Combo_OriginItem_Max.Focus();
            }
        }
        private void Combo_OriginItem_Max1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                this.Sourcevariable2.Combo_OriginItem_Item.Focus();
            }
        }
        private void Combo_OriginItem_Max2_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                this.Sourcevariable3.Combo_OriginItem_Item.Focus();
            }
        }
        private void Combo_OriginItem_Max3_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                this.Sourcevariable4.Combo_OriginItem_Item.Focus();
            }
        }
        private void Combo_OriginItem_Max4_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                this.Sourcevariable5.Combo_OriginItem_Item.Focus();
            }
        }
        private void Combo_OriginItem_Max5_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                this.Combo_NewItem_Item.Focus();
            }
        }
        private void FocusNextControlForGrid(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                //TODO
                if (Sourcevariable1.Combo_OriginItem_Item.IsFocused)
                {
                    if (Sourcevariable1.Combo_OriginItem_Min.IsEnabled == true)
                    {
                        Sourcevariable1.Combo_OriginItem_Min.Focus();
                    }
                    else
                    {
                        Sourcevariable2.Combo_OriginItem_Item.Focus();
                    }
                }
                else if (Sourcevariable1.Combo_OriginItem_Max.IsFocused)
                {
                    Sourcevariable2.Combo_OriginItem_Item.Focus();
                }
                else if (Sourcevariable1.Combo_OriginItem_Min.IsFocused)
                {
                    Sourcevariable1.Combo_OriginItem_Max.Focus();
                }
                else if (Sourcevariable2.Combo_OriginItem_Item.IsFocused)
                {
                    if (Sourcevariable2.Combo_OriginItem_Min.IsEnabled == true)
                    {
                        Sourcevariable2.Combo_OriginItem_Min.Focus();
                    }
                    else
                    {
                        Sourcevariable3.Combo_OriginItem_Item.Focus();
                    }
                }
                else if (Sourcevariable2.Combo_OriginItem_Min.IsFocused)
                {
                    Sourcevariable2.Combo_OriginItem_Max.Focus();
                }
                else if (Sourcevariable2.Combo_OriginItem_Max.IsFocused)
                {
                    Sourcevariable3.Combo_OriginItem_Item.Focus();
                }

                else if (Sourcevariable3.Combo_OriginItem_Item.IsFocused)
                {
                    if (Sourcevariable3.Combo_OriginItem_Min.IsEnabled == true)
                    {
                        Sourcevariable3.Combo_OriginItem_Min.Focus();
                    }
                    else
                    {
                        Sourcevariable4.Combo_OriginItem_Item.Focus();
                    }
                }
                else if (Sourcevariable3.Combo_OriginItem_Min.IsFocused)
                {
                    Sourcevariable3.Combo_OriginItem_Max.Focus();
                }
                else if (Sourcevariable3.Combo_OriginItem_Max.IsFocused)
                {
                    Sourcevariable4.Combo_OriginItem_Item.Focus();
                }

                else if (Sourcevariable4.Combo_OriginItem_Item.IsFocused)
                {
                    if (Sourcevariable4.Combo_OriginItem_Min.IsEnabled == true)
                    {
                        Sourcevariable4.Combo_OriginItem_Min.Focus();
                    }
                    else
                    {
                        Sourcevariable5.Combo_OriginItem_Item.Focus();
                    }
                }
                else if (Sourcevariable4.Combo_OriginItem_Min.IsFocused)
                {
                    Sourcevariable4.Combo_OriginItem_Max.Focus();
                }
                else if (Sourcevariable4.Combo_OriginItem_Max.IsFocused)
                {
                    Sourcevariable5.Combo_OriginItem_Item.Focus();
                }

                else if (Sourcevariable5.Combo_OriginItem_Item.IsFocused)
                {
                    if (Sourcevariable5.Combo_OriginItem_Min.IsEnabled == true)
                    {
                        Sourcevariable5.Combo_OriginItem_Min.Focus();
                    }
                    else
                    {
                        Combo_NewItem_Item.Focus();
                    }
                }
                else if (Sourcevariable5.Combo_OriginItem_Min.IsFocused)
                {
                    Sourcevariable5.Combo_OriginItem_Max.Focus();
                }
                else if (Sourcevariable5.Combo_OriginItem_Max.IsFocused)
                {
                    Combo_NewItem_Item.Focus();
                }
            }
        }
        private void TAbStop()
        {
            this.Sourcevariable1.Combo_OriginItem_Item.IsTabStop = true;
            this.Sourcevariable2.Combo_OriginItem_Item.IsTabStop = true;
            this.Sourcevariable3.Combo_OriginItem_Item.IsTabStop = true;
            this.Sourcevariable4.Combo_OriginItem_Item.IsTabStop = true;
            this.Sourcevariable5.Combo_OriginItem_Item.IsTabStop = true;

            this.Sourcevariable1.Combo_OriginItem_Min.IsTabStop = true;
            this.Sourcevariable2.Combo_OriginItem_Min.IsTabStop = true;
            this.Sourcevariable3.Combo_OriginItem_Min.IsTabStop = true;
            this.Sourcevariable4.Combo_OriginItem_Min.IsTabStop = true;
            this.Sourcevariable5.Combo_OriginItem_Min.IsTabStop = true;

            this.Sourcevariable1.Combo_OriginItem_Max.IsTabStop = true;
            this.Sourcevariable2.Combo_OriginItem_Max.IsTabStop = true;
            this.Sourcevariable3.Combo_OriginItem_Max.IsTabStop = true;
            this.Sourcevariable4.Combo_OriginItem_Max.IsTabStop = true;
            this.Sourcevariable5.Combo_OriginItem_Max.IsTabStop = true;

            this.Sourcevariable1.Combo_OriginItem_Item.TabIndex = 1;
            this.Sourcevariable2.Combo_OriginItem_Item.TabIndex = 4;
            this.Sourcevariable3.Combo_OriginItem_Item.TabIndex = 7;
            this.Sourcevariable4.Combo_OriginItem_Item.TabIndex = 10;
            this.Sourcevariable5.Combo_OriginItem_Item.TabIndex = 13;

            this.Sourcevariable1.Combo_OriginItem_Min.TabIndex = 2;
            this.Sourcevariable2.Combo_OriginItem_Min.TabIndex = 5;
            this.Sourcevariable3.Combo_OriginItem_Min.TabIndex = 8;
            this.Sourcevariable4.Combo_OriginItem_Min.TabIndex = 11;
            this.Sourcevariable5.Combo_OriginItem_Min.TabIndex = 14;

            this.Sourcevariable1.Combo_OriginItem_Max.TabIndex = 3;
            this.Sourcevariable2.Combo_OriginItem_Max.TabIndex = 6;
            this.Sourcevariable3.Combo_OriginItem_Max.TabIndex = 9;
            this.Sourcevariable4.Combo_OriginItem_Max.TabIndex = 12;
            this.Sourcevariable5.Combo_OriginItem_Max.TabIndex = 15;

            try
            {
                this.Combo_Process.IsTabStop = false;
                this.Text_ProcessPart.IsTabStop = false;
                this.Sourcevariable1.List_OriginItem_ChoiceList.IsTabStop = false;
                this.Sourcevariable2.List_OriginItem_ChoiceList.IsTabStop = false;
                this.Sourcevariable3.List_OriginItem_ChoiceList.IsTabStop = false;
                this.Sourcevariable4.List_OriginItem_ChoiceList.IsTabStop = false;
                this.Sourcevariable5.List_OriginItem_ChoiceList.IsTabStop = false;
            }
            catch { }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                TAbStop();

                NewItemSearchbutton.IsEnabled = false;
                NewItemSearchbutton.Opacity = 0.5;
                Command_Entry.IsEnabled = false;

                Style style = Application.Current.FindResource("NormalTextBoxMultiLine_READONLY") as Style;
                Combo_NewItem_Item.Style = style;
                Combo_NewItem_Item.IsEnabled = false;

                Combo_NewItem_AnswerType.Text = Util.Constants.AnswerType.MA;

                string[] choicesList = frmutil.LoadComboWithChoices(Qc4Launcher.Util.Constants.MaxChoiceCount);
                Combo_NewItem_SelectCount.ItemsSource = choicesList;
                Combo_NewItem_SelectCount.SelectedIndex = 0;

                PopulateSAMAVariableList();


                this.Sourcevariable1.Combo_OriginItem_Item.DataContext = SourceVariableListView1;
                this.Sourcevariable2.Combo_OriginItem_Item.DataContext = SourceVariableListView2;
                this.Sourcevariable3.Combo_OriginItem_Item.DataContext = SourceVariableListView3;
                this.Sourcevariable4.Combo_OriginItem_Item.DataContext = SourceVariableListView4;
                this.Sourcevariable5.Combo_OriginItem_Item.DataContext = SourceVariableListView5;

                LoadGridValues(Qc4Launcher.Util.Constants.MaxChoiceCount, null);
                if (processingoption == QC4Common.Common.Constants.STD_DP.Process_Edit || processingoption == QC4Common.Common.Constants.STD_DP.Process_Copy)
                {


                    if (processingoption == QC4Common.Common.Constants.STD_DP.Process_Edit)
                    {
                        Color Combo_ProcessTextboxColor = (Color)ColorConverter.ConvertFromString(QC4Common.Common.Constants.STD_DP.ProcessTextboxColor);
                        Combo_Process.Background = new System.Windows.Media.SolidColorBrush(Combo_ProcessTextboxColor);
                        Color Combo_ProcessTextboxfroreColor = (Color)ColorConverter.ConvertFromString(QC4Common.Common.Constants.STD_DP.ProcessTextboxforeColor);
                        Combo_Process.Foreground = new System.Windows.Media.SolidColorBrush(Combo_ProcessTextboxfroreColor);
                    }

                    //todo for loading
                    iseditorcopy = true;
                    string[,] objarray = dphelper.GetRangevalues(readrow, writerow, workbook);
                    SetValuesToControls(objarray);






                }



                this.Sourcevariable1.Combo_OriginItem_Min.SelectionChanged += new SelectionChangedEventHandler(MinMaxOnSelectionChanged);
                this.Sourcevariable2.Combo_OriginItem_Min.SelectionChanged += new SelectionChangedEventHandler(MinMaxOnSelectionChanged);
                this.Sourcevariable3.Combo_OriginItem_Min.SelectionChanged += new SelectionChangedEventHandler(MinMaxOnSelectionChanged);
                this.Sourcevariable4.Combo_OriginItem_Min.SelectionChanged += new SelectionChangedEventHandler(MinMaxOnSelectionChanged);
                this.Sourcevariable5.Combo_OriginItem_Min.SelectionChanged += new SelectionChangedEventHandler(MinMaxOnSelectionChanged);

                this.Sourcevariable1.Combo_OriginItem_Max.SelectionChanged += new SelectionChangedEventHandler(MinMaxOnSelectionChanged);
                this.Sourcevariable2.Combo_OriginItem_Max.SelectionChanged += new SelectionChangedEventHandler(MinMaxOnSelectionChanged);
                this.Sourcevariable3.Combo_OriginItem_Max.SelectionChanged += new SelectionChangedEventHandler(MinMaxOnSelectionChanged);
                this.Sourcevariable4.Combo_OriginItem_Max.SelectionChanged += new SelectionChangedEventHandler(MinMaxOnSelectionChanged);
                this.Sourcevariable5.Combo_OriginItem_Max.SelectionChanged += new SelectionChangedEventHandler(MinMaxOnSelectionChanged);
                iseditorcopy = false;
                this.Sourcevariable1.Combo_OriginItem_Item.Focus();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
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
                        newvariable = GetFirstSelectedVariable();// Combo_OriginItem_Item.Text;
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

            //limit
            if (question.Length > QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit)
            {
                question = question.PadRight(QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit).Substring(0, QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit);
                Text_NewItem_Question.Text = question;
                //ERR_MSG_MAX_CHAR_LIMIT_EXCEEDS
                //MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_MAX_CHAR_LIMIT_EXCEEDS, QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit));
                //Text_NewItem_Question.Focus();
                //return;
            }

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
            if ((choicegridcount == -1 && Combo_NewItem_SelectCount.SelectedIndex > 0) || (choicegridcount < Combo_NewItem_SelectCount.SelectedIndex))
            {
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


            lasterrorrow = -1;
            for (int i = 0; i < choicecount; i++)
            {
                if (string.IsNullOrEmpty(Convert.ToString(gridchoice.Rows[i][1])) || string.IsNullOrEmpty(Convert.ToString(gridchoice.Rows[i][1]).TrimStart().TrimEnd()))
                {
                    MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT_, LocalResource.LABEL_CHOICE));
                    //frmutil.SetErrorForGrid(gridnewvariable, i, 1, QC4Common.Common.Constants.STD_DP.Background);
                    gridchoice.Rows[i][2] = true;

                    //DataGridRow row = (DataGridRow)gridnewvariable.ItemContainerGenerator.ContainerFromIndex(i);
                    ////gridnewvariable
                    //DataGridCellsPresenter presenter = frmutil.GetVisualChild<DataGridCellsPresenter>(row);
                    //DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(1);
                    //cell.Background = Brushes.Red;
                    lasterrorrow = i;
                    return;
                }
                else //if (lasterrorrow == i)
                {
                    gridchoice.Rows[i][2] = false;
                    //frmutil.SetErrorForGrid(gridnewvariable, i, 1, QC4Common.Common.Constants.STD_DP.Background, true);
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

            string[,] dpsaveinstructios = new string[1, (QC4Common.Common.Constants.DP.MAX_DP_COLUMN - QC4Common.Common.Constants.DP.OnOffColumn) + 1];
            string[,] jointparams = new string[5, 3];
            jointparams = GetJointParams();
            int columncount = 7 + (5 * 2);
            int arraypos = 0;
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
                        dpsaveinstructios[0, i] = Constants.DP.SubstituteOperatorJOINT;
                        break;
                    default:
                        //variable and values
                        //sourceVariableList1 variables
                        if (i % 2 == 0)
                        {
                            dpsaveinstructios[0, i] = jointparams[arraypos, 0];
                            dpsaveinstructios[0, i + 1] = jointparams[arraypos, 1];
                            arraypos++;
                        }

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
                Text_NewItem_Question.Text = frmutil.Addsinglequete(frmutil.UnEscapeCRLF(MagnifyingGlassButton.VariableList.Question));
            }
        }

        private void Combo_NewItem_SelectCount_PreviewKeyDown(object sender, KeyEventArgs e)
        {

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
                            drchoice["IsBlank"] = false;
                            gridchoice.Rows.Add(drchoice);
                        }
                        catch (Exception ex) { }
                    }
                }
                gridnewvariable.DataContext = gridchoice;
            }
            catch (Exception e) { }
        }

        private void Combo_NewItem_SelectCount_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void Gridnewvariable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DataGrid_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab) { gridnewvariable.ScrollIntoView(gridnewvariable.Items[0]); gridnewvariable.SelectedIndex = 0; }
            var uiElement = e.OriginalSource as UIElement;
            bool _ShiftModifierPressed = (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift));
            if (!_ShiftModifierPressed && e.Key == Key.Enter && uiElement != null)
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

                uiElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Down));
                gridnewvariable.SelectedIndex = gridnewvariable.SelectedIndex + 1;
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

        private void PopulateSAMAVariableList()
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
                                        sourceVariableList2.Add(new SourceVariableList()
                                        {
                                            Variable = frmutil.EscapeCRLF(qs.Variable),
                                            AnswerType = qs.AnswerType,
                                            Question = frmutil.EscapeCRLF(qs.Question),
                                            Tableheading = frmutil.EscapeCRLF(qs.TableHeading),
                                            Choices = qs.Choices
                                        });
                                        sourceVariableList3.Add(new SourceVariableList()
                                        {
                                            Variable = frmutil.EscapeCRLF(qs.Variable),
                                            AnswerType = qs.AnswerType,
                                            Question = frmutil.EscapeCRLF(qs.Question),
                                            Tableheading = frmutil.EscapeCRLF(qs.TableHeading),
                                            Choices = qs.Choices
                                        });
                                        sourceVariableList4.Add(new SourceVariableList()
                                        {
                                            Variable = frmutil.EscapeCRLF(qs.Variable),
                                            AnswerType = qs.AnswerType,
                                            Question = frmutil.EscapeCRLF(qs.Question),
                                            Tableheading = frmutil.EscapeCRLF(qs.TableHeading),
                                            Choices = qs.Choices
                                        });
                                        sourceVariableList5.Add(new SourceVariableList()
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

        #region Comboboxes Event handlers
        System.Windows.Controls.ComboBox combo = null;
        bool FirstFocus = true;
        int LastSelected = -1;
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
                    comboBox.SelectedIndex = -1;
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
                System.Windows.Controls.ComboBox sendercombo = ((System.Windows.Controls.ComboBox)sender);
                Combo_KeyDown(sendercombo, sender, e);
            }
        }
        private void Combo_OriginItem_Item5_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Key.Tab != e.Key && !frmutil.IsSystemKey(e))
            {
                System.Windows.Controls.ComboBox sendercombo = ((System.Windows.Controls.ComboBox)sender);
                Combo_KeyDown(sendercombo, sender, e);
            }
            else if (Key.Tab == e.Key)
            {
                if (((System.Windows.Controls.ComboBox)sender).Name == Sourcevariable5.Combo_OriginItem_Item.Name)
                    e.Handled = true;

                if (Sourcevariable5.Combo_OriginItem_Min.IsEnabled == true)
                {
                    Sourcevariable5.Combo_OriginItem_Min.Focus();
                }
                else if (Combo_NewItem_Item.IsEnabled == true)
                {
                    Combo_NewItem_Item.Focus();
                }
                else
                {
                    Combo_NewItem_SelectCount.Focus();
                }
            }
        }
        private void Combo_OriginItem_Item_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (Key.Tab != e.Key && !frmutil.IsSystemKey(e))
            {
                System.Windows.Controls.ComboBox sendercombo = ((System.Windows.Controls.ComboBox)sender);
                Combo_KeyUp(sendercombo, sender, e);
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
        private void Combo_sourceVariable_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            System.Windows.Controls.ComboBox sendercombo = ((System.Windows.Controls.ComboBox)sender);
            Combo_KeyDown(sendercombo, sender, e);
        }

        private void Newvariable3_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Combo_sourceVariable_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            System.Windows.Controls.ComboBox sendercombo = ((System.Windows.Controls.ComboBox)sender);
            Combo_KeyUp(sendercombo, sender, e);
        }
        #endregion


        private void SetComboValue(object sender, int sourcevariable)
        {
            System.Windows.Controls.ComboBox sourceVariable = ((System.Windows.Controls.ComboBox)sender);
            if (sourceVariable.SelectedItem != null)
            {
                int slno = 0;

                SourceVariableList list = new SourceVariableList();
                switch (sourcevariable)
                {
                    case 1:
                        list = (SourceVariableList)(sourceVariable.SelectedItem);
                        //  Newvariable1.Combo_OriginItem_Item.Text = list.Variable;
                        if (list.Choices != null && Util.Definiotion.VariableDictionary.ContainsKey(list.Variable))
                        {
                            variableselectionchange = true;
                            Sourcevariable1.Text_OriginItem_AnswerType_SelectCount.Text = frmutil.EscapeCRLF(list.AnswerType) + LocalResource.LBL_SEPERATOR_SLASH + (list.Choices.Count).ToString();
                            //Sourcevariable1.Text_OriginItem1_SelectCount.Text = (list.Choices.Count).ToString();
                            Sourcevariable1.Text_OriginItem_Question.Text = frmutil.UnEscapeCRLF(list.Question);
                            Sourcevariable1.List_OriginItem_ChoiceList.Items.Clear();
                            foreach (string choice in list.Choices)
                            {
                                slno++;
                                Sourcevariable1.List_OriginItem_ChoiceList.Items.Add(string.Format("{0}: {1}", slno, frmutil.EscapeCRLF(choice)));
                            }

                            SetResetComboValues(sourcevariable, list.Choices.Count, true);

                            string newvarname = list.Variable;
                            ActiveControlsOnSelectionChange(newvarname);
                        }
                        else
                        {
                            sourceVariable.Text = string.Empty;
                            ClearValues(sourcevariable);
                        }
                        break;
                    case 2:
                        list = (SourceVariableList)(sourceVariable.SelectedItem);
                        //  Newvariable1.Combo_OriginItem_Item.Text = list.Variable;
                        if (list.Choices != null && Util.Definiotion.VariableDictionary.ContainsKey(list.Variable))
                        {
                            variableselectionchange = true;
                            Sourcevariable2.Text_OriginItem_AnswerType_SelectCount.Text = frmutil.EscapeCRLF(list.AnswerType) + LocalResource.LBL_SEPERATOR_SLASH + (list.Choices.Count).ToString();
                            //Sourcevariable1.Text_OriginItem1_SelectCount.Text = (list.Choices.Count).ToString();
                            Sourcevariable2.Text_OriginItem_Question.Text = frmutil.UnEscapeCRLF(list.Question);
                            Sourcevariable2.List_OriginItem_ChoiceList.Items.Clear();
                            foreach (string choice in list.Choices)
                            {
                                slno++;
                                Sourcevariable2.List_OriginItem_ChoiceList.Items.Add(string.Format("{0}: {1}", slno, frmutil.EscapeCRLF(choice)));
                            }

                            SetResetComboValues(sourcevariable, list.Choices.Count, true);

                            string newvarname = list.Variable;
                            ActiveControlsOnSelectionChange(newvarname);
                        }
                        else
                        {
                            sourceVariable.Text = string.Empty;
                            ClearValues(sourcevariable);
                        }
                        break;
                    case 3:
                        list = (SourceVariableList)(sourceVariable.SelectedItem);
                        //  Newvariable1.Combo_OriginItem_Item.Text = list.Variable;
                        if (list.Choices != null && Util.Definiotion.VariableDictionary.ContainsKey(list.Variable))
                        {
                            variableselectionchange = true;
                            Sourcevariable3.Text_OriginItem_AnswerType_SelectCount.Text = frmutil.EscapeCRLF(list.AnswerType) + LocalResource.LBL_SEPERATOR_SLASH + (list.Choices.Count).ToString();
                            //Sourcevariable1.Text_OriginItem1_SelectCount.Text = (list.Choices.Count).ToString();
                            Sourcevariable3.Text_OriginItem_Question.Text = frmutil.UnEscapeCRLF(list.Question);
                            Sourcevariable3.List_OriginItem_ChoiceList.Items.Clear();
                            foreach (string choice in list.Choices)
                            {
                                slno++;
                                Sourcevariable3.List_OriginItem_ChoiceList.Items.Add(string.Format("{0}: {1}", slno, frmutil.EscapeCRLF(choice)));
                            }

                            SetResetComboValues(sourcevariable, list.Choices.Count, true);

                            string newvarname = list.Variable;
                            ActiveControlsOnSelectionChange(newvarname);
                        }
                        else
                        {
                            sourceVariable.Text = string.Empty;
                            ClearValues(sourcevariable);
                        }
                        break;
                    case 4:
                        list = (SourceVariableList)(sourceVariable.SelectedItem);
                        //  Newvariable1.Combo_OriginItem_Item.Text = list.Variable;
                        if (list.Choices != null && Util.Definiotion.VariableDictionary.ContainsKey(list.Variable))
                        {
                            variableselectionchange = true;
                            Sourcevariable4.Text_OriginItem_AnswerType_SelectCount.Text = frmutil.EscapeCRLF(list.AnswerType) + LocalResource.LBL_SEPERATOR_SLASH + (list.Choices.Count).ToString();
                            //Sourcevariable1.Text_OriginItem1_SelectCount.Text = (list.Choices.Count).ToString();
                            Sourcevariable4.Text_OriginItem_Question.Text = frmutil.UnEscapeCRLF(list.Question);
                            Sourcevariable4.List_OriginItem_ChoiceList.Items.Clear();
                            foreach (string choice in list.Choices)
                            {
                                slno++;
                                Sourcevariable4.List_OriginItem_ChoiceList.Items.Add(string.Format("{0}: {1}", slno, frmutil.EscapeCRLF(choice)));
                            }

                            SetResetComboValues(sourcevariable, list.Choices.Count, true);

                            string newvarname = list.Variable;
                            ActiveControlsOnSelectionChange(newvarname);
                        }
                        else
                        {
                            sourceVariable.Text = string.Empty;
                            ClearValues(sourcevariable);
                        }
                        break;
                    case 5:
                        list = (SourceVariableList)(sourceVariable.SelectedItem);
                        //  Newvariable1.Combo_OriginItem_Item.Text = list.Variable;
                        if (list.Choices != null && Util.Definiotion.VariableDictionary.ContainsKey(list.Variable))
                        {
                            variableselectionchange = true;
                            Sourcevariable5.Text_OriginItem_AnswerType_SelectCount.Text = frmutil.EscapeCRLF(list.AnswerType) + LocalResource.LBL_SEPERATOR_SLASH + (list.Choices.Count).ToString();
                            //Sourcevariable1.Text_OriginItem1_SelectCount.Text = (list.Choices.Count).ToString();
                            Sourcevariable5.Text_OriginItem_Question.Text = frmutil.UnEscapeCRLF(list.Question);
                            Sourcevariable5.List_OriginItem_ChoiceList.Items.Clear();
                            foreach (string choice in list.Choices)
                            {
                                slno++;
                                Sourcevariable5.List_OriginItem_ChoiceList.Items.Add(string.Format("{0}: {1}", slno, frmutil.EscapeCRLF(choice)));
                            }

                            SetResetComboValues(sourcevariable, list.Choices.Count, true);

                            string newvarname = list.Variable;
                            ActiveControlsOnSelectionChange(newvarname);
                        }
                        else
                        {
                            sourceVariable.Text = string.Empty;
                            ClearValues(sourcevariable);
                        }
                        break;
                }
            }
            else
            {
                sourceVariable.Text = string.Empty;
                ClearValues(sourcevariable);
            }
            SetVariableNameAndQuestion();
        }

        private void OnSelectionChanged1(object sender, EventArgs e)
        {
            // if (iseditorcopy == false)
            {
                SetComboValue(sender, 1);
            }
        }
        private void OnSelectionChanged2(object sender, EventArgs e)
        {
            // if (iseditorcopy == false)
            {
                SetComboValue(sender, 2);
            }
        }
        private void OnSelectionChanged3(object sender, EventArgs e)
        {
            // if (iseditorcopy == false)
            {
                SetComboValue(sender, 3);
            }
        }
        private void OnSelectionChanged4(object sender, EventArgs e)
        {
            // if (iseditorcopy == false)
            {
                SetComboValue(sender, 4);
            }
        }
        private void OnSelectionChanged5(object sender, EventArgs e)
        {
            // if (iseditorcopy == false)
            {
                SetComboValue(sender, 5);
            }
        }

        private void SetResetComboValues(int sourcevariable, int maxcatcount = 0, bool SetValue = true)
        {

            switch (sourcevariable)
            {
                case 1:
                    this.Sourcevariable1.Combo_OriginItem_Min.Items.Clear();
                    this.Sourcevariable1.Combo_OriginItem_Max.Items.Clear();
                    try
                    {
                        this.Sourcevariable1.Combo_OriginItem_Min.IsEnabled = SetValue;
                    }
                    catch { }
                    try
                    {
                        this.Sourcevariable1.Combo_OriginItem_Max.IsEnabled = SetValue;
                    }
                    catch { }
                    if (SetValue)
                    {
                        Setvalues(this.Sourcevariable1.Combo_OriginItem_Min, maxcatcount);
                        Setvalues(this.Sourcevariable1.Combo_OriginItem_Max, maxcatcount);
                        this.Sourcevariable1.Combo_OriginItem_Min.SelectedItem = 1;
                        this.Sourcevariable1.Combo_OriginItem_Max.SelectedItem = maxcatcount;
                        this.Sourcevariable1.Combo_OriginItem_Min.IsTabStop = true;
                        this.Sourcevariable1.Combo_OriginItem_Max.IsTabStop = true;
                        this.Sourcevariable1.Combo_OriginItem_Min.Focusable = true;
                        this.Sourcevariable1.Combo_OriginItem_Max.Focusable = true;
                    }
                    break;
                case 2:
                    this.Sourcevariable2.Combo_OriginItem_Min.Items.Clear();
                    this.Sourcevariable2.Combo_OriginItem_Max.Items.Clear();
                    try
                    {
                        this.Sourcevariable2.Combo_OriginItem_Min.IsEnabled = SetValue;
                    }
                    catch { }
                    try
                    {
                        this.Sourcevariable2.Combo_OriginItem_Max.IsEnabled = SetValue;
                    }
                    catch { }
                    if (SetValue)
                    {
                        Setvalues(this.Sourcevariable2.Combo_OriginItem_Min, maxcatcount);
                        Setvalues(this.Sourcevariable2.Combo_OriginItem_Max, maxcatcount);
                        this.Sourcevariable2.Combo_OriginItem_Min.SelectedItem = 1;
                        this.Sourcevariable2.Combo_OriginItem_Max.SelectedItem = maxcatcount;

                    }
                    break;
                case 3:
                    this.Sourcevariable3.Combo_OriginItem_Min.Items.Clear();
                    this.Sourcevariable3.Combo_OriginItem_Max.Items.Clear();
                    try
                    {
                        this.Sourcevariable3.Combo_OriginItem_Min.IsEnabled = SetValue;
                    }
                    catch { }
                    try
                    {
                        this.Sourcevariable3.Combo_OriginItem_Max.IsEnabled = SetValue;
                    }
                    catch { }
                    if (SetValue)
                    {
                        Setvalues(this.Sourcevariable3.Combo_OriginItem_Min, maxcatcount);
                        Setvalues(this.Sourcevariable3.Combo_OriginItem_Max, maxcatcount);
                        this.Sourcevariable3.Combo_OriginItem_Min.SelectedItem = 1;
                        this.Sourcevariable3.Combo_OriginItem_Max.SelectedItem = maxcatcount;

                    }
                    break;
                case 4:
                    this.Sourcevariable4.Combo_OriginItem_Min.Items.Clear();
                    this.Sourcevariable4.Combo_OriginItem_Max.Items.Clear();
                    try
                    {
                        this.Sourcevariable4.Combo_OriginItem_Min.IsEnabled = SetValue;
                    }
                    catch { }
                    try
                    {
                        this.Sourcevariable4.Combo_OriginItem_Max.IsEnabled = SetValue;
                    }
                    catch { }
                    if (SetValue)
                    {
                        Setvalues(this.Sourcevariable4.Combo_OriginItem_Min, maxcatcount);
                        Setvalues(this.Sourcevariable4.Combo_OriginItem_Max, maxcatcount);
                        this.Sourcevariable4.Combo_OriginItem_Min.SelectedItem = 1;
                        this.Sourcevariable4.Combo_OriginItem_Max.SelectedItem = maxcatcount;

                    }
                    break;
                case 5:
                    this.Sourcevariable5.Combo_OriginItem_Min.Items.Clear();
                    this.Sourcevariable5.Combo_OriginItem_Max.Items.Clear();
                    try
                    {
                        this.Sourcevariable5.Combo_OriginItem_Min.IsEnabled = SetValue;
                    }
                    catch { }
                    try
                    {
                        this.Sourcevariable5.Combo_OriginItem_Max.IsEnabled = SetValue;
                    }
                    catch { }
                    if (SetValue)
                    {
                        Setvalues(this.Sourcevariable5.Combo_OriginItem_Min, maxcatcount);
                        Setvalues(this.Sourcevariable5.Combo_OriginItem_Max, maxcatcount);
                        this.Sourcevariable5.Combo_OriginItem_Min.SelectedItem = 1;
                        this.Sourcevariable5.Combo_OriginItem_Max.SelectedItem = maxcatcount;

                    }
                    break;

            }
        }
        private ComboBox Setvalues(ComboBox cmb, int maxcatcount)
        {
            for (int i = 1; i <= maxcatcount; i++)
            {
                cmb.Items.Add(i);
            }
            return cmb;
        }
        private void ResetOnEmpty(int sourcevariable)
        {
            switch (sourcevariable)
            {
                case 1:
                    Sourcevariable1.Text_OriginItem_AnswerType_SelectCount.Text = string.Empty;
                    Sourcevariable1.Text_OriginItem_Question.Text = string.Empty;
                    Sourcevariable1.List_OriginItem_ChoiceList.Items.Clear();
                    break;
                case 2:
                    Sourcevariable2.Text_OriginItem_AnswerType_SelectCount.Text = string.Empty;
                    Sourcevariable2.Text_OriginItem_Question.Text = string.Empty;
                    Sourcevariable2.List_OriginItem_ChoiceList.Items.Clear();
                    break;
                case 3:
                    Sourcevariable3.Text_OriginItem_AnswerType_SelectCount.Text = string.Empty;
                    Sourcevariable3.Text_OriginItem_Question.Text = string.Empty;
                    Sourcevariable3.List_OriginItem_ChoiceList.Items.Clear();
                    break;
                case 4:
                    Sourcevariable4.Text_OriginItem_AnswerType_SelectCount.Text = string.Empty;
                    Sourcevariable4.Text_OriginItem_Question.Text = string.Empty;
                    Sourcevariable4.List_OriginItem_ChoiceList.Items.Clear();
                    break;
                case 5:
                    Sourcevariable5.Text_OriginItem_AnswerType_SelectCount.Text = string.Empty;
                    Sourcevariable5.Text_OriginItem_Question.Text = string.Empty;
                    Sourcevariable5.List_OriginItem_ChoiceList.Items.Clear();
                    break;
            }


        }
        private void ClearValues(int sourcevariable)
        {
            ResetOnEmpty(sourcevariable);
            SetResetComboValues(sourcevariable, 0, false);
        }
        private void MinMaxOnSelectionChanged(object sender, EventArgs e)
        {
            if (iseditorcopy == false)
            {
                SetNewVariableChoicesAccordingToSelection();
                variableselectionchange = false;
            }
        }

        private void SetNewVariableChoicesAccordingToSelection()
        {
            int choicecount = 0;
            List<string> choices = new List<string>();
            string sourcevariable1 = Sourcevariable1.Combo_OriginItem_Item.SelectedItem == null ? string.Empty : (((SourceVariableList)(Sourcevariable1.Combo_OriginItem_Item.SelectedItem)).Variable);
            string sourcevariable2 = Sourcevariable2.Combo_OriginItem_Item.SelectedItem == null ? string.Empty : (((SourceVariableList)(Sourcevariable2.Combo_OriginItem_Item.SelectedItem)).Variable);
            string sourcevariable3 = Sourcevariable3.Combo_OriginItem_Item.SelectedItem == null ? string.Empty : (((SourceVariableList)(Sourcevariable3.Combo_OriginItem_Item.SelectedItem)).Variable);
            string sourcevariable4 = Sourcevariable4.Combo_OriginItem_Item.SelectedItem == null ? string.Empty : (((SourceVariableList)(Sourcevariable4.Combo_OriginItem_Item.SelectedItem)).Variable);
            string sourcevariable5 = Sourcevariable5.Combo_OriginItem_Item.SelectedItem == null ? string.Empty : (((SourceVariableList)(Sourcevariable5.Combo_OriginItem_Item.SelectedItem)).Variable);
            try { ClearSourceValues(); } catch { }
            if (!string.IsNullOrEmpty(sourcevariable1) && Util.Definiotion.VariableDictionary.ContainsKey(sourcevariable1) && Sourcevariable1.Combo_OriginItem_Min.IsEnabled == true)
            {
                int min = 1;
                int max = Util.Definiotion.VariableDictionary[sourcevariable1].Choices.Count;
                if (!variableselectionchange)
                {
                    int.TryParse(Convert.ToString(Sourcevariable1.Combo_OriginItem_Min.SelectedItem), out min);
                    int.TryParse(Convert.ToString(Sourcevariable1.Combo_OriginItem_Max.SelectedItem), out max);
                }
                if (min > max)
                {
                    int temp = min;
                    min = max;
                    max = temp;
                }
                choicecount += max - min + 1;
                if (choicecount > Qc4Launcher.Util.Constants.MaxChoiceCount)
                {
                    ClearValues(1);
                    Sourcevariable1.Combo_OriginItem_Item.SelectedIndex = clearselection;
                    Sourcevariable1.Combo_OriginItem_Item.Text = string.Empty;
                }
                if (variableselectionchange && !CheckChoiceLimitExceeds(choicecount))
                {
                    Sourcevariable1.Combo_OriginItem_Item.SelectedIndex = clearselection;
                    Sourcevariable1.Combo_OriginItem_Item.Text = string.Empty;
                    return;
                }
                if (min - 1 < 0)
                {
                    min = 0;
                }
                else { min = min - 1; }
                if (max - 1 < 0)
                {
                    max = 0;
                }
                else { max = max - 1; }
                for (int i = min; i <= max; i++)
                {
                    choices.Add(Util.Definiotion.VariableDictionary[sourcevariable1].Choices[i]);
                }
            }
            if (!string.IsNullOrEmpty(sourcevariable2) && Util.Definiotion.VariableDictionary.ContainsKey(sourcevariable2) && Sourcevariable2.Combo_OriginItem_Min.IsEnabled == true)
            {
                int min = 1;
                int max = Util.Definiotion.VariableDictionary[sourcevariable2].Choices.Count;
                //int.TryParse(Sourcevariable2.Combo_OriginItem_Min.Text, out min);
                //int.TryParse(Sourcevariable2.Combo_OriginItem_Max.Text, out max);
                if (!variableselectionchange)
                {
                    int.TryParse(Convert.ToString(Sourcevariable2.Combo_OriginItem_Min.SelectedItem), out min);
                    int.TryParse(Convert.ToString(Sourcevariable2.Combo_OriginItem_Max.SelectedItem), out max);
                }
                if (min > max)
                {
                    int temp = min;
                    min = max;
                    max = temp;
                }
                choicecount += max - min + 1;

                if (choicecount > Qc4Launcher.Util.Constants.MaxChoiceCount)
                {

                    ClearValues(2);
                    Sourcevariable2.Combo_OriginItem_Item.SelectedIndex = clearselection;
                    Sourcevariable2.Combo_OriginItem_Item.Text = string.Empty;
                }

                if (variableselectionchange && !CheckChoiceLimitExceeds(choicecount))
                {
                    Sourcevariable2.Combo_OriginItem_Item.SelectedIndex = clearselection;
                    Sourcevariable2.Combo_OriginItem_Item.Text = string.Empty;
                    return;
                }
                if (min - 1 < 0)
                {
                    min = 0;
                }
                else { min = min - 1; }
                if (max - 1 < 0)
                {
                    max = 0;
                }
                else { max = max - 1; }
                for (int i = min; i <= max; i++)
                {
                    choices.Add(Util.Definiotion.VariableDictionary[sourcevariable2].Choices[i]);
                }
            }
            if (!string.IsNullOrEmpty(sourcevariable3) && Util.Definiotion.VariableDictionary.ContainsKey(sourcevariable3) && Sourcevariable3.Combo_OriginItem_Min.IsEnabled == true)
            {
                int min = 1;
                int max = Util.Definiotion.VariableDictionary[sourcevariable3].Choices.Count;
                if (!variableselectionchange)
                {
                    int.TryParse(Convert.ToString(Sourcevariable3.Combo_OriginItem_Min.SelectedItem), out min);
                    int.TryParse(Convert.ToString(Sourcevariable3.Combo_OriginItem_Max.SelectedItem), out max);
                }
                if (min > max)
                {
                    int temp = min;
                    min = max;
                    max = temp;
                }
                choicecount += max - min + 1;
                if (choicecount > Qc4Launcher.Util.Constants.MaxChoiceCount)
                {

                    ClearValues(3);
                    Sourcevariable3.Combo_OriginItem_Item.SelectedIndex = clearselection;
                    Sourcevariable3.Combo_OriginItem_Item.Text = string.Empty;
                }
                if (variableselectionchange && !CheckChoiceLimitExceeds(choicecount))
                {
                    Sourcevariable3.Combo_OriginItem_Item.SelectedIndex = clearselection;
                    Sourcevariable3.Combo_OriginItem_Item.Text = string.Empty;
                    return;
                }
                if (min - 1 < 0)
                {
                    min = 0;
                }
                else { min = min - 1; }
                if (max - 1 < 0)
                {
                    max = 0;
                }
                else { max = max - 1; }
                for (int i = min; i <= max; i++)
                {
                    choices.Add(Util.Definiotion.VariableDictionary[sourcevariable3].Choices[i]);
                }
            }
            if (!string.IsNullOrEmpty(sourcevariable4) && Util.Definiotion.VariableDictionary.ContainsKey(sourcevariable4) && Sourcevariable4.Combo_OriginItem_Min.IsEnabled == true)
            {
                int min = 1;
                int max = Util.Definiotion.VariableDictionary[sourcevariable4].Choices.Count;
                if (!variableselectionchange)
                {
                    int.TryParse(Convert.ToString(Sourcevariable4.Combo_OriginItem_Min.SelectedItem), out min);
                    int.TryParse(Convert.ToString(Sourcevariable4.Combo_OriginItem_Max.SelectedItem), out max);
                }
                if (min > max)
                {
                    int temp = min;
                    min = max;
                    max = temp;
                }
                choicecount += max - min + 1;
                if (choicecount > Qc4Launcher.Util.Constants.MaxChoiceCount)
                {

                    ClearValues(4);
                    Sourcevariable4.Combo_OriginItem_Item.SelectedIndex = clearselection;
                    Sourcevariable4.Combo_OriginItem_Item.Text = string.Empty;
                }
                if (variableselectionchange && !CheckChoiceLimitExceeds(choicecount))
                {
                    Sourcevariable4.Combo_OriginItem_Item.SelectedIndex = clearselection;
                    Sourcevariable4.Combo_OriginItem_Item.Text = string.Empty;
                    return;
                }
                if (min - 1 < 0)
                {
                    min = 0;
                }
                else { min = min - 1; }
                if (max - 1 < 0)
                {
                    max = 0;
                }
                else { max = max - 1; }
                for (int i = min; i <= max; i++)
                {
                    choices.Add(Util.Definiotion.VariableDictionary[sourcevariable4].Choices[i]);
                }
            }
            if (!string.IsNullOrEmpty(sourcevariable5) && Util.Definiotion.VariableDictionary.ContainsKey(sourcevariable5) && Sourcevariable5.Combo_OriginItem_Min.IsEnabled == true)
            {
                int min = 1;
                int max = Util.Definiotion.VariableDictionary[sourcevariable5].Choices.Count;
                if (!variableselectionchange)
                {
                    int.TryParse(Convert.ToString(Sourcevariable5.Combo_OriginItem_Min.SelectedItem), out min);
                    int.TryParse(Convert.ToString(Sourcevariable5.Combo_OriginItem_Max.SelectedItem), out max);
                }
                if (min > max)
                {
                    int temp = min;
                    min = max;
                    max = temp;
                }
                choicecount += max - min + 1;
                if (choicecount > Qc4Launcher.Util.Constants.MaxChoiceCount)
                {

                    ClearValues(5);
                    Sourcevariable5.Combo_OriginItem_Item.SelectedIndex = clearselection;
                    Sourcevariable5.Combo_OriginItem_Item.Text = string.Empty;
                }
                if (variableselectionchange && !CheckChoiceLimitExceeds(choicecount))
                {
                    Sourcevariable5.Combo_OriginItem_Item.SelectedIndex = clearselection;
                    Sourcevariable5.Combo_OriginItem_Item.Text = string.Empty;
                    return;
                }
                if (min - 1 < 0)
                {
                    min = 0;
                }
                else { min = min - 1; }
                if (max - 1 < 0)
                {
                    max = 0;
                }
                else { max = max - 1; }
                for (int i = min; i <= max; i++)
                {
                    choices.Add(Util.Definiotion.VariableDictionary[sourcevariable5].Choices[i]);
                }
            }
            SetNewVariableChoice(choicecount, choices);

        }
        private void SetNewVariableChoice(int choicecount, List<string> choices)
        {

            if (choicecount > 0 && choicecount <= Qc4Launcher.Util.Constants.MaxChoiceCount)
            {
                Combo_NewItem_SelectCount.SelectedIndex = choicecount;
                int i = 0;
                foreach (string item in choices)
                {
                    gridchoice.Rows[i]["Choice"] = frmutil.EscapeCRLF(item);
                    i++;
                }
            }
            else if (choicecount == 0)
            {

                Combo_NewItem_SelectCount.SelectedIndex = 0;
                LoadGridValues(Qc4Launcher.Util.Constants.MaxChoiceCount, null);
            }
        }
        private bool CheckChoiceLimitExceeds(int choicecount)
        {
            bool retval = true;
            if (choicecount > Qc4Launcher.Util.Constants.MaxChoiceCount)
            {
                MessageDialog.ErrorOk(string.Format(LocalResource.THE_NO_OF_CHOICE_WILL_BE, choicecount) + "\n" + string.Format(LocalResource.THE_NO_OF_CHOICE_SHOULD_BE_EQUAL_LESS_THAN, Qc4Launcher.Util.Constants.MaxChoiceCount));
                retval = false;
            }
            else { retval = true; }
            return retval;
        }

        private void ActiveControlsOnSelectionChange(string newvarname)
        {


            SetControls(newvarname);

        }
        /// <summary>
        /// Method to genearte Variable name and Question
        /// </summary>
        private void SetVariableNameAndQuestion()
        {
            string newvariablename = string.Empty;
            string question = string.Empty;
            string sourcevariable1 = Sourcevariable1.Combo_OriginItem_Item.SelectedItem == null ? string.Empty : (((SourceVariableList)(Sourcevariable1.Combo_OriginItem_Item.SelectedItem)).Variable);
            string sourcevariable2 = Sourcevariable2.Combo_OriginItem_Item.SelectedItem == null ? string.Empty : (((SourceVariableList)(Sourcevariable2.Combo_OriginItem_Item.SelectedItem)).Variable);
            string sourcevariable3 = Sourcevariable3.Combo_OriginItem_Item.SelectedItem == null ? string.Empty : (((SourceVariableList)(Sourcevariable3.Combo_OriginItem_Item.SelectedItem)).Variable);
            string sourcevariable4 = Sourcevariable4.Combo_OriginItem_Item.SelectedItem == null ? string.Empty : (((SourceVariableList)(Sourcevariable4.Combo_OriginItem_Item.SelectedItem)).Variable);
            string sourcevariable5 = Sourcevariable5.Combo_OriginItem_Item.SelectedItem == null ? string.Empty : (((SourceVariableList)(Sourcevariable5.Combo_OriginItem_Item.SelectedItem)).Variable);
            int pos = 0;

            QC4Common.Util.QSUtil qsutil = new QC4Common.Util.QSUtil();
            if (!string.IsNullOrEmpty(sourcevariable1) && Util.Definiotion.VariableDictionary.ContainsKey(sourcevariable1) && Sourcevariable1.Combo_OriginItem_Min.IsEnabled == true)
            {
                if (string.IsNullOrEmpty(newvariablename))
                {
                    newvariablename = qsutil.GetVariableName(sourcevariable1, PopulatedDictionary.Values.ToList());
                }
                else
                {
                    newvariablename += sourcevariable1;
                }
                question += (string.IsNullOrEmpty(question) ? string.Empty : QC4Common.Common.Constants.STD_DP.Plus) + (Util.Definiotion.VariableDictionary[sourcevariable1].TableHeading + QC4Common.Common.Constants.STD_DP.Space + Util.Definiotion.VariableDictionary[sourcevariable1].Question).TrimEnd().TrimStart();
            }
            if (!string.IsNullOrEmpty(sourcevariable2) && Util.Definiotion.VariableDictionary.ContainsKey(sourcevariable2) && Sourcevariable2.Combo_OriginItem_Min.IsEnabled == true)
            {
                if (string.IsNullOrEmpty(newvariablename))
                {
                    newvariablename = qsutil.GetVariableName(sourcevariable2, PopulatedDictionary.Values.ToList());
                }
                else
                {
                    newvariablename += sourcevariable2;
                    newvariablename = qsutil.GetVariableName(newvariablename, PopulatedDictionary.Values.ToList());
                }
                question += (string.IsNullOrEmpty(question) ? string.Empty : QC4Common.Common.Constants.STD_DP.Plus) + (Util.Definiotion.VariableDictionary[sourcevariable2].TableHeading + QC4Common.Common.Constants.STD_DP.Space + Util.Definiotion.VariableDictionary[sourcevariable2].Question).TrimEnd().TrimStart();
            }
            if (!string.IsNullOrEmpty(sourcevariable3) && Util.Definiotion.VariableDictionary.ContainsKey(sourcevariable3) && Sourcevariable3.Combo_OriginItem_Min.IsEnabled == true)
            {
                if (string.IsNullOrEmpty(newvariablename))
                {
                    newvariablename = qsutil.GetVariableName(sourcevariable3, PopulatedDictionary.Values.ToList());
                }
                else
                {
                    newvariablename += sourcevariable3;
                    newvariablename = qsutil.GetVariableName(newvariablename, PopulatedDictionary.Values.ToList());
                }
                question += (string.IsNullOrEmpty(question) ? string.Empty : QC4Common.Common.Constants.STD_DP.Plus) + (Util.Definiotion.VariableDictionary[sourcevariable3].TableHeading + QC4Common.Common.Constants.STD_DP.Space + Util.Definiotion.VariableDictionary[sourcevariable3].Question).TrimEnd().TrimStart();
            }
            if (!string.IsNullOrEmpty(sourcevariable4) && Util.Definiotion.VariableDictionary.ContainsKey(sourcevariable4) && Sourcevariable4.Combo_OriginItem_Min.IsEnabled == true)
            {
                if (string.IsNullOrEmpty(newvariablename))
                {
                    newvariablename = qsutil.GetVariableName(sourcevariable4, PopulatedDictionary.Values.ToList());
                }
                else
                {
                    newvariablename += sourcevariable4;
                    newvariablename = qsutil.GetVariableName(newvariablename, PopulatedDictionary.Values.ToList());
                }
                question += (string.IsNullOrEmpty(question) ? string.Empty : QC4Common.Common.Constants.STD_DP.Plus) + (Util.Definiotion.VariableDictionary[sourcevariable4].TableHeading + QC4Common.Common.Constants.STD_DP.Space + Util.Definiotion.VariableDictionary[sourcevariable4].Question).TrimEnd().TrimStart();
            }
            if (!string.IsNullOrEmpty(sourcevariable5) && Util.Definiotion.VariableDictionary.ContainsKey(sourcevariable5) && Sourcevariable5.Combo_OriginItem_Min.IsEnabled == true)
            {
                if (string.IsNullOrEmpty(newvariablename))
                {
                    newvariablename = qsutil.GetVariableName(sourcevariable5, PopulatedDictionary.Values.ToList());
                }
                else
                {
                    newvariablename += sourcevariable5;
                    newvariablename = qsutil.GetVariableName(newvariablename, PopulatedDictionary.Values.ToList());
                }
                question += (string.IsNullOrEmpty(question) ? string.Empty : QC4Common.Common.Constants.STD_DP.Plus) + (Util.Definiotion.VariableDictionary[sourcevariable5].TableHeading + QC4Common.Common.Constants.STD_DP.Space + Util.Definiotion.VariableDictionary[sourcevariable5].Question).TrimEnd().TrimStart();
            }
            if (string.IsNullOrEmpty(newvariablename))
            {
                Command_Entry.IsEnabled = false;
            }
            Combo_NewItem_Item.Text = newvariablename;
            Text_NewItem_Question.Text = question;
            Text_NewItem_Question.Focus();
            Text_NewItem_Question.Select(Text_NewItem_Question.Text.Length, 0);
        }
        private void SetControls(string newvarname)
        {
           // Combo_NewItem_Item.Text = newvarname;
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
            Command_Entry.IsEnabled = true;
        }
        private void SetMinMaxGreaterLevel(ref int min, ref int max)
        {

            if (min > max)
            {
                int temp = min;
                min = max;
                max = temp;
            }
        }
        private string[,] GetJointParams()
        {
            string[,] jointparams = new string[5, 3];
            string sourcevariable1 = Sourcevariable1.Combo_OriginItem_Item.SelectedItem == null ? string.Empty : (((SourceVariableList)(Sourcevariable1.Combo_OriginItem_Item.SelectedItem)).Variable);
            string sourcevariable2 = Sourcevariable2.Combo_OriginItem_Item.SelectedItem == null ? string.Empty : (((SourceVariableList)(Sourcevariable2.Combo_OriginItem_Item.SelectedItem)).Variable);
            string sourcevariable3 = Sourcevariable3.Combo_OriginItem_Item.SelectedItem == null ? string.Empty : (((SourceVariableList)(Sourcevariable3.Combo_OriginItem_Item.SelectedItem)).Variable);
            string sourcevariable4 = Sourcevariable4.Combo_OriginItem_Item.SelectedItem == null ? string.Empty : (((SourceVariableList)(Sourcevariable4.Combo_OriginItem_Item.SelectedItem)).Variable);
            string sourcevariable5 = Sourcevariable5.Combo_OriginItem_Item.SelectedItem == null ? string.Empty : (((SourceVariableList)(Sourcevariable5.Combo_OriginItem_Item.SelectedItem)).Variable);
            int pos = 0;
            if (!string.IsNullOrEmpty(sourcevariable1) && Util.Definiotion.VariableDictionary.ContainsKey(sourcevariable1) && Sourcevariable1.Combo_OriginItem_Min.IsEnabled == true)
            {
                int min = 1;
                int max = Util.Definiotion.VariableDictionary[sourcevariable1].Choices.Count;
                int.TryParse(Convert.ToString(Sourcevariable1.Combo_OriginItem_Min.SelectedItem), out min);
                int.TryParse(Convert.ToString(Sourcevariable1.Combo_OriginItem_Max.SelectedItem), out max);
                SetMinMaxGreaterLevel(ref min, ref max);
                jointparams[pos, 0] = sourcevariable1;
                jointparams[pos, 1] = Convert.ToString(min) + LocalResource.GRID_LBL_INTEGRATE_SEPERATOR + Convert.ToString(max);
                pos++;
            }
            if (!string.IsNullOrEmpty(sourcevariable2) && Util.Definiotion.VariableDictionary.ContainsKey(sourcevariable2) && Sourcevariable2.Combo_OriginItem_Min.IsEnabled == true)
            {
                int min = 1;
                int max = Util.Definiotion.VariableDictionary[sourcevariable2].Choices.Count;
                //int.TryParse(Sourcevariable2.Combo_OriginItem_Min.Text, out min);
                //int.TryParse(Sourcevariable2.Combo_OriginItem_Max.Text, out max);
                int.TryParse(Convert.ToString(Sourcevariable2.Combo_OriginItem_Min.SelectedItem), out min);
                int.TryParse(Convert.ToString(Sourcevariable2.Combo_OriginItem_Max.SelectedItem), out max);
                SetMinMaxGreaterLevel(ref min, ref max);
                jointparams[pos, 0] = sourcevariable2;
                jointparams[pos, 1] = Convert.ToString(min) + LocalResource.GRID_LBL_INTEGRATE_SEPERATOR + Convert.ToString(max);
                pos++;

            }
            if (!string.IsNullOrEmpty(sourcevariable3) && Util.Definiotion.VariableDictionary.ContainsKey(sourcevariable3) && Sourcevariable3.Combo_OriginItem_Min.IsEnabled == true)
            {
                int min = 1;
                int max = Util.Definiotion.VariableDictionary[sourcevariable3].Choices.Count;
                int.TryParse(Convert.ToString(Sourcevariable3.Combo_OriginItem_Min.SelectedItem), out min);
                int.TryParse(Convert.ToString(Sourcevariable3.Combo_OriginItem_Max.SelectedItem), out max);
                SetMinMaxGreaterLevel(ref min, ref max);
                jointparams[pos, 0] = sourcevariable3;
                jointparams[pos, 1] = Convert.ToString(min) + LocalResource.GRID_LBL_INTEGRATE_SEPERATOR + Convert.ToString(max);
                pos++;
            }
            if (!string.IsNullOrEmpty(sourcevariable4) && Util.Definiotion.VariableDictionary.ContainsKey(sourcevariable4) && Sourcevariable4.Combo_OriginItem_Min.IsEnabled == true)
            {
                int min = 1;
                int max = Util.Definiotion.VariableDictionary[sourcevariable4].Choices.Count;
                int.TryParse(Convert.ToString(Sourcevariable4.Combo_OriginItem_Min.SelectedItem), out min);
                int.TryParse(Convert.ToString(Sourcevariable4.Combo_OriginItem_Max.SelectedItem), out max);
                SetMinMaxGreaterLevel(ref min, ref max);
                jointparams[pos, 0] = sourcevariable4;
                jointparams[pos, 1] = Convert.ToString(min) + LocalResource.GRID_LBL_INTEGRATE_SEPERATOR + Convert.ToString(max);
                pos++;
            }
            if (!string.IsNullOrEmpty(sourcevariable5) && Util.Definiotion.VariableDictionary.ContainsKey(sourcevariable5) && Sourcevariable5.Combo_OriginItem_Min.IsEnabled == true)
            {
                int min = 1;
                int max = Util.Definiotion.VariableDictionary[sourcevariable5].Choices.Count;
                int.TryParse(Convert.ToString(Sourcevariable5.Combo_OriginItem_Min.SelectedItem), out min);
                int.TryParse(Convert.ToString(Sourcevariable5.Combo_OriginItem_Max.SelectedItem), out max);
                SetMinMaxGreaterLevel(ref min, ref max);
                jointparams[pos, 0] = sourcevariable5;
                jointparams[pos, 1] = Convert.ToString(min) + LocalResource.GRID_LBL_INTEGRATE_SEPERATOR + Convert.ToString(max);
                pos++;
            }
            return jointparams;
        }

        private void ClearSourceValues()
        {
            string sourcevariable1 = Sourcevariable1.Combo_OriginItem_Item.SelectedItem == null ? string.Empty : (((SourceVariableList)(Sourcevariable1.Combo_OriginItem_Item.SelectedItem)).Variable);
            string sourcevariable2 = Sourcevariable2.Combo_OriginItem_Item.SelectedItem == null ? string.Empty : (((SourceVariableList)(Sourcevariable2.Combo_OriginItem_Item.SelectedItem)).Variable);
            string sourcevariable3 = Sourcevariable3.Combo_OriginItem_Item.SelectedItem == null ? string.Empty : (((SourceVariableList)(Sourcevariable3.Combo_OriginItem_Item.SelectedItem)).Variable);
            string sourcevariable4 = Sourcevariable4.Combo_OriginItem_Item.SelectedItem == null ? string.Empty : (((SourceVariableList)(Sourcevariable4.Combo_OriginItem_Item.SelectedItem)).Variable);
            string sourcevariable5 = Sourcevariable5.Combo_OriginItem_Item.SelectedItem == null ? string.Empty : (((SourceVariableList)(Sourcevariable5.Combo_OriginItem_Item.SelectedItem)).Variable);

            if (string.IsNullOrEmpty(sourcevariable1) || !Util.Definiotion.VariableDictionary.ContainsKey(sourcevariable1))
            {
                ClearValues(1);
                Sourcevariable1.Combo_OriginItem_Item.SelectedIndex = clearselection;
                Sourcevariable1.Combo_OriginItem_Item.Text = string.Empty;
            }
            if (string.IsNullOrEmpty(sourcevariable2) || !Util.Definiotion.VariableDictionary.ContainsKey(sourcevariable2))
            {
                ClearValues(2);
                Sourcevariable2.Combo_OriginItem_Item.SelectedIndex = clearselection;
                Sourcevariable2.Combo_OriginItem_Item.Text = string.Empty;
            }
            if (string.IsNullOrEmpty(sourcevariable3) || !Util.Definiotion.VariableDictionary.ContainsKey(sourcevariable3))
            {
                ClearValues(3);
                Sourcevariable3.Combo_OriginItem_Item.SelectedIndex = clearselection;
                Sourcevariable3.Combo_OriginItem_Item.Text = string.Empty;
            }
            if (string.IsNullOrEmpty(sourcevariable4) || !Util.Definiotion.VariableDictionary.ContainsKey(sourcevariable4))
            {
                ClearValues(4);
                Sourcevariable4.Combo_OriginItem_Item.SelectedIndex = clearselection;
                Sourcevariable4.Combo_OriginItem_Item.Text = string.Empty;
            }
            if (string.IsNullOrEmpty(sourcevariable5) || !Util.Definiotion.VariableDictionary.ContainsKey(sourcevariable5))
            {
                ClearValues(5);
                Sourcevariable5.Combo_OriginItem_Item.SelectedIndex = clearselection;
                Sourcevariable5.Combo_OriginItem_Item.Text = string.Empty;
            }
        }
        private void SetValuesToControls(string[,] values)
        {
            NewItemSearchbutton.IsEnabled = true;
            Combo_NewItem_Item.IsEnabled = true;
            Text_NewItem_Question.IsEnabled = true;
            NewItemSearchbutton.Opacity = 1;
            Command_Entry.IsEnabled = true;
            bool sourcenotselected = true;
            SetControls(string.Empty);//variable anem passing
            int choicecount = 0;
            int length = 0;
            int sourcevariablecount = 0;
            string newquestionvariable = string.Empty;
            string question = string.Empty;
            if (Util.Definiotion.VariableDictionary.ContainsKey(Convert.ToString(values[0, 6])))
            {
                newquestionvariable = Convert.ToString(values[0, 6]);
                question = (Util.Definiotion.VariableDictionary[newquestionvariable].Question);
                choicecount = (Util.Definiotion.VariableDictionary[newquestionvariable].CategoryCount);
            }


            for (int i = 0; i < choicecount; i++)
            {
                gridchoice.Rows[i][1] = frmutil.EscapeCRLF((Util.Definiotion.VariableDictionary[newquestionvariable]).Choices[i]);
            }
            //  length = sourcevariablecount + (sourcevariablecount - 1) + choicecount;
            int sourvariablecombo = 0;
            int index = 0;
            string andor = string.Empty;
            int minval = -1;
            int maxval = -1;
            int slno = 0;
            for (int i = QC4Common.Common.Constants.DP.InstructionColumn; i < QC4Common.Common.Constants.DP.MAX_DP_COLUMN - 2; i++)//no need to go till max colum,5 source so 5*2=10 columns
            {
                if (string.IsNullOrEmpty(Convert.ToString(values[0, i])))
                {
                    break;
                }
                if (i % 2 == 0)
                {
                    string sourcevariablename = Convert.ToString(values[0, i]);
                    if (!string.IsNullOrEmpty(sourcevariablename) && (Util.Definiotion.VariableDictionary.ContainsKey(sourcevariablename)))
                    {
                        string rangevalue = Convert.ToString(values[0, i + 1]);
                        sourcenotselected = false;
                        switch (sourvariablecombo)
                        {
                            case 0:
                                // svlist = SourceVariableListView1.Where(z => z.Variable == sourcevariablename).FirstOrDefault();
                                //index = SourceVariableListView1.IndexOf(svlist);
                                index = GetIndexByVariableName(sourcevariablename, SourceVariableListView1);
                                Sourcevariable1.Combo_OriginItem_Item.SelectedIndex = index;// GetIndexByVariableName(sourcevariablename, SourceVariableListView1);
                                                                                            // Newvariable1.Combo_OriginItem_Item.SelectedItem = svlist;
                                                                                            // SetComboValue(Newvariable1.Combo_OriginItem_Item, 1);

                                Sourcevariable1.Text_OriginItem_AnswerType_SelectCount.Text = frmutil.EscapeCRLF(Util.Definiotion.VariableDictionary[sourcevariablename].AnswerType) + LocalResource.LBL_SEPERATOR_SLASH + (Util.Definiotion.VariableDictionary[sourcevariablename].Choices.Count).ToString();
                                Sourcevariable1.Text_OriginItem_Question.Text = frmutil.UnEscapeCRLF(Util.Definiotion.VariableDictionary[sourcevariablename].Question);
                                Sourcevariable1.List_OriginItem_ChoiceList.Items.Clear();
                                slno = 0;
                                foreach (string choice in Util.Definiotion.VariableDictionary[sourcevariablename].Choices)
                                {
                                    slno++;
                                    Sourcevariable1.List_OriginItem_ChoiceList.Items.Add(string.Format("{0}: {1}", slno, frmutil.EscapeCRLF(choice)));
                                }

                                if (!string.IsNullOrEmpty(rangevalue))
                                {
                                    string[] splitvalues = rangevalue.Split(Convert.ToChar(LocalResource.GRID_LBL_INTEGRATE_SEPERATOR));
                                    SetResetComboValues(sourvariablecombo + 1, Util.Definiotion.VariableDictionary[sourcevariablename].Choices.Count, true);
                                    try
                                    {
                                        int.TryParse(Convert.ToString(splitvalues[0]), out minval);
                                    }
                                    catch
                                    { minval = 1; }
                                    try
                                    {
                                        int.TryParse(Convert.ToString(splitvalues[1]), out maxval);
                                    }
                                    catch
                                    { maxval = Util.Definiotion.VariableDictionary[sourcevariablename].Choices.Count; }

                                    Sourcevariable1.Combo_OriginItem_Min.SelectedIndex = minval - 1; ;
                                    Sourcevariable1.Combo_OriginItem_Max.SelectedIndex = maxval - 1;
                                }
                                break;
                            case 1:
                                index = GetIndexByVariableName(sourcevariablename, SourceVariableListView2);
                                index = (sourvariablecombo == sourcevariablecount - 1 || sourvariablecombo == sourcevariablecount - 2) ? index : index;
                                Sourcevariable2.Combo_OriginItem_Item.SelectedIndex = index;

                                //string rangevalue = Convert.ToString(values[0, i + 1]);
                                Sourcevariable2.Text_OriginItem_AnswerType_SelectCount.Text = frmutil.EscapeCRLF(Util.Definiotion.VariableDictionary[sourcevariablename].AnswerType) + LocalResource.LBL_SEPERATOR_SLASH + (Util.Definiotion.VariableDictionary[sourcevariablename].Choices.Count).ToString();
                                Sourcevariable2.Text_OriginItem_Question.Text = frmutil.UnEscapeCRLF(Util.Definiotion.VariableDictionary[sourcevariablename].Question);
                                Sourcevariable2.List_OriginItem_ChoiceList.Items.Clear();
                                slno = 0;
                                foreach (string choice in Util.Definiotion.VariableDictionary[sourcevariablename].Choices)
                                {
                                    slno++;
                                    Sourcevariable2.List_OriginItem_ChoiceList.Items.Add(string.Format("{0}: {1}", slno, frmutil.EscapeCRLF(choice)));
                                }
                                if (!string.IsNullOrEmpty(rangevalue))
                                {
                                    string[] splitvalues = rangevalue.Split(Convert.ToChar(LocalResource.GRID_LBL_INTEGRATE_SEPERATOR));
                                    SetResetComboValues(sourvariablecombo + 1, Util.Definiotion.VariableDictionary[sourcevariablename].Choices.Count, true);
                                    try
                                    {
                                        int.TryParse(Convert.ToString(splitvalues[0]), out minval);
                                    }
                                    catch
                                    { minval = 1; }
                                    try
                                    {
                                        int.TryParse(Convert.ToString(splitvalues[1]), out maxval);
                                    }
                                    catch
                                    { maxval = Util.Definiotion.VariableDictionary[sourcevariablename].Choices.Count; }

                                    Sourcevariable2.Combo_OriginItem_Min.SelectedIndex = minval - 1; ;
                                    Sourcevariable2.Combo_OriginItem_Max.SelectedIndex = maxval - 1;
                                }
                                break;
                            case 2:
                                index = GetIndexByVariableName(sourcevariablename, SourceVariableListView3);
                                index = (sourvariablecombo == sourcevariablecount - 1 || sourvariablecombo == sourcevariablecount - 2) ? index : index;
                                Sourcevariable3.Combo_OriginItem_Item.SelectedIndex = index;

                                Sourcevariable3.Text_OriginItem_AnswerType_SelectCount.Text = frmutil.EscapeCRLF(Util.Definiotion.VariableDictionary[sourcevariablename].AnswerType) + LocalResource.LBL_SEPERATOR_SLASH + (Util.Definiotion.VariableDictionary[sourcevariablename].Choices.Count).ToString();
                                Sourcevariable3.Text_OriginItem_Question.Text = frmutil.UnEscapeCRLF(Util.Definiotion.VariableDictionary[sourcevariablename].Question);
                                Sourcevariable3.List_OriginItem_ChoiceList.Items.Clear();
                                slno = 0;
                                foreach (string choice in Util.Definiotion.VariableDictionary[sourcevariablename].Choices)
                                {
                                    slno++;
                                    Sourcevariable3.List_OriginItem_ChoiceList.Items.Add(string.Format("{0}: {1}", slno, frmutil.EscapeCRLF(choice)));
                                }
                                if (!string.IsNullOrEmpty(rangevalue))
                                {
                                    string[] splitvalues = rangevalue.Split(Convert.ToChar(LocalResource.GRID_LBL_INTEGRATE_SEPERATOR));
                                    SetResetComboValues(sourvariablecombo + 1, Util.Definiotion.VariableDictionary[sourcevariablename].Choices.Count, true);
                                    try
                                    {
                                        int.TryParse(Convert.ToString(splitvalues[0]), out minval);
                                    }
                                    catch
                                    { minval = 1; }
                                    try
                                    {
                                        int.TryParse(Convert.ToString(splitvalues[1]), out maxval);
                                    }
                                    catch
                                    { maxval = Util.Definiotion.VariableDictionary[sourcevariablename].Choices.Count; }

                                    Sourcevariable3.Combo_OriginItem_Min.SelectedIndex = minval - 1; ;
                                    Sourcevariable3.Combo_OriginItem_Max.SelectedIndex = maxval - 1;
                                }
                                break;
                            case 3:
                                index = GetIndexByVariableName(sourcevariablename, SourceVariableListView4);
                                index = (sourvariablecombo == sourcevariablecount - 1 || sourvariablecombo == sourcevariablecount - 2) ? index : index;
                                Sourcevariable4.Combo_OriginItem_Item.SelectedIndex = index;
                                Sourcevariable4.Text_OriginItem_AnswerType_SelectCount.Text = frmutil.EscapeCRLF(Util.Definiotion.VariableDictionary[sourcevariablename].AnswerType) + LocalResource.LBL_SEPERATOR_SLASH + (Util.Definiotion.VariableDictionary[sourcevariablename].Choices.Count).ToString();
                                Sourcevariable4.Text_OriginItem_Question.Text = frmutil.UnEscapeCRLF(Util.Definiotion.VariableDictionary[sourcevariablename].Question);
                                Sourcevariable4.List_OriginItem_ChoiceList.Items.Clear();
                                slno = 0;
                                foreach (string choice in Util.Definiotion.VariableDictionary[sourcevariablename].Choices)
                                {
                                    slno++;
                                    Sourcevariable4.List_OriginItem_ChoiceList.Items.Add(string.Format("{0}: {1}", slno, frmutil.EscapeCRLF(choice)));
                                }
                                if (!string.IsNullOrEmpty(rangevalue))
                                {
                                    string[] splitvalues = rangevalue.Split(Convert.ToChar(LocalResource.GRID_LBL_INTEGRATE_SEPERATOR));
                                    SetResetComboValues(sourvariablecombo + 1, Util.Definiotion.VariableDictionary[sourcevariablename].Choices.Count, true);
                                    try
                                    {
                                        int.TryParse(Convert.ToString(splitvalues[0]), out minval);
                                    }
                                    catch
                                    { minval = 1; }
                                    try
                                    {
                                        int.TryParse(Convert.ToString(splitvalues[1]), out maxval);
                                    }
                                    catch
                                    { maxval = Util.Definiotion.VariableDictionary[sourcevariablename].Choices.Count; }

                                    Sourcevariable4.Combo_OriginItem_Min.SelectedIndex = minval - 1; ;
                                    Sourcevariable4.Combo_OriginItem_Max.SelectedIndex = maxval - 1;
                                }
                                break;
                            case 4:
                                index = GetIndexByVariableName(sourcevariablename, SourceVariableListView4);
                                index = (sourvariablecombo == sourcevariablecount - 1 || sourvariablecombo == sourcevariablecount - 2) ? index : index;
                                Sourcevariable5.Combo_OriginItem_Item.SelectedIndex = index;
                                Sourcevariable5.Text_OriginItem_AnswerType_SelectCount.Text = frmutil.EscapeCRLF(Util.Definiotion.VariableDictionary[sourcevariablename].AnswerType) + LocalResource.LBL_SEPERATOR_SLASH + (Util.Definiotion.VariableDictionary[sourcevariablename].Choices.Count).ToString();
                                Sourcevariable5.Text_OriginItem_Question.Text = frmutil.UnEscapeCRLF(Util.Definiotion.VariableDictionary[sourcevariablename].Question);
                                Sourcevariable5.List_OriginItem_ChoiceList.Items.Clear();
                                slno = 0;
                                foreach (string choice in Util.Definiotion.VariableDictionary[sourcevariablename].Choices)
                                {
                                    slno++;
                                    Sourcevariable5.List_OriginItem_ChoiceList.Items.Add(string.Format("{0}: {1}", slno, frmutil.EscapeCRLF(choice)));
                                }
                                if (!string.IsNullOrEmpty(rangevalue))
                                {
                                    string[] splitvalues = rangevalue.Split(Convert.ToChar(LocalResource.GRID_LBL_INTEGRATE_SEPERATOR));
                                    SetResetComboValues(sourvariablecombo + 1, Util.Definiotion.VariableDictionary[sourcevariablename].Choices.Count, true);
                                    try
                                    {
                                        int.TryParse(Convert.ToString(splitvalues[0]), out minval);
                                    }
                                    catch
                                    { minval = 1; }
                                    try
                                    {
                                        int.TryParse(Convert.ToString(splitvalues[1]), out maxval);
                                    }
                                    catch
                                    { maxval = Util.Definiotion.VariableDictionary[sourcevariablename].Choices.Count; }

                                    Sourcevariable5.Combo_OriginItem_Min.SelectedIndex = minval - 1; ;
                                    Sourcevariable5.Combo_OriginItem_Max.SelectedIndex = maxval - 1;
                                }
                                break;

                        }
                        sourvariablecombo++;
                    }

                }



            }
            //gridoriginalitem.DataContext = gridsourcevariables;
            if (processingoption == QC4Common.Common.Constants.STD_DP.Process_Copy)
            {
                QC4Common.Util.QSUtil qsutil = new QC4Common.Util.QSUtil();
                newquestionvariable = qsutil.GetVariableName(newquestionvariable, PopulatedDictionary.Values.ToList());
            }
            Combo_NewItem_AnswerType.Text = Util.Constants.AnswerType.MA;
            Combo_NewItem_Item.Text = newquestionvariable;
            Text_NewItem_Question.Text = question;
            Combo_NewItem_SelectCount.SelectedIndex = choicecount;
            if (sourcenotselected)
            {
                Command_Entry.IsEnabled = false;
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

        private void Content_Rendered(object sender, EventArgs e)
        {
            this.Sourcevariable1.Combo_OriginItem_Item.SelectionChanged += new SelectionChangedEventHandler(OnSelectionChanged1);
            this.Sourcevariable2.Combo_OriginItem_Item.SelectionChanged += new SelectionChangedEventHandler(OnSelectionChanged2);
            this.Sourcevariable3.Combo_OriginItem_Item.SelectionChanged += new SelectionChangedEventHandler(OnSelectionChanged3);
            this.Sourcevariable4.Combo_OriginItem_Item.SelectionChanged += new SelectionChangedEventHandler(OnSelectionChanged4);
            this.Sourcevariable5.Combo_OriginItem_Item.SelectionChanged += new SelectionChangedEventHandler(OnSelectionChanged5);

            this.Sourcevariable1.Combo_OriginItem_Min.PreviewKeyDown += Combo_OriginItem_Min1_PreviewKeyDown;
            this.Sourcevariable2.Combo_OriginItem_Min.PreviewKeyDown += Combo_OriginItem_Min2_PreviewKeyDown;
            this.Sourcevariable3.Combo_OriginItem_Min.PreviewKeyDown += Combo_OriginItem_Min3_PreviewKeyDown;
            this.Sourcevariable4.Combo_OriginItem_Min.PreviewKeyDown += Combo_OriginItem_Min4_PreviewKeyDown;
            this.Sourcevariable5.Combo_OriginItem_Min.PreviewKeyDown += Combo_OriginItem_Min5_PreviewKeyDown;

            this.Sourcevariable1.Combo_OriginItem_Max.PreviewKeyDown += Combo_OriginItem_Max1_PreviewKeyDown;
            this.Sourcevariable2.Combo_OriginItem_Max.PreviewKeyDown += Combo_OriginItem_Max2_PreviewKeyDown;
            this.Sourcevariable3.Combo_OriginItem_Max.PreviewKeyDown += Combo_OriginItem_Max3_PreviewKeyDown;
            this.Sourcevariable4.Combo_OriginItem_Max.PreviewKeyDown += Combo_OriginItem_Max4_PreviewKeyDown;
            this.Sourcevariable5.Combo_OriginItem_Max.PreviewKeyDown += Combo_OriginItem_Max5_PreviewKeyDown;
        }
        
        private string GetFirstSelectedVariable()
        {
            string newvariablename = string.Empty;
            string question = string.Empty;
            string sourcevariable1 = Sourcevariable1.Combo_OriginItem_Item.SelectedItem == null ? string.Empty : (((SourceVariableList)(Sourcevariable1.Combo_OriginItem_Item.SelectedItem)).Variable);
            string sourcevariable2 = Sourcevariable2.Combo_OriginItem_Item.SelectedItem == null ? string.Empty : (((SourceVariableList)(Sourcevariable2.Combo_OriginItem_Item.SelectedItem)).Variable);
            string sourcevariable3 = Sourcevariable3.Combo_OriginItem_Item.SelectedItem == null ? string.Empty : (((SourceVariableList)(Sourcevariable3.Combo_OriginItem_Item.SelectedItem)).Variable);
            string sourcevariable4 = Sourcevariable4.Combo_OriginItem_Item.SelectedItem == null ? string.Empty : (((SourceVariableList)(Sourcevariable4.Combo_OriginItem_Item.SelectedItem)).Variable);
            string sourcevariable5 = Sourcevariable5.Combo_OriginItem_Item.SelectedItem == null ? string.Empty : (((SourceVariableList)(Sourcevariable5.Combo_OriginItem_Item.SelectedItem)).Variable);

            if (!string.IsNullOrEmpty(sourcevariable1) && Util.Definiotion.VariableDictionary.ContainsKey(sourcevariable1) && Sourcevariable1.Combo_OriginItem_Min.IsEnabled == true)
            {
                return sourcevariable1;
            }
            if (!string.IsNullOrEmpty(sourcevariable2) && Util.Definiotion.VariableDictionary.ContainsKey(sourcevariable2) && Sourcevariable2.Combo_OriginItem_Min.IsEnabled == true)
            {
                return sourcevariable2;
            }
            if (!string.IsNullOrEmpty(sourcevariable3) && Util.Definiotion.VariableDictionary.ContainsKey(sourcevariable3) && Sourcevariable3.Combo_OriginItem_Min.IsEnabled == true)
            {
                return sourcevariable3;
            }
            if (!string.IsNullOrEmpty(sourcevariable4) && Util.Definiotion.VariableDictionary.ContainsKey(sourcevariable4) && Sourcevariable4.Combo_OriginItem_Min.IsEnabled == true)
            {
                return sourcevariable4;
            }
            if (!string.IsNullOrEmpty(sourcevariable5) && Util.Definiotion.VariableDictionary.ContainsKey(sourcevariable5) && Sourcevariable5.Combo_OriginItem_Min.IsEnabled == true)
            {
                return sourcevariable5;
            }
            return string.Empty;
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

        private void Gridnewvariable_CopyingRowClipboardContent(object sender, DataGridRowClipboardEventArgs e)
        {
            try
            {
                e.ClipboardRowContent.RemoveAt(0);
            }
            catch { }
        }

        private void Gridnewvariable_IsMouseCaptureWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (gridnewvariable != null && gridnewvariable.CurrentCell != null && gridnewvariable.CurrentCell.Column != null && gridnewvariable.CurrentCell.Column.DisplayIndex == 1)
            {
                gridchoice.Rows[gridnewvariable.SelectedIndex][2] = false; //gridnewvariable.CurrentCell.Column.DisplayIndex
                                                                           // frmutil.SetErrorForGrid(gridnewvariable, gridnewvariable.SelectedIndex, gridnewvariable.CurrentCell.Column.DisplayIndex, QC4Common.Common.Constants.STD_DP.Background, true);
                frmutil.SetErrorForGrid(gridnewvariable, gridnewvariable.SelectedIndex, gridnewvariable.CurrentCell.Column.DisplayIndex, QC4Common.Common.Constants.STD_DP.Foreground, true);
            }
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

        private void Gridnewvariable_PreviewKeyUp_1(object sender, KeyEventArgs e)
        {

        }
        DataProcessNewLoad dataLoad = new DataProcessNewLoad();
        private void Combo_NewItem_Item_TextChanged(object sender, TextChangedEventArgs e)
        {
            QuestionSettings qs = dataLoad.Txt_Change_New_Item(Combo_NewItem_Item.Text);
            if (qs != null)
            {


                int choicecount = 0;
                choicecount = qs.Choices.Count;
                Text_NewItem_Question.Text = qs.Question;
                if (choicecount > 0)
                {
                    Combo_NewItem_SelectCount.SelectedIndex = qs.Choices.Count;
                    for (int i = 0; i < choicecount; i++)
                    {
                        gridchoice.Rows[i][1] = frmutil.EscapeCRLF(qs.Choices[i]);
                    }
                }
            }
        }

        private void Combo_NewItem_Item_KeyUp(object sender, KeyEventArgs e)
        {
           
        }
    }
}
