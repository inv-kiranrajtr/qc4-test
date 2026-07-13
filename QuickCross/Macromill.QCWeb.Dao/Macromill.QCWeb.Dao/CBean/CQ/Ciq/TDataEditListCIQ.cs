
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
    public class TDataEditListCIQ : AbstractBsTDataEditListCQ {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected BsTDataEditListCQ _myCQ;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TDataEditListCIQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel, BsTDataEditListCQ myCQ)
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


        public override String keepDataEditId_ExistsSubQuery_TDataProcessNewItemAsOne(TDataProcessNewItemCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepDataEditId_ExistsSubQuery_TDataProcessNewItemAsOne(subQuery);
        }

        public override String keepDataEditId_ExistsSubQuery_TDeleteDataAsOne(TDeleteDataCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepDataEditId_ExistsSubQuery_TDeleteDataAsOne(subQuery);
        }

        public override String keepDataEditId_ExistsSubQuery_TEditDataAsOne(TEditDataCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepDataEditId_ExistsSubQuery_TEditDataAsOne(subQuery);
        }

        public override String keepDataEditId_ExistsSubQuery_TItemInfoList(TItemInfoCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepDataEditId_ExistsSubQuery_TItemInfoList(subQuery);
        }

        public override String keepDataEditId_NotExistsSubQuery_TDataProcessNewItemAsOne(TDataProcessNewItemCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepDataEditId_NotExistsSubQuery_TDataProcessNewItemAsOne(subQuery);
        }

        public override String keepDataEditId_NotExistsSubQuery_TDeleteDataAsOne(TDeleteDataCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepDataEditId_NotExistsSubQuery_TDeleteDataAsOne(subQuery);
        }

        public override String keepDataEditId_NotExistsSubQuery_TEditDataAsOne(TEditDataCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepDataEditId_NotExistsSubQuery_TEditDataAsOne(subQuery);
        }

        public override String keepDataEditId_NotExistsSubQuery_TItemInfoList(TItemInfoCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepDataEditId_NotExistsSubQuery_TItemInfoList(subQuery);
        }

        public override String keepDataEditId_InScopeSubQuery_TDataProcessNewItemAsOne(TDataProcessNewItemCQ subQuery) {
            return _myCQ.keepDataEditId_InScopeSubQuery_TDataProcessNewItemAsOne(subQuery);
        }

        public override String keepDataEditId_InScopeSubQuery_TDeleteDataAsOne(TDeleteDataCQ subQuery) {
            return _myCQ.keepDataEditId_InScopeSubQuery_TDeleteDataAsOne(subQuery);
        }

        public override String keepDataEditId_InScopeSubQuery_TEditDataAsOne(TEditDataCQ subQuery) {
            return _myCQ.keepDataEditId_InScopeSubQuery_TEditDataAsOne(subQuery);
        }

        public override String keepDataEditId_InScopeSubQuery_TItemInfoList(TItemInfoCQ subQuery) {
            return _myCQ.keepDataEditId_InScopeSubQuery_TItemInfoList(subQuery);
        }

        public override String keepDataEditId_NotInScopeSubQuery_TDataProcessNewItemAsOne(TDataProcessNewItemCQ subQuery) {
            return _myCQ.keepDataEditId_NotInScopeSubQuery_TDataProcessNewItemAsOne(subQuery);
        }

        public override String keepDataEditId_NotInScopeSubQuery_TDeleteDataAsOne(TDeleteDataCQ subQuery) {
            return _myCQ.keepDataEditId_NotInScopeSubQuery_TDeleteDataAsOne(subQuery);
        }

        public override String keepDataEditId_NotInScopeSubQuery_TEditDataAsOne(TEditDataCQ subQuery) {
            return _myCQ.keepDataEditId_NotInScopeSubQuery_TEditDataAsOne(subQuery);
        }

        public override String keepDataEditId_NotInScopeSubQuery_TItemInfoList(TItemInfoCQ subQuery) {
            return _myCQ.keepDataEditId_NotInScopeSubQuery_TItemInfoList(subQuery);
        }
        public override String keepDataEditId_SpecifyDerivedReferrer_TItemInfoList(TItemInfoCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepDataEditId_QueryDerivedReferrer_TItemInfoList(TItemInfoCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepDataEditId_QueryDerivedReferrer_TItemInfoListParameter(Object parameterValue) {
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

        protected override ConditionValue getCValueExecuteNo() {
            return _myCQ.ExecuteNo;
        }


        protected override ConditionValue getCValueExecuteFlag() {
            return _myCQ.ExecuteFlag;
        }


        protected override ConditionValue getCValueEditMenuMasterId() {
            return _myCQ.EditMenuMasterId;
        }


        public override String keepEditMenuMasterId_InScopeSubQuery_TEditMenuMaster(TEditMenuMasterCQ subQuery) {
            return _myCQ.keepEditMenuMasterId_InScopeSubQuery_TEditMenuMaster(subQuery);
        }

        public override String keepEditMenuMasterId_NotInScopeSubQuery_TEditMenuMaster(TEditMenuMasterCQ subQuery) {
            return _myCQ.keepEditMenuMasterId_NotInScopeSubQuery_TEditMenuMaster(subQuery);
        }

        protected override ConditionValue getCValueDescription() {
            return _myCQ.Description;
        }


        protected override ConditionValue getCValueConditionItemViewName() {
            return _myCQ.ConditionItemViewName;
        }


        protected override ConditionValue getCValueTargetItemViewName() {
            return _myCQ.TargetItemViewName;
        }


        protected override ConditionValue getCValueStatus() {
            return _myCQ.Status;
        }


        protected override ConditionValue getCValueLatestFlag() {
            return _myCQ.LatestFlag;
        }


        protected override ConditionValue getCValueDerivedDataEditId() {
            return _myCQ.DerivedDataEditId;
        }


        protected override ConditionValue getCValueDeleteReserveFlag() {
            return _myCQ.DeleteReserveFlag;
        }


        protected override ConditionValue getCValueLastUpdateUser() {
            return _myCQ.LastUpdateUser;
        }


        protected override ConditionValue getCValueLastUpdateDatetime() {
            return _myCQ.LastUpdateDatetime;
        }


        protected override ConditionValue getCValueEditFlag() {
            return _myCQ.EditFlag;
        }


        // ===================================================================================
        //                                                                     Scalar SubQuery
        //                                                                     ===============
        public override String keepScalarSubQuery(TDataEditListCQ subQuery) {
            throw new UnsupportedOperationException("ScalarSubQuery at inline() is unsupported! Sorry!");
        }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        public override String keepMyselfInScopeSubQuery(TDataEditListCQ subQuery) {
            throw new UnsupportedOperationException("MyselfInScopeSubQuery at inline() is unsupported! Sorry!");
        }
    }
}
