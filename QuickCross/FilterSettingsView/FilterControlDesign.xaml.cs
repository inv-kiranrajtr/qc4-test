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
using excel = Microsoft.Office.Interop.Excel;
using System.Collections.ObjectModel;
using QC4Common.Model;
using System.Globalization;

namespace FilterSettingsView
{
    /// <summary>
    /// Interaction logic for FilterControlDesign.xaml
    /// </summary>
    public partial class FilterControlDesign : UserControl
    {
        FilterSettingsClass fsc = new FilterSettingsClass();

        private Dictionary<String, QuestionSettings> PopulatedDictionary = new Dictionary<string, QuestionSettings>();
        // public ObservableCollection<FilterSettingsClass.DataExport> _qstnvariablDD1 = new ObservableCollection<FilterSettingsClass.DataExport>();
        //public ObservableCollection<FilterSettingsClass.DataExport> _qstnvariablDD2 = new ObservableCollection<FilterSettingsClass.DataExport>();
        //public ObservableCollection<FilterSettingsClass.DataExport> _qstnvariablDD3 = new ObservableCollection<FilterSettingsClass.DataExport>();
        //public ObservableCollection<FilterSettingsClass.DataExport> _qstnvariablDD4 = new ObservableCollection<FilterSettingsClass.DataExport>();
        //public ObservableCollection<FilterSettingsClass.DataExport> _qstnvariablDD5 = new ObservableCollection<FilterSettingsClass.DataExport>();
        //public ObservableCollection<FilterSettingsClass.DataExport> _qstnvariablDD6 = new ObservableCollection<FilterSettingsClass.DataExport>();
        public FilterControlDesign()//excel.Workbook workbook, string filePath
        {

            LocalResource.Culture = new CultureInfo(QC4Common.Common.Constants.GlobalMode.Split(',')[0]);
            InitializeComponent();
            check();

        }

        void check()
        {
            if (Check_Refine_Condition.IsChecked == false)
            {
                var bc = new BrushConverter();
                lblCriteriaVariable.Foreground = (Brush)bc.ConvertFrom("#999999");
                lblOperator.Foreground = (Brush)bc.ConvertFrom("#999999");
                lblValue.Foreground = (Brush)bc.ConvertFrom("#999999");
            }
            else
            {

                lblCriteriaVariable.IsEnabled = true;
                lblOperator.IsEnabled = true;
                lblValue.IsEnabled = true;
                var bc = new BrushConverter();
                lblCriteriaVariable.Foreground = (Brush)bc.ConvertFrom("#FF333333");
                lblOperator.Foreground = (Brush)bc.ConvertFrom("#FF333333");
                lblValue.Foreground = (Brush)bc.ConvertFrom("#FF333333");

            }
        }

        private void Check_Refine_Condition_Checked(object sender, RoutedEventArgs e)
        {
            if (Check_Refine_Condition.IsChecked == true)
            {
                Combo_Conditional_Item_1.IsEnabled = true;
            }
            if (Check_Refine_Condition.IsChecked == false)
            {
                //----------Column Heading Enabled ---------------------

                lblCriteriaVariable.IsEnabled = false;
                lblOperator.IsEnabled = false;
                lblValue.IsEnabled = false;

                //-----------------------Row wise Enabled--------------------------------
                Combo_Conditional_Item_1.IsEnabled = false;
                Combo_Conditional_Operator_1.IsEnabled = false;
                Combo_Conditional_Value_1.IsEnabled = false;
                BTnFilter1.IsEnabled = false;
                Option_Conditional_And_1.IsEnabled = false;
                Option_Conditional_Or_1.IsEnabled = false;

                Combo_Conditional_Item_2.IsEnabled = false;
                Combo_Conditional_Operator_2.IsEnabled = false;
                Combo_Conditional_Value_2.IsEnabled = false;
                BTnFilter2.IsEnabled = false;
                Option_Conditional_And_2.IsEnabled = false;
                Option_Conditional_Or_2.IsEnabled = false;

                Combo_Conditional_Item_3.IsEnabled = false;
                Combo_Conditional_Operator_3.IsEnabled = false;
                Combo_Conditional_Value_3.IsEnabled = false;
                BTnFilter3.IsEnabled = false;
                Option_Conditional_And_3.IsEnabled = false;
                Option_Conditional_Or_3.IsEnabled = false;

                Combo_Conditional_Item_4.IsEnabled = false;
                Combo_Conditional_Operator_4.IsEnabled = false;
                Combo_Conditional_Value_4.IsEnabled = false;
                BTnFilter4.IsEnabled = false;
                Option_Conditional_And_4.IsEnabled = false;

                Option_Conditional_Or_4.IsEnabled = false;


                Combo_Conditional_Item_5.IsEnabled = false;
                Combo_Conditional_Operator_5.IsEnabled = false;
                Combo_Conditional_Value_5.IsEnabled = false;
                BTnFilter5.IsEnabled = false;

            }
        }
        private void Combo_Classify_Item_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //// Combo_Classify_Item.IsEnabled = true;
                // int selectedindex = Combo_Classify_Item.SelectedIndex;
                // string NoOfChoice = string.Empty;
                // if (selectedindex >= 0)
                // {

                //     this.TBAnsType.Text =  fsc._qstnvariablDD1[selectedindex].QuestionVariableType.Split('/')[0];
                //     try
                //     {
                //         fsc. _qstnvariablDD1[selectedindex].QuestionChoiceNo = fsc._qstnvariablDD1[selectedindex].QuestionVariableType.Split('/')[1];
                //     }
                //     catch { }
                //     if (fsc._qstnvariablDD1[selectedindex].QuestionChoiceNo != "")
                //         this.TBNoOfChoice.Text = fsc._qstnvariablDD1[selectedindex].QuestionChoiceNo.ToString();
                //     else
                //         this.TBNoOfChoice.Text = "";
                //     this.TAQuestion.Text = fsc._qstnvariablDD1[selectedindex].Question;
                // }
            }
            catch
            { }
        }

        private void Exportpathbtn_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            //dlg.DefaultExt = ".txt";
            dlg.Filter = "All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                Combo_Classify_FolderPath.Text = dlg.FileName;//Text_Output_Path  Combo_Classify_FolderPath
            }
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
                        LastSelected = this.LastSelected,
                        SelectedIndex = ((ComboBox)sender).SelectedIndex,
                        LastSelectedText = this.LastSelectedText
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

        //CheckBox Custom Event Handler
        public class MyCheckBoxClickEventArgs : EventArgs
        {
            public string sendr { get; set; }
            public bool Check { get; set; }
        }
        public delegate void MyCheckBoxClickEventHandler(object sender, MyCheckBoxClickEventArgs e);
        public event MyCheckBoxClickEventHandler MyCheckBoxClick;
        private void OnCheck(object sender, RoutedEventArgs e)
        {
            if (Check_Refine_Condition.IsChecked == false)
            {
                var bc = new BrushConverter();
                lblCriteriaVariable.Foreground = (Brush)bc.ConvertFrom("#999999");
                lblOperator.Foreground = (Brush)bc.ConvertFrom("#999999");
                lblValue.Foreground = (Brush)bc.ConvertFrom("#999999");
            }
            else
            {
                var bc = new BrushConverter();
                lblCriteriaVariable.IsEnabled = true;
                lblOperator.IsEnabled = true;
                lblValue.IsEnabled = true;
                lblCriteriaVariable.Foreground = (Brush)bc.ConvertFrom("#FF333333");
                lblOperator.Foreground = (Brush)bc.ConvertFrom("#FF333333");
                lblValue.Foreground = (Brush)bc.ConvertFrom("#FF333333");

            }
            MyCheckBoxClick?.Invoke(this,
                    new MyCheckBoxClickEventArgs() { sendr = ((CheckBox)sender).Name, Check = (bool)((CheckBox)sender).IsChecked });
        }
        //RadioButton Custom Event Handler
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
        //TextBox Custom Event Handler
        public class MyTextBoxChangeEventArgs : EventArgs
        {
            public string sendr { get; set; }
            public string Text { get; set; }
            
        }
        private static readonly string[] SuggestionValues = {
            "DK"
        };
        public delegate void MyTextBoxChangeEventHandler(object sender, MyTextBoxChangeEventArgs e);
        public event MyTextBoxChangeEventHandler MyTextBoxChange;
        private void OnTextBoxChange(object sender, TextChangedEventArgs e)
        {
            if (Keyboard.IsKeyUp(Key.Back))
            {
                string _currentInput = "";
                string _currentSuggestion = "";
                string _currentText = "";

                int _selectionStart;
                int _selectionLength;
                var sndr = (System.Windows.Controls.TextBox)sender;
                var input = sndr.Text;
               
                if (input.Length > _currentInput.Length && input != _currentSuggestion)
                {
                    _currentSuggestion = SuggestionValues.FirstOrDefault(x => x.StartsWith(input, StringComparison.OrdinalIgnoreCase) != false);
                    if (_currentSuggestion != null)
                    {
                        _currentText = _currentSuggestion;
                        _selectionStart = input.Length;
                        _selectionLength = _currentSuggestion.Length - input.Length;

                        sndr.Text = _currentText;
                        sndr.Select(_selectionStart, _selectionLength);
                    }
                }
                _currentInput = input;
            }
            

            MyTextBoxChange?.Invoke(this,
                    new MyTextBoxChangeEventArgs() { sendr = ((TextBox)sender).Name, Text = (string)((TextBox)sender).Text });
            bool enable = false;
            if (((string)((System.Windows.Controls.TextBox)sender).Text).Length > 0)
            {
                enable = true;
            }

            if (((System.Windows.Controls.TextBox)sender) == Combo_Conditional_Value_1)
            {
                Option_Conditional_And_1.IsEnabled = enable;
                Option_Conditional_Or_1.IsEnabled = enable;
            }
            else if (((System.Windows.Controls.TextBox)sender) == Combo_Conditional_Value_2)
            {
                Option_Conditional_And_2.IsEnabled = enable;
                Option_Conditional_Or_2.IsEnabled = enable;
            }
            else if (((System.Windows.Controls.TextBox)sender) == Combo_Conditional_Value_3)
            {
                Option_Conditional_And_3.IsEnabled = enable;
                Option_Conditional_Or_3.IsEnabled = enable;
            }
            else if (((System.Windows.Controls.TextBox)sender) == Combo_Conditional_Value_4)
            {
                Option_Conditional_And_4.IsEnabled = enable;
                Option_Conditional_Or_4.IsEnabled = enable;
            }
        }

        private void KeyPress(object sender, KeyEventArgs e)
        {
            System.Windows.Controls.ComboBox sen = ((System.Windows.Controls.ComboBox)sender);
            if ((Key.Delete == e.Key || ((sen.SelectedIndex == -1 && sen.Text == "") || (sen.SelectedIndex == -1 && sen.Text != ""))) && (this.Combo_Conditional_Item_1.IsKeyboardFocusWithin || this.Combo_Conditional_Item_1.IsDropDownOpen) &&
                (Option_Conditional_And_1.IsChecked == false && Option_Conditional_Or_1.IsChecked == false))
            {
                LastSelected = 0;
                this.Combo_Conditional_Item_1.SelectedIndex = 0;
            }
            else if ((Key.Delete == e.Key || ((sen.SelectedIndex == -1 && sen.Text == "") || (sen.SelectedIndex == -1 && sen.Text != ""))) && (this.Combo_Conditional_Item_2.IsKeyboardFocusWithin || this.Combo_Conditional_Item_2.IsDropDownOpen) &&
                (Option_Conditional_And_2.IsChecked == false && Option_Conditional_Or_2.IsChecked == false))
            {
                LastSelected = 0;
                this.Combo_Conditional_Item_2.SelectedIndex = 0;
            }
            else if ((Key.Delete == e.Key || ((sen.SelectedIndex == -1 && sen.Text == "") || (sen.SelectedIndex == -1 && sen.Text != ""))) && (this.Combo_Conditional_Item_3.IsKeyboardFocusWithin || this.Combo_Conditional_Item_3.IsDropDownOpen) &&
                (Option_Conditional_And_3.IsChecked == false && Option_Conditional_Or_3.IsChecked == false))
            {
                LastSelected = 0;
                this.Combo_Conditional_Item_3.SelectedIndex = 0;
            }
            else if ((Key.Delete == e.Key || ((sen.SelectedIndex == -1 && sen.Text == "") || (sen.SelectedIndex == -1 && sen.Text != ""))) && (this.Combo_Conditional_Item_4.IsKeyboardFocusWithin || this.Combo_Conditional_Item_4.IsDropDownOpen) &&
                (Option_Conditional_And_4.IsChecked == false && Option_Conditional_Or_4.IsChecked == false))
            {
                LastSelected = 0;
                this.Combo_Conditional_Item_4.SelectedIndex = 0;
            }
            else if ((Key.Delete == e.Key || ((sen.SelectedIndex == -1 && sen.Text == "") || (sen.SelectedIndex == -1 && sen.Text != ""))) && (this.Combo_Conditional_Item_5.IsKeyboardFocusWithin || this.Combo_Conditional_Item_5.IsDropDownOpen))
            {
                LastSelected = 0;
                this.Combo_Conditional_Item_5.SelectedIndex = 0;
            }
            else if ((Key.Delete == e.Key || ((sen.SelectedIndex == -1 && sen.Text == "") || (sen.SelectedIndex == -1 && sen.Text != ""))) && (this.Combo_Classify_Item.IsKeyboardFocusWithin || this.Combo_Classify_Item.IsDropDownOpen))
            {
                LastSelected = 0;
                this.Combo_Classify_Item.SelectedIndex = 0;
            }
            else if (sen.SelectedItem == null && (sen.IsKeyboardFocusWithin || sen.IsDropDownOpen))
            {
                if (sen.Name != "Combo_Classify_Item")
                    sen.SelectedIndex = LastSelected;
            }
        }

        System.Windows.Controls.ComboBox combo = null;
        string LastSelectedText = "";
        private void Combo_Conditional_Item_1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                if (e.IsRepeat)
                { e.Handled = true; }
            }
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
            if (Key.Delete == e.Key && (this.Combo_Conditional_Item_1.IsKeyboardFocusWithin || this.Combo_Conditional_Item_1.IsDropDownOpen) &&
                (Option_Conditional_And_1.IsChecked == false && Option_Conditional_Or_1.IsChecked == false))
            {
                LastSelectedText = "";
                LastSelected = -1;
                e.Handled = false;
            }
            else if (Key.Delete == e.Key && (this.Combo_Conditional_Item_2.IsKeyboardFocusWithin || this.Combo_Conditional_Item_2.IsDropDownOpen) &&
                (Option_Conditional_And_2.IsChecked == false && Option_Conditional_Or_2.IsChecked == false))
            {
                LastSelectedText = "";
                LastSelected = -1;
                e.Handled = false;
            }
            else if (Key.Delete == e.Key && (this.Combo_Conditional_Item_3.IsKeyboardFocusWithin || this.Combo_Conditional_Item_3.IsDropDownOpen) &&
                (Option_Conditional_And_3.IsChecked == false && Option_Conditional_Or_3.IsChecked == false))
            {
                LastSelectedText = "";
                LastSelected = -1;
                e.Handled = false;
            }
            else if (Key.Delete == e.Key && (this.Combo_Conditional_Item_4.IsKeyboardFocusWithin || this.Combo_Conditional_Item_4.IsDropDownOpen) &&
                (Option_Conditional_And_4.IsChecked == false && Option_Conditional_Or_4.IsChecked == false))
            {
                LastSelectedText = "";
                LastSelected = -1;
                e.Handled = false;
            }
            else if (Key.Delete == e.Key && (this.Combo_Conditional_Item_5.IsKeyboardFocusWithin || this.Combo_Conditional_Item_5.IsDropDownOpen))
            {
                LastSelectedText = "";
                LastSelected = -1;
                e.Handled = false;
            }
            else if (Key.Delete == e.Key && (this.Combo_Classify_Item.IsKeyboardFocusWithin || this.Combo_Classify_Item.IsDropDownOpen))
            {
                LastSelectedText = "";
                LastSelected = -1;
                e.Handled = false;
            }
            else if (Key.Delete == e.Key)
            {
                e.Handled = true;
            }
            else if ((txt != null && txt.SelectedText.Length == txt.Text.Length && Key.Back == e.Key) && (this.Combo_Conditional_Item_1.IsKeyboardFocusWithin || this.Combo_Conditional_Item_1.IsDropDownOpen) &&
                (Option_Conditional_And_1.IsChecked == true || Option_Conditional_Or_1.IsChecked == true))
            {
                e.Handled = true;
            }
            else if ((txt != null && txt.SelectedText.Length == txt.Text.Length && Key.Back == e.Key) && (this.Combo_Conditional_Item_2.IsKeyboardFocusWithin || this.Combo_Conditional_Item_2.IsDropDownOpen) &&
                (Option_Conditional_And_2.IsChecked == true || Option_Conditional_Or_2.IsChecked == true))
            {
                e.Handled = true;
            }
            else if ((txt != null && txt.SelectedText.Length == txt.Text.Length && Key.Back == e.Key) && (this.Combo_Conditional_Item_3.IsKeyboardFocusWithin || this.Combo_Conditional_Item_3.IsDropDownOpen) &&
                (Option_Conditional_And_3.IsChecked == true || Option_Conditional_Or_3.IsChecked == true))
            {
                e.Handled = true;
            }
            else if ((txt != null && txt.SelectedText.Length == txt.Text.Length && Key.Back == e.Key) && (this.Combo_Conditional_Item_4.IsKeyboardFocusWithin || this.Combo_Conditional_Item_4.IsDropDownOpen) &&
                (Option_Conditional_And_4.IsChecked == true || Option_Conditional_Or_4.IsChecked == true))
            {
                e.Handled = true;
            }
            int delux = LastSelected;
        }

        int LastSelected = 0;
        private void Combo_Conditional_Item_1_GotFocus(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.ComboBox sen = ((System.Windows.Controls.ComboBox)sender);
            combo = sen;
        }

        private void Combo_Classify_Item_DropDownClosed(object sender, EventArgs e)
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

        private void Combo_Classify_Item_Loaded(object sender, RoutedEventArgs e)
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

        bool FirstFocus = true;
        private void Combo_Classify_Item_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (!FirstFocus)
            {
                System.Windows.Controls.ComboBox sen = ((System.Windows.Controls.ComboBox)sender);
                sen.IsDropDownOpen = true;
            }
            else
                FirstFocus = false;
        }

    }
}
