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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Qc4Launcher.Forms.UserControls
{
    /// <summary>
    /// Interaction logic for IntegrateSourceVariable.xaml
    /// </summary>
    public partial class IntegrateSourceVariable : UserControl
    {
        public IntegrateSourceVariable()
        {
            InitializeComponent();
            this.Combo_OriginItem_Item.Text = string.Empty;
            this.Combo_OriginItem_Item.SelectedIndex = -1;
        }



        //ComboBox Custom Event Handler
        public class MyComboBoxSelectionChangedEventArgs : EventArgs
        {
            public object MyComboBoxItem { get; set; }
            public string sendr { get; set; }
            public int LastSelected { get; set; }
            public int SelectedIndex { get; set; }
            public string LastSelectedText { get; set; }
        }
        public delegate void MyComboBoxSelectionChangedEventHandler(object sender, MyComboBoxSelectionChangedEventArgs e);
        public event MyComboBoxSelectionChangedEventHandler MyComboBoxSelectionChanged;
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (e.AddedItems.Count > 0)
            {
                MyComboBoxSelectionChanged?.Invoke(this,
                    new MyComboBoxSelectionChangedEventArgs()
                    {
                        sendr = ((ComboBox)sender).Name,
                        MyComboBoxItem = e.AddedItems[0],
                        LastSelected =0,// this.LastSelected,
                        SelectedIndex = ((ComboBox)sender).SelectedIndex,
                        LastSelectedText ="0"// this.LastSelectedText
                    });
            }
        }

        //Button Custom Event Handler

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

        private void Combo_OriginItem_Item_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {

        }

        private void Comboloaded(object sender, RoutedEventArgs e)
        {
            this.Text_OriginItem1_SelectCount.Text = string.Empty;
        }
    }
}
