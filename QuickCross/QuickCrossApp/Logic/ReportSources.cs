using Macromill.QCWeb.ReportRequest;
using Macromill.QCWeb.Tabulation;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;

namespace Qc4Launcher.Logic
{
    public class ReportSources
    {
        private List<ReportSource> ThisCollection;
        private string ThisLocationCode;

        public ReportSources()
        {
            ThisCollection = new List<ReportSource>();
        }



        public ReportSource Item(int Index)
        {
            ReportSource rs = null;
            try
            {
                rs = ThisCollection[Index];
            }
            catch (Exception ex) { }
            return rs;
        }

        public long Count()
        {
            return ThisCollection.Count;
        }

        //public function newenum() as stdole.iunknown
        //attribute newenum.vb_usermemid = -4
        //    set newenum = thiscollection.[_newenum]
        //end function

        public ReportSource Add(
                string QuestionName, string QuestionDescription
              , Range TableRange
              , KeyItemInformation KeyItem = null
              , string Comment = null
              , ChartObject ChartObject = null
              , QuestionType QuestionType = 0
              , string RuleDescription = null
              , string FilteringCriteriaDescription = null
              )
        {
            ReportSource tmpSource = new ReportSource();
            tmpSource.Init(this, QuestionName, QuestionDescription, TableRange);
            tmpSource.KeyItem(KeyItem);
            tmpSource.Comment(Comment);
            tmpSource.ChartObject(ChartObject);
            tmpSource.SetQuestionType(QuestionType);
            tmpSource.RuleDescription(RuleDescription);
            tmpSource.FilteringCriteriaDescription(FilteringCriteriaDescription);
            ThisCollection.Add(tmpSource);
            return tmpSource;
        }

        public string LocationCode()
        {
            return ThisLocationCode;
        }

        public void LocationCode(string Arg)
        {
            ThisLocationCode = Arg;
        }

    }



}