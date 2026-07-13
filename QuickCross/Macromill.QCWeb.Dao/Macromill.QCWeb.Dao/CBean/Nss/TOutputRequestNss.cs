
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TOutputRequestNss {

        protected TOutputRequestCQ _query;
        public TOutputRequestNss(TOutputRequestCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TQcwebSurveyInfoNss WithTQcwebSurveyInfo() {
            _query.doNss(delegate() { return _query.QueryTQcwebSurveyInfo(); });
            return new TQcwebSurveyInfoNss(_query.QueryTQcwebSurveyInfo());
        }

        public TOutputReportsetInfoNss WithTOutputReportsetInfo() {
            _query.doNss(delegate() { return _query.QueryTOutputReportsetInfo(); });
            return new TOutputReportsetInfoNss(_query.QueryTOutputReportsetInfo());
        }

        public TOutputCommonNss WithTOutputCommon() {
            _query.doNss(delegate() { return _query.QueryTOutputCommon(); });
            return new TOutputCommonNss(_query.QueryTOutputCommon());
        }


        // ===============================================================================
        //                                                      With Nested Referrer Table
        //                                                      ==========================
    }
}
