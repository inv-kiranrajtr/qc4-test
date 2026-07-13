
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
    public class TTableControlCIQ : AbstractBsTTableControlCQ {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected BsTTableControlCQ _myCQ;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TTableControlCIQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel, BsTTableControlCQ myCQ)
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


        protected override ConditionValue getCValueQcwebid() {
            return _myCQ.Qcwebid;
        }


        public override String keepQcwebid_ExistsSubQuery_TTableDetailInfoList(TTableDetailInfoCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_ExistsSubQuery_TTableDetailInfoList(subQuery);
        }

        public override String keepQcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOne(subQuery);
        }

        public override String keepQcwebid_ExistsSubQuery_TItemInfoList(TItemInfoCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_ExistsSubQuery_TItemInfoList(subQuery);
        }

        public override String keepQcwebid_NotExistsSubQuery_TTableDetailInfoList(TTableDetailInfoCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_NotExistsSubQuery_TTableDetailInfoList(subQuery);
        }

        public override String keepQcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOne(subQuery);
        }

        public override String keepQcwebid_NotExistsSubQuery_TItemInfoList(TItemInfoCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_NotExistsSubQuery_TItemInfoList(subQuery);
        }

        public override String keepQcwebid_InScopeSubQuery_TTableDetailInfoList(TTableDetailInfoCQ subQuery) {
            return _myCQ.keepQcwebid_InScopeSubQuery_TTableDetailInfoList(subQuery);
        }

        public override String keepQcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery) {
            return _myCQ.keepQcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOne(subQuery);
        }

        public override String keepQcwebid_InScopeSubQuery_TItemInfoList(TItemInfoCQ subQuery) {
            return _myCQ.keepQcwebid_InScopeSubQuery_TItemInfoList(subQuery);
        }

        public override String keepQcwebid_NotInScopeSubQuery_TTableDetailInfoList(TTableDetailInfoCQ subQuery) {
            return _myCQ.keepQcwebid_NotInScopeSubQuery_TTableDetailInfoList(subQuery);
        }

        public override String keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery) {
            return _myCQ.keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOne(subQuery);
        }

        public override String keepQcwebid_NotInScopeSubQuery_TItemInfoList(TItemInfoCQ subQuery) {
            return _myCQ.keepQcwebid_NotInScopeSubQuery_TItemInfoList(subQuery);
        }
        public override String keepQcwebid_SpecifyDerivedReferrer_TTableDetailInfoList(TTableDetailInfoCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_SpecifyDerivedReferrer_TItemInfoList(TItemInfoCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_QueryDerivedReferrer_TTableDetailInfoList(TTableDetailInfoCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_QueryDerivedReferrer_TTableDetailInfoListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_QueryDerivedReferrer_TItemInfoList(TItemInfoCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_QueryDerivedReferrer_TItemInfoListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }

        protected override ConditionValue getCValueBaseTableName() {
            return _myCQ.BaseTableName;
        }


        protected override ConditionValue getCValueActiveTableNo() {
            return _myCQ.ActiveTableNo;
        }


        protected override ConditionValue getCValueMaxNo() {
            return _myCQ.MaxNo;
        }


        // ===================================================================================
        //                                                                     Scalar SubQuery
        //                                                                     ===============
        public override String keepScalarSubQuery(TTableControlCQ subQuery) {
            throw new UnsupportedOperationException("ScalarSubQuery at inline() is unsupported! Sorry!");
        }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        public override String keepMyselfInScopeSubQuery(TTableControlCQ subQuery) {
            throw new UnsupportedOperationException("MyselfInScopeSubQuery at inline() is unsupported! Sorry!");
        }
    }
}
