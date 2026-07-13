using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Qc4Launcher.Util.QS
{
    class  GridCopyPaste
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public  int No_Row=0;
      public  int No_Columns = 0;
           public object[,] PastetoDatagrid(object sender)
        {
           
            QC4Common.Util.FormUtil frmutil = new QC4Common.Util.FormUtil();
            DataGrid dataGrid = (DataGrid)sender;
            int datagridColumn = dataGrid.CurrentCell.Column.DisplayIndex;
            DataGridCell cell = frmutil.GetCell(dataGrid, dataGrid.SelectedIndex, datagridColumn);

            bool _altModifierPressed = (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl));
            if (_altModifierPressed && Keyboard.IsKeyDown(Key.V))
            {
                QC4Common.Util.FormUtil formUtil = new QC4Common.Util.FormUtil();
                string clipboardText = "";
                string regexReplacedStr = "";
                if (Clipboard.ContainsText(TextDataFormat.UnicodeText))
                {
                    clipboardText = Clipboard.GetText(TextDataFormat.UnicodeText);
                }
               
                while (clipboardText != "")
                {
                    string str = "";
                    if (clipboardText.Length > 1000000)
                    {
                        str = clipboardText.Substring(0, 1000000);
                        clipboardText = clipboardText.Remove(0, 1000000);
                    }
                    else
                    {
                        str = clipboardText;
                        clipboardText = "";
                    }
                    
                    regexReplacedStr += Regex.Replace(str.Replace("\"\"", "\t( ͡❛ ͜ʖ ͡❛)\""), "(?!(([^\"]*\"){2})*[^\"]*$)(\\n|\\r|\\r\\n)+", string.Empty);

                }
                List<string> choiceWordings = regexReplacedStr.Split(new[] { "\r\n" }, StringSplitOptions.None).ToList();
                for (int i = 0; i <= choiceWordings.Count - 1; i++)
                {
                    string[] nChoiceWordings = choiceWordings[i].Split(new[] { "\r", "\n" }, StringSplitOptions.None);
                    choiceWordings[i] = string.Join("\n", nChoiceWordings).Replace("\"", "").Replace("\t( ͡❛ ͜ʖ ͡❛)", "\"");
                    choiceWordings[i] = choiceWordings[i] + "\r\n";
                }
                clipboardText = string.Empty;

                for (int i=0;i<=choiceWordings.Count-1;i++)
                {
                    clipboardText = clipboardText + choiceWordings[i];
                }
                
                if (clipboardText.EndsWith("\r\n\r\n"))
                {
                    clipboardText = clipboardText.Remove(clipboardText.Length - 4);
                }
                else if(clipboardText.EndsWith("\r\n"))
                {
                    clipboardText = clipboardText.Remove(clipboardText.Length - 2);
                }
               
        

                var dataToPaste = clipboardText.Split(new[] { "\r\n" }, StringSplitOptions.None).Select(i => Regex.Split(i, "\t")).ToArray();
                int MaxCol = 0;


                for (int i=0;i<dataToPaste.Length;i++)
                {
                    if(MaxCol<=dataToPaste[i].Length)
                    {
                        MaxCol = dataToPaste[i].Length;
                    }
                }
                Object[,] copyArray = new Object[dataToPaste.Length, MaxCol];
                string data = string.Empty;
                string odata = string.Empty;
                
                if (dataGrid.Items.Count >= dataToPaste.Length)
                {
                    No_Row = dataToPaste.Length;
                    No_Columns = MaxCol;
                    for (int i = 0; i != dataToPaste.Length; i++)
                    {
                        for (int j = 0; j != MaxCol; j++)
                        {
                            try
                            {
                                copyArray[i, j] = dataToPaste[i][j];
                            }
                            catch
                            {
                                copyArray[i, j] = null;
                            }
                            string str = Convert.ToString(copyArray[i, j]);

                            if (str.Contains("\n"))
                            {
                                copyArray[i, j] = formUtil.EscapeCRLF(str.Remove(str.Length - 1, 1).Remove(0, 1)).TrimStart().TrimEnd();
                            }
                            else if (string.IsNullOrEmpty(str))
                            {
                                copyArray[i, j] = null;
                            }

                        }
                    }
                }
                else
                {
                    MessageDialog.ErrorOk(LocalResource.COPY_ERROR_MSG) ;
                    return null;
                }

                return copyArray;
            }
            return null;
        }

        public void replaceString()
        {

        }
     




        public string CopyEditedCell(string clipboardText)
        {
            try
            {
                QC4Common.Util.FormUtil formUtil = new QC4Common.Util.FormUtil();
                string clipboardText1 = "";
                string regexReplacedStr = "";
                if (Clipboard.ContainsText(TextDataFormat.UnicodeText))
                {
                    clipboardText = Clipboard.GetText(TextDataFormat.UnicodeText);
                }
              
                while (clipboardText != "")
                {
                    string str = "";
                    if (clipboardText.Length > 1000000)
                    {
                        str = clipboardText.Substring(0, 1000000);
                        clipboardText = clipboardText.Remove(0, 1000000);
                    }
                    else
                    {
                        str = clipboardText;
                        clipboardText = "";
                    }
                   
                }
                
            }
            catch (Exception ex)
            {

            }
            return clipboardText;
        }
    }
}

    
