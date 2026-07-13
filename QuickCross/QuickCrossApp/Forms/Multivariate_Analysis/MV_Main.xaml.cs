using QC4Common.DB;
using QC4Common.Model;
using Qc4Launcher.Logic.MultiVariate;
using Qc4Launcher.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static Qc4Launcher.Forms.Multivariate_Analysis.CollectionClass;
using Excel = Microsoft.Office.Interop.Excel;

namespace Qc4Launcher.Forms.Multivariate_Analysis
{
    /// <summary>
    /// Interaction logic for MV_Main.xaml
    /// </summary>
    public partial class MV_Main : Window
    {
        public static bool isprocesssed = false;
        MainWindow mainWindow;
        private Excel.Workbook excelWorkbook;
        List<ProcessMethod> ProcessingMethodList;
        ObservableCollection<MutiVariable> proccesedList = new ObservableCollection<MutiVariable>();
        private Dictionary<String, QuestionSettings> dictionary = new Dictionary<string, QuestionSettings>(); //to get the variable list
        Excel.Application XlApp = null;
        public MV_Main(MainWindow main, Excel.Workbook workbook, string filePath, Excel.Application XlApp)
        {
            mainWindow = main;
            excelWorkbook = workbook;
            InitializeComponent();
            this.XlApp = XlApp;
            dictionary = Definiotion.VariableDictionary;
            ProcessingMethodList = new List<ProcessMethod>();
            ProcessingMethodList.Add(new ProcessMethod() { Name = ProcessingMethod.FACTOR_ANALYSIS, Type = LocalResource.MULTI_FACTOR_ANALYSIS, JType = LocalResource.MULT_FACTOR });
            ProcessingMethodList.Add(new ProcessMethod() { Name = ProcessingMethod.CLUSTER_ANALYSIS, Type = LocalResource.MULTI_CLUSTOR_ANALYSIS, JType = LocalResource.MULTI_CLUSTOR });
            ProcessingMethodList.Add(new ProcessMethod() { Name = ProcessingMethod.PSM_ANALYSIS, Type = LocalResource.MULTI_PSM_ANALYSIS, JType = LocalResource.MULTI_PSM });
            ProcessingMethodList.Add(new ProcessMethod() { Name = ProcessingMethod.CORRESPONDENCE_ANALYSIS, Type = LocalResource.MULTI_CORRESPONDENCE_ANALYSIS, JType = LocalResource.MULTI_CORRESPONDANCE });
            ProcessingMethodList.Add(new ProcessMethod() { Name = ProcessingMethod.CSPORTFOLIO_ANALYSIS, Type = LocalResource.MULTI_CSPORTFOLIO_ANALYSIS, JType = LocalResource.MULTI_CS_PORTFOLIO });
            Multi_Process_Grid.ItemsSource = ProcessingMethodList;
            data_grid.ItemsSource = proccesedList;
          
            MulivariateNewVariableCheck();

            if (QC4Common.Common.Constants.GlobalMode.Split(',')[0] != "ja-JP")
                Button_Help.Visibility = Visibility.Hidden;
            else
                Button_Help.Visibility = Visibility.Visible;
        }
        public void MulivariateNewVariableCheck()
        {
            
            if (Definiotion.VariableDictionary.Values.ToList().Any(q => q.QuestionFlag == QC4Common.Common.Constants.QuestionFlag.New))
            {
                if (Definiotion.VariableDictionary.Values.Count() > 0)
                {
                    List<QuestionSettings> questions = new List<QuestionSettings>();
                    questions = Definiotion.VariableDictionary.Values.ToList();
                    isprocesssed = false;
                    questions = questions.Where(x => x.QuestionFlag != (QC4Common.Common.Constants.QuestionFlag.An)).ToList();
                    DataTable dt = new DataTable();
                    List<string> DataAftrVar = new List<string>();

                    int rowCount = frmutil.IsDataProcessedMulti(excelWorkbook);
                    if (rowCount > 0)
                    {
                       rowCount= rowCount - 1;
                    }
                    int qscount = questions.Count();
                    if (rowCount == qscount)
                    {
                        MainWindow.isprocesssed = true;
                    }
                    else { MainWindow.isprocesssed = false; }
                }
            }
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
        private void b1SetColor(object sender, MouseButtonEventArgs e)
        {
            if (ExpGrid != null)
                ExpGrid.Focus();
        }

        private void Window_Closing(object sender, EventArgs e)
        {
            mainWindow.Show();
        }
        System.Windows.Controls.DataGrid ExpGrid = null;
        private void Multi_Process_Grid_CurrentCellChanged(object sender, EventArgs e)
        {

            var dg = (System.Windows.Controls.DataGrid)sender;
            ExpGrid = dg;
            dg.Focus();
        }
        private void List_GT_Summary_CurrentCellChanged(object sender, EventArgs e)
        {
            var dg = (System.Windows.Controls.DataGrid)sender;
            ExpGrid = dg;
            dg.Focus();
        }


        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var originalSource = (DependencyObject)e.OriginalSource;
            while ((originalSource != null) && !(originalSource is System.Windows.Controls.DataGridRow)) originalSource = VisualTreeHelper.GetParent(originalSource);
            if (originalSource == null) return;
            while ((originalSource != null) && (originalSource is System.Windows.Shapes.Rectangle))
            {
                return;
            }
            if (originalSource == null)
            {
                return;
            }
            ProcessMethod dataProcess = new ProcessMethod();
            dataProcess = (Multi_Process_Grid.SelectedItem) as ProcessMethod;
            if (dataProcess != null)
            {

                switch (dataProcess.Name)
                {
                    case ProcessingMethod.CORRESPONDENCE_ANALYSIS:
                        CorrespondenceAnalysis correspondence = new CorrespondenceAnalysis(excelWorkbook, mainWindow);
                        if (NewVariablecheck())
                        {
                            return;
                        }
                        this.Hide();
                        try
                        {
                            correspondence.ShowDialog();
                        }
                        catch (Exception ex)
                        {
                            string exs = ex.Message;
                        }
                        finally
                        {
                            if (mainWindow.Visibility == Visibility.Hidden)
                            {
                                this.Show();
                            }
                        }
                        break;
                    case ProcessingMethod.CLUSTER_ANALYSIS:
                        Cluster_Analysis cluster_Analysis = new Cluster_Analysis(excelWorkbook, mainWindow);
                        if (NewVariablecheck())
                        {
                            return;
                        }
                        this.Hide();
                        try
                        {
                            cluster_Analysis.ShowDialog();
                        }
                        catch (Exception ex)
                        {
                            string exs = ex.Message;
                        }
                        finally
                        {
                            if (mainWindow.Visibility == Visibility.Hidden)
                            {
                                this.Show();
                            }
                        }
                        break;
                    case ProcessingMethod.FACTOR_ANALYSIS:
                        Factor_Analysis factor_Analysis = new Factor_Analysis(excelWorkbook, mainWindow);
                        if (NewVariablecheck())
                        {
                            return;
                        }
                        this.Hide();
                        try
                        {

                            factor_Analysis.ShowDialog();
                        }
                        catch (Exception ex)
                        {
                            string exs = ex.Message;
                        }
                        finally
                        {
                            if (mainWindow.Visibility == Visibility.Hidden)
                            {
                                this.Show();
                            }
                        }
                        break;
                    case ProcessingMethod.PSM_ANALYSIS:
                        PSM_ANALYSIS pSM_ANALYSIS = new PSM_ANALYSIS(excelWorkbook, mainWindow, XlApp);
                        if (NewVariablecheck())
                        {
                            return;
                        }
                        this.Hide();
                        try
                        {

                            pSM_ANALYSIS.ShowDialog();
                        }
                        catch (Exception ex)
                        {
                            string exs = ex.Message;
                        }
                        finally
                        {
                            if (mainWindow.Visibility == Visibility.Hidden)
                            {
                                this.Show();
                            }
                        }
                        break;
                    case ProcessingMethod.CSPORTFOLIO_ANALYSIS:
                        CSPortfolioAnalysis cs = new CSPortfolioAnalysis(excelWorkbook, mainWindow);
                        if (NewVariablecheck())
                        {
                            return;
                        }
                        this.Hide();
                        try
                        {

                            cs.ShowDialog();
                        }
                        catch (Exception ex)
                        {
                            string exs = ex.Message;
                        }
                        finally
                        {
                            if (mainWindow.Visibility == Visibility.Hidden)
                            {
                                this.Show();
                            }
                        }
                        break;
                    default:
                        MessageBox.Show("Not Implemented");
                        break;
                }
            }
        }



        private void Data_grid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
        }

        private void Btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        QC4Common.Util.FormUtil frmutil = new QC4Common.Util.FormUtil();
        public bool NewVariablecheck()
        {
            if (dictionary.Values.ToList().Any(q => q.QuestionFlag == QC4Common.Common.Constants.QuestionFlag.New))
            {

                if (!MainWindow.isprocesssed)
                {
                    string errormsg = LocalResource.MULTI_MAIN_MSGBX_NOTEXECUTED_SOMEDATA;
                    MessageBoxResult result = MessageBox.Show(errormsg, "QuickCross", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.No)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }
        private void Btn_Setting_process_Click(object sender, RoutedEventArgs e)
        {
            ProcessMethod dataProcess = new ProcessMethod();
            dataProcess = (Multi_Process_Grid.SelectedItem) as ProcessMethod;
            if (dataProcess != null)
            {
                switch (dataProcess.Name)
                {
                    case ProcessingMethod.CORRESPONDENCE_ANALYSIS:
                        CorrespondenceAnalysis correspondence = new CorrespondenceAnalysis(excelWorkbook, mainWindow);
                        if (NewVariablecheck())
                        {
                            return;
                        }
                        this.Hide();
                        try
                        {

                            correspondence.ShowDialog();
                        }
                        catch (Exception ex)
                        {
                            string exs = ex.Message;
                        }
                        finally
                        {
                            if (mainWindow.Visibility == Visibility.Hidden)
                            {
                                this.Show();
                            }
                        }
                        break;
                    case ProcessingMethod.CLUSTER_ANALYSIS:
                        Cluster_Analysis cluster_Analysis = new Cluster_Analysis(excelWorkbook, mainWindow);
                        if (NewVariablecheck())
                        {
                            return;
                        }
                        this.Hide();
                        try
                        {
                            cluster_Analysis.ShowDialog();
                        }
                        catch (Exception ex)
                        {
                            string exs = ex.Message;
                        }
                        finally
                        {
                            if (mainWindow.Visibility == Visibility.Hidden)
                            {
                                this.Show();
                            }
                        }
                        break;
                    case ProcessingMethod.FACTOR_ANALYSIS:
                        Factor_Analysis factor_Analysis = new Factor_Analysis(excelWorkbook, mainWindow);
                        if (NewVariablecheck())
                        {
                            return;
                        }
                        this.Hide();
                        try
                        {

                            factor_Analysis.ShowDialog();
                        }
                        catch (Exception ex)
                        {
                            string exs = ex.Message;
                        }
                        finally
                        {
                            if (mainWindow.Visibility == Visibility.Hidden)
                            {
                                this.Show();
                            }
                        }
                        break;
                    case ProcessingMethod.PSM_ANALYSIS:
                        PSM_ANALYSIS pSM_ANALYSIS = new PSM_ANALYSIS(excelWorkbook, mainWindow, XlApp);
                        if (NewVariablecheck())
                        {
                            return;
                        }
                        this.Hide();
                        try
                        {

                            pSM_ANALYSIS.ShowDialog();
                        }
                        catch (Exception ex)
                        {
                            string exs = ex.Message;
                        }
                        finally
                        {
                            if (mainWindow.Visibility == Visibility.Hidden)
                            {
                                this.Show();
                            }
                        }
                        break;
                    case ProcessingMethod.CSPORTFOLIO_ANALYSIS:
                        CSPortfolioAnalysis cs = new CSPortfolioAnalysis(excelWorkbook, mainWindow);
                        if (NewVariablecheck())
                        {
                            return;
                        }
                        this.Hide();
                        try
                        {

                            cs.ShowDialog();
                        }
                        catch (Exception ex)
                        {
                            string exs = ex.Message;
                        }
                        finally
                        {
                            if (mainWindow.Visibility == Visibility.Hidden)
                            {
                                this.Show();
                            }
                        }
                        break;
                    default:
                        MessageBox.Show("Not Implemented");
                        break;
                }
            }
        }

        private void Button_Help_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(QC4Common.Classes.Help.GetHelpLink(QC4Common.Common.Constants.HelpButtonType.MULTIVARIATE));
        }
        public static DataTable GetMultivariateData(System.Data.SQLite.SQLiteConnection con, string tablename, String queryvariablename)
        {
            DataTable dt = new DataTable();
            dt = DBHelper.GetDataTable("Select " + queryvariablename + " from " + tablename + " order by sort_no ", con);
            return dt;
        }
        public bool IsExistsAnVariable()
        {
            List<QuestionSettings> questions = Definiotion.VariableDictionary.Values.ToList();
            List<QuestionSettings> MultiVariablesN = questions.Where(i => i.QuestionFlag == "An" && i.AnswerType == "N").ToList();
            List<QuestionSettings> MultiVariablesSA = questions.Where(i => i.QuestionFlag == "An" && i.AnswerType == "SA").ToList();
            allAnVariables = questions.Where(i => i.QuestionFlag == "An").ToList();

            List<QuestionSettings> results = allAnVariables.Where(m => !allMultiVariateVariables.Contains(m.Variable)).ToList();
            if (results.Count > 0)
            {
                string errmsg = string.Format(LocalResource.MULTI_VARIABLE_NOTEXIST, results[0].Variable);
                MessageBoxResult result1 = MessageBox.Show(errmsg, "QuickCross", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        List<string> allMultiVariateVariables = new List<string>();
        List<QuestionSettings> allAnVariables = new List<QuestionSettings>();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {




                var SettingSheet = Util.ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Constants.SheetCodeName.MultiVariateSheet);

                Excel.Range dpstart = SettingSheet.Cells[5, 2];
                Excel.Range lastcell = ExcelUtil.EndxlUp(SettingSheet.Cells[5, 2]);
                Excel.Range rar = SettingSheet.Range[dpstart, lastcell];
                var obj = rar.Value;
                int d = 0;
                foreach (Excel.Range cell in rar.Cells)
                {

                    if (cell.Row >= 5 && !string.IsNullOrEmpty(cell.Text))
                    {
                        MutiVariable data = new MutiVariable();
                        Excel.Range start = SettingSheet.Cells[cell.Row, cell.Column];
                        Excel.Range end = ExcelUtil.EndxlRight(start);
                        Excel.Range range = SettingSheet.get_Range(start, end);
                        object[,] res = range.Value;
                        if (res[1, 5] != null)
                        {
                            if (res[1, 5].ToString() == "Cluster")
                            {

                                string fvariable = string.Empty;
                                List<string> join = new List<string>();
                                for (int i = 136; i < res.GetLength(1); i++)
                                {

                                    if (res.GetLength(1) >= 136)
                                    {
                                        if (res[1, i] != null)
                                        {
                                            allMultiVariateVariables.Add(res[1, i].ToString());
                                            join.Add(res[1, i].ToString());
                                        }
                                    }

                                }

                                if (join != null)
                                {
                                    fvariable = string.Join(",", join);
                                }
                                if (join.Count > 0)
                                {
                                    data.ProcessName = LocalResource.MULTI_CLUSTER;
                                    data.VariableName = fvariable;
                                    data.NumberofVariable = join.Count;
                                    proccesedList.Add(data);
                                }
                            }
                            else
                            {


                                string fvariable = string.Empty;
                                List<string> join = new List<string>();
                                for (int i = 136; i < res.GetLength(1); i++)
                                {

                                    if (res.GetLength(1) >= 136)
                                    {
                                        if (res[1, i] != null)
                                        {
                                            allMultiVariateVariables.Add(res[1, i].ToString());
                                            join.Add(res[1, i].ToString());
                                        }
                                    }

                                }

                                if (join != null)
                                {
                                    fvariable = string.Join(",", join);
                                }
                                if (join.Count > 0)
                                {
                                    data.ProcessName = LocalResource.MULTI_FACTOR;
                                    data.VariableName = fvariable;
                                    data.NumberofVariable = join.Count;
                                    proccesedList.Add(data);
                                }
                            }

                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }
            if (DBHelper.checkMultiTableExists(excelWorkbook))
            {
                Btn_Reset.IsEnabled = true;
            }
        }
        private ProgressBar progress = null;
        private void OnWorkerMethodCompletePSM(double value, string status, bool isForceStop = false, bool retainThread = false, bool close = false, bool disableCancel = false)
        {
            progress.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
            new Action(
            delegate ()
            {
                if (close)
                {
                    if (retainThread)
                        progress.UpdateProgressBar(progress.getPbValue(), status, isForceStop, retainThread, disableCancel);
                    else
                        progress.Close();
                }
                else
                {
                    progress.UpdateProgressBar(value, status, isForceStop, retainThread, disableCancel);
                }
            }
            ));
        }
        BackgroundWorker backgroundWorker1 = new BackgroundWorker();
        private void Btn_Reset_Click(object sender, RoutedEventArgs e)
        {


            if (MessageBoxResult.Yes == System.Windows.MessageBox.Show(LocalResource.MV_MAINWINDOW_RESET, LocalResource.TITLE_QC, MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                try
                {
                    progress = new ProgressBar(LocalResource.MULTI_MAIN_RESET);

                    backgroundWorker1.WorkerReportsProgress = true;
                    backgroundWorker1.WorkerSupportsCancellation = true;
                    backgroundWorker1.DoWork += new DoWorkEventHandler((object senderr, DoWorkEventArgs ee) =>
                    {


                        Excel.Worksheet ws = ExcelUtil.GetWorkSheetBySheetName(excelWorkbook, Constants.SheetType.sh_Data_AN);
                        if (ws != null)
                        {
                            QC4Common.Util.ExcelUtil.DeleteMultivariateSheets(excelWorkbook);
                        }



                        var multivariatesheet = Util.ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Constants.SheetCodeName.MultiVariateSheet);
                        Excel.Range mstart = multivariatesheet.Cells[5, 2];
                        Excel.Range lastcell = ExcelUtil.EndxlUp(multivariatesheet.Cells[5, 2]);
                        Excel.Range mlast = multivariatesheet.Cells[lastcell.Row, QC4Common.Common.Constants.STD_DP.MAX_DP_COLUMN];
                        Excel.Range rar = multivariatesheet.Range[mstart, mlast];
                        rar.Clear();

                        Dispatcher.Invoke(() => proccesedList.Clear());
                        OnWorkerMethodCompletePSM(10, LocalResource.DELETING);
                        //delete variables from QS
                        Util.QS.QuestionDelete questionDelete = new Util.QS.QuestionDelete();

                        var array = Util.Definiotion.VariableDictionary.Where(a => (a.Value.QuestionFlag == QC4Common.Common.Constants.QuestionFlag.An)).Select(q => q.Value).ToList();
                        double percent = 1;
                        double per = 0;
                        foreach (QC4Common.Model.QuestionSettings var in array)
                        {

                            per = Math.Round(((percent / array.Count()) * 100) - 1);
                            if (per < 10)
                            {
                                per += 10;
                            }
                            questionDelete.StartQuestionDelete(excelWorkbook, var.Variable);
                            OnWorkerMethodCompletePSM(per, LocalResource.DELETING + ": " + percent + "/" + array.Count());
                            percent++;
                        }
                        DB.DBHelper.DeleteMultivariateTable(excelWorkbook);
                        OnWorkerMethodCompletePSM(100, LocalResource.DELETING);
                        Thread.Sleep(500);
                        OnWorkerMethodCompletePSM(100, LocalResource.COMPLETED, true);
                    });
                    backgroundWorker1.RunWorkerAsync();
                    progress.Owner = this;
                    progress.ShowDialog();
                    if (!DBHelper.checkMultiTableExists(excelWorkbook))
                    {
                        Btn_Reset.IsEnabled = false;
                    }
                }
                catch { }
            }
        }

        private void Btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<QuestionSettings> allquest = dictionary.Values.ToList();
                MutiVariable selected = (MutiVariable)data_grid.SelectedItem;
                string VariableName = string.Empty;
                if (QuestionSetting.QuestionSettingEdit.INdexInt.Count > 0)
                {
                    QuestionSetting.QuestionSettingEdit.INdexInt.Clear();
                }
                if (selected.ProcessName == LocalResource.MULTI_FACTOR)
                {
                    List<int> indexList = new List<int>();
                    string[] var = selected.VariableName.Split(',');
                    int d = 1;
                    foreach (var item in var)
                    {
                        int index = allquest.FindIndex(a => a.Variable == item);
                        if (index >= 0)
                        {
                            indexList.Add(index);
                        }
                       
                        d++;
                    }

                    if (indexList.Count > 0)
                    {
                        QuestionSetting.QuestionSettingEdit.INdexInt = indexList;

                        VariableName = allquest[indexList[0]].Variable;
                        QuestionSetting.QuestionSettingEdit.isFactor = true;
                    }
                }
                else
                {
                    VariableName = selected.VariableName;
                    QuestionSetting.QuestionSettingEdit.isFactor = false;
                }

                QuestionSettings question = allquest.FirstOrDefault(x => x.Variable == VariableName);

                // addqus = true;
                if (question != null)
                {
                    int index = allquest.FindIndex(a => a.Variable == VariableName);
                    this.Hide();
                    Qc4Launcher.Forms.QuestionSetting.QuestionSettingEdit edit = new Qc4Launcher.Forms.QuestionSetting.QuestionSettingEdit(index, excelWorkbook, null, true, this);
                    edit.ShowDialog();
                    edit.Owner = this;
                }
                else
                {
                    
                    return;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
            }
        }

        private void Btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Util.QS.QuestionDelete questionDelete = new Util.QS.QuestionDelete();
                string errormsg = LocalResource.ALERT_DELETE_DATA_PROCESSING;
                string errormsg1 = LocalResource.MULTI_ITEM_DELETE_MSG;
                bool anVar = true;
                MessageBoxResult result = MessageBox.Show(errormsg, "QuickCross", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    int index = data_grid.SelectedIndex;
                    MutiVariable variable = (MutiVariable)data_grid.SelectedItem;
                    {
                        if (MessageBoxResult.Yes == System.Windows.MessageBox.Show(String.Format(errormsg1), LocalResource.TITLE_QC, MessageBoxButton.YesNo, MessageBoxImage.Question))
                        {
                            progress = new ProgressBar(LocalResource.DELETING);

                            backgroundWorker1.WorkerReportsProgress = true;
                            backgroundWorker1.WorkerSupportsCancellation = true;
                            backgroundWorker1.DoWork += new DoWorkEventHandler((object senderr, DoWorkEventArgs ee) =>
                            {
                                DB.QuestionSettingDao questionSettingDao = new DB.QuestionSettingDao(QC4Common.DB.DBHelper.GetConnectionString(excelWorkbook));
                                List<QuestionSettings> allquest = dictionary.Values.ToList();

                                OnWorkerMethodCompletePSM(10, LocalResource.DELETING);

                                if (variable.ProcessName == LocalResource.MULTI_FACTOR)
                                {
                                    string[] var = variable.VariableName.Split(',');
                                    if (var.Count() > 0)
                                    {
                                        foreach (var item in var)
                                        {
                                            if (dictionary.Keys.Contains(item))
                                            {
                                                questionDelete.StartQuestionDelete(excelWorkbook, item);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (dictionary.Keys.Contains(variable.VariableName))
                                    {
                                        questionDelete.StartQuestionDelete(excelWorkbook, variable.VariableName);
                                    }
                                }
                                OnWorkerMethodCompletePSM(70, LocalResource.DELETING);
                                var multivariatesheet = Util.ExcelUtil.GetWorkSheetByCodeName(excelWorkbook, Constants.SheetCodeName.MultiVariateSheet);
                                if (multivariatesheet.Cells[5 + index] != null)
                                {
                                    OnWorkerMethodCompletePSM(100, LocalResource.DELETING, retainThread: true);
                                    ((Excel.Range)multivariatesheet.Rows[5 + index]).Delete(Excel.XlDeleteShiftDirection.xlShiftToLeft);
                                    Dispatcher.Invoke(() => proccesedList.Remove(variable));
                                }
                                Thread.Sleep(500);
                                OnWorkerMethodCompletePSM(100, LocalResource.DELETING, true);
                            });
                            backgroundWorker1.RunWorkerAsync();
                            progress.Owner = this;
                            progress.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                if (Btn_Close.IsFocused)
                {
                    Style style = Application.Current.FindResource("MyFocusVisual") as Style;
                    Multi_Process_Grid.FocusVisualStyle = null;
                    Multi_Process_Grid.Focus();
                    if (Multi_Process_Grid.SelectedIndex < 0 && Multi_Process_Grid.Items.Count > 1)
                    {
                        Multi_Process_Grid.SelectedIndex = 0;
                    }
                    e.Handled = true;

                }
                else if (Btn_Setting_process.IsFocused)
                {
                    if (data_grid.Items.Count > 0)
                    {
                        Style style = Application.Current.FindResource("MyFocusVisual") as Style;
                        data_grid.FocusVisualStyle = null;
                        data_grid.Focus();
                        if (data_grid.SelectedIndex < 0 && data_grid.Items.Count > 0)
                        {
                            data_grid.SelectedIndex = 0;
                        }
                        e.Handled = true;
                    }

                }

            }



        }
        private void List_GT_Summary_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            System.Windows.Controls.DataGrid grid = null;
            System.Windows.Controls.Button btn = null;
            System.Windows.Controls.CheckBox chk = null;

            if (sender is DataGrid)
                grid = sender as System.Windows.Controls.DataGrid;
            else if (sender is Button)
                btn = sender as System.Windows.Controls.Button;
            else if (sender is CheckBox)
                chk = sender as System.Windows.Controls.CheckBox;

            if (grid != null && e.Key == Key.Tab && grid.Name is "Multi_Process_Grid")
            {
                e.Handled = true;
                Btn_Setting_process.Focus();
            }
            else if (grid != null && e.Key == Key.Tab && grid.Name is "data_grid")
            {
                e.Handled = true;
                Btn_Delete.Focus();
            }
            QC4Common.Util.GridCommonFunc.Arrow(sender, e);
        }
    }
}
