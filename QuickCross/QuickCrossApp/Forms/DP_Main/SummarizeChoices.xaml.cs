using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
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

namespace Qc4Launcher.Forms.DP_Main
{
    /// <summary>
    /// Interaction logic for SummarizeChoices.xaml
    /// </summary>
    public partial class SummarizeChoices : Window
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private ObservableCollection<Choice> choices = new ObservableCollection<Choice>();
        public ObservableCollection<Choice> Choices
        {
            get
            {
                return choices;
            }
            set
            {
                choices = value;
            }
        }
        private Choice selectedChoice;
        private SummarizedChoiceDetails summarizedChoice;

        public SummarizedChoiceDetails SummarizedChoice
        {
            get
            {
                return summarizedChoice;
            }
            set
            {
                summarizedChoice = value;
            }
        }

        public Choice SelectedChoice
        {
            get { return selectedChoice; }
            set { selectedChoice = value; }
        }

        public SummarizeChoices(DataTable choices)
        {
            InitializeComponent();
            if (choices != null)
            {
                List<string> positions = new List<string>();
                positions.Add(LocalResource.LBL_START);
                positions.Add(LocalResource.LBL_END);
                Choices = new ObservableCollection<Choice>();
                for (int i = 1; i <= choices.Rows.Count; i++)
                {
                    ///paramList[i] = dtCriteria.Rows[i - 1][0].ToString();
                    Choices.Add(new Choice() { Id = i, ChoiceValue = choices.Rows[i-1][1].ToString() });
                }
                Combo_Top.ItemsSource = Choices;
                Combo_Bottom.ItemsSource = Choices;
                summarizedChoice = new SummarizedChoiceDetails();
                summarizedChoice.IsIncludeSourceChoices = true;
                summarizedChoice.IsTop = true;
                summarizedChoice.IsBottom = false;
                summarizedChoice.TopSelectedPosition = LocalResource.LBL_END;
                summarizedChoice.BottomSelectedPosition =LocalResource.LBL_END;
                data_grid_summarizeChoices.DataContext = summarizedChoice;
                data_grid_summarizeChoicesDetails.DataContext = summarizedChoice;
                Combo_top_position.ItemsSource = positions;
                Combo_bottom_position.ItemsSource = positions;
            }
        }

        private void Btn_closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            summarizedChoice.IsClosed = true;
        }
        public class Choice
        {

            private int _id;

            public int Id
            {
                get { return _id; }
                set { _id = value; }
            }
            private string _choiceValue;

            public string ChoiceValue
            {
                get { return _choiceValue; }
                set { _choiceValue = value; }
            }
        }
        public class SummarizedChoiceDetails
        {
            private bool isIncludeSourceChoices;
            private bool isTop;
            private bool isBottom;
            private bool isClosed;
            private bool isOk = false;
            private string topSelectedChoice;
            private string bottomSelectedChoice;
            private string topSelectedPosition;
            private string bottomSelectedPosition;

            public bool IsIncludeSourceChoices
            {
                get
                {
                    return isIncludeSourceChoices;
                }
                set
                {
                    isIncludeSourceChoices = value;
                }
            }
            public bool IsTop
            {
                get
                {
                    return isTop;
                }
                set
                {
                    isTop = value;
                }
            }
            public bool IsBottom
            {
                get
                {
                    return isBottom;
                }
                set
                {
                    isBottom = value;
                }
            }
            public bool IsClosed
            {
                get
                {
                    return isClosed;
                }
                set
                {
                    isClosed = value;
                }
            }
            public bool IsOk
            {
                get
                {
                    return isOk;
                }
                set
                {
                    isOk = value;
                }
            }
            public string TopSelectedChoice
            {
                get
                {
                    return topSelectedChoice;
                }
                set
                {
                    topSelectedChoice = value;
                }
            }
            public string BottomSelectedChoice
            {
                get
                {
                    return bottomSelectedChoice;
                }
                set
                {
                    bottomSelectedChoice = value;
                }
            }
            public string TopSelectedPosition
            {
                get
                {
                    return topSelectedPosition;
                }
                set
                {
                    topSelectedPosition = value;
                }
            }
            public string BottomSelectedPosition
            {
                get
                {
                    return bottomSelectedPosition;
                }
                set
                {
                    bottomSelectedPosition = value;
                }
            }

        }

        private void Btn_Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                summarizedChoice.TopSelectedPosition = Combo_top_position.SelectedValue.ToString();
                summarizedChoice.BottomSelectedPosition = Combo_bottom_position.SelectedValue.ToString();
                summarizedChoice.TopSelectedChoice = (Combo_Top.SelectedIndex + 1).ToString();
                summarizedChoice.BottomSelectedChoice = (Combo_Bottom.SelectedIndex + 1).ToString();
                summarizedChoice.IsOk = true;
                this.Close();
            }
            catch(Exception ex)
            {
                string message = ex.Message;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
            summarizedChoice.IsClosed = true;
        }

        private void Window_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }
}
