using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Qc4Launcher.Util;
using FilterSettingsView;
using QC4Common.Model;

namespace Qc4Launcher.Classes
{
    class FilterSettings
    {
        private Dictionary<string, System.Windows.Controls.Control> controlObj = new Dictionary<string, System.Windows.Controls.Control>();
        private List<String> elementsInSheet = new List<String>();

        public class DataExport
        {
            public string QuestionVariable { get; set; }
            public string QuestionVariableType { get; set; }
            public string Question { get; set; }
            public int QuestionIndex { get; set; }
            public String QuestionChoiceNo { get; set; }
            public List<String> Choisces { get; set; }
        }
        public List<FilterSettingsClass.DataExport> sdlist = new List<FilterSettingsClass.DataExport>();
       

        public void GetValue( System.Windows.Controls.Control x,ref Dictionary<string, String> ReadValueFromExcel)
		{
			if (x is System.Windows.Controls.ComboBox)
			{
				var myObject = ((System.Windows.Controls.ComboBox)x).SelectedValue as DataExport;

				if (myObject != null)
					ReadValueFromExcel.Add("F_Do_Output_" + ((System.Windows.Controls.ComboBox)x).Name + "_", ((DataExport)((System.Windows.Controls.ComboBox)x).SelectedItem).QuestionVariable);
				else
					ReadValueFromExcel.Add("F_Do_Output_" + ((System.Windows.Controls.ComboBox)x).Name + "_", ((System.Windows.Controls.ComboBox)x).Text);

				////var bindedObj =
				///}
				////if (bindedObj != null)

				//else
				//    ReadValueFromExcel.Add("F_Do_Output_" + ((System.Windows.Controls.ComboBox)x).Name + "_", "");
			}
			else if (x is System.Windows.Controls.TextBox)
				ReadValueFromExcel.Add("F_Do_Output_" + ((System.Windows.Controls.TextBox)x).Name + "_", ((System.Windows.Controls.TextBox)x).Text);

			else if (x is System.Windows.Controls.RadioButton)
				ReadValueFromExcel.Add("F_Do_Output_" + ((System.Windows.Controls.RadioButton)x).Name + "_", ((System.Windows.Controls.RadioButton)x).IsChecked.ToString());

			else if (x is System.Windows.Controls.CheckBox)
				ReadValueFromExcel.Add("F_Do_Output_" + ((System.Windows.Controls.CheckBox)x).Name + "_", ((System.Windows.Controls.CheckBox)x).IsChecked.ToString());
		}


        //public void GetName(System.Windows.Controls.Control x,)
        //{
        //    if (x is System.Windows.Controls.ComboBox)
        //        controlObj.Add(UIElement.Name = "F_Do_Output_" + ((System.Windows.Controls.ComboBox)x).Name + "_", ((System.Windows.Controls.ComboBox)x));

        //    else if (x is System.Windows.Controls.TextBox)
        //        controlObj.Add(Name = "F_Do_Output_" + ((System.Windows.Controls.TextBox)x).Name + "_", ((System.Windows.Controls.TextBox)x));

        //    else if (x is System.Windows.Controls.RadioButton)
        //        controlObj.Add(Name = "F_Do_Output_" + ((System.Windows.Controls.RadioButton)x).Name + "_", ((System.Windows.Controls.RadioButton)x));

        //    else if (x is System.Windows.Controls.CheckBox)
        //        controlObj.Add(Name = "F_Do_Output_" + ((System.Windows.Controls.CheckBox)x).Name + "_", ((System.Windows.Controls.CheckBox)x));
        //}


        public void LoadingData(
            out ObservableCollection<FilterSettingsClass.DataExport> dataExport_LBVariablesToExport,
            out ObservableCollection<FilterSettingsClass.DataExport> qstnvariablDD1, out ObservableCollection<FilterSettingsClass.DataExport> qstnvariablDD2)
        {
            QC4Common.Util.FormUtil formUtil = new QC4Common.Util.FormUtil();
            ObservableCollection<FilterSettingsClass.DataExport> _qstnvariablDD1 = new ObservableCollection<FilterSettingsClass.DataExport>();
            ObservableCollection<FilterSettingsClass.DataExport> _qstnvariablDD2 = new ObservableCollection<FilterSettingsClass.DataExport>();

            ObservableCollection<FilterSettingsClass.DataExport> _dataExport_LBVariablesToExport = new ObservableCollection<FilterSettingsClass.DataExport>();

            List<FilterSettingsClass.DataExport> Combo_Classify = new List<FilterSettingsClass.DataExport>();
            List<FilterSettingsClass.DataExport> Combo_conditional = new List<FilterSettingsClass.DataExport>();

            _qstnvariablDD1.Add(new FilterSettingsClass.DataExport() { QuestionVariable = "", QuestionVariableType = "", Question = "" });
            _qstnvariablDD2.Add(new FilterSettingsClass.DataExport() { QuestionVariable = "", QuestionVariableType = "", Question = "" });

            Dictionary<string, QuestionSettings> PopulatedDictionary = Definiotion.VariableDictionary;
            String[] dictKeys = PopulatedDictionary.Keys.ToArray<String>();

            for (int i = 0; i < dictKeys.Count<String>(); i++)
            {
                QuestionSettings qs = PopulatedDictionary[dictKeys[i]];
                FilterSettingsClass.DataExport item;

                if (qs.CategoryCount != 0)
                    item = new FilterSettingsClass.DataExport() { QuestionVariable = qs.Variable, QuestionVariableType = qs.AnswerType + "/" + qs.CategoryCount, Question = qs.Question, QuestionIndex = i - 1, QuestionChoiceNo = qs.QuestionNumber, Choisces = qs.Choices };
                else
                    item = new FilterSettingsClass.DataExport() { QuestionVariable = qs.Variable, QuestionVariableType = qs.AnswerType, Question = qs.Question, QuestionIndex = i - 1, QuestionChoiceNo = qs.QuestionNumber, Choisces = qs.Choices };

                item.Question = formUtil.EscapeCRLF(item.Question);                              
                _dataExport_LBVariablesToExport.Add(item);

                if (!(item.QuestionVariableType.Equals("D")))
                {
                    _qstnvariablDD2.Add(item);

                    if (!(item.QuestionVariableType.Equals("N")) && !(item.QuestionVariableType.Equals("FA")))
                    {
                        _qstnvariablDD1.Add(item);
                    }
                }
            }

            dataExport_LBVariablesToExport = _dataExport_LBVariablesToExport;
            qstnvariablDD1 = _qstnvariablDD1;
            qstnvariablDD2 = _qstnvariablDD2;
        }



        
        /*Public void ListLoading()
     {
         List<String> removItem = new List<String>();
         bool set = false;
         var SettingSheet = ExcelUtil.GetWorkSheetByCodeName(Workbook, Constants.SheetCodeName.DetailsSetting);
         Excel.Range rar = SettingSheet.get_Range("C:C");
         var obj = rar.Value;
         int rowValue = obj.GetLength(0);
         String[] dictKeys = PopulatedDictionary.Keys.ToArray<String>();


         for (int i = 2; i < rowValue; i++)
         {
             if (obj[i, 1] != null)
             {
                 ExcelAddIn.Sheets.QuestionSettings qs = PopulatedDictionary[obj[i, 1]];

                 set = true;

                 if (qs.CategoryCount != 0)
                     _dataExport_ListBoxCommonCopy.Add(new FilterSettings.DataExport() { QuestionVariable = qs.Variable, QuestionVariableType = qs.AnswerType + "/" + qs.CategoryCount, Question = qs.Question, QuestionIndex = i - 1, QuestionChoiceNo = qs.QuestionNumber, Choisces = qs.Choices });
                 else
                     _dataExport_ListBoxCommonCopy.Add(new FilterSettings.DataExport() { QuestionVariable = qs.Variable, QuestionVariableType = qs.AnswerType, Question = qs.Question, QuestionIndex = i - 1, QuestionChoiceNo = qs.QuestionNumber, Choisces = qs.Choices });

                 removItem.Add(qs.Variable);
                 set = true;
             }
             else
             {

                 break;
             }
         }

         if (set)
         {
             _dataExport_LBVariablesToExport.Clear();
             int j = 0;
             for (int i = 0; i < dictKeys.Count<String>(); i++)
             {
                 var qs = PopulatedDictionary[dictKeys[i]];

                 if (!removItem.Contains(qs.Variable))
                 {
                     if (qs.CategoryCount != 0)
                         _dataExport_LBVariablesToExport.Add(new FilterSettings.DataExport() { QuestionVariable = qs.Variable, QuestionVariableType = qs.AnswerType + "/" + qs.CategoryCount, Question = qs.Question, QuestionIndex = i - 1, QuestionChoiceNo = qs.QuestionNumber, Choisces = qs.Choices });
                     else
                         _dataExport_LBVariablesToExport.Add(new FilterSettings.DataExport() { QuestionVariable = qs.Variable, QuestionVariableType = qs.AnswerType, Question = qs.Question, QuestionIndex = i - 1, QuestionChoiceNo = qs.QuestionNumber, Choisces = qs.Choices });
                 }
             }

             this.LBVariablesToExport.DataContext = _dataExport_LBVariablesToExport;
             this.ListBoxCommonCopy.DataContext = _dataExport_ListBoxCommonCopy;
         }
         else
         {//191
             if (dictKeys.Contains(Constants.QuestionVariableValue.QuestionVariableItem))
             {
                 int loc = 0;
                 for (int i = 0; i < dictKeys.Count<String>(); i++)
                 {
                     if (dictKeys[i] == Constants.QuestionVariableValue.QuestionVariableItem)
                     {
                         loc = i;
                         break;
                     }
                 }
                 _dataExport_ListBoxCommonCopy.Add(_dataExport_LBVariablesToExport.ElementAt(loc));
                 _dataExport_LBVariablesToExport.RemoveAt(loc);
                 this.LBVariablesToExport.DataContext = _dataExport_LBVariablesToExport;
                 this.ListBoxCommonCopy.DataContext = _dataExport_ListBoxCommonCopy;


             }
             else
             {
                 _dataExport_ListBoxCommonCopy.Add(_dataExport_LBVariablesToExport.ElementAt(0));
                 _dataExport_LBVariablesToExport.RemoveAt(0);
                 this.LBVariablesToExport.DataContext = _dataExport_LBVariablesToExport;
                 this.ListBoxCommonCopy.DataContext = _dataExport_ListBoxCommonCopy;
             }
         }

     }*/
    }
}
