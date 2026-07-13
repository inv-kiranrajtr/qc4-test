
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TTableDetailInfoNss {

        protected TTableDetailInfoCQ _query;
        public TTableDetailInfoNss(TTableDetailInfoCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TTableControlNss WithTTableControl() {
            _query.doNss(delegate() { return _query.QueryTTableControl(); });
            return new TTableControlNss(_query.QueryTTableControl());
        }


        // ===============================================================================
        //                                                      With Nested Referrer Table
        //                                                      ==========================
    }
}
