
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
    public class TDefaultEnvColorInfoCCIQ : AbstractBsTDefaultEnvColorInfoCCQ {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected BsTDefaultEnvColorInfoCCQ _myCQ;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TDefaultEnvColorInfoCCIQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel, BsTDefaultEnvColorInfoCCQ myCQ)
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


        protected override ConditionValue getCValueDefEnvColorInfoCId() {
            return _myCQ.DefEnvColorInfoCId;
        }


        public override String keepDefEnvColorInfoCId_ExistsSubQuery_TDefaultEnvColorDtlCList(TDefaultEnvColorDtlCCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepDefEnvColorInfoCId_ExistsSubQuery_TDefaultEnvColorDtlCList(subQuery);
        }

        public override String keepDefEnvColorInfoCId_NotExistsSubQuery_TDefaultEnvColorDtlCList(TDefaultEnvColorDtlCCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepDefEnvColorInfoCId_NotExistsSubQuery_TDefaultEnvColorDtlCList(subQuery);
        }

        public override String keepDefEnvColorInfoCId_InScopeSubQuery_TDefaultEnvColorDtlCList(TDefaultEnvColorDtlCCQ subQuery) {
            return _myCQ.keepDefEnvColorInfoCId_InScopeSubQuery_TDefaultEnvColorDtlCList(subQuery);
        }

        public override String keepDefEnvColorInfoCId_NotInScopeSubQuery_TDefaultEnvColorDtlCList(TDefaultEnvColorDtlCCQ subQuery) {
            return _myCQ.keepDefEnvColorInfoCId_NotInScopeSubQuery_TDefaultEnvColorDtlCList(subQuery);
        }
        public override String keepDefEnvColorInfoCId_SpecifyDerivedReferrer_TDefaultEnvColorDtlCList(TDefaultEnvColorDtlCCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepDefEnvColorInfoCId_QueryDerivedReferrer_TDefaultEnvColorDtlCList(TDefaultEnvColorDtlCCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepDefEnvColorInfoCId_QueryDerivedReferrer_TDefaultEnvColorDtlCListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }

        protected override ConditionValue getCValueLanguage() {
            return _myCQ.Language;
        }


        public override String keepLanguage_InScopeSubQuery_TDefaultEnvBase(TDefaultEnvBaseCQ subQuery) {
            return _myCQ.keepLanguage_InScopeSubQuery_TDefaultEnvBase(subQuery);
        }

        public override String keepLanguage_NotInScopeSubQuery_TDefaultEnvBase(TDefaultEnvBaseCQ subQuery) {
            return _myCQ.keepLanguage_NotInScopeSubQuery_TDefaultEnvBase(subQuery);
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
        public override String keepScalarSubQuery(TDefaultEnvColorInfoCCQ subQuery) {
            throw new UnsupportedOperationException("ScalarSubQuery at inline() is unsupported! Sorry!");
        }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        public override String keepMyselfInScopeSubQuery(TDefaultEnvColorInfoCCQ subQuery) {
            throw new UnsupportedOperationException("MyselfInScopeSubQuery at inline() is unsupported! Sorry!");
        }
    }
}
