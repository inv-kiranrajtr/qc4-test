using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Qc4Launcher.Util
{
    class FindControls
    {

        //only selected tab child controls r returned
        public static void EnumVisual(Visual parent, List<Visual> collection)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {

                Visual childVisual = (Visual)VisualTreeHelper.GetChild(parent, i);


                if (childVisual is System.Windows.Controls.TextBox || childVisual is System.Windows.Controls.TextBlock ||
                    childVisual is System.Windows.Controls.ListBox || childVisual is System.Windows.Controls.ListBoxItem ||
                    childVisual is System.Windows.Controls.RadioButton || childVisual is System.Windows.Controls.CheckBox ||
                    childVisual is System.Windows.Controls.ComboBox || childVisual is System.Windows.Controls.ListBox ||
                    childVisual is System.Windows.Controls.Button || childVisual is System.Windows.Controls.RichTextBox )
                    collection.Add(childVisual);


                EnumVisual(childVisual, collection);
            }
            //return collection;
        }
      //  finding all controls of  given type(textbox/combo/radio)
        public static IEnumerable<T> FindLogicalChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                foreach (object rawChild in LogicalTreeHelper.GetChildren(depObj))
                {
                    if (rawChild is DependencyObject)
                    {
                        DependencyObject child = (DependencyObject)rawChild;
                        if (child is T)
                        {
                            yield return (T)child;
                        }

                        foreach (T childOfChild in FindLogicalChildren<T>(child))
                        {
                            yield return childOfChild;
                        }
                    }
                }
            }
        }
    }
}
