
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
    public class TScenarioQuerylistCIQ : AbstractBsTScenarioQuerylistCQ {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected BsTScenarioQuerylistCQ _myCQ;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TScenarioQuerylistCIQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel, BsTScenarioQuerylistCQ myCQ)
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


        protected override ConditionValue getCValueScenarioQuerylistId() {
            return _myCQ.ScenarioQuerylistId;
        }


        protected override ConditionValue getCValueScenarioTotalizationId() {
            return _myCQ.ScenarioTotalizationId;
        }


        public override String keepScenarioTotalizationId_InScopeSubQuery_TScenarioTotalization(TScenarioTotalizationCQ subQuery) {
            return _myCQ.keepScenarioTotalizationId_InScopeSubQuery_TScenarioTotalization(subQuery);
        }

        public override String keepScenarioTotalizationId_NotInScopeSubQuery_TScenarioTotalization(TScenarioTotalizationCQ subQuery) {
            return _myCQ.keepScenarioTotalizationId_NotInScopeSubQuery_TScenarioTotalization(subQuery);
        }

        protected override ConditionValue getCValueSeqNo() {
            return _myCQ.SeqNo;
        }


        protected override ConditionValue getCValueItemInfoId() {
            return _myCQ.ItemInfoId;
        }


        public override String keepItemInfoId_InScopeSubQuery_TItemInfo(TItemInfoCQ subQuery) {
            return _myCQ.keepItemInfoId_InScopeSubQuery_TItemInfo(subQuery);
        }

        public override String keepItemInfoId_NotInScopeSubQuery_TItemInfo(TItemInfoCQ subQuery) {
            return _myCQ.keepItemInfoId_NotInScopeSubQuery_TItemInfo(subQuery);
        }

        protected override ConditionValue getCValueOperationCode() {
            return _myCQ.OperationCode;
        }


        protected override ConditionValue getCValueConditionString() {
            return _myCQ.ConditionString;
        }


        // ===================================================================================
        //                                                                     Scalar SubQuery
        //                                                                     ===============
        public override String keepScalarSubQuery(TScenarioQuerylistCQ subQuery) {
            throw new UnsupportedOperationException("ScalarSubQuery at inline() is unsupported! Sorry!");
        }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        public override String keepMyselfInScopeSubQuery(TScenarioQuerylistCQ subQuery) {
            throw new UnsupportedOperationException("MyselfInScopeSubQuery at inline() is unsupported! Sorry!");
        }
    }
}
