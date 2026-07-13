using QC4Common.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Shapes;
using Qc4Launcher.Util;
using static FilterSettingsView.FilterSettingsClass;
using log4net;
using System.Reflection;

namespace Qc4Launcher.Forms.Cross_Tabulation
{
    /// <summary>
    /// Interaction logic for TabContent.xaml
    /// </summary>
    public partial class TabContent : UserControl
    {
        private QC4Common.Util.FormUtil formUtil = new QC4Common.Util.FormUtil();
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        QuestionDetails QstnDetails;
        Dictionary<string, QuestionSettings> variableDictionary = new Dictionary<string, QuestionSettings>();
        ObservableCollection<QuestionSettings> DicValues = new ObservableCollection<QuestionSettings>();
        public ObservableCollection<CrossQuestionSetting> _lst_row3_list = new ObservableCollection<CrossQuestionSetting>();
        GraphOptions graphOptions = new GraphOptions();


        public class MyButtonClickEventArgs : EventArgs
        {
            public string sendr { get; set; }
        }
        public delegate void MyButtonClickEventEventHandler(object sender, MyButtonClickEventArgs e);
        public event MyButtonClickEventEventHandler MyButtonClick;
        private void OnClick(object sender, RoutedEventArgs e)
        {
            Btn_qstn_sttng_Click();
            MyButtonClick?.Invoke(this,
                    new MyButtonClickEventArgs() { sendr = ((Button)sender).Name });
        }

        public TabContent()
        {

            InitializeComponent();
            if (!Cross_tab_Settings.VerticalorHorizontal)
            {
                txt_row3.Visibility = Visibility.Hidden;
                txt_column1.Visibility = Visibility.Hidden;
            }
            else
            {
                if (Option_Setting_Summary_Single.IsChecked == true)
                {
                    txt_column.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#D8D4D3"));
                    txt_column.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F4F4F4"));
                }
            }
            Option_Setting_Summary_Double.IsChecked = true;
            //txt_column.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#D8D4D3"));
            //txt_column.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F4F4F4"));
            txt_rd1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));


            Text_Setting_Cross_Hyosoku1.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F4F4F4"));
            Text_Setting_Cross_Hyosoku1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#D8D4D3"));
            Text_Setting_Cross_Hyosoku1.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE1E1E1"));
            Text_Setting_Cross_Hyosoku1.IsReadOnly = true;

            List_Setting_Cross_Hyosoku21.Visibility = Visibility.Hidden;

            Right_Arrow.IsEnabled = false;

            DicValues.Clear();
            QuestionSettings qs;
            btn_qstn_sttng.IsEnabled = false;

            variableDictionary = Util.Definiotion.VariableDictionary;
            if (variableDictionary.Count > 0)
            {
                foreach (KeyValuePair<string, QuestionSettings> item in variableDictionary)
                {
                    qs = variableDictionary[item.Key];
                    DicValues.Add(qs);
                }

            }
            Load_data();

            Text_Setting_Cross_Hyosoku1.DataContext = dataList;
            List_Setting_Cross_Hyosoku2.DataContext = _lst_row2_list;
            List_Setting_Cross_Hyosoku21.DataContext = _lst_row2_list;
            List_Setting_Cross_Hyotou.DataContext = _lst_row3_list;


        }



        Excel.Workbook Workbook;
        private ObservableCollection<CrossQuestionSetting> _dataExport_LBVariablesToExport = new ObservableCollection<CrossQuestionSetting>();
        private void Load_data()
        {



            int count = -1;
            foreach (KeyValuePair<string, QuestionSettings> item in variableDictionary)
            {
                count++;
                QuestionSettings qs = item.Value;
                if (qs.AnswerType != "D" && qs.AnswerType != "FA" && qs.Variable != "" && qs.Variable != "SAMPLEID")
                {
                    if (qs.CategoryCount != 0)
                    {
                        CrossQuestionSetting qsts = new CrossQuestionSetting();
                        QuestionSettings qst = new QuestionSettings();
                        qsts.TableHeading = QC4Common.Classes.Help.RemoveCRLFCharacters(qs.TableHeading);
                        qsts.Variable = QC4Common.Classes.Help.RemoveCRLFCharacters(qs.Variable);
                        qsts.AnswerType = qs.AnswerType + "/" + qs.CategoryCount;
                        qsts.Question = formUtil.EscapeCRLF(qs.Question);
                        qsts.Choices = qs.Choices;
                        qsts.Id = qs.Id;
                        qsts.ItemId = qs.ItemId;
                        qsts.decid = count;
                        qsts.CategoryCount = qs.CategoryCount;
                        _dataExport_LBVariablesToExport.Add(qsts);
                    }
                    else
                    {
                        CrossQuestionSetting qsts = new CrossQuestionSetting();
                        QuestionSettings qst = new QuestionSettings();
                        qsts.TableHeading = QC4Common.Classes.Help.RemoveCRLFCharacters(qs.TableHeading);
                        qsts.Variable = QC4Common.Classes.Help.RemoveCRLFCharacters(qs.Variable);
                        qsts.AnswerType = qs.AnswerType;
                        qsts.Question = formUtil.EscapeCRLF(qs.Question);
                        qsts.Choices = qs.Choices;
                        qsts.Id = qs.Id;
                        qsts.ItemId = qs.ItemId;
                        qsts.CategoryCount = qs.CategoryCount;
                        qsts.decid = count;
                        _dataExport_LBVariablesToExport.Add(qsts);
                    }

                }
            }
            List_Source.ItemsSource = _dataExport_LBVariablesToExport;
        }
        protected void ChangeValue(object sender, EventArgs e)
        {

        }

        private void Txt_source_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchSourceVariable();
            List_Source.UnselectAll();
            if (List_Source.SelectedItems.Count == 0)
            {
                btn_qstn_sttng.IsEnabled = false;
            }


        }
        public void SearchSourceVariable()
        {

            var result = _dataExport_LBVariablesToExport.Where(p => (p.Question.IndexOf(txt_search.Text, StringComparison.OrdinalIgnoreCase) != -1) && (p.Variable.IndexOf(txt_source.Text, StringComparison.OrdinalIgnoreCase) != -1)).ToList();
            List_Source.ItemsSource = result;

        }

        private void Rd_gt_Checked(object sender, RoutedEventArgs e)
        {

        }
        public static List<CrossQuestionSetting> tabdetails = new List<CrossQuestionSetting>();
        private void Btn_qstn_sttng_Click()
        {
            if (List_Source.SelectedItems.Count == 1)
            {

                _dataExport_LBVariablesToExport[List_Source.SelectedIndex].AnswerType = _dataExport_LBVariablesToExport[List_Source.SelectedIndex].AnswerType.Split('/')[0];

                ObservableCollection<CrossQuestionSetting> ddd = new ObservableCollection<CrossQuestionSetting>();
                foreach (CrossQuestionSetting item in List_Source.SelectedItems)
                {
                    ddd.Add(item);
                }

                var selectedFiles = ddd.Cast<CrossQuestionSetting>().ToList();
                QstnDetails = new QuestionDetails(this, selectedFiles);
                QstnDetails.Owner = Window.GetWindow(this);
                QstnDetails.ShowDialog();

            }
            else if (List_Source.SelectedItems.Count > 1)
            {
                MessageBox.Show(LocalResource.CROSS_MESSAGE_BOX_MULTI, "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public List<CrossQuestionSetting> dataList = new List<CrossQuestionSetting>();
        static string Answertype = "";
        static string ChoiceCount = "";
        private void Right_Arrow_Click(object sender, RoutedEventArgs e)
        {
            if (List_Source.SelectedItems.Count > 1)
            {
                MessageBox.Show(LocalResource.CROSS_MESSAGE_BOX_MULTI, "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (List_Source.SelectedItems.Count == 1)
            {

                foreach (CrossQuestionSetting item in List_Source.SelectedItems)
                {
                    if (item.AnswerType == "N")
                    {
                        MessageBox.Show(LocalResource.CROSS_MESSAGE_BOX_N, "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        dataList = null;
                        dataList = new List<CrossQuestionSetting>();
                        Text_Setting_Cross_Hyosoku1.Text = "";
                        //dataList.Add(item);
                        Answertype = item.AnswerType.Split('/')[0];
                        ChoiceCount = item.CategoryCount.ToString();
                        Text_Setting_Cross_Hyosoku1.Text = item.Variable;
                    }
                }

                //  Text_Setting_Cross_Hyosoku1.Text = dataList;


            }
        }
        public ObservableCollection<CrossQuestionSetting> _lst_row2_list = new ObservableCollection<CrossQuestionSetting>();
        ObservableCollection<CrossQuestionSetting> _lst_row2_remove_list = new ObservableCollection<CrossQuestionSetting>();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<CrossQuestionSetting> List2way = new List<CrossQuestionSetting>();
            List<CrossQuestionSetting> mulList2way = new List<CrossQuestionSetting>();
            List<CrossQuestionSetting> allSelectedVariable = new List<CrossQuestionSetting>();
            List<CrossQuestionSetting> ntype = new List<CrossQuestionSetting>();
            if (List_Source.SelectedItems.Count > 0)
            {
                if (List_Setting_Cross_Hyosoku2.Items.Count > 0)
                {
                    foreach (CrossQuestionSetting item in List_Setting_Cross_Hyosoku2.Items)
                    {
                        allSelectedVariable.Add(item);
                    }

                }
                if (List_Source.SelectedItems.Count == 1)
                {
                    foreach (CrossQuestionSetting item in List_Source.SelectedItems)
                    {
                        if (item.AnswerType == "N")
                        {
                            MessageBox.Show(LocalResource.CROSS_MESSAGE_BOX_N, "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            List2way.Add(item);
                        }
                    }
                }
                else
                {
                    foreach (CrossQuestionSetting item in List_Source.SelectedItems)
                    {
                        if (item.AnswerType == "N")
                        {
                            ntype.Add(item);
                        }
                        else
                        {
                            mulList2way.Add(item);
                        }
                    }

                    if (ntype.Count > 0)
                    {
                        MessageBox.Show(LocalResource.CROSS_MESSAGE_BOX_REMOVED, "QuickCross", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                }

            }
            IEnumerable<CrossQuestionSetting> selectedVariables = mulList2way.Cast<CrossQuestionSetting>().OrderBy(x => x.decid);
            allSelectedVariable.AddRange(selectedVariables);
            allSelectedVariable.AddRange(List2way);
            //var lst_eliminate_Duplicate = allSelectedVariable.Select(x=>x.Variable).Distinct().ToList();
            var lst_eliminate_Duplicate = allSelectedVariable.GroupBy(x => x.Variable).Select(y => y.First());
            // var lst_eliminate_Duplicate = allSelectedVariable.DistinctBy(i => i.v).DistinctBy(i => i.ProductId).ToList();
            _lst_row2_list = new ObservableCollection<CrossQuestionSetting>(lst_eliminate_Duplicate);
            List_Setting_Cross_Hyosoku2.ItemsSource = _lst_row2_list;
            List_Setting_Cross_Hyosoku21.ItemsSource = _lst_row2_list;
            loaditemdata(_lst_row2_list);

        }

        private void Lbl_Single_Left_Arrow_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<CrossQuestionSetting> _lst_selected = new ObservableCollection<CrossQuestionSetting>();
            if (List_Setting_Cross_Hyosoku2.SelectedItems.Count > 0)
            {
                foreach (CrossQuestionSetting item in List_Setting_Cross_Hyosoku2.SelectedItems)
                {
                    _lst_selected.Add(item);
                }
                if (_lst_selected.Count > 0)
                {
                    if (_lst_selected.Count == List_Setting_Cross_Hyosoku2.Items.Count)
                    {
                        _lst_row2_list.Clear();

                    }
                    else
                    {
                        foreach (CrossQuestionSetting item in _lst_selected)
                        {
                            if (item != null)
                            {
                                _lst_row2_list.Remove(item);

                            }
                        }
                    }

                }


            }
            loaditemdata(_lst_row2_list);
        }
        public void update()
        {
            crosdd = TabBinding.tabItems;
            foreach (var item in crosdd)
            {
                if (this.Name == item.gpDesign.Name)
                {
                    if (Option_Setting_Summary_Single.IsChecked == true)
                    {
                        item.rdgt = true;
                    }
                    else
                    {
                        item.rdgt = false;
                    }

                }

            }
        }
        /// <summary>
        /// Method to change the Visibility property of the items based on Vertical settings
        /// </summary>
        public void Vertical()
        {
            bool vertON = TabBinding.tabItems.Count > 0 ? TabBinding.tabItems[0].VerticalOnOrOFF : false;//Redmine Id:228799
            
            foreach (var item in TabBinding.tabItems)
            {
                if (vertON)
                {
                    //grid_data.Height = 310;
                    //border1.Height = 112;
                    txt_row1.Visibility = Visibility.Hidden;
                    txt_row2.Visibility = Visibility.Hidden;
                    txt_row3.Visibility = Visibility.Visible;
                    txt_column1.Visibility = Visibility.Visible;
                    txt_column1.Text = LocalResource.CROSS_AXIS_1;
                    txt_column.Text = LocalResource.CROSS_AXIS_2;
                    txt_rd3.Text = LocalResource.CROSS_TXTBX_TOT;

                    if (Option_Setting_Summary_Single.IsChecked == true)
                    {
                        rdgtcheck();
                    }
                    else { rdgtcheck(); }
                    if (Option_Setting_Summary_Double.IsChecked == true) { Twowaycheck(); }
                    else { Twowaycheck(); }
                    if (Option_Setting_Summary_Triple.IsChecked == true) { ThreeWaycheck(); }
                    else { ThreeWaycheck(); }
                }
                else
                {
                    //grid_data.Height = 310;
                    // border1.Height = 100;
                    txt_row1.Visibility = Visibility.Visible;
                    txt_row2.Visibility = Visibility.Visible;
                    txt_row3.Visibility = Visibility.Hidden;
                    txt_column1.Visibility = Visibility.Hidden;
                    txt_column.Text = LocalResource.CROSS_TXTBX_TOT;
                    if (Option_Setting_Summary_Single.IsChecked == true)
                    {
                        rdgtcheck();
                    }
                    else
                    {
                        rdgtcheck();
                    }
                    if (Option_Setting_Summary_Double.IsChecked == true)
                    {
                        Twowaycheck();

                    }
                    else
                    {
                        Twowaycheck();
                    }
                    if (Option_Setting_Summary_Triple.IsChecked == true)
                    {
                        ThreeWaycheck();
                    }
                    else
                    {
                        ThreeWaycheck();
                    }
                }
            }

        }
        public void loaditemdata(ObservableCollection<CrossQuestionSetting> listdata)
        {
            ObservableCollection<CrossQuestionSetting> list_choices = new ObservableCollection<CrossQuestionSetting>();
            list_choices.Clear();
            int count = 0;
            foreach (var item in listdata)
            {
                int choiceid = 0;
                foreach (var choices in item.Choices)
                {
                    choiceid++;
                    count++;
                    CrossQuestionSetting qs = new CrossQuestionSetting();
                    qs.decid = count;
                    qs.decid1 = count;
                    qs.Choice = choices;
                    qs.AnswerType = item.AnswerType;
                    qs.Variable = item.Variable;
                    qs.Choiceid = choiceid;
                    list_choices.Add(qs);
                }
            }



            crosdd = TabBinding.tabItems;
            foreach (var item in crosdd)
            {
                if (this.Name == item.gpDesign.Name)
                {
                    item.lst.Clear();
                    item.lst = list_choices;
                }

            }
        }

        public ObservableCollection<VMTabItems> crosdd = new ObservableCollection<VMTabItems>();
        List<CrossQuestionSetting> sortedVariablesList3 = new List<CrossQuestionSetting>();
        private void Btn_Right_Single_Arrow_Click(object sender, RoutedEventArgs e)
        {
            List<CrossQuestionSetting> SelectedList = new List<CrossQuestionSetting>();
            List<CrossQuestionSetting> SelectedListinRow2 = new List<CrossQuestionSetting>();
            List<CrossQuestionSetting> sortedVariables = new List<CrossQuestionSetting>();
            try
            {
                if (List_Source.SelectedItems.Count > 0)
                {
                    foreach (CrossQuestionSetting item in List_Setting_Cross_Hyotou.Items)
                    {
                        if (item != null)
                        {
                            SelectedListinRow2.Add(item);
                        }
                    }
                    foreach (CrossQuestionSetting item in List_Source.SelectedItems)
                    {
                        SelectedList.Add(item);
                    }


                }
                IEnumerable<CrossQuestionSetting> selectedVariables = SelectedList.Cast<CrossQuestionSetting>().OrderBy(x => x.decid);
                SelectedListinRow2.AddRange(selectedVariables);
                var lst_eliminate_Duplicate = SelectedListinRow2.GroupBy(x => x.Variable).Select(y => y.First());
                _lst_row3_list = new ObservableCollection<CrossQuestionSetting>(lst_eliminate_Duplicate);
                List_Setting_Cross_Hyotou.ItemsSource = _lst_row3_list;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

        }
        private void Btn_left_single_arrow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ObservableCollection<CrossQuestionSetting> _lst_selected = new ObservableCollection<CrossQuestionSetting>();
                if (List_Setting_Cross_Hyotou.SelectedItems.Count > 0)
                {
                    foreach (CrossQuestionSetting item in List_Setting_Cross_Hyotou.SelectedItems)
                    {
                        _lst_selected.Add(item);
                    }
                    if (_lst_selected.Count > 0)
                    {
                        if (_lst_selected.Count == List_Setting_Cross_Hyotou.Items.Count)
                        {
                            _lst_row3_list.Clear();
                            sortedVariablesList3.Clear();
                        }
                        else
                        {
                            foreach (CrossQuestionSetting item in _lst_selected)
                            {
                                if (item != null)
                                {
                                    _lst_row3_list.Remove(item);
                                    sortedVariablesList3.Remove(item);
                                }
                            }
                        }
                    }


                }
                List_Setting_Cross_Hyotou.ItemsSource = _lst_row3_list;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }
        private void Btn_Right_dbl_arrow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _lst_row3_list.Clear();
                if (List_Source.Items.Count > 0)
                {
                    foreach (CrossQuestionSetting items in List_Source.Items)
                    {

                        _lst_row3_list.Add(items);

                    }
                    var lst_eliminate_Duplicate = _lst_row3_list.Distinct().ToList();
                    _lst_row3_list = new ObservableCollection<CrossQuestionSetting>(lst_eliminate_Duplicate);
                    List_Setting_Cross_Hyotou.DataContext = _lst_row3_list;

                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);




            }
        }

        private void Btn_left_dbl_arrow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _lst_row3_list.Clear();
                sortedVariablesList3.Clear();
                List_Setting_Cross_Hyotou.ItemsSource = _lst_row3_list;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }
        }
        /// <summary>
        /// Method to set the Visibility colors based on Horizontal or Vertical settings
        /// </summary>
        public void rdgtcheck()
        {
            if (Cross_tab_Settings.VerticalorHorizontal == false)
            {
                try
                {
                    if (Option_Setting_Summary_Single.IsChecked == true)
                    {
                        txt_row1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#D8D4D3"));
                        txt_row1.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F4F4F4"));
                        txt_row2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#D8D4D3"));
                        txt_row2.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F4F4F4"));
                        txt_column.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                        txt_column.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                        txt_column1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#D8D4D3"));
                        txt_column1.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F4F4F4"));
                        txt_row3.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                        txt_row3.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));

                        txt_rd1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));

                        txt_rd2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                        List_Setting_Cross_Hyosoku21.IsHitTestVisible = false;
                        List_Setting_Cross_Hyosoku21.Visibility = Visibility.Visible;
                        List_Setting_Cross_Hyosoku2.Visibility = Visibility.Hidden;
                        btn_rgt_arrw.IsEnabled = false;
                        Lbl_Single_Left_Arrow.IsEnabled = false;
                        List_Setting_Cross_Hyosoku21.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F4F4F4"));
                        List_Setting_Cross_Hyosoku21.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE1E1E1"));
                        Text_Setting_Cross_Hyosoku1.IsHitTestVisible = false;
                        Text_Setting_Cross_Hyosoku1.IsReadOnly = true;
                        Text_Setting_Cross_Hyosoku1.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F4F4F4"));
                        Text_Setting_Cross_Hyosoku1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#D8D4D3"));
                        Text_Setting_Cross_Hyosoku1.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE1E1E1"));
                        Right_Arrow.IsEnabled = false;


                    }
                }
                catch (Exception ex)
                {
                    _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                }
            }

            else
            {
                if (Option_Setting_Summary_Single.IsChecked == true)
                {
                    txt_row1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#D8D4D3"));
                    txt_row1.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F4F4F4"));
                    txt_row2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                    txt_row2.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                    txt_column.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#D8D4D3"));
                    txt_column.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F4F4F4"));
                    txt_column1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#D8D4D3"));
                    txt_column1.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F4F4F4"));
                    txt_row3.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                    txt_row3.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));

                    txt_rd1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));

                    txt_rd2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                    List_Setting_Cross_Hyosoku21.IsHitTestVisible = false;
                    List_Setting_Cross_Hyosoku21.Visibility = Visibility.Visible;
                    List_Setting_Cross_Hyosoku2.Visibility = Visibility.Hidden;
                    btn_rgt_arrw.IsEnabled = false;
                    Lbl_Single_Left_Arrow.IsEnabled = false;
                    List_Setting_Cross_Hyosoku21.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F4F4F4"));
                    List_Setting_Cross_Hyosoku21.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE1E1E1"));
                    Text_Setting_Cross_Hyosoku1.IsHitTestVisible = false;
                    Text_Setting_Cross_Hyosoku1.IsReadOnly = true;
                    Text_Setting_Cross_Hyosoku1.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F4F4F4"));
                    Text_Setting_Cross_Hyosoku1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#D8D4D3"));
                    Text_Setting_Cross_Hyosoku1.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE1E1E1"));
                    Right_Arrow.IsEnabled = false;


                }
            }
        }
        private void Rd_gt_Checked_1(object sender, RoutedEventArgs e)
        {

            rdgtcheck();

        }
        public void Twowaycheck()
        {

            if (this.Option_Setting_Summary_Double.IsChecked == true)
            {

                txt_row1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#D8D4D3"));
                txt_row2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                //txt_row2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txt_rd1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));

                txt_rd2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txt_rd3.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));

                txt_column.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txt_column.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));

                txt_row1.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F4F4F4"));
                txt_row2.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));

                Text_Setting_Cross_Hyosoku1.IsTabStop = false;
                Text_Setting_Cross_Hyosoku1.IsHitTestVisible = false;
                //txt_row1.IsEnabled = false;
                Text_Setting_Cross_Hyosoku1.IsHitTestVisible = false;
                Text_Setting_Cross_Hyosoku1.IsReadOnly = true;
                Text_Setting_Cross_Hyosoku1.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F4F4F4"));
                Text_Setting_Cross_Hyosoku1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#D8D4D3"));
                Text_Setting_Cross_Hyosoku1.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE1E1E1"));
                txt_rd1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));

                // Text_Setting_Cross_Hyosoku1.IsEnabled = false;
                Right_Arrow.IsEnabled = false;
                btn_rgt_arrw.IsEnabled = true;
                txt_row2.IsEnabled = true;


                List_Setting_Cross_Hyotou.Visibility = Visibility.Visible;
                Btn_Right_dbl_arrow.IsEnabled = true;
                Btn_Right_Single_Arrow.IsEnabled = true;
                Btn_left_single_arrow.IsEnabled = true;
                Btn_left_dbl_arrow.IsEnabled = true;

                List_Setting_Cross_Hyosoku2.IsHitTestVisible = true;
                List_Setting_Cross_Hyosoku2.Visibility = Visibility.Visible;

                List_Setting_Cross_Hyosoku21.Visibility = Visibility.Hidden;

                List_Setting_Cross_Hyosoku2.IsEnabled = true;
                Lbl_Single_Left_Arrow.IsEnabled = true;
                Check_Setting_Cross_Group.IsEnabled = false;

                txt_rd1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
            }
            if (this.Option_Setting_Summary_Double.IsChecked == false)
            {
                if (this.Option_Setting_Summary_Single.IsChecked == true)
                {
                    Check_Setting_Cross_Group.IsEnabled = true;
                    if (Cross_tab_Settings.VerticalorHorizontal == false)
                    {
                        txt_rd1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                        Text_Setting_Cross_Hyosoku1.IsHitTestVisible = true;
                        txt_row1.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F4F4F4"));
                        txt_row2.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F4F4F4"));
                    }

                }

            }

            if (Cross_tab_Settings.VerticalorHorizontal)
            {
                txt_column.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txt_column.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                txt_column1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#D8D4D3"));
                txt_column1.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F4F4F4"));
              
                txt_row3.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txt_row3.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
            }

            update();
        }
        private void Rd_2way_Checked(object sender, RoutedEventArgs e)
        {
            Twowaycheck();

        }
        public void ThreeWaycheck()
        {

            if (Option_Setting_Summary_Triple.IsChecked == true)
            {
                if (Cross_tab_Settings.VerticalorHorizontal)
                {
                    txt_column.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                    txt_column.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                    txt_column1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                    txt_column1.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                    txt_row3.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                    txt_row3.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                }
                txt_row1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txt_row2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txt_row1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txt_rd1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txt_row1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txt_rd2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));

                txt_row1.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                txt_row2.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                txt_row1.IsEnabled = true;
                txt_row2.IsEnabled = true;
                Text_Setting_Cross_Hyosoku1.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                //Text_Setting_Cross_Hyosoku1.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#A6A6A6"));
                Text_Setting_Cross_Hyosoku1.IsHitTestVisible = true;
                Text_Setting_Cross_Hyosoku1.IsEnabled = true;
                Right_Arrow.IsEnabled = true;
                btn_rgt_arrw.IsEnabled = true;
                Lbl_Single_Left_Arrow.IsEnabled = true;

                //List_Setting_Cross_Hyosoku2.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                // List_Setting_Cross_Hyosoku2.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#A6A6A6"));
                List_Setting_Cross_Hyosoku2.IsHitTestVisible = true;
                List_Setting_Cross_Hyosoku2.Visibility = Visibility.Visible;
                List_Setting_Cross_Hyosoku21.Visibility = Visibility.Hidden;

                List_Setting_Cross_Hyosoku2.IsEnabled = true;
                Check_Setting_Cross_Group.IsEnabled = false;
            }
            else
            {
                if (Cross_tab_Settings.VerticalorHorizontal)
                {
                    if (Option_Setting_Summary_Single.IsChecked == true)
                    {
                        txt_column.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#D8D4D3"));
                        txt_column.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F4F4F4"));
                    }
                }
                else
                {
                    txt_rd1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                  //  txt_rd2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                }
            }
            update();
        }
        private void Rd_3way_Checked(object sender, RoutedEventArgs e)
        {
            ThreeWaycheck();
        }

        private void Rd_gt_Click(object sender, RoutedEventArgs e)
        {
            if (Option_Setting_Summary_Single.IsChecked == true)
            {

            }
            if (this.Option_Setting_Summary_Single.IsChecked == true)
            {
                txt_rd1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txt_rd2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txt_row1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txt_row2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                // Text_Setting_Cross_Hyosoku1.Background= (SolidColorBrush)(new BrushConverter().ConvertFrom("#000000"));

                Text_Setting_Cross_Hyosoku1.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F4F4F4"));
                Text_Setting_Cross_Hyosoku1.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DDDDDD"));
                Text_Setting_Cross_Hyosoku1.IsEnabled = true;

                //List_Setting_Cross_Hyosoku2.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F4F4F4"));
                //List_Setting_Cross_Hyosoku2.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DDDDDD"));

                List_Setting_Cross_Hyosoku2.IsHitTestVisible = false;
                List_Setting_Cross_Hyosoku2.Visibility = Visibility.Hidden;
                List_Setting_Cross_Hyosoku21.Visibility = Visibility.Visible;
                Right_Arrow.IsEnabled = false;
                btn_rgt_arrw.IsEnabled = false;
                Lbl_Single_Left_Arrow.IsEnabled = false;
                List_Setting_Cross_Hyosoku2.IsEnabled = false;

            }
        }

        private void List_Source_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (List_Source.SelectedItems.Count > 1)
            {
                btn_qstn_sttng.IsEnabled = false;
            }
            else
            {
                btn_qstn_sttng.IsEnabled = true;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            var result = _dataExport_LBVariablesToExport.Where(p => (p.Question.IndexOf(txt_search.Text, StringComparison.OrdinalIgnoreCase) != -1) && (p.Variable.IndexOf(txt_source.Text, StringComparison.OrdinalIgnoreCase) != -1)).ToList();
            List_Source.ItemsSource = result;
            List_Source.UnselectAll();
            if (List_Source.SelectedItems.Count == 0)
            {
                btn_qstn_sttng.IsEnabled = false;
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
                _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
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
            catch (Exception ex)
            {
                _log.Error(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
            }

        }

        private void ListBoxItem_MouseEnter(object sender, MouseEventArgs e)
        {
            // ListBoxMouseEnter(List_Source, sender, e);
        }
        private void ListBoxItem_MouseEnter1(object sender, MouseEventArgs e)
        {
            // ListBoxMouseEnter(List_Setting_Cross_Hyotou, sender, e);
        }
        private void ListBoxItem_MouseEnter2(object sender, MouseEventArgs e)
        {
            // ListBoxMouseEnter(List_Setting_Cross_Hyosoku2, sender, e);
        }

        private void List_Source_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ListBoxPreviewMouseDown(e);
        }

        private void List_Source_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ListBoxPreviewMouseUp();
        }

        private void List_Source_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            //ListBoxScrollChanged(List_Source, sender, e, 11);
        }

        private void Rd_2way_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.Option_Setting_Summary_Single.IsChecked == true)
            {
                Check_Setting_Cross_Group.IsEnabled = true;
                //txt_rd1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                //txt_rd2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                txt_rd1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txt_rd2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));



                txt_row1.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F4F4F4"));
                txt_row2.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F4F4F4"));

            }
            //if (Option_Setting_Summary_Double.IsChecked == false)
            //{
            //    Option_Setting_Summary_Double.IsChecked = true;
            //}
            update();
        }




        private void Rd_3way_Unchecked(object sender, RoutedEventArgs e)
        {

            if (this.Option_Setting_Summary_Triple.IsChecked == false)
            {


                if (this.Option_Setting_Summary_Single.IsChecked == true)
                {
                    Check_Setting_Cross_Group.IsEnabled = true;
                    txt_column.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#D8D4D3"));
                    txt_column.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F4F4F4"));
                    //txt_rd1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                    //txt_rd2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                    txt_rd1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                    txt_rd2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                    Text_Setting_Cross_Hyosoku1.IsHitTestVisible = false;

                    txt_row1.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F4F4F4"));
                    txt_row2.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F4F4F4"));
                    // txt_row2.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                }
                if (this.Option_Setting_Summary_Single.IsChecked == false)
                {


                    txt_row1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                    txt_row2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                    txt_row1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                    txt_rd1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                    txt_row1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                    txt_rd2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));

                    txt_row1.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                    txt_row2.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                    txt_row1.IsEnabled = true;
                    txt_row2.IsEnabled = true;
                    Text_Setting_Cross_Hyosoku1.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));

                    Text_Setting_Cross_Hyosoku1.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE1E1E1"));

                    Text_Setting_Cross_Hyosoku1.IsEnabled = true;
                    Right_Arrow.IsEnabled = true;
                    btn_rgt_arrw.IsEnabled = true;
                    Lbl_Single_Left_Arrow.IsEnabled = true;

                    //List_Setting_Cross_Hyosoku2.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                    //List_Setting_Cross_Hyosoku2.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#A6A6A6"));
                    List_Setting_Cross_Hyosoku2.IsHitTestVisible = true;
                    List_Setting_Cross_Hyosoku21.Visibility = Visibility.Hidden;
                    List_Setting_Cross_Hyosoku2.Visibility = Visibility.Visible;
                    //
                    List_Setting_Cross_Hyosoku2.IsEnabled = true;
                    Check_Setting_Cross_Group.IsEnabled = false;

                }
            }

            else
            {
                if (Cross_tab_Settings.VerticalorHorizontal)
                {
                    txt_column.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                    txt_column.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                    txt_column1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                    txt_column1.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                    txt_row3.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                    txt_row3.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                }

                Text_Setting_Cross_Hyosoku1.IsHitTestVisible = true;
                Text_Setting_Cross_Hyosoku1.IsTabStop = true;
                Text_Setting_Cross_Hyosoku1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txt_row1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txt_row2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txt_row1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txt_rd1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txt_row1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txt_rd2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txt_rd3.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));

                txt_column.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                txt_column.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));

                txt_row1.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                txt_row2.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                txt_row1.IsEnabled = true;
                txt_row2.IsEnabled = true;
                Text_Setting_Cross_Hyosoku1.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                //Text_Setting_Cross_Hyosoku1.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#A6A6A6"));
                //Text_Setting_Cross_Hyosoku1.IsHitTestVisible = true;
                Text_Setting_Cross_Hyosoku1.IsReadOnly = false;
                Right_Arrow.IsEnabled = true;
                btn_rgt_arrw.IsEnabled = true;
                Lbl_Single_Left_Arrow.IsEnabled = true;

                // List_Setting_Cross_Hyosoku2.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                // List_Setting_Cross_Hyosoku2.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#A6A6A6"));
                List_Setting_Cross_Hyosoku2.IsHitTestVisible = true;
                List_Setting_Cross_Hyosoku21.Visibility = Visibility.Hidden;
                List_Setting_Cross_Hyosoku2.Visibility = Visibility.Visible;

                List_Setting_Cross_Hyosoku2.IsEnabled = true;
                Check_Setting_Cross_Group.IsEnabled = false;

                List_Setting_Cross_Hyotou.Visibility = Visibility.Visible;
                Btn_Right_dbl_arrow.IsEnabled = true;
                Btn_Right_Single_Arrow.IsEnabled = true;
                Btn_left_single_arrow.IsEnabled = true;
                Btn_left_dbl_arrow.IsEnabled = true;

            }

            update();

        }

        private void OnRadioClick(object sender, GraphOptions.MyRadioButtonClickEventArgs e)
        {

        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            // ListBoxMouseEnter(List_Setting_Cross_Hyotou, sender, e);
        }

        private void ListViewItem_MouseEnter_2(object sender, MouseEventArgs e)
        {
            // ListBoxMouseEnter(List_Setting_Cross_Hyosoku2, sender, e);
        }

        private void List_Setting_Cross_Hyosoku2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void List_Setting_Cross_Hyosoku2_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            // ListBoxScrollChanged(List_Setting_Cross_Hyosoku2, sender, e, 11);
        }

        private void List_Setting_Cross_Hyotou_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            //ListBoxScrollChanged(List_Setting_Cross_Hyotou, sender, e, 11);
        }

        private void Text_Setting_Cross_Hyosoku1_TextChanged(object sender, TextChangedEventArgs e)
        {
            //var mySKUs = _dataExport_LBVariablesToExport.Select(l => l.Variable= Text_Setting_Cross_Hyosoku1.Text).ToList();
        }
        System.Windows.Controls.DataGrid ExpGrid = null;
        private void List_Source_CurrentCellChanged(object sender, EventArgs e)
        {
            var dg = (System.Windows.Controls.DataGrid)sender;
            ExpGrid = dg;
            dg.Focus();
        }

        private void List_Source_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                if (Right_Arrow.IsEnabled)
                {
                    Right_Arrow.Focus();
                    e.Handled = true;
                }
                else if (btn_rgt_arrw.IsEnabled)
                {
                    btn_rgt_arrw.Focus();
                    e.Handled = true;
                }
                else if (Btn_Right_dbl_arrow.IsEnabled)
                {
                    Btn_Right_dbl_arrow.Focus();
                    e.Handled = true;
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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            List_Source.UnselectAll();
            List_Setting_Cross_Hyotou.UnselectAll();
            List_Setting_Cross_Hyosoku2.UnselectAll();
            if (List_Source.SelectedItems.Count == 0)
            {
                btn_qstn_sttng.IsEnabled = false;
            }
        }

        private void Txt_search_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void List_Setting_Cross_Hyotou_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            QC4Common.Util.GridCommonFunc.Arrow(sender, e);
        }

        private void List_Setting_Cross_Hyosoku2_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            QC4Common.Util.GridCommonFunc.Arrow(sender, e);
        }

        private void Lbl_Single_Left_Arrow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Key.Tab == e.Key) { List_Setting_Cross_Hyosoku2.Focus(); e.Handled = true; }
        }

        private void Btn_left_dbl_arrow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Key.Tab == e.Key) { List_Setting_Cross_Hyotou.Focus(); e.Handled = true; }
        }
    }
}