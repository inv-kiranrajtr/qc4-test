
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CKey;
using Macromill.QCWeb.Dao.AllCommon.CBean.COption;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ.BS;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.CQ.Ciq {

    [System.Serializable]
    public class TDefaultEnvColorInfoCIQ : AbstractBsTDefaultEnvColorInfoCQ {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected BsTDefaultEnvColorInfoCQ _myCQ;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TDefaultEnvColorInfoCIQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel, BsTDefaultEnvColorInfoCQ myCQ)
            : base(childQuery, sqlClause, aliasName, nestLevel) {
            _myCQ = myCQ;
            _foreignPropertyName = _myCQ.xgetForeignPropertyName();// Accept foreign property name.
            _relationPath = _myCQ.xgetRelationPath();// Accept relation path.
        }

        // ===================================================================================
        //                                                             Override about Register
        //                                                             =======================
        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            throw new UnsupportedOperationException("InlineQuery must not need UNION method: " + baseQueryAsSuper + " : " + unionQueryAsSuper);
        }
    
        protected override void setupConditionValueAndRegisterWhereClause(ConditionKey key, Object value, ConditionValue cvalue, String colName) {
            regIQ(key, value, cvalue, colName);
        }
    
        protected override void setupConditionValueAndRegisterWhereClause(ConditionKey key, Object value, ConditionValue cvalue
                                                                        , String colName, ConditionOption option) {
            regIQ(key, value, cvalue, colName, option);
        }
    
        protected override void registerWhereClause(String whereClause) {
            registerInlineWhereClause(whereClause);
        }
    
        protected override String getInScopeSubQueryRealColumnName(String columnName) {
            if (_onClause) {
                throw new UnsupportedOperationException("InScopeSubQuery of on-clause is unsupported");
            }
            return _onClause ? xgetAliasName() + "." + columnName : columnName;
        }
    
        protected override void registerExistsSubQuery(ConditionQuery subQuery
                                     , String columnName, String relatedColumnName, String propertyName) {
            throw new UnsupportedOperationException("Sorry! ExistsSubQuery at inline view is unsupported. So please use InScopeSubQyery.");
        }


        protected override ConditionValue getCValueDefEnvColorInfoId() {
            return _myCQ.DefEnvColorInfoId;
        }


        public override String keepDefEnvColorInfoId_ExistsSubQuery_TDefaultEnvColorDtlList(TDefaultEnvColorDtlCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepDefEnvColorInfoId_ExistsSubQuery_TDefaultEnvColorDtlList(subQuery);
        }

        public override String keepDefEnvColorInfoId_NotExistsSubQuery_TDefaultEnvColorDtlList(TDefaultEnvColorDtlCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepDefEnvColorInfoId_NotExistsSubQuery_TDefaultEnvColorDtlList(subQuery);
        }

        public override String keepDefEnvColorInfoId_InScopeSubQuery_TDefaultEnvColorDtl(TDefaultEnvColorDtlCQ subQuery) {
            return _myCQ.keepDefEnvColorInfoId_InScopeSubQuery_TDefaultEnvColorDtl(subQuery);
        }

        public override String keepDefEnvColorInfoId_InScopeSubQuery_TDefaultEnvColorDtlList(TDefaultEnvColorDtlCQ subQuery) {
            return _myCQ.keepDefEnvColorInfoId_InScopeSubQuery_TDefaultEnvColorDtlList(subQuery);
        }

        public override String keepDefEnvColorInfoId_NotInScopeSubQuery_TDefaultEnvColorDtl(TDefaultEnvColorDtlCQ subQuery) {
            return _myCQ.keepDefEnvColorInfoId_NotInScopeSubQuery_TDefaultEnvColorDtl(subQuery);
        }

        public override String keepDefEnvColorInfoId_NotInScopeSubQuery_TDefaultEnvColorDtlList(TDefaultEnvColorDtlCQ subQuery) {
            return _myCQ.keepDefEnvColorInfoId_NotInScopeSubQuery_TDefaultEnvColorDtlList(subQuery);
        }
        public override String keepDefEnvColorInfoId_SpecifyDerivedReferrer_TDefaultEnvColorDtlList(TDefaultEnvColorDtlCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepDefEnvColorInfoId_QueryDerivedReferrer_TDefaultEnvColorDtlList(TDefaultEnvColorDtlCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepDefEnvColorInfoId_QueryDerivedReferrer_TDefaultEnvColorDtlListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }

        protected override ConditionValue getCValueQcwebid() {
            return _myCQ.Qcwebid;
        }


        public override String keepQcwebid_InScopeSubQuery_TDefaultEnv(TDefaultEnvCQ subQuery) {
            return _myCQ.keepQcwebid_InScopeSubQuery_TDefaultEnv(subQuery);
        }

        public override String keepQcwebid_NotInScopeSubQuery_TDefaultEnv(TDefaultEnvCQ subQuery) {
            return _myCQ.keepQcwebid_NotInScopeSubQuery_TDefaultEnv(subQuery);
        }

        protected override ConditionValue getCValueTypeCode() {
            return _myCQ.TypeCode;
        }


        protected override ConditionValue getCValueGradationType() {
            return _myCQ.GradationType;
        }


        // ===================================================================================
        //                                                                     Scalar SubQuery
        //                                                                     ===============
        public override String keepScalarSubQuery(TDefaultEnvColorInfoCQ subQuery) {
            throw new UnsupportedOperationException("ScalarSubQuery at inline() is unsupported! Sorry!");
        }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        public override String keepMyselfInScopeSubQuery(TDefaultEnvColorInfoCQ subQuery) {
            throw new UnsupportedOperationException("MyselfInScopeSubQuery at inline() is unsupported! Sorry!");
        }
    }
}
