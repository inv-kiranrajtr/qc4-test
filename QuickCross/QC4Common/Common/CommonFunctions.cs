using log4net;
using Microsoft.Office.Interop.Excel;
using QC4Common.Model;
using QC4Common.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Collections;
using System.Globalization;
using Microsoft.Win32;
using System.Management;

namespace QC4Common.Common
{
    public class CommonFunctions
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        internal static void CellFormatSetting(Range targetRange, string list, string inputTitle,
            int colorIndex = 0, string inputMessage = "", string errorTitle = "", string errorMessage = "",
            bool showError = false, bool cellLocked = false, XlHAlign cellHrztl = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter,
        string preValue = "", bool SheetProtect = true, XlIMEMode iMEMode = XlIMEMode.xlIMEModeDisable)
        {
            targetRange.Locked = cellLocked;
            targetRange.HorizontalAlignment = cellHrztl;
            targetRange.ShrinkToFit = (cellHrztl == XlHAlign.xlHAlignCenter);
            if (colorIndex != 0) { targetRange.Interior.ColorIndex = colorIndex; }
            if (preValue != "") { targetRange.Value = preValue; }


           
            Microsoft.Office.Interop.Excel.Validation validation = targetRange.Validation;
            if (list.Equals(""))
            {
                validation.Delete();
                validation.Add(XlDVType.xlValidateInputOnly);
                if (inputTitle.Length > 25)
                {
                    //validation.InputTitle = Str_LeftB & Left(Input_Title, 25) & Str_RightB_Pattern1
                    validation.InputTitle = inputTitle;
                }
                else
                {
                    // .InputTitle = Str_LeftB & Input_Title & Str_RightB_Pattern2
                    validation.InputTitle = inputTitle;
                }

                //.InputMessage = IIf(Input_Message = "", Str_Please_Select & Input_Title, Input_Message) 'Global対応
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
            // TO DO: need to match with QC3 code
            validation.IMEMode = (int)iMEMode;
            validation.ShowInput = (!inputTitle.Equals(""));
            validation.ErrorTitle = errorTitle;
            validation.ErrorMessage = errorMessage;
            validation.ShowError = showError;
        }

        public SigSettings GetGTSigSettings(Workbook workBook)
        {
            SigSettings sigSettings = new SigSettings();
            try
            {
                Worksheet SettingSheet = ExcelUtil.GetWorkSheetByCodeName(workBook, Constants.SheetCodeName.Setting);
                sigSettings.IsSig1Checked = Convert.ToBoolean(SettingSheet.Range["D12"].Value);
                sigSettings.IsSig5Checked = Convert.ToBoolean(SettingSheet.Range["D13"].Value);
                sigSettings.IsSig10Checked = Convert.ToBoolean(SettingSheet.Range["D14"].Value);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message + "\n" + ex.StackTrace);
            }
            return sigSettings;
        }

        public bool UpdateSigSettings(Workbook workBook, SigSettings sigSettings)
        {
            try
            {
                Worksheet SettingSheet = ExcelUtil.GetWorkSheetByCodeName(workBook, Constants.SheetCodeName.Setting);
                SettingSheet.Range["D12"].Value = sigSettings.IsSig1Checked.ToString();
                SettingSheet.Range["D13"].Value = sigSettings.IsSig5Checked.ToString();
                SettingSheet.Range["D14"].Value = sigSettings.IsSig10Checked.ToString();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message + "\n" + ex.StackTrace);
                return false;
            }
            return true;
        }


        public SigSettings MakeGTSigSettings(int sigMsgValue)
        {
            return new SigSettings()
            {
                IsSig1Checked = sigMsgValue.ToString().Contains('1'),
                IsSig5Checked = sigMsgValue.ToString().Contains('2'),
                IsSig10Checked = sigMsgValue.ToString().Contains('3')
            };
        }
        /// <summary>
        /// Format the message text by limitting 254 characters
        /// </summary>
        /// <param name="mCONVERT_ARG_TOOLTIP_DESC">Message text</param>
        /// <param name="choice">Choice count</param>
        /// <returns>return formatted message</returns>
        /// <remarks>Implemeted this method as per Redmine Id:235106</remarks>
        public static string FormatMsg(string mCONVERT_ARG_TOOLTIP_DESC, string choice)
        {
            string tooltipMsg = string.Format(mCONVERT_ARG_TOOLTIP_DESC, choice);
            if (tooltipMsg.Length > 254)
            {
                int clearCharLen = (tooltipMsg.Length - 254) + 3;
                tooltipMsg = string.Format(mCONVERT_ARG_TOOLTIP_DESC, choice.Substring(0, choice.Length - clearCharLen) + "...");
            }
            return tooltipMsg;
        }

       
        /// <summary>
        /// Function to Remove Full width characters from a given string
        /// </summary>
        /// <param name="text">Inputs a string may or maynot containing fullwidth characters</param>
        /// <returns>Returns a string without any full width character in it </returns>
        [STAThread]
        public string RemoveFullWidth(String text, int datagridColumn)
        {
            var rows = text.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
            StringBuilder filteredText = new StringBuilder();
            for (int i = 0; i < rows.Count; i++)
            {
                string col = "";
                var cols = rows[i].Split(new string[] { "\t" }, StringSplitOptions.None).ToList();
                if (datagridColumn == 1)
                {
                    if (cols.Count > 1)
                    {
                        filteredText.Append(cols[0] + "\t");
                        col = cols[1];
                    }
                    else
                    {
                        filteredText.Append(cols[0]);
                    }
                }
                else if (datagridColumn == 2)
                {
                    col = cols[0];
                }
                if (datagridColumn == 2 || (datagridColumn == 1 && cols.Count > 1))
                {
                    for (int j = 0; j < col.Length; j++)
                    {
                        //check whether the char is full width or not
                        if (col[j].GetHashCode() < 0 || col[j] == '　')
                            break;
                        filteredText.Append(col[j]);
                    }
                }
                if ((i + 1) < rows.Count)
                    filteredText.Append("\r\n");
            }
            return filteredText.ToString();
        }
        /// <summary>
        /// Filter the digits from string by matching the Regx
        /// </summary>
        /// <param name="text">the text contains digits or non digits</param>
        /// <returns>return digits</returns>
        public string DataString(string text)
        {
            string coldata = string.Empty;
            if (text.StartsWith("."))
            {
                text = "0" + text;
            }
            if (Regex.Match(text, @"^(-?\d+)(.*)$").Success)
            {
                string digits = Regex.Match(text, @"^-?\d+(,\d+)*(\.\d+)?$").Value; 
                if (Regex.Match(digits, @"\d+").Success)
                {
                    double isDatadigit = 0;
                    if (double.TryParse(digits, out isDatadigit))
                    {
                        if (Math.Abs(isDatadigit) >= 0)
                        {
                            coldata = digits.Replace(",","");
                        }
                        else
                        {
                            coldata = string.Empty;
                        }
                    }
                    else
                    {
                        coldata = string.Empty;
                    }
                }
            }
            if (coldata.Length>30)
            coldata = coldata.Substring(0, 30);
            return coldata;
        }
    }
    public static class Log4NetExtension
    {
        public static void LogErrorForExcel(this ILog logger, string message)
        {
            string hotfix = null, lastdateupdate = null;
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
            Assembly assemblyy = null;
            Version vers = null;
            string ver = "";
            try
            {
                assemblyy = Assembly.LoadFrom("System.Data.SQLite.dll");
                vers = assemblyy.GetName().Version;
                ver = vers != null ? vers.ToString() : "";
            }
            catch { }
            Assembly assembly = Assembly.GetExecutingAssembly();
            CultureInfo ci = CultureInfo.InstalledUICulture;
            Microsoft.Office.Interop.Excel.Application appVersion = new Microsoft.Office.Interop.Excel.Application();
            appVersion.Visible = false;
            string displayName = CommonResource.Culture == null ? "" : CommonResource.Culture.DisplayName.ToString();
            string systemType = Environment.Is64BitOperatingSystem ? "64-bit Operating System,x64 based processor" : "32-bit Operating System,x32 based processor";
            logger.Error(message + Environment.NewLine + "アプリ情報"
                + "\t" + Environment.NewLine + "QCバージョン: " + assembly.FullName.Split(',')[1]
                + "\t" + Environment.NewLine + "QC言語: " + displayName
                + "\t" + Environment.NewLine + "QCインストールフォルダ場所: " + AppContext.BaseDirectory
                + Environment.NewLine + "端末・関連アプリ情報"
                + "\t" + Environment.NewLine + "OSバージョン: " + Environment.OSVersion + "(" + systemType + ")"
                + "\t" + Environment.NewLine + "OS言語: " + ci.EnglishName
                + "\t" + Environment.NewLine + "MicrosoftOfficeバージョン: " + appVersion.Version.ToString()
                + "\t" + Environment.NewLine + "MicrosoftOffice言語: " + appVersion.LanguageSettings.get_LanguageID(Microsoft.Office.Core.MsoAppLanguageID.msoLanguageIDUI)
                + "\t" + Environment.NewLine + ".NetFrameworkバージョン: " + Get45PlusFromRegistry()
            + "\t" + Environment.NewLine + "その他、使用するアプリケーションのバージョン" + "  SQlite:" + ver
                + Environment.NewLine + "WindowsUpdate情報"
                + "\t" + Environment.NewLine + "Winupdate最終実行日時: " + lastdateupdate
                + "\t" + Environment.NewLine + "Winupdate最新適用パッチID: " + hotfix
                + Environment.NewLine + "Excel設定情報"
                + "\t" + Environment.NewLine + "Excelマクロ設定情報: " + macrosettinginfo
                + "\t" + Environment.NewLine + "ActiveX適用状況: " + activeXinfo);
            //your code
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
            //your code
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
}
