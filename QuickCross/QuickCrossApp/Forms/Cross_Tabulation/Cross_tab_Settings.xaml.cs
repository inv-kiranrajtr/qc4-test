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
using System.Windows.Shapes;
using Excel = Microsoft.Office.Interop.Excel;
using Constants = ExcelAddIn.Common.Constants;

namespace Qc4Launcher.Forms.Cross_Tabulation
{
    /// <summary>
    /// Interaction logic for Cross_tab_Settings.xaml
    /// </summary>
    public partial class Cross_tab_Settings : UserControl
    {

        public class MyButtonClickEventArgs : EventArgs
        {
            public string sendr { get; set; }
        }
        public delegate void MyButtonClickEventEventHandler(object sender, MyButtonClickEventArgs e);
        public event MyButtonClickEventEventHandler MyButtonClick;
        private void OnClick(object sender, RoutedEventArgs e)
        {
            MyButtonClick?.Invoke(this,
                    new MyButtonClickEventArgs() { sendr = ((Button)sender).Name });


            


        }

        public bool checkedthecheckbox { get; set; }

        TabBinding tbbinding = new TabBinding();
       public static Excel.Workbook WorkBook = null;

        public Cross_tab_Settings()
        {
            InitializeComponent();
            selectedindex = 0;
        }
        public static void PageFunction()
        {
           
        }
        private void OnclickData(object sender, EditTabulation.MyComboBoxSelectionChangedEventArgs e)
        {
            selectedindex = e.SelectedIndex;
            tab1.SelectedIndex = selectedindex;
        }

        //RadioButton Custom Event Handler
        public class MyRadioButtonClickEventArgs : EventArgs
        {
            public string sendr { get; set; }
            public bool Check { get; set; }
        }
        public static bool VerticalorHorizontal = false;
        public delegate void MyRadioButtonClickEventHandler(object sender, MyRadioButtonClickEventArgs e);
        public event MyRadioButtonClickEventHandler MyRadioButtonClick;
        private void OnRadioClick(object sender, RoutedEventArgs e)
        {
            MyRadioButtonClick?.Invoke(this,
                    new MyRadioButtonClickEventArgs() { sendr = ((RadioButton)sender).Name, Check = (bool)((RadioButton)sender).IsChecked });

            if (TabBinding.tabItems.Count > 0)
            {

                foreach (var item in TabBinding.tabItems)
                {
                    if (Option_Setting_Output_Lateral.IsChecked == true)
                    {
                        VerticalorHorizontal = false;
                        item.VerticalOnOrOFF = false;
                    }
                    else
                    {
                        VerticalorHorizontal = true;
                        item.VerticalOnOrOFF = true;
                    }
                }
            }
            if (CrossTabulationStd.IsInitialized)
                CrossTabulationStd.LayoutCheckedChanged = true;
        }

        private void CheckBoxCheckedOrNotEvent(object sender, RoutedEventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;

        }

        private void CheckBoxunchecked(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBoxChecked(object sender, RoutedEventArgs e)
        {

            
        }
        
        private void Btn_addtabulation_Click(object sender, RoutedEventArgs e)
        {
           // CrossTabulation.isfromcros = false;
           tbbinding.addItem(Option_Setting_Output_Vertical.IsChecked.Value);
            
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
           
        }
        
        private void Edit_tabulation_Click(object sender, RoutedEventArgs e)
        {
            EditTabulation QstnDetails = new EditTabulation(TabBinding.tabItems);

            QstnDetails.MyComboBoxSelectionChanged += new EditTabulation.MyComboBoxSelectionChangedEventHandler(OnclickData);
            QstnDetails.Owner = Window.GetWindow(this);
            QstnDetails.ShowDialog();
        }
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject rootObject) where T : DependencyObject
        {
            if (rootObject != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(rootObject); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(rootObject, i);

                    if (child != null && child is T)
                        yield return (T)child;

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                        yield return childOfChild;
                }
            }
        }

        private void Chkbx_Checked(object sender, RoutedEventArgs e)
        {
            ObservableCollection<VMTabItems> items = new ObservableCollection<VMTabItems>();
            items = TabBinding.tabItems;
            foreach (var it in items)
            {
                if (it.isactive == true)
                {
                    it._Brushes = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                                   it.OnorOff = LocalResource.CELL_ON;
                }
            }
        }

        private void Chkbx_Unchecked(object sender, RoutedEventArgs e)
        {
            ObservableCollection<VMTabItems> items = new ObservableCollection<VMTabItems>();
            items = TabBinding.tabItems;
            foreach(var it in items)
            {
                if (it.isactive == false)
                {
                    it._Brushes = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                    it.OnorOff = LocalResource.CELL_OFF;
                }
            }
            
        }
        public static int selectedindex = 0;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            tab1.SelectedIndex= selectedindex;
            if (TabBinding.tabItems.Count == 0)
            {

                tbbinding.addItem();
            }
            this.DataContext = tbbinding;
        }

        private void Tab1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedindex = tab1.SelectedIndex;
            //TabContent.demo();
        }
    }
}
