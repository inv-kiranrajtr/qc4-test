using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qc4Launcher.Forms.Cross_Tabulation
{
    public class CrossQuestionSetting
    {
        public int decid { get; set; }
        public int decid1 { get; set; }
        public decimal Id { get; set; }
        public string QuestionNumber { get; set; }
        public string QuestionType { get; set; }
        public string QuestionFlag { get; set; }
        public string QuestionFlagUpdated { get; set; }
        public int? QuestionCount { get; set; }
        public string Variable { get; set; }
        public string VariableBefore { get; set; }
        public string AnswerType { get; set; }
        public string AnswerTypeBefore { get; set; }
        public int CategoryCount { get; set; }
        public int CategoryCountBefore { get; set; }
        public int SeriallNumber { get; set; }
        public string TableHeading { get; set; }
        public string Sort { get; set; }
        public string Question { get; set; }
        public List<string> Choices { get; set; }
        public bool IsNew { get; set; }
        public int SubTotalCount { get; set; }
        public string AddSubTotal { get; set; }
        public string Score { get; set; }
        public string Count { get; set; }
        public string CountBase { get; set; }
        public int ItemId { get; set; }
        public string RowName { get; set; }
        public int RowNumber { get; set; }
        public bool IsFound { get; set; }
        public string Choice { get; set; }
        public int Choiceid { get; set; }
    }
}
