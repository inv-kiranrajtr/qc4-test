using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Shapes;
using static Qc4Launcher.Forms.Multivariate_Analysis.CollectionClass;
using Excel = Microsoft.Office.Interop.Excel;
namespace Qc4Launcher.Forms.Multivariate_Analysis.Usercontrols
{
    /// <summary>
    /// Interaction logic for crosstabulate.xaml
    /// </summary>
    public partial class crosstabulate : UserControl
    {
        Excel.Workbook Worksheet;
        public static Dictionary<string, QuestionSettings> dictionary = new Dictionary<string, QuestionSettings>();
        SourceVariableFromList sourceGetList = new SourceVariableFromList();
        ObservableCollection<psmQuestions> SourceVariableList = new ObservableCollection<psmQuestions>();
        public crosstabulate()
        {
            InitializeComponent();
            DisableEnable();

        }
        List<psmQuestions> allDataToCompare = new List<psmQuestions>();
        public void LoadingData(Excel.Workbook workbook)
        {
            Worksheet = workbook;
            InitialLoad();

            choicelist.IsEnabled = false;
            combobox_choices.IsEnabled = false;
            crosstabulate ct = new crosstabulate();
        }

        public void DisableEnable()
        {
            if (txt_Expensive_variable_List.Count > 0)
            {
                choicetxt2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                choicetxt1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
            }
            else
            {
                choicetxt2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                choicetxt1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));

            }

            if (txt_Expensive_variable_List2.Count > 0)
            {
                choicetxt3.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                choicetxt4.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
            }
            else
            {

                choicetxt3.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                choicetxt4.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
            }
        }
        public void InitialLoad()
        {
            SourceVariableList = new ObservableCollection<psmQuestions>(sourceGetList.GetVariableFromList(Worksheet, "List_Item_SAMA"));
            allDataToCompare = SourceVariableList.ToList();
            psmQuestions insta = new psmQuestions();
            RemovechoiceGreater3();

        }
        public void RemovechoiceGreater3()
        {
            try
            {
                var List = allDataToCompare;
                SourceVariableList.Clear();
                foreach (var item in List)
                {
                    if (item.Choices.Count > 2)
                    {
                        SourceVariableList.Add(item);
                    }
                }
                Multi_Process_Grid.ItemsSource = SourceVariableList;
            }
            catch (Exception ex)
            {
            }
        }
        private void b1SetColor(object sender, MouseButtonEventArgs e)
        {
            if (ExpGrid != null)
                ExpGrid.Focus();
        }
        System.Windows.Controls.DataGrid ExpGrid = null;
        private void Multi_Process_Grid_CurrentCellChanged(object sender, EventArgs e)
        {
            var dg = (System.Windows.Controls.DataGrid)sender;
            ExpGrid = dg;
            dg.Focus();
        }
        ObservableCollection<psmQuestions> txt_Expensive_variable_List = new ObservableCollection<psmQuestions>();
        ObservableCollection<string> ChoicesList = new ObservableCollection<string>();
        public void SetTexBoxValue(string variable,int index)
        {
            try
            {
                if (SourceVariableList.Any(x => x.Variable == variable))
                {
                    var find = SourceVariableList.FirstOrDefault(x => x.Variable == variable);
                    txt_Expensive_variable_List.Clear();
                    txt_Expensive_variable_List.Add(find);
                    SourceVariableList.Remove(find);
                    txt_Expensive_variable.Text = txt_Expensive_variable_List[0].Variable;
                }
                if (txt_Expensive_variable_List.Count > 0)
                {
                    combobox_choices.IsEnabled = true;
                    ChoicesList.Clear();
                    combobox_choices.ItemsSource = ChoicesList;
                    int i = 1;
                    foreach (var item in txt_Expensive_variable_List[0].Choices)
                    {
                        string choice = i + ": " + item;
                        ChoicesList.Add(choice);
                        i++;
                    }
                    combobox_choices.ItemsSource = ChoicesList;
                    combobox_choices.SelectedIndex = (index - 1);
                }
                Multi_Process_Grid.Focus();
                if (txt_Expensive_variable_List.Count > 0 && txt_Expensive_variable_List2.Count > 0)
                {
                    OnUserControlButtonClick();
                }
                else
                {
                    OnUserControlButtonClick2();
                }

                DisableEnable();
            }
            catch (Exception ex)
            {

            }
        }
        public void setTxtVal(string variable,int index)
        {

            try
            {
                if (SourceVariableList.Any(x => x.Variable == variable))
                {
                    var find = SourceVariableList.FirstOrDefault(x => x.Variable == variable);
                    txt_Expensive_variable_List2.Clear();
                    txt_Expensive_variable_List2.Add(find);
                    SourceVariableList.Remove(find);
                    txt_variable_exp.Text = txt_Expensive_variable_List2[0].Variable;
                }
                if (txt_Expensive_variable_List2.Count > 0)
                {
                    choicelist.IsEnabled = true;
                    ChoicesList2.Clear();
                    choicelist.ItemsSource = ChoicesList2;
                    int i = 1;
                    foreach (var item in txt_Expensive_variable_List2[0].Choices)
                    {
                        string choice = i + ": " + item;
                        ChoicesList2.Add(choice);
                        i++;
                    }
                    choicelist.ItemsSource = ChoicesList2;
                    choicelist.SelectedIndex = (index - 1);
                }
                if (txt_Expensive_variable_List.Count > 0 && txt_Expensive_variable_List2.Count > 0)
                {
                    OnUserControlButtonClick();
                }
                else
                {
                    OnUserControlButtonClick2();
                }
                DisableEnable();
            }
            catch (Exception ex)
            {

            }
        }

       
        private void Btn_rgt_arrw_Click(object sender, RoutedEventArgs e)
        {
            if (Multi_Process_Grid.SelectedItem != null)
            {
                psmQuestions crs = (psmQuestions)Multi_Process_Grid.SelectedItem;


                if (txt_Expensive_variable_List.Count > 0)
                {
                    SourceVariableList.Add(txt_Expensive_variable_List[0]); SourceVariableList.Sort(k => k.OrderNo);
                    txt_Expensive_variable_List.Clear();
                    txt_Expensive_variable_List.Add(crs);
                    SourceVariableList.Remove(crs);
                    txt_Expensive_variable.Text = txt_Expensive_variable_List[0].Variable;


                }
                else
                {
                    txt_Expensive_variable_List.Clear();
                    txt_Expensive_variable_List.Add(crs);
                    SourceVariableList.Remove(crs);
                    txt_Expensive_variable.Text = txt_Expensive_variable_List[0].Variable;
                }

               

            }
            if (txt_Expensive_variable_List.Count > 0)
            {
                combobox_choices.IsEnabled = true;
                ChoicesList.Clear();
                combobox_choices.ItemsSource = ChoicesList;
                int i = 1;
                foreach (var item in txt_Expensive_variable_List[0].Choices)
                {
                    string choice = i + ": " + item;
                    ChoicesList.Add(choice);
                    i++;
                }
                combobox_choices.ItemsSource = ChoicesList;
                combobox_choices.SelectedIndex = (ChoicesList.Count - 1);
            }
            Multi_Process_Grid.Focus();
            if (txt_Expensive_variable_List.Count > 0 && txt_Expensive_variable_List2.Count > 0)
            {
                OnUserControlButtonClick();
            }
            else
            {
                OnUserControlButtonClick2();
            }

            DisableEnable();

        }
        public event EventHandler UserControlButtonClicked;
        public event EventHandler UserControlButtonClicked2;
        private void OnUserControlButtonClick()
        {
            if (UserControlButtonClicked != null)
            {
                UserControlButtonClicked(this, EventArgs.Empty);
            }
        }
        private void OnUserControlButtonClick2()
        {
            if (UserControlButtonClicked2 != null)
            {
                UserControlButtonClicked2(this, EventArgs.Empty);
            }
        }
        public event EventHandler CloseButtonClicked;
        protected virtual void OnCloseButtonClicked(EventArgs e)
        {
            var handler = CloseButtonClicked;
            if (handler != null)
                handler(this, e);
        }
        public event EventHandler DisableButton;
        protected virtual void OnDisablebutton(EventArgs e)
        {
            var handler = DisableButton;
            if (handler != null)
                handler(this, e);
        }
        private void Lbl_Single_Left_Arrow_Click(object sender, RoutedEventArgs e)
        {
            if (txt_Expensive_variable_List.Count > 0)
            {
                SourceVariableList.Add(txt_Expensive_variable_List[0]);
                SourceVariableList.Sort(k => k.OrderNo);
                txt_Expensive_variable_List.Clear();
                txt_Expensive_variable.Text = string.Empty;
                ChoicesList.Clear();
                combobox_choices.IsEnabled = false;
                

            }
            else
            {
                txt_Expensive_variable_List.Clear();
                ChoicesList.Clear();
                combobox_choices.IsEnabled = false;
            }
            if (txt_Expensive_variable_List.Count > 0 && txt_Expensive_variable_List2.Count > 0)
            {
                OnUserControlButtonClick();
            }
            else
            {
                OnUserControlButtonClick2();
            }
            DisableEnable();
        }
        public void btnFunction()
        {
            if (txt_Expensive_variable_List.Count > 0 && txt_Expensive_variable_List2.Count > 0)
            {
                OnUserControlButtonClick();
            }
            else
            {
                OnUserControlButtonClick2();
            }
        }
        ObservableCollection<psmQuestions> txt_Expensive_variable_List2 = new ObservableCollection<psmQuestions>();
        ObservableCollection<string> ChoicesList2 = new ObservableCollection<string>();
        private static T GetVisualChild<T>(DependencyObject parent) where T : Visual
        {
            T child = default(T);

            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }
        private void Btn_rgt_arrw1_Click(object sender, RoutedEventArgs e)
        {
            if (Multi_Process_Grid.SelectedItem != null)
            {
                psmQuestions crs = (psmQuestions)Multi_Process_Grid.SelectedItem;


                if (txt_Expensive_variable_List2.Count > 0)
                {
                    SourceVariableList.Add(txt_Expensive_variable_List2[0]); SourceVariableList.Sort(k => k.OrderNo);
                    txt_Expensive_variable_List2.Clear();
                    txt_Expensive_variable_List2.Add(crs);
                    SourceVariableList.Remove(crs);
                    txt_variable_exp.Text = txt_Expensive_variable_List2[0].Variable;


                }
                else
                {
                    txt_Expensive_variable_List2.Clear();
                    txt_Expensive_variable_List2.Add(crs);
                    SourceVariableList.Remove(crs);
                    txt_variable_exp.Text = txt_Expensive_variable_List2[0].Variable;
                }

                

            }
            if (txt_Expensive_variable_List2.Count > 0)
            {
                choicelist.IsEnabled = true;
                ChoicesList2.Clear();
                choicelist.ItemsSource = ChoicesList2;
                int i = 1;
                foreach (var item in txt_Expensive_variable_List2[0].Choices)
                {
                    string choice = i + ": " + item;
                    ChoicesList2.Add(choice);
                    i++;
                }
                choicelist.ItemsSource = ChoicesList2;
                choicelist.SelectedIndex = (ChoicesList2.Count - 1);
            }
            Multi_Process_Grid.Focus();
            if (txt_Expensive_variable_List.Count > 0 && txt_Expensive_variable_List2.Count > 0)
            {
                OnUserControlButtonClick();
            }
            else
            {
                OnUserControlButtonClick2();
            }
            DisableEnable();
        }

        private void Lbl_Single_Left_Arrow1_Click(object sender, RoutedEventArgs e)
        {
            if (txt_Expensive_variable_List2.Count > 0)
            {
                SourceVariableList.Add(txt_Expensive_variable_List2[0]);
                SourceVariableList.Sort(k => k.OrderNo);
                txt_Expensive_variable_List2.Clear();
                txt_variable_exp.Text = string.Empty;
                ChoicesList2.Clear();
                choicelist.IsEnabled = false;
               

            }
            else
            {
                txt_Expensive_variable_List2.Clear();
                ChoicesList2.Clear();
                choicelist.IsEnabled = false;
            }
            if (txt_Expensive_variable_List.Count > 0 && txt_Expensive_variable_List2.Count > 0)
            {
                OnUserControlButtonClick();
            }
            else
            {
                OnUserControlButtonClick2();
            }
            DisableEnable();
        }
        private void List_GT_Summary_CurrentCellChanged(object sender, EventArgs e)
        {
            var dg = (System.Windows.Controls.DataGrid)sender;
            ExpGrid = dg;
            dg.Focus();
        }

        private void List_GT_Summary_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            System.Windows.Controls.DataGrid grid = null;
            System.Windows.Controls.Button btn = null;
            System.Windows.Controls.CheckBox chk = null;

            if (sender is DataGrid)
                grid = sender as System.Windows.Controls.DataGrid;
            else if (sender is Button)
                btn = sender as System.Windows.Controls.Button;
            else if (sender is CheckBox)
                chk = sender as System.Windows.Controls.CheckBox;

            if (grid != null && e.Key == Key.Tab && grid.Name is "Multi_Process_Grid")
            {
                e.Handled = true;
                btn_rgt_arrw.Focus();
            }

            QC4Common.Util.GridCommonFunc.Arrow(sender, e);
        }
        bool FisrtTabFocus = true;
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as TabControl).SelectedIndex == 1 && FisrtTabFocus)
            {
                // Page_GT_Narrow.Focus();
                FisrtTabFocus = false;
            }
            else if ((sender as TabControl).SelectedIndex != 1)
            {
                FisrtTabFocus = true;
            }
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {

            }
        }
    }
}
