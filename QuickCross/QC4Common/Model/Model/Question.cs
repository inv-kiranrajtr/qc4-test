using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QC4Common.Model.Model
{
    public class Question
	{
		public int Id { get; set; }
		public string Variable { get; set; }
		public string AnswerType { get; set; }
		public int CategoryCount { get; set; }

		public Question(int id, string variable, string answerType, int categoryCount)
		{
			Id = id;
			Variable = variable;
			AnswerType = answerType;
			CategoryCount = categoryCount;
		}
	}
}
