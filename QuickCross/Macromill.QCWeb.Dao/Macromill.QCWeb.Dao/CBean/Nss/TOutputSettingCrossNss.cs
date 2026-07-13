
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TOutputSettingCrossNss {

        protected TOutputSettingCrossCQ _query;
        public TOutputSettingCrossNss(TOutputSettingCrossCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TQcwebSurveyInfoNss WithTQcwebSurveyInfo() {
            _query.doNss(delegate() { return _query.QueryTQcwebSurveyInfo(); });
            return new TQcwebSurveyInfoNss(_query.QueryTQcwebSurveyInfo());
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
