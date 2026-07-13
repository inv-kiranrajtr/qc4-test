
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
    public class TDataProcessNewItemCIQ : AbstractBsTDataProcessNewItemCQ {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected BsTDataProcessNewItemCQ _myCQ;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TDataProcessNewItemCIQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel, BsTDataProcessNewItemCQ myCQ)
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


        public override String keepDataEditId_ExistsSubQuery_TDataProcessNewCategoryList(TDataProcessNewCategoryCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepDataEditId_ExistsSubQuery_TDataProcessNewCategoryList(subQuery);
        }

        public override String keepDataEditId_ExistsSubQuery_TDataProcessNewItemSrcList(TDataProcessNewItemSrcCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepDataEditId_ExistsSubQuery_TDataProcessNewItemSrcList(subQuery);
        }

        public override String keepDataEditId_ExistsSubQuery_TIntegConditionList(TIntegConditionCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepDataEditId_ExistsSubQuery_TIntegConditionList(subQuery);
        }

        public override String keepDataEditId_NotExistsSubQuery_TDataProcessNewCategoryList(TDataProcessNewCategoryCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepDataEditId_NotExistsSubQuery_TDataProcessNewCategoryList(subQuery);
        }

        public override String keepDataEditId_NotExistsSubQuery_TDataProcessNewItemSrcList(TDataProcessNewItemSrcCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepDataEditId_NotExistsSubQuery_TDataProcessNewItemSrcList(subQuery);
        }

        public override String keepDataEditId_NotExistsSubQuery_TIntegConditionList(TIntegConditionCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepDataEditId_NotExistsSubQuery_TIntegConditionList(subQuery);
        }

        public override String keepDataEditId_InScopeSubQuery_TDataEditList(TDataEditListCQ subQuery) {
            return _myCQ.keepDataEditId_InScopeSubQuery_TDataEditList(subQuery);
        }

        public override String keepDataEditId_InScopeSubQuery_TDataProcessNewCategoryList(TDataProcessNewCategoryCQ subQuery) {
            return _myCQ.keepDataEditId_InScopeSubQuery_TDataProcessNewCategoryList(subQuery);
        }

        public override String keepDataEditId_InScopeSubQuery_TDataProcessNewItemSrcList(TDataProcessNewItemSrcCQ subQuery) {
            return _myCQ.keepDataEditId_InScopeSubQuery_TDataProcessNewItemSrcList(subQuery);
        }

        public override String keepDataEditId_InScopeSubQuery_TIntegConditionList(TIntegConditionCQ subQuery) {
            return _myCQ.keepDataEditId_InScopeSubQuery_TIntegConditionList(subQuery);
        }

        public override String keepDataEditId_NotInScopeSubQuery_TDataEditList(TDataEditListCQ subQuery) {
            return _myCQ.keepDataEditId_NotInScopeSubQuery_TDataEditList(subQuery);
        }

        public override String keepDataEditId_NotInScopeSubQuery_TDataProcessNewCategoryList(TDataProcessNewCategoryCQ subQuery) {
            return _myCQ.keepDataEditId_NotInScopeSubQuery_TDataProcessNewCategoryList(subQuery);
        }

        public override String keepDataEditId_NotInScopeSubQuery_TDataProcessNewItemSrcList(TDataProcessNewItemSrcCQ subQuery) {
            return _myCQ.keepDataEditId_NotInScopeSubQuery_TDataProcessNewItemSrcList(subQuery);
        }

        public override String keepDataEditId_NotInScopeSubQuery_TIntegConditionList(TIntegConditionCQ subQuery) {
            return _myCQ.keepDataEditId_NotInScopeSubQuery_TIntegConditionList(subQuery);
        }
        public override String keepDataEditId_SpecifyDerivedReferrer_TDataProcessNewCategoryList(TDataProcessNewCategoryCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepDataEditId_SpecifyDerivedReferrer_TDataProcessNewItemSrcList(TDataProcessNewItemSrcCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepDataEditId_SpecifyDerivedReferrer_TIntegConditionList(TIntegConditionCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepDataEditId_QueryDerivedReferrer_TDataProcessNewCategoryList(TDataProcessNewCategoryCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepDataEditId_QueryDerivedReferrer_TDataProcessNewCategoryListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepDataEditId_QueryDerivedReferrer_TDataProcessNewItemSrcList(TDataProcessNewItemSrcCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepDataEditId_QueryDerivedReferrer_TDataProcessNewItemSrcListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepDataEditId_QueryDerivedReferrer_TIntegConditionList(TIntegConditionCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepDataEditId_QueryDerivedReferrer_TIntegConditionListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }

        protected override ConditionValue getCValueSrcItemId() {
            return _myCQ.SrcItemId;
        }


        protected override ConditionValue getCValueNewItemId() {
            return _myCQ.NewItemId;
        }


        protected override ConditionValue getCValueNewItemName() {
            return _myCQ.NewItemName;
        }


        protected override ConditionValue getCValueNewLv1title() {
            return _myCQ.NewLv1title;
        }


        protected override ConditionValue getCValueNewLv2title() {
            return _myCQ.NewLv2title;
        }


        protected override ConditionValue getCValueNewAnswerType() {
            return _myCQ.NewAnswerType;
        }


        protected override ConditionValue getCValueNewCategoryCount() {
            return _myCQ.NewCategoryCount;
        }


        protected override ConditionValue getCValueUnfitFlag() {
            return _myCQ.UnfitFlag;
        }


        protected override ConditionValue getCValueConditionDiv() {
            return _myCQ.ConditionDiv;
        }


        protected override ConditionValue getCValueSeriesFlag() {
            return _myCQ.SeriesFlag;
        }


        protected override ConditionValue getCValueUpperFlag() {
            return _myCQ.UpperFlag;
        }


        protected override ConditionValue getCValueBottomFlag() {
            return _myCQ.BottomFlag;
        }


        protected override ConditionValue getCValueNoanswerZeroFlag() {
            return _myCQ.NoanswerZeroFlag;
        }


        protected override ConditionValue getCValueSelectMethod() {
            return _myCQ.SelectMethod;
        }


        protected override ConditionValue getCValueTargetCategoryCondition() {
            return _myCQ.TargetCategoryCondition;
        }


        protected override ConditionValue getCValueCalcType() {
            return _myCQ.CalcType;
        }


        protected override ConditionValue getCValueFormulaString() {
            return _myCQ.FormulaString;
        }


        // ===================================================================================
        //                                                                     Scalar SubQuery
        //                                                                     ===============
        public override String keepScalarSubQuery(TDataProcessNewItemCQ subQuery) {
            throw new UnsupportedOperationException("ScalarSubQuery at inline() is unsupported! Sorry!");
        }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        public override String keepMyselfInScopeSubQuery(TDataProcessNewItemCQ subQuery) {
            throw new UnsupportedOperationException("MyselfInScopeSubQuery at inline() is unsupported! Sorry!");
        }
    }
}
