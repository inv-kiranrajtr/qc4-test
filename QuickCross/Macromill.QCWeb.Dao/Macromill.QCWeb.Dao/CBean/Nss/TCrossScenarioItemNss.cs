
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TCrossScenarioItemNss {

        protected TCrossScenarioItemCQ _query;
        public TCrossScenarioItemNss(TCrossScenarioItemCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TCrossScenarioTargetNss WithTCrossScenarioTarget() {
            _query.doNss(delegate() { return _query.QueryTCrossScenarioTarget(); });
            return new TCrossScenarioTargetNss(_query.QueryTCrossScenarioTarget());
        }

        public TPolylineCategoryListNss WithTPolylineCategoryList() {
            _query.doNss(delegate() { return _query.QueryTPolylineCategoryList(); });
            return new TPolylineCategoryListNss(_query.QueryTPolylineCategoryList());
        }


        // ===============================================================================
        //                                                      With Nested Referrer Table
        //                                                      ==========================
    }
}
