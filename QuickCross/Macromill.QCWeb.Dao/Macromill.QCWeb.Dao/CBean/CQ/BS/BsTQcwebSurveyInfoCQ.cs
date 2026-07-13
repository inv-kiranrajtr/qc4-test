
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTQcwebSurveyInfoCQ : AbstractBsTQcwebSurveyInfoCQ {

        protected TQcwebSurveyInfoCIQ _inlineQuery;

        public BsTQcwebSurveyInfoCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TQcwebSurveyInfoCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TQcwebSurveyInfoCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TQcwebSurveyInfoCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TQcwebSurveyInfoCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _qcwebid;
        public ConditionValue Qcwebid {
            get { if (_qcwebid == null) { _qcwebid = new ConditionValue(); } return _qcwebid; }
        }
        protected override ConditionValue getCValueQcwebid() { return this.Qcwebid; }


        protected Map<String, TAccessPermissionsInfoCQ> _qcwebid_ExistsSubQuery_TAccessPermissionsInfoAsOneMap;
        public Map<String, TAccessPermissionsInfoCQ> Qcwebid_ExistsSubQuery_TAccessPermissionsInfoAsOne { get { return _qcwebid_ExistsSubQuery_TAccessPermissionsInfoAsOneMap; }}
        public override String keepQcwebid_ExistsSubQuery_TAccessPermissionsInfoAsOne(TAccessPermissionsInfoCQ subQuery) {
            if (_qcwebid_ExistsSubQuery_TAccessPermissionsInfoAsOneMap == null) { _qcwebid_ExistsSubQuery_TAccessPermissionsInfoAsOneMap = new LinkedHashMap<String, TAccessPermissionsInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_ExistsSubQuery_TAccessPermissionsInfoAsOneMap.size() + 1);
            _qcwebid_ExistsSubQuery_TAccessPermissionsInfoAsOneMap.put(key, subQuery); return "Qcwebid_ExistsSubQuery_TAccessPermissionsInfoAsOne." + key;
        }

        protected Map<String, TAllocationCellInfoCQ> _qcwebid_ExistsSubQuery_TAllocationCellInfoListMap;
        public Map<String, TAllocationCellInfoCQ> Qcwebid_ExistsSubQuery_TAllocationCellInfoList { get { return _qcwebid_ExistsSubQuery_TAllocationCellInfoListMap; }}
        public override String keepQcwebid_ExistsSubQuery_TAllocationCellInfoList(TAllocationCellInfoCQ subQuery) {
            if (_qcwebid_ExistsSubQuery_TAllocationCellInfoListMap == null) { _qcwebid_ExistsSubQuery_TAllocationCellInfoListMap = new LinkedHashMap<String, TAllocationCellInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_ExistsSubQuery_TAllocationCellInfoListMap.size() + 1);
            _qcwebid_ExistsSubQuery_TAllocationCellInfoListMap.put(key, subQuery); return "Qcwebid_ExistsSubQuery_TAllocationCellInfoList." + key;
        }

        protected Map<String, TDataEditListCQ> _qcwebid_ExistsSubQuery_TDataEditListListMap;
        public Map<String, TDataEditListCQ> Qcwebid_ExistsSubQuery_TDataEditListList { get { return _qcwebid_ExistsSubQuery_TDataEditListListMap; }}
        public override String keepQcwebid_ExistsSubQuery_TDataEditListList(TDataEditListCQ subQuery) {
            if (_qcwebid_ExistsSubQuery_TDataEditListListMap == null) { _qcwebid_ExistsSubQuery_TDataEditListListMap = new LinkedHashMap<String, TDataEditListCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_ExistsSubQuery_TDataEditListListMap.size() + 1);
            _qcwebid_ExistsSubQuery_TDataEditListListMap.put(key, subQuery); return "Qcwebid_ExistsSubQuery_TDataEditListList." + key;
        }

        protected Map<String, TItemInfoCQ> _qcwebid_ExistsSubQuery_TItemInfoListMap;
        public Map<String, TItemInfoCQ> Qcwebid_ExistsSubQuery_TItemInfoList { get { return _qcwebid_ExistsSubQuery_TItemInfoListMap; }}
        public override String keepQcwebid_ExistsSubQuery_TItemInfoList(TItemInfoCQ subQuery) {
            if (_qcwebid_ExistsSubQuery_TItemInfoListMap == null) { _qcwebid_ExistsSubQuery_TItemInfoListMap = new LinkedHashMap<String, TItemInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_ExistsSubQuery_TItemInfoListMap.size() + 1);
            _qcwebid_ExistsSubQuery_TItemInfoListMap.put(key, subQuery); return "Qcwebid_ExistsSubQuery_TItemInfoList." + key;
        }

        protected Map<String, TNoticeCQ> _qcwebid_ExistsSubQuery_TNoticeListMap;
        public Map<String, TNoticeCQ> Qcwebid_ExistsSubQuery_TNoticeList { get { return _qcwebid_ExistsSubQuery_TNoticeListMap; }}
        public override String keepQcwebid_ExistsSubQuery_TNoticeList(TNoticeCQ subQuery) {
            if (_qcwebid_ExistsSubQuery_TNoticeListMap == null) { _qcwebid_ExistsSubQuery_TNoticeListMap = new LinkedHashMap<String, TNoticeCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_ExistsSubQuery_TNoticeListMap.size() + 1);
            _qcwebid_ExistsSubQuery_TNoticeListMap.put(key, subQuery); return "Qcwebid_ExistsSubQuery_TNoticeList." + key;
        }

        protected Map<String, TOutputRequestCQ> _qcwebid_ExistsSubQuery_TOutputRequestListMap;
        public Map<String, TOutputRequestCQ> Qcwebid_ExistsSubQuery_TOutputRequestList { get { return _qcwebid_ExistsSubQuery_TOutputRequestListMap; }}
        public override String keepQcwebid_ExistsSubQuery_TOutputRequestList(TOutputRequestCQ subQuery) {
            if (_qcwebid_ExistsSubQuery_TOutputRequestListMap == null) { _qcwebid_ExistsSubQuery_TOutputRequestListMap = new LinkedHashMap<String, TOutputRequestCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_ExistsSubQuery_TOutputRequestListMap.size() + 1);
            _qcwebid_ExistsSubQuery_TOutputRequestListMap.put(key, subQuery); return "Qcwebid_ExistsSubQuery_TOutputRequestList." + key;
        }

        protected Map<String, TOutputSettingCQ> _qcwebid_ExistsSubQuery_TOutputSettingAsOneMap;
        public Map<String, TOutputSettingCQ> Qcwebid_ExistsSubQuery_TOutputSettingAsOne { get { return _qcwebid_ExistsSubQuery_TOutputSettingAsOneMap; }}
        public override String keepQcwebid_ExistsSubQuery_TOutputSettingAsOne(TOutputSettingCQ subQuery) {
            if (_qcwebid_ExistsSubQuery_TOutputSettingAsOneMap == null) { _qcwebid_ExistsSubQuery_TOutputSettingAsOneMap = new LinkedHashMap<String, TOutputSettingCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_ExistsSubQuery_TOutputSettingAsOneMap.size() + 1);
            _qcwebid_ExistsSubQuery_TOutputSettingAsOneMap.put(key, subQuery); return "Qcwebid_ExistsSubQuery_TOutputSettingAsOne." + key;
        }

        protected Map<String, TOutputSettingCrossCQ> _qcwebid_ExistsSubQuery_TOutputSettingCrossAsOneMap;
        public Map<String, TOutputSettingCrossCQ> Qcwebid_ExistsSubQuery_TOutputSettingCrossAsOne { get { return _qcwebid_ExistsSubQuery_TOutputSettingCrossAsOneMap; }}
        public override String keepQcwebid_ExistsSubQuery_TOutputSettingCrossAsOne(TOutputSettingCrossCQ subQuery) {
            if (_qcwebid_ExistsSubQuery_TOutputSettingCrossAsOneMap == null) { _qcwebid_ExistsSubQuery_TOutputSettingCrossAsOneMap = new LinkedHashMap<String, TOutputSettingCrossCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_ExistsSubQuery_TOutputSettingCrossAsOneMap.size() + 1);
            _qcwebid_ExistsSubQuery_TOutputSettingCrossAsOneMap.put(key, subQuery); return "Qcwebid_ExistsSubQuery_TOutputSettingCrossAsOne." + key;
        }

        protected Map<String, TOutputSettingFaCQ> _qcwebid_ExistsSubQuery_TOutputSettingFaAsOneMap;
        public Map<String, TOutputSettingFaCQ> Qcwebid_ExistsSubQuery_TOutputSettingFaAsOne { get { return _qcwebid_ExistsSubQuery_TOutputSettingFaAsOneMap; }}
        public override String keepQcwebid_ExistsSubQuery_TOutputSettingFaAsOne(TOutputSettingFaCQ subQuery) {
            if (_qcwebid_ExistsSubQuery_TOutputSettingFaAsOneMap == null) { _qcwebid_ExistsSubQuery_TOutputSettingFaAsOneMap = new LinkedHashMap<String, TOutputSettingFaCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_ExistsSubQuery_TOutputSettingFaAsOneMap.size() + 1);
            _qcwebid_ExistsSubQuery_TOutputSettingFaAsOneMap.put(key, subQuery); return "Qcwebid_ExistsSubQuery_TOutputSettingFaAsOne." + key;
        }

        protected Map<String, TOutputSettingGtCQ> _qcwebid_ExistsSubQuery_TOutputSettingGtAsOneMap;
        public Map<String, TOutputSettingGtCQ> Qcwebid_ExistsSubQuery_TOutputSettingGtAsOne { get { return _qcwebid_ExistsSubQuery_TOutputSettingGtAsOneMap; }}
        public override String keepQcwebid_ExistsSubQuery_TOutputSettingGtAsOne(TOutputSettingGtCQ subQuery) {
            if (_qcwebid_ExistsSubQuery_TOutputSettingGtAsOneMap == null) { _qcwebid_ExistsSubQuery_TOutputSettingGtAsOneMap = new LinkedHashMap<String, TOutputSettingGtCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_ExistsSubQuery_TOutputSettingGtAsOneMap.size() + 1);
            _qcwebid_ExistsSubQuery_TOutputSettingGtAsOneMap.put(key, subQuery); return "Qcwebid_ExistsSubQuery_TOutputSettingGtAsOne." + key;
        }

        protected Map<String, TOutputSettingReportCQ> _qcwebid_ExistsSubQuery_TOutputSettingReportAsOneMap;
        public Map<String, TOutputSettingReportCQ> Qcwebid_ExistsSubQuery_TOutputSettingReportAsOne { get { return _qcwebid_ExistsSubQuery_TOutputSettingReportAsOneMap; }}
        public override String keepQcwebid_ExistsSubQuery_TOutputSettingReportAsOne(TOutputSettingReportCQ subQuery) {
            if (_qcwebid_ExistsSubQuery_TOutputSettingReportAsOneMap == null) { _qcwebid_ExistsSubQuery_TOutputSettingReportAsOneMap = new LinkedHashMap<String, TOutputSettingReportCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_ExistsSubQuery_TOutputSettingReportAsOneMap.size() + 1);
            _qcwebid_ExistsSubQuery_TOutputSettingReportAsOneMap.put(key, subQuery); return "Qcwebid_ExistsSubQuery_TOutputSettingReportAsOne." + key;
        }

        protected Map<String, TOutputTemplateCQ> _qcwebid_ExistsSubQuery_TOutputTemplateListMap;
        public Map<String, TOutputTemplateCQ> Qcwebid_ExistsSubQuery_TOutputTemplateList { get { return _qcwebid_ExistsSubQuery_TOutputTemplateListMap; }}
        public override String keepQcwebid_ExistsSubQuery_TOutputTemplateList(TOutputTemplateCQ subQuery) {
            if (_qcwebid_ExistsSubQuery_TOutputTemplateListMap == null) { _qcwebid_ExistsSubQuery_TOutputTemplateListMap = new LinkedHashMap<String, TOutputTemplateCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_ExistsSubQuery_TOutputTemplateListMap.size() + 1);
            _qcwebid_ExistsSubQuery_TOutputTemplateListMap.put(key, subQuery); return "Qcwebid_ExistsSubQuery_TOutputTemplateList." + key;
        }

        protected Map<String, TQcwebSurveyDetailCQ> _qcwebid_ExistsSubQuery_TQcwebSurveyDetailListMap;
        public Map<String, TQcwebSurveyDetailCQ> Qcwebid_ExistsSubQuery_TQcwebSurveyDetailList { get { return _qcwebid_ExistsSubQuery_TQcwebSurveyDetailListMap; }}
        public override String keepQcwebid_ExistsSubQuery_TQcwebSurveyDetailList(TQcwebSurveyDetailCQ subQuery) {
            if (_qcwebid_ExistsSubQuery_TQcwebSurveyDetailListMap == null) { _qcwebid_ExistsSubQuery_TQcwebSurveyDetailListMap = new LinkedHashMap<String, TQcwebSurveyDetailCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_ExistsSubQuery_TQcwebSurveyDetailListMap.size() + 1);
            _qcwebid_ExistsSubQuery_TQcwebSurveyDetailListMap.put(key, subQuery); return "Qcwebid_ExistsSubQuery_TQcwebSurveyDetailList." + key;
        }

        protected Map<String, TRawdataImportQueInfoCQ> _qcwebid_ExistsSubQuery_TRawdataImportQueInfoListMap;
        public Map<String, TRawdataImportQueInfoCQ> Qcwebid_ExistsSubQuery_TRawdataImportQueInfoList { get { return _qcwebid_ExistsSubQuery_TRawdataImportQueInfoListMap; }}
        public override String keepQcwebid_ExistsSubQuery_TRawdataImportQueInfoList(TRawdataImportQueInfoCQ subQuery) {
            if (_qcwebid_ExistsSubQuery_TRawdataImportQueInfoListMap == null) { _qcwebid_ExistsSubQuery_TRawdataImportQueInfoListMap = new LinkedHashMap<String, TRawdataImportQueInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_ExistsSubQuery_TRawdataImportQueInfoListMap.size() + 1);
            _qcwebid_ExistsSubQuery_TRawdataImportQueInfoListMap.put(key, subQuery); return "Qcwebid_ExistsSubQuery_TRawdataImportQueInfoList." + key;
        }

        protected Map<String, TReportsetCQ> _qcwebid_ExistsSubQuery_TReportsetListMap;
        public Map<String, TReportsetCQ> Qcwebid_ExistsSubQuery_TReportsetList { get { return _qcwebid_ExistsSubQuery_TReportsetListMap; }}
        public override String keepQcwebid_ExistsSubQuery_TReportsetList(TReportsetCQ subQuery) {
            if (_qcwebid_ExistsSubQuery_TReportsetListMap == null) { _qcwebid_ExistsSubQuery_TReportsetListMap = new LinkedHashMap<String, TReportsetCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_ExistsSubQuery_TReportsetListMap.size() + 1);
            _qcwebid_ExistsSubQuery_TReportsetListMap.put(key, subQuery); return "Qcwebid_ExistsSubQuery_TReportsetList." + key;
        }

        protected Map<String, TScenarioTotalizationCQ> _qcwebid_ExistsSubQuery_TScenarioTotalizationListMap;
        public Map<String, TScenarioTotalizationCQ> Qcwebid_ExistsSubQuery_TScenarioTotalizationList { get { return _qcwebid_ExistsSubQuery_TScenarioTotalizationListMap; }}
        public override String keepQcwebid_ExistsSubQuery_TScenarioTotalizationList(TScenarioTotalizationCQ subQuery) {
            if (_qcwebid_ExistsSubQuery_TScenarioTotalizationListMap == null) { _qcwebid_ExistsSubQuery_TScenarioTotalizationListMap = new LinkedHashMap<String, TScenarioTotalizationCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_ExistsSubQuery_TScenarioTotalizationListMap.size() + 1);
            _qcwebid_ExistsSubQuery_TScenarioTotalizationListMap.put(key, subQuery); return "Qcwebid_ExistsSubQuery_TScenarioTotalizationList." + key;
        }

        protected Map<String, TSelectConditionInfoCQ> _qcwebid_ExistsSubQuery_TSelectConditionInfoListMap;
        public Map<String, TSelectConditionInfoCQ> Qcwebid_ExistsSubQuery_TSelectConditionInfoList { get { return _qcwebid_ExistsSubQuery_TSelectConditionInfoListMap; }}
        public override String keepQcwebid_ExistsSubQuery_TSelectConditionInfoList(TSelectConditionInfoCQ subQuery) {
            if (_qcwebid_ExistsSubQuery_TSelectConditionInfoListMap == null) { _qcwebid_ExistsSubQuery_TSelectConditionInfoListMap = new LinkedHashMap<String, TSelectConditionInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_ExistsSubQuery_TSelectConditionInfoListMap.size() + 1);
            _qcwebid_ExistsSubQuery_TSelectConditionInfoListMap.put(key, subQuery); return "Qcwebid_ExistsSubQuery_TSelectConditionInfoList." + key;
        }

        protected Map<String, TSessionControlerCQ> _qcwebid_ExistsSubQuery_TSessionControlerListMap;
        public Map<String, TSessionControlerCQ> Qcwebid_ExistsSubQuery_TSessionControlerList { get { return _qcwebid_ExistsSubQuery_TSessionControlerListMap; }}
        public override String keepQcwebid_ExistsSubQuery_TSessionControlerList(TSessionControlerCQ subQuery) {
            if (_qcwebid_ExistsSubQuery_TSessionControlerListMap == null) { _qcwebid_ExistsSubQuery_TSessionControlerListMap = new LinkedHashMap<String, TSessionControlerCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_ExistsSubQuery_TSessionControlerListMap.size() + 1);
            _qcwebid_ExistsSubQuery_TSessionControlerListMap.put(key, subQuery); return "Qcwebid_ExistsSubQuery_TSessionControlerList." + key;
        }

        protected Map<String, TWeightbackCQ> _qcwebid_ExistsSubQuery_TWeightbackListMap;
        public Map<String, TWeightbackCQ> Qcwebid_ExistsSubQuery_TWeightbackList { get { return _qcwebid_ExistsSubQuery_TWeightbackListMap; }}
        public override String keepQcwebid_ExistsSubQuery_TWeightbackList(TWeightbackCQ subQuery) {
            if (_qcwebid_ExistsSubQuery_TWeightbackListMap == null) { _qcwebid_ExistsSubQuery_TWeightbackListMap = new LinkedHashMap<String, TWeightbackCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_ExistsSubQuery_TWeightbackListMap.size() + 1);
            _qcwebid_ExistsSubQuery_TWeightbackListMap.put(key, subQuery); return "Qcwebid_ExistsSubQuery_TWeightbackList." + key;
        }

        protected Map<String, TAccessPermissionsInfoCQ> _qcwebid_NotExistsSubQuery_TAccessPermissionsInfoAsOneMap;
        public Map<String, TAccessPermissionsInfoCQ> Qcwebid_NotExistsSubQuery_TAccessPermissionsInfoAsOne { get { return _qcwebid_NotExistsSubQuery_TAccessPermissionsInfoAsOneMap; }}
        public override String keepQcwebid_NotExistsSubQuery_TAccessPermissionsInfoAsOne(TAccessPermissionsInfoCQ subQuery) {
            if (_qcwebid_NotExistsSubQuery_TAccessPermissionsInfoAsOneMap == null) { _qcwebid_NotExistsSubQuery_TAccessPermissionsInfoAsOneMap = new LinkedHashMap<String, TAccessPermissionsInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotExistsSubQuery_TAccessPermissionsInfoAsOneMap.size() + 1);
            _qcwebid_NotExistsSubQuery_TAccessPermissionsInfoAsOneMap.put(key, subQuery); return "Qcwebid_NotExistsSubQuery_TAccessPermissionsInfoAsOne." + key;
        }

        protected Map<String, TAllocationCellInfoCQ> _qcwebid_NotExistsSubQuery_TAllocationCellInfoListMap;
        public Map<String, TAllocationCellInfoCQ> Qcwebid_NotExistsSubQuery_TAllocationCellInfoList { get { return _qcwebid_NotExistsSubQuery_TAllocationCellInfoListMap; }}
        public override String keepQcwebid_NotExistsSubQuery_TAllocationCellInfoList(TAllocationCellInfoCQ subQuery) {
            if (_qcwebid_NotExistsSubQuery_TAllocationCellInfoListMap == null) { _qcwebid_NotExistsSubQuery_TAllocationCellInfoListMap = new LinkedHashMap<String, TAllocationCellInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotExistsSubQuery_TAllocationCellInfoListMap.size() + 1);
            _qcwebid_NotExistsSubQuery_TAllocationCellInfoListMap.put(key, subQuery); return "Qcwebid_NotExistsSubQuery_TAllocationCellInfoList." + key;
        }

        protected Map<String, TDataEditListCQ> _qcwebid_NotExistsSubQuery_TDataEditListListMap;
        public Map<String, TDataEditListCQ> Qcwebid_NotExistsSubQuery_TDataEditListList { get { return _qcwebid_NotExistsSubQuery_TDataEditListListMap; }}
        public override String keepQcwebid_NotExistsSubQuery_TDataEditListList(TDataEditListCQ subQuery) {
            if (_qcwebid_NotExistsSubQuery_TDataEditListListMap == null) { _qcwebid_NotExistsSubQuery_TDataEditListListMap = new LinkedHashMap<String, TDataEditListCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotExistsSubQuery_TDataEditListListMap.size() + 1);
            _qcwebid_NotExistsSubQuery_TDataEditListListMap.put(key, subQuery); return "Qcwebid_NotExistsSubQuery_TDataEditListList." + key;
        }

        protected Map<String, TItemInfoCQ> _qcwebid_NotExistsSubQuery_TItemInfoListMap;
        public Map<String, TItemInfoCQ> Qcwebid_NotExistsSubQuery_TItemInfoList { get { return _qcwebid_NotExistsSubQuery_TItemInfoListMap; }}
        public override String keepQcwebid_NotExistsSubQuery_TItemInfoList(TItemInfoCQ subQuery) {
            if (_qcwebid_NotExistsSubQuery_TItemInfoListMap == null) { _qcwebid_NotExistsSubQuery_TItemInfoListMap = new LinkedHashMap<String, TItemInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotExistsSubQuery_TItemInfoListMap.size() + 1);
            _qcwebid_NotExistsSubQuery_TItemInfoListMap.put(key, subQuery); return "Qcwebid_NotExistsSubQuery_TItemInfoList." + key;
        }

        protected Map<String, TNoticeCQ> _qcwebid_NotExistsSubQuery_TNoticeListMap;
        public Map<String, TNoticeCQ> Qcwebid_NotExistsSubQuery_TNoticeList { get { return _qcwebid_NotExistsSubQuery_TNoticeListMap; }}
        public override String keepQcwebid_NotExistsSubQuery_TNoticeList(TNoticeCQ subQuery) {
            if (_qcwebid_NotExistsSubQuery_TNoticeListMap == null) { _qcwebid_NotExistsSubQuery_TNoticeListMap = new LinkedHashMap<String, TNoticeCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotExistsSubQuery_TNoticeListMap.size() + 1);
            _qcwebid_NotExistsSubQuery_TNoticeListMap.put(key, subQuery); return "Qcwebid_NotExistsSubQuery_TNoticeList." + key;
        }

        protected Map<String, TOutputRequestCQ> _qcwebid_NotExistsSubQuery_TOutputRequestListMap;
        public Map<String, TOutputRequestCQ> Qcwebid_NotExistsSubQuery_TOutputRequestList { get { return _qcwebid_NotExistsSubQuery_TOutputRequestListMap; }}
        public override String keepQcwebid_NotExistsSubQuery_TOutputRequestList(TOutputRequestCQ subQuery) {
            if (_qcwebid_NotExistsSubQuery_TOutputRequestListMap == null) { _qcwebid_NotExistsSubQuery_TOutputRequestListMap = new LinkedHashMap<String, TOutputRequestCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotExistsSubQuery_TOutputRequestListMap.size() + 1);
            _qcwebid_NotExistsSubQuery_TOutputRequestListMap.put(key, subQuery); return "Qcwebid_NotExistsSubQuery_TOutputRequestList." + key;
        }

        protected Map<String, TOutputSettingCQ> _qcwebid_NotExistsSubQuery_TOutputSettingAsOneMap;
        public Map<String, TOutputSettingCQ> Qcwebid_NotExistsSubQuery_TOutputSettingAsOne { get { return _qcwebid_NotExistsSubQuery_TOutputSettingAsOneMap; }}
        public override String keepQcwebid_NotExistsSubQuery_TOutputSettingAsOne(TOutputSettingCQ subQuery) {
            if (_qcwebid_NotExistsSubQuery_TOutputSettingAsOneMap == null) { _qcwebid_NotExistsSubQuery_TOutputSettingAsOneMap = new LinkedHashMap<String, TOutputSettingCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotExistsSubQuery_TOutputSettingAsOneMap.size() + 1);
            _qcwebid_NotExistsSubQuery_TOutputSettingAsOneMap.put(key, subQuery); return "Qcwebid_NotExistsSubQuery_TOutputSettingAsOne." + key;
        }

        protected Map<String, TOutputSettingCrossCQ> _qcwebid_NotExistsSubQuery_TOutputSettingCrossAsOneMap;
        public Map<String, TOutputSettingCrossCQ> Qcwebid_NotExistsSubQuery_TOutputSettingCrossAsOne { get { return _qcwebid_NotExistsSubQuery_TOutputSettingCrossAsOneMap; }}
        public override String keepQcwebid_NotExistsSubQuery_TOutputSettingCrossAsOne(TOutputSettingCrossCQ subQuery) {
            if (_qcwebid_NotExistsSubQuery_TOutputSettingCrossAsOneMap == null) { _qcwebid_NotExistsSubQuery_TOutputSettingCrossAsOneMap = new LinkedHashMap<String, TOutputSettingCrossCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotExistsSubQuery_TOutputSettingCrossAsOneMap.size() + 1);
            _qcwebid_NotExistsSubQuery_TOutputSettingCrossAsOneMap.put(key, subQuery); return "Qcwebid_NotExistsSubQuery_TOutputSettingCrossAsOne." + key;
        }

        protected Map<String, TOutputSettingFaCQ> _qcwebid_NotExistsSubQuery_TOutputSettingFaAsOneMap;
        public Map<String, TOutputSettingFaCQ> Qcwebid_NotExistsSubQuery_TOutputSettingFaAsOne { get { return _qcwebid_NotExistsSubQuery_TOutputSettingFaAsOneMap; }}
        public override String keepQcwebid_NotExistsSubQuery_TOutputSettingFaAsOne(TOutputSettingFaCQ subQuery) {
            if (_qcwebid_NotExistsSubQuery_TOutputSettingFaAsOneMap == null) { _qcwebid_NotExistsSubQuery_TOutputSettingFaAsOneMap = new LinkedHashMap<String, TOutputSettingFaCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotExistsSubQuery_TOutputSettingFaAsOneMap.size() + 1);
            _qcwebid_NotExistsSubQuery_TOutputSettingFaAsOneMap.put(key, subQuery); return "Qcwebid_NotExistsSubQuery_TOutputSettingFaAsOne." + key;
        }

        protected Map<String, TOutputSettingGtCQ> _qcwebid_NotExistsSubQuery_TOutputSettingGtAsOneMap;
        public Map<String, TOutputSettingGtCQ> Qcwebid_NotExistsSubQuery_TOutputSettingGtAsOne { get { return _qcwebid_NotExistsSubQuery_TOutputSettingGtAsOneMap; }}
        public override String keepQcwebid_NotExistsSubQuery_TOutputSettingGtAsOne(TOutputSettingGtCQ subQuery) {
            if (_qcwebid_NotExistsSubQuery_TOutputSettingGtAsOneMap == null) { _qcwebid_NotExistsSubQuery_TOutputSettingGtAsOneMap = new LinkedHashMap<String, TOutputSettingGtCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotExistsSubQuery_TOutputSettingGtAsOneMap.size() + 1);
            _qcwebid_NotExistsSubQuery_TOutputSettingGtAsOneMap.put(key, subQuery); return "Qcwebid_NotExistsSubQuery_TOutputSettingGtAsOne." + key;
        }

        protected Map<String, TOutputSettingReportCQ> _qcwebid_NotExistsSubQuery_TOutputSettingReportAsOneMap;
        public Map<String, TOutputSettingReportCQ> Qcwebid_NotExistsSubQuery_TOutputSettingReportAsOne { get { return _qcwebid_NotExistsSubQuery_TOutputSettingReportAsOneMap; }}
        public override String keepQcwebid_NotExistsSubQuery_TOutputSettingReportAsOne(TOutputSettingReportCQ subQuery) {
            if (_qcwebid_NotExistsSubQuery_TOutputSettingReportAsOneMap == null) { _qcwebid_NotExistsSubQuery_TOutputSettingReportAsOneMap = new LinkedHashMap<String, TOutputSettingReportCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotExistsSubQuery_TOutputSettingReportAsOneMap.size() + 1);
            _qcwebid_NotExistsSubQuery_TOutputSettingReportAsOneMap.put(key, subQuery); return "Qcwebid_NotExistsSubQuery_TOutputSettingReportAsOne." + key;
        }

        protected Map<String, TOutputTemplateCQ> _qcwebid_NotExistsSubQuery_TOutputTemplateListMap;
        public Map<String, TOutputTemplateCQ> Qcwebid_NotExistsSubQuery_TOutputTemplateList { get { return _qcwebid_NotExistsSubQuery_TOutputTemplateListMap; }}
        public override String keepQcwebid_NotExistsSubQuery_TOutputTemplateList(TOutputTemplateCQ subQuery) {
            if (_qcwebid_NotExistsSubQuery_TOutputTemplateListMap == null) { _qcwebid_NotExistsSubQuery_TOutputTemplateListMap = new LinkedHashMap<String, TOutputTemplateCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotExistsSubQuery_TOutputTemplateListMap.size() + 1);
            _qcwebid_NotExistsSubQuery_TOutputTemplateListMap.put(key, subQuery); return "Qcwebid_NotExistsSubQuery_TOutputTemplateList." + key;
        }

        protected Map<String, TQcwebSurveyDetailCQ> _qcwebid_NotExistsSubQuery_TQcwebSurveyDetailListMap;
        public Map<String, TQcwebSurveyDetailCQ> Qcwebid_NotExistsSubQuery_TQcwebSurveyDetailList { get { return _qcwebid_NotExistsSubQuery_TQcwebSurveyDetailListMap; }}
        public override String keepQcwebid_NotExistsSubQuery_TQcwebSurveyDetailList(TQcwebSurveyDetailCQ subQuery) {
            if (_qcwebid_NotExistsSubQuery_TQcwebSurveyDetailListMap == null) { _qcwebid_NotExistsSubQuery_TQcwebSurveyDetailListMap = new LinkedHashMap<String, TQcwebSurveyDetailCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotExistsSubQuery_TQcwebSurveyDetailListMap.size() + 1);
            _qcwebid_NotExistsSubQuery_TQcwebSurveyDetailListMap.put(key, subQuery); return "Qcwebid_NotExistsSubQuery_TQcwebSurveyDetailList." + key;
        }

        protected Map<String, TRawdataImportQueInfoCQ> _qcwebid_NotExistsSubQuery_TRawdataImportQueInfoListMap;
        public Map<String, TRawdataImportQueInfoCQ> Qcwebid_NotExistsSubQuery_TRawdataImportQueInfoList { get { return _qcwebid_NotExistsSubQuery_TRawdataImportQueInfoListMap; }}
        public override String keepQcwebid_NotExistsSubQuery_TRawdataImportQueInfoList(TRawdataImportQueInfoCQ subQuery) {
            if (_qcwebid_NotExistsSubQuery_TRawdataImportQueInfoListMap == null) { _qcwebid_NotExistsSubQuery_TRawdataImportQueInfoListMap = new LinkedHashMap<String, TRawdataImportQueInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotExistsSubQuery_TRawdataImportQueInfoListMap.size() + 1);
            _qcwebid_NotExistsSubQuery_TRawdataImportQueInfoListMap.put(key, subQuery); return "Qcwebid_NotExistsSubQuery_TRawdataImportQueInfoList." + key;
        }

        protected Map<String, TReportsetCQ> _qcwebid_NotExistsSubQuery_TReportsetListMap;
        public Map<String, TReportsetCQ> Qcwebid_NotExistsSubQuery_TReportsetList { get { return _qcwebid_NotExistsSubQuery_TReportsetListMap; }}
        public override String keepQcwebid_NotExistsSubQuery_TReportsetList(TReportsetCQ subQuery) {
            if (_qcwebid_NotExistsSubQuery_TReportsetListMap == null) { _qcwebid_NotExistsSubQuery_TReportsetListMap = new LinkedHashMap<String, TReportsetCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotExistsSubQuery_TReportsetListMap.size() + 1);
            _qcwebid_NotExistsSubQuery_TReportsetListMap.put(key, subQuery); return "Qcwebid_NotExistsSubQuery_TReportsetList." + key;
        }

        protected Map<String, TScenarioTotalizationCQ> _qcwebid_NotExistsSubQuery_TScenarioTotalizationListMap;
        public Map<String, TScenarioTotalizationCQ> Qcwebid_NotExistsSubQuery_TScenarioTotalizationList { get { return _qcwebid_NotExistsSubQuery_TScenarioTotalizationListMap; }}
        public override String keepQcwebid_NotExistsSubQuery_TScenarioTotalizationList(TScenarioTotalizationCQ subQuery) {
            if (_qcwebid_NotExistsSubQuery_TScenarioTotalizationListMap == null) { _qcwebid_NotExistsSubQuery_TScenarioTotalizationListMap = new LinkedHashMap<String, TScenarioTotalizationCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotExistsSubQuery_TScenarioTotalizationListMap.size() + 1);
            _qcwebid_NotExistsSubQuery_TScenarioTotalizationListMap.put(key, subQuery); return "Qcwebid_NotExistsSubQuery_TScenarioTotalizationList." + key;
        }

        protected Map<String, TSelectConditionInfoCQ> _qcwebid_NotExistsSubQuery_TSelectConditionInfoListMap;
        public Map<String, TSelectConditionInfoCQ> Qcwebid_NotExistsSubQuery_TSelectConditionInfoList { get { return _qcwebid_NotExistsSubQuery_TSelectConditionInfoListMap; }}
        public override String keepQcwebid_NotExistsSubQuery_TSelectConditionInfoList(TSelectConditionInfoCQ subQuery) {
            if (_qcwebid_NotExistsSubQuery_TSelectConditionInfoListMap == null) { _qcwebid_NotExistsSubQuery_TSelectConditionInfoListMap = new LinkedHashMap<String, TSelectConditionInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotExistsSubQuery_TSelectConditionInfoListMap.size() + 1);
            _qcwebid_NotExistsSubQuery_TSelectConditionInfoListMap.put(key, subQuery); return "Qcwebid_NotExistsSubQuery_TSelectConditionInfoList." + key;
        }

        protected Map<String, TSessionControlerCQ> _qcwebid_NotExistsSubQuery_TSessionControlerListMap;
        public Map<String, TSessionControlerCQ> Qcwebid_NotExistsSubQuery_TSessionControlerList { get { return _qcwebid_NotExistsSubQuery_TSessionControlerListMap; }}
        public override String keepQcwebid_NotExistsSubQuery_TSessionControlerList(TSessionControlerCQ subQuery) {
            if (_qcwebid_NotExistsSubQuery_TSessionControlerListMap == null) { _qcwebid_NotExistsSubQuery_TSessionControlerListMap = new LinkedHashMap<String, TSessionControlerCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotExistsSubQuery_TSessionControlerListMap.size() + 1);
            _qcwebid_NotExistsSubQuery_TSessionControlerListMap.put(key, subQuery); return "Qcwebid_NotExistsSubQuery_TSessionControlerList." + key;
        }

        protected Map<String, TWeightbackCQ> _qcwebid_NotExistsSubQuery_TWeightbackListMap;
        public Map<String, TWeightbackCQ> Qcwebid_NotExistsSubQuery_TWeightbackList { get { return _qcwebid_NotExistsSubQuery_TWeightbackListMap; }}
        public override String keepQcwebid_NotExistsSubQuery_TWeightbackList(TWeightbackCQ subQuery) {
            if (_qcwebid_NotExistsSubQuery_TWeightbackListMap == null) { _qcwebid_NotExistsSubQuery_TWeightbackListMap = new LinkedHashMap<String, TWeightbackCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotExistsSubQuery_TWeightbackListMap.size() + 1);
            _qcwebid_NotExistsSubQuery_TWeightbackListMap.put(key, subQuery); return "Qcwebid_NotExistsSubQuery_TWeightbackList." + key;
        }

        protected Map<String, TAllocationCellInfoCQ> _qcwebid_InScopeSubQuery_TAllocationCellInfoMap;
        public Map<String, TAllocationCellInfoCQ> Qcwebid_InScopeSubQuery_TAllocationCellInfo { get { return _qcwebid_InScopeSubQuery_TAllocationCellInfoMap; }}
        public override String keepQcwebid_InScopeSubQuery_TAllocationCellInfo(TAllocationCellInfoCQ subQuery) {
            if (_qcwebid_InScopeSubQuery_TAllocationCellInfoMap == null) { _qcwebid_InScopeSubQuery_TAllocationCellInfoMap = new LinkedHashMap<String, TAllocationCellInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_InScopeSubQuery_TAllocationCellInfoMap.size() + 1);
            _qcwebid_InScopeSubQuery_TAllocationCellInfoMap.put(key, subQuery); return "Qcwebid_InScopeSubQuery_TAllocationCellInfo." + key;
        }

        protected Map<String, TAccessPermissionsInfoCQ> _qcwebid_InScopeSubQuery_TAccessPermissionsInfoAsOneMap;
        public Map<String, TAccessPermissionsInfoCQ> Qcwebid_InScopeSubQuery_TAccessPermissionsInfoAsOne { get { return _qcwebid_InScopeSubQuery_TAccessPermissionsInfoAsOneMap; }}
        public override String keepQcwebid_InScopeSubQuery_TAccessPermissionsInfoAsOne(TAccessPermissionsInfoCQ subQuery) {
            if (_qcwebid_InScopeSubQuery_TAccessPermissionsInfoAsOneMap == null) { _qcwebid_InScopeSubQuery_TAccessPermissionsInfoAsOneMap = new LinkedHashMap<String, TAccessPermissionsInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_InScopeSubQuery_TAccessPermissionsInfoAsOneMap.size() + 1);
            _qcwebid_InScopeSubQuery_TAccessPermissionsInfoAsOneMap.put(key, subQuery); return "Qcwebid_InScopeSubQuery_TAccessPermissionsInfoAsOne." + key;
        }

        protected Map<String, TAllocationCellInfoCQ> _qcwebid_InScopeSubQuery_TAllocationCellInfoListMap;
        public Map<String, TAllocationCellInfoCQ> Qcwebid_InScopeSubQuery_TAllocationCellInfoList { get { return _qcwebid_InScopeSubQuery_TAllocationCellInfoListMap; }}
        public override String keepQcwebid_InScopeSubQuery_TAllocationCellInfoList(TAllocationCellInfoCQ subQuery) {
            if (_qcwebid_InScopeSubQuery_TAllocationCellInfoListMap == null) { _qcwebid_InScopeSubQuery_TAllocationCellInfoListMap = new LinkedHashMap<String, TAllocationCellInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_InScopeSubQuery_TAllocationCellInfoListMap.size() + 1);
            _qcwebid_InScopeSubQuery_TAllocationCellInfoListMap.put(key, subQuery); return "Qcwebid_InScopeSubQuery_TAllocationCellInfoList." + key;
        }

        protected Map<String, TDataEditListCQ> _qcwebid_InScopeSubQuery_TDataEditListListMap;
        public Map<String, TDataEditListCQ> Qcwebid_InScopeSubQuery_TDataEditListList { get { return _qcwebid_InScopeSubQuery_TDataEditListListMap; }}
        public override String keepQcwebid_InScopeSubQuery_TDataEditListList(TDataEditListCQ subQuery) {
            if (_qcwebid_InScopeSubQuery_TDataEditListListMap == null) { _qcwebid_InScopeSubQuery_TDataEditListListMap = new LinkedHashMap<String, TDataEditListCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_InScopeSubQuery_TDataEditListListMap.size() + 1);
            _qcwebid_InScopeSubQuery_TDataEditListListMap.put(key, subQuery); return "Qcwebid_InScopeSubQuery_TDataEditListList." + key;
        }

        protected Map<String, TItemInfoCQ> _qcwebid_InScopeSubQuery_TItemInfoListMap;
        public Map<String, TItemInfoCQ> Qcwebid_InScopeSubQuery_TItemInfoList { get { return _qcwebid_InScopeSubQuery_TItemInfoListMap; }}
        public override String keepQcwebid_InScopeSubQuery_TItemInfoList(TItemInfoCQ subQuery) {
            if (_qcwebid_InScopeSubQuery_TItemInfoListMap == null) { _qcwebid_InScopeSubQuery_TItemInfoListMap = new LinkedHashMap<String, TItemInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_InScopeSubQuery_TItemInfoListMap.size() + 1);
            _qcwebid_InScopeSubQuery_TItemInfoListMap.put(key, subQuery); return "Qcwebid_InScopeSubQuery_TItemInfoList." + key;
        }

        protected Map<String, TNoticeCQ> _qcwebid_InScopeSubQuery_TNoticeListMap;
        public Map<String, TNoticeCQ> Qcwebid_InScopeSubQuery_TNoticeList { get { return _qcwebid_InScopeSubQuery_TNoticeListMap; }}
        public override String keepQcwebid_InScopeSubQuery_TNoticeList(TNoticeCQ subQuery) {
            if (_qcwebid_InScopeSubQuery_TNoticeListMap == null) { _qcwebid_InScopeSubQuery_TNoticeListMap = new LinkedHashMap<String, TNoticeCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_InScopeSubQuery_TNoticeListMap.size() + 1);
            _qcwebid_InScopeSubQuery_TNoticeListMap.put(key, subQuery); return "Qcwebid_InScopeSubQuery_TNoticeList." + key;
        }

        protected Map<String, TOutputRequestCQ> _qcwebid_InScopeSubQuery_TOutputRequestListMap;
        public Map<String, TOutputRequestCQ> Qcwebid_InScopeSubQuery_TOutputRequestList { get { return _qcwebid_InScopeSubQuery_TOutputRequestListMap; }}
        public override String keepQcwebid_InScopeSubQuery_TOutputRequestList(TOutputRequestCQ subQuery) {
            if (_qcwebid_InScopeSubQuery_TOutputRequestListMap == null) { _qcwebid_InScopeSubQuery_TOutputRequestListMap = new LinkedHashMap<String, TOutputRequestCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_InScopeSubQuery_TOutputRequestListMap.size() + 1);
            _qcwebid_InScopeSubQuery_TOutputRequestListMap.put(key, subQuery); return "Qcwebid_InScopeSubQuery_TOutputRequestList." + key;
        }

        protected Map<String, TOutputSettingCQ> _qcwebid_InScopeSubQuery_TOutputSettingAsOneMap;
        public Map<String, TOutputSettingCQ> Qcwebid_InScopeSubQuery_TOutputSettingAsOne { get { return _qcwebid_InScopeSubQuery_TOutputSettingAsOneMap; }}
        public override String keepQcwebid_InScopeSubQuery_TOutputSettingAsOne(TOutputSettingCQ subQuery) {
            if (_qcwebid_InScopeSubQuery_TOutputSettingAsOneMap == null) { _qcwebid_InScopeSubQuery_TOutputSettingAsOneMap = new LinkedHashMap<String, TOutputSettingCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_InScopeSubQuery_TOutputSettingAsOneMap.size() + 1);
            _qcwebid_InScopeSubQuery_TOutputSettingAsOneMap.put(key, subQuery); return "Qcwebid_InScopeSubQuery_TOutputSettingAsOne." + key;
        }

        protected Map<String, TOutputSettingCrossCQ> _qcwebid_InScopeSubQuery_TOutputSettingCrossAsOneMap;
        public Map<String, TOutputSettingCrossCQ> Qcwebid_InScopeSubQuery_TOutputSettingCrossAsOne { get { return _qcwebid_InScopeSubQuery_TOutputSettingCrossAsOneMap; }}
        public override String keepQcwebid_InScopeSubQuery_TOutputSettingCrossAsOne(TOutputSettingCrossCQ subQuery) {
            if (_qcwebid_InScopeSubQuery_TOutputSettingCrossAsOneMap == null) { _qcwebid_InScopeSubQuery_TOutputSettingCrossAsOneMap = new LinkedHashMap<String, TOutputSettingCrossCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_InScopeSubQuery_TOutputSettingCrossAsOneMap.size() + 1);
            _qcwebid_InScopeSubQuery_TOutputSettingCrossAsOneMap.put(key, subQuery); return "Qcwebid_InScopeSubQuery_TOutputSettingCrossAsOne." + key;
        }

        protected Map<String, TOutputSettingFaCQ> _qcwebid_InScopeSubQuery_TOutputSettingFaAsOneMap;
        public Map<String, TOutputSettingFaCQ> Qcwebid_InScopeSubQuery_TOutputSettingFaAsOne { get { return _qcwebid_InScopeSubQuery_TOutputSettingFaAsOneMap; }}
        public override String keepQcwebid_InScopeSubQuery_TOutputSettingFaAsOne(TOutputSettingFaCQ subQuery) {
            if (_qcwebid_InScopeSubQuery_TOutputSettingFaAsOneMap == null) { _qcwebid_InScopeSubQuery_TOutputSettingFaAsOneMap = new LinkedHashMap<String, TOutputSettingFaCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_InScopeSubQuery_TOutputSettingFaAsOneMap.size() + 1);
            _qcwebid_InScopeSubQuery_TOutputSettingFaAsOneMap.put(key, subQuery); return "Qcwebid_InScopeSubQuery_TOutputSettingFaAsOne." + key;
        }

        protected Map<String, TOutputSettingGtCQ> _qcwebid_InScopeSubQuery_TOutputSettingGtAsOneMap;
        public Map<String, TOutputSettingGtCQ> Qcwebid_InScopeSubQuery_TOutputSettingGtAsOne { get { return _qcwebid_InScopeSubQuery_TOutputSettingGtAsOneMap; }}
        public override String keepQcwebid_InScopeSubQuery_TOutputSettingGtAsOne(TOutputSettingGtCQ subQuery) {
            if (_qcwebid_InScopeSubQuery_TOutputSettingGtAsOneMap == null) { _qcwebid_InScopeSubQuery_TOutputSettingGtAsOneMap = new LinkedHashMap<String, TOutputSettingGtCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_InScopeSubQuery_TOutputSettingGtAsOneMap.size() + 1);
            _qcwebid_InScopeSubQuery_TOutputSettingGtAsOneMap.put(key, subQuery); return "Qcwebid_InScopeSubQuery_TOutputSettingGtAsOne." + key;
        }

        protected Map<String, TOutputSettingReportCQ> _qcwebid_InScopeSubQuery_TOutputSettingReportAsOneMap;
        public Map<String, TOutputSettingReportCQ> Qcwebid_InScopeSubQuery_TOutputSettingReportAsOne { get { return _qcwebid_InScopeSubQuery_TOutputSettingReportAsOneMap; }}
        public override String keepQcwebid_InScopeSubQuery_TOutputSettingReportAsOne(TOutputSettingReportCQ subQuery) {
            if (_qcwebid_InScopeSubQuery_TOutputSettingReportAsOneMap == null) { _qcwebid_InScopeSubQuery_TOutputSettingReportAsOneMap = new LinkedHashMap<String, TOutputSettingReportCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_InScopeSubQuery_TOutputSettingReportAsOneMap.size() + 1);
            _qcwebid_InScopeSubQuery_TOutputSettingReportAsOneMap.put(key, subQuery); return "Qcwebid_InScopeSubQuery_TOutputSettingReportAsOne." + key;
        }

        protected Map<String, TOutputTemplateCQ> _qcwebid_InScopeSubQuery_TOutputTemplateListMap;
        public Map<String, TOutputTemplateCQ> Qcwebid_InScopeSubQuery_TOutputTemplateList { get { return _qcwebid_InScopeSubQuery_TOutputTemplateListMap; }}
        public override String keepQcwebid_InScopeSubQuery_TOutputTemplateList(TOutputTemplateCQ subQuery) {
            if (_qcwebid_InScopeSubQuery_TOutputTemplateListMap == null) { _qcwebid_InScopeSubQuery_TOutputTemplateListMap = new LinkedHashMap<String, TOutputTemplateCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_InScopeSubQuery_TOutputTemplateListMap.size() + 1);
            _qcwebid_InScopeSubQuery_TOutputTemplateListMap.put(key, subQuery); return "Qcwebid_InScopeSubQuery_TOutputTemplateList." + key;
        }

        protected Map<String, TQcwebSurveyDetailCQ> _qcwebid_InScopeSubQuery_TQcwebSurveyDetailListMap;
        public Map<String, TQcwebSurveyDetailCQ> Qcwebid_InScopeSubQuery_TQcwebSurveyDetailList { get { return _qcwebid_InScopeSubQuery_TQcwebSurveyDetailListMap; }}
        public override String keepQcwebid_InScopeSubQuery_TQcwebSurveyDetailList(TQcwebSurveyDetailCQ subQuery) {
            if (_qcwebid_InScopeSubQuery_TQcwebSurveyDetailListMap == null) { _qcwebid_InScopeSubQuery_TQcwebSurveyDetailListMap = new LinkedHashMap<String, TQcwebSurveyDetailCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_InScopeSubQuery_TQcwebSurveyDetailListMap.size() + 1);
            _qcwebid_InScopeSubQuery_TQcwebSurveyDetailListMap.put(key, subQuery); return "Qcwebid_InScopeSubQuery_TQcwebSurveyDetailList." + key;
        }

        protected Map<String, TRawdataImportQueInfoCQ> _qcwebid_InScopeSubQuery_TRawdataImportQueInfoListMap;
        public Map<String, TRawdataImportQueInfoCQ> Qcwebid_InScopeSubQuery_TRawdataImportQueInfoList { get { return _qcwebid_InScopeSubQuery_TRawdataImportQueInfoListMap; }}
        public override String keepQcwebid_InScopeSubQuery_TRawdataImportQueInfoList(TRawdataImportQueInfoCQ subQuery) {
            if (_qcwebid_InScopeSubQuery_TRawdataImportQueInfoListMap == null) { _qcwebid_InScopeSubQuery_TRawdataImportQueInfoListMap = new LinkedHashMap<String, TRawdataImportQueInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_InScopeSubQuery_TRawdataImportQueInfoListMap.size() + 1);
            _qcwebid_InScopeSubQuery_TRawdataImportQueInfoListMap.put(key, subQuery); return "Qcwebid_InScopeSubQuery_TRawdataImportQueInfoList." + key;
        }

        protected Map<String, TReportsetCQ> _qcwebid_InScopeSubQuery_TReportsetListMap;
        public Map<String, TReportsetCQ> Qcwebid_InScopeSubQuery_TReportsetList { get { return _qcwebid_InScopeSubQuery_TReportsetListMap; }}
        public override String keepQcwebid_InScopeSubQuery_TReportsetList(TReportsetCQ subQuery) {
            if (_qcwebid_InScopeSubQuery_TReportsetListMap == null) { _qcwebid_InScopeSubQuery_TReportsetListMap = new LinkedHashMap<String, TReportsetCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_InScopeSubQuery_TReportsetListMap.size() + 1);
            _qcwebid_InScopeSubQuery_TReportsetListMap.put(key, subQuery); return "Qcwebid_InScopeSubQuery_TReportsetList." + key;
        }

        protected Map<String, TScenarioTotalizationCQ> _qcwebid_InScopeSubQuery_TScenarioTotalizationListMap;
        public Map<String, TScenarioTotalizationCQ> Qcwebid_InScopeSubQuery_TScenarioTotalizationList { get { return _qcwebid_InScopeSubQuery_TScenarioTotalizationListMap; }}
        public override String keepQcwebid_InScopeSubQuery_TScenarioTotalizationList(TScenarioTotalizationCQ subQuery) {
            if (_qcwebid_InScopeSubQuery_TScenarioTotalizationListMap == null) { _qcwebid_InScopeSubQuery_TScenarioTotalizationListMap = new LinkedHashMap<String, TScenarioTotalizationCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_InScopeSubQuery_TScenarioTotalizationListMap.size() + 1);
            _qcwebid_InScopeSubQuery_TScenarioTotalizationListMap.put(key, subQuery); return "Qcwebid_InScopeSubQuery_TScenarioTotalizationList." + key;
        }

        protected Map<String, TSelectConditionInfoCQ> _qcwebid_InScopeSubQuery_TSelectConditionInfoListMap;
        public Map<String, TSelectConditionInfoCQ> Qcwebid_InScopeSubQuery_TSelectConditionInfoList { get { return _qcwebid_InScopeSubQuery_TSelectConditionInfoListMap; }}
        public override String keepQcwebid_InScopeSubQuery_TSelectConditionInfoList(TSelectConditionInfoCQ subQuery) {
            if (_qcwebid_InScopeSubQuery_TSelectConditionInfoListMap == null) { _qcwebid_InScopeSubQuery_TSelectConditionInfoListMap = new LinkedHashMap<String, TSelectConditionInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_InScopeSubQuery_TSelectConditionInfoListMap.size() + 1);
            _qcwebid_InScopeSubQuery_TSelectConditionInfoListMap.put(key, subQuery); return "Qcwebid_InScopeSubQuery_TSelectConditionInfoList." + key;
        }

        protected Map<String, TSessionControlerCQ> _qcwebid_InScopeSubQuery_TSessionControlerListMap;
        public Map<String, TSessionControlerCQ> Qcwebid_InScopeSubQuery_TSessionControlerList { get { return _qcwebid_InScopeSubQuery_TSessionControlerListMap; }}
        public override String keepQcwebid_InScopeSubQuery_TSessionControlerList(TSessionControlerCQ subQuery) {
            if (_qcwebid_InScopeSubQuery_TSessionControlerListMap == null) { _qcwebid_InScopeSubQuery_TSessionControlerListMap = new LinkedHashMap<String, TSessionControlerCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_InScopeSubQuery_TSessionControlerListMap.size() + 1);
            _qcwebid_InScopeSubQuery_TSessionControlerListMap.put(key, subQuery); return "Qcwebid_InScopeSubQuery_TSessionControlerList." + key;
        }

        protected Map<String, TWeightbackCQ> _qcwebid_InScopeSubQuery_TWeightbackListMap;
        public Map<String, TWeightbackCQ> Qcwebid_InScopeSubQuery_TWeightbackList { get { return _qcwebid_InScopeSubQuery_TWeightbackListMap; }}
        public override String keepQcwebid_InScopeSubQuery_TWeightbackList(TWeightbackCQ subQuery) {
            if (_qcwebid_InScopeSubQuery_TWeightbackListMap == null) { _qcwebid_InScopeSubQuery_TWeightbackListMap = new LinkedHashMap<String, TWeightbackCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_InScopeSubQuery_TWeightbackListMap.size() + 1);
            _qcwebid_InScopeSubQuery_TWeightbackListMap.put(key, subQuery); return "Qcwebid_InScopeSubQuery_TWeightbackList." + key;
        }

        protected Map<String, TAllocationCellInfoCQ> _qcwebid_NotInScopeSubQuery_TAllocationCellInfoMap;
        public Map<String, TAllocationCellInfoCQ> Qcwebid_NotInScopeSubQuery_TAllocationCellInfo { get { return _qcwebid_NotInScopeSubQuery_TAllocationCellInfoMap; }}
        public override String keepQcwebid_NotInScopeSubQuery_TAllocationCellInfo(TAllocationCellInfoCQ subQuery) {
            if (_qcwebid_NotInScopeSubQuery_TAllocationCellInfoMap == null) { _qcwebid_NotInScopeSubQuery_TAllocationCellInfoMap = new LinkedHashMap<String, TAllocationCellInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotInScopeSubQuery_TAllocationCellInfoMap.size() + 1);
            _qcwebid_NotInScopeSubQuery_TAllocationCellInfoMap.put(key, subQuery); return "Qcwebid_NotInScopeSubQuery_TAllocationCellInfo." + key;
        }

        protected Map<String, TAccessPermissionsInfoCQ> _qcwebid_NotInScopeSubQuery_TAccessPermissionsInfoAsOneMap;
        public Map<String, TAccessPermissionsInfoCQ> Qcwebid_NotInScopeSubQuery_TAccessPermissionsInfoAsOne { get { return _qcwebid_NotInScopeSubQuery_TAccessPermissionsInfoAsOneMap; }}
        public override String keepQcwebid_NotInScopeSubQuery_TAccessPermissionsInfoAsOne(TAccessPermissionsInfoCQ subQuery) {
            if (_qcwebid_NotInScopeSubQuery_TAccessPermissionsInfoAsOneMap == null) { _qcwebid_NotInScopeSubQuery_TAccessPermissionsInfoAsOneMap = new LinkedHashMap<String, TAccessPermissionsInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotInScopeSubQuery_TAccessPermissionsInfoAsOneMap.size() + 1);
            _qcwebid_NotInScopeSubQuery_TAccessPermissionsInfoAsOneMap.put(key, subQuery); return "Qcwebid_NotInScopeSubQuery_TAccessPermissionsInfoAsOne." + key;
        }

        protected Map<String, TAllocationCellInfoCQ> _qcwebid_NotInScopeSubQuery_TAllocationCellInfoListMap;
        public Map<String, TAllocationCellInfoCQ> Qcwebid_NotInScopeSubQuery_TAllocationCellInfoList { get { return _qcwebid_NotInScopeSubQuery_TAllocationCellInfoListMap; }}
        public override String keepQcwebid_NotInScopeSubQuery_TAllocationCellInfoList(TAllocationCellInfoCQ subQuery) {
            if (_qcwebid_NotInScopeSubQuery_TAllocationCellInfoListMap == null) { _qcwebid_NotInScopeSubQuery_TAllocationCellInfoListMap = new LinkedHashMap<String, TAllocationCellInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotInScopeSubQuery_TAllocationCellInfoListMap.size() + 1);
            _qcwebid_NotInScopeSubQuery_TAllocationCellInfoListMap.put(key, subQuery); return "Qcwebid_NotInScopeSubQuery_TAllocationCellInfoList." + key;
        }

        protected Map<String, TDataEditListCQ> _qcwebid_NotInScopeSubQuery_TDataEditListListMap;
        public Map<String, TDataEditListCQ> Qcwebid_NotInScopeSubQuery_TDataEditListList { get { return _qcwebid_NotInScopeSubQuery_TDataEditListListMap; }}
        public override String keepQcwebid_NotInScopeSubQuery_TDataEditListList(TDataEditListCQ subQuery) {
            if (_qcwebid_NotInScopeSubQuery_TDataEditListListMap == null) { _qcwebid_NotInScopeSubQuery_TDataEditListListMap = new LinkedHashMap<String, TDataEditListCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotInScopeSubQuery_TDataEditListListMap.size() + 1);
            _qcwebid_NotInScopeSubQuery_TDataEditListListMap.put(key, subQuery); return "Qcwebid_NotInScopeSubQuery_TDataEditListList." + key;
        }

        protected Map<String, TItemInfoCQ> _qcwebid_NotInScopeSubQuery_TItemInfoListMap;
        public Map<String, TItemInfoCQ> Qcwebid_NotInScopeSubQuery_TItemInfoList { get { return _qcwebid_NotInScopeSubQuery_TItemInfoListMap; }}
        public override String keepQcwebid_NotInScopeSubQuery_TItemInfoList(TItemInfoCQ subQuery) {
            if (_qcwebid_NotInScopeSubQuery_TItemInfoListMap == null) { _qcwebid_NotInScopeSubQuery_TItemInfoListMap = new LinkedHashMap<String, TItemInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotInScopeSubQuery_TItemInfoListMap.size() + 1);
            _qcwebid_NotInScopeSubQuery_TItemInfoListMap.put(key, subQuery); return "Qcwebid_NotInScopeSubQuery_TItemInfoList." + key;
        }

        protected Map<String, TNoticeCQ> _qcwebid_NotInScopeSubQuery_TNoticeListMap;
        public Map<String, TNoticeCQ> Qcwebid_NotInScopeSubQuery_TNoticeList { get { return _qcwebid_NotInScopeSubQuery_TNoticeListMap; }}
        public override String keepQcwebid_NotInScopeSubQuery_TNoticeList(TNoticeCQ subQuery) {
            if (_qcwebid_NotInScopeSubQuery_TNoticeListMap == null) { _qcwebid_NotInScopeSubQuery_TNoticeListMap = new LinkedHashMap<String, TNoticeCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotInScopeSubQuery_TNoticeListMap.size() + 1);
            _qcwebid_NotInScopeSubQuery_TNoticeListMap.put(key, subQuery); return "Qcwebid_NotInScopeSubQuery_TNoticeList." + key;
        }

        protected Map<String, TOutputRequestCQ> _qcwebid_NotInScopeSubQuery_TOutputRequestListMap;
        public Map<String, TOutputRequestCQ> Qcwebid_NotInScopeSubQuery_TOutputRequestList { get { return _qcwebid_NotInScopeSubQuery_TOutputRequestListMap; }}
        public override String keepQcwebid_NotInScopeSubQuery_TOutputRequestList(TOutputRequestCQ subQuery) {
            if (_qcwebid_NotInScopeSubQuery_TOutputRequestListMap == null) { _qcwebid_NotInScopeSubQuery_TOutputRequestListMap = new LinkedHashMap<String, TOutputRequestCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotInScopeSubQuery_TOutputRequestListMap.size() + 1);
            _qcwebid_NotInScopeSubQuery_TOutputRequestListMap.put(key, subQuery); return "Qcwebid_NotInScopeSubQuery_TOutputRequestList." + key;
        }

        protected Map<String, TOutputSettingCQ> _qcwebid_NotInScopeSubQuery_TOutputSettingAsOneMap;
        public Map<String, TOutputSettingCQ> Qcwebid_NotInScopeSubQuery_TOutputSettingAsOne { get { return _qcwebid_NotInScopeSubQuery_TOutputSettingAsOneMap; }}
        public override String keepQcwebid_NotInScopeSubQuery_TOutputSettingAsOne(TOutputSettingCQ subQuery) {
            if (_qcwebid_NotInScopeSubQuery_TOutputSettingAsOneMap == null) { _qcwebid_NotInScopeSubQuery_TOutputSettingAsOneMap = new LinkedHashMap<String, TOutputSettingCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotInScopeSubQuery_TOutputSettingAsOneMap.size() + 1);
            _qcwebid_NotInScopeSubQuery_TOutputSettingAsOneMap.put(key, subQuery); return "Qcwebid_NotInScopeSubQuery_TOutputSettingAsOne." + key;
        }

        protected Map<String, TOutputSettingCrossCQ> _qcwebid_NotInScopeSubQuery_TOutputSettingCrossAsOneMap;
        public Map<String, TOutputSettingCrossCQ> Qcwebid_NotInScopeSubQuery_TOutputSettingCrossAsOne { get { return _qcwebid_NotInScopeSubQuery_TOutputSettingCrossAsOneMap; }}
        public override String keepQcwebid_NotInScopeSubQuery_TOutputSettingCrossAsOne(TOutputSettingCrossCQ subQuery) {
            if (_qcwebid_NotInScopeSubQuery_TOutputSettingCrossAsOneMap == null) { _qcwebid_NotInScopeSubQuery_TOutputSettingCrossAsOneMap = new LinkedHashMap<String, TOutputSettingCrossCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotInScopeSubQuery_TOutputSettingCrossAsOneMap.size() + 1);
            _qcwebid_NotInScopeSubQuery_TOutputSettingCrossAsOneMap.put(key, subQuery); return "Qcwebid_NotInScopeSubQuery_TOutputSettingCrossAsOne." + key;
        }

        protected Map<String, TOutputSettingFaCQ> _qcwebid_NotInScopeSubQuery_TOutputSettingFaAsOneMap;
        public Map<String, TOutputSettingFaCQ> Qcwebid_NotInScopeSubQuery_TOutputSettingFaAsOne { get { return _qcwebid_NotInScopeSubQuery_TOutputSettingFaAsOneMap; }}
        public override String keepQcwebid_NotInScopeSubQuery_TOutputSettingFaAsOne(TOutputSettingFaCQ subQuery) {
            if (_qcwebid_NotInScopeSubQuery_TOutputSettingFaAsOneMap == null) { _qcwebid_NotInScopeSubQuery_TOutputSettingFaAsOneMap = new LinkedHashMap<String, TOutputSettingFaCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotInScopeSubQuery_TOutputSettingFaAsOneMap.size() + 1);
            _qcwebid_NotInScopeSubQuery_TOutputSettingFaAsOneMap.put(key, subQuery); return "Qcwebid_NotInScopeSubQuery_TOutputSettingFaAsOne." + key;
        }

        protected Map<String, TOutputSettingGtCQ> _qcwebid_NotInScopeSubQuery_TOutputSettingGtAsOneMap;
        public Map<String, TOutputSettingGtCQ> Qcwebid_NotInScopeSubQuery_TOutputSettingGtAsOne { get { return _qcwebid_NotInScopeSubQuery_TOutputSettingGtAsOneMap; }}
        public override String keepQcwebid_NotInScopeSubQuery_TOutputSettingGtAsOne(TOutputSettingGtCQ subQuery) {
            if (_qcwebid_NotInScopeSubQuery_TOutputSettingGtAsOneMap == null) { _qcwebid_NotInScopeSubQuery_TOutputSettingGtAsOneMap = new LinkedHashMap<String, TOutputSettingGtCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotInScopeSubQuery_TOutputSettingGtAsOneMap.size() + 1);
            _qcwebid_NotInScopeSubQuery_TOutputSettingGtAsOneMap.put(key, subQuery); return "Qcwebid_NotInScopeSubQuery_TOutputSettingGtAsOne." + key;
        }

        protected Map<String, TOutputSettingReportCQ> _qcwebid_NotInScopeSubQuery_TOutputSettingReportAsOneMap;
        public Map<String, TOutputSettingReportCQ> Qcwebid_NotInScopeSubQuery_TOutputSettingReportAsOne { get { return _qcwebid_NotInScopeSubQuery_TOutputSettingReportAsOneMap; }}
        public override String keepQcwebid_NotInScopeSubQuery_TOutputSettingReportAsOne(TOutputSettingReportCQ subQuery) {
            if (_qcwebid_NotInScopeSubQuery_TOutputSettingReportAsOneMap == null) { _qcwebid_NotInScopeSubQuery_TOutputSettingReportAsOneMap = new LinkedHashMap<String, TOutputSettingReportCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotInScopeSubQuery_TOutputSettingReportAsOneMap.size() + 1);
            _qcwebid_NotInScopeSubQuery_TOutputSettingReportAsOneMap.put(key, subQuery); return "Qcwebid_NotInScopeSubQuery_TOutputSettingReportAsOne." + key;
        }

        protected Map<String, TOutputTemplateCQ> _qcwebid_NotInScopeSubQuery_TOutputTemplateListMap;
        public Map<String, TOutputTemplateCQ> Qcwebid_NotInScopeSubQuery_TOutputTemplateList { get { return _qcwebid_NotInScopeSubQuery_TOutputTemplateListMap; }}
        public override String keepQcwebid_NotInScopeSubQuery_TOutputTemplateList(TOutputTemplateCQ subQuery) {
            if (_qcwebid_NotInScopeSubQuery_TOutputTemplateListMap == null) { _qcwebid_NotInScopeSubQuery_TOutputTemplateListMap = new LinkedHashMap<String, TOutputTemplateCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotInScopeSubQuery_TOutputTemplateListMap.size() + 1);
            _qcwebid_NotInScopeSubQuery_TOutputTemplateListMap.put(key, subQuery); return "Qcwebid_NotInScopeSubQuery_TOutputTemplateList." + key;
        }

        protected Map<String, TQcwebSurveyDetailCQ> _qcwebid_NotInScopeSubQuery_TQcwebSurveyDetailListMap;
        public Map<String, TQcwebSurveyDetailCQ> Qcwebid_NotInScopeSubQuery_TQcwebSurveyDetailList { get { return _qcwebid_NotInScopeSubQuery_TQcwebSurveyDetailListMap; }}
        public override String keepQcwebid_NotInScopeSubQuery_TQcwebSurveyDetailList(TQcwebSurveyDetailCQ subQuery) {
            if (_qcwebid_NotInScopeSubQuery_TQcwebSurveyDetailListMap == null) { _qcwebid_NotInScopeSubQuery_TQcwebSurveyDetailListMap = new LinkedHashMap<String, TQcwebSurveyDetailCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotInScopeSubQuery_TQcwebSurveyDetailListMap.size() + 1);
            _qcwebid_NotInScopeSubQuery_TQcwebSurveyDetailListMap.put(key, subQuery); return "Qcwebid_NotInScopeSubQuery_TQcwebSurveyDetailList." + key;
        }

        protected Map<String, TRawdataImportQueInfoCQ> _qcwebid_NotInScopeSubQuery_TRawdataImportQueInfoListMap;
        public Map<String, TRawdataImportQueInfoCQ> Qcwebid_NotInScopeSubQuery_TRawdataImportQueInfoList { get { return _qcwebid_NotInScopeSubQuery_TRawdataImportQueInfoListMap; }}
        public override String keepQcwebid_NotInScopeSubQuery_TRawdataImportQueInfoList(TRawdataImportQueInfoCQ subQuery) {
            if (_qcwebid_NotInScopeSubQuery_TRawdataImportQueInfoListMap == null) { _qcwebid_NotInScopeSubQuery_TRawdataImportQueInfoListMap = new LinkedHashMap<String, TRawdataImportQueInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotInScopeSubQuery_TRawdataImportQueInfoListMap.size() + 1);
            _qcwebid_NotInScopeSubQuery_TRawdataImportQueInfoListMap.put(key, subQuery); return "Qcwebid_NotInScopeSubQuery_TRawdataImportQueInfoList." + key;
        }

        protected Map<String, TReportsetCQ> _qcwebid_NotInScopeSubQuery_TReportsetListMap;
        public Map<String, TReportsetCQ> Qcwebid_NotInScopeSubQuery_TReportsetList { get { return _qcwebid_NotInScopeSubQuery_TReportsetListMap; }}
        public override String keepQcwebid_NotInScopeSubQuery_TReportsetList(TReportsetCQ subQuery) {
            if (_qcwebid_NotInScopeSubQuery_TReportsetListMap == null) { _qcwebid_NotInScopeSubQuery_TReportsetListMap = new LinkedHashMap<String, TReportsetCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotInScopeSubQuery_TReportsetListMap.size() + 1);
            _qcwebid_NotInScopeSubQuery_TReportsetListMap.put(key, subQuery); return "Qcwebid_NotInScopeSubQuery_TReportsetList." + key;
        }

        protected Map<String, TScenarioTotalizationCQ> _qcwebid_NotInScopeSubQuery_TScenarioTotalizationListMap;
        public Map<String, TScenarioTotalizationCQ> Qcwebid_NotInScopeSubQuery_TScenarioTotalizationList { get { return _qcwebid_NotInScopeSubQuery_TScenarioTotalizationListMap; }}
        public override String keepQcwebid_NotInScopeSubQuery_TScenarioTotalizationList(TScenarioTotalizationCQ subQuery) {
            if (_qcwebid_NotInScopeSubQuery_TScenarioTotalizationListMap == null) { _qcwebid_NotInScopeSubQuery_TScenarioTotalizationListMap = new LinkedHashMap<String, TScenarioTotalizationCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotInScopeSubQuery_TScenarioTotalizationListMap.size() + 1);
            _qcwebid_NotInScopeSubQuery_TScenarioTotalizationListMap.put(key, subQuery); return "Qcwebid_NotInScopeSubQuery_TScenarioTotalizationList." + key;
        }

        protected Map<String, TSelectConditionInfoCQ> _qcwebid_NotInScopeSubQuery_TSelectConditionInfoListMap;
        public Map<String, TSelectConditionInfoCQ> Qcwebid_NotInScopeSubQuery_TSelectConditionInfoList { get { return _qcwebid_NotInScopeSubQuery_TSelectConditionInfoListMap; }}
        public override String keepQcwebid_NotInScopeSubQuery_TSelectConditionInfoList(TSelectConditionInfoCQ subQuery) {
            if (_qcwebid_NotInScopeSubQuery_TSelectConditionInfoListMap == null) { _qcwebid_NotInScopeSubQuery_TSelectConditionInfoListMap = new LinkedHashMap<String, TSelectConditionInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotInScopeSubQuery_TSelectConditionInfoListMap.size() + 1);
            _qcwebid_NotInScopeSubQuery_TSelectConditionInfoListMap.put(key, subQuery); return "Qcwebid_NotInScopeSubQuery_TSelectConditionInfoList." + key;
        }

        protected Map<String, TSessionControlerCQ> _qcwebid_NotInScopeSubQuery_TSessionControlerListMap;
        public Map<String, TSessionControlerCQ> Qcwebid_NotInScopeSubQuery_TSessionControlerList { get { return _qcwebid_NotInScopeSubQuery_TSessionControlerListMap; }}
        public override String keepQcwebid_NotInScopeSubQuery_TSessionControlerList(TSessionControlerCQ subQuery) {
            if (_qcwebid_NotInScopeSubQuery_TSessionControlerListMap == null) { _qcwebid_NotInScopeSubQuery_TSessionControlerListMap = new LinkedHashMap<String, TSessionControlerCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotInScopeSubQuery_TSessionControlerListMap.size() + 1);
            _qcwebid_NotInScopeSubQuery_TSessionControlerListMap.put(key, subQuery); return "Qcwebid_NotInScopeSubQuery_TSessionControlerList." + key;
        }

        protected Map<String, TWeightbackCQ> _qcwebid_NotInScopeSubQuery_TWeightbackListMap;
        public Map<String, TWeightbackCQ> Qcwebid_NotInScopeSubQuery_TWeightbackList { get { return _qcwebid_NotInScopeSubQuery_TWeightbackListMap; }}
        public override String keepQcwebid_NotInScopeSubQuery_TWeightbackList(TWeightbackCQ subQuery) {
            if (_qcwebid_NotInScopeSubQuery_TWeightbackListMap == null) { _qcwebid_NotInScopeSubQuery_TWeightbackListMap = new LinkedHashMap<String, TWeightbackCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotInScopeSubQuery_TWeightbackListMap.size() + 1);
            _qcwebid_NotInScopeSubQuery_TWeightbackListMap.put(key, subQuery); return "Qcwebid_NotInScopeSubQuery_TWeightbackList." + key;
        }

        protected Map<String, TAllocationCellInfoCQ> _qcwebid_SpecifyDerivedReferrer_TAllocationCellInfoListMap;
        public Map<String, TAllocationCellInfoCQ> Qcwebid_SpecifyDerivedReferrer_TAllocationCellInfoList { get { return _qcwebid_SpecifyDerivedReferrer_TAllocationCellInfoListMap; }}
        public override String keepQcwebid_SpecifyDerivedReferrer_TAllocationCellInfoList(TAllocationCellInfoCQ subQuery) {
            if (_qcwebid_SpecifyDerivedReferrer_TAllocationCellInfoListMap == null) { _qcwebid_SpecifyDerivedReferrer_TAllocationCellInfoListMap = new LinkedHashMap<String, TAllocationCellInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_SpecifyDerivedReferrer_TAllocationCellInfoListMap.size() + 1);
            _qcwebid_SpecifyDerivedReferrer_TAllocationCellInfoListMap.put(key, subQuery); return "Qcwebid_SpecifyDerivedReferrer_TAllocationCellInfoList." + key;
        }

        protected Map<String, TDataEditListCQ> _qcwebid_SpecifyDerivedReferrer_TDataEditListListMap;
        public Map<String, TDataEditListCQ> Qcwebid_SpecifyDerivedReferrer_TDataEditListList { get { return _qcwebid_SpecifyDerivedReferrer_TDataEditListListMap; }}
        public override String keepQcwebid_SpecifyDerivedReferrer_TDataEditListList(TDataEditListCQ subQuery) {
            if (_qcwebid_SpecifyDerivedReferrer_TDataEditListListMap == null) { _qcwebid_SpecifyDerivedReferrer_TDataEditListListMap = new LinkedHashMap<String, TDataEditListCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_SpecifyDerivedReferrer_TDataEditListListMap.size() + 1);
            _qcwebid_SpecifyDerivedReferrer_TDataEditListListMap.put(key, subQuery); return "Qcwebid_SpecifyDerivedReferrer_TDataEditListList." + key;
        }

        protected Map<String, TItemInfoCQ> _qcwebid_SpecifyDerivedReferrer_TItemInfoListMap;
        public Map<String, TItemInfoCQ> Qcwebid_SpecifyDerivedReferrer_TItemInfoList { get { return _qcwebid_SpecifyDerivedReferrer_TItemInfoListMap; }}
        public override String keepQcwebid_SpecifyDerivedReferrer_TItemInfoList(TItemInfoCQ subQuery) {
            if (_qcwebid_SpecifyDerivedReferrer_TItemInfoListMap == null) { _qcwebid_SpecifyDerivedReferrer_TItemInfoListMap = new LinkedHashMap<String, TItemInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_SpecifyDerivedReferrer_TItemInfoListMap.size() + 1);
            _qcwebid_SpecifyDerivedReferrer_TItemInfoListMap.put(key, subQuery); return "Qcwebid_SpecifyDerivedReferrer_TItemInfoList." + key;
        }

        protected Map<String, TNoticeCQ> _qcwebid_SpecifyDerivedReferrer_TNoticeListMap;
        public Map<String, TNoticeCQ> Qcwebid_SpecifyDerivedReferrer_TNoticeList { get { return _qcwebid_SpecifyDerivedReferrer_TNoticeListMap; }}
        public override String keepQcwebid_SpecifyDerivedReferrer_TNoticeList(TNoticeCQ subQuery) {
            if (_qcwebid_SpecifyDerivedReferrer_TNoticeListMap == null) { _qcwebid_SpecifyDerivedReferrer_TNoticeListMap = new LinkedHashMap<String, TNoticeCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_SpecifyDerivedReferrer_TNoticeListMap.size() + 1);
            _qcwebid_SpecifyDerivedReferrer_TNoticeListMap.put(key, subQuery); return "Qcwebid_SpecifyDerivedReferrer_TNoticeList." + key;
        }

        protected Map<String, TOutputRequestCQ> _qcwebid_SpecifyDerivedReferrer_TOutputRequestListMap;
        public Map<String, TOutputRequestCQ> Qcwebid_SpecifyDerivedReferrer_TOutputRequestList { get { return _qcwebid_SpecifyDerivedReferrer_TOutputRequestListMap; }}
        public override String keepQcwebid_SpecifyDerivedReferrer_TOutputRequestList(TOutputRequestCQ subQuery) {
            if (_qcwebid_SpecifyDerivedReferrer_TOutputRequestListMap == null) { _qcwebid_SpecifyDerivedReferrer_TOutputRequestListMap = new LinkedHashMap<String, TOutputRequestCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_SpecifyDerivedReferrer_TOutputRequestListMap.size() + 1);
            _qcwebid_SpecifyDerivedReferrer_TOutputRequestListMap.put(key, subQuery); return "Qcwebid_SpecifyDerivedReferrer_TOutputRequestList." + key;
        }

        protected Map<String, TOutputTemplateCQ> _qcwebid_SpecifyDerivedReferrer_TOutputTemplateListMap;
        public Map<String, TOutputTemplateCQ> Qcwebid_SpecifyDerivedReferrer_TOutputTemplateList { get { return _qcwebid_SpecifyDerivedReferrer_TOutputTemplateListMap; }}
        public override String keepQcwebid_SpecifyDerivedReferrer_TOutputTemplateList(TOutputTemplateCQ subQuery) {
            if (_qcwebid_SpecifyDerivedReferrer_TOutputTemplateListMap == null) { _qcwebid_SpecifyDerivedReferrer_TOutputTemplateListMap = new LinkedHashMap<String, TOutputTemplateCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_SpecifyDerivedReferrer_TOutputTemplateListMap.size() + 1);
            _qcwebid_SpecifyDerivedReferrer_TOutputTemplateListMap.put(key, subQuery); return "Qcwebid_SpecifyDerivedReferrer_TOutputTemplateList." + key;
        }

        protected Map<String, TQcwebSurveyDetailCQ> _qcwebid_SpecifyDerivedReferrer_TQcwebSurveyDetailListMap;
        public Map<String, TQcwebSurveyDetailCQ> Qcwebid_SpecifyDerivedReferrer_TQcwebSurveyDetailList { get { return _qcwebid_SpecifyDerivedReferrer_TQcwebSurveyDetailListMap; }}
        public override String keepQcwebid_SpecifyDerivedReferrer_TQcwebSurveyDetailList(TQcwebSurveyDetailCQ subQuery) {
            if (_qcwebid_SpecifyDerivedReferrer_TQcwebSurveyDetailListMap == null) { _qcwebid_SpecifyDerivedReferrer_TQcwebSurveyDetailListMap = new LinkedHashMap<String, TQcwebSurveyDetailCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_SpecifyDerivedReferrer_TQcwebSurveyDetailListMap.size() + 1);
            _qcwebid_SpecifyDerivedReferrer_TQcwebSurveyDetailListMap.put(key, subQuery); return "Qcwebid_SpecifyDerivedReferrer_TQcwebSurveyDetailList." + key;
        }

        protected Map<String, TRawdataImportQueInfoCQ> _qcwebid_SpecifyDerivedReferrer_TRawdataImportQueInfoListMap;
        public Map<String, TRawdataImportQueInfoCQ> Qcwebid_SpecifyDerivedReferrer_TRawdataImportQueInfoList { get { return _qcwebid_SpecifyDerivedReferrer_TRawdataImportQueInfoListMap; }}
        public override String keepQcwebid_SpecifyDerivedReferrer_TRawdataImportQueInfoList(TRawdataImportQueInfoCQ subQuery) {
            if (_qcwebid_SpecifyDerivedReferrer_TRawdataImportQueInfoListMap == null) { _qcwebid_SpecifyDerivedReferrer_TRawdataImportQueInfoListMap = new LinkedHashMap<String, TRawdataImportQueInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_SpecifyDerivedReferrer_TRawdataImportQueInfoListMap.size() + 1);
            _qcwebid_SpecifyDerivedReferrer_TRawdataImportQueInfoListMap.put(key, subQuery); return "Qcwebid_SpecifyDerivedReferrer_TRawdataImportQueInfoList." + key;
        }

        protected Map<String, TReportsetCQ> _qcwebid_SpecifyDerivedReferrer_TReportsetListMap;
        public Map<String, TReportsetCQ> Qcwebid_SpecifyDerivedReferrer_TReportsetList { get { return _qcwebid_SpecifyDerivedReferrer_TReportsetListMap; }}
        public override String keepQcwebid_SpecifyDerivedReferrer_TReportsetList(TReportsetCQ subQuery) {
            if (_qcwebid_SpecifyDerivedReferrer_TReportsetListMap == null) { _qcwebid_SpecifyDerivedReferrer_TReportsetListMap = new LinkedHashMap<String, TReportsetCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_SpecifyDerivedReferrer_TReportsetListMap.size() + 1);
            _qcwebid_SpecifyDerivedReferrer_TReportsetListMap.put(key, subQuery); return "Qcwebid_SpecifyDerivedReferrer_TReportsetList." + key;
        }

        protected Map<String, TScenarioTotalizationCQ> _qcwebid_SpecifyDerivedReferrer_TScenarioTotalizationListMap;
        public Map<String, TScenarioTotalizationCQ> Qcwebid_SpecifyDerivedReferrer_TScenarioTotalizationList { get { return _qcwebid_SpecifyDerivedReferrer_TScenarioTotalizationListMap; }}
        public override String keepQcwebid_SpecifyDerivedReferrer_TScenarioTotalizationList(TScenarioTotalizationCQ subQuery) {
            if (_qcwebid_SpecifyDerivedReferrer_TScenarioTotalizationListMap == null) { _qcwebid_SpecifyDerivedReferrer_TScenarioTotalizationListMap = new LinkedHashMap<String, TScenarioTotalizationCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_SpecifyDerivedReferrer_TScenarioTotalizationListMap.size() + 1);
            _qcwebid_SpecifyDerivedReferrer_TScenarioTotalizationListMap.put(key, subQuery); return "Qcwebid_SpecifyDerivedReferrer_TScenarioTotalizationList." + key;
        }

        protected Map<String, TSelectConditionInfoCQ> _qcwebid_SpecifyDerivedReferrer_TSelectConditionInfoListMap;
        public Map<String, TSelectConditionInfoCQ> Qcwebid_SpecifyDerivedReferrer_TSelectConditionInfoList { get { return _qcwebid_SpecifyDerivedReferrer_TSelectConditionInfoListMap; }}
        public override String keepQcwebid_SpecifyDerivedReferrer_TSelectConditionInfoList(TSelectConditionInfoCQ subQuery) {
            if (_qcwebid_SpecifyDerivedReferrer_TSelectConditionInfoListMap == null) { _qcwebid_SpecifyDerivedReferrer_TSelectConditionInfoListMap = new LinkedHashMap<String, TSelectConditionInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_SpecifyDerivedReferrer_TSelectConditionInfoListMap.size() + 1);
            _qcwebid_SpecifyDerivedReferrer_TSelectConditionInfoListMap.put(key, subQuery); return "Qcwebid_SpecifyDerivedReferrer_TSelectConditionInfoList." + key;
        }

        protected Map<String, TSessionControlerCQ> _qcwebid_SpecifyDerivedReferrer_TSessionControlerListMap;
        public Map<String, TSessionControlerCQ> Qcwebid_SpecifyDerivedReferrer_TSessionControlerList { get { return _qcwebid_SpecifyDerivedReferrer_TSessionControlerListMap; }}
        public override String keepQcwebid_SpecifyDerivedReferrer_TSessionControlerList(TSessionControlerCQ subQuery) {
            if (_qcwebid_SpecifyDerivedReferrer_TSessionControlerListMap == null) { _qcwebid_SpecifyDerivedReferrer_TSessionControlerListMap = new LinkedHashMap<String, TSessionControlerCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_SpecifyDerivedReferrer_TSessionControlerListMap.size() + 1);
            _qcwebid_SpecifyDerivedReferrer_TSessionControlerListMap.put(key, subQuery); return "Qcwebid_SpecifyDerivedReferrer_TSessionControlerList." + key;
        }

        protected Map<String, TWeightbackCQ> _qcwebid_SpecifyDerivedReferrer_TWeightbackListMap;
        public Map<String, TWeightbackCQ> Qcwebid_SpecifyDerivedReferrer_TWeightbackList { get { return _qcwebid_SpecifyDerivedReferrer_TWeightbackListMap; }}
        public override String keepQcwebid_SpecifyDerivedReferrer_TWeightbackList(TWeightbackCQ subQuery) {
            if (_qcwebid_SpecifyDerivedReferrer_TWeightbackListMap == null) { _qcwebid_SpecifyDerivedReferrer_TWeightbackListMap = new LinkedHashMap<String, TWeightbackCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_SpecifyDerivedReferrer_TWeightbackListMap.size() + 1);
            _qcwebid_SpecifyDerivedReferrer_TWeightbackListMap.put(key, subQuery); return "Qcwebid_SpecifyDerivedReferrer_TWeightbackList." + key;
        }

        protected Map<String, TAllocationCellInfoCQ> _qcwebid_QueryDerivedReferrer_TAllocationCellInfoListMap;
        public Map<String, TAllocationCellInfoCQ> Qcwebid_QueryDerivedReferrer_TAllocationCellInfoList { get { return _qcwebid_QueryDerivedReferrer_TAllocationCellInfoListMap; } }
        public override String keepQcwebid_QueryDerivedReferrer_TAllocationCellInfoList(TAllocationCellInfoCQ subQuery) {
            if (_qcwebid_QueryDerivedReferrer_TAllocationCellInfoListMap == null) { _qcwebid_QueryDerivedReferrer_TAllocationCellInfoListMap = new LinkedHashMap<String, TAllocationCellInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_QueryDerivedReferrer_TAllocationCellInfoListMap.size() + 1);
            _qcwebid_QueryDerivedReferrer_TAllocationCellInfoListMap.put(key, subQuery); return "Qcwebid_QueryDerivedReferrer_TAllocationCellInfoList." + key;
        }
        protected Map<String, Object> _qcwebid_QueryDerivedReferrer_TAllocationCellInfoListParameterMap;
        public Map<String, Object> Qcwebid_QueryDerivedReferrer_TAllocationCellInfoListParameter { get { return _qcwebid_QueryDerivedReferrer_TAllocationCellInfoListParameterMap; } }
        public override String keepQcwebid_QueryDerivedReferrer_TAllocationCellInfoListParameter(Object parameterValue) {
            if (_qcwebid_QueryDerivedReferrer_TAllocationCellInfoListParameterMap == null) { _qcwebid_QueryDerivedReferrer_TAllocationCellInfoListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_qcwebid_QueryDerivedReferrer_TAllocationCellInfoListParameterMap.size() + 1);
            _qcwebid_QueryDerivedReferrer_TAllocationCellInfoListParameterMap.put(key, parameterValue); return "Qcwebid_QueryDerivedReferrer_TAllocationCellInfoListParameter." + key;
        }

        protected Map<String, TDataEditListCQ> _qcwebid_QueryDerivedReferrer_TDataEditListListMap;
        public Map<String, TDataEditListCQ> Qcwebid_QueryDerivedReferrer_TDataEditListList { get { return _qcwebid_QueryDerivedReferrer_TDataEditListListMap; } }
        public override String keepQcwebid_QueryDerivedReferrer_TDataEditListList(TDataEditListCQ subQuery) {
            if (_qcwebid_QueryDerivedReferrer_TDataEditListListMap == null) { _qcwebid_QueryDerivedReferrer_TDataEditListListMap = new LinkedHashMap<String, TDataEditListCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_QueryDerivedReferrer_TDataEditListListMap.size() + 1);
            _qcwebid_QueryDerivedReferrer_TDataEditListListMap.put(key, subQuery); return "Qcwebid_QueryDerivedReferrer_TDataEditListList." + key;
        }
        protected Map<String, Object> _qcwebid_QueryDerivedReferrer_TDataEditListListParameterMap;
        public Map<String, Object> Qcwebid_QueryDerivedReferrer_TDataEditListListParameter { get { return _qcwebid_QueryDerivedReferrer_TDataEditListListParameterMap; } }
        public override String keepQcwebid_QueryDerivedReferrer_TDataEditListListParameter(Object parameterValue) {
            if (_qcwebid_QueryDerivedReferrer_TDataEditListListParameterMap == null) { _qcwebid_QueryDerivedReferrer_TDataEditListListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_qcwebid_QueryDerivedReferrer_TDataEditListListParameterMap.size() + 1);
            _qcwebid_QueryDerivedReferrer_TDataEditListListParameterMap.put(key, parameterValue); return "Qcwebid_QueryDerivedReferrer_TDataEditListListParameter." + key;
        }

        protected Map<String, TItemInfoCQ> _qcwebid_QueryDerivedReferrer_TItemInfoListMap;
        public Map<String, TItemInfoCQ> Qcwebid_QueryDerivedReferrer_TItemInfoList { get { return _qcwebid_QueryDerivedReferrer_TItemInfoListMap; } }
        public override String keepQcwebid_QueryDerivedReferrer_TItemInfoList(TItemInfoCQ subQuery) {
            if (_qcwebid_QueryDerivedReferrer_TItemInfoListMap == null) { _qcwebid_QueryDerivedReferrer_TItemInfoListMap = new LinkedHashMap<String, TItemInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_QueryDerivedReferrer_TItemInfoListMap.size() + 1);
            _qcwebid_QueryDerivedReferrer_TItemInfoListMap.put(key, subQuery); return "Qcwebid_QueryDerivedReferrer_TItemInfoList." + key;
        }
        protected Map<String, Object> _qcwebid_QueryDerivedReferrer_TItemInfoListParameterMap;
        public Map<String, Object> Qcwebid_QueryDerivedReferrer_TItemInfoListParameter { get { return _qcwebid_QueryDerivedReferrer_TItemInfoListParameterMap; } }
        public override String keepQcwebid_QueryDerivedReferrer_TItemInfoListParameter(Object parameterValue) {
            if (_qcwebid_QueryDerivedReferrer_TItemInfoListParameterMap == null) { _qcwebid_QueryDerivedReferrer_TItemInfoListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_qcwebid_QueryDerivedReferrer_TItemInfoListParameterMap.size() + 1);
            _qcwebid_QueryDerivedReferrer_TItemInfoListParameterMap.put(key, parameterValue); return "Qcwebid_QueryDerivedReferrer_TItemInfoListParameter." + key;
        }

        protected Map<String, TNoticeCQ> _qcwebid_QueryDerivedReferrer_TNoticeListMap;
        public Map<String, TNoticeCQ> Qcwebid_QueryDerivedReferrer_TNoticeList { get { return _qcwebid_QueryDerivedReferrer_TNoticeListMap; } }
        public override String keepQcwebid_QueryDerivedReferrer_TNoticeList(TNoticeCQ subQuery) {
            if (_qcwebid_QueryDerivedReferrer_TNoticeListMap == null) { _qcwebid_QueryDerivedReferrer_TNoticeListMap = new LinkedHashMap<String, TNoticeCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_QueryDerivedReferrer_TNoticeListMap.size() + 1);
            _qcwebid_QueryDerivedReferrer_TNoticeListMap.put(key, subQuery); return "Qcwebid_QueryDerivedReferrer_TNoticeList." + key;
        }
        protected Map<String, Object> _qcwebid_QueryDerivedReferrer_TNoticeListParameterMap;
        public Map<String, Object> Qcwebid_QueryDerivedReferrer_TNoticeListParameter { get { return _qcwebid_QueryDerivedReferrer_TNoticeListParameterMap; } }
        public override String keepQcwebid_QueryDerivedReferrer_TNoticeListParameter(Object parameterValue) {
            if (_qcwebid_QueryDerivedReferrer_TNoticeListParameterMap == null) { _qcwebid_QueryDerivedReferrer_TNoticeListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_qcwebid_QueryDerivedReferrer_TNoticeListParameterMap.size() + 1);
            _qcwebid_QueryDerivedReferrer_TNoticeListParameterMap.put(key, parameterValue); return "Qcwebid_QueryDerivedReferrer_TNoticeListParameter." + key;
        }

        protected Map<String, TOutputRequestCQ> _qcwebid_QueryDerivedReferrer_TOutputRequestListMap;
        public Map<String, TOutputRequestCQ> Qcwebid_QueryDerivedReferrer_TOutputRequestList { get { return _qcwebid_QueryDerivedReferrer_TOutputRequestListMap; } }
        public override String keepQcwebid_QueryDerivedReferrer_TOutputRequestList(TOutputRequestCQ subQuery) {
            if (_qcwebid_QueryDerivedReferrer_TOutputRequestListMap == null) { _qcwebid_QueryDerivedReferrer_TOutputRequestListMap = new LinkedHashMap<String, TOutputRequestCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_QueryDerivedReferrer_TOutputRequestListMap.size() + 1);
            _qcwebid_QueryDerivedReferrer_TOutputRequestListMap.put(key, subQuery); return "Qcwebid_QueryDerivedReferrer_TOutputRequestList." + key;
        }
        protected Map<String, Object> _qcwebid_QueryDerivedReferrer_TOutputRequestListParameterMap;
        public Map<String, Object> Qcwebid_QueryDerivedReferrer_TOutputRequestListParameter { get { return _qcwebid_QueryDerivedReferrer_TOutputRequestListParameterMap; } }
        public override String keepQcwebid_QueryDerivedReferrer_TOutputRequestListParameter(Object parameterValue) {
            if (_qcwebid_QueryDerivedReferrer_TOutputRequestListParameterMap == null) { _qcwebid_QueryDerivedReferrer_TOutputRequestListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_qcwebid_QueryDerivedReferrer_TOutputRequestListParameterMap.size() + 1);
            _qcwebid_QueryDerivedReferrer_TOutputRequestListParameterMap.put(key, parameterValue); return "Qcwebid_QueryDerivedReferrer_TOutputRequestListParameter." + key;
        }

        protected Map<String, TOutputTemplateCQ> _qcwebid_QueryDerivedReferrer_TOutputTemplateListMap;
        public Map<String, TOutputTemplateCQ> Qcwebid_QueryDerivedReferrer_TOutputTemplateList { get { return _qcwebid_QueryDerivedReferrer_TOutputTemplateListMap; } }
        public override String keepQcwebid_QueryDerivedReferrer_TOutputTemplateList(TOutputTemplateCQ subQuery) {
            if (_qcwebid_QueryDerivedReferrer_TOutputTemplateListMap == null) { _qcwebid_QueryDerivedReferrer_TOutputTemplateListMap = new LinkedHashMap<String, TOutputTemplateCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_QueryDerivedReferrer_TOutputTemplateListMap.size() + 1);
            _qcwebid_QueryDerivedReferrer_TOutputTemplateListMap.put(key, subQuery); return "Qcwebid_QueryDerivedReferrer_TOutputTemplateList." + key;
        }
        protected Map<String, Object> _qcwebid_QueryDerivedReferrer_TOutputTemplateListParameterMap;
        public Map<String, Object> Qcwebid_QueryDerivedReferrer_TOutputTemplateListParameter { get { return _qcwebid_QueryDerivedReferrer_TOutputTemplateListParameterMap; } }
        public override String keepQcwebid_QueryDerivedReferrer_TOutputTemplateListParameter(Object parameterValue) {
            if (_qcwebid_QueryDerivedReferrer_TOutputTemplateListParameterMap == null) { _qcwebid_QueryDerivedReferrer_TOutputTemplateListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_qcwebid_QueryDerivedReferrer_TOutputTemplateListParameterMap.size() + 1);
            _qcwebid_QueryDerivedReferrer_TOutputTemplateListParameterMap.put(key, parameterValue); return "Qcwebid_QueryDerivedReferrer_TOutputTemplateListParameter." + key;
        }

        protected Map<String, TQcwebSurveyDetailCQ> _qcwebid_QueryDerivedReferrer_TQcwebSurveyDetailListMap;
        public Map<String, TQcwebSurveyDetailCQ> Qcwebid_QueryDerivedReferrer_TQcwebSurveyDetailList { get { return _qcwebid_QueryDerivedReferrer_TQcwebSurveyDetailListMap; } }
        public override String keepQcwebid_QueryDerivedReferrer_TQcwebSurveyDetailList(TQcwebSurveyDetailCQ subQuery) {
            if (_qcwebid_QueryDerivedReferrer_TQcwebSurveyDetailListMap == null) { _qcwebid_QueryDerivedReferrer_TQcwebSurveyDetailListMap = new LinkedHashMap<String, TQcwebSurveyDetailCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_QueryDerivedReferrer_TQcwebSurveyDetailListMap.size() + 1);
            _qcwebid_QueryDerivedReferrer_TQcwebSurveyDetailListMap.put(key, subQuery); return "Qcwebid_QueryDerivedReferrer_TQcwebSurveyDetailList." + key;
        }
        protected Map<String, Object> _qcwebid_QueryDerivedReferrer_TQcwebSurveyDetailListParameterMap;
        public Map<String, Object> Qcwebid_QueryDerivedReferrer_TQcwebSurveyDetailListParameter { get { return _qcwebid_QueryDerivedReferrer_TQcwebSurveyDetailListParameterMap; } }
        public override String keepQcwebid_QueryDerivedReferrer_TQcwebSurveyDetailListParameter(Object parameterValue) {
            if (_qcwebid_QueryDerivedReferrer_TQcwebSurveyDetailListParameterMap == null) { _qcwebid_QueryDerivedReferrer_TQcwebSurveyDetailListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_qcwebid_QueryDerivedReferrer_TQcwebSurveyDetailListParameterMap.size() + 1);
            _qcwebid_QueryDerivedReferrer_TQcwebSurveyDetailListParameterMap.put(key, parameterValue); return "Qcwebid_QueryDerivedReferrer_TQcwebSurveyDetailListParameter." + key;
        }

        protected Map<String, TRawdataImportQueInfoCQ> _qcwebid_QueryDerivedReferrer_TRawdataImportQueInfoListMap;
        public Map<String, TRawdataImportQueInfoCQ> Qcwebid_QueryDerivedReferrer_TRawdataImportQueInfoList { get { return _qcwebid_QueryDerivedReferrer_TRawdataImportQueInfoListMap; } }
        public override String keepQcwebid_QueryDerivedReferrer_TRawdataImportQueInfoList(TRawdataImportQueInfoCQ subQuery) {
            if (_qcwebid_QueryDerivedReferrer_TRawdataImportQueInfoListMap == null) { _qcwebid_QueryDerivedReferrer_TRawdataImportQueInfoListMap = new LinkedHashMap<String, TRawdataImportQueInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_QueryDerivedReferrer_TRawdataImportQueInfoListMap.size() + 1);
            _qcwebid_QueryDerivedReferrer_TRawdataImportQueInfoListMap.put(key, subQuery); return "Qcwebid_QueryDerivedReferrer_TRawdataImportQueInfoList." + key;
        }
        protected Map<String, Object> _qcwebid_QueryDerivedReferrer_TRawdataImportQueInfoListParameterMap;
        public Map<String, Object> Qcwebid_QueryDerivedReferrer_TRawdataImportQueInfoListParameter { get { return _qcwebid_QueryDerivedReferrer_TRawdataImportQueInfoListParameterMap; } }
        public override String keepQcwebid_QueryDerivedReferrer_TRawdataImportQueInfoListParameter(Object parameterValue) {
            if (_qcwebid_QueryDerivedReferrer_TRawdataImportQueInfoListParameterMap == null) { _qcwebid_QueryDerivedReferrer_TRawdataImportQueInfoListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_qcwebid_QueryDerivedReferrer_TRawdataImportQueInfoListParameterMap.size() + 1);
            _qcwebid_QueryDerivedReferrer_TRawdataImportQueInfoListParameterMap.put(key, parameterValue); return "Qcwebid_QueryDerivedReferrer_TRawdataImportQueInfoListParameter." + key;
        }

        protected Map<String, TReportsetCQ> _qcwebid_QueryDerivedReferrer_TReportsetListMap;
        public Map<String, TReportsetCQ> Qcwebid_QueryDerivedReferrer_TReportsetList { get { return _qcwebid_QueryDerivedReferrer_TReportsetListMap; } }
        public override String keepQcwebid_QueryDerivedReferrer_TReportsetList(TReportsetCQ subQuery) {
            if (_qcwebid_QueryDerivedReferrer_TReportsetListMap == null) { _qcwebid_QueryDerivedReferrer_TReportsetListMap = new LinkedHashMap<String, TReportsetCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_QueryDerivedReferrer_TReportsetListMap.size() + 1);
            _qcwebid_QueryDerivedReferrer_TReportsetListMap.put(key, subQuery); return "Qcwebid_QueryDerivedReferrer_TReportsetList." + key;
        }
        protected Map<String, Object> _qcwebid_QueryDerivedReferrer_TReportsetListParameterMap;
        public Map<String, Object> Qcwebid_QueryDerivedReferrer_TReportsetListParameter { get { return _qcwebid_QueryDerivedReferrer_TReportsetListParameterMap; } }
        public override String keepQcwebid_QueryDerivedReferrer_TReportsetListParameter(Object parameterValue) {
            if (_qcwebid_QueryDerivedReferrer_TReportsetListParameterMap == null) { _qcwebid_QueryDerivedReferrer_TReportsetListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_qcwebid_QueryDerivedReferrer_TReportsetListParameterMap.size() + 1);
            _qcwebid_QueryDerivedReferrer_TReportsetListParameterMap.put(key, parameterValue); return "Qcwebid_QueryDerivedReferrer_TReportsetListParameter." + key;
        }

        protected Map<String, TScenarioTotalizationCQ> _qcwebid_QueryDerivedReferrer_TScenarioTotalizationListMap;
        public Map<String, TScenarioTotalizationCQ> Qcwebid_QueryDerivedReferrer_TScenarioTotalizationList { get { return _qcwebid_QueryDerivedReferrer_TScenarioTotalizationListMap; } }
        public override String keepQcwebid_QueryDerivedReferrer_TScenarioTotalizationList(TScenarioTotalizationCQ subQuery) {
            if (_qcwebid_QueryDerivedReferrer_TScenarioTotalizationListMap == null) { _qcwebid_QueryDerivedReferrer_TScenarioTotalizationListMap = new LinkedHashMap<String, TScenarioTotalizationCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_QueryDerivedReferrer_TScenarioTotalizationListMap.size() + 1);
            _qcwebid_QueryDerivedReferrer_TScenarioTotalizationListMap.put(key, subQuery); return "Qcwebid_QueryDerivedReferrer_TScenarioTotalizationList." + key;
        }
        protected Map<String, Object> _qcwebid_QueryDerivedReferrer_TScenarioTotalizationListParameterMap;
        public Map<String, Object> Qcwebid_QueryDerivedReferrer_TScenarioTotalizationListParameter { get { return _qcwebid_QueryDerivedReferrer_TScenarioTotalizationListParameterMap; } }
        public override String keepQcwebid_QueryDerivedReferrer_TScenarioTotalizationListParameter(Object parameterValue) {
            if (_qcwebid_QueryDerivedReferrer_TScenarioTotalizationListParameterMap == null) { _qcwebid_QueryDerivedReferrer_TScenarioTotalizationListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_qcwebid_QueryDerivedReferrer_TScenarioTotalizationListParameterMap.size() + 1);
            _qcwebid_QueryDerivedReferrer_TScenarioTotalizationListParameterMap.put(key, parameterValue); return "Qcwebid_QueryDerivedReferrer_TScenarioTotalizationListParameter." + key;
        }

        protected Map<String, TSelectConditionInfoCQ> _qcwebid_QueryDerivedReferrer_TSelectConditionInfoListMap;
        public Map<String, TSelectConditionInfoCQ> Qcwebid_QueryDerivedReferrer_TSelectConditionInfoList { get { return _qcwebid_QueryDerivedReferrer_TSelectConditionInfoListMap; } }
        public override String keepQcwebid_QueryDerivedReferrer_TSelectConditionInfoList(TSelectConditionInfoCQ subQuery) {
            if (_qcwebid_QueryDerivedReferrer_TSelectConditionInfoListMap == null) { _qcwebid_QueryDerivedReferrer_TSelectConditionInfoListMap = new LinkedHashMap<String, TSelectConditionInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_QueryDerivedReferrer_TSelectConditionInfoListMap.size() + 1);
            _qcwebid_QueryDerivedReferrer_TSelectConditionInfoListMap.put(key, subQuery); return "Qcwebid_QueryDerivedReferrer_TSelectConditionInfoList." + key;
        }
        protected Map<String, Object> _qcwebid_QueryDerivedReferrer_TSelectConditionInfoListParameterMap;
        public Map<String, Object> Qcwebid_QueryDerivedReferrer_TSelectConditionInfoListParameter { get { return _qcwebid_QueryDerivedReferrer_TSelectConditionInfoListParameterMap; } }
        public override String keepQcwebid_QueryDerivedReferrer_TSelectConditionInfoListParameter(Object parameterValue) {
            if (_qcwebid_QueryDerivedReferrer_TSelectConditionInfoListParameterMap == null) { _qcwebid_QueryDerivedReferrer_TSelectConditionInfoListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_qcwebid_QueryDerivedReferrer_TSelectConditionInfoListParameterMap.size() + 1);
            _qcwebid_QueryDerivedReferrer_TSelectConditionInfoListParameterMap.put(key, parameterValue); return "Qcwebid_QueryDerivedReferrer_TSelectConditionInfoListParameter." + key;
        }

        protected Map<String, TSessionControlerCQ> _qcwebid_QueryDerivedReferrer_TSessionControlerListMap;
        public Map<String, TSessionControlerCQ> Qcwebid_QueryDerivedReferrer_TSessionControlerList { get { return _qcwebid_QueryDerivedReferrer_TSessionControlerListMap; } }
        public override String keepQcwebid_QueryDerivedReferrer_TSessionControlerList(TSessionControlerCQ subQuery) {
            if (_qcwebid_QueryDerivedReferrer_TSessionControlerListMap == null) { _qcwebid_QueryDerivedReferrer_TSessionControlerListMap = new LinkedHashMap<String, TSessionControlerCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_QueryDerivedReferrer_TSessionControlerListMap.size() + 1);
            _qcwebid_QueryDerivedReferrer_TSessionControlerListMap.put(key, subQuery); return "Qcwebid_QueryDerivedReferrer_TSessionControlerList." + key;
        }
        protected Map<String, Object> _qcwebid_QueryDerivedReferrer_TSessionControlerListParameterMap;
        public Map<String, Object> Qcwebid_QueryDerivedReferrer_TSessionControlerListParameter { get { return _qcwebid_QueryDerivedReferrer_TSessionControlerListParameterMap; } }
        public override String keepQcwebid_QueryDerivedReferrer_TSessionControlerListParameter(Object parameterValue) {
            if (_qcwebid_QueryDerivedReferrer_TSessionControlerListParameterMap == null) { _qcwebid_QueryDerivedReferrer_TSessionControlerListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_qcwebid_QueryDerivedReferrer_TSessionControlerListParameterMap.size() + 1);
            _qcwebid_QueryDerivedReferrer_TSessionControlerListParameterMap.put(key, parameterValue); return "Qcwebid_QueryDerivedReferrer_TSessionControlerListParameter." + key;
        }

        protected Map<String, TWeightbackCQ> _qcwebid_QueryDerivedReferrer_TWeightbackListMap;
        public Map<String, TWeightbackCQ> Qcwebid_QueryDerivedReferrer_TWeightbackList { get { return _qcwebid_QueryDerivedReferrer_TWeightbackListMap; } }
        public override String keepQcwebid_QueryDerivedReferrer_TWeightbackList(TWeightbackCQ subQuery) {
            if (_qcwebid_QueryDerivedReferrer_TWeightbackListMap == null) { _qcwebid_QueryDerivedReferrer_TWeightbackListMap = new LinkedHashMap<String, TWeightbackCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_QueryDerivedReferrer_TWeightbackListMap.size() + 1);
            _qcwebid_QueryDerivedReferrer_TWeightbackListMap.put(key, subQuery); return "Qcwebid_QueryDerivedReferrer_TWeightbackList." + key;
        }
        protected Map<String, Object> _qcwebid_QueryDerivedReferrer_TWeightbackListParameterMap;
        public Map<String, Object> Qcwebid_QueryDerivedReferrer_TWeightbackListParameter { get { return _qcwebid_QueryDerivedReferrer_TWeightbackListParameterMap; } }
        public override String keepQcwebid_QueryDerivedReferrer_TWeightbackListParameter(Object parameterValue) {
            if (_qcwebid_QueryDerivedReferrer_TWeightbackListParameterMap == null) { _qcwebid_QueryDerivedReferrer_TWeightbackListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_qcwebid_QueryDerivedReferrer_TWeightbackListParameterMap.size() + 1);
            _qcwebid_QueryDerivedReferrer_TWeightbackListParameterMap.put(key, parameterValue); return "Qcwebid_QueryDerivedReferrer_TWeightbackListParameter." + key;
        }

        public BsTQcwebSurveyInfoCQ AddOrderBy_Qcwebid_Asc() { regOBA("QCWEBID");return this; }
        public BsTQcwebSurveyInfoCQ AddOrderBy_Qcwebid_Desc() { regOBD("QCWEBID");return this; }

        protected ConditionValue _addDataNo;
        public ConditionValue AddDataNo {
            get { if (_addDataNo == null) { _addDataNo = new ConditionValue(); } return _addDataNo; }
        }
        protected override ConditionValue getCValueAddDataNo() { return this.AddDataNo; }


        public BsTQcwebSurveyInfoCQ AddOrderBy_AddDataNo_Asc() { regOBA("ADD_DATA_NO");return this; }
        public BsTQcwebSurveyInfoCQ AddOrderBy_AddDataNo_Desc() { regOBD("ADD_DATA_NO");return this; }

        protected ConditionValue _surveyNameOrg;
        public ConditionValue SurveyNameOrg {
            get { if (_surveyNameOrg == null) { _surveyNameOrg = new ConditionValue(); } return _surveyNameOrg; }
        }
        protected override ConditionValue getCValueSurveyNameOrg() { return this.SurveyNameOrg; }


        public BsTQcwebSurveyInfoCQ AddOrderBy_SurveyNameOrg_Asc() { regOBA("SURVEY_NAME_ORG");return this; }
        public BsTQcwebSurveyInfoCQ AddOrderBy_SurveyNameOrg_Desc() { regOBD("SURVEY_NAME_ORG");return this; }

        protected ConditionValue _importDatetime;
        public ConditionValue ImportDatetime {
            get { if (_importDatetime == null) { _importDatetime = new ConditionValue(); } return _importDatetime; }
        }
        protected override ConditionValue getCValueImportDatetime() { return this.ImportDatetime; }


        public BsTQcwebSurveyInfoCQ AddOrderBy_ImportDatetime_Asc() { regOBA("IMPORT_DATETIME");return this; }
        public BsTQcwebSurveyInfoCQ AddOrderBy_ImportDatetime_Desc() { regOBD("IMPORT_DATETIME");return this; }

        protected ConditionValue _importFileName;
        public ConditionValue ImportFileName {
            get { if (_importFileName == null) { _importFileName = new ConditionValue(); } return _importFileName; }
        }
        protected override ConditionValue getCValueImportFileName() { return this.ImportFileName; }


        public BsTQcwebSurveyInfoCQ AddOrderBy_ImportFileName_Asc() { regOBA("IMPORT_FILE_NAME");return this; }
        public BsTQcwebSurveyInfoCQ AddOrderBy_ImportFileName_Desc() { regOBD("IMPORT_FILE_NAME");return this; }

        protected ConditionValue _deleteFlag;
        public ConditionValue DeleteFlag {
            get { if (_deleteFlag == null) { _deleteFlag = new ConditionValue(); } return _deleteFlag; }
        }
        protected override ConditionValue getCValueDeleteFlag() { return this.DeleteFlag; }


        public BsTQcwebSurveyInfoCQ AddOrderBy_DeleteFlag_Asc() { regOBA("DELETE_FLAG");return this; }
        public BsTQcwebSurveyInfoCQ AddOrderBy_DeleteFlag_Desc() { regOBD("DELETE_FLAG");return this; }

        protected ConditionValue _viewSurveyName;
        public ConditionValue ViewSurveyName {
            get { if (_viewSurveyName == null) { _viewSurveyName = new ConditionValue(); } return _viewSurveyName; }
        }
        protected override ConditionValue getCValueViewSurveyName() { return this.ViewSurveyName; }


        public BsTQcwebSurveyInfoCQ AddOrderBy_ViewSurveyName_Asc() { regOBA("VIEW_SURVEY_NAME");return this; }
        public BsTQcwebSurveyInfoCQ AddOrderBy_ViewSurveyName_Desc() { regOBD("VIEW_SURVEY_NAME");return this; }

        protected ConditionValue _gtCount;
        public ConditionValue GtCount {
            get { if (_gtCount == null) { _gtCount = new ConditionValue(); } return _gtCount; }
        }
        protected override ConditionValue getCValueGtCount() { return this.GtCount; }


        public BsTQcwebSurveyInfoCQ AddOrderBy_GtCount_Asc() { regOBA("GT_COUNT");return this; }
        public BsTQcwebSurveyInfoCQ AddOrderBy_GtCount_Desc() { regOBD("GT_COUNT");return this; }

        protected ConditionValue _crossCount;
        public ConditionValue CrossCount {
            get { if (_crossCount == null) { _crossCount = new ConditionValue(); } return _crossCount; }
        }
        protected override ConditionValue getCValueCrossCount() { return this.CrossCount; }


        public BsTQcwebSurveyInfoCQ AddOrderBy_CrossCount_Asc() { regOBA("CROSS_COUNT");return this; }
        public BsTQcwebSurveyInfoCQ AddOrderBy_CrossCount_Desc() { regOBD("CROSS_COUNT");return this; }

        protected ConditionValue _faCount;
        public ConditionValue FaCount {
            get { if (_faCount == null) { _faCount = new ConditionValue(); } return _faCount; }
        }
        protected override ConditionValue getCValueFaCount() { return this.FaCount; }


        public BsTQcwebSurveyInfoCQ AddOrderBy_FaCount_Asc() { regOBA("FA_COUNT");return this; }
        public BsTQcwebSurveyInfoCQ AddOrderBy_FaCount_Desc() { regOBD("FA_COUNT");return this; }

        protected ConditionValue _versionNo;
        public ConditionValue VersionNo {
            get { if (_versionNo == null) { _versionNo = new ConditionValue(); } return _versionNo; }
        }
        protected override ConditionValue getCValueVersionNo() { return this.VersionNo; }


        public BsTQcwebSurveyInfoCQ AddOrderBy_VersionNo_Asc() { regOBA("VERSION_NO");return this; }
        public BsTQcwebSurveyInfoCQ AddOrderBy_VersionNo_Desc() { regOBD("VERSION_NO");return this; }

        protected ConditionValue _lastUpdateUser;
        public ConditionValue LastUpdateUser {
            get { if (_lastUpdateUser == null) { _lastUpdateUser = new ConditionValue(); } return _lastUpdateUser; }
        }
        protected override ConditionValue getCValueLastUpdateUser() { return this.LastUpdateUser; }


        public BsTQcwebSurveyInfoCQ AddOrderBy_LastUpdateUser_Asc() { regOBA("LAST_UPDATE_USER");return this; }
        public BsTQcwebSurveyInfoCQ AddOrderBy_LastUpdateUser_Desc() { regOBD("LAST_UPDATE_USER");return this; }

        protected ConditionValue _lastUpdateDatetime;
        public ConditionValue LastUpdateDatetime {
            get { if (_lastUpdateDatetime == null) { _lastUpdateDatetime = new ConditionValue(); } return _lastUpdateDatetime; }
        }
        protected override ConditionValue getCValueLastUpdateDatetime() { return this.LastUpdateDatetime; }


        public BsTQcwebSurveyInfoCQ AddOrderBy_LastUpdateDatetime_Asc() { regOBA("LAST_UPDATE_DATETIME");return this; }
        public BsTQcwebSurveyInfoCQ AddOrderBy_LastUpdateDatetime_Desc() { regOBD("LAST_UPDATE_DATETIME");return this; }

        protected ConditionValue _surveyInfoId;
        public ConditionValue SurveyInfoId {
            get { if (_surveyInfoId == null) { _surveyInfoId = new ConditionValue(); } return _surveyInfoId; }
        }
        protected override ConditionValue getCValueSurveyInfoId() { return this.SurveyInfoId; }


        protected Map<String, TSurveyInfoCQ> _surveyInfoId_InScopeSubQuery_TSurveyInfoMap;
        public Map<String, TSurveyInfoCQ> SurveyInfoId_InScopeSubQuery_TSurveyInfo { get { return _surveyInfoId_InScopeSubQuery_TSurveyInfoMap; }}
        public override String keepSurveyInfoId_InScopeSubQuery_TSurveyInfo(TSurveyInfoCQ subQuery) {
            if (_surveyInfoId_InScopeSubQuery_TSurveyInfoMap == null) { _surveyInfoId_InScopeSubQuery_TSurveyInfoMap = new LinkedHashMap<String, TSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_surveyInfoId_InScopeSubQuery_TSurveyInfoMap.size() + 1);
            _surveyInfoId_InScopeSubQuery_TSurveyInfoMap.put(key, subQuery); return "SurveyInfoId_InScopeSubQuery_TSurveyInfo." + key;
        }

        protected Map<String, TSurveyInfoCQ> _surveyInfoId_NotInScopeSubQuery_TSurveyInfoMap;
        public Map<String, TSurveyInfoCQ> SurveyInfoId_NotInScopeSubQuery_TSurveyInfo { get { return _surveyInfoId_NotInScopeSubQuery_TSurveyInfoMap; }}
        public override String keepSurveyInfoId_NotInScopeSubQuery_TSurveyInfo(TSurveyInfoCQ subQuery) {
            if (_surveyInfoId_NotInScopeSubQuery_TSurveyInfoMap == null) { _surveyInfoId_NotInScopeSubQuery_TSurveyInfoMap = new LinkedHashMap<String, TSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_surveyInfoId_NotInScopeSubQuery_TSurveyInfoMap.size() + 1);
            _surveyInfoId_NotInScopeSubQuery_TSurveyInfoMap.put(key, subQuery); return "SurveyInfoId_NotInScopeSubQuery_TSurveyInfo." + key;
        }

        public BsTQcwebSurveyInfoCQ AddOrderBy_SurveyInfoId_Asc() { regOBA("SURVEY_INFO_ID");return this; }
        public BsTQcwebSurveyInfoCQ AddOrderBy_SurveyInfoId_Desc() { regOBD("SURVEY_INFO_ID");return this; }

        protected ConditionValue _rawdataImportQueInfoId;
        public ConditionValue RawdataImportQueInfoId {
            get { if (_rawdataImportQueInfoId == null) { _rawdataImportQueInfoId = new ConditionValue(); } return _rawdataImportQueInfoId; }
        }
        protected override ConditionValue getCValueRawdataImportQueInfoId() { return this.RawdataImportQueInfoId; }


        protected Map<String, TRawdataImportQueInfoCQ> _rawdataImportQueInfoId_InScopeSubQuery_TRawdataImportQueInfoMap;
        public Map<String, TRawdataImportQueInfoCQ> RawdataImportQueInfoId_InScopeSubQuery_TRawdataImportQueInfo { get { return _rawdataImportQueInfoId_InScopeSubQuery_TRawdataImportQueInfoMap; }}
        public override String keepRawdataImportQueInfoId_InScopeSubQuery_TRawdataImportQueInfo(TRawdataImportQueInfoCQ subQuery) {
            if (_rawdataImportQueInfoId_InScopeSubQuery_TRawdataImportQueInfoMap == null) { _rawdataImportQueInfoId_InScopeSubQuery_TRawdataImportQueInfoMap = new LinkedHashMap<String, TRawdataImportQueInfoCQ>(); }
            String key = "subQueryMapKey" + (_rawdataImportQueInfoId_InScopeSubQuery_TRawdataImportQueInfoMap.size() + 1);
            _rawdataImportQueInfoId_InScopeSubQuery_TRawdataImportQueInfoMap.put(key, subQuery); return "RawdataImportQueInfoId_InScopeSubQuery_TRawdataImportQueInfo." + key;
        }

        protected Map<String, TRawdataImportQueInfoCQ> _rawdataImportQueInfoId_NotInScopeSubQuery_TRawdataImportQueInfoMap;
        public Map<String, TRawdataImportQueInfoCQ> RawdataImportQueInfoId_NotInScopeSubQuery_TRawdataImportQueInfo { get { return _rawdataImportQueInfoId_NotInScopeSubQuery_TRawdataImportQueInfoMap; }}
        public override String keepRawdataImportQueInfoId_NotInScopeSubQuery_TRawdataImportQueInfo(TRawdataImportQueInfoCQ subQuery) {
            if (_rawdataImportQueInfoId_NotInScopeSubQuery_TRawdataImportQueInfoMap == null) { _rawdataImportQueInfoId_NotInScopeSubQuery_TRawdataImportQueInfoMap = new LinkedHashMap<String, TRawdataImportQueInfoCQ>(); }
            String key = "subQueryMapKey" + (_rawdataImportQueInfoId_NotInScopeSubQuery_TRawdataImportQueInfoMap.size() + 1);
            _rawdataImportQueInfoId_NotInScopeSubQuery_TRawdataImportQueInfoMap.put(key, subQuery); return "RawdataImportQueInfoId_NotInScopeSubQuery_TRawdataImportQueInfo." + key;
        }

        public BsTQcwebSurveyInfoCQ AddOrderBy_RawdataImportQueInfoId_Asc() { regOBA("RAWDATA_IMPORT_QUE_INFO_ID");return this; }
        public BsTQcwebSurveyInfoCQ AddOrderBy_RawdataImportQueInfoId_Desc() { regOBD("RAWDATA_IMPORT_QUE_INFO_ID");return this; }

        protected ConditionValue _utf8Flag;
        public ConditionValue Utf8Flag {
            get { if (_utf8Flag == null) { _utf8Flag = new ConditionValue(); } return _utf8Flag; }
        }
        protected override ConditionValue getCValueUtf8Flag() { return this.Utf8Flag; }


        public BsTQcwebSurveyInfoCQ AddOrderBy_Utf8Flag_Asc() { regOBA("UTF8_FLAG");return this; }
        public BsTQcwebSurveyInfoCQ AddOrderBy_Utf8Flag_Desc() { regOBD("UTF8_FLAG");return this; }

        public BsTQcwebSurveyInfoCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTQcwebSurveyInfoCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TQcwebSurveyInfoCQ baseQuery = (TQcwebSurveyInfoCQ)baseQueryAsSuper;
            TQcwebSurveyInfoCQ unionQuery = (TQcwebSurveyInfoCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTSurveyInfo()) {
                unionQuery.QueryTSurveyInfo().reflectRelationOnUnionQuery(baseQuery.QueryTSurveyInfo(), unionQuery.QueryTSurveyInfo());
            }
            if (baseQuery.hasConditionQueryTRawdataImportQueInfo()) {
                unionQuery.QueryTRawdataImportQueInfo().reflectRelationOnUnionQuery(baseQuery.QueryTRawdataImportQueInfo(), unionQuery.QueryTRawdataImportQueInfo());
            }
            if (baseQuery.hasConditionQueryTAllocationCellInfo()) {
                unionQuery.QueryTAllocationCellInfo().reflectRelationOnUnionQuery(baseQuery.QueryTAllocationCellInfo(), unionQuery.QueryTAllocationCellInfo());
            }
            if (baseQuery.hasConditionQueryTSelectConditionInfo()) {
                unionQuery.QueryTSelectConditionInfo().reflectRelationOnUnionQuery(baseQuery.QueryTSelectConditionInfo(), unionQuery.QueryTSelectConditionInfo());
            }
            if (baseQuery.hasConditionQueryTItemInfo()) {
                unionQuery.QueryTItemInfo().reflectRelationOnUnionQuery(baseQuery.QueryTItemInfo(), unionQuery.QueryTItemInfo());
            }
            if (baseQuery.hasConditionQueryTTableControl()) {
                unionQuery.QueryTTableControl().reflectRelationOnUnionQuery(baseQuery.QueryTTableControl(), unionQuery.QueryTTableControl());
            }
            if (baseQuery.hasConditionQueryTDefaultEnv()) {
                unionQuery.QueryTDefaultEnv().reflectRelationOnUnionQuery(baseQuery.QueryTDefaultEnv(), unionQuery.QueryTDefaultEnv());
            }
            if (baseQuery.hasConditionQueryTDefaultEnvColorInfo()) {
                unionQuery.QueryTDefaultEnvColorInfo().reflectRelationOnUnionQuery(baseQuery.QueryTDefaultEnvColorInfo(), unionQuery.QueryTDefaultEnvColorInfo());
            }
            if (baseQuery.hasConditionQueryTScenarioTotalization()) {
                unionQuery.QueryTScenarioTotalization().reflectRelationOnUnionQuery(baseQuery.QueryTScenarioTotalization(), unionQuery.QueryTScenarioTotalization());
            }
            if (baseQuery.hasConditionQueryTReportset()) {
                unionQuery.QueryTReportset().reflectRelationOnUnionQuery(baseQuery.QueryTReportset(), unionQuery.QueryTReportset());
            }
            if (baseQuery.hasConditionQueryTDataEditList()) {
                unionQuery.QueryTDataEditList().reflectRelationOnUnionQuery(baseQuery.QueryTDataEditList(), unionQuery.QueryTDataEditList());
            }
            if (baseQuery.hasConditionQueryTOutputSetting()) {
                unionQuery.QueryTOutputSetting().reflectRelationOnUnionQuery(baseQuery.QueryTOutputSetting(), unionQuery.QueryTOutputSetting());
            }
            if (baseQuery.hasConditionQueryTOutputRequest()) {
                unionQuery.QueryTOutputRequest().reflectRelationOnUnionQuery(baseQuery.QueryTOutputRequest(), unionQuery.QueryTOutputRequest());
            }
            if (baseQuery.hasConditionQueryTAccessPermissionsInfo()) {
                unionQuery.QueryTAccessPermissionsInfo().reflectRelationOnUnionQuery(baseQuery.QueryTAccessPermissionsInfo(), unionQuery.QueryTAccessPermissionsInfo());
            }
            if (baseQuery.hasConditionQueryTSessionControler()) {
                unionQuery.QueryTSessionControler().reflectRelationOnUnionQuery(baseQuery.QueryTSessionControler(), unionQuery.QueryTSessionControler());
            }
            if (baseQuery.hasConditionQueryTNotice()) {
                unionQuery.QueryTNotice().reflectRelationOnUnionQuery(baseQuery.QueryTNotice(), unionQuery.QueryTNotice());
            }
            if (baseQuery.hasConditionQueryTOutputSettingGt()) {
                unionQuery.QueryTOutputSettingGt().reflectRelationOnUnionQuery(baseQuery.QueryTOutputSettingGt(), unionQuery.QueryTOutputSettingGt());
            }
            if (baseQuery.hasConditionQueryTOutputSettingCross()) {
                unionQuery.QueryTOutputSettingCross().reflectRelationOnUnionQuery(baseQuery.QueryTOutputSettingCross(), unionQuery.QueryTOutputSettingCross());
            }
            if (baseQuery.hasConditionQueryTOutputSettingFa()) {
                unionQuery.QueryTOutputSettingFa().reflectRelationOnUnionQuery(baseQuery.QueryTOutputSettingFa(), unionQuery.QueryTOutputSettingFa());
            }
            if (baseQuery.hasConditionQueryTOutputSettingReport()) {
                unionQuery.QueryTOutputSettingReport().reflectRelationOnUnionQuery(baseQuery.QueryTOutputSettingReport(), unionQuery.QueryTOutputSettingReport());
            }
            if (baseQuery.hasConditionQueryTQcwebSurveyDetail()) {
                unionQuery.QueryTQcwebSurveyDetail().reflectRelationOnUnionQuery(baseQuery.QueryTQcwebSurveyDetail(), unionQuery.QueryTQcwebSurveyDetail());
            }
            if (baseQuery.hasConditionQueryTAccessPermissionsInfoAsOne()) {
                unionQuery.QueryTAccessPermissionsInfoAsOne().reflectRelationOnUnionQuery(baseQuery.QueryTAccessPermissionsInfoAsOne(), unionQuery.QueryTAccessPermissionsInfoAsOne());
            }
            if (baseQuery.hasConditionQueryTOutputSettingAsOne()) {
                unionQuery.QueryTOutputSettingAsOne().reflectRelationOnUnionQuery(baseQuery.QueryTOutputSettingAsOne(), unionQuery.QueryTOutputSettingAsOne());
            }
            if (baseQuery.hasConditionQueryTOutputSettingCrossAsOne()) {
                unionQuery.QueryTOutputSettingCrossAsOne().reflectRelationOnUnionQuery(baseQuery.QueryTOutputSettingCrossAsOne(), unionQuery.QueryTOutputSettingCrossAsOne());
            }
            if (baseQuery.hasConditionQueryTOutputSettingFaAsOne()) {
                unionQuery.QueryTOutputSettingFaAsOne().reflectRelationOnUnionQuery(baseQuery.QueryTOutputSettingFaAsOne(), unionQuery.QueryTOutputSettingFaAsOne());
            }
            if (baseQuery.hasConditionQueryTOutputSettingGtAsOne()) {
                unionQuery.QueryTOutputSettingGtAsOne().reflectRelationOnUnionQuery(baseQuery.QueryTOutputSettingGtAsOne(), unionQuery.QueryTOutputSettingGtAsOne());
            }
            if (baseQuery.hasConditionQueryTOutputSettingReportAsOne()) {
                unionQuery.QueryTOutputSettingReportAsOne().reflectRelationOnUnionQuery(baseQuery.QueryTOutputSettingReportAsOne(), unionQuery.QueryTOutputSettingReportAsOne());
            }

        }
    
        protected TSurveyInfoCQ _conditionQueryTSurveyInfo;
        public TSurveyInfoCQ QueryTSurveyInfo() {
            return this.ConditionQueryTSurveyInfo;
        }
        public TSurveyInfoCQ ConditionQueryTSurveyInfo {
            get {
                if (_conditionQueryTSurveyInfo == null) {
                    _conditionQueryTSurveyInfo = xcreateQueryTSurveyInfo();
                    xsetupOuterJoin_TSurveyInfo();
                }
                return _conditionQueryTSurveyInfo;
            }
        }
        protected TSurveyInfoCQ xcreateQueryTSurveyInfo() {
            String nrp = resolveNextRelationPathTSurveyInfo();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TSurveyInfoCQ cq = new TSurveyInfoCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tSurveyInfo"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TSurveyInfo() {
            TSurveyInfoCQ cq = ConditionQueryTSurveyInfo;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("SURVEY_INFO_ID", "SURVEY_INFO_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTSurveyInfo() {
            return resolveNextRelationPath("T_QCWEB_SURVEY_INFO", "tSurveyInfo");
        }
        public bool hasConditionQueryTSurveyInfo() {
            return _conditionQueryTSurveyInfo != null;
        }
        protected TRawdataImportQueInfoCQ _conditionQueryTRawdataImportQueInfo;
        public TRawdataImportQueInfoCQ QueryTRawdataImportQueInfo() {
            return this.ConditionQueryTRawdataImportQueInfo;
        }
        public TRawdataImportQueInfoCQ ConditionQueryTRawdataImportQueInfo {
            get {
                if (_conditionQueryTRawdataImportQueInfo == null) {
                    _conditionQueryTRawdataImportQueInfo = xcreateQueryTRawdataImportQueInfo();
                    xsetupOuterJoin_TRawdataImportQueInfo();
                }
                return _conditionQueryTRawdataImportQueInfo;
            }
        }
        protected TRawdataImportQueInfoCQ xcreateQueryTRawdataImportQueInfo() {
            String nrp = resolveNextRelationPathTRawdataImportQueInfo();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TRawdataImportQueInfoCQ cq = new TRawdataImportQueInfoCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tRawdataImportQueInfo"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TRawdataImportQueInfo() {
            TRawdataImportQueInfoCQ cq = ConditionQueryTRawdataImportQueInfo;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("RAWDATA_IMPORT_QUE_INFO_ID", "RAWDATA_IMPORT_QUE_INFO_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTRawdataImportQueInfo() {
            return resolveNextRelationPath("T_QCWEB_SURVEY_INFO", "tRawdataImportQueInfo");
        }
        public bool hasConditionQueryTRawdataImportQueInfo() {
            return _conditionQueryTRawdataImportQueInfo != null;
        }
        protected TAllocationCellInfoCQ _conditionQueryTAllocationCellInfo;
        public TAllocationCellInfoCQ QueryTAllocationCellInfo() {
            return this.ConditionQueryTAllocationCellInfo;
        }
        public TAllocationCellInfoCQ ConditionQueryTAllocationCellInfo {
            get {
                if (_conditionQueryTAllocationCellInfo == null) {
                    _conditionQueryTAllocationCellInfo = xcreateQueryTAllocationCellInfo();
                    xsetupOuterJoin_TAllocationCellInfo();
                }
                return _conditionQueryTAllocationCellInfo;
            }
        }
        protected TAllocationCellInfoCQ xcreateQueryTAllocationCellInfo() {
            String nrp = resolveNextRelationPathTAllocationCellInfo();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TAllocationCellInfoCQ cq = new TAllocationCellInfoCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tAllocationCellInfo"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TAllocationCellInfo() {
            TAllocationCellInfoCQ cq = ConditionQueryTAllocationCellInfo;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("QCWEBID", "QCWebID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTAllocationCellInfo() {
            return resolveNextRelationPath("T_QCWEB_SURVEY_INFO", "tAllocationCellInfo");
        }
        public bool hasConditionQueryTAllocationCellInfo() {
            return _conditionQueryTAllocationCellInfo != null;
        }
        protected TSelectConditionInfoCQ _conditionQueryTSelectConditionInfo;
        public TSelectConditionInfoCQ QueryTSelectConditionInfo() {
            return this.ConditionQueryTSelectConditionInfo;
        }
        public TSelectConditionInfoCQ ConditionQueryTSelectConditionInfo {
            get {
                if (_conditionQueryTSelectConditionInfo == null) {
                    _conditionQueryTSelectConditionInfo = xcreateQueryTSelectConditionInfo();
                    xsetupOuterJoin_TSelectConditionInfo();
                }
                return _conditionQueryTSelectConditionInfo;
            }
        }
        protected TSelectConditionInfoCQ xcreateQueryTSelectConditionInfo() {
            String nrp = resolveNextRelationPathTSelectConditionInfo();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TSelectConditionInfoCQ cq = new TSelectConditionInfoCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tSelectConditionInfo"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TSelectConditionInfo() {
            TSelectConditionInfoCQ cq = ConditionQueryTSelectConditionInfo;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("QCWEBID", "QCWebID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTSelectConditionInfo() {
            return resolveNextRelationPath("T_QCWEB_SURVEY_INFO", "tSelectConditionInfo");
        }
        public bool hasConditionQueryTSelectConditionInfo() {
            return _conditionQueryTSelectConditionInfo != null;
        }
        protected TItemInfoCQ _conditionQueryTItemInfo;
        public TItemInfoCQ QueryTItemInfo() {
            return this.ConditionQueryTItemInfo;
        }
        public TItemInfoCQ ConditionQueryTItemInfo {
            get {
                if (_conditionQueryTItemInfo == null) {
                    _conditionQueryTItemInfo = xcreateQueryTItemInfo();
                    xsetupOuterJoin_TItemInfo();
                }
                return _conditionQueryTItemInfo;
            }
        }
        protected TItemInfoCQ xcreateQueryTItemInfo() {
            String nrp = resolveNextRelationPathTItemInfo();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TItemInfoCQ cq = new TItemInfoCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tItemInfo"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TItemInfo() {
            TItemInfoCQ cq = ConditionQueryTItemInfo;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("QCWEBID", "QCWebID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTItemInfo() {
            return resolveNextRelationPath("T_QCWEB_SURVEY_INFO", "tItemInfo");
        }
        public bool hasConditionQueryTItemInfo() {
            return _conditionQueryTItemInfo != null;
        }
        protected TTableControlCQ _conditionQueryTTableControl;
        public TTableControlCQ QueryTTableControl() {
            return this.ConditionQueryTTableControl;
        }
        public TTableControlCQ ConditionQueryTTableControl {
            get {
                if (_conditionQueryTTableControl == null) {
                    _conditionQueryTTableControl = xcreateQueryTTableControl();
                    xsetupOuterJoin_TTableControl();
                }
                return _conditionQueryTTableControl;
            }
        }
        protected TTableControlCQ xcreateQueryTTableControl() {
            String nrp = resolveNextRelationPathTTableControl();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TTableControlCQ cq = new TTableControlCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tTableControl"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TTableControl() {
            TTableControlCQ cq = ConditionQueryTTableControl;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("QCWEBID", "QCWebID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTTableControl() {
            return resolveNextRelationPath("T_QCWEB_SURVEY_INFO", "tTableControl");
        }
        public bool hasConditionQueryTTableControl() {
            return _conditionQueryTTableControl != null;
        }
        protected TDefaultEnvCQ _conditionQueryTDefaultEnv;
        public TDefaultEnvCQ QueryTDefaultEnv() {
            return this.ConditionQueryTDefaultEnv;
        }
        public TDefaultEnvCQ ConditionQueryTDefaultEnv {
            get {
                if (_conditionQueryTDefaultEnv == null) {
                    _conditionQueryTDefaultEnv = xcreateQueryTDefaultEnv();
                    xsetupOuterJoin_TDefaultEnv();
                }
                return _conditionQueryTDefaultEnv;
            }
        }
        protected TDefaultEnvCQ xcreateQueryTDefaultEnv() {
            String nrp = resolveNextRelationPathTDefaultEnv();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TDefaultEnvCQ cq = new TDefaultEnvCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tDefaultEnv"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TDefaultEnv() {
            TDefaultEnvCQ cq = ConditionQueryTDefaultEnv;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("QCWEBID", "QCWebID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTDefaultEnv() {
            return resolveNextRelationPath("T_QCWEB_SURVEY_INFO", "tDefaultEnv");
        }
        public bool hasConditionQueryTDefaultEnv() {
            return _conditionQueryTDefaultEnv != null;
        }
        protected TDefaultEnvColorInfoCQ _conditionQueryTDefaultEnvColorInfo;
        public TDefaultEnvColorInfoCQ QueryTDefaultEnvColorInfo() {
            return this.ConditionQueryTDefaultEnvColorInfo;
        }
        public TDefaultEnvColorInfoCQ ConditionQueryTDefaultEnvColorInfo {
            get {
                if (_conditionQueryTDefaultEnvColorInfo == null) {
                    _conditionQueryTDefaultEnvColorInfo = xcreateQueryTDefaultEnvColorInfo();
                    xsetupOuterJoin_TDefaultEnvColorInfo();
                }
                return _conditionQueryTDefaultEnvColorInfo;
            }
        }
        protected TDefaultEnvColorInfoCQ xcreateQueryTDefaultEnvColorInfo() {
            String nrp = resolveNextRelationPathTDefaultEnvColorInfo();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TDefaultEnvColorInfoCQ cq = new TDefaultEnvColorInfoCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tDefaultEnvColorInfo"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TDefaultEnvColorInfo() {
            TDefaultEnvColorInfoCQ cq = ConditionQueryTDefaultEnvColorInfo;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("QCWEBID", "QCWebID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTDefaultEnvColorInfo() {
            return resolveNextRelationPath("T_QCWEB_SURVEY_INFO", "tDefaultEnvColorInfo");
        }
        public bool hasConditionQueryTDefaultEnvColorInfo() {
            return _conditionQueryTDefaultEnvColorInfo != null;
        }
        protected TScenarioTotalizationCQ _conditionQueryTScenarioTotalization;
        public TScenarioTotalizationCQ QueryTScenarioTotalization() {
            return this.ConditionQueryTScenarioTotalization;
        }
        public TScenarioTotalizationCQ ConditionQueryTScenarioTotalization {
            get {
                if (_conditionQueryTScenarioTotalization == null) {
                    _conditionQueryTScenarioTotalization = xcreateQueryTScenarioTotalization();
                    xsetupOuterJoin_TScenarioTotalization();
                }
                return _conditionQueryTScenarioTotalization;
            }
        }
        protected TScenarioTotalizationCQ xcreateQueryTScenarioTotalization() {
            String nrp = resolveNextRelationPathTScenarioTotalization();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TScenarioTotalizationCQ cq = new TScenarioTotalizationCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tScenarioTotalization"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TScenarioTotalization() {
            TScenarioTotalizationCQ cq = ConditionQueryTScenarioTotalization;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("QCWEBID", "QCWebID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTScenarioTotalization() {
            return resolveNextRelationPath("T_QCWEB_SURVEY_INFO", "tScenarioTotalization");
        }
        public bool hasConditionQueryTScenarioTotalization() {
            return _conditionQueryTScenarioTotalization != null;
        }
        protected TReportsetCQ _conditionQueryTReportset;
        public TReportsetCQ QueryTReportset() {
            return this.ConditionQueryTReportset;
        }
        public TReportsetCQ ConditionQueryTReportset {
            get {
                if (_conditionQueryTReportset == null) {
                    _conditionQueryTReportset = xcreateQueryTReportset();
                    xsetupOuterJoin_TReportset();
                }
                return _conditionQueryTReportset;
            }
        }
        protected TReportsetCQ xcreateQueryTReportset() {
            String nrp = resolveNextRelationPathTReportset();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TReportsetCQ cq = new TReportsetCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tReportset"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TReportset() {
            TReportsetCQ cq = ConditionQueryTReportset;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("QCWEBID", "QCWebID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTReportset() {
            return resolveNextRelationPath("T_QCWEB_SURVEY_INFO", "tReportset");
        }
        public bool hasConditionQueryTReportset() {
            return _conditionQueryTReportset != null;
        }
        protected TDataEditListCQ _conditionQueryTDataEditList;
        public TDataEditListCQ QueryTDataEditList() {
            return this.ConditionQueryTDataEditList;
        }
        public TDataEditListCQ ConditionQueryTDataEditList {
            get {
                if (_conditionQueryTDataEditList == null) {
                    _conditionQueryTDataEditList = xcreateQueryTDataEditList();
                    xsetupOuterJoin_TDataEditList();
                }
                return _conditionQueryTDataEditList;
            }
        }
        protected TDataEditListCQ xcreateQueryTDataEditList() {
            String nrp = resolveNextRelationPathTDataEditList();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TDataEditListCQ cq = new TDataEditListCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tDataEditList"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TDataEditList() {
            TDataEditListCQ cq = ConditionQueryTDataEditList;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("QCWEBID", "QCWebID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTDataEditList() {
            return resolveNextRelationPath("T_QCWEB_SURVEY_INFO", "tDataEditList");
        }
        public bool hasConditionQueryTDataEditList() {
            return _conditionQueryTDataEditList != null;
        }
        protected TOutputSettingCQ _conditionQueryTOutputSetting;
        public TOutputSettingCQ QueryTOutputSetting() {
            return this.ConditionQueryTOutputSetting;
        }
        public TOutputSettingCQ ConditionQueryTOutputSetting {
            get {
                if (_conditionQueryTOutputSetting == null) {
                    _conditionQueryTOutputSetting = xcreateQueryTOutputSetting();
                    xsetupOuterJoin_TOutputSetting();
                }
                return _conditionQueryTOutputSetting;
            }
        }
        protected TOutputSettingCQ xcreateQueryTOutputSetting() {
            String nrp = resolveNextRelationPathTOutputSetting();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TOutputSettingCQ cq = new TOutputSettingCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tOutputSetting"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TOutputSetting() {
            TOutputSettingCQ cq = ConditionQueryTOutputSetting;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("QCWEBID", "QCWebID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTOutputSetting() {
            return resolveNextRelationPath("T_QCWEB_SURVEY_INFO", "tOutputSetting");
        }
        public bool hasConditionQueryTOutputSetting() {
            return _conditionQueryTOutputSetting != null;
        }
        protected TOutputRequestCQ _conditionQueryTOutputRequest;
        public TOutputRequestCQ QueryTOutputRequest() {
            return this.ConditionQueryTOutputRequest;
        }
        public TOutputRequestCQ ConditionQueryTOutputRequest {
            get {
                if (_conditionQueryTOutputRequest == null) {
                    _conditionQueryTOutputRequest = xcreateQueryTOutputRequest();
                    xsetupOuterJoin_TOutputRequest();
                }
                return _conditionQueryTOutputRequest;
            }
        }
        protected TOutputRequestCQ xcreateQueryTOutputRequest() {
            String nrp = resolveNextRelationPathTOutputRequest();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TOutputRequestCQ cq = new TOutputRequestCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tOutputRequest"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TOutputRequest() {
            TOutputRequestCQ cq = ConditionQueryTOutputRequest;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("QCWEBID", "QCWebID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTOutputRequest() {
            return resolveNextRelationPath("T_QCWEB_SURVEY_INFO", "tOutputRequest");
        }
        public bool hasConditionQueryTOutputRequest() {
            return _conditionQueryTOutputRequest != null;
        }
        protected TAccessPermissionsInfoCQ _conditionQueryTAccessPermissionsInfo;
        public TAccessPermissionsInfoCQ QueryTAccessPermissionsInfo() {
            return this.ConditionQueryTAccessPermissionsInfo;
        }
        public TAccessPermissionsInfoCQ ConditionQueryTAccessPermissionsInfo {
            get {
                if (_conditionQueryTAccessPermissionsInfo == null) {
                    _conditionQueryTAccessPermissionsInfo = xcreateQueryTAccessPermissionsInfo();
                    xsetupOuterJoin_TAccessPermissionsInfo();
                }
                return _conditionQueryTAccessPermissionsInfo;
            }
        }
        protected TAccessPermissionsInfoCQ xcreateQueryTAccessPermissionsInfo() {
            String nrp = resolveNextRelationPathTAccessPermissionsInfo();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TAccessPermissionsInfoCQ cq = new TAccessPermissionsInfoCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tAccessPermissionsInfo"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TAccessPermissionsInfo() {
            TAccessPermissionsInfoCQ cq = ConditionQueryTAccessPermissionsInfo;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("QCWEBID", "QCWebID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTAccessPermissionsInfo() {
            return resolveNextRelationPath("T_QCWEB_SURVEY_INFO", "tAccessPermissionsInfo");
        }
        public bool hasConditionQueryTAccessPermissionsInfo() {
            return _conditionQueryTAccessPermissionsInfo != null;
        }
        protected TSessionControlerCQ _conditionQueryTSessionControler;
        public TSessionControlerCQ QueryTSessionControler() {
            return this.ConditionQueryTSessionControler;
        }
        public TSessionControlerCQ ConditionQueryTSessionControler {
            get {
                if (_conditionQueryTSessionControler == null) {
                    _conditionQueryTSessionControler = xcreateQueryTSessionControler();
                    xsetupOuterJoin_TSessionControler();
                }
                return _conditionQueryTSessionControler;
            }
        }
        protected TSessionControlerCQ xcreateQueryTSessionControler() {
            String nrp = resolveNextRelationPathTSessionControler();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TSessionControlerCQ cq = new TSessionControlerCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tSessionControler"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TSessionControler() {
            TSessionControlerCQ cq = ConditionQueryTSessionControler;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("QCWEBID", "QCWebID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTSessionControler() {
            return resolveNextRelationPath("T_QCWEB_SURVEY_INFO", "tSessionControler");
        }
        public bool hasConditionQueryTSessionControler() {
            return _conditionQueryTSessionControler != null;
        }
        protected TNoticeCQ _conditionQueryTNotice;
        public TNoticeCQ QueryTNotice() {
            return this.ConditionQueryTNotice;
        }
        public TNoticeCQ ConditionQueryTNotice {
            get {
                if (_conditionQueryTNotice == null) {
                    _conditionQueryTNotice = xcreateQueryTNotice();
                    xsetupOuterJoin_TNotice();
                }
                return _conditionQueryTNotice;
            }
        }
        protected TNoticeCQ xcreateQueryTNotice() {
            String nrp = resolveNextRelationPathTNotice();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TNoticeCQ cq = new TNoticeCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tNotice"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TNotice() {
            TNoticeCQ cq = ConditionQueryTNotice;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("QCWEBID", "QCWebID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTNotice() {
            return resolveNextRelationPath("T_QCWEB_SURVEY_INFO", "tNotice");
        }
        public bool hasConditionQueryTNotice() {
            return _conditionQueryTNotice != null;
        }
        protected TOutputSettingGtCQ _conditionQueryTOutputSettingGt;
        public TOutputSettingGtCQ QueryTOutputSettingGt() {
            return this.ConditionQueryTOutputSettingGt;
        }
        public TOutputSettingGtCQ ConditionQueryTOutputSettingGt {
            get {
                if (_conditionQueryTOutputSettingGt == null) {
                    _conditionQueryTOutputSettingGt = xcreateQueryTOutputSettingGt();
                    xsetupOuterJoin_TOutputSettingGt();
                }
                return _conditionQueryTOutputSettingGt;
            }
        }
        protected TOutputSettingGtCQ xcreateQueryTOutputSettingGt() {
            String nrp = resolveNextRelationPathTOutputSettingGt();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TOutputSettingGtCQ cq = new TOutputSettingGtCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tOutputSettingGt"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TOutputSettingGt() {
            TOutputSettingGtCQ cq = ConditionQueryTOutputSettingGt;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("QCWEBID", "QCWebID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTOutputSettingGt() {
            return resolveNextRelationPath("T_QCWEB_SURVEY_INFO", "tOutputSettingGt");
        }
        public bool hasConditionQueryTOutputSettingGt() {
            return _conditionQueryTOutputSettingGt != null;
        }
        protected TOutputSettingCrossCQ _conditionQueryTOutputSettingCross;
        public TOutputSettingCrossCQ QueryTOutputSettingCross() {
            return this.ConditionQueryTOutputSettingCross;
        }
        public TOutputSettingCrossCQ ConditionQueryTOutputSettingCross {
            get {
                if (_conditionQueryTOutputSettingCross == null) {
                    _conditionQueryTOutputSettingCross = xcreateQueryTOutputSettingCross();
                    xsetupOuterJoin_TOutputSettingCross();
                }
                return _conditionQueryTOutputSettingCross;
            }
        }
        protected TOutputSettingCrossCQ xcreateQueryTOutputSettingCross() {
            String nrp = resolveNextRelationPathTOutputSettingCross();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TOutputSettingCrossCQ cq = new TOutputSettingCrossCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tOutputSettingCross"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TOutputSettingCross() {
            TOutputSettingCrossCQ cq = ConditionQueryTOutputSettingCross;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("QCWEBID", "QCWebID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTOutputSettingCross() {
            return resolveNextRelationPath("T_QCWEB_SURVEY_INFO", "tOutputSettingCross");
        }
        public bool hasConditionQueryTOutputSettingCross() {
            return _conditionQueryTOutputSettingCross != null;
        }
        protected TOutputSettingFaCQ _conditionQueryTOutputSettingFa;
        public TOutputSettingFaCQ QueryTOutputSettingFa() {
            return this.ConditionQueryTOutputSettingFa;
        }
        public TOutputSettingFaCQ ConditionQueryTOutputSettingFa {
            get {
                if (_conditionQueryTOutputSettingFa == null) {
                    _conditionQueryTOutputSettingFa = xcreateQueryTOutputSettingFa();
                    xsetupOuterJoin_TOutputSettingFa();
                }
                return _conditionQueryTOutputSettingFa;
            }
        }
        protected TOutputSettingFaCQ xcreateQueryTOutputSettingFa() {
            String nrp = resolveNextRelationPathTOutputSettingFa();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TOutputSettingFaCQ cq = new TOutputSettingFaCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tOutputSettingFa"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TOutputSettingFa() {
            TOutputSettingFaCQ cq = ConditionQueryTOutputSettingFa;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("QCWEBID", "QCWebID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTOutputSettingFa() {
            return resolveNextRelationPath("T_QCWEB_SURVEY_INFO", "tOutputSettingFa");
        }
        public bool hasConditionQueryTOutputSettingFa() {
            return _conditionQueryTOutputSettingFa != null;
        }
        protected TOutputSettingReportCQ _conditionQueryTOutputSettingReport;
        public TOutputSettingReportCQ QueryTOutputSettingReport() {
            return this.ConditionQueryTOutputSettingReport;
        }
        public TOutputSettingReportCQ ConditionQueryTOutputSettingReport {
            get {
                if (_conditionQueryTOutputSettingReport == null) {
                    _conditionQueryTOutputSettingReport = xcreateQueryTOutputSettingReport();
                    xsetupOuterJoin_TOutputSettingReport();
                }
                return _conditionQueryTOutputSettingReport;
            }
        }
        protected TOutputSettingReportCQ xcreateQueryTOutputSettingReport() {
            String nrp = resolveNextRelationPathTOutputSettingReport();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TOutputSettingReportCQ cq = new TOutputSettingReportCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tOutputSettingReport"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TOutputSettingReport() {
            TOutputSettingReportCQ cq = ConditionQueryTOutputSettingReport;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("QCWEBID", "QCWebID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTOutputSettingReport() {
            return resolveNextRelationPath("T_QCWEB_SURVEY_INFO", "tOutputSettingReport");
        }
        public bool hasConditionQueryTOutputSettingReport() {
            return _conditionQueryTOutputSettingReport != null;
        }
        protected TQcwebSurveyDetailCQ _conditionQueryTQcwebSurveyDetail;
        public TQcwebSurveyDetailCQ QueryTQcwebSurveyDetail() {
            return this.ConditionQueryTQcwebSurveyDetail;
        }
        public TQcwebSurveyDetailCQ ConditionQueryTQcwebSurveyDetail {
            get {
                if (_conditionQueryTQcwebSurveyDetail == null) {
                    _conditionQueryTQcwebSurveyDetail = xcreateQueryTQcwebSurveyDetail();
                    xsetupOuterJoin_TQcwebSurveyDetail();
                }
                return _conditionQueryTQcwebSurveyDetail;
            }
        }
        protected TQcwebSurveyDetailCQ xcreateQueryTQcwebSurveyDetail() {
            String nrp = resolveNextRelationPathTQcwebSurveyDetail();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TQcwebSurveyDetailCQ cq = new TQcwebSurveyDetailCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tQcwebSurveyDetail"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TQcwebSurveyDetail() {
            TQcwebSurveyDetailCQ cq = ConditionQueryTQcwebSurveyDetail;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("QCWEBID", "QCWebID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTQcwebSurveyDetail() {
            return resolveNextRelationPath("T_QCWEB_SURVEY_INFO", "tQcwebSurveyDetail");
        }
        public bool hasConditionQueryTQcwebSurveyDetail() {
            return _conditionQueryTQcwebSurveyDetail != null;
        }


        protected TAccessPermissionsInfoCQ _conditionQueryTAccessPermissionsInfoAsOne;
        public TAccessPermissionsInfoCQ ConditionQueryTAccessPermissionsInfoAsOne {
            get {
                if (_conditionQueryTAccessPermissionsInfoAsOne == null) {
                    _conditionQueryTAccessPermissionsInfoAsOne = createQueryTAccessPermissionsInfoAsOne();
                    xsetupOuterJoin_TAccessPermissionsInfoAsOne();
                }
                return _conditionQueryTAccessPermissionsInfoAsOne;
            }
        }
        public TAccessPermissionsInfoCQ QueryTAccessPermissionsInfoAsOne() { return this.ConditionQueryTAccessPermissionsInfoAsOne; }
        protected TAccessPermissionsInfoCQ createQueryTAccessPermissionsInfoAsOne() {
            String nrp = resolveNextRelationPathTAccessPermissionsInfoAsOne();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TAccessPermissionsInfoCQ cq = new TAccessPermissionsInfoCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tAccessPermissionsInfoAsOne"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TAccessPermissionsInfoAsOne() {
            TAccessPermissionsInfoCQ cq = ConditionQueryTAccessPermissionsInfoAsOne;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("QCWEBID", "QCWEBID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTAccessPermissionsInfoAsOne() {
            return resolveNextRelationPath("T_QCWEB_SURVEY_INFO", "tAccessPermissionsInfoAsOne");
        }
        public bool hasConditionQueryTAccessPermissionsInfoAsOne() {
            return _conditionQueryTAccessPermissionsInfoAsOne != null;
        }

        protected TOutputSettingCQ _conditionQueryTOutputSettingAsOne;
        public TOutputSettingCQ ConditionQueryTOutputSettingAsOne {
            get {
                if (_conditionQueryTOutputSettingAsOne == null) {
                    _conditionQueryTOutputSettingAsOne = createQueryTOutputSettingAsOne();
                    xsetupOuterJoin_TOutputSettingAsOne();
                }
                return _conditionQueryTOutputSettingAsOne;
            }
        }
        public TOutputSettingCQ QueryTOutputSettingAsOne() { return this.ConditionQueryTOutputSettingAsOne; }
        protected TOutputSettingCQ createQueryTOutputSettingAsOne() {
            String nrp = resolveNextRelationPathTOutputSettingAsOne();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TOutputSettingCQ cq = new TOutputSettingCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tOutputSettingAsOne"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TOutputSettingAsOne() {
            TOutputSettingCQ cq = ConditionQueryTOutputSettingAsOne;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("QCWEBID", "QCWEBID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTOutputSettingAsOne() {
            return resolveNextRelationPath("T_QCWEB_SURVEY_INFO", "tOutputSettingAsOne");
        }
        public bool hasConditionQueryTOutputSettingAsOne() {
            return _conditionQueryTOutputSettingAsOne != null;
        }

        protected TOutputSettingCrossCQ _conditionQueryTOutputSettingCrossAsOne;
        public TOutputSettingCrossCQ ConditionQueryTOutputSettingCrossAsOne {
            get {
                if (_conditionQueryTOutputSettingCrossAsOne == null) {
                    _conditionQueryTOutputSettingCrossAsOne = createQueryTOutputSettingCrossAsOne();
                    xsetupOuterJoin_TOutputSettingCrossAsOne();
                }
                return _conditionQueryTOutputSettingCrossAsOne;
            }
        }
        public TOutputSettingCrossCQ QueryTOutputSettingCrossAsOne() { return this.ConditionQueryTOutputSettingCrossAsOne; }
        protected TOutputSettingCrossCQ createQueryTOutputSettingCrossAsOne() {
            String nrp = resolveNextRelationPathTOutputSettingCrossAsOne();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TOutputSettingCrossCQ cq = new TOutputSettingCrossCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tOutputSettingCrossAsOne"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TOutputSettingCrossAsOne() {
            TOutputSettingCrossCQ cq = ConditionQueryTOutputSettingCrossAsOne;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("QCWEBID", "QCWEBID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTOutputSettingCrossAsOne() {
            return resolveNextRelationPath("T_QCWEB_SURVEY_INFO", "tOutputSettingCrossAsOne");
        }
        public bool hasConditionQueryTOutputSettingCrossAsOne() {
            return _conditionQueryTOutputSettingCrossAsOne != null;
        }

        protected TOutputSettingFaCQ _conditionQueryTOutputSettingFaAsOne;
        public TOutputSettingFaCQ ConditionQueryTOutputSettingFaAsOne {
            get {
                if (_conditionQueryTOutputSettingFaAsOne == null) {
                    _conditionQueryTOutputSettingFaAsOne = createQueryTOutputSettingFaAsOne();
                    xsetupOuterJoin_TOutputSettingFaAsOne();
                }
                return _conditionQueryTOutputSettingFaAsOne;
            }
        }
        public TOutputSettingFaCQ QueryTOutputSettingFaAsOne() { return this.ConditionQueryTOutputSettingFaAsOne; }
        protected TOutputSettingFaCQ createQueryTOutputSettingFaAsOne() {
            String nrp = resolveNextRelationPathTOutputSettingFaAsOne();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TOutputSettingFaCQ cq = new TOutputSettingFaCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tOutputSettingFaAsOne"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TOutputSettingFaAsOne() {
            TOutputSettingFaCQ cq = ConditionQueryTOutputSettingFaAsOne;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("QCWEBID", "QCWEBID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTOutputSettingFaAsOne() {
            return resolveNextRelationPath("T_QCWEB_SURVEY_INFO", "tOutputSettingFaAsOne");
        }
        public bool hasConditionQueryTOutputSettingFaAsOne() {
            return _conditionQueryTOutputSettingFaAsOne != null;
        }

        protected TOutputSettingGtCQ _conditionQueryTOutputSettingGtAsOne;
        public TOutputSettingGtCQ ConditionQueryTOutputSettingGtAsOne {
            get {
                if (_conditionQueryTOutputSettingGtAsOne == null) {
                    _conditionQueryTOutputSettingGtAsOne = createQueryTOutputSettingGtAsOne();
                    xsetupOuterJoin_TOutputSettingGtAsOne();
                }
                return _conditionQueryTOutputSettingGtAsOne;
            }
        }
        public TOutputSettingGtCQ QueryTOutputSettingGtAsOne() { return this.ConditionQueryTOutputSettingGtAsOne; }
        protected TOutputSettingGtCQ createQueryTOutputSettingGtAsOne() {
            String nrp = resolveNextRelationPathTOutputSettingGtAsOne();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TOutputSettingGtCQ cq = new TOutputSettingGtCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tOutputSettingGtAsOne"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TOutputSettingGtAsOne() {
            TOutputSettingGtCQ cq = ConditionQueryTOutputSettingGtAsOne;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("QCWEBID", "QCWEBID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTOutputSettingGtAsOne() {
            return resolveNextRelationPath("T_QCWEB_SURVEY_INFO", "tOutputSettingGtAsOne");
        }
        public bool hasConditionQueryTOutputSettingGtAsOne() {
            return _conditionQueryTOutputSettingGtAsOne != null;
        }

        protected TOutputSettingReportCQ _conditionQueryTOutputSettingReportAsOne;
        public TOutputSettingReportCQ ConditionQueryTOutputSettingReportAsOne {
            get {
                if (_conditionQueryTOutputSettingReportAsOne == null) {
                    _conditionQueryTOutputSettingReportAsOne = createQueryTOutputSettingReportAsOne();
                    xsetupOuterJoin_TOutputSettingReportAsOne();
                }
                return _conditionQueryTOutputSettingReportAsOne;
            }
        }
        public TOutputSettingReportCQ QueryTOutputSettingReportAsOne() { return this.ConditionQueryTOutputSettingReportAsOne; }
        protected TOutputSettingReportCQ createQueryTOutputSettingReportAsOne() {
            String nrp = resolveNextRelationPathTOutputSettingReportAsOne();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TOutputSettingReportCQ cq = new TOutputSettingReportCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tOutputSettingReportAsOne"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TOutputSettingReportAsOne() {
            TOutputSettingReportCQ cq = ConditionQueryTOutputSettingReportAsOne;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("QCWEBID", "QCWEBID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTOutputSettingReportAsOne() {
            return resolveNextRelationPath("T_QCWEB_SURVEY_INFO", "tOutputSettingReportAsOne");
        }
        public bool hasConditionQueryTOutputSettingReportAsOne() {
            return _conditionQueryTOutputSettingReportAsOne != null;
        }

	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TQcwebSurveyInfoCQ> _scalarSubQueryMap;
	    public Map<String, TQcwebSurveyInfoCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TQcwebSurveyInfoCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TQcwebSurveyInfoCQ> _myselfInScopeSubQueryMap;
        public Map<String, TQcwebSurveyInfoCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TQcwebSurveyInfoCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
