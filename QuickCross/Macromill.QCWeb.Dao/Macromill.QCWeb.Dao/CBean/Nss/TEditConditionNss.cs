
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TEditConditionNss {

        protected TEditConditionCQ _query;
        public TEditConditionNss(TEditConditionCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TEditDataNss WithTEditData() {
            _query.doNss(delegate() { return _query.QueryTEditData(); });
            return new TEditDataNss(_query.QueryTEditData());
        }


        // ===============================================================================
        //                                                      With Nested Referrer Table
        //                                                      ==========================
    }
}
