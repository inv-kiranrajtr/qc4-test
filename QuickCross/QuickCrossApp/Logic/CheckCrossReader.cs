using log4net;
using Macromill.QCWeb.Question;
using Macromill.QCWeb.Tabulation;
using QC4Common.Model;
using Qc4Launcher.Util;
using System;
using System.Collections.Generic;
using static Macromill.QCWeb.Question.Questions;
using static Qc4Launcher.Logic.CrossSettingsReader;

namespace Qc4Launcher.Logic
{
    internal class CheckCrossReader
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        internal static List<CossTableInput> readSettings(string chkCrsfile, Microsoft.Office.Interop.Excel.Workbook workBook, bool delete = true)
        {
            List<CossTableInput> crTableSets = new List<CossTableInput>();
            Questions questions = DictUpdate.GetQuestions(workBook);
            string tempPath = chkCrsfile;
            string srcItem = null;
            string targetItem = null;
            if (!System.IO.File.Exists(chkCrsfile))
            {
                _log.Warn("Check cross file not present at " + chkCrsfile);
                return crTableSets;
            }
            System.IO.StreamReader reader = new System.IO.StreamReader(tempPath);
            using (reader)
            {
                try
                {
                    for (int i = 0; !reader.EndOfStream; ++i)
                    {
                        string rowBuffer = reader.ReadLine();
                        string[] splitBuffer = rowBuffer.Split('\t');
                        if (splitBuffer.Length != 3)
                        {
                            _log.Warn("length missmatch. skipping.Line:" + rowBuffer);
                            continue;
                        }
                        if (splitBuffer[0].Length == 0 || splitBuffer[1].Length == 0 || splitBuffer[2].Length == 0)
                        {
                            _log.Warn("Invalid entries. skipping.Line:" + rowBuffer);
                            continue;
                        }

                        string[] items = splitBuffer[2].Split(',');
                        if (items.Length < 0)
                        {
                            continue;
                        }

                        string[] target = splitBuffer[1].Split(',');
                        if (target.Length < 0 || target.Length > 2)
                        {
                            continue;
                        }

                        for (int j = 0; j < items.Length; j += 2)
                        {
                            if (items[j].Length == 0)
                            {
                                continue;
                            }

                            srcItem = questions[Convert.ToUInt64(items[j])].Name;
                            targetItem = questions[Convert.ToUInt64(target[0])].Name;
                            if (splitBuffer[0].ToUpper() == "CLASS" || splitBuffer[0].ToUpper() == "INTEGRATE")
                            {
                                //QuestionSettings qstnDet1 = Definiotion.VariableDictionary[target[0]];
                                Question qstn = (Question)questions[Convert.ToUInt64(target[0])];
                                if ((qstn.QuestionType & QuestionType.N) == QuestionType.N)
                                {
                                    continue;
                                }
                                CossTableInput cr = new CossTableInput(srcItem, targetItem, null, null, null, null, null, null);
                                cr.filePathAxis1 = target[1];
                                cr.filePathTarget = items[j + 1];
                                cr.lineNo = findLine(cr.filePathTarget);
                                crTableSets.Add(cr);

                            }
                            else
                            {
                                //QuestionSettings qstnDet1 = Definiotion.VariableDictionary[items[j]];
                                Question qstn = (Question)questions[Convert.ToUInt64(items[j])];
                                if ((qstn.QuestionType & QuestionType.N) == QuestionType.N)
                                {
                                    continue;
                                }
                                CossTableInput cr = new CossTableInput(targetItem, srcItem, null, null, null, null, null, null);
                                cr.filePathTarget = target[1];
                                cr.filePathAxis1 = items[j + 1];
                                cr.lineNo = findLine(cr.filePathTarget);
                                crTableSets.Add(cr);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //throw e;
                    _log.LogError(ex.Message + "\n" + ex.StackTrace + "\n---------------\n" + ex.InnerException);
                }
                finally
                {
                    reader.Close();
                    try
                    {
                        if (delete)
                        {
                            System.IO.File.Delete(tempPath);
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
            return crTableSets;
        }

        private static int findLine(string filePathTarget)
        {
            string path = System.IO.Path.GetFileName(filePathTarget);
            string[] sp = path.Split('_');
            return Convert.ToInt32(sp[0]);
        }
    }

}