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
using System.Windows.Shapes;
using Excel = Microsoft.Office.Interop.Excel;
using Constants = ExcelAddIn.Common.Constants;
using log4net;
using System.Reflection;
using Qc4Launcher.Util;

namespace Qc4Launcher.Forms.Cross_Tabulation
{
    /// <summary>
    /// Interaction logic for EditTabulation.xaml
    /// </summary>
    public partial class EditTabulation : Window
    {
        //  Cross_tab_Settings CrossTab;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        ObservableCollection<VMTabItems> _tabitem = new ObservableCollection<VMTabItems>();
        ObservableCollection<VMTabItems> tabItem { get { return _tabitem; } set { _tabitem = value; } }
        public EditTabulation(ObservableCollection<VMTabItems> tabValues)
        {
            InitializeComponent();
            //  CrossTab = new Cross_tab_Settings();
            _tabitem = tabValues;
            List_Source.DataContext = tabItem;
            Btn_Delete.IsEnabled = false;
            btn_on_off.IsEnabled = false;
            UpArrow.IsEnabled = false;
            DownArrow.IsEnabled = false;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void ListViewItem_PreviewMouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (List_Source.SelectedItems.Count > 0)
            {
                List<int> indexes = new List<int>();
                foreach (object ob in List_Source.SelectedItems)
                {
                    indexes.Add(List_Source.Items.IndexOf(ob));
                }


                foreach (var item in indexes)
                {
                    if (_tabitem[item].isactive == false)
                    {
                        _tabitem[item].isactive = true;
                        _tabitem[item].OnorOff = LocalResource.CELL_ON;

                        _tabitem[item]._Brushes = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));

                    }
                    else
                    {
                        _tabitem[item].isactive = false;
                        _tabitem[item].OnorOff = LocalResource.CELL_OFF;
                        _tabitem[item]._Brushes = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                    }
                }
            }


        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Delete_Click(object sender, RoutedEventArgs e)
        {

            if (List_Source.Items.Count > 1)
            {
                if (List_Source.SelectedItems.Count > 0)
                {
                    MessageBoxResult result;
                    result = MessageBox.Show(LocalResource.CROSS_DELETE_MSG, "QuickCross", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        var selectedFiles = List_Source.SelectedItems.Cast<VMTabItems>().ToList();
                        foreach (VMTabItems item in selectedFiles)
                        {
                            tabItem.Remove(item);
                        }
                    }
                }
            }
            List_Source_SelectionChanged();
        }
        private void UpArrow_Click(object sender, RoutedEventArgs e)
        {
            if (List_Source.SelectedIndex > 0)
            {
                _tabitem.Move(List_Source.SelectedIndex - 1, List_Source.SelectedIndex);
            }
            if (List_Source.SelectedIndex < (List_Source.Items.Count - 1))
            {
                DownArrow.IsEnabled = true;
            }
            if (List_Source.SelectedIndex == 0)
            {
                UpArrow.IsEnabled = false;
            }
            List_Source.ScrollIntoView(List_Source.SelectedItem);
        }
        public class MyComboBoxSelectionChangedEventArgs : EventArgs
        {

            public int SelectedIndex { get; set; }
            
        }
        public delegate void MyComboBoxSelectionChangedEventHandler(object sender, MyComboBoxSelectionChangedEventArgs e);
        public event MyComboBoxSelectionChangedEventHandler MyComboBoxSelectionChanged;
        private void OnSelectionChanged(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (List_Source.SelectedItems.Count > 0)
                {
                    int currentRowIndex = Cross_tab_Settings.selectedindex;
                    if (List_Source.SelectedItems.Count > 1)
                    {
                        currentRowIndex = List_Source.Items.IndexOf(List_Source.CurrentItem);
                    }
                    else
                    {
                        currentRowIndex = List_Source.Items.IndexOf(List_Source.CurrentItem);
                    }

                    List_Source_SelectionChanged();
                    MyComboBoxSelectionChanged?.Invoke(this,
                        new MyComboBoxSelectionChangedEventArgs()
                        {

                        //SelectedIndex = ((DataGrid)sender).SelectedIndex
                        SelectedIndex = currentRowIndex
                        });


                }
            }
            catch (Exception ex) { _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException); }
        }
        private void List_Source_SelectionChanged()
        {
            //int s= List_Source.SelectedIndex;
            // Cross_tab_Settings.selectedindex = s;
            
            int d = List_Source.SelectedIndex;


            if (List_Source.SelectedItems.Count > 0)
            {
                if (List_Source.SelectedItems.Count > 1)
                {
                    Cross_tab_Settings.PageFunction();
                    btn_on_off.IsEnabled = true;
                    UpArrow.IsEnabled = false;

                    DownArrow.IsEnabled = false;
                    if (List_Source.SelectedItems.Count == List_Source.Items.Count)
                    {
                        Btn_Delete.IsEnabled = false;
                    }
                    else
                    {
                        Btn_Delete.IsEnabled = true;
                    }
                }
                else
                {
                    btn_on_off.IsEnabled = true;
                    UpArrow.IsEnabled = true;
                    DownArrow.IsEnabled = true;
                    if (List_Source.SelectedItems.Count != List_Source.Items.Count)
                    {
                        Btn_Delete.IsEnabled = true;
                    }

                }
            }
            else
            {
                Btn_Delete.IsEnabled = false;
                btn_on_off.IsEnabled = false;
                UpArrow.IsEnabled = false;
                DownArrow.IsEnabled = false;
            }
            //if (List_Source.SelectedItems.Count > 1)
            //{
            //    Cross_tab_Settings.PageFunction();
            //    btn_on_off.IsEnabled = true;
            //    UpArrow.IsEnabled = false;

            //    DownArrow.IsEnabled = false;
            //    if (List_Source.SelectedItems.Count == List_Source.Items.Count)
            //    {
            //        Btn_Delete.IsEnabled = false;
            //    }
            //    else
            //    {
            //        Btn_Delete.IsEnabled = true;
            //    }
            //}
            //else
            //{
            //    btn_on_off.IsEnabled = true;
            //    UpArrow.IsEnabled = true;
            //    DownArrow.IsEnabled = true;
            //    if (List_Source.SelectedItems.Count != List_Source.Items.Count)
            //    {
            //        Btn_Delete.IsEnabled = true;
            //    }

            //}
            if (List_Source.SelectedIndex == 0)
            {
                UpArrow.IsEnabled = false;
            }
            if ((List_Source.Items.Count - 1) == List_Source.SelectedIndex)
            {
                DownArrow.IsEnabled = false;
            }

            if (List_Source.Items.Count == 1)
            {
                Btn_Delete.IsEnabled = false;
            }
            

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (List_Source.SelectedIndex < (List_Source.Items.Count - 1))
            {
                _tabitem.Move(List_Source.SelectedIndex + 1, List_Source.SelectedIndex);
            }
            if (List_Source.SelectedIndex > 0)
            {
                UpArrow.IsEnabled = true;
            }
            if (List_Source.SelectedIndex == (List_Source.Items.Count - 1))
            {
                DownArrow.IsEnabled = false;
            }
            List_Source.ScrollIntoView(List_Source.SelectedItem);

        }

        private void List_Source_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

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
                            IEnumerable<VMTabItems> items = listBox.SelectedItems.Cast<VMTabItems>().OrderBy(x => x.Tabid);

                            listBox.SelectedItems.Add(listBox.Items[showedLastItemIndex + Convert.ToInt32(e.VerticalOffset)]);

                            for (int i = StartIndx; i <= listBox.Items.IndexOf(items.Last()); i++)
                            {
                                if (listBox.Items.IndexOf(items.OrderBy(x => x.Tabid).First()) <= StartIndx)
                                    break;
                                listBox.SelectedItems.Add(listBox.Items[i]);
                            }
                            if (listBox.SelectedItems.Cast<VMTabItems>().OrderBy(x => x.Tabid).First().Tabid < StartIndx)
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
                            IEnumerable<VMTabItems> items = listBox.SelectedItems.Cast<VMTabItems>().OrderBy(x => x.Tabid);
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
                        (listBox.Items[i] as VMTabItems).Tabid = i;
                    List<VMTabItems> data = listBox.SelectedItems.Cast<VMTabItems>().OrderBy(x => x.Tabid).ToList();
                    int firstIndex = data[0].Tabid;
                    int lastIndex = data[data.Count - 1].Tabid;
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
                    Indx = listBox.Items.IndexOf((lbi.DataContext as VMTabItems));
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
                        Indx = listBox.Items.IndexOf((lbi.DataContext as VMTabItems));
                        IEnumerable<VMTabItems> items = listBox.SelectedItems.Cast<VMTabItems>();
                        int lastSelectedIndx = listBox.Items.IndexOf(items.LastOrDefault());
                        int bigIndx = listBox.Items.IndexOf(items.OrderBy(x => x.Tabid).Last());
                        if (lastSelectedIndx > Indx && items.Count() > 1 && bigIndx <= Indx + 1 && !IsFirst)
                        {
                            listBox.SelectedItems.Remove(items.LastOrDefault());
                        }
                        else if (lastSelectedIndx < Indx && items.Count() > 1 && bigIndx >= Indx && !IsFirst && StartIndx >= Indx)
                        {
                            listBox.SelectedItems.Remove(items.OrderBy(x => x.Tabid).FirstOrDefault());
                        }
                        listBox.SelectedItems.Add(lbi);
                        lbi.IsSelected = true;
                        IsFirst = false;
                        lbi.Focus();
                    }
                    else  //if-else added
                    {
                        listBox.SelectedItems.Clear();
                        Indx = listBox.Items.IndexOf((lbi.DataContext as VMTabItems));
                        listBox.SelectedItems.Add(lbi);
                        lbi.IsSelected = true;
                        IsFirst = false;
                        lbi.Focus();
                    }
                    for (int i = 0; i < listBox.Items.Count; i++)
                        (listBox.Items[i] as VMTabItems).Tabid = i;
                    List<VMTabItems> data = listBox.SelectedItems.Cast<VMTabItems>().OrderBy(x => x.Tabid).ToList();
                    int firstIndex = data[0].Tabid;
                    int lastIndex = data[data.Count - 1].Tabid;
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


        private void ListBoxItem_MouseEnter(object sender, MouseEventArgs e)
        {
            ////ListBoxMouseEnter(List_Source, sender, e);
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
          //  ListBoxScrollChanged(List_Source, sender, e, 11);
        }

        private void List_Source_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
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

        private void List_Source_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void List_Source_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void List_Source_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            List_Source_SelectionChanged();
        }
    }


}
