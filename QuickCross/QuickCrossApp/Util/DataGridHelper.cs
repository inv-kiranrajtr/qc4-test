using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Data;
using System.Windows.Data;

namespace Qc4Launcher.Util
{
    public class DataGridHelper
    {
        public static bool ischecked = false;
        public static List<int> indecesList = new List<int>();
        public static int columnIndex = -1;
        static DataGridColumnHeader NullHeader = null;
        public T FindVisualChildByName<T>(DependencyObject parent, string name, CheckBox chkSelectAll=null,string purpose="") where T : DependencyObject
        {
            if (purpose== "check")
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
                {
                    var child = VisualTreeHelper.GetChild(parent, i);
                    if (child is CheckBox)
                    {
                        CheckBox chkGridColumn = child as CheckBox;
                        if (chkSelectAll.IsChecked == true)
                        {
                            chkGridColumn.IsChecked = true;
                        }
                        else
                        {
                            chkGridColumn.IsChecked = false;
                        }
                    }
                    else
                    {
                        T result = FindVisualChildByName<T>(child, name, chkSelectAll, "check");
                        if (result != null && result is CheckBox)
                        {
                            CheckBox chkGridColumn = result as CheckBox;
                            if (chkSelectAll.IsChecked == true)
                            {
                                chkGridColumn.IsChecked = true;
                            }
                            else
                            {
                                chkGridColumn.IsChecked = false;
                            }
                        }
                    }
                }
            }
            else if (purpose == "uncheck")
            {
                if (parent is DataGridColumnHeader)
                    NullHeader = (parent as DataGridColumnHeader);
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
                {
                    var child = VisualTreeHelper.GetChild(parent, i);
                    if (child is CheckBox)
                    {
                        CheckBox chkGridColumn = child as CheckBox;
                        if (chkGridColumn.IsChecked == true)
                        {
                            ischecked = true;
                            return null;
                        }
                    }
                    else
                    {
                        T result = FindVisualChildByName<T>(child, name,null, "uncheck");
                        if (result != null && result is CheckBox)
                        {
                            CheckBox chkGridColumn = result as CheckBox;
                            if (chkGridColumn.IsChecked == true)
                            {
                                ischecked = true;
                                return null;
                            }
                        }
                    }
                }

                if (NullHeader!=null&& NullHeader.Column == null)
                {
                    NullHeader.Visibility = Visibility.Hidden;
                }
                else if(NullHeader != null)
                {
                    NullHeader.Visibility = Visibility.Visible;
                }
            }
            else if (purpose == "addIndices")
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
                {
                    var child = VisualTreeHelper.GetChild(parent, i);
                    if (child is CheckBox)
                    {
                        CheckBox chkGridColumn = child as CheckBox;
                        if (chkGridColumn.IsChecked == true)
                        {
                            ischecked = true;
                            indecesList.Add(columnIndex);
                        }
                        columnIndex++;
                    }
                    else
                    {
                        T result = FindVisualChildByName<T>(child, name, null, "addIndices");
                        if (result != null && result is CheckBox)
                        {
                            CheckBox chkGridColumn = result as CheckBox;
                            if (chkGridColumn.IsChecked == true)
                            {
                                ischecked = true;
                                indecesList.Add(columnIndex);
                            }
                            columnIndex++;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
                {
                    var child = VisualTreeHelper.GetChild(parent, i);
                    string controlName = child.GetValue(Control.NameProperty) as string;
                    if (controlName == name)
                    {
                        return child as T;
                    }
                    else
                    {
                        T result = FindVisualChildByName<T>(child, name);
                        if (result != null)
                            return result;
                    }
                }
            }
            return null;
        }

        public DataGridColumnHeader GetColumnHeaderFromColumn(DataGrid dataGrid, DataGridColumn column)
        {
            // dataGrid is the name of your DataGrid. In this case Name="dataGrid"
            List<DataGridColumnHeader> columnHeaders = GetVisualChildCollection<DataGridColumnHeader>(dataGrid);
            foreach (DataGridColumnHeader columnHeader in columnHeaders)
            {
                if (columnHeader.Column == column)
                {
                    columnHeader.Visibility = Visibility.Visible;
                    return columnHeader;
                }
                if(columnHeader.Column==null)
                {
                    columnHeader.Visibility = Visibility.Hidden;
                }
            }
            return null;
        }

        private List<T> GetVisualChildCollection<T>(object parent) where T : Visual
        {
            List<T> visualCollection = new List<T>();
            GetVisualChildCollection(parent as DependencyObject, visualCollection);
            return visualCollection;
        }

        private void GetVisualChildCollection<T>(DependencyObject parent, List<T> visualCollection) where T : Visual
        {
            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is T)
                {
                    visualCollection.Add(child as T);
                }
                else if (child != null)
                {
                    GetVisualChildCollection(child, visualCollection);
                }
            }
        }

        public void SetBgColorForColumn(DataGrid dataGrid, int columnIndex, Brush bgColor, DataGridColumnHeader dataGridColumnHeader)
        {
            try
            {
                //For Body
                Style s = new Style(typeof(DataGridCell));
                s.Setters.Add(new Setter(DataGridCell.BackgroundProperty, bgColor));
                s.Setters.Add(new Setter(DataGridCell.ForegroundProperty, Brushes.Black));
                dataGrid.Columns[columnIndex].CellStyle = s;

                //For Head
                //DataGridHelper dataGridHelper = new DataGridHelper();
                //DataGridColumnHeader dataGridColumnHeader = dataGridHelper.GetColumnHeaderFromColumn(dataGrid, dataGrid.Columns[columnIndex]);
                dataGridColumnHeader.Background = bgColor;
            }
            catch (Exception ex)
            {

            }
        }

        public void BindDataGrid(ref DataGrid dataGrid, DataTable dataTable)
        {
            dataTable = dataTable.Copy();
            FileUtil utl = new FileUtil();
            List<string> columnNames = new List<string>();
            foreach (DataColumn dataColumn in dataTable.Columns)
            {
                columnNames.Add(dataColumn.ColumnName.Replace("_", "__"));
                dataColumn.ColumnName = utl.GetNewColumnName(dataTable);
            }
            dataGrid.DataContext = dataTable;

            for (int i = 0; i <= dataGrid.Columns.Count - 1; i++)
                dataGrid.Columns[i].Header = columnNames[i];

            try
            {
                CollectionViewSource.GetDefaultView(dataGrid.ItemsSource).Refresh();
            }
            catch (Exception ex)
            {

            }
        }

        public void DisableRows(ref DataGrid dataGrid)
        {
            for (int i = 0; i <= dataGrid.Items.Count - 1; i++)
            {
                DataGridRow dataGridRow = dataGrid.ItemContainerGenerator.ContainerFromItem(dataGrid.Items[i]) as DataGridRow;
                dataGridRow.IsEnabled = false;
            }

            try
            {
                CollectionViewSource.GetDefaultView(dataGrid.ItemsSource).Refresh();
            }
            catch (Exception ex)
            {

            }
        }

    }
}
