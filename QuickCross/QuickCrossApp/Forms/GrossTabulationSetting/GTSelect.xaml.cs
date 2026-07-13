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
using FilterSettingsView;
using static FilterSettingsView.FilterSettingsClass;

namespace Qc4Launcher.Forms.GrossTabulationSetting
{
    /// <summary>
    /// Interaction logic for GTSelect.xaml
    /// </summary>
    public partial class GTSelect : Window
    {
        GrossTabulationSetting GTS { get; set; }
        Microsoft.Office.Interop.Excel.Workbook Workbook;
        public GTSelect(GrossTabulationSetting grossTabulationSetting, Microsoft.Office.Interop.Excel.Workbook _workbook)
        {
            Workbook = _workbook;
            GTS = grossTabulationSetting;
            InitializeComponent();
        }

        private void Command_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Command_Entry_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            AddGTSettings addGTSettings = new AddGTSettings((Combo_Summary_Variety.SelectedItem as DataGT).QSType, GTS, Workbook,"");
            addGTSettings.ShowDialog();
        }

        private void Combo_Summary_Variety_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Combo_Summary_Variety.SelectedIndex >= 0)
                Command_Entry.IsEnabled = true;
            else
                Command_Entry.IsEnabled = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<DataGT> tableTypes = GetTableTypes();
            Combo_Summary_Variety.ItemsSource = tableTypes;
        }

        private List<DataGT> GetTableTypes()
        {
            return new List<DataGT>()
            {
                new DataGT {QSType= LocalResource.FOR_SA,QSTypeShort= Util.Constants.GT.GTSA },
                new DataGT {QSType= LocalResource.FOR_MA,QSTypeShort= Util.Constants.GT.GTMA },
                new DataGT {QSType= LocalResource.FOR_N,QSTypeShort= Util.Constants.GT.GTN },
                new DataGT {QSType= LocalResource.FOR_MTM,QSTypeShort= Util.Constants.GT.GTMTM },
                new DataGT {QSType= LocalResource.FOR_MTS,QSTypeShort= Util.Constants.GT.GTMTS },
                new DataGT {QSType= LocalResource.FOR_MTN,QSTypeShort= Util.Constants.GT.GTMTN },
                new DataGT {QSType= LocalResource.FOR_RAT,QSTypeShort= Util.Constants.GT.GTRAT },
                new DataGT {QSType= LocalResource.FOR_RNK,QSTypeShort= Util.Constants.GT.GTRNK }
            };
        }

        private void Window_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }
}
