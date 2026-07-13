
using System;
using System.Reflection;

using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.AllCommon.Util;

namespace Macromill.QCWeb.Dao.AllCommon.Dbm {

    public interface DBMetaProvider {
        DBMeta provideDBMeta(String tableFlexibleName);
        DBMeta provideDBMetaChecked(String tableFlexibleName);
    }

    public class DBMetaInstanceHandler : DBMetaProvider {

        // ===============================================================================
        //                                                                    Resource Map
        //                                                                    ============
        protected static readonly Map<String, DBMeta> _tableDbNameInstanceMap = new HashMap<String, DBMeta>();
        protected static readonly Map<String, String> _tableDbNameClassNameMap;
        protected static readonly Map<String, String> _tableDbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _tablePropertyNameDbNameKeyToLowerMap;

        static DBMetaInstanceHandler() {
            {
                Map<String, String> tmpMap = new HashMap<String, String>();
                tmpMap.put("T_ACCESS_PERMISSIONS_INFO", "Macromill.QCWeb.Dao.BsEntity.Dbm.TAccessPermissionsInfoDbm");
                tmpMap.put("T_ALLOCATION_CELL_INFO", "Macromill.QCWeb.Dao.BsEntity.Dbm.TAllocationCellInfoDbm");
                tmpMap.put("T_APPLICATION", "Macromill.QCWeb.Dao.BsEntity.Dbm.TApplicationDbm");
                tmpMap.put("T_CATEGORY_INFO", "Macromill.QCWeb.Dao.BsEntity.Dbm.TCategoryInfoDbm");
                tmpMap.put("T_CATEGORY_OUTPUT_DETAIL", "Macromill.QCWeb.Dao.BsEntity.Dbm.TCategoryOutputDetailDbm");
                tmpMap.put("T_CATEGORY_OUTPUT_EDIT", "Macromill.QCWeb.Dao.BsEntity.Dbm.TCategoryOutputEditDbm");
                tmpMap.put("T_CODE_MASTER", "Macromill.QCWeb.Dao.BsEntity.Dbm.TCodeMasterDbm");
                tmpMap.put("T_COLOR_INFO_DETAIL_CROSS", "Macromill.QCWeb.Dao.BsEntity.Dbm.TColorInfoDetailCrossDbm");
                tmpMap.put("T_COLOR_INFO_DETAIL_GT", "Macromill.QCWeb.Dao.BsEntity.Dbm.TColorInfoDetailGtDbm");
                tmpMap.put("T_COLOR_SET_INFO_CROSS", "Macromill.QCWeb.Dao.BsEntity.Dbm.TColorSetInfoCrossDbm");
                tmpMap.put("T_COLOR_SET_INFO_GT", "Macromill.QCWeb.Dao.BsEntity.Dbm.TColorSetInfoGtDbm");
                tmpMap.put("T_CROSS_SCENARIO_ITEM", "Macromill.QCWeb.Dao.BsEntity.Dbm.TCrossScenarioItemDbm");
                tmpMap.put("T_CROSS_SCENARIO_TARGET", "Macromill.QCWeb.Dao.BsEntity.Dbm.TCrossScenarioTargetDbm");
                tmpMap.put("T_DATA_EDIT_LIST", "Macromill.QCWeb.Dao.BsEntity.Dbm.TDataEditListDbm");
                tmpMap.put("T_DATA_PROCESS_NEW_CATEGORY", "Macromill.QCWeb.Dao.BsEntity.Dbm.TDataProcessNewCategoryDbm");
                tmpMap.put("T_DATA_PROCESS_NEW_ITEM", "Macromill.QCWeb.Dao.BsEntity.Dbm.TDataProcessNewItemDbm");
                tmpMap.put("T_DATA_PROCESS_NEW_ITEM_SRC", "Macromill.QCWeb.Dao.BsEntity.Dbm.TDataProcessNewItemSrcDbm");
                tmpMap.put("T_DEFAULT_ENV", "Macromill.QCWeb.Dao.BsEntity.Dbm.TDefaultEnvDbm");
                tmpMap.put("T_DEFAULT_ENV_BASE", "Macromill.QCWeb.Dao.BsEntity.Dbm.TDefaultEnvBaseDbm");
                tmpMap.put("T_DEFAULT_ENV_COLOR_DTL", "Macromill.QCWeb.Dao.BsEntity.Dbm.TDefaultEnvColorDtlDbm");
                tmpMap.put("T_DEFAULT_ENV_COLOR_DTL_C", "Macromill.QCWeb.Dao.BsEntity.Dbm.TDefaultEnvColorDtlCDbm");
                tmpMap.put("T_DEFAULT_ENV_COLOR_INFO", "Macromill.QCWeb.Dao.BsEntity.Dbm.TDefaultEnvColorInfoDbm");
                tmpMap.put("T_DEFAULT_ENV_COLOR_INFO_C", "Macromill.QCWeb.Dao.BsEntity.Dbm.TDefaultEnvColorInfoCDbm");
                tmpMap.put("T_DELETE_CONDITION", "Macromill.QCWeb.Dao.BsEntity.Dbm.TDeleteConditionDbm");
                tmpMap.put("T_DELETE_DATA", "Macromill.QCWeb.Dao.BsEntity.Dbm.TDeleteDataDbm");
                tmpMap.put("T_DELETE_SAMPLE_ID_LIST", "Macromill.QCWeb.Dao.BsEntity.Dbm.TDeleteSampleIdListDbm");
                tmpMap.put("T_EDIT_CONDITION", "Macromill.QCWeb.Dao.BsEntity.Dbm.TEditConditionDbm");
                tmpMap.put("T_EDIT_DATA", "Macromill.QCWeb.Dao.BsEntity.Dbm.TEditDataDbm");
                tmpMap.put("T_EDIT_MENU_MASTER", "Macromill.QCWeb.Dao.BsEntity.Dbm.TEditMenuMasterDbm");
                tmpMap.put("T_EDIT_TARGET_ITEM", "Macromill.QCWeb.Dao.BsEntity.Dbm.TEditTargetItemDbm");
                tmpMap.put("T_FA_DATA", "Macromill.QCWeb.Dao.BsEntity.Dbm.TFaDataDbm");
                tmpMap.put("T_FA_LIST_ADD_ITEM", "Macromill.QCWeb.Dao.BsEntity.Dbm.TFaListAddItemDbm");
                tmpMap.put("T_FA_SCENARIO_HEADER", "Macromill.QCWeb.Dao.BsEntity.Dbm.TFaScenarioHeaderDbm");
                tmpMap.put("T_FA_SCENARIO_ITEM", "Macromill.QCWeb.Dao.BsEntity.Dbm.TFaScenarioItemDbm");
                tmpMap.put("T_GT_MATRIX_CHILD", "Macromill.QCWeb.Dao.BsEntity.Dbm.TGtMatrixChildDbm");
                tmpMap.put("T_GT_MATRIX_INFO", "Macromill.QCWeb.Dao.BsEntity.Dbm.TGtMatrixInfoDbm");
                tmpMap.put("T_GT_SCENARIO_ITEM", "Macromill.QCWeb.Dao.BsEntity.Dbm.TGtScenarioItemDbm");
                tmpMap.put("T_INTEG_CONDITION", "Macromill.QCWeb.Dao.BsEntity.Dbm.TIntegConditionDbm");
                tmpMap.put("T_ITEM_INFO", "Macromill.QCWeb.Dao.BsEntity.Dbm.TItemInfoDbm");
                tmpMap.put("T_MAINTENANCE", "Macromill.QCWeb.Dao.BsEntity.Dbm.TMaintenanceDbm");
                tmpMap.put("T_MATRIX_INFO", "Macromill.QCWeb.Dao.BsEntity.Dbm.TMatrixInfoDbm");
                tmpMap.put("T_NOTICE", "Macromill.QCWeb.Dao.BsEntity.Dbm.TNoticeDbm");
                tmpMap.put("T_OUTPUT_COMMON", "Macromill.QCWeb.Dao.BsEntity.Dbm.TOutputCommonDbm");
                tmpMap.put("T_OUTPUT_HISTORY_ITEM", "Macromill.QCWeb.Dao.BsEntity.Dbm.TOutputHistoryItemDbm");
                tmpMap.put("T_OUTPUT_REPORTSET_INFO", "Macromill.QCWeb.Dao.BsEntity.Dbm.TOutputReportsetInfoDbm");
                tmpMap.put("T_OUTPUT_REQUEST", "Macromill.QCWeb.Dao.BsEntity.Dbm.TOutputRequestDbm");
                tmpMap.put("T_OUTPUT_SETTING", "Macromill.QCWeb.Dao.BsEntity.Dbm.TOutputSettingDbm");
                tmpMap.put("T_OUTPUT_SETTING_CROSS", "Macromill.QCWeb.Dao.BsEntity.Dbm.TOutputSettingCrossDbm");
                tmpMap.put("T_OUTPUT_SETTING_FA", "Macromill.QCWeb.Dao.BsEntity.Dbm.TOutputSettingFaDbm");
                tmpMap.put("T_OUTPUT_SETTING_GT", "Macromill.QCWeb.Dao.BsEntity.Dbm.TOutputSettingGtDbm");
                tmpMap.put("T_OUTPUT_SETTING_REPORT", "Macromill.QCWeb.Dao.BsEntity.Dbm.TOutputSettingReportDbm");
                tmpMap.put("T_OUTPUT_SUB_CKLIST", "Macromill.QCWeb.Dao.BsEntity.Dbm.TOutputSubCklistDbm");
                tmpMap.put("T_OUTPUT_SUB_CROSS", "Macromill.QCWeb.Dao.BsEntity.Dbm.TOutputSubCrossDbm");
                tmpMap.put("T_OUTPUT_SUB_FA", "Macromill.QCWeb.Dao.BsEntity.Dbm.TOutputSubFaDbm");
                tmpMap.put("T_OUTPUT_SUB_GT", "Macromill.QCWeb.Dao.BsEntity.Dbm.TOutputSubGtDbm");
                tmpMap.put("T_OUTPUT_TEMPLATE", "Macromill.QCWeb.Dao.BsEntity.Dbm.TOutputTemplateDbm");
                tmpMap.put("T_OUTPUT_TEMPLATE_MASTER", "Macromill.QCWeb.Dao.BsEntity.Dbm.TOutputTemplateMasterDbm");
                tmpMap.put("T_OUTPUT_WP_MASTER", "Macromill.QCWeb.Dao.BsEntity.Dbm.TOutputWpMasterDbm");
                tmpMap.put("T_POLYLINE_CATEGORY_LIST", "Macromill.QCWeb.Dao.BsEntity.Dbm.TPolylineCategoryListDbm");
                tmpMap.put("T_QCWEB_SURVEY_DETAIL", "Macromill.QCWeb.Dao.BsEntity.Dbm.TQcwebSurveyDetailDbm");
                tmpMap.put("T_QCWEB_SURVEY_INFO", "Macromill.QCWeb.Dao.BsEntity.Dbm.TQcwebSurveyInfoDbm");
                tmpMap.put("T_RAWDATA_DELETE_QUE", "Macromill.QCWeb.Dao.BsEntity.Dbm.TRawdataDeleteQueDbm");
                tmpMap.put("T_RAWDATA_IMPORT_QUE_INFO", "Macromill.QCWeb.Dao.BsEntity.Dbm.TRawdataImportQueInfoDbm");
                tmpMap.put("T_REPORT", "Macromill.QCWeb.Dao.BsEntity.Dbm.TReportDbm");
                tmpMap.put("T_REPORTSET", "Macromill.QCWeb.Dao.BsEntity.Dbm.TReportsetDbm");
                tmpMap.put("T_REPORT_CHILD", "Macromill.QCWeb.Dao.BsEntity.Dbm.TReportChildDbm");
                tmpMap.put("T_SCENARIO_QUERYLIST", "Macromill.QCWeb.Dao.BsEntity.Dbm.TScenarioQuerylistDbm");
                tmpMap.put("T_SCENARIO_TOTALIZATION", "Macromill.QCWeb.Dao.BsEntity.Dbm.TScenarioTotalizationDbm");
                tmpMap.put("T_SELECT_CONDITION_INFO", "Macromill.QCWeb.Dao.BsEntity.Dbm.TSelectConditionInfoDbm");
                tmpMap.put("T_SESSION_CONTROLER", "Macromill.QCWeb.Dao.BsEntity.Dbm.TSessionControlerDbm");
                tmpMap.put("T_SURVEY_DATA", "Macromill.QCWeb.Dao.BsEntity.Dbm.TSurveyDataDbm");
                tmpMap.put("T_SURVEY_INFO", "Macromill.QCWeb.Dao.BsEntity.Dbm.TSurveyInfoDbm");
                tmpMap.put("T_TABLE_CONTROL", "Macromill.QCWeb.Dao.BsEntity.Dbm.TTableControlDbm");
                tmpMap.put("T_TABLE_DETAIL_INFO", "Macromill.QCWeb.Dao.BsEntity.Dbm.TTableDetailInfoDbm");
                tmpMap.put("T_WEIGHTBACK", "Macromill.QCWeb.Dao.BsEntity.Dbm.TWeightbackDbm");
                tmpMap.put("T_WEIGHTBACK_VALUE", "Macromill.QCWeb.Dao.BsEntity.Dbm.TWeightbackValueDbm");
                _tableDbNameClassNameMap = tmpMap;//java.util.Collections.unmodifiableMap(tmpMap);
            }

            {
                Map<String, String> tmpMap = new HashMap<String, String>();
                tmpMap.put("T_ACCESS_PERMISSIONS_INFO".ToLower(), "TAccessPermissionsInfo");
                tmpMap.put("T_ALLOCATION_CELL_INFO".ToLower(), "TAllocationCellInfo");
                tmpMap.put("T_APPLICATION".ToLower(), "TApplication");
                tmpMap.put("T_CATEGORY_INFO".ToLower(), "TCategoryInfo");
                tmpMap.put("T_CATEGORY_OUTPUT_DETAIL".ToLower(), "TCategoryOutputDetail");
                tmpMap.put("T_CATEGORY_OUTPUT_EDIT".ToLower(), "TCategoryOutputEdit");
                tmpMap.put("T_CODE_MASTER".ToLower(), "TCodeMaster");
                tmpMap.put("T_COLOR_INFO_DETAIL_CROSS".ToLower(), "TColorInfoDetailCross");
                tmpMap.put("T_COLOR_INFO_DETAIL_GT".ToLower(), "TColorInfoDetailGt");
                tmpMap.put("T_COLOR_SET_INFO_CROSS".ToLower(), "TColorSetInfoCross");
                tmpMap.put("T_COLOR_SET_INFO_GT".ToLower(), "TColorSetInfoGt");
                tmpMap.put("T_CROSS_SCENARIO_ITEM".ToLower(), "TCrossScenarioItem");
                tmpMap.put("T_CROSS_SCENARIO_TARGET".ToLower(), "TCrossScenarioTarget");
                tmpMap.put("T_DATA_EDIT_LIST".ToLower(), "TDataEditList");
                tmpMap.put("T_DATA_PROCESS_NEW_CATEGORY".ToLower(), "TDataProcessNewCategory");
                tmpMap.put("T_DATA_PROCESS_NEW_ITEM".ToLower(), "TDataProcessNewItem");
                tmpMap.put("T_DATA_PROCESS_NEW_ITEM_SRC".ToLower(), "TDataProcessNewItemSrc");
                tmpMap.put("T_DEFAULT_ENV".ToLower(), "TDefaultEnv");
                tmpMap.put("T_DEFAULT_ENV_BASE".ToLower(), "TDefaultEnvBase");
                tmpMap.put("T_DEFAULT_ENV_COLOR_DTL".ToLower(), "TDefaultEnvColorDtl");
                tmpMap.put("T_DEFAULT_ENV_COLOR_DTL_C".ToLower(), "TDefaultEnvColorDtlC");
                tmpMap.put("T_DEFAULT_ENV_COLOR_INFO".ToLower(), "TDefaultEnvColorInfo");
                tmpMap.put("T_DEFAULT_ENV_COLOR_INFO_C".ToLower(), "TDefaultEnvColorInfoC");
                tmpMap.put("T_DELETE_CONDITION".ToLower(), "TDeleteCondition");
                tmpMap.put("T_DELETE_DATA".ToLower(), "TDeleteData");
                tmpMap.put("T_DELETE_SAMPLE_ID_LIST".ToLower(), "TDeleteSampleIdList");
                tmpMap.put("T_EDIT_CONDITION".ToLower(), "TEditCondition");
                tmpMap.put("T_EDIT_DATA".ToLower(), "TEditData");
                tmpMap.put("T_EDIT_MENU_MASTER".ToLower(), "TEditMenuMaster");
                tmpMap.put("T_EDIT_TARGET_ITEM".ToLower(), "TEditTargetItem");
                tmpMap.put("T_FA_DATA".ToLower(), "TFaData");
                tmpMap.put("T_FA_LIST_ADD_ITEM".ToLower(), "TFaListAddItem");
                tmpMap.put("T_FA_SCENARIO_HEADER".ToLower(), "TFaScenarioHeader");
                tmpMap.put("T_FA_SCENARIO_ITEM".ToLower(), "TFaScenarioItem");
                tmpMap.put("T_GT_MATRIX_CHILD".ToLower(), "TGtMatrixChild");
                tmpMap.put("T_GT_MATRIX_INFO".ToLower(), "TGtMatrixInfo");
                tmpMap.put("T_GT_SCENARIO_ITEM".ToLower(), "TGtScenarioItem");
                tmpMap.put("T_INTEG_CONDITION".ToLower(), "TIntegCondition");
                tmpMap.put("T_ITEM_INFO".ToLower(), "TItemInfo");
                tmpMap.put("T_MAINTENANCE".ToLower(), "TMaintenance");
                tmpMap.put("T_MATRIX_INFO".ToLower(), "TMatrixInfo");
                tmpMap.put("T_NOTICE".ToLower(), "TNotice");
                tmpMap.put("T_OUTPUT_COMMON".ToLower(), "TOutputCommon");
                tmpMap.put("T_OUTPUT_HISTORY_ITEM".ToLower(), "TOutputHistoryItem");
                tmpMap.put("T_OUTPUT_REPORTSET_INFO".ToLower(), "TOutputReportsetInfo");
                tmpMap.put("T_OUTPUT_REQUEST".ToLower(), "TOutputRequest");
                tmpMap.put("T_OUTPUT_SETTING".ToLower(), "TOutputSetting");
                tmpMap.put("T_OUTPUT_SETTING_CROSS".ToLower(), "TOutputSettingCross");
                tmpMap.put("T_OUTPUT_SETTING_FA".ToLower(), "TOutputSettingFa");
                tmpMap.put("T_OUTPUT_SETTING_GT".ToLower(), "TOutputSettingGt");
                tmpMap.put("T_OUTPUT_SETTING_REPORT".ToLower(), "TOutputSettingReport");
                tmpMap.put("T_OUTPUT_SUB_CKLIST".ToLower(), "TOutputSubCklist");
                tmpMap.put("T_OUTPUT_SUB_CROSS".ToLower(), "TOutputSubCross");
                tmpMap.put("T_OUTPUT_SUB_FA".ToLower(), "TOutputSubFa");
                tmpMap.put("T_OUTPUT_SUB_GT".ToLower(), "TOutputSubGt");
                tmpMap.put("T_OUTPUT_TEMPLATE".ToLower(), "TOutputTemplate");
                tmpMap.put("T_OUTPUT_TEMPLATE_MASTER".ToLower(), "TOutputTemplateMaster");
                tmpMap.put("T_OUTPUT_WP_MASTER".ToLower(), "TOutputWpMaster");
                tmpMap.put("T_POLYLINE_CATEGORY_LIST".ToLower(), "TPolylineCategoryList");
                tmpMap.put("T_QCWEB_SURVEY_DETAIL".ToLower(), "TQcwebSurveyDetail");
                tmpMap.put("T_QCWEB_SURVEY_INFO".ToLower(), "TQcwebSurveyInfo");
                tmpMap.put("T_RAWDATA_DELETE_QUE".ToLower(), "TRawdataDeleteQue");
                tmpMap.put("T_RAWDATA_IMPORT_QUE_INFO".ToLower(), "TRawdataImportQueInfo");
                tmpMap.put("T_REPORT".ToLower(), "TReport");
                tmpMap.put("T_REPORTSET".ToLower(), "TReportset");
                tmpMap.put("T_REPORT_CHILD".ToLower(), "TReportChild");
                tmpMap.put("T_SCENARIO_QUERYLIST".ToLower(), "TScenarioQuerylist");
                tmpMap.put("T_SCENARIO_TOTALIZATION".ToLower(), "TScenarioTotalization");
                tmpMap.put("T_SELECT_CONDITION_INFO".ToLower(), "TSelectConditionInfo");
                tmpMap.put("T_SESSION_CONTROLER".ToLower(), "TSessionControler");
                tmpMap.put("T_SURVEY_DATA".ToLower(), "TSurveyData");
                tmpMap.put("T_SURVEY_INFO".ToLower(), "TSurveyInfo");
                tmpMap.put("T_TABLE_CONTROL".ToLower(), "TTableControl");
                tmpMap.put("T_TABLE_DETAIL_INFO".ToLower(), "TTableDetailInfo");
                tmpMap.put("T_WEIGHTBACK".ToLower(), "TWeightback");
                tmpMap.put("T_WEIGHTBACK_VALUE".ToLower(), "TWeightbackValue");
                _tableDbNamePropertyNameKeyToLowerMap = tmpMap;//java.util.Collections.unmodifiableMap(tmpMap);
            }

            {
                Map<String, String> tmpMap = new HashMap<String, String>();
                tmpMap.put("TAccessPermissionsInfo".ToLower(), "T_ACCESS_PERMISSIONS_INFO");
                tmpMap.put("TAllocationCellInfo".ToLower(), "T_ALLOCATION_CELL_INFO");
                tmpMap.put("TApplication".ToLower(), "T_APPLICATION");
                tmpMap.put("TCategoryInfo".ToLower(), "T_CATEGORY_INFO");
                tmpMap.put("TCategoryOutputDetail".ToLower(), "T_CATEGORY_OUTPUT_DETAIL");
                tmpMap.put("TCategoryOutputEdit".ToLower(), "T_CATEGORY_OUTPUT_EDIT");
                tmpMap.put("TCodeMaster".ToLower(), "T_CODE_MASTER");
                tmpMap.put("TColorInfoDetailCross".ToLower(), "T_COLOR_INFO_DETAIL_CROSS");
                tmpMap.put("TColorInfoDetailGt".ToLower(), "T_COLOR_INFO_DETAIL_GT");
                tmpMap.put("TColorSetInfoCross".ToLower(), "T_COLOR_SET_INFO_CROSS");
                tmpMap.put("TColorSetInfoGt".ToLower(), "T_COLOR_SET_INFO_GT");
                tmpMap.put("TCrossScenarioItem".ToLower(), "T_CROSS_SCENARIO_ITEM");
                tmpMap.put("TCrossScenarioTarget".ToLower(), "T_CROSS_SCENARIO_TARGET");
                tmpMap.put("TDataEditList".ToLower(), "T_DATA_EDIT_LIST");
                tmpMap.put("TDataProcessNewCategory".ToLower(), "T_DATA_PROCESS_NEW_CATEGORY");
                tmpMap.put("TDataProcessNewItem".ToLower(), "T_DATA_PROCESS_NEW_ITEM");
                tmpMap.put("TDataProcessNewItemSrc".ToLower(), "T_DATA_PROCESS_NEW_ITEM_SRC");
                tmpMap.put("TDefaultEnv".ToLower(), "T_DEFAULT_ENV");
                tmpMap.put("TDefaultEnvBase".ToLower(), "T_DEFAULT_ENV_BASE");
                tmpMap.put("TDefaultEnvColorDtl".ToLower(), "T_DEFAULT_ENV_COLOR_DTL");
                tmpMap.put("TDefaultEnvColorDtlC".ToLower(), "T_DEFAULT_ENV_COLOR_DTL_C");
                tmpMap.put("TDefaultEnvColorInfo".ToLower(), "T_DEFAULT_ENV_COLOR_INFO");
                tmpMap.put("TDefaultEnvColorInfoC".ToLower(), "T_DEFAULT_ENV_COLOR_INFO_C");
                tmpMap.put("TDeleteCondition".ToLower(), "T_DELETE_CONDITION");
                tmpMap.put("TDeleteData".ToLower(), "T_DELETE_DATA");
                tmpMap.put("TDeleteSampleIdList".ToLower(), "T_DELETE_SAMPLE_ID_LIST");
                tmpMap.put("TEditCondition".ToLower(), "T_EDIT_CONDITION");
                tmpMap.put("TEditData".ToLower(), "T_EDIT_DATA");
                tmpMap.put("TEditMenuMaster".ToLower(), "T_EDIT_MENU_MASTER");
                tmpMap.put("TEditTargetItem".ToLower(), "T_EDIT_TARGET_ITEM");
                tmpMap.put("TFaData".ToLower(), "T_FA_DATA");
                tmpMap.put("TFaListAddItem".ToLower(), "T_FA_LIST_ADD_ITEM");
                tmpMap.put("TFaScenarioHeader".ToLower(), "T_FA_SCENARIO_HEADER");
                tmpMap.put("TFaScenarioItem".ToLower(), "T_FA_SCENARIO_ITEM");
                tmpMap.put("TGtMatrixChild".ToLower(), "T_GT_MATRIX_CHILD");
                tmpMap.put("TGtMatrixInfo".ToLower(), "T_GT_MATRIX_INFO");
                tmpMap.put("TGtScenarioItem".ToLower(), "T_GT_SCENARIO_ITEM");
                tmpMap.put("TIntegCondition".ToLower(), "T_INTEG_CONDITION");
                tmpMap.put("TItemInfo".ToLower(), "T_ITEM_INFO");
                tmpMap.put("TMaintenance".ToLower(), "T_MAINTENANCE");
                tmpMap.put("TMatrixInfo".ToLower(), "T_MATRIX_INFO");
                tmpMap.put("TNotice".ToLower(), "T_NOTICE");
                tmpMap.put("TOutputCommon".ToLower(), "T_OUTPUT_COMMON");
                tmpMap.put("TOutputHistoryItem".ToLower(), "T_OUTPUT_HISTORY_ITEM");
                tmpMap.put("TOutputReportsetInfo".ToLower(), "T_OUTPUT_REPORTSET_INFO");
                tmpMap.put("TOutputRequest".ToLower(), "T_OUTPUT_REQUEST");
                tmpMap.put("TOutputSetting".ToLower(), "T_OUTPUT_SETTING");
                tmpMap.put("TOutputSettingCross".ToLower(), "T_OUTPUT_SETTING_CROSS");
                tmpMap.put("TOutputSettingFa".ToLower(), "T_OUTPUT_SETTING_FA");
                tmpMap.put("TOutputSettingGt".ToLower(), "T_OUTPUT_SETTING_GT");
                tmpMap.put("TOutputSettingReport".ToLower(), "T_OUTPUT_SETTING_REPORT");
                tmpMap.put("TOutputSubCklist".ToLower(), "T_OUTPUT_SUB_CKLIST");
                tmpMap.put("TOutputSubCross".ToLower(), "T_OUTPUT_SUB_CROSS");
                tmpMap.put("TOutputSubFa".ToLower(), "T_OUTPUT_SUB_FA");
                tmpMap.put("TOutputSubGt".ToLower(), "T_OUTPUT_SUB_GT");
                tmpMap.put("TOutputTemplate".ToLower(), "T_OUTPUT_TEMPLATE");
                tmpMap.put("TOutputTemplateMaster".ToLower(), "T_OUTPUT_TEMPLATE_MASTER");
                tmpMap.put("TOutputWpMaster".ToLower(), "T_OUTPUT_WP_MASTER");
                tmpMap.put("TPolylineCategoryList".ToLower(), "T_POLYLINE_CATEGORY_LIST");
                tmpMap.put("TQcwebSurveyDetail".ToLower(), "T_QCWEB_SURVEY_DETAIL");
                tmpMap.put("TQcwebSurveyInfo".ToLower(), "T_QCWEB_SURVEY_INFO");
                tmpMap.put("TRawdataDeleteQue".ToLower(), "T_RAWDATA_DELETE_QUE");
                tmpMap.put("TRawdataImportQueInfo".ToLower(), "T_RAWDATA_IMPORT_QUE_INFO");
                tmpMap.put("TReport".ToLower(), "T_REPORT");
                tmpMap.put("TReportset".ToLower(), "T_REPORTSET");
                tmpMap.put("TReportChild".ToLower(), "T_REPORT_CHILD");
                tmpMap.put("TScenarioQuerylist".ToLower(), "T_SCENARIO_QUERYLIST");
                tmpMap.put("TScenarioTotalization".ToLower(), "T_SCENARIO_TOTALIZATION");
                tmpMap.put("TSelectConditionInfo".ToLower(), "T_SELECT_CONDITION_INFO");
                tmpMap.put("TSessionControler".ToLower(), "T_SESSION_CONTROLER");
                tmpMap.put("TSurveyData".ToLower(), "T_SURVEY_DATA");
                tmpMap.put("TSurveyInfo".ToLower(), "T_SURVEY_INFO");
                tmpMap.put("TTableControl".ToLower(), "T_TABLE_CONTROL");
                tmpMap.put("TTableDetailInfo".ToLower(), "T_TABLE_DETAIL_INFO");
                tmpMap.put("TWeightback".ToLower(), "T_WEIGHTBACK");
                tmpMap.put("TWeightbackValue".ToLower(), "T_WEIGHTBACK_VALUE");
                _tablePropertyNameDbNameKeyToLowerMap = tmpMap;//java.util.Collections.unmodifiableMap(tmpMap);
            }
        }

        protected static DBMeta GetDBMeta(String className) {
			Type clazz = ForName(className, AppDomain.CurrentDomain.GetAssemblies());
            if (clazz == null) {
                String msg = "The className was not found: " + className + " assemblys=";
                msg = msg + Seasar.Framework.Util.ToStringUtil.ToString(AppDomain.CurrentDomain.GetAssemblies());
                throw new SystemException(msg);
            }
            System.Reflection.MethodInfo method = clazz.GetMethod("GetInstance");
            return (DBMeta)method.Invoke(null, null);
        }

        protected static Type ForName(string className, Assembly[] assemblys) {
            Type type = Type.GetType(className);
            if(type != null) return type;
            foreach(Assembly assembly in assemblys) {
                type = assembly.GetType(className);
                if(type != null) return type;
            }
            return type;
        }

        // Returns the unmodifiable map that contains all instances of DB meta. (NotNull & NotEmpty)
        public static Map<String, DBMeta> GetUnmodifiableDBMetaMap() {
            InitializeDBMetaMap();
            lock (_tableDbNameInstanceMap) {
                Map<String, DBMeta> copiedMap = new HashMap<String, DBMeta>();
                foreach (String tableDbName in _tableDbNameInstanceMap.keySet()) {
                    copiedMap.put(tableDbName, _tableDbNameInstanceMap.get(tableDbName));
                }
                return copiedMap;
            }
        }

        protected static void InitializeDBMetaMap() {
            if (IsInitialized) {
                return;
            }
            lock (_tableDbNameInstanceMap) {
                Set<String> tableDbNameSet = _tableDbNameClassNameMap.keySet();
                foreach (String tableDbName in tableDbNameSet) {
                    FindDBMeta(tableDbName); // Initialize!
                }
                if (!IsInitialized) {
                    String msg = "Failed to initialize tableDbNameInstanceMap:";
                    msg = msg + " tableDbNameInstanceMap=" + _tableDbNameInstanceMap;
                    throw new IllegalStateException(msg);
                }
            }
        }

        protected static bool IsInitialized { get {
            return _tableDbNameInstanceMap.size() == _tableDbNameClassNameMap.size();
        }}

        // ===============================================================================
        //                                                              Provider Singleton
        //                                                              ==================
        protected static readonly DBMetaProvider _provider = new DBMetaInstanceHandler();

        public static DBMetaProvider getProvider() {
            return _provider;
        }

        public DBMeta provideDBMeta(String tableFlexibleName) {
            return ByTableFlexibleName(tableFlexibleName);
        }

        public DBMeta provideDBMetaChecked(String tableFlexibleName) {
            return FindDBMeta(tableFlexibleName);
        }

        // ===============================================================================
        //                                                                     Find DBMeta
        //                                                                     ===========
        public static DBMeta FindDBMeta(String tableFlexibleName) { // accept quoted name and schema prefix
            DBMeta dbmeta = ByTableFlexibleName(tableFlexibleName);
            if (dbmeta == null) {
                String msg = "The DB meta was not found by the table flexible name: " + tableFlexibleName;
                msg = msg + " key=" + tableFlexibleName + " instanceMap=" + _tableDbNameInstanceMap;
                throw new DBMetaNotFoundException(msg);
            }
            return dbmeta;
        }

        // ===============================================================================
        //                                                                   By Table Name
        //                                                                   =============
        protected static DBMeta ByTableFlexibleName(String tableFlexibleName) {
            AssertStringNotNullAndNotTrimmedEmpty("tableFlexibleName", tableFlexibleName);
            tableFlexibleName = RemoveSchemaIfExists(tableFlexibleName);
            tableFlexibleName = RemoveQuoteIfExists(tableFlexibleName);
            if (_tableDbNameInstanceMap.containsKey(tableFlexibleName)) {
                return ByTableDbName(tableFlexibleName);
            }
            String toLowerKey = tableFlexibleName.ToLower();
            if (_tableDbNamePropertyNameKeyToLowerMap.containsKey(toLowerKey)) {
                String propertyName = (String)_tableDbNamePropertyNameKeyToLowerMap.get(toLowerKey);
                String dbName = (String)_tablePropertyNameDbNameKeyToLowerMap.get(propertyName.ToLower());
                return ByTableDbName(dbName);
            }
            if (_tablePropertyNameDbNameKeyToLowerMap.containsKey(toLowerKey)) {
                String dbName = (String)_tablePropertyNameDbNameKeyToLowerMap.get(toLowerKey);
                return ByTableDbName(dbName);
            }
            return null;
        }

        protected static String RemoveSchemaIfExists(String name) {
            int dotLastIndex = name.LastIndexOf(".");
            if (dotLastIndex >= 0) {
                name = name.Substring(dotLastIndex + 1);
            }
            return name;
        }

        protected static String RemoveQuoteIfExists(String name) {
            if (name.StartsWith("\"") && name.EndsWith("\"")) {
                name = name.Substring(1);
                name = name.Substring(0, name.Length - 1);
            } else if (name.StartsWith("[") && name.EndsWith("]")) {
                name = name.Substring(1);
                name = name.Substring(0, name.Length - 1);
            }
            return name;
        }

        protected static DBMeta ByTableDbName(String tableDbName) {
            AssertStringNotNullAndNotTrimmedEmpty("tableDbName", tableDbName);
            return GetCachedDBMeta(tableDbName);
        }

        // ===============================================================================
        //                                                                   Cached DBMeta
        //                                                                   =============
        protected static DBMeta GetCachedDBMeta(String tableDbName) { // lazy-load (thank you koyak!)
            DBMeta dbmeta = _tableDbNameInstanceMap.get(tableDbName);
            if (dbmeta != null) {
                return dbmeta;
            }
            lock (_tableDbNameInstanceMap) {
                dbmeta = _tableDbNameInstanceMap.get(tableDbName);
                if (dbmeta != null) {
                    return dbmeta;
                }
                String entityName = _tableDbNameClassNameMap.get(tableDbName);
                _tableDbNameInstanceMap.put(tableDbName, GetDBMeta(entityName));
                return _tableDbNameInstanceMap.get(tableDbName);
            }
        }

        // ===============================================================================
        //                                                                  General Helper
        //                                                                  ==============
        // -------------------------------------------------
        //                                     Assert Object
        //                                     -------------
        protected static void AssertObjectNotNull(String variableName, Object value) {
		    SimpleAssertUtil.AssertObjectNotNull(variableName, value);
        }

        // -------------------------------------------------
        //                                     Assert String
        //                                     -------------
        protected static void AssertStringNotNullAndNotTrimmedEmpty(String variableName, String value) {
            SimpleAssertUtil.AssertStringNotNullAndNotTrimmedEmpty(variableName, value);
        }
    }

    public class DBMetaNotFoundException : SystemException {

        public DBMetaNotFoundException(String msg)
        : base(msg) {}
    }
}
