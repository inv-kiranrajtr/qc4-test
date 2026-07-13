
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TColorSetInfoCrossNss {

        protected TColorSetInfoCrossCQ _query;
        public TColorSetInfoCrossNss(TColorSetInfoCrossCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TCrossScenarioTargetNss WithTCrossScenarioTarget() {
            _query.doNss(delegate() { return _query.QueryTCrossScenarioTarget(); });
            return new TCrossScenarioTargetNss(_query.QueryTCrossScenarioTarget());
        }


        // ===============================================================================
        //                                                      With Nested Referrer Table
        //                                                      ==========================
    }
}
