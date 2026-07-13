namespace ExcelAddIn
{
    partial class QC4Ribbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public QC4Ribbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QC4Ribbon));
            this.QC4 = this.Factory.CreateRibbonTab();
            this.Menu = this.Factory.CreateRibbonGroup();
            this.buttonMenu = this.Factory.CreateRibbonButton();
            this.Common = this.Factory.CreateRibbonGroup();
            this.buttonInsert = this.Factory.CreateRibbonButton();
            this.buttonDelete = this.Factory.CreateRibbonButton();
            this.buttonCheck = this.Factory.CreateRibbonButton();
            this.QSettings1 = this.Factory.CreateRibbonGroup();
            this.buttonEQ = this.Factory.CreateRibbonButton();
            this.buttonVEM = this.Factory.CreateRibbonButton();
            this.QSettings2 = this.Factory.CreateRibbonGroup();
            this.buttonCheckQS = this.Factory.CreateRibbonButton();
            this.buttonJump = this.Factory.CreateRibbonButton();
            this.DProcess1 = this.Factory.CreateRibbonGroup();
            this.buttonUp = this.Factory.CreateRibbonButton();
            this.buttonDown = this.Factory.CreateRibbonButton();
            this.buttonCopy = this.Factory.CreateRibbonButton();
            this.buttonPaste = this.Factory.CreateRibbonButton();
            this.buttonUndoDP = this.Factory.CreateRibbonButton();
            this.buttonExecuteDP = this.Factory.CreateRibbonButton();
            this.DProcess2 = this.Factory.CreateRibbonGroup();
            this.checkBoxCross = this.Factory.CreateRibbonCheckBox();
            this.checkBoxList = this.Factory.CreateRibbonCheckBox();
            this.CTab1 = this.Factory.CreateRibbonGroup();
            this.buttonOptionsCT = this.Factory.CreateRibbonButton();
            this.buttonCT = this.Factory.CreateRibbonButton();
            this.buttonChart = this.Factory.CreateRibbonButton();
            this.CTab2 = this.Factory.CreateRibbonGroup();
            this.buttonStatus = this.Factory.CreateRibbonButton();
            this.separator1 = this.Factory.CreateRibbonSeparator();
            this.boxOutFormat = this.Factory.CreateRibbonBox();
            this.labelOutput = this.Factory.CreateRibbonLabel();
            this.checkBoxRows = this.Factory.CreateRibbonCheckBox();
            this.checkBoxCols = this.Factory.CreateRibbonCheckBox();
            this.Summary = this.Factory.CreateRibbonGroup();
            this.buttonOptionsSummary = this.Factory.CreateRibbonButton();
            this.buttonOutput = this.Factory.CreateRibbonButton();
            this.GT1 = this.Factory.CreateRibbonGroup();
            this.buttonExecuteGT = this.Factory.CreateRibbonButton();
            this.buttonOptionsGT = this.Factory.CreateRibbonButton();
            this.buttonAutoSettings = this.Factory.CreateRibbonButton();
            this.GT2 = this.Factory.CreateRibbonGroup();
            this.boxGT = this.Factory.CreateRibbonBox();
            this.checkBoxLevel1 = this.Factory.CreateRibbonCheckBox();
            this.checkBoxLevel5 = this.Factory.CreateRibbonCheckBox();
            this.checkBoxLevel10 = this.Factory.CreateRibbonCheckBox();
            this.labelAlert = this.Factory.CreateRibbonLabel();
            this.FA1 = this.Factory.CreateRibbonGroup();
            this.buttonExecute = this.Factory.CreateRibbonButton();
            this.FA2 = this.Factory.CreateRibbonGroup();
            this.checkBoxSort = this.Factory.CreateRibbonCheckBox();
            this.QC4.SuspendLayout();
            this.Menu.SuspendLayout();
            this.Common.SuspendLayout();
            this.QSettings1.SuspendLayout();
            this.QSettings2.SuspendLayout();
            this.DProcess1.SuspendLayout();
            this.DProcess2.SuspendLayout();
            this.CTab1.SuspendLayout();
            this.CTab2.SuspendLayout();
            this.boxOutFormat.SuspendLayout();
            this.Summary.SuspendLayout();
            this.GT1.SuspendLayout();
            this.GT2.SuspendLayout();
            this.boxGT.SuspendLayout();
            this.FA1.SuspendLayout();
            this.FA2.SuspendLayout();
            this.SuspendLayout();
            // 
            // QC4
            // 
            this.QC4.Groups.Add(this.Menu);
            this.QC4.Groups.Add(this.Common);
            this.QC4.Groups.Add(this.QSettings1);
            this.QC4.Groups.Add(this.QSettings2);
            this.QC4.Groups.Add(this.DProcess1);
            this.QC4.Groups.Add(this.DProcess2);
            this.QC4.Groups.Add(this.CTab1);
            this.QC4.Groups.Add(this.CTab2);
            this.QC4.Groups.Add(this.Summary);
            this.QC4.Groups.Add(this.GT1);
            this.QC4.Groups.Add(this.GT2);
            this.QC4.Groups.Add(this.FA1);
            this.QC4.Groups.Add(this.FA2);
            this.QC4.Label = "&QuickCross";
            this.QC4.Name = "QC4";
            // 
            // Menu
            // 
            this.Menu.Items.Add(this.buttonMenu);
            this.Menu.Label = "&Menu";
            this.Menu.Name = "Menu";
            // 
            // buttonMenu
            // 
            this.buttonMenu.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonMenu.Image = ((System.Drawing.Image)(resources.GetObject("buttonMenu.Image")));
            this.buttonMenu.Label = "Menu";
            this.buttonMenu.Name = "buttonMenu";
            this.buttonMenu.ShowImage = true;
            this.buttonMenu.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonMenu_Click);
            // 
            // Common
            // 
            this.Common.Items.Add(this.buttonInsert);
            this.Common.Items.Add(this.buttonDelete);
            this.Common.Items.Add(this.buttonCheck);
            this.Common.Label = "&Cells";
            this.Common.Name = "Common";
            // 
            // buttonInsert
            // 
            this.buttonInsert.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonInsert.Image = ((System.Drawing.Image)(resources.GetObject("buttonInsert.Image")));
            this.buttonInsert.Label = "&Insert";
            this.buttonInsert.Name = "buttonInsert";
            this.buttonInsert.ShowImage = true;
            this.buttonInsert.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonInsert_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonDelete.Image = ((System.Drawing.Image)(resources.GetObject("buttonDelete.Image")));
            this.buttonDelete.Label = "Delete";
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.ShowImage = true;
            this.buttonDelete.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonDelete_Click);
            // 
            // buttonCheck
            // 
            this.buttonCheck.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonCheck.Image = ((System.Drawing.Image)(resources.GetObject("buttonCheck.Image")));
            this.buttonCheck.Label = "Check";
            this.buttonCheck.Name = "buttonCheck";
            this.buttonCheck.ShowImage = true;
            this.buttonCheck.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonCheck_Click);
            // 
            // QSettings1
            // 
            this.QSettings1.Items.Add(this.buttonEQ);
            this.QSettings1.Items.Add(this.buttonVEM);
            this.QSettings1.Label = "&Operations";
            this.QSettings1.Name = "QSettings1";
            // 
            // buttonEQ
            // 
            this.buttonEQ.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonEQ.Image = ((System.Drawing.Image)(resources.GetObject("buttonEQ.Image")));
            this.buttonEQ.Label = "Export Questionnaire";
            this.buttonEQ.Name = "buttonEQ";
            this.buttonEQ.ShowImage = true;
            this.buttonEQ.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonEQ_Click);
            // 
            // buttonVEM
            // 
            this.buttonVEM.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonVEM.Image = ((System.Drawing.Image)(resources.GetObject("buttonVEM.Image")));
            this.buttonVEM.Label = "Variable Edit Mode";
            this.buttonVEM.Name = "buttonVEM";
            this.buttonVEM.ShowImage = true;
            this.buttonVEM.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonVEM_Click);
            // 
            // QSettings2
            // 
            this.QSettings2.Items.Add(this.buttonCheckQS);
            this.QSettings2.Items.Add(this.buttonJump);
            this.QSettings2.Label = "&Cells";
            this.QSettings2.Name = "QSettings2";
            // 
            // buttonCheckQS
            // 
            this.buttonCheckQS.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonCheckQS.Image = ((System.Drawing.Image)(resources.GetObject("buttonCheckQS.Image")));
            this.buttonCheckQS.Label = "Check";
            this.buttonCheckQS.Name = "buttonCheckQS";
            this.buttonCheckQS.ShowImage = true;
            this.buttonCheckQS.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonCheckQS_Click);
            // 
            // buttonJump
            // 
            this.buttonJump.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonJump.Image = ((System.Drawing.Image)(resources.GetObject("buttonJump.Image")));
            this.buttonJump.Label = "Jump";
            this.buttonJump.Name = "buttonJump";
            this.buttonJump.ShowImage = true;
            this.buttonJump.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonJump_Click);
            // 
            // DProcess1
            // 
            this.DProcess1.Items.Add(this.buttonUp);
            this.DProcess1.Items.Add(this.buttonDown);
            this.DProcess1.Items.Add(this.buttonCopy);
            this.DProcess1.Items.Add(this.buttonPaste);
            this.DProcess1.Items.Add(this.buttonUndoDP);
            this.DProcess1.Items.Add(this.buttonExecuteDP);
            this.DProcess1.Label = "&Operations";
            this.DProcess1.Name = "DProcess1";
            // 
            // buttonUp
            // 
            this.buttonUp.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonUp.Image = ((System.Drawing.Image)(resources.GetObject("buttonUp.Image")));
            this.buttonUp.Label = "Up";
            this.buttonUp.Name = "buttonUp";
            this.buttonUp.ShowImage = true;
            this.buttonUp.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonUp_Click);
            // 
            // buttonDown
            // 
            this.buttonDown.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonDown.Image = ((System.Drawing.Image)(resources.GetObject("buttonDown.Image")));
            this.buttonDown.Label = "Down";
            this.buttonDown.Name = "buttonDown";
            this.buttonDown.ShowImage = true;
            this.buttonDown.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonDown_Click);
            // 
            // buttonCopy
            // 
            this.buttonCopy.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonCopy.Image = ((System.Drawing.Image)(resources.GetObject("buttonCopy.Image")));
            this.buttonCopy.Label = "Copy";
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.ShowImage = true;
            this.buttonCopy.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonCopy_Click);
            // 
            // buttonPaste
            // 
            this.buttonPaste.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonPaste.Image = ((System.Drawing.Image)(resources.GetObject("buttonPaste.Image")));
            this.buttonPaste.Label = "Paste";
            this.buttonPaste.Name = "buttonPaste";
            this.buttonPaste.ShowImage = true;
            this.buttonPaste.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonPaste_Click);
            // 
            // buttonUndoDP
            // 
            this.buttonUndoDP.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonUndoDP.Image = ((System.Drawing.Image)(resources.GetObject("buttonUndoDP.Image")));
            this.buttonUndoDP.Label = "Undo";
            this.buttonUndoDP.Name = "buttonUndoDP";
            this.buttonUndoDP.ShowImage = true;
            this.buttonUndoDP.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonUndoDP_Click);
            // 
            // buttonExecuteDP
            // 
            this.buttonExecuteDP.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonExecuteDP.Image = ((System.Drawing.Image)(resources.GetObject("buttonExecuteDP.Image")));
            this.buttonExecuteDP.Label = "Execute";
            this.buttonExecuteDP.Name = "buttonExecuteDP";
            this.buttonExecuteDP.ShowImage = true;
            this.buttonExecuteDP.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonExecuteDP_Click);
            // 
            // DProcess2
            // 
            this.DProcess2.Items.Add(this.checkBoxCross);
            this.DProcess2.Items.Add(this.checkBoxList);
            this.DProcess2.Label = "&Settings";
            this.DProcess2.Name = "DProcess2";
            // 
            // checkBoxCross
            // 
            this.checkBoxCross.Label = "Check Cross";
            this.checkBoxCross.Name = "checkBoxCross";
            this.checkBoxCross.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.checkBoxCross_Click);
            // 
            // checkBoxList
            // 
            this.checkBoxList.Label = "Check List";
            this.checkBoxList.Name = "checkBoxList";
            this.checkBoxList.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.checkBoxList_Click);
            // 
            // CTab1
            // 
            this.CTab1.Items.Add(this.buttonOptionsCT);
            this.CTab1.Items.Add(this.buttonCT);
            this.CTab1.Items.Add(this.buttonChart);
            this.CTab1.Label = "&Operations";
            this.CTab1.Name = "CTab1";
            // 
            // buttonOptionsCT
            // 
            this.buttonOptionsCT.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonOptionsCT.Image = ((System.Drawing.Image)(resources.GetObject("buttonOptionsCT.Image")));
            this.buttonOptionsCT.Label = "Options";
            this.buttonOptionsCT.Name = "buttonOptionsCT";
            this.buttonOptionsCT.ShowImage = true;
            this.buttonOptionsCT.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonOptionsCT_Click);
            // 
            // buttonCT
            // 
            this.buttonCT.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonCT.Image = ((System.Drawing.Image)(resources.GetObject("buttonCT.Image")));
            this.buttonCT.Label = "Cross Tabulate";
            this.buttonCT.Name = "buttonCT";
            this.buttonCT.ShowImage = true;
            this.buttonCT.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonCT_Click);
            // 
            // buttonChart
            // 
            this.buttonChart.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonChart.Image = ((System.Drawing.Image)(resources.GetObject("buttonChart.Image")));
            this.buttonChart.Label = "Create Chart";
            this.buttonChart.Name = "buttonChart";
            this.buttonChart.ShowImage = true;
            this.buttonChart.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonChart_Click);
            // 
            // CTab2
            // 
            this.CTab2.Items.Add(this.buttonStatus);
            this.CTab2.Items.Add(this.separator1);
            this.CTab2.Items.Add(this.boxOutFormat);
            this.CTab2.Label = "&Settings";
            this.CTab2.Name = "CTab2";
            // 
            // buttonStatus
            // 
            this.buttonStatus.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonStatus.Image = ((System.Drawing.Image)(resources.GetObject("buttonStatus.Image")));
            this.buttonStatus.Label = "Status";
            this.buttonStatus.Name = "buttonStatus";
            this.buttonStatus.ShowImage = true;
            this.buttonStatus.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonStatus_Click);
            // 
            // separator1
            // 
            this.separator1.Name = "separator1";
            // 
            // boxOutFormat
            // 
            this.boxOutFormat.BoxStyle = Microsoft.Office.Tools.Ribbon.RibbonBoxStyle.Vertical;
            this.boxOutFormat.Items.Add(this.labelOutput);
            this.boxOutFormat.Items.Add(this.checkBoxRows);
            this.boxOutFormat.Items.Add(this.checkBoxCols);
            this.boxOutFormat.Name = "boxOutFormat";
            // 
            // labelOutput
            // 
            this.labelOutput.Label = "Output Format";
            this.labelOutput.Name = "labelOutput";
            // 
            // checkBoxRows
            // 
            this.checkBoxRows.Checked = true;
            this.checkBoxRows.Label = "Rows";
            this.checkBoxRows.Name = "checkBoxRows";
            this.checkBoxRows.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.checkBoxRows_Click);
            // 
            // checkBoxCols
            // 
            this.checkBoxCols.Label = "Columns";
            this.checkBoxCols.Name = "checkBoxCols";
            this.checkBoxCols.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.checkBoxCols_Click);
            // 
            // Summary
            // 
            this.Summary.Items.Add(this.buttonOptionsSummary);
            this.Summary.Items.Add(this.buttonOutput);
            this.Summary.Label = "&Operations";
            this.Summary.Name = "Summary";
            // 
            // buttonOptionsSummary
            // 
            this.buttonOptionsSummary.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonOptionsSummary.Image = ((System.Drawing.Image)(resources.GetObject("buttonOptionsSummary.Image")));
            this.buttonOptionsSummary.Label = "Options";
            this.buttonOptionsSummary.Name = "buttonOptionsSummary";
            this.buttonOptionsSummary.ShowImage = true;
            this.buttonOptionsSummary.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonOptionsSummary_Click);
            // 
            // buttonOutput
            // 
            this.buttonOutput.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonOutput.Image = ((System.Drawing.Image)(resources.GetObject("buttonOutput.Image")));
            this.buttonOutput.Label = "Execute";
            this.buttonOutput.Name = "buttonOutput";
            this.buttonOutput.ShowImage = true;
            this.buttonOutput.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonOutput_Click);
            // 
            // GT1
            // 
            this.GT1.Items.Add(this.buttonExecuteGT);
            this.GT1.Items.Add(this.buttonOptionsGT);
            this.GT1.Items.Add(this.buttonAutoSettings);
            this.GT1.Label = "&Operations";
            this.GT1.Name = "GT1";
            // 
            // buttonExecuteGT
            // 
            this.buttonExecuteGT.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonExecuteGT.Image = ((System.Drawing.Image)(resources.GetObject("buttonExecuteGT.Image")));
            this.buttonExecuteGT.Label = "Execute";
            this.buttonExecuteGT.Name = "buttonExecuteGT";
            this.buttonExecuteGT.ShowImage = true;
            this.buttonExecuteGT.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonExecuteGT_Click);
            // 
            // buttonOptionsGT
            // 
            this.buttonOptionsGT.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonOptionsGT.Image = ((System.Drawing.Image)(resources.GetObject("buttonOptionsGT.Image")));
            this.buttonOptionsGT.Label = "Options";
            this.buttonOptionsGT.Name = "buttonOptionsGT";
            this.buttonOptionsGT.ShowImage = true;
            this.buttonOptionsGT.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonOptionsGT_Click);
            // 
            // buttonAutoSettings
            // 
            this.buttonAutoSettings.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonAutoSettings.Image = ((System.Drawing.Image)(resources.GetObject("buttonAutoSettings.Image")));
            this.buttonAutoSettings.Label = "Auto Settings";
            this.buttonAutoSettings.Name = "buttonAutoSettings";
            this.buttonAutoSettings.ShowImage = true;
            this.buttonAutoSettings.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonAutoSettings_Click);
            // 
            // GT2
            // 
            this.GT2.Items.Add(this.boxGT);
            this.GT2.Items.Add(this.labelAlert);
            this.GT2.Label = "&Significance Level";
            this.GT2.Name = "GT2";
            // 
            // boxGT
            // 
            this.boxGT.BoxStyle = Microsoft.Office.Tools.Ribbon.RibbonBoxStyle.Vertical;
            this.boxGT.Items.Add(this.checkBoxLevel1);
            this.boxGT.Items.Add(this.checkBoxLevel5);
            this.boxGT.Items.Add(this.checkBoxLevel10);
            this.boxGT.Name = "boxGT";
            // 
            // checkBoxLevel1
            // 
            this.checkBoxLevel1.Label = "1%";
            this.checkBoxLevel1.Name = "checkBoxLevel1";
            this.checkBoxLevel1.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.checkBoxLevel1_Click);
            // 
            // checkBoxLevel5
            // 
            this.checkBoxLevel5.Label = "5%";
            this.checkBoxLevel5.Name = "checkBoxLevel5";
            this.checkBoxLevel5.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.checkBoxLevel5_Click);
            // 
            // checkBoxLevel10
            // 
            this.checkBoxLevel10.Label = "10%";
            this.checkBoxLevel10.Name = "checkBoxLevel10";
            this.checkBoxLevel10.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.checkBoxLevel10_Click);
            // 
            // labelAlert
            // 
            this.labelAlert.Label = "Alert";
            this.labelAlert.Name = "labelAlert";
            this.labelAlert.Visible = false;
            // 
            // FA1
            // 
            this.FA1.Items.Add(this.buttonExecute);
            this.FA1.Label = "&Operations";
            this.FA1.Name = "FA1";
            // 
            // buttonExecute
            // 
            this.buttonExecute.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonExecute.Image = ((System.Drawing.Image)(resources.GetObject("buttonExecute.Image")));
            this.buttonExecute.Label = "Execute";
            this.buttonExecute.Name = "buttonExecute";
            this.buttonExecute.ShowImage = true;
            this.buttonExecute.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonExecute_Click);
            // 
            // FA2
            // 
            this.FA2.Items.Add(this.checkBoxSort);
            this.FA2.Label = "&Settings";
            this.FA2.Name = "FA2";
            // 
            // checkBoxSort
            // 
            this.checkBoxSort.Label = "Sort additional variables in ascending order";
            this.checkBoxSort.Name = "checkBoxSort";
            // 
            // QC4Ribbon
            // 
            this.Name = "QC4Ribbon";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.QC4);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.QC4Ribbon_Load);
            this.QC4.ResumeLayout(false);
            this.QC4.PerformLayout();
            this.Menu.ResumeLayout(false);
            this.Menu.PerformLayout();
            this.Common.ResumeLayout(false);
            this.Common.PerformLayout();
            this.QSettings1.ResumeLayout(false);
            this.QSettings1.PerformLayout();
            this.QSettings2.ResumeLayout(false);
            this.QSettings2.PerformLayout();
            this.DProcess1.ResumeLayout(false);
            this.DProcess1.PerformLayout();
            this.DProcess2.ResumeLayout(false);
            this.DProcess2.PerformLayout();
            this.CTab1.ResumeLayout(false);
            this.CTab1.PerformLayout();
            this.CTab2.ResumeLayout(false);
            this.CTab2.PerformLayout();
            this.boxOutFormat.ResumeLayout(false);
            this.boxOutFormat.PerformLayout();
            this.Summary.ResumeLayout(false);
            this.Summary.PerformLayout();
            this.GT1.ResumeLayout(false);
            this.GT1.PerformLayout();
            this.GT2.ResumeLayout(false);
            this.GT2.PerformLayout();
            this.boxGT.ResumeLayout(false);
            this.boxGT.PerformLayout();
            this.FA1.ResumeLayout(false);
            this.FA1.PerformLayout();
            this.FA2.ResumeLayout(false);
            this.FA2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab QC4;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup Menu;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonMenu;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup FA1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonExecute;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox checkBoxSort;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup Common;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonInsert;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonDelete;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonCheck;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup QSettings1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonEQ;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup QSettings2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonCheckQS;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonJump;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup DProcess1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonUp;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonDown;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonCopy;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonPaste;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup DProcess2;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox checkBoxCross;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox checkBoxList;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup CTab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonOptionsCT;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonCT;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonChart;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup CTab2;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup Summary;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonOptionsSummary;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonOutput;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup GT1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonExecuteGT;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonOptionsGT;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonAutoSettings;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup GT2;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox boxGT;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox checkBoxLevel1;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox checkBoxLevel5;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox checkBoxLevel10;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonExecuteDP;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup FA2;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox boxOutFormat;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox checkBoxRows;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox checkBoxCols;
        internal Microsoft.Office.Tools.Ribbon.RibbonSeparator separator1;
        internal Microsoft.Office.Tools.Ribbon.RibbonLabel labelAlert;
        internal Microsoft.Office.Tools.Ribbon.RibbonLabel labelOutput;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonStatus;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonVEM;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonUndoDP;
    }

    partial class ThisRibbonCollection
    {
        internal QC4Ribbon qc4Ribbon
        {
            get { return this.GetRibbon<QC4Ribbon>(); }
        }
    }
}
