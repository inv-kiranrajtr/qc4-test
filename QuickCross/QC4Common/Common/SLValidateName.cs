using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace QC4Common.Common
{
    public class SLValidateName
    {
        public static ReturnClass validateName(Worksheet sht)
        {
            Workbook workBook = sht.Application.ActiveWorkbook;
            if (null == sht) return new ReturnClass(true);
            HashSet<string> currentNames = new HashSet<string>();
            HashSet<string> usedNames = new HashSet<string>();
            Range sLSettingRange = CRValidate.findMaxAllocatedRange(sht, false, sht.Range[Constants.SL.SLVariableStartAddress]);
            if (sLSettingRange.Row <= Constants.SL.SLRowInputStart)
            {
                return new ReturnClass(true);
            }
            sLSettingRange = sht.Range[sht.Range[Constants.SL.SLVariableStartAddress], sLSettingRange];

            foreach (Range targetCell in sLSettingRange.Cells)
            {
                Range outputNameSet = targetCell.Offset[0, -1];
                if (!String.IsNullOrEmpty(outputNameSet.Value2))
                {
                    currentNames.Add(outputNameSet.Value2.ToString());
                }

            }
            Range targetCellG = null;
            Range firstSep = null;
            bool nonFirstSet = false;
            foreach (Range targetCell in sLSettingRange.Cells)
            {
                Range outputNameSet = targetCell.Offset[0, -1];

                if (SLValidate.checkSepPresent(targetCell))
                {
                    if (!String.IsNullOrEmpty(targetCell.Value2) && firstSep == null)
                    {
                        firstSep = targetCell;
                    }
                    ReturnClass res = validateOutputName(workBook, targetCell, firstSep, nonFirstSet, sht, currentNames, usedNames);
                    if (res.Result == false)
                    {
                        return res;
                    }
                    firstSep = null;
                    nonFirstSet = false;
                }
                else if (!String.IsNullOrEmpty(targetCell.Value2))
                {
                    if (firstSep == null)
                    {
                        firstSep = targetCell;
                    }
                    else if (!String.IsNullOrEmpty(outputNameSet.Value2))
                    {
                        if (!String.IsNullOrEmpty(outputNameSet.Value2.ToString().Trim()))
                            nonFirstSet = true;
                    }
                }
                targetCellG = targetCell;
            }
            ReturnClass rest = validateOutputName(workBook, targetCellG, firstSep, nonFirstSet, sht, currentNames, usedNames);
            if (rest.Result == false)
            {
                return rest;
            }
            return new ReturnClass(true);
        }

        private static ReturnClass validateOutputName(Workbook workBook, Range targetCell, Range firstSep, bool nonFirstSet, Worksheet sht, HashSet<string> currentNames, HashSet<string> usedNames)
        {
            if (null == firstSep)
            {
                return new ReturnClass(true);
            }
            Range sLSettingRange = sht.Range[firstSep, targetCell];
            Range firstSepName = firstSep.Offset[0, -1];
            String firstSepName_Value2 = firstSepName.Value2 != null ? firstSepName.Value2.ToString().Trim() : null;
            if (String.IsNullOrEmpty(firstSepName_Value2) && nonFirstSet)
            {
                //DialogResult res = MessageDialog.ErrorOkCancel(String.Format("Output name is not set in first variable, Row:{0}.Please click ok for generating name in first varaible"/*AddinResource.SL_OUPUT_NAME_NOT_SET_FIRST*/, firstSepName.Row));
                DialogResult res = MessageDialog.ShowMessageOnWorkBook(String.Format(CommonResource.SL_OUPUT_NAME_NOT_SET_FIRST, firstSepName.Row),
                        Constants.MessageType.ErrorOkCancel, workBook);
                if (res == DialogResult.OK)
                {
                    if (!generateName(workBook, firstSepName, firstSep, currentNames, usedNames))
                    {
                        return new ReturnClass(false, firstSepName);
                    }
                }
                else
                {
                    return new ReturnClass(false, firstSepName);
                }
            }
            else if (String.IsNullOrEmpty(firstSepName_Value2))
            {
                if (!generateName(workBook, firstSepName, firstSep, currentNames, usedNames))
                {
                    return new ReturnClass(false, firstSepName);
                }
            }
            else if (!String.IsNullOrEmpty(firstSepName_Value2) && nonFirstSet)
            {
                //DialogResult res = MessageDialog.ErrorOkCancel(String.Format("Multiple output name is set,Row:{0}.Please click ok for using first one"/*AddinResource.SL_OUPUT_NAME_MULTIPLE*/, firstSepName.Row));
                DialogResult res = MessageDialog.ShowMessageOnWorkBook(String.Format(CommonResource.SL_OUPUT_NAME_MULTIPLE, firstSepName.Row),
                        Constants.MessageType.ErrorOkCancel, workBook);
                if (res == DialogResult.Cancel)
                {
                    return new ReturnClass(false, firstSepName);
                }
                else
                {
                    if (usedNames.Contains(firstSepName.Value2.ToString()))
                    {
                        //DialogResult reslt = MessageDialog.ErrorOkCancel(String.Format("Duplicate name, Row:{ 0}.Please click ok for generating new name"/*AddinResource.SL_OUPUT_NAME_DUPLICATE*/, firstSepName.Row));
                        DialogResult reslt = MessageDialog.ShowMessageOnWorkBook(String.Format(CommonResource.SL_OUPUT_NAME_DUPLICATE, firstSepName.Row),
                            Constants.MessageType.ErrorOkCancel, workBook);

                        if (reslt == DialogResult.OK)
                        {
                            string variable = firstSepName.Value2.ToString();
                            variable = generateName(variable, currentNames);
                            currentNames.Add(variable);
                            usedNames.Add(variable);
                            firstSepName.Value2 = variable;
                        }
                        else
                        {
                            return new ReturnClass(false, firstSepName);
                        }
                    }
                    else
                    {
                        usedNames.Add(firstSepName.Value2.ToString());
                    }
                }
            }
            else if (!String.IsNullOrEmpty(firstSepName_Value2))
            {
                if (usedNames.Contains(firstSepName.Value2.ToString()))
                {
                    //DialogResult res = MessageDialog.ErrorOkCancel(String.Format("Duplicate name, Row:{0}.Please click ok for generating new name"/*AddinResource.SL_OUPUT_NAME_DUPLICATE*/, firstSepName.Row));
                    DialogResult res = MessageDialog.ShowMessageOnWorkBook(String.Format(CommonResource.SL_OUPUT_NAME_DUPLICATE, firstSepName.Row),
                        Constants.MessageType.ErrorOkCancel, workBook);
                    if (res == DialogResult.OK)
                    {
                        string variable = firstSepName.Value2.ToString();
                        variable = generateName(variable, currentNames);
                        currentNames.Add(variable);
                        usedNames.Add(variable);
                        firstSepName.Value2 = variable;
                    }
                    else
                    {
                        return new ReturnClass(false, firstSepName);
                    }
                }
                else
                {
                    usedNames.Add(firstSepName.Value2.ToString());
                }
            }

            return new ReturnClass(true);
        }

        private static bool generateName(Workbook workBook, Range firstSepName, Range firstSep, HashSet<string> currentNames, HashSet<string> usedNames)
        {
            string variable = firstSep.Value2.ToString();
            variable = Regex.Replace(variable, @"s\d*$", "", RegexOptions.IgnoreCase);
            variable = "N" + variable + "_MT";
            if (usedNames.Contains(variable))
            {
                //DialogResult reslt = MessageDialog.ErrorOkCancel(String.Format("Duplicate default name found, Row:{0}.Please click ok for generating name"/*AddinResource.SL_OUPUT_NAME_DUPLICATE_DEFAULT*/, firstSepName.Row));
                DialogResult reslt = MessageDialog.ShowMessageOnWorkBook(String.Format(CommonResource.SL_OUPUT_NAME_DUPLICATE_DEFAULT, firstSepName.Row), 
                    Constants.MessageType.ErrorOkCancel, workBook);
                if (reslt == DialogResult.OK)
                {
                    variable = generateName(variable, currentNames);
                }
                else
                {
                    return false;
                }
            }

            currentNames.Add(variable);
            usedNames.Add(variable);
            firstSepName.Value2 = variable;
            return true;
        }

        private static string generateName(string variable, HashSet<string> currentNames)
        {
            int i = 1;
            String finalVariable = variable;
            while (currentNames.Contains(finalVariable))
            {
                finalVariable = i + variable;
                i++;
            }
            return finalVariable;
        }
    }
}
