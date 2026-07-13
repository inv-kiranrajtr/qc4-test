
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTTableDetailInfoCQ : AbstractBsTTableDetailInfoCQ {

        protected TTableDetailInfoCIQ _inlineQuery;

        public BsTTableDetailInfoCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TTableDetailInfoCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TTableDetailInfoCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TTableDetailInfoCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TTableDetailInfoCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _qcwebid;
        public ConditionValue Qcwebid {
            get { if (_qcwebid == null) { _qcwebid = new ConditionValue(); } return _qcwebid; }
        }
        protected override ConditionValue getCValueQcwebid() { return this.Qcwebid; }


        protected Map<String, TTableControlCQ> _qcwebid_InScopeSubQuery_TTableControlMap;
        public Map<String, TTableControlCQ> Qcwebid_InScopeSubQuery_TTableControl { get { return _qcwebid_InScopeSubQuery_TTableControlMap; }}
        public override String keepQcwebid_InScopeSubQuery_TTableControl(TTableControlCQ subQuery) {
            if (_qcwebid_InScopeSubQuery_TTableControlMap == null) { _qcwebid_InScopeSubQuery_TTableControlMap = new LinkedHashMap<String, TTableControlCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_InScopeSubQuery_TTableControlMap.size() + 1);
            _qcwebid_InScopeSubQuery_TTableControlMap.put(key, subQuery); return "Qcwebid_InScopeSubQuery_TTableControl." + key;
        }

        protected Map<String, TTableControlCQ> _qcwebid_NotInScopeSubQuery_TTableControlMap;
        public Map<String, TTableControlCQ> Qcwebid_NotInScopeSubQuery_TTableControl { get { return _qcwebid_NotInScopeSubQuery_TTableControlMap; }}
        public override String keepQcwebid_NotInScopeSubQuery_TTableControl(TTableControlCQ subQuery) {
            if (_qcwebid_NotInScopeSubQuery_TTableControlMap == null) { _qcwebid_NotInScopeSubQuery_TTableControlMap = new LinkedHashMap<String, TTableControlCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotInScopeSubQuery_TTableControlMap.size() + 1);
            _qcwebid_NotInScopeSubQuery_TTableControlMap.put(key, subQuery); return "Qcwebid_NotInScopeSubQuery_TTableControl." + key;
        }

        public BsTTableDetailInfoCQ AddOrderBy_Qcwebid_Asc() { regOBA("QCWEBID");return this; }
        public BsTTableDetailInfoCQ AddOrderBy_Qcwebid_Desc() { regOBD("QCWEBID");return this; }

        protected ConditionValue _tableNo;
        public ConditionValue TableNo {
            get { if (_tableNo == null) { _tableNo = new ConditionValue(); } return _tableNo; }
        }
        protected override ConditionValue getCValueTableNo() { return this.TableNo; }


        public BsTTableDetailInfoCQ AddOrderBy_TableNo_Asc() { regOBA("TABLE_NO");return this; }
        public BsTTableDetailInfoCQ AddOrderBy_TableNo_Desc() { regOBD("TABLE_NO");return this; }

        protected ConditionValue _usedNo;
        public ConditionValue UsedNo {
            get { if (_usedNo == null) { _usedNo = new ConditionValue(); } return _usedNo; }
        }
        protected override ConditionValue getCValueUsedNo() { return this.UsedNo; }


        public BsTTableDetailInfoCQ AddOrderBy_UsedNo_Asc() { regOBA("USED_NO");return this; }
        public BsTTableDetailInfoCQ AddOrderBy_UsedNo_Desc() { regOBD("USED_NO");return this; }

        public BsTTableDetailInfoCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTTableDetailInfoCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TTableDetailInfoCQ baseQuery = (TTableDetailInfoCQ)baseQueryAsSuper;
            TTableDetailInfoCQ unionQuery = (TTableDetailInfoCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTTableControl()) {
                unionQuery.QueryTTableControl().reflectRelationOnUnionQuery(baseQuery.QueryTTableControl(), unionQuery.QueryTTableControl());
            }

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
            joinOnMap.put("QCWEBID", "QCWEBID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTTableControl() {
            return resolveNextRelationPath("T_TABLE_DETAIL_INFO", "tTableControl");
        }
        public bool hasConditionQueryTTableControl() {
            return _conditionQueryTTableControl != null;
        }

    }
}
