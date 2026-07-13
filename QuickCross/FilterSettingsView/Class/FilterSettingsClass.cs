using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using QC4Common.Model;

namespace FilterSettingsView
{
   public class FilterSettingsClass
    {
        private Dictionary<String, QuestionSettings> PopulatedDictionary = new Dictionary<string, QuestionSettings>();
        private Dictionary<string, System.Windows.Controls.Control> controlObj = new Dictionary<string, System.Windows.Controls.Control>();
        private List<FilterSettingsView.FilterSettingsClass.DataExport> dataFromSheet = new List<FilterSettingsView.FilterSettingsClass.DataExport>();

        public ObservableCollection<FilterSettingsView.FilterSettingsClass.DataExport> _qstnvariablDD1 = new ObservableCollection<FilterSettingsView.FilterSettingsClass.DataExport>();
        public ObservableCollection<FilterSettingsView.FilterSettingsClass.DataExport> _qstnvariablDD2 = new ObservableCollection<FilterSettingsView.FilterSettingsClass.DataExport>();
        public ObservableCollection<FilterSettingsView.FilterSettingsClass.DataExport> _qstnvariablDD3 = new ObservableCollection<FilterSettingsView.FilterSettingsClass.DataExport>();
        public ObservableCollection<FilterSettingsView.FilterSettingsClass.DataExport> _qstnvariablDD4 = new ObservableCollection<FilterSettingsView.FilterSettingsClass.DataExport>();
        public ObservableCollection<FilterSettingsView.FilterSettingsClass.DataExport> _qstnvariablDD5 = new ObservableCollection<FilterSettingsView.FilterSettingsClass.DataExport>();
        public ObservableCollection<FilterSettingsView.FilterSettingsClass.DataExport> _qstnvariablDD6 = new ObservableCollection<FilterSettingsView.FilterSettingsClass.DataExport>();

        private ObservableCollection<FilterSettingsView.FilterSettingsClass.DataExport> _dataExport_LBVariablesToExport = new ObservableCollection<FilterSettingsView.FilterSettingsClass.DataExport>();

        private List<FilterSettingsView.FilterSettingsClass.DataExport> Combo_Classify = new List<FilterSettingsView.FilterSettingsClass.DataExport>();
        private List<FilterSettingsView.FilterSettingsClass.DataExport> Combo_conditional = new List<FilterSettingsView.FilterSettingsClass.DataExport>();
        private List<String> elementsInSheet = new List<String>();

        public class DataExport
        {
            public string QuestionVariable { get; set; }
            public string QuestionVariableType { get; set; }
            public string Question { get; set; }
            public int QuestionIndex { get; set; }
            public String QuestionChoiceNo { get; set; }
            public List<String> Choisces { get; set; }
            public int ItemIndex { get; set; }
        }
        public class DataGT
        {
            public string QuestionVariable { get; set; }
            public string QuestionVariableToolTip { get; set; }
            public string Graph { get; set; }
            public string ONOFF { get; set; }
            public string Question { get; set; }
            public string QuestionToolTip { get; set; }
            public int QuestionIndex { get; set; }
            public string QSType { get; set; }
            public string QSHeading { get; set; }
            public int CategoryCount { get; set; }
            public string QsTypePlusCatCount { get; set; }
            public string Variable { get; set; }
            public int QuestionNumber { get; set; }
            public string QSTypeShort { get; set; }
            public int TempIndex { get; set; }
            public int QsNumberIndex { get; set; }
            public string GTFlag { get; set; }
            public int ItemIndex { get; set; }
            public string Test { get; set; }
        }
        public List<FilterSettingsView.FilterSettingsClass.DataExport> sdlist = new List<FilterSettingsView.FilterSettingsClass.DataExport>();


        public void GetValue(System.Windows.Controls.Control x, ref Dictionary<string, String> ReadValueFromExcel)
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


        public void LoadingData(Dictionary<String, QuestionSettings> PopulatedDictionary, List<FilterSettingsView.FilterSettingsClass.DataExport> dataFromSheet,
            out ObservableCollection<FilterSettingsView.FilterSettingsClass.DataExport> dataExport_LBVariablesToExport,
            out ObservableCollection<FilterSettingsView.FilterSettingsClass.DataExport> qstnvariablDD1, out ObservableCollection<FilterSettingsView.FilterSettingsClass.DataExport> qstnvariablDD2, out ObservableCollection<FilterSettingsView.FilterSettingsClass.DataExport> qstnvariablDD3,
            out ObservableCollection<FilterSettingsView.FilterSettingsClass.DataExport> qstnvariablDD4, out ObservableCollection<FilterSettingsView.FilterSettingsClass.DataExport> qstnvariablDD5, out ObservableCollection<FilterSettingsView.FilterSettingsClass.DataExport> qstnvariablDD6)
        {
            //PopulatedDictionary = Definiotion.VariableDictionary;
            String[] dictKeys = PopulatedDictionary.Keys.ToArray<String>();

            for (int i = 0; i < dictKeys.Count<String>(); i++)
            {
                QuestionSettings qs = PopulatedDictionary[dictKeys[i]];

                if (qs.CategoryCount != 0)
                    dataFromSheet.Add(new DataExport() { QuestionVariable = qs.Variable, QuestionVariableType = qs.AnswerType + "/" + qs.CategoryCount, Question = qs.Question, QuestionIndex = i - 1, QuestionChoiceNo = qs.QuestionNumber, Choisces = qs.Choices });
                else
                    dataFromSheet.Add(new DataExport() { QuestionVariable = qs.Variable, QuestionVariableType = qs.AnswerType, Question = qs.Question, QuestionIndex = i - 1, QuestionChoiceNo = qs.QuestionNumber, Choisces = qs.Choices });
            }

            _qstnvariablDD1.Add(new DataExport() { QuestionVariable = "", QuestionVariableType = "", Question = "" });
            _qstnvariablDD2.Add(new DataExport() { QuestionVariable = "", QuestionVariableType = "", Question = "" });
            _qstnvariablDD3.Add(new DataExport() { QuestionVariable = "", QuestionVariableType = "", Question = "" });
            _qstnvariablDD4.Add(new DataExport() { QuestionVariable = "", QuestionVariableType = "", Question = "" });
            _qstnvariablDD5.Add(new DataExport() { QuestionVariable = "", QuestionVariableType = "", Question = "" });
            _qstnvariablDD6.Add(new DataExport() { QuestionVariable = "", QuestionVariableType = "", Question = "" });

            QC4Common.Util.FormUtil formUtil = new QC4Common.Util.FormUtil();
            foreach (DataExport item in dataFromSheet)
            {
                item.Question = formUtil.EscapeCRLF(item.Question);
                _dataExport_LBVariablesToExport.Add(item);

                if (!(item.QuestionVariableType.Equals("D")))
                {
                    Combo_conditional.Add(item);
                    _qstnvariablDD2.Add(item);
                    _qstnvariablDD3.Add(item);
                    _qstnvariablDD4.Add(item);
                    _qstnvariablDD5.Add(item);
                    _qstnvariablDD6.Add(item);

                    if (!(item.QuestionVariableType.Equals("N")) && !(item.QuestionVariableType.Equals("FA")))
                    {
                        _qstnvariablDD1.Add(item);
                        Combo_Classify.Add(item);
                    }

                }
            }


            dataExport_LBVariablesToExport = _dataExport_LBVariablesToExport;
            qstnvariablDD1 = _qstnvariablDD1;
            qstnvariablDD2 = _qstnvariablDD2;
            qstnvariablDD3 = _qstnvariablDD3;
            qstnvariablDD4 = _qstnvariablDD4;
            qstnvariablDD5 = _qstnvariablDD5;
            qstnvariablDD6 = _qstnvariablDD6;

        }
    }
}
