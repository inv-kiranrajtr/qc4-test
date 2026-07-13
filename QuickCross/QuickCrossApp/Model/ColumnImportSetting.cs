using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Qc4Launcher.Util.Enums;

namespace Qc4Launcher.Model
{
    [Serializable]
    public class ColumnImportSettings
    {
        public DataTable BeforeProcessingData { get; set; }
        public DataTable AfterProcessingData { get; set; }
        public List<ImportInformation> ImportInformations { get; set; }
        public int SelectedIndex { get; set; }
        public string SelectedColumn { get; set; }
        public MAFormat MAformat { get; set; }
        public string NotApplicableCharacter { get; set; }

        public string DestinationFileKey1 { get; set; }
        public string DestinationFileKey2 { get; set; }
        public string SourceFileKey1 { get; set; }
        public string SourceFileKey2 { get; set; }

        public bool IsDataProcessed { get; set; }
        public string NotApplicable { get; set; }

        public int DataCount { get; set; }

    }

    [Serializable]
    public class ImportInformation
    {
        public int ColumnIndex { get; set; }
        public string ColumnName { get; set; }
        public string VariableName { get; set; }
        public string AnswerType { get; set; }
        public int NoOfChoices { get; set; }
        public string QuestionTitle { get; set; }
        public string QuestionSentence { get; set; }
        public List<ChoiceWording> ChoiceWordings { get; set; }
        public bool IsDataValidated { get; set; }
        public bool IsColumnSetForFlagFormat { get; set; }

        public string DBColumn { get; set; }
        public int DBQuestionId { get; set; }

        public List<string> MAColumns { get; set; }

    }

    //public class AnswerRowData
    //{
    //    public List<AnswerColumnData> AnswerColumnDatas { get; set; } // multiple colums in a single row
    //    public string DBRowId { get; set; }
    //}

    [Serializable]
    public class ChoiceWording
    {
        public int SlNo { get; set; }
        public string WordingText { get; set; }
    }


    //public class AnswerColumnData
    //{
    //    public string ColumnName { get; set; }
    //    public string ColumnValue { get; set; }
    //}



}
