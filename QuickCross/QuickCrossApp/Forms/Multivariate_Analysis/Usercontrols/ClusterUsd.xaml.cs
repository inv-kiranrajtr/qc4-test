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
using System.Windows.Shapes;
using static Qc4Launcher.Forms.Multivariate_Analysis.CollectionClass;
using Excel = Microsoft.Office.Interop.Excel;


namespace Qc4Launcher.Forms.Multivariate_Analysis.Usercontrols
{
    /// <summary>
    /// Interaction logic for ClusterUsd.xaml
    /// </summary>
    public partial class ClusterUsd : UserControl
    {
        Excel.Workbook Worksheet;
        public static Dictionary<string, QuestionSettings> dictionary = new Dictionary<string, QuestionSettings>();
        SourceVariableFromList sourceGetList = new SourceVariableFromList();
        ObservableCollection<psmQuestions> SourceVariableList = new ObservableCollection<psmQuestions>();
        public ClusterUsd()
        {
            InitializeComponent();
           
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
                    if (item.Choices.Count > 3)
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
        public void SortItemByDataSelected(List<psmQuestions> list)
        {
            List<psmQuestions> dataSelected = new List<psmQuestions>();
            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    if (item.Choices.Count == list[0].Choices.Count)
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
        }
        private void List_GT_Summary_CurrentCellChanged(object sender, EventArgs e)
        {
            var dg = (System.Windows.Controls.DataGrid)sender;
            ExpGrid = dg;
            dg.Focus();
        }
        public void SortItemByChoice()
        {
            bool iscontain = false;
            if (selectedList.Count > 0)
            {
                SourceVariableList.Clear();

                foreach (var item in allDataToCompare)
                {
                    if (item.Choices.Count == selectedList[0].Choices.Count)
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
             if (Multi_Process_Grid.SelectedItems.Count > 0 && Multi_Process_Grid.SelectedItems.Count<100)
            {
                foreach (psmQuestions crs in Multi_Process_Grid.SelectedItems)
                {
                    selectedList.Add(crs);
                }
                foreach (psmQuestions crs in selectedList)
                {
                    SourceVariableList.Remove(crs);
                }

                selectedList.Sort(k => k.OrderNo);
                SourceVariableList.Sort(k => k.OrderNo);
                
            }
            else
            {
                MessageBox.Show(LocalResource.MULTI_CLUSTOR_MSGBX_WITHIN_100, "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error);
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
            SourceVariableList = new ObservableCollection<psmQuestions>(sourceGetList.GetVariableFromList(Worksheet, "List_Item_SAN"));
            psmQuestions insta = new psmQuestions();
            sourceGetList.RemoveItem(SourceVariableList, insta);
            Multi_Process_Grid.ItemsSource = SourceVariableList;
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
        }

        private void Multi_Process_Grid_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
