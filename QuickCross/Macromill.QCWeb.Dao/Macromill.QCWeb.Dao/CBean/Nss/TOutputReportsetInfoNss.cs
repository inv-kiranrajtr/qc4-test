
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TOutputReportsetInfoNss {

        protected TOutputReportsetInfoCQ _query;
        public TOutputReportsetInfoNss(TOutputReportsetInfoCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TOutputTemplateNss WithTOutputTemplate() {
            _query.doNss(delegate() { return _query.QueryTOutputTemplate(); });
            return new TOutputTemplateNss(_query.QueryTOutputTemplate());
        }


        // ===============================================================================
        //                                                      With Nested Referrer Table
        //                                                      ==========================
    }
}
