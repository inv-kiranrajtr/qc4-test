using Qc4Launcher.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Qc4Launcher.Forms
{
    /// <summary>
    /// Interaction logic for ProgressBar.xaml
    /// </summary>
    public partial class ProgressBar : Window
    {
        public BackgroundWorker thrd;
        public ProgressBar()
        {
            InitializeComponent();
            this.Top = 0;
            this.Left = 0;
            this.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        public ProgressBar(string text)
        {
            InitializeComponent();
            this.Top = 0;
            this.Left = 0;
            this.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            UpdateProgressBar(0, text);
        }

        public ProgressBar(string text, BackgroundWorker thrd)
        {
            InitializeComponent();
            this.Top = 0;
            this.Left = 0;
            this.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            this.thrd = thrd;
            this.Command_Cancel.Visibility = Visibility.Visible;
            UpdateProgressBar(0, text);
        }

        public ProgressBar(bool IsIndeterminate, string status = "")
        {
            InitializeComponent();
            progressBar.IsIndeterminate = IsIndeterminate;
            if (IsIndeterminate)
            {
                status_text.Text = status;
                progress_text.Visibility = Visibility.Hidden;
            }
        }

        private static bool RetainThread = false;

        private void DragableGridMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        public void UpdateProgressBar(double percentage, string status, bool isForceStop = false, bool retainThread = false, bool disableCancel = false)
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
                //this.Close();
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
            timerKeyup = new System.Timers.Timer(500);
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

        public void CloseProgressBar()
        {
            if (!RetainThread)
                this.Close();
        }

        public void UpdateProgressBar(double percentage, string status, bool isHide)
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

        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            bool _altModifierPressed = (Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt));

            if (_altModifierPressed && Keyboard.IsKeyDown(Key.F4))
            {
                e.Handled = true;
            }
        }

        private void Command_Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(LocalResource.IM_MSG_CLOSE_FORM, "QuickCross", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                if (progressBar.Value < 100)
                {
                    Button cBtn = sender as Button;
                    cBtn.IsEnabled = false;
                    if (thrd != null)
                    {
                        thrd.CancelAsync();
                    }
                }
            }
        }

        public double getPbValue()
        {
            return progressBar.Value;
        }
    }
}
