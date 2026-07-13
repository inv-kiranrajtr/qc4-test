
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TDefaultEnvColorInfoNss {

        protected TDefaultEnvColorInfoCQ _query;
        public TDefaultEnvColorInfoNss(TDefaultEnvColorInfoCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TDefaultEnvNss WithTDefaultEnv() {
            _query.doNss(delegate() { return _query.QueryTDefaultEnv(); });
            return new TDefaultEnvNss(_query.QueryTDefaultEnv());
        }

        public TDefaultEnvColorDtlNss WithTDefaultEnvColorDtl() {
            _query.doNss(delegate() { return _query.QueryTDefaultEnvColorDtl(); });
            return new TDefaultEnvColorDtlNss(_query.QueryTDefaultEnvColorDtl());
        }


        // ===============================================================================
        //                                                      With Nested Referrer Table
        //                                                      ==========================
    }
}
