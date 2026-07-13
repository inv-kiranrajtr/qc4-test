using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qc4Launcher.Model
{
	class Question
	{
		public int Id { get; set; }
		public string Variable { get; set; }
		public string AnswerType { get; set; }
		public int CategoryCount { get; set; }
		public string QFlag { get; set; }

		internal Question(int id, string variable, string answerType, int categoryCount)
		{
			Id = id;
			Variable = variable;
			AnswerType = answerType;
			CategoryCount = categoryCount;
		}

		internal Question(int id, string variable, string answerType, int categoryCount, string qFlag)
		{
			Id = id;
			Variable = variable;
			AnswerType = answerType;
			CategoryCount = categoryCount;
			QFlag = qFlag;
		}
	}
}
