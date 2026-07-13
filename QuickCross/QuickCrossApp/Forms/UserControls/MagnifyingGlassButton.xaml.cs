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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Qc4Launcher.Forms.UserControls.NewItemSelectionWindow;

namespace Qc4Launcher.Forms.UserControls
{
    /// <summary>
    /// Interaction logic for MagnifyingGlassButton.xaml
    /// </summary>
    public partial class MagnifyingGlassButton : UserControl
    {
        public MagnifyingGlassButton()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        public string Title { get; set; }
        public string AnswerType { get; set; }
        public ExistingVariableList VariableList
        {
            get; set;
        }


        private void Search_Click(object sender, RoutedEventArgs e)
        {
            
            NewItemSelectionWindow newItemSelectionWindow = new NewItemSelectionWindow(Title, AnswerType);
            newItemSelectionWindow.Owner = Window.GetWindow(this);
            newItemSelectionWindow.ShowDialog();
            VariableList = newItemSelectionWindow.SelectedVariable;
        }
    }
}
