
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
    public abstract class AbstractBsTRawdataImportQueInfoCQ : AbstractConditionQuery {

        public AbstractBsTRawdataImportQueInfoCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_RAWDATA_IMPORT_QUE_INFO"; }
        public override String getTableSqlName() { return "T_RAWDATA_IMPORT_QUE_INFO"; }

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
        public void ExistsTQcwebSurveyInfoList(SubQuery<TQcwebSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepRawdataImportQueInfoId_ExistsSubQuery_TQcwebSurveyInfoList(cb.Query());
            registerExistsSubQuery(cb.Query(), "RAWDATA_IMPORT_QUE_INFO_ID", "RAWDATA_IMPORT_QUE_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepRawdataImportQueInfoId_ExistsSubQuery_TQcwebSurveyInfoList(TQcwebSurveyInfoCQ subQuery);
        public void NotExistsTQcwebSurveyInfoList(SubQuery<TQcwebSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepRawdataImportQueInfoId_NotExistsSubQuery_TQcwebSurveyInfoList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "RAWDATA_IMPORT_QUE_INFO_ID", "RAWDATA_IMPORT_QUE_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepRawdataImportQueInfoId_NotExistsSubQuery_TQcwebSurveyInfoList(TQcwebSurveyInfoCQ subQuery);
        public void InScopeTQcwebSurveyInfoList(SubQuery<TQcwebSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepRawdataImportQueInfoId_InScopeSubQuery_TQcwebSurveyInfoList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "RAWDATA_IMPORT_QUE_INFO_ID", "RAWDATA_IMPORT_QUE_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepRawdataImportQueInfoId_InScopeSubQuery_TQcwebSurveyInfoList(TQcwebSurveyInfoCQ subQuery);
        public void NotInScopeTQcwebSurveyInfoList(SubQuery<TQcwebSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepRawdataImportQueInfoId_NotInScopeSubQuery_TQcwebSurveyInfoList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "RAWDATA_IMPORT_QUE_INFO_ID", "RAWDATA_IMPORT_QUE_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepRawdataImportQueInfoId_NotInScopeSubQuery_TQcwebSurveyInfoList(TQcwebSurveyInfoCQ subQuery);
        public void xsderiveTQcwebSurveyInfoList(String function, SubQuery<TQcwebSurveyInfoCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepRawdataImportQueInfoId_SpecifyDerivedReferrer_TQcwebSurveyInfoList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "RAWDATA_IMPORT_QUE_INFO_ID", "RAWDATA_IMPORT_QUE_INFO_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepRawdataImportQueInfoId_SpecifyDerivedReferrer_TQcwebSurveyInfoList(TQcwebSurveyInfoCQ subQuery);

        public QDRFunction<TQcwebSurveyInfoCB> DerivedTQcwebSurveyInfoList() {
            return xcreateQDRFunctionTQcwebSurveyInfoList();
        }
        protected QDRFunction<TQcwebSurveyInfoCB> xcreateQDRFunctionTQcwebSurveyInfoList() {
            return new QDRFunction<TQcwebSurveyInfoCB>(delegate(String function, SubQuery<TQcwebSurveyInfoCB> subQuery, String operand, Object value) {
                xqderiveTQcwebSurveyInfoList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTQcwebSurveyInfoList(String function, SubQuery<TQcwebSurveyInfoCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepRawdataImportQueInfoId_QueryDerivedReferrer_TQcwebSurveyInfoList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepRawdataImportQueInfoId_QueryDerivedReferrer_TQcwebSurveyInfoListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "RAWDATA_IMPORT_QUE_INFO_ID", "RAWDATA_IMPORT_QUE_INFO_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepRawdataImportQueInfoId_QueryDerivedReferrer_TQcwebSurveyInfoList(TQcwebSurveyInfoCQ subQuery);
        public abstract String keepRawdataImportQueInfoId_QueryDerivedReferrer_TQcwebSurveyInfoListParameter(Object parameterValue);
        public void SetRawdataImportQueInfoId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRawdataImportQueInfoId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetRawdataImportQueInfoId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRawdataImportQueInfoId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regRawdataImportQueInfoId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueRawdataImportQueInfoId(), "RAWDATA_IMPORT_QUE_INFO_ID");
        }
        protected abstract ConditionValue getCValueRawdataImportQueInfoId();

        public void SetQcwebJobNo_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQcwebJobNo_Equal(fRES(v));
        }
        protected void DoSetQcwebJobNo_Equal(String v) { regQcwebJobNo(CK_EQ, v); }
        public void SetQcwebJobNo_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQcwebJobNo_NotEqual(fRES(v));
        }
        protected void DoSetQcwebJobNo_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQcwebJobNo(CK_NES, v);
        }
        public void SetQcwebJobNo_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQcwebJobNo(CK_GT, fRES(v));
        }
        public void SetQcwebJobNo_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQcwebJobNo(CK_LT, fRES(v));
        }
        public void SetQcwebJobNo_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQcwebJobNo(CK_GE, fRES(v));
        }
        public void SetQcwebJobNo_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQcwebJobNo(CK_LE, fRES(v));
        }
        public void SetQcwebJobNo_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQcwebJobNo(), "QCWEB_JOB_NO");
        }
        public void SetQcwebJobNo_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQcwebJobNo(), "QCWEB_JOB_NO");
        }
        public void SetQcwebJobNo_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQcwebJobNo_LikeSearch(v, cLSOP());
        }
        public void SetQcwebJobNo_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQcwebJobNo(), "QCWEB_JOB_NO", option);
        }
        public void SetQcwebJobNo_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQcwebJobNo(), "QCWEB_JOB_NO", option);
        }
        protected void regQcwebJobNo(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQcwebJobNo(), "QCWEB_JOB_NO");
        }
        protected abstract ConditionValue getCValueQcwebJobNo();

        public void SetMainSurveyId_Equal(decimal? v) { regMainSurveyId(CK_EQ, v); }
        public void SetMainSurveyId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMainSurveyId(CK_NES, v);
        }
        public void SetMainSurveyId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMainSurveyId(CK_GT, v);
        }
        public void SetMainSurveyId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMainSurveyId(CK_LT, v);
        }
        public void SetMainSurveyId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMainSurveyId(CK_GE, v);
        }
        public void SetMainSurveyId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regMainSurveyId(CK_LE, v);
        }
        public void SetMainSurveyId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueMainSurveyId(), "MAIN_SURVEY_ID");
        }
        public void SetMainSurveyId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueMainSurveyId(), "MAIN_SURVEY_ID");
        }
        protected void regMainSurveyId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueMainSurveyId(), "MAIN_SURVEY_ID");
        }
        protected abstract ConditionValue getCValueMainSurveyId();

        public void SetSurveyDataType_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetSurveyDataType_Equal(fRES(v));
        }
        /// <summary>
        /// Set the value of NORMAL of surveyDataType as equal. { = }
        /// 標準納品ファイル: 標準納品ファイルを示す
        /// </summary>
        public void SetSurveyDataType_Equal_NORMAL() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetSurveyDataType_Equal(CDef.SurveyDataType.NORMAL.Code);
        }
        /// <summary>
        /// Set the value of ADD of surveyDataType as equal. { = }
        /// 追加納品ファイル: 追加納品ファイルを示す
        /// </summary>
        public void SetSurveyDataType_Equal_ADD() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetSurveyDataType_Equal(CDef.SurveyDataType.ADD.Code);
        }
        protected void DoSetSurveyDataType_Equal(String v) { regSurveyDataType(CK_EQ, v); }
        public void SetSurveyDataType_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetSurveyDataType_NotEqual(fRES(v));
        }
        /// <summary>
        /// Set the value of NORMAL of surveyDataType as notEqual. { &lt;&gt; }
        /// 標準納品ファイル: 標準納品ファイルを示す
        /// </summary>
        public void SetSurveyDataType_NotEqual_NORMAL() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetSurveyDataType_NotEqual(CDef.SurveyDataType.NORMAL.Code);
        }
        /// <summary>
        /// Set the value of ADD of surveyDataType as notEqual. { &lt;&gt; }
        /// 追加納品ファイル: 追加納品ファイルを示す
        /// </summary>
        public void SetSurveyDataType_NotEqual_ADD() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetSurveyDataType_NotEqual(CDef.SurveyDataType.ADD.Code);
        }
        protected void DoSetSurveyDataType_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyDataType(CK_NES, v);
        }
        public void SetSurveyDataType_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueSurveyDataType(), "SURVEY_DATA_TYPE");
        }
        public void SetSurveyDataType_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueSurveyDataType(), "SURVEY_DATA_TYPE");
        }
        protected void regSurveyDataType(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueSurveyDataType(), "SURVEY_DATA_TYPE");
        }
        protected abstract ConditionValue getCValueSurveyDataType();

        public void SetFilepath_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFilepath_Equal(fRES(v));
        }
        protected void DoSetFilepath_Equal(String v) { regFilepath(CK_EQ, v); }
        public void SetFilepath_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFilepath_NotEqual(fRES(v));
        }
        protected void DoSetFilepath_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFilepath(CK_NES, v);
        }
        public void SetFilepath_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFilepath(CK_GT, fRES(v));
        }
        public void SetFilepath_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFilepath(CK_LT, fRES(v));
        }
        public void SetFilepath_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFilepath(CK_GE, fRES(v));
        }
        public void SetFilepath_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFilepath(CK_LE, fRES(v));
        }
        public void SetFilepath_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFilepath(), "FILEPATH");
        }
        public void SetFilepath_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFilepath(), "FILEPATH");
        }
        public void SetFilepath_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFilepath_LikeSearch(v, cLSOP());
        }
        public void SetFilepath_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFilepath(), "FILEPATH", option);
        }
        public void SetFilepath_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFilepath(), "FILEPATH", option);
        }
        protected void regFilepath(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFilepath(), "FILEPATH");
        }
        protected abstract ConditionValue getCValueFilepath();

        public void SetFileName_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFileName_Equal(fRES(v));
        }
        protected void DoSetFileName_Equal(String v) { regFileName(CK_EQ, v); }
        public void SetFileName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFileName_NotEqual(fRES(v));
        }
        protected void DoSetFileName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFileName(CK_NES, v);
        }
        public void SetFileName_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFileName(CK_GT, fRES(v));
        }
        public void SetFileName_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFileName(CK_LT, fRES(v));
        }
        public void SetFileName_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFileName(CK_GE, fRES(v));
        }
        public void SetFileName_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFileName(CK_LE, fRES(v));
        }
        public void SetFileName_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFileName(), "FILE_NAME");
        }
        public void SetFileName_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFileName(), "FILE_NAME");
        }
        public void SetFileName_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFileName_LikeSearch(v, cLSOP());
        }
        public void SetFileName_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFileName(), "FILE_NAME", option);
        }
        public void SetFileName_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFileName(), "FILE_NAME", option);
        }
        protected void regFileName(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFileName(), "FILE_NAME");
        }
        protected abstract ConditionValue getCValueFileName();

        public void SetImportStatus_Equal(int? v) { regImportStatus(CK_EQ, v); }
        /// <summary>
        /// Set the value of NONE_IMPORT of importStatus as equal. { = }
        /// 未取込: 未取込を示す
        /// </summary>
        public void SetImportStatus_Equal_NONE_IMPORT() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.ImportStatus.NONE_IMPORT.Code;
            regImportStatus(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of IMPORT_EXEC of importStatus as equal. { = }
        /// 取込中: 取込中を示す
        /// </summary>
        public void SetImportStatus_Equal_IMPORT_EXEC() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.ImportStatus.IMPORT_EXEC.Code;
            regImportStatus(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of IMPORT_END_ZIP of importStatus as equal. { = }
        /// 取込完(パスワード付きZIP): 取込完(パスワード付きZIP)を示す
        /// </summary>
        public void SetImportStatus_Equal_IMPORT_END_ZIP() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.ImportStatus.IMPORT_END_ZIP.Code;
            regImportStatus(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of IMPORT_END of importStatus as equal. { = }
        /// 取込完: 取込完を示す
        /// </summary>
        public void SetImportStatus_Equal_IMPORT_END() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.ImportStatus.IMPORT_END.Code;
            regImportStatus(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of IMPORT_ERROR of importStatus as equal. { = }
        /// エラー: エラーありを示す
        /// </summary>
        public void SetImportStatus_Equal_IMPORT_ERROR() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.ImportStatus.IMPORT_ERROR.Code;
            regImportStatus(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of IMPORT_SKIP of importStatus as equal. { = }
        /// スキップ: 処理をスキップしたことを示す
        /// </summary>
        public void SetImportStatus_Equal_IMPORT_SKIP() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.ImportStatus.IMPORT_SKIP.Code;
            regImportStatus(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of IMPORT_END_PART_ERROR of importStatus as equal. { = }
        /// 取込完(一部エラーあり): 取込完(一部エラーあり)を示す
        /// </summary>
        public void SetImportStatus_Equal_IMPORT_END_PART_ERROR() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.ImportStatus.IMPORT_END_PART_ERROR.Code;
            regImportStatus(CK_EQ, int.Parse(code));
        }
        public void SetImportStatus_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regImportStatus(CK_NES, v);
        }
        /// <summary>
        /// Set the value of NONE_IMPORT of importStatus as notEqual. { &lt;&gt; }
        /// 未取込: 未取込を示す
        /// </summary>
        public void SetImportStatus_NotEqual_NONE_IMPORT() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.ImportStatus.NONE_IMPORT.Code;
            regImportStatus(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of IMPORT_EXEC of importStatus as notEqual. { &lt;&gt; }
        /// 取込中: 取込中を示す
        /// </summary>
        public void SetImportStatus_NotEqual_IMPORT_EXEC() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.ImportStatus.IMPORT_EXEC.Code;
            regImportStatus(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of IMPORT_END_ZIP of importStatus as notEqual. { &lt;&gt; }
        /// 取込完(パスワード付きZIP): 取込完(パスワード付きZIP)を示す
        /// </summary>
        public void SetImportStatus_NotEqual_IMPORT_END_ZIP() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.ImportStatus.IMPORT_END_ZIP.Code;
            regImportStatus(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of IMPORT_END of importStatus as notEqual. { &lt;&gt; }
        /// 取込完: 取込完を示す
        /// </summary>
        public void SetImportStatus_NotEqual_IMPORT_END() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.ImportStatus.IMPORT_END.Code;
            regImportStatus(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of IMPORT_ERROR of importStatus as notEqual. { &lt;&gt; }
        /// エラー: エラーありを示す
        /// </summary>
        public void SetImportStatus_NotEqual_IMPORT_ERROR() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.ImportStatus.IMPORT_ERROR.Code;
            regImportStatus(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of IMPORT_SKIP of importStatus as notEqual. { &lt;&gt; }
        /// スキップ: 処理をスキップしたことを示す
        /// </summary>
        public void SetImportStatus_NotEqual_IMPORT_SKIP() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.ImportStatus.IMPORT_SKIP.Code;
            regImportStatus(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of IMPORT_END_PART_ERROR of importStatus as notEqual. { &lt;&gt; }
        /// 取込完(一部エラーあり): 取込完(一部エラーあり)を示す
        /// </summary>
        public void SetImportStatus_NotEqual_IMPORT_END_PART_ERROR() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.ImportStatus.IMPORT_END_PART_ERROR.Code;
            regImportStatus(CK_NES, int.Parse(code));
        }
        public void SetImportStatus_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueImportStatus(), "IMPORT_STATUS");
        }
        public void SetImportStatus_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueImportStatus(), "IMPORT_STATUS");
        }
        protected void regImportStatus(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueImportStatus(), "IMPORT_STATUS");
        }
        protected abstract ConditionValue getCValueImportStatus();

        public void SetMessage_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetMessage_Equal(fRES(v));
        }
        protected void DoSetMessage_Equal(String v) { regMessage(CK_EQ, v); }
        public void SetMessage_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetMessage_NotEqual(fRES(v));
        }
        protected void DoSetMessage_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMessage(CK_NES, v);
        }
        public void SetMessage_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMessage(CK_GT, fRES(v));
        }
        public void SetMessage_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMessage(CK_LT, fRES(v));
        }
        public void SetMessage_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMessage(CK_GE, fRES(v));
        }
        public void SetMessage_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMessage(CK_LE, fRES(v));
        }
        public void SetMessage_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueMessage(), "MESSAGE");
        }
        public void SetMessage_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueMessage(), "MESSAGE");
        }
        public void SetMessage_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetMessage_LikeSearch(v, cLSOP());
        }
        public void SetMessage_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueMessage(), "MESSAGE", option);
        }
        public void SetMessage_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueMessage(), "MESSAGE", option);
        }
        public void SetMessage_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMessage(CK_ISN, DUMMY_OBJECT);
        }
        public void SetMessage_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMessage(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regMessage(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueMessage(), "MESSAGE");
        }
        protected abstract ConditionValue getCValueMessage();

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
        public void InScopeTQcwebSurveyInfo(SubQuery<TQcwebSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_InScopeSubQuery_TQcwebSurveyInfo(cb.Query());
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_InScopeSubQuery_TQcwebSurveyInfo(TQcwebSurveyInfoCQ subQuery);
        public void NotInScopeTQcwebSurveyInfo(SubQuery<TQcwebSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfo(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfo(TQcwebSurveyInfoCQ subQuery);
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

        public void SetRequestDatetime_Equal(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRequestDatetime(CK_EQ, v);
        }
        public void SetRequestDatetime_GreaterThan(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRequestDatetime(CK_GT, v);
        }
        public void SetRequestDatetime_LessThan(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRequestDatetime(CK_LT, v);
        }
        public void SetRequestDatetime_GreaterEqual(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRequestDatetime(CK_GE, v);
        }
        public void SetRequestDatetime_LessEqual(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRequestDatetime(CK_LE, v);
        }
        public void SetRequestDatetime_FromTo(DateTime? from, DateTime? to, FromToOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFTQ(from, to, getCValueRequestDatetime(), "REQUEST_DATETIME", option);
        }
        public void SetRequestDatetime_DateFromTo(DateTime? from, DateTime? to) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetRequestDatetime_FromTo(from, to, new DateFromToOption());
        }
        public void SetRequestDatetime_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRequestDatetime(CK_ISN, DUMMY_OBJECT);
        }
        public void SetRequestDatetime_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRequestDatetime(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regRequestDatetime(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueRequestDatetime(), "REQUEST_DATETIME");
        }
        protected abstract ConditionValue getCValueRequestDatetime();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TRawdataImportQueInfoCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TRawdataImportQueInfoCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TRawdataImportQueInfoCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TRawdataImportQueInfoCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TRawdataImportQueInfoCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TRawdataImportQueInfoCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TRawdataImportQueInfoCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TRawdataImportQueInfoCB>(delegate(String function, SubQuery<TRawdataImportQueInfoCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TRawdataImportQueInfoCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TRawdataImportQueInfoCB>", subQuery);
            TRawdataImportQueInfoCB cb = new TRawdataImportQueInfoCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TRawdataImportQueInfoCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TRawdataImportQueInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TRawdataImportQueInfoCB>", subQuery);
            TRawdataImportQueInfoCB cb = new TRawdataImportQueInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "RAWDATA_IMPORT_QUE_INFO_ID", "RAWDATA_IMPORT_QUE_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TRawdataImportQueInfoCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
