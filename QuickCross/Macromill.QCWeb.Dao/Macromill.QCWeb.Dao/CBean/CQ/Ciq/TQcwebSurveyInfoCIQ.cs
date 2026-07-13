
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
    public class TQcwebSurveyInfoCIQ : AbstractBsTQcwebSurveyInfoCQ {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected BsTQcwebSurveyInfoCQ _myCQ;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TQcwebSurveyInfoCIQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel, BsTQcwebSurveyInfoCQ myCQ)
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


        public override String keepQcwebid_ExistsSubQuery_TAccessPermissionsInfoAsOne(TAccessPermissionsInfoCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_ExistsSubQuery_TAccessPermissionsInfoAsOne(subQuery);
        }

        public override String keepQcwebid_ExistsSubQuery_TAllocationCellInfoList(TAllocationCellInfoCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_ExistsSubQuery_TAllocationCellInfoList(subQuery);
        }

        public override String keepQcwebid_ExistsSubQuery_TDataEditListList(TDataEditListCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_ExistsSubQuery_TDataEditListList(subQuery);
        }

        public override String keepQcwebid_ExistsSubQuery_TItemInfoList(TItemInfoCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_ExistsSubQuery_TItemInfoList(subQuery);
        }

        public override String keepQcwebid_ExistsSubQuery_TNoticeList(TNoticeCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_ExistsSubQuery_TNoticeList(subQuery);
        }

        public override String keepQcwebid_ExistsSubQuery_TOutputRequestList(TOutputRequestCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_ExistsSubQuery_TOutputRequestList(subQuery);
        }

        public override String keepQcwebid_ExistsSubQuery_TOutputSettingAsOne(TOutputSettingCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_ExistsSubQuery_TOutputSettingAsOne(subQuery);
        }

        public override String keepQcwebid_ExistsSubQuery_TOutputSettingCrossAsOne(TOutputSettingCrossCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_ExistsSubQuery_TOutputSettingCrossAsOne(subQuery);
        }

        public override String keepQcwebid_ExistsSubQuery_TOutputSettingFaAsOne(TOutputSettingFaCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_ExistsSubQuery_TOutputSettingFaAsOne(subQuery);
        }

        public override String keepQcwebid_ExistsSubQuery_TOutputSettingGtAsOne(TOutputSettingGtCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_ExistsSubQuery_TOutputSettingGtAsOne(subQuery);
        }

        public override String keepQcwebid_ExistsSubQuery_TOutputSettingReportAsOne(TOutputSettingReportCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_ExistsSubQuery_TOutputSettingReportAsOne(subQuery);
        }

        public override String keepQcwebid_ExistsSubQuery_TOutputTemplateList(TOutputTemplateCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_ExistsSubQuery_TOutputTemplateList(subQuery);
        }

        public override String keepQcwebid_ExistsSubQuery_TQcwebSurveyDetailList(TQcwebSurveyDetailCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_ExistsSubQuery_TQcwebSurveyDetailList(subQuery);
        }

        public override String keepQcwebid_ExistsSubQuery_TRawdataImportQueInfoList(TRawdataImportQueInfoCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_ExistsSubQuery_TRawdataImportQueInfoList(subQuery);
        }

        public override String keepQcwebid_ExistsSubQuery_TReportsetList(TReportsetCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_ExistsSubQuery_TReportsetList(subQuery);
        }

        public override String keepQcwebid_ExistsSubQuery_TScenarioTotalizationList(TScenarioTotalizationCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_ExistsSubQuery_TScenarioTotalizationList(subQuery);
        }

        public override String keepQcwebid_ExistsSubQuery_TSelectConditionInfoList(TSelectConditionInfoCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_ExistsSubQuery_TSelectConditionInfoList(subQuery);
        }

        public override String keepQcwebid_ExistsSubQuery_TSessionControlerList(TSessionControlerCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_ExistsSubQuery_TSessionControlerList(subQuery);
        }

        public override String keepQcwebid_ExistsSubQuery_TWeightbackList(TWeightbackCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_ExistsSubQuery_TWeightbackList(subQuery);
        }

        public override String keepQcwebid_NotExistsSubQuery_TAccessPermissionsInfoAsOne(TAccessPermissionsInfoCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_NotExistsSubQuery_TAccessPermissionsInfoAsOne(subQuery);
        }

        public override String keepQcwebid_NotExistsSubQuery_TAllocationCellInfoList(TAllocationCellInfoCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_NotExistsSubQuery_TAllocationCellInfoList(subQuery);
        }

        public override String keepQcwebid_NotExistsSubQuery_TDataEditListList(TDataEditListCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_NotExistsSubQuery_TDataEditListList(subQuery);
        }

        public override String keepQcwebid_NotExistsSubQuery_TItemInfoList(TItemInfoCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_NotExistsSubQuery_TItemInfoList(subQuery);
        }

        public override String keepQcwebid_NotExistsSubQuery_TNoticeList(TNoticeCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_NotExistsSubQuery_TNoticeList(subQuery);
        }

        public override String keepQcwebid_NotExistsSubQuery_TOutputRequestList(TOutputRequestCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_NotExistsSubQuery_TOutputRequestList(subQuery);
        }

        public override String keepQcwebid_NotExistsSubQuery_TOutputSettingAsOne(TOutputSettingCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_NotExistsSubQuery_TOutputSettingAsOne(subQuery);
        }

        public override String keepQcwebid_NotExistsSubQuery_TOutputSettingCrossAsOne(TOutputSettingCrossCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_NotExistsSubQuery_TOutputSettingCrossAsOne(subQuery);
        }

        public override String keepQcwebid_NotExistsSubQuery_TOutputSettingFaAsOne(TOutputSettingFaCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_NotExistsSubQuery_TOutputSettingFaAsOne(subQuery);
        }

        public override String keepQcwebid_NotExistsSubQuery_TOutputSettingGtAsOne(TOutputSettingGtCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_NotExistsSubQuery_TOutputSettingGtAsOne(subQuery);
        }

        public override String keepQcwebid_NotExistsSubQuery_TOutputSettingReportAsOne(TOutputSettingReportCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_NotExistsSubQuery_TOutputSettingReportAsOne(subQuery);
        }

        public override String keepQcwebid_NotExistsSubQuery_TOutputTemplateList(TOutputTemplateCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_NotExistsSubQuery_TOutputTemplateList(subQuery);
        }

        public override String keepQcwebid_NotExistsSubQuery_TQcwebSurveyDetailList(TQcwebSurveyDetailCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_NotExistsSubQuery_TQcwebSurveyDetailList(subQuery);
        }

        public override String keepQcwebid_NotExistsSubQuery_TRawdataImportQueInfoList(TRawdataImportQueInfoCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_NotExistsSubQuery_TRawdataImportQueInfoList(subQuery);
        }

        public override String keepQcwebid_NotExistsSubQuery_TReportsetList(TReportsetCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_NotExistsSubQuery_TReportsetList(subQuery);
        }

        public override String keepQcwebid_NotExistsSubQuery_TScenarioTotalizationList(TScenarioTotalizationCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_NotExistsSubQuery_TScenarioTotalizationList(subQuery);
        }

        public override String keepQcwebid_NotExistsSubQuery_TSelectConditionInfoList(TSelectConditionInfoCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_NotExistsSubQuery_TSelectConditionInfoList(subQuery);
        }

        public override String keepQcwebid_NotExistsSubQuery_TSessionControlerList(TSessionControlerCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_NotExistsSubQuery_TSessionControlerList(subQuery);
        }

        public override String keepQcwebid_NotExistsSubQuery_TWeightbackList(TWeightbackCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_NotExistsSubQuery_TWeightbackList(subQuery);
        }

        public override String keepQcwebid_InScopeSubQuery_TAllocationCellInfo(TAllocationCellInfoCQ subQuery) {
            return _myCQ.keepQcwebid_InScopeSubQuery_TAllocationCellInfo(subQuery);
        }

        public override String keepQcwebid_InScopeSubQuery_TAccessPermissionsInfoAsOne(TAccessPermissionsInfoCQ subQuery) {
            return _myCQ.keepQcwebid_InScopeSubQuery_TAccessPermissionsInfoAsOne(subQuery);
        }

        public override String keepQcwebid_InScopeSubQuery_TAllocationCellInfoList(TAllocationCellInfoCQ subQuery) {
            return _myCQ.keepQcwebid_InScopeSubQuery_TAllocationCellInfoList(subQuery);
        }

        public override String keepQcwebid_InScopeSubQuery_TDataEditListList(TDataEditListCQ subQuery) {
            return _myCQ.keepQcwebid_InScopeSubQuery_TDataEditListList(subQuery);
        }

        public override String keepQcwebid_InScopeSubQuery_TItemInfoList(TItemInfoCQ subQuery) {
            return _myCQ.keepQcwebid_InScopeSubQuery_TItemInfoList(subQuery);
        }

        public override String keepQcwebid_InScopeSubQuery_TNoticeList(TNoticeCQ subQuery) {
            return _myCQ.keepQcwebid_InScopeSubQuery_TNoticeList(subQuery);
        }

        public override String keepQcwebid_InScopeSubQuery_TOutputRequestList(TOutputRequestCQ subQuery) {
            return _myCQ.keepQcwebid_InScopeSubQuery_TOutputRequestList(subQuery);
        }

        public override String keepQcwebid_InScopeSubQuery_TOutputSettingAsOne(TOutputSettingCQ subQuery) {
            return _myCQ.keepQcwebid_InScopeSubQuery_TOutputSettingAsOne(subQuery);
        }

        public override String keepQcwebid_InScopeSubQuery_TOutputSettingCrossAsOne(TOutputSettingCrossCQ subQuery) {
            return _myCQ.keepQcwebid_InScopeSubQuery_TOutputSettingCrossAsOne(subQuery);
        }

        public override String keepQcwebid_InScopeSubQuery_TOutputSettingFaAsOne(TOutputSettingFaCQ subQuery) {
            return _myCQ.keepQcwebid_InScopeSubQuery_TOutputSettingFaAsOne(subQuery);
        }

        public override String keepQcwebid_InScopeSubQuery_TOutputSettingGtAsOne(TOutputSettingGtCQ subQuery) {
            return _myCQ.keepQcwebid_InScopeSubQuery_TOutputSettingGtAsOne(subQuery);
        }

        public override String keepQcwebid_InScopeSubQuery_TOutputSettingReportAsOne(TOutputSettingReportCQ subQuery) {
            return _myCQ.keepQcwebid_InScopeSubQuery_TOutputSettingReportAsOne(subQuery);
        }

        public override String keepQcwebid_InScopeSubQuery_TOutputTemplateList(TOutputTemplateCQ subQuery) {
            return _myCQ.keepQcwebid_InScopeSubQuery_TOutputTemplateList(subQuery);
        }

        public override String keepQcwebid_InScopeSubQuery_TQcwebSurveyDetailList(TQcwebSurveyDetailCQ subQuery) {
            return _myCQ.keepQcwebid_InScopeSubQuery_TQcwebSurveyDetailList(subQuery);
        }

        public override String keepQcwebid_InScopeSubQuery_TRawdataImportQueInfoList(TRawdataImportQueInfoCQ subQuery) {
            return _myCQ.keepQcwebid_InScopeSubQuery_TRawdataImportQueInfoList(subQuery);
        }

        public override String keepQcwebid_InScopeSubQuery_TReportsetList(TReportsetCQ subQuery) {
            return _myCQ.keepQcwebid_InScopeSubQuery_TReportsetList(subQuery);
        }

        public override String keepQcwebid_InScopeSubQuery_TScenarioTotalizationList(TScenarioTotalizationCQ subQuery) {
            return _myCQ.keepQcwebid_InScopeSubQuery_TScenarioTotalizationList(subQuery);
        }

        public override String keepQcwebid_InScopeSubQuery_TSelectConditionInfoList(TSelectConditionInfoCQ subQuery) {
            return _myCQ.keepQcwebid_InScopeSubQuery_TSelectConditionInfoList(subQuery);
        }

        public override String keepQcwebid_InScopeSubQuery_TSessionControlerList(TSessionControlerCQ subQuery) {
            return _myCQ.keepQcwebid_InScopeSubQuery_TSessionControlerList(subQuery);
        }

        public override String keepQcwebid_InScopeSubQuery_TWeightbackList(TWeightbackCQ subQuery) {
            return _myCQ.keepQcwebid_InScopeSubQuery_TWeightbackList(subQuery);
        }

        public override String keepQcwebid_NotInScopeSubQuery_TAllocationCellInfo(TAllocationCellInfoCQ subQuery) {
            return _myCQ.keepQcwebid_NotInScopeSubQuery_TAllocationCellInfo(subQuery);
        }

        public override String keepQcwebid_NotInScopeSubQuery_TAccessPermissionsInfoAsOne(TAccessPermissionsInfoCQ subQuery) {
            return _myCQ.keepQcwebid_NotInScopeSubQuery_TAccessPermissionsInfoAsOne(subQuery);
        }

        public override String keepQcwebid_NotInScopeSubQuery_TAllocationCellInfoList(TAllocationCellInfoCQ subQuery) {
            return _myCQ.keepQcwebid_NotInScopeSubQuery_TAllocationCellInfoList(subQuery);
        }

        public override String keepQcwebid_NotInScopeSubQuery_TDataEditListList(TDataEditListCQ subQuery) {
            return _myCQ.keepQcwebid_NotInScopeSubQuery_TDataEditListList(subQuery);
        }

        public override String keepQcwebid_NotInScopeSubQuery_TItemInfoList(TItemInfoCQ subQuery) {
            return _myCQ.keepQcwebid_NotInScopeSubQuery_TItemInfoList(subQuery);
        }

        public override String keepQcwebid_NotInScopeSubQuery_TNoticeList(TNoticeCQ subQuery) {
            return _myCQ.keepQcwebid_NotInScopeSubQuery_TNoticeList(subQuery);
        }

        public override String keepQcwebid_NotInScopeSubQuery_TOutputRequestList(TOutputRequestCQ subQuery) {
            return _myCQ.keepQcwebid_NotInScopeSubQuery_TOutputRequestList(subQuery);
        }

        public override String keepQcwebid_NotInScopeSubQuery_TOutputSettingAsOne(TOutputSettingCQ subQuery) {
            return _myCQ.keepQcwebid_NotInScopeSubQuery_TOutputSettingAsOne(subQuery);
        }

        public override String keepQcwebid_NotInScopeSubQuery_TOutputSettingCrossAsOne(TOutputSettingCrossCQ subQuery) {
            return _myCQ.keepQcwebid_NotInScopeSubQuery_TOutputSettingCrossAsOne(subQuery);
        }

        public override String keepQcwebid_NotInScopeSubQuery_TOutputSettingFaAsOne(TOutputSettingFaCQ subQuery) {
            return _myCQ.keepQcwebid_NotInScopeSubQuery_TOutputSettingFaAsOne(subQuery);
        }

        public override String keepQcwebid_NotInScopeSubQuery_TOutputSettingGtAsOne(TOutputSettingGtCQ subQuery) {
            return _myCQ.keepQcwebid_NotInScopeSubQuery_TOutputSettingGtAsOne(subQuery);
        }

        public override String keepQcwebid_NotInScopeSubQuery_TOutputSettingReportAsOne(TOutputSettingReportCQ subQuery) {
            return _myCQ.keepQcwebid_NotInScopeSubQuery_TOutputSettingReportAsOne(subQuery);
        }

        public override String keepQcwebid_NotInScopeSubQuery_TOutputTemplateList(TOutputTemplateCQ subQuery) {
            return _myCQ.keepQcwebid_NotInScopeSubQuery_TOutputTemplateList(subQuery);
        }

        public override String keepQcwebid_NotInScopeSubQuery_TQcwebSurveyDetailList(TQcwebSurveyDetailCQ subQuery) {
            return _myCQ.keepQcwebid_NotInScopeSubQuery_TQcwebSurveyDetailList(subQuery);
        }

        public override String keepQcwebid_NotInScopeSubQuery_TRawdataImportQueInfoList(TRawdataImportQueInfoCQ subQuery) {
            return _myCQ.keepQcwebid_NotInScopeSubQuery_TRawdataImportQueInfoList(subQuery);
        }

        public override String keepQcwebid_NotInScopeSubQuery_TReportsetList(TReportsetCQ subQuery) {
            return _myCQ.keepQcwebid_NotInScopeSubQuery_TReportsetList(subQuery);
        }

        public override String keepQcwebid_NotInScopeSubQuery_TScenarioTotalizationList(TScenarioTotalizationCQ subQuery) {
            return _myCQ.keepQcwebid_NotInScopeSubQuery_TScenarioTotalizationList(subQuery);
        }

        public override String keepQcwebid_NotInScopeSubQuery_TSelectConditionInfoList(TSelectConditionInfoCQ subQuery) {
            return _myCQ.keepQcwebid_NotInScopeSubQuery_TSelectConditionInfoList(subQuery);
        }

        public override String keepQcwebid_NotInScopeSubQuery_TSessionControlerList(TSessionControlerCQ subQuery) {
            return _myCQ.keepQcwebid_NotInScopeSubQuery_TSessionControlerList(subQuery);
        }

        public override String keepQcwebid_NotInScopeSubQuery_TWeightbackList(TWeightbackCQ subQuery) {
            return _myCQ.keepQcwebid_NotInScopeSubQuery_TWeightbackList(subQuery);
        }
        public override String keepQcwebid_SpecifyDerivedReferrer_TAllocationCellInfoList(TAllocationCellInfoCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_SpecifyDerivedReferrer_TDataEditListList(TDataEditListCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_SpecifyDerivedReferrer_TItemInfoList(TItemInfoCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_SpecifyDerivedReferrer_TNoticeList(TNoticeCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_SpecifyDerivedReferrer_TOutputRequestList(TOutputRequestCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_SpecifyDerivedReferrer_TOutputTemplateList(TOutputTemplateCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_SpecifyDerivedReferrer_TQcwebSurveyDetailList(TQcwebSurveyDetailCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_SpecifyDerivedReferrer_TRawdataImportQueInfoList(TRawdataImportQueInfoCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_SpecifyDerivedReferrer_TReportsetList(TReportsetCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_SpecifyDerivedReferrer_TScenarioTotalizationList(TScenarioTotalizationCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_SpecifyDerivedReferrer_TSelectConditionInfoList(TSelectConditionInfoCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_SpecifyDerivedReferrer_TSessionControlerList(TSessionControlerCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_SpecifyDerivedReferrer_TWeightbackList(TWeightbackCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_QueryDerivedReferrer_TAllocationCellInfoList(TAllocationCellInfoCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_QueryDerivedReferrer_TAllocationCellInfoListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_QueryDerivedReferrer_TDataEditListList(TDataEditListCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_QueryDerivedReferrer_TDataEditListListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_QueryDerivedReferrer_TItemInfoList(TItemInfoCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_QueryDerivedReferrer_TItemInfoListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_QueryDerivedReferrer_TNoticeList(TNoticeCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_QueryDerivedReferrer_TNoticeListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_QueryDerivedReferrer_TOutputRequestList(TOutputRequestCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_QueryDerivedReferrer_TOutputRequestListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_QueryDerivedReferrer_TOutputTemplateList(TOutputTemplateCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_QueryDerivedReferrer_TOutputTemplateListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_QueryDerivedReferrer_TQcwebSurveyDetailList(TQcwebSurveyDetailCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_QueryDerivedReferrer_TQcwebSurveyDetailListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_QueryDerivedReferrer_TRawdataImportQueInfoList(TRawdataImportQueInfoCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_QueryDerivedReferrer_TRawdataImportQueInfoListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_QueryDerivedReferrer_TReportsetList(TReportsetCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_QueryDerivedReferrer_TReportsetListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_QueryDerivedReferrer_TScenarioTotalizationList(TScenarioTotalizationCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_QueryDerivedReferrer_TScenarioTotalizationListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_QueryDerivedReferrer_TSelectConditionInfoList(TSelectConditionInfoCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_QueryDerivedReferrer_TSelectConditionInfoListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_QueryDerivedReferrer_TSessionControlerList(TSessionControlerCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_QueryDerivedReferrer_TSessionControlerListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_QueryDerivedReferrer_TWeightbackList(TWeightbackCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_QueryDerivedReferrer_TWeightbackListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }

        protected override ConditionValue getCValueAddDataNo() {
            return _myCQ.AddDataNo;
        }


        protected override ConditionValue getCValueSurveyNameOrg() {
            return _myCQ.SurveyNameOrg;
        }


        protected override ConditionValue getCValueImportDatetime() {
            return _myCQ.ImportDatetime;
        }


        protected override ConditionValue getCValueImportFileName() {
            return _myCQ.ImportFileName;
        }


        protected override ConditionValue getCValueDeleteFlag() {
            return _myCQ.DeleteFlag;
        }


        protected override ConditionValue getCValueViewSurveyName() {
            return _myCQ.ViewSurveyName;
        }


        protected override ConditionValue getCValueGtCount() {
            return _myCQ.GtCount;
        }


        protected override ConditionValue getCValueCrossCount() {
            return _myCQ.CrossCount;
        }


        protected override ConditionValue getCValueFaCount() {
            return _myCQ.FaCount;
        }


        protected override ConditionValue getCValueVersionNo() {
            return _myCQ.VersionNo;
        }


        protected override ConditionValue getCValueLastUpdateUser() {
            return _myCQ.LastUpdateUser;
        }


        protected override ConditionValue getCValueLastUpdateDatetime() {
            return _myCQ.LastUpdateDatetime;
        }


        protected override ConditionValue getCValueSurveyInfoId() {
            return _myCQ.SurveyInfoId;
        }


        public override String keepSurveyInfoId_InScopeSubQuery_TSurveyInfo(TSurveyInfoCQ subQuery) {
            return _myCQ.keepSurveyInfoId_InScopeSubQuery_TSurveyInfo(subQuery);
        }

        public override String keepSurveyInfoId_NotInScopeSubQuery_TSurveyInfo(TSurveyInfoCQ subQuery) {
            return _myCQ.keepSurveyInfoId_NotInScopeSubQuery_TSurveyInfo(subQuery);
        }

        protected override ConditionValue getCValueRawdataImportQueInfoId() {
            return _myCQ.RawdataImportQueInfoId;
        }


        public override String keepRawdataImportQueInfoId_InScopeSubQuery_TRawdataImportQueInfo(TRawdataImportQueInfoCQ subQuery) {
            return _myCQ.keepRawdataImportQueInfoId_InScopeSubQuery_TRawdataImportQueInfo(subQuery);
        }

        public override String keepRawdataImportQueInfoId_NotInScopeSubQuery_TRawdataImportQueInfo(TRawdataImportQueInfoCQ subQuery) {
            return _myCQ.keepRawdataImportQueInfoId_NotInScopeSubQuery_TRawdataImportQueInfo(subQuery);
        }

        protected override ConditionValue getCValueUtf8Flag() {
            return _myCQ.Utf8Flag;
        }


        // ===================================================================================
        //                                                                     Scalar SubQuery
        //                                                                     ===============
        public override String keepScalarSubQuery(TQcwebSurveyInfoCQ subQuery) {
            throw new UnsupportedOperationException("ScalarSubQuery at inline() is unsupported! Sorry!");
        }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        public override String keepMyselfInScopeSubQuery(TQcwebSurveyInfoCQ subQuery) {
            throw new UnsupportedOperationException("MyselfInScopeSubQuery at inline() is unsupported! Sorry!");
        }
    }
}
