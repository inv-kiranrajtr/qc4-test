using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QC4Common.Util
{
    public class GridCommonFunc
    {
        public static void Arrow(object sender, KeyEventArgs e)
        {
            if (!(sender is DataGrid))
                return;
            DataGrid grid = sender as DataGrid;
            Style style = null;
            if (e.Key == Key.Tab)
                style = Application.Current.FindResource("MyFocusVisual") as Style;
            grid.FocusVisualStyle = style;
            if (grid.SelectedIndex >= 0)
            {
                var data = grid.SelectedItems;
                int index = 0;
                if (e.Key == Key.Down)
                {
                    if (data.Count > 1)
                    {
                        for (int i = 0; i < data.Count; i++)
                        {
                            int indx = grid.Items.IndexOf(data[i]);
                            if (index < indx)
                            {
                                index = indx;
                            }
                        }
                    }
                    else
                        index = grid.Items.IndexOf(data[0]);
                    index += 1;
                    if (grid.Items.Count > index)
                    {
                        DataGridRow row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromIndex(index);
                        if (row != null)
                            row.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                        grid.SelectedItem = grid.Items[index];
                    }
                    e.Handled = true;
                }
                else if (e.Key == Key.Up)
                {
                    index = grid.Items.IndexOf(data[0]);
                    for (int i = 0; i < data.Count; i++)
                    {
                        int indx = grid.Items.IndexOf(data[i]);
                        if (index > indx)
                        {
                            index = indx;
                        }
                    }
                    DataGridRow row = null;
                    if (index > 0)
                    {
                        index -= 1;
                        row = (System.Windows.Controls.DataGridRow)grid.ItemContainerGenerator.ContainerFromIndex(index);
                        if (row != null)
                        {
                            row.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                        }
                        else
                        {
                            var uiElement = e.OriginalSource as UIElement;
                            grid.Focus();
                            uiElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                        }
                        grid.SelectedItem = grid.Items[index];
                    }
                    if (row != null)
                        e.Handled = true;
                }
            }
        }
        
    }
}
