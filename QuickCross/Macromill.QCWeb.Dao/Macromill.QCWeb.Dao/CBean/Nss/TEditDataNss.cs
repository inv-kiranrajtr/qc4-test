
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TEditDataNss {

        protected TEditDataCQ _query;
        public TEditDataNss(TEditDataCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TDataEditListNss WithTDataEditList() {
            _query.doNss(delegate() { return _query.QueryTDataEditList(); });
            return new TDataEditListNss(_query.QueryTDataEditList());
        }


        // ===============================================================================
        //                                                      With Nested Referrer Table
        //                                                      ==========================
    }
}
