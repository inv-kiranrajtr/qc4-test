
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TOutputSettingNss {

        protected TOutputSettingCQ _query;
        public TOutputSettingNss(TOutputSettingCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TQcwebSurveyInfoNss WithTQcwebSurveyInfo() {
            _query.doNss(delegate() { return _query.QueryTQcwebSurveyInfo(); });
            return new TQcwebSurveyInfoNss(_query.QueryTQcwebSurveyInfo());
        }

        public TOutputHistoryItemNss WithTOutputHistoryItem() {
            _query.doNss(delegate() { return _query.QueryTOutputHistoryItem(); });
            return new TOutputHistoryItemNss(_query.QueryTOutputHistoryItem());
        }


        // ===============================================================================
        //                                                      With Nested Referrer Table
        //                                                      ==========================
        public TQcwebSurveyInfoNss WithTQcwebSurveyInfoAsOne() {
            _query.doNss(delegate() { return _query.QueryTQcwebSurveyInfoAsOne(); });
            return new TQcwebSurveyInfoNss(_query.QueryTQcwebSurveyInfoAsOne());
        }

    }
}
