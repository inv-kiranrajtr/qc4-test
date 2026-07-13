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
	/// Interaction logic for SwapDataErrorList.xaml
	/// </summary>
	public partial class SwapDataErrorList : Window
	{
		public SwapDataErrorList(List<string> list)
		{
			InitializeComponent();
			listView.ItemsSource = list;
		}

		private void btn_Close_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void DragableGridMouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
				DragMove();
		}
	}
}
