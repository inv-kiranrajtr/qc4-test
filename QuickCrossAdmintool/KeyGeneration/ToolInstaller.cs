using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace KeyGeneration
{
    [RunInstaller(true)]
    public partial class ToolInstaller : System.Configuration.Install.Installer
    {
        public ToolInstaller()
        {
            InitializeComponent();
        }

        public override void Uninstall(IDictionary savedState)
        {
            Process application = null;
            foreach (var process in Process.GetProcesses())
            {
                if (!process.ProcessName.ToLower().Contains("creatinginstaller")) continue;
                application = process;
                break;
            }
            if (application != null && application.Responding)
            {
                application.Kill();
                base.Uninstall(savedState);
            }
        }
    }
}
