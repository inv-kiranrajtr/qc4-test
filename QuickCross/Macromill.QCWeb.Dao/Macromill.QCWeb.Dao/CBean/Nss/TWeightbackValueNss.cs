
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TWeightbackValueNss {

        protected TWeightbackValueCQ _query;
        public TWeightbackValueNss(TWeightbackValueCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TWeightbackNss WithTWeightback() {
            _query.doNss(delegate() { return _query.QueryTWeightback(); });
            return new TWeightbackNss(_query.QueryTWeightback());
        }


        // ===============================================================================
        //                                                      With Nested Referrer Table
        //                                                      ==========================
    }
}
