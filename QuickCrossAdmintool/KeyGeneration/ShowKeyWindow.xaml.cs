using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Globalization;
using Logger;

namespace KeyGeneration
{
    /// <summary>
    /// Interaction logic for ShowKeyWindow.xaml
    /// </summary>
    public partial class ShowKeyWindow : Window
    {
        private static readonly Log log = new Log();

        public ShowKeyWindow()
        {
            InitializeComponent();
        }

        private void DragableGridMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void BtnDecrypt_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBoxes();
            try
            {
                string activationKey = txt_ActivationKey.Text;
                if (activationKey == String.Empty)
                {
                    MessageDialog.Warning(AdminResource.WARNING_MSG_ENTER_KEY);
                    return;
                }
                string keyInfo = Cryptography.Decrypt(activationKey, Constants.EncryptDecryptPass);
                if (keyInfo == "")
                {
                    MessageDialog.ErrorOk(AdminResource.ERROR_MSG_INVALID_DATA);
                    return;
                }
                string[] infoArray = keyInfo.Split('\t');
                if (infoArray.Length != 10)
                {
                    MessageDialog.ErrorOk(AdminResource.ERROR_MSG__INVALID_KEY);
                    return;
                }
                DateTime expDate;
                DateTime.TryParseExact(infoArray[5], "dd-MM-yyyy",CultureInfo.InvariantCulture,DateTimeStyles.None, out expDate);
                string eDate = expDate.ToString("yyyy/MM/dd");
                string expiryDate = eDate.Replace('-', '/');
                string cDate = Convert.ToDateTime(infoArray[9]).ToString("yyyy/MM/dd hh:mm:ss");
                string createdDate= cDate.Replace('-', '/');

                txt_UserDomain.Text = (infoArray[0] != "") ? infoArray[0] : "";
                txt_UserName.Text = (infoArray[1] != "") ? infoArray[1] : "";
                txt_ComputerName.Text = (infoArray[2] != "") ? infoArray[2] : "";
                txt_MacAddress.Text = (infoArray[3] != "") ? infoArray[3] : "";
                txt_ExpiryDate.Text = (infoArray[5] != "") ? expiryDate : "";
                txt_KeyType.Text = (infoArray[6] != "") ? infoArray[6] : "";
                txt_GUserDomain.Text = (infoArray[7] != "") ? infoArray[7] : "";
                txt_GUserName.Text = (infoArray[8] != "") ? infoArray[8] : "";
                txt_GTime.Text = (infoArray[9] != "") ? createdDate : "";
            }
            catch (Exception ex)
            {
                // MessageDialog.ErrorOk("An unexpected error occured! " + ex.Message);
                log.WriteLog("Pressed ShowKey button", ex.Message, Log.Level.Error);
            }

        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show(AdminResource.MESSAGE_CLOSE_CONF, AdminResource.MESSAGE_QC, MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }

        }

        private void ClearTextBoxes()
        {
            txt_UserDomain.Text = "";
            txt_UserName.Text = "";
            txt_ComputerName.Text = "";
            txt_MacAddress.Text = "";
            txt_ExpiryDate.Text = "";
            txt_KeyType.Text = "";
            txt_GUserDomain.Text = "";
            txt_GUserName.Text = "";
            txt_GTime.Text = "";
        }
    }
}
