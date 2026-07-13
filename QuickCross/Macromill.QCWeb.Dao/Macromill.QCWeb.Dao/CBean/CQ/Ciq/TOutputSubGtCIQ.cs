
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
    public class TOutputSubGtCIQ : AbstractBsTOutputSubGtCQ {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected BsTOutputSubGtCQ _myCQ;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TOutputSubGtCIQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel, BsTOutputSubGtCQ myCQ)
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


        protected override ConditionValue getCValueOutputSubGtId() {
            return _myCQ.OutputSubGtId;
        }


        protected override ConditionValue getCValueOutputCommonId() {
            return _myCQ.OutputCommonId;
        }


        public override String keepOutputCommonId_InScopeSubQuery_TOutputCommon(TOutputCommonCQ subQuery) {
            return _myCQ.keepOutputCommonId_InScopeSubQuery_TOutputCommon(subQuery);
        }

        public override String keepOutputCommonId_NotInScopeSubQuery_TOutputCommon(TOutputCommonCQ subQuery) {
            return _myCQ.keepOutputCommonId_NotInScopeSubQuery_TOutputCommon(subQuery);
        }

        protected override ConditionValue getCValueOutputTableType() {
            return _myCQ.OutputTableType;
        }


        protected override ConditionValue getCValueOutputTableOrientation() {
            return _myCQ.OutputTableOrientation;
        }


        protected override ConditionValue getCValuePageSettingTableType() {
            return _myCQ.PageSettingTableType;
        }


        protected override ConditionValue getCValuePageSettingPaperSize() {
            return _myCQ.PageSettingPaperSize;
        }


        protected override ConditionValue getCValuePageSettingPaperOrientation() {
            return _myCQ.PageSettingPaperOrientation;
        }


        protected override ConditionValue getCValueMarkingLevel() {
            return _myCQ.MarkingLevel;
        }


        protected override ConditionValue getCValueMarkingMinParameter() {
            return _myCQ.MarkingMinParameter;
        }


        protected override ConditionValue getCValueMarkingCode() {
            return _myCQ.MarkingCode;
        }


        protected override ConditionValue getCValueFilteringExpression() {
            return _myCQ.FilteringExpression;
        }


        // ===================================================================================
        //                                                                     Scalar SubQuery
        //                                                                     ===============
        public override String keepScalarSubQuery(TOutputSubGtCQ subQuery) {
            throw new UnsupportedOperationException("ScalarSubQuery at inline() is unsupported! Sorry!");
        }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        public override String keepMyselfInScopeSubQuery(TOutputSubGtCQ subQuery) {
            throw new UnsupportedOperationException("MyselfInScopeSubQuery at inline() is unsupported! Sorry!");
        }
    }
}
