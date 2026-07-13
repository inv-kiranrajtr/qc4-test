using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using QC4Common.Model;
using Qc4Launcher.Util;
using static Qc4Launcher.Forms.Multivariate_Analysis.CollectionClass;
using System.Collections;
using System.Collections.ObjectModel;
using System.Globalization;
using Constants = Qc4Launcher.Util.Constants;

namespace Qc4Launcher.Forms.Multivariate_Analysis
{
    class SourceVariableFromList
    {
        private QuestionSettings question = new QuestionSettings();
        public Dictionary<string, QuestionSettings> dictionary = new Dictionary<string, QuestionSettings>();
        List<psmQuestions> variableList;
        QC4Common.Util.FormUtil frmutil = new QC4Common.Util.FormUtil();
        public List<psmQuestions> GetVariableFromList(Excel.Workbook workbook, string CodeName,bool sampleid=true)
        {
            try
            {
                int count = 0;
                variableList = new List<psmQuestions>();
                dictionary = Definiotion.VariableDictionary;
                var SettingSheet = ExcelUtil.GetWorkSheetByCodeName(workbook, "Sheet91");
                Range range = SettingSheet.get_Range(CodeName);
                if (range.Cells.Count > 0)
                {
                    if (range.Cells.Count == 1)
                    {
                        if (range.Value != null)
                        {

                            if (Convert.ToString(range.Value) != string.Empty && dictionary.ContainsKey(Convert.ToString(range.Value)))
                            {
                                if (sampleid == false) {  }
                                psmQuestions psm = new psmQuestions();
                                question = dictionary[Convert.ToString(Convert.ToString(range.Value))];
                                psm.Variable = question.Variable;
                                psm.Choices = question.Choices;
                                psm.Question = frmutil.EscapeCRLF(question.Question);
                                psm.AnswerType = question.AnswerType;
                                psm.Score = question.Score;
                                psm.AnswerTypeBefore = question.AnswerTypeBefore;
                                psm.OrderNo = count;
                                if (question.Choices.Count > 0) { psm.AnswerTypeCount = string.Join("/", question.AnswerType, question.Choices.Count); } else { psm.AnswerTypeCount = question.AnswerType; }
                                variableList.Add(psm);
                                count++;
                            }
                        }
                    }
                    else
                    {
                        var objAry = range.Value;
                        if (objAry != null)
                        {
                            int max = objAry.GetLength(0);
                            if (max > 0)
                            {
                                for (int i = 1; i <= max; i++)
                                {
                                    if (objAry[i, 1] != null)
                                    {

                                        if (Convert.ToString(objAry[i, 1]) != string.Empty && dictionary.ContainsKey(Convert.ToString(objAry[i, 1])))
                                        {
                                            psmQuestions psm = new psmQuestions();
                                            question = dictionary[Convert.ToString(objAry[i, 1])];
                                            psm.Variable = question.Variable;
                                            psm.Choices = question.Choices;
                                            psm.Question = frmutil.EscapeCRLF(question.Question);
                                            psm.AnswerType = question.AnswerType;
                                            psm.AnswerTypeBefore = question.AnswerTypeBefore;
                                            psm.Score = question.Score;
                                            psm.OrderNo = count;
                                            if (question.Choices.Count > 0) { psm.AnswerTypeCount = string.Join("/", question.AnswerType, question.Choices.Count); } else { psm.AnswerTypeCount = question.AnswerType; }
                                            variableList.Add(psm);
                                            count++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return variableList;
        }
        public bool WriteFilterrSettings(Excel.Workbook workbook,string[,] array,int endrow,int startrow)
        {
            
            int rowcount = array.GetLength(0);
            var res = Util.ExcelUtil.GetWorkSheetBySheetName(workbook, Constants.SheetType.sh_Sheet2); //GetWorkSheetByCodeName(workbook, Constants.SheetCodeName.MultiVariate);
            Excel.Range start = res.Cells[startrow, 2];
            Excel.Range end = res.Cells[(endrow), 235];
            Excel.Range r = res.Range[start, end];
            r.Value = null;
            var values = r.Value;
            for (int i = 0; i < rowcount; i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    values[i+1, j+1] =array[i,j];
                }
            }
            r.Value = values;
            return true;
        }
        public bool WriteProcess(Excel.Workbook workbook,string codeName,int row)
        {
            var SettingSheet = ExcelUtil.GetWorkSheetByCodeName(workbook, "Sheet91");
            Range range = SettingSheet.get_Range(codeName);
            return true;
        }

        public virtual (ObservableCollection<psmQuestions> ListN, ObservableCollection<psmQuestions> ListSA) GetNVariable(ObservableCollection<psmQuestions> list)
        {
            ObservableCollection<psmQuestions> ListN = new ObservableCollection<psmQuestions>();
            ObservableCollection<psmQuestions> ListSA = new ObservableCollection<psmQuestions>();
            foreach (var item in list)
            {
                if (item.AnswerType == "N")
                {
                    ListN.Add(item);
                }
                else if (!string.IsNullOrEmpty(item.Score))
                {
                    ListSA.Add(item);
                }
            }
            return (ListN, ListSA);
        }
        public virtual void getMinMaxAvg(string variablename,int choice, out double min, out double max, out double avg)
        {
           
                string[] choiceSplit = variablename.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                double[] DoubleChoices = Array.ConvertAll(choiceSplit, double.Parse);
                min = DoubleChoices.Min();
                max = DoubleChoices.Max();
            double sum = DoubleChoices.Sum();
            avg = sum / choice;
            

        }

        public void RemoveValueByCount(ObservableCollection<psmQuestions> obsList,psmQuestions instance)
        {
            try
            {
                var List = obsList;
                obsList.Clear();
                foreach (var item in List)
                {
                    if (item.Choices.Count > 3)
                    {
                        obsList.Add(item);
                    }
                }
            }
            catch(Exception ex)
            {
            }
        }
        public void RemoveItem(ObservableCollection<psmQuestions> collection, psmQuestions instance)
        {
            try
            {
                bool contains = collection.Any(p => p.Variable == "SAMPLEID");
                if (contains)
                {
                    collection.Remove(collection.Where(i => i.Variable == "SAMPLEID").Single());
                }
                
            }catch(Exception ex) { }
        }

        public void Delete(ObservableCollection<psmQuestions> collection, psmQuestions instance)
        {
            try
            {
                collection.Remove(collection.Where(i => i.Variable == instance.Variable).Single());
            }
            catch
            {

            }
        }

    }
    public static class ListExtension
    {
        public static void Sort<TSource, TKey>(this Collection<TSource> source, Func<TSource, TKey> keySelector)
        {
            List<TSource> sortedList = source.OrderBy(keySelector).ToList();
            source.Clear();
            foreach (var sortedItem in sortedList)
                source.Add(sortedItem);
        }
       
        public static void Wherel<TSource>(this Collection<TSource> source, Func<TSource, bool> predicate)
        {
            List<TSource> SortSAN = source.Where(predicate).ToList();
            
            source.Clear();
            foreach (var sortedItem in SortSAN)
                source.Add(sortedItem);
        }
       

        public static void SourceBind<TSource>(this Collection<TSource> source)
        {
           
        }
    }
}
