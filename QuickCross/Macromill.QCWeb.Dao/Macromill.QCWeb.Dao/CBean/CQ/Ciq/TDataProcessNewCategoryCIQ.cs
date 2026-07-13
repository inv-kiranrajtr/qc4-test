
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
    public class TDataProcessNewCategoryCIQ : AbstractBsTDataProcessNewCategoryCQ {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected BsTDataProcessNewCategoryCQ _myCQ;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TDataProcessNewCategoryCIQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel, BsTDataProcessNewCategoryCQ myCQ)
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


        protected override ConditionValue getCValueDataProcessNewCategoryId() {
            return _myCQ.DataProcessNewCategoryId;
        }


        protected override ConditionValue getCValueNewCategoryNo() {
            return _myCQ.NewCategoryNo;
        }


        protected override ConditionValue getCValueNewCategoryName() {
            return _myCQ.NewCategoryName;
        }


        protected override ConditionValue getCValueSrcItemId() {
            return _myCQ.SrcItemId;
        }


        protected override ConditionValue getCValueOperationCode() {
            return _myCQ.OperationCode;
        }


        protected override ConditionValue getCValueConditionString() {
            return _myCQ.ConditionString;
        }


        protected override ConditionValue getCValueBottomValue() {
            return _myCQ.BottomValue;
        }


        protected override ConditionValue getCValueUpperValue() {
            return _myCQ.UpperValue;
        }


        protected override ConditionValue getCValueDataEditId() {
            return _myCQ.DataEditId;
        }


        public override String keepDataEditId_InScopeSubQuery_TDataProcessNewItem(TDataProcessNewItemCQ subQuery) {
            return _myCQ.keepDataEditId_InScopeSubQuery_TDataProcessNewItem(subQuery);
        }

        public override String keepDataEditId_NotInScopeSubQuery_TDataProcessNewItem(TDataProcessNewItemCQ subQuery) {
            return _myCQ.keepDataEditId_NotInScopeSubQuery_TDataProcessNewItem(subQuery);
        }

        // ===================================================================================
        //                                                                     Scalar SubQuery
        //                                                                     ===============
        public override String keepScalarSubQuery(TDataProcessNewCategoryCQ subQuery) {
            throw new UnsupportedOperationException("ScalarSubQuery at inline() is unsupported! Sorry!");
        }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        public override String keepMyselfInScopeSubQuery(TDataProcessNewCategoryCQ subQuery) {
            throw new UnsupportedOperationException("MyselfInScopeSubQuery at inline() is unsupported! Sorry!");
        }
    }
}
