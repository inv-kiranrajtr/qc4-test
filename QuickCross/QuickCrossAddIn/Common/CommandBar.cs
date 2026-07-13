using QC4Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;

namespace ExcelAddIn.Common


{
    class CommandBar
    {
        #region main
        internal static void AddMenuItem(object sh=null, Excel.Range target=null, bool cancel=false)
        {
            ExcelUtil.ResetCellMenu();
            ExcelUtil.ResetCellMenu("Row");
            ExcelUtil.ResetCellMenu("Column");
            Excel.Worksheet sheet = (Excel.Worksheet)sh;

            if (target.Worksheet.CodeName == Constants.SheetType.sh_QuesSetting) // sample code: if the signle cell contains 'abc'
            {
				QSDelete(target.Worksheet, target);
                //ExcelUtil.GetNewMenuItem(AddinResource.QS_QUESTION_CONFIGURATION_CHECK).Click += new Office._CommandBarButtonEvents_ClickEventHandler(ConfigCheckMenuItemClick);
                //ExcelUtil.GetNewMenuItem(Definitions.VariableEditMode ? AddinResource.QS_STOP_VARIABLE_EDIT_MODE : AddinResource.QS_START_VARIABLE_EDIT_MODE).Click += new Office._CommandBarButtonEvents_ClickEventHandler(VariableEditMenuItemClick);
                ExcelUtil.GetNewMenuItem(AddinResource.QS_DATA_INTEGRITY_CHECK).Click += new Office._CommandBarButtonEvents_ClickEventHandler(DbCheckMenuItemClick);
            }
            else if (target.Worksheet.CodeName == Constants.SheetType.GTCounting)
            {
                ReplaceInsertAndDeleteGT(sheet);
                ExcelUtil.GetNewMenuItem("Validate").Click += new Office._CommandBarButtonEvents_ClickEventHandler(ValidateGT);
            }
            else if (target.Worksheet.CodeName == Constants.SheetType.sh_CrossCounting)
            {
                ReplaceInsertAndDeleteCross(sheet);
                ExcelUtil.GetNewMenuItem("Validate").Click += new Office._CommandBarButtonEvents_ClickEventHandler(ValidateCRTab);
            }
            else if (target.Worksheet.CodeName == Constants.SheetType.sh_SummaryList)
            {
                ReplaceInsertAndDeleteSL(sheet);
                ExcelUtil.GetNewMenuItem("Validate").Click += new Office._CommandBarButtonEvents_ClickEventHandler(ValidateSL);
            }

            //if (target.Cells.Count == 1)   // sample code: if only a single cell is selected
            //{
            //}

        }
        #endregion

        #region functions
        
		private static void ReplaceInsertAndDelete()
		{
			//row insert and delete
			if (Definitions.VariableEditMode)
			{
				DisableCommandBar(296);
				DisableCommandBar(293);
				DisableCommandBar(295);
				DisableCommandBar(292);
			}
			else
			{
				ExcelUtil.GetNewMenuItem("&Insert", DeleteCommandBar(296) + 1, "Row").Click += new Office._CommandBarButtonEvents_ClickEventHandler(InsertClick);
				ExcelUtil.GetNewMenuItem("&Delete", DeleteCommandBar(293), "Row").Click += new Office._CommandBarButtonEvents_ClickEventHandler(DeleteClick);
				ExcelUtil.GetNewMenuItem("&Insert", DeleteCommandBar(295)).Click += new Office._CommandBarButtonEvents_ClickEventHandler(InsertClick);
				ExcelUtil.GetNewMenuItem("&Delete", DeleteCommandBar(292)).Click += new Office._CommandBarButtonEvents_ClickEventHandler(DeleteClick);
			}
			//ReplaceCommandBar(296,"Row").Click += new Office._CommandBarButtonEvents_ClickEventHandler(InsertClick); 
			//ReplaceCommandBar(293, "Row").Click += new Office._CommandBarButtonEvents_ClickEventHandler(DeleteClick);
			//cell insert and delete
			
			
			// to disable colum insert and delete
			DisableCommandBar(294);
			DisableCommandBar(297);
			//clear content
		//	DisableCommandBar(293);
		//	DisableCommandBar(296);

        }

        private static void ReplaceInsertAndDeleteCross(Excel.Worksheet sheet)
        {
            //row insert and delete
            ExcelUtil.GetNewMenuItem("&Insert", DeleteCommandBar(296) + 1, "Row").Click += (Office.CommandBarButton Ctrl, ref bool CancelDefault) => InsertClickCr(Ctrl, ref CancelDefault, sheet);
            ExcelUtil.GetNewMenuItem("&Delete", DeleteCommandBar(293), "Row").Click += (Office.CommandBarButton Ctrl, ref bool CancelDefault) => DeleteClickCr(Ctrl, ref CancelDefault, sheet);
            ExcelUtil.GetNewMenuItem("&Insert", DeleteCommandBar(295)).Click += (Office.CommandBarButton Ctrl, ref bool CancelDefault) => InsertClickCr(Ctrl, ref CancelDefault, sheet);
            ExcelUtil.GetNewMenuItem("&Delete", DeleteCommandBar(292)).Click += (Office.CommandBarButton Ctrl, ref bool CancelDefault) => DeleteClickCr(Ctrl, ref CancelDefault, sheet);
            ExcelUtil.GetNewMenuItem("&Insert", DeleteCommandBar(297) + 1, "Column").Click += (Office.CommandBarButton Ctrl, ref bool CancelDefault) => InsertClickCr(Ctrl, ref CancelDefault, sheet);
            ExcelUtil.GetNewMenuItem("&Delete", DeleteCommandBar(294), "Column").Click += (Office.CommandBarButton Ctrl, ref bool CancelDefault) => DeleteClickCr(Ctrl, ref CancelDefault, sheet);
        }

        private static void ReplaceInsertAndDeleteSL(Excel.Worksheet sheet)
        {
            //row insert and delete
            ExcelUtil.GetNewMenuItem("&Insert", DeleteCommandBar(296) + 1, "Row").Click += (Office.CommandBarButton Ctrl, ref bool CancelDefault) => InsertClickSL(Ctrl, ref CancelDefault, sheet);
            ExcelUtil.GetNewMenuItem("&Delete", DeleteCommandBar(293), "Row").Click += (Office.CommandBarButton Ctrl, ref bool CancelDefault) => DeleteClickSL(Ctrl, ref CancelDefault, sheet);
            ExcelUtil.GetNewMenuItem("&Insert", DeleteCommandBar(295)).Click += (Office.CommandBarButton Ctrl, ref bool CancelDefault) => InsertClickSL(Ctrl, ref CancelDefault, sheet);
            ExcelUtil.GetNewMenuItem("&Delete", DeleteCommandBar(292)).Click += (Office.CommandBarButton Ctrl, ref bool CancelDefault) => DeleteClickSL(Ctrl, ref CancelDefault, sheet);
            ExcelUtil.GetNewMenuItem("&Insert", DeleteCommandBar(297) + 1, "Column").Click += (Office.CommandBarButton Ctrl, ref bool CancelDefault) => InsertClickSL(Ctrl, ref CancelDefault, sheet);
            ExcelUtil.GetNewMenuItem("&Delete", DeleteCommandBar(294), "Column").Click += (Office.CommandBarButton Ctrl, ref bool CancelDefault) => DeleteClickSL(Ctrl, ref CancelDefault, sheet);
        }


        private static void ReplaceInsertAndDeleteGT(Excel.Worksheet sheet)
        {
            ExcelUtil.GetNewMenuItem("&Insert", DeleteCommandBar(296) + 1, "Row").Click += (Office.CommandBarButton Ctrl, ref bool CancelDefault) => InsertClickGt(Ctrl, ref CancelDefault, sheet);
            ExcelUtil.GetNewMenuItem("&Delete", DeleteCommandBar(293), "Row").Click += (Office.CommandBarButton Ctrl, ref bool CancelDefault) => DeleteClickGt(Ctrl, ref CancelDefault, sheet);
            ExcelUtil.GetNewMenuItem("&Insert", DeleteCommandBar(295)).Click += (Office.CommandBarButton Ctrl, ref bool CancelDefault) => InsertClickGt(Ctrl, ref CancelDefault, sheet);
            ExcelUtil.GetNewMenuItem("&Delete", DeleteCommandBar(292)).Click += (Office.CommandBarButton Ctrl, ref bool CancelDefault) => DeleteClickGt(Ctrl, ref CancelDefault, sheet);
            DisableCommandBar(294);
            DisableCommandBar(297);
        }

        private static Office.CommandBarButton ReplaceCommandBar(int id, string index = "Cell")
        {
            Office.CommandBarControl cc = ExcelUtil.GetContextMenus().FindControl(Office.MsoControlType.msoControlButton, id);
            Office.CommandBarButton cmd = null;

            if (cc != null)
            {
                cmd = ExcelUtil.GetNewMenuItem(cc.Caption, cc.Index, index);
                cc.Delete();
            }
            return cmd;
        }

        private static int DeleteCommandBar(int id)
        {
            int index = -1;
            Office.CommandBarControl cc = ExcelUtil.GetContextMenus().FindControl(Office.MsoControlType.msoControlButton, id);
            if (cc != null)
            {
                index = cc.Index;
                cc.Delete();
            }
            return index;
        }

        private static void DisableCommandBar(int id, bool enable = true)
        {
            Office.CommandBarControl cc = ExcelUtil.GetContextMenus().FindControl(Office.MsoControlType.msoControlButton, id);
            if (cc != null)
            {
                cc.Enabled = !enable;
            }
        }

        private static void RemoveCutCopyPasteMenuItems()
        {
            Office.CommandBar contextMenu = ExcelUtil.GetCellContextMenu("Row");
            string kk = "";
            for (int i = contextMenu.Controls.Count; i > 0; i--)
            {
                Office.CommandBarControl control = contextMenu.Controls[i];
                kk += control.Id + " -- " + control.Caption + " -- " + control.Index + " -- " + control.Tag + "/";
                //if (control.Caption == "Cu&t") control.Delete();  // Sample code: remove cut menu item
                //else if (control.Caption == "&Copy") control.Delete();  // Sample code: remove copy menu item
                //else if (control.accDescription.Contains("Paste")) control.Enabled = false; // Sample code: remove any paste menu items
            }
            string caption = "";
        }

		private static void QSDelete(Excel.Worksheet sheet, Excel.Range range)
		{
			//ExcelUtil.GetNewMenuItem("&Insert", DeleteCommandBar(296) + 1, "Row").Click += (Office.CommandBarButton Ctrl, ref bool CancelDefault) => InsertClickGt(Ctrl, ref CancelDefault, sheet);
			ExcelUtil.GetNewMenuItem("&Delete", DeleteCommandBar(293), "Row").Click += (Office.CommandBarButton Ctrl, ref bool CancelDefault) => DeleteClickQS(Ctrl, ref CancelDefault, sheet, range);
			//ExcelUtil.GetNewMenuItem("&Insert", DeleteCommandBar(295)).Click += (Office.CommandBarButton Ctrl, ref bool CancelDefault) => InsertClickGt(Ctrl, ref CancelDefault, sheet);
			ExcelUtil.GetNewMenuItem("&Delete", DeleteCommandBar(292)).Click += (Office.CommandBarButton Ctrl, ref bool CancelDefault) => DeleteClickQS(Ctrl, ref CancelDefault, sheet, range);
			DisableCommandBar(294);
			//DisableCommandBar(297);
		}


        #endregion

        #region events

        private static void ConfigCheckMenuItemClick(Microsoft.Office.Core.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            Change.QsValidateClick();
        }

        private static void VariableEditMenuItemClick(Microsoft.Office.Core.CommandBarButton Ctrl, ref bool CancelDefault)
        {
           // QuestionSettingsUtil.VariableEditModeChange();
        }

        private static void DbCheckMenuItemClick(Microsoft.Office.Core.CommandBarButton Ctrl, ref bool CancelDefault)
        {
			ReturnClass rs = new QSValidate(Globals.ThisAddIn.Application.ActiveWorkbook).QuestionConfigurationCheck();
			if (!rs.Result)
			{
				Excel.Range r = (Excel.Range)rs.Value;
				r.Select();
				MessageDialog.ErrorOk(rs.Msg);
				return;
			}

			rs = new QS.IntegrityCheck(Globals.ThisAddIn.Application.ActiveWorkbook).Check(out List<QuestionSettings> l1
				, out List<QuestionSettings> l2, out List<QuestionSettings> l3, out List<QuestionSettings> l4, out List<QuestionSettings> l5, false);

			if (rs.Result)
            {
                MessageDialog.Info(AddinResource.QS_INEGRITY_CHECK_SUCCESS);
            }
            else
            {
				if (rs.Value != null)
				{
					Excel.Range r = (Excel.Range)rs.Value;
					r.Select();
				}
				MessageDialog.ErrorOk(rs.Msg);
			}
        }

        private static void ValidateGT(Microsoft.Office.Core.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            Change.ValidateGT();
        }

        private static void ValidateCRTab(Microsoft.Office.Core.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            Change.ValidateCRTab(Ctrl, ref CancelDefault);
        }

        private static void ValidateSL(Microsoft.Office.Core.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            SLChange.ValidateSL(Ctrl, ref CancelDefault);
        }

        private static void InsertClick(Microsoft.Office.Core.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            Change.QSInsertDeleteClick(ExcelUtil.GetWorkSheetByCodeName(Constants.SheetCodeName.QuestionSetting));
        }

        private static void DeleteClick(Microsoft.Office.Core.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            Change.QSInsertDeleteClick(ExcelUtil.GetWorkSheetByCodeName(Constants.SheetCodeName.QuestionSetting), true);
        }

        private static void DeleteClickCr(Office.CommandBarButton Ctrl, ref bool CancelDefault, Excel.Worksheet sht)
        {
            CrossChange.crossInsertDel(sht, true);
        }

        private static void InsertClickCr(Office.CommandBarButton Ctrl, ref bool CancelDefault, Excel.Worksheet sht)
        {
            CrossChange.crossInsertDel(sht);
        }

        private static void InsertClickGt(Office.CommandBarButton ctrl, ref bool cancelDefault, Excel.Worksheet sheet)
        {
            Change.InsertDeleteClick(sheet);
        }

        private static void DeleteClickGt(Office.CommandBarButton ctrl, ref bool cancelDefault, Excel.Worksheet sheet)
        {
            Change.InsertDeleteClick(sheet, true);
        }

		private static void DeleteClickQS(Office.CommandBarButton ctrl, ref bool cancelDefault, Excel.Worksheet sheet, Excel.Range range)
		{
			QS.CommandBar.DeleteQSRow(range);
		}

		private static void DeleteClickSL(Office.CommandBarButton Ctrl, ref bool CancelDefault, Excel.Worksheet sht)
        {
            SLChange.SLInsertDel(sht, true);
        }

        private static void InsertClickSL(Office.CommandBarButton Ctrl, ref bool CancelDefault, Excel.Worksheet sht)
        {
            SLChange.SLInsertDel(sht);
        }
        private static void FAExecClick(Office.CommandBarButton Ctrl, ref bool CancelDefault, Excel.Worksheet sht)
        {
             FAList.FAExecClick(sht); 
        }
        #endregion
    }
}
