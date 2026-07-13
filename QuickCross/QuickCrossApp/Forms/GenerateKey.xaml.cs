using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
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
using Microsoft.Win32;
using Qc4Launcher.Util;

namespace Qc4Launcher.Forms
{
    /// <summary>
    /// Interaction logic for GenerateKey.xaml
    /// </summary>
    public partial class GenerateKey : Window
    {
        public GenerateKey()
        {
            InitializeComponent();
            txt_version.Text = string.Format("v{0}.{1}.{2}", Assembly.GetExecutingAssembly().GetName().Version.Major.ToString(), Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString(), Assembly.GetExecutingAssembly().GetName().Version.Build.ToString());
            //txt_version.Text = string.Format("v{0}", Assembly.GetExecutingAssembly().GetName().Version.Major.ToString());
            CommonFunction.ActivationKeyChecking();
            if (Constants.IsPro)
            {
                EnableVersionChange();
                using (RegistryKey rKey = Registry.CurrentUser.CreateSubKey(Constants.RegistryPath))
                {
                    txtKey.Text = Convert.ToString(rKey.GetValue(Constants.RegistrykeyName));
                    rKey.Close();
                }
            }
            else
            {
                if (CheckKeyInRegistry())
                    rdSTD.IsChecked = true;
                else
                    DisableVersionChange();
            }
        }

        private bool CheckKeyInRegistry()
        {
            string key = "";
            using (RegistryKey rKey = Registry.CurrentUser.CreateSubKey(Constants.RegistryPath))
            {
                key = Convert.ToString(rKey.GetValue(Constants.RegistrykeyName));
                rKey.Close();
            }
            if (key != "")
                return true;
            else
                return false;
        }

        private void DisableVersionChange()
        {
            rdSTD.IsChecked = true;
            rdPro.IsEnabled = false;
            rdSTD.IsEnabled = false;
            btnVerChange.IsEnabled = false;
        }

        private void EnableVersionChange()
        {
            rdPro.IsEnabled = true;
            rdSTD.IsEnabled = true;
            btnVerChange.IsEnabled = true;
            rdPro.IsChecked = true;
        }

        private void DragableGridMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void BtnGenerate_Click(object sender, RoutedEventArgs e)
        {
            string UserDomainName = Environment.UserDomainName;
            string UserName = Environment.UserName;
            string MachineName = Environment.MachineName;
            string firstMacAddress = NetworkInterface
.GetAllNetworkInterfaces()
.Where(nic => nic.NetworkInterfaceType != NetworkInterfaceType.Loopback && nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
.Select(nic => nic.GetPhysicalAddress().ToString())
.FirstOrDefault();
            string pcInfo = UserDomainName + "\t" + UserName + "\t" + MachineName + "\t" + firstMacAddress + "\t" + Constants.QC4Key;
            string code = Cryptography.Encrypt(pcInfo, Constants.EncryptDecryptPass);
            txtEncryptedCode.Text = code;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            //if (MessageBox.Show(LocalResource.AUT_WANT_TO_CLOSE, LocalResource.TITLE_QC, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            //{
            this.Close();
            //}
        }

        private void BtnGenerateKey_Click(object sender, RoutedEventArgs e)
        {
            string key = txtKey.Text;
            string strMsg = "";
            bool msgDisplayed = false;
            if (key != "")
            {
                if (CommonFunction.ActivationKeyChecking(key))
                {
                    if (WriteKeyToRegistry(key))
                    {
                        MessageBox.Show(LocalResource.AUT_MSG_SUCCESS_KEY, LocalResource.TITLE_QC, MessageBoxButton.OK, MessageBoxImage.Information);
                        EnableVersionChange();
                        msgDisplayed = true;
                        InstallAddIn();//Installs ExcelAddin
                    }
                    else
                    {
                        strMsg = LocalResource.AUT_MSG_FAIL_KEY;
                    }
                }
                else
                {
                    strMsg = LocalResource.AUT_MSG_INVALID_KEY;
                }
            }
            else
            {
                strMsg = LocalResource.AUT_MSG_INVALID_KEY;
            }
            //txtKey.Text = "";
            if (!msgDisplayed)
                MessageBox.Show(strMsg, LocalResource.TITLE_QC, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private bool WriteKeyToRegistry(string key)
        {
            if (SetKeyInRegistry(key))
                return true;
            else
                return false;
        }

        private void BtnDeleteKey_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(LocalResource.AUT_WANT_TO_DEL, LocalResource.TITLE_QC, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                if (SetKeyInRegistry(""))
                {
                    DisableVersionChange();
                    MessageBox.Show(LocalResource.AUT_MSG_DELETE_KEY, LocalResource.TITLE_QC, MessageBoxButton.OK, MessageBoxImage.Information);
                    txtKey.Text = string.Empty;
                }
                else
                {
                    MessageBox.Show(LocalResource.AUT_MSG_NO_DELETE_KEY, LocalResource.TITLE_QC, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool SetKeyInRegistry(String key)
        {
            try
            {
                using (RegistryKey rKey = Registry.CurrentUser.CreateSubKey(Constants.RegistryPath))
                {
                    rKey.SetValue(Constants.RegistrykeyName, key);
                    rKey.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void BtnChangeVersion_Click(object sender, RoutedEventArgs e)
        {

            if (rdPro.IsChecked == true)
            {
                //if (CurrentVersion != "PRO")
                //{
                if (MessageBox.Show(LocalResource.AUT_MSG_STD_PRO, LocalResource.TITLE_QC, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Constants.IsPro = true;
                    CommonFunction.ActivationKeyChecking();
                    if (Constants.IsPro)
                    {
                        MessageBox.Show(LocalResource.AUT_MSG_CHANGE_PRO, LocalResource.TITLE_QC, MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(LocalResource.AUT_MSG_CHANGE_FAIL, LocalResource.TITLE_QC, MessageBoxButton.OK, MessageBoxImage.Error);
                        rdSTD.IsChecked = true;
                    }
                }
                //}
                //else
                //{
                //    MessageBox.Show("Already Changed to PRO");
                //}
            }
            else
            {

                //if (CurrentVersion != "STD")
                //{
                if (MessageBox.Show(LocalResource.AUT_MSG_PRO_STD, LocalResource.TITLE_QC, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Constants.IsPro = false;
                    MessageBox.Show(LocalResource.AUT_MSG_CHANGE_STD, LocalResource.TITLE_QC, MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                //}
                //else
                //{
                //    MessageBox.Show("Already Changed to STD");
                //}
            }
        }
        public static void InstallAddIn()
        {
            // Install Addin if not existing
            try
            {
                string pathForInstallation;
                RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"Software\Classes\QuickCross File\DefaultIcon", true);
                if (myKey != null)
                {
                    pathForInstallation = @"file:///" + myKey.GetValue("").ToString();
                    pathForInstallation = pathForInstallation.Replace(@"\QuickCross\file_icon_m.ico", "");

                    RegistryKey myKey1 = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\Excel\Addins\QC4ExcelAddIn", true);
                    if (myKey1 != null)
                    {
                        using (RegistryKey key = Registry.CurrentUser)
                        {
                            key.DeleteSubKeyTree(@"Software\Microsoft\Office\Excel\Addins\QC4ExcelAddIn", false);
                        }
                    }

                    RegistryKey myKey2 = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\Excel\Addins\QC4ExcelAddIn", true);
                    if (myKey2 == null)
                    {
                        using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Office\Excel\Addins\QC4ExcelAddIn"))
                        {
                            key.SetValue("Description", "QC4ExcelAddIn");
                            key.SetValue("FriendlyName", "ExcelAddIn");
                            key.SetValue("LoadBehavior", 3, RegistryValueKind.DWord);
                            key.SetValue("Manifest", pathForInstallation + @"\QuickCross\QC4ExcelAddIn.vsto|vstolocal");
                        }
                    }
                    return;
                }
                //If OS is 64 bit and Office is 32 bit
                if (IsOS64Bit() && !IsOffice64Bit())
                {
                    myKey = Registry.LocalMachine.OpenSubKey(@"Software\Classes\QuickCross File\DefaultIcon", true);
                    if (myKey != null)
                    {
                        pathForInstallation = @"file:///" + myKey.GetValue("").ToString();
                        pathForInstallation = pathForInstallation.Replace(@"\QuickCross\file_icon_m.ico", "");

                        RegistryKey myKey1 = Registry.LocalMachine.OpenSubKey(@"Software\Wow6432Node\Microsoft\Office\Excel\Addins\QC4ExcelAddIn", true);
                        if (myKey1 != null)
                        {
                            using (RegistryKey key = Registry.LocalMachine)
                            {
                                key.DeleteSubKeyTree(@"Software\Wow6432Node\Microsoft\Office\Excel\Addins\QC4ExcelAddIn", false);
                            }
                        }

                        RegistryKey myKey2 = Registry.LocalMachine.OpenSubKey(@"Software\Wow6432Node\Microsoft\Office\Excel\Addins\QC4ExcelAddIn", true);
                        if (myKey2 == null)
                        {
                            using (var baseReg = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
                            {
                                using (RegistryKey key = baseReg.CreateSubKey(@"Software\Wow6432Node\Microsoft\Office\Excel\Addins\QC4ExcelAddIn"))
                                {
                                    key.SetValue("Description", "QC4ExcelAddIn");
                                    key.SetValue("FriendlyName", "ExcelAddIn");
                                    key.SetValue("LoadBehavior", 3, RegistryValueKind.DWord);
                                    key.SetValue("Manifest", pathForInstallation + @"\QuickCross\QC4ExcelAddIn.vsto|vstolocal");
                                }
                            }
                        } 
                        return;
                    }
                }
                else
                {
                    //Normal cases
                    myKey = Registry.LocalMachine.OpenSubKey(@"Software\Classes\QuickCross File\DefaultIcon", true);
                    if (myKey != null)
                    {
                        pathForInstallation = @"file:///" + myKey.GetValue("").ToString();
                        pathForInstallation = pathForInstallation.Replace(@"\QuickCross\file_icon_m.ico", "");

                        RegistryKey myKey1 = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Office\Excel\Addins\QC4ExcelAddIn", true);
                        if (myKey1 != null)
                        {
                            using (RegistryKey key = Registry.LocalMachine)
                            {
                                key.DeleteSubKeyTree(@"Software\Microsoft\Office\Excel\Addins\QC4ExcelAddIn", false);
                            }
                        }

                        RegistryKey myKey2 = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Office\Excel\Addins\QC4ExcelAddIn", true);
                        if (myKey2 == null)
                        {
                            using (var baseReg = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
                            {
                                using (RegistryKey key = baseReg.CreateSubKey(@"Software\Microsoft\Office\Excel\Addins\QC4ExcelAddIn"))
                                {
                                    key.SetValue("Description", "QC4ExcelAddIn");
                                    key.SetValue("FriendlyName", "ExcelAddIn");
                                    key.SetValue("LoadBehavior", 3, RegistryValueKind.DWord);
                                    key.SetValue("Manifest", pathForInstallation + @"\QuickCross\QC4ExcelAddIn.vsto|vstolocal");
                                }
                            }
                        }
                        return;
                    }
                }

                String currentpath = AppDomain.CurrentDomain.BaseDirectory;
                Process.Start(currentpath+ "QC4ExcelAddIn.vsto");
            }
            catch (Exception e) { try { MessageBox.Show(e.Message); } catch { } }
        }
        public static bool IsOS64Bit()
        {
            if (IntPtr.Size == 8)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public static bool IsOffice64Bit()
        {
            try
            {
                string officeBitness = string.Empty;
                RegistryKey officeKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Office\ClickToRun\Configuration");
                if (officeKey != null)
                {
                    officeBitness = officeKey.GetValue("Platform").ToString();
                }
                else
                {
                    officeKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Office");
                    if (officeKey != null)
                    {
                        string[] subKeys = officeKey.GetSubKeyNames();
                        foreach (string key in subKeys)
                        {
                            if (key.StartsWith("0"))
                            {
                                RegistryKey productKey = officeKey.OpenSubKey(key);
                                if (productKey != null)
                                {
                                    officeBitness = productKey.GetValue("Bitness").ToString();
                                    break;
                                }
                            }
                        }
                    }
                }

                if (officeBitness != "x64") return false;
                else return true;
            }
            catch { return false; }
        }
    }


}
