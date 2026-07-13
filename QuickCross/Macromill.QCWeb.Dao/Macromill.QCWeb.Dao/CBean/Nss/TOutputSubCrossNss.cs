
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TOutputSubCrossNss {

        protected TOutputSubCrossCQ _query;
        public TOutputSubCrossNss(TOutputSubCrossCQ query) { _query = query; }
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
