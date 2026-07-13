
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TItemInfoNss {

        protected TItemInfoCQ _query;
        public TItemInfoNss(TItemInfoCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TQcwebSurveyInfoNss WithTQcwebSurveyInfo() {
            _query.doNss(delegate() { return _query.QueryTQcwebSurveyInfo(); });
            return new TQcwebSurveyInfoNss(_query.QueryTQcwebSurveyInfo());
        }

        public TMatrixInfoNss WithTMatrixInfo() {
            _query.doNss(delegate() { return _query.QueryTMatrixInfo(); });
            return new TMatrixInfoNss(_query.QueryTMatrixInfo());
        }

        public TFaListAddItemNss WithTFaListAddItem() {
            _query.doNss(delegate() { return _query.QueryTFaListAddItem(); });
            return new TFaListAddItemNss(_query.QueryTFaListAddItem());
        }

        public TFaScenarioItemNss WithTFaScenarioItem() {
            _query.doNss(delegate() { return _query.QueryTFaScenarioItem(); });
            return new TFaScenarioItemNss(_query.QueryTFaScenarioItem());
        }

        public TTableControlNss WithTTableControl() {
            _query.doNss(delegate() { return _query.QueryTTableControl(); });
            return new TTableControlNss(_query.QueryTTableControl());
        }

        public TScenarioTotalizationNss WithTScenarioTotalization() {
            _query.doNss(delegate() { return _query.QueryTScenarioTotalization(); });
            return new TScenarioTotalizationNss(_query.QueryTScenarioTotalization());
        }

        public TDataEditListNss WithTDataEditList() {
            _query.doNss(delegate() { return _query.QueryTDataEditList(); });
            return new TDataEditListNss(_query.QueryTDataEditList());
        }


        // ===============================================================================
        //                                                      With Nested Referrer Table
        //                                                      ==========================
    }
}
