using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.IO;
using System.Data;
using Qc4Launcher.Util;
using ProgressBar = Qc4Launcher.Forms.ProgressBar;
using System.Threading;
using Excel = Microsoft.Office.Interop.Excel;
using Constants = Qc4Launcher.Util.Constants;
using ExcelAddIn.Sheets;
using System.Data.SQLite;
using Qc4Launcher.DB;
using static Qc4Launcher.Util.Constants;
using static Qc4Launcher.Util.Enums;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;
using Qc4Launcher.Model;
using ProgressBarForm = Qc4Launcher.Forms.ProgressBar;
using System.Runtime.InteropServices;
using Path = System.IO.Path;
using Qc4Launcher.Forms;
using System.Diagnostics;
using Macromill.QCWeb.Question;
using log4net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Timers;
using Macromill.QCWeb.COMOperate;
using Qc4Launcher.Logic;
using Vb = Microsoft.VisualBasic;
using Qc4Launcher.Logic.DataImport;

namespace Qc4Launcher
{
    /// <summary>
    /// Interaction logic for DataImport.xaml
    /// </summary>
    public partial class DataImport : Window
    {
        //Progress Bar
        #region StaticVariables
        private static Excel.Workbook DataImportFile1WorkBook = null;
        bool IsDialogueDispalayed = false;
        bool IsDuplicated = false;
        bool IsPro = false;
        public bool IsNotValidateFromThread { get; set; }

        private string SelectedFile1 = string.Empty;
        private string SelectedFile2 = string.Empty;
        private static string file1TempFolderPath;
        private ProgressBar progress = null;
        private static Excel.Application excelApp;
        private static DataTable dtFile2 = new DataTable();
        private static bool enableVennDiagramCalculation = true;
        private static DataImportScreenMode screenMode = DataImportScreenMode.FileSelection;
        private static ColumnImportSettings importSettings = new ColumnImportSettings();
        private static DataTable dtFile2JoinedDataBeforeProcessing = new DataTable();
        private static DataTable dtFile2JoinedDataAfterProcessing = new DataTable();
        private static bool IsNextButtonEnabled = false;
        private static bool IsFile1Duplicated = false;
        private static List<KeyValueObject> file1Keys;
        private static List<KeyValueObject> file1Variables;
        bool firstTimeLoad = true;
        bool IsSave = false;
        List<string> TempColumns = new List<string>();

        private static bool allowMessageBox = false;
        private static bool messageBoxAllocated = false;
        private static bool IsDataProcessed = false;
        private static bool ReInsertToTempData = true;
        private static bool IsForceKeyChange = false;
        private static bool IsPasteOperation = false;
        private static bool IsMainWindowFile = false;
        private static DataImportHelper dataImportHelper;
        private static DataMerge parentWindow;
        private static MainWindow mAinWindow = null;
        bool IsHeaderClicked = false;

        DataGridHelper dataGridHelper = new DataGridHelper();

        // From Main Window

        private static string mergeSelectedFile = null;
        private static Excel.Workbook mergeSelectedWb = null;
        private static string mergeFolderPath = null;

        private static string tempDestPath = null;

        private string DBPath
        {
            get
            {
                return file1TempFolderPath + "\\" + TemplateFile.DB_FIlE;
            }
        }

        private string WorkBookPath
        {
            get
            {
                return file1TempFolderPath + "\\" + TemplateFile.QC4_Template;
            }
        }

        bool isFile1Key1Exist => ((string)ddlFile1Key1.SelectedValue) != ComboBoxSettings.NoneText;
        bool isFile1Key2Exist => ((string)ddlFile1Key2.SelectedValue) != ComboBoxSettings.NoneText;
        bool isFile2Key1Exist => ((string)ddlFile2Key1.SelectedValue) != ComboBoxSettings.NoneText;
        bool isFile2Key2Exist => ((string)ddlFile2Key2.SelectedValue) != ComboBoxSettings.NoneText;


        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static ExcelOperate excelOperate;
        #endregion

        #region InitSettings
        public DataImport(MainWindow mainWindow, DataMerge dataMerge, string selectedFile, Excel.Workbook workBook, string file1TempFolder)
        {
            //Main Window
            mAinWindow = mainWindow;
            mergeSelectedFile = selectedFile;
            mergeSelectedWb = workBook;
            mergeFolderPath = file1TempFolder;

            parentWindow = dataMerge;
            InitializeComponent();
            parentWindow.Hide();
            dataImportHelper = new DataImportHelper();
            dataImportHelper.OnWorkerComplete += new DataImportHelper.OnWorkerMethodCompleteDelegate(OnWorkerMethodComplete);

            SetInitSettings(selectedFile, workBook, file1TempFolder);
            firstTimeLoad = false;
            IsPro = CommonFunction.ActivationKeyChecking();
        }

        private void SetInitSettings(string selectedFile, Excel.Workbook workBook, string file1TempFolder)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ja-US");
            screenMode = DataImportScreenMode.FileSelection;
            LoadDefaultFile1(selectedFile, workBook, file1TempFolder);
        }

        #endregion

        #region Events

        #region FileBrowsing
        private void ShowCommonExceptionMessage()
        {
            MessageDialog.ErrorOk(LocalResource.IM_MSG_LOAD_FAILED);
        }

        private void Btn_Open_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Multiselect = false;
                _log.Info("Opening file 1 selection dialog");
                ofd.Filter = FileSettings.MainWindowFileFilter;
                if (ofd.ShowDialog() == true)
                {

                    if (mergeSelectedFile != ofd.FileName)
                    {
                        CloseFile1();
                        SetDefaultDelimitterSettings();
                        txt_FileName.Text = ofd.FileName;
                        SelectedFile1 = ofd.FileName;
                        IsMainWindowFile = false;
                        _log.Info("Selected File 1 - " + SelectedFile1);
                        ValidateFile1();
                        CheckNextButtonIsValid();
                        IsDuplicated = false;
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message + "\n" + ex.StackTrace);
                ShowCommonExceptionMessage();
            }
        }

        private void Btn_Open2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Multiselect = false;
                _log.Info("Opening file 2 selection dialog");
                ofd.Filter = FileSettings.DataImportSourceFileFilter;
                if (ofd.ShowDialog() == true)
                {
                    SetDefaultDelimitterSettings();
                    SetDefaultMAFormat();
                    txt_FileName2.Text = ofd.FileName;
                    SelectedFile2 = ofd.FileName;
                    _log.Info("Selected File 2 - " + SelectedFile2);
                    CheckNextButtonIsValid();
                    IsDuplicated = false;
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
                ShowCommonExceptionMessage();
            }
        }
        #endregion

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsSave)
            {
                if (MessageBox.Show(LocalResource.IM_MSG_CLOSE_FORM, "QuickCross", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    new Thread(() => InitClose()).Start(); // Init all values
                    ExecuteActionWhenIdle(OpenParentWindow);
                }
                else
                {
                    IsClosed = false;
                    e.Cancel = true;
                }
            }
        }

        private void OpenParentWindow()
        {
            parentWindow = new DataMerge(mAinWindow, mergeSelectedFile, mergeSelectedWb, mergeFolderPath);
            parentWindow.Owner = mAinWindow;
            parentWindow.ShowDialog();
        }
        bool IsClosed = false;
        private void Btn_Close_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IsClosed = true;
                CancelAllSettings();
                this.Close();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
                ShowCommonExceptionMessage();
            }
        }
        private void DataGrid_GotFocus(object sender, RoutedEventArgs e)
        {
        }
        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IsClosed = true;
                this.Close();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
                ShowCommonExceptionMessage();
            }
        }

        private void InitClose()
        {
            Dispatcher.Invoke(() =>
            {
                CancelAllSettings();
            });
        }

        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsShowedMergingScreen)
                {
                    IsShowedMergingScreen = false;
                    for (int k = 0; importSettings.ImportInformations.Count > 0 && k < importSettings.ImportInformations[importSettings.SelectedIndex].ChoiceWordings.Count; k++)
                    {
                        importSettings.ImportInformations[importSettings.SelectedIndex].ChoiceWordings[k].WordingText = "";
                    }
                    if (importSettings.ImportInformations.Count > 0)
                        UpdateChoiceWordingsCount(importSettings.ImportInformations[importSettings.SelectedIndex].ChoiceWordings);
                    gridChoiceWording.Columns[1].Width = 20;
                    gridChoiceWording.Columns[1].Width = new DataGridLength(1, DataGridLengthUnitType.Auto);
                }
                GoToPreviousWindow();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
                ShowCommonExceptionMessage();
            }
        }

        private void RadComma_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CheckDelimitterOtherCheckedValidation();
                ReInsertToTempData = true;
                SetFilePreview();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
                ShowCommonExceptionMessage();
            }
        }

        private void RadTab_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                CheckDelimitterOtherCheckedValidation();
                ReInsertToTempData = true;
                SetFilePreview();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
                ShowCommonExceptionMessage();
            }
        }

        private void RadSpace_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                CheckDelimitterOtherCheckedValidation();
                ReInsertToTempData = true;
                SetFilePreview();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
                ShowCommonExceptionMessage();
            }
        }

        private void RadComma_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                CheckDelimitterOtherCheckedValidation();
                ReInsertToTempData = true;
                if (!firstTimeLoad)
                    SetFilePreview();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
                ShowCommonExceptionMessage();
            }
        }

        private void RadOther_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                CheckDelimitterOtherCheckedValidation();
                ReInsertToTempData = true;
                SetFilePreview();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
                ShowCommonExceptionMessage();
            }
        }

        private void Txt_CustomDelimitter_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                StopTimer();
                StartTimer();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
                ShowCommonExceptionMessage();
            }
        }

        private void DdlEncodingChar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ReInsertToTempData = true;
                if (!firstTimeLoad)
                    SetFilePreview();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
                ShowCommonExceptionMessage();
            }
        }


        private void DdlEnclosedChar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ReInsertToTempData = true;
                if (!firstTimeLoad)
                    SetFilePreview();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
                ShowCommonExceptionMessage();
            }
        }

        private void BtnExecute_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                allowMessageBox = true;

                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                if (IsDataValidatedForExecute(true))
                {
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;

                    DataTableHelper dataTableHelper = new DataTableHelper();
                    string dbPath = file1TempFolderPath + "\\" + TemplateFile.DB_FIlE;
                    DataImportTrans dataImportTrans = new DataImportTrans();

                    ColumnImportSettings columnImportSettings = OutputUtil.NewCopy(importSettings);
                    columnImportSettings.ImportInformations = columnImportSettings.ImportInformations.AsEnumerable().Where(field => field.IsColumnSetForFlagFormat == false).ToList();

                    ConfirmationBox confirmationBox = new ConfirmationBox();
                    confirmationBox.Owner = this;
                    if (IsFile1Duplicated)
                    {
                        confirmationBox.btnSave.Visibility = Visibility.Hidden;
                    }
                    if (mergeSelectedFile == SelectedFile1)
                    {
                        confirmationBox.txtbMessage.Text = LocalResource.CONFIRMATION_BOX_CONTENT_TEXT1;
                        confirmationBox.btnSave.Visibility = Visibility.Visible;
                        confirmationBox.btnSaveAs.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        confirmationBox.btnSaveAs.Visibility = Visibility.Visible;
                        confirmationBox.btnSave.Visibility = Visibility.Hidden;
                    }
                    bool? isSave = confirmationBox.ShowDialog();

                    if (isSave != null)
                    {
                        if (isSave == true) // Save
                        {
                            ProgressBarForm progressBarForm = new ProgressBarForm(true, LocalResource.STATUS_SAVING);
                            progressBarForm.Owner = this;

                            string destPath = Path.GetDirectoryName(SelectedFile1) + "\\" + Path.GetFileNameWithoutExtension(SelectedFile1) + Qc4Extension;
                            new Thread(() => SaveFile(columnImportSettings, destPath, progressBarForm)).Start();
                            progressBarForm.ShowDialog();
                        }
                        else // Save As
                        {
                            if (confirmationBox.txtDialogStatus.Text != MessageBoxResult.Cancel.ToString())
                            {
                                SaveFileDialog saveFileDialog = new SaveFileDialog();
                                saveFileDialog.Filter = "qc4 (*" + Qc4Extension + ")|*" + Qc4Extension + "";
                                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                                if (saveFileDialog.ShowDialog() == true)
                                {
                                    string destFileName = saveFileDialog.FileName; // we will get the full path
                                    if (Path.GetExtension(destFileName) != Qc4Extension)
                                        destFileName += Qc4Extension;

                                    ProgressBarForm progressBarForm = new ProgressBarForm(true, LocalResource.STATUS_SAVING);
                                    progressBarForm.Owner = this;
                                    new Thread(() => SaveFile(columnImportSettings, destFileName, progressBarForm, true)).Start();
                                    progressBarForm.ShowDialog();
                                }
                            }
                        }
                    }
                }
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
                ShowCommonExceptionMessage();
            }
        }

        private void DragableGridMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                    DragMove();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
                ShowCommonExceptionMessage();
            }
        }
        bool ItemNameChanged = false;
        private void TxtItemName_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                ItemNameChanged = true;
                importSettings.ImportInformations[importSettings.SelectedIndex].VariableName = txtItemName.Text;
                CheckValidationForQuestion();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
                ShowCommonExceptionMessage();
            }
        }

        private void DdlAnswerType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ComboBoxItem comboAnswerType = (ComboBoxItem)ddlAnswerType.SelectedItem;
                if (comboAnswerType.Content.ToString() == "MA")
                {
                    if (importSettings.ImportInformations != null && importSettings.MAformat == MAFormat.FlagFormat && (importSettings.SelectedIndex + importSettings.ImportInformations[importSettings.SelectedIndex].NoOfChoices) == importSettings.ImportInformations.Count)
                        btn_NextData.IsEnabled = false;
                    else if (importSettings.ImportInformations != null && (importSettings.SelectedIndex + 1) < importSettings.ImportInformations.Count)
                        btn_NextData.IsEnabled = true;
                }
                else if (importSettings.ImportInformations != null && (importSettings.SelectedIndex + 1) < importSettings.ImportInformations.Count)
                    btn_NextData.IsEnabled = true;
                ReleaseColumnsForFlagFormat();
                CheckAnswerChoiceValidation(Visibility.Visible);
                if (importSettings.ImportInformations != null && importSettings.ImportInformations.Count > 0)
                    importSettings.ImportInformations[importSettings.SelectedIndex].AnswerType = comboAnswerType.Content.ToString();

                AllocateColumnsIfFlagFormat();
                ItemNameChanged = true;
                CheckValidationForQuestion();
                SetInformationGridBgColor();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
                ShowCommonExceptionMessage();
            }
        }
        DataGridLength txtSize = 0;
        private void UpdateChoiceWordingList()
        {
            try
            {
                ReleaseColumnsForFlagFormat();
                try
                {
                    int chCount = Convert.ToInt16(txtNoOfChoices.Text);
                    chCount = chCount < 0 ? 0 : chCount;
                    if (chCount > 1000)
                    {
                        txtNoOfChoices.Text = "1000";
                        chCount = 1000;
                    }
                    importSettings.ImportInformations[importSettings.SelectedIndex].NoOfChoices = chCount;
                    if (ddlAnswerType.Text == "MA" && importSettings.MAformat == MAFormat.FlagFormat)
                    {
                        int noOfCho = chCount == 0 ? 1 : chCount;
                        if ((importSettings.SelectedIndex + noOfCho) == importSettings.ImportInformations.Count)
                            btn_NextData.IsEnabled = false;
                        else
                            btn_NextData.IsEnabled = true;
                    }
                }
                catch
                {
                    importSettings.ImportInformations[importSettings.SelectedIndex].NoOfChoices = 0;
                    txtNoOfChoices.Text = "";
                    importSettings.ImportInformations[importSettings.SelectedIndex].ChoiceWordings = new List<ChoiceWording>();
                }
                if (Convert.ToInt16(importSettings.ImportInformations[importSettings.SelectedIndex].NoOfChoices) > 8)
                {
                    if (gridChoiceWording.HorizontalScrollBarVisibility == ScrollBarVisibility.Hidden || isFirstTimeLoad)
                    {
                        gridChoiceWording.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
                        isFirstTimeLoad = false;
                    }
                    gridChoiceWording.Columns[1].Header = "Choice wording ";
                    gridChoiceWording.Columns[1].Width = 20;
                    gridChoiceWording.Columns[1].Width = new DataGridLength(1, DataGridLengthUnitType.Auto);
                }
                else
                {
                    if (gridChoiceWording.HorizontalScrollBarVisibility == ScrollBarVisibility.Hidden || isFirstTimeLoad)
                    {
                        gridChoiceWording.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
                        isFirstTimeLoad = false;
                    }
                    gridChoiceWording.Columns[1].Header = "Choice wording      ";
                }
                txtSize = gridChoiceWording.Columns[1].Width;

                if (importSettings.ImportInformations[importSettings.SelectedIndex].NoOfChoices > 1000)
                {
                    importSettings.ImportInformations[importSettings.SelectedIndex].NoOfChoices = 1000;
                    txtNoOfChoices.Text = "1000";
                    importSettings.ImportInformations[importSettings.SelectedIndex].ChoiceWordings.RemoveRange(1000, Convert.ToInt16(txtNoOfChoices.Text) - 1000);
                }

                int choiceWordings = importSettings.ImportInformations[importSettings.SelectedIndex].NoOfChoices;
                if (importSettings.ImportInformations[importSettings.SelectedIndex].ChoiceWordings.Count < choiceWordings)
                {
                    int noOfItemsToAdd = choiceWordings - importSettings.ImportInformations[importSettings.SelectedIndex].ChoiceWordings.Count;

                    for (int i = 1; i <= noOfItemsToAdd; i++)
                    {
                        ChoiceWording choiceWording = new ChoiceWording()
                        {
                            SlNo = importSettings.ImportInformations[importSettings.SelectedIndex].ChoiceWordings.Count + 1,
                            WordingText = string.Empty
                        };
                        importSettings.ImportInformations[importSettings.SelectedIndex].ChoiceWordings.Add(choiceWording);
                    }
                }
                else if (importSettings.ImportInformations[importSettings.SelectedIndex].ChoiceWordings.Count > choiceWordings)
                {
                    int startIndex = choiceWordings;
                    int removeCount = (importSettings.ImportInformations[importSettings.SelectedIndex].ChoiceWordings.Count) - choiceWordings;
                    importSettings.ImportInformations[importSettings.SelectedIndex].ChoiceWordings.RemoveRange(startIndex, removeCount);
                }

                if (importSettings.ImportInformations[importSettings.SelectedIndex].NoOfChoices > 1000)
                {
                    importSettings.ImportInformations[importSettings.SelectedIndex].NoOfChoices = 1000;
                    txtNoOfChoices.Text = "1000";
                    importSettings.ImportInformations[importSettings.SelectedIndex].ChoiceWordings.RemoveRange(1000, Convert.ToInt16(txtNoOfChoices.Text) - 1000);
                }

                AllocateColumnsIfFlagFormat();
                UpdateChoiceWordingsCount(importSettings.ImportInformations[importSettings.SelectedIndex].ChoiceWordings);
                CheckValidationForQuestion();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
                ShowCommonExceptionMessage();
            }

        }

        private static System.Timers.Timer timerKeyup = null;

        private void TxtNoOfChoices_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                StopTimer();
                StartTimer();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
                ShowCommonExceptionMessage();
            }
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            StopTimer();
            if (screenMode == DataImportScreenMode.FilePreview)
            {
                Dispatcher.Invoke(() => ExecuteRead());
            }
            if (screenMode == DataImportScreenMode.MergingSettings)
            {
                Dispatcher.Invoke(() => TxtNoOfChoices_LostFocus(null, null));
            }
        }


        private void ExecuteRead()
        {
            ReInsertToTempData = true;
            SetFilePreview();
        }

        private void StartTimer()
        {
            timerKeyup = new System.Timers.Timer(1000);
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


        private void TxtQuestionTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                importSettings.ImportInformations[importSettings.SelectedIndex].QuestionTitle = txtQuestionTitle.Text;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
                ShowCommonExceptionMessage();
            }
        }

        private void TxtQuestionSentence_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                importSettings.ImportInformations[importSettings.SelectedIndex].QuestionSentence = txtQuestionSentence.Text;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
                ShowCommonExceptionMessage();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox txtChoiceWording = sender as TextBox;
                if (!IsPasteOperation)
                {
                    int selectIndx = gridChoiceWording.SelectedIndex;
                    if (selectIndx == -1)
                        selectIndx = gridChoiceWording.Items.IndexOf(gridChoiceWording.CurrentItem);
                    UpdateChoiceWordingText(selectIndx, txtChoiceWording.Text);
                }
                else
                {
                    IsPasteOperation = false;
                    int selectedIndex = gridChoiceWording.SelectedIndex;
                    int gridRowLength = gridChoiceWording.Items.Count;

                    string regexReplacedStr = Regex.Replace(txtChoiceWording.Text, "(?!(([^\"]*\"){2})*[^\"]*$)(\\n|\\r|\\r\\n)+", string.Empty);

                    List<string> choiceWordings = regexReplacedStr.Split(new[] { "\r\n" }, StringSplitOptions.None).ToList();

                    for (int i = 0; i <= choiceWordings.Count - 1; i++)
                    {
                        string[] nChoiceWordings = choiceWordings[i].Split(new[] { "\r", "\n" }, StringSplitOptions.None);
                        choiceWordings[i] = string.Join("\n", nChoiceWordings).Replace("\"", "");
                    }

                    int j = 0;
                    for (int i = selectedIndex; i <= gridRowLength - 1; i++)
                    {
                        if (j <= (choiceWordings.Count - 1))
                        {
                            UpdateChoiceWordingText(i, choiceWordings[j]);
                        }
                        j++;
                    }

                    UpdateChoiceWordingsCount(importSettings.ImportInformations[importSettings.SelectedIndex].ChoiceWordings);
                    CheckValidationForQuestion();
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
                ShowCommonExceptionMessage();
            }

        }

        private void RebindChoiceWordings()
        {
            TxtNoOfChoices_LostFocus(null, null);
        }

        private void RefreshChoiceWordingGrid()
        {
            try
            {
                gridChoiceWording.CommitEdit();
                CollectionViewSource.GetDefaultView(gridChoiceWording.ItemsSource).Refresh();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.Message);
            }
        }

        private void DataGridColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IsDataValidatedForExecute();
                IsHeaderClicked = true;
                DataGridColumnHeader dataGridColumnHeader = sender as DataGridColumnHeader;
                int columnIndex = dataGridColumnHeader.DisplayIndex;
                List<ImportInformation> selectedData = importSettings.ImportInformations.AsEnumerable().Where(field => (field.ColumnIndex == columnIndex) && (field.IsColumnSetForFlagFormat == true)).ToList();
                if (selectedData != null && selectedData.Count > 0)
                {
                    MessageDialog.Warning(LocalResource.IM_MSG_WARNING_ALLOCATE_COL);
                }
                else
                {
                    LoadMergingColumnDataToView(columnIndex);
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
                ShowCommonExceptionMessage();
            }
        }
        bool isFirstTimeLoad = false;
        private void Btn_Next_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsDuplicated && ddlFile1Key1.Visibility == Visibility.Visible)
                {
                    MessageDialog.ErrorOk(LocalResource.IM_MSG_IMPORT_DUPLICATE);
                    return;
                }
                isFirstTimeLoad = true;
                gridChoiceWording.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
                GoToNextWindow();
                this.Activate();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
                ShowCommonExceptionMessage();
            }
        }

        private void DdlFile2Key1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                enableVennDiagramCalculation = true;
                if (!IsForceKeyChange) CalculateVennDiagramCounts();
                HandleNextButtonEnability();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
                ShowCommonExceptionMessage();
            }
        }

        private void DdlFile1Key1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                enableVennDiagramCalculation = true;
                if (!IsForceKeyChange) CalculateVennDiagramCounts();
                HandleNextButtonEnability();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
                ShowCommonExceptionMessage();
            }
        }

        private void DdlFile1Key2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                enableVennDiagramCalculation = true;
                if (!IsForceKeyChange) CalculateVennDiagramCounts();
                HandleNextButtonEnability();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
                ShowCommonExceptionMessage();
            }
        }


        private void DdlFile2Key2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                enableVennDiagramCalculation = true;
                if (!IsForceKeyChange) CalculateVennDiagramCounts();
                HandleNextButtonEnability();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
                ShowCommonExceptionMessage();
            }
        }

        private void ChkSelectAll_Changed(object sender, RoutedEventArgs e)
        {
            try
            {
                importSettings.ImportInformations = new List<ImportInformation>(); // Resetting importInformation
                CheckBox chkSelectAll = sender as CheckBox;

                GetVisualChildCollection<DataGridColumnHeader>(gridColumnSelection, "check");

                ValidateCheckedColumns();


                HandleNextButtonEnability();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
                ShowCommonExceptionMessage();
            }
        }
        public List<T> GetVisualChildCollection<T>(object parent, string purp) where T : Visual
        {
            List<T> visualCollection = new List<T>();
            GetVisualChildCollection(parent as DependencyObject, visualCollection, purp);
            return visualCollection;
        }
        static int b = -1;
        private void GetVisualChildCollection<T>(DependencyObject parent, List<T> visualCollection, string purp) where T : Visual
        {
            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is T)
                {
                    if (purp == "check")
                        dataGridHelper.FindVisualChildByName<CheckBox>(child, "chkGridColumn", chkSelectAll, purp);
                    else if (purp == "uncheck")
                        dataGridHelper.FindVisualChildByName<CheckBox>(child, "chkGridColumn", chkSelectAll, purp);
                    else if (purp == "addIndices")
                        dataGridHelper.FindVisualChildByName<CheckBox>(child, "chkGridColumn", chkSelectAll, purp);
                    else if (purp == "setScroll")
                        (child as DataGridColumnHeader).Cursor = Cursors.ScrollS;
                    else if (purp == "setBg")
                    {
                        if (importSettings.SelectedIndex == b)
                        {
                            dataGridHelper.SetBgColorForColumn(gridInformation, b, Brushes.AliceBlue, child as DataGridColumnHeader);
                        }
                        else if (importSettings.ImportInformations != null && b != -1)
                        {
                            List<ImportInformation> importInformations = importSettings.ImportInformations.Where(field => field.ColumnIndex == b).ToList();
                            if (importInformations == null || importInformations.Count == 0)
                            {
                                dataGridHelper.SetBgColorForColumn(gridInformation, b, Brushes.LightGray, child as DataGridColumnHeader);
                            }
                            else
                            {
                                if (importInformations[0].IsDataValidated)
                                {
                                    dataGridHelper.SetBgColorForColumn(gridInformation, b, Brushes.White, child as DataGridColumnHeader);
                                }
                                else
                                {
                                    dataGridHelper.SetBgColorForColumn(gridInformation, b, Brushes.LightGray, child as DataGridColumnHeader);
                                }
                            }
                        }
                        else if (b != -1)
                        {
                            dataGridHelper.SetBgColorForColumn(gridInformation, b, Brushes.LightGray, child as DataGridColumnHeader);
                        }
                        b++;
                    }
                    visualCollection.Add(child as T);
                }
                else if (child != null)
                {
                    GetVisualChildCollection(child, visualCollection, purp);
                }
            }
        }
        private void Btn_PrevData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IsDataValidatedForExecute();
                GoToPrevColumn();
                gridInformation.ScrollIntoView(gridInformation.Items.GetItemAt(0), gridInformation.Columns[importSettings.SelectedIndex]);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
                ShowCommonExceptionMessage();
            }
        }

        private void Btn_NextData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IsDataValidatedForExecute();
                GoToNextColumn();
                gridInformation.ScrollIntoView(gridInformation.Items.GetItemAt(0), gridInformation.Columns[importSettings.SelectedIndex]);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
                ShowCommonExceptionMessage();
            }
        }

        private void ChkGridColumn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                importSettings.ImportInformations = new List<ImportInformation>(); // Resetting importInformation
                ValidateCheckedColumns();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
                ShowCommonExceptionMessage();
            }
        }

        private void Txt_NotApplicableChar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (screenMode == DataImportScreenMode.ColumnSelection)
                importSettings.ImportInformations = new List<ImportInformation>(); // Resetting importInformation
        }

        private void GridFile2Contents_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
        }

        private void TxtNoOfChoices_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                UpdateChoiceWordingList();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
                ShowCommonExceptionMessage();
            }
        }


        #endregion

        #region DataLoading
        private void SaveFile(ColumnImportSettings columnImportSettings, string destFileName, ProgressBarForm progressBarForm, bool isSaveAs = false)
        {
            DataImportTrans dataImportTrans = new DataImportTrans();
            Excel.Worksheet xlWorksheetQns = ExcelUtil.GetWorkSheetByCodeName(DataImportFile1WorkBook, SheetCodeName.QuestionSetting);
            Excel.Worksheet xlWorkSheetAfterProcess = ExcelUtil.GetWorkSheetBySheetName(DataImportFile1WorkBook, SheetType.sh_DataAfterProcess);

            for (int i = 0; i <= columnImportSettings.ImportInformations.Count - 1; i++)
            {
                ImportInformation importInformation = columnImportSettings.ImportInformations[i];
                if (importInformation.AnswerType == AnswerType.MA && columnImportSettings.MAformat == MAFormat.FlagFormat)
                {
                    List<string> MAColumns = new List<string>();
                    int noOfChoices = importInformation.NoOfChoices;
                    for (int j = 0; j <= noOfChoices - 1; j++)
                    {
                        if (j == 0)
                            MAColumns.Add(columnImportSettings.BeforeProcessingData.Columns[(importInformation.ColumnIndex)].ColumnName);
                        else
                            MAColumns.Add(columnImportSettings.BeforeProcessingData.Columns[(importInformation.ColumnIndex + j)].ColumnName);
                    }
                    columnImportSettings.ImportInformations[i].MAColumns = MAColumns;
                }

            }

            columnImportSettings.DataCount = gridInformation.Items.Count;
            columnImportSettings.ImportInformations = columnImportSettings.ImportInformations.AsEnumerable().Where(field => field.IsColumnSetForFlagFormat == false).ToList();

            if (dataImportTrans.UpdateNewColumns(DBPath, DataImportFile1WorkBook, columnImportSettings))
            {
                new DataImportHelper().DropTempTable(DBPath);
                if ((IsMainWindowFile && isSaveAs) || (!IsMainWindowFile))
                {
                    QcFileHelper.SaveFile(destFileName, file1TempFolderPath, true, progressBarForm, DataImportFile1WorkBook);
                }

                Dispatcher.Invoke(() =>
                {
                    CancelAllSettings();
                });
                if (!isSaveAs)
                {
                    try
                    {
                        Dispatcher.Invoke(() =>
                        {
                            progressBarForm.Close();
                        });
                    }
                    catch (Exception e) { }

                    MessageDialog.Info(LocalResource.IM_MSG_IMPORT_COMPLETE2);
                }
                else
                    MessageDialog.Info(LocalResource.IM_MSG_IMPORT_COMPLETE);

            }
            else
            {
                try
                {
                    Dispatcher.Invoke(() =>
                    {
                        progressBarForm.Close();
                    });
                }
                catch (Exception e) { }
                MessageDialog.ErrorOk(LocalResource.IM_MSG_IMPORT_FAILED);
            }
            Dispatcher.Invoke(() =>
            {
                try
                {
                    progressBarForm.Close();
                }
                catch (Exception e) { }
                IsSave = true;
                this.Close();
                mAinWindow.Show();
            });
        }

        private bool IsDataValidatedForExecute(bool IsSaveClick = false)
        {
            allowMessageBox = true;
            messageBoxAllocated = false;
            if (!IsSaveClick)
                CheckValidationForQuestion();
            else
            {
                int selectedIndex = importSettings.SelectedIndex;
                int macols = 0;
                for (int i = 0; i < importSettings.ImportInformations.Count; i++)
                {
                    ImportInformation importInformation = importSettings.ImportInformations[i];
                    if (macols != 0)
                    {
                        macols--;
                    }
                    if (importInformation.AnswerType == "MA" && importSettings.MAformat == MAFormat.FlagFormat)
                    {
                        macols = importInformation.NoOfChoices - 1;
                    }

                    importSettings.SelectedIndex = importInformation.ColumnIndex;
                    if (!CheckValidationForQuestion())
                    {
                        gridInformation.ScrollIntoView(gridInformation.Items.GetItemAt(0), gridInformation.Columns[importSettings.SelectedIndex]);
                        break;
                    }
                    if (importSettings.MAformat == MAFormat.FlagFormat && importInformation.AnswerType == "MA")
                    {
                        i += importInformation.NoOfChoices - 1;
                    }
                }
                gridInformation.ScrollIntoView(gridInformation.Items.GetItemAt(0), gridInformation.Columns[importSettings.SelectedIndex]);
                importSettings.SelectedIndex = selectedIndex;
            }
            allowMessageBox = false;
            messageBoxAllocated = false;
            if (IsMergingScreenValid())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void LoadFile2onTextArea(ProgressBarForm pb)
        {
            IsNextButtonEnabled = false;
            try
            {
                FileUtil fileUtil = new FileUtil();
                if (SelectedFile2 != string.Empty)
                {
                    Encoding encoding = Encoding.UTF8;
                    Dispatcher.Invoke(() =>
                    {
                        if (ddlEncodingChar.SelectedIndex == 0)
                        {
                            encoding = Encoding.GetEncoding("Shift-JIS");
                        }

                        txt_PreviewData.Text = fileUtil.ReadAllTextFromFile(SelectedFile2, encoding, 0, 101);
                    });
                    IsNextButtonEnabled = true;
                }

            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);

                IsNextButtonEnabled = false;
                Dispatcher.Invoke(() =>
                {
                    MessageDialog.ErrorOk(LocalResource.IM_MSG_FAILED_READ);
                    txt_PreviewData.Text = string.Empty;
                    GoToPreviousWindow();
                    ClearFile2Fields();
                });
            }
            finally
            {
                if (pb != null)
                {
                    Dispatcher.Invoke(() =>
                    {
                        pb.Close();
                    });
                }
            }

        }


        private void LoadComboBoxesForVennDiagram(DataTable sourceFileData)
        {
            string dbPath = file1TempFolderPath + "\\" + TemplateFile.DB_FIlE;

            DataTable dt = new DataTable();

            Excel.Worksheet QuestionSettings = ExcelUtil.GetWorkSheetByCodeName(DataImportFile1WorkBook, SheetCodeName.QuestionSetting);
            FileUtil fileUtil = new FileUtil();
            dt = fileUtil.ReadDataFromSheet(QuestionSettings, 2, true, 10);

            DataTable dataTableClone = new DataTable();
            dataTableClone.PrimaryKey = null;

            dataTableClone.Columns.Add("ColumnName");
            dataTableClone.Columns.Add("ColumnValue");

            DataRow drow = dataTableClone.NewRow();
            drow["ColumnName"] = ComboBoxSettings.NoneText;
            drow["ColumnValue"] = ComboBoxSettings.NoneText;
            dataTableClone.Rows.Add(drow);

            file1Keys = new List<KeyValueObject>();
            file1Variables = new List<KeyValueObject>();
            DataTable questions = new DataTable();
            using (SQLiteConnection dbSource = DBHelper.GetConnection(DBHelper.GetConnectionString(DBPath)))
            {
                dbSource.Open();
                string sSql = " Select id,variable from question";
                questions = DBHelper.GetDataTable(sSql, dbSource);
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dataRow = dt.Rows[i];
                drow = dataTableClone.NewRow();
                string flagColumnValue = Convert.ToString(dataRow[1]).Trim();
                string columnName = Convert.ToString(dataRow[5]).Trim();
                string answerType = Convert.ToString(dataRow[6]).Trim();
                KeyValueObject keyValueObject = new KeyValueObject();
                keyValueObject.Key = columnName;
                keyValueObject.Value = flagColumnValue;
                file1Variables.Add(keyValueObject);

                if ((flagColumnValue != "New" && flagColumnValue != "An" && flagColumnValue != "Imp") && answerType != Constants.AnswerType.MA && columnName.Length > 0)
                {
                    drow["ColumnName"] = columnName.Length > 53 ? columnName.Substring(0, 49) + "..." : columnName;
                    if (columnName == QuestionVariableValue.QuestionVariableItem)
                    {
                        drow["ColumnValue"] = DBSettings.SampleIdColumnName;
                    }
                    else
                    {
                        int qId = 0;
                        if (questions.Select("variable ='" + columnName + "'").Length > 0)
                        {
                            var question = questions.Select("variable ='" + columnName + "'").CopyToDataTable();
                            qId = Convert.ToInt32(question.Rows[0][0]);
                        }

                        if (qId != 0)
                            drow["ColumnValue"] = DBSettings.ColumnNamePreText + qId;
                        else
                            drow["ColumnValue"] = ComboBoxSettings.NoneText;
                    }

                    dataTableClone.Rows.Add(drow);
                    keyValueObject.Key = drow["ColumnName"].ToString();
                    keyValueObject.Value = drow["ColumnValue"].ToString();
                    file1Keys.Add(keyValueObject);
                }
            }

            ddlFile1Key1.ItemsSource = dataTableClone.AsDataView();
            ddlFile1Key1.DisplayMemberPath = dataTableClone.Columns["ColumnName"].ToString();
            ddlFile1Key1.SelectedValuePath = dataTableClone.Columns["ColumnValue"].ToString();
            ddlFile1Key1.SelectedIndex = 0;

            ddlFile1Key2.ItemsSource = dataTableClone.AsDataView();
            ddlFile1Key2.DisplayMemberPath = dataTableClone.Columns["ColumnName"].ToString();
            ddlFile1Key2.SelectedValuePath = dataTableClone.Columns["ColumnValue"].ToString();
            ddlFile1Key2.SelectedIndex = 0;

            DataTable dtColumnList = LoadColumnNamesFromDt(sourceFileData);

            ddlFile2Key1.ItemsSource = dtColumnList.AsDataView();
            ddlFile2Key1.DisplayMemberPath = dtColumnList.Columns["ColumnName"].ToString();
            ddlFile2Key1.SelectedValuePath = dtColumnList.Columns["ColumnValue"].ToString();
            ddlFile2Key1.SelectedIndex = 0;

            ddlFile2Key2.ItemsSource = dtColumnList.AsDataView();
            ddlFile2Key2.DisplayMemberPath = dtColumnList.Columns["ColumnName"].ToString();
            ddlFile2Key2.SelectedValuePath = dtColumnList.Columns["ColumnValue"].ToString();
            ddlFile2Key2.SelectedIndex = 0;

        }

        private DataTable LoadColumnNamesFromDt(DataTable mainTable)
        {
            TempColumns = new List<string>();
            DataTable dt = new DataTable();
            dt.Columns.Add("ColumnName");
            dt.Columns.Add("ColumnValue");

            DataRow drow = dt.NewRow(); // Adding none in the 1st position in combo box
            drow["ColumnName"] = ComboBoxSettings.NoneText;
            drow["ColumnValue"] = ComboBoxSettings.NoneText;
            dt.Rows.Add(drow);

            List<string> columns = new List<string>();
            for (int i = 0; i <= mainTable.Columns.Count - 1; i++)
            {
                string columnName = Convert.ToString(mainTable.Columns[i]).Trim();

                columns.Add(columnName);

                drow = dt.NewRow();
                drow["ColumnName"] = (i + 1).ToString() + ". " + (columnName.Length > 53 ? columnName.Substring(0, 49) + "..." : columnName);
                drow["ColumnValue"] = mainTable.Columns[i];
                dt.Rows.Add(drow);
            }
            TempColumns = columns;
            return dt;
        }

        private void AddAllColumnsToCacheIfNotAvailable()
        {
            for (int i = 0; i <= importSettings.BeforeProcessingData.Columns.Count - 1; i++)
            {
                importSettings.SelectedIndex = i;
                importSettings.SelectedColumn = importSettings.BeforeProcessingData.Columns[i].ColumnName;
                GetSelectedInformation();
            }

            importSettings.ImportInformations = importSettings.ImportInformations.OrderBy(field => field.ColumnIndex).ToList();

            importSettings.SelectedIndex = 0;
            CheckColumnNextAndPreviousIsValid();
        }

        private void PopulateModelDataOnView()
        {
            BindInformationOnModelAndScreen(GetSelectedInformation());
        }

        private ImportInformation GetSelectedInformation()
        {
            ImportInformation importInformation = null;
            if (importSettings.ImportInformations != null)
            {
                List<ImportInformation> importInformations = importSettings.ImportInformations.Where(field => field.ColumnIndex == importSettings.SelectedIndex).ToList();
                if (importInformations == null || importInformations.Count == 0)
                {
                    importInformation = GetDefaultImportInformation(importSettings.SelectedIndex, importSettings.SelectedColumn);
                    importSettings.ImportInformations.Add(importInformation);
                }
                else
                {
                    importInformation = importInformations[0];
                }
            }
            else
            {
                importSettings.ImportInformations = new List<ImportInformation>();
                importInformation = GetDefaultImportInformation(importSettings.SelectedIndex, importSettings.SelectedColumn);
                importSettings.ImportInformations.Add(importInformation);
            }
            return importInformation;
        }

        private void LoadMergingColumnDataToView(int columnIndex)
        {
            importSettings.SelectedIndex = columnIndex;
            CheckColumnNextAndPreviousIsValid();
            SetInformationGridBgColor();
            PopulateModelDataOnView();
        }

        #endregion

        #region ScreenNavigation
        private void GoToNextWindow()
        {
            if (IsScreenValid())
            {
                IsNextButtonEnabled = false;
                switch (screenMode)
                {
                    case DataImportScreenMode.FileSelection:
                        string fileExtension = Path.GetExtension(SelectedFile2);
                        if (fileExtension == ".xls" || fileExtension == ".xlsx" || fileExtension == ".xlsm") // if its excel file, no need of showing delimitter and textarea preview
                        {
                            screenMode = DataImportScreenMode.FilePreview;
                            ShowPreviewScreen();
                            if (dtFile2 != null && dtFile2.Rows.Count > 0)
                            {
                                screenMode = DataImportScreenMode.VennDiagram;
                                ShowVennDiagramScreen();
                            }
                            else
                            {
                                screenMode = DataImportScreenMode.FileSelection;
                            }
                        }
                        else
                        {
                            screenMode = DataImportScreenMode.FilePreview;
                            ShowPreviewScreen();
                        }
                        break;

                    case DataImportScreenMode.DelimitterSelection:
                        screenMode = DataImportScreenMode.FilePreview;
                        ShowPreviewScreen();
                        break;

                    case DataImportScreenMode.FilePreview:
                        screenMode = DataImportScreenMode.VennDiagram;
                        ShowVennDiagramScreen();
                        break;
                    case DataImportScreenMode.VennDiagram:

                        screenMode = DataImportScreenMode.ColumnSelection;
                        ShowColumnSelectionScreen();
                        break;
                    case DataImportScreenMode.ColumnSelection:
                        screenMode = DataImportScreenMode.MergingSettings;
                        ShowMergingSettingsScreen();
                        break;
                }
            }
            else
                IsNextButtonEnabled = true;
        }

        private bool IsScreenValid()
        {
            bool isValid = true;
            if (screenMode == DataImportScreenMode.VennDiagram)
            {
                if ((isFile1Key1Exist && !isFile2Key1Exist) || (!isFile1Key1Exist && isFile2Key1Exist) || (isFile1Key2Exist && !isFile2Key2Exist) || (!isFile1Key2Exist && isFile2Key2Exist))
                {
                    isValid = false;
                    MessageDialog.Warning(LocalResource.IM_MSG_FILE_SELECTION_WARN);
                }
                else if (!isFile1Key1Exist && IsPro)
                {
                    System.Windows.Forms.DialogResult res = MessageDialog.WarningOkCancel(LocalResource.IM_KEY_SELECTION_WARNING_MESSAGE);
                    if (res == System.Windows.Forms.DialogResult.Cancel)
                    {
                        isValid = false;
                    }
                }
            }
            return isValid;
        }

        private void GoToPreviousWindow()
        {
            string fileExtension = System.IO.Path.GetExtension(SelectedFile2);
            switch (screenMode)
            {
                case DataImportScreenMode.DelimitterSelection:
                    screenMode = DataImportScreenMode.FileSelection;
                    ShowFileSelectionScreen();
                    break;
                case DataImportScreenMode.FilePreview:
                    if (fileExtension == ".xls" || fileExtension == ".xlsx" || fileExtension == ".xlsm") // if its excel file, no need of showing delimitter and textarea preview
                    {
                        screenMode = DataImportScreenMode.FileSelection;
                        ShowFileSelectionScreen();
                    }
                    else
                    {
                        screenMode = DataImportScreenMode.FileSelection;
                        ShowFileSelectionScreen();
                    }
                    break;

                case DataImportScreenMode.VennDiagram:
                    screenMode = DataImportScreenMode.FilePreview;
                    ShowPreviewScreen();
                    if (fileExtension == ".xls" || fileExtension == ".xlsx" || fileExtension == ".xlsm") // if its excel file, no need of showing delimitter and textarea preview
                    {
                        screenMode = DataImportScreenMode.FileSelection;
                        ShowFileSelectionScreen();
                    }
                    break;
                case DataImportScreenMode.ColumnSelection:
                    screenMode = DataImportScreenMode.VennDiagram;
                    ShowVennDiagramScreen();
                    break;
                case DataImportScreenMode.MergingSettings:
                    screenMode = DataImportScreenMode.ColumnSelection;
                    ShowColumnSelectionScreen();
                    break;

            }

        }

        private void SetNextButtonEnability()
        {
            if (btn_Next != null)
            {
                btn_Next.IsEnabled = IsNextButtonEnabled;
            }
        }

        private void HandleNextButtonEnability()
        {
            ExecuteActionWhenIdle(SetNextButtonEnability);
        }

        private void ShowFileSelectionScreen()
        {
            SetPreviewScreenVisibility(Visibility.Hidden);
            SetDelimitterSettingsVisibility(Visibility.Hidden);
            btn_Back.Visibility = Visibility.Hidden;
            btn_Next.Visibility = Visibility.Visible;
            FileSelectionEnability(true);
            if (SelectedFile1.Trim().Length > 0 && SelectedFile2.Trim().Length > 0)
            {
                btn_Next.IsEnabled = true;
            }
            else
            {
                btn_Next.IsEnabled = false;
            }
        }

        private void FileSelectionEnability(bool isEnabled)
        {
            btn_Open.IsEnabled = isEnabled;
            btn_Open2.IsEnabled = isEnabled;
        }

        private void ShowDelimitterSpecificationScreen()
        {
            SetPreviewScreenVisibility(Visibility.Hidden);
            FileSelectionEnability(false);
            SetFilePreview();
            SetDelimitterSettingsVisibility(Visibility.Visible);
            btn_Back.Visibility = Visibility.Visible;
            HandleNextButtonEnability();
        }

        private void SetDelimitterSettingsVisibility(Visibility visibility)
        {
            lblCharCodeDesc.Visibility = visibility;
            ddlEncodingChar.Visibility = visibility;
            lblDelimitterDesc.Visibility = visibility;
            radGroupDelimitter.Visibility = visibility;
            lblStringQuotesDesc.Visibility = visibility;
            ddlEnclosedChars.Visibility = visibility;
            txt_CustomDelimitter.Visibility = visibility;
            lblFile2Preview.Visibility = visibility;
            txt_PreviewData.Visibility = visibility;
            lblDelimitterSpecify.Visibility = visibility;

            btn_Next.Visibility = Visibility.Visible;
            if (visibility == Visibility.Hidden)
                btn_Next.IsEnabled = false;
            else
                btn_Next.IsEnabled = true;
        }

        private void SetFilePreview()
        {
            if (ReInsertToTempData)
            {
                IsNextButtonEnabled = false;
                try
                {
                    LoadFilePreviewInProgressBar();
                }
                catch (Exception ex)
                {
                    _log.LogError(ex.Message + "\n" + ex.StackTrace);
                }
            }
            else
            {
                IsNextButtonEnabled = true;
            }
            ExecuteActionWhenIdle(HandleNextButtonEnability);
        }

        private void OnWorkerMethodComplete(double value, string status)
        {
            progress.Dispatcher.Invoke(DispatcherPriority.Normal,
            new Action(
            delegate ()
            {
                progress.UpdateProgressBar(value, status);
            }
            ));
        }

        private void LoadFilePreviewInProgressBar()
        {
            try
            {
                if (!IsClosed)
                {
                    progress = new ProgressBar(LocalResource.LBL_DATA_IMPORT_TITILE);
                    progress.Owner = this;
                }
                IsClosed = false;
                if (screenMode == DataImportScreenMode.DelimitterSelection)
                {
                    new Thread(() => LoadFile2onTextArea(progress)).Start();
                    progress.ShowDialog();

                }
                else if (screenMode == DataImportScreenMode.FilePreview)
                {
                    Encoding encoding = Encoding.UTF8;
                    if (ddlEncodingChar.SelectedIndex == 0)
                        encoding = Encoding.GetEncoding("Shift-JIS");

                    string columnDelimitter = ",";

                    if (radTab.IsChecked == true)
                        columnDelimitter = "\t";
                    else if (radSpace.IsChecked == true)
                        columnDelimitter = " ";
                    else if (radOther.IsChecked == true)
                        columnDelimitter = txt_CustomDelimitter.Text;

                    ComboBoxItem typeItem = (ComboBoxItem)ddlEnclosedChars.SelectedItem;

                    char? enclosedChar;
                    if (typeItem.Content.ToString() == "None")
                        enclosedChar = null;
                    else
                        enclosedChar = typeItem.Content.ToString().ToCharArray()[0];

                    if (enclosedChar == null || columnDelimitter != Convert.ToString(enclosedChar))
                    {
                        new Thread(() => LoadSourceFile(encoding, columnDelimitter, enclosedChar)).Start();
                        if (dtFile2 != null)
                        {
                            OnWorkerMethodComplete(1, LocalResource.IM_PROGRESSBAR_OPENING_FILE);
                            progress.ShowDialog();
                        }
                    }
                    else
                    {
                        dtFile2 = new DataTable();
                        MessageDialog.Warning(LocalResource.IM_MSG_INVALID_WARN);

                        LoadDataGrid(true);
                    }


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadDataGrid(bool isEmpty = false)
        {
            Dispatcher.InvokeAsync(() =>
            {
                try
                {
                    if (dtFile2 != null && dtFile2.Columns.Count > 0)
                    {
                        try
                        {
                            dataGridHelper.BindDataGrid(ref gridFile2Contents, dtFile2);
                        }
                        catch (Exception ex)
                        {
                            _log.Info(ex.Message + "\n" + ex.StackTrace);
                            if (ex.Message.Contains("Syntax error in PropertyPath"))
                            {
                                dataGridHelper.BindDataGrid(ref gridFile2Contents, new DataTable());
                                MessageDialog.Warning(LocalResource.IM_MSG_INVALID_FILE_WARN);
                            }
                            else
                            {
                                throw;
                            }
                        }
                    }
                    else
                    {
                        dataGridHelper.BindDataGrid(ref gridFile2Contents, new DataTable());
                        dtFile2 = new DataTable();
                    }
                }
                catch (Exception ex)
                {
                    _log.Info(ex.Message + "\n" + ex.StackTrace);
                    IsNextButtonEnabled = false;
                    dataGridHelper.BindDataGrid(ref gridFile2Contents, new DataTable());
                    dtFile2 = new DataTable();
                    GoToPreviousWindow();
                    ClearFile2Fields();
                    MessageDialog.ErrorOk(LocalResource.IM_MSG_FAILED_READ);
                }
                finally
                {
                    if (dtFile2 != null)
                    {
                        LoadComboBoxesForVennDiagram(dtFile2);

                        SetDefaultMAFormat();
                        ReInsertToTempData = true;
                        enableVennDiagramCalculation = true;

                        OnWorkerMethodComplete(100, LocalResource.IM_PROGRESSBAR_READING_COMPLETED);
                    }
                }

                if (gridFile2Contents != null && gridFile2Contents.Items.Count > 0)
                {
                    if (radOther.IsChecked == true)
                    {
                        if (txt_CustomDelimitter.Text.Length > 0)
                            IsNextButtonEnabled = true;
                        else
                            IsNextButtonEnabled = false;
                    }
                    else
                        IsNextButtonEnabled = true;
                }

                if (!isEmpty)
                {
                    OnWorkerMethodComplete(100, LocalResource.IM_PROGRESSBAR_COMPLETING);
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encoding"></param>
        /// <param name="columnDelimitter"></param>
        /// <param name="enclosedChar"></param>
        /// <param name="delegateFunction"></param> Update Progress function
        private void LoadSourceFile(Encoding encoding, string columnDelimitter, char? enclosedChar)
        {
            dtFile2 = new DataTable();
            try
            {
                if (SelectedFile2 != string.Empty)
                {
                    string fileExtension = Path.GetExtension(SelectedFile2);
                    if (fileExtension == ".xls" || fileExtension == ".xlsx" || fileExtension == ".xlsm")
                    {
                        var stopwatch = new Stopwatch();
                        stopwatch.Start();
                        dtFile2 = dataImportHelper.ReadDataFromExcel(SelectedFile2, 1, 0, true, 100);
                        _log.Info(string.Format("{0} seconds to read file", stopwatch.Elapsed.TotalSeconds.ToString()));
                    }
                    else
                    {
                        var stopwatch = new Stopwatch();
                        stopwatch.Start();
                        dtFile2 = dataImportHelper.ReadDataFromSource(SelectedFile2, columnDelimitter, encoding, enclosedChar, true, 100);
                        _log.Info(string.Format("{0} seconds for insert to db", stopwatch.Elapsed.TotalSeconds.ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                LoadDataGrid();
            }
        }

        private void CalculateVennDiagramCounts()
        {
            if (dtFile2 == null || dtFile2.Rows.Count <= 0)
                return;

            if (!enableVennDiagramCalculation)
            {
                IsNextButtonEnabled = true;
                return;
            }

            if (screenMode != DataImportScreenMode.VennDiagram) return;

            if (ddlFile1Key1.SelectedValue == null || ddlFile2Key1.SelectedValue == null)
            {
                return;
            }

            IsForceKeyChange = true;

            CheckKey2Enability();

            IsForceKeyChange = false;

            progress = new ProgressBar(LocalResource.LBL_DATA_IMPORT_TITILE);
            progress.Owner = this;

            // Inserting data with progress bar
            Encoding encoding = Encoding.UTF8;
            if (ddlEncodingChar.SelectedIndex == 0)
                encoding = Encoding.GetEncoding("Shift-JIS");

            string columnDelimitter = ",";

            if (radTab.IsChecked == true)
                columnDelimitter = "\t";
            else if (radSpace.IsChecked == true)
                columnDelimitter = " ";
            else if (radOther.IsChecked == true)
                columnDelimitter = txt_CustomDelimitter.Text;
            ComboBoxItem typeItem = (ComboBoxItem)ddlEnclosedChars.SelectedItem;

            char? enclosedChar;
            if (typeItem.Content.ToString() == ComboBoxSettings.NoneText)
                enclosedChar = null;
            else
                enclosedChar = typeItem.Content.ToString().ToCharArray()[0];

            new Thread(() => CheckTempTableUpdate(encoding, columnDelimitter, enclosedChar)).Start();
            OnWorkerMethodComplete(1, LocalResource.IM_PROGRESSBAR_CACHEING_FILE);
            progress.ShowDialog();
            if (!InsertFileFailed)
            {
                ProgressBarForm progressBarForm = new ProgressBarForm(true, LocalResource.IM_JOINING_DATA);
                progressBarForm.Owner = this;
                Thread thread = new Thread(() => SetVennDiagramCounts(progressBarForm));
                thread.Start();
                System.Threading.Tasks.Task.Run(() =>
                {
                    thread.Join();
                    Dispatcher.Invoke(() =>
                    {
                        HandleNextButtonEnability();
                        SetVennBottomDesc();
                        progressBarForm.Close();
                    });
                });
                progressBarForm.ShowDialog();
            }
            InsertFileFailed = false;
        }

        private void ShowPreviewScreen()
        {
            FileSelectionEnability(false);
            SetDelimitterSettingsVisibility(Visibility.Hidden);
            SetKeySettingsVisibility(Visibility.Hidden);
            SetFilePreview();
            if (dtFile2 != null && dtFile2.Rows.Count > 0)
            {
                SetPreviewScreenVisibility(Visibility.Visible);
                btn_Back.Visibility = Visibility.Visible;
                CheckDelimitterOtherCheckedValidation();
                HandleNextButtonEnability();
                SetVennBottomDesc();
            }
            else
            {
                FileSelectionEnability(true);
                IsNextButtonEnabled = true;
                HandleNextButtonEnability();
            }
        }

        private void SetPreviewScreenVisibility(Visibility visibility)
        {
            lblFile2Preview.Visibility = visibility;
            gridFile2Contents.Visibility = visibility;

            string fileExtension = System.IO.Path.GetExtension(SelectedFile2);
            if (fileExtension != ".xls" && fileExtension != ".xlsx" && fileExtension != ".xlsm")
            {
                lblCharCodeDesc.Visibility = visibility;
                ddlEncodingChar.Visibility = visibility;
                lblDelimitterDesc.Visibility = visibility;
                radGroupDelimitter.Visibility = visibility;
                lblStringQuotesDesc.Visibility = visibility;
                ddlEnclosedChars.Visibility = visibility;
                txt_CustomDelimitter.Visibility = visibility;
                lblDelimitterSpecify.Visibility = visibility;
            }

            btn_Next.Visibility = Visibility.Visible;
            if (visibility == Visibility.Hidden)
                btn_Next.IsEnabled = false;
            else
                btn_Next.IsEnabled = true;
        }

        private void ShowVennDiagramScreen()
        {
            SetPreviewScreenVisibility(Visibility.Hidden);
            SetColumnSelectionVisibility(Visibility.Hidden);
            SetKeySettingsVisibility(Visibility.Visible);
            CalculateVennDiagramCounts();
            HandleNextButtonEnability();
            SetVennBottomDesc();
        }


        private void SetVennBottomDesc()
        {
            if (screenMode == DataImportScreenMode.VennDiagram)
            {
                if (IsDataProcessed)
                {
                    lblDescriptionIsProcessed.Visibility = Visibility.Visible;
                    lblDescriptionIsNotProcessed.Visibility = Visibility.Hidden;
                }
                else
                {
                    lblDescriptionIsProcessed.Visibility = Visibility.Hidden;
                    lblDescriptionIsNotProcessed.Visibility = Visibility.Visible;
                }
            }
            else
            {
                lblDescriptionIsProcessed.Visibility = Visibility.Hidden;
                lblDescriptionIsNotProcessed.Visibility = Visibility.Hidden;

            }

        }

        private void SetKeySettingsVisibility(Visibility visibility)
        {
            lblFile1Key1.Visibility = visibility;
            ddlFile1Key1.Visibility = visibility;
            lblFile1Key2.Visibility = visibility;
            ddlFile1Key2.Visibility = visibility;
            lblFile2Key1.Visibility = visibility;
            ddlFile2Key1.Visibility = visibility;
            lblFile2Key2.Visibility = visibility;
            ddlFile2Key2.Visibility = visibility;
            imgCirle1.Visibility = visibility;
            imgCirle2.Visibility = visibility;

            lblimg1CountTop.Visibility = visibility;
            lblimgMiddleCountTop.Visibility = visibility;
            lblimg2CountTop.Visibility = visibility;
            lblimg1CountBottom.Visibility = visibility;
            lblimgMiddleCountBottom.Visibility = visibility;
            lblimg2CountBottom.Visibility = visibility;
            btn_Next.Visibility = Visibility.Visible;
            if (visibility == Visibility.Hidden)
                btn_Next.IsEnabled = false;
            else
                btn_Next.IsEnabled = true;
        }

        private void ShowColumnSelectionScreen()
        {
            SetKeySettingsVisibility(Visibility.Hidden);
            SetMergingSettingVisibility(Visibility.Hidden);
            SetColumnSelectionVisibility(Visibility.Visible);
            HandleNextButtonEnability();
            SetVennBottomDesc();
        }

        private void BindColumnSelectionGrid(DataTable dt)
        {
            Dispatcher.Invoke(() =>
            {
                try
                {
                    chkSelectAll.IsChecked = false;
                    dataGridHelper.BindDataGrid(ref gridColumnSelection, dt);
                }
                catch (Exception ex)
                {
                    _log.LogError(ex.Message + "\n" + ex.StackTrace);
                    dataGridHelper.BindDataGrid(ref gridColumnSelection, new DataTable());
                }
            });
        }

        private void SetColumnSelectionVisibility(Visibility visibility)
        {
            lblColumnSelectionSpecify.Visibility = visibility;
            lblMaFormatSpecify.Visibility = visibility;
            ddlMaFormat.Visibility = visibility;
            lblNotApplicableChar.Visibility = visibility;
            txt_NotApplicableChar.Visibility = visibility;
            lblFile2PreviewColSelection.Visibility = visibility;
            chkSelectAll.Visibility = visibility;
            lblSelectAll.Visibility = visibility;
            gridColumnSelection.Visibility = visibility;
            btn_Next.Visibility = visibility;
        }
        bool IsShowedMergingScreen = false;
        private void ShowMergingSettingsScreen()
        {
            DataTableHelper dataTableHelper = new DataTableHelper();
            bool isInformationExist = importSettings.ImportInformations.Count > 0;
            IsShowedMergingScreen = true;
            SetColumnSelectionVisibility(Visibility.Hidden);
            SetMergingSettingVisibility(Visibility.Visible);
            importSettings.BeforeProcessingData = dataTableHelper.GetColumnsByIndex(dtFile2JoinedDataBeforeProcessing.Copy(), GetCheckedColumnsIndeces());
            importSettings.AfterProcessingData = dataTableHelper.GetColumnsByIndex(dtFile2JoinedDataAfterProcessing.Copy(), GetCheckedColumnsIndeces());
            DataTable dt = importSettings.BeforeProcessingData;
            if (dt.Rows.Count > 100)
            {
                dt = dt.AsEnumerable().Skip(0).Take(100).CopyToDataTable();
            }

            dataGridHelper.BindDataGrid(ref gridInformation, dt);

            AddAllColumnsToCacheIfNotAvailable();// Add all columns to static variable
            ExecuteActionWhenIdle(SetCursorForHeader);
            ExecuteActionWhenIdle(SetInformationGridBgColor);
            ExecuteActionWhenIdle(PopulateModelDataOnView);            

            int selectedIndex = ddlMaFormat.SelectedIndex;
            if (selectedIndex == 0)
            {
                importSettings.MAformat = MAFormat.FlagFormat;
            }
            else
            {
                importSettings.MAformat = MAFormat.InCellCommaSeperated;
            }

            importSettings.NotApplicableCharacter = txt_NotApplicableChar.Text;

            importSettings.NotApplicable = txt_NotApplicableChar.Text;

            importSettings.DestinationFileKey1 = ddlFile1Key1.SelectedValue.ToString();
            importSettings.DestinationFileKey2 = ddlFile1Key2.SelectedValue.ToString();
            importSettings.SourceFileKey1 = ddlFile2Key1.SelectedValue.ToString();
            importSettings.SourceFileKey2 = ddlFile2Key2.SelectedValue.ToString();
            importSettings.IsDataProcessed = IsDataProcessed;

            if ((!isInformationExist || IsHeaderClicked) && importSettings.ImportInformations != null && importSettings.ImportInformations.Count > 0)
            {
                IsHeaderClicked = false;
                SQLiteConnection con = DBHelper.GetConnection(DBHelper.GetConnectionString(DBPath));
                con.Open();
                importSettings.DataCount = gridInformation.Items.Count;
                ItemNameChanged = false;
                for (int i = 0; i < importSettings.ImportInformations.Count; i++)
                {
                    importSettings.SelectedIndex = i;
                    importSettings.ImportInformations[i].IsDataValidated = IsValidInformation(importSettings, con, true);
                    importSettings.SelectedIndex = 0;
                }
                con.Close(); con.Dispose();
            }

            SetInformationGridBgColor();
            gridInformation.ScrollIntoView(gridInformation.Items.GetItemAt(0), gridInformation.Columns[importSettings.SelectedIndex]);
        }

        private void SetMergingSettingVisibility(Visibility visibility)
        {
            lblImportedInformationSpecify.Visibility = visibility;
            rectFilePreviewBox.Visibility = visibility;
            lblFile2PreviewInfoScreen.Visibility = visibility;
            gridInformation.Visibility = visibility;
            rectQSettingsBox.Visibility = visibility;
            lblQuestionSetting.Visibility = visibility;
            lblItemName.Visibility = visibility;
            txtItemName.Visibility = visibility;
            lblChoiceWording.Visibility = visibility;
            gridChoiceWording.Visibility = visibility;
            lblAnswerType.Visibility = visibility;
            ddlAnswerType.Visibility = visibility;
            lblNoOfChoices.Visibility = visibility;
            txtNoOfChoices.Visibility = visibility;
            lblQuestionTitle.Visibility = visibility;
            txtQuestionTitle.Visibility = visibility;
            lblQuestionSentence.Visibility = visibility;
            txtQuestionSentence.Visibility = visibility;
            btn_PrevData.Visibility = visibility;
            btn_NextData.Visibility = visibility;
            btnExecute.Visibility = visibility;
            CheckAnswerChoiceValidation(visibility);
            if(IsPro)
            {
                btn_read_qlayout.Visibility = visibility;
                Check_utf8.Visibility = visibility;
                if(QC4Common.Common.Constants.GlobalMode != "ja-JP," + Util.Constants.QCFont.MS_Gothic)
                {
                    Check_utf8.IsChecked = true;
                }
            }
        }

        #endregion

        #region Validation
        private void ValidateFile1()
        {
            try
            {
                excelOperate = new ExcelOperate();
                IsFile1Duplicated = false;
                _log.Info("Validate file 1 started");
                file1TempFolderPath = QcFileHelper.GetTempPath() + Path.GetFileNameWithoutExtension(SelectedFile1) + ".qc4";

                FileUtil launchFile = new FileUtil();
                launchFile.OnWorkerComplete += new FileUtil.OnWorkerMethodCompleteDelegate(OnWorkerMethodComplete);
                progress = new ProgressBar();
                progress.Owner = this;

                string tempPath;
                string ext = Path.GetExtension(SelectedFile1);
                if (ext != Qc4Extension)
                {
                    try
                    {
                        #region Check already file exist
                        if (!Directory.Exists(file1TempFolderPath))
                        {
                            _log.Info("Created temp folder" + file1TempFolderPath);
                            Directory.CreateDirectory(file1TempFolderPath);
                        }
                        #endregion

                        tempPath = QcFileHelper.CreateTempFile(file1TempFolderPath);
                        Qc3Parse parse = new Qc3Parse(this, SelectedFile1, tempPath);
                        DataImportFile1WorkBook = parse.StartParsing(ref excelOperate, ShowAlert: false);

                        SelectedFile1 = parse.TargetPath;
                        txt_FileName.Text = parse.TargetPath;
                        tempPath = parse.TempPath;

                        IsFile1Duplicated = false;
                    }
                    catch (Exception e)
                    {
                        _log.LogError(e.Message + "\n" + e.StackTrace);
                        tempDestPath = dataImportHelper.GetTempDestFilePath(SelectedFile1); // make a copy of file in temp and do trans
                        SelectedFile1 = tempDestPath;
                        tempPath = QcFileHelper.CreateTempFile(Path.GetTempPath() + PathName.TempDataImportPath + Path.GetFileNameWithoutExtension(SelectedFile1), PathName.TempDataImportPath);
                        IsFile1Duplicated = true;

                        Qc3Parse parse = new Qc3Parse(this, SelectedFile1, tempPath);
                        DataImportFile1WorkBook = parse.StartParsing(ref excelOperate, PathName.TempDataImportPath, true);

                        SelectedFile1 = parse.TargetPath;
                        txt_FileName.Text = parse.TargetPath;
                        tempPath = parse.TempPath;
                    }

                    file1TempFolderPath = tempPath;
                    if (DataImportFile1WorkBook != null)
                        excelApp = DataImportFile1WorkBook.Application;
                }
                else
                {
                    tempPath = QcFileHelper.ExtractFile(SelectedFile1);
                    if (tempPath == null) // Copying the same file to another temp location for processing
                    {
                        CloseFile1();

                        #region Check already file exist
                        string tNewFileDir = QcFileHelper.GetTempPathDImport() + Path.GetFileNameWithoutExtension(SelectedFile1);
                        if (Directory.Exists(tNewFileDir))
                        {
                            _log.Info(" Temp path already found : " + tNewFileDir);
                            try
                            {
                                Directory.Delete(tNewFileDir, true);
                                _log.Info("Temp path deleted : " + tNewFileDir);
                            }
                            catch (Exception ex)
                            {
                                MessageDialog.ErrorOk(LocalResource.IM_MSG_FILE_ALREADY_OPEN);
                                _log.Info("Temp path deletion failed " + tNewFileDir);
                                _log.LogError(ex.Message + "\n" + ex.StackTrace);
                                CloseFile1();
                                ClearFile1Fields();
                                this.Cursor = Cursors.Arrow;
                                return;
                            }
                        }
                        if (!Directory.Exists(tNewFileDir))
                        {
                            _log.Info("Created temp folder" + tNewFileDir);
                            Directory.CreateDirectory(tNewFileDir);
                        }
                        #endregion

                        #region Qc4 File Open - Different Location
                        QC4FileHelper fh = new QC4FileHelper();
                        fh.OnWorkerComplete += new QC4FileHelper.OnWorkerMethodCompleteDelegate(OnWorkerMethodComplete);
                        progress = new ProgressBar(LocalResource.IM_PROGRESSBAR_OPENING_FILE);
                        progress.Owner = this;
                        new Thread(() => fh.OpenFileDI(SelectedFile1, ref excelApp, ref DataImportFile1WorkBook, ref tempPath, ref excelOperate, PathName.TempDataImportPath)).Start();
                        progress.ShowDialog();

                        file1TempFolderPath = tempPath;
                        if (tempPath == null || !QC4FileHelper.IsSuccess)
                        {
                            CloseFile1();
                            ClearFile1Fields();
                            QC4FileHelper.IsSuccess = true;
                        }
                        else
                        {
                            IsFile1Duplicated = true;
                        }
                        #endregion
                    }
                    else
                    {
                        QC4FileHelper fh = new QC4FileHelper();
                        fh.OnWorkerComplete += new QC4FileHelper.OnWorkerMethodCompleteDelegate(OnWorkerMethodComplete);
                        progress = new ProgressBar(LocalResource.IM_PROGRESSBAR_OPENING_FILE);
                        progress.Owner = this;
                        new Thread(() => fh.OpenFileDI(SelectedFile1, ref excelApp, ref DataImportFile1WorkBook, ref tempPath, ref excelOperate)).Start();
                        progress.ShowDialog();
                        if (tempPath == null || !QC4FileHelper.IsSuccess)
                        {
                            CloseFile1();
                            ClearFile1Fields();
                            QC4FileHelper.IsSuccess = true;
                        }
                    }
                }
                if (DataImportFile1WorkBook == null)
                {
                    CloseFile1();
                    ClearFile1Fields();
                    this.Cursor = Cursors.Arrow;
                    return;
                }
                else
                {
                    CheckDestFileDataCount();
                }

                this.Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
                CloseFile1();
                ClearFile1Fields();
                MessageDialog.ErrorOk("Error : " + ex.Message);
            }
            finally
            {
                IsDataProcessed = UpdateDataProcessedStatus();               
            }
        }

        private void CheckDestFileDataCount()
        {
            try
            {
                SQLiteConnection con = DBHelper.GetConnection(DBHelper.GetConnectionString(DBPath));
                con.Open();

                if (dataImportHelper.GetAnswerCount(con) == 0 && dataImportHelper.GetAfterProcessCount(con) == 0)
                {
                    MessageDialog.ErrorOk(LocalResource.IM_MSG_NO_DATA);
                    con.Close(); con.Dispose();
                    CloseFile1();
                    ClearFile1Fields();
                }
                else
                {
                    con.Close(); con.Dispose();
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
            }
            finally
            {
                CheckNextButtonIsValid();
            }
        }

        private void ClearFile1Fields()
        {
            file1TempFolderPath = null;
            SelectedFile1 = string.Empty;
            txt_FileName.Text = string.Empty;
        }

        private void ClearFile2Fields()
        {
            SelectedFile2 = string.Empty;
            txt_FileName2.Text = string.Empty;
        }

        private void CheckDelimitterOtherCheckedValidation()
        {
            if (screenMode != DataImportScreenMode.FileSelection)
            {
                if (radOther.IsChecked == true)
                {
                    txt_CustomDelimitter.IsEnabled = true;
                }
                else
                {
                    txt_CustomDelimitter.Text = string.Empty;
                    txt_CustomDelimitter.IsEnabled = false;
                }
            }
        }

        private void CheckNextButtonIsValid()
        {
            switch (screenMode)
            {
                case DataImportScreenMode.FileSelection:
                    if (SelectedFile1 != string.Empty && SelectedFile2 != string.Empty)
                    {
                        IsNextButtonEnabled = true;
                    }
                    break;

                case DataImportScreenMode.DelimitterSelection:
                    if (SelectedFile1 != string.Empty && SelectedFile2 != string.Empty)
                    {
                        IsNextButtonEnabled = true;
                    }
                    break;
            }
            HandleNextButtonEnability();
        }

        private void ValidateCheckedColumns()
        {
            IsNextButtonEnabled = false;

            GetVisualChildCollection<DataGridColumnHeader>(gridColumnSelection, "uncheck");

            if (DataGridHelper.ischecked)
            {
                DataGridHelper.ischecked = false;
                IsNextButtonEnabled = true;
            }

            HandleNextButtonEnability();
        }

        #endregion


        private void CloseFile1()
        {
            try
            {
                if (!IsMainWindowFile)
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    try
                    {
                        if (excelApp != null)
                        {
                            excelApp.DisplayAlerts = false;
                            excelApp.EnableEvents = false;
                            DataImportFile1WorkBook.Close(false);
                            Marshal.ReleaseComObject(DataImportFile1WorkBook);
                            excelApp.Quit();
                            Marshal.ReleaseComObject(excelApp);
                            excelApp = null;
                            if (excelOperate != null)
                                excelOperate.Dispose();
                        }
                    }
                    catch
                    {

                    }

                    DataImportFile1WorkBook = null;
                    if (Directory.Exists(file1TempFolderPath))
                        Directory.Delete(file1TempFolderPath, true);
                }
                else
                {
                    DataImportFile1WorkBook = null;
                    excelApp = null;
                }

                if (File.Exists(tempDestPath))
                    File.Delete(tempDestPath);
                tempDestPath = null;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.Message);
                DataImportFile1WorkBook = null;
            }
        }

        private bool UpdateDataProcessedStatus() // If no error, data is procesed
        {
            bool isProcessed = false;
            try
            {
                using (SQLiteConnection con = DBHelper.GetConnection(DBHelper.GetConnectionString(DBPath)))
                {
                    con.Open();
                    if (DBHelper.TableExist("SELECT count(name) FROM sqlite_master WHERE type = 'table' AND name = 'data_after_process';", con) > 0)
                    {
                        DataTable dataTble = DBHelper.GetDataTable("Select * from data_after_process Limit 1 ", con);
                        isProcessed = true;
                    }
                };
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.Message);
                isProcessed = false;
            }
            return isProcessed;
        }
        bool InsertFileFailed = false;
        private void CheckTempTableUpdate(Encoding encoding, string columnDelimitter, char? enclosedChar)
        {
            if (ReInsertToTempData)
            {
                string fileExtension = Path.GetExtension(SelectedFile2);
                if (fileExtension == ".xls" || fileExtension == ".xlsx" || fileExtension == ".xlsm")
                {
                    if (!dataImportHelper.InsertExcelContentsToTempTable(DBPath, SelectedFile2))
                    {
                        screenMode = DataImportScreenMode.FileSelection;
                        ExecuteActionWhenIdle(SetPreviousMode);
                        InsertFileFailed = true;
                        _log.LogError("Failed to insert data to temp table");
                        MessageDialog.ErrorOk(LocalResource.IM_MSG_FILE2_LOAD_FAILED);
                    }
                }
                else
                {
                    if (!dataImportHelper.InsertSourceFileContentsToTempTable(DBPath, SelectedFile2, columnDelimitter, encoding, enclosedChar))
                    {
                        _log.LogError("Failed to insert data to temp table");
                        MessageDialog.ErrorOk(LocalResource.IM_MSG_FILE2_LOAD_FAILED);
                    }

                }
                ReInsertToTempData = InsertFileFailed;
            }
            OnWorkerMethodComplete(100, LocalResource.IM_PROGRESSBAR_READING_COMPLETED);
        }

        private void SetPreviousMode()
        {
            screenMode = DataImportScreenMode.FileSelection;
            SetDelimitterSettingsVisibility(Visibility.Hidden);
            SetKeySettingsVisibility(Visibility.Hidden);

            SetPreviewScreenVisibility(Visibility.Hidden);
            btn_Back.Visibility = Visibility.Hidden;
            CheckDelimitterOtherCheckedValidation();
            SetVennBottomDesc();
            FileSelectionEnability(true);
            IsNextButtonEnabled = true;

        }

        private void SetVennDiagramCounts(ProgressBarForm pb)
        {
            Dispatcher.Invoke(() =>
            {
                SetDefaultMAFormat(); // MA data need to reset if recalculate
            });

            IsNextButtonEnabled = false;
            dtFile2JoinedDataBeforeProcessing = new DataTable();
            dtFile2JoinedDataAfterProcessing = new DataTable();
            DataImportHelper dataImportHelper = new DataImportHelper();
            using (SQLiteConnection con = DBHelper.GetConnection(DBHelper.GetConnectionString(DBPath)))
            {
                con.Open();

                string file1Key1 = ComboBoxSettings.NoneText;
                string file2Key1 = ComboBoxSettings.NoneText;

                Dispatcher.Invoke(() =>
                {
                    file1Key1 = ddlFile1Key1.SelectedValue.ToString();
                    file2Key1 = ddlFile2Key1.SelectedValue.ToString();
                });

                string file1Key2 = ComboBoxSettings.NoneText;
                string file2Key2 = ComboBoxSettings.NoneText;

                Dispatcher.Invoke(() =>
                {
                    if ((ddlFile1Key2.SelectedValue != null) && (ddlFile2Key2.SelectedValue != null))
                    {
                        file1Key2 = ddlFile1Key2.SelectedValue.ToString();
                        file2Key2 = ddlFile2Key2.SelectedValue.ToString();
                    }
                });

                int countFile1BeforeProcessing = 0;
                int countFile2BeforeProcessing = 0;
                int countTogetherBeforeProcessing = 0;

                int countFile1AfterProcessing = 0;
                int countFile2AfterProcessing = 0;
                int countTogetherAfterProcessing = 0;

                bool isKey1Exist = true;
                if (file1Key1 == ComboBoxSettings.NoneText || file2Key1 == ComboBoxSettings.NoneText)
                {
                    isKey1Exist = false;
                }

                bool isKey2Exist = true;
                if (file1Key2 == ComboBoxSettings.NoneText || file2Key2 == ComboBoxSettings.NoneText)
                {
                    isKey2Exist = false;
                }

                if (isKey1Exist && isKey2Exist)
                {
                    string qry = "select sort_no,`" + file1Key1 + "`,`" + file1Key2 + "` from answers;";
                    DataTable answerData = DBHelper.GetDataTable(qry, con);
                    qry = "select ROWID,`" + file2Key1 + "`,`" + file2Key2 + "` from " + DataImportSettings.DataImportSourceTempTable + ";";
                    DataTable tempData = DBHelper.GetDataTable(qry, con);
                    int ansCount = answerData.Rows.Count;
                    int tmpCount = tempData.Rows.Count;

                    string sql = "SELECT COUNT(*) AS CNTREC FROM pragma_table_info('" + DataImportSettings.DataImportSourceTempTable + "') WHERE name='temprysfiltered'";
                    if (DBHelper.ExecuteScalar(sql, con) > 0)
                    {
                        sql = "update " + DataImportSettings.DataImportSourceTempTable + " set temprysfiltered='';";
                        DBHelper.ExecuteQuery(sql, con);
                    }
                    else
                    {
                        sql = "alter table " + DataImportSettings.DataImportSourceTempTable + " add temprysfiltered TEXT;";
                        DBHelper.ExecuteQuery(sql, con);
                    }
                    sql = "SELECT COUNT(*) AS CNTREC FROM pragma_table_info('" + DataImportSettings.DataImportSourceTempTable + "') WHERE name='temprysfiltered1'";
                    if (DBHelper.ExecuteScalar(sql, con) > 0)
                    {
                        sql = "update " + DataImportSettings.DataImportSourceTempTable + " set temprysfiltered1 = '';";
                        DBHelper.ExecuteQuery(sql, con);
                    }
                    else
                    {
                        sql = "alter table " + DataImportSettings.DataImportSourceTempTable + " add temprysfiltered1 TEXT;";
                        DBHelper.ExecuteQuery(sql, con);
                    }

                    sql = "SELECT COUNT(*) AS CNTREC FROM pragma_table_info('answers') WHERE name='temprysfiltered'";
                    if (DBHelper.ExecuteScalar(sql, con) > 0)
                    {
                        sql = "update answers set temprysfiltered='';";
                        DBHelper.ExecuteQuery(sql, con);
                    }
                    else
                    {
                        sql = "alter table answers add temprysfiltered TEXT;";
                        DBHelper.ExecuteQuery(sql, con);
                    }
                    sql = "SELECT COUNT(*) AS CNTREC FROM pragma_table_info('answers') WHERE name='temprysfiltered1'";
                    if (DBHelper.ExecuteScalar(sql, con) > 0)
                    {
                        sql = "update answers set temprysfiltered1 ='';";
                        DBHelper.ExecuteQuery(sql, con);
                    }
                    else
                    {
                        sql = "alter table answers add temprysfiltered1 TEXT;";
                        DBHelper.ExecuteQuery(sql, con);
                    }

                    StringBuilder updtQry = new StringBuilder("update answers set temprysfiltered = CASE sort_no ");
                    StringBuilder updtId = new StringBuilder(" END WHERE sort_no IN (");
                    for (int i = 0; i < ansCount; i++)
                    {
                        bool isValidData = !string.IsNullOrWhiteSpace(Convert.ToString(answerData.Rows[i][1])) && answerData.Rows[i][1].ToString().ToLower().Contains("e") && Regex.IsMatch(answerData.Rows[i][1].ToString(), @"^[-+]?[0-9]*\.?[0-9]+([eE][-+]?[0-9]+)$") ? true : false;
                        if (!isValidData)
                        {
                            updtQry.Append(" when " + answerData.Rows[i][0].ToString() + " then '" + answerData.Rows[i][1].ToString().Replace("'", "''") + "'");
                            updtId.Append(answerData.Rows[i][0].ToString() + ",");
                            if ((updtQry.Length + updtId.Length) > 900000)
                            {
                                sql = updtQry.ToString() + updtId.ToString();
                                sql = sql.Remove(sql.Length - 1, 1);
                                sql = sql + ");";
                                DBHelper.ExecuteQuery(sql, con);
                                updtQry = new StringBuilder("update answers set temprysfiltered = CASE sort_no ");
                                updtId = new StringBuilder(" END WHERE sort_no IN (");
                            }
                        }
                        else
                        {
                            answerData.Rows[i][1] = ExtractExponential(answerData.Rows[i][1].ToString());
                            updtQry.Append(" when " + answerData.Rows[i][0].ToString() + " then '" + answerData.Rows[i][1].ToString() + "'");
                            updtId.Append(answerData.Rows[i][0].ToString() + ",");
                            if ((updtQry.Length + updtId.Length) > 900000)
                            {
                                sql = updtQry.ToString() + updtId.ToString();
                                sql = sql.Remove(sql.Length - 1, 1);
                                sql = sql + ");";
                                DBHelper.ExecuteQuery(sql, con);
                                updtQry = new StringBuilder("update answers set temprysfiltered = CASE sort_no ");
                                updtId = new StringBuilder(" END WHERE sort_no IN (");
                            }
                        }
                    }

                    sql = updtQry.ToString() + updtId.ToString();
                    if (sql[sql.Length - 1] == ',')
                    {
                        sql = sql.Remove(sql.Length - 1, 1);
                        sql = sql + ");";
                        DBHelper.ExecuteQuery(sql, con);
                    }
                    updtQry = new StringBuilder("update answers set temprysfiltered1 = CASE sort_no ");
                    updtId = new StringBuilder(" END WHERE sort_no IN (");
                    for (int i = 0; i < ansCount; i++)
                    {
                        bool isValidData = !string.IsNullOrWhiteSpace(Convert.ToString(answerData.Rows[i][2])) && answerData.Rows[i][2].ToString().ToLower().Contains("e") && Regex.IsMatch(answerData.Rows[i][2].ToString(), @"^[-+]?[0-9]*\.?[0-9]+([eE][-+]?[0-9]+)$") ? true : false;
                        if (!isValidData)
                        {
                            updtQry.Append(" when " + answerData.Rows[i][0].ToString() + " then '" + answerData.Rows[i][2].ToString().Replace("'", "''") + "'");
                            updtId.Append(answerData.Rows[i][0].ToString() + ",");
                            if ((updtQry.Length + updtId.Length) > 900000)
                            {
                                sql = updtQry.ToString() + updtId.ToString();
                                sql = sql.Remove(sql.Length - 1, 1);
                                sql = sql + ");";
                                DBHelper.ExecuteQuery(sql, con);
                                updtQry = new StringBuilder("update answers set temprysfiltered1 = CASE sort_no ");
                                updtId = new StringBuilder(" END WHERE sort_no IN (");
                            }
                        }
                        else
                        {
                            answerData.Rows[i][2] = ExtractExponential(answerData.Rows[i][2].ToString());
                            updtQry.Append(" when " + answerData.Rows[i][0].ToString() + " then '" + answerData.Rows[i][2].ToString() + "'");
                            updtId.Append(answerData.Rows[i][0].ToString() + ",");
                            if ((updtQry.Length + updtId.Length) > 900000)
                            {
                                sql = updtQry.ToString() + updtId.ToString();
                                sql = sql.Remove(sql.Length - 1, 1);
                                sql = sql + ");";
                                DBHelper.ExecuteQuery(sql, con);
                                updtQry = new StringBuilder("update answers set temprysfiltered1 = CASE sort_no ");
                                updtId = new StringBuilder(" END WHERE sort_no IN (");
                            }
                        }
                    }
                    sql = updtQry.ToString() + updtId.ToString();
                    if (sql[sql.Length - 1] == ',')
                    {
                        sql = sql.Remove(sql.Length - 1, 1);
                        sql = sql + ");";
                        DBHelper.ExecuteQuery(sql, con);
                    }

                    updtQry = new StringBuilder("update " + DataImportSettings.DataImportSourceTempTable + " set temprysfiltered = CASE ROWID ");
                    updtId = new StringBuilder(" END WHERE ROWID IN (");
                    for (int i = 0; i < tmpCount; i++)
                    {
                        bool isValidData = !string.IsNullOrWhiteSpace(Convert.ToString(tempData.Rows[i][1])) && tempData.Rows[i][1].ToString().ToLower().Contains("e") && Regex.IsMatch(tempData.Rows[i][1].ToString(), @"^[-+]?[0-9]*\.?[0-9]+([eE][-+]?[0-9]+)$") ? true : false;
                        if (!isValidData)
                        {
                            updtQry.Append(" when " + tempData.Rows[i][0].ToString() + " then '" + tempData.Rows[i][1].ToString().Replace("'", "''") + "'");
                            updtId.Append(tempData.Rows[i][0].ToString() + ",");
                            if ((updtQry.Length + updtId.Length) > 900000)
                            {
                                sql = updtQry.ToString() + updtId.ToString();
                                sql = sql.Remove(sql.Length - 1, 1);
                                sql = sql + ");";
                                DBHelper.ExecuteQuery(sql, con);
                                updtQry = new StringBuilder("update " + DataImportSettings.DataImportSourceTempTable + " set temprysfiltered = CASE ROWID ");
                                updtId = new StringBuilder(" END WHERE ROWID IN (");
                            }
                        }
                        else
                        {
                            tempData.Rows[i][1] = ExtractExponential(tempData.Rows[i][1].ToString());
                            updtQry.Append(" when " + tempData.Rows[i][0].ToString() + " then '" + tempData.Rows[i][1].ToString() + "'");
                            updtId.Append(tempData.Rows[i][0].ToString() + ",");
                            if ((updtQry.Length + updtId.Length) > 900000)
                            {
                                sql = updtQry.ToString() + updtId.ToString();
                                sql = sql.Remove(sql.Length - 1, 1);
                                sql = sql + ");";
                                DBHelper.ExecuteQuery(sql, con);
                                updtQry = new StringBuilder("update " + DataImportSettings.DataImportSourceTempTable + " set temprysfiltered = CASE ROWID ");
                                updtId = new StringBuilder(" END WHERE ROWID IN (");
                            }
                        }
                    }
                    sql = updtQry.ToString() + updtId.ToString();
                    if (sql[sql.Length - 1] == ',')
                    {
                        sql = sql.Remove(sql.Length - 1, 1);
                        sql = sql + ");";
                        DBHelper.ExecuteQuery(sql, con);
                    }
                    updtQry = new StringBuilder("update " + DataImportSettings.DataImportSourceTempTable + " set temprysfiltered1 = CASE ROWID ");
                    updtId = new StringBuilder(" END WHERE ROWID IN (");
                    for (int i = 0; i < tmpCount; i++)
                    {
                        bool isValidData = !string.IsNullOrWhiteSpace(Convert.ToString(tempData.Rows[i][2])) && tempData.Rows[i][2].ToString().ToLower().Contains("e") && Regex.IsMatch(tempData.Rows[i][2].ToString(), @"^[-+]?[0-9]*\.?[0-9]+([eE][-+]?[0-9]+)$") ? true : false;
                        if (!isValidData)
                        {
                            updtQry.Append(" when " + tempData.Rows[i][0].ToString() + " then '" + tempData.Rows[i][2].ToString().Replace("'", "''") + "'");
                            updtId.Append(tempData.Rows[i][0].ToString() + ",");
                            if ((updtQry.Length + updtId.Length) > 900000)
                            {
                                sql = updtQry.ToString() + updtId.ToString();
                                sql = sql.Remove(sql.Length - 1, 1);
                                sql = sql + ");";
                                DBHelper.ExecuteQuery(sql, con);
                                updtQry = new StringBuilder("update " + DataImportSettings.DataImportSourceTempTable + " set temprysfiltered1 = CASE ROWID ");
                                updtId = new StringBuilder(" END WHERE ROWID IN (");
                            }
                        }
                        else
                        {
                            tempData.Rows[i][2] = ExtractExponential(tempData.Rows[i][2].ToString());
                            updtQry.Append(" when " + tempData.Rows[i][0].ToString() + " then '" + tempData.Rows[i][2].ToString() + "'");
                            updtId.Append(tempData.Rows[i][0].ToString() + ",");
                            if ((updtQry.Length + updtId.Length) > 900000)
                            {
                                sql = updtQry.ToString() + updtId.ToString();
                                sql = sql.Remove(sql.Length - 1, 1);
                                sql = sql + ");";
                                DBHelper.ExecuteQuery(sql, con);
                                updtQry = new StringBuilder("update " + DataImportSettings.DataImportSourceTempTable + " set temprysfiltered1 = CASE ROWID ");
                                updtId = new StringBuilder(" END WHERE ROWID IN (");
                            }
                        }
                    }

                    sql = updtQry.ToString() + updtId.ToString();
                    if (sql[sql.Length - 1] == ',')
                    {
                        sql = sql.Remove(sql.Length - 1, 1);
                        sql = sql + ");";
                        DBHelper.ExecuteQuery(sql, con);
                    }
                }
                else if (isKey1Exist)
                {
                    string qry = "select sort_no,`" + file1Key1 + "` from answers;";
                    DataTable answerData = DBHelper.GetDataTable(qry, con);
                    qry = "select ROWID,`" + file2Key1 + "` from " + DataImportSettings.DataImportSourceTempTable + ";";
                    DataTable tempData = DBHelper.GetDataTable(qry, con);
                    int ansCount = answerData.Rows.Count;
                    int tmpCount = tempData.Rows.Count;

                    string sql = "SELECT COUNT(*) AS CNTREC FROM pragma_table_info('" + DataImportSettings.DataImportSourceTempTable + "') WHERE name='temprysfiltered'";
                    if (DBHelper.ExecuteScalar(sql, con) > 0)
                    {
                        sql = "update " + DataImportSettings.DataImportSourceTempTable + " set temprysfiltered ='';";
                        DBHelper.ExecuteQuery(sql, con);
                    }
                    else
                    {
                        sql = "alter table " + DataImportSettings.DataImportSourceTempTable + " add temprysfiltered TEXT;";
                        DBHelper.ExecuteQuery(sql, con);
                    }
                    sql = "SELECT COUNT(*) AS CNTREC FROM pragma_table_info('answers') WHERE name='temprysfiltered'";
                    if (DBHelper.ExecuteScalar(sql, con) > 0)
                    {
                        sql = "update answers set temprysfiltered ='';";
                        DBHelper.ExecuteQuery(sql, con);
                    }
                    else
                    {
                        sql = "alter table answers add temprysfiltered TEXT;";
                        DBHelper.ExecuteQuery(sql, con);
                    }

                    StringBuilder updtQry = new StringBuilder("update answers set temprysfiltered = CASE sort_no ");
                    StringBuilder updtId = new StringBuilder(" END WHERE sort_no IN (");
                    for (int i = 0; i < ansCount; i++)
                    {
                        bool isValidData = !string.IsNullOrWhiteSpace(Convert.ToString(answerData.Rows[i][1])) && answerData.Rows[i][1].ToString().ToLower().Contains("e") && Regex.IsMatch(answerData.Rows[i][1].ToString(), @"^[-+]?[0-9]*\.?[0-9]+([eE][-+]?[0-9]+)$") ? true : false;
                        if (!isValidData)
                        {
                            updtQry.Append(" when " + answerData.Rows[i][0].ToString() + " then '" + answerData.Rows[i][1].ToString().Replace("'", "''") + "'");
                            updtId.Append(answerData.Rows[i][0].ToString() + ",");
                            if ((updtQry.Length + updtId.Length) > 900000)
                            {
                                sql = updtQry.ToString() + updtId.ToString();
                                sql = sql.Remove(sql.Length - 1, 1);
                                sql = sql + ");";
                                DBHelper.ExecuteQuery(sql, con);
                                updtQry = new StringBuilder("update answers set temprysfiltered = CASE sort_no ");
                                updtId = new StringBuilder(" END WHERE sort_no IN (");
                            }
                        }
                        else
                        {
                            answerData.Rows[i][1] = ExtractExponential(answerData.Rows[i][1].ToString());
                            updtQry.Append(" when " + answerData.Rows[i][0].ToString() + " then '" + answerData.Rows[i][1].ToString() + "'");
                            updtId.Append(answerData.Rows[i][0].ToString() + ",");
                            if ((updtQry.Length + updtId.Length) > 900000)
                            {
                                sql = updtQry.ToString() + updtId.ToString();
                                sql = sql.Remove(sql.Length - 1, 1);
                                sql = sql + ");";
                                DBHelper.ExecuteQuery(sql, con);
                                updtQry = new StringBuilder("update answers set temprysfiltered = CASE sort_no ");
                                updtId = new StringBuilder(" END WHERE sort_no IN (");
                            }
                        }
                    }

                    sql = updtQry.ToString() + updtId.ToString();
                    if (sql[sql.Length - 1] == ',')
                    {
                        sql = sql.Remove(sql.Length - 1, 1);
                        sql = sql + ");";
                        DBHelper.ExecuteQuery(sql, con);
                    }

                    updtQry = new StringBuilder("update " + DataImportSettings.DataImportSourceTempTable + " set temprysfiltered = CASE ROWID ");
                    updtId = new StringBuilder(" END WHERE ROWID IN (");
                    for (int i = 0; i < tmpCount; i++)
                    {
                        bool isValidData = !string.IsNullOrWhiteSpace(Convert.ToString(tempData.Rows[i][1])) && tempData.Rows[i][1].ToString().ToLower().Contains("e") && Regex.IsMatch(tempData.Rows[i][1].ToString(), @"^[-+]?[0-9]*\.?[0-9]+([eE][-+]?[0-9]+)$") ? true : false;
                        if (!isValidData)
                        {
                            updtQry.Append(" when " + tempData.Rows[i][0].ToString() + " then '" + tempData.Rows[i][1].ToString().Replace("'", "''") + "'");
                            updtId.Append(tempData.Rows[i][0].ToString() + ",");
                            if ((updtQry.Length + updtId.Length) > 900000)
                            {
                                sql = updtQry.ToString() + updtId.ToString();
                                sql = sql.Remove(sql.Length - 1, 1);
                                sql = sql + ");";
                                DBHelper.ExecuteQuery(sql, con);
                                updtQry = new StringBuilder("update " + DataImportSettings.DataImportSourceTempTable + " set temprysfiltered = CASE ROWID ");
                                updtId = new StringBuilder(" END WHERE ROWID IN (");
                            }
                        }
                        else
                        {
                            tempData.Rows[i][1] = ExtractExponential(tempData.Rows[i][1].ToString());
                            updtQry.Append(" when " + tempData.Rows[i][0].ToString() + " then '" + tempData.Rows[i][1].ToString() + "'");
                            updtId.Append(tempData.Rows[i][0].ToString() + ",");
                            if ((updtQry.Length + updtId.Length) > 900000)
                            {
                                sql = updtQry.ToString() + updtId.ToString();
                                sql = sql.Remove(sql.Length - 1, 1);
                                sql = sql + ");";
                                DBHelper.ExecuteQuery(sql, con);
                                updtQry = new StringBuilder("update " + DataImportSettings.DataImportSourceTempTable + " set temprysfiltered = CASE ROWID ");
                                updtId = new StringBuilder(" END WHERE ROWID IN (");
                            }
                        }
                    }

                    sql = updtQry.ToString() + updtId.ToString();
                    if (sql[sql.Length - 1] == ',')
                    {
                        sql = sql.Remove(sql.Length - 1, 1);
                        sql = sql + ");";
                        DBHelper.ExecuteQuery(sql, con);
                    }
                }

                DuplicateCheck(con, isKey1Exist, isKey2Exist);

                if (!isKey1Exist && !isKey2Exist)
                {
                    countFile1BeforeProcessing = dataImportHelper.GetAnswerCount(con);
                    countFile2BeforeProcessing = dataImportHelper.GetSourceFileCount(con);

                    if (countFile1BeforeProcessing < countFile2BeforeProcessing)
                    {
                        countTogetherBeforeProcessing = countFile1BeforeProcessing;
                        if (countTogetherBeforeProcessing < 100)
                        {
                            if (countTogetherBeforeProcessing == 0)
                                dtFile2JoinedDataBeforeProcessing = dtFile2.Clone();
                            else
                                dtFile2JoinedDataBeforeProcessing = dtFile2.Copy().AsEnumerable().Skip(0).Take(countTogetherBeforeProcessing).CopyToDataTable();
                        }
                        else
                        {
                            dtFile2JoinedDataBeforeProcessing = dtFile2.Copy();
                        }

                    }
                    else
                    {
                        dtFile2JoinedDataBeforeProcessing = dtFile2.Copy();
                        countTogetherBeforeProcessing = countFile2BeforeProcessing;
                    }

                    countFile1BeforeProcessing = countFile1BeforeProcessing - countTogetherBeforeProcessing;
                    countFile2BeforeProcessing = countFile2BeforeProcessing - countTogetherBeforeProcessing;

                    if (IsDataProcessed)//****************************** after processing
                    {
                        dtFile2JoinedDataAfterProcessing = dtFile2;

                        countFile1AfterProcessing = dataImportHelper.GetAfterProcessCount(con);
                        countFile2AfterProcessing = dataImportHelper.GetSourceFileCount(con);

                        if (countFile1AfterProcessing < countFile2AfterProcessing)
                        {
                            countTogetherAfterProcessing = countFile1AfterProcessing;
                            if (countTogetherBeforeProcessing < 100)
                            {
                                if (countTogetherBeforeProcessing == 0)
                                    dtFile2JoinedDataAfterProcessing = dtFile2.Copy().AsEnumerable().Skip(0).Take(countTogetherAfterProcessing).CopyToDataTable();
                                else
                                    dtFile2JoinedDataAfterProcessing = dtFile2.Clone();
                            }
                            else
                                dtFile2JoinedDataAfterProcessing = dtFile2.Copy();
                        }
                        else //both rows are same or file 1 count is greater than file 2
                        {
                            countTogetherAfterProcessing = countFile2AfterProcessing;
                        }

                        countFile1AfterProcessing = countFile1AfterProcessing - countTogetherAfterProcessing;
                        countFile2AfterProcessing = countFile2AfterProcessing - countTogetherAfterProcessing;

                    }

                    Dispatcher.Invoke(() =>
                    {
                        if (IsDataProcessed)
                        {
                            lblimg1CountTop.Text = countFile1AfterProcessing.ToString();
                            lblimg2CountTop.Text = countFile2AfterProcessing.ToString();
                            lblimgMiddleCountTop.Text = countTogetherAfterProcessing.ToString();

                            lblimg1CountBottom.Text = "(" + countFile1BeforeProcessing.ToString() + ")";
                            lblimg2CountBottom.Text = "(" + countFile2BeforeProcessing.ToString() + ")";
                            lblimgMiddleCountBottom.Text = "(" + countTogetherBeforeProcessing.ToString() + ")";

                        }
                        else
                        {
                            lblimg1CountTop.Text = string.Empty;
                            lblimg2CountTop.Text = string.Empty;
                            lblimgMiddleCountTop.Text = string.Empty;

                            lblimg1CountBottom.Text = countFile1BeforeProcessing.ToString();
                            lblimg2CountBottom.Text = countFile2BeforeProcessing.ToString();
                            lblimgMiddleCountBottom.Text = countTogetherBeforeProcessing.ToString();
                        }
                        this.Activate();
                    });

                    if (countTogetherBeforeProcessing > 0)
                    {
                        enableVennDiagramCalculation = false;
                        IsNextButtonEnabled = true;
                    }

                    BindColumnSelectionGrid(dtFile2JoinedDataBeforeProcessing.Copy());
                    return;
                }

                DataTableHelper dataTableHelper = new DataTableHelper();

                dtFile2JoinedDataBeforeProcessing = dataImportHelper.GetJoinedSourceFileDataBProcess(con, file1Key1, file2Key1, isKey2Exist, file1Key2, file2Key2, TempColumns);

                VennDiagramCounts vennDiagramCounts = dataImportHelper.GetVennDiagramCounts(con, file1Key1, file2Key1, isKey2Exist, file1Key2, file2Key2);

                countFile1AfterProcessing = vennDiagramCounts.DestUnJoinedAP;
                countFile2AfterProcessing = vennDiagramCounts.SourceUnJoinedAP;
                countTogetherAfterProcessing = vennDiagramCounts.JoinedAP;
                countFile1BeforeProcessing = vennDiagramCounts.DestUnJoinedBP;
                countFile2BeforeProcessing = vennDiagramCounts.SourceUnJoinedBP;
                countTogetherBeforeProcessing = vennDiagramCounts.JoinedBP;

                if (IsDataProcessed)
                {
                    Dispatcher.Invoke(() =>
                    {
                        lblimg1CountTop.Text = countFile1AfterProcessing.ToString();
                        lblimg2CountTop.Text = countFile2AfterProcessing.ToString();
                        lblimgMiddleCountTop.Text = countTogetherAfterProcessing.ToString();

                        lblimg1CountBottom.Text = "(" + countFile1BeforeProcessing.ToString() + ")";
                        lblimg2CountBottom.Text = "(" + countFile2BeforeProcessing.ToString() + ")";
                        lblimgMiddleCountBottom.Text = "(" + countTogetherBeforeProcessing.ToString() + ")";

                    });
                }
                else
                {
                    Dispatcher.Invoke(() =>
                    {
                        lblimg1CountTop.Text = string.Empty;
                        lblimg2CountTop.Text = string.Empty;
                        lblimgMiddleCountTop.Text = string.Empty;


                        lblimg1CountBottom.Text = countFile1BeforeProcessing.ToString();
                        lblimg2CountBottom.Text = countFile2BeforeProcessing.ToString();
                        lblimgMiddleCountBottom.Text = countTogetherBeforeProcessing.ToString();
                    });
                }

                Dispatcher.Invoke(() =>
                {
                    BindColumnSelectionGrid(dtFile2JoinedDataBeforeProcessing.Copy());
                });

                if (countTogetherBeforeProcessing > 0 || countTogetherAfterProcessing > 0)
                {
                    enableVennDiagramCalculation = false;
                    IsNextButtonEnabled = true;
                }
            }
        }

        private void DuplicateCheck(SQLiteConnection con, bool isKey1Exist, bool isKey2Exist)
        {
            if (isKey1Exist)
            {
                string sSql = " Select count(A.sort_no) ";
                sSql += " from `Answers` A, ";
                sSql += "`" + DataImportSettings.DataImportSourceTempTable + "` D ";
                sSql += " where A.`temprysfiltered` = D.`temprysfiltered`  ";
                sSql += " and A.`temprysfiltered` != '' and A.`temprysfiltered` is not null ";
                if (isKey2Exist)
                {
                    sSql += " And ";

                    sSql += " A.`temprysfiltered1` =  D.`temprysfiltered1`  ";
                    sSql += " and A.`temprysfiltered1` != '' and A.`temprysfiltered1` is not null ";
                }
                sSql += " GROUP by   A.sort_no HAVING COUNT(*) > 1;";
                int dup = DBHelper.ExecuteScalar(sSql, con);
                if (dup > 1)
                    IsDuplicated = true;
                else
                    IsDuplicated = false;
            }
            else
            {
                IsDuplicated = false;
            }
        }

        private string ExtractExponential(string number)
        {
            string ouput = "";
            number = number.ToLower();
            if (number.Contains("e-"))
            {
                bool isHaveMinuz = number[0] == '-';
                if (number.Contains("-"))
                    number = number.Replace("-", "");
                string[] str = number.Split('e');
                int count = Convert.ToInt32(str[1]);
                ouput = str[0];
                for (int j = 0; j < count; j++)
                {
                    if (ouput.Contains('.'))
                    {
                        string[] splt = ouput.Split('.');
                        if (splt[0] == "0")
                        {
                            ouput = ouput.Replace("0.", "0.0");
                        }
                        else
                        {
                            if (splt[0].Length > 1)
                                ouput = splt[0].Substring(0, splt[0].Length - 1) + "." + splt[0].Substring(splt[0].Length - 1, 1) + splt[1];
                            else
                                ouput = "0." + splt[0] + splt[1];
                        }
                    }
                    else
                    {
                        if (ouput.Length > 1)
                            ouput = ouput[0] + "." + ouput.Substring(1, ouput.Length - 1);
                        else
                            ouput = "0." + ouput;
                    }
                }
                ouput = isHaveMinuz ? "-" + ouput : ouput;
            }
            else
            {

                bool isHaveMinuz = number[0] == '-';
                if (number.Contains("-"))
                    number = number.Replace("-", "");

                string[] str = number.Split('e');
                if (str[1].Contains('+'))
                    str[1] = str[1].Replace("+", "");
                int count = Convert.ToInt32(str[1]);
                ouput = str[0];
                for (int j = 0; j < count; j++)
                {
                    if (ouput.Contains('.'))
                    {
                        string[] splt = ouput.Split('.');
                        if (splt[0] == "")
                            ouput = splt[1];
                        else if (splt[0] == "0")
                            ouput = splt[1];
                        else if (splt[1].Length > 1)
                            ouput = splt[0] + splt[1][0] + "." + splt[1].Substring(1, splt[1].Length - 1);
                        else
                            ouput = splt[0] + splt[1];
                    }
                    else
                    {
                        ouput += "0";
                    }
                }
                ouput = isHaveMinuz ? "-" + ouput : ouput;
            }
            return ouput;
        }

        private void ResetKeyDropDowns()
        {
            ddlFile1Key1.SelectedIndex = 0;
            ddlFile1Key2.SelectedIndex = 0;
            ddlFile2Key1.SelectedIndex = 0;
            ddlFile2Key2.SelectedIndex = 0;
        }


        private void CheckKey2Enability()
        {
            if (ddlFile1Key1.SelectedValue != null && ddlFile2Key1.SelectedValue != null)
            {
                string file1Key1 = ddlFile1Key1.SelectedValue.ToString();
                string file2Key1 = ddlFile2Key1.SelectedValue.ToString();
                if (file1Key1 != ComboBoxSettings.NoneText || file2Key1 != ComboBoxSettings.NoneText)
                {
                    ddlFile1Key2.IsEnabled = true;
                    ddlFile2Key2.IsEnabled = true;
                }
                else
                {
                    if (ddlFile1Key2.SelectedValue != null && ddlFile2Key2.SelectedValue != null)
                    {
                        ddlFile1Key2.SelectedIndex = 0;
                        ddlFile2Key2.SelectedIndex = 0;
                        ddlFile1Key2.IsEnabled = false;
                        ddlFile2Key2.IsEnabled = false;
                    }
                }
            }
        }

        private List<int> GetCheckedColumnsIndeces()
        {
            GetVisualChildCollection<DataGridColumnHeader>(gridColumnSelection, "addIndices");
            List<int> indecesList = DataGridHelper.indecesList;
            DataGridHelper.indecesList = new List<int>();
            DataGridHelper.columnIndex = -1;
            return indecesList;
        }

        private void CheckAnswerChoiceValidation(Visibility visibility)
        {
            if (lblNoOfChoices != null && txtNoOfChoices != null)
            {
                if (visibility == Visibility.Visible)
                {
                    ComboBoxItem comboAnswerType = (ComboBoxItem)ddlAnswerType.SelectedItem;
                    string answerType = AnswerType.SA;
                    if (comboAnswerType != null) answerType = comboAnswerType.Content.ToString();

                    if ((answerType == "SA") || (answerType == "MA"))
                    {
                        lblNoOfChoices.Visibility = Visibility.Visible;
                        txtNoOfChoices.Visibility = Visibility.Visible;
                        lblChoiceWording.Visibility = Visibility.Visible;
                        gridChoiceWording.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        lblNoOfChoices.Visibility = Visibility.Hidden;
                        txtNoOfChoices.Visibility = Visibility.Hidden;

                        lblChoiceWording.Visibility = Visibility.Hidden;
                        gridChoiceWording.Visibility = Visibility.Hidden;

                    }
                }
                else
                {
                    lblNoOfChoices.Visibility = visibility;
                    txtNoOfChoices.Visibility = visibility;

                    lblChoiceWording.Visibility = visibility;
                    gridChoiceWording.Visibility = visibility;
                }
                ValidateCheckedColumns();
            }

        }

        private void ExecuteActionWhenIdle(Action delegateFunction)
        {
            Dispatcher.InvokeAsync(() => { delegateFunction(); }, DispatcherPriority.ApplicationIdle);
        }

        private void SetCursorForHeader()
        {
            GetVisualChildCollection<DataGridColumnHeader>(gridInformation, "setScroll");

        }

        private void SetInformationGridBgColor()
        {
            b = -1;
            GetVisualChildCollection<DataGridColumnHeader>(gridInformation, "setBg");
        }

        private void UpdateChoiceWordingsCount(List<ChoiceWording> choiceWordings)
        {
            gridChoiceWording.DataContext = ToDatatable(choiceWordings);
            RefreshChoiceWordingGrid();
        }


        private DataTable ToDatatable(List<ChoiceWording> choiceWordings)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SlNo");
            dt.Columns.Add("WordingText");
            foreach (ChoiceWording choiceWording in choiceWordings)
            {
                DataRow drow = dt.NewRow();
                drow["SlNo"] = choiceWording.SlNo;
                drow["WordingText"] = choiceWording.WordingText;
                dt.Rows.Add(drow);
            }
            return dt;
        }

        private ImportInformation GetDefaultImportInformation(int columnIndex, string columnName)
        {
            List<ChoiceWording> choiceWordings = new List<ChoiceWording>();
            for (int i = 1; i <= 1000; i++)
            {
                ChoiceWording choiceWording = new ChoiceWording()
                {
                    SlNo = i,
                    WordingText = string.Empty
                };
                choiceWordings.Add(choiceWording);
            }
            return new ImportInformation()
            {
                AnswerType = AnswerType.SA,
                ChoiceWordings = choiceWordings,
                ColumnIndex = columnIndex,
                ColumnName = columnName,
                IsDataValidated = false,
                NoOfChoices = 1000,
                QuestionSentence = string.Empty,
                QuestionTitle = string.Empty,
                VariableName = dataImportHelper.TempColumnNames.FirstOrDefault(x => x == columnName) == null ? "" : columnName.Trim().Length > 24 ? columnName.Trim().Substring(0, 25) : columnName.Trim(),
                IsColumnSetForFlagFormat = false
            };
        }

        private void UpdateChoiceWordingText(int selectedIndex, string wordingText)
        {
            if (selectedIndex >= 0)
                importSettings.ImportInformations[importSettings.SelectedIndex].ChoiceWordings[selectedIndex].WordingText = wordingText;
        }

        private bool CheckValidationForQuestion()
        {
            if (importSettings.ImportInformations != null && importSettings.ImportInformations.Count > 0)
            {
                SQLiteConnection con = DBHelper.GetConnection(DBHelper.GetConnectionString(DBPath));
                con.Open();
                importSettings.DataCount = gridInformation.Items.Count;
                importSettings.ImportInformations[importSettings.SelectedIndex].IsDataValidated = IsValidInformation(importSettings, con);
                if (!ItemNameChanged)
                    SetInformationGridBgColor();
                else
                    ItemNameChanged = false;
                con.Close(); con.Dispose();
                if (IsExecuteButtonValid())
                {
                    btnExecute.IsEnabled = true;
                }
                else
                {
                    btnExecute.IsEnabled = false;
                }
                return importSettings.ImportInformations[importSettings.SelectedIndex].IsDataValidated;
            }
            return false;
        }

        public bool IsValidInformation(ColumnImportSettings importSettings, SQLiteConnection con, bool isInitialCheck = false)
        {
            if (ItemNameChanged)
            {
                return true;
            }
            ImportInformation importInformation = importSettings.ImportInformations[importSettings.SelectedIndex];
            if (!importInformation.IsColumnSetForFlagFormat)
            {

                if (importInformation.VariableName == null || importInformation.VariableName.Trim().Length == 0)
                {
                    if (allowMessageBox && !messageBoxAllocated && !IsDialogueDispalayed)
                    {
                        messageBoxAllocated = true;
                        IsDialogueDispalayed = true;
                        MessageDialog.Warning(String.Format(LocalResource.IM_MSG_ITEM_BLANCK, importInformation.ColumnName));
                        IsDialogueDispalayed = false;
                    }
                    return false;
                }
                else if (!IsDialogueDispalayed && IsVariableNameDuplicated(importInformation.VariableName))
                {
                    if (allowMessageBox && !messageBoxAllocated)
                    {
                        messageBoxAllocated = true;
                        IsDialogueDispalayed = true;
                        MessageDialog.Warning(String.Format(LocalResource.IM_MSG_ITEM_DUPLICATED, importInformation.ColumnName));
                        IsDialogueDispalayed = false;
                    }
                    return false;
                }
                else if (!IsDialogueDispalayed && !QC4Common.Util.QSUtil.ValidateVariable(importInformation.VariableName, out string msg))
                {
                    if (allowMessageBox && !messageBoxAllocated)
                    {
                        messageBoxAllocated = true;
                        IsDialogueDispalayed = true;
                        MessageDialog.Warning(String.Format(LocalResource.IM_MSG_INVALID_CHARACTERS, importInformation.ColumnName));
                        IsDialogueDispalayed = false;
                    }
                    return false;
                }
                else if (!IsDialogueDispalayed && (importInformation.AnswerType == AnswerType.MA || importInformation.AnswerType == AnswerType.SA) && 
                    ((IsNotValidateFromThread && txtNoOfChoices.Text.Trim().Length == 0 && !isInitialCheck) || (importInformation.NoOfChoices == 0 && isInitialCheck)))
                {
                    if (allowMessageBox && !messageBoxAllocated)
                    {
                        messageBoxAllocated = true;
                        IsDialogueDispalayed = true;
                        MessageDialog.Warning(String.Format(LocalResource.IM_MSG_BLANK_CHOICES, importInformation.ColumnName));
                        IsDialogueDispalayed = false;
                    }
                    return false;
                }
                else if (importInformation.AnswerType == AnswerType.MA || importInformation.AnswerType == AnswerType.SA)
                {
                    if (importInformation.NoOfChoices != importInformation.ChoiceWordings.Count)
                    {
                        if (!IsDialogueDispalayed && allowMessageBox && !messageBoxAllocated)
                        {
                            messageBoxAllocated = true;
                            IsDialogueDispalayed = true;
                            MessageDialog.Warning(String.Format(LocalResource.IM_MSG_CHOICE_NOT_MATCH, importInformation.ColumnName));
                            IsDialogueDispalayed = false;
                        }
                        return false;
                    }
                    else if (importInformation.NoOfChoices <= 0)
                    {
                        if (!IsDialogueDispalayed && allowMessageBox && !messageBoxAllocated)
                        {
                            messageBoxAllocated = true;
                            IsDialogueDispalayed = true;
                            MessageDialog.Warning(String.Format(LocalResource.IM_MSG_CHOICE_NOT_ZERO, importInformation.ColumnName));
                            IsDialogueDispalayed = false;
                        }
                        return false;
                    }
                    else if (!IsDialogueDispalayed && !IsDataExistInChoiceWordings(importSettings, con))
                    {
                        if (allowMessageBox && !messageBoxAllocated)
                        {
                            IsDialogueDispalayed = true;
                            messageBoxAllocated = true;
                            MessageDialog.Warning(String.Format(LocalResource.IM_MSG_CHOICE_MISSMATCH, importInformation.ColumnName));
                            IsDialogueDispalayed = false;
                        }
                        return false;
                    }
                    else if (importInformation.AnswerType == AnswerType.MA)
                    {
                        if (importSettings.MAformat == MAFormat.FlagFormat)
                        {
                            bool IsValidNumber = CheckIsAValidNumberForFlagMA(importInformation, con, importSettings.DataCount, importSettings.NotApplicable);
                            if (IsValidNumber)
                            {
                                return true;
                            }
                            else
                            {
                                if (!IsDialogueDispalayed && allowMessageBox && !messageBoxAllocated)
                                {
                                    messageBoxAllocated = true;
                                    IsDialogueDispalayed = true;
                                    MessageDialog.Warning(String.Format(LocalResource.IM_MSG_INVALID_MA_VALUE, importInformation.ColumnName));
                                    IsDialogueDispalayed = false;
                                }
                                return false;
                            }
                        }
                        else
                        {
                            bool IsValidNumber = CheckIsAValidNumberForMA(importInformation, con, importSettings.DataCount, importSettings.NotApplicable);
                            if (IsValidNumber)
                            {
                                return true;
                            }
                            else
                            {
                                if (!IsDialogueDispalayed && allowMessageBox && !messageBoxAllocated)
                                {
                                    messageBoxAllocated = true;
                                    IsDialogueDispalayed = true;
                                    MessageDialog.Warning(String.Format(LocalResource.IM_MSG_INVALID_COLUMN_VALUE, importInformation.ColumnName));
                                    IsDialogueDispalayed = false;
                                }
                                return false;
                            }
                        }
                    }
                    else if (importInformation.AnswerType == AnswerType.SA)
                    {
                        bool IsValidNumber = CheckIsAValidNumberForSA(importInformation, con, importSettings.DataCount, importSettings.NotApplicable);
                        if (IsValidNumber)
                        {
                            return true;
                        }
                        else
                        {
                            if (!IsDialogueDispalayed && allowMessageBox && !messageBoxAllocated)
                            {
                                messageBoxAllocated = true;
                                IsDialogueDispalayed = true;
                                MessageDialog.Warning(String.Format(LocalResource.IM_MSG_INVALID_SA_VALUE, importInformation.ColumnName));
                                IsDialogueDispalayed = false;
                            }
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
                else if (importInformation.AnswerType == AnswerType.N)
                {
                    bool IsValidNumber = CheckIsAValidNumber(importInformation, con, importSettings.DataCount, importSettings.NotApplicable);
                    if (IsValidNumber)
                    {
                        return true;
                    }
                    else
                    {
                        if (!IsDialogueDispalayed && allowMessageBox && !messageBoxAllocated)
                        {
                            messageBoxAllocated = true;
                            IsDialogueDispalayed = true;
                            MessageDialog.Warning(String.Format(LocalResource.IM_MSG_INVALID_COLUMN_VALUE, importInformation.ColumnName));
                            IsDialogueDispalayed = false;
                        }
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        private bool IsExecuteButtonValid()
        {
            int selectedIndex = importSettings.SelectedIndex;
            if (importSettings.ImportInformations != null && importSettings.ImportInformations.Count > 0)
            {
                for (int i = 0; i <= importSettings.ImportInformations.Count - 1; i++)
                {
                    importSettings.SelectedIndex = i;
                    ImportInformation importInformation = importSettings.ImportInformations[i];
                    if (importInformation.IsColumnSetForFlagFormat == false)
                    {

                        if (importInformation.VariableName == null || importInformation.VariableName.Trim().Length == 0)
                        {
                            importSettings.SelectedIndex = selectedIndex;
                            return false;
                        }
                        else if ((importInformation.AnswerType == AnswerType.MA || importInformation.AnswerType == AnswerType.SA) && txtNoOfChoices.Text.Trim().Length == 0)
                        {
                            importSettings.SelectedIndex = selectedIndex;
                            return false;
                        }
                        else if (importInformation.AnswerType == AnswerType.MA || importInformation.AnswerType == AnswerType.SA)
                        {
                            if (importInformation.NoOfChoices != importInformation.ChoiceWordings.Count)
                            {
                                importSettings.SelectedIndex = selectedIndex;
                                return false;
                            }
                        }
                    }
                }
            }
            else
            {
                importSettings.SelectedIndex = selectedIndex;
                return false;
            }

            importSettings.SelectedIndex = selectedIndex;
            return true;
        }

        private bool IsVariableNameDuplicated(string inputName)
        {
            bool IsInvalid = false;

            var selectedList = file1Variables.AsEnumerable().Where(field => field.Key.ToUpper().Trim() == inputName.ToUpper().Trim()).ToList();

            if (selectedList != null && selectedList.Count > 0)
            {
                IsInvalid = true;
            }

            List<ImportInformation> selectedData = importSettings.ImportInformations.AsEnumerable().Where(field => field.VariableName.ToUpper().Trim() == inputName.ToUpper().Trim()).ToList();
            if (selectedData != null && selectedData.Count > 1)
            {
                IsInvalid = true;
            }

            return IsInvalid;

        }

        private bool CheckIsAValidNumberForFlagMA(ImportInformation importInformation, SQLiteConnection con, int dataCount, string notApplicable)
        {
            DataTable dt = importSettings.BeforeProcessingData;
            string columnName = "";

            int choice = 0;
            for (int i = 0; i < importSettings.ImportInformations.Count; i++)
            {
                if (importSettings.SelectedIndex <= i)
                {
                    choice++;
                    columnName = importSettings.ImportInformations[i].ColumnName;
                    string dataSql = GetFilteredData();

                    string sSql = "Select count(*) from (SELECT `" + columnName + "` FROM " + dataSql + " )";
                    sSql += " Where (`" + columnName + "` <> Null ";
                    sSql += "or `" + columnName + "` <> '') And `" + columnName + "` != '1' and `" + columnName + "` != '0' ";
                    if (notApplicable != "")
                        sSql += "and `" + columnName + "` != '" + notApplicable.Replace("'", "''") + "'";

                    DataTable dataTble = DBHelper.GetDataTable(sSql, con);
                    if (Convert.ToInt32(dataTble.Rows[0][0]) > 0)
                    {
                        return false;
                    }
                    if (choice == importInformation.NoOfChoices)
                        break;
                }
            }

            return true;
        }

        private bool CheckIsAValidNumberForMA(ImportInformation importInformation, SQLiteConnection con, int dataCount, string notApplicable)
        {
            DataTable dt = importSettings.BeforeProcessingData;
            int selectedIndex = importSettings.SelectedIndex;
            string columnName = dt.Columns[selectedIndex].ColumnName;

            string dataSql = GetFilteredData();

            string sSql = "Select `" + columnName + "` from (SELECT `" + columnName + "` FROM " + dataSql + " )";
            sSql += " Where (`" + columnName + "` <> Null ";
            sSql += "or `" + columnName + "` <> '') ";
            if (notApplicable != "")
                sSql += "and `" + columnName + "` != '" + notApplicable.Replace("'", "''") + "'";

            DataTable dataTble = DBHelper.GetDataTable(sSql, con);
            if (dataTble.Rows.Count > 0)
            {
                for (int i = 0; i < dataTble.Rows.Count; i++)
                {
                    if (dataTble.Rows[i][columnName] != null && dataTble.Rows[i][columnName] != DBNull.Value && dataTble.Rows[i][columnName].ToString().Any(c => c > 255))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private string GetFilteredData()
        {
            bool IsFile2Exist = false;
            bool IsKey1Exist = false;
            if (importSettings.DestinationFileKey1 != ComboBoxSettings.NoneText && importSettings.SourceFileKey1 != ComboBoxSettings.NoneText)
            {
                IsKey1Exist = true;
                if (importSettings.DestinationFileKey2 != ComboBoxSettings.NoneText && importSettings.SourceFileKey2 != ComboBoxSettings.NoneText)
                    IsFile2Exist = true;
            }
            string dataSql = " (Select t.* from ";

            if (IsKey1Exist)
            {
                dataSql += " answers a, " + DataImportSettings.DataImportSourceTempTable + " t ";
                dataSql += DataImportHelper.GetQuery1(importSettings.DestinationFileKey1, importSettings.DestinationFileKey2, importSettings.SourceFileKey1, importSettings.SourceFileKey2, IsFile2Exist);
            }
            else
                dataSql += DataImportSettings.DataImportSourceTempTable + " t ";

            dataSql += " ) ";
            return dataSql;
        }

        private bool CheckIsAValidNumberForSA(ImportInformation importInformation, SQLiteConnection con, int dataCount, string notApplicable)
        {
            DataTable dt = importSettings.BeforeProcessingData;
            int selectedIndex = importSettings.SelectedIndex;
            string columnName = dt.Columns[selectedIndex].ColumnName;
            string dataSql = GetFilteredData();

            string sSql = "Select count(*) from (SELECT `" + columnName + "` FROM " + dataSql + " )";
            sSql += " Where (`" + columnName + "` <> Null ";
            sSql += "or `" + columnName + "` <> '') And `" + columnName + "` <> '" + notApplicable.Replace("'", "''") + "' AND ";
            sSql += "( CAST(trim(`" + columnName + "`) as int) =0 or CAST(trim(`" + columnName + "`) as int) <=0 or ";
            sSql += "length(CAST(CAST(trim(`" + columnName + "`) as int) as varchar)) != length(trim(`" + columnName + "`)))";
            DataTable dataTble = DBHelper.GetDataTable(sSql, con);
            if (Convert.ToInt32(dataTble.Rows[0][0]) > 0)
            {
                return false;
            }

            return true;
        }

        private bool CheckIsAValidNumber(ImportInformation importInformation, SQLiteConnection con, int dataCount, string notApplicable)
        {
            DataTable dt = importSettings.BeforeProcessingData;
            int selectedIndex = importSettings.SelectedIndex;
            string columnName = dt.Columns[selectedIndex].ColumnName;
            string dataSql = GetFilteredData();

            string sSql = "Select `" + columnName + "` from (SELECT `" + columnName + "` FROM " + dataSql + " )";
            sSql += " Where `" + columnName + "` <> '0' And (`" + columnName + "` <> Null ";
            sSql += "or `" + columnName + "` <> '') And `" + columnName + "` <> '" + notApplicable.Replace("'", "''") + "' ";
            DataTable dataTble = DBHelper.GetDataTable(sSql, con);
            for (int i = 0; i < dataTble.Rows.Count; i++)
            {
                if (dataTble.Rows[i][columnName] != null && dataTble.Rows[i][columnName] != DBNull.Value && !IsValidFormat(dataTble.Rows[i][columnName].ToString()))
                {
                    return false;
                }
            }

            return true;
        }
        private bool IsValidFormat(string str)
        {
            if (str.Any(c => c > 255))
                return false;
            if ((str.Contains("E") || str.Contains("e")))
                return Regex.IsMatch(str, @"^[-+]?[0-9]*\.?[0-9]+([eE][-+]?[0-9]+)$");
            else
                return Regex.IsMatch(str, @"^-?[0-9]\d*(\.\d+)?$");
        }
        
        private bool IsDataExistInChoiceWordings(ColumnImportSettings importSettings, SQLiteConnection con) // open connection
        {
            ImportInformation importInformation = importSettings.ImportInformations[importSettings.SelectedIndex];
            List<ChoiceWording> choiceWordings = importInformation.ChoiceWordings;
            DataTable dt = importSettings.BeforeProcessingData;
            int selectedIndex = importSettings.SelectedIndex;
            string columnName = dt.Columns[selectedIndex].ColumnName;
            string sSql = "";
            DataTable dataTble;
            if (importInformation.AnswerType == AnswerType.SA)
            {
                try
                {
                    sSql = "Select count(*) from " + GetFilteredData() + " ";
                    sSql += " Where ";
                    sSql += " `" + columnName + "` <> '" + importSettings.NotApplicableCharacter.Replace("'", "''") + "' ";
                    sSql += " And `" + columnName + "` is not null And `" + columnName + "` != '' And Cast(`" + columnName + "` As Int) <= 0 ";
                    dataTble = DBHelper.GetDataTable(sSql, con);
                    if (Convert.ToInt32(dataTble.Rows[0][0]) > 0)
                    {
                        if (allowMessageBox && !messageBoxAllocated && !IsDialogueDispalayed)
                        {
                            IsDialogueDispalayed = true;
                            MessageDialog.Warning(String.Format(LocalResource.IM_MSG_INVALID_SA_VALUE, importInformation.ColumnName));
                            messageBoxAllocated = true;
                            IsDialogueDispalayed = false;
                        }
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    _log.LogError(ex.Message + "\n" + ex.Message);
                    if (allowMessageBox && !messageBoxAllocated && !IsDialogueDispalayed)
                    {
                        IsDialogueDispalayed = true;
                        MessageDialog.Warning(String.Format(LocalResource.IM_MSG_INVALID_SA_VALUE, importInformation.ColumnName));
                        messageBoxAllocated = true;
                        IsDialogueDispalayed = false;
                    }
                    return false;
                }

                int rowCount = gridInformation.Items.Count;
                if (importSettings.DestinationFileKey1 != ComboBoxSettings.NoneText && importSettings.SourceFileKey1 != ComboBoxSettings.NoneText)
                {
                    bool IsFile2Exist = false;
                    if (importSettings.DestinationFileKey2 != ComboBoxSettings.NoneText && importSettings.SourceFileKey2 != ComboBoxSettings.NoneText)
                        IsFile2Exist = true;
                    sSql = DataImportHelper.GetMASplittingQueryMax(importInformation.ColumnName, DataImportSettings.DataImportSourceTempTable, importSettings.NotApplicable, rowCount, IsFile2Exist, importSettings, con);
                }
                else
                {
                    sSql = DataImportHelper.GetMASplittingQueryMax(importInformation.ColumnName, DataImportSettings.DataImportSourceTempTable, importSettings.NotApplicable, rowCount);
                }
                dataTble = DBHelper.GetDataTable(sSql, con);
                if (dataTble.Rows[0][0] == DBNull.Value || dataTble.Rows[0][0] == null)
                {
                    if (allowMessageBox && !messageBoxAllocated && !IsDialogueDispalayed)
                    {
                        IsDialogueDispalayed = true;
                        MessageDialog.Warning(String.Format(LocalResource.IM_MSG_NO_DATA_AVAILABLE, importInformation.ColumnName));
                        messageBoxAllocated = true;
                        IsDialogueDispalayed = false;
                    }
                    return false;
                }
                else if (dataTble.Rows[0][0] == DBNull.Value || dataTble.Rows[0][0] == null)
                {
                    if (allowMessageBox && !messageBoxAllocated && !IsDialogueDispalayed)
                    {
                        IsDialogueDispalayed = true;
                        MessageDialog.Warning(String.Format(LocalResource.IM_MSG_NO_DATA_AVAILABLE, importInformation.ColumnName));
                        messageBoxAllocated = true;
                        IsDialogueDispalayed = false;
                    }
                    return false;
                }
                else if (CheckChoiceCount(dataTble.Rows[0][0], importInformation.NoOfChoices))//Redmine Id:228570
                {
                    if (allowMessageBox && !messageBoxAllocated && !IsDialogueDispalayed)
                    {
                        IsDialogueDispalayed = true;
                        MessageDialog.Warning(String.Format(LocalResource.IM_MSG_SA_NOT_GRATERTHAN_CHOICE, importInformation.ColumnName));
                        messageBoxAllocated = true;
                        IsDialogueDispalayed = false;
                    }
                    return false;
                }
            }
            else
            {
                if (importSettings.MAformat == MAFormat.FlagFormat)
                {
                    try
                    {
                        int choiceWordingCount = importInformation.NoOfChoices;
                        int lastIndex = selectedIndex + choiceWordingCount - 1;

                        if (dt.Columns.Count < (lastIndex + 1))
                        {
                            if (allowMessageBox && !messageBoxAllocated && !IsDialogueDispalayed)
                            {
                                IsDialogueDispalayed = true;
                                MessageDialog.Warning(String.Format(LocalResource.IM_MSG_INVALID_NUMBEROF_CHOICES, importInformation.ColumnName));
                                messageBoxAllocated = true;
                                IsDialogueDispalayed = false;
                            }
                            return false;
                        }
                        else
                        {

                            List<string> columnNames = new List<string>();
                            for (int i = selectedIndex; i <= lastIndex; i++)
                            {
                                columnNames.Add(dt.Columns[i].ColumnName);
                            }
                            int ColumnLimit = columnNames.Count;
                            int givenCount = columnNames.Count;
                            int columnNum = 0;
                            while (givenCount != 0)
                            {
                                if (givenCount > DBSettings.MaxNoOfColumnInsertInBulk)
                                {
                                    ColumnLimit = DBSettings.MaxNoOfColumnInsertInBulk;
                                    givenCount = givenCount - DBSettings.MaxNoOfColumnInsertInBulk;
                                }
                                else
                                {
                                    ColumnLimit = givenCount;
                                    givenCount = 0;
                                }

                                sSql = " Select count(*) from `" + DataImportSettings.DataImportSourceTempTable + "` Where ";
                                for (int i = 0; i <= ColumnLimit - 1; i++)
                                {
                                    sSql += " ((`" + columnNames[columnNum] + "` <> '0') And (`" + columnNames[columnNum] + "` <> '1') And (`" + columnNames[columnNum] + "` <> '" + importSettings.NotApplicableCharacter.Replace("'", "''") + "') And (`" + columnNames[columnNum] + "` <> Null)) ";
                                    if ((i + 1) <= ColumnLimit - 1)
                                        sSql += " Or ";
                                }
                                dataTble = DBHelper.GetDataTable(sSql, con);
                                if (Convert.ToInt32(dataTble.Rows[0][0]) > 0)
                                {
                                    if (allowMessageBox && !messageBoxAllocated && !IsDialogueDispalayed)
                                    {
                                        IsDialogueDispalayed = true;
                                        MessageDialog.Warning(String.Format(LocalResource.IM_MSG_INVALID_MA_VALUE, importInformation.ColumnName));
                                        messageBoxAllocated = true;
                                        IsDialogueDispalayed = false;
                                    }
                                    return false;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.LogError(ex.Message + "\n" + ex.Message);
                        if (allowMessageBox && !messageBoxAllocated && !IsDialogueDispalayed)
                        {
                            IsDialogueDispalayed = true;
                            MessageDialog.Warning(String.Format(LocalResource.IM_MSG_DATA_VALIDATION_FAILED, importInformation.ColumnName));
                            messageBoxAllocated = true;
                            IsDialogueDispalayed = false;
                        }
                        return false;
                    }
                } // if is comma seperated
                else
                {
                    sSql = " Select count(*) from  ";
                    sSql += " (Select (printf(\"%d\",Replace(Replace(Replace(`" + columnName + "`,',','1'),'0','1'),'" + importSettings.NotApplicableCharacter.Replace("'", "''") + "','1')) = Replace(Replace(Replace(`" + columnName + "`,',','1'),'0','1'),'" + importSettings.NotApplicableCharacter.Replace("'", "''") + "','1')) As CastCol from `" + DataImportSettings.DataImportSourceTempTable + "` Where `" + columnName + "` <> NULL ) As CastTbl";
                    sSql += " Where ";
                    sSql += " CastTbl.CastCol <= 0 ";

                    dataTble = DBHelper.GetDataTable(sSql, con);
                    if (Convert.ToInt32(dataTble.Rows[0][0]) > 0)
                    {
                        if (allowMessageBox && !messageBoxAllocated && !IsDialogueDispalayed)
                        {
                            IsDialogueDispalayed = true;
                            MessageDialog.Warning(String.Format(LocalResource.IM_MSG_MA_SHOULD_NUMERIC, importInformation.ColumnName));
                            messageBoxAllocated = true;
                            IsDialogueDispalayed = false;
                        }
                        return false;
                    }

                    sSql = "SELECT REPLACE(`" + columnName + "`,',','') as `" + columnName + "` from (SELECT `" + columnName + "` FROM  " + GetFilteredData() + " ) ";
                    sSql += "WHERE `" + columnName + "` <> '" + importSettings.NotApplicable.Replace("'", "''") + "' AND (`" + columnName + "` IS NOT NULL or `" + columnName + "` <> '') AND `" + columnName + "` <> 0 AND ";
                    sSql += "(LENGTH(REPLACE(`" + columnName + "`,',','')) - LENGTH(REPLACE(REPLACE(`" + columnName + "`,',',''), '0', '')))/LENGTH('0') <> LENGTH(REPLACE(`" + columnName + "`,',','')) ";

                    dataTble = DBHelper.GetDataTable(sSql, con);
                    bool isNum = true;
                    for (int t = 0; t < dataTble.Rows.Count; t++)
                    {
                        if (dataTble.Rows[t][columnName] != null && dataTble.Rows[t][columnName] != DBNull.Value && !Regex.IsMatch(dataTble.Rows[t][columnName].ToString(), @"^\d+$"))
                            isNum = false;
                    }
                    if (!isNum)
                    {
                        if (allowMessageBox && !messageBoxAllocated && !IsDialogueDispalayed)
                        {
                            IsDialogueDispalayed = true;
                            MessageDialog.Warning(String.Format(LocalResource.IM_MSG_MA_SHOULD_NUMERIC, importInformation.ColumnName));
                            messageBoxAllocated = true;
                            IsDialogueDispalayed = false;
                        }
                        return false;
                    }

                    int rowCount = gridInformation.Items.Count;
                    if (importSettings.DestinationFileKey1 != ComboBoxSettings.NoneText && importSettings.SourceFileKey1 != ComboBoxSettings.NoneText)
                    {
                        bool IsFile2Exist = false;
                        if (importSettings.DestinationFileKey2 != ComboBoxSettings.NoneText && importSettings.SourceFileKey2 != ComboBoxSettings.NoneText)
                            IsFile2Exist = true;
                        sSql = DataImportHelper.GetMASplittingQueryMax(importInformation.ColumnName, DataImportSettings.DataImportSourceTempTable, importSettings.NotApplicable, rowCount, IsFile2Exist, importSettings, con);
                    }
                    else
                    {
                        sSql = DataImportHelper.GetMASplittingQueryMax(importInformation.ColumnName, DataImportSettings.DataImportSourceTempTable, importSettings.NotApplicable, rowCount);
                    }

                    dataTble = DBHelper.GetDataTable(sSql, con);
                    if (dataTble.Rows[0][0] == DBNull.Value || dataTble.Rows[0][0] == null)
                    {
                        if (allowMessageBox && !messageBoxAllocated && !IsDialogueDispalayed)
                        {
                            IsDialogueDispalayed = true;
                            MessageDialog.Warning(String.Format(LocalResource.IM_MSG_NO_DATA_AVAILABLE, importInformation.ColumnName));
                            messageBoxAllocated = true;
                            IsDialogueDispalayed = false;
                        }
                        return false;
                    }
                    else if (Convert.ToInt32(dataTble.Rows[0][0]) > importInformation.NoOfChoices)
                    {
                        if (allowMessageBox && !messageBoxAllocated && !IsDialogueDispalayed)
                        {
                            IsDialogueDispalayed = true;
                            MessageDialog.Warning(String.Format(LocalResource.IM_MSG_MA_NOT_GRATERTHAN_CHOICE, importInformation.ColumnName));
                            messageBoxAllocated = true;
                            IsDialogueDispalayed = false;
                        }
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// Method to convert object value to integer value
        /// </summary>
        /// <param name="data">object data</param>
        /// <param name="noOfChoices">Variable choice count</param>
        /// <returns></returns>
        private bool CheckChoiceCount(object data, int noOfChoices)
        {
            try
            {
                return Convert.ToInt64(data) > noOfChoices;
            }
            catch
            {
                return true;
            }
        }

        private bool BindInformationOnModelAndScreen(ImportInformation importInformation)
        {
            int selectedIndex = 0;
            switch (importInformation.AnswerType)
            {
                case "SA":
                    selectedIndex = 0;
                    break;
                case "MA":
                    selectedIndex = 1;
                    break;
                case "N":
                    selectedIndex = 2;
                    break;
                case "FA":
                    selectedIndex = 3;
                    break;
            }
            txtItemName.Text = importInformation.VariableName;
            ddlAnswerType.SelectedIndex = selectedIndex;
            txtNoOfChoices.Text = importInformation.NoOfChoices.ToString();
            txtQuestionTitle.Text = importInformation.QuestionTitle;
            txtQuestionSentence.Text = importInformation.QuestionSentence;
            UpdateChoiceWordingsCount(importInformation.ChoiceWordings);
            return true;
        }

        private void AllocateColumnsIfFlagFormat()
        {
            DataTable dt = importSettings.BeforeProcessingData;
            if (dt != null)
            {
                int selectedIndex = importSettings.SelectedIndex;
                string columnName = dt.Columns[selectedIndex].ColumnName;

                ImportInformation importInformation = importSettings.ImportInformations[selectedIndex];

                if ((importSettings.MAformat == MAFormat.FlagFormat) && (importInformation.AnswerType == AnswerType.MA))
                {
                    int choiceWordingCount = importInformation.NoOfChoices;
                    int lastIndex = selectedIndex + choiceWordingCount - 1;
                    if (dt.Columns.Count < (lastIndex + 1))
                    {
                        importSettings.ImportInformations[selectedIndex].NoOfChoices = 0;
                        txtNoOfChoices.Text = "";
                        importSettings.ImportInformations[importSettings.SelectedIndex].ChoiceWordings = new List<ChoiceWording>();
                    }
                    else
                    {
                        for (int i = selectedIndex + 1; i <= lastIndex; i++)
                        {
                            importSettings.ImportInformations[i].IsColumnSetForFlagFormat = true;
                            importSettings.ImportInformations[i].IsDataValidated = true;
                        }
                    }
                }
            }
        }

        private void ReleaseColumnsForFlagFormat()
        {
            DataTable dt = importSettings.BeforeProcessingData;
            if (dt != null)
            {
                int selectedIndex = importSettings.SelectedIndex;

                ImportInformation importInformation = importSettings.ImportInformations[selectedIndex];

                if ((importSettings.MAformat == MAFormat.FlagFormat) && (importInformation.AnswerType == AnswerType.MA))
                {
                    int choiceWordingCount = importInformation.NoOfChoices;
                    int lastIndex = selectedIndex + choiceWordingCount - 1;
                    if (dt.Columns.Count < (lastIndex + 1))
                    {
                        importSettings.ImportInformations[selectedIndex].NoOfChoices = 0;
                        txtNoOfChoices.Text = "";
                        importSettings.ImportInformations[importSettings.SelectedIndex].ChoiceWordings = new List<ChoiceWording>();
                    }
                    else
                    {
                        for (int i = selectedIndex + 1; i <= lastIndex; i++)
                        {
                            importSettings.ImportInformations[i].IsColumnSetForFlagFormat = false;
                            importSettings.ImportInformations[i].IsDataValidated = false;
                        }
                    }
                }
            }
        }

        private void GoToPrevColumn()
        {
            int currentIndex = importSettings.SelectedIndex;
            for (int i = (currentIndex - 1); i >= 0; i--)
            {
                if (importSettings.ImportInformations[i].IsColumnSetForFlagFormat == false)
                {
                    importSettings.SelectedIndex = i;
                    LoadMergingColumnDataToView(i);
                    return;
                }
            }
        }

        private void GoToNextColumn()
        {
            int currentIndex = importSettings.SelectedIndex;
            for (int i = currentIndex + 1; i <= importSettings.ImportInformations.Count - 1; i++)
            {
                if (importSettings.ImportInformations[i].IsColumnSetForFlagFormat == false)
                {
                    importSettings.SelectedIndex = i;
                    LoadMergingColumnDataToView(i);
                    return;
                }
            }
        }

        private void CheckColumnNextAndPreviousIsValid()
        {
            int currentIndex = importSettings.SelectedIndex;
            bool isPrevAvailable = false;
            bool isNextAvailable = false;

            for (int i = (currentIndex - 1); i >= 0; i--)
            {
                if (importSettings.ImportInformations[i].IsColumnSetForFlagFormat == false)
                {
                    isPrevAvailable = true;
                    break;
                }
            }

            for (int i = (currentIndex + 1); i <= importSettings.ImportInformations.Count - 1; i++)
            {
                if (importSettings.ImportInformations[i].IsColumnSetForFlagFormat == false)
                {
                    isNextAvailable = true;
                    break;
                }
            }

            if (isPrevAvailable)
            {
                btn_PrevData.IsEnabled = true;
            }
            else
            {
                btn_PrevData.IsEnabled = false;
            }

            if (isNextAvailable)
            {
                btn_NextData.IsEnabled = true;
            }
            else
            {
                btn_NextData.IsEnabled = false;
            }

        }

        private bool IsMergingScreenValid()
        {
            bool isValid = false;
            List<ImportInformation> selectedData = importSettings.ImportInformations.AsEnumerable().Where(field => field.IsDataValidated == false).ToList();
            if (selectedData != null && selectedData.Count > 0)
            {
                isValid = false;
            }
            else
            {
                isValid = true;
            }
            return isValid;
        }

        private void RemoveTempColumnsFromAnswers(SQLiteConnection dbConn)
        {
            try
            {
                string sql = "SELECT name,type FROM pragma_table_info('answers');";
                DataTable dt = DBHelper.GetDataTable(sql, dbConn);
                StringBuilder columns = new StringBuilder();
                StringBuilder colWithSchema = new StringBuilder();
                sql = "DROP TABLE IF EXISTS tempans; create table tempans as select * from answers";
                DBHelper.ExecuteQuery(sql, dbConn);
                int colCnt = dt.Rows.Count;
                for (int i = 0; i < colCnt; i++)
                {
                    string col = dt.Rows[i][0].ToString();
                    if (col != "temprysfiltered" && col != "temprysfiltered1")
                    {
                        columns.Append(dt.Rows[i][0].ToString() + ",");
                        if (col == "sort_no")
                            colWithSchema.Append("sort_no INTEGER PRIMARY KEY AUTOINCREMENT,");
                        else
                            colWithSchema.Append(dt.Rows[i][0].ToString() + " " + dt.Rows[i][1].ToString() + ",");
                    }
                }
                sql = "drop table answers;";
                DBHelper.ExecuteQuery(sql, dbConn);
                string cols = columns.ToString();
                cols = cols.Remove(cols.Length - 1, 1);
                string colswithSch = colWithSchema.ToString();
                colswithSch = colswithSch.Remove(colswithSch.Length - 1, 1);
                sql = "create table answers(" + colswithSch + ");";
                DBHelper.ExecuteQuery(sql, dbConn);
                sql = "insert into answers select " + cols + " from tempans";
                DBHelper.ExecuteQuery(sql, dbConn);
                sql = "DROP TABLE IF EXISTS tempans;";
                DBHelper.ExecuteQuery(sql, dbConn);
                sql = "VACUUM;";
                DBHelper.ExecuteQuery(sql, dbConn);
            }
            catch (Exception ex)
            {

            }
        }

        private void CancelAllSettings()
        {
            try
            {
                if (SelectedFile1 != "")
                    new DataImportHelper().DropTempTable(DBPath);

                using (SQLiteConnection dbConn = DBHelper.GetConnection(DBHelper.GetConnectionString(DBPath)))
                {
                    dbConn.Open();
                    RemoveTempColumnsFromAnswers(dbConn);
                }
            }
            catch { }

            CloseFile1();
            SelectedFile1 = string.Empty;
            SelectedFile2 = string.Empty;
            file1TempFolderPath = null;
            progress = null;
            dtFile2JoinedDataBeforeProcessing = new DataTable();
            dtFile2JoinedDataAfterProcessing = new DataTable();
            dtFile2 = new DataTable();
            enableVennDiagramCalculation = true;
            screenMode = DataImportScreenMode.FileSelection;
            importSettings = new ColumnImportSettings();
            txt_FileName.Text = string.Empty;
            txt_FileName2.Text = string.Empty;

            SetPreviewScreenVisibility(Visibility.Hidden);
            SetKeySettingsVisibility(Visibility.Hidden);
            SetColumnSelectionVisibility(Visibility.Hidden);
            SetMergingSettingVisibility(Visibility.Hidden);
            ShowFileSelectionScreen();

            IsNextButtonEnabled = false;
            btn_Next.IsEnabled = false;

            SetDefaultDelimitterSettings();
            SetDefaultMAFormat();

            DataImportFile1WorkBook = null;

            IsDataProcessed = false;
            ReInsertToTempData = true;
            IsForceKeyChange = false;
            IsPasteOperation = false;
            SetVennBottomDesc();

            IsMainWindowFile = false;
        }


        private void SetDefaultDelimitterSettings()
        {
            ReInsertToTempData = true;
            enableVennDiagramCalculation = true;
            radComma.IsChecked = true;
            ddlEncodingChar.SelectedIndex = 0;
            ddlEnclosedChars.SelectedIndex = 0;
            txt_CustomDelimitter.Text = string.Empty;
            txt_CustomDelimitter.IsEnabled = false;
        }

        private void SetDefaultMAFormat()
        {
            ddlMaFormat.SelectedIndex = 0;
            txt_NotApplicableChar.Text = string.Empty;
            chkSelectAll.IsChecked = false;
        }

        private void LoadDefaultFile1(string selectedFile, Excel.Workbook workBook, string file1TempFolder)
        {
            IsMainWindowFile = false;
            if (selectedFile != null && selectedFile.Trim().Length > 0 && workBook != null && file1TempFolder != null && file1TempFolder.Trim().Length > 0
                && selectedFile.Substring(selectedFile.Length - 4) == ".qc4" && File.Exists(selectedFile))
            {
                IsMainWindowFile = true;

                CloseFile1();
                SetDefaultDelimitterSettings();
                txt_FileName.Text = selectedFile;
                SelectedFile1 = selectedFile;
                _log.Info("Selected File 1 - " + SelectedFile1);


                IsFile1Duplicated = false;
                DataImportFile1WorkBook = workBook;
                file1TempFolderPath = file1TempFolder;
                excelApp = DataImportFile1WorkBook.Application;

                CheckDestFileDataCount();

                IsDataProcessed = UpdateDataProcessedStatus();

                CheckNextButtonIsValid();
            }
        }


        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void GridInformation_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            try
            {
                e.Row.IsEnabled = false;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
            }

        }

        private void TextBox_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Paste)
            {
                IsPasteOperation = true;
            }
        }

        private void TextBox_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void DdlMaFormat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (screenMode == DataImportScreenMode.ColumnSelection)
                importSettings.ImportInformations = new List<ImportInformation>(); // Resetting importInformation
        }

        private void GridChoiceWording_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender != null)
            {
                DataGrid grid = sender as DataGrid;
                if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                {
                    DataGridRow dgr = grid.ItemContainerGenerator.ContainerFromItem(grid.SelectedItem) as DataGridRow;
                    if (dgr != null && !dgr.IsMouseOver)
                    {
                        (dgr as DataGridRow).IsSelected = false;
                    }
                }
            }
        }

        private void GridChoiceWording_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (gridChoiceWording.CurrentItem != null)
                    gridChoiceWording.SelectedIndex = gridChoiceWording.Items.IndexOf(gridChoiceWording.CurrentItem);
                bool _altModifierPressed = (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl));
                if (_altModifierPressed && Keyboard.IsKeyDown(Key.V) && gridChoiceWording.SelectedIndex >= 0)
                {
                    e.Handled = true;
                    string clipboardText = "";
                    if (Clipboard.ContainsText(TextDataFormat.UnicodeText))
                    {
                        clipboardText = Clipboard.GetText(TextDataFormat.UnicodeText);
                    }
                    IsPasteOperation = false;
                    int selectedIndex = gridChoiceWording.SelectedIndex;
                    int gridRowLength = gridChoiceWording.Items.Count;
                    string regexReplacedStr = "";
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                    while (clipboardText != "")
                    {
                        string str = "";
                        if (clipboardText.Length > 1000000)
                        {
                            str = clipboardText.Substring(0, 1000000);
                            clipboardText = clipboardText.Remove(0, 1000000);
                        }
                        else
                        {
                            str = clipboardText;
                            clipboardText = "";
                        }
                        regexReplacedStr += Regex.Replace(str.Trim().Replace("\"\"", "\t( ͡❛ ͜ʖ ͡❛)\""), "(?!(([^\"]*\"){2})*[^\"]*$)(\\n|\\r|\\r\\n)+", string.Empty);
                    }

                    List<string> choiceWordings = regexReplacedStr.Split(new[] { "\r\n" }, StringSplitOptions.None).ToList();

                    for (int i = 0; i <= choiceWordings.Count - 1; i++)
                    {
                        string[] nChoiceWordings = choiceWordings[i].Split(new[] { "\r", "\n" }, StringSplitOptions.None);
                        choiceWordings[i] = string.Join("\n", nChoiceWordings).Replace("\"", "").Replace("\t( ͡❛ ͜ʖ ͡❛)", "\"");
                    }
                    int j = 0;
                    for (int i = selectedIndex; i <= gridRowLength - 1; i++)
                    {
                        if (j <= (choiceWordings.Count - 1))
                        {
                            UpdateChoiceWordingText(i, choiceWordings[j]);
                        }
                        j++;
                    }

                    UpdateChoiceWordingsCount(importSettings.ImportInformations[importSettings.SelectedIndex].ChoiceWordings);
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void TextBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width > 93.256666666666675 && Convert.ToInt16(importSettings.ImportInformations[importSettings.SelectedIndex].NoOfChoices) > 8)
            {
                gridChoiceWording.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            }
            else if (e.NewSize.Width > 109.69000000000001 && Convert.ToInt16(importSettings.ImportInformations[importSettings.SelectedIndex].NoOfChoices) <= 8)
            {
                gridChoiceWording.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            }
        }

        private void btn_read_qlayout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Multiselect = false;
                ofd.Filter = "Qlayoutファイル|*_Qlayout.csv;*.csv";
                if (ofd.ShowDialog() == true)
                {
                    BatchFileProcess(ofd.FileName);
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "\n" + ex.StackTrace);
                ShowCommonExceptionMessage();
            }
        }

        private void BatchFileProcess(string fileName)
        {
            Encoding encode = Encoding.GetEncoding("shift_jis");
            for (int i = 0; i <= importSettings.BeforeProcessingData.Columns.Count - 1; i++)
            {
                importSettings.SelectedIndex = i;
                importSettings.SelectedColumn = importSettings.BeforeProcessingData.Columns[i].ColumnName;
                importSettings.ImportInformations[i] = GetDefaultImportInformation(importSettings.SelectedIndex, importSettings.SelectedColumn);
            }
            if (Check_utf8.IsChecked == true)
            {
                encode = Encoding.GetEncoding("utf-8");
            }

            BatchFileController qcmController = new BatchFileController(fileName, encode, ref importSettings, DBPath, this);
            ItemNameChanged = false;
            if (qcmController.ProcessBatchFile())
            {
                ExecuteActionWhenIdle(PopulateModelDataOnView);
                SetInformationGridBgColor();
                gridInformation.ScrollIntoView(gridInformation.Items.GetItemAt(0), gridInformation.Columns[0]);
                CheckColumnNextAndPreviousIsValid();
            }
        }
    }// End Class
}


