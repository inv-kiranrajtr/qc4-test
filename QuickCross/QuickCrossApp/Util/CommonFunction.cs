using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using System.Management;
using System.Management.Instrumentation;
using System.Windows.Media;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Macromill.QCWeb.Tabulation;

namespace Qc4Launcher.Util
{
    class CommonFunction
    {
        public static DateTime ExpiryDate { get; set; }

        internal static void CellFormatSetting(Range targetRange, string list, string inputTitle,
           int colorIndex = 0, string inputMessage = "", string errorTitle = "", string errorMessage = "",
           bool showError = true, bool cellLocked = false, XlHAlign cellHrztl = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter,
       string preValue = "", bool SheetProtect = true, XlIMEMode iMEMode = XlIMEMode.xlIMEModeDisable)
        {
            targetRange.Locked = cellLocked;
            targetRange.HorizontalAlignment = cellHrztl;
            targetRange.ShrinkToFit = (cellHrztl == XlHAlign.xlHAlignCenter);
            if (colorIndex != 0) { targetRange.Interior.ColorIndex = colorIndex; }
            if (preValue != "") { targetRange.Value = preValue; }


            Validation validation = targetRange.Validation;
            if (list.Equals(""))
            {
                validation.Delete();
                validation.Add(XlDVType.xlValidateInputOnly);
                if (inputTitle.Length > 25)
                {

                    validation.InputTitle = inputTitle;
                }
                else
                {

                    validation.InputTitle = inputTitle;
                }


                if (inputMessage.Equals(""))
                {
                    validation.InputMessage = inputMessage;
                }
                else
                {
                    validation.InputMessage = inputMessage;
                }
                validation.ShowInput = true;

            }
            else
            {
                validation.Delete();
                validation.Add(XlDVType.xlValidateList, Formula1: list, AlertStyle: XlDVAlertStyle.xlValidAlertStop);
                validation.InCellDropdown = true;
                validation.InputTitle = inputTitle;
                validation.InputMessage = inputMessage;
            }

            validation.IMEMode = (int)iMEMode;
            validation.ShowInput = (!inputTitle.Equals(""));
            validation.ErrorTitle = errorTitle;
            validation.ErrorMessage = errorMessage;
            validation.ShowError = showError;
        }

        public static bool ActivationKeyChecking(string pcInfo = "")
        {
            bool IsPro = true;
            string key = pcInfo;
            if (Constants.IsPro || pcInfo != "")
            {
                if (pcInfo == "")
                {
                    using (RegistryKey rKey = Registry.CurrentUser.CreateSubKey(Constants.RegistryPath))
                    {
                        key = Convert.ToString(rKey.GetValue(Constants.RegistrykeyName));
                        rKey.Close();
                    }
                }
                if (key != null && key != "")
                {
                    string info = Cryptography.Decrypt(key, Constants.EncryptDecryptPass);
                    string[] spltAry = info.Split('\t');
                    string UserDomainName = Environment.UserDomainName;
                    string UserName = Environment.UserName;
                    string MachineName = Environment.MachineName;
                    List<string> macAddresses = NetworkInterface
.GetAllNetworkInterfaces()
.Where(nic => nic.NetworkInterfaceType != NetworkInterfaceType.Loopback && nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
.Select(nic => nic.GetPhysicalAddress().ToString()).ToList();
                    if (spltAry.Length == 10)
                    {
                        if (spltAry[0] != "" && spltAry[0] != UserDomainName)
                            IsPro = false;
                        else if (spltAry[1] != "" && spltAry[1] != UserName)
                            IsPro = false;
                        else if (spltAry[2] != "" && spltAry[2] != MachineName)
                            IsPro = false;
                        else if (spltAry[3] != "" && validateMacAdrs(spltAry[3], macAddresses))
                            IsPro = false;
                        else if (spltAry[4] != Constants.QC4Key)
                            IsPro = false;
                        else if (spltAry[5] == null || spltAry[5] == "")
                            IsPro = false;
                        else if (spltAry[5] != null || spltAry[5] != "")
                        {
                            DateTime expDate;
                            DateTime.TryParseExact(spltAry[5], "dd-MM-yyyy",
                           CultureInfo.InvariantCulture,
                           DateTimeStyles.None, out expDate);
                            ExpiryDate = expDate;
                            if (expDate.ToString() == "01/01/0001 00:00:00")
                                IsPro = false;
                            else
                            {
                                if (expDate < DateTime.Now.Date)
                                    IsPro = false;
                            }
                        }
                    }
                    else
                        IsPro = false;
                }
                else
                    IsPro = false;

                Constants.IsPro = IsPro;
            }
            return Constants.IsPro;
        }

        private static bool validateMacAdrs(string spltAry, List<string> macAddresses)
        {
            for (int i = 0; i < macAddresses.Count; i++)
            {
                if (spltAry == macAddresses[i])
                {
                    return false;
                }
            }
            return true;
        }

        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }

        public static TabulationDescriptions SetDescriptionString()
        {
            return new TabulationDescriptions()
            {
                PreWBtotalDescription = LocalResource.REPORT_PRE_WB_WHOLE_DESCRIPTION_DEFAULT,
                TotalDescription = LocalResource.REPORT_TARGET_WHOLE_DESCRIPTION_DEFAULT,
                TotalAxisDescription = LocalResource.REPORT_AXIS_WHOLE_DESCRIPTION_DEFAULT,
                NADescription = LocalResource.REPORT_NA_DESCRIPTION_DEFAULT,
                IVDescription = LocalResource.REPORT_IV_DESCRIPTION_DEFAULT,
                ParameterDescription = LocalResource.REPORT_PARAMETER_DESCRIPTION_DEFAULT,
                SummaryDescription = LocalResource.REPORT_SUMMARY_DESCRIPTION_DEFAULT,
                AverageDescription = LocalResource.REPORT_AVERAGE_DESCRIPTION_DEFAULT,
                StdevDescription = LocalResource.REPORT_DEVIATION_DESCRIPTION_DEFAULT,
                MinDescription = LocalResource.REPORT_MINIMUM_DESCRIPTION_DEFAULT,
                MaxDescription = LocalResource.REPORT_MAXIMUM_DESCRIPTION_DEFAULT,
                MedianDescription = LocalResource.REPORT_MEDIAN_DESCRIPTION_DEFAULT,
            };
        }

        public static string TranslateOptionMessage(string message)
        {
            string msg = "";
            string[] spltMsg = message.Split('\n');
            for (int i = 0; i < spltMsg.Length; i++)
            {
                if (spltMsg[i].Contains("(") || spltMsg[i].Contains("【"))
                {
                    string delimeterOpen = "(";
                    string delimeterClose = ")";
                    if (spltMsg[i].Contains("【"))
                    {
                        delimeterOpen = "【";
                        delimeterClose = "】";
                    }
                    string text = spltMsg[i].Substring(spltMsg[i].IndexOf(delimeterOpen) + 1, spltMsg[i].IndexOf(delimeterClose) - (spltMsg[i].IndexOf(delimeterOpen) + 1));
                    msg += String.Format(LocalResource.FILTER_CRITERIA_MSG1, text);
                }
                else if (i + 1 == spltMsg.Length)
                    msg += Environment.NewLine + LocalResource.FILTER_CRITERIA_MSG3;
                else
                {
                    if (i == 0)
                        msg += LocalResource.FILTER_CRITERIA_MSG2;
                    else
                        msg += Environment.NewLine + LocalResource.FILTER_CRITERIA_MSG2;
                }
            }
            return msg;
        }

        public static string GetTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"MM\QuickCross\");
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }
    }

    public static class Log4NetExtension
    {
        public static void LogError(this ILog logger, string message, Microsoft.Office.Interop.Excel.Application excelapp = null)
        {
            string hotfix = null, lastdateupdate = null;
            string appinfo = string.Empty;

            try
            {
                const string querys = "SELECT HotFixID,InstalledOn FROM Win32_QuickFixEngineering";
                var search = new ManagementObjectSearcher(querys);
                var collection = search.Get();
                foreach (ManagementObject quickfix in collection)
                {
                    hotfix = quickfix["HotFixID"].ToString();
                    lastdateupdate = quickfix["InstalledOn"].ToString();
                }
            }
            catch (Exception e) { }

            string macrosettinginfo = null;
            try
            {
                if (Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\16.0\Excel\Security") != null)
                {

                    if (Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\16.0\Excel\Security").GetValue("VBAWarnings") == null)
                    {
                        macrosettinginfo = "2";
                    }
                    else
                    {
                        macrosettinginfo = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\16.0\Excel\Security").GetValue("VBAWarnings").ToString();
                    }

                }
                else
                {
                    if (Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\15.0\Excel\Security").GetValue("VBAWarnings") == null)
                    {
                        macrosettinginfo = "2";
                    }
                    else
                    {
                        macrosettinginfo = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\15.0\Excel\Security").GetValue("VBAWarnings").ToString();
                    }
                }
                switch (macrosettinginfo)
                {
                    case "1":
                        macrosettinginfo = "Enable allMacros";
                        break;
                    case "2":
                        macrosettinginfo = "Disable all macros with notification";
                        break;
                    case "3":
                        macrosettinginfo = "Disable all macros except digitally signed macros";
                        break;
                    case "4":
                        macrosettinginfo = "Disable all macros without notification";
                        break;
                    default:
                        macrosettinginfo = "Unable to get Marco status";
                        break;
                }
            }
            catch (Exception e) { }

            string activeXinfo = null;
            try
            {
                if (Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\Common\Security") != null)
                {
                    if (Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\Common\Security").GetValue("DisableAllActiveX") == null)
                    {
                        if (Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\Common\Security").GetValue("UFIControls").ToString() == "1")
                        {
                            activeXinfo = "Enable all controls (without safe mode)";
                        }
                        if (Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\Common\Security").GetValue("UFIControls").ToString() == "2")
                        {
                            activeXinfo = "Enable all controls (with safe mode)";
                        }
                        if (Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\Common\Security").GetValue("UFIControls").ToString() == "3")
                        {
                            activeXinfo = "Unsafe for UFI controls and safe SFI control with minimal restriction (without safe mode)";
                        }
                        if (Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\Common\Security").GetValue("UFIControls").ToString() == "4")
                        {
                            activeXinfo = "Unsafe for UFI controls and safe SFI control with minimal restriction (with safe mode)";
                        }
                        if (Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\Common\Security").GetValue("UFIControls").ToString() == "5")
                        {
                            activeXinfo = "Prompt me be before enabling all controls with minimal restrictions (without safe mode)";
                        }
                        if (Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\Common\Security").GetValue("UFIControls").ToString() == "6")
                        {
                            activeXinfo = "Prompt me be before enabling all controls with minimal restrictions (with safe mode)";
                        }
                    }
                    else if (Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\Common\Security").GetValue("DisableAllActiveX").ToString() == "1")
                    {
                        activeXinfo = "Disable all controls without notification";
                    }
                    else if (Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\Common\Security").GetValue("DisableAllActiveX").ToString() == "0")
                    {
                        if (Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\Common\Security").GetValue("UFIControls").ToString() == "1")
                        {
                            activeXinfo = "Enable all controls (without safe mode)";
                        }
                        if (Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\Common\Security").GetValue("UFIControls").ToString() == "2")
                        {
                            activeXinfo = "Enable all controls (with safe mode)";
                        }
                        if (Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\Common\Security").GetValue("UFIControls").ToString() == "3")
                        {
                            activeXinfo = "Unsafe for UFI controls and safe SFI control with minimal restriction (without safe mode)";
                        }
                        if (Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\Common\Security").GetValue("UFIControls").ToString() == "4")
                        {
                            activeXinfo = "Unsafe for UFI controls and safe SFI control with minimal restriction (with safe mode)";
                        }
                        if (Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\Common\Security").GetValue("UFIControls").ToString() == "5")
                        {
                            activeXinfo = "Prompt me be before enabling all controls with minimal restrictions (without safe mode)";
                        }
                        if (Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\Common\Security").GetValue("UFIControls").ToString() == "6")
                        {
                            activeXinfo = "Prompt me be before enabling all controls with minimal restrictions (with safe mode)";
                        }
                    }
                    else
                    {
                        activeXinfo = "Prompt me be before enabling all controls with minimal restrictions";
                    }
                }
                else
                {
                    activeXinfo = "Prompt me be before enabling all controls with minimal restrictions";
                }
            }
            catch (Exception e) { }
            string sqliteVer = "";
            try
            {
                Assembly assemblyy = Assembly.LoadFrom("System.Data.SQLite.dll");
                Version ver = assemblyy.GetName().Version;
                sqliteVer = ver.ToString();
            }
            catch { }
            Assembly assembly = Assembly.GetExecutingAssembly();
            CultureInfo ci = CultureInfo.InstalledUICulture;
            Microsoft.Office.Interop.Excel.Application appVersion = new Microsoft.Office.Interop.Excel.Application();
            string bit_Excel = string.Empty;
            if (System.Runtime.InteropServices.Marshal.SizeOf(appVersion.HinstancePtr) == 8)
            {
                bit_Excel = "64bit";
            }
            else
            {
                bit_Excel = "32bit";
            }
            appVersion.Visible = false;
            string systemType = Environment.Is64BitOperatingSystem ? "64-bit Operating System,x64 based processor" : "32-bit Operating System,x32 based processor";
            logger.Error(message + Environment.NewLine + "アプリ情報"
                + "\t" + Environment.NewLine + "QCバージョン: " + assembly.FullName.Split(',')[1]
                + "\t" + Environment.NewLine + "QC言語: " + LocalResource.Culture.DisplayName.ToString()
                + "\t" + Environment.NewLine + "QCインストールフォルダ場所: " + AppContext.BaseDirectory
                + Environment.NewLine + "端末・関連アプリ情報"
                + "\t" + Environment.NewLine + "OSバージョン: " + Environment.OSVersion + "(" + systemType + ")"
                + "\t" + Environment.NewLine + "OS言語: " + ci.EnglishName
                + "\t" + Environment.NewLine + "MicrosoftOfficeバージョン: " + appVersion.Version.ToString() + "(" + bit_Excel + ")"
                + "\t" + Environment.NewLine + "MicrosoftOffice言語: " + appVersion.LanguageSettings.get_LanguageID(Microsoft.Office.Core.MsoAppLanguageID.msoLanguageIDUI)
                + "\t" + Environment.NewLine + ".NetFrameworkバージョン: " + Get45PlusFromRegistry()
            + "\t" + Environment.NewLine + "その他、使用するアプリケーションのバージョン" + "  SQlite:" + sqliteVer
                + Environment.NewLine + "WindowsUpdate情報"
                + "\t" + Environment.NewLine + "Winupdate最終実行日時: " + lastdateupdate
                + "\t" + Environment.NewLine + "Winupdate最新適用パッチID: " + hotfix
                + Environment.NewLine + "Excel設定情報"
                + "\t" + Environment.NewLine + "Excelマクロ設定情報: " + macrosettinginfo
                + "\t" + Environment.NewLine + "ActiveX適用状況: " + activeXinfo);
            appVersion.Quit();
        }

        public static void LogFatal(this ILog logger, string message)
        {
            string systemType = Environment.Is64BitOperatingSystem ? "64-bit Operating System,x64 based processor" : "32-bit Operating System,x32 based processor";
            logger.Fatal(message + Environment.NewLine + "User Name: " + Environment.UserName
                + Environment.NewLine + "User Domain Name: " + Environment.UserDomainName
                + Environment.NewLine + "Machine Name: " + Environment.MachineName
                + Environment.NewLine + "OS: " + Environment.OSVersion
                + Environment.NewLine + "System type: " + systemType
                + Environment.NewLine + ".NetFrameworkバージョン: " + Get45PlusFromRegistry()
                + Environment.NewLine + "その他、使用するアプリケーションのバージョン" + "Other application versions");

        }

        public static void Logappinformation(this ILog logger, string message)
        {

            logger.Info(message);
        }
        public static string Loginfo(this ILog logger, string message, Microsoft.Office.Interop.Excel.Application excelapp = null)
        {

            string hotfix = null, lastdateupdate = null;
            string bit_Excel = string.Empty;
            Microsoft.Office.Interop.Excel.Application appVersion = new Microsoft.Office.Interop.Excel.Application();
            appVersion.Visible = false;
            string Excelversion = ReadREGValues.GetExcelVersion();// appVersion.Version.ToString();//= appVersion.Version.ToString() + "." + appVersion.Build.ToString();
            string Excelversion_Code = appVersion.Version.ToString() + "." + appVersion.Build.ToString();
            string Addinstatus = ReadREGValues.GetExcelAddinStatus();
            int excelLang = appVersion.LanguageSettings.get_LanguageID(Microsoft.Office.Core.MsoAppLanguageID.msoLanguageIDUI);
            if (System.Runtime.InteropServices.Marshal.SizeOf(appVersion.HinstancePtr) == 8)
            {
                bit_Excel = "64bit";
            }
            else
            {
                bit_Excel = "32bit";
            }
            appVersion.Quit();
            while (Marshal.ReleaseComObject(appVersion) != 0) { }
            appVersion = null;

            try
            {
                const string querys = "SELECT HotFixID,InstalledOn FROM Win32_QuickFixEngineering";
                var search = new ManagementObjectSearcher(querys);
                var collection = search.Get();
                foreach (ManagementObject quickfix in collection)
                {
                    hotfix = quickfix["HotFixID"].ToString();
                    lastdateupdate = quickfix["InstalledOn"].ToString();
                }
            }
            catch (Exception e) { }

            string macrosettinginfo = null;
            try
            {
                if (Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\16.0\Excel\Security") != null)
                {

                    if (Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\16.0\Excel\Security").GetValue("VBAWarnings") == null)
                    {
                        macrosettinginfo = "2";
                    }
                    else
                    {
                        macrosettinginfo = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\16.0\Excel\Security").GetValue("VBAWarnings").ToString();
                    }

                }
                else
                {
                    if (Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\15.0\Excel\Security").GetValue("VBAWarnings") == null)
                    {
                        macrosettinginfo = "2";
                    }
                    else
                    {
                        macrosettinginfo = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\15.0\Excel\Security").GetValue("VBAWarnings").ToString();
                    }
                }
                switch (macrosettinginfo)
                {
                    case "1":
                        macrosettinginfo = "Enable allMacros";
                        break;
                    case "2":
                        macrosettinginfo = "Disable all macros with notification";
                        break;
                    case "3":
                        macrosettinginfo = "Disable all macros except digitally signed macros";
                        break;
                    case "4":
                        macrosettinginfo = "Disable all macros without notification";
                        break;
                    default:
                        macrosettinginfo = "Unable to get Marco status";
                        break;
                }
            }
            catch (Exception e) { }

            string activeXinfo = null;
            try
            {
                if (Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\Common\Security") != null)
                {
                    if (Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\Common\Security").GetValue("DisableAllActiveX") == null)
                    {
                        if (Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\Common\Security").GetValue("UFIControls").ToString() == "1")
                        {
                            activeXinfo = "Enable all controls (without safe mode)";
                        }
                        if (Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\Common\Security").GetValue("UFIControls").ToString() == "2")
                        {
                            activeXinfo = "Enable all controls (with safe mode)";
                        }
                        if (Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\Common\Security").GetValue("UFIControls").ToString() == "3")
                        {
                            activeXinfo = "Unsafe for UFI controls and safe SFI control with minimal restriction (without safe mode)";
                        }
                        if (Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\Common\Security").GetValue("UFIControls").ToString() == "4")
                        {
                            activeXinfo = "Unsafe for UFI controls and safe SFI control with minimal restriction (with safe mode)";
                        }
                        if (Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\Common\Security").GetValue("UFIControls").ToString() == "5")
                        {
                            activeXinfo = "Prompt me be before enabling all controls with minimal restrictions (without safe mode)";
                        }
                        if (Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\Common\Security").GetValue("UFIControls").ToString() == "6")
                        {
                            activeXinfo = "Prompt me be before enabling all controls with minimal restrictions (with safe mode)";
                        }
                    }
                    else if (Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\Common\Security").GetValue("DisableAllActiveX").ToString() == "1")
                    {
                        activeXinfo = "Disable all controls without notification";
                    }
                    else if (Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\Common\Security").GetValue("DisableAllActiveX").ToString() == "0")
                    {
                        if (Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\Common\Security").GetValue("UFIControls").ToString() == "1")
                        {
                            activeXinfo = "Enable all controls (without safe mode)";
                        }
                        if (Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\Common\Security").GetValue("UFIControls").ToString() == "2")
                        {
                            activeXinfo = "Enable all controls (with safe mode)";
                        }
                        if (Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\Common\Security").GetValue("UFIControls").ToString() == "3")
                        {
                            activeXinfo = "Unsafe for UFI controls and safe SFI control with minimal restriction (without safe mode)";
                        }
                        if (Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\Common\Security").GetValue("UFIControls").ToString() == "4")
                        {
                            activeXinfo = "Unsafe for UFI controls and safe SFI control with minimal restriction (with safe mode)";
                        }
                        if (Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\Common\Security").GetValue("UFIControls").ToString() == "5")
                        {
                            activeXinfo = "Prompt me be before enabling all controls with minimal restrictions (without safe mode)";
                        }
                        if (Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\Common\Security").GetValue("UFIControls").ToString() == "6")
                        {
                            activeXinfo = "Prompt me be before enabling all controls with minimal restrictions (with safe mode)";
                        }
                    }
                    else
                    {
                        activeXinfo = "Prompt me be before enabling all controls with minimal restrictions";
                    }
                }
                else
                {
                    activeXinfo = "Prompt me be before enabling all controls with minimal restrictions";
                }
            }
            catch (Exception e) { }
            Assembly assemblyy = Assembly.LoadFrom("System.Data.SQLite.dll");
            Version ver = assemblyy.GetName().Version;
            Assembly assembly = Assembly.GetExecutingAssembly();
            CultureInfo ci = CultureInfo.InstalledUICulture;
            Util.CommonFunction common = new CommonFunction();
            string systemType = Environment.Is64BitOperatingSystem ? "64-bit Operating System,x64 based processor" : "32-bit Operating System,x32 based processor";
            message = message + Environment.NewLine + "アプリ情報"
                        + "\t" + Environment.NewLine + "QCバージョン: " + assembly.FullName.Split(',')[1]
                        + "\t" + Environment.NewLine + "QC言語: " + LocalResource.Culture.DisplayName.ToString()
                        + "\t" + Environment.NewLine + "QCインストールフォルダ場所: " + AppContext.BaseDirectory
                        + Environment.NewLine + "端末・関連アプリ情報"
                        + "\t" + Environment.NewLine + "OSバージョン: " + Environment.OSVersion + "(" + systemType + ")"
                        + "\t" + Environment.NewLine + "OS言語: " + ci.EnglishName
                        + "\t" + Environment.NewLine + "MicrosoftOfficeバージョン(Code): " + Excelversion_Code + "(" + bit_Excel + ")"
                        + "\t" + Environment.NewLine + "MicrosoftOfficeバージョン(Registry): " + Excelversion + "(" + bit_Excel + ")"
                        + "\t" + Environment.NewLine + "MicrosoftOffice言語: " + excelLang
                         + "\t" + Environment.NewLine + "すべてのアプリケーションアドインを無効にする: " + Addinstatus
                        + "\t" + Environment.NewLine + ".NetFrameworkバージョン: " + Get45PlusFromRegistry()
                    + "\t" + Environment.NewLine + "その他、使用するアプリケーションのバージョン" + "  SQlite:" + ver.ToString()
                        + Environment.NewLine + "WindowsUpdate情報"
                        + "\t" + Environment.NewLine + "Winupdate最終実行日時: " + lastdateupdate
                        + "\t" + Environment.NewLine + "Winupdate最新適用パッチID: " + hotfix
                        + Environment.NewLine + "Excel設定情報"
                        + "\t" + Environment.NewLine + "Excelマクロ設定情報: " + macrosettinginfo
                        + "\t" + Environment.NewLine + "ActiveX適用状況: " + activeXinfo;


            return message;
        }
        private static string Get45PlusFromRegistry()
        {
            const string subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";

            using (var ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkey))
            {
                if (ndpKey != null && ndpKey.GetValue("Release") != null)
                {
                    return $".NET Framework Version: {CheckFor45PlusVersion((int)ndpKey.GetValue("Release"))}";
                }
                else
                {
                    return ".NET Framework Version 4.5 or later is not detected.";
                }
            }

            // Checking the version using >= enables forward compatibility.
            string CheckFor45PlusVersion(int releaseKey)
            {
                if (releaseKey >= 528040)
                    return "4.8 or later";
                if (releaseKey >= 461808)
                    return "4.7.2";
                if (releaseKey >= 461308)
                    return "4.7.1";
                if (releaseKey >= 460798)
                    return "4.7";
                if (releaseKey >= 394802)
                    return "4.6.2";
                if (releaseKey >= 394254)
                    return "4.6.1";
                if (releaseKey >= 393295)
                    return "4.6";
                if (releaseKey >= 379893)
                    return "4.5.2";
                if (releaseKey >= 378675)
                    return "4.5.1";
                if (releaseKey >= 378389)
                    return "4.5";
                // This code should never execute. A non-null release key should mean
                // that 4.5 or later is installed.
                return "No 4.5 or later version detected";
            }
        }

    }
    public static class Cryptography
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region Settings

        private static int _iterations = 2;
        private static int _keySize = 256;

        private static string _hash = "SHA1";
        private static string _salt = "aselrias38490a32"; // Random

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
                _log.LogError("Encryption: " + ex.Message);
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
                    catch
                    {
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
                    catch
                    {
                        return String.Empty;
                    }

                    cipher.Clear();
                }
                return Encoding.UTF8.GetString(decrypted, 0, decryptedByteCount);
            }
            catch (Exception ex)
            {
                _log.LogError("Decryption: " + ex.Message);
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

    public static class ReadREGValues
    {
        public static string GetExcelVersion()
        {
            string version = string.Empty;
            try
            {
                const string subkey = @"SOFTWARE\Microsoft\Office\ClickToRun\Configuration";
                try
                {//HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Office\16.0\Common\ProductVersion
                    using (var excelregKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkey))
                    {
                        if (excelregKey != null && excelregKey.GetValue("VersionToReport") != null)
                        {
                            return excelregKey.GetValue("VersionToReport").ToString();
                        }
                    }
                }
                catch { }
                try
                {
                    using (var excelregKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(subkey))
                    {
                        if (excelregKey != null && excelregKey.GetValue("VersionToReport") != null)
                        {
                            return excelregKey.GetValue("VersionToReport").ToString();
                        }
                    }
                }
                catch { }
                
            }
            catch { }
            return version;
        }
        public static string GetExcelAddinStatus()
        {
            string Addinstatus = Constants.Status.Disable;


            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Office\16.0\Excel\Security"))
                {
                    if (key != null)
                    {
                        Object value = key.GetValue("DisableAllAddins");
                        if (value != null && value.ToString().Equals("1"))
                        {
                            return Constants.Status.Enable;
                        }
                    }
                }
            }
            catch { }

            return Addinstatus;
        }
    }
}
