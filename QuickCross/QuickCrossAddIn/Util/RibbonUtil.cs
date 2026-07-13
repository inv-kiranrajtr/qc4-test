using QC4Common.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;


namespace ExcelAddIn.Util
{
	public class RibbonUtil
	{
		public static bool IsRibbonEnabled = true;
        /// <summary>
        /// Enabling or disabling the Ribbon button
        /// </summary>
        /// <param name="isEnabled">bool value that represent enable or disable</param>
        /// <param name="sheet">Excel Worksheet</param>
		public static void EnableRibbon(bool isEnabled, Excel.Worksheet sheet = null)
		{
    
            if (IsRibbonEnabled == isEnabled)
            {
                return;
            }
            
                IsRibbonEnabled = isEnabled;

            try
            {
               if (sheet == null)
                {
                    sheet = (Excel.Worksheet)Globals.ThisAddIn.Application.ActiveSheet;
                }
                Globals.Ribbons.qc4Ribbon.buttonMenu.Enabled = isEnabled;
                if (sheet.CodeName == Constants.SheetType.sh_QuesSetting)
                {
                    if (!Common.Definitions.VariableEditMode)
                    {
                        Globals.Ribbons.qc4Ribbon.buttonMenu.Enabled = isEnabled;
                        Globals.Ribbons.qc4Ribbon.buttonEQ.Enabled = isEnabled;
                        Globals.Ribbons.qc4Ribbon.buttonCheckQS.Enabled = isEnabled;
                    }
                    Globals.Ribbons.qc4Ribbon.buttonMenu.Enabled = isEnabled;
                    Globals.Ribbons.qc4Ribbon.buttonVEM.Enabled = isEnabled;
                    Globals.Ribbons.qc4Ribbon.buttonJump.Enabled = isEnabled;
                    if (Common.Definitions.VariableEditMode)
                    {
                        Globals.Ribbons.qc4Ribbon.buttonMenu.Enabled = false;
                    }
                }
                else if (!Common.Definitions.VariableEditMode)
                {
                    if (sheet.CodeName == Constants.SheetType.sh_CrossCounting)
                    {
                        Globals.Ribbons.qc4Ribbon.buttonInsert.Enabled = isEnabled;
                        Globals.Ribbons.qc4Ribbon.buttonDelete.Enabled = isEnabled;
                        Globals.Ribbons.qc4Ribbon.buttonCheck.Enabled = isEnabled;
                        Globals.Ribbons.qc4Ribbon.buttonOptionsCT.Enabled = isEnabled;
                        Globals.Ribbons.qc4Ribbon.buttonCT.Enabled = isEnabled;
                        Globals.Ribbons.qc4Ribbon.buttonChart.Enabled = isEnabled;
                        Globals.Ribbons.qc4Ribbon.buttonStatus.Enabled = isEnabled;
                    }
                    else if (sheet.CodeName == Constants.SheetCodeName.DataProcess)
                    {
                        Globals.Ribbons.qc4Ribbon.buttonInsert.Enabled = isEnabled;
                        Globals.Ribbons.qc4Ribbon.buttonDelete.Enabled = isEnabled;
                        Globals.Ribbons.qc4Ribbon.buttonCheck.Enabled = isEnabled;
                        Globals.Ribbons.qc4Ribbon.buttonUp.Enabled = isEnabled;
                        Globals.Ribbons.qc4Ribbon.buttonDown.Enabled = isEnabled;
                        Globals.Ribbons.qc4Ribbon.buttonCopy.Enabled = isEnabled;
                        Globals.Ribbons.qc4Ribbon.buttonPaste.Enabled = isEnabled;
                        Globals.Ribbons.qc4Ribbon.buttonExecuteDP.Enabled = isEnabled;
                        Globals.Ribbons.qc4Ribbon.checkBoxCross.Enabled = isEnabled;
                        Globals.Ribbons.qc4Ribbon.checkBoxList.Enabled = isEnabled;
                        Excel.Worksheet ws = QC4Common.Util.ExcelUtil.GetWorkSheetBySheetName(sheet.Application.ActiveWorkbook, Constants.SheetType.sh_Data01 + "(Processed)");
                        if (ws == null)
                        {
                            Globals.Ribbons.qc4Ribbon.buttonUndoDP.Enabled = false;
                        }
                        else
                        {
                            Globals.Ribbons.qc4Ribbon.buttonUndoDP.Enabled = true;
                        }
                    }
                    else if (sheet.CodeName == Constants.SheetType.sh_SummaryList)
                    {
                        Globals.Ribbons.qc4Ribbon.buttonInsert.Enabled = isEnabled;
                        Globals.Ribbons.qc4Ribbon.buttonDelete.Enabled = isEnabled;
                        Globals.Ribbons.qc4Ribbon.buttonCheck.Enabled = isEnabled;
                        Globals.Ribbons.qc4Ribbon.buttonOptionsSummary.Enabled = isEnabled;
                        Globals.Ribbons.qc4Ribbon.buttonOutput.Enabled = isEnabled;
                    }
                    else if (sheet.CodeName == Constants.SheetType.GTCounting)
                    {
                        Globals.Ribbons.qc4Ribbon.buttonInsert.Enabled = isEnabled;
                        Globals.Ribbons.qc4Ribbon.buttonDelete.Enabled = isEnabled;
                        Globals.Ribbons.qc4Ribbon.buttonCheck.Enabled = isEnabled;
                        Globals.Ribbons.qc4Ribbon.buttonExecuteGT.Enabled = isEnabled;
                        Globals.Ribbons.qc4Ribbon.buttonOptionsGT.Enabled = isEnabled;
                        Globals.Ribbons.qc4Ribbon.buttonAutoSettings.Enabled = isEnabled;
                        Globals.Ribbons.qc4Ribbon.checkBoxLevel1.Enabled = isEnabled;
                        Globals.Ribbons.qc4Ribbon.checkBoxLevel5.Enabled = isEnabled;
                        Globals.Ribbons.qc4Ribbon.checkBoxLevel10.Enabled = isEnabled;
                    }
                    else if (sheet.CodeName == Constants.SheetType.SH_FAList)
                    {
                        Globals.Ribbons.qc4Ribbon.buttonExecute.Enabled = isEnabled;
                        Globals.Ribbons.qc4Ribbon.checkBoxSort.Enabled = isEnabled;
                    }
                   
                }
                Globals.Ribbons.qc4Ribbon.RibbonUI.ActivateTab("QC4");
            }
            catch { }
		}

        public static void RibbonChange(Excel.Worksheet sheet, string codeName = "")
        {
            try
            {
				if (sheet != null)
				{
					codeName = sheet.CodeName;
				}
				HideQC4Groups();
                Globals.Ribbons.qc4Ribbon.buttonMenu.Enabled = true;
                if (codeName == Constants.SheetType.sh_QuesSetting)
                {
                    if (Common.Definitions.VariableEditMode)
                    {
                        Globals.Ribbons.qc4Ribbon.QSettings1.Visible = true;
                        Globals.Ribbons.qc4Ribbon.QSettings2.Visible = true;
                        Globals.Ribbons.qc4Ribbon.buttonEQ.Enabled = true;
                        Globals.Ribbons.qc4Ribbon.buttonVEM.Enabled = true;
                        Globals.Ribbons.qc4Ribbon.buttonCheckQS.Enabled = true;
                        Globals.Ribbons.qc4Ribbon.buttonJump.Enabled = true;
                        Globals.Ribbons.qc4Ribbon.buttonVEM.Label = AddinResource.QS_STOP_VARIABLE_EDIT_MODE;
                        Globals.Ribbons.qc4Ribbon.buttonVEM.Image = AddinResource.edit_stop_hover;
                        Globals.Ribbons.qc4Ribbon.buttonEQ.Enabled = false;
                        Globals.Ribbons.qc4Ribbon.buttonMenu.Enabled = false;
                        Globals.Ribbons.qc4Ribbon.buttonCheckQS.Enabled = false;
                    }
                    else
                    {
                        Globals.Ribbons.qc4Ribbon.QSettings1.Visible = true;
                        Globals.Ribbons.qc4Ribbon.QSettings2.Visible = true;
                        Globals.Ribbons.qc4Ribbon.buttonEQ.Enabled = true;
                        Globals.Ribbons.qc4Ribbon.buttonVEM.Enabled = true;
                        Globals.Ribbons.qc4Ribbon.buttonCheckQS.Enabled = true;
                        Globals.Ribbons.qc4Ribbon.buttonJump.Enabled = true;

                    }
                }
                else if (codeName == Constants.SheetType.sh_CrossCounting)
                {
                    Globals.Ribbons.qc4Ribbon.Common.Visible = true;
                    Globals.Ribbons.qc4Ribbon.CTab1.Visible = true;
                    Globals.Ribbons.qc4Ribbon.CTab2.Visible = true;
                    Globals.Ribbons.qc4Ribbon.buttonInsert.Enabled = true;
                    Globals.Ribbons.qc4Ribbon.buttonDelete.Enabled = true;
                    Globals.Ribbons.qc4Ribbon.buttonCheck.Enabled = true;
                    Globals.Ribbons.qc4Ribbon.buttonOptionsCT.Enabled = true;
                    Globals.Ribbons.qc4Ribbon.buttonCT.Enabled = true;
                    Globals.Ribbons.qc4Ribbon.buttonChart.Enabled = true;
                    Globals.Ribbons.qc4Ribbon.buttonStatus.Enabled = true;
                }
                else if (codeName == Constants.SheetCodeName.DataProcess)
                {
                    Globals.Ribbons.qc4Ribbon.Common.Visible = true;
                    Globals.Ribbons.qc4Ribbon.DProcess1.Visible = true;
                    Globals.Ribbons.qc4Ribbon.DProcess2.Visible = true;
                    Globals.Ribbons.qc4Ribbon.buttonInsert.Enabled = true;
                    Globals.Ribbons.qc4Ribbon.buttonDelete.Enabled = true;
                    Globals.Ribbons.qc4Ribbon.buttonCheck.Enabled = true;
                    Globals.Ribbons.qc4Ribbon.buttonUp.Enabled = true;
                    Globals.Ribbons.qc4Ribbon.buttonDown.Enabled = true;
                    Globals.Ribbons.qc4Ribbon.buttonCopy.Enabled = true;
                    Globals.Ribbons.qc4Ribbon.buttonPaste.Enabled = true;
                    Globals.Ribbons.qc4Ribbon.buttonExecuteDP.Enabled = true;
                    Globals.Ribbons.qc4Ribbon.checkBoxCross.Enabled = true;
                    Globals.Ribbons.qc4Ribbon.checkBoxList.Enabled = true;
                    Excel.Worksheet ws = QC4Common.Util.ExcelUtil.GetWorkSheetBySheetName(sheet.Application.ActiveWorkbook, Constants.SheetType.sh_Data01 + "(Processed)");
                    if (ws == null)
                    {
                        Globals.Ribbons.qc4Ribbon.buttonUndoDP.Enabled = false;
                    }
                    else
                    {
                        Globals.Ribbons.qc4Ribbon.buttonUndoDP.Enabled = true;
                    }
                }
                else if (codeName == Constants.SheetType.sh_SummaryList)
                {
                    Globals.Ribbons.qc4Ribbon.Common.Visible = true;
                    Globals.Ribbons.qc4Ribbon.Summary.Visible = true;
                    Globals.Ribbons.qc4Ribbon.buttonInsert.Enabled = true;
                    Globals.Ribbons.qc4Ribbon.buttonDelete.Enabled = true;
                    Globals.Ribbons.qc4Ribbon.buttonCheck.Enabled = true;
                    Globals.Ribbons.qc4Ribbon.buttonOptionsSummary.Enabled = true;
                    Globals.Ribbons.qc4Ribbon.buttonOutput.Enabled = true;
                }
                else if (codeName == Constants.SheetType.GTCounting)
                {
                    Globals.Ribbons.qc4Ribbon.Common.Visible = true;
                    Globals.Ribbons.qc4Ribbon.GT1.Visible = true;
                    Globals.Ribbons.qc4Ribbon.GT2.Visible = true;
                    Globals.Ribbons.qc4Ribbon.buttonInsert.Enabled = true;
                    Globals.Ribbons.qc4Ribbon.buttonDelete.Enabled = true;
                    Globals.Ribbons.qc4Ribbon.buttonCheck.Enabled = true;
                    Globals.Ribbons.qc4Ribbon.buttonExecuteGT.Enabled = true;
                    Globals.Ribbons.qc4Ribbon.buttonOptionsGT.Enabled = true;
                    Globals.Ribbons.qc4Ribbon.buttonAutoSettings.Enabled = true;
                    Globals.Ribbons.qc4Ribbon.checkBoxLevel1.Enabled = true;
                    Globals.Ribbons.qc4Ribbon.checkBoxLevel5.Enabled = true;
                    Globals.Ribbons.qc4Ribbon.checkBoxLevel10.Enabled = true;
                }
                else if (codeName == Constants.SheetType.SH_FAList)
                {
                    Globals.Ribbons.qc4Ribbon.FA1.Visible = true;
                    Globals.Ribbons.qc4Ribbon.FA2.Visible = true;
                    Globals.Ribbons.qc4Ribbon.buttonExecute.Enabled = true;
                    Globals.Ribbons.qc4Ribbon.checkBoxSort.Enabled = true;
                }

                //Util.RibbonUtil.EnableRibbon(true, sheet);
                if(Globals.Ribbons.qc4Ribbon.RibbonUI != null)
                    Globals.Ribbons.qc4Ribbon.RibbonUI.ActivateTab("QC4");
            }
            catch { }
        }

        public static void HideQC4Groups()
        {
            try
            {
                Globals.Ribbons.qc4Ribbon.Common.Visible = false;
                Globals.Ribbons.qc4Ribbon.CTab1.Visible = false;
                Globals.Ribbons.qc4Ribbon.CTab2.Visible = false;
                Globals.Ribbons.qc4Ribbon.DProcess1.Visible = false;
                Globals.Ribbons.qc4Ribbon.DProcess2.Visible = false;
                Globals.Ribbons.qc4Ribbon.FA1.Visible = false;
                Globals.Ribbons.qc4Ribbon.FA2.Visible = false;
                Globals.Ribbons.qc4Ribbon.GT1.Visible = false;
                Globals.Ribbons.qc4Ribbon.GT2.Visible = false;
                Globals.Ribbons.qc4Ribbon.QSettings1.Visible = false;
                Globals.Ribbons.qc4Ribbon.QSettings2.Visible = false;
                Globals.Ribbons.qc4Ribbon.Summary.Visible = false;
                Globals.Ribbons.qc4Ribbon.Menu.Visible = true;
            }
            catch { }
        }

        public static void GetCheckboxStatus(Excel.Worksheet sheet)
        {
            Excel.Worksheet settingssheet = QC4Common.Common.ExcelUtilForAddIn.GetWorkSheetByCodeName(sheet.Application.ActiveWorkbook,Constants.SheetCodeName.Setting);
            Excel.Range cell = settingssheet.Cells[Constants.PRO_DP.PRO_CCross_CList_Row, Constants.PRO_DP.PRO_CCross_CList_Column];
            if (cell.Text == Convert.ToString(Constants.DP_Checkbox_Value.BothUnchecked))
            {
                Globals.Ribbons.qc4Ribbon.checkBoxCross.Checked = false;
                Globals.Ribbons.qc4Ribbon.checkBoxList.Checked = false;
            }
            else if (cell.Text == Convert.ToString(Constants.DP_Checkbox_Value.OnlyCheckCrossChecked))
            {
                Globals.Ribbons.qc4Ribbon.checkBoxCross.Checked = true;
                Globals.Ribbons.qc4Ribbon.checkBoxList.Checked = false;
            }
            else if (cell.Text == Convert.ToString(Constants.DP_Checkbox_Value.OnlyCheckListChecked))
            {
                Globals.Ribbons.qc4Ribbon.checkBoxCross.Checked = false;
                Globals.Ribbons.qc4Ribbon.checkBoxList.Checked = true;
            }
            else if (cell.Text == Convert.ToString(Constants.DP_Checkbox_Value.BothChecked))
            {
                Globals.Ribbons.qc4Ribbon.checkBoxCross.Checked = true;
                Globals.Ribbons.qc4Ribbon.checkBoxList.Checked = true;
            }
        }

        public static void GetCrossCheckedBoxStatus(Excel.Worksheet sheet)
        {
            Excel.Worksheet settingssheet = QC4Common.Common.ExcelUtilForAddIn.GetWorkSheetByCodeName(sheet.Application.ActiveWorkbook, Constants.SheetCodeName.Setting);
            Excel.Range cell = settingssheet.Cells[QC4Common.Common.Constants.Cross_TableOrientation.PRO_Cross_TableOrientation_Row, QC4Common.Common.Constants.Cross_TableOrientation.PRO_Cross_TableOrientation_Column];
            string val = Convert.ToString(cell.Value);
            if (val == "0")
            {
                Globals.Ribbons.qc4Ribbon.checkBoxRows.Checked = true;
                Globals.Ribbons.qc4Ribbon.checkBoxCols.Checked = false;
            }
            else if (val == "1")
            {
                Globals.Ribbons.qc4Ribbon.checkBoxRows.Checked = false;
                Globals.Ribbons.qc4Ribbon.checkBoxCols.Checked = true;
            }
            else
            {
                //Excel.Worksheet settngSheet = ExcelAddIn.Common.ExcelUtil.GetWorksheetByCodeName(Constants.SheetCodeName.Setting);
                //Excel.Range cellno = settingssheet.Cells[QC4Common.Common.Constants.Cross_TableOrientation.PRO_Cross_TableOrientation_Row, QC4Common.Common.Constants.Cross_TableOrientation.PRO_Cross_TableOrientation_Column];
                if (QC4Common.Common.Constants.GlobalMode.Split(',')[0] == "ja-JP")
                {
                    //cellno.Value = 0;
                    Globals.Ribbons.qc4Ribbon.checkBoxRows.Checked = true;
                    Globals.Ribbons.qc4Ribbon.checkBoxCols.Checked = false;
                }
                else
                {
                    //cellno.Value = 1;
                    Globals.Ribbons.qc4Ribbon.checkBoxRows.Checked = false;
                    Globals.Ribbons.qc4Ribbon.checkBoxCols.Checked = true;
                }
            }

        }
    }
}
