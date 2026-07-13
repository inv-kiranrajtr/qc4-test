using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Reflection;
using log4net;
using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;

namespace ExcelAddIn.Common
{
    internal class CommonFunctions
    {
        internal static void CellFormatInitialize(Range targetRange, bool colorFlag = true)
        {
            Application application = targetRange.Application;
            bool eEFlag = application.EnableEvents;
            application.EnableEvents = false;

            if (colorFlag)
            {
                targetRange.Interior.ColorIndex = XlColorIndex.xlColorIndexNone;
            }
            targetRange.Font.ColorIndex = 0;
			//targetRange.ClearContents();
			QC4Common.Util.ExcelUtil.ClearContents(targetRange);
			targetRange.Validation.Delete();
            targetRange.Locked = true;
            application.EnableEvents = eEFlag;

        }

        internal static void CellFormatInitializeForQSCatChange(Range targetRange, bool colorFlag = true)
        {
            Application application = targetRange.Application;
            bool eEFlag = application.EnableEvents;
            application.EnableEvents = false;

            QC4Common.Util.ExcelUtil.ClearContents(targetRange);
            targetRange.Validation.Delete();
            application.FindFormat.Interior.Color = Constants.Color.LightGrey;
            Range LastCellRow = targetRange.Find("", SearchOrder: XlSearchOrder.xlByRows, SearchDirection: XlSearchDirection.xlPrevious, SearchFormat: true);
            if (LastCellRow != null && LastCellRow.Column >= targetRange.Column)
            {
                Range r1 = targetRange.Worksheet.Cells[targetRange.Row, targetRange.Column];
                Range r2 = targetRange.Worksheet.Cells[targetRange.Row, LastCellRow.Column];
                targetRange = targetRange.Worksheet.get_Range(r1, r2);
                if (colorFlag)
                {
                    targetRange.Interior.ColorIndex = XlColorIndex.xlColorIndexNone;
                }
                targetRange.Font.ColorIndex = 0;
            }
            application.FindFormat.Clear();
            application.EnableEvents = eEFlag;

        }

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


            //If Sheet_Protect = True Then
            //    Call FNC_QC_Sheet_UnProtect(Target_Range.Worksheet)
            //Else
            //If Target_Range.Worksheet.ProtectContents = True Then
            //    Call FNC_QC_Sheet_UnProtect(Target_Range.Worksheet)
            //End If
            //End If
            Validation validation = targetRange.Validation;
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
                //if(list.Length>255)//[Redmine id: 189027]
                //{
                //    Microsoft.Office.Interop.Excel.Worksheet settingssheet = ExcelUtil.GetWorkSheetByCodeName(targetRange.Application.ActiveWorkbook, Constants.SheetCodeName.Setting);
                //    list = "= INDIRECT(" + "\"" + settingssheet.Name + "!$A$1:$A$" + nComboValueCount + "\")";
                //}
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

        internal static string CateList(int cateCnt, bool noDK)
        {
            String sep = CultureInfo.CurrentCulture.TextInfo.ListSeparator;
            if (cateCnt == 0)
            {
                return "";
            }
            string catList = String.Join(sep, Enumerable.Range(1, cateCnt).Select((x => x)));
            if (noDK == false)
            {
                catList += sep+"DK"+sep+"*";
            }
            return catList;
        }

        internal static void CloseUpRange(Range targetCell)
        {
            ArrayList arr = new ArrayList();

            dynamic dataArray = targetCell.Value;
            if (dataArray == null)
                return;
            else if (dataArray.GetType().IsArray)
            {
                int x = 1;
                for (int y = 1; y <= dataArray.GetLength(1); y += 1)
                {
                    if (dataArray[x, y] != null)
                    {
                        arr.Add(dataArray[x, y]);
                    }
                }
            }
            else
            {
                arr.Add(targetCell.Value);
            }
            //targetCell.ClearContents();
            //QC4Common.Util.ExcelUtil.ClearContents(targetCell);
            targetCell.Value = null;
			object[,] arrTarget = new object[1, arr.Count];
            int i = 0;
            foreach (object item in arr)
            {
                var cell = (Range)targetCell.Worksheet.Cells[targetCell.Row, targetCell.Column + i++];
                //if (!Definitions.VariableDictionary.ContainsKey(item.ToString()) && targetCell.Column != 2)
                //{
                //    MessageDialog.ErrorOk(targetCell.Row + AddinResource.ERR_MSG_INVALID_ITEM);
                //    return;
                //}
                //else
                    cell.Value2 = item;
            }
        }
    }
    public static class Log4NetExtension
    {
        public static void LogError(this ILog logger, string message)
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
            string displayName = AddinResource.Culture == null ? "" : AddinResource.Culture.DisplayName.ToString();
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

