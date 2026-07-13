//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ExcelAddIn.Sheets
//{
//    public class QuestionSettings
//    {
//		public decimal Id { get; set; }
//        public string QuestionNumber { get; set; }
//        public string QuestionType { get; set; }
//        public int? QuestionCount { get; set; }
//        public string Variable { get; set; }
//        public string VariableBefore { get; set; }
//		public string AnswerType { get; set; }
//		public string AnswerTypeBefore { get; set; }
//		public int CategoryCount { get; set; }
//		public int CategoryCountBefore { get; set; }
//		public int SeriallNumber { get; set; }
//		public string TableHeading { get; set; }
//		public string WT { get; set; }
//        public string Question { get; set; }
//        public List<string> Choices { get; set; }
//        public bool  IsNew { get; set; }
//		public int SubTotalCount { get; set; }
//        public string Score { get; set; }
//        public string Count { get; set; }
//        public string CountBase { get; set; }
//        public int ItemId { get; set; }
//		public string RowName { get; set; }

//        public List<SubTotal> SubTotals { get; set; }

//        public class SubTotal
//        {
//            public string Subtotal { get; set; }
//            public string Criteria { get; set; }

//            public SubTotal(string subTotal, string criteria)
//            {
//                Subtotal = subTotal;
//                Criteria = criteria;
//            }
//        }

//        public QuestionSettings()
//        {
//			Id = 0;
//            QuestionNumber = "";
//            QuestionType = "";
//            QuestionCount = null;
//            Variable = "";
//			VariableBefore = "";
//			AnswerType = "";
//			AnswerTypeBefore = "";
//			CategoryCount = 0;
//			CategoryCountBefore = 0;
//			WT = "";
//			Score = "";
//			Count = "";
//			CountBase = "";
//			SeriallNumber = 0;
//            TableHeading = "";
//            Question = "";
//			SubTotalCount = 0;
//			Choices = new List<string>();
//            IsNew = false;
//            ItemId = 0;
//			RowName = "";

//		}

//		public QuestionSettings(string variable)
//		{
//			Id = 0;
//			QuestionNumber = "";
//			QuestionType = "";
//			QuestionCount = null;
//			Variable = variable;
//			VariableBefore = "";
//			AnswerType = "";
//			AnswerTypeBefore = "";
//			CategoryCount = 0;
//			CategoryCountBefore = 0;
//			SeriallNumber = 0;
//			TableHeading = "";
//			Question = "";
//			WT = "";
//			Score = "";
//			Count = "";
//			CountBase = "";
//			SubTotalCount = 0;
//			Choices = new List<string>();
//			IsNew = false;
//            ItemId = 0;
//			RowName = "";
//		}

//		public QuestionSettings Clone()
//		{
//			QuestionSettings qs = new QuestionSettings();
//			qs.Id = this.Id;
//			qs.QuestionNumber = this.QuestionNumber;
//			qs.QuestionType = this.QuestionType;
//			qs.QuestionCount = this.QuestionCount;
//			qs.Variable = this.Variable;
//			qs.VariableBefore = "";
//			qs.CategoryCountBefore = 0;
//			qs.AnswerType = this.AnswerType;
//			qs.AnswerTypeBefore = "";
//			qs.CategoryCount = this.CategoryCount;
//			qs.SeriallNumber = this.SeriallNumber;
//			qs.TableHeading = this.TableHeading;
//			qs.Question = this.Question;
//			qs.Count = this.Count;
//			qs.Score = this.Score;
//			qs.CountBase = this.CountBase;
//			qs.WT = this.WT;
//			qs.Choices = this.Choices;
//			qs.IsNew = this.IsNew;
//			qs.SubTotalCount = this.SubTotalCount;
//          //  qs.ItemId = this.ItemId;
//			return qs;
//		}

//		public QuestionSettings(int id, string variable)
//		{
//			this.Id = id;
//			this.VariableBefore = variable;
//		}
//    }
//}
