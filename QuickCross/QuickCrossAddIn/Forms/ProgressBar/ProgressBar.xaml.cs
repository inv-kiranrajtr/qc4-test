using ExcelAddIn;
using Macromill.QCWeb.COMOperate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Timer = System.Timers.Timer;

namespace Qc4Launcher.Forms
{
    /// <summary>
    /// Interaction logic for ProgressBar.xaml
    /// </summary>
    public partial class ProgressBar : Window
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        // private double checkvalue = 0;// //IL_JP_MAM_007:4295055420 


        Microsoft.Office.Interop.Excel.Worksheet WorkSheet = null;
        Microsoft.Office.Interop.Excel.Application Excelapplication = null;
        private static bool RetainThread = false;
        public IntPtr hWnd;
        public bool changeWIndowPos = false;
        public static bool isStop = false;
        public ProgressBar(Microsoft.Office.Interop.Excel.Worksheet sheet)
        {
            InitializeComponent();
            WorkSheet = sheet;
            //WorkSheet.Application.Cursor = Microsoft.Office.Interop.Excel.XlMousePointer.xlWait;
            //this.Cursor = System.Windows.Input.Cursors.Wait;
            //this.Topmost = true;

            SetProgressBar();
            //this.Top = 0;
            //this.Left = 0;
            //this.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            //this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }
        public BackgroundWorker thrd;
        public ProgressBar(Microsoft.Office.Interop.Excel.Worksheet sheet, BackgroundWorker thrd)
        {
            InitializeComponent();
            this.Top = 0;
            this.Left = 0;
            this.thrd = thrd;
            WorkSheet = sheet;
            this.Command_Cancel.Visibility = Visibility.Visible;
            SetProgressBar();
        }
        AutoResetEvent autoReset;
        public ProgressBar(Microsoft.Office.Interop.Excel.Worksheet sheet, BackgroundWorker thrd, AutoResetEvent autoReset)
        {
            InitializeComponent();
            this.Top = 0;
            this.Left = 0;
            this.thrd = thrd;
            this.autoReset = autoReset;
            WorkSheet = sheet;
            this.Command_Cancel.Visibility = Visibility.Visible;
            SetProgressBar();
        }
        public ProgressBar(string a, Microsoft.Office.Interop.Excel.Worksheet sheet)
        {
            InitializeComponent();
            WorkSheet = sheet;
            SetProgressBar(a);
            //this.Top = 0;
            //this.Left = 0;
            //this.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            //this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }
        public ProgressBar(Microsoft.Office.Interop.Excel.Application excelApp)
        {
            InitializeComponent();
            Excelapplication = excelApp;
            SetProgressBar(0);
        }

        private void DragableGridMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void SetProgressBar()
        {

            WindowInteropHelper wih = new WindowInteropHelper(this);
            wih.Owner = new IntPtr(WorkSheet.Application.Hwnd);
            SetParent(wih.Handle, (IntPtr)WorkSheet.Application.Hwnd);
            WorkSheet.Application.Interactive = false;

            WorkSheet.Application.Cursor = Microsoft.Office.Interop.Excel.XlMousePointer.xlDefault;

        }
        private void SetProgressBar(int i)
        {

            WindowInteropHelper wih = new WindowInteropHelper(this);
            wih.Owner = new IntPtr(Excelapplication.Hwnd);
            SetParent(wih.Handle, (IntPtr)Excelapplication.Hwnd);
            Excelapplication.Interactive = false;
            Excelapplication.Cursor = Microsoft.Office.Interop.Excel.XlMousePointer.xlDefault;
            InitializeComponent();

        }
        private void SetProgressBar(string s = null)
        {

            WindowInteropHelper wih = new WindowInteropHelper(this);
            wih.Owner = new IntPtr(Convert.ToInt32(s));
            SetParent(wih.Handle, (IntPtr)Convert.ToInt32(s));

            InitializeComponent();

        }
        private void ReSetProgressBar()
        {
            if (Excelapplication != null)
            {
                Excelapplication.Interactive = true;
                SetForegroundWindow((IntPtr)Excelapplication.Hwnd);
                Excelapplication.Cursor = Microsoft.Office.Interop.Excel.XlMousePointer.xlDefault;
            }
            else
            {
                WorkSheet.Application.Interactive = true;
                SetForegroundWindow((IntPtr)WorkSheet.Application.Hwnd);
                WorkSheet.Application.Cursor = Microsoft.Office.Interop.Excel.XlMousePointer.xlDefault;
            }


            //checkvalue = 0;// //IL_JP_MAM_007:4295055420 
        }

        public void OnWorkerMethodComplete(double value, string status, bool IsError = false, bool retainThread = false, bool IsForceStop = false)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
            new System.Action(
            delegate ()
            {
                this.UpdateProgressBar(value, status, IsError, retainThread, IsForceStop);
            }
            ));
        }

        public void CloseProgressBar()
        {
            try
            {
                if (!RetainThread)
                {
                    this.Close();
                    // ReSetProgressBar();
                }
            }
            finally
            {

                if (WorkSheet != null) COMWholeOperate.releaseComObject(WorkSheet);
                if (Excelapplication != null) COMWholeOperate.releaseComObject(Excelapplication);
            }
        }

        private void UpdateProgressBar(double percentage, string status, bool IsError, bool retainThread, bool IsForceStop)
        {
            RetainThread = retainThread;
            if (IsForceStop) RetainThread = false;

            progressBar.Value = percentage;
            progress_text.Text = Convert.ToInt32(percentage) + "%";
            if (status != "")
            {
                status_text.Text = status;
            }
            if (percentage >= 100)
            {

                StopTimer();
                if (IsForceStop)
                {
                    OnTimedEvent(null, null);
                }
                else
                {
                    StartTimer();
                }
             //   this.Close();
                ReSetProgressBar();
            }
            if (IsError)
            {
                this.Dispatcher.Invoke((Action)(() =>
                {
                    MessageBox.Show(AddinResource.PROGRESSBAR_ERROR, "QuickCross");
                }));
                //WorkSheet.Application.Cursor = Microsoft.Office.Interop.Excel.XlMousePointer.xlDefault;
                //this.Cursor = System.Windows.Input.Cursors.Arrow;
                if (!RetainThread)
                {
                    this.Close();
                    ReSetProgressBar();
                }
            }
        }

        private static System.Timers.Timer timerKeyup = null;

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            StopTimer();
            Dispatcher.Invoke(() => CloseProgressBar());
        }

        private void StartTimer()
        {
            timerKeyup = new Timer(1000);
            timerKeyup.Enabled = true;
            timerKeyup.Start();
            timerKeyup.Elapsed += OnTimedEvent;
        }

        private void StopTimer()
        {
            if (timerKeyup != null)
            {
                timerKeyup.Stop();
                timerKeyup.Dispose();
                timerKeyup.Enabled = false;
                timerKeyup = null;
            }
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            bool _altModifierPressed = (Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt));

            if (_altModifierPressed && Keyboard.IsKeyDown(Key.F4))
            {
                e.Handled = true;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            IntPtr windowHandle = new WindowInteropHelper(this).Handle;
            hWnd = windowHandle;
            int windowHeight = Convert.ToInt32(this.Height);

            // this.WindowState = WindowState.Maximized;
        }
        public static bool retainData = false;
        private void Command_Cancel_Click(object sender, RoutedEventArgs e)
        {
          //  isStop = true;

            if (MessageBox.Show(AddinResource.IM_MSG_CLOSE_FORM, "QuickCross", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                if (progressBar.Value < 100)
                {
                    Button cBtn = sender as Button;
                    cBtn.IsEnabled = false;
                    if (thrd != null)
                    {
                        thrd.CancelAsync();
                    }
                    StopTimer();
                   // OnWorkerMethodComplete((100), AddinResource.DP_PROGRESS_MSG_95, retainThread: true,IsForceStop:true);
                   // isStop = false;
                }
            }
            else
            {
              //  isStop = false;

            }
        }










        public ProgressBar(string text, BackgroundWorker thrd)
        {
            InitializeComponent();
            this.Top = 0;
            this.Left = 0;
            this.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            this.thrd = thrd;
           // this.Command_Cancel.Visibility = Visibility.Visible;
            UpdateProgressBar1(0, text);
        }
        public void UpdateProgressBar1(double percentage, string status, bool isHide)
        {
            if (percentage > 100)
            {
                percentage = 100;
            }
            progressBar.Value = percentage;
            progress_text.Text = Convert.ToInt32(percentage) + "%";
            if (isHide)
            {
                this.Hide();
            }
        }
        public void UpdateProgressBar1(double percentage, string status, bool isForceStop = false, bool retainThread = false, bool disableCancel = false)
        {
            RetainThread = retainThread;

            if (percentage > 100)
            {
                percentage = 100;
            }
            progressBar.Value = percentage;
            progress_text.Text = Convert.ToInt32(percentage) + "%";
            if (status != "")
            {
                status_text.Text = status;
            }
            if (disableCancel)
            {
                this.Command_Cancel.IsEnabled = false;
            }
            if (percentage >= 100)
            {
                this.Command_Cancel.IsEnabled = false;
                if (isForceStop)
                {
                    StopTimer();
                    if (!RetainThread)
                        this.Close();
                }
                else
                {
                    StopTimer();
                    StartTimer();
                }
                this.Close();
            }
        }

        public double getPbValue()
        {
            return progressBar.Value;
        }

    }
}
