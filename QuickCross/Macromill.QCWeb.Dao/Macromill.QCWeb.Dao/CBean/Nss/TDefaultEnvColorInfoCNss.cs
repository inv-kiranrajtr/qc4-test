
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TDefaultEnvColorInfoCNss {

        protected TDefaultEnvColorInfoCCQ _query;
        public TDefaultEnvColorInfoCNss(TDefaultEnvColorInfoCCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TDefaultEnvBaseNss WithTDefaultEnvBase() {
            _query.doNss(delegate() { return _query.QueryTDefaultEnvBase(); });
            return new TDefaultEnvBaseNss(_query.QueryTDefaultEnvBase());
        }


        // ===============================================================================
        //                                                      With Nested Referrer Table
        //                                                      ==========================
    }
}
