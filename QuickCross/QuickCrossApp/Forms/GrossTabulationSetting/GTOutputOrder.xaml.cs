using Qc4Launcher.Forms.GrossTabulationSetting.Common;
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
using static FilterSettingsView.FilterSettingsClass;

namespace Qc4Launcher.Forms.GrossTabulationSetting
{
    /// <summary>
    /// Interaction logic for GTOutputOrder.xaml
    /// </summary>
    public partial class GTOutputOrder : Window
    {
        ObservableCollection<DataGT> DataExport_LBVariablesForGT = new ObservableCollection<DataGT>();
        ObservableCollection<DataGT> DataExport_LBVariablesForGTRight =new ObservableCollection<DataGT>();
        ObservableCollection<DataGT> LastSavedData = new ObservableCollection<DataGT>();
        bool IsNotOrder = true;
        GrossTabulationSetting GTS;
        public GTOutputOrder(ObservableCollection<DataGT> _dataExport_LBVariablesForGT, GrossTabulationSetting grossTabulationSetting)
        {
            GTS = grossTabulationSetting;

            foreach (var data in _dataExport_LBVariablesForGT)
            {
                data.QuestionVariable = data.QuestionVariable;
                DataExport_LBVariablesForGT.Add(data);
            }

            for (int i=0;i< DataExport_LBVariablesForGT.Count;i++)
            {
                LastSavedData.Add(DataExport_LBVariablesForGT[i]);
            }

            for (int i = 0; i < DataExport_LBVariablesForGT.Count; i++)
            {
                DataExport_LBVariablesForGT[i].TempIndex = DataExport_LBVariablesForGT[i].QuestionIndex;
            }
            InitializeComponent();
        }

        private void Command_Cancel_Click(object sender, RoutedEventArgs e)
        {            
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadListBox();
        }

        private void LoadListBox()
        {
            List_Item_Left.ItemsSource = DataExport_LBVariablesForGT;
            List_Item_Right.ItemsSource = DataExport_LBVariablesForGTRight;
        }

        private void Command_Select_All_Right_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < DataExport_LBVariablesForGT.Count; i++)
            {
                DataGT dataGt = DataExport_LBVariablesForGT[i];
                DataExport_LBVariablesForGTRight.Add(dataGt);
            }
            DataExport_LBVariablesForGT.Clear();
            for (int i = 0; i < DataExport_LBVariablesForGTRight.Count; i++)
            {
                DataExport_LBVariablesForGTRight[i].QuestionIndex = i;
            }
            LoadListBox();
            Command_Entry.IsEnabled = true;
        }

        private void Command_Select_Right_Click(object sender, RoutedEventArgs e)
        {
            var items = List_Item_Left.SelectedItems.Cast<DataGT>().ToList();
            for(int i=0;i<items.Count;i++)
            {
                DataExport_LBVariablesForGTRight.Add(items[i]);
                DataExport_LBVariablesForGT.Remove(items[i]);
            }
            if (List_Item_Left.Items.Count == 0)
                Command_Entry.IsEnabled = true;
            for (int i = 0; i < DataExport_LBVariablesForGTRight.Count; i++)
            {
                DataExport_LBVariablesForGTRight[i].QuestionIndex = i;
            }
            LoadListBox();
        }

        private void Command_Select_All_Left_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < DataExport_LBVariablesForGTRight.Count; i++)
            {
                DataGT dataGt = DataExport_LBVariablesForGTRight[i];
                dataGt.QuestionIndex = dataGt.TempIndex;
                DataExport_LBVariablesForGT.Add(dataGt);
            }
            var items = DataExport_LBVariablesForGT.OrderBy(x => x.QuestionIndex).ToList();
            DataExport_LBVariablesForGT.Clear();
            DataExport_LBVariablesForGTRight.Clear();

            for (int i=0;i< items.Count;i++)
            {
                DataExport_LBVariablesForGT.Add(items[i]);
            }
            LoadListBox();
            Command_Entry.IsEnabled = false;
        }

        private void Command_Select_Left_Click(object sender, RoutedEventArgs e)
        {
            var items = List_Item_Right.SelectedItems.Cast<DataGT>().ToList();
            if (items.Count == 0)
                return;
            for (int i = 0; i < items.Count; i++)
            {
                DataGT itm = items[i];
                itm.QuestionIndex = itm.TempIndex;
                DataExport_LBVariablesForGT.Add(itm);
                DataExport_LBVariablesForGTRight.Remove(items[i]);
            }
            items = DataExport_LBVariablesForGT.OrderBy(x => x.QuestionIndex).ToList();
            DataExport_LBVariablesForGT.Clear();
            for (int i = 0; i < items.Count; i++)
            {
                DataExport_LBVariablesForGT.Add(items[i]);
            }
            LoadListBox();
            Command_Entry.IsEnabled = false;
        }      

        private void Command_Entry_Click(object sender, RoutedEventArgs e)
        {
            IsNotOrder = false;
            for (int i = 0; i < DataExport_LBVariablesForGTRight.Count; i++)
            {
                DataGT data = DataExport_LBVariablesForGTRight[i];
                DataExport_LBVariablesForGT.Add(data);
            }
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (IsNotOrder)
            {
                DataExport_LBVariablesForGT.Clear();
                for (int i = 0; i < LastSavedData.Count; i++)
                {
                    DataExport_LBVariablesForGT.Add(LastSavedData[i]);
                    GTS.GTTabulationItems[LastSavedData[i].QuestionIndex].QuestionIndex = LastSavedData[i].QuestionIndex;
                }
            }
            else
            {
                List<DataGT> data = new List<DataGT>();
                for (int i = 0; i < DataExport_LBVariablesForGT.Count; i++)
                {
                    int qsNum = DataExport_LBVariablesForGT[i].QuestionNumber;
                    int startIndx = DataExport_LBVariablesForGT[i].TempIndex;
                    if (qsNum > 0)
                    {
                        qsNum += startIndx;
                        for (int j = startIndx; j < qsNum; j++)
                        {
                            data.Add(GTS.GTTabulationItems[j]);
                        }
                    }
                    else
                        data.Add(DataExport_LBVariablesForGT[i]);
                }
                GTS.GTTabulationItems.Clear();
                for (int i = 0; i < data.Count; i++)
                {
                    data[i].QuestionIndex = i;
                    GTS.GTTabulationItems.Add(data[i]);
                }
            }
        }

        System.Windows.Controls.DataGrid ExpGrid = null;
        private void List_Item_Left_CurrentCellChanged(object sender, EventArgs e)
        {
            var dg = (System.Windows.Controls.DataGrid)sender;
            ExpGrid = dg;
            dg.Focus();
        }

        private void List_Item_Right_CurrentCellChanged(object sender, EventArgs e)
        {
            var dg = (System.Windows.Controls.DataGrid)sender;
            ExpGrid = dg;
            dg.Focus();
        }

        private void Window_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void List_Item_Left_PreviewKeyDown(object sender, KeyEventArgs e)
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
