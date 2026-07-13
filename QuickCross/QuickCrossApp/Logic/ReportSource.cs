using Macromill.QCWeb.ReportRequest;
using Macromill.QCWeb.Tabulation;
using Microsoft.Office.Interop.Excel;

namespace Qc4Launcher.Logic
{
    public class ReportSource
    {

        private string ThisQuestionName;
        private string ThisQuestionDescription;
        private QuestionType ThisQuestionType;
        private string ThisQuestionTypeDescription;
        private Range ThisTableRange;
        private ChartObject ThisChartObject;
        private string ThisComment;
        private string ThisRuleDescription;
        private KeyItemInformation ThisKeyItem;
        private string ThisFilteringCriteriaDescription;
        private ReportSources ThisParentCollection;


        public void Init(ReportSources ParentCollection
              , string QuestionName, string QuestionDescription
              , Range TableRange)
        {
            ThisParentCollection = ParentCollection;
            ThisQuestionName = QuestionName;
            ThisQuestionDescription = QuestionDescription;
            ThisTableRange = TableRange;
        }

        public ReportSources ParentCollection()
        {
            return ThisParentCollection;
        }

        public Range TableRange()
        {
            return ThisTableRange;
        }

        public ChartObject ChartObject()
        {
            return ThisChartObject;
        }

        public void ChartObject(ChartObject Arg)
        {
            ThisChartObject = Arg;
        }


        public string QuestionName()
        {
            return ThisQuestionName;
        }

        public string QuestionDescription(bool WithQuestionType = true)
        {
            return ThisQuestionDescription + (WithQuestionType ? ThisQuestionTypeDescription : "");
        }

        public QuestionType Questiontype()
        {
            return ThisQuestionType;
        }

        public string QuestionTypeDescription()
        {
            return ThisQuestionTypeDescription;
        }

        public string Comment()
        {
            return ThisComment;
        }

        public void Comment(string Arg)
        {
            ThisComment = Arg;
        }

        public string RuleDescription()
        {
            return ThisRuleDescription;
        }

        public void RuleDescription(string Arg)
        {
            ThisRuleDescription = Arg;
        }

        public KeyItemInformation KeyItem()
        {
            return ThisKeyItem;
        }

        public void KeyItem(KeyItemInformation Arg)
        {
            ThisKeyItem = Arg;
        }

        public string FilteringCriteriaDescription()
        {
            return ThisFilteringCriteriaDescription;
        }

        public void FilteringCriteriaDescription(string Arg)
        {
            ThisFilteringCriteriaDescription = Arg;
        }

        public void SetQuestionType(QuestionType Arg)
        {
            ThisQuestionType = Arg & (QuestionType.SA | QuestionType.MA | QuestionType.N | QuestionType.FA);
            ThisQuestionTypeDescription = null;

            switch (ThisQuestionType)
            {
                case QuestionType.SA:
                    ThisQuestionTypeDescription = "SA"; ;
                    break;
                case QuestionType.MA:
                    ThisQuestionTypeDescription = "MA";
                    break;
                case QuestionType.N:
                    ThisQuestionTypeDescription = "N";
                    break;
                case QuestionType.FA:
                    ThisQuestionTypeDescription = "FA";
                    break;
                default:
                    return;
            }
            ThisQuestionTypeDescription = "[" + ThisQuestionTypeDescription + "]";
        }

    }
}