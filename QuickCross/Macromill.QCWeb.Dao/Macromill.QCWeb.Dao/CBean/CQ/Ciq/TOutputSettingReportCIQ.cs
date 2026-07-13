
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
    public class TOutputSettingReportCIQ : AbstractBsTOutputSettingReportCQ {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected BsTOutputSettingReportCQ _myCQ;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TOutputSettingReportCIQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel, BsTOutputSettingReportCQ myCQ)
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


        public override String keepQcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOne(subQuery);
        }

        public override String keepQcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOne(subQuery);
        }

        public override String keepQcwebid_InScopeSubQuery_TQcwebSurveyInfo(TQcwebSurveyInfoCQ subQuery) {
            return _myCQ.keepQcwebid_InScopeSubQuery_TQcwebSurveyInfo(subQuery);
        }

        public override String keepQcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery) {
            return _myCQ.keepQcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOne(subQuery);
        }

        public override String keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfo(TQcwebSurveyInfoCQ subQuery) {
            return _myCQ.keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfo(subQuery);
        }

        public override String keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery) {
            return _myCQ.keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOne(subQuery);
        }

        protected override ConditionValue getCValueFileTypeExcelFlag() {
            return _myCQ.FileTypeExcelFlag;
        }


        protected override ConditionValue getCValueFileTypePpFlag() {
            return _myCQ.FileTypePpFlag;
        }


        protected override ConditionValue getCValueFileTypePdfFlag() {
            return _myCQ.FileTypePdfFlag;
        }


        protected override ConditionValue getCValueReportType() {
            return _myCQ.ReportType;
        }


        protected override ConditionValue getCValueGraphOutputFlag() {
            return _myCQ.GraphOutputFlag;
        }


        protected override ConditionValue getCValueAscFlag() {
            return _myCQ.AscFlag;
        }


        protected override ConditionValue getCValueCommentVisibleFlag() {
            return _myCQ.CommentVisibleFlag;
        }


        protected override ConditionValue getCValueSurveyReportFlag() {
            return _myCQ.SurveyReportFlag;
        }


        protected override ConditionValue getCValueOutputTemplateId() {
            return _myCQ.OutputTemplateId;
        }


        // ===================================================================================
        //                                                                     Scalar SubQuery
        //                                                                     ===============
        public override String keepScalarSubQuery(TOutputSettingReportCQ subQuery) {
            throw new UnsupportedOperationException("ScalarSubQuery at inline() is unsupported! Sorry!");
        }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        public override String keepMyselfInScopeSubQuery(TOutputSettingReportCQ subQuery) {
            throw new UnsupportedOperationException("MyselfInScopeSubQuery at inline() is unsupported! Sorry!");
        }
    }
}
