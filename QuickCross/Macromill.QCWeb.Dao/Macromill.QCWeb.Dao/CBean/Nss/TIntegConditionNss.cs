
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TIntegConditionNss {

        protected TIntegConditionCQ _query;
        public TIntegConditionNss(TIntegConditionCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TDataProcessNewItemNss WithTDataProcessNewItem() {
            _query.doNss(delegate() { return _query.QueryTDataProcessNewItem(); });
            return new TDataProcessNewItemNss(_query.QueryTDataProcessNewItem());
        }


        // ===============================================================================
        //                                                      With Nested Referrer Table
        //                                                      ==========================
    }
}
