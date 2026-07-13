
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TGtScenarioItemNss {

        protected TGtScenarioItemCQ _query;
        public TGtScenarioItemNss(TGtScenarioItemCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TScenarioTotalizationNss WithTScenarioTotalization() {
            _query.doNss(delegate() { return _query.QueryTScenarioTotalization(); });
            return new TScenarioTotalizationNss(_query.QueryTScenarioTotalization());
        }

        public TItemInfoNss WithTItemInfo() {
            _query.doNss(delegate() { return _query.QueryTItemInfo(); });
            return new TItemInfoNss(_query.QueryTItemInfo());
        }


        // ===============================================================================
        //                                                      With Nested Referrer Table
        //                                                      ==========================
    }
}
