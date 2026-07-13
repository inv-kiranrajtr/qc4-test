
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TReportsetNss {

        protected TReportsetCQ _query;
        public TReportsetNss(TReportsetCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TQcwebSurveyInfoNss WithTQcwebSurveyInfo() {
            _query.doNss(delegate() { return _query.QueryTQcwebSurveyInfo(); });
            return new TQcwebSurveyInfoNss(_query.QueryTQcwebSurveyInfo());
        }

        public TReportNss WithTReport() {
            _query.doNss(delegate() { return _query.QueryTReport(); });
            return new TReportNss(_query.QueryTReport());
        }


        // ===============================================================================
        //                                                      With Nested Referrer Table
        //                                                      ==========================
    }
}
