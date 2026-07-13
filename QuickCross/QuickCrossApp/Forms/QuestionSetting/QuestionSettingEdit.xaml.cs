using log4net;
using Qc4Launcher.Forms.Multivariate_Analysis;
using Qc4Launcher.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;//
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Vb = Microsoft.VisualBasic;
using excel = Microsoft.Office.Interop.Excel;
namespace Qc4Launcher.Forms.QuestionSetting
{
    /// <summary>
    /// Interaction logic for QuestionSettingEdit.xaml
    /// </summary>
    public partial class QuestionSettingEdit : Window
    {
        private string tempClipBoardData;
        public int selectedindex;
        string clipboardText = "";
        DataTable dt;
        DataTable tempdt = null;
        DataTable copydt = null;
        private int prevoiuslimit = 1000;
        List<string> names;
        bool checksubsave = false;
        string previewsnumber = string.Empty;
        private bool gridloadfirst = true;
        System.Windows.Controls.CheckBox gridheadercheckboxall;
        System.Windows.Controls.Label labelcheckboxall;
        private static QuestionSettingMainWindow mAinWindow = null;
        private static MV_Main MV_Main = null;
        public bool back_next_btn = false;
        bool checkflag = true;
        DataTable subtotal_dt = null;
        System.Windows.Controls.DataGridCell cell;
        bool matrixvariable_parant = false;
        int matrixvariable_parant_child_count = 0;
        bool matrixvariable_child = false;
        bool updateFromsubtotal = false;
        bool isimprossedkey = false;
        private bool Combo_boxflag = false;
        string previousvalue = string.Empty;
        bool iseditablestate = false;
        private static Microsoft.Office.Interop.Excel.Workbook excelWorkBook = null;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        QC4Common.Util.FormUtil frmutil = new QC4Common.Util.FormUtil();
        string temppath = string.Empty;
        private int lastoccurance = 0;
        string[] cnames;
        string[] snames;
        bool EditSaveErrorflag = false;
        bool multiEdit = false;
        int indexValue = 0;
        private bool ischeckAllboxEnabled;
        public static bool isFactor = false;
        public static List<int> INdexInt = new List<int>();


        public QuestionSettingEdit(int index, Microsoft.Office.Interop.Excel.Workbook workBook, QuestionSettingMainWindow mainWindow = null, bool multiEdit = false, MV_Main mV_Main = null)
        {
            this.selectedindex = index;
            excelWorkBook = workBook;
            mAinWindow = mainWindow;
            this.multiEdit = multiEdit;
            MV_Main = mV_Main;
            InitializeComponent();
            if (!isFactor && multiEdit)
            {
                btn_back.Visibility = Visibility.Hidden;
                btn_next.Visibility = Visibility.Hidden;

            }
            else if (multiEdit && isFactor)
            {
                btn_back.Visibility = Visibility.Hidden;
                btn_next.Visibility = Visibility.Hidden;
                if (INdexInt.Count > 1)
                {
                    btn_next.Visibility = Visibility.Visible;
                }
            }

        }
        private void Disablefields()
        {
            if (selectedindex >= 0)
            {

                GetDatafromvaraibledic();

            }
        }


        private void Data_fillDatagrid()
        {

            if (selectedindex != -1)
            {
                string c_count = (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).CategoryCount.ToString();
                int count = Convert.ToInt32(c_count);
                dt = FillDataGrid(count);

                data_grid.DataContext = dt;
            }


        }



        private void GetDatafromvaraibledic()
        {
            try
            {
                Text_Item_Name.Text = (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).Variable;
                Combo_Answer_Type.Text = (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).AnswerType;
                Combo_Category.SelectedIndex = (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).CategoryCount + 1;
                prevoiuslimit = (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).CategoryCount;
                Text_QuestionA.Text = checksinglequete((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).TableHeading);
                Text_QuestionB.Text = checksinglequete((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).Question.ToString());
                string value = string.Empty;
                if ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).SubTotalCount > 0)
                {
                    value = (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).SubTotalCount.ToString();
                    if ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).AddSubTotal == "1")
                    {
                        Check_SubTotal.IsChecked = true;
                    }
                    else
                    {
                        Check_SubTotal.IsChecked = false;
                    }
                    Command_Subtotal.IsEnabled = true;
                    subtotal_dt = FillSubTotalDatagrid((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).SubTotalCount);

                }
                else
                {
                    Check_SubTotal.IsChecked = false;
                    Command_Subtotal.IsEnabled = false;
                    subtotal_dt = FillSubTotalDatagrid((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).SubTotalCount);
                }
                Check_SubTotal.Content = string.Format(LocalResource.ADDQS_SUBTOTAL_CHECK_LABEL, value);
                if ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).Sort != "")
                {
                    Check_Sort.IsChecked = true;
                    try
                    {
                        Combo_Sort.SelectedIndex = Convert.ToInt32((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).Sort) - 1;

                    }
                    catch (Exception ex)
                    {
                        string message = ex.Message;
                        _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                    }
                }
                else
                {
                    Combo_Sort.SelectedIndex = -1;
                    Check_Sort.IsChecked = false;

                }
                if ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).CountBase.Length > 1)
                {
                    CheckMACN.IsChecked = true;
                }
                else
                {
                    CheckMACN.IsChecked = false;
                }
                if ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).AnswerType.ToString() == "SA")
                {
                    EnablebuttonsLabelsforSA();
                }
                else if ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).AnswerType.ToString() == "MA")
                {
                    EnablebuttonsLabelsforMA();
                }
                else
                {
                    HideButtonsLabels();
                }

                if (selectedindex != -1)
                {
                    int count = (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).CategoryCount;

                    dt = FillDataGrid1(count);

                    data_grid.DataContext = dt;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

        }
        private void HideButtonsLabels()
        {
            Label_Category.Visibility = Visibility.Hidden;
            Combo_Category.Visibility = Visibility.Hidden;
            Check_Sort.Visibility = Visibility.Hidden;
            Label_Sort2.Visibility = Visibility.Hidden;
            Label_Sort3.Visibility = Visibility.Hidden;
            Combo_Sort.IsEnabled = false;
            Combo_Sort.Visibility = Visibility.Hidden;
            Check_SubTotal.Visibility = Visibility.Hidden;
            Command_Subtotal.IsEnabled = false;
            Command_Subtotal.Visibility = Visibility.Hidden;
            CheckMACN.IsEnabled = false;
            CheckMACN.Visibility = Visibility.Hidden;
            data_grid.Visibility = Visibility.Hidden;

            Label_MACN.Visibility = Visibility.Hidden;

        }
        private string GetDuplicateVariableName(string oldvariableName)
        {
            string newVariable = "N" + oldvariableName;
            int j = 1;
            for (int i = 0; i < Util.Definiotion.VariableDictionary.Count; i++)
            {
                if ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(i).Variable == newVariable)
                {
                    newVariable = "N" + j + oldvariableName;
                    i = 0;
                    j++;
                }
            }

            return newVariable;
        }
        private void StringToIntListSmean()
        {
            try
            {
                string str = (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).Score;
                string[] val = str.Split(',');
                snames = val;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }
        private void StringToIntListCmean()
        {
            try
            {
                string str = GetCommaSeperated((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).Count,
                        (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).Variable);
                string[] val = str.Split(',');
                cnames = val;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }
        private DataTable FillDataGrid2(int limit, List<string> Choices)
        {
            QC4Common.Util.FormUtil formUtil = new QC4Common.Util.FormUtil();
            DataTable griddata = CreateTable();
            DataRow dr;
            StringToIntListSmean();
            StringToIntListCmean();
            int j = 0;
            int k = 0;
            for (int i = 1; i <= limit; i++)
            {
                dr = griddata.NewRow();
                dr["No"] = i;
                dr["Choice"] = formUtil.EscapeCRLF(checksinglequete(Choices[i - 1]));
                if (snames != null)
                {
                    if (snames.Length > k)
                    {
                        dr["SMean"] = snames[i - 1];
                        k++;
                    }
                    else
                    {
                        dr["SMean"] = string.Empty;
                    }
                }
                else
                {
                    dr["SMean"] = string.Empty;
                }
                if (cnames != null)
                {
                    if (cnames.Length > j)
                    {
                        if (cnames[j] == i.ToString())
                        {
                            dr["cMean"] = "True";
                            j++;
                        }
                        else
                        {
                            dr["cMean"] = "False";
                        }
                    }
                    else
                    {
                        dr["cMean"] = "False";
                    }
                }
                else
                {
                    dr["cMean"] = string.Empty;
                }


                griddata.Rows.Add(dr);
            }
            if (j == 0)
            {
                CheckMACN.IsEnabled = false;
                data_grid.Columns[2].IsReadOnly = false;
                if (gridheadercheckboxall != null)
                {
                    if (gridheadercheckboxall.IsChecked == false)
                    {
                        checkflag = true;
                    }
                    else if (gridheadercheckboxall.IsChecked == true)
                    {
                        checkflag = false;
                    }
                    else
                    {
                        checkflag = false;
                    }
                    gridheadercheckboxall.IsChecked = false;
                    gridheadercheckboxall.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                }
            }
            else
            {
                data_grid.Columns[2].IsReadOnly = true;
                if (gridheadercheckboxall != null)
                {
                    if (gridheadercheckboxall.IsChecked == true)
                    {
                        checkflag = true;
                    }
                    else if (gridheadercheckboxall.IsChecked == false)
                    {
                        checkflag = false;
                    }
                    else
                    {
                        checkflag = false;
                    }
                    gridheadercheckboxall.IsChecked = true;
                    gridheadercheckboxall.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE1E1E1"));


                }
            }

            return griddata;
        }
        private DataTable FillDataGrid1(int limit)
        {
            QC4Common.Util.FormUtil formUtil = new QC4Common.Util.FormUtil();
            DataTable griddata = CreateTable();
            DataRow dr;
            StringToIntListSmean();
            StringToIntListCmean();
            int j = 0;
            int k = 0;
            for (int i = 1; i <= limit; i++)
            {
                dr = griddata.NewRow();
                dr["No"] = i;
                dr["Choice"] = formUtil.EscapeCRLF(checksinglequete((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).Choices.ElementAt(i - 1).ToString()));
                if (snames != null)
                {
                    if (snames.Length > k)
                    {
                        dr["SMean"] = snames[i - 1];
                        k++;
                    }
                    else
                    {
                        dr["SMean"] = string.Empty;
                    }
                }
                else
                {
                    dr["SMean"] = string.Empty;
                }
                if (cnames != null)
                {
                    if (cnames.Length > j)
                    {
                        if (cnames[j] == i.ToString())
                        {
                            dr["cMean"] = "True";
                            j++;
                        }
                        else
                        {
                            dr["cMean"] = "False";
                        }
                    }
                    else
                    {
                        dr["cMean"] = "False";
                    }
                }
                else
                {
                    dr["cMean"] = string.Empty;
                }


                griddata.Rows.Add(dr);
            }
            if (j == 0)
            {
                CheckMACN.IsEnabled = false;
                data_grid.Columns[2].IsReadOnly = false;
                if (gridheadercheckboxall != null)
                {
                    if (gridheadercheckboxall.IsChecked == false)
                    {
                        checkflag = true;
                    }
                    else if (gridheadercheckboxall.IsChecked == true)
                    {
                        checkflag = false;
                    }
                    else
                    {
                        checkflag = false;
                    }
                    gridheadercheckboxall.IsChecked = false;
                    gridheadercheckboxall.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                }
            }
            else
            {
                data_grid.Columns[2].IsReadOnly = true;
                if (gridheadercheckboxall != null)
                {
                    if (gridheadercheckboxall.IsChecked == true)
                    {
                        checkflag = true;
                    }
                    else if (gridheadercheckboxall.IsChecked == false)
                    {
                        checkflag = false;
                    }
                    else
                    {
                        checkflag = false;
                    }
                    gridheadercheckboxall.IsChecked = true;
                    gridheadercheckboxall.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE1E1E1"));


                }
            }

            return griddata;
        }
        private void Subtotalsetting_btn_Click(object sender, RoutedEventArgs e)
        {
            checksubsave = true;
            if (Check_any_changes())
            {
                checksubsave = false;
                if (MessageBoxResult.Yes == System.Windows.MessageBox.Show(LocalResource.QS_SUBTOTAL_CONFIRMATION, LocalResource.TITLE_QC, MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                    updateFromsubtotal = true;
                    SaveQuestion();
                    if (!EditSaveErrorflag)
                    {
                        Set_Subtotal_values();
                        Qc4Launcher.Forms.QuestionSetting.SubtotalSetting sub = new SubtotalSetting(selectedindex, subtotal_dt, excelWorkBook, matrixvariable_parant_child_count);


                        sub.Closed += Subtotal_close;
                        this.ShowInTaskbar = false;
                        sub.ShowDialog();
                    }
                    else
                    {
                        EditSaveErrorflag = false;
                    }
                }
            }
            else
            {
                Qc4Launcher.Forms.QuestionSetting.SubtotalSetting subtotal = new SubtotalSetting(selectedindex, subtotal_dt, excelWorkBook, matrixvariable_parant_child_count);
                subtotal.Closed += Subtotal_close;
                this.ShowInTaskbar = false;
                subtotal.ShowDialog();
            }



        }
        private void Subtotal_close(object sender, EventArgs e)
        {
            this.ShowInTaskbar = true;
            var child = (sender as Qc4Launcher.Forms.QuestionSetting.SubtotalSetting);
            subtotal_dt = child.subdt;
            if (subtotal_dt.Rows.Count > 0)
            {
                Check_SubTotal.IsChecked = true;
                Check_SubTotal.Content = string.Format(LocalResource.ADDQS_SUBTOTAL_CHECK_LABEL, subtotal_dt.Rows.Count);
            }
            else
            {
                Check_SubTotal.IsChecked = true;
                Check_SubTotal.Content = string.Format(LocalResource.ADDQS_SUBTOTAL_CHECK_LABEL, string.Empty);
            }
            Text_Item_Name.IsEnabled = false;
            Combo_Answer_Type.IsEnabled = false;
            if ((Util.Definiotion.VariableDictionary.Values.ToList().ElementAt(selectedindex).QuestionFlag == "New"))
            {
                if ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).SubTotalCount > 0)
                {
                    Combo_Category.IsEnabled = false;
                }
                else
                {
                    Combo_Category.IsEnabled = true;
                }
            }
            else
            {
                Combo_Category.IsEnabled = false;
            }
            data_grid.DataContext = dt;
            try
            {
                data_grid.Items.Refresh();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
            updateFromsubtotal = false;
        }


        private void LoadItemAnswertype()
        {

        }
        private void Check_SortStateChange(object sender, RoutedEventArgs e)
        {

            if ((bool)Check_Sort.IsChecked)
            {
                Combo_Sort.IsEnabled = true;
                changeCombo_sort();

            }
            else
            {
                Combo_Sort.SelectedIndex = -1;
                Combo_Sort.IsEnabled = false;
            }
        }
        private void AnswertypeChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                if (Text_Item_Name.Text.Length >= 1)
                {
                    addqs_btn_save.IsEnabled = true;
                }

                if (((sender as System.Windows.Controls.ComboBox).SelectedItem as ComboBoxItem) != null)
                {
                    if (((sender as System.Windows.Controls.ComboBox).SelectedItem as ComboBoxItem).Content.ToString() == "SA")
                    {
                        EnablebuttonsLabelsforSA();
                    }
                    else if (((sender as System.Windows.Controls.ComboBox).SelectedItem as ComboBoxItem).Content.ToString() == "MA")
                    {
                        EnablebuttonsLabelsforMA();
                    }
                    else if (((sender as System.Windows.Controls.ComboBox).SelectedItem as ComboBoxItem).Content.ToString() == "FA")
                    {
                        HideButtonsLabels();
                    }
                    else if (((sender as System.Windows.Controls.ComboBox).SelectedItem as ComboBoxItem).Content.ToString() == "N")
                    {
                        HideButtonsLabels();
                    }
                }
                else
                {
                    HideButtonsLabels();


                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }
        private void AddComboxitem()
        {
            try
            {
                Combo_Category.Items.Add(LocalResource.LBL_AUTO);
                for (int i = 0; i < 1000; i++)
                {
                    Combo_Category.Items.Add(i + 1);
                    Combo_Sort.Items.Add(i + 1);
                }
                Combo_Category.Text = Combo_Category.Items.GetItemAt(1).ToString();
                Combo_boxflag = true;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }
        private void EnablebuttonsLabelsforSA()
        {
            try
            {
                Label_Category.Visibility = Visibility.Visible;
                Combo_Category.Visibility = Visibility.Visible;
                Check_Sort.Visibility = Visibility.Visible;
                Label_Sort2.Visibility = Visibility.Visible;
                Label_Sort3.Visibility = Visibility.Visible;
                Combo_Sort.Visibility = Visibility.Visible;
                Check_SubTotal.Visibility = Visibility.Visible;
                CheckMACN.Visibility = Visibility.Hidden;
                Label_MACN.Visibility = Visibility.Hidden;
                Command_Subtotal.Visibility = Visibility.Visible;
                data_grid.Visibility = Visibility.Visible;
                data_grid.Columns[3].Visibility = Visibility.Collapsed;
                if (dt.Rows.Count < 1)
                {
                    dt = FillDataGrid(1000);

                    data_grid.DataContext = dt;
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }



        }
        private void EnablebuttonsLabelsforMA()
        {
            Label_Category.Visibility = Visibility.Visible;
            Combo_Category.Visibility = Visibility.Visible;
            Check_Sort.Visibility = Visibility.Visible;
            Label_Sort2.Visibility = Visibility.Visible;
            Label_Sort3.Visibility = Visibility.Visible;
            Combo_Sort.Visibility = Visibility.Visible;
            Check_SubTotal.Visibility = Visibility.Visible;
            Command_Subtotal.Visibility = Visibility.Visible;
            data_grid.Visibility = Visibility.Visible;
            CheckMACN.Visibility = Visibility.Visible;
            Label_MACN.Visibility = Visibility.Visible;
            data_grid.Columns[3].Visibility = Visibility.Visible;
            if (dt.Rows.Count < 1)
            {
                dt = FillDataGrid(1000);
                data_grid.DataContext = dt;
            }

        }
        private void changeCombo_sort()
        {
            try
            {
                if ((bool)Check_Sort.IsChecked)
                {
                    int limit;
                    if (Combo_Category.SelectedIndex == -1 || Combo_Category.SelectedIndex == 1 || Combo_Category.SelectedIndex == 0)
                    {
                        limit = 1000;
                    }
                    else
                    {
                        limit = Combo_Category.SelectedIndex - 1;
                    }
                    Combo_Sort.Items.Clear();
                    for (int i = 1; i <= limit; i++)
                    {
                        Combo_Sort.Items.Add(i);
                    }
                    if (Combo_Category.SelectedIndex == 0)
                    {
                        Combo_Sort.Text = string.Empty;
                    }
                    else if (limit == 1000)
                    {
                        Combo_Sort.Text = "1";
                    }
                    else
                    {
                        Combo_Sort.Text = limit.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }


        private void Combo_Category_Validation(object sender, SelectionChangedEventArgs e)
        {
            try
            {


                changeCombo_sort();
                if (Combo_Category.SelectedItem != null)
                {
                    if (Combo_Category.SelectedIndex > 1)
                    {
                        if (gridheadercheckboxall != null)
                        {
                            gridheadercheckboxall.IsEnabled = true;
                            checkflag = true;
                        }
                    }

                    System.Windows.Controls.ComboBox combo = sender as System.Windows.Controls.ComboBox;
                    ObservableCollection<string> choicescount = new ObservableCollection<string>();
                    int limit = combo.SelectedIndex;
                    if (limit != 0)
                    {


                        if (limit == 1)
                        {
                            limit = 1000;
                        }
                        else
                        {
                            limit = limit - 1;
                        }

                        if (gridloadfirst)
                        {
                            if (combo != null)
                            {
                                dt = FillDataGrid(limit);
                                data_grid.DataContext = dt;
                                gridloadfirst = false;
                            }
                        }
                        else
                        {

                            if (prevoiuslimit < limit)
                            {
                                DataRow dr;

                                for (; prevoiuslimit < limit; prevoiuslimit++)
                                {
                                    dr = dt.NewRow();
                                    dr[0] = prevoiuslimit + 1;
                                    dr[1] = string.Empty;
                                    dr[2] = string.Empty;
                                    dr[3] = new System.Windows.Controls.CheckBox();
                                    dt.Rows.Add(dr);
                                }

                            }

                            else
                            {
                                int dtcount = dt.Rows.Count;
                                for (; limit < dtcount; dtcount--)
                                {
                                    dt.Rows.RemoveAt(dtcount - 1);
                                }

                            }

                        }
                        if (tempdt == null)
                        {
                            CreateTable1();
                            DataRow dr;
                            for (int i = 0; i < 1000; i++)
                            {
                                if (((string)dt.Rows[i][1] != string.Empty))
                                {
                                    dr = tempdt.NewRow();
                                    dr["Data"] = (string)dt.Rows[i][1];
                                    tempdt.Rows.Add(dr);
                                }
                            }
                        }
                        else if (tempdt.Rows.Count < 1 && (Combo_Category.SelectedIndex == 0 || Combo_Category.SelectedIndex == 1))
                        {
                            if (tempdt == null)
                            {
                                CreateTable1();
                                DataRow dr;
                                for (int i = 0; i < 1000; i++)
                                {
                                    if (((string)dt.Rows[i][1] != string.Empty))
                                    {
                                        dr = tempdt.NewRow();
                                        dr["Data"] = (string)dt.Rows[i][1];
                                        tempdt.Rows.Add(dr);
                                    }
                                }
                            }
                            else
                            {
                                DataRow dr;
                                for (int i = 0; i < 1000; i++)
                                {
                                    if (((string)dt.Rows[i][1] != string.Empty))
                                    {
                                        dr = tempdt.NewRow();
                                        dr["Data"] = (string)dt.Rows[i][1];
                                        tempdt.Rows.Add(dr);
                                    }
                                }
                            }
                        }
                        prevoiuslimit = limit;
                    }

                }

                else
                {

                }
            }
            catch
            {

            }
        }

        private void Combo_sort_Validation(object sender, SelectionChangedEventArgs e)
        {

            if (Combo_Sort.SelectedItem == null)
            {


                Combo_Sort.Text = "";

            }

        }

        private void Check_SubTotalChange(object sender, RoutedEventArgs e)
        {
            if ((bool)Check_SubTotal.IsChecked)
            {
                Command_Subtotal.IsEnabled = true;
            }
            else
            {

                Command_Subtotal.IsEnabled = false;
            }
        }


        private void Window_Closed(object sender, EventArgs e)
        {
            back_next_btn = false;
            if (mAinWindow != null)
            {
                mAinWindow.Show();
            }
            else
            {
                MV_Main.Show();
            }
        }

        private void Windowloaded(object sender, RoutedEventArgs e)
        {
            AddComboxitem();
            Disablefields();
            if (selectedindex == Util.Definiotion.VariableDictionary.Count - 1)
            {
                btn_next.IsEnabled = false;
            }
            if (selectedindex == 0)
            {
                btn_back.IsEnabled = false;
            }
            if (tempdt == null)
            {
                tempdt = FilltempDt();

            }

            addqs_btn_save.IsEnabled = true;
            Set_Subtotal_values();
            if (Check_SubTotal.IsChecked == true && Check_SubTotal.IsEnabled == true)
            {
                Command_Subtotal.IsEnabled = true;
            }
            checkQuestionType();
            copydt = dt.Copy();
        }
        private void Set_Subtotal_values()
        {
            try
            {
                if (subtotal_dt == null)
                {
                    subtotal_dt = FillSubTotalDatagrid((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).SubTotalCount);
                }
            }

            catch (Exception ex)
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }
        private DataTable CreateTableSubtotal()
        {
            DataTable griddata = new DataTable();
            griddata.Columns.Add("Criteria");
            griddata.Columns.Add("Subtotal");
            return griddata;
        }
        private DataTable FillSubTotalDatagrid(int limit)
        {
            DataTable griddata = CreateTableSubtotal();
            DataRow dr;
            for (int i = 1; i <= limit; i++)
            {
                dr = griddata.NewRow();
                dr["Criteria"] = (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).SubTotals.ElementAt(i - 1).Criteria;
                dr["Subtotal"] = (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).SubTotals.ElementAt(i - 1).Subtotal; ;
                griddata.Rows.Add(dr);
            }
            return griddata;
        }
        private void checkQuestionType()
        {
            try
            {
                if ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).QuestionFlag != "New")
                {
                    Text_Item_Name.IsEnabled = false;
                    Combo_Answer_Type.IsEnabled = false;
                    Combo_Category.IsEnabled = false;


                    if ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).QuestionCount > 0)
                    {
                        matrixvariable_parant = true;
                        matrixvariable_parant_child_count = (int)(Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).QuestionCount;
                        matrixvariable_child = false;
                        Text_QuestionA.IsEnabled = true;
                        Text_QuestionB.IsEnabled = true;
                        if (Combo_Sort.SelectedIndex >= 0)
                        {
                            Check_Sort.IsEnabled = true;
                            Check_Sort.IsChecked = true;
                            Combo_Sort.IsEnabled = true;
                        }
                        else
                        {
                            Check_Sort.IsEnabled = true;
                            Check_Sort.IsChecked = false;
                            Combo_Sort.IsEnabled = false;
                        }
                        Check_SubTotal.IsEnabled = true;
                        if (Check_SubTotal.IsChecked == true)
                        {
                            Command_Subtotal.IsEnabled = true;
                        }
                        else
                        {
                            Command_Subtotal.IsEnabled = false;
                        }
                        if ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).CountBase.Length > 1)
                        {
                            CheckMACN.IsChecked = true;
                        }
                        else
                        {
                            CheckMACN.IsChecked = false;
                        }
                        data_grid.IsEnabled = true;
                        for (int i = 1; i < data_grid.Columns.Count; i++)
                        {
                            if (i != 2)
                            {
                                data_grid.Columns[i].IsReadOnly = false;
                            }
                        }
                    }
                    else if (checkSelectedquestionMatrix_child())
                    {
                        Text_QuestionA.IsEnabled = false;
                        Text_QuestionB.IsEnabled = true;
                        Combo_Sort.IsEnabled = false;
                        Command_Subtotal.IsEnabled = false;
                        Check_SubTotal.IsEnabled = false;
                        Check_Sort.IsEnabled = false;
                        if ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).CountBase.Length > 1)
                        {
                            CheckMACN.IsChecked = true;
                        }
                        else
                        {
                            CheckMACN.IsChecked = false;
                        }
                        CheckMACN.IsEnabled = false;
                        for (int i = 1; i < data_grid.Columns.Count; i++)
                        {
                            data_grid.Columns[i].IsReadOnly = true;
                        }
                        data_grid.IsEnabled = false;
                    }
                    else
                    {
                        Text_QuestionA.IsEnabled = true;
                        Text_QuestionB.IsEnabled = true;
                        if (Combo_Sort.SelectedIndex >= 0)
                        {
                            Check_Sort.IsChecked = true;
                            Combo_Sort.IsEnabled = true;
                        }
                        else
                        {
                            Check_Sort.IsChecked = false;
                            Combo_Sort.IsEnabled = false;
                        }
                        Check_SubTotal.IsEnabled = true;
                        if (Check_SubTotal.IsChecked == true)
                        {
                            Command_Subtotal.IsEnabled = true;
                        }
                        else
                        {
                            Command_Subtotal.IsEnabled = false;
                        }
                        if ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).CountBase.Length > 1)
                        {
                            CheckMACN.IsChecked = true;
                        }
                        else
                        {
                            CheckMACN.IsChecked = false;
                        }
                        data_grid.IsEnabled = true;
                        for (int i = 1; i < data_grid.Columns.Count; i++)
                        {
                            if (i != 2)
                            {
                                data_grid.Columns[i].IsReadOnly = false;
                            }
                        }
                    }

                }
                else
                {
                    Text_Item_Name.IsEnabled = false;
                    Combo_Answer_Type.IsEnabled = false;

                    if ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).QuestionCount > 0)
                    {
                        matrixvariable_parant = true;
                        matrixvariable_parant_child_count = (int)(Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).QuestionCount;
                        matrixvariable_child = false;
                        Text_QuestionA.IsEnabled = true;
                        Text_QuestionB.IsEnabled = true;
                        if ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).SubTotalCount > 0)
                        {
                            Combo_Category.IsEnabled = false;
                        }
                        else
                        {
                            Combo_Category.IsEnabled = true;
                        }
                        if (Combo_Sort.SelectedIndex >= 0)
                        {
                            Check_Sort.IsEnabled = true;
                            Check_Sort.IsChecked = true;
                            Combo_Sort.IsEnabled = true;
                        }
                        else
                        {
                            Check_Sort.IsEnabled = true;
                            Check_Sort.IsChecked = false;
                            Combo_Sort.IsEnabled = false;
                        }


                        Check_SubTotal.IsEnabled = true;
                        if (Check_SubTotal.IsChecked == true)
                        {
                            Command_Subtotal.IsEnabled = true;
                        }
                        else
                        {
                            Command_Subtotal.IsEnabled = false;
                        }
                        if ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).CountBase.Length > 1)
                        {
                            CheckMACN.IsChecked = true;
                        }
                        else
                        {
                            CheckMACN.IsChecked = false;
                        }
                        data_grid.IsEnabled = true;
                    }
                    else if (checkSelectedquestionMatrix_child())
                    {
                        Text_QuestionA.IsEnabled = false;
                        Text_QuestionB.IsEnabled = true;
                        Combo_Sort.IsEnabled = false;
                        Combo_Category.IsEnabled = false;
                        Command_Subtotal.IsEnabled = false;
                        Check_SubTotal.IsEnabled = false;
                        Check_Sort.IsEnabled = false;
                        if ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).CountBase.Length > 1)
                        {
                            CheckMACN.IsChecked = true;
                        }
                        if ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).CountBase.Length > 1)
                        {
                            CheckMACN.IsChecked = false;
                        }
                        CheckMACN.IsEnabled = false;
                        data_grid.IsEnabled = false;
                        for (int i = 1; i < data_grid.Columns.Count; i++)
                        {
                            data_grid.Columns[i].IsReadOnly = true;
                        }
                    }
                    else
                    {
                        Text_QuestionA.IsEnabled = true;
                        Text_QuestionB.IsEnabled = true;
                        if ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).SubTotalCount > 0)
                        {
                            Combo_Category.IsEnabled = false;
                        }
                        else
                        {
                            Combo_Category.IsEnabled = true;
                        }

                        if (Combo_Sort.SelectedIndex >= 0)
                        {
                            Check_Sort.IsChecked = true;
                            Combo_Sort.IsEnabled = true;
                        }
                        else
                        {
                            Check_Sort.IsChecked = false;
                            Combo_Sort.IsEnabled = false;
                        }

                        Check_SubTotal.IsEnabled = true;
                        if (Check_SubTotal.IsChecked == true)
                        {
                            Command_Subtotal.IsEnabled = true;
                        }
                        else
                        {
                            Command_Subtotal.IsEnabled = false;
                        }
                        if ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).CountBase.Length > 1)
                        {
                            CheckMACN.IsChecked = true;
                        }
                        else
                        {
                            CheckMACN.IsChecked = false;
                        }
                        data_grid.IsEnabled = true;
                        for (int i = 1; i < data_grid.Columns.Count; i++)
                        {
                            if (i != 2)
                            {
                                data_grid.Columns[i].IsReadOnly = false;
                            }
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
        private bool checkSelectedquestionMatrix_child()
        {
            try
            {
                for (int i = selectedindex; i >= 0; i--)
                {
                    if ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(i).QuestionCount != 0)
                    {
                        if ((i + (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(i).QuestionCount) > selectedindex)
                        {
                            matrixvariable_parant = false;
                            matrixvariable_parant_child_count = 0;
                            matrixvariable_child = true;
                            break;
                        }
                    }
                }
                if (matrixvariable_child)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                return false;
            }

        }
        private void DisableCommoncontrols()
        {
            Text_Item_Name.IsEnabled = false;
            Combo_Answer_Type.IsEnabled = false;
        }
        private void Add_New_question_Save_btn(object sender, RoutedEventArgs e)
        {
            SaveQuestion();

        }
        public void SaveQuestion()
        {
            string addsubtotal = string.Empty;
            if (!(((bool)Check_SubTotal.IsChecked == true && subtotal_dt.Rows.Count <= 0) && !updateFromsubtotal))
            {
                string qustion = Text_QuestionB.Text;
                if (Text_QuestionB.Text.Replace(" ", string.Empty).Length != 0)
                {
                    Util.QS.AddNewQuestion addNew = new Util.QS.AddNewQuestion(excelWorkBook, temppath);
                    if (Combo_Answer_Type.Text == Constants.AnswerType.N || Combo_Answer_Type.Text == Constants.AnswerType.FA || Combo_Answer_Type.Text == "D")
                    {
                        if (addNew.EditToSheet(Text_Item_Name.Text, Combo_Answer_Type.Text, Text_QuestionA.Text.TrimStart().TrimEnd(), Text_QuestionB.Text.TrimStart().TrimEnd(), 0, 0, null, false, selectedindex, matrixvariable_parant, matrixvariable_parant_child_count))
                        {
                            if (!updateFromsubtotal)
                            {


                                MessageDialog.Info(LocalResource.ADDQS_INFO_MSG_SUCCESS);
                            }

                            copydt = dt.Copy();
                            Resetall();
                        }
                        else
                        {
                            EditSaveErrorflag = true;
                        }
                    }
                    else if (choiceCheck(dt))

                    {
                        if (Combo_Answer_Type.Text == Constants.AnswerType.SA)
                        {
                            int index;
                            if (Combo_Category.SelectedIndex == 1 || Combo_Category.SelectedIndex == 0)
                            {
                                index = lastoccurance + 1;
                            }
                            else
                            {
                                index = Combo_Category.SelectedIndex - 1;
                            }
                            if ((Check_SubTotal.IsChecked == true) && ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).SubTotalCount > 0))
                            {
                                addsubtotal = "1";
                            }
                            else
                            {
                                addsubtotal = string.Empty;
                            }

                            if (addNew.EditToSheet(Text_Item_Name.Text, Combo_Answer_Type.Text, Text_QuestionA.Text.TrimStart().TrimEnd(), Text_QuestionB.Text.TrimStart().TrimEnd(), index, Combo_Sort.SelectedIndex, dt, false, selectedindex, matrixvariable_parant, matrixvariable_parant_child_count, addsubtotal: addsubtotal))

                            {
                                if (!updateFromsubtotal)
                                {

                                    MessageDialog.Info(LocalResource.ADDQS_INFO_MSG_SUCCESS);
                                }

                                copydt = dt.Copy();
                                Resetall();

                            }
                            else
                            {
                                EditSaveErrorflag = true;
                            }
                        }
                        if (Combo_Answer_Type.Text == Constants.AnswerType.MA)
                        {
                            int index;
                            bool checkMacn = false;
                            if ((bool)CheckMACN.IsChecked)
                            {
                                checkMacn = true;
                            }
                            if (Combo_Category.SelectedIndex == 1 || Combo_Category.SelectedIndex == 0)
                            {
                                index = lastoccurance + 1;
                            }
                            else
                            {
                                index = Combo_Category.SelectedIndex - 1;
                            }
                            if ((Check_SubTotal.IsChecked == true) && ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).SubTotalCount > 0))
                            {
                                addsubtotal = "1";
                            }
                            else
                            {
                                addsubtotal = string.Empty;
                            }
                            if (addNew.EditToSheet(Text_Item_Name.Text, Combo_Answer_Type.Text, Text_QuestionA.Text.TrimStart().TrimEnd(), Text_QuestionB.Text.TrimStart().TrimEnd(), index, Combo_Sort.SelectedIndex, dt, checkMacn, selectedindex, matrixvariable_parant, matrixvariable_parant_child_count, addsubtotal: addsubtotal))

                            {
                                if (!updateFromsubtotal)
                                {
                                    MessageDialog.Info(LocalResource.ADDQS_INFO_MSG_SUCCESS);

                                }

                                copydt = dt.Copy();
                                Resetall();
                            }
                            else
                            {
                                EditSaveErrorflag = true;
                            }
                        }
                    }
                    else
                    {
                        EditSaveErrorflag = true;
                    }
                }
                else
                {
                    MessageDialog.ErrorOk(LocalResource.ADDQS_ERROR_MSG_QUSTION_TEXT_MISS);
                    EditSaveErrorflag = true;
                }
            }
            else
            {
                MessageDialog.ErrorOk(LocalResource.QS_ERROR_CONFI_SUBTOTAL);
                EditSaveErrorflag = true;
            }
        }
        private void Resetall()
        {

        }
        private bool choiceCheck(DataTable dt)
        {
            try
            {
                int i;

                var count = dt
              .AsEnumerable()

              .Where(p => p.Field<string>("Choice") == string.Empty)
              .Count();
                if (Combo_Category.SelectedIndex == 1)
                {

                    if (count == 1000)
                    {
                        MessageDialog.ErrorOk(LocalResource.QS_CHOICE_NOT_SET);
                        return false;
                    }

                    for (i = 0; i < 1000; i++)
                    {
                        if (dt.Rows[i][1].ToString() != "")
                        {
                            lastoccurance = i;
                        }
                    }
                    for (i = 0; i < lastoccurance; i++)
                    {
                        if (dt.Rows[i][1].ToString() == "")
                        {
                            break;
                        }
                    }
                    if (i < lastoccurance)
                    {
                        MessageDialog.ErrorOk(string.Format(LocalResource.QS_CHOICE_LINE_ERROR, (i + 1).ToString()));
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    if (count == Combo_Category.SelectedIndex - 1)
                    {
                        MessageDialog.ErrorOk(LocalResource.QS_CHOICE_NOT_SET);
                        return false;
                    }
                    else
                    {
                        for (i = 0; i < Combo_Category.SelectedIndex - 1; i++)
                        {
                            if (dt.Rows[i][1].ToString() == "")
                            {
                                break;
                            }
                        }
                        if (i < Combo_Category.SelectedIndex - 1)
                        {
                            MessageDialog.ErrorOk(string.Format(LocalResource.QS_CHOICE_LINE_ERROR, (i + 1).ToString()));
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);

                return false;

            }

        }



        private void Datagrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {


        }


        private DataTable CreateTable()
        {
            DataTable griddata = new DataTable();
            griddata.Columns.Add("No");
            griddata.Columns.Add("Choice");
            griddata.Columns.Add("SMean");
            griddata.Columns.Add("cMean");
            return griddata;
        }
        private DataTable CreateTable1()
        {
            DataTable griddata = new DataTable();
            griddata.Columns.Add("Data");
            return griddata;
        }
        private DataTable FillDataGrid(int limit)
        {
            DataTable griddata = CreateTable();
            DataRow dr;
            for (int i = 1; i <= limit; i++)
            {
                dr = griddata.NewRow();
                dr["No"] = i;
                dr["Choice"] = String.Empty;
                dr["SMean"] = String.Empty;
                dr["cMean"] = String.Empty;
                griddata.Rows.Add(dr);
            }
            return griddata;
        }
        private DataTable FilltempDt()
        {

            DataTable griddata = CreateTable1();
            return griddata;
        }

        private void Listallchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (data_grid.SelectedItems.Count > 0)
                {
                    data_grid.SelectedIndex = data_grid.SelectedIndex;
                    data_grid.CommitEdit();
                }
                if (cell != null)
                {
                    cell.Focus();
                }
                if (tempdt == null)
                {

                }
                if (tempdt.Rows.Count < 1 && (Combo_Category.SelectedIndex == 0 || Combo_Category.SelectedIndex == 1))
                {
                    if (gridheadercheckboxall.IsChecked == false)
                    {
                        checkflag = true;
                    }
                    else
                    {
                        checkflag = false;
                    }
                    gridheadercheckboxall.IsChecked = false;

                }
                if (Combo_Category.SelectedIndex == 0 || Combo_Category.SelectedIndex == 1)
                {
                    if (checkflag)
                    {
                        for (int i = 0; i < tempdt.Rows.Count; i++)
                        {
                            dt.Rows[i][3] = true;

                        }
                        gridheadercheckboxall.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                    }
                    checkflag = true;
                }
                else
                {
                    if (checkflag)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dt.Rows[i][3] = true;
                        }
                        gridheadercheckboxall.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                    }
                    checkflag = true;
                }
                if (data_grid.IsEnabled == false)
                {
                    CheckMACN.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void Listallunchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (iseditablestate)
                {
                    data_grid.SelectedIndex = data_grid.SelectedIndex + 1;
                }
                if (gridheadercheckboxall.Background.ToString() == "#FFE1E1E1")
                {
                    if (Combo_Category.SelectedIndex == 0 || Combo_Category.SelectedIndex == 1)
                    {

                        var falsecount = 0;
                        for (int i = 0; i < tempdt.Rows.Count; i++)
                        {
                            if ((string)dt.Rows[i][3] == "False")
                            {
                                falsecount++;
                            }
                        }
                        var emptycount = 0;
                        for (int i = 0; i < tempdt.Rows.Count; i++)
                        {
                            if ((string)dt.Rows[i][3] == string.Empty)
                            {
                                emptycount++;
                            }
                        }

                        if (tempdt.Rows.Count != (falsecount + emptycount))
                        {
                            for (int i = 0; i < tempdt.Rows.Count; i++)
                            {
                                dt.Rows[i][3] = true;
                                checkflag = true;

                            }
                            gridheadercheckboxall.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                            checkflag = true;
                        }
                    }
                    else
                    {
                        var falsecount = 0;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if ((string)dt.Rows[i][3] == "False")
                            {
                                falsecount++;
                            }
                        }
                        var emptycount = 0;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if ((string)dt.Rows[i][3] == string.Empty)
                            {
                                emptycount++;
                            }
                        }

                        if (dt.Rows.Count != (falsecount + emptycount))
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {

                                dt.Rows[i][3] = true;
                                checkflag = true;

                            }
                            gridheadercheckboxall.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                            checkflag = true;
                        }
                    }

                }
                else
                {
                    if (Combo_Category.SelectedIndex == 0 || Combo_Category.SelectedIndex == 1)
                    {
                        if (checkflag)
                        {
                            for (int i = 0; i < tempdt.Rows.Count; i++)
                            {
                                dt.Rows[i][3] = false;

                            }
                            gridheadercheckboxall.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                        }
                        checkflag = true;
                    }
                    else
                    {
                        if (checkflag)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                dt.Rows[i][3] = false;
                            }
                            gridheadercheckboxall.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                        }
                        checkflag = true;
                    }
                    var emtycount = dt
                 .AsEnumerable()

                 .Where(p => p.Field<string>("cMean") == string.Empty)
                 .Count();
                    var failcount = dt
                .AsEnumerable()

                .Where(p => p.Field<string>("cMean") == "False")
                .Count();
                    if (dt.Rows.Count == emtycount + failcount)
                    {
                        if (gridheadercheckboxall.IsChecked == false)
                        {
                            checkflag = true;
                        }
                        else
                        {
                            checkflag = false;
                        }
                        gridheadercheckboxall.IsChecked = false;
                        CheckMACN.IsEnabled = false;
                        data_grid.Columns[2].IsReadOnly = false;

                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

        }

        private void cellend(object sender, DataGridCellEditEndingEventArgs e)
        {
            cell = sender as System.Windows.Controls.DataGridCell;
            iseditablestate = true;
            try
            {


                System.Windows.Controls.TextBox a = e.EditingElement as System.Windows.Controls.TextBox;
                if (a != null)
                {
                    string aa = a.Text;
                    string sub = string.Empty;
                    string temp = string.Empty;
                    if (e.Column.DisplayIndex == 2)
                    {

                        for (int i = 0; i < aa.Length; i++)
                        {
                            sub = sub + aa[i];
                            if (sub == ".")
                                sub = "0.";
                            if (new Regex(@"^[-+]?\d+(\.\d+)?$").IsMatch(sub))
                            {
                                temp = sub;
                            }
                        }
                        aa = temp;
                        aa = aa.Normalize(NormalizationForm.FormKC);
                        aa = aa.TrimStart(new Char[] { '0' });
                        if (aa.Length == 0 && sub.Length >= 1)
                            aa = "0";
                    }

                    if (aa.Length >= 1)
                    {
                        if (e.Column.DisplayIndex == 2)
                        {
                            if (aa.StartsWith("."))
                            {
                                aa = "0" + aa;
                            }
                            if (!new Regex(@"^[-+]?\d+(\.\d+)?$").IsMatch(aa))
                            {
                                dt.Rows[e.Row.GetIndex()][e.Column.DisplayIndex] = "0";
                            }
                            else
                            {
                                dt.Rows[e.Row.GetIndex()][e.Column.DisplayIndex] = aa;
                            }
                        }

                    }
                    if (e.Column.DisplayIndex == 1 && (Combo_Category.SelectedIndex == 0 || Combo_Category.SelectedIndex == 1 || Combo_Category.SelectedIndex > 1))
                    {

                        if (tempdt.Rows.Count < 1)
                        {
                            gridheadercheckboxall.IsEnabled = true;
                            for (int i = 0; i <= data_grid.SelectedIndex; i++)
                            {
                                DataRow dr;
                                dr = tempdt.NewRow();
                                dr["Data"] = String.Empty;
                                tempdt.Rows.Add(dr);


                            }
                        }
                        else
                        {
                            if (tempdt.Rows.Count < data_grid.SelectedIndex + 1)
                            {

                                for (int i = tempdt.Rows.Count; i < data_grid.SelectedIndex + 1; i++)
                                {

                                    DataRow dr;
                                    dr = tempdt.NewRow();
                                    dr["Data"] = String.Empty;
                                    tempdt.Rows.Add(dr);
                                }
                            }
                        }
                        if (aa.Length < 1)
                        {
                            var emptycount = dt
             .AsEnumerable()

             .Where(p => p.Field<string>("Choice") == string.Empty)
             .Count();
                            if (emptycount == dt.Rows.Count)
                            {
                                tempdt.Rows.Clear();
                            }
                            if (data_grid.SelectedIndex == tempdt.Rows.Count - 1)
                            {
                                tempdt.Rows.RemoveAt(data_grid.SelectedIndex);
                            }
                            else
                            {
                                tempdt.Rows[data_grid.SelectedIndex][0] = string.Empty;
                            }

                        }
                        else
                        {
                            tempdt.Rows[data_grid.SelectedIndex][0] = aa.TrimStart().TrimEnd();
                            dt.Rows[data_grid.SelectedIndex][1] = aa.TrimStart().TrimEnd();
                        }
                        try
                        {
                            if (dt.Rows[data_grid.SelectedIndex][3].ToString() == "True")
                            {
                                CheckMACN.IsEnabled = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            string message = ex.Message;
                            _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                        }
                        var count = tempdt
              .AsEnumerable()

              .Where(p => p.Field<string>("Data") == string.Empty)
              .Count();
                        if (count == tempdt.Rows.Count)
                        {

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
            iseditablestate = false;
        }

        private void getevnt(object sender, RoutedEventArgs e)
        {
            conutbasecheck();
        }
        private void fail(object sender, RoutedEventArgs e)
        {

            try
            {
                int limit = 0;
                var count = dt
              .AsEnumerable()

              .Where(p => p.Field<string>("Choice") == string.Empty)
              .Count();
                if (Combo_Category.SelectedIndex == 1 || Combo_Category.SelectedIndex == 0)
                {
                    if (tempdt.Rows.Count >= 1)
                    {
                        var tempcount = tempdt
                  .AsEnumerable()

                  .Where(p => p.Field<string>("Data").Length >= 1)
                  .Count();
                        var falsecount = 0;
                        for (int i = 0; i < tempdt.Rows.Count; i++)
                        {
                            if ((string)dt.Rows[i][3] == "False")
                            {
                                falsecount++;
                            }
                        }
                        var emptycount = 0;
                        for (int i = 0; i < tempdt.Rows.Count; i++)
                        {
                            if ((string)dt.Rows[i][3] == string.Empty)
                            {
                                emptycount++;
                            }
                        }
                        var truecount = 0;
                        for (int i = 0; i < tempdt.Rows.Count; i++)
                        {
                            if ((string)dt.Rows[i][3] == "True")
                            {
                                truecount++;
                            }
                        }


                        if ((tempdt.Rows.Count == falsecount + emptycount || tempcount == falsecount) && truecount == 0)
                        {
                            CheckMACN.IsEnabled = false;
                            gridheadercheckboxall.IsEnabled = true;
                            if (gridheadercheckboxall.IsChecked == false)
                            {
                                checkflag = true;
                            }
                            else
                            {
                                checkflag = false;
                            }
                            gridheadercheckboxall.IsChecked = false;
                            data_grid.Columns[2].IsReadOnly = false;
                            gridheadercheckboxall.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                        }

                        else
                        {
                            for (int i = 0; i < tempdt.Rows.Count; i++)
                            {
                                if ((string)tempdt.Rows[i][0] != string.Empty && (string)dt.Rows[i][3] == "False")
                                {
                                    CheckMACN.IsEnabled = true;
                                    gridheadercheckboxall.IsEnabled = true;
                                    if (gridheadercheckboxall.IsChecked == true)
                                    {
                                        checkflag = true;
                                    }
                                    else
                                    {
                                        checkflag = false;
                                    }
                                    gridheadercheckboxall.IsChecked = true;
                                    data_grid.Columns[2].IsReadOnly = true;
                                    gridheadercheckboxall.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE1E1E1"));
                                }
                            }
                        }


                    }

                }
                else
                {
                    if (Combo_Category.SelectedIndex > 2 || Combo_Category.SelectedIndex < 1002)
                    {
                        var tempcount = dt
             .AsEnumerable()

             .Where(p => p.Field<string>("Choice").Length >= 1)
             .Count();
                        var falsecount = dt
             .AsEnumerable()

             .Where(p => p.Field<string>("cMean") == "False")
             .Count();
                        var emptycount = dt
            .AsEnumerable()

            .Where(p => p.Field<string>("cMean") == String.Empty)
            .Count();




                        if (dt.Rows.Count == falsecount + emptycount)
                        {
                            CheckMACN.IsEnabled = false;
                            gridheadercheckboxall.IsEnabled = true;
                            if (gridheadercheckboxall.IsChecked == false)
                            {
                                checkflag = true;
                            }
                            else
                            {
                                checkflag = true;
                            }
                            gridheadercheckboxall.IsChecked = false;
                            data_grid.Columns[2].IsReadOnly = false;
                            gridheadercheckboxall.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                        }
                        else
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (dt.Rows[i][3].ToString() == "False")
                                {
                                    CheckMACN.IsEnabled = true;
                                    gridheadercheckboxall.IsEnabled = true;
                                    if (gridheadercheckboxall.IsChecked == true)
                                    {
                                        checkflag = true;
                                    }
                                    else
                                    {
                                        checkflag = false;
                                    }

                                    gridheadercheckboxall.IsChecked = true;
                                    data_grid.Columns[2].IsReadOnly = true;
                                    gridheadercheckboxall.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE1E1E1"));
                                }
                            }
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
        private void conutbasecheck()
        {
            try
            {

                int limit = 0;
                var count = dt
              .AsEnumerable()

              .Where(p => p.Field<string>("Choice") == string.Empty)
              .Count();
                if (Combo_Category.SelectedIndex == 1 || Combo_Category.SelectedIndex == 0)
                {
                    if (tempdt.Rows.Count >= 1)
                    {
                        var tempcount = tempdt
              .AsEnumerable()

              .Where(p => p.Field<string>("Data").Length >= 1)
              .Count();
                        var truecount = dt
             .AsEnumerable()

             .Where(p => p.Field<string>("cMean") == "True")
             .Count();
                        if (tempcount == truecount)
                        {
                            CheckMACN.IsEnabled = true;
                            gridheadercheckboxall.IsEnabled = true;
                            if (gridheadercheckboxall.IsChecked == true)
                            {
                                checkflag = true;
                            }
                            else
                            {
                                checkflag = true;
                            }
                            gridheadercheckboxall.IsChecked = true;
                            data_grid.Columns[2].IsReadOnly = true;
                            gridheadercheckboxall.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE1E1E1"));
                            gridheadercheckboxall.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                        }
                        else
                        {
                            for (int i = 0; i < tempdt.Rows.Count; i++)
                            {
                                if ((string)dt.Rows[i][3] == "True")
                                {
                                    CheckMACN.IsEnabled = true;
                                    gridheadercheckboxall.IsEnabled = true;
                                    if (gridheadercheckboxall.IsChecked == true)
                                    {
                                        checkflag = true;
                                    }
                                    else
                                    {
                                        checkflag = false;
                                    }

                                    gridheadercheckboxall.IsChecked = true;
                                    data_grid.Columns[2].IsReadOnly = true;
                                    gridheadercheckboxall.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE1E1E1"));
                                }
                            }
                        }


                    }
                }
                else
                {
                    if (Combo_Category.SelectedIndex > 2 || Combo_Category.SelectedIndex < 1002)
                    {
                        var tempcount = dt
             .AsEnumerable()

             .Where(p => p.Field<string>("Choice").Length >= 1)
             .Count();
                        var truecount = dt
             .AsEnumerable()

             .Where(p => p.Field<string>("cMean") == "True")
             .Count();



                        if (dt.Rows.Count == truecount)
                        {
                            CheckMACN.IsEnabled = true;
                            gridheadercheckboxall.IsEnabled = true;
                            if (gridheadercheckboxall.IsChecked == true)
                            {
                                checkflag = true;
                            }
                            else
                            {
                                checkflag = true;
                            }

                            gridheadercheckboxall.IsChecked = true;
                            data_grid.Columns[2].IsReadOnly = true;
                            gridheadercheckboxall.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                        }
                        else
                        {
                            int j = 0;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if ((string)dt.Rows[i][3] == "True")
                                {
                                    j++;
                                    CheckMACN.IsEnabled = true;
                                    gridheadercheckboxall.IsEnabled = true;
                                    if (gridheadercheckboxall.IsChecked == true)
                                    {
                                        checkflag = true;
                                    }
                                    else
                                    {
                                        checkflag = false;
                                    }
                                    gridheadercheckboxall.IsChecked = true;
                                    data_grid.Columns[2].IsReadOnly = true;
                                    gridheadercheckboxall.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE1E1E1"));
                                }
                            }

                        }

                    }
                }
                if (data_grid.IsEnabled == false)
                {
                    CheckMACN.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

        }

        private void begincelledit(object sender, DataGridPreparingCellForEditEventArgs e)
        {

        }

        private void Combo_Category_PreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                System.Windows.Controls.ComboBox combo = (System.Windows.Controls.ComboBox)sender;
                string cmbotxt = combo.Text;
                cmbotxt.TrimStart(new Char[] { '0' });
                int result = 0;
                if (int.TryParse(cmbotxt, out result))
                {
                    if ((result > 1001 || result < 0))
                    {
                        MessageDialog.ErrorOk(LocalResource.ADDQS_ALERT_MSG_COMBO_CATEGORY);

                        combo.SelectedIndex = 1001;
                    }
                    else
                    {
                        if (result == 0)
                        {
                            MessageDialog.ErrorOk(LocalResource.ADDQS_ALERT_MSG_COMBO_CATEGORY);

                            combo.SelectedIndex = 2;
                        }
                        else
                        {
                            combo.SelectedIndex = result + 1;
                        }
                    }

                }
                else
                {
                    if (!(cmbotxt == LocalResource.LBL_AUTO))
                    {
                        MessageDialog.ErrorOk(LocalResource.ADDQS_ALERT_MSG_COMBO_CATEGORY);
                        combo.SelectedIndex = 1001;
                    }

                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void Combo_sort_PreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                System.Windows.Controls.ComboBox combo = (System.Windows.Controls.ComboBox)sender;

                int result = 0;
                if (int.TryParse(combo.Text, out result))
                {
                    int max;
                    if (Combo_Category.SelectedIndex == 0 || Combo_Category.SelectedIndex == 1)
                    {
                        max = 1000;
                    }
                    else
                    {
                        max = Combo_Category.SelectedIndex - 1;
                    }
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
        
        private void HandleCopyPaste(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                var ue = e.OriginalSource as FrameworkElement;
                int datagridColumn = data_grid.CurrentCell.Column.DisplayIndex;
                System.Windows.Controls.DataGridCell cell = frmutil.GetCell(data_grid, data_grid.SelectedIndex, datagridColumn);
                bool _altModifierPressed = (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl));
                if (_altModifierPressed && Keyboard.IsKeyDown(Key.V))
                {
                    try
                    {
                        QC4Common.Common.CommonFunctions comFunc = new QC4Common.Common.CommonFunctions();
                        tempClipBoardData = System.Windows.Clipboard.GetText();
                        Clipboard.SetText(comFunc.RemoveFullWidth(Clipboard.GetText(),datagridColumn));
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
                                int datagridRow = data_grid.SelectedIndex;
                                if (data_grid.CurrentCell.Column.DisplayIndex == 1)
                                {

                                    if (No_Column > 2 || No_Row > (dt.Rows.Count - data_grid.SelectedIndex))
                                    {
                                        MessageDialog.ErrorOk(LocalResource.COPY_ERROR_MSG);
                                        e.Handled = true;
                                    }
                                    else
                                    {
                                        int RowIndex = data_grid.SelectedIndex;
                                        for (int i = 0; i < No_Row; i++, RowIndex++)
                                        {
                                            for (int j = 1; j <= No_Column; j++)
                                            {
                                                DataRow dr;
                                                if (j == 1)
                                                {
                                                    if (tempdt.Rows.Count < 1)
                                                    {
                                                        for (int rowadditertion = 0; rowadditertion <= RowIndex; rowadditertion++)
                                                        {
                                                            dr = tempdt.NewRow();
                                                            dr["Data"] = String.Empty;
                                                            tempdt.Rows.Add(dr);
                                                        }
                                                    }
                                                    else if (tempdt.Rows.Count <= dt.Rows.Count)
                                                    {
                                                        if (tempdt.Rows.Count == RowIndex)
                                                        {
                                                            dr = tempdt.NewRow();
                                                            dr["Data"] = String.Empty;
                                                            tempdt.Rows.Add(dr);
                                                        }
                                                        else if (tempdt.Rows.Count < RowIndex)
                                                        {
                                                            for (int rowadditertion = tempdt.Rows.Count; rowadditertion <= RowIndex; rowadditertion++)
                                                            {
                                                                dr = tempdt.NewRow();
                                                                dr["Data"] = String.Empty;
                                                                tempdt.Rows.Add(dr);
                                                            }
                                                        }
                                                    }
                                                    if (data[i, (j - 1)] == null)
                                                    {

                                                        dt.Rows[RowIndex][j] = tempdt.Rows[RowIndex][0] = String.Empty;
                                                    }
                                                    else
                                                    {
                                                        dt.Rows[RowIndex][j] = tempdt.Rows[RowIndex][0] = data[i, (j - 1)].ToString();
                                                    }
                                                }
                                                else
                                                {
                                                    if (j == 2 && data_grid.Columns[2].IsReadOnly == false)
                                                    {
                                                        if (data[i, (j - 1)] == null)
                                                        {
                                                            dt.Rows[RowIndex][j] = String.Empty;
                                                        }
                                                        else
                                                        {
                                                            dt.Rows[RowIndex][j] = comFunc.DataString(data[i, (j - 1)].ToString());
                                                        }
                                                    }

                                                }
                                            }
                                        }
                                    }
                                }
                                else if (data_grid.CurrentCell.Column.DisplayIndex == 2)
                                {

                                    if (No_Column > 1 || No_Row > (dt.Rows.Count - data_grid.SelectedIndex))
                                    {
                                        MessageDialog.ErrorOk(LocalResource.COPY_ERROR_MSG);
                                    }
                                    else
                                    {

                                        int RowIndex = data_grid.SelectedIndex;
                                        for (int i = 0; i < No_Row; i++, RowIndex++)
                                        {
                                            if (data_grid.Columns[2].IsReadOnly == false)
                                            {
                                                if (data[i, 0] == null)
                                                {
                                                    dt.Rows[RowIndex][2] = String.Empty;
                                                }
                                                else
                                                {
                                                    dt.Rows[RowIndex][2] = comFunc.DataString(data[i, 0].ToString());
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
                            if (data != null && data[0, 0] != null)
                                System.Windows.Clipboard.SetText(data[0, 0].ToString());
                        }
                        System.Windows.Clipboard.SetText(tempClipBoardData);
                    }
                    catch (Exception ex)
                    {
                        string message = ex.Message;
                        _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                    }
                }
                try
                {
                    if (e.Key == Key.Delete)
                    {
                        int RowIndex = 0;
                        if ((data_grid.SelectedItems != null) && (data_grid.SelectedItems.Count > 0))
                        {
                            for (int i = data_grid.SelectedItems.Count; i > 0; i--)
                            {
                                var presentRow = (System.Data.DataRowView)data_grid.SelectedItems[i - 1];
                                RowIndex = Convert.ToInt32(presentRow.Row.ItemArray[0].ToString());
                                dt.Rows[RowIndex - 1][1] = String.Empty;
                                dt.Rows[RowIndex - 1][2] = String.Empty;
                                if (tempdt.Rows.Count > 0)
                                {

                                    try
                                    {
                                        if ((RowIndex - 1) == tempdt.Rows.Count - 1)
                                        {
                                            tempdt.Rows.RemoveAt(RowIndex - 1);
                                        }
                                        else
                                        {
                                            tempdt.Rows[RowIndex - 1][0] = string.Empty;
                                        }
                                    }
                                    catch { }
                                }
                            }
                            var emptycount = dt
            .AsEnumerable()

            .Where(p => p.Field<string>("Choice") == string.Empty)
            .Count();
                            if (emptycount == dt.Rows.Count)
                            {
                                tempdt.Rows.Clear();
                            }
                            else
                            {
                                if (Combo_Category.SelectedIndex == 0 || Combo_Category.SelectedIndex == 1)
                                {
                                    tempdt.Rows.Clear();
                                    int last = 0;
                                    for (int j = 0; j < 1000; j++)
                                    {
                                        if (dt.Rows[j][1].ToString() != "")
                                        {
                                            last = j;
                                        }
                                    }
                                    for (int i = 0; i <= last; i++)
                                    {
                                        DataRow dr;
                                        dr = tempdt.NewRow();
                                        dr["Data"] = dt.Rows[i][1].ToString();
                                        tempdt.Rows.Add(dr);
                                    }
                                }

                            }
                        }
                    }

                    int curentIndex = 0;
                    if (e.Key == Key.Enter)
                    {
                        curentIndex = data_grid.CurrentCell.Column.DisplayIndex;
                        e.Handled = true;
                        ue.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));

                    }
                    try { 
                    if (data_grid.Columns[3].Visibility == Visibility.Collapsed && data_grid.CurrentCell.Column.DisplayIndex == 1 && curentIndex == 2)
                    {
                        data_grid.CommitEdit();
                        data_grid.SelectedIndex = data_grid.SelectedIndex + 1;
                    }
                    if (data_grid.Columns[3].Visibility == Visibility.Visible && data_grid.CurrentCell.Column.DisplayIndex == 1 && curentIndex == 3)
                    {
                        data_grid.CommitEdit();
                        data_grid.SelectedIndex = data_grid.SelectedIndex + 1;
                    }
                    if ((e.Key == Key.Tab) && data_grid.Columns[2].IsReadOnly == true)
                        {
                            if (data_grid.SelectedIndex >= 14 && data_grid.CurrentCell.Column.DisplayIndex == 3)
                            {
                                if (addqs_btn_save.IsEnabled == true)
                                {
                                    e.Handled = true;
                                    addqs_btn_save.Focus();
                                }
                                else
                                {
                                    e.Handled = true;
                                    Close_btn.Focus();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string message = ex.Message;
                        _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                    }
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                    _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                }
            }
            catch { }
        }

        private void Handle_save_btn(object sender, System.Windows.Input.KeyEventArgs e)
        {
            System.Windows.Controls.TextBox txtbox = (System.Windows.Controls.TextBox)sender;
            if (txtbox.Text.Length >= 1)
            {
                if (Combo_Answer_Type.SelectedIndex >= 0)
                {
                    addqs_btn_save.IsEnabled = true;
                }
            }
            else
            {
                addqs_btn_save.IsEnabled = false;
            }
        }


        private void Checkbox_all_load(object sender, RoutedEventArgs e)
        {
            try
            {
                gridheadercheckboxall = (System.Windows.Controls.CheckBox)sender;
                if (back_next_btn)
                {
                    conutbasecheck();
                    if (data_grid.IsEnabled == false)
                    {
                        CheckMACN.IsEnabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }

        private void label_countmean(object sender, RoutedEventArgs e)
        {
            try
            {
                labelcheckboxall = (System.Windows.Controls.Label)sender;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }
        private String GetCommaSeperated(string value, string quesvar)
        {
            string commaseperatedvalues = string.Empty;
            bool isnot = false;
            if (value.StartsWith("!") || value.StartsWith("<>"))
            {
                isnot = true;
            }
            if (value.StartsWith("!")) value = value.TrimStart('!');

            List<string> commasep = new List<string>();
            List<string> barsep = new List<string>();
            List<string> minsep = new List<string>();
            List<int> exclidelist = new List<int>();

            string[] criteriacommavalues = value.Split(',');
            foreach (string str in criteriacommavalues)
            {
                commasep.Add(str);
            }

            foreach (string str in commasep)
            {
                if (str.Contains('/'))
                {
                    string[] criteriabarvalues = str.Split('/');
                    foreach (string s in criteriabarvalues)
                    {
                        barsep.Add(s);
                    }
                }
                else
                    barsep.Add(str);
            }

            foreach (string str in barsep)
            {
                if (isnot)
                {
                    string notvalue = str;

                    if (str.StartsWith("!")) notvalue = str.TrimStart('!');
                    else if (str.StartsWith("<>")) notvalue = str.Replace("<>", "");


                    int criteriaend = Util.Definiotion.VariableDictionary[quesvar].CategoryCount;
                    if (str.Contains('-'))
                    {
                        int strt = 0, end = 0;
                        string[] criterisplitvals = notvalue.Split('-');

                        if (criterisplitvals.Length == 1)
                        {
                            try
                            {
                                strt = Convert.ToInt32(criterisplitvals[0]);
                            }
                            catch (Exception ex) { strt = 1; System.Diagnostics.Debug.WriteLine("StackTrace:{0}", ex.StackTrace); }
                            end = strt;

                        }
                        else
                        {
                            try
                            {
                                strt = Convert.ToInt32(criterisplitvals[0]);
                            }
                            catch (Exception ex) { strt = 1; System.Diagnostics.Debug.WriteLine("StackTrace:{0}", ex.StackTrace); }
                            try
                            {
                                end = Convert.ToInt32(criterisplitvals[1]);
                            }
                            catch (Exception ex)
                            {
                                end = Util.Definiotion.VariableDictionary[quesvar].CategoryCount;
                                System.Diagnostics.Debug.WriteLine("StackTrace:{0}", ex.StackTrace);
                            }
                        }

                        for (int ci = strt; ci <= end; ci++)
                        {
                            exclidelist.Add(ci);
                        }

                    }
                    else
                    {
                        try
                        {
                            exclidelist.Add(Convert.ToInt32(str));
                        }
                        catch (Exception ex)
                        {
                            string message = ex.Message;
                            _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                        }
                    }


                }
                else
                {

                    if (str.Contains('-'))
                    {

                        int start = 0, limit = 0;
                        string[] criteriaminvalues = str.Split('-');

                        {

                            try
                            {

                                if (criteriaminvalues.Length == 1)
                                {
                                    try
                                    {
                                        start = Convert.ToInt32(criteriaminvalues[0]);
                                    }
                                    catch (Exception ex) { start = 1; System.Diagnostics.Debug.WriteLine("StackTrace:{0}", ex.StackTrace); }
                                    limit = start;
                                }
                                else
                                {
                                    try
                                    {
                                        start = Convert.ToInt32(criteriaminvalues[0]);
                                    }
                                    catch (Exception ex) { start = 1; System.Diagnostics.Debug.WriteLine("StackTrace:{0}", ex.StackTrace); }
                                    try
                                    {
                                        limit = Convert.ToInt32(criteriaminvalues[1]);
                                    }
                                    catch (Exception ex)
                                    {
                                        limit = Util.Definiotion.VariableDictionary[quesvar].CategoryCount;
                                        System.Diagnostics.Debug.WriteLine("StackTrace:{0}", ex.StackTrace);
                                    }
                                }
                                if (limit < start)
                                {
                                    int temp = limit;
                                    limit = start;
                                    start = temp;
                                }
                            }
                            catch (Exception ex)
                            {
                                string message = ex.Message;
                                _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                            }

                            for (int ci = start; ci <= limit; ci++)
                            {
                                minsep.Add(ci.ToString());
                            }
                        }
                    }
                    else
                        minsep.Add(str);
                }
            }
            if (isnot)
            {
                for (int ci = 1; ci <= Util.Definiotion.VariableDictionary[quesvar].CategoryCount; ci++)
                {
                    if (!exclidelist.Contains(ci))
                    {
                        if (!string.IsNullOrEmpty(commaseperatedvalues))
                        {
                            commaseperatedvalues += ",";
                        }

                        commaseperatedvalues += ci;
                    }
                }
            }
            else
            {
                foreach (string str in minsep)
                {
                    if (!string.IsNullOrEmpty(commaseperatedvalues))
                    {
                        commaseperatedvalues += ",";
                    }

                    commaseperatedvalues += str;
                }
            }
            return commaseperatedvalues;
        }



        private void btn_Close_edit(object sender, RoutedEventArgs e)
        {

            EditSaveErrorflag = false;
            if (Check_SubTotal.IsChecked == true && subtotal_dt.Rows.Count < 1)
            {
                if (MessageBoxResult.Yes == System.Windows.MessageBox.Show(LocalResource.QS_WARN_SAVE_CHANGE, LocalResource.TITLE_QC, MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                    SaveQuestion();

                    if (!EditSaveErrorflag)
                    {
                        copydt = null;
                        this.Close();
                    }
                    else
                    {
                        EditSaveErrorflag = false;
                    }
                }
                else
                {
                    this.Close();
                }
            }
            else if (Check_any_changes())
            {
                if (MessageBoxResult.Yes == System.Windows.MessageBox.Show(LocalResource.QS_WARN_SAVE_CHANGE, LocalResource.TITLE_QC, MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                    SaveQuestion();

                    if (!EditSaveErrorflag)
                    {
                        copydt = null;
                        this.Close();
                    }
                    else
                    {
                        EditSaveErrorflag = false;
                    }
                }
                else
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }
        private bool Check_any_changes()
        {
            bool value = false;
            if (removesingleForcheck(Text_QuestionA.Text.TrimStart().TrimEnd()) != ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).TableHeading).TrimStart().TrimEnd())
                return true;
            else if (removesingleForcheck(Text_QuestionB.Text.TrimStart().TrimEnd()) != ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).Question).TrimStart().TrimEnd())
                return true;

            if ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).AnswerType == Constants.AnswerType.SA || (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).AnswerType == Constants.AnswerType.MA)
            {
                if (Combo_Category.Text != (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).Choices.Count.ToString())
                    return true;
                else if (Combo_Sort.Text != (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).Sort)
                    return true;
                else if (((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).AddSubTotal == "" && Check_SubTotal.IsChecked == true) && (!checksubsave))
                    return true;
                else if ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).AddSubTotal == "1" && Check_SubTotal.IsChecked == false)
                    return true;
                else if ((bool)CheckMACN.IsChecked && (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).CountBase.Length < 1)
                    return true;

                dt = dt.AsEnumerable().Zip<DataRow, DataRow, DataRow>(copydt.AsEnumerable(), (DataRow modif, DataRow orig) =>
                 {
                     if (!orig.ItemArray.SequenceEqual<object>(modif.ItemArray))
                     {
                         value = true;
                     }
                     return modif;
                 }).CopyToDataTable<DataRow>();


                if (value == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        private void btn_Back_edit(object sender, RoutedEventArgs e)
        {
            if (Check_SubTotal.IsChecked == true && subtotal_dt.Rows.Count < 1)
            {
                if (MessageBoxResult.Yes == System.Windows.MessageBox.Show(LocalResource.QS_WARN_SAVE_CHANGE, LocalResource.TITLE_QC, MessageBoxButton.YesNo, MessageBoxImage.Question))
                {

                    SaveQuestion();

                }
            }
            else if (Check_any_changes())
            {
                if (MessageBoxResult.Yes == System.Windows.MessageBox.Show(LocalResource.QS_WARN_SAVE_CHANGE, LocalResource.TITLE_QC, MessageBoxButton.YesNo, MessageBoxImage.Question))
                {

                    SaveQuestion();

                }
            }
            if (!EditSaveErrorflag && !multiEdit)
            {
                back_next_btn = true;
                matrixvariable_parant = false;
                matrixvariable_parant_child_count = 0;
                matrixvariable_child = false;
                selectedindex = selectedindex - 1;
                mAinWindow.prevoiusselecteditem = selectedindex;
                dt.Rows.Clear();
                tempdt.Rows.Clear();
                checkflag = true;
                if (selectedindex != -1)
                {
                    Disablefields();
                    if (selectedindex == 0)
                    {
                        btn_back.IsEnabled = false;
                    }
                }
                if (selectedindex < Util.Definiotion.VariableDictionary.Count)
                {
                    btn_next.IsEnabled = true;
                }
                checkQuestionType();

                copydt = dt.Copy();
            }
            else if (multiEdit && !EditSaveErrorflag)
            {
                int ind = INdexInt.FindIndex(a => a == selectedindex);
                selectedindex = INdexInt[ind-1];
                if (INdexInt.Any(x => x == selectedindex))
                {
                    int index = INdexInt.FindIndex(a => a == selectedindex);
                    back_next_btn = true;
                    matrixvariable_parant = false;
                    matrixvariable_parant_child_count = 0;
                    matrixvariable_child = false;
                    if (index == 0)
                    {
                        btn_back.Visibility = Visibility.Hidden;

                    }
                    if (index < INdexInt.Count - 1)
                    {
                        btn_next.Visibility = Visibility.Visible;
                    }
                    Disablefields();
                    checkQuestionType();

                    copydt = dt.Copy();
                }
                else
                {
                    btn_back.Visibility = Visibility.Hidden;
                    selectedindex = INdexInt[0];
                }
            }
            else
            {
                EditSaveErrorflag = false;
            }
        }

        private void btn_Next_edit(object sender, RoutedEventArgs e)
        {
            if (Check_SubTotal.IsChecked == true && subtotal_dt.Rows.Count < 1)
            {
                if (MessageBoxResult.Yes == System.Windows.MessageBox.Show(LocalResource.QS_WARN_SAVE_CHANGE, LocalResource.TITLE_QC, MessageBoxButton.YesNo, MessageBoxImage.Question))
                {

                    SaveQuestion();

                }
            }
            else if (Check_any_changes())
            {
                if (MessageBoxResult.Yes == System.Windows.MessageBox.Show(LocalResource.QS_WARN_SAVE_CHANGE, LocalResource.TITLE_QC, MessageBoxButton.YesNo, MessageBoxImage.Question))
                {

                    SaveQuestion();

                }
            }
            if (!EditSaveErrorflag && !multiEdit)
            {
                back_next_btn = true;
                matrixvariable_parant = false;
                matrixvariable_parant_child_count = 0;
                matrixvariable_child = false;
                selectedindex = selectedindex + 1;
                mAinWindow.prevoiusselecteditem = selectedindex;
                if (selectedindex > -1)
                {
                    btn_back.IsEnabled = true;
                }
                if (selectedindex != Util.Definiotion.VariableDictionary.Count)
                {

                    if (selectedindex == Util.Definiotion.VariableDictionary.Count - 1)
                    {
                        btn_next.IsEnabled = false;
                    }
                    Disablefields();
                }
                checkQuestionType();

                copydt = dt.Copy();
            }
            else if (!EditSaveErrorflag && multiEdit)
            {
                int ind = INdexInt.FindIndex(a => a == selectedindex);
              //  selectedindex = INdexInt[ind];
                if (INdexInt.Any(x => x == INdexInt[ind+1]))
                {
                    selectedindex = INdexInt[ind+1];
                    int index = INdexInt.FindIndex(a => a == selectedindex);
                    back_next_btn = true;
                    matrixvariable_parant = false;
                    matrixvariable_parant_child_count = 0;
                    matrixvariable_child = false;
                    if (index == INdexInt.Count - 1)
                    {
                        btn_next.Visibility = Visibility.Hidden;

                    }
                    if (index > 0)
                    {
                        btn_back.Visibility = Visibility.Visible;
                    }
                    Disablefields();
                    checkQuestionType();

                    copydt = dt.Copy();
                }
            }
            else
            {
                EditSaveErrorflag = false;
            }
        }
        /// <summary>
        /// Reset the Question details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_reset_edit(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == System.Windows.MessageBox.Show(LocalResource.QS_EDIT_RESET_CONFIRMATION + "\n" + LocalResource.QS_EDIT_RESET_CONTI, LocalResource.TITLE_QC, MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                if ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).QuestionFlag.ToString() == "Org")
                {
                    ResetOrgVariable();
                }
                else
                {
                    Disablefields();
                }
            }

        }
        public void ResetOrgVariable()
        {
            try
            {
                List<string> dataOrg = new List<string>();
                excel.Worksheet SettingSheet = Util.ExcelUtil.GetWorkSheetByCodeName(excelWorkBook, Qc4Launcher.Util.Constants.SheetCodeName.QuestionSettingB);
                excel.Range dpstart = SettingSheet.Cells[4, 6];
                excel.Range lastcell = ExcelUtil.EndxlUp(SettingSheet.Cells[4, 6]);
                excel.Range rar = SettingSheet.Range[dpstart, lastcell];
                var obj = rar.Value;
                int d = 0;
                foreach (excel.Range cell in rar.Cells)
                {

                    if (cell.Row >= 1 && !string.IsNullOrEmpty(cell.Text))
                    {
                        if (cell.Text == Text_Item_Name.Text)
                        {
                            excel.Range start = SettingSheet.Cells[cell.Row, cell.Column];
                            excel.Range end = ExcelUtil.EndxlRight(start);
                            excel.Range range = SettingSheet.get_Range(start, end);
                            object[,] res = range.Value;
                            if (res[1, 1] != null)
                            {

                                //Text_Item_Name.Text = (Util.Definiotion.VariableDictionary.Values.ToList()).FirstOrDefault(selectedindex).Variable;
                                Combo_Answer_Type.Text = res[1, 2].ToString();
                                // Combo_Category.SelectedItem = res[1,3].ToString();
                                if (res[1, 3] != null)
                                {
                                    Combo_Category.SelectedIndex = (Convert.ToInt32(res[1, 3].ToString())) + 1;
                                }
                                // prevoiuslimit = (Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).CategoryCount;
                                if (res[1, 6] != null)
                                {
                                    Text_QuestionA.Text = res[1, 6].ToString();
                                }
                                else
                                {
                                    Text_QuestionA.Text = string.Empty;
                                }
                                if (res[1, 7] != null)
                                {
                                    Text_QuestionB.Text = res[1, 7].ToString();
                                }
                                else
                                {
                                    Text_QuestionB.Text = string.Empty;
                                }
                                if ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).AnswerType.ToString() == "SA")
                                {
                                    EnablebuttonsLabelsforSA();
                                }
                                else if ((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).AnswerType.ToString() == "MA")
                                {
                                    EnablebuttonsLabelsforMA();
                                }
                                else
                                {
                                    HideButtonsLabels();
                                }
                                //Text_QuestionB.Text = checksinglequete((Util.Definiotion.VariableDictionary.Values.ToList()).ElementAt(selectedindex).Question.ToString());
                                int count;
                                if (res[1, 3] != null)
                                {
                                    List<string> choice = new List<string>();
                                    int inc = 0;
                                    for (inc = 8; inc <= res.GetLength(1); inc++)
                                    {
                                        if (res[1, inc] != null)
                                        {
                                            choice.Add(res[1, inc].ToString());
                                        }
                                    }
                                    count = Convert.ToInt16(res[1, 3]);
                                    dt = FillDataGrid2(count, choice);
                                    data_grid.DataContext = dt;
                                }
                                break;
                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {

            }
        }
        private void TextBox_scoreofmean_TextChanged(object sender, TextChangedEventArgs e)
        {
            System.Windows.Controls.TextBox txtgridtext = sender as System.Windows.Controls.TextBox;
            string input = txtgridtext.Text.Normalize(NormalizationForm.FormKC);
            if (!isimprossedkey)
            {
                e.Handled = new Regex(@"^[-]?([0-9]+(\.[0-9]*)?)?$").IsMatch(input);
                if ((e.Handled) && Vb.Information.IsNumeric(input))
                {
                    if (input.Length > 30)
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        previewsnumber = input;
                    }
                }
                else if (input.Length == 1 && !(e.Handled))
                {
                    input = previewsnumber = "0";
                    e.Handled = true;
                }
                else if (input.Length > 0 && !(e.Handled))
                    e.Handled = false;

                if (e.Handled == false && input.Length > 0) input = previewsnumber.Normalize(NormalizationForm.FormKC);

                try
                {
                    txtgridtext.Text = input.Normalize(NormalizationForm.FormKC);
                    txtgridtext.CaretIndex = input.Length;
                }
                catch { }
            }
        }
        public string checksinglequete(string value)
        {

            return value;
        }
        private string removesingleForcheck(string value)
        {
            if (value.StartsWith("'"))
            {
                value = value.Remove(0, 1);
            }
            return value;
        }

        private void DisableRightMenu(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void data_grid_CopyingRowClipboardContent(object sender, DataGridRowClipboardEventArgs e)
        {
            try
            {
                e.ClipboardRowContent.RemoveAt(0);
                e.ClipboardRowContent.RemoveAt(2);
            }
            catch (Exception ex) { _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException); }
        }
        private void data_grid_PreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
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
                int datagridColumn = data_grid.CurrentCell.Column.DisplayIndex;
                System.Windows.Controls.DataGridCell cell = frmutil.GetCell(data_grid, data_grid.SelectedIndex, datagridColumn);
                if (cell.IsEditing)
                {
                    bool _altModifierPressed1 = (Keyboard.IsKeyUp(Key.LeftCtrl) || Keyboard.IsKeyUp(Key.RightCtrl));
                    if (_altModifierPressed1 && e.Key == Key.V)
                    {
                        if (!string.IsNullOrEmpty(clipboardText))
                        {
                            System.Windows.Clipboard.SetText(tempClipBoardData);
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            isimprossedkey = false;
            string a = e.ImeProcessedKey.ToString();
            if (a != "None")
            {
                isimprossedkey = true;
            }
        }
    }
}
