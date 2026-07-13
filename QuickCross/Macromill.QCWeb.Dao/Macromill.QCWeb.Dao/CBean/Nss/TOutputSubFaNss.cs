
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TOutputSubFaNss {

        protected TOutputSubFaCQ _query;
        public TOutputSubFaNss(TOutputSubFaCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TOutputCommonNss WithTOutputCommon() {
            _query.doNss(delegate() { return _query.QueryTOutputCommon(); });
            return new TOutputCommonNss(_query.QueryTOutputCommon());
        }


        // ===============================================================================
        //                                                      With Nested Referrer Table
        //                                                      ==========================
    }
}
