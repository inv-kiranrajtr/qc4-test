using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace KeyGeneration.Util
{
    public class DataGridHelper
    {
        public T FindVisualChildByName<T>(DependencyObject parent, string name) where T : DependencyObject
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
                    return columnHeader;
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

        public void SetBgColorForColumn(ref DataGrid dataGrid, int columnIndex, Brush bgColor)
        {
            try
            {
                //For Body
                Style s = new Style(typeof(DataGridCell));
                s.Setters.Add(new Setter(DataGridCell.BackgroundProperty, bgColor));
                dataGrid.Columns[columnIndex].CellStyle = s;

                //For Head
                DataGridHelper dataGridHelper = new DataGridHelper();
                DataGridColumnHeader dataGridColumnHeader = dataGridHelper.GetColumnHeaderFromColumn(dataGrid, dataGrid.Columns[columnIndex]);
                dataGridColumnHeader.Background = bgColor;
            }
            catch (Exception ex)
            {
                
            }
        }

        public int GetDatagridRowIndex(MouseButtonEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            //Stepping through the visual tree
            while ((dep != null) && !(dep is DataGridCell))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }
            //Is the dep a cell or outside the bounds of Window1?
            if (dep == null | !(dep is DataGridCell))
            {
                return 0;
            }
            else
            {
                DataGridCell cell = new DataGridCell();
                cell = (DataGridCell)dep;
                while ((dep != null) && !(dep is DataGridRow))
                {
                    dep = VisualTreeHelper.GetParent(dep);
                }

                if (dep == null)
                {
                    return 0;
                }
            }
            DataGridRow row = dep as DataGridRow;

            DataGrid dataGrid = ItemsControl.ItemsControlFromItemContainer(row) as System.Windows.Controls.DataGrid;
            int rowindex = dataGrid.ItemContainerGenerator.IndexFromContainer(row); //this returns ROW INDEX
            return rowindex;
        }

    }
}
