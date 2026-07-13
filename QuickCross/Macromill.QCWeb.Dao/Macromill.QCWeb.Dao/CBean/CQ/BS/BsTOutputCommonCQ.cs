
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTOutputCommonCQ : AbstractBsTOutputCommonCQ {

        protected TOutputCommonCIQ _inlineQuery;

        public BsTOutputCommonCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TOutputCommonCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TOutputCommonCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TOutputCommonCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TOutputCommonCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _outputCommonId;
        public ConditionValue OutputCommonId {
            get { if (_outputCommonId == null) { _outputCommonId = new ConditionValue(); } return _outputCommonId; }
        }
        protected override ConditionValue getCValueOutputCommonId() { return this.OutputCommonId; }


        protected Map<String, TOutputSubCklistCQ> _outputCommonId_ExistsSubQuery_TOutputSubCklistListMap;
        public Map<String, TOutputSubCklistCQ> OutputCommonId_ExistsSubQuery_TOutputSubCklistList { get { return _outputCommonId_ExistsSubQuery_TOutputSubCklistListMap; }}
        public override String keepOutputCommonId_ExistsSubQuery_TOutputSubCklistList(TOutputSubCklistCQ subQuery) {
            if (_outputCommonId_ExistsSubQuery_TOutputSubCklistListMap == null) { _outputCommonId_ExistsSubQuery_TOutputSubCklistListMap = new LinkedHashMap<String, TOutputSubCklistCQ>(); }
            String key = "subQueryMapKey" + (_outputCommonId_ExistsSubQuery_TOutputSubCklistListMap.size() + 1);
            _outputCommonId_ExistsSubQuery_TOutputSubCklistListMap.put(key, subQuery); return "OutputCommonId_ExistsSubQuery_TOutputSubCklistList." + key;
        }

        protected Map<String, TOutputSubCrossCQ> _outputCommonId_ExistsSubQuery_TOutputSubCrossListMap;
        public Map<String, TOutputSubCrossCQ> OutputCommonId_ExistsSubQuery_TOutputSubCrossList { get { return _outputCommonId_ExistsSubQuery_TOutputSubCrossListMap; }}
        public override String keepOutputCommonId_ExistsSubQuery_TOutputSubCrossList(TOutputSubCrossCQ subQuery) {
            if (_outputCommonId_ExistsSubQuery_TOutputSubCrossListMap == null) { _outputCommonId_ExistsSubQuery_TOutputSubCrossListMap = new LinkedHashMap<String, TOutputSubCrossCQ>(); }
            String key = "subQueryMapKey" + (_outputCommonId_ExistsSubQuery_TOutputSubCrossListMap.size() + 1);
            _outputCommonId_ExistsSubQuery_TOutputSubCrossListMap.put(key, subQuery); return "OutputCommonId_ExistsSubQuery_TOutputSubCrossList." + key;
        }

        protected Map<String, TOutputSubFaCQ> _outputCommonId_ExistsSubQuery_TOutputSubFaListMap;
        public Map<String, TOutputSubFaCQ> OutputCommonId_ExistsSubQuery_TOutputSubFaList { get { return _outputCommonId_ExistsSubQuery_TOutputSubFaListMap; }}
        public override String keepOutputCommonId_ExistsSubQuery_TOutputSubFaList(TOutputSubFaCQ subQuery) {
            if (_outputCommonId_ExistsSubQuery_TOutputSubFaListMap == null) { _outputCommonId_ExistsSubQuery_TOutputSubFaListMap = new LinkedHashMap<String, TOutputSubFaCQ>(); }
            String key = "subQueryMapKey" + (_outputCommonId_ExistsSubQuery_TOutputSubFaListMap.size() + 1);
            _outputCommonId_ExistsSubQuery_TOutputSubFaListMap.put(key, subQuery); return "OutputCommonId_ExistsSubQuery_TOutputSubFaList." + key;
        }

        protected Map<String, TOutputSubGtCQ> _outputCommonId_ExistsSubQuery_TOutputSubGtListMap;
        public Map<String, TOutputSubGtCQ> OutputCommonId_ExistsSubQuery_TOutputSubGtList { get { return _outputCommonId_ExistsSubQuery_TOutputSubGtListMap; }}
        public override String keepOutputCommonId_ExistsSubQuery_TOutputSubGtList(TOutputSubGtCQ subQuery) {
            if (_outputCommonId_ExistsSubQuery_TOutputSubGtListMap == null) { _outputCommonId_ExistsSubQuery_TOutputSubGtListMap = new LinkedHashMap<String, TOutputSubGtCQ>(); }
            String key = "subQueryMapKey" + (_outputCommonId_ExistsSubQuery_TOutputSubGtListMap.size() + 1);
            _outputCommonId_ExistsSubQuery_TOutputSubGtListMap.put(key, subQuery); return "OutputCommonId_ExistsSubQuery_TOutputSubGtList." + key;
        }

        protected Map<String, TOutputSubCklistCQ> _outputCommonId_NotExistsSubQuery_TOutputSubCklistListMap;
        public Map<String, TOutputSubCklistCQ> OutputCommonId_NotExistsSubQuery_TOutputSubCklistList { get { return _outputCommonId_NotExistsSubQuery_TOutputSubCklistListMap; }}
        public override String keepOutputCommonId_NotExistsSubQuery_TOutputSubCklistList(TOutputSubCklistCQ subQuery) {
            if (_outputCommonId_NotExistsSubQuery_TOutputSubCklistListMap == null) { _outputCommonId_NotExistsSubQuery_TOutputSubCklistListMap = new LinkedHashMap<String, TOutputSubCklistCQ>(); }
            String key = "subQueryMapKey" + (_outputCommonId_NotExistsSubQuery_TOutputSubCklistListMap.size() + 1);
            _outputCommonId_NotExistsSubQuery_TOutputSubCklistListMap.put(key, subQuery); return "OutputCommonId_NotExistsSubQuery_TOutputSubCklistList." + key;
        }

        protected Map<String, TOutputSubCrossCQ> _outputCommonId_NotExistsSubQuery_TOutputSubCrossListMap;
        public Map<String, TOutputSubCrossCQ> OutputCommonId_NotExistsSubQuery_TOutputSubCrossList { get { return _outputCommonId_NotExistsSubQuery_TOutputSubCrossListMap; }}
        public override String keepOutputCommonId_NotExistsSubQuery_TOutputSubCrossList(TOutputSubCrossCQ subQuery) {
            if (_outputCommonId_NotExistsSubQuery_TOutputSubCrossListMap == null) { _outputCommonId_NotExistsSubQuery_TOutputSubCrossListMap = new LinkedHashMap<String, TOutputSubCrossCQ>(); }
            String key = "subQueryMapKey" + (_outputCommonId_NotExistsSubQuery_TOutputSubCrossListMap.size() + 1);
            _outputCommonId_NotExistsSubQuery_TOutputSubCrossListMap.put(key, subQuery); return "OutputCommonId_NotExistsSubQuery_TOutputSubCrossList." + key;
        }

        protected Map<String, TOutputSubFaCQ> _outputCommonId_NotExistsSubQuery_TOutputSubFaListMap;
        public Map<String, TOutputSubFaCQ> OutputCommonId_NotExistsSubQuery_TOutputSubFaList { get { return _outputCommonId_NotExistsSubQuery_TOutputSubFaListMap; }}
        public override String keepOutputCommonId_NotExistsSubQuery_TOutputSubFaList(TOutputSubFaCQ subQuery) {
            if (_outputCommonId_NotExistsSubQuery_TOutputSubFaListMap == null) { _outputCommonId_NotExistsSubQuery_TOutputSubFaListMap = new LinkedHashMap<String, TOutputSubFaCQ>(); }
            String key = "subQueryMapKey" + (_outputCommonId_NotExistsSubQuery_TOutputSubFaListMap.size() + 1);
            _outputCommonId_NotExistsSubQuery_TOutputSubFaListMap.put(key, subQuery); return "OutputCommonId_NotExistsSubQuery_TOutputSubFaList." + key;
        }

        protected Map<String, TOutputSubGtCQ> _outputCommonId_NotExistsSubQuery_TOutputSubGtListMap;
        public Map<String, TOutputSubGtCQ> OutputCommonId_NotExistsSubQuery_TOutputSubGtList { get { return _outputCommonId_NotExistsSubQuery_TOutputSubGtListMap; }}
        public override String keepOutputCommonId_NotExistsSubQuery_TOutputSubGtList(TOutputSubGtCQ subQuery) {
            if (_outputCommonId_NotExistsSubQuery_TOutputSubGtListMap == null) { _outputCommonId_NotExistsSubQuery_TOutputSubGtListMap = new LinkedHashMap<String, TOutputSubGtCQ>(); }
            String key = "subQueryMapKey" + (_outputCommonId_NotExistsSubQuery_TOutputSubGtListMap.size() + 1);
            _outputCommonId_NotExistsSubQuery_TOutputSubGtListMap.put(key, subQuery); return "OutputCommonId_NotExistsSubQuery_TOutputSubGtList." + key;
        }

        protected Map<String, TOutputSubGtCQ> _outputCommonId_InScopeSubQuery_TOutputSubGtMap;
        public Map<String, TOutputSubGtCQ> OutputCommonId_InScopeSubQuery_TOutputSubGt { get { return _outputCommonId_InScopeSubQuery_TOutputSubGtMap; }}
        public override String keepOutputCommonId_InScopeSubQuery_TOutputSubGt(TOutputSubGtCQ subQuery) {
            if (_outputCommonId_InScopeSubQuery_TOutputSubGtMap == null) { _outputCommonId_InScopeSubQuery_TOutputSubGtMap = new LinkedHashMap<String, TOutputSubGtCQ>(); }
            String key = "subQueryMapKey" + (_outputCommonId_InScopeSubQuery_TOutputSubGtMap.size() + 1);
            _outputCommonId_InScopeSubQuery_TOutputSubGtMap.put(key, subQuery); return "OutputCommonId_InScopeSubQuery_TOutputSubGt." + key;
        }

        protected Map<String, TOutputSubCklistCQ> _outputCommonId_InScopeSubQuery_TOutputSubCklistListMap;
        public Map<String, TOutputSubCklistCQ> OutputCommonId_InScopeSubQuery_TOutputSubCklistList { get { return _outputCommonId_InScopeSubQuery_TOutputSubCklistListMap; }}
        public override String keepOutputCommonId_InScopeSubQuery_TOutputSubCklistList(TOutputSubCklistCQ subQuery) {
            if (_outputCommonId_InScopeSubQuery_TOutputSubCklistListMap == null) { _outputCommonId_InScopeSubQuery_TOutputSubCklistListMap = new LinkedHashMap<String, TOutputSubCklistCQ>(); }
            String key = "subQueryMapKey" + (_outputCommonId_InScopeSubQuery_TOutputSubCklistListMap.size() + 1);
            _outputCommonId_InScopeSubQuery_TOutputSubCklistListMap.put(key, subQuery); return "OutputCommonId_InScopeSubQuery_TOutputSubCklistList." + key;
        }

        protected Map<String, TOutputSubCrossCQ> _outputCommonId_InScopeSubQuery_TOutputSubCrossListMap;
        public Map<String, TOutputSubCrossCQ> OutputCommonId_InScopeSubQuery_TOutputSubCrossList { get { return _outputCommonId_InScopeSubQuery_TOutputSubCrossListMap; }}
        public override String keepOutputCommonId_InScopeSubQuery_TOutputSubCrossList(TOutputSubCrossCQ subQuery) {
            if (_outputCommonId_InScopeSubQuery_TOutputSubCrossListMap == null) { _outputCommonId_InScopeSubQuery_TOutputSubCrossListMap = new LinkedHashMap<String, TOutputSubCrossCQ>(); }
            String key = "subQueryMapKey" + (_outputCommonId_InScopeSubQuery_TOutputSubCrossListMap.size() + 1);
            _outputCommonId_InScopeSubQuery_TOutputSubCrossListMap.put(key, subQuery); return "OutputCommonId_InScopeSubQuery_TOutputSubCrossList." + key;
        }

        protected Map<String, TOutputSubFaCQ> _outputCommonId_InScopeSubQuery_TOutputSubFaListMap;
        public Map<String, TOutputSubFaCQ> OutputCommonId_InScopeSubQuery_TOutputSubFaList { get { return _outputCommonId_InScopeSubQuery_TOutputSubFaListMap; }}
        public override String keepOutputCommonId_InScopeSubQuery_TOutputSubFaList(TOutputSubFaCQ subQuery) {
            if (_outputCommonId_InScopeSubQuery_TOutputSubFaListMap == null) { _outputCommonId_InScopeSubQuery_TOutputSubFaListMap = new LinkedHashMap<String, TOutputSubFaCQ>(); }
            String key = "subQueryMapKey" + (_outputCommonId_InScopeSubQuery_TOutputSubFaListMap.size() + 1);
            _outputCommonId_InScopeSubQuery_TOutputSubFaListMap.put(key, subQuery); return "OutputCommonId_InScopeSubQuery_TOutputSubFaList." + key;
        }

        protected Map<String, TOutputSubGtCQ> _outputCommonId_InScopeSubQuery_TOutputSubGtListMap;
        public Map<String, TOutputSubGtCQ> OutputCommonId_InScopeSubQuery_TOutputSubGtList { get { return _outputCommonId_InScopeSubQuery_TOutputSubGtListMap; }}
        public override String keepOutputCommonId_InScopeSubQuery_TOutputSubGtList(TOutputSubGtCQ subQuery) {
            if (_outputCommonId_InScopeSubQuery_TOutputSubGtListMap == null) { _outputCommonId_InScopeSubQuery_TOutputSubGtListMap = new LinkedHashMap<String, TOutputSubGtCQ>(); }
            String key = "subQueryMapKey" + (_outputCommonId_InScopeSubQuery_TOutputSubGtListMap.size() + 1);
            _outputCommonId_InScopeSubQuery_TOutputSubGtListMap.put(key, subQuery); return "OutputCommonId_InScopeSubQuery_TOutputSubGtList." + key;
        }

        protected Map<String, TOutputSubGtCQ> _outputCommonId_NotInScopeSubQuery_TOutputSubGtMap;
        public Map<String, TOutputSubGtCQ> OutputCommonId_NotInScopeSubQuery_TOutputSubGt { get { return _outputCommonId_NotInScopeSubQuery_TOutputSubGtMap; }}
        public override String keepOutputCommonId_NotInScopeSubQuery_TOutputSubGt(TOutputSubGtCQ subQuery) {
            if (_outputCommonId_NotInScopeSubQuery_TOutputSubGtMap == null) { _outputCommonId_NotInScopeSubQuery_TOutputSubGtMap = new LinkedHashMap<String, TOutputSubGtCQ>(); }
            String key = "subQueryMapKey" + (_outputCommonId_NotInScopeSubQuery_TOutputSubGtMap.size() + 1);
            _outputCommonId_NotInScopeSubQuery_TOutputSubGtMap.put(key, subQuery); return "OutputCommonId_NotInScopeSubQuery_TOutputSubGt." + key;
        }

        protected Map<String, TOutputSubCklistCQ> _outputCommonId_NotInScopeSubQuery_TOutputSubCklistListMap;
        public Map<String, TOutputSubCklistCQ> OutputCommonId_NotInScopeSubQuery_TOutputSubCklistList { get { return _outputCommonId_NotInScopeSubQuery_TOutputSubCklistListMap; }}
        public override String keepOutputCommonId_NotInScopeSubQuery_TOutputSubCklistList(TOutputSubCklistCQ subQuery) {
            if (_outputCommonId_NotInScopeSubQuery_TOutputSubCklistListMap == null) { _outputCommonId_NotInScopeSubQuery_TOutputSubCklistListMap = new LinkedHashMap<String, TOutputSubCklistCQ>(); }
            String key = "subQueryMapKey" + (_outputCommonId_NotInScopeSubQuery_TOutputSubCklistListMap.size() + 1);
            _outputCommonId_NotInScopeSubQuery_TOutputSubCklistListMap.put(key, subQuery); return "OutputCommonId_NotInScopeSubQuery_TOutputSubCklistList." + key;
        }

        protected Map<String, TOutputSubCrossCQ> _outputCommonId_NotInScopeSubQuery_TOutputSubCrossListMap;
        public Map<String, TOutputSubCrossCQ> OutputCommonId_NotInScopeSubQuery_TOutputSubCrossList { get { return _outputCommonId_NotInScopeSubQuery_TOutputSubCrossListMap; }}
        public override String keepOutputCommonId_NotInScopeSubQuery_TOutputSubCrossList(TOutputSubCrossCQ subQuery) {
            if (_outputCommonId_NotInScopeSubQuery_TOutputSubCrossListMap == null) { _outputCommonId_NotInScopeSubQuery_TOutputSubCrossListMap = new LinkedHashMap<String, TOutputSubCrossCQ>(); }
            String key = "subQueryMapKey" + (_outputCommonId_NotInScopeSubQuery_TOutputSubCrossListMap.size() + 1);
            _outputCommonId_NotInScopeSubQuery_TOutputSubCrossListMap.put(key, subQuery); return "OutputCommonId_NotInScopeSubQuery_TOutputSubCrossList." + key;
        }

        protected Map<String, TOutputSubFaCQ> _outputCommonId_NotInScopeSubQuery_TOutputSubFaListMap;
        public Map<String, TOutputSubFaCQ> OutputCommonId_NotInScopeSubQuery_TOutputSubFaList { get { return _outputCommonId_NotInScopeSubQuery_TOutputSubFaListMap; }}
        public override String keepOutputCommonId_NotInScopeSubQuery_TOutputSubFaList(TOutputSubFaCQ subQuery) {
            if (_outputCommonId_NotInScopeSubQuery_TOutputSubFaListMap == null) { _outputCommonId_NotInScopeSubQuery_TOutputSubFaListMap = new LinkedHashMap<String, TOutputSubFaCQ>(); }
            String key = "subQueryMapKey" + (_outputCommonId_NotInScopeSubQuery_TOutputSubFaListMap.size() + 1);
            _outputCommonId_NotInScopeSubQuery_TOutputSubFaListMap.put(key, subQuery); return "OutputCommonId_NotInScopeSubQuery_TOutputSubFaList." + key;
        }

        protected Map<String, TOutputSubGtCQ> _outputCommonId_NotInScopeSubQuery_TOutputSubGtListMap;
        public Map<String, TOutputSubGtCQ> OutputCommonId_NotInScopeSubQuery_TOutputSubGtList { get { return _outputCommonId_NotInScopeSubQuery_TOutputSubGtListMap; }}
        public override String keepOutputCommonId_NotInScopeSubQuery_TOutputSubGtList(TOutputSubGtCQ subQuery) {
            if (_outputCommonId_NotInScopeSubQuery_TOutputSubGtListMap == null) { _outputCommonId_NotInScopeSubQuery_TOutputSubGtListMap = new LinkedHashMap<String, TOutputSubGtCQ>(); }
            String key = "subQueryMapKey" + (_outputCommonId_NotInScopeSubQuery_TOutputSubGtListMap.size() + 1);
            _outputCommonId_NotInScopeSubQuery_TOutputSubGtListMap.put(key, subQuery); return "OutputCommonId_NotInScopeSubQuery_TOutputSubGtList." + key;
        }

        protected Map<String, TOutputSubCklistCQ> _outputCommonId_SpecifyDerivedReferrer_TOutputSubCklistListMap;
        public Map<String, TOutputSubCklistCQ> OutputCommonId_SpecifyDerivedReferrer_TOutputSubCklistList { get { return _outputCommonId_SpecifyDerivedReferrer_TOutputSubCklistListMap; }}
        public override String keepOutputCommonId_SpecifyDerivedReferrer_TOutputSubCklistList(TOutputSubCklistCQ subQuery) {
            if (_outputCommonId_SpecifyDerivedReferrer_TOutputSubCklistListMap == null) { _outputCommonId_SpecifyDerivedReferrer_TOutputSubCklistListMap = new LinkedHashMap<String, TOutputSubCklistCQ>(); }
            String key = "subQueryMapKey" + (_outputCommonId_SpecifyDerivedReferrer_TOutputSubCklistListMap.size() + 1);
            _outputCommonId_SpecifyDerivedReferrer_TOutputSubCklistListMap.put(key, subQuery); return "OutputCommonId_SpecifyDerivedReferrer_TOutputSubCklistList." + key;
        }

        protected Map<String, TOutputSubCrossCQ> _outputCommonId_SpecifyDerivedReferrer_TOutputSubCrossListMap;
        public Map<String, TOutputSubCrossCQ> OutputCommonId_SpecifyDerivedReferrer_TOutputSubCrossList { get { return _outputCommonId_SpecifyDerivedReferrer_TOutputSubCrossListMap; }}
        public override String keepOutputCommonId_SpecifyDerivedReferrer_TOutputSubCrossList(TOutputSubCrossCQ subQuery) {
            if (_outputCommonId_SpecifyDerivedReferrer_TOutputSubCrossListMap == null) { _outputCommonId_SpecifyDerivedReferrer_TOutputSubCrossListMap = new LinkedHashMap<String, TOutputSubCrossCQ>(); }
            String key = "subQueryMapKey" + (_outputCommonId_SpecifyDerivedReferrer_TOutputSubCrossListMap.size() + 1);
            _outputCommonId_SpecifyDerivedReferrer_TOutputSubCrossListMap.put(key, subQuery); return "OutputCommonId_SpecifyDerivedReferrer_TOutputSubCrossList." + key;
        }

        protected Map<String, TOutputSubFaCQ> _outputCommonId_SpecifyDerivedReferrer_TOutputSubFaListMap;
        public Map<String, TOutputSubFaCQ> OutputCommonId_SpecifyDerivedReferrer_TOutputSubFaList { get { return _outputCommonId_SpecifyDerivedReferrer_TOutputSubFaListMap; }}
        public override String keepOutputCommonId_SpecifyDerivedReferrer_TOutputSubFaList(TOutputSubFaCQ subQuery) {
            if (_outputCommonId_SpecifyDerivedReferrer_TOutputSubFaListMap == null) { _outputCommonId_SpecifyDerivedReferrer_TOutputSubFaListMap = new LinkedHashMap<String, TOutputSubFaCQ>(); }
            String key = "subQueryMapKey" + (_outputCommonId_SpecifyDerivedReferrer_TOutputSubFaListMap.size() + 1);
            _outputCommonId_SpecifyDerivedReferrer_TOutputSubFaListMap.put(key, subQuery); return "OutputCommonId_SpecifyDerivedReferrer_TOutputSubFaList." + key;
        }

        protected Map<String, TOutputSubGtCQ> _outputCommonId_SpecifyDerivedReferrer_TOutputSubGtListMap;
        public Map<String, TOutputSubGtCQ> OutputCommonId_SpecifyDerivedReferrer_TOutputSubGtList { get { return _outputCommonId_SpecifyDerivedReferrer_TOutputSubGtListMap; }}
        public override String keepOutputCommonId_SpecifyDerivedReferrer_TOutputSubGtList(TOutputSubGtCQ subQuery) {
            if (_outputCommonId_SpecifyDerivedReferrer_TOutputSubGtListMap == null) { _outputCommonId_SpecifyDerivedReferrer_TOutputSubGtListMap = new LinkedHashMap<String, TOutputSubGtCQ>(); }
            String key = "subQueryMapKey" + (_outputCommonId_SpecifyDerivedReferrer_TOutputSubGtListMap.size() + 1);
            _outputCommonId_SpecifyDerivedReferrer_TOutputSubGtListMap.put(key, subQuery); return "OutputCommonId_SpecifyDerivedReferrer_TOutputSubGtList." + key;
        }

        protected Map<String, TOutputSubCklistCQ> _outputCommonId_QueryDerivedReferrer_TOutputSubCklistListMap;
        public Map<String, TOutputSubCklistCQ> OutputCommonId_QueryDerivedReferrer_TOutputSubCklistList { get { return _outputCommonId_QueryDerivedReferrer_TOutputSubCklistListMap; } }
        public override String keepOutputCommonId_QueryDerivedReferrer_TOutputSubCklistList(TOutputSubCklistCQ subQuery) {
            if (_outputCommonId_QueryDerivedReferrer_TOutputSubCklistListMap == null) { _outputCommonId_QueryDerivedReferrer_TOutputSubCklistListMap = new LinkedHashMap<String, TOutputSubCklistCQ>(); }
            String key = "subQueryMapKey" + (_outputCommonId_QueryDerivedReferrer_TOutputSubCklistListMap.size() + 1);
            _outputCommonId_QueryDerivedReferrer_TOutputSubCklistListMap.put(key, subQuery); return "OutputCommonId_QueryDerivedReferrer_TOutputSubCklistList." + key;
        }
        protected Map<String, Object> _outputCommonId_QueryDerivedReferrer_TOutputSubCklistListParameterMap;
        public Map<String, Object> OutputCommonId_QueryDerivedReferrer_TOutputSubCklistListParameter { get { return _outputCommonId_QueryDerivedReferrer_TOutputSubCklistListParameterMap; } }
        public override String keepOutputCommonId_QueryDerivedReferrer_TOutputSubCklistListParameter(Object parameterValue) {
            if (_outputCommonId_QueryDerivedReferrer_TOutputSubCklistListParameterMap == null) { _outputCommonId_QueryDerivedReferrer_TOutputSubCklistListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_outputCommonId_QueryDerivedReferrer_TOutputSubCklistListParameterMap.size() + 1);
            _outputCommonId_QueryDerivedReferrer_TOutputSubCklistListParameterMap.put(key, parameterValue); return "OutputCommonId_QueryDerivedReferrer_TOutputSubCklistListParameter." + key;
        }

        protected Map<String, TOutputSubCrossCQ> _outputCommonId_QueryDerivedReferrer_TOutputSubCrossListMap;
        public Map<String, TOutputSubCrossCQ> OutputCommonId_QueryDerivedReferrer_TOutputSubCrossList { get { return _outputCommonId_QueryDerivedReferrer_TOutputSubCrossListMap; } }
        public override String keepOutputCommonId_QueryDerivedReferrer_TOutputSubCrossList(TOutputSubCrossCQ subQuery) {
            if (_outputCommonId_QueryDerivedReferrer_TOutputSubCrossListMap == null) { _outputCommonId_QueryDerivedReferrer_TOutputSubCrossListMap = new LinkedHashMap<String, TOutputSubCrossCQ>(); }
            String key = "subQueryMapKey" + (_outputCommonId_QueryDerivedReferrer_TOutputSubCrossListMap.size() + 1);
            _outputCommonId_QueryDerivedReferrer_TOutputSubCrossListMap.put(key, subQuery); return "OutputCommonId_QueryDerivedReferrer_TOutputSubCrossList." + key;
        }
        protected Map<String, Object> _outputCommonId_QueryDerivedReferrer_TOutputSubCrossListParameterMap;
        public Map<String, Object> OutputCommonId_QueryDerivedReferrer_TOutputSubCrossListParameter { get { return _outputCommonId_QueryDerivedReferrer_TOutputSubCrossListParameterMap; } }
        public override String keepOutputCommonId_QueryDerivedReferrer_TOutputSubCrossListParameter(Object parameterValue) {
            if (_outputCommonId_QueryDerivedReferrer_TOutputSubCrossListParameterMap == null) { _outputCommonId_QueryDerivedReferrer_TOutputSubCrossListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_outputCommonId_QueryDerivedReferrer_TOutputSubCrossListParameterMap.size() + 1);
            _outputCommonId_QueryDerivedReferrer_TOutputSubCrossListParameterMap.put(key, parameterValue); return "OutputCommonId_QueryDerivedReferrer_TOutputSubCrossListParameter." + key;
        }

        protected Map<String, TOutputSubFaCQ> _outputCommonId_QueryDerivedReferrer_TOutputSubFaListMap;
        public Map<String, TOutputSubFaCQ> OutputCommonId_QueryDerivedReferrer_TOutputSubFaList { get { return _outputCommonId_QueryDerivedReferrer_TOutputSubFaListMap; } }
        public override String keepOutputCommonId_QueryDerivedReferrer_TOutputSubFaList(TOutputSubFaCQ subQuery) {
            if (_outputCommonId_QueryDerivedReferrer_TOutputSubFaListMap == null) { _outputCommonId_QueryDerivedReferrer_TOutputSubFaListMap = new LinkedHashMap<String, TOutputSubFaCQ>(); }
            String key = "subQueryMapKey" + (_outputCommonId_QueryDerivedReferrer_TOutputSubFaListMap.size() + 1);
            _outputCommonId_QueryDerivedReferrer_TOutputSubFaListMap.put(key, subQuery); return "OutputCommonId_QueryDerivedReferrer_TOutputSubFaList." + key;
        }
        protected Map<String, Object> _outputCommonId_QueryDerivedReferrer_TOutputSubFaListParameterMap;
        public Map<String, Object> OutputCommonId_QueryDerivedReferrer_TOutputSubFaListParameter { get { return _outputCommonId_QueryDerivedReferrer_TOutputSubFaListParameterMap; } }
        public override String keepOutputCommonId_QueryDerivedReferrer_TOutputSubFaListParameter(Object parameterValue) {
            if (_outputCommonId_QueryDerivedReferrer_TOutputSubFaListParameterMap == null) { _outputCommonId_QueryDerivedReferrer_TOutputSubFaListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_outputCommonId_QueryDerivedReferrer_TOutputSubFaListParameterMap.size() + 1);
            _outputCommonId_QueryDerivedReferrer_TOutputSubFaListParameterMap.put(key, parameterValue); return "OutputCommonId_QueryDerivedReferrer_TOutputSubFaListParameter." + key;
        }

        protected Map<String, TOutputSubGtCQ> _outputCommonId_QueryDerivedReferrer_TOutputSubGtListMap;
        public Map<String, TOutputSubGtCQ> OutputCommonId_QueryDerivedReferrer_TOutputSubGtList { get { return _outputCommonId_QueryDerivedReferrer_TOutputSubGtListMap; } }
        public override String keepOutputCommonId_QueryDerivedReferrer_TOutputSubGtList(TOutputSubGtCQ subQuery) {
            if (_outputCommonId_QueryDerivedReferrer_TOutputSubGtListMap == null) { _outputCommonId_QueryDerivedReferrer_TOutputSubGtListMap = new LinkedHashMap<String, TOutputSubGtCQ>(); }
            String key = "subQueryMapKey" + (_outputCommonId_QueryDerivedReferrer_TOutputSubGtListMap.size() + 1);
            _outputCommonId_QueryDerivedReferrer_TOutputSubGtListMap.put(key, subQuery); return "OutputCommonId_QueryDerivedReferrer_TOutputSubGtList." + key;
        }
        protected Map<String, Object> _outputCommonId_QueryDerivedReferrer_TOutputSubGtListParameterMap;
        public Map<String, Object> OutputCommonId_QueryDerivedReferrer_TOutputSubGtListParameter { get { return _outputCommonId_QueryDerivedReferrer_TOutputSubGtListParameterMap; } }
        public override String keepOutputCommonId_QueryDerivedReferrer_TOutputSubGtListParameter(Object parameterValue) {
            if (_outputCommonId_QueryDerivedReferrer_TOutputSubGtListParameterMap == null) { _outputCommonId_QueryDerivedReferrer_TOutputSubGtListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_outputCommonId_QueryDerivedReferrer_TOutputSubGtListParameterMap.size() + 1);
            _outputCommonId_QueryDerivedReferrer_TOutputSubGtListParameterMap.put(key, parameterValue); return "OutputCommonId_QueryDerivedReferrer_TOutputSubGtListParameter." + key;
        }

        public BsTOutputCommonCQ AddOrderBy_OutputCommonId_Asc() { regOBA("OUTPUT_COMMON_ID");return this; }
        public BsTOutputCommonCQ AddOrderBy_OutputCommonId_Desc() { regOBD("OUTPUT_COMMON_ID");return this; }

        protected ConditionValue _orderCount;
        public ConditionValue OrderCount {
            get { if (_orderCount == null) { _orderCount = new ConditionValue(); } return _orderCount; }
        }
        protected override ConditionValue getCValueOrderCount() { return this.OrderCount; }


        public BsTOutputCommonCQ AddOrderBy_OrderCount_Asc() { regOBA("ORDER_COUNT");return this; }
        public BsTOutputCommonCQ AddOrderBy_OrderCount_Desc() { regOBD("ORDER_COUNT");return this; }

        protected ConditionValue _tsvFilePath;
        public ConditionValue TsvFilePath {
            get { if (_tsvFilePath == null) { _tsvFilePath = new ConditionValue(); } return _tsvFilePath; }
        }
        protected override ConditionValue getCValueTsvFilePath() { return this.TsvFilePath; }


        public BsTOutputCommonCQ AddOrderBy_TsvFilePath_Asc() { regOBA("TSV_FILE_PATH");return this; }
        public BsTOutputCommonCQ AddOrderBy_TsvFilePath_Desc() { regOBD("TSV_FILE_PATH");return this; }

        protected ConditionValue _excelbookNamePrefix;
        public ConditionValue ExcelbookNamePrefix {
            get { if (_excelbookNamePrefix == null) { _excelbookNamePrefix = new ConditionValue(); } return _excelbookNamePrefix; }
        }
        protected override ConditionValue getCValueExcelbookNamePrefix() { return this.ExcelbookNamePrefix; }


        public BsTOutputCommonCQ AddOrderBy_ExcelbookNamePrefix_Asc() { regOBA("EXCELBOOK_NAME_PREFIX");return this; }
        public BsTOutputCommonCQ AddOrderBy_ExcelbookNamePrefix_Desc() { regOBD("EXCELBOOK_NAME_PREFIX");return this; }

        protected ConditionValue _processStartDatetime;
        public ConditionValue ProcessStartDatetime {
            get { if (_processStartDatetime == null) { _processStartDatetime = new ConditionValue(); } return _processStartDatetime; }
        }
        protected override ConditionValue getCValueProcessStartDatetime() { return this.ProcessStartDatetime; }


        public BsTOutputCommonCQ AddOrderBy_ProcessStartDatetime_Asc() { regOBA("PROCESS_START_DATETIME");return this; }
        public BsTOutputCommonCQ AddOrderBy_ProcessStartDatetime_Desc() { regOBD("PROCESS_START_DATETIME");return this; }

        protected ConditionValue _processForecastEndDatetime;
        public ConditionValue ProcessForecastEndDatetime {
            get { if (_processForecastEndDatetime == null) { _processForecastEndDatetime = new ConditionValue(); } return _processForecastEndDatetime; }
        }
        protected override ConditionValue getCValueProcessForecastEndDatetime() { return this.ProcessForecastEndDatetime; }


        public BsTOutputCommonCQ AddOrderBy_ProcessForecastEndDatetime_Asc() { regOBA("PROCESS_FORECAST_END_DATETIME");return this; }
        public BsTOutputCommonCQ AddOrderBy_ProcessForecastEndDatetime_Desc() { regOBD("PROCESS_FORECAST_END_DATETIME");return this; }

        protected ConditionValue _processEndDatetime;
        public ConditionValue ProcessEndDatetime {
            get { if (_processEndDatetime == null) { _processEndDatetime = new ConditionValue(); } return _processEndDatetime; }
        }
        protected override ConditionValue getCValueProcessEndDatetime() { return this.ProcessEndDatetime; }


        public BsTOutputCommonCQ AddOrderBy_ProcessEndDatetime_Asc() { regOBA("PROCESS_END_DATETIME");return this; }
        public BsTOutputCommonCQ AddOrderBy_ProcessEndDatetime_Desc() { regOBD("PROCESS_END_DATETIME");return this; }

        protected ConditionValue _statusCode;
        public ConditionValue StatusCode {
            get { if (_statusCode == null) { _statusCode = new ConditionValue(); } return _statusCode; }
        }
        protected override ConditionValue getCValueStatusCode() { return this.StatusCode; }


        public BsTOutputCommonCQ AddOrderBy_StatusCode_Asc() { regOBA("STATUS_CODE");return this; }
        public BsTOutputCommonCQ AddOrderBy_StatusCode_Desc() { regOBD("STATUS_CODE");return this; }

        protected ConditionValue _description;
        public ConditionValue Description {
            get { if (_description == null) { _description = new ConditionValue(); } return _description; }
        }
        protected override ConditionValue getCValueDescription() { return this.Description; }


        public BsTOutputCommonCQ AddOrderBy_Description_Asc() { regOBA("DESCRIPTION");return this; }
        public BsTOutputCommonCQ AddOrderBy_Description_Desc() { regOBD("DESCRIPTION");return this; }

        protected ConditionValue _outputType;
        public ConditionValue OutputType {
            get { if (_outputType == null) { _outputType = new ConditionValue(); } return _outputType; }
        }
        protected override ConditionValue getCValueOutputType() { return this.OutputType; }


        public BsTOutputCommonCQ AddOrderBy_OutputType_Asc() { regOBA("OUTPUT_TYPE");return this; }
        public BsTOutputCommonCQ AddOrderBy_OutputType_Desc() { regOBD("OUTPUT_TYPE");return this; }

        protected ConditionValue _outputRequestId;
        public ConditionValue OutputRequestId {
            get { if (_outputRequestId == null) { _outputRequestId = new ConditionValue(); } return _outputRequestId; }
        }
        protected override ConditionValue getCValueOutputRequestId() { return this.OutputRequestId; }


        protected Map<String, TOutputRequestCQ> _outputRequestId_InScopeSubQuery_TOutputRequestMap;
        public Map<String, TOutputRequestCQ> OutputRequestId_InScopeSubQuery_TOutputRequest { get { return _outputRequestId_InScopeSubQuery_TOutputRequestMap; }}
        public override String keepOutputRequestId_InScopeSubQuery_TOutputRequest(TOutputRequestCQ subQuery) {
            if (_outputRequestId_InScopeSubQuery_TOutputRequestMap == null) { _outputRequestId_InScopeSubQuery_TOutputRequestMap = new LinkedHashMap<String, TOutputRequestCQ>(); }
            String key = "subQueryMapKey" + (_outputRequestId_InScopeSubQuery_TOutputRequestMap.size() + 1);
            _outputRequestId_InScopeSubQuery_TOutputRequestMap.put(key, subQuery); return "OutputRequestId_InScopeSubQuery_TOutputRequest." + key;
        }

        protected Map<String, TOutputRequestCQ> _outputRequestId_NotInScopeSubQuery_TOutputRequestMap;
        public Map<String, TOutputRequestCQ> OutputRequestId_NotInScopeSubQuery_TOutputRequest { get { return _outputRequestId_NotInScopeSubQuery_TOutputRequestMap; }}
        public override String keepOutputRequestId_NotInScopeSubQuery_TOutputRequest(TOutputRequestCQ subQuery) {
            if (_outputRequestId_NotInScopeSubQuery_TOutputRequestMap == null) { _outputRequestId_NotInScopeSubQuery_TOutputRequestMap = new LinkedHashMap<String, TOutputRequestCQ>(); }
            String key = "subQueryMapKey" + (_outputRequestId_NotInScopeSubQuery_TOutputRequestMap.size() + 1);
            _outputRequestId_NotInScopeSubQuery_TOutputRequestMap.put(key, subQuery); return "OutputRequestId_NotInScopeSubQuery_TOutputRequest." + key;
        }

        public BsTOutputCommonCQ AddOrderBy_OutputRequestId_Asc() { regOBA("OUTPUT_REQUEST_ID");return this; }
        public BsTOutputCommonCQ AddOrderBy_OutputRequestId_Desc() { regOBD("OUTPUT_REQUEST_ID");return this; }

        protected ConditionValue _wbSettingCode;
        public ConditionValue WbSettingCode {
            get { if (_wbSettingCode == null) { _wbSettingCode = new ConditionValue(); } return _wbSettingCode; }
        }
        protected override ConditionValue getCValueWbSettingCode() { return this.WbSettingCode; }


        public BsTOutputCommonCQ AddOrderBy_WbSettingCode_Asc() { regOBA("WB_SETTING_CODE");return this; }
        public BsTOutputCommonCQ AddOrderBy_WbSettingCode_Desc() { regOBD("WB_SETTING_CODE");return this; }

        protected ConditionValue _noanswerVisibleCode;
        public ConditionValue NoanswerVisibleCode {
            get { if (_noanswerVisibleCode == null) { _noanswerVisibleCode = new ConditionValue(); } return _noanswerVisibleCode; }
        }
        protected override ConditionValue getCValueNoanswerVisibleCode() { return this.NoanswerVisibleCode; }


        public BsTOutputCommonCQ AddOrderBy_NoanswerVisibleCode_Asc() { regOBA("NOANSWER_VISIBLE_CODE");return this; }
        public BsTOutputCommonCQ AddOrderBy_NoanswerVisibleCode_Desc() { regOBD("NOANSWER_VISIBLE_CODE");return this; }

        protected ConditionValue _unmatchVisibleCode;
        public ConditionValue UnmatchVisibleCode {
            get { if (_unmatchVisibleCode == null) { _unmatchVisibleCode = new ConditionValue(); } return _unmatchVisibleCode; }
        }
        protected override ConditionValue getCValueUnmatchVisibleCode() { return this.UnmatchVisibleCode; }


        public BsTOutputCommonCQ AddOrderBy_UnmatchVisibleCode_Asc() { regOBA("UNMATCH_VISIBLE_CODE");return this; }
        public BsTOutputCommonCQ AddOrderBy_UnmatchVisibleCode_Desc() { regOBD("UNMATCH_VISIBLE_CODE");return this; }

        public BsTOutputCommonCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTOutputCommonCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TOutputCommonCQ baseQuery = (TOutputCommonCQ)baseQueryAsSuper;
            TOutputCommonCQ unionQuery = (TOutputCommonCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTOutputRequest()) {
                unionQuery.QueryTOutputRequest().reflectRelationOnUnionQuery(baseQuery.QueryTOutputRequest(), unionQuery.QueryTOutputRequest());
            }
            if (baseQuery.hasConditionQueryTOutputSubGt()) {
                unionQuery.QueryTOutputSubGt().reflectRelationOnUnionQuery(baseQuery.QueryTOutputSubGt(), unionQuery.QueryTOutputSubGt());
            }
            if (baseQuery.hasConditionQueryTOutputSubCross()) {
                unionQuery.QueryTOutputSubCross().reflectRelationOnUnionQuery(baseQuery.QueryTOutputSubCross(), unionQuery.QueryTOutputSubCross());
            }
            if (baseQuery.hasConditionQueryTOutputSubFa()) {
                unionQuery.QueryTOutputSubFa().reflectRelationOnUnionQuery(baseQuery.QueryTOutputSubFa(), unionQuery.QueryTOutputSubFa());
            }
            if (baseQuery.hasConditionQueryTOutputSubCklist()) {
                unionQuery.QueryTOutputSubCklist().reflectRelationOnUnionQuery(baseQuery.QueryTOutputSubCklist(), unionQuery.QueryTOutputSubCklist());
            }

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
            joinOnMap.put("OUTPUT_REQUEST_ID", "OUTPUT_REQUEST_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTOutputRequest() {
            return resolveNextRelationPath("T_OUTPUT_COMMON", "tOutputRequest");
        }
        public bool hasConditionQueryTOutputRequest() {
            return _conditionQueryTOutputRequest != null;
        }
        protected TOutputSubGtCQ _conditionQueryTOutputSubGt;
        public TOutputSubGtCQ QueryTOutputSubGt() {
            return this.ConditionQueryTOutputSubGt;
        }
        public TOutputSubGtCQ ConditionQueryTOutputSubGt {
            get {
                if (_conditionQueryTOutputSubGt == null) {
                    _conditionQueryTOutputSubGt = xcreateQueryTOutputSubGt();
                    xsetupOuterJoin_TOutputSubGt();
                }
                return _conditionQueryTOutputSubGt;
            }
        }
        protected TOutputSubGtCQ xcreateQueryTOutputSubGt() {
            String nrp = resolveNextRelationPathTOutputSubGt();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TOutputSubGtCQ cq = new TOutputSubGtCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tOutputSubGt"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TOutputSubGt() {
            TOutputSubGtCQ cq = ConditionQueryTOutputSubGt;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("OUTPUT_COMMON_ID", "Output_Common_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTOutputSubGt() {
            return resolveNextRelationPath("T_OUTPUT_COMMON", "tOutputSubGt");
        }
        public bool hasConditionQueryTOutputSubGt() {
            return _conditionQueryTOutputSubGt != null;
        }
        protected TOutputSubCrossCQ _conditionQueryTOutputSubCross;
        public TOutputSubCrossCQ QueryTOutputSubCross() {
            return this.ConditionQueryTOutputSubCross;
        }
        public TOutputSubCrossCQ ConditionQueryTOutputSubCross {
            get {
                if (_conditionQueryTOutputSubCross == null) {
                    _conditionQueryTOutputSubCross = xcreateQueryTOutputSubCross();
                    xsetupOuterJoin_TOutputSubCross();
                }
                return _conditionQueryTOutputSubCross;
            }
        }
        protected TOutputSubCrossCQ xcreateQueryTOutputSubCross() {
            String nrp = resolveNextRelationPathTOutputSubCross();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TOutputSubCrossCQ cq = new TOutputSubCrossCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tOutputSubCross"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TOutputSubCross() {
            TOutputSubCrossCQ cq = ConditionQueryTOutputSubCross;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("OUTPUT_COMMON_ID", "Output_Common_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTOutputSubCross() {
            return resolveNextRelationPath("T_OUTPUT_COMMON", "tOutputSubCross");
        }
        public bool hasConditionQueryTOutputSubCross() {
            return _conditionQueryTOutputSubCross != null;
        }
        protected TOutputSubFaCQ _conditionQueryTOutputSubFa;
        public TOutputSubFaCQ QueryTOutputSubFa() {
            return this.ConditionQueryTOutputSubFa;
        }
        public TOutputSubFaCQ ConditionQueryTOutputSubFa {
            get {
                if (_conditionQueryTOutputSubFa == null) {
                    _conditionQueryTOutputSubFa = xcreateQueryTOutputSubFa();
                    xsetupOuterJoin_TOutputSubFa();
                }
                return _conditionQueryTOutputSubFa;
            }
        }
        protected TOutputSubFaCQ xcreateQueryTOutputSubFa() {
            String nrp = resolveNextRelationPathTOutputSubFa();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TOutputSubFaCQ cq = new TOutputSubFaCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tOutputSubFa"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TOutputSubFa() {
            TOutputSubFaCQ cq = ConditionQueryTOutputSubFa;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("OUTPUT_COMMON_ID", "Output_Common_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTOutputSubFa() {
            return resolveNextRelationPath("T_OUTPUT_COMMON", "tOutputSubFa");
        }
        public bool hasConditionQueryTOutputSubFa() {
            return _conditionQueryTOutputSubFa != null;
        }
        protected TOutputSubCklistCQ _conditionQueryTOutputSubCklist;
        public TOutputSubCklistCQ QueryTOutputSubCklist() {
            return this.ConditionQueryTOutputSubCklist;
        }
        public TOutputSubCklistCQ ConditionQueryTOutputSubCklist {
            get {
                if (_conditionQueryTOutputSubCklist == null) {
                    _conditionQueryTOutputSubCklist = xcreateQueryTOutputSubCklist();
                    xsetupOuterJoin_TOutputSubCklist();
                }
                return _conditionQueryTOutputSubCklist;
            }
        }
        protected TOutputSubCklistCQ xcreateQueryTOutputSubCklist() {
            String nrp = resolveNextRelationPathTOutputSubCklist();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TOutputSubCklistCQ cq = new TOutputSubCklistCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tOutputSubCklist"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TOutputSubCklist() {
            TOutputSubCklistCQ cq = ConditionQueryTOutputSubCklist;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("OUTPUT_COMMON_ID", "Output_Common_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTOutputSubCklist() {
            return resolveNextRelationPath("T_OUTPUT_COMMON", "tOutputSubCklist");
        }
        public bool hasConditionQueryTOutputSubCklist() {
            return _conditionQueryTOutputSubCklist != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TOutputCommonCQ> _scalarSubQueryMap;
	    public Map<String, TOutputCommonCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TOutputCommonCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TOutputCommonCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TOutputCommonCQ> _myselfInScopeSubQueryMap;
        public Map<String, TOutputCommonCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TOutputCommonCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TOutputCommonCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
