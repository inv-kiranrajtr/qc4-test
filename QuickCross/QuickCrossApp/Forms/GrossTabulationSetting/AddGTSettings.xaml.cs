using ExcelAddIn.Common;
using FilterSettingsView;
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
using excel = Microsoft.Office.Interop.Excel;

namespace Qc4Launcher.Forms.GrossTabulationSetting
{
    /// <summary>
    /// Interaction logic for AddGTSettings.xaml
    /// </summary>
    public partial class AddGTSettings : Window
    {
        string SelectedTypeFullName = "";
        GrossTabulationSetting GTS { get; set; }
        ObservableCollection<DataGT> QsItems = new ObservableCollection<DataGT>();
        ObservableCollection<DataGT> QsRightItems = new ObservableCollection<DataGT>();
        QC4Common.Util.FormUtil formUtil = new QC4Common.Util.FormUtil();
        excel.Workbook Workbook;
        bool IsMultipleSelection = false;
        DataGT DataGt;
        string Mode = "";
        int CurrentRightListConut = 0;
        string CurrentComboVal = "";
        string CurrentHeader = "";
        string CurrentTextval = "";
        List<DataGT> CurrentItems = new List<DataGT>();
        bool IsClosing = false;
        string TableHeading = "";
        public AddGTSettings(string sType, GrossTabulationSetting gTS, Microsoft.Office.Interop.Excel.Workbook _workbook,string mode, DataGT dataGt=null)
        {
            Workbook = _workbook;
            DataGt = dataGt;
            SelectedTypeFullName = sType;
            GTS = gTS;
            if (GTS != null)
                GTS.Hide();
            Mode = mode;
            InitializeComponent();
        }

        private void Command_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                InitalSettings();
            }
            catch { }
        }

        private void InitalSettings()
        {
            if (Mode == "EDIT" && GTS._dataExport_LBVariablesForGT != null)
            {
                if (GTS._dataExport_LBVariablesForGT[0] == DataGt)
                    Command_Back.IsEnabled = false;
                else
                    Command_Back.IsEnabled = true;

                if (GTS._dataExport_LBVariablesForGT.IndexOf(DataGt) == (GTS._dataExport_LBVariablesForGT.Count - 1))
                    Command_Next.IsEnabled = false;
                else
                    Command_Next.IsEnabled = true;
            }


            List_Item_Left.Focus();
            Combo_Summary_Variety.Text = SelectedTypeFullName;
            Combo_Summary_Variety.Foreground = new SolidColorBrush(Colors.Black);
            if (SelectedTypeFullName == LocalResource.FOR_N || SelectedTypeFullName == LocalResource.FOR_MTN)
            {
                Combo_Graph_Variety.Visibility = Visibility.Hidden;
                Label_Graph_Variety.Visibility = Visibility.Hidden;
            }
            else
            {
                Combo_Graph_Variety.Visibility = Visibility.Visible;
                Label_Graph_Variety.Visibility = Visibility.Visible;
                Combo_Graph_Variety.ItemsSource = CommonFunc.GetChartByAnsType(SelectedTypeFullName);
                Combo_Graph_Variety.SelectedIndex = 0;
                if (Mode == "EDIT" || Mode == "COPY")
                    Combo_Graph_Variety.SelectedItem = DataGt.Graph;
            }
            var QsItems = CommonFunc.LoadQSItemsByType(CommonFunc.GetTypesByStr(SelectedTypeFullName), Workbook);           
            List_Item_Left.ItemsSource = QsItems;
            if (SelectedTypeFullName != LocalResource.FOR_MA && SelectedTypeFullName != LocalResource.FOR_N && SelectedTypeFullName != LocalResource.FOR_SA)
            {
                List_Item_Right.Visibility = Visibility.Visible;
                List_SingleItem_Right.Visibility = Visibility.Hidden;
                Grid_Button_ForMultiple.Visibility = Visibility.Visible;
                Grid_Button_ForSingle.Visibility = Visibility.Hidden;
                IsMultipleSelection = true;
                Label_Title.Visibility = Visibility.Visible;
                Table_Title.Visibility = Visibility.Visible;
            }
            else
            {
                List_Item_Right.Visibility = Visibility.Hidden;
                List_SingleItem_Right.Visibility = Visibility.Visible;
                Grid_Button_ForSingle.Visibility = Visibility.Visible;
                Grid_Button_ForMultiple.Visibility = Visibility.Hidden;
                IsMultipleSelection = false;
                Label_Title.Visibility = Visibility.Hidden;
                Table_Title.Visibility = Visibility.Hidden;
            }

            if (Mode != "" && SelectedTypeFullName != LocalResource.FOR_MA && SelectedTypeFullName != LocalResource.FOR_N && SelectedTypeFullName != LocalResource.FOR_SA)
                MoveToRight(GetMovedItem());
            else if (Mode != "")
            {
                MoveToRightSingle(DataGt.Variable);
                List_Item_Left.SelectedIndex = QsItems.IndexOf(QsItems.Where(x => x.Variable == DataGt.Variable).First());
            }
            if (IsMultipleSelection)
                List_Item_Left.SelectionMode = DataGridSelectionMode.Extended;
            else
                List_Item_Left.SelectionMode = DataGridSelectionMode.Single;
            if (Mode == "EDIT")
            {
                Command_Back.Visibility = Visibility.Visible;
                Command_Next.Visibility = Visibility.Visible;
            }
            else
            {
                Command_Back.Visibility = Visibility.Hidden;
                Command_Next.Visibility = Visibility.Hidden;
            }
            CurrentRightListConut = List_Item_Right.Items.Count;
            if (Combo_Graph_Variety.IsVisible)
                CurrentComboVal = Combo_Graph_Variety.Text;
            if (Label_Title.IsVisible)
                CurrentHeader = Label_Title.Text;
            if (List_SingleItem_Right.IsVisible)
                CurrentTextval = List_SingleItem_Right.Text;
            for(int i=0;i< List_Item_Right.Items.Count;i++)
            {
                CurrentItems.Add(List_Item_Right.Items[i] as DataGT);
            }
        }

        private List<DataGT> GetMovedItem()
        {
            List<DataGT> items = new List<DataGT>();
            for (int i = 0; i < GTS.GTTabulationItems.Count; i++)
            {
                if (GTS.GTTabulationItems[i].QuestionIndex == DataGt.QuestionIndex)
                {
                    if (DataGt.QuestionNumber > 0)
                    {
                        for (int j = 0; j < DataGt.QuestionNumber; j++, i++)
                        {
                            items.Add(GTS.GTTabulationItems[i]);
                        }
                    }
                    else
                    {
                        items.Add(GTS.GTTabulationItems[i]);
                    }
                    break;
                }
            }
            return items;
        }

        private void ButtonSingleRightArrow_Click(object sender, RoutedEventArgs e)
        {
            int c = List_Item_Left.SelectedItems.Count;
            if (c > 0)
            {
                var items = List_Item_Left.SelectedItems.Cast<DataGT>().OrderBy(x=>x.QuestionIndex).ToList();
                MoveToRight(items);
            }
        }

        private void MoveToRight(List<DataGT> items)
        {
            QC4Common.Util.FormUtil formUtil = new QC4Common.Util.FormUtil();
            int c = items.Count;
            TableHeading = string.Empty;
            QsItems.Clear();
            QsItems = CommonFunc.LoadQSItemsBySameTypeAndChoice(items[0], Workbook);          
            List_Item_Left.ItemsSource = QsItems;
            for (int i = 0; i < c; i++)
            {
                DataGT item = (items[i]);
                DataGT itm = QsItems.FirstOrDefault(x => x.Variable == item.Variable);
                if (itm != null)
                {
                    if (Label_Title.Text == "" && !string.IsNullOrEmpty(item.QSHeading) && QsRightItems.Count == 0)
                        Label_Title.Text = TableHeading = formUtil.UnEscapeCRLF(item.QSHeading);
                    if (TableHeading == "")
                        TableHeading = itm.QSHeading;
                    if(Label_Title.Text=="")
                    {
                        Label_Title.Text = TableHeading;
                        if (Mode == "EDIT")
                            Label_Title.Foreground = Brushes.LightGray;
                    }
                    QsRightItems.Add(item);
                }
            }
            items = QsRightItems.OrderBy(x => x.QuestionIndex).ToList();
            QsRightItems.Clear();
            c = items.Count;
            for (int i = 0; i < c; i++)
            {
                DataGT itm = QsItems.FirstOrDefault(x => x.Variable == items[i].Variable);
                QsRightItems.Add(itm);
                QsItems.Remove(itm);
            }
            if (QsRightItems.Count > 0)
                Command_Entry.IsEnabled = true;         
            List_Item_Left.ItemsSource = QsItems;            
            List_Item_Right.ItemsSource = QsRightItems;
        }

        private void ButtonSingleLefttArrow_Click(object sender, RoutedEventArgs e)
        {                  
            int c = List_Item_Right.SelectedItems.Count;
            if (c == 0)
                return;
            if (c== List_Item_Right.Items.Count)
            {
                QsRightItems.Clear();
                if (Mode != "EDIT")
                    Label_Title.Text = "";
                var QsItems = CommonFunc.LoadQSItemsByType(CommonFunc.GetTypesByStr(SelectedTypeFullName), Workbook);                             
                List_Item_Left.ItemsSource = QsItems;
                if (QsRightItems.Count == 0)
                    Command_Entry.IsEnabled = false;
                return;
            }
            var items = List_Item_Right.SelectedItems.Cast<DataGT>().ToList();
            for (int i = 0; i < c; i++)
            {
                DataGT item = (items[i]);
                QsRightItems.Remove(item);
                QsItems.Add(item);
            }
            items = QsItems.OrderBy(x => x.QuestionIndex).ToList();
            QsItems.Clear();
            c = items.Count;
            for (int i = 0; i < c; i++)
            {
                QsItems.Add(items[i]);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (GTS != null)
                GTS.Show();
            if (DataGt != null)
                GrossTabulationSetting.SelectedItem = DataGt;
        }

        private void Command_Entry_Click(object sender, RoutedEventArgs e)
        {
            SaveGT();
        }

        private void SaveGT()
        {
            try
            {
                List<DataGT> items;
                if (List_Item_Right.IsVisible)
                    items = List_Item_Right.Items.Cast<DataGT>().ToList();
                else
                    items = List_Item_Left.Items.Cast<DataGT>().Where(x=>x.Variable== List_SingleItem_Right.Text).ToList();
                if (Mode != "EDIT")
                {
                    int lastIndex = 0;
                    if (GTS.GTTabulationItems.Count > 0)
                        lastIndex = GTS.GTTabulationItems[GTS.GTTabulationItems.Count - 1].QuestionIndex + 1;
                    bool isChangedType = false;
                    if (GTS.GTTabulationItems.Count > 0 && (SelectedTypeFullName == LocalResource.FOR_MTM || SelectedTypeFullName == LocalResource.FOR_MTS
                        || SelectedTypeFullName == LocalResource.FOR_MTN || SelectedTypeFullName == LocalResource.FOR_RAT
                        || SelectedTypeFullName == LocalResource.FOR_RNK))
                    {
                        for (int i = 0; i < items.Count; i++)
                        {
                            if (items[i].QSType == ""&& items[i].QsNumberIndex==0)
                            {
                                isChangedType = true;
                                if (Mode == "")
                                {
                                    GTS.GTTabulationItems.Where(x => x.Variable == items[i].Variable).First().ONOFF = LocalResource.CELL_OFF_EN;
                                }
                            }
                        }
                    }

                    if (isChangedType)
                    {
                        DataGT itmGt = items[items.Count - 1];
                        if (itmGt.QuestionNumber > 0)
                        {
                            lastIndex = GTS.GTTabulationItems.Where(x => x.Variable == itmGt.Variable).First().QuestionIndex;
                            for (int i = 1; i < itmGt.QuestionNumber; i++)
                                lastIndex++;
                            lastIndex++;
                        }
                        else
                        {
                            lastIndex = itmGt.QsNumberIndex > 0 ? lastIndex : GTS.GTTabulationItems.Where(x => x.Variable == itmGt.Variable).First().QuestionIndex + 1;
                        }
                        if ((GTS.GTTabulationItems.Count)== lastIndex) {
                            AddItemToGTList(items, lastIndex);
                        } else
                        {
                            int qsIndex = 0;
                            List<DataGT> itms = new List<DataGT>();
                            for (int i = 0; i < GTS.GTTabulationItems.Count; i++)
                            {
                                if (GTS.GTTabulationItems[i].QuestionIndex == lastIndex)
                                {
                                    GTS.AddedItemIndex = lastIndex;
                                    for (int j = 0; j < items.Count; j++)
                                    {
                                        DataGT item = items[j];
                                        itms.Add(new DataGT()
                                        {
                                            QuestionIndex = qsIndex,
                                            Question = formUtil.EscapeCRLF(item.Question),
                                            QuestionVariable = item.Question == "" ? item.Variable : item.Variable + "(" + formUtil.EscapeCRLF(item.Question) + ")",
                                            Graph = Combo_Graph_Variety.IsVisible ? Combo_Graph_Variety.Text : "",
                                            ONOFF = LocalResource.CELL_ON_EN,
                                            QSType = "GT-" + CommonFunc.GetGTTypeShortByGTTypeFull(SelectedTypeFullName),
                                            Variable = item.Variable,
                                            CategoryCount = item.CategoryCount,
                                            QuestionNumber = j == 0 ? items.Count == 1 ? 0 : items.Count : 0,
                                            QSTypeShort = item.QSTypeShort,
                                            QsTypePlusCatCount = item.CategoryCount > 0 ? item.QSTypeShort + "/" + item.CategoryCount : item.QSTypeShort,
                                            QsNumberIndex = j,
                                            QSHeading = Label_Title.IsVisible ? formUtil.EscapeCRLF(Label_Title.Text) : "",
                                            GTFlag = "0"
                                        });
                                        qsIndex++;
                                    }
                                    DataGT itm = GTS.GTTabulationItems[i];
                                    itm.QuestionIndex = qsIndex;
                                    itms.Add(itm);
                                    qsIndex++;
                                }
                                else
                                {
                                    DataGT itm = GTS.GTTabulationItems[i];
                                    itm.QuestionIndex = qsIndex;
                                    itms.Add(itm);
                                    qsIndex++;
                                }
                            }
                            GTS.GTTabulationItems.Clear();
                            GTS.GTTabulationItems = itms;
                        }
                    }
                    else
                    {
                        AddItemToGTList(items, lastIndex);
                    }                    
                }
                else
                {
                    int qsIndex = 0;
                    int editIndex = DataGt.QuestionIndex;
                    List<DataGT> itms = new List<DataGT>();
                    for (int i = 0; i < GTS.GTTabulationItems.Count; i++)
                    {
                        if (GTS.GTTabulationItems[i].QuestionIndex == editIndex)
                        {
                            int qsNum = GTS.GTTabulationItems[i].QuestionNumber;
                            if (qsNum > 0)
                            {
                                for (int j = 1; j < qsNum; j++)
                                    i++;
                            }
                            for (int j = 0; j < items.Count; j++)
                            {
                                DataGT item = items[j];
                                itms.Add(new DataGT()
                                {
                                    QuestionIndex = qsIndex,
                                    Question = formUtil.EscapeCRLF(item.Question),
                                    QuestionVariable = item.Question == "" ? item.Variable : item.Variable + "(" + formUtil.EscapeCRLF(item.Question) + ")",
                                    Graph = Combo_Graph_Variety.IsVisible ? Combo_Graph_Variety.Text : "",
                                    ONOFF = LocalResource.CELL_ON_EN,
                                    QSType = DataGt.QSType,
                                    Variable = item.Variable,
                                    CategoryCount = item.CategoryCount,
                                    QuestionNumber = j == 0 ? items.Count == 1 ? 0 : items.Count : 0,
                                    QSTypeShort = item.QSTypeShort,
                                    QsTypePlusCatCount = item.CategoryCount > 0 ? item.QSTypeShort + "/" + item.CategoryCount : item.QSTypeShort,
                                    QsNumberIndex = j,
                                    QSHeading = Label_Title.IsVisible ? formUtil.EscapeCRLF(Label_Title.Text) : "",
                                    GTFlag = "1",
                                    Test= DataGt.Test
                                });
                                qsIndex++;
                            }
                        }
                        else
                        {
                            DataGT itm = GTS.GTTabulationItems[i];
                            itm.QuestionIndex = qsIndex;
                            itms.Add(itm);
                            qsIndex++;
                        }
                    }
                    GTS.GTTabulationItems.Clear();
                    GTS.GTTabulationItems = itms;
                    GTS.LoadList();
                    DataGt = GTS._dataExport_LBVariablesForGT.Where(x => x.QuestionIndex == DataGt.QuestionIndex).First();
                    if (Label_Title.Text == "")
                    {
                        Label_Title.Text = TableHeading;
                        Label_Title.Foreground = Brushes.LightGray;
                    }
                    else
                        Label_Title.Foreground = Brushes.Black;
                }
                if (!IsClosing)
                    MessageDialog.Info(LocalResource.GT_TABULATION_SAVE);

                IsClosing = false;
                CurrentRightListConut = List_Item_Right.Items.Count;
                if (Combo_Graph_Variety.IsVisible)
                    CurrentComboVal = Combo_Graph_Variety.Text;
                if (Label_Title.IsVisible)
                    CurrentHeader = Label_Title.Text; 
                if (List_SingleItem_Right.IsVisible)
                    CurrentTextval = List_SingleItem_Right.Text;
                for (int i = 0; i < List_Item_Right.Items.Count; i++)
                {
                    CurrentItems.Add(List_Item_Right.Items[i] as DataGT);
                }
                if (Mode != "EDIT")
                    this.Close();
            }
            catch (Exception ex)
            {

            }
        }

        private void AddItemToGTList(List<DataGT> items, int lastIndex)
        {
            for (int t = 0; t < items.Count; t++)
            {
                DataGT item = items[t];
                if (t == 0)
                {
                    GTS.AddedItemIndex = lastIndex;
                    GTS.GTTabulationItems.Add(new DataGT()
                    {
                        QuestionIndex = lastIndex,
                        Question = formUtil.EscapeCRLF(item.Question),
                        QuestionVariable = item.Question == "" ? item.Variable : item.Variable + "(" + formUtil.EscapeCRLF(item.Question) + ")",
                        Graph = Combo_Graph_Variety.IsVisible ? Combo_Graph_Variety.Text : "",
                        ONOFF = LocalResource.CELL_ON_EN,
                        QSType = "GT-" + CommonFunc.GetGTTypeShortByGTTypeFull(SelectedTypeFullName),
                        Variable = item.Variable,
                        CategoryCount = item.CategoryCount,
                        QuestionNumber = items.Count == 1 ? 0 : items.Count,
                        QSTypeShort = item.QSTypeShort,
                        QsTypePlusCatCount = item.CategoryCount > 0 ? item.QSTypeShort + "/" + item.CategoryCount : item.QSTypeShort,
                        QsNumberIndex = t,
                        QSHeading = Label_Title.IsVisible ? formUtil.EscapeCRLF(Label_Title.Text) : "",
                        GTFlag = "0"
                    });
                    lastIndex++;
                }
                else
                {
                    for (int j = 1; j < items.Count; j++)
                    {
                        item = items[t];
                        GTS.GTTabulationItems.Add(new DataGT()
                        {
                            QuestionIndex = lastIndex,
                            Question = item.Question,
                            QuestionVariable = item.Question,
                            Graph = Combo_Graph_Variety.IsVisible ? Combo_Graph_Variety.Text : "",
                            ONOFF = LocalResource.CELL_ON_EN,
                            QSType = "GT-" + CommonFunc.GetGTTypeShortByGTTypeFull(SelectedTypeFullName),
                            Variable = item.Variable,
                            CategoryCount = item.CategoryCount,
                            QuestionNumber = 0,
                            QSTypeShort = item.QSTypeShort,
                            QsTypePlusCatCount = item.CategoryCount > 0 ? item.QSTypeShort + "/" + item.CategoryCount : item.QSTypeShort,
                            QsNumberIndex = t,
                            QSHeading = Label_Title.IsVisible ? Label_Title.Text : "",
                            GTFlag = "1"
                        });
                        lastIndex++;
                        t++;
                    }
                }
            }
        }

        private void ButtonSingleRightArrow2_Click(object sender, RoutedEventArgs e)
        {
            if (List_Item_Left.SelectedItem != null)
            {
                DataGT item = List_Item_Left.SelectedItem as DataGT;
                MoveToRightSingle(item.Variable);
            }
        }

        private void MoveToRightSingle(string variable)
        {
            List_SingleItem_Right.Text = variable;
            Command_Entry.IsEnabled = true;
        }

        private void ButtonSingleLefttArrow2_Click(object sender, RoutedEventArgs e)
        {          
            MoveToLeftSingle();
        }

        private void MoveToLeftSingle()
        {
            List_SingleItem_Right.Text = "";
            Command_Entry.IsEnabled = false;
        }

        private void Command_Back_Click(object sender, RoutedEventArgs e)
        {
            if (Mode == "EDIT")
            {
                bool isDataExist = false;
                if (List_Item_Right.IsVisible && List_Item_Right.Items.Count > 0)
                    isDataExist = true;
                else if (List_SingleItem_Right.IsVisible && List_SingleItem_Right.Text != "")
                    isDataExist = true;

                if (isDataExist && ((CurrentRightListConut != List_Item_Right.Items.Count || NotMatchItems()) ||
                    (Combo_Graph_Variety.IsVisible && CurrentComboVal != Combo_Graph_Variety.Text) ||
                    (Label_Title.IsVisible && CurrentHeader != Label_Title.Text) ||
                    (CurrentTextval != List_SingleItem_Right.Text)))
                {
                    System.Windows.Forms.DialogResult res = Util.MessageDialog.InfoYesNo(LocalResource.GT_TABULATION_SAVE_WARNING,System.Windows.Forms.MessageBoxIcon.Question);
                    if (res == System.Windows.Forms.DialogResult.Yes)
                    {
                        IsClosing = true;
                        SaveGT();
                    }
                }
            }
            Label_Title.Text = "";
            int itemIndx = GTS._dataExport_LBVariablesForGT.IndexOf(DataGt) - 1;
            if (itemIndx == 0)
                Command_Back.IsEnabled = false;
            else
                Command_Back.IsEnabled = true;

            DataGt = GTS._dataExport_LBVariablesForGT[itemIndx];
            SelectedTypeFullName = CommonFunc.GetFullTypeNameByAnsType(DataGt.QSType);
            QsItems.Clear();
            QsRightItems.Clear();
            InitalSettings();
        }

        private bool NotMatchItems()
        {

            for (int i = 0; i < List_Item_Right.Items.Count; i++)
            {
                if(!CurrentItems.Any(x => x == (List_Item_Right.Items[i] as DataGT)))
                    return true;
            }
            return false;
        }

        private void Command_Next_Click(object sender, RoutedEventArgs e)
        {
            if (Mode == "EDIT")
            {
                bool isDataExist = false;
                if (List_Item_Right.IsVisible && List_Item_Right.Items.Count > 0)
                    isDataExist = true;
                else if(List_SingleItem_Right.IsVisible && List_SingleItem_Right.Text != "")
                    isDataExist = true;

                if (isDataExist && ((CurrentRightListConut != List_Item_Right.Items.Count || NotMatchItems()) ||
                    (Combo_Graph_Variety.IsVisible && CurrentComboVal != Combo_Graph_Variety.Text) ||
                    (Label_Title.IsVisible && CurrentHeader != Label_Title.Text) || 
                    (CurrentTextval != List_SingleItem_Right.Text)))
                {
                    System.Windows.Forms.DialogResult res = Util.MessageDialog.InfoYesNo(LocalResource.GT_TABULATION_SAVE_WARNING, System.Windows.Forms.MessageBoxIcon.Question);
                    if (res == System.Windows.Forms.DialogResult.Yes)
                    {
                        IsClosing = true;
                        SaveGT();
                    }
                }
            }
            Label_Title.Text = "";
            int itemIndx = GTS._dataExport_LBVariablesForGT.IndexOf(DataGt) + 1;
            if ((itemIndx + 1) == GTS._dataExport_LBVariablesForGT.Count)
                Command_Next.IsEnabled = false;
            else
                Command_Next.IsEnabled = true;

            DataGt = GTS._dataExport_LBVariablesForGT[itemIndx];
            SelectedTypeFullName = CommonFunc.GetFullTypeNameByAnsType(DataGt.QSType);
            QsItems.Clear();
            QsRightItems.Clear();
            InitalSettings();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Mode == "EDIT")
            {
                bool isDataExist = false;
                if (List_Item_Right.IsVisible && List_Item_Right.Items.Count > 0)
                    isDataExist = true;
                else if (List_SingleItem_Right.IsVisible && List_SingleItem_Right.Text != "")
                    isDataExist = true;

                if (isDataExist && ((CurrentRightListConut != List_Item_Right.Items.Count || NotMatchItems()) ||
                    (Combo_Graph_Variety.IsVisible && CurrentComboVal != Combo_Graph_Variety.Text) ||
                    (Label_Title.IsVisible && CurrentHeader != Label_Title.Text) ||
                    (CurrentTextval != List_SingleItem_Right.Text)))
                {
                    System.Windows.Forms.DialogResult res = Util.MessageDialog.InfoYesNo(LocalResource.GT_TABULATION_SAVE_WARNING, System.Windows.Forms.MessageBoxIcon.Question);
                    if (res == System.Windows.Forms.DialogResult.Yes)
                    {
                        IsClosing = true;
                        SaveGT();
                    }
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
