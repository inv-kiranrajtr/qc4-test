
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TDefaultEnvColorDtlNss {

        protected TDefaultEnvColorDtlCQ _query;
        public TDefaultEnvColorDtlNss(TDefaultEnvColorDtlCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TDefaultEnvColorInfoNss WithTDefaultEnvColorInfo() {
            _query.doNss(delegate() { return _query.QueryTDefaultEnvColorInfo(); });
            return new TDefaultEnvColorInfoNss(_query.QueryTDefaultEnvColorInfo());
        }


        // ===============================================================================
        //                                                      With Nested Referrer Table
        //                                                      ==========================
    }
}
