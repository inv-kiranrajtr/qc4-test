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

namespace Qc4Launcher
{
	/// <summary>
	/// Interaction logic for DataMerge.xaml
	/// </summary>
	public partial class DataMerge : Window
	{
		public DataMerge()
		{
			InitializeComponent();
		}
		
		private void DragableGridMouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
				DragMove();
		}

		private void btn_Close_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void btn_ExternalData_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("外部データ追加");
		}

		private void btn_CombineSideWays_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("横に結合（qcファイル同士）");
		}

		private void btn_CombineVertically_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("縦に結合（qcファイル同士）");
		}

		private void btn_ScreeningMatch_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("スクリーニングマッチ");
		}

		private void btn_CreateNewQCFile_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("新規qcファイル作成");
		}
	}
}
