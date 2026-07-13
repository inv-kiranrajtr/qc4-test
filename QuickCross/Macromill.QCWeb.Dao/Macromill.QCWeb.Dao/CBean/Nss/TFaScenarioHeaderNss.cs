
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TFaScenarioHeaderNss {

        protected TFaScenarioHeaderCQ _query;
        public TFaScenarioHeaderNss(TFaScenarioHeaderCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TScenarioTotalizationNss WithTScenarioTotalization() {
            _query.doNss(delegate() { return _query.QueryTScenarioTotalization(); });
            return new TScenarioTotalizationNss(_query.QueryTScenarioTotalization());
        }

        public TFaScenarioItemNss WithTFaScenarioItem() {
            _query.doNss(delegate() { return _query.QueryTFaScenarioItem(); });
            return new TFaScenarioItemNss(_query.QueryTFaScenarioItem());
        }

        public TFaListAddItemNss WithTFaListAddItem() {
            _query.doNss(delegate() { return _query.QueryTFaListAddItem(); });
            return new TFaListAddItemNss(_query.QueryTFaListAddItem());
        }


        // ===============================================================================
        //                                                      With Nested Referrer Table
        //                                                      ==========================
    }
}
