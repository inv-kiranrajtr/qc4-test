
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TColorSetInfoGtNss {

        protected TColorSetInfoGtCQ _query;
        public TColorSetInfoGtNss(TColorSetInfoGtCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TGtScenarioItemNss WithTGtScenarioItem() {
            _query.doNss(delegate() { return _query.QueryTGtScenarioItem(); });
            return new TGtScenarioItemNss(_query.QueryTGtScenarioItem());
        }


        // ===============================================================================
        //                                                      With Nested Referrer Table
        //                                                      ==========================
    }
}
