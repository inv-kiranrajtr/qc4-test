
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TOutputTemplateNss {

        protected TOutputTemplateCQ _query;
        public TOutputTemplateNss(TOutputTemplateCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TOutputTemplateMasterNss WithTOutputTemplateMaster() {
            _query.doNss(delegate() { return _query.QueryTOutputTemplateMaster(); });
            return new TOutputTemplateMasterNss(_query.QueryTOutputTemplateMaster());
        }

        public TQcwebSurveyInfoNss WithTQcwebSurveyInfo() {
            _query.doNss(delegate() { return _query.QueryTQcwebSurveyInfo(); });
            return new TQcwebSurveyInfoNss(_query.QueryTQcwebSurveyInfo());
        }


        // ===============================================================================
        //                                                      With Nested Referrer Table
        //                                                      ==========================
    }
}
