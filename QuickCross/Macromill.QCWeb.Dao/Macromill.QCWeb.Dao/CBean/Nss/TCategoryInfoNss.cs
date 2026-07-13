
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TCategoryInfoNss {

        protected TCategoryInfoCQ _query;
        public TCategoryInfoNss(TCategoryInfoCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TItemInfoNss WithTItemInfo() {
            _query.doNss(delegate() { return _query.QueryTItemInfo(); });
            return new TItemInfoNss(_query.QueryTItemInfo());
        }


        // ===============================================================================
        //                                                      With Nested Referrer Table
        //                                                      ==========================
    }
}
