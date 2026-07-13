
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTDataEditListCQ : AbstractBsTDataEditListCQ {

        protected TDataEditListCIQ _inlineQuery;

        public BsTDataEditListCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TDataEditListCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TDataEditListCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TDataEditListCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TDataEditListCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _dataEditId;
        public ConditionValue DataEditId {
            get { if (_dataEditId == null) { _dataEditId = new ConditionValue(); } return _dataEditId; }
        }
        protected override ConditionValue getCValueDataEditId() { return this.DataEditId; }


        protected Map<String, TDataProcessNewItemCQ> _dataEditId_ExistsSubQuery_TDataProcessNewItemAsOneMap;
        public Map<String, TDataProcessNewItemCQ> DataEditId_ExistsSubQuery_TDataProcessNewItemAsOne { get { return _dataEditId_ExistsSubQuery_TDataProcessNewItemAsOneMap; }}
        public override String keepDataEditId_ExistsSubQuery_TDataProcessNewItemAsOne(TDataProcessNewItemCQ subQuery) {
            if (_dataEditId_ExistsSubQuery_TDataProcessNewItemAsOneMap == null) { _dataEditId_ExistsSubQuery_TDataProcessNewItemAsOneMap = new LinkedHashMap<String, TDataProcessNewItemCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_ExistsSubQuery_TDataProcessNewItemAsOneMap.size() + 1);
            _dataEditId_ExistsSubQuery_TDataProcessNewItemAsOneMap.put(key, subQuery); return "DataEditId_ExistsSubQuery_TDataProcessNewItemAsOne." + key;
        }

        protected Map<String, TDeleteDataCQ> _dataEditId_ExistsSubQuery_TDeleteDataAsOneMap;
        public Map<String, TDeleteDataCQ> DataEditId_ExistsSubQuery_TDeleteDataAsOne { get { return _dataEditId_ExistsSubQuery_TDeleteDataAsOneMap; }}
        public override String keepDataEditId_ExistsSubQuery_TDeleteDataAsOne(TDeleteDataCQ subQuery) {
            if (_dataEditId_ExistsSubQuery_TDeleteDataAsOneMap == null) { _dataEditId_ExistsSubQuery_TDeleteDataAsOneMap = new LinkedHashMap<String, TDeleteDataCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_ExistsSubQuery_TDeleteDataAsOneMap.size() + 1);
            _dataEditId_ExistsSubQuery_TDeleteDataAsOneMap.put(key, subQuery); return "DataEditId_ExistsSubQuery_TDeleteDataAsOne." + key;
        }

        protected Map<String, TEditDataCQ> _dataEditId_ExistsSubQuery_TEditDataAsOneMap;
        public Map<String, TEditDataCQ> DataEditId_ExistsSubQuery_TEditDataAsOne { get { return _dataEditId_ExistsSubQuery_TEditDataAsOneMap; }}
        public override String keepDataEditId_ExistsSubQuery_TEditDataAsOne(TEditDataCQ subQuery) {
            if (_dataEditId_ExistsSubQuery_TEditDataAsOneMap == null) { _dataEditId_ExistsSubQuery_TEditDataAsOneMap = new LinkedHashMap<String, TEditDataCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_ExistsSubQuery_TEditDataAsOneMap.size() + 1);
            _dataEditId_ExistsSubQuery_TEditDataAsOneMap.put(key, subQuery); return "DataEditId_ExistsSubQuery_TEditDataAsOne." + key;
        }

        protected Map<String, TItemInfoCQ> _dataEditId_ExistsSubQuery_TItemInfoListMap;
        public Map<String, TItemInfoCQ> DataEditId_ExistsSubQuery_TItemInfoList { get { return _dataEditId_ExistsSubQuery_TItemInfoListMap; }}
        public override String keepDataEditId_ExistsSubQuery_TItemInfoList(TItemInfoCQ subQuery) {
            if (_dataEditId_ExistsSubQuery_TItemInfoListMap == null) { _dataEditId_ExistsSubQuery_TItemInfoListMap = new LinkedHashMap<String, TItemInfoCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_ExistsSubQuery_TItemInfoListMap.size() + 1);
            _dataEditId_ExistsSubQuery_TItemInfoListMap.put(key, subQuery); return "DataEditId_ExistsSubQuery_TItemInfoList." + key;
        }

        protected Map<String, TDataProcessNewItemCQ> _dataEditId_NotExistsSubQuery_TDataProcessNewItemAsOneMap;
        public Map<String, TDataProcessNewItemCQ> DataEditId_NotExistsSubQuery_TDataProcessNewItemAsOne { get { return _dataEditId_NotExistsSubQuery_TDataProcessNewItemAsOneMap; }}
        public override String keepDataEditId_NotExistsSubQuery_TDataProcessNewItemAsOne(TDataProcessNewItemCQ subQuery) {
            if (_dataEditId_NotExistsSubQuery_TDataProcessNewItemAsOneMap == null) { _dataEditId_NotExistsSubQuery_TDataProcessNewItemAsOneMap = new LinkedHashMap<String, TDataProcessNewItemCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_NotExistsSubQuery_TDataProcessNewItemAsOneMap.size() + 1);
            _dataEditId_NotExistsSubQuery_TDataProcessNewItemAsOneMap.put(key, subQuery); return "DataEditId_NotExistsSubQuery_TDataProcessNewItemAsOne." + key;
        }

        protected Map<String, TDeleteDataCQ> _dataEditId_NotExistsSubQuery_TDeleteDataAsOneMap;
        public Map<String, TDeleteDataCQ> DataEditId_NotExistsSubQuery_TDeleteDataAsOne { get { return _dataEditId_NotExistsSubQuery_TDeleteDataAsOneMap; }}
        public override String keepDataEditId_NotExistsSubQuery_TDeleteDataAsOne(TDeleteDataCQ subQuery) {
            if (_dataEditId_NotExistsSubQuery_TDeleteDataAsOneMap == null) { _dataEditId_NotExistsSubQuery_TDeleteDataAsOneMap = new LinkedHashMap<String, TDeleteDataCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_NotExistsSubQuery_TDeleteDataAsOneMap.size() + 1);
            _dataEditId_NotExistsSubQuery_TDeleteDataAsOneMap.put(key, subQuery); return "DataEditId_NotExistsSubQuery_TDeleteDataAsOne." + key;
        }

        protected Map<String, TEditDataCQ> _dataEditId_NotExistsSubQuery_TEditDataAsOneMap;
        public Map<String, TEditDataCQ> DataEditId_NotExistsSubQuery_TEditDataAsOne { get { return _dataEditId_NotExistsSubQuery_TEditDataAsOneMap; }}
        public override String keepDataEditId_NotExistsSubQuery_TEditDataAsOne(TEditDataCQ subQuery) {
            if (_dataEditId_NotExistsSubQuery_TEditDataAsOneMap == null) { _dataEditId_NotExistsSubQuery_TEditDataAsOneMap = new LinkedHashMap<String, TEditDataCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_NotExistsSubQuery_TEditDataAsOneMap.size() + 1);
            _dataEditId_NotExistsSubQuery_TEditDataAsOneMap.put(key, subQuery); return "DataEditId_NotExistsSubQuery_TEditDataAsOne." + key;
        }

        protected Map<String, TItemInfoCQ> _dataEditId_NotExistsSubQuery_TItemInfoListMap;
        public Map<String, TItemInfoCQ> DataEditId_NotExistsSubQuery_TItemInfoList { get { return _dataEditId_NotExistsSubQuery_TItemInfoListMap; }}
        public override String keepDataEditId_NotExistsSubQuery_TItemInfoList(TItemInfoCQ subQuery) {
            if (_dataEditId_NotExistsSubQuery_TItemInfoListMap == null) { _dataEditId_NotExistsSubQuery_TItemInfoListMap = new LinkedHashMap<String, TItemInfoCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_NotExistsSubQuery_TItemInfoListMap.size() + 1);
            _dataEditId_NotExistsSubQuery_TItemInfoListMap.put(key, subQuery); return "DataEditId_NotExistsSubQuery_TItemInfoList." + key;
        }

        protected Map<String, TDataProcessNewItemCQ> _dataEditId_InScopeSubQuery_TDataProcessNewItemAsOneMap;
        public Map<String, TDataProcessNewItemCQ> DataEditId_InScopeSubQuery_TDataProcessNewItemAsOne { get { return _dataEditId_InScopeSubQuery_TDataProcessNewItemAsOneMap; }}
        public override String keepDataEditId_InScopeSubQuery_TDataProcessNewItemAsOne(TDataProcessNewItemCQ subQuery) {
            if (_dataEditId_InScopeSubQuery_TDataProcessNewItemAsOneMap == null) { _dataEditId_InScopeSubQuery_TDataProcessNewItemAsOneMap = new LinkedHashMap<String, TDataProcessNewItemCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_InScopeSubQuery_TDataProcessNewItemAsOneMap.size() + 1);
            _dataEditId_InScopeSubQuery_TDataProcessNewItemAsOneMap.put(key, subQuery); return "DataEditId_InScopeSubQuery_TDataProcessNewItemAsOne." + key;
        }

        protected Map<String, TDeleteDataCQ> _dataEditId_InScopeSubQuery_TDeleteDataAsOneMap;
        public Map<String, TDeleteDataCQ> DataEditId_InScopeSubQuery_TDeleteDataAsOne { get { return _dataEditId_InScopeSubQuery_TDeleteDataAsOneMap; }}
        public override String keepDataEditId_InScopeSubQuery_TDeleteDataAsOne(TDeleteDataCQ subQuery) {
            if (_dataEditId_InScopeSubQuery_TDeleteDataAsOneMap == null) { _dataEditId_InScopeSubQuery_TDeleteDataAsOneMap = new LinkedHashMap<String, TDeleteDataCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_InScopeSubQuery_TDeleteDataAsOneMap.size() + 1);
            _dataEditId_InScopeSubQuery_TDeleteDataAsOneMap.put(key, subQuery); return "DataEditId_InScopeSubQuery_TDeleteDataAsOne." + key;
        }

        protected Map<String, TEditDataCQ> _dataEditId_InScopeSubQuery_TEditDataAsOneMap;
        public Map<String, TEditDataCQ> DataEditId_InScopeSubQuery_TEditDataAsOne { get { return _dataEditId_InScopeSubQuery_TEditDataAsOneMap; }}
        public override String keepDataEditId_InScopeSubQuery_TEditDataAsOne(TEditDataCQ subQuery) {
            if (_dataEditId_InScopeSubQuery_TEditDataAsOneMap == null) { _dataEditId_InScopeSubQuery_TEditDataAsOneMap = new LinkedHashMap<String, TEditDataCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_InScopeSubQuery_TEditDataAsOneMap.size() + 1);
            _dataEditId_InScopeSubQuery_TEditDataAsOneMap.put(key, subQuery); return "DataEditId_InScopeSubQuery_TEditDataAsOne." + key;
        }

        protected Map<String, TItemInfoCQ> _dataEditId_InScopeSubQuery_TItemInfoListMap;
        public Map<String, TItemInfoCQ> DataEditId_InScopeSubQuery_TItemInfoList { get { return _dataEditId_InScopeSubQuery_TItemInfoListMap; }}
        public override String keepDataEditId_InScopeSubQuery_TItemInfoList(TItemInfoCQ subQuery) {
            if (_dataEditId_InScopeSubQuery_TItemInfoListMap == null) { _dataEditId_InScopeSubQuery_TItemInfoListMap = new LinkedHashMap<String, TItemInfoCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_InScopeSubQuery_TItemInfoListMap.size() + 1);
            _dataEditId_InScopeSubQuery_TItemInfoListMap.put(key, subQuery); return "DataEditId_InScopeSubQuery_TItemInfoList." + key;
        }

        protected Map<String, TDataProcessNewItemCQ> _dataEditId_NotInScopeSubQuery_TDataProcessNewItemAsOneMap;
        public Map<String, TDataProcessNewItemCQ> DataEditId_NotInScopeSubQuery_TDataProcessNewItemAsOne { get { return _dataEditId_NotInScopeSubQuery_TDataProcessNewItemAsOneMap; }}
        public override String keepDataEditId_NotInScopeSubQuery_TDataProcessNewItemAsOne(TDataProcessNewItemCQ subQuery) {
            if (_dataEditId_NotInScopeSubQuery_TDataProcessNewItemAsOneMap == null) { _dataEditId_NotInScopeSubQuery_TDataProcessNewItemAsOneMap = new LinkedHashMap<String, TDataProcessNewItemCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_NotInScopeSubQuery_TDataProcessNewItemAsOneMap.size() + 1);
            _dataEditId_NotInScopeSubQuery_TDataProcessNewItemAsOneMap.put(key, subQuery); return "DataEditId_NotInScopeSubQuery_TDataProcessNewItemAsOne." + key;
        }

        protected Map<String, TDeleteDataCQ> _dataEditId_NotInScopeSubQuery_TDeleteDataAsOneMap;
        public Map<String, TDeleteDataCQ> DataEditId_NotInScopeSubQuery_TDeleteDataAsOne { get { return _dataEditId_NotInScopeSubQuery_TDeleteDataAsOneMap; }}
        public override String keepDataEditId_NotInScopeSubQuery_TDeleteDataAsOne(TDeleteDataCQ subQuery) {
            if (_dataEditId_NotInScopeSubQuery_TDeleteDataAsOneMap == null) { _dataEditId_NotInScopeSubQuery_TDeleteDataAsOneMap = new LinkedHashMap<String, TDeleteDataCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_NotInScopeSubQuery_TDeleteDataAsOneMap.size() + 1);
            _dataEditId_NotInScopeSubQuery_TDeleteDataAsOneMap.put(key, subQuery); return "DataEditId_NotInScopeSubQuery_TDeleteDataAsOne." + key;
        }

        protected Map<String, TEditDataCQ> _dataEditId_NotInScopeSubQuery_TEditDataAsOneMap;
        public Map<String, TEditDataCQ> DataEditId_NotInScopeSubQuery_TEditDataAsOne { get { return _dataEditId_NotInScopeSubQuery_TEditDataAsOneMap; }}
        public override String keepDataEditId_NotInScopeSubQuery_TEditDataAsOne(TEditDataCQ subQuery) {
            if (_dataEditId_NotInScopeSubQuery_TEditDataAsOneMap == null) { _dataEditId_NotInScopeSubQuery_TEditDataAsOneMap = new LinkedHashMap<String, TEditDataCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_NotInScopeSubQuery_TEditDataAsOneMap.size() + 1);
            _dataEditId_NotInScopeSubQuery_TEditDataAsOneMap.put(key, subQuery); return "DataEditId_NotInScopeSubQuery_TEditDataAsOne." + key;
        }

        protected Map<String, TItemInfoCQ> _dataEditId_NotInScopeSubQuery_TItemInfoListMap;
        public Map<String, TItemInfoCQ> DataEditId_NotInScopeSubQuery_TItemInfoList { get { return _dataEditId_NotInScopeSubQuery_TItemInfoListMap; }}
        public override String keepDataEditId_NotInScopeSubQuery_TItemInfoList(TItemInfoCQ subQuery) {
            if (_dataEditId_NotInScopeSubQuery_TItemInfoListMap == null) { _dataEditId_NotInScopeSubQuery_TItemInfoListMap = new LinkedHashMap<String, TItemInfoCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_NotInScopeSubQuery_TItemInfoListMap.size() + 1);
            _dataEditId_NotInScopeSubQuery_TItemInfoListMap.put(key, subQuery); return "DataEditId_NotInScopeSubQuery_TItemInfoList." + key;
        }

        protected Map<String, TItemInfoCQ> _dataEditId_SpecifyDerivedReferrer_TItemInfoListMap;
        public Map<String, TItemInfoCQ> DataEditId_SpecifyDerivedReferrer_TItemInfoList { get { return _dataEditId_SpecifyDerivedReferrer_TItemInfoListMap; }}
        public override String keepDataEditId_SpecifyDerivedReferrer_TItemInfoList(TItemInfoCQ subQuery) {
            if (_dataEditId_SpecifyDerivedReferrer_TItemInfoListMap == null) { _dataEditId_SpecifyDerivedReferrer_TItemInfoListMap = new LinkedHashMap<String, TItemInfoCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_SpecifyDerivedReferrer_TItemInfoListMap.size() + 1);
            _dataEditId_SpecifyDerivedReferrer_TItemInfoListMap.put(key, subQuery); return "DataEditId_SpecifyDerivedReferrer_TItemInfoList." + key;
        }

        protected Map<String, TItemInfoCQ> _dataEditId_QueryDerivedReferrer_TItemInfoListMap;
        public Map<String, TItemInfoCQ> DataEditId_QueryDerivedReferrer_TItemInfoList { get { return _dataEditId_QueryDerivedReferrer_TItemInfoListMap; } }
        public override String keepDataEditId_QueryDerivedReferrer_TItemInfoList(TItemInfoCQ subQuery) {
            if (_dataEditId_QueryDerivedReferrer_TItemInfoListMap == null) { _dataEditId_QueryDerivedReferrer_TItemInfoListMap = new LinkedHashMap<String, TItemInfoCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_QueryDerivedReferrer_TItemInfoListMap.size() + 1);
            _dataEditId_QueryDerivedReferrer_TItemInfoListMap.put(key, subQuery); return "DataEditId_QueryDerivedReferrer_TItemInfoList." + key;
        }
        protected Map<String, Object> _dataEditId_QueryDerivedReferrer_TItemInfoListParameterMap;
        public Map<String, Object> DataEditId_QueryDerivedReferrer_TItemInfoListParameter { get { return _dataEditId_QueryDerivedReferrer_TItemInfoListParameterMap; } }
        public override String keepDataEditId_QueryDerivedReferrer_TItemInfoListParameter(Object parameterValue) {
            if (_dataEditId_QueryDerivedReferrer_TItemInfoListParameterMap == null) { _dataEditId_QueryDerivedReferrer_TItemInfoListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_dataEditId_QueryDerivedReferrer_TItemInfoListParameterMap.size() + 1);
            _dataEditId_QueryDerivedReferrer_TItemInfoListParameterMap.put(key, parameterValue); return "DataEditId_QueryDerivedReferrer_TItemInfoListParameter." + key;
        }

        public BsTDataEditListCQ AddOrderBy_DataEditId_Asc() { regOBA("DATA_EDIT_ID");return this; }
        public BsTDataEditListCQ AddOrderBy_DataEditId_Desc() { regOBD("DATA_EDIT_ID");return this; }

        protected ConditionValue _qcwebid;
        public ConditionValue Qcwebid {
            get { if (_qcwebid == null) { _qcwebid = new ConditionValue(); } return _qcwebid; }
        }
        protected override ConditionValue getCValueQcwebid() { return this.Qcwebid; }


        protected Map<String, TQcwebSurveyInfoCQ> _qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap;
        public Map<String, TQcwebSurveyInfoCQ> Qcwebid_InScopeSubQuery_TQcwebSurveyInfo { get { return _qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap; }}
        public override String keepQcwebid_InScopeSubQuery_TQcwebSurveyInfo(TQcwebSurveyInfoCQ subQuery) {
            if (_qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap == null) { _qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap.size() + 1);
            _qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap.put(key, subQuery); return "Qcwebid_InScopeSubQuery_TQcwebSurveyInfo." + key;
        }

        protected Map<String, TQcwebSurveyInfoCQ> _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap;
        public Map<String, TQcwebSurveyInfoCQ> Qcwebid_NotInScopeSubQuery_TQcwebSurveyInfo { get { return _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap; }}
        public override String keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfo(TQcwebSurveyInfoCQ subQuery) {
            if (_qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap == null) { _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap.size() + 1);
            _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap.put(key, subQuery); return "Qcwebid_NotInScopeSubQuery_TQcwebSurveyInfo." + key;
        }

        public BsTDataEditListCQ AddOrderBy_Qcwebid_Asc() { regOBA("QCWEBID");return this; }
        public BsTDataEditListCQ AddOrderBy_Qcwebid_Desc() { regOBD("QCWEBID");return this; }

        protected ConditionValue _executeNo;
        public ConditionValue ExecuteNo {
            get { if (_executeNo == null) { _executeNo = new ConditionValue(); } return _executeNo; }
        }
        protected override ConditionValue getCValueExecuteNo() { return this.ExecuteNo; }


        public BsTDataEditListCQ AddOrderBy_ExecuteNo_Asc() { regOBA("EXECUTE_NO");return this; }
        public BsTDataEditListCQ AddOrderBy_ExecuteNo_Desc() { regOBD("EXECUTE_NO");return this; }

        protected ConditionValue _executeFlag;
        public ConditionValue ExecuteFlag {
            get { if (_executeFlag == null) { _executeFlag = new ConditionValue(); } return _executeFlag; }
        }
        protected override ConditionValue getCValueExecuteFlag() { return this.ExecuteFlag; }


        public BsTDataEditListCQ AddOrderBy_ExecuteFlag_Asc() { regOBA("EXECUTE_FLAG");return this; }
        public BsTDataEditListCQ AddOrderBy_ExecuteFlag_Desc() { regOBD("EXECUTE_FLAG");return this; }

        protected ConditionValue _editMenuMasterId;
        public ConditionValue EditMenuMasterId {
            get { if (_editMenuMasterId == null) { _editMenuMasterId = new ConditionValue(); } return _editMenuMasterId; }
        }
        protected override ConditionValue getCValueEditMenuMasterId() { return this.EditMenuMasterId; }


        protected Map<String, TEditMenuMasterCQ> _editMenuMasterId_InScopeSubQuery_TEditMenuMasterMap;
        public Map<String, TEditMenuMasterCQ> EditMenuMasterId_InScopeSubQuery_TEditMenuMaster { get { return _editMenuMasterId_InScopeSubQuery_TEditMenuMasterMap; }}
        public override String keepEditMenuMasterId_InScopeSubQuery_TEditMenuMaster(TEditMenuMasterCQ subQuery) {
            if (_editMenuMasterId_InScopeSubQuery_TEditMenuMasterMap == null) { _editMenuMasterId_InScopeSubQuery_TEditMenuMasterMap = new LinkedHashMap<String, TEditMenuMasterCQ>(); }
            String key = "subQueryMapKey" + (_editMenuMasterId_InScopeSubQuery_TEditMenuMasterMap.size() + 1);
            _editMenuMasterId_InScopeSubQuery_TEditMenuMasterMap.put(key, subQuery); return "EditMenuMasterId_InScopeSubQuery_TEditMenuMaster." + key;
        }

        protected Map<String, TEditMenuMasterCQ> _editMenuMasterId_NotInScopeSubQuery_TEditMenuMasterMap;
        public Map<String, TEditMenuMasterCQ> EditMenuMasterId_NotInScopeSubQuery_TEditMenuMaster { get { return _editMenuMasterId_NotInScopeSubQuery_TEditMenuMasterMap; }}
        public override String keepEditMenuMasterId_NotInScopeSubQuery_TEditMenuMaster(TEditMenuMasterCQ subQuery) {
            if (_editMenuMasterId_NotInScopeSubQuery_TEditMenuMasterMap == null) { _editMenuMasterId_NotInScopeSubQuery_TEditMenuMasterMap = new LinkedHashMap<String, TEditMenuMasterCQ>(); }
            String key = "subQueryMapKey" + (_editMenuMasterId_NotInScopeSubQuery_TEditMenuMasterMap.size() + 1);
            _editMenuMasterId_NotInScopeSubQuery_TEditMenuMasterMap.put(key, subQuery); return "EditMenuMasterId_NotInScopeSubQuery_TEditMenuMaster." + key;
        }

        public BsTDataEditListCQ AddOrderBy_EditMenuMasterId_Asc() { regOBA("EDIT_MENU_MASTER_ID");return this; }
        public BsTDataEditListCQ AddOrderBy_EditMenuMasterId_Desc() { regOBD("EDIT_MENU_MASTER_ID");return this; }

        protected ConditionValue _description;
        public ConditionValue Description {
            get { if (_description == null) { _description = new ConditionValue(); } return _description; }
        }
        protected override ConditionValue getCValueDescription() { return this.Description; }


        public BsTDataEditListCQ AddOrderBy_Description_Asc() { regOBA("DESCRIPTION");return this; }
        public BsTDataEditListCQ AddOrderBy_Description_Desc() { regOBD("DESCRIPTION");return this; }

        protected ConditionValue _conditionItemViewName;
        public ConditionValue ConditionItemViewName {
            get { if (_conditionItemViewName == null) { _conditionItemViewName = new ConditionValue(); } return _conditionItemViewName; }
        }
        protected override ConditionValue getCValueConditionItemViewName() { return this.ConditionItemViewName; }


        public BsTDataEditListCQ AddOrderBy_ConditionItemViewName_Asc() { regOBA("CONDITION_ITEM_VIEW_NAME");return this; }
        public BsTDataEditListCQ AddOrderBy_ConditionItemViewName_Desc() { regOBD("CONDITION_ITEM_VIEW_NAME");return this; }

        protected ConditionValue _targetItemViewName;
        public ConditionValue TargetItemViewName {
            get { if (_targetItemViewName == null) { _targetItemViewName = new ConditionValue(); } return _targetItemViewName; }
        }
        protected override ConditionValue getCValueTargetItemViewName() { return this.TargetItemViewName; }


        public BsTDataEditListCQ AddOrderBy_TargetItemViewName_Asc() { regOBA("TARGET_ITEM_VIEW_NAME");return this; }
        public BsTDataEditListCQ AddOrderBy_TargetItemViewName_Desc() { regOBD("TARGET_ITEM_VIEW_NAME");return this; }

        protected ConditionValue _status;
        public ConditionValue Status {
            get { if (_status == null) { _status = new ConditionValue(); } return _status; }
        }
        protected override ConditionValue getCValueStatus() { return this.Status; }


        public BsTDataEditListCQ AddOrderBy_Status_Asc() { regOBA("STATUS");return this; }
        public BsTDataEditListCQ AddOrderBy_Status_Desc() { regOBD("STATUS");return this; }

        protected ConditionValue _latestFlag;
        public ConditionValue LatestFlag {
            get { if (_latestFlag == null) { _latestFlag = new ConditionValue(); } return _latestFlag; }
        }
        protected override ConditionValue getCValueLatestFlag() { return this.LatestFlag; }


        public BsTDataEditListCQ AddOrderBy_LatestFlag_Asc() { regOBA("LATEST_FLAG");return this; }
        public BsTDataEditListCQ AddOrderBy_LatestFlag_Desc() { regOBD("LATEST_FLAG");return this; }

        protected ConditionValue _derivedDataEditId;
        public ConditionValue DerivedDataEditId {
            get { if (_derivedDataEditId == null) { _derivedDataEditId = new ConditionValue(); } return _derivedDataEditId; }
        }
        protected override ConditionValue getCValueDerivedDataEditId() { return this.DerivedDataEditId; }


        public BsTDataEditListCQ AddOrderBy_DerivedDataEditId_Asc() { regOBA("DERIVED_DATA_EDIT_ID");return this; }
        public BsTDataEditListCQ AddOrderBy_DerivedDataEditId_Desc() { regOBD("DERIVED_DATA_EDIT_ID");return this; }

        protected ConditionValue _deleteReserveFlag;
        public ConditionValue DeleteReserveFlag {
            get { if (_deleteReserveFlag == null) { _deleteReserveFlag = new ConditionValue(); } return _deleteReserveFlag; }
        }
        protected override ConditionValue getCValueDeleteReserveFlag() { return this.DeleteReserveFlag; }


        public BsTDataEditListCQ AddOrderBy_DeleteReserveFlag_Asc() { regOBA("DELETE_RESERVE_FLAG");return this; }
        public BsTDataEditListCQ AddOrderBy_DeleteReserveFlag_Desc() { regOBD("DELETE_RESERVE_FLAG");return this; }

        protected ConditionValue _lastUpdateUser;
        public ConditionValue LastUpdateUser {
            get { if (_lastUpdateUser == null) { _lastUpdateUser = new ConditionValue(); } return _lastUpdateUser; }
        }
        protected override ConditionValue getCValueLastUpdateUser() { return this.LastUpdateUser; }


        public BsTDataEditListCQ AddOrderBy_LastUpdateUser_Asc() { regOBA("LAST_UPDATE_USER");return this; }
        public BsTDataEditListCQ AddOrderBy_LastUpdateUser_Desc() { regOBD("LAST_UPDATE_USER");return this; }

        protected ConditionValue _lastUpdateDatetime;
        public ConditionValue LastUpdateDatetime {
            get { if (_lastUpdateDatetime == null) { _lastUpdateDatetime = new ConditionValue(); } return _lastUpdateDatetime; }
        }
        protected override ConditionValue getCValueLastUpdateDatetime() { return this.LastUpdateDatetime; }


        public BsTDataEditListCQ AddOrderBy_LastUpdateDatetime_Asc() { regOBA("LAST_UPDATE_DATETIME");return this; }
        public BsTDataEditListCQ AddOrderBy_LastUpdateDatetime_Desc() { regOBD("LAST_UPDATE_DATETIME");return this; }

        protected ConditionValue _editFlag;
        public ConditionValue EditFlag {
            get { if (_editFlag == null) { _editFlag = new ConditionValue(); } return _editFlag; }
        }
        protected override ConditionValue getCValueEditFlag() { return this.EditFlag; }


        public BsTDataEditListCQ AddOrderBy_EditFlag_Asc() { regOBA("EDIT_FLAG");return this; }
        public BsTDataEditListCQ AddOrderBy_EditFlag_Desc() { regOBD("EDIT_FLAG");return this; }

        public BsTDataEditListCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTDataEditListCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TDataEditListCQ baseQuery = (TDataEditListCQ)baseQueryAsSuper;
            TDataEditListCQ unionQuery = (TDataEditListCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTQcwebSurveyInfo()) {
                unionQuery.QueryTQcwebSurveyInfo().reflectRelationOnUnionQuery(baseQuery.QueryTQcwebSurveyInfo(), unionQuery.QueryTQcwebSurveyInfo());
            }
            if (baseQuery.hasConditionQueryTEditMenuMaster()) {
                unionQuery.QueryTEditMenuMaster().reflectRelationOnUnionQuery(baseQuery.QueryTEditMenuMaster(), unionQuery.QueryTEditMenuMaster());
            }
            if (baseQuery.hasConditionQueryTDataProcessNewItemAsOne()) {
                unionQuery.QueryTDataProcessNewItemAsOne().reflectRelationOnUnionQuery(baseQuery.QueryTDataProcessNewItemAsOne(), unionQuery.QueryTDataProcessNewItemAsOne());
            }
            if (baseQuery.hasConditionQueryTDeleteDataAsOne()) {
                unionQuery.QueryTDeleteDataAsOne().reflectRelationOnUnionQuery(baseQuery.QueryTDeleteDataAsOne(), unionQuery.QueryTDeleteDataAsOne());
            }
            if (baseQuery.hasConditionQueryTEditDataAsOne()) {
                unionQuery.QueryTEditDataAsOne().reflectRelationOnUnionQuery(baseQuery.QueryTEditDataAsOne(), unionQuery.QueryTEditDataAsOne());
            }

        }
    
        protected TQcwebSurveyInfoCQ _conditionQueryTQcwebSurveyInfo;
        public TQcwebSurveyInfoCQ QueryTQcwebSurveyInfo() {
            return this.ConditionQueryTQcwebSurveyInfo;
        }
        public TQcwebSurveyInfoCQ ConditionQueryTQcwebSurveyInfo {
            get {
                if (_conditionQueryTQcwebSurveyInfo == null) {
                    _conditionQueryTQcwebSurveyInfo = xcreateQueryTQcwebSurveyInfo();
                    xsetupOuterJoin_TQcwebSurveyInfo();
                }
                return _conditionQueryTQcwebSurveyInfo;
            }
        }
        protected TQcwebSurveyInfoCQ xcreateQueryTQcwebSurveyInfo() {
            String nrp = resolveNextRelationPathTQcwebSurveyInfo();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TQcwebSurveyInfoCQ cq = new TQcwebSurveyInfoCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tQcwebSurveyInfo"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TQcwebSurveyInfo() {
            TQcwebSurveyInfoCQ cq = ConditionQueryTQcwebSurveyInfo;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("QCWEBID", "QCWEBID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTQcwebSurveyInfo() {
            return resolveNextRelationPath("T_DATA_EDIT_LIST", "tQcwebSurveyInfo");
        }
        public bool hasConditionQueryTQcwebSurveyInfo() {
            return _conditionQueryTQcwebSurveyInfo != null;
        }
        protected TEditMenuMasterCQ _conditionQueryTEditMenuMaster;
        public TEditMenuMasterCQ QueryTEditMenuMaster() {
            return this.ConditionQueryTEditMenuMaster;
        }
        public TEditMenuMasterCQ ConditionQueryTEditMenuMaster {
            get {
                if (_conditionQueryTEditMenuMaster == null) {
                    _conditionQueryTEditMenuMaster = xcreateQueryTEditMenuMaster();
                    xsetupOuterJoin_TEditMenuMaster();
                }
                return _conditionQueryTEditMenuMaster;
            }
        }
        protected TEditMenuMasterCQ xcreateQueryTEditMenuMaster() {
            String nrp = resolveNextRelationPathTEditMenuMaster();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TEditMenuMasterCQ cq = new TEditMenuMasterCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tEditMenuMaster"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TEditMenuMaster() {
            TEditMenuMasterCQ cq = ConditionQueryTEditMenuMaster;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("EDIT_MENU_MASTER_ID", "EDIT_MENU_MASTER_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTEditMenuMaster() {
            return resolveNextRelationPath("T_DATA_EDIT_LIST", "tEditMenuMaster");
        }
        public bool hasConditionQueryTEditMenuMaster() {
            return _conditionQueryTEditMenuMaster != null;
        }


        protected TDataProcessNewItemCQ _conditionQueryTDataProcessNewItemAsOne;
        public TDataProcessNewItemCQ ConditionQueryTDataProcessNewItemAsOne {
            get {
                if (_conditionQueryTDataProcessNewItemAsOne == null) {
                    _conditionQueryTDataProcessNewItemAsOne = createQueryTDataProcessNewItemAsOne();
                    xsetupOuterJoin_TDataProcessNewItemAsOne();
                }
                return _conditionQueryTDataProcessNewItemAsOne;
            }
        }
        public TDataProcessNewItemCQ QueryTDataProcessNewItemAsOne() { return this.ConditionQueryTDataProcessNewItemAsOne; }
        protected TDataProcessNewItemCQ createQueryTDataProcessNewItemAsOne() {
            String nrp = resolveNextRelationPathTDataProcessNewItemAsOne();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TDataProcessNewItemCQ cq = new TDataProcessNewItemCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tDataProcessNewItemAsOne"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TDataProcessNewItemAsOne() {
            TDataProcessNewItemCQ cq = ConditionQueryTDataProcessNewItemAsOne;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("DATA_EDIT_ID", "DATA_EDIT_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTDataProcessNewItemAsOne() {
            return resolveNextRelationPath("T_DATA_EDIT_LIST", "tDataProcessNewItemAsOne");
        }
        public bool hasConditionQueryTDataProcessNewItemAsOne() {
            return _conditionQueryTDataProcessNewItemAsOne != null;
        }

        protected TDeleteDataCQ _conditionQueryTDeleteDataAsOne;
        public TDeleteDataCQ ConditionQueryTDeleteDataAsOne {
            get {
                if (_conditionQueryTDeleteDataAsOne == null) {
                    _conditionQueryTDeleteDataAsOne = createQueryTDeleteDataAsOne();
                    xsetupOuterJoin_TDeleteDataAsOne();
                }
                return _conditionQueryTDeleteDataAsOne;
            }
        }
        public TDeleteDataCQ QueryTDeleteDataAsOne() { return this.ConditionQueryTDeleteDataAsOne; }
        protected TDeleteDataCQ createQueryTDeleteDataAsOne() {
            String nrp = resolveNextRelationPathTDeleteDataAsOne();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TDeleteDataCQ cq = new TDeleteDataCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tDeleteDataAsOne"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TDeleteDataAsOne() {
            TDeleteDataCQ cq = ConditionQueryTDeleteDataAsOne;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("DATA_EDIT_ID", "DATA_EDIT_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTDeleteDataAsOne() {
            return resolveNextRelationPath("T_DATA_EDIT_LIST", "tDeleteDataAsOne");
        }
        public bool hasConditionQueryTDeleteDataAsOne() {
            return _conditionQueryTDeleteDataAsOne != null;
        }

        protected TEditDataCQ _conditionQueryTEditDataAsOne;
        public TEditDataCQ ConditionQueryTEditDataAsOne {
            get {
                if (_conditionQueryTEditDataAsOne == null) {
                    _conditionQueryTEditDataAsOne = createQueryTEditDataAsOne();
                    xsetupOuterJoin_TEditDataAsOne();
                }
                return _conditionQueryTEditDataAsOne;
            }
        }
        public TEditDataCQ QueryTEditDataAsOne() { return this.ConditionQueryTEditDataAsOne; }
        protected TEditDataCQ createQueryTEditDataAsOne() {
            String nrp = resolveNextRelationPathTEditDataAsOne();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TEditDataCQ cq = new TEditDataCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tEditDataAsOne"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TEditDataAsOne() {
            TEditDataCQ cq = ConditionQueryTEditDataAsOne;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("DATA_EDIT_ID", "DATA_EDIT_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTEditDataAsOne() {
            return resolveNextRelationPath("T_DATA_EDIT_LIST", "tEditDataAsOne");
        }
        public bool hasConditionQueryTEditDataAsOne() {
            return _conditionQueryTEditDataAsOne != null;
        }

	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TDataEditListCQ> _scalarSubQueryMap;
	    public Map<String, TDataEditListCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TDataEditListCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TDataEditListCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TDataEditListCQ> _myselfInScopeSubQueryMap;
        public Map<String, TDataEditListCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TDataEditListCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TDataEditListCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
