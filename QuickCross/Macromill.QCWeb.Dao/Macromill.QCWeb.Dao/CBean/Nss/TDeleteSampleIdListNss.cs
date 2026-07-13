
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TDeleteSampleIdListNss {

        protected TDeleteSampleIdListCQ _query;
        public TDeleteSampleIdListNss(TDeleteSampleIdListCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TDeleteDataNss WithTDeleteData() {
            _query.doNss(delegate() { return _query.QueryTDeleteData(); });
            return new TDeleteDataNss(_query.QueryTDeleteData());
        }


        // ===============================================================================
        //                                                      With Nested Referrer Table
        //                                                      ==========================
    }
}
