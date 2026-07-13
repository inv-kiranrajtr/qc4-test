using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelAddIn.Model
{
	public class Question
	{
		public int Id { get; set; }
		public string VariableName { get; set; }
		public int IsNew { get; set; }
		public string QuestionNumber { get; set; }
		public int QuestionType { get; set; }
		public int QuestionCount { get; set; }
		public int AnswerType { get; set; }
		public int CategoriesCount { get; set; }
		public string Score { get; set; }
		public int Sort { get; set; }
		public string Column { get; set; }
		public string TableHeading { get; set; }
		public string QuestionSentence { get; set; }
		public int AddSubTotal { get; set; }
		public string Count { get; set; }
		public int CountBase { get; set; }
        internal Question(int id, string variable, string answerType, int categoryCount)
        {
            Id = id;
            VariableName = variable;
            //AnswerType = answerType;
            CategoriesCount = categoryCount;
        }
        public Question()
		{
		}
	}
}
