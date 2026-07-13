
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
    public class TOutputSettingCIQ : AbstractBsTOutputSettingCQ {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected BsTOutputSettingCQ _myCQ;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TOutputSettingCIQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel, BsTOutputSettingCQ myCQ)
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


        public override String keepQcwebid_ExistsSubQuery_TOutputHistoryItemList(TOutputHistoryItemCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_ExistsSubQuery_TOutputHistoryItemList(subQuery);
        }

        public override String keepQcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOne(subQuery);
        }

        public override String keepQcwebid_NotExistsSubQuery_TOutputHistoryItemList(TOutputHistoryItemCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_NotExistsSubQuery_TOutputHistoryItemList(subQuery);
        }

        public override String keepQcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOne(subQuery);
        }

        public override String keepQcwebid_InScopeSubQuery_TQcwebSurveyInfo(TQcwebSurveyInfoCQ subQuery) {
            return _myCQ.keepQcwebid_InScopeSubQuery_TQcwebSurveyInfo(subQuery);
        }

        public override String keepQcwebid_InScopeSubQuery_TOutputHistoryItemList(TOutputHistoryItemCQ subQuery) {
            return _myCQ.keepQcwebid_InScopeSubQuery_TOutputHistoryItemList(subQuery);
        }

        public override String keepQcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery) {
            return _myCQ.keepQcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOne(subQuery);
        }

        public override String keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfo(TQcwebSurveyInfoCQ subQuery) {
            return _myCQ.keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfo(subQuery);
        }

        public override String keepQcwebid_NotInScopeSubQuery_TOutputHistoryItemList(TOutputHistoryItemCQ subQuery) {
            return _myCQ.keepQcwebid_NotInScopeSubQuery_TOutputHistoryItemList(subQuery);
        }

        public override String keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery) {
            return _myCQ.keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOne(subQuery);
        }
        public override String keepQcwebid_SpecifyDerivedReferrer_TOutputHistoryItemList(TOutputHistoryItemCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_QueryDerivedReferrer_TOutputHistoryItemList(TOutputHistoryItemCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_QueryDerivedReferrer_TOutputHistoryItemListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }

        protected override ConditionValue getCValueOutputFileType() {
            return _myCQ.OutputFileType;
        }


        protected override ConditionValue getCValuePartitionFlag() {
            return _myCQ.PartitionFlag;
        }


        protected override ConditionValue getCValueLayoutFlag() {
            return _myCQ.LayoutFlag;
        }


        protected override ConditionValue getCValueOutputType() {
            return _myCQ.OutputType;
        }


        protected override ConditionValue getCValueNoAnswerChar() {
            return _myCQ.NoAnswerChar;
        }


        protected override ConditionValue getCValueUnmacthChar() {
            return _myCQ.UnmacthChar;
        }


        protected override ConditionValue getCValueMultiItemType() {
            return _myCQ.MultiItemType;
        }


        protected override ConditionValue getCValueNumberType() {
            return _myCQ.NumberType;
        }


        // ===================================================================================
        //                                                                     Scalar SubQuery
        //                                                                     ===============
        public override String keepScalarSubQuery(TOutputSettingCQ subQuery) {
            throw new UnsupportedOperationException("ScalarSubQuery at inline() is unsupported! Sorry!");
        }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        public override String keepMyselfInScopeSubQuery(TOutputSettingCQ subQuery) {
            throw new UnsupportedOperationException("MyselfInScopeSubQuery at inline() is unsupported! Sorry!");
        }
    }
}
