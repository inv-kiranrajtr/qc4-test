
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TGtMatrixChildNss {

        protected TGtMatrixChildCQ _query;
        public TGtMatrixChildNss(TGtMatrixChildCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TGtMatrixInfoNss WithTGtMatrixInfo() {
            _query.doNss(delegate() { return _query.QueryTGtMatrixInfo(); });
            return new TGtMatrixInfoNss(_query.QueryTGtMatrixInfo());
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
