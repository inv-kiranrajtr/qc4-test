
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
    public class TOutputReportsetInfoCIQ : AbstractBsTOutputReportsetInfoCQ {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected BsTOutputReportsetInfoCQ _myCQ;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TOutputReportsetInfoCIQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel, BsTOutputReportsetInfoCQ myCQ)
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


        protected override ConditionValue getCValueOutputReportsetInfoId() {
            return _myCQ.OutputReportsetInfoId;
        }


        public override String keepOutputReportsetInfoId_ExistsSubQuery_TOutputRequestList(TOutputRequestCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepOutputReportsetInfoId_ExistsSubQuery_TOutputRequestList(subQuery);
        }

        public override String keepOutputReportsetInfoId_NotExistsSubQuery_TOutputRequestList(TOutputRequestCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepOutputReportsetInfoId_NotExistsSubQuery_TOutputRequestList(subQuery);
        }

        public override String keepOutputReportsetInfoId_InScopeSubQuery_TOutputRequestList(TOutputRequestCQ subQuery) {
            return _myCQ.keepOutputReportsetInfoId_InScopeSubQuery_TOutputRequestList(subQuery);
        }

        public override String keepOutputReportsetInfoId_NotInScopeSubQuery_TOutputRequestList(TOutputRequestCQ subQuery) {
            return _myCQ.keepOutputReportsetInfoId_NotInScopeSubQuery_TOutputRequestList(subQuery);
        }
        public override String keepOutputReportsetInfoId_SpecifyDerivedReferrer_TOutputRequestList(TOutputRequestCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepOutputReportsetInfoId_QueryDerivedReferrer_TOutputRequestList(TOutputRequestCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepOutputReportsetInfoId_QueryDerivedReferrer_TOutputRequestListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }

        protected override ConditionValue getCValueOutputFileTypeCode() {
            return _myCQ.OutputFileTypeCode;
        }


        protected override ConditionValue getCValueReportFilenNamePrefix() {
            return _myCQ.ReportFilenNamePrefix;
        }


        protected override ConditionValue getCValueCommentOutputFlag() {
            return _myCQ.CommentOutputFlag;
        }


        protected override ConditionValue getCValuePowerpointType() {
            return _myCQ.PowerpointType;
        }


        protected override ConditionValue getCValueOutputTemplateId() {
            return _myCQ.OutputTemplateId;
        }


        public override String keepOutputTemplateId_InScopeSubQuery_TOutputTemplate(TOutputTemplateCQ subQuery) {
            return _myCQ.keepOutputTemplateId_InScopeSubQuery_TOutputTemplate(subQuery);
        }

        public override String keepOutputTemplateId_NotInScopeSubQuery_TOutputTemplate(TOutputTemplateCQ subQuery) {
            return _myCQ.keepOutputTemplateId_NotInScopeSubQuery_TOutputTemplate(subQuery);
        }

        // ===================================================================================
        //                                                                     Scalar SubQuery
        //                                                                     ===============
        public override String keepScalarSubQuery(TOutputReportsetInfoCQ subQuery) {
            throw new UnsupportedOperationException("ScalarSubQuery at inline() is unsupported! Sorry!");
        }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        public override String keepMyselfInScopeSubQuery(TOutputReportsetInfoCQ subQuery) {
            throw new UnsupportedOperationException("MyselfInScopeSubQuery at inline() is unsupported! Sorry!");
        }
    }
}
