using Microsoft.Win32;
using Setup.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace QuickCROSS_Setup
{
    class Program
    {
        private static class NativeFunctions
        {
            public enum StdHandle : int
            {
                STD_INPUT_HANDLE = -10,
                STD_OUTPUT_HANDLE = -11,
                STD_ERROR_HANDLE = -12,
            }

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern IntPtr GetStdHandle(int nStdHandle); //returns Handle

            public enum ConsoleMode : uint
            {
                ENABLE_ECHO_INPUT = 0x0004,
                ENABLE_EXTENDED_FLAGS = 0x0080,
                ENABLE_INSERT_MODE = 0x0020,
                ENABLE_LINE_INPUT = 0x0002,
                ENABLE_MOUSE_INPUT = 0x0010,
                ENABLE_PROCESSED_INPUT = 0x0001,
                ENABLE_QUICK_EDIT_MODE = 0x0040,
                ENABLE_WINDOW_INPUT = 0x0008,
                ENABLE_VIRTUAL_TERMINAL_INPUT = 0x0200,

                //screen buffer handle
                ENABLE_PROCESSED_OUTPUT = 0x0001,
                ENABLE_WRAP_AT_EOL_OUTPUT = 0x0002,
                ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004,
                DISABLE_NEWLINE_AUTO_RETURN = 0x0008,
                ENABLE_LVB_GRID_WORLDWIDE = 0x0010
            }

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);
        }

        public static void QuickEditMode(bool Enable)
        {
            //QuickEdit lets the user select text in the console window with the mouse, to copy to the windows clipboard.
            //But selecting text stops the console process (e.g. unzipping). This may not be always wanted.
            IntPtr consoleHandle = NativeFunctions.GetStdHandle((int)NativeFunctions.StdHandle.STD_INPUT_HANDLE);
            UInt32 consoleMode;

            NativeFunctions.GetConsoleMode(consoleHandle, out consoleMode);
            if (Enable)
                consoleMode |= ((uint)NativeFunctions.ConsoleMode.ENABLE_QUICK_EDIT_MODE);
            else
                consoleMode &= ~((uint)NativeFunctions.ConsoleMode.ENABLE_QUICK_EDIT_MODE);

            consoleMode |= ((uint)NativeFunctions.ConsoleMode.ENABLE_EXTENDED_FLAGS);

            NativeFunctions.SetConsoleMode(consoleHandle, consoleMode);
        }

        static void Main(string[] args)
        {
            //Setting up path
            QuickEditMode(false);
            Console.WriteLine("Initializing...");
            //fix for #251475 Change in QuicCrossInstaller path
            string temp;
            if (args.Length == 2)
                temp = GetTemporaryDirectoryForSilent(args[1].ToString());
            else if (args.Length == 1)
                temp = GetTemporaryDirectoryForSilent(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86));
            else
                temp = GetTemporaryDirectory();

            bool noProcessWait = true;
            Process process = new Process();

            Console.WriteLine("Installing Redistributables...");

            //Adding Certificates required
            X509Certificate2 cert = new X509Certificate2(Properties.Resources.AddInCertificate);
            X509Store store = new X509Store(StoreName.TrustedPublisher, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadWrite);
            store.Add(cert);
            store.Close();

            //checking .netFrameWork 4.6  or above is installed
            bool netFrameWorkAvailability = Get45PlusFromRegistry();

            if (netFrameWorkAvailability)
            {
                string[] registryPaths =
                {
                    @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\VSTO Runtime Setup\v4R",
                    @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\VSTO Runtime Setup\v4"
                };

                string[] registryPathsWow64 =
                {
                    @"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\VSTO Runtime Setup\v4R",
                    @"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\VSTO Runtime Setup\v4"
                };

                bool vstoInstalled =
                    registryPaths.Any(p => Registry.GetValue(p, "Version", null) != null) ||
                    (Environment.Is64BitOperatingSystem &&
                     registryPathsWow64.Any(p => Registry.GetValue(p, "Version", null) != null));

                //Checking vsto is installed
                if (vstoInstalled)
                {
                    Console.WriteLine("Vsto is already installed...");
                }
                else
                {
                    try
                    {
                        Console.WriteLine("Installing Vsto Runtime... \n Please wait... it will take some time...");
                        string path = Path.Combine(temp, @"vstor_redist.exe");
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }
                        File.WriteAllBytes(path, Properties.Resources.vstor_redist);
                        ProcessStartInfo startInfo = new ProcessStartInfo(path);
                        startInfo.Arguments = "/q /norestart";
                        process.StartInfo = startInfo;
                        process.Start();

                        noProcessWait = process.WaitForExit(120000);
                        Console.WriteLine("Succesffully Installed 'vsto 2010'");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Vsto Runtime Installation failed... Press any key to exit and Try again...");
                        Console.ReadKey();
                        return;
                    }
                }


                if (noProcessWait) //To check weather process is exited by elapsed time 
                {
                    //Checking OSVersion
                    string path = Path.Combine(temp, @"Quick-CROSS4_64.msi");
                    if (Environment.Is64BitOperatingSystem)
                    {
                        Console.WriteLine("Installing 'Quick-CROSS4 x64'...Please wait until installation completes");

                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }
                        File.WriteAllBytes(path, Properties.Resources.Quick_CROSS4_64);


                        //process.WaitForExit(120000);
                        //Console.WriteLine("Succesffully Installed 'Quick-CROSS4 x64'");
                    }
                    else
                    {
                        Console.WriteLine("Installing 'Quick-CROSS4 x86'...Please wait until installation completes");

                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }
                        File.WriteAllBytes(path, Properties.Resources.Quick_CROSS4_86);

                        //process.WaitForExit(120000);
                        //Console.WriteLine("Succesffully Installed 'Quick-CROSS4 x86'");
                    }
                    if (File.Exists(path))
                    {
                        string param = string.Empty;
                        string silentparam = "/S";
                        string silentparamWithAddin = "/S/addin";
                        string addinOnly = "/addin";
                        string uninstallparam = "/X";
                        string menuparam = "/?";
                        string adminprev = (IsAdministrator() == true) ? "1" : string.Empty;
                        // MessageBox.Show("leng:" + args.Length.ToString());
                        // MessageBox.Show("admin:" + adminprev);

                        foreach (string arg in args)
                        {
                            param += arg;
                        }
                        if (args.Length == 1)
                        {
                            if (param.Trim().Equals(silentparam, StringComparison.CurrentCultureIgnoreCase))//silent install
                            {
                                ProcessStartInfo startInfo = new ProcessStartInfo("msiexec");
                                string npath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
                                string arguments = "/i \"" + path + "\" ALLUSERS=" + adminprev + " TARGETDIR=\"" + npath + "\" /qn+ /norestart /L*V \"" + temp + @"\" + "Quick-CROSS4.log\" ";
                                if (string.IsNullOrEmpty(adminprev))
                                {
                                    arguments = "/i \"" + path + "\"  TARGETDIR=\"" + npath + "\" /qn+ /norestart /L*V \"" + temp + @"\" + "Quick-CROSS4.log\" ";
                                }
                                // MessageBox.Show(args.Length + " ------ " + arguments);
                                startInfo.Arguments = arguments;
                                process.StartInfo = startInfo;
                                process.Start();
                                try
                                {
                                    UnInstallExcelAddIn();//uninstall addin if exists
                                }
                                catch { }


                            }
                            else if (param.Trim().Equals(silentparamWithAddin, StringComparison.CurrentCultureIgnoreCase))//silent install with addin
                            {
                                ProcessStartInfo startInfo = new ProcessStartInfo("msiexec");
                                string npath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
                                string arguments = "/i \"" + path + "\" ALLUSERS=" + adminprev + " TARGETDIR=\"" + npath + "\" /qn+ /norestart /L*V \"" + temp + @"\" + "Quick-CROSS4.log\" ";
                                if (string.IsNullOrEmpty(adminprev))
                                {
                                    arguments = "/i \"" + path + "\"  TARGETDIR=\"" + npath + "\" /qn+ /norestart /L*V \"" + temp + @"\" + "Quick-CROSS4.log\" ";
                                }
                                // MessageBox.Show(args.Length + " ------ " + arguments);
                                startInfo.Arguments = arguments;
                                process.StartInfo = startInfo;
                                process.Start();
                                try
                                {
                                    //Install Addin
                                    String pathforaddin = @"file:///" + npath;
                                    UnInstallExcelAddIn();
                                    InstallExcelAddIn(pathforaddin);
                                }
                                catch { }
                            }
                            else if (param.Trim().Equals(addinOnly, StringComparison.CurrentCultureIgnoreCase))//Install Addin only
                            {
                                try
                                {
                                    //Install Addin
                                    String installedpath = @"file:///" + GetPathForInstallation();
                                    UnInstallExcelAddIn();
                                    InstallExcelAddIn(installedpath);
                                }
                                catch { }
                            }
                            else if (param.Trim().Equals(uninstallparam, StringComparison.CurrentCultureIgnoreCase))//uninstall
                            {
                                ProcessStartInfo startInfo = new ProcessStartInfo("msiexec");
                                string arguments = "/x \"" + path + "\" ALLUSERS=" + adminprev + " /qn+ /norestart /L*V \"" + temp + @"\" + "Quick-CROSS4.log\" ";
                                if (string.IsNullOrEmpty(adminprev))
                                {
                                    arguments = "/x \"" + path + "\"  /qn+ /norestart /L*V \"" + temp + @"\" + "Quick-CROSS4.log\" ";
                                }
                                // MessageBox.Show(args.Length + " ------ " + arguments);
                                startInfo.Arguments = arguments;
                                process.StartInfo = startInfo;
                                process.Start();
                                try
                                {
                                    UnInstallExcelAddIn();//Uninstalls addin
                                }
                                catch { }
                            }
                            else if (param.Trim().Equals(menuparam, StringComparison.CurrentCultureIgnoreCase)) // /? menu
                            {
                                //english installer
                                string msg = "Command line parameters:" +
                                               "\t" + Environment.NewLine + "Default installation: QuickCross-Setup.exe /S " +
                                               "\t" + Environment.NewLine + "Specific path installation: QuickCross-Setup.exe /S [Path of the new installation directory enclosed in double quotes]" +
                                               "\t" + Environment.NewLine + "Uninstallation: QuickCross-Setup.exe /X ";
                                //japanese installr
                                //string msg = "コマンドライン引数:" +
                                //               "\t" + Environment.NewLine + "既定パスへのインストール: QuickCross-Setup.exe /S " +
                                //               "\t" + Environment.NewLine + "指定パスへのインストール: QuickCross-Setup.exe /S [二重引用符で括ったインストール先ディレクトリのパス]" +
                                //               "\t" + Environment.NewLine + "アンインストール: QuickCross-Setup.exe /X ";
                                MessageBox.Show(msg, "QuickCross Installer Help");

                                return;
                            }
                        }


                        else if (args.Length == 2 && args[0].Trim().Equals(silentparamWithAddin, StringComparison.CurrentCultureIgnoreCase))//with path and silentaddin install
                        {
                            string npath = string.Empty;
                            npath = args[1].ToString();
                            ProcessStartInfo startInfo = new ProcessStartInfo("msiexec");
                            string arguments = "/i \"" + path + "\" ALLUSERS=" + adminprev + " TARGETDIR=\"" + npath + "\" /qn+ /norestart /L*V \"" + temp + @"\" + "Quick-CROSS4.log\" ";
                            if (string.IsNullOrEmpty(adminprev))
                            {
                                arguments = "/i \"" + path + "\"  TARGETDIR=\"" + npath + "\" /qn+ /norestart /L*V \"" + temp + @"\" + "Quick-CROSS4.log\" ";
                            }
                            // MessageBox.Show(args.Length + " ------ " + arguments);
                            startInfo.Arguments = arguments;
                            process.StartInfo = startInfo;
                            process.Start();
                            try
                            {
                                //Install Addin
                                String pathforaddin = @"file:///" + npath;
                                UnInstallExcelAddIn();
                                InstallExcelAddIn(pathforaddin);
                            }
                            catch { }
                        }


                        else if (args.Length == 2 && args[0].Trim().Equals(addinOnly, StringComparison.CurrentCultureIgnoreCase))//with path and silent addinonly install
                        {
                            try
                            {
                                string npath = string.Empty;
                                npath = args[1].ToString();
                                //Install Addin
                                npath = @"file:///" + npath;
                                UnInstallExcelAddIn();
                                InstallExcelAddIn(npath);
                            }
                            catch { }
                        }



                        else if (args.Length == 2)//with path
                        {
                            string npath = string.Empty;
                            npath = args[1].ToString();
                            ProcessStartInfo startInfo = new ProcessStartInfo("msiexec");
                            string arguments = "/i \"" + path + "\" ALLUSERS=" + adminprev + " TARGETDIR=\"" + npath + "\" /qn+ /norestart /L*V \"" + temp + @"\" + "Quick-CROSS4.log\" ";
                            if (string.IsNullOrEmpty(adminprev))
                            {
                                arguments = "/i \"" + path + "\"  TARGETDIR=\"" + npath + "\" /qn+ /norestart /L*V \"" + temp + @"\" + "Quick-CROSS4.log\" ";
                            }
                            // MessageBox.Show(args.Length + " ------ " + arguments);
                            startInfo.Arguments = arguments;
                            process.StartInfo = startInfo;
                            process.Start();
                            try
                            {
                                UnInstallExcelAddIn();//uninstall addin if exists
                            }
                            catch { }
                        }
                        else//UI
                        {
                            process = Process.Start(path);
                            //                          
                        }
                    }
                    //MessageBox.Show(Convert.ToString(process.ExitCode));
                }
                else
                {
                    Console.WriteLine("Something went wrong....Installing 'vsto 2010' unsuccessfull...\n Please exit and try again...");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Please install .NET Framework Version 4.6 or Higher....\n Press any key to exit...");
                MessageBox.Show(".NET Frameworkバージョン4.6以降をインストールしてください。\n 終了するには[OK]を押してください。", "QuickCross", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.ReadKey();
            }

        }

        public static string GetTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "QuicCrossInstaller");
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }

        public static string GetTemporaryDirectoryForSilent(string path = null)
        {
            //#251475 Change in QuicCrossInstaller path
            string tempDirectory = Path.Combine(path, "QuicCrossInstaller");
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }

        private static bool Get45PlusFromRegistry()
        {
            const string subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";

            using (var ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkey))
            {
                if (ndpKey != null && ndpKey.GetValue("Release") != null)
                {
                    string value = CheckFor45PlusVersion((int)ndpKey.GetValue("Release"));
                    if (value.Equals("0"))
                    {
                        Console.WriteLine($".NET Framework Version 4.6 or later is not detected.");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine($".NET Framework Version: {value}");
                        return true;
                    }
                }
                else
                {
                    Console.WriteLine(".NET Framework Version 4.6 or later is not detected.");
                    return false;
                }
            }

            // Checking the version using >= enables forward compatibility.
            string CheckFor45PlusVersion(int releaseKey)
            {
                if (releaseKey >= 528040)
                    return "4.8 or later";
                else if (releaseKey >= 461808)
                    return "4.7.2";
                else if (releaseKey >= 461308)
                    return "4.7.1";
                else if (releaseKey >= 460798)
                    return "4.7";
                else if (releaseKey >= 394802)
                    return "4.6.2";
                else if (releaseKey >= 394254)
                    return "4.6.1";
                else if (releaseKey >= 393295)
                    return "4.6";
                else
                    return "0";
            }
        }

        public static bool IsAdministrator()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }


        public static bool IsAddInInstalled()
        {
            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Office\Excel\Addins\QC4ExcelAddIn", true);
            if (myKey != null)
                return true;
            else
                return false;
        }

        public static void InstallExcelAddIn(String path)
        {
            try
            {
                if (IsOS64Bit() && !IsOffice64Bit())
                {
                    //When OS  is 64 and Office is 32 Bit
                    RegistryKey myKey2 = Registry.LocalMachine.OpenSubKey(@"Software\Wow6432Node\Microsoft\Office\Excel\Addins\QC4ExcelAddIn", true);
                    if (myKey2 == null)
                    {
                        using (RegistryKey key = Registry.LocalMachine.CreateSubKey(@"Software\Wow6432Node\Microsoft\Office\Excel\Addins\QC4ExcelAddIn"))
                        {
                            key.SetValue("Description", "QC4ExcelAddIn");
                            key.SetValue("FriendlyName", "ExcelAddIn");
                            key.SetValue("LoadBehavior", 3, RegistryValueKind.DWord);
                            key.SetValue("Manifest", path + @"\QuickCross\QC4ExcelAddIn.vsto|vstolocal");
                        }

                    }
                }
                else
                {
                    RegistryKey myKey2 = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Office\Excel\Addins\QC4ExcelAddIn", true);
                    if (myKey2 == null)
                    {
                        using (RegistryKey key = Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Office\Excel\Addins\QC4ExcelAddIn"))
                        {
                            key.SetValue("Description", "QC4ExcelAddIn");
                            key.SetValue("FriendlyName", "ExcelAddIn");
                            key.SetValue("LoadBehavior", 3, RegistryValueKind.DWord);
                            key.SetValue("Manifest", path + @"\QuickCross\QC4ExcelAddIn.vsto|vstolocal");
                        }

                    }
                }

            }

            catch
            {
                MessageBox.Show("ExcelAddIn installation failed");
                //MessageBox.Show("ExcelAddInのインストールに失敗しました。");//Japanese
            }
        }

        private static void UnInstallExcelAddIn()
        {
            try
            {
                //When OS  is 64 and Office is 32 Bit
                if (IsOS64Bit() && !IsOffice64Bit())
                {
                    RegistryKey myKey1 = Registry.LocalMachine.OpenSubKey(@"Software\Wow6432Node\Microsoft\Office\Excel\Addins\QC4ExcelAddIn", true);
                    if (myKey1 != null)
                    {
                        using (RegistryKey key = Registry.LocalMachine)
                        {
                            key.DeleteSubKeyTree(@"Software\Wow6432Node\Microsoft\Office\Excel\Addins\QC4ExcelAddIn", false);
                        }
                    }
                }

                else
                {
                    RegistryKey myKey1 = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Office\Excel\Addins\QC4ExcelAddIn", true);
                    if (myKey1 != null)
                    {
                        using (RegistryKey key = Registry.LocalMachine)
                        {
                            key.DeleteSubKeyTree(@"Software\Microsoft\Office\Excel\Addins\QC4ExcelAddIn", false);
                        }
                    }
                }
            }
            catch { }
        }
        public static string GetPathForInstallation()
        {
            try
            {
                string pathForInstallation;

                RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"Software\Classes\QuickCross File\DefaultIcon", true);
                if (myKey != null)
                {
                    pathForInstallation = myKey.GetValue("").ToString();
                    pathForInstallation = pathForInstallation.Replace(@"\QuickCross\file_icon_m.ico", "");
                    return pathForInstallation;
                }
                else
                    return null;
            }
            catch { return null; }
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
