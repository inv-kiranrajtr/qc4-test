using log4net;
using Qc4Launcher.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Qc4Launcher.Forms.Cross_Tabulation
{
    /// <summary>
    /// Interaction logic for GraphOptions.xaml
    /// </summary>
    public partial class GraphOptions : UserControl
    {
        ObservableCollection<VMTabItems> tabs = new ObservableCollection<VMTabItems>();
        ObservableCollection<VMTabItems> tabsvalues = new ObservableCollection<VMTabItems>();
        public ObservableCollection<VMTabItems> tabItems;
        //ObservableCollection<CrossQuestionSetting> data { get { return _data; } set { _data = value; } }
        ObservableCollection<CrossQuestionSetting> datasetting = new ObservableCollection<CrossQuestionSetting>();
        public ObservableCollection<CrossQuestionSetting> datasetting_copy = new ObservableCollection<CrossQuestionSetting>();
        public ObservableCollection<CrossQuestionSetting> _data = new ObservableCollection<CrossQuestionSetting>();
        ObservableCollection<CrossQuestionSetting> _removeddata = new ObservableCollection<CrossQuestionSetting>();
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public GraphOptions()
        {
            InitializeComponent();
            //List_ReportOption_Show_ItemQuestion.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F4F4F4"));
            //List_ReportOption_Show_ItemQuestion.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DDDDDD"));
            //List_ReportOption_Show_ItemQuestion2.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F4F4F4"));
            //List_ReportOption_Show_ItemQuestion2.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DDDDDD"));


            //List_ReportOption_Show_ItemQuestion.IsHitTestVisible = true;
            //List_ReportOption_Show_ItemQuestion2.IsHitTestVisible = true;
            List_ReportOption_Show_ItemQuestion1.Visibility = Visibility.Hidden;
            List_ReportOption_Show_ItemQuestion21.Visibility = Visibility.Hidden;
            
            Btn_Right_dbl_arrow.IsEnabled = true;
            Btn_Right_Single_Arrow.IsEnabled = true;
            Btn_left_single_arrow.IsEnabled = true;
            Btn_left_dbl_arrow.IsEnabled = true;
            tabs = TabBinding.tabItems;
            this.List_ReportOption_Show_ItemQuestion.ItemsSource = datasetting_copy;

        }
        public class MyRadioButtonClickEventArgs : EventArgs
        {
            public string sendr { get; set; }
            public bool Check { get; set; }
        }
        public delegate void MyRadioButtonClickEventHandler(object sender, MyRadioButtonClickEventArgs e);
        public event MyRadioButtonClickEventHandler MyRadioButtonClick;
        private void OnRadioClick(object sender, RoutedEventArgs e)
        {
            MyRadioButtonClick?.Invoke(this,
                    new MyRadioButtonClickEventArgs() { sendr = ((RadioButton)sender).Name, Check = (bool)((RadioButton)sender).IsChecked });
        }
        public void disables()
        {
            foreach (var item in tabs)
            {


                if (this.Name == item.TabConten.Name)
                {
                    if (item.TabConten.Option_Setting_Summary_Double.IsChecked == false)
                    {
                        this.List_ReportOption_Show_ItemQuestion1.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F4F4F4"));
                        this.List_ReportOption_Show_ItemQuestion1.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DDDDDD"));
                        this.List_ReportOption_Show_ItemQuestion21.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F4F4F4"));
                        this.List_ReportOption_Show_ItemQuestion21.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DDDDDD"));

                        this.List_ReportOption_Show_ItemQuestion1.IsHitTestVisible = false;
                        this.List_ReportOption_Show_ItemQuestion21.IsHitTestVisible = false;

                        List_ReportOption_Show_ItemQuestion21.Visibility = Visibility.Visible;
                        List_ReportOption_Show_ItemQuestion1.Visibility = Visibility.Visible;
                        List_ReportOption_Show_ItemQuestion.Visibility = Visibility.Hidden;
                        List_ReportOption_Show_ItemQuestion2.Visibility = Visibility.Hidden;

                        Btn_Right_dbl_arrow.IsEnabled = false;
                        Btn_Right_Single_Arrow.IsEnabled = false;
                        Btn_left_single_arrow.IsEnabled = false;
                        Btn_left_dbl_arrow.IsEnabled = false;
                    }
                    else
                    {
                        List_ReportOption_Show_ItemQuestion1.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                        List_ReportOption_Show_ItemQuestion1.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#A6A6A6"));

                        List_ReportOption_Show_ItemQuestion21.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                        List_ReportOption_Show_ItemQuestion21.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#A6A6A6"));

                        List_ReportOption_Show_ItemQuestion21.Visibility = Visibility.Hidden;
                        List_ReportOption_Show_ItemQuestion1.Visibility = Visibility.Hidden;
                        List_ReportOption_Show_ItemQuestion.Visibility = Visibility.Visible;
                        List_ReportOption_Show_ItemQuestion2.Visibility = Visibility.Visible;


                        List_ReportOption_Show_ItemQuestion1.IsHitTestVisible = true;
                        List_ReportOption_Show_ItemQuestion21.IsHitTestVisible = true;
                        Btn_Right_dbl_arrow.IsEnabled = true;
                        Btn_Right_Single_Arrow.IsEnabled = true;
                        Btn_left_single_arrow.IsEnabled = true;
                        Btn_left_dbl_arrow.IsEnabled = true;
                    }
                }
            }
        }
        public void getdata()
        {
            this.Dispatcher.Invoke(() =>
            {
                _data.Clear();
                foreach (var item in tabs)
                {


                    if (this.Name == item.TabConten.Name)
                    {
                        foreach (var items in item.lst)
                        {
                            _data.Add(items);
                        }
                    }
                    if (_data.Count == 0)
                    {
                        datasetting_copy.Clear();
                        //   _dataExport_ListBoxCommonCopy.Clear();
                        List_ReportOption_Show_ItemQuestion2.ItemsSource = _dataExport_ListBoxCommonCopy;
                        List_ReportOption_Show_ItemQuestion21.ItemsSource = _dataExport_ListBoxCommonCopy;
                    }
                }



                if (_dataExport_ListBoxCommonCopy.Count > 0)
                {
                    ObservableCollection<CrossQuestionSetting> _newdata = new ObservableCollection<CrossQuestionSetting>();
                    ObservableCollection<CrossQuestionSetting> _newdata2 = new ObservableCollection<CrossQuestionSetting>();

                    // _removeddata.Clear();
                    foreach (CrossQuestionSetting item in _data)
                    {
                        if (_dataExport_ListBoxCommonCopy.Any(x => x.Choice == item.Choice && x.Variable == item.Variable))
                        {
                            _newdata2.Add(item);
                        }

                    }
                    foreach (CrossQuestionSetting item in _data)//leftside
                    {
                        if (!_dataExport_ListBoxCommonCopy.Any(x => x.Choice == item.Choice && x.Variable == item.Variable))//rigtside
                        {
                            _newdata.Add(item);
                        }

                    }
                    datasetting_copy = _newdata;
                    _dataExport_ListBoxCommonCopy = _newdata2;
                }


                else
                {
                    datasetting_copy = _data;
                }
                this.List_ReportOption_Show_ItemQuestion.ItemsSource = datasetting_copy;
                this.List_ReportOption_Show_ItemQuestion1.ItemsSource = datasetting_copy;
                this.List_ReportOption_Show_ItemQuestion2.ItemsSource = _dataExport_ListBoxCommonCopy;
                this.List_ReportOption_Show_ItemQuestion21.ItemsSource = _dataExport_ListBoxCommonCopy;


            });
        }
        public void LoadListData()
        {

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
                        int bigIndx = listBox.Items.IndexOf(items.OrderBy(x => x.decid1).Last());
                        if (lastSelectedIndx > Indx && items.Count() > 1 && bigIndx <= Indx + 1 && !IsFirst)
                        {
                            listBox.SelectedItems.Remove(items.LastOrDefault());
                        }
                        else if (lastSelectedIndx < Indx && items.Count() > 1 && bigIndx >= Indx && !IsFirst && StartIndx >= Indx)
                        {
                            listBox.SelectedItems.Remove(items.OrderBy(x => x.decid1).FirstOrDefault());
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
                        (listBox.Items[i] as CrossQuestionSetting).decid1 = i;
                    List<CrossQuestionSetting> data = listBox.SelectedItems.Cast<CrossQuestionSetting>().OrderBy(x => x.decid1).ToList();
                    int firstIndex = data[0].decid1;
                    int lastIndex = data[data.Count - 1].decid1;
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
                            IEnumerable<CrossQuestionSetting> items = listBox.SelectedItems.Cast<CrossQuestionSetting>().OrderBy(x => x.decid1);

                            listBox.SelectedItems.Add(listBox.Items[showedLastItemIndex + Convert.ToInt32(e.VerticalOffset)]);

                            for (int i = StartIndx; i <= listBox.Items.IndexOf(items.Last()); i++)
                            {
                                if (listBox.Items.IndexOf(items.OrderBy(x => x.decid1).First()) <= StartIndx)
                                    break;
                                listBox.SelectedItems.Add(listBox.Items[i]);
                            }
                            if (listBox.SelectedItems.Cast<CrossQuestionSetting>().OrderBy(x => x.decid1).First().decid1 < StartIndx)
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
                            IEnumerable<CrossQuestionSetting> items = listBox.SelectedItems.Cast<CrossQuestionSetting>().OrderBy(x => x.decid1);
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
                        (listBox.Items[i] as CrossQuestionSetting).decid1 = i;
                    List<CrossQuestionSetting> data = listBox.SelectedItems.Cast<CrossQuestionSetting>().OrderBy(x => x.decid1).ToList();
                    int firstIndex = data[0].decid1;
                    int lastIndex = data[data.Count - 1].decid1;
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

        private void ListBoxItem_MouseEnter(object sender, MouseEventArgs e)
        {
           // ListBoxMouseEnter(List_ReportOption_Show_ItemQuestion, sender, e);
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
            //ListBoxScrollChanged(List_ReportOption_Show_ItemQuestion, sender, e, 11);
        }
        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            //ListBoxMouseEnter(List_ReportOption_Show_ItemQuestion2, sender, e);
        }



        public ObservableCollection<CrossQuestionSetting> _dataExport_ListBoxCommonCopy = new ObservableCollection<CrossQuestionSetting>();
        private void Btn_Right_Single_Arrow_Click(object sender, RoutedEventArgs e)
        {
            //if (List_ReportOption_Show_ItemQuestion.SelectedItems.Count > 0)
            //{
            //    List<CrossQuestionSetting> removeFromList = new List<CrossQuestionSetting>();
            //    foreach (CrossQuestionSetting item in List_ReportOption_Show_ItemQuestion.SelectedItems)
            //    {
            //        if (item != null)
            //        {
            //            removeFromList.Add(item);
            //        }
            //    }
            //    var items = removeFromList.OrderBy(x => x.decid);
            //    foreach (CrossQuestionSetting item in items)
            //    {
            //        if (item != null)
            //        {
            //            _dataExport_ListBoxCommonCopy.Add(item);
            //            datasetting_copy.Remove(item);
            //        }
            //    }
            //    this.List_ReportOption_Show_ItemQuestion.ItemsSource = datasetting_copy;
            //    this.List_ReportOption_Show_ItemQuestion2.ItemsSource = _dataExport_ListBoxCommonCopy;
            //}
            List<CrossQuestionSetting> cpylst = new List<CrossQuestionSetting>();
            List<CrossQuestionSetting> cpylst1 = new List<CrossQuestionSetting>();
            List<CrossQuestionSetting> cpylst3 = new List<CrossQuestionSetting>();
            List<CrossQuestionSetting> cpylst4 = new List<CrossQuestionSetting>(); 
            List<CrossQuestionSetting> cpylst5 = new List<CrossQuestionSetting>(); 
            if (List_ReportOption_Show_ItemQuestion.SelectedItems.Count > 0)
            {

                if (List_ReportOption_Show_ItemQuestion2.Items.Count>0)
                {
                    foreach(CrossQuestionSetting item in List_ReportOption_Show_ItemQuestion2.Items)
                    {
                        cpylst.Add(item);
                    }
                    foreach (CrossQuestionSetting item in List_ReportOption_Show_ItemQuestion.SelectedItems)
                    {
                        cpylst1.Add(item);
                    }
                    IEnumerable<CrossQuestionSetting> selectedVariables = cpylst1.Cast<CrossQuestionSetting>().OrderBy(x => x.decid);
                    foreach (CrossQuestionSetting data in selectedVariables)
                    {
                        cpylst3.Add(data);
                    }
                    cpylst.AddRange(cpylst3);
                    var items = cpylst3.OrderBy(x => x.decid);
                    foreach (CrossQuestionSetting item in items)
                    {
                        datasetting_copy.Remove(item);
                    }
                    _dataExport_ListBoxCommonCopy = new ObservableCollection<CrossQuestionSetting>(cpylst);
                    this.List_ReportOption_Show_ItemQuestion.ItemsSource = datasetting_copy;
                    this.List_ReportOption_Show_ItemQuestion1.ItemsSource = datasetting_copy;
                    this.List_ReportOption_Show_ItemQuestion2.ItemsSource = _dataExport_ListBoxCommonCopy;
                    this.List_ReportOption_Show_ItemQuestion21.ItemsSource = _dataExport_ListBoxCommonCopy;
                }
                else
                {
                   
                    foreach (CrossQuestionSetting item in List_ReportOption_Show_ItemQuestion.SelectedItems)
                    {
                        cpylst1.Add(item);
                    }
                    IEnumerable<CrossQuestionSetting> selectedVariables = cpylst1.Cast<CrossQuestionSetting>().OrderBy(x => x.decid);
                    foreach (CrossQuestionSetting data in selectedVariables)
                    {
                        cpylst3.Add(data);
                    }
                    var items = cpylst3.OrderBy(x => x.decid);
                    foreach (CrossQuestionSetting item in items)
                    {
                        datasetting_copy.Remove(item);
                    }





                    IEnumerable<CrossQuestionSetting> selectedVariables1 = datasetting_copy.Cast<CrossQuestionSetting>().OrderBy(x => x.decid);
                    foreach (CrossQuestionSetting data in selectedVariables1)
                    {
                        cpylst4.Add(data);
                    }
                    datasetting_copy.Clear();
                    datasetting_copy = new ObservableCollection<CrossQuestionSetting>(cpylst4);

                    _dataExport_ListBoxCommonCopy = new ObservableCollection<CrossQuestionSetting>(cpylst3);
                    this.List_ReportOption_Show_ItemQuestion.ItemsSource = datasetting_copy;
                    this.List_ReportOption_Show_ItemQuestion1.ItemsSource = datasetting_copy;
                    this.List_ReportOption_Show_ItemQuestion2.ItemsSource = _dataExport_ListBoxCommonCopy;
                    this.List_ReportOption_Show_ItemQuestion21.ItemsSource = _dataExport_ListBoxCommonCopy;
                }

            //    List<CrossQuestionSetting> removeFromList = new List<CrossQuestionSetting>();
            //    foreach (CrossQuestionSetting item in List_ReportOption_Show_ItemQuestion.SelectedItems)
            //    {
            //        if (item != null)
            //        {
            //            removeFromList.Add(item);
            //        }
            //    }
            //    var items = removeFromList.OrderBy(x => x.decid);
            //    foreach (CrossQuestionSetting item in items)
            //    {
            //        if (item != null)
            //        {
            //            _dataExport_ListBoxCommonCopy.Add(item);
            //            datasetting_copy.Remove(item);
            //        }
            //    }
            //    this.List_ReportOption_Show_ItemQuestion.DataContext = datasetting_copy;
            //    this.List_ReportOption_Show_ItemQuestion2.DataContext = _dataExport_ListBoxCommonCopy;
            }
        


        }

        private void Btn_Right_dbl_arrow_Click(object sender, RoutedEventArgs e)
        {
            //List<CrossQuestionSetting> copylist = new List<CrossQuestionSetting>();
            //List<CrossQuestionSetting> copylist1 = new List<CrossQuestionSetting>();
            //if (List_ReportOption_Show_ItemQuestion.Items.Count > 0)
            //{
            //    if (List_ReportOption_Show_ItemQuestion2.Items.Count > 0)
            //    {
            //        foreach (CrossQuestionSetting item in datasetting_copy)
            //        {
            //            if (item != null)
            //            {
            //                copylist.Add(item);
            //            }
            //        }
            //    }
            //    else
            //    {

            //        _dataExport_ListBoxCommonCopy.Clear();
            //        foreach (CrossQuestionSetting item in datasetting_copy)
            //        {
            //            if (item != null)
            //            {
            //                _dataExport_ListBoxCommonCopy.Add(item);
            //            }
            //        }
            //    }
            //    copylist1 = new List<CrossQuestionSetting>(_dataExport_ListBoxCommonCopy);
            //    copylist1.AddRange(copylist);
            //    _dataExport_ListBoxCommonCopy = new ObservableCollection<CrossQuestionSetting>(copylist1);
            //    this.List_ReportOption_Show_ItemQuestion2.ItemsSource = _dataExport_ListBoxCommonCopy;
            //    datasetting_copy.Clear();
            //    this.List_ReportOption_Show_ItemQuestion.ItemsSource = datasetting_copy;
            // }
            List<CrossQuestionSetting> AllChoices = new List<CrossQuestionSetting>();
            List<CrossQuestionSetting> AllChoicesOrderd = new List<CrossQuestionSetting>();
            foreach (CrossQuestionSetting item in List_ReportOption_Show_ItemQuestion.Items)
            {
                AllChoices.Add(item);
            }
            foreach (CrossQuestionSetting item in List_ReportOption_Show_ItemQuestion2.Items)
            {
                AllChoices.Add(item);
            }
            IEnumerable<CrossQuestionSetting> selectedVariables = AllChoices.Cast<CrossQuestionSetting>().OrderBy(x => x.decid);
            foreach (CrossQuestionSetting data in selectedVariables)
            {
                AllChoicesOrderd.Add(data);
            }

            _dataExport_ListBoxCommonCopy = new ObservableCollection<CrossQuestionSetting>(AllChoicesOrderd);
            this.List_ReportOption_Show_ItemQuestion2.ItemsSource = _dataExport_ListBoxCommonCopy;
            this.List_ReportOption_Show_ItemQuestion21.ItemsSource = _dataExport_ListBoxCommonCopy;
            datasetting_copy.Clear();
            this.List_ReportOption_Show_ItemQuestion.ItemsSource = datasetting_copy;
            this.List_ReportOption_Show_ItemQuestion1.ItemsSource = datasetting_copy;
        }

        private void Btn_left_dbl_arrow_Click(object sender, RoutedEventArgs e)
        {
            if (List_ReportOption_Show_ItemQuestion2.Items.Count <= 0)
            {
                return;
            }
            // _dataExport_ListBoxCommonCopy.Clear();
            //_dataExport_LBVariablesToExport.Clear();
            List<CrossQuestionSetting> removeFromList = new List<CrossQuestionSetting>();
            foreach (CrossQuestionSetting item in _dataExport_ListBoxCommonCopy)//-----------------------191
            {
                if (item != null)
                {
                    datasetting_copy.Add(item);
                }
            }
            // _dataExport_ListBoxCommonCopy.Add(dataFromSheet.First());//-----------------------191
            _dataExport_ListBoxCommonCopy.Clear();
            this.List_ReportOption_Show_ItemQuestion.ItemsSource = datasetting_copy.OrderBy(x => x.decid);
        }

        private void Btn_left_single_arrow_Click(object sender, RoutedEventArgs e)
        {
            if (List_ReportOption_Show_ItemQuestion2.SelectedItems.Count > 0)
            {
                List<CrossQuestionSetting> removeFromList = new List<CrossQuestionSetting>();
                List<CrossQuestionSetting> sortList = new List<CrossQuestionSetting>();
                bool setIndex = false;
                bool alreadySet = false;
                int position = 0;
                foreach (CrossQuestionSetting item in datasetting_copy)
                {
                    if (item != null)
                    {
                        sortList.Add(item);
                    }
                }


                foreach (CrossQuestionSetting item in List_ReportOption_Show_ItemQuestion2.SelectedItems)
                {
                    if (item != null)
                    {
                        removeFromList.Add(item);
                        if (datasetting_copy.Count == 0)
                        {
                            sortList.Insert(0, item);
                        }
                        else if (datasetting_copy.Count < item.decid + 1)
                        {
                            int i = 0;
                            foreach (var items in datasetting_copy)
                            {
                                if (items.decid > item.decid)
                                {
                                    if (!setIndex)
                                    {
                                        sortList.Insert(0, item);
                                        alreadySet = true;
                                        break;
                                    }
                                    else
                                    {
                                        sortList.Insert(position, item);
                                        break;
                                    }
                                }
                                else
                                {
                                    setIndex = true;
                                    position = i + 1;
                                }
                                if (i == datasetting_copy.Count - 1 && !alreadySet)
                                {
                                    sortList.Insert(position, item);
                                }
                                ++i;
                            }
                            setIndex = false;
                            alreadySet = false;
                        }
                        else
                        {
                            sortList.Insert(item.decid, item);
                        }
                    }
                }
                foreach (CrossQuestionSetting item in removeFromList)
                {
                    if (item != null)
                    {
                        _dataExport_ListBoxCommonCopy.Remove(item);
                    }
                }
                datasetting_copy.Clear();
                sortList = sortList.OrderBy(x => x.decid).ToList();
                foreach (CrossQuestionSetting item in sortList)
                {
                    if (item != null)
                    {
                        datasetting_copy.Add(item);
                    }
                }

                this.List_ReportOption_Show_ItemQuestion.DataContext = datasetting_copy;
                this.List_ReportOption_Show_ItemQuestion2.DataContext = _dataExport_ListBoxCommonCopy;
            }
        }

        private void List_Source_ScrollChanged1(object sender, ScrollChangedEventArgs e)
        {

        }

        private void List_ReportOption_Show_ItemQuestion_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            e.Cancel = true;
        }

        private void List_ReportOption_Show_ItemQuestion2_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            e.Cancel = true;
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
            QC4Common.Util.GridCommonFunc.Arrow(sender, e);
        }
        private void b1SetColor(object sender, MouseButtonEventArgs e)
        {
            if (ExpGrid != null)
                ExpGrid.Focus();
        }
    }
}
