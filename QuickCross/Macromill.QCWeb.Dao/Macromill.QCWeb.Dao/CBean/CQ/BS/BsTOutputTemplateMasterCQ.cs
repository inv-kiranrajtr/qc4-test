
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTOutputTemplateMasterCQ : AbstractBsTOutputTemplateMasterCQ {

        protected TOutputTemplateMasterCIQ _inlineQuery;

        public BsTOutputTemplateMasterCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TOutputTemplateMasterCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TOutputTemplateMasterCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TOutputTemplateMasterCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TOutputTemplateMasterCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _outputTemplateMasterId;
        public ConditionValue OutputTemplateMasterId {
            get { if (_outputTemplateMasterId == null) { _outputTemplateMasterId = new ConditionValue(); } return _outputTemplateMasterId; }
        }
        protected override ConditionValue getCValueOutputTemplateMasterId() { return this.OutputTemplateMasterId; }


        protected Map<String, TOutputTemplateCQ> _outputTemplateMasterId_ExistsSubQuery_TOutputTemplateListMap;
        public Map<String, TOutputTemplateCQ> OutputTemplateMasterId_ExistsSubQuery_TOutputTemplateList { get { return _outputTemplateMasterId_ExistsSubQuery_TOutputTemplateListMap; }}
        public override String keepOutputTemplateMasterId_ExistsSubQuery_TOutputTemplateList(TOutputTemplateCQ subQuery) {
            if (_outputTemplateMasterId_ExistsSubQuery_TOutputTemplateListMap == null) { _outputTemplateMasterId_ExistsSubQuery_TOutputTemplateListMap = new LinkedHashMap<String, TOutputTemplateCQ>(); }
            String key = "subQueryMapKey" + (_outputTemplateMasterId_ExistsSubQuery_TOutputTemplateListMap.size() + 1);
            _outputTemplateMasterId_ExistsSubQuery_TOutputTemplateListMap.put(key, subQuery); return "OutputTemplateMasterId_ExistsSubQuery_TOutputTemplateList." + key;
        }

        protected Map<String, TOutputTemplateCQ> _outputTemplateMasterId_NotExistsSubQuery_TOutputTemplateListMap;
        public Map<String, TOutputTemplateCQ> OutputTemplateMasterId_NotExistsSubQuery_TOutputTemplateList { get { return _outputTemplateMasterId_NotExistsSubQuery_TOutputTemplateListMap; }}
        public override String keepOutputTemplateMasterId_NotExistsSubQuery_TOutputTemplateList(TOutputTemplateCQ subQuery) {
            if (_outputTemplateMasterId_NotExistsSubQuery_TOutputTemplateListMap == null) { _outputTemplateMasterId_NotExistsSubQuery_TOutputTemplateListMap = new LinkedHashMap<String, TOutputTemplateCQ>(); }
            String key = "subQueryMapKey" + (_outputTemplateMasterId_NotExistsSubQuery_TOutputTemplateListMap.size() + 1);
            _outputTemplateMasterId_NotExistsSubQuery_TOutputTemplateListMap.put(key, subQuery); return "OutputTemplateMasterId_NotExistsSubQuery_TOutputTemplateList." + key;
        }

        protected Map<String, TOutputTemplateCQ> _outputTemplateMasterId_InScopeSubQuery_TOutputTemplateListMap;
        public Map<String, TOutputTemplateCQ> OutputTemplateMasterId_InScopeSubQuery_TOutputTemplateList { get { return _outputTemplateMasterId_InScopeSubQuery_TOutputTemplateListMap; }}
        public override String keepOutputTemplateMasterId_InScopeSubQuery_TOutputTemplateList(TOutputTemplateCQ subQuery) {
            if (_outputTemplateMasterId_InScopeSubQuery_TOutputTemplateListMap == null) { _outputTemplateMasterId_InScopeSubQuery_TOutputTemplateListMap = new LinkedHashMap<String, TOutputTemplateCQ>(); }
            String key = "subQueryMapKey" + (_outputTemplateMasterId_InScopeSubQuery_TOutputTemplateListMap.size() + 1);
            _outputTemplateMasterId_InScopeSubQuery_TOutputTemplateListMap.put(key, subQuery); return "OutputTemplateMasterId_InScopeSubQuery_TOutputTemplateList." + key;
        }

        protected Map<String, TOutputTemplateCQ> _outputTemplateMasterId_NotInScopeSubQuery_TOutputTemplateListMap;
        public Map<String, TOutputTemplateCQ> OutputTemplateMasterId_NotInScopeSubQuery_TOutputTemplateList { get { return _outputTemplateMasterId_NotInScopeSubQuery_TOutputTemplateListMap; }}
        public override String keepOutputTemplateMasterId_NotInScopeSubQuery_TOutputTemplateList(TOutputTemplateCQ subQuery) {
            if (_outputTemplateMasterId_NotInScopeSubQuery_TOutputTemplateListMap == null) { _outputTemplateMasterId_NotInScopeSubQuery_TOutputTemplateListMap = new LinkedHashMap<String, TOutputTemplateCQ>(); }
            String key = "subQueryMapKey" + (_outputTemplateMasterId_NotInScopeSubQuery_TOutputTemplateListMap.size() + 1);
            _outputTemplateMasterId_NotInScopeSubQuery_TOutputTemplateListMap.put(key, subQuery); return "OutputTemplateMasterId_NotInScopeSubQuery_TOutputTemplateList." + key;
        }

        protected Map<String, TOutputTemplateCQ> _outputTemplateMasterId_SpecifyDerivedReferrer_TOutputTemplateListMap;
        public Map<String, TOutputTemplateCQ> OutputTemplateMasterId_SpecifyDerivedReferrer_TOutputTemplateList { get { return _outputTemplateMasterId_SpecifyDerivedReferrer_TOutputTemplateListMap; }}
        public override String keepOutputTemplateMasterId_SpecifyDerivedReferrer_TOutputTemplateList(TOutputTemplateCQ subQuery) {
            if (_outputTemplateMasterId_SpecifyDerivedReferrer_TOutputTemplateListMap == null) { _outputTemplateMasterId_SpecifyDerivedReferrer_TOutputTemplateListMap = new LinkedHashMap<String, TOutputTemplateCQ>(); }
            String key = "subQueryMapKey" + (_outputTemplateMasterId_SpecifyDerivedReferrer_TOutputTemplateListMap.size() + 1);
            _outputTemplateMasterId_SpecifyDerivedReferrer_TOutputTemplateListMap.put(key, subQuery); return "OutputTemplateMasterId_SpecifyDerivedReferrer_TOutputTemplateList." + key;
        }

        protected Map<String, TOutputTemplateCQ> _outputTemplateMasterId_QueryDerivedReferrer_TOutputTemplateListMap;
        public Map<String, TOutputTemplateCQ> OutputTemplateMasterId_QueryDerivedReferrer_TOutputTemplateList { get { return _outputTemplateMasterId_QueryDerivedReferrer_TOutputTemplateListMap; } }
        public override String keepOutputTemplateMasterId_QueryDerivedReferrer_TOutputTemplateList(TOutputTemplateCQ subQuery) {
            if (_outputTemplateMasterId_QueryDerivedReferrer_TOutputTemplateListMap == null) { _outputTemplateMasterId_QueryDerivedReferrer_TOutputTemplateListMap = new LinkedHashMap<String, TOutputTemplateCQ>(); }
            String key = "subQueryMapKey" + (_outputTemplateMasterId_QueryDerivedReferrer_TOutputTemplateListMap.size() + 1);
            _outputTemplateMasterId_QueryDerivedReferrer_TOutputTemplateListMap.put(key, subQuery); return "OutputTemplateMasterId_QueryDerivedReferrer_TOutputTemplateList." + key;
        }
        protected Map<String, Object> _outputTemplateMasterId_QueryDerivedReferrer_TOutputTemplateListParameterMap;
        public Map<String, Object> OutputTemplateMasterId_QueryDerivedReferrer_TOutputTemplateListParameter { get { return _outputTemplateMasterId_QueryDerivedReferrer_TOutputTemplateListParameterMap; } }
        public override String keepOutputTemplateMasterId_QueryDerivedReferrer_TOutputTemplateListParameter(Object parameterValue) {
            if (_outputTemplateMasterId_QueryDerivedReferrer_TOutputTemplateListParameterMap == null) { _outputTemplateMasterId_QueryDerivedReferrer_TOutputTemplateListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_outputTemplateMasterId_QueryDerivedReferrer_TOutputTemplateListParameterMap.size() + 1);
            _outputTemplateMasterId_QueryDerivedReferrer_TOutputTemplateListParameterMap.put(key, parameterValue); return "OutputTemplateMasterId_QueryDerivedReferrer_TOutputTemplateListParameter." + key;
        }

        public BsTOutputTemplateMasterCQ AddOrderBy_OutputTemplateMasterId_Asc() { regOBA("OUTPUT_TEMPLATE_MASTER_ID");return this; }
        public BsTOutputTemplateMasterCQ AddOrderBy_OutputTemplateMasterId_Desc() { regOBD("OUTPUT_TEMPLATE_MASTER_ID");return this; }

        protected ConditionValue _path;
        public ConditionValue Path {
            get { if (_path == null) { _path = new ConditionValue(); } return _path; }
        }
        protected override ConditionValue getCValuePath() { return this.Path; }


        public BsTOutputTemplateMasterCQ AddOrderBy_Path_Asc() { regOBA("PATH");return this; }
        public BsTOutputTemplateMasterCQ AddOrderBy_Path_Desc() { regOBD("PATH");return this; }

        protected ConditionValue _md5Hash;
        public ConditionValue Md5Hash {
            get { if (_md5Hash == null) { _md5Hash = new ConditionValue(); } return _md5Hash; }
        }
        protected override ConditionValue getCValueMd5Hash() { return this.Md5Hash; }


        public BsTOutputTemplateMasterCQ AddOrderBy_Md5Hash_Asc() { regOBA("MD5_HASH");return this; }
        public BsTOutputTemplateMasterCQ AddOrderBy_Md5Hash_Desc() { regOBD("MD5_HASH");return this; }

        public BsTOutputTemplateMasterCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTOutputTemplateMasterCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {

        }
    


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TOutputTemplateMasterCQ> _scalarSubQueryMap;
	    public Map<String, TOutputTemplateMasterCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TOutputTemplateMasterCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TOutputTemplateMasterCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TOutputTemplateMasterCQ> _myselfInScopeSubQueryMap;
        public Map<String, TOutputTemplateMasterCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TOutputTemplateMasterCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TOutputTemplateMasterCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
