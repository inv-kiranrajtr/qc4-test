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
    /// Interaction logic for Integrate.xaml
    /// </summary>
    public partial class Integrate : Window
    {
        excel.Workbook workbook;
        QC4Common.Util.FormUtil frmutil = new QC4Common.Util.FormUtil();
        double min = 0, max = 0, avg = 0;
        DataTable gridchoice;
        DataTable gridsourcevariables;
        int readrow;
        int writerow;
        string Processingtype;
        string processingoption;
        public bool isModifiedProcess = false;
        bool isLoaded = false;
        Logic.DataProcessHelper dphelper = new Logic.DataProcessHelper();
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        bool scroll = true;
        bool scrolltofirst = false;
        bool mouseclick = false;
        string clipboardText = "";
        public Integrate(excel.Workbook wb, int stdreadrow, int stdwriterow, string stdProcessingtype, string stdprocessingoption)
        {
            InitializeComponent();
            workbook = wb;
            readrow = stdreadrow;
            writerow = stdwriterow;
            Processingtype = stdProcessingtype;
            processingoption = stdprocessingoption;

            this.Newvariable1.Command_OriginItem_InputSupport.Click += Command_CriteriaValue1_Click;
            this.Newvariable2.Command_OriginItem_InputSupport.Click += Command_CriteriaValue2_Click;
            this.Newvariable3.Command_OriginItem_InputSupport.Click += Command_CriteriaValue3_Click;
            this.Newvariable4.Command_OriginItem_InputSupport.Click += Command_CriteriaValue4_Click;
            this.Newvariable5.Command_OriginItem_InputSupport.Click += Command_CriteriaValue5_Click;
            this.Newvariable6.Command_OriginItem_InputSupport.Click += Command_CriteriaValue6_Click;
            this.Newvariable7.Command_OriginItem_InputSupport.Click += Command_CriteriaValue7_Click;
            this.Newvariable8.Command_OriginItem_InputSupport.Click += Command_CriteriaValue8_Click;
            this.Newvariable9.Command_OriginItem_InputSupport.Click += Command_CriteriaValue9_Click;
            this.Newvariable10.Command_OriginItem_InputSupport.Click += Command_CriteriaValue10_Click;


            this.Newvariable1.Combo_OriginItem_Item.SelectionChanged += Combo_sourceVariable_SelectionChanged1;
            this.Newvariable2.Combo_OriginItem_Item.SelectionChanged += Combo_sourceVariable_SelectionChanged2;
            this.Newvariable3.Combo_OriginItem_Item.SelectionChanged += Combo_sourceVariable_SelectionChanged3;
            this.Newvariable4.Combo_OriginItem_Item.SelectionChanged += Combo_sourceVariable_SelectionChanged4;
            this.Newvariable5.Combo_OriginItem_Item.SelectionChanged += Combo_sourceVariable_SelectionChanged5;
            this.Newvariable6.Combo_OriginItem_Item.SelectionChanged += Combo_sourceVariable_SelectionChanged6;
            this.Newvariable7.Combo_OriginItem_Item.SelectionChanged += Combo_sourceVariable_SelectionChanged7;
            this.Newvariable8.Combo_OriginItem_Item.SelectionChanged += Combo_sourceVariable_SelectionChanged8;
            this.Newvariable9.Combo_OriginItem_Item.SelectionChanged += Combo_sourceVariable_SelectionChanged9;
            this.Newvariable10.Combo_OriginItem_Item.SelectionChanged += Combo_sourceVariable_SelectionChanged10;


            this.Newvariable1.Combo_OriginItem_Item.PreviewKeyDown += Combo_OriginItem_Item_PreviewKeyDown1;
            this.Newvariable2.Combo_OriginItem_Item.PreviewKeyDown += Combo_OriginItem_Item_PreviewKeyDown2;
            this.Newvariable3.Combo_OriginItem_Item.PreviewKeyDown += Combo_OriginItem_Item_PreviewKeyDown3;
            this.Newvariable4.Combo_OriginItem_Item.PreviewKeyDown += Combo_OriginItem_Item_PreviewKeyDown4;
            this.Newvariable5.Combo_OriginItem_Item.PreviewKeyDown += Combo_OriginItem_Item_PreviewKeyDown5;
            this.Newvariable6.Combo_OriginItem_Item.PreviewKeyDown += Combo_OriginItem_Item_PreviewKeyDown6;
            this.Newvariable7.Combo_OriginItem_Item.PreviewKeyDown += Combo_OriginItem_Item_PreviewKeyDown7;
            this.Newvariable8.Combo_OriginItem_Item.PreviewKeyDown += Combo_OriginItem_Item_PreviewKeyDown8;
            this.Newvariable9.Combo_OriginItem_Item.PreviewKeyDown += Combo_OriginItem_Item_PreviewKeyDown9;
            this.Newvariable10.Combo_OriginItem_Item.PreviewKeyDown += Combo_OriginItem_Item_PreviewKeyDown10;

            this.Newvariable1.Combo_OriginItem_Item.PreviewKeyUp += Combo_OriginItem_Item_PreviewKeyUp;
            this.Newvariable1.Combo_OriginItem_Item.PreviewKeyUp += Combo_OriginItem_Item_PreviewKeyUp;
            this.Newvariable2.Combo_OriginItem_Item.PreviewKeyUp += Combo_OriginItem_Item_PreviewKeyUp;
            this.Newvariable3.Combo_OriginItem_Item.PreviewKeyUp += Combo_OriginItem_Item_PreviewKeyUp;
            this.Newvariable4.Combo_OriginItem_Item.PreviewKeyUp += Combo_OriginItem_Item_PreviewKeyUp;
            this.Newvariable5.Combo_OriginItem_Item.PreviewKeyUp += Combo_OriginItem_Item_PreviewKeyUp;
            this.Newvariable6.Combo_OriginItem_Item.PreviewKeyUp += Combo_OriginItem_Item_PreviewKeyUp;
            this.Newvariable7.Combo_OriginItem_Item.PreviewKeyUp += Combo_OriginItem_Item_PreviewKeyUp;
            this.Newvariable8.Combo_OriginItem_Item.PreviewKeyUp += Combo_OriginItem_Item_PreviewKeyUp;
            this.Newvariable9.Combo_OriginItem_Item.PreviewKeyUp += Combo_OriginItem_Item_PreviewKeyUp;
            this.Newvariable10.Combo_OriginItem_Item.PreviewKeyUp += Combo_OriginItem_Item_PreviewKeyUp;

            this.Newvariable10.Command_OriginItem_InputSupport.PreviewKeyDown += Command_OriginItem_InputSupport_PreviewKeyDown10;
            if (QC4Common.Common.Constants.GlobalMode.Split(',')[0] != "ja-JP")
                Button_Help.Visibility = Visibility.Hidden;
            else
                Button_Help.Visibility = Visibility.Visible;
        }

        private void Command_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Help_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(QC4Common.Classes.Help.GetHelpLink(QC4Common.Common.Constants.HelpButtonType.INTEGRATE));
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (MagnifyingGlassButton.VariableList != null)
            {
                Combo_NewItem_Item.Text = MagnifyingGlassButton.VariableList.Variable;
                try { Combo_NewItem_AnswerType.IsEnabled = false; } catch { }
                Combo_NewItem_AnswerType.SelectedItem = MagnifyingGlassButton.VariableList.AnswerType;
                Text_NewItem_Question.Text = frmutil.UnEscapeCRLF(MagnifyingGlassButton.VariableList.Title) + " " + frmutil.UnEscapeCRLF(MagnifyingGlassButton.VariableList.Question);// frmutil.UnEscapeCRLF(MagnifyingGlassButton.VariableList.Question);
                Combo_NewItem_SelectCount.SelectedIndex = MagnifyingGlassButton.VariableList.Choices.Count;
                Command_Entry.IsEnabled = true;
                // Load Choices and bind DT 
                gridchoice = ClearColumn(1, gridchoice.Rows.Count, gridchoice);
                for (int i = 0; i < MagnifyingGlassButton.VariableList.Choices.Count; i++)
                {
                    gridchoice.Rows[i][1] = frmutil.EscapeCRLF(MagnifyingGlassButton.VariableList.Choices[i]);
                }

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


            try
            {
                Combo_NewItem_Item.IsEnabled = false;
                Text_NewItem_Question.IsEnabled = false;
                NewItemSearchbutton.IsEnabled = false;
                NewItemSearchbutton.Opacity = 0.5;
                Command_Entry.IsEnabled = false;
                Command_NewItem_Input.IsEnabled = false;
                Lbl_NewItem_Input.Foreground = new SolidColorBrush(Colors.DimGray);

                LoadSAMAToCombo();
                Option_AND.IsChecked = true;
                Newvariable1.Label_OriginItem_OriginItem.Text = Qc4Launcher.LocalResource.LABEL_INTEGRATE_SOURCE_VARIABLE + "1";
                Newvariable2.Label_OriginItem_OriginItem.Text = Qc4Launcher.LocalResource.LABEL_INTEGRATE_SOURCE_VARIABLE + "2";
                Newvariable3.Label_OriginItem_OriginItem.Text = Qc4Launcher.LocalResource.LABEL_INTEGRATE_SOURCE_VARIABLE + "3";
                Newvariable4.Label_OriginItem_OriginItem.Text = Qc4Launcher.LocalResource.LABEL_INTEGRATE_SOURCE_VARIABLE + "4";
                Newvariable5.Label_OriginItem_OriginItem.Text = Qc4Launcher.LocalResource.LABEL_INTEGRATE_SOURCE_VARIABLE + "5";
                Newvariable6.Label_OriginItem_OriginItem.Text = Qc4Launcher.LocalResource.LABEL_INTEGRATE_SOURCE_VARIABLE + "6";
                Newvariable7.Label_OriginItem_OriginItem.Text = Qc4Launcher.LocalResource.LABEL_INTEGRATE_SOURCE_VARIABLE + "7";
                Newvariable8.Label_OriginItem_OriginItem.Text = Qc4Launcher.LocalResource.LABEL_INTEGRATE_SOURCE_VARIABLE + "8";
                Newvariable9.Label_OriginItem_OriginItem.Text = Qc4Launcher.LocalResource.LABEL_INTEGRATE_SOURCE_VARIABLE + "9";
                Newvariable10.Label_OriginItem_OriginItem.Text = Qc4Launcher.LocalResource.LABEL_INTEGRATE_SOURCE_VARIABLE + "10";

                string[] choicesList = frmutil.LoadComboWithChoices(Qc4Launcher.Util.Constants.MaxChoiceCount);
                Combo_NewItem_SelectCount.ItemsSource = choicesList;
                Combo_NewItem_SelectCount.SelectedIndex = 0;


                LoadGridValues(Qc4Launcher.Util.Constants.MaxChoiceCount, null);


                PopulateMASAVariableList();
                Newvariable1.Combo_OriginItem_Item.Text = "";
                Newvariable1.Combo_OriginItem_Item.DataContext = SourceVariableListView1;
                Newvariable2.Combo_OriginItem_Item.DataContext = SourceVariableListView2;
                Newvariable3.Combo_OriginItem_Item.DataContext = SourceVariableListView3;
                Newvariable4.Combo_OriginItem_Item.DataContext = SourceVariableListView4;
                Newvariable5.Combo_OriginItem_Item.DataContext = SourceVariableListView5;
                Newvariable6.Combo_OriginItem_Item.DataContext = SourceVariableListView6;
                Newvariable7.Combo_OriginItem_Item.DataContext = SourceVariableListView7;
                Newvariable8.Combo_OriginItem_Item.DataContext = SourceVariableListView8;
                Newvariable9.Combo_OriginItem_Item.DataContext = SourceVariableListView9;
                Newvariable10.Combo_OriginItem_Item.DataContext = SourceVariableListView10;


                Newvariable2.Combo_OriginItem_Item.IsEnabled = false;
                Newvariable3.Combo_OriginItem_Item.IsEnabled = false;
                Newvariable4.Combo_OriginItem_Item.IsEnabled = false;
                Newvariable5.Combo_OriginItem_Item.IsEnabled = false;
                Newvariable6.Combo_OriginItem_Item.IsEnabled = false;
                Newvariable7.Combo_OriginItem_Item.IsEnabled = false;
                Newvariable8.Combo_OriginItem_Item.IsEnabled = false;
                Newvariable9.Combo_OriginItem_Item.IsEnabled = false;
                Newvariable10.Combo_OriginItem_Item.IsEnabled = false;



                Newvariable1.Command_OriginItem_InputSupport.IsEnabled = false;
                Newvariable2.Command_OriginItem_InputSupport.IsEnabled = false;
                Newvariable3.Command_OriginItem_InputSupport.IsEnabled = false;
                Newvariable4.Command_OriginItem_InputSupport.IsEnabled = false;
                Newvariable5.Command_OriginItem_InputSupport.IsEnabled = false;
                Newvariable6.Command_OriginItem_InputSupport.IsEnabled = false;
                Newvariable7.Command_OriginItem_InputSupport.IsEnabled = false;
                Newvariable8.Command_OriginItem_InputSupport.IsEnabled = false;
                Newvariable9.Command_OriginItem_InputSupport.IsEnabled = false;
                Newvariable10.Command_OriginItem_InputSupport.IsEnabled = false;



                for (int i = 0; i < 10; i++)
                {
                    gridoriginalitem.Columns[i].IsReadOnly = true;
                }
                Newvariable1.Combo_OriginItem_Item.Text = "";
                Newvariable2.Combo_OriginItem_Item.Text = string.Empty;
                Newvariable3.Combo_OriginItem_Item.SelectedIndex = -1;
                Newvariable4.Combo_OriginItem_Item.Text = null;
                if (Newvariable1.Combo_OriginItem_Item.IsLoaded)
                {
                    Newvariable1.Combo_OriginItem_Item.Text = "";

                }

                if (processingoption == QC4Common.Common.Constants.STD_DP.Process_Edit || processingoption == QC4Common.Common.Constants.STD_DP.Process_Copy)
                {
                    if (processingoption == QC4Common.Common.Constants.STD_DP.Process_Edit)
                    {
                        Color Combo_ProcessTextboxColor = (Color)ColorConverter.ConvertFromString(QC4Common.Common.Constants.STD_DP.ProcessTextboxColor);
                        Combo_Process.Background = new System.Windows.Media.SolidColorBrush(Combo_ProcessTextboxColor);
                        Color Combo_ProcessTextboxfroreColor = (Color)ColorConverter.ConvertFromString(QC4Common.Common.Constants.STD_DP.ProcessTextboxforeColor);
                        Combo_Process.Foreground = new System.Windows.Media.SolidColorBrush(Combo_ProcessTextboxfroreColor);
                    }
                    string[,] objarray = dphelper.GetRangevalues(readrow, writerow, workbook);
                    SetValuesToControls(objarray);
                    if (processingoption == QC4Common.Common.Constants.STD_DP.Process_Edit)
                    {
                        Combo_NewItem_AnswerType.IsEnabled = false;
                    }
                }
                else
                {
                    NewItemSearchbutton.IsEnabled = true;
                    Combo_NewItem_Item.IsEnabled = true;
                    Combo_NewItem_Item.Background = Brushes.White;
                    Text_NewItem_Question.IsEnabled = true;
                    NewItemSearchbutton.Opacity = 1;
                    Text_NewItem_Question.Background = Brushes.White;
                    Style style = Application.Current.FindResource("NormalTextBoxMultiLine") as Style;
                    Text_NewItem_Question.Style = style;
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
            this.Newvariable1.Combo_OriginItem_Item.Focus();
        }

        #region SelectionChanged
        private void Combo_sourceVariable_SelectionChanged1(object sender, SelectionChangedEventArgs e)
        {
            SetComboValue(sender, 1);
        }
        private void Combo_sourceVariable_SelectionChanged2(object sender, SelectionChangedEventArgs e)
        {
            SetComboValue(sender, 2);
        }
        private void Combo_sourceVariable_SelectionChanged3(object sender, SelectionChangedEventArgs e)
        {
            SetComboValue(sender, 3);
        }
        private void Combo_sourceVariable_SelectionChanged4(object sender, SelectionChangedEventArgs e)
        {
            SetComboValue(sender, 4);
        }
        private void Combo_sourceVariable_SelectionChanged5(object sender, SelectionChangedEventArgs e)
        {
            SetComboValue(sender, 5);
        }
        private void Combo_sourceVariable_SelectionChanged6(object sender, SelectionChangedEventArgs e)
        {
            SetComboValue(sender, 6);
        }
        private void Combo_sourceVariable_SelectionChanged7(object sender, SelectionChangedEventArgs e)
        {
            SetComboValue(sender, 7);
        }
        private void Combo_sourceVariable_SelectionChanged8(object sender, SelectionChangedEventArgs e)
        {
            SetComboValue(sender, 8);
        }
        private void Combo_sourceVariable_SelectionChanged9(object sender, SelectionChangedEventArgs e)
        {
            SetComboValue(sender, 9);
        }
        private void Combo_sourceVariable_SelectionChanged10(object sender, SelectionChangedEventArgs e)
        {
            SetComboValue(sender, 10);
        }
        #endregion SelectionChanged

        private void OnComboLoad1(object sender, EventArgs e)//clearing on load
        {
            System.Windows.Controls.ComboBox combo = (System.Windows.Controls.ComboBox)sender;
            try
            {
                if (combo.SelectedItem == null || ((SourceVariableList)(combo.SelectedItem)).AswerTypes == null)
                {
                    Newvariable1.Combo_OriginItem_Item.Text = string.Empty;
                    combo.Text = string.Empty;
                }
                combo.Text = string.Empty;
                Newvariable1.Combo_OriginItem_Item.Text = string.Empty;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }
        #region DropDownClosed
        private void OnSelectionChanged1(object sender, EventArgs e)//these are dropdown closae events
        {
            SetComboValue(sender, 1);
        }
        private void OnSelectionChanged2(object sender, EventArgs e)
        {
            SetComboValue(sender, 2);
        }
        private void OnSelectionChanged3(object sender, EventArgs e)
        {
            SetComboValue(sender, 3);
        }
        private void OnSelectionChanged4(object sender, EventArgs e)
        {
            SetComboValue(sender, 4);
        }
        private void OnSelectionChanged5(object sender, EventArgs e)
        {
            SetComboValue(sender, 5);
        }
        private void OnSelectionChanged6(object sender, EventArgs e)
        {
            SetComboValue(sender, 6);
        }
        private void OnSelectionChanged7(object sender, EventArgs e)
        {
            SetComboValue(sender, 7);
        }
        private void OnSelectionChanged8(object sender, EventArgs e)
        {
            SetComboValue(sender, 8);
        }
        private void OnSelectionChanged9(object sender, EventArgs e)
        {
            SetComboValue(sender, 9);
        }
        private void OnSelectionChanged10(object sender, EventArgs e)
        {
            SetComboValue(sender, 10);
        }
        #endregion  DropDownClosed
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
                        //z  Newvariable1.Combo_OriginItem_Item.Text = list.Variable;

                        Newvariable1.Text_OriginItem1_AnswerType.Text = frmutil.EscapeCRLF(list.AnswerType);
                        Newvariable1.Text_OriginItem1_SelectCount.Text = (list.Choices.Count).ToString();
                        Newvariable1.Text_OriginItem1_Question.Text = frmutil.UnEscapeCRLF(list.Question);// frmutil.UnEscapeCRLF(list.Question);
                        if (list.AnswerType.Equals(QC4Common.Common.Constants.AnswerType.MA) || list.AnswerType.Equals(QC4Common.Common.Constants.AnswerType.SA))
                        {

                            Newvariable1.minmaxavgborder.Visibility = Visibility.Hidden;
                            Newvariable1.minmaxavg.Visibility = Visibility.Hidden;
                            Newvariable1.List_OriginItem_ChoiceList.Visibility = Visibility.Visible;
                            Newvariable1.List_OriginItem_ChoiceList.Items.Clear();
                            foreach (string choice in list.Choices)
                            {
                                slno++;
                                Newvariable1.List_OriginItem_ChoiceList.Items.Add(string.Format("{0}: {1}", slno, frmutil.EscapeCRLF(choice)));
                            }
                            // Newvariable1.Combo_OriginItem_Item.Text = list.Variable;
                            gridoriginalitem.Columns[sourcevariable - 1].IsReadOnly = false;
                        }
                        else
                        {
                            Newvariable1.Text_OriginItem1_SelectCount.Text = string.Empty;
                            Newvariable1.minmaxavgborder.Visibility = Visibility.Visible;
                            Newvariable1.minmaxavg.Visibility = Visibility.Visible;
                            Newvariable1.List_OriginItem_ChoiceList.Visibility = Visibility.Hidden;
                            //get min ,max,mean from DB
                            if (Util.Definiotion.VariableDictionary.ContainsKey(list.Variable) && !frmutil.IsNewQuestion(Util.Definiotion.VariableDictionary[list.Variable].QuestionFlag))
                            {
                                frmutil.NtypeGetMinMaxAvg((Util.Definiotion.VariableDictionary[list.Variable].ItemId) == 0 ? "sample_id" : "q_" + (Util.Definiotion.VariableDictionary[list.Variable].ItemId).ToString(), workbook, out min, out max, out avg);
                                Newvariable1.Text_OriginItem_Min.Text = min.ToString();
                                Newvariable1.Text_OriginItem_Max.Text = max.ToString();
                                Newvariable1.Text_OriginItem_Avg.Text = avg.ToString();
                            }
                            // Newvariable1.Combo_OriginItem_Item.Text = list.Variable;
                            gridoriginalitem.Columns[sourcevariable - 1].IsReadOnly = false;
                        }
                        Newvariable1.Command_OriginItem_InputSupport.IsEnabled = true;
                        Newvariable2.Combo_OriginItem_Item.IsEnabled = true;
                        if (SourceVariableListView2[0].Choices != null)
                        {
                            SourceVariableListView2.Insert(0, NewvariableNullItem());
                        }
                        NewItemColorChangeEnable();
                        // Newvariable1.Combo_OriginItem_Item.Text = list.Variable;


                        Command_Entry.IsEnabled = true;
                        break;
                    case 2:
                        list = (SourceVariableList)(sourceVariable.SelectedItem);

                        if (list.Choices != null)
                        {

                            gridoriginalitem.Columns[sourcevariable - 1].IsReadOnly = false;
                            Newvariable2.Text_OriginItem1_AnswerType.Text = frmutil.EscapeCRLF(list.AnswerType);
                            Newvariable2.Text_OriginItem1_SelectCount.Text = (list.Choices.Count).ToString();
                            Newvariable2.Text_OriginItem1_Question.Text = frmutil.UnEscapeCRLF(list.Question);//frmutil.EscapeCRLF(list.Question);
                            if (list.AnswerType.Equals(QC4Common.Common.Constants.AnswerType.MA) || list.AnswerType.Equals(QC4Common.Common.Constants.AnswerType.SA))
                            {
                                Newvariable2.minmaxavgborder.Visibility = Visibility.Hidden;
                                Newvariable2.minmaxavg.Visibility = Visibility.Hidden;
                                Newvariable2.List_OriginItem_ChoiceList.Visibility = Visibility.Visible;
                                Newvariable2.List_OriginItem_ChoiceList.Items.Clear();
                                foreach (string choice in list.Choices)
                                {
                                    slno++;
                                    Newvariable2.List_OriginItem_ChoiceList.Items.Add(string.Format("{0}: {1}", slno, frmutil.EscapeCRLF(choice)));
                                }
                            }
                            else
                            {
                                Newvariable2.Text_OriginItem1_SelectCount.Text = string.Empty;
                                Newvariable2.minmaxavgborder.Visibility = Visibility.Visible;
                                Newvariable2.minmaxavg.Visibility = Visibility.Visible;
                                Newvariable2.List_OriginItem_ChoiceList.Visibility = Visibility.Hidden;
                                //get min ,max,mean from DB
                                if (Util.Definiotion.VariableDictionary.ContainsKey(list.Variable) && !frmutil.IsNewQuestion(Util.Definiotion.VariableDictionary[list.Variable].QuestionFlag))
                                {
                                    frmutil.NtypeGetMinMaxAvg((Util.Definiotion.VariableDictionary[list.Variable].ItemId) == 0 ? "sample_id" : "q_" + (Util.Definiotion.VariableDictionary[list.Variable].ItemId).ToString(), workbook, out min, out max, out avg);
                                    Newvariable2.Text_OriginItem_Min.Text = min.ToString();
                                    Newvariable2.Text_OriginItem_Max.Text = max.ToString();
                                    Newvariable2.Text_OriginItem_Avg.Text = avg.ToString();
                                }
                            }
                            Newvariable2.Command_OriginItem_InputSupport.IsEnabled = true;
                            Newvariable3.Combo_OriginItem_Item.IsEnabled = true;
                            if (SourceVariableListView3[0].Choices != null)
                            {
                                SourceVariableListView3.Insert(0, NewvariableNullItem());
                            }

                            // Newvariable2.Combo_OriginItem_Item.Text = list.Variable;
                        }
                        else
                        {
                            Newvariable2.Text_OriginItem1_AnswerType.Text = string.Empty;
                            Newvariable2.Text_OriginItem1_SelectCount.Text = string.Empty;
                            Newvariable2.Text_OriginItem1_Question.Text = string.Empty;
                            Newvariable2.minmaxavg.Visibility = Visibility.Hidden;
                            Newvariable2.List_OriginItem_ChoiceList.Visibility = Visibility.Visible;
                            Newvariable2.List_OriginItem_ChoiceList.Items.Clear();
                            Newvariable2.Command_OriginItem_InputSupport.IsEnabled = false;
                            Newvariable3.Combo_OriginItem_Item.IsEnabled = false;
                            gridoriginalitem.Columns[sourcevariable - 1].IsReadOnly = true;
                            Newvariable2.minmaxavgborder.Visibility = Visibility.Hidden;
                            gridsourcevariables = ClearColumn(sourcevariable - 1, gridsourcevariables.Rows.Count, gridsourcevariables);
                            Newvariable2.Combo_OriginItem_Item.Text = string.Empty;
                        }

                        break;
                    case 3:
                        list = (SourceVariableList)(sourceVariable.SelectedItem);

                        if (list.Choices != null)
                        {
                            gridoriginalitem.Columns[sourcevariable - 1].IsReadOnly = false;
                            Newvariable3.Text_OriginItem1_AnswerType.Text = frmutil.EscapeCRLF(list.AnswerType);
                            Newvariable3.Text_OriginItem1_SelectCount.Text = (list.Choices.Count).ToString();
                            Newvariable3.Text_OriginItem1_Question.Text = frmutil.UnEscapeCRLF(list.Question);// frmutil.EscapeCRLF(list.Question);
                            if (list.AnswerType.Equals(QC4Common.Common.Constants.AnswerType.MA) || list.AnswerType.Equals(QC4Common.Common.Constants.AnswerType.SA))
                            {
                                Newvariable3.minmaxavgborder.Visibility = Visibility.Hidden;
                                Newvariable3.minmaxavg.Visibility = Visibility.Hidden;
                                Newvariable3.List_OriginItem_ChoiceList.Visibility = Visibility.Visible;
                                Newvariable3.List_OriginItem_ChoiceList.Items.Clear();
                                foreach (string choice in list.Choices)
                                {
                                    slno++;
                                    Newvariable3.List_OriginItem_ChoiceList.Items.Add(string.Format("{0}: {1}", slno, frmutil.EscapeCRLF(choice)));
                                }
                            }
                            else
                            {
                                Newvariable3.Text_OriginItem1_SelectCount.Text = string.Empty;
                                Newvariable3.minmaxavgborder.Visibility = Visibility.Visible;
                                Newvariable3.minmaxavg.Visibility = Visibility.Visible;
                                Newvariable3.List_OriginItem_ChoiceList.Visibility = Visibility.Hidden;
                                //get min ,max,mean from DB
                                if (Util.Definiotion.VariableDictionary.ContainsKey(list.Variable) && !frmutil.IsNewQuestion(Util.Definiotion.VariableDictionary[list.Variable].QuestionFlag))
                                {
                                    frmutil.NtypeGetMinMaxAvg((Util.Definiotion.VariableDictionary[list.Variable].ItemId) == 0 ? "sample_id" : "q_" + (Util.Definiotion.VariableDictionary[list.Variable].ItemId).ToString(), workbook, out min, out max, out avg);
                                    Newvariable3.Text_OriginItem_Min.Text = min.ToString();
                                    Newvariable3.Text_OriginItem_Max.Text = max.ToString();
                                    Newvariable3.Text_OriginItem_Avg.Text = avg.ToString();
                                }
                            }
                            Newvariable3.Command_OriginItem_InputSupport.IsEnabled = true;
                            Newvariable4.Combo_OriginItem_Item.IsEnabled = true;
                            if (SourceVariableListView4[0].Choices != null)
                            {
                                SourceVariableListView4.Insert(0, NewvariableNullItem());
                            }
                            if (SourceVariableListView2[0].Choices == null)
                            {
                                SourceVariableListView2.RemoveAt(0);
                            }
                            //  Newvariable3.Combo_OriginItem_Item.Text = list.Variable;
                        }
                        else
                        {
                            gridoriginalitem.Columns[sourcevariable - 1].IsReadOnly = true;
                            Newvariable3.Text_OriginItem1_AnswerType.Text = string.Empty;
                            Newvariable3.Text_OriginItem1_SelectCount.Text = string.Empty;
                            Newvariable3.Text_OriginItem1_Question.Text = string.Empty;
                            Newvariable3.minmaxavg.Visibility = Visibility.Hidden;
                            Newvariable3.List_OriginItem_ChoiceList.Visibility = Visibility.Visible;
                            Newvariable3.List_OriginItem_ChoiceList.Items.Clear();
                            Newvariable3.Command_OriginItem_InputSupport.IsEnabled = false;
                            Newvariable4.Combo_OriginItem_Item.IsEnabled = false;
                            Newvariable3.minmaxavgborder.Visibility = Visibility.Hidden;
                            if (SourceVariableListView2[0].Choices != null)
                            {
                                SourceVariableListView2.Insert(0, NewvariableNullItem());
                            }
                            gridsourcevariables = ClearColumn(sourcevariable - 1, gridsourcevariables.Rows.Count, gridsourcevariables);
                            Newvariable3.Combo_OriginItem_Item.Text = string.Empty;
                        }
                        break;
                    case 4:
                        list = (SourceVariableList)(sourceVariable.SelectedItem);
                        //  Newvariable1.Combo_OriginItem_Item.Text = list.Variable;
                        if (list.Choices != null)
                        {
                            gridoriginalitem.Columns[sourcevariable - 1].IsReadOnly = false;
                            Newvariable4.Text_OriginItem1_AnswerType.Text = frmutil.EscapeCRLF(list.AnswerType);
                            Newvariable4.Text_OriginItem1_SelectCount.Text = (list.Choices.Count).ToString();
                            Newvariable4.Text_OriginItem1_Question.Text = frmutil.UnEscapeCRLF(list.Question);// frmutil.EscapeCRLF(list.Question);
                            if (list.AnswerType.Equals(QC4Common.Common.Constants.AnswerType.MA) || list.AnswerType.Equals(QC4Common.Common.Constants.AnswerType.SA))
                            {
                                Newvariable4.minmaxavgborder.Visibility = Visibility.Hidden;
                                Newvariable4.minmaxavg.Visibility = Visibility.Hidden;
                                Newvariable4.List_OriginItem_ChoiceList.Visibility = Visibility.Visible;
                                Newvariable4.List_OriginItem_ChoiceList.Items.Clear();
                                foreach (string choice in list.Choices)
                                {
                                    slno++;
                                    Newvariable4.List_OriginItem_ChoiceList.Items.Add(string.Format("{0}: {1}", slno, frmutil.EscapeCRLF(choice)));
                                }
                            }
                            else
                            {
                                Newvariable4.Text_OriginItem1_SelectCount.Text = string.Empty;
                                Newvariable4.minmaxavgborder.Visibility = Visibility.Visible;
                                Newvariable4.minmaxavg.Visibility = Visibility.Visible;
                                Newvariable4.List_OriginItem_ChoiceList.Visibility = Visibility.Hidden;
                                //get min ,max,mean from DB
                                if (Util.Definiotion.VariableDictionary.ContainsKey(list.Variable) && !frmutil.IsNewQuestion(Util.Definiotion.VariableDictionary[list.Variable].QuestionFlag))
                                {
                                    frmutil.NtypeGetMinMaxAvg((Util.Definiotion.VariableDictionary[list.Variable].ItemId) == 0 ? "sample_id" : "q_" + (Util.Definiotion.VariableDictionary[list.Variable].ItemId).ToString(), workbook, out min, out max, out avg);
                                    Newvariable4.Text_OriginItem_Min.Text = min.ToString();
                                    Newvariable4.Text_OriginItem_Max.Text = max.ToString();
                                    Newvariable4.Text_OriginItem_Avg.Text = avg.ToString();
                                }
                            }
                            Newvariable4.Command_OriginItem_InputSupport.IsEnabled = true;
                            Newvariable5.Combo_OriginItem_Item.IsEnabled = true;
                            if (SourceVariableListView5[0].Choices != null)
                            {
                                SourceVariableListView5.Insert(0, NewvariableNullItem());
                            }
                            if (SourceVariableListView3[0].Choices == null)
                            {
                                SourceVariableListView3.RemoveAt(0);
                            }
                            //  Newvariable4.Combo_OriginItem_Item.Text = list.Variable;
                        }
                        else
                        {
                            gridoriginalitem.Columns[sourcevariable - 1].IsReadOnly = true;
                            Newvariable4.Text_OriginItem1_AnswerType.Text = string.Empty;
                            Newvariable4.Text_OriginItem1_SelectCount.Text = string.Empty;
                            Newvariable4.Text_OriginItem1_Question.Text = string.Empty;
                            Newvariable4.minmaxavg.Visibility = Visibility.Hidden;
                            Newvariable4.List_OriginItem_ChoiceList.Visibility = Visibility.Visible;
                            Newvariable4.List_OriginItem_ChoiceList.Items.Clear();
                            Newvariable4.Command_OriginItem_InputSupport.IsEnabled = false;
                            Newvariable5.Combo_OriginItem_Item.IsEnabled = false;
                            Newvariable4.minmaxavgborder.Visibility = Visibility.Hidden;
                            if (SourceVariableListView3[0].Choices != null)
                            {
                                SourceVariableListView3.Insert(0, NewvariableNullItem());
                            }
                            gridsourcevariables = ClearColumn(sourcevariable - 1, gridsourcevariables.Rows.Count, gridsourcevariables);
                            Newvariable4.Combo_OriginItem_Item.Text = string.Empty;
                        }
                        break;
                    case 5:
                        list = (SourceVariableList)(sourceVariable.SelectedItem);
                        //  Newvariable1.Combo_OriginItem_Item.Text = list.Variable;
                        if (list.Choices != null)
                        {
                            gridoriginalitem.Columns[sourcevariable - 1].IsReadOnly = false;
                            Newvariable5.Text_OriginItem1_AnswerType.Text = frmutil.EscapeCRLF(list.AnswerType);
                            Newvariable5.Text_OriginItem1_SelectCount.Text = (list.Choices.Count).ToString();
                            Newvariable5.Text_OriginItem1_Question.Text = frmutil.UnEscapeCRLF(list.Question);// frmutil.EscapeCRLF(list.Question);
                            if (list.AnswerType.Equals(QC4Common.Common.Constants.AnswerType.MA) || list.AnswerType.Equals(QC4Common.Common.Constants.AnswerType.SA))
                            {
                                Newvariable5.minmaxavgborder.Visibility = Visibility.Hidden;
                                Newvariable5.minmaxavg.Visibility = Visibility.Hidden;
                                Newvariable5.List_OriginItem_ChoiceList.Visibility = Visibility.Visible;
                                Newvariable5.List_OriginItem_ChoiceList.Items.Clear();
                                foreach (string choice in list.Choices)
                                {
                                    slno++;
                                    Newvariable5.List_OriginItem_ChoiceList.Items.Add(string.Format("{0}: {1}", slno, frmutil.EscapeCRLF(choice)));
                                }
                            }
                            else
                            {
                                Newvariable5.Text_OriginItem1_SelectCount.Text = string.Empty;
                                Newvariable5.minmaxavgborder.Visibility = Visibility.Visible;
                                Newvariable5.minmaxavg.Visibility = Visibility.Visible;
                                Newvariable5.List_OriginItem_ChoiceList.Visibility = Visibility.Hidden;
                                //get min ,max,mean from DB
                                if (Util.Definiotion.VariableDictionary.ContainsKey(list.Variable) && !frmutil.IsNewQuestion(Util.Definiotion.VariableDictionary[list.Variable].QuestionFlag))
                                {
                                    frmutil.NtypeGetMinMaxAvg((Util.Definiotion.VariableDictionary[list.Variable].ItemId) == 0 ? "sample_id" : "q_" + (Util.Definiotion.VariableDictionary[list.Variable].ItemId).ToString(), workbook, out min, out max, out avg);
                                    Newvariable5.Text_OriginItem_Min.Text = min.ToString();
                                    Newvariable5.Text_OriginItem_Max.Text = max.ToString();
                                    Newvariable5.Text_OriginItem_Avg.Text = avg.ToString();
                                }
                            }
                            Newvariable5.Command_OriginItem_InputSupport.IsEnabled = true;
                            Newvariable6.Combo_OriginItem_Item.IsEnabled = true;
                            if (SourceVariableListView6[0].Choices != null)
                            {
                                SourceVariableListView6.Insert(0, NewvariableNullItem());
                            }
                            if (SourceVariableListView4[0].Choices == null)
                            {
                                SourceVariableListView4.RemoveAt(0);
                            }
                            //  Newvariable5.Combo_OriginItem_Item.Text = list.Variable;
                        }
                        else
                        {
                            gridoriginalitem.Columns[sourcevariable - 1].IsReadOnly = true;
                            Newvariable5.Text_OriginItem1_AnswerType.Text = string.Empty;
                            Newvariable5.Text_OriginItem1_SelectCount.Text = string.Empty;
                            Newvariable5.Text_OriginItem1_Question.Text = string.Empty;
                            Newvariable5.minmaxavg.Visibility = Visibility.Hidden;
                            Newvariable5.List_OriginItem_ChoiceList.Visibility = Visibility.Visible;
                            Newvariable5.List_OriginItem_ChoiceList.Items.Clear();
                            Newvariable5.Command_OriginItem_InputSupport.IsEnabled = false;
                            Newvariable6.Combo_OriginItem_Item.IsEnabled = false;
                            Newvariable5.minmaxavgborder.Visibility = Visibility.Hidden;
                            if (SourceVariableListView4[0].Choices != null)
                            {
                                SourceVariableListView4.Insert(0, NewvariableNullItem());
                            }
                            gridsourcevariables = ClearColumn(sourcevariable - 1, gridsourcevariables.Rows.Count, gridsourcevariables);
                            Newvariable5.Combo_OriginItem_Item.Text = string.Empty;
                        }
                        break;
                    case 6:
                        list = (SourceVariableList)(sourceVariable.SelectedItem);
                        //  Newvariable1.Combo_OriginItem_Item.Text = list.Variable;
                        if (list.Choices != null)
                        {
                            gridoriginalitem.Columns[sourcevariable - 1].IsReadOnly = false;
                            Newvariable6.Text_OriginItem1_AnswerType.Text = frmutil.EscapeCRLF(list.AnswerType);
                            Newvariable6.Text_OriginItem1_SelectCount.Text = (list.Choices.Count).ToString();
                            Newvariable6.Text_OriginItem1_Question.Text = frmutil.UnEscapeCRLF(list.Question);//frmutil.EscapeCRLF(list.Question);
                            if (list.AnswerType.Equals(QC4Common.Common.Constants.AnswerType.MA) || list.AnswerType.Equals(QC4Common.Common.Constants.AnswerType.SA))
                            {
                                Newvariable6.minmaxavgborder.Visibility = Visibility.Hidden;
                                Newvariable6.minmaxavg.Visibility = Visibility.Hidden;
                                Newvariable6.List_OriginItem_ChoiceList.Visibility = Visibility.Visible;
                                Newvariable6.List_OriginItem_ChoiceList.Items.Clear();
                                foreach (string choice in list.Choices)
                                {
                                    slno++;
                                    Newvariable6.List_OriginItem_ChoiceList.Items.Add(string.Format("{0}: {1}", slno, frmutil.EscapeCRLF(choice)));
                                }
                            }
                            else
                            {
                                Newvariable6.Text_OriginItem1_SelectCount.Text = string.Empty;
                                Newvariable6.minmaxavgborder.Visibility = Visibility.Visible;
                                Newvariable6.minmaxavg.Visibility = Visibility.Visible;
                                Newvariable6.List_OriginItem_ChoiceList.Visibility = Visibility.Hidden;
                                //get min ,max,mean from DB
                                if (Util.Definiotion.VariableDictionary.ContainsKey(list.Variable) && !frmutil.IsNewQuestion(Util.Definiotion.VariableDictionary[list.Variable].QuestionFlag))
                                {
                                    frmutil.NtypeGetMinMaxAvg((Util.Definiotion.VariableDictionary[list.Variable].ItemId) == 0 ? "sample_id" : "q_" + (Util.Definiotion.VariableDictionary[list.Variable].ItemId).ToString(), workbook, out min, out max, out avg);
                                    Newvariable6.Text_OriginItem_Min.Text = min.ToString();
                                    Newvariable6.Text_OriginItem_Max.Text = max.ToString();
                                    Newvariable6.Text_OriginItem_Avg.Text = avg.ToString();
                                }
                            }
                            Newvariable6.Command_OriginItem_InputSupport.IsEnabled = true;
                            Newvariable7.Combo_OriginItem_Item.IsEnabled = true;
                            if (SourceVariableListView7[0].Choices != null)
                            {
                                SourceVariableListView7.Insert(0, NewvariableNullItem());
                            }
                            if (SourceVariableListView5[0].Choices == null)
                            {
                                SourceVariableListView5.RemoveAt(0);
                            }
                            // Newvariable6.Combo_OriginItem_Item.Text = list.Variable;
                        }
                        else
                        {
                            gridoriginalitem.Columns[sourcevariable - 1].IsReadOnly = true;
                            Newvariable6.Text_OriginItem1_AnswerType.Text = string.Empty;
                            Newvariable6.Text_OriginItem1_SelectCount.Text = string.Empty;
                            Newvariable6.Text_OriginItem1_Question.Text = string.Empty;
                            Newvariable6.minmaxavg.Visibility = Visibility.Hidden;
                            Newvariable6.List_OriginItem_ChoiceList.Visibility = Visibility.Visible;
                            Newvariable6.List_OriginItem_ChoiceList.Items.Clear();
                            Newvariable6.Command_OriginItem_InputSupport.IsEnabled = false;
                            Newvariable7.Combo_OriginItem_Item.IsEnabled = false;
                            Newvariable6.minmaxavgborder.Visibility = Visibility.Hidden;
                            if (SourceVariableListView5[0].Choices != null)
                            {
                                SourceVariableListView5.Insert(0, NewvariableNullItem());
                            }
                            gridsourcevariables = ClearColumn(sourcevariable - 1, gridsourcevariables.Rows.Count, gridsourcevariables);
                            Newvariable6.Combo_OriginItem_Item.Text = string.Empty;
                        }
                        break;
                    case 7:
                        list = (SourceVariableList)(sourceVariable.SelectedItem);
                        //  Newvariable1.Combo_OriginItem_Item.Text = list.Variable;
                        if (list.Choices != null)
                        {
                            gridoriginalitem.Columns[sourcevariable - 1].IsReadOnly = false;
                            Newvariable7.Text_OriginItem1_AnswerType.Text = frmutil.EscapeCRLF(list.AnswerType);
                            Newvariable7.Text_OriginItem1_SelectCount.Text = (list.Choices.Count).ToString();
                            Newvariable7.Text_OriginItem1_Question.Text = frmutil.UnEscapeCRLF(list.Question);//frmutil.EscapeCRLF(list.Question);
                            if (list.AnswerType.Equals(QC4Common.Common.Constants.AnswerType.MA) || list.AnswerType.Equals(QC4Common.Common.Constants.AnswerType.SA))
                            {
                                Newvariable7.minmaxavgborder.Visibility = Visibility.Hidden;
                                Newvariable7.minmaxavg.Visibility = Visibility.Hidden;
                                Newvariable7.List_OriginItem_ChoiceList.Visibility = Visibility.Visible;
                                Newvariable7.List_OriginItem_ChoiceList.Items.Clear();
                                foreach (string choice in list.Choices)
                                {
                                    slno++;
                                    Newvariable7.List_OriginItem_ChoiceList.Items.Add(string.Format("{0}: {1}", slno, frmutil.EscapeCRLF(choice)));
                                }
                            }
                            else
                            {
                                Newvariable7.Text_OriginItem1_SelectCount.Text = string.Empty;
                                Newvariable7.minmaxavgborder.Visibility = Visibility.Visible;
                                Newvariable7.minmaxavg.Visibility = Visibility.Visible;
                                Newvariable7.List_OriginItem_ChoiceList.Visibility = Visibility.Hidden;
                                //get min ,max,mean from DB
                                if (Util.Definiotion.VariableDictionary.ContainsKey(list.Variable) && !frmutil.IsNewQuestion(Util.Definiotion.VariableDictionary[list.Variable].QuestionFlag))
                                {
                                    frmutil.NtypeGetMinMaxAvg((Util.Definiotion.VariableDictionary[list.Variable].ItemId) == 0 ? "sample_id" : "q_" + (Util.Definiotion.VariableDictionary[list.Variable].ItemId).ToString(), workbook, out min, out max, out avg);
                                    Newvariable7.Text_OriginItem_Min.Text = min.ToString();
                                    Newvariable7.Text_OriginItem_Max.Text = max.ToString();
                                    Newvariable7.Text_OriginItem_Avg.Text = avg.ToString();
                                }
                            }
                            Newvariable7.Command_OriginItem_InputSupport.IsEnabled = true;
                            Newvariable8.Combo_OriginItem_Item.IsEnabled = true;
                            if (SourceVariableListView8[0].Choices != null)
                            {
                                SourceVariableListView8.Insert(0, NewvariableNullItem());
                            }
                            if (SourceVariableListView6[0].Choices == null)
                            {
                                SourceVariableListView6.RemoveAt(0);
                            }
                            //  Newvariable7.Combo_OriginItem_Item.Text = list.Variable;
                        }
                        else
                        {
                            gridoriginalitem.Columns[sourcevariable - 1].IsReadOnly = true;
                            Newvariable7.Text_OriginItem1_AnswerType.Text = string.Empty;
                            Newvariable7.Text_OriginItem1_SelectCount.Text = string.Empty;
                            Newvariable7.Text_OriginItem1_Question.Text = string.Empty;
                            Newvariable7.minmaxavg.Visibility = Visibility.Hidden;
                            Newvariable7.List_OriginItem_ChoiceList.Visibility = Visibility.Visible;
                            Newvariable7.List_OriginItem_ChoiceList.Items.Clear();
                            Newvariable7.Command_OriginItem_InputSupport.IsEnabled = false;
                            Newvariable8.Combo_OriginItem_Item.IsEnabled = false;
                            Newvariable7.minmaxavgborder.Visibility = Visibility.Hidden;
                            if (SourceVariableListView6[0].Choices != null)
                            {
                                SourceVariableListView6.Insert(0, NewvariableNullItem());
                            }
                            gridsourcevariables = ClearColumn(sourcevariable - 1, gridsourcevariables.Rows.Count, gridsourcevariables);
                            Newvariable7.Combo_OriginItem_Item.Text = string.Empty;
                        }
                        break;
                    case 8:
                        list = (SourceVariableList)(sourceVariable.SelectedItem);
                        //  Newvariable1.Combo_OriginItem_Item.Text = list.Variable;
                        if (list.Choices != null)
                        {
                            gridoriginalitem.Columns[sourcevariable - 1].IsReadOnly = false;
                            Newvariable8.Text_OriginItem1_AnswerType.Text = frmutil.EscapeCRLF(list.AnswerType);
                            Newvariable8.Text_OriginItem1_SelectCount.Text = (list.Choices.Count).ToString();
                            Newvariable8.Text_OriginItem1_Question.Text = frmutil.UnEscapeCRLF(list.Question);//frmutil.EscapeCRLF(list.Question);
                            if (list.AnswerType.Equals(QC4Common.Common.Constants.AnswerType.MA) || list.AnswerType.Equals(QC4Common.Common.Constants.AnswerType.SA))
                            {
                                Newvariable8.minmaxavgborder.Visibility = Visibility.Hidden;
                                Newvariable8.minmaxavg.Visibility = Visibility.Hidden;
                                Newvariable8.List_OriginItem_ChoiceList.Visibility = Visibility.Visible;
                                Newvariable8.List_OriginItem_ChoiceList.Items.Clear();
                                foreach (string choice in list.Choices)
                                {
                                    slno++;
                                    Newvariable8.List_OriginItem_ChoiceList.Items.Add(string.Format("{0}: {1}", slno, frmutil.EscapeCRLF(choice)));
                                }
                            }
                            else
                            {
                                Newvariable8.Text_OriginItem1_SelectCount.Text = string.Empty;
                                Newvariable8.minmaxavgborder.Visibility = Visibility.Visible;
                                Newvariable8.minmaxavg.Visibility = Visibility.Visible;
                                Newvariable8.List_OriginItem_ChoiceList.Visibility = Visibility.Hidden;
                                //get min ,max,mean from DB
                                if (Util.Definiotion.VariableDictionary.ContainsKey(list.Variable) && !frmutil.IsNewQuestion(Util.Definiotion.VariableDictionary[list.Variable].QuestionFlag))
                                {
                                    frmutil.NtypeGetMinMaxAvg((Util.Definiotion.VariableDictionary[list.Variable].ItemId) == 0 ? "sample_id" : "q_" + (Util.Definiotion.VariableDictionary[list.Variable].ItemId).ToString(), workbook, out min, out max, out avg);
                                    Newvariable8.Text_OriginItem_Min.Text = min.ToString();
                                    Newvariable8.Text_OriginItem_Max.Text = max.ToString();
                                    Newvariable8.Text_OriginItem_Avg.Text = avg.ToString();
                                }
                            }
                            Newvariable8.Command_OriginItem_InputSupport.IsEnabled = true;
                            Newvariable9.Combo_OriginItem_Item.IsEnabled = true;
                            if (SourceVariableListView9[0].Choices != null)
                            {
                                SourceVariableListView9.Insert(0, NewvariableNullItem());
                            }
                            if (SourceVariableListView7[0].Choices == null)
                            {
                                SourceVariableListView7.RemoveAt(0);
                            }
                            // Newvariable8.Combo_OriginItem_Item.Text = list.Variable;
                        }
                        else
                        {
                            gridoriginalitem.Columns[sourcevariable - 1].IsReadOnly = true;
                            Newvariable8.Text_OriginItem1_AnswerType.Text = string.Empty;
                            Newvariable8.Text_OriginItem1_SelectCount.Text = string.Empty;
                            Newvariable8.Text_OriginItem1_Question.Text = string.Empty;
                            Newvariable8.minmaxavg.Visibility = Visibility.Hidden;
                            Newvariable8.List_OriginItem_ChoiceList.Visibility = Visibility.Visible;
                            Newvariable8.List_OriginItem_ChoiceList.Items.Clear();
                            Newvariable8.Command_OriginItem_InputSupport.IsEnabled = false;
                            Newvariable9.Combo_OriginItem_Item.IsEnabled = false;
                            Newvariable8.minmaxavgborder.Visibility = Visibility.Hidden;
                            if (SourceVariableListView7[0].Choices != null)
                            {
                                SourceVariableListView7.Insert(0, NewvariableNullItem());
                            }
                            gridsourcevariables = ClearColumn(sourcevariable - 1, gridsourcevariables.Rows.Count, gridsourcevariables);
                            Newvariable8.Combo_OriginItem_Item.Text = string.Empty;
                        }
                        break;
                    case 9:
                        list = (SourceVariableList)(sourceVariable.SelectedItem);
                        //  Newvariable1.Combo_OriginItem_Item.Text = list.Variable;
                        if (list.Choices != null)
                        {
                            gridoriginalitem.Columns[sourcevariable - 1].IsReadOnly = false;
                            Newvariable9.Text_OriginItem1_AnswerType.Text = frmutil.EscapeCRLF(list.AnswerType);
                            Newvariable9.Text_OriginItem1_SelectCount.Text = (list.Choices.Count).ToString();
                            Newvariable9.Text_OriginItem1_Question.Text = frmutil.UnEscapeCRLF(list.Question);//frmutil.EscapeCRLF(list.Question);
                            if (list.AnswerType.Equals(QC4Common.Common.Constants.AnswerType.MA) || list.AnswerType.Equals(QC4Common.Common.Constants.AnswerType.SA))
                            {
                                Newvariable9.minmaxavgborder.Visibility = Visibility.Hidden;
                                Newvariable9.minmaxavg.Visibility = Visibility.Hidden;
                                Newvariable9.List_OriginItem_ChoiceList.Visibility = Visibility.Visible;
                                Newvariable9.List_OriginItem_ChoiceList.Items.Clear();
                                foreach (string choice in list.Choices)
                                {
                                    slno++;
                                    Newvariable9.List_OriginItem_ChoiceList.Items.Add(string.Format("{0}: {1}", slno, frmutil.EscapeCRLF(choice)));
                                }
                            }
                            else
                            {
                                Newvariable9.Text_OriginItem1_SelectCount.Text = string.Empty;
                                Newvariable9.minmaxavgborder.Visibility = Visibility.Visible;
                                Newvariable9.minmaxavg.Visibility = Visibility.Visible;
                                Newvariable9.List_OriginItem_ChoiceList.Visibility = Visibility.Hidden;
                                //get min ,max,mean from DB
                                if (Util.Definiotion.VariableDictionary.ContainsKey(list.Variable) && !frmutil.IsNewQuestion(Util.Definiotion.VariableDictionary[list.Variable].QuestionFlag))
                                {
                                    frmutil.NtypeGetMinMaxAvg((Util.Definiotion.VariableDictionary[list.Variable].ItemId) == 0 ? "sample_id" : "q_" + (Util.Definiotion.VariableDictionary[list.Variable].ItemId).ToString(), workbook, out min, out max, out avg);
                                    Newvariable9.Text_OriginItem_Min.Text = min.ToString();
                                    Newvariable9.Text_OriginItem_Max.Text = max.ToString();
                                    Newvariable9.Text_OriginItem_Avg.Text = avg.ToString();
                                }
                            }
                            Newvariable9.Command_OriginItem_InputSupport.IsEnabled = true;
                            Newvariable10.Combo_OriginItem_Item.IsEnabled = true;
                            if (SourceVariableListView10[0].Choices != null)
                            {
                                SourceVariableListView10.Insert(0, NewvariableNullItem());
                            }
                            if (SourceVariableListView8[0].Choices == null)
                            {
                                SourceVariableListView8.RemoveAt(0);
                            }
                            // Newvariable9.Combo_OriginItem_Item.Text = list.Variable;
                        }
                        else
                        {
                            gridoriginalitem.Columns[sourcevariable - 1].IsReadOnly = true;
                            Newvariable9.Text_OriginItem1_AnswerType.Text = string.Empty;
                            Newvariable9.Text_OriginItem1_SelectCount.Text = string.Empty;
                            Newvariable9.Text_OriginItem1_Question.Text = string.Empty;
                            Newvariable9.minmaxavg.Visibility = Visibility.Hidden;
                            Newvariable9.List_OriginItem_ChoiceList.Visibility = Visibility.Visible;
                            Newvariable9.List_OriginItem_ChoiceList.Items.Clear();
                            Newvariable9.Command_OriginItem_InputSupport.IsEnabled = false;
                            Newvariable10.Combo_OriginItem_Item.IsEnabled = false;
                            Newvariable9.minmaxavgborder.Visibility = Visibility.Hidden;
                            if (SourceVariableListView8[0].Choices != null)
                            {
                                SourceVariableListView8.Insert(0, NewvariableNullItem());
                            }
                            gridsourcevariables = ClearColumn(sourcevariable - 1, gridsourcevariables.Rows.Count, gridsourcevariables);
                            Newvariable9.Combo_OriginItem_Item.Text = string.Empty;
                        }
                        break;
                    case 10:
                        list = (SourceVariableList)(sourceVariable.SelectedItem);
                        //  Newvariable1.Combo_OriginItem_Item.Text = list.Variable;
                        if (list.Choices != null)
                        {
                            gridoriginalitem.Columns[sourcevariable - 1].IsReadOnly = false;
                            Newvariable10.Text_OriginItem1_AnswerType.Text = frmutil.EscapeCRLF(list.AnswerType);
                            Newvariable10.Text_OriginItem1_SelectCount.Text = (list.Choices.Count).ToString();
                            Newvariable10.Text_OriginItem1_Question.Text = frmutil.UnEscapeCRLF(list.Question);//frmutil.EscapeCRLF(list.Question);
                            if (list.AnswerType.Equals(QC4Common.Common.Constants.AnswerType.MA) || list.AnswerType.Equals(QC4Common.Common.Constants.AnswerType.SA))
                            {
                                Newvariable10.minmaxavgborder.Visibility = Visibility.Hidden;
                                Newvariable10.minmaxavg.Visibility = Visibility.Hidden;
                                Newvariable10.List_OriginItem_ChoiceList.Visibility = Visibility.Visible;
                                Newvariable10.List_OriginItem_ChoiceList.Items.Clear();
                                foreach (string choice in list.Choices)
                                {
                                    slno++;
                                    Newvariable10.List_OriginItem_ChoiceList.Items.Add(string.Format("{0}: {1}", slno, frmutil.EscapeCRLF(choice)));
                                }
                            }
                            else
                            {
                                Newvariable10.Text_OriginItem1_SelectCount.Text = string.Empty;
                                Newvariable10.minmaxavgborder.Visibility = Visibility.Visible;
                                Newvariable10.minmaxavg.Visibility = Visibility.Visible;
                                Newvariable10.List_OriginItem_ChoiceList.Visibility = Visibility.Hidden;
                                //get min ,max,mean from DB
                                if (Util.Definiotion.VariableDictionary.ContainsKey(list.Variable) && !frmutil.IsNewQuestion(Util.Definiotion.VariableDictionary[list.Variable].QuestionFlag))
                                {
                                    frmutil.NtypeGetMinMaxAvg((Util.Definiotion.VariableDictionary[list.Variable].ItemId) == 0 ? "sample_id" : "q_" + (Util.Definiotion.VariableDictionary[list.Variable].ItemId).ToString(), workbook, out min, out max, out avg);
                                    Newvariable10.Text_OriginItem_Min.Text = min.ToString();
                                    Newvariable10.Text_OriginItem_Max.Text = max.ToString();
                                    Newvariable10.Text_OriginItem_Avg.Text = avg.ToString();
                                }
                            }
                            Newvariable10.Command_OriginItem_InputSupport.IsEnabled = true;
                            if (SourceVariableListView10[0].Choices != null)
                            {
                                SourceVariableListView10.Insert(0, NewvariableNullItem());
                            }
                            if (SourceVariableListView9[0].Choices == null)
                            {
                                SourceVariableListView9.RemoveAt(0);
                            }
                            // Newvariable10.Combo_OriginItem_Item.Text = list.Variable;
                        }
                        else
                        {
                            gridoriginalitem.Columns[sourcevariable - 1].IsReadOnly = true;
                            Newvariable10.Text_OriginItem1_AnswerType.Text = string.Empty;
                            Newvariable10.Text_OriginItem1_SelectCount.Text = string.Empty;
                            Newvariable10.Text_OriginItem1_Question.Text = string.Empty;
                            Newvariable10.minmaxavg.Visibility = Visibility.Hidden;
                            Newvariable10.List_OriginItem_ChoiceList.Visibility = Visibility.Visible;
                            Newvariable10.List_OriginItem_ChoiceList.Items.Clear();
                            Newvariable10.Command_OriginItem_InputSupport.IsEnabled = false;
                            Newvariable10.minmaxavgborder.Visibility = Visibility.Hidden;
                            if (SourceVariableListView9[0].Choices != null)
                            {
                                SourceVariableListView9.Insert(0, NewvariableNullItem());
                            }
                            gridsourcevariables = ClearColumn(sourcevariable - 1, gridsourcevariables.Rows.Count, gridsourcevariables);
                            Newvariable10.Combo_OriginItem_Item.Text = string.Empty;
                        }
                        break;
                }
                if (isLoaded)
                {
                    gridsourcevariables = ClearColumn(sourcevariable - 1, gridsourcevariables.Rows.Count, gridsourcevariables);
                }
            }
            else
            {
                if (sourcevariable != 1)
                {
                    sourceVariable.Text = string.Empty;
                    ClearSourceComboValues(sourcevariable);
                }
            }
        }

        #region Criteriabuttonclick
        private void Command_CriteriaValue1_Click(object sender, RoutedEventArgs e)
        {

            List<string> choices = new List<string>();
            DataTable newvariabledt = ((DataView)gridnewvariable.ItemsSource).ToTable();
            DataTable originapitemdt = ((DataView)gridoriginalitem.ItemsSource).ToTable();
            if (Newvariable1.Text_OriginItem1_AnswerType.Text.Equals(Util.Constants.AnswerType.SA) || Newvariable1.Text_OriginItem1_AnswerType.Text.Equals(Util.Constants.AnswerType.MA))
                foreach (string listitem in Newvariable1.List_OriginItem_ChoiceList.Items)
                {
                    choices.Add(listitem);
                }
            Integrate_Support_SourceVariable issv = new Integrate_Support_SourceVariable(
                Newvariable1.Label_OriginItem_OriginItem.Text, Newvariable1.Combo_OriginItem_Item.Text, Newvariable1.Text_OriginItem1_AnswerType.Text,
               choices, min, max, avg, Combo_NewItem_SelectCount.SelectedItem.ToString() == QC4Common.CommonResource.LBL_AUTO ? Qc4Launcher.Util.Constants.MaxChoiceCount : int.Parse(Combo_NewItem_SelectCount.SelectedValue.ToString()),
               newvariabledt.DefaultView.ToTable(false, "Choice"), originapitemdt.DefaultView.ToTable(false, "S1"));
            issv.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            issv.Owner = this;
            issv.ShowDialog();
            issv.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            if (Integrate_Support_SourceVariable.issave)
            {
                for (int i = 0; i < Integrate_Support_SourceVariable.gridchoicevalues.Rows.Count; i++)
                {
                    if (Newvariable1.Text_OriginItem1_AnswerType.Text.Equals(Util.Constants.AnswerType.SA) || Newvariable1.Text_OriginItem1_AnswerType.Text.Equals(Util.Constants.AnswerType.MA))
                    {
                        gridsourcevariables.Rows[i][0] = Convert.ToString(Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][3]) + Convert.ToString(Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][4]);
                    }
                    else
                    {

                        string lower = Convert.ToString(Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][5]);
                        string upper = Convert.ToString(Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][7]);
                        string not = string.Empty;
                        not = frmutil.GetOperator(lower);
                        lower = frmutil.TrimStartEqualNotequal(lower);
                        try { lower = frmutil.ReplaceBrackets(lower); } catch { }
                        try { upper = frmutil.ReplaceBrackets(upper); } catch { }
                        if (lower.Contains("-")) lower = "(" + lower + ")";
                        if (upper.Contains("-")) upper = "(" + upper + ")";
                        if (!string.IsNullOrEmpty(lower) || !string.IsNullOrEmpty(upper))
                        {
                            gridsourcevariables.Rows[i][0] = not + lower
                                                            + Qc4Launcher.LocalResource.GRID_LBL_INTEGRATE_SEPERATOR +
                                                            upper;
                        }
                    }
                    gridchoice.Rows[i][1] = Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][2];
                }
                gridoriginalitem.DataContext = gridsourcevariables;
                Integrate_Support_SourceVariable.issave = false;
            }
        }
        private void Command_CriteriaValue2_Click(object sender, RoutedEventArgs e)
        {
            List<string> choices = new List<string>();
            DataTable newvariabledt = ((DataView)gridnewvariable.ItemsSource).ToTable();
            DataTable originapitemdt = ((DataView)gridoriginalitem.ItemsSource).ToTable();
            if (Newvariable2.Text_OriginItem1_AnswerType.Text.Equals(Util.Constants.AnswerType.SA) || Newvariable2.Text_OriginItem1_AnswerType.Text.Equals(Util.Constants.AnswerType.MA))
                foreach (string listitem in Newvariable2.List_OriginItem_ChoiceList.Items)
                {
                    choices.Add(listitem);
                }
            Integrate_Support_SourceVariable issv = new Integrate_Support_SourceVariable(
                Newvariable2.Label_OriginItem_OriginItem.Text, Newvariable2.Combo_OriginItem_Item.Text, Newvariable2.Text_OriginItem1_AnswerType.Text,
               choices, min, max, avg, Combo_NewItem_SelectCount.SelectedItem.ToString() == QC4Common.CommonResource.LBL_AUTO ? Qc4Launcher.Util.Constants.MaxChoiceCount : int.Parse(Combo_NewItem_SelectCount.SelectedValue.ToString()),
               newvariabledt.DefaultView.ToTable(false, "Choice"), originapitemdt.DefaultView.ToTable(false, "S2"));
            issv.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            issv.Owner = this;
            issv.ShowDialog();
            issv.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            if (Integrate_Support_SourceVariable.issave)
            {
                for (int i = 0; i < Integrate_Support_SourceVariable.gridchoicevalues.Rows.Count; i++)
                {
                    if (Newvariable2.Text_OriginItem1_AnswerType.Text.Equals(Util.Constants.AnswerType.SA) || Newvariable2.Text_OriginItem1_AnswerType.Text.Equals(Util.Constants.AnswerType.MA))
                    {
                        gridsourcevariables.Rows[i][1] = Convert.ToString(Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][3]) + Convert.ToString(Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][4]);
                    }
                    else
                    {

                        string lower = Convert.ToString(Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][5]);
                        string upper = Convert.ToString(Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][7]);
                        string not = string.Empty;
                        not = frmutil.GetOperator(lower);
                        lower = frmutil.TrimStartEqualNotequal(lower);
                        try { lower = frmutil.ReplaceBrackets(lower); } catch { }
                        try { upper = frmutil.ReplaceBrackets(upper); } catch { }
                        if (lower.Contains("-")) lower = "(" + lower + ")";
                        if (upper.Contains("-")) upper = "(" + upper + ")";
                        if (!string.IsNullOrEmpty(lower) || !string.IsNullOrEmpty(upper))
                        {
                            gridsourcevariables.Rows[i][1] = not + lower
                                                            + Qc4Launcher.LocalResource.GRID_LBL_INTEGRATE_SEPERATOR +
                                                            upper;
                        }
                    }
                    gridchoice.Rows[i][1] = Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][2];
                }
                gridoriginalitem.DataContext = gridsourcevariables;
                Integrate_Support_SourceVariable.issave = false;
            }
        }
        private void Command_CriteriaValue3_Click(object sender, RoutedEventArgs e)
        {
            List<string> choices = new List<string>();
            DataTable newvariabledt = ((DataView)gridnewvariable.ItemsSource).ToTable();
            DataTable originapitemdt = ((DataView)gridoriginalitem.ItemsSource).ToTable();
            if (Newvariable3.Text_OriginItem1_AnswerType.Text.Equals(Util.Constants.AnswerType.SA) || Newvariable3.Text_OriginItem1_AnswerType.Text.Equals(Util.Constants.AnswerType.MA))
                foreach (string listitem in Newvariable3.List_OriginItem_ChoiceList.Items)
                {
                    choices.Add(listitem);
                }
            Integrate_Support_SourceVariable issv = new Integrate_Support_SourceVariable(
                Newvariable3.Label_OriginItem_OriginItem.Text, Newvariable3.Combo_OriginItem_Item.Text, Newvariable3.Text_OriginItem1_AnswerType.Text,
               choices, min, max, avg, Combo_NewItem_SelectCount.SelectedItem.ToString() == QC4Common.CommonResource.LBL_AUTO ? Qc4Launcher.Util.Constants.MaxChoiceCount : int.Parse(Combo_NewItem_SelectCount.SelectedValue.ToString()),
               newvariabledt.DefaultView.ToTable(false, "Choice"), originapitemdt.DefaultView.ToTable(false, "S3"));
            issv.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            issv.Owner = this;
            issv.ShowDialog();
            issv.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            if (Integrate_Support_SourceVariable.issave)
            {
                for (int i = 0; i < Integrate_Support_SourceVariable.gridchoicevalues.Rows.Count; i++)
                {
                    if (Newvariable3.Text_OriginItem1_AnswerType.Text.Equals(Util.Constants.AnswerType.SA) || Newvariable3.Text_OriginItem1_AnswerType.Text.Equals(Util.Constants.AnswerType.MA))
                    {
                        gridsourcevariables.Rows[i][2] = Convert.ToString(Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][3]) + Convert.ToString(Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][4]);
                    }
                    else
                    {

                        string lower = Convert.ToString(Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][5]);
                        string upper = Convert.ToString(Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][7]);
                        string not = string.Empty;
                        not = frmutil.GetOperator(lower);
                        lower = frmutil.TrimStartEqualNotequal(lower);
                        try { lower = frmutil.ReplaceBrackets(lower); } catch { }
                        try { upper = frmutil.ReplaceBrackets(upper); } catch { }
                        if (lower.Contains("-")) lower = "(" + lower + ")";
                        if (upper.Contains("-")) upper = "(" + upper + ")";
                        if (!string.IsNullOrEmpty(lower) || !string.IsNullOrEmpty(upper))
                        {
                            gridsourcevariables.Rows[i][2] = not + lower
                                                            + Qc4Launcher.LocalResource.GRID_LBL_INTEGRATE_SEPERATOR +
                                                            upper;
                        }
                    }
                    gridchoice.Rows[i][1] = Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][2];
                }
                gridoriginalitem.DataContext = gridsourcevariables;
                Integrate_Support_SourceVariable.issave = false;

            }
        }
        private void Command_CriteriaValue4_Click(object sender, RoutedEventArgs e)
        {
            List<string> choices = new List<string>();
            DataTable newvariabledt = ((DataView)gridnewvariable.ItemsSource).ToTable();
            DataTable originapitemdt = ((DataView)gridoriginalitem.ItemsSource).ToTable();
            if (Newvariable4.Text_OriginItem1_AnswerType.Text.Equals(Util.Constants.AnswerType.SA) || Newvariable4.Text_OriginItem1_AnswerType.Text.Equals(Util.Constants.AnswerType.MA))
                foreach (string listitem in Newvariable4.List_OriginItem_ChoiceList.Items)
                {
                    choices.Add(listitem);
                }
            Integrate_Support_SourceVariable issv = new Integrate_Support_SourceVariable(
                Newvariable4.Label_OriginItem_OriginItem.Text, Newvariable4.Combo_OriginItem_Item.Text, Newvariable4.Text_OriginItem1_AnswerType.Text,
               choices, min, max, avg, Combo_NewItem_SelectCount.SelectedItem.ToString() == QC4Common.CommonResource.LBL_AUTO ? Qc4Launcher.Util.Constants.MaxChoiceCount : int.Parse(Combo_NewItem_SelectCount.SelectedValue.ToString()),
               newvariabledt.DefaultView.ToTable(false, "Choice"), originapitemdt.DefaultView.ToTable(false, "S4"));
            issv.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            issv.Owner = this;
            issv.ShowDialog();
            issv.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            if (Integrate_Support_SourceVariable.issave)
            {
                for (int i = 0; i < Integrate_Support_SourceVariable.gridchoicevalues.Rows.Count; i++)
                {
                    if (Newvariable4.Text_OriginItem1_AnswerType.Text.Equals(Util.Constants.AnswerType.SA) || Newvariable4.Text_OriginItem1_AnswerType.Text.Equals(Util.Constants.AnswerType.MA))
                    {
                        gridsourcevariables.Rows[i][3] = Convert.ToString(Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][3]) + Convert.ToString(Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][4]);
                    }
                    else
                    {

                        string lower = Convert.ToString(Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][5]);
                        string upper = Convert.ToString(Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][7]);
                        string not = string.Empty;
                        not = frmutil.GetOperator(lower);
                        lower = frmutil.TrimStartEqualNotequal(lower);
                        try { lower = frmutil.ReplaceBrackets(lower); } catch { }
                        try { upper = frmutil.ReplaceBrackets(upper); } catch { }
                        if (lower.Contains("-")) lower = "(" + lower + ")";
                        if (upper.Contains("-")) upper = "(" + upper + ")";
                        if (!string.IsNullOrEmpty(lower) || !string.IsNullOrEmpty(upper))
                        {
                            gridsourcevariables.Rows[i][3] = not + lower
                                                            + Qc4Launcher.LocalResource.GRID_LBL_INTEGRATE_SEPERATOR +
                                                            upper;
                        }
                    }
                    gridchoice.Rows[i][1] = Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][2];
                }
                gridoriginalitem.DataContext = gridsourcevariables;
                Integrate_Support_SourceVariable.issave = false;
                gridoriginalitem.Items.Refresh();
            }
        }
        private void Command_CriteriaValue5_Click(object sender, RoutedEventArgs e)
        {
            List<string> choices = new List<string>();
            DataTable newvariabledt = ((DataView)gridnewvariable.ItemsSource).ToTable();
            DataTable originapitemdt = ((DataView)gridoriginalitem.ItemsSource).ToTable();
            if (Newvariable5.Text_OriginItem1_AnswerType.Text.Equals(Util.Constants.AnswerType.SA) || Newvariable5.Text_OriginItem1_AnswerType.Text.Equals(Util.Constants.AnswerType.MA))
                foreach (string listitem in Newvariable5.List_OriginItem_ChoiceList.Items)
                {
                    choices.Add(listitem);
                }
            Integrate_Support_SourceVariable issv = new Integrate_Support_SourceVariable(
                Newvariable5.Label_OriginItem_OriginItem.Text, Newvariable5.Combo_OriginItem_Item.Text, Newvariable5.Text_OriginItem1_AnswerType.Text,
               choices, min, max, avg, Combo_NewItem_SelectCount.SelectedItem.ToString() == QC4Common.CommonResource.LBL_AUTO ? Qc4Launcher.Util.Constants.MaxChoiceCount : int.Parse(Combo_NewItem_SelectCount.SelectedValue.ToString()),
               newvariabledt.DefaultView.ToTable(false, "Choice"), originapitemdt.DefaultView.ToTable(false, "S5"));
            issv.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            issv.Owner = this;
            issv.ShowDialog();
            issv.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            if (Integrate_Support_SourceVariable.issave)
            {
                for (int i = 0; i < Integrate_Support_SourceVariable.gridchoicevalues.Rows.Count; i++)
                {
                    if (Newvariable5.Text_OriginItem1_AnswerType.Text.Equals(Util.Constants.AnswerType.SA) || Newvariable5.Text_OriginItem1_AnswerType.Text.Equals(Util.Constants.AnswerType.MA))
                    {
                        gridsourcevariables.Rows[i][4] = Convert.ToString(Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][3]) + Convert.ToString(Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][4]);
                    }
                    else
                    {

                        string lower = Convert.ToString(Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][5]);
                        string upper = Convert.ToString(Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][7]);
                        string not = string.Empty;
                        not = frmutil.GetOperator(lower);
                        lower = frmutil.TrimStartEqualNotequal(lower);
                        try { lower = frmutil.ReplaceBrackets(lower); } catch { }
                        try { upper = frmutil.ReplaceBrackets(upper); } catch { }
                        if (lower.Contains("-")) lower = "(" + lower + ")";
                        if (upper.Contains("-")) upper = "(" + upper + ")";
                        if (!string.IsNullOrEmpty(lower) || !string.IsNullOrEmpty(upper))
                        {
                            gridsourcevariables.Rows[i][4] = not + lower
                                                            + Qc4Launcher.LocalResource.GRID_LBL_INTEGRATE_SEPERATOR +
                                                            upper;
                        }
                    }
                    gridchoice.Rows[i][1] = Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][2];
                }
                gridoriginalitem.DataContext = gridsourcevariables;
                Integrate_Support_SourceVariable.issave = false;
            }
        }
        private void Command_CriteriaValue6_Click(object sender, RoutedEventArgs e)
        {
            List<string> choices = new List<string>();
            DataTable newvariabledt = ((DataView)gridnewvariable.ItemsSource).ToTable();
            DataTable originapitemdt = ((DataView)gridoriginalitem.ItemsSource).ToTable();
            if (Newvariable6.Text_OriginItem1_AnswerType.Text.Equals(Util.Constants.AnswerType.SA) || Newvariable6.Text_OriginItem1_AnswerType.Text.Equals(Util.Constants.AnswerType.MA))
                foreach (string listitem in Newvariable6.List_OriginItem_ChoiceList.Items)
                {
                    choices.Add(listitem);
                }
            Integrate_Support_SourceVariable issv = new Integrate_Support_SourceVariable(
                Newvariable6.Label_OriginItem_OriginItem.Text, Newvariable6.Combo_OriginItem_Item.Text, Newvariable6.Text_OriginItem1_AnswerType.Text,
               choices, min, max, avg, Combo_NewItem_SelectCount.SelectedItem.ToString() == QC4Common.CommonResource.LBL_AUTO ? Qc4Launcher.Util.Constants.MaxChoiceCount : int.Parse(Combo_NewItem_SelectCount.SelectedValue.ToString()),
               newvariabledt.DefaultView.ToTable(false, "Choice"), originapitemdt.DefaultView.ToTable(false, "S6"));
            issv.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            issv.Owner = this;
            issv.ShowDialog();
            issv.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            if (Integrate_Support_SourceVariable.issave)
            {
                for (int i = 0; i < Integrate_Support_SourceVariable.gridchoicevalues.Rows.Count; i++)
                {
                    if (Newvariable6.Text_OriginItem1_AnswerType.Text.Equals(Util.Constants.AnswerType.SA) || Newvariable6.Text_OriginItem1_AnswerType.Text.Equals(Util.Constants.AnswerType.MA))
                    {
                        gridsourcevariables.Rows[i][5] = Convert.ToString(Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][3]) + Convert.ToString(Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][4]);
                    }
                    else
                    {

                        string lower = Convert.ToString(Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][5]);
                        string upper = Convert.ToString(Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][7]);
                        string not = string.Empty;
                        not = frmutil.GetOperator(lower);
                        lower = frmutil.TrimStartEqualNotequal(lower);
                        try { lower = frmutil.ReplaceBrackets(lower); } catch { }
                        try { upper = frmutil.ReplaceBrackets(upper); } catch { }
                        if (lower.Contains("-")) lower = "(" + lower + ")";
                        if (upper.Contains("-")) upper = "(" + upper + ")";
                        if (!string.IsNullOrEmpty(lower) || !string.IsNullOrEmpty(upper))
                        {
                            gridsourcevariables.Rows[i][5] = not + lower
                                                            + Qc4Launcher.LocalResource.GRID_LBL_INTEGRATE_SEPERATOR +
                                                            upper;
                        }
                    }
                    gridchoice.Rows[i][1] = Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][2];
                }
                gridoriginalitem.DataContext = gridsourcevariables;
                Integrate_Support_SourceVariable.issave = false;
            }
        }
        private void Command_CriteriaValue7_Click(object sender, RoutedEventArgs e)
        {
            List<string> choices = new List<string>();
            DataTable newvariabledt = ((DataView)gridnewvariable.ItemsSource).ToTable();
            DataTable originapitemdt = ((DataView)gridoriginalitem.ItemsSource).ToTable();
            if (Newvariable7.Text_OriginItem1_AnswerType.Text.Equals(Util.Constants.AnswerType.SA) || Newvariable7.Text_OriginItem1_AnswerType.Text.Equals(Util.Constants.AnswerType.MA))
                foreach (string listitem in Newvariable7.List_OriginItem_ChoiceList.Items)
                {
                    choices.Add(listitem);
                }
            Integrate_Support_SourceVariable issv = new Integrate_Support_SourceVariable(
                Newvariable7.Label_OriginItem_OriginItem.Text, Newvariable7.Combo_OriginItem_Item.Text, Newvariable7.Text_OriginItem1_AnswerType.Text,
               choices, min, max, avg, Combo_NewItem_SelectCount.SelectedItem.ToString() == QC4Common.CommonResource.LBL_AUTO ? Qc4Launcher.Util.Constants.MaxChoiceCount : int.Parse(Combo_NewItem_SelectCount.SelectedValue.ToString()),
               newvariabledt.DefaultView.ToTable(false, "Choice"), originapitemdt.DefaultView.ToTable(false, "S7"));
            issv.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            issv.Owner = this;
            issv.ShowDialog();
            issv.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            if (Integrate_Support_SourceVariable.issave)
            {
                for (int i = 0; i < Integrate_Support_SourceVariable.gridchoicevalues.Rows.Count; i++)
                {
                    if (Newvariable7.Text_OriginItem1_AnswerType.Text.Equals(Util.Constants.AnswerType.SA) || Newvariable7.Text_OriginItem1_AnswerType.Text.Equals(Util.Constants.AnswerType.MA))
                    {
                        gridsourcevariables.Rows[i][6] = Convert.ToString(Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][3]) + Convert.ToString(Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][4]);
                    }
                    else
                    {

                        string lower = Convert.ToString(Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][5]);
                        string upper = Convert.ToString(Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][7]);
                        string not = string.Empty;
                        not = frmutil.GetOperator(lower);
                        lower = frmutil.TrimStartEqualNotequal(lower);
                        try { lower = frmutil.ReplaceBrackets(lower); } catch { }
                        try { upper = frmutil.ReplaceBrackets(upper); } catch { }
                        if (lower.Contains("-")) lower = "(" + lower + ")";
                        if (upper.Contains("-")) upper = "(" + upper + ")";
                        if (!string.IsNullOrEmpty(lower) || !string.IsNullOrEmpty(upper))
                        {
                            gridsourcevariables.Rows[i][6] = not + lower
                                                            + Qc4Launcher.LocalResource.GRID_LBL_INTEGRATE_SEPERATOR +
                                                            upper;
                        }
                    }
                    gridchoice.Rows[i][1] = Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][2];
                }
                gridoriginalitem.DataContext = gridsourcevariables;
                Integrate_Support_SourceVariable.issave = false;
            }
        }
        private void Command_CriteriaValue8_Click(object sender, RoutedEventArgs e)
        {
            List<string> choices = new List<string>();
            DataTable newvariabledt = ((DataView)gridnewvariable.ItemsSource).ToTable();
            DataTable originapitemdt = ((DataView)gridoriginalitem.ItemsSource).ToTable();
            if (Newvariable8.Text_OriginItem1_AnswerType.Text.Equals(Util.Constants.AnswerType.SA) || Newvariable8.Text_OriginItem1_AnswerType.Text.Equals(Util.Constants.AnswerType.MA))
                foreach (string listitem in Newvariable8.List_OriginItem_ChoiceList.Items)
                {
                    choices.Add(listitem);
                }
            Integrate_Support_SourceVariable issv = new Integrate_Support_SourceVariable(
                Newvariable8.Label_OriginItem_OriginItem.Text, Newvariable8.Combo_OriginItem_Item.Text, Newvariable8.Text_OriginItem1_AnswerType.Text,
               choices, min, max, avg, Combo_NewItem_SelectCount.SelectedItem.ToString() == QC4Common.CommonResource.LBL_AUTO ? Qc4Launcher.Util.Constants.MaxChoiceCount : int.Parse(Combo_NewItem_SelectCount.SelectedValue.ToString()),
               newvariabledt.DefaultView.ToTable(false, "Choice"), originapitemdt.DefaultView.ToTable(false, "S8"));
            issv.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            issv.Owner = this;
            issv.ShowDialog();
            issv.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            if (Integrate_Support_SourceVariable.issave)
            {
                for (int i = 0; i < Integrate_Support_SourceVariable.gridchoicevalues.Rows.Count; i++)
                {
                    if (Newvariable8.Text_OriginItem1_AnswerType.Text.Equals(Util.Constants.AnswerType.SA) || Newvariable8.Text_OriginItem1_AnswerType.Text.Equals(Util.Constants.AnswerType.MA))
                    {
                        gridsourcevariables.Rows[i][7] = Convert.ToString(Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][3]) + Convert.ToString(Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][4]);
                    }
                    else
                    {

                        string lower = Convert.ToString(Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][5]);
                        string upper = Convert.ToString(Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][7]);
                        string not = string.Empty;
                        not = frmutil.GetOperator(lower);
                        lower = frmutil.TrimStartEqualNotequal(lower);
                        try { lower = frmutil.ReplaceBrackets(lower); } catch { }
                        try { upper = frmutil.ReplaceBrackets(upper); } catch { }
                        if (lower.Contains("-")) lower = "(" + lower + ")";
                        if (upper.Contains("-")) upper = "(" + upper + ")";
                        if (!string.IsNullOrEmpty(lower) || !string.IsNullOrEmpty(upper))
                        {
                            gridsourcevariables.Rows[i][7] = not + lower
                                                            + Qc4Launcher.LocalResource.GRID_LBL_INTEGRATE_SEPERATOR +
                                                            upper;
                        }
                    }
                    gridchoice.Rows[i][1] = Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][2];
                }
                gridoriginalitem.DataContext = gridsourcevariables;
                Integrate_Support_SourceVariable.issave = false;
            }
        }
        private void Command_CriteriaValue9_Click(object sender, RoutedEventArgs e)
        {
            List<string> choices = new List<string>();
            DataTable newvariabledt = ((DataView)gridnewvariable.ItemsSource).ToTable();
            DataTable originapitemdt = ((DataView)gridoriginalitem.ItemsSource).ToTable();
            if (Newvariable9.Text_OriginItem1_AnswerType.Text.Equals(Util.Constants.AnswerType.SA) || Newvariable9.Text_OriginItem1_AnswerType.Text.Equals(Util.Constants.AnswerType.MA))
                foreach (string listitem in Newvariable9.List_OriginItem_ChoiceList.Items)
                {
                    choices.Add(listitem);
                }
            Integrate_Support_SourceVariable issv = new Integrate_Support_SourceVariable(
                Newvariable9.Label_OriginItem_OriginItem.Text, Newvariable9.Combo_OriginItem_Item.Text, Newvariable9.Text_OriginItem1_AnswerType.Text,
               choices, min, max, avg, Combo_NewItem_SelectCount.SelectedItem.ToString() == QC4Common.CommonResource.LBL_AUTO ? Qc4Launcher.Util.Constants.MaxChoiceCount : int.Parse(Combo_NewItem_SelectCount.SelectedValue.ToString()),
               newvariabledt.DefaultView.ToTable(false, "Choice"), originapitemdt.DefaultView.ToTable(false, "S9"));
            issv.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            issv.Owner = this;
            issv.ShowDialog();
            issv.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            if (Integrate_Support_SourceVariable.issave)
            {
                for (int i = 0; i < Integrate_Support_SourceVariable.gridchoicevalues.Rows.Count; i++)
                {
                    if (Newvariable9.Text_OriginItem1_AnswerType.Text.Equals(Util.Constants.AnswerType.SA) || Newvariable9.Text_OriginItem1_AnswerType.Text.Equals(Util.Constants.AnswerType.MA))
                    {
                        gridsourcevariables.Rows[i][8] = Convert.ToString(Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][3]) + Convert.ToString(Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][4]);
                    }
                    else
                    {

                        string lower = Convert.ToString(Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][5]);
                        string upper = Convert.ToString(Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][7]);
                        string not = string.Empty;
                        not = frmutil.GetOperator(lower);
                        lower = frmutil.TrimStartEqualNotequal(lower);
                        try { lower = frmutil.ReplaceBrackets(lower); } catch { }
                        try { upper = frmutil.ReplaceBrackets(upper); } catch { }
                        if (lower.Contains("-")) lower = "(" + lower + ")";
                        if (upper.Contains("-")) upper = "(" + upper + ")";
                        if (!string.IsNullOrEmpty(lower) || !string.IsNullOrEmpty(upper))
                        {
                            gridsourcevariables.Rows[i][8] = not + lower
                                                            + Qc4Launcher.LocalResource.GRID_LBL_INTEGRATE_SEPERATOR +
                                                            upper;
                        }
                    }
                    gridchoice.Rows[i][1] = Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][2];
                }
                gridoriginalitem.DataContext = gridsourcevariables;
                Integrate_Support_SourceVariable.issave = false;
            }
        }
        private void Command_CriteriaValue10_Click(object sender, RoutedEventArgs e)
        {
            List<string> choices = new List<string>();
            DataTable newvariabledt = ((DataView)gridnewvariable.ItemsSource).ToTable();
            DataTable originapitemdt = ((DataView)gridoriginalitem.ItemsSource).ToTable();
            if (Newvariable10.Text_OriginItem1_AnswerType.Text.Equals(Util.Constants.AnswerType.SA) || Newvariable10.Text_OriginItem1_AnswerType.Text.Equals(Util.Constants.AnswerType.MA))
                foreach (string listitem in Newvariable10.List_OriginItem_ChoiceList.Items)
                {
                    choices.Add(listitem);
                }
            Integrate_Support_SourceVariable issv = new Integrate_Support_SourceVariable(
                Newvariable10.Label_OriginItem_OriginItem.Text, Newvariable10.Combo_OriginItem_Item.Text, Newvariable10.Text_OriginItem1_AnswerType.Text,
               choices, min, max, avg, Combo_NewItem_SelectCount.SelectedItem.ToString() == QC4Common.CommonResource.LBL_AUTO ? Qc4Launcher.Util.Constants.MaxChoiceCount : int.Parse(Combo_NewItem_SelectCount.SelectedValue.ToString()),
               newvariabledt.DefaultView.ToTable(false, "Choice"), originapitemdt.DefaultView.ToTable(false, "S10"));
            issv.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            issv.Owner = this;
            issv.ShowDialog();
            issv.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            if (Integrate_Support_SourceVariable.issave)
            {
                for (int i = 0; i < Integrate_Support_SourceVariable.gridchoicevalues.Rows.Count; i++)
                {
                    if (Newvariable10.Text_OriginItem1_AnswerType.Text.Equals(Util.Constants.AnswerType.SA) || Newvariable10.Text_OriginItem1_AnswerType.Text.Equals(Util.Constants.AnswerType.MA))
                    {
                        gridsourcevariables.Rows[i][9] = Convert.ToString(Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][3]) + Convert.ToString(Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][4]);
                    }
                    else
                    {

                        string lower = Convert.ToString(Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][5]);
                        string upper = Convert.ToString(Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][7]);
                        string not = string.Empty;
                        not = frmutil.GetOperator(lower);
                        lower = frmutil.TrimStartEqualNotequal(lower);
                        try { lower = frmutil.ReplaceBrackets(lower); } catch { }
                        try { upper = frmutil.ReplaceBrackets(upper); } catch { }
                        if (lower.Contains("-")) lower = "(" + lower + ")";
                        if (upper.Contains("-")) upper = "(" + upper + ")";
                        if (!string.IsNullOrEmpty(lower) || !string.IsNullOrEmpty(upper))
                        {
                            gridsourcevariables.Rows[i][9] = not + lower
                                                            + Qc4Launcher.LocalResource.GRID_LBL_INTEGRATE_SEPERATOR +
                                                            upper;
                        }
                    }
                    gridchoice.Rows[i][1] = Integrate_Support_SourceVariable.gridchoicevalues.Rows[i][2];
                }
                gridoriginalitem.DataContext = gridsourcevariables;
                Integrate_Support_SourceVariable.issave = false;
            }
        }
        #endregion  Criteriabuttonclick


        #region Observablecollection
        //
        public static List<string> MASAvariables = new List<string>();
        private Dictionary<String, QuestionSettings> PopulatedDictionary = new Dictionary<string, QuestionSettings>();
        private ObservableCollection<SourceVariableList> sourceVariableList1 = new ObservableCollection<SourceVariableList>();
        private ObservableCollection<SourceVariableList> sourceVariableList2 = new ObservableCollection<SourceVariableList>();
        private ObservableCollection<SourceVariableList> sourceVariableList3 = new ObservableCollection<SourceVariableList>();
        private ObservableCollection<SourceVariableList> sourceVariableList4 = new ObservableCollection<SourceVariableList>();
        private ObservableCollection<SourceVariableList> sourceVariableList5 = new ObservableCollection<SourceVariableList>();
        private ObservableCollection<SourceVariableList> sourceVariableList6 = new ObservableCollection<SourceVariableList>();
        private ObservableCollection<SourceVariableList> sourceVariableList7 = new ObservableCollection<SourceVariableList>();
        private ObservableCollection<SourceVariableList> sourceVariableList8 = new ObservableCollection<SourceVariableList>();
        private ObservableCollection<SourceVariableList> sourceVariableList9 = new ObservableCollection<SourceVariableList>();
        private ObservableCollection<SourceVariableList> sourceVariableList10 = new ObservableCollection<SourceVariableList>();
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
        public ObservableCollection<SourceVariableList> SourceVariableListView6
        {
            get
            {
                return sourceVariableList6;
            }
            set
            {
                sourceVariableList6 = value;
            }
        }
        public ObservableCollection<SourceVariableList> SourceVariableListView7
        {
            get
            {
                return sourceVariableList7;
            }
            set
            {
                sourceVariableList7 = value;
            }
        }
        public ObservableCollection<SourceVariableList> SourceVariableListView8
        {
            get
            {
                return sourceVariableList8;
            }
            set
            {
                sourceVariableList8 = value;
            }
        }
        public ObservableCollection<SourceVariableList> SourceVariableListView9
        {
            get
            {
                return sourceVariableList9;
            }
            set
            {
                sourceVariableList9 = value;
            }
        }
        public ObservableCollection<SourceVariableList> SourceVariableListView10
        {
            get
            {
                return sourceVariableList10;
            }
            set
            {
                sourceVariableList10 = value;
            }
        }
        #endregion Observablecollection
        private void PopulateMASAVariableList()
        {
            var SettingSheet = ExcelUtil.GetWorkSheetByCodeName(workbook, "Sheet91");
            PopulatedDictionary = Util.Definiotion.VariableDictionary;

            if (SettingSheet != null)
            {
                Microsoft.Office.Interop.Excel.Range Range = SettingSheet.get_Range("List_Item_SAMAN");
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
                                            Choices = qs.Choices
                                        });
                                        sourceVariableList2.Add(new SourceVariableList()
                                        {
                                            Variable = frmutil.EscapeCRLF(qs.Variable),
                                            AnswerType = qs.AnswerType,
                                            Question = frmutil.EscapeCRLF(qs.Question),
                                            Choices = qs.Choices
                                        });
                                        sourceVariableList3.Add(new SourceVariableList()
                                        {
                                            Variable = frmutil.EscapeCRLF(qs.Variable),
                                            AnswerType = qs.AnswerType,
                                            Question = frmutil.EscapeCRLF(qs.Question),
                                            Choices = qs.Choices
                                        });
                                        sourceVariableList4.Add(new SourceVariableList()
                                        {
                                            Variable = frmutil.EscapeCRLF(qs.Variable),
                                            AnswerType = qs.AnswerType,
                                            Question = frmutil.EscapeCRLF(qs.Question),
                                            Choices = qs.Choices
                                        });
                                        sourceVariableList5.Add(new SourceVariableList()
                                        {
                                            Variable = frmutil.EscapeCRLF(qs.Variable),
                                            AnswerType = qs.AnswerType,
                                            Question = frmutil.EscapeCRLF(qs.Question),
                                            Choices = qs.Choices
                                        });
                                        sourceVariableList6.Add(new SourceVariableList()
                                        {
                                            Variable = frmutil.EscapeCRLF(qs.Variable),
                                            AnswerType = qs.AnswerType,
                                            Question = frmutil.EscapeCRLF(qs.Question),
                                            Choices = qs.Choices
                                        });
                                        sourceVariableList7.Add(new SourceVariableList()
                                        {
                                            Variable = frmutil.EscapeCRLF(qs.Variable),
                                            AnswerType = qs.AnswerType,
                                            Question = frmutil.EscapeCRLF(qs.Question),
                                            Choices = qs.Choices
                                        });
                                        sourceVariableList8.Add(new SourceVariableList()
                                        {
                                            Variable = frmutil.EscapeCRLF(qs.Variable),
                                            AnswerType = qs.AnswerType,
                                            Question = frmutil.EscapeCRLF(qs.Question),
                                            Choices = qs.Choices
                                        });
                                        sourceVariableList9.Add(new SourceVariableList()
                                        {
                                            Variable = frmutil.EscapeCRLF(qs.Variable),
                                            AnswerType = qs.AnswerType,
                                            Question = frmutil.EscapeCRLF(qs.Question),
                                            Choices = qs.Choices
                                        });
                                        sourceVariableList10.Add(new SourceVariableList()
                                        {
                                            Variable = frmutil.EscapeCRLF(qs.Variable),
                                            AnswerType = qs.AnswerType,
                                            Question = frmutil.EscapeCRLF(qs.Question),
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
            private List<string> choices;
            private List<string> aswerTypes;

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
        private void LoadSAMAToCombo()
        {
            Combo_NewItem_AnswerType.Items.Add(Util.Constants.AnswerType.MA);
            Combo_NewItem_AnswerType.Items.Add(Util.Constants.AnswerType.SA);
            Combo_NewItem_AnswerType.SelectedIndex = 0;
        }
        private DataTable CreateChoiceTable()
        {
            DataTable griddata = new DataTable();
            griddata.Columns.Add("SL");
            griddata.Columns.Add("Choice");
            griddata.Columns.Add("IsBlank");
            return griddata;
        }
        private DataTable CreateSourceVariableTable()
        {
            DataTable griddata = new DataTable();
            griddata.Columns.Add("S1");
            griddata.Columns.Add("s2");
            griddata.Columns.Add("s3");
            griddata.Columns.Add("s4");
            griddata.Columns.Add("s5");
            griddata.Columns.Add("s6");
            griddata.Columns.Add("s7");
            griddata.Columns.Add("s8");
            griddata.Columns.Add("s9");
            griddata.Columns.Add("s10");
            return griddata;
        }
        private void LoadGridValues(int rowcount, List<string> choices)
        {

            gridchoice = CreateChoiceTable();
            gridsourcevariables = CreateSourceVariableTable();
            DataRow drchoice;
            DataRow drsourcevariable;
            for (int i = 0; i < rowcount; i++)
            {

                drchoice = gridchoice.NewRow();
                drsourcevariable = gridsourcevariables.NewRow();
                try
                {
                    drchoice["SL"] = (i + 1).ToString();
                    drchoice["Choice"] = string.Empty;
                    drchoice["IsBlank"] = false;
                    gridchoice.Rows.Add(drchoice);

                    drsourcevariable["S1"] = string.Empty;
                    drsourcevariable["s2"] = string.Empty;
                    drsourcevariable["s3"] = string.Empty;
                    drsourcevariable["s4"] = string.Empty;
                    drsourcevariable["s5"] = string.Empty;
                    drsourcevariable["s6"] = string.Empty;
                    drsourcevariable["s7"] = string.Empty;
                    drsourcevariable["s8"] = string.Empty;
                    drsourcevariable["s9"] = string.Empty;
                    drsourcevariable["S10"] = string.Empty;
                    gridsourcevariables.Rows.Add(drsourcevariable);

                }
                catch (Exception ex)
                {
                    _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                }
            }
            gridnewvariable.DataContext = gridchoice;
            gridoriginalitem.DataContext = gridsourcevariables;

        }
        protected void gridcriteriavalues_Scroll(System.Windows.Controls.DataGrid dg, object sender, System.Windows.Controls.Primitives.ScrollEventArgs e, int row)
        {

        }
        protected void gridnewvariable_Scroll(System.Windows.Controls.DataGrid dg, object sender, System.Windows.Controls.Primitives.ScrollEventArgs e, int row)
        {

        }

        private void Combo_NewItem_SelectCount_Selection_Change(object sender, SelectionChangedEventArgs e)
        {
            System.Windows.Controls.ComboBox combo = (System.Windows.Controls.ComboBox)sender;
            int rowcount = 0;

            if (Combo_NewItem_SelectCount.SelectedIndex == 0)
            {
                rowcount = Qc4Launcher.Util.Constants.MaxChoiceCount; ;
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

        private void gridnewvariable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }

        private DataTable ClearColumn(int column, int rowcount, DataTable dt)
        {
            for (int i = 0; i < rowcount; i++)
            {
                dt.Rows[i][column] = string.Empty;
            }
            return dt;

        }

        private void ChangeRowCount(int rownum)
        {

            try
            {
                int rowcount = gridchoice == null ? Qc4Launcher.Util.Constants.MaxChoiceCount : gridchoice.Rows.Count;
                if (rownum <= rowcount)
                {
                    for (int i = rowcount - 1; i >= rownum; i--)
                    {
                        gridchoice.Rows.RemoveAt(i);
                        gridsourcevariables.Rows.RemoveAt(i);
                        // gridchoice.Rows[i][2] = Visibility.Collapsed;

                    }
                }
                else
                {
                    DataRow drchoice;
                    DataRow drsourcevariable;
                    for (int i = 0; i < rownum - rowcount; i++)
                    {

                        drchoice = gridchoice.NewRow();
                        drsourcevariable = gridsourcevariables.NewRow();
                        try
                        {
                            drchoice["SL"] = (i + rowcount + 1).ToString();
                            drchoice["Choice"] = string.Empty;
                            gridchoice.Rows.Add(drchoice);

                            drsourcevariable["S1"] = string.Empty;
                            drsourcevariable["s2"] = string.Empty;
                            drsourcevariable["s3"] = string.Empty;
                            drsourcevariable["s4"] = string.Empty;
                            drsourcevariable["s5"] = string.Empty;
                            drsourcevariable["s6"] = string.Empty;
                            drsourcevariable["s7"] = string.Empty;
                            drsourcevariable["s8"] = string.Empty;
                            drsourcevariable["s9"] = string.Empty;
                            drsourcevariable["S10"] = string.Empty;
                            gridsourcevariables.Rows.Add(drsourcevariable);

                        }
                        catch (Exception ex) { }
                    }
                }

                gridnewvariable.DataContext = gridchoice;
                gridoriginalitem.DataContext = gridsourcevariables;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void Command_Entry_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool isnewquestion = true;
                bool isatleastonevalue = false;
                string newvariable = Combo_NewItem_Item.Text;
                newvariable = newvariable.TrimStart().TrimEnd();
                try { Text_NewItem_Question.Text = Text_NewItem_Question.Text.TrimStart().TrimEnd(); } catch { }
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
                            newvariable = Newvariable1.Combo_OriginItem_Item.Text;
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

                //check new variable already exists
                string[] Sourcevariablenames = GetSourceVariables();
                if (string.IsNullOrEmpty(Text_NewItem_Question.Text))
                {
                    MessageDialog.ErrorOk(LocalResource.ADDQS_ERROR_MSG_QUSTION_TEXT_MISS);
                    return;
                }
                if (Text_NewItem_Question.Text.Length > QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit)
                {
                    Text_NewItem_Question.Text = Text_NewItem_Question.Text.PadRight(QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit).Substring(0, QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit);

                }
                //validation here
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
                if ((choicegridcount == -1 && Combo_NewItem_SelectCount.SelectedIndex > 0) || (choicegridcount < Combo_NewItem_SelectCount.SelectedIndex))//((choicegridcount == -1 && Combo_NewItem_SelectCount.SelectedIndex > 0) )//((choicegridcount == -1 && Combo_NewItem_SelectCount.SelectedIndex > 0) || (choicegridcount < Combo_NewItem_SelectCount.SelectedIndex))
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
                    return;
                }
                int[] sourcevariablepos = GetSourceVariablepositions();

                int maxlimit = choicegridcount;
                if ((Combo_NewItem_SelectCount.SelectedIndex == 0) || (choicegridcount < sourcevariablepos[0] || choicegridcount < sourcevariablepos[1] || choicegridcount < sourcevariablepos[2]
                    || choicegridcount < sourcevariablepos[3] || choicegridcount < sourcevariablepos[4] || choicegridcount < sourcevariablepos[5] || choicegridcount < sourcevariablepos[6]
                    || choicegridcount < sourcevariablepos[7] || choicegridcount < sourcevariablepos[8] || choicegridcount < sourcevariablepos[9]))
                {
                    // maxlimit = sourcevariablepos[0];
                    for (int i = 0; i < 10; i++)
                    {
                        if (maxlimit < sourcevariablepos[i]) { maxlimit = sourcevariablepos[i]; }
                    }
                    // maxlimit = (maxlimit < choicegridcount) ? choicegridcount : maxlimit;



                }
                for (int i = 0; i < maxlimit; i++)
                {
                    if (string.IsNullOrEmpty(Convert.ToString(gridchoice.Rows[i][1])) || string.IsNullOrEmpty(Convert.ToString(gridchoice.Rows[i][1]).TrimStart().TrimEnd()))
                    {
                        //choice less than source variable length --auto
                        //New variable: input of [1] is invalid. [choice] is not input.
                        // ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT
                        MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT_, LocalResource.LABEL_CHOICE));
                        // frmutil.SetErrorForGrid(gridnewvariable, i, 1, QC4Common.Common.Constants.STD_DP.Background);
                        gridchoice.Rows[i][2] = true;
                        return;
                    }
                    else
                    {
                        gridchoice.Rows[i][2] = false;
                        //frmutil.SetErrorForGrid(gridnewvariable, i, 1, QC4Common.Common.Constants.STD_DP.Background, true);
                    }
                    if (Convert.ToString(gridchoice.Rows[i][1]).Length > QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit)
                    {
                        gridchoice.Rows[i][1] = gridchoice.Rows[i][1].ToString().PadRight(QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit).Substring(0, QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit);
                        // MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_MAX_CHAR_LIMIT_EXCEEDS, QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit));
                        // MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + string.Format(LocalResource.ERR_MSG_MAX_CHAR_LIMIT_EXCEEDS, QC4Common.Common.Constants.STD_DP.ExcelCellMaxCharLimit));
                        // frmutil.SetErrorForGrid(gridnewvariable, i, 1, QC4Common.Common.Constants.STD_DP.Foreground);
                        // return;
                    }
                    //else //if (lasterrorrow == i)
                    //{
                    //   // frmutil.SetErrorForGrid(gridnewvariable, i, 1, QC4Common.Common.Constants.STD_DP.Foreground, true);
                    //}
                }
                if (!GridVAlueVAlidation(Sourcevariablenames, sourcevariablepos, maxlimit))
                {
                    return;
                }


                int count = 0;
                int exclude = 0;
                string andor = string.Empty;
                string[] sourcevariablelist = new string[10];
                int sourcevariablepointer = 0;
                if (Option_AND.IsChecked == true)
                {
                    andor = Constants.DP.InstructionAND;
                }
                else if (Option_OR.IsChecked == true)
                {
                    andor = Constants.DP.InstructionOR;
                }
                if (!string.IsNullOrEmpty(Newvariable1.Text_OriginItem1_AnswerType.Text))
                {
                    count++;
                    sourcevariablelist[sourcevariablepointer] = Newvariable1.Combo_OriginItem_Item.Text;
                    sourcevariablepointer++;
                }
                if (!string.IsNullOrEmpty(Newvariable2.Text_OriginItem1_AnswerType.Text))
                {
                    count++;
                    sourcevariablelist[sourcevariablepointer] = Newvariable2.Combo_OriginItem_Item.Text;
                    sourcevariablepointer++;
                }
                if (!string.IsNullOrEmpty(Newvariable3.Text_OriginItem1_AnswerType.Text))
                {
                    count++;
                    sourcevariablelist[sourcevariablepointer] = Newvariable3.Combo_OriginItem_Item.Text;
                    sourcevariablepointer++;
                }
                if (!string.IsNullOrEmpty(Newvariable4.Text_OriginItem1_AnswerType.Text))
                {
                    count++;
                    sourcevariablelist[sourcevariablepointer] = Newvariable4.Combo_OriginItem_Item.Text;
                    sourcevariablepointer++;
                }
                if (!string.IsNullOrEmpty(Newvariable5.Text_OriginItem1_AnswerType.Text))
                {
                    count++;
                    sourcevariablelist[sourcevariablepointer] = Newvariable5.Combo_OriginItem_Item.Text;
                    sourcevariablepointer++;
                }
                if (!string.IsNullOrEmpty(Newvariable6.Text_OriginItem1_AnswerType.Text))
                {
                    count++;
                    sourcevariablelist[sourcevariablepointer] = Newvariable6.Combo_OriginItem_Item.Text;
                    sourcevariablepointer++;
                }
                if (!string.IsNullOrEmpty(Newvariable7.Text_OriginItem1_AnswerType.Text))
                {
                    count++;
                    sourcevariablelist[sourcevariablepointer] = Newvariable7.Combo_OriginItem_Item.Text;
                    sourcevariablepointer++;
                }
                if (!string.IsNullOrEmpty(Newvariable8.Text_OriginItem1_AnswerType.Text))
                {
                    count++;
                    sourcevariablelist[sourcevariablepointer] = Newvariable8.Combo_OriginItem_Item.Text;
                    sourcevariablepointer++;
                }
                if (!string.IsNullOrEmpty(Newvariable9.Text_OriginItem1_AnswerType.Text))
                {
                    count++;
                    sourcevariablelist[sourcevariablepointer] = Newvariable9.Combo_OriginItem_Item.Text;
                    sourcevariablepointer++;
                }
                if (!string.IsNullOrEmpty(Newvariable10.Text_OriginItem1_AnswerType.Text))
                {
                    count++;
                    sourcevariablelist[sourcevariablepointer] = Newvariable10.Combo_OriginItem_Item.Text;
                    sourcevariablepointer++;
                }
                if (Check_After_Unfall.IsChecked == true)
                {
                    exclude = 1;
                }
                int choicecount = 0;
                if (Combo_NewItem_SelectCount.SelectedIndex == 0)
                {
                    choicecount = choicegridcount;// Qc4Launcher.Util.Constants.MaxChoiceCount;//Common.Constants.comboautovalue 
                }
                else
                {
                    choicecount = Convert.ToInt32(Combo_NewItem_SelectCount.Text);
                }
                int paramcount = 2 + count + (count - 1) + choicecount;
                string[] paramlist = new string[paramcount];
                int columncount = 10 + count + (count - 1) + choicecount;// (QC4Common.Common.Constants.DP.MAX_DP_COLUMN - QC4Common.Common.Constants.DP.OnOffColumn) + 1;
                string[,] dpsaveinstructios = new string[1, (QC4Common.Common.Constants.DP.MAX_DP_COLUMN - QC4Common.Common.Constants.DP.OnOffColumn) + 1];//SAVE ARRAY
                int si = 0;
                int rowcount = 0;



                DataTable newvariabledt = ((DataView)gridnewvariable.ItemsSource).ToTable();
                newvariabledt = frmutil.UnEscapeCRLFFromAllRows(newvariabledt);
                Logic.DataProcessHelper dphelper = new Logic.DataProcessHelper();

                string[] dt_Choices_columns = new string[2];
                dt_Choices_columns[0] = "SL";
                dt_Choices_columns[1] = "Choice";
                QC4Common.Util.FormUtil.commaseperatedvalues = string.Empty;

                bool isupdatequestion = false;
                //newvariable = Combo_NewItem_Item.Text ;
                string answertype = Combo_NewItem_AnswerType.Text;
                string question = Text_NewItem_Question.Text;
                if ((Util.Definiotion.VariableDictionary.ContainsKey(newvariable)) && (
                     (Util.Definiotion.VariableDictionary[newvariable].AnswerType != answertype) ||
                    (Util.Definiotion.VariableDictionary[newvariable].CategoryCount != choicecount))
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
                    if ((Util.Definiotion.VariableDictionary[newvariable].Question != frmutil.UnEscapeCRLF(question)))
                    {
                        isupdatequestion = true;
                    }
                    for (int i = 0; i < choicecount; i++)
                    {
                        if (!frmutil.UnEscapeCRLF(Convert.ToString(newvariabledt.Rows[i][1])).Equals((Util.Definiotion.VariableDictionary[newvariable]).Choices[i]))
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
                for (int i = 0; i < choicecount; i++)
                {
                    if (string.IsNullOrEmpty(Convert.ToString(gridchoice.Rows[i][1])) || string.IsNullOrEmpty(Convert.ToString(gridchoice.Rows[i][1]).TrimStart().TrimEnd()))
                    {
                        MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT_, LocalResource.LABEL_CHOICE));
                        frmutil.SetErrorForGrid(gridnewvariable, i, 1, QC4Common.Common.Constants.STD_DP.Background);
                        return;
                    }
                    else { frmutil.SetErrorForGrid(gridnewvariable, i, 1, QC4Common.Common.Constants.STD_DP.Background, true); }
                }
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
                            dpsaveinstructios[0, i] = Constants.DP.SubstituteOperatorINTEGRATE;
                            break;
                        case 8://exclude-integrate
                            dpsaveinstructios[0, i] = exclude.ToString();
                            break;
                        case 9://no of vars
                            dpsaveinstructios[0, i] = count.ToString();
                            break;
                        default:
                            //variable and values
                            if (si < count)
                            {
                                if (i % 2 == 0)
                                {
                                    dpsaveinstructios[0, i] = sourcevariablelist[si];
                                    si++;
                                }
                                else
                                {
                                    dpsaveinstructios[0, i] = andor;
                                }
                            }
                            else
                            {
                                // for (int rowcount = 0; rowcount < choicecount; rowcount++)
                                {

                                    int colcount = 0;
                                    if (count == 1)
                                    {
                                        dpsaveinstructios[0, i] = Convert.ToString(gridsourcevariables.Rows[rowcount][colcount]).TrimStart('=');
                                        if (!string.IsNullOrEmpty(Convert.ToString(gridsourcevariables.Rows[rowcount][colcount]).TrimStart('=')))
                                        {
                                            isatleastonevalue = true;
                                        }
                                    }
                                    else
                                    {
                                        string paramvalues = string.Empty;
                                        for (colcount = 0; colcount < count; colcount++)
                                        {
                                            paramvalues += (colcount != 0 ? ";" : string.Empty) + Convert.ToString(gridsourcevariables.Rows[rowcount][colcount]).TrimStart('=');
                                            if (!string.IsNullOrEmpty(Convert.ToString(gridsourcevariables.Rows[rowcount][colcount]).TrimStart('=')))
                                            {
                                                isatleastonevalue = true;
                                            }
                                        }
                                        dpsaveinstructios[0, i] = paramvalues;
                                    }

                                    rowcount++;
                                }
                            }
                            break;

                    }
                }
                if (!isatleastonevalue)
                {
                    MessageDialog.ErrorOk(string.Format(ExcelAddIn.AddinResource.ERR_MSG_COUNT_PARAM_OVERLAP, QC4Common.Common.Constants.DP.SubstituteOperatorINTEGRATE));
                    return;
                }
                if (dphelper.WriteProcess(workbook, Util.Constants.ProcessingType.CreateNewVariable, newvariable, answertype, question,
                      choicecount, newvariabledt.DefaultView.ToTable(false, dt_Choices_columns), Constants.DP.SubstituteOperatorINTEGRATE, dpsaveinstructios, isnewquestion, writerow, QC4Common.Common.Constants.STD_DP.Process_Create, null, isupdatequestion))//need to pass row num from here for saving 
                {

                    MessageDialog.Info(LocalResource.DATAPROSESS_SAVED_SUCCESSFULLY);
                    isModifiedProcess = true;
                    this.Close();

                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
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
        DataProcessNewLoad dataLoad = new DataProcessNewLoad();
        private void Combo_NewItem_Item_Text_Change(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(Combo_NewItem_Item.Text))
            {
                Combo_NewItem_AnswerType.IsEnabled = false;
            }
            else
            {
                QuestionSettings qs = dataLoad.Txt_Change_New_Item(Combo_NewItem_Item.Text);
                if (qs != null)
                {

                    try
                    {
                        Combo_NewItem_AnswerType.IsEnabled = false;
                        Combo_NewItem_AnswerType.Foreground = new SolidColorBrush(Colors.DimGray);
                    }
                    catch { }
                    // Combo_NewItem_AnswerType.SelectedItem = MagnifyingGlassButton.VariableList.AnswerType;
                    Text_NewItem_Question.Text = frmutil.UnEscapeCRLF(qs.TableHeading) + " " + frmutil.UnEscapeCRLF(qs.Question);// frmutil.UnEscapeCRLF(MagnifyingGlassButton.VariableList.Question);

                    Command_Entry.IsEnabled = true;
                    // Load Choices and bind DT 
                    if (qs.Choices.Count() > 0)
                    {
                        Combo_NewItem_AnswerType.SelectedItem = qs.AnswerType;
                        Combo_NewItem_SelectCount.SelectedIndex = qs.Choices.Count;
                        gridchoice = ClearColumn(1, gridchoice.Rows.Count, gridchoice);
                        for (int i = 0; i < qs.Choices.Count; i++)
                        {
                            gridchoice.Rows[i][1] = frmutil.EscapeCRLF(qs.Choices[i]);
                        }
                    }
                    Combo_NewItem_SelectCount.IsEnabled = false;

                }
                else
                {
                    Combo_NewItem_AnswerType.IsEnabled = true;
                    Combo_NewItem_SelectCount.IsEnabled = true;
                    Combo_NewItem_AnswerType.Foreground = new SolidColorBrush(Colors.Black);
                }
            }
        }

        private void Command_NewItem_Input_Click(object sender, RoutedEventArgs e)
        {

            int choicegridcount = -1;
            int[] sourcevariablepos = GetSourceVariablepositions();
            if (Combo_NewItem_SelectCount.SelectedIndex > 0)
            {
                choicegridcount = int.Parse(Combo_NewItem_SelectCount.Text);
            }
            else { choicegridcount = frmutil.GetLastRow(gridchoice, 1); }
            choicegridcount = frmutil.GetLastRow(gridchoice, 1);
            int maxlimit = choicegridcount;
            if ((Combo_NewItem_SelectCount.SelectedIndex == 0) || (choicegridcount < sourcevariablepos[0] || choicegridcount < sourcevariablepos[1] || choicegridcount < sourcevariablepos[2]
                || choicegridcount < sourcevariablepos[3] || choicegridcount < sourcevariablepos[4] || choicegridcount < sourcevariablepos[5] || choicegridcount < sourcevariablepos[6]
                || choicegridcount < sourcevariablepos[7] || choicegridcount < sourcevariablepos[8] || choicegridcount < sourcevariablepos[9]))
            {
                // maxlimit = sourcevariablepos[0];
                for (int i = 0; i < 10; i++)
                {
                    if (maxlimit < sourcevariablepos[i]) { maxlimit = sourcevariablepos[i]; }
                }
            }
            string[] Sourcevariablenames = GetSourceVariables();
            if (!GridVAlueVAlidation(Sourcevariablenames, sourcevariablepos, maxlimit, true))
            {
                return;
            }

        }


        void ScrollOwner1_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {

        }
        private void Combo_Classify_Item_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {

        }
        private void GetChoiceAndType(int j, out string answertyp, out int noofchoices, out List<string> sourcechoices)
        {
            answertyp = string.Empty;
            noofchoices = 0;
            sourcechoices = new List<string>();
            if (j == 0)
            {
                answertyp = Newvariable1.Text_OriginItem1_AnswerType.Text;
                if (!string.IsNullOrEmpty(Newvariable1.Text_OriginItem1_SelectCount.Text))
                    noofchoices = int.Parse(Newvariable1.Text_OriginItem1_SelectCount.Text);
                foreach (string list in Newvariable1.List_OriginItem_ChoiceList.Items)
                {
                    sourcechoices.Add(list.Split(':')[1]);
                }
            }
            else if (j == 1)
            {
                answertyp = Newvariable2.Text_OriginItem1_AnswerType.Text;
                if (!string.IsNullOrEmpty(Newvariable2.Text_OriginItem1_SelectCount.Text))
                    noofchoices = int.Parse(Newvariable2.Text_OriginItem1_SelectCount.Text);
                foreach (string list in Newvariable2.List_OriginItem_ChoiceList.Items)
                {
                    sourcechoices.Add(list.Split(':')[1]);
                }
            }
            else if (j == 2)
            {
                answertyp = Newvariable3.Text_OriginItem1_AnswerType.Text;
                if (!string.IsNullOrEmpty(Newvariable3.Text_OriginItem1_SelectCount.Text))
                    noofchoices = int.Parse(Newvariable3.Text_OriginItem1_SelectCount.Text);
                foreach (string list in Newvariable3.List_OriginItem_ChoiceList.Items)
                {
                    sourcechoices.Add(list.Split(':')[1]);
                }
            }
            else if (j == 3)
            {
                answertyp = Newvariable4.Text_OriginItem1_AnswerType.Text;
                if (!string.IsNullOrEmpty(Newvariable4.Text_OriginItem1_SelectCount.Text))
                    noofchoices = int.Parse(Newvariable4.Text_OriginItem1_SelectCount.Text);
                foreach (string list in Newvariable4.List_OriginItem_ChoiceList.Items)
                {
                    sourcechoices.Add(list.Split(':')[1]);
                }
            }
            else if (j == 4)
            {
                answertyp = Newvariable5.Text_OriginItem1_AnswerType.Text;
                if (!string.IsNullOrEmpty(Newvariable5.Text_OriginItem1_SelectCount.Text))
                    noofchoices = int.Parse(Newvariable5.Text_OriginItem1_SelectCount.Text);
                foreach (string list in Newvariable5.List_OriginItem_ChoiceList.Items)
                {
                    sourcechoices.Add(list.Split(':')[1]);
                }
            }
            else if (j == 5)
            {
                answertyp = Newvariable6.Text_OriginItem1_AnswerType.Text;
                if (!string.IsNullOrEmpty(Newvariable6.Text_OriginItem1_SelectCount.Text))
                    noofchoices = int.Parse(Newvariable6.Text_OriginItem1_SelectCount.Text);
                foreach (string list in Newvariable6.List_OriginItem_ChoiceList.Items)
                {
                    sourcechoices.Add(list.Split(':')[1]);
                }
            }
            else if (j == 6)
            {
                answertyp = Newvariable7.Text_OriginItem1_AnswerType.Text;
                if (!string.IsNullOrEmpty(Newvariable7.Text_OriginItem1_SelectCount.Text))
                    noofchoices = int.Parse(Newvariable7.Text_OriginItem1_SelectCount.Text);
                foreach (string list in Newvariable7.List_OriginItem_ChoiceList.Items)
                {
                    sourcechoices.Add(list.Split(':')[1]);
                }
            }
            else if (j == 7)
            {
                answertyp = Newvariable8.Text_OriginItem1_AnswerType.Text;
                if (!string.IsNullOrEmpty(Newvariable8.Text_OriginItem1_SelectCount.Text))
                    noofchoices = int.Parse(Newvariable8.Text_OriginItem1_SelectCount.Text);
                foreach (string list in Newvariable8.List_OriginItem_ChoiceList.Items)
                {
                    sourcechoices.Add(list.Split(':')[1]);
                }
            }
            else if (j == 8)
            {
                answertyp = Newvariable9.Text_OriginItem1_AnswerType.Text;
                if (!string.IsNullOrEmpty(Newvariable9.Text_OriginItem1_SelectCount.Text))
                    noofchoices = int.Parse(Newvariable9.Text_OriginItem1_SelectCount.Text);
                foreach (string list in Newvariable9.List_OriginItem_ChoiceList.Items)
                {
                    sourcechoices.Add(list.Split(':')[1]);
                }
            }
            else if (j == 9)
            {
                answertyp = Newvariable10.Text_OriginItem1_AnswerType.Text;
                if (!string.IsNullOrEmpty(Newvariable10.Text_OriginItem1_SelectCount.Text))
                    noofchoices = int.Parse(Newvariable10.Text_OriginItem1_SelectCount.Text);
                foreach (string list in Newvariable10.List_OriginItem_ChoiceList.Items)
                {
                    sourcechoices.Add(list.Split(':')[1]);
                }
            }
        }
        private bool GridVAlueVAlidation(string[] sourcevariablenames, int[] sourcevariablepos, int maxlimit, bool setnewchoicevalues = false)
        {
            int lasterrorrow = -1;
            int lasterrorcolumn = -1;
            if (maxlimit > 0)
            {
                string[] choicetext = new string[maxlimit];
                for (int i = 0; i < 10; i++)
                {

                    bool isnot = false;

                    if (sourcevariablepos[i] > -1)
                    {
                        for (int j = 0; j < maxlimit; j++)
                        {
                            //check MA-SA / N
                            if (!frmutil.IsNumeric(Convert.ToString(gridsourcevariables.Rows[j][i])))
                            {
                                //source variable not numeric
                                //New variable: input of [2] is invalid. Set a numeric value.
                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, j + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_SET_A_NUMERIC_VALUE));
                                frmutil.SetErrorForGrid(gridoriginalitem, j, i, QC4Common.Common.Constants.STD_DP.Foreground);
                                lasterrorrow = j;
                                lasterrorcolumn = i;
                                return false;
                            }
                            else// if (lasterrorrow == j && lasterrorcolumn == i)
                            {
                                frmutil.SetErrorForGrid(gridoriginalitem, j, i, QC4Common.Common.Constants.STD_DP.Foreground, true);
                            }
                            //check if multiple ! is there or other than at start
                            if (!frmutil.IsNotOtherThanStart(Convert.ToString(gridsourcevariables.Rows[j][i])))
                            {
                                ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + string.Format(LocalResource.ERR_MSG_INTEGRATE_CANNOT_SET_ELSEWHERE_THAN_AT_START, "!"));
                                frmutil.SetErrorForGrid(gridoriginalitem, j, i, QC4Common.Common.Constants.STD_DP.Foreground);
                                return false;
                            }
                            else { frmutil.SetErrorForGrid(gridoriginalitem, j, i, QC4Common.Common.Constants.STD_DP.Foreground, true); }
                            lasterrorrow = -1; lasterrorcolumn = -1;
                            if (!frmutil.IsonlySplChar(Convert.ToString(gridsourcevariables.Rows[j][i])))
                            {
                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, j + 1) + "\n" + string.Format(LocalResource.ERR_MSG_INTEGRATE_CANNOT_SET_ALONE, Convert.ToString(gridsourcevariables.Rows[j][i])));
                                frmutil.SetErrorForGrid(gridoriginalitem, j, i, QC4Common.Common.Constants.STD_DP.Foreground);
                                lasterrorrow = j;
                                lasterrorcolumn = i;
                                return false;
                            }
                            else// if (lasterrorrow == j && lasterrorcolumn == i)
                            {
                                frmutil.SetErrorForGrid(gridoriginalitem, j, i, QC4Common.Common.Constants.STD_DP.Foreground, true);
                            }
                            lasterrorrow = -1; lasterrorcolumn = -1;
                            //if (!frmutil.IsMultipleLimit(Convert.ToString(gridsourcevariables.Rows[j][i])))
                            //{
                            //    //source variable multiple separartor
                            //    //New variable: input of [2] is invalid. Multiple [-]s are set.
                            //    MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, j + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_MULTIPLE_LIMIT_SET));
                            //    frmutil.SetErrorForGrid(gridoriginalitem, j, i, QC4Common.Common.Constants.STD_DP.Foreground);
                            //    lasterrorrow = j;
                            //    lasterrorcolumn = i;
                            //    return false;
                            //}
                            //else //if (lasterrorrow == j && lasterrorcolumn == i)
                            //{
                            //    frmutil.SetErrorForGrid(gridoriginalitem, j, i, QC4Common.Common.Constants.STD_DP.Foreground, true);
                            //}
                            string answertyp = string.Empty;// QC4Common.Common.Constants.AnswerType.N;
                            List<string> sourcechoices = new List<string>();
                            int noofchoices = 0;
                            //  GetChoiceAndType(i, out answertyp, out noofchoices, out sourcechoices);
                            answertyp = Util.Definiotion.VariableDictionary[sourcevariablenames[i]].AnswerType;
                            noofchoices = Util.Definiotion.VariableDictionary[sourcevariablenames[i]].Choices.Count;
                            sourcechoices = Util.Definiotion.VariableDictionary[sourcevariablenames[i]].Choices;
                            if (answertyp == QC4Common.Common.Constants.AnswerType.SA || answertyp == QC4Common.Common.Constants.AnswerType.MA)
                            {
                                lasterrorrow = -1; lasterrorcolumn = -1;
                                if (!frmutil.CheckRangeExceeds(QC4Common.Util.FormUtil.commaseperatedvalues, noofchoices))
                                {
                                    //source variable value exceeds choice limit
                                    //New variable: input of [2] is invalid. Set an integer in the range of [1-3].
                                    MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, j + 1) + "\n" + string.Format(LocalResource.ERR_MSG_INTEGRATE_SET_INTEGER_RANGE_OF, "1", noofchoices.ToString()));
                                    frmutil.SetErrorForGrid(gridoriginalitem, j, i, QC4Common.Common.Constants.STD_DP.Foreground);
                                    lasterrorrow = j;
                                    lasterrorcolumn = i;
                                    return false;
                                }
                                else //if (lasterrorrow == j && lasterrorcolumn == i)
                                {
                                    frmutil.SetErrorForGrid(gridoriginalitem, j, i, QC4Common.Common.Constants.STD_DP.Foreground, true);
                                }
                                if (!string.IsNullOrEmpty(Convert.ToString(gridsourcevariables.Rows[j][i])) && !frmutil.ValidateRange(Convert.ToString(gridsourcevariables.Rows[j][i]), 1, noofchoices))
                                {
                                    //source variable value exceeds choice limit
                                    //New variable: input of [2] is invalid. Set an integer in the range of [1-3].
                                    MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_MULTIPLE_LIMIT_SET));
                                    frmutil.SetErrorForGrid(gridoriginalitem, j, i, QC4Common.Common.Constants.STD_DP.Foreground);
                                    lasterrorrow = j;
                                    lasterrorcolumn = i;
                                    return false;
                                }
                                else //if (lasterrorrow == j && lasterrorcolumn == i)
                                {
                                    frmutil.SetErrorForGrid(gridoriginalitem, j, i, QC4Common.Common.Constants.STD_DP.Foreground, true);
                                }

                            }
                            else if (answertyp == QC4Common.Common.Constants.AnswerType.N)
                            {
                                // "/" not allowed
                                if (!frmutil.IsMultipleLimit(Convert.ToString(gridsourcevariables.Rows[j][i])))
                                {
                                    //source variable multiple separartor
                                    //New variable: input of [2] is invalid. Multiple [-]s are set.
                                    MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, j + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_MULTIPLE_LIMIT_SET));
                                    frmutil.SetErrorForGrid(gridoriginalitem, j, i, QC4Common.Common.Constants.STD_DP.Foreground);
                                    lasterrorrow = j;
                                    lasterrorcolumn = i;
                                    return false;
                                }
                                else //if (lasterrorrow == j && lasterrorcolumn == i)
                                {
                                    frmutil.SetErrorForGrid(gridoriginalitem, j, i, QC4Common.Common.Constants.STD_DP.Foreground, true);
                                }

                                lasterrorrow = -1; lasterrorcolumn = -1;
                                if ((Convert.ToString(gridsourcevariables.Rows[j][i])).Contains("/") || (Convert.ToString(gridsourcevariables.Rows[j][i])).Contains(","))
                                {
                                    //New variable: input of [1] is invalid. Cannot specify split range
                                    MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, j + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_CANNOT_SPECIFY_SPLIT_RANGE));
                                    frmutil.SetErrorForGrid(gridoriginalitem, j, i, QC4Common.Common.Constants.STD_DP.Foreground);
                                    lasterrorrow = j;
                                    lasterrorcolumn = i;
                                    return false;

                                }
                                else //if (lasterrorrow == j && lasterrorcolumn == i)
                                {
                                    frmutil.SetErrorForGrid(gridoriginalitem, j, i, QC4Common.Common.Constants.STD_DP.Foreground, true);
                                }

                            }
                            lasterrorrow = -1; lasterrorcolumn = -1;
                            if (!frmutil.GetAllranges(Convert.ToString(gridsourcevariables.Rows[j][i])))// lower and upper limit  
                            {
                                //New variable: input of [1] is invalid. Set the value for the lower limit less than the upper limit.
                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, j + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_SET_LOWER_LIMIT_LESS_THAN_UPPER_LIMIT));
                                frmutil.SetErrorForGrid(gridoriginalitem, j, i, QC4Common.Common.Constants.STD_DP.Foreground);
                                lasterrorrow = j;
                                lasterrorcolumn = i;
                                return false;
                            }
                            else //if (lasterrorrow == j && lasterrorcolumn == i)
                            {
                                frmutil.SetErrorForGrid(gridoriginalitem, j, i, QC4Common.Common.Constants.STD_DP.Foreground, true);
                            }
                            if (setnewchoicevalues)
                            {
                                string newchoicevalues = string.Empty;

                                if (answertyp == QC4Common.Common.Constants.AnswerType.SA || answertyp == QC4Common.Common.Constants.AnswerType.MA)
                                {
                                    string choicevalues = frmutil.GetCommaSeperated(Convert.ToString(gridsourcevariables.Rows[j][i]), noofchoices, false);
                                    string[] choicearray = choicevalues.Split(',');

                                    foreach (string s in choicearray)
                                    {
                                        if (!string.IsNullOrEmpty(s))
                                        {
                                            int id = int.Parse(s);
                                            newchoicevalues += sourcechoices[id - 1];
                                        }
                                    }
                                }
                                else if (answertyp == QC4Common.Common.Constants.AnswerType.N)
                                {
                                    string nchoicevalue = Convert.ToString(gridsourcevariables.Rows[j][i]);// QC4Common.Util.FormUtil.commaseperatedvalues;
                                    nchoicevalue = frmutil.GetNtypeValueSeperatedByComma(nchoicevalue);
                                    //if(nchoicevalue.StartsWith("="))
                                    //{
                                    //    nchoicevalue= nchoicevalue.Replace("=",string.Empty);
                                    //}
                                    //if (nchoicevalue.StartsWith("<>"))
                                    //{
                                    //    nchoicevalue= nchoicevalue.Replace("<>", string.Empty);
                                    //}
                                    string[] nchoicearray = nchoicevalue.Split(',');
                                    if (!string.IsNullOrEmpty(Convert.ToString(nchoicearray[0])))
                                    {
                                        newchoicevalues += Convert.ToString(nchoicearray[0]) + LocalResource.INTEGRATE_NTYPE_NEW_CHOICE_OR_OVER;
                                    }
                                    if (nchoicearray.Length > 1 && !string.IsNullOrEmpty(Convert.ToString(nchoicearray[1])))
                                    {
                                        newchoicevalues += Convert.ToString(nchoicearray[1]) + LocalResource.INTEGRATE_NTYPE_NEW_CHOICE_OR_UNDER;
                                    }

                                }

                                if (Convert.ToString(gridsourcevariables.Rows[j][i]).StartsWith("<>") || Convert.ToString(gridsourcevariables.Rows[j][i]).StartsWith("!"))
                                {
                                    newchoicevalues = string.Format(LocalResource.INTEGRATE_NEW_CHOICE_EXCLUDE_CHOICE, newchoicevalues);
                                }
                                if (i != 0)
                                {
                                    string spacechar = string.Empty;
                                    if (!string.IsNullOrEmpty(Convert.ToString(choicetext[j])) && !string.IsNullOrEmpty(newchoicevalues))//if (!string.IsNullOrEmpty(Convert.ToString(gridchoice.Rows[j][1])) && !string.IsNullOrEmpty(newchoicevalues))
                                    {
                                        spacechar = " ";
                                    }
                                    newchoicevalues = Convert.ToString(choicetext[j]) + spacechar + frmutil.EscapeCRLF(newchoicevalues);
                                }
                                //else
                                //{
                                //    newchoicevalues= Convert.ToString(gridchoice.Rows[j][1]) + newchoicevalues;
                                //}
                                // gridchoice.Rows[j][1] =  newchoicevalues;
                                choicetext[j] = frmutil.EscapeCRLF(newchoicevalues);
                            }
                        }
                    }

                }
                if (setnewchoicevalues)
                {
                    for (int j = 0; j < maxlimit; j++)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(choicetext[j])))
                        {
                            gridchoice.Rows[j][1] = Convert.ToString(choicetext[j]);
                        }
                    }
                }
            }
            return true;
        }

        private void Newvariablescroll_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }

        private void Scroll_Changed(object sender, ScrollChangedEventArgs e)
        {
            if (mouseclick)
            {
                mouseclick = false;
                if (!scroll)
                {
                    return;
                }
            }
            if (scroll)
            {
                if (e.VerticalOffset >= 0.0f)// if (e.VerticalOffset != 0.0f)
                {
                    DataGrid scrollinggrid = gridoriginalitem;
                    if (((DataGrid)sender).Name == gridoriginalitem.Name)
                    {
                        scrollinggrid = gridnewvariable;
                    }
                    ScrollViewer sv = null;
                    Type t = scrollinggrid.GetType();//  Type t = gridoriginalitem.GetType();
                    try
                    {
                        int x = scrollinggrid.Items.Count;//  int x = gridoriginalitem.Items.Count;
                        int pos = 0;
                        if (e.VerticalOffset < x)
                        {
                            pos = Convert.ToInt32(e.VerticalOffset);

                        }
                        else
                        {
                            pos = x;
                        }
                        if (pos < 0)
                        {
                            pos = 0;
                        }
                        if (pos >= x)
                        {
                            pos = x;
                        }

                        sv = t.InvokeMember("InternalScrollHost", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetProperty, null, scrollinggrid, null) as ScrollViewer;
                        sv.ScrollToVerticalOffset(pos);
                        /*  sv = t.InvokeMember("InternalScrollHost", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetProperty, null, gridoriginalitem, null) as ScrollViewer;
                        sv.ScrollToVerticalOffset(pos);*/
                    }
                    catch (Exception ex)
                    {
                        _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                        MessageBox.Show(ex.Message);
                    }
                }
            }


        }
        private void dataGridRowSelected(object sender, RoutedEventArgs e)
        {
            //commented due to unexpected issue
            //DataGrid scrollinggrid = gridoriginalitem;
            //if (((DataGrid)sender).Name == gridoriginalitem.Name)
            //{
            //    scrollinggrid = gridnewvariable;
            //}
            //scrollinggrid.SelectedIndex = ((DataGrid)sender).SelectedIndex;
        }
        private void Scroll_Changed(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }

        private void ScrollBar_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }
        private string[] GetSourceVariables()
        {
            string[] sourcevariablenames = new string[10];
            if (!string.IsNullOrEmpty(Newvariable1.Combo_OriginItem_Item.Text) && Util.Definiotion.VariableDictionary.ContainsKey(Newvariable1.Combo_OriginItem_Item.Text))
            {
                sourcevariablenames[0] = Newvariable1.Combo_OriginItem_Item.Text;
            }
            else { return sourcevariablenames; }
            if (!string.IsNullOrEmpty(Newvariable2.Combo_OriginItem_Item.Text) && Util.Definiotion.VariableDictionary.ContainsKey(Newvariable2.Combo_OriginItem_Item.Text))
            {
                sourcevariablenames[1] = Newvariable2.Combo_OriginItem_Item.Text;
            }
            else { return sourcevariablenames; }
            if (!string.IsNullOrEmpty(Newvariable3.Combo_OriginItem_Item.Text) && Util.Definiotion.VariableDictionary.ContainsKey(Newvariable3.Combo_OriginItem_Item.Text))
            {
                sourcevariablenames[2] = Newvariable3.Combo_OriginItem_Item.Text;
            }
            else { return sourcevariablenames; }
            if (!string.IsNullOrEmpty(Newvariable4.Combo_OriginItem_Item.Text) && Util.Definiotion.VariableDictionary.ContainsKey(Newvariable4.Combo_OriginItem_Item.Text))
            {
                sourcevariablenames[3] = Newvariable4.Combo_OriginItem_Item.Text;
            }
            else { return sourcevariablenames; }
            if (!string.IsNullOrEmpty(Newvariable5.Text_OriginItem1_AnswerType.Text) && Util.Definiotion.VariableDictionary.ContainsKey(Newvariable5.Combo_OriginItem_Item.Text))
            {
                sourcevariablenames[4] = Newvariable5.Combo_OriginItem_Item.Text;
            }
            else { return sourcevariablenames; }
            if (!string.IsNullOrEmpty(Newvariable6.Combo_OriginItem_Item.Text) && Util.Definiotion.VariableDictionary.ContainsKey(Newvariable6.Combo_OriginItem_Item.Text))
            {
                sourcevariablenames[5] = Newvariable6.Combo_OriginItem_Item.Text;
            }
            else { return sourcevariablenames; }
            if (!string.IsNullOrEmpty(Newvariable7.Combo_OriginItem_Item.Text) && Util.Definiotion.VariableDictionary.ContainsKey(Newvariable7.Combo_OriginItem_Item.Text))
            {
                sourcevariablenames[6] = Newvariable7.Combo_OriginItem_Item.Text;
            }
            else { return sourcevariablenames; }
            if (!string.IsNullOrEmpty(Newvariable8.Combo_OriginItem_Item.Text) && Util.Definiotion.VariableDictionary.ContainsKey(Newvariable8.Combo_OriginItem_Item.Text))
            {
                sourcevariablenames[7] = Newvariable8.Combo_OriginItem_Item.Text;
            }
            else { return sourcevariablenames; }
            if (!string.IsNullOrEmpty(Newvariable9.Combo_OriginItem_Item.Text) && Util.Definiotion.VariableDictionary.ContainsKey(Newvariable9.Combo_OriginItem_Item.Text))
            {
                sourcevariablenames[8] = Newvariable9.Combo_OriginItem_Item.Text;
            }
            else { return sourcevariablenames; }
            if (!string.IsNullOrEmpty(Newvariable10.Text_OriginItem1_AnswerType.Text) && Util.Definiotion.VariableDictionary.ContainsKey(Newvariable10.Combo_OriginItem_Item.Text))
            {
                sourcevariablenames[9] = Newvariable10.Combo_OriginItem_Item.Text;
            }
            else { return sourcevariablenames; }
            return sourcevariablenames;
        }
        private int[] GetSourceVariablepositions()
        {
            int[] sourcevariablepos = new int[10];
            for (int i = 0; i < 10; i++)
            {
                sourcevariablepos[i] = frmutil.GetLastRow(gridsourcevariables, i);
            }

            return sourcevariablepos;
        }
        private void SetValuesToControls(string[,] values)
        {
            NewItemSearchbutton.IsEnabled = true;
            Combo_NewItem_Item.IsEnabled = true;
            Text_NewItem_Question.IsEnabled = true;
            NewItemSearchbutton.Opacity = 1;
            Command_Entry.IsEnabled = true;
            NewItemColorChangeEnable();
            int choicecount = 0;
            int length = 0;
            int sourcevariablecount = 0;
            int exclude = 0;
            string newquestionvariable = string.Empty;
            string question = string.Empty;
            if (Util.Definiotion.VariableDictionary.ContainsKey(Convert.ToString(values[0, 6])))
            {
                newquestionvariable = Convert.ToString(values[0, 6]);
                question = (Util.Definiotion.VariableDictionary[newquestionvariable].Question);
                choicecount = (Util.Definiotion.VariableDictionary[newquestionvariable].CategoryCount);
            }
            if (!string.IsNullOrEmpty(Convert.ToString(values[0, 9])))
            {
                sourcevariablecount = int.Parse(Convert.ToString(values[0, 9]));
            }
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
            length = sourcevariablecount + (sourcevariablecount - 1) + choicecount;
            int sourvariablecombo = 0;
            int index = 0;
            int sourcegridloc = 0;
            string andor = string.Empty;
            for (int i = QC4Common.Common.Constants.DP.SubstituteParam3Column - QC4Common.Common.Constants.DP.OnOffColumn; i < QC4Common.Common.Constants.DP.SubstituteParam3Column - 2 + length; i++)
            {
                if (i <= QC4Common.Common.Constants.DP.SubstituteParam3Column - QC4Common.Common.Constants.DP.OnOffColumn + length - choicecount - 1)
                {
                    string sourcevariablename = Convert.ToString(values[0, i]);
                    if (!string.IsNullOrEmpty(sourcevariablename) && (Util.Definiotion.VariableDictionary.ContainsKey(sourcevariablename)))
                    {
                        switch (sourvariablecombo)
                        {
                            case 0:
                                // svlist = SourceVariableListView1.Where(z => z.Variable == sourcevariablename).FirstOrDefault();
                                //index = SourceVariableListView1.IndexOf(svlist);
                                Newvariable1.Combo_OriginItem_Item.SelectedIndex = GetIndexByVariableName(sourcevariablename, SourceVariableListView1);
                                // Newvariable1.Combo_OriginItem_Item.SelectedItem = svlist;
                                // SetComboValue(Newvariable1.Combo_OriginItem_Item, 1);
                                break;
                            case 1:
                                index = GetIndexByVariableName(sourcevariablename, SourceVariableListView2);
                                index = (sourvariablecombo == sourcevariablecount - 1 || sourvariablecombo == sourcevariablecount - 2) ? ++index : ++index;
                                Newvariable2.Combo_OriginItem_Item.SelectedIndex = index;
                                break;
                            case 2:
                                index = GetIndexByVariableName(sourcevariablename, SourceVariableListView3);
                                index = (sourvariablecombo == sourcevariablecount - 1 || sourvariablecombo == sourcevariablecount - 2) ? ++index : ++index;
                                Newvariable3.Combo_OriginItem_Item.SelectedIndex = index;
                                break;
                            case 3:
                                index = GetIndexByVariableName(sourcevariablename, SourceVariableListView4);
                                index = (sourvariablecombo == sourcevariablecount - 1 || sourvariablecombo == sourcevariablecount - 2) ? ++index : ++index;
                                Newvariable4.Combo_OriginItem_Item.SelectedIndex = index;
                                break;
                            case 4:
                                index = GetIndexByVariableName(sourcevariablename, SourceVariableListView4);
                                index = (sourvariablecombo == sourcevariablecount - 1 || sourvariablecombo == sourcevariablecount - 2) ? ++index : ++index;
                                Newvariable5.Combo_OriginItem_Item.SelectedIndex = index;
                                break;
                            case 5:
                                index = GetIndexByVariableName(sourcevariablename, SourceVariableListView4);
                                index = (sourvariablecombo == sourcevariablecount - 1 || sourvariablecombo == sourcevariablecount - 2) ? ++index : ++index;
                                Newvariable6.Combo_OriginItem_Item.SelectedIndex = index;
                                break;
                            case 6:
                                index = GetIndexByVariableName(sourcevariablename, SourceVariableListView4);
                                index = (sourvariablecombo == sourcevariablecount - 1 || sourvariablecombo == sourcevariablecount - 2) ? ++index : ++index;
                                Newvariable7.Combo_OriginItem_Item.SelectedIndex = index;
                                break;
                            case 7:
                                index = GetIndexByVariableName(sourcevariablename, SourceVariableListView4);
                                index = (sourvariablecombo == sourcevariablecount - 1 || sourvariablecombo == sourcevariablecount - 2) ? ++index : ++index;
                                Newvariable8.Combo_OriginItem_Item.SelectedIndex = index;
                                break;
                            case 8:
                                index = GetIndexByVariableName(sourcevariablename, SourceVariableListView4);
                                index = (sourvariablecombo == sourcevariablecount - 1 || sourvariablecombo == sourcevariablecount - 2) ? ++index : ++index;
                                Newvariable9.Combo_OriginItem_Item.SelectedIndex = index;
                                break;
                            case 9:
                                index = GetIndexByVariableName(sourcevariablename, SourceVariableListView4);
                                index = (sourvariablecombo == sourcevariablecount - 1 || sourvariablecombo == sourcevariablecount - 2) ? ++index : ++index;
                                Newvariable10.Combo_OriginItem_Item.SelectedIndex = index;
                                break;

                        }
                        sourvariablecombo++;
                    }
                    else
                    {
                        if (i % 2 != 0)
                        {
                            if (sourcevariablename == QC4Common.Common.Constants.DP.InstructionAND)
                            {
                                Option_AND.IsChecked = true;
                            }
                            else
                            {
                                Option_OR.IsChecked = true;
                            }
                        }
                    }



                }
                else
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(values[0, i])))
                    {

                        string[] choicevalues = Convert.ToString(values[0, i]).Split(';');
                        for (int j = 0; j < choicevalues.Length && sourcegridloc < choicecount; j++)
                        {


                            try
                            {
                                gridoriginalitem.Columns[j].IsReadOnly = false;
                                gridsourcevariables.Rows[sourcegridloc][j] = choicevalues[j];

                            }
                            catch { }


                        }
                    }
                    sourcegridloc++;
                }

            }
            gridoriginalitem.DataContext = gridsourcevariables;
            if (processingoption == QC4Common.Common.Constants.STD_DP.Process_Copy)
            {
                QC4Common.Util.QSUtil qsutil = new QC4Common.Util.QSUtil();
                Combo_NewItem_AnswerType.SelectedItem = (Util.Definiotion.VariableDictionary[newquestionvariable].AnswerType);//Redmine:213454
                newquestionvariable = qsutil.GetVariableName(newquestionvariable, PopulatedDictionary.Values.ToList());
            }
            else { Combo_NewItem_AnswerType.SelectedItem = (Util.Definiotion.VariableDictionary[newquestionvariable].AnswerType); }
            Combo_NewItem_Item.Text = newquestionvariable;
            Text_NewItem_Question.Text = question;
            Combo_NewItem_SelectCount.SelectedIndex = choicecount;

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
        private void NewItemColorChangeEnable()
        {
            NewItemSearchbutton.IsEnabled = true;
            Combo_NewItem_Item.IsEnabled = true;
            Combo_NewItem_Item.Background = Brushes.White;
            Text_NewItem_Question.IsEnabled = true;
            NewItemSearchbutton.Opacity = 1;
            Text_NewItem_Question.Background = Brushes.White;
            Style style = Application.Current.FindResource("NormalTextBoxMultiLine") as Style;
            Text_NewItem_Question.Style = style;
            Command_NewItem_Input.IsEnabled = true;
            Lbl_NewItem_Input.Foreground = new SolidColorBrush(Colors.Black);
        }
        private void DataGrid_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {// PreviewKeyDown="DataGrid_OnPreviewKeyDown"
            scroll = false;
            var uiElement = e.OriginalSource as UIElement;
            bool _ShiftModifierPressed = (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift));
            if (!_ShiftModifierPressed && e.Key == Key.Tab && ((DataGrid)sender).Name == gridnewvariable.Name && scrolltofirst == true)
            {
                e.Handled = true; gridnewvariable.ScrollIntoView(gridnewvariable.Items[0]);
                scrolltofirst = false;
                DataGridCell cell = frmutil.GetCell(gridnewvariable, 0, 1);
                if (cell != null)
                {
                    DataGridRow row = frmutil.GetRow(gridnewvariable, 0, 1);
                    if (row != null)
                    {
                        row.IsSelected = true;
                    }
                    cell.Focus();
                }
                gridnewvariable.SelectedIndex = 0;
                e.Handled = true;

                return;
            }
            if (_ShiftModifierPressed && (e.Key == Key.Enter || e.Key == Key.Tab) && uiElement != null)
            {
                e.Handled = true;
                scroll = true;
                if (((DataGrid)sender).Name == gridnewvariable.Name)
                {

                    if (gridnewvariable.CurrentCell.Column != null)
                    {

                        int row = gridnewvariable.SelectedIndex;
                        int column = -1;
                        row = row - 1;
                        if (row <= -1)
                        {
                            if (Command_NewItem_Input.IsEnabled == true)
                            {
                                Command_NewItem_Input.Focus();
                            }
                            else
                            {
                                Combo_NewItem_SelectCount.Focus();
                            }
                        }
                        else if (gridoriginalitem.Columns[9].IsReadOnly == false)
                        {
                            column = 9;
                        }
                        else if (gridoriginalitem.Columns[8].IsReadOnly == false)
                        {
                            column = 8;
                        }
                        else if (gridoriginalitem.Columns[7].IsReadOnly == false)
                        {
                            column = 7;
                        }
                        else if (gridoriginalitem.Columns[6].IsReadOnly == false)
                        {
                            column = 6;
                        }
                        else if (gridoriginalitem.Columns[5].IsReadOnly == false)
                        {
                            column = 5;
                        }
                        else if (gridoriginalitem.Columns[4].IsReadOnly == false)
                        {
                            column = 4;
                        }
                        else if (gridoriginalitem.Columns[3].IsReadOnly == false)
                        {
                            column = 3;
                        }
                        else if (gridoriginalitem.Columns[2].IsReadOnly == false)
                        {
                            column = 2;
                        }
                        else if (gridoriginalitem.Columns[1].IsReadOnly == false)
                        {
                            column = 1;
                        }
                        else if (gridoriginalitem.Columns[0].IsReadOnly == false)
                        {
                            column = 0;
                        }
                        if (column <= 9 && column >= 0)
                        {
                            // column = column - 1;
                            DataGridCell cell = frmutil.GetCell(gridoriginalitem, row, column);
                            if (cell != null)
                            {
                                // cell.IsSelected = true;
                                DataGridRow roww = frmutil.GetRow(gridoriginalitem, row, column);
                                if (roww != null)
                                {
                                    roww.IsSelected = true;
                                }
                                cell.Focus();
                                // gridoriginalitem.BeginEdit();
                                gridoriginalitem.SelectedIndex = row;
                            }
                        }
                        else
                        {
                            if (row >= 0)
                            {
                                uiElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Up));
                                gridnewvariable.SelectedIndex = row;
                            }
                            else
                            {
                                if (Command_NewItem_Input.IsEnabled == true)
                                {
                                    Command_NewItem_Input.Focus();
                                }
                                else
                                {
                                    Combo_NewItem_SelectCount.Focus();
                                }
                            }
                        }


                    }
                    else
                    {

                        DataGridCell cell = frmutil.GetCell(gridnewvariable, 0, 1);
                        if (cell != null)
                        {
                            DataGridRow row = frmutil.GetRow(gridnewvariable, 0, 1);
                            if (row != null)
                            {
                                row.IsSelected = true;
                            }
                            cell.Focus();
                        }
                        gridnewvariable.SelectedIndex = 0;
                    }
                    // uiElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                }
                else
                {
                    int row = gridoriginalitem.SelectedIndex;
                    int column = gridoriginalitem.CurrentCell.Column.DisplayIndex;

                    if (column == 0)

                    {
                        int index = row;
                        if (row >= 0)
                        {
                            DataGridCell cell = frmutil.GetCell(gridnewvariable, row, 1);
                            if (cell != null)
                            {
                                // cell.IsSelected = true;
                                DataGridRow roww = frmutil.GetRow(gridnewvariable, row, 1);
                                if (roww != null)
                                {
                                    roww.IsSelected = true;
                                }
                                cell.Focus();
                                // gridoriginalitem.BeginEdit();
                                gridnewvariable.SelectedIndex = row;
                            }
                            //gridnewvariable.SelectedItem = gridnewvariable.Items[index];
                            //gridnewvariable.ScrollIntoView(gridnewvariable.Items[index]);
                            //DataGridRow dgrow = (DataGridRow)gridnewvariable.ItemContainerGenerator.ContainerFromItem(gridnewvariable.Items[index]);
                            //dgrow.MoveFocus(new TraversalRequest(FocusNavigationDirection.Previous));
                        }
                    }
                    else
                    {
                        uiElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Left));
                    }

                }
            }
            else if (!_ShiftModifierPressed && (e.Key == Key.Enter || e.Key == Key.Tab) && uiElement != null)
            {
                e.Handled = true;
                scroll = true;
                if (((DataGrid)sender).Name == gridnewvariable.Name)
                {

                    if (gridnewvariable.CurrentCell.Column != null)
                    {
                        if (gridoriginalitem.Columns[0].IsReadOnly == false)
                        {
                            int row = gridnewvariable.SelectedIndex;
                            int column = gridnewvariable.CurrentCell.Column.DisplayIndex;
                            if (gridoriginalitem.Columns[0].IsReadOnly == false)
                            {

                                int index = row;
                                gridoriginalitem.SelectedItem = gridoriginalitem.Items[index];
                                gridoriginalitem.ScrollIntoView(gridoriginalitem.Items[index]);
                                DataGridRow dgrow = (DataGridRow)gridoriginalitem.ItemContainerGenerator.ContainerFromItem(gridoriginalitem.Items[index]);
                                dgrow.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                            }
                            else
                            {
                                if (row == gridnewvariable.Items.Count - 1)
                                {
                                    Check_After_Unfall.Focus();
                                }
                                else
                                {
                                    uiElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Down));
                                }
                            }

                            // gridoriginalitem.rows
                        }
                        else
                        {
                            int row = gridnewvariable.SelectedIndex == -1 ? 0 : gridnewvariable.SelectedIndex;
                            if (row == gridnewvariable.Items.Count - 1)
                            {
                                Check_After_Unfall.Focus();
                            }
                            else
                            {
                                uiElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Down)); gridnewvariable.SelectedIndex = row + 1;// uiElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Down)); gridnewvariable.SelectedIndex = gridnewvariable.SelectedIndex + 1;
                                e.Handled = true;
                            }
                        }
                    }
                    else
                    {

                        DataGridCell cell = frmutil.GetCell(gridnewvariable, 0, 1);
                        if (cell != null)
                        {
                            DataGridRow row = frmutil.GetRow(gridnewvariable, 0, 1);
                            if (row != null)
                            {
                                row.IsSelected = true;
                            }
                            cell.Focus();
                        }
                        gridnewvariable.SelectedIndex = 0;
                        e.Handled = true;
                    }
                    // uiElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                }
                else
                {
                    int row = gridoriginalitem.SelectedIndex;
                    int column = gridoriginalitem.CurrentCell.Column.DisplayIndex;

                    if ((column + 1 >= gridoriginalitem.Columns.Count) || gridoriginalitem.Columns[column + 1].IsReadOnly == true)
                    {
                        int index = row;
                        if (row < gridnewvariable.Items.Count - 1)
                        {
                            gridnewvariable.SelectedItem = gridnewvariable.Items[index + 1];
                            gridnewvariable.ScrollIntoView(gridnewvariable.Items[index + 1]);
                            DataGridRow dgrow = (DataGridRow)gridnewvariable.ItemContainerGenerator.ContainerFromItem(gridnewvariable.Items[index + 1]);
                            dgrow.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                        }
                        else
                        {
                            if (row >= gridnewvariable.Items.Count - 1)
                            {
                                Check_After_Unfall.Focus();
                            }
                        }
                    }
                    else
                    {
                        uiElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Right));
                    }

                }
            }

            else if (e.Key == Key.Up || e.Key == Key.Down)
            {
                scroll = true;
            }
            else
            {
                scroll = false;
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
                    if (((DataGrid)sender).Name == gridnewvariable.Name)
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
                    else
                    {

                        int RowIndex = 0;
                        if ((gridoriginalitem.SelectedItems != null) && (gridoriginalitem.SelectedItems.Count > 0))
                        {
                            for (int i = 0; i < gridoriginalitem.SelectedItems.Count; i++)
                            {
                                var presentRow = (System.Data.DataRowView)gridoriginalitem.SelectedItems[i];
                                // DataGridRow gridrow =( (DataRowView)gridoriginalitem.SelectedItems[i]).Row;
                                // int rindex = gridrow.GetIndex();
                                // IList rows = gridoriginalitem.SelectedItems;
                                RowIndex = gridoriginalitem.SelectedIndex + i;
                                // RowIndex = Convert.ToInt32(presentRow.Row.ItemArray[0].ToString());
                                for (int j = 0; j <= 9; j++)
                                {
                                    gridsourcevariables.Rows[RowIndex][j] = string.Empty;
                                }

                            }
                        }
                    }
                }
                catch { }
            }
            // scroll = true;
        }
        private void DataGrid_OnPreviewKeyDownoriginal(object sender, KeyEventArgs e)
        {

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

        private void Combo_OriginItem_Item_PreviewKeyDown1(object sender, KeyEventArgs e)
        {
            if (Key.Tab != e.Key)
            {
                System.Windows.Controls.ComboBox sendercombo = ((System.Windows.Controls.ComboBox)sender);
                Combo_KeyDown(sendercombo, sender, e);
            }
            else if (Key.Tab == e.Key)
            {
                e.Handled = true;
                if (this.Newvariable1.Command_OriginItem_InputSupport.IsEnabled == true)
                {
                    Newvariable1.Command_OriginItem_InputSupport.Focus();
                }
                else
                {
                    Combo_NewItem_Item.Focus();
                }
            }
        }
        private void Combo_OriginItem_Item_PreviewKeyDown2(object sender, KeyEventArgs e)
        {
            if (Key.Tab != e.Key)
            {
                System.Windows.Controls.ComboBox sendercombo = ((System.Windows.Controls.ComboBox)sender);
                Combo_KeyDown(sendercombo, sender, e);
            }
            else if (Key.Tab == e.Key)
            {
                e.Handled = true;
                if (this.Newvariable2.Command_OriginItem_InputSupport.IsEnabled == true)
                {
                    Newvariable2.Command_OriginItem_InputSupport.Focus();
                }
                else
                {
                    Combo_NewItem_Item.Focus();
                }
            }
        }
        private void Combo_OriginItem_Item_PreviewKeyDown3(object sender, KeyEventArgs e)
        {
            if (Key.Tab != e.Key)
            {
                System.Windows.Controls.ComboBox sendercombo = ((System.Windows.Controls.ComboBox)sender);
                Combo_KeyDown(sendercombo, sender, e);
            }
            else if (Key.Tab == e.Key)
            {
                e.Handled = true;
                if (this.Newvariable3.Command_OriginItem_InputSupport.IsEnabled == true)
                {
                    Newvariable3.Command_OriginItem_InputSupport.Focus();
                }
                else
                {
                    Combo_NewItem_Item.Focus();
                }
            }
        }
        private void Combo_OriginItem_Item_PreviewKeyDown4(object sender, KeyEventArgs e)
        {
            if (Key.Tab != e.Key)
            {
                System.Windows.Controls.ComboBox sendercombo = ((System.Windows.Controls.ComboBox)sender);
                Combo_KeyDown(sendercombo, sender, e);
            }
            else if (Key.Tab == e.Key)
            {
                e.Handled = true;
                if (this.Newvariable4.Command_OriginItem_InputSupport.IsEnabled == true)
                {
                    Newvariable4.Command_OriginItem_InputSupport.Focus();
                }
                else
                {
                    Combo_NewItem_Item.Focus();
                }
            }
        }
        private void Combo_OriginItem_Item_PreviewKeyDown5(object sender, KeyEventArgs e)
        {
            if (Key.Tab != e.Key)
            {
                System.Windows.Controls.ComboBox sendercombo = ((System.Windows.Controls.ComboBox)sender);
                Combo_KeyDown(sendercombo, sender, e);
            }
            else if (Key.Tab == e.Key)
            {
                e.Handled = true;
                if (this.Newvariable5.Command_OriginItem_InputSupport.IsEnabled == true)
                {
                    Newvariable5.Command_OriginItem_InputSupport.Focus();
                }
                else
                {
                    Combo_NewItem_Item.Focus();
                }
            }
        }
        private void Combo_OriginItem_Item_PreviewKeyDown6(object sender, KeyEventArgs e)
        {
            if (Key.Tab != e.Key)
            {
                System.Windows.Controls.ComboBox sendercombo = ((System.Windows.Controls.ComboBox)sender);
                Combo_KeyDown(sendercombo, sender, e);
            }
            else if (Key.Tab == e.Key)
            {
                e.Handled = true;
                if (this.Newvariable6.Command_OriginItem_InputSupport.IsEnabled == true)
                {
                    Newvariable6.Command_OriginItem_InputSupport.Focus();
                }
                else
                {
                    Combo_NewItem_Item.Focus();
                }
            }
        }
        private void Combo_OriginItem_Item_PreviewKeyDown7(object sender, KeyEventArgs e)
        {
            if (Key.Tab != e.Key)
            {
                System.Windows.Controls.ComboBox sendercombo = ((System.Windows.Controls.ComboBox)sender);
                Combo_KeyDown(sendercombo, sender, e);
            }
            else if (Key.Tab == e.Key)
            {
                e.Handled = true;
                if (this.Newvariable7.Command_OriginItem_InputSupport.IsEnabled == true)
                {
                    Newvariable7.Command_OriginItem_InputSupport.Focus();
                }
                else
                {
                    Combo_NewItem_Item.Focus();
                }
            }
        }
        private void Combo_OriginItem_Item_PreviewKeyDown8(object sender, KeyEventArgs e)
        {
            if (Key.Tab != e.Key)
            {
                System.Windows.Controls.ComboBox sendercombo = ((System.Windows.Controls.ComboBox)sender);
                Combo_KeyDown(sendercombo, sender, e);
            }
            else if (Key.Tab == e.Key)
            {
                e.Handled = true;
                if (this.Newvariable8.Command_OriginItem_InputSupport.IsEnabled == true)
                {
                    Newvariable8.Command_OriginItem_InputSupport.Focus();
                }
                else
                {
                    Combo_NewItem_Item.Focus();
                }
            }
        }
        private void Combo_OriginItem_Item_PreviewKeyDown9(object sender, KeyEventArgs e)
        {
            if (Key.Tab != e.Key)
            {
                System.Windows.Controls.ComboBox sendercombo = ((System.Windows.Controls.ComboBox)sender);
                Combo_KeyDown(sendercombo, sender, e);
            }
            else if (Key.Tab == e.Key)
            {
                e.Handled = true;
                if (this.Newvariable9.Command_OriginItem_InputSupport.IsEnabled == true)
                {
                    Newvariable9.Command_OriginItem_InputSupport.Focus();
                }
                else
                {
                    Combo_NewItem_Item.Focus();
                }
            }
        }
        private void Combo_OriginItem_Item_PreviewKeyDown10(object sender, KeyEventArgs e)
        {
            if (Key.Tab != e.Key)
            {
                System.Windows.Controls.ComboBox sendercombo = ((System.Windows.Controls.ComboBox)sender);
                Combo_KeyDown(sendercombo, sender, e);
            }
            else if (Key.Tab == e.Key)
            {
                e.Handled = true;
                if (this.Newvariable10.Command_OriginItem_InputSupport.IsEnabled == true)
                {
                    Newvariable10.Command_OriginItem_InputSupport.Focus();
                }
                else
                {
                    Combo_NewItem_Item.Focus();
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
        private void ClearSourceComboValues(int sourcevariable)
        {
            switch (sourcevariable)
            {
                case 1:

                    break;
                case 2:
                    {
                        Newvariable2.Text_OriginItem1_AnswerType.Text = string.Empty;
                        Newvariable2.Text_OriginItem1_SelectCount.Text = string.Empty;
                        Newvariable2.Text_OriginItem1_Question.Text = string.Empty;
                        Newvariable2.minmaxavg.Visibility = Visibility.Hidden;
                        Newvariable2.List_OriginItem_ChoiceList.Visibility = Visibility.Visible;
                        Newvariable2.List_OriginItem_ChoiceList.Items.Clear();
                        Newvariable2.Command_OriginItem_InputSupport.IsEnabled = false;
                        Newvariable3.Combo_OriginItem_Item.IsEnabled = false;
                        gridoriginalitem.Columns[sourcevariable - 1].IsReadOnly = true;
                        Newvariable2.minmaxavgborder.Visibility = Visibility.Hidden;
                        gridsourcevariables = ClearColumn(sourcevariable - 1, gridsourcevariables.Rows.Count, gridsourcevariables);
                        Newvariable2.Combo_OriginItem_Item.Text = string.Empty;
                    }
                    break;
                case 3:

                    {
                        gridoriginalitem.Columns[sourcevariable - 1].IsReadOnly = true;
                        Newvariable3.Text_OriginItem1_AnswerType.Text = string.Empty;
                        Newvariable3.Text_OriginItem1_SelectCount.Text = string.Empty;
                        Newvariable3.Text_OriginItem1_Question.Text = string.Empty;
                        Newvariable3.minmaxavg.Visibility = Visibility.Hidden;
                        Newvariable3.List_OriginItem_ChoiceList.Visibility = Visibility.Visible;
                        Newvariable3.List_OriginItem_ChoiceList.Items.Clear();
                        Newvariable3.Command_OriginItem_InputSupport.IsEnabled = false;
                        Newvariable4.Combo_OriginItem_Item.IsEnabled = false;
                        Newvariable3.minmaxavgborder.Visibility = Visibility.Hidden;
                        if (SourceVariableListView2[0].Choices != null)
                        {
                            SourceVariableListView2.Insert(0, NewvariableNullItem());
                        }
                        gridsourcevariables = ClearColumn(sourcevariable - 1, gridsourcevariables.Rows.Count, gridsourcevariables);
                        Newvariable3.Combo_OriginItem_Item.Text = string.Empty;
                    }
                    break;
                case 4:

                    {
                        gridoriginalitem.Columns[sourcevariable - 1].IsReadOnly = true;
                        Newvariable4.Text_OriginItem1_AnswerType.Text = string.Empty;
                        Newvariable4.Text_OriginItem1_SelectCount.Text = string.Empty;
                        Newvariable4.Text_OriginItem1_Question.Text = string.Empty;
                        Newvariable4.minmaxavg.Visibility = Visibility.Hidden;
                        Newvariable4.List_OriginItem_ChoiceList.Visibility = Visibility.Visible;
                        Newvariable4.List_OriginItem_ChoiceList.Items.Clear();
                        Newvariable4.Command_OriginItem_InputSupport.IsEnabled = false;
                        Newvariable5.Combo_OriginItem_Item.IsEnabled = false;
                        Newvariable4.minmaxavgborder.Visibility = Visibility.Hidden;
                        if (SourceVariableListView3[0].Choices != null)
                        {
                            SourceVariableListView3.Insert(0, NewvariableNullItem());
                        }
                        gridsourcevariables = ClearColumn(sourcevariable - 1, gridsourcevariables.Rows.Count, gridsourcevariables);
                        Newvariable4.Combo_OriginItem_Item.Text = string.Empty;
                    }
                    break;
                case 5:

                    {
                        gridoriginalitem.Columns[sourcevariable - 1].IsReadOnly = true;
                        Newvariable5.Text_OriginItem1_AnswerType.Text = string.Empty;
                        Newvariable5.Text_OriginItem1_SelectCount.Text = string.Empty;
                        Newvariable5.Text_OriginItem1_Question.Text = string.Empty;
                        Newvariable5.minmaxavg.Visibility = Visibility.Hidden;
                        Newvariable5.List_OriginItem_ChoiceList.Visibility = Visibility.Visible;
                        Newvariable5.List_OriginItem_ChoiceList.Items.Clear();
                        Newvariable5.Command_OriginItem_InputSupport.IsEnabled = false;
                        Newvariable6.Combo_OriginItem_Item.IsEnabled = false;
                        Newvariable5.minmaxavgborder.Visibility = Visibility.Hidden;
                        if (SourceVariableListView4[0].Choices != null)
                        {
                            SourceVariableListView4.Insert(0, NewvariableNullItem());
                        }
                        gridsourcevariables = ClearColumn(sourcevariable - 1, gridsourcevariables.Rows.Count, gridsourcevariables);
                        Newvariable5.Combo_OriginItem_Item.Text = string.Empty;
                    }
                    break;
                case 6:

                    {
                        gridoriginalitem.Columns[sourcevariable - 1].IsReadOnly = true;
                        Newvariable6.Text_OriginItem1_AnswerType.Text = string.Empty;
                        Newvariable6.Text_OriginItem1_SelectCount.Text = string.Empty;
                        Newvariable6.Text_OriginItem1_Question.Text = string.Empty;
                        Newvariable6.minmaxavg.Visibility = Visibility.Hidden;
                        Newvariable6.List_OriginItem_ChoiceList.Visibility = Visibility.Visible;
                        Newvariable6.List_OriginItem_ChoiceList.Items.Clear();
                        Newvariable6.Command_OriginItem_InputSupport.IsEnabled = false;
                        Newvariable7.Combo_OriginItem_Item.IsEnabled = false;
                        Newvariable6.minmaxavgborder.Visibility = Visibility.Hidden;
                        if (SourceVariableListView5[0].Choices != null)
                        {
                            SourceVariableListView5.Insert(0, NewvariableNullItem());
                        }
                        gridsourcevariables = ClearColumn(sourcevariable - 1, gridsourcevariables.Rows.Count, gridsourcevariables);
                        Newvariable6.Combo_OriginItem_Item.Text = string.Empty;
                    }
                    break;
                case 7:

                    {
                        gridoriginalitem.Columns[sourcevariable - 1].IsReadOnly = true;
                        Newvariable7.Text_OriginItem1_AnswerType.Text = string.Empty;
                        Newvariable7.Text_OriginItem1_SelectCount.Text = string.Empty;
                        Newvariable7.Text_OriginItem1_Question.Text = string.Empty;
                        Newvariable7.minmaxavg.Visibility = Visibility.Hidden;
                        Newvariable7.List_OriginItem_ChoiceList.Visibility = Visibility.Visible;
                        Newvariable7.List_OriginItem_ChoiceList.Items.Clear();
                        Newvariable7.Command_OriginItem_InputSupport.IsEnabled = false;
                        Newvariable8.Combo_OriginItem_Item.IsEnabled = false;
                        Newvariable7.minmaxavgborder.Visibility = Visibility.Hidden;
                        if (SourceVariableListView6[0].Choices != null)
                        {
                            SourceVariableListView6.Insert(0, NewvariableNullItem());
                        }
                        gridsourcevariables = ClearColumn(sourcevariable - 1, gridsourcevariables.Rows.Count, gridsourcevariables);
                        Newvariable7.Combo_OriginItem_Item.Text = string.Empty;
                    }
                    break;
                case 8:

                    {
                        gridoriginalitem.Columns[sourcevariable - 1].IsReadOnly = true;
                        Newvariable8.Text_OriginItem1_AnswerType.Text = string.Empty;
                        Newvariable8.Text_OriginItem1_SelectCount.Text = string.Empty;
                        Newvariable8.Text_OriginItem1_Question.Text = string.Empty;
                        Newvariable8.minmaxavg.Visibility = Visibility.Hidden;
                        Newvariable8.List_OriginItem_ChoiceList.Visibility = Visibility.Visible;
                        Newvariable8.List_OriginItem_ChoiceList.Items.Clear();
                        Newvariable8.Command_OriginItem_InputSupport.IsEnabled = false;
                        Newvariable9.Combo_OriginItem_Item.IsEnabled = false;
                        Newvariable8.minmaxavgborder.Visibility = Visibility.Hidden;
                        if (SourceVariableListView7[0].Choices != null)
                        {
                            SourceVariableListView7.Insert(0, NewvariableNullItem());
                        }
                        gridsourcevariables = ClearColumn(sourcevariable - 1, gridsourcevariables.Rows.Count, gridsourcevariables);
                        Newvariable8.Combo_OriginItem_Item.Text = string.Empty;
                    }
                    break;
                case 9:

                    {
                        gridoriginalitem.Columns[sourcevariable - 1].IsReadOnly = true;
                        Newvariable9.Text_OriginItem1_AnswerType.Text = string.Empty;
                        Newvariable9.Text_OriginItem1_SelectCount.Text = string.Empty;
                        Newvariable9.Text_OriginItem1_Question.Text = string.Empty;
                        Newvariable9.minmaxavg.Visibility = Visibility.Hidden;
                        Newvariable9.List_OriginItem_ChoiceList.Visibility = Visibility.Visible;
                        Newvariable9.List_OriginItem_ChoiceList.Items.Clear();
                        Newvariable9.Command_OriginItem_InputSupport.IsEnabled = false;
                        Newvariable10.Combo_OriginItem_Item.IsEnabled = false;
                        Newvariable9.minmaxavgborder.Visibility = Visibility.Hidden;
                        if (SourceVariableListView8[0].Choices != null)
                        {
                            SourceVariableListView8.Insert(0, NewvariableNullItem());
                        }
                        gridsourcevariables = ClearColumn(sourcevariable - 1, gridsourcevariables.Rows.Count, gridsourcevariables);
                        Newvariable9.Combo_OriginItem_Item.Text = string.Empty;
                    }
                    break;
                case 10:

                    {
                        gridoriginalitem.Columns[sourcevariable - 1].IsReadOnly = true;
                        Newvariable10.Text_OriginItem1_AnswerType.Text = string.Empty;
                        Newvariable10.Text_OriginItem1_SelectCount.Text = string.Empty;
                        Newvariable10.Text_OriginItem1_Question.Text = string.Empty;
                        Newvariable10.minmaxavg.Visibility = Visibility.Hidden;
                        Newvariable10.List_OriginItem_ChoiceList.Visibility = Visibility.Visible;
                        Newvariable10.List_OriginItem_ChoiceList.Items.Clear();
                        Newvariable10.Command_OriginItem_InputSupport.IsEnabled = false;
                        Newvariable10.minmaxavgborder.Visibility = Visibility.Hidden;
                        if (SourceVariableListView9[0].Choices != null)
                        {
                            SourceVariableListView9.Insert(0, NewvariableNullItem());
                        }
                        gridsourcevariables = ClearColumn(sourcevariable - 1, gridsourcevariables.Rows.Count, gridsourcevariables);
                        Newvariable10.Combo_OriginItem_Item.Text = string.Empty;
                    }
                    break;
            }
        }
        private void DisableRightMenu(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
        private void HandleCopyPaste(object sender, KeyEventArgs e)
        {
            var uiElement = e.OriginalSource as UIElement;
            try
            {
                Util.QS.GridCopyPaste copyPaste = new Util.QS.GridCopyPaste();
                var data = copyPaste.PastetoDatagrid(sender);
                DataGrid integratenewvariablesourcegrid = new DataGrid();
                if (((DataGrid)sender).Name == gridnewvariable.Name)
                {
                    integratenewvariablesourcegrid = gridnewvariable;
                }
                else
                {
                    integratenewvariablesourcegrid = gridoriginalitem;
                }
                int datagridColumn = integratenewvariablesourcegrid.CurrentCell.Column.DisplayIndex;
                DataGridCell cell = frmutil.GetCell(integratenewvariablesourcegrid, integratenewvariablesourcegrid.SelectedIndex, datagridColumn);
                if (!cell.IsEditing)
                {
                    e.Handled = true;
                    int No_Row = copyPaste.No_Row;
                    int No_Column = copyPaste.No_Columns;
                    if (data != null)
                    {
                        e.Handled = true;
                        if (((DataGrid)sender).Name == gridnewvariable.Name)
                        {
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
                        }
                        else
                        {
                            int datagridRow = gridoriginalitem.SelectedIndex;
                            if (gridoriginalitem.CurrentCell.Column.DisplayIndex >= 0 && gridoriginalitem.CurrentCell.Column.DisplayIndex <= (QC4Common.Common.Constants.Integrate_SourceVariable_Column_Count - 1) && gridoriginalitem.CurrentCell.Column.IsReadOnly == false)
                            {
                                //selection in choice   
                                if (No_Column > QC4Common.Common.Constants.Integrate_SourceVariable_Column_Count || No_Column > QC4Common.Common.Constants.Integrate_SourceVariable_Column_Count - gridoriginalitem.CurrentCell.Column.DisplayIndex || No_Row > gridoriginalitem.Items.Count - (gridoriginalitem.SelectedIndex) + 1)//10 row of grid 
                                {
                                    MessageDialog.ErrorOk(LocalResource.COPY_ERROR_MSG);
                                    e.Handled = true;
                                }
                                else
                                {
                                    int RowIndex = gridoriginalitem.SelectedIndex;
                                    for (int i = 0; i < No_Row; i++, RowIndex++)
                                    {
                                        for (int j = 1, col = gridoriginalitem.CurrentCell.Column.DisplayIndex; j <= No_Column && col < QC4Common.Common.Constants.Integrate_SourceVariable_Column_Count; j++, col++)
                                        {
                                            if (gridoriginalitem.Columns[col].IsReadOnly == false)
                                            {
                                                gridsourcevariables.Rows[RowIndex][col] = Convert.ToString(data[i, (j - 1)]);
                                            }
                                        }

                                    }

                                }
                            }
                        }




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
        private void Command_OriginItem_InputSupport_PreviewKeyDown10(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                this.Combo_NewItem_Item.Focus();
            }
        }
        private void Check_After_Unfall_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                if (Command_Entry.IsEnabled == true)
                { Command_Entry.Focus(); }
                else
                {
                    Command_Cancel.Focus();
                }

            }
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
                gridchoice.Rows[gridnewvariable.SelectedIndex][2] = false;
                //frmutil.SetErrorForGrid(gridnewvariable, gridnewvariable.SelectedIndex, gridnewvariable.CurrentCell.Column.DisplayIndex, QC4Common.Common.Constants.STD_DP.Background, true);
                frmutil.SetErrorForGrid(gridnewvariable, gridnewvariable.SelectedIndex, gridnewvariable.CurrentCell.Column.DisplayIndex, QC4Common.Common.Constants.STD_DP.Foreground, true);
            }
        }

        private void Gridoriginalitem_IsMouseCaptureWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (gridoriginalitem != null && gridoriginalitem.CurrentCell != null && gridoriginalitem.CurrentCell.Column != null)//CHECK WHETHER TO USE DISPLAY INDEX
            {
                frmutil.SetErrorForGrid(gridoriginalitem, gridoriginalitem.SelectedIndex, gridoriginalitem.CurrentCell.Column.DisplayIndex, QC4Common.Common.Constants.STD_DP.Background, true);
                frmutil.SetErrorForGrid(gridoriginalitem, gridoriginalitem.SelectedIndex, gridoriginalitem.CurrentCell.Column.DisplayIndex, QC4Common.Common.Constants.STD_DP.Foreground, true);
            }
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            isLoaded = true;
        }

        private void Gridnewvariable_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (((DataGrid)sender).Name == gridnewvariable.Name)
            {
                gridoriginalitem.SelectedIndex = gridnewvariable.SelectedIndex;
            }
            else
            {
                gridnewvariable.SelectedIndex = gridoriginalitem.SelectedIndex;
            }
            scroll = true;
            mouseclick = true;
        }

        private void Gridnewvariable_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }

        private void Command_NewItem_Input_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                scrolltofirst = true;
                gridnewvariable.Focus();

            }
        }

        private void Combo_NewItem_SelectCount_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                if (Command_NewItem_Input.IsEnabled == true)
                {
                    Command_NewItem_Input.Focus();
                    e.Handled = true;
                }
                else { scrolltofirst = true; gridnewvariable.Focus(); e.Handled = true; }

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

        private void Option_OR_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Key.Tab == e.Key)
            {
                this.Newvariable1.Combo_OriginItem_Item.Focus();
                e.Handled = true;
            }
        }

        private void Gridnewvariable_MouseWheel(object sender, MouseWheelEventArgs e)
        {

            if (e.Delta > 0)
            {
                // The user scrolled up.
                scroll = true;
            }
            else
            {
                // The user scrolled down.
                scroll = true;
            }
        }

        private void Gridnewvariable_MouseUp_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void Gridnewvariable_PreviewKeyUp_1(object sender, KeyEventArgs e)
        {

        }

        private void Gridnewvariable_KeyDown(object sender, KeyEventArgs e)
        {
            scroll = false;

        }

        private void Gridnewvariable_KeyUp(object sender, KeyEventArgs e)
        {
            scroll = true;
            mouseclick = true;
        }

        private void Gridnewvariable_MouseDown(object sender, MouseButtonEventArgs e)
        {
            scroll = false;
            mouseclick = true;
        }

        private void Gridoriginalitem_PreviewKeyUp(object sender, KeyEventArgs e)
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
                int datagridColumn = gridoriginalitem.CurrentCell.Column.DisplayIndex;
                DataGridCell cell = frmutil.GetCell(gridoriginalitem, gridoriginalitem.SelectedIndex, datagridColumn);
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
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                Button_Help.Focus();
            }
        }
        private void Button_Help_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                Option_AND.Focus();
            }
        }
    }
}
