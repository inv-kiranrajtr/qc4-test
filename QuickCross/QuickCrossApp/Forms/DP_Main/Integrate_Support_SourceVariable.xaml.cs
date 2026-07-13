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
using System.Data;
using Qc4Launcher.Forms.GrossTabulationSetting.Common;
using log4net;
using System.Reflection;
using ExcelAddIn.Common;

namespace Qc4Launcher.Forms.DP_Main
{
    /// <summary>
    /// Interaction logic for Integrate_Support_SourceVariable.xaml
    /// </summary>
    public partial class Integrate_Support_SourceVariable : Window
    {
        string title = string.Empty;
        public static DataTable gridchoicevalues;
        public static bool issave = false;
        QC4Common.Util.FormUtil frmutil = new QC4Common.Util.FormUtil();
        int supportrowcount = -1;
        string supportanswertype = string.Empty;
        int supportnoofchoices = 0;
        DataTable dtchoice;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        System.Windows.Controls.DataGrid ExpGrid = null;
        int arrowbuttonrowindex = 0;
        string clipboardText = "";
        public class OperatorsList : List<string>
        {
            public OperatorsList()
            {
                this.Add(LocalResource.GTT_FS_CB_ITEM_VALUE_EQUALS);
                this.Add(LocalResource.GTT_FS_CB_ITEM_VALUE_LESS_OR_GREATER);
                //cmb.Items.Add(LocalResource.GTT_FS_CB_ITEM_VALUE_EQUALS);
                //cmb.Items.Add(LocalResource.GTT_FS_CB_ITEM_VALUE_LESS_OR_GREATER);
            }
        }
        public Integrate_Support_SourceVariable(string sourcevariablelabel, string sourcevariablename, string answertype, List<string> choices, double min, double max, double avg, int rowcount, DataTable choicecolumn, DataTable criteriavaluescolumn)//title list of choices ,choice count
        {
            InitializeComponent();
            try
            {


                title = string.Format("{0} [ {1} ]", sourcevariablelabel, sourcevariablename);
                Label_OriginItem_Item.Text = sourcevariablelabel;
                Combo_OriginItem_Item.Text = sourcevariablename;
                DataTable dtcriteriavalues = CreateriteriavalueTable();
                supportrowcount = rowcount;
                supportanswertype = answertype;
                if (answertype.Equals(Util.Constants.AnswerType.SA) || answertype.Equals(Util.Constants.AnswerType.MA))
                {
                    supportnoofchoices = choices.Count;
                    minmaxavg.Visibility = Visibility.Hidden;
                    minmaxavgborder.Visibility = Visibility.Hidden;
                    List_OriginItem_Item.Visibility = Visibility.Visible;
                    dtchoice = CreateChoiceTable();
                    foreach (string choice in choices)
                    {
                        DataRow drchoice;
                        drchoice = dtchoice.NewRow();
                        try
                        {
                            drchoice["SL"] = (choice.Split(':'))[0];
                            drchoice["Choice"] = (choice.Split(':'))[1];
                            dtchoice.Rows.Add(drchoice);
                        }
                        catch { }
                        // List_OriginItem_Item.Items.Add(choice);
                    } //List_OriginItem_Item.DataContext = choices;
                    List_OriginItem_Item.DataContext = dtchoice;
                    gridcriteriavalues.Columns[0].Visibility = Visibility.Visible;
                    gridcriteriavalues.Columns[1].Visibility = Visibility.Visible;
                    gridcriteriavalues.Columns[2].Visibility = Visibility.Visible;
                    gridcriteriavalues.Columns[3].Visibility = Visibility.Visible;
                    gridcriteriavalues.Columns[4].Visibility = Visibility.Visible;
                    gridcriteriavalues.Columns[5].Visibility = Visibility.Hidden;
                    gridcriteriavalues.Columns[6].Visibility = Visibility.Hidden;
                    gridcriteriavalues.Columns[7].Visibility = Visibility.Hidden;
                }
                else if (answertype.Equals(Util.Constants.AnswerType.N))
                {
                    minmaxavg.Visibility = Visibility.Visible;
                    minmaxavgborder.Visibility = Visibility.Visible;
                    List_OriginItem_Item.Visibility = Visibility.Hidden;
                    Text_OriginItem_Min.Text = min.ToString();
                    Text_OriginItem_Max.Text = max.ToString();
                    Text_OriginItem_Avg.Text = avg.ToString();

                    gridcriteriavalues.Columns[0].Visibility = Visibility.Hidden;
                    gridcriteriavalues.Columns[1].Visibility = Visibility.Visible;
                    gridcriteriavalues.Columns[2].Visibility = Visibility.Visible;
                    gridcriteriavalues.Columns[3].Visibility = Visibility.Hidden;
                    gridcriteriavalues.Columns[4].Visibility = Visibility.Hidden;
                    gridcriteriavalues.Columns[5].Visibility = Visibility.Visible;
                    gridcriteriavalues.Columns[6].Visibility = Visibility.Visible;
                    gridcriteriavalues.Columns[7].Visibility = Visibility.Visible;
                }
                LoadGridValues(rowcount, choicecolumn, criteriavaluescolumn);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void Window_Load(object sender, RoutedEventArgs e)
        {
            this.Title = title;
            try { gridcriteriavalues.Focus(); } catch { }
        }

        private void Command_Cancel_Click(object sender, RoutedEventArgs e)
        {
            issave = false;
            this.Close();
        }
        private DataTable CreateriteriavalueTable()
        {
            DataTable griddata = new DataTable();
            griddata.Columns.Add("Button");
            griddata.Columns.Add("SL");
            griddata.Columns.Add("Choice");
            griddata.Columns.Add("Operator");
            griddata.Columns.Add("CriteriaValue");
            griddata.Columns.Add("LowerLimit");
            griddata.Columns.Add("Seperator");
            griddata.Columns.Add("UpperLimit");
            return griddata;
        }
        private DataTable CreateChoiceTable()
        {
            DataTable griddata = new DataTable();
            griddata.Columns.Add("SL");
            griddata.Columns.Add("Choice");
            return griddata;
        }
        private void LoadGridValues(int rowcount, DataTable choicecolumn, DataTable criteriavaluescolumn)
        {
            //DataTable gridchoicevalues = CreateriteriavalueTable();
            gridchoicevalues = CreateriteriavalueTable();
            DataRow drgridchoicevalues;
            for (int i = 0; i < rowcount; i++)
            {
                drgridchoicevalues = gridchoicevalues.NewRow();
                try
                {
                    string nchoicevalue = string.Empty;
                    string LowerLimit = string.Empty;
                    string UpperLimit = string.Empty;
                    string criteriavalue = string.Empty;
                    string operatorvalue = null;
                    string not = string.Empty;
                    try
                    {
                        nchoicevalue = (Convert.ToString(criteriavaluescolumn.Rows[i][0]));
                        if (supportanswertype == QC4Common.Common.Constants.AnswerType.N)
                        {
                            not = frmutil.GetOperator(nchoicevalue);
                            nchoicevalue = frmutil.GetNtypeValueSeperatedByComma(Convert.ToString(criteriavaluescolumn.Rows[i][0]));// Convert.ToString(criteriavaluescolumn.Rows[i][0]).Split('-');
                        }
                        if (Convert.ToString(criteriavaluescolumn.Rows[i][0]).StartsWith("="))
                        {
                            operatorvalue = LocalResource.GTT_FS_CB_ITEM_VALUE_EQUALS;// "=";
                            nchoicevalue = frmutil.TrimStartEqualNotequal(nchoicevalue);
                        }
                        if (Convert.ToString(criteriavaluescolumn.Rows[i][0]).StartsWith("<>") || Convert.ToString(criteriavaluescolumn.Rows[i][0]).StartsWith("!"))
                        {
                            operatorvalue = LocalResource.GTT_FS_CB_ITEM_VALUE_LESS_OR_GREATER;// "<>";
                            nchoicevalue = frmutil.TrimStartEqualNotequal(nchoicevalue);
                        }
                        string[] values = nchoicevalue.Split(',');
                        LowerLimit = values[0];
                        if (supportanswertype == QC4Common.Common.Constants.AnswerType.N && !string.IsNullOrEmpty(LowerLimit) && !string.IsNullOrEmpty(not))
                        {
                            LowerLimit = not + LowerLimit;
                        }
                        if (values.Length == 2)
                        {
                            UpperLimit = values[1];
                        }
                    }
                    catch { }
                    //ComboBox cmbBox = new ComboBox();
                    //cmbBox.Items.Add("=");
                    //cmbBox.Items.Add("<>");
                    drgridchoicevalues["Button"] = string.Empty;
                    drgridchoicevalues["SL"] = (i + 1).ToString();
                    drgridchoicevalues["Choice"] = Convert.ToString(choicecolumn.Rows[i][0]);// string.Empty;
                    drgridchoicevalues["Operator"] = operatorvalue;// (Convert.ToString(criteriavaluescolumn.Rows[i][0])).StartsWith("=") ? "=" : (Convert.ToString(criteriavaluescolumn.Rows[i][0])).StartsWith("<>") ? "<>" : string.Empty
                    drgridchoicevalues["CriteriaValue"] = nchoicevalue;// Convert.ToString(criteriavaluescolumn.Rows[i][0]);// string.Empty;
                    drgridchoicevalues["LowerLimit"] = LowerLimit;// string.Empty;
                    drgridchoicevalues["Seperator"] = Qc4Launcher.LocalResource.GRID_LBL_INTEGRATE_SEPERATOR; //Qc4Launcher.LocalResource.GRID_LBL_INTEGRATE_SEPERATOR;"{x:Static res:LocalResource.LBL_SEPERATOR_FOR_DISPLAY}"
                    drgridchoicevalues["UpperLimit"] = UpperLimit;// string.Empty;  
                    gridchoicevalues.Rows.Add(drgridchoicevalues);

                }
                catch (Exception ex)
                {
                    _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                }
            }
            gridcriteriavalues.DataContext = gridchoicevalues;
            // return gridchoicevalues;
        }

        private void Command_Entry_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < supportrowcount; i++)
            {
                string cellvalue1 = string.Empty;
                string cellvalue2 = string.Empty;
                if (supportanswertype == QC4Common.Common.Constants.AnswerType.SA || supportanswertype == QC4Common.Common.Constants.AnswerType.MA)
                {
                    cellvalue1 = Convert.ToString(gridchoicevalues.Rows[i][4]);
                    string operatorstring = frmutil.GetOperator_in_string(cellvalue1);
                    if (!string.IsNullOrEmpty(operatorstring))
                    {
                        ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + string.Format(LocalResource.ERR_MSG_OPERATOR_INCLUDED_IN_CRITERIA, operatorstring));
                        frmutil.SetErrorForGrid(gridcriteriavalues, i, 4, QC4Common.Common.Constants.STD_DP.Background);
                        return;
                    }
                    else
                    {
                        frmutil.SetErrorForGrid(gridcriteriavalues, i, 4, QC4Common.Common.Constants.STD_DP.Background, true);
                    }
                    //////string choices = frmutil.GetCommaSeperated(Convert.ToString(gridchoicevalues.Rows[i][4]), supportnoofchoices, false);
                    //////if (!frmutil.CheckRangeExceeds(choices, supportnoofchoices))// if (!frmutil.CheckRangeExceeds(QC4Common.Util.FormUtil.commaseperatedvalues, supportnoofchoices))
                    //////{
                    //////    ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + string.Format(LocalResource.ERR_MSG_INTEGRATE_SET_INTEGER_RANGE_OF, "1", supportnoofchoices.ToString()));
                    //////    return;
                    //////}
                    //////QC4Common.Util.FormUtil.commaseperatedvalues = string.Empty;
                }
                else if (supportanswertype == QC4Common.Common.Constants.AnswerType.N)
                {
                    cellvalue1 = Convert.ToString(gridchoicevalues.Rows[i][5]);
                    cellvalue2 = Convert.ToString(gridchoicevalues.Rows[i][7]);
                    //if (!frmutil.IsNumeric(Convert.ToString(gridchoicevalues.Rows[i][5])) || frmutil.IsNumeric(Convert.ToString(gridchoicevalues.Rows[i][7])))
                    //{
                    //    ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_SET_A_NUMERIC_VALUE));
                    //    return;
                    //}
                    if ((cellvalue1).Contains("/") || (cellvalue1).Contains(","))
                    {
                        //New variable: input of [1] is invalid. Cannot specify split range
                        ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_CANNOT_SPECIFY_SPLIT_RANGE));
                        frmutil.SetErrorForGrid(gridcriteriavalues, i, 5, QC4Common.Common.Constants.STD_DP.Foreground);
                        return;

                    }
                    else
                    {
                        frmutil.SetErrorForGrid(gridcriteriavalues, i, 5, QC4Common.Common.Constants.STD_DP.Foreground, true);
                    }
                    if ((cellvalue2).Contains("/") || (cellvalue2).Contains(","))
                    {
                        //New variable: input of [1] is invalid. Cannot specify split range
                        ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_CANNOT_SPECIFY_SPLIT_RANGE));
                        frmutil.SetErrorForGrid(gridcriteriavalues, i, 7, QC4Common.Common.Constants.STD_DP.Foreground);
                        return;

                    }
                    else
                    {
                        frmutil.SetErrorForGrid(gridcriteriavalues, i, 7, QC4Common.Common.Constants.STD_DP.Foreground, true);
                    }
                    ////IsMultipleLimit
                    //if (!string.IsNullOrEmpty(cellvalue1)&& !frmutil.ValidateRegex(cellvalue1))//(!frmutil.IsLimitPresent(cellvalue1))
                    //{

                    //    ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_MULTIPLE_LIMIT_SET));
                    //    frmutil.SetErrorForGrid(gridcriteriavalues, i, 5, QC4Common.Common.Constants.STD_DP.Foreground);
                    //    return;

                    //}
                    //else
                    //{
                    //    frmutil.SetErrorForGrid(gridcriteriavalues, i, 5, QC4Common.Common.Constants.STD_DP.Foreground, true);
                    //}
                    //if (!string.IsNullOrEmpty(cellvalue2) && !frmutil.ValidateRegex(cellvalue2))//(!frmutil.IsLimitPresent(cellvalue2))
                    //{

                    //    ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_MULTIPLE_LIMIT_SET));
                    //    frmutil.SetErrorForGrid(gridcriteriavalues, i, 7, QC4Common.Common.Constants.STD_DP.Foreground);
                    //    return;

                    //}
                    //else
                    //{
                    //    frmutil.SetErrorForGrid(gridcriteriavalues, i, 7, QC4Common.Common.Constants.STD_DP.Foreground, true);
                    //}
                    if (!frmutil.IsNotOtherThanStart(cellvalue1))//not otherthan start
                    {

                        ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + string.Format(LocalResource.ERR_MSG_INTEGRATE_CANNOT_SET_ELSEWHERE_THAN_AT_START, "!"));
                        frmutil.SetErrorForGrid(gridcriteriavalues, i, 5, QC4Common.Common.Constants.STD_DP.Foreground);
                        return;

                    }
                    else
                    {
                        frmutil.SetErrorForGrid(gridcriteriavalues, i, 5, QC4Common.Common.Constants.STD_DP.Foreground, true);
                    }

                    if (!frmutil.NotExistsInString(cellvalue2))//not in upper value
                    {

                        ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + string.Format(LocalResource.ERR_MSG_INTEGRATE_CANNOT_SET_ELSEWHERE_THAN_AT_START, "!"));
                        frmutil.SetErrorForGrid(gridcriteriavalues, i, 7, QC4Common.Common.Constants.STD_DP.Foreground);
                        return;

                    }
                    else
                    {
                        frmutil.SetErrorForGrid(gridcriteriavalues, i, 7, QC4Common.Common.Constants.STD_DP.Foreground, true);
                    }

                }
                int column = 5;
                if (supportanswertype == QC4Common.Common.Constants.AnswerType.SA || supportanswertype == QC4Common.Common.Constants.AnswerType.MA)
                { column = 4; }
                if (!frmutil.IsNumeric(cellvalue1))
                {
                    ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_SET_A_NUMERIC_VALUE));
                    frmutil.SetErrorForGrid(gridcriteriavalues, i, column, QC4Common.Common.Constants.STD_DP.Foreground);
                    return;
                }
                else { frmutil.SetErrorForGrid(gridcriteriavalues, i, column, QC4Common.Common.Constants.STD_DP.Foreground, true); }
                if (supportanswertype == QC4Common.Common.Constants.AnswerType.N)
                {
                    if (!frmutil.IsNumeric(cellvalue2))
                    {
                        ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_SET_A_NUMERIC_VALUE));
                        frmutil.SetErrorForGrid(gridcriteriavalues, i, 7, QC4Common.Common.Constants.STD_DP.Foreground);
                        return;
                    }
                    else { frmutil.SetErrorForGrid(gridcriteriavalues, i, 7, QC4Common.Common.Constants.STD_DP.Foreground, true); }


                }
                //if (!frmutil.IsMultipleLimit(cellvalue1))
                //{

                //    ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_MULTIPLE_LIMIT_SET));
                //    frmutil.SetErrorForGrid(gridcriteriavalues, i, column, QC4Common.Common.Constants.STD_DP.Foreground);
                //    return;
                //}
                //else { frmutil.SetErrorForGrid(gridcriteriavalues, i, column, QC4Common.Common.Constants.STD_DP.Foreground, true); }
                if (supportanswertype == QC4Common.Common.Constants.AnswerType.N)
                {
                    //IsMultipleLimit
                    if (!string.IsNullOrEmpty(cellvalue1) && !frmutil.ValidateRegex(cellvalue1))//(!frmutil.IsLimitPresent(cellvalue1))
                    {

                        ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_MULTIPLE_LIMIT_SET));
                        frmutil.SetErrorForGrid(gridcriteriavalues, i, 5, QC4Common.Common.Constants.STD_DP.Foreground);
                        return;

                    }
                    else
                    {
                        frmutil.SetErrorForGrid(gridcriteriavalues, i, 5, QC4Common.Common.Constants.STD_DP.Foreground, true);
                    }
                    if (!string.IsNullOrEmpty(cellvalue2) && !frmutil.ValidateRegex(cellvalue2))//(!frmutil.IsLimitPresent(cellvalue2))
                    {

                        ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_MULTIPLE_LIMIT_SET));
                        frmutil.SetErrorForGrid(gridcriteriavalues, i, 7, QC4Common.Common.Constants.STD_DP.Foreground);
                        return;

                    }
                    else
                    {
                        frmutil.SetErrorForGrid(gridcriteriavalues, i, 7, QC4Common.Common.Constants.STD_DP.Foreground, true);
                    }
                    if (!frmutil.IsMultipleLimit(cellvalue1))
                    {

                        ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_MULTIPLE_LIMIT_SET));
                        frmutil.SetErrorForGrid(gridcriteriavalues, i, column, QC4Common.Common.Constants.STD_DP.Foreground);
                        return;
                    }
                    else { frmutil.SetErrorForGrid(gridcriteriavalues, i, column, QC4Common.Common.Constants.STD_DP.Foreground, true); }
                    if (!frmutil.IsMultipleLimit(cellvalue2))
                    {

                        ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_MULTIPLE_LIMIT_SET));
                        frmutil.SetErrorForGrid(gridcriteriavalues, i, 7, QC4Common.Common.Constants.STD_DP.Foreground);
                        return;
                    }
                    else { frmutil.SetErrorForGrid(gridcriteriavalues, i, 7, QC4Common.Common.Constants.STD_DP.Foreground, true); }
                }
                if (!frmutil.GetAllranges(cellvalue1))
                {
                    ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_SET_LOWER_LIMIT_LESS_THAN_UPPER_LIMIT));
                    frmutil.SetErrorForGrid(gridcriteriavalues, i, column, QC4Common.Common.Constants.STD_DP.Foreground);
                    return;
                }
                else { frmutil.SetErrorForGrid(gridcriteriavalues, i, column, QC4Common.Common.Constants.STD_DP.Foreground, true); }
                if (supportanswertype == QC4Common.Common.Constants.AnswerType.N)
                {
                    if (!frmutil.IsMultipleLimit(Convert.ToString(cellvalue1)) || (!string.IsNullOrEmpty(cellvalue1) && !frmutil.ValidateRegexForNType(Convert.ToString(cellvalue1))))
                    {
                        //source variable multiple separartor
                        //New variable: input of [2] is invalid. Multiple [-]s are set.
                        MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_MULTIPLE_LIMIT_SET));
                        frmutil.SetErrorForGrid(gridcriteriavalues, i, 5, QC4Common.Common.Constants.STD_DP.Foreground);

                        return;
                    }
                    else //if (lasterrorrow == j && lasterrorcolumn == i)
                    {
                        frmutil.SetErrorForGrid(gridcriteriavalues, i, 5, QC4Common.Common.Constants.STD_DP.Foreground, true);
                    }
                    if (!frmutil.IsMultipleLimit(Convert.ToString(cellvalue2))|| (!string.IsNullOrEmpty(cellvalue2) && !frmutil.ValidateRegexForNType(Convert.ToString(cellvalue2))))
                    {
                        //source variable multiple separartor
                        //New variable: input of [2] is invalid. Multiple [-]s are set.
                        MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_MULTIPLE_LIMIT_SET));
                        frmutil.SetErrorForGrid(gridcriteriavalues, i, 7, QC4Common.Common.Constants.STD_DP.Foreground);
                       
                        return ;
                    }
                    else //if (lasterrorrow == j && lasterrorcolumn == i)
                    {
                        frmutil.SetErrorForGrid(gridcriteriavalues, i, 7, QC4Common.Common.Constants.STD_DP.Foreground, true);
                    }
                    if (!frmutil.GetAllranges(cellvalue2))
                    {
                        ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_SET_LOWER_LIMIT_LESS_THAN_UPPER_LIMIT));
                        frmutil.SetErrorForGrid(gridcriteriavalues, i, 7, QC4Common.Common.Constants.STD_DP.Foreground);
                        return;
                    }
                    else { frmutil.SetErrorForGrid(gridcriteriavalues, i, 7, QC4Common.Common.Constants.STD_DP.Foreground, true); }
                }


                if (supportanswertype == QC4Common.Common.Constants.AnswerType.SA || supportanswertype == QC4Common.Common.Constants.AnswerType.MA)
                {
                    cellvalue1 = Convert.ToString(gridchoicevalues.Rows[i][4]);
                    string choices = frmutil.GetCommaSeperated(cellvalue1, supportnoofchoices, false);
                    if (!frmutil.CheckRangeExceeds(choices, supportnoofchoices))//QC4Common.Util.FormUtil.commaseperatedvalues
                    {
                        string firststring = string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, (i + 1));
                        string laststring = string.Format(LocalResource.ERR_MSG_INTEGRATE_SET_INTEGER_RANGE_OF, "1", supportnoofchoices.ToString());
                        ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format("{0} \n   {1}", firststring, laststring));
                        frmutil.SetErrorForGrid(gridcriteriavalues, i, 4, QC4Common.Common.Constants.STD_DP.Foreground);
                        return;
                    }
                    else { frmutil.SetErrorForGrid(gridcriteriavalues, i, 4, QC4Common.Common.Constants.STD_DP.Foreground, true); }
                    if (!frmutil.CheckRangeExceeds(QC4Common.Util.FormUtil.commaseperatedvalues, supportnoofchoices))
                    {
                        //source variable value exceeds choice limit
                        //New variable: input of [2] is invalid. Set an integer in the range of [1-3].
                        MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + string.Format(LocalResource.ERR_MSG_INTEGRATE_SET_INTEGER_RANGE_OF, "1", supportnoofchoices.ToString()));
                        frmutil.SetErrorForGrid(gridcriteriavalues, i, 4, QC4Common.Common.Constants.STD_DP.Foreground);
                        
                        return ;
                    }
                    else //if (lasterrorrow == j && lasterrorcolumn == i)
                    {
                        frmutil.SetErrorForGrid(gridcriteriavalues, i, 4, QC4Common.Common.Constants.STD_DP.Foreground, true);
                    }
                    QC4Common.Util.FormUtil.commaseperatedvalues = string.Empty;
                    if (!string.IsNullOrEmpty(cellvalue1) && !frmutil.ValidateRange(cellvalue1, 1, supportnoofchoices))
                    {
                        MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_MULTIPLE_LIMIT_SET));
                        frmutil.SetErrorForGrid(gridcriteriavalues, i, 4, QC4Common.Common.Constants.STD_DP.Foreground);
                        return;
                    }
                    else { frmutil.SetErrorForGrid(gridcriteriavalues, i, 4, QC4Common.Common.Constants.STD_DP.Foreground, true); }
                }
                else if (supportanswertype == QC4Common.Common.Constants.AnswerType.N)
                {
                    cellvalue1 = Convert.ToString(gridchoicevalues.Rows[i][5]);
                    cellvalue2 = Convert.ToString(gridchoicevalues.Rows[i][7]);
                    if ((cellvalue1).Contains("/") || (cellvalue1).Contains(","))
                    {
                        ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_CANNOT_SPECIFY_SPLIT_RANGE));
                        frmutil.SetErrorForGrid(gridcriteriavalues, i, 5, QC4Common.Common.Constants.STD_DP.Foreground);
                        return;
                    }
                    else { frmutil.SetErrorForGrid(gridcriteriavalues, i, 5, QC4Common.Common.Constants.STD_DP.Foreground, true); }
                    if ((cellvalue2).Contains("/") || (cellvalue2).Contains(","))
                    {
                        ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_CANNOT_SPECIFY_SPLIT_RANGE));
                        frmutil.SetErrorForGrid(gridcriteriavalues, i, 7, QC4Common.Common.Constants.STD_DP.Foreground);
                        return;
                    }
                    else { frmutil.SetErrorForGrid(gridcriteriavalues, i, 7, QC4Common.Common.Constants.STD_DP.Foreground, true); }
                    if (!frmutil.IsLowerLimitLesser(cellvalue1, cellvalue2))
                    {
                        ExcelAddIn.Common.MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_INTEGRATE_NEW_VARIABLE_INVALID_CHOICE_INPUT, LocalResource.LABEL_NEW_VARIABLE, i + 1) + "\n" + (LocalResource.ERR_MSG_INTEGRATE_SET_LOWER_LIMIT_LESS_THAN_UPPER_LIMIT));
                        frmutil.SetErrorForGrid(gridcriteriavalues, i, 5, QC4Common.Common.Constants.STD_DP.Foreground);
                        frmutil.SetErrorForGrid(gridcriteriavalues, i, 7, QC4Common.Common.Constants.STD_DP.Foreground);
                        return;
                    }
                    else
                    {
                        frmutil.SetErrorForGrid(gridcriteriavalues, i, 5, QC4Common.Common.Constants.STD_DP.Foreground, true);
                        frmutil.SetErrorForGrid(gridcriteriavalues, i, 7, QC4Common.Common.Constants.STD_DP.Foreground, true);
                    }

                }

            }
            issave = true;
            this.Close();
        }

        private void List_OriginItem_Item_MouseEnter(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                ListBoxItem lbi = sender as ListBoxItem;
                lbi.IsSelected = true;
                lbi.Focus();
                List_OriginItem_Item.SelectedItems.Add(lbi);
            }
        }
        private TargetType GetParent<TargetType>(DependencyObject o) where TargetType : DependencyObject
        {
            if (o == null || o is TargetType) return (TargetType)o;
            return GetParent<TargetType>(VisualTreeHelper.GetParent(o));
        }
        private void Btn_RightArrow_Click(object sender, RoutedEventArgs e)
        {
            // MessageBox.Show("NOT IMPLEMENTED");
            //////////// //DataGridCell gridcell = gridcriteriavalues.CurrentCell;
            //////////// //DataGridCellInfo currentCell = gridcriteriavalues.CurrentCell;
            ///


            int currentrow = -1;
            try
            {
                currentrow = gridcriteriavalues.SelectedIndex;
                if (currentrow > -1)
                {
                    //if (InputManager.Current.MostRecentInputDevice is KeyboardDevice)
                    //{
                    //    var row = GetParent<DataGridRow>((Button)sender);
                    //    var index = gridcriteriavalues.Items.IndexOf(row.Item);
                    //    gridcriteriavalues.ScrollIntoView(gridcriteriavalues.Items[1]);
                    //}



                    int[] choiceselectedlist = new int[List_OriginItem_Item.SelectedItems.Count];
                    // int pos = 0;
                    for (int i = 0; i < List_OriginItem_Item.SelectedItems.Count; i++)
                    {
                        var presentRow = (System.Data.DataRowView)List_OriginItem_Item.SelectedItems[i];

                        choiceselectedlist[i] = Convert.ToInt32(presentRow.Row.ItemArray[0].ToString());

                    }
                    //foreach (string item in List_OriginItem_Item.SelectedItems)
                    //{

                    //    choiceselectedlist[pos] = Convert.ToInt32((item.Split(':'))[0]);
                    //    pos++;

                    //}
                    string choices = frmutil.GetCountMean(choiceselectedlist, choiceselectedlist.Length);
                    if (!string.IsNullOrEmpty(choices))
                    {
                        gridchoicevalues.Rows[currentrow]["Operator"] = LocalResource.GTT_FS_CB_ITEM_VALUE_EQUALS;
                        gridchoicevalues.Rows[currentrow]["CriteriaValue"] = choices;// frmutil.GetCountMean(choiceselectedlist, choiceselectedlist.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }




            ////////////(currentCell.Item as gridchoicevalues).Choices = choiceCriteria.Choices;
            ////////////(currentCell.Item as ChoiceList).Criteria = val1;
            ////////////(currentCell.Item as ChoiceList).SelectedOperator = "=";
            //////////// CollectionViewSource.GetDefaultView(gridcriteriavalues.ItemsSource).Refresh();
        }

        private void LoadCombo_Equal_NotEqual(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Controls.ComboBox cmb = (System.Windows.Controls.ComboBox)sender;
                if (!cmb.Items.Contains(LocalResource.GTT_FS_CB_ITEM_VALUE_EQUALS))
                    cmb.Items.Add(LocalResource.GTT_FS_CB_ITEM_VALUE_EQUALS);
                if (!cmb.Items.Contains(LocalResource.GTT_FS_CB_ITEM_VALUE_LESS_OR_GREATER))
                    cmb.Items.Add(LocalResource.GTT_FS_CB_ITEM_VALUE_LESS_OR_GREATER);
            }
            catch { }
        }
        #region Handling Listbox Mouse events

        #endregion
        private void Choices_grid_CurrentCellChanged(object sender, EventArgs e)
        {
            var dg = (System.Windows.Controls.DataGrid)sender;
            ExpGrid = dg;
            dg.Focus();
        }
        private void DisableRightMenu(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void b1SetColor(object sender, MouseButtonEventArgs e)
        {
            if (ExpGrid != null)
                ExpGrid.Focus();
        }

        private void Grid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                this.gridcriteriavalues.Focus();
            }
            QC4Common.Util.GridCommonFunc.Arrow(sender, e);
        }
        private void HandleCopyPaste(object sender, KeyEventArgs e)
        {
            var uiElement = e.OriginalSource as UIElement;
            try
            {
                Util.QS.GridCopyPaste copyPaste = new Util.QS.GridCopyPaste();
                var data = copyPaste.PastetoDatagrid(sender);
                int datagridColumn = gridcriteriavalues.CurrentCell.Column.DisplayIndex;
                DataGridCell cell = frmutil.GetCell(gridcriteriavalues, gridcriteriavalues.SelectedIndex, datagridColumn);
                if (!cell.IsEditing)
                {
                    e.Handled = true;
                    int No_Row = copyPaste.No_Row;
                    int No_Column = copyPaste.No_Columns;
                    if (data != null)
                    {
                        e.Handled = true;
                        int datagridRow = gridcriteriavalues.SelectedIndex;
                        if (supportanswertype == QC4Common.Common.Constants.AnswerType.SA || supportanswertype == QC4Common.Common.Constants.AnswerType.MA)//2,3,4
                        {
                            if (gridcriteriavalues.CurrentCell.Column.DisplayIndex > 1 && gridcriteriavalues.CurrentCell.Column.DisplayIndex <= (QC4Common.Common.Constants.Integrate_SupportVaariable_SAMA_Last_Column))
                            {
                                //selection in choice
                                if (No_Column > QC4Common.Common.Constants.Integrate_SupportVaariable_SAMA_Last_Column || No_Column > (QC4Common.Common.Constants.Integrate_SupportVaariable_SAMA_Last_Column - gridcriteriavalues.CurrentCell.Column.DisplayIndex) + 1 || No_Row > gridcriteriavalues.Items.Count - (gridcriteriavalues.SelectedIndex) + 1)//Integrate_SupportVaariable_SAMA_Column_Count 
                                {
                                    MessageDialog.ErrorOk(LocalResource.COPY_ERROR_MSG);
                                    e.Handled = true;
                                }
                                else
                                {
                                    int RowIndex = gridcriteriavalues.SelectedIndex;
                                    for (int i = 0; i < No_Row; i++, RowIndex++)
                                    {
                                        // int col = gridcriteriavalues.CurrentCell.Column.DisplayIndex;
                                        for (int j = 1, col = gridcriteriavalues.CurrentCell.Column.DisplayIndex; j <= No_Column && col <= QC4Common.Common.Constants.Integrate_SupportVaariable_SAMA_Last_Column; j++, col++)
                                        {
                                            if (col == 3)
                                            {
                                                if (string.Equals(Convert.ToString(data[i, (j - 1)]), LocalResource.GTT_FS_CB_ITEM_VALUE_EQUALS) || string.Equals(Convert.ToString(data[i, (j - 1)]), LocalResource.GTT_FS_CB_ITEM_VALUE_LESS_OR_GREATER))
                                                {
                                                    gridchoicevalues.Rows[RowIndex][col] = Convert.ToString(data[i, (j - 1)]);
                                                }
                                                else
                                                {
                                                    gridchoicevalues.Rows[RowIndex][col] = null;
                                                }
                                            }
                                            else if (col == 2 || col == 4)
                                            {
                                                gridchoicevalues.Rows[RowIndex][col] = Convert.ToString(data[i, (j - 1)]);
                                            }


                                        }

                                    }

                                }
                            }
                        }
                        else if (supportanswertype == QC4Common.Common.Constants.AnswerType.N)//2,5,6,7
                        {
                            if (gridcriteriavalues.CurrentCell.Column.DisplayIndex > 1 && gridcriteriavalues.CurrentCell.Column.DisplayIndex <= (QC4Common.Common.Constants.Integrate_SupportVaariable_N_Last_Column))
                            {
                                //selection in choice
                                if (No_Column > QC4Common.Common.Constants.Integrate_SupportVaariable_N_Last_Column ||
                                   (gridcriteriavalues.CurrentCell.Column.DisplayIndex == 2 && No_Column > (QC4Common.Common.Constants.Integrate_SupportVaariable_N_Last_Column - gridcriteriavalues.CurrentCell.Column.DisplayIndex) - 1) ||
                                   (gridcriteriavalues.CurrentCell.Column.DisplayIndex > 2 && No_Column > (QC4Common.Common.Constants.Integrate_SupportVaariable_N_Last_Column - gridcriteriavalues.CurrentCell.Column.DisplayIndex) + 1) ||
                                    No_Row > gridcriteriavalues.Items.Count - (gridcriteriavalues.SelectedIndex) + 1)//Integrate_SupportVaariable_SAMA_Column_Count 
                                {
                                    MessageDialog.ErrorOk(LocalResource.COPY_ERROR_MSG);
                                    e.Handled = true;
                                }
                                else
                                {
                                    int RowIndex = gridcriteriavalues.SelectedIndex;
                                    for (int i = 0; i < No_Row; i++, RowIndex++)
                                    {
                                        // int col = gridcriteriavalues.CurrentCell.Column.DisplayIndex;
                                        for (int j = 1, col = gridcriteriavalues.CurrentCell.Column.DisplayIndex; j <= No_Column && col <= QC4Common.Common.Constants.Integrate_SupportVaariable_N_Last_Column; j++, col++)
                                        {
                                            if (gridcriteriavalues.CurrentCell.Column.DisplayIndex == 2 && col == 3)
                                            {
                                                col = 5;
                                            }
                                            else if (col == 6)
                                            {
                                                col = 7;
                                            }

                                            if (col == 2 || col == 5 || col == 7)
                                            {
                                                gridchoicevalues.Rows[RowIndex][col] = Convert.ToString(data[i, (j - 1)]);
                                            }
                                        }

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

        private void gridcriteriavalues_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Delete)
            {
                try
                {
                    int RowIndex = 0;
                    if ((gridcriteriavalues.SelectedItems != null) && (gridcriteriavalues.SelectedItems.Count > 0))
                    {
                        for (int i = 0; i < gridcriteriavalues.SelectedItems.Count; i++)
                        {
                            var presentRow = (System.Data.DataRowView)gridcriteriavalues.SelectedItems[i];
                            RowIndex = Convert.ToInt32(presentRow.Row.ItemArray[1].ToString());
                            if (supportanswertype == QC4Common.Common.Constants.AnswerType.SA || supportanswertype == QC4Common.Common.Constants.AnswerType.MA)//2,3,4
                            {
                                gridchoicevalues.Rows[RowIndex - 1][2] = string.Empty;
                                gridchoicevalues.Rows[RowIndex - 1][3] = null;
                                gridchoicevalues.Rows[RowIndex - 1][4] = string.Empty;
                            }
                            else if (supportanswertype == QC4Common.Common.Constants.AnswerType.N)//2,5,6,7
                            {
                                gridchoicevalues.Rows[RowIndex - 1][2] = string.Empty;
                                gridchoicevalues.Rows[RowIndex - 1][5] = string.Empty;
                                gridchoicevalues.Rows[RowIndex - 1][7] = string.Empty;
                            }

                        }
                    }
                }
                catch { }
            }
            if (e.Key == Key.Tab) { gridcriteriavalues.ScrollIntoView(gridcriteriavalues.Items[0]); }

            bool _ShiftModifierPressed = (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift));
            if (_ShiftModifierPressed && (e.Key == Key.Enter || e.Key == Key.Tab))
            {
                if (gridcriteriavalues.CurrentCell.Column != null)
                {

                    if (supportanswertype == QC4Common.Common.Constants.AnswerType.SA || supportanswertype == QC4Common.Common.Constants.AnswerType.MA)//2,3,4
                    {
                        int datagridRow = gridcriteriavalues.SelectedIndex == -1 ? 0 : gridcriteriavalues.SelectedIndex;
                        int datagridColumn = gridcriteriavalues.CurrentCell.Column.DisplayIndex;

                        int nextcolumn = datagridColumn;
                        if (datagridColumn == 0)
                        {
                            datagridRow = datagridRow - 1;
                            nextcolumn = 4;
                            if (datagridRow <= -1)
                            {
                                List_OriginItem_Item.Focus();
                                e.Handled = true;
                                return;
                            }
                        }
                        else if (datagridColumn == 2)
                        {
                            nextcolumn = 0;
                        }
                        if (datagridColumn == 3)
                        {
                            nextcolumn = datagridColumn - 1;
                        }
                        if (datagridColumn == 4)
                        {
                            nextcolumn = datagridColumn - 1;
                        }
                        DataGridCell cell = frmutil.GetCell(gridcriteriavalues, datagridRow, nextcolumn);
                        if (cell != null)
                        {
                            DataGridRow row = frmutil.GetRow(gridcriteriavalues, datagridRow, nextcolumn);
                            if (row != null)
                            {
                                row.IsSelected = true;
                            }
                            cell.Focus();
                            if (nextcolumn != 0)
                            {
                                gridcriteriavalues.BeginEdit();
                            }
                            gridcriteriavalues.SelectedIndex = datagridRow;
                            if (nextcolumn == 0)
                            {
                                var contentpresenter = cell.Content as ContentPresenter;
                                if (contentpresenter != null)
                                {
                                    contentpresenter.ApplyTemplate();
                                    var content = contentpresenter.ContentTemplate.FindName("gridselectbutton", contentpresenter);
                                    var button = content as Button;
                                    if (button != null)
                                    {
                                        button.Template.LoadContent();
                                        // comboBox.SelectedIndex = 0;
                                        button.Focus();
                                        //  SetArrowButtonValue();
                                    }
                                }
                            }
                            else if (nextcolumn == 3)
                            {
                                var contentpresenter = cell.Content as ContentPresenter;
                                if (contentpresenter != null)
                                {
                                    contentpresenter.ApplyTemplate();
                                    var content = contentpresenter.ContentTemplate.FindName("cmb_dataGrid_operator", contentpresenter);
                                    var comboBox = content as ComboBox;
                                    if (comboBox != null)
                                    {
                                        comboBox.Template.LoadContent();
                                        // comboBox.SelectedIndex = 0;
                                        comboBox.Focus();
                                        // comboBox.IsDropDownOpen = true;
                                    }
                                }
                            }
                        }
                    }
                    else if (supportanswertype == QC4Common.Common.Constants.AnswerType.N)//2,5,6,7
                    {
                        int datagridRow = gridcriteriavalues.SelectedIndex == -1 ? 0 : gridcriteriavalues.SelectedIndex;
                        int datagridColumn = gridcriteriavalues.CurrentCell.Column.DisplayIndex;
                        int nextcolumn = datagridColumn;
                        if (datagridColumn == 2)
                        {
                            nextcolumn = 7;
                            datagridRow = datagridRow - 1;
                            if (datagridRow <= -1)
                            {
                                List_OriginItem_Item.Focus();
                                e.Handled = true;
                                return;
                            }
                        }
                        else if (datagridColumn == 5)
                        {
                            nextcolumn = 2;
                        }
                        else if (datagridColumn == 7)
                        {
                            nextcolumn = 5;
                        }
                        DataGridCell cell = frmutil.GetCell(gridcriteriavalues, datagridRow, nextcolumn);
                        if (cell != null)
                        {
                            // cell.IsSelected = true;
                            DataGridRow row = frmutil.GetRow(gridcriteriavalues, datagridRow, nextcolumn);
                            if (row != null)
                            {
                                row.IsSelected = true;
                            }
                            cell.Focus();
                            gridcriteriavalues.BeginEdit();
                            gridcriteriavalues.SelectedIndex = datagridRow;
                        }
                    }
                    e.Handled = true;
                }
                else
                { gridcriteriavalues.SelectedIndex = 0; }//may b need to change row count

            }
            else if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (gridcriteriavalues.CurrentCell.Column != null)
                {

                    if (supportanswertype == QC4Common.Common.Constants.AnswerType.SA || supportanswertype == QC4Common.Common.Constants.AnswerType.MA)//2,3,4
                    {
                        int datagridRow = gridcriteriavalues.SelectedIndex == -1 ? 0 : gridcriteriavalues.SelectedIndex;
                        int datagridColumn = gridcriteriavalues.CurrentCell.Column.DisplayIndex;

                        int nextcolumn = datagridColumn;
                        if (datagridColumn == 0)
                        {
                            nextcolumn = 2;
                        }
                        else if (datagridColumn == 2)
                        {
                            nextcolumn = datagridColumn + 1;
                        }
                        if (datagridColumn == 3)
                        {
                            nextcolumn = datagridColumn + 1;
                        }
                        if (datagridColumn == 4)
                        {
                            datagridRow = datagridRow + 1;
                            nextcolumn = 0;
                            if (datagridRow == (gridcriteriavalues.Items.Count))
                            {
                                Command_Entry.Focus();
                                e.Handled = true;
                                return;
                            }
                        }
                        DataGridCell cell = frmutil.GetCell(gridcriteriavalues, datagridRow, nextcolumn);
                        if (cell != null)
                        {
                            DataGridRow row = frmutil.GetRow(gridcriteriavalues, datagridRow, nextcolumn);
                            if (row != null)
                            {
                                row.IsSelected = true;
                            }
                            cell.Focus();
                            if (nextcolumn != 0)
                            {
                                gridcriteriavalues.BeginEdit();
                            }
                            gridcriteriavalues.SelectedIndex = datagridRow;
                            if (nextcolumn == 0)
                            {
                                var contentpresenter = cell.Content as ContentPresenter;
                                if (contentpresenter != null)
                                {
                                    contentpresenter.ApplyTemplate();
                                    var content = contentpresenter.ContentTemplate.FindName("gridselectbutton", contentpresenter);
                                    var button = content as Button;
                                    if (button != null)
                                    {
                                        button.Template.LoadContent();
                                        // comboBox.SelectedIndex = 0;
                                        button.Focus();
                                        //  SetArrowButtonValue();
                                    }
                                }
                            }
                            else if (nextcolumn == 3)
                            {
                                var contentpresenter = cell.Content as ContentPresenter;
                                if (contentpresenter != null)
                                {
                                    contentpresenter.ApplyTemplate();
                                    var content = contentpresenter.ContentTemplate.FindName("cmb_dataGrid_operator", contentpresenter);
                                    var comboBox = content as ComboBox;
                                    if (comboBox != null)
                                    {
                                        comboBox.Template.LoadContent();
                                        // comboBox.SelectedIndex = 0;
                                        comboBox.Focus();
                                        // comboBox.IsDropDownOpen = true;
                                    }
                                }
                            }
                        }
                    }
                    else if (supportanswertype == QC4Common.Common.Constants.AnswerType.N)//2,5,6,7
                    {
                        int datagridRow = gridcriteriavalues.SelectedIndex == -1 ? 0 : gridcriteriavalues.SelectedIndex;
                        int datagridColumn = gridcriteriavalues.CurrentCell.Column.DisplayIndex;
                        int nextcolumn = datagridColumn;
                        if (datagridColumn == 2)
                        {
                            nextcolumn = 5;
                        }
                        else if (datagridColumn == 5)
                        {
                            nextcolumn = 7;
                        }
                        else if (datagridColumn == 7)
                        {
                            datagridRow = datagridRow + 1;
                            nextcolumn = 2;
                            if (datagridRow == (gridcriteriavalues.Items.Count))
                            {
                                Command_Entry.Focus();
                                e.Handled = true;
                                return;
                            }
                        }
                        DataGridCell cell = frmutil.GetCell(gridcriteriavalues, datagridRow, nextcolumn);
                        if (cell != null)
                        {
                            // cell.IsSelected = true;
                            DataGridRow row = frmutil.GetRow(gridcriteriavalues, datagridRow, nextcolumn);
                            if (row != null)
                            {
                                row.IsSelected = true;
                            }
                            cell.Focus();
                            gridcriteriavalues.BeginEdit();
                            gridcriteriavalues.SelectedIndex = datagridRow;
                        }
                    }
                    e.Handled = true;
                }
                else
                { gridcriteriavalues.SelectedIndex = 0; }

            }
            else if (e.Key == Key.Space && (supportanswertype == QC4Common.Common.Constants.AnswerType.SA || supportanswertype == QC4Common.Common.Constants.AnswerType.MA) && gridcriteriavalues.CurrentCell.Column != null && gridcriteriavalues.CurrentCell.Column.DisplayIndex == 0)
            {

                int datagridRow = gridcriteriavalues.SelectedIndex == -1 ? 0 : gridcriteriavalues.SelectedIndex;
                int datagridColumn = gridcriteriavalues.CurrentCell.Column.DisplayIndex;
                gridcriteriavalues.SelectedIndex = datagridRow;
                SetArrowButtonValue();

            }
            //TOD  enter function here
            bool _altModifierPressed = (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl));
            if (_altModifierPressed && Keyboard.IsKeyDown(Key.V))
            {

                if (gridcriteriavalues != null && gridcriteriavalues.CurrentCell != null && gridcriteriavalues.CurrentCell.Column != null)
                {
                    if (gridcriteriavalues.CurrentCell.Column.DisplayIndex == 2 || gridcriteriavalues.CurrentCell.Column.DisplayIndex == 3 || gridcriteriavalues.CurrentCell.Column.DisplayIndex == 4 ||
                        gridcriteriavalues.CurrentCell.Column.DisplayIndex == 5 || gridcriteriavalues.CurrentCell.Column.DisplayIndex == 7
                        )
                    {
                        HandleCopyPaste(sender, e);
                    }
                }
            }

        }

        private void Gridcriteriavalues_CopyingRowClipboardContent(object sender, DataGridRowClipboardEventArgs e)
        {
            if (supportanswertype == QC4Common.Common.Constants.AnswerType.SA || supportanswertype == QC4Common.Common.Constants.AnswerType.MA)//2,3,4
            {
                try
                {
                    e.ClipboardRowContent.RemoveAt(0);
                }
                catch { }
                try
                {
                    e.ClipboardRowContent.RemoveAt(0);
                }
                catch { }
            }
            else if (supportanswertype == QC4Common.Common.Constants.AnswerType.N)//2,5,6,7
            {
                try
                {
                    e.ClipboardRowContent.RemoveAt(0);
                }
                catch { }
                try
                {
                    e.ClipboardRowContent.RemoveAt(2);
                }
                catch { }
            }

        }

        private void Btn_RightArrow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                arrowbuttonrowindex = gridcriteriavalues.SelectedIndex;
            }
        }
        private void SetArrowButtonValue()
        {
            int currentrow = -1;
            try
            {
                currentrow = gridcriteriavalues.SelectedIndex;
                if (currentrow > -1)
                {
                    int[] choiceselectedlist = new int[List_OriginItem_Item.SelectedItems.Count];
                    // int pos = 0;
                    for (int i = 0; i < List_OriginItem_Item.SelectedItems.Count; i++)
                    {
                        var presentRow = (System.Data.DataRowView)List_OriginItem_Item.SelectedItems[i];

                        choiceselectedlist[i] = Convert.ToInt32(presentRow.Row.ItemArray[0].ToString());

                    }
                    //foreach (string item in List_OriginItem_Item.SelectedItems)
                    //{

                    //    choiceselectedlist[pos] = Convert.ToInt32((item.Split(':'))[0]);
                    //    pos++;

                    //}
                    string choices = frmutil.GetCountMean(choiceselectedlist, choiceselectedlist.Length);
                    if (!string.IsNullOrEmpty(choices))
                    {
                        gridchoicevalues.Rows[currentrow]["Operator"] = LocalResource.GTT_FS_CB_ITEM_VALUE_EQUALS;
                        gridchoicevalues.Rows[currentrow]["CriteriaValue"] = choices;// frmutil.GetCountMean(choiceselectedlist, choiceselectedlist.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

        }

        private void Gridcriteriavalues_IsMouseCaptureWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //if (supportanswertype == QC4Common.Common.Constants.AnswerType.SA || supportanswertype == QC4Common.Common.Constants.AnswerType.MA)//2,3,4
            //{

            //}
            //else if (supportanswertype == QC4Common.Common.Constants.AnswerType.N)//2,5,6,7
            //{

            //}

            if (gridcriteriavalues != null && gridcriteriavalues.CurrentCell != null && gridcriteriavalues.CurrentCell.Column != null
                && (gridcriteriavalues.CurrentCell.Column.DisplayIndex == 2 || gridcriteriavalues.CurrentCell.Column.DisplayIndex == 3 || gridcriteriavalues.CurrentCell.Column.DisplayIndex == 4 || gridcriteriavalues.CurrentCell.Column.DisplayIndex == 5 || gridcriteriavalues.CurrentCell.Column.DisplayIndex == 7))//Display index for SAMA/N need or not???
            {
                frmutil.SetErrorForGrid(gridcriteriavalues, gridcriteriavalues.SelectedIndex, gridcriteriavalues.CurrentCell.Column.DisplayIndex, QC4Common.Common.Constants.STD_DP.Background, true);
                frmutil.SetErrorForGrid(gridcriteriavalues, gridcriteriavalues.SelectedIndex, gridcriteriavalues.CurrentCell.Column.DisplayIndex, QC4Common.Common.Constants.STD_DP.Foreground, true);
            }
        }

        private void List_OriginItem_Item_CopyingRowClipboardContent(object sender, DataGridRowClipboardEventArgs e)
        {
            try
            {
                //Removes < column from copying
                e.ClipboardRowContent.RemoveAt(0);
            }
            catch (Exception ex) { _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException); }
        }

        private void Gridcriteriavalues_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {


        }

        private void Gridcriteriavalues_PreviewKeyUp(object sender, KeyEventArgs e)
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
                int datagridColumn = gridcriteriavalues.CurrentCell.Column.DisplayIndex;
                DataGridCell cell = frmutil.GetCell(gridcriteriavalues, gridcriteriavalues.SelectedIndex, datagridColumn);
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
    }
}
