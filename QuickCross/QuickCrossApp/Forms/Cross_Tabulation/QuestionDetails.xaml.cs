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

namespace Qc4Launcher.Forms.Cross_Tabulation
{
    /// <summary>
    /// Interaction logic for QuestionDetails.xaml
    /// </summary>
    public partial class QuestionDetails : Window
    {
        public List<CrossQuestionSetting> LstQststng { get; set; }
        public QuestionDetails(TabContent cont ,List<CrossQuestionSetting> QstnSettinglist)
        {
            InitializeComponent();
            //data_grid.IsHitTestVisible = false;
           // this.Topmost = true;
            if (QstnSettinglist != null)
            {

            }
            LstQststng = QstnSettinglist;
            DataContext = LstQststng;
            SelectChoicesFromData(LstQststng);
            //data_grid.RowBackground= (SolidColorBrush)(new BrushConverter().ConvertFrom("#F7F7F7"));
        }
        public List<ListClass> SelectChoicesFromData(List<CrossQuestionSetting> list)
        {
            List<ListClass> list_choices = new List<ListClass>();
            foreach(var item in list)
            {
                int count = 0;
                foreach(var choices in item.Choices)
                {
                    count++;
                    ListClass ls = new ListClass();
                    ls.choice = choices;
                    ls.slno = count;
                    list_choices.Add(ls);
                }
            }
            data_grid.ItemsSource = list_choices;
            return list_choices;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Data_grid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            //e.Row.Header = (e.Row.GetIndex() + 1).ToString();
            //var x = data_grid.CurrentCell;
        }

       
    }
    public class ListClass
    {
        public int slno { get; set; }
        public string choice { get; set; }
    }
}
