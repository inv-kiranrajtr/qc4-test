
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
    public class TEditDataCIQ : AbstractBsTEditDataCQ {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected BsTEditDataCQ _myCQ;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TEditDataCIQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel, BsTEditDataCQ myCQ)
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


        protected override ConditionValue getCValueDataEditId() {
            return _myCQ.DataEditId;
        }


        public override String keepDataEditId_ExistsSubQuery_TEditConditionList(TEditConditionCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepDataEditId_ExistsSubQuery_TEditConditionList(subQuery);
        }

        public override String keepDataEditId_ExistsSubQuery_TEditTargetItemList(TEditTargetItemCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepDataEditId_ExistsSubQuery_TEditTargetItemList(subQuery);
        }

        public override String keepDataEditId_NotExistsSubQuery_TEditConditionList(TEditConditionCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepDataEditId_NotExistsSubQuery_TEditConditionList(subQuery);
        }

        public override String keepDataEditId_NotExistsSubQuery_TEditTargetItemList(TEditTargetItemCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepDataEditId_NotExistsSubQuery_TEditTargetItemList(subQuery);
        }

        public override String keepDataEditId_InScopeSubQuery_TDataEditList(TDataEditListCQ subQuery) {
            return _myCQ.keepDataEditId_InScopeSubQuery_TDataEditList(subQuery);
        }

        public override String keepDataEditId_InScopeSubQuery_TEditConditionList(TEditConditionCQ subQuery) {
            return _myCQ.keepDataEditId_InScopeSubQuery_TEditConditionList(subQuery);
        }

        public override String keepDataEditId_InScopeSubQuery_TEditTargetItemList(TEditTargetItemCQ subQuery) {
            return _myCQ.keepDataEditId_InScopeSubQuery_TEditTargetItemList(subQuery);
        }

        public override String keepDataEditId_NotInScopeSubQuery_TDataEditList(TDataEditListCQ subQuery) {
            return _myCQ.keepDataEditId_NotInScopeSubQuery_TDataEditList(subQuery);
        }

        public override String keepDataEditId_NotInScopeSubQuery_TEditConditionList(TEditConditionCQ subQuery) {
            return _myCQ.keepDataEditId_NotInScopeSubQuery_TEditConditionList(subQuery);
        }

        public override String keepDataEditId_NotInScopeSubQuery_TEditTargetItemList(TEditTargetItemCQ subQuery) {
            return _myCQ.keepDataEditId_NotInScopeSubQuery_TEditTargetItemList(subQuery);
        }
        public override String keepDataEditId_SpecifyDerivedReferrer_TEditConditionList(TEditConditionCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepDataEditId_SpecifyDerivedReferrer_TEditTargetItemList(TEditTargetItemCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepDataEditId_QueryDerivedReferrer_TEditConditionList(TEditConditionCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepDataEditId_QueryDerivedReferrer_TEditConditionListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepDataEditId_QueryDerivedReferrer_TEditTargetItemList(TEditTargetItemCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepDataEditId_QueryDerivedReferrer_TEditTargetItemListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }

        protected override ConditionValue getCValueConditionFlag() {
            return _myCQ.ConditionFlag;
        }


        protected override ConditionValue getCValueEditMethod() {
            return _myCQ.EditMethod;
        }


        protected override ConditionValue getCValueEditValueType() {
            return _myCQ.EditValueType;
        }


        protected override ConditionValue getCValueEditValue() {
            return _myCQ.EditValue;
        }


        protected override ConditionValue getCValueConditionDiv() {
            return _myCQ.ConditionDiv;
        }


        // ===================================================================================
        //                                                                     Scalar SubQuery
        //                                                                     ===============
        public override String keepScalarSubQuery(TEditDataCQ subQuery) {
            throw new UnsupportedOperationException("ScalarSubQuery at inline() is unsupported! Sorry!");
        }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        public override String keepMyselfInScopeSubQuery(TEditDataCQ subQuery) {
            throw new UnsupportedOperationException("MyselfInScopeSubQuery at inline() is unsupported! Sorry!");
        }
    }
}
