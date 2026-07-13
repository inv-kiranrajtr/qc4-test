
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TTableControlNss {

        protected TTableControlCQ _query;
        public TTableControlNss(TTableControlCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================

        // ===============================================================================
        //                                                      With Nested Referrer Table
        //                                                      ==========================
        public TQcwebSurveyInfoNss WithTQcwebSurveyInfoAsOne() {
            _query.doNss(delegate() { return _query.QueryTQcwebSurveyInfoAsOne(); });
            return new TQcwebSurveyInfoNss(_query.QueryTQcwebSurveyInfoAsOne());
        }

    }
}
