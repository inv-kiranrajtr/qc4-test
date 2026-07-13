
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TCategoryOutputEditNss {

        protected TCategoryOutputEditCQ _query;
        public TCategoryOutputEditNss(TCategoryOutputEditCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TScenarioTotalizationNss WithTScenarioTotalization() {
            _query.doNss(delegate() { return _query.QueryTScenarioTotalization(); });
            return new TScenarioTotalizationNss(_query.QueryTScenarioTotalization());
        }

        public TCategoryOutputDetailNss WithTCategoryOutputDetail() {
            _query.doNss(delegate() { return _query.QueryTCategoryOutputDetail(); });
            return new TCategoryOutputDetailNss(_query.QueryTCategoryOutputDetail());
        }


        // ===============================================================================
        //                                                      With Nested Referrer Table
        //                                                      ==========================
    }
}
