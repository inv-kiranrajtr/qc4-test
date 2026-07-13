using Microsoft.Vbe.Interop.Forms;
using Microsoft.VisualBasic.CompilerServices;
using Qc4Launcher.Forms;
using Qc4Launcher.Logic;
using Qc4Launcher.Logic.Gross_Tabulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Threading;
using Excel = Microsoft.Office.Interop.Excel;

namespace Qc4Launcher.Util
{
	class MenuEvent
	{
		private static CommandButton BtnMenu;

		private const string MenuButton = "Command_Menu";

        private static CommandButton BtnCrosProcess;
        private static CommandButton BtnCrosOption;
        private const string CrosProcessBtn = "Command_Cr_Exec";
        private const string CrosOptionBtn = "Command_Cr_Option";

        private const string GTProcessBtn = "Command_Gt_Exec";
        private static CommandButton BtnGTProcess;

        internal static void SetMenuEvent(Excel.Workbook workBook, MainWindow window)
		{

            foreach (Excel.Worksheet workSheet in workBook.Worksheets)
			{
				if (workSheet.CodeName == Constants.SheetCodeName.QuestionSetting)
				{
					foreach (Excel.OLEObject oleObject in workSheet.OLEObjects())
					{
						if (oleObject.Name == MenuButton)
						{
							//BtnMenu = (CommandButton)NewLateBinding.LateGet(workSheet, null, oleObject.Name, null, null, null, null);
							//BtnMenu.Click += () => MenuClick(workSheet,workBook,window);
						}
					}
				}

                if (workSheet.CodeName == Constants.SheetCodeName.CrossTabulation)
                {
                    foreach (Excel.OLEObject oleObject in workSheet.OLEObjects())
                    {
                        if (oleObject.Name == CrosProcessBtn)
                        {
                           // BtnMenu = (CommandButton)NewLateBinding.LateGet(workSheet, null, oleObject.Name, null, null, null, null);
                          //  BtnMenu.Click += () => CrossExec(workBook, workSheet);
                            //oleObject.Delete();
                        }
                    }
                }

                // Gross Tabulation (GT)
                if (workSheet.CodeName == Constants.SheetCodeName.GTTabulation)
                {
                    foreach (Excel.OLEObject oleObject in workSheet.OLEObjects())
                    {
                        if (oleObject.Name == GTProcessBtn)
                        {
                            //BtnMenu = (CommandButton)NewLateBinding.LateGet(workSheet, null, oleObject.Name, null, null, null, null);
                            //BtnMenu.Click += () => CrossExec(workBook, workSheet);
                            //oleObject.Delete();
                        }
                    }
                }

            }
			
		}

		internal static void MenuClick(Excel.Worksheet sheet, Excel.Workbook book,MainWindow window)
		{

			//Excel.Worksheet sSheet = ExcelUtil.GetWorkSheetByCodeName(book, Constants.SheetCodeName.Setting);
			//sSheet.get_Range(Constants.Setting.MODE_CELL).Value = Constants.Setting.MODE_STD;
			//book.Application.Visible = false;
			//window.EnableWindow();
		}

        private static void CrossOption(Excel.Workbook workBook, Excel.Worksheet workSheet)
        {
            MainWindow window = new MainWindow();
            //NativeWindow xlMain = new NativeWindow();
            //xlMain.AssignHandle(new IntPtr(workBook.Application.Hwnd));
            CrossTabulation ct = new CrossTabulation(window, workBook, "tmp");
            WindowInteropHelper wih = new WindowInteropHelper(ct);
            wih.Owner = new IntPtr(workBook.Application.Hwnd);
            ct.ShowDialog();
            // ct.Owner =  (Window)xlMain;
            // xlMain.ReleaseHandle();
        }

        // no need
        private static void CrossExec(Excel.Workbook workBook, Excel.Worksheet workSheet)
        {
           // CrossTabulationQC crossTabulationQC = new CrossTabulationQC();
           // crossTabulationQC.Tabulate(workBook, workSheet);
        }

        //private static void GTExec(Excel.Workbook workBook, Excel.Worksheet workSheet)
        //{
        //    GrossTabulationQc grossTabulationQC = new GrossTabulationQc();
        //    grossTabulationQC.Tabulate(workBook, workSheet);
        //}

        //private static void GtOption(Excel.Workbook workBook, Excel.Worksheet workSheet)
        //{
        //    //NativeWindow xlMain = new NativeWindow();
        //    //xlMain.AssignHandle(new IntPtr(workBook.Application.Hwnd));
        //    GrossTabulation gt = new GrossTabulation(workBook, "tmp");
        //    WindowInteropHelper wih = new WindowInteropHelper(gt);
        //    wih.Owner = new IntPtr(workBook.Application.Hwnd);
        //    gt.ShowDialog();
        //    // ct.Owner =  (Window)xlMain;
        //    // xlMain.ReleaseHandle();
        //}

    }
}
