
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TColorInfoDetailCrossNss {

        protected TColorInfoDetailCrossCQ _query;
        public TColorInfoDetailCrossNss(TColorInfoDetailCrossCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TColorSetInfoCrossNss WithTColorSetInfoCross() {
            _query.doNss(delegate() { return _query.QueryTColorSetInfoCross(); });
            return new TColorSetInfoCrossNss(_query.QueryTColorSetInfoCross());
        }


        // ===============================================================================
        //                                                      With Nested Referrer Table
        //                                                      ==========================
    }
}
