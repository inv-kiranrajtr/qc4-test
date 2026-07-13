using Qc4Launcher.Util;
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
using System.Windows.Shapes;

namespace Qc4Launcher.Forms
{
	/// <summary>
	/// Interaction logic for FilterSettingValue.xaml
	/// </summary>
	public partial class FilterSettingValue : Window
	{
		int ChoiceCount = 0;
		public string CurrentValue { get; set; }
        List<filterClass> listdata = new List<filterClass>();
        QC4Common.Util.FormUtil formUtil = new QC4Common.Util.FormUtil();

        public FilterSettingValue(List<string> val, string CQ1, String CQ2)
		{
			InitializeComponent();
			ChoiceCount = val.Count();

			for (int i = 0; i < ChoiceCount; i++)

			{
                filterClass cd = new filterClass();
                cd.count = i + 1;
                cd.item = (i + 1 + ": " + formUtil.EscapeCRLF(val[i]));
                listdata.Add(cd);

				//valueListBox.Items.Add(i + 1 + ": " + val[i]);
			}
            listdata.Add(new filterClass { item = CQ1 });
            listdata.Add(new filterClass { item = CQ2 });
            //valueListBox.Items.Add(CQ1);
            //valueListBox.Items.Add(CQ2);
            valueListBox.ItemsSource = listdata;
			this.ok_button.IsEnabled = false;


		}

        private void SelectData()
        {
            try
            {
                if (this.valueListBox.SelectedIndex < 0)
                {
                    this.ok_button.IsEnabled = false;
                    return;
                }

                if (valueListBox.SelectedItems.Count > 1 &&
                        (valueListBox.SelectedItems.Cast<filterClass>().Any(x=>x.item== LocalResource.TEXT_FILTER_VALUE_DK_NO_ANSWER)
                        || valueListBox.SelectedItems.Cast<filterClass>().Any(x => x.item == LocalResource.TEXT_FILTER_VALUE_STAR_EXCLUDED)))
                {
                    MessageDialog.ErrorOk(LocalResource.ALERT_FS_SAME_TOGETHER);
                    return;
                }
                //  _dataExport_LBVariablesToExport[List_Source.SelectedIndex].AnswerType

                if (valueListBox.SelectedItems.Count == 1)
                {
                    if (listdata[valueListBox.SelectedIndex].item.Contains(LocalResource.TEXT_FILTER_VALUE_DK_NO_ANSWER))
                    {
                        CurrentValue = "DK";
                        this.Close();
                        return;
                    }
                    else if (listdata[valueListBox.SelectedIndex].item.Contains(LocalResource.TEXT_FILTER_VALUE_STAR_EXCLUDED))
                    {
                        CurrentValue = "*";
                        this.Close();
                        return;
                    }
                    //if (valueListBox.SelectedItems.Contains(LocalResource.TEXT_FILTER_VALUE_DK_NO_ANSWER))
                    //{
                    //    CurrentValue = "DK";
                    //    this.Close();
                    //    return;
                    //}
                    //else if (valueListBox.SelectedItems.Contains(LocalResource.TEXT_FILTER_VALUE_STAR_EXCLUDED))
                    //{
                    //    CurrentValue = "*";
                    //    this.Close();
                    //    return;
                    //}
                }

                if (this.valueListBox.SelectedItems.Count != 1)
                {
                    int[] a = new int[valueListBox.SelectedItems.Count];
                    int j = 0;
                    bool skip = false;
                    bool set = false;

                    foreach (filterClass data in valueListBox.SelectedItems)
                    {
                        if (data.item != LocalResource.TEXT_FILTER_VALUE_DK_NO_ANSWER && data.item != LocalResource.TEXT_FILTER_VALUE_STAR_EXCLUDED)
                        {
                            a[j++] = Convert.ToInt32(data.item.ToString().Split(':')[0]);
                            set = true;
                        }
                        else
                        {
                            CurrentValue = data.item;
                            skip = true;
                            break;
                        }
                    }


                    //foreach (var item in valueListBox.SelectedItems)
                    //{

                    //	if (item.ToString() !=LocalResource.TEXT_FILTER_VALUE_DK_NO_ANSWER && item.ToString() != LocalResource.TEXT_FILTER_VALUE_STAR_EXCLUDED)
                    //	{
                    //		a[j++] = Convert.ToInt32(item.ToString().Split(':')[0]);
                    //		set = true;
                    //	}
                    //	else
                    //	{
                    //		CurrentValue = valueListBox.SelectedItem.ToString();
                    //		skip = true;
                    //		break;
                    //	}
                    //}

                    Array.Sort(a);
                    int value1 = a[0];
                    string startValue = a[0].ToString();
                    string val1 = "";//startValue;
                    string val2 = startValue;
                    int listCount = valueListBox.SelectedItems.Count;
                    if (!skip && set)
                    {
                        for (int k = 0; k < listCount; k++)
                        {
                            List<int> startVal = new List<int>();
                            int s = k;
                            for (int i = s; i < listCount - 1 && a[i] + 1 == a[k + 1]; i++, k++)
                            {
                                startVal.Add(a[k]);
                            }
                            int endVal = a[k];
                            if (startVal.Count > 0)
                            {
                                if (val1 == "")
                                    val1 = startVal[0] + "-" + endVal;
                                else
                                    val1 += "/" + startVal[0] + "-" + endVal;
                            }
                            else
                            {
                                if (val1 == "")
                                    val1 += endVal;
                                else
                                    val1 += "/" + endVal;
                            }

                        }
                        CurrentValue = val1;
                    }
                    else if (skip && set)
                    {
                        CurrentValue = "";
                        MessageBox.Show(LocalResource.FILTERSETTINGS_WRONG_ENTRY, "QuickCross");
                    }
                    else if (skip && !set)
                    {
                        CurrentValue = valueListBox.SelectedItem.ToString();
                    }
                }
                else
                {
                    CurrentValue = listdata[valueListBox.SelectedIndex].item.Split(':')[0];
                }

                this.Close();
            }
            catch
            {
                MessageBox.Show(LocalResource.ALERT_FS_SAME_TOGETHER, "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Ok_button_Click(object sender, RoutedEventArgs e)
        {
            SelectData();
        }

		private void Cancel_button_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void ValueListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			this.ok_button.IsEnabled = true;
		}

        private void ValueListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectData();
        }

        private void valueListBox_CurrentCellChanged(object sender, EventArgs e)
        {
            var dg = (DataGrid)sender;
            dg.Focus();
        }
    }
    public class filterClass
    {
        public int count { get; set; }
        public string item { get; set; }
    }
}
