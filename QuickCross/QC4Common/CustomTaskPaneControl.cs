using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace QC4Common
{
    public partial class CustomTaskPaneControl : UserControl
    {
        private Microsoft.Office.Interop.Excel.Worksheet DataProcess;
        public CustomTaskPaneControl()
        {
            InitializeComponent();
        }
        public void AddListBoxItem(string ItemText)
        {
            listBox1.Items.Add(ItemText);
        }
        public void ClearList()
        {
            listBox1.Items.Clear();
        }
        public void SetSheetObject(Worksheet DPSheet)
        {
            DataProcess = DPSheet;
        }

        private void CustomTaskPaneControl_SizeChanged(object sender, EventArgs e)
        {
            listBox1.Height = this.Height;
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            string text = listBox1.GetItemText(listBox1.SelectedItem);
            DataProcess.Application.ActiveCell.Value = text;
           // ThisAddIn.Application.ActiveCell.Text = text;
        }
    }

}
