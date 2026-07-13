
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TOutputCommonNss {

        protected TOutputCommonCQ _query;
        public TOutputCommonNss(TOutputCommonCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TOutputRequestNss WithTOutputRequest() {
            _query.doNss(delegate() { return _query.QueryTOutputRequest(); });
            return new TOutputRequestNss(_query.QueryTOutputRequest());
        }

        public TOutputSubGtNss WithTOutputSubGt() {
            _query.doNss(delegate() { return _query.QueryTOutputSubGt(); });
            return new TOutputSubGtNss(_query.QueryTOutputSubGt());
        }

        public TOutputSubCrossNss WithTOutputSubCross() {
            _query.doNss(delegate() { return _query.QueryTOutputSubCross(); });
            return new TOutputSubCrossNss(_query.QueryTOutputSubCross());
        }

        public TOutputSubFaNss WithTOutputSubFa() {
            _query.doNss(delegate() { return _query.QueryTOutputSubFa(); });
            return new TOutputSubFaNss(_query.QueryTOutputSubFa());
        }

        public TOutputSubCklistNss WithTOutputSubCklist() {
            _query.doNss(delegate() { return _query.QueryTOutputSubCklist(); });
            return new TOutputSubCklistNss(_query.QueryTOutputSubCklist());
        }


        // ===============================================================================
        //                                                      With Nested Referrer Table
        //                                                      ==========================
    }
}
