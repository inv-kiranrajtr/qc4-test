using log4net;
using Qc4Launcher.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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

namespace Qc4Launcher.Forms.QuestionSetting
{
    /// <summary>
    /// Interaction logic for SubtotalSetting.xaml
    /// </summary>
    public partial class SubtotalSetting : Window
    {
        public DataTable dt = null;
        public DataTable subdt = null;
        System.Windows.Controls.ComboBox cmb_operator;
        int pevoiusselectedindex = 0;
        private static Microsoft.Office.Interop.Excel.Workbook excelWorkBook = null;
        public bool combofirsttime = true;
        int selectedindex = 0;
        int insertrow = -1;
        int deleterow = -1;
        int childCount = 0;
        bool IsParant;
        System.Windows.Controls.DataGrid ExpGrid = null;
        QC4Common.Util.FormUtil frmutil = new QC4Common.Util.FormUtil();
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        QC4Common.Util.FormUtil formUtil = new QC4Common.Util.FormUtil();
        public SubtotalSetting(int selectedindex, DataTable subdt, Microsoft.Office.Interop.Excel.Workbook workBook, int childCount = 0)
        {
            InitializeComponent();
            this.subdt = subdt;
            this.selectedindex = selectedindex;
            excelWorkBook = workBook;
            this.childCount = childCount;

        }


        private DataTable CreateTable()
        {
            DataTable griddata = new DataTable();
            griddata.Columns.Add("No");
            griddata.Columns.Add("Choice");
            griddata.Columns.Add("Operator");
            griddata.Columns.Add("Value");
            return griddata;
        }
        private DataTable FillDataGrid(int limit)
        {
            DataTable griddata = CreateTable();
            DataRow dr;
            for (int i = 1; i <= 10; i++)
            {
                dr = griddata.NewRow();
                dr["No"] = i;
                dr["Choice"] = String.Empty;
                dr["Operator"] = String.Empty;
                dr["Value"] = String.Empty;
                griddata.Rows.Add(dr);
            }
            return griddata;
        }



        private void Window_Closed(object sender, EventArgs e)
        {

        }

        private void Windowloaded(object sender, RoutedEventArgs e)
        {
            Fill_Variable_Name();
            BindDt();
            PreparesubtotaltoDt();
            AddChoiceToList();
            Loaditems_Top_Bottom_Combo_box();

        }
        private void FillsubDt()
        {

        }
        private void PreparesubtotaltoDt()
        {
            QC4Common.Util.FormUtil formUtil = new QC4Common.Util.FormUtil();
            try
            {
                string value;
                for (int i = 0; i < subdt.Rows.Count; i++)
                {
                    dt.Rows[i][0] = i + 1;
                    if ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).SubTotals.Count < 1)
                    {
                        dt.Rows[i][1] = checksinglequete(formUtil.EscapeCRLF(subdt.Rows[i][1].ToString()));
                    }
                    else
                    {
                        dt.Rows[i][1] = checksinglequete(formUtil.EscapeCRLF((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).SubTotals.ElementAt(i).Subtotal));
                    }
                    if (GetOperator(Convert.ToString(subdt.Rows[i][0])) == "")
                    {
                        value = "=";
                    }
                    else
                    {
                        value = GetOperator(Convert.ToString(subdt.Rows[i][0]));
                    }
                    dt.Rows[i][2] = value;
                    dt.Rows[i][3] = formUtil.TrimStartEqualNotequal(Convert.ToString(subdt.Rows[i][0]));
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                 _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

        }
        private string GetOperator(string value)
        {
            try
            {
                if (value.StartsWith("<>") || value.StartsWith("!"))
                {
                    return "<>";
                }
                else if (value.StartsWith("="))
                {
                    return "=";
                }
                return "";
            }
            catch { return ""; }
        }
        private void Loaditems_Top_Bottom_Combo_box()
        {
            try
            {
                for (int i = 0; i < (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).Choices.Count; i++)
                {
                    Combo_Top_Num.Items.Add(i + 1);
                    Combo_Bottom_Num.Items.Add(i + 1);
                }
                if ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).Choices.Count == 1)
                {
                    Combo_Top_Num.SelectedIndex = 0;
                    Combo_Bottom_Num.SelectedIndex = 0;
                }
                else
                {
                    Combo_Top_Num.SelectedIndex = 1;
                    Combo_Bottom_Num.SelectedIndex = 1;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                 _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }
        private void Fill_Variable_Name()
        {
            try
            {
                Text__OriginItem_Item.Text = (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).Variable;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                 _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }
        private void AddChoiceToList()
        {
            QC4Common.Util.FormUtil formUtil = new QC4Common.Util.FormUtil();
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("Index");
                table.Columns.Add("Choices");
                DataRow dr;
                for (int i = 0; i < (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).Choices.Count; i++)
                {
                    dr = table.NewRow();
                    dr[0] = (i + 1);
                    dr[1] = formUtil.EscapeCRLF((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).Choices.ElementAt(i).ToString());
                    table.Rows.Add(dr);
                }
                ChoiceList.ItemsSource = table.AsDataView();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                 _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

        }
        private void BindDt()
        {
            try
            {
                dt = FillDataGrid(10);
                sub_datagrid.DataContext = dt;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                 _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void comboload(object sender, RoutedEventArgs e)
        {
            try
            {
                cmb_operator = (System.Windows.Controls.ComboBox)sender;
                cmb_operator.Items.Add("=");
                cmb_operator.Items.Add("<>");
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                 _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void btn_grid1(object sender, RoutedEventArgs e)
        {
            SavetoGrid(0);
        }

        private void btn_grid2(object sender, RoutedEventArgs e)
        {
            SavetoGrid(1);
        }

        private void btn_grid3(object sender, RoutedEventArgs e)
        {
            SavetoGrid(2);
        }

        private void btn_grid4(object sender, RoutedEventArgs e)
        {
            SavetoGrid(3);
        }

        private void btn_grid5(object sender, RoutedEventArgs e)
        {
            SavetoGrid(4);
        }

        private void btn_grid6(object sender, RoutedEventArgs e)
        {
            SavetoGrid(5);
        }

        private void btn_grid7(object sender, RoutedEventArgs e)
        {
            SavetoGrid(6);
        }

        private void btn_grid8(object sender, RoutedEventArgs e)
        {
            SavetoGrid(7);
        }

        private void btn_grid9(object sender, RoutedEventArgs e)
        {
            SavetoGrid(8);
        }

        private void btn_grid10(object sender, RoutedEventArgs e)
        {
            SavetoGrid(9);
        }
        private void SavetoGrid(int index)
        {
            try
            {

                if ((ChoiceList.SelectedItems != null) && (ChoiceList.SelectedItems.Count > 0))
                {
                    int[] a = new int[ChoiceList.SelectedItems.Count];
                    string choice = string.Empty;
                    for (int i = 0; i < ChoiceList.SelectedItems.Count; i++)
                    {
                        var presentRow = (System.Data.DataRowView)ChoiceList.SelectedItems[i];

                        a[i] = Convert.ToInt32(presentRow.Row.ItemArray[0].ToString());
                        if (i == 0)
                        {
                            choice = presentRow.Row.ItemArray[1].ToString();

                        }
                        else
                        {
                            choice = choice + " " + presentRow.Row.ItemArray[1].ToString();

                        }
                    }
                    dt.Rows[index][1] = choice;
                    dt.Rows[index][2] = "=";
                    dt.Rows[index][3] = formUtil.GetCountMean(a, ChoiceList.SelectedItems.Count);
                }
                else
                {
                    dt.Rows[index][2] = "=";
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                 _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }
        public string checksinglequete(string value)
        {
            if (value.StartsWith("'"))
            {
                value = "'" + value;
            }
            return value;
        }
      

        private void KeymockCtrl(object sender, KeyEventArgs e)
        {
            try
            {
                if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                {
                    pevoiusselectedindex = ChoiceList.SelectedIndex;
                }
                QC4Common.Util.GridCommonFunc.Arrow(sender, e);

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                 _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void Top_bottom_Item_check(object sender, KeyEventArgs e)
        {
            try
            {
                System.Windows.Controls.ComboBox combo = (System.Windows.Controls.ComboBox)sender;
                int result = 0;
                if (int.TryParse(combo.Text, out result))
                {
                    int max = (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).Choices.Count;
                    if (!(result <= max && result >= 1))
                    {
                        combo.SelectedIndex = 0;
                    }
                }
                else
                {
                    combo.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                 _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void Save_subtotal_sheet(object sender, RoutedEventArgs e)
        {
            QC4Common.Util.FormUtil formUtil = new QC4Common.Util.FormUtil();
            try
            {
                if (ValidateSub_total())
                {
                    subdt.Rows.Clear();
                    int lastoccurancechoice = formUtil.GetLastRow(dt, 1);
                    int lastoccuranceOperator = formUtil.GetLastRow(dt, 2);
                    int lastoccuranceValue = formUtil.GetLastRow(dt, 1);
                    int max = LargetOccurance(lastoccurancechoice, lastoccuranceOperator, lastoccuranceValue);
                    DataRow dr;
                    for (int i = 0; i < max; i++)
                    {
                        dr = subdt.NewRow();
                        if (dt.Rows[i][2].ToString() == "=")
                        {
                            dr[0] = checksinglequete(dt.Rows[i][3].ToString());
                        }
                        else
                        {
                            dr[0] = "!" + checksinglequete(dt.Rows[i][3].ToString());
                        }
                        dr[1] = formUtil.UnEscapeCRLF(dt.Rows[i][1].ToString());
                        subdt.Rows.Add(dr);
                    }

                    Util.QS.AddNewQuestion addNew = new Util.QS.AddNewQuestion(excelWorkBook, "");
                    addNew.Savesubtotal_edit(selectedindex, subdt, childCount);
                    this.Close();
                }
                else
                {
                  
                }
                Util.Definiotion.VariableDictionary = QC4Common.Util.DictionaryUtil.PopulateQSDictionary(excelWorkBook);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                 _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }
        private int LargetOccurance(int a, int b, int c)
        {
            if (a < b)
            {
                if (b < c)
                {
                    return c;
                }
                else
                {
                    return b;
                }
            }
            else
            {
                if (a < c)
                {
                    return c;
                }
                else
                {
                    return a;
                }
            }
        }
        private bool ValidateSub_total()
        {
            try
            {
                int lastoccurancechoice = formUtil.GetLastRow(dt, 1);
                int lastoccuranceOperator = formUtil.GetLastRow(dt, 2);
                int lastoccuranceValue = formUtil.GetLastRow(dt, 1);


                int max = LargetOccurance(lastoccurancechoice, lastoccuranceOperator, lastoccuranceValue);
                int i;
                for (i = 0; i < max; i++)
                {

                    if (!(dt.Rows[i][1].ToString().TrimStart().Length > 0 && dt.Rows[i][2].ToString().TrimStart().Length > 0 && dt.Rows[i][3].ToString().TrimStart().Length > 0))
                    {
                        Qc4Launcher.Util.MessageDialog.ErrorOk(string.Format(LocalResource.SUBTOTAL_ALERT_MISSING_COLUMN, (i + 1).ToString()));
                        return false;
                    }
                    if (!QC4Common.Validation.NumberCheck.NUmberCheckSubtotal(dt.Rows[i][3].ToString(), (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).Choices.Count))
                    {
                        Qc4Launcher.Util.MessageDialog.ErrorOk(string.Format(LocalResource.SET_INTEGER_BETWEEN, 1, (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).Choices.Count));
                        return false;

                    }
                }
                if (i >= max)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex) { return false; }
           
        }

        private void Content_Up_data(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sub_datagrid.SelectedIndex != -1)
                {
                    if (sub_datagrid.SelectedIndex != 0)
                    {
                        string tempchoice = string.Empty;
                        string tempOperator = null;
                        string tempvalue = string.Empty;
                        int index = sub_datagrid.SelectedIndex - 1;
                        tempchoice = dt.Rows[index][1].ToString();
                        tempOperator = dt.Rows[index][2].ToString();
                        tempvalue = dt.Rows[index][3].ToString();
                        dt.Rows[index][1] = dt.Rows[sub_datagrid.SelectedIndex][1];
                        dt.Rows[index][2] = dt.Rows[sub_datagrid.SelectedIndex][2];
                        dt.Rows[index][3] = dt.Rows[sub_datagrid.SelectedIndex][3];
                        dt.Rows[sub_datagrid.SelectedIndex][1] = tempchoice;
                        if (tempOperator == null || tempOperator == string.Empty)
                        {
                            dt.Rows[sub_datagrid.SelectedIndex][2] = null;
                        }
                        else
                        {
                            dt.Rows[sub_datagrid.SelectedIndex][2] = tempOperator;
                        }
                        dt.Rows[sub_datagrid.SelectedIndex][3] = tempvalue;
                        sub_datagrid.SelectedIndex = index;

                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                 _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void Content_down_data(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sub_datagrid.SelectedIndex != -1)
                {
                    if (sub_datagrid.SelectedIndex != 9)
                    {
                        string tempchoice = string.Empty;
                        string tempOperator = null;
                        string tempvalue = string.Empty;
                        int index = sub_datagrid.SelectedIndex + 1;
                        tempchoice = dt.Rows[index][1].ToString();
                        if (dt.Rows[index][2].ToString() == string.Empty || dt.Rows[index][2] == null)
                        {
                            tempOperator = null;
                        }
                        else
                        {
                            tempOperator = dt.Rows[index][2].ToString();
                        }
                        tempvalue = dt.Rows[index][3].ToString();
                        dt.Rows[index][1] = dt.Rows[sub_datagrid.SelectedIndex][1];
                        dt.Rows[index][2] = dt.Rows[sub_datagrid.SelectedIndex][2];
                        dt.Rows[index][3] = dt.Rows[sub_datagrid.SelectedIndex][3];
                        dt.Rows[sub_datagrid.SelectedIndex][1] = tempchoice;
                        dt.Rows[sub_datagrid.SelectedIndex][2] = tempOperator;
                        dt.Rows[sub_datagrid.SelectedIndex][3] = tempvalue;
                        sub_datagrid.SelectedIndex = index;
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                 _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void InsertRow_toDataGrid(object sender, RoutedEventArgs e)
        {
            try
            {
                int lastoccurancechoice = formUtil.GetLastRow(dt, 1);
                int lastoccuranceOperator = formUtil.GetLastRow(dt, 2);
                int lastoccuranceValue = formUtil.GetLastRow(dt, 1);
                int max = LargetOccurance(lastoccurancechoice, lastoccuranceOperator, lastoccuranceValue);
                max = max - 1;
                if (sub_datagrid.SelectedIndex != -1)
                {
                    if (max < 9)
                    {
                        if (sub_datagrid.SelectedIndex <= max)
                        {
                           

                            for (int i = max; i >= sub_datagrid.SelectedIndex; i--)
                            {
                                dt.Rows[i + 1][1] = dt.Rows[i][1].ToString();
                                if (dt.Rows[i][2].ToString() == "" || dt.Rows[i][2] == null)
                                {
                                    dt.Rows[i + 1][2] = null;
                                }
                                else
                                {
                                    dt.Rows[i + 1][2] = dt.Rows[i][2].ToString();
                                }

                                dt.Rows[i + 1][3] = dt.Rows[i][3].ToString();
                            }
                            dt.Rows[sub_datagrid.SelectedIndex][1] = string.Empty;
                            dt.Rows[sub_datagrid.SelectedIndex][2] = null;
                            dt.Rows[sub_datagrid.SelectedIndex][3] = string.Empty;
                            sub_datagrid.SelectedIndex = sub_datagrid.SelectedIndex;


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                 _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void DeleteRow_toDataGrid(object sender, RoutedEventArgs e)
        {
            try
            {
                int lastoccurancechoice = formUtil.GetLastRow(dt, 1);
                int lastoccuranceOperator = formUtil.GetLastRow(dt, 2);
                int lastoccuranceValue = formUtil.GetLastRow(dt, 1);
                int max = LargetOccurance(lastoccurancechoice, lastoccuranceOperator, lastoccuranceValue);
                max = max - 1;
                if (sub_datagrid.SelectedIndex != -1)
                {
                    if (max > -1)
                    {
                        if (sub_datagrid.SelectedIndex <= max)
                        {
                         

                            for (int i = sub_datagrid.SelectedIndex; i < max; i++)
                            {
                                dt.Rows[i][1] = dt.Rows[i + 1][1].ToString();
                                if (dt.Rows[i + 1][2].ToString() == "" || dt.Rows[i + 1][2] == null)
                                {
                                    dt.Rows[i][2] = null;
                                }
                                else
                                {
                                    dt.Rows[i][2] = dt.Rows[i + 1][2].ToString();
                                }
                                dt.Rows[i][3] = dt.Rows[i + 1][3].ToString();
                            }
                            dt.Rows[max][1] = string.Empty;
                            dt.Rows[max][2] = null;
                            dt.Rows[max][3] = string.Empty;
                            sub_datagrid.SelectedIndex = sub_datagrid.SelectedIndex;

                        }

                    }

                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                 _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void changeComboselction(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void btn_Top_add(object sender, RoutedEventArgs e)
        {
            try
            {
                int lastoccurancechoice = formUtil.GetLastRow(dt, 1);
                int lastoccuranceOperator = formUtil.GetLastRow(dt, 2);
                int lastoccuranceValue = formUtil.GetLastRow(dt, 1);
                int max = LargetOccurance(lastoccurancechoice, lastoccuranceOperator, lastoccuranceValue);
                if (max == -1)
                {
                    max = 0;
                }
                string topIndex = (Combo_Top_Num.SelectedIndex + 1).ToString();
                if (max != 10)
                {
                    dt.Rows[max][1] = "Top" + " " + topIndex;
                    dt.Rows[max][2] = "=";
                    dt.Rows[max][3] = "1-" + topIndex;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                 _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void btn_Bottom_add(object sender, RoutedEventArgs e)
        {
            try
            {
                int lastoccurancechoice = formUtil.GetLastRow(dt, 1);
                int lastoccuranceOperator = formUtil.GetLastRow(dt, 2);
                int lastoccuranceValue = formUtil.GetLastRow(dt, 1);
                int max = LargetOccurance(lastoccurancechoice, lastoccuranceOperator, lastoccuranceValue);
                if (max == -1)
                {
                    max = 0;
                }
                int bottomIndex = Combo_Bottom_Num.SelectedIndex + 1;
                int choicecount = ChoiceList.Items.Count;
                if (max != 10)
                {

                    dt.Rows[max][1] = "Bottom" + " " + bottomIndex;
                    dt.Rows[max][2] = "=";
                    dt.Rows[max][3] = ((choicecount - bottomIndex) + 1).ToString() + "-" + choicecount.ToString();
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                 _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void Remove_all_from_Dt(object sender, RoutedEventArgs e)
        {
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    dt.Rows[i][1] = string.Empty;
                    dt.Rows[i][2] = null;
                    dt.Rows[i][3] = string.Empty;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                 _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void Return_parant_Window(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void Choices_grid_CurrentCellChanged(object sender, EventArgs e)
        {
            var dg = (System.Windows.Controls.DataGrid)sender;
            ExpGrid = dg;
            dg.Focus();
        }

        private void bt1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                sub_datagrid.Focus();
            
            }

        }

        private void sub_datagrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            var uiElement = e.OriginalSource as UIElement;
            int index = sub_datagrid.SelectedIndex;
            if (sub_datagrid.CurrentCell.Column != null)
            {
                switch (index)
                { 
                    case 9:

                        if (e.Key == Key.Tab)
                        {
                            if (sub_datagrid.CurrentCell.Column.DisplayIndex == 3)
                            {
                                e.Handled = true;
                                Combo_Top_Num.Focus();
                            }
                            else
                            {
                                e.Handled = true;
                                uiElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Right));
                            }
                        }
                        break;
                    default: break;

                }
            }

        }

        private void bt2_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                sub_datagrid.Focus();
                sub_datagrid.SelectedIndex = 1;

               
            }
        }

        private void bt3_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                sub_datagrid.Focus();
           
            }
        }

        private void bt4_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                sub_datagrid.Focus();
               
            }
        }

        private void bt5_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                sub_datagrid.Focus();
              
            }
        }

        private void bt6_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                sub_datagrid.Focus();
              
            }
        }

        private void bt7_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                sub_datagrid.Focus();
              
            }
        }

        private void bt8_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                sub_datagrid.Focus();
          
            }
        }

        private void bt9_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                sub_datagrid.Focus();
               
            }
        }

        private void bt10_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                sub_datagrid.Focus();
              
            }
        }

        private void Combo_Top_Num_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                Add1.Focus();

            }
        }

        private void Add1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                Combo_Bottom_Num.Focus();

            }

        }

        private void Add2_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                insert_row.Focus();

            }
        }

        private void Combo_Bottom_Num_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                Add2.Focus();

            }
        }

        private void insert_row_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                moveup.Focus();

            }

        }

        private void moveup_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                Delete_row.Focus();

            }
        }

        private void Delete_row_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                movedown.Focus();

            }
        }

        private void movedown_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                btn_save.Focus();

            }
        }

        private void btn_save_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                btn_cancel.Focus();

            }
        }

        private void btn_deleteall_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
              
                sub_datagrid.Focus();

            }
        }

        private void btn_cancel_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                btn_deleteall.Focus();

            }
        }

        private void sub_datagrid_GotFocus(object sender, RoutedEventArgs e)
        {
           
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

        private void btn_RightArrow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var presentRow = (System.Data.DataRowView)sub_datagrid.CurrentCell.Item;
                int RowIndex = Convert.ToInt32(presentRow.Row.ItemArray[0].ToString());
                int index = sub_datagrid.SelectedIndex;
                if (index==-1)
                {
                    index = 0;
                }
                else if(index!=RowIndex-1)
                    {
                    index = 0;
                }
                SavetoGrid(index);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                 _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }
        string clipboardText = "";
        private void HandleCopyPaste(object sender, KeyEventArgs e)
        {
            var uiElement = e.OriginalSource as UIElement;
            var ue = e.OriginalSource as FrameworkElement;
            int datagridColumn =sub_datagrid.CurrentCell.Column.DisplayIndex;
            System.Windows.Controls.DataGridCell cell = frmutil.GetCell(sub_datagrid, sub_datagrid.SelectedIndex, datagridColumn);

            bool _altModifierPressed = (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl));
            if (_altModifierPressed && Keyboard.IsKeyDown(Key.V))
            {
                try
                {
                    Util.QS.GridCopyPaste copyPaste = new Util.QS.GridCopyPaste();
                    var data = copyPaste.PastetoDatagrid(sender);
                    int No_Row = copyPaste.No_Row;
                    int No_Column = copyPaste.No_Columns;
                    if (data == null) { e.Handled = true; }
                    if (!cell.IsEditing)
                    {
                        if (data != null)
                        {
                            e.Handled = true;
                            int datagridRow = sub_datagrid.SelectedIndex;
                            if (sub_datagrid.CurrentCell.Column.DisplayIndex == 2)
                            {
                             
                                if (No_Column > 3 || No_Row > (10 - sub_datagrid.SelectedIndex))
                                {
                                    MessageDialog.ErrorOk(LocalResource.COPY_ERROR_MSG);
                                    e.Handled = true;
                                }
                                else
                                {
                                    int RowIndex = sub_datagrid.SelectedIndex;
                                    for (int i = 0; i < No_Row; i++, RowIndex++)
                                    {
                                        for (int j = 1, col = 1; j <= No_Column; j++, col++)
                                        {
                                            if (col == 2)
                                            {
                                                if (data[i, (j - 1)] == null)
                                                {
                                                    
                                                }
                                                else
                                                {
                                                    if (data[i, (j - 1)].ToString() == "=" || data[i, (j - 1)].ToString() == "<>")
                                                    {
                                                        dt.Rows[RowIndex][col] = data[i, (j - 1)].ToString();
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (data[i, (j - 1)] == null)
                                                {
                                                    dt.Rows[RowIndex][col] = string.Empty;
                                                }
                                                else
                                                {
                                                    dt.Rows[RowIndex][col] = data[i, (j - 1)].ToString();
                                                }
                                            }

                                        }

                                    }

                                }
                            }
                            else if (sub_datagrid.CurrentCell.Column.DisplayIndex == 3)
                            {
                                if (No_Column > 2 || No_Row > (10 - sub_datagrid.SelectedIndex))
                                {
                                    MessageDialog.ErrorOk(LocalResource.COPY_ERROR_MSG);
                                    e.Handled = true;
                                }
                                else
                                {
                                    int RowIndex = sub_datagrid.SelectedIndex;
                                    for (int i = 0; i < No_Row; i++, RowIndex++)
                                    {
                                        for (int j = 0; j < No_Column; j++)
                                        {
                                            if (j == 0)
                                            {
                                                if (data[i, j] == null)
                                                {
                                                    
                                                }
                                                else
                                                {
                                                    if (data[i, j].ToString() == "=" || data[i, j].ToString() == "<>")
                                                    {
                                                        dt.Rows[RowIndex][2] = data[i, j].ToString();
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (data[i, j] == null)
                                                {
                                                    dt.Rows[RowIndex][3] = string.Empty;
                                                }
                                                else
                                                {
                                                    dt.Rows[RowIndex][3] = data[i, j].ToString();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else if (sub_datagrid.CurrentCell.Column.DisplayIndex == 4)
                            {
                                if (No_Column > 1 || No_Row > (10 - sub_datagrid.SelectedIndex))
                                {
                                    MessageDialog.ErrorOk(LocalResource.COPY_ERROR_MSG);
                                    e.Handled = true;
                                }
                                else
                                {
                                    int RowIndex = sub_datagrid.SelectedIndex;
                                    for (int i = 0; i < No_Row; i++, RowIndex++)
                                    {
                                        for (int j = 1, col = 3; j <= No_Column; j++, col++)
                                        {
                                            if (data[i, (j - 1)] == null)
                                            {
                                                dt.Rows[RowIndex][col] = string.Empty;
                                            }
                                            else
                                            {
                                                dt.Rows[RowIndex][col] = data[i, (j - 1)].ToString();
                                            }

                                        }
                                    }
                                }
                            }

                        }
                    }
                    else
                    {
                        string regexReplacedStr = "";
                        if (System.Windows.Clipboard.ContainsText(System.Windows.TextDataFormat.UnicodeText))
                        {
                            clipboardText = string.Empty;
                            clipboardText = System.Windows.Clipboard.GetText(System.Windows.TextDataFormat.UnicodeText);
                        }
                        System.Windows.Clipboard.SetText(data[0, 0].ToString());
                    }

                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                     _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                }
            }
            if (e.Key == Key.Delete)
            {
                try
                {
                    int RowIndex = 0;
                    if ((sub_datagrid.SelectedItems != null) && (sub_datagrid.SelectedItems.Count > 0))
                    {
                        for (int i = 0; i < sub_datagrid.SelectedItems.Count; i++)
                        {
                            var presentRow = (System.Data.DataRowView)sub_datagrid.SelectedItems[i];
                            RowIndex = Convert.ToInt32(presentRow.Row.ItemArray[0].ToString());
                            dt.Rows[RowIndex - 1][1] = string.Empty;
                            dt.Rows[RowIndex - 1][2] = null;
                            dt.Rows[RowIndex - 1][3] = string.Empty;
                        }
                    }
                }
                catch { }
            }
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                ue.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }
        public void keydata()
        {
            
        }

        private void CopyCommand(object sender, ExecutedRoutedEventArgs e)
        {
            var person = sub_datagrid.SelectedItems as DataTable;

        }

        private void WPF_DataGrid_GotFocus(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            var key = Key.Tab;                    
            var target = Keyboard.FocusedElement;   
            var routedEvent = Keyboard.KeyDownEvent;

            target.RaiseEvent(new System.Windows.Input.KeyEventArgs(Keyboard.PrimaryDevice,
             System.Windows.PresentationSource.FromVisual((Visual)target), 0, key)
            { RoutedEvent = routedEvent });
        }

        private void sub_datagrid_CopyingRowClipboardContent(object sender, DataGridRowClipboardEventArgs e)
        {
            try
            {
                e.ClipboardRowContent.RemoveAt(0);
                e.ClipboardRowContent.RemoveAt(0);
            }
            catch (Exception ex) {  _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException); }
        }

        private void ChoiceList_CopyingRowClipboardContent(object sender, DataGridRowClipboardEventArgs e)
        {
            try
            {
                e.ClipboardRowContent.RemoveAt(0);
               
            }
            catch (Exception ex) {  _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException); }
        }

        private void sub_datagrid_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            try
            {

                bool _altModifierPressed = (Keyboard.IsKeyUp(Key.LeftCtrl) || Keyboard.IsKeyUp(Key.RightCtrl));
                if (_altModifierPressed && e.Key == Key.C)
                {
                    if (System.Windows.Clipboard.ContainsText(System.Windows.TextDataFormat.UnicodeText))
                    {
                        clipboardText = string.Empty;
                        clipboardText = System.Windows.Clipboard.GetText(System.Windows.TextDataFormat.UnicodeText);
                    }
                }
                int datagridColumn = sub_datagrid.CurrentCell.Column.DisplayIndex;
                System.Windows.Controls.DataGridCell cell = frmutil.GetCell(sub_datagrid, sub_datagrid.SelectedIndex, datagridColumn);
                if (cell.IsEditing)
                {
                    bool _altModifierPressed1 = (Keyboard.IsKeyUp(Key.LeftCtrl) || Keyboard.IsKeyUp(Key.RightCtrl));
                    if (_altModifierPressed1 && Keyboard.IsKeyUp(Key.V))
                    {
                        if (!string.IsNullOrEmpty(clipboardText))
                        {
                            System.Windows.Clipboard.SetText(clipboardText);
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
