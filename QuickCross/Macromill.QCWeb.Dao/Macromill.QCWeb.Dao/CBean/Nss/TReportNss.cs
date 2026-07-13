
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TReportNss {

        protected TReportCQ _query;
        public TReportNss(TReportCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TReportsetNss WithTReportset() {
            _query.doNss(delegate() { return _query.QueryTReportset(); });
            return new TReportsetNss(_query.QueryTReportset());
        }

        public TReportChildNss WithTReportChild() {
            _query.doNss(delegate() { return _query.QueryTReportChild(); });
            return new TReportChildNss(_query.QueryTReportChild());
        }


        // ===============================================================================
        //                                                      With Nested Referrer Table
        //                                                      ==========================
    }
}
