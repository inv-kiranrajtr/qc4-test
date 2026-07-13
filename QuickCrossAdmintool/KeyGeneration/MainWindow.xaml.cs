using KeyGeneration.Util;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
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
using System.Windows.Threading;
using Logger;
using System.Text.RegularExpressions;

namespace KeyGeneration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static DataGridCell selectedCell = null;
        private static MouseButtonEventArgs executedEvent = null;
        private static int selectedCellRow = 0;
        private static int selectedCellCol = 0;
        private static string emptyField = string.Empty;
        private static readonly Log log = new Log();
        private static bool flag = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable dt = TableForSystemInformation();
            DataRow dataRow = dt.NewRow();
            dataRow["Sl No"] = 1;
            dt.Rows.Add(dataRow);
            gridSystemInfo.ItemsSource = dt.DefaultView;
            ExecuteActionWhenIdle(CheckboxLogic);
            date_expiry.DisplayDate = DateTime.Now.Date;
            log.WriteLog("Launch QuickCross admin tool", "Running", Log.Level.Info);
        }

        private void ExecuteActionWhenIdle(Action delegateFunction)
        {
            Dispatcher.InvokeAsync(() => { delegateFunction(); }, DispatcherPriority.ApplicationIdle);
        }

        #region DataGridEventHandlers

        private void DragableGridMouseDown(object sender, MouseButtonEventArgs e) //to drag the window
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void DataGridCell_MouseDown(object sender, MouseButtonEventArgs e) 
        {
            selectedCell = sender as DataGridCell;
            executedEvent = e;
            selectedCellCol = selectedCell.Column.DisplayIndex;
            DataGridHelper dataGridHelper = new DataGridHelper();
            selectedCellRow = dataGridHelper.GetDatagridRowIndex(executedEvent);
            txtKeyType.Focus();  
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e) 
        {
            // Starts the Edit on the row;
            if (e.OriginalSource.GetType() == typeof(TextBlock) && (!flag))
            {
                gridSystemInfo.BeginEdit(e);
                flag = true;
            }
            else if (e.OriginalSource.GetType() == typeof(TextBlock) && (flag))
            {
                gridSystemInfo.BeginEdit();
                flag = false;
            }
        }

        private void TxtKeyType_GotFocus(object sender, RoutedEventArgs e)
        {
            if (executedEvent != null)
                UpdateGrid();
        }

        private void UpdateGrid() // logic for adding new row to the datagrid on mouse click over the datagrid cell
        {
            try
            {
                if (executedEvent != null)
                {
                    DataTable dt = new DataTable();
                    dt = ((DataView)gridSystemInfo.ItemsSource).ToTable();
                    List<string> currentRow = new List<string>();
                    List<string> lastRow = new List<string>();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if(dt.Rows[selectedCellRow][i].ToString() != string.Empty && dt.Rows[selectedCellRow][i].ToString() != null)
                            currentRow.Add(dt.Rows[selectedCellRow][i].ToString());
                        if(dt.Rows[dt.Rows.Count - 1][i].ToString() != string.Empty && dt.Rows[selectedCellRow][i].ToString() != null)
                            lastRow.Add(dt.Rows[dt.Rows.Count - 1][i].ToString());
                    }
                   
                    if ((selectedCellCol >= 0) && (currentRow.Count > 1))
                    {
                        if (lastRow.Count > 1) 
                        {
                            var newRow = dt.NewRow();
                            newRow["Sl No"] = dt.Rows.Count + 1;
                            dt.Rows.Add(newRow);
                            dt.AcceptChanges();
                            gridSystemInfo.ItemsSource = dt.DefaultView;
                        }
                    }
                    ExecuteActionWhenIdle(CheckboxLogic);
                }
            }
            catch (Exception ex)
            {
                log.WriteLog("While updating the grid", ex.Message, Log.Level.Warning);
            }
        }

        private void DataGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is ScrollViewer) //mouse click outside the datagrid
            {
                ((DataGrid)sender).UnselectAll();
            }
        }
       
        private void CheckBox_Click(object sender, RoutedEventArgs e) //logic for getting focus to the selected row while checkbox click
        {
            if (gridSystemInfo.SelectedItems.Count > 0)
            {
                gridSystemInfo.CommitEdit();
            }
            if (selectedCell != null)
                selectedCell.Focus();
            DataTable dt = TableForSystemInformation();
            dt = ((DataView)gridSystemInfo.ItemsSource).ToTable();
        }

        private void GridSystemInfo_PreviewKeyDown(object sender, KeyEventArgs e) //logic for catching clipboard paste event(ctrl+V)
        {
            try
            {
                bool _altModifierPressed = (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl));
                bool _vPressed = Keyboard.IsKeyDown(Key.V);
                if (_altModifierPressed && _vPressed) 
                {
                    if(gridSystemInfo.CurrentItem != null)
                        gridSystemInfo.SelectedIndex = gridSystemInfo.Items.IndexOf(gridSystemInfo.CurrentItem);
                    if (gridSystemInfo.SelectedIndex >= 0)
                    {
                        e.Handled = true;
                        string clipboardText = "";
                        if (Clipboard.ContainsText(TextDataFormat.UnicodeText))
                        {
                            clipboardText = Clipboard.GetText(TextDataFormat.UnicodeText);
                        }
                        DataTable dt = new DataTable();
                        dt = ((DataView)gridSystemInfo.ItemsSource).ToTable();
                        int selectedIndex = gridSystemInfo.SelectedIndex;
                        int gridRowLength = gridSystemInfo.Items.Count;
                        int gridColLength = dt.Columns.Count;
                        string regexReplacedStr = Regex.Replace(clipboardText.Trim().Replace("\"\"", "\tabu\""), "(?!(([^\"]*\"){2})*[^\"]*$)(\\n|\\r|\\r\\n)+", string.Empty);//to identify rows
                        List<string> dataToPaste = regexReplacedStr.Split(new[] { "\r\n" }, StringSplitOptions.None).ToList();
                        string[] dataInCols = new string[] { };
                        for (int i = 0; i <= dataToPaste.Count - 1; i++)
                        {
                            string[] ndataToPaste = dataToPaste[i].Split(new[] { "\r", "\n" }, StringSplitOptions.None);
                            dataToPaste[i] = string.Join("\n", ndataToPaste).Replace("\"", "").Replace("\tabu", "\"");
                            dataInCols = dataToPaste[i].Split('\t');//to identify columns
                            if ((gridColLength - selectedCellCol) < dataInCols.Count())
                            {
                                MessageDialog.Warning(AdminResource.WARNING_MSG_COPYPASTE_2);
                                return;
                            }
                        }
                        
                        if ((gridRowLength - selectedCellRow) < dataToPaste.Count)
                        {
                            for (int k = gridRowLength; k < selectedCellRow + dataToPaste.Count; k++)
                            {
                                var newRow = dt.NewRow();
                                newRow["Sl No"] = dt.Rows.Count + 1;
                                dt.Rows.Add(newRow);
                                dt.AcceptChanges();
                                gridSystemInfo.ItemsSource = dt.DefaultView;
                            }
                        }
                        for (int i = selectedCellRow, j = 0; i < selectedCellRow + dataToPaste.Count && j < dataToPaste.Count; i++, j++)
                        {
                           dataInCols = dataToPaste[j].Split('\t');
                           for (int c = 0; c < dataInCols.Count(); c++)
                           {
                                dt.Rows[i][selectedCellCol + c] = dataInCols[c]; //paste the data to appr. cells

                           }
                        }
                        dt.AcceptChanges();
                        gridSystemInfo.ItemsSource = dt.DefaultView;
                    }
                    else
                        MessageDialog.Warning(AdminResource.WARNING_MSG_COPYPASTE_1);
                }
               
            }
            catch(Exception ex)
            {
                log.WriteLog("While clipboard pasting", ex.Message, Log.Level.Warning);
            }

        }

        #endregion

        #region WindowButtonEvents

        private void BtnGenerateKey_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable dt = TableForSystemInformation();
                dt = ((DataView)gridSystemInfo.ItemsSource).ToTable();
                if (gridSystemInfo.SelectedItems.Count == 0)
                {
                    MessageDialog.Warning(AdminResource.WARNING_MSG_SELECT_ROW_1);
                    return;
                }
                string expiryDate = date_expiry.SelectedDate.ToString();
                if (expiryDate == string.Empty)
                {
                    MessageDialog.ErrorOk(AdminResource.ERROR_MSG_DATE);
                    return;
                }
                bool infoIsEntered = CheckSystemInfo();
                if(! infoIsEntered)
                {
                    MessageDialog.Warning(AdminResource.WARNING_MSG_SELECT_ATLEAST_ONE);
                    return;
                }
                else
                {
                    for (int i = 0; i < gridSystemInfo.SelectedItems.Count; i++)
                    {
                        DataRowView itemRow = (DataRowView)gridSystemInfo.SelectedItems[i];
                        if (itemRow["User domain"].ToString() == string.Empty && itemRow["User name"].ToString() == string.Empty && itemRow["Computer name"].ToString() == string.Empty && itemRow["Mac address"].ToString() == string.Empty)
                        {
                            //uninput data
                            MessageDialog.Warning(AdminResource.WARNING_MSG_ROW + itemRow["Sl No"] + "\n" + AdminResource.WARNING_MSG_DATA);
                            continue;
                        }

                        else
                        {
                            string keyType = txtKeyType.Text;
                            string userDomainName = Environment.UserDomainName;
                            string userName = Environment.UserName;
                            string dateTime = DateTime.Now.ToString();
                            string info = string.Empty;
                            itemRow["Activation Key"] = "";
                            info = CreateIdentificationInfo(itemRow);
                            if (emptyField != string.Empty)
                                emptyField = emptyField.TrimEnd(',');
                            if (info == "")
                            {
                                MessageDialog.ErrorOk(AdminResource.WARNING_MSG_ROW + itemRow["Sl No"] + "\n" + AdminResource.WARNING_MSG_INVALID_DATA);
                                continue;
                            }
                            else if (info == "Error") // if selected system information has no values in it
                            {
                                MessageDialog.Warning(AdminResource.WARNING_MSG_ROW + itemRow["Sl No"] + "\n" + AdminResource.WARNING_MSG_SELECT_INFO + emptyField);
                                emptyField = string.Empty;
                                continue;
                            }
                            else
                            {
                                info += "\t" + (date_expiry.SelectedDate.Value.Date).ToString("dd-MM-yyyy");
                                info += "\t" + keyType;
                                info += "\t" + userDomainName + "\t" + userName + "\t" + dateTime;
                                itemRow["Activation Key"] = Cryptography.Encrypt(info, Constants.EncryptDecryptPass);
                            }

                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageDialog.ErrorOk(AdminResource.ERROR_MSG_ERROR_DATA);
                log.WriteLog("Pressed Generate button", ex.Message, Log.Level.Error);
            }
        }

        private void BtnDeleteKey_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable dt = TableForSystemInformation();
                dt = ((DataView)gridSystemInfo.ItemsSource).ToTable();
                if (gridSystemInfo.SelectedItems.Count == 0)
                {
                    MessageDialog.Warning(AdminResource.WARNING_MSG_SELECT_ROW_2);
                    return;
                }
                MessageBoxResult result;
                result = MessageBox.Show(AdminResource.MESSAGE_DELETE_CONF, AdminResource.MESSAGE_QC, MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    List<string> list = new List<string>();
                    for (int i = 0; i < gridSystemInfo.SelectedItems.Count; i++) //collecting SlNo of rows to be deleted
                    {
                        DataRowView itemRow = (DataRowView)gridSystemInfo.SelectedItems[i];
                        var obj = itemRow["Sl No"].ToString();
                        list.Add(obj);
                    }
                    for( int l = 0; l < list.Count; l++) //logic to delete the selected rows
                    {
                        for (int r = 0; r < dt.Rows.Count; r++)
                        {
                            DataRow dr = dt.Rows[r];
                            if (dr.ItemArray[0].ToString() == list[l])
                            {
                                dr.Delete();
                                dt.AcceptChanges();
                            }
                            else
                                continue;
                        }

                    }
                }
                if (dt.Rows.Count == 0)
                {
                    DataRow dataRow = dt.NewRow();
                    dataRow["Sl No"] = 1;
                    dt.Rows.Add(dataRow);
                }
                else
                {
                    for(int j = 0; j < dt.Rows.Count; j++)
                    {
                        dt.Rows[j][0] = j + 1; //put SlNo of remaining rows correctly
                    }
                }
                gridSystemInfo.ItemsSource = dt.DefaultView;
                ExecuteActionWhenIdle(CheckboxLogic);

            }
            catch (Exception ex)
            {
                MessageDialog.ErrorOk(AdminResource.ERROR_MSG_ERROR_DATA);
                log.WriteLog("Pressed Delete button", ex.Message, Log.Level.Error);
            }

        }

        private void BtnDecryptKey_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable dt = TableForSystemInformation();
                dt = ((DataView)gridSystemInfo.ItemsSource).ToTable();
                if (gridSystemInfo.SelectedItems.Count == 0)
                {
                    MessageDialog.Warning(AdminResource.WARNING_MSG_SELECT_ROW_1);
                    return;
                }
                for (int i = 0; i < gridSystemInfo.SelectedItems.Count; i++)
                {
                    DataRowView itemRow = (DataRowView)gridSystemInfo.SelectedItems[i];
                    var obj = itemRow["System ID"].ToString();
                    if (obj == string.Empty)
                    {
                        MessageDialog.ErrorOk(AdminResource.WARNING_MSG_ROW + itemRow["Sl No"] + "\n" + AdminResource.WARNING_MSG_ROW_WITH_ID);
                        continue;
                    }
                    else
                    {
                        itemRow["User domain"] = "";
                        itemRow["User name"] = "";
                        itemRow["Computer name"] = "";
                        itemRow["Mac address"] = "";
                        string systemInfo = Cryptography.Decrypt(Convert.ToString(obj), Constants.EncryptDecryptPass);
                        if (systemInfo == "")
                        {
                            MessageDialog.ErrorOk(AdminResource.WARNING_MSG_ROW + itemRow["Sl No"] + "\n" + AdminResource.ERROR_MSG_INVALID_DATA);
                            continue;
                        }
                        if (verifyInfo(systemInfo))
                        {
                            string[] infoArray = systemInfo.Split('\t');
                            itemRow["User domain"] = infoArray[0];
                            itemRow["User name"] = infoArray[1];
                            itemRow["Computer name"] = infoArray[2];
                            itemRow["Mac address"] = infoArray[3];
                        }
                        else
                        {
                            MessageDialog.ErrorOk(AdminResource.WARNING_MSG_ROW + itemRow["Sl No"] + "\n" + AdminResource.ERROR_MSG_INVALID_DATA);
                            continue;
                        }
                    }
                }
               
            }
            catch (Exception ex)
            {
                MessageDialog.ErrorOk(AdminResource.ERROR_MSG_ERROR_DATA);
                log.WriteLog("Pressed Decrypt button", ex.Message, Log.Level.Error);
            }

        }

        private void BtnShowKey_Click(object sender, RoutedEventArgs e)
        {
            ShowKeyWindow showKeyWindow = new ShowKeyWindow();
            showKeyWindow.ShowDialog();
        }


        private void BtnExitKey_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show(AdminResource.MESSAGE_CLOSE_CONF, AdminResource.MESSAGE_QC, MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                log.WriteLog("Exit QuickCross Admin tool", "Closing", Log.Level.Info);
                this.Close();
            }

        }

        private string CreateIdentificationInfo(DataRowView selectedRow) // to identify which all information is entered for key generation
        {
            string idenInfo = "";
            int emptyCount = 0;
            try
            {
                foreach (DataGridColumn dataGridColumn in gridSystemInfo.Columns)
                {
                    if (dataGridColumn.Header.ToString() == "User domain")
                    {
                        DataGridHelper dataGridHelper = new DataGridHelper();
                        CheckBox chkGridColumn = dataGridHelper.FindVisualChildByName<CheckBox>(dataGridHelper.GetColumnHeaderFromColumn(gridSystemInfo, dataGridColumn), "chkGridColumn") as CheckBox;
                        if (chkGridColumn.IsChecked == true)
                        {
                            if (selectedRow["User domain"].ToString() != string.Empty)
                            {
                                idenInfo += selectedRow["User domain"];
                            }
                            else
                            {
                                emptyCount++;
                                emptyField += "User domain,";
                            }
                        }
                        
                    }
                    else if (dataGridColumn.Header.ToString() == "User name")
                    {
                        DataGridHelper dataGridHelper = new DataGridHelper();
                        CheckBox chkGridColumn = dataGridHelper.FindVisualChildByName<CheckBox>(dataGridHelper.GetColumnHeaderFromColumn(gridSystemInfo, dataGridColumn), "chkGridColumn") as CheckBox;
                        if (chkGridColumn.IsChecked == true)
                        {
                            if (selectedRow["User name"].ToString() != string.Empty)
                            {
                                idenInfo += "\t" + selectedRow["User name"];
                            }
                            else
                            {
                                emptyCount++;
                                emptyField += "User name,";
                            }
                        }
                        else
                            idenInfo += "\t";
                    }
                    else if (dataGridColumn.Header.ToString() == "Computer name")
                    {
                        DataGridHelper dataGridHelper = new DataGridHelper();
                        CheckBox chkGridColumn = dataGridHelper.FindVisualChildByName<CheckBox>(dataGridHelper.GetColumnHeaderFromColumn(gridSystemInfo, dataGridColumn), "chkGridColumn") as CheckBox;
                        if (chkGridColumn.IsChecked == true)
                        { 
                            if (selectedRow["Computer name"].ToString() != string.Empty)
                            {
                                idenInfo += "\t" + selectedRow["Computer name"];
                            }
                            else
                            {
                                emptyCount++;
                                emptyField += "Computer name,";
                            }
                        }
                        else
                            idenInfo += "\t";
                    }
                    else if (dataGridColumn.Header.ToString() == "Mac address")
                    {
                        DataGridHelper dataGridHelper = new DataGridHelper();
                        CheckBox chkGridColumn = dataGridHelper.FindVisualChildByName<CheckBox>(dataGridHelper.GetColumnHeaderFromColumn(gridSystemInfo, dataGridColumn), "chkGridColumn") as CheckBox;
                        if (chkGridColumn.IsChecked == true)
                        {
                            if (selectedRow["Mac address"].ToString() != string.Empty)
                            {
                                idenInfo += "\t" + selectedRow["Mac address"];
                            }
                            else
                            {
                                emptyCount++;
                                emptyField += "Mac address,";
                            }
                        }
                        else
                            idenInfo += "\t";
                    }
                }

                if (emptyCount > 0)
                    return "Error";
            }
            catch (Exception ex)
            {
                log.WriteLog("While generating activation key", ex.Message, Log.Level.Warning);
                return string.Empty;
            }
            return idenInfo + "\t" + Constants.QC4Key; 
        }

        private bool CheckSystemInfo() // to identify which all check boxes are checked
        {
            int c = 0;
            foreach (DataGridColumn dataGridColumn in gridSystemInfo.Columns)
            {
                if (dataGridColumn.Header.ToString() == "User domain" || dataGridColumn.Header.ToString() == "User name" || dataGridColumn.Header.ToString() == "Computer name" || dataGridColumn.Header.ToString() == "Mac address")
                {
                    DataGridHelper dataGridHelper = new DataGridHelper();
                    CheckBox chkGridColumn = dataGridHelper.FindVisualChildByName<CheckBox>(dataGridHelper.GetColumnHeaderFromColumn(gridSystemInfo, dataGridColumn), "chkGridColumn") as CheckBox;
                    if (chkGridColumn.IsChecked == true)
                    {
                        c++;
                    }
                }
            }
            if (c == 0)
                return false;
            else
                return true;
        }

        private bool verifyInfo(string info) // to verify the validity of system ID
        {
            string[] splitAry = info.Split('\t');
            if (splitAry.Length < 5 || splitAry.Length > 5)
            {
                return false;
            }
            return true;
        }

        private DataTable TableForSystemInformation()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Sl No", typeof(string));
            dt.Columns.Add("System ID", typeof(string));
            dt.Columns.Add("User domain", typeof(string));
            dt.Columns.Add("User name", typeof(string));
            dt.Columns.Add("Computer name", typeof(string));
            dt.Columns.Add("Mac address", typeof(string));
            dt.Columns.Add("E mail", typeof(string));
            dt.Columns.Add("Note", typeof(string));
            dt.Columns.Add("Activation Key", typeof(string));
            return dt;
        }

        private void CheckboxLogic()
        {
            foreach (DataGridColumn dataGridColumn in gridSystemInfo.Columns)
            {
                if (dataGridColumn.Header.ToString() == "Sl No" || dataGridColumn.Header.ToString() == "System ID" || dataGridColumn.Header.ToString() == "E mail" || dataGridColumn.Header.ToString() == "Note" || dataGridColumn.Header.ToString() == "Activation Key")
                {
                    DataGridHelper dataGridHelper = new DataGridHelper();
                    CheckBox chkGridColumn = dataGridHelper.FindVisualChildByName<CheckBox>(dataGridHelper.GetColumnHeaderFromColumn(gridSystemInfo, dataGridColumn), "chkGridColumn") as CheckBox;
                   // chkGridColumn.Visibility = Visibility.Hidden;
                }
            }
        }

        #endregion
    }

    public static class Cryptography
    {
        #region Settings

        private static int _iterations = 2;
        private static int _keySize = 256;

        private static string _hash = "SHA1";
        private static string _salt = "aselrias38490a32"; // Random
        private static readonly Log log = new Log();
        #endregion

        public static string Encrypt(string value, string password)
        {
            return Encrypt<AesManaged>(value, password);
        }
        public static string Encrypt<T>(string value, string password)
                where T : SymmetricAlgorithm, new()
        {
            try
            {
                string _vector = RandomStr();
                byte[] vectorBytes = ASCIIEncoding.ASCII.GetBytes(_vector);
                byte[] saltBytes = ASCIIEncoding.ASCII.GetBytes(_salt);
                byte[] valueBytes = UTF8Encoding.UTF8.GetBytes(value);

                byte[] encrypted;
                using (T cipher = new T())
                {
                    PasswordDeriveBytes _passwordBytes =
                        new PasswordDeriveBytes(password, saltBytes, _hash, _iterations);
                    byte[] keyBytes = _passwordBytes.GetBytes(_keySize / 8);

                    cipher.Mode = CipherMode.CBC;

                    using (ICryptoTransform encryptor = cipher.CreateEncryptor(keyBytes, vectorBytes))
                    {
                        using (MemoryStream to = new MemoryStream())
                        {
                            using (CryptoStream writer = new CryptoStream(to, encryptor, CryptoStreamMode.Write))
                            {
                                writer.Write(valueBytes, 0, valueBytes.Length);
                                writer.FlushFinalBlock();
                                encrypted = to.ToArray();
                            }
                        }
                    }
                    cipher.Clear();
                }
                byte[] valueBytes2 = UTF8Encoding.UTF8.GetBytes(Convert.ToBase64String(encrypted));
                using (T cipher = new T())
                {
                    PasswordDeriveBytes _passwordBytes =
                        new PasswordDeriveBytes(password, saltBytes, _hash, _iterations);
                    byte[] keyBytes = _passwordBytes.GetBytes(_keySize / 8);

                    cipher.Mode = CipherMode.CBC;

                    using (ICryptoTransform encryptor = cipher.CreateEncryptor(keyBytes, vectorBytes))
                    {
                        using (MemoryStream to = new MemoryStream())
                        {
                            using (CryptoStream writer = new CryptoStream(to, encryptor, CryptoStreamMode.Write))
                            {
                                writer.Write(valueBytes2, 0, valueBytes2.Length);
                                writer.FlushFinalBlock();
                                encrypted = to.ToArray();
                            }
                        }
                    }
                    cipher.Clear();
                }
                return Convert.ToBase64String(encrypted).Insert(10, _vector);
            }
            catch (Exception ex)
            {
                MessageDialog.ErrorOk(AdminResource.ERROR_MSG_INVALID_DATA);
                log.WriteLog("During encryption", ex.Message, Log.Level.Warning);
                return string.Empty;
            }
        }

        public static string Decrypt(string value, string password)
        {
            return Decrypt<AesManaged>(value, password);
        }

        public static string Decrypt<T>(string value, string password) where T : SymmetricAlgorithm, new()
        {
            try
            {
                string key = value.Remove(10, 16);
                string _vector = value.Substring(10, 16);
                byte[] vectorBytes = ASCIIEncoding.ASCII.GetBytes(_vector);
                byte[] saltBytes = ASCIIEncoding.ASCII.GetBytes(_salt);
                byte[] valueBytes = Convert.FromBase64String(key);

                byte[] decrypted;
                int decryptedByteCount = 0;
                using (T cipher = new T())
                {
                    PasswordDeriveBytes _passwordBytes = new PasswordDeriveBytes(password, saltBytes, _hash, _iterations);
                    byte[] keyBytes = _passwordBytes.GetBytes(_keySize / 8);

                    cipher.Mode = CipherMode.CBC;

                    try
                    {
                        using (ICryptoTransform decryptor = cipher.CreateDecryptor(keyBytes, vectorBytes))
                        {
                            using (MemoryStream from = new MemoryStream(valueBytes))
                            {
                                using (CryptoStream reader = new CryptoStream(from, decryptor, CryptoStreamMode.Read))
                                {
                                    decrypted = new byte[valueBytes.Length];
                                    decryptedByteCount = reader.Read(decrypted, 0, decrypted.Length);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        log.WriteLog("During decryption", ex.Message, Log.Level.Warning);
                        return String.Empty;
                    }

                    cipher.Clear();
                }
                byte[] valueBytes2 = Convert.FromBase64String(Encoding.UTF8.GetString(decrypted, 0, decryptedByteCount));
                using (T cipher = new T())
                {
                    PasswordDeriveBytes _passwordBytes = new PasswordDeriveBytes(password, saltBytes, _hash, _iterations);
                    byte[] keyBytes = _passwordBytes.GetBytes(_keySize / 8);

                    cipher.Mode = CipherMode.CBC;

                    try
                    {
                        using (ICryptoTransform decryptor = cipher.CreateDecryptor(keyBytes, vectorBytes))
                        {
                            using (MemoryStream from = new MemoryStream(valueBytes2))
                            {
                                using (CryptoStream reader = new CryptoStream(from, decryptor, CryptoStreamMode.Read))
                                {
                                    decrypted = new byte[valueBytes2.Length];
                                    decryptedByteCount = reader.Read(decrypted, 0, decrypted.Length);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        log.WriteLog("During decryption", ex.Message, Log.Level.Warning);
                        return String.Empty;
                        //MessageDialog.ErrorOk(AdminResource.ERROR_MSG_INVALID_DATA);
                    }

                    cipher.Clear();
                }
                return Encoding.UTF8.GetString(decrypted, 0, decryptedByteCount);
            }
            catch (Exception ex)
            {
                //MessageDialog.ErrorOk(AdminResource.ERROR_MSG_INVALID_DATA);
                log.WriteLog("During decryption", ex.Message, Log.Level.Warning);
                return String.Empty;
            }
        }
        public static string RandomStr()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(6, true));
            builder.Append(RandomString(3, false));
            builder.Append(RandomString(3, true));
            builder.Append(RandomString(4, false));
            return builder.ToString();
        }
        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

    }
}
