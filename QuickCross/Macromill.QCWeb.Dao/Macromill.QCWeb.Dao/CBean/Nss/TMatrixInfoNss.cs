
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TMatrixInfoNss {

        protected TMatrixInfoCQ _query;
        public TMatrixInfoNss(TMatrixInfoCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TItemInfoNss WithTItemInfoByItemInfoId() {
            _query.doNss(delegate() { return _query.QueryTItemInfoByItemInfoId(); });
            return new TItemInfoNss(_query.QueryTItemInfoByItemInfoId());
        }

        public TItemInfoNss WithTItemInfoByChildItemInfoId() {
            _query.doNss(delegate() { return _query.QueryTItemInfoByChildItemInfoId(); });
            return new TItemInfoNss(_query.QueryTItemInfoByChildItemInfoId());
        }

        public TCategoryInfoNss WithTCategoryInfo() {
            _query.doNss(delegate() { return _query.QueryTCategoryInfo(); });
            return new TCategoryInfoNss(_query.QueryTCategoryInfo());
        }


        // ===============================================================================
        //                                                      With Nested Referrer Table
        //                                                      ==========================
    }
}
