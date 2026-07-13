
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TDefaultEnvColorDtlCNss {

        protected TDefaultEnvColorDtlCCQ _query;
        public TDefaultEnvColorDtlCNss(TDefaultEnvColorDtlCCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TDefaultEnvColorInfoCNss WithTDefaultEnvColorInfoC() {
            _query.doNss(delegate() { return _query.QueryTDefaultEnvColorInfoC(); });
            return new TDefaultEnvColorInfoCNss(_query.QueryTDefaultEnvColorInfoC());
        }


        // ===============================================================================
        //                                                      With Nested Referrer Table
        //                                                      ==========================
    }
}
