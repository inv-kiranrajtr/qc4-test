
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
    public class TOutputTemplateCIQ : AbstractBsTOutputTemplateCQ {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected BsTOutputTemplateCQ _myCQ;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TOutputTemplateCIQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel, BsTOutputTemplateCQ myCQ)
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


        protected override ConditionValue getCValueOutputTemplateId() {
            return _myCQ.OutputTemplateId;
        }


        public override String keepOutputTemplateId_ExistsSubQuery_TOutputReportsetInfoList(TOutputReportsetInfoCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepOutputTemplateId_ExistsSubQuery_TOutputReportsetInfoList(subQuery);
        }

        public override String keepOutputTemplateId_NotExistsSubQuery_TOutputReportsetInfoList(TOutputReportsetInfoCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepOutputTemplateId_NotExistsSubQuery_TOutputReportsetInfoList(subQuery);
        }

        public override String keepOutputTemplateId_InScopeSubQuery_TOutputReportsetInfoList(TOutputReportsetInfoCQ subQuery) {
            return _myCQ.keepOutputTemplateId_InScopeSubQuery_TOutputReportsetInfoList(subQuery);
        }

        public override String keepOutputTemplateId_NotInScopeSubQuery_TOutputReportsetInfoList(TOutputReportsetInfoCQ subQuery) {
            return _myCQ.keepOutputTemplateId_NotInScopeSubQuery_TOutputReportsetInfoList(subQuery);
        }
        public override String keepOutputTemplateId_SpecifyDerivedReferrer_TOutputReportsetInfoList(TOutputReportsetInfoCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepOutputTemplateId_QueryDerivedReferrer_TOutputReportsetInfoList(TOutputReportsetInfoCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepOutputTemplateId_QueryDerivedReferrer_TOutputReportsetInfoListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }

        protected override ConditionValue getCValueOutputTemplateMasterId() {
            return _myCQ.OutputTemplateMasterId;
        }


        public override String keepOutputTemplateMasterId_InScopeSubQuery_TOutputTemplateMaster(TOutputTemplateMasterCQ subQuery) {
            return _myCQ.keepOutputTemplateMasterId_InScopeSubQuery_TOutputTemplateMaster(subQuery);
        }

        public override String keepOutputTemplateMasterId_NotInScopeSubQuery_TOutputTemplateMaster(TOutputTemplateMasterCQ subQuery) {
            return _myCQ.keepOutputTemplateMasterId_NotInScopeSubQuery_TOutputTemplateMaster(subQuery);
        }

        protected override ConditionValue getCValueUploadPath() {
            return _myCQ.UploadPath;
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

        protected override ConditionValue getCValueAlias() {
            return _myCQ.Alias;
        }


        protected override ConditionValue getCValueCreateDatetime() {
            return _myCQ.CreateDatetime;
        }


        protected override ConditionValue getCValueDeleteFlag() {
            return _myCQ.DeleteFlag;
        }


        // ===================================================================================
        //                                                                     Scalar SubQuery
        //                                                                     ===============
        public override String keepScalarSubQuery(TOutputTemplateCQ subQuery) {
            throw new UnsupportedOperationException("ScalarSubQuery at inline() is unsupported! Sorry!");
        }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        public override String keepMyselfInScopeSubQuery(TOutputTemplateCQ subQuery) {
            throw new UnsupportedOperationException("MyselfInScopeSubQuery at inline() is unsupported! Sorry!");
        }
    }
}
