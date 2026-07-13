

using log4net;
using Microsoft.Office.Interop.Excel;
using QC4Common.Common;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using System.Windows.Interop;

namespace QC4Common.Logic
{

    public class SingleGlobalInstance : IDisposable
    {
        public bool _hasHandle = false;
        Mutex _mutex;
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static bool localPr = false;

        private void InitMutex()
        {
            string appGuid = ((GuidAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(GuidAttribute), false).GetValue(0)).Value;
            string mutexId = string.Format("Local\\{{{0}}}", appGuid);
            _mutex = new Mutex(false, mutexId);
            _log.Debug("appGuid:" + mutexId);

            var allowEveryoneRule = new MutexAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), MutexRights.FullControl, AccessControlType.Allow);
            var securitySettings = new MutexSecurity();
            securitySettings.AddAccessRule(allowEveryoneRule);
            _mutex.SetAccessControl(securitySettings);
        }

        public SingleGlobalInstance(int timeOut, Workbook workBook, System.Windows.Window window = null)
        {
            InitMutex();
            try
            {
                if (timeOut < 0)
                    _hasHandle = _mutex.WaitOne(Timeout.Infinite, false);
                else
                    _hasHandle = _mutex.WaitOne(timeOut, false);
                _log.Debug("mutex success:" + _hasHandle + ", localPr:" + localPr + ", Process:" + Process.GetCurrentProcess().Id + ", thread:" + Thread.CurrentThread.ManagedThreadId);
                if (_hasHandle == false || localPr)
                {
                    if (window != null)
                    {
                        WindowInteropHelper wihMain = new WindowInteropHelper(window);
                        MessageDialog.ShowMessageOnParent(CommonResource.MULTI_INSTANCE_PROCESS_RUNNING_MSG, QC4Common.Common.Constants.MessageType.Info, wihMain.Handle);
                    }
                    else
                    {
                        MessageDialog.ShowMessageOnWorkBook(CommonResource.MULTI_INSTANCE_PROCESS_RUNNING_MSG, QC4Common.Common.Constants.MessageType.Info, workBook);
                    }
                    throw new TimeoutException("Timeout waiting for exclusive access on SingleInstance");
                }
                localPr = true;
            }
            catch (AbandonedMutexException)
            {
                _hasHandle = true;
                localPr = false;
            }
        }


        public void Dispose()
        {
            if (_mutex != null)
            {
                if (_hasHandle)
                {
                    _mutex.ReleaseMutex();
                    localPr = false;
                }
                _mutex.Close();
            }
        }
    }
}