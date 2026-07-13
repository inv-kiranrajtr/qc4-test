
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TColorInfoDetailGtNss {

        protected TColorInfoDetailGtCQ _query;
        public TColorInfoDetailGtNss(TColorInfoDetailGtCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TColorSetInfoGtNss WithTColorSetInfoGt() {
            _query.doNss(delegate() { return _query.QueryTColorSetInfoGt(); });
            return new TColorSetInfoGtNss(_query.QueryTColorSetInfoGt());
        }


        // ===============================================================================
        //                                                      With Nested Referrer Table
        //                                                      ==========================
    }
}
