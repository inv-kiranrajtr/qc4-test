
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
    public class TItemInfoCIQ : AbstractBsTItemInfoCQ {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected BsTItemInfoCQ _myCQ;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TItemInfoCIQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel, BsTItemInfoCQ myCQ)
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


        protected override ConditionValue getCValueItemInfoId() {
            return _myCQ.ItemInfoId;
        }


        public override String keepItemInfoId_ExistsSubQuery_TCategoryInfoList(TCategoryInfoCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepItemInfoId_ExistsSubQuery_TCategoryInfoList(subQuery);
        }

        public override String keepItemInfoId_ExistsSubQuery_TMatrixInfoByItemInfoIdList(TMatrixInfoCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepItemInfoId_ExistsSubQuery_TMatrixInfoByItemInfoIdList(subQuery);
        }

        public override String keepItemInfoId_ExistsSubQuery_TMatrixInfoByChildItemInfoIdList(TMatrixInfoCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepItemInfoId_ExistsSubQuery_TMatrixInfoByChildItemInfoIdList(subQuery);
        }

        public override String keepItemInfoId_ExistsSubQuery_TScenarioQuerylistList(TScenarioQuerylistCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepItemInfoId_ExistsSubQuery_TScenarioQuerylistList(subQuery);
        }

        public override String keepItemInfoId_ExistsSubQuery_TGtScenarioItemList(TGtScenarioItemCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepItemInfoId_ExistsSubQuery_TGtScenarioItemList(subQuery);
        }

        public override String keepItemInfoId_ExistsSubQuery_TFaScenarioItemList(TFaScenarioItemCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepItemInfoId_ExistsSubQuery_TFaScenarioItemList(subQuery);
        }

        public override String keepItemInfoId_ExistsSubQuery_TFaListAddItemList(TFaListAddItemCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepItemInfoId_ExistsSubQuery_TFaListAddItemList(subQuery);
        }

        public override String keepItemInfoId_ExistsSubQuery_TGtMatrixChildList(TGtMatrixChildCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepItemInfoId_ExistsSubQuery_TGtMatrixChildList(subQuery);
        }

        public override String keepItemInfoId_NotExistsSubQuery_TCategoryInfoList(TCategoryInfoCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepItemInfoId_NotExistsSubQuery_TCategoryInfoList(subQuery);
        }

        public override String keepItemInfoId_NotExistsSubQuery_TMatrixInfoByItemInfoIdList(TMatrixInfoCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepItemInfoId_NotExistsSubQuery_TMatrixInfoByItemInfoIdList(subQuery);
        }

        public override String keepItemInfoId_NotExistsSubQuery_TMatrixInfoByChildItemInfoIdList(TMatrixInfoCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepItemInfoId_NotExistsSubQuery_TMatrixInfoByChildItemInfoIdList(subQuery);
        }

        public override String keepItemInfoId_NotExistsSubQuery_TScenarioQuerylistList(TScenarioQuerylistCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepItemInfoId_NotExistsSubQuery_TScenarioQuerylistList(subQuery);
        }

        public override String keepItemInfoId_NotExistsSubQuery_TGtScenarioItemList(TGtScenarioItemCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepItemInfoId_NotExistsSubQuery_TGtScenarioItemList(subQuery);
        }

        public override String keepItemInfoId_NotExistsSubQuery_TFaScenarioItemList(TFaScenarioItemCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepItemInfoId_NotExistsSubQuery_TFaScenarioItemList(subQuery);
        }

        public override String keepItemInfoId_NotExistsSubQuery_TFaListAddItemList(TFaListAddItemCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepItemInfoId_NotExistsSubQuery_TFaListAddItemList(subQuery);
        }

        public override String keepItemInfoId_NotExistsSubQuery_TGtMatrixChildList(TGtMatrixChildCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepItemInfoId_NotExistsSubQuery_TGtMatrixChildList(subQuery);
        }

        public override String keepItemInfoId_InScopeSubQuery_TMatrixInfo(TMatrixInfoCQ subQuery) {
            return _myCQ.keepItemInfoId_InScopeSubQuery_TMatrixInfo(subQuery);
        }

        public override String keepItemInfoId_InScopeSubQuery_TCategoryInfoList(TCategoryInfoCQ subQuery) {
            return _myCQ.keepItemInfoId_InScopeSubQuery_TCategoryInfoList(subQuery);
        }

        public override String keepItemInfoId_InScopeSubQuery_TMatrixInfoByItemInfoIdList(TMatrixInfoCQ subQuery) {
            return _myCQ.keepItemInfoId_InScopeSubQuery_TMatrixInfoByItemInfoIdList(subQuery);
        }

        public override String keepItemInfoId_InScopeSubQuery_TMatrixInfoByChildItemInfoIdList(TMatrixInfoCQ subQuery) {
            return _myCQ.keepItemInfoId_InScopeSubQuery_TMatrixInfoByChildItemInfoIdList(subQuery);
        }

        public override String keepItemInfoId_InScopeSubQuery_TScenarioQuerylistList(TScenarioQuerylistCQ subQuery) {
            return _myCQ.keepItemInfoId_InScopeSubQuery_TScenarioQuerylistList(subQuery);
        }

        public override String keepItemInfoId_InScopeSubQuery_TGtScenarioItemList(TGtScenarioItemCQ subQuery) {
            return _myCQ.keepItemInfoId_InScopeSubQuery_TGtScenarioItemList(subQuery);
        }

        public override String keepItemInfoId_InScopeSubQuery_TFaScenarioItemList(TFaScenarioItemCQ subQuery) {
            return _myCQ.keepItemInfoId_InScopeSubQuery_TFaScenarioItemList(subQuery);
        }

        public override String keepItemInfoId_InScopeSubQuery_TFaListAddItemList(TFaListAddItemCQ subQuery) {
            return _myCQ.keepItemInfoId_InScopeSubQuery_TFaListAddItemList(subQuery);
        }

        public override String keepItemInfoId_InScopeSubQuery_TGtMatrixChildList(TGtMatrixChildCQ subQuery) {
            return _myCQ.keepItemInfoId_InScopeSubQuery_TGtMatrixChildList(subQuery);
        }

        public override String keepItemInfoId_NotInScopeSubQuery_TMatrixInfo(TMatrixInfoCQ subQuery) {
            return _myCQ.keepItemInfoId_NotInScopeSubQuery_TMatrixInfo(subQuery);
        }

        public override String keepItemInfoId_NotInScopeSubQuery_TCategoryInfoList(TCategoryInfoCQ subQuery) {
            return _myCQ.keepItemInfoId_NotInScopeSubQuery_TCategoryInfoList(subQuery);
        }

        public override String keepItemInfoId_NotInScopeSubQuery_TMatrixInfoByItemInfoIdList(TMatrixInfoCQ subQuery) {
            return _myCQ.keepItemInfoId_NotInScopeSubQuery_TMatrixInfoByItemInfoIdList(subQuery);
        }

        public override String keepItemInfoId_NotInScopeSubQuery_TMatrixInfoByChildItemInfoIdList(TMatrixInfoCQ subQuery) {
            return _myCQ.keepItemInfoId_NotInScopeSubQuery_TMatrixInfoByChildItemInfoIdList(subQuery);
        }

        public override String keepItemInfoId_NotInScopeSubQuery_TScenarioQuerylistList(TScenarioQuerylistCQ subQuery) {
            return _myCQ.keepItemInfoId_NotInScopeSubQuery_TScenarioQuerylistList(subQuery);
        }

        public override String keepItemInfoId_NotInScopeSubQuery_TGtScenarioItemList(TGtScenarioItemCQ subQuery) {
            return _myCQ.keepItemInfoId_NotInScopeSubQuery_TGtScenarioItemList(subQuery);
        }

        public override String keepItemInfoId_NotInScopeSubQuery_TFaScenarioItemList(TFaScenarioItemCQ subQuery) {
            return _myCQ.keepItemInfoId_NotInScopeSubQuery_TFaScenarioItemList(subQuery);
        }

        public override String keepItemInfoId_NotInScopeSubQuery_TFaListAddItemList(TFaListAddItemCQ subQuery) {
            return _myCQ.keepItemInfoId_NotInScopeSubQuery_TFaListAddItemList(subQuery);
        }

        public override String keepItemInfoId_NotInScopeSubQuery_TGtMatrixChildList(TGtMatrixChildCQ subQuery) {
            return _myCQ.keepItemInfoId_NotInScopeSubQuery_TGtMatrixChildList(subQuery);
        }
        public override String keepItemInfoId_SpecifyDerivedReferrer_TCategoryInfoList(TCategoryInfoCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepItemInfoId_SpecifyDerivedReferrer_TMatrixInfoByItemInfoIdList(TMatrixInfoCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepItemInfoId_SpecifyDerivedReferrer_TMatrixInfoByChildItemInfoIdList(TMatrixInfoCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepItemInfoId_SpecifyDerivedReferrer_TScenarioQuerylistList(TScenarioQuerylistCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepItemInfoId_SpecifyDerivedReferrer_TGtScenarioItemList(TGtScenarioItemCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepItemInfoId_SpecifyDerivedReferrer_TFaScenarioItemList(TFaScenarioItemCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepItemInfoId_SpecifyDerivedReferrer_TFaListAddItemList(TFaListAddItemCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepItemInfoId_SpecifyDerivedReferrer_TGtMatrixChildList(TGtMatrixChildCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepItemInfoId_QueryDerivedReferrer_TCategoryInfoList(TCategoryInfoCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepItemInfoId_QueryDerivedReferrer_TCategoryInfoListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepItemInfoId_QueryDerivedReferrer_TMatrixInfoByItemInfoIdList(TMatrixInfoCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepItemInfoId_QueryDerivedReferrer_TMatrixInfoByItemInfoIdListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepItemInfoId_QueryDerivedReferrer_TMatrixInfoByChildItemInfoIdList(TMatrixInfoCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepItemInfoId_QueryDerivedReferrer_TMatrixInfoByChildItemInfoIdListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepItemInfoId_QueryDerivedReferrer_TScenarioQuerylistList(TScenarioQuerylistCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepItemInfoId_QueryDerivedReferrer_TScenarioQuerylistListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepItemInfoId_QueryDerivedReferrer_TGtScenarioItemList(TGtScenarioItemCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepItemInfoId_QueryDerivedReferrer_TGtScenarioItemListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepItemInfoId_QueryDerivedReferrer_TFaScenarioItemList(TFaScenarioItemCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepItemInfoId_QueryDerivedReferrer_TFaScenarioItemListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepItemInfoId_QueryDerivedReferrer_TFaListAddItemList(TFaListAddItemCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepItemInfoId_QueryDerivedReferrer_TFaListAddItemListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepItemInfoId_QueryDerivedReferrer_TGtMatrixChildList(TGtMatrixChildCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepItemInfoId_QueryDerivedReferrer_TGtMatrixChildListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
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

        protected override ConditionValue getCValueItemName() {
            return _myCQ.ItemName;
        }


        protected override ConditionValue getCValueSourceDiv() {
            return _myCQ.SourceDiv;
        }


        protected override ConditionValue getCValueItemno() {
            return _myCQ.Itemno;
        }


        protected override ConditionValue getCValueItemType() {
            return _myCQ.ItemType;
        }


        protected override ConditionValue getCValueAnswerType() {
            return _myCQ.AnswerType;
        }


        protected override ConditionValue getCValueSortNumber() {
            return _myCQ.SortNumber;
        }


        protected override ConditionValue getCValueMatrixDiv() {
            return _myCQ.MatrixDiv;
        }


        protected override ConditionValue getCValueLv1title() {
            return _myCQ.Lv1title;
        }


        protected override ConditionValue getCValueLv2title() {
            return _myCQ.Lv2title;
        }


        protected override ConditionValue getCValueOriginalLv1title() {
            return _myCQ.OriginalLv1title;
        }


        protected override ConditionValue getCValueOriginalLv2title() {
            return _myCQ.OriginalLv2title;
        }


        protected override ConditionValue getCValueTableName() {
            return _myCQ.TableName;
        }


        protected override ConditionValue getCValueColumnName() {
            return _myCQ.ColumnName;
        }


        protected override ConditionValue getCValueCategoryEditId() {
            return _myCQ.CategoryEditId;
        }


        public override String keepCategoryEditId_InScopeSubQuery_TScenarioTotalization(TScenarioTotalizationCQ subQuery) {
            return _myCQ.keepCategoryEditId_InScopeSubQuery_TScenarioTotalization(subQuery);
        }

        public override String keepCategoryEditId_NotInScopeSubQuery_TScenarioTotalization(TScenarioTotalizationCQ subQuery) {
            return _myCQ.keepCategoryEditId_NotInScopeSubQuery_TScenarioTotalization(subQuery);
        }

        protected override ConditionValue getCValueDataEditId() {
            return _myCQ.DataEditId;
        }


        public override String keepDataEditId_InScopeSubQuery_TDataEditList(TDataEditListCQ subQuery) {
            return _myCQ.keepDataEditId_InScopeSubQuery_TDataEditList(subQuery);
        }

        public override String keepDataEditId_NotInScopeSubQuery_TDataEditList(TDataEditListCQ subQuery) {
            return _myCQ.keepDataEditId_NotInScopeSubQuery_TDataEditList(subQuery);
        }

        protected override ConditionValue getCValueStatus() {
            return _myCQ.Status;
        }


        protected override ConditionValue getCValueTableNameOrg() {
            return _myCQ.TableNameOrg;
        }


        protected override ConditionValue getCValueColumnNameOrg() {
            return _myCQ.ColumnNameOrg;
        }


        protected override ConditionValue getCValueCompelItemChangeFlag() {
            return _myCQ.CompelItemChangeFlag;
        }


        protected override ConditionValue getCValueSortFlag() {
            return _myCQ.SortFlag;
        }


        protected override ConditionValue getCValueSortRange() {
            return _myCQ.SortRange;
        }


        protected override ConditionValue getCValueMultivariateFlag() {
            return _myCQ.MultivariateFlag;
        }


        protected override ConditionValue getCValueTableNo() {
            return _myCQ.TableNo;
        }


        protected override ConditionValue getCValueColumnNo() {
            return _myCQ.ColumnNo;
        }


        protected override ConditionValue getCValueTableNoOrg() {
            return _myCQ.TableNoOrg;
        }


        protected override ConditionValue getCValueColumnNoOrg() {
            return _myCQ.ColumnNoOrg;
        }


        protected override ConditionValue getCValueLastUpdateUser() {
            return _myCQ.LastUpdateUser;
        }


        protected override ConditionValue getCValueLastUpdateDatetime() {
            return _myCQ.LastUpdateDatetime;
        }


        protected override ConditionValue getCValueNewAtQc3Flag() {
            return _myCQ.NewAtQc3Flag;
        }


        protected override ConditionValue getCValueSortRangeOrg() {
            return _myCQ.SortRangeOrg;
        }


        // ===================================================================================
        //                                                                     Scalar SubQuery
        //                                                                     ===============
        public override String keepScalarSubQuery(TItemInfoCQ subQuery) {
            throw new UnsupportedOperationException("ScalarSubQuery at inline() is unsupported! Sorry!");
        }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        public override String keepMyselfInScopeSubQuery(TItemInfoCQ subQuery) {
            throw new UnsupportedOperationException("MyselfInScopeSubQuery at inline() is unsupported! Sorry!");
        }
    }
}
