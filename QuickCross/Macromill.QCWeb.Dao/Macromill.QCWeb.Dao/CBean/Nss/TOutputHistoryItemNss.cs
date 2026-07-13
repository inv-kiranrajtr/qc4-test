
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TOutputHistoryItemNss {

        protected TOutputHistoryItemCQ _query;
        public TOutputHistoryItemNss(TOutputHistoryItemCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TOutputSettingNss WithTOutputSetting() {
            _query.doNss(delegate() { return _query.QueryTOutputSetting(); });
            return new TOutputSettingNss(_query.QueryTOutputSetting());
        }


        // ===============================================================================
        //                                                      With Nested Referrer Table
        //                                                      ==========================
    }
}
