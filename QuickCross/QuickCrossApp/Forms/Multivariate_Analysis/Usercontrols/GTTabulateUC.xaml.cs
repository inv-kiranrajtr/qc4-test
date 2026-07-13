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
    /// Interaction logic for GTTabulateUC.xaml
    /// </summary>
    public partial class GTTabulateUC : UserControl
    {
        Excel.Workbook Worksheet;
        public static Dictionary<string, QuestionSettings> dictionary = new Dictionary<string, QuestionSettings>();
        SourceVariableFromList sourceGetList = new SourceVariableFromList();
        ObservableCollection<psmQuestions> SourceVariableList = new ObservableCollection<psmQuestions>();
        public GTTabulateUC()
        {
            InitializeComponent();
            combobox_choices.IsEnabled = false;

            DisableEnable();
        }
        private void b1SetColor(object sender, MouseButtonEventArgs e)
        {
            if (ExpGrid != null)
                ExpGrid.Focus();
        }
        List<psmQuestions> allDataToCompare = new List<psmQuestions>();
        public void LoadingData(Excel.Workbook workbook)
        {
            Worksheet = workbook;
            InitialLoad();
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
        public void loadDatafromsheet(List<string> list, int index)
        {
            ObservableCollection<psmQuestions> demoList = new ObservableCollection<psmQuestions>();
            try
            {
                if (list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        var find = SourceVariableList.FirstOrDefault(x => x.Variable == item);
                        demoList.Add(find);
                    }
                    demoList.Sort(k => k.OrderNo);
                    SortItemByDataSelected(demoList);
                    SortItemByChoice();

                    foreach (psmQuestions crs in selectedList)
                    {
                        SourceVariableList.Remove(crs);
                    }
                    selectedList.Sort(k => k.OrderNo);
                    SourceVariableList.Sort(k => k.OrderNo);
                    //BtnDisableEnable();
                    btnEnableFunction();
                }
                if (selectedList.Count > 0)
                {
                    combobox_choices.IsEnabled = true;
                    ChoicesList.Clear();
                    combobox_choices.ItemsSource = ChoicesList;
                    int i = 1;
                    foreach (var item in selectedList[0].Choices)
                    {
                        string choice = i + ": " + item;
                        ChoicesList.Add(choice);
                        i++;
                    }
                    combobox_choices.ItemsSource = ChoicesList;
                    combobox_choices.SelectedIndex = (index - 1);
                }

            }
            catch (Exception ex) { }

        }
        public void SortItemByDataSelected(ObservableCollection<psmQuestions> list)
        {
            List<psmQuestions> dataSelected = new List<psmQuestions>();
            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    if (item.Choices.Count == list[0].Choices.Count && item.AnswerType == list[0].AnswerType)
                    {
                        bool validate = false;
                        int i = 0;
                        foreach (var choices in item.Choices)
                        {
                            if (choices.Equals(list[0].Choices[i]))
                            {
                                validate = true;

                            }
                            else
                            {
                                validate = false;
                                break;
                            }
                            i++;
                        }
                        if (validate)
                        {
                            selectedList.Add(item);
                        }
                    }
                }
            }
            DisableEnable();
        }

        public void DisableEnable()
        {
            if (Selected_grid.Items.Count > 0)
            {
                choicetxt2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                choicetxt1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
            }
            else
            {
                choicetxt2.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                choicetxt1.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));

            }
        }
        public void SortItemByChoice()
        {
            bool iscontain = false;
            if (selectedList.Count > 0)
            {
                SourceVariableList.Clear();

                foreach (var item in allDataToCompare)
                {
                    if (item.Choices.Count == selectedList[0].Choices.Count && item.AnswerType == selectedList[0].AnswerType)
                    {
                        for (int i = 0; i < selectedList[0].Choices.Count; i++)
                        {
                            if (item.Choices[i].Equals(selectedList[0].Choices[i]))
                            {
                                iscontain = true;
                            }
                            else
                            {
                                iscontain = false;

                            }
                            if (!iscontain)
                            {
                                break;
                            }
                        }
                        if (iscontain)
                        {
                            SourceVariableList.Add(item);
                            iscontain = false;
                        }

                    }
                }
            }
        }
        System.Windows.Controls.DataGrid ExpGrid = null;
        private void Multi_Process_Grid_CurrentCellChanged(object sender, EventArgs e)
        {
            var dg = (System.Windows.Controls.DataGrid)sender;
            ExpGrid = dg;
            dg.Focus();
        }
        ObservableCollection<psmQuestions> selectedList = new ObservableCollection<psmQuestions>();
        ObservableCollection<psmQuestions> listdelete = new ObservableCollection<psmQuestions>();
        ObservableCollection<string> ChoicesList = new ObservableCollection<string>();
        private void Btn_rgt_arrw_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<psmQuestions> dataSelected = new ObservableCollection<psmQuestions>();


            if (Multi_Process_Grid.SelectedItems.Count > 0)
            {
                if (Multi_Process_Grid.SelectedItems.Count >= 101 || (Multi_Process_Grid.SelectedItems.Count + Selected_grid.Items.Count) >= 101)
                {
                    MessageBox.Show(LocalResource.MULTI_CLUSTOR_MSGBX_WITHIN_100, "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                foreach (psmQuestions crs in Multi_Process_Grid.SelectedItems)
                {
                    dataSelected.Add(crs);

                    // selectedList.Add(crs);

                }
                dataSelected.Sort(k => k.OrderNo);
                SortItemByDataSelected(dataSelected);
                SortItemByChoice();

                foreach (psmQuestions crs in selectedList)
                {
                    SourceVariableList.Remove(crs);
                }
                selectedList.Sort(k => k.OrderNo);
                SourceVariableList.Sort(k => k.OrderNo);
                //BtnDisableEnable();
                btnEnableFunction();

            }
            if (selectedList.Count > 0)
            {
                combobox_choices.IsEnabled = true;
                ChoicesList.Clear();
                combobox_choices.ItemsSource = ChoicesList;
                int i = 1;
                foreach (var item in selectedList[0].Choices)
                {
                    string choice = i + ": " + item;
                    ChoicesList.Add(choice);
                    i++;
                }
                combobox_choices.ItemsSource = ChoicesList;
                combobox_choices.SelectedIndex = (ChoicesList.Count - 1);
            }

        }
        public void btnEnableFunction()
        {
            if (selectedList.Count > 0)
            {
                OnUserControlButtonClick();

            }
            else
            {
                OnUserControlButtonClick2();
            }
        }
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
        public void InitialLoad()
        {
            SourceVariableList = new ObservableCollection<psmQuestions>(sourceGetList.GetVariableFromList(Worksheet, "List_Item_SAMA"));
            allDataToCompare = SourceVariableList.ToList();
            psmQuestions insta = new psmQuestions();
            RemovechoiceGreater3();

            Selected_grid.ItemsSource = selectedList;
        }
        private void Lbl_Single_Left_Arrow_Click(object sender, RoutedEventArgs e)
        {
            if (Selected_grid.SelectedItems.Count > 0)
            {
                foreach (psmQuestions crs in Selected_grid.SelectedItems)
                {
                    SourceVariableList.Add(crs);
                }
                foreach (psmQuestions crs in SourceVariableList)
                {
                    selectedList.Remove(crs);
                }

                selectedList.Sort(k => k.OrderNo);
                SourceVariableList.Sort(k => k.OrderNo);
                // BtnDisableEnable();


            }

            if (Selected_grid.Items.Count == 0)
            {
                InitialLoad();
                ChoicesList.Clear();
                combobox_choices.IsEnabled = false;
            }
            btnEnableFunction();
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
            if (btn != null && e.Key == Key.Tab && btn.Name is "Lbl_Single_Left_Arrow")
            {
                if (Selected_grid.Items.Count > 0)
                {
                    Selected_grid.FocusVisualStyle = null;
                    Selected_grid.Focus();
                    if (Selected_grid.SelectedIndex < 0 && Selected_grid.Items.Count >= 1)
                    {
                        Selected_grid.SelectedIndex = 0;
                    }
                }
                else
                {
                    Multi_Process_Grid.FocusVisualStyle = null;
                    Multi_Process_Grid.Focus();
                    if (Multi_Process_Grid.SelectedIndex < 0 && Multi_Process_Grid.Items.Count >= 1)
                    {
                        Multi_Process_Grid.SelectedIndex = 0;
                    }
                }
                e.Handled = true;
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
