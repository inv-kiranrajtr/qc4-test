using Qc4Launcher.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using excel = Microsoft.Office.Interop.Excel;
using System.Data;
using System.Collections.ObjectModel;
using static FilterSettingsView.FilterSettingsClass;
using FilterSettingsView;
using static ExcelAddIn.Common.Constants;
using System.Text.RegularExpressions;
using Qc4Launcher.DB;
using log4net;
using System.Reflection;
using System.Windows.Threading;
using Vb = Microsoft.VisualBasic;
using System.Collections;
using System.Threading;
using System.IO;

namespace Qc4Launcher.Forms
{
    /// <summary>
    /// Interaction logic for SampleWeightBack.xaml
    /// </summary>
    public partial class SampleWeightBack : Window
    {
        excel.Workbook Workbook;
        bool IsSaveBtnClick = false;
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private string SelectedFile = "";
        private ObservableCollection<DataExport> _qstnvariablDD1 = new ObservableCollection<DataExport>();
        private List<object> data = new List<object>();
        private ObservableCollection<DataExport> _qstnvariablsa = new ObservableCollection<DataExport>();
        private ObservableCollection<DataExport> _qstnvariablnumeric = new ObservableCollection<DataExport>();
        public bool IsCustomWeighted;
        int checkAssistSheetValue;
        string cmbvariablename;
        bool changecombofirst;
        int variablentot;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        //
        public SampleWeightBack(excel.Workbook workbook, string filePath, ObservableCollection<DataExport> qstnvariablDD1, ObservableCollection<DataExport> qstnvariablnumeric, ref bool cweighted)
        {
            InitializeComponent();
            Workbook = workbook;
            SelectedFile = filePath;
            _qstnvariablDD1 = qstnvariablDD1;
            _qstnvariablnumeric = qstnvariablnumeric;
            IsCustomWeighted = cweighted;
        }
        private void GetSAType()
        {
            foreach (FilterSettingsClass.DataExport item in _qstnvariablDD1)
            {
                if (item.QuestionVariableType.Contains(AnswerType.SA))
                {
                    _qstnvariablsa.Add(item);
                }
            }
        }
        private void Check_Vertical_Checked(object sender, RoutedEventArgs e)
        {
            Check_Vertical_check();
        }
        int WBLen = 0;
        private void Check_Vertical_check()
        {
            if (dispatcherTimer.IsEnabled)
                dispatcherTimer.Stop();





            if (Check_Vertical.IsChecked == true)
            {
                sl.Width = 40;
                choice.Width = 140;

                weight.Width = WBLen < 14 ? 116 : new DataGridLength(1, DataGridLengthUnitType.Auto);
                gvN.Width = 60;
                populationSize.Width = 140;
                populationPortion.Width = 140;
                gvPer.Width = 60;
                Option_WB_N.IsEnabled = true;
                Option_WB_Proportion.IsEnabled = true;
                Command_WB_Calculate.IsEnabled = true;

                if (Option_WB_Proportion.IsChecked == true)
                {
                    gridweightback.Columns[2].Visibility = Visibility.Hidden;
                    gridweightback.Columns[3].Visibility = Visibility.Hidden;
                    gridweightback.Columns[4].Visibility = Visibility.Visible;
                    gridweightback.Columns[5].Visibility = Visibility.Visible;
                }
                else if (Option_WB_N.IsChecked == true)
                {
                    gridweightback.Columns[2].Visibility = Visibility.Visible;
                    gridweightback.Columns[3].Visibility = Visibility.Visible;
                    gridweightback.Columns[4].Visibility = Visibility.Hidden;
                    gridweightback.Columns[5].Visibility = Visibility.Hidden;
                }
                else
                {
                    Option_WB_N.IsChecked = true;
                }
                DataGridColumn gvcol = gridweightback.Columns[6];
                gvcol.IsReadOnly = true;


                dispatcherTimer.Start();
            }
            else
            {
                Option_WB_N.IsEnabled = false;
                Option_WB_Proportion.IsEnabled = false;
                Command_WB_Calculate.IsEnabled = false;
                gridweightback.Columns[2].Visibility = Visibility.Hidden;//hiding all n  % colmns
                gridweightback.Columns[3].Visibility = Visibility.Hidden;
                gridweightback.Columns[4].Visibility = Visibility.Hidden;
                gridweightback.Columns[5].Visibility = Visibility.Hidden;
                DataGridColumn gvcol = gridweightback.Columns[6];
                gvcol.IsReadOnly = false;
                choice.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                weight.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                dispatcherTimer.Start();
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            changecombofirst = true;
            GetSAType();
            this.Combo_OriginItem_Item.DataContext = _qstnvariablsa;
            var SettingSheet = ExcelUtil.GetWorkSheetByCodeName(Workbook, Constants.SheetCodeName.Setting);
            excel.Range range1 = SettingSheet.UsedRange;

            excel.Range starting = SettingSheet.Cells[2, 9];
            excel.Range ending = ExcelUtil.EndxlUp(starting).Offset[1, 3];
            excel.Range weightrange = SettingSheet.get_Range(starting, ending);
            Microsoft.Office.Interop.Excel.Range rar = SettingSheet.get_Range(starting, ending);
            FileUtil fileUtil = new FileUtil();

            DataTable dt = new DataTable();
            object[,] obj = rar.Value;//var obj = rar.Value;
            if (obj != null)
            {
                int rowCount = obj.GetUpperBound(0);
                int colCount = obj.GetUpperBound(1);


                for (int i = 1; i <= rowCount - 1; i++)
                {
                    if (i == 1) // First row - Taking as header
                    {
                        for (int j = 1; j <= colCount; j++) //
                        {
                            if (obj[i, j] == null)
                            {
                                dt.Columns.Add(fileUtil.GetNewColumnName(dt));
                            }
                            else
                            {
                                if (dt.Columns.Contains(Convert.ToString(obj[i, j])))
                                {
                                    dt.Columns.Add(fileUtil.GetNewColumnName(dt, Convert.ToString(obj[i, j]) + "-"));
                                }
                                else
                                {
                                    dt.Columns.Add(Convert.ToString(obj[i, j]));
                                }
                            }
                        }
                    }
                    else
                    {
                        DataRow dr = dt.NewRow();
                        for (int j = 1; j <= colCount; j++) //
                        {
                            dr[j - 1] = Convert.ToString(obj[i, j]);
                        }
                        dt.Rows.Add(dr);
                    }

                }



            }
            cmbvariablename = "";


            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    cmbvariablename = dt.Columns[1].ColumnName;
                    checkAssistSheetValue = Convert.ToInt32(obj[1, 3].ToString());
                }
                else
                {
                    _qstnvariablsa.Insert(0, DataExportObjectCreator());
                    cmbvariablename = _qstnvariablsa[0].QuestionVariable.ToString();
                }
            }
            DataExport target = _qstnvariablsa.Where(z => z.QuestionVariable == cmbvariablename).FirstOrDefault();
            int index = target == null ? -1 : _qstnvariablsa.IndexOf(target);
            Combo_OriginItem_Item.SelectedIndex = index;
            if (checkAssistSheetValue == 0)
            {
                this.Check_Vertical.IsChecked = false;
            }
            else
            {
                this.Check_Vertical.IsChecked = true;
                if (checkAssistSheetValue == 1)
                {
                    Option_WB_N.IsChecked = true;
                    Option_WB_Proportion.IsChecked = false;
                }
                else
                {
                    Option_WB_N.IsChecked = false;
                    Option_WB_Proportion.IsChecked = true;
                }
            }
            DataTable griddata = new DataTable();

            if (_qstnvariablsa[0].QuestionVariable != "")
            {
                griddata = FillDataGrid(dt);
                gridweightback.DataContext = griddata;
            }
            else
            {
                Check_Vertical.IsEnabled = false;
                Export.IsEnabled = false;
                gridweightback.DataContext = null;
            }
            Check_Vertical_check();

        }

        private DataExport DataExportObjectCreator()
        {
            DataExport dataExport_Obj = new DataExport();
            dataExport_Obj.Question = "";
            dataExport_Obj.QuestionChoiceNo = "";
            dataExport_Obj.QuestionIndex = 0;
            dataExport_Obj.QuestionVariable = "";
            dataExport_Obj.QuestionVariableType = "";
            return dataExport_Obj;
        }

        private DataTable CreateTable()
        {
            DataTable griddata = new DataTable();
            griddata.Columns.Add("Sl");
            griddata.Columns.Add("Choice");
            griddata.Columns.Add("N");
            griddata.Columns.Add("Based_On_Population_Size");
            griddata.Columns.Add("per");
            griddata.Columns.Add("Population_Proportion");
            griddata.Columns.Add("Weight");
            return griddata;
        }
        private DataTable FillDataGrid(DataTable sheetdata)
        {
            DataTable griddata = CreateTable();
            DataRow dr;
            if (sheetdata != null)
            {
                string seltex = cmbvariablename;
                DataExport target = _qstnvariablsa.Where(z => z.QuestionVariable == seltex).FirstOrDefault();
                if (target != null && target.QuestionVariable != "")
                {

                    var choices = target.Choisces.ToArray();
                    int count = choices.Length;
                    int colLen = 0;
                    if (sheetdata.Rows.Count > 0)
                    {
                        for (int i = 0; i < count; i++)
                        {

                            dr = griddata.NewRow();
                            try
                            {
                                dr["SL"] = (i + 1).ToString();
                                dr["Choice"] = frmutil.EscapeCRLF(choices[i]);
                                dr["N"] = "0";
                                if (sheetdata.Rows.Count <= i)
                                {
                                    dr["Based_On_Population_Size"] = "";
                                    dr["per"] = "0";
                                    dr["Population_Proportion"] = "";
                                    dr["Weight"] = "";
                                }
                                else
                                {
                                    dr["Based_On_Population_Size"] = sheetdata.Rows[i][1].ToString();
                                    dr["per"] = "0";
                                    dr["Population_Proportion"] = sheetdata.Rows[i][2].ToString();
                                    string s = sheetdata.Rows[i][3].ToString();
                                    dr["Weight"] = s;
                                    if (s.Length > 13)
                                        colLen = 15;
                                }

                                griddata.Rows.Add(dr);
                            }
                            catch { }
                        }

                        WBLen = colLen;
                        weight.Width = WBLen < 14 ? 116 : new DataGridLength(1, DataGridLengthUnitType.Auto);
                    }
                    else
                    {

                        for (int i = 0; i < count; i++)
                        {
                            dr = griddata.NewRow();

                            dr["SL"] = i + 1;
                            dr["Choice"] = choices[i];
                            dr["N"] = "0";
                            dr["Based_On_Population_Size"] = "";
                            dr["per"] = "0";
                            dr["Population_Proportion"] = "";
                            dr["Weight"] = "";
                            griddata.Rows.Add(dr);
                        }
                    }

                }
                else { Combo_OriginItem_Item.SelectedIndex = 0; FillGridOnComboChange(); }
            }
            bool IsDataExist = false;
            try
            {
                griddata = FillNPer(griddata, out IsDataExist);
            }
            catch (Exception ex)
            {

            }
            if (!IsDataExist && _qstnvariablsa[0].QuestionVariable != "")
            {
                Check_Vertical.IsEnabled = false;
                Export.IsEnabled = false;
                Combo_OriginItem_Item_SelectionChanged(null, null);
            }
            else
            {
                if (_qstnvariablsa[0].QuestionVariable != "")
                {
                    Check_Vertical.IsEnabled = true;
                    Export.IsEnabled = true;
                }
                else
                {
                    Check_Vertical.IsEnabled = false;
                    Export.IsEnabled = false;
                    gridweightback.DataContext = null;
                }
            }

            return griddata;
        }
        private void Combo_OriginItem_Item_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                QC4Common.Util.FormUtil formUtil = new QC4Common.Util.FormUtil();
                int selectedindex = Combo_OriginItem_Item.SelectedIndex;
                string NoOfChoice = string.Empty;
                if (selectedindex >= 0)
                {
                    this.Text_OriginItem_AnswerType.Text = _qstnvariablsa[selectedindex].QuestionVariableType.Split('/')[0];
                    try
                    {
                        _qstnvariablsa[selectedindex].QuestionChoiceNo = _qstnvariablsa[selectedindex].QuestionVariableType.Split('/')[1];
                    }
                    catch
                    {
                        this.Text_OriginItem_AnswerType.Text = "";
                        this.Text_OriginItem_SelectCount.Text = "";
                        this.Text_OriginItem_Question.Text = "";
                    }
                    if (_qstnvariablsa[selectedindex].QuestionChoiceNo != "")
                        this.Text_OriginItem_SelectCount.Text = _qstnvariablsa[selectedindex].QuestionChoiceNo.ToString();
                    else
                        this.Text_OriginItem_SelectCount.Text = "";
                    this.Text_OriginItem_Question.Text = formUtil.UnEscapeCRLF(_qstnvariablsa[selectedindex].Question);
                }
                try
                {
                    if (Combo_OriginItem_Item.SelectedItem == null)
                    {
                        this.Text_OriginItem_AnswerType.Text = "";
                        this.Text_OriginItem_SelectCount.Text = "";
                        this.Text_OriginItem_Question.Text = "";
                    }
                }
                catch
                {
                    this.Text_OriginItem_AnswerType.Text = "";
                    this.Text_OriginItem_SelectCount.Text = "";
                    this.Text_OriginItem_Question.Text = "";
                }
            }
            catch { }
            if (!changecombofirst)
            {
                WBLen = 0;
                FillGridOnComboChange();
            }
            else changecombofirst = false;

        }
        private void Option_WB_Proportion_Checked(object sender, RoutedEventArgs e)
        {
            if (Option_WB_Proportion.IsChecked == true)
            {
                gridweightback.Columns[2].Visibility = Visibility.Hidden;
                gridweightback.Columns[3].Visibility = Visibility.Hidden;
                gridweightback.Columns[4].Visibility = Visibility.Visible;
                gridweightback.Columns[5].Visibility = Visibility.Visible;
            }
        }
        private void Option_WB_N_Checked(object sender, RoutedEventArgs e)
        {
            if (Option_WB_N.IsChecked == true)
            {
                gridweightback.Columns[2].Visibility = Visibility.Visible;
                gridweightback.Columns[3].Visibility = Visibility.Visible;
                gridweightback.Columns[4].Visibility = Visibility.Hidden;
                gridweightback.Columns[5].Visibility = Visibility.Hidden;
            }

        }
        private void BTN_CLOSE_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateWeghting())
            {
                CalculateNPer();
                if (Check_Vertical.IsChecked == false)
                    checkAssistSheetValue = 0;
                else
                {
                    if (Option_WB_N.IsChecked == true)
                        checkAssistSheetValue = 1;
                    else if (Option_WB_Proportion.IsChecked == true)
                        checkAssistSheetValue = 2;
                }
                DataTable dt = CreateTable();
                dt = ((DataView)gridweightback.ItemsSource).ToTable();

                var SettingSheet = ExcelUtil.GetWorkSheetByCodeName(Workbook, Constants.SheetCodeName.Setting);

                SettingSheet.Application.EnableEvents = false;

                excel.Range starting = SettingSheet.Cells[2, 9];
                excel.Range ending = ExcelUtil.EndxlUp(starting).Offset[1, 3];
                excel.Range weightrange = SettingSheet.Range[starting, SettingSheet.Cells[dt.Rows.Count + 2, 12]];
                int rowcount = weightrange.Rows.Count;
                object[,] savearray = GetDataFromDatatable(dt);
                weightrange.NumberFormat = "@";
                if (savearray == null) return;
                int row = savearray.GetLength(0);
                int col = savearray.GetLength(1);
                if (rowcount <= dt.Rows.Count)
                    weightrange.Resize[savearray.GetLength(0), savearray.GetLength(1)].Value = savearray;
                else
                    weightrange.Value = savearray;

                SettingSheet.Application.EnableEvents = true;
                CrossTabulation.weighted = true;
                Cross_Tabulation.CrossTabulationStd.weighted = true;
                GrossTabulation.weighted = true;
                SummaryTableForm.weighted = true;
                GrossTabulationSetting.GrossTabulationSetting.weighted = true;
                IsSaveBtnClick = true;
                MessageBox.Show(LocalResource.WEIGHT_BACK_MESSAGE, Constants.MessageBoxTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }

        private object[,] GetDataFromDatatable(DataTable dt)
        {
            object[,] dataObject = new object[dt.Rows.Count + 1, 4];
            dataObject[0, 0] = Constants.WeightBack;
            dataObject[0, 1] = ((FilterSettingsClass.DataExport)((System.Windows.Controls.ComboBox)Combo_OriginItem_Item).SelectedItem).QuestionVariable;
            dataObject[0, 2] = checkAssistSheetValue;
            dataObject[0, 3] = dt.Rows.Count;
            if (checkAssistSheetValue == 0)
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    int k = 0;
                    for (int j = 0; j <= dt.Columns.Count - 1; j++)
                    {
                        if (j == 0)
                        {
                            dataObject[i + 1, 0] = Convert.ToString(dt.Rows[i][j]);
                            k++;
                        }
                        if (j == 6)
                        {
                            if (Convert.ToString(dt.Rows[i][j]) == null || Convert.ToString(dt.Rows[i][j]) == "")
                            {
                                MessageDialog.ErrorOk(string.Format(LocalResource.ERR_MSG_WEIGHTING_VALIDATION, i + 1), this); // ERR_MSG_WEIGHTING_VALIDATION
                                return null;
                            }
                            dataObject[i + 1, 3] = Convert.ToString(dt.Rows[i][j]) == null || Convert.ToString(dt.Rows[i][j]) == "" ? "0" : Convert.ToString(dt.Rows[i][j]);
                            k++;
                        }
                    }
                }
            }
            else if (checkAssistSheetValue == 1)
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    int k = 0;
                    for (int j = 0; j <= dt.Columns.Count - 1; j++)
                    {
                        if (j == 0)
                        {
                            dataObject[i + 1, 0] = Convert.ToString(dt.Rows[i][j]);
                            k++;
                        }
                        if (j == 3)
                        {
                            dataObject[i + 1, 1] = Convert.ToString(dt.Rows[i][j]);
                            k++;
                        }
                        if (j == 6)
                        {
                            dataObject[i + 1, 3] = Convert.ToString(dt.Rows[i][j]);
                            k++;
                        }
                    }
                }
            }
            else if (checkAssistSheetValue == 2)
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= dt.Columns.Count - 1; j++)
                    {
                        int k = 0;
                        if (j == 0)
                        {
                            dataObject[i + 1, 0] = Convert.ToString(dt.Rows[i][j]);
                            k++;
                        }
                        if (j == 5)
                        {
                            dataObject[i + 1, 2] = Convert.ToString(dt.Rows[i][j]);
                            k++;
                        }
                        if (j == 6)
                        {
                            dataObject[i + 1, 3] = Convert.ToString(dt.Rows[i][j]);
                            k++;
                        }
                    }
                }
            }
            return dataObject;

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
        private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txtgridtext = sender as TextBox;
            e.Handled = new Regex("[^0-9.]+").IsMatch(txtgridtext.Text);
            if (!(e.Handled) && Vb.Information.IsNumeric(txtgridtext.Text))
            {
                if (txtgridtext.Text.Length > 15 && gridweightback.CurrentColumn.Header.ToString() != LocalResource.GV_POPULATION_PROPORTION)
                {

                    e.Handled = true;
                }

            }
            else if (txtgridtext.Text.Length > 0)
            {

                e.Handled = true;
            }
            if (e.Handled == true) txtgridtext.Text = txtgridtext.Text.Remove(txtgridtext.Text.Length - 1);
            {
                txtgridtext.CaretIndex = txtgridtext.Text.Length;
            }
        }
        private void AllowOnlyNumbers(object sender, TextChangedEventArgs e)
        {
            TextBox txtgridtext = sender as TextBox;
            e.Handled = new Regex("[^0-9]+").IsMatch(txtgridtext.Text);
            if (e.Handled == true) txtgridtext.Text = txtgridtext.Text.Remove(txtgridtext.Text.Length - 1);
            txtgridtext.CaretIndex = txtgridtext.Text.Length;
        }

        private void Command_WB_Calculate_Click(object sender, RoutedEventArgs e)
        {
            CalculateNPer();

            if (dispatcherTimer.IsEnabled)
                dispatcherTimer.Stop();
            dispatcherTimer.Start();
        }
        private decimal GetTotalPolulation(DataTable dt, out decimal Ntot)
        {
            decimal total = 0;
            decimal ntotal = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][3] == null)
                {
                    dt.Rows[i][3] = "0";
                }
                float x;
                if (!float.TryParse(dt.Rows[i][3].ToString(), out x))
                {
                    dt.Rows[i][3] = "0";
                }

                total += Convert.ToDecimal(dt.Rows[i][3].ToString());
                if (dt.Rows[i][5] == null)
                {
                    dt.Rows[i][5] = "0";
                }

                if (!float.TryParse(dt.Rows[i][5].ToString(), out x))
                {
                    dt.Rows[i][5] = "0";
                }
                if (dt.Rows[i][2] == null)
                {
                    dt.Rows[i][2] = "0";
                }

                if (!float.TryParse(dt.Rows[i][2].ToString(), out x))
                {
                    dt.Rows[i][2] = "0";
                }
                ntotal += Convert.ToDecimal(dt.Rows[i][2].ToString());
            }
            gridweightback.DataContext = dt;
            Ntot = ntotal;
            return total;
        }
        private void FillGridOnComboChange()
        {
            try
            {
                QC4Common.Util.FormUtil formUtil = new QC4Common.Util.FormUtil();
                var itm = (FilterSettingsView.FilterSettingsClass.DataExport)((System.Windows.Controls.ComboBox)Combo_OriginItem_Item).SelectedItem;
                if (itm == null || itm.QuestionVariable == "")
                {
                    Check_Vertical.IsChecked = false;
                    Check_Vertical.IsEnabled = false;
                    Export.IsEnabled = false;
                    gridweightback.DataContext = null;
                    return;
                }
                string seltex = cmbvariablename;
                if (changecombofirst == false)
                {
                    seltex = ((FilterSettingsView.FilterSettingsClass.DataExport)((System.Windows.Controls.ComboBox)Combo_OriginItem_Item).SelectedItem).QuestionVariable;
                }
                DataExport target = _qstnvariablsa.Where(z => z.QuestionVariable == seltex).FirstOrDefault();
                var choices = target.Choisces.ToArray();
                int count = choices.Length;

                string[] choice = new string[count];
                for (int i = 0; i < count; i++)
                {
                    choice[i] = formUtil.EscapeCRLF(choices[i]);
                }

                DataTable dt = CreateTable();
                if (changecombofirst == true)
                {
                    changecombofirst = false;
                }
                else
                {
                    DataRow dr;
                    for (int i = 0; i < count; i++)
                    {
                        dr = dt.NewRow();

                        dr["SL"] = i + 1;
                        dr["Choice"] = choice[i];
                        dr["N"] = "0";
                        dr["Based_On_Population_Size"] = "";
                        dr["per"] = "0";
                        dr["Population_Proportion"] = "";
                        dr["Weight"] = "";
                        dt.Rows.Add(dr);
                    }
                }
                bool IsDataExist = true;
                try
                {
                    dt = FillNPer(dt, out IsDataExist);
                }
                catch (Exception ex)
                {

                }
                gridweightback.DataContext = dt;
                if (!IsDataExist)
                {
                    Check_Vertical.IsEnabled = false;
                    Export.IsEnabled = false;
                    gridweightback.IsEnabled = false;
                    MessageBox.Show(string.Format(LocalResource.WEIGHT_BACK_NO_DATA_MESSAGE, seltex), Constants.MessageBoxTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    gridweightback.IsEnabled = true;
                    Check_Vertical.IsEnabled = true;
                    Export.IsEnabled = true;
                }

            }
            catch (Exception ex) { _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException); }
            if (dispatcherTimer.IsEnabled)
                dispatcherTimer.Stop();
            dispatcherTimer.Start();
        }

        private DataTable Get()
        {
            DataLoadVariableDetails dbload = new DataLoadVariableDetails(Workbook);

            string seltex = cmbvariablename;
            if (changecombofirst == false)
            {
                seltex = ((FilterSettingsView.FilterSettingsClass.DataExport)((System.Windows.Controls.ComboBox)Combo_OriginItem_Item).SelectedItem).QuestionVariable;
            }
            DataTable dt = dbload.GetVariableValues(seltex);

            return dt;
        }
        private DataTable FillNPer(DataTable dt, out bool IsDataExistInAnswerTable)
        {
            DataTable variablevalues = Get();
            variablentot = 0;
            if (variablevalues != null && variablevalues.Rows.Count > 0)
            {
                IsDataExistInAnswerTable = true;
                for (int i = 0; i < variablevalues.Rows.Count; i++)
                {
                    string cellval = variablevalues.Rows[i][0] == null ? "" : variablevalues.Rows[i][0].ToString();
                    int retcellvalue = 0;
                    if (int.TryParse(cellval, out retcellvalue))
                    {
                        dt.Rows[retcellvalue - 1][2] = variablevalues.Rows[i][1];
                        variablentot += Convert.ToInt32(variablevalues.Rows[i][1].ToString());
                    }
                }
                for (int i = 0; i < variablevalues.Rows.Count; i++)
                {
                    string cellval = variablevalues.Rows[i][0] == null ? "" : variablevalues.Rows[i][0].ToString();
                    int retcellvalue = 0;
                    if (int.TryParse(cellval, out retcellvalue))
                    {
                        int nval = 0;
                        string cellnval = variablevalues.Rows[i][1] == null ? "" : variablevalues.Rows[i][1].ToString();
                        if (int.TryParse(cellnval, out nval))
                        {
                            double result = ((float)nval / variablentot);
                            result = result * 100;
                            if (result.ToString().Replace(".", "").Length > 16)
                            {
                                dt.Rows[retcellvalue - 1][4] = result.ToString().Contains('.') ? result.ToString().Substring(0, 17) : result.ToString().Substring(0, 16);
                            }
                            else
                            {
                                dt.Rows[retcellvalue - 1][4] = result.ToString();//result.ToString("#.00")
                            }
                        }


                    }
                }
            }
            else
            {
                IsDataExistInAnswerTable = false;
            }
            return dt;
        }
        private bool ValidateWeghting()
        {
            bool isValid = true;
            DataTable dt = CreateTable();
            dt = ((DataView)gridweightback.ItemsSource).ToTable();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Check_Vertical.IsChecked == true && Option_WB_N.IsChecked == true && (dt.Rows[i][3] == null || dt.Rows[i][3].ToString() == "" || dt.Rows[i][6] == null || dt.Rows[i][6].ToString() == ""))
                {
                    MessageBox.Show(string.Format(LocalResource.ERR_MSG_WEIGHTING_MISSING, i + 1), Constants.MessageBoxTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                    isValid = false;
                    break;
                }
                else if (Check_Vertical.IsChecked == true && Option_WB_Proportion.IsChecked == true && (dt.Rows[i][5] == null || dt.Rows[i][5].ToString() == "" || dt.Rows[i][6] == null || dt.Rows[i][6].ToString() == ""))
                {
                    MessageBox.Show(string.Format(LocalResource.ERR_MSG_WEIGHTING_MISSING, i + 1), Constants.MessageBoxTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                    isValid = false;
                    break;
                }
                else if (Check_Vertical.IsChecked == false && (dt.Rows[i][6] == null || dt.Rows[i][6].ToString() == ""))
                {
                    MessageBox.Show(string.Format(LocalResource.ERR_MSG_WEIGHTING_MISSING, i + 1), Constants.MessageBoxTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                    isValid = false;
                    break;
                }
            }
            return isValid;
        }
        private void CalculateNPer()
        {
            DataTable dt = CreateTable();
            dt = ((DataView)gridweightback.ItemsSource).ToTable();
            decimal ntot = 0;
            int colLen = 0;
            decimal populationtotal = GetTotalPolulation(dt, out ntot);
            if (Check_Vertical.IsChecked == false)

            {//for save button if nothng is selected
                string s = "0";
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        try
                        {
                            s = null == dt.Rows[i][6].ToString() ? "0" : dt.Rows[i][6].ToString();// dt.Rows[i][6]= "0"; 
                            dt.Rows[i][6] = s;
                            if (s.Length > 13)
                                colLen = s.Length;
                        }
                        catch { dt.Rows[i][6] = "0"; }
                    }
                }
            }
            else if (Option_WB_N.IsChecked == true)
            {
                try
                {
                    int n = 0;
                    decimal popsize = 0;
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            n = Convert.ToInt32(dt.Rows[i][2].ToString());
                            popsize = Convert.ToDecimal(dt.Rows[i][3].ToString());
                            try
                            {
                                if (n != 0 && populationtotal != 0)
                                {
                                    decimal dval = ((popsize / (decimal)populationtotal) * (decimal)ntot / n);
                                    dt.Rows[i][6] = dval.ToString();
                                    if (dval.ToString().Length > 13)
                                        colLen = dval.ToString().Length;

                                }
                                else
                                {
                                    dt.Rows[i][6] = "0";
                                }


                            }
                            catch { dt.Rows[i][6] = "0"; }
                        }
                    }
                }
                catch { }
            }
            else if (Option_WB_Proportion.IsChecked == true)
            {
                try
                {
                    decimal per = 0;
                    decimal poppro = 0;
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            per = Convert.ToDecimal(dt.Rows[i][4].ToString());
                            poppro = Convert.ToDecimal(dt.Rows[i][5].ToString());
                            try
                            {
                                string dval = "";
                                if (per != 0 && poppro != 0)
                                {
                                    dval = (poppro / per).ToString();
                                    dt.Rows[i][6] = dval;
                                }
                                else dt.Rows[i][6] = "0";

                                if (dval.ToString().Length > 13)
                                    colLen = dval.ToString().Length;
                            }
                            catch { dt.Rows[i][6] = "0"; }
                        }
                    }
                }
                catch { }
                //%
                //WB = Population Proportion รท %
            }
            else
            {//for save button if nothng is selected
                string s = "0";
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        try
                        {
                            s = null == dt.Rows[i][6].ToString() ? "0" : dt.Rows[i][6].ToString();
                            dt.Rows[i][6] = s;
                            if (s.ToString().Length > 13)
                                colLen = s.ToString().Length;
                        }
                        catch { dt.Rows[i][6] = "0"; }
                    }
                }
            }
            WBLen = colLen;
            weight.Width = WBLen < 14 ? 116 : new DataGridLength(1, DataGridLengthUnitType.Auto);
        }
        QC4Common.Util.FormUtil frmutil = new QC4Common.Util.FormUtil();



        public void CopyPaste(string input)
        {
            var data = input.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries));
        }

        public List<string[]> CopiedTextData()
        {

            List<string[]> copiedArray = new List<string[]>();
            if (Clipboard.ContainsText(TextDataFormat.UnicodeText))
            {
                string CopiedText = Clipboard.GetText(TextDataFormat.UnicodeText);

                if (CopiedText != "" && !string.IsNullOrEmpty(CopiedText) && !string.IsNullOrWhiteSpace(CopiedText))
                {
                    var rows = CopiedText.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    foreach (var item in rows)
                    {
                        string[] columns = item.Split(new string[] { "\t" }, StringSplitOptions.None).ToArray();
                        string[] coldataArray = new string[columns.Length];
                        if (columns.Length > 1)
                        {
                            MessageDialog.ErrorOk(LocalResource.COPY_ERROR_MSG);
                            copiedArray = null;
                            return copiedArray;
                        }
                        int count = 0;
                        foreach (var column in columns)
                        {
                            string coldata = string.Empty;
                            if (Regex.Match(column, @"^(\d+)(.*)$").Success)
                            {
                                string digits = Regex.Match(column, @"^-?\d+(,\d+)*(\.\d+)?$").Value;
                                if (Regex.Match(digits, @"\d+").Success)
                                {
                                    double isDatadigit = 0;
                                    if (double.TryParse(digits, out isDatadigit))
                                    {
                                        if (isDatadigit >= 0)
                                        {
                                            coldata = digits.Replace(",", "");
                                            
                                        }
                                        else
                                        {
                                            coldata = string.Empty;
                                        }
                                    }
                                    else
                                    {
                                        coldata = string.Empty;
                                    }
                                }
                            }
                            coldataArray[count] = coldata;
                            count++;
                        }
                        copiedArray.Add(coldataArray);
                    }
                }

            }

            return copiedArray;
        }
        string clipboardText = string.Empty;
        private void Gridweightback_PreviewKeyDown(object sender, KeyEventArgs e)
        {
           
                try
                {

                    bool _altModifierPressed = (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl));
                    if (_altModifierPressed && Keyboard.IsKeyDown(Key.V))
                    {
                        List<string[]> copiedValues = new List<string[]>();
                        int datagridColumn = gridweightback.CurrentCell.Column.DisplayIndex;
                        if (gridweightback.SelectedIndex == -1) { return; }
                        DataGridCell cell = frmutil.GetCell(gridweightback, gridweightback.SelectedIndex, datagridColumn);
                        if ((datagridColumn == 3 && !cell.IsReadOnly) || (datagridColumn == 5 && !cell.IsReadOnly) || (datagridColumn == 6 && !cell.IsReadOnly))
                        {
                            copiedValues = CopiedTextData();
                        }

                        if (copiedValues != null)
                        {
                            if (copiedValues.Count > 0)
                            {
                                int rowlength = copiedValues.Count();
                                int collength = copiedValues[0].Count();

                                if (!cell.IsEditing)
                                {
                                    int dataGridowIndex = gridweightback.SelectedIndex;
                                    DataTable dataInTable = (DataTable)gridweightback.DataContext;
                                    DataGridRow[] rows = Enumerable.Range(gridweightback.SelectedIndex, Math.Min(gridweightback.Items.Count, copiedValues.Count))
                                        .Select(rowIndex =>
                                        gridweightback.ItemContainerGenerator.ContainerFromIndex(gridweightback.SelectedIndex) as DataGridRow)
                                       .ToArray();
                                    int rowStart = gridweightback.SelectedIndex;
                                    int rowEnd = rows.Length + rowStart;
                                    int totalRowCount = gridweightback.Items.Count;
                                    if (rowEnd > totalRowCount)
                                    {
                                        rowEnd = gridweightback.Items.Count;
                                    }
                                    int count = 0;
                                    if (Check_Vertical.IsChecked == true && (datagridColumn == 3 || datagridColumn == 5))
                                    {
                                        count = 0;
                                        if (datagridColumn == 3)
                                        {
                                            for (int i = rowStart; i < rowEnd; i++)
                                            {
                                                string valData = copiedValues[count][0].Split(new char[] { '.' })[0];
                                                dataInTable.Rows[i][datagridColumn] = valData;
                                                count++;
                                            }
                                        }
                                        else
                                        {
                                            for (int i = rowStart; i < rowEnd; i++)
                                            {
                                                dataInTable.Rows[i][datagridColumn] = copiedValues[count][0];
                                                count++;
                                            }
                                        }
                                    }
                                    else if (datagridColumn == 6)
                                    {
                                        count = 0;
                                        for (int i = rowStart; i < rowEnd; i++)
                                        {
                                            string value = copiedValues[count][0].Length <= 15 ? copiedValues[count][0] : copiedValues[count][0].Substring(0, 15);
                                            dataInTable.Rows[i][datagridColumn] = value;
                                            count++;
                                        }
                                    }
                                    gridweightback.DataContext = dataInTable;
                                }
                                else
                                {

                                    if (Clipboard.ContainsText(TextDataFormat.UnicodeText))
                                    {
                                        clipboardText = string.Empty;
                                        clipboardText = Clipboard.GetText(TextDataFormat.UnicodeText);
                                    }

                                    Clipboard.SetText(copiedValues[0][0].ToString());
                                }




                            }
                            else
                            {
                                e.Handled = true;
                                //  Clipboard.Clear();
                            }
                        }
                        else
                        {
                            e.Handled = true;
                        }
                    }



                }
                catch (Exception ex)
                {

                }

                try
                {
                    if (e.Key == Key.Delete)
                    {
                        int columnSelectedindex = gridweightback.CurrentCell.Column.DisplayIndex;
                        int countColumnSelectedIndex = gridweightback.SelectedIndex > gridweightback.Items.IndexOf(gridweightback.CurrentItem) ?
                            gridweightback.SelectedIndex : gridweightback.Items.IndexOf(gridweightback.CurrentItem);
                        DataGridCell cell = frmutil.GetCell(gridweightback, countColumnSelectedIndex, columnSelectedindex);
                        if (!cell.IsEditing)
                        {

                            if (!cell.IsReadOnly)
                            {
                                var items = gridweightback.SelectedItems;
                                DataTable dataInTable = (DataTable)gridweightback.DataContext;
                                if (columnSelectedindex == 3 || columnSelectedindex == 5 || columnSelectedindex == 6)
                                {
                                    foreach (var selection in gridweightback.SelectedItems)
                                    {
                                        DataRowView row = (DataRowView)selection;
                                        int index = Convert.ToInt32(row.Row[0]) - 1;
                                        dataInTable.Rows[index][columnSelectedindex] = string.Empty;
                                    }
                                }
                                gridweightback.DataContext = dataInTable;
                            }
                        }
                    }
                }
                catch (Exception ex) { }
                int currentCell = gridweightback.CurrentColumn.DisplayIndex;
                int currentRow = gridweightback.SelectedIndex;
                if ((e.Key == Key.Tab || e.Key == Key.Enter) && (currentCell == 3 || currentCell == 5 || currentCell == 6) && (gridweightback.Items.Count - 1) > currentRow)
                {
                    e.Handled = true;
                    gridweightback.SelectedItem = gridweightback.Items[currentRow + 1];
                    gridweightback.ScrollIntoView(gridweightback.Items[currentRow + 1]);
                    gridweightback.CurrentColumn.DisplayIndex = currentCell;
                    gridweightback.CurrentCell = new DataGridCellInfo(gridweightback.Items[currentRow + 1], gridweightback.CurrentColumn);
                }
           
        }

        private void clipboardthreadWorker(string v)
        {
            Clipboard.SetDataObject(v);
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            bool finished = false;
            if (Check_Vertical.IsChecked == true)
            {
                for (int i = 0; i < gridweightback.Items.Count; i++)
                {
                    DataGridRow firstRow = gridweightback.ItemContainerGenerator.ContainerFromItem(gridweightback.Items[i]) as DataGridRow;
                    if (firstRow != null)
                    {
                        DataGridCell firstColumnInFirstRow = gridweightback.Columns[6].GetCellContent(firstRow).Parent as DataGridCell;

                        Style style = Application.Current.FindResource("Cellstyle") as Style;
                        firstColumnInFirstRow.Style = style;
                        weight.Width = WBLen < 14 ? 116 : new DataGridLength(1, DataGridLengthUnitType.Auto);
                        finished = true;
                    }
                    else
                    {
                        finished = false;
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < gridweightback.Items.Count; i++)
                {
                    DataGridRow firstRow = gridweightback.ItemContainerGenerator.ContainerFromItem(gridweightback.Items[i]) as DataGridRow;
                    if (firstRow != null)
                    {
                        DataGridCell firstColumnInFirstRow = gridweightback.Columns[6].GetCellContent(firstRow).Parent as DataGridCell;
                        firstColumnInFirstRow.Style = null;
                        firstColumnInFirstRow.Foreground = Brushes.Black;
                        finished = true;
                    }
                    else
                    {
                        finished = false;
                        break;
                    }
                }
            }
            if (finished)
                dispatcherTimer.Stop();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsSaveBtnClick)
            {
                CrossTabulation.weighted = false;
                Cross_Tabulation.CrossTabulationStd.weighted = false;
                GrossTabulation.weighted = false;
                SummaryTableForm.weighted = false;
            }
        }
        System.Windows.Controls.ComboBox combo = null;
        string LastSelectedText = "";
        int LastSelected = 0;
        private void Combo_OriginItem_Item_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox txt = null;
            if (e.OriginalSource is TextBox)
                txt = (TextBox)e.OriginalSource;

            if (combo.SelectedIndex != 0)
                LastSelected = combo.SelectedIndex;
            else if (combo.SelectedIndex == 0)
            {
                LastSelectedText = combo.Text;
            }
            if (combo.SelectedIndex != 0)
                LastSelected = combo.SelectedIndex;
            if (Key.Delete == e.Key && (this.Combo_OriginItem_Item.IsKeyboardFocusWithin || this.Combo_OriginItem_Item.IsDropDownOpen))
            {
                LastSelectedText = "";
                LastSelected = -1;
                e.Handled = false;
            }
            else if (Key.Delete == e.Key)
            {
                e.Handled = true;
            }
        }

        private void Combo_OriginItem_Item_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            System.Windows.Controls.ComboBox sen = ((System.Windows.Controls.ComboBox)sender);
            combo = sen;
        }

        private void Combo_OriginItem_Item_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.ComboBox sen = ((System.Windows.Controls.ComboBox)sender);
            System.Windows.Controls.TextBox textBox = sen.Template.FindName("PART_EditableTextBox", sen) as System.Windows.Controls.TextBox;
            if (textBox != null)
            {
                textBox.AddHandler(System.Windows.Controls.TextBox.PreviewMouseDownEvent, new MouseButtonEventHandler(textBox_PreviewMouseDown), true);
            }
        }
        void textBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            combo.IsDropDownOpen = true;
        }

        private void Combo_OriginItem_Item_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            System.Windows.Controls.ComboBox sen = ((System.Windows.Controls.ComboBox)sender);
            if ((Key.Delete == e.Key || ((sen.SelectedIndex == -1 && sen.Text == "") || (sen.SelectedIndex == -1 && sen.Text != ""))) && (this.Combo_OriginItem_Item.IsKeyboardFocusWithin || this.Combo_OriginItem_Item.IsDropDownOpen))
            {
                LastSelected = 0;
                this.Combo_OriginItem_Item.SelectedIndex = 0;
            }
            else if (sen.SelectedItem == null && (sen.IsKeyboardFocusWithin || sen.IsDropDownOpen))
            {
                if (sen.Name != "Combo_OriginItem_Item")
                    sen.SelectedIndex = LastSelected;
            }
        }

        private void Combo_OriginItem_Item_DropDownClosed(object sender, EventArgs e)
        {
            System.Windows.Controls.ComboBox sen = ((System.Windows.Controls.ComboBox)sender);
            if (sen.SelectedItem != null)
            {
                sen.Text = ((FilterSettingsClass.DataExport)(sen.SelectedItem)).QuestionVariable;
            }
            else
            {
                sen.Text = "";
            }
        }

        private void Gridweightback_PreviewKeyUp(object sender, KeyEventArgs e)
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
                    int datagridColumn = gridweightback.CurrentCell.Column.DisplayIndex;
                    DataGridCell cell = frmutil.GetCell(gridweightback, gridweightback.SelectedIndex, datagridColumn);
                bool _altModifierPressed1 = (Keyboard.IsKeyUp(Key.LeftCtrl) || Keyboard.IsKeyUp(Key.RightCtrl));
                if (_altModifierPressed1 && e.Key==Key.V)
                {
                    if (cell.IsEditing)
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
        private void DisableRightMenu(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }


    }
}

