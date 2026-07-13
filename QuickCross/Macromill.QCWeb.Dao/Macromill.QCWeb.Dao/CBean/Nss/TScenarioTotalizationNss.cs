
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TScenarioTotalizationNss {

        protected TScenarioTotalizationCQ _query;
        public TScenarioTotalizationNss(TScenarioTotalizationCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TQcwebSurveyInfoNss WithTQcwebSurveyInfo() {
            _query.doNss(delegate() { return _query.QueryTQcwebSurveyInfo(); });
            return new TQcwebSurveyInfoNss(_query.QueryTQcwebSurveyInfo());
        }

        public TGtScenarioItemNss WithTGtScenarioItem() {
            _query.doNss(delegate() { return _query.QueryTGtScenarioItem(); });
            return new TGtScenarioItemNss(_query.QueryTGtScenarioItem());
        }

        public TCrossScenarioTargetNss WithTCrossScenarioTarget() {
            _query.doNss(delegate() { return _query.QueryTCrossScenarioTarget(); });
            return new TCrossScenarioTargetNss(_query.QueryTCrossScenarioTarget());
        }

        public TFaScenarioHeaderNss WithTFaScenarioHeader() {
            _query.doNss(delegate() { return _query.QueryTFaScenarioHeader(); });
            return new TFaScenarioHeaderNss(_query.QueryTFaScenarioHeader());
        }

        public TScenarioQuerylistNss WithTScenarioQuerylist() {
            _query.doNss(delegate() { return _query.QueryTScenarioQuerylist(); });
            return new TScenarioQuerylistNss(_query.QueryTScenarioQuerylist());
        }

        public TCategoryOutputEditNss WithTCategoryOutputEdit() {
            _query.doNss(delegate() { return _query.QueryTCategoryOutputEdit(); });
            return new TCategoryOutputEditNss(_query.QueryTCategoryOutputEdit());
        }

        public TGtMatrixInfoNss WithTGtMatrixInfo() {
            _query.doNss(delegate() { return _query.QueryTGtMatrixInfo(); });
            return new TGtMatrixInfoNss(_query.QueryTGtMatrixInfo());
        }

        public TDefaultEnvNss WithTDefaultEnv() {
            _query.doNss(delegate() { return _query.QueryTDefaultEnv(); });
            return new TDefaultEnvNss(_query.QueryTDefaultEnv());
        }


        // ===============================================================================
        //                                                      With Nested Referrer Table
        //                                                      ==========================
    }
}
