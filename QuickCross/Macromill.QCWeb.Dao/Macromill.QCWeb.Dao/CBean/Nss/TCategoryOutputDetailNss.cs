
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TCategoryOutputDetailNss {

        protected TCategoryOutputDetailCQ _query;
        public TCategoryOutputDetailNss(TCategoryOutputDetailCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TCategoryOutputEditNss WithTCategoryOutputEdit() {
            _query.doNss(delegate() { return _query.QueryTCategoryOutputEdit(); });
            return new TCategoryOutputEditNss(_query.QueryTCategoryOutputEdit());
        }


        // ===============================================================================
        //                                                      With Nested Referrer Table
        //                                                      ==========================
    }
}
