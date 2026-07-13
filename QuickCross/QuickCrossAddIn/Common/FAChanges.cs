using System;
using Microsoft.Office.Interop.Excel;
using System.Data.SQLite;
using ExcelAddIn.DB;
using System.Collections.Generic;
using System.Data;
using Macromill.QCWeb.Exceptions;
using Macromill.QCWeb.Tabulation;
using System.Collections;

namespace ExcelAddIn.Common
{
    internal class FAChanges
    {
        //Logger.Log LogObj;
        internal FAChanges()
        {
            //LogObj = new Logger.Log();
        }
        internal static void FAValueChange(Worksheet sheet, Range target)
        {
            AppHelper appHelper = new AppHelper();
            appHelper.ExcelSet(calculationMode: false, targetApp: sheet.Application);
            if (target.Columns.Count != target.EntireRow.Columns.Count
                && target.Rows.Count != target.EntireColumn.Rows.Count)
            {
                foreach (Range targetCell in target)
                {
                    switch (targetCell.Column)
                    {
                        case Common.Constants.FA.FaColChartType:
                            FAChangeChartType(targetCell);
                            break;
                        //case Common.Constants.FA.FaColGraphType:
                            //break;
                        default:
                            if (targetCell.Column >= Common.Constants.FA.FaColItem && targetCell.Column <= Common.Constants.FA.FaMaxItemNo)
                            {
                                FAChangeItem(targetCell);
                            }
                            else
                            {
                                FAChangeAddItem(targetCell);
                            }
                            break;
                    }

                }
            }

            appHelper.ExcelReset(true, targetApp: sheet.Application);
        }
        private static void FAChangeAddItem(Range targetCell)
        {
            if (targetCell.Row < Constants.FA.FaRowDataStart)
            {

                return;
            }

            Range ItemArea = GetAddRange(targetCell);
            // TO DO : check with qc3 implimentation
            //Call FNC_CloseUp_Range(Item_Range)
            CommonFunctions.CloseUpRange(ItemArea);
            FAList.ExecToggle(targetCell.Worksheet, targetCell, false);
        }
        private static void FAChangeItem(Range targetCell)
        {
            if (targetCell.Row < Constants.FA.FaRowDataStart)
            {
                return;
            }

            Range ItemArea = GetItemRange(targetCell);
            // TO DO : check with qc3 implimentation
            //Call FNC_CloseUp_Range(Item_Range)
            CommonFunctions.CloseUpRange(ItemArea);
            FAList.ExecToggle(targetCell.Worksheet, targetCell, false);
        }
        private static Range GetItemRange(Range targetCell)
        {
            Range startCell = null;
            Range endCell = null;
            if(targetCell.Column == 3)
            {
                startCell = targetCell.Worksheet.Cells[targetCell.Row, targetCell.Column];
                endCell = startCell;
            }
            else
            {
                startCell = targetCell.Worksheet.Cells[targetCell.Row, Constants.FA.FaColItem];
                endCell = targetCell.Worksheet.Cells[targetCell.Row, Constants.FA.FaMaxItemNo];//Constants.FA.FaColItem +
            }
            
            return targetCell.Worksheet.Range[startCell, endCell];
        }

        private static Range GetAddRange(Range targetCell)
        {
            Range startCell = null;
            Range endCell = null;
            if (targetCell.Column == 2)
            {
                startCell = targetCell.Worksheet.Cells[targetCell.Row, targetCell.Column];
                endCell = startCell;
            }
            else
            {
                startCell = targetCell.Worksheet.Cells[targetCell.Row, Constants.FA.FaAddColItem];
                endCell = targetCell.Worksheet.Cells[targetCell.Row, Constants.FA.FaAddMaxItemNo];//Constants.FA.FaAddColItem +
            }

            return targetCell.Worksheet.Range[startCell, endCell];
           
        }
       
      
        private static void FAChangeChartType(Range targetCell)
        {
            if (targetCell.Row < Constants.FA.FaRowDataStart)
            {
                return;
            }
            Range ItemArea = GetItemRange(targetCell);
            CommonFunctions.CloseUpRange(ItemArea);
            FAList.ExecToggle(targetCell.Worksheet, targetCell, false);
        }

    }
}
