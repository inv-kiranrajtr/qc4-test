
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
    public class TWeightbackCIQ : AbstractBsTWeightbackCQ {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected BsTWeightbackCQ _myCQ;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TWeightbackCIQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel, BsTWeightbackCQ myCQ)
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


        protected override ConditionValue getCValueWeightbackId() {
            return _myCQ.WeightbackId;
        }


        public override String keepWeightbackId_ExistsSubQuery_TWeightbackValueList(TWeightbackValueCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepWeightbackId_ExistsSubQuery_TWeightbackValueList(subQuery);
        }

        public override String keepWeightbackId_NotExistsSubQuery_TWeightbackValueList(TWeightbackValueCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepWeightbackId_NotExistsSubQuery_TWeightbackValueList(subQuery);
        }

        public override String keepWeightbackId_InScopeSubQuery_TWeightbackValue(TWeightbackValueCQ subQuery) {
            return _myCQ.keepWeightbackId_InScopeSubQuery_TWeightbackValue(subQuery);
        }

        public override String keepWeightbackId_InScopeSubQuery_TWeightbackValueList(TWeightbackValueCQ subQuery) {
            return _myCQ.keepWeightbackId_InScopeSubQuery_TWeightbackValueList(subQuery);
        }

        public override String keepWeightbackId_NotInScopeSubQuery_TWeightbackValue(TWeightbackValueCQ subQuery) {
            return _myCQ.keepWeightbackId_NotInScopeSubQuery_TWeightbackValue(subQuery);
        }

        public override String keepWeightbackId_NotInScopeSubQuery_TWeightbackValueList(TWeightbackValueCQ subQuery) {
            return _myCQ.keepWeightbackId_NotInScopeSubQuery_TWeightbackValueList(subQuery);
        }
        public override String keepWeightbackId_SpecifyDerivedReferrer_TWeightbackValueList(TWeightbackValueCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepWeightbackId_QueryDerivedReferrer_TWeightbackValueList(TWeightbackValueCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepWeightbackId_QueryDerivedReferrer_TWeightbackValueListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }

        protected override ConditionValue getCValueWeightbackItemId() {
            return _myCQ.WeightbackItemId;
        }


        protected override ConditionValue getCValueAssistCalcFlag() {
            return _myCQ.AssistCalcFlag;
        }


        protected override ConditionValue getCValueAssistCalcType() {
            return _myCQ.AssistCalcType;
        }


        protected override ConditionValue getCValueQcwebid() {
            return _myCQ.Qcwebid;
        }


        public override String keepQcwebid_InScopeSubQuery_TQcwebSurveyInfo(TQcwebSurveyInfoCQ subQuery) {
            return _myCQ.keepQcwebid_InScopeSubQuery_TQcwebSurveyInfo(subQuery);
        }

        public override String keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfo(TQcwebSurveyInfoCQ subQuery) {
            return _myCQ.keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfo(subQuery);
        }

        protected override ConditionValue getCValueLastUpdateUser() {
            return _myCQ.LastUpdateUser;
        }


        protected override ConditionValue getCValueLastUpdateDatetime() {
            return _myCQ.LastUpdateDatetime;
        }


        // ===================================================================================
        //                                                                     Scalar SubQuery
        //                                                                     ===============
        public override String keepScalarSubQuery(TWeightbackCQ subQuery) {
            throw new UnsupportedOperationException("ScalarSubQuery at inline() is unsupported! Sorry!");
        }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        public override String keepMyselfInScopeSubQuery(TWeightbackCQ subQuery) {
            throw new UnsupportedOperationException("MyselfInScopeSubQuery at inline() is unsupported! Sorry!");
        }
    }
}
