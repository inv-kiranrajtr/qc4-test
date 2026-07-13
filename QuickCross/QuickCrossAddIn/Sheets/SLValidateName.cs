using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ExcelAddIn.Common
{
    internal class SLValidateName
    {
    
        internal static ReturnClass validateName()
        {
            Worksheet sht = SLValidate.getSLSheet();
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
                if (outputNameSet.Value2 != null)
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
                    ReturnClass res = validateOutputName(targetCell, firstSep, nonFirstSet, sht, currentNames, usedNames);
                    if (res.Result == false)
                    {
                        return res;
                    }
                    firstSep = null;
                    nonFirstSet = false;
                }
                else if (targetCell.Value2 != null)
                {
                    if (firstSep == null)
                    {
                        firstSep = targetCell;
                    }
                    else if (outputNameSet.Value2 != null)
                    {
                        nonFirstSet = true;
                    }
                }
                targetCellG = targetCell;
            }
            ReturnClass rest = validateOutputName(targetCellG, firstSep, nonFirstSet, sht, currentNames, usedNames);
            if (rest.Result == false)
            {
                return rest;
            }
            return new ReturnClass(true);
        }

        private static ReturnClass validateOutputName(Range targetCell, Range firstSep, bool nonFirstSet, Worksheet sht, HashSet<string> currentNames, HashSet<string> usedNames)
        {
            if (null == firstSep)
            {
                return new ReturnClass(true);
            }
            Range sLSettingRange = sht.Range[firstSep, targetCell];
            Range firstSepName = firstSep.Offset[0, -1];
            if (firstSepName.Value2 == null && nonFirstSet)
            {
                DialogResult res = MessageDialog.ErrorOkCancel(String.Format(AddinResource.SL_OUPUT_NAME_NOT_SET_FIRST, firstSepName.Row));
                if (res == DialogResult.OK)
                {
                    if (!generateName(firstSepName, firstSep, currentNames, usedNames)) {
                        return new ReturnClass(false, firstSepName);
                    }
                }
                else
                {
                    return new ReturnClass(false, firstSepName);
                }
            }
            else if (firstSepName.Value2 == null)
            {
                if (!generateName(firstSepName, firstSep, currentNames, usedNames))
                {
                    return new ReturnClass(false, firstSepName);
                }
            }
            else if (firstSepName.Value2 != null && nonFirstSet)
            {
                DialogResult res = MessageDialog.ErrorOkCancel(String.Format(AddinResource.SL_OUPUT_NAME_MULTIPLE, firstSepName.Row));
                if (res == DialogResult.Cancel)
                {
                    return new ReturnClass(false, firstSepName);
                }
                else
                {
                    if (usedNames.Contains(firstSepName.Value2.ToString()))
                    {
                        DialogResult reslt = MessageDialog.ErrorOkCancel(String.Format(AddinResource.SL_OUPUT_NAME_DUPLICATE, firstSepName.Row));
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
            else if (firstSepName.Value2 != null)
            {
                if (usedNames.Contains(firstSepName.Value2.ToString()))
                {
                    DialogResult res = MessageDialog.ErrorOkCancel(String.Format(AddinResource.SL_OUPUT_NAME_DUPLICATE, firstSepName.Row));
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

        private static bool generateName(Range firstSepName, Range firstSep, HashSet<string> currentNames, HashSet<string> usedNames)
        {
            string variable = firstSep.Value2.ToString();
            variable = Regex.Replace(variable, @"s\d*$", "", RegexOptions.IgnoreCase);
            variable = "N" + variable + "_MT";
            if (usedNames.Contains(variable))
            {
                DialogResult reslt = MessageDialog.ErrorOkCancel(String.Format(AddinResource.SL_OUPUT_NAME_DUPLICATE_DEFAULT, firstSepName.Row));
                if (reslt == DialogResult.OK)
                {
                    variable = generateName(variable, currentNames);
                }
                else {
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