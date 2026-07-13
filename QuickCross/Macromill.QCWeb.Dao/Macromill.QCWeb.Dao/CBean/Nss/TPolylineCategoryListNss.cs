
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TPolylineCategoryListNss {

        protected TPolylineCategoryListCQ _query;
        public TPolylineCategoryListNss(TPolylineCategoryListCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TCrossScenarioItemNss WithTCrossScenarioItem() {
            _query.doNss(delegate() { return _query.QueryTCrossScenarioItem(); });
            return new TCrossScenarioItemNss(_query.QueryTCrossScenarioItem());
        }


        // ===============================================================================
        //                                                      With Nested Referrer Table
        //                                                      ==========================
    }
}
