
using System;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.Nss {

    public class TQcwebSurveyInfoNss {

        protected TQcwebSurveyInfoCQ _query;
        public TQcwebSurveyInfoNss(TQcwebSurveyInfoCQ query) { _query = query; }
        public bool HasConditionQuery { get { return _query != null; } }

        // ===============================================================================
        //                                                       With Nested Foreign Table
        //                                                       =========================
        public TSurveyInfoNss WithTSurveyInfo() {
            _query.doNss(delegate() { return _query.QueryTSurveyInfo(); });
            return new TSurveyInfoNss(_query.QueryTSurveyInfo());
        }

        public TRawdataImportQueInfoNss WithTRawdataImportQueInfo() {
            _query.doNss(delegate() { return _query.QueryTRawdataImportQueInfo(); });
            return new TRawdataImportQueInfoNss(_query.QueryTRawdataImportQueInfo());
        }

        public TAllocationCellInfoNss WithTAllocationCellInfo() {
            _query.doNss(delegate() { return _query.QueryTAllocationCellInfo(); });
            return new TAllocationCellInfoNss(_query.QueryTAllocationCellInfo());
        }

        public TSelectConditionInfoNss WithTSelectConditionInfo() {
            _query.doNss(delegate() { return _query.QueryTSelectConditionInfo(); });
            return new TSelectConditionInfoNss(_query.QueryTSelectConditionInfo());
        }

        public TItemInfoNss WithTItemInfo() {
            _query.doNss(delegate() { return _query.QueryTItemInfo(); });
            return new TItemInfoNss(_query.QueryTItemInfo());
        }

        public TTableControlNss WithTTableControl() {
            _query.doNss(delegate() { return _query.QueryTTableControl(); });
            return new TTableControlNss(_query.QueryTTableControl());
        }

        public TDefaultEnvNss WithTDefaultEnv() {
            _query.doNss(delegate() { return _query.QueryTDefaultEnv(); });
            return new TDefaultEnvNss(_query.QueryTDefaultEnv());
        }

        public TDefaultEnvColorInfoNss WithTDefaultEnvColorInfo() {
            _query.doNss(delegate() { return _query.QueryTDefaultEnvColorInfo(); });
            return new TDefaultEnvColorInfoNss(_query.QueryTDefaultEnvColorInfo());
        }

        public TScenarioTotalizationNss WithTScenarioTotalization() {
            _query.doNss(delegate() { return _query.QueryTScenarioTotalization(); });
            return new TScenarioTotalizationNss(_query.QueryTScenarioTotalization());
        }

        public TReportsetNss WithTReportset() {
            _query.doNss(delegate() { return _query.QueryTReportset(); });
            return new TReportsetNss(_query.QueryTReportset());
        }

        public TDataEditListNss WithTDataEditList() {
            _query.doNss(delegate() { return _query.QueryTDataEditList(); });
            return new TDataEditListNss(_query.QueryTDataEditList());
        }

        public TOutputSettingNss WithTOutputSetting() {
            _query.doNss(delegate() { return _query.QueryTOutputSetting(); });
            return new TOutputSettingNss(_query.QueryTOutputSetting());
        }

        public TOutputRequestNss WithTOutputRequest() {
            _query.doNss(delegate() { return _query.QueryTOutputRequest(); });
            return new TOutputRequestNss(_query.QueryTOutputRequest());
        }

        public TAccessPermissionsInfoNss WithTAccessPermissionsInfo() {
            _query.doNss(delegate() { return _query.QueryTAccessPermissionsInfo(); });
            return new TAccessPermissionsInfoNss(_query.QueryTAccessPermissionsInfo());
        }

        public TSessionControlerNss WithTSessionControler() {
            _query.doNss(delegate() { return _query.QueryTSessionControler(); });
            return new TSessionControlerNss(_query.QueryTSessionControler());
        }

        public TNoticeNss WithTNotice() {
            _query.doNss(delegate() { return _query.QueryTNotice(); });
            return new TNoticeNss(_query.QueryTNotice());
        }

        public TOutputSettingGtNss WithTOutputSettingGt() {
            _query.doNss(delegate() { return _query.QueryTOutputSettingGt(); });
            return new TOutputSettingGtNss(_query.QueryTOutputSettingGt());
        }

        public TOutputSettingCrossNss WithTOutputSettingCross() {
            _query.doNss(delegate() { return _query.QueryTOutputSettingCross(); });
            return new TOutputSettingCrossNss(_query.QueryTOutputSettingCross());
        }

        public TOutputSettingFaNss WithTOutputSettingFa() {
            _query.doNss(delegate() { return _query.QueryTOutputSettingFa(); });
            return new TOutputSettingFaNss(_query.QueryTOutputSettingFa());
        }

        public TOutputSettingReportNss WithTOutputSettingReport() {
            _query.doNss(delegate() { return _query.QueryTOutputSettingReport(); });
            return new TOutputSettingReportNss(_query.QueryTOutputSettingReport());
        }

        public TQcwebSurveyDetailNss WithTQcwebSurveyDetail() {
            _query.doNss(delegate() { return _query.QueryTQcwebSurveyDetail(); });
            return new TQcwebSurveyDetailNss(_query.QueryTQcwebSurveyDetail());
        }


        // ===============================================================================
        //                                                      With Nested Referrer Table
        //                                                      ==========================
        public TAccessPermissionsInfoNss WithTAccessPermissionsInfoAsOne() {
            _query.doNss(delegate() { return _query.QueryTAccessPermissionsInfoAsOne(); });
            return new TAccessPermissionsInfoNss(_query.QueryTAccessPermissionsInfoAsOne());
        }

        public TOutputSettingNss WithTOutputSettingAsOne() {
            _query.doNss(delegate() { return _query.QueryTOutputSettingAsOne(); });
            return new TOutputSettingNss(_query.QueryTOutputSettingAsOne());
        }

        public TOutputSettingCrossNss WithTOutputSettingCrossAsOne() {
            _query.doNss(delegate() { return _query.QueryTOutputSettingCrossAsOne(); });
            return new TOutputSettingCrossNss(_query.QueryTOutputSettingCrossAsOne());
        }

        public TOutputSettingFaNss WithTOutputSettingFaAsOne() {
            _query.doNss(delegate() { return _query.QueryTOutputSettingFaAsOne(); });
            return new TOutputSettingFaNss(_query.QueryTOutputSettingFaAsOne());
        }

        public TOutputSettingGtNss WithTOutputSettingGtAsOne() {
            _query.doNss(delegate() { return _query.QueryTOutputSettingGtAsOne(); });
            return new TOutputSettingGtNss(_query.QueryTOutputSettingGtAsOne());
        }

        public TOutputSettingReportNss WithTOutputSettingReportAsOne() {
            _query.doNss(delegate() { return _query.QueryTOutputSettingReportAsOne(); });
            return new TOutputSettingReportNss(_query.QueryTOutputSettingReportAsOne());
        }

    }
}
