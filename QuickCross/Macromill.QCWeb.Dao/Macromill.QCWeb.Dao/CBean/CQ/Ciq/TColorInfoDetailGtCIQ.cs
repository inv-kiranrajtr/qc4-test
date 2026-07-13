
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
    public class TColorInfoDetailGtCIQ : AbstractBsTColorInfoDetailGtCQ {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected BsTColorInfoDetailGtCQ _myCQ;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TColorInfoDetailGtCIQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel, BsTColorInfoDetailGtCQ myCQ)
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


        protected override ConditionValue getCValueColorInfoDetailGtId() {
            return _myCQ.ColorInfoDetailGtId;
        }


        protected override ConditionValue getCValueGraphColorNo() {
            return _myCQ.GraphColorNo;
        }


        protected override ConditionValue getCValueColorCode() {
            return _myCQ.ColorCode;
        }


        protected override ConditionValue getCValuePatternCode() {
            return _myCQ.PatternCode;
        }


        protected override ConditionValue getCValueColorSetInfoGtId() {
            return _myCQ.ColorSetInfoGtId;
        }


        public override String keepColorSetInfoGtId_InScopeSubQuery_TColorSetInfoGt(TColorSetInfoGtCQ subQuery) {
            return _myCQ.keepColorSetInfoGtId_InScopeSubQuery_TColorSetInfoGt(subQuery);
        }

        public override String keepColorSetInfoGtId_NotInScopeSubQuery_TColorSetInfoGt(TColorSetInfoGtCQ subQuery) {
            return _myCQ.keepColorSetInfoGtId_NotInScopeSubQuery_TColorSetInfoGt(subQuery);
        }

        // ===================================================================================
        //                                                                     Scalar SubQuery
        //                                                                     ===============
        public override String keepScalarSubQuery(TColorInfoDetailGtCQ subQuery) {
            throw new UnsupportedOperationException("ScalarSubQuery at inline() is unsupported! Sorry!");
        }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        public override String keepMyselfInScopeSubQuery(TColorInfoDetailGtCQ subQuery) {
            throw new UnsupportedOperationException("MyselfInScopeSubQuery at inline() is unsupported! Sorry!");
        }
    }
}
