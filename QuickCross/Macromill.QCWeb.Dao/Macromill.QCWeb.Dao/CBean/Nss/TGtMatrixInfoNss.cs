
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TGtMatrixInfoNss {

        protected TGtMatrixInfoCQ _query;
        public TGtMatrixInfoNss(TGtMatrixInfoCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TScenarioTotalizationNss WithTScenarioTotalization() {
            _query.doNss(delegate() { return _query.QueryTScenarioTotalization(); });
            return new TScenarioTotalizationNss(_query.QueryTScenarioTotalization());
        }

        public TGtMatrixChildNss WithTGtMatrixChild() {
            _query.doNss(delegate() { return _query.QueryTGtMatrixChild(); });
            return new TGtMatrixChildNss(_query.QueryTGtMatrixChild());
        }


        // ===============================================================================
        //                                                      With Nested Referrer Table
        //                                                      ==========================
    }
}
