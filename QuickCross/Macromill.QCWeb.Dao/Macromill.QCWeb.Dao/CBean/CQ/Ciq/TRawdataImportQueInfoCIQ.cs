
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
    public class TRawdataImportQueInfoCIQ : AbstractBsTRawdataImportQueInfoCQ {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected BsTRawdataImportQueInfoCQ _myCQ;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TRawdataImportQueInfoCIQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel, BsTRawdataImportQueInfoCQ myCQ)
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


        protected override ConditionValue getCValueRawdataImportQueInfoId() {
            return _myCQ.RawdataImportQueInfoId;
        }


        public override String keepRawdataImportQueInfoId_ExistsSubQuery_TQcwebSurveyInfoList(TQcwebSurveyInfoCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepRawdataImportQueInfoId_ExistsSubQuery_TQcwebSurveyInfoList(subQuery);
        }

        public override String keepRawdataImportQueInfoId_NotExistsSubQuery_TQcwebSurveyInfoList(TQcwebSurveyInfoCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepRawdataImportQueInfoId_NotExistsSubQuery_TQcwebSurveyInfoList(subQuery);
        }

        public override String keepRawdataImportQueInfoId_InScopeSubQuery_TQcwebSurveyInfoList(TQcwebSurveyInfoCQ subQuery) {
            return _myCQ.keepRawdataImportQueInfoId_InScopeSubQuery_TQcwebSurveyInfoList(subQuery);
        }

        public override String keepRawdataImportQueInfoId_NotInScopeSubQuery_TQcwebSurveyInfoList(TQcwebSurveyInfoCQ subQuery) {
            return _myCQ.keepRawdataImportQueInfoId_NotInScopeSubQuery_TQcwebSurveyInfoList(subQuery);
        }
        public override String keepRawdataImportQueInfoId_SpecifyDerivedReferrer_TQcwebSurveyInfoList(TQcwebSurveyInfoCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepRawdataImportQueInfoId_QueryDerivedReferrer_TQcwebSurveyInfoList(TQcwebSurveyInfoCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepRawdataImportQueInfoId_QueryDerivedReferrer_TQcwebSurveyInfoListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }

        protected override ConditionValue getCValueQcwebJobNo() {
            return _myCQ.QcwebJobNo;
        }


        protected override ConditionValue getCValueMainSurveyId() {
            return _myCQ.MainSurveyId;
        }


        protected override ConditionValue getCValueSurveyDataType() {
            return _myCQ.SurveyDataType;
        }


        protected override ConditionValue getCValueFilepath() {
            return _myCQ.Filepath;
        }


        protected override ConditionValue getCValueFileName() {
            return _myCQ.FileName;
        }


        protected override ConditionValue getCValueImportStatus() {
            return _myCQ.ImportStatus;
        }


        protected override ConditionValue getCValueMessage() {
            return _myCQ.Message;
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

        protected override ConditionValue getCValueAddDataNo() {
            return _myCQ.AddDataNo;
        }


        protected override ConditionValue getCValueRequestDatetime() {
            return _myCQ.RequestDatetime;
        }


        // ===================================================================================
        //                                                                     Scalar SubQuery
        //                                                                     ===============
        public override String keepScalarSubQuery(TRawdataImportQueInfoCQ subQuery) {
            throw new UnsupportedOperationException("ScalarSubQuery at inline() is unsupported! Sorry!");
        }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        public override String keepMyselfInScopeSubQuery(TRawdataImportQueInfoCQ subQuery) {
            throw new UnsupportedOperationException("MyselfInScopeSubQuery at inline() is unsupported! Sorry!");
        }
    }
}
