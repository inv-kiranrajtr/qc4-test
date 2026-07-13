
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TDataEditListNss {

        protected TDataEditListCQ _query;
        public TDataEditListNss(TDataEditListCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TQcwebSurveyInfoNss WithTQcwebSurveyInfo() {
            _query.doNss(delegate() { return _query.QueryTQcwebSurveyInfo(); });
            return new TQcwebSurveyInfoNss(_query.QueryTQcwebSurveyInfo());
        }

        public TEditMenuMasterNss WithTEditMenuMaster() {
            _query.doNss(delegate() { return _query.QueryTEditMenuMaster(); });
            return new TEditMenuMasterNss(_query.QueryTEditMenuMaster());
        }


        // ===============================================================================
        //                                                      With Nested Referrer Table
        //                                                      ==========================
        public TDataProcessNewItemNss WithTDataProcessNewItemAsOne() {
            _query.doNss(delegate() { return _query.QueryTDataProcessNewItemAsOne(); });
            return new TDataProcessNewItemNss(_query.QueryTDataProcessNewItemAsOne());
        }

        public TDeleteDataNss WithTDeleteDataAsOne() {
            _query.doNss(delegate() { return _query.QueryTDeleteDataAsOne(); });
            return new TDeleteDataNss(_query.QueryTDeleteDataAsOne());
        }

        public TEditDataNss WithTEditDataAsOne() {
            _query.doNss(delegate() { return _query.QueryTEditDataAsOne(); });
            return new TEditDataNss(_query.QueryTEditDataAsOne());
        }

    }
}
