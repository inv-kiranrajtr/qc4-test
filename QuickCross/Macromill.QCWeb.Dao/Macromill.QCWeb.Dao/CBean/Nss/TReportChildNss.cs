
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TReportChildNss {

        protected TReportChildCQ _query;
        public TReportChildNss(TReportChildCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TReportNss WithTReport() {
            _query.doNss(delegate() { return _query.QueryTReport(); });
            return new TReportNss(_query.QueryTReport());
        }


        // ===============================================================================
        //                                                      With Nested Referrer Table
        //                                                      ==========================
    }
}
