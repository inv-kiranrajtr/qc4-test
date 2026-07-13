
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TCrossScenarioTargetNss {

        protected TCrossScenarioTargetCQ _query;
        public TCrossScenarioTargetNss(TCrossScenarioTargetCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TScenarioTotalizationNss WithTScenarioTotalization() {
            _query.doNss(delegate() { return _query.QueryTScenarioTotalization(); });
            return new TScenarioTotalizationNss(_query.QueryTScenarioTotalization());
        }


        // ===============================================================================
        //                                                      With Nested Referrer Table
        //                                                      ==========================
    }
}
