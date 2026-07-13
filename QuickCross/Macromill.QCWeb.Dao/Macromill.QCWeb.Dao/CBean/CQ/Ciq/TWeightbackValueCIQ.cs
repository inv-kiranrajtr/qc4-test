
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
    public class TWeightbackValueCIQ : AbstractBsTWeightbackValueCQ {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected BsTWeightbackValueCQ _myCQ;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TWeightbackValueCIQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel, BsTWeightbackValueCQ myCQ)
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


        protected override ConditionValue getCValueWeightbackValueId() {
            return _myCQ.WeightbackValueId;
        }


        protected override ConditionValue getCValueWeightbackItemNo() {
            return _myCQ.WeightbackItemNo;
        }


        protected override ConditionValue getCValuePercentValue() {
            return _myCQ.PercentValue;
        }


        protected override ConditionValue getCValueParameterNValue() {
            return _myCQ.ParameterNValue;
        }


        protected override ConditionValue getCValueWeightbackValue() {
            return _myCQ.WeightbackValue;
        }


        protected override ConditionValue getCValueWeightbackId() {
            return _myCQ.WeightbackId;
        }


        public override String keepWeightbackId_InScopeSubQuery_TWeightback(TWeightbackCQ subQuery) {
            return _myCQ.keepWeightbackId_InScopeSubQuery_TWeightback(subQuery);
        }

        public override String keepWeightbackId_NotInScopeSubQuery_TWeightback(TWeightbackCQ subQuery) {
            return _myCQ.keepWeightbackId_NotInScopeSubQuery_TWeightback(subQuery);
        }

        // ===================================================================================
        //                                                                     Scalar SubQuery
        //                                                                     ===============
        public override String keepScalarSubQuery(TWeightbackValueCQ subQuery) {
            throw new UnsupportedOperationException("ScalarSubQuery at inline() is unsupported! Sorry!");
        }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        public override String keepMyselfInScopeSubQuery(TWeightbackValueCQ subQuery) {
            throw new UnsupportedOperationException("MyselfInScopeSubQuery at inline() is unsupported! Sorry!");
        }
    }
}
