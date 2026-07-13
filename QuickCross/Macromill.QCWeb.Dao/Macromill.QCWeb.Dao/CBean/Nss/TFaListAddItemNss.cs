
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TFaListAddItemNss {

        protected TFaListAddItemCQ _query;
        public TFaListAddItemNss(TFaListAddItemCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TFaScenarioHeaderNss WithTFaScenarioHeader() {
            _query.doNss(delegate() { return _query.QueryTFaScenarioHeader(); });
            return new TFaScenarioHeaderNss(_query.QueryTFaScenarioHeader());
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
