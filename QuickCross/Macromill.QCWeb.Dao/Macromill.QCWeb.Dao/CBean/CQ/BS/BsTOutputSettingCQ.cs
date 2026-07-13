
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTOutputSettingCQ : AbstractBsTOutputSettingCQ {

        protected TOutputSettingCIQ _inlineQuery;

        public BsTOutputSettingCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TOutputSettingCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TOutputSettingCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TOutputSettingCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TOutputSettingCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _qcwebid;
        public ConditionValue Qcwebid {
            get { if (_qcwebid == null) { _qcwebid = new ConditionValue(); } return _qcwebid; }
        }
        protected override ConditionValue getCValueQcwebid() { return this.Qcwebid; }


        protected Map<String, TOutputHistoryItemCQ> _qcwebid_ExistsSubQuery_TOutputHistoryItemListMap;
        public Map<String, TOutputHistoryItemCQ> Qcwebid_ExistsSubQuery_TOutputHistoryItemList { get { return _qcwebid_ExistsSubQuery_TOutputHistoryItemListMap; }}
        public override String keepQcwebid_ExistsSubQuery_TOutputHistoryItemList(TOutputHistoryItemCQ subQuery) {
            if (_qcwebid_ExistsSubQuery_TOutputHistoryItemListMap == null) { _qcwebid_ExistsSubQuery_TOutputHistoryItemListMap = new LinkedHashMap<String, TOutputHistoryItemCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_ExistsSubQuery_TOutputHistoryItemListMap.size() + 1);
            _qcwebid_ExistsSubQuery_TOutputHistoryItemListMap.put(key, subQuery); return "Qcwebid_ExistsSubQuery_TOutputHistoryItemList." + key;
        }

        protected Map<String, TQcwebSurveyInfoCQ> _qcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOneMap;
        public Map<String, TQcwebSurveyInfoCQ> Qcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOne { get { return _qcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOneMap; }}
        public override String keepQcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery) {
            if (_qcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOneMap == null) { _qcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOneMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOneMap.size() + 1);
            _qcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOneMap.put(key, subQuery); return "Qcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOne." + key;
        }

        protected Map<String, TOutputHistoryItemCQ> _qcwebid_NotExistsSubQuery_TOutputHistoryItemListMap;
        public Map<String, TOutputHistoryItemCQ> Qcwebid_NotExistsSubQuery_TOutputHistoryItemList { get { return _qcwebid_NotExistsSubQuery_TOutputHistoryItemListMap; }}
        public override String keepQcwebid_NotExistsSubQuery_TOutputHistoryItemList(TOutputHistoryItemCQ subQuery) {
            if (_qcwebid_NotExistsSubQuery_TOutputHistoryItemListMap == null) { _qcwebid_NotExistsSubQuery_TOutputHistoryItemListMap = new LinkedHashMap<String, TOutputHistoryItemCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotExistsSubQuery_TOutputHistoryItemListMap.size() + 1);
            _qcwebid_NotExistsSubQuery_TOutputHistoryItemListMap.put(key, subQuery); return "Qcwebid_NotExistsSubQuery_TOutputHistoryItemList." + key;
        }

        protected Map<String, TQcwebSurveyInfoCQ> _qcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOneMap;
        public Map<String, TQcwebSurveyInfoCQ> Qcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOne { get { return _qcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOneMap; }}
        public override String keepQcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery) {
            if (_qcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOneMap == null) { _qcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOneMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOneMap.size() + 1);
            _qcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOneMap.put(key, subQuery); return "Qcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOne." + key;
        }

        protected Map<String, TQcwebSurveyInfoCQ> _qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap;
        public Map<String, TQcwebSurveyInfoCQ> Qcwebid_InScopeSubQuery_TQcwebSurveyInfo { get { return _qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap; }}
        public override String keepQcwebid_InScopeSubQuery_TQcwebSurveyInfo(TQcwebSurveyInfoCQ subQuery) {
            if (_qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap == null) { _qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap.size() + 1);
            _qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap.put(key, subQuery); return "Qcwebid_InScopeSubQuery_TQcwebSurveyInfo." + key;
        }

        protected Map<String, TOutputHistoryItemCQ> _qcwebid_InScopeSubQuery_TOutputHistoryItemListMap;
        public Map<String, TOutputHistoryItemCQ> Qcwebid_InScopeSubQuery_TOutputHistoryItemList { get { return _qcwebid_InScopeSubQuery_TOutputHistoryItemListMap; }}
        public override String keepQcwebid_InScopeSubQuery_TOutputHistoryItemList(TOutputHistoryItemCQ subQuery) {
            if (_qcwebid_InScopeSubQuery_TOutputHistoryItemListMap == null) { _qcwebid_InScopeSubQuery_TOutputHistoryItemListMap = new LinkedHashMap<String, TOutputHistoryItemCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_InScopeSubQuery_TOutputHistoryItemListMap.size() + 1);
            _qcwebid_InScopeSubQuery_TOutputHistoryItemListMap.put(key, subQuery); return "Qcwebid_InScopeSubQuery_TOutputHistoryItemList." + key;
        }

        protected Map<String, TQcwebSurveyInfoCQ> _qcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOneMap;
        public Map<String, TQcwebSurveyInfoCQ> Qcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOne { get { return _qcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOneMap; }}
        public override String keepQcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery) {
            if (_qcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOneMap == null) { _qcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOneMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOneMap.size() + 1);
            _qcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOneMap.put(key, subQuery); return "Qcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOne." + key;
        }

        protected Map<String, TQcwebSurveyInfoCQ> _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap;
        public Map<String, TQcwebSurveyInfoCQ> Qcwebid_NotInScopeSubQuery_TQcwebSurveyInfo { get { return _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap; }}
        public override String keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfo(TQcwebSurveyInfoCQ subQuery) {
            if (_qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap == null) { _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap.size() + 1);
            _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap.put(key, subQuery); return "Qcwebid_NotInScopeSubQuery_TQcwebSurveyInfo." + key;
        }

        protected Map<String, TOutputHistoryItemCQ> _qcwebid_NotInScopeSubQuery_TOutputHistoryItemListMap;
        public Map<String, TOutputHistoryItemCQ> Qcwebid_NotInScopeSubQuery_TOutputHistoryItemList { get { return _qcwebid_NotInScopeSubQuery_TOutputHistoryItemListMap; }}
        public override String keepQcwebid_NotInScopeSubQuery_TOutputHistoryItemList(TOutputHistoryItemCQ subQuery) {
            if (_qcwebid_NotInScopeSubQuery_TOutputHistoryItemListMap == null) { _qcwebid_NotInScopeSubQuery_TOutputHistoryItemListMap = new LinkedHashMap<String, TOutputHistoryItemCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotInScopeSubQuery_TOutputHistoryItemListMap.size() + 1);
            _qcwebid_NotInScopeSubQuery_TOutputHistoryItemListMap.put(key, subQuery); return "Qcwebid_NotInScopeSubQuery_TOutputHistoryItemList." + key;
        }

        protected Map<String, TQcwebSurveyInfoCQ> _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOneMap;
        public Map<String, TQcwebSurveyInfoCQ> Qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOne { get { return _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOneMap; }}
        public override String keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery) {
            if (_qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOneMap == null) { _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOneMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOneMap.size() + 1);
            _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOneMap.put(key, subQuery); return "Qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOne." + key;
        }

        protected Map<String, TOutputHistoryItemCQ> _qcwebid_SpecifyDerivedReferrer_TOutputHistoryItemListMap;
        public Map<String, TOutputHistoryItemCQ> Qcwebid_SpecifyDerivedReferrer_TOutputHistoryItemList { get { return _qcwebid_SpecifyDerivedReferrer_TOutputHistoryItemListMap; }}
        public override String keepQcwebid_SpecifyDerivedReferrer_TOutputHistoryItemList(TOutputHistoryItemCQ subQuery) {
            if (_qcwebid_SpecifyDerivedReferrer_TOutputHistoryItemListMap == null) { _qcwebid_SpecifyDerivedReferrer_TOutputHistoryItemListMap = new LinkedHashMap<String, TOutputHistoryItemCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_SpecifyDerivedReferrer_TOutputHistoryItemListMap.size() + 1);
            _qcwebid_SpecifyDerivedReferrer_TOutputHistoryItemListMap.put(key, subQuery); return "Qcwebid_SpecifyDerivedReferrer_TOutputHistoryItemList." + key;
        }

        protected Map<String, TOutputHistoryItemCQ> _qcwebid_QueryDerivedReferrer_TOutputHistoryItemListMap;
        public Map<String, TOutputHistoryItemCQ> Qcwebid_QueryDerivedReferrer_TOutputHistoryItemList { get { return _qcwebid_QueryDerivedReferrer_TOutputHistoryItemListMap; } }
        public override String keepQcwebid_QueryDerivedReferrer_TOutputHistoryItemList(TOutputHistoryItemCQ subQuery) {
            if (_qcwebid_QueryDerivedReferrer_TOutputHistoryItemListMap == null) { _qcwebid_QueryDerivedReferrer_TOutputHistoryItemListMap = new LinkedHashMap<String, TOutputHistoryItemCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_QueryDerivedReferrer_TOutputHistoryItemListMap.size() + 1);
            _qcwebid_QueryDerivedReferrer_TOutputHistoryItemListMap.put(key, subQuery); return "Qcwebid_QueryDerivedReferrer_TOutputHistoryItemList." + key;
        }
        protected Map<String, Object> _qcwebid_QueryDerivedReferrer_TOutputHistoryItemListParameterMap;
        public Map<String, Object> Qcwebid_QueryDerivedReferrer_TOutputHistoryItemListParameter { get { return _qcwebid_QueryDerivedReferrer_TOutputHistoryItemListParameterMap; } }
        public override String keepQcwebid_QueryDerivedReferrer_TOutputHistoryItemListParameter(Object parameterValue) {
            if (_qcwebid_QueryDerivedReferrer_TOutputHistoryItemListParameterMap == null) { _qcwebid_QueryDerivedReferrer_TOutputHistoryItemListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_qcwebid_QueryDerivedReferrer_TOutputHistoryItemListParameterMap.size() + 1);
            _qcwebid_QueryDerivedReferrer_TOutputHistoryItemListParameterMap.put(key, parameterValue); return "Qcwebid_QueryDerivedReferrer_TOutputHistoryItemListParameter." + key;
        }

        public BsTOutputSettingCQ AddOrderBy_Qcwebid_Asc() { regOBA("QCWEBID");return this; }
        public BsTOutputSettingCQ AddOrderBy_Qcwebid_Desc() { regOBD("QCWEBID");return this; }

        protected ConditionValue _outputFileType;
        public ConditionValue OutputFileType {
            get { if (_outputFileType == null) { _outputFileType = new ConditionValue(); } return _outputFileType; }
        }
        protected override ConditionValue getCValueOutputFileType() { return this.OutputFileType; }


        public BsTOutputSettingCQ AddOrderBy_OutputFileType_Asc() { regOBA("OUTPUT_FILE_TYPE");return this; }
        public BsTOutputSettingCQ AddOrderBy_OutputFileType_Desc() { regOBD("OUTPUT_FILE_TYPE");return this; }

        protected ConditionValue _partitionFlag;
        public ConditionValue PartitionFlag {
            get { if (_partitionFlag == null) { _partitionFlag = new ConditionValue(); } return _partitionFlag; }
        }
        protected override ConditionValue getCValuePartitionFlag() { return this.PartitionFlag; }


        public BsTOutputSettingCQ AddOrderBy_PartitionFlag_Asc() { regOBA("PARTITION_FLAG");return this; }
        public BsTOutputSettingCQ AddOrderBy_PartitionFlag_Desc() { regOBD("PARTITION_FLAG");return this; }

        protected ConditionValue _layoutFlag;
        public ConditionValue LayoutFlag {
            get { if (_layoutFlag == null) { _layoutFlag = new ConditionValue(); } return _layoutFlag; }
        }
        protected override ConditionValue getCValueLayoutFlag() { return this.LayoutFlag; }


        public BsTOutputSettingCQ AddOrderBy_LayoutFlag_Asc() { regOBA("LAYOUT_FLAG");return this; }
        public BsTOutputSettingCQ AddOrderBy_LayoutFlag_Desc() { regOBD("LAYOUT_FLAG");return this; }

        protected ConditionValue _outputType;
        public ConditionValue OutputType {
            get { if (_outputType == null) { _outputType = new ConditionValue(); } return _outputType; }
        }
        protected override ConditionValue getCValueOutputType() { return this.OutputType; }


        public BsTOutputSettingCQ AddOrderBy_OutputType_Asc() { regOBA("OUTPUT_TYPE");return this; }
        public BsTOutputSettingCQ AddOrderBy_OutputType_Desc() { regOBD("OUTPUT_TYPE");return this; }

        protected ConditionValue _noAnswerChar;
        public ConditionValue NoAnswerChar {
            get { if (_noAnswerChar == null) { _noAnswerChar = new ConditionValue(); } return _noAnswerChar; }
        }
        protected override ConditionValue getCValueNoAnswerChar() { return this.NoAnswerChar; }


        public BsTOutputSettingCQ AddOrderBy_NoAnswerChar_Asc() { regOBA("NO_ANSWER_CHAR");return this; }
        public BsTOutputSettingCQ AddOrderBy_NoAnswerChar_Desc() { regOBD("NO_ANSWER_CHAR");return this; }

        protected ConditionValue _unmacthChar;
        public ConditionValue UnmacthChar {
            get { if (_unmacthChar == null) { _unmacthChar = new ConditionValue(); } return _unmacthChar; }
        }
        protected override ConditionValue getCValueUnmacthChar() { return this.UnmacthChar; }


        public BsTOutputSettingCQ AddOrderBy_UnmacthChar_Asc() { regOBA("UNMACTH_CHAR");return this; }
        public BsTOutputSettingCQ AddOrderBy_UnmacthChar_Desc() { regOBD("UNMACTH_CHAR");return this; }

        protected ConditionValue _multiItemType;
        public ConditionValue MultiItemType {
            get { if (_multiItemType == null) { _multiItemType = new ConditionValue(); } return _multiItemType; }
        }
        protected override ConditionValue getCValueMultiItemType() { return this.MultiItemType; }


        public BsTOutputSettingCQ AddOrderBy_MultiItemType_Asc() { regOBA("MULTI_ITEM_TYPE");return this; }
        public BsTOutputSettingCQ AddOrderBy_MultiItemType_Desc() { regOBD("MULTI_ITEM_TYPE");return this; }

        protected ConditionValue _numberType;
        public ConditionValue NumberType {
            get { if (_numberType == null) { _numberType = new ConditionValue(); } return _numberType; }
        }
        protected override ConditionValue getCValueNumberType() { return this.NumberType; }


        public BsTOutputSettingCQ AddOrderBy_NumberType_Asc() { regOBA("NUMBER_TYPE");return this; }
        public BsTOutputSettingCQ AddOrderBy_NumberType_Desc() { regOBD("NUMBER_TYPE");return this; }

        public BsTOutputSettingCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTOutputSettingCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TOutputSettingCQ baseQuery = (TOutputSettingCQ)baseQueryAsSuper;
            TOutputSettingCQ unionQuery = (TOutputSettingCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTQcwebSurveyInfo()) {
                unionQuery.QueryTQcwebSurveyInfo().reflectRelationOnUnionQuery(baseQuery.QueryTQcwebSurveyInfo(), unionQuery.QueryTQcwebSurveyInfo());
            }
            if (baseQuery.hasConditionQueryTOutputHistoryItem()) {
                unionQuery.QueryTOutputHistoryItem().reflectRelationOnUnionQuery(baseQuery.QueryTOutputHistoryItem(), unionQuery.QueryTOutputHistoryItem());
            }
            if (baseQuery.hasConditionQueryTQcwebSurveyInfoAsOne()) {
                unionQuery.QueryTQcwebSurveyInfoAsOne().reflectRelationOnUnionQuery(baseQuery.QueryTQcwebSurveyInfoAsOne(), unionQuery.QueryTQcwebSurveyInfoAsOne());
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
            return resolveNextRelationPath("T_OUTPUT_SETTING", "tQcwebSurveyInfo");
        }
        public bool hasConditionQueryTQcwebSurveyInfo() {
            return _conditionQueryTQcwebSurveyInfo != null;
        }
        protected TOutputHistoryItemCQ _conditionQueryTOutputHistoryItem;
        public TOutputHistoryItemCQ QueryTOutputHistoryItem() {
            return this.ConditionQueryTOutputHistoryItem;
        }
        public TOutputHistoryItemCQ ConditionQueryTOutputHistoryItem {
            get {
                if (_conditionQueryTOutputHistoryItem == null) {
                    _conditionQueryTOutputHistoryItem = xcreateQueryTOutputHistoryItem();
                    xsetupOuterJoin_TOutputHistoryItem();
                }
                return _conditionQueryTOutputHistoryItem;
            }
        }
        protected TOutputHistoryItemCQ xcreateQueryTOutputHistoryItem() {
            String nrp = resolveNextRelationPathTOutputHistoryItem();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TOutputHistoryItemCQ cq = new TOutputHistoryItemCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tOutputHistoryItem"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TOutputHistoryItem() {
            TOutputHistoryItemCQ cq = ConditionQueryTOutputHistoryItem;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("QCWEBID", "QCWebID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTOutputHistoryItem() {
            return resolveNextRelationPath("T_OUTPUT_SETTING", "tOutputHistoryItem");
        }
        public bool hasConditionQueryTOutputHistoryItem() {
            return _conditionQueryTOutputHistoryItem != null;
        }


        protected TQcwebSurveyInfoCQ _conditionQueryTQcwebSurveyInfoAsOne;
        public TQcwebSurveyInfoCQ ConditionQueryTQcwebSurveyInfoAsOne {
            get {
                if (_conditionQueryTQcwebSurveyInfoAsOne == null) {
                    _conditionQueryTQcwebSurveyInfoAsOne = createQueryTQcwebSurveyInfoAsOne();
                    xsetupOuterJoin_TQcwebSurveyInfoAsOne();
                }
                return _conditionQueryTQcwebSurveyInfoAsOne;
            }
        }
        public TQcwebSurveyInfoCQ QueryTQcwebSurveyInfoAsOne() { return this.ConditionQueryTQcwebSurveyInfoAsOne; }
        protected TQcwebSurveyInfoCQ createQueryTQcwebSurveyInfoAsOne() {
            String nrp = resolveNextRelationPathTQcwebSurveyInfoAsOne();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TQcwebSurveyInfoCQ cq = new TQcwebSurveyInfoCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tQcwebSurveyInfoAsOne"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TQcwebSurveyInfoAsOne() {
            TQcwebSurveyInfoCQ cq = ConditionQueryTQcwebSurveyInfoAsOne;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("QCWEBID", "QCWebID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTQcwebSurveyInfoAsOne() {
            return resolveNextRelationPath("T_OUTPUT_SETTING", "tQcwebSurveyInfoAsOne");
        }
        public bool hasConditionQueryTQcwebSurveyInfoAsOne() {
            return _conditionQueryTQcwebSurveyInfoAsOne != null;
        }

	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TOutputSettingCQ> _scalarSubQueryMap;
	    public Map<String, TOutputSettingCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TOutputSettingCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TOutputSettingCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TOutputSettingCQ> _myselfInScopeSubQueryMap;
        public Map<String, TOutputSettingCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TOutputSettingCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TOutputSettingCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
