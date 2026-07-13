
using System;
using System.Collections.Generic;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CKey;
using Macromill.QCWeb.Dao.AllCommon.CBean.COption;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public abstract class AbstractBsTQcwebSurveyInfoCQ : AbstractConditionQuery {

        public AbstractBsTQcwebSurveyInfoCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_QCWEB_SURVEY_INFO"; }
        public override String getTableSqlName() { return "T_QCWEB_SURVEY_INFO"; }

        public void SetQcwebid_Equal(decimal? v) { regQcwebid(CK_EQ, v); }
        public void SetQcwebid_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQcwebid(CK_NES, v);
        }
        public void SetQcwebid_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQcwebid(CK_GT, v);
        }
        public void SetQcwebid_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQcwebid(CK_LT, v);
        }
        public void SetQcwebid_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQcwebid(CK_GE, v);
        }
        public void SetQcwebid_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regQcwebid(CK_LE, v);
        }
        public void SetQcwebid_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueQcwebid(), "QCWEBID");
        }
        public void SetQcwebid_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueQcwebid(), "QCWEBID");
        }
        public void ExistsTAccessPermissionsInfoAsOne(SubQuery<TAccessPermissionsInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TAccessPermissionsInfoCB>", subQuery);
            TAccessPermissionsInfoCB cb = new TAccessPermissionsInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_ExistsSubQuery_TAccessPermissionsInfoAsOne(cb.Query());
            registerExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_ExistsSubQuery_TAccessPermissionsInfoAsOne(TAccessPermissionsInfoCQ subQuery);
        public void ExistsTAllocationCellInfoList(SubQuery<TAllocationCellInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TAllocationCellInfoCB>", subQuery);
            TAllocationCellInfoCB cb = new TAllocationCellInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_ExistsSubQuery_TAllocationCellInfoList(cb.Query());
            registerExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_ExistsSubQuery_TAllocationCellInfoList(TAllocationCellInfoCQ subQuery);
        public void ExistsTDataEditListList(SubQuery<TDataEditListCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDataEditListCB>", subQuery);
            TDataEditListCB cb = new TDataEditListCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_ExistsSubQuery_TDataEditListList(cb.Query());
            registerExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_ExistsSubQuery_TDataEditListList(TDataEditListCQ subQuery);
        public void ExistsTItemInfoList(SubQuery<TItemInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TItemInfoCB>", subQuery);
            TItemInfoCB cb = new TItemInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_ExistsSubQuery_TItemInfoList(cb.Query());
            registerExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_ExistsSubQuery_TItemInfoList(TItemInfoCQ subQuery);
        public void ExistsTNoticeList(SubQuery<TNoticeCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TNoticeCB>", subQuery);
            TNoticeCB cb = new TNoticeCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_ExistsSubQuery_TNoticeList(cb.Query());
            registerExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_ExistsSubQuery_TNoticeList(TNoticeCQ subQuery);
        public void ExistsTOutputRequestList(SubQuery<TOutputRequestCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputRequestCB>", subQuery);
            TOutputRequestCB cb = new TOutputRequestCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_ExistsSubQuery_TOutputRequestList(cb.Query());
            registerExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_ExistsSubQuery_TOutputRequestList(TOutputRequestCQ subQuery);
        public void ExistsTOutputSettingAsOne(SubQuery<TOutputSettingCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSettingCB>", subQuery);
            TOutputSettingCB cb = new TOutputSettingCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_ExistsSubQuery_TOutputSettingAsOne(cb.Query());
            registerExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_ExistsSubQuery_TOutputSettingAsOne(TOutputSettingCQ subQuery);
        public void ExistsTOutputSettingCrossAsOne(SubQuery<TOutputSettingCrossCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSettingCrossCB>", subQuery);
            TOutputSettingCrossCB cb = new TOutputSettingCrossCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_ExistsSubQuery_TOutputSettingCrossAsOne(cb.Query());
            registerExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_ExistsSubQuery_TOutputSettingCrossAsOne(TOutputSettingCrossCQ subQuery);
        public void ExistsTOutputSettingFaAsOne(SubQuery<TOutputSettingFaCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSettingFaCB>", subQuery);
            TOutputSettingFaCB cb = new TOutputSettingFaCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_ExistsSubQuery_TOutputSettingFaAsOne(cb.Query());
            registerExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_ExistsSubQuery_TOutputSettingFaAsOne(TOutputSettingFaCQ subQuery);
        public void ExistsTOutputSettingGtAsOne(SubQuery<TOutputSettingGtCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSettingGtCB>", subQuery);
            TOutputSettingGtCB cb = new TOutputSettingGtCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_ExistsSubQuery_TOutputSettingGtAsOne(cb.Query());
            registerExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_ExistsSubQuery_TOutputSettingGtAsOne(TOutputSettingGtCQ subQuery);
        public void ExistsTOutputSettingReportAsOne(SubQuery<TOutputSettingReportCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSettingReportCB>", subQuery);
            TOutputSettingReportCB cb = new TOutputSettingReportCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_ExistsSubQuery_TOutputSettingReportAsOne(cb.Query());
            registerExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_ExistsSubQuery_TOutputSettingReportAsOne(TOutputSettingReportCQ subQuery);
        public void ExistsTOutputTemplateList(SubQuery<TOutputTemplateCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputTemplateCB>", subQuery);
            TOutputTemplateCB cb = new TOutputTemplateCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_ExistsSubQuery_TOutputTemplateList(cb.Query());
            registerExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_ExistsSubQuery_TOutputTemplateList(TOutputTemplateCQ subQuery);
        public void ExistsTQcwebSurveyDetailList(SubQuery<TQcwebSurveyDetailCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyDetailCB>", subQuery);
            TQcwebSurveyDetailCB cb = new TQcwebSurveyDetailCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_ExistsSubQuery_TQcwebSurveyDetailList(cb.Query());
            registerExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_ExistsSubQuery_TQcwebSurveyDetailList(TQcwebSurveyDetailCQ subQuery);
        public void ExistsTRawdataImportQueInfoList(SubQuery<TRawdataImportQueInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TRawdataImportQueInfoCB>", subQuery);
            TRawdataImportQueInfoCB cb = new TRawdataImportQueInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_ExistsSubQuery_TRawdataImportQueInfoList(cb.Query());
            registerExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_ExistsSubQuery_TRawdataImportQueInfoList(TRawdataImportQueInfoCQ subQuery);
        public void ExistsTReportsetList(SubQuery<TReportsetCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TReportsetCB>", subQuery);
            TReportsetCB cb = new TReportsetCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_ExistsSubQuery_TReportsetList(cb.Query());
            registerExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_ExistsSubQuery_TReportsetList(TReportsetCQ subQuery);
        public void ExistsTScenarioTotalizationList(SubQuery<TScenarioTotalizationCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TScenarioTotalizationCB>", subQuery);
            TScenarioTotalizationCB cb = new TScenarioTotalizationCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_ExistsSubQuery_TScenarioTotalizationList(cb.Query());
            registerExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_ExistsSubQuery_TScenarioTotalizationList(TScenarioTotalizationCQ subQuery);
        public void ExistsTSelectConditionInfoList(SubQuery<TSelectConditionInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TSelectConditionInfoCB>", subQuery);
            TSelectConditionInfoCB cb = new TSelectConditionInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_ExistsSubQuery_TSelectConditionInfoList(cb.Query());
            registerExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_ExistsSubQuery_TSelectConditionInfoList(TSelectConditionInfoCQ subQuery);
        public void ExistsTSessionControlerList(SubQuery<TSessionControlerCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TSessionControlerCB>", subQuery);
            TSessionControlerCB cb = new TSessionControlerCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_ExistsSubQuery_TSessionControlerList(cb.Query());
            registerExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_ExistsSubQuery_TSessionControlerList(TSessionControlerCQ subQuery);
        public void ExistsTWeightbackList(SubQuery<TWeightbackCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TWeightbackCB>", subQuery);
            TWeightbackCB cb = new TWeightbackCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_ExistsSubQuery_TWeightbackList(cb.Query());
            registerExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_ExistsSubQuery_TWeightbackList(TWeightbackCQ subQuery);
        public void NotExistsTAccessPermissionsInfoAsOne(SubQuery<TAccessPermissionsInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TAccessPermissionsInfoCB>", subQuery);
            TAccessPermissionsInfoCB cb = new TAccessPermissionsInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotExistsSubQuery_TAccessPermissionsInfoAsOne(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotExistsSubQuery_TAccessPermissionsInfoAsOne(TAccessPermissionsInfoCQ subQuery);
        public void NotExistsTAllocationCellInfoList(SubQuery<TAllocationCellInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TAllocationCellInfoCB>", subQuery);
            TAllocationCellInfoCB cb = new TAllocationCellInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotExistsSubQuery_TAllocationCellInfoList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotExistsSubQuery_TAllocationCellInfoList(TAllocationCellInfoCQ subQuery);
        public void NotExistsTDataEditListList(SubQuery<TDataEditListCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDataEditListCB>", subQuery);
            TDataEditListCB cb = new TDataEditListCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotExistsSubQuery_TDataEditListList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotExistsSubQuery_TDataEditListList(TDataEditListCQ subQuery);
        public void NotExistsTItemInfoList(SubQuery<TItemInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TItemInfoCB>", subQuery);
            TItemInfoCB cb = new TItemInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotExistsSubQuery_TItemInfoList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotExistsSubQuery_TItemInfoList(TItemInfoCQ subQuery);
        public void NotExistsTNoticeList(SubQuery<TNoticeCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TNoticeCB>", subQuery);
            TNoticeCB cb = new TNoticeCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotExistsSubQuery_TNoticeList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotExistsSubQuery_TNoticeList(TNoticeCQ subQuery);
        public void NotExistsTOutputRequestList(SubQuery<TOutputRequestCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputRequestCB>", subQuery);
            TOutputRequestCB cb = new TOutputRequestCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotExistsSubQuery_TOutputRequestList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotExistsSubQuery_TOutputRequestList(TOutputRequestCQ subQuery);
        public void NotExistsTOutputSettingAsOne(SubQuery<TOutputSettingCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSettingCB>", subQuery);
            TOutputSettingCB cb = new TOutputSettingCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotExistsSubQuery_TOutputSettingAsOne(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotExistsSubQuery_TOutputSettingAsOne(TOutputSettingCQ subQuery);
        public void NotExistsTOutputSettingCrossAsOne(SubQuery<TOutputSettingCrossCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSettingCrossCB>", subQuery);
            TOutputSettingCrossCB cb = new TOutputSettingCrossCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotExistsSubQuery_TOutputSettingCrossAsOne(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotExistsSubQuery_TOutputSettingCrossAsOne(TOutputSettingCrossCQ subQuery);
        public void NotExistsTOutputSettingFaAsOne(SubQuery<TOutputSettingFaCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSettingFaCB>", subQuery);
            TOutputSettingFaCB cb = new TOutputSettingFaCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotExistsSubQuery_TOutputSettingFaAsOne(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotExistsSubQuery_TOutputSettingFaAsOne(TOutputSettingFaCQ subQuery);
        public void NotExistsTOutputSettingGtAsOne(SubQuery<TOutputSettingGtCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSettingGtCB>", subQuery);
            TOutputSettingGtCB cb = new TOutputSettingGtCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotExistsSubQuery_TOutputSettingGtAsOne(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotExistsSubQuery_TOutputSettingGtAsOne(TOutputSettingGtCQ subQuery);
        public void NotExistsTOutputSettingReportAsOne(SubQuery<TOutputSettingReportCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSettingReportCB>", subQuery);
            TOutputSettingReportCB cb = new TOutputSettingReportCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotExistsSubQuery_TOutputSettingReportAsOne(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotExistsSubQuery_TOutputSettingReportAsOne(TOutputSettingReportCQ subQuery);
        public void NotExistsTOutputTemplateList(SubQuery<TOutputTemplateCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputTemplateCB>", subQuery);
            TOutputTemplateCB cb = new TOutputTemplateCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotExistsSubQuery_TOutputTemplateList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotExistsSubQuery_TOutputTemplateList(TOutputTemplateCQ subQuery);
        public void NotExistsTQcwebSurveyDetailList(SubQuery<TQcwebSurveyDetailCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyDetailCB>", subQuery);
            TQcwebSurveyDetailCB cb = new TQcwebSurveyDetailCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotExistsSubQuery_TQcwebSurveyDetailList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotExistsSubQuery_TQcwebSurveyDetailList(TQcwebSurveyDetailCQ subQuery);
        public void NotExistsTRawdataImportQueInfoList(SubQuery<TRawdataImportQueInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TRawdataImportQueInfoCB>", subQuery);
            TRawdataImportQueInfoCB cb = new TRawdataImportQueInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotExistsSubQuery_TRawdataImportQueInfoList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotExistsSubQuery_TRawdataImportQueInfoList(TRawdataImportQueInfoCQ subQuery);
        public void NotExistsTReportsetList(SubQuery<TReportsetCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TReportsetCB>", subQuery);
            TReportsetCB cb = new TReportsetCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotExistsSubQuery_TReportsetList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotExistsSubQuery_TReportsetList(TReportsetCQ subQuery);
        public void NotExistsTScenarioTotalizationList(SubQuery<TScenarioTotalizationCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TScenarioTotalizationCB>", subQuery);
            TScenarioTotalizationCB cb = new TScenarioTotalizationCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotExistsSubQuery_TScenarioTotalizationList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotExistsSubQuery_TScenarioTotalizationList(TScenarioTotalizationCQ subQuery);
        public void NotExistsTSelectConditionInfoList(SubQuery<TSelectConditionInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TSelectConditionInfoCB>", subQuery);
            TSelectConditionInfoCB cb = new TSelectConditionInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotExistsSubQuery_TSelectConditionInfoList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotExistsSubQuery_TSelectConditionInfoList(TSelectConditionInfoCQ subQuery);
        public void NotExistsTSessionControlerList(SubQuery<TSessionControlerCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TSessionControlerCB>", subQuery);
            TSessionControlerCB cb = new TSessionControlerCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotExistsSubQuery_TSessionControlerList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotExistsSubQuery_TSessionControlerList(TSessionControlerCQ subQuery);
        public void NotExistsTWeightbackList(SubQuery<TWeightbackCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TWeightbackCB>", subQuery);
            TWeightbackCB cb = new TWeightbackCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotExistsSubQuery_TWeightbackList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotExistsSubQuery_TWeightbackList(TWeightbackCQ subQuery);
        public void InScopeTAllocationCellInfo(SubQuery<TAllocationCellInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TAllocationCellInfoCB>", subQuery);
            TAllocationCellInfoCB cb = new TAllocationCellInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_InScopeSubQuery_TAllocationCellInfo(cb.Query());
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWebID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_InScopeSubQuery_TAllocationCellInfo(TAllocationCellInfoCQ subQuery);
        public void InScopeTAccessPermissionsInfoAsOne(SubQuery<TAccessPermissionsInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TAccessPermissionsInfoCB>", subQuery);
            TAccessPermissionsInfoCB cb = new TAccessPermissionsInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_InScopeSubQuery_TAccessPermissionsInfoAsOne(cb.Query());
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_InScopeSubQuery_TAccessPermissionsInfoAsOne(TAccessPermissionsInfoCQ subQuery);
        public void InScopeTAllocationCellInfoList(SubQuery<TAllocationCellInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TAllocationCellInfoCB>", subQuery);
            TAllocationCellInfoCB cb = new TAllocationCellInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_InScopeSubQuery_TAllocationCellInfoList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_InScopeSubQuery_TAllocationCellInfoList(TAllocationCellInfoCQ subQuery);
        public void InScopeTDataEditListList(SubQuery<TDataEditListCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDataEditListCB>", subQuery);
            TDataEditListCB cb = new TDataEditListCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_InScopeSubQuery_TDataEditListList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_InScopeSubQuery_TDataEditListList(TDataEditListCQ subQuery);
        public void InScopeTItemInfoList(SubQuery<TItemInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TItemInfoCB>", subQuery);
            TItemInfoCB cb = new TItemInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_InScopeSubQuery_TItemInfoList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_InScopeSubQuery_TItemInfoList(TItemInfoCQ subQuery);
        public void InScopeTNoticeList(SubQuery<TNoticeCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TNoticeCB>", subQuery);
            TNoticeCB cb = new TNoticeCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_InScopeSubQuery_TNoticeList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_InScopeSubQuery_TNoticeList(TNoticeCQ subQuery);
        public void InScopeTOutputRequestList(SubQuery<TOutputRequestCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputRequestCB>", subQuery);
            TOutputRequestCB cb = new TOutputRequestCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_InScopeSubQuery_TOutputRequestList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_InScopeSubQuery_TOutputRequestList(TOutputRequestCQ subQuery);
        public void InScopeTOutputSettingAsOne(SubQuery<TOutputSettingCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSettingCB>", subQuery);
            TOutputSettingCB cb = new TOutputSettingCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_InScopeSubQuery_TOutputSettingAsOne(cb.Query());
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_InScopeSubQuery_TOutputSettingAsOne(TOutputSettingCQ subQuery);
        public void InScopeTOutputSettingCrossAsOne(SubQuery<TOutputSettingCrossCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSettingCrossCB>", subQuery);
            TOutputSettingCrossCB cb = new TOutputSettingCrossCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_InScopeSubQuery_TOutputSettingCrossAsOne(cb.Query());
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_InScopeSubQuery_TOutputSettingCrossAsOne(TOutputSettingCrossCQ subQuery);
        public void InScopeTOutputSettingFaAsOne(SubQuery<TOutputSettingFaCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSettingFaCB>", subQuery);
            TOutputSettingFaCB cb = new TOutputSettingFaCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_InScopeSubQuery_TOutputSettingFaAsOne(cb.Query());
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_InScopeSubQuery_TOutputSettingFaAsOne(TOutputSettingFaCQ subQuery);
        public void InScopeTOutputSettingGtAsOne(SubQuery<TOutputSettingGtCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSettingGtCB>", subQuery);
            TOutputSettingGtCB cb = new TOutputSettingGtCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_InScopeSubQuery_TOutputSettingGtAsOne(cb.Query());
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_InScopeSubQuery_TOutputSettingGtAsOne(TOutputSettingGtCQ subQuery);
        public void InScopeTOutputSettingReportAsOne(SubQuery<TOutputSettingReportCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSettingReportCB>", subQuery);
            TOutputSettingReportCB cb = new TOutputSettingReportCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_InScopeSubQuery_TOutputSettingReportAsOne(cb.Query());
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_InScopeSubQuery_TOutputSettingReportAsOne(TOutputSettingReportCQ subQuery);
        public void InScopeTOutputTemplateList(SubQuery<TOutputTemplateCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputTemplateCB>", subQuery);
            TOutputTemplateCB cb = new TOutputTemplateCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_InScopeSubQuery_TOutputTemplateList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_InScopeSubQuery_TOutputTemplateList(TOutputTemplateCQ subQuery);
        public void InScopeTQcwebSurveyDetailList(SubQuery<TQcwebSurveyDetailCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyDetailCB>", subQuery);
            TQcwebSurveyDetailCB cb = new TQcwebSurveyDetailCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_InScopeSubQuery_TQcwebSurveyDetailList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_InScopeSubQuery_TQcwebSurveyDetailList(TQcwebSurveyDetailCQ subQuery);
        public void InScopeTRawdataImportQueInfoList(SubQuery<TRawdataImportQueInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TRawdataImportQueInfoCB>", subQuery);
            TRawdataImportQueInfoCB cb = new TRawdataImportQueInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_InScopeSubQuery_TRawdataImportQueInfoList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_InScopeSubQuery_TRawdataImportQueInfoList(TRawdataImportQueInfoCQ subQuery);
        public void InScopeTReportsetList(SubQuery<TReportsetCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TReportsetCB>", subQuery);
            TReportsetCB cb = new TReportsetCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_InScopeSubQuery_TReportsetList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_InScopeSubQuery_TReportsetList(TReportsetCQ subQuery);
        public void InScopeTScenarioTotalizationList(SubQuery<TScenarioTotalizationCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TScenarioTotalizationCB>", subQuery);
            TScenarioTotalizationCB cb = new TScenarioTotalizationCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_InScopeSubQuery_TScenarioTotalizationList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_InScopeSubQuery_TScenarioTotalizationList(TScenarioTotalizationCQ subQuery);
        public void InScopeTSelectConditionInfoList(SubQuery<TSelectConditionInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TSelectConditionInfoCB>", subQuery);
            TSelectConditionInfoCB cb = new TSelectConditionInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_InScopeSubQuery_TSelectConditionInfoList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_InScopeSubQuery_TSelectConditionInfoList(TSelectConditionInfoCQ subQuery);
        public void InScopeTSessionControlerList(SubQuery<TSessionControlerCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TSessionControlerCB>", subQuery);
            TSessionControlerCB cb = new TSessionControlerCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_InScopeSubQuery_TSessionControlerList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_InScopeSubQuery_TSessionControlerList(TSessionControlerCQ subQuery);
        public void InScopeTWeightbackList(SubQuery<TWeightbackCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TWeightbackCB>", subQuery);
            TWeightbackCB cb = new TWeightbackCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_InScopeSubQuery_TWeightbackList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_InScopeSubQuery_TWeightbackList(TWeightbackCQ subQuery);
        public void NotInScopeTAllocationCellInfo(SubQuery<TAllocationCellInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TAllocationCellInfoCB>", subQuery);
            TAllocationCellInfoCB cb = new TAllocationCellInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotInScopeSubQuery_TAllocationCellInfo(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "QCWEBID", "QCWebID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotInScopeSubQuery_TAllocationCellInfo(TAllocationCellInfoCQ subQuery);
        public void NotInScopeTAccessPermissionsInfoAsOne(SubQuery<TAccessPermissionsInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TAccessPermissionsInfoCB>", subQuery);
            TAccessPermissionsInfoCB cb = new TAccessPermissionsInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotInScopeSubQuery_TAccessPermissionsInfoAsOne(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotInScopeSubQuery_TAccessPermissionsInfoAsOne(TAccessPermissionsInfoCQ subQuery);
        public void NotInScopeTAllocationCellInfoList(SubQuery<TAllocationCellInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TAllocationCellInfoCB>", subQuery);
            TAllocationCellInfoCB cb = new TAllocationCellInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotInScopeSubQuery_TAllocationCellInfoList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotInScopeSubQuery_TAllocationCellInfoList(TAllocationCellInfoCQ subQuery);
        public void NotInScopeTDataEditListList(SubQuery<TDataEditListCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDataEditListCB>", subQuery);
            TDataEditListCB cb = new TDataEditListCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotInScopeSubQuery_TDataEditListList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotInScopeSubQuery_TDataEditListList(TDataEditListCQ subQuery);
        public void NotInScopeTItemInfoList(SubQuery<TItemInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TItemInfoCB>", subQuery);
            TItemInfoCB cb = new TItemInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotInScopeSubQuery_TItemInfoList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotInScopeSubQuery_TItemInfoList(TItemInfoCQ subQuery);
        public void NotInScopeTNoticeList(SubQuery<TNoticeCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TNoticeCB>", subQuery);
            TNoticeCB cb = new TNoticeCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotInScopeSubQuery_TNoticeList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotInScopeSubQuery_TNoticeList(TNoticeCQ subQuery);
        public void NotInScopeTOutputRequestList(SubQuery<TOutputRequestCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputRequestCB>", subQuery);
            TOutputRequestCB cb = new TOutputRequestCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotInScopeSubQuery_TOutputRequestList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotInScopeSubQuery_TOutputRequestList(TOutputRequestCQ subQuery);
        public void NotInScopeTOutputSettingAsOne(SubQuery<TOutputSettingCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSettingCB>", subQuery);
            TOutputSettingCB cb = new TOutputSettingCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotInScopeSubQuery_TOutputSettingAsOne(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotInScopeSubQuery_TOutputSettingAsOne(TOutputSettingCQ subQuery);
        public void NotInScopeTOutputSettingCrossAsOne(SubQuery<TOutputSettingCrossCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSettingCrossCB>", subQuery);
            TOutputSettingCrossCB cb = new TOutputSettingCrossCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotInScopeSubQuery_TOutputSettingCrossAsOne(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotInScopeSubQuery_TOutputSettingCrossAsOne(TOutputSettingCrossCQ subQuery);
        public void NotInScopeTOutputSettingFaAsOne(SubQuery<TOutputSettingFaCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSettingFaCB>", subQuery);
            TOutputSettingFaCB cb = new TOutputSettingFaCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotInScopeSubQuery_TOutputSettingFaAsOne(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotInScopeSubQuery_TOutputSettingFaAsOne(TOutputSettingFaCQ subQuery);
        public void NotInScopeTOutputSettingGtAsOne(SubQuery<TOutputSettingGtCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSettingGtCB>", subQuery);
            TOutputSettingGtCB cb = new TOutputSettingGtCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotInScopeSubQuery_TOutputSettingGtAsOne(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotInScopeSubQuery_TOutputSettingGtAsOne(TOutputSettingGtCQ subQuery);
        public void NotInScopeTOutputSettingReportAsOne(SubQuery<TOutputSettingReportCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSettingReportCB>", subQuery);
            TOutputSettingReportCB cb = new TOutputSettingReportCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotInScopeSubQuery_TOutputSettingReportAsOne(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotInScopeSubQuery_TOutputSettingReportAsOne(TOutputSettingReportCQ subQuery);
        public void NotInScopeTOutputTemplateList(SubQuery<TOutputTemplateCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputTemplateCB>", subQuery);
            TOutputTemplateCB cb = new TOutputTemplateCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotInScopeSubQuery_TOutputTemplateList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotInScopeSubQuery_TOutputTemplateList(TOutputTemplateCQ subQuery);
        public void NotInScopeTQcwebSurveyDetailList(SubQuery<TQcwebSurveyDetailCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyDetailCB>", subQuery);
            TQcwebSurveyDetailCB cb = new TQcwebSurveyDetailCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotInScopeSubQuery_TQcwebSurveyDetailList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotInScopeSubQuery_TQcwebSurveyDetailList(TQcwebSurveyDetailCQ subQuery);
        public void NotInScopeTRawdataImportQueInfoList(SubQuery<TRawdataImportQueInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TRawdataImportQueInfoCB>", subQuery);
            TRawdataImportQueInfoCB cb = new TRawdataImportQueInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotInScopeSubQuery_TRawdataImportQueInfoList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotInScopeSubQuery_TRawdataImportQueInfoList(TRawdataImportQueInfoCQ subQuery);
        public void NotInScopeTReportsetList(SubQuery<TReportsetCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TReportsetCB>", subQuery);
            TReportsetCB cb = new TReportsetCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotInScopeSubQuery_TReportsetList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotInScopeSubQuery_TReportsetList(TReportsetCQ subQuery);
        public void NotInScopeTScenarioTotalizationList(SubQuery<TScenarioTotalizationCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TScenarioTotalizationCB>", subQuery);
            TScenarioTotalizationCB cb = new TScenarioTotalizationCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotInScopeSubQuery_TScenarioTotalizationList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotInScopeSubQuery_TScenarioTotalizationList(TScenarioTotalizationCQ subQuery);
        public void NotInScopeTSelectConditionInfoList(SubQuery<TSelectConditionInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TSelectConditionInfoCB>", subQuery);
            TSelectConditionInfoCB cb = new TSelectConditionInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotInScopeSubQuery_TSelectConditionInfoList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotInScopeSubQuery_TSelectConditionInfoList(TSelectConditionInfoCQ subQuery);
        public void NotInScopeTSessionControlerList(SubQuery<TSessionControlerCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TSessionControlerCB>", subQuery);
            TSessionControlerCB cb = new TSessionControlerCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotInScopeSubQuery_TSessionControlerList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotInScopeSubQuery_TSessionControlerList(TSessionControlerCQ subQuery);
        public void NotInScopeTWeightbackList(SubQuery<TWeightbackCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TWeightbackCB>", subQuery);
            TWeightbackCB cb = new TWeightbackCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotInScopeSubQuery_TWeightbackList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotInScopeSubQuery_TWeightbackList(TWeightbackCQ subQuery);
        public void xsderiveTAllocationCellInfoList(String function, SubQuery<TAllocationCellInfoCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TAllocationCellInfoCB>", subQuery);
            TAllocationCellInfoCB cb = new TAllocationCellInfoCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_SpecifyDerivedReferrer_TAllocationCellInfoList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName, aliasName);
        }
        abstract public String keepQcwebid_SpecifyDerivedReferrer_TAllocationCellInfoList(TAllocationCellInfoCQ subQuery);
        public void xsderiveTDataEditListList(String function, SubQuery<TDataEditListCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDataEditListCB>", subQuery);
            TDataEditListCB cb = new TDataEditListCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_SpecifyDerivedReferrer_TDataEditListList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName, aliasName);
        }
        abstract public String keepQcwebid_SpecifyDerivedReferrer_TDataEditListList(TDataEditListCQ subQuery);
        public void xsderiveTItemInfoList(String function, SubQuery<TItemInfoCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TItemInfoCB>", subQuery);
            TItemInfoCB cb = new TItemInfoCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_SpecifyDerivedReferrer_TItemInfoList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName, aliasName);
        }
        abstract public String keepQcwebid_SpecifyDerivedReferrer_TItemInfoList(TItemInfoCQ subQuery);
        public void xsderiveTNoticeList(String function, SubQuery<TNoticeCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TNoticeCB>", subQuery);
            TNoticeCB cb = new TNoticeCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_SpecifyDerivedReferrer_TNoticeList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName, aliasName);
        }
        abstract public String keepQcwebid_SpecifyDerivedReferrer_TNoticeList(TNoticeCQ subQuery);
        public void xsderiveTOutputRequestList(String function, SubQuery<TOutputRequestCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputRequestCB>", subQuery);
            TOutputRequestCB cb = new TOutputRequestCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_SpecifyDerivedReferrer_TOutputRequestList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName, aliasName);
        }
        abstract public String keepQcwebid_SpecifyDerivedReferrer_TOutputRequestList(TOutputRequestCQ subQuery);
        public void xsderiveTOutputTemplateList(String function, SubQuery<TOutputTemplateCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputTemplateCB>", subQuery);
            TOutputTemplateCB cb = new TOutputTemplateCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_SpecifyDerivedReferrer_TOutputTemplateList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName, aliasName);
        }
        abstract public String keepQcwebid_SpecifyDerivedReferrer_TOutputTemplateList(TOutputTemplateCQ subQuery);
        public void xsderiveTQcwebSurveyDetailList(String function, SubQuery<TQcwebSurveyDetailCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyDetailCB>", subQuery);
            TQcwebSurveyDetailCB cb = new TQcwebSurveyDetailCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_SpecifyDerivedReferrer_TQcwebSurveyDetailList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName, aliasName);
        }
        abstract public String keepQcwebid_SpecifyDerivedReferrer_TQcwebSurveyDetailList(TQcwebSurveyDetailCQ subQuery);
        public void xsderiveTRawdataImportQueInfoList(String function, SubQuery<TRawdataImportQueInfoCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TRawdataImportQueInfoCB>", subQuery);
            TRawdataImportQueInfoCB cb = new TRawdataImportQueInfoCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_SpecifyDerivedReferrer_TRawdataImportQueInfoList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName, aliasName);
        }
        abstract public String keepQcwebid_SpecifyDerivedReferrer_TRawdataImportQueInfoList(TRawdataImportQueInfoCQ subQuery);
        public void xsderiveTReportsetList(String function, SubQuery<TReportsetCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TReportsetCB>", subQuery);
            TReportsetCB cb = new TReportsetCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_SpecifyDerivedReferrer_TReportsetList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName, aliasName);
        }
        abstract public String keepQcwebid_SpecifyDerivedReferrer_TReportsetList(TReportsetCQ subQuery);
        public void xsderiveTScenarioTotalizationList(String function, SubQuery<TScenarioTotalizationCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TScenarioTotalizationCB>", subQuery);
            TScenarioTotalizationCB cb = new TScenarioTotalizationCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_SpecifyDerivedReferrer_TScenarioTotalizationList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName, aliasName);
        }
        abstract public String keepQcwebid_SpecifyDerivedReferrer_TScenarioTotalizationList(TScenarioTotalizationCQ subQuery);
        public void xsderiveTSelectConditionInfoList(String function, SubQuery<TSelectConditionInfoCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TSelectConditionInfoCB>", subQuery);
            TSelectConditionInfoCB cb = new TSelectConditionInfoCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_SpecifyDerivedReferrer_TSelectConditionInfoList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName, aliasName);
        }
        abstract public String keepQcwebid_SpecifyDerivedReferrer_TSelectConditionInfoList(TSelectConditionInfoCQ subQuery);
        public void xsderiveTSessionControlerList(String function, SubQuery<TSessionControlerCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TSessionControlerCB>", subQuery);
            TSessionControlerCB cb = new TSessionControlerCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_SpecifyDerivedReferrer_TSessionControlerList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName, aliasName);
        }
        abstract public String keepQcwebid_SpecifyDerivedReferrer_TSessionControlerList(TSessionControlerCQ subQuery);
        public void xsderiveTWeightbackList(String function, SubQuery<TWeightbackCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TWeightbackCB>", subQuery);
            TWeightbackCB cb = new TWeightbackCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_SpecifyDerivedReferrer_TWeightbackList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName, aliasName);
        }
        abstract public String keepQcwebid_SpecifyDerivedReferrer_TWeightbackList(TWeightbackCQ subQuery);

        public QDRFunction<TAllocationCellInfoCB> DerivedTAllocationCellInfoList() {
            return xcreateQDRFunctionTAllocationCellInfoList();
        }
        protected QDRFunction<TAllocationCellInfoCB> xcreateQDRFunctionTAllocationCellInfoList() {
            return new QDRFunction<TAllocationCellInfoCB>(delegate(String function, SubQuery<TAllocationCellInfoCB> subQuery, String operand, Object value) {
                xqderiveTAllocationCellInfoList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTAllocationCellInfoList(String function, SubQuery<TAllocationCellInfoCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TAllocationCellInfoCB>", subQuery);
            TAllocationCellInfoCB cb = new TAllocationCellInfoCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_QueryDerivedReferrer_TAllocationCellInfoList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepQcwebid_QueryDerivedReferrer_TAllocationCellInfoListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepQcwebid_QueryDerivedReferrer_TAllocationCellInfoList(TAllocationCellInfoCQ subQuery);
        public abstract String keepQcwebid_QueryDerivedReferrer_TAllocationCellInfoListParameter(Object parameterValue);

        public QDRFunction<TDataEditListCB> DerivedTDataEditListList() {
            return xcreateQDRFunctionTDataEditListList();
        }
        protected QDRFunction<TDataEditListCB> xcreateQDRFunctionTDataEditListList() {
            return new QDRFunction<TDataEditListCB>(delegate(String function, SubQuery<TDataEditListCB> subQuery, String operand, Object value) {
                xqderiveTDataEditListList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTDataEditListList(String function, SubQuery<TDataEditListCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TDataEditListCB>", subQuery);
            TDataEditListCB cb = new TDataEditListCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_QueryDerivedReferrer_TDataEditListList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepQcwebid_QueryDerivedReferrer_TDataEditListListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepQcwebid_QueryDerivedReferrer_TDataEditListList(TDataEditListCQ subQuery);
        public abstract String keepQcwebid_QueryDerivedReferrer_TDataEditListListParameter(Object parameterValue);

        public QDRFunction<TItemInfoCB> DerivedTItemInfoList() {
            return xcreateQDRFunctionTItemInfoList();
        }
        protected QDRFunction<TItemInfoCB> xcreateQDRFunctionTItemInfoList() {
            return new QDRFunction<TItemInfoCB>(delegate(String function, SubQuery<TItemInfoCB> subQuery, String operand, Object value) {
                xqderiveTItemInfoList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTItemInfoList(String function, SubQuery<TItemInfoCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TItemInfoCB>", subQuery);
            TItemInfoCB cb = new TItemInfoCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_QueryDerivedReferrer_TItemInfoList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepQcwebid_QueryDerivedReferrer_TItemInfoListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepQcwebid_QueryDerivedReferrer_TItemInfoList(TItemInfoCQ subQuery);
        public abstract String keepQcwebid_QueryDerivedReferrer_TItemInfoListParameter(Object parameterValue);

        public QDRFunction<TNoticeCB> DerivedTNoticeList() {
            return xcreateQDRFunctionTNoticeList();
        }
        protected QDRFunction<TNoticeCB> xcreateQDRFunctionTNoticeList() {
            return new QDRFunction<TNoticeCB>(delegate(String function, SubQuery<TNoticeCB> subQuery, String operand, Object value) {
                xqderiveTNoticeList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTNoticeList(String function, SubQuery<TNoticeCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TNoticeCB>", subQuery);
            TNoticeCB cb = new TNoticeCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_QueryDerivedReferrer_TNoticeList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepQcwebid_QueryDerivedReferrer_TNoticeListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepQcwebid_QueryDerivedReferrer_TNoticeList(TNoticeCQ subQuery);
        public abstract String keepQcwebid_QueryDerivedReferrer_TNoticeListParameter(Object parameterValue);

        public QDRFunction<TOutputRequestCB> DerivedTOutputRequestList() {
            return xcreateQDRFunctionTOutputRequestList();
        }
        protected QDRFunction<TOutputRequestCB> xcreateQDRFunctionTOutputRequestList() {
            return new QDRFunction<TOutputRequestCB>(delegate(String function, SubQuery<TOutputRequestCB> subQuery, String operand, Object value) {
                xqderiveTOutputRequestList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTOutputRequestList(String function, SubQuery<TOutputRequestCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TOutputRequestCB>", subQuery);
            TOutputRequestCB cb = new TOutputRequestCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_QueryDerivedReferrer_TOutputRequestList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepQcwebid_QueryDerivedReferrer_TOutputRequestListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepQcwebid_QueryDerivedReferrer_TOutputRequestList(TOutputRequestCQ subQuery);
        public abstract String keepQcwebid_QueryDerivedReferrer_TOutputRequestListParameter(Object parameterValue);

        public QDRFunction<TOutputTemplateCB> DerivedTOutputTemplateList() {
            return xcreateQDRFunctionTOutputTemplateList();
        }
        protected QDRFunction<TOutputTemplateCB> xcreateQDRFunctionTOutputTemplateList() {
            return new QDRFunction<TOutputTemplateCB>(delegate(String function, SubQuery<TOutputTemplateCB> subQuery, String operand, Object value) {
                xqderiveTOutputTemplateList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTOutputTemplateList(String function, SubQuery<TOutputTemplateCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TOutputTemplateCB>", subQuery);
            TOutputTemplateCB cb = new TOutputTemplateCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_QueryDerivedReferrer_TOutputTemplateList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepQcwebid_QueryDerivedReferrer_TOutputTemplateListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepQcwebid_QueryDerivedReferrer_TOutputTemplateList(TOutputTemplateCQ subQuery);
        public abstract String keepQcwebid_QueryDerivedReferrer_TOutputTemplateListParameter(Object parameterValue);

        public QDRFunction<TQcwebSurveyDetailCB> DerivedTQcwebSurveyDetailList() {
            return xcreateQDRFunctionTQcwebSurveyDetailList();
        }
        protected QDRFunction<TQcwebSurveyDetailCB> xcreateQDRFunctionTQcwebSurveyDetailList() {
            return new QDRFunction<TQcwebSurveyDetailCB>(delegate(String function, SubQuery<TQcwebSurveyDetailCB> subQuery, String operand, Object value) {
                xqderiveTQcwebSurveyDetailList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTQcwebSurveyDetailList(String function, SubQuery<TQcwebSurveyDetailCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TQcwebSurveyDetailCB>", subQuery);
            TQcwebSurveyDetailCB cb = new TQcwebSurveyDetailCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_QueryDerivedReferrer_TQcwebSurveyDetailList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepQcwebid_QueryDerivedReferrer_TQcwebSurveyDetailListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepQcwebid_QueryDerivedReferrer_TQcwebSurveyDetailList(TQcwebSurveyDetailCQ subQuery);
        public abstract String keepQcwebid_QueryDerivedReferrer_TQcwebSurveyDetailListParameter(Object parameterValue);

        public QDRFunction<TRawdataImportQueInfoCB> DerivedTRawdataImportQueInfoList() {
            return xcreateQDRFunctionTRawdataImportQueInfoList();
        }
        protected QDRFunction<TRawdataImportQueInfoCB> xcreateQDRFunctionTRawdataImportQueInfoList() {
            return new QDRFunction<TRawdataImportQueInfoCB>(delegate(String function, SubQuery<TRawdataImportQueInfoCB> subQuery, String operand, Object value) {
                xqderiveTRawdataImportQueInfoList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTRawdataImportQueInfoList(String function, SubQuery<TRawdataImportQueInfoCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TRawdataImportQueInfoCB>", subQuery);
            TRawdataImportQueInfoCB cb = new TRawdataImportQueInfoCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_QueryDerivedReferrer_TRawdataImportQueInfoList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepQcwebid_QueryDerivedReferrer_TRawdataImportQueInfoListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepQcwebid_QueryDerivedReferrer_TRawdataImportQueInfoList(TRawdataImportQueInfoCQ subQuery);
        public abstract String keepQcwebid_QueryDerivedReferrer_TRawdataImportQueInfoListParameter(Object parameterValue);

        public QDRFunction<TReportsetCB> DerivedTReportsetList() {
            return xcreateQDRFunctionTReportsetList();
        }
        protected QDRFunction<TReportsetCB> xcreateQDRFunctionTReportsetList() {
            return new QDRFunction<TReportsetCB>(delegate(String function, SubQuery<TReportsetCB> subQuery, String operand, Object value) {
                xqderiveTReportsetList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTReportsetList(String function, SubQuery<TReportsetCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TReportsetCB>", subQuery);
            TReportsetCB cb = new TReportsetCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_QueryDerivedReferrer_TReportsetList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepQcwebid_QueryDerivedReferrer_TReportsetListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepQcwebid_QueryDerivedReferrer_TReportsetList(TReportsetCQ subQuery);
        public abstract String keepQcwebid_QueryDerivedReferrer_TReportsetListParameter(Object parameterValue);

        public QDRFunction<TScenarioTotalizationCB> DerivedTScenarioTotalizationList() {
            return xcreateQDRFunctionTScenarioTotalizationList();
        }
        protected QDRFunction<TScenarioTotalizationCB> xcreateQDRFunctionTScenarioTotalizationList() {
            return new QDRFunction<TScenarioTotalizationCB>(delegate(String function, SubQuery<TScenarioTotalizationCB> subQuery, String operand, Object value) {
                xqderiveTScenarioTotalizationList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTScenarioTotalizationList(String function, SubQuery<TScenarioTotalizationCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TScenarioTotalizationCB>", subQuery);
            TScenarioTotalizationCB cb = new TScenarioTotalizationCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_QueryDerivedReferrer_TScenarioTotalizationList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepQcwebid_QueryDerivedReferrer_TScenarioTotalizationListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepQcwebid_QueryDerivedReferrer_TScenarioTotalizationList(TScenarioTotalizationCQ subQuery);
        public abstract String keepQcwebid_QueryDerivedReferrer_TScenarioTotalizationListParameter(Object parameterValue);

        public QDRFunction<TSelectConditionInfoCB> DerivedTSelectConditionInfoList() {
            return xcreateQDRFunctionTSelectConditionInfoList();
        }
        protected QDRFunction<TSelectConditionInfoCB> xcreateQDRFunctionTSelectConditionInfoList() {
            return new QDRFunction<TSelectConditionInfoCB>(delegate(String function, SubQuery<TSelectConditionInfoCB> subQuery, String operand, Object value) {
                xqderiveTSelectConditionInfoList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTSelectConditionInfoList(String function, SubQuery<TSelectConditionInfoCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TSelectConditionInfoCB>", subQuery);
            TSelectConditionInfoCB cb = new TSelectConditionInfoCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_QueryDerivedReferrer_TSelectConditionInfoList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepQcwebid_QueryDerivedReferrer_TSelectConditionInfoListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepQcwebid_QueryDerivedReferrer_TSelectConditionInfoList(TSelectConditionInfoCQ subQuery);
        public abstract String keepQcwebid_QueryDerivedReferrer_TSelectConditionInfoListParameter(Object parameterValue);

        public QDRFunction<TSessionControlerCB> DerivedTSessionControlerList() {
            return xcreateQDRFunctionTSessionControlerList();
        }
        protected QDRFunction<TSessionControlerCB> xcreateQDRFunctionTSessionControlerList() {
            return new QDRFunction<TSessionControlerCB>(delegate(String function, SubQuery<TSessionControlerCB> subQuery, String operand, Object value) {
                xqderiveTSessionControlerList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTSessionControlerList(String function, SubQuery<TSessionControlerCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TSessionControlerCB>", subQuery);
            TSessionControlerCB cb = new TSessionControlerCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_QueryDerivedReferrer_TSessionControlerList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepQcwebid_QueryDerivedReferrer_TSessionControlerListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepQcwebid_QueryDerivedReferrer_TSessionControlerList(TSessionControlerCQ subQuery);
        public abstract String keepQcwebid_QueryDerivedReferrer_TSessionControlerListParameter(Object parameterValue);

        public QDRFunction<TWeightbackCB> DerivedTWeightbackList() {
            return xcreateQDRFunctionTWeightbackList();
        }
        protected QDRFunction<TWeightbackCB> xcreateQDRFunctionTWeightbackList() {
            return new QDRFunction<TWeightbackCB>(delegate(String function, SubQuery<TWeightbackCB> subQuery, String operand, Object value) {
                xqderiveTWeightbackList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTWeightbackList(String function, SubQuery<TWeightbackCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TWeightbackCB>", subQuery);
            TWeightbackCB cb = new TWeightbackCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_QueryDerivedReferrer_TWeightbackList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepQcwebid_QueryDerivedReferrer_TWeightbackListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepQcwebid_QueryDerivedReferrer_TWeightbackList(TWeightbackCQ subQuery);
        public abstract String keepQcwebid_QueryDerivedReferrer_TWeightbackListParameter(Object parameterValue);
        public void SetQcwebid_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQcwebid(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQcwebid_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQcwebid(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQcwebid(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQcwebid(), "QCWEBID");
        }
        protected abstract ConditionValue getCValueQcwebid();

        public void SetAddDataNo_Equal(long? v) { regAddDataNo(CK_EQ, v); }
        public void SetAddDataNo_NotEqual(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAddDataNo(CK_NES, v);
        }
        public void SetAddDataNo_GreaterThan(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAddDataNo(CK_GT, v);
        }
        public void SetAddDataNo_LessThan(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAddDataNo(CK_LT, v);
        }
        public void SetAddDataNo_GreaterEqual(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAddDataNo(CK_GE, v);
        }
        public void SetAddDataNo_LessEqual(long? v) {
            WhereSetterFlag = true;
            regAddDataNo(CK_LE, v);
        }
        public void SetAddDataNo_InScope(IList<long?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<long?>(CK_INS, cTL<long?>(ls), getCValueAddDataNo(), "ADD_DATA_NO");
        }
        public void SetAddDataNo_NotInScope(IList<long?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<long?>(CK_NINS, cTL<long?>(ls), getCValueAddDataNo(), "ADD_DATA_NO");
        }
        public void SetAddDataNo_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAddDataNo(CK_ISN, DUMMY_OBJECT);
        }
        public void SetAddDataNo_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAddDataNo(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regAddDataNo(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueAddDataNo(), "ADD_DATA_NO");
        }
        protected abstract ConditionValue getCValueAddDataNo();

        public void SetSurveyNameOrg_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetSurveyNameOrg_Equal(fRES(v));
        }
        protected void DoSetSurveyNameOrg_Equal(String v) { regSurveyNameOrg(CK_EQ, v); }
        public void SetSurveyNameOrg_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetSurveyNameOrg_NotEqual(fRES(v));
        }
        protected void DoSetSurveyNameOrg_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyNameOrg(CK_NES, v);
        }
        public void SetSurveyNameOrg_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyNameOrg(CK_GT, fRES(v));
        }
        public void SetSurveyNameOrg_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyNameOrg(CK_LT, fRES(v));
        }
        public void SetSurveyNameOrg_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyNameOrg(CK_GE, fRES(v));
        }
        public void SetSurveyNameOrg_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyNameOrg(CK_LE, fRES(v));
        }
        public void SetSurveyNameOrg_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueSurveyNameOrg(), "SURVEY_NAME_ORG");
        }
        public void SetSurveyNameOrg_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueSurveyNameOrg(), "SURVEY_NAME_ORG");
        }
        public void SetSurveyNameOrg_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetSurveyNameOrg_LikeSearch(v, cLSOP());
        }
        public void SetSurveyNameOrg_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueSurveyNameOrg(), "SURVEY_NAME_ORG", option);
        }
        public void SetSurveyNameOrg_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueSurveyNameOrg(), "SURVEY_NAME_ORG", option);
        }
        protected void regSurveyNameOrg(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueSurveyNameOrg(), "SURVEY_NAME_ORG");
        }
        protected abstract ConditionValue getCValueSurveyNameOrg();

        public void SetImportDatetime_Equal(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regImportDatetime(CK_EQ, v);
        }
        public void SetImportDatetime_GreaterThan(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regImportDatetime(CK_GT, v);
        }
        public void SetImportDatetime_LessThan(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regImportDatetime(CK_LT, v);
        }
        public void SetImportDatetime_GreaterEqual(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regImportDatetime(CK_GE, v);
        }
        public void SetImportDatetime_LessEqual(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regImportDatetime(CK_LE, v);
        }
        public void SetImportDatetime_FromTo(DateTime? from, DateTime? to, FromToOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFTQ(from, to, getCValueImportDatetime(), "IMPORT_DATETIME", option);
        }
        public void SetImportDatetime_DateFromTo(DateTime? from, DateTime? to) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetImportDatetime_FromTo(from, to, new DateFromToOption());
        }
        protected void regImportDatetime(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueImportDatetime(), "IMPORT_DATETIME");
        }
        protected abstract ConditionValue getCValueImportDatetime();

        public void SetImportFileName_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetImportFileName_Equal(fRES(v));
        }
        protected void DoSetImportFileName_Equal(String v) { regImportFileName(CK_EQ, v); }
        public void SetImportFileName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetImportFileName_NotEqual(fRES(v));
        }
        protected void DoSetImportFileName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regImportFileName(CK_NES, v);
        }
        public void SetImportFileName_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regImportFileName(CK_GT, fRES(v));
        }
        public void SetImportFileName_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regImportFileName(CK_LT, fRES(v));
        }
        public void SetImportFileName_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regImportFileName(CK_GE, fRES(v));
        }
        public void SetImportFileName_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regImportFileName(CK_LE, fRES(v));
        }
        public void SetImportFileName_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueImportFileName(), "IMPORT_FILE_NAME");
        }
        public void SetImportFileName_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueImportFileName(), "IMPORT_FILE_NAME");
        }
        public void SetImportFileName_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetImportFileName_LikeSearch(v, cLSOP());
        }
        public void SetImportFileName_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueImportFileName(), "IMPORT_FILE_NAME", option);
        }
        public void SetImportFileName_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueImportFileName(), "IMPORT_FILE_NAME", option);
        }
        protected void regImportFileName(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueImportFileName(), "IMPORT_FILE_NAME");
        }
        protected abstract ConditionValue getCValueImportFileName();

        public void SetDeleteFlag_Equal(int? v) { regDeleteFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of deleteFlag as equal. { = }
        /// はい: 削除を示す
        /// </summary>
        public void SetDeleteFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.DeleteFlag.True.Code;
            regDeleteFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of deleteFlag as equal. { = }
        /// いいえ: 未削除を示す
        /// </summary>
        public void SetDeleteFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.DeleteFlag.False.Code;
            regDeleteFlag(CK_EQ, int.Parse(code));
        }
        public void SetDeleteFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDeleteFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of deleteFlag as notEqual. { &lt;&gt; }
        /// はい: 削除を示す
        /// </summary>
        public void SetDeleteFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.DeleteFlag.True.Code;
            regDeleteFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of deleteFlag as notEqual. { &lt;&gt; }
        /// いいえ: 未削除を示す
        /// </summary>
        public void SetDeleteFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.DeleteFlag.False.Code;
            regDeleteFlag(CK_NES, int.Parse(code));
        }
        public void SetDeleteFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueDeleteFlag(), "DELETE_FLAG");
        }
        public void SetDeleteFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueDeleteFlag(), "DELETE_FLAG");
        }
        protected void regDeleteFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDeleteFlag(), "DELETE_FLAG");
        }
        protected abstract ConditionValue getCValueDeleteFlag();

        public void SetViewSurveyName_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetViewSurveyName_Equal(fRES(v));
        }
        protected void DoSetViewSurveyName_Equal(String v) { regViewSurveyName(CK_EQ, v); }
        public void SetViewSurveyName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetViewSurveyName_NotEqual(fRES(v));
        }
        protected void DoSetViewSurveyName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regViewSurveyName(CK_NES, v);
        }
        public void SetViewSurveyName_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regViewSurveyName(CK_GT, fRES(v));
        }
        public void SetViewSurveyName_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regViewSurveyName(CK_LT, fRES(v));
        }
        public void SetViewSurveyName_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regViewSurveyName(CK_GE, fRES(v));
        }
        public void SetViewSurveyName_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regViewSurveyName(CK_LE, fRES(v));
        }
        public void SetViewSurveyName_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueViewSurveyName(), "VIEW_SURVEY_NAME");
        }
        public void SetViewSurveyName_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueViewSurveyName(), "VIEW_SURVEY_NAME");
        }
        public void SetViewSurveyName_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetViewSurveyName_LikeSearch(v, cLSOP());
        }
        public void SetViewSurveyName_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueViewSurveyName(), "VIEW_SURVEY_NAME", option);
        }
        public void SetViewSurveyName_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueViewSurveyName(), "VIEW_SURVEY_NAME", option);
        }
        protected void regViewSurveyName(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueViewSurveyName(), "VIEW_SURVEY_NAME");
        }
        protected abstract ConditionValue getCValueViewSurveyName();

        public void SetGtCount_Equal(int? v) { regGtCount(CK_EQ, v); }
        public void SetGtCount_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGtCount(CK_NES, v);
        }
        public void SetGtCount_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGtCount(CK_GT, v);
        }
        public void SetGtCount_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGtCount(CK_LT, v);
        }
        public void SetGtCount_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGtCount(CK_GE, v);
        }
        public void SetGtCount_LessEqual(int? v) {
            WhereSetterFlag = true;
            regGtCount(CK_LE, v);
        }
        public void SetGtCount_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueGtCount(), "GT_COUNT");
        }
        public void SetGtCount_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueGtCount(), "GT_COUNT");
        }
        protected void regGtCount(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueGtCount(), "GT_COUNT");
        }
        protected abstract ConditionValue getCValueGtCount();

        public void SetCrossCount_Equal(int? v) { regCrossCount(CK_EQ, v); }
        public void SetCrossCount_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCrossCount(CK_NES, v);
        }
        public void SetCrossCount_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCrossCount(CK_GT, v);
        }
        public void SetCrossCount_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCrossCount(CK_LT, v);
        }
        public void SetCrossCount_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCrossCount(CK_GE, v);
        }
        public void SetCrossCount_LessEqual(int? v) {
            WhereSetterFlag = true;
            regCrossCount(CK_LE, v);
        }
        public void SetCrossCount_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueCrossCount(), "CROSS_COUNT");
        }
        public void SetCrossCount_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueCrossCount(), "CROSS_COUNT");
        }
        protected void regCrossCount(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueCrossCount(), "CROSS_COUNT");
        }
        protected abstract ConditionValue getCValueCrossCount();

        public void SetFaCount_Equal(int? v) { regFaCount(CK_EQ, v); }
        public void SetFaCount_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFaCount(CK_NES, v);
        }
        public void SetFaCount_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFaCount(CK_GT, v);
        }
        public void SetFaCount_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFaCount(CK_LT, v);
        }
        public void SetFaCount_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFaCount(CK_GE, v);
        }
        public void SetFaCount_LessEqual(int? v) {
            WhereSetterFlag = true;
            regFaCount(CK_LE, v);
        }
        public void SetFaCount_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueFaCount(), "FA_COUNT");
        }
        public void SetFaCount_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueFaCount(), "FA_COUNT");
        }
        protected void regFaCount(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFaCount(), "FA_COUNT");
        }
        protected abstract ConditionValue getCValueFaCount();

        public void SetVersionNo_Equal(decimal? v) { regVersionNo(CK_EQ, v); }
        public void SetVersionNo_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regVersionNo(CK_NES, v);
        }
        public void SetVersionNo_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regVersionNo(CK_GT, v);
        }
        public void SetVersionNo_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regVersionNo(CK_LT, v);
        }
        public void SetVersionNo_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regVersionNo(CK_GE, v);
        }
        public void SetVersionNo_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regVersionNo(CK_LE, v);
        }
        public void SetVersionNo_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueVersionNo(), "VERSION_NO");
        }
        public void SetVersionNo_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueVersionNo(), "VERSION_NO");
        }
        protected void regVersionNo(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueVersionNo(), "VERSION_NO");
        }
        protected abstract ConditionValue getCValueVersionNo();

        public void SetLastUpdateUser_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetLastUpdateUser_Equal(fRES(v));
        }
        protected void DoSetLastUpdateUser_Equal(String v) { regLastUpdateUser(CK_EQ, v); }
        public void SetLastUpdateUser_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetLastUpdateUser_NotEqual(fRES(v));
        }
        protected void DoSetLastUpdateUser_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateUser(CK_NES, v);
        }
        public void SetLastUpdateUser_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateUser(CK_GT, fRES(v));
        }
        public void SetLastUpdateUser_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateUser(CK_LT, fRES(v));
        }
        public void SetLastUpdateUser_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateUser(CK_GE, fRES(v));
        }
        public void SetLastUpdateUser_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateUser(CK_LE, fRES(v));
        }
        public void SetLastUpdateUser_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueLastUpdateUser(), "LAST_UPDATE_USER");
        }
        public void SetLastUpdateUser_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueLastUpdateUser(), "LAST_UPDATE_USER");
        }
        public void SetLastUpdateUser_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetLastUpdateUser_LikeSearch(v, cLSOP());
        }
        public void SetLastUpdateUser_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueLastUpdateUser(), "LAST_UPDATE_USER", option);
        }
        public void SetLastUpdateUser_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueLastUpdateUser(), "LAST_UPDATE_USER", option);
        }
        public void SetLastUpdateUser_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateUser(CK_ISN, DUMMY_OBJECT);
        }
        public void SetLastUpdateUser_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateUser(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regLastUpdateUser(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueLastUpdateUser(), "LAST_UPDATE_USER");
        }
        protected abstract ConditionValue getCValueLastUpdateUser();

        public void SetLastUpdateDatetime_Equal(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateDatetime(CK_EQ, v);
        }
        public void SetLastUpdateDatetime_GreaterThan(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateDatetime(CK_GT, v);
        }
        public void SetLastUpdateDatetime_LessThan(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateDatetime(CK_LT, v);
        }
        public void SetLastUpdateDatetime_GreaterEqual(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateDatetime(CK_GE, v);
        }
        public void SetLastUpdateDatetime_LessEqual(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateDatetime(CK_LE, v);
        }
        public void SetLastUpdateDatetime_FromTo(DateTime? from, DateTime? to, FromToOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFTQ(from, to, getCValueLastUpdateDatetime(), "LAST_UPDATE_DATETIME", option);
        }
        public void SetLastUpdateDatetime_DateFromTo(DateTime? from, DateTime? to) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetLastUpdateDatetime_FromTo(from, to, new DateFromToOption());
        }
        public void SetLastUpdateDatetime_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateDatetime(CK_ISN, DUMMY_OBJECT);
        }
        public void SetLastUpdateDatetime_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateDatetime(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regLastUpdateDatetime(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueLastUpdateDatetime(), "LAST_UPDATE_DATETIME");
        }
        protected abstract ConditionValue getCValueLastUpdateDatetime();

        public void SetSurveyInfoId_Equal(decimal? v) { regSurveyInfoId(CK_EQ, v); }
        public void SetSurveyInfoId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyInfoId(CK_NES, v);
        }
        public void SetSurveyInfoId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyInfoId(CK_GT, v);
        }
        public void SetSurveyInfoId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyInfoId(CK_LT, v);
        }
        public void SetSurveyInfoId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyInfoId(CK_GE, v);
        }
        public void SetSurveyInfoId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regSurveyInfoId(CK_LE, v);
        }
        public void SetSurveyInfoId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueSurveyInfoId(), "SURVEY_INFO_ID");
        }
        public void SetSurveyInfoId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueSurveyInfoId(), "SURVEY_INFO_ID");
        }
        public void InScopeTSurveyInfo(SubQuery<TSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TSurveyInfoCB>", subQuery);
            TSurveyInfoCB cb = new TSurveyInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepSurveyInfoId_InScopeSubQuery_TSurveyInfo(cb.Query());
            registerInScopeSubQuery(cb.Query(), "SURVEY_INFO_ID", "SURVEY_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepSurveyInfoId_InScopeSubQuery_TSurveyInfo(TSurveyInfoCQ subQuery);
        public void NotInScopeTSurveyInfo(SubQuery<TSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TSurveyInfoCB>", subQuery);
            TSurveyInfoCB cb = new TSurveyInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepSurveyInfoId_NotInScopeSubQuery_TSurveyInfo(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "SURVEY_INFO_ID", "SURVEY_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepSurveyInfoId_NotInScopeSubQuery_TSurveyInfo(TSurveyInfoCQ subQuery);
        protected void regSurveyInfoId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueSurveyInfoId(), "SURVEY_INFO_ID");
        }
        protected abstract ConditionValue getCValueSurveyInfoId();

        public void SetRawdataImportQueInfoId_Equal(decimal? v) { regRawdataImportQueInfoId(CK_EQ, v); }
        public void SetRawdataImportQueInfoId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRawdataImportQueInfoId(CK_NES, v);
        }
        public void SetRawdataImportQueInfoId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRawdataImportQueInfoId(CK_GT, v);
        }
        public void SetRawdataImportQueInfoId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRawdataImportQueInfoId(CK_LT, v);
        }
        public void SetRawdataImportQueInfoId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRawdataImportQueInfoId(CK_GE, v);
        }
        public void SetRawdataImportQueInfoId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regRawdataImportQueInfoId(CK_LE, v);
        }
        public void SetRawdataImportQueInfoId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueRawdataImportQueInfoId(), "RAWDATA_IMPORT_QUE_INFO_ID");
        }
        public void SetRawdataImportQueInfoId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueRawdataImportQueInfoId(), "RAWDATA_IMPORT_QUE_INFO_ID");
        }
        public void InScopeTRawdataImportQueInfo(SubQuery<TRawdataImportQueInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TRawdataImportQueInfoCB>", subQuery);
            TRawdataImportQueInfoCB cb = new TRawdataImportQueInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepRawdataImportQueInfoId_InScopeSubQuery_TRawdataImportQueInfo(cb.Query());
            registerInScopeSubQuery(cb.Query(), "RAWDATA_IMPORT_QUE_INFO_ID", "RAWDATA_IMPORT_QUE_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepRawdataImportQueInfoId_InScopeSubQuery_TRawdataImportQueInfo(TRawdataImportQueInfoCQ subQuery);
        public void NotInScopeTRawdataImportQueInfo(SubQuery<TRawdataImportQueInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TRawdataImportQueInfoCB>", subQuery);
            TRawdataImportQueInfoCB cb = new TRawdataImportQueInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepRawdataImportQueInfoId_NotInScopeSubQuery_TRawdataImportQueInfo(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "RAWDATA_IMPORT_QUE_INFO_ID", "RAWDATA_IMPORT_QUE_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepRawdataImportQueInfoId_NotInScopeSubQuery_TRawdataImportQueInfo(TRawdataImportQueInfoCQ subQuery);
        protected void regRawdataImportQueInfoId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueRawdataImportQueInfoId(), "RAWDATA_IMPORT_QUE_INFO_ID");
        }
        protected abstract ConditionValue getCValueRawdataImportQueInfoId();

        public void SetUtf8Flag_Equal(int? v) { regUtf8Flag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of utf8Flag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetUtf8Flag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regUtf8Flag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of utf8Flag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetUtf8Flag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regUtf8Flag(CK_EQ, int.Parse(code));
        }
        public void SetUtf8Flag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regUtf8Flag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of utf8Flag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetUtf8Flag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regUtf8Flag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of utf8Flag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetUtf8Flag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regUtf8Flag(CK_NES, int.Parse(code));
        }
        public void SetUtf8Flag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueUtf8Flag(), "UTF8_FLAG");
        }
        public void SetUtf8Flag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueUtf8Flag(), "UTF8_FLAG");
        }
        protected void regUtf8Flag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueUtf8Flag(), "UTF8_FLAG");
        }
        protected abstract ConditionValue getCValueUtf8Flag();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TQcwebSurveyInfoCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TQcwebSurveyInfoCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TQcwebSurveyInfoCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TQcwebSurveyInfoCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TQcwebSurveyInfoCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TQcwebSurveyInfoCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TQcwebSurveyInfoCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TQcwebSurveyInfoCB>(delegate(String function, SubQuery<TQcwebSurveyInfoCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TQcwebSurveyInfoCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TQcwebSurveyInfoCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TQcwebSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TQcwebSurveyInfoCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
